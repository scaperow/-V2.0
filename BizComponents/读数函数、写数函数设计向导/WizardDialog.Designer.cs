namespace BizComponents
{
    partial class WizardDialog
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
            this.MyWizard = new IS.DNS.WinUI.Wizard.Wizard();
            this.SuspendLayout();
            // 
            // MyWizard
            // 
            this.MyWizard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MyWizard.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MyWizard.Location = new System.Drawing.Point(0, 0);
            this.MyWizard.Name = "MyWizard";
            this.MyWizard.Size = new System.Drawing.Size(633, 496);
            this.MyWizard.TabIndex = 0;
            // 
            // WizardDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 496);
            this.Controls.Add(this.MyWizard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WizardDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "向导对话框";
            this.ResumeLayout(false);

        }

        #endregion

        protected IS.DNS.WinUI.Wizard.Wizard MyWizard;

    }
}