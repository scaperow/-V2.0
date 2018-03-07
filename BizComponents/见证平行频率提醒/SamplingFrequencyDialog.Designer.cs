namespace BizComponents
{
    partial class SamplingFrequencyDialog
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.ModelList = new FarPoint.Win.Spread.SheetView();
            this.Button_Save = new System.Windows.Forms.Button();
            this.ButtonExit = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ModelList)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(8, 10);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(708, 498);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.fpSpread1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(700, 472);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "抽检频率设置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // fpSpread1
            // 
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.fpSpread1.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.Location = new System.Drawing.Point(3, 3);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpSpread1.SelectionBlockOptions = FarPoint.Win.Spread.SelectionBlockOptions.Cells;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.ModelList});
            this.fpSpread1.Size = new System.Drawing.Size(694, 466);
            this.fpSpread1.TabIndex = 1;
            this.fpSpread1.TabStripInsertTab = false;
            this.fpSpread1.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // ModelList
            // 
            this.ModelList.Reset();
            this.ModelList.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.ModelList.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.ModelList.ColumnCount = 3;
            this.ModelList.ColumnHeader.Cells.Get(0, 0).Value = "试验模板";
            this.ModelList.ColumnHeader.Cells.Get(0, 1).Value = "见证频率(%)";
            this.ModelList.ColumnHeader.Cells.Get(0, 2).Value = "平行频率(%)";
            this.ModelList.Columns.Get(0).CellType = textCellType1;
            this.ModelList.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.ModelList.Columns.Get(0).Label = "试验模板";
            this.ModelList.Columns.Get(0).Locked = true;
            this.ModelList.Columns.Get(0).Resizable = false;
            this.ModelList.Columns.Get(0).ShowSortIndicator = false;
            this.ModelList.Columns.Get(0).TabStop = false;
            this.ModelList.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.ModelList.Columns.Get(0).Width = 341F;
            numberCellType1.MaximumValue = 100;
            numberCellType1.MinimumValue = 0;
            numberCellType1.NullDisplay = "0";
            this.ModelList.Columns.Get(1).CellType = numberCellType1;
            this.ModelList.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.ModelList.Columns.Get(1).Label = "见证频率(%)";
            this.ModelList.Columns.Get(1).Resizable = false;
            this.ModelList.Columns.Get(1).ShowSortIndicator = false;
            this.ModelList.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.ModelList.Columns.Get(1).Width = 93F;
            numberCellType2.MaximumValue = 100;
            numberCellType2.MinimumValue = 0;
            numberCellType2.NullDisplay = "0";
            this.ModelList.Columns.Get(2).CellType = numberCellType2;
            this.ModelList.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.ModelList.Columns.Get(2).Label = "平行频率(%)";
            this.ModelList.Columns.Get(2).Resizable = false;
            this.ModelList.Columns.Get(2).ShowSortIndicator = false;
            this.ModelList.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.ModelList.Columns.Get(2).Width = 85F;
            this.ModelList.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.ModelList.RowHeader.Visible = false;
            this.ModelList.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // Button_Save
            // 
            this.Button_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Save.Location = new System.Drawing.Point(520, 517);
            this.Button_Save.Name = "Button_Save";
            this.Button_Save.Size = new System.Drawing.Size(75, 23);
            this.Button_Save.TabIndex = 1;
            this.Button_Save.Text = "保存设置";
            this.Button_Save.UseVisualStyleBackColor = true;
            this.Button_Save.Click += new System.EventHandler(this.Button_Save_Click);
            // 
            // ButtonExit
            // 
            this.ButtonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonExit.Location = new System.Drawing.Point(602, 517);
            this.ButtonExit.Name = "ButtonExit";
            this.ButtonExit.Size = new System.Drawing.Size(75, 23);
            this.ButtonExit.TabIndex = 2;
            this.ButtonExit.Text = "关闭";
            this.ButtonExit.UseVisualStyleBackColor = true;
            this.ButtonExit.Click += new System.EventHandler(this.ButtonExit_Click);
            // 
            // SamplingFrequencyDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 551);
            this.Controls.Add(this.ButtonExit);
            this.Controls.Add(this.Button_Save);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SamplingFrequencyDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "监理抽检频率设置";
            this.Load += new System.EventHandler(this.SamplingFrequencyDialog_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ModelList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button Button_Save;
        private System.Windows.Forms.Button ButtonExit;
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView ModelList;
    }
}