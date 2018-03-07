using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

//
using System.Diagnostics;

namespace Yqun.Common.Encoder
{
    public class FileIO
    {
        public FileIO() 
        {
        
        }
                
        /// <summary>
        /// 将二进制数组写为一个完整文件
        /// </summary>
        /// <param name="ReadBytes">二进制数组</param>
        /// <param name="FileName">文件名称</param>
        /// <returns>二进制数据的长度</returns>
        public static long WriteFile(byte[] ReadBytes, string FileName)
        {            
            try 
            {
                using (System.IO.FileStream s = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    try
                    {
                        s.SetLength(0);
                        s.Write(ReadBytes, 0, ReadBytes.Length);
                        s.Close();
                        return ReadBytes.Length;
                    }
                    catch 
                    {
                        s.Close();
                        return -1;
                    }
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// 将字符串按照特定编码集保存为一个完整的文件
        /// </summary>
        /// <param name="ReadString">字符串</param>
        /// <param name="CharSet">编码集</param>
        /// <param name="FileName">文件名</param>
        /// <returns>文件长度</returns>
        public static long WriteFile(string ReadString, string CharSet, string FileName)
        {
            try
            {
                //编码
                byte[] str = System.Text.Encoding.GetEncoding(CharSet).GetBytes(ReadString);
                return WriteFile(str, FileName);
            }
            catch (Exception)
            {
                return -1;
            }
        }
        
        /// <summary>
        /// 将二进制数据附加到文件末尾
        /// </summary>
        /// <param name="ToAppendBytes">二进制数据</param>
        /// <param name="FileName">文件名</param>
        /// <returns>此次二进制数据长度</returns>
        public static int AppendFile(byte[] ToAppendBytes, string FileName)
        {
            try
            {
                using (System.IO.FileStream s = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    //
                    try
                    {
                        s.Position = s.Length;
                        s.Write(ToAppendBytes, 0, ToAppendBytes.Length);
                        s.Close();
                        return ToAppendBytes.Length;
                    }
                    catch 
                    {
                        s.Close();
                        return -1;
                    }
                }
            }
            catch (Exception)
            {
                return -1;
            }            
        }

        /// <summary>
        /// 将字符串按照特定的编码集附加到文件
        /// </summary>
        /// <param name="ToAppendString">字符串</param>
        /// <param name="CharSet">编码集</param>
        /// <param name="FileName">文件名</param>
        /// <returns>附加的字节数组长度</returns>
        public static long AppendFile(string ToAppendString, string CharSet, string FileName)
        {
            try
            {
                //编码
                byte[] str = System.Text.Encoding.GetEncoding(CharSet).GetBytes(ToAppendString);
                return AppendFile(str, FileName);
            }
            catch (Exception)
            {
                return -1;
            }
        }
                
        /// <summary>
        /// 将文件读取到数组
        /// </summary>
        /// <param name="ReadFileName">文件名</param>
        /// <returns>二进制数组</returns>
        public static byte[] ReadFileToBytes(string ReadFileName)
        {
            try
            {
                using (System.IO.FileStream s = new FileStream(ReadFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    try
                    {
                        byte[] data = new byte[s.Length];
                        s.Read(data, 0, (int)data.Length);
                        s.Close();

                        return data;
                    }
                    catch 
                    {
                        s.Close();
                        return new byte[0];
                    }
                }
                
            
            }
            catch (Exception)
            {
                return new byte[0];
            }
        }

        /// <summary>
        /// 将文件读取到字符串
        /// </summary>
        /// <param name="ReadFileName">文件名</param>
        /// <param name="CharSet">编码集</param>
        /// <returns>字符串</returns>
        public static string ReadFileToString(string ReadFileName, string CharSet)
        {
            try
            {
                byte[] data = ReadFileToBytes(ReadFileName);
                string str = System.Text.Encoding.GetEncoding(CharSet).GetString(data);

                return str;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 从文件特定位置读取特定长度的数据
        /// </summary>
        /// <param name="ReadFileName">文件名</param>
        /// <param name="StartPosition">读取开始位置</param>
        /// <param name="Lenth">读取长度</param>
        /// <returns>二进制数组</returns>
        public static byte[] ReadFileToBytes(string ReadFileName, int StartPosition, int Lenth)
        {
            try
            {
                using (System.IO.FileStream s = new FileStream(ReadFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    try
                    {
                        byte[] data = new byte[Lenth];
                        s.Position = StartPosition;
                        int len = s.Read(data, 0, Lenth);
                        s.Close();

                        MemoryStream ms = new MemoryStream(data, 0, len);
                        byte[] b = ms.ToArray();



                        ms.Close();
                        return b;
                    }
                    catch 
                    {
                        s.Close();
                        return new byte[0]; 
                    }
                }
            }
            catch (Exception)
            {
                return new byte[0];
            }
        }
        public static byte[] ReadFileToBytes(string ReadFileName, long StartPosition, int Lenth)
        {
            try
            {
                using (System.IO.FileStream s = new FileStream(ReadFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    try
                    {
                        byte[] data = new byte[Lenth];
                        s.Position = StartPosition;
                        int len = s.Read(data, 0, Lenth);
                        s.Close();

                        MemoryStream ms = new MemoryStream(data, 0, len);
                        byte[] b = ms.ToArray();


                        ms.Close();

                        return b;
                    }
                    catch 
                    {

                        s.Close();
                        return new byte[0];
                    }
                }
            }
            catch (Exception)
            {
                return new byte[0];
            }
        }

        /// <summary>
        /// 从文件特定位置读取特定长度的数据并按照特定的编码集转换为字符串
        /// </summary>
        /// <param name="ReadFileName">文件名</param>
        /// <param name="CharSet">编码集</param>
        /// <param name="StartPosition">开始位置</param>
        /// <param name="Lenth">读取长度</param>
        /// <returns>特定编码集的字符串</returns>
        public static string ReadFileToString(string ReadFileName, string CharSet, int StartPosition, int Lenth)
        {
            try
            {
                byte[] data = ReadFileToBytes(ReadFileName, StartPosition,Lenth);
                string str = System.Text.Encoding.GetEncoding(CharSet).GetString(data);

                return str;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 把文件读成Base64字符串
        /// </summary>
        /// <param name="ReadFileName">文件名称</param>
        /// <returns>Base64字符串</returns>
        public static string FileToBase64(string ReadFileName) 
        {
            byte[] b = ReadFileToBytes(ReadFileName);
            string s = System.Convert.ToBase64String(b);
            return s;
        }


        /// <summary>
        /// 把Base64码解码为文件
        /// </summary>
        /// <param name="ToWriteFile">文件命</param>
        /// <param name="Base64String">Base64码</param>
        /// <returns>文件长度</returns>
        public static long WriteBase64ToFile(string ToWriteFile,string Base64String) 
        {
            byte[] b = System.Convert.FromBase64String(Base64String);
            long l = WriteFile(b, ToWriteFile);
            return l;
        }
    }
}
