using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class Sys_Statistics_Item
    {
        public Guid ID { set; get; }
        public string Name { set; get; }
        public string Columns { set; get; }
        public int Weight { set; get; }
        public int Status { set; get; }
        public int Type { set; get; }

        public Sys_Statistics_Item()
        {
            ID = Guid.NewGuid();
        }
    }
}
