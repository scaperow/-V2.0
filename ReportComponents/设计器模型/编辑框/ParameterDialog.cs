using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FarPoint.Win.Spread.CellType;
using ReportCommon;

namespace ReportComponents
{
    public partial class ParameterDialog : Form
    {
        private ReportConfiguration report;

        public ParameterDialog()
        {
            InitializeComponent();
        }

        public ReportConfiguration Report
        {
            get
            {
                return this.report;
            }
            set
            {
                this.report = value;
            }
        }

        private void ParameterDialog_Load(object sender, EventArgs e)
        {
            List<ReportParameter> Parameters = report.ReportParameters;
            foreach (ReportParameter parameter in Parameters)
            {
                String NodeText = parameter.DisplayName;
                TreeNode Node = new TreeNode(NodeText);
                Node.Tag = parameter;
                treeView_Parameters.Nodes.Add(Node);
            }
        }

        private void ToolStripButton_Click(object sender, EventArgs e)
        {
            if (sender == AddParameterButton)
            {
                AddParameterMethod();
            }
            else if (sender == RemoveParameterButton)
            {
                RemoveParameterMethod();
            }
        }

        private void AddParameterMethod()
        {
            String ParameterName = "Parameter" + treeView_Parameters.Nodes.Count;
            ReportParameter parameter = new ReportParameter();
            parameter.Name = ParameterName;
            parameter.DisplayName = ParameterName;
            parameter.ReportIndex = report.Index;

            TreeNode ParameterNode = new TreeNode();
            ParameterNode.Text = parameter.DisplayName;
            ParameterNode.Tag = parameter;
            treeView_Parameters.Nodes.Add(ParameterNode);
            treeView_Parameters.ExpandAll();
            treeView_Parameters.SelectedNode = ParameterNode;
        }

        private void RemoveParameterMethod()
        {
            TreeNode SelectedNode = treeView_Parameters.SelectedNode;
            SelectedNode.Remove();
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            TreeNode Node = treeView_Parameters.SelectedNode;
            if (Node != null)
            {
                ReportParameter parameter = Node.Tag as ReportParameter;
                if (sender == this.textBox_displayname)
                {
                    parameter.DisplayName = this.textBox_displayname.Text;
                }
                else if (sender == this.textBox_name)
                {
                    parameter.Name = this.textBox_name.Text;
                }

                Node.Text = parameter.DisplayName;
            }
        }

        private void treeView_Parameters_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ReportParameter parameter = e.Node.Tag as ReportParameter;
            this.textBox_name.Text = parameter.Name;
            this.textBox_displayname.Text = parameter.DisplayName;
        }

        private void Button_Ok_Click(object sender, EventArgs e)
        {
            List<ReportParameter> Parameters = new List<ReportParameter>();
            foreach (TreeNode node in treeView_Parameters.Nodes)
            {
                ReportParameter parameter = node.Tag as ReportParameter;
                if (parameter != null)
                {
                    Parameters.Add(parameter);
                }
            }

            if (DepositoryReportParameter.saveReportParameter(report.Index, Parameters))
            {
                report.ReportParameters.Clear();
                report.ReportParameters.AddRange(Parameters);
            }
            else
            {
                MessageBox.Show("报表参数保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
