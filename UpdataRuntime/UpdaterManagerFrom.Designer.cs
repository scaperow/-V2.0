namespace UpdaterManager
{
    partial class UpdaterManagerFrom
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            FarPoint.Win.Spread.CellType.MaskCellType maskCellType1 = new FarPoint.Win.Spread.CellType.MaskCellType();
            FarPoint.Win.Spread.CellType.MaskCellType maskCellType2 = new FarPoint.Win.Spread.CellType.MaskCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdaterManagerFrom));
            this.NewFile_Spread = new FarPoint.Win.Spread.FpSpread();
            this.NewFile_Spread_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.OldFile_Spread = new FarPoint.Win.Spread.FpSpread();
            this.OldFile_Spread_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.BrowseFileButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveButton = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.RefreshButton = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.NewFile_Spread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewFile_Spread_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OldFile_Spread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OldFile_Spread_Sheet1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // NewFile_Spread
            // 
            this.NewFile_Spread.AccessibleDescription = "";
            this.NewFile_Spread.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.NewFile_Spread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NewFile_Spread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.NewFile_Spread.Location = new System.Drawing.Point(3, 42);
            this.NewFile_Spread.Name = "NewFile_Spread";
            this.NewFile_Spread.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.NewFile_Spread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.NewFile_Spread_Sheet1});
            this.NewFile_Spread.Size = new System.Drawing.Size(534, 587);
            this.NewFile_Spread.TabIndex = 2;
            this.NewFile_Spread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // NewFile_Spread_Sheet1
            // 
            this.NewFile_Spread_Sheet1.Reset();
            this.NewFile_Spread_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.NewFile_Spread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.NewFile_Spread_Sheet1.ColumnCount = 3;
            this.NewFile_Spread_Sheet1.RowCount = 0;
            this.NewFile_Spread_Sheet1.ColumnHeader.AutoText = FarPoint.Win.Spread.HeaderAutoText.Blank;
            this.NewFile_Spread_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "文件名称";
            this.NewFile_Spread_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "版本号";
            this.NewFile_Spread_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "编辑";
            this.NewFile_Spread_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.NewFile_Spread_Sheet1.Columns.Get(0).Label = "文件名称";
            this.NewFile_Spread_Sheet1.Columns.Get(0).Locked = true;
            this.NewFile_Spread_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.NewFile_Spread_Sheet1.Columns.Get(0).Width = 329F;
            maskCellType1.Mask = "#.#.####";
            this.NewFile_Spread_Sheet1.Columns.Get(1).CellType = maskCellType1;
            this.NewFile_Spread_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("宋体", 12F);
            this.NewFile_Spread_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.NewFile_Spread_Sheet1.Columns.Get(1).Label = "版本号";
            this.NewFile_Spread_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.NewFile_Spread_Sheet1.Columns.Get(1).Width = 113F;
            this.NewFile_Spread_Sheet1.Columns.Get(2).Label = "编辑";
            this.NewFile_Spread_Sheet1.Columns.Get(2).Width = 75F;
            this.NewFile_Spread_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.NewFile_Spread_Sheet1.RowHeader.Visible = false;
            this.NewFile_Spread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.NewFile_Spread.SetActiveViewport(0, 1, 0);
            // 
            // OldFile_Spread
            // 
            this.OldFile_Spread.AccessibleDescription = "";
            this.OldFile_Spread.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.OldFile_Spread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OldFile_Spread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.OldFile_Spread.Location = new System.Drawing.Point(3, 42);
            this.OldFile_Spread.Name = "OldFile_Spread";
            this.OldFile_Spread.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.OldFile_Spread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.OldFile_Spread_Sheet1});
            this.OldFile_Spread.Size = new System.Drawing.Size(453, 587);
            this.OldFile_Spread.TabIndex = 3;
            this.OldFile_Spread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // OldFile_Spread_Sheet1
            // 
            this.OldFile_Spread_Sheet1.Reset();
            this.OldFile_Spread_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.OldFile_Spread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.OldFile_Spread_Sheet1.ColumnCount = 2;
            this.OldFile_Spread_Sheet1.RowCount = 0;
            this.OldFile_Spread_Sheet1.ColumnHeader.AutoText = FarPoint.Win.Spread.HeaderAutoText.Blank;
            this.OldFile_Spread_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "文件名称";
            this.OldFile_Spread_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "版本号";
            this.OldFile_Spread_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.OldFile_Spread_Sheet1.Columns.Get(0).Label = "文件名称";
            this.OldFile_Spread_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.OldFile_Spread_Sheet1.Columns.Get(0).Width = 329F;
            maskCellType2.Mask = "#.#.####";
            this.OldFile_Spread_Sheet1.Columns.Get(1).CellType = maskCellType2;
            this.OldFile_Spread_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("宋体", 12F);
            this.OldFile_Spread_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.OldFile_Spread_Sheet1.Columns.Get(1).Label = "版本号";
            this.OldFile_Spread_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.OldFile_Spread_Sheet1.Columns.Get(1).Width = 113F;
            this.OldFile_Spread_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.OldFile_Spread_Sheet1.RowHeader.Visible = false;
            this.OldFile_Spread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.OldFile_Spread.SetActiveViewport(0, 1, 0);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.NewFile_Spread);
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(467, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(540, 632);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "新版本文件";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BrowseFileButton,
            this.toolStripSeparator1,
            this.SaveButton});
            this.toolStrip1.Location = new System.Drawing.Point(3, 17);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(534, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // BrowseFileButton
            // 
            this.BrowseFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BrowseFileButton.Image = ((System.Drawing.Image)(resources.GetObject("BrowseFileButton.Image")));
            this.BrowseFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BrowseFileButton.Name = "BrowseFileButton";
            this.BrowseFileButton.Size = new System.Drawing.Size(60, 22);
            this.BrowseFileButton.Text = "选择文件";
            this.BrowseFileButton.ToolTipText = "添加最新的文件";
            this.BrowseFileButton.Click += new System.EventHandler(this.BrowseFileButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // SaveButton
            // 
            this.SaveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.SaveButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveButton.Image")));
            this.SaveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(84, 22);
            this.SaveButton.Text = "上传到服务器";
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.OldFile_Spread);
            this.groupBox2.Controls.Add(this.toolStrip2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(8, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(459, 632);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "旧版本文件";
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RefreshButton});
            this.toolStrip2.Location = new System.Drawing.Point(3, 17);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(453, 25);
            this.toolStrip2.TabIndex = 4;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // RefreshButton
            // 
            this.RefreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.RefreshButton.Image = ((System.Drawing.Image)(resources.GetObject("RefreshButton.Image")));
            this.RefreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(36, 22);
            this.RefreshButton.Text = "刷新";
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // UpdaterManagerFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 648);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdaterManagerFrom";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "上传文件到服务器";
            this.Load += new System.EventHandler(this.SelectFileForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NewFile_Spread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewFile_Spread_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OldFile_Spread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OldFile_Spread_Sheet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FarPoint.Win.Spread.FpSpread NewFile_Spread;
        private FarPoint.Win.Spread.SheetView NewFile_Spread_Sheet1;
        private FarPoint.Win.Spread.FpSpread OldFile_Spread;
        private FarPoint.Win.Spread.SheetView OldFile_Spread_Sheet1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton BrowseFileButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton SaveButton;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton RefreshButton;

    }
}

