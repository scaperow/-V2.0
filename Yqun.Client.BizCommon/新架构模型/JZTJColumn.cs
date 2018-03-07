using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class JZTJColumn
    {
        /// <summary>
        /// 描述,表示列头
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 是否是标准
        /// </summary>
        public bool IsStandard { get; set; }
    }
}
