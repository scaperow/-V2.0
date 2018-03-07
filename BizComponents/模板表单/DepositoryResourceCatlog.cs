using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using Yqun.Services;
using System.Windows.Forms;
using System.Data;
using System.Collections;
using Yqun.Bases;

namespace BizComponents
{
    public class DepositoryResourceCatlog
    {
        public static void InitModelSheetCatlog(TreeView View)
        {
            View.Nodes.Clear();

            DataTable ModelSheetData = Agent.CallLocalService("Yqun.BO.BusinessManager.dll", "InitModelSheetData", new object[] { }) as DataTable;
            if (ModelSheetData != null && ModelSheetData.Rows.Count > 0)
            {
                ModelSheetData.DefaultView.RowFilter = "IsSheet = 'false'";
                DataTable ModelData = ModelSheetData.DefaultView.ToTable();
                foreach (DataRow Row in ModelData.Rows)
                {
                    String ID = Row["ID"].ToString();
                    String Text = Row["Description"].ToString();
                    String strSheets = Row["Sheets"].ToString();
                    #region 增加字段Scts_1和Scdel默认值为0    2013-10-15
                    string Scts_1 = Row["Scts_1"].ToString();
                    string Scdel = Row["Scdel"].ToString();
                    #endregion
                    TreeNode Node = new TreeNode();
                    Node.Name = ID;
                    Node.Text = Text;
                    Node.SelectedImageIndex = 0;
                    Node.ImageIndex = 0;
                    View.Nodes.Add(Node);

                    String[] Sheets = strSheets.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    DataRow[] SheetRows = ModelSheetData.Select(string.Concat("ID in ('", string.Join("','", Sheets), "')"));
                    foreach (DataRow sheetrow in SheetRows)
                    {
                        String SheetID = sheetrow["ID"].ToString();
                        String SheetText = sheetrow["Description"].ToString();

                        TreeNode SheetNode = new TreeNode();
                        SheetNode.Name = SheetID;
                        SheetNode.Text = SheetText;
                        SheetNode.SelectedImageIndex = 1;
                        SheetNode.ImageIndex = 1;
                        //增加判断，如果Scdel标记为已删除，则不加泽次节点   2013-10-15
                        if (Scdel == "0" || string.IsNullOrEmpty(Scdel))
                        {
                            Node.Nodes.Add(SheetNode);
                        }
                    }
                }

                ModelSheetData.Dispose();
                ModelSheetData = null;
            }
        }

