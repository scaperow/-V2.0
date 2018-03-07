namespace BizComponents
{
    partial class ModuleSettingDialog
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.bt_cellSelect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.bt_OK = new System.Windows.Forms.Button();
            this.bt_cancel = new System.Windows.Forms.Button();
            this.tb_cell = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_description = new System.Windows.Forms.TextBox();
            this.cb_show = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择系统字段：";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(101, 47);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(157, 20);
            this.comboBox1.TabIndex = 1;
            // 
            // bt_cellSelect
            // 
            this.bt_cellSelect.Location = new System.Drawing.Point(200, 81);
            this.bt_cellSelect.Name = "bt_cellSelect";
            this.bt_cellSelect.Size = new System.Drawing.Size(58, 23);
            this.bt_cellSelect.TabIndex = 2;
            this.bt_cellSelect.Text = "选择";
            this.bt_cellSelect.UseVisualStyleBackColor = true;
            this.bt_cellSelect.Click += new System.EventHandler(this.bt_cellSelect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "选择数据字段：";
            // 
            // bt_OK
            // 
            this.bt_OK.Location = new System.Drawing.Point(86, 150);
            this.bt_OK.Name = "bt_OK";
            this.bt_OK.Size = new System.Drawing.Size(75, 23);
            this.bt_OK.TabIndex = 2;
            this.bt_OK.Text = "确定";
            this.bt_OK.UseVisualStyleBackColor = true;
            this.bt_OK.Click += new System.EventHandler(this.bt_OK_Click);
            // 
            // bt_cancel
            // 
            this.bt_cancel.Location = new System.Drawing.Point(183, 150);
            this.bt_cancel.Name = "bt_cancel";
            this.bt_cancel.Size = new System.Drawing.Size(75, 23);
            this.bt_cancel.TabIndex = 2;
            this.bt_cancel.Text = "取消";
            this.bt_cancel.UseVisualStyleBackColor = true;
            this.bt_cancel.Click += new System.EventHandler(this.bt_cancel_Click);
            // 
            // tb_cell
            // 
            this.tb_cell.BackColor = System.Drawing.Color.White;
            this.tb_cell.Location = new System.Drawing.Point(101, 81);
            this.tb_cell.Name = "tb_cell";
            this.tb_cell.ReadOnly = true;
            this.tb_cell.Size = new System.Drawing.Size(93, 21);
            this.tb_cell.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "显示名称：";
            // 
            // tb_description
            // 
            this.tb_description.BackColor = System.Drawing.Color.White;
            this.tb_description.Location = new System.Drawing.Point(101, 14);
            this.tb_description.Name = "tb_description";
            this.tb_description.Size = new System.Drawing.Size(157, 21);
            this.tb_description.TabIndex = 3;
            // 
            // cb_show
            // 
            this.cb_show.AutoSize = true;
            this.cb_show.Location = new System.Drawing.Point(101, 120);
            this.cb_show.Name = "cb_show";
            this.cb_show.Size = new System.Drawing.Size(96, 16);
            this.cb_show.TabIndex = 4;
            this.cb_show.Text = "在台账中显示";
            this.cb_show.UseVisualStyleBackColor = true;
            // 
            // ModuleSettingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 188);
            this.Controls.Add(this.cb_show);
            this.Controls.Add(this.tb_description);
            this.Controls.Add(this.tb_cell);
            this.Controls.Add(this.bt_cancel);
            this.Controls.Add(this.bt_OK);
            this.Controls.Add(this.bt_cellSelect);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "ModuleSettingDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统字段设置";
            this.Load += new System.EventHandler(this.ModuleSettingDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button bt_cellSelect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bt_OK;
        private System.Windows.Forms.Button bt_cancel;
        private System.Windows.Forms.TextBox tb_cell;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_description;
        private System.Windows.Forms.CheckBox cb_show;
    }
}