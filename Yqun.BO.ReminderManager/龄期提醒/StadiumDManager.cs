using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Yqun.BO.ReminderManager.龄期提醒
{
    public class StadiumDManager : BOBase
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 获取龄期信息
        /// </summary>
        /// <param name="SNo"></param>
        /// <returns></returns>
        public DataTable GetDataByDelegateNo(string SNo)
        {
            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select ID,F_SYXM ,F_WTBH ,F_ZJRQ");
            sql_select.Append(" ,F_SJBH ,F_PH ,F_SJSize,DateSpan,F_IsDone,F_itemindex from sys_biz_reminder_stadiumData ");
            sql_select.Append(" where F_WTBH='" + SNo + "'");
            return GetDataTable(sql_select.ToString());
        }

        /// <summary>
        /// 更新龄期
        /// </summary>
        /// <param name="SNo">DataID</param>
        /// <param name="ColumnName">列名</param>
        /// <param name="ColumnValue">值</param>
        /// <returns></returns>
        public bool UpdateByDelegateTable(DataTable dt)
        {
            //StringBuilder sqlStr = new StringBuilder();
            //sqlStr.AppendFormat("update sys_biz_reminder_stadiumData set {0}='{1}' where DataID='{2}'",ColumnName,ColumnValue,SNo);
            if (Update(dt)>0)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// 数据删除
        /// </summary>
        /// <param name="sno"></param>
        /// <returns></returns>
        public bool DelByDelegateNo(string sno)
        {
            if (ExcuteCommand("delete from sys_biz_reminder_stadiumData where DataID='" + sno + "'") > 0)
            {
                return true;
            }
            return false;
        }

    }
}
