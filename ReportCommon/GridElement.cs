using System;
using FarPoint.Win.Spread;
using Yqun.Common.Encoder;

namespace ReportCommon
{
    /// <summary>
    /// 单元格元素
    /// </summary>
    [Serializable]
    public class GridElement : ICloneable
    {
        public GridElement()
        {
        }

        public GridElement(int row, int col,int rowspan,int colspan)
        {
            this.rowIndex = row;
            this.columnIndex = col;
            this.rowSpan = rowspan;
            this.columnSpan = colspan;
        }

        String _Index = Guid.NewGuid().ToString();
        public String Index
        {
            get
            {
                return _Index;
            }
            set
            {
                _Index = value;
            }
        }

        [NonSerialized]
        SheetView _report = null;
        public SheetView Report
        {
            get
            {
                return _report;
            }
            set
            {
                _report = value;
            }
        }

        int rowIndex;
        public int Row
        {
            get
            {
                return rowIndex;
            }
            set
            {
                rowIndex = value;
            }
        }

        int rowSpan;
        public int RowSpan
        {
            get
            {
                return rowSpan;
            }
            set
            {
                rowSpan = value;
            }
        }

        int columnIndex;
        public int Column
        {
            get
            {
                return columnIndex;
            }
            set
            {
                columnIndex = value;
            }
        }

        int columnSpan;
        public int ColumnSpan
        {
            get
            {
                return columnSpan;
            }
            set
            {
                columnSpan = value;
            }
        }

        object _value;
        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        Style style = Style.Empty;
        public Style Style
        {
            get
            {
                return style;
            }
            set
            {
                style = value;
            }
        }

        PageBreak pageBreak = PageBreak.Empty;
        public PageBreak PageBreak
        {
            get
            {
                return pageBreak;
            }
            set
            {
                pageBreak = value;
            }
        }

        ExpandOrientation expandOrientation = ExpandOrientation.Default;
        public ExpandOrientation ExpandOrientation
        {
            get
            {
                return expandOrientation;
            }
            set
            {
                expandOrientation = value;
            }
        }

        public String getLiteralCell()
        {
            String LiteralColumn = Arabic_Numerals_Convert.Excel_Word_Numerals(Column);
            String LiteralRow = (Row + 1).ToString();
            return LiteralColumn + LiteralRow;
        }

        public static GridElement FromCell(Cell cell)
        {
            GridElement Element = new GridElement();
            Element.Row = cell.Row.Index;
            Element.Column = cell.Column.Index;
            Element.RowSpan = cell.RowSpan;
            Element.columnSpan = cell.ColumnSpan;
            Element.Value = cell.Value;

            return Element;
        }

        public override string ToString()
        {
            return "(" + this.Row + "," + this.Column + "," + this.rowSpan + "," + this.columnSpan + ")";
        }

        public object Clone()
        {
            GridElement Element = new GridElement();
            Element.Index = this.Index;
            Element.Row = this.Row;
            Element.Column = this.Column;
            Element.RowSpan = this.RowSpan;
            Element.ColumnSpan = this.ColumnSpan;
            Element.Value = this.Value;
            Element.Style = this.Style.Clone() as Style;
            Element.PageBreak = this.PageBreak.Clone() as PageBreak;
            Element.ExpandOrientation = this.ExpandOrientation.Clone() as ExpandOrientation;

            return Element;
        }
    }
}
