namespace BizComponents
{
    partial class CellSelector
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
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Button_Ok = new System.Windows.Forms.Button();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Location = new System.Drawing.Point(660, 535);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 4;
            this.Button_Cancel.Text = "取消";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Button_Ok
            // 
            this.Button_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Button_Ok.Location = new System.Drawing.Point(572, 535);
            this.Button_Ok.Name = "Button_Ok";
            this.Button_Ok.Size = new System.Drawing.Size(75, 23);
            this.Button_Ok.TabIndex = 3;
            this.Button_Ok.Text = "确定";
            this.Button_Ok.UseVisualStyleBackColor = true;
            this.Button_Ok.Click += new System.EventHandler(this.Button_Ok_Click);
            // 
            // TabPage1
            // 
            this.TabPage1.Controls.Add(this.fpSpread1);
            this.TabPage1.Controls.Add(this.label3);
            this.TabPage1.Controls.Add(this.label2);
            this.TabPage1.Controls.Add(this.label1);
            this.TabPage1.Location = new System.Drawing.Point(4, 22);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(5);
            this.TabPage1.Size = new System.Drawing.Size(784, 488);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "模板数据项";
            this.TabPage1.UseVisualStyleBackColor = true;
            // 
            // fpSpread1
            // 
            this.fpSpread1.AccessibleDescription = "";
            this.fpSpread1.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Top;
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.Location = new System.Drawing.Point(5, 5);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.AsNeeded;
            this.fpSpread1.Size = new System.Drawing.Size(774, 457);
            this.fpSpread1.TabIndex = 8;
            this.fpSpread1.TabStripInsertTab = false;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(166, 469);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "颜色的为数据项。";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.LightPink;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(121, 469);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "     ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(9, 469);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "选择说明：背景为";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.TabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(6, 8);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(792, 514);
            this.tabControl1.TabIndex = 8;
            // 
            // CellSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 569);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_Ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CellSelector";
            this.Padding = new System.Windows.Forms.Padding(6, 8, 6, 0);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择数据项";
            this.Load += new System.EventHandler(this.DataItemSelector_Load);
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Button Button_Ok;
        private System.Windows.Forms.TabPage TabPage1;
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
    }
}