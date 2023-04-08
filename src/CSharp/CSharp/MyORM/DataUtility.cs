using System.Collections;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;

public class DataUtility
{
    public readonly string _connectionString;
    public DataUtility(string connectionString)
    {
        _connectionString = connectionString;
    }
    //Used in delete
    public void ExecuteCommand(string command)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand(command, connection);
        try
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Query Executed!");
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            connection.Close();
            Console.ReadKey();
        }
    }
    //Used in GetItembyId(), GetAll()
    public void ReadData (string command)
    {
        var connection = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand(command, connection);
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
                //Print Table values
                ValuePrinter.Printvalue(rows);
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

}