namespace Yqun.Permissions
{
    partial class OrganizationStructManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrganizationStructManager));
            this.OrganizationView = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ToolStripButton_NewUser = new System.Windows.Forms.ToolStripButton();
            this.ToolStripButton_EditUser = new System.Windows.Forms.ToolStripButton();
            this.ToolStripButton_DeleteUser = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripButton_UserAuth = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenu_NewUser = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_EditUser = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_DeleteUser = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.TreeDevices = new System.Windows.Forms.TreeView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.LabelDeviceRoles = new System.Windows.Forms.ToolStripLabel();
            this.ButtonSaveDeviceRole = new System.Windows.Forms.ToolStripButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ToolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // OrganizationView
            // 
            this.OrganizationView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OrganizationView.FullRowSelect = true;
            this.OrganizationView.HideSelection = false;
            this.OrganizationView.ImageIndex = 0;
            this.OrganizationView.ImageList = this.imageList1;
            this.OrganizationView.Location = new System.Drawing.Point(0, 25);
            this.OrganizationView.Name = "OrganizationView";
            this.OrganizationView.SelectedImageIndex = 0;
            this.OrganizationView.Size = new System.Drawing.Size(446, 544);
            this.OrganizationView.TabIndex = 5;
            this.OrganizationView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OrganizationView_MouseClick);
            this.OrganizationView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OrganizationView_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "根结构.bmp");
            this.imageList1.Images.SetKeyName(1, "单位或部门.png");
            this.imageList1.Images.SetKeyName(2, "用户.bmp");
            this.imageList1.Images.SetKeyName(3, "设备.png");
            // 
            // ToolStrip1
            // 
            this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripButton_NewUser,
            this.ToolStripButton_EditUser,
            this.ToolStripButton_DeleteUser,
            this.toolStripSeparator2,
            this.ToolStripButton_UserAuth});
            this.ToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Size = new System.Drawing.Size(446, 25);
            this.ToolStrip1.TabIndex = 6;
            this.ToolStrip1.Text = "toolStrip1";
            // 
            // ToolStripButton_NewUser
            // 
            this.ToolStripButton_NewUser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ToolStripButton_NewUser.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButton_NewUser.Image")));
            this.ToolStripButton_NewUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButton_NewUser.Name = "ToolStripButton_NewUser";
            this.ToolStripButton_NewUser.Size = new System.Drawing.Size(60, 22);
            this.ToolStripButton_NewUser.Text = "新建用户";
            this.ToolStripButton_NewUser.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // ToolStripButton_EditUser
            // 
            this.ToolStripButton_EditUser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ToolStripButton_EditUser.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButton_EditUser.Image")));
            this.ToolStripButton_EditUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButton_EditUser.Name = "ToolStripButton_EditUser";
            this.ToolStripButton_EditUser.Size = new System.Drawing.Size(60, 22);
            this.ToolStripButton_EditUser.Text = "编辑用户";
            this.ToolStripButton_EditUser.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // ToolStripButton_DeleteUser
            // 
            this.ToolStripButton_DeleteUser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ToolStripButton_DeleteUser.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButton_DeleteUser.Image")));
            this.ToolStripButton_DeleteUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButton_DeleteUser.Name = "ToolStripButton_DeleteUser";
            this.ToolStripButton_DeleteUser.Size = new System.Drawing.Size(60, 22);
            this.ToolStripButton_DeleteUser.Text = "删除用户";
            this.ToolStripButton_DeleteUser.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // ToolStripButton_UserAuth
            // 
            this.ToolStripButton_UserAuth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ToolStripButton_UserAuth.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButton_UserAuth.Image")));
            this.ToolStripButton_UserAuth.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButton_UserAuth.Name = "ToolStripButton_UserAuth";
            this.ToolStripButton_UserAuth.Size = new System.Drawing.Size(60, 22);
            this.ToolStripButton_UserAuth.Text = "岗位分配";
            this.ToolStripButton_UserAuth.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenu_NewUser,
            this.ContextMenu_EditUser,
            this.ContextMenu_DeleteUser});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 70);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // ContextMenu_NewUser
            // 
            this.ContextMenu_NewUser.Name = "ContextMenu_NewUser";
            this.ContextMenu_NewUser.Size = new System.Drawing.Size(124, 22);
            this.ContextMenu_NewUser.Text = "新建用户";
            this.ContextMenu_NewUser.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // ContextMenu_EditUser
            // 
            this.ContextMenu_EditUser.Name = "ContextMenu_EditUser";
            this.ContextMenu_EditUser.Size = new System.Drawing.Size(124, 22);
            this.ContextMenu_EditUser.Text = "编辑用户";
            this.ContextMenu_EditUser.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // ContextMenu_DeleteUser
            // 
            this.ContextMenu_DeleteUser.Name = "ContextMenu_DeleteUser";
            this.ContextMenu_DeleteUser.Size = new System.Drawing.Size(124, 22);
            this.ContextMenu_DeleteUser.Text = "删除用户";
            this.ContextMenu_DeleteUser.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.OrganizationView);
            this.splitContainer1.Panel1.Controls.Add(this.ToolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.TreeDevices);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip2);
            this.splitContainer1.Size = new System.Drawing.Size(755, 569);
            this.splitContainer1.SplitterDistance = 446;
            this.splitContainer1.TabIndex = 9;
            // 
            // TreeDevices
            // 
            this.TreeDevices.CheckBoxes = true;
            this.TreeDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeDevices.FullRowSelect = true;
            this.TreeDevices.ImageIndex = 0;
            this.TreeDevices.ImageList = this.imageList1;
            this.TreeDevices.Location = new System.Drawing.Point(0, 25);
            this.TreeDevices.Name = "TreeDevices";
            this.TreeDevices.SelectedImageIndex = 0;
            this.TreeDevices.Size = new System.Drawing.Size(305, 544);
            this.TreeDevices.TabIndex = 9;
            this.TreeDevices.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TreeDevices_AfterCheck);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LabelDeviceRoles,
            this.ButtonSaveDeviceRole});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(305, 25);
            this.toolStrip2.TabIndex = 8;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // LabelDeviceRoles
            // 
            this.LabelDeviceRoles.Name = "LabelDeviceRoles";
            this.LabelDeviceRoles.Size = new System.Drawing.Size(92, 22);
            this.LabelDeviceRoles.Text = "可以操作的设备";
            // 
            // ButtonSaveDeviceRole
            // 
            this.ButtonSaveDeviceRole.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ButtonSaveDeviceRole.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ButtonSaveDeviceRole.Image = ((System.Drawing.Image)(resources.GetObject("ButtonSaveDeviceRole.Image")));
            this.ButtonSaveDeviceRole.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonSaveDeviceRole.Name = "ButtonSaveDeviceRole";
            this.ButtonSaveDeviceRole.Size = new System.Drawing.Size(36, 22);
            this.ButtonSaveDeviceRole.Text = "保存";
            this.ButtonSaveDeviceRole.Click += new System.EventHandler(this.ButtonSaveDeviceRole_Click);
            // 
            // OrganizationStructManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "OrganizationStructManager";
            this.Size = new System.Drawing.Size(755, 569);
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView OrganizationView;
        private System.Windows.Forms.ToolStrip ToolStrip1;
        private System.Windows.Forms.ToolStripButton ToolStripButton_NewUser;
        private System.Windows.Forms.ToolStripButton ToolStripButton_EditUser;
        private System.Windows.Forms.ToolStripButton ToolStripButton_DeleteUser;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_NewUser;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_EditUser;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_DeleteUser;
        private System.Windows.Forms.ToolStripButton ToolStripButton_UserAuth;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView TreeDevices;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel LabelDeviceRoles;
        private System.Windows.Forms.ToolStripButton ButtonSaveDeviceRole;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
