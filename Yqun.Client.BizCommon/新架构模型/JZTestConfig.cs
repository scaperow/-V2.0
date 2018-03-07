using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    /// <summary>
    /// 试验模板上传配置
    /// </summary>
    [Serializable]
    public class JZTestConfig
    {
        /// <summary>
        /// 模板ID
        /// </summary>
        public Guid ModuleID { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public Int32 SerialNumber { get; set; }
        
        /// <summary>
        /// 试验数据配置信息
        /// </summary>
        public List<JZTestCell> Config { get; set; }
    }
}
