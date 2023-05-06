using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Assignment1
{
    public class JsonFormatters
    {
        public static string Convert(object item)
        {
            StringBuilder json = new StringBuilder();
            json.Append("{");
            Type type = item.GetType();
            PropertyInfo[] properties = type.GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo property = properties[i];
                object value = property.GetValue(item);
                Type valueType = value.GetType();
                if (value is IList)
                {
                    json.Append(string.Format("\"{0}\":", property.Name));
                    json.Append("[");
                    IList list = (IList)value;
                    for (int j = 0; i < list.Count; j++)
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
                else if (valueType.IsClass)
                {
                    json.Append(string.Format("\"{0}\":", property.Name));
                    json.Append(Convert(value));
                }
                else
                {
                    json.Append(string.Format("\"{0}\":\"{1}\"", property.Name, value));
                }
                if (Array.IndexOf(properties, property) < properties.Length - 1)
                {
                    json.Append(",");
                }
            }
            json.Append("}");
            return json.ToString();
        }
    }

}
