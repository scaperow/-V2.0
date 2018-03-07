using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BizCommon;
using Yqun.Services;

namespace BizComponents
{
    public class UploadHelperClient
    {
        public static UploadSetting GetUploadSettingByModuleID(Guid moduleID)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetUploadSettingByModuleID", new object[] { moduleID }) as UploadSetting;
        }

        public static Boolean UpdateUploadSetting(Guid moduleID, String json)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateUploadSetting", new object[] { moduleID, json }));
        }

        public static Boolean UploadUpdateFile(JZFile f)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UploadUpdateFile", new object[] { f }));
        }

        public static Boolean UploadFileByLineID(JZFile f, String lineID)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UploadFileByLineID", new object[] { f, lineID }));
        }


        public static GGCUploadSetting GetGGCUploadSettingByModuleID(Guid moduleID)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetGGCUploadSettingByModuleID", new object[] { moduleID }) as GGCUploadSetting;
        }

        public static Boolean UpdateGGCUploadSetting(Guid moduleID, String json)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateGGCUploadSetting", new object[] { moduleID, json }));
        }

        public static GGCUploadSetting GetGGCDocUploadSettingByModuleID(Guid moduleID)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetGGCDocUploadSettingByModuleID", new object[] { moduleID }) as GGCUploadSetting;
        }

        public static Boolean UpdateGGCDocUploadSetting(Guid moduleID, String json)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateGGCDocUploadSetting", new object[] { moduleID, json }));
        }
    }
}
