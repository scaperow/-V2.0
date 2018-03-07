namespace Kingrocket.CJ.Components
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.cmbUserName = new System.Windows.Forms.ComboBox();
            this.txtUserPwd = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblUserPWd = new System.Windows.Forms.Label();
            this.chkShowPwd = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblClear = new System.Windows.Forms.LinkLabel();
            this.PICUpdateStatus = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblUpdateStatus = new System.Windows.Forms.Label();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PICUpdateStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbUserName
            // 
            this.cmbUserName.FormattingEnabled = true;
            this.cmbUserName.Location = new System.Drawing.Point(115, 24);
            this.cmbUserName.Name = "cmbUserName";
            this.cmbUserName.Size = new System.Drawing.Size(161, 20);
            this.cmbUserName.TabIndex = 0;
            // 
            // txtUserPwd
            // 
            this.txtUserPwd.Location = new System.Drawing.Point(115, 63);
            this.txtUserPwd.Name = "txtUserPwd";
            this.txtUserPwd.PasswordChar = '*';
            this.txtUserPwd.Size = new System.Drawing.Size(161, 21);
            this.txtUserPwd.TabIndex = 1;
            // 
            // btnLogin
            // 
            this.btnLogin.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnLogin.Location = new System.Drawing.Point(293, 202);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(72, 23);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(52, 27);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(41, 12);
            this.lblUserName.TabIndex = 4;
            this.lblUserName.Text = "用户名";
            // 
            // lblUserPWd
            // 
            this.lblUserPWd.AutoSize = true;
            this.lblUserPWd.Location = new System.Drawing.Point(52, 69);
            this.lblUserPWd.Name = "lblUserPWd";
            this.lblUserPWd.Size = new System.Drawing.Size(41, 12);
            this.lblUserPWd.TabIndex = 4;
            this.lblUserPWd.Text = "密  码";
            // 
            // chkShowPwd
            // 
            this.chkShowPwd.AutoSize = true;
            this.chkShowPwd.Location = new System.Drawing.Point(291, 68);
            this.chkShowPwd.Name = "chkShowPwd";
            this.chkShowPwd.Size = new System.Drawing.Size(72, 16);
            this.chkShowPwd.TabIndex = 5;
            this.chkShowPwd.Text = "显示密码";
            this.chkShowPwd.UseVisualStyleBackColor = true;
            this.chkShowPwd.Visible = false;
            this.chkShowPwd.CheckedChanged += new System.EventHandler(this.chkShowPwd_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblClear);
            this.groupBox1.Controls.Add(this.txtUserPwd);
            this.groupBox1.Controls.Add(this.cmbUserName);
            this.groupBox1.Controls.Add(this.chkShowPwd);
            this.groupBox1.Controls.Add(this.lblUserName);
            this.groupBox1.Controls.Add(this.lblUserPWd);
            this.groupBox1.Location = new System.Drawing.Point(2, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(399, 100);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            // 
            // lblClear
            // 
            this.lblClear.AutoSize = true;
            this.lblClear.Location = new System.Drawing.Point(300, 27);
            this.lblClear.Name = "lblClear";
            this.lblClear.Size = new System.Drawing.Size(53, 12);
            this.lblClear.TabIndex = 6;
            this.lblClear.TabStop = true;
            this.lblClear.Text = "清除用户";
            this.lblClear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblClear_LinkClicked);
            // 
            // PICUpdateStatus
            // 
            this.PICUpdateStatus.Image = global::Kingrocket.CJ.Components.Properties.Resources.dot_red;
            this.PICUpdateStatus.Location = new System.Drawing.Point(2, 206);
            this.PICUpdateStatus.Name = "PICUpdateStatus";
            this.PICUpdateStatus.Size = new System.Drawing.Size(14, 14);
            this.PICUpdateStatus.TabIndex = 17;
            this.PICUpdateStatus.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = global::Kingrocket.CJ.Components.Properties.Resources.Logo1;
            this.pictureBox1.Location = new System.Drawing.Point(-6, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(407, 73);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // lblUpdateStatus
            // 
            this.lblUpdateStatus.AutoSize = true;
            this.lblUpdateStatus.Location = new System.Drawing.Point(15, 208);
            this.lblUpdateStatus.Name = "lblUpdateStatus";
            this.lblUpdateStatus.Size = new System.Drawing.Size(119, 12);
            this.lblUpdateStatus.TabIndex = 18;
            this.lblUpdateStatus.Text = "系统正在加载.......";
            // 
            // timerUpdate
            // 
            this.timerUpdate.Enabled = true;
            this.timerUpdate.Interval = 1000;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // LoginForm
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 238);
            this.Controls.Add(this.lblUpdateStatus);
            this.Controls.Add(this.PICUpdateStatus);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnLogin);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户登录";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PICUpdateStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblUserPWd;
        public System.Windows.Forms.ComboBox cmbUserName;
        public System.Windows.Forms.TextBox txtUserPwd;
        private System.Windows.Forms.CheckBox chkShowPwd;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel lblClear;
        public System.Windows.Forms.PictureBox PICUpdateStatus;
        public System.Windows.Forms.Label lblUpdateStatus;
        public System.Windows.Forms.Button btnLogin;
        public System.Windows.Forms.Timer timerUpdate;
    }
}