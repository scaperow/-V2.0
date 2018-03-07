namespace BizComponents
{
    partial class FactoryEditor
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
            this.TextFactoryName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TextLinkMan = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TextTel = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TextLongitude = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TextLatitude = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TextAddress = new System.Windows.Forms.TextBox();
            this.ButtonClose = new System.Windows.Forms.Button();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "厂家名称";
            // 
            // TextFactoryName
            // 
            this.TextFactoryName.Location = new System.Drawing.Point(89, 21);
            this.TextFactoryName.Name = "TextFactoryName";
            this.TextFactoryName.Size = new System.Drawing.Size(259, 21);
            this.TextFactoryName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "联系人";
            // 
            // TextLinkMan
            // 
            this.TextLinkMan.Location = new System.Drawing.Point(89, 48);
            this.TextLinkMan.Name = "TextLinkMan";
            this.TextLinkMan.Size = new System.Drawing.Size(259, 21);
            this.TextLinkMan.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "联系电话";
            // 
            // TextTel
            // 
            this.TextTel.Location = new System.Drawing.Point(89, 75);
            this.TextTel.Name = "TextTel";
            this.TextTel.Size = new System.Drawing.Size(259, 21);
            this.TextTel.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "经度";
            // 
            // TextLongitude
            // 
            this.TextLongitude.Location = new System.Drawing.Point(89, 102);
            this.TextLongitude.Name = "TextLongitude";
            this.TextLongitude.Size = new System.Drawing.Size(259, 21);
            this.TextLongitude.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(54, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "纬度";
            // 
            // TextLatitude
            // 
            this.TextLatitude.Location = new System.Drawing.Point(89, 129);
            this.TextLatitude.Name = "TextLatitude";
            this.TextLatitude.Size = new System.Drawing.Size(259, 21);
            this.TextLatitude.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 159);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "厂家地址";
            // 
            // TextAddress
            // 
            this.TextAddress.Location = new System.Drawing.Point(89, 156);
            this.TextAddress.Multiline = true;
            this.TextAddress.Name = "TextAddress";
            this.TextAddress.Size = new System.Drawing.Size(259, 134);
            this.TextAddress.TabIndex = 1;
            // 
            // ButtonClose
            // 
            this.ButtonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonClose.Location = new System.Drawing.Point(278, 332);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(75, 23);
            this.ButtonClose.TabIndex = 2;
            this.ButtonClose.Text = "关闭";
            this.ButtonClose.UseVisualStyleBackColor = true;
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // ButtonSave
            // 
            this.ButtonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonSave.Location = new System.Drawing.Point(197, 332);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(75, 23);
            this.ButtonSave.TabIndex = 2;
            this.ButtonSave.Text = "保存";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // FactoryEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 367);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.ButtonClose);
            this.Controls.Add(this.TextAddress);
            this.Controls.Add(this.TextLatitude);
            this.Controls.Add(this.TextLongitude);
            this.Controls.Add(this.TextTel);
            this.Controls.Add(this.TextLinkMan);
            this.Controls.Add(this.TextFactoryName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FactoryEditor";
            this.Text = "请在实例化函数中设置标题";
            this.Load += new System.EventHandler(this.FactoryEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextFactoryName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TextLinkMan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TextTel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TextLongitude;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TextLatitude;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TextAddress;
        private System.Windows.Forms.Button ButtonClose;
        private System.Windows.Forms.Button ButtonSave;
    }
}