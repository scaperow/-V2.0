using System;
using System.Collections.Generic;
using System.Text;
using Yqun.Permissions.Common;
using Yqun.Services;
using System.Data;

namespace Yqun.Permissions
{
    public class DepositoryPermission
    {
        private DepositoryPermission()
        {
        }

        public static PermissionCollection Init(String RoleIndex)
        {
            return Agent.CallService("Yqun.BO.PermissionManager.dll", "InitPermissions", new object[] { RoleIndex }) as PermissionCollection;
        }

        public static DataTable InitByFunctionsID(String Index)
        {
            StringBuilder sql_Select = new StringBuilder();
            sql_Select.Append("select * from sys_auth_FunctionPermission where FunctionsID = '");
            sql_Select.Append(Index);
            sql_Select.Append("' order by Indentity");

            return Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { sql_Select.ToString() }) as DataTable;
        }

        public static bool Delete(String[] PermissionIndex)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.PermissionManager.dll", "DeletePermissions", new object[] { PermissionIndex }));
        }

        public static bool Update(PermissionCollection Permissions)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.PermissionManager.dll", "UpdatePermissions", new object[] { Permissions }));
        }
    }
}
