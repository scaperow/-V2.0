using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.CalcEngine;

namespace BizFunctionInfos
{
    [Serializable]
    public class SNSixAvergeFunctionInfoGeneral : FunctionInfo
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
                int valideCount = ArgumentConvert.ToInt(args[7]);//平均数的比例，比如：0.1,0.2 -`

                NumericalAverge numericalAverge = new NumericalAvergeByAverge();
                numericalAverge.ComparisonNumber = new AvergeNumber();
                return numericalAverge.getResultGeneral(arguments.ToArray(), percent, valideCount);
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
                return 8;
            }
        }

        public override int MinArgs
        {
            get
            {
                return 8;
            }
        }

        public override string Name
        {
            get
            {
                return "SNSixAvergeGeneral";
            }
        }
    }
}
