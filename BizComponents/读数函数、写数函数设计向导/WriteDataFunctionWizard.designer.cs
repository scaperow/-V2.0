namespace BizComponents
{
    partial class WriteDataFunctionWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WriteDataFunctionWizard));
            this.wizardPage1 = new IS.DNS.WinUI.Wizard.WizardPage();
            this.SheetList = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.TextBox_FunctionName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.wizardPage2 = new IS.DNS.WinUI.Wizard.WizardPage();
            this.ConditionList = new FarPoint.Win.Spread.FpSpread();
            this.ConditionList_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.Button_RemoveRow = new System.Windows.Forms.Button();
            this.Button_AddRow = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.WriteToTable_DataItem_Editor = new System.Windows.Forms.ComboBox();
            this.WriteToTable_Condition_Editor = new System.Windows.Forms.ComboBox();
            this.WriteToTable_Value_Editor = new Yqun.Controls.ComboTree();
            this.wizardPage3 = new IS.DNS.WinUI.Wizard.WizardPage();
            this.ModificationList = new FarPoint.Win.Spread.FpSpread();
            this.ModificationList_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.Button_DeleteValue = new System.Windows.Forms.Button();
            this.Button_AddValue = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.Modification_DataItem_Editor = new System.Windows.Forms.ComboBox();
            this.ModificationType_Editor = new System.Windows.Forms.ComboBox();
            this.ModificationValue_Editor = new Yqun.Controls.ComboTree();
            this.wizardPage4 = new IS.DNS.WinUI.Wizard.WizardPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ReportInfomation = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.MyWizard.SuspendLayout();
            this.wizardPage1.SuspendLayout();
            this.wizardPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConditionList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConditionList_Sheet1)).BeginInit();
            this.wizardPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ModificationList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ModificationList_Sheet1)).BeginInit();
            this.wizardPage4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MyWizard
            // 
            this.MyWizard.Controls.Add(this.wizardPage1);
            this.MyWizard.Controls.Add(this.wizardPage2);
            this.MyWizard.Controls.Add(this.wizardPage3);
            this.MyWizard.Controls.Add(this.wizardPage4);
            this.MyWizard.Pages.AddRange(new IS.DNS.WinUI.Wizard.WizardPage[] {
            this.wizardPage1,
            this.wizardPage2,
            this.wizardPage3,
            this.wizardPage4});
            this.MyWizard.Size = new System.Drawing.Size(569, 463);
            this.MyWizard.FinishButtonClick += new IS.DNS.WinUI.Wizard.FinishClickHandle(this.MyWizard_FinishButtonClick);
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.SheetList);
            this.wizardPage1.Controls.Add(this.label2);
            this.wizardPage1.Controls.Add(this.TextBox_FunctionName);
            this.wizardPage1.Controls.Add(this.label1);
            this.wizardPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPage1.IsFinishPage = false;
            this.wizardPage1.Location = new System.Drawing.Point(0, 0);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Size = new System.Drawing.Size(569, 415);
            this.wizardPage1.TabIndex = 1;
            this.wizardPage1.CloseFromNext += new IS.DNS.WinUI.Wizard.PageEventHandler(this.wizardPage1_CloseFromNext);
            // 
            // SheetList
            // 
            this.SheetList.FullRowSelect = true;
            this.SheetList.HideSelection = false;
            this.SheetList.ImageIndex = 0;
            this.SheetList.ImageList = this.imageList1;
            this.SheetList.Location = new System.Drawing.Point(9, 79);
            this.SheetList.Name = "SheetList";
            this.SheetList.SelectedImageIndex = 0;
            this.SheetList.Size = new System.Drawing.Size(551, 325);
            this.SheetList.TabIndex = 7;
            this.SheetList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.SheetList_NodeMouseClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "选择一个待写入的目标数据表：";
            // 
            // TextBox_FunctionName
            // 
            this.TextBox_FunctionName.Location = new System.Drawing.Point(9, 29);
            this.TextBox_FunctionName.Name = "TextBox_FunctionName";
            this.TextBox_FunctionName.Size = new System.Drawing.Size(551, 21);
            this.TextBox_FunctionName.TabIndex = 5;
            this.TextBox_FunctionName.Text = "请输入函数名";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "函数名称：";
            // 
            // wizardPage2
            // 
            this.wizardPage2.Controls.Add(this.ConditionList);
            this.wizardPage2.Controls.Add(this.Button_RemoveRow);
            this.wizardPage2.Controls.Add(this.Button_AddRow);
            this.wizardPage2.Controls.Add(this.label3);
            this.wizardPage2.Controls.Add(this.WriteToTable_DataItem_Editor);
            this.wizardPage2.Controls.Add(this.WriteToTable_Condition_Editor);
            this.wizardPage2.Controls.Add(this.WriteToTable_Value_Editor);
            this.wizardPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPage2.IsFinishPage = false;
            this.wizardPage2.Location = new System.Drawing.Point(0, 0);
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.Size = new System.Drawing.Size(569, 415);
            this.wizardPage2.TabIndex = 2;
            this.wizardPage2.CloseFromNext += new IS.DNS.WinUI.Wizard.PageEventHandler(this.wizardPage2_CloseFromNext);
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
            this.ConditionList.TabIndex = 4;
            this.ConditionList.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.ConditionList.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.ConditionList_CellClick);
            // 
            // ConditionList_Sheet1
            // 
            this.ConditionList_Sheet1.Reset();
            this.ConditionList_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.ConditionList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.ConditionList_Sheet1.ColumnCount = 3;
            this.ConditionList_Sheet1.RowCount = 100;
            this.ConditionList_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "写入数据表-数据项";
            this.ConditionList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "条件";
            this.ConditionList_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "条件值";
            this.ConditionList_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.ConditionList_Sheet1.Columns.Get(0).Label = "写入数据表-数据项";
            this.ConditionList_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.ConditionList_Sheet1.Columns.Get(0).Width = 185F;
            this.ConditionList_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.ConditionList_Sheet1.Columns.Get(1).Label = "条件";
            this.ConditionList_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.ConditionList_Sheet1.Columns.Get(1).Width = 100F;
            this.ConditionList_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.ConditionList_Sheet1.Columns.Get(2).Label = "条件值";
            this.ConditionList_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.ConditionList_Sheet1.Columns.Get(2).Width = 170F;
            this.ConditionList_Sheet1.RowHeader.AutoText = FarPoint.Win.Spread.HeaderAutoText.Blank;
            this.ConditionList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // Button_RemoveRow
            // 
            this.Button_RemoveRow.Location = new System.Drawing.Point(502, 59);
            this.Button_RemoveRow.Name = "Button_RemoveRow";
            this.Button_RemoveRow.Size = new System.Drawing.Size(59, 23);
            this.Button_RemoveRow.TabIndex = 3;
            this.Button_RemoveRow.Text = "删除行";
            this.Button_RemoveRow.UseVisualStyleBackColor = true;
            this.Button_RemoveRow.Click += new System.EventHandler(this.Button_RemoveRow_Click);
            // 
            // Button_AddRow
            // 
            this.Button_AddRow.Location = new System.Drawing.Point(502, 31);
            this.Button_AddRow.Name = "Button_AddRow";
            this.Button_AddRow.Size = new System.Drawing.Size(59, 23);
            this.Button_AddRow.TabIndex = 2;
            this.Button_AddRow.Text = "添加行";
            this.Button_AddRow.UseVisualStyleBackColor = true;
            this.Button_AddRow.Click += new System.EventHandler(this.Button_AddRow_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "设置过滤条件说明";
            // 
            // WriteToTable_DataItem_Editor
            // 
            this.WriteToTable_DataItem_Editor.DropDownHeight = 160;
            this.WriteToTable_DataItem_Editor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WriteToTable_DataItem_Editor.FormattingEnabled = true;
            this.WriteToTable_DataItem_Editor.IntegralHeight = false;
            this.WriteToTable_DataItem_Editor.Location = new System.Drawing.Point(48, 223);
            this.WriteToTable_DataItem_Editor.Name = "WriteToTable_DataItem_Editor";
            this.WriteToTable_DataItem_Editor.Size = new System.Drawing.Size(203, 21);
            this.WriteToTable_DataItem_Editor.TabIndex = 9;
            this.WriteToTable_DataItem_Editor.DropDownClosed += new System.EventHandler(this.WriteToTable_DataItem_Editor_DropDownClosed);
            // 
            // WriteToTable_Condition_Editor
            // 
            this.WriteToTable_Condition_Editor.DropDownHeight = 160;
            this.WriteToTable_Condition_Editor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WriteToTable_Condition_Editor.DropDownWidth = 120;
            this.WriteToTable_Condition_Editor.FormattingEnabled = true;
            this.WriteToTable_Condition_Editor.IntegralHeight = false;
            this.WriteToTable_Condition_Editor.Location = new System.Drawing.Point(196, 155);
            this.WriteToTable_Condition_Editor.Name = "WriteToTable_Condition_Editor";
            this.WriteToTable_Condition_Editor.Size = new System.Drawing.Size(101, 21);
            this.WriteToTable_Condition_Editor.TabIndex = 6;
            this.WriteToTable_Condition_Editor.DropDownClosed += new System.EventHandler(this.WriteToTable_Condition_Editor_DropDownClosed);
            // 
            // WriteToTable_Value_Editor
            // 
            this.WriteToTable_Value_Editor.Location = new System.Drawing.Point(324, 198);
            this.WriteToTable_Value_Editor.Name = "WriteToTable_Value_Editor";
            this.WriteToTable_Value_Editor.Size = new System.Drawing.Size(122, 20);
            this.WriteToTable_Value_Editor.TabIndex = 8;
            this.WriteToTable_Value_Editor.Value = null;
            this.WriteToTable_Value_Editor.FinishEditing += new AT.STO.UI.Win.DropDownValueChangedEventHandler(this.WriteToTable_Value_Editor_FinishEditing);
            this.WriteToTable_Value_Editor.DropDownClosed += new System.EventHandler(this.WriteToTable_Value_Editor_DropDownClosed);
            // 
            // wizardPage3
            // 
            this.wizardPage3.Controls.Add(this.ModificationList);
            this.wizardPage3.Controls.Add(this.Button_DeleteValue);
            this.wizardPage3.Controls.Add(this.Button_AddValue);
            this.wizardPage3.Controls.Add(this.label4);
            this.wizardPage3.Controls.Add(this.Modification_DataItem_Editor);
            this.wizardPage3.Controls.Add(this.ModificationType_Editor);
            this.wizardPage3.Controls.Add(this.ModificationValue_Editor);
            this.wizardPage3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPage3.IsFinishPage = false;
            this.wizardPage3.Location = new System.Drawing.Point(0, 0);
            this.wizardPage3.Name = "wizardPage3";
            this.wizardPage3.Size = new System.Drawing.Size(569, 415);
            this.wizardPage3.TabIndex = 3;
            this.wizardPage3.CloseFromNext += new IS.DNS.WinUI.Wizard.PageEventHandler(this.wizardPage3_CloseFromNext);
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
            this.ModificationList.TabIndex = 8;
            this.ModificationList.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.ModificationList.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.ModificationList_CellClick);
            // 
            // ModificationList_Sheet1
            // 
            this.ModificationList_Sheet1.Reset();
            this.ModificationList_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.ModificationList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.ModificationList_Sheet1.ColumnCount = 3;
            this.ModificationList_Sheet1.RowCount = 100;
            this.ModificationList_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "数据项";
            this.ModificationList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "修改类型";
            this.ModificationList_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "本表单";
            this.ModificationList_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.ModificationList_Sheet1.Columns.Get(0).Label = "数据项";
            this.ModificationList_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.ModificationList_Sheet1.Columns.Get(0).Width = 185F;
            this.ModificationList_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.ModificationList_Sheet1.Columns.Get(1).Label = "修改类型";
            this.ModificationList_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.ModificationList_Sheet1.Columns.Get(1).Width = 100F;
            this.ModificationList_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.ModificationList_Sheet1.Columns.Get(2).Label = "本表单";
            this.ModificationList_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.ModificationList_Sheet1.Columns.Get(2).Width = 170F;
            this.ModificationList_Sheet1.RowHeader.AutoText = FarPoint.Win.Spread.HeaderAutoText.Blank;
            this.ModificationList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // Button_DeleteValue
            // 
            this.Button_DeleteValue.Location = new System.Drawing.Point(502, 59);
            this.Button_DeleteValue.Name = "Button_DeleteValue";
            this.Button_DeleteValue.Size = new System.Drawing.Size(59, 23);
            this.Button_DeleteValue.TabIndex = 7;
            this.Button_DeleteValue.Text = "删除行";
            this.Button_DeleteValue.UseVisualStyleBackColor = true;
            this.Button_DeleteValue.Click += new System.EventHandler(this.Button_DeleteValue_Click);
            // 
            // Button_AddValue
            // 
            this.Button_AddValue.Location = new System.Drawing.Point(502, 31);
            this.Button_AddValue.Name = "Button_AddValue";
            this.Button_AddValue.Size = new System.Drawing.Size(59, 23);
            this.Button_AddValue.TabIndex = 6;
            this.Button_AddValue.Text = "添加行";
            this.Button_AddValue.UseVisualStyleBackColor = true;
            this.Button_AddValue.Click += new System.EventHandler(this.Button_AddValue_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "修改对应关系说明";
            // 
            // Modification_DataItem_Editor
            // 
            this.Modification_DataItem_Editor.DropDownHeight = 160;
            this.Modification_DataItem_Editor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Modification_DataItem_Editor.FormattingEnabled = true;
            this.Modification_DataItem_Editor.IntegralHeight = false;
            this.Modification_DataItem_Editor.Location = new System.Drawing.Point(27, 121);
            this.Modification_DataItem_Editor.Name = "Modification_DataItem_Editor";
            this.Modification_DataItem_Editor.Size = new System.Drawing.Size(203, 21);
            this.Modification_DataItem_Editor.TabIndex = 9;
            this.Modification_DataItem_Editor.DropDownClosed += new System.EventHandler(this.Modification_DataItem_Editor_DropDownClosed);
            // 
            // ModificationType_Editor
            // 
            this.ModificationType_Editor.DropDownHeight = 160;
            this.ModificationType_Editor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ModificationType_Editor.FormattingEnabled = true;
            this.ModificationType_Editor.IntegralHeight = false;
            this.ModificationType_Editor.Location = new System.Drawing.Point(192, 207);
            this.ModificationType_Editor.Name = "ModificationType_Editor";
            this.ModificationType_Editor.Size = new System.Drawing.Size(101, 21);
            this.ModificationType_Editor.TabIndex = 10;
            this.ModificationType_Editor.DropDownClosed += new System.EventHandler(this.ModificationType_Editor_DropDownClosed);
            // 
            // ModificationValue_Editor
            // 
            this.ModificationValue_Editor.Location = new System.Drawing.Point(301, 258);
            this.ModificationValue_Editor.Name = "ModificationValue_Editor";
            this.ModificationValue_Editor.Size = new System.Drawing.Size(122, 20);
            this.ModificationValue_Editor.TabIndex = 11;
            this.ModificationValue_Editor.Value = null;
            this.ModificationValue_Editor.FinishEditing += new AT.STO.UI.Win.DropDownValueChangedEventHandler(this.ModificationValue_Editor_FinishEditing);
            this.ModificationValue_Editor.DropDownClosed += new System.EventHandler(this.ModificationValue_Editor_DropDownClosed);
            // 
            // wizardPage4
            // 
            this.wizardPage4.Controls.Add(this.panel1);
            this.wizardPage4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPage4.IsFinishPage = false;
            this.wizardPage4.Location = new System.Drawing.Point(0, 0);
            this.wizardPage4.Name = "wizardPage4";
            this.wizardPage4.Size = new System.Drawing.Size(569, 415);
            this.wizardPage4.TabIndex = 4;
            this.wizardPage4.ShowFromNext += new System.EventHandler(this.wizardPage4_ShowFromNext);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.ReportInfomation);
            this.panel1.Location = new System.Drawing.Point(8, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(552, 394);
            this.panel1.TabIndex = 1;
            // 
            // ReportInfomation
            // 
            this.ReportInfomation.AutoSize = true;
            this.ReportInfomation.Location = new System.Drawing.Point(0, 0);
            this.ReportInfomation.Name = "ReportInfomation";
            this.ReportInfomation.Size = new System.Drawing.Size(187, 13);
            this.ReportInfomation.TabIndex = 0;
            this.ReportInfomation.Text = "这里显示写数函数的报告信息！！";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            this.imageList1.Images.SetKeyName(0, "表单.png");
            // 
            // WriteDataFunctionWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 463);
            this.HelpButton = true;
            this.Name = "WriteDataFunctionWizard";
            this.Text = "写数函数设置向导";
            this.MyWizard.ResumeLayout(false);
            this.wizardPage1.ResumeLayout(false);
            this.wizardPage1.PerformLayout();
            this.wizardPage2.ResumeLayout(false);
            this.wizardPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConditionList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConditionList_Sheet1)).EndInit();
            this.wizardPage3.ResumeLayout(false);
            this.wizardPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ModificationList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ModificationList_Sheet1)).EndInit();
            this.wizardPage4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private IS.DNS.WinUI.Wizard.WizardPage wizardPage3;
        private IS.DNS.WinUI.Wizard.WizardPage wizardPage2;
        private IS.DNS.WinUI.Wizard.WizardPage wizardPage1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Button_RemoveRow;
        private System.Windows.Forms.Button Button_AddRow;
        private System.Windows.Forms.Button Button_DeleteValue;
        private System.Windows.Forms.Button Button_AddValue;
        private System.Windows.Forms.Label label4;
        private IS.DNS.WinUI.Wizard.WizardPage wizardPage4;
        private System.Windows.Forms.TreeView SheetList;
        private FarPoint.Win.Spread.SheetView ConditionList_Sheet1;
        private FarPoint.Win.Spread.SheetView ModificationList_Sheet1;
        private System.Windows.Forms.Label ReportInfomation;
        private System.Windows.Forms.ComboBox WriteToTable_Condition_Editor;
        private Yqun.Controls.ComboTree WriteToTable_Value_Editor;
        private Yqun.Controls.ComboTree ModificationValue_Editor;
        private System.Windows.Forms.ComboBox ModificationType_Editor;
        private System.Windows.Forms.ComboBox Modification_DataItem_Editor;
        internal FarPoint.Win.Spread.FpSpread ConditionList;
        internal FarPoint.Win.Spread.FpSpread ModificationList;
        private System.Windows.Forms.TextBox TextBox_FunctionName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox WriteToTable_DataItem_Editor;
        private System.Windows.Forms.ImageList imageList1;
    }
}