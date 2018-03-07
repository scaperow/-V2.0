namespace BizModules
{
    partial class ReadWriteFunctionDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReadWriteFunctionDialog));
            this.Button_Exit = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.WriteFuncPage = new System.Windows.Forms.TabPage();
            this.ReadFuncPage = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button_Exit
            // 
            this.Button_Exit.Location = new System.Drawing.Point(553, 540);
            this.Button_Exit.Name = "Button_Exit";
            this.Button_Exit.Size = new System.Drawing.Size(79, 23);
            this.Button_Exit.TabIndex = 1;
            this.Button_Exit.Text = "关闭";
            this.Button_Exit.UseVisualStyleBackColor = true;
            this.Button_Exit.Click += new System.EventHandler(this.Button_Exit_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "表单.png");
            this.imageList1.Images.SetKeyName(1, "函数.png");
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.WriteFuncPage);
            this.tabControl1.Controls.Add(this.ReadFuncPage);
            this.tabControl1.Location = new System.Drawing.Point(9, 9);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(712, 522);
            this.tabControl1.TabIndex = 6;
            // 
            // WriteFuncPage
            // 
            this.WriteFuncPage.Location = new System.Drawing.Point(4, 22);
            this.WriteFuncPage.Name = "WriteFuncPage";
            this.WriteFuncPage.Size = new System.Drawing.Size(575, 457);
            this.WriteFuncPage.TabIndex = 0;
            this.WriteFuncPage.Text = "写数函数";
            this.WriteFuncPage.UseVisualStyleBackColor = true;
            // 
            // ReadFuncPage
            // 
            this.ReadFuncPage.Location = new System.Drawing.Point(4, 22);
            this.ReadFuncPage.Name = "ReadFuncPage";
            this.ReadFuncPage.Size = new System.Drawing.Size(704, 496);
            this.ReadFuncPage.TabIndex = 1;
            this.ReadFuncPage.Text = "读数函数";
            this.ReadFuncPage.UseVisualStyleBackColor = true;
            // 
            // ReadWriteFunctionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 573);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.Button_Exit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReadWriteFunctionDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "配置函数";
            this.Load += new System.EventHandler(this.ReadWriteFunctionDialog_Load);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Button_Exit;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage WriteFuncPage;
        private System.Windows.Forms.TabPage ReadFuncPage;
    }
}