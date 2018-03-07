using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Yqun.Common.ContextCache;
using Yqun.Interfaces;
using System.Drawing;

namespace Yqun.Services
{
    /// <summary>
    /// 该类用来合并主窗口和子窗口的菜单
    /// </summary>
    public class MainMenuHelper
    {
        static String MenuKey = "B647C4FA-4505-4819-A2EE-65E533998826";
        static String CommandKey = "8005A36C-F438-40AC-ACEB-A636BF0F75A7";

        /// <summary>
        /// 添加菜单、命令到系统缓存中
        /// </summary>
        /// <param name="modelID">模块ID</param>
        /// <returns></returns>
        public static void AddMenuToCache(IMenu model)
        {
            if (Cache.CustomCache.Contains(SystemString.菜单栏))
            {
                MenuStrip MainMenu = (MenuStrip)Cache.CustomCache[SystemString.菜单栏];

                MenuStrip ModelMenu = model.MainMenu;
                IModule Model = model as IModule;
                String modelID = Model.BizID;

                Dictionary<string, ToolStripItem[]> MenuCollection = null;
                if (Cache.CustomCache.Contains(MenuKey))
                {
                    MenuCollection = (Dictionary<string, ToolStripItem[]>)Cache.CustomCache[MenuKey];
                }
                else
                {
                    MenuCollection = new Dictionary<string, ToolStripItem[]>();
                    Cache.CustomCache.Add(MenuKey, MenuCollection);
                }

                ToolStripItem[] WillAddedItems = new ToolStripItem[ModelMenu.Items.Count];
                ModelMenu.Items.CopyTo(WillAddedItems, 0);
                MenuCollection.Add(modelID, WillAddedItems);
            }

            if (Cache.CustomCache.Contains(SystemString.工具栏))
            {
                ToolStrip MainTool = (ToolStrip)Cache.CustomCache[SystemString.工具栏];

                ToolStrip ModelCommand = model.MainTool;
                IModule Model = model as IModule;
                String modelID = Model.BizID;

                Dictionary<string, ToolStripItem[]> CommandCollection = null;
                if (Cache.CustomCache.Contains(CommandKey))
                {
                    CommandCollection = Cache.CustomCache[CommandKey] as Dictionary<string, ToolStripItem[]>;
                }
                else
                {
                    CommandCollection = new Dictionary<string, ToolStripItem[]>();
                    Cache.CustomCache.Add(CommandKey, CommandCollection);
                }

                ToolStripItem[] WillAddedCommands = new ToolStripItem[ModelCommand.Items.Count];
                ModelCommand.Items.CopyTo(WillAddedCommands, 0);
                CommandCollection.Add(modelID, WillAddedCommands);
            }
        }

        /// <summary>
        /// 移除菜单、命令从缓存中
        /// </summary>
        /// <param name="modelID">模块ID</param>
        /// <returns></returns>
        public static void RemoveMenuFromCache(String modelID)
        {
            if (Cache.CustomCache.Contains(SystemString.菜单栏))
            {
                MenuStrip MainMenu = (MenuStrip)Cache.CustomCache[SystemString.菜单栏];

                Dictionary<string, ToolStripItem[]> MenuCollection = Cache.CustomCache[MenuKey] as Dictionary<string, ToolStripItem[]>;
                ToolStripItem[] WillAddedItems = MenuCollection[modelID] as ToolStripItem[];
                foreach (ToolStripItem Item in WillAddedItems)
                {
                    MainMenu.Items.Remove(Item);
                }

                MenuCollection.Remove(modelID);
            }

            if (Cache.CustomCache.Contains(SystemString.工具栏))
            {
                ToolStrip MainTool = (ToolStrip)Cache.CustomCache[SystemString.工具栏];

                Dictionary<string, ToolStripItem[]> CommandCollection = Cache.CustomCache[CommandKey] as Dictionary<string, ToolStripItem[]>;
                ToolStripItem[] WillAddedCommands = CommandCollection[modelID] as ToolStripItem[];
                foreach (ToolStripItem Item in WillAddedCommands)
                {
                    MainTool.Items.Remove(Item);
                }

                CommandCollection.Remove(modelID);
            }
        }

        /// <summary>
        /// 显示菜单、命令到主窗口中
        /// </summary>
        /// <param name="modelID">模块</param>
        /// <returns></returns>
        public static void ShowMenuAtMainForm(String modelID, ToolStripMenuItem[] ExistingMenuItem)
        {
            if (Cache.CustomCache.Contains(SystemString.菜单栏))
            {
                MenuStrip MainMenu = (MenuStrip)Cache.CustomCache[SystemString.菜单栏];

                Dictionary<string, ToolStripItem[]> MenuCollection = null;
                if (Cache.CustomCache.Contains(MenuKey))
                {
                    MenuCollection = (Dictionary<string, ToolStripItem[]>)Cache.CustomCache[MenuKey];
                }
                else
                {
                    MenuCollection = new Dictionary<string, ToolStripItem[]>();
                    Cache.CustomCache.Add(MenuKey, MenuCollection);
                }

                MainMenu.Items.Clear();
                MainMenu.Items.AddRange(ExistingMenuItem);

                ToolStripItem[] WillAddedItems = null;
                if (MenuCollection.ContainsKey(modelID))
                {
                    WillAddedItems = MenuCollection[modelID];
                }

                if (WillAddedItems != null)
                {
                    for (int i = 0; i < WillAddedItems.Length; i++)
                    {
                        WillAddedItems[i].ImageTransparentColor = Color.White;
                        MainMenu.Items.Insert(0, WillAddedItems[i]);
                    }
                }
            }

            if (Cache.CustomCache.Contains(SystemString.工具栏))
            {
                ToolStrip MainTool = (ToolStrip)Cache.CustomCache[SystemString.工具栏];

                Dictionary<string, ToolStripItem[]> CommandCollection = null;
                if (Cache.CustomCache.Contains(CommandKey))
                {
                    CommandCollection = Cache.CustomCache[CommandKey] as Dictionary<string, ToolStripItem[]>;
                }
                else
                {
                    CommandCollection = new Dictionary<string, ToolStripItem[]>();
                    Cache.CustomCache.Add(CommandKey, CommandCollection);
                }

                MainTool.Items.Clear();

                ToolStripItem[] WillAddedCommands = null;
                if (CommandCollection.ContainsKey(modelID))
                {
                    WillAddedCommands = CommandCollection[modelID];
                }

                if (WillAddedCommands != null)
                {
                    MainTool.SuspendLayout();
                    for (int i = 0; i < WillAddedCommands.Length; i++)
                    {
                        WillAddedCommands[i].TextImageRelation = TextImageRelation.ImageAboveText;
                        WillAddedCommands[i].ImageTransparentColor = Color.White;
                        WillAddedCommands[i].Font = new Font("宋体", 9);

                        MainTool.Items.Add(WillAddedCommands[i]);
                    }
                    MainTool.ResumeLayout();
                }
            }
        }
    }
}
