using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.Win;
using Yqun.BO.PermissionManager;
using System.Data;
using Yqun.Permissions.Common;
using Yqun.Permissions.Runtime;
using FarPoint.Win.Spread;

namespace Yqun.BO.BusinessManager
{
    public class SupervisionReportManager : BOBase
    {
        RoleManager RoleManager = new RoleManager();
        PermissionManager.PermissionManager PermissionManager = new Yqun.BO.PermissionManager.PermissionManager();

        public static String TreeID = "6ED9D9CB-117E-4d8c-A63B-0157BD1F9DFD";
        public void GenerateReport()
        {
            StringBuilder SQL_DataSource = new StringBuilder();
            SQL_DataSource.Append("select a.nodecode as foldercode,b.description as foldername,c.nodecode as modulecode,d.description as modulename,d.id ");
            SQL_DataSource.Append("from sys_engs_ItemInfo as b,");
            SQL_DataSource.Append("(SELECT NodeCode,ralationid FROM sys_engs_Tree ");
            SQL_DataSource.Append("WHERE (SUBSTRING(NodeCode, 0, 13) IN");
            SQL_DataSource.Append("(SELECT NodeCode FROM sys_engs_Tree AS sys_engs_Tree_1 WHERE (NodeType = '@unit_施工单位'))) and nodetype = '@folder') as a,");
            SQL_DataSource.Append("(SELECT NodeCode,ralationid FROM sys_engs_Tree ");
            SQL_DataSource.Append("WHERE (SUBSTRING(NodeCode, 0, 13) IN (SELECT NodeCode FROM sys_engs_Tree AS sys_engs_Tree_1 WHERE (NodeType = '@unit_施工单位'))) and nodetype = '@module') as c,");
            //增加查询条件  Scdel=0  2013-10-17
            SQL_DataSource.Append("sys_biz_Module as d where b.Scdel=0 and b.id = a.ralationid and c.ralationid = d.id and c.nodecode like a.nodecode + '%' order by a.nodecode,c.nodecode");

            DataTable DataSource = GetDataTable(SQL_DataSource.ToString());

            DataTable DataResule = new DataTable();

            DataColumn Column = new DataColumn("CompanyName");
            Column.DataType = typeof(String);
            DataResule.Columns.Add(Column);

            Column = new DataColumn("CompanyCode");
            Column.DataType = typeof(String);
            DataResule.Columns.Add(Column);

            Column = new DataColumn("ModelCode");
            Column.DataType = typeof(String);
            DataResule.Columns.Add(Column);

            Column = new DataColumn("FolderName");
            Column.DataType = typeof(String);
            DataResule.Columns.Add(Column);

            Column = new DataColumn("ModuleName");
            Column.DataType = typeof(String);
            DataResule.Columns.Add(Column);

            Column = new DataColumn("PXRate");
            Column.DataType = System.Type.GetType("System.Decimal");
            DataResule.Columns.Add(Column);

            Column = new DataColumn("YZRate");
            Column.DataType = System.Type.GetType("System.Decimal");
            DataResule.Columns.Add(Column);

            Column = new DataColumn("SelectTable");
            Column.DataType = typeof(String);
            DataResule.Columns.Add(Column);

            foreach (DataRow Row in DataSource.Rows)
            {
                String TableName = "biz_norm_extent_" + Row["id"].ToString();            
                
                StringBuilder SQL_Company = new StringBuilder();
                SQL_Company.Append("select a.Description,b.NodeCode from sys_engs_CompanyInfo as a,sys_engs_Tree as b");
                //增加查询条件  Scdel=0     2013-10-17
                SQL_Company.Append("where a.Scdel=0 and");
                SQL_Company.Append("b.NodeCode ='");
                SQL_Company.Append(Row["foldercode"].ToString().Substring(0, 12));
                SQL_Company.Append("' and a.id = b.ralationid");

                DataTable CompanyInfo = GetDataTable(SQL_Company.ToString());

                DataRow newRow = DataResule.NewRow();
                newRow["CompanyName"] = CompanyInfo.Rows[0]["Description"].ToString();
                newRow["CompanyCode"] = CompanyInfo.Rows[0]["NodeCode"].ToString();
                newRow["FolderName"] = Row["FolderName"].ToString();
                newRow["ModuleName"] = Row["ModuleName"].ToString();
                newRow["SelectTable"] = TableName;
                newRow["ModelCode"] = Row["modulecode"].ToString();
                

                DataResule.Rows.Add(newRow);
            }

            StringBuilder Sql_Select = new StringBuilder();
            Sql_Select.Append("select code from sys_auth_Organization where type = '");
            Sql_Select.Append("监理单位");
            Sql_Select.Append("'");

            DataTable Organization = GetDataTable(Sql_Select.ToString());

            if (Organization != null && Organization.Rows.Count > 0)
            {
                PermissionCollection Permissions = new PermissionCollection();
                foreach (DataRow Row in Organization.Rows)
                {
                    int PXCount;
                    int JZCount;
                    int AllCount;
                    Sql_Select = new StringBuilder();
                    //增加查询条件  Scdel=0  2013-10-17
                    Sql_Select.Append("select * from sys_auth_Users where Scdel=0 and code like '");
                    Sql_Select.Append(Row["code"].ToString());
                    Sql_Select.Append("%'");

                    DataTable Users = GetDataTable(Sql_Select.ToString());
                    if (Users != null && Users.Rows.Count > 0)
                    {
                        foreach (DataRow UserRow in Users.Rows)
                        {
                            RoleCollection Roles = RoleManager.InitRoleInformation(UserRow["ID"].ToString());
                            foreach (Role role in Roles)
                            {
                                PermissionCollection _Permissions = PermissionManager.InitPermissions(role.Index);
                                foreach (Permission Permission in _Permissions)
                                {
                                    if (!Permissions.Contains(Permission))
                                    {
                                        Permissions.Add(Permission);
                                    }
                                }
                            }
                        }
                    }

                    IAuthPolicy AuthPolicy = AuthManager.GetTreeAuth(TreeID, Permissions);
                    DataTable SelectData = new DataTable();
                    SelectData = DataResule.Clone();

                    StringBuilder Sql_JLSelect = new StringBuilder();
                    Sql_JLSelect.Append("select NodeCode from sys_engs_Tree where nodetype ='@unit_监理单位'");

                    DataTable JLData = GetDataTable(Sql_Select.ToString());

                    foreach (DataRow JLRow in JLData.Rows)
                    {
                        if (AuthPolicy.HasAuth(JLRow["NodeCode"].ToString()))
                        {
                            foreach (DataRow SelectRow in DataResule.Rows)
                            {
                                if (AuthPolicy.HasAuth(SelectRow["NodeCode"].ToString()))
                                {
                                    StringBuilder SQL_PXCount = new StringBuilder();
                                    SQL_PXCount.Append("select count(id) from ");
                                    SQL_PXCount.Append(SelectRow["SelectTable"].ToString());
                                    SQL_PXCount.Append("where trytype = '");
                                    SQL_PXCount.Append("平行 and scpt ='");
                                    SQL_PXCount.Append(JLRow["NodeCode"].ToString());
                                    SQL_PXCount.Append("' and scct in (select id from ");
                                    SQL_PXCount.Append(SelectRow["SelectTable"].ToString());
                                    SQL_PXCount.Append(" where scpt ='");
                                    SQL_PXCount.Append(SelectRow["modulecode"].ToString());
                                    SQL_PXCount.Append("')");

                                    PXCount = Convert.ToInt32(ExcuteScalar(SQL_PXCount.ToString()));

                                    StringBuilder SQL_JZCount = new StringBuilder();
                                    SQL_JZCount.Append("select count(id) from ");
                                    SQL_JZCount.Append(SelectRow["SelectTable"].ToString());
                                    SQL_JZCount.Append("where trytype = '");
                                    SQL_JZCount.Append("见证 and scpt ='");
                                    SQL_JZCount.Append(JLRow["NodeCode"].ToString());
                                    SQL_JZCount.Append("' and scct in (select id from ");
                                    SQL_JZCount.Append(SelectRow["SelectTable"].ToString());
                                    SQL_JZCount.Append(" where scpt ='");
                                    SQL_JZCount.Append(SelectRow["modulecode"].ToString());
                                    SQL_JZCount.Append("')");

                                    JZCount = Convert.ToInt32(ExcuteScalar(SQL_JZCount.ToString()));

                                    StringBuilder SQL_ALLCount = new StringBuilder();
                                    SQL_ALLCount.Append("select count(id) from ");
                                    SQL_ALLCount.Append(SelectRow["SelectTable"].ToString());
                                    SQL_ALLCount.Append("where scpt ='");
                                    SQL_ALLCount.Append(SelectRow["modulecode"].ToString());
                                    SQL_ALLCount.Append("'");

                                    AllCount = Convert.ToInt32(ExcuteScalar(SQL_ALLCount.ToString()));

                                    SelectRow["PXRate"] = PXCount / AllCount;
                                    SelectRow["YZRate"] = JZCount / AllCount;

                                    SelectData.ImportRow(SelectRow);
                                }
                            }

                            DrawSupervisionReport(SelectData, Row["Description"].ToString());
                        }
                    }
                }
            }
        }

