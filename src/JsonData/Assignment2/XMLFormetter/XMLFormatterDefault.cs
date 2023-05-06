using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLFormetter
{
    public class XMLFormatterDefault
    {
        private static string _xmlContent;

        public static string Convert<T>(T item)
        {
            if (item == null)
                return null;
            var xmlSerializer = new XmlSerializer(typeof(T));
            using(var writter = new StringWriter())
            {
                xmlSerializer.Serialize(writter, item);
                var xmlContent = writter.ToString();
                _xmlContent= xmlContent;

            }
            return _xmlContent;
        }
    }
}
