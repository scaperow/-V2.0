using System;
using System.Collections.Generic;
using System.Text;
using Yqun.Data.DataBase;
using BizCommon;
using System.Data;
using System.Reflection;

namespace Yqun.BO.BusinessManager
{
    public class SheetManager : BOBase
    {
        DataSchemaManager DataSchemaManager = new DataSchemaManager();
        DataTableManager DataTableManager = new DataTableManager();
        TableNameManager TableNameManager = new TableNameManager();

        public List<SheetConfiguration> InitSheetConfiguration(string[] Indexes)
        {
            SheetConfiguration[] Configurations = new SheetConfiguration[Indexes.Length];

            StringBuilder Sql_Sheets = new StringBuilder();
            //增加查询条件Scdel=0   2013-10-19
            Sql_Sheets.Append("Select ID,Description,CatlogCode,SheetStyle,DataTable from sys_biz_Sheet where Scdel=0 and ID in ('");
            Sql_Sheets.Append(string.Join("','", Indexes));
            Sql_Sheets.Append("')");

            StringBuilder Sql_Tables = new StringBuilder();
            //增加查询条件Scdel=0   2013-10-19
            Sql_Tables.Append("Select * from sys_Tables where Scdel=0 and TableName in (");
            Sql_Tables.Append("Select DataTable from sys_biz_Sheet where Scdel=0 and ID in ('"); 
            Sql_Tables.Append(string.Join("','", Indexes));
            Sql_Tables.Append("'))");

            StringBuilder Sql_FieldInfos = new StringBuilder();
            //增加查询条件Scdel=0   2013-10-19
            Sql_FieldInfos.Append("Select * from sys_Columns where Scdel=0 and TableName in (");
            Sql_FieldInfos.Append("Select DataTable from sys_biz_Sheet where Scdel=0 and ID in ('");
            Sql_FieldInfos.Append(string.Join("','", Indexes));
            Sql_FieldInfos.Append("'))");
            Sql_FieldInfos.Append(" order by scts");

            StringBuilder Sql_DataAreas = new StringBuilder();
            //增加查询条件Scdel=0   2013-10-19
            Sql_DataAreas.Append("Select SheetID,TableName,ColumnName,Range From sys_biz_DataArea Where Scdel=0 and SheetID in ('");
            Sql_DataAreas.Append(string.Join("','", Indexes));
            Sql_DataAreas.Append("')");

            List<string> sql_Commands = new List<string>();
            sql_Commands.Add(Sql_Sheets.ToString());
            sql_Commands.Add(Sql_Tables.ToString());
            sql_Commands.Add(Sql_FieldInfos.ToString());
            sql_Commands.Add(Sql_DataAreas.ToString());

            DataSet dataset = GetDataSet(sql_Commands.ToArray());
            if (dataset != null)
            {
                DataTable DataSheets = dataset.Tables["sys_biz_Sheet"];
                DataTable DataTables = dataset.Tables["sys_Tables"];
                DataTable DataFields = dataset.Tables["sys_Columns"];
                DataTable DataAreas = dataset.Tables["sys_biz_DataArea"];

                foreach (DataRow SheetRow in DataSheets.Rows)
                {
                    SheetConfiguration Configuration = new SheetConfiguration();
                    Configuration.Index = SheetRow["ID"].ToString();
                    Configuration.Code = SheetRow["CatlogCode"].ToString();
                    Configuration.SheetStyle = SheetRow["SheetStyle"].ToString();
                    Configuration.Description = SheetRow["Description"].ToString();

                    string DataTable = SheetRow["DataTable"].ToString();
                    if (!string.IsNullOrEmpty(DataTable))
                    {
                        DataRow[] TableRows = DataTables.Select("TableName = '" + DataTable + "'");
                        if (TableRows.Length > 0)
                        {
                            Configuration.DataTableSchema.Schema = new TableDefineInfo();
                            Configuration.DataTableSchema.Schema.Name = DataTable;
                            Configuration.DataTableSchema.Schema.Description = TableRows[0]["DESCRIPTION"].ToString();
                            Configuration.DataTableSchema.Schema.Index = TableRows[0]["ID"].ToString();

                            DataRow[] FieldRows = DataFields.Select("TableName = '" + DataTable + "'", "colname");
                            foreach (DataRow FieldRow in FieldRows)
                            {
                                FieldDefineInfo FieldInfo = new FieldDefineInfo(Configuration.DataTableSchema.Schema);
                                FieldInfo.Index = FieldRow["ID"].ToString();
                                FieldInfo.Description = FieldRow["DESCRIPTION"].ToString();
                                FieldInfo.FieldName = FieldRow["COLNAME"].ToString();
                                FieldInfo.IsKeyField = (FieldRow["IsKeyField"] != DBNull.Value ? Convert.ToBoolean(FieldRow["IsKeyField"]) : false);

                                String scpt = FieldRow["scpt"].ToString();
                                FieldInfo.IsNotNull = ((scpt.Length > 0 ? scpt.Substring(0, 1) : "0") == "1" ? true : false);
                                FieldInfo.IsNotCopy = ((scpt.Length > 1 ? scpt.Substring(1, 1) : "0") == "1" ? true : false);
                                FieldInfo.IsPingxing = ((scpt.Length > 2 ? scpt.Substring(2, 1) : "0") == "1" ? true : false);
                                FieldInfo.IsReadOnly = ((scpt.Length > 3 ? scpt.Substring(3, 1) : "0") == "1" ? true : false);

                                String ColType = FieldRow["COLType"].ToString();
                                FieldInfo.FieldType = FieldType.GetFieldType(ColType);

                                DataRow[] dataAreas = DataAreas.Select("SheetID='" + Configuration.Index + "' and TableName='" + DataTable + "' and ColumnName='" + FieldInfo.FieldName + "'");
                                if (dataAreas.Length > 0)
                                {
                                    FieldInfo.RangeInfo = dataAreas[0]["Range"].ToString();
                                }
                            }
                        }
                    }

                    int Index = Array.IndexOf(Indexes, Configuration.Index);
                    Configurations[Index] = Configuration;
                }
            }

            return new List<SheetConfiguration>(Configurations);
        }

