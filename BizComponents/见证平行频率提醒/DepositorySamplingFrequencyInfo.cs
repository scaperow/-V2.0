using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using Yqun.Services;

namespace BizComponents
{
    public class DepositorySamplingFrequencyInfo
    {
        public static SamplingFrequencyInfo InitSamplingFrequencyInfo(String Index)
        {
            return Agent.CallService("Yqun.BO.ReminderManager.dll", "InitSamplingFrequencyInfo", new object[] { Index }) as SamplingFrequencyInfo;
        }

        public static Boolean UpdateSamplingFrequencyInfo(SamplingFrequencyInfo Info)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReminderManager.dll", "UpdateSamplingFrequencyInfo", new object[] { Info }));
        }

        public static String SupervisionReportInfo(String Code)
        {
            return (Agent.CallService("Yqun.BO.BusinessManager.dll", "SupervisionInfo", new object[] { Code })).ToString();
        }
    }
}
