using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Yqun.Bases;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using BizComponents;
using Yqun.Client;
using Yqun.Interfaces;
using Yqun.Common.ContextCache;
using Yqun.Permissions.Runtime;
using Yqun.Permissions.Common;
using BizCommon;
using ReportComponents;
using Yqun.Permissions;
using System.Threading;
using BizComponents.审批资料修改;

namespace BizModules
{
    public partial class BizWindow : ControlStandard
    {
        //工程目录窗口
        internal ProjectCatlogContent ProjectCatlogContent;
        //资源目录窗口
        internal ModelCatlogContent ModelCatlogContent;
        //台账窗口
        internal BizViewerContent BizViewerContent;
        //查询窗口
        internal QueryDataContent QueryDataContent;

        protected String ConfigFilename;
        protected DeserializeDockContent dockcontent;

        internal Dictionary<string, bool> AuthDictionary = new Dictionary<string, bool>();

        public BizWindow()
        {
            InitializeComponent();

            dockcontent = new DeserializeDockContent(ReloadContent);

            ProjectCatlogContent = new ProjectCatlogContent();

            ModelCatlogContent = new ModelCatlogContent();
            BizViewerContent = new BizViewerContent();
            QueryDataContent = new QueryDataContent();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            ReminderInformation.ClearReminderLabelToStatusBar();
            base.OnHandleDestroyed(e);
        }

        /// <summary>
        /// 应用权限到业务功能
        /// </summary>
        public override void ApplySystemAuth()
        {
            if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
                return;

            #region 工程列表右击弹出菜单控制

            bool IsLine = false;
            for (int i = 0; i < ProjectCatlogContent.ProjectContextMenu.Items.Count; i++)
            {
                bool ParentMenuVisible = false;
                if (ProjectCatlogContent.ProjectContextMenu.Items[i].Tag != null)
                {
                    ToolStripMenuItem StripItem = (ToolStripMenuItem)ProjectCatlogContent.ProjectContextMenu.Items[i];
                    if (StripItem.DropDownItems.Count > 0)
                    {
                        for (int j = 0; j < StripItem.DropDownItems.Count; j++)
                        {
                            if (StripItem.DropDownItems[j].Tag != null)
                            {
                                ToolStripMenuItem SubStripItem = (ToolStripMenuItem)StripItem.DropDownItems[j];
                                Boolean visible = AuthManager.GetFunctionAuth(BizID, SubStripItem.Tag.ToString());
                                SubStripItem.Visible = visible;

                                IsLine = IsLine | visible;
                                ParentMenuVisible = ParentMenuVisible | visible;

                                if (!AuthDictionary.ContainsKey(SubStripItem.Tag.ToString()) && visible)
                                    AuthDictionary.Add(SubStripItem.Tag.ToString(), visible);
                            }
                        }
                    }
                    else
                    {

                        Boolean visible = AuthManager.GetFunctionAuth(BizID, StripItem.Tag.ToString());
                        StripItem.Visible = visible;

                        IsLine = IsLine | visible;
                        ParentMenuVisible = ParentMenuVisible | visible;
                    }

                    StripItem.Visible = ParentMenuVisible;
                    if (i > 0 && ProjectCatlogContent.ProjectContextMenu.Items[i - 1] is ToolStripSeparator)
                    {
                        ProjectCatlogContent.ProjectContextMenu.Items[i - 1].Visible = ParentMenuVisible;
                    }

                    if (!AuthDictionary.ContainsKey(StripItem.Tag.ToString()) && ParentMenuVisible)
                        AuthDictionary.Add(StripItem.Tag.ToString(), ParentMenuVisible);
                }
                else
                {
                    ProjectCatlogContent.ProjectContextMenu.Items[i].Visible = IsLine;

                    if (i > 0 && ProjectCatlogContent.ProjectContextMenu.Items[i - 1] is ToolStripSeparator)
                    {
                        ProjectCatlogContent.ProjectContextMenu.Items[i - 1].Visible = IsLine;
                    }
                }
            }

            #endregion 工程列表右击弹出菜单控制

            #region 系统菜单权限控制

            ToolStrip MainTool = (ToolStrip)Cache.CustomCache[SystemString.工具栏];
            for (int i = 0; i < MainTool.Items.Count; i++)
            {
                if (MainTool.Items[i].Tag != null)
                {
                    if (MainTool.Items[i] is ToolStripButton)
                    {
                        ToolStripButton MenuItem = (ToolStripButton)MainTool.Items[i];
                        if (MenuItem.Tag != null && MenuItem.Tag.ToString() != "@Layout")
                        {
                            Boolean visible = AuthManager.GetFunctionAuth(BizID, MenuItem.Tag.ToString());
                            MenuItem.Visible = visible;

                            if (!AuthDictionary.ContainsKey(MenuItem.Tag.ToString()) && visible)
                                AuthDictionary.Add(MenuItem.Tag.ToString(), visible);
                        }
                        else
                        {
                            MenuItem.Visible = true;

                            if (!AuthDictionary.ContainsKey(MenuItem.Tag.ToString()))
                                AuthDictionary.Add(MenuItem.Tag.ToString(), true);
                        }
                    }
                }
            }

            ToolStrip MainMenu = (ToolStrip)Cache.CustomCache[SystemString.菜单栏];
            for (int i = 0; i < MainMenu.Items.Count; i++)
            {
                ToolStripMenuItem MenuItem = (ToolStripMenuItem)MainMenu.Items[i];
                bool ParentMenuVisible = false;

                if (MenuItem.Tag != null && MenuItem.Tag.ToString().ToLower() == "@prject")
                {
                    for (int j = 0; j < MenuItem.DropDownItems.Count; j++)
                    {
                        bool SubVisible = false;
                        if (MenuItem.DropDownItems[j].Tag != null && MenuItem.DropDownItems[j].Tag.ToString().ToLower() == "@prject")
                        {
                            ToolStripMenuItem SubMenuItem = (ToolStripMenuItem)MenuItem.DropDownItems[j];

                            for (int k = 0; k < SubMenuItem.DropDownItems.Count; k++)
                            {
                                ToolStripMenuItem GrandMenuItem = (ToolStripMenuItem)SubMenuItem.DropDownItems[k];
                                if (GrandMenuItem.Tag != null)
                                {
                                    Boolean visible = AuthManager.GetFunctionAuth(BizID, GrandMenuItem.Tag.ToString());
                                    GrandMenuItem.Visible = visible;
                                    SubVisible = SubVisible | visible;

                                    if (!AuthDictionary.ContainsKey(GrandMenuItem.Tag.ToString()) && visible)
                                        AuthDictionary.Add(GrandMenuItem.Tag.ToString(), visible);
                                }
                            }

                            SubMenuItem.Visible = SubVisible;

                            if (j < MenuItem.DropDownItems.Count - 1 && MenuItem.DropDownItems[j + 1] is ToolStripSeparator)
                            {
                                MenuItem.DropDownItems[j + 1].Visible = SubVisible;
                            }
                        }
                    }
                }
                else if (MenuItem.Tag.ToString() == "@System" ||
                         MenuItem.Tag.ToString() == "@MenuItemWindow" ||
                         MenuItem.Tag.ToString() == "@MenuItemHelp")
                {
                    MenuItem.Visible = true;
                }
                else
                {
                    for (int j = 0; j < MenuItem.DropDownItems.Count; j++)
                    {
                        if (MenuItem.DropDownItems[j].Tag != null)
                        {
                            ToolStripMenuItem SubMenuItem = (ToolStripMenuItem)MenuItem.DropDownItems[j];
                            Boolean visible = AuthManager.GetFunctionAuth(BizID, SubMenuItem.Tag.ToString());
                            SubMenuItem.Visible = visible;

                            if (j < MenuItem.DropDownItems.Count - 1 && MenuItem.DropDownItems[j + 1] is ToolStripSeparator)
                            {
                                MenuItem.DropDownItems[j + 1].Visible = visible;
                            }

                            if (!AuthDictionary.ContainsKey(SubMenuItem.Tag.ToString()) && visible)
                                AuthDictionary.Add(SubMenuItem.Tag.ToString(), visible);

                            ParentMenuVisible = ParentMenuVisible | visible;
                        }
                    }

                    MenuItem.Visible = ParentMenuVisible;
                }
            }

            #endregion 系统菜单权限控制
        }

