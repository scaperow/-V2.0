namespace BizComponents
{
    partial class TestOverTimeProcess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestOverTimeProcess));
            this.lb_reason = new System.Windows.Forms.Label();
            this.tb_reason = new System.Windows.Forms.TextBox();
            this.tb_process = new System.Windows.Forms.TextBox();
            this.bt_approval = new System.Windows.Forms.Button();
            this.bt_disapproval = new System.Windows.Forms.Button();
            this.bt_cancel = new System.Windows.Forms.Button();
            this.lb_process = new System.Windows.Forms.Label();
            this.Table = new Yqun.Client.BizUI.MyCell();
            this.Sheet = new FarPoint.Win.Spread.SheetView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ApplyAll = new System.Windows.Forms.CheckBox();
            this.btnReset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Table)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sheet)).BeginInit();
            this.SuspendLayout();
            // 
            // lb_reason
            // 
            this.lb_reason.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lb_reason.AutoSize = true;
            this.lb_reason.Location = new System.Drawing.Point(10, 199);
            this.lb_reason.Name = "lb_reason";
            this.lb_reason.Size = new System.Drawing.Size(53, 12);
            this.lb_reason.TabIndex = 1;
            this.lb_reason.Text = "原因内容";
            // 
            // tb_reason
            // 
            this.tb_reason.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_reason.Location = new System.Drawing.Point(12, 214);
            this.tb_reason.Multiline = true;
            this.tb_reason.Name = "tb_reason";
            this.tb_reason.ReadOnly = true;
            this.tb_reason.Size = new System.Drawing.Size(612, 60);
            this.tb_reason.TabIndex = 0;
            // 
            // tb_process
            // 
            this.tb_process.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_process.Location = new System.Drawing.Point(12, 303);
            this.tb_process.Multiline = true;
            this.tb_process.Name = "tb_process";
            this.tb_process.Size = new System.Drawing.Size(612, 62);
            this.tb_process.TabIndex = 0;
            // 
            // bt_approval
            // 
            this.bt_approval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_approval.Location = new System.Drawing.Point(336, 390);
            this.bt_approval.Name = "bt_approval";
            this.bt_approval.Size = new System.Drawing.Size(75, 23);
            this.bt_approval.TabIndex = 2;
            this.bt_approval.Text = "通过";
            this.bt_approval.UseVisualStyleBackColor = true;
            this.bt_approval.Click += new System.EventHandler(this.bt_approval_Click);
            // 
            // bt_disapproval
            // 
            this.bt_disapproval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_disapproval.Location = new System.Drawing.Point(417, 390);
            this.bt_disapproval.Name = "bt_disapproval";
            this.bt_disapproval.Size = new System.Drawing.Size(75, 23);
            this.bt_disapproval.TabIndex = 2;
            this.bt_disapproval.Text = "拒绝";
            this.bt_disapproval.UseVisualStyleBackColor = true;
            this.bt_disapproval.Click += new System.EventHandler(this.bt_disapproval_Click);
            // 
            // bt_cancel
            // 
            this.bt_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_cancel.Location = new System.Drawing.Point(538, 390);
            this.bt_cancel.Name = "bt_cancel";
            this.bt_cancel.Size = new System.Drawing.Size(86, 23);
            this.bt_cancel.TabIndex = 2;
            this.bt_cancel.Text = "关闭";
            this.bt_cancel.UseVisualStyleBackColor = true;
            this.bt_cancel.Click += new System.EventHandler(this.bt_cancel_Click);
            // 
            // lb_process
            // 
            this.lb_process.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lb_process.AutoSize = true;
            this.lb_process.Location = new System.Drawing.Point(10, 288);
            this.lb_process.Name = "lb_process";
            this.lb_process.Size = new System.Drawing.Size(53, 12);
            this.lb_process.TabIndex = 1;
            this.lb_process.Text = "处理内容";
            // 
            // Table
            // 
            this.Table.AccessibleDescription = "Table, Sheet1, Row 0, Column 0, ";
            this.Table.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Table.BackColor = System.Drawing.SystemColors.Control;
            this.Table.IsEditing = false;
            this.Table.Location = new System.Drawing.Point(12, 24);
            this.Table.Name = "Table";
            this.Table.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Table.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.Sheet});
            this.Table.Size = new System.Drawing.Size(612, 153);
            this.Table.TabIndex = 3;
            this.Table.Watermark = null;
            this.Table.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(this.Table_SelectionChanged);
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
            this.Sheet.Columns.Get(4).Width = 92F;
            this.Sheet.Columns.Get(5).Label = "试验数据";
            this.Sheet.Columns.Get(5).Width = 131F;
            this.Sheet.Columns.Get(6).Label = "实际试验日期";
            this.Sheet.Columns.Get(6).Width = 142F;
            this.Sheet.Columns.Get(7).Label = "龄期到期日期";
            this.Sheet.Columns.Get(7).Width = 112F;
            this.Sheet.PrintInfo.PaperSize = ((System.Drawing.Printing.PaperSize)(resources.GetObject("resource.PaperSize")));
            this.Sheet.PrintInfo.PdfFileName = "0";
            this.Sheet.RowHeader.Columns.Default.Resizable = true;
            this.Sheet.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.Sheet.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.Sheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, -103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "试验数据";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "试验数据";
            // 
            // ApplyAll
            // 
            this.ApplyAll.AutoSize = true;
            this.ApplyAll.Checked = true;
            this.ApplyAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ApplyAll.Location = new System.Drawing.Point(222, 394);
            this.ApplyAll.Name = "ApplyAll";
            this.ApplyAll.Size = new System.Drawing.Size(108, 16);
            this.ApplyAll.TabIndex = 4;
            this.ApplyAll.Text = "应用到所有试验";
            this.ApplyAll.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Location = new System.Drawing.Point(14, 390);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "打回";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // TestOverTimeProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 425);
            this.Controls.Add(this.ApplyAll);
            this.Controls.Add(this.Table);
            this.Controls.Add(this.tb_process);
            this.Controls.Add(this.lb_process);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lb_reason);
            this.Controls.Add(this.bt_cancel);
            this.Controls.Add(this.tb_reason);
            this.Controls.Add(this.bt_disapproval);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.bt_approval);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TestOverTimeProcess";
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
        private System.Windows.Forms.TextBox tb_process;
        private System.Windows.Forms.Button bt_approval;
        private System.Windows.Forms.Button bt_disapproval;
        private System.Windows.Forms.Button bt_cancel;
        private System.Windows.Forms.Label lb_reason;
        private System.Windows.Forms.Label lb_process;
        private Yqun.Client.BizUI.MyCell Table;
        private FarPoint.Win.Spread.SheetView Sheet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox ApplyAll;
        private System.Windows.Forms.Button btnReset;
    }
}