        public static void InitModuleCatlog(TreeView View)
        {
            View.Nodes.Clear();
            TreeNode TopNode = new TreeNode();
            TopNode.Name = "";
            TopNode.Text = "模板列表";
            TopNode.SelectedImageIndex = 1;
            TopNode.ImageIndex = 0;
            View.Nodes.Add(TopNode);
            DataSet ds = ModuleHelperClient.GetModuleCategoryAndModule();
            DataTable dt = ds.Tables["dbo.sys_biz_ModuleCatlog"];
            DataTable sheetDT = ds.Tables["dbo.sys_module"];

            string strDeniedModuleIDs = ""; 
            //Yqun.Common.ContextCache.ApplicationContext.Current.DeniedModuleIDs;
            List<string> lstDeniedModuleIDs;
            if (!string.IsNullOrEmpty(strDeniedModuleIDs))
            {
                lstDeniedModuleIDs = new List<string>(strDeniedModuleIDs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            }
            else
            {
                lstDeniedModuleIDs = new List<string>();
            }
            bool isDenied = false;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    TreeNode node = new TreeNode();
                    node.Name = row["CatlogCode"].ToString();
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
                            if (lstDeniedModuleIDs.Count > 0)
                            {
                                isDenied = false;
                                foreach (string item in lstDeniedModuleIDs)
                                {
                                    if (item == n.Name)
                                    {
                                        isDenied = true;
                                        break;
                                    }
                                }
                            }
                            if (isDenied == true)
                            { 
                                continue;
                            }
                            else
                            {
                                node.Nodes.Add(n);
                            }
                        }
                    }
                }
            }
            View.ExpandAll();
        }

        public static List<IndexDescriptionPair> GetModels()
        {
            List<IndexDescriptionPair> r = new List<IndexDescriptionPair>();

            StringBuilder Sql_Module = new StringBuilder();
            //过滤Scdel标记为已删除的数据    2013-10-15
            Sql_Module.Append("Select ID,Description,Scdel from sys_biz_Module where (Scdel IS NULL or Scdel=0) order by CatlogCode");
            //Sql_Module.Append("Select ID,Description from sys_biz_Module order by CatlogCode");
            DataTable ModuleData = Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { Sql_Module.ToString() }) as DataTable;
            if (ModuleData != null)
            {
                foreach (DataRow Row in ModuleData.Rows)
                {
                    String Index = Row["ID"].ToString();
                    String Description = Row["Description"].ToString();

                    IndexDescriptionPair pair = new IndexDescriptionPair();
                    pair.Index = Index;
                    pair.Description = Description;
                    r.Add(pair);
                }
            }

            return r;
        }

        public static string GetNextCode(string ParentCode)
        {
            List<int> Values = new List<int>();
            StringBuilder Sql_Select = new StringBuilder();
            // 增加查询条件 Scdel=0     2013-10-19
            Sql_Select.Append("Select CatlogCode from sys_biz_ModuleCatlog where Scdel=0 and CatlogCode like '");
            Sql_Select.Append(ParentCode);
            Sql_Select.Append("____'");

            DataTable Data = Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { Sql_Select.ToString() }) as DataTable;
            if (Data != null && Data.Rows.Count > 0)
            {
                foreach (DataRow row in Data.Rows)
                {
                    string Value = row["CatlogCode"].ToString();
                    int Int = int.Parse(Value.Remove(0, ParentCode.Length));
                    Values.Add(Int);
                }
            }

            Sql_Select.Replace(Sql_Select.ToString(), "");
            Sql_Select = new StringBuilder();
            Sql_Select.Append("Select CatlogCode from sys_biz_Module where CatlogCode like '");
            Sql_Select.Append(ParentCode);
            Sql_Select.Append("____'");

            Data = Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { Sql_Select.ToString() }) as DataTable;
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

        public static bool New(string Code, string Name)
        {
            StringBuilder Sql_Insert = new StringBuilder();
            // 增加条件 Scts_1=Getdate()     2013-10-19
            Sql_Insert.Append("Insert into sys_biz_ModuleCatlog(ID,CatlogCode,CatlogName,Scts_1) values('");
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
            //增加字段Scdel=0     2013-10-19
            Sql_Select.Append("Select * From sys_biz_Module Where Scdel=0 and CatlogCode like '");
            Sql_Select.Append(Code);
            Sql_Select.Append("%'");
            DataTable Data = Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { Sql_Select.ToString() }) as DataTable;
            if (Data != null && Data.Rows.Count > 0)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    String Index = Row["ID"].ToString();
                    ModuleConfiguration Configuration = DepositoryModuleConfiguration.InitModuleConfiguration(Index);
                    DepositoryModuleConfiguration.Delete(Configuration);
                }
            }
            //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，便于数据同步     2013-10-15
            StringBuilder Sql_Delete = new StringBuilder();
            //Sql_Delete.Append("Delete From sys_biz_ModuleCatlog Where CatlogCode like '");
            //Sql_Delete.Append(Code);
            //Sql_Delete.Append("%'");
            Sql_Delete.Append("Update sys_biz_ModuleCatlog Set Scts_1='" + DateTime.Now + "',Scdel=1");
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
            //增加字段Scts_1     2013-10-15
            Sql_Update.Append("Update sys_biz_ModuleCatlog Set CatlogName='");
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
