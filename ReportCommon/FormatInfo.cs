using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCommon
{
    [Serializable]
    public class FormatInfo : ICloneable
    {
        public readonly static FormatInfo Empty = new FormatInfo();

        String index = Guid.NewGuid().ToString();
        public String Index
        {
            get
            {
                return index;
            }
            set
            {
                index = value;
            }
        }

        FormatStyle style;
        public FormatStyle Style
        {
            get
            {
                return this.style;
            }
            set
            {
                this.style = value;
            }
        }

        String format;
        public String Format
        {
            get
            {
                return format;
            }
            set
            {
                format = value;
            }
        }

        public object Clone()
        {
            FormatInfo formatInfo = new FormatInfo();
            formatInfo.Index = this.Index;
            formatInfo.Format = this.Format;
            formatInfo.Style = this.Style;

            return formatInfo;
        }

        public override string ToString()
        {
            return format;
        }
    }
}
