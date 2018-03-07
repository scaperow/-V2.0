using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.IO;
using Yqun.Interfaces;
using Yqun.Bases.ControlBases;
using Yqun.Common.ContextCache;
using Yqun.Bases;
using System.Threading;
using Yqun.Bases.ClassBases;
using System.Net;
using System.Text;
using System.Drawing.Drawing2D;
using System.Xml;
using System.Diagnostics;
using System.Collections.Generic;
using Yqun.Controls.ScrollingTextControl;
using WeifenLuo.WinFormsUI.Docking;
using System.ComponentModel;
using Yqun;
using Yqun.MainUI.Properties;
using Yqun.Permissions.Common;
using Yqun.Services;
using BizCommon;
using System.Runtime.InteropServices;
using BizComponents;
namespace Yqun.MainUI
{
    public delegate void SetStatusVisibleHander(Boolean flag);

    /// <summary>
    /// Summary description for MainForm.
    /// </summary>
    public partial class MainForm : Form, Yqun.Interfaces.WinUILayout
    {
        protected ToolStrip ToolStrip;
        protected StatusStrip StatusBar;
        protected MenuStrip menuStrip1;

        private DockPanel dockPanel;
        private BackgroundWorker backgroundWorker1;
        private IContainer components;
        private ToolStripStatusLabel State_5 = null;

        Kingrocket.NotifyClient.ClientHelper client;
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MainForm()
        {
            InitializeComponent();

            this.Icon = Resources.logo16;

            dockPanel.ContentAdded += new EventHandler<DockContentEventArgs>(dockPanel_ContentAdded);
            dockPanel.ContentClosing += new FormClosingEventHandler(dockManager_ContentClosing);
            dockPanel.ContentClosed += new FormClosedEventHandler(dockManager_ContentClosed);
            dockPanel.ActiveDocumentChanged += new EventHandler(dockManager_ActiveDocumentChanged);

            string text = Yqun.ClientConfigurationInfo.GetAppName();
            this.Text = text;

            LoadMainMenu(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;

            if (!Cache.CustomCache.Contains(SystemString.菜单栏))
            {
                Cache.CustomCache.Add(SystemString.菜单栏, this.MainMenuStrip);
            }

            if (!Cache.CustomCache.Contains(SystemString.工具栏))
            {
                Cache.CustomCache.Add(SystemString.工具栏, this.ToolStrip);
            }

            if (!Cache.CustomCache.Contains(SystemString.主窗口))
            {
                Cache.CustomCache.Add(SystemString.主窗口, this);
            }
        }

        #region 窗体设计器

        private void InitializeComponent()
        {
            WeifenLuo.WinFormsUI.Docking.DockPanelSkin dockPanelSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPanelSkin();
            WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin1 = new WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient1 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient2 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient3 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient4 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient5 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient3 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient6 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient7 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            this.ToolStrip = new System.Windows.Forms.ToolStrip();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // ToolStrip
            // 
            this.ToolStrip.Location = new System.Drawing.Point(0, 24);
            this.ToolStrip.Name = "ToolStrip";
            this.ToolStrip.Size = new System.Drawing.Size(1027, 25);
            this.ToolStrip.TabIndex = 3;
            this.ToolStrip.Text = "工具栏";
            // 
            // StatusBar
            // 
            this.StatusBar.AutoSize = false;
            this.StatusBar.Location = new System.Drawing.Point(0, 702);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(1027, 22);
            this.StatusBar.TabIndex = 4;
            this.StatusBar.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1027, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dockPanel
            // 
            this.dockPanel.ActiveAutoHideContent = null;
            this.dockPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.DockBackColor = System.Drawing.SystemColors.ControlDark;
            this.dockPanel.Location = new System.Drawing.Point(0, 49);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(1027, 653);
            dockPanelGradient1.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient1.StartColor = System.Drawing.SystemColors.ControlLight;
            autoHideStripSkin1.DockStripGradient = dockPanelGradient1;
            tabGradient1.EndColor = System.Drawing.SystemColors.Control;
            tabGradient1.StartColor = System.Drawing.SystemColors.Control;
            tabGradient1.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            autoHideStripSkin1.TabGradient = tabGradient1;
            dockPanelSkin1.AutoHideStripSkin = autoHideStripSkin1;
            tabGradient2.EndColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.StartColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.ActiveTabGradient = tabGradient2;
            dockPanelGradient2.EndColor = System.Drawing.SystemColors.Control;
            dockPanelGradient2.StartColor = System.Drawing.SystemColors.Control;
            dockPaneStripGradient1.DockStripGradient = dockPanelGradient2;
            tabGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.InactiveTabGradient = tabGradient3;
            dockPaneStripSkin1.DocumentGradient = dockPaneStripGradient1;
            tabGradient4.EndColor = System.Drawing.SystemColors.ActiveCaption;
            tabGradient4.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient4.StartColor = System.Drawing.SystemColors.GradientActiveCaption;
            tabGradient4.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            dockPaneStripToolWindowGradient1.ActiveCaptionGradient = tabGradient4;
            tabGradient5.EndColor = System.Drawing.SystemColors.Control;
            tabGradient5.StartColor = System.Drawing.SystemColors.Control;
            tabGradient5.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient1.ActiveTabGradient = tabGradient5;
            dockPanelGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            dockPaneStripToolWindowGradient1.DockStripGradient = dockPanelGradient3;
            tabGradient6.EndColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient6.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient6.StartColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient6.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient1.InactiveCaptionGradient = tabGradient6;
            tabGradient7.EndColor = System.Drawing.Color.Transparent;
            tabGradient7.StartColor = System.Drawing.Color.Transparent;
            tabGradient7.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            dockPaneStripToolWindowGradient1.InactiveTabGradient = tabGradient7;
            dockPaneStripSkin1.ToolWindowGradient = dockPaneStripToolWindowGradient1;
            dockPanelSkin1.DockPaneStripSkin = dockPaneStripSkin1;
            this.dockPanel.Skin = dockPanelSkin1;
            this.dockPanel.TabIndex = 8;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1027, 724);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.ToolStrip);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.Text = "铁路试验信息管理系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!ProcessHelper.IsRuningProcess("JZUpgradeAgent"))
            {
                ProcessStartInfo Info = new ProcessStartInfo();
                Info.CreateNoWindow = false;
                Info.UseShellExecute = true;
                Info.FileName = Path.Combine(Application.StartupPath, "JZUpgrade.exe");
                Info.Arguments = "\"8\"";
                //1 管理系统文件+不执行，2 采集系统文件+不执行，3 管理系统数据+不执行，
                //4 管理系统文件+数据+执行； 
                //5 采集系统文件+执行
                //6 管理系统执行
                //7 采集系统执行
                //8 管理系统文件+执行
                Process.Start(Info);

            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #endregion 窗体设计器

        #region 主窗口

        public virtual void MainForm_Load(object sender, System.EventArgs e)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockManager.config");
            if (File.Exists(configFile))
                if (dockPanel == null)
                    dockPanel.LoadFromXml(configFile, new DeserializeDockContent(GetContentFromPersistString));

            AddStatusStrip();

            try
            {
                ShowNavBarExplorer();
                //backgroundWorker1.RunWorkerAsync();

                CheckForIllegalCrossThreadCalls = false;
                string LineID = Yqun.Common.ContextCache.ApplicationContext.Current.InProject.Index;
                string UserName = Yqun.Common.ContextCache.ApplicationContext.Current.UserName;
                string LineName = Yqun.Common.ContextCache.ApplicationContext.Current.InProject.Description;
                string TestRoomCode = Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code;
                string SegmentName = Yqun.Common.ContextCache.ApplicationContext.Current.InSegment.Description;
                string CompanyName = Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Description;
                string TestRoomName = Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Description;
                client = new Kingrocket.NotifyClient.ClientHelper(LineID, UserName, LineName, TestRoomCode, SegmentName, CompanyName, TestRoomName);
            }
            catch (Exception ex)
            {
            }

            //ThreadPool.QueueUserWorkItem(new WaitCallback(RunQuartzService), Yqun.Common.ContextCache.ApplicationContext.Current);
        }

