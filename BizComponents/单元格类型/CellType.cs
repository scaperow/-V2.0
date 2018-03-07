using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using System.ComponentModel;
using System.Windows.Forms;
using FarPoint.Win.Spread.CellType;
using System.Drawing;
using Yqun.Client.BizUI;
using FarPoint.Win.Spread;
using BizCommon.Properties;
using FarPoint.Win;
using Yqun.Bases.ClassBases;
using System.Data;
using System.Xml;
using System.Globalization;
using Yqun.Common.ContextCache;
using Yqun.Services;
using Yqun.Bases;
using System.Runtime.InteropServices;
using FarPoint.Excel;
using System.Drawing.Drawing2D;
using System.Windows.Forms.VisualStyles;

namespace BizComponents
{
    //
    //主子模型中的单元格类型中包括常规类型、数字类型、文本类型、百分号类型、图片类型、超链接类型、货币类型、日期时间类型、复选框类型、上下标类型、条形码类型、图表单元格类型、标段号单元格类型
    //

    /// <summary>
    /// 数字类型
    /// </summary>
    [Serializable]
    public class NumberCellType : FarPoint.Win.Spread.CellType.NumberCellType, IConvertable, IGetFieldType
    {
        public FieldType FieldType
        {
            get
            {
                return FieldType.Number;
            }
        }

        public override Control GetEditorControl(FarPoint.Win.Spread.Appearance appearance, float zoomFactor)
        {
            Control ctl = base.GetEditorControl(appearance, zoomFactor);
            base.SubEditor = null;
            return ctl;
        }

        public override string ToString()
        {
            return FieldType.Description;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IConvertable 成员

        public bool ConvertTo(FieldType FieldType)
        {
            if (FieldType.Description == "货币" ||
                FieldType.Description == "百分号" ||
                FieldType.Description == "条形码")
                return true;
            else
                return false;
        }

        public bool EqualsBasicDataType(FieldType FieldType)
        {
            if (FieldType.Description == "货币" ||
                FieldType.Description == "百分号" ||
                FieldType.Description == "条形码")
                return true;
            else
                return false;
        }

        #endregion

        public override object Clone()
        {
            NumberCellType cellType = new NumberCellType();
            return cellType;
        }
    }

    /// <summary>
    /// 文本类型
    /// </summary>
    [Serializable]
    public class TextCellType : FarPoint.Win.Spread.CellType.TextCellType, IConvertable, IGetFieldType
    {
        public TextCellType()
        {
            Multiline = false;
            WordWrap = false;
        }

        public virtual FieldType FieldType
        {
            get
            {
                return FieldType.Text;
            }
        }

        public override string ToString()
        {
            return FieldType.Description;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IConvertable 成员

        public bool ConvertTo(FieldType FieldType)
        {
            if (FieldType.Description == "货币" ||
                FieldType.Description == "百分号" ||
                FieldType.Description == "条形码" ||
                FieldType.Description == "数字" ||
                FieldType.Description == "超链接" ||
                FieldType.Description == "日期时间" ||
                FieldType.Description == "复选框")
                return true;
            else
                return false;
        }

        public bool EqualsBasicDataType(FieldType FieldType)
        {
            if (FieldType.Description == "超链接")
                return true;
            else
                return false;
        }

        #endregion

        public override object Clone()
        {
            TextCellType cellType = new TextCellType();
            cellType.Multiline = this.Multiline;
            cellType.WordWrap = this.WordWrap;
            return cellType;
        }

        protected new virtual FarPoint.Win.HorizontalAlignment ToHorizontalAlignment(CellHorizontalAlignment alignment)
        {
            switch (alignment)
            {
                case CellHorizontalAlignment.Left: return FarPoint.Win.HorizontalAlignment.Left;
                case CellHorizontalAlignment.Center: return FarPoint.Win.HorizontalAlignment.Center;
                case CellHorizontalAlignment.Right: return FarPoint.Win.HorizontalAlignment.Right;
                default: return FarPoint.Win.HorizontalAlignment.Left;
            }
        }

        protected new virtual FarPoint.Win.VerticalAlignment ToVerticalAlignment(CellVerticalAlignment alignment)
        {
            switch (alignment)
            {
                case CellVerticalAlignment.Top: return FarPoint.Win.VerticalAlignment.Top;
                case CellVerticalAlignment.Center: return FarPoint.Win.VerticalAlignment.Center;
                case CellVerticalAlignment.Bottom: return FarPoint.Win.VerticalAlignment.Bottom;
                default: return FarPoint.Win.VerticalAlignment.Top;
            }
        }

        protected virtual void GetTextRectangle(Graphics g, Rectangle r, ref Font f, FarPoint.Win.Spread.Appearance appearance, ref RectangleF rText, string paintString)
        {
            do
            {
                String Text = paintString.TrimStart('\r', '\n');
                SizeF sizef = g.MeasureString(Text, f);
                int LineNumber = 0, i = 1, start = 0;
                while (i < Text.Length)
                {
                    SizeF stringsize = g.MeasureString(Text.Substring(start, i - start + 1), f);
                    if (stringsize.Width > r.Width)
                    {
                        start = i;
                        LineNumber++;
                    }

                    i++;
                }

                if (start < Text.Length)
                    LineNumber++;

                SizeF size = new SizeF(r.Width, sizef.Height * LineNumber);
                FarPoint.Win.HorizontalAlignment alignment = this.ToHorizontalAlignment(appearance.HorizontalAlignment);
                FarPoint.Win.VerticalAlignment alignment2 = this.ToVerticalAlignment(appearance.VerticalAlignment);
                rText = new RectangleF(r.X, r.Y, size.Width, size.Height);
                if (alignment == FarPoint.Win.HorizontalAlignment.Center)
                {
                    rText.X += Convert.ToSingle((r.Width / 2.0) - (size.Width / 2.0));
                }
                else if (alignment == FarPoint.Win.HorizontalAlignment.Right)
                {
                    rText.X += r.Width - size.Width;
                }
                if (alignment2 == FarPoint.Win.VerticalAlignment.Center)
                {
                    rText.Y += Convert.ToSingle((r.Height / 2.0) - (size.Height / 2.0));
                }
                else if (alignment2 == FarPoint.Win.VerticalAlignment.Bottom)
                {
                    rText.Y += r.Height - size.Height;
                }
                SizeF SS = g.MeasureString(Text, f, new SizeF(r.Width, r.Height));
                if (SS.Height + 5 >= r.Height)
                {
                    float fs = f.Size - 0.2f;
                    if (fs < 1)
                    {
                        fs = 1;
                        break;
                    }
                    f = new Font(f.FontFamily, fs, f.Style);
                }
                else
                {
                    break;
                }

            } while (true);
        }

        public override void PaintCell(Graphics g, Rectangle r, FarPoint.Win.Spread.Appearance appearance, object value, bool isSelected, bool isLocked, float zoomFactor)
        {
            string s = null;
            StringFormat sf = new StringFormat();
            sf.FormatFlags |= StringFormatFlags.NoClip;

            FarPoint.Win.HorizontalAlignment HAlignment = this.ToHorizontalAlignment(appearance.HorizontalAlignment);
            switch (HAlignment)
            {
                case FarPoint.Win.HorizontalAlignment.Left:
                    sf.Alignment = StringAlignment.Near;
                    break;
                case FarPoint.Win.HorizontalAlignment.Right:
                    sf.Alignment = StringAlignment.Far;
                    break;
                default:
                    sf.Alignment = StringAlignment.Center;
                    break;
            }

            FarPoint.Win.VerticalAlignment VAlignment = this.ToVerticalAlignment(appearance.VerticalAlignment);
            switch (VAlignment)
            {
                case FarPoint.Win.VerticalAlignment.Top:
                    sf.LineAlignment = StringAlignment.Near;
                    break;
                case FarPoint.Win.VerticalAlignment.Bottom:
                    sf.LineAlignment = StringAlignment.Far;
                    break;
                default:
                    sf.LineAlignment = StringAlignment.Center;
                    break;
            }

            RectangleF rText = new RectangleF(r.X, r.Y, r.Width, r.Height);

            Color backColor = appearance.BackColor;
            Color foreColor = appearance.ForeColor;
            if (isSelected)
            {
                if (!appearance.SelectionBackColor.IsEmpty)
                    backColor = appearance.SelectionBackColor;
                if (!appearance.SelectionForeColor.IsEmpty)
                    foreColor = appearance.SelectionForeColor;
            }
            if (isLocked)
            {
                if (!appearance.LockBackColor.IsEmpty)
                    backColor = appearance.LockBackColor;
                if (!appearance.LockForeColor.IsEmpty)
                    foreColor = appearance.LockForeColor;
            }

            SolidBrush backBrush, foreBrush;
            GraphicsState state = g.Save();

            g.IntersectClip(r);

            if (value != null)
                s = this.Format(value);

            sf.Trimming = StringTrim;
            if (appearance.Font == null)
                appearance.Font = new Font("System", 10);

            backBrush = new SolidBrush(backColor);
            g.FillRectangle(backBrush, r);
            backBrush.Dispose();

            if (s != null && s.Length > 0)
            {
                foreBrush = new SolidBrush(foreColor);

                Font f = (Font)appearance.Font;
                if (zoomFactor != 1.0f)
                {
                    float fs = f.Size * zoomFactor;
                    f = new Font(f.FontFamily, fs, f.Style);
                }

                //GetTextRectangle(g, r, ref f, appearance, ref rText, s);
                //zhangdahang 20131203
                rText = r;
                while (g.MeasureString(s.ToString(), f, new SizeF(r.Width, r.Height)).Height + 5 >= r.Height && f.Size >= 5)
                {
                    f = new Font(f.FontFamily, f.Size - 0.2f, f.Style);
                }


                switch (TextOrientation)
                {
                    case TextOrientation.TextHorizontal:
                        //g.DrawRectangle(Pens.Black, rText.X, rText.Y, rText.Width-1, rText.Height);
                        g.DrawString(s, f, foreBrush, rText, sf);
                        break;

                    case TextOrientation.TextHorizontalFlipped:
                        g.TranslateTransform(rText.Width, rText.Height / 2);
                        g.RotateTransform(180);
                        g.DrawString(s, f, foreBrush, new RectangleF(-rText.X, -(rText.Y + 1 + (rText.Height / 2)), rText.Width, rText.Height), sf);
                        break;

                    case TextOrientation.TextVertical:
                        g.TranslateTransform(rText.Width / 2, 0);
                        g.RotateTransform(90);
                        g.DrawString(s, f, foreBrush, new RectangleF(rText.Y, -rText.X - (rText.Width / 2), r.Height, rText.Width), sf);
                        break;

                    case TextOrientation.TextVerticalFlipped:
                        g.TranslateTransform(rText.Width / 2, rText.Height);
                        g.RotateTransform(-90);
                        g.DrawString(s, f, foreBrush, new RectangleF(-rText.Y, rText.X - (r.Width / 2), r.Height, r.Width), sf);
                        break;

                    case TextOrientation.TextTopDown:
                        for (int i = 0; i < s.Length; i++)
                        {
                            SizeF textSize = g.MeasureString(s[i].ToString(CultureInfo.InvariantCulture), f);
                            sf.Alignment = StringAlignment.Center;
                            sf.LineAlignment = StringAlignment.Near;
                            g.DrawString(s[i].ToString(CultureInfo.InvariantCulture), f, foreBrush, rText, sf);
                            rText.Y += (int)Math.Ceiling(textSize.Height);
                        }
                        break;
                }

                g.ResetTransform();

                foreBrush.Dispose();
                if (zoomFactor != 1.0f) f.Dispose();
            }
            g.Restore(state);
        }
    }

    /// <summary>
    /// 长文本类型
    /// </summary>
    [Serializable]
    public class LongTextCellType : TextCellType
    {
        public LongTextCellType()
        {
            Multiline = true;
            WordWrap = true;
        }

        public override FieldType FieldType
        {
            get
            {
                return FieldType.LongText;
            }
        }

        public override object Clone()
        {
            LongTextCellType cellType = new LongTextCellType();
            cellType.Multiline = this.Multiline;
            cellType.WordWrap = this.WordWrap;
            return cellType;
        }

