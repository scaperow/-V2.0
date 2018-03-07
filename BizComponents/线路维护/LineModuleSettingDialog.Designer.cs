namespace BizComponents
{
    partial class LineModuleSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LineModuleSetting));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeLines = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.treeModule = new System.Windows.Forms.TreeView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.rb_sheet = new System.Windows.Forms.RadioButton();
            this.rb_module = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeLines);
            this.groupBox1.Controls.Add(this.treeModule);
            this.groupBox1.Location = new System.Drawing.Point(12, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(633, 472);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择模板/线路禁止发布线路";
            // 
            // treeLines
            // 
            this.treeLines.CheckBoxes = true;
            this.treeLines.FullRowSelect = true;
            this.treeLines.HideSelection = false;
            this.treeLines.ImageIndex = 2;
            this.treeLines.ImageList = this.imageList1;
            this.treeLines.Location = new System.Drawing.Point(395, 17);
            this.treeLines.Name = "treeLines";
            this.treeLines.SelectedImageIndex = 2;
            this.treeLines.Size = new System.Drawing.Size(232, 452);
            this.treeLines.TabIndex = 1;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            this.imageList1.Images.SetKeyName(0, "关闭文件夹.bmp");
            this.imageList1.Images.SetKeyName(1, "打开文件夹.bmp");
            this.imageList1.Images.SetKeyName(2, "表单.png");
            // 
            // treeModule
            // 
            this.treeModule.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeModule.FullRowSelect = true;
            this.treeModule.HideSelection = false;
            this.treeModule.ImageIndex = 0;
            this.treeModule.ImageList = this.imageList1;
            this.treeModule.Location = new System.Drawing.Point(3, 17);
            this.treeModule.Name = "treeModule";
            this.treeModule.SelectedImageIndex = 0;
            this.treeModule.Size = new System.Drawing.Size(386, 452);
            this.treeModule.TabIndex = 0;
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
            this.ButtonOk.Location = new System.Drawing.Point(457, 510);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(75, 23);
            this.ButtonOk.TabIndex = 2;
            this.ButtonOk.Text = "保存";
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
            // rb_sheet
            // 
            this.rb_sheet.AutoSize = true;
            this.rb_sheet.Location = new System.Drawing.Point(377, 513);
            this.rb_sheet.Name = "rb_sheet";
            this.rb_sheet.Size = new System.Drawing.Size(47, 16);
            this.rb_sheet.TabIndex = 7;
            this.rb_sheet.TabStop = true;
            this.rb_sheet.Text = "表单";
            this.rb_sheet.UseVisualStyleBackColor = true;
            this.rb_sheet.CheckedChanged += new System.EventHandler(this.rb_sheet_CheckedChanged);
            // 
            // rb_module
            // 
            this.rb_module.AutoSize = true;
            this.rb_module.Location = new System.Drawing.Point(312, 512);
            this.rb_module.Name = "rb_module";
            this.rb_module.Size = new System.Drawing.Size(47, 16);
            this.rb_module.TabIndex = 6;
            this.rb_module.TabStop = true;
            this.rb_module.Text = "模板";
            this.rb_module.UseVisualStyleBackColor = true;
            this.rb_module.CheckedChanged += new System.EventHandler(this.rb_sheet_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 510);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "*单次只能保存一个模板/表单禁止发布的线路";
            // 
            // LineModuleSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 545);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rb_sheet);
            this.Controls.Add(this.rb_module);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LineModuleSetting";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "线路模板设置";
            this.Load += new System.EventHandler(this.ReferenceSheetDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.TreeView treeModule;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TreeView treeLines;
        private System.Windows.Forms.RadioButton rb_sheet;
        private System.Windows.Forms.RadioButton rb_module;
        private System.Windows.Forms.Label label1;
    }
}