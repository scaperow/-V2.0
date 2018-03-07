using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using BizCommon;
using Yqun.Common.ContextCache;
using Yqun.BO.ReminderManager;
using System.Configuration;
using System.Data;

namespace Yqun.BO.BusinessManager
{
    public class CellChangedNotifyHelper : BOBase
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void NotifyLeader(Guid dataID, List<CellChangedNotifyItem> items, string UserName, bool IsAdministrator, Guid requestID, string LineID, string TestRoomCode, string SegmentName, string CompanyName, string TestRoomName, string LineName)
        {
            ThreadParameter p = new ThreadParameter();
            p.DataID = dataID;
            p.Items = items;
            p.IsAdministrator = IsAdministrator;
            p.UserName = UserName;
            p.RequestID = requestID;

            p.LineID = LineID;
            p.TestRoomCode = TestRoomCode;
            p.SegmentName = SegmentName;
            p.CompanyName = CompanyName;
            p.TestRoomName = TestRoomName;
            p.LineName = LineName;
            ThreadPool.QueueUserWorkItem(new WaitCallback(Execute), p);
        }

        private void Execute(object paremeter)
        {
            ThreadParameter p = paremeter as ThreadParameter;
            if (p.Items == null || p.Items.Count == 0)
            {
                logger.Error("未修改关键字段，document的ID为" + p.DataID + "；");
                return;
            }

            //logger.Error("p.IsAdministrator:" + p.IsAdministrator + "；");
            try
            {
                String json = Newtonsoft.Json.JsonConvert.SerializeObject(p.Items);
                DocumentHelper dh = new DocumentHelper();
                Sys_Document doc = dh.GetDocumentBaseInfoByID(p.DataID);
                if (doc != null)
                {
                    String sql = string.Empty;
                    string JLUserName = string.Empty, JLContent = string.Empty, SGUserName = string.Empty, SGContent = string.Empty;
                    DateTime JLOPTime = DateTime.Now, SGOPTime = DateTime.Now;
                    DataTable dtJL = new DataTable();
                    #region 处理监理和施工意见
                    if (p.RequestID != Guid.Empty)
                    {
                        sql = string.Format("SELECT RequestBy,RequestTime,Reason,ApprovePerson,ApproveTime,ProcessReason FROM dbo.sys_request_change WHERE id='{0}'", p.RequestID);
                        dtJL = GetDataTable(sql);
                        if (dtJL != null && dtJL.Rows.Count > 0)
                        {
                            JLUserName = dtJL.Rows[0]["ApprovePerson"] == null ? "" : dtJL.Rows[0]["ApprovePerson"].ToString();
                            JLContent = dtJL.Rows[0]["ProcessReason"] == null ? "" : dtJL.Rows[0]["ProcessReason"].ToString();
                            SGUserName = dtJL.Rows[0]["RequestBy"] == null ? "" : dtJL.Rows[0]["RequestBy"].ToString();
                            SGContent = dtJL.Rows[0]["Reason"] == null ? "" : dtJL.Rows[0]["Reason"].ToString();
                            JLOPTime = dtJL.Rows[0]["ApproveTime"] == null ? DateTime.Now : DateTime.Parse(dtJL.Rows[0]["ApproveTime"].ToString());
                            SGOPTime = dtJL.Rows[0]["RequestTime"] == null ? DateTime.Now : DateTime.Parse(dtJL.Rows[0]["RequestTime"].ToString());
                            JLContent = JLContent.Replace("'", " ");
                            SGContent = SGContent.Replace("'", " ");
                            sql = "SELECT 1 FROM dbo.sys_KeyModify WHERE RequestID='" + p.RequestID + "' AND ModifyItem='" + json + "'";
                            DataTable dtIsExist = GetDataTable(sql);
                            if (dtIsExist != null && dtIsExist.Rows.Count == 0)
                            {
                                #region INSERT
                                sql = String.Format(@"INSERT INTO dbo.sys_KeyModify
                                ( DataID ,
                                  ModuleID ,
                                  RequestID ,
                                  TestRoomCode ,
                                  BGBH ,
                                  DataName ,
                                  ModifyItem ,
                                  ModifyBy ,
                                  ModifyTime ,
                                  Status,JLUserName ,JLOPTime ,JLContent ,SGUserName ,SGOPTime ,SGContent 
                                )
                        VALUES  ( '{0}' ,
                                  '{1}' ,
                                  '{2}' ,
                                  '{3}' ,
                                  '{4}' ,
                                  '{5}' ,
                                  '{6}' ,
                                  '{7}' ,
                                  GETDATE(),
                                  0,'{8}','{9}','{10}','{11}','{12}','{13}' 
                                )", doc.ID, doc.ModuleID, p.RequestID, doc.TestRoomCode, doc.BGBH, doc.DataName, json, p.UserName, JLUserName, JLOPTime, JLContent, SGUserName, SGOPTime, SGContent);
                                ExcuteCommand(sql);
                                #endregion
                                #region 发送短信
                                if (!p.IsAdministrator && ConfigurationManager.AppSettings["ChangedSMSStart"] == "1")
                                {
                                    String smsContent = GetSMSContent(doc.ID, p.Items, p.UserName, p.RequestID, dtJL);
                                    //logger.Error("smsContent:" + smsContent);
                                    if (String.IsNullOrEmpty(smsContent))
                                    {
                                        logger.Error("未得到短信信息，document的ID为" + p.DataID + "；");
                                        return;
                                    }
                                    String[] cells = GetSMSCells();
                                    if (cells.Length == 0)
                                    {
                                        logger.Error("未设置手机收信人信息，document的ID为" + p.DataID + "；");
                                        return;
                                    }
                                    String errorMsg = SMSAgent.CallRemoteService(cells, smsContent);
                                    SMSManager smsManager = new SMSManager();
                                    smsManager.SendAppMessage(p.LineID, doc.ID.ToString(), p.TestRoomCode, p.SegmentName, p.CompanyName, p.TestRoomName, p.LineName, 2, smsContent);
                                    if (!string.IsNullOrEmpty(errorMsg))
                                    {
                                        logger.Error("修改资料短信短信发送完成：" + errorMsg);
                                    }
                                }
                                else
                                {
                                    logger.Error("管理员不发短信或者未打开短信发送开关ChangedSMSCells，document的ID为" + p.DataID.ToString() + "");
                                }
                                #endregion
                            }
                        }
                        else
                        {
                            logger.Error("ChangedSMSCells dtJL Is null or dtJL.Rows.Count==0!DataID:" + p.DataID.ToString() + " RequestID:" + p.RequestID);
                            return;
                        }
                    }
                    else
                    {
                        logger.Error("ChangedSMSCells RequestID Is null!DataID为" + p.DataID.ToString() + "");
                        return;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                logger.Error("ChangedSMSCells error!DataID为" + p.DataID.ToString() + " ex:" + ex.ToString());
            }
        }

        private String GetSMSContent(Guid dataID, List<CellChangedNotifyItem> items, String userName, Guid requestID, DataTable dtJL)
        {
            try
            {
                String sql = String.Format(@"
            SELECT c.工程名称 as 工程,
            c.标段名称 as 标段,
            c.单位名称 as 单位,
            c.试验室名称 as 试验室,b.Name AS ReportName FROM dbo.sys_document a
            JOIN dbo.sys_module b ON a.ModuleID = b.ID
            JOIN dbo.v_bs_codeName c ON a.TestRoomCode=c.试验室编码
            WHERE a.ID='{0}'", dataID);

                DataTable SmsInfo = GetDataTable(sql);
                //sql = string.Format("SELECT ApprovePerson,ApproveTime FROM dbo.sys_request_change WHERE id='{0}'", requestID);
                //DataTable dtJL = GetDataTable(sql);
                String text = "";
                if (SmsInfo != null && SmsInfo.Rows.Count > 0 && dtJL != null && dtJL.Rows.Count > 0)
                {
                    DataRow row = SmsInfo.Rows[0];
                    DataRow rowJL = dtJL.Rows[0];
                    DateTime dtApproveTime = new DateTime();
                    if (DateTime.TryParse(rowJL["ApproveTime"].ToString(), out dtApproveTime))
                    {
                        //logger.Error("str:1");
                        String str = string.Format("{0}:{6}监理{5}通过申请,{1}于{2}将{3}的{4}的", row["工程"], userName, DateTime.Now.ToString("yyyy-MM-dd HH:mm"), row["标段"].ToString() + row["单位"].ToString() + row["试验室"].ToString(), row["ReportName"], rowJL["ApprovePerson"], dtApproveTime.ToString("yyyy-MM-dd HH:mm"));
                        //logger.Error("str:" + str);
                        // row["工程"].ToString() + "：" + userName + "于" + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分") +"将" + row["标段"].ToString() + row["单位"].ToString() + row["试验室"].ToString() + "的" +row["ReportName"].ToString() + @"的";
                        foreach (var item in items)
                        {
                            text += item.Description + "从" + item.OriginalValue + "改为" + item.CurrentValue + ";";
                        }
                        str = str + text;
                        return str;
                    }
                    else
                    {
                        logger.Error(string.Format("GetSMSContent ApproveTime error.dataID:{0} requestID:{1} ApproveTime:{2}", dataID, requestID, row["ApproveTime"]));
                    }
                }
                else
                {
                    logger.Error(string.Format("GetSMSContent SmsInfo or dtJL is null or Count is 0.dataID:{0} requestID:{1}", dataID, requestID));
                }

            }
            catch (Exception ex)
            {
                logger.Error(string.Format("GetSMSContent error.dataID:{0} requestID:{1} error:{2}", dataID, requestID, ex.Message));
            }

            return "";
        }

        private String[] GetSMSCells()
        {
            String phone = ConfigurationManager.AppSettings["ChangedSMSCells"];
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

        //注释by Tan in 20140911
        //private Guid GetLastRequestID(Guid dataID)
        //{
        //    String sql = "SELECT TOP 1 ID FROM dbo.sys_request_change WHERE DataID='" + dataID + "' ORDER BY RequestTime DESC";
        //    DataTable dt = GetDataTable(sql);
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        return new Guid(dt.Rows[0][0].ToString());
        //    }
        //    return Guid.Empty;
        //}



        private class ThreadParameter
        {
            public Guid DataID;
            public List<CellChangedNotifyItem> Items;
            public Boolean IsAdministrator;
            public String UserName;
            public Guid RequestID;
            public string LineID;
            public string TestRoomCode;
            public string SegmentName;
            public string CompanyName;
            public string TestRoomName;
            public string LineName;
        }
    }
}
