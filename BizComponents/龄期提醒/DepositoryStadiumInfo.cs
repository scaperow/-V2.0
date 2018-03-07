using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using Yqun.Services;
using System.Data;

namespace BizComponents
{
    public class DepositoryStadiumInfo
    {
        public static List<IndexDescriptionPair> GetModels()
        {
            return Agent.CallService("Yqun.BO.ReminderManager.dll", "GetModels", new object[] { }) as List<IndexDescriptionPair>;
        }

        public static List<StadiumInfo> InitStadiumInfos()
        {
            return Agent.CallService("Yqun.BO.ReminderManager.dll", "InitStadiumInfos", new object[] { }) as List<StadiumInfo>;
        }

        public static DataTable InitStadiumConfig()
        {
            return Agent.CallService("Yqun.BO.ReminderManager.dll", "InitStadiumConfig", new object[] { }) as DataTable;
        }

        public static StadiumInfo InitStadiumInfo(String Index)
        {
            return Agent.CallService("Yqun.BO.ReminderManager.dll", "InitStadiumInfo", new object[] { Index }) as StadiumInfo;
        }

        public static Boolean DeleteStadiumInfo(String Index)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReminderManager.dll", "DeleteStadiumInfo", new object[] { Index }));
        }

        public static Boolean UpdateStadiumInfo(StadiumInfo info)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReminderManager.dll", "UpdateStadiumInfo", new object[] { info }));
        }

        public static Boolean UpdateStadiumConfig(string modelID,string config,bool isactive)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReminderManager.dll", "UpdateStadiumConfig", new object[] { modelID,config,isactive }));
        }

        /// <summary>
        /// 生成历史Stadium数据
        /// </summary>
        /// <param name="modelID"></param>
        /// <param name="searRange"></param>
        /// <param name="config"></param>
        /// <param name="isactive"></param>
        /// <returns></returns>
        public static Boolean CreateTestStadiumData()
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "MoveOldStadiumData", new object[] { }));
        }

        /// <summary>
        /// 生成历史Stadium数据
        /// </summary>
        /// <param name="modelID"></param>
        /// <param name="searRange"></param>
        /// <param name="config"></param>
        /// <param name="isactive"></param>
        /// <returns></returns>
        public static Boolean DeleteWrongStadiumData()
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "DeleteWrongStadiumData", new object[] { }));
        }

        /// <summary>
        /// 西成三标中心，解决老混凝土模板龄期的问题
        /// </summary>
        /// <returns></returns>
        public static Boolean SpStadiumData()
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "SpStadiumData", new object[] { }));
        }

        /// <summary>
        /// 将现有的资料中的报告日期复制到表外数据表的BGRQ字段中
        /// </summary>
        /// <returns></returns>
        public static Boolean CopyBGRQToExtentTable()
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "CopyBGRQToExtentTable", new object[] { }));
        }

        /// <summary>
        /// 更新不合格标记
        /// </summary>
        /// <returns></returns>
        public static Boolean UpdateIsQualified()
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateIsQualified", new object[] { }));
        }
        /// <summary>
        /// 列新模板版本
        /// 20131205
        /// </summary>
        /// <returns></returns>
        public static Boolean UpdateModulesVision()
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateModulesVision", new object[] { }));
        }


        public static void MoveSheetCellLogic()
        {
            Agent.CallService("Yqun.BO.BusinessManager.dll", "MoveSheetCellLogic", new object[] { });
        }

        public static void MoveSheetFormulas()
        {
            Agent.CallService("Yqun.BO.BusinessManager.dll", "MoveSheetFormulas", new object[] { });
        }
        public static void GenerateCellLogic()
        {
            Agent.CallService("Yqun.BO.BusinessManager.dll", "GenerateCellLogic", new object[] { });
        }
    }
}
