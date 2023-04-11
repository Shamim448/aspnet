using System.Collections;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;

public class TestORM<G, T> where T : IIdBase<G>
{
    private string _connectionString;
    public TestORM(string connectionString)
    {
        _connectionString = connectionString;
    }
    public void Insert(T item)
    {
        Type type = typeof(T);
        BasicObjectInsert(item);

        PropertyInfo[] propertiesOfList = type.GetProperties();
        foreach (var property in propertiesOfList)
        {
            var values = property.GetValue(item);
            if ((property.PropertyType.IsGenericType || values is IList) && values != null)
            {
                IList list = (IList) values;
                foreach (var value in list)
                {
                    BasicObjectInsert(value);
                }
            }  
             else if(property.PropertyType.IsClass && values != null && property.PropertyType != typeof(string))
             {
                BasicObjectInsert(values);
            }
        }        
    }
    public  void BasicObjectInsert(object item)
    {
        Type type = item.GetType();
        var tableName = type.Name;
        PropertyInfo[] properties = type.GetProperties().Where(p => !p.PropertyType.IsGenericType &&
        !p.PropertyType.IsClass || p.PropertyType == typeof(string)).ToArray();
        string columnName = string.Join(", ", properties.Select(c => c.Name));
        string parametersName = string.Join(", ", properties.Select(p => $"@{p.Name}"));
        string sql = $"Insert into {tableName} ({columnName}) Values({parametersName})";

        var connection = new SqlConnection(_connectionString);

        connection.Open();
        SqlCommand cmd = new SqlCommand(sql, connection);
        foreach (var property in properties)
        {
            cmd.Parameters.AddWithValue($"@{property.Name}", property.GetValue(item));
        }
        Console.WriteLine(sql);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Record Insert Successfully");
        
    }
    //public void ChieldObjectInsert(object item)
    //{
    //    Type type = item.GetType();
    //    var tableName = type.Name;
    //    PropertyInfo[] properties = type.GetProperties().Where(p => !p.PropertyType.IsGenericType &&
    //    !p.PropertyType.IsClass || p.PropertyType == typeof(string)).ToArray();
    //    string columnName = string.Join(", ", properties.Select(c => c.Name));
    //    string parametersName = string.Join(", ", properties.Select(p => $"@{p.Name}"));
    //    string sql = $"Insert into {tableName} ({columnName}) Values({parametersName})";

    //    using (var connection = new SqlConnection(_connectionString))
    //    {
    //        connection.Open();
    //        SqlCommand cmd = new SqlCommand(sql, connection);       
    //        foreach (var property in properties)
    //        {
    //            cmd.Parameters.AddWithValue($"@{property.Name}", property.GetValue(item));                
    //        }
    //        //cmd.Parameters.AddWithValue($"@CourseId", item(item));
    //        Console.WriteLine(sql);
    //        cmd.ExecuteNonQuery();           
    //    }
    //}
   






    public void Inserttt(T entity)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            //using(var transection  = connection.BeginTransaction())
            //{
            try
            {
                InsertItem(entity, connection /*transection*/);
                //transection.Commit();
            }
            catch (Exception ex)
            {
                //transection.Rollback();
                throw ex;
            }
            finally
            {
                //transection.Dispose();
                connection.Close();
            }
            //}
        }
    }

    public void InsertItem(object item, SqlConnection connection /*SqlTransaction transection*/)
    {
        var itemType = item.GetType();
        var tableName = itemType.Name;
        var properties = itemType.GetProperties();
        var sql = $"INSERT INTO {tableName} ({GetColumnList(properties)}) VALUES({GetParameterList(properties)})";
        using (var command = new SqlCommand(sql, connection /*transection*/))
        {
            foreach (var property in properties)
            {
                var value = property.GetValue(item);
                var parameter = new SqlParameter($"@{property.Name}", value ?? DBNull.Value);
                command.Parameters.Add(parameter);
            }
            command.ExecuteNonQuery();
        }
        //Nested object handle
        foreach (var property in properties)
        {
            if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
            {
                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    var list = (IEnumerable<object>)property.GetValue(item);
                    if (list != null)
                    {
                        foreach (var itemlist in list)
                        {
                            InsertItem(itemlist, connection /*transection*/);
                        }
                    }
                }
                else
                {
                    var nestedItem = property.GetValue(item);
                    if (nestedItem != null)
                    {
                        InsertItem(nestedItem, connection /*transection*/);
                    }
                }
            }
        }
    }
    public string GetColumnList(PropertyInfo[] properties)
    {
        var column = new List<string>();
        foreach (var property in properties)
        {
            column.Add(property.Name);
        }
        return string.Join(", ", column);
    }
    public string GetParameterList(PropertyInfo[] properties)
    {
        var parameter = new List<string>();
        foreach (var property in properties)
        {
            parameter.Add($"@{property.Name}");
        }
        return string.Join(", ", parameter);
    }

    public void Inserts(T item)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var transaction = connection.BeginTransaction();
            try
            {
                // Insert the item
                var type = item.GetType();
                var properties = type.GetProperties().Where(p => p.Name != "Id");
                var columns = string.Join(", ", properties.Select(p => $"[{p.Name}]"));
                var values = string.Join(", ", properties.Select(p => $"@{p.Name}"));
                var commandText = $"INSERT INTO [{type.Name}] ({columns}) VALUES ({values})";
                var command = new SqlCommand(commandText, connection, transaction);
                foreach (var property in properties)
                {
                    command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(item) ?? DBNull.Value);
                }
                var id = (G)command.ExecuteScalar();
                type.GetProperty("Id").SetValue(item, id);

                // Insert the nested objects recursively
                foreach (var property in properties)
                {
                    if (property.PropertyType == typeof(List<>))
                    {
                        var list = (IEnumerable)property.GetValue(item);
                        if (list != null)
                        {
                            foreach (var nestedItem in list)
                            {
                                var nestedType = nestedItem.GetType();
                                nestedType.GetProperty($"{type.Name}Id").SetValue(nestedItem, id);
                                var myOrmType = typeof(MyORM<,>).MakeGenericType(typeof(G), nestedType);
                                var myOrm = Activator.CreateInstance(myOrmType, _connectionString);
                                myOrmType.GetMethod("Insert").Invoke(myOrm, new[] { nestedItem });
                            }
                        }
                    }
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }


}