        protected override void GetTextRectangle(Graphics g, Rectangle r, ref Font f, FarPoint.Win.Spread.Appearance appearance, ref RectangleF rText, string paintString)
        {
            do
            {
                String Text = paintString.TrimStart('\r', '\n');
                SizeF sizef = g.MeasureString(Text, f);
                int LineNumber = 0, i = 1, start = 0;
                while (i < Text.Length)
                {
                    SizeF stringsize = g.MeasureString(Text.Substring(start, i - start + 1), f);
                    if (stringsize.Width > r.Width)
                    {
                        start = i;
                        LineNumber++;
                    }

                    i++;
                }

                if (start < Text.Length)
                    LineNumber++;

                SizeF size = new SizeF(r.Width, sizef.Height * LineNumber);
                FarPoint.Win.HorizontalAlignment alignment = this.ToHorizontalAlignment(appearance.HorizontalAlignment);
                FarPoint.Win.VerticalAlignment alignment2 = this.ToVerticalAlignment(appearance.VerticalAlignment);
                rText = new RectangleF(r.X, r.Y, size.Width, size.Height);
                if (alignment == FarPoint.Win.HorizontalAlignment.Center)
                {
                    rText.X += Convert.ToSingle((r.Width / 2.0) - (size.Width / 2.0));
                }
                else if (alignment == FarPoint.Win.HorizontalAlignment.Right)
                {
                    rText.X += r.Width - size.Width;
                }
                if (alignment2 == FarPoint.Win.VerticalAlignment.Center)
                {
                    rText.Y += Convert.ToSingle((r.Height / 2.0) - (size.Height / 2.0));
                }
                else if (alignment2 == FarPoint.Win.VerticalAlignment.Bottom)
                {
                    rText.Y += r.Height - size.Height;
                }

                if (rText.Height > r.Height)
                {
                    float fs = f.Size - 1f;
                    if (fs < 1)
                    {
                        fs = 1;
                        break;
                    }
                    f = new Font(f.FontFamily, fs, f.Style);
                }
                else
                {
                    break;
                }

            } while (true);
        }
    }

    /// <summary>
    /// 百分号类型
    /// </summary>
    [Serializable]
    public class PercentCellType : FarPoint.Win.Spread.CellType.PercentCellType, IConvertable, IGetFieldType
    {
        public FieldType FieldType
        {
            get
            {
                return FieldType.Percent;
            }
        }

        public override string ToString()
        {
            return FieldType.Description;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IConvertable 成员

        public bool ConvertTo(FieldType FieldType)
        {
            if (FieldType.Description == "数字" ||
                FieldType.Description == "货币" ||
                FieldType.Description == "条形码")
                return true;
            else
                return false;
        }

        public bool EqualsBasicDataType(FieldType FieldType)
        {
            if (FieldType.Description == "数字" ||
                FieldType.Description == "货币" ||
                FieldType.Description == "条形码")
                return true;
            else
                return false;
        }

        #endregion

        public override object Clone()
        {
            PercentCellType cellType = new PercentCellType();
            return cellType;
        }
    }

    /// <summary>
    /// 图片类型
    /// </summary>
    [Serializable]
    public class ImageCellType : FarPoint.Win.Spread.CellType.ImageCellType, IConvertable, IGetFieldType
    {
        ImageList List;
        Rectangle r = new Rectangle(0, 0, 100, 100);

        public ImageCellType()
        {
            List = new ImageList();
            List.Images.Add(IcoResource.System_Windows_Forms_PictureBox);
            List.TransparentColor = Color.Magenta;
        }

        public FieldType FieldType
        {
            get
            {
                return FieldType.Image;
            }
        }

        public override string ToString()
        {
            return FieldType.Description;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IConvertable 成员

        public bool ConvertTo(FieldType FieldType)
        {
            return false;
        }

        public bool EqualsBasicDataType(FieldType FieldType)
        {
            return false;
        }

        #endregion

        public override void StartEditing(EventArgs e, bool selectAll, bool autoClipboard)
        {
            base.StartEditing(e, selectAll, autoClipboard);
        }

        public override object Clone()
        {
            ImageCellType cellType = new ImageCellType();
            return cellType;
        }
        public override void PaintCell(Graphics g, Rectangle r, FarPoint.Win.Spread.Appearance appearance, object value, bool isSelected, bool isLocked, float zoomFactor)
        {
            if (value is Image)
            {
                base.PaintCell(g, r, appearance, value, isSelected, isLocked, zoomFactor);
            }
            else
            {
                appearance.HorizontalAlignment = CellHorizontalAlignment.Center;
                appearance.VerticalAlignment = CellVerticalAlignment.Center;
                base.PaintCell(g, r, appearance, List.Images[0], isSelected, isLocked, zoomFactor);
            }
        }

    }

    /// <summary>
    /// 超链接类型
    /// </summary>
    [Serializable]
    public class HyperLinkCellType : FarPoint.Win.Spread.CellType.HyperLinkCellType, IConvertable, IGetFieldType
    {
        public FieldType FieldType
        {
            get
            {
                return FieldType.HyperLink;
            }
        }

        public override string ToString()
        {
            return FieldType.Description;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IConvertable 成员

        public bool ConvertTo(FieldType FieldType)
        {
            return false;
        }

        public bool EqualsBasicDataType(FieldType FieldType)
        {
            return false;
        }

        #endregion

        public override object Clone()
        {
            HyperLinkCellType cellType = new HyperLinkCellType();
            return cellType;
        }
    }

    /// <summary>
    /// 货币类型
    /// </summary>
    [Serializable]
    public class CurrencyCellType : FarPoint.Win.Spread.CellType.CurrencyCellType, IConvertable, IGetFieldType
    {
        public FieldType FieldType
        {
            get
            {
                return FieldType.Currency;
            }
        }

        public override string ToString()
        {
            return FieldType.Description;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IConvertable 成员

        public bool ConvertTo(FieldType FieldType)
        {
            if (FieldType.Description == "数字" ||
                FieldType.Description == "百分号" ||
                FieldType.Description == "条形码")
                return true;
            else
                return false;
        }

        public bool EqualsBasicDataType(FieldType FieldType)
        {
            if (FieldType.Description == "数字" ||
                FieldType.Description == "百分号" ||
                FieldType.Description == "条形码")
                return true;
            else
                return false;
        }

        #endregion

        public override object Clone()
        {
            CurrencyCellType cellType = new CurrencyCellType();
            return cellType;
        }
    }

    /// <summary>
    /// 日期时间类型
    /// </summary>
    [Serializable]
    public class DateTimeCellType : FarPoint.Win.Spread.CellType.DateTimeCellType, IConvertable, IGetFieldType
    {
        public DateTimeCellType()
        {
        }

        public FieldType FieldType
        {
            get
            {
                return FieldType.DateTime;
            }
        }

        public override string ToString()
        {
            return FieldType.Description;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IConvertable 成员

        public bool ConvertTo(FieldType FieldType)
        {
            return false;
        }

        public bool EqualsBasicDataType(FieldType FieldType)
        {
            return false;
        }

        #endregion

        public override object Clone()
        {
            DateTimeCellType cellType = new DateTimeCellType();
            return cellType;
        }

        protected new virtual FarPoint.Win.HorizontalAlignment ToHorizontalAlignment(CellHorizontalAlignment alignment)
        {
            switch (alignment)
            {
                case CellHorizontalAlignment.Left: return FarPoint.Win.HorizontalAlignment.Left;
                case CellHorizontalAlignment.Center: return FarPoint.Win.HorizontalAlignment.Center;
                case CellHorizontalAlignment.Right: return FarPoint.Win.HorizontalAlignment.Right;
                default: return FarPoint.Win.HorizontalAlignment.Left;
            }
        }

        protected new virtual FarPoint.Win.VerticalAlignment ToVerticalAlignment(CellVerticalAlignment alignment)
        {
            switch (alignment)
            {
                case CellVerticalAlignment.Top: return FarPoint.Win.VerticalAlignment.Top;
                case CellVerticalAlignment.Center: return FarPoint.Win.VerticalAlignment.Center;
                case CellVerticalAlignment.Bottom: return FarPoint.Win.VerticalAlignment.Bottom;
                default: return FarPoint.Win.VerticalAlignment.Top;
            }
        }

        protected virtual void GetTextRectangle(Graphics g, Rectangle r, ref Font f, FarPoint.Win.Spread.Appearance appearance, ref RectangleF rText, string paintString)
        {
            do
            {
                String Text = paintString.TrimStart('\r', '\n');
                SizeF sizef = g.MeasureString(Text, f);
                int LineNumber = 0, i = 1, start = 0;
                while (i < Text.Length)
                {
                    SizeF stringsize = g.MeasureString(Text.Substring(start, i - start + 1), f);
                    if (stringsize.Width > r.Width)
                    {
                        start = i;
                        LineNumber++;
                    }

                    i++;
                }

                if (start < Text.Length)
                    LineNumber++;

                SizeF size = new SizeF(r.Width, sizef.Height * LineNumber);
                FarPoint.Win.HorizontalAlignment alignment = this.ToHorizontalAlignment(appearance.HorizontalAlignment);
                FarPoint.Win.VerticalAlignment alignment2 = this.ToVerticalAlignment(appearance.VerticalAlignment);
                rText = new RectangleF(r.X, r.Y, size.Width, size.Height);
                if (alignment == FarPoint.Win.HorizontalAlignment.Center)
                {
                    rText.X += Convert.ToSingle((r.Width / 2.0) - (size.Width / 2.0));
                }
                else if (alignment == FarPoint.Win.HorizontalAlignment.Right)
                {
                    rText.X += r.Width - size.Width;
                }
                if (alignment2 == FarPoint.Win.VerticalAlignment.Center)
                {
                    rText.Y += Convert.ToSingle((r.Height / 2.0) - (size.Height / 2.0));
                }
                else if (alignment2 == FarPoint.Win.VerticalAlignment.Bottom)
                {
                    rText.Y += r.Height - size.Height;
                }

                if (rText.Height > r.Height)
                {
                    float fs = f.Size - 1f;
                    if (fs < 1)
                    {
                        fs = 1;
                        break;
                    }
                    f = new Font(f.FontFamily, fs, f.Style);
                }
                else
                {
                    break;
                }

            } while (true);
        }

        public override void PaintCell(Graphics g, Rectangle r, FarPoint.Win.Spread.Appearance appearance, object value, bool isSelected, bool isLocked, float zoomFactor)
        {
            string s = null;
            StringFormat sf = new StringFormat();
            sf.FormatFlags |= StringFormatFlags.NoClip;

            FarPoint.Win.HorizontalAlignment HAlignment = this.ToHorizontalAlignment(appearance.HorizontalAlignment);
            switch (HAlignment)
            {
                case FarPoint.Win.HorizontalAlignment.Left:
                    sf.Alignment = StringAlignment.Near;
                    break;
                case FarPoint.Win.HorizontalAlignment.Right:
                    sf.Alignment = StringAlignment.Far;
                    break;
                default:
                    sf.Alignment = StringAlignment.Center;
                    break;
            }

            FarPoint.Win.VerticalAlignment VAlignment = this.ToVerticalAlignment(appearance.VerticalAlignment);
            switch (VAlignment)
            {
                case FarPoint.Win.VerticalAlignment.Top:
                    sf.LineAlignment = StringAlignment.Near;
                    break;
                case FarPoint.Win.VerticalAlignment.Bottom:
                    sf.LineAlignment = StringAlignment.Far;
                    break;
                default:
                    sf.LineAlignment = StringAlignment.Center;
                    break;
            }

            RectangleF rText = new RectangleF(r.X, r.Y, r.Width, r.Height);

            Color backColor = appearance.BackColor;
            Color foreColor = appearance.ForeColor;
            if (isSelected)
            {
                if (!appearance.SelectionBackColor.IsEmpty)
                    backColor = appearance.SelectionBackColor;
                if (!appearance.SelectionForeColor.IsEmpty)
                    foreColor = appearance.SelectionForeColor;
            }
            if (isLocked)
            {
                if (!appearance.LockBackColor.IsEmpty)
                    backColor = appearance.LockBackColor;
                if (!appearance.LockForeColor.IsEmpty)
                    foreColor = appearance.LockForeColor;
            }

            SolidBrush backBrush, foreBrush;
            GraphicsState state = g.Save();

            g.IntersectClip(r);

            if (value != null)
                s = this.Format(value);

            sf.Trimming = StringTrim;
            if (appearance.Font == null)
                appearance.Font = new Font("System", 10);

            backBrush = new SolidBrush(backColor);
            g.FillRectangle(backBrush, r);
            backBrush.Dispose();

            if (s != null && s.Length > 0)
            {
                foreBrush = new SolidBrush(foreColor);

                Font f = (Font)appearance.Font;
                if (zoomFactor != 1.0f)
                {
                    float fs = f.Size * zoomFactor;
                    f = new Font(f.FontFamily, fs, f.Style);
                }

                GetTextRectangle(g, r, ref f, appearance, ref rText, s);

                switch (TextOrientation)
                {
                    case TextOrientation.TextHorizontal:
                        g.DrawString(s, f, foreBrush, rText, sf);
                        break;

                    case TextOrientation.TextHorizontalFlipped:
                        g.TranslateTransform(rText.Width, rText.Height / 2);
                        g.RotateTransform(180);
                        g.DrawString(s, f, foreBrush, new RectangleF(-rText.X, -(rText.Y + 1 + (rText.Height / 2)), rText.Width, rText.Height), sf);
                        break;

                    case TextOrientation.TextVertical:
                        g.TranslateTransform(rText.Width / 2, 0);
                        g.RotateTransform(90);
                        g.DrawString(s, f, foreBrush, new RectangleF(rText.Y, -rText.X - (rText.Width / 2), r.Height, rText.Width), sf);
                        break;

                    case TextOrientation.TextVerticalFlipped:
                        g.TranslateTransform(rText.Width / 2, rText.Height);
                        g.RotateTransform(-90);
                        g.DrawString(s, f, foreBrush, new RectangleF(-rText.Y, rText.X - (r.Width / 2), r.Height, r.Width), sf);
                        break;

                    case TextOrientation.TextTopDown:
                        for (int i = 0; i < s.Length; i++)
                        {
                            SizeF textSize = g.MeasureString(s[i].ToString(CultureInfo.InvariantCulture), f);
                            sf.Alignment = StringAlignment.Center;
                            sf.LineAlignment = StringAlignment.Near;
                            g.DrawString(s[i].ToString(CultureInfo.InvariantCulture), f, foreBrush, rText, sf);
                            rText.Y += (int)Math.Ceiling(textSize.Height);
                        }
                        break;
                }

                g.ResetTransform();

                foreBrush.Dispose();
                if (zoomFactor != 1.0f) f.Dispose();
            }
            g.Restore(state);
        }
    }