        public List<IndexDescriptionPair> InitSheetInfo(string[] Indexes)
        {
            List<IndexDescriptionPair> pairs = new List<IndexDescriptionPair>();

            StringBuilder Sql_Sheets = new StringBuilder();
            //2013-10-27 增加查询条件 Scdel=0 Scdel为新增字段，0表示未删除，1表示已删除。
            Sql_Sheets.Append("Select ID,Scts, Description, DataTable,Scdel from sys_biz_Sheet where Scdel=0 and ID in ('");
            Sql_Sheets.Append(string.Join("','", Indexes));
            Sql_Sheets.Append("')");

            DataTable Data = GetDataTable(Sql_Sheets.ToString());
            foreach (DataRow Row in Data.Rows)
            {
                String Index = Row["ID"].ToString();
                String Scts = Row["Scts"].ToString();
                String Description = Row["Description"].ToString();
                String DataTable = Row["DataTable"].ToString();

                IndexDescriptionPair pair = new IndexDescriptionPair();
                pair.Index = Index;
                pair.Scts = Scts;
                pair.Description = Description;
                pair.DataTable = DataTable;
                pairs.Add(pair);
            }

            return pairs;
        }

        public Boolean HaveSheetConfiguration(string Index)
        {
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件Scdel=0   2013-10-19
            Sql_Select.Append("Select ID from sys_biz_Sheet where Scdel=0 and ID='");
            Sql_Select.Append(Index);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            return (Data != null && Data.Rows.Count > 0);
        }

        public bool NewSheetConfiguration(SheetConfiguration Sheet)
        {
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件Scdel=0   2013-10-19
            Sql_Select.Append("Select ID,SCTS,Scts_1,CatlogCode,Description,SheetStyle,DataTable From sys_biz_Sheet Where Scdel=0 and ID='");
            Sql_Select.Append(Sheet.Index);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null)
            {
                DataRow Row = Data.NewRow();
                Row["ID"] = Sheet.Index;
                Row["SCTS"] = DateTime.Now.ToString();
                Row["CatlogCode"] = Sheet.Code;
                Row["Description"] = Sheet.Description;
                Row["SheetStyle"] = Sheet.SheetStyle;
                Row["DataTable"] = "";

                Row["Scts_1"] = DateTime.Now.ToString();
                Data.Rows.Add(Row);
            }

