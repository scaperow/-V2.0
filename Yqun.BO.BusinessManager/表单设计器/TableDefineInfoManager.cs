using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using System.Data;
using Yqun.Data.DataBase;
using FarPoint.Win.Spread;
using FarPoint.Win;
using System.Reflection;

namespace Yqun.BO.BusinessManager
{
    public class TableDefineInfoManager : BOBase
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public TableDefineInfo GetTableDefineInfo(string SheetIndex, string TableName)
        {
            if (string.IsNullOrEmpty(SheetIndex) || string.IsNullOrEmpty(TableName))
                return null;

            TableDefineInfo Info = null;

            StringBuilder Sql_Select = new StringBuilder();
            // 增加查询条件   (Scdel IS NULL or Scdel=0)  2013-10-16
            Sql_Select.Append("Select * from sys_Tables where (Scdel IS NULL or Scdel=0) And TABLENAME='");
            Sql_Select.Append(TableName);
            Sql_Select.Append("'");

            StringBuilder Sql_FieldInfos = new StringBuilder();
            //增加查询条件Scdel=0     2013-10-19
            Sql_FieldInfos.Append("Select * from sys_Columns where (Scdel IS NULL or Scdel=0) And TableName ='");
            Sql_FieldInfos.Append(TableName);
            Sql_FieldInfos.Append("' order by colname");

            StringBuilder Sql_DataArea = new StringBuilder();
            //增加查询条件Scdel=0     2013-10-19
            Sql_DataArea.Append("Select SheetID,TableName,ColumnName,Range From sys_biz_DataArea where (Scdel IS NULL or Scdel=0) And SheetID ='");
            Sql_DataArea.Append(SheetIndex);
            Sql_DataArea.Append("' and TableName ='");
            Sql_DataArea.Append(TableName);
            Sql_DataArea.Append("' order by scts");

            List<string> Sql_Commands = new List<string>();
            Sql_Commands.Add(Sql_Select.ToString());
            Sql_Commands.Add(Sql_FieldInfos.ToString());
            Sql_Commands.Add(Sql_DataArea.ToString());

            DataSet dataset = GetDataSet(Sql_Commands.ToArray());
            if (dataset != null)
            {
                DataTable TableInfo = dataset.Tables["sys_Tables"];
                DataTable FieldInfoData = dataset.Tables["sys_Columns"];
                DataTable DataAreaInfo = dataset.Tables["sys_biz_DataArea"];

                Info = new TableDefineInfo();
                Info.Name = TableName;
                Info.Description = TableInfo.Rows[0]["DESCRIPTION"].ToString();
                Info.Index = TableInfo.Rows[0]["ID"].ToString();

                foreach (DataRow row in FieldInfoData.Rows)
                {
                    FieldDefineInfo FieldInfo = new FieldDefineInfo(Info);
                    FieldInfo.Index = row["ID"].ToString();
                    FieldInfo.Description = row["DESCRIPTION"].ToString();
                    FieldInfo.FieldName = row["COLNAME"].ToString();
                    FieldInfo.IsKeyField = Convert.ToBoolean(row["IsKeyField"]);

                    String scpt = row["scpt"].ToString();
                    FieldInfo.IsNotNull = ((scpt.Length > 0 ? scpt.Substring(0, 1) : "0") == "1" ? true : false);
                    FieldInfo.IsNotCopy = ((scpt.Length > 1 ? scpt.Substring(1, 1) : "0") == "1" ? true : false);
                    FieldInfo.IsPingxing = ((scpt.Length > 2 ? scpt.Substring(2, 1) : "0") == "1" ? true : false);
                    FieldInfo.IsReadOnly = ((scpt.Length > 3 ? scpt.Substring(3, 1) : "0") == "1" ? true : false);

                    String ColType = row["COLType"].ToString();
                    FieldInfo.FieldType = FieldType.GetFieldType(ColType);

                    DataRow[] DataAreas = DataAreaInfo.Select("ColumnName='" + FieldInfo.FieldName + "'");
                    if (DataAreas.Length > 0)
                    {
                        FieldInfo.RangeInfo = DataAreas[0]["Range"].ToString();
                    }
                }
            }

            return Info;
        }

