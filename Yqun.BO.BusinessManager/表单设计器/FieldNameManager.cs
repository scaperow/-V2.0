using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Yqun.BO.BusinessManager
{
    public class FieldNameManager : BOBase
    {
        public Boolean HasFieldName(string TableName,string FieldName)
        {
            StringBuilder sql_Select = new StringBuilder();
            sql_Select.Append("select id from sys_columns where tablename='");
            sql_Select.Append(TableName);
            sql_Select.Append("' and colname='");
            sql_Select.Append(FieldName);
            sql_Select.Append("'");

            DataTable Data = GetDataTable(sql_Select.ToString());
            return (Data != null && Data.Rows.Count > 0);
        }
    }
}