    /// <summary>
    /// 复选框类型
    /// </summary>
    [Serializable]
    public class CheckBoxCellType : FarPoint.Win.Spread.CellType.CheckBoxCellType, IConvertable, IGetFieldType
    {
        public FieldType FieldType
        {
            get
            {
                return FieldType.CheckBox;
            }
        }

        public override string ToString()
        {
            return FieldType.Description;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IConvertable 成员

        public bool ConvertTo(FieldType FieldType)
        {
            return false;
        }

        public bool EqualsBasicDataType(FieldType FieldType)
        {
            return false;
        }

        #endregion

        public override object Clone()
        {
            CheckBoxCellType cellType = new CheckBoxCellType();
            return cellType;
        }
    }

    /// <summary>
    /// 上下标类型
    /// </summary>
    [Serializable]
    public class RichTextCellType : FarPoint.Win.Spread.CellType.RichTextCellType, IConvertable, IGetFieldType
    {
        ContextMenu ContextMenu;
        FarPoint.Win.Spread.CellType.RichTextEditor Editor;
        YqunRichTextBox DrawingOperation;
        Font DefaultFont = new Font("Times New Roman", 10);

        public RichTextCellType()
        {
            Multiline = true;
            WordWrap = true;

            ContextMenu = new ContextMenu();

            MenuItem MenuItem = new MenuItem();
            MenuItem.Text = "剪切";
            MenuItem.Click += new EventHandler(MenuItem_Click);
            ContextMenu.MenuItems.Add(MenuItem);

            MenuItem = new MenuItem();
            MenuItem.Text = "复制";
            MenuItem.Click += new EventHandler(MenuItem_Click);
            ContextMenu.MenuItems.Add(MenuItem);

            MenuItem = new MenuItem();
            MenuItem.Text = "粘贴";
            MenuItem.Click += new EventHandler(MenuItem_Click);
            ContextMenu.MenuItems.Add(MenuItem);

            MenuItem = new MenuItem();
            MenuItem.Text = "-";
            ContextMenu.MenuItems.Add(MenuItem);

            MenuItem = new MenuItem();
            MenuItem.Text = "上标";
            MenuItem.Click += new EventHandler(MenuItem_Click);
            ContextMenu.MenuItems.Add(MenuItem);

            MenuItem = new MenuItem();
            MenuItem.Text = "下标";
            MenuItem.Click += new EventHandler(MenuItem_Click);
            ContextMenu.MenuItems.Add(MenuItem);

            MenuItem = new MenuItem();
            MenuItem.Text = "取消上下标";
            MenuItem.Click += new EventHandler(MenuItem_Click);
            ContextMenu.MenuItems.Add(MenuItem);

            DrawingOperation = new YqunRichTextBox();
            DrawingOperation.Font = DefaultFont;
        }

        void MenuItem_Click(object sender, EventArgs e)
        {
            MenuItem MenuItem = sender as MenuItem;
            if (MenuItem.Text == "上标")
            {
                Editor.SelectionCharOffset = (int)(Editor.SelectionFont.Size - 4);
                Editor.SelectionFont = new Font(Editor.SelectionFont.Name, Editor.SelectionFont.Size - 4);
            }
            else if (MenuItem.Text == "下标")
            {
                Editor.SelectionCharOffset = -(int)(Editor.SelectionFont.Size - 4);
                Editor.SelectionFont = new Font(Editor.SelectionFont.Name, Editor.SelectionFont.Size - 4);
            }
            else if (MenuItem.Text == "取消上下标")
            {
                Editor.SelectionCharOffset = 0;
                Editor.SelectionFont = Editor.Font;
            }
            else if (MenuItem.Text == "剪切")
            {
                Editor.Cut();
            }
            else if (MenuItem.Text == "复制")
            {
                Editor.Copy();
            }
            else if (MenuItem.Text == "粘贴")
            {
                Editor.Paste();
            }
        }

        public override Control GetEditorControl(FarPoint.Win.Spread.Appearance appearance, float zoomFactor)
        {
            Control c = base.GetEditorControl(appearance, zoomFactor);

            if (c.ContextMenu != ContextMenu)
            {
                c.ContextMenu = ContextMenu;
                Editor = c as FarPoint.Win.Spread.CellType.RichTextEditor;
                Editor.Font = DefaultFont;
                Editor.SelectionFont = DefaultFont;
            }
            return Editor;
        }

        public FieldType FieldType
        {
            get
            {
                return FieldType.RichText;
            }
        }

