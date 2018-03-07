using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Yqun.BO.RealtimeDataManager
{
    public class ParameterManager : BOBase
    {
        public List<string> GetParameters()
        {
            List<string> Result = new List<string>();

            StringBuilder sql_RealtimeParameters = new StringBuilder();
            sql_RealtimeParameters.Append("select Parameter from sys_biz_realtime_Parameters");
            DataTable RealtimeParameters = GetDataTable(sql_RealtimeParameters.ToString());
            foreach (DataRow Row in RealtimeParameters.Rows)
            {
                String Parameter = Row["Parameter"].ToString();
                Result.Add(Parameter);
            }

            return Result;
        }
    }
}
