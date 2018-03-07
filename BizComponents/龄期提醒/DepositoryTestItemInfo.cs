using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BizCommon;
using System.Data;
using Yqun.Services;

namespace BizComponents
{
    public class DepositoryTestItemInfo
    {
        public static List<TestItemInfo> GetTestItemInfos()
        {
            return Agent.CallService("Yqun.BO.ReminderManager.dll", "GetTestItemInfos", new object[] { }) as List<TestItemInfo>;
        }

        public static DataTable GetTestItemData()
        {
            return Agent.CallService("Yqun.BO.ReminderManager.dll", "GetTestItemData", new object[] { }) as DataTable;
        }

        public static Boolean UpdateTestItemData(DataTable ItemData)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReminderManager.dll", "UpdateTestItemData", new object[] { ItemData }));
        }
    }
}
