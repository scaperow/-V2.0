using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using System.Data;
using System.Reflection;
using System.Collections;

namespace Yqun.BO.BusinessManager
{
    public class ModuleManager : BOBase
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        SheetManager SheetManager = new SheetManager();
        FunctionItemInfoManager FunctionItemInfoManager = new FunctionItemInfoManager();
        TableDefineInfoManager TableDefineInfoManager = new TableDefineInfoManager();
        DataSchemaManager DataSchemaManager = new DataSchemaManager();

        public ModuleConfiguration InitModuleConfiguration(String Index)
        {
            ModuleConfiguration Configuration = null;

            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件Scdel=0     2013-10-19
            Sql_Select.Append("Select ID,CatlogCode,Description,Sheets from sys_biz_Module where Scdel=0 and ID ='");
            Sql_Select.Append(Index);
            Sql_Select.Append("'");

            String ExtentTableName = "biz_norm_extent_" + Index;

            StringBuilder Sql_ExtentSheet = new StringBuilder();
            //增加查询条件Scdel=0     2013-10-19
            Sql_ExtentSheet.Append("Select * From sys_Tables Where Scdel=0 and TABLENAME ='");
            Sql_ExtentSheet.Append(ExtentTableName);
            Sql_ExtentSheet.Append("'");

            StringBuilder Sql_FieldInfos = new StringBuilder();
            //增加查询条件Scdel=0     2013-10-19
            Sql_FieldInfos.Append("Select * from sys_Columns where Scdel=0 and TableName ='");
            Sql_FieldInfos.Append(ExtentTableName);
            Sql_FieldInfos.Append("' order by colname");

            List<string> Sql_Commands = new List<string>();
            Sql_Commands.Add(Sql_Select.ToString());
            Sql_Commands.Add(Sql_ExtentSheet.ToString());
            Sql_Commands.Add(Sql_FieldInfos.ToString());

            DataSet dataset = GetDataSet(Sql_Commands.ToArray());
            if (dataset != null)
            {
                DataTable data_tables = dataset.Tables["sys_Tables"];
                DataTable data_columns = dataset.Tables["sys_Columns"];
                DataTable data_module = dataset.Tables["sys_biz_Module"];

                if (data_module.Rows.Count > 0)
                {
                    DataRow Row = data_module.Rows[0];

                    Configuration = new ModuleConfiguration();
                    Configuration.Index = Row["ID"].ToString();
                    Configuration.Code = Row["CatlogCode"].ToString();
                    Configuration.Description = Row["Description"].ToString();

                    string Sheets = Row["Sheets"].ToString();
                    if (!string.IsNullOrEmpty(Sheets))
                    {
                        String[] SheetList = Sheets.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        List<SheetConfiguration> SheetObjects = SheetManager.InitSheetConfiguration(SheetList);
                        Configuration.Sheets.AddRange(SheetObjects);
                    }

                    DataRow[] tableRows = data_tables.Select("TABLENAME = 'biz_norm_extent_" + Row["ID"].ToString() + "'");
                    if (tableRows != null && tableRows.Length > 0)
                    {
                        DataRow tableRow = tableRows[0];
                        Configuration.ExtentDataSchema.Index = tableRow["ID"].ToString();
                        Configuration.ExtentDataSchema.Name = tableRow["TABLENAME"].ToString();
                        Configuration.ExtentDataSchema.Description = tableRow["DESCRIPTION"].ToString();

                        DataRow[] columnRows = data_columns.Select("TABLENAME = 'biz_norm_extent_" + Row["ID"].ToString() + "'", "colname");
                        foreach (DataRow columnRow in columnRows)
                        {
                            FieldDefineInfo FieldInfo = new FieldDefineInfo(Configuration.ExtentDataSchema);
                            FieldInfo.Index = columnRow["ID"].ToString();
                            FieldInfo.Description = columnRow["DESCRIPTION"].ToString();
                            FieldInfo.FieldName = columnRow["COLNAME"].ToString();
                            FieldInfo.IsKeyField = (columnRow["IsKeyField"] != DBNull.Value ? Convert.ToBoolean(columnRow["IsKeyField"]) : false);

                            String ColType = columnRow["COLType"].ToString();
                            FieldInfo.FieldType = FieldType.GetFieldType(ColType);
                        }
                    }
                }
                else
                {
                    logger.Error(string.Format("sys_biz_Module 中未找到模板ID为 {0} 的记录", Index));
                }
            }

            return Configuration;
        }

