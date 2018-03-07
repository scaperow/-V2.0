namespace BizModules
{
    partial class ModelCatlogContent
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
            this.tControl_Model = new System.Windows.Forms.TabControl();
            this.tPage_Model = new System.Windows.Forms.TabPage();
            this.tPage_Sheet = new System.Windows.Forms.TabPage();
            this.tControl_Model.SuspendLayout();
            this.SuspendLayout();
            // 
            // tControl_Model
            // 
            this.tControl_Model.Controls.Add(this.tPage_Model);
            this.tControl_Model.Controls.Add(this.tPage_Sheet);
            this.tControl_Model.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tControl_Model.Location = new System.Drawing.Point(0, 0);
            this.tControl_Model.Name = "tControl_Model";
            this.tControl_Model.SelectedIndex = 0;
            this.tControl_Model.Size = new System.Drawing.Size(295, 515);
            this.tControl_Model.TabIndex = 1;
            // 
            // tPage_Model
            // 
            this.tPage_Model.Location = new System.Drawing.Point(4, 21);
            this.tPage_Model.Name = "tPage_Model";
            this.tPage_Model.Padding = new System.Windows.Forms.Padding(3);
            this.tPage_Model.Size = new System.Drawing.Size(287, 490);
            this.tPage_Model.TabIndex = 0;
            this.tPage_Model.Text = "试验模板";
            this.tPage_Model.UseVisualStyleBackColor = true;
            // 
            // tPage_Sheet
            // 
            this.tPage_Sheet.Location = new System.Drawing.Point(4, 21);
            this.tPage_Sheet.Name = "tPage_Sheet";
            this.tPage_Sheet.Padding = new System.Windows.Forms.Padding(3);
            this.tPage_Sheet.Size = new System.Drawing.Size(287, 490);
            this.tPage_Sheet.TabIndex = 1;
            this.tPage_Sheet.Text = "试验表单";
            this.tPage_Sheet.UseVisualStyleBackColor = true;
            // 
            // ModelCatlogContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 515);
            this.Controls.Add(this.tControl_Model);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HideOnClose = true;
            this.Name = "ModelCatlogContent";
            this.Text = "模板列表";
            this.Load += new System.EventHandler(this.ModelCatlogContent_Load);
            this.tControl_Model.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tControl_Model;
        private System.Windows.Forms.TabPage tPage_Model;
        private System.Windows.Forms.TabPage tPage_Sheet;
    }
}