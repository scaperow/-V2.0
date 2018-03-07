using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BizCommon;
using Yqun.BO.QualificationManager;
using Yqun.Common.ContextCache;

namespace Yqun.BO.ReminderManager
{
    public class LabStadiumListManager : BOBase
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        TestItemManager testItemManager = new TestItemManager();
        StadiumInfoManager stadiumInfoManager = new StadiumInfoManager();
        QualificationAuthManager QualificationManager = new QualificationAuthManager();

        public DataTable GetLabStaidumSMSList(String TestRoomCode)
        {
            DataTable result = new DataTable();

            // 45行 增加查询条件 Scdel=0  2013-10-17
            String sql = @"select 
            a.DataID,
            a.ModelCode,
            a.ModelIndex,
            e.ShortName as 工程,
            c.Description as 标段,
            b.Description as 单位,
            d.Description as 试验室,
			(select Description from sys_biz_Module as f where f.ID = a.ModelIndex) as 试验模板,
			a.F_SYXM as 试验项目,
            a.F_WTBH as 委托编号,
			a.F_ZJRQ as 制件日期,
			a.F_SJBH as 试验编号,
			a.F_PH as 批号,
            a.F_Added,
            a.F_SJSize as 试验尺寸,
		    a.DateSpan as 龄期,
			a.F_IsDone
            from 
            sys_biz_reminder_stadiumData as a,
            (select a.NodeCode,b.ShortName from sys_engs_Tree as a,sys_engs_ProjectInfo as b where a.Scdel=0 and a.RalationID = b.ID) as e,
            (select a.NodeCode,b.Description from sys_engs_Tree as a,sys_engs_CompanyInfo as b where a.RalationID = b.ID) as b,
            (select a.NodeCode,b.Description from sys_engs_Tree as a,sys_engs_SectionInfo as b where a.RalationID = b.ID) as c,
            (select a.NodeCode,b.Description from sys_engs_Tree as a,sys_engs_ItemInfo as b where a.RalationID = b.ID) as d
            where b.NodeCode = substring(a.ModelCode,0,Len(a.ModelCode)-7) and 
                  c.NodeCode = substring(a.ModelCode,0,Len(a.ModelCode)-11) and
                  d.NodeCode = substring(a.ModelCode,0,Len(a.ModelCode)-3) and
                  e.NodeCode = substring(a.ModelCode,0,Len(a.ModelCode)-15) and
				  a.F_IsDone is null ";

            sql = sql + " and ModelCode like '" + TestRoomCode + "%'";

            List<string> modelIndexs = QualificationManager.InitQualificationModelIndex(TestRoomCode);
            foreach (var item in modelIndexs)
            {
                DataTable tb = GetDataTable("SELECT SearchRange,StadiumConfig FROM dbo.sys_biz_reminder_stadiumInfo WHERE ID='" + item + "'");
                if (tb != null && tb.Rows.Count > 0)
                {
                    String range = tb.Rows[0]["SearchRange"].ToString();
                    String newSql = sql + " AND a.ModelIndex='" + item +
                        "' AND DATEDIFF(day, DATEADD(DAY, a.DateSpan, a.F_ZJRQ), GETDATE()) IN (" + range + ")";

                    newSql = newSql + " order by a.F_SYXM";

                    DataTable subTable = GetDataTable(newSql);
                    if (subTable != null)
                    {
                        result.Merge(subTable);
                    }
                }
            }

            return result;
        }

