using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class JZFormulaData
    {
        public int ColumnIndex { get; set; }
        public int RowIndex { get; set; }
        public string Formula { get; set; }
        public Guid ModelIndex { get; set; }
        public Guid SheetIndex { get; set; }
        public int FormulaType { get; set; }
    }
}
