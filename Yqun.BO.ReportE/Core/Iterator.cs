using System;
using System.Collections.Generic;
using System.Threading;
using System.Collections;
using FarPoint.Win.Spread;
using System.Drawing.Printing;
using ReportCommon;

namespace Yqun.BO.ReportE.Core
{
    public interface Iterator
    {
        bool hasNext();
        Object next();
        void remove();
    }

    public class PaginateReportIterator : Iterator
    {
        internal PageReport showReport;
        internal int[] pageNumberArray = null;
        internal int[] totalPageNumberArray = null;
        internal int pageCount = 0;
        internal PageGenerateThread pageGenerateThread = null;
        internal Boolean finish = false;
        internal DynamicValueList columnWidthList;
        internal DynamicValueList rowHeightList;
        internal ArrayList pageColumnIndexList = new ArrayList();
        internal ArrayList pageRowIndexList = new ArrayList();
        internal ArrayList workSheetPageLists = new ArrayList();
        internal PrintInfo reportSettings;

        public PaginateReportIterator(PageReport paramPageReport)
        {
            this.showReport = paramPageReport;
            this.reportSettings = this.showReport.getReportSettings();
            this.columnWidthList = ReportHelper.getColumnWidthList(paramPageReport.ToWorkSheet());
            this.rowHeightList = ReportHelper.getRowHeightList(paramPageReport.ToWorkSheet());
            dealWithPageInfor();
        }

        public int PageRowCount
        {
            get
            {
                return this.pageRowIndexList.Count - 1;
            }
        }

        public int PageColumnCount
        {
            get
            {
                return this.pageColumnIndexList.Count - 1;
            }
        }

        public int size()
        {
            return this.pageCount;
        }

        private void dealWithPageInfor()
        {
            int Resolution = showReport.getResolution();
            PaperSize localPaperSize = this.reportSettings.PaperSize;
            PrintMargin localMargin = this.reportSettings.Margin;
            double d1,d2;
            if (this.reportSettings.Orientation == PrintOrientation.Portrait)
            {
                d1 = Math.Min(localPaperSize.Width,localPaperSize.Height);
                d2 = Math.Max(localPaperSize.Width, localPaperSize.Height);
            }
            else
            {
                d1 = Math.Max(localPaperSize.Width, localPaperSize.Height);
                d2 = Math.Min(localPaperSize.Width, localPaperSize.Height);
            }

            int InnerWidth = (int)((d1 - localMargin.Left - localMargin.Right) * Resolution / 100.0);
            int InnerHeight = (int)((d2 - localMargin.Top - localMargin.Bottom - localMargin.Header - localMargin.Footer) * Resolution / 100.0);

            int ColumnCount = this.showReport.ToWorkSheet().GetLastNonEmptyColumn(NonEmptyItemFlag.Data) + 1;
            int RowCount = this.showReport.ToWorkSheet().GetLastNonEmptyRow(NonEmptyItemFlag.Data) + 1;

            SheetView report = this.showReport.ToWorkSheet();
            int cCount = report.Columns.Count;
            int rCount = report.Rows.Count;
            for (int i = 0; i < rCount; i++)
            {
                for (int j = 0; j < cCount; j++)
                {
                    if (report.Cells[i, j].Tag is GridElement)
                    {
                        GridElement Element = report.Cells[i, j].Tag as GridElement;
                        ColumnCount = Math.Max(ColumnCount, report.Cells[i, j].Column.Index + report.Cells[i, j].ColumnSpan);
                        RowCount = Math.Max(RowCount, report.Cells[i, j].Row.Index + report.Cells[i, j].RowSpan);
                    }
                }
            }

            List<int> rowPageBreaks_Object = new List<int>();
            List<int> columnPageBreaks_Object = new List<int>();
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    if (report.Cells[i, j].Tag is GridElement)
                    {
                        GridElement Element = report.Cells[i, j].Tag as GridElement;
                        if (Element.PageBreak.IsAfterRow)
                        {
                            rowPageBreaks_Object.Add(report.Cells[i, j].Row.Index + report.Cells[i, j].RowSpan);
                        }
                    }
                }
            }

