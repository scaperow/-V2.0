using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Yqun.Services;
using BizCommon;

namespace BizComponents
{
    public class DepositoryModuleUserFields
    {
        public static List<ModuleField> GetModuleFields(String ModuleID, String ModuleCode)
        {
            List<ModuleField> Fields = Agent.CallLocalService("Yqun.BO.BusinessManager.dll", "GetModuleFields", new object[] { ModuleID, ModuleCode }) as List<ModuleField>; ;
            return Fields;
        }

        public static List<ModuleField> GetModuleUserFields(String ModuleID, String ModuleCode)
        {
            List<ModuleField> Fields = Agent.CallLocalService("Yqun.BO.BusinessManager.dll", "GetModuleUserFields", new object[] { ModuleID, ModuleCode }) as List<ModuleField>; ;
            return Fields;
        }

        public static Boolean NewModuleUserField(String ModuleID, String ModuleCode, ModuleField moduleField)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallLocalService("Yqun.BO.BusinessManager.dll", "NewModuleUserField", new object[] { ModuleID, ModuleCode, moduleField }));
            return Result;
        }

        public static Boolean DeleteModuleUserField(String ModuleID, String ModuleCode, String Index)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallLocalService("Yqun.BO.BusinessManager.dll", "DeleteModuleUserField", new object[] { ModuleID, ModuleCode, Index }));
            return Result;
        }

        public static Boolean UpdateModuleUserField(String ModuleID, String ModuleCode, ModuleField moduleField)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallLocalService("Yqun.BO.BusinessManager.dll", "UpdateModuleUserField", new object[] { ModuleID, ModuleCode, moduleField }));
            return Result;
        }

        public static Boolean UpdateModuleUserField(String ModuleID, String ModuleCode, String[] Index, float[] Width)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallLocalService("Yqun.BO.BusinessManager.dll", "UpdateModuleUserField", new object[] { ModuleID, ModuleCode, Index, Width }));
            return Result;
        }
    }
}
