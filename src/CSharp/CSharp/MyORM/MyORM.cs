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
    public void Insert(T entity)
    {
        var tablename = typeof(T).Name;
        //StringBuilder columnName = new StringBuilder();
        PropertyInfo[] properties = typeof(T).GetProperties().Where(p => p.Name != "Id").ToArray();
       
        //collect table column name
        string columnName = string.Join(", ", properties.Select(c => c.Name));
        //column name set as a parameter name
        string parametersName = string.Join(", ", properties.Select(p => $"@{p.Name}"));
        //sql query
        string sql = $"Insert into {tablename} ({columnName}) Values({parametersName})";
        //call sqlconnection and open connection 
        using SqlConnection connection = new SqlConnection(_connectionString) ;
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            //instialize sqlcommend it takes a sql query and a sqlconnection
            using SqlCommand cmd = new SqlCommand(sql, connection);
         //initialize value in parameters
            foreach(var property in properties)
            {
                cmd.Parameters.AddWithValue($"@{property.Name}", property.GetValue(entity));
                //check value get or not
                var value = property.GetValue(entity);
            }
            cmd.ExecuteNonQuery();     
    }
}