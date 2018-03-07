using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.Win.Spread;
using System.Drawing;
using FarPoint.Win;

namespace ReportCommon
{
    public class ReportHelper
    {
        public static Point[] calculateLastColumnAndRowOfFloatElement(SheetView paramReport, FloatElement paramFloatElement)
        {
            int column = paramFloatElement.getColumn();
            int row = paramFloatElement.getRow();
            int leftdistance = paramFloatElement.getLeftDistance();
            int topdistance = paramFloatElement.getTopDistance();
            Size localSize = paramFloatElement.getSize();
            DynamicValueList columnWidthList = new DynamicValueList(72, paramReport.Columns.Count);
            for (int j = 0; j < paramReport.Columns.Count; j++)
                columnWidthList.set(j, Convert.ToInt32(paramReport.Columns[j].Width));
            DynamicValueList rowHeightList = new DynamicValueList(19, paramReport.Rows.Count);
            for (int j = 0; j < paramReport.Rows.Count; j++)
                rowHeightList.set(j, Convert.ToInt32(paramReport.Rows[j].Height));

            int n = column;
            int i1 = row;
            int i2 = 0;
            int i3 = 0;
            int i4 = -leftdistance;

            for (int i5 = column; ; i5++)
            {
                i4 += columnWidthList.get(i5);
                if (i4 < localSize.Width)
                    continue;
                n = i5;
                i2 = localSize.Width - (i4 - columnWidthList.get(i5));
                break;
            }

            i4 = -topdistance;
            for (int i5 = row; ; i5++)
            {
                i4 += rowHeightList.get(i5);
                if (i4 < localSize.Height)
                    continue;
                i1 = i5;
                i3 = localSize.Height - (i4 - rowHeightList.get(i5));
                break;
            }
            return new Point[] { new Point(n, i1), new Point(i2, i3) };
        }

        public static DynamicValueList getRowHeightList(SheetView paramReport)
        {
            int cCount = paramReport.GetLastNonEmptyColumn(NonEmptyItemFlag.Data) + 1;
            int rCount = paramReport.GetLastNonEmptyRow(NonEmptyItemFlag.Data) + 1;

            for (int m = 0; m < rCount; m++)
            {
                for (int n = 0; n < cCount; n++)
                {
                    if (paramReport.Cells[m, n].Tag is GridElement)
                    {
                        cCount = Math.Max(cCount, paramReport.Cells[m, n].Column.Index + paramReport.Cells[m, n].ColumnSpan);
                        rCount = Math.Max(rCount, paramReport.Cells[m, n].Row.Index + paramReport.Cells[m, n].RowSpan);
                    }
                }
            }

            foreach (IElement Element in paramReport.DrawingContainer.ContainedObjects)
            {
                if (Element is FloatElement)
                {
                    FloatElement localFloatElement = Element as FloatElement;
                    Point[] arrayOfPoint = ReportHelper.calculateLastColumnAndRowOfFloatElement(paramReport, localFloatElement);
                    cCount = Math.Max(cCount, arrayOfPoint[0].X);
                    rCount = Math.Max(rCount, arrayOfPoint[0].Y);
                }
            }

            DynamicValueList localDynamicValueList = new DynamicValueList(19, rCount);
            for (int j = 0; j < rCount; j++)
                localDynamicValueList.set(j, Convert.ToInt32(paramReport.Rows[j].Height));
            return localDynamicValueList;
        }

        public static DynamicValueList getColumnWidthList(SheetView paramReport)
        {
            int cCount = paramReport.GetLastNonEmptyColumn(NonEmptyItemFlag.Data) + 1;
            int rCount = paramReport.GetLastNonEmptyRow(NonEmptyItemFlag.Data) + 1;

            for (int m = 0; m < rCount; m++)
            {
                for (int n = 0; n < cCount; n++)
                {
                    if (paramReport.Cells[m, n].Tag is GridElement)
                    {
                        cCount = Math.Max(cCount, paramReport.Cells[m, n].Column.Index + paramReport.Cells[m, n].ColumnSpan);
                        rCount = Math.Max(rCount, paramReport.Cells[m, n].Row.Index + paramReport.Cells[m, n].RowSpan);
                    }
                }
            }

            foreach (IElement Element in paramReport.DrawingContainer.ContainedObjects)
            {
                if (Element is FloatElement)
                {
                    FloatElement localFloatElement = Element as FloatElement;
                    Point[] arrayOfPoint = ReportHelper.calculateLastColumnAndRowOfFloatElement(paramReport, localFloatElement);
                    cCount = Math.Max(cCount, arrayOfPoint[0].X);
                    rCount = Math.Max(rCount, arrayOfPoint[0].Y);
                }
            }

            DynamicValueList localDynamicValueList = new DynamicValueList(72, cCount);
            for (int j = 0; j < cCount; j++)
                localDynamicValueList.set(j, Convert.ToInt32(paramReport.Columns[j].Width));
            return localDynamicValueList;
        }
    }

    public class DynamicValueList
    {
        private int[] intArray;
        private int defaultValue = 1;

        public DynamicValueList(int paramInt) :
            this(paramInt, 10)
        {
        }

        public DynamicValueList(int paramInt, int[] paramArrayOfInt)
        {
            this.defaultValue = paramInt;
            this.intArray = paramArrayOfInt;
        }

