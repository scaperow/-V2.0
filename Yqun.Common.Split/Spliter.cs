using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Yqun.Common.Split
{
    public class Spliter
    {
        /// <summary>
        /// ���ַ������շָ����ָ����ַ������飬���ִ�Сд
        /// ����ָ���Ϊ"T23"���ַ�����AAAT23BBBT23CCCT23�й�T23���� �ָ�Ϊ��AAA��BBB��CCC���й���һ���մ�
        /// </summary>
        /// <param name="UnSplitString">ԭʼ�ַ���</param>
        /// <param name="SplitUnit">�ָ��������ִ�Сд�����ȿ����Ƕ���ַ�</param>
        /// <returns>�ַ�������</returns>
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
        /// ���ַ������շָ����ָ����ַ�������,�ָ��������ִ�Сд
        /// ����ָ���Ϊ"t23"���ַ�����AAAT23BBBT23CCCT23�й�T23���� �ָ�Ϊ��AAA��BBB��CCC���й���һ���մ�,
        /// </summary>
        /// <param name="UnSplitString">ԭʼ�ַ���</param>
        /// <param name="SplitUnit">�ָ����������ִ�Сд�����ȿ����Ƕ���ַ�</param>
        /// <returns>����ֵ��Ȼ���ִ�Сд���������ݴ���дԭ��</returns>
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
        /// �ÿ�ͷ�ַ����ͽ�β�ַ���Ϊ�ָ����ָ���һ���ַ������飬�ָ������ִ�Сд
        /// ���磺S1Ϊ��ͷ�ָ�����S2Ϊ��β�ָ������ַ�����AAS1BBS2CCS1DDS2S1�й�S2���ָ�Ϊ��BB��DD���й�
        /// </summary>
        /// <param name="UnSplitString">ԭ��</param>
        /// <param name="FirstSplitUnit">��ͷ�ָ���</param>
        /// <param name="LastSplitUnit">��β�ָ���</param>
        /// <returns>�ַ�������</returns>
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
        /// �ÿ�ͷ�ַ����ͽ�β�ַ���Ϊ�ָ����ָ���һ���ַ������飬�ָ��������ִ�Сд
        /// ���磺s1Ϊ��ͷ�ָ�����s2Ϊ��β�ָ������ַ�����AAS1BBS2CCS1DDS2S1�й�S2���ָ�Ϊ��BB��DD���й�
        /// </summary>
        /// <param name="UnSplitString">ԭ��</param>
        /// <param name="FirstSplitUnit">��ͷ�ָ���</param>
        /// <param name="LastSplitUnit">��β�ָ���</param>
        /// <returns>�ַ�������</returns>
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
