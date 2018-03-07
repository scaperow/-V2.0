using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ShuXianCaiJiModule.parse
{
    public class WHParse : JZParseBase
    {
        StringBuilder _StringBuilder = new StringBuilder();
        float Force = 0.0f;
        Logger log = new Logger(Path.Combine(Directory.GetCurrentDirectory(), "Log"));
        public float Parse(object obj)
        {
            try
            {
                byte[] tempbyte = (byte[])obj;
                _StringBuilder.Append(ASCIIEncoding.Default.GetString(tempbyte, 0, tempbyte.Length));

                if (_StringBuilder.ToString().LastIndexOf("F") < _StringBuilder.ToString().LastIndexOf("$") && _StringBuilder.ToString().LastIndexOf("F") > -1)
                {
                    if (_StringBuilder.ToString().LastIndexOf("F") != 0)
                    {
                        _StringBuilder.Remove(0, _StringBuilder.ToString().LastIndexOf("F"));
                    }
                    Force = float.Parse(_StringBuilder.ToString().Substring(1, 7));
                    _StringBuilder.Remove(0, _StringBuilder.ToString().IndexOf("$") + 1);
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.StackTrace, true, false);
                log.WriteLog(_StringBuilder.ToString(), true, false);
            }

            return Force;
        }

        #region JZParseBase 成员


        public void SetModel(SXCJModule _SXCJModule)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
