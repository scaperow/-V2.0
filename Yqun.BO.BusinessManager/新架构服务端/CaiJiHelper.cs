using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BizCommon;
using System.Threading;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Web;
using FarPoint.Win;
using FarPoint.Win.Spread;
using System.Reflection;
using FarPoint.Win.Spread.CellType;
using System.Configuration;
using RabbitMQ.Client;
using FarPoint.Win.Spread.Model;
using System.Text.RegularExpressions;
using System.Drawing;
using Yqun.Common.ContextCache;
using System.Collections;

namespace Yqun.BO.BusinessManager
{
    public class CaiJiHelper : BOBase
    {

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 获取上传到服务器的采集数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetUnusedTestData(string strWTBH)
        {
            strWTBH = strWTBH.Replace("'", "''");
            string strWhere = string.Empty;
            try
            {
                string dataID = new Guid(strWTBH).ToString();
                strWhere = " dataID = '" + dataID + "' ";
            }
            catch
            {
                strWhere = " WTBH like '%" + strWTBH + "%' ";
            }

            String sql = @"SELECT CASE d.Status WHEN '1' THEN '是' WHEN '0' THEN '否' WHEN '2' THEN '待' WHEN '3' THEN '过' WHEN '-1' THEN '存' WHEN '-2' THEN '错' WHEN '-3' THEN '错' ELSE CAST(Status AS VARCHAR(4)) END AS 是否入库,
                CASE  WHEN StadiumID='00000000-0000-0000-0000-000000000000' THEN '是' ELSE	'否' END AS 是否离线,
                d.CreatedTime AS 上传时间,
                d.WTBH AS 委托编号,
                m.Name AS 试验项目,
                d.SerialNumber AS 序号,
                d.UserName AS 试验用户,
                d.TestData AS 试验结果,
                 ModuleID ,d.ID,
                d.TestRoomCode AS 试验室编码,
                d.MachineCode AS 机器编码
                 FROM dbo.sys_test_data d
                 INNER JOIN dbo.sys_module m ON d.ModuleID=m.ID where " + strWhere +
                 " ORDER BY 委托编号, d.CreatedTime desc,序号";

            return GetDataTable(sql);
        }

        public Boolean InStadiumRange(Guid dataID, DateTime dtCreatedTime, string TestRoomCode)
        {
            Boolean flag = true;
            if (dataID != Guid.Empty)
            {
                String sql = "SELECT TestRoomCode FROM dbo.sys_stadium WHERE DataID='" + dataID + "' AND StartTime<='" + dtCreatedTime.ToString("yyyy-MM-dd HH:mm:ss") + "' AND EndTime>='" + dtCreatedTime.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                DataTable dt = GetDataTable(sql);
                if (dt != null && dt.Rows.Count == 0)
                {
                    flag = false;
                    //监理单位不进待审核
                    if (TestRoomCode.Length == 16)
                    {
                        sql = "SELECT NodeType FROM dbo.sys_engs_Tree WHERE NodeCode='" + TestRoomCode.Substring(0, 12) + "'";
                        dt = GetDataTable(sql);
                        if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["NodeType"].ToString() == "@unit_监理单位")
                        {
                            flag = true;
                        }
                    }
                }
            }
            return flag;
        }

        /// <summary>
        /// Status标记意义：
        /// -1：试验数据为删除状态
        /// 1：试验数据有效，并已存入管理系统
        /// 0：用户重新做的试验数据，并未存入管理系统，覆盖前试验
        /// 2：该数据已经上传到信息中心
        /// </summary>
        /// <returns></returns>
        public Boolean UploadTestData(Guid documentID, Guid moduleID, Guid stadiumID, String wtbh, String testRoomCode,
                Int32 seriaNumber, String userName, String testData, String realTimeData, Int32 totalNumber, String machineCode)
        {

            return UploadTestDataNew(documentID, moduleID, stadiumID, wtbh, testRoomCode, seriaNumber,
                userName, testData, realTimeData, totalNumber, machineCode, "", "");
        }

        /// <summary>
        /// 数据上传（新架构）
        /// Status标记意义：
        /// -1：试验数据为删除状态
        /// 1：试验数据有效，并已存入管理系统
        /// 0：用户重新做的试验数据，并未存入管理系统，覆盖前试验
        /// 2：该数据已经上传到信息中心
        /// </summary>
        /// <returns></returns>
        public Boolean UploadTestDataNew(Guid documentID, Guid moduleID, Guid stadiumID, String wtbh, String testRoomCode,
                Int32 seriaNumber, String userName, String testData, String realTimeData, Int32 totalNumber, String machineCode, String UploadInfo, string UploadCode)
        {
            return UploadTestDataWithSYTime(documentID, moduleID, stadiumID, wtbh, testRoomCode, seriaNumber, userName, testData, realTimeData, totalNumber, machineCode, UploadInfo, UploadCode, DateTime.Now);
        }

