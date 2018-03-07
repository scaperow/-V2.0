using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ReportCommon;

namespace ReportComponents
{
    public partial class VariableEditor : Form
    {
        List<ReportParameter> Parameters;
        GridElement Element;

        public VariableEditor(List<ReportParameter> Parameters, GridElement Element)
        {
            InitializeComponent();

            this.Parameters = Parameters;
            this.Element = Element;
        }

        public GridElement ReportElement
        {
            get
            {
                return this.Element;
            }
        }

        private void VariableEditor_Load(object sender, EventArgs e)
        {
            foreach (ReportParameter param in Parameters)
            {
                TreeNode Node = new TreeNode();
                Node.Name = param.Name;
                Node.Text = param.Name;
                Node.Tag = param.Value;
                VariableView.Nodes.Add(Node);
            }

            VariableView.ExpandAll();

            if (Element != null && Element.Value is Variable)
            {
                Variable variable = Element.Value as Variable;
                int Index = VariableView.Nodes.IndexOfKey(variable.Name);
                if (Index > -1)
                {
                    VariableView.SelectedNode = VariableView.Nodes[Index];
                }
            }
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            if (Element == null || !(Element.Value is Variable))
                Element.Value = new Variable();

            TreeNode Node = VariableView.SelectedNode;
            Variable Variable = Element.Value as Variable;
            Variable.Name = Node.Name;
            Variable.Value = Node.Tag;

            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
