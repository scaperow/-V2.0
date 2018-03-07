using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace BizCommon
{
    [Serializable]
    public class JZCellStyle
    {
        public Color ForColor { set; get; }
        public Color BackColor { set; get; }
        public string FamilyName { set; get; }
        public FontStyle FontStyle { set; get; }
        public float FontSize { set; get; }
    }
}
