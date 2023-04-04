using System.Collections;
using System.Data.Common;
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
    Type type = typeof(T);
    //Insert value
    public void Insert(T entity)
    {
        #region Insert_Operation
        //get object info
        EntityInfo entityInfo = new EntityInfo(entity);
        var tablename = entityInfo.GetObjectName();
        string columnName = entityInfo.GetColumn(); 
        string parametersName = entityInfo.GetParameters(); 
        //sql query for parametarized Insert
        string sql = $"Insert into {tablename} ({columnName}) Values({parametersName})";
        //call sqlconnection and open connection 
        SqlConnection connection = new SqlConnection(_connectionString);
        
        //instialize sqlcommend it takes a sql query and a sqlconnection
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
                cmd.ExecuteNonQuery();
                Console.WriteLine("Record Insert Successfully");
            }
        }
        catch(SqlException ex)
        {
            Console.WriteLine("Error Generated. Details: " + ex.ToString());
        }
        finally { 
            connection.Close(); 
        }
       
        #endregion
    }
    public void Update(T entity) { }
    public void Delete(G id) { }
    public void GetById(G id)    {
        //sql query for select all data
        string sql = $"Select * from {type.Name} Where Id = {id}";
        using var connection = new SqlConnection(_connectionString);  
        using SqlCommand cmd = new SqlCommand(sql, connection);      
        try
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                while (reader.Read())
                {
                    Dictionary<string, object> columns = new Dictionary<string, object>();
                    foreach (var column in reader.GetColumnSchema())
                    {
                        columns.Add(column.ColumnName, reader[column.ColumnName]);
                    }
                    rows.Add(columns);
                }
                //Print Table Header
                if(rows.Count > 0) {
                    foreach (var col in rows[0].Keys)
                    {
                        Console.Write(col + "\t");
                    }
                    Console.WriteLine();
                    //Print All Value
                    foreach (var v in rows)
                    {
                        foreach (var k in v.Values)
                        {
                            Console.Write(k + "\t");
                        }
                        Console.WriteLine();
                    }
                } 
                else { 
                    Console.WriteLine($"There is no value in this {id} id."); 
                }
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
    public void GetAll() {
        #region Get_All_Table_Data
        //sql query for select all data
        string sql = $"Select * from {type.Name}";
        var connection = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand( sql, connection);
        try
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                while (reader.Read())
                {
                    Dictionary<string, object> columns = new Dictionary<string, object>();
                    foreach (var column in reader.GetColumnSchema())
                    {
                        columns.Add(column.ColumnName, reader[column.ColumnName]);
                    }
                    rows.Add(columns);
                }
                //Print Table Header
                foreach (var col in rows[0].Keys)
                {
                    Console.Write(col + "\t");
                }
                Console.WriteLine();
                //Print All Value
                foreach (var v in rows)
                {
                    foreach (var k in v.Values)
                    {
                        Console.Write(k + "\t");
                    }
                    Console.WriteLine();
                }
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
       
        
        #endregion
    }
}