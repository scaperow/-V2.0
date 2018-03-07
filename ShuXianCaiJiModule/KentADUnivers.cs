using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Ports;
using ShuXianCaiJiModule.parse;

namespace ShuXianCaiJiModule
{
    public class KentADUnivers : MachineBase
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
        byte[] bytes;

        public KentADUnivers(bool isTest)
        {
            if (isTest)
            {
                parse = new EmptyParse();
            }
            else
            {
                parse = new KentADParse();
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
                parse.SetModel(Module);
            }

            ///打开串口并发送接受数据指令
            if (!_SerialPort.IsOpen && !IsFinished)
            {
                _SerialPort.Open();
                //byte[] tempByte = DataEncoder.strToToHexByte("A5A5A5A5A5A5A5A5A5A5");
                //_SerialPort.Write(tempByte, 0, tempByte.Length);
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
                bytes = new byte[_SerialPort.BytesToRead];
                _SerialPort.Read(bytes, 0, bytes.Length);
                Force=parse.Parse(bytes);

                #region 保证解析器正常工作后删除

                if (Module.SpecialSetting.IsDebug)
                {
                    log.WriteLog("[" + Module.SpecialSetting.MachineCompany + "] [" + Module.SpecialSetting.MachineType + "] Second=" + DateTime.Now.ToString() +
                        "; Force=" + Force + "; ", true, false);
                }

                #endregion

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
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString(), false, false);
            }
        }
    }
}