        private void RunQuartzService(object State)
        {
            //Yqun.Common.ContextCache.ApplicationContext.Current = State as Yqun.Common.ContextCache.ApplicationContext;
            //LocalQuartzService.GetQuartzService().Start();
        }

        /// <summary>
        /// 显示导航窗口
        /// </summary>
        protected virtual void ShowNavBarExplorer()
        {
            NavBarExplorer m_solutionExplorer = new NavBarExplorer();
            m_solutionExplorer.obj = this;
            m_solutionExplorer.IsRunTime = true;
            Icon icon = Icon.FromHandle(Resources.解决方案.GetHicon());
            this.AddControl(m_solutionExplorer.Name, "解决方案", icon, m_solutionExplorer, Yqun.Interfaces.LayoutDockType.Left, m_solutionExplorer.Name);
        }

        public DockContent FindContentByName(string Key)
        {
            foreach (DockContent c in dockPanel.Contents)
            {
                if (c.Name == Key)
                {
                    return c;
                }
            }

            return null;
        }

        public virtual void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string configFile = Path.Combine(SystemFolder.DockingConfig, "dockManager.config");
            dockPanel.SaveAsXml(configFile);
            DialogResult DResult = MessageBox.Show("确定要退出系统?", "退出", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DResult == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                //退出提醒服务
                //LocalQuartzService.GetQuartzService().Shutdown();
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;

                if (!Yqun.Common.ContextCache.ApplicationContext.Current.ISLocalService)
                {
                    //int hd = BizCommon.JZUser32Helper.FindWindow(null, "更新进度");
                    //if (hd == 0)
                    //{
                    //    ProcessStartInfo Info = new ProcessStartInfo();
                    //    Info.CreateNoWindow = false;
                    //    Info.UseShellExecute = true;
                    //    Info.FileName = Path.Combine(Application.StartupPath, "JZUpdate.exe");
                    //    Info.Arguments = "\"8\"";
                    //1 管理系统文件+不执行，2 采集系统文件+不执行，3 管理系统数据+不执行，
                    //4 管理系统文件+数据+执行； 
                    //5 采集系统文件+执行
                    //6 管理系统执行
                    //7 采集系统执行
                    //8 管理系统文件+执行
                    //Process.Start(Info);
                    //Thread.Sleep(500);
                    //hd = BizCommon.JZUser32Helper.FindWindow(null, "更新进度");
                    //}

                    //BizCommon.JZUser32Helper.COPYDATASTRUCT cds;
                    //cds.dwData = (IntPtr)100;
                    //cds.lpData = "1";
                    //cds.cbData = 10;
                    //BizCommon.JZUser32Helper.SendMessage(hd, WM_COPYDATA, 0, ref cds);
                }
                try
                {
                    //logger.Error("mainform closed");
                    if (client != null)
                    {
                        client.Close();
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                    logger.Error("client close:" + ex.Message);
                }
            }
        }

        public virtual DockContent GetContentFromPersistString(string persistString)
        {
            return null;
        }

        public virtual DockContent FindContent(string text)
        {
            foreach (DockContent c in dockPanel.Contents)
                if (c.Text == text)
                    return c;

            return null;
        }

        #endregion 主窗口

        #region WinUILayout Members

        public new void ActiveControl(string Key, LayoutDockType DockType)
        {
            DockContent content = GetContent(Key, DockType);
            if (content != null)
                content.DockHandler.Activate();
            else
            {
                int Count = GetDockPaneCount(Key);

                string Caption = Key;
                if (Count != 0)
                    Caption = Key + "(" + Count.ToString() + ")";

                content = new DockContent();
                content.Text = Caption;
                content.Tag = Key;

                if (DockType == LayoutDockType.Middle)
                    content.DockAreas = DockAreas.Document;
                else
                    content.DockAreas = DockAreas.DockBottom | DockAreas.DockLeft | DockAreas.DockTop | DockAreas.DockRight;

                content.Show(dockPanel, GetDockState(DockType));
            }
        }

