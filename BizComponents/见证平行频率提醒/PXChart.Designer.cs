namespace BizComponents
{
    partial class PXChart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PXChart));
            this.label1 = new System.Windows.Forms.Label();
            this.line1 = new Steema.TeeChart.Styles.Line();
            this.bar1 = new Steema.TeeChart.Styles.Bar();
            this.tChart1 = new Steema.TeeChart.TChart();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(23, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 20);
            this.label1.TabIndex = 1;
            // 
            // line1
            // 
            // 
            // 
            // 
            this.line1.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(53)))));
            this.line1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(53)))));
            this.line1.ColorEach = false;
            // 
            // 
            // 
            this.line1.LinePen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(78)))), ((int)(((byte)(26)))));
            // 
            // 
            // 
            // 
            // 
            // 
            this.line1.Marks.Callout.ArrowHead = Steema.TeeChart.Styles.ArrowHeadStyles.None;
            this.line1.Marks.Callout.ArrowHeadSize = 8;
            // 
            // 
            // 
            this.line1.Marks.Callout.Brush.Color = System.Drawing.Color.Black;
            this.line1.Marks.Callout.Distance = 0;
            this.line1.Marks.Callout.Draw3D = false;
            this.line1.Marks.Callout.Length = 10;
            this.line1.Marks.Callout.Style = Steema.TeeChart.Styles.PointerStyles.Rectangle;
            this.line1.Marks.Callout.Visible = false;
            // 
            // 
            // 
            this.line1.Pointer.HorizSize = 10;
            this.line1.Pointer.Style = Steema.TeeChart.Styles.PointerStyles.Rectangle;
            this.line1.Title = "line1";
            // 
            // 
            // 
            this.line1.XValues.DataMember = "X";
            this.line1.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.line1.YValues.DataMember = "Y";
            // 
            // bar1
            // 
            // 
            // 
            // 
            this.bar1.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(102)))), ((int)(((byte)(163)))));
            this.bar1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(102)))), ((int)(((byte)(163)))));
            this.bar1.ColorEach = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.bar1.Marks.Callout.ArrowHead = Steema.TeeChart.Styles.ArrowHeadStyles.None;
            this.bar1.Marks.Callout.ArrowHeadSize = 8;
            // 
            // 
            // 
            this.bar1.Marks.Callout.Brush.Color = System.Drawing.Color.Black;
            this.bar1.Marks.Callout.Distance = 0;
            this.bar1.Marks.Callout.Draw3D = false;
            this.bar1.Marks.Callout.Length = 20;
            this.bar1.Marks.Callout.Style = Steema.TeeChart.Styles.PointerStyles.Rectangle;
            this.bar1.Marks.Callout.Visible = false;
            // 
            // 
            // 
            this.bar1.Pen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(61)))), ((int)(((byte)(98)))));
            this.bar1.Title = "bar1";
            // 
            // 
            // 
            this.bar1.XValues.DataMember = "X";
            this.bar1.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.bar1.YValues.DataMember = "Bar";
            // 
            // tChart1
            // 
            // 
            // 
            // 
            this.tChart1.Aspect.ZOffset = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart1.Axes.Bottom.MaximumOffset = 42;
            this.tChart1.Axes.Bottom.MinimumOffset = 42;
            // 
            // 
            // 
            this.tChart1.Axes.Left.MaximumOffset = 37;
            this.tChart1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tChart1.Location = new System.Drawing.Point(25, 71);
            this.tChart1.Name = "tChart1";
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart1.Panel.Pen.Width = 5;
            this.tChart1.Series.Add(this.bar1);
            this.tChart1.Series.Add(this.line1);
            this.tChart1.Size = new System.Drawing.Size(584, 477);
            this.tChart1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(556, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(41, 21);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "3";
            this.textBox1.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(556, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(40, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "计算";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(25, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 10);
            this.label2.TabIndex = 4;
            this.label2.Text = "注：总分100";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(280, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 20);
            this.label3.TabIndex = 5;
            // 
            // PXChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 560);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tChart1);
            this.Name = "PXChart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PXChart";
            this.Load += new System.EventHandler(this.PXChart_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Steema.TeeChart.Styles.Line line1;
        private Steema.TeeChart.Styles.Bar bar1;
        private Steema.TeeChart.TChart tChart1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;



    }
}