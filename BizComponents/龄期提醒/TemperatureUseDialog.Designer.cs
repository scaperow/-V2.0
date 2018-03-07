namespace BizComponents
{
    partial class TemperatureUseDialog
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
            this.LabelSystemTempreature = new System.Windows.Forms.Label();
            this.ComboAllTemperature = new System.Windows.Forms.ComboBox();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.ButtonClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LabelSystemTempreature
            // 
            this.LabelSystemTempreature.AutoSize = true;
            this.LabelSystemTempreature.Location = new System.Drawing.Point(14, 17);
            this.LabelSystemTempreature.Name = "LabelSystemTempreature";
            this.LabelSystemTempreature.Size = new System.Drawing.Size(125, 12);
            this.LabelSystemTempreature.TabIndex = 31;
            this.LabelSystemTempreature.Text = "选择要使用的温度类型";
            // 
            // ComboAllTemperature
            // 
            this.ComboAllTemperature.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboAllTemperature.DisplayMember = "Name";
            this.ComboAllTemperature.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboAllTemperature.FormattingEnabled = true;
            this.ComboAllTemperature.Location = new System.Drawing.Point(14, 32);
            this.ComboAllTemperature.Name = "ComboAllTemperature";
            this.ComboAllTemperature.Size = new System.Drawing.Size(262, 20);
            this.ComboAllTemperature.TabIndex = 32;
            this.ComboAllTemperature.ValueMember = "ID";
            // 
            // ButtonSave
            // 
            this.ButtonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonSave.Location = new System.Drawing.Point(118, 99);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(75, 23);
            this.ButtonSave.TabIndex = 2;
            this.ButtonSave.Text = "保存";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ButtonClose
            // 
            this.ButtonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonClose.Location = new System.Drawing.Point(201, 99);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(75, 23);
            this.ButtonClose.TabIndex = 2;
            this.ButtonClose.Text = "关闭";
            this.ButtonClose.UseVisualStyleBackColor = true;
            this.ButtonClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // TemperatureUseDialog
            // 
            this.AcceptButton = this.ButtonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ButtonClose;
            this.ClientSize = new System.Drawing.Size(288, 134);
            this.Controls.Add(this.ButtonClose);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.ComboAllTemperature);
            this.Controls.Add(this.LabelSystemTempreature);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TemperatureUseDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "温度类型设置";
            this.Load += new System.EventHandler(this.TemperatureUseDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelSystemTempreature;
        private System.Windows.Forms.ComboBox ComboAllTemperature;
        private System.Windows.Forms.Button ButtonSave;
        private System.Windows.Forms.Button ButtonClose;
    }
}