        public void AddControl(string Key, string Description, Icon icon, Control ToAddControl, LayoutDockType DockType, string ReferenceKey)
        {
            if (DockType == LayoutDockType.Surround)
                throw new ArgumentException("参数DockType的停靠状态无效.");

            int Count = GetDockPaneCount(Description);
            string Caption = Description;
            if (Count != 0)
                Caption = Description + "(" + Count.ToString() + ")";

            DockContent content = null;
            switch (DockType)
            {
                case LayoutDockType.Left:
                case LayoutDockType.Right:
                case LayoutDockType.Top:
                case LayoutDockType.Bottom:
                    content = GetContent(Key, LayoutDockType.Surround);
                    if (content != null)
                    {
                        if (GetLayoutDockType(content.DockState) == DockType)
                        {
                            content.Activate();
                            return;
                        }

                        DockPane[] cw = GetDockPane(GetDockState(DockType));
                        DockPane selectcw = GetDockPane(ReferenceKey, cw);

                        if (cw != null)
                        {
                            if (selectcw != null)
                                content.Pane = selectcw;
                            else
                                content.Pane = cw[0];

                            if (icon != null)
                                content.Icon = icon;
                        }
                        else
                        {
                            content = new DockContent();
                            content.Text = Caption;
                            content.Tag = Key;
                            content.DockAreas = DockAreas.DockBottom | DockAreas.DockLeft | DockAreas.DockTop | DockAreas.DockRight;

                            if (icon != null)
                                content.Icon = icon;

                            ToAddControl.Dock = DockStyle.Fill;
                            if (!content.Controls.Contains(ToAddControl))
                                content.Controls.Add(ToAddControl);

                            content.Show(dockPanel, GetDockState(DockType));
                        }
                    }
                    else
                    {
                        content = new DockContent();
                        content.Text = Caption;
                        content.Tag = Key;

                        if (icon != null)
                            content.Icon = icon;

                        ToAddControl.Dock = DockStyle.Fill;
                        if (!content.Controls.Contains(ToAddControl))
                            content.Controls.Add(ToAddControl);

                        content.Show(dockPanel, GetDockState(DockType));
                    }

                    break;
                case LayoutDockType.Middle:
                    content = GetContent(Key, LayoutDockType.Middle);
                    if (content != null)
                    {
                        if (GetLayoutDockType(content.DockState) == DockType)
                        {
                            content.Activate();
                            return;
                        }

                        DockPane[] cw = GetDockPane(GetDockState(DockType));
                        if (cw != null)
                        {
                            content.Pane = cw[0];
                            if (icon != null)
                                content.Icon = icon;
                        }
                        else
                        {
                            content = new DockContent();
                            if (!content.Controls.Contains(ToAddControl))
                                content.Controls.Add(ToAddControl);
                            ToAddControl.Dock = DockStyle.Fill;
                            content.Text = Caption;
                            content.Tag = Key;
                            content.DockAreas = DockAreas.Document;

                            if (icon != null)
                                content.Icon = icon;

                            content.Show(dockPanel, GetDockState(DockType));

                        }
                    }
                    else
                    {
                        content = new DockContent();
                        content.Text = Caption;
                        content.Tag = Key;

                        if (icon != null)
                            content.Icon = icon;

                        ToAddControl.Dock = DockStyle.Fill;
                        if (!content.Controls.Contains(ToAddControl))
                            content.Controls.Add(ToAddControl);

                        content.Show(dockPanel, GetDockState(DockType));
                    }

                    break;
            }
        }

        public bool Contains(string Key, LayoutDockType DockType)
        {
            DockContent content = GetContent(Key, DockType);
            return content != null;
        }

        public Control GetControl(string Key, LayoutDockType DockType)
        {
            try
            {
                DockContent content = GetContent(Key, DockType);
                return content != null ? content.Controls[0] : null;
            }
            catch
            { }

            return null;
        }

        public void Hide(string Key, LayoutDockType DockType)
        {
            DockContent content = GetContent(Key, DockType);
            if (content != null)
                content.Hide();
        }

        public string IndexOf(string Description, LayoutDockType DockType)
        {
            string ResultKey = string.Empty;
            switch (DockType)
            {
                case LayoutDockType.Left:
                    ResultKey = GetContentText(Description, DockState.DockLeftAutoHide);
                    if (ResultKey != null)
                        return ResultKey;

                    return GetContentText(Description, DockState.DockLeft);
                case LayoutDockType.Right:
                    ResultKey = GetContentText(Description, DockState.DockRightAutoHide);
                    if (ResultKey != null)
                        return ResultKey;

                    return GetContentText(Description, DockState.DockRight);
                case LayoutDockType.Top:
                    ResultKey = GetContentText(Description, DockState.DockTopAutoHide);
                    if (ResultKey != null)
                        return ResultKey;

                    return GetContentText(Description, DockState.DockTop);
                case LayoutDockType.Bottom:
                    ResultKey = GetContentText(Description, DockState.DockBottomAutoHide);
                    if (ResultKey != null)
                        return ResultKey;

                    return GetContentText(Description, DockState.DockBottom);
                case LayoutDockType.Middle:
                    return GetContentText(Description, DockState.Document);
            }

            return "";
        }

        public void Remove(string Key, LayoutDockType DockType)
        {
            bool bRemoved;
            switch (DockType)
            {
                case LayoutDockType.Left:
                    bRemoved = RemoveContent(Key, DockState.DockLeftAutoHide);
                    if (bRemoved == false)
                        bRemoved = RemoveContent(Key, DockState.DockLeft);

                    break;
                case LayoutDockType.Right:
                    bRemoved = RemoveContent(Key, DockState.DockRightAutoHide);
                    if (bRemoved == false)
                        bRemoved = RemoveContent(Key, DockState.DockRight);

                    break;
                case LayoutDockType.Top:
                    bRemoved = RemoveContent(Key, DockState.DockTopAutoHide);
                    if (bRemoved == false)
                        bRemoved = RemoveContent(Key, DockState.DockTop);

                    break;
                case LayoutDockType.Bottom:
                    bRemoved = RemoveContent(Key, DockState.DockBottomAutoHide);
                    if (bRemoved == false)
                        RemoveContent(Key, DockState.DockBottom);

                    break;
                case LayoutDockType.Middle:
                    RemoveContent(Key, DockState.Document);
                    break;
            }

            RemoveContent(Key, DockState.Float);
            RemoveContent(Key, DockState.Hidden);
        }

