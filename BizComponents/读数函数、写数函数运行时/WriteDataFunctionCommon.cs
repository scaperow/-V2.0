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
    public class DepositoryWriteFunction
    {
        /// <summary>
        /// 获得指定模板的写数函数
        /// </summary>
        /// <param name="SheetID"></param>
        /// <returns></returns>
        public static List<WriteDataFunctionInfo> InitByModelIndex(string ModelIndex)
        {
            List<WriteDataFunctionInfo> WriteDataFunctionInfos = Agent.CallService("Yqun.BO.BusinessManager.dll", "InitWriteFunctionByModelIndex", new object[] { ModelIndex }) as List<WriteDataFunctionInfo>;
            return WriteDataFunctionInfos;
        }

        public static WriteDataFunctionInfo InitByFunctionIndex(string FunctionIndex)
        {
            WriteDataFunctionInfo functionInfo = Agent.CallService("Yqun.BO.BusinessManager.dll", "InitWriteFunctionByFunctionIndex", new object[] { FunctionIndex }) as WriteDataFunctionInfo; ;
            return functionInfo;
        }

        public static bool Save(WriteDataFunctionInfo FunctionInfo)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "SaveWriteFunctionInfo", new object[] { FunctionInfo }));
            return Result;
        }

        public static Boolean Delete(WriteDataFunctionInfo FunctionInfo)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "DeleteWriteFunctionInfo", new object[] { FunctionInfo }));
            return Result;
        }
    }
}
