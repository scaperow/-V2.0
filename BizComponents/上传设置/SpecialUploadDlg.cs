using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BizComponents
{
    public partial class SpecialUploadDlg : Form
    {
        Guid moduleID;
        public SpecialUploadDlg(Guid moduleID, String moduleName)
        {
            InitializeComponent();
            this.Text = this.Text + "[" + moduleName + "]";
            this.moduleID = moduleID;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                if (treeView1.SelectedNode.Tag.ToString().ToLower() == "true")
                {
                    Guid destModuleID = new Guid(treeView1.SelectedNode.Name);
                    Boolean flag = LineHelperClient.UpdateSpecialModule(moduleID, destModuleID);
                    MessageBox.Show(flag ? "成功" : "失败");
                }
            }
        }

        private void SpecialUploadDlg_Load(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            TreeNode TopNode = new TreeNode();
            TopNode.Name = "";
            TopNode.Text = "模板列表";
            TopNode.SelectedImageIndex = 1;
            TopNode.ImageIndex = 0;
            treeView1.Nodes.Add(TopNode);
            DataSet ds = LineHelperClient.GetLineModuleList();
            DataTable dt = ds.Tables["dbo.sys_biz_ModuleCatlog"];
            DataTable sheetDT = ds.Tables["dbo.sys_module"];

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
                            node.Nodes.Add(n);
                        }
                    }
                }
            }
            treeView1.ExpandAll();
        }
    }
}
