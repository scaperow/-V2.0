using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    /// <summary>
    /// 试验项目枚举
    /// </summary>
    [Serializable]
    public enum JZTestEnum
    {
        /// <summary>
        /// 万能机：断后标距
        /// </summary>
        DHBJ,

        /// <summary>
        /// 万能机：屈服力
        /// </summary>
        QFL,

        /// <summary>
        /// 万能机：拉断最大力
        /// </summary>
        LDZDL,

        /// <summary>
        /// 压力机：破坏荷载
        /// </summary>
        PHHZ
    }
}
