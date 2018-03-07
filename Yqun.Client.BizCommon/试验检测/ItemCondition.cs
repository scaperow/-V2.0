using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class ItemCondition
    {
        String _Index = Guid.NewGuid().ToString();
        public String Index
        {
            get
            {
                return _Index;
            }
            set
            {
                _Index = value;
            }
        }

        String _EvaluateIndex;
        public String EvaluateIndex
        {
            get
            {
                return _EvaluateIndex;
            }
            set
            {
                _EvaluateIndex = value;
            }
        }

        String _Text;
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

        String _Specifiedvalue;
        public String Specifiedvalue
        {
            get
            {
                return _Specifiedvalue;
            }
            set
            {
                _Specifiedvalue = value;
            }
        }

        String _Truevalue;
        public String TrueValue
        {
            get
            {
                return _Truevalue;
            }
            set
            {
                _Truevalue = value;
            }
        }

        String _Expression;
        public String Expression
        {
            get
            {
                return _Expression;
            }
            set
            {
                _Expression = value;
            }
        }
    }
}
