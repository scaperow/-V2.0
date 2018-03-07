using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using FyComm32;

namespace ShuXianCaiJiHlperClient
{
    public class FYUnivers : IMachines
    {
        /// <summary>
        /// 使用log4net.dll日志接口实现日志记录
        /// </summary>
        private readonly log4net.ILog _Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region ISent 成员
        //单次试验数据获得事件
        public event DataReceiveDelegate DataReceive;
        //试验完成事件
        public event TestFinishedDelegate TestFinished;

        public int PortBaud //串口波特率
        { get; set; }
        public string PortName //串口名称
        { get; set; }
        public int CurrentNumber //试验试件总数
        { get; set; }
        public Boolean IsContinue //是否继续发送命令
        { get; set; }
        public Boolean IsFinished //是否处于有效数据区
        { get; set; }
        public Boolean IsRecordLog //是否开始记录运行日志
        { get; set; }

        public double MaxForce
        { get; set; }

        public double HForce
        { get; set; }

        public double LForce
        { get; set; }

        FyComm rfpcomm;            //丰仪万能机的联网接口组件
        float Force;                //存储上一次力值，避免 丰仪仪表不准确的情况发生
        /// <summary>
        /// 开始采集
        /// </summary>
        /// <param name="ControllerOrder"></param>
        public void StartAcquisition()
        {
            if (rfpcomm == null)
            {
                rfpcomm = new FyComm();
                rfpcomm.OnAddItem += new IFyCommEvents_OnAddItemEventHandler(rfpcomm_OnAddItem);

                rfpcomm.Baud = Convert.ToUInt32(PortBaud);
                rfpcomm.PortNo = Convert.ToInt32(PortName.Substring(3));
            }
            IsContinue = true;
            IsFinished = false;
            try
            {
                if (!rfpcomm.Active)
                {
                    rfpcomm.Active = true;
                }
                while (!IsContinue)
                {
                    rfpcomm.Active = false;
                }
            }
            catch (Exception ex)
            {
                _Log.Error(ex.ToString());
            }
        }

        /// <summary>
        /// 丰仪返回数据事件
        /// </summary>
        /// <param name="Force"></param>
        /// <param name="Disp"></param>
        /// <param name="Ext"></param>
        void rfpcomm_OnAddItem(float Second, float Force, float Disp, float Extend)
        {
            if ((Force - this.Force) < 35)
            {
                try
                {
                    DataReceive(Force);
                    if (IsContinue)
                    {
                        this.Force = Force;
                        //当力值低于5时,判断试验结束
                        if (Force < 10 && IsFinished)
                        {
                            IsFinished = false;
                            CurrentNumber++;
                            TestFinished(CurrentNumber);
                        }
                        if (Force > 10 && !IsFinished)
                        {
                            IsFinished = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _Log.Error(ex.ToString());
                }
            }
        }

        #endregion
    }
}
