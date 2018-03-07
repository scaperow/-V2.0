using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BizCommon;
using Yqun.Services;
using System.Data;

namespace BizComponents
{
    public class DepositoryDataModificationInfo
    {
        public static DataTable InitDataModificationList(String[] TestRoomCode)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "InitDataModificationList", new object[] { TestRoomCode }) as DataTable;
        }

        public static DataTable InitDataModificationList(String NodeCode, DateTime Start, DateTime End)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "InitDataModificationList", new object[] { NodeCode, Start, End }) as DataTable;
        }

        public static DataTable InitDataModificationList(String segment, String company, String testroom, DateTime start, DateTime end, String status, String content, String user)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "InitDataModificationList", new object[] { segment, company, testroom, start, end, status, content, user }) as DataTable;
        }

        public static Boolean HaveDataModificationInfo(String DataID)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "HaveDataModificationInfo", new object[] { DataID }));
        }

        public static DataTable HaveDataModificationInfoByID(String DataID)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "HaveDataModificationInfoByID", new object[] { DataID }) as DataTable; 
        }

        public static Boolean NewDataModificationInfo(DataModificationInfo Info)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "NewDataModificationInfo", new object[] { Info }));
        }

        public static Boolean UpdateDataModificationInfo(String[] IDs, String ApprovePerson, String State, String processReason)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateDataModificationInfo", new object[] { IDs, ApprovePerson, State, processReason }));
        }
        public static Boolean UpdateDataModificationInfoAndStadium(String[] IDs, String ApprovePerson, String State, String processReason, int IsRequestStadium)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateDataModificationInfoAndStadium", new object[] { IDs, ApprovePerson, State, processReason, IsRequestStadium }));
        }

        public static Int64 GetOperateLogIDByModifyID(String modifyID)
        {
            return Int64.Parse(Agent.CallService("Yqun.BO.BusinessManager.dll", "GetOperateLogIDByModifyID", new object[] { modifyID }).ToString());
        }
    }
}
