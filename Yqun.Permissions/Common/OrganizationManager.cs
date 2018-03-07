using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Yqun.Services;
using Yqun.Permissions.Common;

namespace Yqun.Permissions
{
    public class OrganizationManager
    {
        public static Hashtable GetProjectInformation(String UserName)
        {
            return Agent.CallService("Yqun.BO.PermissionManager.dll", "GetProjectInformation", new object[] { UserName }) as Hashtable;
        }
    }
}
