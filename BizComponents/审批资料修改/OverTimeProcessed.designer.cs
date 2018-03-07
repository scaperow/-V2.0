namespace BizComponents.审批资料修改
{
    partial class OverTimeProcessed
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OverTimeProcessed));
            this.LoadingStatus = new System.Windows.Forms.Label();
            this.NavigatorBar = new System.Windows.Forms.ToolStrip();
            this.First = new System.Windows.Forms.ToolStripButton();
            this.Previous = new System.Windows.Forms.ToolStripButton();
            this.Index = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.Numbers = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.Next = new System.Windows.Forms.ToolStripButton();
            this.Last = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TotalCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.Size = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Close = new System.Windows.Forms.Button();
            this.Datas = new Yqun.Client.BizUI.MyCell();
            this.Datas_SheetInfo = new FarPoint.Win.Spread.SheetView();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.填写原因ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.处理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NavigatorBar.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datas_SheetInfo)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoadingStatus
            // 
            this.LoadingStatus.AutoSize = true;
            this.LoadingStatus.Location = new System.Drawing.Point(12, 470);
            this.LoadingStatus.Name = "LoadingStatus";
            this.LoadingStatus.Size = new System.Drawing.Size(0, 12);
            this.LoadingStatus.TabIndex = 2;
            // 
            // NavigatorBar
            // 
            this.NavigatorBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.NavigatorBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.First,
            this.Previous,
            this.Index,
            this.toolStripSeparator9,
            this.Numbers,
            this.toolStripSeparator8,
            this.Next,
            this.Last,
            this.toolStripSeparator1,
            this.TotalCount,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.Size,
            this.toolStripLabel2});
            this.NavigatorBar.Location = new System.Drawing.Point(0, 0);
            this.NavigatorBar.Name = "NavigatorBar";
            this.NavigatorBar.Size = new System.Drawing.Size(728, 25);
            this.NavigatorBar.TabIndex = 3;
            this.NavigatorBar.Text = "导航栏";
            // 
            // First
            // 
            this.First.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.First.Enabled = false;
            this.First.Image = ((System.Drawing.Image)(resources.GetObject("First.Image")));
            this.First.Name = "First";
            this.First.RightToLeftAutoMirrorImage = true;
            this.First.Size = new System.Drawing.Size(23, 22);
            this.First.Text = "移到第一页";
            this.First.Click += new System.EventHandler(this.First_Click);
            // 
            // Previous
            // 
            this.Previous.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Previous.Enabled = false;
            this.Previous.Image = ((System.Drawing.Image)(resources.GetObject("Previous.Image")));
            this.Previous.Name = "Previous";
            this.Previous.RightToLeftAutoMirrorImage = true;
            this.Previous.Size = new System.Drawing.Size(23, 22);
            this.Previous.Text = "移到上一页";
            this.Previous.Click += new System.EventHandler(this.Previous_Click);
            // 
            // Index
            // 
            this.Index.AccessibleName = "页";
            this.Index.AutoSize = false;
            this.Index.Name = "Index";
            this.Index.Size = new System.Drawing.Size(50, 20);
            this.Index.Text = "1";
            this.Index.ToolTipText = "当前页";
            this.Index.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Index_KeyDown);
            this.Index.Click += new System.EventHandler(this.Index_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // Numbers
            // 
            this.Numbers.Name = "Numbers";
            this.Numbers.Size = new System.Drawing.Size(32, 22);
            this.Numbers.Text = "/ {0}";
            this.Numbers.ToolTipText = "总页数";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // Next
            // 
            this.Next.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Next.Enabled = false;
            this.Next.Image = ((System.Drawing.Image)(resources.GetObject("Next.Image")));
            this.Next.Name = "Next";
            this.Next.RightToLeftAutoMirrorImage = true;
            this.Next.Size = new System.Drawing.Size(23, 22);
            this.Next.Text = "移到下一页";
            this.Next.ToolTipText = "移到下一页";
            this.Next.Click += new System.EventHandler(this.Next_Click);
            // 
            // Last
            // 
            this.Last.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Last.Enabled = false;
            this.Last.Image = ((System.Drawing.Image)(resources.GetObject("Last.Image")));
            this.Last.Name = "Last";
            this.Last.RightToLeftAutoMirrorImage = true;
            this.Last.Size = new System.Drawing.Size(23, 22);
            this.Last.Text = "移到最后一页";
            this.Last.ToolTipText = "移到最后一页";
            this.Last.Click += new System.EventHandler(this.Last_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // TotalCount
            // 
            this.TotalCount.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TotalCount.Name = "TotalCount";
            this.TotalCount.Size = new System.Drawing.Size(79, 22);
            this.TotalCount.Text = "共 {0} 条记录";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(56, 22);
            this.toolStripLabel1.Text = "每页显示";
            // 
            // Size
            // 
            this.Size.Name = "Size";
            this.Size.Size = new System.Drawing.Size(40, 25);
            this.Size.Text = "20";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(20, 22);
            this.toolStripLabel2.Text = "条";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Close);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 450);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(728, 44);
            this.panel1.TabIndex = 4;
            // 
            // Close
            // 
            this.Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Close.Location = new System.Drawing.Point(641, 9);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(75, 23);
            this.Close.TabIndex = 0;
            this.Close.Text = "关闭(&C)";
            this.Close.UseVisualStyleBackColor = true;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // Datas
            // 
            this.Datas.AccessibleDescription = "Datas, SheetInfo, Row 0, Column 0, ";
            this.Datas.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.Datas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Datas.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.Datas.IsEditing = false;
            this.Datas.Location = new System.Drawing.Point(0, 25);
            this.Datas.Name = "Datas";
            this.Datas.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.Datas.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.Datas_SheetInfo});
            this.Datas.Size = new System.Drawing.Size(728, 425);
            this.Datas.TabIndex = 6;
            this.Datas.TabStripInsertTab = false;
            this.Datas.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            this.Datas.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.Datas.Watermark = null;
            // 
            // Datas_SheetInfo
            // 
            this.Datas_SheetInfo.Reset();
            this.Datas_SheetInfo.SheetName = "SheetInfo";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.Datas_SheetInfo.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.Datas_SheetInfo.ColumnCount = 0;
            this.Datas_SheetInfo.RowCount = 1;
            this.Datas_SheetInfo.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.Datas_SheetInfo.PrintInfo.PaperSize = ((System.Drawing.Printing.PaperSize)(resources.GetObject("resource.PaperSize")));
            this.Datas_SheetInfo.RowHeader.Visible = false;
            this.Datas_SheetInfo.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.Datas.SetActiveViewport(0, 0, 1);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.填写原因ToolStripMenuItem,
            this.处理ToolStripMenuItem,
            this.删除ToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(153, 92);
            // 
            // 填写原因ToolStripMenuItem
            // 
            this.填写原因ToolStripMenuItem.Name = "填写原因ToolStripMenuItem";
            this.填写原因ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.填写原因ToolStripMenuItem.Text = "填写原因";
            this.填写原因ToolStripMenuItem.Click += new System.EventHandler(this.填写原因ToolStripMenuItem_Click);
            // 
            // 处理ToolStripMenuItem
            // 
            this.处理ToolStripMenuItem.Name = "处理ToolStripMenuItem";
            this.处理ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.处理ToolStripMenuItem.Text = "处理";
            this.处理ToolStripMenuItem.Click += new System.EventHandler(this.处理ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            // 
            // OverTimeProcessed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 494);
            this.Controls.Add(this.Datas);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.NavigatorBar);
            this.Controls.Add(this.LoadingStatus);
            this.Name = "OverTimeProcessed";
            this.Text = "查看过期试验";
            this.Load += new System.EventHandler(this.OverTimeProcessed_Load);
            this.NavigatorBar.ResumeLayout(false);
            this.NavigatorBar.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Datas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datas_SheetInfo)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LoadingStatus;
        private System.Windows.Forms.ToolStrip NavigatorBar;
        internal System.Windows.Forms.ToolStripButton First;
        internal System.Windows.Forms.ToolStripButton Previous;
        internal System.Windows.Forms.ToolStripTextBox Index;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        internal System.Windows.Forms.ToolStripLabel Numbers;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        internal System.Windows.Forms.ToolStripButton Next;
        internal System.Windows.Forms.ToolStripButton Last;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel TotalCount;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox Size;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Close;
        public Yqun.Client.BizUI.MyCell Datas;
        public FarPoint.Win.Spread.SheetView Datas_SheetInfo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem 填写原因ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 处理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
    }
}