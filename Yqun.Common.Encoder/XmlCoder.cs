using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;


using System.Diagnostics;

namespace Yqun.Common.Encoder
{
    public class XmlCoder
    {
        /// <summary>
        /// 通过特定Tag获取XML节点集
        /// </summary>
        /// <param name="XmlDocumentText">XML文档</param>
        /// <param name="TagName">Tag名称</param>
        /// <returns>节点集</returns>
        public static XmlNodeList GetNodeList(string XmlDocumentText,string TagName) 
        {
            XmlNodeList elemList = null;
            XmlDocument doc = new XmlDocument();

            //判断XmlDocumentText是否为完整XML文档，如果不完整，创建根节点
            //过特定Tag获取XML节点集
            try
            {                
                doc.LoadXml(XmlDocumentText);
                //XmlNode book;
                XmlElement root = doc.DocumentElement;                  
                elemList = root.GetElementsByTagName(TagName);

                return elemList;
            }
            catch (Exception e)
            {
                string str = "有多个根元素。";
                //如果是以下错误则加根节点，否则返回空节点。
                if (e.Message.Contains(str) == true)
                {
                    doc.LoadXml("<root>" + XmlDocumentText + "</root>");
                    XmlElement root = doc.DocumentElement;
                    elemList = root.GetElementsByTagName(TagName);
                }
                else
                {
                    elemList = null;
                }

                return elemList;
            } 
        }

        /// <summary>
        /// 通过特定Tag获取XML节点集节点的InnerText字符串数组
        /// </summary>
        /// <param name="XmlDocumentText">XML文档</param>
        /// <param name="TagName">Tag名称</param>
        /// <returns>各节点InnerText的数组</returns>
        public static string[] GetNodesInnerText(string XmlDocumentText, string TagName) 
        {
            //判断XmlDocumentText是否为完整XML文档，如果不完整，创建根节点
            //过特定Tag获取XML节点集的各节点InnerText的字符串数组

            XmlNodeList elemList = null;
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(XmlDocumentText);
                //XmlNode book;
                XmlElement root = doc.DocumentElement;
                elemList = root.GetElementsByTagName(TagName);

                string[] s = new string[elemList.Count];

                for (int i = 0; i < elemList.Count; i++)
                { 
                    s[i] = elemList[i].InnerText;
                }

                return s;
            }
            catch (Exception e)
            {
                string str = "有多个根元素。";
                //如果是以下错误则加根节点，否则返回空节点。
                if (e.Message.Contains(str) == true)
                {
                    doc.LoadXml("<root>" + XmlDocumentText + "</root>");
                    XmlElement root = doc.DocumentElement;
                    elemList = root.GetElementsByTagName(TagName);

                    string[] s = new string[elemList.Count];

                    for (int i = 0; i < elemList.Count; i++)
                    {
                        s[i] = elemList[i].InnerText;
                    }

                    return s;
                }
                else
                {
                    string[] s = new string[0];
                    return s;
                }
            } 
        }

