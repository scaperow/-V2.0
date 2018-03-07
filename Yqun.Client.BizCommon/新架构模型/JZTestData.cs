using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    /// <summary>
    /// 试验数据模型
    /// </summary>
    [Serializable]
    public class JZTestData
    {
        /// <summary>
        /// 委托编号
        /// </summary>
        public String WTBH { get; set; }
        /// <summary>
        /// 资料ID
        /// </summary>
        public Guid DocumentID { get; set; }
        /// <summary>
        /// 龄期ID
        /// </summary>
        public Guid StadiumID { get; set; }
        /// <summary>
        /// 模板ID
        /// </summary>
        public Guid ModuleID { get; set; }
        /// <summary>
        /// 试验室编码
        /// </summary>
        public String TestRoomCode { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public String UserName { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public Int32 SerialNumber { get; set; }
        /// <summary>
        /// 试验结果数据
        /// </summary>
        public List<JZTestCell> TestCells { get; set; }
        /// <summary>
        /// 试验实时数据
        /// </summary>
        public List<JZRealTimeData> RealTimeData { get; set; }
    }
}
