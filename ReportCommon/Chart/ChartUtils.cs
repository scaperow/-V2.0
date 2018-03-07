using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace ReportCommon.Chart
{
    public class ChartUtils
    {
        public static string[] convertStringToColumnRow(String paramString)
        {
            if ((paramString == null) || (paramString.Trim().Length == 0))
                return new string[0];
            List<string> localArrayList = new List<string>();
            String[] arrayOfString = paramString.Trim().Split(',');
            if ((arrayOfString != null) && (arrayOfString.Length > 0))
                for (int i = 0; i < arrayOfString.Length; i++)
                    dealStringWithOutComma(localArrayList, arrayOfString[i]);
            return localArrayList.ToArray();
        }

        private static void dealStringWithOutComma(List<string> paramList, String paramString)
        {
            int i = -1, j = -1, k = -1, m = -1, n = -1;
            int[] localColumnRow;

            String[] arrayOfString = paramString.Trim().Split(':');
            if ((arrayOfString != null) && (arrayOfString.Length > 1))
            {
                i = -1;
                j = -1;
                k = -1;
                m = -1;

                for (n = 0; n < arrayOfString.Length; n++)
                {
                    localColumnRow = Coords.ConvertColumn_Row(arrayOfString[n].Trim());
                    if (i == -1)
                    {
                        i = localColumnRow[0];
                        j = localColumnRow[0];
                        k = localColumnRow[1];
                        m = localColumnRow[1];
                    }
                    else
                    {
                        i = Math.Min(i, localColumnRow[0]);
                        j = Math.Max(j, localColumnRow[0]);
                        k = Math.Min(k, localColumnRow[1]);
                        m = Math.Max(m, localColumnRow[1]);
                    }
                }

                n = k;
            }

            String strColumnRow;
            while (n <= m)
            {
                for (int i1 = i; i1 <= j; i1++)
                {
                    strColumnRow = Coords.GetColumn_Row(n, i1);
                    if (paramList.Contains(strColumnRow))
                        continue;
                    paramList.Add(strColumnRow);
                }
                n++;
            }
        }
    }
}
