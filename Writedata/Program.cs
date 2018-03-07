using System;
using System.Text;
using System.Data;
using Yqun.Service;
using System.IO;
using System.Threading;

using Yqun.Common.Encoder;
using System.Runtime.InteropServices;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BizCommon;
using System.Windows.Forms;
using ShuXianCaiJiModule;
using ShuXianCaiJiComponents;


namespace Writedata
{
    public class Program
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [DllImport("zhtClientApi.dll")]
        public extern static int Key_SignData(uint hashalgo, uint signType, string clearText, uint clearTextLength, ref byte sign, ref int signLength);
        [DllImport("SignRadom.dll")]
        public extern static int IsSign();

        static void Main(string[] args)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config"));
            log4net.Config.XmlConfigurator.ConfigureAndWatch(file);
            
            logger.Info(string.Format("WriteData:{0}", DateTime.Now.ToString()));

            Thread.Sleep(3000);
            DoMain();
        }

        public static void DoMain()
        {
            Logger log = new Logger(Path.Combine(Directory.GetCurrentDirectory(), "Log"));

            
                logger.Info("正在获取操作员信息...");
                String Operator = "";
                StringBuilder sql_login = new StringBuilder();
                sql_login.Append("select top 1 * from sys_login");

                DataTable LoginData = Agent.CallLocalService("Yqun.BO.BOBase.dll", "GetDataTable", new object[] { sql_login.ToString() }) as DataTable;
                if (LoginData != null && LoginData.Rows.Count > 0)
                {
                    Operator = LoginData.Rows[0]["username"].ToString();
                }

                logger.Info(string.Format("操作员的信息为 {0}", Operator));

                StringBuilder sql_select = new StringBuilder();
                sql_select.Append("select * from sys_testdata");

                DataTable Data = Agent.CallLocalService("Yqun.BO.BOBase.dll", "GetDataTable", new object[] { sql_select.ToString() }) as DataTable;
                if (Data == null || Data.Columns.Count == 0)
                {
                    logger.Info("获取待上传的数据失败");
                }
                else
                {
                    if (!ConfigurationManager.ConfigurationManager.AppSettings.ContainsKey("TestCode") || System.Convert.ToString(ConfigurationManager.ConfigurationManager.AppSettings["TestCode"]) == "")
                        throw new Exception("应用配置错误，无效的TestCode标记值");

                    String TestCode = System.Convert.ToString(ConfigurationManager.ConfigurationManager.AppSettings["TestCode"]);

                    if (!ConfigurationManager.ConfigurationManager.AppSettings.ContainsKey("EquipmentCode") || System.Convert.ToString(ConfigurationManager.ConfigurationManager.AppSettings["EquipmentCode"]) == "")
                        throw new Exception("应用配置错误，无效的EquipmentCode标记值");

                    String EquipmentCode = System.Convert.ToString(ConfigurationManager.ConfigurationManager.AppSettings["EquipmentCode"]);

                    if (!ConfigurationManager.ConfigurationManager.AppSettings.ContainsKey("Type") || (System.Convert.ToString(ConfigurationManager.ConfigurationManager.AppSettings["Type"]) != "1" &&
                                                                                 System.Convert.ToString(ConfigurationManager.ConfigurationManager.AppSettings["Type"]) != "2"))
                        throw new Exception("应用配置错误，无效的Type标记值" + (ConfigurationManager.ConfigurationManager.AppSettings.ContainsKey("Type") ? ":" + ConfigurationManager.ConfigurationManager.AppSettings["Type"].ToString() : ""));

                    String Type = System.Convert.ToString(ConfigurationManager.ConfigurationManager.AppSettings["Type"]);

                    if (!ConfigurationManager.ConfigurationManager.AppSettings.ContainsKey("ZSXH") || System.Convert.ToString(ConfigurationManager.ConfigurationManager.AppSettings["ZSXH"]) == "")
                        throw new Exception("应用配置错误，无效的ZSXH标记值");

                    String ZSXH = System.Convert.ToString(ConfigurationManager.ConfigurationManager.AppSettings["ZSXH"]);

                    logger.Info(string.Format("待上传的试验数据共{0}条", Data.Rows.Count));

                    //完善待发送数据的信息
                    foreach (DataRow Row in Data.Rows)
                    {
                        logger.Info("开始循环data数据");
                        try
                        {
                            Row["testcode"] = TestCode;
                            Row["sb_code"] = EquipmentCode;
                            Row["fsdate"] = DateTime.Now.ToString("yyyy-MM-dd");
                            Row["postinfo"] = Serialize.SerializeToBytes(GetPostInfo(Operator, Type, EquipmentCode, ZSXH, Row));

                            //2014.2.12 yuhongxi 新增新架构上传代码
                            byte[] b = (byte[])Data.Rows[0]["curve"];

                            string CurveText = System.Text.Encoding.Default.GetString(b);
                            logger.Info("1");
                            JZTestData _JZTestData = new JZTestData();

                            List<JZTestConfig> _JZTestConfig = null;
                            CaijiCommHelper _CaijiCommHelper = new CaijiCommHelper(log);
                            _JZTestConfig = _CaijiCommHelper.GetConfigJson(Row["modelid"].ToString(), true);
                            if (_JZTestConfig[int.Parse(Row["curorder"].ToString()) - 1].SerialNumber <= _JZTestConfig.Count)
                            {
                                for (int i = 0; i < _JZTestConfig[int.Parse(Row["curorder"].ToString()) - 1].Config.Count; i++)
                                {
                                    switch (_JZTestConfig[int.Parse(Row["curorder"].ToString()) - 1].Config[i].Name)
                                    {
                                        case JZTestEnum.DHBJ:
                                            {
                                                _JZTestConfig[int.Parse(Row["curorder"].ToString()) - 1].Config[i].Value = Row["dhbj"];
                                                break;
                                            }
                                        case JZTestEnum.LDZDL:
                                            {

                                                _JZTestConfig[int.Parse(Row["curorder"].ToString()) - 1].Config[i].Value = Row["zdlz"];
                                                break;
                                            }
                                        case JZTestEnum.PHHZ:
                                            {

                                                _JZTestConfig[int.Parse(Row["curorder"].ToString()) - 1].Config[i].Value = Row["zdlz"];
                                                break;
                                            }
                                        case JZTestEnum.QFL:
                                            {
                                                _JZTestConfig[int.Parse(Row["curorder"].ToString()) - 1].Config[i].Value = Row["qflz"];
                                                break;
                                            }
                                    }
                                }
                            }
                            _JZTestData.SerialNumber = _JZTestConfig[int.Parse(Row["curorder"].ToString()) - 1].SerialNumber;
                            _JZTestData.TestCells = _JZTestConfig[int.Parse(Row["curorder"].ToString()) - 1].Config;
                            if (string.IsNullOrEmpty(_JZTestData.WTBH))
                            {
                                _JZTestData.WTBH = Row["synum"].ToString();
                                _JZTestData.ModuleID = new Guid(Row["modelid"].ToString());
                            }
                            logger.Info(_JZTestData.WTBH);

                            _JZTestData.RealTimeData = new List<JZRealTimeData>();
                            string[] CurveTexts = CurveText.Split(';');
                            DateTime _SartTime;
                            _SartTime = DateTime.Now;
                            for (int i = 0; i < CurveTexts.Length; i++)
                            {
                                try
                                {
                                    string[] DataText = CurveTexts[i].Split(',');
                                    JZRealTimeData tempJZRealTimeData = new JZRealTimeData();
                                    tempJZRealTimeData.Time = _SartTime.AddSeconds(float.Parse(DataText[0]));
                                    tempJZRealTimeData.Value = float.Parse(DataText[2]);
                                    _JZTestData.RealTimeData.Add(tempJZRealTimeData);
                                }
                                catch (Exception ex)
                                {
                                    logger.Info(ex.Message);
                                }
                            }
                            DataTable dt = _CaijiCommHelper.GetSingleStadium((ConfigurationManager.ConfigurationManager.AppSettings["TestCode"].ToString()), _JZTestData.WTBH);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                _JZTestData.StadiumID = new Guid(dt.Rows[0]["id"].ToString());
                                _JZTestData.DocumentID = new Guid(dt.Rows[0]["DataID"].ToString());
                            }
                            logger.Info(_JZTestData.StadiumID.ToString());
                            logger.Info(_JZTestData.DocumentID.ToString());

                            sql_select = new StringBuilder();
                            sql_select.Append("select testcount from sys_testnum where synum ='" + _JZTestData.WTBH + "'");

                            DataTable dtcount = Agent.CallLocalService("Yqun.BO.BOBase.dll", "GetDataTable", new object[] { sql_select.ToString() }) as DataTable;
                            int TotalNumber = System.Convert.ToInt32(dtcount.Rows[0]["testcount"]);

                            if (_CaijiCommHelper.UploadTestData(_JZTestData, TotalNumber, _JZTestData.SerialNumber, (ConfigurationManager.ConfigurationManager.AppSettings["MachineCode"].ToString()), "", "", _JZTestData.TestCells, _JZTestData.RealTimeData))
                            {
                                StringBuilder sql_delete = new StringBuilder();
                                sql_delete.Append("delete from sys_testdata where id = ");
                                sql_delete.Append(System.Convert.ToInt32(Row["id"]));
                                object r = Agent.CallLocalService("Yqun.BO.BOBase.dll", "ExcuteCommand", new object[] { sql_delete.ToString() });
                                logger.Info(string.Format("清空sys_testdata数据表返回状态值：{0}", r));
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Info(ex.Message);
                        }
                        logger.Info("结束循环data数据");
                    }
                    #region OldCode
                    //logger.Info("开始上传数据");
                    ////调用传输服务将采集的数据上传到服务器
                    //Boolean Result = System.Convert.ToBoolean(Agent.CallRemoteService("Yqun.BO.DataTransportManager.dll", "WriteTestData", new object[] { Data }));

                    //String Message = (Result ? "上传采集的试验数据成功" : "上传采集的试验数据失败");
                    //logger.Info(Message);

                    //if (Result)
                    //{
                    //    logger.Info(string.Format("共上传试验数据{0}条", Data.Rows.Count));
                    //    List<string> SynumList = new List<string>();
                    //    foreach (DataRow Row in Data.Rows)
                    //    {
                    //        String synum = Row["synum"].ToString();
                    //        if (!SynumList.Contains(synum))
                    //        {
                    //            SynumList.Add(synum);
                    //        }
                    //    }
                    //    foreach (string synum in SynumList)
                    //    {
                    //        logger.Info(synum);
                    //    }
                    //}
                    //else//将数据缓存到sys_testdata_bak表中
                    //{
                    //    //修改PostDataInfo对象中的重发标记为1(重发数据)
                    //    foreach (DataRow Row in Data.Rows)
                    //    {
                    //        if (Row["postinfo"] != DBNull.Value)
                    //        {
                    //            PostDataInfo Info = Serialize.DeSerializeFromBytes((byte[])Row["postinfo"]) as PostDataInfo;
                    //            Info.CFBS = "1";
                    //            Row["postinfo"] = Serialize.SerializeToBytes(Info);
                    //        }
                    //    }

                    //    Result = System.Convert.ToBoolean(Agent.CallLocalService("Yqun.BO.BOBase.dll", "WriteTestDataBak", new object[] { Data }));
                    //    if (Result)
                    //    {
                    //        logger.Info("采集数据成功保存到缓存表中");
                    //    }
                    //    else
                    //    {
                    //        logger.Info("采集数据保存到缓存表中失败");
                    //    }
                    //}

                    //logger.Info(string.Format("正在标记试验项目已做：{0}", Result));

                    ////标记试验项目已做
                    //sql_select = new StringBuilder();
                    //sql_select.Append("select modelId,itemId,synum,lq from sys_testdata");
                    //DataTable testdata = Agent.CallLocalService("Yqun.BO.BOBase.dll", "GetDataTable", new object[] { sql_select.ToString() }) as DataTable;
                    //if (testdata != null && testdata.Columns.Count > 0)
                    //{
                    //    Result = System.Convert.ToBoolean(Agent.CallRemoteService("Yqun.BO.DataTransportManager.dll", "RemoteUpdateIsDone", new object[] { testdata }));
                    //    logger.Info(string.Format("标记试验项目已做：{0}", Result));
                    //}
                    //else
                    //{
                    //    logger.Info("标记试验项目已做出错");
                    //}

                    ////清空sys_testdata中的数据
                    //StringBuilder sql_delete = new StringBuilder();
                    //sql_delete.Append("delete from sys_testdata");
                    //object r = Agent.CallLocalService("Yqun.BO.BOBase.dll", "ExcuteCommand", new object[] { sql_delete.ToString() });
                    //logger.Info(string.Format("清空sys_testdata数据表返回状态值：{0}", r));
                    #endregion
                }
            
        }

        private static object GetPostInfo(String Operator, String Type, String EquipmentCode, String ZSXH, DataRow Row)
        {
            logger.Error("开始创建PostInfo信息");
            PostDataInfo postInfo = new PostDataInfo();
            try
            {
                string Json = "";
                string QMJson = string.Empty;
                byte[] QMResult = new Byte[173];

                String WTBH = Row["synum"].ToString();
                String SYSJ = Row["testdate"].ToString();
                String Size = Row["size"].ToString();
                String LQ = Row["lq"].ToString();
                float YSBJ = System.Convert.ToSingle(Row["ysbj"] != DBNull.Value ? Row["ysbj"] : "0");
                float DHBJ = System.Convert.ToSingle(Row["dhbj"] != DBNull.Value ? Row["dhbj"] : "0");
                float QFLZ = System.Convert.ToSingle(Row["qflz"] != DBNull.Value ? Row["qflz"] : "0");
                float MaxLZ = System.Convert.ToSingle(Row["zdlz"] != DBNull.Value ? Row["zdlz"] : "0");

                String Curve = "";
                List<string> values = new List<string>();
                List<string> _values = new List<string>();
                if (Row["curve"] != DBNull.Value)
                {
                    Curve = Encoding.Default.GetString((byte[])Row["curve"]);

                    Regex r = new Regex(",[0123456789.]+($|;)");
                    MatchCollection ms = r.Matches(Curve);
                    foreach (Match m in ms)
                    {
                        values.Add(m.Value.Trim(',', ';'));
                    }

                    String strtemp = "";
                    for (int i = 0; i < values.Count; i++)
                    {
                        if (strtemp.Length > 360)
                            break;
                        if (i % 3 == 0)
                            _values.Add(values[i]);
                        strtemp = string.Join(",", _values.ToArray());
                    }

                    _values.Add(MaxLZ.ToString());
                }

                if (Type == "1")
                {
                    YLJDataInfo yljInfo = new YLJDataInfo();
                    yljInfo.F_GUID = Guid.NewGuid().ToString();
                    yljInfo.F_ISWJJ = "0";//根据试验项目就可以区分是否掺外加剂
                    yljInfo.F_KYLZ = string.Join(",", _values.ToArray());
                    yljInfo.F_LQ = LQ;
                    yljInfo.F_OPERATOR = Operator;
                    yljInfo.F_RID = "1";
                    yljInfo.F_RTCODE = "";
                    yljInfo.F_SBBH = EquipmentCode;
                    yljInfo.F_SYSJ = SYSJ;
                    yljInfo.F_WTBH = WTBH;

                    logger.Error(yljInfo.F_KYLZ);

                    string json = JsonSerialize(yljInfo);
                    Json = ToBase64(json);
                }
                else if (Type == "2")
                {
                    WNJDataInfo wnjInfo = new WNJDataInfo();
                    wnjInfo.F_GUID = Guid.NewGuid().ToString();
                    wnjInfo.F_DHBJ = DHBJ.ToString();
                    wnjInfo.F_GCZJ = Size;
                    wnjInfo.F_LZ = string.Join(",", _values.ToArray());
                    wnjInfo.F_OPERATOR = Operator;
                    wnjInfo.F_QFLZ = QFLZ.ToString();
                    wnjInfo.F_RID = "1";
                    wnjInfo.F_RTCODE = "";
                    wnjInfo.F_SBBH = EquipmentCode;
                    wnjInfo.F_SCL = (YSBJ != 0 ? (DHBJ - YSBJ) / YSBJ : 0).ToString();
                    wnjInfo.F_SYSJ = SYSJ;
                    wnjInfo.F_WTBH = WTBH;

                    logger.Error(wnjInfo.F_LZ);

                    string json = JsonSerialize(wnjInfo);
                    Json = ToBase64(json);
                }

                postInfo.Type = Type;
                postInfo.MK = "2";
                postInfo.GN = (Type == "1" ? "1" : "2");//压力机是1，万能机是2
                postInfo.JMFS = "1";
                postInfo.BBH = "1";
                postInfo.KFSBH = "2";
                postInfo.ZSXH = ZSXH;
                postInfo.FSSJ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                postInfo.SBH = EquipmentCode;

                int IsNeedSign = 1;
                if (IsNeedSign == 0)
                {
                    int signLength = 0;
                    int r = Key_SignData(32772, 0, Json, (uint)Json.Length, ref QMResult[0], ref signLength);
                    if (r == 0)
                    {
                        List<byte> buffer = new List<byte>();
                        byte[] bttemp = new byte[1];
                        foreach (byte btt in QMResult)
                        {
                            if (btt != 0)
                            {
                                bttemp[0] = btt;
                                QMJson += Encoding.ASCII.GetString(bttemp);
                            }
                            else
                            {
                                break;
                            }
                        }
                        postInfo.SFQM = "1";
                    }
                    else if (r == -1)
                    {
                        //没有接入签名硬件
                        postInfo.SFQM = "2";
                    }
                    else if (r == 256)
                    {
                        //签名硬件过忙
                        postInfo.SFQM = "2";
                    }
                    else
                    {
                        //签名失败
                        postInfo.SFQM = "2";
                    }

                }
                else
                {
                    postInfo.SFQM = "0";
                }

                postInfo.ZWCD = Json.Length.ToString();
                postInfo.SZQM = QMJson;
                postInfo.JMZW = Json;
                postInfo.SJJY = "2";
                postInfo.CFBS = "0";
                postInfo.YLCS = "";

                return postInfo;
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);

            }
            return postInfo;
        }

        /// <summary>
        /// 将对象转换为 JSON 字符串。
        /// </summary>
        /// <param name="obj">要序列化的对象。</param>
        /// <returns>序列化的 JSON 字符串。</returns>
        private static string JsonSerialize(object obj)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            return jsSerializer.Serialize(obj);
        }

        private static string ToBase64(string NotBase64String)
        {
            byte[] BinaryData = System.Text.Encoding.Default.GetBytes(NotBase64String);
            string Base64 = System.Convert.ToBase64String(BinaryData);
            return Base64;
        }
    }
}