        public override string ToString()
        {
            return FieldType.Description;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IConvertable 成员

        public bool ConvertTo(FieldType FieldType)
        {
            return false;
        }

        public bool EqualsBasicDataType(FieldType FieldType)
        {
            return false;
        }

        #endregion

        public override object Clone()
        {
            RichTextCellType cellType = new RichTextCellType();
            return cellType;
        }

        public override void PaintCell(Graphics g, Rectangle r, FarPoint.Win.Spread.Appearance appearance, object value, bool isSelected, bool isLocked, float zoomFactor)
        {
            if (appearance.Font == null)
                appearance.Font = new Font("Times New Roman", 10);

            DrawingOperation.Size = r.Size;
            DrawingOperation.Font = appearance.Font;

            Color backColor = appearance.BackColor;
            Color foreColor = appearance.ForeColor;

            if (isLocked)
            {
                if (!appearance.LockBackColor.IsEmpty)
                {
                    backColor = appearance.LockBackColor;
                }
                if (!appearance.LockForeColor.IsEmpty)
                {
                    foreColor = appearance.LockForeColor;
                }
            }

            if (isSelected)
            {
                if (!appearance.SelectionBackColor.IsEmpty)
                {
                    backColor = appearance.SelectionBackColor;
                }
                if (!appearance.SelectionForeColor.IsEmpty)
                {
                    foreColor = appearance.SelectionForeColor;
                }
            }

            if (backColor != Color.Empty)
            {
                SolidBrush brush = new SolidBrush(backColor);
                g.FillRectangle(brush, r);
                brush.Dispose();
            }

            if ((value == null) || ((value is string) && (((string)value).Length == 0)))
            {
                if (backColor == Color.Transparent)
                {
                    backColor = Color.White;
                }

                if (this.BackgroundImage != null)
                {
                    this.BackgroundImage.Paint(g, r);
                }
            }
            else
            {
                if (value is String)
                {
                    try
                    {
                        if (value.ToString().IndexOf(@"{\rtf1\") == 0)
                        {
                            DrawingOperation.Rtf = value.ToString();
                        }
                        else
                        {
                            DrawingOperation.Text = value.ToString();
                        }
                    }
                    catch
                    {
                        DrawingOperation.Text = value.ToString();
                    }
                }
                else
                {
                    try
                    {
                        DrawingOperation.Text = value.ToString();
                    }
                    catch
                    {
                    }
                }

                if (appearance.RightToLeft)
                {
                    DrawingOperation.RightToLeft = RightToLeft.Yes;
                }

                DrawingOperation.ForeColor = foreColor;
                DrawingOperation.BackColor = backColor;
                DrawingOperation.ZoomFactor = zoomFactor;

                float LinesHeight = 0f, LinesWidth = 0f;
                int CharStart = 0;
                if (DrawingOperation.TextLength != 0)
                {
                    CharStart = DrawingOperation.GetFirstCharIndexFromLine(0);
                }

                int CharEnd = 0;
                if (DrawingOperation.TextLength >= 1)
                {
                    CharEnd = DrawingOperation.TextLength - 1;
                }

                string MeasureString = string.Empty;
                try
                {
                    try
                    {
                        MeasureString = DrawingOperation.Text.Substring(CharStart, CharEnd - CharStart + 1);
                    }
                    catch
                    {
                        MeasureString = DrawingOperation.Text.Substring(CharStart);
                    }
                }
                catch
                {
                    MeasureString = DrawingOperation.Text;
                }

                if (MeasureString != "")
                {
                    SizeF LineSizeF = g.MeasureString(MeasureString.Substring(CharStart, CharEnd - CharStart + 1).Replace("\n", ""), DrawingOperation.Font);
                    LinesWidth = LineSizeF.Width;

                    int LineCount = DrawingOperation.GetLineFromCharIndex(DrawingOperation.TextLength - 1) + 1;
                    LinesHeight = LineSizeF.Height * LineCount;
                }

                float Left = 0f, Top = 0f;
                if (appearance.RightToLeft)
                {
                    if ((appearance.HorizontalAlignment == CellHorizontalAlignment.Left) || (appearance.HorizontalAlignment == CellHorizontalAlignment.General))
                    {
                        Left = r.Width - LinesWidth;
                        if (Left < 0) Left = 0;
                    }
                    else if (appearance.HorizontalAlignment == CellHorizontalAlignment.Center)
                    {
                        Left = (r.Width - LinesWidth) / 2;
                        if (Left < 0) Left = 0;
                    }
                    else if (appearance.HorizontalAlignment == CellHorizontalAlignment.Right)
                    {
                        Left = 0;
                    }
                }
                else if ((appearance.HorizontalAlignment == CellHorizontalAlignment.Left) || (appearance.HorizontalAlignment == CellHorizontalAlignment.General))
                {
                    Left = 0;
                }
                else if (appearance.HorizontalAlignment == CellHorizontalAlignment.Center)
                {
                    Left = (r.Width - LinesWidth) / 2;
                    if (Left < 0) Left = 0;
                }
                else if (appearance.HorizontalAlignment == CellHorizontalAlignment.Right)
                {
                    Left = r.Width - LinesWidth;
                    if (Left < 0) Left = 0;
                }

                Top = (r.Height - LinesHeight) / 2;
                if (Top < 0) Top = 3;

                if (DrawingOperation.TextLength == 0)
                {
                    if (backColor == Color.Transparent)
                    {
                        backColor = Color.White;
                    }
                    if (this.BackgroundImage != null)
                    {
                        this.BackgroundImage.Paint(g, r);
                    }
                }
                else
                {
                    if (isLocked && !appearance.LockForeColor.IsEmpty)
                    {
                        DrawingOperation.SelectAll();
                        DrawingOperation.SelectionColor = appearance.LockForeColor;
                    }
                    if (isSelected)
                    {
                        DrawingOperation.SelectAll();
                        DrawingOperation.SelectionColor = foreColor;
                    }

                    DrawingOperation.Draw(g, r, 0, Top);
                }
            }
        }
    }

    /// <summary>
    /// 删除线类型
    /// </summary>
    [Serializable]
    public class DeleteLineCellType : FarPoint.Win.Spread.CellType.RichTextCellType, IConvertable, IGetFieldType
    {
        ContextMenu ContextMenu;
        FarPoint.Win.Spread.CellType.RichTextEditor Editor;
        YqunRichTextBox DrawingOperation;
        Font DefaultFont = new Font("宋体", 10.5f);


        public DeleteLineCellType()
        {
            Multiline = true;
            WordWrap = true;

            ContextMenu = new ContextMenu();

            MenuItem MenuItem = new MenuItem();
            MenuItem.Text = "删除线";
            MenuItem.Click += new EventHandler(MenuItem_Click);
            ContextMenu.MenuItems.Add(MenuItem);

            MenuItem = new MenuItem();
            MenuItem.Text = "取消删除线";
            MenuItem.Click += new EventHandler(MenuItem_Click);
            ContextMenu.MenuItems.Add(MenuItem);


            DrawingOperation = new YqunRichTextBox();
            DrawingOperation.Font = DefaultFont;
        }

        void MenuItem_Click(object sender, EventArgs e)
        {
            MenuItem MenuItem = sender as MenuItem;
            if (MenuItem.Text == "删除线")
            {
                Editor.SelectionFont = new Font(Editor.Font.Name, Editor.Font.Size, FontStyle.Strikeout);
            }
            if (MenuItem.Text == "取消删除线")
            {
                Editor.SelectionFont = new Font(Editor.Font.Name, Editor.Font.Size, FontStyle.Regular);
            }
            else if (MenuItem.Text == "剪切")
            {
                Editor.Cut();
            }
            else if (MenuItem.Text == "复制")
            {
                Editor.Copy();
            }
            else if (MenuItem.Text == "粘贴")
            {
                Editor.Paste();
            }
        }

        public override Control GetEditorControl(FarPoint.Win.Spread.Appearance appearance, float zoomFactor)
        {
            Control c = base.GetEditorControl(appearance, zoomFactor);

            if (c.ContextMenu != ContextMenu)
            {
                c.ContextMenu = ContextMenu;
                Editor = c as FarPoint.Win.Spread.CellType.RichTextEditor;
                Editor.Font = DefaultFont;
                Editor.SelectionFont = DefaultFont;
            }
            return Editor;
        }

        public FieldType FieldType
        {
            get
            {
                return FieldType.DeleteLine;
            }
        }

        public override string ToString()
        {
            return FieldType.Description;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IConvertable 成员

        public bool ConvertTo(FieldType FieldType)
        {
            return false;
        }

        public bool EqualsBasicDataType(FieldType FieldType)
        {
            return false;
        }

        #endregion

        public override object Clone()
        {
            RichTextCellType cellType = new RichTextCellType();
            return cellType;
        }

        public override void PaintCell(Graphics g, Rectangle r, FarPoint.Win.Spread.Appearance appearance, object value, bool isSelected, bool isLocked, float zoomFactor)
        {
            appearance.Font = DefaultFont;
            if (appearance.Font == null)
                appearance.Font = DefaultFont;

            DrawingOperation.Size = r.Size;
            DrawingOperation.Font = appearance.Font;

            Color backColor = appearance.BackColor;
            Color foreColor = appearance.ForeColor;

            if (isLocked)
            {
                if (!appearance.LockBackColor.IsEmpty)
                {
                    backColor = appearance.LockBackColor;
                }
                if (!appearance.LockForeColor.IsEmpty)
                {
                    foreColor = appearance.LockForeColor;
                }
            }

            if (isSelected)
            {
                if (!appearance.SelectionBackColor.IsEmpty)
                {
                    backColor = appearance.SelectionBackColor;
                }
                if (!appearance.SelectionForeColor.IsEmpty)
                {
                    foreColor = appearance.SelectionForeColor;
                }
            }

            if (backColor != Color.Empty)
            {
                SolidBrush brush = new SolidBrush(backColor);
                g.FillRectangle(brush, r);
                brush.Dispose();
            }

            if ((value == null) || ((value is string) && (((string)value).Length == 0)))
            {
                if (backColor == Color.Transparent)
                {
                    backColor = Color.White;
                }

                if (this.BackgroundImage != null)
                {
                    this.BackgroundImage.Paint(g, r);
                }
            }
            else
            {
                if (value is String)
                {
                    try
                    {
                        if (value.ToString().IndexOf(@"{\rtf1\") == 0)
                        {
                            DrawingOperation.Rtf = value.ToString();
                        }
                        else
                        {
                            DrawingOperation.Text = value.ToString();
                        }
                    }
                    catch
                    {
                        DrawingOperation.Text = value.ToString();
                    }
                }
                else
                {
                    try
                    {
                        DrawingOperation.Text = value.ToString();
                    }
                    catch
                    {
                    }
                }

                if (appearance.RightToLeft)
                {
                    DrawingOperation.RightToLeft = RightToLeft.Yes;
                }

                DrawingOperation.ForeColor = foreColor;
                DrawingOperation.BackColor = backColor;
                DrawingOperation.ZoomFactor = zoomFactor;

                float LinesHeight = 0f, LinesWidth = 0f;
                int CharStart = 0;
                if (DrawingOperation.TextLength != 0)
                {
                    CharStart = DrawingOperation.GetFirstCharIndexFromLine(0);
                }

                int CharEnd = 0;
                if (DrawingOperation.TextLength >= 1)
                {
                    CharEnd = DrawingOperation.TextLength - 1;
                }

                string MeasureString = string.Empty;
                try
                {
                    try
                    {
                        MeasureString = DrawingOperation.Text.Substring(CharStart, CharEnd - CharStart + 1);
                    }
                    catch
                    {
                        MeasureString = DrawingOperation.Text.Substring(CharStart);
                    }
                }
                catch
                {
                    MeasureString = DrawingOperation.Text;
                }

                if (MeasureString != "")
                {
                    SizeF LineSizeF = g.MeasureString(MeasureString.Substring(CharStart, CharEnd - CharStart + 1).Replace("\n", ""), DrawingOperation.Font);
                    LinesWidth = LineSizeF.Width;

                    int LineCount = DrawingOperation.GetLineFromCharIndex(DrawingOperation.TextLength - 1) + 1;
                    LinesHeight = LineSizeF.Height * LineCount;
                }

                float Left = 0f, Top = 0f;
                if (appearance.RightToLeft)
                {
                    if ((appearance.HorizontalAlignment == CellHorizontalAlignment.Left) || (appearance.HorizontalAlignment == CellHorizontalAlignment.General))
                    {
                        Left = r.Width - LinesWidth;
                        if (Left < 0) Left = 0;
                    }
                    else if (appearance.HorizontalAlignment == CellHorizontalAlignment.Center)
                    {
                        Left = (r.Width - LinesWidth) / 2;
                        if (Left < 0) Left = 0;
                    }
                    else if (appearance.HorizontalAlignment == CellHorizontalAlignment.Right)
                    {
                        Left = 0;
                    }
                }
                else if ((appearance.HorizontalAlignment == CellHorizontalAlignment.Left) || (appearance.HorizontalAlignment == CellHorizontalAlignment.General))
                {
                    Left = 0;
                }
                else if (appearance.HorizontalAlignment == CellHorizontalAlignment.Center)
                {
                    Left = (r.Width - LinesWidth) / 2;
                    if (Left < 0) Left = 0;
                }
                else if (appearance.HorizontalAlignment == CellHorizontalAlignment.Right)
                {
                    Left = r.Width - LinesWidth;
                    if (Left < 0) Left = 0;
                }

                Top = (r.Height - LinesHeight) / 2;
                if (Top < 0) Top = 3;

                if (DrawingOperation.TextLength == 0)
                {
                    if (backColor == Color.Transparent)
                    {
                        backColor = Color.White;
                    }
                    if (this.BackgroundImage != null)
                    {
                        this.BackgroundImage.Paint(g, r);
                    }
                }
                else
                {
                    if (isLocked && !appearance.LockForeColor.IsEmpty)
                    {
                        DrawingOperation.SelectAll();
                        DrawingOperation.SelectionColor = appearance.LockForeColor;
                    }
                    if (isSelected)
                    {
                        DrawingOperation.SelectAll();
                        DrawingOperation.SelectionColor = foreColor;
                    }

                    DrawingOperation.Draw(g, r, 0, Top);
                }
            }
        }
    }
    /// <summary>
    /// 条形码类型
    /// </summary>
    [Serializable]
    public class BarCodeCellType : FarPoint.Win.Spread.CellType.BarCodeCellType, IConvertable, IGetFieldType
    {
        public FieldType FieldType
        {
            get
            {
                return FieldType.BarCode;
            }
        }

        public override string ToString()
        {
            return FieldType.Description;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IConvertable 成员

        public bool ConvertTo(FieldType FieldType)
        {
            if (FieldType.Description == "数字" ||
                FieldType.Description == "百分号" ||
                FieldType.Description == "货币")
                return true;
            else
                return false;
        }

        public bool EqualsBasicDataType(FieldType FieldType)
        {
            if (FieldType.Description == "数字" ||
                FieldType.Description == "百分号" ||
                FieldType.Description == "货币")
                return true;
            else
                return false;
        }

        #endregion

        public override object Clone()
        {
            BarCodeCellType cellType = new BarCodeCellType();
            return cellType;
        }
    }

    /// <summary>
    /// 下拉框类型，支持参照
    /// </summary>
    [Serializable]
    public class DownListCellType : FarPoint.Win.Spread.CellType.EditBaseCellType, IConvertable, IGetFieldType, IDisposable
    {
        //[NonSerialized]
        //MultiColumnList MultiColumnView;
        [NonSerialized]
        TreeViewList TreeView;

        DictionaryReference ditionaryReference;
        SheetReference sheetReference;

        public event EventHandler<ReferenceEventArgs> ReferenceFinished;

        public DownListCellType()
        {
            ditionaryReference = new DictionaryReference();
            sheetReference = new SheetReference();

            TreeView = new TreeViewList(ditionaryReference);
            TreeView.ValueChanged += new EventHandler(Reference_ValueChanged);

            //MultiColumnView = new MultiColumnList(sheetReference);
            //MultiColumnView.ValueChanged += new EventHandler(Reference_ValueChanged);
        }

        public override object IsReservedLocation(Graphics g, int x, int y, Rectangle rc, FarPoint.Win.Spread.Appearance appearance, object value, float zoomFactor)
        {
            TreeView.size = rc.Size;
            //MultiColumnView.size = rc.Size;

            Rectangle rectButton = new Rectangle(rc.X + rc.Width - SystemInformation.VerticalScrollBarWidth, rc.Y, SystemInformation.VerticalScrollBarWidth, rc.Height);
            if (rectButton.Contains(new Point(x, y)))
                return this;
            else
                return null;
        }

        public override Control GetEditorControl(FarPoint.Win.Spread.Appearance appearance, float zoomFactor)
        {
            Control c = base.GetEditorControl(appearance, zoomFactor);
            ((GeneralEditor)c).ButtonStyle = ButtonStyle.DropDown;
            ((GeneralEditor)c).ControlType = ControlType.ButtonEdit;

            //if (!DesignMode)
            //{
            if (ReferenceStyle == ReferenceStyle.Dictionary)
            {
                TreeView.InitializeData();
            }
            else if (ReferenceStyle == ReferenceStyle.Sheet)
            {
                //MultiColumnView.InitializeData();
            }
            //}

            return c;
        }

        public override void PaintCell(Graphics g, Rectangle r, FarPoint.Win.Spread.Appearance appearance, object value, bool isSelected, bool isLocked, float zoomFactor)
        {
            Rectangle newR = new Rectangle(r.X, r.Y, r.Width - SystemInformation.VerticalScrollBarWidth, r.Height);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("FarPoint.Win.Images", System.Reflection.Assembly.GetAssembly(typeof(FarPoint.Win.IElement)));

            FarPoint.Win.ButtonElement b = new FarPoint.Win.ButtonElement();
            b.Visible = DropDownButton;
            b.Border = new BevelBorder(BevelBorderType.Raised, SystemColors.ControlLightLight, SystemColors.ControlDark, 1);
            if (rm != null)
            {
                b.ForegroundImage = new Picture((System.Drawing.Image)rm.GetObject("dropDown"), RenderStyle.StretchAndScale, Color.Magenta, FarPoint.Win.HorizontalAlignment.Center, FarPoint.Win.VerticalAlignment.Center);
            }
            Rectangle buttonRect = new Rectangle(r.X + r.Width - SystemInformation.VerticalScrollBarWidth, r.Y, SystemInformation.VerticalScrollBarWidth, r.Height);
            #region 下拉框自动缩小
            if (appearance.Font == null)
                appearance.Font = new Font("System", 10);
            string s = "";
            if (value != null)
                s = this.Format(value);
            //RectangleF rText = new RectangleF(r.X, r.Y, r.Width, r.Height);
            Font f = (Font)appearance.Font;
            if (zoomFactor != 1.0f)
            {
                float fs = f.Size * zoomFactor;
                f = new Font(f.FontFamily, fs, f.Style);
            }

            //rText = r;
            SizeF sf = g.MeasureString(s.ToString(), f);
            while ((sf.Height + 5 >= r.Height|| sf.Width + 5 > r.Width) && f.Size >= 5 )
            {
                f = new Font(f.FontFamily, f.Size - 0.2f, f.Style);
                sf = g.MeasureString(s.ToString(), f);
            }
            appearance.Font = f;
            #endregion

            base.PaintCell(g, DropDownButton ? newR : r, appearance, value, isSelected, isLocked, zoomFactor);
            if (DropDownButton)
            {
                b.OnPaintBackground(g, buttonRect);
                b.OnPaint(g, buttonRect);
            }
        }

        void Reference_ValueChanged(object sender, EventArgs e)
        {
            IReferenceData ReferenceData = sender as IReferenceData;
            ReferenceEventArgs refArgs = new ReferenceEventArgs(ReferenceInfo, ReferenceData.Data);
            OnReferenceFinished(refArgs);
        }

        public FieldType FieldType
        {
            get
            {
                return FieldType.DownList;
            }
        }

        Boolean _DropDownButton = true;
        public new Boolean DropDownButton
        {
            get
            {
                return _DropDownButton;
            }
            set
            {
                _DropDownButton = value;
            }
        }

        public override string ToString()
        {
            return FieldType.Description;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IConvertable 成员

        public bool ConvertTo(FieldType FieldType)
        {
            return false;
        }

        public bool EqualsBasicDataType(FieldType FieldType)
        {
            return false;
        }

        #endregion

        private DropDownStyle _Style = DropDownStyle.TreeList;
        public DropDownStyle DropDownStyle
        {
            get
            {
                return _Style;
            }
            set
            {
                _Style = value;
                if (!DesignMode)
                {
                    switch (_Style)
                    {
                        case DropDownStyle.TreeList:
                            SubEditor = TreeView;
                            break;
                        case DropDownStyle.MultiColumnList:
                            //SubEditor = MultiColumnView;
                            break;
                    }
                }
            }
        }

        public ReferenceInfoBase ReferenceInfo
        {
            get
            {
                if (_Style == DropDownStyle.TreeList)
                {
                    return ditionaryReference;
                }
                else
                {
                    return sheetReference;
                }
            }
        }

        public ReferenceStyle ReferenceStyle
        {
            get
            {
                if (_Style == DropDownStyle.TreeList)
                {
                    return ReferenceStyle.Dictionary;
                }
                else
                {
                    return ReferenceStyle.Sheet;
                }
            }
            set
            {
                switch (value)
                {
                    case ReferenceStyle.Dictionary:
                        DropDownStyle = DropDownStyle.TreeList;
                        break;
                    case ReferenceStyle.Sheet:
                        DropDownStyle = DropDownStyle.MultiColumnList;
                        break;
                }
            }
        }

        Boolean _DesignMode = true;
        public Boolean DesignMode
        {
            get
            {
                return _DesignMode;
            }
            set
            {
                _DesignMode = value;
                if (_DesignMode)
                {
                    SubEditor = null;
                }
                else
                {
                    switch (ReferenceStyle)
                    {
                        case ReferenceStyle.Dictionary:
                            DropDownStyle = DropDownStyle.TreeList;
                            break;
                        case ReferenceStyle.Sheet:
                            DropDownStyle = DropDownStyle.MultiColumnList;
                            break;
                    }
                }
            }
        }

        protected void OnReferenceFinished(ReferenceEventArgs e)
        {
            if (ReferenceFinished != null)
            {
                ReferenceFinished(this, e);
            }
        }

        #region 序列化

        public override bool Serialize(System.Xml.XmlTextWriter w)
        {
            bool Result = base.Serialize(w);

            w.WriteElementString("DesignMode", this.DesignMode.ToString(CultureInfo.InvariantCulture));
            Serializer.SerializeEnum(this.DropDownStyle, "DropDownStyle", w);
            Serializer.SerializeEnum(this.ReferenceStyle, "ReferenceStyle", w);

            w.WriteStartElement("ReferenceInfo");
            if (ReferenceStyle == ReferenceStyle.Dictionary)
            {
                Serializer.SerializeString(ditionaryReference.TableName, "TableName", w);
                Serializer.SerializeString(ditionaryReference.ColumnName, "ColumnName", w);
                Serializer.SerializeString(ditionaryReference.ReferenceXml, "ReferenceXml", w);
                Serializer.SerializeString(ditionaryReference.DictionaryIndex, "DictionaryIndex", w);
            }
            else
            {
                Serializer.SerializeString(sheetReference.TableName, "TableName", w);
                Serializer.SerializeString(sheetReference.ColumnName, "ColumnName", w);
                Serializer.SerializeString(sheetReference.ReferenceXml, "ReferenceXml", w);

                w.WriteStartElement("ReferenceItems");
                foreach (ReferenceItem Item in sheetReference.ReferenceItems)
                {
                    w.WriteStartElement("ReferenceItem");
                    Serializer.SerializeString(Item.TableName, "Item_TableName", w);
                    Serializer.SerializeString(Item.ColumnName, "Item_ColumnName", w);
                    Serializer.SerializeString(Item.ReferenceTableName, "Item_ReferenceTableName", w);
                    Serializer.SerializeString(Item.ReferenceTableText, "Item_ReferenceTableText", w);
                    Serializer.SerializeString(Item.ReferenceColumnName, "Item_ReferenceColumnName", w);
                    Serializer.SerializeString(Item.ReferenceColumnText, "Item_ReferenceColumnText", w);
                    w.WriteEndElement();
                }
                w.WriteEndElement();

                Serializer.SerializeString(BinarySerializer.Serialize(sheetReference.DataFilter), "DataFilter", w);
            }
            w.WriteEndElement();

            return Result;
        }

        public override bool Deserialize(System.Xml.XmlNodeReader r)
        {
            try
            {
                Boolean Result = base.Deserialize(r);

                Boolean flag = false;
                Boolean flag1 = false;
                Boolean flag2 = false;
                Boolean flag3 = false;
                Boolean flag4 = false;
                Boolean flag5 = false;
                Boolean flag6 = false;
                Boolean flag7 = false;
                Boolean flag8 = false;
                Boolean flag9 = false;
                Boolean flag10 = false;
                Boolean flag11 = false;
                Boolean flag12 = false;
                Boolean flag13 = false;
                Boolean flag14 = false;

                ReferenceItem Item = null;

                if (r.IsEmptyElement)
                {
                    return Result;
                }

            Label_0060:
                if (flag)
                {
                    if (!r.Read())
                    {
                        return Result;
                    }
                }
                else
                {
                    flag = Result;
                }

                switch (r.NodeType)
                {
                    case XmlNodeType.Element:
                        if (r.Name.Equals("DropDownStyle"))
                        {
                            flag1 = true;
                        }
                        else if (r.Name.Equals("ReferenceStyle"))
                        {
                            flag2 = true;
                        }
                        else if (r.Name.Equals("DesignMode"))
                        {
                            flag3 = true;
                        }
                        else if (r.Name.Equals("TableName"))
                        {
                            flag4 = true;
                        }
                        else if (r.Name.Equals("ColumnName"))
                        {
                            flag5 = true;
                        }
                        else if (r.Name.Equals("ReferenceXml"))
                        {
                            flag6 = true;
                        }
                        else if (r.Name.Equals("DictionaryIndex"))
                        {
                            flag7 = true;
                        }
                        else if (r.Name.Equals("ReferenceItem"))
                        {
                            Item = new ReferenceItem();
                        }
                        else if (r.Name.Equals("Item_TableName"))
                        {
                            flag8 = true;
                        }
                        else if (r.Name.Equals("Item_ColumnName"))
                        {
                            flag9 = true;
                        }
                        else if (r.Name.Equals("Item_ReferenceTableName"))
                        {
                            flag10 = true;
                        }
                        else if (r.Name.Equals("Item_ReferenceTableText"))
                        {
                            flag11 = true;
                        }
                        else if (r.Name.Equals("Item_ReferenceColumnName"))
                        {
                            flag12 = true;
                        }
                        else if (r.Name.Equals("Item_ReferenceColumnText"))
                        {
                            flag13 = true;
                        }
                        else if (r.Name.Equals("DataFilter"))
                        {
                            flag14 = true;
                        }
                        goto Label_0060;
                    case XmlNodeType.Text:
                        if (flag1)
                        {
                            this.DropDownStyle = (DropDownStyle)Enum.Parse(typeof(DropDownStyle), r.Value);
                        }
                        if (flag2)
                        {
                            this.ReferenceStyle = (ReferenceStyle)Enum.Parse(typeof(ReferenceStyle), r.Value);
                        }
                        if (flag3)
                        {
                            this.DesignMode = Convert.ToBoolean(r.Value);
                        }
                        if (flag4)
                        {
                            if (ReferenceStyle == ReferenceStyle.Dictionary)
                            {
                                ditionaryReference.TableName = r.Value;
                            }
                            else if (ReferenceStyle == ReferenceStyle.Sheet)
                            {
                                sheetReference.TableName = r.Value;
                            }
                        }
                        if (flag5)
                        {
                            if (ReferenceStyle == ReferenceStyle.Dictionary)
                            {
                                ditionaryReference.ColumnName = r.Value;
                            }
                            else if (ReferenceStyle == ReferenceStyle.Sheet)
                            {
                                sheetReference.ColumnName = r.Value;
                            }
                        }
                        if (flag6)
                        {
                            if (ReferenceStyle == ReferenceStyle.Dictionary)
                            {
                                ditionaryReference.ReferenceXml = r.Value;
                            }
                            else if (ReferenceStyle == ReferenceStyle.Sheet)
                            {
                                sheetReference.ReferenceXml = r.Value;
                            }
                        }
                        if (flag7)
                        {
                            if (ReferenceStyle == ReferenceStyle.Dictionary)
                            {
                                ditionaryReference.DictionaryIndex = r.Value;
                            }
                        }
                        if (flag8)
                        {
                            if (ReferenceStyle == ReferenceStyle.Sheet && Item != null)
                            {
                                Item.TableName = r.Value;
                            }
                        }
                        if (flag9)
                        {
                            if (ReferenceStyle == ReferenceStyle.Sheet && Item != null)
                            {
                                Item.ColumnName = r.Value;
                            }
                        }
                        if (flag10)
                        {
                            if (ReferenceStyle == ReferenceStyle.Sheet && Item != null)
                            {
                                Item.ReferenceTableName = r.Value;
                            }
                        }
                        if (flag11)
                        {
                            if (ReferenceStyle == ReferenceStyle.Sheet && Item != null)
                            {
                                Item.ReferenceTableText = r.Value;
                            }
                        }
                        if (flag12)
                        {
                            if (ReferenceStyle == ReferenceStyle.Sheet && Item != null)
                            {
                                Item.ReferenceColumnName = r.Value;
                            }
                        }
                        if (flag13)
                        {
                            if (ReferenceStyle == ReferenceStyle.Sheet && Item != null)
                            {
                                Item.ReferenceColumnText = r.Value;
                            }
                        }
                        if (flag14)
                        {
                            if (ReferenceStyle == ReferenceStyle.Sheet)
                            {
                                sheetReference.DataFilter = BinarySerializer.Deserialize(r.Value) as DataFilterCondition;
                            }
                        }
                        goto Label_0060;
                    case XmlNodeType.EndElement:
                        if (flag1 && r.Name.Equals("DropDownStyle"))
                        {
                            flag1 = false;
                            goto Label_0060;
                        }
                        if (flag2 && r.Name.Equals("ReferenceStyle"))
                        {
                            flag2 = false;
                            goto Label_0060;
                        }
                        if (flag3 && r.Name.Equals("DesignMode"))
                        {
                            flag3 = false;
                            goto Label_0060;
                        }
                        if (flag4 && r.Name.Equals("TableName"))
                        {
                            flag4 = false;
                            goto Label_0060;
                        }
                        if (flag5 && r.Name.Equals("ColumnName"))
                        {
                            flag5 = false;
                            goto Label_0060;
                        }
                        if (flag6 && r.Name.Equals("ReferenceXml"))
                        {
                            flag6 = false;
                            goto Label_0060;
                        }
                        if (flag7 && r.Name.Equals("DictionaryIndex"))
                        {
                            flag7 = false;
                            goto Label_0060;
                        }
                        if (r.Name.Equals("ReferenceItem"))
                        {
                            sheetReference.ReferenceItems.Add(Item);
                            Item = null;
                            goto Label_0060;
                        }
                        if (flag8 && r.Name.Equals("Item_TableName"))
                        {
                            flag8 = false;
                            goto Label_0060;
                        }
                        if (flag9 && r.Name.Equals("Item_ColumnName"))
                        {
                            flag9 = false;
                            goto Label_0060;
                        }
                        if (flag10 && r.Name.Equals("Item_ReferenceTableName"))
                        {
                            flag10 = false;
                            goto Label_0060;
                        }
                        if (flag11 && r.Name.Equals("Item_ReferenceTableText"))
                        {
                            flag11 = false;
                            goto Label_0060;
                        }
                        if (flag12 && r.Name.Equals("Item_ReferenceColumnName"))
                        {
                            flag12 = false;
                            goto Label_0060;
                        }
                        if (flag13 && r.Name.Equals("Item_ReferenceColumnText"))
                        {
                            flag13 = false;
                            goto Label_0060;
                        }
                        if (flag14 && r.Name.Equals("DataFilter"))
                        {
                            flag14 = false;
                            goto Label_0060;
                        }
                        goto Label_0060;
                }
                goto Label_0060;
            }
            catch
            {
                return true;
            }
        }

        #endregion 序列化

        public override object Clone()
        {
            DownListCellType cellType = new DownListCellType();
            cellType._DesignMode = this._DesignMode;
            cellType._Style = this._Style;

            if (this.ditionaryReference != null)
            {
                cellType.ditionaryReference.ColumnName = this.ditionaryReference.ColumnName;
                cellType.ditionaryReference.DictionaryIndex = this.ditionaryReference.DictionaryIndex;
                cellType.ditionaryReference.ReferenceXml = this.ditionaryReference.ReferenceXml;
                cellType.ditionaryReference.TableName = this.ditionaryReference.TableName;
            }

            if (this.sheetReference != null)
            {
                cellType.sheetReference.TableName = this.sheetReference.TableName;
                cellType.sheetReference.ColumnName = this.sheetReference.ColumnName;
                cellType.sheetReference.ReferenceXml = this.sheetReference.ReferenceXml;
                foreach (ReferenceItem Item in cellType.sheetReference.ReferenceItems)
                {
                    ReferenceItem item = new ReferenceItem();
                    item.TableName = Item.TableName;
                    item.ColumnName = Item.ColumnName;
                    item.ReferenceTableName = Item.ReferenceTableName;
                    item.ReferenceColumnName = Item.ReferenceColumnName;
                    cellType.sheetReference.ReferenceItems.Add(item);
                }
            }

            return cellType;
        }

        #region IDisposable 成员

        void IDisposable.Dispose()
        {
            if (TreeView != null)
            {
                TreeView.Dispose();
            }
            TreeView = null;
        }

        #endregion
    }

    #region 下拉框编辑器类型

    /// <summary>
    /// 树形下拉框
    /// </summary>
    internal class TreeViewList : TreeView, ISubEditor, IReferenceData
    {
        private Boolean Loaded;
        private DictionaryReference Reference;

        public event EventHandler CloseUp;
        public event EventHandler ValueChanged;

        internal Size size;

        internal class MouseMessageFilter : IMessageFilter
        {
            public TreeViewList list;

            public MouseMessageFilter(TreeViewList list)
            {
                this.list = list;
            }
            public bool PreFilterMessage(ref System.Windows.Forms.Message m)
            {
                // Blocks all the messages relating to the left mouse button.
                if (m.Msg == 32)
                {
                    if (m.WParam.ToInt32() == 531)
                        return true;
                    else
                        return false;
                }

                return false;
            }
        }

        internal MouseMessageFilter filter;
        public TreeViewList(DictionaryReference Reference)
        {
            filter = new MouseMessageFilter(this);
            Application.AddMessageFilter(filter);
            this.DoubleClick += new EventHandler(TreeViewList_DoubleClick);
            this.KeyDown += new KeyEventHandler(TreeViewList_KeyDown);
            this.FullRowSelect = true;

            this.Reference = Reference;
        }

        void TreeViewList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && this.SelectedNode != null)
            {
                if (ValueChanged != null)
                    ValueChanged(this, EventArgs.Empty);
                if (CloseUp != null)
                    CloseUp(this, EventArgs.Empty);
            }
        }

        void TreeViewList_DoubleClick(object sender, EventArgs e)
        {
            if (this.SelectedNode != null)
            {
                if (ValueChanged != null)
                    ValueChanged(this, EventArgs.Empty);
                if (CloseUp != null)
                    CloseUp(this, EventArgs.Empty);
            }
        }

        public Point GetLocation(Rectangle rect)
        {
            return Point.Empty;
        }

        public Size GetPreferredSize()
        {
            return new Size(size.Width, 150);
        }

        public Control GetSubEditorControl()
        {
            return this;
        }

        public object GetValue()
        {
            return this.SelectedNode.Text;
        }

        public void SetValue(object value)
        {
            if (value == null)
                value = "";

            TreeNode Node = FindNode(value.ToString());
            this.SelectedNode = Node;
        }

        public void InitializeData()
        {
            if (!Loaded)
            {
                Loaded = true;

                this.Nodes.Clear();

                //String TableName = sys_cache_tables.sys_dictionary.ToString();
                //Cache.TableDictionary.Tables[TableName].DefaultView.Sort = "CodeClass,Code";
                //Cache.TableDictionary.Tables[TableName].DefaultView.RowFilter = "CodeClass ='" + Reference.DictionaryIndex + "' And Code is not null";
                //foreach (DataRowView row in Cache.TableDictionary.Tables[TableName].DefaultView)

                DataTable Data = DictionaryManager.GetDictionaryByCodeClass(Reference.DictionaryIndex);
                //Data.DefaultView.RowFilter = "CodeClass ='" + Reference.DictionaryIndex + "' and Code is not null";
                foreach (DataRow row in Data.Rows)
                {
                    String ID = row["ID"].ToString();
                    String Code = row["Code"].ToString();
                    String Description = row["DESCRIPTION"].ToString();

                    TreeNode Node = new TreeNode();
                    Node.Text = Description;
                    Node.Name = Code;
                    Node.Tag = ID;

                    Nodes.Add(Node);
                }

                this.ExpandAll();
            }
        }

        public TreeNode FindNode(string Text)
        {
            foreach (TreeNode Node in Nodes)
            {
                if (Node.Text == Text)
                {
                    return Node;
                }
            }

            return null;
        }

        #region IReferenceData 成员

        Dictionary<string, string> RefData = new Dictionary<string, string>();
        public Dictionary<string, string> Data
        {
            get
            {
                RefData.Clear();
                return RefData;
            }
        }

        #endregion
    }

    internal class MultiColumnList : FpSpread, ISubEditor, IReferenceData
    {
        private SheetReference Reference;

        private IBorder Border = new DoubleLineBorder(Color.Black, false, false, false, true);
        private Dictionary<int, string> FilterValues = new Dictionary<int, string>();

        private const String CHOOSE = "选择";
        private const String ALL = "全部";

        public event EventHandler ValueChanged;
        public event EventHandler CloseUp;

        internal Size size;

        internal class MouseMessageFilter : IMessageFilter
        {
            public MultiColumnList list;

            public MouseMessageFilter(MultiColumnList list)
            {
                this.list = list;
            }
            public bool PreFilterMessage(ref System.Windows.Forms.Message m)
            {
                // Blocks all the messages relating to the left mouse button.
                if (m.Msg == 32)
                {
                    if (m.WParam.ToInt32() == 531)
                        return true;
                    else
                        return false;
                }

                return false;
            }
        }

        internal MouseMessageFilter filter;
        public MultiColumnList(SheetReference Reference)
        {
            filter = new MouseMessageFilter(this);
            Application.AddMessageFilter(filter);
            this.HorizontalScrollBarPolicy = ScrollBarPolicy.Never;
            this.RowSplitBoxPolicy = SplitBoxPolicy.Never;
            this.ColumnSplitBoxPolicy = SplitBoxPolicy.Never;

            SheetView s = new SheetView();
            s.RowHeaderVisible = false;
            s.OperationMode = OperationMode.RowMode;

            s.RowHeader.Columns[0].Width = 60;
            s.SetColumnWidth(0, Convert.ToInt32(s.GetPreferredColumnWidth(0)));
            s.SetColumnWidth(1, Convert.ToInt32(s.GetPreferredColumnWidth(1)));
            s.SetColumnWidth(2, Convert.ToInt32(s.GetPreferredColumnWidth(2)));
            this.Sheets.Add(s);
            this.CellDoubleClick += new CellClickEventHandler(this.CellDoubleClicked);
            this.KeyDown += new KeyEventHandler(this.SpreadKeyDown);
            this.ComboCloseUp += new EditorNotifyEventHandler(MultiColumnList_ComboCloseUp);

            this.Reference = Reference;
        }

        void MultiColumnList_ComboCloseUp(object sender, EditorNotifyEventArgs e)
        {
            FilterValues.Clear();
            foreach (Column Column in ActiveSheet.Columns)
            {
                if (ActiveSheet.Cells[0, Column.Index].Text != ALL)
                {
                    FilterValues.Add(Column.Index, ActiveSheet.Cells[0, Column.Index].Text);
                }
            }

            for (int i = 1; i < ActiveSheet.Rows.Count; i++)
            {
                Boolean Visible = true;
                foreach (int Index in FilterValues.Keys)
                {
                    Visible = Visible & (ActiveSheet.Cells[i, Index].Text == FilterValues[Index]);
                }

                ActiveSheet.Rows[i].Visible = Visible;
            }
        }

        private void CellDoubleClicked(object sender, CellClickEventArgs e)
        {
            if (e.Row > 0)
            {
                if (ValueChanged != null)
                    ValueChanged(this, EventArgs.Empty);
                if (CloseUp != null)
                    CloseUp(this, EventArgs.Empty);
            }
        }

        private void SpreadKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && ActiveSheet.ActiveRowIndex > 0)
            {
                if (ValueChanged != null)
                    ValueChanged(this, EventArgs.Empty);
                if (CloseUp != null)
                    CloseUp(this, EventArgs.Empty);
            }
        }

        public Point GetLocation(Rectangle rect)
        {
            return Point.Empty;
        }

        public Control GetSubEditorControl()
        {
            return this;
        }

        public object GetValue()
        {
            int ColumnIndex = -1;
            if (Reference.RootReferenceItem == null)
                return "";

            ReferenceItem RootReferenceItem = Reference.RootReferenceItem;
            foreach (Column column in ActiveSheet.Columns)
            {
                if (String.Concat("COL_NORM", column.Label) == RootReferenceItem.ReferenceColumnName)
                {
                    ColumnIndex = column.Index;
                    break;
                }
            }

            return ActiveSheet.GetValue(ActiveSheet.ActiveRowIndex, ColumnIndex);
        }

        public void SetValue(object value)
        {
            if (value == null)
                return;

            int ColumnIndex = -1;
            if (Reference.RootReferenceItem == null)
                return;

            ReferenceItem RootReferenceItem = Reference.RootReferenceItem;
            foreach (Column column in ActiveSheet.Columns)
            {
                if (String.Concat("COL_NORM", column.Label) == RootReferenceItem.ReferenceColumnName)
                {
                    ColumnIndex = column.Index;
                    break;
                }
            }

            if (ColumnIndex != -1)
            {
                for (int i = 1; i < ActiveSheet.Rows.Count; i++)
                {
                    if (ActiveSheet.Cells[i, ColumnIndex].Text == value.ToString())
                    {
                        ActiveSheet.SetActiveCell(i, ColumnIndex);
                        break;
                    }
                }
            }
        }

        public Size GetPreferredSize()
        {
            return new Size(500, 250);
        }

        #region IReferenceData 成员

        Dictionary<string, string> RefData = new Dictionary<string, string>();
        public Dictionary<string, string> Data
        {
            get
            {
                RefData.Clear();
                int RowIndex = ActiveSheet.ActiveRowIndex;
                foreach (Column Column in ActiveSheet.Columns)
                {
                    RefData.Add(String.Concat("COL_NORM", Column.Label), ActiveSheet.Cells[RowIndex, Column.Index].Text);
                }

                return RefData;
            }
        }

        #endregion

        public void InitializeData()
        {
            String referenceTable = "";
            List<string> columns = new List<string>();
            Dictionary<string, string> columnTexts = new Dictionary<string, string>();
            foreach (ReferenceItem Item in Reference.ReferenceItems)
            {
                referenceTable = Item.ReferenceTableName;
                columns.Add(string.Format("[{0}]", Item.ReferenceColumnName));
                columnTexts.Add(Item.ReferenceColumnName, Item.ReferenceColumnText);
            }

            StringBuilder sql_Select = new StringBuilder();
            sql_Select.Append("select ID,");
            sql_Select.Append(string.Join(",", columns.ToArray()));
            sql_Select.Append(" from [");
            sql_Select.Append(referenceTable);
            sql_Select.Append("]");

            if (Reference.DataFilter != null && Reference.DataFilter.IsValidExpression())
            {
                sql_Select.Append(" where not ");
                sql_Select.Append(Reference.DataFilter.GetExpression());
            }

            DataTable Data = Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { sql_Select.ToString() }) as DataTable;
            BindSheetView(ActiveSheet, Data, columnTexts);
            AutoColumnsWidth(ActiveSheet, AutoSizeFlags.Contents, 150);
        }

        /// <summary>
        /// 在sheetView里显示数据
        /// </summary>
        /// <param name="DataSource">数据源</param>
        private void BindSheetView(SheetView SheetView, object DataSource, Dictionary<string, string> ColumnTexts)
        {
            SheetView.Rows.Count = 0;
            SheetView.Columns.Count = 0;

            if (DataSource is DataTable)
            {
                DataTable dt = DataSource as DataTable;

                SheetView.Rows.Count = dt.Rows.Count + 1;
                SheetView.RowHeader.AutoText = HeaderAutoText.Blank;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        ActiveSheet.RowHeader.Rows[i].Label = CHOOSE;
                    }
                    else
                    {
                        ActiveSheet.RowHeader.Rows[i].Label = (i - 1).ToString();
                    }
                }

                SheetView.Columns.Count = dt.Columns.Count;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName.ToLower() == "id" ||
                        dt.Columns[i].ColumnName.ToLower() == "scpt" ||
                        dt.Columns[i].ColumnName.ToLower() == "scts")
                    {
                        SheetView.ColumnHeader.Columns[i].Visible = false;
                        SheetView.ColumnHeader.Columns[i].Label = dt.Columns[i].ColumnName;
                    }
                    else
                    {
                        SheetView.ColumnHeader.Columns[i].Label = ColumnTexts[dt.Columns[i].ColumnName];
                    }

                    SheetView.Cells[0, i].CellType = new ComboBoxCellType();
                    SheetView.Cells[0, i].Border = Border;
                }

                List<string> Items;
                Dictionary<int, List<string>> ComboBoxes = new Dictionary<int, List<string>>();
                if (dt.Rows.Count >= dt.Columns.Count)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (ComboBoxes.ContainsKey(j))
                        {
                            Items = ComboBoxes[j];
                        }
                        else
                        {
                            Items = new List<string>();
                            ComboBoxes.Add(j, Items);
                        }

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            SheetView.Cells[i + 1, j].Value = dt.DefaultView[i][j];
                            if (!Items.Contains(SheetView.Cells[i + 1, j].Value.ToString()))
                            {
                                Items.Add(SheetView.Cells[i + 1, j].Value.ToString());
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            SheetView.Cells[i + 1, j].Value = dt.DefaultView[i][j];

                            if (ComboBoxes.ContainsKey(j))
                            {
                                Items = ComboBoxes[j];
                            }
                            else
                            {
                                Items = new List<string>();
                                ComboBoxes.Add(j, Items);
                            }

                            if (!Items.Contains(SheetView.Cells[i + 1, j].Value.ToString()))
                            {
                                Items.Add(SheetView.Cells[i + 1, j].Value.ToString());
                            }
                        }
                    }
                }

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (ComboBoxes.ContainsKey(i))
                    {
                        ComboBoxCellType ComboBox = SheetView.Cells[0, i].CellType as ComboBoxCellType;
                        ComboBoxes[i].Insert(0, ALL);
                        ComboBox.Items = ComboBoxes[i];
                    }

