using System;
using System.Collections.Generic;
using System.Text;
using Yqun.Services;

namespace BizComponents
{
    public class TableNameCreator
    {
        public static Boolean HasTableName(string TableName)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "HasTableName", new object[] { TableName }));
        }
    }
}
