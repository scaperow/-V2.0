using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.CalcEngine;

namespace BizFunctionInfos
{
    [Serializable]
    /// <summary>
    /// 当三个数中有一个数超过三个数的平均值的某个比例，去掉这个数，求另外两个数的平均值；
    /// 当三个数中有两个数超过三个数的平均值的某个比例，表示这组数不合格，给出错误提示。
    /// </summary>
    public class SNThreeAvergeFunctionInfo : FunctionInfo
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

                NumericalAverge numericalAverge = new NumericalAvergeByAverge();
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
                return "SNThreeAverge";
            }
        }
    }
}
