using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Yqun.Services;

namespace BizComponents
{
    public partial class SheetConfigurationDialog : Form
    {
        public SheetConfigurationDialog()
        {
            InitializeComponent();
        }

        private void SheetConfigurationDialog_Load(object sender, EventArgs e)
        {
            StringBuilder Sql_Select = new StringBuilder();
            //2013-10-17 增加查询条件  Scdel=0
            Sql_Select.Append("Select Description,DataTable,Scdel from sys_biz_Sheet where Scdel=0 Order by DESCRIPTION");
            DataTable Data = Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { Sql_Select.ToString() }) as DataTable;
            foreach (DataRow Row in Data.Rows)
            {
                String Description = Row["DESCRIPTION"].ToString();
                String TableName = Row["DataTable"].ToString();

                TreeNode Node = new TreeNode();
                Node.ImageIndex = 0;
                Node.SelectedImageIndex = 0;
                Node.Name = TableName;
                Node.Text = Description;

                SheetTreeView.Nodes.Add(Node);
            }

            SheetTreeView.ExpandAll();
        }

        private void SheetTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse || e.Action == TreeViewAction.ByKeyboard)
            {
                foreach (TreeNode Node in e.Node.TreeView.Nodes)
                {
                    if (Node != e.Node)
                        Node.Checked = false;
                }
            }
        }

        String _SelectedTable = "";
        public String SelectedTable
        {
            get
            {
                return _SelectedTable;
            }
        }

        private void Button_Ok_Click(object sender, EventArgs e)
        {
            foreach (TreeNode Node in SheetTreeView.Nodes)
            {
                if (Node.Checked)
                {
                    _SelectedTable = Node.Name;
                    break;
                }
            }

            if (_SelectedTable == "")
            {
                this.DialogResult = DialogResult.None;
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
