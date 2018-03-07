using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FyComm32;
using System.Threading;

namespace ShuXianCaiJiModule
{
    public class FYIPressure:MachineBase
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
                if (IsUnitTest)
                {
                    RunUnitTest();
                }
                else
                {
                    Comm = new FyComm();
                    Comm.EnabledUDP(true);
                    Comm.SwitchDevice(1);
                    Comm.OnAddItem += new IFyCommEvents_OnAddItemEventHandler(Comm_OnAddItem);

                    Comm.Host = Module.SpecialSetting.RemotIP;
                    Comm.HostPort = Module.SpecialSetting.RemotPort;
                    Comm.LocalPort = Module.SpecialSetting.LocalPort;
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
                    Comm.Active = false;
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
