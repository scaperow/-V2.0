using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BizCommon;

namespace BizComponents
{
    public partial class SheetReferenceFilterDialog : Form
    {
        String TableName;
        DataFilterCondition dataFilterCondition;

        public SheetReferenceFilterDialog(String TableName, DataFilterCondition dataFilterCondition)
        {
            InitializeComponent();

            this.TableName = TableName;
            this.dataFilterCondition = dataFilterCondition;
        }

        private void ReferenceFilterDialog_Load(object sender, EventArgs e)
        {
            SheetfilterControl.InitializeControls();

            ComboBox ComboBoxColumns = SheetfilterControl.ComboBoxColumns;
            TableDefineInfo TableInfo = DepositoryTableDefineInfo.GetTableDefineInfo(TableName);
            if (TableInfo != null)
            {
                foreach (FieldDefineInfo Field in TableInfo.FieldInfos)
                {
                    if (!ColumnRegular.JudgeColumnName(Field.Description))
                    {
                        ComboBoxColumns.Items.Add(Field);
                    }
                }
            }

            TreeView filterView = SheetfilterControl.tViewFilters;
            filterView.Nodes.Clear();
            foreach (DataFilterItem Item in dataFilterCondition.Items)
            {
                TreeNode Node = new TreeNode();
                Node.Text = string.Format("{0} {1}", Item.ConditionalOperator, Item.ToString());
                Node.Tag = Item;
                filterView.Nodes.Add(Node);

                if (Node.Index == 0)
                    Node.Text = Item.ToString();
            }

            SheetfilterControl.Button_Add.Click += new EventHandler(Button_Add_Click);
            SheetfilterControl.Button_Delete.Click += new EventHandler(Button_Delete_Click);
        }

        void Button_Add_Click(object sender, EventArgs e)
        {
            ComboBox ComboBoxColumns = SheetfilterControl.ComboBoxColumns;
            ComboBox ComboBoxOperations = SheetfilterControl.ComboBoxOperations;
            ComboBox ComboBoxParameters = SheetfilterControl.ComboBoxParameters;
            RadioButton RadioButton = SheetfilterControl.rButton_And;

            DataFilterItem Item = new DataFilterItem();
            Item.ConditionalOperator = (RadioButton.Checked ? "and" : "or");
            Item.LeftItem = (ComboBoxColumns.SelectedItem as FieldDefineInfo).FieldName;
            Item.CompareOperation = ComboBoxOperations.SelectedItem as CompareOperation;
            Item.RightItem = (ComboBoxParameters.SelectedItem as IParameter).Name;

            TreeNode Node = new TreeNode();
            Node.Text = string.Format("{0} {1}", Item.ConditionalOperator, Item.ToString());
            Node.Tag = Item;

            TreeView filterView = SheetfilterControl.tViewFilters;
            filterView.Nodes.Add(Node);

            if (Node.Index == 0)
                Node.Text = Item.ToString();
        }

        void Button_Delete_Click(object sender, EventArgs e)
        {
            TreeView filterView = SheetfilterControl.tViewFilters;
            TreeNode Node = filterView.SelectedNode;
            if (Node != null)
            {
                string Message = "你确定要删除条件 ‘" + Node.Text + "’ 吗？";
                if (DialogResult.Yes == MessageBox.Show(Message, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    Node.Remove();

                    if (filterView.Nodes.Count > 0)
                    {
                        filterView.Nodes[0].Text = filterView.Nodes[0].Tag.ToString();
                    }
                }
            }
        }

        private void Button_Exit_Click(object sender, EventArgs e)
        {
            dataFilterCondition.Items.Clear();

            TreeView filterView = SheetfilterControl.tViewFilters;
            foreach (TreeNode Node in filterView.Nodes)
            {
                dataFilterCondition.Items.Add(Node.Tag as DataFilterItem);
            }

            this.Close();
        }
    }
}
