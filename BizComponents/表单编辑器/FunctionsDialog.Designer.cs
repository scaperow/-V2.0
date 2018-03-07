namespace BizComponents
{
    partial class FunctionsDialog
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
            this.Button_Validate = new System.Windows.Forms.Button();
            this.tBox_Expression = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lBox_FunctionType = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lBox_FunctionName = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lBox_SheetName = new System.Windows.Forms.ListBox();
            this.label_FunctionInfo = new System.Windows.Forms.Label();
            this.Button_Ok = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Button_Validate);
            this.groupBox1.Controls.Add(this.tBox_Expression);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(7, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(596, 185);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // Button_Validate
            // 
            this.Button_Validate.Location = new System.Drawing.Point(18, 156);
            this.Button_Validate.Name = "Button_Validate";
            this.Button_Validate.Size = new System.Drawing.Size(75, 23);
            this.Button_Validate.TabIndex = 1;
            this.Button_Validate.Text = "检查合法性";
            this.Button_Validate.UseVisualStyleBackColor = true;
            // 
            // tBox_Expression
            // 
            this.tBox_Expression.Location = new System.Drawing.Point(18, 29);
            this.tBox_Expression.Multiline = true;
            this.tBox_Expression.Name = "tBox_Expression";
            this.tBox_Expression.Size = new System.Drawing.Size(574, 122);
            this.tBox_Expression.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "请在下面的文本框里面输入公式:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "=";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "函数类型:";
            // 
            // lBox_FunctionType
            // 
            this.lBox_FunctionType.FormattingEnabled = true;
            this.lBox_FunctionType.ItemHeight = 12;
            this.lBox_FunctionType.Location = new System.Drawing.Point(7, 205);
            this.lBox_FunctionType.Name = "lBox_FunctionType";
            this.lBox_FunctionType.Size = new System.Drawing.Size(145, 160);
            this.lBox_FunctionType.TabIndex = 2;
            this.lBox_FunctionType.SelectedIndexChanged += new System.EventHandler(this.lBox_FunctionType_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(157, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "函数名:";
            // 
            // lBox_FunctionName
            // 
            this.lBox_FunctionName.FormattingEnabled = true;
            this.lBox_FunctionName.ItemHeight = 12;
            this.lBox_FunctionName.Location = new System.Drawing.Point(158, 205);
            this.lBox_FunctionName.Name = "lBox_FunctionName";
            this.lBox_FunctionName.Size = new System.Drawing.Size(181, 160);
            this.lBox_FunctionName.TabIndex = 4;
            this.lBox_FunctionName.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lBox_FunctionName_MouseDoubleClick);
            this.lBox_FunctionName.SelectedIndexChanged += new System.EventHandler(this.lBox_FunctionName_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(343, 190);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "表单名称:";
            // 
            // lBox_SheetName
            // 
            this.lBox_SheetName.FormattingEnabled = true;
            this.lBox_SheetName.ItemHeight = 12;
            this.lBox_SheetName.Location = new System.Drawing.Point(345, 205);
            this.lBox_SheetName.Name = "lBox_SheetName";
            this.lBox_SheetName.Size = new System.Drawing.Size(258, 160);
            this.lBox_SheetName.TabIndex = 6;
            this.lBox_SheetName.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lBox_SheetName_MouseDoubleClick);
            // 
            // label_FunctionInfo
            // 
            this.label_FunctionInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_FunctionInfo.Location = new System.Drawing.Point(7, 370);
            this.label_FunctionInfo.Name = "label_FunctionInfo";
            this.label_FunctionInfo.Size = new System.Drawing.Size(596, 76);
            this.label_FunctionInfo.TabIndex = 17;
            // 
            // Button_Ok
            // 
            this.Button_Ok.Location = new System.Drawing.Point(431, 460);
            this.Button_Ok.Name = "Button_Ok";
            this.Button_Ok.Size = new System.Drawing.Size(75, 23);
            this.Button_Ok.TabIndex = 18;
            this.Button_Ok.Text = "确定";
            this.Button_Ok.UseVisualStyleBackColor = true;
            this.Button_Ok.Click += new System.EventHandler(this.Button_Ok_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Location = new System.Drawing.Point(517, 460);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 19;
            this.Button_Cancel.Text = "取消";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // FunctionsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 493);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_Ok);
            this.Controls.Add(this.label_FunctionInfo);
            this.Controls.Add(this.lBox_SheetName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lBox_FunctionName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lBox_FunctionType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FunctionsDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "编辑公式";
            this.Load += new System.EventHandler(this.FunctionsDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tBox_Expression;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Button_Validate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lBox_FunctionType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lBox_FunctionName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox lBox_SheetName;
        private System.Windows.Forms.Label label_FunctionInfo;
        private System.Windows.Forms.Button Button_Ok;
        private System.Windows.Forms.Button Button_Cancel;
    }
}