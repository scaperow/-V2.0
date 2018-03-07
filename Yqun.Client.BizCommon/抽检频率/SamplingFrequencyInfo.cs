using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class SamplingFrequencyInfo
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

        List<ItemFrequency> _ItemFrequencys = new List<ItemFrequency>();
        public List<ItemFrequency> ItemFrequencys
        {
            get
            {
                return _ItemFrequencys;
            }
            set
            {
                _ItemFrequencys = value;
            }
        }
    }
}
