using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShuXianCaiJiModule.parse
{
    public class KentADParse : JZParseBase
    {
        #region JZParseBase 成员
        StringBuilder _StringBuilder = new StringBuilder();
        float Force = 0.00f;
        SXCJModule _SXCJModule = null;
        int num = 1;

        public float Parse(object obj)
        {
            byte[] bytes = (byte[])obj;
            for (int i = 0; i < bytes.Length; i++)
            {
                _StringBuilder.Append(bytes[i].ToString("X2"));
            }
            if (_StringBuilder.ToString().IndexOf("F0") > 0)
            {
                _StringBuilder.Remove(0, _StringBuilder.ToString().IndexOf("F0"));
            }
            while (_StringBuilder.Length >= 22)
            {
                Force = float.Parse(_StringBuilder.ToString().Substring(8, 6));
                Force = (Force / 65536 * _SXCJModule.SpecialSetting.BDValue)/num- _SXCJModule.SpecialSetting.ZeroParameters ;
                _StringBuilder.Remove(0, _StringBuilder.ToString().IndexOf("0F") + 2);
            }
            return Force;
        }

        public void SetModel(SXCJModule sxckModel)
        {
            _SXCJModule = sxckModel;
            switch (_SXCJModule.SpecialSetting.PointNum)
            {
                case 0:
                    {
                        num = 1;
                        break;
                    }
                case 1:
                    {
                        num = 10;
                        break;
                    }
                case 2:
                    {
                        num = 100;
                        break;
                    }
                case 3:
                    {
                        num = 1000;
                        break;
                    }
            }
        }

        #endregion
    }
}
