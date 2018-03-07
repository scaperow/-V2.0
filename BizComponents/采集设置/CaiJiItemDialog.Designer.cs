namespace BizComponents
{
    partial class CaiJiItemDialog
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
            FarPoint.Win.Spread.CellType.ButtonCellType buttonCellType1 = new FarPoint.Win.Spread.CellType.ButtonCellType();
            FarPoint.Win.Spread.CellType.GeneralCellType generalCellType1 = new FarPoint.Win.Spread.CellType.GeneralCellType();
            FarPoint.Win.Spread.CellType.ButtonCellType buttonCellType2 = new FarPoint.Win.Spread.CellType.ButtonCellType();
            FarPoint.Win.Spread.CellType.GeneralCellType generalCellType2 = new FarPoint.Win.Spread.CellType.GeneralCellType();
            FarPoint.Win.Spread.CellType.ButtonCellType buttonCellType3 = new FarPoint.Win.Spread.CellType.ButtonCellType();
            FarPoint.Win.Spread.CellType.GeneralCellType generalCellType3 = new FarPoint.Win.Spread.CellType.GeneralCellType();
            FarPoint.Win.Spread.CellType.ButtonCellType buttonCellType4 = new FarPoint.Win.Spread.CellType.ButtonCellType();
            FarPoint.Win.Spread.CellType.ButtonCellType buttonCellType5 = new FarPoint.Win.Spread.CellType.ButtonCellType();
            FarPoint.Win.Spread.CellType.GeneralCellType generalCellType4 = new FarPoint.Win.Spread.CellType.GeneralCellType();
            FarPoint.Win.Spread.CellType.ButtonCellType buttonCellType6 = new FarPoint.Win.Spread.CellType.ButtonCellType();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.fpSpread2 = new FarPoint.Win.Spread.FpSpread();
            this.SheetView2 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.sheetView1 = new FarPoint.Win.Spread.SheetView();
            this.btn_AddDays = new System.Windows.Forms.Button();
            this.cb_active = new System.Windows.Forms.CheckBox();
            this.tb_moduleName = new System.Windows.Forms.TextBox();
            this.rb_ylj = new System.Windows.Forms.RadioButton();
            this.rb_wnj = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SheetView2)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择试验(&M)：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(13, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "试验类别：";
            // 
            // ButtonOk
            // 
            this.ButtonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonOk.Location = new System.Drawing.Point(564, 587);
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
            this.ButtonCancel.Location = new System.Drawing.Point(670, 587);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 7;
            this.ButtonCancel.Text = "取消";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // fpSpread2
            // 
            this.fpSpread2.AccessibleDescription = "fpSpread2, Sheet1";
            this.fpSpread2.BackColor = System.Drawing.SystemColors.Control;
            this.fpSpread2.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpSpread2.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread2.Location = new System.Drawing.Point(3, 19);
            this.fpSpread2.Name = "fpSpread2";
            this.fpSpread2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread2.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpSpread2.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.SheetView2});
            this.fpSpread2.Size = new System.Drawing.Size(759, 421);
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
            this.SheetView2.ColumnCount = 8;
            this.SheetView2.RowCount = 0;
            this.SheetView2.ColumnHeader.Cells.Get(0, 0).Value = "删除";
            this.SheetView2.ColumnHeader.Cells.Get(0, 1).Value = "序号";
            this.SheetView2.ColumnHeader.Cells.Get(0, 2).Value = "屈服力";
            this.SheetView2.ColumnHeader.Cells.Get(0, 3).Value = "选择";
            this.SheetView2.ColumnHeader.Cells.Get(0, 4).Value = "拉断最大力";
            this.SheetView2.ColumnHeader.Cells.Get(0, 5).Value = "选择";
            this.SheetView2.ColumnHeader.Cells.Get(0, 6).Value = "断后标距";
            this.SheetView2.ColumnHeader.Cells.Get(0, 7).Value = "选择";
            buttonCellType1.ButtonColor2 = System.Drawing.SystemColors.ButtonFace;
            buttonCellType1.Text = "删除";
            this.SheetView2.Columns.Get(0).CellType = buttonCellType1;
            this.SheetView2.Columns.Get(0).Label = "删除";
            this.SheetView2.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.SheetView2.Columns.Get(1).Label = "序号";
            this.SheetView2.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.SheetView2.Columns.Get(2).CellType = generalCellType1;
            this.SheetView2.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.SheetView2.Columns.Get(2).Label = "屈服力";
            this.SheetView2.Columns.Get(2).Locked = true;
            this.SheetView2.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.SheetView2.Columns.Get(2).Width = 127F;
            buttonCellType2.ButtonColor2 = System.Drawing.SystemColors.ButtonFace;
            buttonCellType2.Text = "设置";
            this.SheetView2.Columns.Get(3).CellType = buttonCellType2;
            this.SheetView2.Columns.Get(3).Label = "选择";
            this.SheetView2.Columns.Get(4).CellType = generalCellType2;
            this.SheetView2.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.SheetView2.Columns.Get(4).Label = "拉断最大力";
            this.SheetView2.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.SheetView2.Columns.Get(4).Width = 121F;
            buttonCellType3.ButtonColor2 = System.Drawing.SystemColors.ButtonFace;
            buttonCellType3.Text = "设置";
            this.SheetView2.Columns.Get(5).CellType = buttonCellType3;
            this.SheetView2.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.SheetView2.Columns.Get(5).Label = "选择";
            this.SheetView2.Columns.Get(6).CellType = generalCellType3;
            this.SheetView2.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.SheetView2.Columns.Get(6).Label = "断后标距";
            this.SheetView2.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.SheetView2.Columns.Get(6).Width = 121F;
            buttonCellType4.ButtonColor2 = System.Drawing.SystemColors.ButtonFace;
            buttonCellType4.Text = "设置";
            this.SheetView2.Columns.Get(7).CellType = buttonCellType4;
            this.SheetView2.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.SheetView2.Columns.Get(7).Label = "选择";
            this.SheetView2.Columns.Get(7).Resizable = false;
            this.SheetView2.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.SheetView2.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.SheetView2.RowHeader.Columns.Default.Resizable = false;
            this.SheetView2.RowHeader.Visible = false;
            this.SheetView2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread2.SetActiveViewport(0, 1, 0);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fpSpread1);
            this.groupBox1.Controls.Add(this.fpSpread2);
            this.groupBox1.Location = new System.Drawing.Point(10, 120);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(765, 446);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置试验数据更新单元格信息";
            // 
            // fpSpread1
            // 
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1";
            this.fpSpread1.BackColor = System.Drawing.SystemColors.Control;
            this.fpSpread1.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.Location = new System.Drawing.Point(3, 13);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.sheetView1});
            this.fpSpread1.Size = new System.Drawing.Size(759, 421);
            this.fpSpread1.TabIndex = 11;
            this.fpSpread1.TabStripInsertTab = false;
            this.fpSpread1.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpSpread2_ButtonClicked);
            // 
            // sheetView1
            // 
            this.sheetView1.Reset();
            this.sheetView1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.sheetView1.ColumnCount = 4;
            this.sheetView1.RowCount = 0;
            this.sheetView1.ColumnHeader.Cells.Get(0, 0).Value = "删除";
            this.sheetView1.ColumnHeader.Cells.Get(0, 1).Value = "序号";
            this.sheetView1.ColumnHeader.Cells.Get(0, 2).Value = "破坏荷载";
            this.sheetView1.ColumnHeader.Cells.Get(0, 3).Value = "选择";
            buttonCellType5.ButtonColor2 = System.Drawing.SystemColors.ButtonFace;
            buttonCellType5.Text = "删除";
            this.sheetView1.Columns.Get(0).CellType = buttonCellType5;
            this.sheetView1.Columns.Get(0).Label = "删除";
            this.sheetView1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.Columns.Get(1).Label = "序号";
            this.sheetView1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(2).CellType = generalCellType4;
            this.sheetView1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.Columns.Get(2).Label = "破坏荷载";
            this.sheetView1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(2).Width = 162F;
            buttonCellType6.ButtonColor2 = System.Drawing.SystemColors.ButtonFace;
            buttonCellType6.Text = "设置";
            this.sheetView1.Columns.Get(3).CellType = buttonCellType6;
            this.sheetView1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.Columns.Get(3).Label = "选择";
            this.sheetView1.Columns.Get(3).Resizable = false;
            this.sheetView1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.sheetView1.RowHeader.Columns.Default.Resizable = false;
            this.sheetView1.RowHeader.Visible = false;
            this.sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread1.SetActiveViewport(0, 1, 0);
            // 
            // btn_AddDays
            // 
            this.btn_AddDays.Location = new System.Drawing.Point(691, 100);
            this.btn_AddDays.Name = "btn_AddDays";
            this.btn_AddDays.Size = new System.Drawing.Size(75, 23);
            this.btn_AddDays.TabIndex = 12;
            this.btn_AddDays.Text = "添加";
            this.btn_AddDays.UseVisualStyleBackColor = true;
            this.btn_AddDays.Click += new System.EventHandler(this.btn_AddDays_Click);
            // 
            // cb_active
            // 
            this.cb_active.AutoSize = true;
            this.cb_active.Location = new System.Drawing.Point(622, 16);
            this.cb_active.Name = "cb_active";
            this.cb_active.Size = new System.Drawing.Size(48, 16);
            this.cb_active.TabIndex = 20;
            this.cb_active.Text = "启用";
            this.cb_active.UseVisualStyleBackColor = true;
            // 
            // tb_moduleName
            // 
            this.tb_moduleName.Location = new System.Drawing.Point(127, 14);
            this.tb_moduleName.Name = "tb_moduleName";
            this.tb_moduleName.ReadOnly = true;
            this.tb_moduleName.Size = new System.Drawing.Size(489, 21);
            this.tb_moduleName.TabIndex = 15;
            // 
            // rb_ylj
            // 
            this.rb_ylj.AutoSize = true;
            this.rb_ylj.Location = new System.Drawing.Point(128, 47);
            this.rb_ylj.Name = "rb_ylj";
            this.rb_ylj.Size = new System.Drawing.Size(59, 16);
            this.rb_ylj.TabIndex = 21;
            this.rb_ylj.TabStop = true;
            this.rb_ylj.Text = "压力机";
            this.rb_ylj.UseVisualStyleBackColor = true;
            this.rb_ylj.CheckedChanged += new System.EventHandler(this.rb_ylj_CheckedChanged);
            // 
            // rb_wnj
            // 
            this.rb_wnj.AutoSize = true;
            this.rb_wnj.Location = new System.Drawing.Point(214, 47);
            this.rb_wnj.Name = "rb_wnj";
            this.rb_wnj.Size = new System.Drawing.Size(59, 16);
            this.rb_wnj.TabIndex = 21;
            this.rb_wnj.TabStop = true;
            this.rb_wnj.Text = "万能机";
            this.rb_wnj.UseVisualStyleBackColor = true;
            this.rb_wnj.CheckedChanged += new System.EventHandler(this.rb_ylj_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(296, 47);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(47, 16);
            this.radioButton1.TabIndex = 21;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "其它";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.rb_ylj_CheckedChanged);
            // 
            // CaiJiItemDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 654);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.rb_wnj);
            this.Controls.Add(this.rb_ylj);
            this.Controls.Add(this.btn_AddDays);
            this.Controls.Add(this.cb_active);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tb_moduleName);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CaiJiItemDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "采集设置";
            this.Load += new System.EventHandler(this.ItemInfoDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SheetView2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Button ButtonCancel;
        private FarPoint.Win.Spread.FpSpread fpSpread2;
        private FarPoint.Win.Spread.SheetView SheetView2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_AddDays;
        private System.Windows.Forms.CheckBox cb_active;
        private System.Windows.Forms.TextBox tb_moduleName;
        private System.Windows.Forms.RadioButton rb_ylj;
        private System.Windows.Forms.RadioButton rb_wnj;
        private System.Windows.Forms.RadioButton radioButton1;
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView sheetView1;
    }
}