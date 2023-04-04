using System.Collections;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;

public static class ValuePrinter
{   
    public static void Printvalue(List<Dictionary<string, object>> rows)
    {
        if (rows.Count > 0)
        {
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
        else
        {
            Console.WriteLine($"There is no value in this id.");
        }
    }

}