namespace BizComponents
{
    partial class TemperatureDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemperatureDialog));
            this.panel1 = new System.Windows.Forms.Panel();
            this.FpSpread = new Yqun.Client.BizUI.MyCell();
            this.FpSpread_Info = new FarPoint.Win.Spread.SheetView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TotalCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.SetPage = new System.Windows.Forms.ToolStripButton();
            this.PreviewButton = new System.Windows.Forms.ToolStripButton();
            this.ButtonPrint = new System.Windows.Forms.ToolStripButton();
            this.label8 = new System.Windows.Forms.Label();
            this.Button_Save = new System.Windows.Forms.Button();
            this.Button_Exit = new System.Windows.Forms.Button();
            this.lblTemperatureSum = new System.Windows.Forms.Label();
            this.DateTimePicker_End = new System.Windows.Forms.DateTimePicker();
            this.ComboTemperature = new System.Windows.Forms.ComboBox();
            this.Button_Query = new System.Windows.Forms.Button();
            this.LinkCustom = new System.Windows.Forms.LinkLabel();
            this.DateTimePicker_Start = new System.Windows.Forms.DateTimePicker();
            this.LineRename = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.LinkDelete = new System.Windows.Forms.LinkLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread_Info)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.FpSpread);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(200, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(726, 436);
            this.panel1.TabIndex = 16;
            // 
            // FpSpread
            // 
            this.FpSpread.AccessibleDescription = "FpSpread, SheetInfo, Row 0, Column 0, ";
            this.FpSpread.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FpSpread.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.FpSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FpSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.FpSpread.IsEditing = false;
            this.FpSpread.Location = new System.Drawing.Point(0, 25);
            this.FpSpread.Name = "FpSpread";
            this.FpSpread.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.FpSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FpSpread_Info});
            this.FpSpread.Size = new System.Drawing.Size(726, 411);
            this.FpSpread.TabIndex = 0;
            this.FpSpread.TabStripInsertTab = false;
            this.FpSpread.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            this.FpSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.FpSpread.Watermark = null;
            this.FpSpread.EditChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.FpSpread_EditChange);
            // 
            // FpSpread_Info
            // 
            this.FpSpread_Info.Reset();
            this.FpSpread_Info.SheetName = "SheetInfo";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FpSpread_Info.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FpSpread_Info.ColumnCount = 0;
            this.FpSpread_Info.RowCount = 1;
            this.FpSpread_Info.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.FpSpread_Info.PrintInfo.PaperSize = ((System.Drawing.Printing.PaperSize)(resources.GetObject("resource.PaperSize")));
            this.FpSpread_Info.RowHeader.Visible = false;
            this.FpSpread_Info.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.FpSpread.SetActiveViewport(0, 0, 1);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TotalCount,
            this.toolStripLabel1,
            this.SetPage,
            this.PreviewButton,
            this.ButtonPrint});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(726, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // TotalCount
            // 
            this.TotalCount.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TotalCount.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TotalCount.Name = "TotalCount";
            this.TotalCount.Size = new System.Drawing.Size(79, 22);
            this.TotalCount.Text = "共 {0} 条记录";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Margin = new System.Windows.Forms.Padding(10, 1, 0, 2);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(80, 22);
            this.toolStripLabel1.Text = "温度试验记录";
            // 
            // SetPage
            // 
            this.SetPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SetPage.Image = global::BizComponents.Properties.Resources.页面设置;
            this.SetPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SetPage.Name = "SetPage";
            this.SetPage.Size = new System.Drawing.Size(23, 22);
            this.SetPage.Text = "页面设置";
            this.SetPage.Click += new System.EventHandler(this.SetPage_Click);
            // 
            // PreviewButton
            // 
            this.PreviewButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PreviewButton.Image = global::BizComponents.Properties.Resources.打印预览;
            this.PreviewButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PreviewButton.Name = "PreviewButton";
            this.PreviewButton.Size = new System.Drawing.Size(23, 22);
            this.PreviewButton.Text = "打印预览";
            this.PreviewButton.Click += new System.EventHandler(this.PreviewButton_Click);
            // 
            // ButtonPrint
            // 
            this.ButtonPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonPrint.Image = global::BizComponents.Properties.Resources.打印;
            this.ButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonPrint.Name = "ButtonPrint";
            this.ButtonPrint.Size = new System.Drawing.Size(23, 22);
            this.ButtonPrint.Text = "打印";
            this.ButtonPrint.Click += new System.EventHandler(this.ButtonPrint_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 100);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 24;
            this.label8.Text = "记录日期(&R):";
            // 
            // Button_Save
            // 
            this.Button_Save.Location = new System.Drawing.Point(5, 202);
            this.Button_Save.Name = "Button_Save";
            this.Button_Save.Size = new System.Drawing.Size(175, 27);
            this.Button_Save.TabIndex = 18;
            this.Button_Save.Text = "保存";
            this.Button_Save.UseVisualStyleBackColor = true;
            this.Button_Save.Click += new System.EventHandler(this.Button_Save_Click);
            // 
            // Button_Exit
            // 
            this.Button_Exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button_Exit.Location = new System.Drawing.Point(5, 406);
            this.Button_Exit.Name = "Button_Exit";
            this.Button_Exit.Size = new System.Drawing.Size(175, 27);
            this.Button_Exit.TabIndex = 18;
            this.Button_Exit.Text = "关闭";
            this.Button_Exit.UseVisualStyleBackColor = true;
            this.Button_Exit.Click += new System.EventHandler(this.Button_Exit_Click);
            // 
            // lblTemperatureSum
            // 
            this.lblTemperatureSum.AutoSize = true;
            this.lblTemperatureSum.Location = new System.Drawing.Point(3, 232);
            this.lblTemperatureSum.Name = "lblTemperatureSum";
            this.lblTemperatureSum.Size = new System.Drawing.Size(71, 12);
            this.lblTemperatureSum.TabIndex = 24;
            this.lblTemperatureSum.Text = "总温度：0℃";
            // 
            // DateTimePicker_End
            // 
            this.DateTimePicker_End.Location = new System.Drawing.Point(5, 142);
            this.DateTimePicker_End.Name = "DateTimePicker_End";
            this.DateTimePicker_End.Size = new System.Drawing.Size(175, 21);
            this.DateTimePicker_End.TabIndex = 16;
            // 
            // ComboTemperature
            // 
            this.ComboTemperature.DisplayMember = "Name";
            this.ComboTemperature.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboTemperature.FormattingEnabled = true;
            this.ComboTemperature.Location = new System.Drawing.Point(5, 28);
            this.ComboTemperature.Name = "ComboTemperature";
            this.ComboTemperature.Size = new System.Drawing.Size(175, 20);
            this.ComboTemperature.TabIndex = 25;
            this.ComboTemperature.ValueMember = "ID";
            this.ComboTemperature.SelectedIndexChanged += new System.EventHandler(this.ComboTemperature_SelectedIndexChanged);
            // 
            // Button_Query
            // 
            this.Button_Query.Location = new System.Drawing.Point(5, 169);
            this.Button_Query.Name = "Button_Query";
            this.Button_Query.Size = new System.Drawing.Size(175, 27);
            this.Button_Query.TabIndex = 18;
            this.Button_Query.Text = "查询";
            this.Button_Query.UseVisualStyleBackColor = true;
            this.Button_Query.Click += new System.EventHandler(this.Button_Query_Click);
            // 
            // LinkCustom
            // 
            this.LinkCustom.AutoSize = true;
            this.LinkCustom.Location = new System.Drawing.Point(3, 51);
            this.LinkCustom.Name = "LinkCustom";
            this.LinkCustom.Size = new System.Drawing.Size(41, 12);
            this.LinkCustom.TabIndex = 26;
            this.LinkCustom.TabStop = true;
            this.LinkCustom.Text = "自定义";
            this.LinkCustom.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkCustom_LinkClicked);
            // 
            // DateTimePicker_Start
            // 
            this.DateTimePicker_Start.Location = new System.Drawing.Point(5, 115);
            this.DateTimePicker_Start.Name = "DateTimePicker_Start";
            this.DateTimePicker_Start.Size = new System.Drawing.Size(175, 21);
            this.DateTimePicker_Start.TabIndex = 17;
            // 
            // LineRename
            // 
            this.LineRename.AutoSize = true;
            this.LineRename.Location = new System.Drawing.Point(50, 51);
            this.LineRename.Name = "LineRename";
            this.LineRename.Size = new System.Drawing.Size(41, 12);
            this.LineRename.TabIndex = 26;
            this.LineRename.TabStop = true;
            this.LineRename.Text = "重命名";
            this.LineRename.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LineRename_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 24;
            this.label1.Text = "温度类型(&T)";
            // 
            // LinkDelete
            // 
            this.LinkDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LinkDelete.AutoSize = true;
            this.LinkDelete.LinkColor = System.Drawing.Color.Red;
            this.LinkDelete.Location = new System.Drawing.Point(151, 51);
            this.LinkDelete.Name = "LinkDelete";
            this.LinkDelete.Size = new System.Drawing.Size(29, 12);
            this.LinkDelete.TabIndex = 26;
            this.LinkDelete.TabStop = true;
            this.LinkDelete.Text = "删除";
            this.LinkDelete.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkDelete_LinkClicked);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.LinkDelete);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.LineRename);
            this.panel2.Controls.Add(this.DateTimePicker_Start);
            this.panel2.Controls.Add(this.LinkCustom);
            this.panel2.Controls.Add(this.Button_Query);
            this.panel2.Controls.Add(this.ComboTemperature);
            this.panel2.Controls.Add(this.DateTimePicker_End);
            this.panel2.Controls.Add(this.lblTemperatureSum);
            this.panel2.Controls.Add(this.Button_Exit);
            this.panel2.Controls.Add(this.Button_Save);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(6, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(194, 436);
            this.panel2.TabIndex = 15;
            // 
            // TemperatureDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 448);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TemperatureDialog";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "试验温度记录";
            this.Load += new System.EventHandler(this.TemperatureDialog_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread_Info)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public Yqun.Client.BizUI.MyCell FpSpread;
        public FarPoint.Win.Spread.SheetView FpSpread_Info;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel TotalCount;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton ButtonPrint;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.LinkLabel LinkDelete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel LineRename;
        private System.Windows.Forms.DateTimePicker DateTimePicker_Start;
        private System.Windows.Forms.LinkLabel LinkCustom;
        private System.Windows.Forms.Button Button_Query;
        private System.Windows.Forms.ComboBox ComboTemperature;
        private System.Windows.Forms.DateTimePicker DateTimePicker_End;
        private System.Windows.Forms.Label lblTemperatureSum;
        private System.Windows.Forms.Button Button_Exit;
        private System.Windows.Forms.Button Button_Save;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStripButton SetPage;
        private System.Windows.Forms.ToolStripButton PreviewButton;

    }
}