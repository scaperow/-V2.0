using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Xml;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ReportCommon;
using ReportCommon.Chart;

namespace ReportCommon
{
    //
    //报表中的单元格类型中包括常规类型，斜线单元格类型
    //不同于主子模型中的单元格类型
    //

    /// <summary>
    /// 常规类型
    /// </summary>
    [Serializable]
    public class GeneralCellType : FarPoint.Win.Spread.CellType.GeneralCellType
    {
        public override void PaintCell(System.Drawing.Graphics g, System.Drawing.Rectangle r, FarPoint.Win.Spread.Appearance appearance, object value, bool isSelected, bool isLocked, float zoomFactor)
        {
            if (value is GridElement)
            {
                GridElement Element = value as GridElement;
                if (Element.Value is Slash) //绘出一个斜线单元格
                {
                    ReportElementDrawing.PaintSlash(g, r, appearance, Element);
                }
                else if (Element.Value is DataColumn) //绘出一个数据列
                {
                    ReportElementDrawing.PaintDataColumn(g, r, appearance, Element);
                }
                else if (Element.Value is Formula) //绘出一个公式
                {
                    ReportElementDrawing.PaintFormula(g, r, appearance, Element);
                }
                else if (Element.Value is Picture) //绘出一个图片
                {
                    ReportElementDrawing.PaintImage(g, r, appearance, Element);
                }
                else if (Element.Value is LiteralText)//绘出文本
                {
                    LiteralText literalText = Element.Value as LiteralText;
                    base.PaintCell(g, r, appearance, literalText.Text, isSelected, isLocked, zoomFactor);
                }
                else if (Element.Value is Variable)//绘出一个变量
                {
                    ReportElementDrawing.PaintVariable(g, r, appearance, Element);
                }
                else if (Element.Value is ChartPainter)//绘出一个图表
                {
                    ReportElementDrawing.PaintChart(g, r, appearance, Element);
                }
                else
                {
                    base.PaintCell(g, r, appearance, "", isSelected, isLocked, zoomFactor);
                }

                ReportElementDrawing.PaintPageBreak(g, r, appearance, Element);
            }
            else
            {
                base.PaintCell(g, r, appearance, value, isSelected, isLocked, zoomFactor);
            }
        }
    }

    [Serializable]
    public class TextCellType : FarPoint.Win.Spread.CellType.TextCellType
    {
        GridElement Element = null;
        TextBox textbox = new TextBox();

        public TextCellType()
        {
            textbox.Multiline = true;
            textbox.WordWrap = true;

            this.Multiline = true;
            this.WordWrap = true;
        }

        public override void SetEditorValue(object value)
        {
            Element = value as GridElement;
        }

        public override object GetEditorValue()
        {
            return Element;
        }

        public override Control GetEditorControl(FarPoint.Win.Spread.Appearance appearance, float zoomFactor)
        {
            return textbox;
        }

        public override void StartEditing(EventArgs e, bool selectAll, bool autoClipboard)
        {
            selectAll = true;

            if (Element != null && Element.Value is LiteralText)
            {
                LiteralText literalText = Element.Value as LiteralText;
                textbox.Text = literalText.Text;
            }

            base.StartEditing(e, selectAll, autoClipboard);
        }

        public override bool StopEditing()
        {
            if (Element != null && Element.Value is LiteralText)
            {
                LiteralText literalText = Element.Value as LiteralText;
                textbox.Text = literalText.Text;
            }

            base.FireEditingStopped();

            return true;
        }

        public override void PaintCell(Graphics g, Rectangle r, FarPoint.Win.Spread.Appearance appearance, object value, bool isSelected, bool isLocked, float zoomFactor)
        {
            if (value != null && value is GridElement)
            {
                GridElement Element = value as GridElement;
                if (Element.Value is LiteralText)
                {
                    LiteralText literalText = Element.Value as LiteralText;
                    base.PaintCell(g, r, appearance, literalText.Text, isSelected, isLocked, zoomFactor);
                }
            }
            else
            {
                base.PaintCell(g, r, appearance, value, isSelected, isLocked, zoomFactor);
            }
        }
    }

    /// <summary>
    /// 报表元素绘画引擎
    /// </summary>
    public class ReportElementDrawing
    {
        static StringFormat Format;
        static ReportElementDrawing()
        {
            Format = new StringFormat();
            Format.Alignment = StringAlignment.Center;
            Format.LineAlignment = StringAlignment.Center;
        }

