using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Yqun.Services;
using BizCommon;
using Yqun.Common.ContextCache;
using Yqun.Bases.ClassBases;

namespace BizComponents
{
    public class DepositoryFieldDefineInfo
    {
        public static List<FieldDefineInfo> GetFieldDefineInfos(TableDefineInfo TableInfo)
        {
            List<FieldDefineInfo> FieldInfos = Agent.CallService("Yqun.BO.BusinessManager.dll", "GetFieldDefineInfos", new object[] { TableInfo }) as List<FieldDefineInfo>;
            return FieldInfos;
        }

        public static bool New(FieldDefineInfo Info)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "NewFieldDefineInfo", new object[] { Info }));
            return Result;
        }

        public static Boolean Delete(FieldDefineInfo Info)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "DeleteFieldDefineInfo", new object[] { Info }));
            return Result;
        }

        public static Boolean Update(FieldDefineInfo Info)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateFieldDefineInfo", new object[] { Info }));
            return Result;
        }
    }
}
