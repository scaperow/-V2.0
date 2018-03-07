namespace Yqun.MainUI
{
    partial class HelpFile
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
            this.web = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // web
            // 
            this.web.Dock = System.Windows.Forms.DockStyle.Fill;
            this.web.Location = new System.Drawing.Point(8, 7);
            this.web.MinimumSize = new System.Drawing.Size(20, 20);
            this.web.Name = "web";
            this.web.Size = new System.Drawing.Size(506, 302);
            this.web.TabIndex = 3;
            this.web.Url = new System.Uri("http://www.kingrocket.com/userhelp/HelpFileDown.html", System.UriKind.Absolute);
            // 
            // HelpFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 316);
            this.Controls.Add(this.web);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HelpFile";
            this.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AppHelp";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser web;



    }
}