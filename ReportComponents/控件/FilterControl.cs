using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ReportCommon;

namespace ReportComponents
{
    public partial class FilterControl : UserControl
    {
        CombineFilterCondition _Condition;

        public FilterControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化控件基本信息
        /// </summary>
        /// <param name="report"></param>
        public void InitControl(ReportConfiguration Report)
        {
            cBoxValueStyle.SelectedIndex = 0;
            rButton_Normal.Checked = true;
            rButton_And.Checked = true;

            //加载数据集
            cBoxValueDataset.Items.Clear();
            foreach (TableData dataSource in Report.DataSources)
            {
                cBoxValueDataset.Items.Add(dataSource);
            }

            if (cBoxValueDataset.Items.Count > 0)
            {
                cBoxValueDataset.SelectedIndex = 0;
            }

            //加载比较运算符
            cBoxOperation.Items.Clear();
            cBoxOperation.Items.Add(CompareOperation.等于);
            cBoxOperation.Items.Add(CompareOperation.不等于);
            cBoxOperation.Items.Add(CompareOperation.大于);
            cBoxOperation.Items.Add(CompareOperation.大于或等于);
            cBoxOperation.Items.Add(CompareOperation.小于);
            cBoxOperation.Items.Add(CompareOperation.小于或等于);
            cBoxOperation.Items.Add(CompareOperation.始于);
            cBoxOperation.Items.Add(CompareOperation.不始于);
            cBoxOperation.SelectedIndex = 0;

            //加载报表的参数
            comboBox_Parameters.Items.Clear();
            foreach (ReportParameter parameter in Report.ReportParameters)
            {
                comboBox_Parameters.Items.Add(parameter);
            }
        }

        public void UpdateCurrentDatasetFields(TableData Source)
        {
            cBoxKeyColumn.Items.Clear();
            cBoxKeyColumn.Tag = Source;
            for (int i = 0; i < Source.GetColumnCount(); i++)
            {
                FieldInfo Info = new FieldInfo();
                Info.FieldName = Source.GetColumnName(i);
                Info.FieldDescription = Source.GetColumnText(i);
                Info.FieldDataType = Source.GetColumnType(i).Name;

                cBoxKeyColumn.Items.Add(Info);
            }

            if (cBoxKeyColumn.Items.Count > 0)
                cBoxKeyColumn.SelectedIndex = 0;
        }

        private void cBoxValueDataset_SelectedIndexChanged(object sender, EventArgs e)
        {
            cBoxValueColumn.Items.Clear();
            TableData Source = cBoxValueDataset.SelectedItem as TableData;
            for (int i = 0; i < Source.GetColumnCount(); i++)
            {
                FieldInfo Info = new FieldInfo();
                Info.FieldName = Source.GetColumnName(i);
                Info.FieldDescription = Source.GetColumnText(i);
                Info.FieldDataType = Source.GetColumnType(i).Name;

                cBoxValueColumn.Items.Add(Info);
            }

            if (cBoxValueColumn.Items.Count > 0)
                cBoxValueColumn.SelectedIndex = 0;
        }

        /// <summary>
        /// 初始化条件对象
        /// </summary>
        /// <param name="Condition"></param>
        public void SetCondition(CombineFilterCondition Condition)
        {
            _Condition = Condition;

            if (_Condition != null)
            {
                tViewFilters.Nodes.Clear();
                foreach (FilterCondition filter in _Condition.FilterConditions)
                {
                    TreeNode Node = new TreeNode();
                    tViewFilters.Nodes.Add(Node);

                    Node.Name = filter.ToString();
                    Node.Text = Node.Index > 0 ? filter.ToString() : filter.ToString().Substring(filter.ToString().IndexOf(' ')).Trim();
                    Node.Tag = filter;
                }
            }
        }

        /// <summary>
        /// 获得编辑好的条件对象
        /// </summary>
        /// <returns></returns>
        public CombineFilterCondition GetConditon()
        {
            CombineFilterCondition Result = new CombineFilterCondition();
            foreach (TreeNode Node in tViewFilters.Nodes)
            {
                FilterCondition filter = Node.Tag as FilterCondition;
                Result.FilterConditions.Add(filter);
            }

            return Result;
        }

