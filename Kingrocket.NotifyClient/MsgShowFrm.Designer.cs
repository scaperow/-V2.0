namespace Kingrocket.NotifyClient
{
    partial class MsgShowFrm
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
            this.lblMsg = new System.Windows.Forms.Label();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblMsg.Location = new System.Drawing.Point(9, 37);
            this.lblMsg.MaximumSize = new System.Drawing.Size(240, 140);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(53, 12);
            this.lblMsg.TabIndex = 0;
            this.lblMsg.Text = "消息内容";
            // 
            // picClose
            // 
            this.picClose.Image = global::Kingrocket.NotifyClient.Properties.Resources.msgshowclose;
            this.picClose.Location = new System.Drawing.Point(217, 0);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(32, 30);
            this.picClose.TabIndex = 2;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "消息提示";
            // 
            // MsgShowFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Kingrocket.NotifyClient.Properties.Resources.msgshowbg;
            this.ClientSize = new System.Drawing.Size(250, 170);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picClose);
            this.Controls.Add(this.lblMsg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MsgShowFrm";
            this.ShowInTaskbar = false;
            this.Text = "消息提示";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MsgShowFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.PictureBox picClose;
        private System.Windows.Forms.Label label1;
    }
}