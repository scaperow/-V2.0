namespace BizComponents
{
    partial class SheetReferenceDialog
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
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Button_Ok = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tableFlow1 = new Yqun.Client.BizUI.TableFlow();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Button_Append = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("宋体", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(9, 517);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(422, 24);
            this.label5.TabIndex = 9;
            this.label5.Text = "说明：通过为参照表单设置筛选条件以便筛选出符合用户要求的数据项。";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(11, 543);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(746, 9);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            // 
            // Button_Ok
            // 
            this.Button_Ok.AutoSize = true;
            this.Button_Ok.Location = new System.Drawing.Point(544, 558);
            this.Button_Ok.Name = "Button_Ok";
            this.Button_Ok.Size = new System.Drawing.Size(75, 27);
            this.Button_Ok.TabIndex = 11;
            this.Button_Ok.Text = "确定";
            this.Button_Ok.UseVisualStyleBackColor = true;
            this.Button_Ok.Click += new System.EventHandler(this.Button_Ok_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.AutoSize = true;
            this.Button_Cancel.Location = new System.Drawing.Point(637, 558);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 27);
            this.Button_Cancel.TabIndex = 12;
            this.Button_Cancel.Text = "取消";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(443, 523);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "点击此处设置";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // tableFlow1
            // 
            this.tableFlow1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableFlow1.FieldHeight = 24;
            this.tableFlow1.Location = new System.Drawing.Point(9, 29);
            this.tableFlow1.Name = "tableFlow1";
            this.tableFlow1.Size = new System.Drawing.Size(746, 482);
            this.tableFlow1.TabIndex = 3;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Button_Append});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(764, 25);
            this.toolStrip1.TabIndex = 15;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Button_Append
            // 
            this.Button_Append.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Button_Append.Image = global::BizComponents.Properties.Resources.导入参照表;
            this.Button_Append.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_Append.Name = "Button_Append";
            this.Button_Append.Size = new System.Drawing.Size(23, 22);
            this.Button_Append.Text = "选择参照表单";
            this.Button_Append.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // SheetReferenceDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 594);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_Ok);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tableFlow1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SheetReferenceDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "表单参照编辑器";
            this.Load += new System.EventHandler(this.SheetReferenceDialog_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Yqun.Client.BizUI.TableFlow tableFlow1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button Button_Ok;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton Button_Append;
    }
}