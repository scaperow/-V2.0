namespace ReportComponents
{
    partial class EntireLineReportDialog
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.reportViewer1 = new ReportComponents.ReportViewer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.startdate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.enddate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.Button_Query = new System.Windows.Forms.Button();
            this.cBox_Types = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.collapsibleSplitter1 = new Yqun.Controls.CollapsibleSplitter();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.reportViewer1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(278, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(512, 536);
            this.groupBox2.TabIndex = 3;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.Button_Query);
            this.groupBox1.Controls.Add(this.cBox_Types);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(262, 536);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置报表";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.startdate);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.enddate);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(8, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 48);
            this.panel1.TabIndex = 14;
            // 
            // startdate
            // 
            this.startdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.startdate.Location = new System.Drawing.Point(69, 2);
            this.startdate.Name = "startdate";
            this.startdate.Size = new System.Drawing.Size(169, 21);
            this.startdate.TabIndex = 11;
            this.startdate.Tag = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "开始时间：";
            // 
            // enddate
            // 
            this.enddate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.enddate.Location = new System.Drawing.Point(69, 26);
            this.enddate.Name = "enddate";
            this.enddate.Size = new System.Drawing.Size(169, 21);
            this.enddate.TabIndex = 12;
            this.enddate.Tag = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "结束时间：";
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
            // cBox_Types
            // 
            this.cBox_Types.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cBox_Types.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBox_Types.FormattingEnabled = true;
            this.cBox_Types.Items.AddRange(new object[] {
            "全线资料统计",
            "人员档案统计"});
            this.cBox_Types.Location = new System.Drawing.Point(77, 20);
            this.cBox_Types.Name = "cBox_Types";
            this.cBox_Types.Size = new System.Drawing.Size(171, 20);
            this.cBox_Types.TabIndex = 6;
            this.cBox_Types.SelectedIndexChanged += new System.EventHandler(this.cBox_Types_SelectedIndexChanged);
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
            // collapsibleSplitter1
            // 
            this.collapsibleSplitter1.AnimationDelay = 20;
            this.collapsibleSplitter1.AnimationStep = 20;
            this.collapsibleSplitter1.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
            this.collapsibleSplitter1.ControlToHide = this.groupBox1;
            this.collapsibleSplitter1.ExpandParentForm = false;
            this.collapsibleSplitter1.Location = new System.Drawing.Point(270, 8);
            this.collapsibleSplitter1.MinSize = 12;
            this.collapsibleSplitter1.Name = "collapsibleSplitter1";
            this.collapsibleSplitter1.TabIndex = 4;
            this.collapsibleSplitter1.TabStop = false;
            this.collapsibleSplitter1.UseAnimations = false;
            this.collapsibleSplitter1.VisualStyle = Yqun.Controls.VisualStyles.Win9x;
            // 
            // EntireLineReportDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 552);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.collapsibleSplitter1);
            this.Controls.Add(this.groupBox1);
            this.Name = "EntireLineReportDialog";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "资料统计、人员统计、设备统计";
            this.Load += new System.EventHandler(this.EntireLineReportDialog_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private ReportViewer reportViewer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Button_Query;
        private System.Windows.Forms.DateTimePicker @enddate;
        private System.Windows.Forms.DateTimePicker @startdate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cBox_Types;
        private System.Windows.Forms.Label label1;
        private Yqun.Controls.CollapsibleSplitter collapsibleSplitter1;
        private System.Windows.Forms.Panel panel1;
    }
}