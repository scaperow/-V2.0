using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class Sys_TaiZhang
    {
        public List<JZCustomView> CustomViews { get; set; }
        public List<List<JZCell>> Data { get; set; }
        public Int32 TotalCount { get; set; }
    }
}
