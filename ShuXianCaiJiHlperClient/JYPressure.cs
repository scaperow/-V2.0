using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace ShuXianCaiJiHlperClient
{
    public class JYPressure : IMachines
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

        SerialPort SerialPort;
        /// <summary>
        /// 开始采集
        /// </summary>
        /// <param name="ControllerOrder"></param>
        public void StartAcquisition()
        {
            SerialPort = new SerialPort(PortName, PortBaud, Parity.None, 8, StopBits.One);
            SerialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
            SerialPort.Open();
            IsContinue = true;
            IsFinished = false;
            try
            {
                while (IsContinue)
                {
                    byte[] s = new byte[] { 1, 3, 0, 17, 0, 2, 1, 22 };
                    SerialPort.Write(s, 0, s.Length);
                }
                if (SerialPort.IsOpen)
                {
                    SerialPort.Close();
                }
            }
            catch (Exception ex)
            {
                _Log.Error(ex.ToString());
            }
        }
        byte[] Abytes;              //全局变量字节数组,用于配合bytes分析字节数组数据,判断当前信息是否获取完整
        double JYYLJBD;              //记录建仪仪表压力机力值达到最大且稳固了的力值   
        int bj;                     //用于建仪的压力机,标记当前试验是否结束

        /// <summary>
        /// 串口返回信息触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                Abytes = new byte[SerialPort.BytesToRead];
                SerialPort.Read(Abytes, 0, Abytes.Length);
                if (Abytes.Length != 0)
                {
                    //建仪仪表的协议返回值不设定长,所以只能加条件长度大于7,头数据位1,第6位为4.注:此型号仪表用usb线接受数据符合格式的频率很慢,不能符合需求
                    if (Abytes.Length > 7 && Abytes[0].ToString() == "1" && Abytes[5].ToString() == "4")
                    {
                        string HForce = Abytes[6].ToString();   //高位数为第7位
                        string LForce = Abytes[7].ToString();   //低位数为第8位
                        double Force = (Convert.ToDouble(HForce) * 256 + Convert.ToDouble(LForce)) / 10;  //用高低位计算力值

                        //目前为止,建仪仪表遇到的因传感器问题返回的无关数据

                        //记录固定值时,不统计0
                        if (Force > 0 && Force < 3000)
                        {
                            DataReceive(Force);

                            //建仪压力机仪表自有其特点.在操作员松油后力值降到0又返回到最大值并持续不动,所以建仪仪表设定为当力值持续不变8次的时候,认为试验结束.
                            if (Force == JYYLJBD)
                            {
                                bj++;
                                String text = " 力值 =  " + Force.ToString("00.00");
                                if (bj > 8)
                                {
                                    IsFinished = false;
                                    CurrentNumber++;
                                    TestFinished(CurrentNumber);
                                }
                            }
                            else
                            {
                                //当力值改变时,计数器清零
                                bj = 0;
                                JYYLJBD = Force;
                                IsFinished = true;
                            }
                        }

                    }
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
