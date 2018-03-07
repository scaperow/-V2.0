using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using ShuXianCaiJiComponents;

namespace ShuXianCaiJiHlperClient
{
    public class NewKentUnivers : IMachines
    {
        /// <summary>
        /// 使用log4net.dll日志接口实现日志记录
        /// </summary>
        private static readonly log4net.ILog _Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


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

        #region IMachines 成员

        public event DataReceiveDelegate DataReceive;

        public event TestFinishedDelegate TestFinished;

        SerialPort SerialPort;

        double Force = 0.00;

        StringBuilder _StringBuilder = new StringBuilder();

        /// 开始采集
        /// </summary>
        /// <param name="ControllerOrder"></param>
        public void StartAcquisition()
        {

            _Log.Error(PortName + "---" + PortBaud.ToString());
            SerialPort = new SerialPort(PortName, PortBaud, Parity.None, 8, StopBits.One);
            SerialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
            IsContinue = true;
            IsFinished = false;
            try
            {
                if (!SerialPort.IsOpen)
                {
                    _Log.Error("打开串口");
                    SerialPort.Open();

                    byte[] tempByte = DataEncoder.strToToHexByte("A5A5A5A5A5A5A5A5A5A5");
                    _Log.Error("发送10个A5");
                    SerialPort.Write(tempByte, 0, tempByte.Length);
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
                byte[] bytes = new byte[SerialPort.BytesToRead];
                SerialPort.Read(bytes, 0, bytes.Length);
                for (int i = 0; i < bytes.Length; i++)
                {
                    _StringBuilder.Append(bytes[i].ToString("X2"));
                }
                int n = _StringBuilder.ToString().IndexOf("F0");
                if (_StringBuilder.ToString().IndexOf("F0") > 0)
                {
                    _StringBuilder.Remove(0, _StringBuilder.ToString().IndexOf("FO"));
                }
                _Log.Error(_StringBuilder.ToString());
                while (_StringBuilder.Length >= 30)
                {
                    if (_StringBuilder.Length >= 30)
                    {
                        if (_StringBuilder.ToString().Substring(5, 1) == "2")
                        {
                            Force = Convert.ToDouble(_StringBuilder.ToString().Substring(6, 6)) / 100.00;
                        }
                        else if (_StringBuilder.ToString().Substring(5, 1) == "1")
                        {
                            Force = Convert.ToDouble(_StringBuilder.ToString().Substring(6, 6)) / 10;
                        }
                        _StringBuilder.Remove(0, _StringBuilder.ToString().IndexOf("0F") + 2);
                    }
                }

                _Log.Error(Force.ToString());
                //当实时力值大于10KN的时候,开始正式采集数据
                if (Force > 10)
                {
                    IsFinished = true;
                }
                //因为万能机油门不如压力机稳定,会出现波动现象,因此将判断试验结束的标准线定为5KN,防止因波动导致的误差
                if (Force < 5 && IsFinished)
                {
                    IsFinished = false;
                    CurrentNumber++;
                    TestFinished(CurrentNumber);
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
