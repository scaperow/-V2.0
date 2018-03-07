using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;

namespace BizComponents
{
    public class XmlSerializer
    {
        static SoapFormatter formatter = new SoapFormatter();
        public static String Serialize(object obj)
        {
            MemoryStream ms = new MemoryStream();
            formatter.Serialize(ms, obj);
            String Result = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return Result;
        }

        public static object Deserialize(String xml)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(xml);
            MemoryStream ms = new MemoryStream(bytes);
            object obj = formatter.Deserialize(ms);
            ms.Close();
            return obj;
        }
    }
}
