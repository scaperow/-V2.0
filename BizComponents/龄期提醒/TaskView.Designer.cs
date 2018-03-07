namespace BizComponents
{
    partial class TaskView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskView));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.spread_stadium = new Yqun.Client.BizUI.MyCell();
            this.spread_stadium_sheet = new FarPoint.Win.Spread.SheetView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.spread_invalide = new Yqun.Client.BizUI.MyCell();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.spread_request = new Yqun.Client.BizUI.MyCell();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.spread_test_overtime = new Yqun.Client.BizUI.MyCell();
            this.OverTimeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.填写原因ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.处理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sheet_test_overtime = new FarPoint.Win.Spread.SheetView();
            this.TabParallel = new System.Windows.Forms.TabPage();
            this.TabWitness = new System.Windows.Forms.TabPage();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.原因分析ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FarInvalidPX = new Yqun.Client.BizUI.MyCell();
            this.SheetInvalidPX = new FarPoint.Win.Spread.SheetView();
            this.FarInvalidJZ = new Yqun.Client.BizUI.MyCell();
            this.SheetInvalidJZ = new FarPoint.Win.Spread.SheetView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spread_stadium)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spread_stadium_sheet)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spread_invalide)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spread_request)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spread_test_overtime)).BeginInit();
            this.OverTimeMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sheet_test_overtime)).BeginInit();
            this.TabParallel.SuspendLayout();
            this.TabWitness.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FarInvalidPX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SheetInvalidPX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FarInvalidJZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SheetInvalidJZ)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.TabParallel);
            this.tabControl1.Controls.Add(this.TabWitness);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1184, 622);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.spread_stadium);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1176, 596);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "今日到期待做试验";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // spread_stadium
            // 
            this.spread_stadium.AccessibleDescription = "FpSpread, SheetInfo, Row 0, Column 0, ";
            this.spread_stadium.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.spread_stadium.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spread_stadium.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.spread_stadium.IsEditing = false;
            this.spread_stadium.Location = new System.Drawing.Point(3, 3);
            this.spread_stadium.Name = "spread_stadium";
            this.spread_stadium.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.spread_stadium.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spread_stadium_sheet});
            this.spread_stadium.Size = new System.Drawing.Size(1170, 590);
            this.spread_stadium.TabIndex = 1;
            this.spread_stadium.TabStripInsertTab = false;
            this.spread_stadium.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            this.spread_stadium.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.spread_stadium.Watermark = null;
            this.spread_stadium.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FpSpread_MouseDown);
            // 
            // spread_stadium_sheet
            // 
            this.spread_stadium_sheet.Reset();
            this.spread_stadium_sheet.SheetName = "SheetInfo";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.spread_stadium_sheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.spread_stadium_sheet.ColumnCount = 0;
            this.spread_stadium_sheet.RowCount = 1;
            this.spread_stadium_sheet.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.spread_stadium_sheet.PrintInfo.PaperSize = ((System.Drawing.Printing.PaperSize)(resources.GetObject("resource.PaperSize2")));
            this.spread_stadium_sheet.RowHeader.Visible = false;
            this.spread_stadium_sheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.spread_stadium.SetActiveViewport(0, 0, 1);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.spread_invalide);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1176, 596);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "待处理不合格报告";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // spread_invalide
            // 
            this.spread_invalide.AccessibleDescription = "FpSpread, SheetInfo, Row 0, Column 0, ";
            this.spread_invalide.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.spread_invalide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spread_invalide.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.spread_invalide.IsEditing = false;
            this.spread_invalide.Location = new System.Drawing.Point(3, 3);
            this.spread_invalide.Name = "spread_invalide";
            this.spread_invalide.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.spread_invalide.Size = new System.Drawing.Size(1170, 590);
            this.spread_invalide.TabIndex = 2;
            this.spread_invalide.TabStripInsertTab = false;
            this.spread_invalide.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            this.spread_invalide.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.spread_invalide.Watermark = null;
            this.spread_invalide.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.spread_invalide_CellDoubleClick);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.spread_request);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1176, 596);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "待审批用户申请";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // spread_request
            // 
            this.spread_request.AccessibleDescription = "FpSpread, SheetInfo, Row 0, Column 0, ";
            this.spread_request.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.spread_request.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spread_request.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.spread_request.IsEditing = false;
            this.spread_request.Location = new System.Drawing.Point(3, 3);
            this.spread_request.Name = "spread_request";
            this.spread_request.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.spread_request.Size = new System.Drawing.Size(1170, 590);
            this.spread_request.TabIndex = 3;
            this.spread_request.TabStripInsertTab = false;
            this.spread_request.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            this.spread_request.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.spread_request.Watermark = null;
            this.spread_request.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.spread_request_CellDoubleClick);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.spread_test_overtime);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1176, 596);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "待审批过期试验";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // spread_test_overtime
            // 
            this.spread_test_overtime.AccessibleDescription = "FpSpread, SheetInfo, Row 0, Column 0, ";
            this.spread_test_overtime.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.spread_test_overtime.ContextMenuStrip = this.OverTimeMenu;
            this.spread_test_overtime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spread_test_overtime.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.spread_test_overtime.IsEditing = false;
            this.spread_test_overtime.Location = new System.Drawing.Point(3, 3);
            this.spread_test_overtime.Name = "spread_test_overtime";
            this.spread_test_overtime.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.spread_test_overtime.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.sheet_test_overtime});
            this.spread_test_overtime.Size = new System.Drawing.Size(1170, 590);
            this.spread_test_overtime.TabIndex = 4;
            this.spread_test_overtime.TabStripInsertTab = false;
            this.spread_test_overtime.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            this.spread_test_overtime.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.spread_test_overtime.Watermark = null;
            // 
            // OverTimeMenu
            // 
            this.OverTimeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.填写原因ToolStripMenuItem,
            this.处理ToolStripMenuItem,
            this.删除ToolStripMenuItem});
            this.OverTimeMenu.Name = "contextMenuStrip2";
            this.OverTimeMenu.Size = new System.Drawing.Size(125, 70);
            this.OverTimeMenu.Opening += new System.ComponentModel.CancelEventHandler(this.OverTimeMenu_Opening);
            // 
            // 填写原因ToolStripMenuItem
            // 
            this.填写原因ToolStripMenuItem.Name = "填写原因ToolStripMenuItem";
            this.填写原因ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.填写原因ToolStripMenuItem.Text = "填写原因";
            this.填写原因ToolStripMenuItem.Click += new System.EventHandler(this.填写原因ToolStripMenuItem_Click);
            // 
            // 处理ToolStripMenuItem
            // 
            this.处理ToolStripMenuItem.Name = "处理ToolStripMenuItem";
            this.处理ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.处理ToolStripMenuItem.Text = "处理";
            this.处理ToolStripMenuItem.Click += new System.EventHandler(this.处理ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // sheet_test_overtime
            // 
            this.sheet_test_overtime.Reset();
            this.sheet_test_overtime.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.sheet_test_overtime.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.sheet_test_overtime.PrintInfo.PaperSize = ((System.Drawing.Printing.PaperSize)(resources.GetObject("resource.PaperSize1")));
            this.sheet_test_overtime.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // TabParallel
            // 
            this.TabParallel.Controls.Add(this.FarInvalidPX);
            this.TabParallel.Location = new System.Drawing.Point(4, 22);
            this.TabParallel.Name = "TabParallel";
            this.TabParallel.Padding = new System.Windows.Forms.Padding(3);
            this.TabParallel.Size = new System.Drawing.Size(1176, 596);
            this.TabParallel.TabIndex = 4;
            this.TabParallel.Text = "平行率不满足";
            this.TabParallel.UseVisualStyleBackColor = true;
            // 
            // TabWitness
            // 
            this.TabWitness.Controls.Add(this.FarInvalidJZ);
            this.TabWitness.Location = new System.Drawing.Point(4, 22);
            this.TabWitness.Name = "TabWitness";
            this.TabWitness.Padding = new System.Windows.Forms.Padding(3);
            this.TabWitness.Size = new System.Drawing.Size(1176, 596);
            this.TabWitness.TabIndex = 5;
            this.TabWitness.Text = "见证率不满足";
            this.TabWitness.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.原因分析ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 26);
            // 
            // 原因分析ToolStripMenuItem
            // 
            this.原因分析ToolStripMenuItem.Name = "原因分析ToolStripMenuItem";
            this.原因分析ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.原因分析ToolStripMenuItem.Text = "不合格数据处理";
            this.原因分析ToolStripMenuItem.Click += new System.EventHandler(this.原因分析ToolStripMenuItem_Click);
            // 
            // FarInvalidPX
            // 
            this.FarInvalidPX.AccessibleDescription = "FarInvalidPX, Sheet1, Row 0, Column 0, ";
            this.FarInvalidPX.BackColor = System.Drawing.Color.Transparent;
            this.FarInvalidPX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FarInvalidPX.IsEditing = false;
            this.FarInvalidPX.Location = new System.Drawing.Point(3, 3);
            this.FarInvalidPX.Name = "FarInvalidPX";
            this.FarInvalidPX.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.FarInvalidPX.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.SheetInvalidPX});
            this.FarInvalidPX.Size = new System.Drawing.Size(1170, 590);
            this.FarInvalidPX.TabIndex = 0;
            this.FarInvalidPX.Watermark = null;
            // 
            // SheetInvalidPX
            // 
            this.SheetInvalidPX.Reset();
            this.SheetInvalidPX.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.SheetInvalidPX.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.SheetInvalidPX.ColumnCount = 9;
            this.SheetInvalidPX.ColumnHeader.Cells.Get(0, 0).Value = "标段";
            this.SheetInvalidPX.ColumnHeader.Cells.Get(0, 1).Value = "监理单位";
            this.SheetInvalidPX.ColumnHeader.Cells.Get(0, 2).Value = "施工单位";
            this.SheetInvalidPX.ColumnHeader.Cells.Get(0, 3).Value = "施工单位试验室";
            this.SheetInvalidPX.ColumnHeader.Cells.Get(0, 4).Value = "试验名称";
            this.SheetInvalidPX.ColumnHeader.Cells.Get(0, 5).Value = "标准平行率（%）";
            this.SheetInvalidPX.ColumnHeader.Cells.Get(0, 6).Value = "施工单位资料总数";
            this.SheetInvalidPX.ColumnHeader.Cells.Get(0, 7).Value = "平行次数";
            this.SheetInvalidPX.ColumnHeader.Cells.Get(0, 8).Value = "平行频率";
            this.SheetInvalidPX.Columns.Get(0).Label = "标段";
            this.SheetInvalidPX.Columns.Get(0).Tag = "segment";
            this.SheetInvalidPX.Columns.Get(0).Width = 104F;
            this.SheetInvalidPX.Columns.Get(1).Label = "监理单位";
            this.SheetInvalidPX.Columns.Get(1).Tag = "jl";
            this.SheetInvalidPX.Columns.Get(1).Width = 202F;
            this.SheetInvalidPX.Columns.Get(2).Label = "施工单位";
            this.SheetInvalidPX.Columns.Get(2).Tag = "sg";
            this.SheetInvalidPX.Columns.Get(2).Width = 122F;
            this.SheetInvalidPX.Columns.Get(3).Label = "施工单位试验室";
            this.SheetInvalidPX.Columns.Get(3).Tag = "testroom";
            this.SheetInvalidPX.Columns.Get(3).Width = 115F;
            this.SheetInvalidPX.Columns.Get(4).Label = "试验名称";
            this.SheetInvalidPX.Columns.Get(4).Tag = "modelName";
            this.SheetInvalidPX.Columns.Get(4).Width = 116F;
            this.SheetInvalidPX.Columns.Get(5).Label = "标准平行率（%）";
            this.SheetInvalidPX.Columns.Get(5).Tag = "condition";
            this.SheetInvalidPX.Columns.Get(5).Width = 121F;
            this.SheetInvalidPX.Columns.Get(6).Label = "施工单位资料总数";
            this.SheetInvalidPX.Columns.Get(6).Tag = "zjCount";
            this.SheetInvalidPX.Columns.Get(6).Width = 117F;
            this.SheetInvalidPX.Columns.Get(7).Label = "平行次数";
            this.SheetInvalidPX.Columns.Get(7).Tag = "pxCount";
            this.SheetInvalidPX.Columns.Get(8).Label = "平行频率";
            this.SheetInvalidPX.Columns.Get(8).Tag = "frequency";
            this.SheetInvalidPX.DefaultStyle.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.SheetInvalidPX.DefaultStyle.Parent = "DataAreaDefault";
            this.SheetInvalidPX.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.SheetInvalidPX.PrintInfo.PaperSize = ((System.Drawing.Printing.PaperSize)(resources.GetObject("resource.PaperSize")));
            this.SheetInvalidPX.PrintInfo.PdfFileName = "0";
            this.SheetInvalidPX.RowHeader.Columns.Default.Resizable = false;
            this.SheetInvalidPX.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FarInvalidJZ
            // 
            this.FarInvalidJZ.AccessibleDescription = "";
            this.FarInvalidJZ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FarInvalidJZ.IsEditing = false;
            this.FarInvalidJZ.Location = new System.Drawing.Point(3, 3);
            this.FarInvalidJZ.Name = "FarInvalidJZ";
            this.FarInvalidJZ.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.SheetInvalidJZ});
            this.FarInvalidJZ.Size = new System.Drawing.Size(1170, 590);
            this.FarInvalidJZ.TabIndex = 0;
            this.FarInvalidJZ.Watermark = null;
            // 
            // SheetInvalidJZ
            // 
            this.SheetInvalidJZ.Reset();
            this.SheetInvalidJZ.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.SheetInvalidJZ.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.SheetInvalidJZ.ColumnCount = 9;
            this.SheetInvalidJZ.ColumnHeader.Cells.Get(0, 0).Value = "标段";
            this.SheetInvalidJZ.ColumnHeader.Cells.Get(0, 1).Value = "监理单位";
            this.SheetInvalidJZ.ColumnHeader.Cells.Get(0, 2).Value = "施工单位";
            this.SheetInvalidJZ.ColumnHeader.Cells.Get(0, 3).Value = "施工单位试验室";
            this.SheetInvalidJZ.ColumnHeader.Cells.Get(0, 4).Value = "试验名称";
            this.SheetInvalidJZ.ColumnHeader.Cells.Get(0, 5).Value = "标准见证频率（%）";
            this.SheetInvalidJZ.ColumnHeader.Cells.Get(0, 6).Value = "施工单位资料总数";
            this.SheetInvalidJZ.ColumnHeader.Cells.Get(0, 7).Value = "见证次数";
            this.SheetInvalidJZ.ColumnHeader.Cells.Get(0, 8).Value = "见证频率（%）";
            this.SheetInvalidJZ.Columns.Get(0).Label = "标段";
            this.SheetInvalidJZ.Columns.Get(0).Tag = "segment";
            this.SheetInvalidJZ.Columns.Get(0).Width = 90F;
            this.SheetInvalidJZ.Columns.Get(1).Label = "监理单位";
            this.SheetInvalidJZ.Columns.Get(1).Tag = "jl";
            this.SheetInvalidJZ.Columns.Get(1).Width = 230F;
            this.SheetInvalidJZ.Columns.Get(2).Label = "施工单位";
            this.SheetInvalidJZ.Columns.Get(2).Tag = "sg";
            this.SheetInvalidJZ.Columns.Get(2).Width = 107F;
            this.SheetInvalidJZ.Columns.Get(3).Label = "施工单位试验室";
            this.SheetInvalidJZ.Columns.Get(3).Tag = "testroom";
            this.SheetInvalidJZ.Columns.Get(3).Width = 122F;
            this.SheetInvalidJZ.Columns.Get(4).Label = "试验名称";
            this.SheetInvalidJZ.Columns.Get(4).Tag = "modelName";
            this.SheetInvalidJZ.Columns.Get(4).Width = 114F;
            this.SheetInvalidJZ.Columns.Get(5).Label = "标准见证频率（%）";
            this.SheetInvalidJZ.Columns.Get(5).Tag = "condition";
            this.SheetInvalidJZ.Columns.Get(5).Width = 120F;
            this.SheetInvalidJZ.Columns.Get(6).Label = "施工单位资料总数";
            this.SheetInvalidJZ.Columns.Get(6).Tag = "zjCount";
            this.SheetInvalidJZ.Columns.Get(6).Width = 112F;
            this.SheetInvalidJZ.Columns.Get(7).Label = "见证次数";
            this.SheetInvalidJZ.Columns.Get(7).Tag = "pxCount";
            this.SheetInvalidJZ.Columns.Get(7).Width = 63F;
            this.SheetInvalidJZ.Columns.Get(8).Label = "见证频率（%）";
            this.SheetInvalidJZ.Columns.Get(8).Tag = "frequency";
            this.SheetInvalidJZ.Columns.Get(8).Width = 99F;
            this.SheetInvalidJZ.DefaultStyle.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.SheetInvalidJZ.DefaultStyle.Parent = "DataAreaDefault";
            this.SheetInvalidJZ.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.SheetInvalidJZ.PrintInfo.PaperSize = ((System.Drawing.Printing.PaperSize)(resources.GetObject("resource.PaperSize1")));
            this.SheetInvalidJZ.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // TaskView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 622);
            this.Controls.Add(this.tabControl1);
            this.Name = "TaskView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "待做事项列表";
            this.Load += new System.EventHandler(this.TaskView_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spread_stadium)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spread_stadium_sheet)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spread_invalide)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spread_request)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spread_test_overtime)).EndInit();
            this.OverTimeMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sheet_test_overtime)).EndInit();
            this.TabParallel.ResumeLayout(false);
            this.TabWitness.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FarInvalidPX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SheetInvalidPX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FarInvalidJZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SheetInvalidJZ)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        public Yqun.Client.BizUI.MyCell spread_stadium;
        public FarPoint.Win.Spread.SheetView spread_stadium_sheet;
        public Yqun.Client.BizUI.MyCell spread_invalide;
        public Yqun.Client.BizUI.MyCell spread_request;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 原因分析ToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage4;
        public Yqun.Client.BizUI.MyCell spread_test_overtime;
        private System.Windows.Forms.ContextMenuStrip OverTimeMenu;
        private System.Windows.Forms.ToolStripMenuItem 填写原因ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 处理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private FarPoint.Win.Spread.SheetView sheet_test_overtime;
        private System.Windows.Forms.TabPage TabParallel;
        private System.Windows.Forms.TabPage TabWitness;
        private Yqun.Client.BizUI.MyCell FarInvalidPX;
        private FarPoint.Win.Spread.SheetView SheetInvalidPX;
        private Yqun.Client.BizUI.MyCell FarInvalidJZ;
        private FarPoint.Win.Spread.SheetView SheetInvalidJZ;
    }
}