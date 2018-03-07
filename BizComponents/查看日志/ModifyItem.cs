using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizComponents
{
    [Serializable]
    public class ModifyItem
    {
        public String SheetName { get; set; }
        public String CellPosition { get; set; }
        public String OriginalValue { get; set; }
        public String CurrentValue { get; set; }
    }
}
