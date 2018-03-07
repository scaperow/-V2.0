using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.Win.Spread;

namespace ReportCommon
{
    public class PrintInfomation
    {
        public static PrintInfo Default;
        static PrintInfomation()
        {
            Default = new PrintInfo();

            Default.UseMax = true;
            Default.BestFitCols = false;
            Default.BestFitRows = false;
            Default.ZoomFactor = 1;

            Default.ShowBorder = false;
            Default.ShowColor = false;
            Default.ShowColumnHeader = PrintHeader.Hide;
            Default.ShowRowHeader = PrintHeader.Hide;
            Default.ShowGrid = false;
            Default.ShowShadows = false;
            Default.ShowPrintDialog = true;

            Default.Header = "";
            Default.Footer = "";

            Default.Margin.Top = (int)UnitConverter.MillimeterToCentiInch(3M * 10);
            Default.Margin.Bottom = (int)UnitConverter.MillimeterToCentiInch(2M * 10);
            Default.Margin.Left = (int)UnitConverter.MillimeterToCentiInch(2.5M * 10);
            Default.Margin.Right = (int)UnitConverter.MillimeterToCentiInch(2.5M * 10);
            Default.Margin.Header = (int)UnitConverter.MillimeterToCentiInch(0 * 10);
            Default.Margin.Footer = (int)UnitConverter.MillimeterToCentiInch(0 * 10);

            Default.Orientation = PrintOrientation.Portrait;
            Default.PageOrder = PrintPageOrder.DownThenOver;
            Default.Centering = Centering.None;
            Default.PaperSize = new System.Drawing.Printing.PaperSize("A4", 827, 1169);
        }
    }

    public class Papers
    {
        static Papers()
        {
            if (_PaperDic == null)
            {
                _PaperDic = new Dictionary<string, string>();
                _PaperDic.Add("A3", "A3,1169,1654");
                _PaperDic.Add("A4", "A4,827,1169");
                _PaperDic.Add("A5", "A5,583,827");
                _PaperDic.Add("B5", "B5,717,1012");
            }
        }

        static Dictionary<string, string> _PaperDic;
        public static Dictionary<string, string> PaperDic
        {
            get
            {
                return _PaperDic;
            }
            set
            {
                _PaperDic = value;
            }
        }
    }
}