        /// <summary>
        /// 画斜线
        /// </summary>
        public static void PaintSlash(System.Drawing.Graphics g, System.Drawing.Rectangle r, FarPoint.Win.Spread.Appearance appearance, GridElement Element)
        {
            using (Brush brush = new SolidBrush(appearance.ForeColor))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

                Brush BackColorBrush = new SolidBrush(appearance.BackColor);
                g.FillRectangle(BackColorBrush, r);
                BackColorBrush.Dispose();

                Pen myPen = new System.Drawing.Pen(Color.Black);

                int count = 0;
                float Angle, radians;
                int rightX, rightY;

                Slash slash = Element.Value as Slash;
                string[] sArray = slash.Text.Split('|');
                if (sArray.Length != 0)
                {
                    count = sArray.Length - 1;
                }
                int sum = count + 1;

                if (slash.RotationStyle == RotationStyle.Counterclockwise)
                {
                    if (count == 1)
                    {
                        g.DrawLine(myPen, r.X, r.Y + r.Height, r.X + r.Width, r.Y);
                    }
                    else if (count == 2)
                    {
                        g.DrawLine(myPen, r.X, r.Y + r.Height, r.X + (2.0f / 3) * r.Width, r.Y);
                        g.DrawLine(myPen, r.X, r.Y + r.Height, r.X + r.Width, r.Y + (1.0f / 3) * r.Height);
                    }
                    else if (count > 2)
                    {
                        float height;
                        for (int j = 1; j <= count; j++)
                        {
                            Angle = (90 / sum) * j;
                            height = r.Width / (float)(Math.Tan(Angle * Math.PI / 180.0f));
                            rightX = r.X + r.Width;
                            rightY = r.Y + (int)height;

                            g.DrawLine(myPen, r.X, r.Y, rightX, rightY);
                        }
                    }

                    for (int i = 0; i < sArray.Length; i++)
                    {
                        if (i == 0)
                        {
                            Rectangle _rect = new Rectangle(r.X, r.Y, Convert.ToInt32(((sum - count + 1.0f) / sum) * (r.Width / sum)), Convert.ToInt32(count * 1.0 / sum * r.Height));
                            g.DrawString(sArray[i], appearance.Font, brush, _rect, Format);
                        }
                        else if (i == sArray.Length - 1)
                        {
                            Rectangle _rect = new Rectangle(Convert.ToInt32(r.X + (1.0f / sum) * r.Width), Convert.ToInt32(r.Y + r.Height - 1.0f / sum * r.Height), Convert.ToInt32((sum - count * 1.0f) * r.Width / sum), Convert.ToInt32((sum - count * 1.0f) / sum * r.Height));
                            g.DrawString(sArray[i], appearance.Font, brush, _rect, Format);
                        }
                        else
                        {
                            if (count == 2)
                            {
                                radians = Convert.ToSingle(Math.Atan(r.Height * 1.0 / r.Width));
                                Angle = Convert.ToSingle(radians * 180 / Math.PI);
                            }
                            else
                            {
                                Angle = (1 + 2 * i) * (45.0f / (count + 1));
                            }

                            g.TranslateTransform(r.X + 3, r.Y + r.Height - 7);
                            g.RotateTransform(-(float)Angle);
                            g.DrawString(sArray[i], appearance.Font, brush, r.Width / 3f, 0);
                            g.ResetTransform();
                        }
                    }
                }
                else
                {
                    if (count == 1)
                    {
                        g.DrawLine(myPen, r.X, r.Y, r.X + r.Width, r.Y + r.Height);
                    }
                    else if (count == 2)
                    {
                        g.DrawLine(myPen, r.X, r.Y, r.X + (2.0f / 3) * r.Width, r.Y + r.Height);
                        g.DrawLine(myPen, r.X, r.Y, r.X + r.Width, r.Y + (2.0f / 3) * r.Height);
                    }
                    else if (count > 2)
                    {
                        float height;
                        for (int j = 1; j <= count; j++)
                        {
                            Angle = (90 / sum) * j;
                            height = r.Width / (float)(Math.Tan(Angle * Math.PI / 180.0f));
                            rightX = r.X + r.Width;
                            rightY = r.Y + (int)height;

                            g.DrawLine(myPen, r.X, r.Y, rightX, rightY);
                        }
                    }

                    for (int i = 0; i < sArray.Length; i++)
                    {
                        if (i == 0)
                        {
                            Rectangle _rect = new Rectangle(Convert.ToInt32(r.X + r.Width / sum * 1.0), r.Y, Convert.ToInt32(count * r.Width / sum * 1.0), Convert.ToInt32(((sum - count + 1.0f) / sum) * (r.Height / sum)));
                            g.DrawString(sArray[i], appearance.Font, brush, _rect, Format);
                        }
                        else if (i == sArray.Length - 1)
                        {
                            Rectangle _rect = new Rectangle(r.X, Convert.ToInt32(r.Y + r.Height / sum * 1.0), Convert.ToInt32((sum - count + 1.0f) * r.Width / sum / sum), Convert.ToInt32(count * 1.0 * r.Height / sum));
                            g.DrawString(sArray[i], appearance.Font, brush, _rect, Format);
                        }
                        else
                        {
                            if (count == 2)
                            {
                                radians = Convert.ToSingle(Math.Atan(r.Height * 1.0 / r.Width));
                                Angle = Convert.ToSingle(radians * 180 / Math.PI);
                            }
                            else
                            {
                                Angle = (1 + 2 * i) * (45 / (count + 1));
                            }

                            g.TranslateTransform(r.X + 3, r.Y - 4);
                            g.RotateTransform((float)Angle);
                            g.DrawString(sArray[i], appearance.Font, brush, r.Width / 2.8f, 0);
                            g.ResetTransform();
                        }
                    }
                }

                myPen.Dispose();
            }
        }

