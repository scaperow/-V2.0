namespace Yqun.MainUI
{
    partial class ChangeDefaultPasswordForm
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
            this.grbChangPwd = new System.Windows.Forms.GroupBox();
            this.lblErrorInfo = new System.Windows.Forms.Label();
            this.txtTwoPwd = new System.Windows.Forms.TextBox();
            this.txtOnePwd = new System.Windows.Forms.TextBox();
            this.lblTwoPwd = new System.Windows.Forms.Label();
            this.lblOnePwd = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.grbChangPwd.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbChangPwd
            // 
            this.grbChangPwd.Controls.Add(this.lblErrorInfo);
            this.grbChangPwd.Controls.Add(this.txtTwoPwd);
            this.grbChangPwd.Controls.Add(this.txtOnePwd);
            this.grbChangPwd.Controls.Add(this.lblTwoPwd);
            this.grbChangPwd.Controls.Add(this.lblOnePwd);
            this.grbChangPwd.Location = new System.Drawing.Point(21, 12);
            this.grbChangPwd.Name = "grbChangPwd";
            this.grbChangPwd.Size = new System.Drawing.Size(357, 167);
            this.grbChangPwd.TabIndex = 0;
            this.grbChangPwd.TabStop = false;
            this.grbChangPwd.Text = "修改默认密码信息";
            // 
            // lblErrorInfo
            // 
            this.lblErrorInfo.AutoSize = true;
            this.lblErrorInfo.Font = new System.Drawing.Font("宋体", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblErrorInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblErrorInfo.Location = new System.Drawing.Point(27, 133);
            this.lblErrorInfo.Name = "lblErrorInfo";
            this.lblErrorInfo.Size = new System.Drawing.Size(309, 9);
            this.lblErrorInfo.TabIndex = 3;
            this.lblErrorInfo.Text = "新密码长度大于4，而且不能为初始密码，如：1、111111、888888";
            // 
            // txtTwoPwd
            // 
            this.txtTwoPwd.Location = new System.Drawing.Point(143, 87);
            this.txtTwoPwd.Name = "txtTwoPwd";
            this.txtTwoPwd.PasswordChar = '*';
            this.txtTwoPwd.Size = new System.Drawing.Size(178, 21);
            this.txtTwoPwd.TabIndex = 2;
            // 
            // txtOnePwd
            // 
            this.txtOnePwd.Location = new System.Drawing.Point(143, 36);
            this.txtOnePwd.Name = "txtOnePwd";
            this.txtOnePwd.PasswordChar = '*';
            this.txtOnePwd.Size = new System.Drawing.Size(178, 21);
            this.txtOnePwd.TabIndex = 1;
            // 
            // lblTwoPwd
            // 
            this.lblTwoPwd.AutoSize = true;
            this.lblTwoPwd.Location = new System.Drawing.Point(38, 90);
            this.lblTwoPwd.Name = "lblTwoPwd";
            this.lblTwoPwd.Size = new System.Drawing.Size(65, 12);
            this.lblTwoPwd.TabIndex = 0;
            this.lblTwoPwd.Text = "新密码确认";
            // 
            // lblOnePwd
            // 
            this.lblOnePwd.AutoSize = true;
            this.lblOnePwd.Location = new System.Drawing.Point(40, 39);
            this.lblOnePwd.Name = "lblOnePwd";
            this.lblOnePwd.Size = new System.Drawing.Size(65, 12);
            this.lblOnePwd.TabIndex = 0;
            this.lblOnePwd.Text = "您的新密码";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(102, 185);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancle
            // 
            this.btnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancle.Location = new System.Drawing.Point(221, 185);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 4;
            this.btnCancle.Text = "取消";
            this.btnCancle.UseVisualStyleBackColor = true;
            // 
            // ChangeDefaultPasswordForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 224);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grbChangPwd);
            this.MaximizeBox = false;
            this.Name = "ChangeDefaultPasswordForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改默认密码";
            this.Activated += new System.EventHandler(this.ChangeDefaultPasswordForm_Activated);
            this.grbChangPwd.ResumeLayout(false);
            this.grbChangPwd.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbChangPwd;
        private System.Windows.Forms.TextBox txtTwoPwd;
        private System.Windows.Forms.TextBox txtOnePwd;
        private System.Windows.Forms.Label lblTwoPwd;
        private System.Windows.Forms.Label lblOnePwd;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Label lblErrorInfo;
    }
}