            Boolean Result = false;

            try
            {
                object r = Update(Data);
                Result = (Convert.ToInt32(r) == 1);
            }
            catch
            { }

            return Result;
        }

        public bool UpdateSheetConfiguration(SheetConfiguration Sheet)
        {
            Boolean Result = false;

            StringBuilder Sql_Sheets = new StringBuilder();
            //2013-10-17  增加查询条件  Scdel=0   0未删除  1已删除
            Sql_Sheets.Append("Select ID,SCTS,CatlogCode,Description,SheetStyle,DataTable,SCTS_1,Scdel From sys_biz_Sheet Where Scdel=0 and ID='");
            Sql_Sheets.Append(Sheet.Index);
            Sql_Sheets.Append("'");

            StringBuilder Sql_DataAreas = new StringBuilder();
            //2013-10-17  增加查询条件  Scdel=0   0未删除  1已删除
            Sql_DataAreas.Append("Select ID,SCTS,SheetID,TableName,ColumnName,Range,SCTS_1,Scdel From sys_biz_DataArea Where Scdel=0 and SheetID='");
            Sql_DataAreas.Append(Sheet.Index);
            Sql_DataAreas.Append("'");

            List<string> sql_Commands = new List<string>();
            sql_Commands.Add(Sql_Sheets.ToString());
            sql_Commands.Add(Sql_DataAreas.ToString());

            DataSet dataset = GetDataSet(sql_Commands.ToArray());
            if (dataset != null)
            {
                DataTable DataSheets = dataset.Tables["sys_biz_Sheet"];
                DataTable DataAreas = dataset.Tables["sys_biz_DataArea"];

                if (DataSheets != null)
                {
                    if (DataSheets.Rows.Count > 0)
                    {
                        DataRow SheetRow = DataSheets.Rows[0];
                        SheetRow["ID"] = Sheet.Index;
                        SheetRow["SCTS"] = DateTime.Now.ToString();
                        SheetRow["CatlogCode"] = Sheet.Code;
                        SheetRow["Description"] = Sheet.Description;
                        SheetRow["SheetStyle"] = Sheet.SheetStyle;
                        SheetRow["SCTS_1"] = DateTime.Now.ToString();

                        if (Sheet.DataTableSchema.Schema != null)
                        {
                            foreach (FieldDefineInfo fieldInfo in Sheet.DataTableSchema.Schema.FieldInfos)
                            {
                                DataRow[] AreaRows = DataAreas.Select("SheetID='" + Sheet.Index + "' and TableName='" + Sheet.DataTableSchema.Schema.Name + "' and ColumnName='" + fieldInfo.FieldName + "'");
                                if (AreaRows.Length > 0)
                                {
                                    AreaRows[0]["SheetID"] = Sheet.Index;
                                    AreaRows[0]["SCTS"] = DateTime.Now.ToString();
                                    AreaRows[0]["TableName"] = Sheet.DataTableSchema.Schema.Name;
                                    AreaRows[0]["ColumnName"] = fieldInfo.FieldName;
                                    AreaRows[0]["Range"] = fieldInfo.RangeInfo;
                                    AreaRows[0]["SCTS_1"] = DateTime.Now.ToString();
                                }
                                else
                                {
                                    DataRow AreaRow = DataAreas.NewRow();
                                    AreaRow["ID"] = Guid.NewGuid().ToString();
                                    AreaRow["SCTS"] = DateTime.Now.ToString();
                                    AreaRow["SheetID"] = Sheet.Index;
                                    AreaRow["TableName"] = Sheet.DataTableSchema.Schema.Name;
                                    AreaRow["ColumnName"] = fieldInfo.FieldName;
                                    AreaRow["Range"] = fieldInfo.RangeInfo;
                                    AreaRows[0]["SCTS_1"] = DateTime.Now.ToString();
                                    DataAreas.Rows.Add(AreaRow);
                                }
                            }

                            SheetRow["DataTable"] = Sheet.DataTableSchema.Schema.Name;
                        }
                        else
                        {
                            SheetRow["DataTable"] = "";
                        }
                    }
                    else
                    {
                        DataRow SheetRow = DataSheets.NewRow();
                        SheetRow["ID"] = Sheet.Index;
                        SheetRow["SCTS"] = DateTime.Now.ToString();
                        SheetRow["CatlogCode"] = Sheet.Code;
                        SheetRow["Description"] = Sheet.Description;
                        SheetRow["SheetStyle"] = Sheet.SheetStyle;
                        SheetRow["SCTS_1"] = DateTime.Now.ToString();

                        if (Sheet.DataTableSchema.Schema != null)
                        {
                            foreach (FieldDefineInfo fieldInfo in Sheet.DataTableSchema.Schema.FieldInfos)
                            {
                                DataRow[] AreaRows = DataAreas.Select("SheetID='" + Sheet.Index + "' and TableName='" + Sheet.DataTableSchema.Schema.Name + "' and ColumnName='" + fieldInfo.FieldName + "'");
                                if (AreaRows.Length > 0)
                                {
                                    AreaRows[0]["SheetID"] = Sheet.Index;
                                    AreaRows[0]["SCTS"] = DateTime.Now.ToString();
                                    AreaRows[0]["TableName"] = Sheet.DataTableSchema.Schema.Name;
                                    AreaRows[0]["ColumnName"] = fieldInfo.FieldName;
                                    AreaRows[0]["Range"] = fieldInfo.RangeInfo;
                                    AreaRows[0]["SCTS_1"] = DateTime.Now.ToString();
                                }
                                else
                                {
                                    DataRow AreaRow = DataAreas.NewRow();
                                    AreaRow["ID"] = Guid.NewGuid().ToString();
                                    AreaRow["SCTS"] = DateTime.Now.ToString();
                                    AreaRow["SheetID"] = Sheet.Index;
                                    AreaRow["TableName"] = Sheet.DataTableSchema.Schema.Name;
                                    AreaRow["ColumnName"] = fieldInfo.FieldName;
                                    AreaRow["Range"] = fieldInfo.RangeInfo;
                                    AreaRows[0]["SCTS_1"] = DateTime.Now.ToString();
                                    DataAreas.Rows.Add(AreaRow);
                                }
                            }

                            SheetRow["DataTable"] = Sheet.DataTableSchema.Schema.Name;
                        }
                        else
                        {
                            SheetRow["DataTable"] = "";
                        }

                        DataSheets.Rows.Add(SheetRow);
                    }
                }

                IDbConnection Connection = GetConntion();
                Transaction Transaction = new Transaction(Connection);

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
                catch
                {
                    Transaction.Rollback();
                }
            }

            return Result;
        }

