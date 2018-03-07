using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    /// <summary>
    /// 试验单元格数据来源
    /// </summary>
    [Serializable]
    public class JZTestCell
    {
        /// <summary>
        /// 试验项目，如：屈服力、压力
        /// </summary>
        public JZTestEnum Name { get; set; }
        /// <summary>
        /// 表单ID
        /// </summary>
        public Guid SheetID { get; set; }
       
        /// <summary>
        /// 单元格位置，如H4
        /// </summary>
        public String CellName { get; set; }
        /// <summary>
        /// 试验结果值
        /// </summary>
        public Object Value { get; set; }
    }
}
