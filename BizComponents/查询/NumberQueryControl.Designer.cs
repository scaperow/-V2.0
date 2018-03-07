namespace BizComponents
{
    partial class NumberQueryControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NumberQueryControl));
            this.FieldText = new System.Windows.Forms.Label();
            this.STextBox = new System.Windows.Forms.TextBox();
            this.ETextBox = new System.Windows.Forms.TextBox();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // FieldText
            // 
            this.FieldText.BackColor = System.Drawing.SystemColors.Control;
            this.FieldText.Location = new System.Drawing.Point(1, 1);
            this.FieldText.Name = "FieldText";
            this.FieldText.Size = new System.Drawing.Size(80, 51);
            this.FieldText.TabIndex = 0;
            this.FieldText.Text = "字段名称";
            // 
            // STextBox
            // 
            this.STextBox.Location = new System.Drawing.Point(86, 3);
            this.STextBox.Name = "STextBox";
            this.STextBox.Size = new System.Drawing.Size(128, 21);
            this.STextBox.TabIndex = 11;
            // 
            // ETextBox
            // 
            this.ETextBox.Location = new System.Drawing.Point(86, 27);
            this.ETextBox.Name = "ETextBox";
            this.ETextBox.Size = new System.Drawing.Size(128, 21);
            this.ETextBox.TabIndex = 12;
            // 
            // DeleteButton
            // 
            this.DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DeleteButton.ImageList = this.imageList1;
            this.DeleteButton.Location = new System.Drawing.Point(221, 3);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 21);
            this.DeleteButton.TabIndex = 13;
            this.DeleteButton.Text = "清除";
            this.DeleteButton.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            this.imageList1.Images.SetKeyName(0, "Delete.bmp");
            // 
            // NumberQueryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.ETextBox);
            this.Controls.Add(this.STextBox);
            this.Controls.Add(this.FieldText);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.Name = "NumberQueryControl";
            this.Size = new System.Drawing.Size(300, 52);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label FieldText;
        private System.Windows.Forms.TextBox STextBox;
        private System.Windows.Forms.TextBox ETextBox;
        private System.Windows.Forms.ImageList imageList1;
        public System.Windows.Forms.Button DeleteButton;
    }
}
