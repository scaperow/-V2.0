namespace Kingrocket.CJ.Components
{
    partial class StaidumInfoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaidumInfoForm));
            this.button_OK = new System.Windows.Forms.Button();
            this.treeView_Select = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(234, 267);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 1;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // treeView_Select
            // 
            this.treeView_Select.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView_Select.CheckBoxes = true;
            this.treeView_Select.Location = new System.Drawing.Point(1, 2);
            this.treeView_Select.Name = "treeView_Select";
            this.treeView_Select.Size = new System.Drawing.Size(308, 255);
            this.treeView_Select.TabIndex = 3;
            this.treeView_Select.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView_Select_AfterCheck);
            // 
            // StaidumInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 300);
            this.Controls.Add(this.treeView_Select);
            this.Controls.Add(this.button_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StaidumInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选取就绪试验编号";
            this.Load += new System.EventHandler(this.ShowReadyTestForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StaidumInfoForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.TreeView treeView_Select;

    }
}