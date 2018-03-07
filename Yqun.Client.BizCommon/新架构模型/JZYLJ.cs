using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    /// <summary>
    /// 压力机模型
    /// </summary>
    [Serializable]
    public class JZYLJ
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
        /// 试件尺寸
        /// </summary>
        public String SJCC { get; set; }
        /// <summary>
        /// 强度等级
        /// </summary>
        public String QDDJ { get; set; }
        /// <summary>
        /// 试件组号
        /// </summary>
        public String SJZH { get; set; }
        /// <summary>
        /// 原始标距
        /// </summary>
        public String YSBJ { get; set; }
        /// <summary>
        /// 试件序号，从1开始
        /// </summary>
        public Int32 ItemIndex { get; set; }
        
    }
}
