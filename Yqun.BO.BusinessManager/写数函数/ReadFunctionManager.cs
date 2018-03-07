using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;

namespace Yqun.BO.BusinessManager
{
    public class ReadFunctionManager
    {
        /// <summary>
        /// 获得所有指定表单的读数函数
        /// </summary>
        /// <param name="SheetID"></param>
        /// <returns></returns>
        public List<ReadDataFunctionInfo> InitReadFunctionByModelIndex(string ModelIndex)
        {
            return null;
        }

        public ReadDataFunctionInfo InitReadFunctionByFunctionIndex(string FunctionIndex)
        {
            return null;
        }

        public bool SaveReadFunctionInfo(ReadDataFunctionInfo FunctionInfo)
        {
            return false;
        }

        public Boolean DeleteReadFunctionInfo(ReadDataFunctionInfo FunctionInfo)
        {
            return false;
        }
    }
}
