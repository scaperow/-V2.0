using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCommon
{
    [Serializable]
    public class PageBreak : ICloneable
    {
        public readonly static PageBreak Empty = new PageBreak();

        Boolean _IsAfterRow;
        public Boolean IsAfterRow
        {
            get
            {
                return _IsAfterRow;
            }
            set
            {
                _IsAfterRow = value;
            }
        }

        public object Clone()
        {
            PageBreak pageBreak = new PageBreak();
            pageBreak.IsAfterRow = this.IsAfterRow;

            return pageBreak;
        }
    }
}
