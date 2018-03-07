namespace SerialGenerator
{
    partial class Generator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Generator));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.Btn_Generator = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.Button_Copy = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(56, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "序列号：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(162, 26);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(404, 21);
            this.textBox1.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(57, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "激活码：";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(162, 136);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(302, 21);
            this.textBox3.TabIndex = 5;
            // 
            // Btn_Generator
            // 
            this.Btn_Generator.Location = new System.Drawing.Point(21, 77);
            this.Btn_Generator.Name = "Btn_Generator";
            this.Btn_Generator.Size = new System.Drawing.Size(549, 37);
            this.Btn_Generator.TabIndex = 6;
            this.Btn_Generator.Text = "生成激活码!";
            this.Btn_Generator.UseVisualStyleBackColor = true;
            this.Btn_Generator.Click += new System.EventHandler(this.Btn_Generator_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(57, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "说明：";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(162, 174);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(404, 162);
            this.textBox4.TabIndex = 8;
            // 
            // PictureBox1
            // 
            this.PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(0, 207);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(161, 150);
            this.PictureBox1.TabIndex = 9;
            this.PictureBox1.TabStop = false;
            // 
            // Button_Copy
            // 
            this.Button_Copy.Location = new System.Drawing.Point(470, 136);
            this.Button_Copy.Name = "Button_Copy";
            this.Button_Copy.Size = new System.Drawing.Size(96, 23);
            this.Button_Copy.TabIndex = 10;
            this.Button_Copy.Text = "复制到剪贴板";
            this.Button_Copy.UseVisualStyleBackColor = true;
            this.Button_Copy.Click += new System.EventHandler(this.Button_Copy_Click);
            // 
            // Generator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(583, 357);
            this.Controls.Add(this.Button_Copy);
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Btn_Generator);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Generator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "铁路试验信息管理系统--注册机";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button Btn_Generator;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox4;
        internal System.Windows.Forms.PictureBox PictureBox1;
        private System.Windows.Forms.Button Button_Copy;
    }
}