using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BizCommon;

namespace BizComponents
{
    public partial class CaiJiChartView : Form
    {
        Guid caiJiID = Guid.Empty;

        public CaiJiChartView(Guid _caijiID)
        {
            InitializeComponent();
            caiJiID = _caijiID;
            this.ChartLineControl.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            this.ChartLineControl.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            ChartLineControl.Series[0].ToolTip = "力值: #VALY1";
        }

        private void InitChart()
        {
            this.ChartLineControl.Series[0].Points.Clear();
            this.ChartLineControl.ChartAreas[0].AxisX.Maximum = 260;
            this.ChartLineControl.ChartAreas[0].AxisY.Maximum = 300;
            this.ChartLineControl.ChartAreas[0].AxisX.Minimum = 0;
            this.ChartLineControl.ChartAreas[0].AxisX.Title = "时间";
            this.ChartLineControl.ChartAreas[0].AxisX.Interval = 20;
            this.ChartLineControl.ChartAreas[0].AxisY.Title = "力值";
            this.ChartLineControl.ChartAreas[0].Name = "曲线图";
            ChartLine(0, 0);
        }

        /// <summary>
        /// 画曲线图
        /// </summary>
        /// <param name="XValue"></param>
        /// <param name="YValue"></param>
        private void ChartLine(double XValue, double YValue)
        {
            try
            {
                if (XValue > this.ChartLineControl.ChartAreas[0].AxisX.Maximum)
                {
                    this.ChartLineControl.ChartAreas[0].AxisX.Maximum = XValue + 50;
                    this.ChartLineControl.ChartAreas[0].AxisX.Interval += 20;
                }
                if (YValue > this.ChartLineControl.ChartAreas[0].AxisY.Maximum)
                {
                    this.ChartLineControl.ChartAreas[0].AxisY.Maximum = YValue + 50;
                }

                this.ChartLineControl.Series[0].Points.AddXY(XValue, YValue);
                this.ChartLineControl.Invalidate();
            }
            catch
            {
              
            }
        }

        private void CaiJiChartView_Load(object sender, EventArgs e)
        {
            String str = CaiJiHelperClient.GetRealTimeTestData(caiJiID);

            string RealTimeData = str;
            try
            {
                str = BizCommon.JZCommonHelper.GZipDecompressString(RealTimeData);
            }
            catch
            {
                str = RealTimeData;
            }

            List<JZRealTimeData> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZRealTimeData>>(str);
            if (list!=null && list.Count > 0)
            {
                DateTime last = list[0].Time;
                for (int i = 0; i < list.Count; i++)
                {
                    ChartLine(Math.Round((list[i].Time - last).TotalMilliseconds / 1000, 2), list[i].Value);
                }
            }
        }
    }
}
