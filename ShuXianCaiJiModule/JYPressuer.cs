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
    /// 建仪压力机-测试通过
    /// </summary>
    public class JYPressuer:MachineBase
    {

        /// <summary>
        /// 串口对象
        /// </summary>
        SerialPort _SerialPort;
        /// <summary>
        /// 实时力值
        /// </summary>
        float Force = 0.00f;
        float OldForce = 0.00f;


        /// <summary>
        /// 解析数据
        /// </summary>
        StringBuilder _StringBuilder = null;

        byte[] _SendByte = null;

        byte[] Abytes=null;

        public JYPressuer(bool isTest)
        {
            if (isTest)
            {
                parse = new EmptyParse();
            }
            else
            {
                parse = new JYParse();
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
                _SendByte = new byte[] { 1, 3, 0, 17, 0, 2, 1, 22 };
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
                Abytes = new byte[_SerialPort.BytesToRead];
                _SerialPort.Read(Abytes, 0, Abytes.Length);
                Force=parse.Parse(Abytes);


                #region 保证解析器正常工作后删除
                //if (parse != null)
                //{
                //    Force = parse.Parse(Abytes);
                //}
                //else
                //{

                //    if (Abytes.Length != 0)
                //    {
                //        //建仪仪表的协议返回值不设定长,所以只能加条件长度大于7,头数据位1,第6位为4.注:此型号仪表用usb线接受数据符合格式的频率很慢,不能符合需求
                //        if (Abytes.Length > 7 && Abytes[0].ToString() == "1" && Abytes[5].ToString() == "4")
                //        {
                //            Force = (float.Parse(Abytes[6].ToString()) * 256 + float.Parse(Abytes[7].ToString())) / 10.0f;
                //        }
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
                if ((Force - OldForce) > 50)
                {
                    Force = OldForce;
                }
                else
                {
                    OldForce = Force;
                }
                OnDataReceive(Force);
                if (!IsValidData&&Force > Module.SpecialSetting.MinValidValue)
                {
                    IsValidData = true;
                }
                if (IsValidData&&Force < Module.SpecialSetting.MinFinishValue && IsValidData)
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
