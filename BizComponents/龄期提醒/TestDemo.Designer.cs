namespace BizComponents
{
    partial class TestDemo
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
            this.btn_sent = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.txtPhones = new System.Windows.Forms.TextBox();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_sent
            // 
            this.btn_sent.Location = new System.Drawing.Point(365, 12);
            this.btn_sent.Name = "btn_sent";
            this.btn_sent.Size = new System.Drawing.Size(66, 23);
            this.btn_sent.TabIndex = 0;
            this.btn_sent.Text = "发送";
            this.btn_sent.UseVisualStyleBackColor = true;
            this.btn_sent.Click += new System.EventHandler(this.btn_sent_Click);
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(365, 41);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(66, 23);
            this.btn_close.TabIndex = 0;
            this.btn_close.Text = "关闭";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // txtPhones
            // 
            this.txtPhones.Location = new System.Drawing.Point(12, 12);
            this.txtPhones.Name = "txtPhones";
            this.txtPhones.Size = new System.Drawing.Size(347, 21);
            this.txtPhones.TabIndex = 1;
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(12, 62);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(347, 97);
            this.txtContent.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "接收人以英文逗号\",\"分割";
            // 
            // TestDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 193);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.txtPhones);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_sent);
            this.Name = "TestDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "短信发送测试";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.TestDemo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_sent;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.TextBox txtPhones;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.Label label1;
    }
}