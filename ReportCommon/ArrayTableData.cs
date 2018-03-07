using System;
using System.Runtime.Serialization;
using System.Data;

namespace ReportCommon
{
    /// <summary>
    /// 内置数据集
    /// </summary>
    [Serializable]
    public class ArrayTableData : AbstractTableData
    {
        public ArrayTableData()
        {
        }

        protected ArrayTableData(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        public override System.Data.DataTable GetDataSource()
        {
            DataTable Schema = GetSchema();
            Schema.DefaultView.RowFilter = DataFilter.ToString();
            DataTable Table = Schema.DefaultView.ToTable();
            Table.TableName = GetTableName();
            return Table;
        }
    }
}
