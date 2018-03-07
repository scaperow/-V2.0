using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace ShuXianCaiJiHlperClient
{
    /// <summary>
    /// 肯特仪表老版本液晶屏
    /// </summary>
    public class OldKentUnivers
    {
        /// <summary>
        /// 使用log4net.dll日志接口实现日志记录
        /// </summary>
        private static readonly log4net.ILog _Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
        /// 串口实例
        /// </summary>
        SerialPort SerialPort;

        /// <summary>
        /// 全局变量字节数组,用于配合bytes分析字节数组数据,判断当前信息是否获取完整
        /// </summary>
        byte[] Abytes;

        /// <summary>
        /// 指令是否开始，开始
        /// </summary>
        bool IsStart = false;

        /// <summary>
        /// 开始标志索引
        /// </summary>
        int SartIndex = 0;

        /// <summary>
        /// 当前力值
        /// </summary>
        double Force = 0.00;

        /// <summary>
        /// 开始操作
        /// </summary>
        public void StartAcquisition()
        {
            SerialPort = new SerialPort(PortName, PortBaud, Parity.None, 8, StopBits.One);
            SerialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
            try
            {
                if (!SerialPort.IsOpen)
                {
                    SerialPort.Open();
                }

                byte[] s = new byte[] { 0XA5, 0XA5, 0XA5, 0XA5, 0XA5, 0XA5, 0XA5, 0XA5, 0XA5, 0XA5 };
                SerialPort.Write(s, 0, s.Length);
                IsContinue = true;
                IsFinished = false;
                while (!IsContinue)
                {
                    SerialPort.Close();
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
                if (SerialPort == null)
                {
                    return;
                }
                byte[] bytes = new byte[SerialPort.BytesToRead];
                if (bytes != null && bytes.Length > 0)
                {
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        if (bytes[i] == 240)
                        {
                            IsStart = true;
                            SartIndex = i;
                        }
                        if (IsStart && bytes[i] == 15)
                        {
                            int low3 = Abytes[6] & 0xf;
                            int high3 = (Abytes[6] >> 4) * 10;
                            int low2 = Abytes[5] & 0xf;
                            int high2 = (Abytes[5] >> 4) * 10;
                            int low1 = Abytes[4] & 0xf;
                            int high1 = (Abytes[4] >> 4) * 10;
                            Force = (Convert.ToDouble(low1 + high1) * 10000 + Convert.ToDouble(low2 + high2) * 100 + Convert.ToDouble(low3 + high3)) / 100;
                            if (Force > 10)
                            {
                                DataReceive(Force);
                                IsFinished = true;
                            }
                            if (!(Force > 5))
                            {
                                IsFinished = false;
                                CurrentNumber++;
                                TestFinished(CurrentNumber);
                            }
                            IsStart = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _Log.Error(ex.ToString());
            }
        }
    }
}
