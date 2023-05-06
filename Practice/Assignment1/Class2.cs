using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
public class JsonFormatterb
{
    public static string Convert(object item)
    {
        if (item == null)
            return null;
        StringBuilder json = new StringBuilder();

        json.Append("{");
        Type type = item.GetType();
        PropertyInfo[] properties = type.GetProperties();
        for (int i = 0; i < properties.Length; i++)
        {
            json.Append("\"" + properties[i].Name + "\":");
            if (properties[i].PropertyType.IsValueType || properties[i].PropertyType == typeof(string))
            {
                json.Append("\"" + properties[i].GetValue(item) + "\"");
            }
            else if (properties[i].PropertyType.IsArray || properties[i].PropertyType.IsGenericType)
            {
               
                IEnumerable enumerable = properties[i].GetValue(item) as IEnumerable;
                if (enumerable != null)
                {
                    json.Append("[");
                    foreach (object obj in enumerable)
                    {
                        json.Append(Convert(obj));
                        json.Append(",");
                    }
                }
                json.Remove(json.Length - 1, 1);
                json.Append("]");
            }
            else
            {
                json.Append(Convert(properties[i].GetValue(item)));
            }
            if (i < properties.Length - 1)
            {
                json.Append(",");
            }
        }
        json.Append("}");

        return json.ToString();
    }

}

