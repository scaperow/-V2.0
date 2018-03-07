namespace Yqun.Permissions
{
    partial class RoleDialog
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Button_OK = new System.Windows.Forms.Button();
            this.TextBox_Name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CheckBox_IsAdmin = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(7, 30);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(487, 8);
            this.groupBox2.TabIndex = 32;
            this.groupBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(11, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(480, 19);
            this.label3.TabIndex = 31;
            this.label3.Text = "说明：新建岗位。当勾选“系统管理员”时，该岗位上的用户则具有所有权限。";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(7, 271);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(487, 8);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Location = new System.Drawing.Point(389, 291);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 27;
            this.Button_Cancel.Text = "取消";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Button_OK
            // 
            this.Button_OK.Location = new System.Drawing.Point(294, 291);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 26;
            this.Button_OK.Text = "确定";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // TextBox_Name
            // 
            this.TextBox_Name.Location = new System.Drawing.Point(68, 49);
            this.TextBox_Name.Name = "TextBox_Name";
            this.TextBox_Name.Size = new System.Drawing.Size(422, 21);
            this.TextBox_Name.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 22;
            this.label1.Text = "名称(&N):";
            // 
            // CheckBox_IsAdmin
            // 
            this.CheckBox_IsAdmin.AutoSize = true;
            this.CheckBox_IsAdmin.Location = new System.Drawing.Point(68, 80);
            this.CheckBox_IsAdmin.Name = "CheckBox_IsAdmin";
            this.CheckBox_IsAdmin.Size = new System.Drawing.Size(84, 16);
            this.CheckBox_IsAdmin.TabIndex = 33;
            this.CheckBox_IsAdmin.Text = "系统管理员";
            this.CheckBox_IsAdmin.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(13, 249);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(330, 12);
            this.label2.TabIndex = 34;
            this.label2.Text = "注意：勾选“系统管理员“后当前岗位则具有所有权限。";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RoleDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 323);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CheckBox_IsAdmin);
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
            this.Name = "RoleDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新建岗位";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox TextBox_Name;
        internal System.Windows.Forms.CheckBox CheckBox_IsAdmin;
        private System.Windows.Forms.Label label2;
    }
}