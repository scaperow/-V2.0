namespace BizComponents
{
    partial class NotifyUserMsgDlg
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
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.FpSpread_Info = new FarPoint.Win.Spread.SheetView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.totalCount = new System.Windows.Forms.ToolStripLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.cmbLines = new System.Windows.Forms.ComboBox();
            this.lblMsg = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread_Info)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fpSpread1
            // 
            this.fpSpread1.AccessibleDescription = "";
            this.fpSpread1.AllowDragFill = true;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.Location = new System.Drawing.Point(0, 32);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FpSpread_Info});
            this.fpSpread1.Size = new System.Drawing.Size(843, 386);
            this.fpSpread1.TabIndex = 6;
            // 
            // FpSpread_Info
            // 
            this.FpSpread_Info.Reset();
            this.FpSpread_Info.SheetName = "Sheet1";
            this.FpSpread_Info.ColumnCount = 0;
            this.FpSpread_Info.RowCount = 1;
            this.fpSpread1.SetActiveViewport(0, 0, 1);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.totalCount});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 418);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(843, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "共 {0} 条记录";
            // 
            // totalCount
            // 
            this.totalCount.Name = "totalCount";
            this.totalCount.Size = new System.Drawing.Size(79, 22);
            this.totalCount.Text = "共 {0} 条记录";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblMsg);
            this.panel1.Controls.Add(this.txtMsg);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.ButtonOk);
            this.panel1.Controls.Add(this.chkSelectAll);
            this.panel1.Controls.Add(this.cmbLines);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(843, 32);
            this.panel1.TabIndex = 8;
            // 
            // txtMsg
            // 
            this.txtMsg.Location = new System.Drawing.Point(274, 6);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(344, 21);
            this.txtMsg.TabIndex = 3;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(3, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(58, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // ButtonOk
            // 
            this.ButtonOk.Location = new System.Drawing.Point(624, 4);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(70, 23);
            this.ButtonOk.TabIndex = 2;
            this.ButtonOk.Text = "发送消息";
            this.ButtonOk.UseVisualStyleBackColor = true;
            this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(229, 8);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(48, 16);
            this.chkSelectAll.TabIndex = 1;
            this.chkSelectAll.Text = "全选";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // cmbLines
            // 
            this.cmbLines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLines.FormattingEnabled = true;
            this.cmbLines.Location = new System.Drawing.Point(67, 6);
            this.cmbLines.Name = "cmbLines";
            this.cmbLines.Size = new System.Drawing.Size(156, 20);
            this.cmbLines.TabIndex = 0;
            this.cmbLines.SelectedIndexChanged += new System.EventHandler(this.cmbLines_SelectedIndexChanged);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.ForeColor = System.Drawing.Color.Red;
            this.lblMsg.Location = new System.Drawing.Point(700, 10);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 12);
            this.lblMsg.TabIndex = 9;
            // 
            // NotifyUserMsgDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 443);
            this.Controls.Add(this.fpSpread1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NotifyUserMsgDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "消息发送";
            this.Load += new System.EventHandler(this.NotifyUserMsgDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread_Info)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView FpSpread_Info;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel totalCount;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbLines;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblMsg;

    }
}