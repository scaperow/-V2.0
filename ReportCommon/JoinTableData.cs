using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.Serialization;

namespace ReportCommon
{
    /// <summary>
    /// 联接数据表
    /// </summary>
    [Serializable]
    public class JoinTableData : AbstractTableData
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public JoinTableData()
        {
        }

        protected JoinTableData(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        public string GetQueryCommand()
        {
            return SQLCommandBuilder.GetCommand(GetSchema());
        }

        public override DataTable GetDataSource()
        {
            String sqlCommand = GetQueryCommand();
            DataTable Data = DataAdapter.ExecuteCommand(sqlCommand);
            Data.DefaultView.RowFilter = DataFilter.ToString();
            DataTable Table = Data.DefaultView.ToTable();
            Table.TableName = GetTableName();

            return Table;
        }
    }
}
