using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.Win.Spread.DrawingSpace;
using System.Drawing;
using Yqun.Client.BizUI;
using FarPoint.Win.Spread;
using BizComponents.Properties;
using System.Windows.Forms;
using System.Globalization;
using System.Xml;
using FarPoint.Win;
using System.Drawing.Drawing2D;
using BizCommon;

namespace BizComponents
{
    /// <summary>
    /// 图表Shape
    /// </summary>
    [Serializable]
    public class ChartShape : RectangleShape
    {
        [NonSerialized]
        ChartControl2 _ChartControl;
        [NonSerialized]
        SheetView _SheetView;

        public ChartShape()
        {
            _ChartControl = new ChartControl2(ActiveSheet);
            Tag = _ChartControl.GetSerializeXML();
            _ChartControl.CellSize = new Size(Width, Height);

            CanPrint = true;
            CanRotate = false;
            ShadowColor = Color.Transparent;
            ShapeOutlineColor = Color.Transparent;
            Border = new EmptyBorder();
        }

        public override bool Locked
        {
            get
            {
                return base.Locked;
            }
            set
            {
                base.Locked = value;

                if (value)
                {
                    CanMove = Moving.None;
                    CanSize = Sizing.None;
                }
                else
                {
                    CanMove = Moving.HorizontalAndVertical;
                    CanSize = Sizing.HeightAndWidth;
                }

                CanRotate = value;
                
            }
        }

        public SheetView ActiveSheet
        {
            get
            {
                return _SheetView;
            }
            set
            {
                _SheetView = value;
            }
        }

        public ChartControl2 ChartControl
        {
            get
            {
                return _ChartControl;
            }
        }

        public void UpdateChart()
        {
            ChartControl.RefreshFpSpread(ActiveSheet);
            ChartControl.Deserialize(Tag.ToString());
            ChartControl.UpdateChartImage();
        }

        protected override void PaintSpecialForeground(Graphics g, Rectangle r)
        {
            g.DrawImage(ChartControl.ChartImage, r);
            Picture = ChartControl.ChartImage;
        }

        protected override void PaintBorder(Graphics g, Rectangle r)
        {
            g.DrawRectangle(Pens.Transparent, r);
        }

        protected override void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Width" || e.PropertyName == "Height")
            {
                if (ChartControl != null)
                {
                    ChartControl.CellSize = new Size(Width, Height);
                    ChartControl.UpdateChartImage();
                }
            }
        }

