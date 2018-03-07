using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel.Configuration;
using System.Configuration;
using System.Xml;
using System.IO;

namespace BizCommon
{
    public class ConfigHelper
    {
        #region 操作XML
        /// <summary>
        /// 更新Sys.xml节点的值
        /// </summary>
        /// <param name="NodeName"></param>
        /// <param name="NodeContent"></param>
        /// <returns></returns>
        public static bool UpdateSysXmlValue(string NodeName, string NodeContent)
        {
            return UpdateXmlValue("/Sys.xml", "config/" + NodeName, NodeContent);
        }
        public static bool UpdateXmlValue(string FilePath, string NodePath, string NodeContent)
        {
            try
            {
                XmlDocument XmlDoc = new XmlDocument();
                XmlDoc.Load(GetXmlFilePath(FilePath));   //载入Xml文档
                XmlDoc.SelectSingleNode(NodePath).InnerText = NodeContent;
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 获取Sys.xml节点的值
        /// </summary>
        /// <param name="NodeName"></param>
        /// <returns></returns>
        public static string GetSysXmlValue(string NodeName)
        {
            return GetXmlValue("/Sys.xml", "config/" + NodeName);
        }
        public static string GetXmlValue(string FilePath, string NodePath)
        {
            string strValue = string.Empty;
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(GetXmlFilePath(FilePath));   //载入Xml文档
            strValue = XmlDoc.SelectSingleNode(NodePath).InnerText;
            return strValue;
        }
        /// <summary>
        /// 返回Xml文件实际路径
        /// </summary>
        /// <param name="xmlFile">文件虚拟路径</param>
        /// <returns></returns>
        public static string GetXmlFilePath(string FilePath)
        {
            return Directory.GetCurrentDirectory() + FilePath;
        }
        #endregion
        #region 修改Wcf客户端Client的Endpoint
        /// <summary>
        /// 读取EndpointAddress
        /// </summary>
        /// <param name="endpointName"></param>
        /// <returns></returns>
        public static string GetEndpointAddress(string endpointName)
        {
            ClientSection clientSection = ConfigurationManager.GetSection("system.serviceModel/client") as ClientSection;
            foreach (ChannelEndpointElement item in clientSection.Endpoints)
            {
                if (item.Name == endpointName)
                    return item.Address.ToString();
            }
            return string.Empty;
        }


        /// <summary>
        /// 设置EndpointAddress
        /// </summary>
        /// <param name="endpointName"></param>
        /// <param name="address"></param>
        public static void SetEndpointAddress(string endpointName, string address)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ClientSection clientSection = config.GetSection("system.serviceModel/client") as ClientSection;
            foreach (ChannelEndpointElement item in clientSection.Endpoints)
            {
                if (item.Name != endpointName)
                    continue;
                item.Address = new Uri(address);
                break;
            }
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("system.serviceModel");
        }
        #endregion

        #region 领导版使用
        
        
        public static List<string> GetLines()
        {
            List<string> list = new List<string>();
            XmlDocument document = new XmlDocument();
            document.Load(GetXmlFilePath("/lines.xml"));
            XmlNodeList childNodes = document.SelectNodes("config")[0].ChildNodes;
            for (int i = 0; i < childNodes.Count; i++)
            {
                XmlNode node = childNodes.Item(i);
                list.Add(node.Attributes["name"].Value);
            }
            return list;
        }

        public static void UpdateByLineName(string name)
        {
            string address = "";
            XmlDocument document = new XmlDocument();
            document.Load(GetXmlFilePath("/lines.xml"));
            XmlNodeList childNodes = document.SelectNodes("config")[0].ChildNodes;
            for (int i = 0; i < childNodes.Count; i++)
            {
                XmlNode node = childNodes.Item(i);
                if (node.Attributes["name"].Value == name)
                {
                    address = node.InnerText;
                    break;
                }
            }
            if (address != "")
            {
                SetEndpointAddress("TransferServiceEndPoint", address);
            }
        }
        #endregion

    }
}
