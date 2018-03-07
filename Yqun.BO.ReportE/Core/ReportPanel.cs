using System;
using FarPoint.Win.Spread;
using FarPoint.Win;
using System.Collections;
using System.Windows.Forms;
using ReportCommon;

namespace Yqun.BO.ReportE.Core
{
    public class ReportPanel
    {
        private FpSpread Document_FpSpread;
        private SheetView Document_SheetView;

        public ReportPanel()
        {
            Document_FpSpread = new FpSpread();
            Document_SheetView = new SheetView();
            Document_FpSpread.Sheets.Add(Document_SheetView);
        }

        internal FpSpread ReportSpread
        {
            get
            {
                return Document_FpSpread;
            }
        }

        internal SheetView Report
        {
            get
            {
                return Document_SheetView;
            }
        }

        private void InitializeComponent()
        {
            Document_FpSpread.SelectionBlockOptions = FarPoint.Win.Spread.SelectionBlockOptions.None;
            Document_FpSpread.Cursor = Cursors.Default;

            Document_SheetView.Reset();
            Document_SheetView.Cells[Document_SheetView.Rows.Count - 1, Document_SheetView.Columns.Count - 1].Value = DBNull.Value;
            Document_SheetView.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            Document_SheetView.HorizontalGridLine = new GridLine(GridLineType.None);
            Document_SheetView.VerticalGridLine = Document_SheetView.HorizontalGridLine;
            Document_SheetView.RowHeaderVisible = false;
            Document_SheetView.ColumnHeaderVisible = false;
        }

        public void Run(Report report, Hashtable parameters)
        {
            InitializeComponent();
            ResolveReport(report, parameters);
        }

        private void ResolveReport(Report report, Hashtable parameters)
        {
            try
            {
                SheetView sheetView = (SheetView)Serializer.LoadObjectXml(typeof(SheetView), Serializer.GetObjectXml(report.ReportSheet, "SheetView"), "SheetView");
                ReportEngine reportEngine = new ReportEngine(report.Configuration.DataSources, sheetView, parameters);
                PageReport pageReport = reportEngine.execute();

                PaginateReportIterator iterator = pageReport.generateReportPageIterator();

                DynamicValueList ColumnWidthList = ReportHelper.getColumnWidthList(pageReport.ToWorkSheet());
                DynamicValueList RowHeightList = ReportHelper.getRowHeightList(pageReport.ToWorkSheet());

                DrawingOP drawingOP = new DrawingOP(this, pageReport, RowHeightList, ColumnWidthList);
                while (iterator.hasNext())
                {
                    ReportPage reportPage = iterator.next() as ReportPage;
                    drawingOP.DrawReport(reportPage);
                }
            }
            catch(Exception ex)
            {
                Report.Reset();
                Report.ColumnCount = 6;
                Report.RowCount = 2;
                Report.RowHeaderVisible = false;
                Report.ColumnHeaderVisible = false;
                Report.Columns[0].Width = 700;
                Report.Rows[0].Height = 500;
                Report.Cells[0, 0].Text = "报表解析出错：" + ex.Message;
            }
        }
    }
}
