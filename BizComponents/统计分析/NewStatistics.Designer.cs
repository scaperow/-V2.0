namespace BizComponents
{
    partial class NewStatistics
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
            this.components = new System.ComponentModel.Container();
            this.LabelTip = new System.Windows.Forms.Label();
            this.ButtonOK = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.LabelWeight = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.RadioMaterial = new System.Windows.Forms.RadioButton();
            this.RadioConcrete = new System.Windows.Forms.RadioButton();
            this.Errors = new System.Windows.Forms.ErrorProvider(this.components);
            this.TextItemName = new System.Windows.Forms.TextBox();
            this.TextWeight = new System.Windows.Forms.TextBox();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Errors)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelTip
            // 
            this.LabelTip.AutoSize = true;
            this.LabelTip.Location = new System.Drawing.Point(12, 24);
            this.LabelTip.Name = "LabelTip";
            this.LabelTip.Size = new System.Drawing.Size(65, 12);
            this.LabelTip.TabIndex = 1;
            this.LabelTip.Text = "统计项名称";
            // 
            // ButtonOK
            // 
            this.ButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButtonOK.Location = new System.Drawing.Point(113, 255);
            this.ButtonOK.Name = "ButtonOK";
            this.ButtonOK.Size = new System.Drawing.Size(75, 23);
            this.ButtonOK.TabIndex = 2;
            this.ButtonOK.Text = "确定";
            this.ButtonOK.UseVisualStyleBackColor = true;
            this.ButtonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Location = new System.Drawing.Point(194, 255);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 2;
            this.ButtonCancel.Text = "取消";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // LabelWeight
            // 
            this.LabelWeight.AutoSize = true;
            this.LabelWeight.Location = new System.Drawing.Point(12, 84);
            this.LabelWeight.Name = "LabelWeight";
            this.LabelWeight.Size = new System.Drawing.Size(29, 12);
            this.LabelWeight.TabIndex = 4;
            this.LabelWeight.Text = "权重";
            this.LabelWeight.Click += new System.EventHandler(this.LabelWeight_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "类型";
            this.label1.Click += new System.EventHandler(this.LabelWeight_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.RadioMaterial);
            this.panel3.Controls.Add(this.RadioConcrete);
            this.panel3.Location = new System.Drawing.Point(149, 141);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(120, 22);
            this.panel3.TabIndex = 21;
            // 
            // RadioMaterial
            // 
            this.RadioMaterial.AutoSize = true;
            this.RadioMaterial.Checked = true;
            this.RadioMaterial.Location = new System.Drawing.Point(4, 3);
            this.RadioMaterial.Name = "RadioMaterial";
            this.RadioMaterial.Size = new System.Drawing.Size(47, 16);
            this.RadioMaterial.TabIndex = 18;
            this.RadioMaterial.TabStop = true;
            this.RadioMaterial.Text = "原材";
            this.RadioMaterial.UseVisualStyleBackColor = true;
            // 
            // RadioConcrete
            // 
            this.RadioConcrete.AutoSize = true;
            this.RadioConcrete.Location = new System.Drawing.Point(57, 3);
            this.RadioConcrete.Name = "RadioConcrete";
            this.RadioConcrete.Size = new System.Drawing.Size(59, 16);
            this.RadioConcrete.TabIndex = 18;
            this.RadioConcrete.Text = "混凝土";
            this.RadioConcrete.UseVisualStyleBackColor = true;
            // 
            // Errors
            // 
            this.Errors.ContainerControl = this;
            // 
            // TextItemName
            // 
            this.TextItemName.Location = new System.Drawing.Point(12, 39);
            this.TextItemName.Name = "TextItemName";
            this.TextItemName.Size = new System.Drawing.Size(257, 21);
            this.TextItemName.TabIndex = 22;
            this.TextItemName.Validating += new System.ComponentModel.CancelEventHandler(this.TextItemName_Validating);
            // 
            // TextWeight
            // 
            this.TextWeight.Location = new System.Drawing.Point(14, 99);
            this.TextWeight.Name = "TextWeight";
            this.TextWeight.Size = new System.Drawing.Size(255, 21);
            this.TextWeight.TabIndex = 23;
            this.TextWeight.Validating += new System.ComponentModel.CancelEventHandler(this.TextWeight_Validating);
            // 
            // NewStatistics
            // 
            this.AcceptButton = this.ButtonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.CancelButton = this.ButtonCancel;
            this.ClientSize = new System.Drawing.Size(281, 290);
            this.Controls.Add(this.TextWeight);
            this.Controls.Add(this.TextItemName);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LabelWeight);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonOK);
            this.Controls.Add(this.LabelTip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "NewStatistics";
            this.Text = "新建统计项";
            this.Load += new System.EventHandler(this.Input_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Errors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelTip;
        private System.Windows.Forms.Button ButtonOK;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Label LabelWeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton RadioMaterial;
        private System.Windows.Forms.RadioButton RadioConcrete;
        private System.Windows.Forms.ErrorProvider Errors;
        private System.Windows.Forms.TextBox TextItemName;
        private System.Windows.Forms.TextBox TextWeight;
    }
}