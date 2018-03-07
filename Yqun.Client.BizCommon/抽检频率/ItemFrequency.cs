using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class ItemFrequency
    {
        String _ModelIndex;
        public String ModelIndex
        {
            get
            {
                return _ModelIndex;
            }
            set
            {
                _ModelIndex = value;
            }
        }

        String _ModelName;
        public String ModelName
        {
            get
            {
                return _ModelName;
            }
            set
            {
                _ModelName = value;
            }
        }

        String _FrequencyInfoIndex;
        public String FrequencyInfoIndex
        {
            get
            {
                return _FrequencyInfoIndex;
            }
            set
            {
                _FrequencyInfoIndex = value;
            }
        }

        float _JianZhengFrequency = 0;
        public float JianZhengFrequency
        {
            get
            {
                return _JianZhengFrequency;
            }
            set
            {
                _JianZhengFrequency = value;
            }
        }

        float _PingXingFrequency = 0;
        public float PingXingFrequency
        {
            get
            {
                return _PingXingFrequency;
            }
            set
            {
                _PingXingFrequency = value;
            }
        }
    }
}
