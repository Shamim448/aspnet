
using System.Collections;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;

public class TestORM<G, T> where T : IEntity<G>
{
    public readonly string _connectionString;
    public readonly DataUtility _dataUtility;
    public string DbConnection = "Server=.\\SQLEXPRESS;Database=Aspnetb8;User Id=aspnetb8;Password=123456;";
    public TestORM(string connectionString)
    {
        _connectionString = connectionString;
        _dataUtility = new DataUtility(DbConnection);

    }


    public void Insert(T item)
    {
        Type type = typeof(T);
        

        BasicObjectInsert(item);
    
    }
    public  void BasicObjectInsert(object item, object p = null, object baseClass = null)
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
        if(baseClass != null)
        {
            var foreignKeyProperty = properties.FirstOrDefault(p => p.Name == $"{baseClass}Id");
            // var foreignKeyProperty =  $"{baseClass}Id";
            if (foreignKeyProperty != null)
            {
                foreignKeyProperty.SetValue(item, p);
            }
        }
        
        foreach (var property in properties)
        {
            // Get the foreign key object's ID property value
            

            cmd.Parameters.AddWithValue($"@{property.Name}", property.GetValue(item)); 
        }
        cmd.ExecuteNonQuery();

        PropertyInfo[] ChieldProperty = type.GetProperties();

        var primaryKeyProperty = properties.FirstOrDefault(p => p.Name == "Id");
        if (primaryKeyProperty == null)
        {
            throw new ArgumentException($"Type '{type.Name}' does not have a primary key property named 'Id'");
        }

        var primaryKeyValue = primaryKeyProperty.GetValue(item);
        if (primaryKeyValue == null)
        {
            throw new ArgumentException($"Primary key value for type '{type.Name}' cannot be null");
        }
        baseClass = type.Name;

        foreach (var property in ChieldProperty)
        {

            var values = property.GetValue(item);
            if ((property.PropertyType.IsGenericType || values is IList) && values != null)
            {
                IList list = (IList)values;
                foreach (var value in list)
                {
                    BasicObjectInsert(value, primaryKeyValue, baseClass);

                }
            }
            else if (property.PropertyType.IsClass && values != null && property.PropertyType != typeof(string))
            {
                BasicObjectInsert(values, primaryKeyValue, baseClass);
            }
        }

    }



    public void Update(T entity)
    {
        Type type = entity.GetType();
        var tableName = type.Name;
        EntityInfo entityInfo = new EntityInfo(entity);
        string columnName = string.Join(", ", entityInfo.GetProperties().Select(p => $"{p.Name} = @{p.Name}"));
        string sql = $"Update {tableName} set {columnName} where Id = @Id";
        SqlConnection connection = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand(sql, connection);
        //initialize value in parameters
        foreach (var property in entityInfo.GetProperties())
        {
            cmd.Parameters.AddWithValue($"@{property.Name}", property.GetValue(entity));
            //check value get or not
            var value = property.GetValue(entity);
        }
        try
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
                cmd.Parameters.AddWithValue("@Id", entity.Id);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Record Update Successfully");
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error Generated. Details: " + ex.ToString());
        }
        finally
        {
            connection.Close();
        }
    }

    public void Delete(G id)
    {
        #region Delete_By _ID
        Type type = typeof(T);
        var tableName = type.Name;
        string sql = $"Delete From {tableName} Where Id = '{id}'";
        _dataUtility.ExecuteCommand(sql);

        #endregion
    }
    public void GetById(G id)
    {
        #region Print_Value_By_Id
        Type type = typeof(T);
        var tableName = type.Name;
        //sql query for select all data
        string sql = $"Select * from {tableName} Where Id = '{id}'";
        _dataUtility.ReadData(sql);
        #endregion
    }

    public void GetAll()
    {
        #region Get_All_Table_Data
        Type type = typeof(T);
        GetAll(type);
        
        #endregion
    }
    public void GetAll(object item)
    {
        Type type = item.GetType();
        var tableName = type.Name;
        PropertyInfo[] properties = type.GetProperties();

        foreach (var property in properties)
        {

            var values = property.GetValue(item);
            if ((property.PropertyType.IsGenericType || values is IList) && values != null)
            {
                IList list = (IList)values;
                foreach (var value in list)
                {
                    GetAll(value);

                }
            }
            else if (property.PropertyType.IsClass && values != null && property.PropertyType != typeof(string))
            {
                GetAll(values);
            }
        }
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

    


}
