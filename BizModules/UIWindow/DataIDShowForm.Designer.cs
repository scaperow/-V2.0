namespace BizModules.UIWindow
{
    partial class DataIDShowForm
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
            this.txtDataID = new System.Windows.Forms.TextBox();
            this.lblDataID = new System.Windows.Forms.Label();
            this.btnCopyAndClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtDataID
            // 
            this.txtDataID.Location = new System.Drawing.Point(102, 30);
            this.txtDataID.Name = "txtDataID";
            this.txtDataID.Size = new System.Drawing.Size(319, 21);
            this.txtDataID.TabIndex = 0;
            // 
            // lblDataID
            // 
            this.lblDataID.AutoSize = true;
            this.lblDataID.Location = new System.Drawing.Point(22, 33);
            this.lblDataID.Name = "lblDataID";
            this.lblDataID.Size = new System.Drawing.Size(65, 12);
            this.lblDataID.TabIndex = 1;
            this.lblDataID.Text = "数据DataID";
            // 
            // btnCopyAndClose
            // 
            this.btnCopyAndClose.Location = new System.Drawing.Point(115, 69);
            this.btnCopyAndClose.Name = "btnCopyAndClose";
            this.btnCopyAndClose.Size = new System.Drawing.Size(220, 23);
            this.btnCopyAndClose.TabIndex = 2;
            this.btnCopyAndClose.Text = "复制到剪贴板并关闭";
            this.btnCopyAndClose.UseVisualStyleBackColor = true;
            this.btnCopyAndClose.Click += new System.EventHandler(this.btnCopyAndClose_Click);
            // 
            // DataIDShowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 104);
            this.Controls.Add(this.btnCopyAndClose);
            this.Controls.Add(this.lblDataID);
            this.Controls.Add(this.txtDataID);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataIDShowForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "查看DataID";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDataID;
        private System.Windows.Forms.Label lblDataID;
        private System.Windows.Forms.Button btnCopyAndClose;
    }
}