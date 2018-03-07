namespace BizComponents
{
    partial class PXReportRelationDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PXReportRelationDialog));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.FpSpread = new Yqun.Client.BizUI.MyCell();
            this.FpSpread_Info = new FarPoint.Win.Spread.SheetView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TotalCount = new System.Windows.Forms.ToolStripLabel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.DateTimePicker_Start = new System.Windows.Forms.DateTimePicker();
            this.DateTimePicker_End = new System.Windows.Forms.DateTimePicker();
            this.Button_Query = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.Button_Export = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBGBH = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread_Info)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.FpSpread);
            this.groupBox2.Controls.Add(this.toolStrip1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(199, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(729, 570);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "平行关系对应查询";
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
            this.FpSpread.Size = new System.Drawing.Size(723, 525);
            this.FpSpread.TabIndex = 0;
            this.FpSpread.TabStripInsertTab = false;
            this.FpSpread.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            this.FpSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.FpSpread.Watermark = null;
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
            this.toolStrip1.Location = new System.Drawing.Point(3, 542);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(723, 25);
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
            this.splitter1.Location = new System.Drawing.Point(199, 6);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 570);
            this.splitter1.TabIndex = 14;
            this.splitter1.TabStop = false;
            // 
            // DateTimePicker_Start
            // 
            this.DateTimePicker_Start.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DateTimePicker_Start.Location = new System.Drawing.Point(95, 40);
            this.DateTimePicker_Start.Name = "DateTimePicker_Start";
            this.DateTimePicker_Start.Size = new System.Drawing.Size(88, 21);
            this.DateTimePicker_Start.TabIndex = 17;
            // 
            // DateTimePicker_End
            // 
            this.DateTimePicker_End.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DateTimePicker_End.Location = new System.Drawing.Point(95, 67);
            this.DateTimePicker_End.Name = "DateTimePicker_End";
            this.DateTimePicker_End.Size = new System.Drawing.Size(88, 21);
            this.DateTimePicker_End.TabIndex = 16;
            // 
            // Button_Query
            // 
            this.Button_Query.Location = new System.Drawing.Point(113, 113);
            this.Button_Query.Name = "Button_Query";
            this.Button_Query.Size = new System.Drawing.Size(70, 27);
            this.Button_Query.TabIndex = 18;
            this.Button_Query.Text = "查询";
            this.Button_Query.UseVisualStyleBackColor = true;
            this.Button_Query.Click += new System.EventHandler(this.Button_Query_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 24);
            this.label8.TabIndex = 24;
            this.label8.Text = "日期范围:\r\n(施工报告)";
            // 
            // Button_Export
            // 
            this.Button_Export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Export.Location = new System.Drawing.Point(17, 113);
            this.Button_Export.Name = "Button_Export";
            this.Button_Export.Size = new System.Drawing.Size(70, 27);
            this.Button_Export.TabIndex = 34;
            this.Button_Export.Text = "导出到xls";
            this.Button_Export.UseVisualStyleBackColor = true;
            this.Button_Export.Click += new System.EventHandler(this.Button_Export_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBGBH);
            this.groupBox1.Controls.Add(this.Button_Export);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.Button_Query);
            this.groupBox1.Controls.Add(this.DateTimePicker_End);
            this.groupBox1.Controls.Add(this.DateTimePicker_Start);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(193, 570);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 24;
            this.label1.Text = "报告编号:";
            // 
            // txtBGBH
            // 
            this.txtBGBH.Location = new System.Drawing.Point(95, 16);
            this.txtBGBH.Name = "txtBGBH";
            this.txtBGBH.Size = new System.Drawing.Size(88, 21);
            this.txtBGBH.TabIndex = 35;
            // 
            // PXReportRelationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 582);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "PXReportRelationDialog";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "平行关系对应查询";
            this.Load += new System.EventHandler(this.PXReportDialog_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread_Info)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        public Yqun.Client.BizUI.MyCell FpSpread;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel TotalCount;
        public FarPoint.Win.Spread.SheetView FpSpread_Info;
        private System.Windows.Forms.DateTimePicker DateTimePicker_Start;
        private System.Windows.Forms.DateTimePicker DateTimePicker_End;
        private System.Windows.Forms.Button Button_Query;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button Button_Export;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBGBH;
        private System.Windows.Forms.Label label1;

    }
}