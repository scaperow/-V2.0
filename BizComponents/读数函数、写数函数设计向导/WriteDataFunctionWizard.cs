using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Yqun.Bases;
using Yqun.Client;
using Yqun.Common.ContextCache;
using Yqun.Common.Encoder;
using FarPoint.Win.Spread;
using Yqun.Services;
using Yqun.Controls;
using BizCommon;

namespace BizComponents
{
    public partial class WriteDataFunctionWizard : WizardDialog
    {
        private ModuleConfiguration Module;
        private TreeView FunctionList;
        private WriteDataFunctionInfo FunctionInfo;
        private String ReadFromTableName;

        public WriteDataFunctionWizard(ModuleConfiguration Module, TreeView FunctionList)
        {
            this.Module = Module;
            this.FunctionList = FunctionList;

            InitializeComponent();

            InitComponent();
            InitEventList();
        }

        private void InitEventList()
        {
            SheetList.NodeMouseClick += new TreeNodeMouseClickEventHandler(SheetList_NodeMouseClick);
        }

        private void InitComponent()
        {
            InitSheetList();
            InitControlList();
            InitModificationList();
        }

        /// <summary>
        ///加载模板中所有表单的数据项值
        /// </summary>
        /// <param name="Schemas"></param>
        public void InitDataItems()
        {
            foreach (SheetConfiguration Sheet in Module.Sheets)
            {
                DataTableSchema DataTable = Sheet.DataTableSchema;

                TreeView treeView = WriteToTable_Value_Editor.DropDownControl;
                treeView.Nodes.Clear();
                if (DataTable.Schema != null)
                {
                    TableDefineInfo TableInfo = DataTable.Schema;
                    DropDownNode Node = new DropDownNode(TableInfo.GetHashCode(), TableInfo.Description);
                    Node.Name = TableInfo.Name;
                    treeView.Nodes.Add(Node);

                    #region 时间戳字段

                    DropDownNode SubNode = new DropDownNode((int)DateTime.Now.Ticks, "时间戳");
                    SubNode.Name = "scts";

                    FieldInfo fieldInfo = new FieldInfo();
                    fieldInfo.Name = "scts";
                    fieldInfo.Text = "时间戳";
                    SubNode.Tag = fieldInfo;

                    Node.Nodes.Add(SubNode);

                    #endregion 时间戳字段

                    foreach (FieldDefineInfo FieldInfo in TableInfo.FieldInfos)
                    {
                        if (!ColumnRegular.JudgeColumnName(FieldInfo.Description))
                        {
                            SubNode = new DropDownNode(FieldInfo.GetHashCode(), FieldInfo.Description);
                            SubNode.Name = FieldInfo.FieldName;

                            fieldInfo = new FieldInfo();
                            fieldInfo.Name = FieldInfo.FieldName;
                            fieldInfo.Text = FieldInfo.Description;
                            SubNode.Tag = fieldInfo;

                            Node.Nodes.Add(SubNode);
                        }
                    }
                }

                treeView.ExpandAll();

                treeView = ModificationValue_Editor.DropDownControl;
                treeView.Nodes.Clear();
                if (DataTable.Schema != null)
                {
                    TableDefineInfo TableInfo = DataTable.Schema;
                    DropDownNode Node = new DropDownNode(TableInfo.GetHashCode(), TableInfo.Description);
                    Node.Name = TableInfo.Name;
                    treeView.Nodes.Add(Node);

                    #region 时间戳字段

                    DropDownNode SubNode = new DropDownNode((int)DateTime.Now.Ticks, "时间戳");
                    SubNode.Name = "scts";

                    FieldInfo fieldInfo = new FieldInfo();
                    fieldInfo.Name = "scts";
                    fieldInfo.Text = "时间戳";
                    SubNode.Tag = fieldInfo;

                    Node.Nodes.Add(SubNode);

                    #endregion 时间戳字段

                    foreach (FieldDefineInfo FieldInfo in TableInfo.FieldInfos)
                    {
                        if (!ColumnRegular.JudgeColumnName(FieldInfo.Description))
                        {
                            SubNode = new DropDownNode(FieldInfo.GetHashCode(), FieldInfo.Description);
                            SubNode.Name = FieldInfo.FieldName;

                            fieldInfo = new FieldInfo();
                            fieldInfo.Name = FieldInfo.FieldName;
                            fieldInfo.Text = FieldInfo.Description;
                            SubNode.Tag = fieldInfo;

                            Node.Nodes.Add(SubNode);
                        }
                    }
                }

                treeView.ExpandAll();
            }
        }

