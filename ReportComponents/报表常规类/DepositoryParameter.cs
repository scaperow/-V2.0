using System;
using System.Collections.Generic;
using Yqun.Services;
using ReportCommon;

namespace ReportComponents
{
    public class DepositoryReportParameter
    {
        public static List<ReportParameter> getReportParameters(String ReportIndex)
        {
            return Agent.CallService("Yqun.BO.ReportManager.dll", "getReportParameters", new object[] { ReportIndex }) as List<ReportParameter>;
        }

        public static Boolean saveReportParameter(String ReportIndex, List<ReportParameter> Parameters)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReportManager.dll", "saveReportParameter", new object[] { ReportIndex, Parameters }));
        }
    }
}
