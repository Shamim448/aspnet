using System.Collections;
using System.Net;
using System.Reflection;
using System.Text;
//CREATE TABLE Persons (
//    PersonID int,
//    LastName varchar(255),
//    FirstName varchar(255),
//    Address varchar(255),
//    City varchar(255)
//);
public class MyORM
{
    public string Insert (object item)
    {
        StringBuilder sql = new StringBuilder ();
        sql.Append("CREATE TABLE ");
        Type type = item.GetType();
        string TName = type.Name;
        sql.Append(TName + " (");
        PropertyInfo[] properties = type.GetProperties();
        for(int i = 0; i < properties.Length; i++)
        {
            var values = properties[i].GetValue(item);
            if (properties[i].PropertyType == typeof(int))
            {
                sql.Append(properties[i].Name + " int, ");
            }
            else if (properties[i].PropertyType.IsGenericType ||  values is IList )
            {
                IList list = (IList) values;
                foreach( var value in list)
                {
                    sql.Append(Insert(value));
                }           
            }
            else
            {
                sql.Append(properties[i].Name + " varchar(255), ");
            }
        }
        sql.Append (")");
        return sql.ToString ();
    }
}