        private void InitSheetList()
        {
            SheetList.Nodes.Clear();
            TreeNode TopNode = new TreeNode();
            TopNode.Text = "表单列表";
            Selection Selection = new Selection();
            Selection.TypeFlag = "@TopNode";
            TopNode.Tag = Selection;
            SheetList.Nodes.Add(TopNode);

            //列出所有需要写入的数据表
            StringBuilder Sql_Select = new StringBuilder();
            Sql_Select.Append("select Description,DataTable from sys_biz_Sheet");
            DataTable Data = Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { Sql_Select.ToString() }) as DataTable;
            foreach (DataRow Row in Data.Rows)
            {
                String tableName = Row["DataTable"].ToString();
                String description = Row["Description"].ToString();

                if (!string.IsNullOrEmpty(tableName))
                {
                    TreeNode TableNode = new TreeNode();
                    TableNode.Name = tableName;
                    TableNode.Text = description;
                    Selection = new Selection();
                    Selection.TypeFlag = "@TableNode";
                    TableNode.Tag = Selection;
                    TopNode.Nodes.Add(TableNode);
                }
            }

            TopNode.Expand();
        }

        private void InitControlList()
        {
            //加载过滤条件
            WriteToTable_Condition_Editor.Items.Clear();
            WriteToTable_Condition_Editor.Items.Add("等于");
            WriteToTable_Condition_Editor.Items.Add("大于");
            WriteToTable_Condition_Editor.Items.Add("大于或等于");
            WriteToTable_Condition_Editor.Items.Add("小于");
            WriteToTable_Condition_Editor.Items.Add("小于或等于");
            WriteToTable_Condition_Editor.Items.Add("取最大值");
            WriteToTable_Condition_Editor.Items.Add("取最小值");
        }

        private void InitModificationList()
        {
            //加载修改类型
            ModificationType_Editor.Items.Clear();
            ModificationType_Editor.Items.Add("累计加");
            ModificationType_Editor.Items.Add("累计减");
            ModificationType_Editor.Items.Add("赋值");
        }

        private void InitFields(TreeNode Node)
        {
            Selection Selection = Node.Tag as Selection;
            if (Selection.TypeFlag.ToLower() == "@tablenode")
            {
                String TableName = Node.Name;

                WriteToTable_DataItem_Editor.Items.Clear();
                Modification_DataItem_Editor.Items.Clear();
                DataTable Data = Agent.CallService("Yqun.BO.LoginBO.dll", "GetTableStruct", new object[] { TableName }) as DataTable;
                foreach (DataColumn Column in Data.Columns)
                {
                    if (Column.ColumnName.ToLower() == "id" ||
                        Column.ColumnName.ToLower() == "scpt")
                        continue;

                    FieldInfo FieldInfo = new FieldInfo();
                    if (Column.ColumnName.ToLower() == "scts")
                    {
                        FieldInfo.Text = "时间戳";
                    }
                    else
                    {
                        FieldInfo.Text = Column.ColumnName;
                    }

                    FieldInfo.Name = Column.ColumnName;

                    WriteToTable_DataItem_Editor.Items.Add(FieldInfo);
                    Modification_DataItem_Editor.Items.Add(FieldInfo);
                }
            }
        }

        void SheetList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            InitFields(e.Node);

