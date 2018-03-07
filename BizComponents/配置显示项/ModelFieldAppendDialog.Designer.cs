namespace BizComponents
{
    partial class ModelFieldAppendDialog
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
            this.cBox_IsEdit = new System.Windows.Forms.CheckBox();
            this.cBox_IsVisible = new System.Windows.Forms.CheckBox();
            this.cBox_IsNull = new System.Windows.Forms.CheckBox();
            this.tBox_Description = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rButton_UseData = new System.Windows.Forms.RadioButton();
            this.Button_Ok = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.gBox_DisplayContent = new System.Windows.Forms.GroupBox();
            this.label_DataItemInfo = new System.Windows.Forms.Label();
            this.Button_Cell = new System.Windows.Forms.Button();
            this.Button_SettingFormula = new System.Windows.Forms.Button();
            this.label_FormulaInfo = new System.Windows.Forms.Label();
            this.rButton_UseFormula = new System.Windows.Forms.RadioButton();
            this.gBox_Other = new System.Windows.Forms.GroupBox();
            this.cBox_IsSystem = new System.Windows.Forms.CheckBox();
            this.gBox_DisplayContent.SuspendLayout();
            this.gBox_Other.SuspendLayout();
            this.SuspendLayout();
            // 
            // cBox_IsEdit
            // 
            this.cBox_IsEdit.AutoSize = true;
            this.cBox_IsEdit.Enabled = false;
            this.cBox_IsEdit.Location = new System.Drawing.Point(103, 21);
            this.cBox_IsEdit.Name = "cBox_IsEdit";
            this.cBox_IsEdit.Size = new System.Drawing.Size(84, 16);
            this.cBox_IsEdit.TabIndex = 3;
            this.cBox_IsEdit.Text = "是否可编辑";
            this.cBox_IsEdit.UseVisualStyleBackColor = true;
            // 
            // cBox_IsVisible
            // 
            this.cBox_IsVisible.AutoSize = true;
            this.cBox_IsVisible.Location = new System.Drawing.Point(18, 21);
            this.cBox_IsVisible.Name = "cBox_IsVisible";
            this.cBox_IsVisible.Size = new System.Drawing.Size(72, 16);
            this.cBox_IsVisible.TabIndex = 4;
            this.cBox_IsVisible.Text = "是否显示";
            this.cBox_IsVisible.UseVisualStyleBackColor = true;
            // 
            // cBox_IsNull
            // 
            this.cBox_IsNull.AutoSize = true;
            this.cBox_IsNull.Location = new System.Drawing.Point(200, 21);
            this.cBox_IsNull.Name = "cBox_IsNull";
            this.cBox_IsNull.Size = new System.Drawing.Size(96, 16);
            this.cBox_IsNull.TabIndex = 5;
            this.cBox_IsNull.Text = "是否允许为空";
            this.cBox_IsNull.UseVisualStyleBackColor = true;
            // 
            // tBox_Description
            // 
            this.tBox_Description.Location = new System.Drawing.Point(12, 28);
            this.tBox_Description.Name = "tBox_Description";
            this.tBox_Description.Size = new System.Drawing.Size(553, 21);
            this.tBox_Description.TabIndex = 6;
            this.tBox_Description.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "描述信息：";
            // 
            // rButton_UseData
            // 
            this.rButton_UseData.AutoSize = true;
            this.rButton_UseData.Location = new System.Drawing.Point(18, 23);
            this.rButton_UseData.Name = "rButton_UseData";
            this.rButton_UseData.Size = new System.Drawing.Size(107, 16);
            this.rButton_UseData.TabIndex = 1;
            this.rButton_UseData.TabStop = true;
            this.rButton_UseData.Text = "使用模板数据项";
            this.rButton_UseData.UseVisualStyleBackColor = true;
            // 
            // Button_Ok
            // 
            this.Button_Ok.Location = new System.Drawing.Point(374, 324);
            this.Button_Ok.Name = "Button_Ok";
            this.Button_Ok.Size = new System.Drawing.Size(75, 23);
            this.Button_Ok.TabIndex = 11;
            this.Button_Ok.Text = "确定";
            this.Button_Ok.UseVisualStyleBackColor = true;
            this.Button_Ok.Click += new System.EventHandler(this.Button_Ok_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Location = new System.Drawing.Point(464, 323);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 12;
            this.Button_Cancel.Text = "取消";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // gBox_DisplayContent
            // 
            this.gBox_DisplayContent.Controls.Add(this.label_DataItemInfo);
            this.gBox_DisplayContent.Controls.Add(this.Button_Cell);
            this.gBox_DisplayContent.Controls.Add(this.Button_SettingFormula);
            this.gBox_DisplayContent.Controls.Add(this.label_FormulaInfo);
            this.gBox_DisplayContent.Controls.Add(this.rButton_UseData);
            this.gBox_DisplayContent.Controls.Add(this.rButton_UseFormula);
            this.gBox_DisplayContent.Location = new System.Drawing.Point(12, 57);
            this.gBox_DisplayContent.Name = "gBox_DisplayContent";
            this.gBox_DisplayContent.Size = new System.Drawing.Size(553, 190);
            this.gBox_DisplayContent.TabIndex = 14;
            this.gBox_DisplayContent.TabStop = false;
            this.gBox_DisplayContent.Text = "显示内容";
            // 
            // label_DataItemInfo
            // 
            this.label_DataItemInfo.BackColor = System.Drawing.Color.White;
            this.label_DataItemInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_DataItemInfo.Enabled = false;
            this.label_DataItemInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_DataItemInfo.Location = new System.Drawing.Point(40, 40);
            this.label_DataItemInfo.Name = "label_DataItemInfo";
            this.label_DataItemInfo.Size = new System.Drawing.Size(343, 20);
            this.label_DataItemInfo.TabIndex = 16;
            this.label_DataItemInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Button_Cell
            // 
            this.Button_Cell.Location = new System.Drawing.Point(389, 38);
            this.Button_Cell.Name = "Button_Cell";
            this.Button_Cell.Size = new System.Drawing.Size(75, 23);
            this.Button_Cell.TabIndex = 16;
            this.Button_Cell.Text = "选择...";
            this.Button_Cell.UseVisualStyleBackColor = true;
            this.Button_Cell.Click += new System.EventHandler(this.Button_Cell_Click);
            // 
            // Button_SettingFormula
            // 
            this.Button_SettingFormula.Enabled = false;
            this.Button_SettingFormula.Location = new System.Drawing.Point(389, 83);
            this.Button_SettingFormula.Name = "Button_SettingFormula";
            this.Button_SettingFormula.Size = new System.Drawing.Size(75, 23);
            this.Button_SettingFormula.TabIndex = 5;
            this.Button_SettingFormula.Text = "设置公式";
            this.Button_SettingFormula.UseVisualStyleBackColor = true;
            this.Button_SettingFormula.Visible = false;
            // 
            // label_FormulaInfo
            // 
            this.label_FormulaInfo.BackColor = System.Drawing.Color.White;
            this.label_FormulaInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_FormulaInfo.Enabled = false;
            this.label_FormulaInfo.Location = new System.Drawing.Point(40, 84);
            this.label_FormulaInfo.Name = "label_FormulaInfo";
            this.label_FormulaInfo.Size = new System.Drawing.Size(343, 21);
            this.label_FormulaInfo.TabIndex = 4;
            this.label_FormulaInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_FormulaInfo.Visible = false;
            // 
            // rButton_UseFormula
            // 
            this.rButton_UseFormula.AutoSize = true;
            this.rButton_UseFormula.Location = new System.Drawing.Point(18, 67);
            this.rButton_UseFormula.Name = "rButton_UseFormula";
            this.rButton_UseFormula.Size = new System.Drawing.Size(71, 16);
            this.rButton_UseFormula.TabIndex = 3;
            this.rButton_UseFormula.TabStop = true;
            this.rButton_UseFormula.Text = "使用公式";
            this.rButton_UseFormula.UseVisualStyleBackColor = true;
            this.rButton_UseFormula.Visible = false;
            // 
            // gBox_Other
            // 
            this.gBox_Other.Controls.Add(this.cBox_IsSystem);
            this.gBox_Other.Controls.Add(this.cBox_IsEdit);
            this.gBox_Other.Controls.Add(this.cBox_IsNull);
            this.gBox_Other.Controls.Add(this.cBox_IsVisible);
            this.gBox_Other.Location = new System.Drawing.Point(12, 254);
            this.gBox_Other.Name = "gBox_Other";
            this.gBox_Other.Size = new System.Drawing.Size(553, 50);
            this.gBox_Other.TabIndex = 15;
            this.gBox_Other.TabStop = false;
            this.gBox_Other.Text = "其他选项";
            // 
            // cBox_IsSystem
            // 
            this.cBox_IsSystem.AutoSize = true;
            this.cBox_IsSystem.Enabled = false;
            this.cBox_IsSystem.Location = new System.Drawing.Point(308, 21);
            this.cBox_IsSystem.Name = "cBox_IsSystem";
            this.cBox_IsSystem.Size = new System.Drawing.Size(108, 16);
            this.cBox_IsSystem.TabIndex = 7;
            this.cBox_IsSystem.Text = "是否系统显示项";
            this.cBox_IsSystem.UseVisualStyleBackColor = true;
            // 
            // ModelFieldAppendDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 365);
            this.Controls.Add(this.gBox_Other);
            this.Controls.Add(this.gBox_DisplayContent);
            this.Controls.Add(this.tBox_Description);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_Ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModelFieldAppendDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加显示项";
            this.Load += new System.EventHandler(this.ModelFieldAppendDialog_Load);
            this.gBox_DisplayContent.ResumeLayout(false);
            this.gBox_DisplayContent.PerformLayout();
            this.gBox_Other.ResumeLayout(false);
            this.gBox_Other.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cBox_IsEdit;
        private System.Windows.Forms.CheckBox cBox_IsVisible;
        private System.Windows.Forms.CheckBox cBox_IsNull;
        private System.Windows.Forms.TextBox tBox_Description;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rButton_UseData;
        private System.Windows.Forms.Button Button_Ok;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.GroupBox gBox_DisplayContent;
        private System.Windows.Forms.Label label_FormulaInfo;
        private System.Windows.Forms.Button Button_SettingFormula;
        private System.Windows.Forms.GroupBox gBox_Other;
        private System.Windows.Forms.CheckBox cBox_IsSystem;
        private System.Windows.Forms.Button Button_Cell;
        private System.Windows.Forms.Label label_DataItemInfo;
        private System.Windows.Forms.RadioButton rButton_UseFormula;
    }
}