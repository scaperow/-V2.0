using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Yqun.Interfaces;
using Yqun.Exceptions;
using Yqun.Services;
using System.Collections;
using System.Threading;
using Yqun.Common.ContextCache;
using Yqun.Bases.ClassBases;
using Yqun.Permissions;
using Yqun.Permissions.Runtime;
using Yqun.Permissions.Common;

namespace Yqun.Bases.ControlBases
{
    public partial class ControlBase : UserControl, IMessage, IModule, IMenu, ISystemAuth, IApplyAuth
    {
        public System.Windows.Forms.ToolStrip ToolStrip_Main;
        public System.Windows.Forms.MenuStrip MenuStrip_Main;
        public System.Windows.Forms.ContextMenuStrip ContextMenuStrip_Main;

        private Boolean m_Modified;

        public ControlBase()
        {
            ToolStrip_Main = new ToolStrip();
            MenuStrip_Main = new MenuStrip();
            ContextMenuStrip_Main = new ContextMenuStrip();

            InitializeComponent();
        }

        public Boolean Modified
        {
            get
            {
                return m_Modified;
            }
            set
            {
                m_Modified = value;
            }
        }

        #region 加载菜单、工具栏、快捷菜单

        public virtual void LoadMenu()
        {

        }

        public virtual void LoadTool()
        {

        }

        #endregion

        #region IMessage Members

        /// <summary>
        /// 派生类里需要重载
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        public virtual object InvokeMessage(YqunMessage Message)
        {
            object ReturnValue = null;

            bool haveAuth = AuthManager.GetFunctionAuth(BizID, Message.TypeFlag);
            if (!haveAuth)
            {
                try
                {
                    switch (Message.TypeFlag.ToUpper())
                    {
                        default:
                            break;
                    }

                    return ReturnValue;
                }
                catch
                { }
            }

            return ReturnValue;
        }

        #endregion

        #region IModule Members

        string _BizID = "";
        public virtual string BizID
        {
            get
            {
                return _BizID;
            }
            set
            {
                _BizID = value;
            }
        }

        #endregion

        public void SetToolItemEnable(string TypeFlag, bool Enabled, bool Visible)
        {
            ArrayList arr = GetToolControls();
            for (int i = 0; i < arr.Count; i++)
            {
                if (arr[i] is ContextMenuStrip)
                {
                    ContextMenuStrip tool = arr[i] as ContextMenuStrip;
                    ToolStripItem item = GetToolItem(tool, TypeFlag);
                    if (item != null)
                    {
                        item.Enabled = Enabled;
                        item.Visible = Visible;
                    }
                }

                else if (arr[i] is MenuStrip)
                {
                    MenuStrip tool = arr[i] as MenuStrip;
                    ToolStripItem item = GetToolItem(tool, TypeFlag);
                    if (item != null)
                    {
                        item.Enabled = Enabled;
                        item.Visible = Visible;
                    }
                }
                else if (arr[i] is ToolStrip)
                {
                    ToolStrip tool = arr[i] as ToolStrip;
                    ToolStripItem item = GetToolItem(tool, TypeFlag);
                    if (item != null)
                    {
                        item.Enabled = Enabled;
                        item.Visible = Visible;

                        int Index = tool.Items.IndexOf(item);
                        Index++;
                        if (Index < tool.Items.Count && tool.Items[Index] is ToolStripSeparator)
                        {
                            tool.Items[Index].Visible = Visible;
                        }
                    }
                }
            }

            if (Cache.CustomCache.Contains("B647C4FA-4505-4819-A2EE-65E533998826"))
            {
                Dictionary<string, ToolStripMenuItem[]> MenuCollection =
                    (Dictionary<string, ToolStripMenuItem[]>)Cache.CustomCache["B647C4FA-4505-4819-A2EE-65E533998826"];

                if (MenuCollection.ContainsKey(this.BizID))
                {
                    ToolStripMenuItem[] tools = MenuCollection[this.BizID];
                    for (int i = 0; i < tools.Length; i++)
                    {
                        ToolStripMenuItem item0 = tools[i] as ToolStripMenuItem;
                        if (item0 != null)
                        {
                            ToolStripItem it = GetToolItem(item0, TypeFlag);
                            if (it != null)
                            {
                                it.Enabled = Enabled;
                                it.Visible = Visible;
                            }
                        }
                    }
                }
            }

            if (Cache.CustomCache.Contains("8005A36C-F438-40AC-ACEB-A636BF0F75A7"))
            {
                Dictionary<string, ToolStripItem[]> MenuCollection =
                    (Dictionary<string, ToolStripItem[]>)Cache.CustomCache["8005A36C-F438-40AC-ACEB-A636BF0F75A7"];

                if (MenuCollection.ContainsKey(this.BizID))
                {
                    ToolStripItem[] tools = MenuCollection[this.BizID];
                    for (int i = 0; i < tools.Length; i++)
                    {
                        ToolStripItem item0 = tools[i] as ToolStripItem;
                        object ooo = item0.Tag;
                        if (ooo is string)
                        {
                            string tager = ooo.ToString();

                            if (tager == TypeFlag)
                            {
                                item0.Enabled = Enabled;
                                item0.Visible = Visible;

                                break;
                            }
                        }
                    }
                }
            }
        }

        public void SetToolItemEnable(string TypeFlag, bool Enabled)
        {
            SetToolItemEnable(TypeFlag, Enabled, true);
        }

        protected virtual ArrayList GetToolControls()
        {
            ArrayList arr = new ArrayList();
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is ToolStrip)
                {
                    ToolStrip c = this.Controls[i] as ToolStrip;
                    arr.Add(c);
                }
                else if (this.Controls[i].ContextMenuStrip != null)
                {
                    ContextMenuStrip c = this.Controls[i].ContextMenuStrip;
                    arr.Add(c);
                }

