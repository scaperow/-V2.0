using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShuXianCaiJiComponents
{
    public class QFModule
    {
        /// <summary>
        /// 屈服过程中含的点数量
        /// </summary>
        public Int32 QFCount { get; set; }

        /// <summary>
        /// 上屈服
        /// </summary>
        public float UpQF { get; set; }

        /// <summary>
        /// 下屈服
        /// </summary>
        public float DownQF { get; set; }
    }
}
