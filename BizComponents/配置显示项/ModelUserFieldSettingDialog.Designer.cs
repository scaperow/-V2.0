namespace BizComponents
{
    partial class ModelUserFieldSettingDialog
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.AppendButton = new System.Windows.Forms.ToolStripButton();
            this.RemoveButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MovePrevButton = new System.Windows.Forms.ToolStripButton();
            this.MoveNextButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Button_Exit = new System.Windows.Forms.Button();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.bt_save = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AppendButton,
            this.RemoveButton,
            this.toolStripSeparator1,
            this.MovePrevButton,
            this.MoveNextButton,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(657, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // AppendButton
            // 
            this.AppendButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.AppendButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AppendButton.Name = "AppendButton";
            this.AppendButton.Size = new System.Drawing.Size(72, 22);
            this.AppendButton.Text = "添加显示项";
            this.AppendButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // RemoveButton
            // 
            this.RemoveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.RemoveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(72, 22);
            this.RemoveButton.Text = "删除显示项";
            this.RemoveButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // MovePrevButton
            // 
            this.MovePrevButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MovePrevButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MovePrevButton.Name = "MovePrevButton";
            this.MovePrevButton.Size = new System.Drawing.Size(60, 22);
            this.MovePrevButton.Text = "向上移动";
            this.MovePrevButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // MoveNextButton
            // 
            this.MoveNextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MoveNextButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MoveNextButton.Name = "MoveNextButton";
            this.MoveNextButton.Size = new System.Drawing.Size(60, 22);
            this.MoveNextButton.Text = "向下移动";
            this.MoveNextButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // Button_Exit
            // 
            this.Button_Exit.Location = new System.Drawing.Point(520, 489);
            this.Button_Exit.Name = "Button_Exit";
            this.Button_Exit.Size = new System.Drawing.Size(85, 23);
            this.Button_Exit.TabIndex = 2;
            this.Button_Exit.Text = "关闭";
            this.Button_Exit.UseVisualStyleBackColor = true;
            this.Button_Exit.Click += new System.EventHandler(this.Button_Exit_Click);
            // 
            // fpSpread1
            // 
            this.fpSpread1.AccessibleDescription = "";
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.Location = new System.Drawing.Point(3, 28);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(651, 455);
            this.fpSpread1.TabIndex = 3;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread1_CellDoubleClick);
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 10;
            this.fpSpread1_Sheet1.RowCount = 0;
            this.fpSpread1_Sheet1.RowHeader.ColumnCount = 0;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "描述信息";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "数据项";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "数据项类型";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "可见性";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "可编辑";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "允许为空";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "用户定义";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "前景色";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "背景色";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "显示格式";
            this.fpSpread1_Sheet1.Columns.Get(2).Label = "数据项类型";
            this.fpSpread1_Sheet1.Columns.Get(2).Width = 72F;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread1.SetActiveViewport(0, 1, 0);
            // 
            // bt_save
            // 
            this.bt_save.Location = new System.Drawing.Point(399, 489);
            this.bt_save.Name = "bt_save";
            this.bt_save.Size = new System.Drawing.Size(85, 23);
            this.bt_save.TabIndex = 2;
            this.bt_save.Text = "保存";
            this.bt_save.UseVisualStyleBackColor = true;
            this.bt_save.Click += new System.EventHandler(this.bt_save_Click);
            // 
            // ModelUserFieldSettingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 519);
            this.Controls.Add(this.fpSpread1);
            this.Controls.Add(this.bt_save);
            this.Controls.Add(this.Button_Exit);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModelUserFieldSettingDialog";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "台账设置";
            this.Load += new System.EventHandler(this.ModelFieldSettingDialog_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Button Button_Exit;
        private System.Windows.Forms.ToolStripButton AppendButton;
        private System.Windows.Forms.ToolStripButton RemoveButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton MovePrevButton;
        private System.Windows.Forms.ToolStripButton MoveNextButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private System.Windows.Forms.Button bt_save;
    }
}