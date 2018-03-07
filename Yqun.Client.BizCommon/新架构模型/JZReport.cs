using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class JZReport
    {
        public Int32 StartRowIndex { get; set; }
        public Int32 RepeatRowCount { get; set; }
        public Int32 StartColumnIndex { get; set; }
        public Int32 ColumnCount { get; set; }
    }
}
