using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.CalcEngine;

namespace BizFunctionInfos
{
    [Serializable]
    /// <summary>
    /// 凝结时间函数
    /// </summary>
    public class NJThreeAvergeFunctionInfo : FunctionInfo
    {
        public override object Evaluate(object[] args)
        {
            try
            {
                List<double> arguments = new List<double>();
                arguments.Add(ArgumentConvert.ToDouble(args[0]));
                arguments.Add(ArgumentConvert.ToDouble(args[1]));
                arguments.Add(ArgumentConvert.ToDouble(args[2]));
          

                double percent = ArgumentConvert.ToDouble(args[3]);//数值之间的差值，比如：27,30

                NumericalAverge numericalAverge = new NumericalAvergeByNJMedian();
                return numericalAverge.getResult(arguments.ToArray(), percent);

                
            }
            catch
            {
                return CalcError.Value;
            }
        }

        public override int MaxArgs
        {
            get
            {
                return 4;
            }
        }

        public override int MinArgs
        {
            get
            {
                return 4;
            }
        }

        public override string Name
        {
            get
            {
                return "NJThreeAverge";
            }
        }
    }
}
