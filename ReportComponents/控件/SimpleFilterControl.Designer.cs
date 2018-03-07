namespace ReportComponents
{
    partial class SimpleFilterControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.cBoxValueDataset = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cBoxValueStyle = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cBoxValueColumn = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.Button_Delete = new System.Windows.Forms.Button();
            this.cBoxOperation = new System.Windows.Forms.ComboBox();
            this.tViewFilters = new System.Windows.Forms.TreeView();
            this.Button_Add = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rButton_Or = new System.Windows.Forms.RadioButton();
            this.rButton_And = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cBox_Parameters = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.cBoxValueStyle);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.cBoxValueColumn);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.Button_Delete);
            this.groupBox1.Controls.Add(this.cBoxOperation);
            this.groupBox1.Controls.Add(this.tViewFilters);
            this.groupBox1.Controls.Add(this.Button_Add);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(690, 514);
            this.groupBox1.TabIndex = 64;
            this.groupBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.cBoxValueDataset);
            this.panel2.Location = new System.Drawing.Point(414, 10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 42);
            this.panel2.TabIndex = 65;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1, 4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 18;
            this.label9.Text = "数据集:";
            // 
            // cBoxValueDataset
            // 
            this.cBoxValueDataset.DropDownWidth = 160;
            this.cBoxValueDataset.FormattingEnabled = true;
            this.cBoxValueDataset.Location = new System.Drawing.Point(3, 19);
            this.cBoxValueDataset.Name = "cBoxValueDataset";
            this.cBoxValueDataset.Size = new System.Drawing.Size(194, 20);
            this.cBoxValueDataset.TabIndex = 20;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(617, 14);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(35, 12);
            this.label18.TabIndex = 63;
            this.label18.Text = "类型:";
            // 
            // cBoxValueStyle
            // 
            this.cBoxValueStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxValueStyle.DropDownWidth = 60;
            this.cBoxValueStyle.FormattingEnabled = true;
            this.cBoxValueStyle.Items.AddRange(new object[] {
            "值",
            "参数"});
            this.cBoxValueStyle.Location = new System.Drawing.Point(619, 29);
            this.cBoxValueStyle.Name = "cBoxValueStyle";
            this.cBoxValueStyle.Size = new System.Drawing.Size(63, 20);
            this.cBoxValueStyle.TabIndex = 64;
            this.cBoxValueStyle.SelectedIndexChanged += new System.EventHandler(this.cBoxValueStyle_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(6, 53);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(675, 1);
            this.pictureBox1.TabIndex = 62;
            this.pictureBox1.TabStop = false;
            // 
            // cBoxValueColumn
            // 
            this.cBoxValueColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxValueColumn.DropDownWidth = 120;
            this.cBoxValueColumn.FormattingEnabled = true;
            this.cBoxValueColumn.Location = new System.Drawing.Point(6, 29);
            this.cBoxValueColumn.Name = "cBoxValueColumn";
            this.cBoxValueColumn.Size = new System.Drawing.Size(274, 20);
            this.cBoxValueColumn.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 19;
            this.label8.Text = "数据列:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(286, 14);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 12);
            this.label16.TabIndex = 53;
            this.label16.Text = "运算条件:";
            // 
            // Button_Delete
            // 
            this.Button_Delete.Location = new System.Drawing.Point(619, 57);
            this.Button_Delete.Name = "Button_Delete";
            this.Button_Delete.Size = new System.Drawing.Size(63, 23);
            this.Button_Delete.TabIndex = 61;
            this.Button_Delete.Text = "删除";
            this.Button_Delete.UseVisualStyleBackColor = true;
            // 
            // cBoxOperation
            // 
            this.cBoxOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxOperation.DropDownWidth = 120;
            this.cBoxOperation.FormattingEnabled = true;
            this.cBoxOperation.Location = new System.Drawing.Point(287, 29);
            this.cBoxOperation.Name = "cBoxOperation";
            this.cBoxOperation.Size = new System.Drawing.Size(123, 20);
            this.cBoxOperation.TabIndex = 54;
            // 
            // tViewFilters
            // 
            this.tViewFilters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tViewFilters.HideSelection = false;
            this.tViewFilters.Location = new System.Drawing.Point(7, 83);
            this.tViewFilters.Name = "tViewFilters";
            this.tViewFilters.Size = new System.Drawing.Size(674, 423);
            this.tViewFilters.TabIndex = 60;
            // 
            // Button_Add
            // 
            this.Button_Add.Location = new System.Drawing.Point(553, 57);
            this.Button_Add.Name = "Button_Add";
            this.Button_Add.Size = new System.Drawing.Size(63, 23);
            this.Button_Add.TabIndex = 57;
            this.Button_Add.Text = "增加";
            this.Button_Add.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rButton_Or);
            this.panel1.Controls.Add(this.rButton_And);
            this.panel1.Location = new System.Drawing.Point(410, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(139, 21);
            this.panel1.TabIndex = 59;
            // 
            // rButton_Or
            // 
            this.rButton_Or.AutoSize = true;
            this.rButton_Or.Location = new System.Drawing.Point(76, 3);
            this.rButton_Or.Name = "rButton_Or";
            this.rButton_Or.Size = new System.Drawing.Size(59, 16);
            this.rButton_Or.TabIndex = 44;
            this.rButton_Or.TabStop = true;
            this.rButton_Or.Text = "或(OR)";
            this.rButton_Or.UseVisualStyleBackColor = true;
            // 
            // rButton_And
            // 
            this.rButton_And.AutoSize = true;
            this.rButton_And.Checked = true;
            this.rButton_And.Location = new System.Drawing.Point(5, 3);
            this.rButton_And.Name = "rButton_And";
            this.rButton_And.Size = new System.Drawing.Size(65, 16);
            this.rButton_And.TabIndex = 43;
            this.rButton_And.TabStop = true;
            this.rButton_And.Text = "与(AND)";
            this.rButton_And.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.cBox_Parameters);
            this.panel3.Location = new System.Drawing.Point(414, 10);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 42);
            this.panel3.TabIndex = 66;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "参数列表:";
            // 
            // cBox_Parameters
            // 
            this.cBox_Parameters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBox_Parameters.DropDownWidth = 160;
            this.cBox_Parameters.FormattingEnabled = true;
            this.cBox_Parameters.Location = new System.Drawing.Point(3, 19);
            this.cBox_Parameters.Name = "cBox_Parameters";
            this.cBox_Parameters.Size = new System.Drawing.Size(194, 20);
            this.cBox_Parameters.TabIndex = 20;
            // 
            // SimpleFilterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "SimpleFilterControl";
            this.Size = new System.Drawing.Size(690, 514);
            this.Load += new System.EventHandler(this.SimpleFilterControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.ComboBox cBoxValueDataset;
        public System.Windows.Forms.Button Button_Delete;
        public System.Windows.Forms.ComboBox cBoxOperation;
        public System.Windows.Forms.RadioButton rButton_And;
        public System.Windows.Forms.TreeView tViewFilters;
        public System.Windows.Forms.Button Button_Add;
        public System.Windows.Forms.RadioButton rButton_Or;
        public System.Windows.Forms.ComboBox cBoxValueColumn;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox cBox_Parameters;
        internal System.Windows.Forms.ComboBox cBoxValueStyle;


    }
}
