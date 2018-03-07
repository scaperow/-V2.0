using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BizCommon;
using System.Threading;
using Yqun.Common.ContextCache;

namespace Yqun.BO.ReminderManager
{
    public class DataReportVerification : BOBase
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        EvaluateConditionManager Manager = new EvaluateConditionManager();
        ItemConditionManager ItemConditionManager = new ItemConditionManager();

        /// <summary>
        /// 验证试验报告中每个试验项目是否合格
        /// </summary>
        public void Evaluation(List<string> IDList, String ModelIndex, DataSet Data)
        {
            if (IDList.Count == 0)
                return;

            ThreadParameter p = new ThreadParameter();
            p.ModelIndex = ModelIndex;
            p.Data = Data;
            p.IsAdministrator = ApplicationContext.Current.IsAdministrator;

            ThreadPool.QueueUserWorkItem(new WaitCallback(Execute), p);
        }

        private void Execute(object paremeter)
        {
            Boolean Result;

            ThreadParameter p = paremeter as ThreadParameter;
            String ModelIndex = p.ModelIndex;
            DataSet Data = p.Data;
            StringBuilder sql_Select = new StringBuilder();
            // 增加查询条件 Scdel=0     2013-10-19
            sql_Select.Append("Select Description, Sheets from sys_biz_Module where Scdel=0 and ID ='");
            sql_Select.Append(ModelIndex);
            sql_Select.Append("'");

            DataTable Table = GetDataTable(sql_Select.ToString());
            if (Table != null && Table.Rows.Count > 0)
            {
                DataRow Row = Table.Rows[0];
                String Sheets = Row["Sheets"].ToString();
                String[] SheetArray = Sheets.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (SheetArray.Length > 0)
                {
                    String ReportIndex = SheetArray[SheetArray.Length - 1];
                    ReportEvaluateCondition Evaluate = Manager.InitEvaluateConditions(ModelIndex, ReportIndex);
                    if (Evaluate != null)
                    {
                        DataTable ExtentData = Data.Tables[string.Format("[biz_norm_extent_{0}]", ModelIndex)];
                        String DataID = ExtentData.Rows[0]["ID"].ToString();
                        String SCTS = ExtentData.Rows[0]["SCTS"].ToString();
                        String SCPT = ExtentData.Rows[0]["SCPT"].ToString();
                        String ReportName = Row["Description"].ToString();

                        String ReportNumber = "";
                        if (Evaluate.ReportNumber != "")
                        {
                            String[] keywords = Evaluate.ReportNumber.Trim('{', '}').Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                            DataTable TempData = Data.Tables[string.Format("[{0}]", keywords[0].Trim())];
                            ReportNumber = TempData.Rows[0][keywords[1].Trim()].ToString();
                        }
                        else
                        {
                            logger.Error(string.Format("试验模板‘{0}’没有配置报告编号",Evaluate.Description));
                        }

                        String ReportDate = "";
                        if (Evaluate.ReportDate != "")
                        {
                            String[] keywords = Evaluate.ReportDate.Trim('{', '}').Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                            DataTable TempData = Data.Tables[string.Format("[{0}]", keywords[0].Trim())];
                            ReportDate = TempData.Rows[0][keywords[1].Trim()].ToString();
                        }
                        else
                        {
                            logger.Error(string.Format("试验模板‘{0}’没有配置报告日期", Evaluate.Description));
                        }

                        List<string> NotStandardItems = new List<string>();
                        foreach (ItemCondition Item in Evaluate.Items)
                        {
                            String Specifiedvalue = Calculator.CalcCondition(Item.Specifiedvalue, Data);
                            if (string.Compare(Calculator.CalcQualified(Item.Expression, Data), Boolean.TrueString, true) != 0 && Specifiedvalue != "")
                            {
                                string[] words = Item.TrueValue.Trim('{','}').Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                                DataTable TableData = Data.Tables[string.Format("[{0}]", words[0].Trim())];
                                object value = TableData.Rows[0][words[1].Trim()];
                                
                                //logger.Error("Item.Specifiedvalue:" + Item.Specifiedvalue);
                                Item.Specifiedvalue = Specifiedvalue;
                                //logger.Error("Item.Specifiedvalue:" + Specifiedvalue);

                                NotStandardItems.Add(string.Format("{0},{1},{2}", Item.Text, Item.Specifiedvalue, value));
                            }
                        }
                        
                        if (NotStandardItems.Count > 0)
                        {
                            Boolean needSendSMS = false;
                            SMSManager smsManager = new SMSManager();
                            if (!p.IsAdministrator)
                            {
                                needSendSMS = smsManager.NeedSendSMS(DataID, string.Join("||", NotStandardItems.ToArray()));
                            }
                            
                            Result = ItemConditionManager.UpdateItemConditions(DataID, SCTS, SCPT, ModelIndex, ReportName, ReportNumber, ReportDate, string.Join("||", NotStandardItems.ToArray()));
                            if (needSendSMS)
                            {
                                //smsManager.SendSMS(DataID, string.Join("||", NotStandardItems.ToArray()));
                            }
                        }
                        else
                        {
                            //判断并修改AdditionalQualified标记
                            ItemConditionManager.QualifyInvalidReport(DataID);
                        }
                    }
                }
            }

            try
            {
                if (ModelIndex != TestRoomBasicInformation.拌和站基本情况登记表 &&
                    ModelIndex != TestRoomBasicInformation.试验人员技术档案 &&
                    ModelIndex != TestRoomBasicInformation.试验室仪器设备汇总表 &&
                    ModelIndex != TestRoomBasicInformation.试验室综合情况登记表)
                {
                    String tablename = string.Format("[biz_norm_extent_{0}]", ModelIndex);
                    DataTable ExtentData = Data.Tables[tablename];
                    String DataID = ExtentData.Rows[0]["ID"].ToString();
                    String sql = String.Format("select ID from sys_biz_reminder_evaluateData where ID='{0}' and AdditionalQualified=0", DataID);
                    DataTable data = GetDataTable(sql);
                    int v = (data != null && data.Rows.Count > 0 ? 0 : 1);
                    sql = "update " + tablename + " set IsQualified = " + v + " where ID='" + DataID + "'";
                    int r = ExcuteCommand(sql);
                    //logger.Error("更新合格字段IsQualified的返回值为" + r);
                }
            }
            catch (Exception ex)
            {
                logger.Error("更新合格字段IsQualified失败，原因是" + ex.Message);
            }
        }

        private class ThreadParameter
        {
            public String ModelIndex;
            public DataSet Data;
            public Boolean IsAdministrator;
        }
    }
}
