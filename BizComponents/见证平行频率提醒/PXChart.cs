using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BizComponents
{
    public partial class PXChart : Form
    {
        String testRoomID = "";
        
        DateTime start = DateTime.Now;
        DateTime end = DateTime.Now;
        String modelID = "";
        String modelName = "";
        String frequency = "";
        DataTable Data = new DataTable();
        int count = 3;
        public PXChart(String testRoomID, DateTime start, DateTime end, String modelID, String modelName, String frequency)
        {
            this.testRoomID = testRoomID;
            this.start = start;
            this.end = end;
            this.modelID = modelID;
            this.modelName = modelName;
            this.frequency = frequency;
            InitializeComponent();

        }
        //柱状原始
        private void PXChart_Load(object sender, EventArgs e)
        {
            tChart1.Text = modelName;
            this.Text = modelName;

            bar1.Title = "自检";
           
            line1.Title = "平行";
          
            tChart1.Chart.Aspect.View3D = false;
            tChart1.Axes.Bottom.Title.Text = "时间";
            tChart1.Axes.Left.Title.Text = "数量（个）";
            Data = PXJZReportDataList.GetPXJZReportChart(testRoomID, start, end, modelID);
            if (Data != null)
            {
                bar1.CustomBarWidth = 5;
                tChart1.Axes.Left.Minimum = 0;
                tChart1.Axes.Left.Increment = 1;
                line1.LinePen.Width = 3;
                line1.Marks.Visible = true;
                tChart1.Axes.Bottom.Increment = 1;
                tChart1.Axes.Bottom.Labels.DateTimeFormat = "yyyy-MM-dd";
                tChart1.Axes.Bottom.Automatic = true;
                foreach (DataRow item in Data.Rows)
                {
                    bar1.Add(DateTime.Parse(item["chartDate"].ToString()), Int32.Parse(item["zjCount"].ToString()), Color.Green);
                    line1.Add(DateTime.Parse(item["chartDate"].ToString()), Int32.Parse(item["pxjzCount"].ToString()), Color.Red);

                }
            }
            tChart1.Axes.Bottom.Labels.Angle = 270;  //设置时间横向和竖向  
            bar1.Color = Color.Green;
            line1.Color = Color.Red;
            Double result = GetQulify(Data, start, end, count);
            label1.Text = "平行频率质量系数：" + ((Int32)result).ToString();
            label3.Text = "平行频率(%)：" + frequency;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text.Trim(), out count))
            {
                count = int.Parse(textBox1.Text.Trim());
            }
            else
            {
                count = 3;
            }
            Double result = GetQulify(Data, start, end, count);
            label1.Text = "平行频率质量系数：" + ((Int32)result).ToString();
        }

        private Double GetQulify(DataTable dt, DateTime startdate, DateTime enddate, int count)
        {
            Double result = 0;
            startdate = DateTime.Parse(dt.Rows[0]["chartDate"].ToString());
            enddate = DateTime.Parse(dt.Rows[dt.Rows.Count - 1]["chartDate"].ToString()).AddDays(1);
            List<DateList> list = SplitDate(startdate, enddate, count);
            if (list != null)
            {
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        DateTime chartDate = DateTime.Parse(dr["chartDate"].ToString());
                        Double px = Int32.Parse(dr["pxjzCount"].ToString());
                        Double zj = Int32.Parse(dr["zjCount"].ToString());
                        list = GetNumber(list, chartDate, px, zj);
                    }
                    list = GetB35(list);
                    Double sum = 0;
                    foreach (DateList item in list)
                    {
                        sum += item.PXZJNumber;
                    }
                    Double C3 = sum / list.Count;

                    Double sum1 = 0;
                    foreach (DateList item in list)
                    {
                        sum1 += (C3 - item.PXZJNumber) * (C3 - item.PXZJNumber);
                    }

                    Double G3 = sum1 / list.Count;
                    Double F4 = Math.Sqrt(G3);
                    if (C3 == 0)
                    {
                        result = 0;
                    }
                    else
                    {
                        result = 100 - 10 * F4;
                    }
                }
            }
            return result;
        }

        private List<DateList> GetB35(List<DateList> list)
        {
            foreach (DateList item in list)
            {
                if (item.ZNumber != 0)
                {
                    item.PXZJNumber = (item.PNumber / item.ZNumber)*10;
                }
            }
            return list;
        }
        private List<DateList> SplitDate(DateTime startdate, DateTime enddate, int num)
        {
            List<DateList> list = new List<DateList>();
            if (enddate.CompareTo(startdate) > 0)
            {
                TimeSpan s = enddate - startdate;
                int n = s.Days;
                if (num>n)
                {
                    num = n;
                }
                int m = n / num;
                for (int i = 1; i <= num; i++)
                {
                    DateList item = new DateList();
                    if (i == 1)
                    {
                        item.StartDate = startdate;
                        item.EndDate = startdate.AddDays(m);
                    }
                    else if (i == num)
                    {
                        item.StartDate = list[i - 2].EndDate;
                        item.EndDate = enddate;
                    }
                    else
                    {
                        item.StartDate = list[i - 2].EndDate;
                        item.EndDate = list[i - 2].EndDate.AddDays(m);
                    }
                    list.Add(item);
                }
            }
            else
            {
                list = null;
            }
            return list;
        }

        private List<DateList> GetNumber(List<DateList> list, DateTime chartDate, Double px, Double zj)
        {
            foreach (DateList item in list)
            {
                if (item.StartDate.CompareTo(chartDate) != 1 && item.EndDate.CompareTo(chartDate) == 1)
                {
                    item.PNumber += px;
                    item.ZNumber += zj;
                }
            }

            return list;
        }
    }
}


public class DateList
{
    public DateTime StartDate
    {
        get;
        set;
    }
    public DateTime EndDate
    {
        get;
        set;
    }

    public Double PNumber
    {
        get;
        set;
    }

    public Double ZNumber
    {
        get;
        set;
    }

    public Double PXZJNumber
    {
        get;
        set;
    }

}