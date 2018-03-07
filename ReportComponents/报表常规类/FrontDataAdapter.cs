using System;
using System.Collections.Generic;
using System.Text;
using Yqun.Services;
using System.Data;

namespace ReportCommon
{
    public class FrontDataAdapter : IDataSourceAdapter
    {
        public System.Data.DataTable ExecuteCommand(string sqlCommand)
        {
            return Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { sqlCommand }) as DataTable;
        }
    }
}
