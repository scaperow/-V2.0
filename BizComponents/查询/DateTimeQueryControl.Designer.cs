namespace BizComponents
{
    partial class DateTimeQueryControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DateTimeQueryControl));
            this.FieldText = new System.Windows.Forms.Label();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.startdateTime = new System.Windows.Forms.DateTimePicker();
            this.enddateTime = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // FieldText
            // 
            this.FieldText.BackColor = System.Drawing.SystemColors.Control;
            this.FieldText.Location = new System.Drawing.Point(1, 1);
            this.FieldText.Name = "FieldText";
            this.FieldText.Size = new System.Drawing.Size(80, 52);
            this.FieldText.TabIndex = 0;
            this.FieldText.Text = "字段名称";
            // 
            // DeleteButton
            // 
            this.DeleteButton.BackColor = System.Drawing.SystemColors.Control;
            this.DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DeleteButton.ImageList = this.imageList1;
            this.DeleteButton.Location = new System.Drawing.Point(221, 3);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 21);
            this.DeleteButton.TabIndex = 6;
            this.DeleteButton.Text = "清除";
            this.DeleteButton.UseVisualStyleBackColor = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            this.imageList1.Images.SetKeyName(0, "Delete.bmp");
            // 
            // startdateTime
            // 
            this.startdateTime.Location = new System.Drawing.Point(86, 3);
            this.startdateTime.Name = "startdateTime";
            this.startdateTime.Size = new System.Drawing.Size(125, 21);
            this.startdateTime.TabIndex = 9;
            // 
            // enddateTime
            // 
            this.enddateTime.Location = new System.Drawing.Point(86, 28);
            this.enddateTime.Name = "enddateTime";
            this.enddateTime.Size = new System.Drawing.Size(125, 21);
            this.enddateTime.TabIndex = 10;
            // 
            // DateTimeQueryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.enddateTime);
            this.Controls.Add(this.startdateTime);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.FieldText);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.Name = "DateTimeQueryControl";
            this.Size = new System.Drawing.Size(300, 53);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label FieldText;
        private System.Windows.Forms.ImageList imageList1;
        public System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.DateTimePicker startdateTime;
        private System.Windows.Forms.DateTimePicker enddateTime;
    }
}