        public TableDefineInfo GetTableDefineInfo(string TableName)
        {
            if (string.IsNullOrEmpty(TableName))
                return null;

            TableDefineInfo Info = null;

            StringBuilder Sql_Select = new StringBuilder();
            // 增加查询条件   (Scdel IS NULL or Scdel=0)  2013-10-16
            Sql_Select.Append("Select * from sys_Tables where (Scdel IS NULL or Scdel=0) And TABLENAME='");
            Sql_Select.Append(TableName);
            Sql_Select.Append("'");

            StringBuilder Sql_FieldInfos = new StringBuilder();
            //增加查询条件Scdel=0     2013-10-19
            Sql_FieldInfos.Append("Select * from sys_Columns where (Scdel IS NULL or Scdel=0) And TableName ='");
            Sql_FieldInfos.Append(TableName);
            Sql_FieldInfos.Append("'");

            List<string> Sql_Commands = new List<string>();
            Sql_Commands.Add(Sql_Select.ToString());
            Sql_Commands.Add(Sql_FieldInfos.ToString());

            DataSet dataset = GetDataSet(Sql_Commands.ToArray());
            if (dataset != null)
            {
                DataTable TableInfo = dataset.Tables["sys_Tables"];
                DataTable FieldInfoData = dataset.Tables["sys_Columns"];

                Info = new TableDefineInfo();
                Info.Name = TableName;
                Info.Description = TableInfo.Rows[0]["DESCRIPTION"].ToString();
                Info.Index = TableInfo.Rows[0]["ID"].ToString();

                foreach (DataRow row in FieldInfoData.Rows)
                {
                    FieldDefineInfo FieldInfo = new FieldDefineInfo(Info);
                    FieldInfo.Index = row["ID"].ToString();
                    FieldInfo.Description = row["DESCRIPTION"].ToString();
                    FieldInfo.FieldName = row["COLNAME"].ToString();
                    FieldInfo.IsKeyField = Convert.ToBoolean(row["IsKeyField"]);

                    String scpt = row["scpt"].ToString();
                    FieldInfo.IsNotNull = ((scpt.Length > 0 ? scpt.Substring(0, 1) : "0") == "1" ? true : false);
                    FieldInfo.IsNotCopy = ((scpt.Length > 1 ? scpt.Substring(1, 1) : "0") == "1" ? true : false);
                    FieldInfo.IsPingxing = ((scpt.Length > 2 ? scpt.Substring(2, 1) : "0") == "1" ? true : false);
                    FieldInfo.IsReadOnly = ((scpt.Length > 3 ? scpt.Substring(3, 1) : "0") == "1" ? true : false);

                    String ColType = row["COLType"].ToString();
                    FieldInfo.FieldType = FieldType.GetFieldType(ColType);
                }
            }

            return Info;
        }

        public List<TableDefineInfo> GetTableDefineInfos()
        {
            List<TableDefineInfo> Infos = new List<TableDefineInfo>();

            StringBuilder sql_Select = new StringBuilder();
            // 增加查询条件   (Scdel IS NULL or Scdel=0)  2013-10-16
            sql_Select.Append("select * from sys_Tables Where (Scdel IS NULL or Scdel=0) ");
            DataTable Data = GetDataTable(sql_Select.ToString());
            foreach (DataRow row in Data.Rows)
            {
                TableDefineInfo Info = new TableDefineInfo();
                Info.Name = row["TableName"].ToString(); ;
                Info.Description = row["DESCRIPTION"].ToString();
                Info.Index = row["ID"].ToString();

                Infos.Add(Info);
            }

            return Infos;
        }

