namespace BizComponents
{
    partial class QuerySponsorModifyDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuerySponsorModifyDialog));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_user = new System.Windows.Forms.TextBox();
            this.tb_content = new System.Windows.Forms.TextBox();
            this.cb_state = new System.Windows.Forms.ComboBox();
            this.ComboBox_TestRooms = new System.Windows.Forms.ComboBox();
            this.ComboBox_Company = new System.Windows.Forms.ComboBox();
            this.ComboBox_Segments = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Button_Query = new System.Windows.Forms.Button();
            this.EndDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.StartDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ProImageList = new System.Windows.Forms.ImageList(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread1_Sheet = new FarPoint.Win.Spread.SheetView();
            this.collapsibleSplitter1 = new Yqun.Controls.CollapsibleSplitter();
            this.Button_Export = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Button_Export);
            this.groupBox1.Controls.Add(this.tb_user);
            this.groupBox1.Controls.Add(this.tb_content);
            this.groupBox1.Controls.Add(this.cb_state);
            this.groupBox1.Controls.Add(this.ComboBox_TestRooms);
            this.groupBox1.Controls.Add(this.ComboBox_Company);
            this.groupBox1.Controls.Add(this.ComboBox_Segments);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.Button_Query);
            this.groupBox1.Controls.Add(this.EndDateTimePicker);
            this.groupBox1.Controls.Add(this.StartDateTimePicker);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(248, 558);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // tb_user
            // 
            this.tb_user.Location = new System.Drawing.Point(90, 170);
            this.tb_user.Name = "tb_user";
            this.tb_user.Size = new System.Drawing.Size(150, 21);
            this.tb_user.TabIndex = 35;
            // 
            // tb_content
            // 
            this.tb_content.Location = new System.Drawing.Point(89, 138);
            this.tb_content.Name = "tb_content";
            this.tb_content.Size = new System.Drawing.Size(150, 21);
            this.tb_content.TabIndex = 35;
            // 
            // cb_state
            // 
            this.cb_state.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_state.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_state.FormattingEnabled = true;
            this.cb_state.Items.AddRange(new object[] {
            "全部",
            "已提交",
            "通过",
            "不通过"});
            this.cb_state.Location = new System.Drawing.Point(89, 107);
            this.cb_state.Name = "cb_state";
            this.cb_state.Size = new System.Drawing.Size(150, 20);
            this.cb_state.TabIndex = 34;
            this.cb_state.SelectedIndexChanged += new System.EventHandler(this.ComboBox_State_SelectedIndexChanged);
            // 
            // ComboBox_TestRooms
            // 
            this.ComboBox_TestRooms.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBox_TestRooms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_TestRooms.FormattingEnabled = true;
            this.ComboBox_TestRooms.Location = new System.Drawing.Point(90, 76);
            this.ComboBox_TestRooms.Name = "ComboBox_TestRooms";
            this.ComboBox_TestRooms.Size = new System.Drawing.Size(150, 20);
            this.ComboBox_TestRooms.TabIndex = 34;
            // 
            // ComboBox_Company
            // 
            this.ComboBox_Company.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBox_Company.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Company.FormattingEnabled = true;
            this.ComboBox_Company.Location = new System.Drawing.Point(90, 45);
            this.ComboBox_Company.Name = "ComboBox_Company";
            this.ComboBox_Company.Size = new System.Drawing.Size(150, 20);
            this.ComboBox_Company.TabIndex = 33;
            this.ComboBox_Company.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Company_SelectedIndexChanged);
            // 
            // ComboBox_Segments
            // 
            this.ComboBox_Segments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBox_Segments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Segments.FormattingEnabled = true;
            this.ComboBox_Segments.Location = new System.Drawing.Point(90, 14);
            this.ComboBox_Segments.Name = "ComboBox_Segments";
            this.ComboBox_Segments.Size = new System.Drawing.Size(150, 20);
            this.ComboBox_Segments.TabIndex = 32;
            this.ComboBox_Segments.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Segments_SelectedIndexChanged);
            this.ComboBox_Segments.DropDown += new System.EventHandler(this.ComboBox_Segments_DropDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(10, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 31;
            this.label5.Text = "试验室(&L):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(10, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 30;
            this.label2.Text = "选择单位(&C):";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(10, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 29;
            this.label6.Text = "选择标段(&B):";
            // 
            // Button_Query
            // 
            this.Button_Query.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Query.Location = new System.Drawing.Point(134, 276);
            this.Button_Query.Name = "Button_Query";
            this.Button_Query.Size = new System.Drawing.Size(105, 27);
            this.Button_Query.TabIndex = 20;
            this.Button_Query.Text = "查询";
            this.Button_Query.UseVisualStyleBackColor = true;
            this.Button_Query.Click += new System.EventHandler(this.Button_Query_Click);
            // 
            // EndDateTimePicker
            // 
            this.EndDateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.EndDateTimePicker.Location = new System.Drawing.Point(88, 234);
            this.EndDateTimePicker.Name = "EndDateTimePicker";
            this.EndDateTimePicker.Size = new System.Drawing.Size(151, 21);
            this.EndDateTimePicker.TabIndex = 16;
            this.EndDateTimePicker.Tag = "";
            // 
            // StartDateTimePicker
            // 
            this.StartDateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.StartDateTimePicker.Location = new System.Drawing.Point(88, 202);
            this.StartDateTimePicker.Name = "StartDateTimePicker";
            this.StartDateTimePicker.Size = new System.Drawing.Size(151, 21);
            this.StartDateTimePicker.TabIndex = 15;
            this.StartDateTimePicker.Tag = "";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 173);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "申请人(&U):";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 141);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "申请内容(&D):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "审批状态(&T):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 240);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "结束时间(&E):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "开始时间(&S):";
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.fpSpread1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(264, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(737, 558);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "资料修改";
            // 
            // fpSpread1
            // 
            this.fpSpread1.AccessibleDescription = "";
            this.fpSpread1.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.Location = new System.Drawing.Point(3, 17);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet});
            this.fpSpread1.Size = new System.Drawing.Size(731, 538);
            this.fpSpread1.TabIndex = 1;
            this.fpSpread1.TabStripInsertTab = false;
            this.fpSpread1.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread1_CellDoubleClick);
            // 
            // fpSpread1_Sheet
            // 
            this.fpSpread1_Sheet.Reset();
            this.fpSpread1_Sheet.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet.ColumnCount = 5;
            this.fpSpread1_Sheet.RowCount = 0;
            this.fpSpread1_Sheet.ColumnHeader.AutoText = FarPoint.Win.Spread.HeaderAutoText.Blank;
            this.fpSpread1_Sheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread1.SetActiveViewport(0, 1, 0);
            // 
            // collapsibleSplitter1
            // 
            this.collapsibleSplitter1.AnimationDelay = 20;
            this.collapsibleSplitter1.AnimationStep = 20;
            this.collapsibleSplitter1.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
            this.collapsibleSplitter1.ControlToHide = this.groupBox1;
            this.collapsibleSplitter1.ExpandParentForm = false;
            this.collapsibleSplitter1.Location = new System.Drawing.Point(256, 8);
            this.collapsibleSplitter1.Name = "collapsibleSplitter1";
            this.collapsibleSplitter1.TabIndex = 2;
            this.collapsibleSplitter1.TabStop = false;
            this.collapsibleSplitter1.UseAnimations = false;
            this.collapsibleSplitter1.VisualStyle = Yqun.Controls.VisualStyles.DoubleDots;
            // 
            // Button_Export
            // 
            this.Button_Export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Export.Location = new System.Drawing.Point(11, 276);
            this.Button_Export.Name = "Button_Export";
            this.Button_Export.Size = new System.Drawing.Size(105, 27);
            this.Button_Export.TabIndex = 36;
            this.Button_Export.Text = "导出到xls";
            this.Button_Export.UseVisualStyleBackColor = true;
            this.Button_Export.Click += new System.EventHandler(this.Button_Export_Click);
            // 
            // QuerySponsorModifyDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 574);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.collapsibleSplitter1);
            this.Controls.Add(this.groupBox1);
            this.Name = "QuerySponsorModifyDialog";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查询资料修改";
            this.Load += new System.EventHandler(this.QuerySponsorModifyDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker EndDateTimePicker;
        private System.Windows.Forms.DateTimePicker StartDateTimePicker;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Button_Query;
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet;
        private System.Windows.Forms.ImageList ProImageList;
        private Yqun.Controls.CollapsibleSplitter collapsibleSplitter1;
        private System.Windows.Forms.ComboBox ComboBox_TestRooms;
        private System.Windows.Forms.ComboBox ComboBox_Company;
        private System.Windows.Forms.ComboBox ComboBox_Segments;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cb_state;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_user;
        private System.Windows.Forms.TextBox tb_content;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button Button_Export;
    }
}