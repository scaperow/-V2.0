using System;
using System.Collections.Generic;
using System.Text;
using Yqun.Services;

namespace BizComponents
{
    public class FieldNameCreator
    {
        public static Boolean HasFieldName(string tableName,string fieldName)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "HasFieldName", new object[] { tableName,fieldName }));
        }
    }
}
