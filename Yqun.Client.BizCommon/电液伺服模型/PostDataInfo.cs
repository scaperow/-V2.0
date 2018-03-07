using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class PostDataInfo
    {
        String _Type;
        String _MK;
        String _GN;
        String _BBH;
        String _KFSBH;
        String _SBH;
        String _FSSJ;
        String _ZWCD;
        String _JMFS;
        String _SFQM;
        String _CFBS;
        String _YLCS;
        String _SJJY;
        String _SZQM;
        String _JMZW;
        String _ZSXH;

        /// <summary>
        /// 设备类型
        /// </summary>
        public String Type
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

        /// <summary>
        /// 所属模块编号 1：拌合站 2：试验室
        /// </summary>
        public String MK
        {
            get
            {
                return _MK;
            }
            set
            {
                _MK = value;
            }
        }

        /// <summary>
        /// 所属功能编号 拌和站：1：拌和数据 试验室：1：压力机数据 2：万能材料试验机数据
        /// </summary>
        public String GN
        {
            get
            {
                return _GN;
            }
            set
            {
                _GN = value;
            }
        }

        /// <summary>
        /// 版本号
        /// </summary>
        public String BBH
        {
            get
            {
                return _BBH;
            }
            set
            {
                _BBH = value;
            }
        }

        /// <summary>
        /// 采集软件开发商编号
        /// 由RCPMIS系统统一编码
        /// </summary>
        public String KFSBH
        {
            get
            {
                return _KFSBH;
            }
            set
            {
                _KFSBH = value;
            }
        }

        /// <summary>
        /// 终端设备号
        /// </summary>
        /// <remarks>
        /// 拌合机、压力机、万能材料机等设备编号
        /// </remarks>
        public String SBH
        {
            get
            {
                return _SBH;
            }
            set
            {
                _SBH = value;
            }
        }

        /// <summary>
        /// 发送时间
        /// </summary>
        /// <remarks>
        /// YYYY-MM-DD HH:MM:SS
        /// </remarks>
        public String FSSJ
        {
            get
            {
                return _FSSJ;
            }
            set
            {
                _FSSJ = value;
            }
        }

        /// <summary>
        /// 正文长度
        /// </summary>
        /// <remarks>
        /// 经加密的正文数据长度，用于检验传输完整性。
        /// </remarks>
        public String ZWCD
        {
            get
            {
                return _ZWCD;
            }
            set
            {
                _ZWCD = value;
            }
        }

        /// <summary>
        /// 正文加密方法
        /// </summary>
        /// <remarks>
        /// 对正文进行加密的方式。默认为1。
        /// 1：自定义加密算法
        /// 2：数字证书加密
        /// </remarks>
        public String JMFS
        {
            get
            {
                return _JMFS;
            }
            set
            {
                _JMFS = value;
            }
        }

        /// <summary>
        /// 正文是否签名
        /// </summary>
        /// <remarks>
        /// 0：未签名
        /// 1：已签名
        /// </remarks>
        public String SFQM
        {
            get
            {
                return _SFQM;
            }
            set
            {
                _SFQM = value;
            }
        }

        /// <summary>
        /// 重发标识
        /// </summary>
        /// <remarks>
        /// 0：新数据
        /// 1：重发数据
        /// </remarks>
        public String CFBS
        {
            get
            {
                return _CFBS;
            }
            set
            {
                _CFBS = value;
            }
        }

        /// <summary>
        /// 预留参数
        /// </summary>
        /// <remarks>
        /// 预留功能位
        /// </remarks>
        public String YLCS
        {
            get
            {
                return _YLCS;
            }
            set
            {
                _YLCS = value;
            }
        }

        /// <summary>
        /// 数据校验
        /// </summary>
        /// <remarks>
        /// 加密正文内容校验值，主要用于验证数据正文的完整性。固定长度。
        /// </remarks>
        public String SJJY
        {
            get
            {
                return _SJJY;
            }
            set
            {
                _SJJY = value;
            }
        }

        /// <summary>
        /// 数字签名
        /// </summary>
        /// <remarks>
        /// 明文正文的数字签名。固定长度。
        /// </remarks>
        public String SZQM
        {
            get
            {
                return _SZQM;
            }
            set
            {
                _SZQM = value;
            }
        }

        /// <summary>
        /// 加密数据正文内容。
        /// </summary>
        /// <remarks>
        /// 密文形式业务数据。
        /// </remarks>
        public String JMZW
        {
            get
            {
                return _JMZW;
            }
            set
            {
                _JMZW = value;
            }
        }

        /// <summary>
        /// 加密狗序列号
        /// </summary>
        public String ZSXH
        {
            get
            {
                return _ZSXH;
            }
            set
            {
                _ZSXH = value;
            }
        }
    }
}
