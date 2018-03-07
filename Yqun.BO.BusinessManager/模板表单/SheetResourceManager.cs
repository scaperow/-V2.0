using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Yqun.BO.BusinessManager
{
    public class SheetResourceManager : BOBase
    {
        public DataTable InitSheetResource()
        {
            //增加字段,Scts_1,Scdel  2012-10-15
            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("Select ID,CatlogCode,Description,'true' as IsSheet,Scts_1,Scdel from sys_biz_Sheet where Scdel=0 ");
            sql_select.Append(" union ");
            sql_select.Append("Select ID,CatlogCode,CatlogName as Description,'false' as IsSheet,Scts_1,Scdel from sys_biz_SheetCatlog Where Scdel=0 ");
            sql_select.Append("order by CatlogCode");

            return GetDataTable(sql_select.ToString());
        }
    }
}
