using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Yqun.Permissions.Common
{
    public interface ISystemAuth
    {
        /// <summary>
        /// 获得模块的功能列表
        /// </summary>
        /// <returns></returns>
        ModuleInfo GetFunctionInfos();

        /// <summary>
        /// 获得模块中的所有树
        /// </summary>
        /// <param name="rootNodes"></param>
        void LoadTrees(TreeView TreeView);

        /// <summary>
        /// 获得模块的所有表单的列
        /// </summary>
        /// <returns></returns>
        TableInfoCollection GetTablesInfo();
    }
}
