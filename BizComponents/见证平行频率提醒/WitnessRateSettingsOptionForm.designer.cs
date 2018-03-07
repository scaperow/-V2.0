namespace BizComponents.见证平行频率提醒
{
    partial class WitnessRateSettingsOptionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WitnessRateSettingsOptionForm));
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.FpSpread_Info = new FarPoint.Win.Spread.SheetView();
            this.FpSpread = new Yqun.Client.BizUI.MyCell();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.totalCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread_Info)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox1,
            this.toolStripButton1});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(723, 25);
            this.toolStrip2.TabIndex = 4;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // fpSpread1
            // 
            this.fpSpread1.AccessibleDescription = "";
            this.fpSpread1.AllowDragFill = true;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.Location = new System.Drawing.Point(0, 0);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FpSpread_Info});
            this.fpSpread1.Size = new System.Drawing.Size(723, 450);
            this.fpSpread1.TabIndex = 3;
            // 
            // FpSpread_Info
            // 
            this.FpSpread_Info.Reset();
            this.FpSpread_Info.SheetName = "Sheet1";
            this.FpSpread_Info.ColumnCount = 0;
            this.FpSpread_Info.RowCount = 1;
            this.fpSpread1.SetActiveViewport(0, 0, 1);
            // 
            // FpSpread
            // 
            this.FpSpread.AccessibleDescription = "";
            this.FpSpread.Location = new System.Drawing.Point(0, 0);
            this.FpSpread.Name = "FpSpread";
            this.FpSpread.Size = new System.Drawing.Size(0, 0);
            this.FpSpread.TabIndex = 2;
            this.FpSpread.Watermark = null;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.totalCount});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 450);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(723, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "共 {0} 条记录";
            // 
            // totalCount
            // 
            this.totalCount.Name = "totalCount";
            this.totalCount.Size = new System.Drawing.Size(79, 22);
            this.totalCount.Text = "共 {0} 条记录";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "平行率",
            "见证率"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 25);
            this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "设置";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // WitnessRateSettingsOptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 475);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.fpSpread1);
            this.Controls.Add(this.FpSpread);
            this.Controls.Add(this.toolStrip1);
            this.MaximizeBox = false;
            this.Name = "WitnessRateSettingsOptionForm";
            this.ShowIcon = false;
            this.Text = "见证、平行率设置选项";
            this.Load += new System.EventHandler(this.WitnessRateSettingsOptionForm_Load);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread_Info)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        public Yqun.Client.BizUI.MyCell FpSpread;
        private System.Windows.Forms.ToolStripLabel totalCount;
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView FpSpread_Info;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}