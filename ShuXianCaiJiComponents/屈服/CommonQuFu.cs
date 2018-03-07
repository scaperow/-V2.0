using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using ShuXianCaiJiModule;

namespace ShuXianCaiJiComponents
{
    public class CommonQuFu : QuFuBase
    {
        #region QuFuBase 成员

        public String Description = "通用屈服";

        public QFModule CalQuFu(SXCJModule module, List<float> list, double qfLimit, float maxForce, MachineBase machine)
        {
            List<QFModule> resultList = new List<QFModule>();//屈服过程集合
            float lastLZ = 0.0f;//上个点的力值
            Boolean startQF = false;//开始进入屈服阶段标记
            float upQF = 0.0f;//上屈服
            float downQF = 0.0f;//下屈服
            Int32 qfCount = 0;//屈服点数量
            Int32 tmpShangShengCount = 0;//连续上升计数器
            Int32 tempXiaJiangCount = 0;
            for (int i = 0; i < list.Count; i++)
            {
                float lz = list[i];
                if (lz < module.SpecialSetting.QFStartValue)
                {
                    continue;//如果在设定的有效力值之下，跳过
                }
                if (lz == maxForce)
                {
                    break;//如果到最大力值，结束循环
                }
                if (!startQF)//还没记录到下降
                {
                    if (lz < lastLZ)//当前力值比上一个力值小时
                    {
                        if (tempXiaJiangCount >= module.SpecialSetting.PrecisionGrade)
                        {
                            startQF = true;
                            qfCount = 1;
                            upQF = lastLZ;
                            downQF = lz;
                            tmpShangShengCount = 0;
                        }
                        else
                        {
                            tempXiaJiangCount++;
                        }
                        
                    }
                    else
                    {
                        lastLZ = lz;
                        if (tempXiaJiangCount != 0)
                        {
                            tempXiaJiangCount = 0 ;
                        }
                    }
                }
                else//进入屈服判断过程
                {
                    if (tmpShangShengCount > module.SpecialSetting.QFPoints)//连续上升N次，表示屈服过程结束
                    {
                        QFModule m = new QFModule();
                        m.QFCount = qfCount - tmpShangShengCount;
                        m.UpQF = upQF;
                        m.DownQF = downQF;
                        resultList.Add(m);
                        upQF = 0.0f;
                        downQF = 0.0f;
                        startQF = false;
                    }
                    if (lz >= lastLZ)//曲线上升或水平
                    {
                        tmpShangShengCount++;
                        tempXiaJiangCount = 0;
                    }
                    else//曲线下降，更新下屈服，并将连续上升计数器清零
                    {
                        downQF = lz < downQF ? lz : downQF;
                        if (tempXiaJiangCount >= module.SpecialSetting.PrecisionGrade)
                        {
                            tmpShangShengCount = 0;
                            tempXiaJiangCount = 0;
                        }
                        tempXiaJiangCount++;
                    }
                    qfCount++;
                    lastLZ = lz;
                }
            }
            return GetRealQFModule(resultList);
        }

        #endregion

        /// <summary>
        /// 得到集合中点数量最多的屈服过程
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private QFModule GetRealQFModule(List<QFModule> list)
        {
            QFModule m = null;
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (i == 0)
                    {
                        m = list[i];
                    }
                    else
                    {
                        if (m.QFCount <= list[i].QFCount)
                        {
                            m = list[i];
                        }
                    }
                }
            }
            return m;
        }
    }
}
