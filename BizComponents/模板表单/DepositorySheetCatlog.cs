using System;
using System.Collections.Generic;
using System.Text;
using Yqun.Services;
using System.Windows.Forms;
using BizCommon;
using System.Data;

namespace BizComponents
{
    public class DepositorySheetCatlog
    {
        public static void InitSheetCatlog(TreeView View)
        {
            View.Nodes.Clear();
            TreeNode TopNode = new TreeNode();
            TopNode.Name = "";
            TopNode.Text = "表单列表";
            TopNode.SelectedImageIndex = 1;
            TopNode.ImageIndex = 0;
            View.Nodes.Add(TopNode);
            DataSet ds = ModuleHelperClient.GetSheetCategoryAndSheet();
            DataTable dt = ds.Tables["dbo.sys_biz_SheetCatlog"];
            DataTable sheetDT = ds.Tables["dbo.sys_sheet"];
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    TreeNode node = new TreeNode();
                    node.Name = row["ID"].ToString();
                    node.Text = row["CatlogName"].ToString();
                    node.SelectedImageIndex = 1;
                    node.ImageIndex = 0;
                    node.Tag = false;
                    TopNode.Nodes.Add(node);
                    DataRow[] sheetRows = sheetDT.Select("CatlogCode like '" + row["CatlogCode"] + "%' ", "Name asc");
                    if (sheetRows != null && sheetRows.Length > 0)
                    {
                        foreach (DataRow r1 in sheetRows)
                        {
                            TreeNode n = new TreeNode();
                            n.Name = r1["ID"].ToString();
                            n.Text = r1["Name"].ToString();
                            n.SelectedImageIndex = 2;
                            n.ImageIndex = 2;
                            n.Tag = true;
                            node.Nodes.Add(n);
                        }
                    }
                }
            }
            View.ExpandAll();
        }

        public static DataTable GetSheetResource(String FolderCode)
        {
            StringBuilder sql_select = new StringBuilder();
            //增加了字段,Scts_1,Scdel    2013-10-15
            sql_select.Append("select ID,Description,DataTable,Scts_1,Scdel from sys_biz_sheet where CatlogCode like '");
            sql_select.Append(FolderCode);
            sql_select.Append("%' order by CatlogCode");

            return Agent.CallLocalService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { sql_select.ToString() }) as DataTable;
        }

        public static string GetNextCode(string ParentCode)
        {
            List<int> Values = new List<int>();
            String catlogCode = "";
            DataTable dt = Agent.CallRemoteService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { "select CatlogCode from sys_biz_SheetCatlog where ID='" + ParentCode + "'" }) as DataTable;
            catlogCode = dt.Rows[0][0].ToString();
            StringBuilder Sql_Select = new StringBuilder();
            // 增加查询条件 Scdel=0     2013-10-19
            Sql_Select.Append("Select CatlogCode from sys_biz_SheetCatlog where Scdel=0 and CatlogCode like '");
            Sql_Select.Append(catlogCode);
            Sql_Select.Append("____'");

            DataTable Data = Agent.CallLocalService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { Sql_Select.ToString() }) as DataTable;
            if (Data != null && Data.Rows.Count > 0)
            {
                foreach (DataRow row in Data.Rows)
                {
                    string Value = row["CatlogCode"].ToString();
                    int Int = int.Parse(Value.Remove(0, catlogCode.Length));
                    Values.Add(Int);
                }
            }

            Sql_Select = new StringBuilder();
            // 增加查询条件 Scdel=0     2013-10-19
            Sql_Select.Append("Select CatlogCode from sys_biz_Sheet where Scdel=0 and CatlogCode like '");
            Sql_Select.Append(catlogCode);
            Sql_Select.Append("____'");

            Data = Agent.CallLocalService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { Sql_Select.ToString() }) as DataTable;
            if (Data != null && Data.Rows.Count > 0)
            {
                foreach (DataRow row in Data.Rows)
                {
                    string Value = row["CatlogCode"].ToString();
                    int Int = int.Parse(Value.Remove(0, catlogCode.Length));
                    Values.Add(Int);
                }
            }

            int i = 1;
            while (Values.Contains(i)) ++i;

            return catlogCode + i.ToString("0000");
        }

        public static bool New(string Code, string Name)
        {
            StringBuilder Sql_Insert = new StringBuilder();
            // 增加条件 Scts_1=Getdate()     2013-10-19
            Sql_Insert.Append("Insert into sys_biz_SheetCatlog(ID,CatlogCode,CatlogName,Scts_1) values('");
            Sql_Insert.Append(Guid.NewGuid().ToString());
            Sql_Insert.Append("','");
            Sql_Insert.Append(Code);
            Sql_Insert.Append("','");
            Sql_Insert.Append(Name);
            Sql_Insert.Append("','");
            Sql_Insert.Append(DateTime.Now.ToString());
            Sql_Insert.Append("')");

            Boolean Result = false;

            try
            {
                object r = Agent.CallService("Yqun.BO.LoginBO.dll", "ExcuteCommand", new object[] { Sql_Insert.ToString() });
                Result = (Convert.ToInt32(r) == 1);
            }
            catch
            { }

            return Result;
        }

        public static bool Delete(string Code)
        {
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件Scdel=0   2013-10-19
            Sql_Select.Append("Select * From sys_biz_Sheet Where Scdel=0 and CatlogCode like '");
            Sql_Select.Append(Code);
            Sql_Select.Append("%'");
            DataTable Data = Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { Sql_Select.ToString() }) as DataTable;
            if (Data != null && Data.Rows.Count > 0)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    String Index = Row["ID"].ToString();
                    SheetConfiguration Configuration = DepositorySheetConfiguration.InitConfiguration(Index);
                    DepositorySheetConfiguration.Delete(Configuration);
                }
            }
            //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，便于数据同步     2013-10-15
            StringBuilder Sql_Delete = new StringBuilder();
            //Sql_Delete.Append("Delete From sys_biz_SheetCatlog Where CatlogCode like '");
            //Sql_Delete.Append(Code);
            //Sql_Delete.Append("%'");
            Sql_Delete.Append("Update sys_biz_SheetCatlog Set Scts_1='"+DateTime.Now + "',Scdel=1");
            Sql_Delete.Append(" Where CatlogCode like '"); 
            Sql_Delete.Append(Code);
            Sql_Delete.Append("%'");

            Boolean Result = false;

            try
            {
                object r = Agent.CallService("Yqun.BO.LoginBO.dll", "ExcuteCommand", new object[] { Sql_Delete.ToString() });
                Result = (Convert.ToInt32(r) == 1);
            }
            catch
            { }

            return Result;
        }

        public static bool Update(string Code, string Name)
        {
            StringBuilder Sql_Update = new StringBuilder();
            //增加条件Scts_1=Getdate()   2013-10-15
            Sql_Update.Append("Update sys_biz_SheetCatlog Set CatlogName='");
            Sql_Update.Append(Name);
            Sql_Update.Append("',Scts=GetDate(),Scts_1=GetDate() Where CatlogCode = '");
            Sql_Update.Append(Code);
            Sql_Update.Append("'");

            Boolean Result = false;

            try
            {
                object r = Agent.CallService("Yqun.BO.LoginBO.dll", "ExcuteCommand", new object[] { Sql_Update.ToString() });
                Result = (Convert.ToInt32(r) == 1);
            }
            catch
            { }

            return Result;
        }
    }
}
