using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FarPoint.Win.Spread;
using FarPoint.Win;
using ReportCommon;

namespace ReportCommon
{
    [Serializable]
    public class Style : ICloneable
    {
        public readonly static Style Empty = new Style();

        Color _ForeColor;
        public Color ForeColor
        {
            get
            {
                return _ForeColor;
            }
            set
            {
                _ForeColor = value;
            }
        }

        Color _BackColor;
        public Color BackColor
        {
            get
            {
                return _BackColor;
            }
            set
            {
                _BackColor = value;
            }
        }

        Font _Font;
        public Font Font
        {
            get
            {
                return _Font;
            }
            set
            {
                _Font = value;
            }
        }

        IBorder _Border = null;
        public IBorder Border
        {
            get
            {
                return _Border;
            }
            set
            {
                _Border = value;
            }
        }

        FormatInfo _FormatInfo = FormatInfo.Empty;
        public FormatInfo FormatInfo
        {
            get
            {
                return _FormatInfo;
            }
            set
            {
                _FormatInfo = value;
            }
        }

        int _HorizontalAlignment = 2;
        public int HorizontalAlignment
        {
            get
            {
                return _HorizontalAlignment;
            }
            set
            {
                _HorizontalAlignment = value;
            }
        }

        int _VerticalAlignment = 2;
        public int VerticalAlignment
        {
            get
            {
                return _VerticalAlignment;
            }
            set
            {
                _VerticalAlignment = value;
            }
        }

        public object Clone()
        {
            Style style = new Style();
            style.BackColor = this.BackColor;
            style.ForeColor = this.ForeColor;
            style.Font = (this.Font != null? this.Font.Clone() as Font : null);
            style.Border = this.Border;
            style.FormatInfo = this.FormatInfo.Clone() as FormatInfo;
            style.HorizontalAlignment = this.HorizontalAlignment;
            style.VerticalAlignment = this.VerticalAlignment;

            return style;
        }
    }
}
