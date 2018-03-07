using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class Sys_Device
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { set; get; }
        /// <summary>
        /// 机器编码
        /// </summary>
        public string MachineCode { set; get; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { set; get; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { set; get; }
        /// <summary>
        /// 类型
        /// </summary>
        public DeviceTypeEnum DeviceType { set; get; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastEditTime { set; get; }
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string LastEditBy { set; get; }
        /// <summary>
        /// 生产厂家名称
        /// </summary>
        public string DeviceCompany { set; get; }
        /// <summary>
        /// 是否为电液伺服
        /// </summary>
        public bool IsDYSF { set; get; }
        /// <summary>
        /// 公管中心编码
        /// </summary>
        public string RemoteCode1 { set; get; }
        /// <summary>
        /// 信息中心编码
        /// </summary>
        public string RemoteCode2 { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { set; get; }
        /// <summary>
        /// 客户端配置
        /// </summary>
        public string ClientConfig { set; get; }
        /// <summary>
        /// 配置文件更新时间
        /// </summary>
        public DateTime ConfigUpdateTime { set; get; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { set; get; }
        /// <summary>
        /// 实验室编码
        /// </summary>
        public string TestRoomCode { set; get; }
        /// <summary>
        /// 客户端配置文件状态
        /// </summary>
        public int ConfigStatus { set; get; }
        /// <summary>
        /// 量程
        /// </summary>
        public int Quantum { set; get; }
    }
}
