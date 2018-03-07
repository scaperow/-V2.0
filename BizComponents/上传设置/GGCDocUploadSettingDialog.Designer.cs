namespace BizComponents
{
    partial class GGCDocUploadSettingDialog
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
            FarPoint.Win.Spread.CellType.GeneralCellType generalCellType1 = new FarPoint.Win.Spread.CellType.GeneralCellType();
            FarPoint.Win.Spread.CellType.GeneralCellType generalCellType2 = new FarPoint.Win.Spread.CellType.GeneralCellType();
            FarPoint.Win.Spread.CellType.ButtonCellType buttonCellType1 = new FarPoint.Win.Spread.CellType.ButtonCellType();
            this.label1 = new System.Windows.Forms.Label();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.fpSpread2 = new FarPoint.Win.Spread.FpSpread();
            this.SheetView2 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_moduleName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_rootName = new System.Windows.Forms.TextBox();
            this.bt_preview = new System.Windows.Forms.Button();
            this.process1 = new System.Diagnostics.Process();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SheetView2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "试验模板：";
            // 
            // ButtonOk
            // 
            this.ButtonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonOk.Location = new System.Drawing.Point(507, 547);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(75, 23);
            this.ButtonOk.TabIndex = 6;
            this.ButtonOk.Text = "保存";
            this.ButtonOk.UseVisualStyleBackColor = true;
            this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonCancel.Location = new System.Drawing.Point(630, 547);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 7;
            this.ButtonCancel.Text = "取消";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // fpSpread2
            // 
            this.fpSpread2.AccessibleDescription = "fpSpread2, Sheet1, Row 0, Column 0, ";
            this.fpSpread2.BackColor = System.Drawing.SystemColors.Control;
            this.fpSpread2.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpSpread2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread2.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread2.Location = new System.Drawing.Point(3, 17);
            this.fpSpread2.Name = "fpSpread2";
            this.fpSpread2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread2.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpSpread2.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.SheetView2});
            this.fpSpread2.Size = new System.Drawing.Size(713, 413);
            this.fpSpread2.TabIndex = 10;
            this.fpSpread2.TabStripInsertTab = false;
            this.fpSpread2.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            this.fpSpread2.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread2.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpSpread2_ButtonClicked);
            // 
            // SheetView2
            // 
            this.SheetView2.Reset();
            this.SheetView2.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.SheetView2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.SheetView2.ColumnCount = 3;
            this.SheetView2.RowCount = 0;
            this.SheetView2.ColumnHeader.Cells.Get(0, 0).Value = "项目描述";
            this.SheetView2.ColumnHeader.Cells.Get(0, 1).Value = "数据";
            this.SheetView2.ColumnHeader.Cells.Get(0, 2).Value = "选择";
            this.SheetView2.Columns.Get(0).CellType = generalCellType1;
            this.SheetView2.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.SheetView2.Columns.Get(0).Label = "项目描述";
            this.SheetView2.Columns.Get(0).Locked = true;
            this.SheetView2.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.SheetView2.Columns.Get(0).Width = 272F;
            this.SheetView2.Columns.Get(1).CellType = generalCellType2;
            this.SheetView2.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.SheetView2.Columns.Get(1).Label = "数据";
            this.SheetView2.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.SheetView2.Columns.Get(1).Width = 259F;
            buttonCellType1.ButtonColor2 = System.Drawing.SystemColors.ButtonFace;
            buttonCellType1.Text = "设置";
            this.SheetView2.Columns.Get(2).CellType = buttonCellType1;
            this.SheetView2.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.SheetView2.Columns.Get(2).Label = "选择";
            this.SheetView2.Columns.Get(2).Resizable = false;
            this.SheetView2.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.SheetView2.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.SheetView2.RowHeader.Columns.Default.Resizable = false;
            this.SheetView2.RowHeader.Visible = false;
            this.SheetView2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread2.SetActiveViewport(0, 1, 0);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fpSpread2);
            this.groupBox1.Location = new System.Drawing.Point(10, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(719, 433);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置上传数据信息";
            // 
            // tb_moduleName
            // 
            this.tb_moduleName.Location = new System.Drawing.Point(127, 14);
            this.tb_moduleName.Name = "tb_moduleName";
            this.tb_moduleName.ReadOnly = true;
            this.tb_moduleName.Size = new System.Drawing.Size(463, 21);
            this.tb_moduleName.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(13, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "根节点：";
            // 
            // tb_rootName
            // 
            this.tb_rootName.Location = new System.Drawing.Point(127, 49);
            this.tb_rootName.Name = "tb_rootName";
            this.tb_rootName.Size = new System.Drawing.Size(217, 21);
            this.tb_rootName.TabIndex = 18;
            // 
            // bt_preview
            // 
            this.bt_preview.Location = new System.Drawing.Point(415, 47);
            this.bt_preview.Name = "bt_preview";
            this.bt_preview.Size = new System.Drawing.Size(75, 23);
            this.bt_preview.TabIndex = 19;
            this.bt_preview.Text = "预览数据";
            this.bt_preview.UseVisualStyleBackColor = true;
            this.bt_preview.Click += new System.EventHandler(this.bt_preview_Click);
            // 
            // process1
            // 
            this.process1.StartInfo.Domain = "";
            this.process1.StartInfo.LoadUserProfile = false;
            this.process1.StartInfo.Password = null;
            this.process1.StartInfo.StandardErrorEncoding = null;
            this.process1.StartInfo.StandardOutputEncoding = null;
            this.process1.StartInfo.UserName = "";
            this.process1.SynchronizingObject = this;
            // 
            // GGCDocUploadSettingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 592);
            this.Controls.Add(this.bt_preview);
            this.Controls.Add(this.tb_rootName);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tb_moduleName);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GGCDocUploadSettingDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "工管中心文档上传设置";
            this.Load += new System.EventHandler(this.ItemInfoDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SheetView2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Button ButtonCancel;
        private FarPoint.Win.Spread.FpSpread fpSpread2;
        private FarPoint.Win.Spread.SheetView SheetView2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_moduleName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_rootName;
        private System.Windows.Forms.Button bt_preview;
        private System.Diagnostics.Process process1;
    }
}