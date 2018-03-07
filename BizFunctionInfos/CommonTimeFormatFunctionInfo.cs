using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.CalcEngine;

namespace BizFunctionInfos
{
    /// <summary>
    /// 获取两个日期之间的差值，并以指定的格式输出
    /// </summary>
     [Serializable]
    public class CommonTimeFormatFunctionInfo : FunctionInfo
    {
        public override object Evaluate(object[] args)
        {
            try
            {
                DateTime start_date = ArgumentConvert.ToDateTime(args[0]);
                DateTime end_date = ArgumentConvert.ToDateTime(args[1]);
                String type = ArgumentConvert.ToString(args[2]).Trim().ToUpper();
                //int hasDecimal = CalcConvert.ToInt(args[3]);

                double time = 0;
                TimeSpan ts = end_date.Subtract(start_date);
                switch (type)
                {
                    case "D"://天数
                        time = ts.TotalDays;
                        break;
                    case "H"://小时数
                        time = (ts.TotalHours < 0 ? ts.TotalHours + 24 : ts.TotalHours);
                        break;
                    case "M"://分钟数
                        time = (ts.TotalMinutes < 0 ? ts.TotalMinutes + 24 * 60 : ts.TotalMinutes);
                        break;
                    case "S"://秒数
                        time = (ts.TotalSeconds < 0 ? ts.TotalSeconds + 24 * 3600 : ts.TotalSeconds);
                        break;
                }

                if (time != 0)
                    return time;
                return DBNull.Value;
            }
            catch
            {
                throw new InvalidCastException();
            }
        }

        public override int MaxArgs
        {
            get
            { 
                return 4; 
            }
        }

        public override int MinArgs
        {
            get
            {
                return 3;
            }
        }

        public override string Name
        {
            get
            {
                return "CommonTimeFormat";
            }
        }
    }
}
