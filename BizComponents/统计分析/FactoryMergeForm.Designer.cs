namespace BizComponents
{
    partial class FactoryMergeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FactoryMergeForm));
            this.DataTotalLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.NavigatorBar = new System.Windows.Forms.ToolStrip();
            this.First = new System.Windows.Forms.ToolStripButton();
            this.Previous = new System.Windows.Forms.ToolStripButton();
            this.Index = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.Numbers = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.Next = new System.Windows.Forms.ToolStripButton();
            this.Last = new System.Windows.Forms.ToolStripButton();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.ButtonClose = new System.Windows.Forms.Button();
            this.ButtonRight = new System.Windows.Forms.Button();
            this.ButtonLeft = new System.Windows.Forms.Button();
            this.TextSearchAll = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TextSearchMerge = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.TreeAll = new System.Windows.Forms.TreeView();
            this.label3 = new System.Windows.Forms.Label();
            this.TreeMerge = new System.Windows.Forms.TreeView();
            this.ButtonRefresh = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.NavigatorBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataTotalLabel
            // 
            this.DataTotalLabel.Name = "DataTotalLabel";
            this.DataTotalLabel.Size = new System.Drawing.Size(87, 22);
            this.DataTotalLabel.Text = "共有 0 条数据 ";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // NavigatorBar
            // 
            this.NavigatorBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.NavigatorBar.AutoSize = false;
            this.NavigatorBar.Dock = System.Windows.Forms.DockStyle.None;
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
            this.DataTotalLabel});
            this.NavigatorBar.Location = new System.Drawing.Point(156, 689);
            this.NavigatorBar.Name = "NavigatorBar";
            this.NavigatorBar.Size = new System.Drawing.Size(881, 25);
            this.NavigatorBar.TabIndex = 14;
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
            // 
            // Index
            // 
            this.Index.AccessibleName = "页";
            this.Index.AutoSize = false;
            this.Index.Name = "Index";
            this.Index.Size = new System.Drawing.Size(50, 20);
            this.Index.Text = "1";
            this.Index.ToolTipText = "当前页";
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
            // 
            // ButtonSave
            // 
            this.ButtonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonSave.Location = new System.Drawing.Point(516, 516);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(75, 23);
            this.ButtonSave.TabIndex = 18;
            this.ButtonSave.Text = "合并";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // ButtonClose
            // 
            this.ButtonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonClose.Location = new System.Drawing.Point(597, 516);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(75, 23);
            this.ButtonClose.TabIndex = 18;
            this.ButtonClose.Text = "关闭";
            this.ButtonClose.UseVisualStyleBackColor = true;
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // ButtonRight
            // 
            this.ButtonRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonRight.Location = new System.Drawing.Point(307, 23);
            this.ButtonRight.Name = "ButtonRight";
            this.ButtonRight.Size = new System.Drawing.Size(75, 23);
            this.ButtonRight.TabIndex = 19;
            this.ButtonRight.Text = ">";
            this.ButtonRight.UseVisualStyleBackColor = true;
            this.ButtonRight.Click += new System.EventHandler(this.ButtonRight_Click);
            // 
            // ButtonLeft
            // 
            this.ButtonLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonLeft.Location = new System.Drawing.Point(307, 52);
            this.ButtonLeft.Name = "ButtonLeft";
            this.ButtonLeft.Size = new System.Drawing.Size(75, 23);
            this.ButtonLeft.TabIndex = 19;
            this.ButtonLeft.Text = "<";
            this.ButtonLeft.UseVisualStyleBackColor = true;
            this.ButtonLeft.Click += new System.EventHandler(this.ButtonLeft_Click);
            // 
            // TextSearchAll
            // 
            this.TextSearchAll.Location = new System.Drawing.Point(14, 25);
            this.TextSearchAll.Name = "TextSearchAll";
            this.TextSearchAll.Size = new System.Drawing.Size(284, 21);
            this.TextSearchAll.TabIndex = 20;
            this.TextSearchAll.Visible = false;
            this.TextSearchAll.TextChanged += new System.EventHandler(this.TextSearchAll_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "检索";
            this.label1.Visible = false;
            // 
            // TextSearchMerge
            // 
            this.TextSearchMerge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TextSearchMerge.Location = new System.Drawing.Point(388, 25);
            this.TextSearchMerge.Name = "TextSearchMerge";
            this.TextSearchMerge.Size = new System.Drawing.Size(284, 21);
            this.TextSearchMerge.TabIndex = 20;
            this.TextSearchMerge.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(386, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 21;
            this.label2.Text = "检索";
            this.label2.Visible = false;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // TreeAll
            // 
            this.TreeAll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TreeAll.FullRowSelect = true;
            this.TreeAll.ItemHeight = 19;
            this.TreeAll.Location = new System.Drawing.Point(14, 24);
            this.TreeAll.Name = "TreeAll";
            this.TreeAll.ShowLines = false;
            this.TreeAll.ShowPlusMinus = false;
            this.TreeAll.ShowRootLines = false;
            this.TreeAll.Size = new System.Drawing.Size(284, 486);
            this.TreeAll.TabIndex = 22;
            this.TreeAll.DoubleClick += new System.EventHandler(this.TreeAll_DoubleClick);
            this.TreeAll.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeAll_AfterSelect);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 21;
            this.label3.Text = "所有厂家";
            // 
            // TreeMerge
            // 
            this.TreeMerge.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TreeMerge.FullRowSelect = true;
            this.TreeMerge.ItemHeight = 19;
            this.TreeMerge.Location = new System.Drawing.Point(388, 24);
            this.TreeMerge.Name = "TreeMerge";
            this.TreeMerge.ShowLines = false;
            this.TreeMerge.ShowPlusMinus = false;
            this.TreeMerge.ShowRootLines = false;
            this.TreeMerge.Size = new System.Drawing.Size(284, 486);
            this.TreeMerge.TabIndex = 22;
            this.TreeMerge.DoubleClick += new System.EventHandler(this.TreeMerge_DoubleClick);
            // 
            // ButtonRefresh
            // 
            this.ButtonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonRefresh.Location = new System.Drawing.Point(12, 516);
            this.ButtonRefresh.Name = "ButtonRefresh";
            this.ButtonRefresh.Size = new System.Drawing.Size(75, 23);
            this.ButtonRefresh.TabIndex = 18;
            this.ButtonRefresh.Text = "刷新";
            this.ButtonRefresh.UseVisualStyleBackColor = true;
            this.ButtonRefresh.Click += new System.EventHandler(this.ButtonRefresh_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(386, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 23;
            this.label4.Text = "准备合并的厂家";
            // 
            // FactoryMergeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 551);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TreeMerge);
            this.Controls.Add(this.TreeAll);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextSearchMerge);
            this.Controls.Add(this.TextSearchAll);
            this.Controls.Add(this.ButtonLeft);
            this.Controls.Add(this.ButtonRight);
            this.Controls.Add(this.ButtonClose);
            this.Controls.Add(this.ButtonRefresh);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.NavigatorBar);
            this.Name = "FactoryMergeForm";
            this.Text = "厂家信息合并";
            this.Load += new System.EventHandler(this.FactoryManagement_Load);
            this.NavigatorBar.ResumeLayout(false);
            this.NavigatorBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripLabel DataTotalLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        internal System.Windows.Forms.ToolStripButton Last;
        internal System.Windows.Forms.ToolStripButton Next;
        private System.Windows.Forms.ToolStrip NavigatorBar;
        internal System.Windows.Forms.ToolStripButton First;
        internal System.Windows.Forms.ToolStripButton Previous;
        internal System.Windows.Forms.ToolStripTextBox Index;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        internal System.Windows.Forms.ToolStripLabel Numbers;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.Button ButtonSave;
        private System.Windows.Forms.Button ButtonClose;
        private System.Windows.Forms.Button ButtonRight;
        private System.Windows.Forms.Button ButtonLeft;
        private System.Windows.Forms.TextBox TextSearchAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextSearchMerge;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TreeView TreeAll;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TreeView TreeMerge;
        private System.Windows.Forms.Button ButtonRefresh;
        private System.Windows.Forms.Label label4;
    }
}