                GetToolControls(this.Controls[i], arr);
            }

            arr.Add(ToolStrip_Main);
            arr.Add(MenuStrip_Main);
            arr.Add(ContextMenuStrip_Main);

            return arr;
        }

        protected virtual void GetToolControls(Control c, ArrayList arr)
        {
            for (int i = 0; i < c.Controls.Count; i++)
            {
                if (c.Controls[i] is ToolStrip)
                {
                    ToolStrip tool = c.Controls[i] as ToolStrip;
                    arr.Add(tool);
                }
                else if (c.Controls[i].ContextMenuStrip != null)
                {
                    ContextMenuStrip tool = c.Controls[i].ContextMenuStrip;
                    arr.Add(tool);
                }

                GetToolControls(c.Controls[i], arr);
            }
        }

        protected virtual void GetMenuItem(ToolStripMenuItem menu, string TypeFlag, ref Hashtable h)
        {
            if (h == null)
            {
                h = new Hashtable();
            }

            if (menu == null)
            {
                return;
            }

            object o = menu.Tag;

            if (o is string)
            {
                string tager = o.ToString();
                if (tager == TypeFlag)
                {
                    h.Add(TypeFlag, menu);
                    return;
                }
            }

            for (int i = 0; i < menu.DropDownItems.Count; i++)
            {
                ToolStripMenuItem me = menu.DropDownItems[i] as ToolStripMenuItem;
                GetMenuItem(me, TypeFlag, ref h);
            }
        }

        private ToolStripItem GetToolItem(ToolStrip Tool, string TypeFlag)
        {
            for (int i = 0; i < Tool.Items.Count; i++)
            {
                ToolStripItem menu = Tool.Items[i];
                object o = menu.Tag;

                if (o is string)
                {
                    string tager = o.ToString();
                    if (tager == TypeFlag)
                    {
                        return menu;
                    }
                }
            }

            return null;
        }

        private ToolStripItem GetToolItem(MenuStrip Menu, string TypeFlag)
        {
            Hashtable h = new Hashtable();
            for (int i = 0; i < Menu.Items.Count; i++)
            {
                ToolStripMenuItem menu = Menu.Items[i] as ToolStripMenuItem;
                GetMenuItem(menu, TypeFlag, ref h);
            }

            if (h.ContainsKey(TypeFlag))
            {
                return h[TypeFlag] as ToolStripItem;
            }
            else
            {
                return null;
            }
        }

        private ToolStripItem GetToolItem(ToolStripMenuItem menu, string TypeFlag)
        {
            Hashtable h = new Hashtable();
            GetMenuItem(menu, TypeFlag, ref h);
            if (h.ContainsKey(TypeFlag))
            {
                return h[TypeFlag] as ToolStripItem;
            }
            else
            {
                return null;
            }
        }

        private ToolStripItem GetToolItem(ContextMenuStrip Menu, string TypeFlag)
        {
            Hashtable h = new Hashtable();
            for (int i = 0; i < Menu.Items.Count; i++)
            {
                ToolStripMenuItem menu = Menu.Items[i] as ToolStripMenuItem;
                GetMenuItem(menu, TypeFlag, ref h);
            }

            if (h.ContainsKey(TypeFlag))
            {
                return h[TypeFlag] as ToolStripItem;
            }
            else
            {
                return null;
            }
        }

        #region 事件关联与功能响应

        protected void LinkEvents(ToolStrip Strip)
        {
            foreach (ToolStripItem Item in Strip.Items)
            {
                if (!(Item is ToolStripSeparator))
                {
                    LinkEvents(Item);
                }
            }
        }

        protected void LinkEvents(ToolStripItem Item)
        {
            if (Item is ToolStripMenuItem)
            {
                ToolStripMenuItem MenuItem = Item as ToolStripMenuItem;
                if (MenuItem.HasDropDownItems)
                {
                    foreach (ToolStripItem SubItem in MenuItem.DropDownItems)
                    {
                        if (!(SubItem is ToolStripSeparator))
                        {
                            SubItem.Click -= new EventHandler(Item_Click);
                            SubItem.Click += new EventHandler(Item_Click);

                            LinkEvents(SubItem);
                        }
                    }
                }
            }

            if (Item is ToolStripButton)
            {
                Item.Click -= new EventHandler(Item_Click);
                Item.Click += new EventHandler(Item_Click);
            }
        }

        void Item_Click(object sender, EventArgs e)
        {
            ToolStripItem Item = sender as ToolStripItem;
            String Tag = (Item.Tag != null? Item.Tag.ToString():"");
            YqunMessage m = new YqunMessage();
            m.TypeFlag = Tag;
            InvokeMessage(m);
        }

        #endregion

        #region IMenu 成员

        public MenuStrip MainMenu
        {
            get
            {
                return MenuStrip_Main;
            }
        }

        public ToolStrip MainTool
        {
            get
            {
                return ToolStrip_Main;
            }
        }

        #endregion

        #region ISystemAuth 成员

        public virtual ModuleInfo GetFunctionInfos()
        {
            return new ModuleInfo();
        }

        public virtual void LoadTrees(TreeView TreeView)
        {
        }

        public virtual TableInfoCollection GetTablesInfo()
        {
            return new TableInfoCollection();
        }

        #endregion

        #region IApplyAuth 成员

        public virtual void ApplySystemAuth()
        {
            
        }

        #endregion

        #region IValidateData 成员

        public virtual bool ValidateData()
        {
            return false;
        }

        #endregion
    }
}