            FunctionInfo.WriteInTableText = e.Node.Name;
            FunctionInfo.WriteInTableIndex = e.Node.Name;
        }

        /// <summary>
        /// 初始化写数函数
        /// </summary>
        /// <param name="FunctionInfo"></param>
        public void InitFunctionInfo(WriteDataFunctionInfo FunctionInfo)
        {
            this.FunctionInfo = FunctionInfo;
            //显示写数函数的名称
            TextBox_FunctionName.Text = FunctionInfo.Name;
            ReadFromTableName = FunctionInfo.ReadOutTableIndex;

            //显示写数函数的写入数据的数据表
            if (!string.IsNullOrEmpty(FunctionInfo.WriteInTableIndex))
            {
                TreeNode[] Nodes = SheetList.Nodes.Find(FunctionInfo.WriteInTableIndex,true);
                if (Nodes.Length > 0)
                {
                    SheetList.SelectedNode = Nodes[0];
                    InitFields(Nodes[0]);
                }
            }

            //显示写数函数的过滤条件

            //绑定数据时保证显示第一行，以免报错“System.ArgumentOutOfRangeException: Invalid low bound argument”
            if (ConditionList.ActiveSheet.Rows.Count > 0)
            {
                ConditionList.ShowRow(ConditionList.GetActiveRowViewportIndex(), 0, VerticalPosition.Top);
            }

            ConditionList.ActiveSheet.Rows.Count = 0;
            ConditionList.ActiveSheet.Rows.Count = 600;
            foreach(ExpressionInfo Info in FunctionInfo.Conditions)
            {
                ConditionList.ActiveSheet.Rows.Add(0,1);
                ConditionList.ActiveSheet.Rows[0].Tag = Info;

                foreach(FieldInfo fieldInfo in WriteToTable_DataItem_Editor.Items)
                {
                    if (fieldInfo.Name == Info.DataItem.Name)
                    {
                        ConditionList.ActiveSheet.Cells[0, 0].Value = fieldInfo;
                        break;
                    }
                }

                TreeNode[] dropDownNodes = WriteToTable_Value_Editor.DropDownControl.Nodes.Find(Info.DataValue.Name,true);
                if (dropDownNodes.Length > 0)
                {
                    WriteToTable_Value_Editor.DropDownControl.SelectedNode = dropDownNodes[0];
                    ConditionList.ActiveSheet.Cells[0, 2].Value = dropDownNodes[0].Tag as FieldInfo;
                }

                ConditionList.ActiveSheet.Cells[0, 1].Value = Info.Operation;
            }

            //显示写数函数的修改数据项

            //绑定数据时保证显示第一行，以免报错“System.ArgumentOutOfRangeException: Invalid low bound argument”
            if (ModificationList.ActiveSheet.Rows.Count > 0)
            {
                ModificationList.ShowRow(ModificationList.GetActiveRowViewportIndex(), 0, VerticalPosition.Top);
            }

            ModificationList.ActiveSheet.Rows.Count = 0;
            ModificationList.ActiveSheet.Rows.Count = 600;
            foreach (ExpressionInfo Info in FunctionInfo.Modifications)
            {
                ModificationList.ActiveSheet.Rows.Add(0, 1);
                ModificationList.ActiveSheet.Rows[0].Tag = Info;

                foreach (FieldInfo fieldInfo in Modification_DataItem_Editor.Items)
                {
                    if (fieldInfo.Name == Info.DataItem.Name)
                    {
                        ModificationList.ActiveSheet.Cells[0, 0].Value = fieldInfo;
                        break;
                    }
                }

                TreeNode[] dropDownNodes = ModificationValue_Editor.DropDownControl.Nodes.Find(Info.DataValue.Name, true);
                if (dropDownNodes.Length > 0)
                {
                    ModificationValue_Editor.DropDownControl.SelectedNode = dropDownNodes[0];
                    ModificationList.ActiveSheet.Cells[0, 2].Value = dropDownNodes[0].Tag as FieldInfo;
                }

                ModificationList.ActiveSheet.Cells[0, 1].Value = Info.Operation;
            }
        }

        #region 向导页跳转校验

        private void wizardPage1_CloseFromNext(object sender, IS.DNS.WinUI.Wizard.PageEventArgs e)
        {
            if (TextBox_FunctionName.Text == "请输入函数名称")
            {
                MessageBox.Show("请输入函数名称。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TextBox_FunctionName.Focus();
                TextBox_FunctionName.SelectAll();
                e.Page = wizardPage1;
            }
            else
            {
                TreeNode Node = SheetList.SelectedNode as TreeNode;
                if (Node == null ||
                   (Node != null && (Node.Tag as Selection).TypeFlag.ToLower() != "@tablenode"))
                {
                    MessageBox.Show("请选择一个数据表。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Page = wizardPage1;
                }
            }
        }

        private void wizardPage2_CloseFromNext(object sender, IS.DNS.WinUI.Wizard.PageEventArgs e)
        {
            bool HaveInvalidData = false;
            foreach (Row row in ConditionList.ActiveSheet.Rows)
            {
                if (ConditionList.ActiveSheet.Cells[row.Index, 0].Value != null &&
                    ConditionList.ActiveSheet.Cells[row.Index, 1].Value != null &&
                    ConditionList.ActiveSheet.Cells[row.Index, 2].Value != null)
                    continue;

                if (ConditionList.ActiveSheet.Cells[row.Index, 0].Value == null &&
                    ConditionList.ActiveSheet.Cells[row.Index, 1].Value == null &&
                    ConditionList.ActiveSheet.Cells[row.Index, 2].Value == null)
                    continue;

                HaveInvalidData = HaveInvalidData || ConditionList.ActiveSheet.Cells[row.Index,0].Value == null;
                HaveInvalidData = HaveInvalidData || ConditionList.ActiveSheet.Cells[row.Index,1].Value == null;
                HaveInvalidData = HaveInvalidData || ConditionList.ActiveSheet.Cells[row.Index,2].Value == null;
            }

            if (HaveInvalidData)
            {
                MessageBox.Show("包含没有配置的数据项。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Page = wizardPage2;
            }

            //这里需要验证操作符两端的字段类型是否一致，只有类型一致时才可以进入下一步
        }

        private void wizardPage3_CloseFromNext(object sender, IS.DNS.WinUI.Wizard.PageEventArgs e)
        {
            bool HaveInvalidData = false;
            foreach (Row row in ModificationList.ActiveSheet.Rows)
            {
                if (ModificationList.ActiveSheet.Cells[row.Index, 0].Value != null &&
                    ModificationList.ActiveSheet.Cells[row.Index, 1].Value != null &&
                    ModificationList.ActiveSheet.Cells[row.Index, 2].Value != null)
                    continue;

                if (ModificationList.ActiveSheet.Cells[row.Index, 0].Value == null &&
                    ModificationList.ActiveSheet.Cells[row.Index, 1].Value == null &&
                    ModificationList.ActiveSheet.Cells[row.Index, 2].Value == null)
                    continue;

                HaveInvalidData = HaveInvalidData || ModificationList.ActiveSheet.Cells[row.Index, 0].Value == null;
                HaveInvalidData = HaveInvalidData || ModificationList.ActiveSheet.Cells[row.Index, 1].Value == null;
                HaveInvalidData = HaveInvalidData || ModificationList.ActiveSheet.Cells[row.Index, 2].Value == null;
            }

            if (HaveInvalidData)
            {
                MessageBox.Show("包含没有配置的修改项。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Page = wizardPage3;
            }

            //这里需要验证操作符两端的字段类型是否一致，只有类型一致时才可以进入下一步
        }

        private void wizardPage4_ShowFromNext(object sender, EventArgs e)
        {
            FunctionInfo.Name = TextBox_FunctionName.Text;
            FunctionInfo.WriteInTableIndex = SheetList.SelectedNode.Name;
            FunctionInfo.WriteInTableText = SheetList.SelectedNode.Name;

            FunctionInfo.ReadOutTableIndex = ReadFromTableName;

            FunctionInfo.Conditions.Clear();
            foreach (Row row in ConditionList.ActiveSheet.Rows)
            {
                if (row.Tag != null && row.Tag is ExpressionInfo)
                {
                    FunctionInfo.Conditions.Add(row.Tag as ExpressionInfo);
                }
            }

            FunctionInfo.Modifications.Clear();
            foreach (Row row in ModificationList.ActiveSheet.Rows)
            {
                if (row.Tag != null && row.Tag is ExpressionInfo)
                {
                    FunctionInfo.Modifications.Add(row.Tag as ExpressionInfo);
                }
            }

            this.ReportInfomation.Text = FunctionInfo.ReportInfo();
        }

        private void MyWizard_FinishButtonClick(object sender, EventArgs e)
        {
            if (DepositoryWriteFunction.Save(FunctionInfo))
            {
                TreeNode[] Infos = FunctionList.Nodes.Find(FunctionInfo.Index,true);
                if (Infos.Length == 0)
                {
                    TreeNode ParentNode = new TreeNode();
                    if (FunctionList.SelectedNode.Tag is SheetConfiguration)
                    {
                        ParentNode = FunctionList.SelectedNode;
                    }
                    else if (FunctionList.SelectedNode.Tag is WriteDataFunctionInfo)
                    {
                        ParentNode = FunctionList.SelectedNode.Parent;
                    }

                    TreeNode Node = new TreeNode();
                    Node.Name = FunctionInfo.Index;
                    Node.Text = FunctionInfo.Name;
                    Node.SelectedImageIndex = 1;
                    Node.ImageIndex = 1;
                    ParentNode.Nodes.Add(Node);

                    Node.EnsureVisible();
                }
            }
        }

        #endregion 向导页跳转校验

        #region 设置写数函数取数条件

        private void ConditionList_CellClick(object sender, CellClickEventArgs e)
        {
            //点中的是一个有效的单元格
            if (e.Row > -1 && e.Column > -1)
            {
                Rectangle Rect = ConditionList.GetCellRectangle(
                                                                ConditionList.GetActiveRowViewportIndex(),
                                                                ConditionList.GetActiveColumnViewportIndex(),
                                                                e.Row,
                                                                e.Column);

                Point ScreenPoint = ConditionList.PointToScreen(Rect.Location);
                Point ClientPoint = wizardPage2.PointToClient(ScreenPoint);
                switch (e.Column)
                {
                    case 0:
                        WriteToTable_DataItem_Editor.Location = ClientPoint;
                        WriteToTable_DataItem_Editor.Size = Rect.Size;
                        WriteToTable_DataItem_Editor.BringToFront();
                        WriteToTable_DataItem_Editor.DroppedDown = true;
                        break;
                    case 1:
                        WriteToTable_Condition_Editor.Location = ClientPoint;
                        WriteToTable_Condition_Editor.Size = Rect.Size;
                        WriteToTable_Condition_Editor.BringToFront();
                        WriteToTable_Condition_Editor.DroppedDown = true;
                        break;
                    case 2:
                        WriteToTable_Value_Editor.Location = ClientPoint;
                        WriteToTable_Value_Editor.Size = Rect.Size;
                        WriteToTable_Value_Editor.BringToFront();
                        WriteToTable_Value_Editor.DroppedDown = true;
                        break;
                }

                ConditionList.ActiveSheet.SetActiveCell(e.Row, e.Column);
            }
        }

        private void WriteToTable_DataItem_Editor_DropDownClosed(object sender, EventArgs e)
        {
            if (ConditionList.ActiveSheet.ActiveRow.Tag == null)
            {
                ConditionList.ActiveSheet.ActiveRow.Tag = new ExpressionInfo();
            }

            ExpressionInfo expressionInfo = ConditionList.ActiveSheet.ActiveRow.Tag as ExpressionInfo;
            expressionInfo.DataItem = WriteToTable_DataItem_Editor.SelectedItem as FieldInfo;
            ConditionList.ActiveSheet.Cells[ConditionList.ActiveSheet.ActiveRowIndex, 0].Value = WriteToTable_DataItem_Editor.SelectedItem;

            WriteToTable_DataItem_Editor.SendToBack();
        }

        private void WriteToTable_Condition_Editor_DropDownClosed(object sender, EventArgs e)
        {
            if (ConditionList.ActiveSheet.ActiveRow.Tag == null)
            {
                ConditionList.ActiveSheet.ActiveRow.Tag = new ExpressionInfo();
            }

            ExpressionInfo expressionInfo = ConditionList.ActiveSheet.ActiveRow.Tag as ExpressionInfo;
            if (WriteToTable_Condition_Editor.SelectedItem != null)
            {
                expressionInfo.Operation = WriteToTable_Condition_Editor.SelectedItem.ToString();
                ConditionList.ActiveSheet.Cells[ConditionList.ActiveSheet.ActiveRowIndex, 1].Value = WriteToTable_Condition_Editor.SelectedItem.ToString();
            }

            WriteToTable_Condition_Editor.SendToBack();
        }

        private void WriteToTable_Value_Editor_FinishEditing(object sender, AT.STO.UI.Win.DropDownValueChangedEventArgs e)
        {
            if (ConditionList.ActiveSheet.ActiveRow.Tag == null)
            {
                ConditionList.ActiveSheet.ActiveRow.Tag = new ExpressionInfo();
            }

            ExpressionInfo expressionInfo = ConditionList.ActiveSheet.ActiveRow.Tag as ExpressionInfo;
            DropDownNode dropDownNode = e.Value as DropDownNode;
            if (dropDownNode.Tag is FieldInfo && dropDownNode.Parent != null)
            {
                ReadFromTableName = dropDownNode.Parent.Name;
            }
            expressionInfo.DataValue = dropDownNode.Tag as FieldInfo;
            ConditionList.ActiveSheet.Cells[ConditionList.ActiveSheet.ActiveRowIndex, 2].Value = dropDownNode.Tag as FieldInfo;

            WriteToTable_Value_Editor.SendToBack();
        }

        private void WriteToTable_Value_Editor_DropDownClosed(object sender, System.EventArgs e)
        {
            WriteToTable_Value_Editor.SendToBack();
        }

        private void Button_AddRow_Click(object sender, EventArgs e)
        {
            ConditionList.ActiveSheet.Rows.Add(ConditionList.ActiveSheet.Rows.Count-1, 1);
            ConditionList.ActiveSheet.Rows[ConditionList.ActiveSheet.Rows.Count-1].Tag = Guid.NewGuid().ToString();
        }

        private void Button_RemoveRow_Click(object sender, EventArgs e)
        {
            ConditionList.ActiveSheet.Rows.Remove(ConditionList.ActiveSheet.ActiveRowIndex, 1);
        }

        #endregion 设置写数函数取数条件

        #region  设置写数函数计算方式和写入目标

        private void ModificationList_CellClick(object sender, CellClickEventArgs e)
        {
            //点中的是一个有效的单元格
            if (e.Row > -1 && e.Column > -1)
            {
                Rectangle Rect = ModificationList.GetCellRectangle(
                                                                ModificationList.GetActiveRowViewportIndex(),
                                                                ModificationList.GetActiveColumnViewportIndex(),
                                                                e.Row,
                                                                e.Column);

                Point ScreenPoint = ConditionList.PointToScreen(Rect.Location);
                Point ClientPoint = wizardPage2.PointToClient(ScreenPoint);
                switch (e.Column)
                {
                    case 0:
                        Modification_DataItem_Editor.Location = ClientPoint;
                        Modification_DataItem_Editor.Size = Rect.Size;
                        Modification_DataItem_Editor.BringToFront();
                        Modification_DataItem_Editor.DroppedDown = true;
                        break;
                    case 1:
                        ModificationType_Editor.Location = ClientPoint;
                        ModificationType_Editor.Size = Rect.Size;
                        ModificationType_Editor.BringToFront();
                        ModificationType_Editor.DroppedDown = true;
                        break;
                    case 2:
                        ModificationValue_Editor.Location = ClientPoint;
                        ModificationValue_Editor.Size = Rect.Size;
                        ModificationValue_Editor.BringToFront();
                        ModificationValue_Editor.DroppedDown = true;
                        break;
                }

                ModificationList.ActiveSheet.SetActiveCell(e.Row, e.Column);
            }
        }

        private void Modification_DataItem_Editor_DropDownClosed(object sender, EventArgs e)
        {
            if (ModificationList.ActiveSheet.ActiveRow.Tag == null)
            {
                ModificationList.ActiveSheet.ActiveRow.Tag = new ExpressionInfo();
            }

            ExpressionInfo expressionInfo = ModificationList.ActiveSheet.ActiveRow.Tag as ExpressionInfo;
            expressionInfo.DataItem = Modification_DataItem_Editor.SelectedItem as FieldInfo;
            ModificationList.ActiveSheet.Cells[ModificationList.ActiveSheet.ActiveRowIndex,0].Value = Modification_DataItem_Editor.SelectedItem as FieldInfo;

            Modification_DataItem_Editor.SendToBack();
        }

        private void ModificationType_Editor_DropDownClosed(object sender, EventArgs e)
        {
            if (ModificationList.ActiveSheet.ActiveRow.Tag == null)
            {
                ModificationList.ActiveSheet.ActiveRow.Tag = new ExpressionInfo();
            }

            ExpressionInfo expressionInfo = ModificationList.ActiveSheet.ActiveRow.Tag as ExpressionInfo;
            if (ModificationType_Editor.SelectedItem != null)
            {
                expressionInfo.Operation = ModificationType_Editor.SelectedItem.ToString();
                ModificationList.ActiveSheet.Cells[ModificationList.ActiveSheet.ActiveRowIndex, 1].Value = ModificationType_Editor.SelectedItem.ToString();
            }

            ModificationType_Editor.SendToBack();
        }

        private void ModificationValue_Editor_FinishEditing(object sender, AT.STO.UI.Win.DropDownValueChangedEventArgs e)
        {
            if (ModificationList.ActiveSheet.ActiveRow.Tag == null)
            {
                ModificationList.ActiveSheet.ActiveRow.Tag = new ExpressionInfo();
            }

            ExpressionInfo expressionInfo = ModificationList.ActiveSheet.ActiveRow.Tag as ExpressionInfo;
            DropDownNode dropDownNode = e.Value as DropDownNode;
            expressionInfo.DataValue = dropDownNode.Tag as FieldInfo;
            ModificationList.ActiveSheet.Cells[ModificationList.ActiveSheet.ActiveRowIndex, 2].Value = dropDownNode.Tag as FieldInfo;

            ModificationValue_Editor.SendToBack();
        }

        private void ModificationValue_Editor_DropDownClosed(object sender, System.EventArgs e)
        {
            ModificationValue_Editor.SendToBack();
        }

        private void Button_AddValue_Click(object sender, EventArgs e)
        {
            ModificationList.ActiveSheet.Rows.Add(ModificationList.ActiveSheet.Rows.Count - 1, 1);
            ModificationList.ActiveSheet.Rows[ModificationList.ActiveSheet.Rows.Count - 1].Tag = Guid.NewGuid().ToString();
        }

        private void Button_DeleteValue_Click(object sender, EventArgs e)
        {
            ModificationList.ActiveSheet.Rows.Remove(ModificationList.ActiveSheet.ActiveRowIndex, 1);
        }

        #endregion  设置写数函数计算方式和写入目标
    }
}
