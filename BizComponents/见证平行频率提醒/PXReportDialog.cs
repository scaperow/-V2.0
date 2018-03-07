using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using Yqun.Common.ContextCache;
using Yqun.Interfaces;
using BizCommon;
using Yqun.Bases;
using System.IO;
using FarPoint.Win.Spread.Model;

namespace BizComponents
{
    public partial class PXReportDialog : Form
    {
        public PXReportDialog()
        {
            InitializeComponent();
        }

        private void PXReportDialog_Load(object sender, EventArgs e)
        {
            ComboBox_Segments.Items.Clear();
            ComboBox_Segments.Items.Add("全部标段");
            ComboBox_Segments.SelectedIndex = 0;
            DateTime start = DateTime.Parse("2013-7-1");
            if (DateTime.Now.AddDays(-30) > start)
            {
                start = DateTime.Now.AddDays(-30);
            }
            DateTimePicker_Start.Text = start.ToString("yyyy年M月dd日");
            DateTimePicker_End.Text = DateTime.Now.ToString("yyyy年M月dd日");

            FpSpread_Info.Columns.Count = 10;
            FpSpread_Info.ColumnHeader.RowCount = 1;
            
            FpSpread_Info.ColumnHeader.Cells[0, 0].Text = "标段";
            FpSpread_Info.ColumnHeader.Cells[0, 0].Tag = "segment";
            FpSpread_Info.Columns[0].Width = 100;

            FpSpread_Info.ColumnHeader.Cells[0, 1].Text = "监理单位";
            FpSpread_Info.ColumnHeader.Cells[0, 1].Tag = "jl";
            FpSpread_Info.Columns[1].Width = 150;

            FpSpread_Info.ColumnHeader.Cells[0, 2].Text = " 施工单位";
            FpSpread_Info.ColumnHeader.Cells[0, 2].Tag = "sg";
            FpSpread_Info.Columns[2].Width = 150;

            FpSpread_Info.ColumnHeader.Cells[0, 3].Text = "施工单位试验室";
            FpSpread_Info.ColumnHeader.Cells[0, 3].Tag = "testroom";
            FpSpread_Info.Columns[3].Width = 150;

            FpSpread_Info.ColumnHeader.Cells[0, 4].Text = "试验名称";
            FpSpread_Info.ColumnHeader.Cells[0, 4].Tag = "modelName";
            FpSpread_Info.Columns[4].Width = 250;

            FpSpread_Info.ColumnHeader.Cells[0, 5].Text = "标准平行频率(%)";
            FpSpread_Info.ColumnHeader.Cells[0, 5].Tag = "condition";
            FpSpread_Info.Columns[5].Width = 100;

            FpSpread_Info.ColumnHeader.Cells[0, 6].Text = "施工单位资料总数";
            FpSpread_Info.ColumnHeader.Cells[0, 6].Tag = "zjCount";
            FpSpread_Info.Columns[6].Width = 130;

            FpSpread_Info.ColumnHeader.Cells[0, 7].Text = "平行次数";
            FpSpread_Info.ColumnHeader.Cells[0, 7].Tag = "pxCount";
            FpSpread_Info.Columns[7].Width = 100;

            FpSpread_Info.ColumnHeader.Cells[0, 8].Text = "平行频率(%)";
            FpSpread_Info.ColumnHeader.Cells[0, 8].Tag = "frequency";
            FpSpread_Info.Columns[8].Width = 100;

            FpSpread_Info.ColumnHeader.Cells[0, 9].Text = "是否符合要求";
            FpSpread_Info.ColumnHeader.Cells[0, 9].Tag = "result";
            FpSpread_Info.Columns[9].Width = 100;

        }

        private void ComboBox_Segments_DropDown(object sender, EventArgs e)
        {
            ComboBox_Segments.Items.Clear();
            List<Prjsct> PrjsctList = DepositoryPrjsctInfo.QueryPrjscts(Yqun.Common.ContextCache.ApplicationContext.Current.InProject.Code);
            ComboBox_Segments.Items.Add("全部标段");
            ComboBox_Segments.Items.AddRange(PrjsctList.ToArray());
        }

        private void ComboBox_Segments_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox_Company.Items.Clear();
            ComboBox_Company.Items.Add("全部单位");

            if (ComboBox_Segments.SelectedItem is Prjsct)
            {
                Prjsct prjsct = ComboBox_Segments.SelectedItem as Prjsct;
                List<Orginfo> OrgInfos = DepositoryOrganInfo.QueryOrgans(prjsct.PrjsctCode, "");
                ComboBox_Company.Items.AddRange(OrgInfos.ToArray());
            }

            if (ComboBox_Company.Items.Count > 1)
                ComboBox_Company.SelectedIndex = 1;
            else
                ComboBox_Company.SelectedIndex = 0;
        }

        private void ComboBox_Company_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox_TestRooms.Items.Clear();
            ComboBox_TestRooms.Items.Add("全部试验室");

            if (ComboBox_Company.SelectedItem is Orginfo)
            {
                Orginfo Orginfo = ComboBox_Company.SelectedItem as Orginfo;
                List<PrjFolder> PrjFolders = DepositoryFolderInfo.QueryPrjFolders(Orginfo.DepCode, "");
                ComboBox_TestRooms.Items.AddRange(PrjFolders.ToArray());
            }

