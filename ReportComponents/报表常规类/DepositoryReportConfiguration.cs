using System;
using System.Collections.Generic;
using System.Data;
using Yqun.Services;
using ReportCommon;
using Yqun.Common.ContextCache;
using Yqun.Bases;
using FarPoint.Win;
using FarPoint.Win.Spread.CellType;
using System.IO;
using Yqun.Bases.ClassBases;

namespace ReportComponents
{
    public class DepositoryReportConfiguration
    {
        public static ReportConfiguration InitReportConfiguration(String Index)
        {
            return Agent.CallService("Yqun.BO.ReportManager.dll", "InitReportConfiguration", new object[] { Index }) as ReportConfiguration;
        }

        public static Boolean HaveReport(string Index)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReportManager.dll", "HaveReportConfiguration", new object[] { Index }));
        }

        public static bool New(ReportConfiguration Report)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReportManager.dll", "NewReportConfiguration", new object[] { Report }));
        }

        public static bool Update(ReportConfiguration Report)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReportManager.dll", "UpdateReportConfiguration", new object[] { Report }));
        }

        public static bool UpdateReportName(String ReportIndex, String Description)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReportManager.dll", "UpdateReportName", new object[] { ReportIndex, Description }));
        }

        public static bool Delete(ReportConfiguration Report)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReportManager.dll", "DeleteReportConfiguration", new object[] { Report }));
        }

        public static bool Delete(String ReportIndex)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReportManager.dll", "DeleteReportConfiguration", new object[] { ReportIndex }));
        }

        public static bool IsUpdatable(String Index, Object scts)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReportManager.dll", "IsUpdatable", new object[] { Index, scts }));
        }

        public static DataTable GetReportConfig()
        {
            return Agent.CallService("Yqun.BO.ReportManager.dll", "GetReportConfig", new object[] { }) as DataTable;
        }

        public static DataTable GetUnBindModules()
        {
            return Agent.CallService("Yqun.BO.ReportManager.dll", "GetUnBindModules", new object[] { }) as DataTable;
        }

        public static DataTable GetReportConfigList()
        {
            return Agent.CallService("Yqun.BO.ReportManager.dll", "GetReportConfigList", new object[] { }) as DataTable;
        }

        public static DataTable GetReportConfigData()
        {
            return Agent.CallService("Yqun.BO.ReportManager.dll", "GetReportConfigData", new object[] { }) as DataTable;
        }

        public static void AddReportConfigModule(String moduleID, String configID)
        {
            Agent.CallService("Yqun.BO.ReportManager.dll", "AddReportConfigModule", new object[] { moduleID, configID });
        }

        public static void RemoveReportConfigModule(String moduleID, String configID)
        {
            Agent.CallService("Yqun.BO.ReportManager.dll", "RemoveReportConfigModule", new object[] { moduleID, configID });
        }
    }
}
