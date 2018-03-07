using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    public interface IParameter
    {
        String Text
        {
            get;
        }

        String Name
        {
            get;
        }

        String Value
        {
            get;
        }
    }

    /// <summary>
    /// 当前月的第一天
    /// </summary>
    public class FirstDayInCurrentMonth : IParameter
    {
        string _Text = "当前月第一天";
        public string Text
        {
            get
            {
                return _Text;
            }
        }

        public string Name
        {
            get
            {
                return string.Format("{{ {0} }}", Text);
            }
        }

        public string Value
        {
            get
            {
                return string.Format("{0}-{1}-01 00:00:00", DateTime.Now.Year, DateTime.Now.Month);
            }
        }

        public override string ToString()
        {
            return Text;
        }
    }

    /// <summary>
    /// 当前月的最后一天
    /// </summary>
    public class LastDayInCurrentMonth : IParameter
    {
        string _Text = "当前月最后一天";
        public string Text
        {
            get
            {
                return _Text;
            }
        }

        public string Name
        {
            get
            {
                return string.Format("{{ {0} }}",Text);
            }
        }

        public string Value
        {
            get
            {
                DateTime lastDay = new DateTime(DateTime.Now.AddMonths(1).Year, DateTime.Now.AddMonths(1).Month, 1).AddDays(-1);
                return string.Format("{0}-{1}-{2} 23:59:59", lastDay.Year, lastDay.Month, lastDay.Day);
            }
        }

        public override string ToString()
        {
            return Text;
        }
    }

    public class ParameterValueManager
    {
        static Dictionary<String, IParameter> Values = new Dictionary<String, IParameter>();
        static ParameterValueManager()
        {
            FirstDayInCurrentMonth FirstDay = new FirstDayInCurrentMonth();
            Values.Add(FirstDay.Name, FirstDay);
            LastDayInCurrentMonth LastDay = new LastDayInCurrentMonth();
            Values.Add(LastDay.Name, LastDay);
        }

        public static IParameter GetValue(String Name)
        {
            if (Values.ContainsKey(Name))
                return Values[Name];
            return null;
        }
    }
}
