using System;
using System.Collections.Generic;
using System.Text;

namespace Yqun.Permissions.Common
{
    public interface IApplyAuth
    {
        /// <summary>
        /// 应用权限到模块
        /// </summary>
        void ApplySystemAuth();
    }
}
