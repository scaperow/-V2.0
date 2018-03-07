using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.Win.Spread;
using ReportCommon;

namespace Yqun.BO.ReportE.Core
{
    public class SheetUtil
    {
        public static String calculateReportDefaultLeftParent(SheetView ReportSheet, Object[,] Elements, String Cell)
        {
            int[] coord = Coords.ConvertColumn_Row(Cell);
            int ActiveRow = coord[1];
            int ActiveCol = coord[0];
            int ActiveRowSpan = ReportSheet.Cells[Cell].RowSpan;
            int ActiveColSpan = ReportSheet.Cells[Cell].ColumnSpan;

            GridElement Result = null;
            int MinValue = int.MaxValue;
            for (int i = 0; i < Elements.GetLength(0); i++)
            {
                for (int j = 0; j < Elements.GetLength(1); j++)
                {
                    GridElement Element = Elements[i, j] as GridElement;
                    if (Element == null)
                        continue;

                    if (Element.getLiteralCell() == Cell.ToLower())
                        continue;

                    DataColumn data = Element.Value as DataColumn;
                    if (data != null && data.DataSetting == DataSetting.Aggregation)
                        continue;

                    if (Element.Value is Formula)
                        continue;

                    if (Element.Value is DataColumn)
                    {
                        Cell cell = ReportSheet.Cells[Element.getLiteralCell()];
                        if (ActiveRow >= cell.Row.Index && ActiveRow < cell.Row.Index + cell.RowSpan &&
                            cell.Column.Index < ActiveCol)
                        {
                            if (Math.Abs(cell.Column.Index - ActiveCol) < MinValue)
                            {
                                MinValue = Math.Abs(cell.Column.Index - ActiveCol);
                                Result = Element;
                            }
                        }
                    }
                }
            }

            if (Result != null && Result.ExpandOrientation.Orientation == ExpandOrientation.TopToBottom)
            {
                Cell r = ReportSheet.Cells[Result.getLiteralCell()];
                return Coords.GetColumn_Row(r.Row.Index, r.Column.Index);
            }
            else
            {
                return "";
            }
        }

        public static String calculateReportDefaultTopParent(SheetView ReportSheet, Object[,] Elements, String Cell)
        {
            int[] coord = Coords.ConvertColumn_Row(Cell);
            int ActiveRow = coord[1];
            int ActiveCol = coord[0];
            int ActiveRowSpan = ReportSheet.Cells[Cell].RowSpan;
            int ActiveColSpan = ReportSheet.Cells[Cell].ColumnSpan;

            GridElement Result = null;
            int MinValue = int.MaxValue;
            for (int i = 0; i < Elements.GetLength(0); i++)
            {
                for (int j = 0; j < Elements.GetLength(1); j++)
                {
                    GridElement Element = Elements[i, j] as GridElement;
                    if (Element == null)
                        continue;

                    if (Element.getLiteralCell() == Cell.ToLower())
                        continue;

                    DataColumn data = Element.Value as DataColumn;
                    if (data != null && data.DataSetting == DataSetting.Aggregation)
                        continue;

                    if (Element.Value is Formula)
                        continue;

                    if (Element.Value is DataColumn)
                    {
                        Cell cell = ReportSheet.Cells[Element.getLiteralCell()];
                        if (ActiveCol >= cell.Column.Index && ActiveCol < cell.Column.Index + cell.ColumnSpan &&
                            cell.Row.Index < ActiveRow)
                        {
                            if (Math.Abs(cell.Row.Index - ActiveRow) < MinValue)
                            {
                                MinValue = Math.Abs(cell.Row.Index - ActiveRow);
                                Result = Element;
                            }
                        }
                    }
                }
            }

            if (Result != null && Result.ExpandOrientation.Orientation == ExpandOrientation.LeftToRight)
            {
                Cell r = ReportSheet.Cells[Result.getLiteralCell()];
                return Coords.GetColumn_Row(r.Row.Index, r.Column.Index);
            }
            else
            {
                return "";
            }
        }
 
        public static void calculateReportDefaultParent(SheetView paramReport, Object[,] Elements)
        {
            //GetLastNonEmptyRow方法获得是某一行或某一列的索引，所以行数(列数) = 行(列)索引号 + 1
            int ColumnCount = paramReport.GetLastNonEmptyColumn(NonEmptyItemFlag.Data) + 1;
            int RowCount = paramReport.GetLastNonEmptyRow(NonEmptyItemFlag.Data) + 1;

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    if (paramReport.Cells[i, j].Value is GridElement)
                    {
                        GridElement Element = paramReport.Cells[i, j].Value as GridElement;
                        ExpandOrientation Orientation = Element.ExpandOrientation;

                        if (Orientation.isDefaultLeftParent)
                        {
                            Orientation.LeftParent = calculateReportDefaultLeftParent(paramReport, Elements, Element.getLiteralCell());
                        }

                        if (Orientation.isDefaultTopParent)
                        {
                            Orientation.TopParent = calculateReportDefaultTopParent(paramReport, Elements, Element.getLiteralCell());
                        }
                    }
                }
            }
        }
    }
}
