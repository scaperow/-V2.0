using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.Model;
using Yqun.Common.Encoder;
using Yqun.Common.ContextCache;
using BizComponents;
using Yqun.Bases.ClassBases;
using BizCommon;
using Yqun.Services;
using System.Text;
using System.Linq;
using FarPoint.Win.Spread.CellType;

namespace BizModules
{
    public partial class SheetDataItemDialog : Form
    {
        SheetDesinger sheetDesigner;
        List<Cell> cells;

        public SheetDataItemDialog(SheetDesinger Designer, List<Cell> _cells)
        {
            InitializeComponent();
            sheetDesigner = Designer;
            cells = (from n in _cells orderby n.ToString() select n).ToList();
        }

        private void DataItemForm_Load(object sender, EventArgs e)
        {
            foreach (Cell cell in cells)
            {
                DataItems.ActiveSheet.Rows.Add(DataItems.ActiveSheet.Rows.Count, 1);
                int RowIndex = DataItems.ActiveSheet.Rows.Count - 1;
                DataItems.ActiveSheet.Rows[RowIndex].Tag = cell;
                DataItems.ActiveSheet.Cells[RowIndex, 0].Value = cell.Column.Label + cell.Row.Label;
                JZCellProperty property = cell.Tag as JZCellProperty;
                IGetFieldType FieldTypeGetter = cell.CellType as IGetFieldType;
                if (FieldTypeGetter != null)
                {
                    DataItems.ActiveSheet.Cells[RowIndex, 2].Value = FieldTypeGetter.FieldType.Description;
                }
                if (property == null)
                {
                    property = new JZCellProperty();
                    cell.Tag = property;
                }
                DataItems.ActiveSheet.Cells[RowIndex, 1].Value = property.Description;
                DataItems.ActiveSheet.Cells[RowIndex, 3].Value = property.IsUnique;
                DataItems.ActiveSheet.Cells[RowIndex, 4].Value = property.IsNotNull;
                DataItems.ActiveSheet.Cells[RowIndex, 5].Value = property.IsNotCopy;
                DataItems.ActiveSheet.Cells[RowIndex, 6].Value = property.IsPingxing;
                DataItems.ActiveSheet.Cells[RowIndex, 7].Value = property.IsReadOnly;
                DataItems.ActiveSheet.Cells[RowIndex, 8].Value = property.IsKey;
            }
        }

        private void Button_ok_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DataItems.ActiveSheet.Rows.Count; i++)
            {
                Cell cell = DataItems.ActiveSheet.Rows[i].Tag as Cell;
                if (cell != null)
                {
                    JZCellProperty property = cell.Tag as JZCellProperty;
                    if (property == null)
                    {
                        property = new JZCellProperty();
                        cell.Tag = property;
                    }
                    if (DataItems.ActiveSheet.Cells[i, 1].Value != null)
                    {
                        property.Description = DataItems.ActiveSheet.Cells[i, 1].Value.ToString();
                    }
                    
                    property.IsUnique = System.Convert.ToBoolean(DataItems.ActiveSheet.Cells[i, 3].Value);
                    property.IsNotNull = System.Convert.ToBoolean(DataItems.ActiveSheet.Cells[i, 4].Value);
                    property.IsNotCopy = System.Convert.ToBoolean(DataItems.ActiveSheet.Cells[i, 5].Value);
                    property.IsPingxing = System.Convert.ToBoolean(DataItems.ActiveSheet.Cells[i, 6].Value);
                    property.IsReadOnly = System.Convert.ToBoolean(DataItems.ActiveSheet.Cells[i, 7].Value);
                    property.IsKey = System.Convert.ToBoolean(DataItems.ActiveSheet.Cells[i, 8].Value);

                    if (cell.CellType != null && DataItems_Sheet1.Cells[i, 2].Value.ToString() == cell.CellType.ToString())
                    {
                    }
                    else
                    {
                        IGetFieldType FieldTypeGetter = CellTypeFactory.CreateCellType(DataItems_Sheet1.Cells[i, 2].Value.ToString()) as IGetFieldType;
                        cell.CellType = FieldTypeGetter as ICellType;
                    }

                    AddDataAreaCell(cell);
                }
            }

            Close();
            String Message = "设置完毕，点击保存按钮后生效";
            MessageBoxIcon Icon = MessageBoxIcon.Information;
            MessageBox.Show(Message, "提示", MessageBoxButtons.OK, Icon);
        }

        private void Button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddDataAreaCell(Cell cell)
        {
            if (!sheetDesigner.dataCells.Contains(cell))
            {
                sheetDesigner.dataCells.Add(cell);
            }
        }
    }   

}
