using System;
using System.Collections.Generic;
using System.Text;
using Yqun.Permissions.Common;
using Yqun.Services;

namespace Yqun.Permissions
{
    public class DepositoryUser
    {
        public static List<User> Init()
        {
            return Agent.CallService("Yqun.BO.PermissionManager.dll", "InitUsers", new object[] { }) as List<User>;
        }

        public static User Init(String UserIndex)
        {
            return Agent.CallService("Yqun.BO.PermissionManager.dll", "InitUsers", new object[] { UserIndex }) as User;
        }

        public static List<User> Init(List<string> UserIndexes)
        {
            return Agent.CallService("Yqun.BO.PermissionManager.dll", "InitUsers", new object[] { UserIndexes }) as List<User>;
        }

        public static List<User> Init(String[] RoleIndex)
        {
            return Agent.CallService("Yqun.BO.PermissionManager.dll", "InitUsers", new object[] { RoleIndex }) as List<User>;
        }

        public static String GetNextCode(String ParentCode)
        {
            return Agent.CallService("Yqun.BO.PermissionManager.dll", "GetNextUserCode", new object[] { ParentCode }).ToString();
        }

        public static Boolean New(User user)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.PermissionManager.dll", "NewUser", new object[] { user }));
        }

        public static Boolean Delete(String Index)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.PermissionManager.dll", "DeleteUser", new object[] { Index }));
        }

        public static Boolean Update(User user)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.PermissionManager.dll", "UpdateUser", new object[] { user }));
        }

        public static Boolean Update(User[] users)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.PermissionManager.dll", "UpdateUser", new object[] { users }));
        }
    }
}
