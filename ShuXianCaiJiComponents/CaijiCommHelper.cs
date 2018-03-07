using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BizCommon;
using Yqun.Services;
using Yqun.Common.Encoder;
using ShuXianCaiJiModule;

namespace ShuXianCaiJiComponents
{
    public class CaijiCommHelper
    {
        /// <summary>
        /// 记录日志
        /// </summary>
        Logger log = null;

        /// <summary>
        /// 采集软件SQL
        /// </summary>
        SqlLocalHlper _SqlLocalHlper = new SqlLocalHlper();

        /// <summary>
        /// 获取单个模板的配置文件
        /// </summary>
        List<JZTestConfig> _ListJzTestConfig = null;

        object _LockObject = new object();

        private SXCJModule _SXCJModule = null;

        public CaijiCommHelper(Logger log,SXCJModule model)
        {
            this.log = log;
            _SXCJModule = model;
        }
        /// <summary>
        /// 测试本地数据库连接
        /// </summary>
        /// <returns></returns>
        public bool TestDbConnection()
        {
            try
            {
                DataTable dt = _SqlLocalHlper.GetLocalData("select * from UserInfo where 1<>1;");
                if (dt != null && dt.Columns.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
                return false;
            }
        }

        /// <summary>
        /// 测试服务器连接
        /// </summary>
        /// <returns></returns>
        public bool TestServerConnection()
        {
            try
            {
                return bool.Parse(Agent.TestNetwork().ToString());
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, false);
                return false;
            }
        }

        /// <summary>
        ///  用户登录
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Pwd"></param>
        /// <returns></returns>
        public bool Login(string UserName, string Pwd, bool IsRemoteService)
        {
            try
            {
                return Agent.LoginProcess(UserName, Pwd, IsRemoteService);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
                return false;
            }
        }

        public string Login(string UserName, string Pwd,string MachineCode, bool IsRemoteService)
        {
            try
            {
                return Agent.LoginProcessmsg(UserName, Pwd, MachineCode, IsRemoteService);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
                return "false";
            }
        }

        /// <summary>
        /// 设置用户上下文
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Pwd"></param>
        public void SetUserContext(string UserName, string Pwd)
        {
            Agent.SetUserContext(UserName, Pwd);
        }

        /// <summary>
        /// 用户登出
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool LogOut(string UserName)
        {
            return false;
        }

        /// <summary>
        /// 获取用户登录信息
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public DataTable GetUserInfo(string UserName, string Pwd)
        {
            return _SqlLocalHlper.GetLocalData("select * from userinfo where UserName='" + UserName + "' and UserPwd='" + EncryptSerivce.Encrypt(Pwd) + "'");
        }

        /// <summary>
        /// 同步本地用户信息
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="UserPwd"></param>
        /// <param name="UserCode"></param>
        /// <param name="UserTestCode"></param>
        /// <returns></returns>
        public bool SysUserInfo(string UserName, string UserPwd, string UserCode, string UserTestCode)
        {
            DataTable _dt = _SqlLocalHlper.GetLocalData("select * from userinfo where UserName='" + UserName + "'");
            if (_dt != null && _dt.Rows.Count > 0)
            {
                _SqlLocalHlper.Execute("delete from userinfo where UserName='" + UserName + "'");

            }
            return _SqlLocalHlper.Execute("insert into UserInfo values('" + Guid.NewGuid().ToString() + "','" + UserName + "','" + EncryptSerivce.Encrypt(UserPwd) + "','" + UserCode + "','" + UserTestCode + "')");
        }

