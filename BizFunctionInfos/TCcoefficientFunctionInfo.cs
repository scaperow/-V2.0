using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.CalcEngine;

namespace BizFunctionInfos
{
    [Serializable]
    /// <summary>
    /// 温度修正系数
    /// </summary>
    public class TCcoefficientFunctionInfo : FunctionInfo
    {
        public override object Evaluate(object[] args)
        {
            int args0 = ArgumentConvert.ToInt(args[0]);
            double value = 0;
            switch (args0)
            {
                case 15:
                    value = 0.002;
                    break;
                case 16:
                case 17:
                    value = 0.003;
                    break;
                case 18:
                case 19:
                    value = 0.004;
                    break;
                case 20:
                case 21:
                    value = 0.005;
                    break;
                case 22:
                case 23:
                    value = 0.006;
                    break;
                case 24:
                    value = 0.007;
                    break;
                case 25:
                    value = 0.008;
                    break;
            }

            if (value != 0)
                return value;
            return CalcError.Value;
        }

        public override int MaxArgs
        {
            get 
            {
                return 1;
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
                return "TCcoefficient";
            }
        }
    }
}