        public bool DeleteSheetConfiguration(SheetConfiguration Sheet)
        {
            //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，便于数据同步   2013-10-17
            StringBuilder Sql_Sheet = new StringBuilder();
            //Sql_Sheet.Append("Delete From sys_biz_Sheet Where ID ='");
            //Sql_Sheet.Append(Sheet.Index);
            //Sql_Sheet.Append("'");
            Sql_Sheet.Append("Update sys_biz_Sheet Set Scts_1=Getdate(),Scdel=1 Where ID ='");
            Sql_Sheet.Append(Sheet.Index);
            Sql_Sheet.Append("'");

            StringBuilder Sql_DataArea = new StringBuilder();
            if (Sheet.DataTableSchema.Schema != null)
            {
                //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，便于数据同步   2013-10-17
                //Sql_DataArea.Append("Delete From sys_biz_DataArea Where SheetID ='");
                //Sql_DataArea.Append(Sheet.Index);
                //Sql_DataArea.Append("' and TableName='");
                //Sql_DataArea.Append(Sheet.DataTableSchema.Schema.Name);
                //Sql_DataArea.Append("'");
                Sql_DataArea.Append("Update sys_biz_DataArea Set Scts_1=Getdate(),Scdel=1 Where SheetID ='");
                Sql_DataArea.Append(Sheet.Index);
                Sql_DataArea.Append("' and TableName='");
                Sql_DataArea.Append(Sheet.DataTableSchema.Schema.Name);
                Sql_DataArea.Append("'");
            }
            StringBuilder Sql_Column = new StringBuilder();
            if (Sheet.DataTableSchema.Schema != null)
            {
                //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，便于数据同步   2013-10-17
                //Sql_Column.Append("Delete From sys_columns Where  TableName='");
                //Sql_Column.Append(Sheet.DataTableSchema.Schema.Name);
                //Sql_Column.Append("'");
                Sql_Column.Append("Update sys_columns Set Scts_1=Getdate(),Scdel=1 Where TableName='");
                Sql_Column.Append(Sheet.DataTableSchema.Schema.Name);
                Sql_Column.Append("'");
            }
            StringBuilder Sql_SheetDrop = new StringBuilder();
            if (Sheet.DataTableSchema.Schema != null)
            {
                Sql_SheetDrop.Append("drop table [" + Sheet.DataTableSchema.Schema.Name + "]");
            }

            List<string> sql_Commands = new List<string>();
            sql_Commands.Add(Sql_Sheet.ToString());
            if (Sheet.DataTableSchema.Schema != null)
            {
                sql_Commands.Add(Sql_DataArea.ToString());
                sql_Commands.Add(Sql_Column.ToString());
                sql_Commands.Add(Sql_SheetDrop.ToString());

            }

            Boolean Result = false;

            IDbConnection Connection = GetConntion();
            Transaction Transaction = new Transaction(Connection);

            try
            {
                object r = ExcuteCommands(sql_Commands.ToArray(), Transaction);
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
            catch
            {
                Transaction.Rollback();
            }

            if (Sheet.DataTableSchema.Schema != null)
            {
                Result = Result & DataSchemaManager.DeleteTableStruct(Sheet.DataTableSchema.Schema.Name);
            }

            return Result;
        }

        public List<String> IsReferenceSheet(SheetConfiguration sheet)
        {
            List<String> Result = new List<string>();

            StringBuilder Sql_Command = new StringBuilder();
            // 增加查询条件 Scdel=0     2013-10-19
            Sql_Command.Append("Select ID,Description From sys_biz_Module Where Scdel=0 and Sheets like ',");
            Sql_Command.Append(sheet.Index);
            Sql_Command.Append(",'");

            DataTable Data = GetDataTable(Sql_Command.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    String Description = Row["Description"].ToString();
                    Result.Add(Description);
                }
            }

            return Result;
        }