        /// <summary>
        /// 获取模板配置信息，并更新本地配置信息
        /// </summary>
        /// <returns></returns>
        public bool UpdateLocalModelInfo()
        {
            try
            {
                DataTable dt = GetTestConfigList();
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (_SqlLocalHlper.Execute("delete from sys_module_config;"))
                    {
                        StringBuilder tempsb = new StringBuilder();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (tempsb.Length > 0)
                            {
                                tempsb.Remove(0, tempsb.Length);
                            }
                            tempsb.Append("insert into sys_module_config values(");
                            tempsb.AppendFormat("'{0}'", dt.Rows[i]["ID"].ToString());
                            tempsb.AppendFormat(",'{0}'", dt.Rows[i]["ModuleID"].ToString());
                            tempsb.AppendFormat(",'{0}'", dt.Rows[i]["ModuleName"].ToString());
                            tempsb.AppendFormat(",{0}", dt.Rows[i]["DeviceType"].ToString());
                            tempsb.AppendFormat(",{0}", dt.Rows[i]["SerialNumber"].ToString());
                            tempsb.AppendFormat(",'{0}'", dt.Rows[i]["Config"].ToString());
                            tempsb.Append(");");
                            try
                            {
                                if (!_SqlLocalHlper.Execute(tempsb.ToString()))
                                {
                                    return false;
                                }
                            }
                            catch (Exception exConfigModel)
                            {
                                log.WriteLog("插入重复的模板信息，模板ID：" + dt.Rows[i]["ModuleID"].ToString(), true, false);
                                log.WriteLog(exConfigModel.StackTrace, true, false);
                            }
                        }
                    }
                }
                else
                {
                    log.WriteLog("未获取到模板配置信息", true, false);
                }
                return true;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
                return false;
            }
        }

        /// <summary>
        /// 根据实验室编码获取模板信息
        /// </summary>
        /// <param name="TestCode"></param>
        /// <returns></returns>
        public bool UpdateLocalModelInfo(string TestCode)
        {
            try
            {
                DataTable dt = GetTestConfigList(TestCode);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (_SqlLocalHlper.Execute("delete from sys_module_config;"))
                    {
                        StringBuilder tempsb = new StringBuilder();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (tempsb.Length > 0)
                            {
                                tempsb.Remove(0, tempsb.Length);
                            }
                            tempsb.Append("insert into sys_module_config values(");
                            tempsb.AppendFormat("'{0}'", dt.Rows[i]["ID"].ToString());
                            tempsb.AppendFormat(",'{0}'", dt.Rows[i]["ModuleID"].ToString());
                            tempsb.AppendFormat(",'{0}'", dt.Rows[i]["ModuleName"].ToString());
                            tempsb.AppendFormat(",{0}", dt.Rows[i]["DeviceType"].ToString());
                            tempsb.AppendFormat(",{0}", dt.Rows[i]["SerialNumber"].ToString());
                            tempsb.AppendFormat(",'{0}'", dt.Rows[i]["Config"].ToString());
                            tempsb.Append(");");
                            try
                            {
                                if (!_SqlLocalHlper.Execute(tempsb.ToString()))
                                {
                                    return false;
                                }
                            }
                            catch (Exception exConfigModel)
                            {
                                log.WriteLog("插入重复的模板信息，模板ID：" + dt.Rows[i]["ModuleID"].ToString(), true, false);
                                log.WriteLog(exConfigModel.StackTrace, true, false);
                            }
                        }
                    }
                }
                else
                {
                    log.WriteLog("未获取到模板配置信息", true, false);
                }
                return true;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
                return false;
            }
        }

        /// <summary>
        /// 获取实验列表
        /// </summary>
        /// <param name="testroom">实验室Code</param>
        /// <param name="deviceType">实验设备类型</param>
        /// <returns>实验列表</returns>
        public DataTable GetStadiumList(String testroom, Int32 deviceType)
        {
            try
            {
                return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetStadiumList", new object[] { string.Empty, string.Empty, testroom, deviceType }) as DataTable;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
                return null;
            }
        }

        /// <summary>
        /// 获取模板配置信息
        /// </summary>
        /// <param name="TestCode"></param>
        /// <returns></returns>
        public DataTable GetTestConfigList(string TestCode)
        {
            try
            {
                return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetTestConfigList", new object[] { TestCode }) as DataTable;

            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
                return null;
            }
        }


        /// <summary>
        /// 获取模板配置信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetTestConfigList()
        {
            try
            {
                return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetTestConfigList", new object[] { }) as DataTable;

            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
                return null;
            }
        }

        /// <summary>
        /// 获取本地模板信息
        /// </summary>
        /// <param name="DeviceType"></param>
        /// <returns></returns>
        public DataTable GetLocalTestConfig(string DeviceType)
        {
            try
            {
                return _SqlLocalHlper.GetDataTable("select distinct moduleID,moduleName from  sys_module_config where DeviceType=" + DeviceType + ";");
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
                return null;
            }
        }

        /// <summary>
        /// 获取单个委托信息
        /// </summary>
        /// <param name="DeviceType"></param>
        /// <returns></returns>
        public DataTable GetSingleStadium(String testRoomCode, String wtbh)
        {
            try
            {
                return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetSingleStadium", new object[] { testRoomCode, wtbh }) as DataTable;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
                return null;
            }
        }


        /// <summary>
        /// 数据上传
        /// </summary>
        /// <param name="_JZTestData">实验数据对象</param>
        /// <param name="_TotalNumber">当前试验试件总数</param>
        /// <param name="_EquipmentCode">设备编码</param>
        /// <returns></returns>
        public bool UploadTestData(JZTestData _JZTestData, Int32 _TotalNumber, Int32 currentNumber, string _EquipmentCode,string _UploadJson,string UploadCode, List<JZTestCell> testData, List<JZRealTimeData> realTimeData)
        {
            try
            {
                Guid _tempGuid = UpdateCurrentDataAsLocal(_JZTestData, _TotalNumber, currentNumber, _EquipmentCode, _UploadJson, UploadCode, testData, realTimeData); 
                log.WriteLog("数据保存本地WTBH：" + _JZTestData.WTBH, false, false);
                Boolean uploaded = UploadTestData(_JZTestData.
DocumentID, _JZTestData.ModuleID, _JZTestData.StadiumID, _JZTestData.WTBH, _JZTestData.TestRoomCode, currentNumber, _JZTestData.UserName, ObjectToJson(testData), DataEncoder.GZipCompressString(ObjectToJson(realTimeData)), _TotalNumber, _EquipmentCode, _UploadJson, UploadCode,DateTime.Now.AddMinutes(_SXCJModule.SpecialSetting.TimeSpanMinute));
                if (uploaded)
                {
                    log.WriteLog("数据WTBH：" + _JZTestData.WTBH + "上传成功", false, false);
                    DelUpdateSuceesData(_tempGuid.ToString());
                    return true;
                }
                else
                {
                    log.WriteLog("数据WTBH：" + _JZTestData.WTBH + "上传失败", false, false);
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
            return false;
        }

        /// <summary>
        ///  上传数据，返回GUID
        /// </summary>
        /// <param name="_JZTestData"></param>
        /// <param name="_TotalNumber"></param>
        /// <param name="currentNumber"></param>
        /// <param name="_EquipmentCode"></param>
        /// <param name="_UploadJson"></param>
        /// <param name="UploadCode"></param>
        /// <param name="testData"></param>
        /// <param name="realTimeData"></param>
        /// <param name="_Guid"></param>
        /// <returns></returns>
        public bool UploadTestData(JZTestData _JZTestData, Int32 _TotalNumber, Int32 currentNumber, string _EquipmentCode, string _UploadJson, string UploadCode, List<JZTestCell> testData, List<JZRealTimeData> realTimeData,out Guid _Guid)
        {
            _Guid = Guid.Empty;
            try
            {
                _Guid = UpdateCurrentDataAsLocal(_JZTestData, _TotalNumber, currentNumber, _EquipmentCode, _UploadJson, UploadCode, testData, realTimeData);
                log.WriteLog("数据保存本地WTBH：" + _JZTestData.WTBH, false, false);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
            try
            {

                Boolean uploaded = UploadTestData(_JZTestData.
DocumentID, _JZTestData.ModuleID, _JZTestData.StadiumID, _JZTestData.WTBH, _JZTestData.TestRoomCode, currentNumber, _JZTestData.UserName, ObjectToJson(testData), DataEncoder.GZipCompressString(ObjectToJson(realTimeData)), _TotalNumber, _EquipmentCode, _UploadJson, UploadCode, DateTime.Now.AddMinutes(_SXCJModule.SpecialSetting.TimeSpanMinute));
                if (uploaded)
                {
                    log.WriteLog("数据WTBH：" + _JZTestData.WTBH + "上传成功", false, false);
                    DelUpdateSuceesData(_Guid.ToString());
                    return true;
                }
                else
                {
                    log.WriteLog("数据WTBH：" + _JZTestData.WTBH + "上传失败", false, false);
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
            _Guid = Guid.Empty;
            return false;
        }


        private Guid UpdateCurrentDataAsLocal(JZTestData _JZTestData, int _TotalNumber, int currentNumber, string EquipmenCode, string UpDataInfo, string UploadCode, List<JZTestCell> testData, List<JZRealTimeData> realTimeData)
        {
            Guid _tempGuid=Guid.NewGuid();
            StringBuilder _tempSB = new StringBuilder();
            _tempSB.Append("insert into sys_test_data values(");
            _tempSB.AppendFormat("'{0}'", _tempGuid);
            _tempSB.AppendFormat(",'{0}'", _JZTestData.DocumentID);
            _tempSB.AppendFormat(",'{0}'", _JZTestData.StadiumID);
            _tempSB.AppendFormat(",'{0}'", _JZTestData.ModuleID.ToString());
            _tempSB.AppendFormat(",'{0}'", _JZTestData.WTBH.ToString());
            _tempSB.AppendFormat(",'{0}'", _JZTestData.TestRoomCode.ToString());
            _tempSB.AppendFormat(",{0}", currentNumber.ToString());
            _tempSB.AppendFormat(",'{0}'", _JZTestData.UserName.ToString());
            _tempSB.AppendFormat(",'{0}'",DateTime.Now.AddMinutes(_SXCJModule.SpecialSetting.TimeSpanMinute));
            _tempSB.AppendFormat(",'{0}'", ObjectToJson(testData));
            _tempSB.AppendFormat(",'{0}'", DataEncoder.GZipCompressString(ObjectToJson(realTimeData)));
            _tempSB.AppendFormat(",1");
            _tempSB.AppendFormat(",{0}", _TotalNumber);
            _tempSB.AppendFormat(",'{0}'", _JZTestData.TestRoomCode);
            _tempSB.AppendFormat(",'{0}'", EquipmenCode);
            _tempSB.AppendFormat(",'{0}'", UpDataInfo.Replace("'","''"));
            _tempSB.AppendFormat(",'{0}'", UploadCode);
            _tempSB.AppendFormat(",0");
            _tempSB.AppendFormat(",0");
            _tempSB.Append(");");
            try
            {
                log.WriteLog(_tempSB.ToString(), false, false);

                lock (_LockObject)
                {
                    _SqlLocalHlper.Execute(_tempSB.ToString());
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
                try
                {
                    if (GetColumCount())
                    {
                        _SqlLocalHlper.Execute(_tempSB.ToString());
                    }
                }
                catch (Exception ex2)
                {
                    log.WriteLog(ex2.Message + "\r\n" + ex2.StackTrace, true, true);
                    log.WriteLog(_tempSB.ToString(), true, true);
                    if (ex2.Message.Trim() == "")
                    {
                        UpdateCurrentDataAsLocalFor2Sql(_JZTestData, _TotalNumber, currentNumber, EquipmenCode, UpDataInfo, UploadCode, testData, realTimeData);
                    }
                }
            }
            return _tempGuid;
        }

        private bool DelUpdateSuceesData(string TempID)
        {
            try
            {
                lock (_LockObject)
                {
                    _SqlLocalHlper.Execute("delete from sys_test_data where ID='" + TempID + "'");
                }
                return true;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
            return false; 
        }

        /// <summary>
        ///  上传数据
        /// </summary>
        /// <param name="documentID">资料ID</param>
        /// <param name="moduleID">模板ID</param>
        /// <param name="stadiumID">龄期ID</param>
        /// <param name="wtbh">委托编号</param>
        /// <param name="testRoomCode">实验室编码</param>
        /// <param name="seriaNumber">当前试件序号</param>
        /// <param name="userName">当前用户</param>
        /// <param name="testData">实验数据</param>
        /// <param name="realTimeData">实验实时数据</param>
        /// <param name="totalNumber">试件总数</param>
        /// <param name="machineCode">设备编码</param>
        /// <returns></returns>
        private Boolean UploadTestData(Guid documentID, Guid moduleID, Guid stadiumID, String wtbh, String testRoomCode,
               Int32 seriaNumber, String userName, String testData, String realTimeData, Int32 totalNumber, String machineCode, string UploadInfo, string UploadCode,DateTime TestTime)
        {
            try
            {
                return bool.Parse(Agent.CallService("Yqun.BO.BusinessManager.dll", "UploadTestDataWithSYTime",
                    new object[] {documentID, moduleID,  stadiumID,  (wtbh??""),  (testRoomCode??""),
                seriaNumber,  (userName??""),  (testData??""),  (realTimeData??""),totalNumber,
                (machineCode??""),(UploadInfo??""),(UploadCode??""),TestTime}).ToString());
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
                return false;
            }
        }

        /// <summary>
        /// 上传本地数据
        /// </summary>
        /// <returns></returns>
        public bool UpdateLocalDateToServer()
        {
            try
            {
                DataTable dt = _SqlLocalHlper.GetDataTable("select * from sys_test_data where Status=1 order by SerialNumber;");
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        try
                        {
                            if (UploadTestData(new Guid(dt.Rows[i]["DataID"].ToString()), new Guid(dt.Rows[i]["ModuleID"].ToString()), new Guid(dt.Rows[i]["StadiumID"].ToString()), dt.Rows[i]["WTBH"].ToString(), dt.Rows[i]["TestRoomCode"].ToString(), int.Parse(dt.Rows[i]["SerialNumber"].ToString()), dt.Rows[i]["UserName"].ToString(), dt.Rows[i]["TestData"].ToString(), dt.Rows[i]["RealTimeDate"].ToString(), System.Convert.ToInt32(dt.Rows[i]["TotalNumber"].ToString()), dt.Rows[i]["EquipmentCode"].ToString(), dt.Rows[i]["UploadInfo"].ToString(), dt.Rows[i]["UploadCode"].ToString(), DateTime.Parse(dt.Rows[i]["CreatedTime"].ToString())))
                            {
                                DelUpdateSuceesData(dt.Rows[i]["ID"].ToString());
                            }
                        }
                        catch (Exception ex)
                        {
                            log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, false, false);
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception exData)
            {
                log.WriteLog(exData.Message + "\r\n" + exData.StackTrace, true, true);
                return false;
            }
        }

        public void UpdateLocalDateToServerThread()
        {
            try
            {
                DataTable dt = _SqlLocalHlper.GetDataTable("select * from sys_test_data where Status=1 order by SerialNumber;");
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        try
                        {
                            if (UploadTestData(new Guid(dt.Rows[i]["DataID"].ToString()), new Guid(dt.Rows[i]["ModuleID"].ToString()), new Guid(dt.Rows[i]["StadiumID"].ToString()), dt.Rows[i]["WTBH"].ToString(), dt.Rows[i]["TestRoomCode"].ToString(), int.Parse(dt.Rows[i]["SerialNumber"].ToString()), dt.Rows[i]["UserName"].ToString(), dt.Rows[i]["TestData"].ToString(), dt.Rows[i]["RealTimeDate"].ToString(), System.Convert.ToInt32(dt.Rows[i]["TotalNumber"].ToString()), dt.Rows[i]["EquipmentCode"].ToString(), dt.Rows[i]["UploadInfo"].ToString(), dt.Rows[i]["UploadCode"].ToString(), DateTime.Parse(dt.Rows[i]["CreatedTime"].ToString())))
                            {
                                DelUpdateSuceesData(dt.Rows[i]["ID"].ToString());
                            }
                        }
                        catch (Exception ex)
                        {
                            log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, false, false);
                        }
                    }
                }
            }
            catch (Exception exData)
            {
                log.WriteLog(exData.Message + "\r\n" + exData.StackTrace, true, true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Guid"></param>
        /// <returns></returns>
        public bool UpdateLocalDateToServer(string _Guid)
        {
            try
            {
                DataTable dt = _SqlLocalHlper.GetDataTable("select * from sys_test_data where Status=1 and ID='"+_Guid.ToString()+"' order by SerialNumber;");
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        try
                        {
                            if (UploadTestData(new Guid(dt.Rows[i]["DataID"].ToString()), new Guid(dt.Rows[i]["ModuleID"].ToString()), new Guid(dt.Rows[i]["StadiumID"].ToString()), dt.Rows[i]["WTBH"].ToString(), dt.Rows[i]["TestRoomCode"].ToString(), int.Parse(dt.Rows[i]["SerialNumber"].ToString()), dt.Rows[i]["UserName"].ToString(), dt.Rows[i]["TestData"].ToString(), dt.Rows[i]["RealTimeDate"].ToString(), System.Convert.ToInt32(dt.Rows[i]["TotalNumber"].ToString()), dt.Rows[i]["EquipmentCode"].ToString(), dt.Rows[i]["UploadInfo"].ToString(), dt.Rows[i]["UploadCode"].ToString(), DateTime.Parse(dt.Rows[i]["CreatedTime"].ToString())))
                            {
                                DelUpdateSuceesData(dt.Rows[i]["ID"].ToString());
                            }
                        }
                        catch (Exception ex)
                        {
                            log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, false, false);
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception exData)
            {
                log.WriteLog(exData.Message + "\r\n" + exData.StackTrace, true, true);
                return false;
            }
        }
        /// <summary>
        /// 获取单个模板的配置信（本地）
        /// </summary>
        /// <param name="ModelID"></param>
        /// <returns></returns>
        public List<JZTestConfig> GetConfigJson(string ModelID)
        {
            if (_ListJzTestConfig == null)
            {
                _ListJzTestConfig = new List<JZTestConfig>();
            }
            else
            {
                _ListJzTestConfig.Clear();
            }
            DataTable dt = _SqlLocalHlper.GetDataTable("select * from sys_module_config where ModuleID='" + ModelID.ToUpper() + "' order by SerialNumber;");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JZTestConfig tempJZTestConfig = new JZTestConfig();
                    tempJZTestConfig.ModuleID = new Guid(dt.Rows[i]["ModuleID"].ToString());
                    tempJZTestConfig.SerialNumber = Int32.Parse(dt.Rows[i]["SerialNumber"].ToString());
                    tempJZTestConfig.Config = JsonToObject<List<JZTestCell>>(dt.Rows[i]["Config"].ToString());
                    _ListJzTestConfig.Add(tempJZTestConfig);
                }
            }
            return _ListJzTestConfig;
        }

        /// <summary>
        /// 获取单个模板的配置信（服务端）
        /// </summary>
        /// <param name="ModelID"></param>
        /// <returns></returns>
        public List<JZTestConfig> GetConfigJson(string ModelID,Boolean IsServer)
        {
            if (_ListJzTestConfig == null)
            {
                _ListJzTestConfig = new List<JZTestConfig>();
            }
            else
            {
                _ListJzTestConfig.Clear();
            }
            String Sql_Select = "select * from sys_module_config where ModuleID='" + ModelID.ToUpper() + "' order by SerialNumber;";
            DataTable dt = Agent.CallRemoteService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { Sql_Select }) as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JZTestConfig tempJZTestConfig = new JZTestConfig();
                    tempJZTestConfig.ModuleID = new Guid(dt.Rows[i]["ModuleID"].ToString());
                    tempJZTestConfig.SerialNumber = Int32.Parse(dt.Rows[i]["SerialNumber"].ToString());
                    tempJZTestConfig.Config = JsonToObject<List<JZTestCell>>(dt.Rows[i]["Config"].ToString());
                    _ListJzTestConfig.Add(tempJZTestConfig);
                }
            }
            return _ListJzTestConfig;
        }

        /// <summary>
        /// 获取铁道部模板
        /// </summary>
        /// <param name="ModelID"></param>
        /// <returns></returns>
        public string GetInfoModelCode(string ModelID)
        {
            try
            {
                return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetModuleInfoRTCode", new object[] { new Guid(ModelID) }).ToString();
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取设备编号
        /// </summary>
        /// <param name="ECode"></param>
        /// <returns></returns>
        public string GetECode(string ECode)
        {
            try
            {
                return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetECode", new object[] { ECode }).ToString();
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
                return string.Empty;
            }
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
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
                return string.Empty;
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
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
                return default(T);
            }
        }

        /// <summary>
        /// 判断表sys_text_data字段是否完成
        /// </summary>
        /// <returns></returns>
        public bool GetColumCount()
        {
            try
            {
                if (_SqlLocalHlper.GetDataTable("select * from sys_test_data where 1<>1").Columns.Count != 19)
                {
                    _SqlLocalHlper.ExcuteCommand("drop table sys_test_data");
                    _SqlLocalHlper.ExcuteCommand(@"CREATE TABLE [sys_test_data](
	                        [ID] VarChar (50) NOT NULL,
	                        [DataID] VarChar (50) NOT NULL,
	                        [StadiumID] VarChar (50) NOT NULL,
	                        [ModuleID] VarChar (50) NOT NULL,
	                        [WTBH] VarChar (100) NOT NULL,
	                        [TestRoomCode] VarChar (50) NOT NULL,
	                        [SerialNumber] Int  NOT NULL,
	                        [UserName] VarChar (50) NOT NULL,
	                        [CreatedTime] datetime NOT NULL,
	                        [TestData] Text NOT NULL,
	                        [RealTimeDate] Text NOT NULL,
	                        [Status] Int NOT NULL,
                        [TotalNumber] Int  NOT NULL,
	                        [MachineCode] VarChar (50) NOT NULL,
	                        [EquipmentCode] VarChar (50) NOT NULL,
	                        [UploadInfo] Text NOT NULL,
	                        [UploadCode] VarChar (10) NOT NULL,
	                        [UploadTDB] Int NOT NULL,
	                        [UploadEMC] Int NOT NULL
                        )");
                }
                return true;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
            return false;
        }

        /// <summary>
        /// 保存实验数据到本地；分2条sql完成
        /// </summary>
        /// <param name="_JZTestData"></param>
        /// <param name="_TotalNumber"></param>
        /// <param name="currentNumber"></param>
        /// <param name="EquipmenCode"></param>
        /// <param name="UpDataInfo"></param>
        /// <param name="UploadCode"></param>
        /// <param name="testData"></param>
        /// <param name="realTimeData"></param>
        /// <returns></returns>
        public bool UpdateCurrentDataAsLocalFor2Sql(JZTestData _JZTestData, int _TotalNumber, int currentNumber, string EquipmenCode, string UpDataInfo, string UploadCode, List<JZTestCell> testData, List<JZRealTimeData> realTimeData)
        {
            bool IsSuceess = false;
            Guid _tempGuid = Guid.NewGuid();
            StringBuilder _tempSB = new StringBuilder();
            _tempSB.Append("insert into sys_test_data values(");
            _tempSB.AppendFormat("'{0}'", _tempGuid);
            _tempSB.AppendFormat(",'{0}'", Guid.Empty);
            _tempSB.AppendFormat(",'{0}'", Guid.Empty);
            _tempSB.AppendFormat(",'{0}'", _JZTestData.ModuleID.ToString());
            _tempSB.AppendFormat(",'{0}'", _JZTestData.WTBH.ToString());
            _tempSB.AppendFormat(",'{0}'", _JZTestData.TestRoomCode.ToString());
            _tempSB.AppendFormat(",{0}", currentNumber.ToString());
            _tempSB.AppendFormat(",'{0}'", _JZTestData.UserName.ToString());
            _tempSB.AppendFormat(",'{0}'", DateTime.Now.AddMinutes(_SXCJModule.SpecialSetting.TimeSpanMinute));
            _tempSB.AppendFormat(",'{0}'", ObjectToJson(testData));
            _tempSB.AppendFormat(",'{0}'", "");
            _tempSB.AppendFormat(",1");
            _tempSB.AppendFormat(",{0}", _TotalNumber);
            _tempSB.AppendFormat(",'{0}'", _JZTestData.TestRoomCode);
            _tempSB.AppendFormat(",'{0}'", EquipmenCode);
            _tempSB.AppendFormat(",'{0}'", UpDataInfo.Replace("'", "''"));
            _tempSB.AppendFormat(",'{0}'", UploadCode);
            _tempSB.AppendFormat(",0");
            _tempSB.AppendFormat(",0");
            _tempSB.Append(");");
            try
            {
                log.WriteLog(_tempSB.ToString(), false, false);
                _SqlLocalHlper.Execute(_tempSB.ToString());
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
                try
                {
                    if (GetColumCount())
                    {
                        _SqlLocalHlper.Execute(_tempSB.ToString());
                    }
                }
                catch (Exception ex2)
                {
                    log.WriteLog(ex2.Message + "\r\n" + ex2.StackTrace, true, true);
                    log.WriteLog(_tempSB.ToString(), true, true);
                    IsSuceess = false;
                }
            }
            _tempSB.Remove(0, _tempSB.Length);
            _tempSB.AppendFormat("update sys_test_data set RealTimeDate='{0}' where ID='{1}'", ObjectToJson(realTimeData),_tempGuid.ToString());
            try
            {
                log.WriteLog(_tempSB.ToString(), false, false);
                _SqlLocalHlper.Execute(_tempSB.ToString());
                IsSuceess = true;
            }
            catch (Exception ex1)
            {
                log.WriteLog(ex1.Message + "\r\n" + ex1.StackTrace, true, true);
            }

            return IsSuceess;
        }


        /// <summary>
        ///  上传实验室配置信息
        /// </summary>
        /// <param name="TestCode">实验室编码</param>
        /// <param name="MachineCode">设备编码编码</param>
        /// <param name="ClientConfigStr">配置JSON串</param>
        /// <returns>True:成功；False:失败</returns>
        public bool UploadClientConfig(string TestCode,string MachineCode, string ClientConfigStr)
        {
            try
            {
                return bool.Parse(Agent.CallService("Yqun.BO.BusinessManager.dll", "UploadClientConfig",
                        new object[] { TestCode,MachineCode, ClientConfigStr }).ToString());
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + System.Environment.NewLine + ex.StackTrace, true, true);
                return false;
            }
        }

        /// <summary>
        /// 添加采集系统配置信息
        /// </summary>
        /// <param name="TestCode"></param>
        /// <param name="MachineCode"></param>
        /// <param name="ClientConfigStr"></param>
        /// <param name="MachineType"></param>
        /// <param name="SoftType"></param>
        /// <returns></returns>
        public bool UploadClientConfigAndType(string TestCode, string MachineCode, string ClientConfigStr, string MachineType, string SoftType)
        {
            try
            {
                return bool.Parse(Agent.CallService("Yqun.BO.BusinessManager.dll", "UploadClientConfigAndType",
                        new object[] { TestCode, MachineCode, ClientConfigStr, MachineType, SoftType }).ToString());
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + System.Environment.NewLine + ex.StackTrace, true, true);
                return false;
            }
        }

        /// <summary>
        ///  更新实验室配置信息
        /// </summary>
        /// <param name="TestCode">实验室编码</param>
        /// <param name="ClientConfigStr">配置JSON串</param>
        /// <returns>True:成功；False:失败</returns>
        public bool UpDateClientConfig(string MachineCode, string ClientConfigStr)
        {
            try
            {
                return bool.Parse(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdataClientConfig",
                        new object[] { MachineCode, ClientConfigStr }).ToString());
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + System.Environment.NewLine + ex.StackTrace, true, true);
                return false;
            }
        }

        /// <summary>
        /// 更新服务器上客户端配置
        /// </summary>
        /// <param name="MachineCode">设备编码</param>
        /// <param name="ClientConfigStr">配置字符串</param>
        /// <param name="MachineType">设备类型：1、压力机；2、万能机</param>
        /// <param name="SoftType">软件类型：0、数显；1、电液伺服</param>
        /// <returns></returns>
        public bool UpDateClientConfigAndType(string MachineCode, string ClientConfigStr,string MachineType,string SoftType)
        {
            try
            {
                return bool.Parse(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdataClientConfigAndType",
                        new object[] { MachineCode, ClientConfigStr, MachineType, SoftType }).ToString());
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + System.Environment.NewLine + ex.StackTrace, true, true);
                return false;
            }
        }

        /// <summary>
        /// 获取服务器配置信息
        /// </summary>
        /// <param name="_SXCJModule">配置实例对象</param>
        /// <param name="MachineCode">设备编码编码</param>
        /// <returns>True:成功；False:失败</returns>
        public bool GetClientConfig(ref SXCJModule _SXCJModule, string MachineCode)
        {
            try
            {
                string ConfiStr = Agent.CallService("Yqun.BO.BusinessManager.dll", "GetClientConfig",
                            new object[] { MachineCode }).ToString();
                _SXCJModule.SpecialSetting = Newtonsoft.Json.JsonConvert.DeserializeObject<SpecialSetting>(ConfiStr);
                new ConfigOperation().SaveModul(_SXCJModule);
                SetClientConfigStatus(MachineCode);
                return true;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + System.Environment.NewLine + ex.StackTrace, true, true);
            }
            return false;
        }

        /// <summary>
        /// 获取实验室配置信息状态
        /// </summary>
        /// <param name="MachineCode">设备编码编码</param>
        /// <returns>
        /// -1：本地程序异常
        ///  0：不存在实验室信息，并以客服端为主同步实验室配置信息
        ///  1：已存在实验室信息，并以客服端为主同步实验室配置信息
        ///  2：已存在实验室信息，并以服务器为主同步实验室配置信息
        /// </returns>
        public int GetClientConfigStatus(string MachineCode)
        {
            try
            {
                return int.Parse(Agent.CallService("Yqun.BO.BusinessManager.dll", "GetClientConfigStatus",
                            new object[] { MachineCode }).ToString());
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + System.Environment.NewLine + ex.StackTrace, true, true);
                return -1;
            }
        }

        /// <summary>
        ///  设置客户端状态为主
        /// </summary>
        /// <param name="MachineCode">设备编码编码</param>
        /// <returns></returns>
        public bool SetClientConfigStatus(string MachineCode)
        {
            try
            {
                return bool.Parse(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateClientConfigStatus",new object[] { MachineCode }).ToString());
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + System.Environment.NewLine + ex.StackTrace, true, true);
                return false;
            }
        }

        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetServerTime()
        {
            try
            {
                return DateTime.Parse(Agent.CallService("Yqun.BO.LoginBO.dll", "GetServerTime",new object[] {}).ToString());
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + System.Environment.NewLine + ex.StackTrace, true, true);
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// 获取单挑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetSingeStadiumByDataID(Guid dataID)
        {
            try
            {
                return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetStadiumDataByDataID", new object[] { dataID }) as DataTable;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + System.Environment.NewLine + ex.StackTrace, true, true);
                return null;
            }
        }

        /// <summary>
        /// 公管中心是否验证
        /// </summary>
        /// <returns></returns>
        public bool GetIsRemotCheck()
        {
            try
            {
                return bool.Parse(Agent.CallService("Yqun.BO.BusinessManager.dll", "IsRemotGGZXCheck", new object[] { }).ToString());
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + System.Environment.NewLine + ex.StackTrace, true, true);
                return false;
            }
        }
    }
}
