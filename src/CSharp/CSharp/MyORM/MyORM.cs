using System.Collections;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;
public class MyORM<G, T> where T : IIdBase<G>
{
    public readonly string _connectionString;
    public readonly DataUtility _dataUtility;
    public string DbConnection = "Server=.\\SQLEXPRESS;Database=Aspnetb8;User Id=aspnetb8;Password=123456;";
    public MyORM(string connectionString)
    {
        _connectionString = connectionString;
        _dataUtility = new DataUtility(DbConnection);

    }
    
    Type type = typeof(T);
    string tableName = typeof(T).Name;
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
        PropertyInfo[] properties = entityInfo.GetProperties();
        
        foreach (var property in properties)
        {
            cmd.Parameters.AddWithValue($"@{property.Name}", property.GetValue(entity));
            //check value get or not
            
        }
        foreach (var property in properties)
        {
            var value = property.GetValue(entity);
            if (property.PropertyType.IsGenericType || value is IList)
            {
                IList list = (IList)value;
                if (list != null)
                {
                    foreach(var item in list)
                    {
                       // Insert(T item);
                    }
                }
            }
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
    public void Update(T entity) {
        EntityInfo entityInfo = new EntityInfo(entity);
        string columnName = string.Join(", ", entityInfo.GetProperties().Select(p => $"{p.Name} = @{p.Name}"));
        string sql = $"Update {tableName} set {columnName} where Title = @Title";
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
                Console.WriteLine("Record Insert Successfully");
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
    public void Delete(T entity)
    {
        EntityInfo entityInfo = new EntityInfo(entity);
      // string s = $"ALTER TABLE {tableName} DROP COLUMN {entityInfo.GetColumn()}";
        string sql = $"Drop Table {entityInfo.GetObjectName()}";
        _dataUtility.ExecuteCommand(sql);
    }
    public void Delete(G id) {
        #region Delete_By _ID
        string sql = $"Delete From {tableName} Where Id = {id}";
        _dataUtility.ExecuteCommand(sql);
        #endregion
    }
    public void GetById(G id)    {
        #region Print_Value_By_Id
        //sql query for select all data
        string sql = $"Select * from {tableName} Where Id = {id}";
        _dataUtility.ReadData(sql);
        #endregion
    }
    public void GetAll() {
        #region Get_All_Table_Data
        //sql query for select all data
        string sql = $"Select * from {tableName}";
        _dataUtility.ReadData(sql);       
        #endregion
    }
}
