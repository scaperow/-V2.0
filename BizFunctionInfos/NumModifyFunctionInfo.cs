using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.CalcEngine;
using System.Collections;

namespace BizFunctionInfos
{
    [Serializable]
    /// <summary>
    /// 实现四舍六入五单双的公式
    /// </summary>
    public class NumModifyFunctionInfo : FunctionInfo
    {
        NumericalRound numericalRound;
        NumericalRound02 numericalRound02;
        NumericalRound05 numericalRound05;

        public NumModifyFunctionInfo()
        {
            numericalRound = new NumericalRound();
            numericalRound02 = new NumericalRound02(numericalRound);
            numericalRound05 = new NumericalRound05(numericalRound);
        }

        /// <summary>
        /// 参数说明
        /// value：修约的数值
        /// precision：0表示保留到整数，-1表示保留一位小数，-2标书保留两位小数
        /// roundRule：0表示四舍六入五单双，0.2表示0.2修约，0.5表示0.5修约
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public override object Evaluate(object[] args)
        {
            double value = 0;
            int precision = 1;
            string roundRule = "0";
            double r = 0;

            try
            {
                value = ArgumentConvert.ToDouble(args[0]);
                precision = ArgumentConvert.ToInt(args[1]);
                roundRule = ArgumentConvert.ToString(args[2]);

                switch (roundRule)
                {
                    case "0":
                        r = numericalRound.Evaluate(value, precision);
                        break;
                    case "0.2":
                        r = numericalRound02.Evaluate(value, precision);
                        break;
                    case "0.5":
                        r = numericalRound05.Evaluate(value, precision);
                        break;
                }

                return NumericalFormatProvier.Format(r, precision);
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
                return 3;
            }
        }

        public override int MinArgs
        {
            get 
            {
                return 1;
            }
        }

        public override string Name
        {
            get 
            {
                return "NumModify";
            }
        }
    }


    #region 颗粒分析的试验结果级配区属
    
    #endregion

    #region 颗粒分析的试验结果粗细程度
    
    

    #endregion
}
