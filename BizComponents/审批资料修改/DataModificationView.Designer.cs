namespace BizComponents
{
    partial class DataModificationView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataModificationView));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FpSpread = new Yqun.Client.BizUI.MyCell();
            this.FpSpread_Info = new FarPoint.Win.Spread.SheetView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel_Info = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.YesButton = new System.Windows.Forms.ToolStripButton();
            this.NoButton = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Button_Exit = new System.Windows.Forms.Button();
            this.TextBox_Reason = new System.Windows.Forms.TextBox();
            this.TextBox_Content = new System.Windows.Forms.TextBox();
            this.label_Date = new System.Windows.Forms.Label();
            this.label_Sponsor = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread_Info)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.FpSpread);
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(626, 560);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "申请列表";
            // 
            // FpSpread
            // 
            this.FpSpread.AccessibleDescription = "FpSpread, SheetInfo, Row 0, Column 0, ";
            this.FpSpread.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.FpSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FpSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.FpSpread.Location = new System.Drawing.Point(3, 42);
            this.FpSpread.Name = "FpSpread";
            this.FpSpread.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.FpSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FpSpread_Info});
            this.FpSpread.Size = new System.Drawing.Size(620, 515);
            this.FpSpread.TabIndex = 0;
            this.FpSpread.TabStripInsertTab = false;
            this.FpSpread.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            this.FpSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.FpSpread.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.FpSpread_CellClick);
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
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel_Info,
            this.toolStripLabel2,
            this.toolStripSeparator1,
            this.YesButton,
            this.NoButton});
            this.toolStrip1.Location = new System.Drawing.Point(3, 17);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(620, 25);
            this.toolStrip1.TabIndex = 14;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel_Info
            // 
            this.toolStripLabel_Info.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripLabel_Info.Name = "toolStripLabel_Info";
            this.toolStripLabel_Info.Size = new System.Drawing.Size(55, 22);
            this.toolStripLabel_Info.Text = "日期(&D):";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(67, 22);
            this.toolStripLabel2.Text = "2013/1/21";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // YesButton
            // 
            this.YesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.YesButton.Image = ((System.Drawing.Image)(resources.GetObject("YesButton.Image")));
            this.YesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.YesButton.Name = "YesButton";
            this.YesButton.Size = new System.Drawing.Size(36, 22);
            this.YesButton.Text = "通过";
            this.YesButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // NoButton
            // 
            this.NoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.NoButton.Image = ((System.Drawing.Image)(resources.GetObject("NoButton.Image")));
            this.NoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NoButton.Name = "NoButton";
            this.NoButton.Size = new System.Drawing.Size(48, 22);
            this.NoButton.Text = "不通过";
            this.NoButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Button_Exit);
            this.groupBox2.Controls.Add(this.TextBox_Reason);
            this.groupBox2.Controls.Add(this.TextBox_Content);
            this.groupBox2.Controls.Add(this.label_Date);
            this.groupBox2.Controls.Add(this.label_Sponsor);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Location = new System.Drawing.Point(633, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(256, 560);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "详细信息";
            // 
            // Button_Exit
            // 
            this.Button_Exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button_Exit.Location = new System.Drawing.Point(14, 514);
            this.Button_Exit.Name = "Button_Exit";
            this.Button_Exit.Size = new System.Drawing.Size(229, 23);
            this.Button_Exit.TabIndex = 8;
            this.Button_Exit.Text = "关闭";
            this.Button_Exit.UseVisualStyleBackColor = true;
            this.Button_Exit.Click += new System.EventHandler(this.Button_Exit_Click);
            // 
            // TextBox_Reason
            // 
            this.TextBox_Reason.BackColor = System.Drawing.Color.White;
            this.TextBox_Reason.Location = new System.Drawing.Point(13, 258);
            this.TextBox_Reason.Multiline = true;
            this.TextBox_Reason.Name = "TextBox_Reason";
            this.TextBox_Reason.ReadOnly = true;
            this.TextBox_Reason.Size = new System.Drawing.Size(230, 233);
            this.TextBox_Reason.TabIndex = 7;
            // 
            // TextBox_Content
            // 
            this.TextBox_Content.BackColor = System.Drawing.Color.White;
            this.TextBox_Content.Location = new System.Drawing.Point(13, 148);
            this.TextBox_Content.Multiline = true;
            this.TextBox_Content.Name = "TextBox_Content";
            this.TextBox_Content.ReadOnly = true;
            this.TextBox_Content.Size = new System.Drawing.Size(230, 82);
            this.TextBox_Content.TabIndex = 6;
            // 
            // label_Date
            // 
            this.label_Date.BackColor = System.Drawing.Color.White;
            this.label_Date.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_Date.Location = new System.Drawing.Point(13, 94);
            this.label_Date.Name = "label_Date";
            this.label_Date.Size = new System.Drawing.Size(230, 23);
            this.label_Date.TabIndex = 5;
            this.label_Date.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_Sponsor
            // 
            this.label_Sponsor.BackColor = System.Drawing.Color.White;
            this.label_Sponsor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_Sponsor.Location = new System.Drawing.Point(13, 42);
            this.label_Sponsor.Name = "label_Sponsor";
            this.label_Sponsor.Size = new System.Drawing.Size(230, 23);
            this.label_Sponsor.TabIndex = 4;
            this.label_Sponsor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 239);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "原因(&R):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "内容(&C):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "申请时间(&D):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "申请者(&S):";
            // 
            // DataModificationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 574);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DataModificationView";
            this.Padding = new System.Windows.Forms.Padding(7);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查看修改申请";
            this.Load += new System.EventHandler(this.DataModificationView_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread_Info)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public Yqun.Client.BizUI.MyCell FpSpread;
        public FarPoint.Win.Spread.SheetView FpSpread_Info;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_Info;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton YesButton;
        private System.Windows.Forms.ToolStripButton NoButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_Date;
        private System.Windows.Forms.Label label_Sponsor;
        private System.Windows.Forms.TextBox TextBox_Reason;
        private System.Windows.Forms.TextBox TextBox_Content;
        private System.Windows.Forms.Button Button_Exit;

    }
}