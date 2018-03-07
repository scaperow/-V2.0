using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.CalcEngine;

namespace BizFunctionInfos
{
    [Serializable]
    public class WJJhreeAvergeFunctionInfo : FunctionInfo
    {
        public override object Evaluate(object[] args)
        {
            try
            {
                List<double> arguments = new List<double>();
                arguments.Add(ArgumentConvert.ToDouble(args[0]));
                arguments.Add(ArgumentConvert.ToDouble(args[1]));
                arguments.Add(ArgumentConvert.ToDouble(args[2]));

                double percent = ArgumentConvert.ToDouble(args[3]);//平均数的比例，比如：0.1,0.2

                NumericalAverge numericalAverge = new NumericalAvergeByWJJTongMedian();
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
                return "WJJhreeAverge";
            }
        }
    }
}
