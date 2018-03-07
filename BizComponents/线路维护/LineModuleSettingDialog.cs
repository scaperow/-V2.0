using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BizComponents;
using BizCommon;
using FarPoint.Win.Spread;
using Yqun.Bases;

namespace BizComponents
{
    public partial class LineModuleSetting : Form
    {
        private int isModule = 1;
        //private Boolean isRelationSheet = true;

        public LineModuleSetting()
        {
            InitializeComponent();
        }

        private void ReferenceSheetDialog_Load(object sender, EventArgs e)
        {
            LoadLines();
            //treeModule.AfterCheck += new TreeViewEventHandler(treeModule_AfterCheck);
            treeModule.AfterSelect += new TreeViewEventHandler(treeModule_AfterSelect);
            rb_module.Checked = true;            
        }

        void treeModule_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse)
            {
                if (e.Node.Tag != null&&Convert.ToBoolean(e.Node.Tag))
                {
                    string ModuleID = e.Node.Name.ToString();
                    List<string> lstLineIDs = ModuleHelperClient.GetLineModuleByModuleID(ModuleID,isModule);
                    foreach (TreeNode node in treeLines.Nodes[0].Nodes)
                    {
                        bool bChecked = false;
                        foreach (string lineid in lstLineIDs)
                        {
                            if (lineid == node.Name)
                            {
                                bChecked = true;
                                node.Checked = true;
                                break;
                            }
                        }
                        if (bChecked == false)
                        {
                            node.Checked = false;
                        }
                    }
                }
            }
        }

        private void LoadModules()
        {
            DepositoryResourceCatlog.InitModuleCatlog(treeModule);
        }

        private void LoadLines()
        {
            DataTable dt = LineHelperClient.GetLineList();
            if (dt != null)
            {
                treeLines.Nodes.Clear();
                TreeNode TopNode = new TreeNode();
                TopNode.Name = "";
                TopNode.Text = "线路列表";
                TopNode.SelectedImageIndex = 1;
                TopNode.ImageIndex = 0;
                treeLines.Nodes.Add(TopNode);
                foreach (DataRow row in dt.Rows)
                {
                    TreeNode n = new TreeNode();
                    n.Name = row["ID"].ToString();
                    n.Text = row["LineName"].ToString();
                    n.SelectedImageIndex = 2;
                    n.ImageIndex = 2;
                    TopNode.Nodes.Add(n);
                }
            }
            treeLines.ExpandAll();
            treeLines.AfterCheck += new TreeViewEventHandler(treeLines_AfterCheck);
        }

        void treeLines_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    foreach (TreeNode item in e.Node.Nodes)
                    {
                        item.Checked = e.Node.Checked;
                    }
                }
            }
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            List<String> lineIDs = new List<String>();
            string ModuleID = string.Empty;

            foreach (TreeNode node in treeLines.Nodes[0].Nodes)
            {
                if (node.Checked)
                {
                    lineIDs.Add(node.Name);
                }
            }


            if (treeModule.SelectedNode.Tag != null && Convert.ToBoolean(treeModule.SelectedNode.Tag))
            {
                ModuleID = treeModule.SelectedNode.Name.ToString();
            }
            if (string.IsNullOrEmpty(ModuleID))
            {
                MessageBox.Show("请选择要设置的模板(表单)！");
                return;
            }
            Boolean flag = false;
            flag = ModuleHelperClient.SaveLineModule(ModuleID, lineIDs,isModule);
            if (flag == true)
            {
                MessageBox.Show("保存成功");
            }
            else
            {
                MessageBox.Show("保存失败");
            }
            
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void rb_sheet_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_module.Checked)
            {
                isModule = 1;
                DepositoryResourceCatlog.InitModuleCatlog(treeModule);
            }
            else if (rb_sheet.Checked)
            {
                isModule = 0;
                DepositorySheetCatlog.InitSheetCatlog(treeModule);
            }
        }
    }
}
