using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Yqun.Common.Split
{
    public class Spliter
    {
        /// <summary>
        /// 将字符串按照分隔符分隔成字符串数组，区分大小写
        /// 比如分隔符为"T23"，字符串“AAAT23BBBT23CCCT23中国T23”， 分隔为：AAA、BBB、CCC、中国、一个空串
        /// </summary>
        /// <param name="UnSplitString">原始字符串</param>
        /// <param name="SplitUnit">分隔符，区分大小写，长度可以是多个字符</param>
        /// <returns>字符串数组</returns>
        public static string[] SplitString(string UnSplitString, string SplitUnit)
        {
            UnSplitString = UnSplitString + SplitUnit;
            ArrayList al = new ArrayList();
            int k =UnSplitString.Length;
            for (int i = 0; i < k; i++)
            {
                int j = UnSplitString.IndexOf(SplitUnit);
                if (j == -1)
                {
                    break;
                }
                string str = UnSplitString.Substring(0, j);
                al.Add(str);
                UnSplitString = UnSplitString.Substring(j + SplitUnit.Length);
            }
            if (UnSplitString == SplitUnit)
            {
                al.Add("");
            }
            string[] split = new string[al.Count];
            al.CopyTo(split);


            return split;
        }

        /// <summary>
        /// 将字符串按照分隔符分隔成字符串数组,分隔符不区分大小写
        /// 比如分隔符为"t23"，字符串“AAAT23BBBT23CCCT23中国T23”， 分隔为：AAA、BBB、CCC、中国、一个空串,
        /// </summary>
        /// <param name="UnSplitString">原始字符串</param>
        /// <param name="SplitUnit">分隔符，不区分大小写，长度可以是多个字符</param>
        /// <returns>返回值仍然区分大小写，保持数据打下写原样</returns>
        public static string[] SplitStringIgnoreCaps(string UnSplitString, string SplitUnit)
        {
            string unsplit = UnSplitString.ToLower();
            string splitunit = SplitUnit.ToLower();
            unsplit = unsplit + splitunit;
            UnSplitString = UnSplitString + SplitUnit;
            ArrayList al = new ArrayList();
            int k = unsplit.Length;
            for (int i = 0; i < k; i++)
            {
                int m = unsplit.IndexOf(splitunit);
                if (m == -1)
                {
                    break;
                }
                string str = UnSplitString.Substring(0, m);
                al.Add(str);
                string s = unsplit;
                unsplit = unsplit.Substring(m + splitunit.Length);
                UnSplitString = UnSplitString.Substring(s.IndexOf(splitunit) + splitunit.Length);
            }
            if (unsplit == SplitUnit)
            {
                al.Add("");
            }
            string[] split = new string[al.Count];
            al.CopyTo(split);


            return split;
        }

        /// <summary>
        /// 用开头字符串和结尾字符串为分隔符分隔成一个字符串数组，分隔符区分大小写
        /// 比如：S1为开头分隔符，S2为结尾分隔符，字符串：AAS1BBS2CCS1DDS2S1中国S2，分隔为：BB、DD、中国
        /// </summary>
        /// <param name="UnSplitString">原串</param>
        /// <param name="FirstSplitUnit">开头分隔符</param>
        /// <param name="LastSplitUnit">结尾分隔符</param>
        /// <returns>字符串数组</returns>
        public static string[] SplitString(string UnSplitString, string FirstSplitUnit, string LastSplitUnit)
        {
            ArrayList al = new ArrayList();
            for (int k = 0; k < UnSplitString.Length; k++)
            {
                int o = UnSplitString.IndexOf(FirstSplitUnit);
                int i = UnSplitString.IndexOf(FirstSplitUnit) + FirstSplitUnit.Length;
                int j = UnSplitString.IndexOf(LastSplitUnit);
                if ((o == -1) || (j == -1))
                {
                    break;
                }
                string str = UnSplitString.Substring(i, j - i);
                al.Add(str);
                UnSplitString = UnSplitString.Substring(j + LastSplitUnit.Length);
            }
            string[] split = new string[al.Count];
            al.CopyTo(split);


            return split;
        }

        /// <summary>
        /// 用开头字符串和结尾字符串为分隔符分隔成一个字符串数组，分隔符不区分大小写
        /// 比如：s1为开头分隔符，s2为结尾分隔符，字符串：AAS1BBS2CCS1DDS2S1中国S2，分隔为：BB、DD、中国
        /// </summary>
        /// <param name="UnSplitString">原串</param>
        /// <param name="FirstSplitUnit">开头分隔符</param>
        /// <param name="LastSplitUnit">结尾分隔符</param>
        /// <returns>字符串数组</returns>
        public static string[] SplitStringIgnoreCaps(string UnSplitString, string FirstSplitUnit, string LastSplitUnit)
        {
            ArrayList al = new ArrayList();
            string unsplit = UnSplitString.ToLower();
            string first = FirstSplitUnit.ToLower();
            string last = LastSplitUnit.ToLower();

            for (int k = 0; k < unsplit.Length; k++)
            {
                int o = unsplit.IndexOf(first);
                int i = unsplit.IndexOf(first) + first.Length;
                int j = unsplit.IndexOf(last);
                if ((o == -1) || (j == -1))
                {
                    break;
                }
                string str = UnSplitString.Substring(i, j - i);
                al.Add(str);
                unsplit = unsplit.Substring(j + last.Length);
                UnSplitString = UnSplitString.Substring(j + last.Length);
            }
            string[] split = new string[al.Count];
            al.CopyTo(split);

            return split;
        }
    }
}
