namespace ReportComponents
{
    partial class DataColumnEditor
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tPageCommon = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rButton_None = new System.Windows.Forms.RadioButton();
            this.rButton_LeftToRight = new System.Windows.Forms.RadioButton();
            this.rButton_TopToBottom = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cBoxAggregation = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.cBoxGroup = new System.Windows.Forms.ComboBox();
            this.rButtonList = new System.Windows.Forms.RadioButton();
            this.rButtonAggregation = new System.Windows.Forms.RadioButton();
            this.rButtonGroup = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TopCoordControl = new ReportComponents.CoordControl();
            this.LeftCoordControl = new ReportComponents.CoordControl();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cBoxDatasets = new System.Windows.Forms.ComboBox();
            this.cBoxFields = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tPageFilter = new System.Windows.Forms.TabPage();
            this.filterControl1 = new ReportComponents.FilterControl();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.tPageAdvanced = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.Button_Ok = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tPageCommon.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tPageFilter.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tPageAdvanced.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tPageCommon);
            this.tabControl1.Controls.Add(this.tPageFilter);
            this.tabControl1.Controls.Add(this.tPageAdvanced);
            this.tabControl1.Location = new System.Drawing.Point(8, 9);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(575, 441);
            this.tabControl1.TabIndex = 0;
            // 
            // tPageCommon
            // 
            this.tPageCommon.Controls.Add(this.groupBox4);
            this.tPageCommon.Controls.Add(this.groupBox3);
            this.tPageCommon.Controls.Add(this.groupBox2);
            this.tPageCommon.Controls.Add(this.groupBox1);
            this.tPageCommon.Location = new System.Drawing.Point(4, 21);
            this.tPageCommon.Name = "tPageCommon";
            this.tPageCommon.Padding = new System.Windows.Forms.Padding(3);
            this.tPageCommon.Size = new System.Drawing.Size(567, 416);
            this.tPageCommon.TabIndex = 0;
            this.tPageCommon.Text = "基本";
            this.tPageCommon.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rButton_None);
            this.groupBox4.Controls.Add(this.rButton_LeftToRight);
            this.groupBox4.Controls.Add(this.rButton_TopToBottom);
            this.groupBox4.Location = new System.Drawing.Point(6, 225);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(555, 47);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "扩展方向";
            // 
            // rButton_None
            // 
            this.rButton_None.AutoSize = true;
            this.rButton_None.Location = new System.Drawing.Point(403, 20);
            this.rButton_None.Name = "rButton_None";
            this.rButton_None.Size = new System.Drawing.Size(59, 16);
            this.rButton_None.TabIndex = 6;
            this.rButton_None.TabStop = true;
            this.rButton_None.Text = "不扩展";
            this.rButton_None.UseVisualStyleBackColor = true;
            // 
            // rButton_LeftToRight
            // 
            this.rButton_LeftToRight.AutoSize = true;
            this.rButton_LeftToRight.Location = new System.Drawing.Point(206, 20);
            this.rButton_LeftToRight.Name = "rButton_LeftToRight";
            this.rButton_LeftToRight.Size = new System.Drawing.Size(71, 16);
            this.rButton_LeftToRight.TabIndex = 5;
            this.rButton_LeftToRight.TabStop = true;
            this.rButton_LeftToRight.Text = "从左到右";
            this.rButton_LeftToRight.UseVisualStyleBackColor = true;
            // 
            // rButton_TopToBottom
            // 
            this.rButton_TopToBottom.AutoSize = true;
            this.rButton_TopToBottom.Location = new System.Drawing.Point(9, 20);
            this.rButton_TopToBottom.Name = "rButton_TopToBottom";
            this.rButton_TopToBottom.Size = new System.Drawing.Size(71, 16);
            this.rButton_TopToBottom.TabIndex = 4;
            this.rButton_TopToBottom.TabStop = true;
            this.rButton_TopToBottom.Text = "从上到下";
            this.rButton_TopToBottom.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cBoxAggregation);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.cBoxGroup);
            this.groupBox3.Controls.Add(this.rButtonList);
            this.groupBox3.Controls.Add(this.rButtonAggregation);
            this.groupBox3.Controls.Add(this.rButtonGroup);
            this.groupBox3.Location = new System.Drawing.Point(6, 125);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(555, 94);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "数据设置";
            // 
            // cBoxAggregation
            // 
            this.cBoxAggregation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxAggregation.Enabled = false;
            this.cBoxAggregation.FormattingEnabled = true;
            this.cBoxAggregation.Location = new System.Drawing.Point(286, 65);
            this.cBoxAggregation.Name = "cBoxAggregation";
            this.cBoxAggregation.Size = new System.Drawing.Size(59, 20);
            this.cBoxAggregation.TabIndex = 8;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(309, 17);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(52, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "自定义";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // cBoxGroup
            // 
            this.cBoxGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxGroup.Enabled = false;
            this.cBoxGroup.FormattingEnabled = true;
            this.cBoxGroup.Items.AddRange(new object[] {
            "普通",
            "高级"});
            this.cBoxGroup.Location = new System.Drawing.Point(248, 19);
            this.cBoxGroup.Name = "cBoxGroup";
            this.cBoxGroup.Size = new System.Drawing.Size(59, 20);
            this.cBoxGroup.TabIndex = 3;
            this.cBoxGroup.SelectedIndexChanged += new System.EventHandler(this.cBoxGroup_SelectedIndexChanged);
            // 
            // rButtonList
            // 
            this.rButtonList.AutoSize = true;
            this.rButtonList.Location = new System.Drawing.Point(12, 43);
            this.rButtonList.Name = "rButtonList";
            this.rButtonList.Size = new System.Drawing.Size(323, 16);
            this.rButtonList.TabIndex = 6;
            this.rButtonList.TabStop = true;
            this.rButtonList.Text = "列表(数据列中的所有记录展示出来，无论数据是否重复)";
            this.rButtonList.UseVisualStyleBackColor = true;
            // 
            // rButtonAggregation
            // 
            this.rButtonAggregation.AutoSize = true;
            this.rButtonAggregation.Location = new System.Drawing.Point(12, 66);
            this.rButtonAggregation.Name = "rButtonAggregation";
            this.rButtonAggregation.Size = new System.Drawing.Size(275, 16);
            this.rButtonAggregation.TabIndex = 5;
            this.rButtonAggregation.TabStop = true;
            this.rButtonAggregation.Text = "汇总(包括求和、取平均数、最大值和最小值等)";
            this.rButtonAggregation.UseVisualStyleBackColor = true;
            this.rButtonAggregation.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // rButtonGroup
            // 
            this.rButtonGroup.AutoSize = true;
            this.rButtonGroup.Location = new System.Drawing.Point(12, 20);
            this.rButtonGroup.Name = "rButtonGroup";
            this.rButtonGroup.Size = new System.Drawing.Size(239, 16);
            this.rButtonGroup.TabIndex = 4;
            this.rButtonGroup.TabStop = true;
            this.rButtonGroup.Text = "分组(将数据列中相同内容的值进行合并)";
            this.rButtonGroup.UseVisualStyleBackColor = true;
            this.rButtonGroup.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TopCoordControl);
            this.groupBox2.Controls.Add(this.LeftCoordControl);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(6, 73);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(555, 46);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "父格设置";
            // 
            // TopCoordControl
            // 
            this.TopCoordControl.ForeColor = System.Drawing.Color.DimGray;
            this.TopCoordControl.Location = new System.Drawing.Point(330, 14);
            this.TopCoordControl.Name = "TopCoordControl";
            this.TopCoordControl.Size = new System.Drawing.Size(181, 24);
            this.TopCoordControl.TabIndex = 7;
            this.TopCoordControl.ValueChanged += new System.EventHandler<ReportComponents.ValueChangedEventArgs>(this.CoordControl_ValueChanged);
            // 
            // LeftCoordControl
            // 
            this.LeftCoordControl.ForeColor = System.Drawing.Color.DimGray;
            this.LeftCoordControl.Location = new System.Drawing.Point(59, 14);
            this.LeftCoordControl.Name = "LeftCoordControl";
            this.LeftCoordControl.Size = new System.Drawing.Size(181, 24);
            this.LeftCoordControl.TabIndex = 6;
            this.LeftCoordControl.ValueChanged += new System.EventHandler<ReportComponents.ValueChangedEventArgs>(this.CoordControl_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "左父格:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(280, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "上父格:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cBoxDatasets);
            this.groupBox1.Controls.Add(this.cBoxFields);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(6, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(555, 63);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择数据列";
            // 
            // cBoxDatasets
            // 
            this.cBoxDatasets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxDatasets.FormattingEnabled = true;
            this.cBoxDatasets.Location = new System.Drawing.Point(12, 36);
            this.cBoxDatasets.Name = "cBoxDatasets";
            this.cBoxDatasets.Size = new System.Drawing.Size(260, 20);
            this.cBoxDatasets.TabIndex = 1;
            this.cBoxDatasets.SelectedIndexChanged += new System.EventHandler(this.cBoxDatasets_SelectedIndexChanged);
            // 
            // cBoxFields
            // 
            this.cBoxFields.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxFields.FormattingEnabled = true;
            this.cBoxFields.Location = new System.Drawing.Point(282, 36);
            this.cBoxFields.Name = "cBoxFields";
            this.cBoxFields.Size = new System.Drawing.Size(257, 20);
            this.cBoxFields.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "数据集:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(280, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "数据列:";
            // 
            // tPageFilter
            // 
            this.tPageFilter.Controls.Add(this.filterControl1);
            this.tPageFilter.Controls.Add(this.groupBox5);
            this.tPageFilter.Location = new System.Drawing.Point(4, 21);
            this.tPageFilter.Name = "tPageFilter";
            this.tPageFilter.Padding = new System.Windows.Forms.Padding(3);
            this.tPageFilter.Size = new System.Drawing.Size(567, 416);
            this.tPageFilter.TabIndex = 1;
            this.tPageFilter.Text = "过滤";
            this.tPageFilter.UseVisualStyleBackColor = true;
            // 
            // filterControl1
            // 
            this.filterControl1.Location = new System.Drawing.Point(6, 54);
            this.filterControl1.Name = "filterControl1";
            this.filterControl1.Size = new System.Drawing.Size(555, 301);
            this.filterControl1.TabIndex = 1;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.checkBox1);
            this.groupBox5.Location = new System.Drawing.Point(6, 4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(555, 44);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "父格条件";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(45, 19);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(324, 16);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "将父格子作为过滤条件(适合于父子格来自于同一数据列)";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // tPageAdvanced
            // 
            this.tPageAdvanced.Controls.Add(this.groupBox6);
            this.tPageAdvanced.Location = new System.Drawing.Point(4, 21);
            this.tPageAdvanced.Name = "tPageAdvanced";
            this.tPageAdvanced.Padding = new System.Windows.Forms.Padding(3);
            this.tPageAdvanced.Size = new System.Drawing.Size(567, 416);
            this.tPageAdvanced.TabIndex = 2;
            this.tPageAdvanced.Text = "高级";
            this.tPageAdvanced.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.numericUpDown1);
            this.groupBox6.Controls.Add(this.checkBox2);
            this.groupBox6.Location = new System.Drawing.Point(6, 4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(555, 53);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "补充空白数据";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(93, 19);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(51, 21);
            this.numericUpDown1.TabIndex = 1;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(15, 21);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(72, 16);
            this.checkBox2.TabIndex = 0;
            this.checkBox2.Text = "数据倍数";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // Button_Ok
            // 
            this.Button_Ok.Location = new System.Drawing.Point(412, 462);
            this.Button_Ok.Name = "Button_Ok";
            this.Button_Ok.Size = new System.Drawing.Size(68, 23);
            this.Button_Ok.TabIndex = 1;
            this.Button_Ok.Text = "确定";
            this.Button_Ok.UseVisualStyleBackColor = true;
            this.Button_Ok.Click += new System.EventHandler(this.Button_Ok_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Location = new System.Drawing.Point(494, 462);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(68, 23);
            this.Button_Cancel.TabIndex = 2;
            this.Button_Cancel.Text = "取消";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // DataColumnEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 495);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_Ok);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataColumnEditor";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据列";
            this.Load += new System.EventHandler(this.DataColumnEditor_Load);
            this.tabControl1.ResumeLayout(false);
            this.tPageCommon.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tPageFilter.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tPageAdvanced.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tPageCommon;
        private System.Windows.Forms.TabPage tPageFilter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cBoxDatasets;
        private System.Windows.Forms.ComboBox cBoxFields;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button Button_Ok;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rButtonList;
        private System.Windows.Forms.RadioButton rButtonAggregation;
        private System.Windows.Forms.RadioButton rButtonGroup;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox cBoxGroup;
        private System.Windows.Forms.ComboBox cBoxAggregation;
        private System.Windows.Forms.RadioButton rButton_None;
        private System.Windows.Forms.RadioButton rButton_LeftToRight;
        private System.Windows.Forms.RadioButton rButton_TopToBottom;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox checkBox1;
        private FilterControl filterControl1;
        private CoordControl TopCoordControl;
        private CoordControl LeftCoordControl;
        private System.Windows.Forms.TabPage tPageAdvanced;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}