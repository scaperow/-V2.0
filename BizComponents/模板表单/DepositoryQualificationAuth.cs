using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yqun.Services;

namespace BizComponents
{
    public class DepositoryQualificationAuth
    {
        public static List<String> InitQualificationModelIndex(String FolderCode)
        {
            return Agent.CallService("Yqun.BO.QualificationAuthManager.dll", "InitQualificationModelIndex", new object[] { FolderCode }) as List<String>;
        }

        public static List<String> InitTestRoomQualificationAuth(String FolderCode)
        {
            return Agent.CallService("Yqun.BO.QualificationAuthManager.dll", "InitQualificationAuthInfo", new object[] { FolderCode }) as List<String>;
        }

        public static Boolean UpdateQualificationAuth(String FolderCode, String Codes)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.QualificationAuthManager.dll", "UpdateQualificationAuth", new object[] { FolderCode, Codes }));
        }
    }
}
