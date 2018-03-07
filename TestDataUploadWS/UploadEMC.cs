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
using System.Threading;

namespace TestDataUploadWS
{
    /// <summary>
    /// 把以前的数据上传到工管中心
    /// </summary>
    public class UploadEMC
    {
        private string IPAddress = string.Empty;
        private string Port = string.Empty;
        private String LineName = "";
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public UploadEMC(String _LineName, String _IPAddress, String _Port)
        {
            IPAddress = _IPAddress;
            Port = _Port;
            LineName = _LineName;
        }
        public void StartUpload()
        {
            ThreadParameter p = new ThreadParameter();
            ThreadPool.QueueUserWorkItem(new WaitCallback(UploadGGCTestDataInfo), p);
        }
        #region 试验资料上传

        /// <summary>
        /// 上传试验数据
        /// </summary>
        private void UploadGGCTestDataInfo(object paremeter)
        {
            try
            {
                bool bHasData = true;//只传一次，设置为false,一直传直到没有数据设置为true
                bool bStartUpload = false;
                do
                {
                    int hour = DateTime.Now.Hour;
                    string UploadTimeSpan = ConfigurationManager.AppSettings["GGCUploadTimeSpan"];
                    if (!string.IsNullOrEmpty(UploadTimeSpan))
                    {
                        #region 判断时间段
                        string[] sUTS = UploadTimeSpan.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string s in sUTS)
                        {
                            if (!string.IsNullOrEmpty(s))
                            {
                                string[] ss = s.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                int iStart = int.Parse(ss[0]);
                                int iEnd = int.Parse(ss[1]);
                                if (hour >= iStart && hour <= iEnd)
                                {
                                    bStartUpload = true;
                                    break;
                                }
                            }
                        }
                        #endregion
                        if (bStartUpload == true)
                        {
                            //logger.Info("开始上传");
                            #region 上传
                            string sql = @"SELECT top 10 ID,Data,ModuleID,WTBH,BGBH FROM dbo.sys_document WHERE  Status>0 and GGCNeedUpload=1 and BGBH is not null and BGBH<>'' AND ModuleID IN( SELECT ID FROM dbo.sys_module WHERE DeviceType>0 and IsActive=1 and ModuleALTGG is not null and ModuleALTGG<>'') {0} 
  ORDER BY CreatedTime";//排除人员、设备、试验室和没有设置RTCODE的模板
                            string strNeedUploadTestRoomCode = GetNeedUploadTestRoomCode();
                            if (!string.IsNullOrEmpty(strNeedUploadTestRoomCode))
                            {
                                sql = string.Format(sql, "  AND TestRoomCode IN(" + strNeedUploadTestRoomCode + ") ");
                            }
                            else
                            {
                                sql = string.Format(sql, "  AND 1=0 ");
                            }
                            //logger.Error("sql:" + sql);
                            DataTable dt = GetDataTable(sql);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                JZDocument doc;
                                Guid ModuleID;
                                string TestRoomCode;
                                int SerialNumber, TotallNumber;
                                string UserName;
                                //List<JZTestCell> cells;
                                string RealTimeData, MachineCode, TestData;//, UploadInfo, UploadCode;
                                Sys_Module module = null;
                                string DataID = string.Empty;
                                DataTable dtTestData;
                                Guid TestDataID;
                                #region 上传资料列表
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    DataID = dt.Rows[i]["ID"].ToString();
                                    doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(dt.Rows[i]["Data"].ToString());
                                    ModuleID = new Guid(dt.Rows[i]["ModuleID"].ToString());

                                    module = GetModuleBaseInfoByID(ModuleID);
                                    dtTestData = GetTestData(DataID);
                                    if (dtTestData != null && dtTestData.Rows.Count > 0)
                                    {
                                        #region 上传资料所属试验数据
                                        for (int j = 0; j < dtTestData.Rows.Count; j++)
                                        {
                                            TestDataID = new Guid(dtTestData.Rows[j]["ID"].ToString());
                                            MachineCode = dtTestData.Rows[j]["MachineCode"].ToString();
                                            SerialNumber = int.Parse(dtTestData.Rows[j]["SerialNumber"].ToString());
                                            TestData = dtTestData.Rows[j]["TestData"].ToString();
                                            UserName = dtTestData.Rows[j]["UserName"].ToString();
                                            TestRoomCode = dtTestData.Rows[j]["TestRoomCode"].ToString();
                                            RealTimeData = dtTestData.Rows[j]["RealTimeData"].ToString();
                                            TotallNumber = int.Parse(dtTestData.Rows[j]["TotallNumber"].ToString());
                                            UploadCaiJiData(doc, module, TestDataID, MachineCode, SerialNumber, TestData, UserName, RealTimeData, TestRoomCode);
                                            logger.Info(string.Format("【{0}】试验数据上传成功,DataID:{1},TestDataID:{2}", LineName, DataID, TestDataID));
                                            //logger.Info("SerialNumber:" + SerialNumber+""+TotallNumber);
                                            if (SerialNumber == TotallNumber)
                                            {
                                                UploadLabDocBasic(doc, module, UserName, TestRoomCode);
                                                logger.Info(string.Format("【{0}】试验数文档上传成功,DataID:{1},TestDataID:{2}", LineName, DataID, TestDataID));
                                            }
                                        }
                                        UpdateGGCDocumentNeedUpload(DataID, 0);
                                        #endregion
                                    }
                                    else
                                    {
                                        UpdateGGCDocumentNeedUpload(DataID, 2);//资料没有对应的试验数据
                                        logger.Info(string.Format("【{0}】该资料没有对应的试验数据,DataID:" + DataID, LineName));
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                bHasData = false;
                                logger.Info(string.Format("【{0}】没有需要上传的资料", LineName));
                            }
                            #endregion
                        }
                    }
                } while (bHasData && bStartUpload);
                logger.Info(string.Format("【{0}】上传资料结束", LineName));
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("【{0}】UploadGGCTestDataInfo error:" + ex.ToString(), LineName));
            }
        }
        #endregion
        #region 上传公用方法
        /// <summary>
        /// 旧的上传到工管中心的接口
        /// </summary>
        protected void UploadLabDocBasic(JZDocument doc, Sys_Module ModuleBase, string UserName, string TestRoomCode)
        {
            String lineAddress = "net.tcp://" + IPAddress + ":" + Port + "/TransferService.svc";
            CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "UploadLabDocBasic",
               new Object[] { doc, ModuleBase, UserName, TestRoomCode });
        }
        /// <summary>
        /// 新的上传采集数据到工管中心的接口
        /// </summary>
        protected void UploadCaiJiData(JZDocument doc, Sys_Module ModuleBase, Guid TestDataID, string MachineCode, int SeriaNumber, string TestData, string UserName, string RealTimeData, string TestRoomCode)
        {
            String lineAddress = "net.tcp://" + IPAddress + ":" + Port + "/TransferService.svc";
            CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "UploadCaiJiData",
               new Object[] { doc, ModuleBase, TestDataID, MachineCode, SeriaNumber, TestData, UserName, RealTimeData, TestRoomCode });
        }
        /// <summary>
        /// 旧的上传到工管中心的接口
        /// </summary>
        protected bool UpdateToEMC(JZDocument doc, Guid moduleID, Guid stadiumID, String wtbh, String testRoomCode,
                Int32 seriaNumber, String userName, List<JZTestCell> cells, String realTimeData, string machineBH)
        {
            String lineAddress = "net.tcp://" + IPAddress + ":" + Port + "/TransferService.svc";
            bool bSuccess = bool.Parse(CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "UpdateToEMC",
               new Object[] { doc, moduleID,  stadiumID,  wtbh,  testRoomCode,
                 seriaNumber,  userName,  cells,  realTimeData,  machineBH }).ToString());
            return bSuccess;
        }

        protected DataTable GetTestData(string DataID)
        {
            String strSQL = "SELECT * FROM dbo.sys_test_data WHERE DataID='" + DataID + "' AND Status=1 ORDER BY SerialNumber ";
            DataTable dt = new DataTable();
            String lineAddress = "net.tcp://" + IPAddress + ":" + Port + "/TransferService.svc";
            dt = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "GetDataTable",
               new Object[] { strSQL }) as DataTable;
            return dt;
        }

        protected Sys_Module GetModuleBaseInfoByID(Guid ModuleID)
        {

            Sys_Module module = new Sys_Module();
            String lineAddress = "net.tcp://" + IPAddress + ":" + Port + "/TransferService.svc";
            module = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "GetModuleBaseInfoByID",
               new Object[] { ModuleID }) as Sys_Module;
            return module;
        }
        Boolean UpdateGGCDocumentNeedUpload(string strID, int GGCNeedUpload)
        {
            String lineAddress = "net.tcp://" + IPAddress + ":" + Port + "/TransferService.svc";
            //logger.Error("strID:" + strID + " GGCNeedUpload:" + GGCNeedUpload);
            object obj = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "UpdateGGCDocumentNeedUpload",
               new Object[] { strID, GGCNeedUpload });

            if (obj != null)
            {
                return Boolean.Parse(obj.ToString());
            }
            else
            {
                return false;
            }
        }
        string GetNeedUploadTestRoomCode()
        {
            String lineAddress = "net.tcp://" + IPAddress + ":" + Port + "/TransferService.svc";
            object obj = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "GetNeedUploadTestRoomCode",
               new Object[] { });

            if (obj != null)
            {
                return obj.ToString();
            }
            else
            {
                return "";
            }
        }
        protected DataTable GetDataTable(String strSQL)
        {

            DataTable dt = new DataTable();
            String lineAddress = "net.tcp://" + IPAddress + ":" + Port + "/TransferService.svc";
            dt = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "GetDataTable",
               new Object[] { strSQL }) as DataTable;
            return dt;
        }
        #endregion
        #region 通用方法

        public object CallRemoteServerMethod(String address, string AssemblyName, string MethodName, object[] Parameters)
        {
            object obj = null;
            try
            {
                using (ChannelFactory<ITransferService> channelFactory = new ChannelFactory<ITransferService>("sClient", new EndpointAddress(address)))
                {

                    ITransferService proxy = channelFactory.CreateChannel();
                    using (proxy as IDisposable)
                    {
                        Hashtable hashtable = new Hashtable();
                        hashtable["assembly_name"] = AssemblyName;
                        hashtable["method_name"] = MethodName;
                        hashtable["method_paremeters"] = Parameters;

                        Stream source_stream = Yqun.Common.Encoder.Serialize.SerializeToStream(hashtable);
                        Stream zip_stream = Yqun.Common.Encoder.Compression.CompressStream(source_stream);
                        source_stream.Dispose();
                        Stream stream_result = proxy.InvokeMethod(zip_stream);
                        zip_stream.Dispose();
                        Stream ms = ReadMemoryStream(stream_result);
                        stream_result.Dispose();
                        Stream unzip_stream = Yqun.Common.Encoder.Compression.DeCompressStream(ms);
                        ms.Dispose();
                        Hashtable Result = Yqun.Common.Encoder.Serialize.DeSerializeFromStream(unzip_stream) as Hashtable;

                        obj = Result["return_value"];
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("call remote server method error: " + ex.Message);
            }
            return obj;
        }

        private MemoryStream ReadMemoryStream(Stream Params)
        {
            MemoryStream serviceStream = new MemoryStream();
            byte[] buffer = new byte[10000];
            int bytesRead = 0;
            int byteCount = 0;

            do
            {
                bytesRead = Params.Read(buffer, 0, buffer.Length);
                serviceStream.Write(buffer, 0, bytesRead);

                byteCount = byteCount + bytesRead;
            } while (bytesRead > 0);

            serviceStream.Position = 0;

            return serviceStream;
        }
        #endregion
        private class ThreadParameter
        {
        }
    }

}
