namespace BizComponents
{
    partial class ExpressionControl
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.cBox_ComparisonOperator = new System.Windows.Forms.ComboBox();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.TextBox3 = new System.Windows.Forms.TextBox();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.TextBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.panel3.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cBox_ComparisonOperator);
            this.panel3.Controls.Add(this.Panel1);
            this.panel3.Controls.Add(this.Panel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(405, 24);
            this.panel3.TabIndex = 9;
            // 
            // cBox_ComparisonOperator
            // 
            this.cBox_ComparisonOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBox_ComparisonOperator.FormattingEnabled = true;
            this.cBox_ComparisonOperator.Location = new System.Drawing.Point(3, 2);
            this.cBox_ComparisonOperator.Name = "cBox_ComparisonOperator";
            this.cBox_ComparisonOperator.Size = new System.Drawing.Size(100, 20);
            this.cBox_ComparisonOperator.TabIndex = 2;
            this.cBox_ComparisonOperator.SelectedIndexChanged += new System.EventHandler(this.cBox_ComparisonOperator_SelectedIndexChanged);
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.TextBox3);
            this.Panel2.Location = new System.Drawing.Point(109, -1);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(294, 27);
            this.Panel2.TabIndex = 4;
            // 
            // TextBox3
            // 
            this.TextBox3.Location = new System.Drawing.Point(3, 3);
            this.TextBox3.Margin = new System.Windows.Forms.Padding(0);
            this.TextBox3.Name = "TextBox3";
            this.TextBox3.Size = new System.Drawing.Size(288, 21);
            this.TextBox3.TabIndex = 0;
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.TextBox2);
            this.Panel1.Controls.Add(this.label1);
            this.Panel1.Controls.Add(this.TextBox1);
            this.Panel1.Location = new System.Drawing.Point(109, -1);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(294, 26);
            this.Panel1.TabIndex = 3;
            // 
            // TextBox2
            // 
            this.TextBox2.Location = new System.Drawing.Point(165, 2);
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.Size = new System.Drawing.Size(129, 21);
            this.TextBox2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(138, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "与";
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(3, 2);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(127, 21);
            this.TextBox1.TabIndex = 0;
            // 
            // ExpressionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Name = "ExpressionControl";
            this.Size = new System.Drawing.Size(405, 24);
            this.Load += new System.EventHandler(this.ExpressionControl_Load);
            this.panel3.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cBox_ComparisonOperator;
        private System.Windows.Forms.Panel Panel2;
        private System.Windows.Forms.TextBox TextBox3;
        private System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.TextBox TextBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextBox1;
    }
}
