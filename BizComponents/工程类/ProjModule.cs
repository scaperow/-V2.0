using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Yqun.Services;
using System.Windows.Forms;
using Yqun.Data.DataBase;
using BizCommon;

namespace BizComponents
{
    public class ProjModule
    {
        public static List<QueryInfo> QueryModuleInfo(string FolderCode)
        {
            List<QueryInfo> ModuleList = Agent.CallService("Yqun.BO.BusinessManager.dll", "QueryModuleInfo", new object[] { FolderCode }) as List<QueryInfo>;
            return ModuleList;
        }

        public static Boolean HasData(string ModuleCode, string ModuleIndex)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "HasData", new object[] { ModuleCode, ModuleIndex }));
            return Result;
        }

        public static Boolean HasDataByModuleIDAndTestRoomCode(String moduleID, String testRoomCode)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "HasDataByModuleIDAndTestRoomCode", new object[] { moduleID, testRoomCode }));
            return Result;
        }

        public static Boolean RemoveModuleFromTestRoom(String moduleID, String testRoomCode)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "RemoveModuleFromTestRoom", new object[] { moduleID, testRoomCode }));
            return Result;
        }

        public static Boolean Delete(string ModuleCode)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "DeleteModule", new object[] { ModuleCode }));
            return Result;
        }

        public static Boolean HaveModuleInfo(string TreeCode, string ModuleId)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "HaveModuleInfo", new object[] { TreeCode, ModuleId }));
            return Result;
        }

        public static Boolean SaveTemlateResult(string ModuleID, string ModuleCode)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "SaveTemlateResult", new object[] { ModuleID, ModuleCode }));
            return Result; 
        }       
    }    
}
