using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShuXianCaiJiComponents
{
    public class QFNewCommon : QuFuBase
    {
        #region QuFuBase 成员

        public String Description = "屈服新通用";


        public QFModule CalQuFu(ShuXianCaiJiModule.SXCJModule module, List<float> list, double qfLimit, float maxForce, ShuXianCaiJiModule.MachineBase machine)
        {
            List<QFModule> resultList = new List<QFModule>();//屈服过程集合

            float lastLZ = 0.0f;//上个点的力值
            Boolean startQF = false;//开始进入屈服阶段标记
            float upQF = 0.0f;//上屈服
            float downQF = 0.0f;//下屈服
            Int32 qfCount = 0;//屈服点数量
            for (int i = 0; i < list.Count; i++)
            {
                float lz = list[i];
                if (lz < module.SpecialSetting.QFStartValue)
                {
                    continue;//如果在设定的有效力值之下，跳过
                }
                if (lz >= maxForce*0.9)
                {
                    break;//如果到最大力值，结束循环
                }
                if (!startQF)//还没记录到下降
                {
                    if (lz <= lastLZ)//当前力值比上一个力值小时
                    {
                        startQF = true;
                        qfCount = 1;
                        upQF = lastLZ;
                        downQF = lz;
                    }
                    else
                    {
                        lastLZ = lz;
                    }
                }
                else//进入屈服判断过程
                {
                    if (lz > upQF)//超过上屈服，表示屈服过程结束
                    {
                        if (qfCount > module.SpecialSetting.PrecisionGrade)//在精度范围内的点数
                        {
                            QFModule m = new QFModule();
                            m.QFCount = qfCount;
                            m.UpQF = upQF;
                            m.DownQF = downQF;
                            resultList.Add(m);
                        }
                        upQF = 0.0f;
                        downQF = 0.0f;
                        startQF = false;
                    }
                    else//曲线下降，更新下屈服
                    {
                        downQF = lz < downQF ? lz : downQF;
                    }
                    qfCount++;
                    lastLZ = lz;
                }
            }
            return GetRealQFModule(resultList,qfLimit,maxForce);
        }

        private QFModule GetRealQFModule(List<QFModule> list, double qfLimit,float maxForce)
        {
            QFModule m = null;
            QFModule m_qflimit = null;
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
                        if (list[i].DownQF > qfLimit  && list[i].DownQF < maxForce * 0.9)
                        {
                            if (m_qflimit != null)
                            {
                                if (m_qflimit.QFCount < list[i].QFCount)
                                {
                                    m_qflimit = list[i];
                                }
                            }
                            else
                            {
                                m_qflimit = list[i];
                            }
                        }
                    }
                }
            }
            if (m_qflimit != null)
            {
                return m_qflimit;
            }
            else
            {
                return m;
            }
        }
        #endregion
    }
}
