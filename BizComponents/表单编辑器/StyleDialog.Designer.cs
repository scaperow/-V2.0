namespace BizComponents
{
    partial class StyleDialog
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
            this.tPage_Font = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ForeColorPicker = new Com.Windows.Forms.ColorPicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Font_Strikeout = new System.Windows.Forms.CheckBox();
            this.Font_Underline = new System.Windows.Forms.CheckBox();
            this.Font_Italic = new System.Windows.Forms.CheckBox();
            this.Font_Bold = new System.Windows.Forms.CheckBox();
            this.Font_Regular = new System.Windows.Forms.CheckBox();
            this.tPage_Color = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.BackColorPicker = new Com.Windows.Forms.ColorPicker();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tPage_Font.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tPage_Color.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tPage_Font);
            this.tabControl1.Controls.Add(this.tPage_Color);
            this.tabControl1.Location = new System.Drawing.Point(8, 7);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(353, 279);
            this.tabControl1.TabIndex = 0;
            // 
            // tPage_Font
            // 
            this.tPage_Font.Controls.Add(this.groupBox3);
            this.tPage_Font.Controls.Add(this.groupBox2);
            this.tPage_Font.Controls.Add(this.groupBox1);
            this.tPage_Font.Location = new System.Drawing.Point(4, 21);
            this.tPage_Font.Name = "tPage_Font";
            this.tPage_Font.Padding = new System.Windows.Forms.Padding(3);
            this.tPage_Font.Size = new System.Drawing.Size(345, 254);
            this.tPage_Font.TabIndex = 0;
            this.tPage_Font.Text = "字体";
            this.tPage_Font.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ForeColorPicker);
            this.groupBox3.Location = new System.Drawing.Point(14, 200);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(157, 40);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "颜色";
            // 
            // ForeColorPicker
            // 
            this.ForeColorPicker.BackColor = System.Drawing.SystemColors.Window;
            this.ForeColorPicker.Context = null;
            this.ForeColorPicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ForeColorPicker.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ForeColorPicker.Location = new System.Drawing.Point(3, 17);
            this.ForeColorPicker.Name = "ForeColorPicker";
            this.ForeColorPicker.ReadOnly = false;
            this.ForeColorPicker.Size = new System.Drawing.Size(151, 21);
            this.ForeColorPicker.TabIndex = 0;
            this.ForeColorPicker.Text = "White";
            this.ForeColorPicker.Value = System.Drawing.Color.White;
            this.ForeColorPicker.ValueChanged += new System.EventHandler(this.ForeColorPicker_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(181, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(153, 230);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "示例";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 210);
            this.label1.TabIndex = 10;
            this.label1.Text = "样式";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Font_Strikeout);
            this.groupBox1.Controls.Add(this.Font_Underline);
            this.groupBox1.Controls.Add(this.Font_Italic);
            this.groupBox1.Controls.Add(this.Font_Bold);
            this.groupBox1.Controls.Add(this.Font_Regular);
            this.groupBox1.Location = new System.Drawing.Point(14, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(157, 184);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字形";
            // 
            // Font_Strikeout
            // 
            this.Font_Strikeout.AutoSize = true;
            this.Font_Strikeout.Location = new System.Drawing.Point(12, 108);
            this.Font_Strikeout.Name = "Font_Strikeout";
            this.Font_Strikeout.Size = new System.Drawing.Size(60, 16);
            this.Font_Strikeout.TabIndex = 18;
            this.Font_Strikeout.Text = "删除线";
            this.Font_Strikeout.UseVisualStyleBackColor = true;
            this.Font_Strikeout.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // Font_Underline
            // 
            this.Font_Underline.AutoSize = true;
            this.Font_Underline.Location = new System.Drawing.Point(12, 86);
            this.Font_Underline.Name = "Font_Underline";
            this.Font_Underline.Size = new System.Drawing.Size(60, 16);
            this.Font_Underline.TabIndex = 17;
            this.Font_Underline.Text = "下划线";
            this.Font_Underline.UseVisualStyleBackColor = true;
            this.Font_Underline.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // Font_Italic
            // 
            this.Font_Italic.AutoSize = true;
            this.Font_Italic.Location = new System.Drawing.Point(12, 64);
            this.Font_Italic.Name = "Font_Italic";
            this.Font_Italic.Size = new System.Drawing.Size(48, 16);
            this.Font_Italic.TabIndex = 16;
            this.Font_Italic.Text = "倾斜";
            this.Font_Italic.UseVisualStyleBackColor = true;
            this.Font_Italic.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // Font_Bold
            // 
            this.Font_Bold.AutoSize = true;
            this.Font_Bold.Location = new System.Drawing.Point(12, 42);
            this.Font_Bold.Name = "Font_Bold";
            this.Font_Bold.Size = new System.Drawing.Size(48, 16);
            this.Font_Bold.TabIndex = 15;
            this.Font_Bold.Text = "加粗";
            this.Font_Bold.UseVisualStyleBackColor = true;
            this.Font_Bold.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // Font_Regular
            // 
            this.Font_Regular.AutoSize = true;
            this.Font_Regular.Location = new System.Drawing.Point(12, 20);
            this.Font_Regular.Name = "Font_Regular";
            this.Font_Regular.Size = new System.Drawing.Size(48, 16);
            this.Font_Regular.TabIndex = 14;
            this.Font_Regular.Text = "常规";
            this.Font_Regular.UseVisualStyleBackColor = true;
            this.Font_Regular.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // tPage_Color
            // 
            this.tPage_Color.Controls.Add(this.groupBox5);
            this.tPage_Color.Controls.Add(this.groupBox4);
            this.tPage_Color.Location = new System.Drawing.Point(4, 21);
            this.tPage_Color.Name = "tPage_Color";
            this.tPage_Color.Padding = new System.Windows.Forms.Padding(3);
            this.tPage_Color.Size = new System.Drawing.Size(345, 254);
            this.tPage_Color.TabIndex = 1;
            this.tPage_Color.Text = "图案";
            this.tPage_Color.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Location = new System.Drawing.Point(181, 10);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(153, 230);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "示例";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(3, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 210);
            this.label2.TabIndex = 10;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.BackColorPicker);
            this.groupBox4.Location = new System.Drawing.Point(14, 10);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(157, 40);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "底色";
            // 
            // BackColorPicker
            // 
            this.BackColorPicker.BackColor = System.Drawing.SystemColors.Window;
            this.BackColorPicker.Context = null;
            this.BackColorPicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BackColorPicker.ForeColor = System.Drawing.SystemColors.WindowText;
            this.BackColorPicker.Location = new System.Drawing.Point(3, 17);
            this.BackColorPicker.Name = "BackColorPicker";
            this.BackColorPicker.ReadOnly = false;
            this.BackColorPicker.Size = new System.Drawing.Size(151, 21);
            this.BackColorPicker.TabIndex = 0;
            this.BackColorPicker.Text = "White";
            this.BackColorPicker.Value = System.Drawing.Color.White;
            this.BackColorPicker.ValueChanged += new System.EventHandler(this.BackColorPicker_ValueChanged);
            // 
            // ButtonOk
            // 
            this.ButtonOk.Location = new System.Drawing.Point(182, 296);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(75, 23);
            this.ButtonOk.TabIndex = 0;
            this.ButtonOk.Text = "确定";
            this.ButtonOk.UseVisualStyleBackColor = true;
            this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(269, 295);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 1;
            this.ButtonCancel.Text = "取消";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // StyleDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 325);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StyleDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "单元格格式";
            this.Load += new System.EventHandler(this.StyleDialog_Load);
            this.tabControl1.ResumeLayout(false);
            this.tPage_Font.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tPage_Color.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tPage_Font;
        private System.Windows.Forms.TabPage tPage_Color;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label2;
        public Com.Windows.Forms.ColorPicker BackColorPicker;
        public Com.Windows.Forms.ColorPicker ForeColorPicker;
        public System.Windows.Forms.CheckBox Font_Regular;
        public System.Windows.Forms.CheckBox Font_Strikeout;
        public System.Windows.Forms.CheckBox Font_Underline;
        public System.Windows.Forms.CheckBox Font_Italic;
        public System.Windows.Forms.CheckBox Font_Bold;
    }
}