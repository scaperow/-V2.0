namespace BizModules
{
    partial class ModelDesigner
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelDesigner));
            this.MainStrip = new System.Windows.Forms.ToolStrip();
            this.ImportSheetButton = new System.Windows.Forms.ToolStripButton();
            this.RemoveSheetButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveModuleButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.WriteFunctionButton = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fpSpreadEditor1 = new BizComponents.FpSpreadEditor();
            this.MainStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainStrip
            // 
            this.MainStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImportSheetButton,
            this.RemoveSheetButton,
            this.toolStripSeparator2,
            this.SaveModuleButton,
            this.toolStripSeparator3,
            this.WriteFunctionButton});
            this.MainStrip.Location = new System.Drawing.Point(0, 0);
            this.MainStrip.Name = "MainStrip";
            this.MainStrip.Size = new System.Drawing.Size(635, 25);
            this.MainStrip.TabIndex = 1;
            this.MainStrip.Text = "常规工具栏";
            // 
            // ImportSheetButton
            // 
            this.ImportSheetButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ImportSheetButton.Image = global::BizModules.Properties.Resources.导入表单;
            this.ImportSheetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ImportSheetButton.Name = "ImportSheetButton";
            this.ImportSheetButton.Size = new System.Drawing.Size(23, 22);
            this.ImportSheetButton.Text = "添加表单";
            this.ImportSheetButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // RemoveSheetButton
            // 
            this.RemoveSheetButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RemoveSheetButton.Image = global::BizModules.Properties.Resources.删除表单;
            this.RemoveSheetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RemoveSheetButton.Name = "RemoveSheetButton";
            this.RemoveSheetButton.Size = new System.Drawing.Size(23, 22);
            this.RemoveSheetButton.Text = "移除表单";
            this.RemoveSheetButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // SaveModuleButton
            // 
            this.SaveModuleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveModuleButton.Image = global::BizModules.Properties.Resources.保存;
            this.SaveModuleButton.ImageTransparentColor = System.Drawing.Color.White;
            this.SaveModuleButton.Name = "SaveModuleButton";
            this.SaveModuleButton.Size = new System.Drawing.Size(23, 22);
            this.SaveModuleButton.Text = "保存模板";
            this.SaveModuleButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // WriteFunctionButton
            // 
            this.WriteFunctionButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.WriteFunctionButton.Image = ((System.Drawing.Image)(resources.GetObject("WriteFunctionButton.Image")));
            this.WriteFunctionButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.WriteFunctionButton.Name = "WriteFunctionButton";
            this.WriteFunctionButton.Size = new System.Drawing.Size(23, 22);
            this.WriteFunctionButton.Text = "配置写数事件";
            this.WriteFunctionButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // fpSpreadEditor1
            // 
            this.fpSpreadEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpreadEditor1.Location = new System.Drawing.Point(0, 25);
            this.fpSpreadEditor1.Name = "fpSpreadEditor1";
            this.fpSpreadEditor1.Size = new System.Drawing.Size(635, 467);
            this.fpSpreadEditor1.TabIndex = 2;
            // 
            // ModelDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 492);
            this.Controls.Add(this.fpSpreadEditor1);
            this.Controls.Add(this.MainStrip);
            this.Name = "ModelDesigner";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "模板设计器";
            this.Load += new System.EventHandler(this.ModelDesigner_Load);
            this.MainStrip.ResumeLayout(false);
            this.MainStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip MainStrip;
        private System.Windows.Forms.ToolStripButton ImportSheetButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton SaveModuleButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton WriteFunctionButton;
        private BizComponents.FpSpreadEditor fpSpreadEditor1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripButton RemoveSheetButton;
    }
}