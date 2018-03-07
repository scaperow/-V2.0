namespace ReportComponents
{
    partial class DataFieldSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataFieldSelector));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.FpSpread = new FarPoint.Win.Spread.FpSpread();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ModelView = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.CheckBox_ModelIndex = new System.Windows.Forms.CheckBox();
            this.CheckBox_ModelName = new System.Windows.Forms.CheckBox();
            this.CheckBox_SCTS = new System.Windows.Forms.CheckBox();
            this.CheckBox_SCPT = new System.Windows.Forms.CheckBox();
            this.CheckBox_SCCT = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread)).BeginInit();
            this.tabControl2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(303, 9);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(615, 581);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.FpSpread);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(5);
            this.tabPage1.Size = new System.Drawing.Size(607, 555);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "模板数据项";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // FpSpread
            // 
            this.FpSpread.AccessibleDescription = "";
            this.FpSpread.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FpSpread.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.FpSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.FpSpread.Location = new System.Drawing.Point(4, 5);
            this.FpSpread.Name = "FpSpread";
            this.FpSpread.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.FpSpread.Size = new System.Drawing.Size(597, 520);
            this.FpSpread.TabIndex = 8;
            this.FpSpread.TabStripInsertTab = false;
            this.FpSpread.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Always;
            this.FpSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(166, 533);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "颜色的为数据项。";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(121, 533);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "     ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(9, 533);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "选择说明：背景为";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ButtonOk
            // 
            this.ButtonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonOk.Location = new System.Drawing.Point(724, 596);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(75, 23);
            this.ButtonOk.TabIndex = 10;
            this.ButtonOk.Text = "确定";
            this.ButtonOk.UseVisualStyleBackColor = true;
            this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonCancel.Location = new System.Drawing.Point(805, 596);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 11;
            this.ButtonCancel.Text = "取消";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // tabControl2
            // 
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Location = new System.Drawing.Point(7, 9);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(290, 581);
            this.tabControl2.TabIndex = 12;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ModelView);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(282, 555);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "模板列表";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ModelView
            // 
            this.ModelView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ModelView.FullRowSelect = true;
            this.ModelView.HideSelection = false;
            this.ModelView.ImageIndex = 0;
            this.ModelView.ImageList = this.imageList1;
            this.ModelView.Location = new System.Drawing.Point(3, 3);
            this.ModelView.Name = "ModelView";
            this.ModelView.SelectedImageIndex = 0;
            this.ModelView.Size = new System.Drawing.Size(276, 549);
            this.ModelView.TabIndex = 0;
            this.ModelView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.ModelView_NodeMouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            this.imageList1.Images.SetKeyName(0, "关闭文件夹.bmp");
            this.imageList1.Images.SetKeyName(1, "打开文件夹.bmp");
            this.imageList1.Images.SetKeyName(2, "表单.bmp");
            // 
            // CheckBox_ModelIndex
            // 
            this.CheckBox_ModelIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckBox_ModelIndex.AutoSize = true;
            this.CheckBox_ModelIndex.Checked = true;
            this.CheckBox_ModelIndex.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBox_ModelIndex.Location = new System.Drawing.Point(14, 596);
            this.CheckBox_ModelIndex.Name = "CheckBox_ModelIndex";
            this.CheckBox_ModelIndex.Size = new System.Drawing.Size(60, 16);
            this.CheckBox_ModelIndex.TabIndex = 13;
            this.CheckBox_ModelIndex.Text = "模板ID";
            this.CheckBox_ModelIndex.UseVisualStyleBackColor = true;
            // 
            // CheckBox_ModelName
            // 
            this.CheckBox_ModelName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckBox_ModelName.AutoSize = true;
            this.CheckBox_ModelName.Checked = true;
            this.CheckBox_ModelName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBox_ModelName.Location = new System.Drawing.Point(77, 596);
            this.CheckBox_ModelName.Name = "CheckBox_ModelName";
            this.CheckBox_ModelName.Size = new System.Drawing.Size(72, 16);
            this.CheckBox_ModelName.TabIndex = 14;
            this.CheckBox_ModelName.Text = "模板名称";
            this.CheckBox_ModelName.UseVisualStyleBackColor = true;
            // 
            // CheckBox_SCTS
            // 
            this.CheckBox_SCTS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckBox_SCTS.AutoSize = true;
            this.CheckBox_SCTS.Checked = true;
            this.CheckBox_SCTS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBox_SCTS.Location = new System.Drawing.Point(151, 596);
            this.CheckBox_SCTS.Name = "CheckBox_SCTS";
            this.CheckBox_SCTS.Size = new System.Drawing.Size(60, 16);
            this.CheckBox_SCTS.TabIndex = 15;
            this.CheckBox_SCTS.Text = "时间戳";
            this.CheckBox_SCTS.UseVisualStyleBackColor = true;
            // 
            // CheckBox_SCPT
            // 
            this.CheckBox_SCPT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckBox_SCPT.AutoSize = true;
            this.CheckBox_SCPT.Checked = true;
            this.CheckBox_SCPT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBox_SCPT.Location = new System.Drawing.Point(215, 596);
            this.CheckBox_SCPT.Name = "CheckBox_SCPT";
            this.CheckBox_SCPT.Size = new System.Drawing.Size(48, 16);
            this.CheckBox_SCPT.TabIndex = 16;
            this.CheckBox_SCPT.Text = "SCPT";
            this.CheckBox_SCPT.UseVisualStyleBackColor = true;
            // 
            // CheckBox_SCCT
            // 
            this.CheckBox_SCCT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckBox_SCCT.AutoSize = true;
            this.CheckBox_SCCT.Checked = true;
            this.CheckBox_SCCT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBox_SCCT.Location = new System.Drawing.Point(269, 596);
            this.CheckBox_SCCT.Name = "CheckBox_SCCT";
            this.CheckBox_SCCT.Size = new System.Drawing.Size(48, 16);
            this.CheckBox_SCCT.TabIndex = 17;
            this.CheckBox_SCCT.Text = "SCCT";
            this.CheckBox_SCCT.UseVisualStyleBackColor = true;
            // 
            // DataFieldSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 627);
            this.Controls.Add(this.CheckBox_SCCT);
            this.Controls.Add(this.CheckBox_SCPT);
            this.Controls.Add(this.CheckBox_SCTS);
            this.Controls.Add(this.CheckBox_ModelName);
            this.Controls.Add(this.CheckBox_ModelIndex);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataFieldSelector";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "选择模板字段";
            this.Load += new System.EventHandler(this.DataFieldSelector_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread)).EndInit();
            this.tabControl2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Button ButtonCancel;
        private FarPoint.Win.Spread.FpSpread FpSpread;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TreeView ModelView;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.CheckBox CheckBox_ModelIndex;
        private System.Windows.Forms.CheckBox CheckBox_ModelName;
        private System.Windows.Forms.CheckBox CheckBox_SCTS;
        private System.Windows.Forms.CheckBox CheckBox_SCPT;
        private System.Windows.Forms.CheckBox CheckBox_SCCT;
    }
}