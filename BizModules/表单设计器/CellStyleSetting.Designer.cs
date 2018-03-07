namespace BizModules
{
    partial class CellStyleSetting
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
            this.ButtonExit = new System.Windows.Forms.Button();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonExit
            // 
            this.ButtonExit.Location = new System.Drawing.Point(399, 447);
            this.ButtonExit.Name = "ButtonExit";
            this.ButtonExit.Size = new System.Drawing.Size(75, 23);
            this.ButtonExit.TabIndex = 4;
            this.ButtonExit.Text = "关闭";
            this.ButtonExit.UseVisualStyleBackColor = true;
            this.ButtonExit.Click += new System.EventHandler(this.ButtonExit_Click);
            // 
            // ButtonSave
            // 
            this.ButtonSave.Location = new System.Drawing.Point(318, 447);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(75, 23);
            this.ButtonSave.TabIndex = 4;
            this.ButtonSave.Text = "保存";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.propertyGrid1);
            this.panel1.Location = new System.Drawing.Point(13, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(510, 423);
            this.panel1.TabIndex = 3;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(508, 421);
            this.propertyGrid1.TabIndex = 0;
            // 
            // CellStyleSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 480);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.ButtonExit);
            this.Controls.Add(this.panel1);
            this.Name = "CellStyleSetting";
            this.Text = "设置单元格样式";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButtonExit;
        private System.Windows.Forms.Button ButtonSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;

    }
}