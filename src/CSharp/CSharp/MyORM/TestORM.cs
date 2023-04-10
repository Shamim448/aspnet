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
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            // Insert the main object
            var command = new SqlCommand($"INSERT INTO {typeof(T).Name} DEFAULT VALUES; SELECT SCOPE_IDENTITY()", connection);
            var id = (G)command.ExecuteScalar();
            item.Id = id;

            // Recursively insert child objects and collections
            InsertChildObjects(item, connection);

            connection.Close();
        }
    }

    private void InsertChildObjects(object obj, SqlConnection connection)
    {
        var type = obj.GetType();
        var properties = type.GetProperties();

        foreach (var property in properties)
        {
            if (property.PropertyType.IsPrimitive || property.PropertyType == typeof(string))
            {
                // If the property is a primitive type or a string, insert it as a parameter
                var value = property.GetValue(obj);
                if (value != null)
                {
                    var parameter = new SqlParameter($"@{property.Name}", value);
                    var command = new SqlCommand($"UPDATE {type.Name} SET {property.Name} = @{property.Name} WHERE Id = @Id", connection);
                    command.Parameters.Add(parameter);
                    command.Parameters.AddWithValue("@Id", ((IIdBase<G>)obj).Id);
                    command.ExecuteNonQuery();
                }
            }
            else if (property.PropertyType.GetInterfaces().Contains(typeof(IEnumerable)) && property.PropertyType != typeof(string))
            {
                // If the property is a collection, recursively insert each item in the collection
                var collection = (IEnumerable)property.GetValue(obj);
                if (collection != null)
                {
                    foreach (var item in collection)
                    {
                        InsertChildObjects(item, connection);

                        // Insert the relationship between the parent and child object
                        var childType = item.GetType();
                        var childId = ((IIdBase<G>)item).Id;
                        var command = new SqlCommand($"UPDATE {type.Name} SET {childType.Name}Id = @childId WHERE Id = @parentId", connection);
                        command.Parameters.AddWithValue("@childId", childId);
                        command.Parameters.AddWithValue("@parentId", ((IIdBase<G>)obj).Id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            else if (typeof(IIdBase<G>).IsAssignableFrom(property.PropertyType))
            {
                // If the property is a nested object, recursively insert it
                var child = property.GetValue(obj);
                if (child != null)
                {
                    InsertChildObjects(child, connection);

                    // Insert the relationship between the parent and child object
                    var childType = child.GetType();
                    var childId = ((IIdBase<G>)child).Id;
                    var command = new SqlCommand($"UPDATE {type.Name} SET {childType.Name}Id = @childId WHERE Id = @parentId", connection);
                    command.Parameters.AddWithValue("@childId", childId);
                    command.Parameters.AddWithValue("@parentId", ((IIdBase<G>)obj).Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }




    //public void Insert (T entity)
    //{
    //   using (var connection = new SqlConnection(_connectionString))
    //    {
    //        connection.Open();
    //        //using(var transection  = connection.BeginTransaction())
    //        //{
    //            try
    //            {
    //                InsertItem(entity, connection /*transection*/);
    //                //transection.Commit();
    //            }
    //            catch (Exception ex)
    //            {
    //                //transection.Rollback();
    //                throw ex;
    //            }
    //            finally { 
    //                //transection.Dispose();
    //                connection.Close();
    //            }
    //        //}
    //    }
    //}

    //public void InsertItem(object item, SqlConnection connection /*SqlTransaction transection*/)
    //{
    //    var itemType = item.GetType();
    //    var tableName = itemType.Name;
    //    var properties = itemType.GetProperties();
    //    var sql = $"INSERT INTO {tableName} ({GetColumnList(properties)}) VALUES({GetParameterList(properties)})";
    //    using (var command = new SqlCommand(sql, connection /*transection*/))
    //    {
    //        foreach (var property in properties)
    //        {
    //            var value = property.GetValue(item);
    //            var parameter = new SqlParameter($"@{property.Name}", value ?? DBNull.Value);
    //            command.Parameters.Add(parameter);
    //        }
    //        command.ExecuteNonQuery();
    //    }
    //    //Nested object handle
    //    foreach (var property in properties)
    //    {
    //        if(property.PropertyType.IsClass && property.PropertyType != typeof(string))
    //        {
    //            if(property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
    //            {
    //                var list = (IEnumerable<object>)property.GetValue(item);
    //                if(list != null)
    //                {
    //                    foreach(var itemlist  in list)
    //                    {
    //                        InsertItem(itemlist, connection /*transection*/);
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                var nestedItem = property.GetValue(item);
    //                if(nestedItem != null)
    //                {
    //                    InsertItem(nestedItem, connection /*transection*/);
    //                }
    //            }
    //        }
    //    }
    //}
    //public string GetColumnList(PropertyInfo[] properties)
    //{
    //    var column = new List<string>();
    //    foreach (var property in properties)
    //    {
    //        column.Add(property.Name);
    //    }
    //    return string.Join(", ", column);
    //}
    //public string GetParameterList(PropertyInfo[] properties)
    //{
    //    var parameter = new List<string>();
    //    foreach (var property in properties)
    //    {
    //        parameter.Add($"@{property.Name}");
    //    }
    //    return string.Join(", ", parameter);
    //}

    //public void Inserts(T item)
    //{
    //    using (var connection = new SqlConnection(_connectionString))
    //    {
    //        connection.Open();
    //        var transaction = connection.BeginTransaction();
    //        try
    //        {
    //            // Insert the item
    //            var type = item.GetType();
    //            var properties = type.GetProperties().Where(p => p.Name != "Id");
    //            var columns = string.Join(", ", properties.Select(p => $"[{p.Name}]"));
    //            var values = string.Join(", ", properties.Select(p => $"@{p.Name}"));
    //            var commandText = $"INSERT INTO [{type.Name}] ({columns}) VALUES ({values})";
    //            var command = new SqlCommand(commandText, connection, transaction);
    //            foreach (var property in properties)
    //            {
    //                command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(item) ?? DBNull.Value);
    //            }
    //            var id = (G)command.ExecuteScalar();
    //            type.GetProperty("Id").SetValue(item, id);

    //            // Insert the nested objects recursively
    //            foreach (var property in properties)
    //            {
    //                if (property.PropertyType == typeof(List<>))
    //                {
    //                    var list = (IEnumerable)property.GetValue(item);
    //                    if (list != null)
    //                    {
    //                        foreach (var nestedItem in list)
    //                        {
    //                            var nestedType = nestedItem.GetType();
    //                            nestedType.GetProperty($"{type.Name}Id").SetValue(nestedItem, id);
    //                            var myOrmType = typeof(MyORM<,>).MakeGenericType(typeof(G), nestedType);
    //                            var myOrm = Activator.CreateInstance(myOrmType, _connectionString);
    //                            myOrmType.GetMethod("Insert").Invoke(myOrm, new[] { nestedItem });
    //                        }
    //                    }
    //                }
    //            }

    //            transaction.Commit();
    //        }
    //        catch
    //        {
    //            transaction.Rollback();
    //            throw;
    //        }
    //    }
    //}


}