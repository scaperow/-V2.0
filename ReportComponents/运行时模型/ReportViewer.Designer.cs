namespace ReportComponents
{
    partial class ReportViewer
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
            this.PrintPreviewControl1 = new System.Windows.Forms.PrintPreviewControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._btnPrint = new System.Windows.Forms.ToolStripButton();
            this._btnPageSetup = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsDDownPages = new System.Windows.Forms.ToolStripDropDownButton();
            this.pageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pagesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pagesToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.pagesToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnZoom = new System.Windows.Forms.ToolStripButton();
            this.tsComboZoom = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._btnFirst = new System.Windows.Forms.ToolStripButton();
            this.tsBtnPrev = new System.Windows.Forms.ToolStripButton();
            this.tsTxtCurrentPage = new System.Windows.Forms.ToolStripTextBox();
            this.tsLblTotalPages = new System.Windows.Forms.ToolStripLabel();
            this.tsBtnNext = new System.Windows.Forms.ToolStripButton();
            this._btnLast = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.Button_ImportExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PrintPreviewControl1
            // 
            this.PrintPreviewControl1.BackColor = System.Drawing.Color.White;
            this.PrintPreviewControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrintPreviewControl1.Location = new System.Drawing.Point(0, 25);
            this.PrintPreviewControl1.Name = "PrintPreviewControl1";
            this.PrintPreviewControl1.Size = new System.Drawing.Size(759, 512);
            this.PrintPreviewControl1.TabIndex = 3;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btnPrint,
            this._btnPageSetup,
            this.toolStripSeparator1,
            this.tsDDownPages,
            this.toolStripSeparator4,
            this.tsBtnZoom,
            this.tsComboZoom,
            this.toolStripSeparator2,
            this._btnFirst,
            this.tsBtnPrev,
            this.tsTxtCurrentPage,
            this.tsLblTotalPages,
            this.tsBtnNext,
            this._btnLast,
            this.toolStripSeparator3,
            this.Button_ImportExcel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(759, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _btnPrint
            // 
            this._btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._btnPrint.Image = global::ReportComponents.Properties.Resources.打印;
            this._btnPrint.ImageTransparentColor = System.Drawing.Color.White;
            this._btnPrint.Name = "_btnPrint";
            this._btnPrint.Size = new System.Drawing.Size(23, 22);
            this._btnPrint.Text = "打印";
            this._btnPrint.Click += new System.EventHandler(this.tsBtnPrint_Click);
            // 
            // _btnPageSetup
            // 
            this._btnPageSetup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._btnPageSetup.Enabled = false;
            this._btnPageSetup.Image = global::ReportComponents.Properties.Resources.页面设置;
            this._btnPageSetup.ImageTransparentColor = System.Drawing.Color.White;
            this._btnPageSetup.Name = "_btnPageSetup";
            this._btnPageSetup.Size = new System.Drawing.Size(23, 22);
            this._btnPageSetup.Text = "页面设置";
            this._btnPageSetup.Click += new System.EventHandler(this.tsBtnPageSettings_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsDDownPages
            // 
            this.tsDDownPages.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsDDownPages.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pageToolStripMenuItem,
            this.pagesToolStripMenuItem,
            this.pagesToolStripMenuItem1,
            this.pagesToolStripMenuItem2,
            this.pagesToolStripMenuItem3});
            this.tsDDownPages.Image = global::ReportComponents.Properties.Resources.多页;
            this.tsDDownPages.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDDownPages.Name = "tsDDownPages";
            this.tsDDownPages.Size = new System.Drawing.Size(29, 22);
            this.tsDDownPages.Tag = "1";
            this.tsDDownPages.ToolTipText = "显示页数";
            this.tsDDownPages.Click += new System.EventHandler(this.NumOfPages_Click);
            // 
            // pageToolStripMenuItem
            // 
            this.pageToolStripMenuItem.Name = "pageToolStripMenuItem";
            this.pageToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.pageToolStripMenuItem.Tag = "1";
            this.pageToolStripMenuItem.Text = "单页";
            // 
            // pagesToolStripMenuItem
            // 
            this.pagesToolStripMenuItem.Name = "pagesToolStripMenuItem";
            this.pagesToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.pagesToolStripMenuItem.Tag = "2";
            this.pagesToolStripMenuItem.Text = "两页";
            // 
            // pagesToolStripMenuItem1
            // 
            this.pagesToolStripMenuItem1.Name = "pagesToolStripMenuItem1";
            this.pagesToolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.pagesToolStripMenuItem1.Tag = "4";
            this.pagesToolStripMenuItem1.Text = "四页";
            // 
            // pagesToolStripMenuItem2
            // 
            this.pagesToolStripMenuItem2.Name = "pagesToolStripMenuItem2";
            this.pagesToolStripMenuItem2.Size = new System.Drawing.Size(100, 22);
            this.pagesToolStripMenuItem2.Tag = "6";
            this.pagesToolStripMenuItem2.Text = "六页";
            // 
            // pagesToolStripMenuItem3
            // 
            this.pagesToolStripMenuItem3.Name = "pagesToolStripMenuItem3";
            this.pagesToolStripMenuItem3.Size = new System.Drawing.Size(100, 22);
            this.pagesToolStripMenuItem3.Tag = "8";
            this.pagesToolStripMenuItem3.Text = "八页";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsBtnZoom
            // 
            this.tsBtnZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnZoom.Image = global::ReportComponents.Properties.Resources.缩放;
            this.tsBtnZoom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnZoom.Margin = new System.Windows.Forms.Padding(10, 1, 0, 2);
            this.tsBtnZoom.Name = "tsBtnZoom";
            this.tsBtnZoom.Size = new System.Drawing.Size(23, 22);
            this.tsBtnZoom.Text = "缩放";
            this.tsBtnZoom.ToolTipText = "合适大小";
            this.tsBtnZoom.Click += new System.EventHandler(this.tsBtnZoom_Click);
            // 
            // tsComboZoom
            // 
            this.tsComboZoom.Items.AddRange(new object[] {
            "自动",
            "10%",
            "25%",
            "50%",
            "75%",
            "100%",
            "150%",
            "200%",
            "300%",
            "400%",
            "500%"});
            this.tsComboZoom.Name = "tsComboZoom";
            this.tsComboZoom.Size = new System.Drawing.Size(100, 25);
            this.tsComboZoom.Text = "自动";
            this.tsComboZoom.ToolTipText = "缩放";
            this.tsComboZoom.Leave += new System.EventHandler(this.tsComboZoom_Leave);
            this.tsComboZoom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tsComboZoom_KeyPress);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _btnFirst
            // 
            this._btnFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._btnFirst.Image = global::ReportComponents.Properties.Resources.第一页;
            this._btnFirst.Name = "_btnFirst";
            this._btnFirst.Size = new System.Drawing.Size(23, 22);
            this._btnFirst.Text = "首页";
            this._btnFirst.Click += new System.EventHandler(this.Navigate_Click);
            // 
            // tsBtnPrev
            // 
            this.tsBtnPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnPrev.Image = global::ReportComponents.Properties.Resources.上一页;
            this.tsBtnPrev.Name = "tsBtnPrev";
            this.tsBtnPrev.Size = new System.Drawing.Size(23, 22);
            this.tsBtnPrev.Text = "上一页";
            this.tsBtnPrev.Click += new System.EventHandler(this.Navigate_Click);
            // 
            // tsTxtCurrentPage
            // 
            this.tsTxtCurrentPage.AcceptsReturn = true;
            this.tsTxtCurrentPage.AccessibleName = "位置";
            this.tsTxtCurrentPage.AutoSize = false;
            this.tsTxtCurrentPage.Name = "tsTxtCurrentPage";
            this.tsTxtCurrentPage.Size = new System.Drawing.Size(50, 21);
            this.tsTxtCurrentPage.Text = "0";
            this.tsTxtCurrentPage.ToolTipText = "当前页";
            this.tsTxtCurrentPage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tsTxtCurrentPage_KeyPress);
            // 
            // tsLblTotalPages
            // 
            this.tsLblTotalPages.Name = "tsLblTotalPages";
            this.tsLblTotalPages.Size = new System.Drawing.Size(32, 22);
            this.tsLblTotalPages.Text = "/ {0}";
            this.tsLblTotalPages.ToolTipText = "总页数";
            // 
            // tsBtnNext
            // 
            this.tsBtnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnNext.Image = global::ReportComponents.Properties.Resources.下一页;
            this.tsBtnNext.Name = "tsBtnNext";
            this.tsBtnNext.Size = new System.Drawing.Size(23, 22);
            this.tsBtnNext.Text = "下一页";
            this.tsBtnNext.Click += new System.EventHandler(this.Navigate_Click);
            // 
            // _btnLast
            // 
            this._btnLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._btnLast.Image = global::ReportComponents.Properties.Resources.最后一页;
            this._btnLast.Name = "_btnLast";
            this._btnLast.Size = new System.Drawing.Size(23, 22);
            this._btnLast.Text = "末页";
            this._btnLast.Click += new System.EventHandler(this.Navigate_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // Button_ImportExcel
            // 
            this.Button_ImportExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Button_ImportExcel.Image = global::ReportComponents.Properties.Resources.导出Excel;
            this.Button_ImportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_ImportExcel.Name = "Button_ImportExcel";
            this.Button_ImportExcel.Size = new System.Drawing.Size(23, 22);
            this.Button_ImportExcel.Text = "导出Excel";
            this.Button_ImportExcel.ToolTipText = "导出到Excel";
            this.Button_ImportExcel.Click += new System.EventHandler(this.Button_ImportExcel_Click);
            // 
            // ReportViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PrintPreviewControl1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ReportViewer";
            this.Size = new System.Drawing.Size(759, 537);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PrintPreviewControl PrintPreviewControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _btnPrint;
        private System.Windows.Forms.ToolStripButton _btnPageSetup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton tsDDownPages;
        private System.Windows.Forms.ToolStripMenuItem pageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pagesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem pagesToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem pagesToolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsBtnZoom;
        private System.Windows.Forms.ToolStripComboBox tsComboZoom;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton _btnFirst;
        private System.Windows.Forms.ToolStripButton tsBtnPrev;
        internal System.Windows.Forms.ToolStripTextBox tsTxtCurrentPage;
        internal System.Windows.Forms.ToolStripLabel tsLblTotalPages;
        private System.Windows.Forms.ToolStripButton tsBtnNext;
        private System.Windows.Forms.ToolStripButton _btnLast;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton Button_ImportExcel;
    }
}