        private void DrawSupervisionReport(DataTable SelectData, String SupervisionName)
        {
            String CompanyName = string.Empty;
            String SYSName = string.Empty;
            int i = 0;
            SheetView SheetView = new SheetView();
            SheetView.Columns.Clear();
            SheetView.Rows.Clear();

            //设置报表行列
            SheetView.Columns.Count = 6;
            SheetView.FrozenRowCount = 4;
            SheetView.Rows.Count = 4;
            SheetView.SetColumnWidth(0, getMeasureString("单位试验室名称"));
            SheetView.SetColumnWidth(1, getMeasureString("单位试验室名称"));
            SheetView.SetColumnWidth(2, getMeasureString("单位试验室名称"));
            SheetView.SetColumnWidth(3, getMeasureString("单位试验室名称"));
            SheetView.SetColumnWidth(4, getMeasureString("单位试验室名称"));
            SheetView.SetColumnWidth(5, getMeasureString("单位试验室名称"));

            //设置报表表头
            SheetView.Models.Span.Add(0, 0, 1, SheetView.Columns.Count);
            SheetView.Cells[0, 0].Value = "监理平行见证频率表";

            SheetView.Models.Span.Add(1, 0, 3, 1);
            SheetView.Cells[1, 0].Value = "单位试验室";

            SheetView.Models.Span.Add(1, 1, 3, 1);
            SheetView.Cells[1, 1].Value = "模板";

            SheetView.Models.Span.Add(1, 2, 1, 4);
            SheetView.Cells[1, 2].Value = "平行见证情况";

            SheetView.Models.Span.Add(2, 2, 1, 4);

            //SheetView.Cells[2, 2].Value = StaticStart.ToShortDateString() + "至" + StaticEnd.ToShortDateString();

            SheetView.Cells[3, 2].Value = "平行频率";
            SheetView.Columns[2].Tag = "";

            SheetView.Cells[3, 3].Value = "是否合格"; ;

            SheetView.Cells[3, 4].Value = "见证频率";
            SheetView.Columns[4].Tag = "";

            SheetView.Cells[3, 5].Value = "是否合格"; ;

            foreach (DataRow Row in SelectData.Rows)
            {
                if (SelectData.Rows.IndexOf(Row) == 0)
                {
                    CompanyName = Row["CompanyName"].ToString();
                    SYSName = Row["FolderName"].ToString();
                    i = 1;
                    SheetView.Rows.Add(SheetView.Rows.Count, 1);
                    SheetView.Cells[SheetView.Rows.Count - 1, 1].Value = Row["ModuleName"].ToString();
                    SheetView.Cells[SheetView.Rows.Count - 1, 2].Value = Row["PXRate"].ToString();
                    SheetView.Cells[SheetView.Rows.Count - 1, 4].Value = Row["YZRate"].ToString();
                }
                else if (CompanyName.Equals(Row["CompanyName"].ToString()) && SYSName.Equals(Row["FolderName"].ToString()))
                {
                    i = i + 1;
                    SheetView.Rows.Add(SheetView.Rows.Count, 1);
                    SheetView.Cells[SheetView.Rows.Count - 1, 1].Value = Row["ModuleName"].ToString();
                    SheetView.Cells[SheetView.Rows.Count - 1, 2].Value = Row["PXRate"].ToString();
                    SheetView.Cells[SheetView.Rows.Count - 1, 4].Value = Row["YZRate"].ToString();
                }
                else if (CompanyName.Equals(Row["CompanyName"].ToString()) && !SYSName.Equals(Row["FolderName"].ToString()))
                {
                    SheetView.Models.Span.Add(SheetView.Rows.Count - i, 0, i, 1);
                    SheetView.Cells[SheetView.Rows.Count - i, 0].Value = CompanyName + "--" + SYSName;
                    SYSName = Row["FolderName"].ToString();
                    SheetView.Rows.Add(SheetView.Rows.Count, 1);
                    SheetView.Cells[SheetView.Rows.Count - 1, 1].Value = Row["ModuleName"].ToString();
                    SheetView.Cells[SheetView.Rows.Count - 1, 2].Value = Row["PXRate"].ToString();
                    SheetView.Cells[SheetView.Rows.Count - 1, 4].Value = Row["YZRate"].ToString();
                    i = 1;
                }
                else if (!CompanyName.Equals(Row["CompanyName"].ToString()) && !SYSName.Equals(Row["FolderName"].ToString()))
                {
                    SheetView.Models.Span.Add(SheetView.Rows.Count - i, 0, i, 1);
                    SheetView.Cells[SheetView.Rows.Count - i, 0].Value = CompanyName + "--" + SYSName;
                    SYSName = Row["FolderName"].ToString();
                    CompanyName = Row["CompanyName"].ToString();
                    SheetView.Rows.Add(SheetView.Rows.Count, 1);
                    SheetView.Cells[SheetView.Rows.Count - 1, 1].Value = Row["ModuleName"].ToString();
                    SheetView.Cells[SheetView.Rows.Count - 1, 2].Value = Row["PXRate"].ToString();
                    SheetView.Cells[SheetView.Rows.Count - 1, 4].Value = Row["YZRate"].ToString();
                    i = 1;
                }
            }

            String SheetXML = Serializer.GetObjectXml(SheetView, "SheetView");

            string updatesql = "select * from sys_biz_reminder_supervisionReport where SupervisionName = '" + SupervisionName + "'";
            DataTable UpdateData = GetDataTable(updatesql);
            if (UpdateData != null && UpdateData.Rows.Count == 0)
            {
                DataRow newRow = UpdateData.NewRow();
                newRow["ID"] = Guid.NewGuid().ToString();
                newRow["SupervisionName"] = SupervisionName;
                newRow["sheetstyle"] = SheetXML;
                UpdateData.Rows.Add(newRow);
            }
            if (UpdateData != null && UpdateData.Rows.Count > 0)
            {
                UpdateData.Rows[0]["sheetstyle"] = SheetXML;
            }
            object r = Update(UpdateData);
        }

