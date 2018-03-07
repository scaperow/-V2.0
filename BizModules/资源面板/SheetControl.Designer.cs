namespace BizModules
{
    partial class SheetControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SheetControl));
            this.FpSheetView = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SheetContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SheetDesignerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.QuerySheetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.CopySheetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.NewSheetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditSheetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteSheetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.NewFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.CheckDataZoneMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SheetContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // FpSheetView
            // 
            this.FpSheetView.AllowDrop = true;
            this.FpSheetView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FpSheetView.HideSelection = false;
            this.FpSheetView.ImageIndex = 0;
            this.FpSheetView.ImageList = this.imageList1;
            this.FpSheetView.Location = new System.Drawing.Point(0, 0);
            this.FpSheetView.Name = "FpSheetView";
            this.FpSheetView.SelectedImageIndex = 0;
            this.FpSheetView.Size = new System.Drawing.Size(263, 535);
            this.FpSheetView.TabIndex = 2;
            this.FpSheetView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.FpSheetView_AfterLabelEdit);
            this.FpSheetView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FpSheetView_MouseUp);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            this.imageList1.Images.SetKeyName(0, "关闭文件夹.bmp");
            this.imageList1.Images.SetKeyName(1, "打开文件夹.bmp");
            this.imageList1.Images.SetKeyName(2, "表单.png");
            // 
            // SheetContextMenu
            // 
            this.SheetContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SheetDesignerMenuItem,
            this.toolStripMenuItem4,
            this.QuerySheetMenuItem,
            this.toolStripMenuItem1,
            this.CopySheetMenuItem,
            this.toolStripMenuItem5,
            this.NewSheetMenuItem,
            this.EditSheetMenuItem,
            this.DeleteSheetMenuItem,
            this.toolStripMenuItem2,
            this.NewFolderMenuItem,
            this.EditFolderMenuItem,
            this.DeleteFolderMenuItem,
            this.toolStripMenuItem3,
            this.CheckDataZoneMenuItem});
            this.SheetContextMenu.Name = "ModelContextMenu";
            this.SheetContextMenu.Size = new System.Drawing.Size(153, 276);
            this.SheetContextMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ContextMenuStrip_ItemClicked);
            this.SheetContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip_Opening);
            // 
            // SheetDesignerMenuItem
            // 
            this.SheetDesignerMenuItem.Name = "SheetDesignerMenuItem";
            this.SheetDesignerMenuItem.Size = new System.Drawing.Size(152, 22);
            this.SheetDesignerMenuItem.Tag = "@SheetDesigner";
            this.SheetDesignerMenuItem.Text = "表单设计器";
            this.SheetDesignerMenuItem.Click += new System.EventHandler(this.SheetMenuStrip_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(149, 6);
            // 
            // QuerySheetMenuItem
            // 
            this.QuerySheetMenuItem.Name = "QuerySheetMenuItem";
            this.QuerySheetMenuItem.Size = new System.Drawing.Size(152, 22);
            this.QuerySheetMenuItem.Tag = "@SearchSheet";
            this.QuerySheetMenuItem.Text = "查找表单";
            this.QuerySheetMenuItem.Click += new System.EventHandler(this.SheetMenuStrip_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // CopySheetMenuItem
            // 
            this.CopySheetMenuItem.Name = "CopySheetMenuItem";
            this.CopySheetMenuItem.Size = new System.Drawing.Size(152, 22);
            this.CopySheetMenuItem.Tag = "@CopySheet";
            this.CopySheetMenuItem.Text = "复制表单";
            this.CopySheetMenuItem.Click += new System.EventHandler(this.SheetMenuStrip_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(149, 6);
            // 
            // NewSheetMenuItem
            // 
            this.NewSheetMenuItem.Name = "NewSheetMenuItem";
            this.NewSheetMenuItem.Size = new System.Drawing.Size(152, 22);
            this.NewSheetMenuItem.Tag = "@NewSheet";
            this.NewSheetMenuItem.Text = "新建表单";
            this.NewSheetMenuItem.Click += new System.EventHandler(this.SheetMenuStrip_Click);
            // 
            // EditSheetMenuItem
            // 
            this.EditSheetMenuItem.Name = "EditSheetMenuItem";
            this.EditSheetMenuItem.Size = new System.Drawing.Size(152, 22);
            this.EditSheetMenuItem.Tag = "@RenameSheet";
            this.EditSheetMenuItem.Text = "重命名";
            this.EditSheetMenuItem.Click += new System.EventHandler(this.SheetMenuStrip_Click);
            // 
            // DeleteSheetMenuItem
            // 
            this.DeleteSheetMenuItem.Name = "DeleteSheetMenuItem";
            this.DeleteSheetMenuItem.Size = new System.Drawing.Size(152, 22);
            this.DeleteSheetMenuItem.Tag = "@DeleteSheet";
            this.DeleteSheetMenuItem.Text = "删除表单";
            this.DeleteSheetMenuItem.Click += new System.EventHandler(this.SheetMenuStrip_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
            // 
            // NewFolderMenuItem
            // 
            this.NewFolderMenuItem.Name = "NewFolderMenuItem";
            this.NewFolderMenuItem.Size = new System.Drawing.Size(152, 22);
            this.NewFolderMenuItem.Tag = "@NewSheetFolder";
            this.NewFolderMenuItem.Text = "新建文件夹";
            this.NewFolderMenuItem.Click += new System.EventHandler(this.SheetMenuStrip_Click);
            // 
            // EditFolderMenuItem
            // 
            this.EditFolderMenuItem.Name = "EditFolderMenuItem";
            this.EditFolderMenuItem.Size = new System.Drawing.Size(152, 22);
            this.EditFolderMenuItem.Tag = "@EditSheetFolder";
            this.EditFolderMenuItem.Text = "编辑文件夹";
            this.EditFolderMenuItem.Click += new System.EventHandler(this.SheetMenuStrip_Click);
            // 
            // DeleteFolderMenuItem
            // 
            this.DeleteFolderMenuItem.Name = "DeleteFolderMenuItem";
            this.DeleteFolderMenuItem.Size = new System.Drawing.Size(152, 22);
            this.DeleteFolderMenuItem.Tag = "@DeleteSheetFolder";
            this.DeleteFolderMenuItem.Text = "删除文件夹";
            this.DeleteFolderMenuItem.Click += new System.EventHandler(this.SheetMenuStrip_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(149, 6);
            // 
            // CheckDataZoneMenuItem
            // 
            this.CheckDataZoneMenuItem.Name = "CheckDataZoneMenuItem";
            this.CheckDataZoneMenuItem.Size = new System.Drawing.Size(152, 22);
            this.CheckDataZoneMenuItem.Tag = "@CheckDataZone";
            this.CheckDataZoneMenuItem.Text = "一致性校验";
            this.CheckDataZoneMenuItem.Click += new System.EventHandler(this.SheetMenuStrip_Click);
            // 
            // SheetControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FpSheetView);
            this.Name = "SheetControl";
            this.Size = new System.Drawing.Size(263, 535);
            this.SheetContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView FpSheetView;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip SheetContextMenu;
        private System.Windows.Forms.ToolStripMenuItem SheetDesignerMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem NewSheetMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditSheetMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteSheetMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem NewFolderMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditFolderMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteFolderMenuItem;
        private System.Windows.Forms.ToolStripMenuItem QuerySheetMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem CopySheetMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem CheckDataZoneMenuItem;
    }
}
