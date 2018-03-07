namespace BizComponents
{
    partial class TestOverTimeReason
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestOverTimeReason));
            this.tb_reason = new System.Windows.Forms.TextBox();
            this.bt_save = new System.Windows.Forms.Button();
            this.bt_cancel = new System.Windows.Forms.Button();
            this.Table = new Yqun.Client.BizUI.MyCell();
            this.Sheet = new FarPoint.Win.Spread.SheetView();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.lb_reason = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_process = new System.Windows.Forms.TextBox();
            this.lb_process = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Table)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sheet)).BeginInit();
            this.SuspendLayout();
            // 
            // tb_reason
            // 
            this.tb_reason.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_reason.Location = new System.Drawing.Point(14, 218);
            this.tb_reason.Multiline = true;
            this.tb_reason.Name = "tb_reason";
            this.tb_reason.Size = new System.Drawing.Size(610, 60);
            this.tb_reason.TabIndex = 0;
            // 
            // bt_save
            // 
            this.bt_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_save.Location = new System.Drawing.Point(468, 390);
            this.bt_save.Name = "bt_save";
            this.bt_save.Size = new System.Drawing.Size(75, 23);
            this.bt_save.TabIndex = 2;
            this.bt_save.Text = "保存";
            this.bt_save.UseVisualStyleBackColor = true;
            this.bt_save.Click += new System.EventHandler(this.bt_save_Click);
            // 
            // bt_cancel
            // 
            this.bt_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_cancel.Location = new System.Drawing.Point(549, 390);
            this.bt_cancel.Name = "bt_cancel";
            this.bt_cancel.Size = new System.Drawing.Size(75, 23);
            this.bt_cancel.TabIndex = 2;
            this.bt_cancel.Text = "取消";
            this.bt_cancel.UseVisualStyleBackColor = true;
            this.bt_cancel.Click += new System.EventHandler(this.bt_cancel_Click);
            // 
            // Table
            // 
            this.Table.AccessibleDescription = "Table, Sheet1, Row 0, Column 0, ";
            this.Table.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Table.BackColor = System.Drawing.SystemColors.Control;
            this.Table.IsEditing = false;
            this.Table.Location = new System.Drawing.Point(14, 33);
            this.Table.Name = "Table";
            this.Table.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Table.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.Sheet});
            this.Table.Size = new System.Drawing.Size(610, 155);
            this.Table.TabIndex = 4;
            this.Table.Watermark = null;
            // 
            // Sheet
            // 
            this.Sheet.Reset();
            this.Sheet.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.Sheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.Sheet.ColumnCount = 8;
            this.Sheet.AllowNoteEdit = false;
            this.Sheet.ColumnHeader.Cells.Get(0, 0).Value = "标段名称";
            this.Sheet.ColumnHeader.Cells.Get(0, 1).Value = "单位名称";
            this.Sheet.ColumnHeader.Cells.Get(0, 2).Value = "试验室名称";
            this.Sheet.ColumnHeader.Cells.Get(0, 3).Value = "模板名称";
            this.Sheet.ColumnHeader.Cells.Get(0, 4).Value = "委托编号";
            this.Sheet.ColumnHeader.Cells.Get(0, 5).Value = "试验数据";
            this.Sheet.ColumnHeader.Cells.Get(0, 6).Value = "实际试验日期";
            this.Sheet.ColumnHeader.Cells.Get(0, 7).Value = "龄期到期日期";
            this.Sheet.Columns.Get(0).Label = "标段名称";
            this.Sheet.Columns.Get(0).Width = 116F;
            this.Sheet.Columns.Get(1).Label = "单位名称";
            this.Sheet.Columns.Get(1).Width = 93F;
            this.Sheet.Columns.Get(2).Label = "试验室名称";
            this.Sheet.Columns.Get(2).Width = 102F;
            this.Sheet.Columns.Get(3).Label = "模板名称";
            this.Sheet.Columns.Get(3).Width = 119F;
            this.Sheet.Columns.Get(4).Label = "委托编号";
            this.Sheet.Columns.Get(4).Width = 89F;
            this.Sheet.Columns.Get(5).Label = "试验数据";
            this.Sheet.Columns.Get(5).Width = 131F;
            this.Sheet.Columns.Get(6).Label = "实际试验日期";
            this.Sheet.Columns.Get(6).Width = 140F;
            this.Sheet.Columns.Get(7).Label = "龄期到期日期";
            this.Sheet.Columns.Get(7).Width = 112F;
            this.Sheet.PrintInfo.PaperSize = ((System.Drawing.Printing.PaperSize)(resources.GetObject("resource.PaperSize")));
            this.Sheet.PrintInfo.PdfFileName = "0";
            this.Sheet.RowHeader.Columns.Default.Resizable = true;
            this.Sheet.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.Sheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // chkAll
            // 
            this.chkAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAll.AutoSize = true;
            this.chkAll.Checked = true;
            this.chkAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAll.Location = new System.Drawing.Point(354, 394);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(108, 16);
            this.chkAll.TabIndex = 3;
            this.chkAll.Text = "应用到所有试验";
            this.chkAll.UseVisualStyleBackColor = true;
            // 
            // lb_reason
            // 
            this.lb_reason.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lb_reason.AutoSize = true;
            this.lb_reason.Location = new System.Drawing.Point(12, 203);
            this.lb_reason.Name = "lb_reason";
            this.lb_reason.Size = new System.Drawing.Size(53, 12);
            this.lb_reason.TabIndex = 4;
            this.lb_reason.Text = "延时原因";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "试验数据";
            // 
            // tb_process
            // 
            this.tb_process.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_process.BackColor = System.Drawing.SystemColors.Control;
            this.tb_process.Location = new System.Drawing.Point(14, 302);
            this.tb_process.Multiline = true;
            this.tb_process.Name = "tb_process";
            this.tb_process.ReadOnly = true;
            this.tb_process.Size = new System.Drawing.Size(610, 62);
            this.tb_process.TabIndex = 6;
            // 
            // lb_process
            // 
            this.lb_process.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lb_process.AutoSize = true;
            this.lb_process.Location = new System.Drawing.Point(12, 287);
            this.lb_process.Name = "lb_process";
            this.lb_process.Size = new System.Drawing.Size(53, 12);
            this.lb_process.TabIndex = 7;
            this.lb_process.Text = "处理内容";
            // 
            // TestOverTimeReason
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 425);
            this.Controls.Add(this.tb_process);
            this.Controls.Add(this.lb_process);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Table);
            this.Controls.Add(this.lb_reason);
            this.Controls.Add(this.tb_reason);
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.bt_cancel);
            this.Controls.Add(this.bt_save);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TestOverTimeReason";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "延时处理";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.TestOverTimeProcess_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Table)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sheet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_reason;
        private System.Windows.Forms.Button bt_save;
        private System.Windows.Forms.Button bt_cancel;
        private Yqun.Client.BizUI.MyCell Table;
        private FarPoint.Win.Spread.SheetView Sheet;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.Label lb_reason;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_process;
        private System.Windows.Forms.Label lb_process;
    }
}