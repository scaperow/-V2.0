using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.Model;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Steema.TeeChart.Styles;
using FarPoint.Win;
using FarPoint.Win.Spread.DrawingSpace;
using Yqun.Common.Encoder;

namespace Yqun.Client.BizUI
{
    public partial class ChartControl2 : UserControl
    {
        MyCell myCell;
        SheetView activeSheet;
        ChartDrawer2 drawer = new ChartDrawer2();

        Dictionary<string, List<string>> chartInputData = new Dictionary<string, List<string>>();
        Dictionary<string, List<string>> chartOutputData = new Dictionary<string, List<string>>();
        string chartLabels;
        Dictionary<string, object> SerializeInfo = new Dictionary<string, object>();

        public ChartControl2(SheetView sheet)
        {
            InitializeComponent();

            if (sheet != null)
            {
                activeSheet = sheet;
                myCell = GetParent(sheet);
                activeSheet.CellChanged += new SheetViewEventHandler(activeSheet_CellChanged);
                activeSheet.CellChanged += new SheetViewEventHandler(activeSheet_CellChanged);
                myCell.EditModeOff -= new EventHandler(myCell_EditModeOff);
                myCell.EditModeOff += new EventHandler(myCell_EditModeOff);
            }
        }

        void activeSheet_CellChanged(object sender, SheetViewEventArgs e)
        {
            if (myCell == null)
                return;

            CellRange SelectRange = new CellRange(e.Row, e.Column, 1, 1);
            if (!SelectRange.IsValidRange(myCell.ActiveSheet))
                return;

            Boolean Have = false;
            foreach (List<string> ls in chartInputData.Values)
            {
                List<string> x = new List<string>(ls[0].ToLower().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                List<string> y = new List<string>(ls[1].ToLower().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                String r = Arabic_Numerals_Convert.Excel_Word_Numerals(e.Column) + (e.Row + 1).ToString();
                if (x.Contains(r.ToLower()) || y.Contains(r.ToLower()))
                {
                    Have = true;
                    break;
                }
            }

            if (!Have)
                return;

            UpdateChartData();
            UpdateChartImage();
        }

        void myCell_EditModeOff(object sender, EventArgs e)
        {
            if (myCell == null)
                return;

            CellRange SelectRange = new CellRange(myCell.ActiveSheet.ActiveRowIndex, myCell.ActiveSheet.ActiveColumnIndex, 1, 1);
            if (!SelectRange.IsValidRange(myCell.ActiveSheet)) 
                return;

            Boolean Have = false;
            foreach (List<string> ls in chartInputData.Values)
            {
                List<string> x = new List<string>(ls[0].ToLower().Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries));
                List<string> y = new List<string>(ls[1].ToLower().Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries));
                String r = Arabic_Numerals_Convert.Excel_Word_Numerals(myCell.ActiveSheet.ActiveColumnIndex) + (myCell.ActiveSheet.ActiveRowIndex + 1).ToString();
                if (x.Contains(r.ToLower()) || y.Contains(r.ToLower()))
                {
                    Have = true;
                    break;
                }
            }

            if (!Have)
                return;

            UpdateChartData();
            UpdateChartImage();
        }

        //获得SheetView所在的FpSpread控件
        public MyCell GetParent(SheetView sheet)
        {
            SpreadView spreadView = sheet.ContainingViews[sheet.ContainingViews.Length - 1];
            return spreadView.Owner as MyCell;
        }

        public void RefreshFpSpread(SheetView Sheet)
        {
            if (Sheet != null)
            {
                activeSheet = Sheet;
                myCell = GetParent(Sheet);
                activeSheet.CellChanged += new SheetViewEventHandler(activeSheet_CellChanged);
                activeSheet.CellChanged += new SheetViewEventHandler(activeSheet_CellChanged);
                myCell.EditModeOff -= new EventHandler(myCell_EditModeOff);
                myCell.EditModeOff += new EventHandler(myCell_EditModeOff);
                
            }
        }

        #region 通知图表重画

