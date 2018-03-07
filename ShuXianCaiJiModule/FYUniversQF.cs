using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using FyComm32;
using ShuXianCaiJiModule.parse;

namespace ShuXianCaiJiModule
{
    public class FYUniversQF : MachineBase
    {
        /// <summary>
        /// 丰仪组件对象
        /// </summary>
        FyComm _Comm;

        /// <summary>
        /// 0：不启用读取屈服方法
        /// 1：启用读取屈服方法
        /// 2：实验开始
        /// 3：调用开始实验方法完成
        /// 4：实验结束,调用结束方法调用完成;读取屈服力值
        /// 5：读取屈服力值完成
        /// 6: 读取屈服失败或异常
        /// </summary>
        Int32 QFState = 1;

        /// <summary>
        /// 屈服力值
        /// </summary>
        float QFLLz = 0.00f;

        /// <summary>
        /// 屈服力值
        /// </summary>
        float QFHLz = 0.00f;

        /// <summary>
        /// 最大力值
        /// </summary>
        float MaxForce = 0.00f;

        double FinishForce = 0.00;

        bool IsFinish = true;

        public override void StartAcquisition()
        {
            try
            {
                if (parse!=null)
                {
                    FYTest(parse as FYParse);
                }
                else
                {
                    FYInit();

                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
        }

        private void FYInit()
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
                    if (Module.SpecialSetting.IsDebug)
                    {
                        log.WriteLog("MaxForce=0.00f", true, false);
                        log.WriteLog("FinishForce=0.00f", true, false);
                        log.WriteLog("QFState=1", true, false);
                    }
                    MaxForce = 0.00f;
                    FinishForce = 0.00f;
                    QFState = 1;
                    IsFinish = true;
                }
                try
                {
                    if (QFState == 2)
                    {
                        if (Module.SpecialSetting.IsDebug)
                        {
                            log.WriteLog("QFState==2", false, false);
                            log.WriteLog("_Comm.BeginTest", false, false);
                        }
                        _Comm.BeginTest();
                        QFState = 3;
                        if (Module.SpecialSetting.IsDebug)
                        {
                            log.WriteLog("QFState = 3", false, false);
                        }

                    }
                    else if (QFState == 4)
                    {
                        if (Module.SpecialSetting.IsDebug)
                        {
                            log.WriteLog("QFState ==4", false, false);
                            log.WriteLog("_Comm.EndTest", false, false);
                        }
                        _Comm.EndTest();
                        QFLLz = _Comm.FeL;
                        QFHLz = _Comm.FeH;
                        QFState = 5;
                        if (Module.SpecialSetting.IsDebug)
                        {
                            log.WriteLog("QFLLz = " + QFLLz.ToString()+";QFHLz="+QFHLz.ToString(), false, false);
                            log.WriteLog("QFState = 5", false, false);
                        }
                    }
                }
                catch (Exception exQF)
                {
                    QFState = 6;
                    if (Module.SpecialSetting.IsDebug)
                    {
                        log.WriteLog("QFState = 6", false, false);
                    }
                    log.WriteLog(exQF.Message + "\r\n" + exQF.StackTrace, true, true);
                }

                Thread.Sleep(500);
            }
            _Comm.Active = false;
        }

