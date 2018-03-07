using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BizComponents;
using FarPoint.Win.Spread;
using FarPoint.Win;
using FarPoint.Win.Spread.DrawingSpace;

namespace BizModules
{
    public partial class SupervisionReportViewer : Form
    {
        public SupervisionReportViewer()
        {
            InitializeComponent();
        }

        private void SupervisionReportViewer_Load(object sender, EventArgs e)
        {
            String Code = Yqun.Common.ContextCache.ApplicationContext.Current.UserCode;
            String SheetStyle = DepositorySamplingFrequencyInfo.SupervisionReportInfo(Code);

            if (!string.IsNullOrEmpty(SheetStyle))
            {
                myCell1.Sheets.Clear();
                SheetView SheetView = Serializer.LoadObjectXml(typeof(SheetView), SheetStyle, "SheetView") as SheetView;
                myCell1.Sheets.Add(SheetView);
            }
        }

        private void ToolStripItem_Click(object sender, EventArgs e)
        {
            if (sender == ExportExcelButton)
            {
                ExportExcelFile();
            }
            else if (sender == PrintButton)
            {
                PrintDocument();
            }
            else if (sender == PrintPreviewButton)
            {
                PrintPreviewDocument();
            }
        }

        private void ExportExcelFile()
        {
            SaveFileDialog FileDialog = new SaveFileDialog();
            FileDialog.Filter = "Excel files (*.xls)|*.xls";
            FileDialog.FilterIndex = 2;
            FileDialog.InitialDirectory = Application.StartupPath;
            FileDialog.RestoreDirectory = true;
            if (FileDialog.ShowDialog() == DialogResult.OK)
            {
                FpSpread fpspread = new FpSpread();
                fpspread.Sheets.Add(myCell1.ActiveSheet);
                fpspread.SaveExcel(FileDialog.FileName);
                fpspread.Sheets.Clear();
            }
        }

        private void PrintDocument()
        {
            myCell1.ActiveSheet.PrintInfo.Preview = false;
            myCell1.ActiveSheet.PrintInfo.ShowPrintDialog = true;

            int SheetIndex = myCell1.Sheets.IndexOf(myCell1.ActiveSheet);
            myCell1.PrintSheet(SheetIndex);
        }

        private void PrintPreviewDocument()
        {
            myCell1.ActiveSheet.PrintInfo.Preview = true;
            myCell1.ActiveSheet.PrintInfo.ShowPrintDialog = true;

            int SheetIndex = myCell1.Sheets.IndexOf(myCell1.ActiveSheet);
            myCell1.PrintSheet(SheetIndex);
        }
    }
}
