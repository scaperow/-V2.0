using System.Collections.Generic;
using FarPoint.Win.Spread;

namespace Yqun.BO.ReportE.Core
{
    public class ReportPage
    {
        private PageReport source_report;
        private FT[] column_line_array;
        private FT[] row_line_array;
        private int currentPageNumber;
        private int totalPages;
        private int pageRowIndex;
        private int pageColumnIndex;

        public ReportPage(PageReport paramReport, FT[] paramArrayOfFT1, FT[] paramArrayOfFT2)
        {
            this.source_report = paramReport;
            this.row_line_array = paramArrayOfFT1;
            this.column_line_array = paramArrayOfFT2;
        }

        public SheetView getSource_Report()
        {
            return this.source_report.ToWorkSheet();
        }

        public FT[] getColumn_Line_Array()
        {
            return this.column_line_array;
        }

        public FT[] getRow_Line_Array()
        {
            return this.row_line_array;
        }

        public float[] getColumnWidthArray()
        {
            List<float> localIntList = new List<float>();
            for (int i = 0; i < this.column_line_array.Length; i++)
            {
                FT localFT = this.column_line_array[i];
                for (int j = localFT.from; j <= localFT.to; j++)
                    localIntList.Add(this.source_report.ToWorkSheet().Columns[j].Width);
            }
            return localIntList.ToArray();
        }

        public float[] getRowHeightArray()
        {
            List<float> localIntList = new List<float>();
            for (int i = 0; i < this.row_line_array.Length; i++)
            {
                FT localFT = this.row_line_array[i];
                for (int j = localFT.from; j <= localFT.to; j++)
                    localIntList.Add(this.source_report.ToWorkSheet().Rows[j].Height);
            }
            return localIntList.ToArray();
        }

        public void setCurrentPageNumber(int paramInt)
        {
            this.currentPageNumber = paramInt;
        }

        public int getCurrentPageNumber()
        {
            return this.currentPageNumber;
        }

        public void setTotalPages(int paramInt)
        {
            this.totalPages = paramInt;
        }

        public int getTotalPages()
        {
            return this.totalPages;
        }

        public int getPageRowIndex()
        {
            return this.pageRowIndex;
        }

        public void setPageRowIndex(int paramInt)
        {
            this.pageRowIndex = paramInt;
        }

        public int getPageColumnIndex()
        {
            return this.pageColumnIndex;
        }

        public void setPageColumnIndex(int paramInt)
        {
            this.pageColumnIndex = paramInt;
        }
    }
}
