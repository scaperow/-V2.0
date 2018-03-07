using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Yqun.BO.ReminderManager
{
    public class StadiumModelManager : BOBase
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataTable GetStadiumModels()
        {
            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select ID,Description,OrderIndex from sys_biz_reminder_stadiummodel order by OrderIndex");

            return GetDataTable(sql_select.ToString());
        }

        public Boolean UpdateStadiumModels(DataTable Models)
        {
            Boolean Result = false;

            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select ID,Description,OrderIndex from sys_biz_reminder_stadiummodel order by OrderIndex");

            DataTable Data = GetDataTable(sql_select.ToString());
            if (Data != null)
            {
                foreach (DataRow row in Models.Rows)
                {
                    String Index = row["ID"].ToString();
                    String Description = row["Description"].ToString();
                    String OrderIndex = row["OrderIndex"].ToString();

                    DataRow Row = null;
                    DataRow[] DataRows = Data.Select("ID='" + Index + "'");
                    if (DataRows.Length > 0)
                        Row = DataRows[0];
                    else
                    {
                        Row = Data.NewRow();
                        Data.Rows.Add(Row);
                    }

                    Row["ID"] = Index;
                    Row["Description"] = Description;
                    Row["OrderIndex"] = OrderIndex;
                }

                try
                {
                    int r = Update(Data);
                    Result = (r == 1);
                }
                catch
                {
                }
            }

            return Result;
        }

        /// <summary>
        /// 添加龄期原因分析
        /// </summary>
        /// <param name="comment">原因内容</param>
        /// <param name="userCode">当前用户</param>
        /// <returns>true成功，false失败</returns>
        public bool SetSGComment(string comment, string userCode, string DataID)
        {
            string sqlStr = string.Format("update dbo.sys_biz_reminder_stadiumData set SGComment='{0}',LastSGUser='{1}',LastSGTime=getdate() where id='{2}'", comment, userCode, DataID);
            if (ExcuteCommand(sqlStr) > 0)
            {
                return true;
            }
            return false;
        }

         /// <summary>
         /// 添加龄期监理意见或领导意见
         /// </summary>
        /// <param name="comment">监理意见或领导意见内容</param>
         /// <param name="userCode">当前用户</param>
        /// <returns>true成功，false失败</returns>
        public bool SetJLComment(string comment, string userCode, string DataID)
        {
            string sqlStr = string.Format("update dbo.sys_biz_reminder_stadiumData set JLComment='{0}',LastJLUser='{1}',LastJLTime=getdate()  where ID='{2}'", comment, userCode, DataID);
            if (ExcuteCommand(sqlStr) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取龄期原因分析
        /// </summary>
        /// <param name="modelID">模版ID</param>
        /// <returns>原因内容</returns>
        public string GetSGComment(string DataID)
        {
            string sqlStr = string.Format("select SGComment from dbo.sys_biz_reminder_stadiumData where ID='{0}'", DataID);
            DataTable dt = GetDataTable(sqlStr);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取龄期意见
        /// </summary>
        /// <param name="modelID">模版ID</param>
        /// <returns>意见内容</returns>
        public string GetJLComment(string DataID)
        {
            string sqlStr = string.Format("select JLComment from dbo.sys_biz_reminder_stadiumData where ID='{0}'", DataID);
            DataTable dt = GetDataTable(sqlStr);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            return string.Empty;
        }
    }
}
