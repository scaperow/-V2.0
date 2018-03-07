using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCommon
{
    [Serializable]
    public class CompareOperation
    {
        public static CompareOperation 等于 = new CompareOperation("等于", " = ");
        public static CompareOperation 不等于 = new CompareOperation("不等于", " <> ");
        public static CompareOperation 大于 = new CompareOperation("大于", " > ");
        public static CompareOperation 大于或等于 = new CompareOperation("大于或等于", " >= ");
        public static CompareOperation 小于 = new CompareOperation("小于", " < ");
        public static CompareOperation 小于或等于 = new CompareOperation("小于或等于", " <= ");
        public static CompareOperation 包含 = new CompareOperation("包含", " like ");
        public static CompareOperation 不包含 = new CompareOperation("不包含", " not like ");
        public static CompareOperation 属于 = new CompareOperation("属于", " in ");
        public static CompareOperation 不属于 = new CompareOperation("不属于", " not in ");
        public static CompareOperation 始于 = new CompareOperation("始于", "  like  ");
        public static CompareOperation 不始于 = new CompareOperation("不始于", "  not like  ");
        public static CompareOperation 是 = new CompareOperation("是", "  is  ");
        public static CompareOperation 不是 = new CompareOperation("不是", "  is not ");

        public CompareOperation(String Text, String Value)
        {
            _Text = Text;
            _Value = Value;
        }

        String _Text = "";
        public String Text
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
            }
        }

        String _Value = "";
        public String Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        public override string ToString()
        {
            return Text;
        }
    }

    [Serializable]
    public class Item
    {
        FilterStyle _Style = FilterStyle.DataColumn;
        public FilterStyle Style
        {
            get
            {
                return _Style;
            }
            set
            {
                _Style = value;
            }
        }

        string _TableName = "";
        public string TableName
        {
            get
            {
                return _TableName;
            }
            set
            {
                _TableName = value;
            }
        }

        string _FieldName = "";
        public string FieldName
        {
            get
            {
                return _FieldName;
            }
            set
            {
                _FieldName = value;
            }
        }

        object _Value;
        public object Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        Boolean _IsNull = false;
        public Boolean IsNull
        {
            get
            {
                return _IsNull;
            }
            set
            {
                _IsNull = value;
            }
        }

        string _ParameterName = "";
        public string ParameterName
        {
            get
            {
                return this._ParameterName;
            }
            set
            {
                this._ParameterName = value;
            }
        }
    }
}
