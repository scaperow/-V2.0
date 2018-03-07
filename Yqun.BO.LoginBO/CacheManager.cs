using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using Yqun.Data.DataBase;
using System.Data.SqlClient;

namespace Yqun.BO
{
    public class CacheManager : BOBase
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public object GetMaxSCTS(String TableName)
        {
            StringBuilder sql_count = new StringBuilder();
            sql_count.Append("select count(*) from ");
            sql_count.Append(TableName);

            int Count = Convert.ToInt32(ExcuteScalar(sql_count.ToString()));
            if (Count > 0)
            {
                StringBuilder sql_max = new StringBuilder();
                sql_max.Append("select max(scts) from ");
                sql_max.Append(TableName);

                return ExcuteScalar(sql_max.ToString());
            }

            return null;
        }

        /// <summary>
        /// 获取Scts_1 的最大时间
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <returns></returns>
        public object GetMaxSCTS1(String TableName)
        {
            StringBuilder sql_max = new StringBuilder();
            sql_max.Append("select max(scts_1) from ");
            sql_max.Append(TableName);

            return ExcuteScalar(sql_max.ToString());


        }

        /// <summary>
        /// 从服务器端获取要更新的数据  寇志凯  2013-10-18
        /// </summary>
        /// <param name="ht"></param>
        /// <returns></returns>
        public DataSet GetNewData(List<String> ht)
        {
            DataSet ds = new DataSet();
            try
            {
                foreach (var item in ht)
                {
                   
                    logger.Error(item);
                    DataTable dt = GetDataTable(item);
                    
                    if (dt.Rows.Count > 0)
                    {
                        ds.Tables.Add(dt.Copy());
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("获取被更新数据错误：" + ex.Message);

            }
            return ds;
        }

        /// <summary>
        /// 比较客户端数据库是否有字段Scdel,Scts_1,如果没有则增加  寇志凯  2013-10-18
        /// </summary>
        public void CompareField()
        {
            #region  同步 Scts_1,Scdel字段
            string sql = "";
            sql = @"if col_length('sys_biz_Cache', 'Scts_1') is NULL
                ALTER TABLE sys_biz_Cache ADD Scts_1 DATETIME
                if col_length('sys_biz_Cache', 'Scdel') is NULL
                ALTER TABLE sys_biz_Cache ADD Scdel smallint NOT NULL DEFAULT 0
                
                if col_length('sys_biz_moduleview', 'Scts_1') is NULL
                ALTER TABLE sys_biz_moduleview ADD Scts_1 DATETIME
                if col_length('sys_biz_moduleview', 'Scdel') is NULL
                ALTER TABLE sys_biz_moduleview ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_biz_ModuleCatlog', 'Scts_1') is NULL
                ALTER TABLE sys_biz_ModuleCatlog ADD Scts_1 DATETIME
                if col_length('sys_biz_ModuleCatlog', 'Scdel') is NULL
                ALTER TABLE sys_biz_ModuleCatlog ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_biz_SheetCatlog', 'Scts_1') is NULL
                ALTER TABLE sys_biz_SheetCatlog ADD Scts_1 DATETIME
                if col_length('sys_biz_SheetCatlog', 'Scdel') is NULL
                ALTER TABLE sys_biz_SheetCatlog ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_biz_Module', 'Scts_1') is NULL
                ALTER TABLE sys_biz_Module ADD Scts_1 DATETIME
                if col_length('sys_biz_Module', 'Scdel') is NULL
                ALTER TABLE sys_biz_Module ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_biz_Sheet', 'Scts_1') is NULL
                ALTER TABLE sys_biz_Sheet ADD Scts_1 DATETIME
                if col_length('sys_biz_Sheet', 'Scdel') is NULL
                ALTER TABLE sys_biz_Sheet ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_tables', 'Scts_1') is NULL
                ALTER TABLE sys_tables ADD Scts_1 DATETIME
                if col_length('sys_tables', 'Scdel') is NULL
                ALTER TABLE sys_tables ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_columns', 'Scts_1') is NULL
                ALTER TABLE sys_columns ADD Scts_1 DATETIME
                if col_length('sys_columns', 'Scdel') is NULL
                ALTER TABLE sys_columns ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_biz_DataArea', 'Scts_1') is NULL
                ALTER TABLE sys_biz_DataArea ADD Scts_1 DATETIME
                if col_length('sys_biz_DataArea', 'Scdel') is NULL
                ALTER TABLE sys_biz_DataArea ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_moduleview', 'Scts_1') is NULL
                ALTER TABLE sys_moduleview ADD Scts_1 DATETIME
                if col_length('sys_moduleview', 'Scdel') is NULL
                ALTER TABLE sys_moduleview ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_biz_FunctionInfos', 'Scts_1') is NULL
                ALTER TABLE sys_biz_FunctionInfos ADD Scts_1 DATETIME
                if col_length('sys_biz_FunctionInfos', 'Scdel') is NULL
                ALTER TABLE sys_biz_FunctionInfos ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_biz_CrossSheetFormulas', 'Scts_1') is NULL
                ALTER TABLE sys_biz_CrossSheetFormulas ADD Scts_1 DATETIME
                if col_length('sys_biz_CrossSheetFormulas', 'Scdel') is NULL
                ALTER TABLE sys_biz_CrossSheetFormulas ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_dictionary', 'Scts_1') is NULL
                ALTER TABLE sys_dictionary ADD Scts_1 DATETIME
                if col_length('sys_dictionary', 'Scdel') is NULL
                ALTER TABLE sys_dictionary ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_qualificationauth', 'Scts_1') is NULL
                ALTER TABLE sys_qualificationauth ADD Scts_1 DATETIME
                if col_length('sys_qualificationauth', 'Scdel') is NULL
                ALTER TABLE sys_qualificationauth ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_biz_reminder_evaluatecondition', 'Scts_1') is NULL
                ALTER TABLE sys_biz_reminder_evaluatecondition ADD Scts_1 DATETIME
                if col_length('sys_biz_reminder_evaluatecondition', 'Scdel') is NULL
                ALTER TABLE sys_biz_reminder_evaluatecondition ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_biz_reminder_Itemcondition', 'Scts_1') is NULL
                ALTER TABLE sys_biz_reminder_Itemcondition ADD Scts_1 DATETIME
                if col_length('sys_biz_reminder_Itemcondition', 'Scdel') is NULL
                ALTER TABLE sys_biz_reminder_Itemcondition ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_biz_Reporttables', 'Scts_1') is NULL
                ALTER TABLE sys_biz_Reporttables ADD Scts_1 DATETIME
                if col_length('sys_biz_Reporttables', 'Scdel') is NULL
                ALTER TABLE sys_biz_Reporttables ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_biz_ReportRTConfig', 'Scts_1') is NULL
                ALTER TABLE sys_biz_ReportRTConfig ADD Scts_1 DATETIME
                if col_length('sys_biz_ReportRTConfig', 'Scdel') is NULL
                ALTER TABLE sys_biz_ReportRTConfig ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_biz_ReportFormatStrings', 'Scts_1') is NULL
                ALTER TABLE sys_biz_ReportFormatStrings ADD Scts_1 DATETIME
                if col_length('sys_biz_ReportFormatStrings', 'Scdel') is NULL
                ALTER TABLE sys_biz_ReportFormatStrings ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_biz_ReportParameters', 'Scts_1') is NULL
                ALTER TABLE sys_biz_ReportParameters ADD Scts_1 DATETIME
                if col_length('sys_biz_ReportParameters', 'Scdel') is NULL
                ALTER TABLE sys_biz_ReportParameters ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_biz_ReportSheet', 'Scts_1') is NULL
                ALTER TABLE sys_biz_ReportSheet ADD Scts_1 DATETIME
                if col_length('sys_biz_ReportSheet', 'Scdel') is NULL
                ALTER TABLE sys_biz_ReportSheet ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_auth_Roles', 'Scts_1') is NULL
                ALTER TABLE sys_auth_Roles ADD Scts_1 DATETIME
                if col_length('sys_auth_Roles', 'Scdel') is NULL
                ALTER TABLE sys_auth_Roles ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_auth_Users', 'Scts_1') is NULL
                ALTER TABLE sys_auth_Users ADD Scts_1 DATETIME
                if col_length('sys_auth_Users', 'Scdel') is NULL
                ALTER TABLE sys_auth_Users ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_auth_Permissions', 'Scts_1') is NULL
                ALTER TABLE sys_auth_Permissions ADD Scts_1 DATETIME
                if col_length('sys_auth_Permissions', 'Scdel') is NULL
                ALTER TABLE sys_auth_Permissions ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_auth_FunctionPermission', 'Scts_1') is NULL
                ALTER TABLE sys_auth_FunctionPermission ADD Scts_1 DATETIME
                if col_length('sys_auth_FunctionPermission', 'Scdel') is NULL
                ALTER TABLE sys_auth_FunctionPermission ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_auth_RecordPermission', 'Scts_1') is NULL
                ALTER TABLE sys_auth_RecordPermission ADD Scts_1 DATETIME
                if col_length('sys_auth_RecordPermission', 'Scdel') is NULL
                ALTER TABLE sys_auth_RecordPermission ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_engs_Tree', 'Scts_1') is NULL
                ALTER TABLE sys_engs_Tree ADD Scts_1 DATETIME
                if col_length('sys_engs_Tree', 'Scdel') is NULL
                ALTER TABLE sys_engs_Tree ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_engs_ProjectInfo', 'Scts_1') is NULL
                ALTER TABLE sys_engs_ProjectInfo ADD Scts_1 DATETIME
                if col_length('sys_engs_ProjectInfo', 'Scdel') is NULL
                ALTER TABLE sys_engs_ProjectInfo ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_engs_SectionInfo', 'Scts_1') is NULL
                ALTER TABLE sys_engs_SectionInfo ADD Scts_1 DATETIME
                if col_length('sys_engs_SectionInfo', 'Scdel') is NULL
                ALTER TABLE sys_engs_SectionInfo ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_engs_CompanyInfo', 'Scts_1') is NULL
                ALTER TABLE sys_engs_CompanyInfo ADD Scts_1 DATETIME
                if col_length('sys_engs_CompanyInfo', 'Scdel') is NULL
                ALTER TABLE sys_engs_CompanyInfo ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_engs_ItemInfo', 'Scts_1') is NULL
                ALTER TABLE sys_engs_ItemInfo ADD Scts_1 DATETIME
                if col_length('sys_engs_ItemInfo', 'Scdel') is NULL
                ALTER TABLE sys_engs_ItemInfo ADD Scdel smallint NOT NULL DEFAULT 0

                if col_length('sys_biz_ReportCatlog', 'Scts_1') is NULL
                ALTER TABLE sys_biz_ReportCatlog ADD Scts_1 DATETIME
                if col_length('sys_biz_ReportCatlog', 'Scdel') is NULL
                ALTER TABLE sys_biz_ReportCatlog ADD Scdel smallint NOT NULL DEFAULT 0

                ";
            #endregion
            ExcuteCommand(sql);
        }

        /// <summary>
        /// 获得待更新的数据的所有的不同的时间戳
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="scts"></param>
        /// <returns></returns>
        public DataTable GetNewDataSCTS(String TableName, object scts)
        {
            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select distinct scts from ");
            sql_select.Append(TableName);

            DateTime dt;
            if (scts != null && DateTime.TryParse(scts.ToString(), out dt))
            {
                sql_select.Append(" where DateDiff(second,'");
                sql_select.Append(dt.ToString());
                sql_select.Append("',scts) > 0");
            }

            sql_select.Append(" order by scts");

            return GetDataTable(sql_select.ToString());
        }

        /// <summary>
        /// 获得待更新的数据的总记录数
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="scts"></param>
        /// <returns></returns>
        public object GetNewDataCount(String TableName, object scts)
        {
            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select count(*) from ");
            sql_select.Append(TableName);

            DateTime dt;
            if (scts != null && DateTime.TryParse(scts.ToString(), out dt))
            {
                sql_select.Append(" where DateDiff(second,'");
                sql_select.Append(dt.ToString());
                sql_select.Append("',scts) > 0");
            }

            return ExcuteScalar(sql_select.ToString());
        }

        /// <summary>
        /// 获取指定数据表中比指定的时间大的所有数据
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="scts"></param>
        /// <returns></returns>
        public DataTable GetNewTableData(String TableName, object scts)
        {
            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select * from ");
            sql_select.Append(TableName);

            DateTime dt;
            if (scts != null && DateTime.TryParse(scts.ToString(), out dt))
            {
                sql_select.Append(" where DateDiff(second,'");
                sql_select.Append(dt.ToString());
                sql_select.Append("',scts) > 0");
            }

            return GetDataTable(sql_select.ToString());
        }

        /// <summary>
        /// 获取指定数据表中
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="scts"></param>
        /// <returns></returns>
        public DataTable GetNewTableData2(String TableName, object scts)
        {
            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select * from ");
            sql_select.Append(TableName);

            DateTime dt;
            if (scts != null && DateTime.TryParse(scts.ToString(), out dt))
            {
                sql_select.Append(" where DateDiff(second,'");
                sql_select.Append(dt.ToString());
                sql_select.Append("',scts) = 0");
            }
            else if (scts == DBNull.Value)
            {
                sql_select.Append(" where scts is null");
            }
            else
            {
                sql_select.Append(" where 1<>1");
            }

            return GetDataTable(sql_select.ToString());
        }

        public DataTable GetNewTableData3(String TableName, List<string> IDList)
        {
            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select * from ");
            sql_select.Append(TableName);

            if (IDList.Count > 0)
            {
                sql_select.Append(" where ID in ('");
                sql_select.Append(string.Join("','", IDList.ToArray()));
                sql_select.Append("')");
            }
            else
            {
                sql_select.Append(" where 1<>1");
            }

            return GetDataTable(sql_select.ToString());
        }

        public List<String> GetNewTableIDList(String TableName)
        {
            List<string> IDList = new List<string>();

            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select ID from ");
            sql_select.Append(TableName);

            DataTable Data = GetDataTable(sql_select.ToString());
            if (Data != null)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    String Index = Row["ID"].ToString();
                    IDList.Add(Index);
                }
            }

            return IDList;
        }
        public Boolean UpdateTableData(String TableName, DataTable Data)
        {
            Boolean Result = false;

            //zhangdahang 20131118
            List<string> values = new List<string>();
            DataTable KeyData = Data.DefaultView.ToTable(false, "ID");
            foreach (DataRow Row in KeyData.Rows)
            {
                values.Add(Row["ID"].ToString());
            }

                StringBuilder sql_select = new StringBuilder();
                sql_select.Append("select * from ");
                sql_select.Append(TableName);
                sql_select.Append(string.Concat(" where ID in ('", string.Join("','", values.ToArray()), "')  order by ID"));

                DataTable LocalTableData = GetDataTable(sql_select.ToString());
                Data.DefaultView.Sort = "ID";
                DataTable source = Data.DefaultView.ToTable();
                if (LocalTableData != null)
                {
                    if (LocalTableData.Rows.Count == Data.Rows.Count)
                    {
                        for (int i = 0; i < source.Rows.Count; i++)
                        {
                            LocalTableData.Rows[i].ItemArray = source.Rows[i].ItemArray.Clone() as object[];
                        }
                    }
                    else
                    {
                        for (int i = 0; i < source.Rows.Count; i++)
                        {
                            DataRow[] rows = LocalTableData.Select("ID = '" + source.Rows[i]["ID"].ToString() + "'");

                            if (rows.Length == 0)
                            {
                                DataRow newRow = LocalTableData.NewRow();
                                LocalTableData.Rows.InsertAt(newRow, i);
                                newRow.ItemArray = source.Rows[i].ItemArray;
                            }
                            else
                            {
                                rows[0].ItemArray = source.Rows[i].ItemArray;
                            }
                        }
                    }
                    try
                    {
                        int r = Update(LocalTableData);
                        Result = (r == 1);
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                    }
                }
            return Result;
        }

        /// <summary>
        /// 将缓存表中的时间戳设置为当前时间，并且每条记录时间都差1毫秒
        /// </summary>
        public void SetCatchTimeToCurrent()
        {

        }
    }
}
