using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class ModuleSetting
    {
        public String DocColumn { get; set; }
        public Guid SheetID { get; set; }
        public String CellName { get; set; }
        public String Description { get; set; }
        public Boolean IsShow { get; set; }
    }
}
