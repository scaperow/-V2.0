using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using System.Runtime.Serialization;

namespace ReportCommon
{
    /// <summary>
    /// 数据源接口
    /// </summary>
    public interface TableData : ISerializable
    {
        IDataSourceAdapter DataAdapter
        {
            get;
            set;
        }

        CombineFilterCondition DataFilter
        {
            get;
        }

        String GetTableName();
        String GetTableText();
        Type GetColumnType(int colIndex);
        String GetColumnName(int colIndex);
        String GetColumnText(int colIndex);
        int GetColumnCount();
        DataTable GetSchema();

        DataTable GetDataSource();
    }
}
