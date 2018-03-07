using System;
using System.Collections.Generic;
using System.Text;
using Yqun.Bases;
using System.Windows.Forms;
using System.Data;
using Yqun.Services;
using Yqun.Data.DataBase;
using Yqun.Client;
using Yqun.Permissions.Common;
using BizCommon;

namespace BizComponents
{
    public class CacheResourceCatlog
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

            DataTable ModelData = Agent.CallLocalService("Yqun.BO.BusinessManager.dll", "InitModelResource", new object[] { }) as DataTable;
            if (ModelData != null && ModelData.Rows.Count > 0)
            {
                foreach (DataRow row in ModelData.Rows)
                {
                    string Index = row["ID"].ToString();
                    string Code = row["CatlogCode"].ToString();
                    string Name = row["Description"].ToString();
                    Boolean IsModel = Convert.ToBoolean(row["IsModel"]);
                    #region 增加字段Scts_1和Scdel默认值为0    2013-10-15
                    string Scts_1 = row["Scts_1"].ToString();
                    string Scdel = row["Scdel"].ToString();
                    #endregion
                    string ParentCode = "";
                    try
                    {
                        ParentCode = Code.Substring(0, Code.Length - 4);
                    }
                    catch
                    { }

                    TreeNode[] Nodes = View.Nodes.Find(ParentCode, true);
                    if (Nodes.Length > 0)
                    {
                        TreeNode ParentNode = Nodes[0];

                        TreeNode Node = new TreeNode();
                        Node.Name = Code;
                        Node.Text = Name;

                        Selection selection = new Selection();
                        selection.TypeFlag = IsModel.ToString();
                        selection.ID = Index;
                        Node.Tag = selection;

                        if (!IsModel)
                        {
                            Node.SelectedImageIndex = 1;
                            Node.ImageIndex = 0;
                        }
                        else
                        {
                            Node.SelectedImageIndex = 2;
                            Node.ImageIndex = 2;
                        }

                        //增加判断，如果Scdel标记为已删除，则不加载次节点   2013-10-15
                        if (Scdel == "0" || string.IsNullOrEmpty(Scdel))
                        {
                            ParentNode.Nodes.Add(Node);
                        }
                    }
                }

                ModelData.Dispose();
                ModelData = null;
            }

            View.ExpandAll();
        }

        public static List<IndexDescriptionPair> GetModels()
        {
            List<IndexDescriptionPair> r = new List<IndexDescriptionPair>();

            StringBuilder Sql_Module = new StringBuilder();
            //过滤Scdel标记为已删除的数据    2013-10-15
            Sql_Module.Append("Select ID,Description,Dcdel from sys_biz_Module Where (Scdel IS NULL or Scdel=0) order by CatlogCode");
            //Sql_Module.Append("Select ID,Description from sys_biz_Module order by CatlogCode");
            DataTable ModuleData = Agent.CallLocalService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { Sql_Module.ToString() }) as DataTable;
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
    }
}
