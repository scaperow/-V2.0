using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Drawing;

namespace Yqun.Interfaces
{
    #region WinUILayout

    /// <summary>
    /// 窗口布局接口
    /// </summary>
    public interface WinUILayout
    {
        /// <summary>
        /// 是否存在该选项卡
        /// </summary>
        /// <param name="Key">选项卡的名称</param>
        /// <param name="DockType">布局位置</param>
        bool Contains(string Key, LayoutDockType DockType);

        /// <summary>
        /// 激活某选项卡
        /// </summary>
        /// <param name="Key">选项卡的名称</param>
        /// <param name="DockType">布局位置</param>
        void ActiveControl(string Key, LayoutDockType DockType);

        /// <summary>
        /// 把控件添加到选项卡
        /// </summary>
        /// <param name="Key">选项卡的名称</param>
        /// <param name="Description">选项卡的描述</param>
        /// <param name="ToAddControl">带添加的控件</param>
        /// <param name="DockType">布局位置</param>
        void AddControl(string Key, string Description, Icon icon, Control ToAddControl, LayoutDockType DockType, string ReferenceKey);

        /// <summary>
        /// 获取某布局位置上的控件
        /// </summary>
        /// <param name="Key">选项卡的名称</param>
        /// <param name="DockType">布局位置</param>
        /// <returns>控件的基类</returns>
        Control GetControl(string Key, LayoutDockType DockType);

        /// <summary>
        /// 移除某位置上的选项卡
        /// </summary>
        /// <param name="Key">选项卡的名称</param>
        /// <param name="DockType">布局位置</param>
        void Remove(string Key, LayoutDockType DockType);

        /// <summary>
        /// 隐藏某选项卡
        /// </summary>
        /// <param name="Key">选项卡的名称</param>
        /// <param name="DockType">布局位置</param>
        void Hide(string Key, LayoutDockType DockType);

        /// <summary>
        /// 获得某选项卡的Key
        /// </summary>
        /// <param name="Description">选项卡的描述</param>
        /// <param name="DockType">布局位置</param>
        /// <returns>选项卡的Key</returns>
        string IndexOf(string Description, LayoutDockType DockType);
    }

    #endregion

    #region LayoutDockType

    /// <summary>
    /// 布局停靠方式
    /// </summary>
    /// 
    [Serializable]
    public enum LayoutDockType 
    {
        NULL = 0,

        /// <summary>
        /// 上
        /// </summary>
        Top=1,

        /// <summary>
        /// 左
        /// </summary>
        Left=2,

        /// <summary>
        /// 下
        /// </summary>
        Bottom=4,

        /// <summary>
        /// 右
        /// </summary>
        Right=8,

        /// <summary>
        /// 中间
        /// </summary>
        Middle=16,

        /// <summary>
        /// 四周
        /// </summary>
        Surround = Left | Right | Top | Bottom

    }

    #endregion

    #region IMessage

    /// <summary>
    /// 处理外部命令
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// 处理外部命令
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        object InvokeMessage(YqunMessage Message);
    }

    #endregion

    #region IModule

    public interface IModule
    {
        string BizID { get; set; }
    }

    #endregion IModule

    #region IMenu

    public interface IMenu
    {
        MenuStrip MainMenu { get; }
        ToolStrip MainTool { get; }
    }

    #endregion

    [Serializable]
    public struct YqunMessage
    {
        public string BizID;
        public string TypeFlag;
        public object Content;
        public Dictionary<string, object> Parameters;
        public object ReturnValue;
    }
}