        /// <summary>
        /// 查找停靠的Tab页
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        private DockContent GetContent(string Key, LayoutDockType DockType)
        {
            DockContent result = null;
            switch (DockType)
            {
                case LayoutDockType.Left:
                    DockPane[] DockLeft = GetDockPane(DockState.DockLeft);

                    result = GetContent(Key, DockLeft);
                    if (result != null)
                        return result;

                    DockPane[] DockLeftAutoHide = GetDockPane(DockState.DockLeftAutoHide);
                    return GetContent(Key, DockLeftAutoHide);
                case LayoutDockType.Right:

                    DockPane[] DockRight = GetDockPane(DockState.DockRight);

                    result = GetContent(Key, DockRight);
                    if (result != null)
                        return result;

                    DockPane[] DockRightAutoHide = GetDockPane(DockState.DockRightAutoHide);
                    return GetContent(Key, DockRightAutoHide);
                case LayoutDockType.Top:

                    DockPane[] DockTop = GetDockPane(DockState.DockTop);

                    result = GetContent(Key, DockTop);
                    if (result != null)
                        return result;

                    DockPane[] DockTopAutoHide = GetDockPane(DockState.DockTopAutoHide);
                    return GetContent(Key, DockTopAutoHide);
                case LayoutDockType.Bottom:

                    DockPane[] DockBottom = GetDockPane(DockState.DockBottom);

                    result = GetContent(Key, DockBottom);
                    if (result != null)
                        return result;

                    DockPane[] DockBottomAutoHide = GetDockPane(DockState.DockBottomAutoHide);
                    return GetContent(Key, DockBottomAutoHide);
                case LayoutDockType.Middle:

                    DockPane[] Document = GetDockPane(DockState.Document);

                    return GetContent(Key, Document);
                case LayoutDockType.Surround:

                    DockPane[] DockLeft0 = GetDockPane(DockState.DockLeft);
                    result = GetContent(Key, DockLeft0);
                    if (result != null)
                        return result;

                    DockPane[] DockLeftAutoHide0 = GetDockPane(DockState.DockLeftAutoHide);
                    result = GetContent(Key, DockLeftAutoHide0);
                    if (result != null)
                        return result;

                    DockPane[] DockRight1 = GetDockPane(DockState.DockRight);
                    result = GetContent(Key, DockRight1);
                    if (result != null)
                        return result;

                    DockPane[] DockRightAutoHide1 = GetDockPane(DockState.DockRightAutoHide);
                    result = GetContent(Key, DockRightAutoHide1);
                    if (result != null)
                        return result;

                    DockPane[] DockTop2 = GetDockPane(DockState.DockTop);
                    result = GetContent(Key, DockTop2);
                    if (result != null)
                        return result;

                    DockPane[] DockTopAutoHide2 = GetDockPane(DockState.DockTopAutoHide);
                    result = GetContent(Key, DockTopAutoHide2);
                    if (result != null)
                        return result;

                    DockPane[] DockBottom3 = GetDockPane(DockState.DockBottom);
                    result = GetContent(Key, DockBottom3);
                    if (result != null)
                        return result;

                    DockPane[] DockBottomAutoHide3 = GetDockPane(DockState.DockBottomAutoHide);
                    return GetContent(Key, DockBottomAutoHide3);
                default:
                    break;
            }

            return result;
        }

