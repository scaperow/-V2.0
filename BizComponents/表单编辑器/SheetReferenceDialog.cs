using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Yqun.Services;
using Yqun.Controls;
using BizCommon;
using Yqun.Common.Encoder;
using Lassalle.Flow;

namespace BizComponents
{
    public partial class SheetReferenceDialog : Form
    {
        const int TableLeft = 20;
        const int TableTop = 20;
        const int TableWidth = 200;

        DownListCellType CellType = null;
        SheetConfiguration SheetConfiguration = null;
        int RowIndex;
        int ColumnIndex;

        String TableName, ColumnName;

        String ReferenceTableName;
        DataFilterCondition DataFilter;

        public SheetReferenceDialog(int RowIndex, int ColumnIndex, DownListCellType CellType)
        {
            InitializeComponent();

            this.SheetConfiguration = SheetConfiguration;
            this.RowIndex = RowIndex;
            this.ColumnIndex = ColumnIndex;
            this.CellType = CellType;

            DataFilter = new DataFilterCondition();
        }

        private void SheetReferenceDialog_Load(object sender, EventArgs e)
        {
            //显示参照关系图
            String Cell = Arabic_Numerals_Convert.Excel_Word_Numerals(ColumnIndex) + (RowIndex + 1).ToString();
            InitReferenceDiagram(Cell);
        }

        /// <summary>
        /// 向AddFlow控件里面添加一个表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender == Button_Append)
            {
                SheetConfigurationDialog Dialog = new SheetConfigurationDialog();
                if (DialogResult.OK == Dialog.ShowDialog())
                {
                    String TableName = Dialog.SelectedTable;
                    AppendTableByTableName(TableName, true);
                    ReferenceTableName = TableName;
                }
            }
        }

        private void InitReferenceDiagram(String Cell)
        {
            tableFlow1.Clear();

            SheetReference Reference = CellType.ReferenceInfo as SheetReference;
            if (Reference != null && !String.IsNullOrEmpty(Reference.ReferenceXml))
            {
                TableName = Reference.TableName;
                ColumnName = Reference.ColumnName;
                DataFilter = Reference.DataFilter;
                LoadFromXml(Reference.ReferenceXml);
            }
            else
            {
                DataTableSchema DataTable = SheetConfiguration.DataTableSchema;
                bool HaveItem = DataTable.HaveDataItem(Cell);
                if (HaveItem)
                {
                    FieldDefineInfo FieldInfo = DataTable.GetDataItem(Cell);
                    if (FieldInfo != null && DataTable.Schema != null)
                    {
                        TableName = DataTable.Schema.Name;
                        ColumnName = FieldInfo.FieldName;
                        AppendTableByTableName(TableName, false);
                    }
                    else
                    {
                        MessageBox.Show("表单‘" + SheetConfiguration.Description + "’没有设置数据项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void AppendTableByTableName(string TableName, bool IsReferencedTable)
        {
            if (!HaveTableDiagram(TableName))
            {
                TableDefineInfo TableDefineInfo = DepositoryTableDefineInfo.GetTableDefineInfo(TableName);

                int TableHeight = tableFlow1.FieldHeight;
                foreach (FieldDefineInfo fieldInfo in TableDefineInfo.FieldInfos)
                {
                    if (!ColumnRegular.JudgeColumnName(fieldInfo.Description))
                    {
                        TableHeight = TableHeight + tableFlow1.FieldHeight;
                    }
                }

                Node tableNode = tableFlow1.AddTable(TableDefineInfo.Description, TableLeft, TableTop, TableWidth, TableHeight);
                tableNode.Properties["Name"].Value = TableDefineInfo.Name;
                tableNode.Properties["Text"].Value = TableDefineInfo.Description;

                foreach (FieldDefineInfo fieldInfo in TableDefineInfo.FieldInfos)
                {
                    if (!ColumnRegular.JudgeColumnName(fieldInfo.Description))
                    {
                        Node FieldNode = tableFlow1.AddField(tableNode, fieldInfo.Description + "(" + fieldInfo.FieldType.DisplayType + ")", !IsReferencedTable, IsReferencedTable, Color.Transparent);
                        FieldNode.Properties["Name"].Value = fieldInfo.FieldName;
                        FieldNode.Properties["Text"].Value = fieldInfo.Description;
                        FieldNode.Properties["DispType"].Value = fieldInfo.FieldType.DisplayType;
                    }
                }
            }
        }

        private bool HaveTableDiagram(String TableName)
        {
            Boolean Result = false;
            foreach (Node node in tableFlow1.addFlow_Tables.Nodes)
            {
                if (tableFlow1.IsTableNode(node) && node.Properties["Name"].Value.ToString() == TableName)
                {
                    if (node.Properties["Name"].Value.ToString() == TableName)
                    {
                        Result = true;
                        break;
                    }
                }
            }

            return Result;
        }

        private void LoadFromXml(string ReferenceXml)
        {
            tableFlow1.LoadFromXml(ReferenceXml);
        }

        private string SaveAsXml()
        {
            return tableFlow1.SaveAsXml();
        }

        private List<ReferenceItem> GetReferenceItems()
        {
            List<ReferenceItem> ReferenceItems = new List<ReferenceItem>();
            foreach (Node node in tableFlow1.addFlow_Tables.Nodes)
            {
                if (tableFlow1.IsField(node) && node.InLinkable)
                {
                    if (node.Links.Count > 0)
                    {
                        Link link = node.Links[0];

                        ReferenceItem Item = new ReferenceItem();
                        Item.TableName = link.Dst.Parent.Properties["Name"].Value.ToString();
                        Item.ColumnName = link.Dst.Properties["Name"].Value.ToString();
                        Item.ReferenceTableName = link.Org.Parent.Properties["Name"].Value.ToString();
                        Item.ReferenceTableText = link.Org.Parent.Properties["Text"].Value.ToString();
                        Item.ReferenceColumnName = link.Org.Properties["Name"].Value.ToString();
                        Item.ReferenceColumnText = link.Org.Properties["Text"].Value.ToString();

                        ReferenceItems.Add(Item);
                    }
                }
            }

            return ReferenceItems;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            SheetReferenceFilterDialog ReferenceFilterDialog = new SheetReferenceFilterDialog(ReferenceTableName, DataFilter);
            ReferenceFilterDialog.ShowDialog();
        }

        private void Button_Ok_Click(object sender, EventArgs e)
        {
            CellType.ReferenceStyle = BizCommon.ReferenceStyle.Sheet;
            SheetReference Reference = CellType.ReferenceInfo as SheetReference;
            Reference.TableName = TableName;
            Reference.ColumnName = ColumnName;
            Reference.ReferenceItems = GetReferenceItems();
            Reference.ReferenceXml = SaveAsXml();
            Reference.DataFilter = DataFilter;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
