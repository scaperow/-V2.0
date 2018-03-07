using System;
using System.Collections.Generic;
using ReportCommon;
using Yqun.Services;

namespace ReportComponents
{
    public class DepositoryReportFormat
    {
        public static Dictionary<FormatStyle, FormatStringGroup> InitReportFormats()
        {
            Dictionary<FormatStyle, FormatStringGroup> Groups = new Dictionary<FormatStyle, FormatStringGroup>();
            foreach (FormatStyle Format in Enum.GetValues(typeof(FormatStyle)))
            {
                FormatStringGroup group = InitReportFormats(Format);
                Groups.Add(Format, group);
            }

            return Groups;
        }

        public static FormatStringGroup InitReportFormats(FormatStyle FormatStyle)
        {
            return Agent.CallService("Yqun.BO.ReportManager.dll", "InitReportFormats", new object[] { FormatStyle }) as FormatStringGroup;
        }

        public static Boolean UpdateReportFormats(FormatInfo String)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReportManager.dll", "UpdateReportFormats", new object[] { String }));
        }

        public static Boolean DeleteReportFormats(FormatInfo String)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReportManager.dll", "DeleteReportFormats", new object[] { String }));
        }
    }
}
