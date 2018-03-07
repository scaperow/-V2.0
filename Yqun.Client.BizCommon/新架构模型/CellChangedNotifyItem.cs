using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class CellChangedNotifyItem
    {
        public String Description { get; set; }
        public String CellPosition { get; set; }
        public String OriginalValue { get; set; }
        public String CurrentValue { get; set; }
    }
}
