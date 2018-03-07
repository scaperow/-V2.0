using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yqun.Services;

namespace BizComponents
{
    public class DataTableConsistencyManager
    {
        public static List<String> GetErrorFieldList(String SheetIndex, String DataTableName)
        {
            List<String> o = Agent.CallService("Yqun.BO.BusinessManager.dll", "GetErrorFieldList", new object[] { SheetIndex, DataTableName }) as List<String>;
            return o;
        }
    }
}
