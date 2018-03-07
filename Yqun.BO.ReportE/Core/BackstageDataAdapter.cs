using System;
using System.Collections.Generic;
using System.Text;
using ReportCommon;

namespace Yqun.BO.ReportE.Core
{
    public class BackstageDataAdapter : IDataSourceAdapter
    {
        BOBase BOBase = new BOBase();

        public System.Data.DataTable ExecuteCommand(string sqlCommand)
        {
            return BOBase.GetDataTable(sqlCommand);
        }
    }
}
