namespace BizComponents
{
    partial class UnusedTestDataView
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_ShowTestRoomInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.FpSpread_Info = new FarPoint.Win.Spread.SheetView();
            this.collapsibleSplitter1 = new Yqun.Controls.CollapsibleSplitter();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStartMQAll = new System.Windows.Forms.Button();
            this.btnReloadMQ = new System.Windows.Forms.Button();
            this.btnManualUploadMQ = new System.Windows.Forms.Button();
            this.btn_search = new System.Windows.Forms.Button();
            this.txtWTBH = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread_Info)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.fpSpread1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(788, 558);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "采集数据";
            // 
            // fpSpread1
            // 
            this.fpSpread1.AccessibleDescription = "";
            this.fpSpread1.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpSpread1.ContextMenuStrip = this.contextMenuStrip1;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.Location = new System.Drawing.Point(3, 17);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FpSpread_Info});
            this.fpSpread1.Size = new System.Drawing.Size(782, 538);
            this.fpSpread1.TabIndex = 1;
            this.fpSpread1.TabStripInsertTab = false;
            this.fpSpread1.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.ToolStripMenuItem_ShowTestRoomInfo});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 48);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItem1.Text = "查看采集曲线";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // ToolStripMenuItem_ShowTestRoomInfo
            // 
            this.ToolStripMenuItem_ShowTestRoomInfo.Name = "ToolStripMenuItem_ShowTestRoomInfo";
            this.ToolStripMenuItem_ShowTestRoomInfo.Size = new System.Drawing.Size(160, 22);
            this.ToolStripMenuItem_ShowTestRoomInfo.Text = "查看试验室信息";
            this.ToolStripMenuItem_ShowTestRoomInfo.Click += new System.EventHandler(this.ToolStripMenuItem_ShowTestRoomInfo_Click);
            // 
            // FpSpread_Info
            // 
            this.FpSpread_Info.Reset();
            this.FpSpread_Info.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FpSpread_Info.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FpSpread_Info.ColumnCount = 5;
            this.FpSpread_Info.RowCount = 0;
            this.FpSpread_Info.ColumnHeader.AutoText = FarPoint.Win.Spread.HeaderAutoText.Blank;
            this.FpSpread_Info.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread1.SetActiveViewport(0, 1, 0);
            // 
            // collapsibleSplitter1
            // 
            this.collapsibleSplitter1.AnimationDelay = 20;
            this.collapsibleSplitter1.AnimationStep = 20;
            this.collapsibleSplitter1.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
            this.collapsibleSplitter1.ControlToHide = null;
            this.collapsibleSplitter1.ExpandParentForm = false;
            this.collapsibleSplitter1.Location = new System.Drawing.Point(8, 8);
            this.collapsibleSplitter1.Name = "collapsibleSplitter1";
            this.collapsibleSplitter1.TabIndex = 2;
            this.collapsibleSplitter1.TabStop = false;
            this.collapsibleSplitter1.UseAnimations = false;
            this.collapsibleSplitter1.VisualStyle = Yqun.Controls.VisualStyles.DoubleDots;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(8, 8);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.btnStartMQAll);
            this.splitContainer1.Panel1.Controls.Add(this.btnReloadMQ);
            this.splitContainer1.Panel1.Controls.Add(this.btnManualUploadMQ);
            this.splitContainer1.Panel1.Controls.Add(this.btn_search);
            this.splitContainer1.Panel1.Controls.Add(this.txtWTBH);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(993, 558);
            this.splitContainer1.SplitterDistance = 201;
            this.splitContainer1.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDesc);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(0, 112);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(195, 184);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "状态说明";
            // 
            // txtDesc
            // 
            this.txtDesc.BackColor = System.Drawing.SystemColors.Control;
            this.txtDesc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDesc.Location = new System.Drawing.Point(16, 20);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(133, 82);
            this.txtDesc.TabIndex = 5;
            this.txtDesc.Text = "否: 未入库  \r\n是:入库成功 \r\n待:待入库 \r\n过:过期试验 \r\n存:值已存在  \r\n错:错误数据(附1)";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 40);
            this.label2.TabIndex = 6;
            this.label2.Text = "附1:模板,试验室编码,委托编号或者采集数据不对,或者在线的试验但传上来的DataID为空";
            // 
            // btnStartMQAll
            // 
            this.btnStartMQAll.Location = new System.Drawing.Point(70, 532);
            this.btnStartMQAll.Name = "btnStartMQAll";
            this.btnStartMQAll.Size = new System.Drawing.Size(114, 23);
            this.btnStartMQAll.TabIndex = 4;
            this.btnStartMQAll.Text = "重新加载所有队列";
            this.btnStartMQAll.UseVisualStyleBackColor = true;
            this.btnStartMQAll.Click += new System.EventHandler(this.btnStartMQAll_Click);
            // 
            // btnReloadMQ
            // 
            this.btnReloadMQ.Location = new System.Drawing.Point(16, 83);
            this.btnReloadMQ.Name = "btnReloadMQ";
            this.btnReloadMQ.Size = new System.Drawing.Size(79, 23);
            this.btnReloadMQ.TabIndex = 4;
            this.btnReloadMQ.Text = "重新入库";
            this.btnReloadMQ.UseVisualStyleBackColor = true;
            this.btnReloadMQ.Click += new System.EventHandler(this.btnReloadMQ_Click);
            // 
            // btnManualUploadMQ
            // 
            this.btnManualUploadMQ.Location = new System.Drawing.Point(70, 503);
            this.btnManualUploadMQ.Name = "btnManualUploadMQ";
            this.btnManualUploadMQ.Size = new System.Drawing.Size(114, 23);
            this.btnManualUploadMQ.TabIndex = 4;
            this.btnManualUploadMQ.Text = "启动实时队列(60)";
            this.btnManualUploadMQ.UseVisualStyleBackColor = true;
            this.btnManualUploadMQ.Click += new System.EventHandler(this.btnManualUploadMQ_Click);
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(101, 83);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(83, 23);
            this.btn_search.TabIndex = 2;
            this.btn_search.Text = "查询";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // txtWTBH
            // 
            this.txtWTBH.Location = new System.Drawing.Point(19, 42);
            this.txtWTBH.Name = "txtWTBH";
            this.txtWTBH.Size = new System.Drawing.Size(165, 21);
            this.txtWTBH.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "委托编号：";
            // 
            // UnusedTestDataView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 574);
            this.Controls.Add(this.splitContainer1);
            this.Name = "UnusedTestDataView";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查看采集数据";
            this.Load += new System.EventHandler(this.UnusedTestDataView_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FpSpread_Info)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView FpSpread_Info;
        private Yqun.Controls.CollapsibleSplitter collapsibleSplitter1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.TextBox txtWTBH;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ShowTestRoomInfo;
        private System.Windows.Forms.Button btnManualUploadMQ;
        private System.Windows.Forms.Button btnReloadMQ;
        private System.Windows.Forms.Button btnStartMQAll;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}