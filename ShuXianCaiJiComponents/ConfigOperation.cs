using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShuXianCaiJiModule;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using SerialPortCommon;
using System.Windows.Forms;

namespace ShuXianCaiJiComponents
{
    public class ConfigOperation
    {


        private string CFilePath = string.Empty;
        private string SFilePath = string.Empty;


        public ConfigOperation()
        {
        }

        public ConfigOperation(string filepath)
        {
            CFilePath = filepath + "/CommSetting.jz";
            SFilePath = filepath + "/SpecialSetting.jz";
        }


        /// <summary>
        /// 实例对象转化Json串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string ObjectToJson(object obj)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Json装换称Object实例对象
        /// </summary>
        /// <param name="Json"></param>
        /// <returns></returns>
        public T JsonToObject<T>(string Json)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(Json);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 程序加载，获取配置信息
        /// </summary>
        /// <param name="_SXCJModule"></param>
        public void GetSXCJModule(out SXCJModule _SXCJModule)
        {
            try
            {
                string _FilePathSpecialSetting =string.Empty;

                if (string.IsNullOrEmpty(SFilePath))
                {
                    _FilePathSpecialSetting = Path.Combine(Application.StartupPath, "SpecialSetting.jz");
                }
                else
                {
                    _FilePathSpecialSetting = SFilePath;
                }
                _SXCJModule = new SXCJModule();
                if (!File.Exists(_FilePathSpecialSetting))
                {
                    _SXCJModule.SpecialSetting = new SpecialSetting();
                    GetOldSXCJModule(ref _SXCJModule);
                    SaveModul(_SXCJModule);
                }
                else
                {
                    _SXCJModule.SpecialSetting = JsonToObject<SpecialSetting>(UnZipStringAndOpen(_FilePathSpecialSetting));
                }
                string _FilePathCommSetting = string.Empty;
                if (string.IsNullOrEmpty(CFilePath))
                {
                    _FilePathCommSetting = Path.Combine(Application.StartupPath, "CommSetting.jz");
                }
                else
                {
                    _FilePathCommSetting = CFilePath;
                }
                if (File.Exists(_FilePathCommSetting))
                {
                    _SXCJModule.CommonSetting = JsonToObject<CommonSetting>(UnZipStringAndOpen(_FilePathCommSetting));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 保存配置信息
        /// </summary>
        /// <param name="_SXCJModule"></param>
        /// <returns></returns>
        public bool SaveModul(SXCJModule module)
        {
            try
            {
                string _FilePathSpecialSetting = Path.Combine(Application.StartupPath, "SpecialSetting.jz");
                string _FilePathCommSetting = Path.Combine(Application.StartupPath, "CommSetting.jz");
                ZipStringAndSave(_FilePathSpecialSetting, ObjectToJson(module.SpecialSetting));
                module.CommonSetting = new CommonSetting();
                module.CommonSetting.GJJBList = new List<ComboBoxItem>();
                module.CommonSetting.GJJBList.Add(new ComboBoxItem("235", "HPB235"));
                module.CommonSetting.GJJBList.Add(new ComboBoxItem("335", "HRB335"));
                module.CommonSetting.GJJBList.Add(new ComboBoxItem("335", "HRB335E"));
                module.CommonSetting.GJJBList.Add(new ComboBoxItem("300", "HPB300"));
                module.CommonSetting.GJJBList.Add(new ComboBoxItem("400", "HRB400"));
                module.CommonSetting.GJJBList.Add(new ComboBoxItem("400", "HRB400E"));
                module.CommonSetting.GJJBList.Add(new ComboBoxItem("500", "HRB500"));
                module.CommonSetting.GJJBList.Add(new ComboBoxItem("500", "HRB500E"));
                module.CommonSetting.GJJBList.Add(new ComboBoxItem("500", "CRB550"));
                module.CommonSetting.GJJBList.Add(new ComboBoxItem("585", "CRB650"));
                module.CommonSetting.GJJBList.Add(new ComboBoxItem("720", "CRB800"));
                module.CommonSetting.GJJBList.Add(new ComboBoxItem("875", "CRB970"));

                module.CommonSetting.GJSLList = new List<ComboBoxItem>();
                module.CommonSetting.GJSLList.Add(new ComboBoxItem("2", "2根"));
                module.CommonSetting.GJSLList.Add(new ComboBoxItem("3", "3根"));
                module.CommonSetting.GJSLList.Add(new ComboBoxItem("4", "4根"));
                module.CommonSetting.GJSLList.Add(new ComboBoxItem("5", "5根"));

                module.CommonSetting.HNTSLList = new List<ComboBoxItem>();
                module.CommonSetting.HNTSLList.Add(new ComboBoxItem("3", "1组（共3块）"));
                module.CommonSetting.HNTSLList.Add(new ComboBoxItem("6", "2组（共6块）"));
                module.CommonSetting.HNTSLList.Add(new ComboBoxItem("9", "3组（共9块）"));
                module.CommonSetting.HNTSLList.Add(new ComboBoxItem("12", "4组（共12块）"));
                module.CommonSetting.HNTSLList.Add(new ComboBoxItem("15", "5组（共15块）"));

                module.CommonSetting.GJCCLList = new List<ComboBoxItem>();
                for (int i = 4; i < 41; i++)
                {

                    if (i < 12)
                    {
                        module.CommonSetting.GJCCLList.Add(new ComboBoxItem(i.ToString(), i + "  mm"));
                        module.CommonSetting.GJCCLList.Add(new ComboBoxItem((i + 0.5).ToString(), i.ToString() + ".5mm"));
                    }
                    else
                    {
                        module.CommonSetting.GJCCLList.Add(new ComboBoxItem(i.ToString(), i + "mm"));
                    }
                }

                module.CommonSetting.HNTCCList = new List<ComboBoxItem>();
                module.CommonSetting.HNTCCList.Add(new ComboBoxItem("100×100×100", "100×100×100"));
                module.CommonSetting.HNTCCList.Add(new ComboBoxItem("150×150×150", "150×150×150"));
                module.CommonSetting.HNTCCList.Add(new ComboBoxItem("200×200×200", "200×200×200"));
                module.CommonSetting.HNTCCList.Add(new ComboBoxItem("70.7×70.7×70.7", "70.7×70.7×70.7"));
                module.CommonSetting.HNTCCList.Add(new ComboBoxItem("70.7×70.7×70.7", "φ100×100"));

                module.CommonSetting.HNTJBList = new List<ComboBoxItem>();
                module.CommonSetting.HNTJBList.Add(new ComboBoxItem("C5", "C5"));
                module.CommonSetting.HNTJBList.Add(new ComboBoxItem("C10", "C10"));
                module.CommonSetting.HNTJBList.Add(new ComboBoxItem("C15", "C15"));
                module.CommonSetting.HNTJBList.Add(new ComboBoxItem("C20", "C20"));
                module.CommonSetting.HNTJBList.Add(new ComboBoxItem("C25", "C25"));
                module.CommonSetting.HNTJBList.Add(new ComboBoxItem("C30", "C30"));
                module.CommonSetting.HNTJBList.Add(new ComboBoxItem("C35", "C35"));
                module.CommonSetting.HNTJBList.Add(new ComboBoxItem("C40", "C40"));
                module.CommonSetting.HNTJBList.Add(new ComboBoxItem("C45", "C45"));
                module.CommonSetting.HNTJBList.Add(new ComboBoxItem("C50", "C50"));
                module.CommonSetting.HNTJBList.Add(new ComboBoxItem("C55", "C55"));
                module.CommonSetting.HNTJBList.Add(new ComboBoxItem("C60", "C60"));
                module.CommonSetting.HNTJBList.Add(new ComboBoxItem("C65", "C65"));
                module.CommonSetting.HNTJBList.Add(new ComboBoxItem("C70", "C70"));
                module.CommonSetting.HNTJBList.Add(new ComboBoxItem("C75", "C75"));
                module.CommonSetting.HNTJBList.Add(new ComboBoxItem("C80", "C80"));

                module.CommonSetting.QFNameList = new List<ComboBoxItem>();
                module.CommonSetting.QFNameList.Add(new ComboBoxItem("通用", "通用"));
                module.CommonSetting.QFNameList.Add(new ComboBoxItem("新通用", "新通用"));
                module.CommonSetting.QFNameList.Add(new ComboBoxItem("OKEQF", "欧凯屈服"));
                module.CommonSetting.QFNameList.Add(new ComboBoxItem("FYQF", "丰仪屈服"));
                module.CommonSetting.QFNameList.Add(new ComboBoxItem("通用0722", "通用0722"));
                module.CommonSetting.CJNameList = new List<ComboBoxItem>();
                module.CommonSetting.CJNameList.Add(new ComboBoxItem("丰仪", "丰仪")); 
                module.CommonSetting.CJNameList.Add(new ComboBoxItem("欧凯", "欧凯"));
                module.CommonSetting.CJNameList.Add(new ComboBoxItem("建仪", "建仪"));
                module.CommonSetting.CJNameList.Add(new ComboBoxItem("欧凯", "晨鑫"));
                module.CommonSetting.CJNameList.Add(new ComboBoxItem("威海", "威海"));
                module.CommonSetting.CJNameList.Add(new ComboBoxItem("威海屏显", "威海屏显"));
                module.CommonSetting.CJNameList.Add(new ComboBoxItem("肯特新液晶", "肯特新液晶"));
                module.CommonSetting.CJNameList.Add(new ComboBoxItem("肯特液晶", "肯特液晶"));
                module.CommonSetting.CJNameList.Add(new ComboBoxItem("肯特数显", "肯特数显"));
                module.CommonSetting.CJNameList.Add(new ComboBoxItem("丰仪万能机", "丰仪万能机"));

                module.CommonSetting.CJNameList.Add(new ComboBoxItem("丰仪万能机转压力机", "丰仪万能机转压力机"));
                module.CommonSetting.CJNameList.Add(new ComboBoxItem("肯特AD液晶", "肯特AD液晶"));

                ZipStringAndSave(_FilePathCommSetting, ObjectToJson(module.CommonSetting));
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取配置文件Json字符串
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        private string UnZipStringAndOpen(string FilePath)
        {
            FileStream _FileStream = new FileStream(FilePath, FileMode.Open);
            try
            {
                byte[] temp = new byte[2048];
                int num = _FileStream.Read(temp, 0, temp.Length);
                //byte[] tempUnZip = DataEncoder.Decompress(temp, 0, num);

                _FileStream.Close();
                _FileStream = null;

                return DataEncoder.GZipDecompressString(System.Text.Encoding.UTF8.GetString(temp, 0, num));
            }
            catch (Exception ex)
            {
                _FileStream.Close();
                _FileStream = null;
                throw ex;
            }
        }

        /// <summary>
        /// 压缩Json并保存
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="JsonStr"></param>
        /// <returns></returns>
        private bool ZipStringAndSave(string FilePath, string JsonStr)
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                }
                FileStream _FileStream = new FileStream(FilePath, FileMode.CreateNew);
                try
                {
                    if (!string.IsNullOrEmpty(JsonStr) && JsonStr.Length > 0)
                    {

                        string tempEncoder = DataEncoder.GZipCompressString(JsonStr);
                        byte[] temp = System.Text.Encoding.UTF8.GetBytes(tempEncoder);
                        _FileStream.Write(temp, 0, temp.Length);
                        _FileStream.Flush();
                        _FileStream.Close();
                        _FileStream = null;
                    }
                }
                catch (Exception exf)
                {
                    _FileStream.Close();
                    _FileStream = null;
                    throw exf;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        #region 获取之前版本配置文件

        /// <summary>
        /// 从之前版本中读取用户配置信息
        /// </summary>
        /// <param name="_SXCJModule"></param>
        private void GetOldSXCJModule(ref SXCJModule _SXCJModule)
        {
            try
            {
                //DataTable _DataTable = GetCompany();
                //SerialPortInfo _SerialPortInfo = GetSerialPort();
                _SXCJModule.SpecialSetting.PortName = "COM1";

                _SXCJModule.SpecialSetting.PortBaud = 9600;
                _SXCJModule.SpecialSetting.MachineCompany = "丰仪";
                _SXCJModule.SpecialSetting.MachineType = 2;
                _SXCJModule.SpecialSetting.MachineCode = "00010001000100010000";
                _SXCJModule.SpecialSetting.TestRoomCode = "0001000100010000";
                _SXCJModule.SpecialSetting.MachineKeyCode = "";
                _SXCJModule.SpecialSetting.QFName = "新通用";
                _SXCJModule.SpecialSetting.DrawChartInterval = 1000;
                _SXCJModule.SpecialSetting.MinFinishValue = 10.00;
                _SXCJModule.SpecialSetting.MinValidValue = 20.00;
                _SXCJModule.SpecialSetting.PointNum = 2;
                _SXCJModule.SpecialSetting.QFPoints = 15;
                _SXCJModule.SpecialSetting.QFStartValueMPA = 100;
                _SXCJModule.SpecialSetting.QuFuType = 2;
                _SXCJModule.SpecialSetting.XDefaultMaxValue = 200;
                _SXCJModule.SpecialSetting.YDefaultMaxValue = 150;
                _SXCJModule.SpecialSetting.DrawChartInterval = 1000;
                _SXCJModule.SpecialSetting.InvalidValueRangeList = new List<ValueRange>();
                _SXCJModule.SpecialSetting.IsMedianValueFilteringAlgorithmNumber = true;
                _SXCJModule.SpecialSetting.MedianValueFilteringAlgorithmNumber = 9;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 返回仪表配置Table
        /// </summary>
        /// <returns></returns>
        private DataTable GetCompany()
        {
            try
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
                //File.Delete(FilePath);
                return CompanyInfos.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 返回串口信息配置对象
        /// </summary>
        /// <returns></returns>
        private SerialPortInfo GetSerialPort()
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
                //File.Delete(FilePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return SPInfo;
        }

        /// <summary>
        /// 返回2.0版本中串口配置对象
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private static object Deserialize(String xml)
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  设备类型转化
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string GetDeviceType(string name)
        {
            try
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
                            return name;
                        }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
