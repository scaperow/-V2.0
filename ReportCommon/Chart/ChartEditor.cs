using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Steema.TeeChart.Languages;
using Steema.TeeChart.Styles;

namespace ReportCommon.Chart
{
    public partial class ChartEditor : Form
    {
        ChartPainter _ChartPainter;

        public ChartEditor()
        {
            InitializeComponent();

            ComboBox_Functions.Items.Clear();
            ComboBox_Functions.Items.Add(FunctionInfo.Sum);
            ComboBox_Functions.Items.Add(FunctionInfo.Avg);
            ComboBox_Functions.Items.Add(FunctionInfo.Max);
            ComboBox_Functions.Items.Add(FunctionInfo.Min);
            ComboBox_Functions.Items.Add(FunctionInfo.Count);
            ComboBox_Functions.Items.Add(FunctionInfo.None);
        }

        public ChartPainter ChartPainter
        {
            get
            {
                return _ChartPainter;
            }
            set
            {
                _ChartPainter = value;
                if (value != null)
                {
                    SuspendLayout();
                    ChineseSimp.Activate();
                    TeeChartContainer.Controls.Clear();
                    _ChartPainter.TChart.Parent = TeeChartContainer;
                    _ChartPainter.TChart.Dock = DockStyle.Fill;
                    _ChartPainter.TChart.Graphics3D.BufferStyle = Steema.TeeChart.Drawing.BufferStyle.None;
                    TeeChartContainer.Controls.Add(_ChartPainter.TChart);
                    chartController.Chart = _ChartPainter.TChart;
                    chartListBox1.Chart = _ChartPainter.TChart;
                    editor1.Chart = _ChartPainter.TChart;
                    ResumeLayout(true);
                }
            }
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (chartListBox1.SelectedSeries == null)
                return;

            if (chartListBox1.SelectedSeries.Tag == null)
                chartListBox1.SelectedSeries.Tag = Guid.NewGuid().ToString();

            string SeriesIndex = chartListBox1.SelectedSeries.Tag.ToString();
            ChartAxisAtt AxisAtt;

            if (_ChartPainter._AxisInfoList.ContainsKey(SeriesIndex))
            {
                AxisAtt = _ChartPainter._AxisInfoList[SeriesIndex];
            }
            else
            {
                AxisAtt = new ChartAxisAtt();
                _ChartPainter._AxisInfoList.Add(SeriesIndex, AxisAtt);
            }

            if (rButton_BindData.Checked)
            {
                AxisAtt.DataDefinition = new ChartDataDefinition();
                Panel_BindData.BringToFront();
            }
            else
            {
                AxisAtt.DataDefinition = new ReportDataDefinition();
                Panel_DataArea.BringToFront();
            }
        }

        private void chartListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chartListBox1.SelectedSeries == null) 
                return;

            if (chartListBox1.SelectedSeries.Tag == null)
                chartListBox1.SelectedSeries.Tag = Guid.NewGuid().ToString();

            string SeriesIndex = chartListBox1.SelectedSeries.Tag.ToString();
            ChartAxisAtt AxisAtt;

            if (_ChartPainter._AxisInfoList.ContainsKey(SeriesIndex))
            {
                AxisAtt = _ChartPainter._AxisInfoList[SeriesIndex];
            }
            else
            {
                AxisAtt = new ChartAxisAtt();
                _ChartPainter._AxisInfoList.Add(SeriesIndex, AxisAtt);
            }

            rButton_BindData.Checked = AxisAtt.DataDefinition is ChartDataDefinition;
            rButton_BindArea.Checked = AxisAtt.DataDefinition is ReportDataDefinition;

