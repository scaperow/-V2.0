namespace BizComponents
{
    partial class ReadDataFunctionWizard
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
            this.wizardPage1 = new IS.DNS.WinUI.Wizard.WizardPage();
            this.Biz_DataTableList = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.TextBox_FunctionName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.wizardPage3 = new IS.DNS.WinUI.Wizard.WizardPage();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.ConditionList = new FarPoint.Win.Spread.FpSpread();
            this.ConditionList_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.Button_RemoveRow = new System.Windows.Forms.Button();
            this.Button_AddRow = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.WriteToTable_DataItem_Editor = new System.Windows.Forms.ComboBox();
            this.WriteToTable_Condition_Editor = new System.Windows.Forms.ComboBox();
            this.WriteToTable_Value_Editor = new Yqun.Controls.ComboTree();
            this.wizardPage4 = new IS.DNS.WinUI.Wizard.WizardPage();
            this.ModificationList = new FarPoint.Win.Spread.FpSpread();
            this.ModificationList_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.Button_DeleteValue = new System.Windows.Forms.Button();
            this.Button_AddValue = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.Modification_DataItem_Editor = new System.Windows.Forms.ComboBox();
            this.ModificationType_Editor = new System.Windows.Forms.ComboBox();
            this.ModificationValue_Editor = new Yqun.Controls.ComboTree();
            this.wizardPage5 = new IS.DNS.WinUI.Wizard.WizardPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ReportInfomation = new System.Windows.Forms.Label();
            this.wizardPage2 = new IS.DNS.WinUI.Wizard.WizardPage();
            this.EventListView = new System.Windows.Forms.TreeView();
            this.label6 = new System.Windows.Forms.Label();
            this.MyWizard.SuspendLayout();
            this.wizardPage1.SuspendLayout();
            this.wizardPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConditionList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConditionList_Sheet1)).BeginInit();
            this.wizardPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ModificationList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ModificationList_Sheet1)).BeginInit();
            this.wizardPage5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.wizardPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // MyWizard
            // 
            this.MyWizard.Controls.Add(this.wizardPage2);
            this.MyWizard.Controls.Add(this.wizardPage1);
            this.MyWizard.Controls.Add(this.wizardPage3);
            this.MyWizard.Controls.Add(this.wizardPage4);
            this.MyWizard.Controls.Add(this.wizardPage5);
            this.MyWizard.Pages.AddRange(new IS.DNS.WinUI.Wizard.WizardPage[] {
            this.wizardPage1,
            this.wizardPage2,
            this.wizardPage3,
            this.wizardPage4,
            this.wizardPage5});
            this.MyWizard.Size = new System.Drawing.Size(569, 463);
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.Biz_DataTableList);
            this.wizardPage1.Controls.Add(this.label2);
            this.wizardPage1.Controls.Add(this.TextBox_FunctionName);
            this.wizardPage1.Controls.Add(this.label1);
            this.wizardPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPage1.IsFinishPage = false;
            this.wizardPage1.Location = new System.Drawing.Point(0, 0);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Size = new System.Drawing.Size(569, 415);
            this.wizardPage1.TabIndex = 1;
            // 
            // Biz_DataTableList
            // 
            this.Biz_DataTableList.FullRowSelect = true;
            this.Biz_DataTableList.HideSelection = false;
            this.Biz_DataTableList.Location = new System.Drawing.Point(9, 79);
            this.Biz_DataTableList.Name = "Biz_DataTableList";
            this.Biz_DataTableList.Size = new System.Drawing.Size(551, 325);
            this.Biz_DataTableList.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "选择一个读取数据的数据源：";
            // 
            // TextBox_FunctionName
            // 
            this.TextBox_FunctionName.Location = new System.Drawing.Point(9, 29);
            this.TextBox_FunctionName.Name = "TextBox_FunctionName";
            this.TextBox_FunctionName.Size = new System.Drawing.Size(551, 21);
            this.TextBox_FunctionName.TabIndex = 9;
            this.TextBox_FunctionName.Text = "请输入函数名";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "函数名称：";
            // 
            // wizardPage3
            // 
            this.wizardPage3.Controls.Add(this.numericUpDown1);
            this.wizardPage3.Controls.Add(this.label7);
            this.wizardPage3.Controls.Add(this.ConditionList);
            this.wizardPage3.Controls.Add(this.Button_RemoveRow);
            this.wizardPage3.Controls.Add(this.Button_AddRow);
            this.wizardPage3.Controls.Add(this.label3);
            this.wizardPage3.Controls.Add(this.WriteToTable_DataItem_Editor);
            this.wizardPage3.Controls.Add(this.WriteToTable_Condition_Editor);
            this.wizardPage3.Controls.Add(this.WriteToTable_Value_Editor);
            this.wizardPage3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPage3.IsFinishPage = false;
            this.wizardPage3.Location = new System.Drawing.Point(0, 0);
            this.wizardPage3.Name = "wizardPage3";
            this.wizardPage3.Size = new System.Drawing.Size(569, 415);
            this.wizardPage3.TabIndex = 2;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Enabled = false;
            this.numericUpDown1.Location = new System.Drawing.Point(502, 381);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(59, 21);
            this.numericUpDown1.TabIndex = 18;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(499, 362);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Top N 取值：";
            // 
            // ConditionList
            // 
            this.ConditionList.AccessibleDescription = "ConditionList, Sheet1, Row 0, Column 0, ";
            this.ConditionList.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.ConditionList.Location = new System.Drawing.Point(9, 28);
            this.ConditionList.Name = "ConditionList";
            this.ConditionList.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.ConditionList.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.ConditionList_Sheet1});
            this.ConditionList.Size = new System.Drawing.Size(488, 376);
            this.ConditionList.TabIndex = 13;
            this.ConditionList.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // ConditionList_Sheet1
            // 
            this.ConditionList_Sheet1.Reset();
            this.ConditionList_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.ConditionList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.ConditionList_Sheet1.ColumnCount = 3;
            this.ConditionList_Sheet1.RowCount = 100;
            this.ConditionList_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "数据源-数据项";
            this.ConditionList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "条件";
            this.ConditionList_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "条件值";
            this.ConditionList_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.ConditionList_Sheet1.Columns.Get(0).Label = "数据源-数据项";
            this.ConditionList_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.ConditionList_Sheet1.Columns.Get(0).Width = 185F;
            this.ConditionList_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.ConditionList_Sheet1.Columns.Get(1).Label = "条件";
            this.ConditionList_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.ConditionList_Sheet1.Columns.Get(1).Width = 100F;
            this.ConditionList_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.ConditionList_Sheet1.Columns.Get(2).Label = "条件值";
            this.ConditionList_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.ConditionList_Sheet1.Columns.Get(2).Width = 182F;
            this.ConditionList_Sheet1.RowHeader.AutoText = FarPoint.Win.Spread.HeaderAutoText.Blank;
            this.ConditionList_Sheet1.RowHeader.Visible = false;
            this.ConditionList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // Button_RemoveRow
            // 
            this.Button_RemoveRow.Location = new System.Drawing.Point(502, 59);
            this.Button_RemoveRow.Name = "Button_RemoveRow";
            this.Button_RemoveRow.Size = new System.Drawing.Size(59, 23);
            this.Button_RemoveRow.TabIndex = 12;
            this.Button_RemoveRow.Text = "删除行";
            this.Button_RemoveRow.UseVisualStyleBackColor = true;
            // 
            // Button_AddRow
            // 
            this.Button_AddRow.Location = new System.Drawing.Point(502, 31);
            this.Button_AddRow.Name = "Button_AddRow";
            this.Button_AddRow.Size = new System.Drawing.Size(59, 23);
            this.Button_AddRow.TabIndex = 11;
            this.Button_AddRow.Text = "添加行";
            this.Button_AddRow.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "设置筛选条件说明";
            // 
            // WriteToTable_DataItem_Editor
            // 
            this.WriteToTable_DataItem_Editor.DropDownHeight = 160;
            this.WriteToTable_DataItem_Editor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WriteToTable_DataItem_Editor.FormattingEnabled = true;
            this.WriteToTable_DataItem_Editor.IntegralHeight = false;
            this.WriteToTable_DataItem_Editor.Location = new System.Drawing.Point(47, 223);
            this.WriteToTable_DataItem_Editor.Name = "WriteToTable_DataItem_Editor";
            this.WriteToTable_DataItem_Editor.Size = new System.Drawing.Size(203, 21);
            this.WriteToTable_DataItem_Editor.TabIndex = 16;
            // 
            // WriteToTable_Condition_Editor
            // 
            this.WriteToTable_Condition_Editor.DropDownHeight = 160;
            this.WriteToTable_Condition_Editor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WriteToTable_Condition_Editor.DropDownWidth = 120;
            this.WriteToTable_Condition_Editor.FormattingEnabled = true;
            this.WriteToTable_Condition_Editor.IntegralHeight = false;
            this.WriteToTable_Condition_Editor.Location = new System.Drawing.Point(195, 155);
            this.WriteToTable_Condition_Editor.Name = "WriteToTable_Condition_Editor";
            this.WriteToTable_Condition_Editor.Size = new System.Drawing.Size(101, 21);
            this.WriteToTable_Condition_Editor.TabIndex = 14;
            // 
            // WriteToTable_Value_Editor
            // 
            this.WriteToTable_Value_Editor.Location = new System.Drawing.Point(323, 215);
            this.WriteToTable_Value_Editor.Name = "WriteToTable_Value_Editor";
            this.WriteToTable_Value_Editor.Size = new System.Drawing.Size(122, 20);
            this.WriteToTable_Value_Editor.TabIndex = 15;
            this.WriteToTable_Value_Editor.Value = null;
            // 
            // wizardPage4
            // 
            this.wizardPage4.Controls.Add(this.ModificationList);
            this.wizardPage4.Controls.Add(this.Button_DeleteValue);
            this.wizardPage4.Controls.Add(this.Button_AddValue);
            this.wizardPage4.Controls.Add(this.label4);
            this.wizardPage4.Controls.Add(this.Modification_DataItem_Editor);
            this.wizardPage4.Controls.Add(this.ModificationType_Editor);
            this.wizardPage4.Controls.Add(this.ModificationValue_Editor);
            this.wizardPage4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPage4.IsFinishPage = false;
            this.wizardPage4.Location = new System.Drawing.Point(0, 0);
            this.wizardPage4.Name = "wizardPage4";
            this.wizardPage4.Size = new System.Drawing.Size(569, 415);
            this.wizardPage4.TabIndex = 3;
            // 
            // ModificationList
            // 
            this.ModificationList.AccessibleDescription = "";
            this.ModificationList.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.ModificationList.Location = new System.Drawing.Point(9, 28);
            this.ModificationList.Name = "ModificationList";
            this.ModificationList.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.ModificationList.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.ModificationList_Sheet1});
            this.ModificationList.Size = new System.Drawing.Size(488, 376);
            this.ModificationList.TabIndex = 15;
            this.ModificationList.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // ModificationList_Sheet1
            // 
            this.ModificationList_Sheet1.Reset();
            this.ModificationList_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.ModificationList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.ModificationList_Sheet1.ColumnCount = 3;
            this.ModificationList_Sheet1.RowCount = 100;
            this.ModificationList_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "数据源-数据项";
            this.ModificationList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "修改类型";
            this.ModificationList_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "本表单";
            this.ModificationList_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.ModificationList_Sheet1.Columns.Get(0).Label = "数据源-数据项";
            this.ModificationList_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.ModificationList_Sheet1.Columns.Get(0).Width = 185F;
            this.ModificationList_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.ModificationList_Sheet1.Columns.Get(1).Label = "修改类型";
            this.ModificationList_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.ModificationList_Sheet1.Columns.Get(1).Width = 100F;
            this.ModificationList_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.ModificationList_Sheet1.Columns.Get(2).Label = "本表单";
            this.ModificationList_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.ModificationList_Sheet1.Columns.Get(2).Width = 182F;
            this.ModificationList_Sheet1.RowHeader.AutoText = FarPoint.Win.Spread.HeaderAutoText.Blank;
            this.ModificationList_Sheet1.RowHeader.Visible = false;
            this.ModificationList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // Button_DeleteValue
            // 
            this.Button_DeleteValue.Location = new System.Drawing.Point(502, 59);
            this.Button_DeleteValue.Name = "Button_DeleteValue";
            this.Button_DeleteValue.Size = new System.Drawing.Size(59, 23);
            this.Button_DeleteValue.TabIndex = 14;
            this.Button_DeleteValue.Text = "删除行";
            this.Button_DeleteValue.UseVisualStyleBackColor = true;
            // 
            // Button_AddValue
            // 
            this.Button_AddValue.Location = new System.Drawing.Point(502, 31);
            this.Button_AddValue.Name = "Button_AddValue";
            this.Button_AddValue.Size = new System.Drawing.Size(59, 23);
            this.Button_AddValue.TabIndex = 13;
            this.Button_AddValue.Text = "添加行";
            this.Button_AddValue.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "读取对应关系说明";
            // 
            // Modification_DataItem_Editor
            // 
            this.Modification_DataItem_Editor.DropDownHeight = 160;
            this.Modification_DataItem_Editor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Modification_DataItem_Editor.FormattingEnabled = true;
            this.Modification_DataItem_Editor.IntegralHeight = false;
            this.Modification_DataItem_Editor.Location = new System.Drawing.Point(26, 121);
            this.Modification_DataItem_Editor.Name = "Modification_DataItem_Editor";
            this.Modification_DataItem_Editor.Size = new System.Drawing.Size(203, 21);
            this.Modification_DataItem_Editor.TabIndex = 16;
            // 
            // ModificationType_Editor
            // 
            this.ModificationType_Editor.DropDownHeight = 160;
            this.ModificationType_Editor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ModificationType_Editor.FormattingEnabled = true;
            this.ModificationType_Editor.IntegralHeight = false;
            this.ModificationType_Editor.Location = new System.Drawing.Point(191, 207);
            this.ModificationType_Editor.Name = "ModificationType_Editor";
            this.ModificationType_Editor.Size = new System.Drawing.Size(101, 21);
            this.ModificationType_Editor.TabIndex = 17;
            // 
            // ModificationValue_Editor
            // 
            this.ModificationValue_Editor.Location = new System.Drawing.Point(300, 280);
            this.ModificationValue_Editor.Name = "ModificationValue_Editor";
            this.ModificationValue_Editor.Size = new System.Drawing.Size(122, 20);
            this.ModificationValue_Editor.TabIndex = 18;
            this.ModificationValue_Editor.Value = null;
            // 
            // wizardPage5
            // 
            this.wizardPage5.Controls.Add(this.panel1);
            this.wizardPage5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPage5.IsFinishPage = false;
            this.wizardPage5.Location = new System.Drawing.Point(0, 0);
            this.wizardPage5.Name = "wizardPage5";
            this.wizardPage5.Size = new System.Drawing.Size(569, 415);
            this.wizardPage5.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.ReportInfomation);
            this.panel1.Location = new System.Drawing.Point(8, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(552, 394);
            this.panel1.TabIndex = 2;
            // 
            // ReportInfomation
            // 
            this.ReportInfomation.AutoSize = true;
            this.ReportInfomation.Location = new System.Drawing.Point(0, 0);
            this.ReportInfomation.Name = "ReportInfomation";
            this.ReportInfomation.Size = new System.Drawing.Size(187, 13);
            this.ReportInfomation.TabIndex = 0;
            this.ReportInfomation.Text = "这里显示读数函数的报告信息！！";
            // 
            // wizardPage2
            // 
            this.wizardPage2.Controls.Add(this.EventListView);
            this.wizardPage2.Controls.Add(this.label6);
            this.wizardPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPage2.IsFinishPage = false;
            this.wizardPage2.Location = new System.Drawing.Point(0, 0);
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.Size = new System.Drawing.Size(569, 415);
            this.wizardPage2.TabIndex = 5;
            // 
            // EventListView
            // 
            this.EventListView.FullRowSelect = true;
            this.EventListView.HideSelection = false;
            this.EventListView.Location = new System.Drawing.Point(9, 29);
            this.EventListView.Name = "EventListView";
            this.EventListView.Size = new System.Drawing.Size(551, 375);
            this.EventListView.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "触发事件列表：";
            // 
            // ReadDataFunctionWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 463);
            this.Name = "ReadDataFunctionWizard";
            this.Text = "读数函数";
            this.MyWizard.ResumeLayout(false);
            this.wizardPage1.ResumeLayout(false);
            this.wizardPage1.PerformLayout();
            this.wizardPage3.ResumeLayout(false);
            this.wizardPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConditionList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConditionList_Sheet1)).EndInit();
            this.wizardPage4.ResumeLayout(false);
            this.wizardPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ModificationList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ModificationList_Sheet1)).EndInit();
            this.wizardPage5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.wizardPage2.ResumeLayout(false);
            this.wizardPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private IS.DNS.WinUI.Wizard.WizardPage wizardPage1;
        private System.Windows.Forms.TreeView Biz_DataTableList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TextBox_FunctionName;
        private System.Windows.Forms.Label label1;
        private IS.DNS.WinUI.Wizard.WizardPage wizardPage3;
        internal FarPoint.Win.Spread.FpSpread ConditionList;
        private FarPoint.Win.Spread.SheetView ConditionList_Sheet1;
        private System.Windows.Forms.Button Button_RemoveRow;
        private System.Windows.Forms.Button Button_AddRow;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox WriteToTable_DataItem_Editor;
        private System.Windows.Forms.ComboBox WriteToTable_Condition_Editor;
        private Yqun.Controls.ComboTree WriteToTable_Value_Editor;
        private IS.DNS.WinUI.Wizard.WizardPage wizardPage4;
        internal FarPoint.Win.Spread.FpSpread ModificationList;
        private FarPoint.Win.Spread.SheetView ModificationList_Sheet1;
        private System.Windows.Forms.Button Button_DeleteValue;
        private System.Windows.Forms.Button Button_AddValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox Modification_DataItem_Editor;
        private System.Windows.Forms.ComboBox ModificationType_Editor;
        private Yqun.Controls.ComboTree ModificationValue_Editor;
        private IS.DNS.WinUI.Wizard.WizardPage wizardPage5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label ReportInfomation;
        private IS.DNS.WinUI.Wizard.WizardPage wizardPage2;
        private System.Windows.Forms.TreeView EventListView;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}