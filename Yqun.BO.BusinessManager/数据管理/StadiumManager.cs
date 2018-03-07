using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Yqun.Common.ContextCache;
using System.Threading;
using BizCommon;

namespace Yqun.BO.BusinessManager
{
    public class StadiumManager : BOBase
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void InitStadium(String ModelIndex, DataSet dataset)
        {
            ThreadParameter p = new ThreadParameter();
            p.Data = dataset;
            p.ModelIndex = ModelIndex;
            ApplicationContext context = ApplicationContext.Current;
            p.UserName = context.UserName;
            ThreadPool.QueueUserWorkItem(new WaitCallback(Execute), p);
        }

        private void Execute(object paremeter)
        {
            ThreadParameter p = paremeter as ThreadParameter;
            DataSet dataset = p.Data;
            String modelIndex = p.ModelIndex;
            if (dataset == null)
                return;
            try
            {
                DataTable stadiumInfo = GetStadiumInfoByModelIndex(modelIndex);
                // 检查是否是设置龄期的模板
                if (stadiumInfo != null && stadiumInfo.Rows.Count > 0)
                {
                    StadiumConfig config = Newtonsoft.Json.JsonConvert.DeserializeObject<StadiumConfig>(stadiumInfo.Rows[0]["StadiumConfig"].ToString());
                    if (config != null)
                    {
                        DataTable tb = dataset.Tables["[biz_norm_extent_" + modelIndex + "]"];
                        if (tb != null && tb.Rows.Count > 0)
                        {
                            String dataID = tb.Rows[0]["ID"].ToString();
                            //DataTable stadiumData = GetStadiumDataByDataID(dataID);
                            ////检查是否该资料已经进入龄期提醒表，不考虑龄期中是否已经完成；如果已经存在，需要考虑更新是否完成；
                            //if (stadiumData == null || stadiumData.Rows.Count == 0)
                            //{
                            //    SaveStadiumData(dataset, config, modelIndex);//保存龄期
                            //}
                            //else
                            //{
                            //    //如果数据已经存在于龄期表，检查制件日期是否变更，并根据配置更新是否完成的字段
                            //    UpdateStadiumData(dataset, config, modelIndex, stadiumData);
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("龄期提醒：error" + ex.Message);
            }
        }

        private DataTable GetStadiumInfoByModelIndex(String modelIndex)
        {
            String sql = String.Format("SELECT * FROM dbo.sys_biz_reminder_stadiumInfo WHERE ID='{0}'  AND IsActive=1", modelIndex);
            DataTable tb = GetDataTable(sql);

            return tb;
        }


        private void SaveStadiumData(DataSet dataset, StadiumConfig config, String modelIndex)
        {
            DataTable extTable = dataset.Tables["[biz_norm_extent_" + modelIndex + "]"];
            String dataID = extTable.Rows[0]["ID"].ToString();
            String modelCode = extTable.Rows[0]["SCPT"].ToString();
            String fName = extTable.Rows[0]["DataName"].ToString();
            String fPH = config.fPH == null ? "" : dataset.Tables[config.fPH.TableName].Rows[0][config.fPH.ColumnName].ToString().Replace("'", "''");
            String fZJRQ = config.fZJRQ == null ? "" : dataset.Tables[config.fZJRQ.TableName].Rows[0][config.fZJRQ.ColumnName].ToString();
            String fSJBH = config.fSJBH == null ? "" : dataset.Tables[config.fSJBH.TableName].Rows[0][config.fSJBH.ColumnName].ToString().Replace("'", "''");
            String fSJSize = config.fSJSize == null ? "" : dataset.Tables[config.fSJSize.TableName].Rows[0][config.fSJSize.ColumnName].ToString();
            String fAdded = config.fAdded == null ? "" : dataset.Tables[config.fAdded.TableName].Rows[0][config.fAdded.ColumnName].ToString();
            String fBGBH = config.fBGBH == null ? "" : dataset.Tables[config.fBGBH.TableName].Rows[0][config.fBGBH.ColumnName].ToString();
            String fWTBH = config.fWTBH == null ? "" : dataset.Tables[config.fWTBH.TableName].Rows[0][config.fWTBH.ColumnName].ToString();

            if (String.IsNullOrEmpty(fZJRQ))
            {
                //无制件日期则不保存
                //logger.Error("无制件日期，无法保存龄期提醒数据: id=" + dataID + "; fName=" + fName + "; fBGBH=" + fBGBH);
                return;
            }
            DateTime zjrq;
            if (!DateTime.TryParse(fZJRQ, out zjrq))
            {
                logger.Error("制件日期格式不对 " + fZJRQ);
                return;
            }

            foreach (var item in config.DayList)
            {
                Int32 days = item.Days;
                if (item.IsParameterDays)
                {
                    String pDays = dataset.Tables[item.PDays.TableName].Rows[0][item.PDays.ColumnName].ToString().ToLower();
                    if (!int.TryParse(pDays.Replace("d", ""), out days))
                    {
                        continue;
                    }
                    //混凝土三级配特殊处理
                    switch (days)
                    {
                        case 1:
                            item.ItemID = "1";
                            item.ItemName = "混凝土抗压1天";
                            break;
                        case 3:
                            item.ItemID = "92";
                            item.ItemName = "混凝土抗压3天";
                            break;
                        case 7:
                            item.ItemID = "93";
                            item.ItemName = "混凝土抗压7天";
                            break;
                        case 28:
                            item.ItemID = "94";
                            item.ItemName = "混凝土抗压28天";
                            break;
                        case 56:
                            item.ItemID = "95";
                            item.ItemName = "混凝土抗压56天";
                            break;
                        default:
                            break;
                    }
                }
                //if ((DateTime.Now - zjrq).Days > days)
                //{//提醒的时间已经过了
                //    logger.Error("提醒的时间已经过了 zjrq=" + zjrq + ", days=" + days);
                //    continue;
                //}
                String insertSql = String.Format(@"INSERT INTO dbo.sys_biz_reminder_stadiumData
                                        ( DataID ,
                                          Scts ,
                                          ModelCode ,
                                          ModelIndex ,
                                          DateSpan ,
                                          F_Name ,
                                          F_ItemId ,
                                          F_PH ,
                                          F_ZJRQ ,
                                          F_SJBH ,
                                          F_SJSize ,
                                          F_SYXM ,
                                          F_BGBH ,
                                          F_WTBH,
                                          F_Added
                                        )
                                VALUES  ( '{0}' , '{1}' ,'{2}' ,'{3}' ,'{4}' ,'{5}' ,'{6}' ,'{7}' ,
                                          '{8}' , '{9}' , '{10}' , '{11}' , '{12}' , '{13}','{14}' )",
                                 dataID, DateTime.Now.ToString(), modelCode, modelIndex,
                                 days, fName, item.ItemID, fPH, fZJRQ, fSJBH, fSJSize, item.ItemName, fBGBH, fWTBH, fAdded);
                //logger.Error("插入龄期提醒： " + insertSql);
                ExcuteCommand(insertSql);
            }

        }

        private void UpdateStadiumData(DataSet dataset, StadiumConfig config, String modelIndex, DataTable stadiumData)
        {
            DataTable extTable = dataset.Tables["[biz_norm_extent_" + modelIndex + "]"];
            String dataID = extTable.Rows[0]["ID"].ToString();
            String modelCode = extTable.Rows[0]["SCPT"].ToString();
            String fName = extTable.Rows[0]["DataName"].ToString();
            String fPH = config.fPH == null ? "" : dataset.Tables[config.fPH.TableName].Rows[0][config.fPH.ColumnName].ToString().Replace("'", "''");
            String fZJRQ = config.fZJRQ == null ? "" : dataset.Tables[config.fZJRQ.TableName].Rows[0][config.fZJRQ.ColumnName].ToString();
            String fSJBH = config.fSJBH == null ? "" : dataset.Tables[config.fSJBH.TableName].Rows[0][config.fSJBH.ColumnName].ToString().Replace("'", "''");
            String fSJSize = config.fSJSize == null ? "" : dataset.Tables[config.fSJSize.TableName].Rows[0][config.fSJSize.ColumnName].ToString();
            String fAdded = config.fAdded == null ? "" : dataset.Tables[config.fAdded.TableName].Rows[0][config.fAdded.ColumnName].ToString();
            String fBGBH = config.fBGBH == null ? "" : dataset.Tables[config.fBGBH.TableName].Rows[0][config.fBGBH.ColumnName].ToString();
            String fWTBH = config.fWTBH == null ? "" : dataset.Tables[config.fWTBH.TableName].Rows[0][config.fWTBH.ColumnName].ToString();

            DateTime zjrq;
            if (!DateTime.TryParse(fZJRQ, out zjrq))
            {
                //制件日期格式不对
                logger.Error("制件日期格式不对 " + fZJRQ);
                return;
            }

            foreach (DataRow row in stadiumData.Rows)
            {
                if (row["F_IsDone"] != DBNull.Value && row["F_IsDone"].ToString() == "1")
                {
                    //取龄期提醒的数据，可能是多条，过滤已经完成的。
                    logger.Error("取龄期提醒的数据，可能是多条，过滤已经完成的 F_IsDone=" + row["F_IsDone"].ToString());
                    continue;
                }

                foreach (var item in config.DayList)
                {
                    Int32 days = item.Days;
                    if (item.IsParameterDays)
                    {
                        String pDays = dataset.Tables[item.PDays.TableName].Rows[0][item.PDays.ColumnName].ToString().ToLower();
                        if (!int.TryParse(pDays.Replace("d", ""), out days))
                        {
                            continue;
                        }
                        //混凝土三级配特殊处理
                        switch (days)
                        {
                            case 1:
                                item.ItemID = "1";
                                item.ItemName = "混凝土抗压1天";
                                break;
                            case 3:
                                item.ItemID = "92";
                                item.ItemName = "混凝土抗压3天";
                                break;
                            case 7:
                                item.ItemID = "93";
                                item.ItemName = "混凝土抗压7天";
                                break;
                            case 28:
                                item.ItemID = "94";
                                item.ItemName = "混凝土抗压28天";
                                break;
                            case 56:
                                item.ItemID = "95";
                                item.ItemName = "混凝土抗压56天";
                                break;
                            default:
                                break;
                        }
                    }
                    String updateSql = "";
                    
                    if (item.IsParameterDays && config.DayList.Count == 1)
                    {
                        //三级配
                        DataTable dt2 = GetDataTable(String.Format("Select ID from dbo.sys_biz_reminder_stadiumData WHERE DataID='{0}' ", dataID));
                        if (dt2 != null && dt2.Rows.Count > 0)
                        {
                            updateSql = String.Format(@"UPDATE dbo.sys_biz_reminder_stadiumData SET 
                                                F_BGBH='{0}'{1},F_Name='{2}',F_PH='{3}',F_SJBH='{4}',F_ItemId='{11}', F_SYXM='{12}',
                                                F_SJSize='{5}',F_WTBH='{6}',F_ZJRQ='{7}',Scts=GETDATE(),DateSpan={9}, F_Added='{10}'
                                                WHERE DataID='{8}'",
                                                        fBGBH, "", fName, fPH, fSJBH, fSJSize, fWTBH, fZJRQ, dataID, days, fAdded, item.ItemID, item.ItemName);
                            ExcuteCommand(updateSql);
                        }
                        else
                        {
                            SaveStadiumData(dataset, config, modelIndex);
                        }
                    }
                    else
                    {

                        DataTable dt = GetDataTable(String.Format("Select ID from dbo.sys_biz_reminder_stadiumData WHERE DataID='{0}' AND DateSpan='{1}' ", dataID, days));
                        if ((dt == null || dt.Rows.Count == 0))
                        {
                            SaveStadiumData(dataset, config, modelIndex);
                        }
                        else
                        {
                            if (item.ValidInfo == null)
                            {
                                updateSql = String.Format(@"UPDATE dbo.sys_biz_reminder_stadiumData SET 
                                                F_BGBH='{0}'{1},F_Name='{2}',F_PH='{3}',F_SJBH='{4}',F_ItemId='{11}', F_SYXM='{12}',
                                                F_SJSize='{5}',F_WTBH='{6}',F_ZJRQ='{7}',Scts=GETDATE(), F_Added='{10}'
                                                WHERE DataID='{8}' AND DateSpan='{9}'",
                                                        fBGBH, "", fName, fPH, fSJBH, fSJSize, fWTBH, fZJRQ, dataID, days, fAdded, item.ItemID, item.ItemName);
                            }
                            else
                            {
                                String isDone = "null";
                                String validValue = dataset.Tables[item.ValidInfo.TableName].Rows[0][item.ValidInfo.ColumnName] == DBNull.Value ? "" :
                                    dataset.Tables[item.ValidInfo.TableName].Rows[0][item.ValidInfo.ColumnName].ToString().Trim();
                                if (validValue != "" && validValue != "/")
                                {
                                    isDone = "1";
                                }
                                updateSql = String.Format(@"UPDATE dbo.sys_biz_reminder_stadiumData SET 
                                                F_BGBH='{0}',F_IsDone={1},F_Name='{2}',F_PH='{3}',F_SJBH='{4}', F_ItemId='{11}', F_SYXM='{12}',
                                                F_SJSize='{5}',F_WTBH='{6}',F_ZJRQ='{7}',Scts=GETDATE(), F_Added='{10}'
                                                WHERE DataID='{8}' AND DateSpan='{9}'",
                                                        fBGBH, isDone, fName, fPH, fSJBH, fSJSize, fWTBH, fZJRQ, dataID, days, fAdded, item.ItemID, item.ItemName);
                            }
                            //logger.Error("更新龄期提醒： " + updateSql);
                            ExcuteCommand(updateSql);
                        }
                    }
                }
            }
        }

        private class ThreadParameter
        {
            public DataSet Data;
            public String ModelIndex;
            public String UserName;
        }

        public void MoveOldStadiumData()
        {
            ModuleManager Manager = new ModuleManager();
            TableDefineInfoManager TableManager = new TableDefineInfoManager();
            ModelDataManager DataManager = new ModelDataManager();

            DataTable tb = GetDataTable("SELECT DISTINCT a.ID FROM dbo.sys_biz_reminder_stadiumInfo a join sys_biz_Module b on a.id=b.id WHERE a.IsActive=1");
            if (tb != null)
            {
                foreach (DataRow row in tb.Rows)
                {
                    String modelIndex = row["ID"].ToString();
                    DataTable dataTb = GetDataTable("select * from [biz_norm_extent_" + modelIndex + "]");

                    if (dataTb != null)
                    {
                        foreach (DataRow dataRow in dataTb.Rows)
                        {
                            try
                            {
                                List<IndexDescriptionPair> Sheets = Manager.InitModuleInfo(modelIndex);

                                List<TableDefineInfo> TableSchemas = new List<TableDefineInfo>();
                                foreach (IndexDescriptionPair pair in Sheets)
                                {
                                    TableDefineInfo Info = TableManager.GetTableDefineInfo(pair.Index, pair.DataTable);
                                    TableSchemas.Add(Info);
                                }
                                String extentTable = "biz_norm_extent_" + modelIndex;
                                TableDefineInfo ExtentTableSchema = TableManager.GetTableDefineInfo("1", extentTable);
                                DataSet ds = DataManager.GetData(TableSchemas, ExtentTableSchema, dataRow["ID"].ToString(), dataRow["scpt"].ToString());

                                InitStadium(modelIndex, ds);
                            }
                            catch (Exception ex)
                            {
                                logger.Error("Move Old Data Error: " + ex.Message + "; ModelIndex=" + modelIndex + "; DataID=" + dataRow["ID"].ToString());
                            }
                        }
                    }

                }
            }
        }

        public void DeleteWrongStadiumData()
        {
            ModuleManager Manager = new ModuleManager();
            TableDefineInfoManager TableManager = new TableDefineInfoManager();
            ModelDataManager DataManager = new ModelDataManager();


            String modelIndex = "05d0d71b-def3-42ee-a16a-79b34de97e9b";
            DataTable dataTb = GetDataTable("select * from [biz_norm_extent_" + modelIndex + "]");

            if (dataTb != null)
            {
                foreach (DataRow dataRow in dataTb.Rows)
                {
                    try
                    {
                        DataTable dt = GetDataTable("SELECT * FROM sys_biz_reminder_stadiumData WHERE DataID = '"+
                            dataRow["ID"].ToString() + "' AND F_IsDone=0");
                        if (dt == null || dt.Rows.Count == 0)
                        {
                            continue;
                        }
                        List<IndexDescriptionPair> Sheets = Manager.InitModuleInfo(modelIndex);

                        List<TableDefineInfo> TableSchemas = new List<TableDefineInfo>();
                        foreach (IndexDescriptionPair pair in Sheets)
                        {
                            TableDefineInfo Info = TableManager.GetTableDefineInfo(pair.Index, pair.DataTable);
                            TableSchemas.Add(Info);
                        }
                        String extentTable = "biz_norm_extent_" + modelIndex;
                        TableDefineInfo ExtentTableSchema = TableManager.GetTableDefineInfo("1", extentTable);
                        DataSet ds = DataManager.GetData(TableSchemas, ExtentTableSchema, dataRow["ID"].ToString(), dataRow["scpt"].ToString());
                        Object key = ds.Tables["[biz_norm_混凝土检查试件抗压强度试验报告]"].Rows[0]["col_norm_I33"];
                        String deleteSql = "Update dbo.sys_biz_reminder_stadiumData set F_IsDone=1 WHERE F_IsDone IS NULL AND DataID='{0}' AND DateSpan={1}";
                        if (key != DBNull.Value)
                        {
                            if (key.ToString().Contains("28"))
                            {
                                ExcuteCommand(String.Format(deleteSql, dataRow["ID"].ToString(), 56));
                            }
                            else if (key.ToString().Contains("56"))
                            {
                                ExcuteCommand(String.Format(deleteSql, dataRow["ID"].ToString(), 28));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Delete Old Data Error: " + ex.Message + "; ModelIndex=" + modelIndex + "; DataID=" + dataRow["ID"].ToString());
                    }
                }
            }

        }

        /// <summary>
        /// 为西成线11标中心，删除219条资料的28d数据，并生成1条56d数据
        /// </summary>
        public void SpStadiumData()
        {
            ModuleManager Manager = new ModuleManager();
            TableDefineInfoManager TableManager = new TableDefineInfoManager();
            ModelDataManager DataManager = new ModelDataManager();


            String modelIndex = "05d0d71b-def3-42ee-a16a-79b34de97e9b";
            DataTable dataTb = GetDataTable("select * from [biz_norm_extent_" + modelIndex + "] WHERE scpt LIKE '0001001700010001%' ");
            DataTable stadiumInfo = GetStadiumInfoByModelIndex(modelIndex);
             StadiumConfig config = Newtonsoft.Json.JsonConvert.DeserializeObject<StadiumConfig>(stadiumInfo.Rows[0]["StadiumConfig"].ToString());
                 
            if (dataTb != null)
            {
                foreach (DataRow dataRow in dataTb.Rows)
                {
                    try
                    {
                        DataTable dt = GetDataTable("SELECT * FROM sys_biz_reminder_stadiumData WHERE DataID='" + dataRow["ID"].ToString() + "' AND DateSpan = 56");
                        if (dt.Rows.Count == 0)
                        {

                        List<IndexDescriptionPair> Sheets = Manager.InitModuleInfo(modelIndex);

                        List<TableDefineInfo> TableSchemas = new List<TableDefineInfo>();
                        foreach (IndexDescriptionPair pair in Sheets)
                        {
                            TableDefineInfo Info = TableManager.GetTableDefineInfo(pair.Index, pair.DataTable);
                            TableSchemas.Add(Info);
                        }
                        String extentTable = "biz_norm_extent_" + modelIndex;
                        TableDefineInfo ExtentTableSchema = TableManager.GetTableDefineInfo("1", extentTable);
                        DataSet dataset = DataManager.GetData(TableSchemas, ExtentTableSchema, dataRow["ID"].ToString(), dataRow["scpt"].ToString());

                        DataTable extTable = dataset.Tables["[biz_norm_extent_" + modelIndex + "]"];
                        String dataID = extTable.Rows[0]["ID"].ToString();

                        String modelCode = extTable.Rows[0]["SCPT"].ToString();
                        String fName = extTable.Rows[0]["DataName"].ToString();
                        String fPH = config.fPH == null ? "" : dataset.Tables[config.fPH.TableName].Rows[0][config.fPH.ColumnName].ToString().Replace("'", "''");
                        String fZJRQ = config.fZJRQ == null ? "" : dataset.Tables[config.fZJRQ.TableName].Rows[0][config.fZJRQ.ColumnName].ToString();
                        String fSJBH = config.fSJBH == null ? "" : dataset.Tables[config.fSJBH.TableName].Rows[0][config.fSJBH.ColumnName].ToString().Replace("'", "''");
                        String fSJSize = config.fSJSize == null ? "" : dataset.Tables[config.fSJSize.TableName].Rows[0][config.fSJSize.ColumnName].ToString();
                        String fAdded = config.fAdded == null ? "" : dataset.Tables[config.fAdded.TableName].Rows[0][config.fAdded.ColumnName].ToString();
                        String fBGBH = config.fBGBH == null ? "" : dataset.Tables[config.fBGBH.TableName].Rows[0][config.fBGBH.ColumnName].ToString();
                        String fWTBH = config.fWTBH == null ? "" : dataset.Tables[config.fWTBH.TableName].Rows[0][config.fWTBH.ColumnName].ToString();
                        
                            String insertSql = String.Format(@"INSERT INTO dbo.sys_biz_reminder_stadiumData
                                        ( DataID ,
                                          Scts ,
                                          ModelCode ,
                                          ModelIndex ,
                                          DateSpan ,
                                          F_Name ,
                                          F_ItemId ,
                                          F_PH ,
                                          F_ZJRQ ,
                                          F_SJBH ,
                                          F_SJSize ,
                                          F_SYXM ,
                                          F_BGBH ,
                                          F_WTBH,
                                          F_Added
                                        )
                                VALUES  ( '{0}' , '{1}' ,'{2}' ,'{3}' ,'{4}' ,'{5}' ,'{6}' ,'{7}' ,
                                          '{8}' , '{9}' , '{10}' , '{11}' , '{12}' , '{13}','{14}' )",
                                     dataID, DateTime.Now.ToString(), modelCode, modelIndex,
                                     56, fName, "95", fPH, fZJRQ, fSJBH, fSJSize, "混凝土抗压56天", fBGBH, fWTBH, fAdded);
                            ExcuteCommand(insertSql);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error("SP Old Data Error: " + ex.Message + "; ModelIndex=" + modelIndex + "; DataID=" + dataRow["ID"].ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 将现有的资料中的报告日期复制到表外数据表的BGRQ字段中
        /// </summary>
        public void CopyBGRQToExtentTable()
        {
            Boolean Result = false;

            logger.Error("开始获取所有的模板信息...");

            StringBuilder Sql_Select = new StringBuilder();
            // 增加查询条件 Scdel=0     2013-10-19
            Sql_Select.Append("Select ID,Description from sys_biz_Module Where Scdel=0 ");

            DataTable Models = GetDataTable(Sql_Select.ToString());
            if (Models != null)
            {
                logger.Error("共获取到" + Models.Rows.Count + "个模板的数据");

                foreach (DataRow Row in Models.Rows)
                {
                    String ModelIndex = Row["ID"].ToString();
                    String Description = Row["Description"].ToString();

                    logger.Error("正在获取模板‘" + Description + "’(" + ModelIndex + ") 的台账显示项“报告日期”的配置信息...");

                    StringBuilder sql_Select = new StringBuilder();
                    sql_Select.Append("Select TableName,Contents from sys_moduleview where ModuleID ='");
                    sql_Select.Append(ModelIndex);
                    sql_Select.Append("' and Description='报告日期'");

                    DataTable Table = GetDataTable(sql_Select.ToString());
                    if (Table != null && Table.Rows.Count > 0)
                    {
                        logger.Error("获取模板‘" + Description + "’(" + ModelIndex + ") 的台账显示项“报告日期”配置成功");

                        DataRow ModuleViewRow = Table.Rows[0];

                        String TableName = ModuleViewRow["TableName"].ToString();
                        String FieldName = ModuleViewRow["Contents"].ToString();

                        logger.Error("正在使用模板‘" + Description + "’(" + ModelIndex + ") 的“报告日期”配置获取所有的资料...");

                        StringBuilder sql_Select_1 = new StringBuilder();
                        sql_Select_1.Append("select ID,");
                        sql_Select_1.Append(FieldName);
                        sql_Select_1.Append(" from ");
                        sql_Select_1.Append(string.Format("[{0}]", TableName));

                        StringBuilder sql_Select_2 = new StringBuilder();
                        sql_Select_2.Append("select ID,BGRQ from ");
                        sql_Select_2.Append(string.Format("[biz_norm_extent_{0}]", ModelIndex));
                        sql_Select_2.Append(" where BGRQ is null ");
                        DataTable ReportDateTable = GetDataTable(sql_Select_1.ToString());
                        DataTable BGRQTable = GetDataTable(sql_Select_2.ToString());
                        if (ReportDateTable != null && BGRQTable != null)
                        {
                            logger.Error("使用模板‘" + Description + "’(" + ModelIndex + ") 的“报告日期”配置共获得" + ReportDateTable.Rows.Count + "条资料");

                            //foreach (DataRow ReportDateRow in ReportDateTable.Rows)
                            //{
                            //    String DataID = ReportDateRow["ID"].ToString();
                            //    object ReportDate = ReportDateRow[FieldName];

                            //    DataRow[] bgrqs = BGRQTable.Select("ID='" + DataID + "'");
                            //    if (bgrqs.Length > 0)
                            //    {
                            //        bgrqs[0]["BGRQ"] = ReportDate;
                            //    }
                            //}
                            foreach (DataRow row in BGRQTable.Rows)
                            {
                                String DataID = row["ID"].ToString();
                                DataRow[] bgrqs = ReportDateTable.Select("ID='" + DataID + "'");
                                if (bgrqs.Length > 0)
                                {
                                    object ReportDate = bgrqs[0][FieldName];
                                    row["BGRQ"] = ReportDate;
                                }
                            }
                            try
                            {
                                object r = Update(BGRQTable);
                                Result = (Convert.ToInt32(r) == 1);
                            }
                            catch (Exception ex)
                            {
                                logger.Error("更新模板‘" + Description + "’(" + ModelIndex + ") 的“BGRQ字段”时失败，原因是" + ex.Message);
                            }

                            if (Result)
                                logger.Error("更新模板‘" + Description + "’(" + ModelIndex + ") 的“BGRQ字段”时成功");
                        }
                        else
                        {
                            logger.Error("使用模板‘" + Description + "’(" + ModelIndex + ") 的“报告日期”的配置未找到资料");
                        }
                    }
                    else
                    {
                        logger.Error("未找到模板‘" + Description + "’(" + ModelIndex + ") 的台账显示项“报告日期”的配置信息");
                    }
                }
            }
        }
    }
}