        public DataTable GetLabStadiumReminderList(String TestRoomCode, String Type)
        {
            Boolean IsAdmin = ApplicationContext.Current.IsAdministrator;
            if (IsAdmin || TestRoomCode == "")
                return new DataTable();

            List<string> TestItems = testItemManager.GetTestItemList(Type);

            String sql = @"SELECT 
            a.ID,
            a.DataID,
            a.ModelCode,
            a.ModelIndex,
            a.DateSpan,
            a.F_Name,
            a.F_PH,
            a.F_ZJRQ,
            a.F_SJBH,
            a.F_SJSize,
            a.F_SYXM,
            a.F_BGBH,
            a.F_WTBH,
            a.F_ItemIndex
            FROM dbo.sys_biz_reminder_stadiumData a 
            WHERE a.F_IsDone IS NULL and a.F_ItemId in ('" + string.Join("','",TestItems.ToArray()) + "')";

            List<string> modelIndexs = QualificationManager.InitQualificationModelIndex(TestRoomCode);
            sql += " AND LEFT(a.ModelCode, 16)='" + TestRoomCode + "' ";

            DataTable result = new DataTable();
            foreach (var item in modelIndexs)
            {
                DataTable tb = GetDataTable("SELECT SearchRange,StadiumConfig FROM dbo.sys_biz_reminder_stadiumInfo WHERE ID='" + item + "'");
                if (tb != null && tb.Rows.Count > 0)
                {
                    String range = tb.Rows[0]["SearchRange"].ToString();
                    String newSql = sql + " AND a.ModelIndex='" + item +
                        "' AND DATEDIFF(day, DATEADD(DAY, a.DateSpan, a.F_ZJRQ), GETDATE()) IN (" + range + ")";

                    //logger.Error(newSql);

                    DataTable subTable = GetDataTable(newSql);
                    if (subTable != null)
                    {
                        result.Merge(subTable);
                    }
                }
            }

            return result;
        }

        public DataTable GetLabStadiumReminderData(String ItemId, String TestRoomCode)
        {
            Boolean IsAdmin = ApplicationContext.Current.IsAdministrator;
            if (IsAdmin || TestRoomCode == "")
                return new DataTable();

            String sql = @"SELECT 
            a.ID,
            a.DataID,
            a.ModelCode,
            a.ModelIndex,
            a.DateSpan,
            a.F_Name,
            a.F_PH,
            a.F_ZJRQ,
            a.F_SJBH,
            a.F_SJSize,
            a.F_SYXM,
            a.F_BGBH,
            a.F_WTBH
            FROM dbo.sys_biz_reminder_stadiumData a 
            WHERE a.F_IsDone IS NULL and a.F_ItemId ='" + ItemId + "' ";

            List<string> modelIndexs = QualificationManager.InitQualificationModelIndex(TestRoomCode);
            sql += " AND LEFT(a.ModelCode, 16)='" + TestRoomCode + "' ";

            DataTable result = new DataTable();
            foreach (var item in modelIndexs)
            {
                String sql2 = "SELECT SearchRange,StadiumConfig FROM dbo.sys_biz_reminder_stadiumInfo WHERE ID='" + item + "'";
                DataTable tb = GetDataTable(sql2);
                if (tb != null && tb.Rows.Count > 0)
                {
                    String range = tb.Rows[0]["SearchRange"].ToString();
                    String newSql = sql + " AND a.ModelIndex='" + item +
                        "' AND DATEDIFF(day, DATEADD(DAY, a.DateSpan, a.F_ZJRQ), GETDATE()) IN (" + range + ")";

                    //logger.Error(newSql);

                    DataTable subTable = GetDataTable(newSql);
                    if (subTable != null)
                    {
                        result.Merge(subTable);
                    }

                    String newRedoSql = sql + " AND a.ModelIndex='" + item +
                        "' AND DATEDIFF(day, DATEADD(DAY, a.DateSpan, a.F_ZJRQ), GETDATE()) > 0";

                    logger.Error(newRedoSql);

                    DataTable subTable1 = GetDataTable(newRedoSql);
                    if (subTable1 != null)
                    {
                        result.Merge(subTable1);
                    }
                }
            }

            return result;
        }

