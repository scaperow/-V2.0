using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    public enum DeviceStatusEnum
    {
        Enable = 0,
        Disable = 1,
        Delete = 2

    }

    public enum DeviceTypeEnum
    {
        /// <summary>
        /// 万能机
        /// </summary>
        Universal = 2,
        /// <summary>
        /// 压力机
        /// </summary>
        Pressure = 1
    }

    /// <summary>
    /// 当调用存储过程 sp_pager 时需要传入的 @OrderType 值
    /// </summary>
    public enum OrderEnum
    {
        /// <summary>
        /// 降序
        /// </summary>
        Asc = 0,
        /// <summary>
        /// 升序
        /// </summary>
        Desc = 1
    }

    /// <summary>
    /// 当调用存储过程 sp_pager 时需要传入的 @doCount 值
    /// </summary>
    public enum DocountEnum
    {
        /// <summary>
        /// 获取数据
        /// </summary>
        GetData = 0,
        /// <summary>
        /// 获取页总数
        /// </summary>
        GetTotalCount = 1

    }
}
