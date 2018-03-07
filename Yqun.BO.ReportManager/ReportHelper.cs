using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.Win.Spread;
using FarPoint.Win;
using System.Data;
using BizCommon;
using System.Collections;
using ReportCommon;

namespace Yqun.BO.ReportManager
{
    public class ReportHelper : BOBase
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public String GetReportString(Guid reportID, String start, String end, String testRoomCodes, Hashtable list)
        {
            String result = "";
            String sql = "SELECT SheetStyle,Config FROM sys_report WHERE ID='" + reportID + "'";
            DataTable dt = GetDataTable(sql);
            if(dt==null||dt.Rows.Count==0)
            {
                return "";
            }
            String xml = dt.Rows[0][0].ToString();
            SheetView sheetView = (SheetView)Serializer.LoadObjectXml(typeof(SheetView), xml, "SheetView");
            JZReport report = Newtonsoft.Json.JsonConvert.DeserializeObject<JZReport>(dt.Rows[0][1].ToString());
            if (sheetView != null && report!=null)
            {
                testRoomCodes = testRoomCodes.Replace("'", "");
                sql = "EXEC dbo.sp_report @sDate = '" + start + "',@eDate = '" + end + "',@testRoomCode = '" + testRoomCodes + "'";
                dt = GetDataTable(sql);
                //logger.Error(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sheetView.CopyRange(report.StartRowIndex + i, report.StartColumnIndex, report.StartRowIndex + 1 + i, 
                        report.StartColumnIndex, report.RepeatRowCount, report.ColumnCount, false);
                }

                for (int i = 0; i < sheetView.Rows.Count; i++)
                {
                    for (int j = 0; j < report.ColumnCount; j++)
                    {
                        Cell cell = sheetView.Cells[i, j];
                        if (cell.Value == null || cell.Value.ToString().Trim() == "")
                        {
                            continue;
                        }
                        else
                        {
                            if (cell.Value.ToString().StartsWith("@"))
                            {
                                cell.Value = list[cell.Value];
                            }
                            else
                            {
                                if (report.StartRowIndex <= i)
                                {
                                    int index = i - report.StartRowIndex;
                                    if (index >= 0 && index < dt.Rows.Count)
                                    {
                                        if (dt.Columns.Contains(cell.Value.ToString()))
                                        {
                                            cell.Value = dt.Rows[index][cell.Value.ToString()].ToString();
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
                sheetView.RemoveRows(report.StartRowIndex + dt.Rows.Count, 1);
                result = Serializer.GetObjectXml(sheetView, "SheetView");
            }
            return result;
        }

        public Boolean SaveReport(Guid reportID, String sheetXML, String config)
        {
            String sql = String.Format("UPDATE dbo.sys_report SET SheetStyle='{0}', Config='{1}' WHERE ID='{2}'",
                sheetXML.Replace("'", "''"), config.Replace("'", "''"), reportID);
            Int32 i = ExcuteCommand(sql);
            return i == 1;
        }

        public Guid GetReportIDByName(String name)
        {
            String sql = "SELECT ID FROM dbo.sys_report WHERE Name='" + name + "'";
            DataTable dt = GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                return new Guid(dt.Rows[0][0].ToString());
            }
            return Guid.Empty;
        }

        public List<ReportParameter> GetReportParametersByReportID(Guid reportID)
        {
            String sql = "SELECT * FROM dbo.sys_report_parameters WHERE ReportID='" + reportID + "'";
            DataTable dt = GetDataTable(sql);
            List<ReportParameter> reportParameters = new List<ReportParameter>();
            if (dt != null)
            {
                foreach (DataRow Row in dt.Rows)
                {
                    ReportParameter parameter = new ReportParameter();
                    parameter.Index = Row["ID"].ToString();
                    parameter.ReportIndex = Row["ReportID"].ToString();
                    parameter.Name = Row["Name"].ToString();
                    parameter.DisplayName = Row["Description"].ToString();

                    reportParameters.Add(parameter);
                }
            }
            return reportParameters;
        }

        public String GetReportXML(String id)
        {
            String result = "";
            String sql = "SELECT SheetStyle FROM sys_report WHERE ID='" + id + "'";
            DataTable dt = GetDataTable(sql);
            if (dt == null || dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }

        public DataTable GetReportCatlog()
        {
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件 Scdel=0  2013-10-17
            Sql_Select.Append("Select ID,CatlogCode,Description,'true' as IsReport from sys_report  where IsActive=1");
            Sql_Select.Append(" Union ");
            Sql_Select.Append("Select ID,CatlogCode,CatlogName as Description,'false' as IsReport from sys_biz_ReportCatlog order by CatlogCode");

            return GetDataTable(Sql_Select.ToString());
        }

        public DataTable GetReportConfig()
        {
            DataTable dt = GetReportConfigData();
            
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    String moduleName = "";
                    String moduleID = "";
                    DataTable subDT = GetDataTable("SELECT a.ModuleID,b.Name as ModuleName FROM dbo.sys_report_config_module a JOIN dbo.sys_module b ON a.ModuleID=b.ID WHERE a.ReportConfigID=" + row["ID"]);
                    if (subDT != null && subDT.Rows.Count > 0)
                    {
                        foreach (DataRow subRow in subDT.Rows)
                        {
                            moduleName += subRow["ModuleName"] + ",";
                            moduleID += subRow["ModuleID"] + ",";
                        }
                        moduleID = moduleID.TrimEnd(',');
                        moduleName = moduleName.TrimEnd(',');
                        row["ModuleID"] = moduleID;
                        row["ModuleName"] = moduleName;
                    }
                }
            }
            return dt;
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

        public DataTable GetReportConfigData()
        {
            String sql = "SELECT ID ,TestName ,UnitName ,Frequency,'' as ModuleID, '' as ModuleName FROM dbo.sys_report_config WHERE IsActive=1 ORDER BY ID";
            DataTable dt = GetDataTable(sql);
            return dt;
        }

        public DataTable GetReportConfigList()
        {
            String sql = @"SELECT a.ID,a.TestName,c.Name AS ModuleName,c.ID as ModuleID FROM dbo.sys_report_config a 
                            JOIN dbo.sys_report_config_module b ON a.ID=b.ReportConfigID
                            JOIN dbo.sys_module c ON b.ModuleID = c.ID
                            WHERE a.IsActive=1 ORDER BY a.ID ";
            return GetDataTable(sql);
        }

        public void AddReportConfigModule(String moduleID, String configID)
        {
            RemoveReportConfigModule(moduleID, configID);
            String sql = String.Format("INSERT INTO dbo.sys_report_config_module( ReportConfigID, ModuleID ) VALUES  ( {0},'{1}')",
                configID, moduleID);
            ExcuteCommand(sql);
        }

        public void RemoveReportConfigModule(String moduleID, String configID)
        {
            String sql = String.Format("DELETE dbo.sys_report_config_module WHERE ModuleID='{0}' AND ReportConfigID={1}",
                moduleID, configID);
            ExcuteCommand(sql);
        }

        public DataTable GetUnBindModules()
        {
            String sql = "SELECT ID,Name FROM dbo.sys_module WHERE ID NOT IN (SELECT DISTINCT ModuleID FROM dbo.sys_report_config_module) AND IsActive = 1 ORDER BY Name";
            return GetDataTable(sql);
        }
    }
}
