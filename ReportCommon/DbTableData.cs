using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.Serialization;

namespace ReportCommon
{
    /// <summary>
    /// 数据库中的数据表
    /// </summary>
    [Serializable]
    public class DbTableData : AbstractTableData
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        String _sqlCommand = string.Empty;

        public DbTableData()
        {
        }

        protected DbTableData(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _sqlCommand = info.GetString("sqlCommand");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("sqlCommand", _sqlCommand);
        }

        public string GetQueryCommand()
        {
            return _sqlCommand;
        }

        public string SetQueryCommand(String Command)
        {
            return _sqlCommand = Command;
        }

        public override DataTable GetDataSource()
        {
            DataTable Data = DataAdapter.ExecuteCommand(_sqlCommand);
            Data.DefaultView.RowFilter = DataFilter.ToString();
            DataTable Table = Data.DefaultView.ToTable();
            Table.TableName = GetTableName();

            return Table;
        }
    }
}
