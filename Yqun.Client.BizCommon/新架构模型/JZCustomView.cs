using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    /// <summary>
    /// 系统或个人台账设置
    /// </summary>
    [Serializable]
    public class JZCustomView
    {
        public String Description { get; set; }
        public Guid SheetID { get; set; }
        public String CellName { get; set; }
        public Int32 ForeColor { get; set; }
        public Int32 BgColor { get; set; }
        public float ColumnWidth { get; set; }
        public Boolean IsEdit { get; set; }
        public String DocColumn { get; set; }
    }
}
