using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Yqun.Services;

namespace ReportComponents
{
    public class DepositoryRTReportConfig
    {
        public static List<String> GetRTReports(String GroupName)
        {
            List<String> Reports = new List<String>();
            //增加查询条件 Scdel=0  判断数据是否已删除  2013-10-17
            StringBuilder sql_Select = new StringBuilder();
            sql_Select.Append("select ReportName from sys_biz_ReportRTConfig where Scdel=0 and GroupName='");
            sql_Select.Append(GroupName);
            sql_Select.Append("'");

            DataTable Data = Agent.CallService("Yqun.BO.ReportManager.dll", "GetDataTable", new object[] { sql_Select.ToString() }) as DataTable;
            if (Data != null)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    String ReportName = Row["ReportName"].ToString();
                    Reports.Add(ReportName);
                }
            }

            return Reports;
        }
    }
}
