using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Yqun.Common.Encoder
{
    public class Arabic_Numerals_Convert
    {
        public static string ToRoman_Numerals(int num)
        {
            int[] arabic = {
            1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1
        };
            String[] roman = {
            "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV",
            "I"
        };

            int i = 0;
            StringBuilder romanNumber = new StringBuilder();

            while (num > 0)
            {
                while (num >= arabic[i])
                {
                    num = num - arabic[i];
                    romanNumber.Append(roman[i]);
                }
                i = i + 1;
            }
            return romanNumber.ToString();

        }

        public static string Chinese_Numerals(int Index)
        {
            string Number = Index.ToString();
            string[] strNum = { "一", "二", "三", "四", "五", "六", "七", "八", "九" };
            int bl = -1;
            bool ch = true;
            int len = Number.Length;
            if (len > 24)
            {
                throw new Exception("数字过大,无法转换!");
            }
            string strResult = "";
            string[] strSZ = new string[len];
            for (int i = 0; i < len; i++)
            {
                strSZ[i] = Number.Substring(i, 1);
                if (!Regex.IsMatch(strSZ[i], "^[0-9]$"))
                {
                    throw new Exception("错误,输入了非数字符号!");
                }
                if (strSZ[0] == "0" && ch)//检验首位出现零的情况
                {
                    if (i != len - 1 && strSZ[i] == "0" && strSZ[i + 1] != "0")
                        bl = i;
                    else
                        ch = false;
                }
            }

            for (int i = 0; i < len; i++)
            {
                int num = len - i;
                if (strSZ[i] != "0")
                {
                    strResult += strNum[System.Convert.ToInt32(strSZ[i]) - 1];//将阿拉伯数字转换成中文大写数字
                    //加上单位
                    if (num % 4 == 2)
                        strResult += "十";
                    if (num % 4 == 3)
                        strResult += "百";
                    if (num % 4 == 0)
                        strResult += "千";
                    if (num % 4 == 1)
                    {
                        if (num / 4 == 1)
                            strResult += "万";
                        if (num / 4 == 2)
                            strResult += "亿";
                        if (num / 4 == 3)
                            strResult += "万";
                        if (num / 4 == 4)
                            strResult += "亿";
                        if (num / 4 == 5)
                            strResult += "万";
                    }
                }
                else
                {
                    if (i > bl)
                    {
                        if ((i != len - 1 && strSZ[i + 1] != "0" && (num - 1) % 4 != 0))
                        {
                            //此处判断“0”不是出现在末尾，且下一位也不是“0”；
                            //如 10012332 ,两个零只要读一个零
                            strResult += "零";
                        }
                        if (i != len - 1 && strSZ[i + 1] != "0")
                        {
                            switch (num)
                            {
                                //此处出现的情况是如 10002332，"0"出现在万位上就应该加上一个"万".
                                case 5: strResult += "万";
                                    break;
                                case 9: strResult += "亿";
                                    break;
                                case 13: strResult += "万";
                                    break;
                            }

                        }
                        if (i != len - 1 && strSZ[i + 1] != "0" && (num - 1) % 4 == 0)
                        {
                            //此处出现的情况是如 10002332，“0”出现在万位上就应该加上一个“零”
                            strResult += "零";
                        }
                    }
                }
            }
            return strResult;
        }

        public static string Excel_Word_Numerals(int i)
        {
            char[] list = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            StringBuilder sb = new StringBuilder();
            while (i / 26 != 0)
            {
                sb.Append(list[i / 26 - 1]);
                i = i % 26;
            }
            i = i % 26;
            sb.Append(list[i]);
            return sb.ToString();
        }

        public static double ToNumerals(string Letters)
        {
            char[] list = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            double Result = 0;
            char[] letters = Letters.ToCharArray();
            Array.Reverse(letters);
            for(int i = 0;i < letters.Length; i++)
            {
                int value = Array.IndexOf(list, letters[i]) + 1;
                Result = Result + value * Math.Pow(26, i);
            }

            return Result - 1;
        }
    }
}
