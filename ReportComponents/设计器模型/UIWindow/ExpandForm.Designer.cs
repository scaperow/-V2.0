namespace ReportComponents
{
    partial class ExpandForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rButton_None = new System.Windows.Forms.RadioButton();
            this.rButton_LeftToRight = new System.Windows.Forms.RadioButton();
            this.rButton_TopToBottom = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Button_DefinedGroup = new System.Windows.Forms.Button();
            this.cBoxAggregation = new System.Windows.Forms.ComboBox();
            this.cBoxGroup = new System.Windows.Forms.ComboBox();
            this.rButton_Aggregation = new System.Windows.Forms.RadioButton();
            this.rButton_Group = new System.Windows.Forms.RadioButton();
            this.rButton_List = new System.Windows.Forms.RadioButton();
            this.TopCoordControl = new ReportComponents.CoordControl();
            this.LeftCoordControl = new ReportComponents.CoordControl();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TopCoordControl);
            this.groupBox1.Controls.Add(this.LeftCoordControl);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(402, 47);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "父格设置";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(203, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "上父格:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "左父格:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rButton_None);
            this.groupBox2.Controls.Add(this.rButton_LeftToRight);
            this.groupBox2.Controls.Add(this.rButton_TopToBottom);
            this.groupBox2.Location = new System.Drawing.Point(8, 54);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(402, 41);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "扩展方向";
            // 
            // rButton_None
            // 
            this.rButton_None.AutoSize = true;
            this.rButton_None.Location = new System.Drawing.Point(300, 16);
            this.rButton_None.Name = "rButton_None";
            this.rButton_None.Size = new System.Drawing.Size(59, 16);
            this.rButton_None.TabIndex = 2;
            this.rButton_None.TabStop = true;
            this.rButton_None.Text = "不扩展";
            this.rButton_None.UseVisualStyleBackColor = true;
            this.rButton_None.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // rButton_LeftToRight
            // 
            this.rButton_LeftToRight.AutoSize = true;
            this.rButton_LeftToRight.Location = new System.Drawing.Point(154, 16);
            this.rButton_LeftToRight.Name = "rButton_LeftToRight";
            this.rButton_LeftToRight.Size = new System.Drawing.Size(71, 16);
            this.rButton_LeftToRight.TabIndex = 1;
            this.rButton_LeftToRight.TabStop = true;
            this.rButton_LeftToRight.Text = "从左到右";
            this.rButton_LeftToRight.UseVisualStyleBackColor = true;
            this.rButton_LeftToRight.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // rButton_TopToBottom
            // 
            this.rButton_TopToBottom.AutoSize = true;
            this.rButton_TopToBottom.Location = new System.Drawing.Point(8, 16);
            this.rButton_TopToBottom.Name = "rButton_TopToBottom";
            this.rButton_TopToBottom.Size = new System.Drawing.Size(71, 16);
            this.rButton_TopToBottom.TabIndex = 0;
            this.rButton_TopToBottom.TabStop = true;
            this.rButton_TopToBottom.Text = "从上到下";
            this.rButton_TopToBottom.UseVisualStyleBackColor = true;
            this.rButton_TopToBottom.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panel1);
            this.groupBox3.Location = new System.Drawing.Point(416, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.groupBox3.Size = new System.Drawing.Size(201, 91);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Button_DefinedGroup);
            this.panel1.Controls.Add(this.cBoxAggregation);
            this.panel1.Controls.Add(this.cBoxGroup);
            this.panel1.Controls.Add(this.rButton_Aggregation);
            this.panel1.Controls.Add(this.rButton_Group);
            this.panel1.Controls.Add(this.rButton_List);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(195, 74);
            this.panel1.TabIndex = 7;
            // 
            // Button_DefinedGroup
            // 
            this.Button_DefinedGroup.Location = new System.Drawing.Point(107, -1);
            this.Button_DefinedGroup.Name = "Button_DefinedGroup";
            this.Button_DefinedGroup.Size = new System.Drawing.Size(56, 22);
            this.Button_DefinedGroup.TabIndex = 6;
            this.Button_DefinedGroup.Text = "自定义";
            this.Button_DefinedGroup.UseVisualStyleBackColor = true;
            // 
            // cBoxAggregation
            // 
            this.cBoxAggregation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxAggregation.FormattingEnabled = true;
            this.cBoxAggregation.Location = new System.Drawing.Point(52, 46);
            this.cBoxAggregation.Name = "cBoxAggregation";
            this.cBoxAggregation.Size = new System.Drawing.Size(54, 20);
            this.cBoxAggregation.TabIndex = 5;
            this.cBoxAggregation.SelectedIndexChanged += new System.EventHandler(this.cBoxAggregation_SelectedIndexChanged);
            // 
            // cBoxGroup
            // 
            this.cBoxGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxGroup.FormattingEnabled = true;
            this.cBoxGroup.Location = new System.Drawing.Point(52, 0);
            this.cBoxGroup.Name = "cBoxGroup";
            this.cBoxGroup.Size = new System.Drawing.Size(54, 20);
            this.cBoxGroup.TabIndex = 4;
            // 
            // rButton_Aggregation
            // 
            this.rButton_Aggregation.AutoSize = true;
            this.rButton_Aggregation.Location = new System.Drawing.Point(6, 48);
            this.rButton_Aggregation.Name = "rButton_Aggregation";
            this.rButton_Aggregation.Size = new System.Drawing.Size(47, 16);
            this.rButton_Aggregation.TabIndex = 3;
            this.rButton_Aggregation.TabStop = true;
            this.rButton_Aggregation.Text = "汇总";
            this.rButton_Aggregation.UseVisualStyleBackColor = true;
            this.rButton_Aggregation.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // rButton_Group
            // 
            this.rButton_Group.AutoSize = true;
            this.rButton_Group.Location = new System.Drawing.Point(6, 2);
            this.rButton_Group.Name = "rButton_Group";
            this.rButton_Group.Size = new System.Drawing.Size(47, 16);
            this.rButton_Group.TabIndex = 2;
            this.rButton_Group.TabStop = true;
            this.rButton_Group.Text = "分组";
            this.rButton_Group.UseVisualStyleBackColor = true;
            this.rButton_Group.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // rButton_List
            // 
            this.rButton_List.AutoSize = true;
            this.rButton_List.Location = new System.Drawing.Point(6, 25);
            this.rButton_List.Name = "rButton_List";
            this.rButton_List.Size = new System.Drawing.Size(47, 16);
            this.rButton_List.TabIndex = 1;
            this.rButton_List.TabStop = true;
            this.rButton_List.Text = "列表";
            this.rButton_List.UseVisualStyleBackColor = true;
            this.rButton_List.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // TopCoordControl
            // 
            this.TopCoordControl.ForeColor = System.Drawing.Color.DimGray;
            this.TopCoordControl.Location = new System.Drawing.Point(255, 15);
            this.TopCoordControl.Name = "TopCoordControl";
            this.TopCoordControl.Size = new System.Drawing.Size(127, 24);
            this.TopCoordControl.TabIndex = 4;
            this.TopCoordControl.ValueChanged += new System.EventHandler<ReportComponents.ValueChangedEventArgs>(this.CoordControl_ValueChanged);
            // 
            // LeftCoordControl
            // 
            this.LeftCoordControl.ForeColor = System.Drawing.Color.DimGray;
            this.LeftCoordControl.Location = new System.Drawing.Point(59, 15);
            this.LeftCoordControl.Name = "LeftCoordControl";
            this.LeftCoordControl.Size = new System.Drawing.Size(127, 24);
            this.LeftCoordControl.TabIndex = 1;
            this.LeftCoordControl.ValueChanged += new System.EventHandler<ReportComponents.ValueChangedEventArgs>(this.CoordControl_ValueChanged);
            // 
            // ExpandForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(623, 105);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HideOnClose = true;
            this.Name = "ExpandForm";
            this.Text = "扩展";
            this.Load += new System.EventHandler(this.ExpandForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rButton_None;
        private System.Windows.Forms.RadioButton rButton_LeftToRight;
        private System.Windows.Forms.RadioButton rButton_TopToBottom;
        private System.Windows.Forms.Button Button_DefinedGroup;
        private System.Windows.Forms.ComboBox cBoxAggregation;
        private System.Windows.Forms.ComboBox cBoxGroup;
        private System.Windows.Forms.RadioButton rButton_Aggregation;
        private System.Windows.Forms.RadioButton rButton_Group;
        private System.Windows.Forms.RadioButton rButton_List;
        private ReportComponents.CoordControl TopCoordControl;
        private ReportComponents.CoordControl LeftCoordControl;
        private System.Windows.Forms.Panel panel1;
    }
}