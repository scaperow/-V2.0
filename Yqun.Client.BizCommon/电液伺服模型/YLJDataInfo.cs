using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class YLJDataInfo
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
        /// 抗压力值
        /// </summary>
        String _F_KYLZ;
        public String F_KYLZ
        {
            get
            {
                return _F_KYLZ;
            }
            set
            {
                _F_KYLZ = value;
            }
        }

        /// <summary>
        /// 龄期
        /// </summary>
        String _F_LQ;
        public String F_LQ
        {
            get
            {
                return _F_LQ;
            }
            set
            {
                _F_LQ = value;
            }
        }

        /// <summary>
        /// 是否掺外加剂
        /// </summary>
        String _F_ISWJJ;
        public String F_ISWJJ
        {
            get
            {
                return _F_ISWJJ;
            }
            set
            {
                _F_ISWJJ = value;
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
