using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class BioKeyInfo
    {
        public string RalationID { get; set; }
        public string RegisterID { get; set; }
        public string RegisterName { get; set; }
        public object Template { get; set; }
        public string UserCode { get; set; }
    }
}
