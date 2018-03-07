using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Yqun.BO.ReminderManager
{
    public class WirteLog : BOBase
    {

        /// <summary>
        /// 获取当前修改的修改原因
        /// </summary>
        /// <param name="weno"></param>
        /// <returns></returns>
        public DataTable GetReasonTable(string dataID, string type)
        {
            return GetDataTable("select ReasonID,WTBH,UpdateUser,SQUser,SPUser,ReasonContent,RecoderTime,OptionType from dbo.sys_auth_CustomerService_UpdateInfo where WTBH='" + dataID + "' and optiontype=" + type + " order by RecoderTime desc");
        }

        /// <summary>
        /// 更新修改原因
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool UpdataReason(DataTable dt)
        {
            if (Update(dt) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 写入登录日志
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public bool LoginLog(string sqlStr)
        {
            if (ExcuteCommand(sqlStr) > 0)
            {
                return true;
            }
            return false;
        }
    }
}
