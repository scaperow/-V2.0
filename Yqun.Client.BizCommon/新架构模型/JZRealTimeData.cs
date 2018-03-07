using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    /// <summary>
    /// 实时数据对象
    /// </summary>
    [Serializable]
    public class JZRealTimeData
    {
        /// <summary>
        /// 实时时间
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 实时值
        /// </summary>
        public float Value { get; set; }
    }
}
