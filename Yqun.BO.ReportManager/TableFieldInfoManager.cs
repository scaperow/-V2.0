using System;
using System.Collections.Generic;
using System.Text;
using ReportCommon;
using System.Data;
using BizCommon;

namespace Yqun.BO.ReportManager
{
    public class FieldInfoManager : BOBase
    {
        public List<string> GetTables()
        {
            List<string> Result = new List<string>();
            //增加查询条件 Scdel=0  判断数据是否为已删除    2013-10-17
            StringBuilder sql_Reporttables = new StringBuilder();
            sql_Reporttables.Append("select TableName from sys_biz_Reporttables where Scdel=0");
            DataTable Reporttables = GetDataTable(sql_Reporttables.ToString());
            foreach (DataRow Row in Reporttables.Rows)
            {
                String TableName = Row["TableName"].ToString();
                Result.Add(TableName);
            }

            return Result;
        }

        public DataTable GetFields(String[] Tables)
        {
            DataTable Schema = new DataTable();
            Schema.Columns.Add("TableName", typeof(string));
            Schema.Columns.Add("ColumnName", typeof(string));

            List<string> sql_Commands = new List<string>();
            foreach (String str in Tables)
            {
                sql_Commands.Add(string.Format("select * from {0}", str));
            }

            DataSet DataSchemas = GetDataSet(sql_Commands.ToArray());
            foreach (DataTable Data in DataSchemas.Tables)
            {
                foreach (System.Data.DataColumn DataColumn in Data.Columns)
                {
                    DataRow Row = Schema.NewRow();
                    Row["TableName"] = Data.TableName;
                    Row["ColumnName"] = DataColumn.ColumnName;
                    Schema.Rows.Add(Row);
                }
            }

            return Schema;
        }

        public Boolean AnalysisSQLCommand(String command)
        {
            object r = ExcuteCommand(command);
            return (Convert.ToInt32(r) == 1);
        }

        public List<string> GetFields(String command)
        {
            List<string> DataColumns = new List<string>();

            DataTable Data = GetDataTable(command);
            foreach (System.Data.DataColumn DataColumn in Data.Columns)
            {
                DataColumns.Add(DataColumn.ColumnName);
            }

            return DataColumns;
        }
    }
}
