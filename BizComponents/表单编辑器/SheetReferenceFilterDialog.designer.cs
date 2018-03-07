namespace BizComponents
{
    partial class SheetReferenceFilterDialog
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
            this.Button_Exit = new System.Windows.Forms.Button();
            this.SheetfilterControl = new BizComponents.FilterControl();
            this.SuspendLayout();
            // 
            // Button_Exit
            // 
            this.Button_Exit.Location = new System.Drawing.Point(478, 407);
            this.Button_Exit.Name = "Button_Exit";
            this.Button_Exit.Size = new System.Drawing.Size(75, 23);
            this.Button_Exit.TabIndex = 1;
            this.Button_Exit.Text = "关  闭";
            this.Button_Exit.UseVisualStyleBackColor = true;
            this.Button_Exit.Click += new System.EventHandler(this.Button_Exit_Click);
            // 
            // SheetfilterControl
            // 
            this.SheetfilterControl.Location = new System.Drawing.Point(9, 7);
            this.SheetfilterControl.Margin = new System.Windows.Forms.Padding(0);
            this.SheetfilterControl.Name = "SheetfilterControl";
            this.SheetfilterControl.Size = new System.Drawing.Size(583, 392);
            this.SheetfilterControl.TabIndex = 2;
            // 
            // SheetReferenceFilterDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 439);
            this.Controls.Add(this.SheetfilterControl);
            this.Controls.Add(this.Button_Exit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SheetReferenceFilterDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置表单筛选条件";
            this.Load += new System.EventHandler(this.ReferenceFilterDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Button_Exit;
        private FilterControl SheetfilterControl;
    }
}