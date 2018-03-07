namespace BizModules
{
    partial class QualificationAuthForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.Modeltree = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.button_Save = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Modeltree);
            this.panel1.Location = new System.Drawing.Point(10, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(611, 523);
            this.panel1.TabIndex = 0;
            // 
            // Modeltree
            // 
            this.Modeltree.CheckBoxes = true;
            this.Modeltree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Modeltree.Location = new System.Drawing.Point(0, 0);
            this.Modeltree.Name = "Modeltree";
            this.Modeltree.Size = new System.Drawing.Size(611, 523);
            this.Modeltree.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(9, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "当前授权试验室";
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(419, 567);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(75, 23);
            this.button_Save.TabIndex = 2;
            this.button_Save.Text = "保存";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(503, 567);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 3;
            this.button_Cancel.Text = "取消";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // QualificationAuthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 600);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QualificationAuthForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "试验室资质授权";
            this.Load += new System.EventHandler(this.QualificationAuthForm_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView Modeltree;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Button button_Cancel;
    }
}