using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShuXianCaiJiModule.parse
{
    public class OKEUniversParse : JZParseBase
    {
        #region JZParseBase 成员

        float Force = 0.00f;
        byte[] Abytes;
        public float Parse(object obj)
        {
            byte[] bytes = (byte[])obj;
            try
            {
                if (bytes.Length > 0)
                {
                    //当返回二进制数据打头为'253'时,认为是协议头数据
                    if (bytes[0] == 253)
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
                    if (Abytes.Length == 9)
                    {
                        Force = float.Parse((Abytes[6] * 256 * 256 + Abytes[5] * 256 + Abytes[4]).ToString()) / 1000.00f;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return Force;
        }

        #endregion

        #region JZParseBase 成员


        public void SetModel(SXCJModule _SXCJModule)
        {
        }

        #endregion
    }
}
