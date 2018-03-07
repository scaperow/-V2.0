namespace BizComponents
{

    partial class PagerControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PagerControl));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.blnkFirst = new System.Windows.Forms.ToolStripButton();
            this.blnkPrev = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.lblCurrentPage = new System.Windows.Forms.ToolStripTextBox();
            this.lblPageCount = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.blnkNext = new System.Windows.Forms.ToolStripButton();
            this.blnkLast = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblTotalCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.blnkFirst,
            this.blnkPrev,
            this.bindingNavigatorSeparator,
            this.lblCurrentPage,
            this.lblPageCount,
            this.bindingNavigatorSeparator2,
            this.blnkNext,
            this.blnkLast,
            this.bindingNavigatorSeparator1,
            this.lblTotalCount});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(580, 28);
            this.toolStrip1.TabIndex = 58;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // blnkFirst
            // 
            this.blnkFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.blnkFirst.Image = ((System.Drawing.Image)(resources.GetObject("blnkFirst.Image")));
            this.blnkFirst.Name = "blnkFirst";
            this.blnkFirst.RightToLeftAutoMirrorImage = true;
            this.blnkFirst.Size = new System.Drawing.Size(23, 25);
            this.blnkFirst.Text = "移到第一条记录";
            this.blnkFirst.Click += new System.EventHandler(this.blnkFirst_Click);
            // 
            // blnkPrev
            // 
            this.blnkPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.blnkPrev.Image = ((System.Drawing.Image)(resources.GetObject("blnkPrev.Image")));
            this.blnkPrev.Name = "blnkPrev";
            this.blnkPrev.RightToLeftAutoMirrorImage = true;
            this.blnkPrev.Size = new System.Drawing.Size(23, 25);
            this.blnkPrev.Text = "移到上一条记录";
            this.blnkPrev.Click += new System.EventHandler(this.blnkPrev_Click);
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 28);
            // 
            // lblCurrentPage
            // 
            this.lblCurrentPage.AccessibleName = "位置";
            this.lblCurrentPage.AutoSize = false;
            this.lblCurrentPage.Name = "lblCurrentPage";
            this.lblCurrentPage.Size = new System.Drawing.Size(50, 23);
            this.lblCurrentPage.Text = "0";
            this.lblCurrentPage.ToolTipText = "当前位置";
            // 
            // lblPageCount
            // 
            this.lblPageCount.Name = "lblPageCount";
            this.lblPageCount.Size = new System.Drawing.Size(32, 25);
            this.lblPageCount.Text = "/ {0}";
            this.lblPageCount.ToolTipText = "总项数";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // blnkNext
            // 
            this.blnkNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.blnkNext.Image = ((System.Drawing.Image)(resources.GetObject("blnkNext.Image")));
            this.blnkNext.Name = "blnkNext";
            this.blnkNext.RightToLeftAutoMirrorImage = true;
            this.blnkNext.Size = new System.Drawing.Size(23, 25);
            this.blnkNext.Text = "移到下一条记录";
            this.blnkNext.Click += new System.EventHandler(this.blnkNext_Click);
            // 
            // blnkLast
            // 
            this.blnkLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.blnkLast.Image = ((System.Drawing.Image)(resources.GetObject("blnkLast.Image")));
            this.blnkLast.Name = "blnkLast";
            this.blnkLast.RightToLeftAutoMirrorImage = true;
            this.blnkLast.Size = new System.Drawing.Size(23, 25);
            this.blnkLast.Text = "移到最后一条记录";
            this.blnkLast.Click += new System.EventHandler(this.blnkLast_Click);
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(71, 25);
            this.lblTotalCount.Text = "共{0}条记录";
            // 
            // PagerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.toolStrip1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(78)))), ((int)(((byte)(151)))));
            this.Name = "PagerControl";
            this.Size = new System.Drawing.Size(580, 28);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton blnkFirst;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton blnkNext;
        private System.Windows.Forms.ToolStripTextBox lblCurrentPage;
        private System.Windows.Forms.ToolStripLabel lblPageCount;
        private System.Windows.Forms.ToolStripButton blnkLast;
        private System.Windows.Forms.ToolStripButton blnkPrev;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripLabel lblTotalCount;

    }
}
