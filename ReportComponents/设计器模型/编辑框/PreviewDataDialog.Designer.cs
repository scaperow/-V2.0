namespace ReportComponents
{
    partial class PreviewDataDialog
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
            this.Button_Close = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fpSpread = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread_DataView = new FarPoint.Win.Spread.SheetView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread_DataView)).BeginInit();
            this.SuspendLayout();
            // 
            // Button_Close
            // 
            this.Button_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Close.Location = new System.Drawing.Point(517, 487);
            this.Button_Close.Name = "Button_Close";
            this.Button_Close.Size = new System.Drawing.Size(89, 26);
            this.Button_Close.TabIndex = 1;
            this.Button_Close.Text = "关闭";
            this.Button_Close.UseVisualStyleBackColor = true;
            this.Button_Close.Click += new System.EventHandler(this.Button_Close_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.fpSpread);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(649, 477);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "预览数据（前50条数据）";
            // 
            // fpSpread
            // 
            this.fpSpread.AccessibleDescription = "";
            this.fpSpread.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread.Location = new System.Drawing.Point(3, 17);
            this.fpSpread.Name = "fpSpread";
            this.fpSpread.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread_DataView});
            this.fpSpread.Size = new System.Drawing.Size(643, 457);
            this.fpSpread.TabIndex = 0;
            this.fpSpread.TabStripInsertTab = false;
            this.fpSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpSpread_DataView
            // 
            this.fpSpread_DataView.Reset();
            this.fpSpread_DataView.SheetName = "Sheet1";
            this.fpSpread_DataView.ColumnCount = 5;
            this.fpSpread_DataView.RowCount = 0;
            this.fpSpread.SetActiveViewport(0, 1, 0);
            // 
            // PreviewDataDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 518);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Button_Close);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PreviewDataDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "预览窗口";
            this.Load += new System.EventHandler(this.PreviewDataDialog_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread_DataView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Button_Close;
        private System.Windows.Forms.GroupBox groupBox1;
        private FarPoint.Win.Spread.FpSpread fpSpread;
        private FarPoint.Win.Spread.SheetView fpSpread_DataView;
    }
}