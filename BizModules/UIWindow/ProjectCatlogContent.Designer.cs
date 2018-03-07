namespace BizModules
{
    partial class ProjectCatlogContent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectCatlogContent));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.MainListTree = new System.Windows.Forms.TreeView();
            this.ProImageList = new System.Windows.Forms.ImageList(this.components);
            this.ProjectContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NewDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TaizhangSettingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BatchPrintMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.SystemNewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewProjectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewCompanyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewSectionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditProjectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditDepMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditPrjsctMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SystemDeleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteProjectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteCompanyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteSectionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteModelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.QuaAuthMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TestTemperatureMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LineMenuItemForDevice = new System.Windows.Forms.ToolStripSeparator();
            this.DeviceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateDeviceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeviceManagementItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PXRelationReportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.ProjectContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(252, 513);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.MainListTree);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(244, 487);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "工程结构";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // MainListTree
            // 
            this.MainListTree.AllowDrop = true;
            this.MainListTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainListTree.HideSelection = false;
            this.MainListTree.ImageIndex = 0;
            this.MainListTree.ImageList = this.ProImageList;
            this.MainListTree.Location = new System.Drawing.Point(3, 3);
            this.MainListTree.Name = "MainListTree";
            this.MainListTree.SelectedImageIndex = 0;
            this.MainListTree.Size = new System.Drawing.Size(238, 481);
            this.MainListTree.TabIndex = 0;
            this.MainListTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.MainListTree_NodeMouseDoubleClick);
            this.MainListTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.MainListTree_AfterSelect);
            this.MainListTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.MainListTree_NodeMouseClick);
            // 
            // ProImageList
            // 
            this.ProImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ProImageList.ImageStream")));
            this.ProImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ProImageList.Images.SetKeyName(0, "工程列表.png");
            this.ProImageList.Images.SetKeyName(1, "关闭工程.bmp");
            this.ProImageList.Images.SetKeyName(2, "打开工程.bmp");
            this.ProImageList.Images.SetKeyName(3, "标段.png");
            this.ProImageList.Images.SetKeyName(4, "单位.png");
            this.ProImageList.Images.SetKeyName(5, "文件夹.png");
            this.ProImageList.Images.SetKeyName(6, "表单.png");
            // 
            // ProjectContextMenu
            // 
            this.ProjectContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewDataMenuItem,
            this.TaizhangSettingMenuItem,
            this.ExportDataMenuItem,
            this.BatchPrintMenuItem,
            this.toolStripMenuItem1,
            this.SystemNewMenuItem,
            this.EditMenuItem,
            this.SystemDeleteMenuItem,
            this.toolStripSeparator1,
            this.QuaAuthMenuItem,
            this.TestTemperatureMenuItem,
            this.LineMenuItemForDevice,
            this.DeviceMenuItem,
            this.PXRelationReportMenuItem});
            this.ProjectContextMenu.Name = "ProjectContextMenu";
            this.ProjectContextMenu.Size = new System.Drawing.Size(173, 286);
            // 
            // NewDataMenuItem
            // 
            this.NewDataMenuItem.Name = "NewDataMenuItem";
            this.NewDataMenuItem.Size = new System.Drawing.Size(172, 22);
            this.NewDataMenuItem.Tag = "@module_newdata";
            this.NewDataMenuItem.Text = "新建资料";
            this.NewDataMenuItem.Click += new System.EventHandler(this.ProjectContextMenu_Click);
            // 
            // TaizhangSettingMenuItem
            // 
            this.TaizhangSettingMenuItem.Name = "TaizhangSettingMenuItem";
            this.TaizhangSettingMenuItem.Size = new System.Drawing.Size(172, 22);
            this.TaizhangSettingMenuItem.Tag = "@module_modesetup";
            this.TaizhangSettingMenuItem.Text = "台账设置";
            this.TaizhangSettingMenuItem.Click += new System.EventHandler(this.ProjectContextMenu_Click);
            // 
            // ExportDataMenuItem
            // 
            this.ExportDataMenuItem.Name = "ExportDataMenuItem";
            this.ExportDataMenuItem.Size = new System.Drawing.Size(172, 22);
            this.ExportDataMenuItem.Tag = "@ExportData";
            this.ExportDataMenuItem.Text = "导出资料";
            this.ExportDataMenuItem.Click += new System.EventHandler(this.ProjectContextMenu_Click);
            // 
            // BatchPrintMenuItem
            // 
            this.BatchPrintMenuItem.Name = "BatchPrintMenuItem";
            this.BatchPrintMenuItem.Size = new System.Drawing.Size(172, 22);
            this.BatchPrintMenuItem.Tag = "@BatchPrint";
            this.BatchPrintMenuItem.Text = "打印资料";
            this.BatchPrintMenuItem.Click += new System.EventHandler(this.ProjectContextMenu_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(169, 6);
            // 
            // SystemNewMenuItem
            // 
            this.SystemNewMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewProjectMenuItem,
            this.NewCompanyMenuItem,
            this.NewSectionMenuItem,
            this.NewFolderMenuItem});
            this.SystemNewMenuItem.Name = "SystemNewMenuItem";
            this.SystemNewMenuItem.Size = new System.Drawing.Size(172, 22);
            this.SystemNewMenuItem.Tag = "@new";
            this.SystemNewMenuItem.Text = "新建";
            this.SystemNewMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // NewProjectMenuItem
            // 
            this.NewProjectMenuItem.Name = "NewProjectMenuItem";
            this.NewProjectMenuItem.Size = new System.Drawing.Size(136, 22);
            this.NewProjectMenuItem.Tag = "@top_newproject";
            this.NewProjectMenuItem.Text = "新建工程";
            this.NewProjectMenuItem.Click += new System.EventHandler(this.ProjectContextMenu_Click);
            // 
            // NewCompanyMenuItem
            // 
            this.NewCompanyMenuItem.Name = "NewCompanyMenuItem";
            this.NewCompanyMenuItem.Size = new System.Drawing.Size(136, 22);
            this.NewCompanyMenuItem.Tag = "@eng_newdepartment";
            this.NewCompanyMenuItem.Text = "新建单位";
            this.NewCompanyMenuItem.Click += new System.EventHandler(this.ProjectContextMenu_Click);
            // 
            // NewSectionMenuItem
            // 
            this.NewSectionMenuItem.Name = "NewSectionMenuItem";
            this.NewSectionMenuItem.Size = new System.Drawing.Size(136, 22);
            this.NewSectionMenuItem.Tag = "@unit_newprjsct";
            this.NewSectionMenuItem.Text = "新建标段";
            this.NewSectionMenuItem.Click += new System.EventHandler(this.ProjectContextMenu_Click);
            // 
            // NewFolderMenuItem
            // 
            this.NewFolderMenuItem.Name = "NewFolderMenuItem";
            this.NewFolderMenuItem.Size = new System.Drawing.Size(136, 22);
            this.NewFolderMenuItem.Tag = "@tenders_newfolder";
            this.NewFolderMenuItem.Text = "新建文件夹";
            this.NewFolderMenuItem.Click += new System.EventHandler(this.ProjectContextMenu_Click);
            // 
            // EditMenuItem
            // 
            this.EditMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditProjectMenuItem,
            this.EditDepMenuItem,
            this.EditPrjsctMenuItem,
            this.EditFolderMenuItem});
            this.EditMenuItem.Name = "EditMenuItem";
            this.EditMenuItem.Size = new System.Drawing.Size(172, 22);
            this.EditMenuItem.Tag = "@edit";
            this.EditMenuItem.Text = "编辑";
            // 
            // EditProjectMenuItem
            // 
            this.EditProjectMenuItem.Name = "EditProjectMenuItem";
            this.EditProjectMenuItem.Size = new System.Drawing.Size(136, 22);
            this.EditProjectMenuItem.Tag = "@eng_editproject";
            this.EditProjectMenuItem.Text = "编辑工程";
            this.EditProjectMenuItem.Click += new System.EventHandler(this.ProjectContextMenu_Click);
            // 
            // EditDepMenuItem
            // 
            this.EditDepMenuItem.Name = "EditDepMenuItem";
            this.EditDepMenuItem.Size = new System.Drawing.Size(136, 22);
            this.EditDepMenuItem.Tag = "@unit_editdepartment";
            this.EditDepMenuItem.Text = "编辑单位";
            this.EditDepMenuItem.Click += new System.EventHandler(this.ProjectContextMenu_Click);
            // 
            // EditPrjsctMenuItem
            // 
            this.EditPrjsctMenuItem.Name = "EditPrjsctMenuItem";
            this.EditPrjsctMenuItem.Size = new System.Drawing.Size(136, 22);
            this.EditPrjsctMenuItem.Tag = "@tenders_editcontract";
            this.EditPrjsctMenuItem.Text = "编辑标段";
            this.EditPrjsctMenuItem.Click += new System.EventHandler(this.ProjectContextMenu_Click);
            // 
            // EditFolderMenuItem
            // 
            this.EditFolderMenuItem.Name = "EditFolderMenuItem";
            this.EditFolderMenuItem.Size = new System.Drawing.Size(136, 22);
            this.EditFolderMenuItem.Tag = "@folder_editfolder";
            this.EditFolderMenuItem.Text = "编辑文件夹";
            this.EditFolderMenuItem.Click += new System.EventHandler(this.ProjectContextMenu_Click);
            // 
            // SystemDeleteMenuItem
            // 
            this.SystemDeleteMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteProjectMenuItem,
            this.DeleteCompanyMenuItem,
            this.DeleteSectionMenuItem,
            this.DeleteFolderMenuItem,
            this.DeleteModelMenuItem});
            this.SystemDeleteMenuItem.Name = "SystemDeleteMenuItem";
            this.SystemDeleteMenuItem.Size = new System.Drawing.Size(172, 22);
            this.SystemDeleteMenuItem.Tag = "@del";
            this.SystemDeleteMenuItem.Text = "删除";
            this.SystemDeleteMenuItem.Click += new System.EventHandler(this.ProjectContextMenu_Click);
            // 
            // DeleteProjectMenuItem
            // 
            this.DeleteProjectMenuItem.Name = "DeleteProjectMenuItem";
            this.DeleteProjectMenuItem.Size = new System.Drawing.Size(136, 22);
            this.DeleteProjectMenuItem.Tag = "@eng_deleproject";
            this.DeleteProjectMenuItem.Text = "删除工程";
            this.DeleteProjectMenuItem.Click += new System.EventHandler(this.ProjectContextMenu_Click);
            // 
            // DeleteCompanyMenuItem
            // 
            this.DeleteCompanyMenuItem.Name = "DeleteCompanyMenuItem";
            this.DeleteCompanyMenuItem.Size = new System.Drawing.Size(136, 22);
            this.DeleteCompanyMenuItem.Tag = "@unit_deledepartment";
            this.DeleteCompanyMenuItem.Text = "删除单位";
            this.DeleteCompanyMenuItem.Click += new System.EventHandler(this.ProjectContextMenu_Click);
            // 
            // DeleteSectionMenuItem
            // 
            this.DeleteSectionMenuItem.Name = "DeleteSectionMenuItem";
            this.DeleteSectionMenuItem.Size = new System.Drawing.Size(136, 22);
            this.DeleteSectionMenuItem.Tag = "@tenders_delecontract";
            this.DeleteSectionMenuItem.Text = "删除标段";
            this.DeleteSectionMenuItem.Click += new System.EventHandler(this.ProjectContextMenu_Click);
            // 
            // DeleteFolderMenuItem
            // 
            this.DeleteFolderMenuItem.Name = "DeleteFolderMenuItem";
            this.DeleteFolderMenuItem.Size = new System.Drawing.Size(136, 22);
            this.DeleteFolderMenuItem.Tag = "@folder_delefolder";
            this.DeleteFolderMenuItem.Text = "删除文件夹";
            this.DeleteFolderMenuItem.Click += new System.EventHandler(this.ProjectContextMenu_Click);
            // 
            // DeleteModelMenuItem
            // 
            this.DeleteModelMenuItem.Name = "DeleteModelMenuItem";
            this.DeleteModelMenuItem.Size = new System.Drawing.Size(136, 22);
            this.DeleteModelMenuItem.Tag = "@module_delemodule";
            this.DeleteModelMenuItem.Text = "删除模板";
            this.DeleteModelMenuItem.Click += new System.EventHandler(this.ProjectContextMenu_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(169, 6);
            // 
            // QuaAuthMenuItem
            // 
            this.QuaAuthMenuItem.Name = "QuaAuthMenuItem";
            this.QuaAuthMenuItem.Size = new System.Drawing.Size(172, 22);
            this.QuaAuthMenuItem.Tag = "@quaauth";
            this.QuaAuthMenuItem.Text = "资质授权";
            this.QuaAuthMenuItem.Click += new System.EventHandler(this.ProjectContextMenu_Click);
            // 
            // TestTemperatureMenuItem
            // 
            this.TestTemperatureMenuItem.Name = "TestTemperatureMenuItem";
            this.TestTemperatureMenuItem.Size = new System.Drawing.Size(172, 22);
            this.TestTemperatureMenuItem.Tag = "@teststadiumtemperature";
            this.TestTemperatureMenuItem.Text = "温度试验记录";
            this.TestTemperatureMenuItem.Click += new System.EventHandler(this.ProjectContextMenu_Click);
            // 
            // LineMenuItemForDevice
            // 
            this.LineMenuItemForDevice.Name = "LineMenuItemForDevice";
            this.LineMenuItemForDevice.Size = new System.Drawing.Size(169, 6);
            // 
            // DeviceMenuItem
            // 
            this.DeviceMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateDeviceMenuItem,
            this.DeviceManagementItem});
            this.DeviceMenuItem.Name = "DeviceMenuItem";
            this.DeviceMenuItem.Size = new System.Drawing.Size(172, 22);
            this.DeviceMenuItem.Tag = "";
            this.DeviceMenuItem.Text = "设备管理";
            // 
            // CreateDeviceMenuItem
            // 
            this.CreateDeviceMenuItem.Name = "CreateDeviceMenuItem";
            this.CreateDeviceMenuItem.Size = new System.Drawing.Size(152, 22);
            this.CreateDeviceMenuItem.Tag = "@createdevice";
            this.CreateDeviceMenuItem.Text = "添加设备";
            this.CreateDeviceMenuItem.Click += new System.EventHandler(this.ProjectContextMenu_Click);
            // 
            // DeviceManagementItem
            // 
            this.DeviceManagementItem.Name = "DeviceManagementItem";
            this.DeviceManagementItem.Size = new System.Drawing.Size(152, 22);
            this.DeviceManagementItem.Tag = "@devicemanagement";
            this.DeviceManagementItem.Text = "管理所有设备";
            this.DeviceManagementItem.Click += new System.EventHandler(this.ProjectContextMenu_Click);
            // 
            // PXRelationReportMenuItem
            // 
            this.PXRelationReportMenuItem.Name = "PXRelationReportMenuItem";
            this.PXRelationReportMenuItem.Size = new System.Drawing.Size(172, 22);
            this.PXRelationReportMenuItem.Tag = "@pxrelationreport";
            this.PXRelationReportMenuItem.Text = "平行关系对应查询";
            this.PXRelationReportMenuItem.Click += new System.EventHandler(this.ProjectContextMenu_Click);
            // 
            // ProjectCatlogContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(252, 513);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HideOnClose = true;
            this.Name = "ProjectCatlogContent";
            this.Text = "工程列表";
            this.Load += new System.EventHandler(this.ProjectCatlogContent_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ProjectContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ImageList ProImageList;
        public System.Windows.Forms.TreeView MainListTree;
        private System.Windows.Forms.ToolStripMenuItem NewDataMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExportDataMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TaizhangSettingMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem SystemNewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewProjectMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewCompanyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewSectionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewFolderMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SystemDeleteMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteProjectMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteCompanyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteSectionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteFolderMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BatchPrintMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditProjectMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditDepMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditPrjsctMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditFolderMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteModelMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem QuaAuthMenuItem;
        internal System.Windows.Forms.ContextMenuStrip ProjectContextMenu;
        private System.Windows.Forms.ToolStripMenuItem TestTemperatureMenuItem;
        private System.Windows.Forms.ToolStripSeparator LineMenuItemForDevice;
        private System.Windows.Forms.ToolStripMenuItem DeviceMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CreateDeviceMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeviceManagementItem;
        private System.Windows.Forms.ToolStripMenuItem MenuTemperatureSet;
        private System.Windows.Forms.ToolStripMenuItem PXRelationReportMenuItem;

    }
}