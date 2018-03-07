using System;
using System.Collections.Generic;
using System.Text;
using Yqun.Services;

namespace BizComponents
{
    /// <summary>
    /// 数据表管理类
    /// </summary>
    public class DataSchemaManager
    {
        public static Boolean UpdateTableStruct(string TableName)
        {
            object o = Agent.CallService("Yqun.BO.TableManager.dll", "CreateDataTable", new object[] { TableName });
            return Convert.ToBoolean(o);
        }

        public static Boolean DeleteTableStruct(string TableName)
        {
            object o = Agent.CallService("Yqun.BO.TableManager.dll", "DeleteDataTable", new object[] { TableName });
            return Convert.ToBoolean(o);
        }

        public static Boolean AppendColumnStruct(string TableName, string[] ColumnName)
        {
            object o = Agent.CallService("Yqun.BO.TableManager.dll", "AppendDataColumn", new object[] { TableName, ColumnName });
            return Convert.ToBoolean(o);
        }

        public static Boolean UpdateColumnStruct(string TableName, string[] ColumnName)
        {
            object o = Agent.CallService("Yqun.BO.TableManager.dll", "UpdateDataColumn", new object[] { TableName, ColumnName });
            return Convert.ToBoolean(o);
        }

        public static Boolean DeleteColumnStruct(string TableName, string[] ColumnName)
        {
            object o = Agent.CallService("Yqun.BO.TableManager.dll", "DeleteDataColumn", new object[] { TableName, ColumnName });
            return Convert.ToBoolean(o);
        }

        public Boolean HasData(string TableName)
        {
            object o = Agent.CallService("Yqun.BO.TableManager.dll", "HasData", new object[] { TableName });
            return Convert.ToBoolean(o);
        }
    }
}
