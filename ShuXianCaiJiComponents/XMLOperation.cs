using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using Yqun.Common.Encoder;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using SerialPortCommon;

namespace ShuXianCaiJiComponents
{
    public class XMLOperation
    {

        private string _XMLPath = Path.Combine(System.Windows.Forms.Application.StartupPath, "Config.xml");
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsExists()
        {
            return File.Exists(Path.Combine(System.Windows.Forms.Application.StartupPath, "Config.xml"));
        }

        /// <summary>
        /// 创建XML
        /// </summary>
        /// <returns></returns>
        public bool CreateXML()
        {
            try
            {
                DataTable _DataTableM = GetCompany();

                SerialPortInfo _DataTableS = GetSerialPort();

                XmlDocument _XmlDocument = new XmlDocument();

                XmlDeclaration _XmlDeclaration = _XmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
                _XmlDocument.AppendChild(_XmlDeclaration);

                XmlElement _XmlElementConfig = _XmlDocument.CreateElement("Config");
                _XmlDocument.AppendChild(_XmlElementConfig);

                XmlElement _XmlElementSerialPort = _XmlDocument.CreateElement("SerialPort");
                _XmlElementConfig.AppendChild(_XmlElementSerialPort);

                XmlElement _XmlElementPort = _XmlDocument.CreateElement("Port");
                if (_DataTableS == null)
                {
                    _XmlElementPort.InnerText = "COM1";
                }
                else
                {
                    _XmlElementPort.InnerText = _DataTableS.SerialPortName;
                }
                _XmlElementSerialPort.AppendChild(_XmlElementPort);
                XmlElement _XmlElementBaudRate = _XmlDocument.CreateElement("BaudRate");
                if (_DataTableS == null)
                {
                    _XmlElementBaudRate.InnerText = "9600";
                }
                else
                {
                    _XmlElementBaudRate.InnerText = _DataTableS.SerialPortBaudRate.ToString();
                }
                _XmlElementSerialPort.AppendChild(_XmlElementBaudRate);

                XmlElement _XmlElementDataBit = _XmlDocument.CreateElement("DataBit");
                if (_DataTableS == null)
                {
                    _XmlElementDataBit.InnerText = "8";
                }
                else
                {
                    _XmlElementDataBit.InnerText = _DataTableS.SerialPortDataBit.ToString();
                }
                _XmlElementSerialPort.AppendChild(_XmlElementDataBit);

                XmlElement _XmlElementParity = _XmlDocument.CreateElement("Parity");
                if (_DataTableS == null)
                {
                    _XmlElementParity.InnerText = "None";
                }
                else
                {
                    _XmlElementParity.InnerText = _DataTableS.SerialPortParity.ToString();
                }
                _XmlElementSerialPort.AppendChild(_XmlElementParity);

                XmlElement _XmlElementStopBit = _XmlDocument.CreateElement("StopBit");
                if (_DataTableS == null)
                {
                    _XmlElementStopBit.InnerText = "1";
                }
                else
                {
                    _XmlElementStopBit.InnerText = _DataTableS.SerialPortStopBit.ToString();
                }
                _XmlElementSerialPort.AppendChild(_XmlElementStopBit);

                XmlElement _XmlElementSystemParameters = _XmlDocument.CreateElement("SystemParameters");
                _XmlElementConfig.AppendChild(_XmlElementSystemParameters);

                XmlElement _XmlElementManuFacturer = _XmlDocument.CreateElement("ManuFacturer");
                if (_DataTableM == null || _DataTableM.Rows.Count <= 0)
                {
                    _XmlElementManuFacturer.InnerText = "丰仪";
                }
                else
                {
                    _XmlElementManuFacturer.InnerText = GetDeviceType(_DataTableM.Rows[0]["MachineName"].ToString());
                }
                _XmlElementSystemParameters.AppendChild(_XmlElementManuFacturer);

                XmlElement _XmlElementDeviceType = _XmlDocument.CreateElement("DeviceType");
                if (_DataTableM == null || _DataTableM.Rows.Count <= 0)
                {
                    _XmlElementDeviceType.InnerText = "1";
                }
                else
                {
                    _XmlElementDeviceType.InnerText = _DataTableM.Rows[0]["MachineType"].ToString();
                }
                _XmlElementSystemParameters.AppendChild(_XmlElementDeviceType);

                XmlElement _XmlElementSequenceSignature = _XmlDocument.CreateElement("SequenceSignature");
                if (_DataTableM == null || _DataTableM.Rows.Count <= 0)
                {
                    _XmlElementSequenceSignature.InnerText = string.Empty;
                }
                else
                {
                    _XmlElementSequenceSignature.InnerText = _DataTableM.Rows[0]["KeyCode"].ToString();
                }
                _XmlElementSystemParameters.AppendChild(_XmlElementSequenceSignature);

                XmlElement _XmlElementTestCode = _XmlDocument.CreateElement("TestCode");
                if (_DataTableM == null || _DataTableM.Rows.Count <= 0)
                {
                    _XmlElementTestCode.InnerText = string.Empty;
                }
                else
                {
                    _XmlElementTestCode.InnerText = _DataTableM.Rows[0]["MachineCode"].ToString();
                }
                _XmlElementSystemParameters.AppendChild(_XmlElementTestCode);

                XmlElement _XmlElementEquipmentCode = _XmlDocument.CreateElement("EquipmentCode");
                if (_DataTableM == null || _DataTableM.Rows.Count <= 0)
                {
                    _XmlElementEquipmentCode.InnerText = string.Empty;
                }
                else
                {
                    _XmlElementEquipmentCode.InnerText = _DataTableM.Rows[0]["EquipmentCode"].ToString();
                }
                _XmlElementSystemParameters.AppendChild(_XmlElementEquipmentCode);


                XmlElement _XmlElementReset = _XmlDocument.CreateElement("Reset");
                _XmlElementReset.InnerText = string.Empty;
                _XmlElementSystemParameters.AppendChild(_XmlElementReset);

                XmlElement _XmlElementPoint = _XmlDocument.CreateElement("Point");
                _XmlElementPoint.InnerText = "2";
                _XmlElementSystemParameters.AppendChild(_XmlElementPoint);
                if (!File.Exists(_XMLPath))
                {
                    FileStream fs =  File.Create(_XMLPath);
                    fs.Flush();
                    fs.Close();
                    fs.Dispose();
                }
                _XmlDocument.Save(_XMLPath);
                EncoderFile();
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                throw ex;
            }
        }



