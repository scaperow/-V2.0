using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BizCommon;

namespace ShuXianCaiJiModule
{
    [Serializable]
    public class UploadModule
    {
        public List<float> List { get; set; }
        public Int32 CurrentNumber { get; set; }
        public List<JZRealTimeData> RealTimeData { get; set; }
        public float MaxForce { get; set; }
        public Int32 TotalNumber { get; set; }
    }
}
