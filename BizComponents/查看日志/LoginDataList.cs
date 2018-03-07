using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yqun.Services;
using System.Data;

namespace BizComponents
{
    public class LoginDataList
    {
        public static DataTable GetLoginLogInfos(String segment, String company, String testroom, DateTime Start, DateTime End, String username,int pageindex,int PageSize, int doCount)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetLoginLogInfo", new object[] { segment, company, testroom, Start, End, username, pageindex, PageSize, doCount }) as DataTable;
        }

        public static DataTable GetOperateLogList(String segment, String company, String testroom, DateTime Start, DateTime End, String username, int pageindex, int PageSize, int doCount)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetOperateLogList", new object[] { segment, company, testroom, Start, End, username, pageindex, PageSize, doCount }) as DataTable;
        }

        public static DataTable GetOperateBaseInfo(Int64 logID)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetOperateBaseInfo", new object[] { logID }) as DataTable;
        }
    }
}
