using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShuXianCaiJiComponents
{
    /// <summary>
    /// 实验室压力机上传铁道部数据结构
    /// </summary>
    public class UploadYLJInfo
    {
        /// <summary>
        /// 实验时间
        /// </summary>
        public String F_SYSJ { get; set; }
        /// <summary>
        /// 实验室编码
        /// </summary>
        public String F_RTCODE { get; set; }
        /// <summary>
        /// 委托编号
        /// </summary>
        public String F_WTBH { get; set; }
        /// <summary>
        /// 设备编码
        /// </summary>
        public String F_SBBH { get; set; }
        /// <summary>
        /// 抗压力值
        /// </summary>
        public String F_KYLZ { get; set; }
        /// <summary>
        /// 龄期
        /// </summary>
        public String F_LQ { get; set; }
        /// <summary>
        /// 是否添加外加剂
        /// </summary>
        public String F_ISWJJ { get; set; }
        /// <summary>
        /// 操作员名称
        /// </summary>
        public String F_OPERATOR { get; set; }
        /// <summary>
        /// GUID
        /// </summary>
        public String F_GUID { get; set; }
        /// <summary>
        /// 试件尺寸
        /// </summary>
        public String F_SJCC { get; set; }
        /// <summary>
        /// 强度等级
        /// </summary>
        public String F_QDDJ { get; set; }
        /// <summary>
        /// 事件编号
        /// </summary>
        public String F_SJBH { get; set; }
        /// <summary>
        /// 原始抗压力值
        /// </summary>
        public String F_YSKYLZ { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public String F_SOFTCOM { get; set; }
    }
}
