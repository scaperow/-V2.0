using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCommon
{
    /// <summary>
    /// 格式类型
    /// </summary>
    [Serializable]
    public enum FormatStyle : byte
    {
        /// <summary>
        /// 常规
        /// </summary>
        General,

        /// <summary>
        /// 数字
        /// </summary>
        Number,

        /// <summary>
        /// 货币
        /// </summary>
        Currency,

        /// <summary>
        /// 百分比
        /// </summary>
        Percent,

        /// <summary>
        /// 科学计数法
        /// </summary>
        ScientificCount,

        /// <summary>
        /// 日期型
        /// </summary>
        Date,

        /// <summary>
        /// 时间型
        /// </summary>
        Time,

        /// <summary>
        /// 文本型
        /// </summary>
        Text
    }
}
