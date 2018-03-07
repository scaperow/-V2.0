using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using FarPoint.Win;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using FarPoint.Win.Spread.Model;
using FarPoint.Win.SuperEdit;
using FarPoint.Excel;
using FarPoint.CalcEngine;
using FarPoint.PluginCalendar;
using FarPoint.PluginCalendar.WinForms;
using System.Drawing.Drawing2D;
using System.Collections;
using FarPoint.Win.Spread.DrawingSpace;
using System.ComponentModel;

namespace Yqun.Client.BizUI
{
    public class MyCell : FpSpread
    {
        public List<float> RowHeights = new List<float>();
        public List<float> ColumnWidths = new List<float>();

        float TotalWidth = 0f;
        float TotalHeight = 0f;

        float ScaleX = 100, ScaleY = 100;

        PointF p1, p2, p3;

        int LeftColumn, RightColumn;
        int TopRow, BottomRow;
        private JZWatermark watermark = null;
        public JZWatermark Watermark
        {
            get
            {
                return watermark;
            }
            set
            {
                watermark = value;
            }
        }

        public Boolean IsEditing { get; set; }

        public MyCell()
        {
            this.Sheets.Changed += new CollectionChangeEventHandler(Sheets_Changed);
            this.Sheets.Changing += new CollectionChangeEventHandler(Sheets_Changing);
            this.HorizontalScrollBar.Scroll += new ScrollEventHandler(HorizontalScrollBar_Scroll);
            this.VerticalScrollBar.Scroll += new ScrollEventHandler(VerticalScrollBar_Scroll);
            this.PrintBackground += new PrintBackgroundEventHandler(MyCell_PrintBackground);

            p1 = new PointF();
            p2 = new PointF();
            p3 = new PointF();
        }

        void MyCell_PrintBackground(object sender, PrintBackgroundEventArgs e)
        {
            if (Watermark != null)
            {
                FarPoint.Win.Picture pic = new FarPoint.Win.Picture(System.Drawing.Image.FromFile(Watermark.FilePath),
                    FarPoint.Win.RenderStyle.Normal);
                pic.AlignHorz = FarPoint.Win.HorizontalAlignment.Left;
                pic.AlignVert = FarPoint.Win.VerticalAlignment.Top;

                pic.Paint(e.Graphics, new Rectangle(e.SheetRectangle.X + Watermark.Left, e.SheetRectangle.Y + Watermark.Top, 
                    Watermark.Width, Watermark.Height));
            }
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // MyCell
            // 
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }

        /// <summary>
        /// 除去页边距后的页面宽度
        /// </summary>
        public float PaperWidth
        {
            get
            {
                if (ActiveSheet.PrintInfo.PaperSize == null)
                    ActiveSheet.PrintInfo.PaperSize = new System.Drawing.Printing.PaperSize("A4", 827, 1169);

                if (ActiveSheet.PrintInfo.Orientation == PrintOrientation.Portrait)
                {
                    int InnerWidth = ActiveSheet.PrintInfo.PaperSize.Width - ActiveSheet.PrintInfo.Margin.Left - ActiveSheet.PrintInfo.Margin.Right;
                    return ScaleX * InnerWidth / 100;
                }
                else
                {
                    int InnerWidth = ActiveSheet.PrintInfo.PaperSize.Height - ActiveSheet.PrintInfo.Margin.Left - ActiveSheet.PrintInfo.Margin.Right - ActiveSheet.PrintInfo.HeaderHeight - ActiveSheet.PrintInfo.FooterHeight - 2;
                    return ScaleY * InnerWidth / 100;
                }
            }
        }

        /// <summary>
        /// 除去页边距后的页面高度
        /// </summary>
        public float PaperHeight
        {
            get
            {
                if (ActiveSheet.PrintInfo.PaperSize == null)
                    ActiveSheet.PrintInfo.PaperSize = new System.Drawing.Printing.PaperSize("A4", 827, 1169);

                if (ActiveSheet.PrintInfo.Orientation == PrintOrientation.Portrait)
                {
                    int InnerHeight = ActiveSheet.PrintInfo.PaperSize.Height - ActiveSheet.PrintInfo.Margin.Top - ActiveSheet.PrintInfo.Margin.Bottom - ActiveSheet.PrintInfo.HeaderHeight - ActiveSheet.PrintInfo.FooterHeight;
                    return ScaleY * InnerHeight / 100;
                }
                else
                {
                    int InnerHeight = ActiveSheet.PrintInfo.PaperSize.Width - ActiveSheet.PrintInfo.Margin.Top - ActiveSheet.PrintInfo.Margin.Bottom;
                    return ScaleX * InnerHeight / 100;
                }
            }
        }