                    ActiveSheet.Cells[0, i].Text = ALL;
                }
            }
        }

        public void AutoColumnsWidth(SheetView Sheet, AutoSizeFlags flags, int Minimum)
        {
            if (Sheet.ColumnHeader.Columns.Count == 0)
                return;

            AutoColumnsWidth(Sheet, 0, Sheet.ColumnHeader.Columns.Count - 1, flags, Minimum);
        }

        public void AutoColumnsWidth(SheetView Sheet, int startcolumn, int endcolumn, AutoSizeFlags flags, int Minimum)
        {
            int start = startcolumn < 0 ? 0 : startcolumn;
            int end = endcolumn > Sheet.ColumnHeader.Columns.Count ? Sheet.ColumnHeader.Columns.Count - 1 : endcolumn;

            int temp;
            if (start > end)
            {
                temp = start;
                end = temp;
                start = end;
            }

            if (flags == AutoSizeFlags.Header)
            {
                for (int i = start; i <= end; i++)
                {
                    SizeF sizef = GetTextRect(Sheet.ColumnHeader.Columns[i].Label);
                    float WideWidth = (sizef.Width < Minimum ? Minimum : sizef.Width);
                    Sheet.ColumnHeader.Columns[i].Width = WideWidth / Sheet.ColumnHeader.Columns[i].Label.Length * (Sheet.ColumnHeader.Columns[i].Label.Length + 2);
                }
            }
            else
            {
                for (int i = start; i <= end; i++)
                {
                    float Width = 0;
                    for (int j = 0; j < Sheet.Rows.Count; j++)
                    {
                        SizeF TextSize = GetTextRect(Sheet.Cells[j, i].Text);

                        if (TextSize.Width > Width)
                        {
                            Width = TextSize.Width;
                        }
                    }

                    float WideWidth = (Width < Minimum ? Minimum : Width);
                    Sheet.ColumnHeader.Columns[i].Width = WideWidth;
                }
            }
        }

