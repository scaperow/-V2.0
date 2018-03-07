namespace Yqun.Permissions
{
    partial class AuthorizationManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthorizationManager));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.SpreadContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SelectedItems = new System.Windows.Forms.ToolStripMenuItem();
            this.UnSelectedItems = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.collapsibleSplitter1 = new Yqun.Controls.CollapsibleSplitter();
            this.RoleListContainer = new System.Windows.Forms.TabControl();
            this.AuthObjectList = new System.Windows.Forms.TabPage();
            this.RolesView = new System.Windows.Forms.TreeView();
            this.ToolStrip2 = new System.Windows.Forms.ToolStrip();
            this.NewRoleButton = new System.Windows.Forms.ToolStripButton();
            this.EditRoleButton = new System.Windows.Forms.ToolStripButton();
            this.DeleteRoleButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveAuthButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.AuthRow = new System.Windows.Forms.Panel();
            this.RecordsView = new System.Windows.Forms.TreeView();
            this.AuthColumn = new System.Windows.Forms.Panel();
            this.DataColumnsSpread = new FarPoint.Win.Spread.FpSpread();
            this.DataColumnsSpread_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.AuthFunction = new System.Windows.Forms.Panel();
            this.FunctionsSpread = new FarPoint.Win.Spread.FpSpread();
            this.FunctionsSpread_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.AuthData = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SpreadContextMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.RoleListContainer.SuspendLayout();
            this.AuthObjectList.SuspendLayout();
            this.ToolStrip2.SuspendLayout();
            this.AuthRow.SuspendLayout();
            this.AuthColumn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataColumnsSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataColumnsSpread_Sheet1)).BeginInit();
            this.AuthFunction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionsSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionsSpread_Sheet1)).BeginInit();
            this.AuthData.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "默认.png");
            this.imageList1.Images.SetKeyName(1, "移除.png");
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "根结构.bmp");
            this.imageList2.Images.SetKeyName(1, "单位或部门.png");
            this.imageList2.Images.SetKeyName(2, "角色.png");
            // 
            // SpreadContextMenu
            // 
            this.SpreadContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SelectedItems,
            this.UnSelectedItems});
            this.SpreadContextMenu.Name = "SpreadContextMenu";
            this.SpreadContextMenu.Size = new System.Drawing.Size(142, 48);
            this.SpreadContextMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.SpreadContextMenu_ItemClicked);
            // 
            // SelectedItems
            // 
            this.SelectedItems.Name = "SelectedItems";
            this.SelectedItems.Size = new System.Drawing.Size(141, 22);
            this.SelectedItems.Text = "选中(&S)";
            this.SelectedItems.ToolTipText = "选中所有选项";
            // 
            // UnSelectedItems
            // 
            this.UnSelectedItems.Name = "UnSelectedItems";
            this.UnSelectedItems.Size = new System.Drawing.Size(141, 22);
            this.UnSelectedItems.Text = "取消选中(&U)";
            this.UnSelectedItems.ToolTipText = "取消选中选项";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.collapsibleSplitter1);
            this.panel1.Controls.Add(this.RoleListContainer);
            this.panel1.Controls.Add(this.ToolStrip2);
            this.panel1.Controls.Add(this.AuthRow);
            this.panel1.Controls.Add(this.AuthColumn);
            this.panel1.Controls.Add(this.AuthFunction);
            this.panel1.Controls.Add(this.AuthData);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(7, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(818, 612);
            this.panel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(317, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(501, 587);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 18;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // collapsibleSplitter1
            // 
            this.collapsibleSplitter1.AnimationDelay = 20;
            this.collapsibleSplitter1.AnimationStep = 20;
            this.collapsibleSplitter1.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
            this.collapsibleSplitter1.ControlToHide = this.RoleListContainer;
            this.collapsibleSplitter1.ExpandParentForm = false;
            this.collapsibleSplitter1.Location = new System.Drawing.Point(309, 25);
            this.collapsibleSplitter1.Name = "collapsibleSplitter1";
            this.collapsibleSplitter1.TabIndex = 16;
            this.collapsibleSplitter1.TabStop = false;
            this.collapsibleSplitter1.UseAnimations = false;
            this.collapsibleSplitter1.VisualStyle = Yqun.Controls.VisualStyles.XP;
            // 
            // RoleListContainer
            // 
            this.RoleListContainer.Controls.Add(this.AuthObjectList);
            this.RoleListContainer.Dock = System.Windows.Forms.DockStyle.Left;
            this.RoleListContainer.Location = new System.Drawing.Point(0, 25);
            this.RoleListContainer.Name = "RoleListContainer";
            this.RoleListContainer.SelectedIndex = 0;
            this.RoleListContainer.Size = new System.Drawing.Size(309, 587);
            this.RoleListContainer.TabIndex = 15;
            // 
            // AuthObjectList
            // 
            this.AuthObjectList.Controls.Add(this.RolesView);
            this.AuthObjectList.Location = new System.Drawing.Point(4, 22);
            this.AuthObjectList.Name = "AuthObjectList";
            this.AuthObjectList.Padding = new System.Windows.Forms.Padding(3);
            this.AuthObjectList.Size = new System.Drawing.Size(301, 561);
            this.AuthObjectList.TabIndex = 0;
            this.AuthObjectList.Text = " 岗位列表 ";
            this.AuthObjectList.UseVisualStyleBackColor = true;
            // 
            // RolesView
            // 
            this.RolesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RolesView.FullRowSelect = true;
            this.RolesView.HideSelection = false;
            this.RolesView.ImageIndex = 0;
            this.RolesView.ImageList = this.imageList2;
            this.RolesView.Location = new System.Drawing.Point(3, 3);
            this.RolesView.Name = "RolesView";
            this.RolesView.SelectedImageIndex = 0;
            this.RolesView.Size = new System.Drawing.Size(295, 555);
            this.RolesView.TabIndex = 0;
            this.RolesView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.RolesView_AfterSelect);
            this.RolesView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.RolesView_NodeMouseClick);
            // 
            // ToolStrip2
            // 
            this.ToolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewRoleButton,
            this.EditRoleButton,
            this.DeleteRoleButton,
            this.toolStripSeparator1,
            this.SaveAuthButton,
            this.toolStripSeparator3});
            this.ToolStrip2.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip2.Name = "ToolStrip2";
            this.ToolStrip2.Size = new System.Drawing.Size(818, 25);
            this.ToolStrip2.TabIndex = 14;
            this.ToolStrip2.Text = "toolStrip2";
            // 
            // NewRoleButton
            // 
            this.NewRoleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.NewRoleButton.Image = ((System.Drawing.Image)(resources.GetObject("NewRoleButton.Image")));
            this.NewRoleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewRoleButton.Name = "NewRoleButton";
            this.NewRoleButton.Size = new System.Drawing.Size(60, 22);
            this.NewRoleButton.Text = "新建岗位";
            this.NewRoleButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // EditRoleButton
            // 
            this.EditRoleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.EditRoleButton.Image = ((System.Drawing.Image)(resources.GetObject("EditRoleButton.Image")));
            this.EditRoleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditRoleButton.Name = "EditRoleButton";
            this.EditRoleButton.Size = new System.Drawing.Size(60, 22);
            this.EditRoleButton.Text = "编辑岗位";
            this.EditRoleButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // DeleteRoleButton
            // 
            this.DeleteRoleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DeleteRoleButton.Image = ((System.Drawing.Image)(resources.GetObject("DeleteRoleButton.Image")));
            this.DeleteRoleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteRoleButton.Name = "DeleteRoleButton";
            this.DeleteRoleButton.Size = new System.Drawing.Size(60, 22);
            this.DeleteRoleButton.Text = "删除岗位";
            this.DeleteRoleButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // SaveAuthButton
            // 
            this.SaveAuthButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.SaveAuthButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveAuthButton.Image")));
            this.SaveAuthButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveAuthButton.Name = "SaveAuthButton";
            this.SaveAuthButton.Size = new System.Drawing.Size(108, 22);
            this.SaveAuthButton.Text = "保存选中岗位权限";
            this.SaveAuthButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // AuthRow
            // 
            this.AuthRow.Controls.Add(this.RecordsView);
            this.AuthRow.Location = new System.Drawing.Point(222, 72);
            this.AuthRow.Name = "AuthRow";
            this.AuthRow.Size = new System.Drawing.Size(267, 223);
            this.AuthRow.TabIndex = 20;
            // 
            // RecordsView
            // 
            this.RecordsView.CheckBoxes = true;
            this.RecordsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RecordsView.FullRowSelect = true;
            this.RecordsView.HideSelection = false;
            this.RecordsView.Location = new System.Drawing.Point(0, 0);
            this.RecordsView.Name = "RecordsView";
            this.RecordsView.Size = new System.Drawing.Size(267, 223);
            this.RecordsView.TabIndex = 0;
            this.RecordsView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.RecordsView_NodeMouseClick);
            // 
            // AuthColumn
            // 
            this.AuthColumn.Controls.Add(this.DataColumnsSpread);
            this.AuthColumn.Location = new System.Drawing.Point(520, 72);
            this.AuthColumn.Name = "AuthColumn";
            this.AuthColumn.Size = new System.Drawing.Size(257, 223);
            this.AuthColumn.TabIndex = 21;
            // 
            // DataColumnsSpread
            // 
            this.DataColumnsSpread.AccessibleDescription = "";
            this.DataColumnsSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataColumnsSpread.Location = new System.Drawing.Point(0, 0);
            this.DataColumnsSpread.Name = "DataColumnsSpread";
            this.DataColumnsSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.DataColumnsSpread_Sheet1});
            this.DataColumnsSpread.Size = new System.Drawing.Size(257, 223);
            this.DataColumnsSpread.TabIndex = 0;
            this.DataColumnsSpread.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.Spread_CellClick);
            this.DataColumnsSpread.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(this.Spread_SelectionChanged);
            this.DataColumnsSpread.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.Spread_CellDoubleClick);
            // 
            // DataColumnsSpread_Sheet1
            // 
            this.DataColumnsSpread_Sheet1.Reset();
            this.DataColumnsSpread_Sheet1.SheetName = "Sheet1";
            // 
            // AuthFunction
            // 
            this.AuthFunction.Controls.Add(this.FunctionsSpread);
            this.AuthFunction.Location = new System.Drawing.Point(222, 310);
            this.AuthFunction.Name = "AuthFunction";
            this.AuthFunction.Size = new System.Drawing.Size(267, 235);
            this.AuthFunction.TabIndex = 19;
            // 
            // FunctionsSpread
            // 
            this.FunctionsSpread.AccessibleDescription = "";
            this.FunctionsSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FunctionsSpread.Location = new System.Drawing.Point(0, 0);
            this.FunctionsSpread.Name = "FunctionsSpread";
            this.FunctionsSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FunctionsSpread_Sheet1});
            this.FunctionsSpread.Size = new System.Drawing.Size(267, 235);
            this.FunctionsSpread.TabIndex = 0;
            this.FunctionsSpread.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.Spread_CellClick);
            this.FunctionsSpread.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(this.Spread_SelectionChanged);
            this.FunctionsSpread.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.Spread_CellDoubleClick);
            // 
            // FunctionsSpread_Sheet1
            // 
            this.FunctionsSpread_Sheet1.Reset();
            this.FunctionsSpread_Sheet1.SheetName = "Sheet1";
            // 
            // AuthData
            // 
            this.AuthData.Controls.Add(this.groupBox3);
            this.AuthData.Controls.Add(this.groupBox2);
            this.AuthData.Controls.Add(this.comboBox1);
            this.AuthData.Controls.Add(this.label1);
            this.AuthData.Location = new System.Drawing.Point(281, 256);
            this.AuthData.Name = "AuthData";
            this.AuthData.Size = new System.Drawing.Size(364, 315);
            this.AuthData.TabIndex = 22;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.treeView2);
            this.groupBox3.Location = new System.Drawing.Point(188, 45);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(173, 244);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "选择禁止查看字段数据";
            // 
            // treeView2
            // 
            this.treeView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView2.ImageIndex = 0;
            this.treeView2.ImageList = this.imageList1;
            this.treeView2.Location = new System.Drawing.Point(3, 17);
            this.treeView2.Name = "treeView2";
            this.treeView2.SelectedImageIndex = 0;
            this.treeView2.Size = new System.Drawing.Size(167, 224);
            this.treeView2.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.treeView1);
            this.groupBox2.Location = new System.Drawing.Point(9, 45);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(171, 244);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "选择字段条件";
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(3, 17);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(165, 224);
            this.treeView1.TabIndex = 0;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(91, 17);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(34, 20);
            this.comboBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择数据表：";
            // 
            // AuthorizationManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "AuthorizationManager";
            this.Padding = new System.Windows.Forms.Padding(7, 0, 7, 7);
            this.Size = new System.Drawing.Size(832, 619);
            this.Click += new System.EventHandler(this.ToolStripButton_Click);
            this.SpreadContextMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.RoleListContainer.ResumeLayout(false);
            this.AuthObjectList.ResumeLayout(false);
            this.ToolStrip2.ResumeLayout(false);
            this.ToolStrip2.PerformLayout();
            this.AuthRow.ResumeLayout(false);
            this.AuthColumn.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataColumnsSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataColumnsSpread_Sheet1)).EndInit();
            this.AuthFunction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FunctionsSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionsSpread_Sheet1)).EndInit();
            this.AuthData.ResumeLayout(false);
            this.AuthData.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView RecordsView;
        private System.Windows.Forms.ToolStrip ToolStrip2;
        private System.Windows.Forms.ToolStripButton NewRoleButton;
        private System.Windows.Forms.ToolStripButton EditRoleButton;
        private System.Windows.Forms.ToolStripButton DeleteRoleButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton SaveAuthButton;
        private Yqun.Controls.CollapsibleSplitter collapsibleSplitter1;
        private System.Windows.Forms.TabControl RoleListContainer;
        private System.Windows.Forms.TabPage AuthObjectList;
        private System.Windows.Forms.TreeView RolesView;
        private FarPoint.Win.Spread.FpSpread FunctionsSpread;
        private FarPoint.Win.Spread.SheetView FunctionsSpread_Sheet1;
        private FarPoint.Win.Spread.FpSpread DataColumnsSpread;
        private FarPoint.Win.Spread.SheetView DataColumnsSpread_Sheet1;
        private System.Windows.Forms.ContextMenuStrip SpreadContextMenu;
        private System.Windows.Forms.ToolStripMenuItem SelectedItems;
        private System.Windows.Forms.ToolStripMenuItem UnSelectedItems;
        private System.Windows.Forms.Panel AuthColumn;
        private System.Windows.Forms.Panel AuthRow;
        private System.Windows.Forms.Panel AuthFunction;
        private System.Windows.Forms.Panel AuthData;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TreeView treeView2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.TabControl tabControl1;

    }
}
