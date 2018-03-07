namespace Yqun.Permissions
{
    partial class UserDialog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Button_New = new System.Windows.Forms.Button();
            this.Button_Remove = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Button_OK = new System.Windows.Forms.Button();
            this.TextBox_Name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RolesView = new System.Windows.Forms.DataGridView();
            this.Column_Role = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TextBox_Password1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TextBox_Password2 = new System.Windows.Forms.TextBox();
            this.CheckBox_PasswordChar = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.RolesView)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button_New
            // 
            this.Button_New.Location = new System.Drawing.Point(7, 176);
            this.Button_New.Name = "Button_New";
            this.Button_New.Size = new System.Drawing.Size(75, 23);
            this.Button_New.TabIndex = 13;
            this.Button_New.Text = "添加岗位";
            this.Button_New.UseVisualStyleBackColor = true;
            this.Button_New.Click += new System.EventHandler(this.Button_New_Click);
            // 
            // Button_Remove
            // 
            this.Button_Remove.Location = new System.Drawing.Point(82, 176);
            this.Button_Remove.Name = "Button_Remove";
            this.Button_Remove.Size = new System.Drawing.Size(75, 23);
            this.Button_Remove.TabIndex = 14;
            this.Button_Remove.Text = "移除岗位";
            this.Button_Remove.UseVisualStyleBackColor = true;
            this.Button_Remove.Click += new System.EventHandler(this.Button_Remove_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(8, 338);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(500, 9);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Location = new System.Drawing.Point(391, 356);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 16;
            this.Button_Cancel.Text = "取消";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Button_OK
            // 
            this.Button_OK.Location = new System.Drawing.Point(299, 357);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 15;
            this.Button_OK.Text = "确定";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // TextBox_Name
            // 
            this.TextBox_Name.Location = new System.Drawing.Point(86, 48);
            this.TextBox_Name.Name = "TextBox_Name";
            this.TextBox_Name.Size = new System.Drawing.Size(406, 21);
            this.TextBox_Name.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "用户名称:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(480, 19);
            this.label3.TabIndex = 20;
            this.label3.Text = "说明：新建用户，给用户选择合适的岗位，则具有该岗位的权限。";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(8, 30);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(500, 9);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            // 
            // RolesView
            // 
            this.RolesView.AllowUserToAddRows = false;
            this.RolesView.AllowUserToDeleteRows = false;
            this.RolesView.AllowUserToResizeColumns = false;
            this.RolesView.AllowUserToResizeRows = false;
            this.RolesView.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RolesView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.RolesView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RolesView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_Role});
            this.RolesView.Dock = System.Windows.Forms.DockStyle.Top;
            this.RolesView.Location = new System.Drawing.Point(3, 17);
            this.RolesView.Name = "RolesView";
            this.RolesView.RowHeadersVisible = false;
            this.RolesView.RowTemplate.Height = 23;
            this.RolesView.Size = new System.Drawing.Size(475, 151);
            this.RolesView.TabIndex = 22;
            // 
            // Column_Role
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column_Role.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column_Role.HeaderText = "岗位";
            this.Column_Role.Name = "Column_Role";
            this.Column_Role.ReadOnly = true;
            this.Column_Role.Width = 400;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.RolesView);
            this.groupBox3.Controls.Add(this.Button_New);
            this.groupBox3.Controls.Add(this.Button_Remove);
            this.groupBox3.Location = new System.Drawing.Point(11, 133);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(481, 205);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "所属岗位列表";
            // 
            // TextBox_Password1
            // 
            this.TextBox_Password1.Location = new System.Drawing.Point(86, 76);
            this.TextBox_Password1.Name = "TextBox_Password1";
            this.TextBox_Password1.PasswordChar = '*';
            this.TextBox_Password1.Size = new System.Drawing.Size(406, 21);
            this.TextBox_Password1.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "用户密码:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 26;
            this.label4.Text = "确认密码:";
            // 
            // TextBox_Password2
            // 
            this.TextBox_Password2.Location = new System.Drawing.Point(86, 105);
            this.TextBox_Password2.Name = "TextBox_Password2";
            this.TextBox_Password2.PasswordChar = '*';
            this.TextBox_Password2.Size = new System.Drawing.Size(406, 21);
            this.TextBox_Password2.TabIndex = 27;
            // 
            // CheckBox_PasswordChar
            // 
            this.CheckBox_PasswordChar.AutoSize = true;
            this.CheckBox_PasswordChar.Location = new System.Drawing.Point(22, 360);
            this.CheckBox_PasswordChar.Name = "CheckBox_PasswordChar";
            this.CheckBox_PasswordChar.Size = new System.Drawing.Size(72, 16);
            this.CheckBox_PasswordChar.TabIndex = 28;
            this.CheckBox_PasswordChar.Text = "显示字符";
            this.CheckBox_PasswordChar.UseVisualStyleBackColor = true;
            this.CheckBox_PasswordChar.CheckedChanged += new System.EventHandler(this.CheckBox_PasswordChar_CheckedChanged);
            // 
            // UserDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 391);
            this.Controls.Add(this.CheckBox_PasswordChar);
            this.Controls.Add(this.TextBox_Password2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TextBox_Password1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.TextBox_Name);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新建用户";
            ((System.ComponentModel.ISupportInitialize)(this.RolesView)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Button_New;
        private System.Windows.Forms.Button Button_Remove;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.TextBox TextBox_Name;
        internal System.Windows.Forms.DataGridView RolesView;
        private System.Windows.Forms.GroupBox groupBox3;
        internal System.Windows.Forms.TextBox TextBox_Password1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox TextBox_Password2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Role;
        private System.Windows.Forms.CheckBox CheckBox_PasswordChar;
    }
}