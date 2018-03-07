using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Yqun.Common.Encoder
{
    public class Convert
    {
        #region static

        /// <summary>
        /// 将字符串按照特定字符编码集生成Base64码
        /// </summary>
        /// <param name="NotBase64String">字符串</param>
        /// <param name="CharSet">字符编码集</param>
        /// <returns>Base64码</returns>
        public static string ToBase64(string NotBase64String, string CharSet)
        {
            //添加代码
            byte[] binaryData = System.Text.Encoding.GetEncoding(CharSet).GetBytes(NotBase64String);
            string tobase = System.Convert.ToBase64String(binaryData);
            return tobase;
        }

        /// <summary>
        /// 将Base64码根据特定字符编码集解码为普通字符串
        /// </summary>
        /// <param name="Base64String">Base64码</param>
        /// <param name="CharSet">字符编码集</param>
        /// <returns>解码后的字符串</returns>
        public static string Frombase64(string Base64String, string CharSet)
        {
            //添加代码
            byte[] binaryData = System.Convert.FromBase64String(Base64String);
            string formbase = System.Text.Encoding.GetEncoding(CharSet).GetString(binaryData);
            return formbase;
        }

        /// <summary>
        /// 颜色转换为Int值
        /// </summary>
        /// <param name="InputColor">颜色</param>
        /// <returns>用Int表示的颜色</returns>
        public static int ColorToInt(System.Drawing.Color InputColor)
        {
            //添加代码
            return InputColor.ToArgb();
        }

        /// <summary>
        /// 将Int值转换为颜色
        /// </summary>
        /// <param name="InputColor">用Int表示的颜色</param>
        /// <returns>转换后的颜色</returns>
        public static System.Drawing.Color ColorFromInt(int InputColor)
        {
            //添加代码
            return System.Drawing.Color.FromArgb(InputColor);
        }

        /// <summary>
        /// 将整数转化为十六进制字符串
        /// </summary>
        /// <param name="IntValue">整数</param>
        /// <returns>十六进制字符串</returns>
        public static string FromIntToHex(int IntValue) 
        {
            //添加代码
            Int16 i = System.Convert.ToInt16(IntValue);
            string formint = System.Convert.ToString(i, 16);
            return formint;
        }

        /// <summary>
        /// 将十六进制值转换为整数
        /// </summary>
        /// <param name="HexValue">十六进制字符串</param>
        /// <returns>整数</returns>
        public static int FromhexToInt(string HexValue) 
        {
            //添加代码
            int formto = System.Convert.ToInt32(HexValue,16);
            return formto;
        }

        #endregion

        /// <summary>
        /// 获取一串汉字的拼音声母
        /// </summary>
        /// <param name="chinese">Unicode格式的汉字字符串</param>
        /// <returns>拼音声母字符串</returns>
        public static String ConvertSpin(String chinese)
        {
            char[] buffer = new char[chinese.Length];
            for (int i = 0; i < chinese.Length; i++)
            {
                buffer[i] = ConvertSpin(chinese[i]);
            }
            return new String(buffer);
        }

        /// <summary>
        /// 获取一个汉字的拼音声母
        /// </summary>
        /// <param name="chinese">Unicode格式的一个汉字</param>
        /// <returns>汉字的声母</returns>
        public static char ConvertSpin(Char chinese)
        {
            Encoding gb2312 = Encoding.GetEncoding("GB2312");
            Encoding unicode = Encoding.Unicode;

            byte[] unicodeBytes = unicode.GetBytes(new Char[] { chinese });
            byte[] asciiBytes = Encoding.Convert(unicode, gb2312, unicodeBytes);

            // 计算该汉字的GB-2312编码
            int n = (int)asciiBytes[0] << 8;

            if (asciiBytes.GetUpperBound(0) == 1)
            {
                n += (int)asciiBytes[1];
            }

            // 根据汉字区域码获取拼音声母
            if (In(0xB0A1, 0xB0C4, n)) return 'a';
            if (In(0XB0C5, 0XB2C0, n)) return 'b';
            if (In(0xB2C1, 0xB4ED, n)) return 'c';
            if (In(0xB4EE, 0xB6E9, n)) return 'd';
            if (In(0xB6EA, 0xB7A1, n)) return 'e';
            if (In(0xB7A2, 0xB8c0, n)) return 'f';
            if (In(0xB8C1, 0xB9FD, n)) return 'g';
            if (In(0xB9FE, 0xBBF6, n)) return 'h';
            if (In(0xBBF7, 0xBFA5, n)) return 'j';
            if (In(0xBFA6, 0xC0AB, n)) return 'k';
            if (In(0xC0AC, 0xC2E7, n)) return 'l';
            if (In(0xC2E8, 0xC4C2, n)) return 'm';
            if (In(0xC4C3, 0xC5B5, n)) return 'n';
            if (In(0xC5B6, 0xC5BD, n)) return 'o';
            if (In(0xC5BE, 0xC6D9, n)) return 'p';
            if (In(0xC6DA, 0xC8BA, n)) return 'q';
            if (In(0xC8BB, 0xC8F5, n)) return 'r';
            if (In(0xC8F6, 0xCBF0, n)) return 's';
            if (In(0xCBFA, 0xCDD9, n)) return 't';
            if (In(0xCDDA, 0xCEF3, n)) return 'w';
            if (In(0xCEF4, 0xD188, n)) return 'x';
            if (In(0xD1B9, 0xD4D0, n)) return 'y';
            if (In(0xD4D1, 0xD7F9, n)) return 'z';
            return chinese;
        }

        private static bool In(int Lp, int Hp, int Value)
        {
            return ((Value <= Hp) && (Value >= Lp));
        }
    }

}
