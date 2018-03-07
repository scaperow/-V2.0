namespace BizModules
{
    partial class ReferenceSheetDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReferenceSheetDialog));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SheetList = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Controls.Add(this.SheetList);
            this.groupBox1.Location = new System.Drawing.Point(12, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(633, 472);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择下面的表单";
            // 
            // SheetList
            // 
            this.SheetList.CheckBoxes = true;
            this.SheetList.Dock = System.Windows.Forms.DockStyle.Left;
            this.SheetList.FullRowSelect = true;
            this.SheetList.HideSelection = false;
            this.SheetList.ImageIndex = 0;
            this.SheetList.ImageList = this.imageList1;
            this.SheetList.Location = new System.Drawing.Point(3, 17);
            this.SheetList.Name = "SheetList";
            this.SheetList.SelectedImageIndex = 0;
            this.SheetList.Size = new System.Drawing.Size(338, 452);
            this.SheetList.TabIndex = 0;
            this.SheetList.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.SheetList_AfterCheck);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            this.imageList1.Images.SetKeyName(0, "关闭文件夹.bmp");
            this.imageList1.Images.SetKeyName(1, "打开文件夹.bmp");
            this.imageList1.Images.SetKeyName(2, "表单.png");
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(12, 487);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(631, 8);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // ButtonOk
            // 
            this.ButtonOk.Location = new System.Drawing.Point(434, 510);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(75, 23);
            this.ButtonOk.TabIndex = 2;
            this.ButtonOk.Text = "确定";
            this.ButtonOk.UseVisualStyleBackColor = true;
            this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(538, 510);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 3;
            this.ButtonCancel.Text = "取消";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // treeView1
            // 
            this.treeView1.FullRowSelect = true;
            this.treeView1.HideSelection = false;
            this.treeView1.ImageIndex = 2;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(347, 17);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 2;
            this.treeView1.Size = new System.Drawing.Size(257, 452);
            this.treeView1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(607, 96);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(21, 135);
            this.button1.TabIndex = 3;
            this.button1.Text = "∧∧";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(608, 247);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(21, 135);
            this.button2.TabIndex = 4;
            this.button2.Tag = "";
            this.button2.Text = "∨∨";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ReferenceSheetDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 545);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReferenceSheetDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "引用现有表单";
            this.Load += new System.EventHandler(this.ReferenceSheetDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.TreeView SheetList;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TreeView treeView1;
    }
}