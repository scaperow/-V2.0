namespace Yqun.Permissions
{
    partial class UserAuthDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserAuthDialog));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RoleView = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.UsersView = new System.Windows.Forms.TreeView();
            this.collapsibleSplitter1 = new Yqun.Controls.CollapsibleSplitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Button_Exit = new System.Windows.Forms.Button();
            this.Button_Ok = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RoleView);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(7, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5, 3, 5, 5);
            this.groupBox1.Size = new System.Drawing.Size(305, 481);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "岗位列表";
            // 
            // RoleView
            // 
            this.RoleView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RoleView.FullRowSelect = true;
            this.RoleView.HideSelection = false;
            this.RoleView.ImageIndex = 0;
            this.RoleView.ImageList = this.imageList1;
            this.RoleView.Location = new System.Drawing.Point(5, 17);
            this.RoleView.Name = "RoleView";
            this.RoleView.SelectedImageIndex = 0;
            this.RoleView.Size = new System.Drawing.Size(295, 459);
            this.RoleView.TabIndex = 0;
            this.RoleView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.RolesView_NodeMouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "根结构.bmp");
            this.imageList1.Images.SetKeyName(1, "单位或部门.png");
            this.imageList1.Images.SetKeyName(2, "角色.png");
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.UsersView);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(320, 52);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(5, 3, 5, 5);
            this.groupBox3.Size = new System.Drawing.Size(512, 481);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "用户列表";
            // 
            // UsersView
            // 
            this.UsersView.CheckBoxes = true;
            this.UsersView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UsersView.FullRowSelect = true;
            this.UsersView.HideSelection = false;
            this.UsersView.Location = new System.Drawing.Point(5, 17);
            this.UsersView.Name = "UsersView";
            this.UsersView.Size = new System.Drawing.Size(502, 459);
            this.UsersView.TabIndex = 0;
            this.UsersView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.UsersView_AfterCheck);
            this.UsersView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.UsersView_NodeMouseClick);
            // 
            // collapsibleSplitter1
            // 
            this.collapsibleSplitter1.AnimationDelay = 20;
            this.collapsibleSplitter1.AnimationStep = 20;
            this.collapsibleSplitter1.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
            this.collapsibleSplitter1.ControlToHide = this.RoleView;
            this.collapsibleSplitter1.ExpandParentForm = false;
            this.collapsibleSplitter1.Location = new System.Drawing.Point(312, 52);
            this.collapsibleSplitter1.Name = "collapsibleSplitter1";
            this.collapsibleSplitter1.TabIndex = 3;
            this.collapsibleSplitter1.TabStop = false;
            this.collapsibleSplitter1.UseAnimations = false;
            this.collapsibleSplitter1.VisualStyle = Yqun.Controls.VisualStyles.Win9x;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Button_Exit);
            this.panel1.Controls.Add(this.Button_Ok);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(7, 533);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(825, 39);
            this.panel1.TabIndex = 1;
            // 
            // Button_Exit
            // 
            this.Button_Exit.Location = new System.Drawing.Point(549, 8);
            this.Button_Exit.Name = "Button_Exit";
            this.Button_Exit.Size = new System.Drawing.Size(90, 23);
            this.Button_Exit.TabIndex = 2;
            this.Button_Exit.Text = "关闭";
            this.Button_Exit.UseVisualStyleBackColor = true;
            this.Button_Exit.Click += new System.EventHandler(this.Button_Exit_Click);
            // 
            // Button_Ok
            // 
            this.Button_Ok.Location = new System.Drawing.Point(444, 8);
            this.Button_Ok.Name = "Button_Ok";
            this.Button_Ok.Size = new System.Drawing.Size(90, 23);
            this.Button_Ok.TabIndex = 1;
            this.Button_Ok.Text = "确定";
            this.Button_Ok.UseVisualStyleBackColor = true;
            this.Button_Ok.Click += new System.EventHandler(this.Button_Ok_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(825, 34);
            this.label1.TabIndex = 5;
            this.label1.Text = "说明：给用户规定岗位，则具有了该岗位的权限。一个用户仅能分配一个岗位。";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(7, 43);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(825, 9);
            this.panel2.TabIndex = 6;
            // 
            // UserAuthDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 578);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.collapsibleSplitter1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserAuthDialog";
            this.Padding = new System.Windows.Forms.Padding(7, 9, 7, 6);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "岗位分配管理";
            this.Load += new System.EventHandler(this.UserAuthForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TreeView RoleView;
        private Yqun.Controls.CollapsibleSplitter collapsibleSplitter1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Button_Ok;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView UsersView;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button Button_Exit;
        private System.Windows.Forms.ImageList imageList1;
    }
}