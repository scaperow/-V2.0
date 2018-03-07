namespace BizComponents
{
    partial class GeneratePXRelationDialog
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtBGBH = new System.Windows.Forms.ToolStripTextBox();
            this.btnSearch = new System.Windows.Forms.ToolStripButton();
            this.lblMsg = new System.Windows.Forms.ToolStripLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.spread_stadium = new FarPoint.Win.Spread.FpSpread();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiResetPXRelation = new System.Windows.Forms.ToolStripMenuItem();
            this.spread_stadium_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spread_stadium)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spread_stadium_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.txtBGBH,
            this.btnSearch,
            this.lblMsg});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(685, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(97, 22);
            this.toolStripLabel1.Text = "报告/委托编号：";
            // 
            // txtBGBH
            // 
            this.txtBGBH.Name = "txtBGBH";
            this.txtBGBH.Size = new System.Drawing.Size(250, 25);
            // 
            // btnSearch
            // 
            this.btnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSearch.Image = global::BizComponents.Properties.Resources.输入1;
            this.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(23, 22);
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 22);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.spread_stadium);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(685, 375);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "监理对应模板的可平行试验";
            // 
            // spread_stadium
            // 
            this.spread_stadium.AccessibleDescription = "";
            this.spread_stadium.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.spread_stadium.ContextMenuStrip = this.contextMenuStrip1;
            this.spread_stadium.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spread_stadium.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.spread_stadium.Location = new System.Drawing.Point(3, 17);
            this.spread_stadium.Name = "spread_stadium";
            this.spread_stadium.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.spread_stadium.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spread_stadium_Sheet1});
            this.spread_stadium.Size = new System.Drawing.Size(679, 355);
            this.spread_stadium.TabIndex = 1;
            this.spread_stadium.TabStripInsertTab = false;
            this.spread_stadium.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            this.spread_stadium.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiResetPXRelation});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 26);
            // 
            // tsmiResetPXRelation
            // 
            this.tsmiResetPXRelation.Name = "tsmiResetPXRelation";
            this.tsmiResetPXRelation.Size = new System.Drawing.Size(148, 22);
            this.tsmiResetPXRelation.Text = "生成平行关系";
            this.tsmiResetPXRelation.Click += new System.EventHandler(this.tsmiResetPXRelation_Click);
            // 
            // spread_stadium_Sheet1
            // 
            this.spread_stadium_Sheet1.Reset();
            this.spread_stadium_Sheet1.SheetName = "Sheet1";
            this.spread_stadium_Sheet1.ColumnCount = 5;
            this.spread_stadium_Sheet1.RowCount = 0;
            this.spread_stadium.SetActiveViewport(0, 1, 0);
            // 
            // GeneratePXRelationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 400);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStrip1);
            this.Name = "GeneratePXRelationDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "生成平行对应关系";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spread_stadium)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spread_stadium_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox txtBGBH;
        private System.Windows.Forms.ToolStripButton btnSearch;
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread spread_stadium;
        private FarPoint.Win.Spread.SheetView spread_stadium_Sheet1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiResetPXRelation;
        private System.Windows.Forms.ToolStripLabel lblMsg;
    }
}