        private int getMeasureString(string InputStr)
        {
            return 14;
        }

        public Boolean AlterNewColumn(String TestIndex)
        {
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件Scdel=0     2013-10-19
            Sql_Select.Append("Select tablename from sys_tables Where Scdel=0 ");

            DataTable TableName = GetDataTable(Sql_Select.ToString());

            if (TableName != null && TableName.Rows.Count > 0)
            {
                List<String> SqlCommonds = new List<string>();
                foreach (DataRow Row in TableName.Rows)
                {
                    String SqlCommond = "Alter table [" + Row["tablename"].ToString() + "] add [SCCT] nvarchar(50) null";
                    SqlCommonds.Add(SqlCommond);
                }
                object r = ExcuteCommands(SqlCommonds.ToArray());
            }
            return true;
        }

        public String SupervisionInfo(String Code)
        {
            String SheetStyle = string.Empty;
            StringBuilder Sql_Select = new StringBuilder();
            Sql_Select.Append("select sheetstyle from sys_biz_reminder_supervisionReport ");
            Sql_Select.Append("where Code ='");
            Sql_Select.Append(Code);
            Sql_Select.Append("'");

            DataTable Dt = GetDataTable(Sql_Select.ToString());
            if (Dt!=null && Dt.Rows.Count > 0)
            {
                SheetStyle = Dt.Rows[0]["sheetstyle"].ToString();
            }

            return SheetStyle;
        }
    }
}
