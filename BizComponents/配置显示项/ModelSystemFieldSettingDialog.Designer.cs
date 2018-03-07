namespace BizComponents
{
    partial class ModelSystemFieldSettingDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelSystemFieldSettingDialog));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.AppendButton = new System.Windows.Forms.ToolStripButton();
            this.RemoveButton = new System.Windows.Forms.ToolStripButton();
            this.Button_Exit = new System.Windows.Forms.Button();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.bt_save = new System.Windows.Forms.Button();
            this.bt_apply = new System.Windows.Forms.Button();
            this.cb_moduleType = new System.Windows.Forms.CheckBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AppendButton,
            this.RemoveButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(377, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // AppendButton
            // 
            this.AppendButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.AppendButton.Image = ((System.Drawing.Image)(resources.GetObject("AppendButton.Image")));
            this.AppendButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AppendButton.Name = "AppendButton";
            this.AppendButton.Size = new System.Drawing.Size(72, 22);
            this.AppendButton.Text = "添加显示项";
            this.AppendButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // RemoveButton
            // 
            this.RemoveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.RemoveButton.Image = ((System.Drawing.Image)(resources.GetObject("RemoveButton.Image")));
            this.RemoveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(72, 22);
            this.RemoveButton.Text = "删除显示项";
            this.RemoveButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // Button_Exit
            // 
            this.Button_Exit.Location = new System.Drawing.Point(266, 375);
            this.Button_Exit.Name = "Button_Exit";
            this.Button_Exit.Size = new System.Drawing.Size(85, 23);
            this.Button_Exit.TabIndex = 2;
            this.Button_Exit.Text = "关闭";
            this.Button_Exit.UseVisualStyleBackColor = true;
            this.Button_Exit.Click += new System.EventHandler(this.Button_Exit_Click);
            // 
            // fpSpread1
            // 
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.fpSpread1.BackColor = System.Drawing.SystemColors.Control;
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.Location = new System.Drawing.Point(3, 28);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(367, 311);
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
            this.fpSpread1_Sheet1.ColumnCount = 3;
            this.fpSpread1_Sheet1.RowCount = 0;
            this.fpSpread1_Sheet1.RowHeader.ColumnCount = 0;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "描述信息";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "数据项";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "系统列";
            this.fpSpread1_Sheet1.Columns.Get(0).Label = "描述信息";
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 131F;
            this.fpSpread1_Sheet1.Columns.Get(1).Label = "数据项";
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 81F;
            this.fpSpread1_Sheet1.Columns.Get(2).Label = "系统列";
            this.fpSpread1_Sheet1.Columns.Get(2).Width = 122F;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread1.SetActiveViewport(0, 1, 0);
            // 
            // bt_save
            // 
            this.bt_save.Location = new System.Drawing.Point(168, 375);
            this.bt_save.Name = "bt_save";
            this.bt_save.Size = new System.Drawing.Size(85, 23);
            this.bt_save.TabIndex = 2;
            this.bt_save.Text = "确定";
            this.bt_save.UseVisualStyleBackColor = true;
            this.bt_save.Click += new System.EventHandler(this.bt_save_Click);
            // 
            // bt_apply
            // 
            this.bt_apply.Location = new System.Drawing.Point(13, 375);
            this.bt_apply.Name = "bt_apply";
            this.bt_apply.Size = new System.Drawing.Size(85, 23);
            this.bt_apply.TabIndex = 2;
            this.bt_apply.Text = "应用设置";
            this.bt_apply.UseVisualStyleBackColor = true;
            this.bt_apply.Click += new System.EventHandler(this.bt_apply_Click);
            // 
            // cb_moduleType
            // 
            this.cb_moduleType.AutoSize = true;
            this.cb_moduleType.Location = new System.Drawing.Point(13, 345);
            this.cb_moduleType.Name = "cb_moduleType";
            this.cb_moduleType.Size = new System.Drawing.Size(108, 16);
            this.cb_moduleType.TabIndex = 4;
            this.cb_moduleType.Text = "是否为试验模板";
            this.cb_moduleType.UseVisualStyleBackColor = true;
            // 
            // ModelSystemFieldSettingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 411);
            this.Controls.Add(this.cb_moduleType);
            this.Controls.Add(this.fpSpread1);
            this.Controls.Add(this.bt_apply);
            this.Controls.Add(this.bt_save);
            this.Controls.Add(this.Button_Exit);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModelSystemFieldSettingDialog";
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
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private System.Windows.Forms.Button bt_save;
        private System.Windows.Forms.Button bt_apply;
        private System.Windows.Forms.CheckBox cb_moduleType;
    }
}