        public override void DoMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && 
                !Locked &&
                Rectangle.Contains(e.X,e.Y))
            {
                ShowChartEditor();
            }
        }

        protected void ShowChartEditor()
        {
            ChartControl.Deserialize(Tag.ToString());

            ChartEditor2 ChartEditor = new ChartEditor2(ChartControl);
            if (DialogResult.OK == ChartEditor.ShowDialog())
            {
                Tag = ChartControl.GetSerializeXML();
                ChartControl.UpdateChartImage();
            }
        }
    }

    /// <summary>
    /// 公式Shape
    /// </summary>
    [Serializable]
    public class EquationShape : RectangleShape
    {
        [NonSerialized]
        EquationDialog EquationDialog;
        [NonSerialized]
        YqunRichTextBox DrawingOP;

        public EquationShape()
        {
            DrawingOP = new YqunRichTextBox();
            DrawingOP.SetBounds(Left, Top, Width, Height);

            if (this.Parent != null)
            {
                (this.Parent as FpSpread).Controls.Add(DrawingOP);
            }

            EquationDialog = new EquationDialog();
            Tag = EquationDialog.GetSerializeRtf();

            CanPrint = true;
            CanRotate = false;
            ShadowColor = Color.Transparent;
            ShapeOutlineColor = Color.Transparent;
        }

        public override bool Locked
        {
            get
            {
                return base.Locked;
            }
            set
            {
                base.Locked = value;

                if (value)
                {
                    CanMove = Moving.None;
                    CanSize = Sizing.None;
                }
                else
                {
                    CanMove = Moving.HorizontalAndVertical;
                    CanSize = Sizing.HeightAndWidth;
                }

                CanRotate = value;
            }
        }

        protected override void PaintSpecialForeground(Graphics g, Rectangle r)
        {
            MyCell fpSpread = this.Parent as MyCell;
            if (fpSpread != null)
            {
                SheetView Sheet = fpSpread.ActiveSheet;

                Boolean IsPreview = true;
                try
                {
                    if (Sheet.SheetCorner.Cells[0, 0].Tag != null)
                        Boolean.TryParse(Sheet.SheetCorner.Cells[0, 0].Tag.ToString(), out IsPreview);
                }
                catch
                { }

                int activeRowViewportIndex = fpSpread.GetActiveRowViewportIndex();
                int activeColumnViewportIndex = fpSpread.GetActiveColumnViewportIndex();

                Rectangle rowHeaderRect = fpSpread.GetRowHeaderRectangle(activeRowViewportIndex);
                Rectangle columnHeaderRect = fpSpread.GetColumnHeaderRectangle(activeColumnViewportIndex);

                int paddingLeft, paddingTop;
                if (DrawingOP.IsPrinter(g))
                {
                    paddingLeft = Sheet.PrintInfo.Margin.Left;
                    paddingTop = Sheet.PrintInfo.Margin.Top;

                    float X = r.X;
                    while (X >= fpSpread.PaperWidth)
                        X = X - fpSpread.PaperWidth;
                    r.X = Convert.ToInt32(X);

                    float Y = r.Y;
                    while (Y >= fpSpread.PaperHeight) 
                        Y = Y - fpSpread.PaperHeight;
                    r.Y = Convert.ToInt32(Y);
                }
                else
                {
                    paddingLeft = rowHeaderRect.Width;
                    paddingTop = columnHeaderRect.Height;
                }

                int topRowIndex = fpSpread.GetViewportTopRow(activeRowViewportIndex);
                int leftColumnIndex = fpSpread.GetViewportLeftColumn(activeColumnViewportIndex);

                Rectangle cellRect = new Rectangle(0, 0, (int)Sheet.Columns[leftColumnIndex].Width, (int)Sheet.Rows[topRowIndex].Height);

                int dX = Convert.ToInt32(fpSpread.ColumnWidths[leftColumnIndex] - cellRect.Width - paddingLeft);
                int dY = Convert.ToInt32(fpSpread.RowHeights[topRowIndex] - cellRect.Height - paddingTop);

                Rectangle localRect = new Rectangle(r.X - dX, r.Y - dY, r.Width, r.Height);
                DrawingOP.Draw(g, localRect, Tag.ToString(), IsPreview);
            }
        }

        protected override void PaintBorder(Graphics g, Rectangle r)
        {
            g.DrawRectangle(Pens.Transparent, r);
        }

        protected override void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Width" || e.PropertyName == "Height")
            {
                 MyCell fpSpread = this.Parent as MyCell;
                 if (fpSpread != null)
                 {
                     Graphics g = fpSpread.CreateGraphics();
                     PaintSpecialForeground(g, this.Rectangle);
                 }
            }
        }

        public override void DoMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && !Locked)
            {
                ShowEquationEditor();
            }

            base.DoMouseUp(sender, e);
        }

        protected void ShowEquationEditor()
        {
            EquationDialog.DeserializeRtf(Tag.ToString());

            if (DialogResult.OK == EquationDialog.ShowDialog())
            {
                Tag = EquationDialog.GetSerializeRtf();
                Size = DrawingOP.GetObjectSize(new Size(100, 100));
            }
        }

        public override void Dispose(bool disposing)
        {
            if ((this.Parent as FpSpread).Controls.Contains(DrawingOP))
            {
                (this.Parent as FpSpread).Controls.Remove(DrawingOP);
            }
            base.Dispose(disposing);
        }
    }

    /// <summary>
    /// 公式Shape
    /// </summary>
    [Serializable]
    public class RectShape : RectangleShape
    {
        public RectShape()
        {
            CanPrint = true;
            CanRotate = true;
            ShadowColor = Color.Transparent;
            ShapeOutlineColor = Color.Black;
            BackColor = Color.Black;
        }

        public override bool Locked
        {
            get
            {
                return base.Locked;
            }
            set
            {
                base.Locked = value;

                if (value)
                {
                    CanMove = Moving.None;
                    CanSize = Sizing.None;
                }
                else
                {
                    CanMove = Moving.HorizontalAndVertical;
                    CanSize = Sizing.HeightAndWidth;
                }

                CanRotate = value;
            }
        }


        protected override void PaintBorder(Graphics g, Rectangle r)
        {
            g.DrawRectangle(Pens.Black, r);
        }

    }
}
