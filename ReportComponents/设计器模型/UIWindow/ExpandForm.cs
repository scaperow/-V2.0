using System;
using WeifenLuo.WinFormsUI.Docking;
using FarPoint.Win.Spread;
using Yqun.Common.Encoder;
using ReportCommon;

namespace ReportComponents
{
    public partial class ExpandForm : DockContent
    {
        public ExpandForm()
        {
            InitializeComponent();
        }

        public ReportWindow ReportWindow
        {
            get
            {
                return DockPanel.Parent as ReportWindow;
            }
        }

        public ReportDesignUI ReportDesigner
        {
            get
            {
                return ReportWindow.reportDesignForm.ReportDesignPanel;
            }
        }

        public SheetView ActiveSheet
        {
            get
            {
                return ReportDesigner.FpSpread.ActiveSheet;
            }
        }

        public GridElement ActiveElement
        {
            get
            {
                if (Report != null)
                {
                    Cell Cell = ActiveSheet.ActiveCell;
                    string Index = Report.GetElementIndex(Cell.Row.Index, Cell.Column.Index);
                    return Report.GetElement(Index);
                }

                return null;
            }
        }

        public Report Report
        {
            get
            {
                return ReportWindow.report;
            }
        }

        private void ExpandForm_Load(object sender, EventArgs e)
        {
            ReportDesigner.FpSpread.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(FpSpread_SelectionChanged);

            //指定分组配置
            rButton_Group.Checked = true;
            cBoxGroup.Items.Clear();
            cBoxGroup.Items.Add("普通");
            cBoxGroup.Items.Add("高级");
            cBoxGroup.SelectedIndex = 0;
            //指定默认的扩展方式
            rButton_TopToBottom.Checked = true;
            //加载汇总的方式
            cBoxAggregation.Items.Clear();
            cBoxAggregation.Items.Add(FunctionInfo.Sum);
            cBoxAggregation.Items.Add(FunctionInfo.Avg);
            cBoxAggregation.Items.Add(FunctionInfo.Max);
            cBoxAggregation.Items.Add(FunctionInfo.Min);
            cBoxAggregation.Items.Add(FunctionInfo.Count);
            cBoxAggregation.Items.Add(FunctionInfo.None);
            cBoxAggregation.SelectedIndex = 0;
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            cBoxGroup.Enabled = rButton_Group.Checked;
            cBoxAggregation.Enabled = rButton_Aggregation.Checked;

            rButton_TopToBottom.Enabled = !rButton_Aggregation.Checked;
            rButton_LeftToRight.Enabled = !rButton_Aggregation.Checked;
            rButton_None.Enabled = !rButton_Aggregation.Checked;
        }

