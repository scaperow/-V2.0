using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Diagnostics;
using ICSharpCode.SharpZipLib.GZip;

namespace JZUpgrade
{
    public class Compression
    {
        /// <summary>
        /// 将数据流压缩  
        /// </summary>
        /// <param name="UnCompressBytes">为压缩后的数据流</param>
        /// <returns>压缩后的数据流</returns>
        public static Stream CompressStream(Stream stream)
        {
            MemoryStream ms = new MemoryStream();
            GZipOutputStream OutputStream = new GZipOutputStream(ms);
            OutputStream.SetLevel(9);

            byte[] Buffer = new byte[1024];
            int Count;
            while ((Count = stream.Read(Buffer,0, Buffer.Length)) != 0)
            {
                OutputStream.Write(Buffer, 0, Count);
            }

            OutputStream.Flush();
            OutputStream.Finish();

            ms.Seek(0, SeekOrigin.Begin);

            return ms;
        }

        /// <summary>
        /// 将压缩后的数据流解压
        /// </summary>
        /// <param name="CompressBytes">已压缩的数据流</param>
        /// <returns>解压后的数据流</returns>
        public static Stream DeCompressStream(Stream stream)
        {
            byte[] Buffer = new byte[2048];
            MemoryStream ms = new MemoryStream();
            Stream UnZipStream = new GZipInputStream(stream) as Stream;

            int Count;
            while ((Count = UnZipStream.Read(Buffer, 0, Buffer.Length)) != 0)
            {
                ms.Write(Buffer, 0, Buffer.Length);
            }

            UnZipStream.Close();

            ms.Seek(0, SeekOrigin.Begin);

            return ms;
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

        /// <summary>
        /// 将文件压缩到新的文件
        /// </summary>
        /// <param name="FromFileName">未压缩的文件名称</param>
        /// <param name="ToFileName">压缩后的文件名称</param>
        /// <returns>成功返回1，不成功返回-1</returns>
        public static int CompressFile(string FromFileName,string ToFileName) 
        {
            try
            {
                // 打开一个文件到infile.
                FileStream infile = new FileStream(FromFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                FileStream outfile = new FileStream(ToFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                GZipStream compressedzipStream = new GZipStream(outfile, CompressionMode.Compress);

                byte[] buffer = new byte[1024];
                int count;
                while ((count = infile.Read(buffer, 0, 1024)) > 0)
                {                    
                    compressedzipStream.Write(buffer, 0, count);
                    compressedzipStream.Flush();                    
                }
                compressedzipStream.Close();
                outfile.Close();
                infile.Close();                

                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// 将压缩后的文件解压为新的文件
        /// </summary>
        /// <param name="FromFileName">压缩过的文件</param>
        /// <param name="ToFileName">解压到的文件名称</param>
        /// <returns>成功返回1，不成功返回-1</returns>
        public static int DeCompressFile(string FromFileName, string ToFileName) 
        {
            try
            {
                FileStream infile = new FileStream(FromFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                GZipStream zipStream = new GZipStream(infile, CompressionMode.Decompress);
                FileStream outfile = new FileStream(ToFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                byte[] buffer = new byte[1024];
                int count;
                while((count = zipStream.Read(buffer,0,1024)) > 0)
                {
                    outfile.Write(buffer, 0, count);
                    outfile.Flush();
                }
                
                zipStream.Close();
                infile.Close();
                outfile.Close();

                return 1;
            }
            catch(Exception e)
            {
                return -1;
            }
        }
    }
}
