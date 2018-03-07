using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.CalcEngine;

namespace BizFunctionInfos
{
    [Serializable]
    public class HTHSixTeenAvergeFunctionInfo : FunctionInfo
    {
        public override object Evaluate(object[] args)
        {
            try
            {
                double[] arguments = new double[16];
                arguments[0] = ArgumentConvert.ToDouble(args[0]);
                arguments[1] = ArgumentConvert.ToDouble(args[1]);
                arguments[2] = ArgumentConvert.ToDouble(args[2]);
                arguments[3] = ArgumentConvert.ToDouble(args[3]);
                arguments[4] = ArgumentConvert.ToDouble(args[4]);
                arguments[5] = ArgumentConvert.ToDouble(args[5]);
                arguments[6] = ArgumentConvert.ToDouble(args[6]);
                arguments[7] = ArgumentConvert.ToDouble(args[7]);
                arguments[8] = ArgumentConvert.ToDouble(args[8]);
                arguments[9] = ArgumentConvert.ToDouble(args[9]);
                arguments[10] = ArgumentConvert.ToDouble(args[10]);
                arguments[11] = ArgumentConvert.ToDouble(args[11]);
                arguments[12] = ArgumentConvert.ToDouble(args[12]);
                arguments[13] = ArgumentConvert.ToDouble(args[13]);
                arguments[14] = ArgumentConvert.ToDouble(args[14]);
                arguments[15] = ArgumentConvert.ToDouble(args[15]);

                Array.Sort(arguments);

                double sum = 0;
                for (int i = 3; i < 13; i++)
                {
                    sum = sum + arguments[i];
                }

                return sum / 10;
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
                return 16;
            }
        }

        public override int MinArgs
        {
            get
            {
                return 16;
            }
        }

        public override string Name
        {
            get
            {
                return "HTHSixTeenAverge";
            }
        }
    }
}
