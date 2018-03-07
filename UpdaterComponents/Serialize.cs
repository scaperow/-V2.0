using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;


namespace UpdaterComponents
{
    public class Serialize
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 二进制序列化对象
        /// </summary>
        /// <param name="HereObject">拟序列化对象</param>
        /// <returns>二进制数组</returns>
        public static byte[] SerializeToBytes(object HereObject)
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream s = new MemoryStream();
                bf.Serialize(s, HereObject);
                byte[] data = s.ToArray();
                s.Close();
                return data;
            }
            catch (Exception ee)
            {
                return new byte[0];
            }
            
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <param name="HereBytes">二进制序列化内容</param>
        /// <returns>特定对象</returns>
        public static object DeSerializeFromBytes(byte[] HereBytes)
        {
            try
            {
                object obj = null;
                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream s = new MemoryStream(HereBytes);
                obj = (object)bf.Deserialize(s);
                s.Close();
                return obj;
            }
            catch (Exception ee)
            {
                return null;
            }
        }

        /// <summary>
        /// XML序列化特定对象
        /// </summary>
        /// <param name="HereObject">对象</param>
        /// <returns>字符串</returns>
        public static string SerializeToXml(object HereObject)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(HereObject.GetType());
                MemoryStream s = new MemoryStream();
                xs.Serialize(s, HereObject);
                byte[] str = s.ToArray();
                s.Close();
                string d = System.Text.Encoding.UTF8.GetString(str);
                return d;
            }
            catch (Exception ee)
            {
                return ee.ToString();
            }
        }
        
        /// <summary>
        /// XML反序列化对象
        /// </summary>
        /// <param name="HereXml">XML内容</param>
        /// <param name="HereType">类型</param>
        /// <returns>对象</returns>
        public static object DeSerializeFromXml(string HereXml, Type HereType)
        {
            try
            {
                object obj = null;
                byte[] b = System.Text.Encoding.UTF8.GetBytes(HereXml);
                XmlSerializer xs = new XmlSerializer(HereType);
                MemoryStream s = new MemoryStream(b);
                obj = (object)xs.Deserialize(s);
                s.Close();
                return obj;
            }
            catch (Exception ee)
            {
                return ee.Message;
            }
        }

        /// <summary>
        /// 二进制序列化对象
        /// </summary>
        /// <param name="HereObject">拟序列化对象</param>
        /// <returns>二进制数组</returns>
        public static Stream SerializeToStream(object HereObject)
        {
            String error;
            BinaryFormatter bf = new BinaryFormatter();

            try
            {
                MemoryStream s = new MemoryStream();
                bf.Serialize(s, HereObject);

                s.Seek(0, SeekOrigin.Begin);

                return s;
            }
            catch (OutOfMemoryException ex)
            {
                error = ex.Message;

                String tempfilename = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                FileStream fs = new FileStream(tempfilename, FileMode.Create, FileAccess.ReadWrite);
                bf.Serialize(fs, HereObject);

                fs.Position = 0;

                return fs;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                logger.Error(error);
            }

            return null;
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <param name="HereBytes">二进制序列化内容</param>
        /// <returns>特定对象</returns>
        public static object DeSerializeFromStream(Stream HereStream)
        {
            String error;
            object obj = null;
            BinaryFormatter bf = new BinaryFormatter();

            try
            {
                obj = (object)bf.Deserialize(HereStream);
                return obj;
            }
            catch (OutOfMemoryException ex)
            {
                error = ex.Message;

                String tempfilename = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                FileStream fs = new FileStream(tempfilename, FileMode.Create, FileAccess.ReadWrite);

                byte[] buffer = new byte[4096];
                int bytesRead = 0;

                do
                {
                    bytesRead = HereStream.Read(buffer, 0, buffer.Length);
                    fs.Write(buffer, 0, bytesRead);

                } while (bytesRead > 0);

                fs.Position = 0;

                obj = (object)bf.Deserialize(fs);
                fs.Close();
                return obj;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return obj;
        }
    }
}
