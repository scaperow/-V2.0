namespace ReportComponents
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Button_Down = new System.Windows.Forms.Button();
            this.Button_Up = new System.Windows.Forms.Button();
            this.Button_Delete = new System.Windows.Forms.Button();
            this.tViewFilters = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rButton_Or = new System.Windows.Forms.RadioButton();
            this.rButton_And = new System.Windows.Forms.RadioButton();
            this.Button_Edit = new System.Windows.Forms.Button();
            this.Button_Add = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.rButton_Formula = new System.Windows.Forms.RadioButton();
            this.rButton_Normal = new System.Windows.Forms.RadioButton();
            this.label13 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.cBoxValueStyle = new System.Windows.Forms.ComboBox();
            this.cBoxKeyColumn = new System.Windows.Forms.ComboBox();
            this.cBoxOperation = new System.Windows.Forms.ComboBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.cBoxValueDataset = new System.Windows.Forms.ComboBox();
            this.cBoxValueColumn = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.comboBox_Parameters = new System.Windows.Forms.ComboBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.tBoxValue = new System.Windows.Forms.TextBox();
            this.cBoxIsNULL = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tBoxFormula = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.Button_DefinedFormula = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Button_Down);
            this.groupBox2.Controls.Add(this.Button_Up);
            this.groupBox2.Controls.Add(this.Button_Delete);
            this.groupBox2.Controls.Add(this.tViewFilters);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.Button_Edit);
            this.groupBox2.Controls.Add(this.Button_Add);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.rButton_Formula);
            this.groupBox2.Controls.Add(this.rButton_Normal);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.panel7);
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Location = new System.Drawing.Point(0, -1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(555, 299);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            // 
            // Button_Down
            // 
            this.Button_Down.Location = new System.Drawing.Point(485, 181);
            this.Button_Down.Name = "Button_Down";
            this.Button_Down.Size = new System.Drawing.Size(63, 23);
            this.Button_Down.TabIndex = 52;
            this.Button_Down.Text = "下移";
            this.Button_Down.UseVisualStyleBackColor = true;
            this.Button_Down.Click += new System.EventHandler(this.Button_Down_Click);
            // 
            // Button_Up
            // 
            this.Button_Up.Location = new System.Drawing.Point(485, 152);
            this.Button_Up.Name = "Button_Up";
            this.Button_Up.Size = new System.Drawing.Size(63, 23);
            this.Button_Up.TabIndex = 51;
            this.Button_Up.Text = "上移";
            this.Button_Up.UseVisualStyleBackColor = true;
            this.Button_Up.Click += new System.EventHandler(this.Button_Up_Click);
            // 
            // Button_Delete
            // 
            this.Button_Delete.Location = new System.Drawing.Point(485, 123);
            this.Button_Delete.Name = "Button_Delete";
            this.Button_Delete.Size = new System.Drawing.Size(63, 23);
            this.Button_Delete.TabIndex = 50;
            this.Button_Delete.Text = "删除";
            this.Button_Delete.UseVisualStyleBackColor = true;
            this.Button_Delete.Click += new System.EventHandler(this.Button_Delete_Click);
            // 
            // tViewFilters
            // 
            this.tViewFilters.HideSelection = false;
            this.tViewFilters.Location = new System.Drawing.Point(4, 121);
            this.tViewFilters.Name = "tViewFilters";
            this.tViewFilters.Size = new System.Drawing.Size(477, 172);
            this.tViewFilters.TabIndex = 49;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rButton_Or);
            this.panel1.Controls.Add(this.rButton_And);
            this.panel1.Location = new System.Drawing.Point(233, 95);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(139, 21);
            this.panel1.TabIndex = 48;
            // 
            // rButton_Or
            // 
            this.rButton_Or.AutoSize = true;
            this.rButton_Or.Location = new System.Drawing.Point(74, 3);
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
            this.rButton_And.Location = new System.Drawing.Point(3, 3);
            this.rButton_And.Name = "rButton_And";
            this.rButton_And.Size = new System.Drawing.Size(65, 16);
            this.rButton_And.TabIndex = 43;
            this.rButton_And.TabStop = true;
            this.rButton_And.Text = "与(AND)";
            this.rButton_And.UseVisualStyleBackColor = true;
            // 
            // Button_Edit
            // 
            this.Button_Edit.Location = new System.Drawing.Point(485, 94);
            this.Button_Edit.Name = "Button_Edit";
            this.Button_Edit.Size = new System.Drawing.Size(63, 23);
            this.Button_Edit.TabIndex = 46;
            this.Button_Edit.Text = "编辑";
            this.Button_Edit.UseVisualStyleBackColor = true;
            // 
            // Button_Add
            // 
            this.Button_Add.Location = new System.Drawing.Point(418, 94);
            this.Button_Add.Name = "Button_Add";
            this.Button_Add.Size = new System.Drawing.Size(63, 23);
            this.Button_Add.TabIndex = 45;
            this.Button_Add.Text = "增加";
            this.Button_Add.UseVisualStyleBackColor = true;
            this.Button_Add.Click += new System.EventHandler(this.Button_Add_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Location = new System.Drawing.Point(2, 31);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(555, 8);
            this.groupBox3.TabIndex = 42;
            this.groupBox3.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(125, 109);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 12);
            this.label7.TabIndex = 39;
            // 
            // rButton_Formula
            // 
            this.rButton_Formula.AutoSize = true;
            this.rButton_Formula.Location = new System.Drawing.Point(345, 18);
            this.rButton_Formula.Name = "rButton_Formula";
            this.rButton_Formula.Size = new System.Drawing.Size(47, 16);
            this.rButton_Formula.TabIndex = 38;
            this.rButton_Formula.TabStop = true;
            this.rButton_Formula.Text = "公式";
            this.rButton_Formula.UseVisualStyleBackColor = true;
            this.rButton_Formula.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // rButton_Normal
            // 
            this.rButton_Normal.AutoSize = true;
            this.rButton_Normal.Location = new System.Drawing.Point(160, 18);
            this.rButton_Normal.Name = "rButton_Normal";
            this.rButton_Normal.Size = new System.Drawing.Size(47, 16);
            this.rButton_Normal.TabIndex = 37;
            this.rButton_Normal.TabStop = true;
            this.rButton_Normal.Text = "普通";
            this.rButton_Normal.UseVisualStyleBackColor = true;
            this.rButton_Normal.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 19);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 12);
            this.label13.TabIndex = 36;
            this.label13.Text = "类型:";
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.label15);
            this.panel7.Controls.Add(this.label16);
            this.panel7.Controls.Add(this.label17);
            this.panel7.Controls.Add(this.label18);
            this.panel7.Controls.Add(this.cBoxValueStyle);
            this.panel7.Controls.Add(this.cBoxKeyColumn);
            this.panel7.Controls.Add(this.cBoxOperation);
            this.panel7.Controls.Add(this.panel5);
            this.panel7.Controls.Add(this.panel3);
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Location = new System.Drawing.Point(1, 40);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(552, 50);
            this.panel7.TabIndex = 40;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(228, 5);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(23, 12);
            this.label15.TabIndex = 27;
            this.label15.Text = "值:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(119, 5);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(35, 12);
            this.label16.TabIndex = 4;
            this.label16.Text = "操作:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(5, 4);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(47, 12);
            this.label17.TabIndex = 3;
            this.label17.Text = "可选列:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(485, 5);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(35, 12);
            this.label18.TabIndex = 6;
            this.label18.Text = "类型:";
            // 
            // cBoxValueStyle
            // 
            this.cBoxValueStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxValueStyle.DropDownWidth = 60;
            this.cBoxValueStyle.FormattingEnabled = true;
            this.cBoxValueStyle.Items.AddRange(new object[] {
            "数据列",
            "值",
            "参数"});
            this.cBoxValueStyle.Location = new System.Drawing.Point(487, 19);
            this.cBoxValueStyle.Name = "cBoxValueStyle";
            this.cBoxValueStyle.Size = new System.Drawing.Size(59, 20);
            this.cBoxValueStyle.TabIndex = 12;
            this.cBoxValueStyle.SelectedIndexChanged += new System.EventHandler(this.cBoxValueStyle_SelectedIndexChanged);
            // 
            // cBoxKeyColumn
            // 
            this.cBoxKeyColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxKeyColumn.DropDownWidth = 120;
            this.cBoxKeyColumn.FormattingEnabled = true;
            this.cBoxKeyColumn.Location = new System.Drawing.Point(3, 20);
            this.cBoxKeyColumn.Name = "cBoxKeyColumn";
            this.cBoxKeyColumn.Size = new System.Drawing.Size(111, 20);
            this.cBoxKeyColumn.TabIndex = 7;
            // 
            // cBoxOperation
            // 
            this.cBoxOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxOperation.DropDownWidth = 120;
            this.cBoxOperation.FormattingEnabled = true;
            this.cBoxOperation.Location = new System.Drawing.Point(120, 20);
            this.cBoxOperation.Name = "cBoxOperation";
            this.cBoxOperation.Size = new System.Drawing.Size(105, 20);
            this.cBoxOperation.TabIndex = 8;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.cBoxValueDataset);
            this.panel5.Controls.Add(this.cBoxValueColumn);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Location = new System.Drawing.Point(230, 18);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(252, 23);
            this.panel5.TabIndex = 25;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(2, 5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 18;
            this.label9.Text = "数据集:";
            // 
            // cBoxValueDataset
            // 
            this.cBoxValueDataset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxValueDataset.DropDownWidth = 160;
            this.cBoxValueDataset.FormattingEnabled = true;
            this.cBoxValueDataset.Location = new System.Drawing.Point(49, 1);
            this.cBoxValueDataset.Name = "cBoxValueDataset";
            this.cBoxValueDataset.Size = new System.Drawing.Size(98, 20);
            this.cBoxValueDataset.TabIndex = 20;
            this.cBoxValueDataset.SelectedIndexChanged += new System.EventHandler(this.cBoxValueDataset_SelectedIndexChanged);
            // 
            // cBoxValueColumn
            // 
            this.cBoxValueColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxValueColumn.DropDownWidth = 120;
            this.cBoxValueColumn.FormattingEnabled = true;
            this.cBoxValueColumn.Location = new System.Drawing.Point(170, 1);
            this.cBoxValueColumn.Name = "cBoxValueColumn";
            this.cBoxValueColumn.Size = new System.Drawing.Size(82, 20);
            this.cBoxValueColumn.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(149, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 12);
            this.label8.TabIndex = 19;
            this.label8.Text = "列:";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.comboBox_Parameters);
            this.panel3.Location = new System.Drawing.Point(230, 18);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(252, 23);
            this.panel3.TabIndex = 53;
            // 
            // comboBox_Parameters
            // 
            this.comboBox_Parameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox_Parameters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Parameters.FormattingEnabled = true;
            this.comboBox_Parameters.Location = new System.Drawing.Point(0, 0);
            this.comboBox_Parameters.Name = "comboBox_Parameters";
            this.comboBox_Parameters.Size = new System.Drawing.Size(250, 20);
            this.comboBox_Parameters.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.tBoxValue);
            this.panel8.Controls.Add(this.cBoxIsNULL);
            this.panel8.Location = new System.Drawing.Point(230, 18);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(252, 23);
            this.panel8.TabIndex = 24;
            // 
            // tBoxValue
            // 
            this.tBoxValue.Location = new System.Drawing.Point(3, 0);
            this.tBoxValue.Name = "tBoxValue";
            this.tBoxValue.Size = new System.Drawing.Size(194, 21);
            this.tBoxValue.TabIndex = 16;
            // 
            // cBoxIsNULL
            // 
            this.cBoxIsNULL.AutoSize = true;
            this.cBoxIsNULL.Location = new System.Drawing.Point(203, 3);
            this.cBoxIsNULL.Name = "cBoxIsNULL";
            this.cBoxIsNULL.Size = new System.Drawing.Size(48, 16);
            this.cBoxIsNULL.TabIndex = 17;
            this.cBoxIsNULL.Text = "空值";
            this.cBoxIsNULL.UseVisualStyleBackColor = true;
            this.cBoxIsNULL.CheckedChanged += new System.EventHandler(this.cBoxIsNULL_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.tBoxFormula);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.Button_DefinedFormula);
            this.panel2.Location = new System.Drawing.Point(1, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(552, 50);
            this.panel2.TabIndex = 41;
            // 
            // tBoxFormula
            // 
            this.tBoxFormula.Location = new System.Drawing.Point(60, 4);
            this.tBoxFormula.Multiline = true;
            this.tBoxFormula.Name = "tBoxFormula";
            this.tBoxFormula.Size = new System.Drawing.Size(419, 41);
            this.tBoxFormula.TabIndex = 14;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(5, 5);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 13;
            this.label14.Text = "公式等于";
            // 
            // Button_DefinedFormula
            // 
            this.Button_DefinedFormula.Location = new System.Drawing.Point(483, 5);
            this.Button_DefinedFormula.Name = "Button_DefinedFormula";
            this.Button_DefinedFormula.Size = new System.Drawing.Size(63, 22);
            this.Button_DefinedFormula.TabIndex = 15;
            this.Button_DefinedFormula.Text = "定义";
            this.Button_DefinedFormula.UseVisualStyleBackColor = true;
            // 
            // FilterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Name = "FilterControl";
            this.Size = new System.Drawing.Size(555, 301);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button Button_Edit;
        private System.Windows.Forms.Button Button_Add;
        private System.Windows.Forms.RadioButton rButton_Or;
        private System.Windows.Forms.RadioButton rButton_And;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton rButton_Formula;
        private System.Windows.Forms.RadioButton rButton_Normal;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox tBoxFormula;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button Button_DefinedFormula;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cBoxValueStyle;
        private System.Windows.Forms.ComboBox cBoxKeyColumn;
        private System.Windows.Forms.ComboBox cBoxOperation;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.TextBox tBoxValue;
        private System.Windows.Forms.CheckBox cBoxIsNULL;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cBoxValueDataset;
        private System.Windows.Forms.ComboBox cBoxValueColumn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView tViewFilters;
        private System.Windows.Forms.Button Button_Down;
        private System.Windows.Forms.Button Button_Up;
        private System.Windows.Forms.Button Button_Delete;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox comboBox_Parameters;

    }
}