            if (rButton_BindData.Checked)
            {
                ChartDataDefinition chartDataDefinition = AxisAtt.DataDefinition as ChartDataDefinition;
                TextBox_CatlogAxis.Text = string.Join("\n", chartDataDefinition.CatlogAxises.ToArray());
                TextBox_SeriesAxis.Text = chartDataDefinition.SeriesAxis;
                ComboBox_Functions.SelectedIndex = ComboBox_Functions.Items.IndexOf(chartDataDefinition.FunctionInfo);
                checkBox1.Checked = chartDataDefinition.DistinctCatlog;
            }
            else
            {
                ReportDataDefinition reportDataDefinition = AxisAtt.DataDefinition as ReportDataDefinition;
                TextBox_CatlogAxis1.Text = reportDataDefinition.CatlogArea;
                TextBox_SeriesAxis1.Text = reportDataDefinition.SeriesArea;
                TextBox_NameAxis1.Text = reportDataDefinition.SeriesNameArea;
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (chartListBox1.SelectedSeries != null)
            {
                if (chartListBox1.SelectedSeries.Tag == null)
                    chartListBox1.SelectedSeries.Tag = Guid.NewGuid().ToString();

                string SeriesIndex = chartListBox1.SelectedSeries.Tag.ToString();
                if (_ChartPainter._AxisInfoList.ContainsKey(SeriesIndex))
                {
                    ChartAxisAtt AxisAtt = _ChartPainter._AxisInfoList[SeriesIndex];
                    ChartDataDefinition chartDataDefinition = AxisAtt.DataDefinition as ChartDataDefinition;
                    ReportDataDefinition reportDataDefinition = AxisAtt.DataDefinition as ReportDataDefinition;

                    if (sender == TextBox_CatlogAxis)
                    {
                        chartDataDefinition.CatlogAxises.Clear();
                        chartDataDefinition.CatlogAxises.AddRange(TextBox_CatlogAxis.Lines);
                    }
                    else if (sender == TextBox_SeriesAxis)
                    {
                        chartDataDefinition.SeriesAxis = TextBox_SeriesAxis.Text;
                    }
                    else if (sender == TextBox_CatlogAxis1)
                    {
                        reportDataDefinition.CatlogArea = TextBox_CatlogAxis1.Text;
                    }
                    else if (sender == TextBox_SeriesAxis1)
                    {
                        reportDataDefinition.SeriesArea = TextBox_SeriesAxis1.Text;
                    }
                    else if (sender == TextBox_NameAxis1)
                    {
                        reportDataDefinition.SeriesNameArea = TextBox_NameAxis1.Text;
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chartListBox1.SelectedSeries != null)
            {
                if (chartListBox1.SelectedSeries.Tag == null)
                    chartListBox1.SelectedSeries.Tag = Guid.NewGuid().ToString();

                string SeriesIndex = chartListBox1.SelectedSeries.Tag.ToString();
                if (_ChartPainter._AxisInfoList.ContainsKey(SeriesIndex))
                {
                    ChartAxisAtt AxisAtt = _ChartPainter._AxisInfoList[SeriesIndex];
                    if (AxisAtt.DataDefinition is ChartDataDefinition)
                    {
                        ChartDataDefinition chartDataDefinition = AxisAtt.DataDefinition as ChartDataDefinition;
                        chartDataDefinition.DistinctCatlog = checkBox1.Checked;
                    }
                }
            }
        }

        private void ComboBox_Functions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chartListBox1.SelectedSeries != null)
            {
                if (chartListBox1.SelectedSeries.Tag == null)
                    chartListBox1.SelectedSeries.Tag = Guid.NewGuid().ToString();

                string SeriesIndex = chartListBox1.SelectedSeries.Tag.ToString();
                if (_ChartPainter._AxisInfoList.ContainsKey(SeriesIndex))
                {
                    ChartAxisAtt AxisAtt = _ChartPainter._AxisInfoList[SeriesIndex];
                    if (AxisAtt.DataDefinition is ChartDataDefinition)
                    {
                        ChartDataDefinition chartDataDefinition = AxisAtt.DataDefinition as ChartDataDefinition;
                        chartDataDefinition.FunctionInfo = ComboBox_Functions.SelectedItem as FunctionInfo;
                    }
                }
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
