namespace BizComponents
{
    partial class SearchDialog
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton_JP = new System.Windows.Forms.RadioButton();
            this.radioButton_ZW = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.button = new System.Windows.Forms.Button();
            this.TextBox_Search = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button);
            this.groupBox1.Controls.Add(this.TextBox_Search);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 67);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton_JP);
            this.panel1.Controls.Add(this.radioButton_ZW);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 20);
            this.panel1.TabIndex = 0;
            // 
            // radioButton_JP
            // 
            this.radioButton_JP.AutoSize = true;
            this.radioButton_JP.Checked = true;
            this.radioButton_JP.Location = new System.Drawing.Point(37, 3);
            this.radioButton_JP.Name = "radioButton_JP";
            this.radioButton_JP.Size = new System.Drawing.Size(71, 16);
            this.radioButton_JP.TabIndex = 0;
            this.radioButton_JP.TabStop = true;
            this.radioButton_JP.Text = "简拼查找";
            this.radioButton_JP.UseVisualStyleBackColor = true;
            // 
            // radioButton_ZW
            // 
            this.radioButton_ZW.AutoSize = true;
            this.radioButton_ZW.Location = new System.Drawing.Point(148, 3);
            this.radioButton_ZW.Name = "radioButton_ZW";
            this.radioButton_ZW.Size = new System.Drawing.Size(71, 16);
            this.radioButton_ZW.TabIndex = 1;
            this.radioButton_ZW.Text = "中文查找";
            this.radioButton_ZW.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "提示：请输入要查找目标名前几个字的声母";
            // 
            // button
            // 
            this.button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button.Location = new System.Drawing.Point(160, 94);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(75, 23);
            this.button.TabIndex = 1;
            this.button.Text = "b";
            this.button.UseVisualStyleBackColor = true;
            // 
            // TextBox_Search
            // 
            this.TextBox_Search.Location = new System.Drawing.Point(12, 38);
            this.TextBox_Search.Name = "TextBox_Search";
            this.TextBox_Search.Size = new System.Drawing.Size(250, 21);
            this.TextBox_Search.TabIndex = 0;
            this.TextBox_Search.TextChanged += new System.EventHandler(this.TextBox_Search_TextChanged);
            // 
            // SearchDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 67);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查找";
            this.Load += new System.EventHandler(this.SearchDialog_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SearchDialog_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.RadioButton radioButton_JP;
        public System.Windows.Forms.RadioButton radioButton_ZW;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button;
        public System.Windows.Forms.TextBox TextBox_Search;
    }
}