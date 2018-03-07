namespace BizModules
{
    partial class SupervisionReportViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SupervisionReportViewer));
            this.myCell1 = new Yqun.Client.BizUI.MyCell();
            this.myCell1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ExportExcelButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.PrintButton = new System.Windows.Forms.ToolStripButton();
            this.PrintPreviewButton = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.myCell1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myCell1_Sheet1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // myCell1
            // 
            this.myCell1.AccessibleDescription = "";
            this.myCell1.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.myCell1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myCell1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.myCell1.Location = new System.Drawing.Point(0, 25);
            this.myCell1.Name = "myCell1";
            this.myCell1.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.myCell1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.myCell1_Sheet1});
            this.myCell1.Size = new System.Drawing.Size(785, 547);
            this.myCell1.TabIndex = 0;
            this.myCell1.TabStripInsertTab = false;
            this.myCell1.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            this.myCell1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // myCell1_Sheet1
            // 
            this.myCell1_Sheet1.Reset();
            this.myCell1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.myCell1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.myCell1_Sheet1.ColumnCount = 3;
            this.myCell1_Sheet1.RowCount = 0;
            this.myCell1_Sheet1.PrintInfo.PaperSize = ((System.Drawing.Printing.PaperSize)(resources.GetObject("resource.PaperSize")));
            this.myCell1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.myCell1.SetActiveViewport(0, 1, 0);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExportExcelButton,
            this.toolStripSeparator1,
            this.PrintButton,
            this.PrintPreviewButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(785, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ExportExcelButton
            // 
            this.ExportExcelButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ExportExcelButton.Image = global::BizModules.Properties.Resources.导出Excel;
            this.ExportExcelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ExportExcelButton.Name = "ExportExcelButton";
            this.ExportExcelButton.Size = new System.Drawing.Size(23, 22);
            this.ExportExcelButton.Text = "导出Excel";
            this.ExportExcelButton.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // PrintButton
            // 
            this.PrintButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PrintButton.Image = global::BizModules.Properties.Resources.打印;
            this.PrintButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.Size = new System.Drawing.Size(23, 22);
            this.PrintButton.Text = "打印";
            this.PrintButton.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // PrintPreviewButton
            // 
            this.PrintPreviewButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PrintPreviewButton.Image = global::BizModules.Properties.Resources.打印预览;
            this.PrintPreviewButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PrintPreviewButton.Name = "PrintPreviewButton";
            this.PrintPreviewButton.Size = new System.Drawing.Size(23, 22);
            this.PrintPreviewButton.Text = "打印预览";
            this.PrintPreviewButton.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // SupervisionReportViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 572);
            this.Controls.Add(this.myCell1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "SupervisionReportViewer";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "监理平行见证统计报表";
            this.Load += new System.EventHandler(this.SupervisionReportViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.myCell1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myCell1_Sheet1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Yqun.Client.BizUI.MyCell myCell1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private FarPoint.Win.Spread.SheetView myCell1_Sheet1;
        private System.Windows.Forms.ToolStripButton ExportExcelButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton PrintButton;
        private System.Windows.Forms.ToolStripButton PrintPreviewButton;

    }
}