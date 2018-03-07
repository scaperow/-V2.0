namespace BizComponents
{
    partial class DeviceDetail
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.Delete = new System.Windows.Forms.Button();
            this.Edit = new System.Windows.Forms.Button();
            this.Close = new System.Windows.Forms.Button();
            this.Information = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.Delete);
            this.panel1.Controls.Add(this.Edit);
            this.panel1.Controls.Add(this.Close);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 359);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(464, 52);
            this.panel1.TabIndex = 1;
            // 
            // Delete
            // 
            this.Delete.Location = new System.Drawing.Point(91, 17);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(75, 23);
            this.Delete.TabIndex = 0;
            this.Delete.Text = "删除(&D)";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // Edit
            // 
            this.Edit.Location = new System.Drawing.Point(11, 17);
            this.Edit.Name = "Edit";
            this.Edit.Size = new System.Drawing.Size(75, 23);
            this.Edit.TabIndex = 0;
            this.Edit.Text = "修改(&E)";
            this.Edit.UseVisualStyleBackColor = true;
            this.Edit.Click += new System.EventHandler(this.Edit_Click);
            // 
            // Close
            // 
            this.Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Close.Location = new System.Drawing.Point(377, 17);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(75, 23);
            this.Close.TabIndex = 0;
            this.Close.Text = "关闭(&C)";
            this.Close.UseVisualStyleBackColor = true;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // Information
            // 
            this.Information.BackColor = System.Drawing.Color.White;
            this.Information.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Information.Location = new System.Drawing.Point(0, 0);
            this.Information.Multiline = true;
            this.Information.Name = "Information";
            this.Information.ReadOnly = true;
            this.Information.Size = new System.Drawing.Size(464, 359);
            this.Information.TabIndex = 2;
            // 
            // DeviceDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(464, 411);
            this.Controls.Add(this.Information);
            this.Controls.Add(this.panel1);
            this.Name = "DeviceDetail";
            this.Text = "设备详情";
            this.Load += new System.EventHandler(this.DeviceDetail_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.Button Edit;
        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.TextBox Information;

    }
}