namespace ReportComponents
{
    partial class TableDataFilterDialog
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
            this.filterControl = new ReportComponents.SimpleFilterControl();
            this.SuspendLayout();
            // 
            // Button_Exit
            // 
            this.Button_Exit.Location = new System.Drawing.Point(577, 515);
            this.Button_Exit.Name = "Button_Exit";
            this.Button_Exit.Size = new System.Drawing.Size(75, 23);
            this.Button_Exit.TabIndex = 2;
            this.Button_Exit.Text = "关  闭";
            this.Button_Exit.UseVisualStyleBackColor = true;
            this.Button_Exit.Click += new System.EventHandler(this.Button_Exit_Click);
            // 
            // filterControl
            // 
            this.filterControl.Location = new System.Drawing.Point(7, 2);
            this.filterControl.Name = "filterControl";
            this.filterControl.Size = new System.Drawing.Size(688, 505);
            this.filterControl.TabIndex = 0;
            // 
            // TableDataFilterDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 545);
            this.Controls.Add(this.Button_Exit);
            this.Controls.Add(this.filterControl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TableDataFilterDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "条件编辑窗口";
            this.Load += new System.EventHandler(this.DataSourceFilterDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ReportComponents.SimpleFilterControl filterControl;
        private System.Windows.Forms.Button Button_Exit;
    }
}