        public SizeF GetTextRect(string Text)
        {
            Graphics g = this.CreateGraphics();
            return g.MeasureString(Text, this.Font);
        }

    }

    #endregion 下拉框编辑器类型

    /// <summary>
    /// 参照事件对象
    /// </summary>
    public class ReferenceEventArgs : EventArgs
    {
        public ReferenceEventArgs(ReferenceInfoBase ReferenceInfo, Dictionary<string, string> ReferenceData)
        {
            _RefInfo = ReferenceInfo;

            _RefData.Clear();
            foreach (String Key in ReferenceData.Keys)
            {
                _RefData.Add(Key, ReferenceData[Key]);
            }
        }

        ReferenceInfoBase _RefInfo = new ReferenceInfoBase();
        public ReferenceInfoBase ReferenceInfo
        {
            get
            {
                return _RefInfo;
            }
        }

        Dictionary<string, string> _RefData = new Dictionary<string, string>();
        public Dictionary<string, string> ReferenceData
        {
            get
            {
                return _RefData;
            }
        }
    }

    /// <summary>
    /// 图表单元格类型，支持显示图表
    /// </summary>
    [Serializable]
    public class ChartCellType : FarPoint.Win.Spread.CellType.EditBaseCellType
    {
        [NonSerialized]
        ChartControl2 _ChartControl;
        [NonSerialized]
        SheetView _SheetView;

