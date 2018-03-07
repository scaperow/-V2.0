using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using ReportCommon;
using ReportCommon.Chart;
using Yqun.BO.ReportE.Properties;
using FarPoint.Win.Spread;

namespace Yqun.BO.ReportE.Core
{
    public class SlashCellType : FarPoint.Win.Spread.CellType.GeneralCellType
    {
        StringFormat strFormat = null;
        public SlashCellType()
        {
            strFormat = new StringFormat();
            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Center;
        }

        public override void PaintCell(System.Drawing.Graphics g, System.Drawing.Rectangle r, FarPoint.Win.Spread.Appearance appearance, object value, bool isSelected, bool isLocked, float zoomFactor)
        {
            if (value is Slash)
            {
                Slash Slash = value as Slash;
                DrawingSlash(g, r, appearance, Slash);
                return;
            }

            base.PaintCell(g, r, appearance, value, isSelected, isLocked, zoomFactor);
        }

        private void DrawingSlash(System.Drawing.Graphics g, System.Drawing.Rectangle r, FarPoint.Win.Spread.Appearance appearance, Slash Slash)
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

                Slash slash = Slash;
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
                            g.DrawString(sArray[i], appearance.Font, brush, _rect, strFormat);
                        }
                        else if (i == sArray.Length - 1)
                        {
                            Rectangle _rect = new Rectangle(Convert.ToInt32(r.X + (1.0f / sum) * r.Width), Convert.ToInt32(r.Y + r.Height - 1.0f / sum * r.Height), Convert.ToInt32((sum - count * 1.0f) * r.Width / sum), Convert.ToInt32((sum - count * 1.0f) / sum * r.Height));
                            g.DrawString(sArray[i], appearance.Font, brush, _rect, strFormat);
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
                            g.DrawString(sArray[i], appearance.Font, brush, _rect, strFormat);
                        }
                        else if (i == sArray.Length - 1)
                        {
                            Rectangle _rect = new Rectangle(r.X, Convert.ToInt32(r.Y + r.Height / sum * 1.0), Convert.ToInt32((sum - count + 1.0f) * r.Width / sum / sum), Convert.ToInt32(count * 1.0 * r.Height / sum));
                            g.DrawString(sArray[i], appearance.Font, brush, _rect, strFormat);
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
    }

    public class ImageCellType : FarPoint.Win.Spread.CellType.ImageCellType
    {
    }

    public class ChartCellType : FarPoint.Win.Spread.CellType.ImageCellType
    {
        public override void PaintCell(Graphics g, Rectangle r, FarPoint.Win.Spread.Appearance appearance, object value, bool isSelected, bool isLocked, float zoomFactor)
        {
            if (value is ChartPainter)
            {
                ChartPainter chartPainter = value as ChartPainter;
                IPainter Painter = chartPainter as IPainter;
                Image image = Painter.Paint(r.Width, r.Height);
                g.DrawImage(image, r.X, r.Y, r.Width, r.Height);
            }
            else
            {
                appearance.HorizontalAlignment = CellHorizontalAlignment.Center;
                appearance.VerticalAlignment = CellVerticalAlignment.Center;
                base.PaintCell(g, r, appearance, Resource.图表类型, isSelected, isLocked, zoomFactor);
            }
        }
    }
}