            PrintInfo printInfo = this.showReport.ToWorkSheet().PrintInfo;
            if (printInfo == null)
            {
                this.pageColumnIndexList.Add(new IdxHeaderFooter(0));

                int totalWidth = 0;
                int columnWidth = 0;
                for (int i = 0; i < ColumnCount; i++)
                {
                    totalWidth += (columnWidth = this.columnWidthList.get(i));
                    if (totalWidth <= InnerWidth && !columnPageBreaks_Object.Contains(i))
                        continue;
                    if (((IdxHeaderFooter)this.pageColumnIndexList[this.pageColumnIndexList.Count - 1]).getIndex() == i)
                        throw new DeathCycleException("分页处理死循环出现在列：" + i);
                    this.pageColumnIndexList.Add(new IdxHeaderFooter(i));
                    totalWidth = columnWidth;
                }

                this.pageColumnIndexList.Add(new IdxHeaderFooter(ColumnCount));

                this.pageRowIndexList.Add(new IdxHeaderFooter(0));

                totalWidth = 0;
                columnWidth = 0;
                for (int i = 0; i < RowCount; i++)
                {
                    totalWidth += (columnWidth = this.rowHeightList.get(i));
                    if (totalWidth <= InnerHeight && !rowPageBreaks_Object.Contains(i))
                        continue;
                    if (((IdxHeaderFooter)this.pageRowIndexList[this.pageRowIndexList.Count - 1]).getIndex() == i)
                        throw new DeathCycleException("分页处理死循环出现在行：" + i);
                    this.pageRowIndexList.Add(new IdxHeaderFooter(i));
                    totalWidth = columnWidth;
                }

                this.pageRowIndexList.Add(new IdxHeaderFooter(RowCount));
            }
            else
            {
                List<int> headerRepeatCols_Object = new List<int>();
                List<int> footerRepeatCols_Object = new List<int>();
                int totalWidth = 0;
                List<int> footerRepeatCols = null;
                for (int i = printInfo.ColStart; i <= printInfo.ColEnd; i++)
                {
                    for (int j = 0; j < ColumnCount; j++)
                    {
                        if (this.showReport.getColumnOIIntList()[j] != i)
                            continue;
                        totalWidth += this.columnWidthList.get(j);
                        if (footerRepeatCols == null)
                            footerRepeatCols = new List<int>();
                        footerRepeatCols.Add(j);
                        footerRepeatCols_Object.Add(i);
                        break;
                    }
                }

                this.pageColumnIndexList.Add(new IdxHeaderFooter(0, null, footerRepeatCols));

                if (totalWidth > InnerWidth)
                    throw new ApplicationException("重复列宽超过了页面宽度。");

                for (int i = 0; i < ColumnCount; i++)
                {
                    if (footerRepeatCols_Object.Contains(this.showReport.getColumnOIIntList()[i]) && !columnPageBreaks_Object.Contains(i))
                    {
                        List<int> footerRepeatList = ((IdxHeaderFooter)this.pageColumnIndexList[this.pageColumnIndexList.Count - 1]).getFooterRepeatList();
                        if (footerRepeatList == null)
                            continue;
                        int Index = footerRepeatList.IndexOf(i);
                        if (Index == -1)
                            continue;
                        footerRepeatList.Remove(Index);
                    }
                    else
                    {
                        totalWidth += this.columnWidthList.get(i);
                        if ((totalWidth <= InnerWidth) && !columnPageBreaks_Object.Contains(i))
                            continue;
                        if (((IdxHeaderFooter)this.pageColumnIndexList[this.pageColumnIndexList.Count - 1]).getIndex() == i)
                            throw new DeathCycleException("分页处理死循环出现在列：" + i);

                        headerRepeatCols_Object.Clear();
                        footerRepeatCols_Object.Clear();
                        totalWidth = 0;
                        int columnPageBreakIndex = i;
                        List<int> headerRepeatList = null;
                        List<int> footerRepeatList = null;
                        while ((i < ColumnCount) && printInfo.RepeatColStart <= this.showReport.getColumnOIIntList()[i] && this.showReport.getColumnOIIntList()[i] <= printInfo.RepeatColEnd)
                        {
                            totalWidth += this.columnWidthList.get(i);
                            headerRepeatCols_Object.Add(this.showReport.getColumnOIIntList()[i]);
                            i++;
                        }

                        if (i >= ColumnCount)
                        {
                            if (totalWidth > InnerWidth)
                                throw new ApplicationException("重复列宽超过了页面宽度。");
                            this.pageColumnIndexList.Add(new IdxHeaderFooter(columnPageBreakIndex, headerRepeatList, footerRepeatList));
                            break;
                        }

                        for (int k = printInfo.RepeatColStart; k <= printInfo.RepeatColEnd; k++)
                        {
                            for (int l = columnPageBreakIndex - 1; l >= 0; l--)
                            {
                                if (this.showReport.getColumnOIIntList()[l] != k)
                                    continue;
                                if (headerRepeatCols_Object.Contains(k))
                                    break;
                                totalWidth += this.columnWidthList.get(l);
                                if (headerRepeatList == null)
                                    headerRepeatList = new List<int>();
                                headerRepeatList.Add(l);
                                break;
                            }
                        }

                        for (int k = printInfo.ColStart; k <= printInfo.ColEnd; k++)
                        {
                            for (int l = i + 1; l < ColumnCount; l++)
                            {
                                if (this.showReport.getColumnOIIntList()[l] != k)
                                    continue;
                                totalWidth += this.columnWidthList.get(l);
                                if (footerRepeatList == null)
                                    footerRepeatList = new List<int>();
                                footerRepeatList.Add(l);
                                footerRepeatCols_Object.Add(k);
                                break;
                            }
                        }

                        this.pageColumnIndexList.Add(new IdxHeaderFooter(columnPageBreakIndex, headerRepeatList, footerRepeatList));
                        totalWidth += this.columnWidthList.get(i);
                        if (totalWidth <= InnerWidth)
                            continue;
                        throw new ApplicationException("重复列宽超过了页面宽度。");
                    }
                }

                if (this.pageColumnIndexList.Count == 0 || ((IdxHeaderFooter)this.pageColumnIndexList[this.pageColumnIndexList.Count - 1]).getIndex() < ColumnCount)
                {
                    this.pageColumnIndexList.Add(new IdxHeaderFooter(ColumnCount));
                }

                List<int> headerRepeatRows_Object = new List<int>();
                List<int> footerRepeatRows_Object = new List<int>();
                int totalHeight = 0;
                List<int> footerRepeatRows = null;
                for (int i = printInfo.RowStart; i <= printInfo.RowEnd; i++)
                    for (int j = 0; j < RowCount; j++)
                    {
                        if (this.showReport.getRowOIIntList()[j] != i)
                            continue;
                        totalHeight += this.rowHeightList.get(j);
                        if (footerRepeatRows == null)
                            footerRepeatRows = new List<int>();
                        footerRepeatRows.Add(j);
                        footerRepeatRows_Object.Add(i);
                        break;
                    }
                if (totalHeight > InnerHeight)
                    throw new ApplicationException("重复行高超过了页面高度。");
                this.pageRowIndexList.Add(new IdxHeaderFooter(0, null, footerRepeatCols));
                for (int i = 0; i < RowCount; i++)
                {
                    if ((((List<int>)footerRepeatRows_Object).Contains(this.showReport.getRowOIIntList()[i])) && (!((List<int>)rowPageBreaks_Object).Contains(i)))
                    {
                        List<int> footerRepeatList = ((IdxHeaderFooter)this.pageRowIndexList[this.pageRowIndexList.Count - 1]).getFooterRepeatList();
                        if (footerRepeatList == null)
                            continue;
                        int Index = footerRepeatList.IndexOf(i);
                        if (Index == -1)
                            continue;
                        footerRepeatList.Remove(Index);
                    }
                    else
                    {
                        totalHeight += this.rowHeightList.get(i);
                        if ((totalHeight <= InnerHeight) && (!((List<int>)rowPageBreaks_Object).Contains(i)))
                            continue;
                        if (((IdxHeaderFooter)this.pageRowIndexList[this.pageRowIndexList.Count - 1]).getIndex() == i)
                            throw new DeathCycleException("分页处理死循环出现在行：" + i);
                        headerRepeatRows_Object.Clear();
                        footerRepeatRows_Object.Clear();
                        totalHeight = 0;
                        int rowPageBreakIndex = i;
                        List<int> headerRepeatList = null;
                        List<int> footerRepeatList = null;
                        while ((i < RowCount) && printInfo.RepeatRowStart <= this.showReport.getRowOIIntList()[i] && (this.showReport.getRowOIIntList()[i] <= printInfo.RepeatRowEnd))
                        {
                            totalHeight += this.rowHeightList.get(i);
                            headerRepeatRows_Object.Add(this.showReport.getRowOIIntList()[i]);
                            i++;
                        }
                        if (i >= RowCount)
                        {
                            if (totalHeight > InnerHeight)
                                throw new ApplicationException("重复的行高超过了页面高度。");

                            this.pageRowIndexList.Add(new IdxHeaderFooter(rowPageBreakIndex, headerRepeatList, footerRepeatList));
                            break;
                        }
                        for (int k = ((PrintInfo)printInfo).RepeatRowStart; k <= ((PrintInfo)printInfo).RepeatRowEnd; k++)
                        {
                            for (int l = rowPageBreakIndex - 1; l >= 0; l--)
                            {
                                if (this.showReport.getRowOIIntList()[l] != k)
                                    continue;
                                if (headerRepeatRows_Object.Contains(k))
                                    break;
                                totalHeight += this.rowHeightList.get(l);
                                if (headerRepeatList == null)
                                    headerRepeatList = new List<int>();
                                headerRepeatList.Add(l);
                                break;
                            }
                        }
                        for (int k = printInfo.RowStart; k <= printInfo.RowEnd; k++)
                        {
                            for (int l = i + 1; l < RowCount; l++)
                            {
                                if (this.showReport.getRowOIIntList()[l] != k)
                                    continue;
                                totalHeight += this.rowHeightList.get(l);
                                if (footerRepeatList == null)
                                    footerRepeatList = new List<int>();
                                footerRepeatList.Add(l);
                                footerRepeatRows_Object.Add(k);
                                break;
                            }
                        }
                        this.pageRowIndexList.Add(new IdxHeaderFooter(rowPageBreakIndex, headerRepeatList, footerRepeatList));
                        totalHeight += this.rowHeightList.get(i);
                        if (totalHeight <= InnerHeight)
                            continue;
                        throw new ApplicationException("重复的行高超过了页面高度。");
                    }
                }
            }

