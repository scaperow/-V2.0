using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Yqun.BO.BusinessManager
{
    public class TableNameManager : BOBase
    {
        public Boolean HasTableName(string TableName)
        {
            StringBuilder sql_Select = new StringBuilder();
            //增加查询条件Scdel=0     2013-10-19
            sql_Select.Append("select id from sys_tables where tablename='");
            sql_Select.Append(TableName);
            sql_Select.Append("'");

            DataTable Data = GetDataTable(sql_Select.ToString());
            return (Data != null && Data.Rows.Count > 0);
        }

        public String GetTableName(string TableName)
        {
            string tempTableName = TableName;
            int Index = 1;

            while (HasTableName(tempTableName))
            {
                tempTableName = TableName + "_" + (Index++).ToString();
            }

            return tempTableName;
        }
    }
}
