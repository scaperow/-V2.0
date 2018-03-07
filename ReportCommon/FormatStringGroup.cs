using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCommon
{
    /// <summary>
    /// 一类格式字符串,例如数字类，货币类等等
    /// </summary>
    [Serializable]
    public class FormatStringGroup
    {
        String _Example = "";
        public String Example
        {
            get
            {
                return _Example;
            }
            set
            {
                _Example = value;
            }
        }

        List<FormatInfo> _FormatInfos = new List<FormatInfo>();
        public List<FormatInfo> FormatInfos
        {
            get
            {
                return _FormatInfos;
            }
        }
    }
}