        /// <summary>
        /// 更新所有列宽
        /// </summary>
        /// <returns></returns>
        private void UpdateColumnsWidth()
        {
            ColumnWidths.Clear();
            TotalWidth = 0f;

            if (ActiveSheet != null)
            {
                for (int i = 0; i < ActiveSheet.ColumnHeader.Columns.Count; i++)
                {
                    TotalWidth = TotalWidth + ActiveSheet.ColumnHeader.Columns[i].Width;
                    ColumnWidths.Add(TotalWidth);
                }
            }
        }

        /// <summary>
        /// 更新所有行高
        /// </summary>
        /// <returns></returns>
        private void UpdateRowsHeight()
        {
            RowHeights.Clear();
            TotalHeight = 0f;

            if (ActiveSheet != null)
            {
                for (int i = 0; i < ActiveSheet.RowHeader.Rows.Count; i++)
                {
                    TotalHeight = TotalHeight + ActiveSheet.RowHeader.Rows[i].Height;
                    RowHeights.Add(TotalHeight);
                }
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (ActiveSheet != null && ActiveSheet.ContainingViews.Length > 0)
            {
                SpreadView View = ActiveSheet.ContainingViews[0];
                TopRow = View.GetViewportTopRow(ActiveSheetIndex, 0);
                BottomRow = View.GetViewportBottomRow(ActiveSheetIndex, 0);

                Invalidate();
            }
        }

        void VerticalScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (ActiveSheet != null && ActiveSheet.ContainingViews.Length > 0)
            {
                SpreadView View = ActiveSheet.ContainingViews[0];
                TopRow = View.GetViewportTopRow(ActiveSheetIndex, 0);
                BottomRow = View.GetViewportBottomRow(ActiveSheetIndex, 0);

                Invalidate();
            }
        }

        void HorizontalScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (ActiveSheet != null && ActiveSheet.ContainingViews.Length > 0)
            {
                SpreadView View = ActiveSheet.ContainingViews[0];
                LeftColumn = View.GetViewportLeftColumn(ActiveSheetIndex, 0);
                RightColumn = View.GetViewportRightColumn(ActiveSheetIndex, 0);

                Invalidate();
            }
        }

        void Sheets_Changing(object sender, CollectionChangeEventArgs e)
        {
            if (ActiveSheet != null)
            {
                ActiveSheet.PropertyChanged -= new SheetViewPropertyChangeEventHandler(Sheet_PropertyChanged);
            }
        }

        void Sheets_Changed(object sender, CollectionChangeEventArgs e)
        {
            if (ActiveSheet != null)
            {
                ActiveSheet.PropertyChanged += new SheetViewPropertyChangeEventHandler(Sheet_PropertyChanged);

                UpdateColumnsWidth();
                UpdateRowsHeight();
            }
        }

