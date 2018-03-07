using System;
using System.Collections.Generic;
using System.Text;

namespace Yqun.Client.BizUI
{
    public class JZWatermark
    {
        public String FilePath { get; set; }
        public Int32 Left { get; set; }
        public Int32 Top { get; set; }
        public Int32 Opacity { get; set; }
        public Guid SheetID { get; set; }
        public Int32 Width { get; set; }
        public Int32 Height { get; set; }
    }
}
