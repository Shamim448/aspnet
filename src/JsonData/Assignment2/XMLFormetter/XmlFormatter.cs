using System;
using System.Buffers.Binary;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace XMLFormetter
{
    public class XmlFormatter
    {
        public int cnt = 0;
        public static string Convert(object @class, string s = null)
        {
            StringBuilder xml = new StringBuilder();
            Type type = @class.GetType();

            if (s == null)
            {
                xml.Append($"<{type.Name}> \n");
                s = type.Name;
            }
            FieldInfo[] fields = type.GetFields();
            #region field-type
            foreach (var field in fields)
            {
                xml.Append($"<{field.Name}>");
                xml.Append(field.GetValue(@class));
                xml.Append($"</{field.Name}>\n");
            }
            #endregion
            PropertyInfo[] propertis = type.GetProperties();
            foreach (var prop in propertis)
            {
                xml.Append($"<{prop.Name}>");
                var values = prop.GetValue(@class);
                //need to check null
                if (prop.PropertyType.IsPrimitive || prop.PropertyType == typeof(string))
                {
                    xml.Append(values);
                }
                else if (prop.PropertyType.IsGenericType || values is IList)
                {
                    IList list = (IList)values;
                    foreach (var item in list)
                    {
                        xml.Append(Convert(item));
                    }
                }
                else if (prop.PropertyType.IsClass)
                {
                    xml.Append(Convert(values, s));
                }
                else if (values is DateTime)
                {
                    xml.Append(values);
                }
                else //not primitive or list or datetime or string or class
                {
                    xml.Append(values);
                }
                //property closing tag
                xml.Append($"</{prop.Name}>\n");
            }
            if (s == type.Name)
            {
                xml.Append($"\n</{type.Name}>");//end tag of main class
            }




            return xml.ToString();
        }
    }
}
