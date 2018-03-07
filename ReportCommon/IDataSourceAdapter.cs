using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ReportCommon
{
    public interface IDataSourceAdapter
    {
        DataTable ExecuteCommand(string sqlCommand);
    }
}
