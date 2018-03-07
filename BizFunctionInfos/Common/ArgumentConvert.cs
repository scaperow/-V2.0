using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.CalcEngine;

namespace BizFunctionInfos
{
    public class ArgumentConvert
    {
        public static double ToDouble(object value)
        {
            if (value is CalcReference)
            {
                CalcReference cr = (CalcReference)value;
                value = cr.GetValue(cr.Row, cr.Column);
            }

            return CalcConvert.ToDouble(value);
        }

        public static int ToInt(object value)
        {
            if (value is CalcReference)
            {
                CalcReference cr = (CalcReference)value;
                value = cr.GetValue(cr.Row, cr.Column);
            }

            return CalcConvert.ToInt(value);
        }

        public static String ToString(object value)
        {
            if (value is CalcReference)
            {
                CalcReference cr = (CalcReference)value;
                value = cr.GetValue(cr.Row, cr.Column);
            }

            return CalcConvert.ToString(value);
        }

        public static DateTime ToDateTime(object value)
        {
            if (value is CalcReference)
            {
                CalcReference cr = (CalcReference)value;
                value = cr.GetValue(cr.Row, cr.Column);
            }

            return CalcConvert.ToDateTime(value);
        }
    }
}
