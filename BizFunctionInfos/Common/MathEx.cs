using System;
using System.Collections.Generic;
using System.Text;

namespace BizFunctionInfos
{
    public class MathEx
    {
        public static double Max(double[] arguments)
        {
            if (arguments.Length == 0)
                throw new InvalidCastException();
            else if (arguments.Length == 1)
                return arguments[0];
            else
            {
                double r = arguments[0];
                foreach (double a in arguments)
                {
                    r = Math.Max(r, a);
                }

                return r;
            }
        }

        public static double Min(double[] arguments)
        {
            if (arguments.Length == 0)
                throw new InvalidCastException();
            else if (arguments.Length == 1)
                return arguments[0];
            else
            {
                double r = arguments[0];
                foreach (double a in arguments)
                {
                    r = Math.Min(r, a);
                }

                return r;
            }
        }
    }
}
