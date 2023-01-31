using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
namespace Assignment1;
public class JsonFormatter
{
    public static string Convert(object item)
    {
        //Null Checing
        if (item == null)
            return null;
        StringBuilder json = new StringBuilder();
        json.Append("{");
        Type type = item.GetType();
        
        PropertyInfo[] properties = type.GetProperties();
        for (int i = 0; i < properties.Length; i++)
        {
            PropertyInfo property = properties[i];
           
            object value = property.GetValue(item);
            //Generic Collection Check
            if (value is IList)
            {
                json.Append(string.Format("\"{0}\":", property.Name));
                json.Append("[");
                IList list = (IList)value;
                for (int j = 0; j < list.Count; j++)
                {
                    object listItem = list[j];
                    json.Append(Convert(listItem));
                    if (j < list.Count - 1)
                    {
                        json.Append(",");
                    }
                }
                json.Append("]");
            }
            //if value found
            else if (value != null)
            {
                json.Append(string.Format("\"{0}\":", property.Name));
               // if (properties[i].PropertyType.IsValueType || properties[i].PropertyType == typeof(string))
               if(value is string)
                {
                    json.Append(string.Format("\"{0}\"", properties[i].GetValue(item)));
                }
               //class type check
                else if(properties[i].PropertyType.IsClass)
                 {
                    json.Append(Convert(properties[i].GetValue(item)));
                }
                else if (value is DateTime)
                {
                    //json.Append("\"" + properties[i].GetValue(item) + "\"");
                    json.Append(string.Format("\"{0}\"", properties[i].GetValue(item)));
                }
                else 
                {
                    json.Append( properties[i].GetValue(item));
                }
            }         
            //if property not set
            else
            {
                json.Append(string.Format("\"{0}\":", property.Name));
                json.Append("null");
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
