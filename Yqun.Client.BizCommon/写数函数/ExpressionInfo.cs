using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class ExpressionInfo
    {
        public ExpressionInfo()
        {
            _Index = Guid.NewGuid().ToString();
            _DataItem = new FieldInfo();
            _DataValue = new FieldInfo();
        }

        private String _Index;
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

        private FieldInfo _DataItem;
        public FieldInfo DataItem
        {
            get
            {
                return _DataItem;
            }
            set
            {
                _DataItem = value;
            }
        }

        private String _Operation;
        public String Operation
        {
            get
            {
                return _Operation;
            }
            set
            {
                _Operation = value;
            }
        }

        private FieldInfo _DataValue;
        public FieldInfo DataValue
        {
            get
            {
                return _DataValue;
            }
            set
            {
                _DataValue = value;
            }
        }
    }
}
