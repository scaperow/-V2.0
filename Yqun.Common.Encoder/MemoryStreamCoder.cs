using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Yqun.Common.Encoder
{
    public class MemoryStreamCoder
    {
        public MemoryStreamCoder() 
        {
        
        }

        public static System.IO.MemoryStream ToMemoryStream(byte[] Bytes)
        {
            System.IO.MemoryStream m = new System.IO.MemoryStream(Bytes);
            m.Position = 0;
            return m;
        }

        public static System.IO.MemoryStream ToMemoryStream(string InputFileName)
        {

            byte[] b = FileIO.ReadFileToBytes(InputFileName);
            System.IO.MemoryStream m = new System.IO.MemoryStream(b);
            m.Position = 0;
            return m;
        }

        public static string FromMemoryStream(System.IO.MemoryStream m, string CharSet)
        {
            byte[] b = m.ToArray();
            string s = System.Text.Encoding.GetEncoding(CharSet).GetString(b);
            m.Close();
            return s;

        }

        public static byte[] FromMemoryStream(System.IO.MemoryStream m)
        {
            byte[] b = m.ToArray();
            m.Close();
            return b;
        }

        public static void FromMemoryStream(string OutputFileName, System.IO.MemoryStream m)
        {
            byte[] b = FromMemoryStream(m);
            FileIO.WriteFile(b, OutputFileName);
            m.Close();
        }
    }
}
