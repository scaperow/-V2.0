namespace BizComponents
{
    partial class CaiJiChartView
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.groupBox2 = new System.Windows.Forms.Panel();
            this.ChartLineControl = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChartLineControl)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.groupBox2.Controls.Add(this.ChartLineControl);
            this.groupBox2.Location = new System.Drawing.Point(7, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(571, 377);
            this.groupBox2.TabIndex = 10;
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
            // CaiJiChartView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 412);
            this.Controls.Add(this.groupBox2);
            this.Name = "CaiJiChartView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CaiJiChartView";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.CaiJiChartView_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChartLineControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel groupBox2;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartLineControl;
    }
}