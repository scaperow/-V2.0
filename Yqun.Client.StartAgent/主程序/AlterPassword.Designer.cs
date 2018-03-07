namespace Yqun.MainUI
{
    partial class AlterPassword
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.OldPassword = new System.Windows.Forms.TextBox();
            this.OnePassword = new System.Windows.Forms.TextBox();
            this.TwoPassword = new System.Windows.Forms.TextBox();
            this.OkPassword = new System.Windows.Forms.Button();
            this.CancelPassword = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "您的旧密码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "您的新密码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "新密码确认：";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // OldPassword
            // 
            this.OldPassword.Location = new System.Drawing.Point(99, 28);
            this.OldPassword.Name = "OldPassword";
            this.OldPassword.PasswordChar = '*';
            this.OldPassword.Size = new System.Drawing.Size(156, 21);
            this.OldPassword.TabIndex = 3;
            // 
            // OnePassword
            // 
            this.OnePassword.Location = new System.Drawing.Point(99, 59);
            this.OnePassword.Name = "OnePassword";
            this.OnePassword.PasswordChar = '*';
            this.OnePassword.Size = new System.Drawing.Size(156, 21);
            this.OnePassword.TabIndex = 4;
            // 
            // TwoPassword
            // 
            this.TwoPassword.Location = new System.Drawing.Point(100, 91);
            this.TwoPassword.Name = "TwoPassword";
            this.TwoPassword.PasswordChar = '*';
            this.TwoPassword.Size = new System.Drawing.Size(156, 21);
            this.TwoPassword.TabIndex = 5;
            // 
            // OkPassword
            // 
            this.OkPassword.Location = new System.Drawing.Point(98, 155);
            this.OkPassword.Name = "OkPassword";
            this.OkPassword.Size = new System.Drawing.Size(75, 25);
            this.OkPassword.TabIndex = 6;
            this.OkPassword.Text = "确 认";
            this.OkPassword.UseVisualStyleBackColor = true;
            this.OkPassword.Click += new System.EventHandler(this.OkPassword_Click);
            // 
            // CancelPassword
            // 
            this.CancelPassword.Location = new System.Drawing.Point(193, 155);
            this.CancelPassword.Name = "CancelPassword";
            this.CancelPassword.Size = new System.Drawing.Size(75, 25);
            this.CancelPassword.TabIndex = 7;
            this.CancelPassword.Text = "取 消";
            this.CancelPassword.UseVisualStyleBackColor = true;
            this.CancelPassword.Click += new System.EventHandler(this.CancelPassword_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TwoPassword);
            this.groupBox1.Controls.Add(this.OnePassword);
            this.groupBox1.Controls.Add(this.OldPassword);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(272, 133);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "密码信息";
            // 
            // AlterPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 193);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CancelPassword);
            this.Controls.Add(this.OkPassword);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AlterPassword";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "修改密码";
            this.Load += new System.EventHandler(this.AlterPassword_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox OldPassword;
        private System.Windows.Forms.TextBox OnePassword;
        private System.Windows.Forms.TextBox TwoPassword;
        private System.Windows.Forms.Button OkPassword;
        private System.Windows.Forms.Button CancelPassword;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}