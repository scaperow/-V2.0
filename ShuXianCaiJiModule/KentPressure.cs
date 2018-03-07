﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Threading;
using ShuXianCaiJiModule.parse;

namespace ShuXianCaiJiModule
{
    public class KentPressure : MachineBase
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

        public KentPressure(bool isTest)
        {
            if (isTest)
            {
                parse = new EmptyParse();
            }
            else
            {
                parse = new KentParse();
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
            if (!_SerialPort.IsOpen&&!IsFinished)
            {
                _SerialPort.Open(); ;
            }
            while (!IsFinished)
            {
                Thread.Sleep(500);
            }
            ///试验结束关闭串口
            if (IsFinished && _SerialPort.IsOpen)
            {
                _SerialPort.Write("F");
                Thread.Sleep(200);
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
                Force=parse.Parse(bytes);

                #region 保证解析器正常工作后删除
                //for (int i = 0; i < bytes.Length; i++)
                //{
                //    _StringBuilder.Append(bytes[i].ToString("X2"));
                //}
                //int n = _StringBuilder.ToString().IndexOf("F0");
                //if (_StringBuilder.ToString().IndexOf("F0") > 0)
                //{
                //    _StringBuilder.Remove(0, _StringBuilder.ToString().IndexOf("FO"));
                //}
                //while (_StringBuilder.Length >= 30)
                //{
                //    if (_StringBuilder.Length >= 30)
                //    {
                //        if (_StringBuilder.ToString().Substring(5, 1) == "2")
                //        {
                //            Force = float.Parse(_StringBuilder.ToString().Substring(6, 6)) / 100.00f;
                //        }
                //        else if (_StringBuilder.ToString().Substring(5, 1) == "1")
                //        {
                //            Force = float.Parse(_StringBuilder.ToString().Substring(6, 6)) / 10f;
                //        }
                //        _StringBuilder.Remove(0, _StringBuilder.ToString().IndexOf("0F") + 2);
                //    }

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
                log.WriteLog(ex.ToString(), false, false);
                log.WriteLog(_StringBuilder.ToString(), false, false);
                _StringBuilder.Remove(0, _StringBuilder.ToString().IndexOf("0F") + 2);
            }
        }
    }
}
