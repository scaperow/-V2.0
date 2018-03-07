using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.CalcEngine;

namespace BizFunctionInfos
{
    [Serializable]
    /// <summary>
    /// 当六个数中有一个数超过六个数的平均值的比例，去掉这个数，求另外五个数的平均值；
    /// 当六个数中有两个数超过六个数的平均值的比例，表示这组数不合格，给出错误提示。
    /// </summary>
    public class SNSixAvergeFunctionInfo : FunctionInfo
    {
        public override object Evaluate(object[] args)
        {
            try
            {
                List<double> arguments = new List<double>();
                arguments.Add(ArgumentConvert.ToDouble(args[0]));
                arguments.Add(ArgumentConvert.ToDouble(args[1]));
                arguments.Add(ArgumentConvert.ToDouble(args[2]));
                arguments.Add(ArgumentConvert.ToDouble(args[3]));
                arguments.Add(ArgumentConvert.ToDouble(args[4]));
                arguments.Add(ArgumentConvert.ToDouble(args[5]));

                double percent = ArgumentConvert.ToDouble(args[6]);//平均数的比例，比如：0.1,0.2 -`

                NumericalAverge numericalAverge = new NumericalAvergeByAverge();
                numericalAverge.ComparisonNumber = new AvergeNumber();
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
                return 7;
            }
        }

        public override int MinArgs
        {
            get
            {
                return 7;
            }
        }

        public override string Name
        {
            get
            {
                return "SNSixAverge";
            }
        }
    }
}
