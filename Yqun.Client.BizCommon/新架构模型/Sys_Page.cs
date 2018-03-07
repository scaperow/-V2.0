using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace BizCommon
{
    [Serializable]
    public class Sys_Page
    {
        public DataTable Source { set; get; }
        public int TotalCount { set; get; }
        public int Index { set; get; }
        public int Size { set; get; }
    }
}
