using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BizCommon;
using System.IO;
using System.Threading;

namespace ShuXianCaiJiModule
{
    public class FYTest
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


        private bool _Active = false;

        public bool Active
        {
            get { return _Active; }
            set
            {
                _Active = value;
                Thread _Thread=null;
                if (_Active)
                {
                    if (isFomart)
                    {
                        _Thread = new Thread(GetTestData);
                        _Thread.IsBackground = true;
                        _Thread.Start();
                    }
                    else
                    {
                        _Thread = new Thread(GetTestDataLine);
                        _Thread.IsBackground = true;
                        _Thread.Start();
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

        /// <summary>
        /// JSON
        /// </summary>
        private void GetTestData()
        {
            String filePath = @"D:\all.txt";
            List<JZRealTimeData> list = null;
            String line = "";
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if (line.Trim().Length > 10)
                    {
                        if (line.IndexOf("]") <= 0)
                        {
                            line = line + "]";
                        }
                        list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZRealTimeData>>(line);
                        DateTime last = list[0].Time;
                        foreach (var item in list)
                        {
                            if (OnAddItem != null)
                            {
                                OnAddItem(0.00f, float.Parse(item.Value.ToString()), 0.00f, 0.00f);
                            }
                            //Thread.Sleep((int)(item.Time - last).TotalMilliseconds);
                            //last = item.Time;
                            Thread.Sleep(100);
                            if (!Active)
                            {
                                break;
                            }
                        }
                    }
                    if (!Active)
                    {
                        break;
                    }
                }
            }
        }

        private void GetTestDataLine()
        {
            String filePath = @"D:\test10.txt";
            String line = "";
            using (StreamReader sr = new StreamReader(filePath))
            {
                float time=0.00f;
                float Force=0.00f;
                while (!sr.EndOfStream && Active)
                {
                    line = sr.ReadLine();
                    if (line.IndexOf("Second") > 0 && line.IndexOf("Force") > 0)
                    {
                        try
                        {
                            time =float.Parse(line.Substring(line.IndexOf("Second") + 7, line.IndexOf("Force") - line.IndexOf("Second") - 9));
                            Force= float.Parse(line.Substring(line.IndexOf("Force") + 6, line.IndexOf("Disp") - line.IndexOf("Force") - 8));

                        }
                        catch (Exception ex)
                        {
 
                        }
                        if (OnAddItem != null)
                        {
                            OnAddItem(time, Force, 0.00f, 0.00f);
                            Thread.Sleep(200);
                        }
                    }
                }
            }
        }
    }
}
