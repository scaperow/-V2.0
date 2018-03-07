namespace Kingrocket.CJ.Components
{
    partial class MainForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.SysMenuStrip = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItemSystemSet = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemStadium = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStartTest = new System.Windows.Forms.Button();
            this.btnEndTest = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.DResultView = new FarPoint.Win.Spread.SheetView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtLZ = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.bt_check = new System.Windows.Forms.Button();
            this.cb_jz = new System.Windows.Forms.CheckBox();
            this.rb_xqf = new System.Windows.Forms.RadioButton();
            this.rb_sqf = new System.Windows.Forms.RadioButton();
            this.label_DelCode = new System.Windows.Forms.Label();
            this.label_TestName = new System.Windows.Forms.Label();
            this.label_SJCC = new System.Windows.Forms.Label();
            this.label_SJJB = new System.Windows.Forms.Label();
            this.label_Count = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_DelCode = new System.Windows.Forms.TextBox();
            this.textBox_CurrentNumber = new System.Windows.Forms.TextBox();
            this.comboBox_SJCC = new System.Windows.Forms.ComboBox();
            this.comboBox_SJJB = new System.Windows.Forms.ComboBox();
            this.comboBox_Name = new System.Windows.Forms.ComboBox();
            this.comboBox_Count = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.Panel();
            this.ChartLineControl = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTitleLZ = new System.Windows.Forms.Label();
            this.lblLZ = new System.Windows.Forms.Label();
            this.lblTitleTime = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.timer_UploadLocalData = new System.Windows.Forms.Timer(this.components);
            this.SysMenuStrip.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DResultView)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChartLineControl)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SysMenuStrip
            // 
            this.SysMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemSystemSet,
            this.ToolStripMenuItemStadium,
            this.ToolStripMenuItemAbout});
            this.SysMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.SysMenuStrip.Name = "SysMenuStrip";
            this.SysMenuStrip.Size = new System.Drawing.Size(1017, 25);
            this.SysMenuStrip.TabIndex = 1;
            this.SysMenuStrip.Text = "menuStrip1";
            // 
            // ToolStripMenuItemSystemSet
            // 
            this.ToolStripMenuItemSystemSet.Name = "ToolStripMenuItemSystemSet";
            this.ToolStripMenuItemSystemSet.Size = new System.Drawing.Size(68, 21);
            this.ToolStripMenuItemSystemSet.Text = "系统配置";
            this.ToolStripMenuItemSystemSet.Click += new System.EventHandler(this.ToolStripMenuItemSystemSet_Click);
            // 
            // ToolStripMenuItemStadium
            // 
            this.ToolStripMenuItemStadium.Name = "ToolStripMenuItemStadium";
            this.ToolStripMenuItemStadium.Size = new System.Drawing.Size(68, 21);
            this.ToolStripMenuItemStadium.Text = "选择试验";
            this.ToolStripMenuItemStadium.Click += new System.EventHandler(this.ToolStripMenuItemStadium_Click);
            // 
            // ToolStripMenuItemAbout
            // 
            this.ToolStripMenuItemAbout.Name = "ToolStripMenuItemAbout";
            this.ToolStripMenuItemAbout.Size = new System.Drawing.Size(44, 21);
            this.ToolStripMenuItemAbout.Text = "关于";
            this.ToolStripMenuItemAbout.Click += new System.EventHandler(this.ToolStripMenuItemAbout_Click);
            // 
            // btnStartTest
            // 
            this.btnStartTest.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnStartTest.Location = new System.Drawing.Point(670, 497);
            this.btnStartTest.Name = "btnStartTest";
            this.btnStartTest.Size = new System.Drawing.Size(111, 30);
            this.btnStartTest.TabIndex = 14;
            this.btnStartTest.Text = "开始试验";
            this.btnStartTest.UseVisualStyleBackColor = true;
            this.btnStartTest.Click += new System.EventHandler(this.btnStartTest_Click);
            // 
            // btnEndTest
            // 
            this.btnEndTest.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEndTest.Location = new System.Drawing.Point(813, 496);
            this.btnEndTest.Name = "btnEndTest";
            this.btnEndTest.Size = new System.Drawing.Size(111, 30);
            this.btnEndTest.TabIndex = 13;
            this.btnEndTest.Text = "结束试验";
            this.btnEndTest.UseVisualStyleBackColor = true;
            this.btnEndTest.Click += new System.EventHandler(this.btnEndTest_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.fpSpread1);
            this.groupBox5.Location = new System.Drawing.Point(600, 306);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(405, 185);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "试验结果";
            // 
            // fpSpread1
            // 
            this.fpSpread1.AccessibleDescription = "";
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.Location = new System.Drawing.Point(3, 17);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.DResultView});
            this.fpSpread1.Size = new System.Drawing.Size(399, 165);
            this.fpSpread1.TabIndex = 5;
            // 
            // DResultView
            // 
            this.DResultView.Reset();
            this.DResultView.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.DResultView.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.DResultView.ColumnCount = 5;
            this.DResultView.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            this.DResultView.RowHeader.Visible = false;
            this.DResultView.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtLZ);
            this.groupBox4.Location = new System.Drawing.Point(600, 218);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(405, 85);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "实时数据";
            // 
            // txtLZ
            // 
            this.txtLZ.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLZ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLZ.Location = new System.Drawing.Point(3, 17);
            this.txtLZ.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.txtLZ.Multiline = true;
            this.txtLZ.Name = "txtLZ";
            this.txtLZ.ReadOnly = true;
            this.txtLZ.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLZ.Size = new System.Drawing.Size(399, 65);
            this.txtLZ.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.bt_check);
            this.groupBox3.Controls.Add(this.cb_jz);
            this.groupBox3.Controls.Add(this.rb_xqf);
            this.groupBox3.Controls.Add(this.rb_sqf);
            this.groupBox3.Controls.Add(this.label_DelCode);
            this.groupBox3.Controls.Add(this.label_TestName);
            this.groupBox3.Controls.Add(this.label_SJCC);
            this.groupBox3.Controls.Add(this.label_SJJB);
            this.groupBox3.Controls.Add(this.label_Count);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.textBox_DelCode);
            this.groupBox3.Controls.Add(this.textBox_CurrentNumber);
            this.groupBox3.Controls.Add(this.comboBox_SJCC);
            this.groupBox3.Controls.Add(this.comboBox_SJJB);
            this.groupBox3.Controls.Add(this.comboBox_Name);
            this.groupBox3.Controls.Add(this.comboBox_Count);
            this.groupBox3.Location = new System.Drawing.Point(600, 25);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(405, 190);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "试验信息";
            // 
            // bt_check
            // 
            this.bt_check.Location = new System.Drawing.Point(346, 19);
            this.bt_check.Name = "bt_check";
            this.bt_check.Size = new System.Drawing.Size(42, 23);
            this.bt_check.TabIndex = 20;
            this.bt_check.Text = "√";
            this.bt_check.UseVisualStyleBackColor = true;
            this.bt_check.Click += new System.EventHandler(this.bt_check_Click);
            // 
            // cb_jz
            // 
            this.cb_jz.AutoSize = true;
            this.cb_jz.Location = new System.Drawing.Point(337, 163);
            this.cb_jz.Name = "cb_jz";
            this.cb_jz.Size = new System.Drawing.Size(48, 16);
            this.cb_jz.TabIndex = 19;
            this.cb_jz.Text = "见证";
            this.cb_jz.UseVisualStyleBackColor = true;
            // 
            // rb_xqf
            // 
            this.rb_xqf.AutoSize = true;
            this.rb_xqf.Location = new System.Drawing.Point(248, 163);
            this.rb_xqf.Name = "rb_xqf";
            this.rb_xqf.Size = new System.Drawing.Size(59, 16);
            this.rb_xqf.TabIndex = 18;
            this.rb_xqf.TabStop = true;
            this.rb_xqf.Text = "下屈服";
            this.rb_xqf.UseVisualStyleBackColor = true;
            this.rb_xqf.CheckedChanged += new System.EventHandler(this.rb_sqf_CheckedChanged);
            // 
            // rb_sqf
            // 
            this.rb_sqf.AutoSize = true;
            this.rb_sqf.Location = new System.Drawing.Point(159, 163);
            this.rb_sqf.Name = "rb_sqf";
            this.rb_sqf.Size = new System.Drawing.Size(59, 16);
            this.rb_sqf.TabIndex = 18;
            this.rb_sqf.TabStop = true;
            this.rb_sqf.Text = "上屈服";
            this.rb_sqf.UseVisualStyleBackColor = true;
            this.rb_sqf.CheckedChanged += new System.EventHandler(this.rb_sqf_CheckedChanged);
            // 
            // label_DelCode
            // 
            this.label_DelCode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_DelCode.Location = new System.Drawing.Point(18, 24);
            this.label_DelCode.Name = "label_DelCode";
            this.label_DelCode.Size = new System.Drawing.Size(60, 12);
            this.label_DelCode.TabIndex = 9;
            this.label_DelCode.Text = "委托编号";
            // 
            // label_TestName
            // 
            this.label_TestName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_TestName.Location = new System.Drawing.Point(18, 52);
            this.label_TestName.Name = "label_TestName";
            this.label_TestName.Size = new System.Drawing.Size(60, 12);
            this.label_TestName.TabIndex = 10;
            this.label_TestName.Text = "试验项目";
            // 
            // label_SJCC
            // 
            this.label_SJCC.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_SJCC.Location = new System.Drawing.Point(18, 80);
            this.label_SJCC.Name = "label_SJCC";
            this.label_SJCC.Size = new System.Drawing.Size(60, 12);
            this.label_SJCC.TabIndex = 11;
            this.label_SJCC.Text = "试件尺寸";
            // 
            // label_SJJB
            // 
            this.label_SJJB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_SJJB.Location = new System.Drawing.Point(18, 108);
            this.label_SJJB.Name = "label_SJJB";
            this.label_SJJB.Size = new System.Drawing.Size(60, 12);
            this.label_SJJB.TabIndex = 6;
            this.label_SJJB.Text = "试件级别";
            // 
            // label_Count
            // 
            this.label_Count.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_Count.Location = new System.Drawing.Point(18, 136);
            this.label_Count.Name = "label_Count";
            this.label_Count.Size = new System.Drawing.Size(60, 12);
            this.label_Count.TabIndex = 7;
            this.label_Count.Text = "试件总数";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.Location = new System.Drawing.Point(18, 164);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "试件序号";
            // 
            // textBox_DelCode
            // 
            this.textBox_DelCode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_DelCode.Location = new System.Drawing.Point(83, 19);
            this.textBox_DelCode.Name = "textBox_DelCode";
            this.textBox_DelCode.Size = new System.Drawing.Size(257, 21);
            this.textBox_DelCode.TabIndex = 13;
            // 
            // textBox_CurrentNumber
            // 
            this.textBox_CurrentNumber.AcceptsTab = true;
            this.textBox_CurrentNumber.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_CurrentNumber.Location = new System.Drawing.Point(83, 161);
            this.textBox_CurrentNumber.Name = "textBox_CurrentNumber";
            this.textBox_CurrentNumber.Size = new System.Drawing.Size(46, 21);
            this.textBox_CurrentNumber.TabIndex = 12;
            this.textBox_CurrentNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_CurrentNumber_KeyPress);
            // 
            // comboBox_SJCC
            // 
            this.comboBox_SJCC.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBox_SJCC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_SJCC.FormattingEnabled = true;
            this.comboBox_SJCC.Location = new System.Drawing.Point(83, 76);
            this.comboBox_SJCC.Name = "comboBox_SJCC";
            this.comboBox_SJCC.Size = new System.Drawing.Size(305, 20);
            this.comboBox_SJCC.TabIndex = 14;
            // 
            // comboBox_SJJB
            // 
            this.comboBox_SJJB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBox_SJJB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_SJJB.FormattingEnabled = true;
            this.comboBox_SJJB.Location = new System.Drawing.Point(83, 104);
            this.comboBox_SJJB.Name = "comboBox_SJJB";
            this.comboBox_SJJB.Size = new System.Drawing.Size(305, 20);
            this.comboBox_SJJB.TabIndex = 15;
            // 
            // comboBox_Name
            // 
            this.comboBox_Name.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBox_Name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Name.FormattingEnabled = true;
            this.comboBox_Name.Location = new System.Drawing.Point(83, 48);
            this.comboBox_Name.Name = "comboBox_Name";
            this.comboBox_Name.Size = new System.Drawing.Size(305, 20);
            this.comboBox_Name.TabIndex = 16;
            this.comboBox_Name.SelectedIndexChanged += new System.EventHandler(this.comboBox_Name_SelectedIndexChanged);
            // 
            // comboBox_Count
            // 
            this.comboBox_Count.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBox_Count.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Count.FormattingEnabled = true;
            this.comboBox_Count.Location = new System.Drawing.Point(83, 132);
            this.comboBox_Count.Name = "comboBox_Count";
            this.comboBox_Count.Size = new System.Drawing.Size(305, 20);
            this.comboBox_Count.TabIndex = 17;
            this.comboBox_Count.SelectedIndexChanged += new System.EventHandler(this.comboBox_Count_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.groupBox2.Controls.Add(this.ChartLineControl);
            this.groupBox2.Location = new System.Drawing.Point(12, 150);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(571, 377);
            this.groupBox2.TabIndex = 9;
            // 
            // ChartLineControl
            // 
            chartArea1.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.Name = "ChartArea1";
            this.ChartLineControl.ChartAreas.Add(chartArea1);
            this.ChartLineControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChartLineControl.Location = new System.Drawing.Point(0, 0);
            this.ChartLineControl.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.ChartLineControl.Name = "ChartLineControl";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.MarkerSize = 1;
            series1.MarkerStep = 10;
            series1.Name = "Series1";
            this.ChartLineControl.Series.Add(series1);
            this.ChartLineControl.Size = new System.Drawing.Size(569, 375);
            this.ChartLineControl.TabIndex = 0;
            title1.Name = "Title1";
            title1.Text = "曲线图";
            this.ChartLineControl.Titles.Add(title1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTitleLZ);
            this.groupBox1.Controls.Add(this.lblLZ);
            this.groupBox1.Controls.Add(this.lblTitleTime);
            this.groupBox1.Controls.Add(this.lblTime);
            this.groupBox1.Location = new System.Drawing.Point(12, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(571, 117);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // lblTitleLZ
            // 
            this.lblTitleLZ.BackColor = System.Drawing.Color.Black;
            this.lblTitleLZ.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitleLZ.ForeColor = System.Drawing.Color.Yellow;
            this.lblTitleLZ.Location = new System.Drawing.Point(6, 19);
            this.lblTitleLZ.Name = "lblTitleLZ";
            this.lblTitleLZ.Size = new System.Drawing.Size(132, 86);
            this.lblTitleLZ.TabIndex = 4;
            this.lblTitleLZ.Text = "力值";
            this.lblTitleLZ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLZ
            // 
            this.lblLZ.BackColor = System.Drawing.Color.Black;
            this.lblLZ.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLZ.ForeColor = System.Drawing.Color.Yellow;
            this.lblLZ.Location = new System.Drawing.Point(139, 19);
            this.lblLZ.Name = "lblLZ";
            this.lblLZ.Size = new System.Drawing.Size(132, 86);
            this.lblLZ.TabIndex = 5;
            this.lblLZ.Text = "000.00";
            this.lblLZ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleTime
            // 
            this.lblTitleTime.BackColor = System.Drawing.Color.Black;
            this.lblTitleTime.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitleTime.ForeColor = System.Drawing.Color.Yellow;
            this.lblTitleTime.Location = new System.Drawing.Point(299, 19);
            this.lblTitleTime.Name = "lblTitleTime";
            this.lblTitleTime.Size = new System.Drawing.Size(132, 86);
            this.lblTitleTime.TabIndex = 6;
            this.lblTitleTime.Text = "时间";
            this.lblTitleTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTime
            // 
            this.lblTime.BackColor = System.Drawing.Color.Black;
            this.lblTime.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTime.ForeColor = System.Drawing.Color.Yellow;
            this.lblTime.Location = new System.Drawing.Point(432, 19);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(132, 86);
            this.lblTime.TabIndex = 7;
            this.lblTime.Text = "000.00";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer_UploadLocalData
            // 
            this.timer_UploadLocalData.Interval = 30000;
            this.timer_UploadLocalData.Tick += new System.EventHandler(this.timer_UploadLocalData_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 552);
            this.Controls.Add(this.btnStartTest);
            this.Controls.Add(this.btnEndTest);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.SysMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.SysMenuStrip;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "铁路试验实时采集系统";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CaiJiForm_FormClosing);
            this.SysMenuStrip.ResumeLayout(false);
            this.SysMenuStrip.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DResultView)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChartLineControl)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip SysMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSystemSet;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemStadium;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAbout;
        private System.Windows.Forms.Button btnStartTest;
        private System.Windows.Forms.Button btnEndTest;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtLZ;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cb_jz;
        private System.Windows.Forms.RadioButton rb_xqf;
        private System.Windows.Forms.RadioButton rb_sqf;
        private System.Windows.Forms.Label label_DelCode;
        private System.Windows.Forms.Label label_TestName;
        private System.Windows.Forms.Label label_SJCC;
        private System.Windows.Forms.Label label_SJJB;
        private System.Windows.Forms.Label label_Count;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_DelCode;
        private System.Windows.Forms.TextBox textBox_CurrentNumber;
        private System.Windows.Forms.ComboBox comboBox_SJCC;
        private System.Windows.Forms.ComboBox comboBox_SJJB;
        private System.Windows.Forms.ComboBox comboBox_Name;
        private System.Windows.Forms.ComboBox comboBox_Count;
        private System.Windows.Forms.Panel groupBox2;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartLineControl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTitleLZ;
        private System.Windows.Forms.Label lblLZ;
        private System.Windows.Forms.Label lblTitleTime;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button bt_check;
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView DResultView;
        private System.Windows.Forms.Timer timer_UploadLocalData;
        
    }
}