        /// <summary>
        /// 数据上传（新架构）
        /// Status标记意义：
        /// -1：试验数据为删除状态
        /// 1：试验数据有效，并已存入管理系统
        /// 0：用户重新做的试验数据，并未存入管理系统，覆盖前试验
        /// 2：该数据已经上传到信息中心
        /// </summary>
        /// <returns></returns>
        public Boolean UploadTestDataWithSYTime(Guid documentID, Guid moduleID, Guid stadiumID, String wtbh, String testRoomCode,
                Int32 seriaNumber, String userName, String testData, String realTimeData, Int32 totalNumber, String machineCode, String UploadInfo, string UploadCode, DateTime SYTime)
        {
            //logger.Info("收到数据：" + wtbh + "；seriaNumber=" + seriaNumber + "；testData=" + testData);
            Guid dataID = documentID;
            Guid id = Guid.NewGuid();
            DocumentHelper dh = new DocumentHelper();
            Boolean flag = false;
            int status = 0;
            try
            {
                if (stadiumID == Guid.Empty)
                {
                    //离线做试验
                    dataID = dh.GetDocumentIDByTestRoomModuleAndWTBH(moduleID, testRoomCode, wtbh);
                }
                String sql = String.Format(@"INSERT INTO dbo.sys_test_data
                            ( ID ,
                              DataID ,
                              StadiumID ,
                              ModuleID ,
                              WTBH ,
                              TestRoomCode ,
                              SerialNumber ,
                              UserName ,
                              CreatedTime ,
                              TestData ,
                              RealTimeData ,
                              MachineCode,
                              Status,
                              UploadInfo,
                              UploadCode,
                              TotallNumber
                            )
                    VALUES  ( '{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{15}','{8}','{9}','{10}',{11},'{12}','{13}',{14})",
                id, dataID, stadiumID, moduleID, wtbh, testRoomCode, seriaNumber, userName, testData, realTimeData,
                machineCode, status, UploadInfo.Replace("'", "''"), UploadCode, totalNumber, SYTime);
                flag = ExcuteCommand(sql) == 1;

                if (seriaNumber == totalNumber)
                {
                    if (stadiumID != Guid.Empty)
                    {
                        ExcuteCommand("UPDATE dbo.sys_stadium SET F_IsDone=1 WHERE ID='" + stadiumID + "'");
                    }
                    else if (dataID != Guid.Empty)
                    {
                        ExcuteCommand("UPDATE dbo.sys_stadium SET F_IsDone=1 WHERE DataID='" + dataID + "'");
                    }
                }
                string LineID = Yqun.Common.Encoder.EncryptSerivce.Dencrypt(ConfigurationManager.AppSettings["DataInstance"]);

                if (dataID != Guid.Empty && flag)
                {
                    var factory = new ConnectionFactory() { HostName = "localhost" };
                    using (var connection = factory.CreateConnection(999999))
                    {
                        using (var channel = connection.CreateModel())
                        {
                            channel.QueueDeclare(LineID, true, false, false, null);
                            string message = dataID.ToString() + "," + id.ToString();
                            var body = Encoding.UTF8.GetBytes(message);
                            IBasicProperties properties = channel.CreateBasicProperties();
                            properties.DeliveryMode = 2;


                            channel.BasicPublish("", LineID, properties, body);
                            //logger.Info(string.Format("RabbitMQ Send queue:{0} message:{1}", dataID.ToString(), message));
                            //Thread.Sleep(1000);
                        }
                    }

                    CaiJiRabbitMQHelper cjm = new CaiJiRabbitMQHelper();
                    cjm.StartApplyQueue(LineID);
                }
                else
                {
                    sql = "UPDATE dbo.sys_test_data SET Status=-2 WHERE ID=@ID";
                    SqlCommand cmd = new SqlCommand(sql);
                    cmd.Parameters.Add(new SqlParameter("@ID", id));

                    ExcuteCommandsWithTransaction(new List<IDbCommand>() { cmd });

                    logger.Error(wtbh + " 未进入sys_test_data表");
                }
            }
            catch (Exception ex)
            {
                logger.Error("UploadTestDataNew Error:wtbh:" + wtbh + ",ErrorMsg:" + ex.ToString());
                flag = false;
            }
            finally
            {
                UnLockDocument(dataID);
            }
            return flag;
        }
        public void ReloadRabbitMQ(string strStartTime)
        {
            string strWhere = " Status=0 ";
            DateTime dtStartTime = new DateTime();
            bool bIsDate = DateTime.TryParse(strStartTime, out dtStartTime);
            if (bIsDate == true)
            {
                strWhere += " and CreatedTime>'" + dtStartTime.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            }
            else
            {
                strWhere += " and WTBH='" + strStartTime + "'";
            }
            strWhere += " order by CreatedTime ";

            string strSQL = "SELECT ID,DataID FROM dbo.sys_test_data WHERE " + strWhere;
            DataTable dt = GetDataTable(strSQL);
            if (dt != null && dt.Rows.Count > 0)
            {
                string LineID = Yqun.Common.Encoder.EncryptSerivce.Dencrypt(ConfigurationManager.AppSettings["DataInstance"]);
                string DataID = string.Empty;
                string ID = string.Empty;

                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection(999999))
                {
                    using (var channel = connection.CreateModel())
                    {
                        //channel.QueueDeclare(dataID.ToString(), false, false, true, null);

                        channel.QueueDeclare(LineID, true, false, false, null);


                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataID = dt.Rows[i]["DataID"].ToString();
                            ID = dt.Rows[i]["ID"].ToString();

                            string message = DataID + "," + ID;
                            var body = Encoding.UTF8.GetBytes(message);
                            IBasicProperties properties = channel.CreateBasicProperties();
                            properties.DeliveryMode = 2;
                            channel.BasicPublish("", LineID, properties, body);
                            //logger.Info("ReloadRabbitMQ message:" + message);
                        }

                        //logger.Info(string.Format("RabbitMQ Send queue:{0} message:{1}", dataID.ToString(), message));
                        //Thread.Sleep(1000);
                    }
                }
                ManualUploadMQ();
            }
        }
        /// <summary>
        /// 手动上传RabbitMQ队列数据
        /// </summary>
        public void ManualUploadMQ()
        {
            string LineID = Yqun.Common.Encoder.EncryptSerivce.Dencrypt(ConfigurationManager.AppSettings["DataInstance"]);
            CaiJiRabbitMQHelper cjm = new CaiJiRabbitMQHelper();

            cjm.StartApplyQueue(LineID);

        }
        public static void Main(string[] args)
        {
            string[] ps = new string[]{
                                        @"E:\MyWork\铁路试验信息管理系统V2.0\客户端\D(Demo)(新架构)\AppConfig\Data",
                                        @"E:\MyWork\铁路试验信息管理系统V2.0\客户端\D(Demo)(新架构)\AppConfig\Dock",
                                        @"E:\MyWork\铁路试验信息管理系统V2.0\客户端\D(Demo)(新架构)\AppConfig\DataSource"
                                      };
            //Assembly a = Assembly.LoadFrom(@"C:\temp\JZTPS1\JZTPS\RefrenceCenter\BizComponents.dll");
            String DataAdapterType = "SQLClient";// DataSetCoder.GetProperty(DataSet, 0, "name", "DataAdapterType", "value").ToString();
            String DataBaseType = "MSSQLServer2k5";// DataSetCoder.GetProperty(DataSet, 0, "name", "DataBaseType", "value").ToString();
            String DataISAttach = "False";// DataSetCoder.GetProperty(DataSet, 0, "name", "DataISAttach", "value").ToString();
            String DataIntegratedLogin = "False";// DataSetCoder.GetProperty(DataSet, 0, "name", "DataIntegratedLogin", "value").ToString();

            string DataSource = "112.124.99.146";
            string DataInstance = "SYGLDB_Demo";
            string DataUserName = "sygldb_kingrocket_f";
            string DataPassword = "wdxlzyn@#830";

            ServerLoginInfos.DBConnectionInfo.DataAdapterType = DataAdapterType;
            ServerLoginInfos.DBConnectionInfo.DataBaseType = DataBaseType;
            ServerLoginInfos.DBConnectionInfo.DataSource = DataSource;
            ServerLoginInfos.DBConnectionInfo.DataInstance = DataInstance;
            ServerLoginInfos.DBConnectionInfo.DataUserName = DataUserName;
            ServerLoginInfos.DBConnectionInfo.DataPassword = DataPassword;
            ServerLoginInfos.DBConnectionInfo.DataISAttach = DataISAttach;
            ServerLoginInfos.DBConnectionInfo.LocalStartPath = AppDomain.CurrentDomain.BaseDirectory + @"\";
            ServerLoginInfos.DBConnectionInfo.DataIntegratedLogin = DataIntegratedLogin;
            for (int i = 0; i < ps.Length; i++)
            {
                if (!Directory.Exists(ps[i]))
                {
                    Directory.CreateDirectory(ps[i]);
                }
            }
            DocumentHelper dh = new DocumentHelper();
            ModuleHelper mh = new ModuleHelper();
            Guid moduleID = new Guid("F34C2B8B-DDBE-4C04-BD01-F08B0F479AE8");
            Sys_Module module = mh.GetModuleBaseInfoByID(moduleID);
            #region doc
            JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>("{\"ID\":\"0ea611dc-f512-4266-976b-99437eabeac1\",\"Sheets\":[{\"ID\":\"61460056-693c-4d71-b82b-dd1be919563b\",\"Name\":\"钢筋试验任务委托单\",\"Cells\":[{\"Name\":\"B5\",\"Value\":\"中铁一局成贵铁路项目经理部三分部\"},{\"Name\":\"C10\",\"Value\":\"P-11402505-21/B\"},{\"Name\":\"C11\",\"Value\":\"HPB300\"},{\"Name\":\"C12\",\"Value\":7.98},{\"Name\":\"C13\",\"Value\":\"文阁隧道钢筋加工场\"},{\"Name\":\"C14\",\"Value\":\"无油污、无锈蚀\"},{\"Name\":\"C15\",\"Value\":\"光圆\"},{\"Name\":\"C16\",\"Value\":\"文阁隧道\"},{\"Name\":\"C17\",\"Value\":\"隧道\"},{\"Name\":\"C18\",\"Value\":\"GB 1499.1-2008\"},{\"Name\":\"C19\",\"Value\":\"1.拉伸试验  2.弯曲试验\"},{\"Name\":\"C20\",\"Value\":null},{\"Name\":\"C21\",\"Value\":\"2014-09-18T00:00:00\"},{\"Name\":\"C22\",\"Value\":null},{\"Name\":\"C23\",\"Value\":null},{\"Name\":\"C24\",\"Value\":\"镇雄县大湾镇石田村\"},{\"Name\":\"C25\",\"Value\":null},{\"Name\":\"C7\",\"Value\":\"螺纹钢\"},{\"Name\":\"C8\",\"Value\":\"CGSG10-3-GY-YP20140918-02\"},{\"Name\":\"C9\",\"Value\":\"山西华丰冶炼集团有限公司\"},{\"Name\":\"G12\",\"Value\":\"t\"},{\"Name\":\"H22\",\"Value\":null},{\"Name\":\"H23\",\"Value\":null},{\"Name\":\"H24\",\"Value\":\"15096835803\"},{\"Name\":\"H25\",\"Value\":null},{\"Name\":\"H5\",\"Value\":\"CGSG10-3-GY-W20140918-02\"},{\"Name\":\"I11\",\"Value\":\"8\"},{\"Name\":\"A1\",\"Value\":\"Demo\"}]},{\"ID\":\"27fb3546-6297-4d50-9741-c55adf356613\",\"Name\":\"钢筋试验记录（线下）\",\"Cells\":[{\"Name\":\"A39\",\"Value\":null},{\"Name\":\"A49\",\"Value\":null},{\"Name\":\"B50\",\"Value\":null},{\"Name\":\"C12\",\"Value\":\"万能材料试验机\"},{\"Name\":\"C13\",\"Value\":\"连续式标点机\"},{\"Name\":\"C14\",\"Value\":\"游标卡尺\"},{\"Name\":\"C5\",\"Value\":\"CGSG10-3-GY-YP20140918-02\"},{\"Name\":\"C6\",\"Value\":\"HPB300\"},{\"Name\":\"C7\",\"Value\":\"山西华丰冶炼集团有限公司\"},{\"Name\":\"C8\",\"Value\":\"P-11402505-21/B\"},{\"Name\":\"C9\",\"Value\":7.98},{\"Name\":\"D12\",\"Value\":\"WE-600S\"},{\"Name\":\"D13\",\"Value\":\"LB-40\"},{\"Name\":\"D14\",\"Value\":\"/\"},{\"Name\":\"D15\",\"Value\":\"无油污、无锈蚀\"},{\"Name\":\"D19\",\"Value\":null},{\"Name\":\"D20\",\"Value\":null},{\"Name\":\"D21\",\"Value\":null},{\"Name\":\"D22\",\"Value\":null},{\"Name\":\"D23\",\"Value\":\"8.00\"},{\"Name\":\"D24\",\"Value\":\"0.0\"},{\"Name\":\"D25\",\"Value\":\"1\"},{\"Name\":\"D26\",\"Value\":null},{\"Name\":\"D27\",\"Value\":null},{\"Name\":\"D28\",\"Value\":null},{\"Name\":\"D29\",\"Value\":null},{\"Name\":\"D30\",\"Value\":null},{\"Name\":\"D31\",\"Value\":null},{\"Name\":\"D33\",\"Value\":\"8\"},{\"Name\":\"D34\",\"Value\":\"50.27\"},{\"Name\":\"D35\",\"Value\":\"40\"},{\"Name\":\"D36\",\"Value\":null},{\"Name\":\"D37\",\"Value\":null},{\"Name\":\"D38\",\"Value\":\"/\"},{\"Name\":\"D39\",\"Value\":null},{\"Name\":\"D40\",\"Value\":\"/\"},{\"Name\":\"D41\",\"Value\":\"/\"},{\"Name\":\"D42\",\"Value\":null},{\"Name\":\"D43\",\"Value\":\"延性断裂\"},{\"Name\":\"D44\",\"Value\":\"/\"},{\"Name\":\"D45\",\"Value\":\"/\"},{\"Name\":\"D46\",\"Value\":\"/\"},{\"Name\":\"D47\",\"Value\":\"/\"},{\"Name\":\"E21\",\"Value\":null},{\"Name\":\"E40\",\"Value\":null},{\"Name\":\"F12\",\"Value\":\"力-1\"},{\"Name\":\"F13\",\"Value\":\"力-3\"},{\"Name\":\"F14\",\"Value\":\"力-4\"},{\"Name\":\"F21\",\"Value\":null},{\"Name\":\"F9\",\"Value\":\"t\"},{\"Name\":\"G19\",\"Value\":null},{\"Name\":\"G20\",\"Value\":null},{\"Name\":\"G21\",\"Value\":null},{\"Name\":\"G22\",\"Value\":null},{\"Name\":\"G23\",\"Value\":\"8.02\"},{\"Name\":\"G24\",\"Value\":\"0.0\"},{\"Name\":\"G26\",\"Value\":null},{\"Name\":\"G27\",\"Value\":null},{\"Name\":\"G28\",\"Value\":null},{\"Name\":\"G29\",\"Value\":null},{\"Name\":\"G30\",\"Value\":null},{\"Name\":\"G31\",\"Value\":null},{\"Name\":\"G33\",\"Value\":null},{\"Name\":\"G36\",\"Value\":null},{\"Name\":\"G37\",\"Value\":null},{\"Name\":\"G38\",\"Value\":\"/\"},{\"Name\":\"G39\",\"Value\":null},{\"Name\":\"G40\",\"Value\":\"/\"},{\"Name\":\"G41\",\"Value\":\"/\"},{\"Name\":\"G42\",\"Value\":null},{\"Name\":\"G43\",\"Value\":\"延性断裂\"},{\"Name\":\"G46\",\"Value\":\"/\"},{\"Name\":\"G47\",\"Value\":\"/\"},{\"Name\":\"G50\",\"Value\":null},{\"Name\":\"G6\",\"Value\":\"GB1499.1-2008\"},{\"Name\":\"H12\",\"Value\":\"600kN\"},{\"Name\":\"H13\",\"Value\":\"/\"},{\"Name\":\"H14\",\"Value\":\"200mm\"},{\"Name\":\"H21\",\"Value\":null},{\"Name\":\"I21\",\"Value\":null},{\"Name\":\"I40\",\"Value\":null},{\"Name\":\"J12\",\"Value\":\"0.1kN\"},{\"Name\":\"J13\",\"Value\":\"/\"},{\"Name\":\"J14\",\"Value\":\"0.02mm\"},{\"Name\":\"J19\",\"Value\":null},{\"Name\":\"J20\",\"Value\":null},{\"Name\":\"J21\",\"Value\":null},{\"Name\":\"J22\",\"Value\":null},{\"Name\":\"J23\",\"Value\":\"7.96\"},{\"Name\":\"J24\",\"Value\":\"0.0\"},{\"Name\":\"J25\",\"Value\":null},{\"Name\":\"J26\",\"Value\":null},{\"Name\":\"J27\",\"Value\":null},{\"Name\":\"J28\",\"Value\":null},{\"Name\":\"J29\",\"Value\":null},{\"Name\":\"J30\",\"Value\":null},{\"Name\":\"J31\",\"Value\":null},{\"Name\":\"J33\",\"Value\":\"8\"},{\"Name\":\"J34\",\"Value\":\"50.27\"},{\"Name\":\"J35\",\"Value\":null},{\"Name\":\"J36\",\"Value\":null},{\"Name\":\"J37\",\"Value\":null},{\"Name\":\"J38\",\"Value\":\"/\"},{\"Name\":\"J39\",\"Value\":null},{\"Name\":\"J40\",\"Value\":\"/\"},{\"Name\":\"J41\",\"Value\":\"/\"},{\"Name\":\"J42\",\"Value\":\"/\"},{\"Name\":\"J43\",\"Value\":\"/\"},{\"Name\":\"J44\",\"Value\":\"180\"},{\"Name\":\"J45\",\"Value\":\"8\"},{\"Name\":\"J46\",\"Value\":\"无裂纹、无起皮\"},{\"Name\":\"J47\",\"Value\":\"合格\"},{\"Name\":\"K21\",\"Value\":null},{\"Name\":\"L12\",\"Value\":\"19\"},{\"Name\":\"L15\",\"Value\":\"GB 1499.1-2008\"},{\"Name\":\"L21\",\"Value\":null},{\"Name\":\"L5\",\"Value\":\"CGSG10-3-GY-J20140919-02\"},{\"Name\":\"L6\",\"Value\":\"CGSG10-3-GY-W20140918-02\"},{\"Name\":\"L7\",\"Value\":\"2014-09-18T00:00:00\"},{\"Name\":\"L8\",\"Value\":\"光圆\"},{\"Name\":\"L9\",\"Value\":\"2014-09-19T00:00:00\"},{\"Name\":\"M19\",\"Value\":null},{\"Name\":\"M20\",\"Value\":null},{\"Name\":\"M21\",\"Value\":null},{\"Name\":\"M22\",\"Value\":null},{\"Name\":\"M23\",\"Value\":\"7.98\"},{\"Name\":\"M24\",\"Value\":\"0.0\"},{\"Name\":\"M26\",\"Value\":null},{\"Name\":\"M27\",\"Value\":null},{\"Name\":\"M28\",\"Value\":null},{\"Name\":\"M29\",\"Value\":null},{\"Name\":\"M30\",\"Value\":null},{\"Name\":\"M31\",\"Value\":null},{\"Name\":\"M33\",\"Value\":null},{\"Name\":\"M36\",\"Value\":null},{\"Name\":\"M37\",\"Value\":null},{\"Name\":\"M38\",\"Value\":\"/\"},{\"Name\":\"M39\",\"Value\":null},{\"Name\":\"M40\",\"Value\":\"/\"},{\"Name\":\"M41\",\"Value\":\"/\"},{\"Name\":\"M42\",\"Value\":\"/\"},{\"Name\":\"M43\",\"Value\":\"/\"},{\"Name\":\"M46\",\"Value\":\"无裂纹、无起皮\"},{\"Name\":\"M47\",\"Value\":\"合格\"},{\"Name\":\"M50\",\"Value\":null},{\"Name\":\"N12\",\"Value\":\"/\"},{\"Name\":\"O23\",\"Value\":\"8.00\"},{\"Name\":\"O24\",\"Value\":\"0.0\"},{\"Name\":\"O40\",\"Value\":null},{\"Name\":\"A1\",\"Value\":\"Demo\"}]},{\"ID\":\"f7a42737-8464-4a9e-9cc2-668858d4a4b2\",\"Name\":\"钢筋试验报告（线下）\",\"Cells\":[{\"Name\":\"A28\",\"Value\":null},{\"Name\":\"A36\",\"Value\":\"《钢筋混凝土用钢 第1部分:热轧光圆钢筋》GB 1499.1-2008《金属材料 拉伸试验 第1部分:室温试验方法》GB/T 228.1-2010《金属材料 弯曲试验方法》GB/T 232-2010《冶金技术标准的数值修约与检测数值的判定》YB/T 081-2013\"},{\"Name\":\"B39\",\"Value\":null},{\"Name\":\"C5\",\"Value\":\"中铁一局成贵铁路项目经理部三分部\"},{\"Name\":\"C6\",\"Value\":\"文阁隧道\"},{\"Name\":\"C7\",\"Value\":\"隧道\"},{\"Name\":\"C8\",\"Value\":\"P-11402505-21/B\"},{\"Name\":\"C9\",\"Value\":7.98},{\"Name\":\"D13\",\"Value\":null},{\"Name\":\"D14\",\"Value\":null},{\"Name\":\"D15\",\"Value\":\"±0.3\"},{\"Name\":\"D16\",\"Value\":\"±7\"},{\"Name\":\"D17\",\"Value\":null},{\"Name\":\"D18\",\"Value\":null},{\"Name\":\"D19\",\"Value\":null},{\"Name\":\"D20\",\"Value\":null},{\"Name\":\"D21\",\"Value\":null},{\"Name\":\"D22\",\"Value\":\"/\"},{\"Name\":\"D23\",\"Value\":\"8\"},{\"Name\":\"D24\",\"Value\":\"50.27\"},{\"Name\":\"D25\",\"Value\":\"5d\"},{\"Name\":\"D26\",\"Value\":\"≥300\"},{\"Name\":\"D27\",\"Value\":\"≥420\"},{\"Name\":\"D28\",\"Value\":\"≥25.0\"},{\"Name\":\"D29\",\"Value\":\"≥10.0\"},{\"Name\":\"D30\",\"Value\":\"延性断裂\"},{\"Name\":\"D31\",\"Value\":\"180\"},{\"Name\":\"D32\",\"Value\":\"d\"},{\"Name\":\"D33\",\"Value\":\"无裂纹、无起皮\"},{\"Name\":\"D34\",\"Value\":\"合格\"},{\"Name\":\"E28\",\"Value\":\"/\"},{\"Name\":\"E31\",\"Value\":\"/\"},{\"Name\":\"E39\",\"Value\":null},{\"Name\":\"E8\",\"Value\":\"HPB300\"},{\"Name\":\"E9\",\"Value\":\"t\"},{\"Name\":\"F13\",\"Value\":\"/\"},{\"Name\":\"F14\",\"Value\":\"/\"},{\"Name\":\"F15\",\"Value\":\"0.0\"},{\"Name\":\"F16\",\"Value\":\"1\"},{\"Name\":\"F17\",\"Value\":null},{\"Name\":\"F18\",\"Value\":null},{\"Name\":\"F19\",\"Value\":null},{\"Name\":\"F20\",\"Value\":null},{\"Name\":\"F21\",\"Value\":null},{\"Name\":\"F22\",\"Value\":\"1\"},{\"Name\":\"F23\",\"Value\":\"8\"},{\"Name\":\"F24\",\"Value\":\"50.27\"},{\"Name\":\"F25\",\"Value\":40.0},{\"Name\":\"F26\",\"Value\":\"/\"},{\"Name\":\"F27\",\"Value\":\"/\"},{\"Name\":\"F28\",\"Value\":\"/\"},{\"Name\":\"F29\",\"Value\":\"/\"},{\"Name\":\"F30\",\"Value\":\"延性断裂\"},{\"Name\":\"F31\",\"Value\":\"/\"},{\"Name\":\"F32\",\"Value\":\"/\"},{\"Name\":\"F33\",\"Value\":\"/\"},{\"Name\":\"F34\",\"Value\":\"/\"},{\"Name\":\"I13\",\"Value\":\"/\"},{\"Name\":\"I14\",\"Value\":\"/\"},{\"Name\":\"I15\",\"Value\":\"0.0\"},{\"Name\":\"I18\",\"Value\":null},{\"Name\":\"I19\",\"Value\":null},{\"Name\":\"I20\",\"Value\":null},{\"Name\":\"I21\",\"Value\":null},{\"Name\":\"I22\",\"Value\":\"2\"},{\"Name\":\"I25\",\"Value\":null},{\"Name\":\"I26\",\"Value\":\"/\"},{\"Name\":\"I27\",\"Value\":\"/\"},{\"Name\":\"I28\",\"Value\":\"/\"},{\"Name\":\"I29\",\"Value\":\"/\"},{\"Name\":\"I30\",\"Value\":\"延性断裂\"},{\"Name\":\"I31\",\"Value\":null},{\"Name\":\"I33\",\"Value\":\"/\"},{\"Name\":\"I34\",\"Value\":\"/\"},{\"Name\":\"I36\",\"Value\":\"该批钢筋所检项目均符合《钢筋混凝土用钢 第一部分:热轧光圆钢筋》GB 1499.1-2008的规范要求。\"},{\"Name\":\"I39\",\"Value\":null},{\"Name\":\"K15\",\"Value\":\"0.0\"},{\"Name\":\"K5\",\"Value\":\"CGSG10-3-GY-G20140919-02\"},{\"Name\":\"K6\",\"Value\":\"CGSG10-3-GY-W20140918-02\"},{\"Name\":\"K7\",\"Value\":\"山西华丰冶炼集团有限公司\"},{\"Name\":\"K8\",\"Value\":\"CGSG10-3-GY-J20140919-02\"},{\"Name\":\"K9\",\"Value\":\"2014-09-19T00:00:00\"},{\"Name\":\"L13\",\"Value\":\"/\"},{\"Name\":\"L14\",\"Value\":\"/\"},{\"Name\":\"L15\",\"Value\":null},{\"Name\":\"L16\",\"Value\":null},{\"Name\":\"L17\",\"Value\":null},{\"Name\":\"L18\",\"Value\":null},{\"Name\":\"L19\",\"Value\":null},{\"Name\":\"L20\",\"Value\":null},{\"Name\":\"L21\",\"Value\":null},{\"Name\":\"L22\",\"Value\":\"3\"},{\"Name\":\"L23\",\"Value\":\"8\"},{\"Name\":\"L24\",\"Value\":\"50.27\"},{\"Name\":\"L25\",\"Value\":\"/\"},{\"Name\":\"L26\",\"Value\":\"/\"},{\"Name\":\"L27\",\"Value\":\"/\"},{\"Name\":\"L28\",\"Value\":\"/\"},{\"Name\":\"L29\",\"Value\":\"/\"},{\"Name\":\"L30\",\"Value\":\"/\"},{\"Name\":\"L31\",\"Value\":\"180\"},{\"Name\":\"L32\",\"Value\":\"8\"},{\"Name\":\"L33\",\"Value\":\"无裂纹、无起皮\"},{\"Name\":\"L34\",\"Value\":\"合格\"},{\"Name\":\"M15\",\"Value\":\"0.0\"},{\"Name\":\"N13\",\"Value\":\"/\"},{\"Name\":\"N14\",\"Value\":\"/\"},{\"Name\":\"N18\",\"Value\":null},{\"Name\":\"N19\",\"Value\":null},{\"Name\":\"N20\",\"Value\":null},{\"Name\":\"N21\",\"Value\":null},{\"Name\":\"N22\",\"Value\":\"4\"},{\"Name\":\"N25\",\"Value\":null},{\"Name\":\"N26\",\"Value\":\"/\"},{\"Name\":\"N27\",\"Value\":\"/\"},{\"Name\":\"N28\",\"Value\":\"/\"},{\"Name\":\"N29\",\"Value\":\"/\"},{\"Name\":\"N30\",\"Value\":\"/\"},{\"Name\":\"N31\",\"Value\":null},{\"Name\":\"N33\",\"Value\":\"无裂纹、无起皮\"},{\"Name\":\"N34\",\"Value\":\"合格\"},{\"Name\":\"N39\",\"Value\":null},{\"Name\":\"O15\",\"Value\":\"0.0\"},{\"Name\":\"Q11\",\"Value\":\"\"},{\"Name\":\"A1\",\"Value\":\"Demo\"}]},{\"ID\":\"3b1fa0f4-8c41-40dc-b9d7-3a6de845d087\",\"Name\":\"报验表\",\"Cells\":[{\"Name\":\"D13\",\"Value\":\"螺纹钢\"},{\"Name\":\"D14\",\"Value\":null},{\"Name\":\"D15\",\"Value\":\"7.98t\"},{\"Name\":\"D16\",\"Value\":null},{\"Name\":\"D17\",\"Value\":null},{\"Name\":\"D18\",\"Value\":null},{\"Name\":\"D19\",\"Value\":\"山西华丰冶炼集团有限公司\"},{\"Name\":\"D20\",\"Value\":null},{\"Name\":\"D21\",\"Value\":\"文阁隧道钢筋加工场\"},{\"Name\":\"D22\",\"Value\":null},{\"Name\":\"D23\",\"Value\":null},{\"Name\":\"D24\",\"Value\":null},{\"Name\":\"D25\",\"Value\":null},{\"Name\":\"E13\",\"Value\":null},{\"Name\":\"E14\",\"Value\":null},{\"Name\":\"E15\",\"Value\":null},{\"Name\":\"E16\",\"Value\":null},{\"Name\":\"E17\",\"Value\":null},{\"Name\":\"E18\",\"Value\":null},{\"Name\":\"E19\",\"Value\":null},{\"Name\":\"E20\",\"Value\":null},{\"Name\":\"E21\",\"Value\":null},{\"Name\":\"E22\",\"Value\":null},{\"Name\":\"E23\",\"Value\":null},{\"Name\":\"E24\",\"Value\":null},{\"Name\":\"E25\",\"Value\":null},{\"Name\":\"F13\",\"Value\":null},{\"Name\":\"F14\",\"Value\":null},{\"Name\":\"F15\",\"Value\":null},{\"Name\":\"F16\",\"Value\":null},{\"Name\":\"F17\",\"Value\":null},{\"Name\":\"F18\",\"Value\":null},{\"Name\":\"F19\",\"Value\":null},{\"Name\":\"F20\",\"Value\":null},{\"Name\":\"F21\",\"Value\":null},{\"Name\":\"F22\",\"Value\":null},{\"Name\":\"F23\",\"Value\":null},{\"Name\":\"F24\",\"Value\":null},{\"Name\":\"F25\",\"Value\":null},{\"Name\":\"G13\",\"Value\":null},{\"Name\":\"G14\",\"Value\":null},{\"Name\":\"G15\",\"Value\":null},{\"Name\":\"G16\",\"Value\":null},{\"Name\":\"G17\",\"Value\":null},{\"Name\":\"G18\",\"Value\":null},{\"Name\":\"G19\",\"Value\":null},{\"Name\":\"G20\",\"Value\":null},{\"Name\":\"G21\",\"Value\":null},{\"Name\":\"G22\",\"Value\":null},{\"Name\":\"G23\",\"Value\":null},{\"Name\":\"G24\",\"Value\":null},{\"Name\":\"G25\",\"Value\":null},{\"Name\":\"H13\",\"Value\":null},{\"Name\":\"H14\",\"Value\":null},{\"Name\":\"H15\",\"Value\":null},{\"Name\":\"H16\",\"Value\":null},{\"Name\":\"H17\",\"Value\":null},{\"Name\":\"H18\",\"Value\":null},{\"Name\":\"H19\",\"Value\":null},{\"Name\":\"H20\",\"Value\":null},{\"Name\":\"H21\",\"Value\":null},{\"Name\":\"H22\",\"Value\":null},{\"Name\":\"H23\",\"Value\":null},{\"Name\":\"H24\",\"Value\":null},{\"Name\":\"H25\",\"Value\":null},{\"Name\":\"I13\",\"Value\":null},{\"Name\":\"I14\",\"Value\":null},{\"Name\":\"I15\",\"Value\":null},{\"Name\":\"I16\",\"Value\":null},{\"Name\":\"I17\",\"Value\":null},{\"Name\":\"I18\",\"Value\":null},{\"Name\":\"I19\",\"Value\":null},{\"Name\":\"I20\",\"Value\":null},{\"Name\":\"I21\",\"Value\":null},{\"Name\":\"I22\",\"Value\":null},{\"Name\":\"I23\",\"Value\":null},{\"Name\":\"I24\",\"Value\":null},{\"Name\":\"I25\",\"Value\":null},{\"Name\":\"J13\",\"Value\":\"/\"},{\"Name\":\"J14\",\"Value\":\"/\"},{\"Name\":\"J15\",\"Value\":\"/\"},{\"Name\":\"J16\",\"Value\":\"/\"},{\"Name\":\"J17\",\"Value\":\"/\"},{\"Name\":\"J18\",\"Value\":\"/\"},{\"Name\":\"J19\",\"Value\":\"/\"},{\"Name\":\"J20\",\"Value\":\"/\"},{\"Name\":\"J21\",\"Value\":\"/\"},{\"Name\":\"J22\",\"Value\":\"/\"},{\"Name\":\"J23\",\"Value\":\"/\"},{\"Name\":\"J24\",\"Value\":\"/\"},{\"Name\":\"J25\",\"Value\":\"/\"},{\"Name\":\"C3\",\"Value\":\"新建铁路成都至贵阳线              乐山至贵阳段\"},{\"Name\":\"I3\",\"Value\":\"CGSG10标\"},{\"Name\":\"N3\",\"Value\":null},{\"Name\":\"I9\",\"Value\":null},{\"Name\":\"I10\",\"Value\":null},{\"Name\":\"I11\",\"Value\":null},{\"Name\":\"I12\",\"Value\":null},{\"Name\":\"K12\",\"Value\":null},{\"Name\":\"M12\",\"Value\":null},{\"Name\":\"A27\",\"Value\":null},{\"Name\":\"I28\",\"Value\":null},{\"Name\":\"M29\",\"Value\":null},{\"Name\":\"K29\",\"Value\":null},{\"Name\":\"I29\",\"Value\":null},{\"Name\":\"B4\",\"Value\":\"四川铁科建设监理有限公司成贵铁路CGJL-5标监理项目部\"},{\"Name\":\"A6\",\"Value\":\"\"},{\"Name\":\"A7\",\"Value\":\"\"},{\"Name\":\"A8\",\"Value\":\"\"},{\"Name\":\"A1\",\"Value\":\"Demo\"}]}]}");
            #endregion


            CaiJiHelper helper = new CaiJiHelper();

            List<List<JZTestCell>> testDates = new List<List<JZTestCell>>();
            testDates.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZTestCell>>("[{\"Name\":1,\"SheetID\":\"27fb3546-6297-4d50-9741-c55adf356613\",\"CellName\":\"D37\",\"Value\":18.5},{\"Name\":2,\"SheetID\":\"27fb3546-6297-4d50-9741-c55adf356613\",\"CellName\":\"D39\",\"Value\":28.3},{\"Name\":0,\"SheetID\":\"27fb3546-6297-4d50-9741-c55adf356613\",\"CellName\":\"D36\",\"Value\":\"53.50\"}]"));
            testDates.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZTestCell>>("[{\"Name\":1,\"SheetID\":\"27fb3546-6297-4d50-9741-c55adf356613\",\"CellName\":\"G37\",\"Value\":5.1},{\"Name\":2,\"SheetID\":\"27fb3546-6297-4d50-9741-c55adf356613\",\"CellName\":\"G39\",\"Value\":28.6},{\"Name\":0,\"SheetID\":\"27fb3546-6297-4d50-9741-c55adf356613\",\"CellName\":\"G36\",\"Value\":\"53.00\"}]"));
            foreach (var cells in testDates)
            {
                if (cells != null && cells.Count > 0)
                {
                    Boolean changed = false;
                    #region 值是否存在
                    foreach (JZTestCell cell in cells)
                    {
                        foreach (JZSheet sheet in doc.Sheets)
                        {
                            if (sheet.ID == cell.SheetID)
                            {
                                foreach (JZCell dataCell in sheet.Cells)
                                {
                                    if (dataCell.Name == cell.CellName)
                                    {
                                        if (dataCell.Value != null && dataCell.Value.ToString().Trim() != "" && dataCell.Value.ToString().Trim() != "/")
                                        {
                                            //已经存在值，不更新
                                            logger.Info("已经存在值" + dataCell.Value.ToString() + "，不更新: " + dataCell.Name + "<>" + cell.Value + "; wtbh=");
                                        }
                                        else
                                        {
                                            //用上传值去更新资料中的单元格
                                            logger.Info("更新单元格: " + dataCell.Name + "=" + cell.Value);
                                            dataCell.Value = cell.Value;
                                            changed = true;
                                        }
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                    }
                    #endregion

                    if (changed)
                    {
                        helper.GenerateDocNew(doc, moduleID);
                        //if (dh.ApplyModuleSetting(doc, module.ModuleSettings))//给modulesetting定义字段赋值
                        //{
                        //    //验证是否合格，并发短信
                        //    QualifyHelper qh = new QualifyHelper();
                        //    qh.Qualify(doc, module);
                        //}

                    }
                }
            }
            String json = Newtonsoft.Json.JsonConvert.SerializeObject(doc).Replace("'", "''");
            json = json + "";

        }

        #region 通用
        private object CallLocalService(string AssemblyName, string MethodName, object[] Parameters)
        {
            string DataSource = "112.124.99.146";
            string DataInstance = "SYGLDB_Demo";
            string DataUserName = "sygldb_kingrocket_f";
            string DataPassword = "wdxlzyn@#830";
            return null;// Yqun.Services.Transfer.CallLocalServiceWithDBArgs(AssemblyName, MethodName, Parameters, DataSource, DataInstance, DataUserName, DataPassword);
        }

        #endregion


        public Boolean ApplyUploadData(Guid id, Guid documentID, Guid moduleID, Guid stadiumID, String wtbh, String testRoomCode,
                Int32 seriaNumber, String userName, String testData, String realTimeData, Int32 totalNumber, String machineCode, String UploadInfo, string UploadCode)
        {
            return ApplyUploadDataNew(id, documentID, moduleID, stadiumID, wtbh, testRoomCode,
                 seriaNumber, userName, testData, realTimeData, totalNumber, machineCode, UploadInfo, UploadCode, "");
        }
        public Boolean ApplyUploadDataNew(Guid id, Guid documentID, Guid moduleID, Guid stadiumID, String wtbh, String testRoomCode,
                Int32 seriaNumber, String userName, String testData, String realTimeData, Int32 totalNumber, String machineCode, String UploadInfo, string UploadCode, string LineID)
        {
            Boolean isFinished = false;
            isFinished = seriaNumber == totalNumber;
            DocumentHelper dh = new DocumentHelper();
            Boolean flag = false;
            try
            {
                JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(dh.GetDocumentByID(documentID));
                String sql = "";
                if (doc == null)
                    return flag;
                ModuleHelper mh = new ModuleHelper();
                Sys_Module module = mh.GetModuleBaseInfoByID(moduleID);
                //FpSpread spread = null;
                List<JZTestCell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZTestCell>>(testData);
                if (cells != null && cells.Count > 0)
                {
                    Boolean changed = false;
                    #region 更新单元格
                    foreach (JZTestCell cell in cells)
                    {
                        foreach (JZSheet sheet in doc.Sheets)
                        {
                            if (sheet.ID == cell.SheetID)
                            {
                                foreach (JZCell dataCell in sheet.Cells)
                                {
                                    if (dataCell.Name == cell.CellName)
                                    {
                                        if (dataCell.Value != null && dataCell.Value.ToString().Trim() != "" && dataCell.Value.ToString().Trim() != "/")
                                        {
                                            //已经存在值，不更新
                                            logger.Info("已经存在值" + dataCell.Value.ToString() + "，不更新: " + dataCell.Name + "<>" + cell.Value + "; wtbh=" + wtbh);
                                        }
                                        else
                                        {
                                            //用上传值去更新资料中的单元格
                                            logger.Info("更新单元格: " + dataCell.Name + "=" + cell.Value + "; wtbh=" + wtbh);
                                            dataCell.Value = cell.Value;
                                            changed = true;
                                        }
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                    }
                    #endregion
                    #region 更新数据库
                    if (changed)
                    {
                        //模拟打开、保存资料、发送不合格报警
                        //spread = GenerateDoc(doc, moduleID);
                        GenerateDocNew(doc, moduleID);

                        //更新Document，和sys_test_data的Status为1
                        String json = Newtonsoft.Json.JsonConvert.SerializeObject(doc).Replace("'", "''");
                        sql = "UPDATE dbo.sys_document SET Data=@Data,MachineCode=@MachineCode,IsLock=1,LastEditedTime=getdate() WHERE ID=@ID";
                        SqlCommand cmd1 = new SqlCommand(sql);
                        cmd1.Parameters.Add(new SqlParameter("@ID", doc.ID));
                        cmd1.Parameters.Add(new SqlParameter("@Data", json));
                        cmd1.Parameters.Add(new SqlParameter("@MachineCode", machineCode));

                        sql = "UPDATE dbo.sys_test_data SET Status=0 WHERE DataID=@DataID AND SerialNumber=@SerialNumber";
                        SqlCommand cmd2 = new SqlCommand(sql);
                        cmd2.Parameters.Add(new SqlParameter("@DataID", doc.ID));
                        cmd2.Parameters.Add(new SqlParameter("@SerialNumber", seriaNumber));

                        sql = "UPDATE dbo.sys_test_data SET Status=1 WHERE ID=@ID";
                        SqlCommand cmd3 = new SqlCommand(sql);
                        cmd3.Parameters.Add(new SqlParameter("@ID", id));

                        flag = ExcuteCommandsWithTransaction(new List<IDbCommand>() { cmd1, cmd2, cmd3 });
                        if (flag)
                        {
                            logger.Info("ApplyUploadData 更新完毕 id:" + id);
                            if (dh.ApplyModuleSetting(doc, module.ModuleSettings))//给modulesetting定义字段赋值
                            {
                                //验证是否合格，并发短信
                                QualifyHelper qh = new QualifyHelper();
                                qh.Qualify(doc, module);
                            }
                            #region 上传实时数据到工管中心
                            if (!string.IsNullOrEmpty(module.ModuleALTGG) && ConfigurationManager.AppSettings["GGCStartUpload"] == "1")
                            {
                                GGCUploadHelper ggcUH = new GGCUploadHelper();
                                ggcUH.UploadCaiJiData(doc, module, id, machineCode, seriaNumber, testData, userName, realTimeData, testRoomCode);
                            }
                            #endregion
                        }
                        else
                        {
                            logger.Error("ExcuteCommandsWithTransaction 事物返回false，wtbh=" + wtbh);
                        }
                    }
                    else
                    {
                        sql = "UPDATE dbo.sys_test_data SET Status=-1 WHERE ID=@ID";
                        SqlCommand cmd = new SqlCommand(sql);
                        cmd.Parameters.Add(new SqlParameter("@ID", id));

                        flag = ExcuteCommandsWithTransaction(new List<IDbCommand>() { cmd });
                    }
                    #endregion
                }
                else
                {
                    UpdateTestDataStatus(id, -3);
                }

                //临时抓取工管中心数据
                if (ConfigurationManager.AppSettings["GetDataFromGGC"] == "1")
                {
                    try
                    {
                        GetDataFromEMC.GetDataIntervalClient GetDataFromEMC = new Yqun.BO.BusinessManager.GetDataFromEMC.GetDataIntervalClient();
                        string jsoPressureInfo = string.Empty;
                        string errMsg = string.Empty;
                        if (module.DeviceType == 1)
                        {
                            string GGCTestRoomCodeMap = System.Configuration.ConfigurationManager.AppSettings["GGCTestRoomCodeMap"];
                            Hashtable testRoomCodeMap = new Hashtable();
                            testRoomCodeMap = InitTestRoomCodeMap(GGCTestRoomCodeMap);
                            string xzbm = testRoomCodeMap[testRoomCode].ToString() + "YL01";
                            int i = GetDataFromEMC.GetPressureData(out jsoPressureInfo, out errMsg, "3", xzbm, wtbh);
                            if (i == -1)
                            {
                                logger.Error(errMsg + "YHX");
                            }
                            if (i == 1)
                            {
                                logger.Error(jsoPressureInfo + "YHX");
                            }

                        }
                        if (module.DeviceType == 2)
                        {
                            string GGCTestRoomCodeMap = System.Configuration.ConfigurationManager.AppSettings["GGCTestRoomCodeMap"];
                            Hashtable testRoomCodeMap = new Hashtable();
                            testRoomCodeMap = InitTestRoomCodeMap(GGCTestRoomCodeMap);
                            string xzbm = testRoomCodeMap[testRoomCode].ToString() + "WN01";
                            int i = GetDataFromEMC.GetUniversalData(out jsoPressureInfo, out errMsg, "3", xzbm, wtbh);
                            if (i == -1)
                            {
                                logger.Error(errMsg + "YHX");
                            }
                            if (i == 1)
                            {
                                logger.Error(jsoPressureInfo + "YHX");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message + "YHX");
                    }
                }


                ThreadParameter parameter = new ThreadParameter();
                parameter.IsSaved = flag;
                parameter.SeriaNumber = seriaNumber;
                parameter.DataID = documentID;
                parameter.TestData = testData;
                parameter.TestDataID = id;
                parameter.machineBH = machineCode;
                parameter.ModuleID = moduleID;
                parameter.realTimeData = realTimeData;
                parameter.stadiumID = stadiumID;
                parameter.testRoomCode = testRoomCode;
                parameter.userName = userName;
                parameter.wtbh = wtbh;
                parameter.isFinished = seriaNumber == totalNumber;
                parameter.LineID = LineID;
                ThreadPool.QueueUserWorkItem(new WaitCallback(Execute), parameter);
            }
            catch (Exception ex)
            {
                logger.Error("" + LineID + "ApplyUploadData Error:wtbh:" + wtbh + ",ErrorMsg:" + ex.ToString());
                flag = false;
                //throw;
            }
            finally
            {
                //UnLockDocument(documentID);
                //ContinueProcess(documentID);
                GC.Collect();
                //Thread.Sleep(500);
            }
            return flag;
        }

        private Hashtable InitTestRoomCodeMap(String map)
        {
            Hashtable testRoomCodeMap = new Hashtable();
            if (!String.IsNullOrEmpty(map))
            {
                String[] item = map.Split(new Char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (item.Length > 0)
                {
                    foreach (var sub in item)
                    {
                        String[] subItem = sub.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        if (subItem.Length == 2)
                        {
                            testRoomCodeMap.Add(subItem[0], subItem[1]);
                        }
                    }
                }
            }
            return testRoomCodeMap;
        }

        private void Execute(object paremeter)
        {
            ThreadParameter p = paremeter as ThreadParameter;
            DocumentHelper dh = new DocumentHelper();
            ModuleHelper mh = new ModuleHelper();

            try
            {
                if (p.IsSaved)
                {
                    if (p.isFinished)
                    {
                        //if (p.stadiumID != Guid.Empty)
                        //{
                        //    ExcuteCommand("UPDATE dbo.sys_stadium SET F_IsDone=1 WHERE ID='" + p.stadiumID + "'");
                        //}
                        //else if (p.DataID != Guid.Empty)
                        //{
                        //    ExcuteCommand("UPDATE dbo.sys_stadium SET F_IsDone=1 WHERE DataID='" + p.DataID + "'");
                        //}
                        try
                        {
                            #region 上传试验文档到工管中心
                            Sys_Module module = mh.GetModuleBaseInfoByID(p.ModuleID);
                            if (!string.IsNullOrEmpty(module.ModuleALTGG) && ConfigurationManager.AppSettings["GGCStartUpload"] == "1")
                            {
                                JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(dh.GetDocumentByID(p.DataID));
                                GGCUploadHelper ggcUH = new GGCUploadHelper();
                                ggcUH.UploadLabDocBasic(doc, module, p.userName, p.testRoomCode, p.machineBH);
                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            logger.Error("" + p.LineID + "上传试验文档到工管中心 error:" + ex.ToString());
                        }
                    }

                    //自动生成Excel，图片和报告页的pdf
                    //SourceHelper sourceHelper = new SourceHelper();
                    //sourceHelper.CreateRalationFiles(p.fpSpread, doc.ID, p.module.ReportIndex);
                    if (!String.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["UploadInfoCenter"]))
                    {
                        JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(dh.GetDocumentByID(p.DataID));
                        List<JZTestCell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZTestCell>>(p.TestData);
                        if (System.Configuration.ConfigurationManager.AppSettings["UploadInfoCenter"].Trim() == "1")
                        {
                            logger.Error("" + p.LineID + "上传UpdateToInformationCenter返回：" + new UpdateToInformationCenter().PostDataToTDB(doc, p.ModuleID, p.stadiumID, p.wtbh, p.testRoomCode, p.SeriaNumber, p.userName, cells, p.realTimeData, p.machineBH, p.UploadInfo, p.UploadCode));
                        }
                        if (System.Configuration.ConfigurationManager.AppSettings["UploadEngineeringManagementCenter"].Trim() == "1")
                        {
                            logger.Error("" + p.LineID + "上传UpdateToEngineeringManagementCenter返回：" + new UpdateToEngineeringManagementCenter().UpdateToEMC(doc, p.ModuleID, p.stadiumID, p.wtbh, p.testRoomCode, p.SeriaNumber, p.userName, cells, p.realTimeData, p.machineBH));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("" + p.LineID + "上传数据 TestDataID:" + p.TestDataID + " error：" + ex.ToString());
            }
        }

        private object getInstance(string AssemblyName, string TypeName)
        {
            try
            {
                object ins = null;
                if (File.Exists(AssemblyName))
                {
                    Assembly a = Assembly.LoadFrom(AssemblyName);
                    ins = a.CreateInstance(TypeName);
                }

                return ins;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    throw ex.InnerException;
                }
                else
                {
                    throw ex;
                }
            }
        }

        public List<FarPoint.CalcEngine.FunctionInfo> GetFunctionInfos()
        {
            List<FarPoint.CalcEngine.FunctionInfo> FunctionItems = new List<FarPoint.CalcEngine.FunctionInfo>();
            String sql = "select * from sys_biz_FunctionInfos where Scdel=0 ";

            DataTable Data = GetDataTable(sql);
            if (Data != null)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    try
                    {
                        String PathName = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~"),
                            "bin/" + Row["AssemblyName"].ToString());//E:\JZTPS\RefrenceCenter  System.Web.Hosting.HostingEnvironment.MapPath("~")
                        FarPoint.CalcEngine.FunctionInfo Info = getInstance(PathName, Row["FullClassName"].ToString())
                            as FarPoint.CalcEngine.FunctionInfo;
                        if (Info != null)
                            FunctionItems.Add(Info);
                    }
                    catch
                    {
                    }
                }
            }
            return FunctionItems;
        }

        public List<FarPoint.CalcEngine.FunctionInfo> GetFunctionInfosNew()
        {
            List<FarPoint.CalcEngine.FunctionInfo> FunctionItems = new List<FarPoint.CalcEngine.FunctionInfo>();
            String sql = "select * from sys_biz_FunctionInfos where Scdel=0 ";

            DataTable Data = GetDataTable(sql);
            if (Data != null)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    try
                    {
                        String PathName = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory,
                            Row["AssemblyName"].ToString());
                        FarPoint.CalcEngine.FunctionInfo Info = getInstance(PathName, Row["FullClassName"].ToString())
                            as FarPoint.CalcEngine.FunctionInfo;
                        if (Info != null)
                            FunctionItems.Add(Info);
                    }
                    catch
                    {
                    }
                }
            }
            return FunctionItems;
        }

        private SheetView GetSheetViewByID(Guid sheetID, FpSpread fpSpread)
        {
            foreach (SheetView view in fpSpread.Sheets)
            {
                if (new Guid(view.Tag.ToString()) == sheetID)
                {
                    return view;
                }
            }
            return null;
        }

        private Dictionary<Guid, DefaultSheetDataModel> GenerateSheetBoard(JZDocument doc)
        {
            Dictionary<Guid, DefaultSheetDataModel> models = new Dictionary<Guid, DefaultSheetDataModel>();
            List<FarPoint.CalcEngine.FunctionInfo> Infos = GetFunctionInfos();
            ModuleHelper mh = new ModuleHelper();
            string strSheeIDs = string.Empty;
            List<string> lstIDs = new List<string>();
            foreach (var item in doc.Sheets)
            {
                lstIDs.Add(item.ID.ToString());
            }
            strSheeIDs = "'" + string.Join("','", lstIDs.ToArray()) + "'";
            using (DataTable dt = mh.GetSheetSFormulasByIDs(strSheeIDs))// CallLocalService("Yqun.BO.BusinessManager.dll", "GetLineFormulaByModuleIndex", new object[] { strSheeIDs }) as  DataTable
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        DefaultSheetDataModel model = new DefaultSheetDataModel(100, 60);
                        model.Name = row["Name"].ToString();

                        List<JZFormulaData> formulas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZFormulaData>>(JZCommonHelper.GZipDecompressString(row["Formulas"].ToString()));
                        foreach (FarPoint.CalcEngine.FunctionInfo Info in Infos)
                        {
                            model.AddCustomFunction(Info);
                        }
                        if (formulas != null)
                        {
                            foreach (var formula in formulas)
                            {
                                model.SetFormula(formula.RowIndex, formula.ColumnIndex, formula.Formula);
                            }
                        }
                        models.Add(new Guid(row["ID"].ToString()), model);
                    }
                }
            }
            return models;
        }

        public void GenerateDoc(JZDocument document, Guid moduleIndex)
        {
            FpSpread fpSpread = new FpSpread();
            try
            {
                ModuleHelper mh = new ModuleHelper();

                List<JZFormulaData> CrossSheetLineFormulaInfos = mh.GetLineFormulaByModuleIndex(moduleIndex);
                Dictionary<Guid, SheetView> SheetCollection = new Dictionary<Guid, SheetView>();
                List<FarPoint.CalcEngine.FunctionInfo> Infos = GetFunctionInfos();

                foreach (JZSheet sheet in document.Sheets)
                {
                    String sheetXML = JZCommonHelper.GZipDecompressString(mh.GetSheetXMLByID(sheet.ID));
                    SheetView SheetView = Serializer.LoadObjectXml(typeof(SheetView), sheetXML, "SheetView") as SheetView;
                    SheetView.Tag = sheet.ID;
                    SheetView.Cells[0, 0].Value = "";
                    SheetView.Protect = true;

                    fpSpread.Sheets.Add(SheetView);

                    SheetCollection.Add(sheet.ID, SheetView);

                    foreach (FarPoint.CalcEngine.FunctionInfo Info in Infos)
                    {
                        SheetView.AddCustomFunction(Info);
                    }
                    foreach (JZCell dataCell in sheet.Cells)
                    {
                        Cell cell = SheetView.Cells[dataCell.Name];

                        if (cell != null)
                        {
                            cell.Value = dataCell.Value;
                        }
                    }
                }
                fpSpread.LoadFormulas(true);
                foreach (JZFormulaData formula in CrossSheetLineFormulaInfos)
                {
                    if (SheetCollection.ContainsKey(formula.SheetIndex))
                    {
                        SheetView Sheet = SheetCollection[formula.SheetIndex];
                        Cell cell = Sheet.Cells[formula.RowIndex, formula.ColumnIndex];
                        if (cell != null)
                        {
                            try
                            {
                                if (formula.Formula.ToUpper().Trim() == "NA()")
                                {
                                    cell.Formula = "";
                                }
                                else
                                {
                                    cell.Formula = formula.Formula;
                                }
                            }
                            catch (Exception ex)
                            {
                                // logger.Error("GenerateDoc" + ex.Message);
                            }
                        }
                    }
                }
                fpSpread.LoadFormulas(true);

                foreach (JZSheet sheet in document.Sheets)
                {
                    SheetView view = GetSheetViewByID(sheet.ID, fpSpread);
                    if (view == null)
                    {
                        continue;
                    }
                    foreach (JZCell dataCell in sheet.Cells)
                    {
                        Cell cell = view.Cells[dataCell.Name];
                        if (cell != null)
                        {
                            if (String.IsNullOrEmpty(cell.Formula))
                            {
                                continue;
                            }
                            IGetFieldType FieldTypeGetter = cell.CellType as IGetFieldType;
                            if (FieldTypeGetter != null && FieldTypeGetter.FieldType.Description == "图片")
                            {
                                continue;
                            }
                            else if (FieldTypeGetter != null && FieldTypeGetter.FieldType.Description == "数字")
                            {
                                if (cell.Value != null)
                                {
                                    Decimal d;
                                    if (Decimal.TryParse(cell.Value.ToString().Trim(' ', '\r', '\n'), out d))
                                    {
                                        dataCell.Value = d;
                                    }
                                    else
                                    {
                                        dataCell.Value = null;
                                    }
                                }
                            }
                            else
                            {
                                dataCell.Value = cell.Value;
                            }
                            if (dataCell.Value != null && dataCell.Value is String)
                            {
                                dataCell.Value = dataCell.Value.ToString().Trim(' ', '\r', '\n');
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Error("采集构造Farpoint组件错误：" + ex.Message);
            }
            //foreach (SheetView item in fpSpread.Sheets)
            //{
            //    item.Dispose();
            //}
            fpSpread.Dispose();
            fpSpread = null;
            //GC.Collect();
            //return fpSpread;
        }
        private void GenerateDocNew(JZDocument document, Guid moduleIndex)
        {
            try
            {
                ModuleHelper mh = new ModuleHelper();
                List<JZFormulaData> CrossSheetLineFormulaInfos = mh.GetLineFormulaByModuleIndex(moduleIndex);
                Dictionary<Guid, DefaultSheetDataModel> models = GenerateSheetBoard(document);
                ReferenceManager refManger = new ReferenceManager(models);

                foreach (JZFormulaData formula in CrossSheetLineFormulaInfos)
                {
                    // Debug.WriteLine(models[formula.SheetIndex].Name + "\t" + SheetHelper.GetCellName(formula.RowIndex, formula.ColumnIndex) + "\t" + formula.Formula);
                    //logger.Error("formula:" + formula.Formula);
                    formula.Formula = formula.Formula.Replace("'", "");
                    refManger.AddCrossLineFormula(formula);
                }

                #region put data change delegate for each model
                foreach (var model in models.Values)
                {
                    model.Changed += (object sender, SheetDataModelEventArgs e) =>
                    {
                        DefaultSheetDataModel dataModel = sender as DefaultSheetDataModel;
                        string cellFullName = dataModel.Name + "!" + SheetHelper.GetCellName(e.Row, e.Column);
                        refManger.UpdateCellValue(cellFullName, dataModel.GetValue(e.Row, e.Column));
                        //Debug.WriteLine(string.Format("{0}-cell[{1},{2}]=={3}", dataModel.Name, e.Row, e.Column, dataModel.GetValue(e.Row, e.Column)));
                    };
                }
                #endregion

                #region fill data
                foreach (var sheet in document.Sheets)
                {
                    if (models.ContainsKey(sheet.ID))
                    {
                        DefaultSheetDataModel model = models[sheet.ID];

                        foreach (var cell in sheet.Cells)
                        {
                            if (cell.Value != null && cell.Value.ToString() != "/")
                            {
                                Point pt = SheetHelper.GetCellPosition(cell.Name);
                                if (string.IsNullOrEmpty(model.GetFormula(pt.Y, pt.X)))
                                {
                                    //Debug.WriteLine("fill data -- " + model.Name + "--" + cell.Name + " : " + cell.Value.ToString());
                                    model.SetValue(pt.Y, pt.X, cell.Value);
                                }
                            }
                        }
                    }
                }
                #endregion


                #region extract data
                foreach (JZSheet sheet in document.Sheets)
                {
                    if (models.ContainsKey(sheet.ID))
                    {
                        DefaultSheetDataModel model = models[sheet.ID];

                        foreach (JZCell dataCell in sheet.Cells)
                        {
                            Point pt = SheetHelper.GetCellPosition(dataCell.Name);
                            object value = model.GetValue(pt.Y, pt.X);
                            if (value != null)
                            {
                                if (String.IsNullOrEmpty(model.GetFormula(pt.Y, pt.X)))
                                {
                                    continue;
                                }

                                dataCell.Value = model.GetValue(pt.Y, pt.X);

                                if (dataCell.Value != null && dataCell.Value is String)
                                {
                                    dataCell.Value = dataCell.Value.ToString().Trim(' ', '\r', '\n');
                                }

                            }
                        }
                    }
                }
                #endregion

            }
            catch (Exception ex)
            {
                logger.Error(string.Format("GenerateDocNew DocID:{0} error：" + ex.Message, document.ID));
                throw;
            }
        }
        private class ThreadParameter
        {
            public Guid DataID;
            public String TestData;
            public Guid TestDataID;
            public Int32 SeriaNumber;
            public Boolean IsSaved;
            //public Sys_Module module;
            public Guid stadiumID;
            public String wtbh;
            public String testRoomCode;
            public String userName;
            public String realTimeData;
            public string machineBH;
            public Boolean isFinished;
            public string UploadInfo;
            public string UploadCode;
            //public FpSpread fpSpread;
            public Guid ModuleID;
            public string LineID;

        }

        /// <summary>
        /// 获取模板库配置模板信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetTestConfigList()
        {
            String sql = @"SELECT a.ID, a.ModuleID,a.SerialNumber,a.Config,b.Name AS ModuleName, b.DeviceType 
                FROM sys_module_config a
                JOIN dbo.sys_module b ON a.ModuleID = b.ID Where a.IsActive=1 Order by a.ModuleID, a.SerialNumber";

            return GetDataTable(sql);

        }

        /// <summary>
        /// 根据用户模板列表获取模板配置信息
        /// </summary>
        /// <param name="testCode"></param>
        /// <returns></returns>
        public DataTable GetTestConfigList(string testCode)
        {
            String sql = @"SELECT b.ID,b.ModuleID,b.SerialNumber,b.Config,c.Name AS ModuleName,c.DeviceType FROM dbo.sys_engs_Tree a JOIN sys_module_config b ON a.RalationID =b.ModuleID JOIN dbo.sys_module c ON a.RalationID =c.ID WHERE a.NodeType='@module' AND a.NodeCode LIKE '" + testCode + "%' AND LEN(a.NodeCode)>16  AND a.Scdel=0 ORDER BY b.ModuleID, b.SerialNumber";

            return GetDataTable(sql);

        }

        /// <summary>
        /// 获取工管中心设别编码
        /// </summary>
        /// <param name="ECode"></param>
        /// <returns></returns>
        public string GetECode(string ECode)
        {
            DataTable _dt = GetDataTable("select RemoteCode1 from dbo.sys_devices where  MachineCode='" + ECode + "'");
            if (_dt != null && _dt.Rows.Count > 0)
            {
                return _dt.Rows[0][0].ToString();
            }
            return ECode;
        }

        /// <summary>
        /// 获取铁道部信息中心设备编码
        /// </summary>
        /// <param name="ECode"></param>
        /// <returns></returns>
        public string GetInfoCode(string ECode)
        {
            DataTable _dt = GetDataTable("select RemoteCode2  from dbo.sys_devices where  MachineCode='" + ECode + "'");
            if (_dt != null && _dt.Rows.Count > 0)
            {
                return _dt.Rows[0][0].ToString();
            }
            return ECode;
        }

        public DataTable GetTestDataByDataID(Guid dataID)
        {
            String sql = "select ID, SerialNumber, TestData  from sys_test_data where status=1 and DataID='" +
                dataID + "' order by SerialNumber";
            return GetDataTable(sql);
        }

        public String GetRealTimeDataByID(Guid ID)
        {
            String sql = "select RealTimeData  from sys_test_data where ID='" + ID + "' ";
            DataTable dt = GetDataTable(sql);
            if (dt == null || dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }

        public void SaveCaiJiLog(String testRoomCode, String machineCode, String userName, String errorMsg)
        {
            String sql = String.Format(@"INSERT INTO dbo.sys_caiji_log
                            ( TestRoomCode ,
                              MachineCode ,
                              UserName ,
                              ErrMsg 
                            )
                    VALUES  ( '{0}' ,
                              '{1}' ,
                              '{2}' ,
                              '{3}' )", testRoomCode, machineCode, userName,
                 errorMsg.Replace("'", "''"));
            ExcuteCommand(sql);
        }

        public void SaveTestOverTime(Guid testID, Guid documentID, Guid moduleID, Guid stadiumID, String wtbh, String testRoomCode,
                Int32 seriaNumber, String userName, String testData, String realTimeData, Int32 totalNumber, String machineCode, String UploadInfo, string UploadCode)
        {
            if (IsExistTestOverTime(testID) == false)
            {
                String sql = String.Format(@"INSERT INTO dbo.sys_test_overtime
                                                        ( ID ,
                                                          CaiJiID ,
                                                          DataID ,
                                                          StadiumID ,
                                                          ModuleID ,
                                                          WTBH ,
                                                          TestRoomCode ,
                                                          SerialNumber ,
                                                          UserName ,
                                                          CreatedTime ,
                                                          TestData ,
                                                          RealTimeData ,
                                                          MachineCode ,
                                                          UploadInfo ,
                                                          UploadCode ,
                                                          UploadTDB ,
                                                          UploadEMC ,
                                                          TotallNumber 
                                                        )
                                                VALUES  ( NEWID(),
                                                          '{0}',
                                                          '{1}',
                                                          '{2}',
                                                          '{3}',
                                                          '{4}',
                                                          '{5}',
                                                          '{6}',
                                                          '{7}',
                                                          GETDATE(),
                                                          '{8}',
                                                          '{9}',
                                                          '{10}',
                                                          '{11}',
                                                          '{12}',
                                                          0 , 
                                                          0 , 
                                                          '{13}'
                                                        )",
                    testID, documentID, stadiumID, moduleID, wtbh, testRoomCode, seriaNumber,
                    userName, testData, realTimeData, machineCode,
                    UploadInfo, UploadCode, totalNumber);

                ExcuteCommand(sql);
            }
            else
            {
                logger.Error("SaveTestOverTime exists CaiJiID:" + testID);
            }
        }

        private bool IsExistTestOverTime(Guid CaiJiID)
        {

            String sql = "select ID  from sys_test_overtime where CaiJiID='" + CaiJiID + "' ";
            DataTable dt = GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #region 指纹注册验证

        /// <summary>
        /// 指纹注册
        /// </summary>
        /// <param name="_BioKeyInfo"></param>
        /// <returns></returns>
        public bool KeyReginUser(BioKeyInfo _BioKeyInfo)
        {
            DataTable _dt = GetDataTable("select * from sys_BioKeyInfo");

            if (_dt != null)
            {
                DataRow Row = _dt.NewRow();
                Row["ID"] = _BioKeyInfo.RegisterID;
                Row["RegisterName"] = _BioKeyInfo.RegisterName;
                Row["Template"] = _BioKeyInfo.Template;
                Row["UserCode"] = _BioKeyInfo.UserCode;
                Row["RalationID"] = _BioKeyInfo.RalationID;

                _dt.Rows.Add(Row);
            }

            if (Update(_dt) > 0)
            {
                return true;
            }
            return false;
        }

        public List<BioKeyInfo> GetTemplates(String UserCode)
        {
            List<BioKeyInfo> BioKeyInfos = new List<BioKeyInfo>();
            String RalationID = GetCompanyID(UserCode);
            if (RalationID != string.Empty)
            {
                StringBuilder sql_select = new StringBuilder();
                sql_select.Append("select * from sys_BioKeyInfo where RalationID =('");
                sql_select.Append(RalationID);
                sql_select.Append("')");
                DataTable dt = GetDataTable(sql_select.ToString());
                if (dt != null)
                {
                    foreach (DataRow Row in dt.Rows)
                    {
                        BioKeyInfo BioKeyInfo = new BioKeyInfo();
                        BioKeyInfo.RegisterID = Row["ID"].ToString();
                        BioKeyInfo.RegisterName = Row["RegisterName"].ToString();
                        BioKeyInfo.Template = Row["Template"];
                        BioKeyInfos.Add(BioKeyInfo);
                    }
                }
            }
            return BioKeyInfos;
        }

        public String RemoteGetCompanyID(String UserCode)
        {
            String CompanyID = string.Empty;
            StringBuilder Sql_ID = new StringBuilder();
            Sql_ID.Append("Select RalationID from sys_engs_Tree where nodecode = '");
            Sql_ID.Append(UserCode.Substring(0, 12));
            Sql_ID.Append("'");
            DataTable Dt = GetDataTable(Sql_ID.ToString());
            if (Dt != null && Dt.Rows.Count > 0)
            {
                CompanyID = Dt.Rows[0]["RalationID"].ToString();
            }

            Sql_ID = new StringBuilder();
            Sql_ID.Append("select id from sys_engs_CompanyInfo where ConstructionCompany like '%");
            Sql_ID.Append(CompanyID);
            Sql_ID.Append("%' and description != '北京铁科检测中心'");
            Dt = GetDataTable(Sql_ID.ToString());

            if (Dt != null && Dt.Rows.Count > 0)
            {
                CompanyID = Dt.Rows[0]["id"].ToString();
            }

            return CompanyID;
        }

        public String GetCompanyID(String UserCode)
        {
            String CompanyID = string.Empty;

            CompanyID = RemoteGetCompanyID(UserCode);

            return CompanyID;
        }

        #endregion

        /// <summary>
        /// 是否成功
        /// </summary>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        public bool IsExist(string UserCode)
        {
            DataTable _dt = GetDataTable("select * from sys_BioKeyInfo where UserCode='" + UserCode + "'");

            if (_dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 加载历史数据，返回强度等级、事件尺寸
        /// </summary>
        /// <param name="wtbh"></param>
        /// <param name="modelid"></param>
        /// <param name="testroom"></param>
        /// <returns></returns>
        public DataTable GetTestInfo(string DataID)
        {
            DataTable dt = null;
            try
            {

                dt = GetDataTable("select F_SJSize,F_Added from dbo.sys_stadium where DataID='" + DataID + "'");

            }
            catch (Exception ex)
            {
                logger.Error("GetTestInfo" + ex.Message);
            }
            return dt;
        }

        public String GetRealTimeTestData(Guid id)
        {
            String str = "";
            try
            {

                DataTable dt = GetDataTable("SELECT RealTimeData FROM dbo.sys_test_data WHERE ID='" + id + "'");
                if (dt != null && dt.Rows.Count >= 1)
                {
                    str = dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                logger.Error("GetRealTimeTestData" + ex.Message);
            }
            return str;
        }

        private void LockDocument(Guid dataID)
        {
            if (dataID != Guid.Empty)
            {
                String sql = "UPDATE dbo.sys_document SET IsLock=1 WHERE ID='" + dataID + "'";
                ExcuteCommand(sql);
            }
        }

        private void UnLockDocument(Guid dataID)
        {
            if (dataID != Guid.Empty)
            {
                String sql = "UPDATE dbo.sys_document SET IsLock=0 WHERE ID='" + dataID + "'";
                ExcuteCommand(sql);
            }
        }

        private bool GetAndSetLockStatue(Guid id)
        {
            var sql =
                 " SELECT @locked = IsLock FROM dbo.sys_document WHERE ID = @ID" +
                 " IF @locked = 0" +
                 " BEGIN" +
                 " UPDATE dbo.sys_document SET IsLock = 1 WHERE ID = @ID" +
                 " END" +
                 " RETURN";

            if (GetConntion() is SqlConnection)
            {
                SqlConnection connection = null;
                SqlTransaction transaction = null;
                SqlCommand command = null;

                try
                {
                    connection = new SqlConnection(GetConnString());
                    connection.Open();

                    transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted, "DocumentLockstatsTransaction");
                    command = new SqlCommand(sql, connection);

                    command.Parameters.Add(new SqlParameter("@ID", id.ToString()));
                    command.Parameters.Add("@locked", SqlDbType.Int);
                    command.Parameters["@locked"].Direction = ParameterDirection.Output;
                    command.Transaction = transaction;
                    command.ExecuteScalar();
                    transaction.Commit();

                    var locked = command.Parameters["@locked"].Value.ToString();

                    if (locked == "0")
                    {
                        // return unlock statue
                        return false;
                    }

                    // return lock statue
                    return true;
                }
                catch (Exception e)
                {
                    logger.Error("在查询文档锁定状态时发生异常", e);

                    if (transaction != null)
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception transactionException)
                        {
                            logger.Error("在事物回滚时发生了异常", transactionException);
                        }
                    }
                }
                finally
                {
                    if (connection != null)
                    {
                        try
                        {
                            connection.Close();
                        }
                        catch (Exception ce)
                        {
                            logger.Error("在关闭数据库连接时发生异常", ce);
                        }
                    }
                }
            }
            else
            {
                if (IsDocumentLocked(id))
                {
                    return true;
                }
                else
                {
                    LockDocument(id);
                    return false;
                }
            }

            logger.Info("没有设置文档的锁定状态，默认返回了未锁定状态");
            return false;
        }

        private bool IsDocumentLocked(Guid dataID)
        {
            Boolean flag = false;
            String sql = "SELECT ID FROM  dbo.sys_document WHERE ID='" + dataID + "' AND IsLock=1";
            DataTable dt = GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                flag = true;
            }
            return flag;
        }

        private void ContinueProcess(Guid dataID)
        {
            String sql = "SELECT TOP 1 * FROM dbo.sys_test_data WHERE DataID='" +
                dataID + "' AND Status=2 ORDER BY SerialNumber";
            DataTable dt = GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (GetAndSetLockStatue(dataID) == false)
                {
                    try
                    {
                        Guid id = new Guid(dt.Rows[0]["ID"].ToString());
                        Guid documentID = new Guid(dt.Rows[0]["DataID"].ToString());
                        Guid ModuleID = new Guid(dt.Rows[0]["ModuleID"].ToString());
                        Guid StadiumID = new Guid(dt.Rows[0]["StadiumID"].ToString());
                        String wtbh = dt.Rows[0]["WTBH"].ToString();
                        String TestRoomCode = dt.Rows[0]["TestRoomCode"].ToString();
                        Int32 SerialNumber = Int32.Parse(dt.Rows[0]["SerialNumber"].ToString());
                        String UserName = dt.Rows[0]["UserName"].ToString();
                        String TestData = dt.Rows[0]["TestData"].ToString();
                        String RealTimeData = dt.Rows[0]["RealTimeData"].ToString();
                        Int32 TotallNumber = Int32.Parse(dt.Rows[0]["TotallNumber"].ToString());
                        String MachineCode = dt.Rows[0]["MachineCode"].ToString();
                        String UploadInfo = dt.Rows[0]["UploadInfo"].ToString();
                        String UploadCode = dt.Rows[0]["UploadCode"].ToString();
                        ApplyUploadData(id, documentID, ModuleID, StadiumID, wtbh, TestRoomCode, SerialNumber, UserName,
                            TestData, RealTimeData, TotallNumber, MachineCode, UploadInfo, UploadCode);
                    }
                    catch (Exception ex)
                    {
                        logger.Error("ContinueProcess error: " + ex.Message + "; DataID=" + dataID);
                    }
                    finally
                    {
                        UnLockDocument(dataID);
                    }
                }
                else
                {
                    Thread.Sleep(1000);
                    logger.Info("正在等待解锁后更新资料：DataID=" + dataID);
                    ContinueProcess(dataID);
                }
            }
        }

        /// <summary>
        /// 上传客户端配置信息
        /// </summary>
        /// <param name="TestCode">实验室编码</param>
        /// <param name="ConfigStr">配置字符串</param>
        /// <returns>True：成功；False：失败</returns>
        public bool UploadClientConfig(string TestCode, string MachineCode, string ConfigStr)
        {
            bool IsSucceed = false;
            try
            {
                string SqlStr = "INSERT INTO dbo.sys_devices (ID,TestRoomCode, MachineCode,ClientConfig,ConfigStatus,ConfigUpdateTime) VALUES ('{0}','{1}','{2}','{3}',{4},{5})";
                SqlStr = string.Format(SqlStr, Guid.NewGuid(), TestCode, MachineCode, ConfigStr, 1, "getdate()");

                ExcuteCommand(SqlStr);
                IsSucceed = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + System.Environment.NewLine + ex.StackTrace);
                IsSucceed = false;
            }
            return IsSucceed;
        }

        /// <summary>
        /// 添加配置信息
        /// </summary>
        /// <param name="TestCode">实验室编码</param>
        /// <param name="MachineCode">机器 编码</param>
        /// <param name="ConfigStr">配置字符串</param>
        /// <param name="MachineType">设备类型：1、压力机；2、万能机</param>
        /// <param name="SoftType">软件类型：0、数显；1、电液伺服</param>
        /// <returns>True陈功；False失败</returns>
        public bool UploadClientConfigAndType(string TestCode, string MachineCode, string ConfigStr, string MachineType, string SoftType)
        {
            bool IsSucceed = false;
            try
            {
                string SqlStr = "INSERT INTO dbo.sys_devices (ID,TestRoomCode, MachineCode,ClientConfig,ConfigStatus,ConfigUpdateTime,DeviceType,IsDYSF) VALUES ('{0}','{1}','{2}','{3}',{4},{5},{6},{7})";
                SqlStr = string.Format(SqlStr, Guid.NewGuid(), TestCode, MachineCode, ConfigStr, 1, "getdate()", MachineType, SoftType);

                ExcuteCommand(SqlStr);
                IsSucceed = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + System.Environment.NewLine + ex.StackTrace);
                IsSucceed = false;
            }
            return IsSucceed;
        }

        /// <summary>
        /// 更新客户端配置信息
        /// </summary>
        /// <param name="TestCode">实验室编码</param>
        /// <param name="ConfigStr">配置字符串</param>
        /// <returns>True：成功；False：失败</returns>
        public bool UpdataClientConfig(string MachineCode, string ConfigStr)
        {
            bool IsSucceed = false;
            try
            {
                string SqlStr = "UPDATE dbo.sys_devices SET ClientConfig='{0}',ConfigUpdateTime=GETDATE(),ConfigStatus=1 WHERE MachineCode='{1}'";
                SqlStr = string.Format(SqlStr, ConfigStr, MachineCode);

                ExcuteCommand(SqlStr);
                IsSucceed = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + System.Environment.NewLine + ex.StackTrace);
                IsSucceed = false;
            }
            return IsSucceed;
        }
        /// <summary>
        /// 更新服务上客户端信息
        /// </summary>
        /// <param name="MachineCode">设备编码</param>
        /// <param name="ConfigStr">配置字符串</param>
        /// <param name="MachineType">设备类型：1、压力机；2、万能机</param>
        /// <param name="SoftType">软件类型：0、数显；1、电液伺服</param>
        /// <returns></returns>
        public bool UpdataClientConfigAndType(string MachineCode, string ConfigStr, string MachineType, string SoftType)
        {
            bool IsSucceed = false;
            try
            {
                string SqlStr = "UPDATE dbo.sys_devices SET ClientConfig='{0}',ConfigUpdateTime=GETDATE(),ConfigStatus=1,DeviceType={1},IsDYSF={2} WHERE MachineCode='{3}'";
                SqlStr = string.Format(SqlStr, ConfigStr, MachineType, SoftType, MachineCode);

                ExcuteCommand(SqlStr);
                IsSucceed = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + System.Environment.NewLine + ex.StackTrace);
                IsSucceed = false;
            }
            return IsSucceed;
        }

        /// <summary>
        ///  设置客户端状态为主
        /// </summary>
        /// <param name="TestCode"></param>
        /// <returns></returns>
        public bool UpdateClientConfigStatus(string MachineCode)
        {
            bool IsSucceed = false;
            try
            {
                string SqlStr = "UPDATE dbo.sys_devices SET ConfigUpdateTime=GETDATE(),ConfigStatus=1 WHERE MachineCode='{0}'";
                SqlStr = string.Format(SqlStr, MachineCode);

                ExcuteCommand(SqlStr);
                IsSucceed = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + System.Environment.NewLine + ex.StackTrace);
                IsSucceed = false;
            }
            return IsSucceed;
        }

        /// <summary>
        /// 获配置信息
        /// </summary>
        /// <param name="TestCode">实验室编码</param>
        /// <returns>客户端配置信息</returns>
        public string GetClientConfig(string MachineCode)
        {
            try
            {
                string SqlStr = "SELECT ClientConfig FROM  dbo.sys_devices WHERE MachineCode='" + MachineCode + "'";
                DataTable dt = GetDataTable(SqlStr);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取实验室配置状态
        /// </summary>
        /// <param name="TestCode">实验室编码</param>
        /// <returns>
        /// 0：配置信息不存在
        /// 1：客户端为主，客户端更新服务器
        /// 2：服务器为主，服务器更新客户端
        /// </returns>
        public int GetClientConfigStatus(string MachineCode)
        {
            int ClientCinfigStatus = 0;
            try
            {
                string SqlStr = "SELECT ConfigStatus FROM  dbo.sys_devices WHERE MachineCode='" + MachineCode + "'";
                DataTable dt = GetDataTable(SqlStr);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ClientCinfigStatus = Convert.ToInt32(dt.Rows[0][0].ToString());
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
            return ClientCinfigStatus;
        }


        public Sys_TestData GetTestDataModel(Guid TestDataID)
        {
            BizCommon.Sys_TestData model = new BizCommon.Sys_TestData();
            String sql = "select ID,DataID,StadiumID,ModuleID,WTBH,TestRoomCode,SerialNumber,UserName,CreatedTime,TestData,RealTimeData,MachineCode,Status,UploadInfo,UploadCode,UploadTDB,UploadEMC,TotallNumber from sys_test_data where ID='" + TestDataID + "'";
            using (DataTable dt = GetDataTable(sql))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    if (row != null)
                    {
                        if (row["ID"] != null && row["ID"].ToString() != "")
                        {
                            model.ID = new Guid(row["ID"].ToString());
                        }
                        if (row["DataID"] != null && row["DataID"].ToString() != "")
                        {
                            model.DataID = new Guid(row["DataID"].ToString());
                        }
                        if (row["StadiumID"] != null && row["StadiumID"].ToString() != "")
                        {
                            model.StadiumID = new Guid(row["StadiumID"].ToString());
                        }
                        if (row["ModuleID"] != null && row["ModuleID"].ToString() != "")
                        {
                            model.ModuleID = new Guid(row["ModuleID"].ToString());
                        }
                        if (row["WTBH"] != null)
                        {
                            model.WTBH = row["WTBH"].ToString();
                        }
                        if (row["TestRoomCode"] != null)
                        {
                            model.TestRoomCode = row["TestRoomCode"].ToString();
                        }
                        if (row["SerialNumber"] != null && row["SerialNumber"].ToString() != "")
                        {
                            model.SerialNumber = int.Parse(row["SerialNumber"].ToString());
                        }
                        if (row["UserName"] != null)
                        {
                            model.UserName = row["UserName"].ToString();
                        }
                        if (row["CreatedTime"] != null && row["CreatedTime"].ToString() != "")
                        {
                            model.CreatedTime = DateTime.Parse(row["CreatedTime"].ToString());
                        }
                        if (row["TestData"] != null)
                        {
                            model.TestData = row["TestData"].ToString();
                        }
                        if (row["RealTimeData"] != null)
                        {
                            model.RealTimeData = row["RealTimeData"].ToString();
                        }
                        if (row["MachineCode"] != null)
                        {
                            model.MachineCode = row["MachineCode"].ToString();
                        }
                        if (row["Status"] != null && row["Status"].ToString() != "")
                        {
                            model.Status = int.Parse(row["Status"].ToString());
                        }
                        if (row["UploadInfo"] != null)
                        {
                            model.UploadInfo = row["UploadInfo"].ToString();
                        }
                        if (row["UploadCode"] != null)
                        {
                            model.UploadCode = row["UploadCode"].ToString();
                        }
                        if (row["UploadTDB"] != null && row["UploadTDB"].ToString() != "")
                        {
                            model.UploadTDB = int.Parse(row["UploadTDB"].ToString());
                        }
                        if (row["UploadEMC"] != null && row["UploadEMC"].ToString() != "")
                        {
                            model.UploadEMC = int.Parse(row["UploadEMC"].ToString());
                        }
                        if (row["TotallNumber"] != null && row["TotallNumber"].ToString() != "")
                        {
                            model.TotallNumber = int.Parse(row["TotallNumber"].ToString());
                        }
                    }
                    else
                    {
                        model = null;
                    }
                }
                else
                {
                    model = null;
                }
            }
            return model;
        }

        public bool UpdateTestDataStatus(Guid id, int Status)
        {

            string sql = "UPDATE dbo.sys_test_data SET Status=" + Status + " WHERE ID=@ID";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@ID", id));

            return ExcuteCommandsWithTransaction(new List<IDbCommand>() { cmd });
        }

        public void SyncSaveDoc(JZDocument doc, Guid moduleID)
        {
            try
            {
                logger.Error("SyncSaveDoc dataid:" + doc.ID + " start");
                DocumentHelper dh = new DocumentHelper();
                String json = Newtonsoft.Json.JsonConvert.SerializeObject(doc).Replace("'", "''");
                String sql = "UPDATE dbo.sys_document SET Data=@Data,NeedUpload=1,LastEditedTime=getdate(),IsLock=0 WHERE ID=@ID";
                SqlCommand cmd1 = new SqlCommand(sql);
                cmd1.Parameters.Add(new SqlParameter("@ID", doc.ID));
                cmd1.Parameters.Add(new SqlParameter("@Data", json));

                bool flag = ExcuteCommandsWithTransaction(new List<IDbCommand>() { cmd1 });
                ModuleHelper mh = new ModuleHelper();
                Sys_Module module = mh.GetModuleBaseInfoByID(moduleID);

                if (flag)
                {
                    if (dh.ApplyModuleSetting(doc, module.ModuleSettings))//给modulesetting定义字段赋值
                    {
                        //验证是否合格，并发短信
                        QualifyHelper qh = new QualifyHelper();
                        qh.Qualify(doc, module);
                    }
                }
                else
                {
                    logger.Error("ExcuteCommandsWithTransaction 事物返回false，SyncSaveDoc");
                }
                logger.Error("SyncSaveDoc moduleID:" + moduleID + " end");
            }
            catch (Exception ex)
            {
                logger.Error("SyncSaveDoc dataid:" + doc.ID + " error:" + ex.ToString());

            }
        }

        public int CaiJiCountByDataID(Guid DataID)
        {
            int iCount = 0;
            try
            {
                string strSQL = "SELECT Count(1) FROM dbo.sys_test_data WHERE DataID='" + DataID + "'";
                DataTable dt = GetDataTable(strSQL);
                if (dt != null && dt.Rows.Count > 0)
                {
                    iCount = Convert.ToInt32(dt.Rows[0][0].ToString());
                }
            }
            catch (Exception ex)
            {
                logger.Error("CaiJiCountByDataID DataID:" + DataID + " error:" + ex.Message);
            }
            return iCount;
        }

        /// <summary>
        /// 是否公管中心远程验证
        /// 配置文件“IsRemotCheck”，0不验证返回false；1验证返回true
        /// </summary>
        /// <returns></returns>
        public bool IsRemotGGZXCheck()
        {
            try
            {
                if (System.Configuration.ConfigurationManager.AppSettings["IsRemotCheck"].ToString() == "1")
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
                logger.Error(ex.StackTrace);
                return false;
            }
        }
    }


    public class ExternalReference
    {
        public string ReferencedFullName { get; set; }

        public int ColumnIndex { get; set; }

        public int RowIndex { get; set; }

        public Guid SheetID { get; set; }
    }

    public class ReferenceManager
    {
        public static readonly int FakedCellColumnIndex = 59;
        private Dictionary<Guid, DefaultSheetDataModel> models;
        private Dictionary<Guid, int> modelRowIndex = new Dictionary<Guid, int>();
        private Dictionary<string, List<ReferenceCell>> referenceCells = new Dictionary<string, List<ReferenceCell>>();
        private Dictionary<Guid, Dictionary<string, string>> internalCellMaps = new Dictionary<Guid, Dictionary<string, string>>();

        private List<string> GetReferenceCellName(string formula)
        {
            Dictionary<string, string> names = new Dictionary<string, string>();
            Match match = Regex.Match(formula, @"(?<!"")([\w（）()']+?)!([a-z]+\d+)(?!"")", RegexOptions.IgnoreCase);//@"(?<!"")(\w+?)!([a-z]+\d+)(?!"")"

            string[] arrReplace = new string[] { "IF(", "OR(", "JZSORT(", "'", "NUMMODIFY(", "YEAR(", "MONTH(", "DAY(" };
            string strSheetAndCell = string.Empty;
            while (match.Success)
            {
                strSheetAndCell = match.Value.ToUpper();
                foreach (string str in arrReplace)
                {
                    strSheetAndCell = strSheetAndCell.Replace(str, "");
                }
                //if (match.Value.ToUpper().IndexOf("") >= 0 || match.Value.ToUpper().IndexOf("") >= 0 || match.Value.ToUpper().IndexOf("") >= 0)
                //{
                //    names[match.Value.ToUpper().Replace("IF(", "").Replace("'", "").Replace("OR(", "").Replace("JZSORT(", "")] = match.Value.ToUpper().Replace("IF(", "").Replace("'", "").Replace("OR(", "").Replace("JZSORT(", "");
                //}
                //else
                //{
                //}
                names[strSheetAndCell] = strSheetAndCell;
                match = match.NextMatch();
            }

            List<string> result = new List<string>();
            foreach (var kv in names)
            {
                result.Add(kv.Key);
            }
            return result;
        }

        public ReferenceManager(Dictionary<Guid, DefaultSheetDataModel> models)
        {
            this.models = models;
            foreach (var kv in models)
            {
                modelRowIndex[kv.Key] = 0;
            }
        }

        public void AddCrossLineFormula(JZFormulaData formula)
        {
            if (models.ContainsKey(formula.SheetIndex))
            {
                DefaultSheetDataModel model = models[formula.SheetIndex];
                if (formula.Formula.ToUpper().Trim() == "NA()")
                {
                    model.SetFormula(formula.RowIndex, formula.ColumnIndex, "");
                }
                else
                {
                    string newFormula = formula.Formula;
                    List<string> referenceCellsNames = GetReferenceCellName(formula.Formula);
                    if (referenceCellsNames.Count > 0)
                    {
                        foreach (var cellName in referenceCellsNames)
                        {
                            //determine if the cellname is an internal cell
                            Match match = Regex.Match(cellName, @"(?<!"")([\w（）()']+?)!([a-z]+\d+)(?!"")", RegexOptions.IgnoreCase);
                            string sheetName = match.Groups[1].Value;
                            string simpleCellName = match.Groups[2].Value;
                            if (model.Name == sheetName) //the current cells is an internal cell instead of an external cell
                            {
                                newFormula = newFormula.Replace(cellName, simpleCellName);
                                //Debug.WriteLine(cellName + " is an internal cell : " + simpleCellName);
                            }
                            else
                            {
                                string internalCellName = AddReferenceCell(cellName, formula.SheetIndex);
                                //Debug.WriteLine(internalCellName + " represents " + cellName); 
                                newFormula = newFormula.Replace(cellName, internalCellName);
                            }
                        }

                        //Debug.WriteLine(model.Name+"\t"+SheetHelper.GetCellName(formula.RowIndex,formula.ColumnIndex)+"\t"+newFormula);
                        model.SetFormula(formula.RowIndex, formula.ColumnIndex, newFormula);
                    }
                    else
                    {
                        model.SetFormula(formula.RowIndex, formula.ColumnIndex, formula.Formula);
                    }
                }
            }

        }

        public string AddReferenceCell(string sourceFullName, Guid referenceModelID)
        {
            string fakedCellName;
            Dictionary<string, string> modelInternalCellMap;
            if (internalCellMaps.ContainsKey(referenceModelID))
            {
                modelInternalCellMap = internalCellMaps[referenceModelID];
            }
            else
            {
                modelInternalCellMap = new Dictionary<string, string>();
            }

            if (!modelInternalCellMap.ContainsKey(sourceFullName))
            {
                int rowIndex = modelRowIndex[referenceModelID];
                fakedCellName = SheetHelper.GetCellName(rowIndex, FakedCellColumnIndex);
                if (rowIndex >= models[referenceModelID].RowCount)
                {
                    models[referenceModelID].RowCount = rowIndex + 1;
                }

                List<ReferenceCell> cells;
                if (referenceCells.ContainsKey(sourceFullName))
                {
                    cells = referenceCells[sourceFullName];
                }
                else
                {
                    cells = new List<ReferenceCell>();
                    referenceCells[sourceFullName] = cells;
                }

                ReferenceCell referenceCell = new ReferenceCell();
                referenceCell.RowIndex = rowIndex;
                referenceCell.ColumnIndex = FakedCellColumnIndex;
                referenceCell.Model = models[referenceModelID];

                cells.Add(referenceCell);

                rowIndex++;
                modelRowIndex[referenceModelID] = rowIndex;
            }
            else
            {
                fakedCellName = modelInternalCellMap[sourceFullName];
            }

            return fakedCellName;
        }

        public void UpdateCellValue(String cellFullName, object value)
        {
            if (referenceCells.ContainsKey(cellFullName))
            {
                List<ReferenceCell> cells = referenceCells[cellFullName];
                foreach (var cell in cells)
                {
                    cell.Model.SetValue(cell.RowIndex, cell.ColumnIndex, value);
                }
            }
        }
    }

    public class ReferenceCell
    {
        public DefaultSheetDataModel Model { get; set; }
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
    }

    public static class SheetHelper
    {
        public static string GetCellName(int rowIndex, int colIndex)
        {
            //if (rowIndex >= 99)
            //{
            //    colIndex--;
            //    rowIndex = rowIndex-99;
            //}
            return GetColumnName(colIndex) + (rowIndex + 1);
        }

        public static string GetColumnName(int colIndex)
        {
            string result = "";
            if (colIndex < 26)
            {
                result = "" + (char)('A' + (short)colIndex);
            }
            else
            {
                int repeatedNumber = (colIndex - 26) / 26;
                int currentIndex = colIndex % 26;
                result = new string(new char[] { (char)('A' + (short)repeatedNumber), (char)('A' + (short)currentIndex) });
            }

            return result;
        }

        public static Point GetCellPosition(string cellName)
        {

            Point pt = Point.Empty;
            Match match = Regex.Match(cellName, @"([a-z]+)(\d+)", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                string columnString = match.Groups[1].Value.ToUpper();
                string rowIndexString = match.Groups[2].Value;
                pt.Y = int.Parse(rowIndexString) - 1;
                if (columnString.Length > 2)
                {
                    throw new Exception("Invalid cell name!");
                }
                else if (columnString.Length == 2)
                {
                    pt.X = (columnString[0] - 'A' + 1) * 26 + (columnString[1] - 'A');
                }
                else
                {
                    pt.X = (columnString[0] - 'A');
                }
            }
            else
            {
                throw new Exception("Invalid cell name");
            }
            return pt;
        }

        public static int GetCellIndex(string columnString)
        {
            if (columnString.Length > 2)
            {
                throw new Exception("Invalid cell name!");
            }
            else if (columnString.Length == 2)
            {
                return (columnString[0] - 'A') * 26 + (columnString[1] - 'A');
            }
            else
            {
                return columnString[0] - 'A';
            }
        }

        public static bool HasExternalReference(string formula)
        {
            return Regex.IsMatch(formula, @"(?<!"")(\w+?)!([a-z]+\d+)(?!"")", RegexOptions.IgnoreCase);
        }
    }


}
