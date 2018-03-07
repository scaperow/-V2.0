using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Yqun.Services;

namespace BizComponents
{
    public class DepositoryLabStadiumList
    {
        public static DataTable GetLabStadiumList(String TestRoomCode)
        {
            return Agent.CallService("Yqun.BO.ReminderManager.dll", "GetLabStadiumReminderInfos", new object[] { TestRoomCode }) as DataTable;
        }

        public static DataTable GetStadiumData(String TestRoomCode, String segment, String company, String testroom)
        {

            return Agent.CallService("Yqun.BO.ReminderManager.dll", "GetStadiumData",
                new object[] { TestRoomCode, segment, company, testroom }) as DataTable;
        }

        public static Boolean HasLabStadiumList(DateTime Date, String TestRoomCode, Boolean IsAdmin)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReminderManager.dll", "HasLabStadiumList", new object[] { Date, TestRoomCode, IsAdmin }));
        }

        public static DataTable InitLabStadiumList(DateTime Date, String TestRoomCode, Boolean IsAdmin)
        {
            return Agent.CallService("Yqun.BO.ReminderManager.dll", "InitLabStadiumList", new object[] { Date, TestRoomCode, IsAdmin }) as DataTable;
        }

        public static Boolean UpdateLabStadiumList(DataTable Data, DateTime Date)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReminderManager.dll", "UpdateLabStadiumList", new object[] { Data, Date }));
        }

        /// <summary>
        /// 龄期原因分析、监理意见处理，返回用户类型
        /// </summary>
        /// <returns></returns>
        public static string GetUserType(string NodeCode)
        {
            Object obj = Agent.CallService("Yqun.BO.BusinessManager.dll", "GetUserType", new object[] { NodeCode });
            return obj == null ? "" : obj.ToString();
        }

        public static string GetModleType(string NodeCode)
        {
            Object obj = Agent.CallService("Yqun.BO.ReminderManager.dll", "GetModelType", new object[] { NodeCode });
            return obj == null ? "" : obj.ToString();
        }

        /// <summary>
        /// 添加龄期原因分析
        /// </summary>
        /// <param name="comment">原因内容</param>
        /// <param name="userCode">当前用户</param>
        /// <returns>true成功，false失败</returns>
        public static bool SetSGComment(string comment, string currentuser, string ID)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReminderManager.dll", "SetSGComment", new object[] { comment, currentuser, ID }));
        }

        /// <summary>
        /// 添加龄期监理意见或领导意见
        /// </summary>
        /// <param name="comment">监理意见或领导意见内容</param>
        /// <param name="userCode">当前用户</param>
        /// <returns>true成功，false失败</returns>
        public static bool SetJLComment(string comment, string currentuser, string ID)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReminderManager.dll", "SetJLComment", new object[] { comment, currentuser, ID }));
        }
        /// <summary>
        /// 获取龄期原因分析
        /// </summary>
        /// <param name="modelID">模版ID</param>
        /// <returns>原因内容</returns>
        public static string GetSGComment(string ID)
        {
            return Agent.CallService("Yqun.BO.ReminderManager.dll", "GetSGComment", new object[] { ID }).ToString();
        }
        /// <summary>
        /// 获取龄期意见
        /// </summary>
        /// <param name="modelID">模版ID</param>
        /// <returns>意见内容</returns>
        public static string GetJLComment(string ID)
        {
            return Agent.CallService("Yqun.BO.ReminderManager.dll", "GetJLComment", new object[] { ID }).ToString();
        }
    }
}
