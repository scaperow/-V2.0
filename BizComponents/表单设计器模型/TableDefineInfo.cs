using System;
using System.Text;
using System.Data;
using Yqun.Services;
using BizCommon;
using Yqun.Common.ContextCache;
using Yqun.Bases.ClassBases;
using System.Collections.Generic;

namespace BizComponents
{
    public class DepositoryTableDefineInfo
    {
        public static List<TableDefineInfo> GetTableDefineInfos()
        {
            List<TableDefineInfo> Infos = Agent.CallService("Yqun.BO.BusinessManager.dll", "GetTableDefineInfos", new object[] { }) as List<TableDefineInfo>;
            return Infos;
        }

        public static TableDefineInfo GetTableDefineInfo(string TableName)
        {
            TableDefineInfo Info = Agent.CallService("Yqun.BO.BusinessManager.dll", "GetTableDefineInfo", new object[] { TableName }) as TableDefineInfo;
            return Info;
        }

        public static bool New(TableDefineInfo Info)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "NewTableDefineInfo", new object[] { Info }));
            return Result;
        }

        public static Boolean Delete(TableDefineInfo Info)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "DeleteTableDefineInfo", new object[] { Info }));
            return Result;
        }

        public static Boolean Update(TableDefineInfo Info)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateTableDefineInfo", new object[] { Info }));
            return Result;
        }
    }
}
