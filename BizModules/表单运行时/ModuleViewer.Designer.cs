namespace BizModules
{
    partial class ModuleViewer
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
            fpSpreadViewer1.Dispose();
     

            fpSpreadViewer1 = null;
       

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModuleViewer));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.SponsorModifyButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.NewDataButton = new System.Windows.Forms.ToolStripButton();
            this.SaveDataButton = new System.Windows.Forms.ToolStripButton();
            this.SelectTemperatureTypeButton = new System.Windows.Forms.ToolStripButton();
            this.TemperatureListButton = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItem_AddImage = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_RemoveImage = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItem_SetHyperLink = new System.Windows.Forms.ToolStripMenuItem();
            this.fpSpreadViewer1 = new BizComponents.FpSpreadViewer();
            this.ButtonSetImage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SponsorModifyButton,
            this.toolStripSeparator1,
            this.NewDataButton,
            this.SaveDataButton,
            this.SelectTemperatureTypeButton,
            this.TemperatureListButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(792, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // SponsorModifyButton
            // 
            this.SponsorModifyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SponsorModifyButton.Image = global::BizModules.Properties.Resources.审批修改;
            this.SponsorModifyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SponsorModifyButton.Name = "SponsorModifyButton";
            this.SponsorModifyButton.Size = new System.Drawing.Size(23, 22);
            this.SponsorModifyButton.Text = "申请修改";
            this.SponsorModifyButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // NewDataButton
            // 
            this.NewDataButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewDataButton.Image = global::BizModules.Properties.Resources.新建表单;
            this.NewDataButton.ImageTransparentColor = System.Drawing.Color.White;
            this.NewDataButton.Name = "NewDataButton";
            this.NewDataButton.Size = new System.Drawing.Size(23, 22);
            this.NewDataButton.Text = "新建资料";
            this.NewDataButton.Visible = false;
            this.NewDataButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // SaveDataButton
            // 
            this.SaveDataButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveDataButton.Image = global::BizModules.Properties.Resources.保存;
            this.SaveDataButton.ImageTransparentColor = System.Drawing.Color.White;
            this.SaveDataButton.Name = "SaveDataButton";
            this.SaveDataButton.Size = new System.Drawing.Size(23, 22);
            this.SaveDataButton.Text = "保存资料";
            this.SaveDataButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // SelectTemperatureTypeButton
            // 
            this.SelectTemperatureTypeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SelectTemperatureTypeButton.Image = ((System.Drawing.Image)(resources.GetObject("SelectTemperatureTypeButton.Image")));
            this.SelectTemperatureTypeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SelectTemperatureTypeButton.Name = "SelectTemperatureTypeButton";
            this.SelectTemperatureTypeButton.Size = new System.Drawing.Size(23, 22);
            this.SelectTemperatureTypeButton.Text = "温度类型设置";
            this.SelectTemperatureTypeButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // TemperatureListButton
            // 
            this.TemperatureListButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TemperatureListButton.Image = ((System.Drawing.Image)(resources.GetObject("TemperatureListButton.Image")));
            this.TemperatureListButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TemperatureListButton.Name = "TemperatureListButton";
            this.TemperatureListButton.Size = new System.Drawing.Size(23, 22);
            this.TemperatureListButton.Text = "试验温度记录";
            this.TemperatureListButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ButtonSetImage,
            this.MenuItem_AddImage,
            this.MenuItem_RemoveImage});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(159, 92);
            // 
            // MenuItem_AddImage
            // 
            this.MenuItem_AddImage.Name = "MenuItem_AddImage";
            this.MenuItem_AddImage.Size = new System.Drawing.Size(158, 22);
            this.MenuItem_AddImage.Text = "设置并编辑图片";
            this.MenuItem_AddImage.Click += new System.EventHandler(this.MenuItem_AddImage_Click);
            // 
            // MenuItem_RemoveImage
            // 
            this.MenuItem_RemoveImage.Name = "MenuItem_RemoveImage";
            this.MenuItem_RemoveImage.Size = new System.Drawing.Size(158, 22);
            this.MenuItem_RemoveImage.Text = "清除图片";
            this.MenuItem_RemoveImage.Click += new System.EventHandler(this.MenuItem_RemoveImage_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_SetHyperLink});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(135, 26);
            // 
            // MenuItem_SetHyperLink
            // 
            this.MenuItem_SetHyperLink.Name = "MenuItem_SetHyperLink";
            this.MenuItem_SetHyperLink.Size = new System.Drawing.Size(134, 22);
            this.MenuItem_SetHyperLink.Text = "设置超链接";
            this.MenuItem_SetHyperLink.Click += new System.EventHandler(this.MenuItem_SetHyperLink_Click);
            // 
            // fpSpreadViewer1
            // 
            this.fpSpreadViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpreadViewer1.Location = new System.Drawing.Point(0, 25);
            this.fpSpreadViewer1.Name = "fpSpreadViewer1";
            this.fpSpreadViewer1.Size = new System.Drawing.Size(792, 596);
            this.fpSpreadViewer1.TabIndex = 2;
            // 
            // ButtonSetImage
            // 
            this.ButtonSetImage.Name = "ButtonSetImage";
            this.ButtonSetImage.Size = new System.Drawing.Size(158, 22);
            this.ButtonSetImage.Text = "设置图片";
            this.ButtonSetImage.Click += new System.EventHandler(this.ButtonSetImage_Click);
            // 
            // ModuleViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 621);
            this.Controls.Add(this.fpSpreadViewer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ModuleViewer";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "资料编辑器";
            this.Load += new System.EventHandler(this.ModuleViewer_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModuleViewer_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton NewDataButton;
        private BizComponents.FpSpreadViewer fpSpreadViewer1;
        private System.Windows.Forms.ToolStripButton SaveDataButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton SponsorModifyButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_AddImage;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_RemoveImage;
        private System.Windows.Forms.ToolStripButton SelectTemperatureTypeButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_SetHyperLink;
        private System.Windows.Forms.ToolStripButton TemperatureListButton;
        private System.Windows.Forms.ToolStripMenuItem ButtonSetImage;
    }
}