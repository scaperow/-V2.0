namespace BizComponents
{
    partial class DictionaryReferenceItemEditor
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
            this.TextBox_Items = new System.Windows.Forms.TextBox();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TextBox_Items
            // 
            this.TextBox_Items.Location = new System.Drawing.Point(12, 32);
            this.TextBox_Items.Multiline = true;
            this.TextBox_Items.Name = "TextBox_Items";
            this.TextBox_Items.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TextBox_Items.Size = new System.Drawing.Size(437, 313);
            this.TextBox_Items.TabIndex = 0;
            // 
            // ButtonOk
            // 
            this.ButtonOk.Location = new System.Drawing.Point(324, 359);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(75, 23);
            this.ButtonOk.TabIndex = 1;
            this.ButtonOk.Text = "确定";
            this.ButtonOk.UseVisualStyleBackColor = true;
            this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "在文本框中输入字符串(每行一个):";
            // 
            // ReferenceItemEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 397);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.TextBox_Items);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReferenceItemEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "编辑字典项";
            this.Load += new System.EventHandler(this.ReferenceItemEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox TextBox_Items;
    }
}