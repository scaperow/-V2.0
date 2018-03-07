using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yqun.Services;
using BizCommon;
using System.Data;

namespace BizComponents
{
    public class LineHelperClient
    {
        public static Boolean GenerateUpdateFile(Guid projectID)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "GenerateUpdateFile", new object[] { projectID }));
        }

        public static Boolean UpdateLine(Guid projectID)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateLine", new object[] { projectID }));
        }

        public static DataTable GetLineList()
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetLineList", new object[] { }) as DataTable;
        }

        public static Boolean SyncLineAndModule(List<String> moduleIDs, List<Guid> lineIDs, Boolean isModule, Boolean isRelationSheet)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "SyncLineAndModule", new object[] { moduleIDs, lineIDs, isModule, isRelationSheet }));
        }

        public static DataSet GetLineModuleList()
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetLineModuleList", new object[] { }) as DataSet;
        }

        public static Boolean UpdateSpecialModule(Guid sourceModuleID, Guid destModuleID)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateSpecialModule", new object[] { sourceModuleID, destModuleID }));
        }
        /// <summary>
        /// 获取所有在线用户
        /// </summary>
        public static DataTable GetOnlineUserList()
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetOnlineUserList", new object[] { }) as DataTable;
        }
    }
}
