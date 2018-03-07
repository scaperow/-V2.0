using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Yqun.BO.ReminderManager
{
    public class RepairRateOfParallel : BOBase
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// 根据施工报告ID获取平行关系
        /// </summary>
        /// <param name="TestDataID"></param>
        /// <returns></returns>
        public DataTable GetSGData(string SGTestDataID)
        {
            return GetDataTable("select * from biz_px_relation where SGDataID='" + SGTestDataID + "'");
        }

        /// <summary>
        /// 获取所有模版
        /// </summary>
        /// <returns></returns>
        public DataTable GetModelAll(string condition)
        {
            return GetDataTable("select * from dbo.sys_biz_Module  where " + condition);
        }

        /// <summary>
        /// 更新平行关系
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int UpdatePXInfo(DataTable dt)
        {
            return Update(dt);
        }

        /// <summary>
        /// 根据监理报告ID获取平行关系
        /// </summary>
        /// <param name="JLTestDataID"></param>
        /// <returns></returns>
        public DataTable GetPXInfo(string JLTestDataID)
        {
            return GetDataTable("select * from biz_px_relation where PXDataID='" + JLTestDataID + "'");
        }


        /// <summary>
        /// 删除施工关系
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int DelSGInfo(string sgDataid)
        {
            return ExcuteCommand("delete from biz_px_relation where SGDataID='" + sgDataid + "'");
        }

        /// <summary>
        /// 删除平行关系
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int DelPXInfo(string pxDataid)
        {
            return ExcuteCommand("delete from biz_px_relation where PXDataID='" + pxDataid + "'");
        }

        /// <summary>
        /// 获取实验室相关信息
        /// </summary>
        /// <param name="TestID"></param>
        /// <returns></returns>
        public DataTable GetTestInfo(string TestID)
        {
            return GetDataTable(" select * from [dbo].[Sys_Tree] where NodeCode in('" + TestID.Substring(0, 4) + "','" + TestID.Substring(0, 8) + "','" + TestID.Substring(0, 12) + "','" + TestID + "') order by OrderID");
        }


        /// <summary>
        /// 插入新平行关系
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <returns></returns>
        public int InsertIntoPXInfo(string SqlStr)
        {
            return ExcuteCommand(SqlStr);
        }
    }
}