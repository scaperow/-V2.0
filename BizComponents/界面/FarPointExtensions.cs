using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;

namespace FarPoint.Win.Spread
{
    public static class FarPointExtensions
    {
        public static Row[] GetSelectionRows(SheetView sheet)
        {
            var rows = new List<Row>();
            var ranges = sheet.GetSelections();
            foreach (var range in ranges)
            {
                var i = range.Row;
                var c = range.RowCount + i;

                for (; i < c; i++)
                {
                    var row = sheet.Rows[i];

                    rows.Add(row);
                }
            }

            return rows.ToArray();
        }

        public static SheetView CloneSheet(SheetView sheet, int rowCount)
        {
            var n = new SheetView();
            n.Rows.Count = rowCount;
            n.Columns.Count = sheet.Columns.Count;
            n.DefaultStyle = sheet.DefaultStyle;
            n.OperationMode = sheet.OperationMode;
            var index = 0;
            foreach (Column c in sheet.Columns)
            {
                n.Columns[index].Label = c.Label;
                n.Columns[index].CellType = c.CellType;
                n.Columns[index].Width = c.Width;

                index++;
            }

            return n;
        }

        public static void LockCell(Cell cell, Object tag)
        {
            cell.CellType = new TextCellType();
            cell.Locked = true;
            cell.Text = "";
            cell.Value = "";
            cell.Tag = tag;
        }
    }
}
