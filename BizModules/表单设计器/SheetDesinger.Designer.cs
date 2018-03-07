namespace BizModules
{
    partial class SheetDesinger
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

            FpSpread.Dispose();
            fpSheetEditor1.Dispose();

            fpSheetEditor1 = null;

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MainStrip = new System.Windows.Forms.ToolStrip();
            this.SaveSheetButton = new System.Windows.Forms.ToolStripButton();
            this.SaveSheetStyleButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.CheckDataZoneButton = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fpSheetEditor1 = new BizComponents.FpSheetEditor();
            this.showDataArea = new System.Windows.Forms.ToolStripButton();
            this.MainStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainStrip
            // 
            this.MainStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveSheetButton,
            this.SaveSheetStyleButton,
            this.toolStripSeparator1,
            this.CheckDataZoneButton,
            this.showDataArea});
            this.MainStrip.Location = new System.Drawing.Point(0, 0);
            this.MainStrip.Name = "MainStrip";
            this.MainStrip.Size = new System.Drawing.Size(792, 25);
            this.MainStrip.TabIndex = 0;
            this.MainStrip.Text = "toolStrip1";
            // 
            // SaveSheetButton
            // 
            this.SaveSheetButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveSheetButton.Image = global::BizModules.Properties.Resources.保存;
            this.SaveSheetButton.ImageTransparentColor = System.Drawing.Color.White;
            this.SaveSheetButton.Name = "SaveSheetButton";
            this.SaveSheetButton.Size = new System.Drawing.Size(23, 22);
            this.SaveSheetButton.Text = "保存表单";
            this.SaveSheetButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // SaveSheetStyleButton
            // 
            this.SaveSheetStyleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveSheetStyleButton.Image = global::BizModules.Properties.Resources.保存样式;
            this.SaveSheetStyleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveSheetStyleButton.Name = "SaveSheetStyleButton";
            this.SaveSheetStyleButton.Size = new System.Drawing.Size(23, 22);
            this.SaveSheetStyleButton.Text = "保存样式";
            this.SaveSheetStyleButton.Visible = false;
            this.SaveSheetStyleButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // CheckDataZoneButton
            // 
            this.CheckDataZoneButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.CheckDataZoneButton.Image = global::BizModules.Properties.Resources.一致性校验;
            this.CheckDataZoneButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CheckDataZoneButton.Name = "CheckDataZoneButton";
            this.CheckDataZoneButton.Size = new System.Drawing.Size(72, 22);
            this.CheckDataZoneButton.Text = "一致性校验";
            this.CheckDataZoneButton.ToolTipText = "检查单元格类型与字段类型是否一致";
            this.CheckDataZoneButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // fpSheetEditor1
            // 
            this.fpSheetEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSheetEditor1.Location = new System.Drawing.Point(0, 25);
            this.fpSheetEditor1.Name = "fpSheetEditor1";
            this.fpSheetEditor1.Size = new System.Drawing.Size(792, 548);
            this.fpSheetEditor1.TabIndex = 1;
            // 
            // showDataArea
            // 
            this.showDataArea.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.showDataArea.Image = global::BizModules.Properties.Resources.一致性校验;
            this.showDataArea.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showDataArea.Name = "showDataArea";
            this.showDataArea.Size = new System.Drawing.Size(72, 22);
            this.showDataArea.Text = "显示数据区";
            this.showDataArea.ToolTipText = "显示数据区";
            this.showDataArea.Click += new System.EventHandler(this.showDataArea_Click);
            // 
            // SheetDesinger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.fpSheetEditor1);
            this.Controls.Add(this.MainStrip);
            this.Name = "SheetDesinger";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "表单设计器";
            this.Load += new System.EventHandler(this.SheetDesinger_Load);
            this.MainStrip.ResumeLayout(false);
            this.MainStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip MainStrip;
        private System.Windows.Forms.ToolStripButton SaveSheetButton;
        private System.Windows.Forms.ToolStripButton SaveSheetStyleButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private BizComponents.FpSheetEditor fpSheetEditor1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton CheckDataZoneButton;
        private System.Windows.Forms.ToolStripButton showDataArea;
    }
}