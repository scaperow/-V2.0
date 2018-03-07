using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.Win.Spread;
using System.Drawing;

namespace Yqun.BO.ReportE.Core
{
    public class PageReport
    {
        SheetView report;
        PrintInfo reportSettings = new PrintInfo();
        List<int> oiRowIntList = new List<int>();
        List<int> oiColumnIntList = new List<int>();
        int resolution = 100;

        public PageReport(SheetView Report)
        {
            this.report = Report;
        }

        public List<int> getRowOIIntList()
        {
            return this.oiRowIntList;
        }

        public List<int> getColumnOIIntList()
        {
            return this.oiColumnIntList;
        }

        public PaginateReportIterator generateReportPageIterator()
        {
            PaginateReportIterator localPaginateReportIterator = new PaginateReportIterator(this);
            return localPaginateReportIterator;
        }

        public PrintInfo getReportSettings()
        {
            reportSettings.CopyFrom(this.report.PrintInfo);

            reportSettings.RepeatColStart = -1;
            reportSettings.RepeatColEnd = -1;
            reportSettings.RepeatRowStart = -1;
            reportSettings.RepeatRowEnd = -1;

            reportSettings.ColStart = -1;
            reportSettings.ColEnd = -1;
            reportSettings.RowStart = -1;
            reportSettings.RowEnd = -1; 
            
            return reportSettings;
        }

        public SheetView ToWorkSheet()
        {
            return this.report;
        }

        public int getResolution()
        {
            return this.resolution;
        }

        public void setResolution(int paramInt)
        {
            this.resolution = paramInt;
        }
    }
}
