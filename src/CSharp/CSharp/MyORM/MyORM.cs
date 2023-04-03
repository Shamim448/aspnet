using System.Collections;
using System.Data.SqlClient;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;
public class MyORM<G, T> where T : IIdBase<G>
{
    public readonly string _connectionString;
    public MyORM(string connectionString)
    {
        _connectionString = connectionString;
    }
   
    //Insert value
    public int Insert(T entity)
    {
        var tablename = typeof(T).Name;

        //StringBuilder columnName = new StringBuilder();
        PropertyInfo[] properties = typeof(T).GetProperties().Where(p => p.Name != "Id").ToArray();
       
        //collect table column name
        string columnName = string.Join(", ", properties.Select(c => c.Name));
        //column name set as a parameter name
        string parametersName = string.Join(", ", properties.Select(p => $"@{p.Name}"));
        
        string sql = $"Insert into {tablename} ({columnName}) Values({parametersName})";
        using SqlConnection connection = new SqlConnection(_connectionString) ;
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            using SqlCommand cmd = new SqlCommand(sql, connection);
            foreach(var property in properties)
            {
                cmd.Parameters.AddWithValue($"@{property.Name}", property.GetValue(entity));
                
                var a = property.GetValue(entity);
            }
            int effected = cmd.ExecuteNonQuery();
          return effected;
    }
}