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
        /// ͨ���ض�Tag��ȡXML�ڵ㼯
        /// </summary>
        /// <param name="XmlDocumentText">XML�ĵ�</param>
        /// <param name="TagName">Tag����</param>
        /// <returns>�ڵ㼯</returns>
        public static XmlNodeList GetNodeList(string XmlDocumentText,string TagName) 
        {
            XmlNodeList elemList = null;
            XmlDocument doc = new XmlDocument();

            //�ж�XmlDocumentText�Ƿ�Ϊ����XML�ĵ���������������������ڵ�
            //���ض�Tag��ȡXML�ڵ㼯
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
                string str = "�ж����Ԫ�ء�";
                //��������´�����Ӹ��ڵ㣬���򷵻ؿսڵ㡣
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
        /// ͨ���ض�Tag��ȡXML�ڵ㼯�ڵ��InnerText�ַ�������
        /// </summary>
        /// <param name="XmlDocumentText">XML�ĵ�</param>
        /// <param name="TagName">Tag����</param>
        /// <returns>���ڵ�InnerText������</returns>
        public static string[] GetNodesInnerText(string XmlDocumentText, string TagName) 
        {
            //�ж�XmlDocumentText�Ƿ�Ϊ����XML�ĵ���������������������ڵ�
            //���ض�Tag��ȡXML�ڵ㼯�ĸ��ڵ�InnerText���ַ�������

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
                string str = "�ж����Ԫ�ء�";
                //��������´�����Ӹ��ڵ㣬���򷵻ؿսڵ㡣
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
        ///  ͨ���ض�Tag��ȡXML��һ���ڵ��InnerText
        /// </summary>
        /// <param name="XmlDocumentText">XML�ĵ�</param>
        /// <param name="TagName">Tag����</param>
        /// <returns>��һ���ڵ��InnerText</returns>
        public static string GetFistNodeInnerText(string XmlDocumentText, string TagName)
        {
            //�ж�XmlDocumentText�Ƿ�Ϊ����XML�ĵ���������������������ڵ�
            //��ȡ��һ���ڵ��InnerText
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
                string str = "�ж����Ԫ�ء�";
                //��������´�����Ӹ��ڵ㣬���򷵻ؿսڵ㡣
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
        /// ͨ���ض�Tag��ȡXML��һ���ڵ��InnerXml
        /// </summary>
        /// <param name="XmlDocumentText">XML�ĵ�</param>
        /// <param name="TagName">Tag����</param>
        /// <returns>��һ���ڵ��InnerXml</returns>
        public static string GetFistNodeInnerXml(string XmlDocumentText, string TagName)
        {
            //�ж�XmlDocumentText�Ƿ�Ϊ����XML�ĵ���������������������ڵ�
            //��ȡ��һ���ڵ��InnerXML
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
                string str = "�ж����Ԫ�ء�";
                //��������´�����Ӹ��ڵ㣬���򷵻ؿսڵ㡣
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
        /// ͨ���ض�Tag��ȡXML��һ���ڵ������Attribute
        /// </summary>
        /// <param name="XmlDocumentText">XML�ĵ�</param>
        /// <param name="TagName">Tag����</param>
        /// <returns>XmlAttributeCollection</returns>
        public static XmlAttributeCollection GetFirstNodeAttributes(string XmlDocumentText, string TagName) 
        {
            //�ж�XmlDocumentText�Ƿ�Ϊ����XML�ĵ���������������������ڵ�
            //��ȡ��һ���ڵ������Attribute

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
                string str = "�ж����Ԫ�ء�";
                //��������´�����Ӹ��ڵ㣬���򷵻ؿսڵ㡣
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
        /// ͨ���ض�Tag��ȡXML��һ���ڵ���ض�Attribute
        /// </summary>
        /// <param name="XmlDocumentText">XML�ĵ�</param>
        /// <param name="TagName">Tag����</param>
        /// <param name="AttributeName">Attribute</param>
        /// <returns>Attribute�ַ���</returns>
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
                string str = "�ж����Ԫ�ء�";
                //��������´�����Ӹ��ڵ㣬���򷵻ؿսڵ㡣
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
