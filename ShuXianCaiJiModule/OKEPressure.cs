using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using ShuXianCaiJiModule.parse;

namespace ShuXianCaiJiModule
{
    /// <summary>
    /// 欧凯压力机-采数测试通过
    /// </summary>
    public class OKEPressure:MachineBase
    {
        /// <summary>
        /// 串口对象
        /// </summary>
        SerialPort _SerialPort;
        /// <summary>
        /// 实时力值
        /// </summary>
        float Force = 0.00f;

        /// <summary>
        /// 解析数据
        /// </summary>
        StringBuilder _StringBuilder = null;

        /// <summary>
        /// 发送指令byte数组
        /// </summary>
        byte[] _SendByte = null;

        /// <summary>
        /// 存储未接受完的byte数组
        /// </summary>
        byte[] Abytes = null;

        public OKEPressure(bool isTest)
        {
            if (isTest)
            {
                parse = new EmptyParse();
            }
            else
            {
                parse = new OKEPressureParse();
            }
        }

        /// <summary>
        /// 开始试验
        /// </summary>
        public override void StartAcquisition()
        {
            ///初始化串口
            if (_SerialPort == null)
            {
                _SerialPort = new SerialPort(Module.SpecialSetting.PortName, Module.SpecialSetting.PortBaud, Parity.None, 8, StopBits.One);
                _SerialPort.DataReceived += new SerialDataReceivedEventHandler(_SerialPort_DataReceived);
                _StringBuilder = new StringBuilder();
                _SendByte = new byte[] { 0XA5, 00, 0X00, 00, 0x00, 4, 0, 0, 0, 0, 0X5A };
            }

            ///打开串口并发送接受数据指令
            if (!_SerialPort.IsOpen && !IsFinished)
            {
                _SerialPort.Open();
            }

            while (!IsFinished)
            {
                _SerialPort.Write(_SendByte, 0, _SendByte.Length);
                Thread.Sleep(200);
            }

            ///试验结束关闭串口
            if (IsFinished && _SerialPort.IsOpen)
            {
                _SerialPort.Close();
            }
        }

        /// <summary>
        /// 接受数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                #region 解析数据
                byte[] bytes = new byte[_SerialPort.BytesToRead];
                _SerialPort.Read(bytes, 0, bytes.Length);
                //当字符串长度大于3且以'165'开头时认为当前返回数据是协议头数据
                Force = parse.Parse(bytes);

                #region 解析器正常工作后删除
                //if (bytes.Length > 0)
                //{
                //    if (bytes[0].ToString() == "165")
                //    {
                //        Abytes = bytes;
                //    }
                //    //当其他情况时,认为是协议中间数据,将其加到已知的头数据之后
                //    else
                //    {
                //        byte[] newbyte = new byte[Abytes.Length + bytes.Length];
                //        Abytes.CopyTo(newbyte, 0);
                //        bytes.CopyTo(newbyte, Abytes.Length);
                //        Abytes = newbyte;
                //    }
                //}
                //    //当经过初步处理的字符串长度达到11时
                //if (Abytes.Length >= 11)
                //{
                //    Force = (float.Parse(Abytes[7].ToString()) * 256 * 256 + float.Parse(Abytes[8].ToString()) * 256 + float.Parse(Abytes[9].ToString())) / 100.00f;
                //}


                #endregion
                #endregion

                #region 数据处理
                if (Module.SpecialSetting.IsDebug)
                {
                    log.WriteLog("[" + Module.SpecialSetting.MachineCompany + "] [" + Module.SpecialSetting.MachineType + "] Second=" + DateTime.Now.ToString() +
                        "; Force=" + Force + "; ", true, false);
                }
                if (!IsValidValue(Force))
                {
                    log.WriteLog("接收到无效数据：" + Force.ToString(), false, false);
                    return;
                }

                OnDataReceive(Force);

                if (Force > Module.SpecialSetting.MinValidValue)
                {
                    IsValidData = true;
                }
                if (Force < Module.SpecialSetting.MinFinishValue && IsValidData)
                {
                    IsValidData = false;
                    OnTestFinished(CurrentNumber);
                }

                #endregion
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message+"\r\n"+ex.StackTrace, false, false);
            }
        }
    }
}
