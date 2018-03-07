using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.IO;
using System.IO.Compression;

namespace Yqun.Services
{
    public class Serialize
    {
        static Serialize() 
        {}

        public static byte[] SerializeMessage(Hashtable message)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter fm = new BinaryFormatter();
                fm.Serialize(ms, message);
                byte[] b = ms.ToArray();
                return b;
            }
        }

        public static Hashtable DeserializeMessage(byte[] MessageBytes)
        {
            using (MemoryStream ms = new MemoryStream(MessageBytes))
            {
                string str = System.Text.UTF8Encoding.UTF8.GetString(MessageBytes);
                BinaryFormatter fm = new BinaryFormatter();
                object o = fm.Deserialize(ms);
                return o as Hashtable;
            }

        }
        /// <summary>
        /// 将二进制数组压缩  
        /// </summary>
        /// <param name="UnCompressBytes">为压缩的字节数组</param>
        /// <returns>压缩后的字节数组</returns>
        public static byte[] CompressBytes(byte[] UnCompressBytes)
        {
            if (UnCompressBytes == null)
            {
                return new byte[0];
            }

            if (UnCompressBytes.Length == 0)
            {
                return new byte[0];
            }

            using (MemoryStream fs = new MemoryStream())
            {
                Hashtable p = new Hashtable();
                p.Add("value", UnCompressBytes);
                using (GZipStream compressStream = new GZipStream(fs, CompressionMode.Compress))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(compressStream, p);
                }
                return fs.ToArray();
            }
        }

        /// <summary>
        /// 将压缩后的字节数组解压
        /// </summary>
        /// <param name="CompressBytes">已压缩的字节数组</param>
        /// <returns>解压后的字节数组</returns>
        public static byte[] DeCompressBytes(byte[] CompressBytes)
        {
            if (CompressBytes == null)
            {
                return new byte[0];
            }

            if (CompressBytes.Length == 0)
            {
                return new byte[0];
            }

            using (MemoryStream fs = new MemoryStream(CompressBytes))
            {
                using (GZipStream decompressStream = new GZipStream(fs, CompressionMode.Decompress))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    Hashtable tm = (bf.Deserialize(decompressStream) as Hashtable);
                    byte[] Value = tm["value"] as byte[];
                    return Value;
                }
            }
        }

        public static byte[] CompressMessage(Hashtable message)
        {
            using (MemoryStream fs = new MemoryStream())
            {

                using (GZipStream compressStream = new GZipStream(fs, CompressionMode.Compress))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(compressStream, message);
                }
                return fs.ToArray();
            }
        }

        public static Hashtable DeCompressMessage(byte[] CompressBytes)
        {
            try
            {
                using (MemoryStream fs = new MemoryStream(CompressBytes))
                {
                    using (GZipStream decompressStream = new GZipStream(fs, CompressionMode.Decompress))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        Hashtable tm = (bf.Deserialize(decompressStream) as Hashtable);
                        return tm;
                    }
                }
            }
            catch (Exception) 
            {
                Hashtable hs = new Hashtable();
                hs.Add("return_value", null);
                return hs;
            }
        }
    }
}
