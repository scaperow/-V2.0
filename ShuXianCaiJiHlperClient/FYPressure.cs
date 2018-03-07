using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FyComm32;

namespace ShuXianCaiJiHlperClient
{
    /// <summary>
    /// 丰仪压力机
    /// </summary>
    public class FYPressure:IMachines
    {


        /// <summary>
        /// 使用log4net.dll日志接口实现日志记录
        /// </summary>
        private static readonly log4net.ILog _Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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

        FyComm Comm;          //丰仪压力机的联网接口组件
        /// <summary>
        /// 开始采集
        /// </summary>
        /// <param name="ControllerOrder"></param>
        public void StartAcquisition()
        {
            Comm = new FyComm();
            Comm.SwitchDevice(1);
            Comm.OnAddItem += new IFyCommEvents_OnAddItemEventHandler(Comm_OnAddItem);

            Comm.Baud = Convert.ToUInt32(PortBaud);
            Comm.PortNo = Comm.GetPortNo(PortName);

            IsContinue = true;
            IsFinished = false;
            try
            {
                Comm.Active = true;

                while (!IsContinue)
                {
                    Comm.Active = false;
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
        /// <param name="Milliseconds"></param>
        /// <param name="Force"></param>
        /// <param name="Disp"></param>
        /// <param name="Extend"></param>
        void Comm_OnAddItem(float Second, float Force, float Disp, float Extend)
        {
            try
            {
                DataReceive(Force);

                if (Force > 50)
                {
                    IsFinished = true;
                }
                //当力值>50时,开始正式采集数据,<30则认为是当前试件采数结束                           
                if (!(Force > 30) && IsFinished)
                {
                    IsFinished = false;
                    CurrentNumber++;
                    TestFinished(CurrentNumber);
                }
            }
            catch (Exception ex)
            {
                _Log.Error(ex.ToString());
            }
        }
        #endregion

    }
}
