using System;
using System.Collections.Generic;
using System.Text;
using ReportCommon;
using Yqun.Services;
using System.Collections;

namespace ReportComponents
{
    public class ReportService
    {
        public static List<ReportParameter> GetReportParameters(Guid reportID)
        {
            return Agent.CallService("Yqun.BO.ReportManager.dll", "GetReportParametersByReportID", new object[] { reportID }) as List<ReportParameter>;
        }

        public static String DoReport(String descriptor, List<ReportParameter> parameters)
        {
            Hashtable list = new Hashtable();
            foreach (ReportParameter parameter in parameters)
            {
                list.Add(parameter.Name, parameter);
            }
            return Convert.ToString(Agent.CallService("Yqun.BO.ReportManager.dll", "DoReport", new object[] { }));
        }

        public static String GetReportString(Guid reportID, String start, String end, String testRoomCodes, List<ReportParameter> parameters)
        {
            Hashtable list = new Hashtable();
            foreach (ReportParameter parameter in parameters)
            {
                list.Add(parameter.Name, parameter.Value);
            }
            return Convert.ToString(Agent.CallService("Yqun.BO.ReportManager.dll", "GetReportString", new object[] { reportID, start, end, testRoomCodes, list }));
        }

        public static Boolean SaveReport(Guid reportID, String sheetXML, String config)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReportManager.dll", "SaveReport", new object[] { reportID, sheetXML, config }));
        }

        public static Guid GetReportIDByName(String name)
        {
            return (Guid)Agent.CallService("Yqun.BO.ReportManager.dll", "GetReportIDByName", new object[] { name });
        }

        public static String GetReportXML(String id)
        {
            return Agent.CallService("Yqun.BO.ReportManager.dll", "GetReportXML", new object[] { id }).ToString();
        }
    }
}