        /// <summary>
        /// 加载窗口布局
        /// </summary>
        /// <param name="SheetIndex"></param>
        protected virtual void LoadLayout(string SheetIndex)
        {
            if (!Directory.Exists(SystemFolder.DockingConfig))
            {
                Directory.CreateDirectory(SystemFolder.DockingConfig);
            }

            ConfigFilename = System.IO.Path.Combine(SystemFolder.DockingConfig, SheetIndex + ".config");

            dockPanel.ActiveAutoHideContent = null;
            dockPanel.Parent = this;

            dockPanel.Visible = false;
            dockPanel.SuspendLayout(true);
            if (System.IO.File.Exists(ConfigFilename))
            {
                dockPanel.LoadFromXml(ConfigFilename, dockcontent);
            }
            else
            {
                InitDefaultLayout();
            }

            dockPanel.ResumeLayout(true, true);

            dockPanel.Visible = true;

            ShowDefaultLayout();
        }

        protected virtual IDockContent ReloadContent(String persistString)
        {
            switch (persistString.ToLower())
            {
                case "bizwindow.projectcatlogcontent":
                    ProjectCatlogContent = new ProjectCatlogContent();
                    return ProjectCatlogContent;
                case "bizwindow.resourcecatlogcontent":
                    ModelCatlogContent = new ModelCatlogContent();
                    return ModelCatlogContent;
                case "bizwindow.bizviewercontent":
                    BizViewerContent = new BizViewerContent();
                    return BizViewerContent;
                case "bizwindow.querydatacontent":
                    QueryDataContent = new QueryDataContent();
                    return QueryDataContent;
            }

            return null;
        }

        protected virtual void InitDefaultLayout()
        {
            ProjectCatlogContent.Show(dockPanel, DockState.DockLeft);
            QueryDataContent.Show(dockPanel, DockState.DockLeft);
            BizViewerContent.Show(dockPanel, DockState.Document);
            ModelCatlogContent.Show(dockPanel, DockState.DockRightAutoHide);
        }

        protected virtual void ShowDefaultLayout()
        {
            if (!ProjectCatlogContent.IsHidden)
                ProjectCatlogContent.Activate();
            if (!BizViewerContent.IsHidden)
                BizViewerContent.Activate();

            BizViewerContent.DataViewControl.Panel_Filter = QueryDataContent.Panel_Filter;
        }

