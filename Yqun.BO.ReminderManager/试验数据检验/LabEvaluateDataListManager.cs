using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Yqun.BO.QualificationManager;
using Yqun.Common.ContextCache;
using Yqun.Permissions.Runtime;
using Yqun.Permissions.Common;
using System.IO;

namespace Yqun.BO.ReminderManager
{
    public class LabEvaluateDataListManager : BOBase
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        List<MemoryStream>  _ListImg=new List<MemoryStream>();

        QualificationAuthManager QualificationManager = new QualificationAuthManager();

        /// <summary>
        /// 过时了，不使用
        /// </summary>
        /// <param name="IsAdministrator"></param>
        /// <param name="CompanyIndex"></param>
        /// <param name="CompanyType"></param>
        /// <returns></returns>
        public List<string> GetTestRoomList(Boolean IsAdministrator, String CompanyIndex, String CompanyType)
        {
            List<string> Result = new List<string>();

            if (CompanyType == "@unit_施工单位")
                return Result;

            StringBuilder sql_select = new StringBuilder();
            //  增加查询条件 Scdel=0  2013-10-17
            sql_select.Append("select NodeCode from sys_engs_Tree where Scdel=0 and RalationID in (");

            if (!string.IsNullOrEmpty(CompanyIndex) && CompanyType == "@unit_监理单位")
            {
                List<String> CompanyList = new List<string>();

                StringBuilder sql_subquery = new StringBuilder();
                //增加查询条件  Scdel=0     2013-10-17
                sql_subquery.Append("select ConstructionCompany from sys_engs_CompanyInfo where Scdel=0 and ID = '");
                sql_subquery.Append(CompanyIndex);
                sql_subquery.Append("'");

                DataTable TableData = GetDataTable(sql_subquery.ToString());
                if (TableData != null)
                {
                    foreach (DataRow Row in TableData.Rows)
                    {
                        String list = Row["ConstructionCompany"].ToString();
                        CompanyList.AddRange(list.Split(','));
                    }
                }

                sql_select.Append(string.Concat("'", string.Join("','", CompanyList.ToArray()), "'"));
            }
            else if (CompanyType == "@unit_建设单位" || IsAdministrator || CompanyIndex == "")
            {
                //增加查询条件  Scdel=0     2013-10-17
                sql_select.Append("select ID from sys_engs_CompanyInfo where Scdel=0 and DepType = '@unit_监理单位' or DepType = '@unit_施工单位'");
            }

            sql_select.Append(")");

            List<string> Filters = new List<string>();
            DataTable CompanyData = GetDataTable(sql_select.ToString());
            if (CompanyData != null)
            {
                foreach (DataRow Row in CompanyData.Rows)
                {
                    String NodeCode = Row["NodeCode"].ToString();
                    Filters.Add(string.Format("NodeCode like '{0}____'", NodeCode));
                }
            }

            if (Filters.Count > 0)
            {
                StringBuilder sql_filters = new StringBuilder();
                sql_filters.Append("select NodeCode from sys_engs_Tree where ");
                sql_filters.Append(string.Join(" or ", Filters.ToArray()));

                DataTable Data = GetDataTable(sql_filters.ToString());
                if (Data != null)
                {
                    foreach (DataRow Row in Data.Rows)
                    {
                        String NodeCode = Row["NodeCode"].ToString();
                        Result.Add(NodeCode);
                    }
                }
            }

            return Result;
        }

        public DataTable GetEvaluateReminderInfos(String[] TestRoomCode, DateTime Start, DateTime End)
        {
            return GetEvaluateReminderInfos(TestRoomCode, "", "", Start, End, "");
        }

