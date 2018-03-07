namespace ReportCommon.Chart
{
    partial class ChartEditor
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
            this.chartController = new Steema.TeeChart.ChartController();
            this.editor1 = new Steema.TeeChart.Editor(this.components);
            this.chartListBox1 = new Steema.TeeChart.ChartListBox(this.components);
            this.TeeChartContainer = new System.Windows.Forms.Panel();
            this.rButton_BindData = new System.Windows.Forms.RadioButton();
            this.rButton_BindArea = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Panel_BindData = new System.Windows.Forms.Panel();
            this.ComboBox_Functions = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TextBox_SeriesAxis = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TextBox_CatlogAxis = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Panel_DataArea = new System.Windows.Forms.Panel();
            this.TextBox_SeriesAxis1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TextBox_NameAxis1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TextBox_CatlogAxis1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.Panel_BindData.SuspendLayout();
            this.Panel_DataArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartController
            // 
            this.chartController.ButtonSize = Steema.TeeChart.ControllerButtonSize.x24;
            this.chartController.Editor = this.editor1;
            this.chartController.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.chartController.LabelValues = true;
            this.chartController.Location = new System.Drawing.Point(0, 0);
            this.chartController.Name = "chartController";
            this.chartController.Size = new System.Drawing.Size(631, 31);
            this.chartController.TabIndex = 0;
            this.chartController.Text = "chartController1";
            // 
            // editor1
            // 
            this.editor1.HighLightTabs = false;
            this.editor1.Location = new System.Drawing.Point(0, 0);
            this.editor1.Name = "editor1";
            this.editor1.Options = null;
            this.editor1.TabIndex = 0;
            // 
            // chartListBox1
            // 
            this.chartListBox1.AllowDrop = true;
            this.chartListBox1.FormattingEnabled = true;
            this.chartListBox1.Location = new System.Drawing.Point(7, 32);
            this.chartListBox1.Name = "chartListBox1";
            this.chartListBox1.OtherItems = null;
            this.chartListBox1.Size = new System.Drawing.Size(200, 292);
            this.chartListBox1.TabIndex = 1;
            this.chartListBox1.SelectedIndexChanged += new System.EventHandler(this.chartListBox1_SelectedIndexChanged);
            // 
            // TeeChartContainer
            // 
            this.TeeChartContainer.Location = new System.Drawing.Point(215, 33);
            this.TeeChartContainer.Name = "TeeChartContainer";
            this.TeeChartContainer.Size = new System.Drawing.Size(407, 291);
            this.TeeChartContainer.TabIndex = 5;
            // 
            // rButton_BindData
            // 
            this.rButton_BindData.AutoSize = true;
            this.rButton_BindData.Checked = true;
            this.rButton_BindData.Location = new System.Drawing.Point(12, 59);
            this.rButton_BindData.Name = "rButton_BindData";
            this.rButton_BindData.Size = new System.Drawing.Size(71, 16);
            this.rButton_BindData.TabIndex = 6;
            this.rButton_BindData.TabStop = true;
            this.rButton_BindData.Text = "绑定数据";
            this.rButton_BindData.UseVisualStyleBackColor = true;
            this.rButton_BindData.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // rButton_BindArea
            // 
            this.rButton_BindArea.AutoSize = true;
            this.rButton_BindArea.Location = new System.Drawing.Point(12, 110);
            this.rButton_BindArea.Name = "rButton_BindArea";
            this.rButton_BindArea.Size = new System.Drawing.Size(71, 16);
            this.rButton_BindArea.TabIndex = 7;
            this.rButton_BindArea.Text = "图表数据";
            this.rButton_BindArea.UseVisualStyleBackColor = true;
            this.rButton_BindArea.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rButton_BindArea);
            this.groupBox1.Controls.Add(this.rButton_BindData);
            this.groupBox1.Controls.Add(this.Panel_BindData);
            this.groupBox1.Controls.Add(this.Panel_DataArea);
            this.groupBox1.Location = new System.Drawing.Point(6, 327);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(615, 200);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // Panel_BindData
            // 
            this.Panel_BindData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel_BindData.Controls.Add(this.checkBox1);
            this.Panel_BindData.Controls.Add(this.ComboBox_Functions);
            this.Panel_BindData.Controls.Add(this.label7);
            this.Panel_BindData.Controls.Add(this.TextBox_SeriesAxis);
            this.Panel_BindData.Controls.Add(this.label3);
            this.Panel_BindData.Controls.Add(this.TextBox_CatlogAxis);
            this.Panel_BindData.Controls.Add(this.label1);
            this.Panel_BindData.Location = new System.Drawing.Point(89, 15);
            this.Panel_BindData.Name = "Panel_BindData";
            this.Panel_BindData.Size = new System.Drawing.Size(520, 178);
            this.Panel_BindData.TabIndex = 8;
            // 
            // ComboBox_Functions
            // 
            this.ComboBox_Functions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Functions.FormattingEnabled = true;
            this.ComboBox_Functions.Location = new System.Drawing.Point(374, 134);
            this.ComboBox_Functions.Name = "ComboBox_Functions";
            this.ComboBox_Functions.Size = new System.Drawing.Size(121, 20);
            this.ComboBox_Functions.TabIndex = 7;
            this.ComboBox_Functions.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Functions_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(86, 137);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(179, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "说明：内容格式为表名称.列名称";
            // 
            // TextBox_SeriesAxis
            // 
            this.TextBox_SeriesAxis.BackColor = System.Drawing.Color.White;
            this.TextBox_SeriesAxis.Location = new System.Drawing.Point(88, 106);
            this.TextBox_SeriesAxis.Name = "TextBox_SeriesAxis";
            this.TextBox_SeriesAxis.Size = new System.Drawing.Size(410, 21);
            this.TextBox_SeriesAxis.TabIndex = 5;
            this.TextBox_SeriesAxis.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "序列字段：";
            // 
            // TextBox_CatlogAxis
            // 
            this.TextBox_CatlogAxis.AcceptsReturn = true;
            this.TextBox_CatlogAxis.BackColor = System.Drawing.Color.White;
            this.TextBox_CatlogAxis.Location = new System.Drawing.Point(88, 12);
            this.TextBox_CatlogAxis.Multiline = true;
            this.TextBox_CatlogAxis.Name = "TextBox_CatlogAxis";
            this.TextBox_CatlogAxis.Size = new System.Drawing.Size(410, 66);
            this.TextBox_CatlogAxis.TabIndex = 1;
            this.TextBox_CatlogAxis.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(14, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "分类字段：";
            // 
            // Panel_DataArea
            // 
            this.Panel_DataArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel_DataArea.Controls.Add(this.TextBox_SeriesAxis1);
            this.Panel_DataArea.Controls.Add(this.label2);
            this.Panel_DataArea.Controls.Add(this.TextBox_NameAxis1);
            this.Panel_DataArea.Controls.Add(this.label4);
            this.Panel_DataArea.Controls.Add(this.TextBox_CatlogAxis1);
            this.Panel_DataArea.Controls.Add(this.label5);
            this.Panel_DataArea.Location = new System.Drawing.Point(89, 15);
            this.Panel_DataArea.Name = "Panel_DataArea";
            this.Panel_DataArea.Size = new System.Drawing.Size(520, 178);
            this.Panel_DataArea.TabIndex = 9;
            // 
            // TextBox_SeriesAxis1
            // 
            this.TextBox_SeriesAxis1.BackColor = System.Drawing.Color.White;
            this.TextBox_SeriesAxis1.Location = new System.Drawing.Point(88, 137);
            this.TextBox_SeriesAxis1.Name = "TextBox_SeriesAxis1";
            this.TextBox_SeriesAxis1.Size = new System.Drawing.Size(410, 21);
            this.TextBox_SeriesAxis1.TabIndex = 12;
            this.TextBox_SeriesAxis1.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "序列范围：";
            // 
            // TextBox_NameAxis1
            // 
            this.TextBox_NameAxis1.BackColor = System.Drawing.Color.White;
            this.TextBox_NameAxis1.Location = new System.Drawing.Point(88, 106);
            this.TextBox_NameAxis1.Name = "TextBox_NameAxis1";
            this.TextBox_NameAxis1.Size = new System.Drawing.Size(410, 21);
            this.TextBox_NameAxis1.TabIndex = 10;
            this.TextBox_NameAxis1.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "名称范围：";
            // 
            // TextBox_CatlogAxis1
            // 
            this.TextBox_CatlogAxis1.AcceptsReturn = true;
            this.TextBox_CatlogAxis1.BackColor = System.Drawing.Color.White;
            this.TextBox_CatlogAxis1.Location = new System.Drawing.Point(88, 12);
            this.TextBox_CatlogAxis1.Multiline = true;
            this.TextBox_CatlogAxis1.Name = "TextBox_CatlogAxis1";
            this.TextBox_CatlogAxis1.Size = new System.Drawing.Size(410, 85);
            this.TextBox_CatlogAxis1.TabIndex = 8;
            this.TextBox_CatlogAxis1.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(14, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "分类范围：";
            // 
            // ButtonOk
            // 
            this.ButtonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButtonOk.Location = new System.Drawing.Point(377, 533);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(75, 23);
            this.ButtonOk.TabIndex = 9;
            this.ButtonOk.Text = "确定";
            this.ButtonOk.UseVisualStyleBackColor = true;
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(471, 533);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 10;
            this.ButtonCancel.Text = "取消";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(90, 83);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(84, 16);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "去掉重复值";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // ChartEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 568);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.TeeChartContainer);
            this.Controls.Add(this.chartListBox1);
            this.Controls.Add(this.chartController);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChartEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图表编辑器";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Panel_BindData.ResumeLayout(false);
            this.Panel_BindData.PerformLayout();
            this.Panel_DataArea.ResumeLayout(false);
            this.Panel_DataArea.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Steema.TeeChart.ChartController chartController;
        private Steema.TeeChart.ChartListBox chartListBox1;
        private System.Windows.Forms.Panel TeeChartContainer;
        private System.Windows.Forms.RadioButton rButton_BindData;
        private System.Windows.Forms.RadioButton rButton_BindArea;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel Panel_BindData;
        private System.Windows.Forms.Panel Panel_DataArea;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox TextBox_CatlogAxis;
        internal System.Windows.Forms.TextBox TextBox_SeriesAxis;
        private Steema.TeeChart.Editor editor1;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Label label7;
        internal System.Windows.Forms.TextBox TextBox_NameAxis1;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox TextBox_CatlogAxis1;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox TextBox_SeriesAxis1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ComboBox_Functions;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}