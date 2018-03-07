using System;
using System.Drawing;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using BizComponents;
using BizCommon;
using FarPoint.Win;
using FarPoint.Win.Spread.Model;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using FarPoint.Win.Spread.CellType;

namespace BizModules
{
    public partial class DataItemsViewDialog : Form
    {
        SheetDesinger moduleDesigner;
        public List<Cell> cells;
        public DataItemsViewDialog(SheetDesinger Designer, List<Cell> _cells)
        {
            InitializeComponent();
            cells = (from n in  _cells orderby n.ToString() select n).ToList();
            moduleDesigner = Designer;
        }

        private void DetailDataItems_Load(object sender, EventArgs e)
        {
            DataItems_Sheet1.Columns.Count = 9;
            DataItems_Sheet1.ColumnHeader.Columns[0].Label = "位置";
            DataItems_Sheet1.ColumnHeader.Columns[1].Label = "描述";
            DataItems_Sheet1.ColumnHeader.Columns[2].Label = "单元格类型";
            DataItems_Sheet1.ColumnHeader.Columns[3].Label = "值唯一";
            DataItems_Sheet1.ColumnHeader.Columns[4].Label = "不允许空";
            DataItems_Sheet1.ColumnHeader.Columns[5].Label = "不可复制";
            DataItems_Sheet1.ColumnHeader.Columns[6].Label = "可平行";
            DataItems_Sheet1.ColumnHeader.Columns[7].Label = "只读";
            DataItems_Sheet1.ColumnHeader.Columns[8].Label = "关键值";
            TableList.Items.Add(moduleDesigner.SheetName);
           
            if (TableList.Items.Count > 0)
            {
                TableList.SelectedIndex = 0;
            }
        }

        private void TableList_SelectedIndexChanged(object sender, EventArgs e)
        {            
            foreach (Cell cell in cells)
            {
                
                DataItems_Sheet1.Rows.Add(DataItems_Sheet1.Rows.Count, 1);
                int RowIndex = DataItems_Sheet1.Rows.Count - 1;
                DataItems_Sheet1.Rows[RowIndex].Tag = cell;
                DataItems_Sheet1.Cells[RowIndex, 0].Value = cell.Column.Label+cell.Row.Label;
                JZCellProperty property = cell.Tag as JZCellProperty;
                IGetFieldType FieldTypeGetter = cell.CellType as IGetFieldType;
                if (FieldTypeGetter != null)
                {
                    DataItems_Sheet1.Cells[RowIndex, 2].Value = FieldTypeGetter.FieldType.Description;
                }

                if (property != null)
                {
                    DataItems_Sheet1.Cells[RowIndex, 1].Value = property.Description;
                    DataItems_Sheet1.Cells[RowIndex, 3].Value = property.IsUnique;
                    DataItems_Sheet1.Cells[RowIndex, 4].Value = property.IsNotNull;
                    DataItems_Sheet1.Cells[RowIndex, 5].Value = property.IsNotCopy;
                    DataItems_Sheet1.Cells[RowIndex, 6].Value = property.IsPingxing;
                    DataItems_Sheet1.Cells[RowIndex, 7].Value = property.IsReadOnly;
                    DataItems_Sheet1.Cells[RowIndex, 8].Value = property.IsKey;
                }
                else
                {
                    DataItems_Sheet1.Rows[RowIndex].BackColor = Color.Red;
                }
            }
        }

        private void Button_deletetable_Click(object sender, EventArgs e)
        {
            Int32 activeRowIndex = DataItems.ActiveSheet.ActiveRowIndex;
            Cell cell = DataItems_Sheet1.Rows[activeRowIndex].Tag as Cell;
            if (cell != null)
            {
                cells.Remove(cell);
                DataItems_Sheet1.Rows.Remove(activeRowIndex, 1);
            }
        }

        private void ButtonAll_Click(object sender, EventArgs e)
        {
            CellRange cr = DataItems_Sheet1.GetSelection(0);
            if (cr != null && cr.Column != -1 && cr.Row == -1 &&
                DataItems_Sheet1.Columns[cr.Column].CellType is FarPoint.Win.Spread.CellType.CheckBoxCellType)
            {
                foreach (Row Row in DataItems_Sheet1.Rows)
                {
                    DataItems_Sheet1.Cells[Row.Index, cr.Column].Value = true;
                }
            }
        }

        private void ButtonRevert_Click(object sender, EventArgs e)
        {
            CellRange cr = DataItems_Sheet1.GetSelection(0);
            if (cr != null && cr.Column != -1 && cr.Row == -1 &&
            DataItems_Sheet1.Columns[cr.Column].CellType is FarPoint.Win.Spread.CellType.CheckBoxCellType)
            {
                foreach (Row Row in DataItems_Sheet1.Rows)
                {
                    Boolean value = Convert.ToBoolean(DataItems_Sheet1.Cells[Row.Index, cr.Column].Value);
                    DataItems_Sheet1.Cells[Row.Index, cr.Column].Value = !value;
                }
            }
        }

        private void Button_ok_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DataItems_Sheet1.Rows.Count; i++)
            {
                Cell cell = DataItems_Sheet1.Rows[i].Tag as Cell;
                if (cell != null)
                {
                    JZCellProperty property = cell.Tag as JZCellProperty;
                    if (property == null)
                    {
                        property = new JZCellProperty();
                        cell.Tag = property;
                    }
                    if (DataItems_Sheet1.Cells[i, 1].Value != null)
                    {
                        property.Description = DataItems_Sheet1.Cells[i, 1].Value.ToString();
                    }
                    property.IsUnique = Convert.ToBoolean(DataItems_Sheet1.Cells[i, 3].Value);
                    property.IsNotNull = Convert.ToBoolean(DataItems_Sheet1.Cells[i, 4].Value);
                    property.IsNotCopy = Convert.ToBoolean(DataItems_Sheet1.Cells[i, 5].Value);
                    property.IsPingxing = Convert.ToBoolean(DataItems_Sheet1.Cells[i, 6].Value);
                    property.IsReadOnly = Convert.ToBoolean(DataItems_Sheet1.Cells[i, 7].Value);
                    property.IsKey = Convert.ToBoolean(DataItems_Sheet1.Cells[i, 8].Value);
                    if (cell.CellType != null && DataItems_Sheet1.Cells[i, 2].Value.ToString() == cell.CellType.ToString())
                    {
                    }
                    else
                    {
                        IGetFieldType FieldTypeGetter = CellTypeFactory.CreateCellType(DataItems_Sheet1.Cells[i, 2].Value.ToString()) as IGetFieldType;
                        cell.CellType = FieldTypeGetter as ICellType;
                    }
                    
                }
            }
            this.DialogResult = DialogResult.OK;
            Close();
            String Message ="设置完毕，点击保存按钮后生效";
            MessageBoxIcon Icon =  MessageBoxIcon.Information;
            MessageBox.Show(Message, "提示", MessageBoxButtons.OK, Icon);
        }

        private void Button_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
