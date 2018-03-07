using System;
using FarPoint.Win;
using FarPoint.Win.Spread;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace BizCommon
{
    /// <summary>
    /// 表单配置类
    /// </summary>
    [Serializable]
    public class SheetConfiguration
    {
        static String blankSheet;
        static SheetConfiguration()
        {
            SheetView SheetView = new SheetView();
            SheetView.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.MultiRange;
            SheetView.PrintInfo = PrintInfomation.DefaultPrintInfomation.getDefaultPrintInfomation();
            blankSheet = Serializer.GetObjectXml(SheetView, "SheetView");
        }

        public static String BlankSheet
        {
            get
            {
                return blankSheet;
            }
        }

        public SheetConfiguration()
        {
            _DataTableSchema = new DataTableSchema();
            m_Index = Guid.NewGuid().ToString();
        }

        string m_Index;
        public string Index
        {
            get
            {
                return m_Index;
            }
            set
            {
                m_Index = value;
            }
        }

        string m_Description;
        public string Description
        {
            get
            {
                return m_Description;
            }
            set
            {
                m_Description = value;
            }
        }

        string m_Code;
        public string Code
        {
            get
            {
                return m_Code;
            }
            set
            {
                m_Code = value;
            }
        }

        DataTableSchema _DataTableSchema;
        public DataTableSchema DataTableSchema
        {
            get
            {
                return _DataTableSchema;
            }
            set
            {
                _DataTableSchema = value;
            }
        }

        string m_SheetStyle;
        public string SheetStyle
        {
            get
            {
                return m_SheetStyle;
            }
            set
            {
                m_SheetStyle = value;
            }
        }

        /// <summary>
        /// 检查单元格类型
        /// </summary>
        /// <returns></returns>
        public Boolean IsValidSheet(SheetView ActiveSheet, out String Information)
        {
            Boolean Result = true;
            if (DataTableSchema.Schema == null)
            {
                Information = "";
                return Result;
            }

            List<string> Fields = new List<string>();
            foreach (FieldDefineInfo FieldInfo in DataTableSchema.Schema.FieldInfos)
            {
                Result = Result & (ActiveSheet.Cells[FieldInfo.RangeInfo].CellType != null);
                if (ActiveSheet.Cells[FieldInfo.RangeInfo].CellType == null)
                    Fields.Add(string.Format("{0}[{1}]", FieldInfo.Description, FieldInfo.RangeInfo));
            }

            Information = string.Concat("错误的数据项列表：\r\n", string.Join(",\r\n", Fields.ToArray()),"\r\n\r\n请核实后再保存。");
            return Result;
        }
    }

    [Serializable]
    public class PrintInfomation : PrintInfo, ISerializeSupport, ISerializable
    {
        public static PrintInfomation DefaultPrintInfomation = new PrintInfomation();
        public PrintInfomation getDefaultPrintInfomation()
        {
            PrintInfomation DefaultInfomation = new PrintInfomation();

            DefaultInfomation.UseMax = true;
            DefaultInfomation.BestFitCols = false;
            DefaultInfomation.BestFitRows = false;
            DefaultInfomation.ZoomFactor = 1;

            DefaultInfomation.ShowBorder = false;
            DefaultInfomation.ShowColor = false;
            DefaultInfomation.ShowColumnHeader = PrintHeader.Hide;
            DefaultInfomation.ShowRowHeader = PrintHeader.Hide;
            DefaultInfomation.ShowGrid = false;
            DefaultInfomation.ShowShadows = false;
            DefaultInfomation.ShowPrintDialog = true;

            DefaultInfomation.Header = "";
            DefaultInfomation.Footer = "";

            DefaultInfomation.Margin.Top = (int)UnitConverter.MillimeterToCentiInch(3M * 10);
            DefaultInfomation.Margin.Bottom = (int)UnitConverter.MillimeterToCentiInch(2M * 10);
            DefaultInfomation.Margin.Left = (int)UnitConverter.MillimeterToCentiInch(2.5M * 10);
            DefaultInfomation.Margin.Right = (int)UnitConverter.MillimeterToCentiInch(2.5M * 10);
            DefaultInfomation.Margin.Header = (int)UnitConverter.MillimeterToCentiInch(0 * 10);
            DefaultInfomation.Margin.Footer = (int)UnitConverter.MillimeterToCentiInch(0 * 10);

            DefaultInfomation.Orientation = PrintOrientation.Landscape;
            DefaultInfomation.Centering = Centering.None;
            DefaultInfomation.PaperSize = new System.Drawing.Printing.PaperSize("A4", 827, 1169);

            return DefaultInfomation;
        }

        public bool ReMind = false;
        public bool AutoZoom = false;

        public float Zoom = 0;
        public string PaperKey = "";
        public double ErrorWidth = 0;

        public PrintInfomation()
        {

        }

        protected PrintInfomation(SerializationInfo info, StreamingContext context)
        {
            ReMind = info.GetBoolean("ReMind");
            AutoZoom = info.GetBoolean("AutoZoom");
            Zoom = (float)info.GetDouble("Zoom");
            PaperKey = info.GetString("PaperKey");
            ErrorWidth = info.GetDouble("ErrorWidth");
        }

        public new void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ReMind", ReMind);
            info.AddValue("AutoZoom", AutoZoom);
            info.AddValue("Zoom", Zoom);
            info.AddValue("PaperKey", PaperKey);
            info.AddValue("ErrorWidth", ErrorWidth);
        }
    }

    public class UnitConverter
    {
        static decimal Ratio = 3.9370080011160063M;
        public static decimal MillimeterToCentiInch(decimal mm)
        {
            decimal temp = mm * Ratio;
            return Math.Round(temp);
        }

        public static decimal CentiInchToMillimeter(decimal inch)
        {
            decimal temp = inch / Ratio;
            return Math.Round(temp);
        }
    }
}
