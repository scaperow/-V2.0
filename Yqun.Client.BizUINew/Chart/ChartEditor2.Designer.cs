using System.Windows.Forms;
namespace Yqun.Client.BizUI
{
    partial class ChartEditor2
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
            this.Btn_Ok = new System.Windows.Forms.Button();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.TeeChartContainer = new System.Windows.Forms.Panel();
            this.chartListBox1 = new Steema.TeeChart.ChartListBox(this.components);
            this.XYValues = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tBoxYValue = new System.Windows.Forms.TextBox();
            this.tBoxXValue = new System.Windows.Forms.TextBox();
            this.editor1 = new Steema.TeeChart.Editor(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.OutputYValue = new System.Windows.Forms.TextBox();
            this.OutputXValue = new System.Windows.Forms.TextBox();
            this.chartController = new Steema.TeeChart.Commander();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tBox_Labels = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.XYValues.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_Ok
            // 
            this.Btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Btn_Ok.Location = new System.Drawing.Point(393, 563);
            this.Btn_Ok.Name = "Btn_Ok";
            this.Btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.Btn_Ok.TabIndex = 2;
            this.Btn_Ok.Text = "确定";
            this.Btn_Ok.UseVisualStyleBackColor = true;
            this.Btn_Ok.Click += new System.EventHandler(this.Btn_Ok_Click);
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Btn_Cancel.Location = new System.Drawing.Point(497, 563);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Btn_Cancel.TabIndex = 3;
            this.Btn_Cancel.Text = "取消";
            this.Btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // TeeChartContainer
            // 
            this.TeeChartContainer.Location = new System.Drawing.Point(222, 34);
            this.TeeChartContainer.Name = "TeeChartContainer";
            this.TeeChartContainer.Size = new System.Drawing.Size(371, 268);
            this.TeeChartContainer.TabIndex = 4;
            // 
            // chartListBox1
            // 
            this.chartListBox1.AllowDrop = true;
            this.chartListBox1.FormattingEnabled = true;
            this.chartListBox1.Location = new System.Drawing.Point(8, 34);
            this.chartListBox1.Name = "chartListBox1";
            this.chartListBox1.OtherItems = null;
            this.chartListBox1.Size = new System.Drawing.Size(208, 268);
            this.chartListBox1.TabIndex = 0;
            this.chartListBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chartListBox1_MouseDoubleClick);
            this.chartListBox1.SelectedIndexChanged += new System.EventHandler(this.chartListBox1_SelectedIndexChanged);
            // 
            // XYValues
            // 
            this.XYValues.Controls.Add(this.label2);
            this.XYValues.Controls.Add(this.label1);
            this.XYValues.Controls.Add(this.tBoxYValue);
            this.XYValues.Controls.Add(this.tBoxXValue);
            this.XYValues.Location = new System.Drawing.Point(7, 310);
            this.XYValues.Name = "XYValues";
            this.XYValues.Padding = new System.Windows.Forms.Padding(8);
            this.XYValues.Size = new System.Drawing.Size(585, 93);
            this.XYValues.TabIndex = 5;
            this.XYValues.TabStop = false;
            this.XYValues.Text = "输入值位置(坐标值可以为常数也可以为单位格位置)";
            // 
            // label2
            // 
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(4, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "X坐标：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(4, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Y坐标：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tBoxYValue
            // 
            this.tBoxYValue.Location = new System.Drawing.Point(75, 52);
            this.tBoxYValue.Multiline = true;
            this.tBoxYValue.Name = "tBoxYValue";
            this.tBoxYValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tBoxYValue.Size = new System.Drawing.Size(499, 30);
            this.tBoxYValue.TabIndex = 1;
            this.tBoxYValue.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // tBoxXValue
            // 
            this.tBoxXValue.Location = new System.Drawing.Point(75, 17);
            this.tBoxXValue.Multiline = true;
            this.tBoxXValue.Name = "tBoxXValue";
            this.tBoxXValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tBoxXValue.Size = new System.Drawing.Size(499, 30);
            this.tBoxXValue.TabIndex = 0;
            this.tBoxXValue.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // editor1
            // 
            this.editor1.HighLightTabs = false;
            this.editor1.Location = new System.Drawing.Point(0, 0);
            this.editor1.Name = "editor1";
            this.editor1.Options = null;
            this.editor1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.OutputYValue);
            this.groupBox1.Controls.Add(this.OutputXValue);
            this.groupBox1.Location = new System.Drawing.Point(7, 407);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(8);
            this.groupBox1.Size = new System.Drawing.Size(585, 93);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "输出值位置(坐标值可以为单元格位置也可以为-1，XY坐标为-1表示跳过此输出值)";
            // 
            // label3
            // 
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(4, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "X坐标：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(4, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 23);
            this.label4.TabIndex = 2;
            this.label4.Text = "Y坐标：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OutputYValue
            // 
            this.OutputYValue.Location = new System.Drawing.Point(75, 54);
            this.OutputYValue.Multiline = true;
            this.OutputYValue.Name = "OutputYValue";
            this.OutputYValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.OutputYValue.Size = new System.Drawing.Size(499, 30);
            this.OutputYValue.TabIndex = 1;
            this.OutputYValue.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // OutputXValue
            // 
            this.OutputXValue.Location = new System.Drawing.Point(75, 17);
            this.OutputXValue.Multiline = true;
            this.OutputXValue.Name = "OutputXValue";
            this.OutputXValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.OutputXValue.Size = new System.Drawing.Size(499, 30);
            this.OutputXValue.TabIndex = 0;
            this.OutputXValue.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // chartController
            // 
            this.chartController.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.chartController.DropDownArrows = true;
            this.chartController.Editor = this.editor1;
            this.chartController.LabelValues = true;
            this.chartController.Location = new System.Drawing.Point(3, 0);
            this.chartController.Name = "chartController";
            this.chartController.ShowToolTips = true;
            this.chartController.Size = new System.Drawing.Size(597, 37);
            this.chartController.TabIndex = 8;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tBox_Labels);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(8, 502);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(584, 45);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "轴标签";
            // 
            // tBox_Labels
            // 
            this.tBox_Labels.Location = new System.Drawing.Point(75, 15);
            this.tBox_Labels.Multiline = true;
            this.tBox_Labels.Name = "tBox_Labels";
            this.tBox_Labels.Size = new System.Drawing.Size(498, 22);
            this.tBox_Labels.TabIndex = 1;
            this.tBox_Labels.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "横 轴：";
            // 
            // ChartEditor2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 596);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chartListBox1);
            this.Controls.Add(this.XYValues);
            this.Controls.Add(this.TeeChartContainer);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.Btn_Ok);
            this.Controls.Add(this.chartController);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChartEditor2";
            this.Padding = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图表编辑器";
            this.Load += new System.EventHandler(this.ChartEditor2_Load);
            this.XYValues.ResumeLayout(false);
            this.XYValues.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_Ok;
        private System.Windows.Forms.Button Btn_Cancel;
        private System.Windows.Forms.Panel TeeChartContainer;
        private Steema.TeeChart.ChartListBox chartListBox1;
        private System.Windows.Forms.GroupBox XYValues;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tBoxYValue;
        private System.Windows.Forms.TextBox tBoxXValue;
        private Steema.TeeChart.Editor editor1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox OutputYValue;
        private System.Windows.Forms.TextBox OutputXValue;
        private Steema.TeeChart.Commander chartController;
        private GroupBox groupBox2;
        private TextBox tBox_Labels;
        private Label label5;
    }
}