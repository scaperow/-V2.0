using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BizCommon;
using Yqun.BO.ReminderManager;
using Yqun.Data.DataBase;
using System.IO;
using System.Xml.Serialization;

namespace Yqun.BO.BusinessManager
{
    public class ModelDataManager : BOBase
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        DataFlagManager DataFlagManager = new DataFlagManager();
        DataReportVerification ReportVerification = new DataReportVerification();
        DataCopyHandler DataCopyHandler = new DataCopyHandler();
        ItemConditionManager ItemConditionManager = new ItemConditionManager();
        DataModificationManager DataModificationManager = new DataModificationManager();
        StadiumManager stadiumManager = new StadiumManager();

        //获得台账数据
        public DataSet GetData(String ModuleCode, List<ModuleField> ModuleFields, String Filter, List<ModuleField> GroupInfo, int ItemCount, int PageIndex)
        {
            return null;
            Dictionary<String, List<String>> Sql_Fields = new Dictionary<String, List<String>>();
            List<String> Sql_Tables = new List<String>();
            foreach (ModuleField modelField in ModuleFields)
            {
                String TableName = string.Format("[{0}]", modelField.TableName.Trim('[', ']', ' '));
                if (!Sql_Tables.Contains(TableName))
                {
                    Sql_Tables.Add(TableName);
                }

                if (!Sql_Fields.ContainsKey(modelField.TableName.Trim()))
                {
                    Sql_Fields.Add(modelField.TableName.Trim(), new List<string>());
                }

                if (!Sql_Fields[modelField.TableName.Trim()].Contains(modelField.Contents))
                {
                    Sql_Fields[modelField.TableName.Trim()].Add(modelField.Contents);
                }
            }

            List<String> Sql_Wheres = new List<String>();
            for (int i = 1; i < Sql_Tables.Count; i++)
            {
                String PrevTable = Sql_Tables[i - 1];
                String Table = Sql_Tables[i];

                Sql_Wheres.Add(PrevTable + ".ID = " + Table + ".ID");
            }

            if (!string.IsNullOrEmpty(Filter))
                Sql_Wheres.Add(Filter);

            DataSet DataSet = new DataSet();
            if (Sql_Tables.Count > 0)
            {
                StringBuilder Sql_Command = new StringBuilder();
                StringBuilder Sql_IDs = new StringBuilder();

                String OrderBy = Sql_Tables[0] + ".SCTS desc";
                foreach (ModuleField mf in GroupInfo)
                {
                    OrderBy += ",[" + mf.TableName + "]." + mf.Contents + " desc";
                }

                Sql_Command.Append("select ");
                Sql_Command.Append("ROW_NUMBER() OVER(Order By ");
                Sql_Command.Append(OrderBy);
                Sql_Command.Append(") AS rowNum,");
                Sql_Command.Append(Sql_Tables[0] + ".ID");
                Sql_Command.Append(" from ");
                Sql_Command.Append(string.Join(",", Sql_Tables.ToArray()));

                Sql_Command.Append(" where ");
                Sql_Command.Append(Sql_Tables[0] + ".SCPT='");
                Sql_Command.Append(ModuleCode);
                Sql_Command.Append("'");

                if (Sql_Wheres.Count > 0)
                {
                    Sql_Command.Append(" and ");
                    Sql_Command.Append(string.Join(" and ", Sql_Wheres.ToArray()));
                }

                Sql_IDs.Append("select * from (");
                Sql_IDs.Append(Sql_Command.ToString());
                Sql_IDs.Append(") as t WHERE rowNum > ");
                Sql_IDs.Append((PageIndex - 1) * ItemCount);
                Sql_IDs.Append(" and rowNum <= ");
                Sql_IDs.Append(PageIndex * ItemCount);

                DataTable Data = GetDataTable(Sql_IDs.ToString());
                if (Data != null)
                {
                    List<string> Indexs = new List<string>();
                    foreach (DataRow Row in Data.Rows)
                    {
                        Indexs.Add(Row["ID"].ToString());
                    }

                    if (Indexs.Count > 0)
                    {
                        List<String> Commands = new List<string>();
                        foreach (String tableName in Sql_Tables)
                        {
                            List<string> sql_Fields = Sql_Fields[tableName.Trim('[', ']')];

                            StringBuilder Sql_Select = new StringBuilder();
                            Sql_Select.Append("select ID,SCPT,SCTS,SCCT,SCCS");

                            if (!tableName.EndsWith(TestRoomBasicInformation.拌和站基本情况登记表) ||
                                !tableName.EndsWith(TestRoomBasicInformation.试验人员技术档案) ||
                                !tableName.EndsWith(TestRoomBasicInformation.试验室仪器设备汇总表) ||
                                !tableName.EndsWith(TestRoomBasicInformation.试验室综合情况登记表))
                            {
                                Sql_Select.Append(",");
                                Sql_Select.Append(string.Join(",", sql_Fields.ToArray()));
                            }
                            else if (sql_Fields.Count > 0)
                            {
                                Sql_Select.Append(",");
                                Sql_Select.Append(string.Join(",", sql_Fields.ToArray()));
                            }

                            Sql_Select.Append(" from ");
                            Sql_Select.Append(tableName);
                            Sql_Select.Append(" where ID in ('");
                            Sql_Select.Append(string.Join("','", Indexs.ToArray()));
                            Sql_Select.Append("') order by scts desc");

                            Commands.Add(Sql_Select.ToString());
                        }

                        DataSet = GetDataSet(Commands.ToArray());
                    }
                }
            }

            return DataSet;
        }

        /// <summary>
        /// 获得台账数据条数（2013-07-31，解决数据显示和数据条数不相符问题）
        /// </summary>
        /// <param name="ModuleCode"></param>
        /// <param name="ModuleFields">显示字段集合</param>
        /// <returns></returns>
        public int GetCount(String ModuleCode, List<ModuleField> ModuleFields)
        {
            int Result = 0;
            return Result;
            List<String> Sql_Tables = new List<String>();
            foreach (ModuleField modelField in ModuleFields)
            {
                String TableName = string.Format("[{0}]", modelField.TableName.Trim('[', ']', ' '));
                if (!Sql_Tables.Contains(TableName))
                {
                    Sql_Tables.Add(TableName);
                }
            }

            StringBuilder Sql_Select = new StringBuilder();
            Sql_Select.Append("select Count(1) from ");
            Sql_Select.AppendFormat("{0}", Sql_Tables[0]);
            Sql_Select.AppendFormat(" where {0}.scpt='", Sql_Tables[0]);
            Sql_Select.Append(ModuleCode);
            Sql_Select.Append("' ");
            

            try
            {
                Result = Convert.ToInt32(ExcuteScalar(Sql_Select.ToString()));
            }
            catch
            {
            }

            return Result;
        }

        //public int GetCount(TableDefineInfo ExtentDataSchema, String ModuleCode)
        //{
        //    int Result = 0;

        //    StringBuilder Sql_Select = new StringBuilder();
        //    Sql_Select.Append("select Count(*) from [");
        //    Sql_Select.Append(ExtentDataSchema.Name);
        //    Sql_Select.Append("] where scpt='");
        //    Sql_Select.Append(ModuleCode);
        //    Sql_Select.Append("'");

        //    try
        //    {
        //        Result = Convert.ToInt32(ExcuteScalar(Sql_Select.ToString()));
        //    }
        //    catch
        //    {
        //    }

        //    return Result;
        //}



