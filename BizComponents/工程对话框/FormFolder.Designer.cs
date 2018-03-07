namespace BizComponents
{
    partial class FormFolder
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
            this.JzGrpBox = new System.Windows.Forms.GroupBox();
            this.tFolderName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtOrderID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.JzGrpBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // JzGrpBox
            // 
            this.JzGrpBox.Controls.Add(this.txtOrderID);
            this.JzGrpBox.Controls.Add(this.label7);
            this.JzGrpBox.Controls.Add(this.tFolderName);
            this.JzGrpBox.Controls.Add(this.label1);
            this.JzGrpBox.Location = new System.Drawing.Point(12, 6);
            this.JzGrpBox.Name = "JzGrpBox";
            this.JzGrpBox.Size = new System.Drawing.Size(406, 135);
            this.JzGrpBox.TabIndex = 14;
            this.JzGrpBox.TabStop = false;
            // 
            // tFolderName
            // 
            this.tFolderName.Location = new System.Drawing.Point(100, 23);
            this.tFolderName.Name = "tFolderName";
            this.tFolderName.Size = new System.Drawing.Size(259, 21);
            this.tFolderName.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "试验室名称：";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(263, 160);
            this.btnOK.Name = "btnOK";
            this.btnOK.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnOK.Size = new System.Drawing.Size(65, 23);
            this.btnOK.TabIndex = 17;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(353, 160);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnCancel.Size = new System.Drawing.Size(65, 23);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtOrderID
            // 
            this.txtOrderID.Location = new System.Drawing.Point(100, 59);
            this.txtOrderID.Name = "txtOrderID";
            this.txtOrderID.Size = new System.Drawing.Size(259, 21);
            this.txtOrderID.TabIndex = 66;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(22, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 67;
            this.label7.Text = "排序(BS):";
            // 
            // FormFolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 202);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.JzGrpBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormFolder";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "新建试验室";
            this.Load += new System.EventHandler(this.FormFolder_Load);
            this.JzGrpBox.ResumeLayout(false);
            this.JzGrpBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox JzGrpBox;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tFolderName;
        private System.Windows.Forms.TextBox txtOrderID;
        private System.Windows.Forms.Label label7;
    }
}