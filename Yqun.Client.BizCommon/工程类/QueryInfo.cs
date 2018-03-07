using System;

namespace BizCommon
{
    [Serializable]
    public class QueryInfo
    {
        string _ModuleId;
        public String ModuleId
        {
            get
            {
                return _ModuleId;
            }
            set
            {
                _ModuleId = value;
            }
        }

        string _ModuleCode;
        public String ModuleCode
        {
            get
            {
                return _ModuleCode;
            }
            set
            {
                _ModuleCode = value;
            }
        }

        string _ModuleDescription;
        public String ModuleDescription
        {
            get
            {
                return _ModuleDescription;
            }
            set
            {
                _ModuleDescription = value;
            }
        }

        public override string ToString()
        {
            return ModuleDescription;
        }
    }
}
