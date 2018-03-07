using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Yqun.BO.BusinessManager
{
    public class ModelSheetResourceManager : BOBase
    {
        public DataTable InitModelSheetData()
        {
            //增加查询条件Scdel=0   2013-10-15
            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select ID,CatlogCode,Description,Sheets,Scts_1,Scdel, 'false' as IsSheet from sys_biz_Module Where Scdel=0 ");
            sql_select.Append(" union ");
            sql_select.Append("select ID,CatlogCode,Description,'' as Sheets,Scts_1,Scdel,'true' as IsSheet from sys_biz_Sheet Where Scdel=0 ");
            sql_select.Append("order by CatlogCode");

            return GetDataTable(sql_select.ToString());
        }
    }
}
