using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Yqun.BO.BusinessManager
{
    public class TestRoomCodeHelper : BOBase
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public List<String> GetValidTestRoomCodeList()
        {
            int isAdmin = Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator ? 1 : 0;
            if (isAdmin == 0)
            {//usercode=8表示是领导，建立在工程下
                isAdmin = Yqun.Common.ContextCache.ApplicationContext.Current.UserCode.Length == 8 ? 1 : 0;
            }
            String sql = "";
            String roleID= "-11";
            
            if (Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type == "@unit_建设单位" || Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type == "@unit_施工单位")
            {
                if (Yqun.Common.ContextCache.ApplicationContext.Current.Roles.Count > 0)
                {
                    if (Yqun.Common.ContextCache.ApplicationContext.Current.Roles[0].Permissions.Count >= 2)
                    {
                        roleID = Yqun.Common.ContextCache.ApplicationContext.Current.Roles[0].Permissions[1].Index;
                    }
                }
                sql = @"SELECT DISTINCT RecordCode FROM (SELECT RecordCode FROM dbo.sys_auth_RecordPermission WHERE RecordsID='" + roleID + @"'
                    AND LEN(RecordCode)=16
                    UNION                          
                    SELECT NodeCode AS RecordCode FROM dbo.sys_engs_Tree WHERE LEN(NodeCode)=16 AND LEFT(NodeCode,12) IN 
                    (SELECT RecordCode FROM dbo.sys_auth_RecordPermission 
                    WHERE RecordsID='" + roleID + @"'
                    AND LEN(RecordCode)=12)
                    UNION
                    SELECT NodeCode AS RecordCode FROM dbo.sys_engs_Tree WHERE LEN(NodeCode)=16 AND LEFT(NodeCode,8) IN 
                    (SELECT RecordCode FROM dbo.sys_auth_RecordPermission 
                    WHERE RecordsID='" + roleID + "' AND LEN(RecordCode)=8)) a";
            }
            else if (Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type == "@unit_监理单位")
            {
                sql = "EXEC dbo.sp_getValidTestRoomCode @id = '" + Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Index +
                "', @isAdmin=" + isAdmin;
            }
            else
            {
                isAdmin = 1;
                sql = "EXEC dbo.sp_getValidTestRoomCode @id = '" + Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Index +
                "', @isAdmin=" + isAdmin;
            }
            //logger.Error(sql);
            DataTable dt = GetDataTable(sql);

            List<String> list = new List<string>();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(dt.Rows[i][0].ToString());
                }
            }
            return list;
        }
    }
}
