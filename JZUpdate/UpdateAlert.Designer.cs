namespace JZUpgrade
{
    partial class UpdateAlert
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateAlert));
            this.web = new System.Windows.Forms.WebBrowser();
            this.btnOK = new System.Windows.Forms.Button();
            this.pnlWeb = new System.Windows.Forms.Panel();
            this.pnlButton = new System.Windows.Forms.Panel();
            this.pnlWeb.SuspendLayout();
            this.pnlButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // web
            // 
            this.web.Dock = System.Windows.Forms.DockStyle.Fill;
            this.web.Location = new System.Drawing.Point(0, 0);
            this.web.MinimumSize = new System.Drawing.Size(20, 20);
            this.web.Name = "web";
            this.web.Size = new System.Drawing.Size(420, 392);
            this.web.TabIndex = 2;
            this.web.Url = new System.Uri("http://www.kingrocket.com/updatedesc/glupdate.html", System.UriKind.Absolute);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(311, 1);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(95, 35);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "立即更新";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pnlWeb
            // 
            this.pnlWeb.Controls.Add(this.web);
            this.pnlWeb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWeb.Location = new System.Drawing.Point(0, 0);
            this.pnlWeb.Name = "pnlWeb";
            this.pnlWeb.Size = new System.Drawing.Size(420, 392);
            this.pnlWeb.TabIndex = 4;
            // 
            // pnlButton
            // 
            this.pnlButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.pnlButton.Controls.Add(this.btnOK);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButton.Location = new System.Drawing.Point(0, 392);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(420, 38);
            this.pnlButton.TabIndex = 4;
            // 
            // UpdateAlert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 430);
            this.Controls.Add(this.pnlWeb);
            this.Controls.Add(this.pnlButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateAlert";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "更新说明";
            this.Load += new System.EventHandler(this.UpdateAlert_Load);
            this.pnlWeb.ResumeLayout(false);
            this.pnlButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser web;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel pnlWeb;
        private System.Windows.Forms.Panel pnlButton;

    }
}