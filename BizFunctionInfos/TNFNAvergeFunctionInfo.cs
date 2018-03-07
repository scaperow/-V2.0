using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.CalcEngine;

namespace BizFunctionInfos
{
    [Serializable]
    public class TNFNAvergeFunctionInfo: FunctionInfo
    {
        public override object Evaluate(object[] args)
        {
            if (args.Length < 3)
            {
                return CalcError.Value;
            }

            var avergeHelper = new AvergeNumber();

            var n1 = ArgumentConvert.ToDouble(args[0]);
            var n2 = ArgumentConvert.ToDouble(args[1]);
            var n3 = ArgumentConvert.ToDouble(args[2]);

            var numbers = new double[]{n1,n2,n3};
            var difference = MathEx.Max(numbers) - MathEx.Min(numbers);
            var averge = avergeHelper.getNumber(numbers);
            var off20 = averge * 0.2;
            
            if (difference <= off20)
            {
                return Math.Round(averge, 6);
            }
            else
            {
                if (args.Length < 4)
                {
                    return CalcError.Value;
                }

                var n4 = ArgumentConvert.ToDouble(args[3]);
                numbers = new double[] { n1, n2, n3, n4};
                averge = avergeHelper.getNumber(numbers);
                var max = -1d;
                var exclude = -1d;

                for (var i = 0; i < numbers.Length; i++)
                {
                    var m = Math.Abs(numbers[i] - averge);

                    if (max == -1d)
                    {
                        max = m;
                        exclude = numbers[i];
                    }
                    else
                    {
                        if (m > max)
                        {
                            max = m;
                            exclude = numbers[i];
                        }
                    }
                }

                var result = new List<double>(3);
                for (var i = 0; i <  numbers.Length; i++)
                {
                    if (numbers[i] != exclude)
                    {
                        result.Add(numbers[i]);
                    }
                }

                 return  Math.Round(avergeHelper.getNumber(result.ToArray()),6);
            }
        }

        public override int MaxArgs
        {
            get {return 4; }
        }

        public override int MinArgs
        {
            get {return 3; }
        }

        public override string Name
        {
            get {return "TNFNAverge"; }
        }
    }
}