        /// <summary>
        ///  通过特定Tag获取XML第一个节点的InnerText
        /// </summary>
        /// <param name="XmlDocumentText">XML文档</param>
        /// <param name="TagName">Tag名称</param>
        /// <returns>第一个节点的InnerText</returns>
        public static string GetFistNodeInnerText(string XmlDocumentText, string TagName)
        {
            //判断XmlDocumentText是否为完整XML文档，如果不完整，创建根节点
            //获取第一个节点的InnerText
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.LoadXml(XmlDocumentText);
                XmlNode root = doc.DocumentElement;
                XmlNode node = root.SelectSingleNode(TagName);
                if(node == null)
                {
                    try
                    {
                        XmlNodeList list = GetNodeList(XmlDocumentText, TagName);
                        string str = "";
                        if (list.Count > 0)
                        {
                            str = list.Item(0).InnerText;
                        }
                        str = str.Replace("&lt;", "<");
                        str = str.Replace("&gt;", ">");
                        str = str.Replace("&amp;", "&");
                        return str;
                    }
                    catch
                    {
                        return "";
                    }
                }
                return node.InnerText;
            }
            catch (Exception e)
            {
                string str = "有多个根元素。";
                //如果是以下错误则加根节点，否则返回空节点。
                if (e.Message.Contains(str) == true)
                {
                    doc.LoadXml("<root>" + XmlDocumentText + "</root>");
                    XmlNode root = doc.DocumentElement;
                    XmlNode node = root.SelectSingleNode(TagName);
                    return node.InnerText;
                }
                else
                {
                    try
                    {
                        XmlNodeList list = GetNodeList(XmlDocumentText, TagName);
                        
                        if (list.Count > 0)
                        {
                            str = list.Item(0).InnerText;
                        }
                        str = str.Replace("&lt;", "<");
                        str = str.Replace("&gt;", ">");
                        str = str.Replace("&amp;", "&");
                        return str;
                    }
                    catch 
                    {
                        return "";
                    }
                }
            }
        }

 
        /// <summary>
        /// 通过特定Tag获取XML第一个节点的InnerXml
        /// </summary>
        /// <param name="XmlDocumentText">XML文档</param>
        /// <param name="TagName">Tag名称</param>
        /// <returns>第一个节点的InnerXml</returns>
        public static string GetFistNodeInnerXml(string XmlDocumentText, string TagName)
        {
            //判断XmlDocumentText是否为完整XML文档，如果不完整，创建根节点
            //获取第一个节点的InnerXML
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.LoadXml(XmlDocumentText);
                XmlNode root = doc.DocumentElement;
                XmlNode node = root.SelectSingleNode(TagName);
                return node.InnerXml;
            }
            catch (Exception e)
            {
                string str = "有多个根元素。";
                //如果是以下错误则加根节点，否则返回空节点。
                if (e.Message.Contains(str) == true)
                {
                    doc.LoadXml("<root>" + XmlDocumentText + "</root>");
                    XmlNode root = doc.DocumentElement;
                    XmlNode node = root.SelectSingleNode(TagName);
                    return node.InnerXml;
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 通过特定Tag获取XML第一个节点的所有Attribute
        /// </summary>
        /// <param name="XmlDocumentText">XML文档</param>
        /// <param name="TagName">Tag名称</param>
        /// <returns>XmlAttributeCollection</returns>
        public static XmlAttributeCollection GetFirstNodeAttributes(string XmlDocumentText, string TagName) 
        {
            //判断XmlDocumentText是否为完整XML文档，如果不完整，创建根节点
            //获取第一个节点的所有Attribute

            XmlDocument doc = new XmlDocument();

            try
            {
                doc.LoadXml(XmlDocumentText);
                XmlNode root = doc.DocumentElement;
                XmlNode node = root.SelectSingleNode(TagName);
                XmlAttributeCollection ac = node.Attributes;
                //Debug.Write(ac[0] + " ; " + ac.Count.ToString());

                return ac;
            }
            catch (Exception e)
            {
                string str = "有多个根元素。";
                //如果是以下错误则加根节点，否则返回空节点。
                if (e.Message.Contains(str) == true)
                {
                    doc.LoadXml("<root>" + XmlDocumentText + "</root>");
                    XmlNode root = doc.DocumentElement;
                    XmlNode node = root.SelectSingleNode(TagName);
                    XmlAttributeCollection ac = node.Attributes;
                    //Debug.Write(ac[0] + " ; " + ac.Count.ToString());

                    return ac;
                }
                else
                {                    
                    return null;
                }
            }
        }

        /// <summary>
        /// 通过特定Tag获取XML第一个节点的特定Attribute
        /// </summary>
        /// <param name="XmlDocumentText">XML文档</param>
        /// <param name="TagName">Tag名称</param>
        /// <param name="AttributeName">Attribute</param>
        /// <returns>Attribute字符串</returns>
        public static string GetFirstNodeAttribute(string XmlDocumentText, string TagName,string AttributeName)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.LoadXml(XmlDocumentText);
                XmlNode root = doc.DocumentElement;
                XmlNode node = root.SelectSingleNode(TagName);
                XmlAttribute at = (XmlAttribute)node.Attributes.GetNamedItem(AttributeName);
                //Debug.Write(at.Value);

                return at.Value;
            }
            catch (Exception e)
            {
                string str = "有多个根元素。";
                //如果是以下错误则加根节点，否则返回空节点。
                if (e.Message.Contains(str) == true)
                {
                    doc.LoadXml("<root>" + XmlDocumentText + "</root>");
                    XmlNode root = doc.DocumentElement;
                    XmlNode node = root.SelectSingleNode(TagName);
                    XmlAttribute at = (XmlAttribute)node.Attributes.GetNamedItem(AttributeName);
                    //Debug.Write(at.Value);

                    return at.Value;
                }
                else
                {
                    return "";
                }
            }
        }
   
    }
}
