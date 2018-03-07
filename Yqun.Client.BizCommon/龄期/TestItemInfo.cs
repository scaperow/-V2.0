using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class TestItemInfo
    {
        String _Index;
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

        String _Description;
        public String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
            }
        }

        public override string ToString()
        {
            return "[" + Index + "]" + Description;
        }
    }
}