        /// <summary>
        /// 画数据列
        /// </summary>
        public static void PaintDataColumn(System.Drawing.Graphics g, System.Drawing.Rectangle r, FarPoint.Win.Spread.Appearance appearance, GridElement Element)
        {
            DataColumn dataColumn = Element.Value as DataColumn;

            using (Brush brush = new SolidBrush(appearance.ForeColor))
            {
                Brush BackColorBrush = new SolidBrush(appearance.BackColor);
                g.FillRectangle(BackColorBrush, r);
                BackColorBrush.Dispose();

                String Prefix = "";
                switch (dataColumn.DataSetting)
                {
                    case DataSetting.Group:
                        Prefix = "Group(";
                        break;
                    case DataSetting.List:
                        Prefix = "List(";
                        break;
                    case DataSetting.Aggregation:
                        Prefix = "Aggregation(" + dataColumn.FunctionInfo.Text + ",";
                        break;
                }

                String Text = Prefix + dataColumn.TableText + "." + dataColumn.FieldText + ")";
                g.DrawString(Text, appearance.Font, brush, r, Format);
            }
        }

        /// <summary>
        /// 画公式
        /// </summary>
        public static void PaintFormula(System.Drawing.Graphics g, System.Drawing.Rectangle r, FarPoint.Win.Spread.Appearance appearance, GridElement Element)
        {
            Formula formula = Element.Value as Formula;

            using (Brush brush = new SolidBrush(appearance.ForeColor))
            {
                Brush BackColorBrush = new SolidBrush(appearance.BackColor);
                g.FillRectangle(BackColorBrush, r);
                BackColorBrush.Dispose();

                g.DrawString("=" + formula.Expression, appearance.Font, brush, r, Format);
            }
        }

        /// <summary>
        /// 画图片
        /// </summary>
        public static void PaintImage(System.Drawing.Graphics g, System.Drawing.Rectangle r, FarPoint.Win.Spread.Appearance appearance, GridElement Element)
        {
            Picture image = Element.Value as Picture;

            Brush BackColorBrush = new SolidBrush(appearance.BackColor);
            g.FillRectangle(BackColorBrush, r);
            BackColorBrush.Dispose();

            g.DrawImage(image.Image, r.X, r.Y, r.Width, r.Height);
        }

        public static void PaintPageBreak(System.Drawing.Graphics g, System.Drawing.Rectangle r, FarPoint.Win.Spread.Appearance appearance, GridElement Element)
        {
            PageBreak pageBreak = Element.PageBreak;
            if (pageBreak.IsAfterRow)
            {
                g.DrawRectangle(Pens.Transparent, r.X, r.Y, 5, 5);
                g.FillRectangle(Brushes.Green, r.X, r.Y, 5, 5);
            }
        }

        public static void PaintVariable(System.Drawing.Graphics g, System.Drawing.Rectangle r, FarPoint.Win.Spread.Appearance appearance, GridElement Element)
        {
            Variable variable = Element.Value as Variable;

            using (Brush brush = new SolidBrush(appearance.ForeColor))
            {
                Brush BackColorBrush = new SolidBrush(appearance.BackColor);
                g.FillRectangle(BackColorBrush, r);
                BackColorBrush.Dispose();

                g.DrawString("=" + variable.Name, appearance.Font, brush, r, Format);
            }
        }

        public static void PaintChart(System.Drawing.Graphics g, System.Drawing.Rectangle r, FarPoint.Win.Spread.Appearance appearance, GridElement Element)
        {
            ChartPainter chartPainter = Element.Value as ChartPainter;
            chartPainter.UpdateDataSource();
            IPainter Painter = chartPainter as IPainter;
            Image image = Painter.Paint(r.Width, r.Height);
            g.DrawImage(image, r.X, r.Y, r.Width, r.Height);
        }
    }
}
