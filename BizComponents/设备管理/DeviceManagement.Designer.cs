namespace BizComponents
{
    partial class DeviceManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceManagement));
            this.Add = new System.Windows.Forms.Button();
            this.Close = new System.Windows.Forms.Button();
            this.Edit = new System.Windows.Forms.Button();
            this.Search = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Delete = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Detail = new System.Windows.Forms.Button();
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
            this.DataTotalLabel = new System.Windows.Forms.ToolStripLabel();
            this.Filter = new System.Windows.Forms.TextBox();
            this.SearchContainer = new System.Windows.Forms.Panel();
            this.check_all = new System.Windows.Forms.CheckBox();
            this.Deleted = new System.Windows.Forms.CheckBox();
            this.Universal = new System.Windows.Forms.CheckBox();
            this.Pressure = new System.Windows.Forms.CheckBox();
            this.Section = new System.Windows.Forms.ComboBox();
            this.Unit = new System.Windows.Forms.ComboBox();
            this.TestRoom = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Cells = new Yqun.Client.BizUI.MyCell();
            this.Sheet = new FarPoint.Win.Spread.SheetView();
            this.panel1.SuspendLayout();
            this.NavigatorBar.SuspendLayout();
            this.SearchContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Cells)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sheet)).BeginInit();
            this.SuspendLayout();
            // 
            // Add
            // 
            this.Add.Location = new System.Drawing.Point(8, 28);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(128, 33);
            this.Add.TabIndex = 1;
            this.Add.Text = "添加设备(&A)";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // Close
            // 
            this.Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Close.Location = new System.Drawing.Point(8, 606);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(131, 30);
            this.Close.TabIndex = 3;
            this.Close.Text = "关闭(&C)";
            this.Close.UseVisualStyleBackColor = true;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // Edit
            // 
            this.Edit.Location = new System.Drawing.Point(8, 67);
            this.Edit.Name = "Edit";
            this.Edit.Size = new System.Drawing.Size(128, 33);
            this.Edit.TabIndex = 4;
            this.Edit.Text = "修改设备(&E)";
            this.Edit.UseVisualStyleBackColor = true;
            this.Edit.Click += new System.EventHandler(this.Edit_Click);
            // 
            // Search
            // 
            this.Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Search.Location = new System.Drawing.Point(825, 55);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(75, 40);
            this.Search.TabIndex = 6;
            this.Search.Text = "搜索";
            this.Search.UseVisualStyleBackColor = true;
            this.Search.Click += new System.EventHandler(this.Search_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(6, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(377, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "输入 创建人/设备编码/厂家名称/公管中心编码/信息中心编码 以搜索";
            // 
            // Delete
            // 
            this.Delete.Location = new System.Drawing.Point(8, 106);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(128, 33);
            this.Delete.TabIndex = 4;
            this.Delete.Text = "删除设备(&D)";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.Add);
            this.panel1.Controls.Add(this.Edit);
            this.panel1.Controls.Add(this.Detail);
            this.panel1.Controls.Add(this.Delete);
            this.panel1.Controls.Add(this.Close);
            this.panel1.Location = new System.Drawing.Point(0, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(143, 648);
            this.panel1.TabIndex = 8;
            // 
            // Detail
            // 
            this.Detail.Location = new System.Drawing.Point(8, 145);
            this.Detail.Name = "Detail";
            this.Detail.Size = new System.Drawing.Size(128, 33);
            this.Detail.TabIndex = 4;
            this.Detail.Text = "详情(&D)";
            this.Detail.UseVisualStyleBackColor = true;
            this.Detail.Click += new System.EventHandler(this.Detail_Click);
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
            this.NavigatorBar.Location = new System.Drawing.Point(157, 622);
            this.NavigatorBar.Name = "NavigatorBar";
            this.NavigatorBar.Size = new System.Drawing.Size(918, 25);
            this.NavigatorBar.TabIndex = 9;
            this.NavigatorBar.Text = "导航栏";
            this.NavigatorBar.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.NavigatorBar_ItemClicked);
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
            // DataTotalLabel
            // 
            this.DataTotalLabel.Name = "DataTotalLabel";
            this.DataTotalLabel.Size = new System.Drawing.Size(87, 22);
            this.DataTotalLabel.Text = "共有 0 条数据 ";
            // 
            // Filter
            // 
            this.Filter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Filter.Location = new System.Drawing.Point(8, 24);
            this.Filter.Name = "Filter";
            this.Filter.Size = new System.Drawing.Size(640, 21);
            this.Filter.TabIndex = 11;
            this.Filter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Filter_KeyDown);
            // 
            // SearchContainer
            // 
            this.SearchContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchContainer.BackColor = System.Drawing.Color.WhiteSmoke;
            this.SearchContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchContainer.Controls.Add(this.check_all);
            this.SearchContainer.Controls.Add(this.Deleted);
            this.SearchContainer.Controls.Add(this.Universal);
            this.SearchContainer.Controls.Add(this.Pressure);
            this.SearchContainer.Controls.Add(this.Section);
            this.SearchContainer.Controls.Add(this.Unit);
            this.SearchContainer.Controls.Add(this.TestRoom);
            this.SearchContainer.Controls.Add(this.Filter);
            this.SearchContainer.Controls.Add(this.Search);
            this.SearchContainer.Controls.Add(this.label7);
            this.SearchContainer.Controls.Add(this.label6);
            this.SearchContainer.Controls.Add(this.label5);
            this.SearchContainer.Controls.Add(this.label2);
            this.SearchContainer.Controls.Add(this.label1);
            this.SearchContainer.Location = new System.Drawing.Point(157, 12);
            this.SearchContainer.Name = "SearchContainer";
            this.SearchContainer.Size = new System.Drawing.Size(918, 108);
            this.SearchContainer.TabIndex = 12;
            // 
            // check_all
            // 
            this.check_all.AutoSize = true;
            this.check_all.Location = new System.Drawing.Point(827, 32);
            this.check_all.Name = "check_all";
            this.check_all.Size = new System.Drawing.Size(48, 16);
            this.check_all.TabIndex = 21;
            this.check_all.Text = "全部";
            this.check_all.UseVisualStyleBackColor = true;
            this.check_all.CheckedChanged += new System.EventHandler(this.check_all_CheckedChanged);
            // 
            // Deleted
            // 
            this.Deleted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Deleted.AutoSize = true;
            this.Deleted.Location = new System.Drawing.Point(684, 76);
            this.Deleted.Name = "Deleted";
            this.Deleted.Size = new System.Drawing.Size(96, 16);
            this.Deleted.TabIndex = 20;
            this.Deleted.Text = "已删除的设备";
            this.Deleted.UseVisualStyleBackColor = true;
            // 
            // Universal
            // 
            this.Universal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Universal.AutoSize = true;
            this.Universal.Checked = true;
            this.Universal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Universal.Location = new System.Drawing.Point(684, 54);
            this.Universal.Name = "Universal";
            this.Universal.Size = new System.Drawing.Size(60, 16);
            this.Universal.TabIndex = 19;
            this.Universal.Text = "万能机";
            this.Universal.UseVisualStyleBackColor = true;
            // 
            // Pressure
            // 
            this.Pressure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Pressure.AutoSize = true;
            this.Pressure.Checked = true;
            this.Pressure.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Pressure.Location = new System.Drawing.Point(684, 32);
            this.Pressure.Name = "Pressure";
            this.Pressure.Size = new System.Drawing.Size(60, 16);
            this.Pressure.TabIndex = 19;
            this.Pressure.Text = "压力机";
            this.Pressure.UseVisualStyleBackColor = true;
            // 
            // Section
            // 
            this.Section.DisplayMember = "PrjsctName";
            this.Section.FormattingEnabled = true;
            this.Section.Location = new System.Drawing.Point(8, 75);
            this.Section.Name = "Section";
            this.Section.Size = new System.Drawing.Size(180, 20);
            this.Section.TabIndex = 17;
            this.Section.ValueMember = "PrjsctCode";
            this.Section.SelectedIndexChanged += new System.EventHandler(this.Section_SelectedIndexChanged);
            // 
            // Unit
            // 
            this.Unit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Unit.DisplayMember = "DepName";
            this.Unit.FormattingEnabled = true;
            this.Unit.Location = new System.Drawing.Point(194, 75);
            this.Unit.Name = "Unit";
            this.Unit.Size = new System.Drawing.Size(270, 20);
            this.Unit.TabIndex = 16;
            this.Unit.ValueMember = "DepCode";
            this.Unit.SelectedIndexChanged += new System.EventHandler(this.Unit_SelectedIndexChanged);
            // 
            // TestRoom
            // 
            this.TestRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TestRoom.DisplayMember = "FolderName";
            this.TestRoom.FormattingEnabled = true;
            this.TestRoom.Location = new System.Drawing.Point(470, 75);
            this.TestRoom.Name = "TestRoom";
            this.TestRoom.Size = new System.Drawing.Size(180, 20);
            this.TestRoom.TabIndex = 15;
            this.TestRoom.ValueMember = "FolderCode";
            this.TestRoom.SelectedIndexChanged += new System.EventHandler(this.TestRoom_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.DimGray;
            this.label7.Location = new System.Drawing.Point(468, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 7;
            this.label7.Text = "试验室";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.DimGray;
            this.label6.Location = new System.Drawing.Point(192, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "单位";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(6, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "标段";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(682, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "只检索";
            // 
            // Cells
            // 
            this.Cells.AccessibleDescription = "Cells, Sheet1";
            this.Cells.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Cells.BackColor = System.Drawing.Color.White;
            this.Cells.IsEditing = false;
            this.Cells.Location = new System.Drawing.Point(157, 126);
            this.Cells.Name = "Cells";
            this.Cells.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Cells.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.Sheet});
            this.Cells.Size = new System.Drawing.Size(918, 493);
            this.Cells.TabIndex = 10;
            this.Cells.Watermark = null;
            this.Cells.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.Cells_CellClick_1);
            // 
            // Sheet
            // 
            this.Sheet.Reset();
            this.Sheet.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.Sheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.Sheet.PrintInfo.PaperSize = ((System.Drawing.Printing.PaperSize)(resources.GetObject("resource.PaperSize")));
            this.Sheet.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.Sheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // DeviceManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1087, 647);
            this.Controls.Add(this.SearchContainer);
            this.Controls.Add(this.Cells);
            this.Controls.Add(this.NavigatorBar);
            this.Controls.Add(this.panel1);
            this.Name = "DeviceManagement";
            this.Text = "设备管理";
            this.Load += new System.EventHandler(this.DeviceManager_Load);
            this.panel1.ResumeLayout(false);
            this.NavigatorBar.ResumeLayout(false);
            this.NavigatorBar.PerformLayout();
            this.SearchContainer.ResumeLayout(false);
            this.SearchContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Cells)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sheet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.Button Edit;
        private System.Windows.Forms.Button Search;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.Panel panel1;
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
        private Yqun.Client.BizUI.MyCell Cells;
        private System.Windows.Forms.Button Detail;
        private System.Windows.Forms.TextBox Filter;
        private FarPoint.Win.Spread.SheetView Sheet;
        private System.Windows.Forms.Panel SearchContainer;
        private System.Windows.Forms.ComboBox Section;
        private System.Windows.Forms.ComboBox Unit;
        private System.Windows.Forms.ComboBox TestRoom;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox Deleted;
        private System.Windows.Forms.CheckBox Universal;
        private System.Windows.Forms.CheckBox Pressure;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripLabel DataTotalLabel;
        private System.Windows.Forms.CheckBox check_all;
    }
}