namespace BizComponents
{
    partial class ConditionFormatDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.expressionControl1 = new BizComponents.ExpressionControl();
            this.ButtonFormatDialog = new System.Windows.Forms.Button();
            this.lb_Example = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cBox_Value = new System.Windows.Forms.ComboBox();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.ButtonClear = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.expressionControl1);
            this.groupBox1.Controls.Add(this.ButtonFormatDialog);
            this.groupBox1.Controls.Add(this.lb_Example);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cBox_Value);
            this.groupBox1.Location = new System.Drawing.Point(10, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(534, 134);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "条件1(&1)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "说明文字";
            // 
            // expressionControl1
            // 
            this.expressionControl1.Location = new System.Drawing.Point(121, 41);
            this.expressionControl1.Name = "expressionControl1";
            this.expressionControl1.Size = new System.Drawing.Size(404, 25);
            this.expressionControl1.TabIndex = 2;
            // 
            // ButtonFormatDialog
            // 
            this.ButtonFormatDialog.Location = new System.Drawing.Point(419, 84);
            this.ButtonFormatDialog.Name = "ButtonFormatDialog";
            this.ButtonFormatDialog.Size = new System.Drawing.Size(101, 23);
            this.ButtonFormatDialog.TabIndex = 3;
            this.ButtonFormatDialog.Text = "格式...";
            this.ButtonFormatDialog.UseVisualStyleBackColor = true;
            this.ButtonFormatDialog.Click += new System.EventHandler(this.ButtonFormatDialog_Click);
            // 
            // lb_Example
            // 
            this.lb_Example.BackColor = System.Drawing.Color.White;
            this.lb_Example.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_Example.Location = new System.Drawing.Point(121, 77);
            this.lb_Example.Name = "lb_Example";
            this.lb_Example.Size = new System.Drawing.Size(276, 40);
            this.lb_Example.TabIndex = 6;
            this.lb_Example.Text = "条件格式";
            this.lb_Example.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(14, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 40);
            this.label2.TabIndex = 5;
            this.label2.Text = "条件为真时，待用格式如右图所示";
            // 
            // cBox_Value
            // 
            this.cBox_Value.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBox_Value.FormattingEnabled = true;
            this.cBox_Value.Location = new System.Drawing.Point(14, 43);
            this.cBox_Value.Name = "cBox_Value";
            this.cBox_Value.Size = new System.Drawing.Size(100, 20);
            this.cBox_Value.TabIndex = 1;
            this.cBox_Value.SelectedIndexChanged += new System.EventHandler(this.cBox_Value_SelectedIndexChanged);
            // 
            // ButtonOk
            // 
            this.ButtonOk.Location = new System.Drawing.Point(334, 156);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(75, 23);
            this.ButtonOk.TabIndex = 4;
            this.ButtonOk.Text = "确定";
            this.ButtonOk.UseVisualStyleBackColor = true;
            this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(431, 156);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 5;
            this.ButtonCancel.Text = "取消";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // ButtonClear
            // 
            this.ButtonClear.Location = new System.Drawing.Point(12, 156);
            this.ButtonClear.Name = "ButtonClear";
            this.ButtonClear.Size = new System.Drawing.Size(75, 23);
            this.ButtonClear.TabIndex = 6;
            this.ButtonClear.Text = "清除";
            this.ButtonClear.UseVisualStyleBackColor = true;
            this.ButtonClear.Click += new System.EventHandler(this.ButtonClear_Click);
            // 
            // ConditionFormatDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 195);
            this.Controls.Add(this.ButtonClear);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConditionFormatDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "条件格式";
            this.Load += new System.EventHandler(this.ConditionFormatDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cBox_Value;
        private System.Windows.Forms.Button ButtonFormatDialog;
        private System.Windows.Forms.Label lb_Example;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Button ButtonCancel;
        private ExpressionControl expressionControl1;
        private System.Windows.Forms.Button ButtonClear;
        private System.Windows.Forms.Label label1;
    }
}