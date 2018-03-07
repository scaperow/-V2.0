using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Yqun.Data.DataBase;
using System.Configuration;
using System.Threading;
using System.IO;
using System.Collections;
using System.ServiceModel;
using System.Net;
using TransferServiceCommon;

namespace Yqun.BO.ReminderManager
{
    public class SMSManager : BOBase
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Boolean RecordSMSItem(String DataIndex, String[] cellnumbers, String content, String sentStatus)
        {
            Boolean Result = true;
            IDbConnection DbConnection = GetConntion();
            Transaction Transaction = new Transaction(DbConnection);

            try
            {
                List<string> sql_Commands = new List<string>();

                //更新错误报告中记录
                String updateSql = String.Format("UPDATE dbo.sys_invalid_document SET SentCount=SentCount+1, LastSentStatus='{0}',LastSentTime=GETDATE() WHERE ID='{1}'", sentStatus, DataIndex);
                sql_Commands.Add(updateSql);

                //插入短信日志表
                foreach (var item in cellnumbers)
                {
                    sql_Commands.Add(String.Format("INSERT INTO dbo.sys_sms_log( SMSPhone ,SMSContent ,SentError ,RelationID) VALUES  ('{0}', '{1}', '{2}', '{3}')", item, content, sentStatus, DataIndex));
                }

                int[] ints = ExcuteCommands(sql_Commands.ToArray(), Transaction);

                for (int i = 0; i < ints.Length; i++)
                {
                    if (i != 0)
                    {
                        Result = Result && (Convert.ToInt32(ints[i]) == 1);
                    }
                    else
                    {
                        Result = (Convert.ToInt32(ints[i]) == 1);
                    }
                }

                if (Result)
                {
                    Transaction.Commit();
                }
                else
                {
                    Transaction.Rollback();
                    logger.Error("更新短信发送数据失败：ID为【" + DataIndex + "】的错误报告，内容异常");
                }
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
                logger.Error("更新短信发送数据失败：ID为【" + DataIndex + "】的错误报告，更新失败");
            }

            return Result;
        }

        public void SendSMS(String DataIndex, String invalidItem, string LineID, string TestRoomCode, string SegmentName, string CompanyName, string TestRoomName, string LineName)
        {
            if (StartSMS())
            {
                ThreadParameter p = new ThreadParameter();
                p.DataIndex = DataIndex;
                p.InvalidItem = invalidItem;
                p.LineID = LineID;
                p.TestRoomCode = TestRoomCode;
                p.SegmentName = SegmentName;
                p.CompanyName = CompanyName;
                p.TestRoomName = TestRoomName;
                p.LineName = LineName;

                ThreadPool.QueueUserWorkItem(new WaitCallback(Execute), p);
            }
            else
            {
                logger.Error("短信服务已关闭");
            }
        }

        private void Execute(object paremeter)
        {
            ThreadParameter p = paremeter as ThreadParameter;
            String DataIndex = p.DataIndex;
            String invalidItem = p.InvalidItem;
            string[] cells = GetSMSCells();
            if (cells.Length == 0)
            {
                logger.Error("获取短信收件人电话失败，未设置SMSCells");
                return;
            }
            string smsContent = GetSMSContent(DataIndex, invalidItem);


            if (String.IsNullOrEmpty(smsContent))
            {
                logger.Error("获取短信内容失败，错误报告ID为：" + DataIndex);
                return;
            }
            smsContent = smsContent + "  " + DateTime.Now;
            var errorMsg = "";
            errorMsg = SMSAgent.CallRemoteService(cells, smsContent);
            RecordSMSItem(DataIndex, cells, smsContent, errorMsg);

            string smsTower = smsContent.ToUpper();

            #region 给领导发
            if (smsTower.Contains("#VALUE") ||
                smsTower.Contains("TRUE") ||
                smsTower.Contains("FALSE") ||
                smsTower.Contains("#N/A") ||
                smsTower.Contains("#REF!") ||
                smsTower.Contains("#DIV/0!") ||
                smsTower.Contains("#NUM!") ||
                smsTower.Contains("#NAME?") ||
                smsTower.Contains("#NULL!") ||
                smsTower.Contains("第一根断后伸长率的实测值为-") ||
                smsTower.Contains("第二根断后伸长率的实测值为-") ||
                smsTower.Contains("第一根屈服点的实测值为0") ||
                smsTower.Contains("第二根屈服点的实测值为0")
                )
            {
                logger.Error("内含关键词，不发送短信,DataID=" + DataIndex);
            }
            else
            {
                string[] cellsleader = GetSMSCellsLeader(DataIndex);
                if (cellsleader.Length > 0)
                {
                    errorMsg = SMSAgent.CallRemoteService(cellsleader, smsContent);
                    RecordSMSItem(DataIndex, cellsleader, smsContent, errorMsg);
                }
            }
            #endregion
            //给App推送消息
            SendAppMessage(p.LineID, DataIndex, p.TestRoomCode, p.SegmentName, p.CompanyName, p.TestRoomName, p.LineName, 1, smsContent);
        }
        public string TestSMSSend(string phones, string context)
        {
            string[] cells = phones.Split(',');
            string smsContent = context;
            return SMSAgent.CallRemoteService(cells, smsContent);
        }