        String _ChartData = "";

        public ChartCellType()
        {
            Static = true;

            _ChartControl = new ChartControl2(ActiveSheet);
            _ChartData = _ChartControl.GetSerializeXML();
        }

        private String ChartData
        {
            get
            {
                return _ChartData;
            }
            set
            {
                _ChartData = value;
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

        public Size ChartSize
        {
            get
            {
                return ChartControl.Size;
            }
            set
            {
                ChartControl.Size = value;
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
            ChartControl.Deserialize(ChartData);
            ChartControl.UpdateChartImage();
        }

        public void ShowChartEditor()
        {
            ChartControl.Deserialize(_ChartData);

            ChartEditor2 ChartEditor = new ChartEditor2(ChartControl);
            if (DialogResult.OK == ChartEditor.ShowDialog())
            {
                _ChartData = ChartControl.GetSerializeXML();
                ChartControl.UpdateChartImage();
            }
        }

        public override void PaintCell(Graphics g, Rectangle r, FarPoint.Win.Spread.Appearance appearance, object value, bool isSelected, bool isLocked, float zoomFactor)
        {
            Image image = ChartControl.ChartImage;
            Rectangle rect = r;
            if (image == null)
            {
                image = IcoResource.图表类型;

                Size imagesize = IcoResource.图表类型.Size;
                rect.X = r.Left + (r.Width - imagesize.Width) / 2;
                rect.Y = r.Top + (r.Height - imagesize.Height) / 2;
                rect.Size = IcoResource.图表类型.Size;
            }

            g.Clear(appearance.BackColor);
            g.DrawImage(image, rect);
        }

        #region 序列化

        public override bool Serialize(System.Xml.XmlTextWriter w)
        {
            bool Result = base.Serialize(w);

            w.WriteElementString("ChartData", this.ChartData.ToString(CultureInfo.InvariantCulture));

            return Result;
        }

        public override bool Deserialize(System.Xml.XmlNodeReader r)
        {
            Boolean Result = base.Deserialize(r);

            Boolean flag = false;
            Boolean flag2 = false;

            if (r.IsEmptyElement)
            {
                return Result;
            }

        Label_0060:
            if (flag)
            {
                if (!r.Read())
                {
                    return Result;
                }
            }
            else
            {
                flag = Result;
            }

            switch (r.NodeType)
            {
                case XmlNodeType.Element:
                    if (r.Name.Equals("ChartData"))
                    {
                        flag2 = true;
                    }
                    goto Label_0060;
                case XmlNodeType.Text:
                    if (flag2)
                    {
                        this.ChartData = r.Value;
                    }
                    goto Label_0060;
                case XmlNodeType.EndElement:
                    if (flag2 && r.Name.Equals("ChartData"))
                    {
                        flag2 = false;
                        goto Label_0060;
                    }
                    goto Label_0060;
            }
            goto Label_0060;
        }

        #endregion 序列化
    }

    /// <summary>
    /// 掩码单元格类型
    /// </summary>
    [Serializable]
    public class MaskCellType : FarPoint.Win.Spread.CellType.MaskCellType, IConvertable, IGetFieldType
    {
        public MaskCellType()
        {
            this.CustomMaskCharacters = new string[1];
            this.CustomMaskCharacters[0] = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-()（）复检";
            this.Mask = "00000000000000000000000000000000000";
        }

        public FieldType FieldType
        {
            get
            {
                return FieldType.Mask;
            }
        }

        public override string Format(object obj)
        {
            String s = base.Format(obj);
            return s.Trim(this.MaskChar);
        }

        public override object GetEditorValue()
        {
            object s = base.GetEditorValue();
            if (s == null) s = "";
            return s.ToString().Trim(this.MaskChar);
        }

        public override string ToString()
        {
            return FieldType.Description;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IConvertable 成员

        public bool ConvertTo(FieldType FieldType)
        {
            return false;
        }

        public bool EqualsBasicDataType(FieldType FieldType)
        {
            return false;
        }

        #endregion

        public override object Clone()
        {
            MaskCellType cellType = new MaskCellType();
            return cellType;
        }

        protected new virtual FarPoint.Win.HorizontalAlignment ToHorizontalAlignment(CellHorizontalAlignment alignment)
        {
            switch (alignment)
            {
                case CellHorizontalAlignment.Left: return FarPoint.Win.HorizontalAlignment.Left;
                case CellHorizontalAlignment.Center: return FarPoint.Win.HorizontalAlignment.Center;
                case CellHorizontalAlignment.Right: return FarPoint.Win.HorizontalAlignment.Right;
                default: return FarPoint.Win.HorizontalAlignment.Left;
            }
        }

        protected new virtual FarPoint.Win.VerticalAlignment ToVerticalAlignment(CellVerticalAlignment alignment)
        {
            switch (alignment)
            {
                case CellVerticalAlignment.Top: return FarPoint.Win.VerticalAlignment.Top;
                case CellVerticalAlignment.Center: return FarPoint.Win.VerticalAlignment.Center;
                case CellVerticalAlignment.Bottom: return FarPoint.Win.VerticalAlignment.Bottom;
                default: return FarPoint.Win.VerticalAlignment.Top;
            }
        }

        protected virtual void GetTextRectangle(Graphics g, Rectangle r, ref Font f, FarPoint.Win.Spread.Appearance appearance, ref RectangleF rText, string paintString)
        {
            do
            {
                String Text = paintString.TrimStart('\r', '\n');
                SizeF sizef = g.MeasureString(Text, f);
                int LineNumber = 0, i = 1, start = 0;
                while (i < Text.Length)
                {
                    SizeF stringsize = g.MeasureString(Text.Substring(start, i - start + 1), f);
                    if (stringsize.Width > r.Width)
                    {
                        start = i;
                        LineNumber++;
                    }

                    i++;
                }

                if (start < Text.Length)
                    LineNumber++;

                SizeF size = new SizeF(r.Width, sizef.Height * LineNumber);
                FarPoint.Win.HorizontalAlignment alignment = this.ToHorizontalAlignment(appearance.HorizontalAlignment);
                FarPoint.Win.VerticalAlignment alignment2 = this.ToVerticalAlignment(appearance.VerticalAlignment);
                rText = new RectangleF(r.X, r.Y, size.Width, size.Height);
                if (alignment == FarPoint.Win.HorizontalAlignment.Center)
                {
                    rText.X += Convert.ToSingle((r.Width / 2.0) - (size.Width / 2.0));
                }
                else if (alignment == FarPoint.Win.HorizontalAlignment.Right)
                {
                    rText.X += r.Width - size.Width;
                }
                if (alignment2 == FarPoint.Win.VerticalAlignment.Center)
                {
                    rText.Y += Convert.ToSingle((r.Height / 2.0) - (size.Height / 2.0));
                }
                else if (alignment2 == FarPoint.Win.VerticalAlignment.Bottom)
                {
                    rText.Y += r.Height - size.Height;
                }

                if (rText.Height > r.Height)
                {
                    float fs = f.Size - 1f;
                    if (fs < 1)
                    {
                        fs = 1;
                        break;
                    }
                    f = new Font(f.FontFamily, fs, f.Style);
                }
                else
                {
                    break;
                }

            } while (true);
        }

        public override void PaintCell(Graphics g, Rectangle r, FarPoint.Win.Spread.Appearance appearance, object value, bool isSelected, bool isLocked, float zoomFactor)
        {
            string s = null;
            StringFormat sf = new StringFormat();
            sf.FormatFlags |= StringFormatFlags.NoClip;

            FarPoint.Win.HorizontalAlignment HAlignment = this.ToHorizontalAlignment(appearance.HorizontalAlignment);
            switch (HAlignment)
            {
                case FarPoint.Win.HorizontalAlignment.Left:
                    sf.Alignment = StringAlignment.Near;
                    break;
                case FarPoint.Win.HorizontalAlignment.Right:
                    sf.Alignment = StringAlignment.Far;
                    break;
                default:
                    sf.Alignment = StringAlignment.Center;
                    break;
            }

            FarPoint.Win.VerticalAlignment VAlignment = this.ToVerticalAlignment(appearance.VerticalAlignment);
            switch (VAlignment)
            {
                case FarPoint.Win.VerticalAlignment.Top:
                    sf.LineAlignment = StringAlignment.Near;
                    break;
                case FarPoint.Win.VerticalAlignment.Bottom:
                    sf.LineAlignment = StringAlignment.Far;
                    break;
                default:
                    sf.LineAlignment = StringAlignment.Center;
                    break;
            }

            RectangleF rText = new RectangleF(r.X, r.Y, r.Width, r.Height);

            Color backColor = appearance.BackColor;
            Color foreColor = appearance.ForeColor;
            if (isSelected)
            {
                if (!appearance.SelectionBackColor.IsEmpty)
                    backColor = appearance.SelectionBackColor;
                if (!appearance.SelectionForeColor.IsEmpty)
                    foreColor = appearance.SelectionForeColor;
            }
            if (isLocked)
            {
                if (!appearance.LockBackColor.IsEmpty)
                    backColor = appearance.LockBackColor;
                if (!appearance.LockForeColor.IsEmpty)
                    foreColor = appearance.LockForeColor;
            }

            SolidBrush backBrush, foreBrush;
            GraphicsState state = g.Save();

            g.IntersectClip(r);

            if (value != null)
                s = this.Format(value);

            sf.Trimming = StringTrim;
            if (appearance.Font == null)
                appearance.Font = new Font("System", 10);

            backBrush = new SolidBrush(backColor);
            g.FillRectangle(backBrush, r);
            backBrush.Dispose();

            if (s != null && s.Length > 0)
            {
                foreBrush = new SolidBrush(foreColor);

                Font f = (Font)appearance.Font;
                if (zoomFactor != 1.0f)
                {
                    float fs = f.Size * zoomFactor;
                    f = new Font(f.FontFamily, fs, f.Style);
                }

                //GetTextRectangle(g, r, ref f, appearance, ref rText, s);
                //Modified by Tan In 20140829 保证掩码和文本同样的显示效果
                //rText = r;
                while (g.MeasureString(s.ToString(), f, new SizeF(r.Width, r.Height)).Height + 5 >= r.Height && f.Size >= 5)
                {
                    f = new Font(f.FontFamily, f.Size - 0.2f, f.Style);
                }


                switch (TextOrientation)
                {
                    case TextOrientation.TextHorizontal:
                        g.DrawString(s, f, foreBrush, rText, sf);
                        break;

                    case TextOrientation.TextHorizontalFlipped:
                        g.TranslateTransform(rText.Width, rText.Height / 2);
                        g.RotateTransform(180);
                        g.DrawString(s, f, foreBrush, new RectangleF(-rText.X, -(rText.Y + 1 + (rText.Height / 2)), rText.Width, rText.Height), sf);
                        break;

                    case TextOrientation.TextVertical:
                        g.TranslateTransform(rText.Width / 2, 0);
                        g.RotateTransform(90);
                        g.DrawString(s, f, foreBrush, new RectangleF(rText.Y, -rText.X - (rText.Width / 2), r.Height, rText.Width), sf);
                        break;

                    case TextOrientation.TextVerticalFlipped:
                        g.TranslateTransform(rText.Width / 2, rText.Height);
                        g.RotateTransform(-90);
                        g.DrawString(s, f, foreBrush, new RectangleF(-rText.Y, rText.X - (r.Width / 2), r.Height, r.Width), sf);
                        break;

                    case TextOrientation.TextTopDown:
                        for (int i = 0; i < s.Length; i++)
                        {
                            SizeF textSize = g.MeasureString(s[i].ToString(CultureInfo.InvariantCulture), f);
                            sf.Alignment = StringAlignment.Center;
                            sf.LineAlignment = StringAlignment.Near;
                            g.DrawString(s[i].ToString(CultureInfo.InvariantCulture), f, foreBrush, rText, sf);
                            rText.Y += (int)Math.Ceiling(textSize.Height);
                        }
                        break;
                }

                g.ResetTransform();

                foreBrush.Dispose();
                if (zoomFactor != 1.0f) f.Dispose();
            }
            g.Restore(state);
        }
    }

    /// <summary>
    /// 组合框单元格类型
    /// </summary>
    [Serializable]
    public class ComboBoxCellType : FarPoint.Win.Spread.CellType.EditBaseCellType
    {
        List<string> sArray = new List<string>();
        [NonSerialized]
        ComboBoxEditor ItemEditor = new ComboBoxEditor();

        public ComboBoxCellType()
        {
        }

        public override Control GetEditorControl(FarPoint.Win.Spread.Appearance appearance, float zoomFactor)
        {
            Control c = base.GetEditorControl(appearance, zoomFactor);
            ((GeneralEditor)c).ButtonStyle = ButtonStyle.DropDown;
            ((GeneralEditor)c).ControlType = ControlType.ButtonEdit;

            ItemEditor.InitializeItems(sArray.ToArray());

            return c;
        }

        public List<string> Items
        {
            get
            {
                return sArray;
            }
            set
            {
                sArray = value;
            }
        }

        Boolean _DropDownButton = true;
        public new Boolean DropDownButton
        {
            get
            {
                return _DropDownButton;
            }
            set
            {
                _DropDownButton = value;
                SubEditor = ItemEditor;
            }
        }

        public override object IsReservedLocation(Graphics g, int x, int y, Rectangle rc, FarPoint.Win.Spread.Appearance appearance, object value, float zoomFactor)
        {
            ItemEditor.size = rc.Size;

            Rectangle rectButton = new Rectangle(rc.X + rc.Width - SystemInformation.VerticalScrollBarWidth, rc.Y, SystemInformation.VerticalScrollBarWidth, rc.Height);
            if (rectButton.Contains(new Point(x, y)))
                return this;
            else
                return null;
        }

        public override void PaintCell(Graphics g, Rectangle r, FarPoint.Win.Spread.Appearance appearance, object value, bool isSelected, bool isLocked, float zoomFactor)
        {
            Rectangle newR = new Rectangle(r.X, r.Y, r.Width - SystemInformation.VerticalScrollBarWidth, r.Height);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("FarPoint.Win.Images", System.Reflection.Assembly.GetAssembly(typeof(FarPoint.Win.IElement)));

            FarPoint.Win.ButtonElement b = new FarPoint.Win.ButtonElement();
            b.Visible = DropDownButton;
            b.Border = new BevelBorder(BevelBorderType.Raised, SystemColors.ControlLightLight, SystemColors.ControlDark, 1);
            if (rm != null)
            {
                b.ForegroundImage = new Picture((System.Drawing.Image)rm.GetObject("dropDown"), RenderStyle.StretchAndScale, Color.Magenta, FarPoint.Win.HorizontalAlignment.Center, FarPoint.Win.VerticalAlignment.Center);
            }
            Rectangle buttonRect = new Rectangle(r.X + r.Width - SystemInformation.VerticalScrollBarWidth, r.Y, SystemInformation.VerticalScrollBarWidth, r.Height);

            base.PaintCell(g, DropDownButton ? newR : r, appearance, value, isSelected, isLocked, zoomFactor);
            if (DropDownButton)
            {
                b.OnPaintBackground(g, buttonRect);
                b.OnPaint(g, buttonRect);
            }
        }

        public override object Clone()
        {
            ComboBoxCellType CellType = new ComboBoxCellType();
            CellType.DropDownButton = this.DropDownButton;
            CellType.Items = this.Items;
            CellType.SubEditor = ItemEditor;
            return CellType;
        }
    }

    internal class ComboBoxEditor : TreeView, ISubEditor
    {
        public event EventHandler CloseUp;
        public event EventHandler ValueChanged;

        internal Size size;

        internal class MouseMessageFilter : IMessageFilter
        {
            public ComboBoxEditor list;

            public MouseMessageFilter(ComboBoxEditor list)
            {
                this.list = list;
            }
            public bool PreFilterMessage(ref System.Windows.Forms.Message m)
            {
                // Blocks all the messages relating to the left mouse button.
                if (m.Msg == 32)
                {
                    if (m.WParam.ToInt32() == 531)
                        return true;
                    else
                        return false;
                }

                return false;
            }
        }

        internal MouseMessageFilter filter;
        public ComboBoxEditor()
        {
            filter = new MouseMessageFilter(this);
            Application.AddMessageFilter(filter);
            this.DoubleClick += new EventHandler(TreeViewList_DoubleClick);
            this.KeyDown += new KeyEventHandler(TreeViewList_KeyDown);
            this.FullRowSelect = true;
            this.ShowRootLines = false;
        }

        void TreeViewList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && this.SelectedNode != null)
            {
                if (ValueChanged != null)
                    ValueChanged(this, EventArgs.Empty);
                if (CloseUp != null)
                    CloseUp(this, EventArgs.Empty);
            }
        }

        void TreeViewList_DoubleClick(object sender, EventArgs e)
        {
            if (this.SelectedNode != null)
            {
                if (ValueChanged != null)
                    ValueChanged(this, EventArgs.Empty);
                if (CloseUp != null)
                    CloseUp(this, EventArgs.Empty);
            }
        }

        public Point GetLocation(Rectangle rect)
        {
            return Point.Empty;
        }

        public Size GetPreferredSize()
        {
            return new Size(size.Width, 50);
        }

        public Control GetSubEditorControl()
        {
            return this;
        }

        public object GetValue()
        {
            return this.SelectedNode.Text;
        }

        public void SetValue(object value)
        {
            if (value == null)
                value = "";

            TreeNode Node = FindNode(value.ToString());
            this.SelectedNode = Node;
        }

        public void InitializeItems(String[] Array)
        {
            this.Nodes.Clear();
            foreach (String str in Array)
                this.Nodes.Add(str);
            this.ExpandAll();
        }

        public TreeNode FindNode(string Text)
        {
            foreach (TreeNode Node in Nodes)
            {
                if (Node.Text == Text)
                {
                    return Node;
                }
            }

            return null;
        }
    }
}
