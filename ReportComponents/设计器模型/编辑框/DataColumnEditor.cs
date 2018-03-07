using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Yqun.Common.Encoder;
using FarPoint.Win.Spread;
using ReportCommon;

namespace ReportComponents
{
    public partial class DataColumnEditor : Form
    {
        Report Report;
        GridElement Element;
        String Index;

        public DataColumnEditor(Report Report, String Index, GridElement Element)
        {
            InitializeComponent();

            this.Report = Report;
            this.Element = Element;
            this.Index = Index;
        }

        public GridElement ReportElement
        {
            get
            {
                return this.Element;
            }
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button_Ok_Click(object sender, EventArgs e)
        {
            DataColumn DataColumn = null;
            if (Element == null || !(Element.Value is DataColumn))
            {
                DataColumn = new DataColumn();
            }
            else
            {
                DataColumn = Element.Value as DataColumn;
            }

            TableData dataSource = cBoxDatasets.SelectedItem as TableData;
            if (dataSource != null)
            {
                DataColumn.TableName = dataSource.GetTableName();
                DataColumn.TableText = dataSource.GetTableText();
            }

            FieldInfo Info = cBoxFields.SelectedItem as FieldInfo;
            if (Info != null)
            {
                DataColumn.FieldName = Info.FieldName;
                DataColumn.FieldText = Info.FieldDescription;
            }

            if (rButtonGroup.Checked)
            {
                DataColumn.DataSetting = DataSetting.Group;
                DataColumn.IsUserDefinedGroup = (cBoxGroup.SelectedIndex == 1);
                if (DataColumn.IsUserDefinedGroup)
                {

                }
            }
            else if (rButtonList.Checked)
            {
                DataColumn.DataSetting = DataSetting.List;
            }
            else if (rButtonAggregation.Checked)
            {
                DataColumn.DataSetting = DataSetting.Aggregation;
                DataColumn.FunctionInfo = cBoxAggregation.SelectedItem as FunctionInfo;
            }

            if (rButton_LeftToRight.Checked)
            {
                Element.ExpandOrientation.Orientation = ExpandOrientation.LeftToRight;
            }
            else if (rButton_TopToBottom.Checked)
            {
                Element.ExpandOrientation.Orientation = ExpandOrientation.TopToBottom;
            }
            else if (rButton_None.Checked)
            {
                Element.ExpandOrientation.Orientation = ExpandOrientation.None;
            }

            DataColumn.UseParentCellFilter = checkBox1.Checked;
            DataColumn.DataFilter = filterControl1.GetConditon();

            if (checkBox2.Checked)
            {
                DataColumn.BlankCount = System.Convert.ToInt32(numericUpDown1.Value);
            }
            else
            {
                DataColumn.BlankCount = 0;
            }

            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void DataColumnEditor_Load(object sender, EventArgs e)
        {
            //加载数据集
            cBoxDatasets.Items.Clear();
            foreach (TableData dataSource in Report.Configuration.DataSources)
            {
                cBoxDatasets.Items.Add(dataSource);
            }

            if (cBoxDatasets.Items.Count > 0)
            {
                cBoxDatasets.SelectedIndex = 0;
            }

            //指定分组配置
            rButtonGroup.Checked = true;
            cBoxGroup.SelectedIndex = 0;
            //指定默认的扩展方式
            rButton_TopToBottom.Checked = true;
            //初始化条件编辑器
            filterControl1.InitControl(Report.Configuration);
            //加载汇总的方式
            cBoxAggregation.Items.Clear();
            cBoxAggregation.Items.Add(FunctionInfo.Sum);
            cBoxAggregation.Items.Add(FunctionInfo.Avg);
            cBoxAggregation.Items.Add(FunctionInfo.Max);
            cBoxAggregation.Items.Add(FunctionInfo.Min);
            cBoxAggregation.Items.Add(FunctionInfo.Count);
            cBoxAggregation.Items.Add(FunctionInfo.None);
            cBoxAggregation.SelectedIndex = 0;

            if (Element != null && Element.Value is DataColumn)
            {
                DataColumn DataColumn = Element.Value as DataColumn;

                int Index = 0;
                foreach (TableData dataSource in cBoxDatasets.Items)
                {
                    if (dataSource.GetTableName().ToLower() == DataColumn.TableName.ToLower())
                    {
                        Index = cBoxDatasets.Items.IndexOf(dataSource);
                        break;
                    }
                }

                if (cBoxDatasets.Items.Count > 0)
                {
                    cBoxDatasets.SelectedIndex = Index;
                }
                else
                {
                    cBoxDatasets.SelectedIndex = -1;
                }

                Index = 0;
                foreach (FieldInfo Info in cBoxFields.Items)
                {
                    if (Info.FieldName.ToLower() == DataColumn.FieldName.ToLower())
                    {
                        Index = cBoxFields.Items.IndexOf(Info);
                        break;
                    }
                }

                if (cBoxFields.Items.Count > 0)
                {
                    cBoxFields.SelectedIndex = Index;
                }
                else
                {
                    cBoxFields.SelectedIndex = -1;
                }

                if (Element.ExpandOrientation.isDefaultLeftParent)
                {
                    LeftCoordControl.setValue(ValueSource.Default, "");
                }
                else
                {
                    ValueSource source = (Element.ExpandOrientation.LeftParent == "" ? ValueSource.None : ValueSource.UseDefined);
                    Cell Cell = Report.ReportSheet.GetCellFromTag(null, Element.ExpandOrientation.LeftParent);
                    String cell = "";
                    if (Cell != null)
                        cell = Arabic_Numerals_Convert.Excel_Word_Numerals(Cell.Column.Index) + (Cell.Row.Index + 1).ToString();
                    LeftCoordControl.setValue(source, cell);
                }

                if (Element.ExpandOrientation.isDefaultTopParent)
                {
                    TopCoordControl.setValue(ValueSource.Default, "");
                }
                else
                {
                    ValueSource source = (Element.ExpandOrientation.TopParent == "" ? ValueSource.None : ValueSource.UseDefined);
                    Cell Cell = Report.ReportSheet.GetCellFromTag(null, Element.ExpandOrientation.TopParent);
                    String cell = "";
                    if (Cell != null)
                        cell = Arabic_Numerals_Convert.Excel_Word_Numerals(Cell.Column.Index) + (Cell.Row.Index + 1).ToString();
                    TopCoordControl.setValue(source, cell);
                }

                if (DataColumn.DataSetting == DataSetting.Group)
                {
                    rButtonGroup.Checked = true;
                    cBoxGroup.SelectedIndex = (DataColumn.IsUserDefinedGroup ? 1 : 0);
                }
                else if (DataColumn.DataSetting == DataSetting.List)
                {
                    rButtonList.Checked = true;
                }
                else if (DataColumn.DataSetting == DataSetting.Aggregation)
                {
                    rButtonAggregation.Checked = true;
                    Index = -1;
                    foreach (FunctionInfo Info in cBoxAggregation.Items)
                    {
                        if (Info.Name.ToLower() == DataColumn.FunctionInfo.Name.ToLower())
                        {
                            Index = cBoxAggregation.Items.IndexOf(Info);
                            break;
                        }
                    }
                    cBoxAggregation.SelectedIndex = Index;
                }

                if (Element.ExpandOrientation.Orientation == ExpandOrientation.TopToBottom)
                {
                    rButton_TopToBottom.Checked = true;
                }
                else if (Element.ExpandOrientation.Orientation == ExpandOrientation.LeftToRight)
                {
                    rButton_LeftToRight.Checked = true;
                }
                else if (Element.ExpandOrientation.Orientation == ExpandOrientation.None)
                {
                    rButton_None.Checked = true;
                }

                checkBox1.Checked = DataColumn.UseParentCellFilter;
                filterControl1.SetCondition(DataColumn.DataFilter);

                checkBox2.Checked = (DataColumn.BlankCount != 0);
                numericUpDown1.Enabled = checkBox2.Checked;
                numericUpDown1.Value = (numericUpDown1.Enabled ? DataColumn.BlankCount : 1);
            }
        }

        /// <summary>
        /// 编辑自定义分组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void cBoxDatasets_SelectedIndexChanged(object sender, EventArgs e)
        {
            cBoxFields.Items.Clear();
            TableData Source = cBoxDatasets.SelectedItem as TableData;
            if (Source != null)
            {
                for (int i = 0; i < Source.GetColumnCount(); i++)
                {
                    FieldInfo Info = new FieldInfo();
                    Info.FieldName = Source.GetColumnName(i);
                    Info.FieldDescription = Source.GetColumnText(i);
                    Info.FieldDataType = Source.GetColumnType(i).Name;

                    cBoxFields.Items.Add(Info);
                }
            }

            if (cBoxFields.Items.Count > 0)
            {
                cBoxFields.SelectedIndex = 0;
                filterControl1.UpdateCurrentDatasetFields(Source);
            }
        }

        private void cBoxGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            button3.Enabled = (cBoxGroup.SelectedIndex == 1);
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            cBoxGroup.Enabled = rButtonGroup.Checked;
            cBoxAggregation.Enabled = rButtonAggregation.Checked;

            rButton_TopToBottom.Enabled = !rButtonAggregation.Checked;
            rButton_LeftToRight.Enabled = !rButtonAggregation.Checked;
            rButton_None.Enabled = !rButtonAggregation.Checked;
        }

        private void CoordControl_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (sender == LeftCoordControl)
            {
                switch (e.ValueSource)
                {
                    case ValueSource.None:
                        Element.ExpandOrientation.LeftParent = "";
                        Element.ExpandOrientation.isDefaultLeftParent = false;
                        break;
                    case ValueSource.Default:
                        Element.ExpandOrientation.isDefaultLeftParent = true;
                        break;
                    case ValueSource.UseDefined:
                        Element.ExpandOrientation.LeftParent = Report.GetElementIndex(e.Value);
                        Element.ExpandOrientation.isDefaultLeftParent = false;
                        break;
                }
            }
            else if (sender == TopCoordControl)
            {
                switch (e.ValueSource)
                {
                    case ValueSource.None:
                        Element.ExpandOrientation.TopParent = "";
                        Element.ExpandOrientation.isDefaultTopParent = false;
                        break;
                    case ValueSource.Default:
                        Element.ExpandOrientation.isDefaultTopParent = true;
                        break;
                    case ValueSource.UseDefined:
                        Element.ExpandOrientation.TopParent = Report.GetElementIndex(e.Value);
                        Element.ExpandOrientation.isDefaultTopParent = false;
                        break;
                }
            }
        }

        //补充数据空白
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = checkBox2.Checked;
        }
    }
}
