using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yqun.Services;
using System.Data;

namespace BizComponents
{
    public class ProjectHelperClient
    {
        public static DataTable GetTestRoomCodeView()
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetTestRoomCodeView", new object[] { }) as DataTable;
        }
        public static DataTable GetTestRoomInfoByCode(string TestRoomCode)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetTestRoomInfoByCode", new object[] { TestRoomCode }) as DataTable;
        }

        public static bool SyncGGCUserAuth()
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "SyncGGCUserAuth", new object[] {}).ToString());
        }
    }
}
