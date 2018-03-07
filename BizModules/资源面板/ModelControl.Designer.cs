namespace BizModules
{
    partial class ModelControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelControl));
            this.TemplateTree = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ModelContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ModelDesignerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemImport = new System.Windows.Forms.ToolStripMenuItem();
            this.TestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.QueryModelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.TaizhangSettingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemExport = new System.Windows.Forms.ToolStripMenuItem();
            this.NewModelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditModelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteModelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.NewFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.LineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LineModuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LineFormulaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GGCUploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GGCDocUploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StasticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SpecialUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListDrag = new System.Windows.Forms.ImageList(this.components);
            this.ModelContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // TemplateTree
            // 
            this.TemplateTree.AllowDrop = true;
            this.TemplateTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TemplateTree.HideSelection = false;
            this.TemplateTree.ImageIndex = 0;
            this.TemplateTree.ImageList = this.imageList1;
            this.TemplateTree.Location = new System.Drawing.Point(0, 0);
            this.TemplateTree.Name = "TemplateTree";
            this.TemplateTree.SelectedImageIndex = 0;
            this.TemplateTree.Size = new System.Drawing.Size(260, 541);
            this.TemplateTree.TabIndex = 1;
            this.TemplateTree.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.TemplateTree_GiveFeedback);
            this.TemplateTree.DragLeave += new System.EventHandler(this.TemplateTree_DragLeave);
            this.TemplateTree.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TemplateTree_AfterLabelEdit);
            this.TemplateTree.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TemplateTree_MouseUp);
            this.TemplateTree.DragEnter += new System.Windows.Forms.DragEventHandler(this.TemplateTree_DragEnter);
            this.TemplateTree.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TemplateTree_ItemDrag);
            this.TemplateTree.DragOver += new System.Windows.Forms.DragEventHandler(this.TemplateTree_DragOver);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            this.imageList1.Images.SetKeyName(0, "关闭文件夹.bmp");
            this.imageList1.Images.SetKeyName(1, "打开文件夹.bmp");
            this.imageList1.Images.SetKeyName(2, "表单.bmp");
            // 
            // ModelContextMenu
            // 
            this.ModelContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ModelDesignerMenuItem,
            this.toolStripMenuItem4,
            this.ToolStripMenuItemImport,
            this.TestToolStripMenuItem,
            this.QueryModelMenuItem,
            this.toolStripMenuItem5,
            this.TaizhangSettingMenuItem,
            this.ToolStripMenuItemExport,
            this.NewModelMenuItem,
            this.EditModelMenuItem,
            this.DeleteModelMenuItem,
            this.toolStripMenuItem2,
            this.NewFolderMenuItem,
            this.EditFolderMenuItem,
            this.DeleteFolderMenuItem,
            this.toolStripMenuItem3,
            this.LineToolStripMenuItem,
            this.LineModuleToolStripMenuItem,
            this.LineFormulaToolStripMenuItem,
            this.UploadToolStripMenuItem,
            this.GGCUploadToolStripMenuItem,
            this.GGCDocUploadToolStripMenuItem,
            this.StasticsToolStripMenuItem,
            this.SpecialUpdateToolStripMenuItem});
            this.ModelContextMenu.Name = "ModelContextMenu";
            this.ModelContextMenu.Size = new System.Drawing.Size(175, 490);
            this.ModelContextMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ContextMenuStrip1_ItemClicked);
            this.ModelContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip_Opening);
            // 
            // ModelDesignerMenuItem
            // 
            this.ModelDesignerMenuItem.Name = "ModelDesignerMenuItem";
            this.ModelDesignerMenuItem.Size = new System.Drawing.Size(174, 22);
            this.ModelDesignerMenuItem.Tag = "@ModelDesigner";
            this.ModelDesignerMenuItem.Text = "模板设计器";
            this.ModelDesignerMenuItem.Click += new System.EventHandler(this.ModelMenuStrip_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(171, 6);
            // 
            // ToolStripMenuItemImport
            // 
            this.ToolStripMenuItemImport.Name = "ToolStripMenuItemImport";
            this.ToolStripMenuItemImport.Size = new System.Drawing.Size(174, 22);
            this.ToolStripMenuItemImport.Tag = "@stadium";
            this.ToolStripMenuItemImport.Text = "龄期设置";
            this.ToolStripMenuItemImport.Click += new System.EventHandler(this.ModelMenuStrip_Click);
            // 
            // TestToolStripMenuItem
            // 
            this.TestToolStripMenuItem.Name = "TestToolStripMenuItem";
            this.TestToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.TestToolStripMenuItem.Tag = "@test";
            this.TestToolStripMenuItem.Text = "采集设置";
            this.TestToolStripMenuItem.Click += new System.EventHandler(this.ModelMenuStrip_Click);
            // 
            // QueryModelMenuItem
            // 
            this.QueryModelMenuItem.Name = "QueryModelMenuItem";
            this.QueryModelMenuItem.Size = new System.Drawing.Size(174, 22);
            this.QueryModelMenuItem.Tag = "@SearchModel";
            this.QueryModelMenuItem.Text = "查找模板";
            this.QueryModelMenuItem.Click += new System.EventHandler(this.ModelMenuStrip_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(171, 6);
            // 
            // TaizhangSettingMenuItem
            // 
            this.TaizhangSettingMenuItem.Name = "TaizhangSettingMenuItem";
            this.TaizhangSettingMenuItem.Size = new System.Drawing.Size(174, 22);
            this.TaizhangSettingMenuItem.Tag = "@TaizhangSetting";
            this.TaizhangSettingMenuItem.Text = "台账设置";
            this.TaizhangSettingMenuItem.Click += new System.EventHandler(this.ModelMenuStrip_Click);
            // 
            // ToolStripMenuItemExport
            // 
            this.ToolStripMenuItemExport.Name = "ToolStripMenuItemExport";
            this.ToolStripMenuItemExport.Size = new System.Drawing.Size(174, 22);
            this.ToolStripMenuItemExport.Tag = "@export";
            this.ToolStripMenuItemExport.Text = "检验设置";
            this.ToolStripMenuItemExport.Click += new System.EventHandler(this.ModelMenuStrip_Click);
            // 
            // NewModelMenuItem
            // 
            this.NewModelMenuItem.Name = "NewModelMenuItem";
            this.NewModelMenuItem.Size = new System.Drawing.Size(174, 22);
            this.NewModelMenuItem.Tag = "@NewModel";
            this.NewModelMenuItem.Text = "新建模板";
            this.NewModelMenuItem.Click += new System.EventHandler(this.ModelMenuStrip_Click);
            // 
            // EditModelMenuItem
            // 
            this.EditModelMenuItem.Name = "EditModelMenuItem";
            this.EditModelMenuItem.Size = new System.Drawing.Size(174, 22);
            this.EditModelMenuItem.Tag = "@RenameModel";
            this.EditModelMenuItem.Text = "重命名";
            this.EditModelMenuItem.Click += new System.EventHandler(this.ModelMenuStrip_Click);
            // 
            // DeleteModelMenuItem
            // 
            this.DeleteModelMenuItem.Name = "DeleteModelMenuItem";
            this.DeleteModelMenuItem.Size = new System.Drawing.Size(174, 22);
            this.DeleteModelMenuItem.Tag = "@DeleteModel";
            this.DeleteModelMenuItem.Text = "删除模板";
            this.DeleteModelMenuItem.Click += new System.EventHandler(this.ModelMenuStrip_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(171, 6);
            // 
            // NewFolderMenuItem
            // 
            this.NewFolderMenuItem.Name = "NewFolderMenuItem";
            this.NewFolderMenuItem.Size = new System.Drawing.Size(174, 22);
            this.NewFolderMenuItem.Tag = "@NewModelFolder";
            this.NewFolderMenuItem.Text = "新建文件夹";
            this.NewFolderMenuItem.Click += new System.EventHandler(this.ModelMenuStrip_Click);
            // 
            // EditFolderMenuItem
            // 
            this.EditFolderMenuItem.Name = "EditFolderMenuItem";
            this.EditFolderMenuItem.Size = new System.Drawing.Size(174, 22);
            this.EditFolderMenuItem.Tag = "@EditModelFolder";
            this.EditFolderMenuItem.Text = "编辑文件夹";
            this.EditFolderMenuItem.Click += new System.EventHandler(this.ModelMenuStrip_Click);
            // 
            // DeleteFolderMenuItem
            // 
            this.DeleteFolderMenuItem.Name = "DeleteFolderMenuItem";
            this.DeleteFolderMenuItem.Size = new System.Drawing.Size(174, 22);
            this.DeleteFolderMenuItem.Tag = "@DeleteModelFolder";
            this.DeleteFolderMenuItem.Text = "删除文件夹";
            this.DeleteFolderMenuItem.Click += new System.EventHandler(this.ModelMenuStrip_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(171, 6);
            // 
            // LineToolStripMenuItem
            // 
            this.LineToolStripMenuItem.Name = "LineToolStripMenuItem";
            this.LineToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.LineToolStripMenuItem.Tag = "@line";
            this.LineToolStripMenuItem.Text = "线路维护";
            this.LineToolStripMenuItem.Click += new System.EventHandler(this.ModelMenuStrip_Click);
            // 
            // LineModuleToolStripMenuItem
            // 
            this.LineModuleToolStripMenuItem.Name = "LineModuleToolStripMenuItem";
            this.LineModuleToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.LineModuleToolStripMenuItem.Tag = "@linemodule";
            this.LineModuleToolStripMenuItem.Text = "线路模板设置";
            this.LineModuleToolStripMenuItem.Click += new System.EventHandler(this.ModelMenuStrip_Click);
            // 
            // LineFormulaToolStripMenuItem
            // 
            this.LineFormulaToolStripMenuItem.Name = "LineFormulaToolStripMenuItem";
            this.LineFormulaToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.LineFormulaToolStripMenuItem.Tag = "@lineformula";
            this.LineFormulaToolStripMenuItem.Text = "线路公式";
            this.LineFormulaToolStripMenuItem.ToolTipText = "维护本线路公式";
            this.LineFormulaToolStripMenuItem.Click += new System.EventHandler(this.ModelMenuStrip_Click);
            // 
            // UploadToolStripMenuItem
            // 
            this.UploadToolStripMenuItem.Name = "UploadToolStripMenuItem";
            this.UploadToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.UploadToolStripMenuItem.Tag = "@upload";
            this.UploadToolStripMenuItem.Text = "上传设置";
            this.UploadToolStripMenuItem.Click += new System.EventHandler(this.ModelMenuStrip_Click);
            // 
            // GGCUploadToolStripMenuItem
            // 
            this.GGCUploadToolStripMenuItem.Name = "GGCUploadToolStripMenuItem";
            this.GGCUploadToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.GGCUploadToolStripMenuItem.Tag = "@ggcupload";
            this.GGCUploadToolStripMenuItem.Text = "GGC采集上传设置";
            this.GGCUploadToolStripMenuItem.Click += new System.EventHandler(this.ModelMenuStrip_Click);
            // 
            // GGCDocUploadToolStripMenuItem
            // 
            this.GGCDocUploadToolStripMenuItem.Name = "GGCDocUploadToolStripMenuItem";
            this.GGCDocUploadToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.GGCDocUploadToolStripMenuItem.Tag = "@ggcdocupload";
            this.GGCDocUploadToolStripMenuItem.Text = "GGC文档上传设置";
            this.GGCDocUploadToolStripMenuItem.Click += new System.EventHandler(this.ModelMenuStrip_Click);
            // 
            // StasticsToolStripMenuItem
            // 
            this.StasticsToolStripMenuItem.Name = "StasticsToolStripMenuItem";
            this.StasticsToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.StasticsToolStripMenuItem.Tag = "@statisticsmap";
            this.StasticsToolStripMenuItem.Text = "统计项模板设置";
            this.StasticsToolStripMenuItem.Click += new System.EventHandler(this.ModelMenuStrip_Click);
            // 
            // SpecialUpdateToolStripMenuItem
            // 
            this.SpecialUpdateToolStripMenuItem.Name = "SpecialUpdateToolStripMenuItem";
            this.SpecialUpdateToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.SpecialUpdateToolStripMenuItem.Tag = "@specialupdate";
            this.SpecialUpdateToolStripMenuItem.Text = "自定义更新";
            this.SpecialUpdateToolStripMenuItem.Click += new System.EventHandler(this.ModelMenuStrip_Click);
            // 
            // imageListDrag
            // 
            this.imageListDrag.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListDrag.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListDrag.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ModelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TemplateTree);
            this.Name = "ModelControl";
            this.Size = new System.Drawing.Size(260, 541);
            this.Load += new System.EventHandler(this.ModelControl_Load);
            this.ModelContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView TemplateTree;
        private System.Windows.Forms.ContextMenuStrip ModelContextMenu;
        private System.Windows.Forms.ToolStripMenuItem ModelDesignerMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem QueryModelMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem NewModelMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditModelMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteModelMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem NewFolderMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditFolderMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteFolderMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem TaizhangSettingMenuItem;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageListDrag;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemExport;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemImport;
        private System.Windows.Forms.ToolStripMenuItem TestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LineFormulaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UploadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SpecialUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LineModuleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GGCUploadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GGCDocUploadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StasticsToolStripMenuItem;
    }
}
