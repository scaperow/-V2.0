using System;
using Yqun.Services;

namespace ReportComponents
{
    public class DepositoryRepairSheetConfiguration
    {
        public static Boolean RepairSheetConfiguration()
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReportManager.dll", "RepairSheetConfiguration", new object[] { }));
        }
    }
}