        void FpSpread_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            if (ActiveElement != null)
            {
                panel1.Visible = (ActiveElement.Value is DataColumn);

                //扩展属性
                if (ActiveElement.ExpandOrientation.Orientation == ExpandOrientation.LeftToRight)
                {
                    rButton_LeftToRight.Checked = true;
                }
                else if (ActiveElement.ExpandOrientation.Orientation == ExpandOrientation.TopToBottom)
                {
                    rButton_TopToBottom.Checked = true;
                }
                else if (ActiveElement.ExpandOrientation.Orientation == ExpandOrientation.None)
                {
                    rButton_None.Checked = true;
                }

                //数据列属性
                if (ActiveElement.Value is DataColumn)
                {
                    DataColumn data = ActiveElement.Value as DataColumn;

                    rButton_LeftToRight.Enabled = (data.DataSetting != DataSetting.Aggregation);
                    rButton_TopToBottom.Enabled = (data.DataSetting != DataSetting.Aggregation);
                    rButton_None.Enabled = (data.DataSetting != DataSetting.Aggregation);

                    if (data.DataSetting == DataSetting.List)
                    {
                        rButton_List.Checked = true;
                    }
                    else if (data.DataSetting == DataSetting.Group)
                    {
                        rButton_Group.Checked = true;
                    }
                    else if (data.DataSetting == DataSetting.Aggregation)
                    {
                        rButton_Aggregation.Checked = true;
                        cBoxAggregation.SelectedItem = data.FunctionInfo;
                    }
                }

                //左父格属性
                if (ActiveElement.ExpandOrientation.isDefaultLeftParent)
                {
                    LeftCoordControl.setValue(ValueSource.Default, "");
                }
                else
                {
                    ValueSource source = (ActiveElement.ExpandOrientation.LeftParent == "" ? ValueSource.None : ValueSource.UseDefined);
                    Cell Cell = Report.ReportSheet.GetCellFromTag(null, ActiveElement.ExpandOrientation.LeftParent);
                    String cell = "";
                    if (Cell != null)
                        cell = Arabic_Numerals_Convert.Excel_Word_Numerals(Cell.Column.Index) + (Cell.Row.Index + 1).ToString();
                    LeftCoordControl.setValue(source, cell);
                }

                //上父格
                if (ActiveElement.ExpandOrientation.isDefaultTopParent)
                {
                    TopCoordControl.setValue(ValueSource.Default, "");
                }
                else
                {
                    ValueSource source = (ActiveElement.ExpandOrientation.TopParent == "" ? ValueSource.None : ValueSource.UseDefined);
                    Cell Cell = Report.ReportSheet.GetCellFromTag(null, ActiveElement.ExpandOrientation.TopParent);
                    String cell = "";
                    if (Cell != null)
                        cell = Arabic_Numerals_Convert.Excel_Word_Numerals(Cell.Column.Index) + (Cell.Row.Index + 1).ToString();

                    TopCoordControl.setValue(source, cell);
                }
            }
        }

        private void RadioButton_Click(object sender, EventArgs e)
        {
            if (ActiveElement != null)
            {
                if (sender == rButton_TopToBottom)
                {
                    ActiveElement.ExpandOrientation.Orientation = ExpandOrientation.TopToBottom;
                }
                else if (sender == rButton_LeftToRight)
                {
                    ActiveElement.ExpandOrientation.Orientation = ExpandOrientation.LeftToRight;
                }
                else if (sender == rButton_None)
                {
                    ActiveElement.ExpandOrientation.Orientation = ExpandOrientation.None;
                }
            }
        }

        private void CoordControl_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (ActiveElement != null)
            {
                if (sender == LeftCoordControl)
                {
                    switch (e.ValueSource)
                    {
                        case ValueSource.None:
                            ActiveElement.ExpandOrientation.LeftParent = "";
                            ActiveElement.ExpandOrientation.isDefaultLeftParent = false;
                            break;
                        case ValueSource.Default:
                            ActiveElement.ExpandOrientation.isDefaultLeftParent = true;
                            break;
                        case ValueSource.UseDefined:
                            ActiveElement.ExpandOrientation.LeftParent = Report.GetElementIndex(e.Value);
                            ActiveElement.ExpandOrientation.isDefaultLeftParent = false;
                            break;
                    }
                }
                else if (sender == TopCoordControl)
                {
                    switch (e.ValueSource)
                    {
                        case ValueSource.None:
                            ActiveElement.ExpandOrientation.TopParent = "";
                            ActiveElement.ExpandOrientation.isDefaultTopParent = false;
                            break;
                        case ValueSource.Default:
                            ActiveElement.ExpandOrientation.isDefaultTopParent = true;
                            break;
                        case ValueSource.UseDefined:
                            ActiveElement.ExpandOrientation.TopParent = Report.GetElementIndex(e.Value);
                            ActiveElement.ExpandOrientation.isDefaultTopParent = false;
                            break;
                    }
                }
            }
        }

        private void cBoxAggregation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveElement != null && ActiveElement.Value is DataColumn)
            {
                DataColumn data = ActiveElement.Value as DataColumn;
                String cell = ActiveElement.getLiteralCell();
                if (data.DataSetting == DataSetting.Aggregation)
                {
                    data.FunctionInfo = cBoxAggregation.SelectedItem as FunctionInfo;
                    ActiveSheet.Cells[cell].Invalidate();
                }
            }
        }
    }
}
