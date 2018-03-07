namespace BizComponents
{
    partial class InvalidReportNote
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
            this.tb_note = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.btnAddImage = new System.Windows.Forms.Button();
            this.bt_cancel = new System.Windows.Forms.Button();
            this.bt_save = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // tb_note
            // 
            this.tb_note.Location = new System.Drawing.Point(26, 27);
            this.tb_note.Multiline = true;
            this.tb_note.Name = "tb_note";
            this.tb_note.Size = new System.Drawing.Size(705, 181);
            this.tb_note.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.fpSpread1);
            this.panel1.Controls.Add(this.btnAddImage);
            this.panel1.Location = new System.Drawing.Point(26, 230);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(705, 125);
            this.panel1.TabIndex = 2;
            // 
            // fpSpread1
            // 
            this.fpSpread1.AccessibleDescription = "";
            this.fpSpread1.AllowUserZoom = false;
            this.fpSpread1.AutoClipboard = false;
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.fpSpread1.Location = new System.Drawing.Point(3, 13);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(618, 100);
            this.fpSpread1.TabIndex = 3;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread1_CellDoubleClick);
            this.fpSpread1.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpSpread1_ButtonClicked);
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 3;
            this.fpSpread1_Sheet1.RowCount = 0;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "图像编号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "图像名称";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "删除";
            this.fpSpread1_Sheet1.Columns.Get(0).Label = "图像编号";
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 200F;
            this.fpSpread1_Sheet1.Columns.Get(1).Label = "图像名称";
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 300F;
            buttonCellType1.ButtonColor2 = System.Drawing.SystemColors.ButtonFace;
            buttonCellType1.Text = "删除";
            this.fpSpread1_Sheet1.Columns.Get(2).CellType = buttonCellType1;
            this.fpSpread1_Sheet1.Columns.Get(2).Label = "删除";
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread1.SetActiveViewport(0, 1, 0);
            // 
            // btnAddImage
            // 
            this.btnAddImage.Location = new System.Drawing.Point(627, 13);
            this.btnAddImage.Name = "btnAddImage";
            this.btnAddImage.Size = new System.Drawing.Size(75, 23);
            this.btnAddImage.TabIndex = 1;
            this.btnAddImage.Text = "添加";
            this.btnAddImage.UseVisualStyleBackColor = true;
            this.btnAddImage.Click += new System.EventHandler(this.btnAddImage_Click);
            // 
            // bt_cancel
            // 
            this.bt_cancel.Location = new System.Drawing.Point(396, 361);
            this.bt_cancel.Name = "bt_cancel";
            this.bt_cancel.Size = new System.Drawing.Size(75, 23);
            this.bt_cancel.TabIndex = 1;
            this.bt_cancel.Text = "取消";
            this.bt_cancel.UseVisualStyleBackColor = true;
            this.bt_cancel.Click += new System.EventHandler(this.bt_cancel_Click);
            // 
            // bt_save
            // 
            this.bt_save.Location = new System.Drawing.Point(300, 361);
            this.bt_save.Name = "bt_save";
            this.bt_save.Size = new System.Drawing.Size(75, 23);
            this.bt_save.TabIndex = 1;
            this.bt_save.Text = "保存";
            this.bt_save.UseVisualStyleBackColor = true;
            this.bt_save.Click += new System.EventHandler(this.bt_save_Click);
            // 
            // InvalidReportNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 406);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bt_save);
            this.Controls.Add(this.bt_cancel);
            this.Controls.Add(this.tb_note);
            this.Name = "InvalidReportNote";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "原因分析";
            this.Load += new System.EventHandler(this.InvalidReportNote_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_note;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAddImage;
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private System.Windows.Forms.Button bt_cancel;
        private System.Windows.Forms.Button bt_save;
    }
}