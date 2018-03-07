using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class Sys_RequestChange
    {
        public Guid ID { get; set; }
        public Guid DocumentID { get; set; }
        public String Caption { get; set; }
        public String Reason { get; set; }
        public int IsRequestStadium { get; set; }
    }
}