        //public Boolean CopySheet(String SheetIndex, String NewSheetIndex, String NewSheetCode, String NewSheetName)
        //{
        //    Boolean Result = false;

        //    List<SheetConfiguration> Sheets = InitSheetConfiguration(new string[] { SheetIndex });
        //    if (Sheets.Count > 0)
        //    {
        //        SheetConfiguration sheet = Sheets[0];
        //        String TableName = TableNameManager.GetTableName(string.Concat("biz_norm_", NewSheetName));

        //        StringBuilder sql_Sheet = new StringBuilder();
        //        //增加查询条件Scdel=0   2013-10-19
        //        sql_Sheet.Append("Select * from sys_biz_Sheet where Scdel=0 and ID ='");
        //        sql_Sheet.Append(NewSheetIndex);
        //        sql_Sheet.Append("'");

        //        StringBuilder sql_Table = new StringBuilder();
        //        sql_Table.Append("Select * from sys_Tables where 1<>1");

        //        StringBuilder sql_FieldInfo = new StringBuilder();
        //        sql_FieldInfo.Append("Select * from sys_Columns where 1<>1");

        //        StringBuilder sql_DataArea = new StringBuilder();
        //        sql_DataArea.Append("Select * From sys_biz_DataArea Where 1<>1");

        //        List<string> sql_Commands = new List<string>();
        //        sql_Commands.Add(sql_Sheet.ToString());
        //        sql_Commands.Add(sql_Table.ToString());
        //        sql_Commands.Add(sql_FieldInfo.ToString());
        //        sql_Commands.Add(sql_DataArea.ToString());

