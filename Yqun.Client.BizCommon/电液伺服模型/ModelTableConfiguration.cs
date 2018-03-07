using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class ModelTableConfiguration
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

        String _ItemIndex;
        public String ItemIndex
        {
            get
            {
                return _ItemIndex;
            }
            set
            {
                _ItemIndex = value;
            }
        }

        String _ItemName;
        public String ItemName
        {
            get
            {
                return _ItemName;
            }
            set
            {
                _ItemName = value;
            }
        }

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

        String _WTBHFieldName;
        public String WTBHFieldName
        {
            get
            {
                return _WTBHFieldName;
            }
            set
            {
                _WTBHFieldName = value;
            }
        }

        /// <summary>
        /// 最大力值
        /// </summary>
        String _MAXLZ;
        public String MAXLZ
        {
            get
            {
                return _MAXLZ;
            }
            set
            {
                _MAXLZ = value;
            }
        }

        /// <summary>
        /// 屈服力值
        /// </summary>
        String _QFLZ;
        public String QFLZ
        {
            get
            {
                return _QFLZ;
            }
            set
            {
                _QFLZ = value;
            }
        }

        /// <summary>
        /// 断后标距
        /// </summary>
        String _DHBJ;
        public String DHBJ
        {
            get
            {
                return _DHBJ;
            }
            set
            {
                _DHBJ = value;
            }
        }

        int _SJOrder;
        public int SJOrder
        {
            get
            {
                return _SJOrder;
            }
            set
            {
                _SJOrder = value;
            }
        }

        int _Type;
        public int Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
            }
        }
    }
}
