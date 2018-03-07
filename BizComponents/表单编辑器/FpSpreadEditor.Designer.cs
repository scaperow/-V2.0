using Yqun.Client.BizUI;
namespace BizComponents
{
    partial class FpSpreadEditor
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
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FpSpreadEditor));
            this.CommonTools = new System.Windows.Forms.ToolStrip();
            this.ExportExcelButton = new System.Windows.Forms.ToolStripButton();
            this.ShearButton = new System.Windows.Forms.ToolStripButton();
            this.CopyButton = new System.Windows.Forms.ToolStripButton();
            this.PasteButton = new System.Windows.Forms.ToolStripButton();
            this.PageSettingButton = new System.Windows.Forms.ToolStripButton();
            this.PrintButton = new System.Windows.Forms.ToolStripButton();
            this.PrintPreviewButton = new System.Windows.Forms.ToolStripButton();
            this.fpSpread = new Yqun.Client.BizUI.MyCell();
            this.fpSpread_Sheet2 = new FarPoint.Win.Spread.SheetView();
            this.FunctionStrip = new System.Windows.Forms.ToolStrip();
            this.tTextBox_XY = new System.Windows.Forms.ToolStripTextBox();
            this.FormulaCancelButton = new System.Windows.Forms.ToolStripButton();
            this.FormulaOkButton = new System.Windows.Forms.ToolStripButton();
            this.FormulaButton2 = new System.Windows.Forms.ToolStripButton();
            this.UndoButton = new System.Windows.Forms.ToolStripButton();
            this.RedoButton = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.CommonTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread_Sheet2)).BeginInit();
            this.FunctionStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator11
            // 
            toolStripSeparator11.Name = "toolStripSeparator11";
            toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
            // 
            // CommonTools
            // 
            this.CommonTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExportExcelButton,
            toolStripSeparator1,
            this.ShearButton,
            this.CopyButton,
            this.PasteButton,
            toolStripSeparator2,
            this.PageSettingButton,
            this.PrintButton,
            this.PrintPreviewButton,
            toolStripSeparator11,
            this.UndoButton,
            this.RedoButton});
            this.CommonTools.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.CommonTools.Location = new System.Drawing.Point(0, 0);
            this.CommonTools.Name = "CommonTools";
            this.CommonTools.Size = new System.Drawing.Size(671, 25);
            this.CommonTools.TabIndex = 0;
            this.CommonTools.Text = "常用工具";
            // 
            // ExportExcelButton
            // 
            this.ExportExcelButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ExportExcelButton.Image = global::BizComponents.Properties.Resources.导出Excel;
            this.ExportExcelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ExportExcelButton.Name = "ExportExcelButton";
            this.ExportExcelButton.Size = new System.Drawing.Size(23, 22);
            this.ExportExcelButton.Text = "导出Excel文件";
            this.ExportExcelButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // ShearButton
            // 
            this.ShearButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ShearButton.Image = global::BizComponents.Properties.Resources.剪切;
            this.ShearButton.ImageTransparentColor = System.Drawing.Color.White;
            this.ShearButton.Name = "ShearButton";
            this.ShearButton.Size = new System.Drawing.Size(23, 22);
            this.ShearButton.Text = "剪切";
            this.ShearButton.Click += new System.EventHandler(this.ToolStripButton_Click);
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
            this.PrintButton.Click += new System.EventHandler(this.ToolStripButton_Click);
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
            // fpSpread
            // 
            this.fpSpread.AccessibleDescription = "";
            this.fpSpread.BackColor = System.Drawing.SystemColors.Control;
            this.fpSpread.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread.IsEditing = false;
            this.fpSpread.Location = new System.Drawing.Point(0, 50);
            this.fpSpread.Name = "fpSpread";
            this.fpSpread.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread_Sheet2});
            this.fpSpread.Size = new System.Drawing.Size(671, 457);
            this.fpSpread.TabIndex = 0;
            this.fpSpread.TabStrip.Editable = true;
            this.fpSpread.TabStripInsertTab = false;
            this.fpSpread.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Always;
            this.fpSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread.Watermark = null;
            this.fpSpread.MouseUp += new System.Windows.Forms.MouseEventHandler(this.fpSpread_MouseUp);
            this.fpSpread.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(this.fpSpread_SelectionChanged);
            // 
            // fpSpread_Sheet2
            // 
            this.fpSpread_Sheet2.Reset();
            this.fpSpread_Sheet2.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread_Sheet2.PrintInfo.PaperSize = ((System.Drawing.Printing.PaperSize)(resources.GetObject("resource.PaperSize")));
            this.fpSpread_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FunctionStrip
            // 
            this.FunctionStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tTextBox_XY,
            this.FormulaCancelButton,
            this.FormulaOkButton,
            this.FormulaButton2});
            this.FunctionStrip.Location = new System.Drawing.Point(0, 25);
            this.FunctionStrip.Name = "FunctionStrip";
            this.FunctionStrip.Size = new System.Drawing.Size(671, 25);
            this.FunctionStrip.TabIndex = 5;
            this.FunctionStrip.Text = "toolStrip2";
            // 
            // tTextBox_XY
            // 
            this.tTextBox_XY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tTextBox_XY.Name = "tTextBox_XY";
            this.tTextBox_XY.Size = new System.Drawing.Size(200, 25);
            this.tTextBox_XY.Text = "A1";
            this.tTextBox_XY.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FormulaCancelButton
            // 
            this.FormulaCancelButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FormulaCancelButton.Image = global::BizComponents.Properties.Resources.取消1;
            this.FormulaCancelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FormulaCancelButton.Name = "FormulaCancelButton";
            this.FormulaCancelButton.Size = new System.Drawing.Size(23, 22);
            this.FormulaCancelButton.Text = "取消";
            this.FormulaCancelButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // FormulaOkButton
            // 
            this.FormulaOkButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FormulaOkButton.Image = global::BizComponents.Properties.Resources.输入1;
            this.FormulaOkButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FormulaOkButton.Name = "FormulaOkButton";
            this.FormulaOkButton.Size = new System.Drawing.Size(23, 22);
            this.FormulaOkButton.Text = "输入";
            this.FormulaOkButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // FormulaButton2
            // 
            this.FormulaButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FormulaButton2.Image = global::BizComponents.Properties.Resources.公式;
            this.FormulaButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FormulaButton2.Name = "FormulaButton2";
            this.FormulaButton2.Size = new System.Drawing.Size(23, 22);
            this.FormulaButton2.Text = "公式";
            this.FormulaButton2.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // UndoButton
            // 
            this.UndoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UndoButton.Image = global::BizComponents.Properties.Resources.撤销;
            this.UndoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UndoButton.Name = "UndoButton";
            this.UndoButton.Size = new System.Drawing.Size(23, 22);
            this.UndoButton.Text = "撤消";
            this.UndoButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // RedoButton
            // 
            this.RedoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RedoButton.Image = global::BizComponents.Properties.Resources.重复;
            this.RedoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RedoButton.Name = "RedoButton";
            this.RedoButton.Size = new System.Drawing.Size(23, 22);
            this.RedoButton.Text = "重做";
            this.RedoButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // FpSpreadEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fpSpread);
            this.Controls.Add(this.FunctionStrip);
            this.Controls.Add(this.CommonTools);
            this.Name = "FpSpreadEditor";
            this.Size = new System.Drawing.Size(671, 507);
            this.Load += new System.EventHandler(this.FpSpreadEditor_Load);
            this.CommonTools.ResumeLayout(false);
            this.CommonTools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread_Sheet2)).EndInit();
            this.FunctionStrip.ResumeLayout(false);
            this.FunctionStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton ShearButton;
        private System.Windows.Forms.ToolStripButton CopyButton;
        private System.Windows.Forms.ToolStripButton PasteButton;
        private System.Windows.Forms.ToolStripButton PrintButton;
        private System.Windows.Forms.ToolStripButton PrintPreviewButton;
        private MyCell fpSpread;
        private System.Windows.Forms.ToolStripButton ExportExcelButton;
        private System.Windows.Forms.ToolStripButton PageSettingButton;
        private FarPoint.Win.Spread.SheetView fpSpread_Sheet2;
        private System.Windows.Forms.ToolStripTextBox tTextBox_XY;
        private System.Windows.Forms.ToolStripButton FormulaCancelButton;
        private System.Windows.Forms.ToolStripButton FormulaOkButton;
        private System.Windows.Forms.ToolStripButton FormulaButton2;
        protected System.Windows.Forms.ToolStrip CommonTools;
        protected System.Windows.Forms.ToolStrip FunctionStrip;
        private System.Windows.Forms.ToolStripButton UndoButton;
        private System.Windows.Forms.ToolStripButton RedoButton;
    }
}
