namespace BizComponents
{
    partial class EquationDialog
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CutObjectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyObjectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteObjectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rTextBox = new BizComponents.RichTextBoxEx();
            this.ClearMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rTextBox);
            this.groupBox1.Location = new System.Drawing.Point(11, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(529, 356);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "公式内容";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(423, 376);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(14, 376);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "清空内容";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CutObjectMenuItem,
            this.CopyObjectMenuItem,
            this.PasteObjectMenuItem,
            this.toolStripMenuItem1,
            this.ClearMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 120);
            // 
            // CutObjectMenuItem
            // 
            this.CutObjectMenuItem.Name = "CutObjectMenuItem";
            this.CutObjectMenuItem.Size = new System.Drawing.Size(152, 22);
            this.CutObjectMenuItem.Text = "剪切";
            this.CutObjectMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // CopyObjectMenuItem
            // 
            this.CopyObjectMenuItem.Name = "CopyObjectMenuItem";
            this.CopyObjectMenuItem.Size = new System.Drawing.Size(152, 22);
            this.CopyObjectMenuItem.Text = "复制";
            this.CopyObjectMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // PasteObjectMenuItem
            // 
            this.PasteObjectMenuItem.Name = "PasteObjectMenuItem";
            this.PasteObjectMenuItem.Size = new System.Drawing.Size(152, 22);
            this.PasteObjectMenuItem.Text = "粘贴";
            this.PasteObjectMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // rTextBox
            // 
            this.rTextBox.ContextMenuStrip = this.contextMenuStrip1;
            this.rTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rTextBox.Location = new System.Drawing.Point(3, 17);
            this.rTextBox.Name = "rTextBox";
            this.rTextBox.Size = new System.Drawing.Size(523, 336);
            this.rTextBox.TabIndex = 0;
            this.rTextBox.Text = "";
            // 
            // ClearMenuItem
            // 
            this.ClearMenuItem.Name = "ClearMenuItem";
            this.ClearMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ClearMenuItem.Text = "清空";
            this.ClearMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // EquationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 416);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EquationDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "插入公式";
            this.groupBox1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private BizComponents.RichTextBoxEx rTextBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem CutObjectMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyObjectMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PasteObjectMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ClearMenuItem;
    }
}