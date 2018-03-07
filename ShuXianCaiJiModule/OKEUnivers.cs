using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Runtime.InteropServices;
using ShuXianCaiJiModule.parse;

namespace ShuXianCaiJiModule
{
    /// <summary>
    /// 欧凯万能机-采数测试通过
    /// </summary>
    public class OKEUnivers : MachineBase
    {
        /// <summary>
        /// 串口对象
        /// </summary>
        SerialPort _SerialPort;
        /// <summary>
        /// 实时力值
        /// </summary>
        float Force = 0.00f;

        /// <summary>
        /// 解析数据
        /// </summary>
        StringBuilder _StringBuilder = null;

        /// <summary>
        /// 发送指令byte数组
        /// </summary>
        byte[] _SendByte = null;

        /// <summary>
        /// 存储未接受完的byte数组
        /// </summary>
        byte[] Abytes = null;

        /// <summary>
        /// 读取屈服的次数
        /// </summary>
        int QFCount = 0;



        /// <summary>
        /// 获取屈服
        /// 0：未获取屈服获取屈服获取成功
        /// 1：获取屈服
        /// 2：屈服未准备
        /// 3：屈服准备好
        /// 4：读取屈服
        /// 5: 读取屈服成功
        /// 6：用户按清零键，屈服不能读取，屈服0.00f
        /// </summary>
        Int32 QFSate = 0;

        /// <summary>
        /// 屈服力值
        /// </summary>
        float QFLz = 0.00f;

        public OKEUnivers(bool isTest)
        {
            if (isTest)
            {
                parse = new EmptyParse();
            }
            else
            {
                parse = new OKEUniversParse();
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
                _SendByte = new byte[] { 0XFD, 0x07, 0x11, 0x00, 0x00, 0x00, 0x00, 0x00, 0x15 };
            }

            ///打开串口并发送接受数据指令
            if (!_SerialPort.IsOpen && !IsFinished)
            {
                _SerialPort.Open();
                QFSate = 0;
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
                byte[] bytes = new byte[_SerialPort.BytesToRead];
                if (bytes != null && bytes.Length > 0)
                {
                    _SerialPort.Read(bytes, 0, bytes.Length);
                    if (bytes.Length > 0)
                    {
                        //当返回二进制数据打头为'253'时,认为是协议头数据
                        if (bytes[0] == 253)
                        {
                            Abytes = bytes;
                        }
                        //当其他情况时,认为是协议中间数据,将其加到已知的头数据之后
                        else
                        {
                            byte[] newbyte = new byte[Abytes.Length + bytes.Length];
                            Abytes.CopyTo(newbyte, 0);
                            bytes.CopyTo(newbyte, Abytes.Length);
                            Abytes = newbyte;
                        }
                    }
                }
                if (Abytes.Length == 9)
                {
                    //欧凯万能机实时力值的计算方式及可用位数
                    if (QFSate != 0)
                    {

                        if (QFSate == 1)
                        {
                            if (Module.SpecialSetting.IsDebug)
                            {
                                log.WriteLog("S-QFSate==1", false, false);
                                log.WriteLog(Abytes[2].ToString() + "---------" + Abytes[4].ToString(), false, false);
                            }
                            if ((Abytes[2] == 208 || Abytes[2] == 224) && Abytes[4] == 85)
                            {
                                QFSate = 3;
                                if (Module.SpecialSetting.IsDebug)
                                {
                                    log.WriteLog("S-QFSate = 3", false, false);
                                }
                            }
                            else if ((Abytes[2] == 209 || Abytes[2] == 225) && Abytes[4] == 0)
                            {
                                QFSate = 6;
                            }
                            else
                            {
                                QFSate = 2;
                                if (Module.SpecialSetting.IsDebug)
                                {
                                    log.WriteLog("S-QFSate = 2", false, false);
                                }
                            }
                        }
                        else if (QFSate == 4)
                        {
                            if (Module.SpecialSetting.IsDebug)
                            {
                                log.WriteLog("S-QFSate==4", false, false);
                                log.WriteLog(Abytes[2].ToString(), false, false);
                            }
                            if (Abytes[2] == 226 || Abytes[2] == 210)
                            {
                                if (Module.SpecialSetting.IsDebug)
                                {
                                    log.WriteLog(Abytes[0] + " " + Abytes[1] + " " + Abytes[2] + " " + Abytes[3] + " " + Abytes[4] + " " + Abytes[5] + " " + Abytes[6] + " " + Abytes[7] + " " + Abytes[8] + " ", false, false);
                                }
                                Ram4 _Ram4 = new Ram4();
                                _Ram4.C_HH = Abytes[7];
                                _Ram4.C_HI = Abytes[6];
                                _Ram4.C_LO = Abytes[5];
                                _Ram4.C_LL = Abytes[4];
                                QFLz = _Ram4.FValue/1000.00f;
                                QFSate = 5;
                                if (Module.SpecialSetting.IsDebug)
                                {
                                    log.WriteLog("S-QFSate = 5", false, false);
                                }
                            }
                            else
                            {
                                QFSate = 3;
                                if (Module.SpecialSetting.IsDebug)
                                {
                                    log.WriteLog("S-QFSate = 3", false, false);
                                }
                            }
                        }
                    }
                    else
                    {
                        Force = float.Parse((Abytes[6] * 256 * 256 + Abytes[5] * 256 + Abytes[4]).ToString()) / 1000.00f;
                    }
                }
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
                log.WriteLog(ex.StackTrace, false, false);
            }
        }

