using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BizCommon;
using System.ServiceModel;
using System.Collections;
using TransferServiceCommon;
using System.Xml;
using System.IO;
using System.Data;
using System.Configuration;
using RabbitMQ.Client;
using System.Threading;
using RabbitMQ.Client.Events;
using Yqun.Services;
using Yqun.BO.BusinessManager;

namespace TestDataUploadWS
{
    /// <summary>
    /// 采集数据上传
    /// </summary>
    public class UploadCaiJi
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly object objMQ = new object();
        string DataSource,  DataInstance,  DataUserName,  DataPassword;
        public UploadCaiJi(string _DataSource, string _DataInstance, string _DataUserName, string _DataPassword)
        {
            DataSource = _DataSource;
            DataInstance = _DataInstance;
            DataUserName = _DataUserName;
            DataPassword = _DataPassword;
        }
        public void StartApplyQueue(string LineID)
        {
            ThreadParameter p = new ThreadParameter();
            p.LineID = LineID;
            //p.DataID = _DataID;
            //p.TestDataID = _TestDataID;
            ThreadPool.QueueUserWorkItem(new WaitCallback(ApplyQueue), p);
        }
        private void ApplyQueue(object paremeter)
        {
            ThreadParameter p = paremeter as ThreadParameter;
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection(999999))
            {
                using (var channel = connection.CreateModel())
                {
                    QueueingBasicConsumer consumer = null;

                    var queue_name = channel.QueueDeclare(p.LineID, true, false, false, null);//持久化队列

                    if (queue_name.ConsumerCount > 0)
                    {
                        return;
                    }
                    consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(p.LineID, false, consumer);

                    //channel.BasicQos(0, 1, false);
                    Boolean hasNext = true;
                    while (hasNext)
                    {
                        byte[] body = null;
                        var message = "";
                        BasicDeliverEventArgs ea = null;
                        try
                        {
                            //Thread.Sleep(3000);
                            consumer.Queue.Dequeue(3000, out ea);
                            if (ea != null)
                            {
                                body = ea.Body;
                                message = Encoding.UTF8.GetString(body);
                                string[] ss = message.Split(',');
                                if (ss.Length == 2)
                                {
                                    DealCaiJi(new Guid(ss[0]), new Guid(ss[1]));
                                }
                                channel.BasicAck(ea.DeliveryTag, false);
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Error(string.Format("【{0}】RabbitMQ ApplyQueue message:{1} error:{2}", p.LineID, message, ex.ToString()));
                        }
                    }
                }
            }
        }
        private void DealCaiJi(Guid DataID, Guid TestDataID)
        {
            CaiJiHelper caiji = new CaiJiHelper();
            BizCommon.Sys_TestData mdlTestData = CallLocalService("Yqun.BO.BusinessManager.dll", "GetTestDataModel", new object[] { TestDataID }) as Sys_TestData; //caiji.GetTestDataModel(TestDataID);
            if (mdlTestData != null)
            {
                Boolean inStadiumRange = true;
                if (mdlTestData.StadiumID == Guid.Empty)
                {
                    //inStadiumRange = caiji.InStadiumRange(DataID);
                }
                if (inStadiumRange)
                {
                    if (mdlTestData.Status == 0)
                    {
                        CallLocalService("Yqun.BO.BusinessManager.dll", "ApplyUploadDataNew", new object[] { mdlTestData.ID, mdlTestData.DataID, mdlTestData.ModuleID, mdlTestData.StadiumID, mdlTestData.WTBH, mdlTestData.TestRoomCode, mdlTestData.SerialNumber, mdlTestData.UserName, mdlTestData.TestData, mdlTestData.RealTimeData, mdlTestData.TotallNumber, mdlTestData.MachineCode, mdlTestData.UploadInfo, mdlTestData.UploadCode, DataInstance });
                        //caiji.ApplyUploadData(mdlTestData.ID, mdlTestData.DataID, mdlTestData.ModuleID, mdlTestData.StadiumID, mdlTestData.WTBH, mdlTestData.TestRoomCode, mdlTestData.SerialNumber, mdlTestData.UserName, mdlTestData.TestData, mdlTestData.RealTimeData, mdlTestData.TotallNumber, mdlTestData.MachineCode,                             mdlTestData.UploadInfo, mdlTestData.UploadCode);
                    }
                }
                else
                {
                    CallLocalService("Yqun.BO.BusinessManager.dll", "SaveTestOverTime", new object[] { mdlTestData.ID, mdlTestData.DataID, mdlTestData.ModuleID, mdlTestData.StadiumID, mdlTestData.WTBH, mdlTestData.TestRoomCode, mdlTestData.SerialNumber, mdlTestData.UserName, mdlTestData.TestData, mdlTestData.RealTimeData, mdlTestData.TotallNumber, mdlTestData.MachineCode, mdlTestData.UploadInfo, mdlTestData.UploadCode });
                    //caiji.SaveTestOverTime(mdlTestData.ID, mdlTestData.DataID, mdlTestData.ModuleID, mdlTestData.StadiumID, mdlTestData.WTBH, mdlTestData.TestRoomCode, mdlTestData.SerialNumber, mdlTestData.UserName, mdlTestData.TestData, mdlTestData.RealTimeData, mdlTestData.TotallNumber, mdlTestData.MachineCode, mdlTestData.UploadInfo, mdlTestData.UploadCode);
                }
            }
            else
            {
                logger.Info("【" + DataInstance + "】DealCaiJi TestDataModel Is Null.TestDataID:" + TestDataID);
            }
        }
        private class ThreadParameter
        {
            public string LineID;
            public Guid DataID;
            public Guid TestDataID;
            //public bool InStadiumRange;
        }

