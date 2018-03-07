namespace ReportComponents
{
    partial class TestRoomReportDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestRoomReportDialog));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Button_Query = new System.Windows.Forms.Button();
            this.EndDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.StartDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.ProImageList = new System.Windows.Forms.ImageList(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.cBox_Types = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.reportViewer1 = new ReportComponents.ReportViewer();
            this.collapsibleSplitter1 = new Yqun.Controls.CollapsibleSplitter();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Button_Query);
            this.groupBox1.Controls.Add(this.EndDateTimePicker);
            this.groupBox1.Controls.Add(this.StartDateTimePicker);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cBox_Types);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(262, 536);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置报表";
            // 
            // Button_Query
            // 
            this.Button_Query.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Query.Location = new System.Drawing.Point(131, 481);
            this.Button_Query.Name = "Button_Query";
            this.Button_Query.Size = new System.Drawing.Size(113, 23);
            this.Button_Query.TabIndex = 13;
            this.Button_Query.Text = "查询";
            this.Button_Query.UseVisualStyleBackColor = true;
            this.Button_Query.Click += new System.EventHandler(this.Button_Query_Click);
            // 
            // EndDateTimePicker
            // 
            this.EndDateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.EndDateTimePicker.Location = new System.Drawing.Point(77, 388);
            this.EndDateTimePicker.Name = "EndDateTimePicker";
            this.EndDateTimePicker.Size = new System.Drawing.Size(171, 21);
            this.EndDateTimePicker.TabIndex = 12;
            this.EndDateTimePicker.Tag = "";
            // 
            // StartDateTimePicker
            // 
            this.StartDateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.StartDateTimePicker.Location = new System.Drawing.Point(77, 364);
            this.StartDateTimePicker.Name = "StartDateTimePicker";
            this.StartDateTimePicker.Size = new System.Drawing.Size(171, 21);
            this.StartDateTimePicker.TabIndex = 11;
            this.StartDateTimePicker.Tag = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 392);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "结束时间：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 368);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "开始时间：";
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.CheckBoxes = true;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.ProImageList;
            this.treeView1.Location = new System.Drawing.Point(13, 64);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(235, 291);
            this.treeView1.TabIndex = 8;
            this.treeView1.Tag = "";
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            // 
            // ProImageList
            // 
            this.ProImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ProImageList.ImageStream")));
            this.ProImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ProImageList.Images.SetKeyName(0, "工程列表.png");
            this.ProImageList.Images.SetKeyName(1, "关闭工程.bmp");
            this.ProImageList.Images.SetKeyName(2, "打开工程.bmp");
            this.ProImageList.Images.SetKeyName(3, "单位.png");
            this.ProImageList.Images.SetKeyName(4, "标段.png");
            this.ProImageList.Images.SetKeyName(5, "文件夹.png");
            this.ProImageList.Images.SetKeyName(6, "表单.png");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "统计范围：";
            // 
            // cBox_Types
            // 
            this.cBox_Types.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cBox_Types.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBox_Types.FormattingEnabled = true;
            this.cBox_Types.Items.AddRange(new object[] {
            "周报",
            "月报",
            "试验室资料统计"});
            this.cBox_Types.Location = new System.Drawing.Point(77, 20);
            this.cBox_Types.Name = "cBox_Types";
            this.cBox_Types.Size = new System.Drawing.Size(171, 20);
            this.cBox_Types.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "报表类型：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.reportViewer1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(278, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(512, 536);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "显示报表";
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.Document = null;
            this.reportViewer1.Location = new System.Drawing.Point(3, 17);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ShowPageSettingsButton = true;
            this.reportViewer1.ShowPrinterSettingsBeforePrint = true;
            this.reportViewer1.Size = new System.Drawing.Size(506, 516);
            this.reportViewer1.TabIndex = 1;
            this.reportViewer1.UseAntiAlias = false;
            // 
            // collapsibleSplitter1
            // 
            this.collapsibleSplitter1.AnimationDelay = 20;
            this.collapsibleSplitter1.AnimationStep = 20;
            this.collapsibleSplitter1.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
            this.collapsibleSplitter1.ControlToHide = this.groupBox1;
            this.collapsibleSplitter1.ExpandParentForm = false;
            this.collapsibleSplitter1.Location = new System.Drawing.Point(270, 8);
            this.collapsibleSplitter1.Name = "collapsibleSplitter1";
            this.collapsibleSplitter1.TabIndex = 2;
            this.collapsibleSplitter1.TabStop = false;
            this.collapsibleSplitter1.UseAnimations = false;
            this.collapsibleSplitter1.VisualStyle = Yqun.Controls.VisualStyles.Win9x;
            // 
            // TestRoomReportDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 552);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.collapsibleSplitter1);
            this.Controls.Add(this.groupBox1);
            this.Name = "TestRoomReportDialog";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "周报、月报";
            this.Load += new System.EventHandler(this.PeriodsReportDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private ReportViewer reportViewer1;
        private System.Windows.Forms.ComboBox cBox_Types;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker EndDateTimePicker;
        private System.Windows.Forms.DateTimePicker StartDateTimePicker;
        private System.Windows.Forms.Button Button_Query;
        private System.Windows.Forms.ImageList ProImageList;
        private Yqun.Controls.CollapsibleSplitter collapsibleSplitter1;
    }
}