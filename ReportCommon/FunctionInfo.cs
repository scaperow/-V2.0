using System;

namespace ReportCommon
{
    /// <summary>
    /// 函数信息类
    /// </summary>
    [Serializable]
    public class FunctionInfo
    {
        [NonSerialized]
        readonly public static FunctionInfo Sum = new SumFunctionInfo();
        [NonSerialized]
        readonly public static FunctionInfo Avg = new AvgFunctionInfo();
        [NonSerialized]
        readonly public static FunctionInfo Max = new MaxFunctionInfo();
        [NonSerialized]
        readonly public static FunctionInfo Min = new MinFunctionInfo();
        [NonSerialized]
        readonly public static FunctionInfo Count = new CountFunctionInfo();
        [NonSerialized]
        readonly public static FunctionInfo None = new NoneFunctionInfo();

        public FunctionInfo()
        {
        }

        protected String _Name = "";
        public String Name
        {
            get
            {
                return _Name;
            }
        }

        protected String _Text = "";
        public String Text
        {
            get
            {
                return _Text;
            }
        }

        public virtual object Eval(object[] values)
        {
            return 0;
        }

        public override string ToString()
        {
            return Text;
        }

        public override bool Equals(object obj)
        {
            FunctionInfo function = obj as FunctionInfo;
            if (function != null)
            {
                return string.Compare(this.Name, function.Name) == 0;
            }

            return false;
        }
    }

    [Serializable]
    public class SumFunctionInfo : FunctionInfo
    {
        public SumFunctionInfo()
        {
            this._Name = "Sum";
            this._Text = "求和";
        }

        public override object Eval(object[] values)
        {
            double sum = 0;
            if (values == null)
                return 0;
            else
            {
                double r = 0;
                foreach (object obj in values)
                {
                    if (obj == null)
                        continue;
                    double.TryParse(obj.ToString(), out r);
                    sum = sum + r;
                }

                return sum;
            }
        }
    }

    [Serializable]
    public class AvgFunctionInfo : FunctionInfo
    {
        public AvgFunctionInfo()
        {
            this._Name = "Avg";
            this._Text = "平均";
        }

        public override object Eval(object[] values)
        {
            double sum = 0;
            if (values == null)
                return 0;
            else
            {
                double r = 0;
                foreach (object obj in values)
                {
                    if (obj == null)
                        continue;
                    double.TryParse(obj.ToString(), out r);
                    sum = sum + r;
                }

                return values.Length != 0 ? sum / values.Length : 0;
            }
        }
    }

    [Serializable]
    public class MaxFunctionInfo : FunctionInfo
    {
        public MaxFunctionInfo()
        {
            this._Name = "Max";
            this._Text = "最大值";
        }

        public override object Eval(object[] values)
        {
            double MaxValue = double.MinValue;
            if (values == null)
                return 0;
            else
            {
                double r = MaxValue;
                foreach (object obj in values)
                {
                    if (obj == null)
                        continue;
                    double.TryParse(obj.ToString(), out r);
                    MaxValue = Math.Max(MaxValue, r);
                }

                return MaxValue;
            }
        }
    }

    [Serializable]
    public class MinFunctionInfo : FunctionInfo
    {
        public MinFunctionInfo()
        {
            this._Name = "Min";
            this._Text = "最小值";
        }

        public override object Eval(object[] values)
        {
            double MinValue = double.MaxValue;
            if (values == null)
                return 0;
            else
            {
                double r = MinValue;
                foreach (object obj in values)
                {
                    if (obj == null)
                        continue;
                    double.TryParse(obj.ToString(), out r);
                    MinValue = Math.Min(MinValue, r);
                }

                return MinValue;
            }
        }
    }

    [Serializable]
    public class CountFunctionInfo : FunctionInfo
    {
        public CountFunctionInfo()
        {
            this._Name = "Count";
            this._Text = "个数";
        }

        public override object Eval(object[] values)
        {
            if (values == null)
                return 0;
            else
            {
                return values.Length;
            }
        }
    }

    [Serializable]
    public class NoneFunctionInfo : FunctionInfo
    {
        public NoneFunctionInfo()
        {
            this._Name = "None";
            this._Text = "无";
        }

        public override object Eval(object[] values)
        {
            return base.Eval(values);
        }
    }
}
