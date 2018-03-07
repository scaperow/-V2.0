namespace BizComponents
{
    partial class StatisticsManagement
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
            FarPoint.Win.Spread.CellType.HyperLinkCellType hyperLinkCellType1 = new FarPoint.Win.Spread.CellType.HyperLinkCellType();
            FarPoint.Win.Spread.CellType.GeneralCellType generalCellType1 = new FarPoint.Win.Spread.CellType.GeneralCellType();
            FarPoint.Win.Spread.CellType.ComboBoxCellType comboBoxCellType1 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            FarPoint.Win.Spread.CellType.GeneralCellType generalCellType2 = new FarPoint.Win.Spread.CellType.GeneralCellType();
            FarPoint.Win.Spread.CellType.ButtonCellType buttonCellType1 = new FarPoint.Win.Spread.CellType.ButtonCellType();
            FarPoint.Win.Spread.CellType.HyperLinkCellType hyperLinkCellType2 = new FarPoint.Win.Spread.CellType.HyperLinkCellType();
            FarPoint.Win.Spread.CellType.HyperLinkCellType hyperLinkCellType3 = new FarPoint.Win.Spread.CellType.HyperLinkCellType();
            this.ComboModules = new System.Windows.Forms.ComboBox();
            this.TextModuleName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TableModules = new FarPoint.Win.Spread.FpSpread();
            this.TableModules_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.TableStatistics = new FarPoint.Win.Spread.FpSpread();
            this.TableStatistics_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.ButtonNew = new System.Windows.Forms.Button();
            this.ButtonModify = new System.Windows.Forms.Button();
            this.ButtonDelete = new System.Windows.Forms.Button();
            this.LinkNewStatistics = new System.Windows.Forms.LinkLabel();
            this.LinkModifyStatistics = new System.Windows.Forms.LinkLabel();
            this.LinkDeleteStatistics = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.TableModules)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TableModules_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TableStatistics)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TableStatistics_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // ComboModules
            // 
            this.ComboModules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboModules.DisplayMember = "ItemName";
            this.ComboModules.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboModules.FormattingEnabled = true;
            this.ComboModules.Location = new System.Drawing.Point(355, 13);
            this.ComboModules.Name = "ComboModules";
            this.ComboModules.Size = new System.Drawing.Size(392, 20);
            this.ComboModules.TabIndex = 20;
            this.ComboModules.ValueMember = "ItemID";
            this.ComboModules.SelectedIndexChanged += new System.EventHandler(this.ComboModules_SelectedIndexChanged);
            // 
            // TextModuleName
            // 
            this.TextModuleName.Location = new System.Drawing.Point(81, 12);
            this.TextModuleName.Name = "TextModuleName";
            this.TextModuleName.ReadOnly = true;
            this.TextModuleName.Size = new System.Drawing.Size(221, 21);
            this.TextModuleName.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(308, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "分类：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(10, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "当前模板：";
            // 
            // TableModules
            // 
            this.TableModules.AccessibleDescription = "TableModules, Sheet1, Row 0, Column 0, ";
            this.TableModules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TableModules.BackColor = System.Drawing.SystemColors.Control;
            this.TableModules.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TableModules.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.TableModules.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.TableModules.Location = new System.Drawing.Point(12, 51);
            this.TableModules.Name = "TableModules";
            this.TableModules.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TableModules.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.TableModules.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.TableModules_Sheet1});
            this.TableModules.Size = new System.Drawing.Size(735, 195);
            this.TableModules.TabIndex = 21;
            this.TableModules.TabStripInsertTab = false;
            this.TableModules.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            this.TableModules.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.TableModules.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.TableModules_CellClick);
            this.TableModules.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(this.TableModules_SelectionChanged);
            this.TableModules.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.TableModules_ButtonClicked);
            // 
            // TableModules_Sheet1
            // 
            this.TableModules_Sheet1.Reset();
            this.TableModules_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.TableModules_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.TableModules_Sheet1.ColumnCount = 2;
            this.TableModules_Sheet1.RowCount = 1;
            this.TableModules_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "该分类下关联的模板";
            this.TableModules_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "删除";
            this.TableModules_Sheet1.Columns.Get(0).Label = "该分类下关联的模板";
            this.TableModules_Sheet1.Columns.Get(0).Width = 415F;
            hyperLinkCellType1.LinkArea = new System.Windows.Forms.LinkArea(0, 2);
            hyperLinkCellType1.LinkAreas = new System.Windows.Forms.LinkArea[] {
        new System.Windows.Forms.LinkArea(0, 2)};
            hyperLinkCellType1.LinkColor = System.Drawing.Color.Red;
            hyperLinkCellType1.Text = "删除";
            hyperLinkCellType1.VisitedLinkColor = System.Drawing.Color.DarkRed;
            this.TableModules_Sheet1.Columns.Get(1).CellType = hyperLinkCellType1;
            this.TableModules_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.TableModules_Sheet1.Columns.Get(1).Label = "删除";
            this.TableModules_Sheet1.Columns.Get(1).Locked = true;
            this.TableModules_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.TableModules_Sheet1.Columns.Get(1).Width = 102F;
            this.TableModules_Sheet1.DefaultStyle.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.TableModules_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.TableModules_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.TableModules_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.TableModules_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.TableModules_Sheet1.RowHeader.Visible = false;
            this.TableModules_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.TableModules_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.TableModules_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // TableStatistics
            // 
            this.TableStatistics.AccessibleDescription = "TableStatistics, Sheet1";
            this.TableStatistics.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TableStatistics.BackColor = System.Drawing.SystemColors.Control;
            this.TableStatistics.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TableStatistics.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.TableStatistics.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.TableStatistics.Location = new System.Drawing.Point(12, 252);
            this.TableStatistics.Name = "TableStatistics";
            this.TableStatistics.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TableStatistics.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.TableStatistics.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.TableStatistics_Sheet1});
            this.TableStatistics.Size = new System.Drawing.Size(734, 363);
            this.TableStatistics.TabIndex = 21;
            this.TableStatistics.TabStripInsertTab = false;
            this.TableStatistics.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            this.TableStatistics.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.TableStatistics.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.TableStatistics_CellClick);
            this.TableStatistics.Change += new FarPoint.Win.Spread.ChangeEventHandler(this.TableStatistics_Change);
            this.TableStatistics.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.TableStatistics_ButtonClicked);
            // 
            // TableStatistics_Sheet1
            // 
            this.TableStatistics_Sheet1.Reset();
            this.TableStatistics_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.TableStatistics_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.TableStatistics_Sheet1.ColumnCount = 7;
            this.TableStatistics_Sheet1.RowCount = 0;
            this.TableStatistics_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "统计项/标准值";
            this.TableStatistics_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "数据类型";
            this.TableStatistics_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "表单";
            this.TableStatistics_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "单元格";
            this.TableStatistics_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "设置";
            this.TableStatistics_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "添加";
            this.TableStatistics_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "删除/清除";
            this.TableStatistics_Sheet1.Columns.Get(0).CellType = generalCellType1;
            this.TableStatistics_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.TableStatistics_Sheet1.Columns.Get(0).Label = "统计项/标准值";
            this.TableStatistics_Sheet1.Columns.Get(0).Locked = false;
            this.TableStatistics_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.TableStatistics_Sheet1.Columns.Get(0).Width = 112F;
            comboBoxCellType1.ButtonAlign = FarPoint.Win.ButtonAlign.Right;
            comboBoxCellType1.Items = new string[] {
        "项目类型",
        "厂家名称",
        "报告编号",
        "报告日期",
        "强度等级",
        "品种等级",
        "施工部位",
        "数量",
        "组值",
        "小数点1",
        "小数点2",
        "小数点3",
        "小数点4",
        "小数点5",
        "小数点6",
        "小数点7",
        "小数点8",
        "小数点9",
        "小数点10",
        "小数点11",
        "小数点12",
        "小数点13",
        "小数点14",
        "小数点15",
        "小数点16",
        "小数点17",
        "小数点18",
        "小数点19",
        "时间1",
        "时间2",
        "时间3",
        "时间4",
        "时间5",
        "文本1",
        "文本2",
        "文本3",
        "文本4",
        "文本5",
        "文本6",
        "文本7",
        "文本8",
        "文本9",
        "文本10",
        "文本11",
        "文本12",
        "文本13",
        "文本14",
        "文本15",
        "文本16"};
            this.TableStatistics_Sheet1.Columns.Get(1).CellType = comboBoxCellType1;
            this.TableStatistics_Sheet1.Columns.Get(1).Label = "数据类型";
            this.TableStatistics_Sheet1.Columns.Get(1).Width = 121F;
            this.TableStatistics_Sheet1.Columns.Get(2).Label = "表单";
            this.TableStatistics_Sheet1.Columns.Get(2).Locked = true;
            this.TableStatistics_Sheet1.Columns.Get(2).Width = 126F;
            this.TableStatistics_Sheet1.Columns.Get(3).CellType = generalCellType2;
            this.TableStatistics_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.TableStatistics_Sheet1.Columns.Get(3).Label = "单元格";
            this.TableStatistics_Sheet1.Columns.Get(3).Locked = true;
            this.TableStatistics_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.TableStatistics_Sheet1.Columns.Get(3).Width = 97F;
            buttonCellType1.ButtonColor2 = System.Drawing.SystemColors.ButtonFace;
            buttonCellType1.Text = "设置";
            this.TableStatistics_Sheet1.Columns.Get(4).CellType = buttonCellType1;
            this.TableStatistics_Sheet1.Columns.Get(4).Label = "设置";
            this.TableStatistics_Sheet1.Columns.Get(4).Resizable = false;
            this.TableStatistics_Sheet1.Columns.Get(4).Width = 72F;
            hyperLinkCellType2.LinkArea = new System.Windows.Forms.LinkArea(0, 2);
            hyperLinkCellType2.LinkAreas = new System.Windows.Forms.LinkArea[] {
        new System.Windows.Forms.LinkArea(0, 2)};
            hyperLinkCellType2.Text = "添加";
            this.TableStatistics_Sheet1.Columns.Get(5).CellType = hyperLinkCellType2;
            this.TableStatistics_Sheet1.Columns.Get(5).Label = "添加";
            this.TableStatistics_Sheet1.Columns.Get(5).Resizable = false;
            this.TableStatistics_Sheet1.Columns.Get(5).Width = 66F;
            hyperLinkCellType3.LinkArea = new System.Windows.Forms.LinkArea(0, 2);
            hyperLinkCellType3.LinkAreas = new System.Windows.Forms.LinkArea[] {
        new System.Windows.Forms.LinkArea(0, 2)};
            hyperLinkCellType3.Text = "删除";
            this.TableStatistics_Sheet1.Columns.Get(6).CellType = hyperLinkCellType3;
            this.TableStatistics_Sheet1.Columns.Get(6).Label = "删除/清除";
            this.TableStatistics_Sheet1.Columns.Get(6).Resizable = false;
            this.TableStatistics_Sheet1.Columns.Get(6).Width = 102F;
            this.TableStatistics_Sheet1.DefaultStyle.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.TableStatistics_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.TableStatistics_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.TableStatistics_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.TableStatistics_Sheet1.RowHeader.Visible = false;
            this.TableStatistics_Sheet1.CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(this.TableStatistics_Sheet1_CellChanged);
            this.TableStatistics_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.TableStatistics.SetActiveViewport(0, 1, 0);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonCancel.Location = new System.Drawing.Point(678, 621);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 23;
            this.ButtonCancel.Text = "关闭";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // ButtonOk
            // 
            this.ButtonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonOk.Location = new System.Drawing.Point(597, 621);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(75, 23);
            this.ButtonOk.TabIndex = 22;
            this.ButtonOk.Text = "保存";
            this.ButtonOk.UseVisualStyleBackColor = true;
            this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // ButtonNew
            // 
            this.ButtonNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonNew.Location = new System.Drawing.Point(12, 621);
            this.ButtonNew.Name = "ButtonNew";
            this.ButtonNew.Size = new System.Drawing.Size(75, 23);
            this.ButtonNew.TabIndex = 22;
            this.ButtonNew.Text = "新增";
            this.ButtonNew.UseVisualStyleBackColor = true;
            this.ButtonNew.Visible = false;
            this.ButtonNew.Click += new System.EventHandler(this.ButtonNew_Click);
            // 
            // ButtonModify
            // 
            this.ButtonModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonModify.Location = new System.Drawing.Point(93, 621);
            this.ButtonModify.Name = "ButtonModify";
            this.ButtonModify.Size = new System.Drawing.Size(75, 23);
            this.ButtonModify.TabIndex = 22;
            this.ButtonModify.Text = "修改";
            this.ButtonModify.UseVisualStyleBackColor = true;
            this.ButtonModify.Visible = false;
            this.ButtonModify.Click += new System.EventHandler(this.ButtonModify_Click);
            // 
            // ButtonDelete
            // 
            this.ButtonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonDelete.Location = new System.Drawing.Point(174, 621);
            this.ButtonDelete.Name = "ButtonDelete";
            this.ButtonDelete.Size = new System.Drawing.Size(75, 23);
            this.ButtonDelete.TabIndex = 22;
            this.ButtonDelete.Text = "删除";
            this.ButtonDelete.UseVisualStyleBackColor = true;
            this.ButtonDelete.Visible = false;
            this.ButtonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // LinkNewStatistics
            // 
            this.LinkNewStatistics.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LinkNewStatistics.AutoSize = true;
            this.LinkNewStatistics.Location = new System.Drawing.Point(575, 36);
            this.LinkNewStatistics.Name = "LinkNewStatistics";
            this.LinkNewStatistics.Size = new System.Drawing.Size(53, 12);
            this.LinkNewStatistics.TabIndex = 24;
            this.LinkNewStatistics.TabStop = true;
            this.LinkNewStatistics.Text = "添加分类";
            this.LinkNewStatistics.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkNewStatistics_LinkClicked);
            // 
            // LinkModifyStatistics
            // 
            this.LinkModifyStatistics.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LinkModifyStatistics.AutoSize = true;
            this.LinkModifyStatistics.Location = new System.Drawing.Point(634, 36);
            this.LinkModifyStatistics.Name = "LinkModifyStatistics";
            this.LinkModifyStatistics.Size = new System.Drawing.Size(53, 12);
            this.LinkModifyStatistics.TabIndex = 24;
            this.LinkModifyStatistics.TabStop = true;
            this.LinkModifyStatistics.Text = "修改分类";
            this.LinkModifyStatistics.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkModifyStatistics_LinkClicked);
            // 
            // LinkDeleteStatistics
            // 
            this.LinkDeleteStatistics.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LinkDeleteStatistics.AutoSize = true;
            this.LinkDeleteStatistics.Location = new System.Drawing.Point(693, 36);
            this.LinkDeleteStatistics.Name = "LinkDeleteStatistics";
            this.LinkDeleteStatistics.Size = new System.Drawing.Size(53, 12);
            this.LinkDeleteStatistics.TabIndex = 24;
            this.LinkDeleteStatistics.TabStop = true;
            this.LinkDeleteStatistics.Text = "删除分类";
            this.LinkDeleteStatistics.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkDeleteStatistics_LinkClicked);
            // 
            // StatisticsManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 656);
            this.Controls.Add(this.LinkDeleteStatistics);
            this.Controls.Add(this.LinkModifyStatistics);
            this.Controls.Add(this.LinkNewStatistics);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonDelete);
            this.Controls.Add(this.ButtonModify);
            this.Controls.Add(this.ButtonNew);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.TableStatistics);
            this.Controls.Add(this.TableModules);
            this.Controls.Add(this.ComboModules);
            this.Controls.Add(this.TextModuleName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "StatisticsManagement";
            this.Text = "统计项模板设置";
            this.Load += new System.EventHandler(this.StatisticsManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TableModules)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TableModules_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TableStatistics)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TableStatistics_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ComboModules;
        private System.Windows.Forms.TextBox TextModuleName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private FarPoint.Win.Spread.FpSpread TableModules;
        private FarPoint.Win.Spread.SheetView TableModules_Sheet1;
        private FarPoint.Win.Spread.FpSpread TableStatistics;
        private FarPoint.Win.Spread.SheetView TableStatistics_Sheet1;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Button ButtonNew;
        private System.Windows.Forms.Button ButtonModify;
        private System.Windows.Forms.Button ButtonDelete;
        private System.Windows.Forms.LinkLabel LinkNewStatistics;
        private System.Windows.Forms.LinkLabel LinkModifyStatistics;
        private System.Windows.Forms.LinkLabel LinkDeleteStatistics;
    }
}