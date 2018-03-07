namespace BizComponents
{
    partial class DataDialog
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
            this.fpSpreadViewer1 = new BizComponents.FpSpreadViewer();
            this.SuspendLayout();
            // 
            // fpSpreadViewer1
            // 
            this.fpSpreadViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpreadViewer1.Location = new System.Drawing.Point(0, 0);
            this.fpSpreadViewer1.Name = "fpSpreadViewer1";
            this.fpSpreadViewer1.Size = new System.Drawing.Size(792, 573);
            this.fpSpreadViewer1.TabIndex = 2;
            // 
            // DataDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.fpSpreadViewer1);
            this.Name = "DataDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "查看资料（使用\"Ctl+滚轮\"组合键可缩放视图）";
            this.Load += new System.EventHandler(this.DataDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private FpSpreadViewer fpSpreadViewer1;
    }
}