namespace BizComponents
{
    partial class QueryReportEvaluateDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryReportEvaluateDialog));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnTestUpload = new System.Windows.Forms.Button();
            this.Button_Export = new System.Windows.Forms.Button();
            this.TextBox_TestItems = new System.Windows.Forms.TextBox();
            this.TextBox_ReportName = new System.Windows.Forms.TextBox();
            this.TextBox_ReportNumber = new System.Windows.Forms.TextBox();
            this.ComboBox_TestRooms = new System.Windows.Forms.ComboBox();
            this.ComboBox_Company = new System.Windows.Forms.ComboBox();
            this.ComboBox_Segments = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Button_Exit = new System.Windows.Forms.Button();
            this.Button_Query = new System.Windows.Forms.Button();
            this.DateTimePicker_End = new System.Windows.Forms.DateTimePicker();
            this.DateTimePicker_Start = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.FpSpread = new Yqun.Client.BizUI.MyCell();
            this.FpSpread_Info = new FarPoint.Win.Spread.SheetView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TotalCount = new System.Windows.Forms.ToolStripLabel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.查看ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnTestSMSSend = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread_Info)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnTestSMSSend);
            this.groupBox1.Controls.Add(this.btnTestUpload);
            this.groupBox1.Controls.Add(this.Button_Export);
            this.groupBox1.Controls.Add(this.TextBox_TestItems);
            this.groupBox1.Controls.Add(this.TextBox_ReportName);
            this.groupBox1.Controls.Add(this.TextBox_ReportNumber);
            this.groupBox1.Controls.Add(this.ComboBox_TestRooms);
            this.groupBox1.Controls.Add(this.ComboBox_Company);
            this.groupBox1.Controls.Add(this.ComboBox_Segments);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.Button_Exit);
            this.groupBox1.Controls.Add(this.Button_Query);
            this.groupBox1.Controls.Add(this.DateTimePicker_End);
            this.groupBox1.Controls.Add(this.DateTimePicker_Start);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 570);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnTestUpload
            // 
            this.btnTestUpload.Location = new System.Drawing.Point(35, 519);
            this.btnTestUpload.Name = "btnTestUpload";
            this.btnTestUpload.Size = new System.Drawing.Size(75, 23);
            this.btnTestUpload.TabIndex = 33;
            this.btnTestUpload.Text = "模拟上传";
            this.btnTestUpload.UseVisualStyleBackColor = true;
            this.btnTestUpload.Visible = false;
            this.btnTestUpload.Click += new System.EventHandler(this.button1_Click);
            // 
            // Button_Export
            // 
            this.Button_Export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Export.Location = new System.Drawing.Point(14, 254);
            this.Button_Export.Name = "Button_Export";
            this.Button_Export.Size = new System.Drawing.Size(105, 27);
            this.Button_Export.TabIndex = 32;
            this.Button_Export.Text = "导出到xls";
            this.Button_Export.UseVisualStyleBackColor = true;
            this.Button_Export.Click += new System.EventHandler(this.Button_Export_Click);
            // 
            // TextBox_TestItems
            // 
            this.TextBox_TestItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox_TestItems.Location = new System.Drawing.Point(92, 159);
            this.TextBox_TestItems.Name = "TextBox_TestItems";
            this.TextBox_TestItems.Size = new System.Drawing.Size(151, 21);
            this.TextBox_TestItems.TabIndex = 31;
            // 
            // TextBox_ReportName
            // 
            this.TextBox_ReportName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox_ReportName.Location = new System.Drawing.Point(92, 102);
            this.TextBox_ReportName.Name = "TextBox_ReportName";
            this.TextBox_ReportName.Size = new System.Drawing.Size(151, 21);
            this.TextBox_ReportName.TabIndex = 30;
            // 
            // TextBox_ReportNumber
            // 
            this.TextBox_ReportNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox_ReportNumber.Location = new System.Drawing.Point(92, 130);
            this.TextBox_ReportNumber.Name = "TextBox_ReportNumber";
            this.TextBox_ReportNumber.Size = new System.Drawing.Size(151, 21);
            this.TextBox_ReportNumber.TabIndex = 29;
            // 
            // ComboBox_TestRooms
            // 
            this.ComboBox_TestRooms.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBox_TestRooms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_TestRooms.FormattingEnabled = true;
            this.ComboBox_TestRooms.Location = new System.Drawing.Point(92, 74);
            this.ComboBox_TestRooms.Name = "ComboBox_TestRooms";
            this.ComboBox_TestRooms.Size = new System.Drawing.Size(151, 20);
            this.ComboBox_TestRooms.TabIndex = 28;
            // 
            // ComboBox_Company
            // 
            this.ComboBox_Company.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBox_Company.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Company.FormattingEnabled = true;
            this.ComboBox_Company.Location = new System.Drawing.Point(92, 47);
            this.ComboBox_Company.Name = "ComboBox_Company";
            this.ComboBox_Company.Size = new System.Drawing.Size(151, 20);
            this.ComboBox_Company.TabIndex = 27;
            this.ComboBox_Company.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Company_SelectedIndexChanged);
            // 
            // ComboBox_Segments
            // 
            this.ComboBox_Segments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBox_Segments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Segments.FormattingEnabled = true;
            this.ComboBox_Segments.Location = new System.Drawing.Point(92, 20);
            this.ComboBox_Segments.Name = "ComboBox_Segments";
            this.ComboBox_Segments.Size = new System.Drawing.Size(151, 20);
            this.ComboBox_Segments.TabIndex = 26;
            this.ComboBox_Segments.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Segments_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 163);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 25;
            this.label9.Text = "试验项目(&R):";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 191);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 24;
            this.label8.Text = "报告日期(&R):";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 134);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 23;
            this.label7.Text = "报告编号(&N):";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 106);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 22;
            this.label6.Text = "试验报告(&R):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(12, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "试验室(&L):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(12, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "选择单位(&C):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(12, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "选择标段(&B):";
            // 
            // Button_Exit
            // 
            this.Button_Exit.Location = new System.Drawing.Point(416, 43);
            this.Button_Exit.Name = "Button_Exit";
            this.Button_Exit.Size = new System.Drawing.Size(75, 23);
            this.Button_Exit.TabIndex = 18;
            this.Button_Exit.Text = "关闭";
            this.Button_Exit.UseVisualStyleBackColor = true;
            this.Button_Exit.Click += new System.EventHandler(this.Button_Exit_Click);
            // 
            // Button_Query
            // 
            this.Button_Query.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Query.Location = new System.Drawing.Point(139, 254);
            this.Button_Query.Name = "Button_Query";
            this.Button_Query.Size = new System.Drawing.Size(105, 27);
            this.Button_Query.TabIndex = 18;
            this.Button_Query.Text = "查询";
            this.Button_Query.UseVisualStyleBackColor = true;
            this.Button_Query.Click += new System.EventHandler(this.Button_Query_Click);
            // 
            // DateTimePicker_End
            // 
            this.DateTimePicker_End.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DateTimePicker_End.Location = new System.Drawing.Point(92, 215);
            this.DateTimePicker_End.Name = "DateTimePicker_End";
            this.DateTimePicker_End.Size = new System.Drawing.Size(151, 21);
            this.DateTimePicker_End.TabIndex = 16;
            // 
            // DateTimePicker_Start
            // 
            this.DateTimePicker_Start.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DateTimePicker_Start.Location = new System.Drawing.Point(92, 188);
            this.DateTimePicker_Start.Name = "DateTimePicker_Start";
            this.DateTimePicker_Start.Size = new System.Drawing.Size(151, 21);
            this.DateTimePicker_Start.TabIndex = 17;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.FpSpread);
            this.groupBox2.Controls.Add(this.toolStrip1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(262, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(666, 570);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "试验报告列表";
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
            this.FpSpread.Size = new System.Drawing.Size(660, 525);
            this.FpSpread.TabIndex = 0;
            this.FpSpread.TabStripInsertTab = false;
            this.FpSpread.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            this.FpSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.FpSpread.Watermark = null;
            this.FpSpread.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.FpSpread_CellDoubleClick);
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
            this.toolStrip1.Size = new System.Drawing.Size(660, 25);
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
            this.splitter1.Location = new System.Drawing.Point(262, 6);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 570);
            this.splitter1.TabIndex = 14;
            this.splitter1.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查看ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 26);
            // 
            // 查看ToolStripMenuItem
            // 
            this.查看ToolStripMenuItem.Name = "查看ToolStripMenuItem";
            this.查看ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.查看ToolStripMenuItem.Text = "处理不合格报告";
            this.查看ToolStripMenuItem.Click += new System.EventHandler(this.查看ToolStripMenuItem_Click);
            // 
            // btnTestSMSSend
            // 
            this.btnTestSMSSend.Location = new System.Drawing.Point(116, 519);
            this.btnTestSMSSend.Name = "btnTestSMSSend";
            this.btnTestSMSSend.Size = new System.Drawing.Size(75, 23);
            this.btnTestSMSSend.TabIndex = 33;
            this.btnTestSMSSend.Text = "测试短信";
            this.btnTestSMSSend.UseVisualStyleBackColor = true;
            this.btnTestSMSSend.Visible = false;
            this.btnTestSMSSend.Click += new System.EventHandler(this.btnTestSMSSend_Click);
            // 
            // QueryReportEvaluateDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 582);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "QueryReportEvaluateDialog";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "查询不合格报告";
            this.Load += new System.EventHandler(this.QueryReportEvaluateDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread_Info)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        public Yqun.Client.BizUI.MyCell FpSpread;
        public FarPoint.Win.Spread.SheetView FpSpread_Info;
        private System.Windows.Forms.Button Button_Exit;
        private System.Windows.Forms.Button Button_Query;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripLabel TotalCount;
        private System.Windows.Forms.ComboBox ComboBox_TestRooms;
        private System.Windows.Forms.ComboBox ComboBox_Company;
        private System.Windows.Forms.ComboBox ComboBox_Segments;
        private System.Windows.Forms.DateTimePicker DateTimePicker_End;
        private System.Windows.Forms.DateTimePicker DateTimePicker_Start;
        private System.Windows.Forms.TextBox TextBox_ReportNumber;
        private System.Windows.Forms.TextBox TextBox_ReportName;
        private System.Windows.Forms.TextBox TextBox_TestItems;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 查看ToolStripMenuItem;
        private System.Windows.Forms.Button Button_Export;
        private System.Windows.Forms.Button btnTestUpload;
        private System.Windows.Forms.Button btnTestSMSSend;

    }
}