        private void FYTest(FYParse _FYParse)
        {
            FYParse _TComm = _FYParse;
            _TComm.OnAddItem += new FYParse.OnAddItemd(Comm_OnAddItem);

            _TComm._SerialPort.BaudRate = Convert.ToInt32(Module.SpecialSetting.PortBaud);
            _TComm._SerialPort.PortName = Module.SpecialSetting.PortName;
            while (!IsFinished)
            {
                if (_TComm.Active != true)
                {
                    if (Module.SpecialSetting.IsDebug)
                    {
                        log.WriteLog("_Comm.Active changed = true", true, false);
                    }
                    _TComm.isFomart = true;
                    _TComm.Active = true;
                    if (Module.SpecialSetting.IsDebug)
                    {
                        log.WriteLog("MaxForce=0.00f", true, false);
                        log.WriteLog("FinishForce=0.00f", true, false);
                        log.WriteLog("QFState=1", true, false);
                    }
                    MaxForce = 0.00f;
                    FinishForce = 0.00f;
                    QFState = 1;
                    IsFinish = true;
                }
                try
                {
                    if (QFState == 2)
                    {
                        if (Module.SpecialSetting.IsDebug)
                        {
                            log.WriteLog("QFState==2", false, false);
                            log.WriteLog("_Comm.BeginTest", false, false);
                        }
                        _TComm.BeginTest();
                        QFState = 3;
                        if (Module.SpecialSetting.IsDebug)
                        {
                            log.WriteLog("QFState = 3", false, false);
                        }

                    }
                    else if (QFState == 4)
                    {
                        if (Module.SpecialSetting.IsDebug)
                        {
                            log.WriteLog("QFState ==4", false, false);
                            log.WriteLog("_Comm.EndTest", false, false);
                        }
                        _TComm.EndTest();
                        QFLLz = _TComm.FeL;
                        QFHLz = _TComm.FeH;
                        QFState = 5;
                        if (Module.SpecialSetting.IsDebug)
                        {
                            log.WriteLog("QFState = 5", false, false);
                        }
                    }
                }
                catch (Exception exQF)
                {
                    QFState = 6;
                    if (Module.SpecialSetting.IsDebug)
                    {
                        log.WriteLog("QFState = 6", false, false);
                    }
                    log.WriteLog(exQF.Message + "\r\n" + exQF.StackTrace, true, true);
                }

                Thread.Sleep(500);
            }
            _TComm.Active = false;
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

                if (Force > Module.SpecialSetting.MinValidValue && IsFinish)
                {
                    MaxForce = 0.00f;
                    if (IsFinish)
                    {
                        if (Module.SpecialSetting.IsDebug)
                        {
                            log.WriteLog("IsFinish = false", false, false);
                        }
                        IsFinish = false;
                    }
                    IsValidData = true;
                    if (QFState == 1)
                    {
                        QFState = 2;
                        if (Module.SpecialSetting.IsDebug)
                        {
                            log.WriteLog("QFState = 2", false, false);
                        }
                    }
                }

                if (MaxForce < Force)
                {
                    MaxForce = Force;
                    FinishForce = MaxForce * Module.SpecialSetting.MinFinishValue;
                    if (Module.SpecialSetting.IsDebug)
                    {
                        log.WriteLog("FinishForce :" + FinishForce.ToString(), false, false);
                    }
                }

                if (Force < FinishForce && IsValidData)
                {
                    if (FinishForce > Module.SpecialSetting.MinValidValue)
                    {
                        FinishForce = Module.SpecialSetting.MinValidValue;
                        return;
                    }

                    if (Module.SpecialSetting.IsDebug)
                    {
                        log.WriteLog("FinishForce :FinishForce=" + FinishForce.ToString(), false, false);
                        log.WriteLog("MaxForce :MaxForce=" + MaxForce.ToString(), false, false);
                        log.WriteLog("QFState :QFState=" + QFState.ToString(), false, false);
                    }
                    if (QFState == 3)
                    {
                        if (Module.SpecialSetting.IsDebug)
                        {
                            log.WriteLog("QFState == 3", false, false);
                        }
                        QFState = 4;

                        if (Module.SpecialSetting.IsDebug)
                        {
                            log.WriteLog("QFState = 4", false, false);
                        }
                    }
                    if (QFState == 5)
                    {
                        OnTestFinished(CurrentNumber);
                        QFState = 6;
                    }

                    if (Force < Module.SpecialSetting.MinValidValue && (QFState>5||QFState==1))
                    {
                        IsFinish = true;
                        IsValidData = false;
                        if (QFState == 6)
                        {
                            QFState = 1;
                        }
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

        public bool GetQF(out float qfllz, out float qfhlz)
        {
            if (Module.SpecialSetting.IsDebug)
            {
                log.WriteLog("GetQF", false, false);
            }
            qfllz = 0.00f;
            qfhlz = 0.00f;
            qfllz = QFLLz;
            qfhlz = QFHLz;
            QFState = 1;
            if (Module.SpecialSetting.IsDebug)
            {
                log.WriteLog("QFState = 1", false, false);
                log.WriteLog("QFLLz:" + QFLLz + "qfhlz:" + QFHLz, false, false);
            }
            return true;
        }
    }
}
