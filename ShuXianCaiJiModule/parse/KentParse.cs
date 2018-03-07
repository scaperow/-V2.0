using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ShuXianCaiJiModule.parse
{
    class KentParse : JZParseBase
    {
        #region JZParseBase 成员

        /// <summary>
        /// 实时力值
        /// </summary>
        float Force = 0.00f;

        /// <summary>
        /// 解析数据
        /// </summary>
        StringBuilder _StringBuilder = new StringBuilder();
        Logger log = new Logger(Path.Combine(Directory.GetCurrentDirectory(), "Log"));
        public float Parse(object obj)
        {

            byte[] bytes = (byte[])obj;
            for (int i = 0; i < bytes.Length; i++)
            {
                _StringBuilder.Append(bytes[i].ToString("X2"));
            }
            int n = _StringBuilder.ToString().IndexOf("F0");
            if (_StringBuilder.ToString().IndexOf("F0") > 0)
            {
                _StringBuilder.Remove(0, _StringBuilder.ToString().IndexOf("F0"));
            }
            while (_StringBuilder.Length >= 30)
            {
                log.WriteLog(_StringBuilder.ToString(), false, false);
                if (_StringBuilder.Length >= 30)
                {
                    if (_StringBuilder.ToString().Substring(5, 1) == "2")
                    {
                        Force = float.Parse(_StringBuilder.ToString().Substring(6, 6)) / 100.00f;
                    }
                    else if (_StringBuilder.ToString().Substring(5, 1) == "1")
                    {
                        Force = float.Parse(_StringBuilder.ToString().Substring(6, 6)) / 10f;
                    }
                    if (_StringBuilder.ToString().Substring(4, 1) == "1")
                    {
                        Force = -Force;
                    }

                    log.WriteLog(Force.ToString(), false, false);
                    _StringBuilder.Remove(0, _StringBuilder.ToString().IndexOf("0F") + 2);
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