            if (this.pageRowIndexList.Count == 0 || ((IdxHeaderFooter)this.pageRowIndexList[this.pageRowIndexList.Count - 1]).getIndex() < RowCount)
            {
                this.pageRowIndexList.Add(new IdxHeaderFooter(RowCount));
            }

            this.pageCount = ((this.pageColumnIndexList.Count - 1) * (this.pageRowIndexList.Count - 1));
        }

        public Boolean hasNext()
        {
            if (this.pageGenerateThread == null)
            {
                this.pageGenerateThread = new PageGenerateThread(this);
                this.pageGenerateThread.Run();
            }

            while (true)
            {
                if (this.workSheetPageLists.Count > 0)
                    return true;

                if (this.finish)
                    break;

                Thread.Sleep(500);
            }
            return false;
        }

        public Object next()
        {
            if (this.workSheetPageLists.Count == 0)
                throw new ApplicationException("已在最后一个元素");
            Object r = this.workSheetPageLists[0];
            this.workSheetPageLists.RemoveAt(0);
            return r;
        }

        public void remove()
        {
            throw new NotImplementedException();
        }
    }

    public class PageGenerateThread
    {
        Thread thread = null;
        private PaginateReportIterator iterator;

        public PageGenerateThread(PaginateReportIterator paramPaginateReportIterator)
        {
            this.iterator = paramPaginateReportIterator;
        }

        public void Run()
        {
            //thread = new Thread(new ParameterizedThreadStart(ThreadProc));
            //thread.Priority = ThreadPriority.BelowNormal;
            //thread.SetApartmentState(ApartmentState.STA);
            //thread.Start(null);

            ThreadProc(null);
        }

        private void ThreadProc(object parameter)
        {
            int i = 0;
            int m, n;
            int prevIndex, Index;
            ArrayList ColumnFTList;

            PrintPageOrder PageOrder = this.iterator.reportSettings.PageOrder;
            if (PageOrder == PrintPageOrder.DownThenOver)
            {
                int colIndex = 1;
                int pageColumnIndexCount = this.iterator.pageColumnIndexList.Count;
                while (colIndex < pageColumnIndexCount)
                {
                    ColumnFTList = new ArrayList();

                    m = ((IdxHeaderFooter)this.iterator.pageColumnIndexList[colIndex - 1]).getIndex();
                    n = ((IdxHeaderFooter)this.iterator.pageColumnIndexList[colIndex]).getIndex();

                    if (((IdxHeaderFooter)this.iterator.pageColumnIndexList[colIndex - 1]).getHeadRepeatList() != null)
                    {
                        List<int> HeadRepeatList = ((IdxHeaderFooter)this.iterator.pageColumnIndexList[colIndex - 1]).getHeadRepeatList();
                        int count = 0;
                        int start = -1;
                        for (int k = 0; k < HeadRepeatList.Count; k++)
                        {
                            if (k == 0)
                            {
                                start = HeadRepeatList[k];
                                count++;
                            }
                            else if (HeadRepeatList[k] == start + count)
                            {
                                count++;
                            }
                            else
                            {
                                ColumnFTList.Add(new FT(start, start + count));
                                start = HeadRepeatList[k];
                                count = 1;
                            }
                        }

                        if ((start != -1) && (count > 0))
                            ColumnFTList.Add(new FT(start, start + count - 1));
                    }

                    if (n > m)
                        ColumnFTList.Add(new FT(m, (colIndex != pageColumnIndexCount - 1 ? n - 1 : n)));

                    if (((IdxHeaderFooter)this.iterator.pageColumnIndexList[colIndex - 1]).getFooterRepeatList() != null)
                    {
                        List<int> FooterRepeatList = ((IdxHeaderFooter)this.iterator.pageColumnIndexList[colIndex - 1]).getFooterRepeatList();
                        int count = 0;
                        int start = -1;
                        for (int k = 0; k < FooterRepeatList.Count; k++)
                            if (k == 0)
                            {
                                start = FooterRepeatList[k];
                                count++;
                            }
                            else if (FooterRepeatList[k] == start + count)
                            {
                                count++;
                            }
                            else
                            {
                                ColumnFTList.Add(new FT(start, start + count));
                                start = FooterRepeatList[k];
                                count = 1;
                            }
                        if ((start != -1) && (count > 0))
                            ColumnFTList.Add(new FT(start, start + count - 1));
                    }

                    int rowIndex = 1;
                    int pageRowIndexCount = this.iterator.pageRowIndexList.Count;
                    ArrayList RowFTList;
                    while (rowIndex < pageRowIndexCount)
                    {
                        prevIndex = ((IdxHeaderFooter)this.iterator.pageRowIndexList[rowIndex - 1]).getIndex();
                        Index = ((IdxHeaderFooter)this.iterator.pageRowIndexList[rowIndex]).getIndex();
                        RowFTList = new ArrayList();
                        if (((IdxHeaderFooter)this.iterator.pageRowIndexList[rowIndex - 1]).getHeadRepeatList() != null)
                        {
                            List<int> HeadRepeatList = ((IdxHeaderFooter)this.iterator.pageRowIndexList[rowIndex - 1]).getHeadRepeatList();
                            int count = 0;
                            int start = -1;
                            for (int k = 0; k < HeadRepeatList.Count; k++)
                            {
                                if (k == 0)
                                {
                                    start = HeadRepeatList[k];
                                    count++;
                                }
                                else if (HeadRepeatList[k] == start + count)
                                {
                                    count++;
                                }
                                else
                                {
                                    RowFTList.Add(new FT(start, start + count));
                                    start = HeadRepeatList[k];
                                    count = 1;
                                }
                            }

                            if ((start != -1) && (count > 0))
                                RowFTList.Add(new FT(start, start + count - 1));
                        }

                        if (Index > prevIndex)
                            RowFTList.Add(new FT(prevIndex, (rowIndex != pageRowIndexCount - 1 ? Index - 1 : Index)));

                        if (((IdxHeaderFooter)this.iterator.pageRowIndexList[rowIndex - 1]).getFooterRepeatList() != null)
                        {
                            List<int> HeadRepeatList = ((IdxHeaderFooter)this.iterator.pageRowIndexList[rowIndex - 1]).getFooterRepeatList();
                            int count = 0;
                            int start = -1;
                            for (int k = 0; k < HeadRepeatList.Count; k++)
                            {
                                if (k == 0)
                                {
                                    start = HeadRepeatList[k];
                                    count++;
                                }
                                else if (HeadRepeatList[k] == start + count)
                                {
                                    count++;
                                }
                                else
                                {
                                    RowFTList.Add(new FT(start, start + count));
                                    start = HeadRepeatList[k];
                                    count = 1;
                                }
                            }

                            if ((start != -1) && (count > 0))
                                RowFTList.Add(new FT(start, start + count - 1));
                        }

                        dealWithReportPage(RowFTList, ColumnFTList, i,rowIndex,colIndex);
                        i++;
                        rowIndex++;
                    }
                    colIndex++;
                }
            }
            else if (PageOrder == PrintPageOrder.OverThenDown)
            {
                int rowIndex1 = 1;
                int pageRowIndexCount1 = this.iterator.pageRowIndexList.Count;
                ArrayList RowFTList1;
                while (rowIndex1 < pageRowIndexCount1)
                {
                    m = ((IdxHeaderFooter)this.iterator.pageRowIndexList[rowIndex1 - 1]).getIndex();
                    n = ((IdxHeaderFooter)this.iterator.pageRowIndexList[rowIndex1]).getIndex();
                    RowFTList1 = new ArrayList();
                    if (((IdxHeaderFooter)this.iterator.pageRowIndexList[rowIndex1 - 1]).getHeadRepeatList() != null)
                    {
                        List<int> HeadRepeatList = ((IdxHeaderFooter)this.iterator.pageRowIndexList[rowIndex1 - 1]).getHeadRepeatList();
                        int count = 0;
                        int start = -1;
                        for (int k = 0; k < HeadRepeatList.Count; k++)
                        {
                            if (k == 0)
                            {
                                start = HeadRepeatList[k];
                                count++;
                            }
                            else if (HeadRepeatList[k] == start + count)
                            {
                                count++;
                            }
                            else
                            {
                                RowFTList1.Add(new FT(start, start + count));
                                start = HeadRepeatList[k];
                                count = 1;
                            }
                        }

                        if ((start != -1) && (count > 0))
                            RowFTList1.Add(new FT(start, start + count));
                    }
                    if (n > m)
                        RowFTList1.Add(new FT(m, n));

                    if (((IdxHeaderFooter)this.iterator.pageRowIndexList[rowIndex1 - 1]).getFooterRepeatList() != null)
                    {
                        List<int> FooterRepeatList = ((IdxHeaderFooter)this.iterator.pageRowIndexList[rowIndex1 - 1]).getFooterRepeatList();
                        int count = 0;
                        int start = -1;
                        for (int k = 0; k < FooterRepeatList.Count; k++)
                        {
                            if (k == 0)
                            {
                                start = FooterRepeatList[k];
                                count++;
                            }
                            else if (FooterRepeatList[k] == start + count)
                            {
                                count++;
                            }
                            else
                            {
                                RowFTList1.Add(new FT(start, start + count));
                                start = FooterRepeatList[k];
                                count = 1;
                            }
                        }

                        if ((start != -1) && (count > 0))
                            RowFTList1.Add(new FT(start, start + count));
                    }

                    int colIndex1 = 1;
                    int pageColumnIndexCount1 = this.iterator.pageColumnIndexList.Count;
                    ArrayList ColumnFTList1;
                    while (colIndex1 < pageColumnIndexCount1)
                    {
                        prevIndex = ((IdxHeaderFooter)this.iterator.pageColumnIndexList[colIndex1 - 1]).getIndex();
                        Index = ((IdxHeaderFooter)this.iterator.pageColumnIndexList[colIndex1]).getIndex();
                        ColumnFTList1 = new ArrayList();
                        if (((IdxHeaderFooter)this.iterator.pageColumnIndexList[colIndex1 - 1]).getHeadRepeatList() != null)
                        {
                            List<int> HeadRepeatList = ((IdxHeaderFooter)this.iterator.pageColumnIndexList[colIndex1 - 1]).getHeadRepeatList();
                            int count = 0;
                            int start = -1;
                            for (int k = 0; k < HeadRepeatList.Count; k++)
                            {
                                if (k == 0)
                                {
                                    start = HeadRepeatList[k];
                                    count++;
                                }
                                else if (HeadRepeatList[k] == start + count)
                                {
                                    count++;
                                }
                                else
                                {
                                    ColumnFTList1.Add(new FT(start, start + count));
                                    start = HeadRepeatList[k];
                                    count = 1;
                                }
                            }

                            if ((start != -1) && (count > 0))
                                ColumnFTList1.Add(new FT(start, start + count));
                        }

                        if (Index > prevIndex)
                            ColumnFTList1.Add(new FT(prevIndex, Index));

                        if (((IdxHeaderFooter)this.iterator.pageColumnIndexList[colIndex1 - 1]).getFooterRepeatList() != null)
                        {
                            List<int> FooterRepeatList = ((IdxHeaderFooter)this.iterator.pageColumnIndexList[colIndex1 - 1]).getFooterRepeatList();
                            int count = 0;
                            int start = -1;
                            for (int k = 0; k < FooterRepeatList.Count; k++)
                            {
                                if (k == 0)
                                {
                                    start = FooterRepeatList[k];
                                    count++;
                                }
                                else if (FooterRepeatList[k] == start + count)
                                {
                                    count++;
                                }
                                else
                                {
                                    ColumnFTList1.Add(new FT(start, start + count));
                                    start = FooterRepeatList[k];
                                    count = 1;
                                }
                            }

                            if ((start != -1) && (count > 0))
                                ColumnFTList1.Add(new FT(start, start + count));
                        }

                        dealWithReportPage(RowFTList1, ColumnFTList1, i, rowIndex1, colIndex1);
                        i++;
                        colIndex1++;
                    }
                    rowIndex1++;
                }
            }

            iterator.finish = true;
        }

        private void dealWithReportPage(ArrayList RowFTList, ArrayList ColumnFTList, int paramInt,int rowIndex,int columnIndex)
        {
            ReportPage localReportPage = new ReportPage(this.iterator.showReport, (FT[])RowFTList.ToArray(typeof(FT)), (FT[])ColumnFTList.ToArray(typeof(FT)));
            int i = 1;
            int j = paramInt + i;
            if ((this.iterator.pageNumberArray != null) && (this.iterator.pageNumberArray.Length > paramInt))
                j = this.iterator.pageNumberArray[paramInt];
            int k = this.iterator.pageCount;
            if ((this.iterator.totalPageNumberArray != null) && (this.iterator.totalPageNumberArray.Length > paramInt))
                k = this.iterator.totalPageNumberArray[paramInt];
            localReportPage.setPageRowIndex(rowIndex);
            localReportPage.setPageColumnIndex(columnIndex);
            localReportPage.setCurrentPageNumber(j);
            localReportPage.setTotalPages(k);
            this.iterator.workSheetPageLists.Add(localReportPage);
        }
    }

    internal class IdxHeaderFooter
    {
        private int idx;
        private List<int> headerRepeatList;
        private List<int> footerRepeatList;

        public IdxHeaderFooter(int paramInt) :
            this(paramInt, null, null)
        {
        }

        public IdxHeaderFooter(int paramInt, List<int> paramIntList1, List<int> paramIntList2)
        {
            this.idx = paramInt;
            this.headerRepeatList = paramIntList1;
            this.footerRepeatList = paramIntList2;
        }

        public int getIndex()
        {
            return this.idx;
        }

        public List<int> getHeadRepeatList()
        {
            return this.headerRepeatList;
        }

        public List<int> getFooterRepeatList()
        {
            return this.footerRepeatList;
        }
    }
}
