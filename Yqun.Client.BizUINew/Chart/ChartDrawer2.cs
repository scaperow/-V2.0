using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using Steema.TeeChart.Styles;

namespace Yqun.Client.BizUI
{
    public class ChartDrawer2
    {
        //获得图片
        public Image DrawImage(int Width, int Height)
        {
            foreach (Series series in tChart.Series)
            {
                series.CheckDataSource();
            }
         
            Steema.TeeChart.Export.BitmapFormat bitMap = tChart.Export.Image.Bitmap;
            bitMap.Height = Height;
            bitMap.Width = Width;
            MemoryStream ms = new MemoryStream();
            bitMap.Save(ms);
            Image img= Bitmap.FromStream(ms);
            Graphics g = Graphics.FromImage(img);
            g.DrawRectangle(Pens.White, 0, 0, Width - 1, Height - 1);
            g.Dispose();

            return img;
        }

        //序列化
        public string Serialize()
        {
            string Result;
            Chart.Export.Template.IncludeData = true;
            using (MemoryStream ms = new MemoryStream())
            {
                Chart.Export.Template.Save(ms);
                Result = Convert.ToBase64String(ms.ToArray());
                ms.Close();
            }

            return Result;
        }

        //反序列化
        public void Deserialize(string data)
        {
            byte[] bytes = Convert.FromBase64String(data);
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                Chart.Import.Template.Load(ms);
                ms.Close();
            }
        }

        //3.5绘图控件
        Steema.TeeChart.TChart tChart = new Steema.TeeChart.TChart();
        public Steema.TeeChart.TChart Chart
        {
            get
            {
                return tChart;
            }
        }
   }
}