        public DynamicValueList(int paramInt1, int paramInt2)
        {
            if (paramInt2 < 0)
                throw new ArgumentException("Illegal Capacity: " + paramInt2);
            this.defaultValue = paramInt1;
            this.intArray = createIntArray(paramInt2);
        }

        public void trimToSize()
        {
            int i;
            for (i = this.intArray.Length - 1; (i >= 0) && (this.intArray[i] == this.defaultValue); i--) ;
            if (i < this.intArray.Length - 1)
            {
                int[] arrayOfInt = this.intArray;
                this.intArray = createIntArray(i + 1);
                Array.Copy(arrayOfInt, 0, this.intArray, 0, this.intArray.Length);
            }
        }

        public int getDefaultValue()
        {
            return this.defaultValue;
        }

        public void setDefaultValue(int paramInt)
        {
            this.defaultValue = paramInt;
        }

        public int getLastIndex()
        {
            trimToSize();
            return this.intArray.Length - 1;
        }

        public int get(int paramInt)
        {
            if ((paramInt < 0) || (paramInt > this.intArray.Length - 1))
                return this.defaultValue;
            return this.intArray[paramInt];
        }

        public void set(int paramInt1, int paramInt2)
        {
            if (paramInt1 < 0)
                return;
            if ((paramInt1 >= this.intArray.Length) && (paramInt2 == this.defaultValue))
                return;
            checkIntArrayBounds(paramInt1);
            this.intArray[paramInt1] = paramInt2;
        }

        public void insert(int paramInt)
        {
            insert(paramInt, 1);
        }

        public void insert(int paramInt1, int paramInt2)
        {
            if ((paramInt1 < 0) || (paramInt2 <= 0))
                return;
            checkIntArrayBounds(paramInt1);
            int[] arrayOfInt = this.intArray;
            this.intArray = new int[arrayOfInt.Length + paramInt2];

            for (int i = 0; i < paramInt2; i++)
            {
                this.intArray[paramInt1 + i] = this.defaultValue;
            }

            Array.Copy(arrayOfInt, 0, this.intArray, 0, paramInt1);
            Array.Copy(arrayOfInt, paramInt1, this.intArray, paramInt1 + paramInt2, arrayOfInt.Length - paramInt1);
        }

        public void remove(int paramInt)
        {
            if ((paramInt < 0) || (paramInt > this.intArray.Length - 1))
                return;
            int[] arrayOfInt = this.intArray;
            this.intArray = new int[this.intArray.Length - 1];
            Array.Copy(arrayOfInt, 0, this.intArray, 0, paramInt);
            Array.Copy(arrayOfInt, paramInt + 1, this.intArray, paramInt, arrayOfInt.Length - (paramInt + 1));
        }

        public void removeRange(int paramInt1, int paramInt2)
        {
            if ((paramInt1 < 0) || (paramInt2 < 0) || (paramInt1 >= this.intArray.Length) || (paramInt2 >= this.intArray.Length) || (paramInt1 >= paramInt2))
                return;
            int[] arrayOfInt = this.intArray;
            this.intArray = new int[arrayOfInt.Length - (paramInt2 - paramInt1 + 1)];
            Array.Copy(arrayOfInt, 0, this.intArray, 0, paramInt1);
            Array.Copy(arrayOfInt, paramInt2 + 1, this.intArray, paramInt1, arrayOfInt.Length - (paramInt2 + 1));
        }

        public void reset()
        {
            for (int i = 0; i < this.intArray.Length; i++)
                this.intArray[i] = this.defaultValue;
        }

        public int getRangeValueFromZero(int paramInt)
        {
            return getRangeValue(0, paramInt);
        }

        public int getRangeValue(int paramInt1, int paramInt2)
        {
            int i = 0;
            int j = Math.Min(paramInt1, paramInt2);
            int k = Math.Max(paramInt1, paramInt2);
            j = Math.Max(0, j);
            int m = j;
            int n = Math.Min(k, this.intArray.Length);
            while (m < n)
            {
                i += this.intArray[m];
                m++;
            }
            if (k > this.intArray.Length)
                i += (k - Math.Max(this.intArray.Length, j)) * this.defaultValue;
            return paramInt1 <= paramInt2 ? i : -i;
        }

        public int getValueIndex(int paramInt)
        {
            return getValueIndex(paramInt, 0);
        }

        public int getValueIndex(int paramInt1, int paramInt2)
        {
            int i = 0;
            for (int j = paramInt2; ; j++)
            {
                i += get(j);
                if (i > paramInt1)
                    return j;
            }
        }

        private void checkIntArrayBounds(int paramInt)
        {
            while (paramInt >= this.intArray.Length)
                duplicateIntArray();
        }

        private int[] createIntArray(int paramInt)
        {
            int[] arrayOfInt = new int[paramInt];
            for (int i = 0; i < arrayOfInt.Length; i++)
                arrayOfInt[i] = this.defaultValue;
            return arrayOfInt;
        }

        private void duplicateIntArray()
        {
            int[] arrayOfInt = this.intArray;
            this.intArray = new int[Math.Max(10, arrayOfInt.Length * 2)];
            Array.Copy(arrayOfInt, 0, this.intArray, 0, arrayOfInt.Length);

            for (int i = 0; i < this.intArray.Length; i++)
            {
                this.intArray[arrayOfInt.Length + i] = this.defaultValue;
            }
        }
    }
}