            if (ComboBox_TestRooms.Items.Count > 1)
                ComboBox_TestRooms.SelectedIndex = 1;
            else
                ComboBox_TestRooms.SelectedIndex = 0;
        }

        private void Button_Query_Click(object sender, EventArgs e)
        {
            ProgressScreen.Current.ShowSplashScreen();
            ProgressScreen.Current.SetStatus = "正在获取数据...";
            FpSpread_Info.OperationMode = OperationMode.ReadOnly;           

            String segment = "";
            String company = "";
            String testroom = "";
            if (ComboBox_Segments.SelectedItem is Prjsct)
            {
                segment = (ComboBox_Segments.SelectedItem as Prjsct).PrjsctCode;
            }
            if (ComboBox_Company.SelectedItem is Orginfo)
            {
                company = (ComboBox_Company.SelectedItem as Orginfo).DepCode;
            }
            if (ComboBox_TestRooms.SelectedItem is PrjFolder)
            {
                testroom = (ComboBox_TestRooms.SelectedItem as PrjFolder).FolderCode;
            }

            DateTime StartDate = DateTime.Parse(string.Format("{0}-{1}-{2} 00:00:00", DateTimePicker_Start.Value.Year, DateTimePicker_Start.Value.Month, DateTimePicker_Start.Value.Day));
            DateTime EndDate = DateTime.Parse(string.Format("{0}-{1}-{2} 23:59:59", DateTimePicker_End.Value.Year, DateTimePicker_End.Value.Month, DateTimePicker_End.Value.Day));
            if (DateTimePicker_Start.Value < DateTime.Parse("2013-7-1"))
            {
                MessageBox.Show("请查询2013年7月1号以后的数据");
                return;
            }
            DataTable Data = PXJZReportDataList.GetPXReportInfos(segment, company, testroom, StartDate, EndDate);
            if (Data != null)
            {
                FpSpread.ShowRow(FpSpread.GetActiveRowViewportIndex(), 0, VerticalPosition.Top);
                FpSpread_Info.Rows.Count = Data.Rows.Count;
                if (Data.Rows.Count > 0)
                {
                    
                    for (int i = 0; i < Data.Rows.Count; i++)
                    {
                        FpSpread_Info.Rows[i].HorizontalAlignment = CellHorizontalAlignment.Center;
                        for (int j = 0; j < FpSpread_Info.ColumnHeader.Columns.Count; j++)
                        {
                            String v = Data.Rows[i][FpSpread_Info.ColumnHeader.Cells[0, j].Tag.ToString()].ToString();
                            FpSpread_Info.Cells[i, j].Value = v;
                            if (v == "不满足")
                            {
                                FpSpread_Info.Rows[i].BackColor = Color.FromArgb(255, 192, 192);
                            }
                            else
                            {
                                FpSpread_Info.Rows[i].BackColor = Color.FromName(System.Drawing.KnownColor.Control.ToString());
                            }
                        }
                    }

                    for (int i = 0; i < Data.Rows.Count; i++)
                    {
                        FpSpread_Info.Rows[i].Tag = Data.Rows[i]["testroomID"].ToString() + "," + Data.Rows[i]["modelID"].ToString() + "," + Data.Rows[i]["modelName"].ToString() + "," + Data.Rows[i]["frequency"].ToString();
                    }
                }
                else
                {
                    ProgressScreen.Current.CloseSplashScreen();
                    this.Activate();
                    MessageBox.Show("无数据，请重新选择条件!");
                }
            }
            else
            {
                ProgressScreen.Current.CloseSplashScreen();
                this.Activate();
                MessageBox.Show("无数据，请重新选择条件!");
            }
            ProgressScreen.Current.CloseSplashScreen();
            this.Activate();
            if (Data == null)
                TotalCount.Text = string.Format("共 {0} 条记录", 0);
            else
                TotalCount.Text = string.Format("共 {0} 条记录", Data.Rows.Count);
        }

        private void FpSpread_CellDoubleClick(object sender, CellClickEventArgs e)
        {
              Row row = FpSpread.ActiveSheet.ActiveRow;
              if (row != null && row.Tag is String)
              {
                  string[] strArr=row.Tag.ToString().Split(',');

                  String testRoomID = strArr[0].ToString();
                  String modelID = strArr[1].ToString();
                  String modelName = strArr[2].ToString();
                  String frequency = strArr[3].ToString();


                  DateTime StartDate = DateTime.Parse(string.Format("{0}-{1}-{2} 00:00:00", DateTimePicker_Start.Value.Year, DateTimePicker_Start.Value.Month, DateTimePicker_Start.Value.Day));
                  DateTime EndDate = DateTime.Parse(string.Format("{0}-{1}-{2} 23:59:59", DateTimePicker_End.Value.Year, DateTimePicker_End.Value.Month, DateTimePicker_End.Value.Day));

                  PXChart chart = new PXChart(testRoomID, StartDate, EndDate, modelID, modelName, frequency);
                  chart.ShowDialog();
              }
        }

        private void Button_Export_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (DialogResult.OK == folderBrowserDialog1.ShowDialog())
            {
                String fullPath = folderBrowserDialog1.SelectedPath;
                String fileName = Path.Combine(fullPath, string.Format("平行频率({0}-{1}-{2}).xls", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
                FpSpread.SaveExcel(fileName, IncludeHeaders.ColumnHeadersCustomOnly);
                MessageBox.Show(string.Format("平行频率“{0}”导出完成", fileName));
            }
        }
    }
}
