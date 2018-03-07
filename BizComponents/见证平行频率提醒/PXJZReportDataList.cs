using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Yqun.Services;

namespace BizComponents
{
    public class PXJZReportDataList
    {
        public static DataTable GetPXReportInfos(String segment, String company, String testroom, DateTime Start, DateTime End)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetPXJZReportInfo", new object[] { segment, company, testroom, Start, End, 1 }) as DataTable;
        }
        public static DataTable GetPXReportRelation(String PXTestRoomCode, Guid ModuleID, DateTime StartTime, DateTime EndTime, string BGBH)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetPXReportRelation", new object[] { PXTestRoomCode, ModuleID, StartTime, EndTime, BGBH }) as DataTable;
        }
        public static DataTable GetJZReportInfos(String segment, String company, String testroom, DateTime Start, DateTime End)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetPXJZReportInfo", new object[] { segment, company, testroom, Start, End, 2 }) as DataTable;
        }

        public static DataTable GetInvalidJZ(DateTime start, DateTime end)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetInvalidJZ", new object[] { start, end }) as DataTable;
        }

        public static DataTable GetInvalidPX(DateTime start, DateTime end)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetInvalidPX", new object[] { start, end }) as DataTable;
        }

        public static DataTable GetPXJZReportChart(String testRoomID, DateTime start, DateTime end, String modelID)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetPXJZReportChart", new object[] { testRoomID, start, end, modelID }) as DataTable;
        }

        /// <summary>
        /// 获取服务器标准数据集
        /// </summary>
        /// <param name="type">1=平行，2=见证</param>
        /// <returns>标准数据集</returns>
        public static DataTable GetWitnessRateInfos(string type)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetWitnessRateInfo", new object[] { type }) as DataTable;
        }

        /// <summary>
        /// 设置见证频率
        /// </summary>
        /// <param name="dt">设置后数据集</param>
        /// <returns>true成功，false失败</returns>
        public static string SetWitnessRateInfo(DataTable dt)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "SetWinessRateInfo", new object[] { dt as object }).ToString();
        }

        /// <summary>
        /// 根据报告编号或委托编号查询可被平行的报告编号
        /// </summary>
        /// <returns>标准数据集</returns>
        public static DataTable GetEnPXedReport(Guid ModuleID, String BGBH)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetEnPXedReport", new object[] { ModuleID, BGBH }) as DataTable;
        }

        /// <summary>
        /// 生成平行对应关系
        /// </summary>
        public static int GeneratePXRelation(Guid SGDataID, Guid PXDataID)
        {
            return Convert.ToInt32(Agent.CallService("Yqun.BO.BusinessManager.dll", "GeneratePXRelation", new object[] { SGDataID, PXDataID }));
        }
        /// <summary>
        /// 删除平行对应关系
        /// </summary>
        public static int DeletePXRelation(Guid PXDataID)
        {
            return Convert.ToInt32(Agent.CallService("Yqun.BO.BusinessManager.dll", "DeletePXRelation", new object[] {  PXDataID }));
        }
    }
}
