using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.Win.Spread.CellType;
using ReportCommon;

namespace ReportCommon
{
    public class CellFormatManager
    {
        public static ICellType CreateInstance(FormatInfo Format)
        {
            ICellType r = null;
            if (Format.Style == FormatStyle.Currency ||
                Format.Style == FormatStyle.General ||
                Format.Style == FormatStyle.Number ||
                Format.Style == FormatStyle.Percent ||
                Format.Style == FormatStyle.ScientificCount)
            {
                FarPoint.Win.Spread.CellType.GeneralCellType rs = new FarPoint.Win.Spread.CellType.GeneralCellType();
                rs.FormatString = Format.Format;
                rs.WordWrap = true;
                r = rs;
                return r;
            }
            else if (Format.Style == FormatStyle.Date ||
                     Format.Style == FormatStyle.Time)
            {
                DateTimeCellType ds = new DateTimeCellType();
                ds.UserDefinedFormat = Format.Format;
                r = ds;
                return r;
            }
            else if (Format.Style == FormatStyle.Text)
            {
                FarPoint.Win.Spread.CellType.TextCellType rs = new FarPoint.Win.Spread.CellType.TextCellType();
                rs.Multiline = true;
                rs.WordWrap = true;
                r = rs;
                return r;
            }
            else
            {
                FarPoint.Win.Spread.CellType.GeneralCellType gs = new FarPoint.Win.Spread.CellType.GeneralCellType();
                gs.FormatString = Format.Format;
                r = gs;
                return r;
            }
        }
    }
}
