using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace BizComponents
{
    /// <summary>
    /// 字段类型默认值
    /// </summary>
    public class DefaultValue
    {
        public static DataRow InitDataRow(DataRow r)
        {
            for (int i = 0; i < r.Table.Columns.Count; i++)
            {
                if (r.Table.Columns[i].DataType == typeof(int) ||
                    r.Table.Columns[i].DataType == typeof(short) ||
                    r.Table.Columns[i].DataType == typeof(long) ||
                    r.Table.Columns[i].DataType == typeof(decimal)
                    )
                {
                    r[i] = 0;
                }

                if (r.Table.Columns[i].DataType == typeof(byte[]))
                {
                    r[i] = DBNull.Value;
                }

                if (r.Table.Columns[i].DataType == typeof(DateTime))
                {
                    r[i] = DateTime.Now;
                }
            }

            return r;
        }
    }
}
