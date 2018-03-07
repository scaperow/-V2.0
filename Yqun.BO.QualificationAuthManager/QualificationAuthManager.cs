using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Yqun.BO.QualificationManager
{
    public class QualificationAuthManager : BOBase
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public List<String> InitQualificationModelIndex(String FolderCode)
        {
            List<String> List = new List<string>();

            List<String> Codes = InitQualificationAuthInfo(FolderCode);
            if (Codes.Count > 0)
            {
                //增加查询条件 Scdel=0 2013-10-17
                StringBuilder sql_select = new StringBuilder();
                sql_select.Append("select ID from sys_biz_Module where Scdel=0 and ");
                for (int i = 0; i < Codes.Count;i++)
                {
                    Codes[i] = string.Format("CatlogCode like '{0}%'", Codes[i]);
                }
                sql_select.Append(string.Join(" or ", Codes.ToArray()));

                DataTable Data = GetDataTable(sql_select.ToString());
                if (Data != null)
                {
                    foreach (DataRow Row in Data.Rows)
                    {
                        String Index = Row["ID"].ToString();
                        List.Add(Index);
                    }
                }
            }
            return List;
        }

        public string GetDeniedModuleIDs(String FolderCode)
        {
            string strIDs = string.Empty;

            //增加查询条件 Scdel=0 2013-10-17
            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("Select Code from sys_QualificationAuth ");
            sql_select.Append("where Scdel=0 and FolderCode = '");
            sql_select.Append(FolderCode);
            sql_select.Append("'");

            DataTable Data = GetDataTable(sql_select.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                strIDs = Data.Rows[0]["Code"].ToString();
            }

            return strIDs;
        }
        public List<String> InitQualificationAuthInfo(String FolderCode)
        {
            List<String> List = new List<string>();

            //增加查询条件 Scdel=0 2013-10-17
            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("Select Code from sys_QualificationAuth ");
            sql_select.Append("where Scdel=0 and FolderCode = '");
            sql_select.Append(FolderCode);
            sql_select.Append("'");

            DataTable Data = GetDataTable(sql_select.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                string[] Codes = Data.Rows[0]["Code"].ToString().Split(',');
                List.AddRange(Codes);
            }

            return List;
        }

        public Boolean UpdateQualificationAuth(String FolderCode, String Codes)
        {
            Boolean Result = false;

            //增加查询条件 Scdel=0 2013-10-17
            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("Select * from sys_QualificationAuth ");
            sql_select.Append("where Scdel=0 and FolderCode = '");
            sql_select.Append(FolderCode);
            sql_select.Append("'");

            DataTable Data = GetDataTable(sql_select.ToString());
            if (Data != null)
            {
                DataRow DataRow;
                if (Data.Rows.Count > 0)
                {
                    DataRow = Data.Rows[0];
                    DataRow["Scts"] = DateTime.Now.ToString();
                    DataRow["Code"] = Codes;
                    DataRow["FolderCode"] = FolderCode;

                    DataRow["Scts_1"] = DateTime.Now.ToString();
                }
                else
                {
                    DataRow = Data.NewRow();
                    DataRow["ID"] = Guid.NewGuid().ToString();
                    DataRow["Scts"] = DateTime.Now.ToString();
                    DataRow["Code"] = Codes;
                    DataRow["FolderCode"] = FolderCode;

                    DataRow["Scts_1"] = DateTime.Now.ToString();
                    Data.Rows.Add(DataRow);
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
    }
}
