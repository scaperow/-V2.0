namespace BizComponents.查询统计
{
    partial class CheckQueryControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckQueryControl));
            this.SOperation = new System.Windows.Forms.ComboBox();
            this.FieldText = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.DeleteButton = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // SOperation
            // 
            this.SOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SOperation.FormattingEnabled = true;
            this.SOperation.Location = new System.Drawing.Point(83, 3);
            this.SOperation.Name = "SOperation";
            this.SOperation.Size = new System.Drawing.Size(99, 20);
            this.SOperation.TabIndex = 13;
            // 
            // FieldText
            // 
            this.FieldText.BackColor = System.Drawing.SystemColors.Control;
            this.FieldText.Location = new System.Drawing.Point(2, 3);
            this.FieldText.Name = "FieldText";
            this.FieldText.Size = new System.Drawing.Size(75, 21);
            this.FieldText.TabIndex = 12;
            this.FieldText.Text = "字段名称";
            this.FieldText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            this.imageList1.Images.SetKeyName(0, "Delete.bmp");
            // 
            // DeleteButton
            // 
            this.DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DeleteButton.ImageList = this.imageList1;
            this.DeleteButton.Location = new System.Drawing.Point(221, 3);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 21);
            this.DeleteButton.TabIndex = 15;
            this.DeleteButton.Text = "清除";
            this.DeleteButton.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(191, 7);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 16;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // CheckQueryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.SOperation);
            this.Controls.Add(this.FieldText);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.Name = "CheckQueryControl";
            this.Size = new System.Drawing.Size(300, 27);
            this.Resize += new System.EventHandler(this.CheckQueryControl_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox SOperation;
        private System.Windows.Forms.Label FieldText;
        private System.Windows.Forms.ImageList imageList1;
        public System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}
