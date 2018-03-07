using System;
using System.Collections.Generic;
using Quartz;
using System.Data;
using Yqun.Data.DataBase;
using System.Configuration;

namespace Yqun.BO.ReminderManager
{
    public class StadiumSMSJob : BOBase, IJob
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        LabStadiumListManager labstadiumlistManager = new LabStadiumListManager();

        public void Execute(IJobExecutionContext context)
        {
            if (StartSMS())
            {
                logger.Info("开始执行龄期短信提醒...");

                //  增加查询条件 Scdel=0  2013-10-17
                String sql = @"select NodeCode from sys_engs_Tree
                where Scdel=0 and NodeType = '@folder' and NodeCode = '0001000100010002'";

                DataTable TestRoomData = GetDataTable(sql);
                if (TestRoomData != null)
                {
                    string[] cells = GetStadiumSMSCells();
                    if (cells.Length == 0)
                    {
                        logger.Error("获取龄期短信收件人电话失败，未设置StadiumSMSCells");
                        return;
                    }

                    foreach (DataRow Row in TestRoomData.Rows)
                    {
                        String code = Row["NodeCode"].ToString();
                        DataTable stadiumData = labstadiumlistManager.GetLabStaidumSMSList(code);
                        if (stadiumData != null && stadiumData.Rows.Count > 0)
                        {
                            logger.Info("发现" + stadiumData.Rows.Count + "条试验龄期信息");

                            String prevtestItem = "";
                            String smsContent = "试验提醒:";
                            List<String> Lines = new List<String>();
                            foreach (DataRow stadiumRow in stadiumData.Rows)
                            {
                                string wtbh = stadiumRow["委托编号"].ToString();
                                string testItem = stadiumRow["试验项目"].ToString();

                                if (wtbh.Trim() == "")
                                    continue;

                                string partwtbh = wtbh;
                                int index = wtbh.IndexOf(DateTime.Now.Year.ToString());
                                if (index != -1)
                                    partwtbh = "尾号" + wtbh.Substring(index);

                                if (testItem != prevtestItem)
                                {
                                    DataRow[] rows = stadiumData.Select("试验项目='" + testItem + "'");
                                    smsContent = smsContent + "\r\n" + testItem.Trim() + "(" + rows.Length + ")";
                                    prevtestItem = testItem;
                                }

                                smsContent = smsContent + "\r\n" + partwtbh + ",";
                            }

                            smsContent = smsContent.Trim(',') + "\r\n" + DateTime.Now;

                            logger.Info(smsContent);

                            var errorMsg = "";
                            String subContent = smsContent;

                            int i = 1;
                            while (subContent.Length > 70)
                            {
                                string content = subContent.Substring(0, 70);
                                errorMsg += SMSAgent.CallRemoteService(cells, content);
                                subContent = subContent.Replace(content, "");
                                i++;
                            }
                            if (subContent.Length > 0)
                            {
                                errorMsg += SMSAgent.CallRemoteService(cells, subContent);
                            }
                            RecordSMSItem(code, cells, smsContent, errorMsg);
                        }
                    }

                    logger.Info("发送龄期短信提醒完毕");
                }
                else
                {
                    logger.Info("获取试验室编码列表失败");
                }
            }
        }

        private Boolean RecordSMSItem(String DataIndex, String[] cellnumbers, String content, String sentStatus)
        {
            Boolean Result = false;
            IDbConnection DbConnection = GetConntion();
            Transaction Transaction = new Transaction(DbConnection);

            try
            {
                List<string> sql_Commands = new List<string>();

                //插入短信日志表
                foreach (var item in cellnumbers)
                {
                    sql_Commands.Add(String.Format("INSERT INTO dbo.sys_sms_stadium_log( SMSPhone ,SMSContent ,SentError ,RelationID) VALUES  ('{0}', '{1}', '{2}', '{3}')", item, content, sentStatus, DataIndex));
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

        private String[] GetStadiumSMSCells()
        {
            String phone = ConfigurationManager.AppSettings["StadiumSMSCells"];
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

        private Boolean StartSMS()
        {
            String start = ConfigurationManager.AppSettings["SMSStart"];
            if (start == "1")
            {
                return true;
            }
            return false;
        }
    }
}
