namespace BizComponents
{
    partial class ItemInfoDialog
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
            FarPoint.Win.Spread.CellType.ButtonCellType buttonCellType2 = new FarPoint.Win.Spread.CellType.ButtonCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.ButtonCellType buttonCellType3 = new FarPoint.Win.Spread.CellType.ButtonCellType();
            FarPoint.Win.Spread.CellType.ButtonCellType buttonCellType4 = new FarPoint.Win.Spread.CellType.ButtonCellType();
            FarPoint.Win.Spread.CellType.ButtonCellType buttonCellType5 = new FarPoint.Win.Spread.CellType.ButtonCellType();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.fpSpread2 = new FarPoint.Win.Spread.FpSpread();
            this.Sheet_Columns = new FarPoint.Win.Spread.SheetView();
            this.txtStadiumRange = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_AddDays = new System.Windows.Forms.Button();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.sheetView1 = new FarPoint.Win.Spread.SheetView();
            this.label8 = new System.Windows.Forms.Label();
            this.cb_active = new System.Windows.Forms.CheckBox();
            this.tb_moduleName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTemperature = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sheet_Columns)).BeginInit();
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
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "龄期提醒范围：";
            // 
            // ButtonOk
            // 
            this.ButtonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonOk.Location = new System.Drawing.Point(564, 587);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(75, 23);
            this.ButtonOk.TabIndex = 6;
            this.ButtonOk.Text = "确定";
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
            this.fpSpread2.AccessibleDescription = "fpSpread2, Sheet1, Row 0, Column 0, 批号";
            this.fpSpread2.BackColor = System.Drawing.SystemColors.Control;
            this.fpSpread2.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpSpread2.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread2.Location = new System.Drawing.Point(3, 19);
            this.fpSpread2.Name = "fpSpread2";
            this.fpSpread2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread2.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpSpread2.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.Sheet_Columns});
            this.fpSpread2.Size = new System.Drawing.Size(759, 198);
            this.fpSpread2.TabIndex = 10;
            this.fpSpread2.TabStripInsertTab = false;
            this.fpSpread2.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            this.fpSpread2.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread2.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpSpread2_ButtonClicked);
            // 
            // Sheet_Columns
            // 
            this.Sheet_Columns.Reset();
            this.Sheet_Columns.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.Sheet_Columns.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.Sheet_Columns.ColumnCount = 3;
            this.Sheet_Columns.RowCount = 8;
            this.Sheet_Columns.Cells.Get(0, 0).Value = "批号";
            this.Sheet_Columns.Cells.Get(1, 0).Value = "制件日期";
            this.Sheet_Columns.Cells.Get(2, 0).Value = "试件编号";
            this.Sheet_Columns.Cells.Get(3, 0).Value = "试件尺寸";
            this.Sheet_Columns.Cells.Get(4, 0).Locked = false;
            this.Sheet_Columns.Cells.Get(4, 0).Value = "报告编号";
            this.Sheet_Columns.Cells.Get(5, 0).Locked = true;
            this.Sheet_Columns.Cells.Get(5, 0).Value = "委托编号";
            this.Sheet_Columns.Cells.Get(6, 0).Value = "附加信息";
            this.Sheet_Columns.Cells.Get(7, 0).ParseFormatString = "G";
            this.Sheet_Columns.Cells.Get(7, 0).Value = "代表数量";
            this.Sheet_Columns.ColumnHeader.Cells.Get(0, 0).Value = "列描述";
            this.Sheet_Columns.ColumnHeader.Cells.Get(0, 1).Value = "数据区";
            this.Sheet_Columns.ColumnHeader.Cells.Get(0, 2).Value = "功能";
            this.Sheet_Columns.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.Sheet_Columns.Columns.Get(0).Label = "列描述";
            this.Sheet_Columns.Columns.Get(0).Locked = true;
            this.Sheet_Columns.Columns.Get(0).Resizable = false;
            this.Sheet_Columns.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.Sheet_Columns.Columns.Get(0).Width = 230F;
            this.Sheet_Columns.Columns.Get(1).Label = "数据区";
            this.Sheet_Columns.Columns.Get(1).Width = 450F;
            buttonCellType1.ButtonColor2 = System.Drawing.SystemColors.ButtonFace;
            buttonCellType1.Text = "设置";
            this.Sheet_Columns.Columns.Get(2).CellType = buttonCellType1;
            this.Sheet_Columns.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.Sheet_Columns.Columns.Get(2).Label = "功能";
            this.Sheet_Columns.Columns.Get(2).Resizable = false;
            this.Sheet_Columns.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.Sheet_Columns.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.Sheet_Columns.RowHeader.Columns.Default.Resizable = false;
            this.Sheet_Columns.RowHeader.Visible = false;
            this.Sheet_Columns.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // txtStadiumRange
            // 
            this.txtStadiumRange.Location = new System.Drawing.Point(128, 44);
            this.txtStadiumRange.Name = "txtStadiumRange";
            this.txtStadiumRange.Size = new System.Drawing.Size(157, 21);
            this.txtStadiumRange.TabIndex = 15;
            this.txtStadiumRange.Text = "24";
            this.txtStadiumRange.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStadiumRange_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(334, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(305, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "说明：24表示仅在龄期到期后24小时内提醒；以此类推。";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_AddDays);
            this.groupBox1.Controls.Add(this.fpSpread1);
            this.groupBox1.Controls.Add(this.fpSpread2);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(10, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(765, 468);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置提醒显示列";
            // 
            // btn_AddDays
            // 
            this.btn_AddDays.Location = new System.Drawing.Point(5, 223);
            this.btn_AddDays.Name = "btn_AddDays";
            this.btn_AddDays.Size = new System.Drawing.Size(75, 23);
            this.btn_AddDays.TabIndex = 12;
            this.btn_AddDays.Text = "添加龄期天数";
            this.btn_AddDays.UseVisualStyleBackColor = true;
            this.btn_AddDays.Click += new System.EventHandler(this.btn_AddDays_Click);
            // 
            // fpSpread1
            // 
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1";
            this.fpSpread1.BackColor = System.Drawing.SystemColors.Control;
            this.fpSpread1.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.Location = new System.Drawing.Point(5, 252);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.sheetView1});
            this.fpSpread1.Size = new System.Drawing.Size(759, 208);
            this.fpSpread1.TabIndex = 11;
            this.fpSpread1.TabStripInsertTab = false;
            this.fpSpread1.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpSpread1_ButtonClicked);
            // 
            // sheetView1
            // 
            this.sheetView1.Reset();
            this.sheetView1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.sheetView1.ColumnCount = 9;
            this.sheetView1.RowCount = 0;
            this.sheetView1.ColumnHeader.Cells.Get(0, 0).Value = "龄期天数";
            this.sheetView1.ColumnHeader.Cells.Get(0, 1).Value = "实验项目";
            this.sheetView1.ColumnHeader.Cells.Get(0, 3).Value = "是否变量";
            this.sheetView1.ColumnHeader.Cells.Get(0, 4).Value = "龄期变量";
            this.sheetView1.ColumnHeader.Cells.Get(0, 6).Value = "验证数据来源";
            this.sheetView1.ColumnHeader.Cells.Get(0, 8).Value = "删除";
            this.sheetView1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.Columns.Get(0).Label = "龄期天数";
            this.sheetView1.Columns.Get(0).Locked = false;
            this.sheetView1.Columns.Get(0).Resizable = false;
            this.sheetView1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(1).Label = "实验项目";
            this.sheetView1.Columns.Get(1).Width = 120F;
            buttonCellType2.ButtonColor2 = System.Drawing.SystemColors.ButtonFace;
            buttonCellType2.Text = "设置";
            this.sheetView1.Columns.Get(2).CellType = buttonCellType2;
            this.sheetView1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.Columns.Get(2).Resizable = false;
            this.sheetView1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(3).CellType = checkBoxCellType1;
            this.sheetView1.Columns.Get(3).Label = "是否变量";
            this.sheetView1.Columns.Get(3).Locked = false;
            this.sheetView1.Columns.Get(3).Resizable = false;
            this.sheetView1.Columns.Get(4).Label = "龄期变量";
            this.sheetView1.Columns.Get(4).Width = 120F;
            buttonCellType3.ButtonColor2 = System.Drawing.SystemColors.ButtonFace;
            buttonCellType3.Text = "设置";
            this.sheetView1.Columns.Get(5).CellType = buttonCellType3;
            this.sheetView1.Columns.Get(6).Label = "验证数据来源";
            this.sheetView1.Columns.Get(6).Width = 120F;
            buttonCellType4.ButtonColor2 = System.Drawing.SystemColors.ButtonFace;
            buttonCellType4.Text = "设置";
            this.sheetView1.Columns.Get(7).CellType = buttonCellType4;
            buttonCellType5.ButtonColor2 = System.Drawing.SystemColors.ButtonFace;
            buttonCellType5.Text = "删除";
            this.sheetView1.Columns.Get(8).CellType = buttonCellType5;
            this.sheetView1.Columns.Get(8).Label = "删除";
            this.sheetView1.Columns.Get(8).Width = 55F;
            this.sheetView1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.sheetView1.RowHeader.Columns.Default.Resizable = true;
            this.sheetView1.RowHeader.Columns.Get(0).Width = 34F;
            this.sheetView1.RowHeader.Visible = false;
            this.sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread1.SetActiveViewport(0, 1, 0);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(86, 228);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(209, 12);
            this.label8.TabIndex = 16;
            this.label8.Text = "说明：负数表示小时，正数代表天数。";
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(291, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "小时 ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(12, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "同条件温度：";
            // 
            // txtTemperature
            // 
            this.txtTemperature.Location = new System.Drawing.Point(127, 71);
            this.txtTemperature.Name = "txtTemperature";
            this.txtTemperature.Size = new System.Drawing.Size(157, 21);
            this.txtTemperature.TabIndex = 15;
            this.txtTemperature.Text = "0";
            this.txtTemperature.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStadiumRange_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(333, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(281, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "说明：表示温度累积需要达到的数值,0表示不设置。";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(290, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "摄氏度";
            // 
            // ItemInfoDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 654);
            this.Controls.Add(this.cb_active);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tb_moduleName);
            this.Controls.Add(this.txtTemperature);
            this.Controls.Add(this.txtStadiumRange);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ItemInfoDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "属性设置";
            this.Load += new System.EventHandler(this.ItemInfoDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sheet_Columns)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private FarPoint.Win.Spread.SheetView Sheet_Columns;
        private System.Windows.Forms.TextBox txtStadiumRange;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView sheetView1;
        private System.Windows.Forms.Button btn_AddDays;
        private System.Windows.Forms.CheckBox cb_active;
        private System.Windows.Forms.TextBox tb_moduleName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTemperature;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}