        public void UpdateChartData()
        {
            if (ActiveSheet != null)
            {
                //更新输入值
                foreach (Series series in drawer.Chart.Series)
                {
                    string seriesIndex = series.GetHashCode().ToString();
                    if (chartInputData.ContainsKey(seriesIndex))
                    {
                        series.Clear();
                        if (series.Function == null)
                        {
                            DataList List = new DataList();
                            if (!(series.DataSource is DataTable))
                            {
                                series.DataSource = List.Data;
                            }
                            else
                            {
                                List.SetData(series.DataSource as DataTable);
                            }

                            List.DeleteData();

                            string[] XTag = chartInputData[seriesIndex][0].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                            string[] YTag = chartInputData[seriesIndex][1].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                            string[] XLabels = chartLabels.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                            int DataLength = Math.Min(XTag.Length, YTag.Length);
                            for (int i = 0; i < DataLength; i++)
                            {
                                double Xtemp = 0, Ytemp = 0;
                                string XText = "", YText = "";
                                string XLabel = "";

                                try
                                {
                                    XText = ActiveSheet.Cells[XTag[i].Trim('\r', '\n')].Text;
                                }
                                catch
                                {
                                    XText = XTag[i].Trim('\r', '\n');
                                }

                                try
                                {
                                    YText = ActiveSheet.Cells[YTag[i].Trim('\r', '\n')].Text;
                                }
                                catch
                                {
                                    YText = YTag[i].Trim('\r', '\n');
                                }

                                try
                                {
                                    XLabel = ActiveSheet.Cells[XLabels[i].Trim('\r', '\n')].Text;
                                }
                                catch
                                {
                                    try
                                    {
                                        XLabel = XLabels[i].Trim('\r', '\n');
                                    }
                                    catch
                                    {
                                        XLabel = XText;
                                    }
                                }

                                if (double.TryParse(XText, out Xtemp) &&
                                    double.TryParse(YText, out Ytemp)
                                    )
                                {
                                    List.AddData(Xtemp, Ytemp, XLabel);
                                }
                                else
                                {
                                    continue;
                                }
                            }

                            series.XValues.DataMember = List.ColXData.ToString();
                            series.YValues.DataMember = List.ColYData.ToString();
                            series.LabelMember = List.ColLabels.ToString();

                            series.DataSource = List.Data;
                        }
                    }
                }

                //更新输出值
                foreach (Series series in drawer.Chart.Series)
                {
                    series.CheckDataSource();
                }

                //将输出值更新到表单
                foreach (Series series in drawer.Chart.Series)
                {
                    string seriesIndex = series.GetHashCode().ToString();
                    if (chartOutputData.ContainsKey(seriesIndex))
                    {
                        if (series.Function != null)
                        {
                            string[] XTag = chartOutputData[seriesIndex][0].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                            string[] YTag = chartOutputData[seriesIndex][1].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                            int DataLength = Math.Min(XTag.Length, YTag.Length);
                            for (int i = 0; i < DataLength; i++)
                            {
                                string XText = "", YText = "";

                                XText = XTag[i].Trim('\r', '\n');
                                YText = YTag[i].Trim('\r', '\n');

                                try
                                {
                                    ActiveSheet.Cells[XText].Value = series[i].X.ToString();
                                    ActiveSheet.Cells[YText].Value = series[i].Y.ToString();
                                }
                                catch
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion 通知图表重画

        public ChartDrawer2 Drawer
        {
            get
            {
                return drawer;
            }
        }

        public Dictionary<string, List<string>> ChartInputData
        {
            get
            {
                return chartInputData;
            }
        }

        public Dictionary<string, List<string>> ChartOutputData
        {
            get
            {
                return chartOutputData;
            }
        }

        public String ChartLabels
        {
            get
            {
                return chartLabels;
            }
            set
            {
                chartLabels = value;
            }
        }

        public SheetView ActiveSheet
        {
            get
            {
                return activeSheet;
            }
            set
            {
                activeSheet = value;
            }
        }

        Size _Size = Size.Empty;
        public Size CellSize
        {
            get
            {
                return _Size;
            }
            set
            {
                _Size = value;
            }
        }

        System.Drawing.Image _ChartImage;
        public virtual System.Drawing.Image ChartImage
        {
            get
            {
                return _ChartImage;
            }
            set
            {
                _ChartImage = value;
            }
        }

        public virtual void UpdateChartImage()
        {
            _ChartImage = drawer.DrawImage(CellSize.Width, CellSize.Height);
        }

        #region 序列化图表数据

        public virtual string GetSerializeXML()
        {
            string chartxml = drawer.Serialize();

            Dictionary<string, List<string>> Data = new Dictionary<string, List<string>>();
            foreach (Series series in drawer.Chart.Series)
            {
                string seriesIndex = series.GetHashCode().ToString();
                if (chartInputData.ContainsKey(seriesIndex))
                    Data.Add(seriesIndex, chartInputData[seriesIndex]);
            }

            if (SerializeInfo.ContainsKey("相关信息"))
                SerializeInfo["相关信息"] = Data;
            else
                SerializeInfo.Add("相关信息", Data);

            Data = new Dictionary<string, List<string>>();
            foreach (Series series in drawer.Chart.Series)
            {
                string seriesIndex = series.GetHashCode().ToString();
                if (chartOutputData.ContainsKey(seriesIndex))
                    Data.Add(seriesIndex, chartOutputData[seriesIndex]);
            }

            if (SerializeInfo.ContainsKey("相关信息1"))
                SerializeInfo["相关信息1"] = Data;
            else
                SerializeInfo.Add("相关信息1", Data);

            if (SerializeInfo.ContainsKey("标签信息"))
                SerializeInfo["标签信息"] = chartLabels;
            else
                SerializeInfo.Add("标签信息", chartLabels);

            if (SerializeInfo.ContainsKey("图表控件"))
                SerializeInfo["图表控件"] = chartxml;
            else
                SerializeInfo.Add("图表控件", chartxml);

            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            formatter.Serialize(ms, SerializeInfo);
            return System.Convert.ToBase64String(ms.ToArray());
        }

        public virtual void Deserialize(string xml) 
        {
            byte[] bytes = System.Convert.FromBase64String(xml);
            MemoryStream ms = new MemoryStream(bytes);
            BinaryFormatter formatter = new BinaryFormatter();
            SerializeInfo = (Dictionary<string, object>)formatter.Deserialize(ms);

            chartInputData = new Dictionary<string, List<string>>();
            if (SerializeInfo.ContainsKey("相关信息"))
            {
                if (SerializeInfo["相关信息"] is Dictionary<string, List<string>>)
                {
                    chartInputData = SerializeInfo["相关信息"] as Dictionary<string, List<string>>;
                }
            }

            chartOutputData = new Dictionary<string, List<string>>();
            if (SerializeInfo.ContainsKey("相关信息1"))
            {
                if (SerializeInfo["相关信息1"] is Dictionary<string, List<string>>)
                {
                    chartOutputData = SerializeInfo["相关信息1"] as Dictionary<string, List<string>>;
                }
            }

            chartLabels = "";
            if (SerializeInfo.ContainsKey("标签信息"))
            {
                chartLabels = System.Convert.ToString(SerializeInfo["标签信息"]);
            }

            string chartxml = "";
            if (SerializeInfo.ContainsKey("图表控件"))
            {
                chartxml = System.Convert.ToString(SerializeInfo["图表控件"]);
            }

            drawer.Deserialize(chartxml);

            int i = 0, j = 0;
            List<List<string>> CellData = new List<List<string>>(chartInputData.Values);
            Dictionary<string, List<string>> Data = new Dictionary<string, List<string>>();
            List<List<string>> CellData1 = new List<List<string>>(chartOutputData.Values);
            Dictionary<string, List<string>> Data1 = new Dictionary<string, List<string>>();
            foreach (Series series in drawer.Chart.Series)
            {
                string seriesIndex = series.GetHashCode().ToString();

                if (i < CellData.Count)
                {
                    Data.Add(seriesIndex, CellData[i++]);
                }

                if (j < CellData1.Count)
                {
                    Data1.Add(seriesIndex, CellData1[j++]);
                }
            }

            chartInputData = Data;
            chartOutputData = Data1;
            UpdateChartData();
            UpdateChartImage();
        }

        #endregion 序列化图表数据
    }

    public class DataList
    {
        private DataTable sourceTable;
        private DataColumn colXData;
        private DataColumn colYData;
        private DataColumn colLabels;

        public DataList()
        {
            // Create DataTable
            sourceTable = new DataTable("sourceTable"); 
            colXData = new DataColumn("XData", Type.GetType("System.Double"));
            colYData = new DataColumn("YData", Type.GetType("System.Double"));
            colLabels = new DataColumn("Labels", Type.GetType("System.String"));

            sourceTable.Columns.Add(colXData);
            sourceTable.Columns.Add(colYData);
            sourceTable.Columns.Add(colLabels);
        }

        public void SetData(DataTable Data)
        {
            sourceTable = Data;
        }

        public void AddData(double x, double y,string xLabel)
        {
            DataRow NewRow = sourceTable.NewRow();
            NewRow["XData"] = x;
            NewRow["YData"] = y;
            NewRow["Labels"] = xLabel;
            sourceTable.Rows.Add(NewRow);
        }

        public void DeleteData(int Index)
        {
            if (Index < sourceTable.Rows.Count)
            {
                sourceTable.Rows.RemoveAt(Index);
            }
        }

        public void DeleteData()
        {
            sourceTable.Rows.Clear();
        }

        public DataTable Data
        {
            get
            {
                return sourceTable;
            }
        }

        public DataColumn ColXData
        {
            get
            {
                return colXData;
            }
        }

        public DataColumn ColYData
        {
            get
            {
                return colYData;
            }
        }

        public DataColumn ColLabels
        {
            get
            {
                return colLabels;
            }
        }
    }
}
