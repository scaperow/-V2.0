namespace BizComponents
{
    partial class FormatStyleForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Button_Delete = new System.Windows.Forms.Button();
            this.Button_Append = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lBox_FormatString = new System.Windows.Forms.ListBox();
            this.tBox_FormatString = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rButton_ScientificCount = new System.Windows.Forms.RadioButton();
            this.rButton_Text = new System.Windows.Forms.RadioButton();
            this.rButton_Time = new System.Windows.Forms.RadioButton();
            this.rButton_Date = new System.Windows.Forms.RadioButton();
            this.rButton_Percent = new System.Windows.Forms.RadioButton();
            this.rButton_Currency = new System.Windows.Forms.RadioButton();
            this.rButton_Number = new System.Windows.Forms.RadioButton();
            this.rButton_General = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_Example = new System.Windows.Forms.Label();
            this.Button_Ok = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(8, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(619, 459);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Button_Delete);
            this.tabPage1.Controls.Add(this.Button_Append);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.lBox_FormatString);
            this.tabPage1.Controls.Add(this.tBox_FormatString);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(611, 434);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "样式";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // Button_Delete
            // 
            this.Button_Delete.Location = new System.Drawing.Point(552, 402);
            this.Button_Delete.Name = "Button_Delete";
            this.Button_Delete.Size = new System.Drawing.Size(51, 23);
            this.Button_Delete.TabIndex = 15;
            this.Button_Delete.Text = "删除";
            this.Button_Delete.UseVisualStyleBackColor = true;
            this.Button_Delete.Click += new System.EventHandler(this.Button_Delete_Click);
            // 
            // Button_Append
            // 
            this.Button_Append.Location = new System.Drawing.Point(495, 402);
            this.Button_Append.Name = "Button_Append";
            this.Button_Append.Size = new System.Drawing.Size(51, 23);
            this.Button_Append.TabIndex = 14;
            this.Button_Append.Text = "添加";
            this.Button_Append.UseVisualStyleBackColor = true;
            this.Button_Append.Click += new System.EventHandler(this.Button_Append_Click);
            // 
            // textBox1
            // 
            this.textBox1.HideSelection = false;
            this.textBox1.Location = new System.Drawing.Point(105, 404);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(384, 21);
            this.textBox1.TabIndex = 13;
            // 
            // lBox_FormatString
            // 
            this.lBox_FormatString.FormattingEnabled = true;
            this.lBox_FormatString.ItemHeight = 12;
            this.lBox_FormatString.Location = new System.Drawing.Point(105, 94);
            this.lBox_FormatString.Name = "lBox_FormatString";
            this.lBox_FormatString.Size = new System.Drawing.Size(498, 304);
            this.lBox_FormatString.TabIndex = 12;
            // 
            // tBox_FormatString
            // 
            this.tBox_FormatString.BackColor = System.Drawing.Color.White;
            this.tBox_FormatString.HideSelection = false;
            this.tBox_FormatString.Location = new System.Drawing.Point(105, 67);
            this.tBox_FormatString.Name = "tBox_FormatString";
            this.tBox_FormatString.ReadOnly = true;
            this.tBox_FormatString.Size = new System.Drawing.Size(498, 21);
            this.tBox_FormatString.TabIndex = 11;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rButton_ScientificCount);
            this.groupBox2.Controls.Add(this.rButton_Text);
            this.groupBox2.Controls.Add(this.rButton_Time);
            this.groupBox2.Controls.Add(this.rButton_Date);
            this.groupBox2.Controls.Add(this.rButton_Percent);
            this.groupBox2.Controls.Add(this.rButton_Currency);
            this.groupBox2.Controls.Add(this.rButton_Number);
            this.groupBox2.Controls.Add(this.rButton_General);
            this.groupBox2.Location = new System.Drawing.Point(8, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(91, 364);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "分类";
            // 
            // rButton_ScientificCount
            // 
            this.rButton_ScientificCount.AutoSize = true;
            this.rButton_ScientificCount.Location = new System.Drawing.Point(15, 131);
            this.rButton_ScientificCount.Name = "rButton_ScientificCount";
            this.rButton_ScientificCount.Size = new System.Drawing.Size(71, 16);
            this.rButton_ScientificCount.TabIndex = 10;
            this.rButton_ScientificCount.TabStop = true;
            this.rButton_ScientificCount.Text = "科学计数";
            this.rButton_ScientificCount.UseVisualStyleBackColor = true;
            this.rButton_ScientificCount.Click += new System.EventHandler(this.radioButton_Click);
            // 
            // rButton_Text
            // 
            this.rButton_Text.AutoSize = true;
            this.rButton_Text.Location = new System.Drawing.Point(15, 212);
            this.rButton_Text.Name = "rButton_Text";
            this.rButton_Text.Size = new System.Drawing.Size(59, 16);
            this.rButton_Text.TabIndex = 9;
            this.rButton_Text.TabStop = true;
            this.rButton_Text.Text = "文本型";
            this.rButton_Text.UseVisualStyleBackColor = true;
            this.rButton_Text.Click += new System.EventHandler(this.radioButton_Click);
            // 
            // rButton_Time
            // 
            this.rButton_Time.AutoSize = true;
            this.rButton_Time.Location = new System.Drawing.Point(15, 158);
            this.rButton_Time.Name = "rButton_Time";
            this.rButton_Time.Size = new System.Drawing.Size(59, 16);
            this.rButton_Time.TabIndex = 8;
            this.rButton_Time.TabStop = true;
            this.rButton_Time.Text = "时间型";
            this.rButton_Time.UseVisualStyleBackColor = true;
            this.rButton_Time.Click += new System.EventHandler(this.radioButton_Click);
            // 
            // rButton_Date
            // 
            this.rButton_Date.AutoSize = true;
            this.rButton_Date.Location = new System.Drawing.Point(15, 185);
            this.rButton_Date.Name = "rButton_Date";
            this.rButton_Date.Size = new System.Drawing.Size(59, 16);
            this.rButton_Date.TabIndex = 7;
            this.rButton_Date.TabStop = true;
            this.rButton_Date.Text = "日期型";
            this.rButton_Date.UseVisualStyleBackColor = true;
            this.rButton_Date.Click += new System.EventHandler(this.radioButton_Click);
            // 
            // rButton_Percent
            // 
            this.rButton_Percent.AutoSize = true;
            this.rButton_Percent.Location = new System.Drawing.Point(15, 104);
            this.rButton_Percent.Name = "rButton_Percent";
            this.rButton_Percent.Size = new System.Drawing.Size(59, 16);
            this.rButton_Percent.TabIndex = 6;
            this.rButton_Percent.TabStop = true;
            this.rButton_Percent.Text = "百分比";
            this.rButton_Percent.UseVisualStyleBackColor = true;
            this.rButton_Percent.Click += new System.EventHandler(this.radioButton_Click);
            // 
            // rButton_Currency
            // 
            this.rButton_Currency.AutoSize = true;
            this.rButton_Currency.Location = new System.Drawing.Point(15, 77);
            this.rButton_Currency.Name = "rButton_Currency";
            this.rButton_Currency.Size = new System.Drawing.Size(47, 16);
            this.rButton_Currency.TabIndex = 5;
            this.rButton_Currency.TabStop = true;
            this.rButton_Currency.Text = "货币";
            this.rButton_Currency.UseVisualStyleBackColor = true;
            this.rButton_Currency.Click += new System.EventHandler(this.radioButton_Click);
            // 
            // rButton_Number
            // 
            this.rButton_Number.AutoSize = true;
            this.rButton_Number.Location = new System.Drawing.Point(15, 50);
            this.rButton_Number.Name = "rButton_Number";
            this.rButton_Number.Size = new System.Drawing.Size(47, 16);
            this.rButton_Number.TabIndex = 4;
            this.rButton_Number.TabStop = true;
            this.rButton_Number.Text = "数字";
            this.rButton_Number.UseVisualStyleBackColor = true;
            this.rButton_Number.Click += new System.EventHandler(this.radioButton_Click);
            // 
            // rButton_General
            // 
            this.rButton_General.AutoSize = true;
            this.rButton_General.Location = new System.Drawing.Point(15, 23);
            this.rButton_General.Name = "rButton_General";
            this.rButton_General.Size = new System.Drawing.Size(47, 16);
            this.rButton_General.TabIndex = 3;
            this.rButton_General.TabStop = true;
            this.rButton_General.Text = "常规";
            this.rButton_General.UseVisualStyleBackColor = true;
            this.rButton_General.Click += new System.EventHandler(this.radioButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_Example);
            this.groupBox1.Location = new System.Drawing.Point(8, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(595, 54);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "示例";
            // 
            // label_Example
            // 
            this.label_Example.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Example.Location = new System.Drawing.Point(3, 17);
            this.label_Example.Name = "label_Example";
            this.label_Example.Size = new System.Drawing.Size(589, 34);
            this.label_Example.TabIndex = 4;
            this.label_Example.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Button_Ok
            // 
            this.Button_Ok.Location = new System.Drawing.Point(428, 484);
            this.Button_Ok.Name = "Button_Ok";
            this.Button_Ok.Size = new System.Drawing.Size(75, 23);
            this.Button_Ok.TabIndex = 1;
            this.Button_Ok.Text = "确定";
            this.Button_Ok.UseVisualStyleBackColor = true;
            this.Button_Ok.Click += new System.EventHandler(this.Button_Ok_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Location = new System.Drawing.Point(518, 484);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 2;
            this.Button_Cancel.Text = "取消";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // FormatStyleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 518);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_Ok);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormatStyleForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "单元格样式窗口";
            this.Load += new System.EventHandler(this.FormatStyleForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button Button_Ok;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_Example;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rButton_ScientificCount;
        private System.Windows.Forms.RadioButton rButton_Text;
        private System.Windows.Forms.RadioButton rButton_Time;
        private System.Windows.Forms.RadioButton rButton_Date;
        private System.Windows.Forms.RadioButton rButton_Percent;
        private System.Windows.Forms.RadioButton rButton_Currency;
        private System.Windows.Forms.RadioButton rButton_Number;
        private System.Windows.Forms.RadioButton rButton_General;
        private System.Windows.Forms.TextBox tBox_FormatString;
        private System.Windows.Forms.ListBox lBox_FormatString;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Button_Delete;
        private System.Windows.Forms.Button Button_Append;

    }
}