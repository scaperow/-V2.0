namespace ReportComponents
{
    partial class ColumnOrderDialog
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
            this.Button_Next = new System.Windows.Forms.Button();
            this.Button_Prev = new System.Windows.Forms.Button();
            this.TreeView_Columns = new System.Windows.Forms.TreeView();
            this.Button_Save = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Button_Next
            // 
            this.Button_Next.Location = new System.Drawing.Point(393, 84);
            this.Button_Next.Name = "Button_Next";
            this.Button_Next.Size = new System.Drawing.Size(75, 23);
            this.Button_Next.TabIndex = 2;
            this.Button_Next.Text = "向下";
            this.Button_Next.UseVisualStyleBackColor = true;
            this.Button_Next.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // Button_Prev
            // 
            this.Button_Prev.Location = new System.Drawing.Point(393, 55);
            this.Button_Prev.Name = "Button_Prev";
            this.Button_Prev.Size = new System.Drawing.Size(75, 23);
            this.Button_Prev.TabIndex = 3;
            this.Button_Prev.Text = "向上";
            this.Button_Prev.UseVisualStyleBackColor = true;
            this.Button_Prev.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // TreeView_Columns
            // 
            this.TreeView_Columns.FullRowSelect = true;
            this.TreeView_Columns.HideSelection = false;
            this.TreeView_Columns.Location = new System.Drawing.Point(8, 11);
            this.TreeView_Columns.Name = "TreeView_Columns";
            this.TreeView_Columns.Size = new System.Drawing.Size(378, 340);
            this.TreeView_Columns.TabIndex = 4;
            // 
            // Button_Save
            // 
            this.Button_Save.Location = new System.Drawing.Point(393, 12);
            this.Button_Save.Name = "Button_Save";
            this.Button_Save.Size = new System.Drawing.Size(75, 23);
            this.Button_Save.TabIndex = 5;
            this.Button_Save.Text = "关闭(&C)";
            this.Button_Save.UseVisualStyleBackColor = true;
            this.Button_Save.Click += new System.EventHandler(this.Button_Exit_Click);
            // 
            // ColumnOrderDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 360);
            this.Controls.Add(this.Button_Save);
            this.Controls.Add(this.TreeView_Columns);
            this.Controls.Add(this.Button_Prev);
            this.Controls.Add(this.Button_Next);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColumnOrderDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置列顺序";
            this.Load += new System.EventHandler(this.ColumnOrderDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Button_Next;
        private System.Windows.Forms.Button Button_Prev;
        private System.Windows.Forms.TreeView TreeView_Columns;
        private System.Windows.Forms.Button Button_Save;
    }
}