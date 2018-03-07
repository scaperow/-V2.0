namespace Yqun.MainUI
{
    partial class LoginDialog
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_OK = new System.Windows.Forms.Button();
            this.text_Pass = new System.Windows.Forms.TextBox();
            this.com_User = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.picClearUser = new System.Windows.Forms.PictureBox();
            this.picSystemSetting = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblUpdateMsg = new System.Windows.Forms.Label();
            this.picUpdateMsg = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClearUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSystemSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUpdateMsg)).BeginInit();
            this.SuspendLayout();
            // 
            // button_OK
            // 
            this.button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_OK.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_OK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_OK.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_OK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.button_OK.Location = new System.Drawing.Point(221, 189);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(140, 30);
            this.button_OK.TabIndex = 3;
            this.button_OK.Text = "登录";
            this.button_OK.UseVisualStyleBackColor = false;
            // 
            // text_Pass
            // 
            this.text_Pass.BackColor = System.Drawing.Color.White;
            this.text_Pass.Location = new System.Drawing.Point(97, 71);
            this.text_Pass.Name = "text_Pass";
            this.text_Pass.PasswordChar = '●';
            this.text_Pass.Size = new System.Drawing.Size(170, 21);
            this.text_Pass.TabIndex = 2;
            // 
            // com_User
            // 
            this.com_User.BackColor = System.Drawing.Color.White;
            this.com_User.Location = new System.Drawing.Point(97, 32);
            this.com_User.Name = "com_User";
            this.com_User.Size = new System.Drawing.Size(170, 20);
            this.com_User.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(39, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "用户名:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(38, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "密  码:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.picClearUser);
            this.groupBox1.Controls.Add(this.picSystemSetting);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.com_User);
            this.groupBox1.Controls.Add(this.text_Pass);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(7, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(354, 121);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // picClearUser
            // 
            this.picClearUser.BackgroundImage = global::Yqun.MainUI.Properties.Resources.icon_clean;
            this.picClearUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picClearUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picClearUser.Location = new System.Drawing.Point(274, 33);
            this.picClearUser.Name = "picClearUser";
            this.picClearUser.Size = new System.Drawing.Size(19, 19);
            this.picClearUser.TabIndex = 17;
            this.picClearUser.TabStop = false;
            this.picClearUser.Click += new System.EventHandler(this.picClearUser_Click);
            // 
            // picSystemSetting
            // 
            this.picSystemSetting.BackgroundImage = global::Yqun.MainUI.Properties.Resources.systemsetting;
            this.picSystemSetting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picSystemSetting.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picSystemSetting.Location = new System.Drawing.Point(274, 71);
            this.picSystemSetting.Name = "picSystemSetting";
            this.picSystemSetting.Size = new System.Drawing.Size(19, 19);
            this.picSystemSetting.TabIndex = 17;
            this.picSystemSetting.TabStop = false;
            this.picSystemSetting.Click += new System.EventHandler(this.picSystemSetting_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = global::Yqun.MainUI.Properties.Resources.Logo;
            this.pictureBox1.Location = new System.Drawing.Point(-1, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(371, 60);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // lblUpdateMsg
            // 
            this.lblUpdateMsg.AutoSize = true;
            this.lblUpdateMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblUpdateMsg.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUpdateMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.lblUpdateMsg.Location = new System.Drawing.Point(21, 201);
            this.lblUpdateMsg.Margin = new System.Windows.Forms.Padding(0);
            this.lblUpdateMsg.Name = "lblUpdateMsg";
            this.lblUpdateMsg.Size = new System.Drawing.Size(89, 12);
            this.lblUpdateMsg.TabIndex = 16;
            this.lblUpdateMsg.Text = "正在检查更新…";
            // 
            // picUpdateMsg
            // 
            this.picUpdateMsg.BackgroundImage = global::Yqun.MainUI.Properties.Resources.dot_red;
            this.picUpdateMsg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picUpdateMsg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picUpdateMsg.Location = new System.Drawing.Point(8, 200);
            this.picUpdateMsg.Name = "picUpdateMsg";
            this.picUpdateMsg.Size = new System.Drawing.Size(14, 14);
            this.picUpdateMsg.TabIndex = 17;
            this.picUpdateMsg.TabStop = false;
            // 
            // LoginDialog
            // 
            this.AcceptButton = this.button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(368, 227);
            this.Controls.Add(this.picUpdateMsg);
            this.Controls.Add(this.lblUpdateMsg);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button_OK);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "LoginDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户登录";
            this.Load += new System.EventHandler(this.LoginDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClearUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSystemSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUpdateMsg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_OK;
        public System.Windows.Forms.TextBox text_Pass;
        public System.Windows.Forms.ComboBox com_User;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblUpdateMsg;
        private System.Windows.Forms.PictureBox picSystemSetting;
        private System.Windows.Forms.PictureBox picUpdateMsg;
        private System.Windows.Forms.PictureBox picClearUser;
    }
}