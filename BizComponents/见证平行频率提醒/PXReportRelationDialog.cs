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
    public partial class PXReportRelationDialog : Form
    {
        string PXTestRoomCode;
        Guid ModuleID;
        public PXReportRelationDialog(string pxTestRoomCode, Guid moduleid)
        {
            InitializeComponent();
            PXTestRoomCode = pxTestRoomCode;
            ModuleID = moduleid;
        }

        private void PXReportDialog_Load(object sender, EventArgs e)
        {
            DateTime start = DateTime.Parse("2013-7-1");
            if (DateTime.Now.AddDays(-30) > start)
            {
                start = DateTime.Now.AddDays(-30);
            }
            DateTimePicker_Start.Text = start.ToString("yyyy年M月dd日");
            DateTimePicker_End.Text = DateTime.Now.ToString("yyyy年M月dd日");

            FpSpread_Info.Columns.Count = 10;
            FpSpread_Info.ColumnHeader.RowCount = 1;

            FpSpread_Info.ColumnHeader.Cells[0, 0].Text = "施工标段";
            FpSpread_Info.Columns[0].Width = 60;

            FpSpread_Info.ColumnHeader.Cells[0, 1].Text = "施工单位";
            FpSpread_Info.Columns[1].Width = 60;

            FpSpread_Info.ColumnHeader.Cells[0, 2].Text = "施工试验室";
            FpSpread_Info.Columns[2].Width = 80;

            FpSpread_Info.ColumnHeader.Cells[0, 3].Text = "平行委托编号";
            FpSpread_Info.Columns[3].Width = 100;

            FpSpread_Info.ColumnHeader.Cells[0, 4].Text = "平行报告编号";
            FpSpread_Info.Columns[4].Width = 100;

            FpSpread_Info.ColumnHeader.Cells[0, 5].Text = "平行报告日期";
            FpSpread_Info.Columns[5].Width = 80;

            FpSpread_Info.ColumnHeader.Cells[0, 6].Text = "施工委托编号";
            FpSpread_Info.Columns[6].Width = 100;

            FpSpread_Info.ColumnHeader.Cells[0, 7].Text = "施工报告编号";
            FpSpread_Info.Columns[7].Width = 100;

            FpSpread_Info.ColumnHeader.Cells[0, 8].Text = "施工报告日期";
            FpSpread_Info.Columns[8].Width = 80;

            FpSpread_Info.ColumnHeader.Cells[0, 9].Text = "平行时间";
            FpSpread_Info.Columns[9].Width = 80;

            FarPoint.Win.Spread.CellType.DateTimeCellType datetime = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            datetime.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.ShortDate;
            FpSpread_Info.Columns[5].CellType = datetime;
            FpSpread_Info.Columns[8].CellType = datetime;
            FpSpread_Info.Columns[9].CellType = datetime;

        }

        private void Button_Query_Click(object sender, EventArgs e)
        {
            ProgressScreen.Current.ShowSplashScreen();
            ProgressScreen.Current.SetStatus = "正在获取数据...";
            FpSpread_Info.OperationMode = OperationMode.ReadOnly;

            DateTime StartDate = DateTime.Parse(string.Format("{0}-{1}-{2} 00:00:00", DateTimePicker_Start.Value.Year, DateTimePicker_Start.Value.Month, DateTimePicker_Start.Value.Day));
            DateTime EndDate = DateTime.Parse(string.Format("{0}-{1}-{2} 23:59:59", DateTimePicker_End.Value.Year, DateTimePicker_End.Value.Month, DateTimePicker_End.Value.Day));
            string BGBH = txtBGBH.Text.Trim();
            DataTable Data = PXJZReportDataList.GetPXReportRelation(PXTestRoomCode, ModuleID, StartDate, EndDate,BGBH);
            if (Data != null)
            {
                //BizComponents.TextCellType text = new BizComponents.TextCellType();
                //text.ReadOnly = false;
                FpSpread.ShowRow(FpSpread.GetActiveRowViewportIndex(), 0, VerticalPosition.Top);
                FpSpread_Info.Rows.Count = Data.Rows.Count;
                if (Data.Rows.Count > 0)
                {
                    for (int i = 0; i < Data.Rows.Count; i++)
                    {
                        FpSpread_Info.Rows[i].HorizontalAlignment = CellHorizontalAlignment.Center;
                        for (int j = 0; j < FpSpread_Info.ColumnHeader.Columns.Count; j++)
                        {
                            String v = Data.Rows[i][j].ToString();
                            FpSpread_Info.Cells[i, j].Value = v;
                            FpSpread_Info.Cells[i, j].Locked = false;
                            //FpSpread_Info.Cells[i, j].CellType = text;
                        }
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

        private void Button_Export_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (DialogResult.OK == folderBrowserDialog1.ShowDialog())
            {
                String fullPath = folderBrowserDialog1.SelectedPath;
                String fileName = Path.Combine(fullPath, string.Format("平行关系对应查询({0}-{1}-{2}).xls", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
                FpSpread.SaveExcel(fileName, IncludeHeaders.ColumnHeadersCustomOnly);
                MessageBox.Show(string.Format("平行关系对应查询“{0}”导出完成", fileName));
            }
        }
    }
}
