using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace ShuXianCaiJiHlperClient
{
    public class OKEUnivers : IMachines
    {

        /// <summary>
        /// 使用log4net.dll日志接口实现日志记录
        /// </summary>
        private static readonly log4net.ILog _Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region ISent 成员
        //单次试验数据获得事件
        public event DataReceiveDelegate DataReceive;
        //试验完成事件
        public event TestFinishedDelegate TestFinished;

        public int PortBaud //串口波特率
        { get; set; }
        public string PortName //串口名称
        { get; set; }
        public int CurrentNumber //试验试件总数
        { get; set; }
        public Boolean IsContinue //是否继续发送命令
        { get; set; }
        public Boolean IsFinished //是否处于有效数据区
        { get; set; }
        public Boolean IsRecordLog //是否开始记录运行日志
        { get; set; }
        public double MaxForce
        { get; set; }

        public double HForce
        { get; set; }

        public double LForce
        { get; set; }


        SerialPort SerialPort;
        /// <summary>
        /// 开始采集
        /// </summary>
        /// <param name="ControllerOrder"></param>
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
                IsContinue = true;
                IsFinished = false;

                while (IsContinue)
                {
                    byte[] s = new byte[] { 0XFD, 0x07, 0x11, 0x00, 0x00, 0x00, 0x00, 0x00, 0x15 };
                    if (!SerialPort.IsOpen)
                    {
                        SerialPort.Open();
                    }
                    SerialPort.Write(s, 0, s.Length);
                    Thread.Sleep(100);
                }

                if (SerialPort.IsOpen)
                {
                    SerialPort.Close();
                }
            }
            catch (Exception ex)
            {
                _Log.Error(ex.ToString());
            }
        }
        byte[] Abytes;              //全局变量字节数组,用于配合bytes分析字节数组数据,判断当前信息是否获取完整

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
                    SerialPort.Read(bytes, 0, bytes.Length);
                    if (bytes.Length > 0)
                    {
                        //当返回二进制数据打头为'253'时,认为是协议头数据
                        if (bytes[0] == 253)
                        {
                            Abytes = bytes;
                        }
                        //当其他情况时,认为是协议中间数据,将其加到已知的头数据之后
                        else
                        {
                            byte[] newbyte = new byte[Abytes.Length + bytes.Length];
                            Abytes.CopyTo(newbyte, 0);
                            bytes.CopyTo(newbyte, Abytes.Length);
                            Abytes = newbyte;
                        }
                    }
                }
                if (Abytes.Length == 9)
                {
                    //欧凯万能机实时力值的计算方式及可用位数
                    double force = (Convert.ToDouble(Abytes[6] * 256 * 256 + Abytes[5] * 256 + Abytes[4])) / 1000;

                    //当实时力值大于10KN的时候,开始正式采集数据
                    if (force > 20 && force < 1500)
                    {
                        DataReceive(force);
                        IsFinished = true;
                    }
                    //因为万能机油门不如压力机稳定,会出现波动现象,因此将判断试验结束的标准线定为5KN,防止因波动导致的误差
                    if (!(force > 20) && IsFinished)
                    {
                        IsFinished = false;
                        CurrentNumber++;
                        TestFinished(CurrentNumber);
                    }
                }
            }
            catch (Exception ex)
            {
                _Log.Error(ex.ToString());
            }
        }
        #endregion
    }
}
