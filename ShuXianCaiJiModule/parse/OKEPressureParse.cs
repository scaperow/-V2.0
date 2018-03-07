using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShuXianCaiJiModule.parse
{
    public class OKEPressureParse : JZParseBase
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
                    if (bytes[0].ToString() == "165")
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
                }
                //当经过初步处理的字符串长度达到11时
                if (Abytes.Length >= 11)
                {
                    Force = (float.Parse(Abytes[7].ToString()) * 256 * 256 + float.Parse(Abytes[8].ToString()) * 256 + float.Parse(Abytes[9].ToString())) / 100.00f;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
