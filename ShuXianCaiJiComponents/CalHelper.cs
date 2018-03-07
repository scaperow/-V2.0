using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShuXianCaiJiModule;
using System.Data;
using Yqun.Services;

namespace ShuXianCaiJiComponents
{
    public class CalHelper
    {
        static bool isRemarkQF = false;
        public static float CalQF(SXCJModule module, List<float> list, Double qfLimit, float maxForce, Logger log, MachineBase machine)
        {
            float qf = 0.0f;
            try
            {
                if (module.SpecialSetting.MachineType == 2 && maxForce > 0 && list != null && list.Count > 0)
                {
                    QFModule m = null;
                    QuFuBase quBase = QuFuFactory.GetQuFuModule(module.SpecialSetting.QFName);

                ReSet:
                    if (quBase != null)
                    {
                        m = quBase.CalQuFu(module, list, qfLimit, maxForce, machine);
                    }
                    if (m == null)//无屈服过程，是否乘以参数？
                    {
                        qf = (float)(maxForce * module.SpecialSetting.QFParameter);
                    }
                    else
                    {
                        if (module.SpecialSetting.QuFuType == 1)
                        {
                            qf = m.UpQF;
                        }
                        else
                        {
                            qf = m.DownQF;
                        }
                        if (qf == 0.00f && !isRemarkQF)
                        {
                            if (module.SpecialSetting.IsDebug)
                            {
                                log.WriteLog("调用备用屈服算法通用0722！", true, false);
                            }
                            isRemarkQF = true;
                            quBase = QuFuFactory.GetQuFuModule("通用0722");
                            goto ReSet;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
            isRemarkQF = false;
            return qf;
        }

        public static SXCJModule LoadModule()
        {
            ConfigOperation _ConfigOperation = new ConfigOperation();
            SXCJModule _SXCJModule = new SXCJModule();
            _ConfigOperation.GetSXCJModule(out _SXCJModule);
            return _SXCJModule;
        }

        public static Guid GetDocumentIDByTestRoomModuleAndWTBH(Guid moduleID, String testRoomCode, String wtbh)
        {
            try
            {
                return new Guid(Agent.CallService("Yqun.BO.BusinessManager.dll", "GetDocumentIDByTestRoomModuleAndWTBH", new object[] { moduleID, testRoomCode, wtbh }).ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetTestDataByDataID(Guid dataID)
        {
            try
            {
                return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetTestDataByDataID", new object[] { dataID }) as DataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static String GetRealTimeDataByID(Guid id)
        {
            try
            {
                return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetRealTimeDataByID", new object[] { id }).ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static DataTable GetTestInfoByDataID(string DataID)
        {
            try
            {
                return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetTestInfo", new object[] { DataID }) as DataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
