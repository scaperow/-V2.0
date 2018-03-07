using System;
using System.Windows.Forms;
using ReportCommon;
using System.Data;
using FarPoint.Win.Spread;
using BizComponents;
using System.Collections.Generic;

namespace ReportComponents
{
    public partial class ReportConfigDataDialog : Form
    {
        public ReportConfigDataDialog()
        {
            InitializeComponent();
        }

        private void ReportConfigDataDialog_Load(object sender, EventArgs e)
        {
            BindModuleData();
            BindTree();
        }

        private void BindModuleData()
        {
            DataTable dt = DepositoryReportConfiguration.GetUnBindModules();
            treeView2.Nodes.Clear();
            TreeNode top = new TreeNode("模板列表");
            treeView2.Nodes.Add(top);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TreeNode n = new TreeNode(dt.Rows[i]["Name"].ToString());
                    n.Name = dt.Rows[i]["ID"].ToString();
                    top.Nodes.Add(n);
                }
            }
        }

        private void BindTree()
        {
            treeView1.Nodes.Clear();
            TreeNode top = new TreeNode("报表配置");
            treeView1.Nodes.Add(top);
            DataTable dt = DepositoryReportConfiguration.GetReportConfigData();
            DataTable dtModule = DepositoryReportConfiguration.GetReportConfigList();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TreeNode configNode = new TreeNode(dt.Rows[i]["TestName"].ToString());
                    configNode.Name = dt.Rows[i]["ID"].ToString();
                    top.Nodes.Add(configNode);
                    DataRow[] dtCM = dtModule.Select("ID=" + configNode.Name);
                    if (dtCM != null && dtCM.Length > 0)
                    {
                        for (int j = 0; j < dtCM.Length; j++)
                        {
                            TreeNode subNode = new TreeNode(dtCM[j]["ModuleName"].ToString());
                            subNode.Name = dtCM[j]["ModuleID"].ToString();
                            configNode.Nodes.Add(subNode);
                        }
                    }
                }
                treeView1.ExpandAll();
            }
        }

        private void Button_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            int selected;
            List<TreeNode> nodes = GetSelectedModules();
            if (nodes.Count > 0 && treeView1.SelectedNode != null)
            {
                if (int.TryParse(treeView1.SelectedNode.Name, out selected))
                {
                    foreach (TreeNode item in nodes)
                    {
                        DepositoryReportConfiguration.AddReportConfigModule(item.Name, treeView1.SelectedNode.Name);
                    }
                    BindModuleData();
                    BindTree();
                }
            }
        }

        private List<TreeNode> GetSelectedModules()
        {
            List<TreeNode> nodes = new List<TreeNode>();
            foreach (TreeNode node in treeView2.Nodes[0].Nodes)
            {
                if (node.Checked)
                {
                    nodes.Add(node);
                }
            }
            return nodes;
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                if (treeView1.SelectedNode.Name.Length > 10)
                {
                    DepositoryReportConfiguration.RemoveReportConfigModule(treeView1.SelectedNode.Name,
                        treeView1.SelectedNode.Parent.Name);
                    BindModuleData();
                    BindTree();
                }
            }
        }

    }
}