        /// <summary>
        /// 获取配置信息加载到主窗体
        /// </summary>
        /// <returns></returns>
        public DataTable GetXML()
        {
            DataTable _dt = null;
            try
            {
                if (!IsExists())
                {
                    CreateXML();
                }
                UnEncoderFile();
                XmlDocument _XmlDocument = new XmlDocument();
                _XmlDocument.Load(_XMLPath);
                _dt = new DataTable();
                _dt.TableName = _XmlDocument.SelectSingleNode(@"Config/SystemParameters/EquipmentCode").InnerText;
                DataColumn Column = new DataColumn("MachineName");
                Column.DataType = typeof(String);
                _dt.Columns.Add(Column);

                Column = new DataColumn("EquipmentCode");
                Column.DataType = typeof(String);
                _dt.Columns.Add(Column);

                Column = new DataColumn("HaveControl");
                Column.DataType = typeof(byte[]);
                _dt.Columns.Add(Column);

                Column = new DataColumn("MachineType");
                Column.DataType = typeof(String);
                _dt.Columns.Add(Column);

                Column = new DataColumn("KeyCode");
                Column.DataType = typeof(String);
                _dt.Columns.Add(Column);
                Column = new DataColumn("PortName");
                Column.DataType = typeof(String);
                _dt.Columns.Add(Column);
                Column = new DataColumn("PortBaud");
                Column.DataType = typeof(Int32);
                _dt.Columns.Add(Column);
                Column = new DataColumn("PortDateBit");
                Column.DataType = typeof(Int32);
                _dt.Columns.Add(Column);
                Column = new DataColumn("PortParity");
                Column.DataType = typeof(String);
                _dt.Columns.Add(Column);
                Column = new DataColumn("StopBit");
                Column.DataType = typeof(String);
                _dt.Columns.Add(Column);
                Column = new DataColumn("Reset");
                Column.DataType = typeof(String);
                _dt.Columns.Add(Column);
                Column = new DataColumn("Point");
                Column.DataType = typeof(String);
                _dt.Columns.Add(Column);
                DataRow Row = _dt.NewRow();
              
                Row["MachineName"] = _XmlDocument.SelectSingleNode(@"Config/SystemParameters/ManuFacturer").InnerText;
                Row["EquipmentCode"] = _XmlDocument.SelectSingleNode(@"Config/SystemParameters/EquipmentCode").InnerText;
                Row["MachineType"] = _XmlDocument.SelectSingleNode(@"Config/SystemParameters/DeviceType").InnerText;
                Row["KeyCode"] = _XmlDocument.SelectSingleNode(@"Config/SystemParameters/SequenceSignature").InnerText;
                Row["PortName"] = _XmlDocument.SelectSingleNode(@"Config/SerialPort/Port").InnerText;
                Row["PortBaud"] = _XmlDocument.SelectSingleNode(@"Config/SerialPort/BaudRate").InnerText;
                Row["PortDateBit"] = _XmlDocument.SelectSingleNode(@"Config/SerialPort/DataBit").InnerText;
                Row["PortParity"] = _XmlDocument.SelectSingleNode(@"Config/SerialPort/Parity").InnerText;
                Row["StopBit"] = _XmlDocument.SelectSingleNode(@"Config/SerialPort/StopBit").InnerText;
                Row["Reset"] = _XmlDocument.SelectSingleNode(@"Config/SystemParameters/Reset").InnerText;
                Row["Point"] = _XmlDocument.SelectSingleNode(@"Config/SystemParameters/Point").InnerText;
                _dt.Rows.Add(Row);
                EncoderFile();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return _dt;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <returns></returns>
        public bool EncoderFile()
        {
            try
            {
                StringBuilder _UMSB = new StringBuilder();
                FileStream _FS = new FileStream(_XMLPath, FileMode.Open);
                byte[] _tempbyte = new byte[1024];

                int num = _FS.Read(_tempbyte, 0, 1024);
                while (num > 0)
                {
                    _UMSB.Append(System.Text.Encoding.UTF8.GetString(_tempbyte, 0, num));
                    num = _FS.Read(_tempbyte, 0, 1024);
                }
                _FS.Close();
                _FS.Dispose();
                File.Delete(_XMLPath);
                FileStream _Efs = new FileStream(_XMLPath, FileMode.CreateNew);
                byte[] _Etempbyte = System.Text.Encoding.UTF8.GetBytes(EncryptSerivce.Encrypt(_UMSB.ToString()));
                _Efs.Write(_Etempbyte, 0, _Etempbyte.Length);
                _Efs.Flush();
                _Efs.Close();
                _Efs.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <returns></returns>
        public bool UnEncoderFile()
        {
            try
            {
                StringBuilder _UMSB = new StringBuilder();
                FileStream _FS = new FileStream(_XMLPath, FileMode.Open);
                byte[] _tempbyte = new byte[1024];

                int num = _FS.Read(_tempbyte, 0, 1024);
                while (num > 0)
                {
                    _UMSB.Append(System.Text.Encoding.UTF8.GetString(_tempbyte, 0, num));
                    num = _FS.Read(_tempbyte, 0, 1024);
                }
                _FS.Close();
                _FS.Dispose();
                File.Delete(_XMLPath);
                FileStream _Efs = new FileStream(_XMLPath, FileMode.CreateNew);
                byte[] _Etempbyte = System.Text.Encoding.UTF8.GetBytes(EncryptSerivce.Dencrypt(_UMSB.ToString()));
                _Efs.Write(_Etempbyte, 0, _Etempbyte.Length);
                _Efs.Flush();
                _Efs.Close();
                _Efs.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取MachineInfos
        /// </summary>
        /// <returns></returns>
        public DataTable GetCompany()
        {
            String FilePath = Path.Combine(Application.StartupPath, "MachineInfos.xml");
            if (!File.Exists(FilePath))
            {
                return null;
            }
            DataSet CompanyInfos = new DataSet();
            CompanyInfos.ReadXml(FilePath);

            if (!CompanyInfos.Tables[0].Columns.Contains("EquipmentCode"))
            {
                DataColumn _DataColumn = new DataColumn();
                _DataColumn.ColumnName = "EquipmentCode";
                CompanyInfos.Tables[0].Columns.Add(_DataColumn);
                CompanyInfos.Tables[0].Rows[0]["EquipmentCode"] = CompanyInfos.Tables[0].Rows[0]["MachineCode"] + "0000";
            }
            File.Delete(FilePath);
            return CompanyInfos.Tables[0];
        }

        /// <summary>
        public  SerialPortInfo GetSerialPort()
        {
            String FilePath = Path.Combine(Application.StartupPath, "SerialPortInfos.xml");
            if (!File.Exists(FilePath))
            {
                return null;
            }
            DataSet SerialPortDate = new DataSet();
            SerialPortInfo SPInfo = new SerialPortInfo();
            try
            {
                SerialPortDate.ReadXml(FilePath);

                SPInfo = (Deserialize(SerialPortDate.Tables[0].Rows[0]["SerialPortInfo"].ToString())) as SerialPortInfo;
                File.Delete(FilePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return SPInfo;
        }

        public static object Deserialize(String xml)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                byte[] bytes = System.Convert.FromBase64String(xml);
                MemoryStream ms = new MemoryStream(bytes);
                object obj = formatter.Deserialize(ms);
                ms.Close();
                return obj;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string GetDeviceType(string name)
        {
            switch (name.ToUpper())
            {
                case "TY":
                    {
                        return "丰仪";
                    }
                case "OKE":
                    {
                        return "欧凯";
                    }
                case "KENT":
                    {
                        return "肯特新液晶";
                    }
                case "SKENT":
                    {
                        return "肯特数显";
                    }
                case "OKENT":
                    {
                        return "肯特液晶";
                    }
                case "WH":
                    {
                        return "威海";
                    }
                case "JY":
                    {
                        return "建仪";
                    }
                default:
                    {
                        return "丰仪";
                    }
            }
        }
    }
}