        //获取多条模板的数据
        public DataSet GetData(List<TableDefineInfo> TableSchemas, TableDefineInfo ExtentDataSchema, String[] DataID, String DataCode)
        {
            return null;
            List<String> Commands = new List<string>();
            foreach (TableDefineInfo Table in TableSchemas)
            {
                if (Table == null)
                    continue;

                StringBuilder Sql_Select = new StringBuilder();
                Sql_Select.Append("select ID,SCPT,SCTS,SCCT,SCCS,");
                Sql_Select.Append(DataTableSchema.GetFieldList(Table));
                Sql_Select.Append(" from [");
                Sql_Select.Append(Table.Name);
                Sql_Select.Append("] where ID in ('");
                Sql_Select.Append(string.Join("','", DataID));
                Sql_Select.Append("')");

                if (DataCode != "")
                {
                    Sql_Select.Append(" and ");
                    Sql_Select.Append(" SCPT='");
                    Sql_Select.Append(DataCode);
                    Sql_Select.Append("'");
                }

                Commands.Add(Sql_Select.ToString());
            }

            //表外字段
            StringBuilder Sql_Select2 = new StringBuilder();
            Sql_Select2.Append("select ID,SCPT,SCTS,SCCT,SCCS");
            if (ExtentDataSchema.FieldInfos.Count > 0)
            {
                Sql_Select2.Append(",");
                Sql_Select2.Append(DataTableSchema.GetFieldList(ExtentDataSchema));
            }
            Sql_Select2.Append(" from [");
            Sql_Select2.Append(ExtentDataSchema.Name);
            Sql_Select2.Append("] where ID in ('");
            Sql_Select2.Append(string.Join("','", DataID));
            Sql_Select2.Append("')");

            if (DataCode != "")
            {
                Sql_Select2.Append(" and ");
                Sql_Select2.Append(" SCPT='");
                Sql_Select2.Append(DataCode);
                Sql_Select2.Append("'");
            }

            Commands.Add(Sql_Select2.ToString());

            DataSet DataSet = GetDataSet(Commands.ToArray());
            if (DataSet == null)
                DataSet = new DataSet();

            return DataSet;
        }

        //获得一条模板的数据
        public DataSet GetData(List<TableDefineInfo> TableSchemas, TableDefineInfo ExtentDataSchema, String DataID, String DataCode)
        {
            return null;
            List<String> Commands = new List<string>();
            foreach (TableDefineInfo Table in TableSchemas)
            {
                if (Table == null)
                    continue;

                StringBuilder Sql_Select = new StringBuilder();
                Sql_Select.Append("select ID,SCPT,SCTS,SCCT,SCCS,");
                Sql_Select.Append(DataTableSchema.GetFieldList(Table));
                Sql_Select.Append(" from [");
                Sql_Select.Append(Table.Name);
                Sql_Select.Append("] where ID ='");
                Sql_Select.Append(DataID);
                Sql_Select.Append("' and ");
                Sql_Select.Append(" SCPT='");
                Sql_Select.Append(DataCode);
                Sql_Select.Append("'");

                Commands.Add(Sql_Select.ToString());
            }

            //表外字段
            StringBuilder Sql_Select2 = new StringBuilder();
            Sql_Select2.Append("select ID,SCPT,SCTS,SCCT,SCCS");
            if (ExtentDataSchema.FieldInfos.Count > 0)
            {
                Sql_Select2.Append(",");
                Sql_Select2.Append(DataTableSchema.GetFieldList(ExtentDataSchema));
            }
            Sql_Select2.Append(" from [");
            Sql_Select2.Append(ExtentDataSchema.Name);
            Sql_Select2.Append("] where ID ='");
            Sql_Select2.Append(DataID);
            Sql_Select2.Append("' and ");
            Sql_Select2.Append(" SCPT='");
            Sql_Select2.Append(DataCode);
            Sql_Select2.Append("'");

            Commands.Add(Sql_Select2.ToString());

            DataSet DataSet = GetDataSet(Commands.ToArray());
            if (DataSet == null)
                DataSet = new DataSet();

            return DataSet;
        }

        //获得模板的全部数据
        public DataSet GetData(List<TableDefineInfo> TableSchemas, TableDefineInfo ExtentDataSchema, String DataCode)
        {
            return null;
            List<String> Commands = new List<string>();
            foreach (TableDefineInfo Table in TableSchemas)
            {
                if (Table == null)
                    continue;

                StringBuilder Sql_Select = new StringBuilder();
                Sql_Select.Append("select ID,SCPT,SCTS,SCCT,SCCS,");
                Sql_Select.Append(DataTableSchema.GetFieldList(Table));
                Sql_Select.Append(" from [");
                Sql_Select.Append(Table.Name);
                Sql_Select.Append("] where SCPT='");
                Sql_Select.Append(DataCode);
                Sql_Select.Append("'");

                Commands.Add(Sql_Select.ToString());
            }

            //表外字段
            StringBuilder Sql_Select2 = new StringBuilder();
            Sql_Select2.Append("select ID,SCPT,SCTS,SCCT,SCCS");
            if (ExtentDataSchema.FieldInfos.Count > 0)
            {
                Sql_Select2.Append(",");
                Sql_Select2.Append(DataTableSchema.GetFieldList(ExtentDataSchema));
            }
            Sql_Select2.Append(" from [");
            Sql_Select2.Append(ExtentDataSchema.Name);
            Sql_Select2.Append("] where SCPT='");
            Sql_Select2.Append(DataCode);
            Sql_Select2.Append("'");

            Commands.Add(Sql_Select2.ToString());

            DataSet DataSet = GetDataSet(Commands.ToArray());
            if (DataSet == null)
                DataSet = new DataSet();

            return DataSet;
        }

        //删除与模板相关的数据
        public Boolean DeleteData(List<TableDefineInfo> TableSchemas, TableDefineInfo ExtentDataSchema, String ModuleCode)
        {
            Boolean Result = false;
            return Result;
            List<String> Commands = new List<string>();
            foreach (TableDefineInfo Table in TableSchemas)
            {
                if (Table == null)
                    continue;

                StringBuilder sql_Delete = new StringBuilder();
                sql_Delete.Append("delete from [");
                sql_Delete.Append(Table.Name);
                sql_Delete.Append("] where ");
                sql_Delete.Append("SCPT='");
                sql_Delete.Append(ModuleCode);
                sql_Delete.Append("'");

                Commands.Add(sql_Delete.ToString());
            }

            //表外字段
            StringBuilder sql_Delete2 = new StringBuilder();
            sql_Delete2.Append("delete from [");
            sql_Delete2.Append(ExtentDataSchema.Name);
            sql_Delete2.Append("] where ");
            sql_Delete2.Append("SCPT='");
            sql_Delete2.Append(ModuleCode);
            sql_Delete2.Append("'");

            //报告上传标记
            StringBuilder sql_Delete3 = new StringBuilder();
            sql_Delete3.Append("delete from sys_biz_DataUpload where SCPT='");
            sql_Delete3.Append(ModuleCode);
            sql_Delete3.Append("'");

            //报告不合格检测项
            StringBuilder sql_Delete4 = new StringBuilder();
            sql_Delete4.Append("delete from sys_biz_reminder_evaluateData where ModelCode='");
            sql_Delete4.Append(ModuleCode);
            sql_Delete4.Append("'");

            //龄期提醒数据
            StringBuilder sql_Delete5 = new StringBuilder();
            sql_Delete5.Append("delete from sys_biz_reminder_stadiumData where ModelCode='");
            sql_Delete5.Append(ModuleCode);
            sql_Delete5.Append("'");

            IDbConnection Connection = GetConntion();
            Transaction Transaction = new Transaction(Connection);

            try
            {
                int r = ExcuteCommand(sql_Delete2.ToString(), Transaction);
                Result = (r == 1);

                foreach (String command in Commands)
                {
                    r = ExcuteCommand(command, Transaction);
                    Result = Result && (r == 1);
                }

                r = ExcuteCommand(sql_Delete3.ToString(), Transaction);
                Result = Result && (r == 1);

                r = ExcuteCommand(sql_Delete4.ToString(), Transaction);
                Result = Result && (r == 1);

                r = ExcuteCommand(sql_Delete5.ToString(), Transaction);
                Result = Result && (r == 1);

                if (Result)
                    Transaction.Commit();
                else
                    Transaction.Rollback();
            }
            catch
            {
                Transaction.Rollback();
            }

            return Result;
        }

