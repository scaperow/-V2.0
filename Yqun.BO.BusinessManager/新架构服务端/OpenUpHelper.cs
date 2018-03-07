using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Yqun.BO.BusinessManager
{
    public class OpenUpHelper : BOBase
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataTable GetTable(string sql)
        {
            return GetDataTable(sql);
        }

        public int Insert(string table, Dictionary<string, object> columns)
        {
            if (columns.Keys.Count == 0)
            {
                return 0;
            }

            var builder = new StringBuilder();
            var fields = new List<string>();
            var parameters = new List<string>();
            var command = GetDbCommand("");

            foreach (var key in columns.Keys)
            {
                if (columns[key] != null)
                {
                    fields.Add(key);
                    parameters.Add("@" + key);
                    command.Parameters.Add(new SqlParameter("@" + key, columns[key]));
                }
            }

            var fs = string.Join(",", fields.ToArray());
            var ps = string.Join(",", parameters.ToArray());

            builder.Append("INSERT INTO " + table + " (" + fs + ") VALUES (" + ps + ")");
            command.CommandText = builder.ToString();

            if (command.Connection.State != ConnectionState.Open)
            {
                try
                {
                    command.Connection.Open();
                    command.ExecuteNonQuery();

                    return 0;
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
                finally
                {
                    try
                    {
                        command.Connection.Close();
                        command.Connection.Dispose();
                    }
                    catch (Exception) { }
                }
            }

            return -1;
        }

        public int Execute(string sql)
        {
            return Execute(sql);
        }
    }
}
