using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using ShuXianCaiJiModule.parse;

namespace ShuXianCaiJiModule
{
    public class KentSXUnivers:MachineBase
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
        /// 指令接受不全，保存字节数组
        /// </summary>
        byte[] Abytes;

        public KentSXUnivers(bool isTest)
        {

            if (isTest)
            {
                parse = new EmptyParse();
            }
            else
            {
                parse = new KentSXParse();
            }
        }

        /// <summary>
        /// 开始采集
        /// </summary>
        public override void StartAcquisition()
        {
            ///初始化串口
            if (_SerialPort == null)
            {
                _SerialPort = new SerialPort(Module.SpecialSetting.PortName, Module.SpecialSetting.PortBaud, Parity.None, 8, StopBits.One);
                _SerialPort.DataReceived += new SerialDataReceivedEventHandler(_SerialPort_DataReceived);
                _StringBuilder = new StringBuilder();
            }

            ///打开串口并发送接受数据指令
            if (!_SerialPort.IsOpen && !IsFinished)
            {
                _SerialPort.Open();
                byte[] tempByte = DataEncoder.strToToHexByte("A5 A5 A5 A5 A5 A5 A5 A5 A5 A5");
                _SerialPort.Write(tempByte, 0, tempByte.Length);
            }
            while (!IsFinished)
            {
                Thread.Sleep(500);
            }
            ///试验结束关闭串口
            if (IsFinished && _SerialPort.IsOpen)
            {
                _SerialPort.Close();
            }
        }

        /// <summary>
        /// 串口返回信息触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                byte[] bytes = new byte[_SerialPort.BytesToRead];
                _SerialPort.Read(bytes, 0, bytes.Length);
                //当返回二进制数据打头为'240'时,认为是协议头数据
                Force=parse.Parse(bytes);

                #region 解析器正常工作后删除

                //if (bytes[0] == 240)
                //{
                //    Abytes = bytes;
                //}
                ////当其他情况时,认为是协议中间数据,将其加到已知的头数据之后
                //else
                //{
                //    byte[] newbyte = new byte[Abytes.Length + bytes.Length];
                //    Abytes.CopyTo(newbyte, 0);
                //    bytes.CopyTo(newbyte, Abytes.Length);
                //    Abytes = newbyte;
                //}
                //#region 解析力值
                //int low3 = Abytes[3] & 0xf;
                //int high3 = (Abytes[3] >> 4) * 10;

                //int low2 = Abytes[2] & 0xf;
                //int high2 = (Abytes[2] >> 4) * 10;

                //int low1 = Abytes[1] & 0xf;
                //int high1 = (Abytes[1] >> 4) * 10;

                //Force = (float)(((Convert.ToDouble(low1 + high1) * 10000 + Convert.ToDouble(low2 + high2) * 100 + Convert.ToDouble(low3 + high3)) / 10));
                //#endregion
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
                log.WriteLog(ex.ToString(), false, false);
            }
        }
    }
}
