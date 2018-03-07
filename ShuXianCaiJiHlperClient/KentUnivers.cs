﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace ShuXianCaiJiHlperClient
{
    public class KentUnivers : IMachines
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
            SerialPort.Open();
            IsContinue = true;
            IsFinished = false;
            try
            {

                while (!IsContinue)
                {
                    SerialPort.Close();
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
                byte[] bytes = new byte[SerialPort.BytesToRead];
                SerialPort.Read(bytes, 0, bytes.Length);
                //当返回二进制数据打头为'240'时,认为是协议头数据
                if (bytes[0] == 240)
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

                if (Abytes.Length == 15)
                {
                    string Force = string.Empty;
                    if (Abytes[0].ToString() == "240")
                    {
                        int low = Abytes[2] & 0xf;
                        int high = (Abytes[2] >> 4) * 10;

                        int low3 = Abytes[9] & 0xf;
                        int high3 = (Abytes[9] >> 4) * 10;

                        int low2 = Abytes[8] & 0xf;
                        int high2 = (Abytes[8] >> 4) * 10;

                        int low1 = Abytes[7] & 0xf;
                        int high1 = (Abytes[7] >> 4) * 10;

                        Force = ((Convert.ToDouble(low1 + high1) * 10000 + Convert.ToDouble(low2 + high2) * 100 + Convert.ToDouble(low3 + high3)) / 100).ToString();
                    }
                    if (Abytes[0].ToString() == "18")
                    {
                        int low = Abytes[0] & 0xf;
                        int high = (Abytes[0] >> 4) * 10;


                        int low3 = Abytes[3] & 0xf;
                        int high3 = (Abytes[3] >> 4) * 10;

                        int low2 = Abytes[2] & 0xf;
                        int high2 = (Abytes[2] >> 4) * 10;

                        int low1 = Abytes[1] & 0xf;
                        int high1 = (Abytes[1] >> 4) * 10;

                        Force = ((Convert.ToDouble(low1 + high1) * 10000 + Convert.ToDouble(low2 + high2) * 100 + Convert.ToDouble(low3 + high3)) / 100).ToString();
                    }
                    Double force = Convert.ToDouble(Force);


                    //当实时力值大于10KN的时候,开始正式采集数据
                    if (force > 10)
                    {
                        DataReceive(Force);
                        IsFinished = true;
                    }
                    //因为万能机油门不如压力机稳定,会出现波动现象,因此将判断试验结束的标准线定为5KN,防止因波动导致的误差
                    if (!(force > 5))
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