        //删除与模板相关的数据
        public Boolean DeleteData(List<TableDefineInfo> TableSchemas, TableDefineInfo ExtentDataSchema, String modelID, String DataID, String ModelCode)
        {
            Boolean Result = false;
            return Result;
            //删除时的操作日志; 
            DataSet ds = GetData(TableSchemas, ExtentDataSchema, DataID, ModelCode);
            String deleteLogSql = DataModificationManager.GetDeleteLogSql(modelID, ds);
            Boolean flag = false;
            //logger.Error("current record:InProject.Description=" + Yqun.Common.ContextCache.ApplicationContext.Current.InProject.Description);
            List<String> Commands = new List<string>();
            foreach (TableDefineInfo Table in TableSchemas)
            {
                if (Table == null)
                    continue;

                StringBuilder sql_Delete = new StringBuilder();
                sql_Delete.Append("delete from [");
                sql_Delete.Append(Table.Name);
                sql_Delete.Append("] where ");
                sql_Delete.Append("SCPT='");
                sql_Delete.Append(ModelCode);
                sql_Delete.Append("' and ID='");
                sql_Delete.Append(DataID);
                sql_Delete.Append("'");

                Commands.Add(sql_Delete.ToString());
            }

            //表外字段
            StringBuilder sql_Delete2 = new StringBuilder();
            sql_Delete2.Append("delete from [");
            sql_Delete2.Append(ExtentDataSchema.Name);
            sql_Delete2.Append("] where ");
            sql_Delete2.Append("SCPT='");
            sql_Delete2.Append(ModelCode);
            sql_Delete2.Append("' and ID='");
            sql_Delete2.Append(DataID);
            sql_Delete2.Append("'");

            //报告上传标记
            StringBuilder sql_Delete3 = new StringBuilder();
            sql_Delete3.Append("delete from sys_biz_DataUpload where SCPT='");
            sql_Delete3.Append(ModelCode);
            sql_Delete3.Append("' and ID='");
            sql_Delete3.Append(DataID);
            sql_Delete3.Append("'");

            //报告不合格检测项
            StringBuilder sql_Delete4 = new StringBuilder();
            sql_Delete4.Append("update sys_biz_reminder_evaluateData set AdditionalQualified=2, QualifiedTime=getdate() where ModelCode='");
            sql_Delete4.Append(ModelCode);
            sql_Delete4.Append("' and ID='");
            sql_Delete4.Append(DataID);
            sql_Delete4.Append("'");

            //龄期提醒数据
            StringBuilder sql_Delete5 = new StringBuilder();
            sql_Delete5.Append("delete from sys_biz_reminder_stadiumData where ModelCode='");
            sql_Delete5.Append(ModelCode);
            sql_Delete5.Append("' and DataID='");
            sql_Delete5.Append(DataID);
            sql_Delete5.Append("'");

            //如果是监理的平行资料，删除平行记录
            StringBuilder sql_Delete6 = new StringBuilder();
            sql_Delete6.Append("DELETE FROM dbo.biz_px_relation WHERE PXDataID='");
            sql_Delete6.Append(DataID);
            sql_Delete6.Append("'");

            IDbConnection Connection = GetConntion();
            Transaction Transaction = new Transaction(Connection);

            try
            {
                int r = ExcuteCommand(sql_Delete2.ToString(), Transaction);
                Result = (r == 1);

                foreach (String command in Commands)
                {
                    r = ExcuteCommand(command, Transaction);
                    Result = Result && (r == 1);
                }

                r = ExcuteCommand(sql_Delete3.ToString(), Transaction);
                Result = Result && (r == 1);

                r = ExcuteCommand(sql_Delete4.ToString(), Transaction);
                Result = Result && (r == 1);

                r = ExcuteCommand(sql_Delete5.ToString(), Transaction);
                Result = Result && (r == 1);

                ExcuteCommand(sql_Delete6.ToString(), Transaction);
                
                if (Result)
                {
                    Transaction.Commit();
                    flag = true;
                }
                else
                    Transaction.Rollback();
            }
            catch
            {
                Transaction.Rollback();
            }

            if (flag)
            {
                ExcuteCommand(deleteLogSql);
            }

            return Result;
        }

        //更新资料数据，并验证试验报告是否合格
        public Boolean UpdateData(String ModelIndex, DataSet dataset, Boolean IsSaveOperation)
        {
            Boolean Result = true;
            return Result;
            try
            {
                List<string> UpdatedIndexList = new List<string>();
                List<string> UpdatedCodeList = new List<string>();

                DataSet changedDataSet = dataset.Copy();
                DataTable Data = dataset.Tables[0];

                foreach (DataRow Row in Data.Rows)
                {
                    UpdatedIndexList.Add(Row["ID"].ToString());
                    UpdatedCodeList.Add(Row["SCPT"].ToString());
                }

                object r = Update(dataset);
                Result = (Convert.ToInt32(r) == 1);

                Result = Result && DataFlagManager.UpdateDataFlagInfo(UpdatedIndexList, UpdatedCodeList);

                //验证试验报告是否合格,保存修改记录; IsSaveOperation=True为保存或者更新；
                if (IsSaveOperation)
                {
                    //判断是否是复制
                    if (!IsCopy(ModelIndex, dataset))
                    {
                        //判断试验报告是否合格
                        ReportVerification.Evaluation(UpdatedIndexList, ModelIndex, changedDataSet);
                        //将报告日期的数据迁移到表外数据表中
                        DataCopyHandler.Function(UpdatedIndexList, ModelIndex, changedDataSet);
                    }
                    //保存试验报告的修改日志
                    DataModificationManager.saveDataModificationLog(ModelIndex, changedDataSet);
                }
                //初始化龄期数据
                stadiumManager.InitStadium(ModelIndex, changedDataSet);
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("UpdateData(String ModelIndex, DataSet dataset, Boolean IsSaveOperation),原因是{0}", ex.Message));
            }