        private String GetSMSContent(String DataIndex, String F_InvalidItem)
        {
            // 182行 增加查询条件 a.Scdel=0  2013-10-17
            String sql = String.Format(@"
            select b.工程名称 as 工程,
            b.标段名称 as 标段,
            b.单位名称 as 单位,
            b.试验室名称 as 试验室,
            g.Name AS ReportName,
            f.BGBH AS ReportNumber,
            f.BGRQ,
            a.F_InvalidItem,
            f.WTBH
            from 
            sys_invalid_document as a,
            v_bs_codeName b,
            dbo.sys_document f,
            dbo.sys_module g
            where b.试验室编码 = f.TestRoomCode and
            a.ID='{0}' AND a.ID = f.ID AND f.ModuleID = g.ID", DataIndex);

            DataTable SmsInfo = GetDataTable(sql);
            String text = "";
            if (SmsInfo != null)
            {
                if (SmsInfo.Rows.Count > 0)
                {
                    DataRow row = SmsInfo.Rows[0];
                    text = row["工程"].ToString() + row["标段"].ToString() + row["单位"].ToString() +
                        row["试验室"].ToString() + "的" + row["ReportName"].ToString() + @"的";

                    String invalidItem = F_InvalidItem;
                    String[] conditions = invalidItem.Split(new String[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in conditions)
                    {
                        String[] subItems = item.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        if (subItems.Length == 3)
                        {
                            text += subItems[0] + "的实测值为" + subItems[2] + "，标准值为" + subItems[1] + "；";
                        }
                    }
                    if (row["ReportNumber"] == DBNull.Value || row["ReportNumber"].ToString().Trim() == "")
                    {
                        if (row["WTBH"] == DBNull.Value || row["WTBH"].ToString().Trim() == "")
                        {
                            text += "无委托或报告编号";
                        }
                        else
                        {
                            text += "委托编号为" + row["WTBH"].ToString() + "，该试验不合格";
                        }
                    }
                    else
                    {
                        text += "报告编号为" + row["ReportNumber"].ToString() + "，该试验不合格";
                    }
                }
            }

            return text;
        }

        private String[] GetSMSCells()
        {
            String phone = ConfigurationManager.AppSettings["SMSCells"];
            String[] cells = null;

            if (!String.IsNullOrEmpty(phone))
            {
                cells = phone.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                cells = new String[0];
            }

            return cells;
        }

        private String[] GetSMSCellsLeader(String DataIndex)
        {
            string testcode = GetTestroomCode(DataIndex);
            List<String> cells = new List<string>();
            try
            {
                String sql = String.Format(@"select CellPhone from sys_sms_receiver  where IsActive=1 and TestRoomCode='{0}' ", testcode);
                DataTable dt = GetDataTable(sql);
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        cells.Add(dr["CellPhone"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error(e.ToString());

            }
            return cells.ToArray();
        }


        private String GetTestroomCode(String DataIndex)
        {
            //  增加查询条件 a.Scdel=0  2013-10-17
            String sql = String.Format(@"
                            SELECT b.TestRoomCode FROM dbo.sys_invalid_document a
                            JOIN dbo.sys_document b ON a.ID = b.ID WHERE a.ID='{0}'", DataIndex);

            DataTable SmsInfo = GetDataTable(sql);
            String text = "";
            if (SmsInfo != null)
            {
                if (SmsInfo.Rows.Count > 0)
                {
                    text = SmsInfo.Rows[0][0].ToString();
                }
            }

            return text;
        }

        public Boolean NeedSendSMS(String DataIndex, String invalidItem)
        {
            Boolean flag = false;
            String sql = String.Format("SELECT ID FROM dbo.sys_invalid_document WHERE ID='{0}' AND  F_InvalidItem='{1}' AND SentCount>0", DataIndex, invalidItem);
            DataTable tb = GetDataTable(sql);
            if (tb != null)
            {
                if (tb.Rows.Count == 0)
                {
                    flag = true;
                }
            }
            return flag;
        }

        private Boolean StartSMS()
        {
            String start = ConfigurationManager.AppSettings["SMSStart"];
            if (start == "1")
            {
                return true;
            }
            return false;
        }

        private class ThreadParameter
        {
            public String DataIndex;
            public String InvalidItem;
            public string LineID;
            public string TestRoomCode;
            public string SegmentName;
            public string CompanyName;
            public string TestRoomName;
            public string LineName;
        }

        #region

        public bool SendAppMessage(string LineID, string DataID, string TestRoomCode, string SegmentName, string CompanyName, string TestRoomName, string LineName, int MsgType, string MsgFull)
        {
            string Msg = string.Empty;
            switch (MsgType)
            {
                case 1:
                    Msg = LineName + SegmentName + CompanyName + TestRoomName + "不合格报告提醒";
                    break;
                case 2:
                    Msg = LineName + SegmentName + CompanyName + TestRoomName + "关键值修改提醒";
                    break;
                default:
                    break;
            }

            string moduleLibAddress = "net.tcp://Lib.kingrocket.com:8066/TransferService.svc";
            //去模板库中取模板数据
            object obj = CallRemoteServerMethod(moduleLibAddress, "Yqun.BO.ReminderManager.dll", "SendAppMessageLib",
                            new Object[] { LineID, DataID, TestRoomCode, SegmentName, CompanyName, TestRoomName, MsgType, Msg, MsgFull });
            //object obj = CallRemoteServerMethod(moduleLibAddress, "Yqun.BO.ReminderManager.dll", "SendAppMessageLib1",
            //                new Object[] {});

            return Convert.ToBoolean(obj);
        }
        private DataTable GetLineTagAndName(string LineID)
        {
            String strSQL = "select LineTag,Description from sys_line where ID='" + LineID + "'";
            DataTable dt = new DataTable();
            //string moduleLibAddress = "net.tcp://Lib.kingrocket.com:8066/TransferService.svc";
            //dt = CallRemoteServerMethod(moduleLibAddress, "Yqun.BO.BusinessManager.dll", "GetDataTable",new Object[] { strSQL }) as DataTable;
            dt = GetDataTable(strSQL);
            return dt;
        }
        public bool SendAppMessageLib(string LineID, string DataID, string TestRoomCode, string SegmentName, string CompanyName, string TestRoomName, int MsgType, string Msg, string MsgFull)
        {
            int LineTag = 0;
            string LineName = string.Empty;
            DataTable dt = GetLineTagAndName(LineID);
            if (dt != null && dt.Rows.Count > 0)
            {
                LineTag = int.Parse(dt.Rows[0]["LineTag"].ToString());
                LineName = dt.Rows[0]["Description"].ToString();
            }
            else
            {
                return false;
            }
            String sql = string.Empty;
            #region INSERT
            sql = String.Format(@"INSERT INTO dbo.app_message
        ( Msg ,
          MsgFull ,
          MsgType ,
          SendTime ,
          LineID ,
          LineTag ,
          TestRoomCode ,
          LineName ,
          SegmentName ,
          CompanyName ,
          TestRoomName ,
          Result ,
          DataID
        )
VALUES  ( N'{1}' , -- Msg - nvarchar(255)
          '{2}' , -- MsgFull - text
          {3} , -- MsgType - smallint
          getdate() , -- SendTime - datetime
          '{0}' , -- LineID - uniqueidentifier
          {4} , -- LineTag - int
          N'{5}' , -- TestRoomCode - nvarchar(20)
          N'{6}' , -- LineName - nvarchar(255)
          N'{7}' , -- SegmentName - nvarchar(255)
          N'{8}' , -- CompanyName - nvarchar(255)
          N'{9}' , -- TestRoomName - nvarchar(255)
          0 , -- Result - smallint
          '{10}'  -- DataID - uniqueidentifier
        )", LineID, Msg, MsgFull, MsgType, LineTag, TestRoomCode, LineName, SegmentName, CompanyName, TestRoomName, DataID);
            //logger.Error("SendAppMessageLib  LineID:" + LineID + " DataID:" + DataID + " TestRoomCode:" + TestRoomCode + " SegmentName:" + SegmentName + " CompanyName:" + CompanyName + " TestRoomName:" + TestRoomName + " MsgType:" + MsgType + " Msg:" + Msg + " MsgFull:" + MsgFull + " LineTag:" + LineTag + " LineName:" + LineName);
            int rows = ExcuteCommand(sql);

            #endregion
            #region Post提交
            if (rows > 0)
            {
                string postUrl = "http://app.kingrocket.com:9311/notification/send";
                string paramData = string.Format("lineID={0}&lineTag={1}&testRoom={2}&category={3}&message={4}", LineID, LineTag, TestRoomName, MsgType, Msg);
                PostWebRequest(postUrl, paramData, Encoding.UTF8);
                return true;
            }
            else
            {
                return false;
            }
            #endregion
        }
        #region 通用
        /// <summary>
        /// Post提交数据
        /// </summary>
        /// <param name="postUrl"></param>
        /// <param name="paramData"></param>
        /// <param name="dataEncode"></param>
        /// <returns></returns>
        private string PostWebRequest(string postUrl, string paramData, Encoding dataEncode)
        {
            string ret = string.Empty;
            try
            {
                byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                logger.Error("PostWebRequest error:" + ex.Message);
            }
            return ret;
        }



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
        #endregion
    }
}
