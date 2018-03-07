using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ReportComponents;
using Yqun.Services;
using ReportCommon;

namespace ReportComponents
{
    public partial class TableDataFilterDialog : Form
    {
        String ReportIndex;
        TableData Source;

        public TableDataFilterDialog(String ReportIndex, TableData Source)
        {
            InitializeComponent();

            this.ReportIndex = ReportIndex;
            this.Source = Source;

            filterControl.Button_Add.Click += new EventHandler(Button_Add_Click);
            filterControl.Button_Delete.Click += new EventHandler(Button_Delete_Click);
        }

        private void DataSourceFilterDialog_Load(object sender, EventArgs e)
        {
            filterControl.cBoxValueColumn.Items.Clear();
            for (int i = 0; i < Source.GetColumnCount(); i++)
            {
                FieldInfo Info = new FieldInfo();
                Info.FieldName = Source.GetColumnName(i);
                Info.FieldDescription = Source.GetColumnText(i);
                Info.FieldDataType = Source.GetColumnType(i).Name;

                filterControl.cBoxValueColumn.Items.Add(Info);
            }
            if (filterControl.cBoxValueColumn.Items.Count > 0)
                filterControl.cBoxValueColumn.SelectedIndex = 0;

            //加载当前报表的参数
            filterControl.cBox_Parameters.Items.Clear();
            List<ReportParameter> ReportParameters = DepositoryReportParameter.getReportParameters(ReportIndex);
            filterControl.cBox_Parameters.Items.AddRange(ReportParameters.ToArray());

            //加载数据表的筛选条件
            TreeView FilterView = filterControl.tViewFilters;
            CombineFilterCondition DataFilter = Source.DataFilter;
            foreach (FilterCondition Filter in DataFilter.FilterConditions)
            {
                TreeNode Node = new TreeNode();
                Node.Name = Filter.ToString();
                Node.Text = FilterView.Nodes.Count == 0 ? Filter.ToString().Replace(Filter.Operation.ToString(), "") : Filter.ToString();
                Node.Tag = Filter;
                FilterView.Nodes.Add(Node);
            }
        }

        void Button_Add_Click(object sender, EventArgs e)
        {
            TreeView FilterView = filterControl.tViewFilters;

            FilterCondition filter = new FilterCondition();
            filter.Operation = (filterControl.rButton_And.Checked? BooleanOperation.And : BooleanOperation.Or);
            filter.LeftItem.FieldName = filterControl.cBoxValueColumn.SelectedItem.ToString();
            filter.CompareOperation = filterControl.cBoxOperation.SelectedItem as CompareOperation;
            if (filterControl.cBoxValueStyle.SelectedIndex == 0)
            {
                filter.RightItem.Style = FilterStyle.Value;
                filter.RightItem.Value = filterControl.cBoxValueDataset.SelectedItem.ToString();
            }
            else if (filterControl.cBoxValueStyle.SelectedIndex == 1)
            {
                filter.RightItem.Style = FilterStyle.Parameter;
                filter.RightItem.ParameterName = filterControl.cBox_Parameters.SelectedItem.ToString();
            }

            Source.DataFilter.FilterConditions.Add(filter);

            String Text = filter.ToString();
            Text = Text.Replace(filter.Operation.ToString(), "");

            TreeNode[] Nodes = FilterView.Nodes.Find(filter.ToString(), false);
            if (Nodes.Length == 0)
            {
                TreeNode Node = new TreeNode();
                Node.Name = filter.ToString();
                Node.Text = FilterView.Nodes.Count == 0 ? Text : filter.ToString();
                Node.Tag = filter;
                FilterView.Nodes.Add(Node);
                FilterView.SelectedNode = Node;
            }
            else
            {
                MessageBox.Show("条件已经存在。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void Button_Delete_Click(object sender, EventArgs e)
        {
            TreeNode Node = filterControl.tViewFilters.SelectedNode;
            if (Node == null)
                return;

            string Msg = "你确定要删除条件 ‘" + Node.Text + "’ 吗？";
            if (DialogResult.Yes == MessageBox.Show(Msg, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                FilterCondition filterCondition = Node.Tag as FilterCondition;
                Source.DataFilter.FilterConditions.Remove(filterCondition);
                Node.Remove();
            }
        }

        private void Button_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
