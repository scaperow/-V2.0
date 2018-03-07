using System.Collections.Generic;
using FarPoint.Win.Spread;
using System.Drawing;
using FarPoint.Win.Spread.Model;
using FarPoint.Win;
using ReportCommon;
using System.Windows.Forms;
using System;

namespace Yqun.BO.ReportE.Core
{
    public class DrawingOP
    {
        private ReportPanel reportPanel;
        private PageReport pageReport;
        private PaginateReportIterator iterator;
        private Rectangle[,] PageRectangles;
        private DynamicValueList rowHeightList;
        private DynamicValueList columnWidthList;

        public DrawingOP(ReportPanel reportPanel, PageReport pageReport, DynamicValueList rowHeightList, DynamicValueList columnWidthList)
        {
            this.reportPanel = reportPanel;
            this.pageReport = pageReport;
            this.iterator = pageReport.generateReportPageIterator();
            this.PageRectangles = new Rectangle[iterator.PageRowCount, iterator.PageColumnCount];
            this.rowHeightList = rowHeightList;
            this.columnWidthList = columnWidthList;

            DoPageInfo();
        }

        protected void DoPageInfo()
        {
            SheetView source_report = reportPanel.Report;
            FpSpread source_fpspread = reportPanel.ReportSpread;
            source_report.PrintInfo.CopyFrom(pageReport.getReportSettings());

            source_report.Rows.Count = 0;
            source_report.Columns.Count = 0;

            Dictionary<int, int> pageRowIndexs = new Dictionary<int, int>();
            Dictionary<int, int> pageColumnIndexs = new Dictionary<int, int>();
            while(iterator.hasNext())
            {
                ReportPage reportPage = iterator.next() as ReportPage;

                float[] columns = reportPage.getColumnWidthArray();
                float[] rows = reportPage.getRowHeightArray();

                if (!pageColumnIndexs.ContainsKey(reportPage.getPageColumnIndex()))
                {
                    pageColumnIndexs.Add(reportPage.getPageColumnIndex(), source_report.Columns.Count);

                    source_report.Columns.Add(source_report.Columns.Count, columns.Length);
                    for (int i = columns.Length - 1; i >= 0; i--)
                    {
                        source_report.Columns[source_report.Columns.Count - 1 - i].Width = columns[columns.Length - 1 - i];
                    }

                    source_report.Columns[pageColumnIndexs[reportPage.getPageColumnIndex()]].PageBreak = true;
                }

                if (!pageRowIndexs.ContainsKey(reportPage.getPageRowIndex()))
                {
                    pageRowIndexs.Add(reportPage.getPageRowIndex(), source_report.Rows.Count);

                    source_report.Rows.Add(source_report.Rows.Count, rows.Length);
                    for (int i = rows.Length - 1; i >= 0; i--)
                    {
                        source_report.Rows[source_report.Rows.Count - 1 - i].Height = rows[rows.Length - 1 - i];
                    }

                    source_report.Rows[pageRowIndexs[reportPage.getPageRowIndex()]].PageBreak = true;
                }

                Rectangle Rect = new Rectangle();
                Rect.X = pageRowIndexs[reportPage.getPageRowIndex()];
                Rect.Y = pageColumnIndexs[reportPage.getPageColumnIndex()];
                Rect.Width = rows.Length;
                Rect.Height = columns.Length;
                PageRectangles[reportPage.getPageRowIndex() - 1, reportPage.getPageColumnIndex() - 1] = Rect;
            }

            int FloatColumnCount = 0, FloatRowCount = 0;
            foreach (IElement Shape in pageReport.ToWorkSheet().DrawingContainer.ContainedObjects)
            {
                if (Shape is FloatElement)
                {
                    FloatElement localFloatElement = Shape as FloatElement;
                    Point[] arrayOfPoint = ReportHelper.calculateLastColumnAndRowOfFloatElement(pageReport.ToWorkSheet(), localFloatElement);
                    FloatColumnCount = Math.Max(FloatColumnCount, arrayOfPoint[0].X);
                    FloatRowCount = Math.Max(FloatRowCount, arrayOfPoint[0].Y);
                }
            }

            if (source_report.Columns.Count < FloatColumnCount)
                source_report.Columns.Add(source_report.Columns.Count, FloatColumnCount - source_report.Columns.Count);
            if (source_report.Rows.Count < FloatRowCount)
                source_report.Rows.Add(source_report.Rows.Count, FloatRowCount - source_report.Rows.Count);

            foreach (IElement Shape in pageReport.ToWorkSheet().DrawingContainer.ContainedObjects)
            {
                if (Shape is FloatElement)
                {
                    FloatElement localFloatElement = Shape as FloatElement;
                    FloatElement tmpfloatElement = localFloatElement.Clone() as FloatElement;
                    tmpfloatElement.Locked = true;
                    source_report.AddShape(tmpfloatElement);
                    tmpfloatElement.Update();
                }
            }
        }

