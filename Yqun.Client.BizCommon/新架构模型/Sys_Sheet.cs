using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class Sys_Sheet
    {
        public Guid ID { get; set; }
        public String Name { get; set; }
        public String CatlogCode { get; set; }
        public String SheetXML { get; set; }
        public String SheetData { get; set; }
        public String CellLogic { get; set; }
        public String Formulas { get; set; }
    }
}
