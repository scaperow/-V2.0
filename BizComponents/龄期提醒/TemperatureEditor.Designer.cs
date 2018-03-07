namespace BizComponents
{
    partial class TemperatureEditor
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
            this.TextName = new System.Windows.Forms.TextBox();
            this.ButtonClose = new System.Windows.Forms.Button();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.CheckIsSystem = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "温度类型名称";
            // 
            // TextName
            // 
            this.TextName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TextName.Location = new System.Drawing.Point(12, 29);
            this.TextName.MaxLength = 50;
            this.TextName.Name = "TextName";
            this.TextName.Size = new System.Drawing.Size(288, 21);
            this.TextName.TabIndex = 1;
            // 
            // ButtonClose
            // 
            this.ButtonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonClose.Location = new System.Drawing.Point(225, 113);
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
            this.ButtonSave.Location = new System.Drawing.Point(144, 113);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(75, 23);
            this.ButtonSave.TabIndex = 2;
            this.ButtonSave.Text = "保存";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // CheckIsSystem
            // 
            this.CheckIsSystem.AutoSize = true;
            this.CheckIsSystem.Location = new System.Drawing.Point(13, 117);
            this.CheckIsSystem.Name = "CheckIsSystem";
            this.CheckIsSystem.Size = new System.Drawing.Size(108, 16);
            this.CheckIsSystem.TabIndex = 3;
            this.CheckIsSystem.Text = "是否为系统级别";
            this.CheckIsSystem.UseVisualStyleBackColor = true;
            // 
            // TemperatureEditor
            // 
            this.AcceptButton = this.ButtonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ButtonClose;
            this.ClientSize = new System.Drawing.Size(312, 148);
            this.Controls.Add(this.CheckIsSystem);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.ButtonClose);
            this.Controls.Add(this.TextName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TemperatureEditor";
            this.Text = "自定义温度类型";
            this.Load += new System.EventHandler(this.TemperatureEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextName;
        private System.Windows.Forms.Button ButtonClose;
        private System.Windows.Forms.Button ButtonSave;
        private System.Windows.Forms.CheckBox CheckIsSystem;
    }
}