using System;
using System.Collections.Generic;
using System.Text;
using ReportCommon;
using System.Data;
using FarPoint.Win;
using FarPoint.Win.Spread.CellType;
using System.Reflection;

namespace Yqun.BO.ReportManager
{
    public class ReportParameterManager : BOBase
    {
        public List<ReportParameter> getReportParameters(String ReportIndex)
        {
            List<ReportParameter> reportParameters = new List<ReportParameter>();
            if (ReportIndex != "")
            {
                //增加查询条件 Scdel=0  判断数据是否已删除  2013-10-17
                StringBuilder sql_select = new StringBuilder();
                sql_select.Append("select * from sys_biz_ReportParameters where Scdel=0 and ReportID='");
                sql_select.Append(ReportIndex);
                sql_select.Append("' ");
                sql_select.Append("order by ReportID, scts");

                DataTable Data = GetDataTable(sql_select.ToString());
                if (Data != null)
                {
                    foreach (DataRow Row in Data.Rows)
                    {
                        ReportParameter parameter = new ReportParameter();
                        parameter.Index = Row["ID"].ToString();
                        parameter.ReportIndex = Row["ReportID"].ToString();
                        parameter.Name = Row["Name"].ToString();
                        parameter.DisplayName = Row["DisplayName"].ToString();

                        reportParameters.Add(parameter);
                    }
                }
            }

            return reportParameters;
        }

        public Boolean saveReportParameter(String ReportIndex, List<ReportParameter> Parameters)
        {
            //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，用于数据同步     2013-10-17
            StringBuilder sql_delete = new StringBuilder();
            //sql_delete.Append("delete from sys_biz_ReportParameters where ReportID = '");
            //sql_delete.Append(ReportIndex);
            //sql_delete.Append("'");
            sql_delete.Append("Update sys_biz_ReportParameters Set Scts_1=Getdate(),Scdel=1 where ReportID = '");
            sql_delete.Append(ReportIndex);
            sql_delete.Append("'");

            bool result = false;
            object r;
            try
            {
                r = ExcuteCommand(sql_delete.ToString());
                result = (Convert.ToInt32(r) == 1);
            }
            catch
            {
            }

            //增加查询条件 Scdel=0  判断数据是否已删除  2013-10-17
            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select * from sys_biz_ReportParameters where Scdel=0 and ReportID='");
            sql_select.Append(ReportIndex);
            sql_select.Append("'");

            DataTable Data = GetDataTable(sql_select.ToString());
            foreach (ReportParameter parameter in Parameters)
            {
                DataRow Row = Data.NewRow();
                Row["ID"] = parameter.Index;
                Row["ReportID"] = parameter.ReportIndex;
                Row["Name"] = parameter.Name;
                Row["DisplayName"] = parameter.DisplayName;

                Row["Scts_1"] = DateTime.Now.ToString();

                Data.Rows.Add(Row);
            }

            try
            {
                r = Update(Data);
                result = result & (Convert.ToInt32(r) == 1);
            }
            catch
            {
            }

            return result;
        }
    }
}