        //        DataSet dataset = GetDataSet(sql_Commands.ToArray());
        //        if (dataset != null)
        //        {
        //            DataTable DataSheets = dataset.Tables["sys_biz_Sheet"];
        //            DataTable DataTables = dataset.Tables["sys_Tables"];
        //            DataTable DataFields = dataset.Tables["sys_Columns"];
        //            DataTable DataAreas = dataset.Tables["sys_biz_DataArea"];

        //            DataRow SheetRow = DataSheets.NewRow();
        //            DataSheets.Rows.Add(SheetRow);

        //            SheetRow["ID"] = NewSheetIndex;
        //            SheetRow["SCTS"] = DateTime.Now.ToString();
        //            SheetRow["CatlogCode"] = NewSheetCode;
        //            SheetRow["Description"] = NewSheetName;
        //            SheetRow["SheetStyle"] = sheet.SheetStyle;

        //            SheetRow["DataTable"] = "";
        //            //增加条件Scts_1=Getdate()   2013-10-19
        //            SheetRow["Scts_1"] = DateTime.Now.ToString();
        //            if (sheet.DataTableSchema.Schema != null)
        //            {
        //                TableDefineInfo TableInfo = sheet.DataTableSchema.Schema;
        //                DataRow TableRow = DataTables.NewRow();
        //                DataTables.Rows.Add(TableRow);

        //                TableRow["ID"] = Guid.NewGuid().ToString();
        //                TableRow["scts"] = DateTime.Now.ToString();
        //                TableRow["scpt"] = 0;
        //                TableRow["Description"] = NewSheetName;
        //                TableRow["TABLENAME"] = TableName;
        //                TableRow["TABLETYPE"] = TableInfo.Type;
        //                //增加条件Scts_1=Getdate()   2013-10-19
        //                TableRow["Scts_1"] = DateTime.Now.ToString();

        //                foreach (FieldDefineInfo fieldInfo in TableInfo.FieldInfos)
        //                {
        //                    DataRow FieldRow = DataFields.NewRow();
        //                    DataFields.Rows.Add(FieldRow);

        //                    FieldRow["ID"] = Guid.NewGuid().ToString();
        //                    FieldRow["scts"] = DateTime.Now.ToString();
        //                    FieldRow["scpt"] = 0;
        //                    FieldRow["COLNAME"] = fieldInfo.FieldName;
        //                    FieldRow["DESCRIPTION"] = fieldInfo.Description;
        //                    FieldRow["COLTYPE"] = fieldInfo.FieldType.Description;
        //                    FieldRow["TABLENAME"] = TableName;
        //                    FieldRow["IsKeyField"] = fieldInfo.IsKeyField;
        //                    //增加条件Scts_1=Getdate()   2013-10-19
        //                    FieldRow["Scts_1"] = DateTime.Now.ToString();
                            
        //                    DataRow DataAreaRow = DataAreas.NewRow();
        //                    DataAreas.Rows.Add(DataAreaRow);

        //                    DataAreaRow["ID"] = Guid.NewGuid().ToString();
        //                    DataAreaRow["SCTS"] = DateTime.Now.ToString();
        //                    DataAreaRow["SheetID"] = NewSheetIndex;
        //                    DataAreaRow["TableName"] = TableName;
        //                    DataAreaRow["ColumnName"] = fieldInfo.FieldName;
        //                    DataAreaRow["Range"] = fieldInfo.RangeInfo;
        //                    //增加条件Scts_1=Getdate()   2013-10-19
        //                    DataAreaRow["Scts_1"] = DateTime.Now.ToString();
        //                }

        //                SheetRow["DataTable"] = TableName;
        //            }

        //            IDbConnection Connection = GetConntion();
        //            Transaction Transaction = new Transaction(Connection);

        //            try
        //            {
        //                int r = Update(dataset, Transaction);
        //                Result = (r == 1);

        //                if (Result)
        //                {
        //                    Transaction.Commit();
        //                    Result = Result && DataTableManager.CreateDataTable(TableName);
        //                }
        //                else
        //                {
        //                    Transaction.Rollback();
        //                }
        //            }
        //            catch
        //            {
        //                Transaction.Rollback();
        //            }
        //        }
        //    }

        //    return Result;
        //}
    }
}
