using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;

namespace ReportCommon
{
    public abstract class AbstractTableData : TableData
    {
        private String _TableName = String.Empty;
        private String _TableText = String.Empty;
        private DataTable _TableSchema = new DataTable("TableSchema");

        public AbstractTableData()
        {
        }

        //反序列化
        protected AbstractTableData(SerializationInfo info, StreamingContext context)
        {
            _TableName = info.GetString("TableName");
            _TableText = info.GetString("TableText");
            _TableSchema = info.GetValue("TableSchema", typeof(DataTable)) as DataTable;
            _DataFilter = info.GetValue("DataFilter", typeof(CombineFilterCondition)) as CombineFilterCondition;
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("TableName", _TableName);
            info.AddValue("TableText", _TableText);
            info.AddValue("TableSchema", _TableSchema);
            info.AddValue("DataFilter", _DataFilter);
        }

        IDataSourceAdapter _DataAdapter;
        public IDataSourceAdapter DataAdapter
        {
            get
            {
                return _DataAdapter;
            }
            set
            {
                _DataAdapter = value;
            }
        }

        CombineFilterCondition _DataFilter = new CombineFilterCondition();
        public CombineFilterCondition DataFilter
        {
            get
            {
                return _DataFilter;
            }
        }

        public string GetTableName()
        {
            return _TableName;
        }

        public void SetTableName(string TableName)
        {
            _TableName = TableName;
            _TableSchema.TableName = _TableName;
        }

        public string GetTableText()
        {
            return _TableText;
        }

        public void SetTableText(string TableText)
        {
            _TableText = TableText;
        }

        public void ClearColumns()
        {
            _TableSchema.Columns.Clear();
        }

        public Boolean HaveColumn(string Name)
        {
            return _TableSchema.Columns.Contains(Name);
        }

        public int AddStringColumn(string Name)
        {
            return AddColumn(Name, typeof(string));
        }

        public int AddIntegerColumn(string Name)
        {
            return AddColumn(Name, typeof(long));
        }

        public int AddFloatColumn(string Name)
        {
            return AddColumn(Name, typeof(double));
        }

        public int AddDateColumn(string Name)
        {
            return AddColumn(Name, typeof(DateTime));
        }

        public int AddColumn(string Name, Type DataType)
        {
            if (!HaveColumn(Name))
            {
                System.Data.DataColumn col = new System.Data.DataColumn(Name);
                col.DataType = DataType;

                _TableSchema.Columns.Add(col);
            }

            return _TableSchema.Columns.Count - 1;
        }

        public void DeleteColumn(string Name)
        {
            _TableSchema.Columns.Remove(Name);
        }

        public void EditColumn(string Name, string NewName)
        {
            int Index = _TableSchema.Columns.IndexOf(Name);
            if (Index != -1)
            {
                System.Data.DataColumn col = _TableSchema.Columns[Index];
                col.ColumnName = NewName;
            }
        }

        public Type GetColumnType(int colIndex)
        {
            if (colIndex >= 0 && colIndex < _TableSchema.Columns.Count)
                return _TableSchema.Columns[colIndex].DataType;
            else
                throw new ArgumentOutOfRangeException();
        }

        public string GetColumnName(int colIndex)
        {
            if (colIndex >= 0 && colIndex < _TableSchema.Columns.Count)
                return _TableSchema.Columns[colIndex].ColumnName;
            else
                throw new ArgumentOutOfRangeException();
        }

        public string GetColumnText(int colIndex)
        {
            if (colIndex >= 0 && colIndex < _TableSchema.Columns.Count)
                return _TableSchema.Columns[colIndex].Caption;
            else
                throw new ArgumentOutOfRangeException();
        }

        public void SetColumnText(int colIndex, string caption)
        {
            if (colIndex >= 0 && colIndex < _TableSchema.Columns.Count)
                _TableSchema.Columns[colIndex].Caption = caption;
        }

        public string GetDataTypeAbbr(int colIndex)
        {
            if (colIndex >= 0 && colIndex < _TableSchema.Columns.Count)
            {
                Type DataType = _TableSchema.Columns[colIndex].DataType;
                if (DataType == typeof(string))
                    return "S";
                else if (DataType == typeof(long))
                    return "I";
                else if (DataType == typeof(double))
                    return "F";
                else if (DataType == typeof(DateTime))
                    return "D";
            }

            throw new ArgumentOutOfRangeException();
        }

        public int GetColumnCount()
        {
            return _TableSchema.Columns.Count;
        }

        public void ClearRows()
        {
            _TableSchema.Rows.Clear();
        }

        public DataRow AddRow()
        {
            DataRow NewRow = _TableSchema.NewRow();
            _TableSchema.Rows.Add(NewRow);
            return NewRow;
        }

        public DataRow AddRow(int Index)
        {
            DataRow NewRow = _TableSchema.NewRow();
            if (Index < _TableSchema.Rows.Count)
            {
                _TableSchema.Rows.InsertAt(NewRow, Index);
            }
            else
            {
                _TableSchema.Rows.Add(NewRow);
            }
            return NewRow;
        }

        public void RemoveRow(DataRow Row)
        {
            try
            {
                _TableSchema.Rows.Remove(Row);
            }
            catch
            {
            }
        }

        public void RemoveRow(int Index)
        {
            try
            {
                _TableSchema.Rows.RemoveAt(Index);
            }
            catch
            {
            }
        }

        public DataTable GetSchema()
        {
            return _TableSchema;
        }

        public abstract System.Data.DataTable GetDataSource();

        public override string ToString()
        {
            return GetTableName();
        }
    }
}
