namespace BizComponents.试验数据检测
{
    partial class ImageShowForm
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
            this.pictureBoxRight = new System.Windows.Forms.PictureBox();
            this.pictureBoxLeft = new System.Windows.Forms.PictureBox();
            this.pictureBoxClose = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxRight
            // 
            this.pictureBoxRight.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxRight.Location = new System.Drawing.Point(599, 177);
            this.pictureBoxRight.Name = "pictureBoxRight";
            this.pictureBoxRight.Size = new System.Drawing.Size(100, 89);
            this.pictureBoxRight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxRight.TabIndex = 1;
            this.pictureBoxRight.TabStop = false;
            this.pictureBoxRight.MouseLeave += new System.EventHandler(this.pictureBoxRight_MouseLeave);
            this.pictureBoxRight.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxRight_MouseMove);
            this.pictureBoxRight.Click += new System.EventHandler(this.pictureBoxRight_Click);
            // 
            // pictureBoxLeft
            // 
            this.pictureBoxLeft.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxLeft.Location = new System.Drawing.Point(1, 177);
            this.pictureBoxLeft.Name = "pictureBoxLeft";
            this.pictureBoxLeft.Size = new System.Drawing.Size(100, 89);
            this.pictureBoxLeft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLeft.TabIndex = 1;
            this.pictureBoxLeft.TabStop = false;
            this.pictureBoxLeft.MouseLeave += new System.EventHandler(this.pictureBoxLeft_MouseLeave);
            this.pictureBoxLeft.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxLeft_MouseMove);
            this.pictureBoxLeft.Click += new System.EventHandler(this.pictureBoxLeft_Click);
            // 
            // pictureBoxClose
            // 
            this.pictureBoxClose.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxClose.Location = new System.Drawing.Point(654, 0);
            this.pictureBoxClose.Name = "pictureBoxClose";
            this.pictureBoxClose.Size = new System.Drawing.Size(45, 50);
            this.pictureBoxClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxClose.TabIndex = 0;
            this.pictureBoxClose.TabStop = false;
            this.pictureBoxClose.MouseLeave += new System.EventHandler(this.pictureBoxClose_MouseLeave);
            this.pictureBoxClose.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxClose_MouseMove);
            this.pictureBoxClose.Click += new System.EventHandler(this.pictureBoxClose_Click);
            // 
            // ImageShowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(700, 475);
            this.Controls.Add(this.pictureBoxRight);
            this.Controls.Add(this.pictureBoxLeft);
            this.Controls.Add(this.pictureBoxClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ImageShowForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ImageShowForm";
            this.Load += new System.EventHandler(this.ImageShowForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxClose;
        private System.Windows.Forms.PictureBox pictureBoxLeft;
        private System.Windows.Forms.PictureBox pictureBoxRight;
    }
}