using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCommon
{
    public class UnitConverter
    {
        static decimal Ratio = 3.9370080011160063M;
        public static decimal MillimeterToCentiInch(decimal mm)
        {
            decimal temp = mm * Ratio;
            return Math.Round(temp);
        }

        public static decimal CentiInchToMillimeter(decimal inch)
        {
            decimal temp = inch / Ratio;
            return Math.Round(temp);
        }
    }
}
