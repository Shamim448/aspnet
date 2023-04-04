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

}