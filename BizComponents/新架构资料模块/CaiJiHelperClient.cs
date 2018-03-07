using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Yqun.Services;

namespace BizComponents
{
    public class CaiJiHelperClient
    {

        public static DataTable GetUnusedTestData(string strWTBH)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetUnusedTestData", new object[] { strWTBH}) as DataTable;
        }

        public static String GetRealTimeTestData(Guid id)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetRealTimeTestData", new object[] { id }).ToString();
        }

        public static DataTable GetTestOverTimeByID(Guid id)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetTestOverTimeByID", new object[] { id }) as DataTable;
        }

        public static DataTable GetTestOverTimeByDataID(Guid dataID)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetTestOverTimeByDataID", new object[] { dataID }) as DataTable;

        }

        public static void SubmitComment(Guid testOverTimeID, String comment)
        {
            Agent.CallService("Yqun.BO.BusinessManager.dll", "SubmitComment", new object[] { testOverTimeID, comment });
        }
        public static void SubmitCommentMulti(Guid[] ids, String comment, Int32 status)
        {
            Agent.CallService("Yqun.BO.BusinessManager.dll", "SubmitCommentMulti", new object[] { ids, comment, status });
        }

        public static void ProcessTestOverTime(Guid[] ids, String comment, Int32 status)
        {
            Agent.CallService("Yqun.BO.BusinessManager.dll", "ProcessTestOverTime", new object[] { ids, comment, status });
        }
        public static void UpdateTestOverTimeStatus(Guid[] ids, Int32 status)
        {
            Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateTestOverTimeStatus", new object[] { ids, status });
        }

        public static void ManualUploadMQ()
        {
            Agent.CallService("Yqun.BO.BusinessManager.dll", "ManualUploadMQ", new object[] { });
        }
        public static void ReloadRabbitMQ(string strStartTime)
        {
            Agent.CallService("Yqun.BO.BusinessManager.dll", "ReloadRabbitMQ", new object[] {strStartTime });
        }
    }
}
