using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.CalcEngine;

namespace BizFunctionInfos
{
    [Serializable]
    /// <summary>
    /// 龄期函数
    /// </summary>
    /// <remarks>
    /// 两个日期之间相隔多少天
    /// </remarks>
    public class TermFunctionInfo : FunctionInfo
    {
        public override object Evaluate(object[] args)
        {
            try
            {
                DateTime start_date = ArgumentConvert.ToDateTime(args[0]);
                DateTime end_date = ArgumentConvert.ToDateTime(args[1]);

                int Days = end_date.Subtract(start_date).Days;
                if (Days != 0)
                    return Days;
            }
            catch
            {
            }

            return CalcError.Value;
        }

        public override int MaxArgs
        {
            get 
            {
                return 2;
            }
        }

        public override int MinArgs
        {
            get 
            {
                return 2;
            }
        }

        public override string Name
        {
            get 
            {
                return "Term";
            }
        }
    }
}
