namespace ReportComponents
{
    partial class SlashEditor
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
            this.tBoxSlashText = new System.Windows.Forms.TextBox();
            this.rButton_Clockwise = new System.Windows.Forms.RadioButton();
            this.rButton_Counterclockwise = new System.Windows.Forms.RadioButton();
            this.Button_Ok = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "输入斜线文字，以“|”分隔 例如 年龄|班级";
            // 
            // tBoxSlashText
            // 
            this.tBoxSlashText.Location = new System.Drawing.Point(6, 26);
            this.tBoxSlashText.Multiline = true;
            this.tBoxSlashText.Name = "tBoxSlashText";
            this.tBoxSlashText.Size = new System.Drawing.Size(336, 118);
            this.tBoxSlashText.TabIndex = 1;
            // 
            // rButton_Clockwise
            // 
            this.rButton_Clockwise.AutoSize = true;
            this.rButton_Clockwise.Location = new System.Drawing.Point(13, 151);
            this.rButton_Clockwise.Name = "rButton_Clockwise";
            this.rButton_Clockwise.Size = new System.Drawing.Size(119, 16);
            this.rButton_Clockwise.TabIndex = 2;
            this.rButton_Clockwise.TabStop = true;
            this.rButton_Clockwise.Text = "从左上项右下发散";
            this.rButton_Clockwise.UseVisualStyleBackColor = true;
            // 
            // rButton_Counterclockwise
            // 
            this.rButton_Counterclockwise.AutoSize = true;
            this.rButton_Counterclockwise.Checked = true;
            this.rButton_Counterclockwise.Location = new System.Drawing.Point(141, 151);
            this.rButton_Counterclockwise.Name = "rButton_Counterclockwise";
            this.rButton_Counterclockwise.Size = new System.Drawing.Size(119, 16);
            this.rButton_Counterclockwise.TabIndex = 3;
            this.rButton_Counterclockwise.TabStop = true;
            this.rButton_Counterclockwise.Text = "从左下向右上发散";
            this.rButton_Counterclockwise.UseVisualStyleBackColor = true;
            // 
            // Button_Ok
            // 
            this.Button_Ok.Location = new System.Drawing.Point(194, 177);
            this.Button_Ok.Name = "Button_Ok";
            this.Button_Ok.Size = new System.Drawing.Size(68, 23);
            this.Button_Ok.TabIndex = 4;
            this.Button_Ok.Text = "确定";
            this.Button_Ok.UseVisualStyleBackColor = true;
            this.Button_Ok.Click += new System.EventHandler(this.Button_Ok_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Location = new System.Drawing.Point(274, 177);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(68, 23);
            this.Button_Cancel.TabIndex = 5;
            this.Button_Cancel.Text = "取消";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // SlashEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 208);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_Ok);
            this.Controls.Add(this.rButton_Counterclockwise);
            this.Controls.Add(this.rButton_Clockwise);
            this.Controls.Add(this.tBoxSlashText);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SlashEditor";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "斜线定义";
            this.Load += new System.EventHandler(this.SlashEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tBoxSlashText;
        private System.Windows.Forms.RadioButton rButton_Clockwise;
        private System.Windows.Forms.RadioButton rButton_Counterclockwise;
        private System.Windows.Forms.Button Button_Ok;
        private System.Windows.Forms.Button Button_Cancel;
    }
}