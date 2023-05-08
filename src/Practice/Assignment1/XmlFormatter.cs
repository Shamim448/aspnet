using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class XmlFormatter
    {
        public static string Convert(object item)
        {
            //Null Checking
            if (item == null)
                return null;
            StringBuilder xml = new StringBuilder();
            Type type = item.GetType();
            xml.Append($"<{type.Name}>");

            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields )
            {
                xml.Append(string.Format("<{0}>{1}", field.Name, field.GetValue(item)));
                 
                xml.Append(string.Format("</{0}>", field.Name));

            }
            PropertyInfo[] properties = type.GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                #region property-type
                PropertyInfo property = properties[i];
                object value = property.GetValue(item);
                //Generic Collection Check
                if (value is IList)
                {
                    xml.Append(string.Format("<{0}>", property.Name));
                    IList list = (IList)value;
                    for (int j = 0; j < list.Count; j++)
                    {
                        object listItem = list[j];
                        xml.Append(Convert(listItem));
                    }
                    xml.Append(string.Format("</{0}>", property.Name));
                }
                //if value found
                else if (value != null)
                {
                    xml.Append(string.Format("<{0}>", property.Name));
                    
                    if (value is string)
                    {
                        xml.Append(string.Format("{0}", properties[i].GetValue(item)));
                    }
                    //class type check
                    else if (properties[i].PropertyType.IsClass)
                    {
                        xml.Append(Convert(properties[i].GetValue(item)));
                    }
                    else if (value is DateTime)
                    {
                        xml.Append(string.Format("{0}", properties[i].GetValue(item)));
                    }
                    else
                    {
                        xml.Append(properties[i].GetValue(item));
                    }
                    xml.Append(string.Format("</{0}>", property.Name));
                }
                //if property not set
                else
                {
                    xml.Append(string.Format("<{0} />", property.Name));
                }
                #endregion
            }
            xml.Append($"</{type.Name}>");
            return xml.ToString();
        }
    }

}
