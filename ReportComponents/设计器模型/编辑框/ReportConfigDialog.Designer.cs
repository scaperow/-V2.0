namespace ReportComponents
{
    partial class ReportConfigDialog
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
            this.ButtonOk = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.FpSpread = new FarPoint.Win.Spread.FpSpread();
            this.FpSpread_Panel = new FarPoint.Win.Spread.SheetView();
            this.Button_DelRow = new System.Windows.Forms.Button();
            this.Button_AddRow = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread_Panel)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
           
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
            this.FpSpread.Size = new System.Drawing.Size(657, 455);
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 14);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(740, 510);
            this.tabControl1.TabIndex = 15;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Button_AddRow);
            this.tabPage1.Controls.Add(this.Button_DelRow);
            this.tabPage1.Controls.Add(this.FpSpread);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(732, 484);
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
            this.groupBox2.Location = new System.Drawing.Point(8, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.groupBox2.Size = new System.Drawing.Size(746, 527);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            // 
            // CustomDataTableDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 578);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonOk);
            this.Name = "CustomDataTableDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "自定义数据表";
            this.Load += new System.EventHandler(this.CustomDataTableDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread_Panel)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Button ButtonCancel;
        private FarPoint.Win.Spread.FpSpread FpSpread;
        private FarPoint.Win.Spread.SheetView FpSpread_Panel;
        private System.Windows.Forms.Button Button_DelRow;
        private System.Windows.Forms.Button Button_AddRow;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;

    }
}