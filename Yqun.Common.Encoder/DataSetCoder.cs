using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Yqun.Common.Encoder
{
    public class DataSetCoder
    {
        /// <summary>
        /// 二进制序列化数据集
        /// </summary>
        /// <param name="HereDataSet">数据集</param>
        /// <returns>数据流</returns>
        public static Stream SerializeDataSet(DataSet dataset) 
        {
            Stream s = null;
            BinaryFormatter matter = new BinaryFormatter();
            try
            {
                s = new MemoryStream();
                matter.Serialize(s, dataset);
            }
            catch(OutOfMemoryException ex)
            {

            }

            return s;
        }

        /// <summary>
        /// 反二进制序列化数据集
        /// </summary>
        /// <param name="HereBytes">序列化后的数组</param>
        /// <returns>数据集</returns>
        public static DataSet DeSerializeDataSet(Stream stream) 
        {
            BinaryFormatter matter = new BinaryFormatter();
            DataSet ds = matter.Deserialize(stream) as DataSet;
            return ds;
        }

        /// <summary>
        /// XML 序列化数据集
        /// </summary>
        /// <param name="HereDataSet">数据集</param>
        /// <returns>XML字符串</returns>
        public static string SerializeDataSetToXml(DataSet HereDataSet)
        {
            XmlSerializer ser = new XmlSerializer(typeof(DataSet));
            MemoryStream s = new MemoryStream();
            ser.Serialize(s, HereDataSet);
            byte[] inarray = s.ToArray();
            string data = System.Text.UTF8Encoding.UTF8.GetString(inarray);
            return data;
        }

        /// <summary>
        /// 从Xml反序列化数据集
        /// </summary>
        /// <param name="HereDataSetXml">XML字符串</param>
        /// <returns>数据集</returns>
        public static DataSet DeSerializeDataSetFromXml(string HereDataSetXml)
        {
            DataSet ds = null;
            byte[] inarray = System.Text.UTF8Encoding.UTF8.GetBytes(HereDataSetXml);
            XmlSerializer matter = new XmlSerializer(typeof(DataSet));
            MemoryStream s = new MemoryStream(inarray);
            ds = (DataSet)matter.Deserialize(s);
            s.Close();
            return ds;
        }

        public static object GetProperty(DataSet InfoSet,
            int TableIndex,
            string IDColumnName,
            string IDValue,
            string AimCoumnName)
        {
            try
            {
                string sele = IDColumnName + " = " + "'" + IDValue + "'";
                DataRow[] rs = InfoSet.Tables[TableIndex].Select(sele);
                if (rs.Length > 0)
                {
                    object o = rs[0][AimCoumnName];
                    return o;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public static int SetProperty(ref DataSet InfoSet,
            int TableIndex,
            string IDColumnName,
            string IDValue,
            string AimCoumnName,
            object AimValue
            )
        {
            try
            {
                string sele = IDColumnName + " = " + "'" + IDValue + "'";
                DataRow[] rs = InfoSet.Tables[TableIndex].Select(sele);
                if (rs.Length > 0)
                {
                    rs[0][AimCoumnName] = AimValue;
                    return 1;
                }
                else
                {
                    DataRow r = InfoSet.Tables[TableIndex].NewRow();
                    r[AimCoumnName] = AimValue;
                    r[IDColumnName] = IDValue;
                    InfoSet.Tables[TableIndex].Rows.Add(r);
                    return 1;
                }
            }
            catch
            {
                return -1;
            }
        }

        public static int DelProperty(ref DataSet InfoSet,
            int TableIndex,
            string IDColumnName,
            string IDValue) 
        {
            try
            {
                string sele = IDColumnName + " = " + "'" + IDValue + "'";
                DataRow[] rs = InfoSet.Tables[TableIndex].Select(sele);
                if (rs.Length > 0)
                {
                    for (int i = rs.Length - 1; i >= 0; i--)
                    {
                        rs[i].Delete();
                    }
                    return 1;
                }

                return 1;
            }
            catch 
            {
                return -1;            
            }
        }


        public static void SaveEncryAndCompressDataSetToFile(string FileName ,DataSet d) 
        {
            string xml = SerializeDataSetToXml(d);
            FileIO.WriteFile(xml, "utf-8", FileName);
        }

        public static DataSet DecryAndDecompreeDataSetFromFile(string FileName) 
        {
            string xml = FileIO.ReadFileToString(FileName, "utf-8");
            return DeSerializeDataSetFromXml(xml);
        }

        #region 通用瘦身序列化

        public static string Serialize(DataSet d)
        {
            try
            {
                MemoryStream ms = new MemoryStream();

                d.WriteXml(ms, XmlWriteMode.IgnoreSchema);

                byte[] bs = ms.ToArray();
                ms.Close();

                string xml = System.Text.Encoding.UTF8.GetString(bs);

                return xml;
            }
            catch
            {
                return "";
            }
        }

        #endregion

        #region 通用瘦身反序列化

        public static DataSet DeSerialize(DataSet d, string xml)
        {
            try
            {
                byte[] bs = System.Text.Encoding.UTF8.GetBytes(xml);
                MemoryStream ms = new MemoryStream(bs);
                d.ReadXml(ms, XmlReadMode.IgnoreSchema);
                ms.Close();

                return d;
            }
            catch
            {
                return null;
            }
        }

        #endregion

    }
}
