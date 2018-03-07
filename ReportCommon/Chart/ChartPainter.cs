using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using Steema.TeeChart.Styles;
using FarPoint.Win;
using Yqun.Bases;
using Steema.TeeChart;
using System.Xml;
using System.Data;
using System.Collections.ObjectModel;

namespace ReportCommon.Chart
{
    [Serializable]
    public class ChartPainter : IPainter, ICloneable
    {
        internal Steema.TeeChart.TChart tChart = new Steema.TeeChart.TChart();
        internal Dictionary<string, ChartAxisAtt> _AxisInfoList = new Dictionary<string, ChartAxisAtt>();

        public Steema.TeeChart.TChart TChart
        {
            get
            {
                return tChart;
            }
        }

        public ReadOnlyCollection<ChartAxisAtt> ChartAxisAtts
        {
            get
            {
                List<ChartAxisAtt> AxisInfos = new List<ChartAxisAtt>(_AxisInfoList.Values);
                return AxisInfos.AsReadOnly();
            }
        }

        protected string SerializeTeeChart(TChart TChart)
        {
            string Result;
            tChart.Export.Template.IncludeData = true;
            using (MemoryStream ms = new MemoryStream())
            {
                tChart.Export.Template.Save(ms);
                Result = Convert.ToBase64String(ms.ToArray());
                ms.Close();
            }

            return Result;
        }

        protected void Deserialize(string data, TChart TChart)
        {
            byte[] bytes = Convert.FromBase64String(data);
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                TChart.Import.Template.Load(ms);
                ms.Close();
            }
        }

        public void UpdateDataSource()
        {
            foreach (Series series in tChart.Series)
            {
                String seriesIndex = series.Tag.ToString();
                if (_AxisInfoList.ContainsKey(seriesIndex))
                {
                    ChartAxisAtt AxisAtt = _AxisInfoList[seriesIndex];
                    if (AxisAtt.DataList == null)
                        AxisAtt.DataList = new DataList();

                    series.Clear();
                    if (series.Function == null)
                    {
                        series.XValues.DataMember = AxisAtt.DataList.ColXData.ToString();
                        series.YValues.DataMember = AxisAtt.DataList.ColYData.ToString();
                        series.LabelMember = AxisAtt.DataList.ColLabels.ToString();

                        series.DataSource = AxisAtt.DataList.Data;
                    }
                }
            }
        }

        public Image Paint(int width, int height)
        {
            foreach (Series series in tChart.Series)
            {
                series.CheckDataSource();
            }

            Steema.TeeChart.Export.BitmapFormat bitMap = tChart.Export.Image.Bitmap;
            bitMap.Height = height;
            bitMap.Width = width;
            MemoryStream ms = new MemoryStream();
            bitMap.Save(ms);
            Image img = Bitmap.FromStream(ms);
            Graphics g = Graphics.FromImage(img);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.DrawRectangle(Pens.White, 0, 0, width - 1, height - 1);
            g.Dispose();

            return img;
        }

        public object Clone()
        {
            ChartPainter ChartPainter = new ChartPainter();
            TChart _TChart = new TChart();
            Deserialize(SerializeTeeChart(tChart), _TChart);
            ChartPainter.tChart = _TChart;
            ChartPainter._AxisInfoList = BinarySerializer.Deserialize(BinarySerializer.Serialize(_AxisInfoList)) as Dictionary<string, ChartAxisAtt>;
            return ChartPainter;
        }
    }
}
