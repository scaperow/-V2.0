using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class DataFilterItem
    {
        String _ConditionalOperator;
        public String ConditionalOperator
        {
            get
            {
                return _ConditionalOperator;
            }
            set
            {
                _ConditionalOperator = value;
            }
        }

        String _LeftItem;
        public String LeftItem
        {
            get
            {
                return _LeftItem;
            }
            set
            {
                _LeftItem = value;
            }
        }

        CompareOperation _compareOperation = CompareOperation.等于;
        public CompareOperation CompareOperation
        {
            get
            {
                return _compareOperation;
            }
            set
            {
                _compareOperation = value;
            }
        }

        String _RightItem;
        public String RightItem
        {
            get
            {
                return _RightItem;
            }
            set
            {
                _RightItem = value;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", LeftItem, CompareOperation.Value, RightItem);
        }

        public string ToExpression()
        {
            String key = RightItem;
            IParameter Paramter = ParameterValueManager.GetValue(key);
            object value;
            if (Paramter != null)
                value = Paramter.Value;
            else
                value = key;

            String Result = "1<>1";
            if (value != null)
            {
                if (value.GetType().FullName.ToLower().Contains("int") || value.GetType().FullName.ToLower().Contains("decimal"))
                {
                    Result = string.Format("[{0}]{1}{2}", LeftItem, CompareOperation.Value, Convert.ToDouble(value));
                }
                else if (value.GetType().FullName.ToLower().Contains("datetime"))
                {
                    DateTime datetime = Convert.ToDateTime(value);
                    string operation = "=";
                    if (CompareOperation.ToString() == CompareOperation.大于.ToString())
                    {
                        operation = ">=";
                        datetime = datetime.AddDays(1);
                    }
                    else if (CompareOperation.ToString() == CompareOperation.大于或等于.ToString())
                    {
                        operation = ">=";
                    }
                    else if (CompareOperation.ToString() == CompareOperation.小于.ToString())
                    {
                        operation = "<";
                    }
                    else if (CompareOperation.ToString() == CompareOperation.小于或等于.ToString())
                    {
                        operation = "<";
                        datetime = datetime.AddDays(1);
                    }

                    Result = string.Format("[{0}]{1}'{2}'", LeftItem, operation, datetime.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
                }
                else if (value.GetType().FullName.ToLower().Contains("dbnull"))
                {
                    Result = string.Format("[{0}] is null", LeftItem);
                }
                else
                {
                    String Format = "[{0}]{1}'{2}'";
                    if (CompareOperation.ToString() == CompareOperation.包含.ToString() ||
                        CompareOperation.ToString() == CompareOperation.不包含.ToString())
                    {
                        Format = "[{0}] {1} '%{2}%'";
                    }

                    Result = string.Format(Format, LeftItem, CompareOperation.Value, Convert.ToString(value));
                }
            }

            return Result;
        }
    }
}
