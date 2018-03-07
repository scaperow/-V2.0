using System;
using System.Collections.Generic;
using System.Text;

namespace Yqun.BO.BusinessManager
{
    public class DataSchemaManager : BOBase
    {
        DataTableManager DataTableManager = new DataTableManager();

        public Boolean UpdateTableStruct(string TableName)
        {
            object o = DataTableManager.CreateDataTable(TableName);
            return Convert.ToBoolean(o);
        }

        public Boolean DeleteTableStruct(string TableName)
        {
            object o = DataTableManager.DeleteDataTable(TableName);
            return Convert.ToBoolean(o);
        }

        public Boolean AppendColumnStruct(string TableName, string[] ColumnName)
        {
            object o = DataTableManager.AppendDataColumn(TableName, ColumnName);
            return Convert.ToBoolean(o);
        }

        public Boolean UpdateDataColumn(string TableName, string[] ColumnName)
        {
            object o = DataTableManager.UpdateDataColumn(TableName, ColumnName);
            return Convert.ToBoolean(o);
        }

        public Boolean DeleteDataColumn(string TableName, string[] ColumnName)
        {
            object o = DataTableManager.DeleteDataColumn(TableName, ColumnName);
            return Convert.ToBoolean(o);
        }

        public Boolean HasData(string TableName)
        {
            object o = DataTableManager.HasData(TableName);
            return Convert.ToBoolean(o);
        }
    }
}
