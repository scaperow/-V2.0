namespace BizComponents
{
    partial class InputRemarkInfoForm
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
            this.groupBoxRemarkInfo = new System.Windows.Forms.GroupBox();
            this.btnCancle = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtRemarkInfo = new System.Windows.Forms.TextBox();
            this.groupBoxRemarkInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxRemarkInfo
            // 
            this.groupBoxRemarkInfo.Controls.Add(this.btnCancle);
            this.groupBoxRemarkInfo.Controls.Add(this.btnOk);
            this.groupBoxRemarkInfo.Controls.Add(this.txtRemarkInfo);
            this.groupBoxRemarkInfo.Location = new System.Drawing.Point(12, 12);
            this.groupBoxRemarkInfo.Name = "groupBoxRemarkInfo";
            this.groupBoxRemarkInfo.Size = new System.Drawing.Size(441, 272);
            this.groupBoxRemarkInfo.TabIndex = 0;
            this.groupBoxRemarkInfo.TabStop = false;
            this.groupBoxRemarkInfo.Text = "龄期原因分析、监理意见、领导意见";
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(248, 236);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 3;
            this.btnCancle.Text = "取消";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(105, 236);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtRemarkInfo
            // 
            this.txtRemarkInfo.Location = new System.Drawing.Point(17, 33);
            this.txtRemarkInfo.Multiline = true;
            this.txtRemarkInfo.Name = "txtRemarkInfo";
            this.txtRemarkInfo.Size = new System.Drawing.Size(406, 190);
            this.txtRemarkInfo.TabIndex = 1;
            // 
            // InputRemarkInfoForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancle;
            this.ClientSize = new System.Drawing.Size(468, 294);
            this.Controls.Add(this.groupBoxRemarkInfo);
            this.MaximizeBox = false;
            this.Name = "InputRemarkInfoForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.groupBoxRemarkInfo.ResumeLayout(false);
            this.groupBoxRemarkInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxRemarkInfo;
        private System.Windows.Forms.TextBox txtRemarkInfo;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Button btnOk;
    }
}