namespace BizComponents
{
    partial class StadiumItemSelector
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.lView_ExtentDataItems = new System.Windows.Forms.ListView();
            this.columnHeader_Name = new System.Windows.Forms.ColumnHeader();
            this.columnHeader_Description = new System.Windows.Forms.ColumnHeader();
            this.columnHeader_FieldType = new System.Windows.Forms.ColumnHeader();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tBox_DataItemName = new System.Windows.Forms.TextBox();
            this.cBox_FieldTypes = new System.Windows.Forms.ComboBox();
            this.Button_Append = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.Button_Delete = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tBox_DataItemText = new System.Windows.Forms.TextBox();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Button_Ok = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            this.TabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.TabPage1);
            this.tabControl1.Controls.Add(this.TabPage2);
            this.tabControl1.Location = new System.Drawing.Point(8, 10);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(771, 522);
            this.tabControl1.TabIndex = 9;
            // 
            // TabPage1
            // 
            this.TabPage1.Controls.Add(this.fpSpread1);
            this.TabPage1.Controls.Add(this.label3);
            this.TabPage1.Controls.Add(this.label2);
            this.TabPage1.Controls.Add(this.label1);
            this.TabPage1.Location = new System.Drawing.Point(4, 22);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(5);
            this.TabPage1.Size = new System.Drawing.Size(763, 496);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "模板数据项";
            this.TabPage1.UseVisualStyleBackColor = true;
            // 
            // fpSpread1
            // 
            this.fpSpread1.AccessibleDescription = "";
            this.fpSpread1.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Top;
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.Location = new System.Drawing.Point(5, 5);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.AsNeeded;
            this.fpSpread1.Size = new System.Drawing.Size(753, 457);
            this.fpSpread1.TabIndex = 8;
            this.fpSpread1.TabStripInsertTab = false;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(166, 469);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "颜色的为数据项。";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(121, 469);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "     ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(9, 469);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "选择说明：背景为";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TabPage2
            // 
            this.TabPage2.Controls.Add(this.lView_ExtentDataItems);
            this.TabPage2.Controls.Add(this.groupBox1);
            this.TabPage2.Location = new System.Drawing.Point(4, 22);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(5);
            this.TabPage2.Size = new System.Drawing.Size(763, 496);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "表外数据项";
            this.TabPage2.UseVisualStyleBackColor = true;
            // 
            // lView_ExtentDataItems
            // 
            this.lView_ExtentDataItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_Name,
            this.columnHeader_Description,
            this.columnHeader_FieldType});
            this.lView_ExtentDataItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lView_ExtentDataItems.FullRowSelect = true;
            this.lView_ExtentDataItems.HideSelection = false;
            this.lView_ExtentDataItems.Location = new System.Drawing.Point(5, 5);
            this.lView_ExtentDataItems.MultiSelect = false;
            this.lView_ExtentDataItems.Name = "lView_ExtentDataItems";
            this.lView_ExtentDataItems.Size = new System.Drawing.Size(508, 486);
            this.lView_ExtentDataItems.TabIndex = 0;
            this.lView_ExtentDataItems.UseCompatibleStateImageBehavior = false;
            this.lView_ExtentDataItems.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader_Name
            // 
            this.columnHeader_Name.Text = "名称";
            this.columnHeader_Name.Width = 117;
            // 
            // columnHeader_Description
            // 
            this.columnHeader_Description.Text = "描述信息";
            this.columnHeader_Description.Width = 191;
            // 
            // columnHeader_FieldType
            // 
            this.columnHeader_FieldType.Text = "数据类型";
            this.columnHeader_FieldType.Width = 100;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tBox_DataItemName);
            this.groupBox1.Controls.Add(this.cBox_FieldTypes);
            this.groupBox1.Controls.Add(this.Button_Append);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.Button_Delete);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tBox_DataItemText);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(513, 5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(245, 486);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            // 
            // tBox_DataItemName
            // 
            this.tBox_DataItemName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tBox_DataItemName.Location = new System.Drawing.Point(81, 21);
            this.tBox_DataItemName.Name = "tBox_DataItemName";
            this.tBox_DataItemName.Size = new System.Drawing.Size(156, 21);
            this.tBox_DataItemName.TabIndex = 11;
            // 
            // cBox_FieldTypes
            // 
            this.cBox_FieldTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cBox_FieldTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBox_FieldTypes.FormattingEnabled = true;
            this.cBox_FieldTypes.Location = new System.Drawing.Point(81, 75);
            this.cBox_FieldTypes.Name = "cBox_FieldTypes";
            this.cBox_FieldTypes.Size = new System.Drawing.Size(156, 20);
            this.cBox_FieldTypes.TabIndex = 16;
            // 
            // Button_Append
            // 
            this.Button_Append.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Append.Location = new System.Drawing.Point(12, 115);
            this.Button_Append.Name = "Button_Append";
            this.Button_Append.Size = new System.Drawing.Size(70, 20);
            this.Button_Append.TabIndex = 9;
            this.Button_Append.Text = "添加";
            this.Button_Append.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "数据类型：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button_Delete
            // 
            this.Button_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Delete.Location = new System.Drawing.Point(96, 115);
            this.Button_Delete.Name = "Button_Delete";
            this.Button_Delete.Size = new System.Drawing.Size(70, 20);
            this.Button_Delete.TabIndex = 10;
            this.Button_Delete.Text = "删除";
            this.Button_Delete.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "描述信息：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "名    称：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tBox_DataItemText
            // 
            this.tBox_DataItemText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tBox_DataItemText.Location = new System.Drawing.Point(81, 48);
            this.tBox_DataItemText.Name = "tBox_DataItemText";
            this.tBox_DataItemText.Size = new System.Drawing.Size(156, 21);
            this.tBox_DataItemText.TabIndex = 13;
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Location = new System.Drawing.Point(631, 543);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 11;
            this.Button_Cancel.Text = "取消";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Button_Ok
            // 
            this.Button_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Button_Ok.Location = new System.Drawing.Point(543, 543);
            this.Button_Ok.Name = "Button_Ok";
            this.Button_Ok.Size = new System.Drawing.Size(75, 23);
            this.Button_Ok.TabIndex = 10;
            this.Button_Ok.Text = "确定";
            this.Button_Ok.UseVisualStyleBackColor = true;
            this.Button_Ok.Click += new System.EventHandler(this.Button_Ok_Click);
            // 
            // StadiumItemSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 577);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_Ok);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StadiumItemSelector";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择数据项";
            this.Load += new System.EventHandler(this.StadiumItemSelector_Load);
            this.tabControl1.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            this.TabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage TabPage1;
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage TabPage2;
        private System.Windows.Forms.ListView lView_ExtentDataItems;
        private System.Windows.Forms.ColumnHeader columnHeader_Name;
        private System.Windows.Forms.ColumnHeader columnHeader_Description;
        private System.Windows.Forms.ColumnHeader columnHeader_FieldType;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tBox_DataItemName;
        private System.Windows.Forms.ComboBox cBox_FieldTypes;
        private System.Windows.Forms.Button Button_Append;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button Button_Delete;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tBox_DataItemText;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Button Button_Ok;
    }
}