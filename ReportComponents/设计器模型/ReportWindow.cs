using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using FarPoint.Win.Spread;
using ReportCommon;
using Yqun.Bases.ControlBases;
using BizCommon;
using FarPoint.Win;

namespace ReportComponents
{
    public partial class ReportWindow : Form
    {
        //报表数据集
        internal DataSourceForm dataSourceForm;
        //报表设计窗口
        internal ReportDesignForm reportDesignForm;
        //报表目录窗口
        internal ReportCatlogForm reportCatlogForm;
        //扩展信息窗口
        internal ExpandForm expandForm;

        //当前编辑的报表对象
        internal Report report;

        internal String reportID;

        public ReportWindow()
        {
            InitializeComponent();

            dataSourceForm = new DataSourceForm();
            reportDesignForm = new ReportDesignForm();
            reportCatlogForm = new ReportCatlogForm();
            expandForm = new ExpandForm();
        }

        /// <summary>
        /// 加载窗口布局
        /// </summary>
        private void ReportWindow_Load(object sender, EventArgs e)
        {
            LoadTool();
            LoadLayout();
        }

        /// <summary>
        /// 为了解决DockPanel不刷新界面的Bug
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            if (this.Size.IsEmpty) return;
            base.OnSizeChanged(e);
        }

        private void LoadLayout()
        {
            dockPanel1.ActiveAutoHideContent = null;
            dockPanel1.Parent = this;

            dockPanel1.Visible = false;
            dockPanel1.SuspendLayout(true);

            dataSourceForm.Show(dockPanel1, DockState.DockLeft);
            reportCatlogForm.Show(dataSourceForm.DockHandler.PanelPane, DockAlignment.Bottom, 0.5);
            reportDesignForm.Show(dockPanel1, DockState.Document);
            expandForm.Show(reportDesignForm.DockHandler.PanelPane, DockAlignment.Bottom, 0.20);

            dockPanel1.ResumeLayout(true, true);
            dockPanel1.Visible = true;

            if (!dataSourceForm.IsHidden)
                dataSourceForm.Activate();
        }

        private void LoadTool()
        {
            ToolStripButton Button = new ToolStripButton();
            Button.Text = "保存";
            Button.Tag = "@Save";
            Button.DisplayStyle = ToolStripItemDisplayStyle.Text;
            Button.Click += new EventHandler(MenuItem_Click);
            ToolStrip_Main.Items.Add(Button);

            ToolStrip_Main.Items.Add(new ToolStripSeparator());

            ToolStripMenuItem MenuItem = new ToolStripMenuItem();
            MenuItem.Text = "窗口布局";
            MenuItem.Tag = "@Layout";
            MenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            ToolStrip_Main.Items.Add(MenuItem);

            ToolStripMenuItem SubMenuItem = new ToolStripMenuItem();
            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "报表目录";
            SubMenuItem.Tag = "@reportcatalog";
            SubMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            SubMenuItem.Click += new EventHandler(MenuItem_Click);
            MenuItem.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "报表数据集";
            SubMenuItem.Tag = "@datasourceform";
            SubMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            SubMenuItem.Click += new EventHandler(MenuItem_Click);
            MenuItem.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "报表设计器";
            SubMenuItem.Tag = "@reportdesignform";
            SubMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            SubMenuItem.Click += new EventHandler(MenuItem_Click);
            MenuItem.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "扩展属性";
            SubMenuItem.Tag = "@expandform";
            SubMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            SubMenuItem.Click += new EventHandler(MenuItem_Click);
            MenuItem.DropDownItems.Add(SubMenuItem);
        }

        void MenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem Item = sender as ToolStripItem;
            if (Item.Tag is String)
            {
                String TypeFlag = Item.Tag.ToString();
                switch (TypeFlag.ToLower())
                {
                    case "@save":
                        SaveReportData();
                        break;
                    case "@reportcatalog":
                        ShowDockContent(reportCatlogForm);
                        break;
                    case "@datasourceform":
                        ShowDockContent(dataSourceForm);
                        break;
                    case "@reportdesignform":
                        ShowDockContent(reportDesignForm);
                        break;
                    case "@expandform":
                        ShowDockContent(expandForm);
                        break;
                }
            }
        }

        public void OpenReport(String Index)
        {
            //初始化报表信息
            reportID = Index;
            String xml = ReportService.GetReportXML(Index);
            SheetView Sheet = (SheetView)Serializer.LoadObjectXml(typeof(SheetView), xml, "SheetView");
            reportDesignForm.ReportDesignPanel.ActiveSheet = Sheet;
            reportDesignForm.EnableToolStripButton(true);

            //加载数据源信息
            dataSourceForm.ShowTableFields();
            dataSourceForm.EnableToolStripButton(true);
        }

        public void RemoveReport()
        {
            reportDesignForm.ReportDesignPanel.ClearSheets();
            reportDesignForm.EnableToolStripButton(false);
            dataSourceForm.RemoveAll();
            dataSourceForm.EnableToolStripButton(false);
            report = null;
        }

        /// <summary>
        /// 保存报表配置
        /// </summary>
        private void SaveReportData()
        {
            //report.Configuration.SheetStyle = reportDesignForm.ReportDesignPanel.GetActiveSheetXml();
            //DepositoryReportConfiguration.Update(report.Configuration);
            JZReport r = new JZReport()
            {
                ColumnCount = 26,
                RepeatRowCount = 4,
                StartRowIndex = 7,
                StartColumnIndex = 0
            };
            String json = Newtonsoft.Json.JsonConvert.SerializeObject(r);
            ReportService.SaveReport(new Guid(reportID),
                reportDesignForm.ReportDesignPanel.GetActiveSheetXml(), json);
        }

        private void ShowDockContent(DockContent Content)
        {
            if (Content == reportCatlogForm)
            {
                reportCatlogForm.Show(dockPanel1, DockState.DockLeft);
            }
            else if (Content == reportDesignForm)
            {
                reportDesignForm.Show(dockPanel1, DockState.Document);
            }
            else if (Content == dataSourceForm)
            {
                dataSourceForm.Show(reportCatlogForm.DockHandler.PanelPane, DockAlignment.Bottom, 0.5);
            }
            else if (Content == expandForm)
            {
                expandForm.Show(reportDesignForm.DockHandler.PanelPane, DockAlignment.Bottom, 0.20);
            }
        }
    }
}