            return Result;
        }

        /// <summary>
        /// 校验是否唯一
        /// </summary>
        /// <param name="TestRoomCode"></param>
        /// <param name="DataID"></param>
        /// <param name="Field"></param>
        /// <param name="FieldValue"></param>
        /// <returns></returns>
        ///<remarks>
        ///
        /// 通过复制新建资料
        /// Select Count(*) From [表名字] Where ID = [ID值]  Return 1
        /// Select Count(*) From [表名字] Where ID = [ID值] and 试验室编码=[试验室编码] and 委托编号=[委托编号]  Return 0
        /// Select Count(*) From [表名字] Where试验室编码= [试验室编码] and 委托编号=[委托编号]  Return 0
        /// 
        /// 通过新建资料菜单新建资料
        /// Select Count(*) From [表名字] Where ID = [ID值]  Return 0
        /// Select Count(*) From [表名字] Where ID = [ID值] and 试验室编码=[试验室编码] and 委托编号=[委托编号]  Return 0
        /// Select Count(*) From [表名字] Where试验室编码 = [试验室编码] and 委托编号=[委托编号]  Return 0
        /// 
        /// 如果编辑资料时，委托编号不为空，设为只读模式
        /// 编辑资料时不修改委托编号
        /// Select Count(*) From [表名字] Where ID = [ID值]  Return 1
        /// Select Count(*) From [表名字] Where ID = [ID值] and试验室编码=[试验室编码] and 委托编号=[委托编号]  Return 1
        /// Select Count(*) From [表名字] Where试验室编码= [试验室编码] and 委托编号=[委托编号]  Return 1
        /// 
        /// Count0
        /// Count1
        /// Count2
        /// </remarks>
        public Boolean IsUniqueField(String TestRoomCode, String DataID, FieldDefineInfo Field, object FieldValue)
        {
            Boolean Result = false;

            if (FieldValue == null || FieldValue == DBNull.Value || FieldValue.ToString() == "")
            {
                logger.Error(string.Format("记录ID {0} 的记录中字段名称 {1}是唯一的", DataID, Field.Description));
                return true;
            }

            StringBuilder sql_select_0 = new StringBuilder();
            sql_select_0.Append("select Count(*) from [");
            sql_select_0.Append(Field.TableInfo.Name);
            sql_select_0.Append("] where ID='");
            sql_select_0.Append(DataID);
            sql_select_0.Append("'");

            StringBuilder sql_select_2 = new StringBuilder();
            sql_select_2.Append("select Count(*) from [");
            sql_select_2.Append(Field.TableInfo.Name);
            sql_select_2.Append("] where scpt like '");
            sql_select_2.Append(TestRoomCode);
            sql_select_2.Append("____' and ");
            sql_select_2.Append(Field.FieldName);
            sql_select_2.Append(" = ");

            if (Field.FieldType.Description == FieldType.CheckBox.Description ||
                Field.FieldType.Description == FieldType.DateTime.Description ||
                Field.FieldType.Description == FieldType.DownList.Description ||
                Field.FieldType.Description == FieldType.HyperLink.Description ||
                Field.FieldType.Description == FieldType.Mask.Description ||
                Field.FieldType.Description == FieldType.RichText.Description ||
                Field.FieldType.Description == FieldType.Text.Description ||
                Field.FieldType.Description == FieldType.LongText.Description)
            {
                sql_select_2.Append(string.Format("'{0}'", FieldValue.ToString()));
            }
            else
            {
                sql_select_2.Append(FieldValue.ToString());
            }

            StringBuilder sql_select_1 = new StringBuilder();
            sql_select_1.Append(sql_select_2.ToString());
            sql_select_1.Append(" and ID='");
            sql_select_1.Append(DataID);
            sql_select_1.Append("'");

            try
            {
                int Count0 = Convert.ToInt32(ExcuteScalar(sql_select_0.ToString()));
                int Count1 = Convert.ToInt32(ExcuteScalar(sql_select_1.ToString()));
                int Count2 = Convert.ToInt32(ExcuteScalar(sql_select_2.ToString()));

                //logger.Info(sql_select_0.ToString());
                //logger.Info(sql_select_1.ToString());
                //logger.Info(sql_select_2.ToString());

                Result = ((Count0 == 1 && Count1 == 0 && Count2 == 0) ||
                         (Count0 == 0 && Count1 == 0 && Count2 == 0) ||
                         (Count0 == 1 && Count1 == 1 && Count2 == 1));
            }
            catch
            {
            }

            return Result;
        }

        /// <summary>
        /// 暂时不使用，SCCS同时用做提交申请时记录ID的作用
        /// </summary>
        /// <param name="ExtentDataSchema"></param>
        /// <param name="RelationID"></param>
        /// <returns></returns>
        public Boolean HasPingXingData(TableDefineInfo ExtentDataSchema, String RelationID)
        {
            Boolean Result = false;

            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select Count(*) from [");
            sql_select.Append(ExtentDataSchema.Name);
            sql_select.Append("] where SCCS='");
            sql_select.Append(RelationID);
            sql_select.Append("'");

            try
            {
                int Count = Convert.ToInt32(ExcuteScalar(sql_select.ToString()));
                Result = (Count >= 1);
            }
            catch
            {
            }

            return Result;
        }

        public Boolean HasPingXingData(String RelationID)
        {
            Boolean Result = false;

            String sql = String.Format("SELECT COUNT(1) FROM dbo.biz_px_relation WHERE SGDataID='{0}'", RelationID);

            try
            {
                int Count = Convert.ToInt32(ExcuteScalar(sql));
                Result = (Count >= 1);
            }
            catch
            {
            }

            return Result;
        }

        public void NewPingXingRelation(String dataID, String pxingID, String sgTestRoomCode)
        {
            String sql = String.Format(@"INSERT INTO dbo.biz_px_relation
                                                ( SGDataID ,
                                                  PXDataID ,
                                                  SGTestRoomCode ,
                                                  PXTestRoomCode ,
                                                  PXTime )
                                     VALUES  ( '{0}' ,
                                                  '{1}' ,
                                                  '{2}' ,
                                                  '{3}' ,
                                                  GETDATE() )",
                      dataID, pxingID, sgTestRoomCode, Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code);
            ExcuteCommand(sql);
        }

        public Boolean UpdateExtentFields(String ModelCode, String TableName, String FieldName, String DataID, String Value)
        {
            Boolean Result = false;

            StringBuilder sql_update = new StringBuilder();
            sql_update.Append("update ");
            sql_update.Append(TableName);
            sql_update.Append(" set ");
            sql_update.Append(FieldName);
            sql_update.Append(" = '");
            sql_update.Append(Value.Trim());
            sql_update.Append("' where ID='");
            sql_update.Append(DataID);
            sql_update.Append("' and SCPT='");
            sql_update.Append(ModelCode);
            sql_update.Append("'");

            try
            {
                //logger.Error(sql_update.ToString());
                int r = ExcuteCommand(sql_update.ToString());
                Result = (r == 1);
                if (FieldName.ToLower() == "dataname")
                {
                    UpdateStadiumDataName(DataID, Value.Trim());
                }
            }
            catch (Exception ex)
            {
                logger.Error("ModelDataManager.UpdateExtentFields:" + ex.Message);
            }

            return Result;
        }

        private void UpdateStadiumDataName(String id, String dataName)
        {
            String value = dataName.Replace("'", "''");
            String updateSql = String.Format("UPDATE dbo.sys_biz_reminder_stadiumData SET F_Name='{0}' WHERE DataID='{1}'",
                value, id);
            ExcuteCommand(updateSql);
        }

        private Boolean IsCopy(String ModelIndex, DataSet dataset)
        {
            Boolean flag = false;
            try
            {
                DataTable extTable = dataset.Tables["[biz_norm_extent_" + ModelIndex + "]"];
                if (extTable.Rows[0]["SCCS"] != DBNull.Value)
                {
                    String sccs = extTable.Rows[0]["SCCS"].ToString();
                    String sql = String.Format("SELECT * FROM dbo.sys_biz_DataModification WHERE ID='{0}'", sccs);
                    DataTable dt = GetDataTable(sql);
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        flag = true;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("is copy: " + ex.Message);
            }

            return flag;
        }

        /// <summary>
        /// 通过模板ID得到与模板相关表的数据内容xml
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public String GetModuleRelationData(String id)
        {
            String result = "";
            try
            {
                //生成biz module对象
                ModuleXMLEntity module = new ModuleXMLEntity();
                module.ID = id;
                // 增加查询条件 Scdel=0     2013-10-19
                String sql = String.Format("SELECT * FROM dbo.sys_biz_Module WHERE Scdel=0 and ID='{0}'", id);
                DataTable dt = GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    module.SCTS = dt.Rows[0]["SCTS"].ToString();
                    module.Description = dt.Rows[0]["Description"].ToString();
                    module.CatlogCode = dt.Rows[0]["CatlogCode"].ToString();
                    module.Sheets = dt.Rows[0]["Sheets"].ToString();
                    module.ExtentSheet = dt.Rows[0]["ExtentSheet"].ToString();
                    module.SheetsList = new List<ModuleXMLSheets>();
                    module.CrossSheetFormulasList = new List<ModuleXMLCrossSheetFormulas>();
                    module.ModuleViewList = new List<ModuleXMLModuleView>();

                    //导出表外表信息
                    String extTableName = "biz_norm_extent_" + id;
                    // 增加查询条件 Scdel=0     2013-10-19
                    sql = String.Format("SELECT * FROM dbo.sys_tables WHERE Scdel=0 and TABLENAME='{0}'", extTableName);
                    dt = GetDataTable(sql);
                    ModuleXMLExtTable extTable = new ModuleXMLExtTable();
                    extTable.ColumnList = new List<ModuleXMLColumn>();
                    ModuleXMLTables extSysTable = new ModuleXMLTables()
                    {
                        TABLENAME = extTableName
                    };
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        extSysTable.ID = dt.Rows[0]["ID"].ToString();
                        extSysTable.Description = dt.Rows[0]["Description"].ToString();
                        extSysTable.SCPT = dt.Rows[0]["SCPT"].ToString();
                        extSysTable.SCTS = dt.Rows[0]["SCTS"].ToString();
                        extSysTable.TABLETYPE = dt.Rows[0]["TABLETYPE"].ToString();
                    }
                    extTable.TableEntity = extSysTable;

                    // 增加查询条件 Scdel=0     2013-10-19
                    sql = String.Format("SELECT * FROM dbo.sys_Columns WHERE Scdel=0 and TABLENAME='{0}'", extTableName);
                    dt = GetDataTable(sql);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ModuleXMLColumn extColumn = new ModuleXMLColumn()
                            {
                                TABLENAME = extTableName,
                                ID = dt.Rows[i]["ID"].ToString(),
                                Description = dt.Rows[i]["DESCRIPTION"].ToString(),
                                SCPT = dt.Rows[i]["SCPT"].ToString(),
                                SCTS = dt.Rows[i]["SCTS"].ToString(),
                                ColName = dt.Rows[i]["COLNAME"].ToString(),
                                ColType = dt.Rows[i]["COLTYPE"].ToString(),
                                IsKeyField = dt.Rows[i]["IsKeyField"] == DBNull.Value ? 0 : (bool.Parse(dt.Rows[i]["IsKeyField"].ToString()) ? 1 : 0)
                            };
                            extTable.ColumnList.Add(extColumn);
                        }
                    }

                    //循环生成sheet
                    String[] sheetIDs = module.Sheets.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var sheetID in sheetIDs)
                    {
                        ModuleXMLSheets sheet = new ModuleXMLSheets() { ID = sheetID };
                        module.SheetsList.Add(sheet);
                        //增加查询条件 Scdel=0    2012-10-15
                        sql = String.Format("SELECT * FROM dbo.sys_biz_Sheet WHERE Scdel=0 and ID='{0}'", sheetID);
                        dt = GetDataTable(sql);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            sheet.SCTS = dt.Rows[0]["SCTS"].ToString();
                            sheet.CatlogCode = dt.Rows[0]["CatlogCode"].ToString();
                            sheet.Description = dt.Rows[0]["Description"].ToString();
                            sheet.DataTable = dt.Rows[0]["DataTable"].ToString();
                            sheet.SheetStyle = dt.Rows[0]["SheetStyle"].ToString();
                            sheet.ColumnList = new List<ModuleXMLColumn>();
                            sheet.DataAreaList = new List<ModuleXMLDataArea>();

                            //生成表sys_tables  
                            //增加查询条件Scdel=0     2013-10-19
                            sql = String.Format("SELECT * FROM dbo.sys_tables WHERE Scdel=0 and TABLENAME='{0}'", sheet.DataTable);
                            dt = GetDataTable(sql);

                            ModuleXMLTables table = new ModuleXMLTables()
                            {
                                TABLENAME = sheet.DataTable
                            };
                            if (dt != null && dt.Rows.Count > 0)
                            {

                                table.ID = dt.Rows[0]["ID"].ToString();
                                table.Description = dt.Rows[0]["Description"].ToString();
                                table.SCPT = dt.Rows[0]["SCPT"].ToString();
                                table.SCTS = dt.Rows[0]["SCTS"].ToString();
                                table.TABLETYPE = dt.Rows[0]["TABLETYPE"].ToString();
                            }
                            sheet.TableEntity = table;

                            //在sheet 中生成column
                            //增加查询条件Scdel=0     2013-10-19
                            sql = String.Format("SELECT * FROM dbo.sys_Columns WHERE Scdel=0 and TABLENAME='{0}'", sheet.DataTable);
                            dt = GetDataTable(sql);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    ModuleXMLColumn column = new ModuleXMLColumn()
                                    {
                                        TABLENAME = sheet.DataTable,
                                        ID = dt.Rows[i]["ID"].ToString(),
                                        Description = dt.Rows[i]["DESCRIPTION"].ToString(),
                                        SCPT = dt.Rows[i]["SCPT"].ToString(),
                                        SCTS = dt.Rows[i]["SCTS"].ToString(),
                                        ColName = dt.Rows[i]["COLNAME"].ToString(),
                                        ColType = dt.Rows[i]["COLTYPE"].ToString(),
                                        IsKeyField = dt.Rows[i]["IsKeyField"] == DBNull.Value ? 0 : (bool.Parse(dt.Rows[i]["IsKeyField"].ToString()) ? 1 : 0)
                                    };
                                    sheet.ColumnList.Add(column);
                                }
                            }

                            //在sheet 中生成DataArea
                            //增加查询条件Scdel=0     2013-10-19
                            sql = String.Format("SELECT * FROM dbo.sys_biz_DataArea WHERE Scdel=0 and SheetID = '{0}'", sheetID);
                            dt = GetDataTable(sql);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    ModuleXMLDataArea dataArea = new ModuleXMLDataArea()
                                    {
                                        ColumnName = dt.Rows[i]["ColumnName"].ToString(),
                                        ID = dt.Rows[i]["ID"].ToString(),
                                        SheetID = dt.Rows[i]["SheetID"].ToString(),
                                        SCTS = dt.Rows[i]["SCTS"].ToString(),
                                        Range = dt.Rows[i]["Range"].ToString(),
                                        TableName = dt.Rows[i]["TableName"].ToString()
                                    };
                                    sheet.DataAreaList.Add(dataArea);
                                }
                            }
                        }
                    }

                    //生成跨表公式
                    //增加查询条件Scdel=0     2013-10-19
                    sql = String.Format("SELECT * FROM dbo.sys_biz_CrossSheetFormulas WHERE Scdel=0 and ModelIndex='{0}'", id);
                    dt = GetDataTable(sql);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ModuleXMLCrossSheetFormulas formulas = new ModuleXMLCrossSheetFormulas()
                            {
                                ID = dt.Rows[i]["ID"].ToString(),
                                Formula = dt.Rows[i]["Formula"].ToString(),
                                ModuleID = id,
                                SCTS = dt.Rows[i]["SCTS"].ToString(),
                                SheetID = dt.Rows[i]["SheetIndex"].ToString(),
                                ColumnIndex = dt.Rows[i]["ColumnIndex"].ToString(),
                                RowIndex = dt.Rows[i]["RowIndex"].ToString()
                            };
                            module.CrossSheetFormulasList.Add(formulas);
                        }
                    }

                    //生成系统台账设置
                    //增加查询条件Scdel=0     2013-10-19
                    sql = String.Format("SELECT * FROM sys_moduleview WHERE Scdel=0 and ModuleID='{0}'", id);
                    dt = GetDataTable(sql);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ModuleXMLModuleView moduleView = new ModuleXMLModuleView()
                            {
                                ID = dt.Rows[i]["ID"].ToString(),
                                ModuleID = id,
                                SCTS = dt.Rows[i]["SCTS"].ToString(),
                                ContentFieldType = dt.Rows[i]["ContentFieldType"].ToString(),
                                Contents = dt.Rows[i]["Contents"].ToString(),
                                ContentText = dt.Rows[i]["ContentText"].ToString(),
                                ContentType = dt.Rows[i]["ContentType"].ToString(),
                                Description = dt.Rows[i]["Description"].ToString(),
                                DisplayStyle = dt.Rows[i]["DisplayStyle"].ToString(),
                                ModuleCode = dt.Rows[i]["ModuleCode"].ToString(),
                                TableName = dt.Rows[i]["TableName"].ToString(),
                                TableText = dt.Rows[i]["TableText"].ToString()
                            };
                            if (dt.Rows[i]["ColumnWidth"] != DBNull.Value)
                            {
                                moduleView.ColumnWidth = float.Parse(dt.Rows[i]["ColumnWidth"].ToString());
                            }
                            if (dt.Rows[i]["IsEdit"] != DBNull.Value)
                            {
                                moduleView.IsEdit = bool.Parse(dt.Rows[i]["IsEdit"].ToString()) ? 1 : 0;
                            }
                            if (dt.Rows[i]["IsNull"] != DBNull.Value)
                            {
                                moduleView.IsNull = bool.Parse(dt.Rows[i]["IsNull"].ToString()) ? 1 : 0;
                            }
                            if (dt.Rows[i]["IsVisible"] != DBNull.Value)
                            {
                                moduleView.IsVisible = bool.Parse(dt.Rows[i]["IsVisible"].ToString()) ? 1 : 0;
                            }
                            if (dt.Rows[i]["ForeColor"] != DBNull.Value)
                            {
                                moduleView.ForeColor = int.Parse(dt.Rows[i]["ForeColor"].ToString());
                            }
                            if (dt.Rows[i]["BgColor"] != DBNull.Value)
                            {
                                moduleView.BgColor = int.Parse(dt.Rows[i]["BgColor"].ToString());
                            }
                            if (dt.Rows[i]["OrderIndex"] != DBNull.Value)
                            {
                                moduleView.OrderIndex = int.Parse(dt.Rows[i]["OrderIndex"].ToString());
                            }

                            module.ModuleViewList.Add(moduleView);
                        }
                    }

                    Type type = module.GetType();
                    XmlSerializer sz = new XmlSerializer(type);
                    MemoryStream ms = new MemoryStream();
                    sz.Serialize(ms, module);
                    result = Encoding.UTF8.GetString(ms.ToArray());
                    ms.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("导出模板出错(" + id + "): " + ex.Message);
                result = "";
            }
            return result;
        }

        /// <summary>
        /// 导入模板数据
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public String ImportModule(ModuleXMLEntity module)
        {
            String result = "";
            if (module != null)
            {
                // 增加查询条件 Scdel=0     2013-10-19
                String sql = String.Format("SELECT * FROM dbo.sys_biz_Module WHERE Scdel=0 and ID='{0}'", module.ID);
                DataTable dt = GetDataTable(sql);
                DataRow Row = null;
                object r = null;
                if (dt != null && dt.Rows.Count == 0)
                {
                    Row = dt.NewRow();
                    dt.Rows.Add(Row);
                    Row["ID"] = module.ID;
                }
                else
                {
                    Row = dt.Rows[0];
                    result += "存在表sys_biz_Module: " + module.Description;
                }

                try
                {
                    Row["Description"] = module.Description;
                    Row["SCTS"] = DateTime.Now.ToString();
                    Row["CatlogCode"] = module.CatlogCode;
                    Row["Sheets"] = module.Sheets;
                    Row["ExtentSheet"] = module.ExtentSheet;
                    r = Update(dt);
                    result += "导入表sys_biz_Module成功: " + module.Description;
                }
                catch (Exception ex)
                {
                    logger.Error("导入sys_biz_Module报错：" + module.Description + " | " + ex.Message);
                    result = "导入sys_biz_Module报错：" + module.Description + " | " + ex.Message;
                }

                DataTableManager dtManager = new DataTableManager();

                //插入表外表信息
                if (module.ExtTable != null)
                {
                    //增加查询条件Scdel=0     2013-10-19
                    sql = String.Format("SELECT * FROM dbo.sys_tables WHERE Scdel=0 and TABLENAME='{0}'", module.ExtTable.TableEntity.TABLENAME);
                    dt = GetDataTable(sql);
                    if (dt != null && dt.Rows.Count == 0)
                    {
                        Row = dt.NewRow();
                        dt.Rows.Add(Row);
                        Row["ID"] = module.ExtTable.TableEntity.ID;
                    }
                    else
                    {
                        Row = dt.Rows[0];
                        result += " \n存在sys_tables：" + module.ExtTable.TableEntity.TABLENAME;
                    }
                    try
                    {
                        Row["SCPT"] = module.ExtTable.TableEntity.SCPT;
                        Row["SCTS"] = DateTime.Now.ToString();
                        Row["DESCRIPTION"] = module.ExtTable.TableEntity.Description;
                        Row["TABLENAME"] = module.ExtTable.TableEntity.TABLENAME;
                        Row["TABLETYPE"] = module.ExtTable.TableEntity.TABLETYPE;
                        r = Update(dt);
                        result += " \n导入表sys_tables成功: " + module.ExtTable.TableEntity.TABLENAME;
                    }
                    catch (Exception ex)
                    {
                        logger.Error("导入sys_tables报错：" + module.ExtTable.TableEntity.TABLENAME + " | " + ex.Message);
                        result += " \n导入sys_tables报错：" + module.ExtTable.TableEntity.TABLENAME + " | " + ex.Message;
                    }

                    if (module.ExtTable.ColumnList != null)
                    {
                        //增加查询条件Scdel=0     2013-10-19
                        //sql = String.Format("DELETE FROM dbo.sys_Columns WHERE TABLENAME='{0}'", module.ExtTable.TableEntity.TABLENAME);
                        sql = String.Format("Update dbo.sys_Columns Set Scts_1=Getdate(),Scdel=1 WHERE TABLENAME='{0}'", module.ExtTable.TableEntity.TABLENAME);
                        ExcuteCommand(sql);
                        //增加查询条件Scdel=0     2013-10-19
                        sql = String.Format("SELECT * FROM dbo.sys_Columns WHERE Scdel=0 and TABLENAME='{0}'", module.ExtTable.TableEntity.TABLENAME);
                        dt = GetDataTable(sql);
                        if (dt != null && dt.Rows.Count == 0)
                        {
                            foreach (var column in module.ExtTable.ColumnList)
                            {
                                Row = dt.NewRow();
                                dt.Rows.Add(Row);
                                try
                                {
                                    Row["ID"] = column.ID;
                                    Row["SCPT"] = column.SCPT;
                                    Row["SCTS"] = DateTime.Now.ToString();
                                    Row["DESCRIPTION"] = column.Description;
                                    Row["TABLENAME"] = column.TABLENAME;
                                    Row["COLNAME"] = column.ColName;
                                    Row["COLTYPE"] = column.ColType;
                                    Row["IsKeyField"] = column.IsKeyField;

                                    Row["SCTS_1"] = DateTime.Now.ToString();

                                }
                                catch (Exception ex)
                                {
                                    logger.Error("导入sys_Columns报错：" + column.Description + " | " + ex.Message);
                                    result += " \n导入sys_Columns报错：" + column.Description + " | " + ex.Message;
                                }
                            }
                            try
                            {
                                r = Update(dt);
                                result += " \n导入表sys_Columns成功: " + module.ExtTable.TableEntity.TABLENAME;
                            }
                            catch (Exception ex)
                            {
                                logger.Error("导入sys_Columns报错： " + ex.Message);
                                result += " \n导入sys_Columns报错： " + ex.Message;
                            }
                        }
                        else
                        {
                            result += " \n存在sys_Columns：" + module.ExtTable.TableEntity.TABLENAME;
                        }
                    }
                }

                if (module.SheetsList != null)
                {
                    foreach (var sheet in module.SheetsList)
                    {
                        //增加查询条件 Scdel=0    2012-10-15
                        sql = String.Format("SELECT * FROM dbo.sys_biz_Sheet WHERE Scdel=0 and ID='{0}'", sheet.ID);
                        dt = GetDataTable(sql);
                        //插入sys_biz_Sheet表
                        if (dt != null && dt.Rows.Count == 0)
                        {
                            Row = dt.NewRow();
                            dt.Rows.Add(Row);
                            Row["ID"] = sheet.ID;
                        }
                        else
                        {
                            Row = dt.Rows[0];
                            result += " \n存在sys_biz_Sheet：" + sheet.DataTable;
                        }

                        try
                        {
                            Row["Description"] = sheet.Description;
                            Row["SCTS"] = DateTime.Now.ToString();
                            Row["CatlogCode"] = sheet.CatlogCode;
                            Row["DataTable"] = sheet.DataTable;
                            Row["SheetStyle"] = sheet.SheetStyle;
                            r = Update(dt);
                            result += " \n导入表sys_biz_Sheet成功: " + sheet.Description;
                        }
                        catch (Exception ex)
                        {
                            logger.Error("导入sys_biz_Sheet报错：" + sheet.Description + " | " + ex.Message);
                            result += " \n导入sys_biz_Sheet报错：" + sheet.Description + " | " + ex.Message;
                        }

                        //插入sys_tables表
                        //增加查询条件Scdel=0     2013-10-19
                        sql = String.Format("SELECT * FROM dbo.sys_tables WHERE Scdel=0 and TABLENAME='{0}'", sheet.DataTable);
                        dt = GetDataTable(sql);
                        if (dt != null && dt.Rows.Count == 0)
                        {
                            Row = dt.NewRow();
                            dt.Rows.Add(Row);
                            Row["ID"] = sheet.TableEntity.ID;
                        }
                        else
                        {
                            Row = dt.Rows[0];
                            result += " \n存在sys_tables：" + sheet.TableEntity.TABLENAME;
                        }
                        try
                        {
                            Row["SCPT"] = sheet.TableEntity.SCPT;
                            Row["SCTS"] = DateTime.Now.ToString();
                            Row["DESCRIPTION"] = sheet.TableEntity.Description;
                            Row["TABLENAME"] = sheet.TableEntity.TABLENAME;
                            Row["TABLETYPE"] = sheet.TableEntity.TABLETYPE;
                            r = Update(dt);
                            result += " \n导入表sys_tables成功: " + sheet.TableEntity.TABLENAME;
                        }
                        catch (Exception ex)
                        {
                            logger.Error("导入sys_tables报错：" + sheet.TableEntity.TABLENAME + " | " + ex.Message);
                            result += " \n导入sys_tables报错：" + sheet.TableEntity.TABLENAME + " | " + ex.Message;
                        }

                        //插入ColumnList
                        if (sheet.ColumnList != null)
                        {
                            //增加查询条件Scdel=0     2013-10-19
                            //sql = String.Format("DELETE FROM dbo.sys_Columns WHERE TABLENAME='{0}'", sheet.DataTable);
                            sql = String.Format("Update dbo.sys_Columns Set Scts_1=Getdate(),Scdel=1 WHERE TABLENAME='{0}'", sheet.DataTable);
                            ExcuteCommand(sql);
                            //增加查询条件Scdel=0     2013-10-19
                            sql = String.Format("SELECT * FROM dbo.sys_Columns WHERE Scdel=0 and TABLENAME='{0}'", sheet.DataTable);
                            dt = GetDataTable(sql);
                            if (dt != null && dt.Rows.Count == 0)
                            {
                                foreach (var column in sheet.ColumnList)
                                {
                                    Row = dt.NewRow();
                                    dt.Rows.Add(Row);
                                    try
                                    {
                                        Row["ID"] = column.ID;
                                        Row["SCPT"] = column.SCPT;
                                        Row["SCTS"] = DateTime.Now.ToString();
                                        Row["DESCRIPTION"] = column.Description;
                                        Row["TABLENAME"] = column.TABLENAME;
                                        Row["COLNAME"] = column.ColName;
                                        Row["COLTYPE"] = column.ColType;
                                        Row["IsKeyField"] = column.IsKeyField;

                                    }
                                    catch (Exception ex)
                                    {
                                        logger.Error("导入sys_Columns报错：" + column.Description + " | " + ex.Message);
                                        result += " \n导入sys_Columns报错：" + column.Description + " | " + ex.Message;
                                    }
                                }
                                try
                                {
                                    r = Update(dt);
                                    result += " \n导入表sys_Columns成功: " + sheet.DataTable;
                                }
                                catch (Exception ex)
                                {
                                    logger.Error("导入sys_Columns报错： " + ex.Message);
                                    result += " \n导入sys_Columns报错： " + ex.Message;
                                }
                            }
                            else
                            {
                                result += " \n存在sys_Columns：" + sheet.DataTable;
                            }
                        }

                        //插入数据区
                        if (sheet.DataAreaList != null)
                        {
                            //增加查询条件Scdel=0     2013-10-19
                            //sql = String.Format("DELETE FROM dbo.sys_biz_DataArea WHERE SheetID = '{0}'", sheet.ID);
                            sql = String.Format("Update dbo.sys_biz_DataArea Set Scts_1=Getdate(),Scdel=1 WHERE SheetID = '{0}'", sheet.ID);
                            ExcuteCommand(sql);
                            //增加查询条件Scdel=0     2013-10-19
                            sql = String.Format("SELECT * FROM dbo.sys_biz_DataArea WHERE Scdel=0 and SheetID = '{0}'", sheet.ID);
                            dt = GetDataTable(sql);
                            if (dt != null && dt.Rows.Count == 0)
                            {
                                foreach (var dataArea in sheet.DataAreaList)
                                {
                                    Row = dt.NewRow();
                                    dt.Rows.Add(Row);
                                    try
                                    {
                                        Row["ID"] = dataArea.ID;
                                        Row["SCTS"] = DateTime.Now.ToString();
                                        Row["SheetID"] = dataArea.SheetID;
                                        Row["TableName"] = dataArea.TableName;
                                        Row["ColumnName"] = dataArea.ColumnName;
                                        Row["Range"] = dataArea.Range;
                                    }
                                    catch (Exception ex)
                                    {
                                        logger.Error("导入sys_biz_DataArea报错：" + dataArea.ID + " | " + ex.Message);
                                        result += " \n导入sys_biz_DataArea报错：" + dataArea.ID + " | " + ex.Message;
                                    }
                                }
                                try
                                {
                                    r = Update(dt);
                                    result += " \n导入表sys_biz_DataArea成功: " + sheet.DataTable;
                                }
                                catch (Exception ex)
                                {
                                    logger.Error("导入sys_biz_DataArea报错 | " + ex.Message);
                                    result += " \n导入sys_biz_DataArea报错：| " + ex.Message;
                                }
                            }
                            else
                            {
                                result += " \n存在sys_Columns, sheetID=" + sheet.Description;
                            }
                        }
                        //生成或同步表单表结构
                        try
                        {
                            dtManager.CreateDataTable(sheet.DataTable);
                        }
                        catch (Exception ex)
                        {
                            logger.Error("生成表单表报错：" + sheet.DataTable + " | " + ex.Message);
                            result += " \n生成表单表报错：" + sheet.DataTable + " | " + ex.Message;
                        }
                    }
                }

                //插入跨表公式
                if (module.CrossSheetFormulasList != null)
                {
                    //增加查询条件Scdel=0     2013-10-19
                    //sql = String.Format("DELETE FROM dbo.sys_biz_CrossSheetFormulas WHERE ModelIndex='{0}'", module.ID);
                    sql = String.Format("Update dbo.sys_biz_CrossSheetFormulas Set Scts_1=Getdate(),Scdel=1 WHERE ModelIndex='{0}'", module.ID);
                    ExcuteCommand(sql);
                    //增加查询条件Scdel=0     2013-10-19
                    sql = String.Format("SELECT * FROM dbo.sys_biz_CrossSheetFormulas WHERE ModelIndex='{0}'", module.ID);
                    dt = GetDataTable(sql);
                    if (dt != null && dt.Rows.Count == 0)
                    {
                        foreach (var formula in module.CrossSheetFormulasList)
                        {
                            DataRow[] rows = dt.Select("ID='" + formula.ID + "'");
                            if (rows.Length > 0)
                            {
                                rows[0]["SCTS"] = DateTime.Now.ToString();
                                rows[0]["ModelIndex"] = formula.ModuleID;
                                rows[0]["SheetIndex"] = formula.SheetID;
                                rows[0]["RowIndex"] = formula.RowIndex;
                                rows[0]["ColumnIndex"] = formula.ColumnIndex;
                                rows[0]["Formula"] = formula.Formula;
                                rows[0]["Scts_1"] = DateTime.Now.ToString();
                                rows[0]["Scdel"] = 1;
                            }
                            else
                            {
                                Row = dt.NewRow();
                                dt.Rows.Add(Row);
                                try
                                {
                                    Row["ID"] = formula.ID;
                                    Row["SCTS"] = DateTime.Now.ToString();

                                    Row["ModelIndex"] = formula.ModuleID;
                                    Row["SheetIndex"] = formula.SheetID;
                                    Row["RowIndex"] = formula.RowIndex;
                                    Row["ColumnIndex"] = formula.ColumnIndex;
                                    Row["Formula"] = formula.Formula;
                                    Row["Scts_1"] = DateTime.Now.ToString();
                                    Row["Scdel"] = 1;
                                }
                                catch (Exception ex)
                                {
                                    logger.Error("导入sys_biz_CrossSheetFormulas报错：" + module.Description + " | " + ex.Message);
                                    result += " \n导入sys_biz_CrossSheetFormulas报错：" + module.Description + " | " + ex.Message;
                                }
                            }
                        }
                        try
                        {
                            r = Update(dt);
                            result += " \n导入跨表公式表sys_biz_CrossSheetFormulas成功";
                        }
                        catch (Exception ex)
                        {
                            logger.Error("导入sys_biz_CrossSheetFormulas报错：" + module.Description + " | " + ex.Message);
                            result += "\n 导入sys_biz_CrossSheetFormulas报错：" + module.Description + " | " + ex.Message;
                        }
                    }
                    else
                    {
                        result += " \n存在sys_biz_CrossSheetFormulas,module=" + module.Description;
                    }
                }

                //插入台账设置
                if (module.ModuleViewList != null)
                {
                    sql = String.Format("DELETE FROM sys_moduleview WHERE ModuleID='{0}'", module.ID);
                    ExcuteCommand(sql);
                    sql = String.Format("SELECT * FROM sys_moduleview WHERE ModuleID='{0}'", module.ID);
                    dt = GetDataTable(sql);
                    if (dt != null && dt.Rows.Count == 0)
                    {
                        foreach (var moduleView in module.ModuleViewList)
                        {
                            Row = dt.NewRow();
                            dt.Rows.Add(Row);
                            try
                            {
                                Row["ID"] = moduleView.ID;
                                Row["SCTS"] = DateTime.Now.ToString();
                                Row["ModuleID"] = moduleView.ModuleID;
                                Row["ContentFieldType"] = moduleView.ContentFieldType;
                                Row["Contents"] = moduleView.Contents;
                                Row["ContentText"] = moduleView.ContentText;
                                Row["ContentType"] = moduleView.ContentType;
                                Row["Description"] = moduleView.Description;
                                Row["DisplayStyle"] = moduleView.DisplayStyle;
                                Row["ModuleCode"] = moduleView.ModuleCode;
                                Row["TableName"] = moduleView.TableName;
                                Row["TableText"] = moduleView.TableText;

                                if (moduleView.ColumnWidth != null)
                                {
                                    Row["ColumnWidth"] = moduleView.ColumnWidth;
                                }
                                if (moduleView.IsEdit != null)
                                {
                                    Row["IsEdit"] = moduleView.IsEdit;
                                }
                                if (moduleView.IsNull != null)
                                {
                                    Row["IsNull"] = moduleView.IsNull;
                                }
                                if (moduleView.IsVisible != null)
                                {
                                    Row["IsVisible"] = moduleView.IsVisible;
                                }

                                if (moduleView.ForeColor != null)
                                {
                                    Row["ForeColor"] = moduleView.ForeColor;
                                }

                                if (moduleView.BgColor != null)
                                {
                                    Row["BgColor"] = moduleView.BgColor;
                                }

                                if (moduleView.OrderIndex != null)
                                {
                                    Row["OrderIndex"] = moduleView.OrderIndex;
                                }
                            }
                            catch (Exception ex)
                            {
                                logger.Error("导入sys_moduleview报错：" + moduleView.Description + " | " + ex.Message);
                                result += " \n导入sys_moduleview报错：" + moduleView.Description + " | " + ex.Message;
                            }
                        }
                        try
                        {
                            r = Update(dt);
                            result += " \n导入台账设置表sys_moduleview成功";
                        }
                        catch (Exception ex)
                        {
                            logger.Error("导入sys_moduleview报错：| " + ex.Message);
                            result += " \n导入sys_moduleview报错： | " + ex.Message;
                        }
                    }
                    else
                    {
                        result += " \n存在sys_moduleview：" + module.Description;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 通过不合格资料数据表更新表外表中IsQualified字段
        /// </summary>
        public void UpdateIsQualified()
        {
            // 增加查询条件 Scdel=0     2013-10-19
            String sql = "select ID,Description from sys_biz_Module Where Scdel=0 ";
            DataTable Models = GetDataTable(sql);
            if (Models != null)
            {
                foreach (DataRow Row in Models.Rows)
                {
                    String Index = Row["ID"].ToString();

                    sql = "select ID from [biz_norm_extent_" + Index + "]";
                    DataTable ExtentData = GetDataTable(sql);
                    if (ExtentData != null && ExtentData.Columns.Count > 0)
                    {
                        foreach (DataRow row in ExtentData.Rows)
                        {
                            String DataID = "";
                            try
                            {
                                DataID = row["ID"].ToString();

                                sql = String.Format("select ID from sys_biz_reminder_evaluateData where ID='{0}' and AdditionalQualified=0", DataID);
                                DataTable data = GetDataTable(sql);
                                int v = (data != null && data.Rows.Count > 0 ? 0 : 1);
                                sql = "update [biz_norm_extent_" + Index + "] set IsQualified = " + v + " where ID='" + DataID + "'";
                                int r = ExcuteCommand(sql);
                                //logger.Error("更新合格字段IsQualified的返回值为" + r + ":ID=" + DataID + ";modelindex=" + Index);
                            }
                            catch (Exception ex)
                            {
                                logger.Error("更新合格字段IsQualified失败： " + ex.Message + ":ID=" + DataID + ";modelindex=" + Index);
                            }
                           
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 更新模块库中所有最模块的最近一次编辑时间到Cache表中
        /// zhangdahang 20131205
        /// </summary>
        public void UpdateModulesVision()
        {
            try
            {
                DataTable CacheTable = GetDataTable("select tableName from sys_biz_cache");
                StringBuilder sb = new StringBuilder();
                foreach (DataRow row in CacheTable.Rows)
                {
                    string tableName = row["tableName"].ToString();
                    sb.Append("update sys_biz_cache set scts_1=( select max(scts_1) from " + tableName + ") where tableName='"+tableName+"';");
                }
                ExcuteCommand(sb.ToString());
                //logger.Error(sb.ToString());
            }
            catch (Exception ex)
            {
                logger.Error("更新模块库版本失败： " + ex.Message );
            }

        }
    }
}
