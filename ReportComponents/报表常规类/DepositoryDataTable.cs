using System;
using System.Collections.Generic;
using System.Text;
using Yqun.Services;
using System.Data;

namespace ReportComponents
{
    public class DepositoryDataTable
    {
        public static List<string> GetTables()
        {
            return Agent.CallService("Yqun.BO.ReportManager.dll", "GetTables", new object[] { }) as List<string>;
        }

        public static DataTable GetFields(String[] Tables)
        {
            return Agent.CallService("Yqun.BO.ReportManager.dll", "GetFields", new object[] { Tables }) as DataTable;
        }

        public static Boolean AnalysisSQLCommand(String Command)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReportManager.dll", "AnalysisSQLCommand", new object[] { Command }));
        }

        public static List<string> GetFields(String Command)
        {
            return Agent.CallService("Yqun.BO.ReportManager.dll", "GetFields", new object[] { Command }) as List<string>;
        }
    }
}