        public bool NewTableDefineInfo(TableDefineInfo Info)
        {
            if (Info == null)
                return false;

            bool Result = false;
            DataRow Row;
            //插入数据时修改Scts_1字段 为当前日期   2013-10-16
            StringBuilder Sql_Tables = new StringBuilder();
            Sql_Tables.Append("Select ID,SCTS,DESCRIPTION,TABLENAME,TABLETYPE,Scts_1 From sys_Tables Where Scdel=0 and TableName='");
            Sql_Tables.Append(Info.Name);
            Sql_Tables.Append("'");

            StringBuilder Sql_Fields = new StringBuilder();
            //增加查询条件Scdel=0     2013-10-19
            Sql_Fields.Append("Select ID,SCTS,SCPT,COLNAME,DESCRIPTION,COLTYPE,TABLENAME,IsKeyField,Scts_1 From sys_Columns Where Scdel=0 and TableName='");
            Sql_Fields.Append(Info.Name);
            Sql_Fields.Append("'");

            List<string> Sql_Commands = new List<string>();
            Sql_Commands.Add(Sql_Tables.ToString());
            Sql_Commands.Add(Sql_Fields.ToString());

            DataSet dataset = GetDataSet(Sql_Commands.ToArray());
            if (dataset != null)
            {
                DataTable TableInfo = dataset.Tables["sys_Tables"];
                DataTable FieldInfoData = dataset.Tables["sys_Columns"];

                Row = TableInfo.NewRow();
                Row["ID"] = Info.Index;
                Row["SCTS"] = DateTime.Now.ToString();
                Row["Description"] = Info.Description;
                Row["TABLENAME"] = Info.Name;
                Row["TABLETYPE"] = Info.Type;
                Row["Scts_1"] = DateTime.Now.ToString();
                TableInfo.Rows.Add(Row);

                foreach (FieldDefineInfo FieldInfo in Info.FieldInfos)
                {
                    Row = FieldInfoData.NewRow();
                    Row["ID"] = FieldInfo.Index;
                    Row["SCTS"] = DateTime.Now.ToString();
                    Row["SCPT"] = string.Concat(FieldInfo.IsNotNull ? "1" : "0", FieldInfo.IsNotCopy ? "1" : "0", FieldInfo.IsPingxing ? "1" : "0", FieldInfo.IsReadOnly ? "1" : "0");
                    Row["COLNAME"] = FieldInfo.FieldName;
                    Row["DESCRIPTION"] = FieldInfo.Description;
                    Row["COLTYPE"] = FieldInfo.FieldType.Description;
                    Row["TABLENAME"] = FieldInfo.TableInfo.Name;
                    Row["IsKeyField"] = FieldInfo.IsKeyField;
                    Row["Scts_1"] = DateTime.Now.ToString();

                    FieldInfoData.Rows.Add(Row);
                }

                IDbConnection DbConnection = GetConntion();
                Transaction Transaction = new Transaction(DbConnection);

                try
                {
                    object r = Update(dataset, Transaction);
                    Result = (Convert.ToInt32(r) == 1);

                    if (Result)
                    {
                        Transaction.Commit();
                    }
                    else
                    {
                        Transaction.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    Transaction.Rollback();

                    logger.Error(String.Format("NewTableDefineInfo(TableDefineInfo Info) 创建表信息‘sys_Tables,sys_Columns’出错，原因：{0}", ex.Message));
                    return false;
                }
            }

            return Result;
        }

        public Boolean DeleteTableDefineInfo(TableDefineInfo Info)
        {
            if (Info == null)
                return false;

            //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，用于数据同步     2013-10-15
            StringBuilder Sql_DeleteTable = new StringBuilder();
            //Sql_DeleteTable.Append("Delete From sys_Tables Where ID ='");
            //Sql_DeleteTable.Append(Info.Index);
            //Sql_DeleteTable.Append("'");
            Sql_DeleteTable.Append("Update sys_Tables set Scts_1='" + DateTime.Now + "',Scdel=1 Where ID ='");
            Sql_DeleteTable.Append(Info.Index);
            Sql_DeleteTable.Append("'");

            StringBuilder Sql_DeleteField = new StringBuilder();
            //Sql_DeleteField.Append("Delete From sys_Columns Where TableName='");
            //Sql_DeleteField.Append(Info.Name);
            //Sql_DeleteField.Append("'");
            Sql_DeleteField.Append("Update sys_Columns set Scts_1='" + DateTime.Now + "',Scdel=1 Where TableName='");
            Sql_DeleteField.Append(Info.Name);
            Sql_DeleteField.Append("'");

            List<string> Sql_Commands = new List<string>();
            Sql_Commands.Add(Sql_DeleteTable.ToString());
            Sql_Commands.Add(Sql_DeleteField.ToString());

            Boolean Result = false;

            IDbConnection DbConnection = GetConntion();
            Transaction Transaction = new Transaction(DbConnection);

            try
            {
                object r = ExcuteCommands(Sql_Commands.ToArray(), Transaction);
                int[] ints = (int[])r;
                for (int i = 0; i < ints.Length; i++)
                {
                    if (i != 0)
                    {
                        Result = Result & (Convert.ToInt32(ints[i]) == 1);
                    }
                    else
                    {
                        Result = (Convert.ToInt32(ints[i]) == 1);
                    }
                }

                if (Result)
                {
                    Transaction.Commit();
                }
                else
                {
                    Transaction.Rollback();
                }
            }
            catch (Exception ex)
            {
                Transaction.Rollback();

                logger.Error(String.Format("DeleteTableDefineInfo(TableDefineInfo Info) 删除表信息‘sys_Tables,sys_Columns’出错，原因：{0}", ex.Message));
                return false;
            }

            return Result;
        }

        public Boolean UpdateTableDefineInfo(TableDefineInfo Info)
        {
            if (Info == null)
                return false;

            Boolean Result = false;

            StringBuilder Sql_Tables = new StringBuilder();
            //增加查询条件Scdel=0     2013-10-19
            Sql_Tables.Append("Select ID,SCTS,DESCRIPTION,TABLENAME,TABLETYPE,Scts_1 From sys_Tables Where Scdel=0 and ID='");
            Sql_Tables.Append(Info.Index);
            Sql_Tables.Append("'");

            StringBuilder Sql_Fields = new StringBuilder();
            //增加查询条件Scdel=0     2013-10-19
            Sql_Fields.Append("Select ID,SCTS,SCPT,COLNAME,DESCRIPTION,COLTYPE,TABLENAME,IsKeyField,Scts_1 From sys_Columns Where Scdel=0 and TableName='");
            Sql_Fields.Append(Info.Name);
            Sql_Fields.Append("'");

            List<string> Sql_Commands = new List<string>();
            Sql_Commands.Add(Sql_Tables.ToString());
            Sql_Commands.Add(Sql_Fields.ToString());

            DataSet dataset = GetDataSet(Sql_Commands.ToArray());
            if (dataset != null)
            {
                DataTable TableInfo = dataset.Tables["sys_Tables"];
                DataTable FieldInfoData = dataset.Tables["sys_Columns"];

                if (TableInfo != null && TableInfo.Rows.Count > 0)
                {
                    DataRow Row = TableInfo.Rows[0];
                    Row["SCTS"] = DateTime.Now.ToString();
                    Row["DESCRIPTION"] = Info.Description;
                    Row["TABLENAME"] = Info.Name;
                    Row["TABLETYPE"] = Info.Type;
                    Row["Scts_1"] = DateTime.Now.ToString();
                }
                else
                {
                    DataRow Row = TableInfo.NewRow();
                    Row["ID"] = Info.Index;
                    Row["SCTS"] = DateTime.Now.ToString();
                    Row["DESCRIPTION"] = Info.Description;
                    Row["TABLENAME"] = Info.Name;
                    Row["TABLETYPE"] = Info.Type;
                    Row["Scts_1"] = DateTime.Now.ToString();
                    TableInfo.Rows.Add(Row);
                }

                foreach (FieldDefineInfo fieldInfo in Info.FieldInfos)
                {
                    DataRow[] Rows = FieldInfoData.Select("ID='" + fieldInfo.Index + "'");
                    if (Rows.Length > 0)
                    {
                        DataRow Row = Rows[0];
                        Row["SCTS"] = DateTime.Now.ToString();
                        Row["SCPT"] = string.Concat(fieldInfo.IsNotNull ? "1" : "0", fieldInfo.IsNotCopy ? "1" : "0", fieldInfo.IsPingxing ? "1" : "0", fieldInfo.IsReadOnly ? "1" : "0");
                        Row["COLNAME"] = fieldInfo.FieldName;
                        Row["DESCRIPTION"] = fieldInfo.Description;
                        Row["COLTYPE"] = fieldInfo.FieldType.Description;
                        Row["TABLENAME"] = Info.Name;
                        Row["IsKeyField"] = fieldInfo.IsKeyField;
                        Row["Scts_1"] = DateTime.Now.ToString();
                    }
                    else
                    {
                        DataRow Row = FieldInfoData.NewRow();
                        Row["ID"] = fieldInfo.Index;
                        Row["SCTS"] = DateTime.Now.ToString();
                        Row["SCPT"] = string.Concat(fieldInfo.IsNotNull ? "1" : "0", fieldInfo.IsNotCopy ? "1" : "0", fieldInfo.IsPingxing ? "1" : "0", fieldInfo.IsReadOnly ? "1" : "0");
                        Row["COLNAME"] = fieldInfo.FieldName;
                        Row["DESCRIPTION"] = fieldInfo.Description;
                        Row["COLTYPE"] = fieldInfo.FieldType.Description;
                        Row["TABLENAME"] = Info.Name;
                        Row["IsKeyField"] = fieldInfo.IsKeyField;
                        Row["Scts_1"] = DateTime.Now.ToString();
                        FieldInfoData.Rows.Add(Row);
                    }
                }

                IDbConnection DbConnection = GetConntion();
                Transaction Transaction = new Transaction(DbConnection);

                try
                {
                    object r = Update(dataset, Transaction);
                    Result = (Convert.ToInt32(r) == 1);

                    if (Result)
                    {
                        Transaction.Commit();
                    }
                    else
                    {
                        Transaction.Rollback();
                    }
                }
            catch (Exception ex)
            {
                Transaction.Rollback();

                logger.Error(String.Format("UpdateTableDefineInfo(TableDefineInfo Info) 更新表信息‘sys_Tables,sys_Columns’出错，原因：{0}", ex.Message));
                return false;
            }
            }

            return Result;
        }
    }
}