        public void DrawReport(ReportPage reportPage)
        {
            SheetView source_report = reportPage.getSource_Report();
            Point Point1 = new Point(0, 0);

            FT[] column_line_array = reportPage.getColumn_Line_Array();
            FT[] row_line_array = reportPage.getRow_Line_Array();

            int PageRowIndex = reportPage.getPageRowIndex();
            int PageColumnIndex = reportPage.getPageColumnIndex();

            int top_row_line = 0;
            if (PageRowIndex == 0 && row_line_array.Length > 0)
            {
                top_row_line = row_line_array[0].from;
            }

            int left_column_line = 0;
            if (PageColumnIndex == 0 && column_line_array.Length > 0)
            {
                left_column_line = column_line_array[0].from;
            }

            for (int i = 0; i < column_line_array.Length; i++)
            {
                FT localFT1 = column_line_array[i];
                for (int j = 0; j < row_line_array.Length; j++)
                {
                    FT localFT2 = row_line_array[j];
                    intersectCell(source_report, PageRowIndex, PageColumnIndex, top_row_line, left_column_line, localFT1, localFT2, Point1);

                    Point1.X = localFT2.to - localFT2.from + 1;
                }

                Point1.X = 0;
                Point1.Y = localFT1.to - localFT1.from + 1;
            }
        }

        public void intersectCell(SheetView source_report, int pageRowIndex, int pageColumnIndex, int top_row_line, int left_column_line, FT localFT1, FT localFT2, Point Point1)
        {
            List<Cell> Cells = new List<Cell>();
            Rectangle rect = PageRectangles[pageRowIndex-1, pageColumnIndex-1];
            SheetView target_report = reportPanel.Report;
            for (int i = localFT2.from; i <= localFT2.to; i++)
            {
                for (int j = localFT1.from; j <= localFT1.to; j++)
                {
                    Cell source_cell = source_report.Cells[i, j];
                    CellRange source_spancell = source_report.GetSpanCell(i, j);
                    if (source_spancell != null)
                    {
                        source_cell = source_report.Cells[source_spancell.Row, source_spancell.Column];
                    }

                    if (!Cells.Contains(source_cell))
                        Cells.Add(source_cell);
                    else
                        continue;

                    Cell target_cell = target_report.Cells[rect.X + i - localFT2.from + Point1.X, rect.Y + j - localFT1.from + Point1.Y];

                    if (source_cell.Row.Index + source_cell.RowSpan > localFT2.to)
                    {
                        target_cell.RowSpan = localFT2.to - source_cell.Row.Index + 1;
                    }
                    else if (source_cell.Row.Index < localFT2.from)
                    {
                        target_cell.RowSpan = source_cell.Row.Index + source_cell.RowSpan - localFT2.from;
                    }
                    else
                    {
                        target_cell.RowSpan = source_cell.RowSpan;
                    }

                    if (source_cell.Column.Index + source_cell.ColumnSpan > localFT1.to)
                    {
                        target_cell.ColumnSpan = localFT1.to - source_cell.Column.Index + 1;
                    }
                    else if (source_cell.Column.Index < localFT1.from)
                    {
                        target_cell.ColumnSpan = source_cell.Column.Index + source_cell.ColumnSpan - localFT1.from;
                    }
                    else
                    {
                        target_cell.ColumnSpan = source_cell.ColumnSpan;
                    }

                    target_cell.Value = source_cell.Value;

                    target_cell.CellType = source_cell.CellType;
                    target_cell.BackColor = source_cell.BackColor;

                    target_cell.Border = GetNewBorder(source_cell, true, true, true, true);

                    target_cell.ForeColor = source_cell.ForeColor;
                    target_cell.Font = source_cell.Font;
                    target_cell.HorizontalAlignment = source_cell.HorizontalAlignment;
                    target_cell.VerticalAlignment = source_cell.VerticalAlignment;
                }
            }
        }

        private IBorder GetNewBorder(Cell source_cell, bool left, bool top, bool right, bool bottom)
        {
            IBorder border = source_cell.Border;
            IBorder newBorder = source_cell.Border;

            if (border is LineBorder)
            {
                LineBorder lineBorder = border as LineBorder;
                newBorder = new LineBorder(lineBorder.Color, lineBorder.Thickness, left, top, right, bottom);
            }
            else if (border is BevelBorder)
            {
                BevelBorder bevelBorder = border as BevelBorder;
                newBorder = new BevelBorder(bevelBorder.Type, bevelBorder.Highlight, bevelBorder.Shadow, bevelBorder.Thickness, left, top, right, bottom);
            }
            else if (border is ComplexBorder)
            {
                ComplexBorder complexBorder = border as ComplexBorder;
            }
            else if (border is CompoundBorder)
            {

            }
            else if (border is DoubleLineBorder)
            {

            }
            else if (border is RoundedLineBorder)
            {

            }

            return newBorder;
        }
    }
}
