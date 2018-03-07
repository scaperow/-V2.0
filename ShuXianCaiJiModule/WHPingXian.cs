using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Runtime.InteropServices;

namespace ShuXianCaiJiModule
{
    public class WHPingXian : MachineBase
    {

        [DllImport("CH365DLL.dll")]
        private extern static Boolean OpenDevice();

        [DllImport("CH365DLL.dll")]
        private extern static Boolean CloseDevice();

        [DllImport("CH365DLL.dll")]
        private extern static long AD1();
        [DllImport("CH365DLL.dll")]
        private extern static Boolean SetAd1210Zero(byte code);

        /// <summary>
        /// 实时力值
        /// </summary>
        float Force = 0.00f;


        /// <summary>
        /// 开始试验
        /// </summary>
        public override void StartAcquisition()
        {
            if (!OpenDevice())
            {
                log.WriteLog("未能与设备通信，请检查连接", false, false);
            }
            while (!IsFinished)
            {
                ReceiveData();
                Thread.Sleep(150);
            }

            ///试验结束关闭串口
            if (IsFinished)
            {
                CloseDevice();
            }
        }

        /// <summary>
        /// 接受数据
        /// </summary>
        void ReceiveData()
        {
            try
            {
                string x = AD1().ToString("X2");
                int y = Convert.ToInt32(x.Substring(x.Length - 8, 8), 16);

                Force = (float)((y - Module.SpecialSetting.DPlace) * Module.SpecialSetting.SpecialD / 100000);

                log.WriteLog("yhxyhx" + x + "||" + y.ToString() + "||" + Force.ToString(), true, false);

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