        private DockContent GetContent(string Key, DockPane[] DockPanes)
        {
            if (DockPanes == null)
                throw new ArgumentNullException("DockPanes 为null。");

            foreach (DockPane Pane in DockPanes)
            {
                foreach (DockContent Content in Pane.Contents)
                {
                    if (Content.Controls.Count > 0 && Content.Tag.ToString().ToLower() == Key.ToLower())
                    {
                        ControlBase Base = Content.Controls[0] as ControlBase;
                        if (Base != null)
                        {
                            return Content;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 查找某种状态的ContentWindow
        /// </summary>
        /// <param name="dockstate"></param>
        /// <returns></returns>
        private DockPane[] GetDockPane(DockState dockstate)
        {
            int DockPanes = 0;
            for (int i = 0; i < dockPanel.Panes.Count; i++)
            {
                DockPane cw = dockPanel.Panes[i];
                if (cw.DockState == dockstate)
                    DockPanes++;
            }

            if (DockPanes == 0)
                return new DockPane[0];

            DockPane[] Result = new DockPane[DockPanes];
            int Index = 0;
            for (int i = 0; i < dockPanel.Panes.Count; i++)
            {
                DockPane cw = dockPanel.Panes[i];
                if (cw.DockState == dockstate)
                    Result[Index++] = cw;
            }

            return Result;
        }

        /// <summary>
        /// DockState => LayoutDockType
        /// </summary>
        /// <param name="dockstate"></param>
        /// <returns></returns>
        private LayoutDockType GetLayoutDockType(DockState dockstate)
        {
            switch (dockstate)
            {
                case DockState.DockLeft:
                case DockState.DockLeftAutoHide:
                    return LayoutDockType.Left;
                case DockState.DockRight:
                case DockState.DockRightAutoHide:
                    return LayoutDockType.Right;
                case DockState.DockTop:
                case DockState.DockTopAutoHide:
                    return LayoutDockType.Top;
                case DockState.DockBottom:
                case DockState.DockBottomAutoHide:
                    return LayoutDockType.Bottom;
            }

            return LayoutDockType.Middle;
        }

        /// <summary>
        /// LayoutDockType => DockState
        /// </summary>
        /// <param name="DockType"></param>
        /// <returns></returns>
        private DockState GetDockState(LayoutDockType DockType)
        {
            switch (DockType)
            {
                case LayoutDockType.Left:
                    DockPane[] DockLeftAutoHide = GetDockPane(DockState.DockLeftAutoHide);
                    if (DockLeftAutoHide != null)
                        return DockState.DockLeftAutoHide;
                    else
                        return DockState.DockLeft;
                case LayoutDockType.Right:
                    DockPane[] DockRightAutoHide = GetDockPane(DockState.DockRightAutoHide);
                    if (DockRightAutoHide != null)
                        return DockState.DockRightAutoHide;
                    else
                        return DockState.DockRight;
                case LayoutDockType.Top:
                    DockPane[] DockTopAutoHide = GetDockPane(DockState.DockTopAutoHide);
                    if (DockTopAutoHide != null)
                        return DockState.DockTopAutoHide;
                    else
                        return DockState.DockTop;
                case LayoutDockType.Bottom:
                    DockPane[] DockBottomAutoHide = GetDockPane(DockState.DockBottomAutoHide);
                    if (DockBottomAutoHide != null)
                        return DockState.DockBottomAutoHide;
                    else
                        return DockState.DockBottom;
                case LayoutDockType.Middle:
                    return DockState.Document;
            }

            return DockState.Float;
        }

        private DockPane GetDockPane(string Key, DockPane[] WhereDockPane)
        {
            for (int i = 0; i < WhereDockPane.Length; i++)
            {
                DockPane cw = WhereDockPane[i];
                for (int j = 0; j < cw.Contents.Count; j++)
                {
                    if (cw.Contents[j].DockHandler.Form.Name == Key)
                        return cw;
                }
            }

            return null;
        }

        private int GetDockPaneCount(string Description)
        {
            int CountofDesc = 0;
            for (int i = 0; i < dockPanel.Panes.Count; i++)
            {
                DockPane cw = dockPanel.Panes[i];
                for (int j = 0; j < cw.Contents.Count; j++)
                {
                    if (cw.Contents[j].DockHandler.Form.Text == Description)
                        CountofDesc++;
                }
            }

            return CountofDesc;
        }

        private string GetContentText(string Description, DockState dockState)
        {
            for (int i = 0; i < dockPanel.Panes.Count; i++)
            {
                DockPane cw = dockPanel.Panes[i];
                if (cw.DockState == dockState)
                {
                    for (int j = 0; j < cw.Contents.Count; j++)
                    {
                        if (cw.Contents[j].DockHandler.Form.Text == Description)
                            return cw.Contents[j].DockHandler.Form.Text;
                    }
                }
            }

            return null;
        }

        private bool RemoveContent(string Key, DockState dockState)
        {
            List<DockPane> DockPanes = new List<DockPane>();
            foreach (DockPane Pane in dockPanel.Panes)
            {
                if (Pane.DockState == dockState)
                {
                    DockPanes.Add(Pane);
                }
            }

            foreach (DockPane Pane in DockPanes)
            {
                foreach (DockContent c in Pane.Contents)
                {
                    if (c.Name == Key)
                    {
                        c.Hide();
                    }
                }
            }

            return true;
        }

        #endregion

        #region UI Helper

        protected MenuStrip GetMainMenu()
        {
            return this.menuStrip1;
        }

        protected ToolStrip GetToolStrip()
        {
            return this.ToolStrip;
        }

        protected StatusStrip GetStatusBar()
        {
            return this.StatusBar;
        }

        #endregion UI Helper

        #region 停靠组件事件

        void dockManager_ContentClosing(object sender, FormClosingEventArgs e)
        {
            DockContent Content = sender as DockContent;
            if (Content.Controls.Count == 0 || !Content.Controls[0].GetType().IsSubclassOf(typeof(ControlBase)))
                return;

            //保存窗口布局
            IDockingInfo DockingInfo = Content.Controls[0] as IDockingInfo;
            if (DockingInfo != null)
                DockingInfo.UpdateDockingInfo();

            ControlBase ControlBase = Content.Controls[0] as ControlBase;
            if (ControlBase != null)
            {
                if (ControlBase.Modified)
                {
                    string message = "是否将更改保存到数据库中？";
                    if (DialogResult.No == MessageBox.Show(message, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        IMessage Message = ControlBase as IMessage;
                        YqunMessage yqunMessage = new YqunMessage();
                        yqunMessage.BizID = ControlBase.BizID;
                        yqunMessage.TypeFlag = "@savedata";
                        Message.InvokeMessage(yqunMessage);
                    }
                }
            }
        }

        void dockManager_ContentClosed(object sender, FormClosedEventArgs e)
        {
            DockContent Content = sender as DockContent;
            if (Content.Controls.Count == 0 || !Content.Controls[0].GetType().IsSubclassOf(typeof(ControlBase)))
                return;

            ControlBase ControlBase = Content.Controls[0] as ControlBase;
            Yqun.Services.MainMenuHelper.RemoveMenuFromCache(ControlBase.BizID);
        }

        private void dockManager_ActiveDocumentChanged(object sender, System.EventArgs e)
        {
            IDockContent Content = dockPanel.ActiveDocument;
            if (Content != null && GetLayoutDockType(Content.DockHandler.DockState) == LayoutDockType.Middle)
            {
                try
                {
                    ControlBase ControlBase = Content.DockHandler.Form.Controls[0] as ControlBase;
                    Yqun.Services.MainMenuHelper.ShowMenuAtMainForm(ControlBase.BizID, new ToolStripMenuItem[] { MenuItemSystem, MenuItemWindow, MenuItemHelp });

                    //应用权限到模块
                    IApplyAuth ApplyAuth = ControlBase as IApplyAuth;
                    if (ApplyAuth != null)
                    {
                        ApplyAuth.ApplySystemAuth();
                    }
                }
                catch
                { }
            }
        }

        void dockPanel_ContentAdded(object sender, DockContentEventArgs e)
        {
            IDockContent Content = e.Content;
            if (Content.DockHandler.Form.Controls.Count == 0 || !Content.DockHandler.Form.Controls[0].GetType().IsSubclassOf(typeof(ControlBase)))
                return;

            ControlBase ControlBase = Content.DockHandler.Form.Controls[0] as ControlBase;
            IMenu menu = ControlBase as IMenu;
            Yqun.Services.MainMenuHelper.AddMenuToCache(menu);
        }

        #endregion 停靠组件事件

        #region 增加状态栏

        private void AddStatusStrip()
        {
            StatusBar.SuspendLayout();

            ToolStripStatusLabel State_UserName = new System.Windows.Forms.ToolStripStatusLabel();
            State_UserName.TextAlign = ContentAlignment.MiddleCenter;
            State_UserName.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            State_UserName.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            State_UserName.Name = "State_UserName";
            State_UserName.Dock = DockStyle.Left;

            ToolStripStatusLabel State_Project = new System.Windows.Forms.ToolStripStatusLabel();
            State_Project.TextAlign = ContentAlignment.MiddleCenter;
            State_Project.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            State_Project.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            State_Project.Name = "State_UserName";
            State_Project.Dock = DockStyle.Left;

            ToolStripStatusLabel State_Service = new System.Windows.Forms.ToolStripStatusLabel();
            State_Service.TextAlign = ContentAlignment.MiddleCenter;
            State_Service.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            State_Service.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            State_Service.Name = "State_Service";
            State_Service.Dock = DockStyle.Left;

            ScrollingText scrollingText = new ScrollingText();
            scrollingText.ShowBorder = false;
            scrollingText.BackgroundBrush = new SolidBrush(this.BackColor);
            scrollingText.Font = new Font(scrollingText.Font.FontFamily, scrollingText.Font.Size, FontStyle.Regular | FontStyle.Underline);
            scrollingText.ScrollDirection = ScrollDirection.RightToLeft;
            scrollingText.VerticleTextPosition = VerticleTextPosition.Center;
            scrollingText.TextScrollSpeed = 100;
            scrollingText.StopScrollOnMouseOver = true;
            scrollingText.ScrollText = "联系在线客服请点击!";// "客户服务电话：010-51916122转601，029-62763213";
            scrollingText.Width = 300;
            scrollingText.TextClicked -= new ScrollingText.TextClickEventHandler(scrollingText_TextClicked);
            scrollingText.TextClicked += new ScrollingText.TextClickEventHandler(scrollingText_TextClicked);
            ToolStripControlHost ScrollingTextHost = new ToolStripControlHost(scrollingText);
            ScrollingTextHost.BackColor = this.BackColor;
            ScrollingTextHost.Dock = DockStyle.Fill;

            ToolStripStatusLabel State_Reminder_1 = new System.Windows.Forms.ToolStripStatusLabel();
            State_Reminder_1.Font = new Font(State_Reminder_1.Font.FontFamily, State_Reminder_1.Font.Size, FontStyle.Regular | FontStyle.Underline);
            State_Reminder_1.TextAlign = ContentAlignment.MiddleCenter;
            State_Reminder_1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            State_Reminder_1.ForeColor = Color.Blue;
            State_Reminder_1.Name = "State_Reminder_1";
            State_Reminder_1.Dock = DockStyle.Fill;
            State_Reminder_1.IsLink = true;
            State_Reminder_1.Width = 150;
            State_Reminder_1.Visible = false;

            ToolStripStatusLabel State_Reminder_2 = new System.Windows.Forms.ToolStripStatusLabel();
            State_Reminder_2.Font = new Font(State_Reminder_1.Font.FontFamily, State_Reminder_1.Font.Size, FontStyle.Regular | FontStyle.Underline);
            State_Reminder_2.TextAlign = ContentAlignment.MiddleCenter;
            State_Reminder_2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            State_Reminder_2.ForeColor = Color.Blue;
            State_Reminder_2.Name = "State_Reminder_2";
            State_Reminder_2.Dock = DockStyle.Fill;
            State_Reminder_2.IsLink = true;
            State_Reminder_2.Width = 150;
            State_Reminder_2.Visible = false;

            ToolStripStatusLabel State_Reminder_3 = new System.Windows.Forms.ToolStripStatusLabel();
            State_Reminder_3.Font = new Font(State_Reminder_1.Font.FontFamily, State_Reminder_1.Font.Size, FontStyle.Regular | FontStyle.Underline);
            State_Reminder_3.TextAlign = ContentAlignment.MiddleCenter;
            State_Reminder_3.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            State_Reminder_3.ForeColor = Color.Blue;
            State_Reminder_3.Name = "State_Reminder_3";
            State_Reminder_3.Dock = DockStyle.Fill;
            State_Reminder_3.IsLink = true;
            State_Reminder_3.Width = 150;
            State_Reminder_3.Visible = false;

            ToolStripStatusLabel State_Reminder_4 = new System.Windows.Forms.ToolStripStatusLabel();
            State_Reminder_4.Font = new Font(State_Reminder_1.Font.FontFamily, State_Reminder_1.Font.Size, FontStyle.Regular | FontStyle.Underline);
            State_Reminder_4.TextAlign = ContentAlignment.MiddleCenter;
            State_Reminder_4.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            State_Reminder_4.LinkColor = Color.Red;
            State_Reminder_4.Name = "State_Reminder_4";
            State_Reminder_4.Dock = DockStyle.Fill;
            State_Reminder_4.IsLink = true;
            State_Reminder_4.Width = 150;
            State_Reminder_4.Visible = false;


            State_5 = new System.Windows.Forms.ToolStripStatusLabel("正在获取更新……", Resources.ajax_loader);
            State_5.TextAlign = ContentAlignment.MiddleLeft;
            State_5.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            State_5.Name = "State_5";
            State_5.Dock = DockStyle.Fill;
            State_5.Width = 250;
            State_5.Visible = false;

            this.StatusBar.Items.AddRange(new ToolStripItem[] {
                                            State_UserName,
                                            State_Project,
                                            State_Service,
                                            ScrollingTextHost,
                                            new ToolStripSeparator(),
                                            State_Reminder_1,
                                            State_Reminder_2,
                                            State_Reminder_3,
                                            State_Reminder_4,
                                            State_5});

            State_UserName.Text = "当前用户:" + Yqun.Common.ContextCache.ApplicationContext.Current.UserName;
            State_Project.Text = Yqun.Common.ContextCache.ApplicationContext.Current.InProject.Description;
            State_Service.Text = (Yqun.Common.ContextCache.ApplicationContext.Current.ISLocalService ? "本地数据服务" : "网络数据服务");
            StatusBar.ResumeLayout(true);

            if (!Cache.CustomCache.ContainsKey(SystemString.连接状态))
                Cache.CustomCache.Add(SystemString.连接状态, State_Service);
            if (!Cache.CustomCache.ContainsKey(SystemString.龄期提醒))
                Cache.CustomCache.Add(SystemString.龄期提醒, State_Reminder_1);
            if (!Cache.CustomCache.ContainsKey(SystemString.监理平行频率提醒))
                Cache.CustomCache.Add(SystemString.监理平行频率提醒, State_Reminder_2);
            if (!Cache.CustomCache.ContainsKey(SystemString.报告不合格提醒))
                Cache.CustomCache.Add(SystemString.报告不合格提醒, State_Reminder_3);
            if (!Cache.CustomCache.ContainsKey(SystemString.审批修改))
                Cache.CustomCache.Add(SystemString.审批修改, State_Reminder_4);
        }

        void scrollingText_TextClicked(object sender, EventArgs args)
        {
            Process.Start("http://www.kingrocket.com");
        }

        #endregion 增加状态栏

        #region 主菜单

        ToolStripMenuItem MenuItemSystem; //系统
        ToolStripMenuItem MenuItemWindow; //窗口
        ToolStripMenuItem MenuItemHelp; //帮助
        ToolStripMenuItem SubMenuItem; //菜单项

        public void LoadMainMenu(MenuStrip MenuStrip)
        {
            MenuItemSystem = new ToolStripMenuItem();
            MenuItemSystem.Text = "系统(&S)";
            MenuItemSystem.Tag = "@System";
            MenuStrip.Items.Add(MenuItemSystem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Name = "UpdatePassword";
            SubMenuItem.Tag = "@UpdatePassword";
            SubMenuItem.Click += new EventHandler(ToolStripMenu_Click);
            SubMenuItem.Text = "修改密码";
            MenuItemSystem.DropDownItems.Add(SubMenuItem);

            ToolStripMenuItem SubMenuItem2 = new ToolStripMenuItem();
            SubMenuItem2.Name = "SystemSetting";
            SubMenuItem2.Tag = "@SystemSetting";
            SubMenuItem2.Click += new EventHandler(ToolStripMenu_Click);
            SubMenuItem2.Text = "系统设置";

            ToolStripMenuItem DeviceManageItem = new ToolStripMenuItem();
            DeviceManageItem.Name = "DeviceManage";
            DeviceManageItem.Tag = "@devicemanagement";
            DeviceManageItem.Click += new EventHandler(ToolStripMenu_Click);
            DeviceManageItem.Text = "设备管理";

            if (Yqun.Common.ContextCache.ApplicationContext.Current.UserCode == "-1" ||
                Yqun.Common.ContextCache.ApplicationContext.Current.UserCode == "-2")
            {
                DeviceManageItem.Visible = SubMenuItem2.Visible = true;
            }
            else
            {
                DeviceManageItem.Visible = SubMenuItem2.Visible = false;
            }
            MenuItemSystem.DropDownItems.Add(SubMenuItem2);
            MenuItemSystem.DropDownItems.Add(DeviceManageItem);

            //查看无用采集数据
            ToolStripMenuItem SubMenuItem22 = new ToolStripMenuItem();
            SubMenuItem22.Name = "unuseddata";
            SubMenuItem22.Tag = "@unuseddata";
            SubMenuItem22.Click += new EventHandler(ToolStripMenu_Click);
            SubMenuItem22.Text = "查看采集数据";
            if (Yqun.Common.ContextCache.ApplicationContext.Current.UserCode == "-1" ||
                Yqun.Common.ContextCache.ApplicationContext.Current.UserCode == "-2")
            {
                SubMenuItem22.Visible = true;
            }
            else
            {
                SubMenuItem22.Visible = false;
            }
            MenuItemSystem.DropDownItems.Add(SubMenuItem22);

            //试验室编码查询
            ToolStripMenuItem SubMenuItem3 = new ToolStripMenuItem();
            SubMenuItem3.Name = "TestRoomCodeView";
            SubMenuItem3.Tag = "@TestRoomCodeView";
            SubMenuItem3.Click += new EventHandler(ToolStripMenu_Click);
            SubMenuItem3.Text = "试验室编码";
            if (Yqun.Common.ContextCache.ApplicationContext.Current.UserCode == "-1" ||
                Yqun.Common.ContextCache.ApplicationContext.Current.UserCode == "-2")
            {
                SubMenuItem3.Visible = true;
            }
            else
            {
                SubMenuItem3.Visible = false;
            }
            MenuItemSystem.DropDownItems.Add(SubMenuItem3);

            //修改龄期
            ToolStripMenuItem SubMenuItem4 = new ToolStripMenuItem();
            SubMenuItem4.Name = "ResetStadium";
            SubMenuItem4.Tag = "@ResetStadium";
            SubMenuItem4.Click += new EventHandler(ToolStripMenu_Click);
            SubMenuItem4.Text = "重新设置龄期";
            if (Yqun.Common.ContextCache.ApplicationContext.Current.UserCode == "-1" ||
                Yqun.Common.ContextCache.ApplicationContext.Current.UserCode == "-2")
            {
                SubMenuItem4.Visible = true;
            }
            else
            {
                SubMenuItem4.Visible = false;
            }
            MenuItemSystem.DropDownItems.Add(SubMenuItem4);

            MenuItemWindow = new ToolStripMenuItem();
            MenuItemWindow.Name = "MenuItemWindow";
            MenuItemWindow.Tag = "@MenuItemWindow";
            MenuItemWindow.Text = "窗口(&W)";
            MenuStrip.Items.Add(MenuItemWindow);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Name = "MenuItemNavBarExplorer";
            SubMenuItem.Tag = "@MenuItemNavBarExplorer";
            SubMenuItem.ShortcutKeys = Keys.ControlKey & Keys.N;
            SubMenuItem.ShortcutKeyDisplayString = "Ctrl+N";
            SubMenuItem.Click += new EventHandler(ToolStripMenu_Click);
            SubMenuItem.Text = "导航窗口";
            MenuItemWindow.DropDownItems.Add(SubMenuItem);

            MenuItemHelp = new ToolStripMenuItem();
            MenuItemHelp.Name = "MenuItemHelp";
            MenuItemHelp.Tag = "@MenuItemHelp";
            MenuItemHelp.Text = "帮助(&H)";
            MenuStrip.Items.Add(MenuItemHelp);

            MenuItemHelp.DropDownItems.Clear();
            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Name = "MenuItemHandbook";
            SubMenuItem.Tag = "@MenuItemHandbook";
            SubMenuItem.Click += new EventHandler(ToolStripMenu_Click);
            SubMenuItem.Text = "用户手册";
            SubMenuItem.ShortcutKeyDisplayString = "F1";
            MenuItemHelp.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Name = "MenuItemAbout";
            SubMenuItem.Tag = "@MenuItemAbout";
            SubMenuItem.Click += new EventHandler(ToolStripMenu_Click);
            SubMenuItem.Text = "关于 " + Application.ProductName;
            MenuItemHelp.DropDownItems.Add(SubMenuItem);
        }

        #endregion 主菜单

        #region 菜单项响应

        private void ToolStripMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem MenuItem = sender as ToolStripMenuItem;
            if (MenuItem.Tag != null)
            {
                string ItemTag = MenuItem.Tag.ToString();
                switch (ItemTag.ToLower())
                {
                    case "@menuitemnavbarexplorer":
                        ShowNavBarExplorer(); //显示功能目录
                        break;
                    case "@menuitemhandbook":
                        ShowHelpFile(); //显示帮助主题
                        break;
                    case "@menuitemabout":
                        ShowAboutWindow(); //显示关于
                        break;
                    case "@menuitemexit": //关闭程序
                        this.Close();
                        break;
                    case "@updatepassword": //修改密码
                        UpdatePassword();
                        break;
                    case "@systemsetting"://系统设置
                        SystemSetting();
                        break;
                    case "@devicemanagement"://设备管理
                        DeviceManage();
                        break;
                    case "@testroomcodeview"://试验室编码查询
                        ViewTestRoomCode();
                        break;
                    case "@unuseddata"://查看无用采集数据
                        ViewUnusedTestData();
                        break;
                    case "@resetstadium"://重新设置龄期
                        ResetStadium();
                        break;
                }
            }
        }

        private void DeviceManage()
        {
            var form = new DeviceManagement();
            form.Show();
        }

        private void ResetStadium()
        {
            ResetStadiumDialog rsd = new ResetStadiumDialog();
            rsd.ShowInTaskbar = true;
            rsd.Show();
        }

        private void ViewUnusedTestData()
        {
            UnusedTestDataView utdv = new UnusedTestDataView();
            utdv.ShowInTaskbar = true;
            utdv.Show();
        }

        private void ViewTestRoomCode()
        {
            TestRoomCodeView trcv = new TestRoomCodeView();
            trcv.ShowDialog();
        }

        /// <summary>
        /// 系统设置
        /// </summary>
        private void SystemSetting()
        {
            SystemSettingDlg appHelp = new SystemSettingDlg();
            appHelp.ShowDialog();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        protected virtual void UpdatePassword()
        {
            AlterPassword AlterForm = new AlterPassword();
            AlterForm.ShowDialog();
        }

        /// <summary>
        /// 显示关于窗口
        /// </summary>
        private void ShowAboutWindow()
        {
            AppHelp appHelp = new AppHelp();
            appHelp.ShowDialog();
        }

        /// <summary>
        /// 显示帮助文件
        /// </summary>
        private void ShowHelpFile()
        {
            HelpFile appHelp = new HelpFile();
            appHelp.ShowDialog();
            //String FileName = Path.Combine(Application.StartupPath, @"help\Help.chm");
            //try
            //{
            //    ProcessStartInfo StartInfo = new ProcessStartInfo(FileName);
            //    StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            //    System.Diagnostics.Process.Start(StartInfo);
            //}
            //catch
            //{
            //    string message = "帮助文件 ‘" + FileName + "’ 没有找到。";
            //    MessageBox.Show(message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        #endregion 菜单项响应

        /// <summary>
        /// 启动更新进程，开始执行循环更新任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Boolean isRemote = !Yqun.Common.ContextCache.ApplicationContext.Current.ISLocalService;
            String fileType = "\"1\"";
            while (isRemote)
            {
                Process[] arrayProcess = Process.GetProcessesByName("JZUpdate");

                if (arrayProcess.Length == 0)
                {
                    ProcessStartInfo Info = new ProcessStartInfo();
                    Info.CreateNoWindow = false;
                    Info.UseShellExecute = true;
                    Info.FileName = Path.Combine(Application.StartupPath, "JZUpdate.exe");
                    Info.Arguments = fileType;
                    //1 管理系统文件+不执行，2 采集系统文件+不执行，3 管理系统数据+不执行，
                    //4 管理系统文件+数据+执行； 
                    //5 采集系统文件+执行
                    //6 管理系统执行
                    //7 采集系统执行
                    //8 管理系统文件+执行

                    Process.Start(Info);
                    this.BringToFront();
                    if (fileType == "\"1\"")
                    {
                        fileType = "\"3\"";
                    }
                    else if (fileType == "\"3\"")
                    {
                        fileType = "\"1\"";
                    }
                }
                Thread.Sleep(1 * 10 * 1000);//等待10秒
            }
        }

        protected override void DefWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_COPYDATA:
                    BizCommon.JZUser32Helper.COPYDATASTRUCT myStr = new BizCommon.JZUser32Helper.COPYDATASTRUCT();
                    Type mytype = myStr.GetType();
                    myStr = (BizCommon.JZUser32Helper.COPYDATASTRUCT)m.GetLParam(mytype);
                    if (myStr.lpData == "1")
                    {
                        UpdateStatusLabel(true);
                    }
                    else
                    {
                        UpdateStatusLabel(false);
                    }
                    break;
                default:
                    base.DefWndProc(ref m);
                    break;
            }

        }

        private void UpdateStatusLabel(Boolean flag)
        {
            if (this.InvokeRequired)
            {
                SetStatusVisibleHander ssvh = new SetStatusVisibleHander(UpdateStatusLabel);
                this.BeginInvoke(ssvh, flag);
            }
            else
            {
                State_5.Visible = flag;
            }
        }

        private const int WM_COPYDATA = 0x004A;
    }
}
