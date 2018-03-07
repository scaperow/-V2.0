using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using Yqun.Services;

namespace BizComponents
{
    public class DepositoryReadFunction
    {
        /// <summary>
        /// 获得指定模板的读数函数
        /// </summary>
        /// <param name="SheetID"></param>
        /// <returns></returns>
        public static List<ReadDataFunctionInfo> InitByModelIndex(string ModelIndex)
        {
            List<ReadDataFunctionInfo> ReadDataFunctionInfos = Agent.CallService("Yqun.BO.BusinessManager.dll", "InitReadFunctionByModelIndex", new object[] { ModelIndex }) as List<ReadDataFunctionInfo>;
            return ReadDataFunctionInfos;
        }

        public static ReadDataFunctionInfo InitByFunctionIndex(string FunctionIndex)
        {
            ReadDataFunctionInfo functionInfo = Agent.CallService("Yqun.BO.BusinessManager.dll", "InitReadFunctionByFunctionIndex", new object[] { FunctionIndex }) as ReadDataFunctionInfo;
            return functionInfo;
        }

        public static bool Save(ReadDataFunctionInfo FunctionInfo)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "SaveReadFunctionInfo", new object[] { FunctionInfo }));
            return Result;
        }

        public static Boolean Delete(ReadDataFunctionInfo FunctionInfo)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "DeleteReadFunctionInfo", new object[] { FunctionInfo }));
            return Result;
        }
    }
}
