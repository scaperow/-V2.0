using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BizCommon;
using System.Threading;
using System.IO;
using ShuXianCaiJiModule.parse;

namespace ShuXianCaiJiModule
{
    public delegate void DataReceiveDelegate(object GetobjectiveData);
    public delegate void TestFinishedDelegate(Int32 CurrentNumber);
    public delegate void TestFinishChanged(Boolean flag);

    public abstract class MachineBase
    {
        private int _TempNum = 0;
        private float _TempForce = 0.00f;
        private float _TempForceL = 0.00f;

        private float[] _TempForces = null;

        public event DataReceiveDelegate DataReceive;
        public event TestFinishedDelegate TestFinished;
        public event TestFinishChanged TestFinishChanged;

        public SXCJModule Module { get; set; }

        public Int32 CurrentNumber { get; set; }

        public Logger log { get; set; }

        public JZParseBase parse { get; set; }

        /// <summary>
        /// 当前试验数据是否是有效的
        /// </summary>
        public Boolean IsValidData { get; set; }

        private Boolean isFinished = false;
        /// <summary>
        /// 整个试验结束
        /// </summary>
        public Boolean IsFinished
        {
            get
            {
                return isFinished;
            }
            set
            {
                if (isFinished != value)
                {
                    isFinished = value;
                    if (TestFinishChanged != null)
                    {
                        TestFinishChanged(value);
                    }
                }
            }
        }

        /// <summary>
        /// 是否进行单元调试
        /// </summary>
        public Boolean IsUnitTest { get; set; }


        public abstract void StartAcquisition();

        protected virtual void OnDataReceive(Object lz)
        {
            if (this.DataReceive != null)
            {
                this.DataReceive(lz);
            }
        }

        protected virtual void OnTestFinished(int currentNumber)
        {
            if (this.TestFinished != null)
            {
                this.TestFinished(currentNumber);
            }
        }

        public virtual Boolean IsValidValue(float value)
        {
            Boolean valid = true;
            if (Module.SpecialSetting.InvalidValueRangeList != null && Module.SpecialSetting.InvalidValueRangeList.Count > 0)
            {
                foreach (ValueRange item in Module.SpecialSetting.InvalidValueRangeList)
                {
                    if (value >= item.MinValue && value <= item.MaxValue)
                    {
                        valid = false;
                        break;
                    }
                }
            }
            return valid;
        }


        public virtual bool MedianValueFilteringAlgorithm(ref float Force,SXCJModule _SXCJModule)
        {
            if (_TempNum < _SXCJModule.SpecialSetting.MedianValueFilteringAlgorithmNumber)
            {
                if (_TempForces == null)
                {
                    _TempForces = new float[_SXCJModule.SpecialSetting.MedianValueFilteringAlgorithmNumber];
                }

                _TempForces[_TempNum] = Force;
                _TempNum++;
            }
            else
            {
                float temp;
                for (int i = 0; i < _TempForces.Length-1; i++)
                {
                    if (_TempForces[i] > _TempForces[i + 1])
                    {
                        temp = _TempForces[i];
                        _TempForces[i] = _TempForces[i + 1];
                        _TempForces[i + 1] = temp;
                    }
                }
                Force = _TempForces[_SXCJModule.SpecialSetting.MedianValueFilteringAlgorithmNumber/2];
                _TempNum = 0;
                return true;
            }
            return false;
        }

        float oldForce = 0.00f;
        float a = 0.99f;
        public virtual bool Filter(ref float Force, SXCJModule _SXCJModule)
        {

            if (oldForce != Force)
            {
                Force = float.Parse( (Force* a + (1.0 - a) * oldForce).ToString());
                return true;
            }
            oldForce = Force;
            return false;
        }

        public virtual bool FilterPrecision(ref float Force, SXCJModule _SXCJModule)
        {
            if (_TempForce > Force)
            {
                _TempNum++;
                if (_TempNum >= 3)
                {
                    _TempForce = Force;
                    _TempForceL = 0.00f;
                    return true;
                }
                else
                {
                    if (_TempForceL == 0.00f)
                    {
                        _TempForceL = _TempForce;
                    }
                    _TempForce = Force;
                    return false;
                }
            }
            else
            {
                _TempNum = 0;
                if (_TempForceL != 0.00f && _TempForceL < Force)
                {
                    _TempForceL = 0.00f;
                    _TempForce = Force;
                    return true;
                }
                else
                {
                    _TempForce = Force;
                    return false;
                }
            }
        }

        public void RunUnitTest()
        {
            String filePath = @"D:\all.txt";
            List<JZRealTimeData> list = null;
            String line = "";
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZRealTimeData>>(line);
                    DateTime last = list[0].Time;
                    foreach (var item in list)
                    {
                        TriggerDataReceive(item.Value);
                        Thread.Sleep((int)(item.Time - last).TotalMilliseconds);
                        last = item.Time;
                    }
                }
            }
        }

        public void RunSpecialTest(List<JZRealTimeData> list)
        {
            DateTime last = list[0].Time;
            foreach (var item in list)
            {
                TriggerDataReceive(item.Value);
                Thread.Sleep((int)(item.Time - last).TotalMilliseconds);
                last = item.Time;
            }
        }

        protected virtual void TriggerDataReceive(float f)
        {
        }
    }
}
