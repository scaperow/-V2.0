namespace BizModules
{
    partial class HyperLinkSettingDialog
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
            this.txtShowText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.RadioLink = new System.Windows.Forms.RadioButton();
            this.RadioAttach = new System.Windows.Forms.RadioButton();
            this.ButtonUpload = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLinkUrl = new System.Windows.Forms.TextBox();
            this.PanelAttach = new System.Windows.Forms.Panel();
            this.Preview = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.PanelHyperlink = new System.Windows.Forms.Panel();
            this.PanelAttach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Preview)).BeginInit();
            this.PanelHyperlink.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtShowText
            // 
            this.txtShowText.Location = new System.Drawing.Point(83, 40);
            this.txtShowText.Name = "txtShowText";
            this.txtShowText.Size = new System.Drawing.Size(292, 21);
            this.txtShowText.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "显示文字 :";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(300, 306);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "取消";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(219, 306);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // RadioLink
            // 
            this.RadioLink.AutoSize = true;
            this.RadioLink.Checked = true;
            this.RadioLink.Location = new System.Drawing.Point(144, 12);
            this.RadioLink.Name = "RadioLink";
            this.RadioLink.Size = new System.Drawing.Size(59, 16);
            this.RadioLink.TabIndex = 33;
            this.RadioLink.TabStop = true;
            this.RadioLink.Text = "超链接";
            this.RadioLink.UseVisualStyleBackColor = true;
            this.RadioLink.CheckedChanged += new System.EventHandler(this.RadioLink_CheckedChanged);
            // 
            // RadioAttach
            // 
            this.RadioAttach.AutoSize = true;
            this.RadioAttach.Location = new System.Drawing.Point(209, 12);
            this.RadioAttach.Name = "RadioAttach";
            this.RadioAttach.Size = new System.Drawing.Size(47, 16);
            this.RadioAttach.TabIndex = 33;
            this.RadioAttach.Text = "附件";
            this.RadioAttach.UseVisualStyleBackColor = true;
            this.RadioAttach.CheckedChanged += new System.EventHandler(this.RadioAttach_CheckedChanged);
            // 
            // ButtonUpload
            // 
            this.ButtonUpload.Location = new System.Drawing.Point(138, 306);
            this.ButtonUpload.Name = "ButtonUpload";
            this.ButtonUpload.Size = new System.Drawing.Size(75, 23);
            this.ButtonUpload.TabIndex = 4;
            this.ButtonUpload.Text = "选择附件";
            this.ButtonUpload.UseVisualStyleBackColor = true;
            this.ButtonUpload.Visible = false;
            this.ButtonUpload.Click += new System.EventHandler(this.ButtonUpload_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "链接地址 :";
            // 
            // txtLinkUrl
            // 
            this.txtLinkUrl.Location = new System.Drawing.Point(84, 13);
            this.txtLinkUrl.Name = "txtLinkUrl";
            this.txtLinkUrl.Size = new System.Drawing.Size(292, 21);
            this.txtLinkUrl.TabIndex = 2;
            // 
            // PanelAttach
            // 
            this.PanelAttach.Controls.Add(this.Preview);
            this.PanelAttach.Controls.Add(this.label3);
            this.PanelAttach.Location = new System.Drawing.Point(1, 67);
            this.PanelAttach.Name = "PanelAttach";
            this.PanelAttach.Size = new System.Drawing.Size(398, 233);
            this.PanelAttach.TabIndex = 36;
            this.PanelAttach.Visible = false;
            // 
            // Preview
            // 
            this.Preview.BackColor = System.Drawing.Color.White;
            this.Preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Preview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Preview.Location = new System.Drawing.Point(84, 13);
            this.Preview.Name = "Preview";
            this.Preview.Size = new System.Drawing.Size(292, 204);
            this.Preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Preview.TabIndex = 5;
            this.Preview.TabStop = false;
            this.Preview.Click += new System.EventHandler(this.Preview_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "附件预览 :";
            // 
            // PanelHyperlink
            // 
            this.PanelHyperlink.Controls.Add(this.txtLinkUrl);
            this.PanelHyperlink.Controls.Add(this.label2);
            this.PanelHyperlink.Location = new System.Drawing.Point(1, 67);
            this.PanelHyperlink.Name = "PanelHyperlink";
            this.PanelHyperlink.Size = new System.Drawing.Size(401, 48);
            this.PanelHyperlink.TabIndex = 34;
            // 
            // HyperLinkSettingDialog
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(397, 339);
            this.Controls.Add(this.PanelAttach);
            this.Controls.Add(this.ButtonUpload);
            this.Controls.Add(this.txtShowText);
            this.Controls.Add(this.PanelHyperlink);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RadioAttach);
            this.Controls.Add(this.RadioLink);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HyperLinkSettingDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "属性设置";
            this.Load += new System.EventHandler(this.HyperLinkSettingDialog_Load);
            this.PanelAttach.ResumeLayout(false);
            this.PanelAttach.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Preview)).EndInit();
            this.PanelHyperlink.ResumeLayout(false);
            this.PanelHyperlink.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtShowText;
        private System.Windows.Forms.RadioButton RadioLink;
        private System.Windows.Forms.RadioButton RadioAttach;
        private System.Windows.Forms.Button ButtonUpload;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLinkUrl;
        private System.Windows.Forms.Panel PanelAttach;
        private System.Windows.Forms.PictureBox Preview;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel PanelHyperlink;
    }
}