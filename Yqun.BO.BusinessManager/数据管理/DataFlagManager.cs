using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Yqun.BO.BusinessManager
{
    public class DataFlagManager : BOBase
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Boolean UpdateDataFlagInfo(List<string> IDList, List<string> SCPTList)
        {
            if (IDList.Count == 0 || SCPTList.Count == 0)
                return true;

            Boolean Result = false;

            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select ID,SCPT,SCRU,SCRC from sys_biz_DataUpload where ID in ('");
            sql_select.Append(string.Join("','", IDList.ToArray()));
            sql_select.Append("')");

            DataTable Data = GetDataTable(sql_select.ToString());
            
            if (Data != null)
            {
                DataRow DataRow = null;
                foreach (String ID in IDList)
                {
                    int index = IDList.IndexOf(ID);
                    DataRow[] DataRows = Data.Select("ID='" + ID + "'");
                    if (DataRows.Length > 0)
                    {
                        DataRow = DataRows[0];
                        DataRow["SCRU"] = false;
                    }
                    else
                    {
                        DataRow = Data.NewRow();
                        DataRow["ID"] = ID;
                        DataRow["SCPT"] = SCPTList[index];
                        DataRow["SCRU"] = false;
                        DataRow["SCRC"] = 1;
                        Data.Rows.Add(DataRow);
                    }
                }

                try
                {
                    int r = Update(Data);
                    Result = (r == 1);
                }
                catch
                {
                }
            }

            return Result;
        }

        public Boolean DeleteDataFlagInfo(List<string> IDList)
        {
            if (IDList.Count == 0)
                return true;

            Boolean Result = false;

            StringBuilder sql_delete = new StringBuilder();
            sql_delete.Append("delete from sys_biz_DataUpload where ID in ('");
            sql_delete.Append(string.Join("','", IDList.ToArray()));
            sql_delete.Append("')");
           
            try
            {
                int r = ExcuteCommand(sql_delete.ToString());
                Result = (r == 1);
            }
            catch
            {
            }

            return Result;
        }
    }
}
