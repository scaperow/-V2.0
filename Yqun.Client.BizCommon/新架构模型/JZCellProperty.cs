using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class JZCellProperty
    {
        /// <summary>
        /// 是否为重要字段
        /// </summary>
        public Boolean IsKey { get; set; }

        /// <summary>
        /// 是否在当前试验室、当前模板中唯一
        /// </summary>
        public Boolean IsUnique { get; set; }

        /// <summary>
        /// 是否为只读字段，不允许普通用户编辑
        /// </summary>
        public Boolean IsReadOnly { get; set; }

        /// <summary>
        /// 是否该字段允许为空
        /// </summary>
        public Boolean IsNotNull { get; set; }

        /// <summary>
        /// 改字段内容是否允许被复制
        /// </summary>
        public Boolean IsNotCopy { get; set; }

        /// <summary>
        /// 该字段是否允许被平行
        /// </summary>
        public Boolean IsPingxing { get; set; }

        /// <summary>
        /// 该字段内容代表的意义
        /// </summary>
        public String Description { get; set; }
    }
}