        public DataTable GetLabStadiumReminderInfos(String TestRoomCode)
        {
            Boolean IsAdmin = ApplicationContext.Current.IsAdministrator;
            //if (HasLabStadiumList(DateTime.Now, TestRoomCode, IsAdmin))
            if (IsAdmin || TestRoomCode == "")
                return InitLabStadiumList(DateTime.Now, TestRoomCode, IsAdmin);

            List<string> ModelIndexs = QualificationManager.InitQualificationModelIndex(TestRoomCode);
            List<StadiumInfo> Infos = stadiumInfoManager.InitStadiumInfos();
            DataTable TotalData = new DataTable();

            try
            {
                foreach (StadiumInfo Info in Infos)
                {
                    StringBuilder sql_select = GetSQLCommand(Info, TestRoomCode);
                    if (sql_select.ToString().Length > 0)
                    {
                        DataTable Data = GetDataTable(sql_select.ToString());
                        DataTable NewData = Data.Clone();
                        if (Data != null)
                        {
                            foreach (DataRow Row in Data.Rows)
                            {
                                String ModelIndex = Row["ModelIndex"].ToString();
                                if (ModelIndexs.Contains(ModelIndex))
                                {
                                    DataRow NewRow = NewData.NewRow();
                                    NewRow.ItemArray = Row.ItemArray;
                                    NewData.Rows.Add(NewRow);
                                }
                            }
                            TotalData.Merge(NewData);
                        }
                    }
                }

                if (TotalData.Rows.Count > 0)
                {
                    //保存龄期提醒信息到服务器
                    Boolean r = UpdateLabStadiumList(TotalData, DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            string[] columNames = new string[]{"ID",
                                               "ModelCode",
                                               "ModelIndex",
                                               "DateSpan",                                               
                                               "名称",
                                               "批号",
                                               "制件日期",
                                               "试件编号",
                                               "试件尺寸",
                                               "试验项目",
                                               "报告编号",
                                               "委托编号"
                                                };
            TotalData.DefaultView.Sort = "试件编号";
            DataTable ResultData = TotalData.DefaultView.ToTable("DataTable1", false, columNames);
            return ResultData;
        }

        private StringBuilder GetSQLCommand(StadiumInfo Info, String TestRoomCode)
        {
            List<string> sql_Tables = new List<string>();
            List<String> sql_Fields = new List<string>();
            String[] Tokens;
            foreach (String field in Info.F_Columns)
            {
                String ItemName = field.Substring(0, field.IndexOf(','));
                String ItemValue = field.Substring(field.IndexOf(',')).Trim(',');

                if (ItemValue != "")
                {
                    if (ItemName != "试验项目")
                    {
                        Tokens = ItemValue.Split('.');
                        sql_Fields.Add(string.Format("[{0}].[{1}] as {2}", Tokens[0], Tokens[1], ItemName));

                        if (!sql_Tables.Contains(string.Format("[{0}]", Tokens[0])))
                            sql_Tables.Add(string.Format("[{0}]", Tokens[0]));
                    }
                    else
                    {
                        if (ItemValue.Trim().StartsWith("{") && ItemValue.Trim().EndsWith("}"))
                        {
                            StringBuilder sql_field_F_ItemId = new StringBuilder();
                            StringBuilder sql_field_F_Name = new StringBuilder();
                            sql_field_F_ItemId.Append("'ItemId' = Case ");
                            sql_field_F_Name.Append(string.Format("'{0}' = Case ", ItemName));
                            String[] itemIdList = ItemValue.Trim('{', '}').Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            String[] lqList = Info.F_List.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string lq in lqList)
                            {
                                int k = Array.IndexOf(lqList, lq);
                                sql_field_F_ItemId.Append(string.Format(" When {0}='{1}' Then '{2}' ", "Convert(varchar(10),DateDiff(Day," + Info.F_ZJRQ + ",GetDate()))", lq, itemIdList[k]));
                                sql_field_F_Name.Append(string.Format(" When {0}='{1}' Then (select ItemName from sys_biz_reminder_testitem where ID='{2}') ", "Convert(varchar(10),DateDiff(Day," + Info.F_ZJRQ + ",GetDate()))", lq, itemIdList[k]));
                            }
                            sql_field_F_ItemId.Append(" End ");
                            sql_field_F_Name.Append(" End ");
                            sql_Fields.Insert(0, sql_field_F_ItemId.ToString());
                            sql_Fields.Add(sql_field_F_Name.ToString());
                        }
                        else
                        {
                            Tokens = Info.F_List.Split('.');
                            if (!sql_Tables.Contains(string.Format("[{0}]", Tokens[0])))
                                sql_Tables.Add(string.Format("[{0}]", Tokens[0]));

                            sql_Fields.Insert(0, string.Format("{0} as ItemId", ItemValue));
                            sql_Fields.Add(string.Format("(select ItemName from sys_biz_reminder_testitem where ID=({0})) as {1}", ItemValue.Trim('\''), ItemName));
                        }
                    }
                }
                else
                {
                    sql_Fields.Add(string.Format("'' as {0}", ItemName));
                }
            }

            //Tokens = Info.F_ZJRQ.Split(new char[] { '.' });
            //sql_Fields.Add(string.Format("[{0}].[{1}] as 试验日期", Tokens[0], Tokens[1]));
            //if (!sql_Tables.Contains(string.Format("[{0}]", Tokens[0])))
            //    sql_Tables.Add(string.Format("[{0}]", Tokens[0]));

            List<string> sql_Wheres = new List<string>();
            Tokens = Info.F_List.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in Tokens)
            {
                sql_Wheres.Add(string.Format("DateDiff(Day,{0},GetDate()) = Convert(int,Replace({1},'d',''))", Info.F_ZJRQ, s));
            }

            List<string> sql_Relations = new List<string>();
            if (sql_Tables.Count > 1)
            {
                string left = sql_Tables[0];
                foreach (String table in sql_Tables)
                {
                    if (table == left)
                        continue;

                    sql_Relations.Add(string.Format("{0}.ID = {1}.ID", left, table));
                }
            }

            StringBuilder sql_select = new StringBuilder();
            if (sql_Fields.Count > 0 && sql_Tables.Count > 0 && sql_Wheres.Count > 0)
            {
                String ID = String.Format("{0}.ID", sql_Tables[0]);
                String ModelCode = String.Format("{0}.SCPT as ModelCode", sql_Tables[0]);
                String ModelIndex = String.Format("'{0}' as ModelIndex", Info.Index);
                String DateSpan = string.Format("DateDiff(Day,{0},GetDate()) as DateSpan", Info.F_ZJRQ);
                String SCTS = String.Format("{0}.SCTS", sql_Tables[0]);
                String Date = String.Format("({0} between DATEADD(\"Day\", -30,GETDATE()) and GetDate())", SCTS);
                String TestRoom = String.Format("{0}.SCPT like '{1}____'", sql_Tables[0], TestRoomCode);

                sql_select.Append("select ");
                sql_select.Append(ID);
                sql_select.Append(",");
                sql_select.Append(ModelCode);
                sql_select.Append(",");
                sql_select.Append(ModelIndex);
                sql_select.Append(",");
                sql_select.Append(DateSpan);
                sql_select.Append(",");             
                sql_select.Append(string.Join(",", sql_Fields.ToArray()));
                sql_select.Append(" from ");
                sql_select.Append(string.Join(",", sql_Tables.ToArray()));
                //sql_select.Append(",v_codeName as a1,v_codeName as a2,v_codeName as a3 ");
                sql_select.Append(" where ");
                sql_select.Append(Date);
                sql_select.Append(" and ");
                sql_select.Append(string.Concat("(", string.Join(" or ", sql_Wheres.ToArray()), ")"));
                sql_select.Append(" and ");
                sql_select.Append(TestRoom);

                if (sql_Relations.Count > 0)
                {
                    sql_select.Append(" and ");
                    sql_select.Append(string.Join(" and ", sql_Relations.ToArray()));
                }             
            }
            return sql_select;
        }

        public Boolean HasLabStadiumList(DateTime Date, String FolderCode, Boolean IsAdmin)
        {
            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select count(*) from sys_biz_reminder_stadiumData where Date='");
            sql_select.Append(Date.ToShortDateString());
            sql_select.Append("'");

            if (!IsAdmin)
            {
                sql_select.Append(" and ModelCode like '");
                sql_select.Append(FolderCode);
                sql_select.Append("____'");
            }

            object Count = ExcuteScalar(sql_select.ToString());
            return Convert.ToInt32(Count) > 0;
        }

        public DataTable InitLabStadiumList(DateTime Date, String FolderCode, Boolean IsAdmin)
        {
            DataTable Data = new DataTable();
            Data.Columns.Add("ID", typeof(string));
            Data.Columns.Add("ModelCode", typeof(string));
            Data.Columns.Add("ModelIndex", typeof(string));
            Data.Columns.Add("DateSpan", typeof(string));
            //Data.Columns.Add("ItemId", typeof(string));
            Data.Columns.Add("名称", typeof(string));
            Data.Columns.Add("批号", typeof(string));
            Data.Columns.Add("制件日期", typeof(string));
            Data.Columns.Add("试件编号", typeof(string));
            Data.Columns.Add("试件尺寸", typeof(string));
            Data.Columns.Add("试验项目", typeof(string));
            Data.Columns.Add("报告编号", typeof(string));
            Data.Columns.Add("委托编号", typeof(string));

            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select * from sys_biz_reminder_stadiumData where Date='");
            sql_select.Append(Date.ToShortDateString());
            sql_select.Append("'");

            if (!IsAdmin && FolderCode != "")
            {
                sql_select.Append(" and ModelCode like '");
                sql_select.Append(FolderCode);
                sql_select.Append("____'");
            }

            sql_select.Append(" order by Date desc,F_SJBH desc");

            //logger.Error(sql_select.ToString());

            DataTable TableData = GetDataTable(sql_select.ToString());
            if (TableData != null)
            {
                foreach (DataRow DataRow in TableData.Rows)
                {
                    DataRow Row = Data.NewRow();
                    Row["ID"] = DataRow["ID"];
                    Row["ModelCode"] = DataRow["ModelCode"];
                    Row["ModelIndex"] = DataRow["ModelIndex"];
                    Row["DateSpan"] = DataRow["DateSpan"];
                    //Row["ItemId"] = DataRow["F_ItemId"];
                    Row["名称"] = DataRow["F_Name"];
                    Row["批号"] = DataRow["F_PH"];
                    Row["制件日期"] = DataRow["F_ZJRQ"];
                    Row["试件编号"] = DataRow["F_SJBH"];
                    Row["试件尺寸"] = DataRow["F_SJSize"];
                    Row["试验项目"] = DataRow["F_SYXM"];
                    Row["报告编号"] = DataRow["F_BGBH"];
                    Row["委托编号"] = DataRow["F_WTBH"];
                    Data.Rows.Add(Row);
                }
            }

            return Data;
        }

        public Boolean UpdateLabStadiumList(DataTable Data, DateTime Date)
        {
            Boolean Result = true;

            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select * from sys_biz_reminder_stadiumData where Date='");
            sql_select.Append(Date.ToShortDateString());
            sql_select.Append("'");

            DataTable TableData = GetDataTable(sql_select.ToString());
            if (TableData != null)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    DataRow DataRow;
                    DataRow[] DataRows = TableData.Select("ID='" + Row["ID"].ToString() + "' and Date='" + Date.ToShortDateString() + "'");
                    if (DataRows.Length == 0)
                    {
                        DataRow = TableData.NewRow();
                        DataRow["ID"] = Row["ID"];
                        DataRow["Date"] = Date.ToShortDateString();
                        DataRow["SCTS"] = DateTime.Now.ToString();
                        TableData.Rows.Add(DataRow);
                    }
                    else
                    {
                        DataRow = DataRows[0];
                    }

                    DataRow["ModelCode"] = Row["ModelCode"];
                    DataRow["ModelIndex"] = Row["ModelIndex"];
                    DataRow["DateSpan"] = Row["DateSpan"];
                    DataRow["F_ItemId"] = Row["ItemId"];
                    DataRow["F_Name"] = Row["名称"];
                    DataRow["F_PH"] = Row["批号"];
                    DataRow["F_ZJRQ"] = Row["制件日期"];
                    DataRow["F_SJBH"] = Row["试件编号"];
                    DataRow["F_SJSize"] = Row["试件尺寸"];
                    DataRow["F_SYXM"] = Row["试验项目"];
                    DataRow["F_BGBH"] = Row["报告编号"];
                    DataRow["F_WTBH"] = Row["委托编号"];
                }

                try
                {
                    int r = Update(TableData);
                    Result = (r == 1);
                }
                catch(Exception ex)
                {
                    logger.Error(string.Format("UpdateLabStadiumList出错：{0}", ex.Message));
                }
            }

            return Result;
        }

        
    }
}
