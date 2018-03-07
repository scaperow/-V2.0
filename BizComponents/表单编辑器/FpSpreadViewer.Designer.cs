using Yqun.Client.BizUI;
namespace BizComponents
{
    partial class FpSpreadViewer
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FpSpreadViewer));
            this.fpSpread = new Yqun.Client.BizUI.MyCell();
            this.fpSpread_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ExportExcelButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.CutButton = new System.Windows.Forms.ToolStripButton();
            this.CopyButton = new System.Windows.Forms.ToolStripButton();
            this.PasteButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.PageSettingButton = new System.Windows.Forms.ToolStripButton();
            this.PrintButton = new System.Windows.Forms.ToolStripButton();
            this.PrintAllButton = new System.Windows.Forms.ToolStripButton();
            this.PrintPreviewButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.UndoButton = new System.Windows.Forms.ToolStripButton();
            this.RedoButton = new System.Windows.Forms.ToolStripButton();
            this.SymbolBar = new System.Windows.Forms.ToolStrip();
            this.FormatToolStrip = new System.Windows.Forms.ToolStrip();
            this.SpecialCharButton = new System.Windows.Forms.ToolStripButton();
            this.ExportPDFButton = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread_Sheet1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.FormatToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // fpSpread
            // 
            this.fpSpread.AccessibleDescription = "";
            this.fpSpread.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread.Location = new System.Drawing.Point(0, 50);
            this.fpSpread.Name = "fpSpread";
            this.fpSpread.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread_Sheet1});
            this.fpSpread.Size = new System.Drawing.Size(658, 419);
            this.fpSpread.TabIndex = 0;
            this.fpSpread.TabStripInsertTab = false;
            this.fpSpread.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Always;
            this.fpSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread.LeaveCell += new FarPoint.Win.Spread.LeaveCellEventHandler(this.fpSpread_LeaveCell);
            this.fpSpread.EnterCell += new FarPoint.Win.Spread.EnterCellEventHandler(this.fpSpread_EnterCell);
            // 
            // fpSpread_Sheet1
            // 
            this.fpSpread_Sheet1.Reset();
            this.fpSpread_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread_Sheet1.ColumnCount = 0;
            this.fpSpread_Sheet1.RowCount = 0;
            this.fpSpread_Sheet1.PrintInfo.PaperSize = ((System.Drawing.Printing.PaperSize)(resources.GetObject("resource.PaperSize")));
            this.fpSpread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread.SetActiveViewport(0, 1, 1);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExportExcelButton,
            this.ExportPDFButton,
            this.toolStripSeparator1,
            this.CutButton,
            this.CopyButton,
            this.PasteButton,
            this.toolStripSeparator2,
            this.PageSettingButton,
            this.PrintButton,
            this.PrintAllButton,
            this.PrintPreviewButton,
            this.toolStripSeparator3,
            this.UndoButton,
            this.RedoButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(658, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ExportExcelButton
            // 
            this.ExportExcelButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ExportExcelButton.Image = global::BizComponents.Properties.Resources.导出Excel;
            this.ExportExcelButton.ImageTransparentColor = System.Drawing.Color.White;
            this.ExportExcelButton.Name = "ExportExcelButton";
            this.ExportExcelButton.Size = new System.Drawing.Size(23, 22);
            this.ExportExcelButton.Text = "导出为Excel";
            this.ExportExcelButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // CutButton
            // 
            this.CutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CutButton.Image = global::BizComponents.Properties.Resources.剪切;
            this.CutButton.ImageTransparentColor = System.Drawing.Color.White;
            this.CutButton.Name = "CutButton";
            this.CutButton.Size = new System.Drawing.Size(23, 22);
            this.CutButton.Text = "剪切";
            this.CutButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // CopyButton
            // 
            this.CopyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CopyButton.Image = global::BizComponents.Properties.Resources.复制;
            this.CopyButton.ImageTransparentColor = System.Drawing.Color.White;
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(23, 22);
            this.CopyButton.Text = "复制";
            this.CopyButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // PasteButton
            // 
            this.PasteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PasteButton.Image = global::BizComponents.Properties.Resources.粘贴;
            this.PasteButton.ImageTransparentColor = System.Drawing.Color.White;
            this.PasteButton.Name = "PasteButton";
            this.PasteButton.Size = new System.Drawing.Size(23, 22);
            this.PasteButton.Text = "粘贴";
            this.PasteButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // PageSettingButton
            // 
            this.PageSettingButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PageSettingButton.Image = global::BizComponents.Properties.Resources.页面设置;
            this.PageSettingButton.ImageTransparentColor = System.Drawing.Color.White;
            this.PageSettingButton.Name = "PageSettingButton";
            this.PageSettingButton.Size = new System.Drawing.Size(23, 22);
            this.PageSettingButton.Text = "页面设置";
            this.PageSettingButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // PrintButton
            // 
            this.PrintButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PrintButton.Image = global::BizComponents.Properties.Resources.打印;
            this.PrintButton.ImageTransparentColor = System.Drawing.Color.White;
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.Size = new System.Drawing.Size(23, 22);
            this.PrintButton.Text = "打印";
            this.PrintButton.ToolTipText = "打印(Ctrl+P)";
            this.PrintButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // PrintAllButton
            // 
            this.PrintAllButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PrintAllButton.Image = global::BizComponents.Properties.Resources.打印全部;
            this.PrintAllButton.ImageTransparentColor = System.Drawing.Color.White;
            this.PrintAllButton.Name = "PrintAllButton";
            this.PrintAllButton.Size = new System.Drawing.Size(23, 22);
            this.PrintAllButton.Text = "打印全部";
            this.PrintAllButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // PrintPreviewButton
            // 
            this.PrintPreviewButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PrintPreviewButton.Image = global::BizComponents.Properties.Resources.打印预览;
            this.PrintPreviewButton.ImageTransparentColor = System.Drawing.Color.White;
            this.PrintPreviewButton.Name = "PrintPreviewButton";
            this.PrintPreviewButton.Size = new System.Drawing.Size(23, 22);
            this.PrintPreviewButton.Text = "打印预览";
            this.PrintPreviewButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // UndoButton
            // 
            this.UndoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UndoButton.Image = global::BizComponents.Properties.Resources.撤销;
            this.UndoButton.ImageTransparentColor = System.Drawing.Color.White;
            this.UndoButton.Name = "UndoButton";
            this.UndoButton.Size = new System.Drawing.Size(23, 22);
            this.UndoButton.Text = "撤销";
            this.UndoButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // RedoButton
            // 
            this.RedoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RedoButton.Image = global::BizComponents.Properties.Resources.重复;
            this.RedoButton.ImageTransparentColor = System.Drawing.Color.White;
            this.RedoButton.Name = "RedoButton";
            this.RedoButton.Size = new System.Drawing.Size(23, 22);
            this.RedoButton.Text = "重复";
            this.RedoButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // SymbolBar
            // 
            this.SymbolBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SymbolBar.Location = new System.Drawing.Point(0, 469);
            this.SymbolBar.Name = "SymbolBar";
            this.SymbolBar.ShowItemToolTips = false;
            this.SymbolBar.Size = new System.Drawing.Size(658, 25);
            this.SymbolBar.TabIndex = 2;
            this.SymbolBar.Text = "toolStrip2";
            // 
            // FormatToolStrip
            // 
            this.FormatToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SpecialCharButton});
            this.FormatToolStrip.Location = new System.Drawing.Point(0, 25);
            this.FormatToolStrip.Name = "FormatToolStrip";
            this.FormatToolStrip.Size = new System.Drawing.Size(658, 25);
            this.FormatToolStrip.TabIndex = 3;
            this.FormatToolStrip.Text = "toolStrip2";
            // 
            // SpecialCharButton
            // 
            this.SpecialCharButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SpecialCharButton.Image = global::BizComponents.Properties.Resources.特殊符号;
            this.SpecialCharButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SpecialCharButton.Name = "SpecialCharButton";
            this.SpecialCharButton.Size = new System.Drawing.Size(23, 22);
            this.SpecialCharButton.Text = "特殊符号";
            this.SpecialCharButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // ExportPDFButton
            // 
            this.ExportPDFButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ExportPDFButton.Image = ((System.Drawing.Image)(resources.GetObject("ExportPDFButton.Image")));
            this.ExportPDFButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ExportPDFButton.Name = "ExportPDFButton";
            this.ExportPDFButton.Size = new System.Drawing.Size(70, 22);
            this.ExportPDFButton.Text = "导出到PDF";
            this.ExportPDFButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // FpSpreadViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fpSpread);
            this.Controls.Add(this.FormatToolStrip);
            this.Controls.Add(this.SymbolBar);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FpSpreadViewer";
            this.Size = new System.Drawing.Size(658, 494);
            this.Load += new System.EventHandler(this.FpSpreadViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread_Sheet1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.FormatToolStrip.ResumeLayout(false);
            this.FormatToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyCell fpSpread;
        private FarPoint.Win.Spread.SheetView fpSpread_Sheet1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton CutButton;
        private System.Windows.Forms.ToolStripButton CopyButton;
        private System.Windows.Forms.ToolStripButton PasteButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton PageSettingButton;
        private System.Windows.Forms.ToolStripButton PrintButton;
        private System.Windows.Forms.ToolStripButton PrintPreviewButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton UndoButton;
        private System.Windows.Forms.ToolStripButton RedoButton;
        private System.Windows.Forms.ToolStripButton PrintAllButton;
        private System.Windows.Forms.ToolStrip SymbolBar;
        private System.Windows.Forms.ToolStrip FormatToolStrip;
        private System.Windows.Forms.ToolStripButton SpecialCharButton;
        private System.Windows.Forms.ToolStripButton ExportExcelButton;
        private System.Windows.Forms.ToolStripButton ExportPDFButton;

    }
}
