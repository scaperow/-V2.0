using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.CalcEngine;

namespace BizFunctionInfos
{
    public interface IEvaluate
    {
        double Evaluate(double value, int precision);
    }

    [Serializable]
    /// <summary>
    /// 四舍六入五单双算法
    /// </summary>
    public class NumericalRound : IEvaluate
    {
        #region IEvaluate 成员

        public double Evaluate(double value, int precision)
        {
            FunctionInfo RoundUpFunction = FunctionInfo.RoundUpFunction;
            FunctionInfo RoundDownFunction = FunctionInfo.RoundDownFunction;

            double r = 0;

            double number = value * Math.Pow(10, -precision);
            string sNumber = number.ToString();
            if (sNumber.IndexOf(".") == -1 || sNumber.IndexOf(".") == sNumber.Length - 2)
                sNumber = number.ToString("0.00");

            byte digital = Convert.ToByte(sNumber.Substring(sNumber.IndexOf('.') + 1, 1));
            if (digital > 5)
            {
                r = Convert.ToDouble(RoundUpFunction.Evaluate(new object[] { number, 0 }));
            }
            else if (digital < 5)
            {
                r = Convert.ToDouble(RoundDownFunction.Evaluate(new object[] { number, 0 }));
            }
            else
            {
                byte nearDigital = Convert.ToByte(sNumber.Substring(sNumber.IndexOf('.') - 1, 1));
                double nearRightDigital = Convert.ToDouble(string.Format(".{0}",sNumber.Substring(sNumber.IndexOf('.') + 2)));
                if (nearDigital % 2 == 1 || nearRightDigital != 0)
                {
                    r = Convert.ToDouble(RoundUpFunction.Evaluate(new object[] { number, 0 }));
                }
                else
                {
                    r = Convert.ToDouble(RoundDownFunction.Evaluate(new object[] { number, 0 }));
                }
            }

            return r * Math.Pow(10, precision);
        }

        #endregion
    }

    [Serializable]
    /// <summary>
    /// 0.2单位修约
    /// </summary>
    public class NumericalRound02 : IEvaluate
    {
        IEvaluate number;

        public NumericalRound02(IEvaluate number)
        {
            this.number = number;
        }

        #region IEvaluate 成员

        public double Evaluate(double value, int precision)
        {
            double r = value * 5;
            r = number.Evaluate(r, precision + 1);
            return r / 5;
        }

        #endregion
    }

    [Serializable]
    /// <summary>
    /// 0.5单位修约
    /// </summary>
    public class NumericalRound05 : IEvaluate
    {
        IEvaluate number;

        public NumericalRound05(IEvaluate number)
        {
            this.number = number;
        }

        #region IEvaluate 成员

        public double Evaluate(double value, int precision)
        {
            double r = value * 2;
            r = number.Evaluate(r, precision + 1);
            return r / 2;
        }

        #endregion
    }

    [Serializable]
    public class NumericalFormatProvier
    {
        public static String Format(double value, int precision)
        {
            if (precision < 0)
            {
                int i = Math.Abs(precision);
                String format = "0.";
                while (i > 0)
                {
                    format = format + "0";
                    i--;
                }

                return value.ToString(format);
            }

            return value.ToString();
        }
    }
}
