namespace ReportComponents
{
    partial class ParameterDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParameterDialog));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeView_Parameters = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.AddParameterButton = new System.Windows.Forms.ToolStripButton();
            this.RemoveParameterButton = new System.Windows.Forms.ToolStripButton();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_basic = new System.Windows.Forms.TabPage();
            this.textBox_displayname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.Button_Ok = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage_basic.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeView_Parameters);
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Location = new System.Drawing.Point(7, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.groupBox1.Size = new System.Drawing.Size(228, 401);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "参数列表";
            // 
            // treeView_Parameters
            // 
            this.treeView_Parameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_Parameters.FullRowSelect = true;
            this.treeView_Parameters.HideSelection = false;
            this.treeView_Parameters.Location = new System.Drawing.Point(3, 39);
            this.treeView_Parameters.Name = "treeView_Parameters";
            this.treeView_Parameters.ShowLines = false;
            this.treeView_Parameters.ShowNodeToolTips = true;
            this.treeView_Parameters.ShowPlusMinus = false;
            this.treeView_Parameters.ShowRootLines = false;
            this.treeView_Parameters.Size = new System.Drawing.Size(222, 359);
            this.treeView_Parameters.TabIndex = 1;
            this.treeView_Parameters.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_Parameters_AfterSelect);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddParameterButton,
            this.RemoveParameterButton});
            this.toolStrip1.Location = new System.Drawing.Point(3, 14);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(222, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // AddParameterButton
            // 
            this.AddParameterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.AddParameterButton.Image = ((System.Drawing.Image)(resources.GetObject("AddParameterButton.Image")));
            this.AddParameterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddParameterButton.Name = "AddParameterButton";
            this.AddParameterButton.Size = new System.Drawing.Size(60, 22);
            this.AddParameterButton.Text = "添加参数";
            this.AddParameterButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // RemoveParameterButton
            // 
            this.RemoveParameterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.RemoveParameterButton.Image = ((System.Drawing.Image)(resources.GetObject("RemoveParameterButton.Image")));
            this.RemoveParameterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RemoveParameterButton.Name = "RemoveParameterButton";
            this.RemoveParameterButton.Size = new System.Drawing.Size(60, 22);
            this.RemoveParameterButton.Text = "删除参数";
            this.RemoveParameterButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Location = new System.Drawing.Point(498, 413);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 2;
            this.Button_Cancel.Text = "取消";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_basic);
            this.tabControl1.Location = new System.Drawing.Point(243, 14);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(365, 391);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage_basic
            // 
            this.tabPage_basic.Controls.Add(this.textBox_displayname);
            this.tabPage_basic.Controls.Add(this.label1);
            this.tabPage_basic.Controls.Add(this.label2);
            this.tabPage_basic.Controls.Add(this.textBox_name);
            this.tabPage_basic.Location = new System.Drawing.Point(4, 22);
            this.tabPage_basic.Name = "tabPage_basic";
            this.tabPage_basic.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_basic.Size = new System.Drawing.Size(357, 365);
            this.tabPage_basic.TabIndex = 0;
            this.tabPage_basic.Text = "基本属性";
            this.tabPage_basic.UseVisualStyleBackColor = true;
            // 
            // textBox_displayname
            // 
            this.textBox_displayname.Location = new System.Drawing.Point(55, 42);
            this.textBox_displayname.Name = "textBox_displayname";
            this.textBox_displayname.Size = new System.Drawing.Size(287, 21);
            this.textBox_displayname.TabIndex = 7;
            this.textBox_displayname.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "描述：";
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(55, 15);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(287, 21);
            this.textBox_name.TabIndex = 6;
            this.textBox_name.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // Button_Ok
            // 
            this.Button_Ok.Location = new System.Drawing.Point(416, 413);
            this.Button_Ok.Name = "Button_Ok";
            this.Button_Ok.Size = new System.Drawing.Size(75, 23);
            this.Button_Ok.TabIndex = 11;
            this.Button_Ok.Text = "确定";
            this.Button_Ok.UseVisualStyleBackColor = true;
            this.Button_Ok.Click += new System.EventHandler(this.Button_Ok_Click);
            // 
            // ParameterDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 444);
            this.Controls.Add(this.Button_Ok);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ParameterDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "参数编辑器";
            this.Load += new System.EventHandler(this.ParameterDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage_basic.ResumeLayout(false);
            this.tabPage_basic.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.ToolStripButton AddParameterButton;
        private System.Windows.Forms.ToolStripButton RemoveParameterButton;
        private System.Windows.Forms.TreeView treeView_Parameters;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_basic;
        private System.Windows.Forms.TextBox textBox_displayname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.Button Button_Ok;
    }
}