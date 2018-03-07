using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShuXianCaiJiModule.parse
{
    public class EmptyParse : JZParseBase
    {
        #region JZParseBase 成员

        StringBuilder _StringBuilder = new StringBuilder();
        float Force = 0.0f;

        public float Parse(object obj)
        {
            byte[] tempbyte = (byte[])obj;
            _StringBuilder.Append(System.Text.Encoding.UTF8.GetString(tempbyte));

            while (_StringBuilder.ToString().IndexOf("a") >= 0 && _StringBuilder.ToString().IndexOf("b") > 0 && _StringBuilder.ToString().IndexOf("a") < _StringBuilder.ToString().IndexOf("b"))
            {
                try
                {
                    Force = float.Parse(_StringBuilder.ToString().Substring(_StringBuilder.ToString().IndexOf("a") + 1, _StringBuilder.ToString().IndexOf("b") - _StringBuilder.ToString().IndexOf("a") - 1));
                    _StringBuilder.Remove(0, _StringBuilder.ToString().IndexOf("b") + 1);
                }
                catch
                {
                    _StringBuilder.Remove(0, _StringBuilder.ToString().IndexOf("b") + 1);
                }
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
