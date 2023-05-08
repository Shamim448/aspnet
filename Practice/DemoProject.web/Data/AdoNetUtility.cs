

using Microsoft.Data.SqlClient;

namespace DemoProject.web.Data
{
    public class AdoNetUtility
    {
        private readonly SqlConnection _connection;
        public AdoNetUtility(string connection)
        {
            _connection = new SqlConnection(connection);
        }
        public void WriteOperation(string sql)
        {
            Console.WriteLine(sql);
            
        }
    }
}
