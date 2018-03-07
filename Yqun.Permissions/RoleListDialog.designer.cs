namespace Yqun.Permissions
{
    partial class RoleListDialog
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
            this.Button_OK = new System.Windows.Forms.Button();
            this.Button_Close = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "请在岗位列表中选择一个岗位：";
            // 
            // Button_OK
            // 
            this.Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Button_OK.Location = new System.Drawing.Point(306, 275);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 2;
            this.Button_OK.Text = "确认";
            this.Button_OK.UseVisualStyleBackColor = true;
            // 
            // Button_Close
            // 
            this.Button_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_Close.Location = new System.Drawing.Point(398, 275);
            this.Button_Close.Name = "Button_Close";
            this.Button_Close.Size = new System.Drawing.Size(75, 23);
            this.Button_Close.TabIndex = 3;
            this.Button_Close.Text = "关闭";
            this.Button_Close.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(12, 32);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(476, 234);
            this.treeView1.TabIndex = 5;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // RoleList
            // 
            this.AcceptButton = this.Button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Button_Close;
            this.ClientSize = new System.Drawing.Size(500, 309);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.Button_Close);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RoleList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "岗位列表";
            this.Load += new System.EventHandler(this.RoleList_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button Button_Close;
        private System.Windows.Forms.TreeView treeView1;
    }
}