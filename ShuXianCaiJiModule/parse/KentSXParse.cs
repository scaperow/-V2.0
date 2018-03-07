using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShuXianCaiJiModule.parse
{
    public class KentSXParse : JZParseBase
    {
        #region JZParseBase 成员

         float Force = 0.00f;

        /// <summary>
        /// 解析数据
        /// </summary>
        StringBuilder _StringBuilder = null;

        /// <summary>
        /// 指令接受不全，保存字节数组
        /// </summary>
        byte[] Abytes;
        /// <summary>


        public float Parse(object obj)
        {
            byte[] bytes = (byte[])obj;
            if (bytes[0] == 240)
            {
                Abytes = bytes;
            }
            //当其他情况时,认为是协议中间数据,将其加到已知的头数据之后
            else
            {
                byte[] newbyte = new byte[Abytes.Length + bytes.Length];
                Abytes.CopyTo(newbyte, 0);
                bytes.CopyTo(newbyte, Abytes.Length);
                Abytes = newbyte;
            }
            int low3 = Abytes[3] & 0xf;
            int high3 = (Abytes[3] >> 4) * 10;

            int low2 = Abytes[2] & 0xf;
            int high2 = (Abytes[2] >> 4) * 10;

            int low1 = Abytes[1] & 0xf;
            int high1 = (Abytes[1] >> 4) * 10;

            Force = (float)(((Convert.ToDouble(low1 + high1) * 10000 + Convert.ToDouble(low2 + high2) * 100 + Convert.ToDouble(low3 + high3)) / 10));
            return Force;
        }
        #endregion

        #region JZParseBase 成员


        public void SetModel(SXCJModule _SXCJModule)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
