using Yqun.Client.BizUI;
namespace ReportComponents
{
    partial class ConstantDataTableDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConstantDataTableDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.TextBoxTableName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.FpSpread = new Yqun.Client.BizUI.MyCell();
            this.FpSpread_Panel = new FarPoint.Win.Spread.SheetView();
            this.Button_DelRow = new System.Windows.Forms.Button();
            this.Button_AddRow = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Button_AddColumn = new System.Windows.Forms.ToolStripDropDownButton();
            this.MenuItem_String = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Integer = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Float = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Date = new System.Windows.Forms.ToolStripMenuItem();
            this.Button_RenameColumn = new System.Windows.Forms.ToolStripButton();
            this.Button_DeleteColumn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Button_ModelField = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Button_ImportData = new System.Windows.Forms.ToolStripButton();
            this.Button_ExportData = new System.Windows.Forms.ToolStripButton();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread_Panel)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称：";
            // 
            // TextBoxTableName
            // 
            this.TextBoxTableName.Location = new System.Drawing.Point(48, 11);
            this.TextBoxTableName.Name = "TextBoxTableName";
            this.TextBoxTableName.Size = new System.Drawing.Size(687, 21);
            this.TextBoxTableName.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Location = new System.Drawing.Point(6, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox1.Size = new System.Drawing.Size(729, 517);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 14);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(729, 503);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.FpSpread);
            this.tabPage1.Controls.Add(this.Button_DelRow);
            this.tabPage1.Controls.Add(this.Button_AddRow);
            this.tabPage1.Controls.Add(this.toolStrip1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(721, 477);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // FpSpread
            // 
            this.FpSpread.AccessibleDescription = "";
            this.FpSpread.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FpSpread.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.FpSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.FpSpread.Location = new System.Drawing.Point(2, 28);
            this.FpSpread.Name = "FpSpread";
            this.FpSpread.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.FpSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FpSpread_Panel});
            this.FpSpread.Size = new System.Drawing.Size(646, 448);
            this.FpSpread.TabIndex = 6;
            this.FpSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
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
            this.FpSpread_Panel.PrintInfo.PaperSize = ((System.Drawing.Printing.PaperSize)(resources.GetObject("resource.PaperSize")));
            this.FpSpread_Panel.RowHeader.Visible = false;
            this.FpSpread_Panel.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.FpSpread.SetActiveViewport(0, 1, 1);
            // 
            // Button_DelRow
            // 
            this.Button_DelRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_DelRow.Location = new System.Drawing.Point(651, 54);
            this.Button_DelRow.Name = "Button_DelRow";
            this.Button_DelRow.Size = new System.Drawing.Size(65, 23);
            this.Button_DelRow.TabIndex = 5;
            this.Button_DelRow.Text = "删除行";
            this.Button_DelRow.UseVisualStyleBackColor = true;
            this.Button_DelRow.Click += new System.EventHandler(this.Button_DelRow_Click);
            // 
            // Button_AddRow
            // 
            this.Button_AddRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_AddRow.Location = new System.Drawing.Point(651, 31);
            this.Button_AddRow.Name = "Button_AddRow";
            this.Button_AddRow.Size = new System.Drawing.Size(65, 23);
            this.Button_AddRow.TabIndex = 4;
            this.Button_AddRow.Text = "插入行";
            this.Button_AddRow.UseVisualStyleBackColor = true;
            this.Button_AddRow.Click += new System.EventHandler(this.Button_AddRow_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Button_AddColumn,
            this.Button_RenameColumn,
            this.Button_DeleteColumn,
            this.toolStripSeparator1,
            this.Button_ModelField,
            this.toolStripSeparator2,
            this.Button_ImportData,
            this.Button_ExportData});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(715, 25);
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
            this.Button_AddColumn.Image = ((System.Drawing.Image)(resources.GetObject("Button_AddColumn.Image")));
            this.Button_AddColumn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_AddColumn.Name = "Button_AddColumn";
            this.Button_AddColumn.Size = new System.Drawing.Size(57, 22);
            this.Button_AddColumn.Text = "添加列";
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
            this.MenuItem_Integer.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // MenuItem_Float
            // 
            this.MenuItem_Float.Name = "MenuItem_Float";
            this.MenuItem_Float.Size = new System.Drawing.Size(127, 22);
            this.MenuItem_Float.Text = "小数(F)";
            this.MenuItem_Float.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // MenuItem_Date
            // 
            this.MenuItem_Date.Name = "MenuItem_Date";
            this.MenuItem_Date.Size = new System.Drawing.Size(127, 22);
            this.MenuItem_Date.Text = "日期(D)";
            this.MenuItem_Date.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // Button_RenameColumn
            // 
            this.Button_RenameColumn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Button_RenameColumn.Image = ((System.Drawing.Image)(resources.GetObject("Button_RenameColumn.Image")));
            this.Button_RenameColumn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_RenameColumn.Name = "Button_RenameColumn";
            this.Button_RenameColumn.Size = new System.Drawing.Size(60, 22);
            this.Button_RenameColumn.Text = "重命名列";
            this.Button_RenameColumn.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // Button_DeleteColumn
            // 
            this.Button_DeleteColumn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Button_DeleteColumn.Image = ((System.Drawing.Image)(resources.GetObject("Button_DeleteColumn.Image")));
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
            // Button_ModelField
            // 
            this.Button_ModelField.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Button_ModelField.Image = ((System.Drawing.Image)(resources.GetObject("Button_ModelField.Image")));
            this.Button_ModelField.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_ModelField.Name = "Button_ModelField";
            this.Button_ModelField.Size = new System.Drawing.Size(84, 22);
            this.Button_ModelField.Text = "选择模板字段";
            this.Button_ModelField.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            this.ButtonOk.Location = new System.Drawing.Point(551, 557);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(75, 23);
            this.ButtonOk.TabIndex = 3;
            this.ButtonOk.Text = "确定";
            this.ButtonOk.UseVisualStyleBackColor = true;
            this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(632, 557);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 4;
            this.ButtonCancel.Text = "取消";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // ConstantDataTableDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 588);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.TextBoxTableName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConstantDataTableDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "内置数据表";
            this.Load += new System.EventHandler(this.ConstantDataTableDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread_Panel)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextBoxTableName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private MyCell FpSpread;
        private FarPoint.Win.Spread.SheetView FpSpread_Panel;
        private System.Windows.Forms.Button Button_DelRow;
        private System.Windows.Forms.Button Button_AddRow;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton Button_AddColumn;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_String;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Integer;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Float;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Date;
        private System.Windows.Forms.ToolStripButton Button_RenameColumn;
        private System.Windows.Forms.ToolStripButton Button_DeleteColumn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton Button_ImportData;
        private System.Windows.Forms.ToolStripButton Button_ExportData;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripButton Button_ModelField;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}