        /// <summary>
        /// 获取下屈服
        /// </summary>
        /// <returns></returns>
        public float GetQuFuL()
        {
            if (Module.SpecialSetting.IsDebug)
            {
                log.WriteLog("GetQuFuL被调用", false, false);
            }
            try
            {
                if (Module.SpecialSetting.IsDebug)
                {
                    log.WriteLog("开始读取屈服", false, false);
                }
                QFSate = 0;
                while (true)
                {
                    if (QFSate == 0 || QFSate == 2 || QFSate == 1)
                    {
                        QFLz = 0.00f;
                        if (Module.SpecialSetting.IsDebug)
                        {
                            log.WriteLog("QFSate == 0 || QFSate == 2", false, false);
                        }
                        try
                        {
                            if (!_SerialPort.IsOpen)
                            {
                                _SerialPort.Open();
                            }
                        }
                        catch (Exception ex)
                        {
                            log.WriteLog(ex.StackTrace, true, false);
                            QFSate = 5;
                            QFLz = 0.00F;
                            QFCount = 0;
                            break;
                        }
                        byte[] _TempSendByte = new byte[] { 0XFD, 0x07, 0xE0, 0x00, 0x00, 0x00, 0x00, 0x00, 0xE4 };
                        _SerialPort.Write(_TempSendByte, 0, _TempSendByte.Length);
                        QFSate = 1;
                        if (QFCount >= 40)
                        {
                            QFCount = 0;
                            QFSate = 5;
                            QFLz = 0.00F;
                            if (Module.SpecialSetting.IsDebug)
                            {
                                log.WriteLog("QFSate = 1", true, false);
                            }

                        }
                        else
                        {
                            QFCount++;
                            if (Module.SpecialSetting.IsDebug)
                            {
                                log.WriteLog("QFSate = 1", true, false);
                            }
                        }
                    }
                    else if (QFSate == 3)
                    {
                        if (Module.SpecialSetting.IsDebug)
                        {
                            log.WriteLog("QFSate == 3", true, false);
                        }
                        try
                        {
                            if (!_SerialPort.IsOpen)
                            {
                                _SerialPort.Open();
                            }
                        }
                        catch (Exception ex)
                        {
                            log.WriteLog(ex.StackTrace, true, false);
                            QFSate = 5;
                            QFLz = 0.00F;
                            QFCount = 0;
                            break;
                        }
                        byte[] _TempSendByte = new byte[] { 0XFD, 0x07, 0xE2, 0x00, 0x00, 0x00, 0x00, 0x00, 0xE6 };
                        _SerialPort.Write(_TempSendByte, 0, _TempSendByte.Length);
                        if (QFCount == 20)
                        {
                            QFLz = 0.00F;
                            QFSate = 5;
                            if (Module.SpecialSetting.IsDebug)
                            {
                                log.WriteLog("QFSate = 5", true, false);
                            }
                            QFCount = 0;
                        }
                        else
                        {
                            QFCount++;
                            QFSate = 4;
                            if (Module.SpecialSetting.IsDebug)
                            {
                                log.WriteLog("QFSate = 4", true, false);
                            }
                        }
                    }
                    else if (QFSate == 5)
                    {
                        if (Module.SpecialSetting.IsDebug)
                        {
                            log.WriteLog("QFSate == 5", true, false);
                        }
                        QFSate = 0;
                        if (Module.SpecialSetting.IsDebug)
                        {
                            log.WriteLog("QFSate = 0", true, false); 
                            log.WriteLog("QFLz = " + QFLz.ToString(), false, false);
                        }
                        if (IsFinished && _SerialPort.IsOpen)
                        {
                            _SerialPort.Close();
                        }
                        QFCount = 0;
                        return QFLz;
                    }
                    else if (QFSate == 6)
                    {
                        QFLz = 0.00f;
                        break;
                    }
                    Thread.Sleep(200);
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.StackTrace, true, false);
            }
            return QFLz;
        }