        public override void LoadMenu()
        {
            //审批
            ToolStripMenuItem ApprovalItem = new ToolStripMenuItem();
            ApprovalItem.Text = "审批(&A)";
            ApprovalItem.Tag = "@Approval";
            MenuStrip_Main.Items.Add(ApprovalItem);

            ToolStripMenuItem SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "查询资料修改";
            SubMenuItem.Tag = "@querydatamodify";
            ApprovalItem.DropDownItems.Add(SubMenuItem);

            ToolStripMenuItem OvertimeTestProcessed = new ToolStripMenuItem();
            OvertimeTestProcessed.Text = "查看已审批的过期试验";
            OvertimeTestProcessed.Tag = "@testovertimeprocessed";
            ApprovalItem.DropDownItems.Add(OvertimeTestProcessed);

            //消息提醒
            ToolStripMenuItem MessageNoticeItem = new ToolStripMenuItem();
            MessageNoticeItem.Text = "消息提醒(&N)";
            MessageNoticeItem.Tag = "@MessageNotice";
            MenuStrip_Main.Items.Add(MessageNoticeItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "报告检测条件设置";
            SubMenuItem.Tag = "@evaluatecondition";
            //MessageNoticeItem.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "监理抽检频率设置";
            SubMenuItem.Tag = "@samplingfrequency";
            //MessageNoticeItem.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "试验龄期设置";
            SubMenuItem.Tag = "@teststadiuminfo";
            MessageNoticeItem.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "温度试验记录";
            SubMenuItem.Tag = "@teststadiumtemperature";
            MessageNoticeItem.DropDownItems.Add(SubMenuItem);

            MessageNoticeItem.DropDownItems.Add(new ToolStripSeparator());

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "查询不合格报告";
            SubMenuItem.Tag = "@queryunqualifiedreport";
            MessageNoticeItem.DropDownItems.Add(SubMenuItem);


            //日志查询
            ToolStripMenuItem logItem = new ToolStripMenuItem();
            logItem.Text = "日志查询(&L)";
            logItem.Tag = "@logItem";
            MenuStrip_Main.Items.Add(logItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "查看登录日志";
            SubMenuItem.Tag = "@seeloginlog";
            logItem.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "查询修改日志";
            SubMenuItem.Tag = "@seeoperatelog";
            logItem.DropDownItems.Add(SubMenuItem);

            //统计
            ToolStripMenuItem EditItem = new ToolStripMenuItem();
            EditItem.Text = "统计(&E)";
            EditItem.Tag = "@statistics";
            MenuStrip_Main.Items.Add(EditItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "周报、月报";
            SubMenuItem.Tag = "@superstatistics";
            EditItem.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "资料统计、人员统计、设备统计";
            SubMenuItem.Tag = "@superstatistics_1";
            EditItem.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "报表设计器（管理员）";
            SubMenuItem.Tag = "@statisticsdesigner";
            EditItem.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "平行频率查询";
            SubMenuItem.Tag = "@pxreport";
            EditItem.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "见证频率查询";
            SubMenuItem.Tag = "@jzreport";
            EditItem.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "见证频率自定义设置";
            SubMenuItem.Tag = "@jzsetting";
            EditItem.DropDownItems.Add(SubMenuItem);

            var factory = new ToolStripMenuItem();
            factory.Text = "厂家信息管理";
            factory.Tag = "@factorymanagement";
            EditItem.DropDownItems.Add(factory);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "合并重复厂家";
            SubMenuItem.Tag = "@factorymerge";
            factory.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "设备情况";
            SubMenuItem.Tag = "@devicesummary";
            EditItem.DropDownItems.Add(SubMenuItem);


            //资料
            EditItem = new ToolStripMenuItem();
            EditItem.Text = "资料(&M)";
            EditItem.Tag = "@data";
            MenuStrip_Main.Items.Add(EditItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "新建资料";
            SubMenuItem.Tag = "@module_newdata";
            SubMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            EditItem.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "编辑资料";
            SubMenuItem.Tag = "@data_editdata";
            SubMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            EditItem.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "删除资料";
            SubMenuItem.Tag = "@data_deledata";
            SubMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            EditItem.DropDownItems.Add(SubMenuItem);

            EditItem.DropDownItems.Add(new ToolStripSeparator());

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "复制资料";
            SubMenuItem.Tag = "@data_copydata";
            SubMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            EditItem.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "粘贴资料";
            SubMenuItem.Tag = "@data_pastdata";
            SubMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            EditItem.DropDownItems.Add(SubMenuItem);

            EditItem.DropDownItems.Add(new ToolStripSeparator());

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "台账设置";
            SubMenuItem.Tag = "@module_modesetup";
            SubMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            EditItem.DropDownItems.Add(SubMenuItem);

            EditItem = new ToolStripMenuItem();
            EditItem.Text = "工程(&P)";
            EditItem.Tag = "@prject";
            MenuStrip_Main.Items.Add(EditItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "新建";
            SubMenuItem.Tag = "@prject";
            SubMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            EditItem.DropDownItems.Add(SubMenuItem);

            ToolStripMenuItem GrandMenuItem = new ToolStripMenuItem();
            GrandMenuItem.Text = "新建工程";
            GrandMenuItem.Tag = "@top_newproject";
            GrandMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            SubMenuItem.DropDownItems.Add(GrandMenuItem);

            GrandMenuItem = new ToolStripMenuItem();
            GrandMenuItem.Text = "新建单位";
            GrandMenuItem.Tag = "@eng_newdepartment";
            GrandMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            SubMenuItem.DropDownItems.Add(GrandMenuItem);

            GrandMenuItem = new ToolStripMenuItem();
            GrandMenuItem.Text = "新建标段";
            GrandMenuItem.Tag = "@unit_newprjsct";
            GrandMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            SubMenuItem.DropDownItems.Add(GrandMenuItem);

            GrandMenuItem = new ToolStripMenuItem();
            GrandMenuItem.Text = "新建文件夹";
            GrandMenuItem.Tag = "@tenders_newfolder";
            GrandMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            SubMenuItem.DropDownItems.Add(GrandMenuItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "编辑";
            SubMenuItem.Tag = "@prject";
            SubMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            EditItem.DropDownItems.Add(SubMenuItem);

            GrandMenuItem = new ToolStripMenuItem();
            GrandMenuItem.Text = "编辑工程";
            GrandMenuItem.Tag = "@eng_editproject";
            GrandMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            SubMenuItem.DropDownItems.Add(GrandMenuItem);

            GrandMenuItem = new ToolStripMenuItem();
            GrandMenuItem.Text = "编辑单位";
            GrandMenuItem.Tag = "@unit_editdepartment";
            GrandMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            SubMenuItem.DropDownItems.Add(GrandMenuItem);

            GrandMenuItem = new ToolStripMenuItem();
            GrandMenuItem.Text = "编辑标段";
            GrandMenuItem.Tag = "@tenders_editcontract";
            GrandMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            SubMenuItem.DropDownItems.Add(GrandMenuItem);

            GrandMenuItem = new ToolStripMenuItem();
            GrandMenuItem.Text = "编辑文件夹";
            GrandMenuItem.Tag = "@folder_editfolder";
            GrandMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            SubMenuItem.DropDownItems.Add(GrandMenuItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "删除";
            SubMenuItem.Tag = "@prject";
            SubMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            EditItem.DropDownItems.Add(SubMenuItem);

            GrandMenuItem = new ToolStripMenuItem();
            GrandMenuItem.Text = "删除工程";
            GrandMenuItem.Tag = "@eng_deleproject";
            GrandMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            SubMenuItem.DropDownItems.Add(GrandMenuItem);

            GrandMenuItem = new ToolStripMenuItem();
            GrandMenuItem.Text = "删除单位";
            GrandMenuItem.Tag = "@unit_deledepartment";
            GrandMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            SubMenuItem.DropDownItems.Add(GrandMenuItem);

            GrandMenuItem = new ToolStripMenuItem();
            GrandMenuItem.Text = "删除标段";
            GrandMenuItem.Tag = "@tenders_delecontract";
            GrandMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            SubMenuItem.DropDownItems.Add(GrandMenuItem);

            GrandMenuItem = new ToolStripMenuItem();
            GrandMenuItem.Text = "删除文件夹";
            GrandMenuItem.Tag = "@folder_delefolder";
            GrandMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            SubMenuItem.DropDownItems.Add(GrandMenuItem);

            GrandMenuItem = new ToolStripMenuItem();
            GrandMenuItem.Text = "删除模板";
            GrandMenuItem.Tag = "@module_delemodule";
            GrandMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            SubMenuItem.DropDownItems.Add(GrandMenuItem);

            EditItem.DropDownItems.Add(new ToolStripSeparator());

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "退出";
            SubMenuItem.Tag = "@quit";
            EditItem.DropDownItems.Add(SubMenuItem);

        }

        public override void LoadTool()
        {
            ToolStripButton EditMenuItem = new ToolStripButton();

            EditMenuItem.Text = "新建工程";
            EditMenuItem.Tag = "@top_newproject";
            ToolStrip_Main.Items.Add(EditMenuItem);

            EditMenuItem = new ToolStripButton();
            EditMenuItem.Text = "编辑工程";
            EditMenuItem.Tag = "@eng_editproject";
            ToolStrip_Main.Items.Add(EditMenuItem);

            EditMenuItem = new ToolStripButton();
            EditMenuItem.Text = "删除工程";
            EditMenuItem.Tag = "@eng_deleproject";
            ToolStrip_Main.Items.Add(EditMenuItem);

            ToolStrip_Main.Items.Add(new ToolStripSeparator());

            EditMenuItem = new ToolStripButton();
            EditMenuItem.Text = "新建标段";
            EditMenuItem.Tag = "@unit_newcontract";
            ToolStrip_Main.Items.Add(EditMenuItem);

            EditMenuItem = new ToolStripButton();
            EditMenuItem.Text = "编辑标段";
            EditMenuItem.Tag = "@tenders_editcontract";
            ToolStrip_Main.Items.Add(EditMenuItem);

            EditMenuItem = new ToolStripButton();
            EditMenuItem.Text = "删除标段";
            EditMenuItem.Tag = "@tenders_delecontract";
            ToolStrip_Main.Items.Add(EditMenuItem);

            ToolStrip_Main.Items.Add(new ToolStripSeparator());

            EditMenuItem = new ToolStripButton();
            EditMenuItem.Text = "新建单位";
            EditMenuItem.Tag = "@eng_newdepartment";
            ToolStrip_Main.Items.Add(EditMenuItem);

            EditMenuItem = new ToolStripButton();
            EditMenuItem.Text = "编辑单位";
            EditMenuItem.Tag = "@unit_editdepartment";
            ToolStrip_Main.Items.Add(EditMenuItem);

            EditMenuItem = new ToolStripButton();
            EditMenuItem.Text = "删除单位";
            EditMenuItem.Tag = "@unit_deledepartment";
            ToolStrip_Main.Items.Add(EditMenuItem);

            ToolStrip_Main.Items.Add(new ToolStripSeparator());

            //编辑资料
            EditMenuItem = new ToolStripButton();
            EditMenuItem.Text = "新建资料";
            EditMenuItem.Tag = "@module_newdata";
            ToolStrip_Main.Items.Add(EditMenuItem);

            EditMenuItem = new ToolStripButton();
            EditMenuItem.Text = "编辑资料";
            EditMenuItem.Tag = "@data_editdata";
            ToolStrip_Main.Items.Add(EditMenuItem);

            EditMenuItem = new ToolStripButton();
            EditMenuItem.Text = "删除资料";
            EditMenuItem.Tag = "@data_deledata";
            ToolStrip_Main.Items.Add(EditMenuItem);

            ToolStripMenuItem MenuItem = new ToolStripMenuItem();
            MenuItem.Text = "窗口布局";
            MenuItem.Tag = "@Layout";
            MenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            ToolStrip_Main.Items.Add(MenuItem);

            ToolStripMenuItem SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "工程列表";
            SubMenuItem.Tag = "@projectcatlogcontent";
            SubMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            MenuItem.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "模板列表";
            SubMenuItem.Tag = "@modulecatlogcontent";
            SubMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            MenuItem.DropDownItems.Add(SubMenuItem);

            MenuItem.DropDownItems.Add(new ToolStripSeparator());

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "台账面板";
            SubMenuItem.Tag = "@bizviewercontent";
            SubMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            MenuItem.DropDownItems.Add(SubMenuItem);
        }

        public override object InvokeMessage(Yqun.Interfaces.YqunMessage Message)
        {
            switch (Message.TypeFlag.ToLower())
            {
                case "@open":
                    OpenModule(Message);
                    break;
                case "@projectcatlogcontent":               //工程结构
                    ShowDockContent(ProjectCatlogContent);
                    break;
                case "@modulecatlogcontent":                //模板列表
                    ShowDockContent(ModelCatlogContent);
                    break;
                case "@bizviewercontent":                   //台账面板
                    ShowDockContent(BizViewerContent);
                    break;
                case "@top_newproject":
                    ProjectCatlogContent.NewProject();      //新建工程
                    break;
                case "@eng_editproject":
                    ProjectCatlogContent.EditProject();     //编辑工程
                    break;
                case "@eng_deleproject":
                    ProjectCatlogContent.DeleteProject();   //删除工程
                    break;
                case "@unit_newprjsct":
                    ProjectCatlogContent.NewPrjsct();       //新建标段
                    break;
                case "@tenders_editcontract":
                    ProjectCatlogContent.EditPrjsct();      //编辑标段
                    break;
                case "@tenders_delecontract":
                    ProjectCatlogContent.DeletePrjsct();    //删除标段
                    break;
                case "@eng_newdepartment":
                    ProjectCatlogContent.NewCompany();      //新建单位
                    break;
                case "@unit_editdepartment":
                    ProjectCatlogContent.EditCompany();     //编辑单位
                    break;
                case "@unit_deledepartment":
                    ProjectCatlogContent.DeleteCompany();   //删除单位
                    break;
                case "@tenders_newfolder":
                    ProjectCatlogContent.NewFolder();       //新建文件夹
                    break;
                case "@folder_editfolder":
                    ProjectCatlogContent.EditFolder();      //编辑文件夹
                    break;
                case "@folder_delefolder":
                    ProjectCatlogContent.DeleteFolder();    //删除文件夹
                    break;
                case "@module_newdata":                     //新建资料
                    ProjectCatlogContent.NewData();
                    break;
                case "@data_editdata":
                    BizViewerContent.DataViewControl.EditData();
                    break;
                case "@data_deledata":
                    BizViewerContent.DataViewControl.DeleteData();
                    break;
                case "@data_selectdataid":
                    BizViewerContent.DataViewControl.SelectDataID();//查看DataID
                    break;
                case "@temperaturelist":
                    BizViewerContent.DataViewControl.TemperatureList();
                    break;
                case "@data_copydata":
                    BizViewerContent.DataViewControl.CopyData();
                    break;
                case "@module_modesetup":                   //台账设置
                    ProjectCatlogContent.ModeSetUp();
                    break;
                case "@statisticsdesigner":                 //报表设计器（管理员）
                    ConfigReportInfo();
                    break;
                case "@factorymerge":
                    FactoryMerge();
                    break;
                case "@devicesummary":
                    SummaryDevice();
                    break;
                case "@pxreport":                 //平行频率查询
                    PXReport();
                    break;
                case "@jzreport":                 //见证频率查询
                    JZReport();
                    break;
                case "@jzsetting":                 //见证频率设置
                    JZSetting();
                    break;

                case "@superstatistics":                    //周报、月报
                    ShowSuperStatistics();
                    break;
                case "@superstatistics_1":                    //资料统计、人员统计
                    ShowSuperStatistics_1();
                    break;
                case "@evaluatecondition":
                    EvaluateConditionSet();                 //不合格报告提醒
                    break;
                case "@samplingfrequency":
                    SamplingFrequencySet();                 //监理抽检频率设置
                    break;
                case "@teststadiuminfo":
                    TestStadiumInfoSet();                   //试验龄期设置
                    break;
                case "@teststadiumtemperature":
                    TestStadiumTemperature();                   //温度试验记录
                    break;
                case "@queryunqualifiedreport":
                    QueryUnqualifiedReport();               //不合格报告查询
                    break;
                case "@querydatamodify":
                    QueryDataModify();               //资料修改审批查询
                    break;
                case "@testovertimeprocessed":
                    TestOverTimeProcessed();
                    break;
                case "@seeloginlog":   //查看登录日志
                    ViewLoginLog();
                    break;
                case "@seeoperatelog":   //查看修改日志
                    ViewOperateLog();
                    break;
                case "@quit":                               //退出程序
                    exitapp();
                    break;

            }

            return base.InvokeMessage(Message);
        }

        private void SummaryDevice()
        {
            var summary = new DeviceSummary();
            summary.Show();
        }

        private void FactoryMerge()
        {
            var form = new FactoryMergeForm();
            form.Show();
        }

        private void TestOverTimeProcessed()
        {
            OverTimeProcessed form = new OverTimeProcessed();
            form.Show();
        }

        private void exitapp()
        {
            Application.OpenForms["MainForm"].Close();
        }

        private void OpenModule(Yqun.Interfaces.YqunMessage Message)
        {
            string BizID = Message.BizID;
            LoadLayout(BizID);

            Boolean SYLQTX = AuthManager.GetFunctionAuth(BizID, "@stadiumreminder");
            Boolean JLPXPLTX = AuthManager.GetFunctionAuth(BizID, "@samplingfrequencyreminder");
            Boolean BHGBGTZ = AuthManager.GetFunctionAuth(BizID, "@unqualifiedreportreminder");
            Boolean SPXG = AuthManager.GetFunctionAuth(BizID, "@approvaldatamodify");

            ReminderInformation.AddReminderLabelToStatusBar(SYLQTX, JLPXPLTX, BHGBGTZ, SPXG);
            ReminderInformation.ReminderLabStadiumList();
        }

        private void ShowDockContent(DockContent Content)
        {
            if (Content == ProjectCatlogContent)
            {
                ProjectCatlogContent.Show(dockPanel, DockState.DockLeft);
            }
            else if (Content == BizViewerContent)
            {
                BizViewerContent.Show(dockPanel, DockState.Document);
            }
            else if (Content == ModelCatlogContent)
            {
                ModelCatlogContent.Show(dockPanel, DockState.DockRight);
            }
        }

        /// <summary>
        /// 遍历树节点
        /// </summary>
        /// <param name="TreeNode"></param>
        /// <param name="OldSelection"></param>
        /// <param name="NewNodeText"></param>
        internal void TraversingTreeNode(TreeNodeCollection TreeNode, Selection OldSelection, String NewNodeText)
        {
            foreach (TreeNode n in TreeNode)
            {
                Selection s = n.Tag as Selection;
                if (s.ID == OldSelection.ID)
                {
                    n.Text = NewNodeText;
                }
                else
                {
                    TraversingTreeNode(n.Nodes, OldSelection, NewNodeText);
                }
            }
        }

        private void dockPanel_ActiveContentChanged(object sender, EventArgs e)
        {
            ToolStrip MainMenu = (ToolStrip)Cache.CustomCache[SystemString.菜单栏];
            for (int i = 0; i < MainMenu.Items.Count; i++)
            {
                ToolStripMenuItem MenuItem = (ToolStripMenuItem)MainMenu.Items[i];
                if (MenuItem.Tag != null && MenuItem.Tag.ToString().ToLower() == "@data")
                {
                    MenuItem.Enabled = true;
                    for (int j = 0; j < MenuItem.DropDownItems.Count; j++)
                    {
                        if (MenuItem.DropDownItems[j].Tag != null &&
                            (MenuItem.DropDownItems[j].Tag.ToString() == "@data_editdata" ||
                             MenuItem.DropDownItems[j].Tag.ToString() == "@data_deledata" ||
                             MenuItem.DropDownItems[j].Tag.ToString() == "@data_copydata" ||
                             MenuItem.DropDownItems[j].Tag.ToString() == "@module_modesetup"))
                        {
                            MenuItem.DropDownItems[j].Enabled = (dockPanel.ActiveContent == BizViewerContent);

                            if (!Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
                            {
                                MenuItem.DropDownItems[j].Enabled = (dockPanel.ActiveContent == BizViewerContent) && AuthDictionary.ContainsKey(MenuItem.DropDownItems[j].Tag.ToString());
                            }
                        }
                    }
                }
            }
        }

        #region 报表

        /// <summary>
        /// 配置报表信息
        /// </summary>
        private void ConfigReportInfo()
        {
            ReportWindow ReportWindow = new ReportWindow();
            Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
            ReportWindow.Location = Owner.PointToScreen(Owner.ClientRectangle.Location);
            ReportWindow.Size = Owner.ClientRectangle.Size;
            ReportWindow.ShowDialog();
        }

        /// <summary>
        /// 平行率报告
        /// </summary>
        private void PXReport()
        {
            PXReportDialog ReportWindow = new PXReportDialog();
            Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
            ReportWindow.Location = Owner.PointToScreen(Owner.ClientRectangle.Location);
            ReportWindow.Size = Owner.ClientRectangle.Size;
            ReportWindow.ShowDialog();
        }

        /// <summary>
        /// 见证率报告
        /// </summary>
        private void JZReport()
        {
            JZReportDialog ReportWindow = new JZReportDialog();
            Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
            ReportWindow.Location = Owner.PointToScreen(Owner.ClientRectangle.Location);
            ReportWindow.Size = Owner.ClientRectangle.Size;
            ReportWindow.ShowDialog();
        }


        /// <summary>
        /// 见证率设置
        /// </summary>
        private void JZSetting()
        {
            BizComponents.见证平行频率提醒.WitnessRateSettingsOptionForm witnessRateSettingsOptionForm = new BizComponents.见证平行频率提醒.WitnessRateSettingsOptionForm();
            Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
            witnessRateSettingsOptionForm.Location = Owner.PointToScreen(Owner.ClientRectangle.Location);
            witnessRateSettingsOptionForm.Size = Owner.ClientRectangle.Size;
            witnessRateSettingsOptionForm.ShowDialog();
        }

        /// <summary>
        /// 周报、月报
        /// </summary>
        private void ShowSuperStatistics()
        {
            TestRoomReportDialog TestRoomReportDialog = new TestRoomReportDialog();
            Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
            TestRoomReportDialog.Location = Owner.PointToScreen(Owner.ClientRectangle.Location);
            TestRoomReportDialog.Size = Owner.ClientRectangle.Size;
            TestRoomReportDialog.ShowDialog(Owner);
        }

        /// <summary>
        /// 资料统计、人员统计
        /// </summary>
        private void ShowSuperStatistics_1()
        {
            EntireLineReportDialog EntireLineReportDialog = new EntireLineReportDialog();
            Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
            EntireLineReportDialog.Location = Owner.PointToScreen(Owner.ClientRectangle.Location);
            EntireLineReportDialog.Size = Owner.ClientRectangle.Size;
            EntireLineReportDialog.ShowDialog(Owner);
        }

        #endregion 报表

        /// <summary>
        /// 报告检测条件设置
        /// </summary>
        private void EvaluateConditionSet()
        {

        }

        /// <summary>
        /// 监理抽检频率设置
        /// </summary>
        private void SamplingFrequencySet()
        {
            SamplingFrequencyDialog SamplingFrequencyDialog = new SamplingFrequencyDialog();
            SamplingFrequencyDialog.ShowDialog();
        }

        /// <summary>
        /// 试验龄期设置
        /// </summary>
        private void TestStadiumInfoSet()
        {
            StadiumDialog StadiumDialog = new StadiumDialog();
            StadiumDialog.ShowDialog();
        }

        /// <summary>
        /// 温度试验记录
        /// </summary>
        private void TestStadiumTemperature()
        {
            String TestRoomCode = Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code;
            TemperatureDialog temperatureDialog = new TemperatureDialog(TestRoomCode);
            temperatureDialog.ShowDialog();
        }
        /// <summary>
        /// 查询不合格报告
        /// </summary>
        private void QueryUnqualifiedReport()
        {
            QueryReportEvaluateDialog Dialog = new QueryReportEvaluateDialog();
            Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
            Dialog.Location = Owner.PointToScreen(Owner.ClientRectangle.Location);
            Dialog.Size = Owner.ClientRectangle.Size;
            Dialog.ShowDialog();
        }

        /// <summary>
        /// 查看登录日志
        /// </summary>
        private void ViewLoginLog()
        {
            ViewLoginLogDialog Dialog = new ViewLoginLogDialog();
            Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
            Dialog.Location = Owner.PointToScreen(Owner.ClientRectangle.Location);
            Dialog.Size = Owner.ClientRectangle.Size;
            Dialog.ShowDialog();
        }

        /// <summary>
        /// 查看操作日志
        /// </summary>
        private void ViewOperateLog()
        {
            ViewOperateLogDialog Dialog = new ViewOperateLogDialog();
            Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
            Dialog.Location = Owner.PointToScreen(Owner.ClientRectangle.Location);
            Dialog.Size = Owner.ClientRectangle.Size;
            Dialog.ShowDialog();
        }

        /// <summary>
        /// 查看资料修改记录
        /// </summary>
        private void QueryDataModify()
        {
            QuerySponsorModifyDialog Dialog = new QuerySponsorModifyDialog();
            Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
            Dialog.Location = Owner.PointToScreen(Owner.ClientRectangle.Location);
            Dialog.Size = Owner.ClientRectangle.Size;
            Dialog.ShowDialog();
        }

        #region 权限管理

        /// <summary>
        /// 工程结构权限
        /// </summary>
        /// <param name="TreeView"></param>
        public override void LoadTrees(TreeView TreeView)
        {
            TreeView.ImageList = ProjectCatlogContent.MainListTree.ImageList;
            DepositoryProjectCatlog.InitProjectCatlogWithoutModel(TreeView);
            TreeView.ExpandAll();
        }

        /// <summary>
        /// 功能权限
        /// </summary>
        /// <returns></returns>
        public override ModuleInfo GetFunctionInfos()
        {
            ModuleInfo moduleInfo = new ModuleInfo();
            moduleInfo.Text = "业务管理";
            moduleInfo.Index = "5E9BBC0B-7DE4-43d4-9BA3-740C37C93144";

            ////////////////////////////////////////////////////////////////////

            //工程结构管理
            SubModuleInfo submoduleInfo = new SubModuleInfo();
            submoduleInfo.Text = "工程结构管理";
            submoduleInfo.Index = "852A810C-4247-44c6-961E-9F72E11B7250";
            moduleInfo.SubModuleInfos.Add(submoduleInfo);

            FunctionInfo finfo = new FunctionInfo();
            finfo.Index = "@top_newproject";
            finfo.Text = "新建工程";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@eng_editproject";
            finfo.Text = "编辑工程";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@eng_deleproject";
            finfo.Text = "删除工程";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@unit_newprjsct";
            finfo.Text = "新建标段";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@tenders_editcontract";
            finfo.Text = "编辑标段";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@tenders_delecontract";
            finfo.Text = "删除标段";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@eng_newdepartment";
            finfo.Text = "新建单位";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@unit_editdepartment";
            finfo.Text = "编辑单位";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@unit_deledepartment";
            finfo.Text = "删除单位";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@tenders_newfolder";
            finfo.Text = "新建文件夹";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@folder_editfolder";
            finfo.Text = "编辑文件夹";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@folder_delefolder";
            finfo.Text = "删除文件夹";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@module_delemodule";
            finfo.Text = "删除模板实例";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@quaauth";
            finfo.Text = "资质授权";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@teststadiumtemperature";
            finfo.Text = "温度试验记录";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@temperatureset";
            finfo.Text = "温度类型设置";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@devicemanagement";
            finfo.Text = "设备管理";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@quit";
            finfo.Text = "退出";
            submoduleInfo.FunctionInfos.Add(finfo);

            //////////////////////////////////////////////////////////////////////////

            //工程资料管理
            submoduleInfo = new SubModuleInfo();
            submoduleInfo.Text = "工程资料管理";
            submoduleInfo.Index = "D5EFF118-C7A1-4197-B433-0A8F77E09FA9";
            moduleInfo.SubModuleInfos.Add(submoduleInfo);

            finfo = new FunctionInfo();
            finfo.Index = "@module_pingxingnewdata";
            finfo.Text = "生成平行资料";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@pxrelationreport";
            finfo.Text = "平行关系对应查询";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@module_newdata";
            finfo.Text = "新建资料";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@data_editdata";
            finfo.Text = "编辑资料";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@data_deledata";
            finfo.Text = "删除资料";
            submoduleInfo.FunctionInfos.Add(finfo);


            finfo = new FunctionInfo();
            finfo.Index = "@data_selectdataid";
            finfo.Text = "查看DataID";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@temperaturelist";
            finfo.Text = "查看温度记录";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@data_copydata";
            finfo.Text = "复制资料";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@data_pastdata";
            finfo.Text = "粘贴资料";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@module_modesetup";
            finfo.Text = "台账设置";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@data_exportdata";
            finfo.Text = "导出台账";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@data_batchprint";
            finfo.Text = "批量打印";
            submoduleInfo.FunctionInfos.Add(finfo);

            ///////////////////////////////////////////////////////////////////////

            //统计
            submoduleInfo = new SubModuleInfo();
            submoduleInfo.Text = "统计";
            submoduleInfo.Index = "A25EB10A-F1DA-4e49-8490-09B226E6D78B";
            moduleInfo.SubModuleInfos.Add(submoduleInfo);

            finfo = new FunctionInfo();
            finfo.Index = "@superstatistics";
            finfo.Text = "周报、月报";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@superstatistics_1";
            finfo.Text = "资料统计、人员统计、设备统计";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@statisticsdesigner";
            finfo.Text = "报表设计器（管理员）";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@pxreport";
            finfo.Text = "平行率报告";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@jzreport";
            finfo.Text = "见证率报告";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@devicesummary";
            finfo.Text = "设备情况";
            submoduleInfo.FunctionInfos.Add(finfo);

            //////////////////////////////////////////////////////////////////////

            //消息提醒
            submoduleInfo = new SubModuleInfo();
            submoduleInfo.Text = "消息提醒";
            submoduleInfo.Index = "10EF6576-75F5-4142-B353-0B0639874552";
            moduleInfo.SubModuleInfos.Add(submoduleInfo);

            finfo = new FunctionInfo();
            finfo.Index = "@evaluatecondition";
            finfo.Text = "报告检测条件设置";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@samplingfrequency";
            finfo.Text = "监理抽检频率设置";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@teststadiuminfo";
            finfo.Text = "试验龄期设置";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@queryunqualifiedreport";
            finfo.Text = "查询不合格报告";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@teststadiumtemperature";
            finfo.Text = "温度试验记录";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@temperatureset";
            finfo.Text = "温度类型设置";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@stadiumreminder";
            finfo.Text = "待做事项列表(状态栏)";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@samplingfrequencyreminder";
            finfo.Text = "监理平行频率提醒(状态栏)";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@unqualifiedreportreminder";
            finfo.Text = "不合格报告提醒(状态栏)";
            submoduleInfo.FunctionInfos.Add(finfo);

            //////////////////////////////////////////////////////////////////////

            //审批
            submoduleInfo = new SubModuleInfo();
            submoduleInfo.Text = "审批";
            submoduleInfo.Index = "E9031D48-9662-4a53-B754-5F34B1494797";
            moduleInfo.SubModuleInfos.Add(submoduleInfo);

            finfo = new FunctionInfo();
            finfo.Index = "@querydatamodify";
            finfo.Text = "查询资料修改";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@testovertimeprocessed";
            finfo.Text = "查看已审批的过期试验";
            submoduleInfo.FunctionInfos.Add(finfo);


            finfo = new FunctionInfo();
            finfo.Index = "@approvaldatamodify";
            finfo.Text = "审批修改(状态栏)";
            submoduleInfo.FunctionInfos.Add(finfo);

            ////////////////////////////////////////////////////////////////////////////

            //日志
            submoduleInfo = new SubModuleInfo();
            submoduleInfo.Text = "日志";
            submoduleInfo.Index = "20AD32F0-D6D4-4888-B915-52A354A2AB69";
            moduleInfo.SubModuleInfos.Add(submoduleInfo);

            finfo = new FunctionInfo();
            finfo.Index = "@seeloginlog";
            finfo.Text = "查看登陆日志";
            submoduleInfo.FunctionInfos.Add(finfo);

            finfo = new FunctionInfo();
            finfo.Index = "@seeoperatelog";
            finfo.Text = "查询修改日志";
            submoduleInfo.FunctionInfos.Add(finfo);

            return moduleInfo;
        }

        #endregion 权限管理
    }
}
