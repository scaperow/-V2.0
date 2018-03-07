using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShuXianCaiJiModule
{
    [Serializable]
    public class SpecialSetting
    {
        /// <summary>
        /// 端口号
        /// </summary>
        public String PortName { get; set; }

        /// <summary>
        /// 波特率
        /// </summary>
        public Int32 PortBaud { get; set; }

        /// <summary>
        /// 最小的有效数据，大于此数时标志试验开始进入有效数据阶段，试验正式开始
        /// </summary>
        public Double MinValidValue { get; set; }

        /// <summary>
        /// 最小的标志结束数据，试验正式开始后，接收值小于此数，标志试验结束
        /// </summary>
        public Double MinFinishValue { get; set; }

        /// <summary>
        /// 无效数据范围集合，在此范围内的数据点将被过滤
        /// </summary>
        public List<ValueRange> InvalidValueRangeList { get; set; }

        /// <summary>
        /// 设备厂家
        /// </summary>
        public String MachineCompany { get; set; }

        /// <summary>
        /// 设备类型1压力机，2万能机
        /// </summary>
        public Int32 MachineType { get; set; }

        /// <summary>
        /// 是否是调试过程
        /// </summary>
        public Boolean IsDebug { get; set; }

        /// <summary>
        /// 设备编码
        /// </summary>
        public String MachineCode { get; set; }

        /// <summary>
        /// 试验室编码
        /// </summary>
        public String TestRoomCode { get; set; }

        /// <summary>
        /// 图形绘制频率，默认为1000毫秒
        /// </summary>
        public Int32 DrawChartInterval { get; set; }

        /// <summary>
        /// 曲线图X轴默认最大值
        /// </summary>
        public Double XDefaultMaxValue { get; set; }

        /// <summary>
        /// 曲线图Y轴默认最大值
        /// </summary>
        public Double YDefaultMaxValue { get; set; }

        /// <summary>
        /// 钢筋试验是否使用下屈服计算屈服强度，1为使用上屈服,2为使用下屈服
        /// </summary>
        public Int16 QuFuType { get; set; }

        /// <summary>
        /// 签名序列（加密锁）
        /// </summary>
        public string MachineKeyCode { get; set; }

        /// <summary>
        /// 计算屈服参数（无屈服是屈服系数）
        /// </summary>
        public double QFParameter { get; set; }

        /// <summary>
        ///  计算屈服开始力值（有KN改MPA为）
        /// </summary>
        public double QFStartValue{ get; set; }

        private double _QFStartValueMPA = 100;

        /// <summary>
        /// 兆帕
        /// </summary>
        public double QFStartValueMPA
        {
            get
            {
                return _QFStartValueMPA;
            }
            set
            {
                _QFStartValueMPA = value;
            }
        }

        /// <summary>
        /// 连续上升点数标志屈服过程完毕
        /// </summary>
        public int QFPoints { get; set; }

        /// <summary>
        /// 屈服名称
        /// </summary>
        public string QFName { get; set; }

        /// <summary>
        /// 仪表归零参数（Kent液晶屏）
        /// </summary>
        public float ZeroParameters { get; set; }

        /// <summary>
        /// AD码标定值（Kent老液晶屏早期版本）
        /// </summary>
        public float BDValue { get; set; }

        /// <summary>
        /// 力值小数位数
        /// </summary>
        public int PointNum { get; set; }

        private Int32 _validCount = 30;
        /// <summary>
        /// 有效点数集合，当采集完一块/根后，若集合中不重复点数小于此值，放弃当前试验
        /// </summary>
        public Int32 ValidCount
        {
            get
            {
                return _validCount;
            }
            set
            {
                _validCount = value;
            }
        }

        /// <summary>
        /// N
        /// </summary>
        public Int32 MedianValueFilteringAlgorithmNumber { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public Boolean IsMedianValueFilteringAlgorithmNumber { get; set; }

        private Int32 precisionGrade = 3;
        /// <summary>
        /// 屈服精度等级
        /// </summary>
        public Int32 PrecisionGrade
        {
            get
            {
                return precisionGrade;
            }
            set
            {
                precisionGrade = value;
            }
        }

        /// <summary>
        /// 计算特殊参数
        /// </summary>
        public double SpecialD { get; set; }

        /// <summary>
        /// 参数精度
        /// </summary>
        public double DPlace { get; set; }


        private double _MinValidValueMPA = 100;

        private int _CommunicationType = 0;

        /// <summary>
        /// 通讯类型
        /// 0：串口通讯(默认)
        /// 1：UDP通讯
        /// 2：TCP/IP通讯
        /// </summary>
        public int CommunicationType
        {
            get { return _CommunicationType; }
            set { _CommunicationType = value; }
        }

        private ushort _LocalPort = 8000;

        /// <summary>
        /// 本地端口
        /// 默认8000
        /// </summary>
        public ushort LocalPort
        {
            get { return _LocalPort; }
            set { _LocalPort = value; }
        }
        private ushort _RemotPort = 8001;

        /// <summary>
        /// 远程端口（仪表端口）
        /// 默认8001
        /// </summary>
        public ushort RemotPort
        {
            get { return _RemotPort; }
            set { _RemotPort = value; }
        }

        private string _RemotIP = string.Empty;

        /// <summary>
        /// 远程IP（仪表IP）
        /// </summary>
        public string RemotIP
        {
            get { return _RemotIP; }
            set { _RemotIP = value; }
        }

        
        private string _LocalIP = string.Empty;

        /// <summary>
        /// 本地IP
        /// </summary>
        public string LocalIP
        {
            
            get { return _LocalIP; }
            set { _LocalIP = value; }
        }

        /// <summary>
        /// 存储当前服务器和用户本地时间差值
        /// </summary>
        public double TimeSpanMinute { get; set; }  

    }
}
