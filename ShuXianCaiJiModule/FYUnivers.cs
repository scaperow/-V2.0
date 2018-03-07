using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FyComm32;
using System.Threading;
using ShuXianCaiJiModule.parse;

namespace ShuXianCaiJiModule
{
    public class FYUnivers:MachineBase
    {
        /// <summary>
        /// 丰仪组件对象
        /// </summary>
        FyComm _Comm;

        public override void StartAcquisition()
        {
            try
            {
                if (parse!=null)
                {
                    FYParse _FYParse = parse as FYParse;
                    _FYParse.OnAddItem += new FYParse.OnAddItemd(Comm_OnAddItem);

                    _FYParse._SerialPort.BaudRate = Convert.ToInt32(Module.SpecialSetting.PortBaud);
                    _FYParse._SerialPort.PortName = Module.SpecialSetting.PortName; 
                    while (!IsFinished)
                    {
                        if (_FYParse.Active != true)
                        {
                            if (Module.SpecialSetting.IsDebug)
                            {
                                log.WriteLog("_Comm.Active changed = true", true, false);
                            }
                            _FYParse.Active = true;
                        }
                        Thread.Sleep(500);
                    }
                    _FYParse.Active = false;
                }
                else
                {
                    _Comm = new FyComm();
                    _Comm.OnAddItem += new IFyCommEvents_OnAddItemEventHandler(Comm_OnAddItem);

                    _Comm.Baud = Convert.ToUInt32(Module.SpecialSetting.PortBaud);
                    _Comm.PortNo = Convert.ToInt32(Module.SpecialSetting.PortName.Substring(3));

                    while (!IsFinished)
                    {
                        if (_Comm.Active != true)
                        {
                            if (Module.SpecialSetting.IsDebug)
                            {
                                log.WriteLog("_Comm.Active changed = true", true, false);
                            }
                            _Comm.Active = true;
                        }
                        Thread.Sleep(500);
                    }
                    _Comm.Active = false;

                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
        }

        /// <summary>
        /// 丰仪返回数据事件
        /// </summary>
        /// <param name="Milliseconds"></param>
        /// <param name="Force"></param>
        /// <param name="Disp"></param>
        /// <param name="Extend"></param>
        public void Comm_OnAddItem(float Second, float Force, float Disp, float Extend)
        {
            try
            {
                if (Module.SpecialSetting.IsDebug)
                {
                    log.WriteLog("[" + Module.SpecialSetting.MachineCompany + "] [" + Module.SpecialSetting.MachineType + "] Second=" + Second +
                        "; Force=" + Force + "; Disp=" + Disp + "; Extend=" + Extend, true, false);
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
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message, false, false);
            }
        }

        protected override void TriggerDataReceive(float f)
        {
            Comm_OnAddItem(0f, f, 0f, 0f);
        }
    }
}
