namespace BizComponents
{
    partial class FilterControl
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
            this.TextBox_Value = new System.Windows.Forms.TextBox();
            this.rButton_Or = new System.Windows.Forms.RadioButton();
            this.label18 = new System.Windows.Forms.Label();
            this.rButton_And = new System.Windows.Forms.RadioButton();
            this.cBoxValueStyle = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ComboBoxColumns = new System.Windows.Forms.ComboBox();
            this.ComboBoxParameters = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.Button_Delete = new System.Windows.Forms.Button();
            this.ComboBoxOperations = new System.Windows.Forms.ComboBox();
            this.tViewFilters = new System.Windows.Forms.TreeView();
            this.Button_Add = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TextBox_Value);
            this.groupBox1.Controls.Add(this.rButton_Or);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.rButton_And);
            this.groupBox1.Controls.Add(this.cBoxValueStyle);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.ComboBoxColumns);
            this.groupBox1.Controls.Add(this.ComboBoxParameters);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.Button_Delete);
            this.groupBox1.Controls.Add(this.ComboBoxOperations);
            this.groupBox1.Controls.Add(this.tViewFilters);
            this.groupBox1.Controls.Add(this.Button_Add);
            this.groupBox1.Location = new System.Drawing.Point(0, -4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(579, 392);
            this.groupBox1.TabIndex = 64;
            this.groupBox1.TabStop = false;
            // 
            // TextBox_Value
            // 
            this.TextBox_Value.Location = new System.Drawing.Point(330, 29);
            this.TextBox_Value.Name = "TextBox_Value";
            this.TextBox_Value.Size = new System.Drawing.Size(173, 21);
            this.TextBox_Value.TabIndex = 65;
            // 
            // rButton_Or
            // 
            this.rButton_Or.AutoSize = true;
            this.rButton_Or.Location = new System.Drawing.Point(430, 62);
            this.rButton_Or.Name = "rButton_Or";
            this.rButton_Or.Size = new System.Drawing.Size(59, 16);
            this.rButton_Or.TabIndex = 44;
            this.rButton_Or.TabStop = true;
            this.rButton_Or.Text = "或(OR)";
            this.rButton_Or.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(510, 14);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 12);
            this.label18.TabIndex = 63;
            this.label18.Text = "类型：";
            // 
            // rButton_And
            // 
            this.rButton_And.AutoSize = true;
            this.rButton_And.Checked = true;
            this.rButton_And.Location = new System.Drawing.Point(359, 62);
            this.rButton_And.Name = "rButton_And";
            this.rButton_And.Size = new System.Drawing.Size(65, 16);
            this.rButton_And.TabIndex = 43;
            this.rButton_And.TabStop = true;
            this.rButton_And.Text = "与(AND)";
            this.rButton_And.UseVisualStyleBackColor = true;
            // 
            // cBoxValueStyle
            // 
            this.cBoxValueStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxValueStyle.DropDownWidth = 60;
            this.cBoxValueStyle.FormattingEnabled = true;
            this.cBoxValueStyle.Location = new System.Drawing.Point(510, 29);
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
            this.pictureBox1.Size = new System.Drawing.Size(569, 1);
            this.pictureBox1.TabIndex = 62;
            this.pictureBox1.TabStop = false;
            // 
            // ComboBoxColumns
            // 
            this.ComboBoxColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxColumns.DropDownWidth = 120;
            this.ComboBoxColumns.FormattingEnabled = true;
            this.ComboBoxColumns.Location = new System.Drawing.Point(7, 29);
            this.ComboBoxColumns.Name = "ComboBoxColumns";
            this.ComboBoxColumns.Size = new System.Drawing.Size(210, 20);
            this.ComboBoxColumns.TabIndex = 21;
            // 
            // ComboBoxParameters
            // 
            this.ComboBoxParameters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxParameters.DropDownWidth = 160;
            this.ComboBoxParameters.FormattingEnabled = true;
            this.ComboBoxParameters.Location = new System.Drawing.Point(330, 29);
            this.ComboBoxParameters.Name = "ComboBoxParameters";
            this.ComboBoxParameters.Size = new System.Drawing.Size(173, 20);
            this.ComboBoxParameters.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 19;
            this.label8.Text = "数据列：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(327, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 18;
            this.label9.Text = "值：";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(221, 14);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 12);
            this.label16.TabIndex = 53;
            this.label16.Text = "运算符：";
            // 
            // Button_Delete
            // 
            this.Button_Delete.Location = new System.Drawing.Point(510, 85);
            this.Button_Delete.Name = "Button_Delete";
            this.Button_Delete.Size = new System.Drawing.Size(63, 23);
            this.Button_Delete.TabIndex = 61;
            this.Button_Delete.Text = "删除";
            this.Button_Delete.UseVisualStyleBackColor = true;
            // 
            // ComboBoxOperations
            // 
            this.ComboBoxOperations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxOperations.DropDownWidth = 120;
            this.ComboBoxOperations.FormattingEnabled = true;
            this.ComboBoxOperations.Location = new System.Drawing.Point(222, 29);
            this.ComboBoxOperations.Name = "ComboBoxOperations";
            this.ComboBoxOperations.Size = new System.Drawing.Size(103, 20);
            this.ComboBoxOperations.TabIndex = 54;
            // 
            // tViewFilters
            // 
            this.tViewFilters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tViewFilters.HideSelection = false;
            this.tViewFilters.Location = new System.Drawing.Point(7, 84);
            this.tViewFilters.Name = "tViewFilters";
            this.tViewFilters.Size = new System.Drawing.Size(496, 302);
            this.tViewFilters.TabIndex = 60;
            // 
            // Button_Add
            // 
            this.Button_Add.Location = new System.Drawing.Point(510, 59);
            this.Button_Add.Name = "Button_Add";
            this.Button_Add.Size = new System.Drawing.Size(63, 23);
            this.Button_Add.TabIndex = 57;
            this.Button_Add.Text = "增加";
            this.Button_Add.UseVisualStyleBackColor = true;
            // 
            // FilterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "FilterControl";
            this.Size = new System.Drawing.Size(582, 390);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.ComboBox ComboBoxParameters;
        public System.Windows.Forms.Button Button_Delete;
        public System.Windows.Forms.ComboBox ComboBoxOperations;
        public System.Windows.Forms.RadioButton rButton_And;
        public System.Windows.Forms.TreeView tViewFilters;
        public System.Windows.Forms.Button Button_Add;
        public System.Windows.Forms.RadioButton rButton_Or;
        public System.Windows.Forms.ComboBox ComboBoxColumns;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cBoxValueStyle;
        private System.Windows.Forms.TextBox TextBox_Value;


    }
}
