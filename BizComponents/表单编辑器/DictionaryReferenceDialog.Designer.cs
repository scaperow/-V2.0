namespace BizComponents
{
    partial class DictionaryReferenceDialog
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
            this.DictionaryView = new System.Windows.Forms.TreeView();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItem_NewFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_ModifyFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_DeleteFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItem_EditItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DictionaryView);
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(518, 416);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字典参照列表";
            // 
            // DictionaryView
            // 
            this.DictionaryView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DictionaryView.HideSelection = false;
            this.DictionaryView.Location = new System.Drawing.Point(3, 17);
            this.DictionaryView.Name = "DictionaryView";
            this.DictionaryView.Size = new System.Drawing.Size(512, 396);
            this.DictionaryView.TabIndex = 4;
            this.DictionaryView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DictionaryView_MouseClick);
            this.DictionaryView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.DictionaryView_AfterLabelEdit);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(413, 432);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 4;
            this.ButtonCancel.Text = "取消";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // ButtonOk
            // 
            this.ButtonOk.Location = new System.Drawing.Point(324, 432);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(75, 23);
            this.ButtonOk.TabIndex = 5;
            this.ButtonOk.Text = "确定";
            this.ButtonOk.UseVisualStyleBackColor = true;
            this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_NewFolder,
            this.MenuItem_ModifyFolder,
            this.MenuItem_DeleteFolder,
            this.toolStripSeparator2,
            this.MenuItem_EditItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 98);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // MenuItem_NewFolder
            // 
            this.MenuItem_NewFolder.Name = "MenuItem_NewFolder";
            this.MenuItem_NewFolder.Size = new System.Drawing.Size(148, 22);
            this.MenuItem_NewFolder.Text = "新建字典分类";
            this.MenuItem_NewFolder.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // MenuItem_ModifyFolder
            // 
            this.MenuItem_ModifyFolder.Name = "MenuItem_ModifyFolder";
            this.MenuItem_ModifyFolder.Size = new System.Drawing.Size(148, 22);
            this.MenuItem_ModifyFolder.Text = "修改字典分类";
            this.MenuItem_ModifyFolder.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // MenuItem_DeleteFolder
            // 
            this.MenuItem_DeleteFolder.Name = "MenuItem_DeleteFolder";
            this.MenuItem_DeleteFolder.Size = new System.Drawing.Size(148, 22);
            this.MenuItem_DeleteFolder.Text = "删除字典分类";
            this.MenuItem_DeleteFolder.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(145, 6);
            // 
            // MenuItem_EditItem
            // 
            this.MenuItem_EditItem.Name = "MenuItem_EditItem";
            this.MenuItem_EditItem.Size = new System.Drawing.Size(148, 22);
            this.MenuItem_EditItem.Text = "编辑子项";
            this.MenuItem_EditItem.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // ButtonSave
            // 
            this.ButtonSave.Location = new System.Drawing.Point(10, 431);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(91, 23);
            this.ButtonSave.TabIndex = 7;
            this.ButtonSave.Text = "保存字典配置";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // DictionaryReferenceDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 466);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DictionaryReferenceDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "字典参照编辑器";
            this.Load += new System.EventHandler(this.ReferenceForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView DictionaryView;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_NewFolder;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_ModifyFolder;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_DeleteFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Button ButtonSave;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_EditItem;
    }
}