using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace ShuXianCaiJiHlperClient
{
    public class OKEPressure : IMachines
    {
        /// <summary>
        /// 使用log4net.dll日志接口实现日志记录
        /// </summary>
        private static readonly log4net.ILog _Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region ISent 成员

        byte[] Abytes;              //全局变量字节数组,用于配合bytes分析字节数组数据,判断当前信息是否获取完整
        String text;                //全局变量文本,用于在端口接受数据事件中获取信息后在另外的线程中进行分析


        List<byte> _List = new List<byte>();

        /// <summary>
        /// 接受Baty字符串
        /// </summary>
        String _StringBuilder;

        /// <summary>
        /// 接受字符串
        /// </summary>
        String _StringBuilderReveive;

        /// <summary>
        /// 实时数据
        /// </summary>
        double _TempForce = 0.00;

        /// <summary>
        /// 单次试验数据获得事件
        /// </summary>
        public event DataReceiveDelegate DataReceive;

        /// <summary>
        /// 试验完成事件
        /// </summary>
        public event TestFinishedDelegate TestFinished;

        /// <summary>
        /// 串口波特率
        /// </summary>
        public int PortBaud
        { get; set; }

        /// <summary>
        /// 串口名称
        /// </summary>
        public string PortName
        { get; set; }

        /// <summary>
        /// 试验试件总数
        /// </summary>
        public int CurrentNumber
        { get; set; }

        /// <summary>
        /// 是否继续发送命令
        /// </summary>
        public Boolean IsContinue
        { get; set; }

        /// <summary>
        /// 是否处于有效数据区
        /// </summary>
        public Boolean IsFinished
        { get; set; }
        public Boolean IsRecordLog //是否开始记录运行日志
        { get; set; }
        public double MaxForce
        { get; set; }

        public double HForce
        { get; set; }

        public double LForce
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        SerialPort _SerialPort;

        /// <summary>
        /// 开始采集
        /// </summary>
        /// <param name="ControllerOrder"></param>
        public void StartAcquisition()
        {
            try
            {
                _SerialPort = new SerialPort(PortName, PortBaud, Parity.None, 8, StopBits.One);
                _SerialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
                if (!_SerialPort.IsOpen)
                {
                    _SerialPort.Open();
                }
                IsContinue = true;
                IsFinished = false;

                while (IsContinue)
                {
                    byte[] WriteByte = new byte[] { 0XA5, 00, 0X00, 00, 0x00, 4, 0, 0, 0, 0, 0X5A };
                    _SerialPort.Write(WriteByte, 0, WriteByte.Length);
                }
                if (_SerialPort.IsOpen)
                {
                    _SerialPort.Close();
                }
            }
            catch (Exception ex)
            {
                _Log.Error(ex.ToString());
            }
        }

        /// <summary>
        /// 串口返回信息触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (_SerialPort == null)
                {
                    return;
                }

                byte[] bytes = new byte[_SerialPort.BytesToRead];
                _SerialPort.Read(bytes, 0, bytes.Length);
                _StringBuilder = "";
                if (bytes != null)
                {
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        _StringBuilder += bytes[i].ToString();
                    }
                }
                _Log.Info(_StringBuilder);
                //当字符串长度大于3且以'165'开头时认为当前返回数据是协议头数据
                if (!(_StringBuilder.Length < 3))
                {
                    if (_StringBuilder.Substring(0, 3) == "165")
                    {
                        _StringBuilderReveive = _StringBuilder;
                        Abytes = bytes;
                    }                    
                }
                //当其他情况时,认为是协议中间数据,将其加到已知的头数据之后
                else
                {
                    _StringBuilderReveive += _StringBuilder;
                    
                    byte[] newbyte = new byte[Abytes.Length + bytes.Length];
                    Abytes.CopyTo(newbyte, 0);
                    bytes.CopyTo(newbyte, Abytes.Length);
                    Abytes = newbyte;
                }
                
                if (_StringBuilderReveive.Substring(_StringBuilderReveive.Length - 2, 2) == "90")
                {
                    //当经过初步处理的字符串长度达到11时
                    if (Abytes.Length >= 11)
                    {
                        _TempForce = (Convert.ToDouble(Abytes[7].ToString()) * 256 * 256 + Convert.ToDouble(Abytes[8].ToString()) * 256 + Convert.ToDouble(Abytes[9].ToString())) / 100;  //计算实时力值
                        //欧凯压力机在不给油时会不停的返回3个数据位均为255的信息,计算得出的力值达到16550+,为避免此现象,将力值最大值限制在2500以内
                        if (_TempForce < 2500)
                        {
                            DataReceive(_TempForce);

                            if (_TempForce > 50)
                            {
                                if (DataReceive != null)
                                {
                                    IsFinished = true;
                                }
                            }
                            //当力值>50时,开始正式采集数据,<30则认为是当前试件采数结束                           
                            if (!(_TempForce > 30) && IsFinished)
                            {
                                IsFinished = false;
                                CurrentNumber++;
                                TestFinished(CurrentNumber);
                            }
                        }

                        _StringBuilder.Remove(0, 11);
                    }
                }
            }
            catch (Exception ex)
            {
                _Log.Error(ex.ToString());
            }
        }


        void SerialPort_DataReceived1(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (_SerialPort == null)
                {
                    return;
                }

                byte[] bytes = new byte[_SerialPort.BytesToRead];
                _SerialPort.Read(bytes, 0, bytes.Length);
                _List.InsertRange(_List.Count>0?_List.Count - 1:0, bytes);

                if (_List.IndexOf(165) > 0)
                {
                    _List.RemoveRange(0, _List.IndexOf(165));
                }

                while (_List.IndexOf(165) == 0 && _List.Count==11)
                {
                    _TempForce = (Convert.ToDouble(_List[7].ToString()) * 256 * 256 + Convert.ToDouble(_List[8].ToString()) * 256 + Convert.ToDouble(_List[9].ToString())) / 100;
                    _List.RemoveRange(0,11);
                    //计算实时力值
                    //欧凯压力机在不给油时会不停的返回3个数据位均为255的信息,计算得出的力值达到16550+,为避免此现象,将力值最大值限制在2500以内
                    if (DataReceive != null)
                    {
                        DataReceive(_TempForce);
                    }

                    if (_TempForce < 2500)
                    {
                        if (_TempForce > 10 && !IsFinished)
                        {
                            IsFinished = true;
                        }
                        //当力值>50时,开始正式采集数据,<30则认为是当前试件采数结束                           
                        if (_TempForce < 10 && IsFinished)
                        {
                            IsFinished = false;
                            CurrentNumber++;
                            TestFinished(CurrentNumber);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
        #endregion
    }
}
