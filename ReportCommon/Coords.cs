using System;
using System.Collections.Generic;
using System.Text;
using Yqun.Common.Encoder;

namespace ReportCommon
{
    /// <summary>
    /// 坐标拆分类
    /// </summary>
    public class Coords
    {
        public static string[] Split(string Range)
        {
            int DigitIndex = Range.Length - 1;
            char[] chars = Range.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (Char.IsDigit(chars[i]))
                {
                    DigitIndex = i;
                    break;
                }
            }

            List<string> Parts = new List<string>();
            Parts.Add(Range.Substring(0, DigitIndex));
            Parts.Add(Range.Substring(DigitIndex));
            return Parts.ToArray();
        }

        public static int[] ConvertColumn_Row(string Range)
        {
            int DigitIndex = Range.Length - 1;
            char[] chars = Range.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (Char.IsDigit(chars[i]))
                {
                    DigitIndex = i;
                    break;
                }
            }

            List<int> Parts = new List<int>();
            Parts.Add(System.Convert.ToInt32(Arabic_Numerals_Convert.ToNumerals(Range.Substring(0, DigitIndex))));
            Parts.Add(System.Convert.ToInt32(Range.Substring(DigitIndex)) - 1);
            return Parts.ToArray();
        }

        public static string GetColumn_Row(int Row, int Column)
        {
            return Arabic_Numerals_Convert.Excel_Word_Numerals(Column) + (Row + 1).ToString();
        }
    }
}
