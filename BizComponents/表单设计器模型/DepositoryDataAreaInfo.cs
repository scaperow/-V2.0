using System;
using System.Collections.Generic;
using System.Text;
using Yqun.Services;
using System.Data;
using BizCommon;

namespace BizComponents
{
    public class DepositoryDataAreaInfo
    {
        public static Boolean New(string SheetID, TableDefineInfo TableInfo)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "NewDataAreaInfo", new object[] { SheetID, TableInfo }));
            return Result;
        }

        public static Boolean Delete(string SheetID, TableDefineInfo TableInfo)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "DeleteDataAreaInfo", new object[] { SheetID, TableInfo }));
            return Result;
        }

        public static Boolean Update(string SheetID, TableDefineInfo TableInfo)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateDataAreaInfo", new object[] { SheetID, TableInfo }));
            return Result;
        }
    }
}
