namespace ReportComponents
{
    partial class ReportCatlogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportCatlogForm));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.NewFolderButton = new System.Windows.Forms.ToolStripButton();
            this.ModifyFolderButton = new System.Windows.Forms.ToolStripButton();
            this.DeleteFolderButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.NewReportButton = new System.Windows.Forms.ToolStripButton();
            this.ModifyReportButton = new System.Windows.Forms.ToolStripButton();
            this.DeleteReportButton = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItem_NewFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_ModifyFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_DeleteFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItem_NewReport = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_ModifyReport = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_DeleteReport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.HideSelection = false;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.LabelEdit = true;
            this.treeView1.Location = new System.Drawing.Point(0, 25);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(302, 508);
            this.treeView1.TabIndex = 3;
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            this.treeView1.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView1_AfterLabelEdit);
            this.treeView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseUp);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            this.imageList1.Images.SetKeyName(0, "关闭文件夹.bmp");
            this.imageList1.Images.SetKeyName(1, "打开文件夹.bmp");
            this.imageList1.Images.SetKeyName(2, "表单.bmp");
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewFolderButton,
            this.ModifyFolderButton,
            this.DeleteFolderButton,
            this.toolStripSeparator1,
            this.NewReportButton,
            this.ModifyReportButton,
            this.DeleteReportButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(302, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // NewFolderButton
            // 
            this.NewFolderButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewFolderButton.Image = global::ReportComponents.Properties.Resources.新建文件夹;
            this.NewFolderButton.ImageTransparentColor = System.Drawing.Color.White;
            this.NewFolderButton.Name = "NewFolderButton";
            this.NewFolderButton.Size = new System.Drawing.Size(23, 22);
            this.NewFolderButton.Text = "新建文件夹";
            this.NewFolderButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // ModifyFolderButton
            // 
            this.ModifyFolderButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ModifyFolderButton.Image = global::ReportComponents.Properties.Resources.编辑文件夹;
            this.ModifyFolderButton.ImageTransparentColor = System.Drawing.Color.White;
            this.ModifyFolderButton.Name = "ModifyFolderButton";
            this.ModifyFolderButton.Size = new System.Drawing.Size(23, 22);
            this.ModifyFolderButton.Text = "修改文件夹";
            this.ModifyFolderButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // DeleteFolderButton
            // 
            this.DeleteFolderButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DeleteFolderButton.Image = global::ReportComponents.Properties.Resources.删除文件夹;
            this.DeleteFolderButton.ImageTransparentColor = System.Drawing.Color.White;
            this.DeleteFolderButton.Name = "DeleteFolderButton";
            this.DeleteFolderButton.Size = new System.Drawing.Size(23, 22);
            this.DeleteFolderButton.Text = "删除文件夹";
            this.DeleteFolderButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // NewReportButton
            // 
            this.NewReportButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewReportButton.Image = global::ReportComponents.Properties.Resources.新建报表;
            this.NewReportButton.ImageTransparentColor = System.Drawing.Color.White;
            this.NewReportButton.Name = "NewReportButton";
            this.NewReportButton.Size = new System.Drawing.Size(23, 22);
            this.NewReportButton.Text = "新建报表";
            this.NewReportButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // ModifyReportButton
            // 
            this.ModifyReportButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ModifyReportButton.Image = global::ReportComponents.Properties.Resources.编辑报表;
            this.ModifyReportButton.ImageTransparentColor = System.Drawing.Color.White;
            this.ModifyReportButton.Name = "ModifyReportButton";
            this.ModifyReportButton.Size = new System.Drawing.Size(23, 22);
            this.ModifyReportButton.Text = "修改报表名称";
            this.ModifyReportButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // DeleteReportButton
            // 
            this.DeleteReportButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DeleteReportButton.Image = global::ReportComponents.Properties.Resources.删除报表;
            this.DeleteReportButton.ImageTransparentColor = System.Drawing.Color.White;
            this.DeleteReportButton.Name = "DeleteReportButton";
            this.DeleteReportButton.Size = new System.Drawing.Size(23, 22);
            this.DeleteReportButton.Text = "删除报表";
            this.DeleteReportButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_NewFolder,
            this.MenuItem_ModifyFolder,
            this.MenuItem_DeleteFolder,
            this.toolStripSeparator2,
            this.MenuItem_NewReport,
            this.MenuItem_ModifyReport,
            this.MenuItem_DeleteReport});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 164);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // MenuItem_NewFolder
            // 
            this.MenuItem_NewFolder.Name = "MenuItem_NewFolder";
            this.MenuItem_NewFolder.Size = new System.Drawing.Size(152, 22);
            this.MenuItem_NewFolder.Text = "新建文件夹";
            this.MenuItem_NewFolder.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // MenuItem_ModifyFolder
            // 
            this.MenuItem_ModifyFolder.Name = "MenuItem_ModifyFolder";
            this.MenuItem_ModifyFolder.Size = new System.Drawing.Size(152, 22);
            this.MenuItem_ModifyFolder.Text = "修改文件夹";
            this.MenuItem_ModifyFolder.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // MenuItem_DeleteFolder
            // 
            this.MenuItem_DeleteFolder.Name = "MenuItem_DeleteFolder";
            this.MenuItem_DeleteFolder.Size = new System.Drawing.Size(152, 22);
            this.MenuItem_DeleteFolder.Text = "删除文件夹";
            this.MenuItem_DeleteFolder.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // MenuItem_NewReport
            // 
            this.MenuItem_NewReport.Name = "MenuItem_NewReport";
            this.MenuItem_NewReport.Size = new System.Drawing.Size(152, 22);
            this.MenuItem_NewReport.Text = "新建报表";
            this.MenuItem_NewReport.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // MenuItem_ModifyReport
            // 
            this.MenuItem_ModifyReport.Name = "MenuItem_ModifyReport";
            this.MenuItem_ModifyReport.Size = new System.Drawing.Size(152, 22);
            this.MenuItem_ModifyReport.Text = "修改报表";
            this.MenuItem_ModifyReport.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // MenuItem_DeleteReport
            // 
            this.MenuItem_DeleteReport.Name = "MenuItem_DeleteReport";
            this.MenuItem_DeleteReport.Size = new System.Drawing.Size(152, 22);
            this.MenuItem_DeleteReport.Text = "删除报表";
            this.MenuItem_DeleteReport.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // ReportCatlogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 533);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HideOnClose = true;
            this.Name = "ReportCatlogForm";
            this.Text = "报表目录";
            this.Load += new System.EventHandler(this.ReportCatlogForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton NewFolderButton;
        private System.Windows.Forms.ToolStripButton ModifyFolderButton;
        private System.Windows.Forms.ToolStripButton DeleteFolderButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton NewReportButton;
        private System.Windows.Forms.ToolStripButton ModifyReportButton;
        private System.Windows.Forms.ToolStripButton DeleteReportButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_NewFolder;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_ModifyFolder;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_DeleteFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_NewReport;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_ModifyReport;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_DeleteReport;
        private System.Windows.Forms.ImageList imageList1;


    }
}