        /// <summary>
        /// 获取上屈服
        /// </summary>
        /// <returns></returns>
        public float GetQuFuH()
        {
            if (Module.SpecialSetting.IsDebug)
            {
                log.WriteLog("GetQuFuH被调用", true, false);
            }
            QFSate = 0;
            try
            {
                while (true)
                {
                    if (QFSate == 0 || QFSate == 2)
                    {
                        QFLz = 0.00f;
                        if (Module.SpecialSetting.IsDebug)
                        {
                            log.WriteLog("QFSate == 0 || QFSate == 2", true, false);
                        }
                        try
                        {
                            if (!_SerialPort.IsOpen)
                            {
                                _SerialPort.Open();
                            }
                        }
                        catch (Exception ex)
                        {
                            log.WriteLog(ex.StackTrace, true, false);
                            QFSate = 5;
                            QFLz = 0.00F;
                            QFCount = 0;
                            break;
                        }
                        byte[] _TempSendByte = new byte[] { 0XFD, 0x07, 0xD0, 0x00, 0x00, 0x00, 0x00, 0x00, 0xD4 };
                        _SerialPort.Write(_TempSendByte, 0, _TempSendByte.Length);
                        QFSate = 1;
                        if (QFCount == 40)
                        {
                            QFLz = 0.00F;
                            QFSate = 5;
                        }
                        else
                        {
                            QFCount++;
                            if (Module.SpecialSetting.IsDebug)
                            {
                                log.WriteLog("QFSate = 1", true, false);
                            }
                        }
                    }
                    else if (QFSate == 3)
                    {
                        if (Module.SpecialSetting.IsDebug)
                        {
                            log.WriteLog("QFSate == 3", true, false);
                        }
                        try
                        {
                            if (!_SerialPort.IsOpen)
                            {
                                _SerialPort.Open();
                            }
                        }
                        catch (Exception ex)
                        {
                            log.WriteLog(ex.StackTrace, true, false);
                            QFSate = 5;
                            QFLz = 0.00F;
                            QFCount = 0;
                            break;
                        }
                        byte[] _TempSendByte = new byte[] { 0XFD, 0x07, 0xD2, 0x00, 0x00, 0x00, 0x00, 0x00, 0xD6 };
                        _SerialPort.Write(_TempSendByte, 0, _TempSendByte.Length);
                        if (QFCount == 20)
                        {
                            QFLz = 0.00F;
                            QFSate = 5;
                        }
                        else
                        {
                            QFCount++;
                            QFSate = 4;
                            if (Module.SpecialSetting.IsDebug)
                            {
                                log.WriteLog("QFSate = 4", true, false);
                            }
                        }
                    }
                    else if (QFSate == 5)
                    {
                        if (Module.SpecialSetting.IsDebug)
                        {
                            log.WriteLog("QFSate == 5", true, false);
                        }
                        QFSate = 0;
                        if (Module.SpecialSetting.IsDebug)
                        {
                            log.WriteLog("QFSate = 0", true, false);
                            log.WriteLog("QFLz = " + QFLz.ToString(), false, false);
                        }
                        if (IsFinished && _SerialPort.IsOpen)
                        {
                            _SerialPort.Close();
                        }
                        QFCount = 0;
                        return QFLz;
                    }
                    else if (QFSate == 6)
                    {
                        QFLz = 0.00f;
                        break;
                    }
                    Thread.Sleep(200);
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.StackTrace, true, false);
            }
            return QFLz;
        }

        [StructLayout(LayoutKind.Explicit)]     ///定义这个共用体
        public struct Ram4
        {
            [FieldOffset(0)]
            public float FValue;		
            [FieldOffset(0)]
            public Int32 IValue;
            [FieldOffset(0)]
            public UInt32 UIValue;
            [FieldOffset(0)]
            public byte C_LL;	
            [FieldOffset(1)]
            public byte C_LO;	
            [FieldOffset(2)]
            public byte C_HI;	
            [FieldOffset(3)]
            public byte C_HH;	
        }
    }
}

