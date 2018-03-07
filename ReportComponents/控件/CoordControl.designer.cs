namespace ReportComponents
{
    partial class CoordControl
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.numberControl2 = new ReportComponents.NumberControl();
            this.numberControl1 = new ReportComponents.NumberControl();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(0, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 23);
            this.label1.TabIndex = 5;
            this.label1.Text = "默认";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.numberControl2);
            this.panel1.Controls.Add(this.numberControl1);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(117, 23);
            this.panel1.TabIndex = 6;
            // 
            // numberControl2
            // 
            this.numberControl2.Location = new System.Drawing.Point(61, 0);
            this.numberControl2.Max = 100;
            this.numberControl2.Min = 0;
            this.numberControl2.Name = "numberControl2";
            this.numberControl2.Size = new System.Drawing.Size(57, 21);
            this.numberControl2.Style = ReportComponents.DisplayStyle.Digital;
            this.numberControl2.TabIndex = 1;
            this.numberControl2.Value = "1";
            // 
            // numberControl1
            // 
            this.numberControl1.Location = new System.Drawing.Point(0, 0);
            this.numberControl1.Max = 100;
            this.numberControl1.Min = 0;
            this.numberControl1.Name = "numberControl1";
            this.numberControl1.Size = new System.Drawing.Size(61, 21);
            this.numberControl1.Style = ReportComponents.DisplayStyle.Letter;
            this.numberControl1.TabIndex = 0;
            this.numberControl1.Value = "A";
            // 
            // radioButton1
            // 
            this.radioButton1.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton1.AutoCheck = false;
            this.radioButton1.CausesValidation = false;
            this.radioButton1.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton1.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.radioButton1.Location = new System.Drawing.Point(117, 0);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(22, 24);
            this.radioButton1.TabIndex = 7;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "▼";
            this.radioButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(0, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "无";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CoordControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.ForeColor = System.Drawing.Color.DimGray;
            this.Name = "CoordControl";
            this.Size = new System.Drawing.Size(138, 24);
            this.Resize += new System.EventHandler(this.CoordControl_Resize);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButton1;
        private NumberControl numberControl2;
        private NumberControl numberControl1;
        private System.Windows.Forms.Label label2;

    }
}
