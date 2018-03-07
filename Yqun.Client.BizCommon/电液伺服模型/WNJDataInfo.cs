using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class WNJDataInfo
    {
        /// <summary>
        /// 试验时间
        /// </summary>
        String _F_SYSJ;
        public String F_SYSJ
        {
            get
            {
                return _F_SYSJ;
            }
            set
            {
                _F_SYSJ = value;
            }
        }

        /// <summary>
        /// 厂商编码
        /// </summary>
        String _F_RTCODE;
        public String F_RTCODE
        {
            get
            {
                return _F_RTCODE;
            }
            set
            {
                _F_RTCODE = value;
            }
        }

        /// <summary>
        /// 委托编号
        /// </summary>
        String _F_WTBH;
        public String F_WTBH
        {
            get
            {
                return _F_WTBH;
            }
            set
            {
                _F_WTBH = value;
            }
        }

        /// <summary>
        /// 设备编号
        /// </summary>
        String _F_SBBH;
        public String F_SBBH
        {
            get
            {
                return _F_SBBH;
            }
            set
            {
                _F_SBBH = value;
            }
        }

        /// <summary>
        /// 公称直径
        /// </summary>
        String _F_GCZJ;
        public String F_GCZJ
        {
            get
            {
                return _F_GCZJ;
            }
            set
            {
                _F_GCZJ = value;
            }
        }

        /// <summary>
        /// 力值
        /// </summary>
        String _F_LZ;
        public String F_LZ
        {
            get
            {
                return _F_LZ;
            }
            set
            {
                _F_LZ = value;
            }
        }

        /// <summary>
        /// 屈服力值
        /// </summary>
        String _F_QFLZ;
        public String F_QFLZ
        {
            get
            {
                return _F_QFLZ;
            }
            set
            {
                _F_QFLZ = value;
            }
        }

        /// <summary>
        /// 断后标距
        /// </summary>
        String _F_DHBJ;
        public String F_DHBJ
        {
            get
            {
                return _F_DHBJ;
            }
            set
            {
                _F_DHBJ = value;
            }
        }

        /// <summary>
        /// 伸长率
        /// </summary>
        String _F_SCL;
        public String F_SCL
        {
            get
            {
                return _F_SCL;
            }
            set
            {
                _F_SCL = value;
            }
        }

        /// <summary>
        /// 试验员
        /// </summary>
        String _F_OPERATOR;
        public String F_OPERATOR
        {
            get
            {
                return _F_OPERATOR;
            }
            set
            {
                _F_OPERATOR = value;
            }
        }

        /// <summary>
        /// 内部标识符，恒为1
        /// </summary>
        String _F_RID;
        public String F_RID
        {
            get
            {
                return _F_RID;
            }
            set
            {
                _F_RID = value;
            }
        }

        /// <summary>
        /// ID标识符
        /// </summary>
        String _F_GUID;
        public String F_GUID
        {
            get
            {
                return _F_GUID;
            }
            set
            {
                _F_GUID = value;
            }
        }
    }
}
