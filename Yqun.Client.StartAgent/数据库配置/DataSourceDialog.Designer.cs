namespace Yqun.MainUI
{
    partial class DataSourceDialog
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
            this.tControlConfigInfo = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Panel_DJB = new System.Windows.Forms.Panel();
            this.TextBox_Information = new System.Windows.Forms.TextBox();
            this.DataIntegratedLogin = new System.Windows.Forms.CheckBox();
            this.DataISAttach = new System.Windows.Forms.CheckBox();
            this.PasswordTxt = new System.Windows.Forms.TextBox();
            this.UserTxt = new System.Windows.Forms.TextBox();
            this.DataBaseTxt = new System.Windows.Forms.TextBox();
            this.DataSourceTxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Panel_WLB = new System.Windows.Forms.Panel();
            this.Button_Exit = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtServerAddress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tControlConfigInfo.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.Panel_DJB.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.Panel_WLB.SuspendLayout();
            this.SuspendLayout();
            // 
            // tControlConfigInfo
            // 
            this.tControlConfigInfo.Controls.Add(this.tabPage1);
            this.tControlConfigInfo.Controls.Add(this.tabPage2);
            this.tControlConfigInfo.Location = new System.Drawing.Point(13, 14);
            this.tControlConfigInfo.Name = "tControlConfigInfo";
            this.tControlConfigInfo.SelectedIndex = 0;
            this.tControlConfigInfo.Size = new System.Drawing.Size(472, 333);
            this.tControlConfigInfo.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Panel_DJB);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(464, 307);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "配置本地数据源";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // Panel_DJB
            // 
            this.Panel_DJB.Controls.Add(this.TextBox_Information);
            this.Panel_DJB.Controls.Add(this.DataIntegratedLogin);
            this.Panel_DJB.Controls.Add(this.DataISAttach);
            this.Panel_DJB.Controls.Add(this.PasswordTxt);
            this.Panel_DJB.Controls.Add(this.UserTxt);
            this.Panel_DJB.Controls.Add(this.DataBaseTxt);
            this.Panel_DJB.Controls.Add(this.DataSourceTxt);
            this.Panel_DJB.Controls.Add(this.label4);
            this.Panel_DJB.Controls.Add(this.label3);
            this.Panel_DJB.Controls.Add(this.label2);
            this.Panel_DJB.Controls.Add(this.label1);
            this.Panel_DJB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_DJB.Location = new System.Drawing.Point(3, 3);
            this.Panel_DJB.Name = "Panel_DJB";
            this.Panel_DJB.Size = new System.Drawing.Size(458, 301);
            this.Panel_DJB.TabIndex = 1;
            // 
            // TextBox_Information
            // 
            this.TextBox_Information.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextBox_Information.Font = new System.Drawing.Font("宋体", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TextBox_Information.Location = new System.Drawing.Point(36, 264);
            this.TextBox_Information.Multiline = true;
            this.TextBox_Information.Name = "TextBox_Information";
            this.TextBox_Information.ReadOnly = true;
            this.TextBox_Information.Size = new System.Drawing.Size(384, 21);
            this.TextBox_Information.TabIndex = 9;
            this.TextBox_Information.Text = "说明:[apppath]表示应用程序的安装目录";
            // 
            // DataIntegratedLogin
            // 
            this.DataIntegratedLogin.AutoSize = true;
            this.DataIntegratedLogin.Checked = true;
            this.DataIntegratedLogin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DataIntegratedLogin.Location = new System.Drawing.Point(117, 176);
            this.DataIntegratedLogin.Name = "DataIntegratedLogin";
            this.DataIntegratedLogin.Size = new System.Drawing.Size(162, 16);
            this.DataIntegratedLogin.TabIndex = 8;
            this.DataIntegratedLogin.Text = "使用Windows集成安全登录";
            this.DataIntegratedLogin.UseVisualStyleBackColor = true;
            this.DataIntegratedLogin.Click += new System.EventHandler(this.DataIntegratedLogin_Click);
            // 
            // DataISAttach
            // 
            this.DataISAttach.AutoSize = true;
            this.DataISAttach.Checked = true;
            this.DataISAttach.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DataISAttach.Location = new System.Drawing.Point(117, 203);
            this.DataISAttach.Name = "DataISAttach";
            this.DataISAttach.Size = new System.Drawing.Size(210, 16);
            this.DataISAttach.TabIndex = 2;
            this.DataISAttach.Text = "连接到SQL Server Express 数据源";
            this.DataISAttach.UseVisualStyleBackColor = true;
            // 
            // PasswordTxt
            // 
            this.PasswordTxt.Location = new System.Drawing.Point(117, 137);
            this.PasswordTxt.Name = "PasswordTxt";
            this.PasswordTxt.PasswordChar = '*';
            this.PasswordTxt.Size = new System.Drawing.Size(303, 21);
            this.PasswordTxt.TabIndex = 7;
            // 
            // UserTxt
            // 
            this.UserTxt.Location = new System.Drawing.Point(117, 99);
            this.UserTxt.Name = "UserTxt";
            this.UserTxt.Size = new System.Drawing.Size(303, 21);
            this.UserTxt.TabIndex = 6;
            // 
            // DataBaseTxt
            // 
            this.DataBaseTxt.Location = new System.Drawing.Point(117, 62);
            this.DataBaseTxt.Name = "DataBaseTxt";
            this.DataBaseTxt.Size = new System.Drawing.Size(303, 21);
            this.DataBaseTxt.TabIndex = 5;
            // 
            // DataSourceTxt
            // 
            this.DataSourceTxt.Location = new System.Drawing.Point(117, 25);
            this.DataSourceTxt.Name = "DataSourceTxt";
            this.DataSourceTxt.Size = new System.Drawing.Size(303, 21);
            this.DataSourceTxt.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "密      码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "用  户  名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "数据库名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器名称：";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.Panel_WLB);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(464, 307);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "网络服务配置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Panel_WLB
            // 
            this.Panel_WLB.Controls.Add(this.textBox1);
            this.Panel_WLB.Controls.Add(this.txtServerAddress);
            this.Panel_WLB.Controls.Add(this.label5);
            this.Panel_WLB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_WLB.Location = new System.Drawing.Point(3, 3);
            this.Panel_WLB.Name = "Panel_WLB";
            this.Panel_WLB.Size = new System.Drawing.Size(458, 301);
            this.Panel_WLB.TabIndex = 1;
            // 
            // Button_Exit
            // 
            this.Button_Exit.Location = new System.Drawing.Point(376, 362);
            this.Button_Exit.Name = "Button_Exit";
            this.Button_Exit.Size = new System.Drawing.Size(90, 23);
            this.Button_Exit.TabIndex = 1;
            this.Button_Exit.Text = "保存并关闭";
            this.Button_Exit.UseVisualStyleBackColor = true;
            this.Button_Exit.Click += new System.EventHandler(this.Button_Exit_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(293, 362);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "测试连接";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("宋体", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(38, 47);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(408, 21);
            this.textBox1.TabIndex = 12;
            this.textBox1.Text = "地址:端口，不包括前面的http://和后面的/,如:118.26.99.68:8122";
            // 
            // txtServerAddress
            // 
            this.txtServerAddress.Location = new System.Drawing.Point(119, 20);
            this.txtServerAddress.Name = "txtServerAddress";
            this.txtServerAddress.Size = new System.Drawing.Size(303, 21);
            this.txtServerAddress.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "服务端地址：";
            // 
            // DataSourceDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 404);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Button_Exit);
            this.Controls.Add(this.tControlConfigInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataSourceDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "配置数据源";
            this.Load += new System.EventHandler(this.DataSourceDialog_Load);
            this.tControlConfigInfo.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.Panel_DJB.ResumeLayout(false);
            this.Panel_DJB.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.Panel_WLB.ResumeLayout(false);
            this.Panel_WLB.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tControlConfigInfo;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button Button_Exit;
        private System.Windows.Forms.Panel Panel_DJB;
        private System.Windows.Forms.Panel Panel_WLB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PasswordTxt;
        private System.Windows.Forms.TextBox UserTxt;
        private System.Windows.Forms.TextBox DataBaseTxt;
        private System.Windows.Forms.TextBox DataSourceTxt;
        private System.Windows.Forms.CheckBox DataIntegratedLogin;
        private System.Windows.Forms.CheckBox DataISAttach;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox TextBox_Information;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txtServerAddress;
        private System.Windows.Forms.Label label5;

    }
}