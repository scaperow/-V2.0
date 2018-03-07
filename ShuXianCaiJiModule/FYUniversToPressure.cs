using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShuXianCaiJiModule.parse;
using System.Threading;
using FyComm32;

namespace ShuXianCaiJiModule
{
    public class FYUniversToPressure : MachineBase
    {
        FyComm Comm;          //丰仪压力机的联网接口组件
        /// <summary>
        /// 开始采集
        /// </summary>
        /// <param name="ControllerOrder"></param>
        public override void StartAcquisition()
        {
            try
            {
                if (parse != null)
                {
                    FYParse _FYParse = parse as FYParse;
                    _FYParse.SwitchDevice(1);
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
                    Comm = new FyComm();
                    Comm.OnAddItem += new IFyCommEvents_OnAddItemEventHandler(Comm_OnAddItem);

                    Comm.Baud = Convert.ToUInt32(Module.SpecialSetting.PortBaud);
                    Comm.PortNo = Comm.GetPortNo(Module.SpecialSetting.PortName);
                    while (!IsFinished)
                    {
                        if (Comm.Active != true)
                        {
                            if (Module.SpecialSetting.IsDebug)
                            {
                                log.WriteLog("Comm.Active changed = true", true, false);
                            }
                            Comm.Active = true;
                        }
                        Thread.Sleep(500);
                    }
                    if (Module.SpecialSetting.IsDebug)
                    {
                        log.WriteLog("1Comm.Active changed = false", true, false);
                    }
                    Comm.Active = false;
                    if (Module.SpecialSetting.IsDebug)
                    {
                        log.WriteLog("2Comm.Active changed = false", true, false);
                    }
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
                OnDataReceive(Math.Round(Force, 2));
                if (Force > Module.SpecialSetting.MinValidValue)
                {
                    IsValidData = true;
                    if (Module.SpecialSetting.IsDebug)
                    {
                        log.WriteLog("IsValidData = true", true, false);
                    }
                }
                if (Force < Module.SpecialSetting.MinFinishValue && IsValidData)
                {
                    IsValidData = false;
                    OnTestFinished(CurrentNumber);

                    if (Module.SpecialSetting.IsDebug)
                    {
                        log.WriteLog("IsValidData = false, trigger ontestfinished", true, false);
                    }
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