        public List<IndexDescriptionPair> InitModuleInfo(String Index)
        {
            List<IndexDescriptionPair> pairs = new List<IndexDescriptionPair>();

            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件Scdel=0     2013-10-19
            Sql_Select.Append("Select Sheets from sys_biz_Module where Scdel=0 and ID ='");
            Sql_Select.Append(Index);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                DataRow Row = Data.Rows[0];
                String[] Indexes = Row["Sheets"].ToString().Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);
                pairs = SheetManager.InitSheetInfo(Indexes);
            }

            return pairs;
        }

        public Hashtable InitModelTableInfo(String Index)
        {
            Hashtable Results = new Hashtable();
            List<IndexDescriptionPair> Pairs = new List<IndexDescriptionPair>();

            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件Scdel=0     2013-10-19
            Sql_Select.Append("Select Sheets from sys_biz_Module where Scdel=0 and ID ='");
            Sql_Select.Append(Index);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                DataRow Row = Data.Rows[0];
                String[] Indexes = Row["Sheets"].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                Pairs = SheetManager.InitSheetInfo(Indexes);
            }

            foreach (IndexDescriptionPair pair in Pairs)
            {
                TableDefineInfo info = TableDefineInfoManager.GetTableDefineInfo(pair.Index, pair.DataTable);
                Results.Add(pair.Index, info);
            }

            return Results;
        }

        public Boolean HaveModuleConfiguration(String Index)
        {
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件Scdel=0     2013-10-19
            Sql_Select.Append("Select ID from sys_biz_Module where Scdel=0 and ID='");
            Sql_Select.Append(Index);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            return (Data != null && Data.Rows.Count > 0);
        }

        public Boolean NewModuleConfiguration(ModuleConfiguration module)
        {
            Boolean Result = false;

            StringBuilder Sql_module = new StringBuilder();
            //增加查询条件Scdel=0     2013-10-19
            Sql_module.Append("Select ID,SCTS,SCTS_1,Description,CatlogCode,Sheets,ExtentSheet From sys_biz_Module Where Scdel=0 and ID='");
            Sql_module.Append(module.Index);
            Sql_module.Append("'");

            StringBuilder Sql_ExtentSheet = new StringBuilder();
            //增加查询条件Scdel=0     2013-10-19
            Sql_ExtentSheet.Append("Select * From sys_Tables Where Scdel=0 and TABLENAME='");
            Sql_ExtentSheet.Append(module.ExtentDataSchema.Name);
            Sql_ExtentSheet.Append("'");

            List<string> sql_Commands = new List<string>();
            sql_Commands.Add(Sql_module.ToString());
            sql_Commands.Add(Sql_ExtentSheet.ToString());

            DataSet dataset = GetDataSet(sql_Commands.ToArray());
            if (dataset != null)
            {
                DataTable data_tables = dataset.Tables["sys_Tables"];
                DataTable data_module = dataset.Tables["sys_biz_Module"];

                if (data_tables != null)
                {
                    DataRow Row = data_tables.NewRow();
                    Row["ID"] = module.ExtentDataSchema.Index;
                    Row["SCTS"] = DateTime.Now.ToString();
                    Row["Description"] = module.ExtentDataSchema.Description;
                    Row["TABLENAME"] = module.ExtentDataSchema.Name;
                    Row["TABLETYPE"] = module.ExtentDataSchema.Type;
                    Row["SCTS_1"] = DateTime.Now.ToString();
                    data_tables.Rows.Add(Row);
                }

                if (data_module != null)
                {
                    DataRow Row = data_module.NewRow();
                    Row["ID"] = module.Index;
                    Row["SCTS"] = DateTime.Now.ToString();
                    Row["Description"] = module.Description;
                    Row["CatlogCode"] = module.Code;

                    Row["Sheets"] = "";
                    Row["ExtentSheet"] = module.ExtentDataSchema.Index;
                    Row["SCTS_1"] = DateTime.Now.ToString();

                    data_module.Rows.Add(Row);
                }
            }

            try
            {
                object r = Update(dataset);
                Result = (Convert.ToInt32(r) == 1);

                Result = Result & DataSchemaManager.UpdateTableStruct(module.ExtentDataSchema.Name);
                PXJZDataManager pxjz = new PXJZDataManager();
                pxjz.NewModel(module.Description, module.Code, module.Index);
            }
            catch
            { }

            return Result;
        }

        public Boolean DeleteModuleConfiguration(ModuleConfiguration module)
        {
            Boolean Result = true;

            StringBuilder Sql_Delete = new StringBuilder();
            //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，便于数据同步     2013-10-19
            //Sql_Delete.Append("Delete From sys_biz_Module Where ID ='");
            //Sql_Delete.Append(module.Index);
            //Sql_Delete.Append("'");
            Sql_Delete.Append("Update sys_biz_Module Set Scts_1=Getdate(),Scdel=1 Where ID ='");
            Sql_Delete.Append(module.Index);
            Sql_Delete.Append("'");

            StringBuilder Sql_ExtentSheet = new StringBuilder();
            //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，便于数据同步     2013-10-19
            //Sql_ExtentSheet.Append("Delete From sys_Tables Where ID ='");
            //Sql_ExtentSheet.Append(module.ExtentDataSchema.Index);
            //Sql_ExtentSheet.Append("'");
            Sql_ExtentSheet.Append("Update sys_Tables Set Scts_1=Getdate(),Scdel=1 Where ID ='");
            Sql_ExtentSheet.Append(module.ExtentDataSchema.Index);
            Sql_ExtentSheet.Append("'");

            StringBuilder Sql_DeleteField = new StringBuilder();
            //Sql_DeleteField.Append("Delete From sys_Columns Where TableName='");
            //Sql_DeleteField.Append(module.ExtentDataSchema.Name);
            //Sql_DeleteField.Append("'");
            Sql_DeleteField.Append("Update sys_Columns Set Scts_1=Getdate(),Scdel=1 Where TableName='");
            Sql_DeleteField.Append(module.ExtentDataSchema.Name);
            Sql_DeleteField.Append("'");

            StringBuilder Sql_DeleteFunctionInfo = new StringBuilder();
            //Sql_DeleteFunctionInfo.Append("Delete From sys_biz_CrossSheetFormulas Where ModelIndex='");
            //Sql_DeleteFunctionInfo.Append(module.Index);
            //Sql_DeleteFunctionInfo.Append("'");
            Sql_DeleteFunctionInfo.Append("Update sys_biz_CrossSheetFormulas Set Scts_1=Getdate(),Scdel=1 Where ModelIndex='");
            Sql_DeleteFunctionInfo.Append(module.Index);
            Sql_DeleteFunctionInfo.Append("'");

            StringBuilder Sql_DeleteCrossSheetFormulas = new StringBuilder();
            //Sql_DeleteCrossSheetFormulas.Append("Delete from sys_biz_CrossSheetFormulas where ModelIndex='");
            //Sql_DeleteCrossSheetFormulas.Append(module.Index);
            //Sql_DeleteCrossSheetFormulas.Append("'");
            Sql_DeleteCrossSheetFormulas.Append("Update sys_biz_CrossSheetFormulas Set Scts_1=Getdate(),Scdel=1 Where ModelIndex='");
            Sql_DeleteCrossSheetFormulas.Append(module.Index);
            Sql_DeleteCrossSheetFormulas.Append("'");

            StringBuilder Sql_DeleteModuleView = new StringBuilder();
            //Sql_DeleteModuleView.Append("delete sys_moduleview where ModuleID='");
            //Sql_DeleteModuleView.Append(module.Index);
            //Sql_DeleteModuleView.Append("'");
            Sql_DeleteModuleView.Append("Update sys_moduleview Set Scts_1=Getdate(),Scdel=1 where Scdel=0 and ModuleID='");
            Sql_DeleteModuleView.Append(module.Index);
            Sql_DeleteModuleView.Append("'");

            List<String> sql_Commands = new List<string>();
            sql_Commands.Add(Sql_Delete.ToString());
            sql_Commands.Add(Sql_ExtentSheet.ToString());
            sql_Commands.Add(Sql_DeleteField.ToString());
            sql_Commands.Add(Sql_DeleteFunctionInfo.ToString());
            sql_Commands.Add(Sql_DeleteCrossSheetFormulas.ToString());
            sql_Commands.Add(Sql_DeleteModuleView.ToString());

            try
            {
                int[] r = ExcuteCommands(sql_Commands.ToArray());
                for (int i = 0; i < r.Length; i++)
                {
                    Result = Result & (Convert.ToInt32(r[i]) == 1);
                }
                Result = Result & DataSchemaManager.DeleteTableStruct(module.ExtentDataSchema.Name);
                
            }
            catch(Exception ex)
            {
                logger.Error("删除错误= " + ex.Message);
            }

            try
            {
                PXJZDataManager pxjz = new PXJZDataManager();
                pxjz.DeleteModel(module.Index);
            }
            catch 
            {
            }
            return Result;
        }

        public Boolean UpdateModuleConfiguration(ModuleConfiguration module)
        {
            List<String> SheetIndexs = new List<string>();
            foreach (SheetConfiguration Sheet in module.Sheets)
            {
                SheetIndexs.Add(Sheet.Index);
            }

            StringBuilder Sql_Select = new StringBuilder();
            // 增加查询条件 Scdel=0     2013-10-19
            Sql_Select.Append("Select ID,SCTS,Description,CatlogCode,Sheets,Scts_1 From sys_biz_Module Where Scdel=0 and ID='");
            Sql_Select.Append(module.Index);
            Sql_Select.Append("'");
            
            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                DataRow Row = Data.Rows[0];
                Row["ID"] = module.Index;
                Row["SCTS"] = DateTime.Now.ToString();
                Row["Description"] = module.Description;
                Row["CatlogCode"] = module.Code;

                if (SheetIndexs.Count > 0)
                {
                    Row["Sheets"] = "," + String.Join(",", SheetIndexs.ToArray()) + ",";
                }
                else
                {
                    Row["Sheets"] = "";
                }
                Row["Scts_1"] = DateTime.Now.ToString();
            }
            else
            {
                DataRow Row = Data.NewRow();
                Row["ID"] = module.Index;
                Row["SCTS"] = DateTime.Now.ToString();
                Row["Description"] = module.Description;
                Row["CatlogCode"] = module.Code;

                if (SheetIndexs.Count > 0)
                {
                    Row["Sheets"] = "," + String.Join(",", SheetIndexs.ToArray()) + ",";
                }
                else
                {
                    Row["Sheets"] = "";
                }
                Row["Scts_1"] = DateTime.Now.ToString();

                Data.Rows.Add(Row);
            }

            Boolean Result = false;

            try
            {
                logger.Error("begin updage");

                object r = Update(Data);
                logger.Error("end update");

                Result = (Convert.ToInt32(r) == 1);
                if (Result)
                {
                    Result = Result & TableDefineInfoManager.UpdateTableDefineInfo(module.ExtentDataSchema);
                    PXJZDataManager pxjz = new PXJZDataManager();
                    pxjz.RenameModel(module.Description, module.Index);
                }
            }
            catch(Exception e)
            {
                logger.Error(e.ToString());
 
            }

            return Result;
        }

        public Boolean UpdateSheetConfigurations(ModuleConfiguration module)
        {
            Boolean Result = true;

            List<String> SheetIndexs = new List<string>();
            foreach (SheetConfiguration Sheet in module.Sheets)
            {
                SheetIndexs.Add(Sheet.Index);
            }

            StringBuilder Sql_Select = new StringBuilder();
            // 增加查询条件 Scdel=0     2013-10-19
            Sql_Select.Append("Select ID,SCTS,Scts_1,Description,CatlogCode,Sheets From sys_biz_Module Where Scdel=0 and ID='");
            Sql_Select.Append(module.Index);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                DataRow Row = Data.Rows[0];
                Row["ID"] = module.Index;
                Row["SCTS"] = DateTime.Now.ToString();
                Row["Description"] = module.Description;
                Row["CatlogCode"] = module.Code;

                if (SheetIndexs.Count > 0)
                {
                    Row["Sheets"] = "," + String.Join(",", SheetIndexs.ToArray()) + ",";
                }
                else
                {
                    Row["Sheets"] = "";
                }
                Row["Scts_1"] = DateTime.Now.ToString();
            }
            else
            {
                DataRow Row = Data.NewRow();
                Row["ID"] = module.Index;
                Row["Description"] = module.Description;
                Row["CatlogCode"] = module.Code;

                if (SheetIndexs.Count > 0)
                {
                    Row["Sheets"] = "," + String.Join(",", SheetIndexs.ToArray()) + ",";
                }

                Row["Scts_1"] = DateTime.Now.ToString();
                Data.Rows.Add(Row);
            }

            try
            {
                object r = Update(Data);
                Result = Result & (Convert.ToInt32(r) == 1);
            }
            catch
            { }

            return Result;
        }

        public Boolean IsReferenceModel(ModuleConfiguration model)
        {
            StringBuilder Sql_Have = new StringBuilder();
            // 增加查询条件 Scdel=0      2013-10-17
            Sql_Have.Append("Select Count(ID) From sys_engs_Tree Where Scdel=0 and RalationID = '");
            Sql_Have.Append(model.Index);
            Sql_Have.Append("'");

            object r = ExcuteScalar(Sql_Have.ToString());
            Boolean Result = (Convert.ToInt32(r) != 0);
            return Result;
        }

        public Boolean AlterNewColumn()
        {
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件Scdel=0     2013-10-19
            Sql_Select.Append("Select tablename from sys_tables Where Scdel=0 ");

            DataTable TableName = GetDataTable(Sql_Select.ToString());

            if (TableName != null && TableName.Rows.Count > 0)
            {
                List<String> SqlCommonds = new List<string>();
                foreach (DataRow Row in TableName.Rows)
                {
                    String SqlCommond = "Alter table [" + Row["tablename"].ToString() + "] add [SCCT] nvarchar(50) null";
                    SqlCommonds.Add(SqlCommond);
                }
                object r = ExcuteCommands(SqlCommonds.ToArray());
            }
            return true;
        }
    }
}
