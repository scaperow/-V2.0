namespace ReportComponents
{
    partial class ReportDesignForm
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
            this.ReportDesignPanel = new ReportComponents.ReportDesignUI();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ParameterButton = new System.Windows.Forms.ToolStripButton();
            this.ParameterUIButton = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ReportDesignPanel
            // 
            this.ReportDesignPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportDesignPanel.Location = new System.Drawing.Point(0, 25);
            this.ReportDesignPanel.Name = "ReportDesignPanel";
            this.ReportDesignPanel.Size = new System.Drawing.Size(645, 452);
            this.ReportDesignPanel.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ParameterButton,
            this.ParameterUIButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(645, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ParameterButton
            // 
            this.ParameterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ParameterButton.Enabled = false;
            this.ParameterButton.Image = global::ReportComponents.Properties.Resources.报表参数;
            this.ParameterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ParameterButton.Name = "ParameterButton";
            this.ParameterButton.Size = new System.Drawing.Size(23, 22);
            this.ParameterButton.Text = "报表参数";
            this.ParameterButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // ParameterUIButton
            // 
            this.ParameterUIButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ParameterUIButton.Enabled = false;
            this.ParameterUIButton.Image = global::ReportComponents.Properties.Resources.参数界面;
            this.ParameterUIButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ParameterUIButton.Name = "ParameterUIButton";
            this.ParameterUIButton.Size = new System.Drawing.Size(23, 22);
            this.ParameterUIButton.Text = "参数界面";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // ReportDesignForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 477);
            this.Controls.Add(this.ReportDesignPanel);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HideOnClose = true;
            this.Name = "ReportDesignForm";
            this.Text = "报表设计器";
            this.Load += new System.EventHandler(this.ReportDesignForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal ReportDesignUI ReportDesignPanel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton ParameterButton;
        private System.Windows.Forms.ToolStripButton ParameterUIButton;
    }
}