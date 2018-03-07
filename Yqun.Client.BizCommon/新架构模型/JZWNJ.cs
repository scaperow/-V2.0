using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    /// <summary>
    /// 万能机试验模型
    /// </summary>
    [Serializable]
    public class JZWNJ
    {
        /// <summary>
        /// 模板ID
        /// </summary>
        public Guid ModuleID { get; set; }
        /// <summary>
        /// 龄期ID
        /// </summary>
        public Guid StadiumID { get; set; }
        /// <summary>
        /// 资料ID
        /// </summary>
        public Guid DocumentID { get; set; }
        /// <summary>
        /// 试验名称
        /// </summary>
        public String TestName { get; set; }
        /// <summary>
        /// 委托编号
        /// </summary>
        public String WTBH { get; set; }
        /// <summary>
        /// 钢筋直径
        /// </summary>
        public String GJZJ { get; set; }
        /// <summary>
        /// 级别代号
        /// </summary>
        public String JBDH { get; set; }
        /// <summary>
        /// 试件序号，从1开始
        /// </summary>
        public Int32 ItemIndex { get; set; }
    }
}
