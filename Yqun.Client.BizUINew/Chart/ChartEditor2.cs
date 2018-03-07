using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Steema.TeeChart.Editors;
using Steema.TeeChart.Languages;
using System.Threading;

namespace Yqun.Client.BizUI
{
    public partial class ChartEditor2 : Form
    {
        ChartControl2 chartControl;
        public ChartEditor2(ChartControl2 Chart)
        {
            InitializeComponent();

            chartControl = Chart; 
            SetTeeChart(chartControl.Drawer);

            AppDomain.CurrentDomain.UnhandledException -= new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.ExceptionObject.ToString());
        }

        private void SetTeeChart(ChartDrawer2 Drawer)
        {
            if (Drawer == null)
                return;

            SuspendLayout();
            TeeChartContainer.Controls.Clear();
            ChineseSimp.Activate();
            Drawer.Chart.Dock = DockStyle.Fill;
            Drawer.Chart.Parent = TeeChartContainer;
            TeeChartContainer.Controls.Add(Drawer.Chart);
            chartController.Chart = Drawer.Chart;
            chartListBox1.Chart = Drawer.Chart;
            editor1.Chart = Drawer.Chart;
            ResumeLayout(true);
        }

        private void ChartEditor2_Load(object sender, EventArgs e)
        {
            tBox_Labels.Text = chartControl.ChartLabels;
        }

        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            chartControl.UpdateChartData();
            chartControl.ChartImage = chartControl.Drawer.DrawImage(chartControl.CellSize.Width, chartControl.CellSize.Height);
        }

        private void chartListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chartListBox1.SelectedSeries == null) return;

            string SeriesIndex = chartListBox1.SelectedSeries.GetHashCode().ToString();
            List<string> XYValue;

            //输入值
            if (chartControl.ChartInputData.ContainsKey(SeriesIndex))
            {
                XYValue = chartControl.ChartInputData[SeriesIndex];
            }
            else
            {
                XYValue = new List<string>();
                XYValue.Add("");
                XYValue.Add("");
                chartControl.ChartInputData.Add(SeriesIndex, XYValue);
            }

            tBoxXValue.Text = XYValue[0];
            tBoxYValue.Text = XYValue[1];

            //输出值
            if (chartControl.ChartOutputData.ContainsKey(SeriesIndex))
            {
                XYValue = chartControl.ChartOutputData[SeriesIndex];
            }
            else
            {
                XYValue = new List<string>();
                XYValue.Add("");
                XYValue.Add("");
                chartControl.ChartOutputData.Add(SeriesIndex, XYValue);
            }

            OutputXValue.Text = XYValue[0];
            OutputYValue.Text = XYValue[1];
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (chartListBox1.SelectedSeries != null)
            {
                if (chartListBox1.SelectedSeries != null)
                {
                    string SeriesIndex = chartListBox1.SelectedSeries.GetHashCode().ToString();

                    //输入值
                    if (chartControl.ChartInputData.ContainsKey(SeriesIndex))
                    {
                        if (sender == tBoxXValue)
                        {
                            chartControl.ChartInputData[SeriesIndex][0] = tBoxXValue.Text;
                        }
                        else if (sender == tBoxYValue)
                        {
                            chartControl.ChartInputData[SeriesIndex][1] = tBoxYValue.Text;
                        }
                    }

                    //输出值
                    if (chartControl.ChartOutputData.ContainsKey(SeriesIndex))
                    {
                        if (sender == OutputXValue)
                        {
                            chartControl.ChartOutputData[SeriesIndex][0] = OutputXValue.Text;
                        }
                        else if (sender == OutputYValue)
                        {
                            chartControl.ChartOutputData[SeriesIndex][1] = OutputYValue.Text;
                        }
                    }
                }
            }

            if (sender == tBox_Labels)
            {
                chartControl.ChartLabels = tBox_Labels.Text;
            }
        }

        private void chartListBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            chartListBox1.ShowEditor();
        }
    }
}
