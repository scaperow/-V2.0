using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.CalcEngine;

namespace BizFunctionInfos
{
    [Serializable]
    public class JZNear2NumAvgFunctionInfo : FunctionInfo
    {
        public override object Evaluate(object[] args)
        {

            Double a1 = 0;
            Double a2 = 0;
            Double a3 = 0;
            Double aTemp = 0;

            if (args[0] == null)
            {
                a1 = 0;
            }
            else if (args[0].ToString().Trim() == "" || args[0].ToString().Trim() == "/")
            {
                a1 = 0;
            }
            else
            {
                a1 = Convert.ToDouble(args[0]);
            }

            if (args[1] == null)
            {
                a2 = 0;
            }
            else if (args[1].ToString().Trim() == "" || args[1].ToString().Trim() == "/")
            {
                a2 = 0;
            }
            else
            {
                a2 = Convert.ToDouble(args[1]);
            }

            if (args[2] == null)
            {
                a3 = 0;
            }
            else if (args[2].ToString().Trim() == "" || args[2].ToString().Trim() == "/")
            {
                a3 = 0;
            }
            else
            {
                a3 = Convert.ToDouble(args[2]);
            }

            Object result = null;
            if (a1 > a2)
            {
                aTemp = a1;
                a1 = a2;
                a2 = aTemp;
            }
            if (a1 > a3)
            {
                aTemp = a1;
                a1 = a3;
                a3 = aTemp;
            }
            if (a2 > a3)
            {
                aTemp = a2;
                a2 = a3;
                a3 = aTemp;
            }
            Double at1 = 0;
            Double at2 = 0;
            Double aResult = 0;
            at1 = a2 - a1;
            at2 = a3 - a2;
            if (at1 < at2)
            {
                aResult = (a1 + a2) / 2;
            }
            else
            {
                aResult = (a2 + a3) / 2;
            }
            result = aResult.ToString("f5");
            return result;
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
                return 3;
            }
        }

        public override string Name
        {
            get
            {
                return "JZNear2NumAvg";
            }
        }
    }
}
