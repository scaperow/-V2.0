using System;
using FarPoint.Win.Spread;
using FarPoint.Win;
using System.Drawing;
using ReportCommon;
using System.Collections.Generic;

namespace ReportCommon
{
    /// <summary>
    /// 报表对象
    /// </summary>
    public class Report
    {
        private ReportConfiguration ReportConfiguration;
        private Object[,] Elements;
        private List<FloatElement> FloatElements = new List<FloatElement>();

        public Report(ReportConfiguration Configuration)
        {
            ReportConfiguration = Configuration;
        }

        public SheetView InitReport(String Index)
        {
            //加载表单配置信息
            String SheetStyle = ReportConfiguration.SheetStyle;
            if (!string.IsNullOrEmpty(SheetStyle))
            {
                Panel = Serializer.LoadObjectXml(typeof(SheetView), SheetStyle, "SheetView") as SheetView;
            }
            else
            {
                Panel = new SheetView();
                Panel.PrintInfo = PrintInfomation.Default;
            }
            
            Panel.AutoCalculation = false;
            Panel.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;

            InitElements(Panel);

            return Panel;
        }

        public Object[,] InitElements(SheetView Report)
        {
            int Column = Report.GetLastNonEmptyColumn(NonEmptyItemFlag.Data) + 1;
            int Row = Report.GetLastNonEmptyRow(NonEmptyItemFlag.Data) + 1;

            Elements = new Object[Row, Column];

            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Column; j++)
                {
                    if (Report.Cells[i, j].Value is GridElement)
                    {
                        GridElement Element = Report.Cells[i, j].Value as GridElement;
                        Element.Report = Report;
                        Elements[i, j] = Element;
                    }
                }
            }

            return Elements;
        }

        /// <summary>
        /// 获得报表元素的Index
        /// </summary>
        /// <param name="Row"></param>
        /// <param name="Column"></param>
        /// <returns></returns>
        public String GetElementIndex(int Row, int Column)
        {
            string cell = Coords.GetColumn_Row(Row, Column);
            return GetElementIndex(cell);
        }

        /// <summary>
        /// 获得报表元素的Index
        /// </summary>
        /// <param name="cellTag"></param>
        /// <returns></returns>
        public String GetElementIndex(string cell)
        {
            if (string.IsNullOrEmpty(cell))
                return "";

            if (Panel.Cells[cell].Tag == null)
            {
                Panel.Cells[cell].Tag = Guid.NewGuid().ToString();
            }

            return Panel.Cells[cell].Tag.ToString();
        }

        /// <summary>
        /// 获得某个位置上的报表元素类型
        /// </summary>
        public GridElement GetElement(string Index)
        {
            if (string.IsNullOrEmpty(Index))
                return null;

            Cell Cell = ReportSheet.GetCellFromTag(null, Index);
            GridElement Element = Cell.Value as GridElement;

            if (Element != null)
                Element.Report = ReportSheet;

            return Element;
        }

        public void SetElement(GridElement Element)
        {
            if (Element == null)
                return;

            Cell Cell = ReportSheet.Cells[Element.getLiteralCell()];
            Cell.VerticalAlignment = CellVerticalAlignment.Center;
            Cell.HorizontalAlignment = CellHorizontalAlignment.Center;
            Cell.Value = Element.Value.ToString();
        }

        /// <summary>
        /// 清除元素的全部
        /// </summary>
        public void DelElement(GridElement Element)
        {
            if (Element == null)
                return;

            Cell Cell = ReportSheet.GetCellFromTag(null, Element.Index);

            Cell.BackColor = Color.White;
            Cell.ForeColor = Color.Black;
            Cell.Border = null;
            Cell.Value = null;
            Cell.Tag = null;
        }

        /// <summary>
        /// 清除元素的风格
        /// </summary>
        /// <param name="Element"></param>
        public void DelStyle(GridElement Element)
        {
            if (Element == null)
                return;

            Cell Cell = ReportSheet.GetCellFromTag(null, Element.Index);

            Cell.BackColor = Color.White;
            Cell.ForeColor = Color.Black;
            Cell.Border = null;

            Element.Style.FormatInfo = null;
        }

        /// <summary>
        /// 清除元素的内容
        /// </summary>
        /// <param name="ELement"></param>
        public void DelContent(GridElement Element)
        {
            if (Element == null)
                return;

            Cell Cell = ReportSheet.GetCellFromTag(null, Element.Index);

            Cell.Value = null;
            Cell.Tag = null;
        }

        public List<FloatElement> InitFloatElements(SheetView Report)
        {
            FloatElements.Clear();
            foreach (IElement Element in Report.DrawingContainer.ContainedObjects)
            {
                if (Element is FloatElement)
                {
                    FloatElement localFloatElement = Element as FloatElement;
                    FloatElements.Add(localFloatElement);
                }
            }

            return FloatElements;
        }

        /// <summary>
        /// 添加浮动元素
        /// </summary>
        /// <param name="Element"></param>
        public void SetFloatElement(FloatElement Element)
        {
            ReportSheet.DrawingContainer.ContainedObjects.Add(Element);
        }

        /// <summary>
        /// 删除浮动元素
        /// </summary>
        /// <param name="Element"></param>
        public void DelFloatElement(FloatElement Element)
        {
            ReportSheet.DrawingContainer.ContainedObjects.Remove(Element);
        }

        public Boolean HasReportParameters()
        {
            Boolean Result = false;

            Object[,] Elements = InitElements(Panel);
            for (int i = 0; i < Elements.GetLength(0); i++)
            {
                for (int j = 0; j < Elements.GetLength(1); j++)
                {
                    if (Elements[i, j] is GridElement)
                    {
                        GridElement Element = Elements[i, j] as GridElement;
                        if (Element.Value is DataColumn)
                        {
                            DataColumn DataColumn = Element.Value as DataColumn;
                            if (DataColumn.DataFilter != null)
                            {
                                foreach (FilterCondition Condition in DataColumn.DataFilter.FilterConditions)
                                {
                                    if (Condition.RightItem != null && Condition.RightItem.Style == FilterStyle.Parameter)
                                    {
                                        Result = true;
                                        break;
                                    }
                                }
                            }
                        }
                        else if (Element.Value is Formula)
                        {

                        }
                    }
                }
            }

            return Result;
        }

        public void RefreshReport(SheetView Report)
        {
            int Column = Report.GetLastNonEmptyColumn(NonEmptyItemFlag.Data) + 1;
            int Row = Report.GetLastNonEmptyRow(NonEmptyItemFlag.Data) + 1;

            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Column; j++)
                {
                    if (Report.Cells[i, j].Value is GridElement)
                    {
                        GridElement Element = Report.Cells[i, j].Value as GridElement;
                        Element.Row = Report.Cells[i, j].Row.Index;
                        Element.Column = Report.Cells[i, j].Column.Index;
                        Element.RowSpan = Report.Cells[i, j].RowSpan;
                        Element.ColumnSpan = Report.Cells[i, j].ColumnSpan;

                        if (Element.Value is Formula)
                        {
                            Formula formula = Element.Value as Formula;
                            formula.Expression = Report.Cells[i, j].Formula;
                        }

                        Report.Cells[i, j].Invalidate();
                    }
                }
            }
        }

        public ReportConfiguration Configuration
        {
            get
            {
                return ReportConfiguration;
            }
        }

        SheetView Panel = null;
        public SheetView ReportSheet
        {
            get
            {
                return Panel;
            }
        }
    }
}
