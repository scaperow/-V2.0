using System;
using System.Collections.Generic;
using System.Text;

namespace Yqun.Permissions.Common
{
    public interface IRole
    {
        RoleCollection Roles
        {
            get;
        }
    }
}