        void Sheet_PropertyChanged(object sender, SheetViewPropertyChangeEventArgs e)
        {
            if (e.PropertyName == "PrintInfo")
            {
                Invalidate();
            }
            else if (e.PropertyName == "RowCount" || e.PropertyName == "ColumnCount")
            {
                if (ActiveSheet.ContainingViews.Length > 0)
                {
                    SpreadView View = ActiveSheet.ContainingViews[0];
                    TopRow = View.GetViewportTopRow(ActiveSheetIndex, 0);
                    BottomRow = View.GetViewportBottomRow(ActiveSheetIndex, 0);
                    LeftColumn = View.GetViewportLeftColumn(ActiveSheetIndex, 0);
                    RightColumn = View.GetViewportRightColumn(ActiveSheetIndex, 0);
                }

                UpdateColumnsWidth();
                UpdateRowsHeight();

                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            DrawPageLine(e.Graphics);
            DrawPagePoint(e.Graphics);
        }

        private void DrawPagePoint(Graphics g)
        {
            try
            {
                if (ActiveSheet != null && ActiveSheet.ContainingViews.Length > 0)
                {
                    SpreadView View = ActiveSheet.ContainingViews[0];
                    Rectangle Rect = View.GetViewportRectangle(0, 0);

                    float LeftWidth = 0f;
                    for (float i = PaperWidth; i < TotalWidth; i += PaperWidth)
                    {
                        if (LeftColumn != 0)
                            LeftWidth = ColumnWidths[LeftColumn - 1];

                        float tmp = i - LeftWidth + Rect.Left;
                        if (tmp >= Rect.Left && tmp <= Rect.Right)
                        {
                            p1.X = tmp - 4f;
                            p1.Y = 0f;
                            p2.X = tmp + 4f;
                            p2.Y = 0f;
                            p3.X = tmp;
                            p3.Y = 9f;

                            Brush brush = new SolidBrush(Color.DarkGray);
                            g.FillPolygon(brush, new PointF[] { p1, p2, p3 }, System.Drawing.Drawing2D.FillMode.Alternate);
                            brush.Dispose();
                        }
                    }

                    float TopHeight = 0f;
                    for (float i = PaperHeight; i < TotalHeight; i += PaperHeight)
                    {
                        if (TopRow != 0)
                            TopHeight = RowHeights[TopRow - 1];

                        float tmp = i - TopHeight + Rect.Top;
                        if (tmp >= Rect.Top && tmp <= Rect.Bottom)
                        {
                            p1.X = 0f;
                            p1.Y = tmp - 4f;
                            p2.X = 0f;
                            p2.Y = tmp + 4f;
                            p3.X = 9f;
                            p3.Y = tmp;

                            Brush brush = new SolidBrush(Color.DarkGray);
                            g.FillPolygon(brush, new PointF[] { p1, p2, p3 }, System.Drawing.Drawing2D.FillMode.Alternate);
                            brush.Dispose();
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void DrawPageLine(Graphics g)
        {
            try
            {
                if (ActiveSheet != null && ActiveSheet.ContainingViews.Length > 0)
                {
                    Pen pen = new Pen(SystemColors.ControlDarkDark, 2f);
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                    SpreadView View = ActiveSheet.ContainingViews[0];
                    Rectangle Rect = View.GetViewportRectangle(0, 0);

                    float temp = 0f;
                    int Count = 0;
                    float LeftWidth = 0f;
                    for (int i = 0; i < ActiveSheet.ColumnHeader.Columns.Count; i++)
                    {
                        temp = temp + ActiveSheet.ColumnHeader.Columns[i].Width;
                        Count++;

                        if (temp - ActiveSheet.ColumnHeader.Columns[i].Width == PaperWidth)
                        {
                            float x = 0f;
                            if (Count > 1)
                            {
                                x = ColumnWidths[i] + Rect.Left - ActiveSheet.ColumnHeader.Columns[i].Width - 1;
                            }
                            else
                            {
                                x = ColumnWidths[i] + Rect.Left;
                            }

                            if (LeftColumn != 0)
                                LeftWidth = ColumnWidths[LeftColumn - 1];

                            float tmp = x - LeftWidth;
                            if (tmp >= Rect.Left && tmp <= Rect.Right)
                            {
                                g.DrawLine(pen, tmp, Rect.Top, tmp, this.Height - Rect.Top);
                            }

                            temp = temp - PaperWidth;
                            Count = 0;
                        }
                    }

                    Count = 0;
                    temp = 0f;
                    float TopHeight = 0f;
                    for (int i = 0; i < ActiveSheet.RowHeader.Rows.Count; i++)
                    {
                        temp = temp + ActiveSheet.RowHeader.Rows[i].Height;
                        Count++;

                        if (temp - ActiveSheet.RowHeader.Rows[i].Height == PaperHeight)
                        {
                            float y = 0f;
                            if (Count > 1)
                            {
                                y = RowHeights[i] + Rect.Top - ActiveSheet.RowHeader.Rows[i].Height - 1;
                            }
                            else
                            {
                                y = RowHeights[i] + Rect.Top;
                            }

                            if (TopRow != 0)
                                TopHeight = RowHeights[TopRow - 1];

                            float tmp = y - TopHeight;
                            if (tmp >= Rect.Top && tmp <= Rect.Bottom)
                            {
                                g.DrawLine(pen, Rect.Left, tmp, this.Width - Rect.Left, tmp);
                            }

                            temp = temp - PaperHeight;
                            Count = 0;
                        }
                    }

                    pen.Dispose();
                }
            }
            catch
            {
            }
        }

        protected override void OnColumnWidthChanged(FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            base.OnColumnWidthChanged(e);

            if (ActiveSheet != null)
            {
                foreach (ColumnWidthChangeExtents Extents in e.ColumnList)
                {
                    for (int i = Extents.FirstColumn; i < ActiveSheet.ColumnHeader.Columns.Count; i++)
                    {
                        int previ = i - 1;
                        float prevColumnsWidth = 0;
                        if (previ >= 0)
                            prevColumnsWidth = ColumnWidths[previ];

                        ColumnWidths[i] = prevColumnsWidth + e.View.Owner.ActiveSheet.Columns[i].Width;
                    }
                }

                TotalWidth = 0f;
                if (ActiveSheet.ColumnHeader.Columns.Count > 0)
                {
                    TotalWidth = ColumnWidths[ActiveSheet.ColumnHeader.Columns.Count - 1];
                }

                Invalidate();
            }
        }

        protected override void OnRowHeightChanged(RowHeightChangedEventArgs e)
        {
            base.OnRowHeightChanged(e);

            if (ActiveSheet != null)
            {
                foreach (RowHeightChangeExtents Extents in e.RowList)
                {
                    for (int i = Extents.FirstRow; i < ActiveSheet.RowHeader.Rows.Count; i++)
                    {
                        int previ = i - 1;
                        float prevRowsHeight = 0;
                        if (previ >= 0)
                            prevRowsHeight = RowHeights[previ];

                        RowHeights[i] = prevRowsHeight + e.View.Owner.ActiveSheet.Rows[i].Height;
                    }
                }

                TotalHeight = 0f;
                if (ActiveSheet.RowHeader.Rows.Count > 0)
                {
                    TotalHeight = RowHeights[ActiveSheet.RowHeader.Rows.Count - 1];
                }

                Invalidate();
            }
        }

        protected override void OnActiveSheetChanged(EventArgs e)
        {
            base.OnActiveSheetChanged(e);

            if (ActiveSheet != null && ActiveSheet.ContainingViews.Length > 0)
            {
                SpreadView View = ActiveSheet.ContainingViews[0];
                TopRow = View.GetViewportTopRow(ActiveSheetIndex, 0);
                BottomRow = View.GetViewportBottomRow(ActiveSheetIndex, 0);
                LeftColumn = View.GetViewportLeftColumn(ActiveSheetIndex, 0);
                RightColumn = View.GetViewportRightColumn(ActiveSheetIndex, 0);
            }

            UpdateColumnsWidth();
            UpdateRowsHeight();

            Invalidate();
        }

        [Browsable(false)]
        public List<CellRange> Selections
        {
            get
            {
                List<CellRange> Result = new List<CellRange>();
                if (Ranges.ContainsKey(ActiveSheet))
                {
                    Result = Ranges[ActiveSheet];
                }

                return Result;
            }
        }

        private Dictionary<SheetView, List<CellRange>> Ranges = new Dictionary<SheetView, List<CellRange>>();
        public virtual List<CellRange> GetSelections(SheetView SheetView)
        {
            return Ranges[SheetView];
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    CellRange cells = this.ActiveSheet.GetSelection(0);
                    for (int x = 0; x < cells.RowCount; x++)
                    {
                        for (int y = 0; y < cells.ColumnCount; y++)
                        {
                            int row = cells.Row + x;
                            int col = cells.Column + y;

                            this.ActiveSheet.Cells[row, col].Value = DBNull.Value;
                            this.ActiveSheet.Cells[row, col].Text = "";
                        }
                    }
                }
                catch { }
            }

            base.OnKeyDown(e);
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            //获得用户选择的区域
            SheetView sheetview = Sheets[e.View.ActiveSheetIndex];
            List<CellRange> CellRanges = null;
            if (Ranges.ContainsKey(sheetview))
            {
                CellRanges = Ranges[sheetview];
            }
            else
            {
                CellRanges = new List<CellRange>();
            }

            CellRange temp = null;
            switch (sheetview.SelectionPolicy)
            {
                case SelectionPolicy.MultiRange:
                    temp = PreprocessCellRange(sheetview, e.Range);
                    if (GetSelectionCount(sheetview) == 0)
                    {
                        ClearSelection(sheetview);
                    }

                    if (GetSelectionCount(sheetview) != 0)
                    {
                        if (PressCtrl)
                        {
                            AddSelection(sheetview, temp, true);
                        }
                    }
                    else
                    {
                        AddSelection(sheetview, temp, false);
                    }
                    break;
                case SelectionPolicy.Range:
                case SelectionPolicy.Single:
                    temp = PreprocessCellRange(sheetview, e.Range);
                    AddSelection(sheetview, temp, false);
                    break;
            }

            base.OnSelectionChanged(e);
        }

        private bool PressCtrl = false;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            try
            {
                if (FpSpread.ModifierKeys != Keys.Control)
                {
                    ClearSelection(ActiveSheet);
                    PressCtrl = false;
                }
                else
                {
                    PressCtrl = true;
                }

                base.OnMouseDown(e);
            }
            catch { }
        }

        private void AddSelection(SheetView SheetView, CellRange CellRange, bool IsNew)
        {
            if (!IsNew && Ranges.ContainsKey(SheetView))
            {
                Ranges[SheetView].Clear();
            }

            List<CellRange> temp = new List<CellRange>();
            if (Ranges.ContainsKey(SheetView))
            {
                temp = Ranges[SheetView];
            }
            else
            {
                Ranges.Add(SheetView, temp);
            }

            temp.Add(CellRange);
        }

        private void ClearSelection(SheetView SheetView)
        {
            if (Ranges.ContainsKey(SheetView))
            {
                Ranges[SheetView].Clear();
            }
        }

        private CellRange PreprocessCellRange(SheetView SheetView, CellRange Range)
        {
            CellRange ResultRange = null;
            if (Range.Column != -1 && Range.Row != -1)
            {
                ResultRange = Range;
            }
            else
            {
                int Column = 0, ColumnCount = 0;
                if (Range.Column == -1)
                {
                    Column = 0;
                    ColumnCount = SheetView.Columns.Count;
                }
                else
                {
                    Column = Range.Column;
                    ColumnCount = Range.ColumnCount;
                }

                int Row = 0, RowCount = 0;
                if (Range.Row == -1)
                {
                    Row = 0;
                    RowCount = SheetView.Rows.Count;
                }
                else
                {
                    Row = Range.Row;
                    RowCount = Range.RowCount;
                }

                ResultRange = new CellRange(Row, Column, RowCount, ColumnCount);
            }

            return ResultRange;
        }



        private int GetSelectionCount(SheetView SheetView)
        {
            if (Ranges.ContainsKey(SheetView))
            {
                return Ranges[SheetView].Count;
            }

            return 0;
        }

        #region 打印

        public new void PrintSheet(int SheetIndex)
        {
            if (SheetIndex >= 0 && SheetIndex < Sheets.Count)
            {
                SheetView sheetView = Sheets[SheetIndex];

                MyCell Spread = new MyCell();
                if (sheetView != null && sheetView.Tag != null)
                {
                    if (Watermark != null)
                    {
                        if (this.Watermark.SheetID == new Guid(sheetView.Tag.ToString()))
                        {
                            Spread.Watermark = this.Watermark;

                            sheetView.PrintInfo.Opacity = Watermark.Opacity;
                        }
                    }
                }
                Spread.Sheets.Add(sheetView);
                OwnerPrintDocument Document = new OwnerPrintDocument(Spread);
               
                Document.PrintInfo = sheetView.PrintInfo;
                
                if (Sheets[SheetIndex].PrintInfo.Preview)
                {
                    PrintPreviewDialog Dialog = new PrintPreviewDialog();
                    Dialog.WindowState = FormWindowState.Maximized;
                    Dialog.PrintPreviewControl.Zoom = 1.0;
                    Dialog.Document = Document;
                    Dialog.UseAntiAlias = true;
                    Dialog.ShowDialog();
                }
                else
                {
                    Document.Print();
                }
            }
            else if (SheetIndex == -1)
            {
                PrintDialog Dialog = new PrintDialog();
                Dialog.AllowSomePages = true;
                Dialog.PrinterSettings.PrintRange = System.Drawing.Printing.PrintRange.SomePages;
                Dialog.PrinterSettings.FromPage = 1;
                Dialog.PrinterSettings.ToPage = 1;
                Dialog.UseEXDialog = true;

                if (DialogResult.OK == Dialog.ShowDialog())
                {
                    foreach (SheetView Sheet in Sheets)
                    {
                        FpSpread Spread = new MyCell();
                        Spread.Sheets.Add(Sheet);
                        OwnerPrintDocument Document = new OwnerPrintDocument(Spread);
                        Document.ShowPrinterDialog = false;
                        Document.PrinterSettings = Dialog.PrinterSettings;
                        Document.PrintInfo = Sheet.PrintInfo;
                        Document.Print();
                    }
                }
            }
        }

        #endregion 打印

        const int WM_KEYDOWN = 0x100;
        const int WM_SYSKEYDOWN = 0x104;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.Msg == WM_KEYDOWN || msg.Msg == WM_SYSKEYDOWN)
            {
                //switch (keyData)
                //{
                //    case Keys.Left:
                //        return true;
                //    case Keys.Right:
                //        return true;
                //    case Keys.Up:
                //        return true;
                //    case Keys.Down:
                //        return true;
                //}
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
