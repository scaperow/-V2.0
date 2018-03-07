using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BizCommon;
using Yqun.Services;

namespace BizComponents
{
    public class DepositoryLinkedReport
    {
        public static List<LinkedReportInfo> GetLinkedReportInfos(String NodeFlag, String NodeCode)
        {
            return Agent.CallService("Yqun.BO.ExtraBO.dll", "GetLinkedReportInfos", new object[] { NodeFlag, NodeCode }) as List<LinkedReportInfo>;
        }
    }
}