        private void cBoxValueStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBoxValueStyle.SelectedIndex == 0)
            {
                cBoxOperation.Items.Clear();
                cBoxOperation.Items.Add(CompareOperation.等于);
                cBoxOperation.Items.Add(CompareOperation.不等于);
                cBoxOperation.Items.Add(CompareOperation.大于);
                cBoxOperation.Items.Add(CompareOperation.大于或等于);
                cBoxOperation.Items.Add(CompareOperation.小于);
                cBoxOperation.Items.Add(CompareOperation.小于或等于);
                cBoxOperation.Items.Add(CompareOperation.始于);
                cBoxOperation.Items.Add(CompareOperation.不始于);

                panel5.BringToFront();
            }
            else if (cBoxValueStyle.SelectedIndex == 1 && !cBoxIsNULL.Checked)
            {
                cBoxOperation.Items.Clear();
                cBoxOperation.Items.Add(CompareOperation.等于);
                cBoxOperation.Items.Add(CompareOperation.不等于);
                cBoxOperation.Items.Add(CompareOperation.大于);
                cBoxOperation.Items.Add(CompareOperation.大于或等于);
                cBoxOperation.Items.Add(CompareOperation.小于);
                cBoxOperation.Items.Add(CompareOperation.小于或等于);

                panel8.BringToFront();
            }
            else if (cBoxValueStyle.SelectedIndex == 1 && cBoxIsNULL.Checked)
            {
                cBoxOperation.Items.Clear();
                cBoxOperation.Items.Add(CompareOperation.是);
                cBoxOperation.Items.Add(CompareOperation.不是);

                panel8.BringToFront();
            }
            else if (cBoxValueStyle.SelectedIndex == 2)
            {
                cBoxOperation.Items.Clear();
                cBoxOperation.Items.Add(CompareOperation.等于);
                cBoxOperation.Items.Add(CompareOperation.不等于);
                cBoxOperation.Items.Add(CompareOperation.大于);
                cBoxOperation.Items.Add(CompareOperation.大于或等于);
                cBoxOperation.Items.Add(CompareOperation.小于);
                cBoxOperation.Items.Add(CompareOperation.小于或等于);
                cBoxOperation.Items.Add(CompareOperation.包含);
                cBoxOperation.Items.Add(CompareOperation.不包含);
                cBoxOperation.Items.Add(CompareOperation.始于);
                cBoxOperation.Items.Add(CompareOperation.不始于);

                panel3.BringToFront();
            }
        }

        private void RadioButton_Click(object sender, EventArgs e)
        {
            if (sender == this.rButton_Normal)
            {
                panel7.BringToFront();
            }
            else if (sender == this.rButton_Formula)
            {
                panel2.BringToFront();
            }
        }

        /// <summary>
        /// 删除条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Delete_Click(object sender, EventArgs e)
        {
            TreeNode Node = tViewFilters.SelectedNode;
            if (Node != null && Node.Tag is FilterCondition)
            {
                string message = "你确定要删除条件 ‘" + Node.Text + "’ 吗？";
                if (DialogResult.Yes == MessageBox.Show(message, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    Node.Remove();
                }
            }
        }

        /// <summary>
        /// 上移条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Up_Click(object sender, EventArgs e)
        {
            TreeNode Node = tViewFilters.SelectedNode;
            if (Node.PrevNode != null)
            {
                tViewFilters.BeginUpdate();

                TreeNode tmpNode = Node.Clone() as TreeNode;
                tViewFilters.Nodes.Insert(Node.PrevNode.Index, tmpNode);
                Node.Remove();
                tViewFilters.SelectedNode = tmpNode;

                FilterCondition filter = tmpNode.NextNode.Tag as FilterCondition;
                tmpNode.NextNode.Text = filter.Operation + " " + filter.ToString();

                if (tmpNode.Index == 0)
                {
                    filter = tmpNode.Tag as FilterCondition;
                    tmpNode.Text = filter.ToString();
                }
                else
                {
                    filter = tmpNode.Tag as FilterCondition;
                    tmpNode.Text = filter.Operation + " " + filter.ToString();
                }

                tViewFilters.EndUpdate();
            }
        }

        /// <summary>
        /// 下移条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Down_Click(object sender, EventArgs e)
        {
            TreeNode Node = tViewFilters.SelectedNode;
            if (Node.NextNode != null)
            {
                tViewFilters.BeginUpdate();

                TreeNode tmpNode = Node.Clone() as TreeNode;
                if (Node.NextNode.NextNode != null)
                {
                    tViewFilters.Nodes.Insert(Node.NextNode.NextNode.Index, tmpNode);
                }
                else
                {
                    tViewFilters.Nodes.Add(tmpNode);
                }

                Node.Remove();
                tViewFilters.SelectedNode = tmpNode;

                FilterCondition filter = tmpNode.Tag as FilterCondition;
                tmpNode.Text = filter.Operation + " " + filter.ToString();

                if (tmpNode.Index == 1)
                {
                    filter = tmpNode.PrevNode.Tag as FilterCondition;
                    tmpNode.PrevNode.Text = filter.ToString();
                }
                else
                {
                    filter = tmpNode.PrevNode.Tag as FilterCondition;
                    tmpNode.PrevNode.Text = filter.Operation + " " + filter.ToString();
                }

                tViewFilters.EndUpdate();
            }
        }

        /// <summary>
        /// 增加条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Add_Click(object sender, EventArgs e)
        {
            TableData Source = cBoxKeyColumn.Tag as TableData;
            BooleanOperation Operation = (rButton_And.Checked ? BooleanOperation.And : BooleanOperation.Or);

            ReportParameter Parameter = comboBox_Parameters.SelectedItem as ReportParameter;

            FilterCondition filter = new FilterCondition();
            filter.Operation = Operation;
            filter.IsFormula = rButton_Formula.Checked;
            if (rButton_Normal.Checked)
            {
                filter.LeftItem.TableName = Source.GetTableName();
                filter.LeftItem.FieldName = (cBoxKeyColumn.SelectedItem as FieldInfo).FieldName;

                filter.CompareOperation = cBoxOperation.SelectedItem as CompareOperation;

                if (cBoxValueStyle.SelectedIndex == 0)
                {
                    filter.RightItem.Style = FilterStyle.DataColumn;
                    filter.RightItem.TableName = (cBoxValueDataset.SelectedItem as TableData).GetTableName();
                    filter.RightItem.FieldName = (cBoxValueColumn.SelectedItem as FieldInfo).FieldName;
                }
                else if (cBoxValueStyle.SelectedIndex == 1)
                {
                    filter.RightItem.Style = FilterStyle.Value;
                    filter.RightItem.IsNull = cBoxIsNULL.Checked;
                    filter.RightItem.Value = tBoxValue.Text;
                }
                else if (cBoxValueStyle.SelectedIndex == 2)
                {
                    filter.RightItem.Style = FilterStyle.Parameter;
                    filter.RightItem.ParameterName = comboBox_Parameters.SelectedItem.ToString();
                }
            }
            else if (rButton_Formula.Checked)
            {
                filter.Formula = tBoxFormula.Text;
            }

            String Text = filter.ToString();
            Text = Text.Replace(filter.Operation.ToString(), "");

            TreeNode[] Nodes = tViewFilters.Nodes.Find(filter.ToString(), false);
            if (Nodes.Length == 0)
            {
                TreeNode Node = new TreeNode();
                Node.Name = filter.ToString();
                Node.Text = tViewFilters.Nodes.Count == 0 ? Text : filter.ToString();
                Node.Tag = filter;
                tViewFilters.Nodes.Add(Node);
                tViewFilters.SelectedNode = Node;
            }
            else
            {
                MessageBox.Show("条件已经存在。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cBoxIsNULL_CheckedChanged(object sender, EventArgs e)
        {
            tBoxValue.Enabled = !cBoxIsNULL.Checked;

            if (cBoxIsNULL.Checked)
            {
                cBoxOperation.Items.Clear();
                cBoxOperation.Items.Add(CompareOperation.是);
                cBoxOperation.Items.Add(CompareOperation.不是);
            }
            else
            {
                cBoxOperation.Items.Clear();
                cBoxOperation.Items.Add(CompareOperation.等于);
                cBoxOperation.Items.Add(CompareOperation.不等于);
                cBoxOperation.Items.Add(CompareOperation.大于);
                cBoxOperation.Items.Add(CompareOperation.大于或等于);
                cBoxOperation.Items.Add(CompareOperation.小于);
                cBoxOperation.Items.Add(CompareOperation.小于或等于);
            }
        }
    }
}
