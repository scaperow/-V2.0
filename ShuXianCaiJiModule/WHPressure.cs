using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using ShuXianCaiJiModule.parse;

namespace ShuXianCaiJiModule
{
    public class WHPressure:MachineBase
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

        public WHPressure(bool isTest)
        {
            if (isTest)
            {
                parse = new EmptyParse();
            }
            else
            {
                parse = new WHParse();
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
            }

            ///打开串口并发送接受数据指令
            if (!_SerialPort.IsOpen && !IsFinished)
            {
                _SerialPort.Open();
            }

            while (!IsFinished)
            {
                _SerialPort.Write("F");
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
                byte[] temp = new byte[_SerialPort.BytesToRead];
                _SerialPort.Read(temp, 0, temp.Length);

                Force=parse.Parse(temp);

                #region 保证解析器正常工作后删除
                //    int i = 0;
                //    int DataLength = _SerialPort.BytesToRead;
                //    while (i < DataLength)
                //    {
                //        byte[] dbs = new byte[1024];
                //        int len = _SerialPort.Read(dbs, 0, 1024);
                //        _StringBuilder.Append(ASCIIEncoding.Default.GetString(dbs, 0, len));
                //        i += len;
                //    }

                //    if (_StringBuilder.ToString().LastIndexOf("F") < _StringBuilder.ToString().LastIndexOf("$") && _StringBuilder.ToString().LastIndexOf("F") > -1)
                //    {
                //        Force = float.Parse(_StringBuilder.ToString().Substring(1, 7));
                //        _StringBuilder.Remove(0, _StringBuilder.ToString().IndexOf("$") + 1);
                //    }
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
                log.WriteLog(ex.ToString(), false, false);
            }
        }
    }
}
