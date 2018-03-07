using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace ShuXianCaiJiHlperClient
{
    public class WHUnivers : IMachines
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
            try
            {
                SerialPort = new SerialPort(PortName, PortBaud, Parity.None, 8, StopBits.One);
                SerialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
                SerialPort.Open();
                IsContinue = true;
                IsFinished = false;

                while (IsContinue)
                {
                    SerialPort.Write("F");
                    Thread.Sleep(200);
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
        String text;                //全局变量文本,用于在端口接受数据事件中获取信息后在另外的线程中进行分析

        /// <summary>
        /// 串口返回信息触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int i = 0;
                int DataLength = SerialPort.BytesToRead;

                string returnStr = "";
                StringBuilder sb = new StringBuilder();
                while (i < DataLength)
                {
                    byte[] dbs = new byte[1024];
                    int len = SerialPort.Read(dbs, 0, 1024);
                    sb.Append(ASCIIEncoding.Default.GetString(dbs, 0, len));
                    i += len;
                }
                //将仪表返回的数据转化为字符串
                returnStr = sb.ToString();
                //如果字符串以'F'开头,则认为是数据起点
                if (returnStr.Contains("F"))
                {
                    text = returnStr;
                }
                //其他情况均顺序加到字符串结尾
                else
                {
                    text += returnStr;
                }

                if (text.Substring(text.Length - 1, 1) == "$")
                {
                    string force = text.Substring(1, 7);  //实时力值为字符串的2-8位
                    string disp = text.Substring(14, 7);  //位移为字符串的14-20位

                    //当力值>10KN时,开始正式采集数据
                    if (Convert.ToDouble(force) > 10)
                    {
                        DataReceive(force);
                        IsFinished = true;
                    }
                    //因为万能机油门不如压力机稳定,会出现波动现象,因此将判断试验结束的标准线定为5KN,防止因波动导致的误差
                    if (!(Convert.ToDouble(force) > 5))
                    {
                        IsFinished = false;
                        CurrentNumber++;
                        TestFinished(CurrentNumber);
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
