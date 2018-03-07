using System;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using ReportCommon;

namespace ReportComponents
{
    public partial class PageElementDialog : Form
    {
        GeneralCellType GeneralType = new GeneralCellType();
        ReportWindow ReportWindow = null;

        public PageElementDialog(ReportWindow reportWindow)
        {
            InitializeComponent();

            ReportWindow = reportWindow;
        }

        public SheetView ActiveSheet
        {
            get
            {
                return ReportWindow.reportDesignForm.ReportDesignPanel.ActiveSheet;
            }
        }

        public Report Report
        {
            get
            {
                return ReportWindow.report;
            }
        }

        private void CellPageDialog_Load(object sender, EventArgs e)
        {
            Cell Cell = ActiveSheet.ActiveCell;
            String Index = Report.GetElementIndex(ActiveSheet.ActiveRowIndex, ActiveSheet.ActiveColumnIndex);
            GridElement Element = Report.GetElement(Index);
            if (Element != null)
            {
                cBox_AfterRow.Checked = Element.PageBreak.IsAfterRow;
            }
        }

        private void cBox_AfterRow_Click(object sender, EventArgs e)
        {
            Cell Cell = ActiveSheet.ActiveCell;
            String Index = Report.GetElementIndex(ActiveSheet.ActiveRowIndex, ActiveSheet.ActiveColumnIndex);
            GridElement Element = Report.GetElement(Index);
            if (Element == null)
            {
                Element = new GridElement();
                Element.Index = Index;
                Element.Row = Cell.Row.Index;
                Element.Column = Cell.Column.Index;
                Element.RowSpan = Cell.RowSpan;
                Element.ColumnSpan = Cell.ColumnSpan;
                Element.Report = ActiveSheet;

                Cell.CellType = GeneralType;
            }

            Element.PageBreak.IsAfterRow = cBox_AfterRow.Checked;
            Report.SetElement(Element);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
