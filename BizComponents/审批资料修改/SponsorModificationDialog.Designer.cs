namespace BizComponents
{
    partial class SponsorModificationDialog
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
            this.txtContent = new System.Windows.Forms.TextBox();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.SponsorModifyInfo = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkIsRequestStadium = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "内容(&C):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "原因(&R):";
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(68, 12);
            this.txtContent.MaxLength = 20;
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(495, 21);
            this.txtContent.TabIndex = 2;
            // 
            // txtReason
            // 
            this.txtReason.Location = new System.Drawing.Point(68, 43);
            this.txtReason.MaxLength = 100;
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(495, 268);
            this.txtReason.TabIndex = 3;
            // 
            // ButtonOk
            // 
            this.ButtonOk.Location = new System.Drawing.Point(343, 454);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(75, 23);
            this.ButtonOk.TabIndex = 4;
            this.ButtonOk.Text = "确定";
            this.ButtonOk.UseVisualStyleBackColor = true;
            this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(437, 454);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 4;
            this.ButtonCancel.Text = "取消";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // SponsorModifyInfo
            // 
            this.SponsorModifyInfo.AutoSize = true;
            this.SponsorModifyInfo.Font = new System.Drawing.Font("宋体", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SponsorModifyInfo.Location = new System.Drawing.Point(65, 363);
            this.SponsorModifyInfo.Name = "SponsorModifyInfo";
            this.SponsorModifyInfo.Size = new System.Drawing.Size(314, 16);
            this.SponsorModifyInfo.TabIndex = 5;
            this.SponsorModifyInfo.Text = "你已申请过，待此申请处理完后再申请！";
            this.SponsorModifyInfo.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(12, 435);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(553, 9);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // chkIsRequestStadium
            // 
            this.chkIsRequestStadium.AutoSize = true;
            this.chkIsRequestStadium.Location = new System.Drawing.Point(68, 317);
            this.chkIsRequestStadium.Name = "chkIsRequestStadium";
            this.chkIsRequestStadium.Size = new System.Drawing.Size(132, 16);
            this.chkIsRequestStadium.TabIndex = 7;
            this.chkIsRequestStadium.Text = "申请修改龄期做试验";
            this.chkIsRequestStadium.UseVisualStyleBackColor = true;
            // 
            // SponsorModificationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 489);
            this.Controls.Add(this.chkIsRequestStadium);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.SponsorModifyInfo);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SponsorModificationDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "申请修改";
            this.Load += new System.EventHandler(this.SponsorModificationDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Label SponsorModifyInfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkIsRequestStadium;
    }
}