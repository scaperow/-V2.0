using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Yqun.BO.ReportManager
{
    public class ReportCatlogManager : BOBase
    {
        public DataTable InitReportCatlog()
        {
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件 Scdel=0  2013-10-17
            Sql_Select.Append("Select ID,CatlogCode,Description,'true' as IsReport from sys_biz_ReportSheet  where Scdel=0");
            Sql_Select.Append(" Union ");
            Sql_Select.Append("Select ID,CatlogCode,CatlogName as Description,'false' as IsReport from sys_biz_ReportCatlog order by CatlogCode");

            return GetDataTable(Sql_Select.ToString());
        }

        public string GetReportIndex(string ReportCode)
        {
            string ReportIndex = "";

            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件 Scdel=0  2013-10-17
            Sql_Select.Append("Select ID from sys_biz_ReportSheet where Scdel=0 and CatlogCode='");
            Sql_Select.Append(ReportCode);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                ReportIndex = Data.Rows[0]["ID"].ToString();
            }

            return ReportIndex;
        }

        public string GetNewReportName(string FolderCode, string ReportName)
        {
            string tempReportName = ReportName;
            Boolean Have = true;
            int Index = 1;

            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件 Scdel=0  2013-10-17
            Sql_Select.Append("Select Description from sys_biz_ReportSheet where Scdel=0 and CatlogCode like '");
            Sql_Select.Append(FolderCode);
            Sql_Select.Append("_%'");

            DataTable Data = GetDataTable(Sql_Select.ToString()); 
            if (Data != null && Data.Rows.Count > 0)
            {
                while (Have)
                {
                    Have = false;
                    foreach (DataRow row in Data.Rows)
                    {
                        if (tempReportName == row["Description"].ToString())
                        {
                            Have = true;
                            break;
                        }
                    }

                    if (Have)
                    {
                        tempReportName = ReportName + "_" + (Index++).ToString();
                    }
                }
            }

            return tempReportName;
        }

        public string GetNewFolderName(string FolderCode, string FolderName)
        {
            string tempSheetName = FolderName;
            Boolean Have = true;
            int Index = 1;

            StringBuilder Sql_Select = new StringBuilder();
            Sql_Select.Append("Select CatlogName from sys_biz_ReportCatlog where CatlogCode like '");
            Sql_Select.Append(FolderCode);
            Sql_Select.Append("_%'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                while (Have)
                {
                    Have = false;
                    foreach (DataRow row in Data.Rows)
                    {
                        if (tempSheetName == row["CatlogName"].ToString())
                        {
                            Have = true;
                            break;
                        }
                    }

                    if (Have)
                    {
                        tempSheetName = FolderName + "_" + (Index++).ToString();
                    }
                }
            }

            return tempSheetName;
        }

        public string GetNextReportCatlogCode(string ParentCode)
        {
            List<int> Values = new List<int>();
            StringBuilder Sql_Select = new StringBuilder();
            Sql_Select.Append("Select CatlogCode from sys_biz_ReportCatlog where CatlogCode like '");
            Sql_Select.Append(ParentCode);
            Sql_Select.Append("____'");

            DataTable Data = GetDataTable(Sql_Select.ToString()) as DataTable;
            if (Data != null && Data.Rows.Count > 0)
            {
                foreach (DataRow row in Data.Rows)
                {
                    string Value = row["CatlogCode"].ToString();
                    int Int = int.Parse(Value.Remove(0, ParentCode.Length));
                    Values.Add(Int);
                }
            }

            Sql_Select = new StringBuilder();
            //增加查询条件 Scdel=0  2013-10-17
            Sql_Select.Append("Select CatlogCode from sys_biz_ReportSheet where Scdel=0 and CatlogCode like '");
            Sql_Select.Append(ParentCode);
            Sql_Select.Append("____'");

            Data = GetDataTable(Sql_Select.ToString()) as DataTable;
            if (Data != null && Data.Rows.Count > 0)
            {
                foreach (DataRow row in Data.Rows)
                {
                    string Value = row["CatlogCode"].ToString();
                    int Int = int.Parse(Value.Remove(0, ParentCode.Length));
                    Values.Add(Int);
                }
            }
            int i = 1;
            while (Values.Contains(i)) ++i;

            return ParentCode + i.ToString("0000");
        }

        public bool NewReportCatlog(string Code, string Name)
        {
            StringBuilder Sql_Insert = new StringBuilder();
            Sql_Insert.Append("Insert into sys_biz_ReportCatlog(ID,CatlogCode,CatlogName) values('");
            Sql_Insert.Append(Guid.NewGuid().ToString());
            Sql_Insert.Append("','");
            Sql_Insert.Append(Code);
            Sql_Insert.Append("','");
            Sql_Insert.Append(Name);
            Sql_Insert.Append("')");

            Boolean Result = false;

            try
            {
                object r = ExcuteCommand(Sql_Insert.ToString());
                Result = (Convert.ToInt32(r) == 1);
            }
            catch
            { }

            return Result;
        }

        public bool DeleteReportCatlog(string Code)
        {
            StringBuilder Sql_Delete = new StringBuilder();
            Sql_Delete.Append("Delete From sys_biz_ReportCatlog Where CatlogCode like '");
            Sql_Delete.Append(Code);
            Sql_Delete.Append("%'");

            Boolean Result = false;

            try
            {
                object r = ExcuteCommand(Sql_Delete.ToString());
                Result = (Convert.ToInt32(r) == 1);
            }
            catch
            { }

            return Result;
        }

        public bool UpdateReportCatlog(string Code, string Name)
        {
            StringBuilder Sql_Update = new StringBuilder();
            Sql_Update.Append("Update sys_biz_ReportCatlog Set CatlogName='");
            Sql_Update.Append(Name);
            Sql_Update.Append("' Where CatlogCode = '");
            Sql_Update.Append(Code);
            Sql_Update.Append("'");

            Boolean Result = false;

            try
            {
                object r = ExcuteCommand(Sql_Update.ToString()); 
                Result = (Convert.ToInt32(r) == 1);
            }
            catch
            { }

            return Result;
        }
    }
}
