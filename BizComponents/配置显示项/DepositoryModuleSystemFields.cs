using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BizCommon;
using Yqun.Services;

namespace BizComponents
{
    public class DepositoryModuleSystemFields
    {
        public static List<ModuleField> GetModuleSystemFields(String ModuleID, String ModuleCode)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetModuleSystemFields", new object[] { ModuleID, ModuleCode }) as List<ModuleField>; ;
        }

        public static Boolean NewModuleSystemField(String ModuleID, String ModuleCode, ModuleField moduleField)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "NewModuleSystemField", new object[] { ModuleID, ModuleCode, moduleField }));
        }

        public static Boolean DeleteModuleSystemField(String ModuleID, String ModuleCode, String Index)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "DeleteModuleSystemField", new object[] { ModuleID, ModuleCode, Index }));
        }

        public static Boolean UpdateModuleSystemField(String ModuleID, String ModuleCode, ModuleField moduleField)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateModuleSystemField", new object[] { ModuleID, ModuleCode, moduleField }));
        }

        public static Boolean RemoveModuleSystemFields(String moduleIndex)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "RemoveModuleSystemFields", new object[] { moduleIndex }));
        }

        public static Boolean RemoveModuleSystemFields(String moduleIndex, String TableName)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "RemoveModuleSystemFields", new object[] { moduleIndex, TableName }));
        }
    }
}
