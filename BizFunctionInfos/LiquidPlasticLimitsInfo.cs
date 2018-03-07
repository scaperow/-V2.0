using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.CalcEngine;

namespace BizFunctionInfos
{
    [Serializable]
    /// <summary>
    /// 液塑限
    /// </summary>
    public class LiquidPlasticLimitsInfo : FunctionInfo
    {
        NumModifyFunctionInfo NumModifyFunction = new NumModifyFunctionInfo();

        public override object Evaluate(object[] args)
        {
            try
            {
                double[] logvalues = new double[7];
                logvalues[0] = Math.Log10(ArgumentConvert.ToDouble(args[0]));
                logvalues[1] = Math.Log10(ArgumentConvert.ToDouble(args[1]));
                logvalues[2] = Math.Log10(ArgumentConvert.ToDouble(args[2]));
                logvalues[3] = Math.Log10(ArgumentConvert.ToDouble(args[3]));
                logvalues[4] = Math.Log10(ArgumentConvert.ToDouble(args[4]));
                logvalues[5] = Math.Log10(ArgumentConvert.ToDouble(args[5]));
                logvalues[6] = Math.Log10(ArgumentConvert.ToDouble(args[6]));

                //y = bx+a
                double a = (logvalues[3] - logvalues[1]) / (logvalues[2] - logvalues[0]);
                double b = logvalues[1] - a * logvalues[0];

                //y = mx+n
                double m = (logvalues[5] - logvalues[1]) / (logvalues[4] - logvalues[0]);
                double n = logvalues[1] - m * logvalues[0];

                double logvalue2 = Math.Log10(2);
                double x1 = (logvalue2 - b) / a;
                double x2 = (logvalue2 - n) / m;

                if (Math.Abs(Math.Pow(10, x1) - Math.Pow(10, x2)) > 2)
                    return CalcError.Value;
                else
                {
                    //y = gx+h
                    double cv = x1 + (x2 - x1) / 2;
                    double g = (logvalue2 - logvalues[1]) / (cv - logvalues[0]);
                    double h = logvalues[1] - g * logvalues[0];
                    double z = (logvalues[6] - h) / g;
                    double dz = Math.Pow(10, z);
                    return NumModifyFunction.Evaluate(new object[] { dz, -1, 0 });
                }
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
                return "LiquidPlasticLimits";
            }
        }
    }
}
