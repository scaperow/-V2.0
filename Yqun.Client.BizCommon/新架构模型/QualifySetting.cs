using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class QualifySetting
    {
        public Guid SheetID { get; set; }
        public String CellName { get; set; }
    }
}
