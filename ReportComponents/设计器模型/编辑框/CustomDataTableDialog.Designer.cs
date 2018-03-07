namespace ReportComponents
{
    partial class CustomDataTableDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomDataTableDialog));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Button_AddColumn = new System.Windows.Forms.ToolStripDropDownButton();
            this.MenuItem_String = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Integer = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Float = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Date = new System.Windows.Forms.ToolStripMenuItem();
            this.Button_RenameColumn = new System.Windows.Forms.ToolStripButton();
            this.Button_DeleteColumn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Button_ColumnOrder = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Button_FieldSetting = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.Button_ImportData = new System.Windows.Forms.ToolStripButton();
            this.Button_ExportData = new System.Windows.Forms.ToolStripButton();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.FpSpread = new FarPoint.Win.Spread.FpSpread();
            this.FpSpread_Panel = new FarPoint.Win.Spread.SheetView();
            this.Button_DelRow = new System.Windows.Forms.Button();
            this.Button_AddRow = new System.Windows.Forms.Button();
            this.TextBoxTableName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread_Panel)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Button_AddColumn,
            this.Button_RenameColumn,
            this.Button_DeleteColumn,
            this.toolStripSeparator1,
            this.Button_ColumnOrder,
            this.toolStripSeparator2,
            this.Button_FieldSetting,
            this.toolStripSeparator3,
            this.Button_ImportData,
            this.Button_ExportData});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(726, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Button_AddColumn
            // 
            this.Button_AddColumn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Button_AddColumn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_String,
            this.MenuItem_Integer,
            this.MenuItem_Float,
            this.MenuItem_Date});
            this.Button_AddColumn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_AddColumn.Name = "Button_AddColumn";
            this.Button_AddColumn.Size = new System.Drawing.Size(57, 22);
            this.Button_AddColumn.Text = "添加列";
            this.Button_AddColumn.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // MenuItem_String
            // 
            this.MenuItem_String.Name = "MenuItem_String";
            this.MenuItem_String.Size = new System.Drawing.Size(127, 22);
            this.MenuItem_String.Text = "字符串(S)";
            this.MenuItem_String.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // MenuItem_Integer
            // 
            this.MenuItem_Integer.Name = "MenuItem_Integer";
            this.MenuItem_Integer.Size = new System.Drawing.Size(127, 22);
            this.MenuItem_Integer.Text = "整型(I)";
            this.MenuItem_Integer.Visible = false;
            this.MenuItem_Integer.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // MenuItem_Float
            // 
            this.MenuItem_Float.Name = "MenuItem_Float";
            this.MenuItem_Float.Size = new System.Drawing.Size(127, 22);
            this.MenuItem_Float.Text = "小数(F)";
            this.MenuItem_Float.Visible = false;
            this.MenuItem_Float.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // MenuItem_Date
            // 
            this.MenuItem_Date.Name = "MenuItem_Date";
            this.MenuItem_Date.Size = new System.Drawing.Size(127, 22);
            this.MenuItem_Date.Text = "日期(D)";
            this.MenuItem_Date.Visible = false;
            this.MenuItem_Date.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // Button_RenameColumn
            // 
            this.Button_RenameColumn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Button_RenameColumn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_RenameColumn.Name = "Button_RenameColumn";
            this.Button_RenameColumn.Size = new System.Drawing.Size(60, 22);
            this.Button_RenameColumn.Text = "重命名列";
            this.Button_RenameColumn.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // Button_DeleteColumn
            // 
            this.Button_DeleteColumn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Button_DeleteColumn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_DeleteColumn.Name = "Button_DeleteColumn";
            this.Button_DeleteColumn.Size = new System.Drawing.Size(48, 22);
            this.Button_DeleteColumn.Text = "删除列";
            this.Button_DeleteColumn.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // Button_ColumnOrder
            // 
            this.Button_ColumnOrder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Button_ColumnOrder.Image = ((System.Drawing.Image)(resources.GetObject("Button_ColumnOrder.Image")));
            this.Button_ColumnOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_ColumnOrder.Name = "Button_ColumnOrder";
            this.Button_ColumnOrder.Size = new System.Drawing.Size(48, 22);
            this.Button_ColumnOrder.Text = "列排序";
            this.Button_ColumnOrder.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // Button_FieldSetting
            // 
            this.Button_FieldSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Button_FieldSetting.Image = ((System.Drawing.Image)(resources.GetObject("Button_FieldSetting.Image")));
            this.Button_FieldSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_FieldSetting.Name = "Button_FieldSetting";
            this.Button_FieldSetting.Size = new System.Drawing.Size(84, 22);
            this.Button_FieldSetting.Text = "选择模板字段";
            this.Button_FieldSetting.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // Button_ImportData
            // 
            this.Button_ImportData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Button_ImportData.Image = ((System.Drawing.Image)(resources.GetObject("Button_ImportData.Image")));
            this.Button_ImportData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_ImportData.Name = "Button_ImportData";
            this.Button_ImportData.Size = new System.Drawing.Size(72, 22);
            this.Button_ImportData.Text = "导入数据表";
            this.Button_ImportData.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // Button_ExportData
            // 
            this.Button_ExportData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Button_ExportData.Image = ((System.Drawing.Image)(resources.GetObject("Button_ExportData.Image")));
            this.Button_ExportData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_ExportData.Name = "Button_ExportData";
            this.Button_ExportData.Size = new System.Drawing.Size(72, 22);
            this.Button_ExportData.Text = "导出数据表";
            this.Button_ExportData.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // ButtonOk
            // 
            this.ButtonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonOk.Location = new System.Drawing.Point(581, 545);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(75, 23);
            this.ButtonOk.TabIndex = 8;
            this.ButtonOk.Text = "确定";
            this.ButtonOk.UseVisualStyleBackColor = true;
            this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonCancel.Location = new System.Drawing.Point(662, 545);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 9;
            this.ButtonCancel.Text = "取消";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // FpSpread
            // 
            this.FpSpread.AccessibleDescription = "";
            this.FpSpread.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FpSpread.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.FpSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.FpSpread.Location = new System.Drawing.Point(0, 28);
            this.FpSpread.Margin = new System.Windows.Forms.Padding(0);
            this.FpSpread.Name = "FpSpread";
            this.FpSpread.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.FpSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FpSpread_Panel});
            this.FpSpread.Size = new System.Drawing.Size(657, 431);
            this.FpSpread.TabIndex = 9;
            this.FpSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.FpSpread.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.FpSpread_CellClick);
            this.FpSpread.EditModeOff += new System.EventHandler(this.FpSpread_EditModeOff);
            // 
            // FpSpread_Panel
            // 
            this.FpSpread_Panel.Reset();
            this.FpSpread_Panel.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FpSpread_Panel.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FpSpread_Panel.ColumnCount = 0;
            this.FpSpread_Panel.RowCount = 0;
            this.FpSpread_Panel.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.FpSpread_Panel.RowHeader.Visible = false;
            this.FpSpread_Panel.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.FpSpread.SetActiveViewport(0, 1, 1);
            // 
            // Button_DelRow
            // 
            this.Button_DelRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_DelRow.Location = new System.Drawing.Point(661, 56);
            this.Button_DelRow.Name = "Button_DelRow";
            this.Button_DelRow.Size = new System.Drawing.Size(65, 23);
            this.Button_DelRow.TabIndex = 12;
            this.Button_DelRow.Text = "删除行";
            this.Button_DelRow.UseVisualStyleBackColor = true;
            this.Button_DelRow.Click += new System.EventHandler(this.Button_DelRow_Click);
            // 
            // Button_AddRow
            // 
            this.Button_AddRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_AddRow.Location = new System.Drawing.Point(661, 30);
            this.Button_AddRow.Name = "Button_AddRow";
            this.Button_AddRow.Size = new System.Drawing.Size(65, 23);
            this.Button_AddRow.TabIndex = 11;
            this.Button_AddRow.Text = "插入行";
            this.Button_AddRow.UseVisualStyleBackColor = true;
            this.Button_AddRow.Click += new System.EventHandler(this.Button_AddRow_Click);
            // 
            // TextBoxTableName
            // 
            this.TextBoxTableName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxTableName.Location = new System.Drawing.Point(50, 11);
            this.TextBoxTableName.Name = "TextBoxTableName";
            this.TextBoxTableName.Size = new System.Drawing.Size(704, 21);
            this.TextBoxTableName.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "名称：";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 14);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(740, 486);
            this.tabControl1.TabIndex = 15;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.toolStrip1);
            this.tabPage1.Controls.Add(this.Button_AddRow);
            this.tabPage1.Controls.Add(this.Button_DelRow);
            this.tabPage1.Controls.Add(this.FpSpread);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(732, 460);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.tabControl1);
            this.groupBox2.Location = new System.Drawing.Point(8, 36);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.groupBox2.Size = new System.Drawing.Size(746, 503);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(14, 549);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(321, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "说明：“.\"，\"{\"，\"}\"符号均为数据表定义保留符号。";
            // 
            // CustomDataTableDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 578);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.TextBoxTableName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonOk);
            this.Name = "CustomDataTableDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "自定义数据表";
            this.Load += new System.EventHandler(this.CustomDataTableDialog_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread_Panel)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.ToolStripDropDownButton Button_AddColumn;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_String;
        private System.Windows.Forms.ToolStripButton Button_RenameColumn;
        private System.Windows.Forms.ToolStripButton Button_DeleteColumn;
        private FarPoint.Win.Spread.FpSpread FpSpread;
        private FarPoint.Win.Spread.SheetView FpSpread_Panel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton Button_FieldSetting;
        private System.Windows.Forms.Button Button_DelRow;
        private System.Windows.Forms.Button Button_AddRow;
        private System.Windows.Forms.TextBox TextBoxTableName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Integer;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Float;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Date;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton Button_ImportData;
        private System.Windows.Forms.ToolStripButton Button_ExportData;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripButton Button_ColumnOrder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Label label2;
    }
}