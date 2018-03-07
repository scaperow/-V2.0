using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Yqun.BO.BusinessManager
{
    /// <summary>
    /// 用户操作类
    /// </summary>
    public class UserHelper : BOBase
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region 试验室
        /// <summary>
        /// 获取试验室用户可以操作的线路信息
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public DataTable GetSysUserLineList(string UserName)
        {
            String sql = string.Format(@"SELECT ID,LineName,Description,IPAddress,Port FROM dbo.sys_line 
WHERE 
 id IN(SELECT lineid FROM dbo.sys_BaseLine_Users WHERE UserName	='{0}')
  Order By LineName", UserName);
            return GetDataTable(sql);
        }

        /// <summary>
        /// 获取试验室用户最后选择的线路
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public string GetSysUserLastLineID(string UserName)
        {
            String sql = string.Format(@"SELECT LineID FROM dbo.sys_BaseUsers WHERE UserName='{0}'", UserName);
            return ExcuteScalar(sql).ToString();
        }
        /// <summary>
        /// 更新试验室用户最后选择的线路
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool UpdateSysUserLastLine(string UserName,string LineID,string Description)
        {
            String sql = string.Format(@"UPDATE dbo.sys_BaseUsers SET LineID='{1}',Descrption='{2}' WHERE UserName='{0}'", UserName, LineID,Description);
            return ExcuteCommand(sql) > 0 ? true : false ;
        }
        ///// <summary>
        ///// 获取拌合站用户的编码
        ///// </summary>
        ///// <param name="UserName"></param>
        ///// <returns></returns>
        //public string GetSysUserTestCode(string UserName)
        //{
        //    String sql = string.Format(@"SELECT TestCode FROM dbo.sys_BaseUsers WHERE UserName='{0}'", UserName);
        //    return ExcuteScalar(sql).ToString();
        //}

        /// <summary>
        /// 获取用户类型
        /// </summary>
        /// <param name="nodeCode">节点Code</param>
        /// <returns>nodetype字段内容</returns>
        public string GetUserType(string nodeCode)
        {
            //  增加查询条件 Scdel=0  2013-10-17
            string sqlStr = string.Format("Select NodeType from dbo.sys_engs_Tree where Scdel=0 and NodeCode='{0}'", nodeCode);
            DataTable dt = GetDataTable(sqlStr);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            return string.Empty;
        }

        #endregion
        #region 拌合站
//        /// <summary>
//        /// 获取拌合站用户可以操作的线路信息
//        /// </summary>
//        /// <param name="UserName"></param>
//        /// <returns></returns>
//        public DataTable GetBhzUserLineList(string UserName)
//        {
//            String sql = string.Format(@"SELECT ID,LineName,Description,IPAddress,Port FROM dbo.sys_line 
//WHERE 
// id IN(SELECT lineid FROM dbo.bhz_sys_BaseLine_Users WHERE UserName	='{0}')
//  Order By LineName", UserName);
//            return GetDataTable(sql);
//        }

        ///// <summary>
        ///// 获取拌合站用户最后选择的线路
        ///// </summary>
        ///// <param name="UserName"></param>
        ///// <returns></returns>
        //public string GetBhzUserLastLineID(string UserName)
        //{
        //    String sql = string.Format(@"SELECT LineID FROM dbo.bhz_sys_BaseUsers WHERE UserName='{0}'", UserName);
        //    return ExcuteScalar(sql).ToString();
        //}
        ///// <summary>
        ///// 更新拌合站用户最后选择的线路
        ///// </summary>
        ///// <param name="UserName"></param>
        ///// <returns></returns>
        //public bool UpdateBhzUserLastLine(string UserName, string LineID, string Description)
        //{
        //    String sql = string.Format(@"UPDATE dbo.bhz_sys_BaseUsers SET LineID='{1}',Descrption='{2}' WHERE UserName='{0}'", UserName, LineID, Description);
        //    return ExcuteCommand(sql) > 0 ? true : false;
        //}

        /// <summary>
        /// 获取拌合站用户信息
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public DataTable GetBhzUserInfo(string UserName)
        {
            String sql = string.Format(@"SELECT * FROM dbo.bhz_User WHERE UserName='{0}'", UserName);
            return GetDataTable(sql);
        }

        /// <summary>
        /// 获取拌合站用户可以操作的线路信息
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public DataTable GetBhzUserLineList(string UserName)
        {
            String sql = string.Format(@"SELECT ID,LineName,Description,IPAddress,Port FROM dbo.sys_line 
WHERE 
 id IN(SELECT lineid FROM dbo.bhz_sys_BaseLine_Users WHERE UserName	='{0}')
  Order By LineName", UserName);
            return GetDataTable(sql);
        }

        /// <summary>
        /// 获取拌合站用户最后选择的线路
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public string GetBhzUserLastLineID(string UserName)
        {
            String sql = string.Format(@"SELECT LineID FROM dbo.bhz_sys_BaseUsers WHERE UserName='{0}'", UserName);
            return ExcuteScalar(sql).ToString();
        }
        /// <summary>
        /// 更新拌合站用户最后选择的线路
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool UpdateBhzUserLastLine(string UserName, string LineID, string Description)
        {
            String sql = string.Format(@"UPDATE dbo.bhz_sys_BaseUsers SET LineID='{1}',Descrption='{2}' WHERE UserName='{0}'", UserName, LineID, Description);
            return ExcuteCommand(sql) > 0 ? true : false;
        }
        /// <summary>
        /// 获取拌合站用户的编码
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public string GetBhzUserTestCode(string UserName)
        {
            String sql = string.Format(@"SELECT TestCode FROM dbo.bhz_User WHERE UserName='{0}'", UserName);
            return ExcuteScalar(sql).ToString();
        }
        #endregion
    }
}
