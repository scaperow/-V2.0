using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Yqun.BO.ReminderManager
{
    public class UserSysClass : BOBase
    {
        StringBuilder _sbSql = new StringBuilder();
        DataTable dt = null;

        /// <summary>
        /// 判断有数是否存在
        /// </summary>
        /// <param name="LineName"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool IsUserExist(string DataBaseName,string UserName)
        {
            dt = null;
            if (_sbSql.Length > 0)
            {
                _sbSql.Remove(0, _sbSql.Length);
            }

            _sbSql.Append("select * from ");
            _sbSql.Append(DataBaseName);
            _sbSql.Append(".dbo.sys_auth_Users where username='"+UserName+"'");
            dt = GetDataTable(_sbSql.ToString());
            if (dt!=null&&dt.Rows.Count>0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DataBaseName"></param>
        /// <param name="SXDateBaseName"></param>
        /// <param name="DYDateBase"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool SysALLUserExist(string DataBaseName,string SXDateBaseName,string DYDateBase, string UserName)
        {
            if (_sbSql.Length > 0)
            {
                _sbSql.Remove(0, _sbSql.Length);
            }

            _sbSql.Append("delete  " + SXDateBaseName + ".dbo.sys_auth_Users  where username='" + UserName + "';");

            _sbSql.Append("delete  " + DYDateBase + ".dbo.sys_auth_Users  where username='" + UserName + "';");

            _sbSql.Append("insert into " + SXDateBaseName);
            _sbSql.Append(".dbo.sys_auth_Users ");
            _sbSql.Append("select id,SCTS,Code,UserName,Password,Roles,IsSys from ");
            _sbSql.Append(DataBaseName);
            _sbSql.Append(".dbo.sys_auth_Users where username='" + UserName + "';");

            _sbSql.Append("insert into " + DYDateBase);
            _sbSql.Append(".dbo.sys_auth_Users ");
            _sbSql.Append("select id,SCTS,Code,UserName,Password,Roles,IsSys from ");
            _sbSql.Append(DataBaseName);
            _sbSql.Append(".dbo.sys_auth_Users where username='" + UserName + "';");
            if (ExcuteCommand(_sbSql.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
    }
}
