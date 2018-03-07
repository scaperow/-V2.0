namespace BizComponents
{
    partial class TestProjectDialog
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
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.listBox_TestItems = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(126, 474);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 11;
            this.ButtonCancel.Text = "取消";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // ButtonOk
            // 
            this.ButtonOk.Location = new System.Drawing.Point(15, 474);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(75, 23);
            this.ButtonOk.TabIndex = 9;
            this.ButtonOk.Text = "确定";
            this.ButtonOk.UseVisualStyleBackColor = true;
            this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // listBox_TestItems
            // 
            this.listBox_TestItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_TestItems.FormattingEnabled = true;
            this.listBox_TestItems.ItemHeight = 12;
            this.listBox_TestItems.Location = new System.Drawing.Point(3, 17);
            this.listBox_TestItems.Name = "listBox_TestItems";
            this.listBox_TestItems.Size = new System.Drawing.Size(186, 436);
            this.listBox_TestItems.TabIndex = 0;
            this.listBox_TestItems.SelectedIndexChanged += new System.EventHandler(this.listBox_TestItems_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBox_TestItems);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(192, 456);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "插入试验项目";
            // 
            // TestProjectDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 516);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.groupBox1);
            this.Name = "TestProjectDialog";
            this.Text = "编辑试验项目";
            this.Load += new System.EventHandler(this.TestProjectDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.ListBox listBox_TestItems;
        private System.Windows.Forms.GroupBox groupBox1;

    }
}