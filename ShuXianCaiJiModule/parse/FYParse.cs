using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;

namespace ShuXianCaiJiModule.parse
{
    public class FYParse:JZParseBase
    {
        /// <summary>
        /// 是否为万能机
        /// True万能机
        /// False压力机
        /// </summary>
        private bool IsUnivers = true;

        /// <summary>
        /// 测试丰仪仪表
        /// True屈服实例
        /// False万能机实例
        /// </summary>
        public bool FYQF = true;

        public SerialPort _SerialPort = new SerialPort();

        private bool _Active = false;

        public bool Active
        {
            get { return _Active; }
            set
            {
                _Active = value;
                if (_Active)
                {
                    if (!_SerialPort.IsOpen)
                    {
                        _SerialPort.Open();
                    }
                }
                else
                {
                    if (_SerialPort.IsOpen)
                    {
                        _SerialPort.Close();
                    }
                }
            }
        }

        public bool isFomart = false;

        /// <summary>
        /// 下屈服
        /// </summary>
        public float FeL = 0.0f;

        /// <summary>
        /// 上屈服
        /// </summary>
        public float FeH = 0.0f;

        /// <summary>
        /// 事件委托
        /// </summary>
        /// <param name="Second">事件</param>
        /// <param name="Force">力值</param>
        /// <param name="Disp"></param>
        /// <param name="Extend"></param>
        public delegate void OnAddItemd(float Second, float Force, float Disp, float Extend);

        /// <summary>
        /// 读数事件
        /// </summary>
        public event OnAddItemd OnAddItem;


        StringBuilder _StringBuilder = new StringBuilder();
        float Force = 0.0f;

        public FYParse()
        {
            _SerialPort.StopBits = StopBits.One;
            _SerialPort.DataBits = 8;
            _SerialPort.Parity = Parity.None;
 _SerialPort.DataReceived+=new SerialDataReceivedEventHandler(_SerialPort_DataReceived);
        }


        /// <summary>
        /// 压力机调用
        /// </summary>
        /// <param name="IsPressure"></param>
        public void SwitchDevice(int IsPressure)
        {
            IsUnivers = false;
        }

        /// <summary>
        /// 开始实验
        /// </summary>
        public void BeginTest()
        {
            FeL = 0.00f;
            FeH = 0.00f;
        }

        /// <summary>
        /// 结束实验
        /// </summary>
        public void EndTest()
        {
            FeL = 66.88f;
            FeH = 77.99f;
        }

        private void _SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] tempbyte = new byte[_SerialPort.BytesToRead];
            _SerialPort.Read(tempbyte, 0, tempbyte.Length);
            _StringBuilder.Append(System.Text.Encoding.UTF8.GetString(tempbyte));

            while (_StringBuilder.ToString().IndexOf("a") >= 0 && _StringBuilder.ToString().IndexOf("b") > 0 && _StringBuilder.ToString().IndexOf("a") < _StringBuilder.ToString().IndexOf("b"))
            {
                try
                {
                    Force = float.Parse(_StringBuilder.ToString().Substring(_StringBuilder.ToString().IndexOf("a") + 1, _StringBuilder.ToString().IndexOf("b") - _StringBuilder.ToString().IndexOf("a") - 1));
                    _StringBuilder.Remove(0, _StringBuilder.ToString().IndexOf("b") + 1);
                    if (OnAddItem != null)
                    {
                        OnAddItem(0.00f, Force, 0.00f, 0.00f);
                    }
                }
                catch
                {
                    _StringBuilder.Remove(0, _StringBuilder.ToString().IndexOf("b") + 1);
                }
            }
        }

        #region JZParseBase 成员

        public float Parse(object obj)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region JZParseBase 成员


        public void SetModel(SXCJModule _SXCJModule)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