        public DataTable GetEvaluateReminderInfos(String[] TestRoomCode, String sReportName, String sReportNumber, DateTime Start, DateTime End, String sTestItem)
        {
            DataTable DataList = new DataTable();
            DataList.Columns.Add("ID", typeof(string));
            DataList.Columns.Add("DataIndex", typeof(string));
            DataList.Columns.Add("ModelCode", typeof(string));
            DataList.Columns.Add("ModelIndex", typeof(string));
            DataList.Columns.Add("标段", typeof(string));
            DataList.Columns.Add("单位", typeof(string));
            DataList.Columns.Add("试验室", typeof(string));
            DataList.Columns.Add("试验报告", typeof(string));
            DataList.Columns.Add("报告编号", typeof(string));
            DataList.Columns.Add("报告日期", typeof(string));
            DataList.Columns.Add("不合格项目", typeof(string));
            DataList.Columns.Add("标准规定值", typeof(string));
            DataList.Columns.Add("实测值", typeof(string));
            DataList.Columns.Add("原因分析", typeof(string));
            DataList.Columns.Add("监理意见", typeof(string));

            //141行  增加查询条件 Scdel=0  2013-10-17
            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select ");
            sql_select.Append("a.ID,");
            sql_select.Append("a.ModelCode,");
            sql_select.Append("a.ModelIndex,");
            sql_select.Append("c.Description as SectionName,");
            sql_select.Append("b.Description as CompanyName,");
            sql_select.Append("d.Description as TestRoomName,");
            sql_select.Append("a.ReportName,");
            sql_select.Append("a.ReportNumber,");
            sql_select.Append("a.ReportDate,");
            sql_select.Append("a.F_InvalidItem, ");
               sql_select.Append("a.SGComment, ");
               sql_select.Append("a.JLComment ");
            sql_select.Append("from ");
            sql_select.Append("sys_biz_reminder_evaluateData as a,");
            sql_select.Append("(select a.NodeCode,b.Description from sys_engs_Tree as a,sys_engs_CompanyInfo as b where a.Scdel=0 and a.RalationID = b.ID) as b,");
            sql_select.Append("(select a.NodeCode,b.Description from sys_engs_Tree as a,sys_engs_SectionInfo as b where a.RalationID = b.ID) as c,");
            sql_select.Append("(select a.NodeCode,b.Description from sys_engs_Tree as a,sys_engs_ItemInfo as b where a.RalationID = b.ID) as d  ");
            sql_select.Append("where b.NodeCode = substring(a.ModelCode,0,Len(a.ModelCode)-7) and ");
            sql_select.Append("      c.NodeCode = substring(a.ModelCode,0,Len(a.ModelCode)-11) and");
            sql_select.Append("      d.NodeCode = substring(a.ModelCode,0,Len(a.ModelCode)-3) and (");
            sql_select.Append("a.ReportDate >= '");
            sql_select.Append(Start.ToShortDateString());
            sql_select.Append("' and a.ReportDate <= '");
            sql_select.Append(End.AddDays(1).AddSeconds(-1).ToString());
            sql_select.Append("') and AdditionalQualified=0 ");

            if (sReportName != "")
            {
                sql_select.Append(" and a.ReportName like '%");
                sql_select.Append(sReportName);
                sql_select.Append("%' ");
            }

            if (sReportNumber != "")
            {
                sql_select.Append(" and a.ReportNumber like '%");
                sql_select.Append(sReportNumber);
                sql_select.Append("%' ");
            }

            if (sTestItem != "")
            {
                sql_select.Append(" and a.F_InvalidItem like '%");
                sql_select.Append(sTestItem);
                sql_select.Append("%' ");
            }

            if (TestRoomCode != null && TestRoomCode.Length > 0)
            {
                List<string> subExpressions = new List<string>();
                foreach (String code in TestRoomCode)
                {
                    subExpressions.Add(string.Format("a.ModelCode like '{0}%'", code));
                }
                sql_select.Append(string.Concat(" and (", string.Join(" or ", subExpressions.ToArray()), ")"));
            }
           

            ApplicationContext AppContext = ApplicationContext.Current;
            if (!AppContext.IsAdministrator)
            {
                sql_select.Append(" and a.F_InvalidItem NOT LIKE '%#VALUE%' ");

                //  增加查询条件 Scdel=0  2013-10-17
                IAuthPolicy AuthPolicy = AuthManager.GetTreeAuth("6ED9D9CB-117E-4d8c-A63B-0157BD1F9DFD");
                DataTable Data2 = GetDataTable("SELECT NodeCode FROM dbo.sys_engs_Tree WHERE Scdel=0 and NodeType='@folder'");
                List<String> real2 = new List<string>();
                if (Data2 != null)
                {
                    foreach (DataRow Row in Data2.Rows)
                    {
                        String Code = Row["NodeCode"].ToString();
                        if (AuthPolicy.HasAuth(Code))
                        {
                            real2.Add(Code);
                        }
                    }
                }
                sql_select.Append(" AND LEFT(a.ModelCode,16) in ('" + String.Join("','", real2.ToArray()) + "') ");

            }
            logger.Error("不合格报告： "+sql_select.ToString());
            DataTable Data = GetDataTable(sql_select.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    String ID = Row["ID"].ToString();
                    String ModelCode = Row["ModelCode"].ToString();
                    String ModelIndex = Row["ModelIndex"].ToString();
                    String SectionName = Row["SectionName"].ToString();
                    String CompanyName = Row["CompanyName"].ToString();
                    String TestRoomName = Row["TestRoomName"].ToString();
                    String ReportName = Row["ReportName"].ToString();
                    String ReportNumber = Row["ReportNumber"].ToString();
                    String ReportDate = Row["ReportDate"].ToString();
                    String InvalidItem = Row["F_InvalidItem"].ToString();
                    String SGComment = Row["SGComment"].ToString();
                    String JLComment = Row["JLComment"].ToString();

                    String[] Items = InvalidItem.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (String Item in Items)
                    {
                        String[] substrings = Item.Split(',');
                        if (sTestItem != "")
                        {
                            if (!substrings[0].Contains(sTestItem))
                            {
                                continue;
                            }
                        }
                        DataRow ItemRow = DataList.NewRow();
                        ItemRow["ID"] = Guid.NewGuid().ToString();
                        ItemRow["DataIndex"] = ID;
                        ItemRow["ModelCode"] = ModelCode;
                        ItemRow["ModelIndex"] = ModelIndex;
                        ItemRow["标段"] = SectionName;
                        ItemRow["单位"] = CompanyName;
                        ItemRow["试验室"] = TestRoomName;
                        ItemRow["试验报告"] = ReportName;
                        ItemRow["报告编号"] = ReportNumber;
                        ItemRow["报告日期"] = ReportDate;
                        ItemRow["不合格项目"] = substrings[0];
                        ItemRow["标准规定值"] = substrings[1];
                        ItemRow["实测值"] = substrings[2];
                        ItemRow["原因分析"] = SGComment;
                        ItemRow["监理意见"] = JLComment;
                        DataList.Rows.Add(ItemRow);
                    }
                }
            }

            return DataList;
        }

        public Boolean HasLabEvaluateDataList(String[] TestRoomCode, DateTime Start, DateTime End)
        {
            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select count(*) from sys_biz_reminder_evaluateData where SCTS >= '");
            sql_select.Append(Start.ToShortDateString());
            sql_select.Append("' and SCTS <= '");
            sql_select.Append(End.AddDays(1).AddSeconds(-1).ToString());
            sql_select.Append("' and AdditionalQualified=0 ");

            List<string> subExpressions = new List<string>();
            foreach (String code in TestRoomCode)
            {
                subExpressions.Add(string.Format("ModelCode like '{0}____'", code));
            }
            sql_select.Append(string.Concat(" and (", string.Join(" or ", subExpressions.ToArray()), ")"));

            object Count = ExcuteScalar(sql_select.ToString());
            return Convert.ToInt32(Count) > 0;
        }
    }
}
