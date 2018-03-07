using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.Win.Spread.CellType;

namespace BizComponents
{
    public class CellTypeFactory
    {
        public static ICellType CreateCellType(string CellType)
        {
            ICellType Result = null;

            switch (CellType.ToLower())
            {
                case "文本":
                    Result = new TextCellType();
                    break;
                case "数字":
                    Result = new NumberCellType();
                    break;
                case "百分号":
                    Result = new PercentCellType();
                    break;
                case "图片":
                    Result = new ImageCellType();
                    break;
                case "超链接":
                    Result = new HyperLinkCellType();
                    break;
                case "货币":
                    Result = new CurrencyCellType();
                    break;
                case "日期时间":
                    Result = new DateTimeCellType();
                    break;
                case "复选框":
                    Result = new CheckBoxCellType();
                    break;
                case "上下标":
                    Result = new RichTextCellType();
                    break;
                case "条形码":
                    Result = new BarCodeCellType();
                    break;
                case "下拉框":
                    Result = new DownListCellType();
                    break;
                case "掩码":
                    Result = new MaskCellType();
                    break;
                case "删除线":
                    Result = new DeleteLineCellType();
                    break;
                default:
                    Result = new TextCellType();
                    break;
            }

            return Result;
        }
    }
}
