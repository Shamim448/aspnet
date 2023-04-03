using System.Collections;
using System.Data.SqlClient;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;

public class EntityInfo
{
    private readonly Type _type;  
    public EntityInfo(object obj)
    {
        _type = obj.GetType();
    }
    public string GetObjectName()
    {
        string tableName = _type.Name;
        return tableName;
    }
    public PropertyInfo[] GetProperties()
    {
        return _type.GetProperties().Where(p => p.Name != "Id").ToArray();
    }
    //collect table column name
    public string GetColumn()
    {
        return string.Join(", ", GetProperties().Select(c => c.Name));
    }
    //column name set as a parameter name
    public string GetParameters()
    {
        return string.Join(", ", GetProperties().Select(p => $"@{p.Name}"));
    }
}