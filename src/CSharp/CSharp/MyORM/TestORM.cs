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

    public void Insert (T entity)
    {
       using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using(var transection  = connection.BeginTransaction())
            {
                try
                {
                    InsertItem(entity, connection, transection);
                    transection.Commit();
                }
                catch (Exception ex)
                {
                    transection.Rollback();
                    throw ex;
                }
                finally { 
                    transection.Dispose();
                    connection.Close();
                }
            }
        }
    }

    public void InsertItem(object item, SqlConnection connection, SqlTransaction transection)
    {
        var itemType = item.GetType();
        var tableName = itemType.Name;
        var properties = itemType.GetProperties();
        var sql = $"INSERT INTO {tableName} ({GetColumnList(properties)}) VALUES({GetParameterList(properties)})";
        using (var command = new SqlCommand(sql, connection, transection))
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
            if(property.PropertyType.IsClass && property.PropertyType != typeof(string))
            {
                if(property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    var list = (IEnumerable<object>)property.GetValue(item);
                    if(list != null)
                    {
                        foreach(var itemlist  in list)
                        {
                            InsertItem(itemlist, connection, transection);
                        }
                    }
                }
                else
                {
                    var nestedItem = property.GetValue(item);
                    if(nestedItem != null)
                    {
                        InsertItem(nestedItem, connection, transection);
                    }
                }
            }
        }
    }
    public string GetColumnList(PropertyInfo[] properties)
    {
        return "";
    }
    public string GetParameterList(PropertyInfo[] properties)
    {
        return "";
    }
}