using Yqun.Client.BizUI;

namespace BizModules
{
    partial class DataViewControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataViewControl));
            this.myCell1 = new Yqun.Client.BizUI.MyCell();
            this.myCell1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.GeneratePXRelationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewPingxingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectDataIDMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.CopyDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.ExportDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BatchPrintMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.TemperatureListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gBox_Container = new System.Windows.Forms.GroupBox();
            this.NavigatorBar = new System.Windows.Forms.ToolStrip();
            this.FirstButton = new System.Windows.Forms.ToolStripButton();
            this.PrevButton = new System.Windows.Forms.ToolStripButton();
            this.TextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.StripLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.NextButton = new System.Windows.Forms.ToolStripButton();
            this.LastButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TotalCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtPageCount = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AppendFilterMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.DeletePXRelationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.myCell1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myCell1_Sheet1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.gBox_Container.SuspendLayout();
            this.NavigatorBar.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // myCell1
            // 
            this.myCell1.AccessibleDescription = "myCell1, Sheet1, Row 0, Column 0, ";
            this.myCell1.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.myCell1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myCell1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.myCell1.IsEditing = false;
            this.myCell1.Location = new System.Drawing.Point(3, 17);
            this.myCell1.Name = "myCell1";
            this.myCell1.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.myCell1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.myCell1_Sheet1});
            this.myCell1.Size = new System.Drawing.Size(670, 440);
            this.myCell1.TabIndex = 0;
            this.myCell1.TabStripInsertTab = false;
            this.myCell1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.myCell1.Watermark = null;
            // 
            // myCell1_Sheet1
            // 
            this.myCell1_Sheet1.Reset();
            this.myCell1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.myCell1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.myCell1_Sheet1.ColumnCount = 0;
            this.myCell1_Sheet1.RowCount = 0;
            this.myCell1_Sheet1.ColumnHeader.AutoText = FarPoint.Win.Spread.HeaderAutoText.Blank;
            this.myCell1_Sheet1.GroupBarBackColor = System.Drawing.Color.Moccasin;
            this.myCell1_Sheet1.GroupBarHeight = 32;
            this.myCell1_Sheet1.GroupBarVisible = true;
            this.myCell1_Sheet1.GroupVerticalIndent = 0;
            this.myCell1_Sheet1.PrintInfo.PaperSize = ((System.Drawing.Printing.PaperSize)(resources.GetObject("resource.PaperSize")));
            this.myCell1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.myCell1.SetActiveViewport(0, 1, 1);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeletePXRelationMenuItem,
            this.GeneratePXRelationMenuItem,
            this.NewPingxingMenuItem,
            this.NewMenuItem,
            this.EditMenuItem,
            this.DeleteMenuItem,
            this.SelectDataIDMenuItem,
            this.toolStripMenuItem1,
            this.CopyDataMenuItem,
            this.PasteDataMenuItem,
            this.toolStripMenuItem3,
            this.ExportDataMenuItem,
            this.BatchPrintMenuItem,
            this.toolStripMenuItem2,
            this.TemperatureListToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(173, 308);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip_Opening);
            // 
            // GeneratePXRelationMenuItem
            // 
            this.GeneratePXRelationMenuItem.Name = "GeneratePXRelationMenuItem";
            this.GeneratePXRelationMenuItem.Size = new System.Drawing.Size(172, 22);
            this.GeneratePXRelationMenuItem.Tag = "@module_generatepxrelation";
            this.GeneratePXRelationMenuItem.Text = "生成平行对应关系";
            this.GeneratePXRelationMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // NewPingxingMenuItem
            // 
            this.NewPingxingMenuItem.Name = "NewPingxingMenuItem";
            this.NewPingxingMenuItem.Size = new System.Drawing.Size(172, 22);
            this.NewPingxingMenuItem.Tag = "@module_pingxingnewdata";
            this.NewPingxingMenuItem.Text = "生成平行资料";
            this.NewPingxingMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // NewMenuItem
            // 
            this.NewMenuItem.Name = "NewMenuItem";
            this.NewMenuItem.Size = new System.Drawing.Size(172, 22);
            this.NewMenuItem.Tag = "@module_newdata";
            this.NewMenuItem.Text = "新建资料";
            this.NewMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // EditMenuItem
            // 
            this.EditMenuItem.Name = "EditMenuItem";
            this.EditMenuItem.Size = new System.Drawing.Size(172, 22);
            this.EditMenuItem.Tag = "@data_editdata";
            this.EditMenuItem.Text = "编辑资料";
            this.EditMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // DeleteMenuItem
            // 
            this.DeleteMenuItem.Name = "DeleteMenuItem";
            this.DeleteMenuItem.Size = new System.Drawing.Size(172, 22);
            this.DeleteMenuItem.Tag = "@data_deledata";
            this.DeleteMenuItem.Text = "删除资料";
            this.DeleteMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // SelectDataIDMenuItem
            // 
            this.SelectDataIDMenuItem.Name = "SelectDataIDMenuItem";
            this.SelectDataIDMenuItem.Size = new System.Drawing.Size(172, 22);
            this.SelectDataIDMenuItem.Tag = "@data_selectdataid";
            this.SelectDataIDMenuItem.Text = "查看DataID";
            this.SelectDataIDMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(169, 6);
            // 
            // CopyDataMenuItem
            // 
            this.CopyDataMenuItem.Name = "CopyDataMenuItem";
            this.CopyDataMenuItem.Size = new System.Drawing.Size(172, 22);
            this.CopyDataMenuItem.Tag = "@data_copydata";
            this.CopyDataMenuItem.Text = "复制资料";
            this.CopyDataMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // PasteDataMenuItem
            // 
            this.PasteDataMenuItem.Name = "PasteDataMenuItem";
            this.PasteDataMenuItem.Size = new System.Drawing.Size(172, 22);
            this.PasteDataMenuItem.Tag = "@data_pastdata";
            this.PasteDataMenuItem.Text = "粘贴资料";
            this.PasteDataMenuItem.Visible = false;
            this.PasteDataMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(169, 6);
            // 
            // ExportDataMenuItem
            // 
            this.ExportDataMenuItem.Name = "ExportDataMenuItem";
            this.ExportDataMenuItem.Size = new System.Drawing.Size(172, 22);
            this.ExportDataMenuItem.Tag = "@data_exportdata";
            this.ExportDataMenuItem.Text = "导出台账";
            this.ExportDataMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // BatchPrintMenuItem
            // 
            this.BatchPrintMenuItem.Name = "BatchPrintMenuItem";
            this.BatchPrintMenuItem.Size = new System.Drawing.Size(172, 22);
            this.BatchPrintMenuItem.Tag = "@data_batchprint";
            this.BatchPrintMenuItem.Text = "批量打印";
            this.BatchPrintMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(169, 6);
            this.toolStripMenuItem2.Visible = false;
            // 
            // TemperatureListToolStripMenuItem
            // 
            this.TemperatureListToolStripMenuItem.Name = "TemperatureListToolStripMenuItem";
            this.TemperatureListToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.TemperatureListToolStripMenuItem.Tag = "@temperaturelist";
            this.TemperatureListToolStripMenuItem.Text = "查看温度记录";
            this.TemperatureListToolStripMenuItem.Visible = false;
            this.TemperatureListToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // gBox_Container
            // 
            this.gBox_Container.Controls.Add(this.myCell1);
            this.gBox_Container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gBox_Container.Location = new System.Drawing.Point(0, 2);
            this.gBox_Container.Name = "gBox_Container";
            this.gBox_Container.Size = new System.Drawing.Size(676, 460);
            this.gBox_Container.TabIndex = 1;
            this.gBox_Container.TabStop = false;
            this.gBox_Container.Text = "显示资料";
            // 
            // NavigatorBar
            // 
            this.NavigatorBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.NavigatorBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.NavigatorBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FirstButton,
            this.PrevButton,
            this.TextBox,
            this.toolStripSeparator9,
            this.StripLabel,
            this.toolStripSeparator8,
            this.NextButton,
            this.LastButton,
            this.toolStripSeparator1,
            this.TotalCount,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.txtPageCount,
            this.toolStripLabel2});
            this.NavigatorBar.Location = new System.Drawing.Point(0, 462);
            this.NavigatorBar.Name = "NavigatorBar";
            this.NavigatorBar.Size = new System.Drawing.Size(676, 25);
            this.NavigatorBar.TabIndex = 2;
            this.NavigatorBar.Text = "导航栏";
            // 
            // FirstButton
            // 
            this.FirstButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FirstButton.Image = ((System.Drawing.Image)(resources.GetObject("FirstButton.Image")));
            this.FirstButton.Name = "FirstButton";
            this.FirstButton.RightToLeftAutoMirrorImage = true;
            this.FirstButton.Size = new System.Drawing.Size(23, 22);
            this.FirstButton.Text = "移到第一页";
            this.FirstButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // PrevButton
            // 
            this.PrevButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PrevButton.Image = ((System.Drawing.Image)(resources.GetObject("PrevButton.Image")));
            this.PrevButton.Name = "PrevButton";
            this.PrevButton.RightToLeftAutoMirrorImage = true;
            this.PrevButton.Size = new System.Drawing.Size(23, 22);
            this.PrevButton.Text = "移到上一页";
            this.PrevButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // TextBox
            // 
            this.TextBox.AccessibleName = "页";
            this.TextBox.AutoSize = false;
            this.TextBox.Name = "TextBox";
            this.TextBox.Size = new System.Drawing.Size(50, 20);
            this.TextBox.Text = "0";
            this.TextBox.ToolTipText = "当前页";
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // StripLabel
            // 
            this.StripLabel.Name = "StripLabel";
            this.StripLabel.Size = new System.Drawing.Size(32, 22);
            this.StripLabel.Text = "/ {0}";
            this.StripLabel.ToolTipText = "总页数";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // NextButton
            // 
            this.NextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NextButton.Image = ((System.Drawing.Image)(resources.GetObject("NextButton.Image")));
            this.NextButton.Name = "NextButton";
            this.NextButton.RightToLeftAutoMirrorImage = true;
            this.NextButton.Size = new System.Drawing.Size(23, 22);
            this.NextButton.Text = "移到下一页";
            this.NextButton.ToolTipText = "移到下一页";
            this.NextButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // LastButton
            // 
            this.LastButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LastButton.Image = ((System.Drawing.Image)(resources.GetObject("LastButton.Image")));
            this.LastButton.Name = "LastButton";
            this.LastButton.RightToLeftAutoMirrorImage = true;
            this.LastButton.Size = new System.Drawing.Size(23, 22);
            this.LastButton.Text = "移到最后一页";
            this.LastButton.ToolTipText = "移到最后一页";
            this.LastButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // TotalCount
            // 
            this.TotalCount.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TotalCount.Name = "TotalCount";
            this.TotalCount.Size = new System.Drawing.Size(79, 22);
            this.TotalCount.Text = "共 {0} 条记录";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(56, 22);
            this.toolStripLabel1.Text = "每页显示";
            // 
            // txtPageCount
            // 
            this.txtPageCount.Name = "txtPageCount";
            this.txtPageCount.Size = new System.Drawing.Size(40, 25);
            this.txtPageCount.Text = "20";
            this.txtPageCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPageCount_KeyPress);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(20, 22);
            this.toolStripLabel2.Text = "条";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AppendFilterMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(149, 26);
            // 
            // AppendFilterMenuItem
            // 
            this.AppendFilterMenuItem.Name = "AppendFilterMenuItem";
            this.AppendFilterMenuItem.Size = new System.Drawing.Size(148, 22);
            this.AppendFilterMenuItem.Text = "添加筛选条件";
            // 
            // DeletePXRelationMenuItem
            // 
            this.DeletePXRelationMenuItem.Name = "DeletePXRelationMenuItem";
            this.DeletePXRelationMenuItem.Size = new System.Drawing.Size(172, 22);
            this.DeletePXRelationMenuItem.Tag = "@module_generatepxrelation";
            this.DeletePXRelationMenuItem.Text = "删除平行对应关系";
            this.DeletePXRelationMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // DataViewControl
            // 
            this.Controls.Add(this.gBox_Container);
            this.Controls.Add(this.NavigatorBar);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "DataViewControl";
            this.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.Size = new System.Drawing.Size(676, 487);
            ((System.ComponentModel.ISupportInitialize)(this.myCell1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myCell1_Sheet1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.gBox_Container.ResumeLayout(false);
            this.NavigatorBar.ResumeLayout(false);
            this.NavigatorBar.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyCell myCell1;
        private FarPoint.Win.Spread.SheetView myCell1_Sheet1;
        private System.Windows.Forms.GroupBox gBox_Container;
        private System.Windows.Forms.ToolStrip NavigatorBar;
        internal System.Windows.Forms.ToolStripButton FirstButton;
        internal System.Windows.Forms.ToolStripButton PrevButton;
        internal System.Windows.Forms.ToolStripTextBox TextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        internal System.Windows.Forms.ToolStripLabel StripLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        internal System.Windows.Forms.ToolStripButton NextButton;
        internal System.Windows.Forms.ToolStripButton LastButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem NewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem CopyDataMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PasteDataMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem ExportDataMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BatchPrintMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem AppendFilterMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStripMenuItem NewPingxingMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel TotalCount;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox txtPageCount;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripMenuItem SelectDataIDMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem TemperatureListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GeneratePXRelationMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeletePXRelationMenuItem;
    }
}
