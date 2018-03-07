using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Yqun.Bases
{
    public class BinarySerializer
    {
        static System.Runtime.Serialization.IFormatter formatter = new BinaryFormatter();
        public static String Serialize(object obj)
        {
            MemoryStream ms = new MemoryStream();
            formatter.Serialize(ms, obj);
            String Result = System.Convert.ToBase64String(ms.ToArray());
            ms.Close();
            return Result;
        }

        public static object Deserialize(String xml)
        {
            try
            {
                byte[] bytes = System.Convert.FromBase64String(xml);
                MemoryStream ms = new MemoryStream(bytes);
                object obj = formatter.Deserialize(ms);
                ms.Close();
                return obj;
            }
            catch
            { }

            return null;
        }
    }
}
