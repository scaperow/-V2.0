namespace BizComponents
{
    partial class TJItemDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TJItemDialog));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Button_Save = new System.Windows.Forms.Button();
            this.Button_Exit = new System.Windows.Forms.Button();
            this.Button_Add = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.FpSpread = new Yqun.Client.BizUI.MyCell();
            this.FpSpread_Info = new FarPoint.Win.Spread.SheetView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TotalCount = new System.Windows.Forms.ToolStripLabel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread_Info)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Button_Save);
            this.groupBox1.Controls.Add(this.Button_Exit);
            this.groupBox1.Controls.Add(this.Button_Add);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(9, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(151, 436);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作";
            // 
            // Button_Save
            // 
            this.Button_Save.Location = new System.Drawing.Point(12, 60);
            this.Button_Save.Name = "Button_Save";
            this.Button_Save.Size = new System.Drawing.Size(105, 27);
            this.Button_Save.TabIndex = 18;
            this.Button_Save.Text = "保存";
            this.Button_Save.UseVisualStyleBackColor = true;
            this.Button_Save.Click += new System.EventHandler(this.Button_Save_Click);
            // 
            // Button_Exit
            // 
            this.Button_Exit.Location = new System.Drawing.Point(8, 403);
            this.Button_Exit.Name = "Button_Exit";
            this.Button_Exit.Size = new System.Drawing.Size(105, 27);
            this.Button_Exit.TabIndex = 18;
            this.Button_Exit.Text = "关闭";
            this.Button_Exit.UseVisualStyleBackColor = true;
            this.Button_Exit.Click += new System.EventHandler(this.Button_Exit_Click);
            // 
            // Button_Add
            // 
            this.Button_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Add.Location = new System.Drawing.Point(12, 17);
            this.Button_Add.Name = "Button_Add";
            this.Button_Add.Size = new System.Drawing.Size(105, 27);
            this.Button_Add.TabIndex = 18;
            this.Button_Add.Text = "新增";
            this.Button_Add.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.FpSpread);
            this.groupBox2.Controls.Add(this.toolStrip1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(160, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(766, 436);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "统计项";
            // 
            // FpSpread
            // 
            this.FpSpread.AccessibleDescription = "FpSpread, SheetInfo, Row 0, Column 0, ";
            this.FpSpread.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.FpSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FpSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.FpSpread.IsEditing = false;
            this.FpSpread.Location = new System.Drawing.Point(3, 17);
            this.FpSpread.Name = "FpSpread";
            this.FpSpread.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.FpSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FpSpread_Info});
            this.FpSpread.Size = new System.Drawing.Size(760, 391);
            this.FpSpread.TabIndex = 0;
            this.FpSpread.TabStripInsertTab = false;
            this.FpSpread.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            this.FpSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.FpSpread.Watermark = null;
            this.FpSpread.EditChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.FpSpread_EditChange);
            // 
            // FpSpread_Info
            // 
            this.FpSpread_Info.Reset();
            this.FpSpread_Info.SheetName = "SheetInfo";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FpSpread_Info.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FpSpread_Info.ColumnCount = 0;
            this.FpSpread_Info.RowCount = 1;
            this.FpSpread_Info.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.FpSpread_Info.PrintInfo.PaperSize = ((System.Drawing.Printing.PaperSize)(resources.GetObject("resource.PaperSize")));
            this.FpSpread_Info.RowHeader.Visible = false;
            this.FpSpread_Info.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.FpSpread.SetActiveViewport(0, 0, 1);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TotalCount});
            this.toolStrip1.Location = new System.Drawing.Point(3, 408);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(760, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // TotalCount
            // 
            this.TotalCount.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TotalCount.Name = "TotalCount";
            this.TotalCount.Size = new System.Drawing.Size(79, 22);
            this.TotalCount.Text = "共 {0} 条记录";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(6, 6);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 436);
            this.splitter1.TabIndex = 14;
            this.splitter1.TabStop = false;
            // 
            // TJItemDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 448);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.splitter1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TJItemDialog";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "统计项";
            this.Load += new System.EventHandler(this.TemperatureDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread_Info)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        public Yqun.Client.BizUI.MyCell FpSpread;
        public FarPoint.Win.Spread.SheetView FpSpread_Info;
        private System.Windows.Forms.Button Button_Exit;
        private System.Windows.Forms.Button Button_Add;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel TotalCount;
        private System.Windows.Forms.Button Button_Save;

    }
}