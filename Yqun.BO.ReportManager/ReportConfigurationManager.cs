using System;
using System.Collections.Generic;
using System.Text;
using ReportCommon;
using System.Data;
using System.Xml;
using System.Reflection;
using System.IO;
using Yqun.Bases;

namespace Yqun.BO.ReportManager
{
    public class ReportConfigurationManager : BOBase
    {
        ReportParameterManager ReportParameterManager = new ReportParameterManager();

        public ReportConfiguration InitReportConfiguration(String Index)
        {
            ReportConfiguration ReportConfiguration = new ReportConfiguration();

            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件 Scdel=0  2013-10-17
            Sql_Select.Append("Select ID,CatlogCode,Description,SheetStyle,DataSources From sys_biz_ReportSheet Where Scdel=0 and ID ='");
            Sql_Select.Append(Index);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                DataRow Row = Data.Rows[0];
                ReportConfiguration.Index = Row["ID"].ToString();
                ReportConfiguration.Code = Row["CatlogCode"].ToString();
                ReportConfiguration.Description = Row["Description"].ToString();
                ReportConfiguration.SheetStyle = Row["SheetStyle"].ToString();

                TableDataCollection Sources = BinarySerializer.Deserialize(Row["DataSources"].ToString()) as TableDataCollection;
                if (Sources != null)
                {
                    foreach (TableData Source in Sources)
                    {
                        ReportConfiguration.DataSources.Add(Source);
                    }
                }

                ReportConfiguration.ReportParameters = ReportParameterManager.getReportParameters(ReportConfiguration.Index);
            }

            return ReportConfiguration;
        }

        public String GetReportIndex(String Descriptor)
        {
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件 Scdel=0  2013-10-17
            Sql_Select.Append("Select ID From sys_report Where IsActive=1 and Description ='");
            Sql_Select.Append(Descriptor);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                return Data.Rows[0]["ID"].ToString();
            }

            return string.Empty;
        }

        public Boolean HaveReportConfiguration(string Index)
        {
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件 Scdel=0  2013-10-17
            Sql_Select.Append("Select ID From sys_biz_ReportSheet Where Scdel=0 and ID ='");
            Sql_Select.Append(Index);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            return (Data != null && Data.Rows.Count > 0);
        }

        public bool NewReportConfiguration(ReportConfiguration Report)
        {
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件 Scdel=0  2013-10-17
            Sql_Select.Append("Select ID,SCTS,CatlogCode,Description,SheetStyle,DataSources From sys_biz_ReportSheet Where Scdel=0 and ID ='");
            Sql_Select.Append(Report.Index);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null)
            {
                DataRow Row = Data.NewRow();
                Row["ID"] = Report.Index;
                Row["SCTS"] = DateTime.Now.ToString();
                Row["CatlogCode"] = Report.Code;
                Row["Description"] = Report.Description;
                Row["SheetStyle"] = Report.SheetStyle;
                Row["DataSources"] = BinarySerializer.Serialize(Report.DataSources);
                Data.Rows.Add(Row);
            }

            Boolean Result = false;

            try
            {
                object r = Update(Data);
                Result = (System.Convert.ToInt32(r) == 1);
            }
            catch
            { }

            return Result;
        }

        public bool UpdateReportConfiguration(ReportConfiguration Report)
        {
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件 Scdel=0  2013-10-17
            Sql_Select.Append("Select ID,SCTS,CatlogCode,Description,SheetStyle,DataSources From sys_biz_ReportSheet Where Scdel=0 and ID ='");
            Sql_Select.Append(Report.Index);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null)
            {
                DataRow Row;
                if (Data.Rows.Count > 0)
                {
                    Row = Data.Rows[0];
                }
                else
                {
                    Row = Data.NewRow();
                    Data.Rows.Add(Row);
                }

                Row["ID"] = Report.Index;
                Row["SCTS"] = DateTime.Now.ToString();
                Row["CatlogCode"] = Report.Code;
                Row["Description"] = Report.Description;
                Row["SheetStyle"] = Report.SheetStyle;
                Row["DataSources"] = BinarySerializer.Serialize(Report.DataSources);
            }

            Boolean Result = false;

            try
            {
                object r = Update(Data);
                Result = (System.Convert.ToInt32(r) == 1);
            }
            catch
            { }

            return Result;
        }

        public bool UpdateReportName(String ReportIndex, String Description)
        {
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件 Scdel=0  2013-10-17
            Sql_Select.Append("Select ID,SCTS,Description From sys_biz_ReportSheet Where Scdel=0 and ID ='");
            Sql_Select.Append(ReportIndex);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null)
            {
                DataRow Row;
                if (Data.Rows.Count > 0)
                {
                    Row = Data.Rows[0];
                }
                else
                {
                    Row = Data.NewRow();
                    Data.Rows.Add(Row);
                }

                Row["SCTS"] = DateTime.Now.ToString();
                Row["Description"] = Description;
            }

            Boolean Result = false;

            try
            {
                object r = Update(Data);
                Result = (System.Convert.ToInt32(r) == 1);
            }
            catch
            { }

            return Result;
        }

        public bool DeleteReportConfiguration(ReportConfiguration Report)
        {
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件 Scdel=0  2013-10-17
            Sql_Select.Append("Select ID,CatlogCode,Description,SheetStyle,DataSources From sys_biz_ReportSheet Where Scdel=0 and ID ='");
            Sql_Select.Append(Report.Index);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    Row.Delete();
                }
            }

            Boolean Result = false;

            try
            {
                object r = Update(Data);
                Result = (System.Convert.ToInt32(r) == 1);
            }
            catch
            { }

            return Result;
        }

        public bool DeleteReportConfiguration(String ReportIndex)
        {
            StringBuilder Sql_Delete = new StringBuilder();
            //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，用于数据同步     2013-10-17
            //Sql_Delete.Append("Delete From sys_biz_ReportSheet Where ID ='");
            //Sql_Delete.Append(ReportIndex);
            //Sql_Delete.Append("'");
            Sql_Delete.Append("Update sys_biz_ReportSheet Set Scts_1=Getdate(),Scdel=1 Where ID ='");
            Sql_Delete.Append(ReportIndex);
            Sql_Delete.Append("'");

            Boolean Result = false;

            try
            {
                object r = ExcuteCommand(Sql_Delete.ToString());
                Result = (System.Convert.ToInt32(r) == 1);
            }
            catch
            { }

            return Result;
        }

        public Boolean IsUpdatable(String Index, Object scts)
        {
            Object Scts = DBNull.Value;

            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件 Scdel=0  2013-10-17
            Sql_Select.Append("Select scts from sys_biz_ReportSheet where Scdel=0 and ID='");
            Sql_Select.Append(Index);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                DataRow Row = Data.Rows[0];
                Scts = Row["scts"];
            }

            if (scts == DBNull.Value)
                return true;
            else if (Scts == DBNull.Value)
                return false;
            else
                return Convert.ToDateTime(Scts) > Convert.ToDateTime(scts);
        }
    }
}