        #region 采集上传
//        public Boolean ApplyUploadData(Guid id, Guid documentID, Guid moduleID, Guid stadiumID, String wtbh, String testRoomCode,
//                Int32 seriaNumber, String userName, String testData, String realTimeData, Int32 totalNumber, String machineCode, String UploadInfo, string UploadCode)
//        {
//            Boolean isFinished = false;
//            isFinished = seriaNumber == totalNumber;
//            DocumentHelper dh = new DocumentHelper();
//            Boolean flag = false;
//            try
//            {
//                JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(dh.GetDocumentByID(documentID));
//                String sql = "";
//                if (doc == null)
//                    return flag;
//                ModuleHelper mh = new ModuleHelper();
//                Sys_Module module = mh.GetModuleBaseInfoByID(moduleID);
//                //FpSpread spread = null;
//                List<JZTestCell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZTestCell>>(testData);
//                if (cells != null && cells.Count > 0)
//                {
//                    Boolean changed = false;
//                    #region 更新单元格
//                    foreach (JZTestCell cell in cells)
//                    {
//                        foreach (JZSheet sheet in doc.Sheets)
//                        {
//                            if (sheet.ID == cell.SheetID)
//                            {
//                                foreach (JZCell dataCell in sheet.Cells)
//                                {
//                                    if (dataCell.Name == cell.CellName)
//                                    {
//                                        if (dataCell.Value != null && dataCell.Value.ToString().Trim() != "" && dataCell.Value.ToString().Trim() != "/")
//                                        {
//                                            //已经存在值，不更新
//                                            logger.Info("已经存在值" + dataCell.Value.ToString() + "，不更新: " + dataCell.Name + "<>" + cell.Value + "; wtbh=" + wtbh);
//                                        }
//                                        else
//                                        {
//                                            //用上传值去更新资料中的单元格
//                                            logger.Info("更新单元格: " + dataCell.Name + "=" + cell.Value + "; wtbh=" + wtbh);
//                                            dataCell.Value = cell.Value;
//                                            changed = true;
//                                        }
//                                        break;
//                                    }
//                                }
//                                break;
//                            }
//                        }
//                    }
//                    #endregion
//                    #region 更新数据库
//                    if (changed)
//                    {
//                        //模拟打开、保存资料、发送不合格报警
//                        //spread = GenerateDoc(doc, moduleID);
//                        GenerateDoc(doc, moduleID);

//                        //更新Document，和sys_test_data的Status为1
//                        String json = Newtonsoft.Json.JsonConvert.SerializeObject(doc).Replace("'", "''");
//                        sql = "UPDATE dbo.sys_document SET Data=@Data,MachineCode=@MachineCode,NeedUpload=1,LastEditedTime=getdate() WHERE ID=@ID";
//                        SqlCommand cmd1 = new SqlCommand(sql);
//                        cmd1.Parameters.Add(new SqlParameter("@ID", doc.ID));
//                        cmd1.Parameters.Add(new SqlParameter("@Data", json));
//                        cmd1.Parameters.Add(new SqlParameter("@MachineCode", machineCode));

//                        sql = "UPDATE dbo.sys_test_data SET Status=0 WHERE DataID=@DataID AND SerialNumber=@SerialNumber";
//                        SqlCommand cmd2 = new SqlCommand(sql);
//                        cmd2.Parameters.Add(new SqlParameter("@DataID", doc.ID));
//                        cmd2.Parameters.Add(new SqlParameter("@SerialNumber", seriaNumber));

//                        sql = "UPDATE dbo.sys_test_data SET Status=1 WHERE ID=@ID";
//                        SqlCommand cmd3 = new SqlCommand(sql);
//                        cmd3.Parameters.Add(new SqlParameter("@ID", id));

//                        flag = ExcuteCommandsWithTransaction(new List<IDbCommand>() { cmd1, cmd2, cmd3 });
//                        if (flag)
//                        {
//                            logger.Info("ApplyUploadData 更新完毕 id:" + id);
//                            if (dh.ApplyModuleSetting(doc, module.ModuleSettings))//给modulesetting定义字段赋值
//                            {
//                                //验证是否合格，并发短信
//                                QualifyHelper qh = new QualifyHelper();
//                                qh.Qualify(doc, module);
//                            }
//                            #region 上传实时数据到工管中心
//                            if (!string.IsNullOrEmpty(module.ModuleALTGG) && ConfigurationManager.AppSettings["GGCStartUpload"] == "1")
//                            {
//                                GGCUploadHelper ggcUH = new GGCUploadHelper();
//                                ggcUH.UploadCaiJiData(doc, module, id, machineCode, seriaNumber, testData, userName, realTimeData, testRoomCode);
//                            }
//                            #endregion
//                        }
//                        else
//                        {
//                            logger.Error("ExcuteCommandsWithTransaction 事物返回false，wtbh=" + wtbh);
//                        }
//                    }
//                    else
//                    {
//                        sql = "UPDATE dbo.sys_test_data SET Status=-1 WHERE ID=@ID";
//                        SqlCommand cmd = new SqlCommand(sql);
//                        cmd.Parameters.Add(new SqlParameter("@ID", id));

//                        flag = ExcuteCommandsWithTransaction(new List<IDbCommand>() { cmd });
//                    }
//                    #endregion
//                }

//                ThreadParameter parameter = new ThreadParameter();
//                parameter.IsSaved = flag;
//                parameter.SeriaNumber = seriaNumber;
//                parameter.DataID = documentID;
//                parameter.TestData = testData;
//                parameter.TestDataID = id;
//                parameter.machineBH = machineCode;
//                parameter.ModuleID = moduleID;
//                parameter.realTimeData = realTimeData;
//                parameter.stadiumID = stadiumID;
//                parameter.testRoomCode = testRoomCode;
//                parameter.userName = userName;
//                parameter.wtbh = wtbh;
//                parameter.isFinished = seriaNumber == totalNumber;
//                ThreadPool.QueueUserWorkItem(new WaitCallback(Execute), parameter);
//            }
//            catch (Exception ex)
//            {
//                logger.Error("ApplyUploadData Error:wtbh:" + wtbh + ",ErrorMsg:" + ex.Message);
//                flag = false;
//            }
//            finally
//            {
//                //UnLockDocument(documentID);
//                //ContinueProcess(documentID);
//                GC.Collect();
//                //Thread.Sleep(500);
//            }
//            return flag;
//        }

//        private void Execute(object paremeter)
//        {
//            ThreadParameter p = paremeter as ThreadParameter;
//            DocumentHelper dh = new DocumentHelper();
//            ModuleHelper mh = new ModuleHelper();

//            try
//            {
//                if (p.IsSaved)
//                {
//                    JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(dh.GetDocumentByID(p.DataID));
//                    if (p.isFinished)
//                    {
//                        //if (p.stadiumID != Guid.Empty)
//                        //{
//                        //    ExcuteCommand("UPDATE dbo.sys_stadium SET F_IsDone=1 WHERE ID='" + p.stadiumID + "'");
//                        //}
//                        //else if (p.DataID != Guid.Empty)
//                        //{
//                        //    ExcuteCommand("UPDATE dbo.sys_stadium SET F_IsDone=1 WHERE DataID='" + p.DataID + "'");
//                        //}
//                        try
//                        {
//                            #region 上传试验文档到工管中心
//                            Sys_Module module = mh.GetModuleBaseInfoByID(p.ModuleID);
//                            if (!string.IsNullOrEmpty(module.ModuleALTGG) && ConfigurationManager.AppSettings["GGCStartUpload"] == "1")
//                            {
//                                GGCUploadHelper ggcUH = new GGCUploadHelper();
//                                ggcUH.UploadLabDocBasic(doc, module, p.userName, p.testRoomCode);
//                            }
//                            #endregion
//                        }
//                        catch (Exception ex)
//                        {
//                            logger.Error("上传试验文档到工管中心 error:" + ex.ToString());
//                        }
//                    }

//                    //自动生成Excel，图片和报告页的pdf
//                    //SourceHelper sourceHelper = new SourceHelper();
//                    //sourceHelper.CreateRalationFiles(p.fpSpread, doc.ID, p.module.ReportIndex);
//                    if (!String.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["UploadInfoCenter"]))
//                    {
//                        List<JZTestCell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZTestCell>>(p.TestData);
//                        if (System.Configuration.ConfigurationManager.AppSettings["UploadInfoCenter"].Trim() == "1")
//                        {
//                            logger.Error("上传UpdateToInformationCenter返回：" + new UpdateToInformationCenter().PostDataToTDB(doc, p.ModuleID, p.stadiumID, p.wtbh, p.testRoomCode, p.SeriaNumber, p.userName, cells, p.realTimeData, p.machineBH, p.UploadInfo, p.UploadCode));
//                        }
//                        if (System.Configuration.ConfigurationManager.AppSettings["UploadEngineeringManagementCenter"].Trim() == "1")
//                        {
//                            logger.Error("上传UpdateToEngineeringManagementCenter返回：" + new UpdateToEngineeringManagementCenter().UpdateToEMC(doc, p.ModuleID, p.stadiumID, p.wtbh, p.testRoomCode, p.SeriaNumber, p.userName, cells, p.realTimeData, p.machineBH));
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.Error("上传数据error：" + ex.ToString());
//            }
//        }


//        public Sys_TestData GetTestDataModel(Guid TestDataID)
//        {
//            BizCommon.Sys_TestData model = new BizCommon.Sys_TestData();
//            String sql = "select ID,DataID,StadiumID,ModuleID,WTBH,TestRoomCode,SerialNumber,UserName,CreatedTime,TestData,RealTimeData,MachineCode,Status,UploadInfo,UploadCode,UploadTDB,UploadEMC,TotallNumber from sys_test_data where ID='" + TestDataID + "'";
//            using (DataTable dt = GetDataTable(sql))
//            {
//                if (dt != null && dt.Rows.Count > 0)
//                {
//                    DataRow row = dt.Rows[0];
//                    if (row != null)
//                    {
//                        if (row["ID"] != null && row["ID"].ToString() != "")
//                        {
//                            model.ID = new Guid(row["ID"].ToString());
//                        }
//                        if (row["DataID"] != null && row["DataID"].ToString() != "")
//                        {
//                            model.DataID = new Guid(row["DataID"].ToString());
//                        }
//                        if (row["StadiumID"] != null && row["StadiumID"].ToString() != "")
//                        {
//                            model.StadiumID = new Guid(row["StadiumID"].ToString());
//                        }
//                        if (row["ModuleID"] != null && row["ModuleID"].ToString() != "")
//                        {
//                            model.ModuleID = new Guid(row["ModuleID"].ToString());
//                        }
//                        if (row["WTBH"] != null)
//                        {
//                            model.WTBH = row["WTBH"].ToString();
//                        }
//                        if (row["TestRoomCode"] != null)
//                        {
//                            model.TestRoomCode = row["TestRoomCode"].ToString();
//                        }
//                        if (row["SerialNumber"] != null && row["SerialNumber"].ToString() != "")
//                        {
//                            model.SerialNumber = int.Parse(row["SerialNumber"].ToString());
//                        }
//                        if (row["UserName"] != null)
//                        {
//                            model.UserName = row["UserName"].ToString();
//                        }
//                        if (row["CreatedTime"] != null && row["CreatedTime"].ToString() != "")
//                        {
//                            model.CreatedTime = DateTime.Parse(row["CreatedTime"].ToString());
//                        }
//                        if (row["TestData"] != null)
//                        {
//                            model.TestData = row["TestData"].ToString();
//                        }
//                        if (row["RealTimeData"] != null)
//                        {
//                            model.RealTimeData = row["RealTimeData"].ToString();
//                        }
//                        if (row["MachineCode"] != null)
//                        {
//                            model.MachineCode = row["MachineCode"].ToString();
//                        }
//                        if (row["Status"] != null && row["Status"].ToString() != "")
//                        {
//                            model.Status = int.Parse(row["Status"].ToString());
//                        }
//                        if (row["UploadInfo"] != null)
//                        {
//                            model.UploadInfo = row["UploadInfo"].ToString();
//                        }
//                        if (row["UploadCode"] != null)
//                        {
//                            model.UploadCode = row["UploadCode"].ToString();
//                        }
//                        if (row["UploadTDB"] != null && row["UploadTDB"].ToString() != "")
//                        {
//                            model.UploadTDB = int.Parse(row["UploadTDB"].ToString());
//                        }
//                        if (row["UploadEMC"] != null && row["UploadEMC"].ToString() != "")
//                        {
//                            model.UploadEMC = int.Parse(row["UploadEMC"].ToString());
//                        }
//                        if (row["TotallNumber"] != null && row["TotallNumber"].ToString() != "")
//                        {
//                            model.TotallNumber = int.Parse(row["TotallNumber"].ToString());
//                        }
//                    }
//                    else
//                    {
//                        model = null;
//                    }
//                }
//                else
//                {
//                    model = null;
//                }
//            }
//            return model;
//        }
//        public void SaveTestOverTime(Guid testID, Guid documentID, Guid moduleID, Guid stadiumID, String wtbh, String testRoomCode,
//                Int32 seriaNumber, String userName, String testData, String realTimeData, Int32 totalNumber, String machineCode, String UploadInfo, string UploadCode)
//        {
//            String sql = String.Format(@"INSERT INTO dbo.sys_test_overtime
//                                                        ( ID ,
//                                                          CaiJiID ,
//                                                          DataID ,
//                                                          StadiumID ,
//                                                          ModuleID ,
//                                                          WTBH ,
//                                                          TestRoomCode ,
//                                                          SerialNumber ,
//                                                          UserName ,
//                                                          CreatedTime ,
//                                                          TestData ,
//                                                          RealTimeData ,
//                                                          MachineCode ,
//                                                          UploadInfo ,
//                                                          UploadCode ,
//                                                          UploadTDB ,
//                                                          UploadEMC ,
//                                                          TotallNumber 
//                                                        )
//                                                VALUES  ( NEWID(),
//                                                          '{0}',
//                                                          '{1}',
//                                                          '{2}',
//                                                          '{3}',
//                                                          '{4}',
//                                                          '{5}',
//                                                          '{6}',
//                                                          '{7}',
//                                                          GETDATE(),
//                                                          '{8}',
//                                                          '{9}',
//                                                          '{10}',
//                                                          '{11}',
//                                                          '{12}',
//                                                          0 , 
//                                                          0 , 
//                                                          '{13}'
//                                                        )",
//                testID, documentID, stadiumID, moduleID, wtbh, testRoomCode, seriaNumber,
//                userName, testData, realTimeData, machineCode,
//                UploadInfo, UploadCode, totalNumber);

//            ExcuteCommand(sql);
//        }
        #endregion

        #region 通用
        public object CallLocalService(string AssemblyName, string MethodName, object[] Parameters)
        {

            return Transfer.CallLocalServiceWithDBArgs(AssemblyName, MethodName, Parameters,DataSource,DataInstance,DataUserName,DataPassword);
        }

        #endregion
    }

}
