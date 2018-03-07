namespace BizComponents
{
    partial class TestDemoGGC
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
            this.btn_sent = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.rb_yl = new System.Windows.Forms.RadioButton();
            this.rb_wn = new System.Windows.Forms.RadioButton();
            this.cb_items = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_phhz = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_ldzdl = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_qfl = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_dhbj = new System.Windows.Forms.TextBox();
            this.tb_wtbh = new System.Windows.Forms.TextBox();
            this.list_Statum = new System.Windows.Forms.ListBox();
            this.rb_offLine = new System.Windows.Forms.RadioButton();
            this.rb_statum = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_total = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cb_module = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUploadAll = new System.Windows.Forms.Button();
            this.btnUploadAllOne = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_sent
            // 
            this.btn_sent.Location = new System.Drawing.Point(233, 383);
            this.btn_sent.Name = "btn_sent";
            this.btn_sent.Size = new System.Drawing.Size(85, 23);
            this.btn_sent.TabIndex = 0;
            this.btn_sent.Text = "上传一组试验";
            this.btn_sent.UseVisualStyleBackColor = true;
            this.btn_sent.Click += new System.EventHandler(this.btn_sent_Click);
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(324, 384);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 23);
            this.btn_close.TabIndex = 0;
            this.btn_close.Text = "关闭";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // rb_yl
            // 
            this.rb_yl.AutoSize = true;
            this.rb_yl.Location = new System.Drawing.Point(13, 30);
            this.rb_yl.Name = "rb_yl";
            this.rb_yl.Size = new System.Drawing.Size(59, 16);
            this.rb_yl.TabIndex = 1;
            this.rb_yl.TabStop = true;
            this.rb_yl.Text = "压力机";
            this.rb_yl.UseVisualStyleBackColor = true;
            this.rb_yl.CheckedChanged += new System.EventHandler(this.rb_yl_CheckedChanged);
            // 
            // rb_wn
            // 
            this.rb_wn.AutoSize = true;
            this.rb_wn.Location = new System.Drawing.Point(13, 71);
            this.rb_wn.Name = "rb_wn";
            this.rb_wn.Size = new System.Drawing.Size(59, 16);
            this.rb_wn.TabIndex = 1;
            this.rb_wn.TabStop = true;
            this.rb_wn.Text = "万能机";
            this.rb_wn.UseVisualStyleBackColor = true;
            this.rb_wn.CheckedChanged += new System.EventHandler(this.rb_yl_CheckedChanged);
            // 
            // cb_items
            // 
            this.cb_items.FormattingEnabled = true;
            this.cb_items.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15"});
            this.cb_items.Location = new System.Drawing.Point(346, 13);
            this.cb_items.Name = "cb_items";
            this.cb_items.Size = new System.Drawing.Size(68, 20);
            this.cb_items.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(90, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "破坏荷载";
            // 
            // tb_phhz
            // 
            this.tb_phhz.Location = new System.Drawing.Point(158, 30);
            this.tb_phhz.Name = "tb_phhz";
            this.tb_phhz.Size = new System.Drawing.Size(83, 21);
            this.tb_phhz.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "拉断最大力";
            // 
            // tb_ldzdl
            // 
            this.tb_ldzdl.Location = new System.Drawing.Point(158, 70);
            this.tb_ldzdl.Name = "tb_ldzdl";
            this.tb_ldzdl.Size = new System.Drawing.Size(83, 21);
            this.tb_ldzdl.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(251, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "屈服力";
            // 
            // tb_qfl
            // 
            this.tb_qfl.Location = new System.Drawing.Point(313, 71);
            this.tb_qfl.Name = "tb_qfl";
            this.tb_qfl.Size = new System.Drawing.Size(82, 21);
            this.tb_qfl.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(91, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "断后标距";
            // 
            // tb_dhbj
            // 
            this.tb_dhbj.Location = new System.Drawing.Point(159, 97);
            this.tb_dhbj.Name = "tb_dhbj";
            this.tb_dhbj.Size = new System.Drawing.Size(82, 21);
            this.tb_dhbj.TabIndex = 4;
            // 
            // tb_wtbh
            // 
            this.tb_wtbh.Location = new System.Drawing.Point(114, 226);
            this.tb_wtbh.Name = "tb_wtbh";
            this.tb_wtbh.Size = new System.Drawing.Size(300, 21);
            this.tb_wtbh.TabIndex = 4;
            // 
            // list_Statum
            // 
            this.list_Statum.FormattingEnabled = true;
            this.list_Statum.ItemHeight = 12;
            this.list_Statum.Location = new System.Drawing.Point(100, 280);
            this.list_Statum.Name = "list_Statum";
            this.list_Statum.Size = new System.Drawing.Size(314, 88);
            this.list_Statum.TabIndex = 5;
            // 
            // rb_offLine
            // 
            this.rb_offLine.AutoSize = true;
            this.rb_offLine.Location = new System.Drawing.Point(13, 226);
            this.rb_offLine.Name = "rb_offLine";
            this.rb_offLine.Size = new System.Drawing.Size(95, 16);
            this.rb_offLine.TabIndex = 6;
            this.rb_offLine.TabStop = true;
            this.rb_offLine.Text = "离线委托编号";
            this.rb_offLine.UseVisualStyleBackColor = true;
            this.rb_offLine.CheckedChanged += new System.EventHandler(this.rb_offLine_CheckedChanged);
            // 
            // rb_statum
            // 
            this.rb_statum.AutoSize = true;
            this.rb_statum.Location = new System.Drawing.Point(13, 280);
            this.rb_statum.Name = "rb_statum";
            this.rb_statum.Size = new System.Drawing.Size(71, 16);
            this.rb_statum.TabIndex = 6;
            this.rb_statum.TabStop = true;
            this.rb_statum.Text = "选择龄期";
            this.rb_statum.UseVisualStyleBackColor = true;
            this.rb_statum.CheckedChanged += new System.EventHandler(this.rb_offLine_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "试验数量";
            // 
            // cb_total
            // 
            this.cb_total.FormattingEnabled = true;
            this.cb_total.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15"});
            this.cb_total.Location = new System.Drawing.Point(97, 12);
            this.cb_total.Name = "cb_total";
            this.cb_total.Size = new System.Drawing.Size(82, 20);
            this.cb_total.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(248, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "当前第几根/块";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 192);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "试验模板";
            // 
            // cb_module
            // 
            this.cb_module.FormattingEnabled = true;
            this.cb_module.Items.AddRange(new object[] {
            "混凝土检查试件抗压强度试验报告",
            "钢筋试验报告"});
            this.cb_module.Location = new System.Drawing.Point(97, 189);
            this.cb_module.Name = "cb_module";
            this.cb_module.Size = new System.Drawing.Size(317, 20);
            this.cb_module.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rb_yl);
            this.groupBox1.Controls.Add(this.rb_wn);
            this.groupBox1.Controls.Add(this.tb_phhz);
            this.groupBox1.Controls.Add(this.tb_dhbj);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tb_ldzdl);
            this.groupBox1.Controls.Add(this.tb_qfl);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(13, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(401, 133);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // btnUploadAll
            // 
            this.btnUploadAll.Location = new System.Drawing.Point(42, 384);
            this.btnUploadAll.Name = "btnUploadAll";
            this.btnUploadAll.Size = new System.Drawing.Size(85, 23);
            this.btnUploadAll.TabIndex = 8;
            this.btnUploadAll.Text = "上传所有龄期";
            this.btnUploadAll.UseVisualStyleBackColor = true;
            this.btnUploadAll.Click += new System.EventHandler(this.btnUploadAll_Click);
            // 
            // btnUploadAllOne
            // 
            this.btnUploadAllOne.Location = new System.Drawing.Point(133, 384);
            this.btnUploadAllOne.Name = "btnUploadAllOne";
            this.btnUploadAllOne.Size = new System.Drawing.Size(94, 23);
            this.btnUploadAllOne.TabIndex = 8;
            this.btnUploadAllOne.Text = "上传一次试验";
            this.btnUploadAllOne.UseVisualStyleBackColor = true;
            this.btnUploadAllOne.Click += new System.EventHandler(this.btnUploadAllOne_Click);
            // 
            // TestDemoGGC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 418);
            this.Controls.Add(this.btnUploadAllOne);
            this.Controls.Add(this.btnUploadAll);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rb_statum);
            this.Controls.Add(this.rb_offLine);
            this.Controls.Add(this.list_Statum);
            this.Controls.Add(this.tb_wtbh);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cb_module);
            this.Controls.Add(this.cb_total);
            this.Controls.Add(this.cb_items);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_sent);
            this.Name = "TestDemoGGC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GGC模拟上传采集数据";
            this.Load += new System.EventHandler(this.TestDemo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_sent;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.RadioButton rb_yl;
        private System.Windows.Forms.RadioButton rb_wn;
        private System.Windows.Forms.ComboBox cb_items;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_phhz;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_ldzdl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_qfl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_dhbj;
        private System.Windows.Forms.TextBox tb_wtbh;
        private System.Windows.Forms.ListBox list_Statum;
        private System.Windows.Forms.RadioButton rb_offLine;
        private System.Windows.Forms.RadioButton rb_statum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cb_total;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cb_module;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnUploadAll;
        private System.Windows.Forms.Button btnUploadAllOne;
    }
}