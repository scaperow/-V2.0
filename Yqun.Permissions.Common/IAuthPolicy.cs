using System;
using System.Collections.Generic;
using System.Text;

namespace Yqun.Permissions.Common
{
    public interface IAuthPolicy
    {
        /// <summary>
        /// 判断是否有权限
        /// </summary>
        /// <returns></returns>
        Boolean HasAuth(String Code);

    }
}
