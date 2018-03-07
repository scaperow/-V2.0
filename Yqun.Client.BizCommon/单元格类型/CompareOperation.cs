using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
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
        public static CompareOperation Is = new CompareOperation("Is", " is ");

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
}
