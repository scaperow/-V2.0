using System;
using System.Collections.Generic;
using System.Text;
using Yqun.Permissions.Common;
using System.Data;
using Yqun.Data.DataBase;
using System.Reflection;

namespace Yqun.BO.PermissionManager
{
    public class PermissionManager : BOBase
    {
        public PermissionCollection InitPermissions(String RoleIndex)
        {
            PermissionCollection permissionCollection = new PermissionCollection();

            if (string.IsNullOrEmpty(RoleIndex))
                return permissionCollection;

            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append("Select Permissions from sys_auth_Roles where Scdel=0 and ID='");
            Sql_Select.Append(RoleIndex);
            Sql_Select.Append("'");

            String Permissions = ExcuteScalar(Sql_Select.ToString()) as String;
            if (Permissions != null)
            {
                String[] Permission = Permissions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                permissionCollection = InitPermissions(Permission);
            }

            return permissionCollection;
        }

        internal PermissionCollection InitPermissions(String[] PermissionIndex)
        {
            PermissionCollection permissionCollection = new PermissionCollection();

            if (PermissionIndex == null || PermissionIndex.Length == 0)
                return permissionCollection;

            StringBuilder Sql_Permissions = new StringBuilder();
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Permissions.Append("select * from sys_auth_Permissions where Scdel=0 and ID in ");
            Sql_Permissions.Append(string.Concat("('", string.Join("','", PermissionIndex), "')"));
            Sql_Permissions.Append(" order by ClsInfo");

            StringBuilder Sql_FieldPermission = new StringBuilder();
            Sql_FieldPermission.Append("select * from sys_auth_FieldPermission where FieldsID in (");
            //增加查询条件  Scdel=0  2013-10-17
            Sql_FieldPermission.Append("select ID from sys_auth_Permissions where Scdel=0 and ID in ");
            Sql_FieldPermission.Append(string.Concat("('", string.Join("','", PermissionIndex), "')"));
            Sql_FieldPermission.Append(" And ClsInfo='Fields'");
            Sql_FieldPermission.Append(") order by Indentity");

            StringBuilder Sql_RecordPermission = new StringBuilder();
            Sql_RecordPermission.Append("Select * from sys_auth_RecordPermission where RecordsID in (");
            //增加查询条件  Scdel=0  2013-10-17
            Sql_RecordPermission.Append("select ID from sys_auth_Permissions where Scdel=0 and ID in ");
            Sql_RecordPermission.Append(string.Concat("('", string.Join("','", PermissionIndex), "')"));
            Sql_RecordPermission.Append(" And ClsInfo='Records'");
            Sql_RecordPermission.Append(") order by Indentity");
             
            StringBuilder Sql_FunctionPermission = new StringBuilder();
            Sql_FunctionPermission.Append("Select * from sys_auth_FunctionPermission where FunctionsID in (");
            //增加查询条件  Scdel=0  2013-10-17
            Sql_FunctionPermission.Append("select ID from sys_auth_Permissions where Scdel=0 and ID in ");
            Sql_FunctionPermission.Append(string.Concat("('", string.Join("','", PermissionIndex), "')"));
            Sql_FunctionPermission.Append(" And ClsInfo='Functions'");
            Sql_FunctionPermission.Append(") order by Indentity");

            StringBuilder Sql_DataPermission = new StringBuilder();
            Sql_DataPermission.Append("Select * from sys_auth_DataPermission where TableID in (");
            //增加查询条件  Scdel=0  2013-10-17
            Sql_DataPermission.Append("select ID from sys_auth_Permissions where Scdel=0 and ID in ");
            Sql_DataPermission.Append(string.Concat("('", string.Join("','", PermissionIndex), "')"));
            Sql_DataPermission.Append(" And ClsInfo='Datas'");
            Sql_DataPermission.Append(") order by TableID");

            List<String> Sql_Commands = new List<string>();
            Sql_Commands.Add(Sql_Permissions.ToString());
            Sql_Commands.Add(Sql_FieldPermission.ToString());
            Sql_Commands.Add(Sql_RecordPermission.ToString());
            Sql_Commands.Add(Sql_FunctionPermission.ToString());
            Sql_Commands.Add(Sql_DataPermission.ToString());

            DataSet dataset = GetDataSet(Sql_Commands.ToArray());
            if (dataset != null)
            {
                DataTable PermissionDataTable = dataset.Tables["sys_auth_Permissions"];
                DataTable FieldPermissionDataTable = dataset.Tables["sys_auth_FieldPermission"];
                DataTable RecordPermissionDataTable = dataset.Tables["sys_auth_RecordPermission"];
                DataTable FunctionPermissionDataTable = dataset.Tables["sys_auth_FunctionPermission"];
                DataTable DataPermissionDataTable = dataset.Tables["sys_auth_DataPermission"];

                foreach (DataRow Row in PermissionDataTable.Rows)
                {
                    String Index = Row["ID"].ToString();
                    String Cls = Row["ClsInfo"].ToString();
                    String ModelIndex = Row["ModuleID"].ToString();
                    String Description = Row["Description"].ToString();

                    PermissionType Type = Type = (PermissionType)Enum.Parse(typeof(PermissionType), Cls);
                    if (Type == PermissionType.Records)
                    {
                        RecordsPermission recordsPermission = new RecordsPermission();
                        recordsPermission.ModuleID = ModelIndex;
                        recordsPermission.Index = Index;
                        recordsPermission.Caption = Description;
                        permissionCollection.Add(recordsPermission);

                        DataRow[] RecordRows = RecordPermissionDataTable.Select("RecordsID='" + Index + "'");
                        foreach (DataRow RecordRow in RecordRows)
                        {
                            RecordListElement recordListElement = new RecordListElement();
                            recordListElement.Caption = RecordRow["Description"].ToString();
                            recordListElement.Index = RecordRow["Indentity"].ToString();
                            recordListElement.Code = RecordRow["RecordCode"].ToString();
                            recordsPermission.RecordPermissionList.Add(recordListElement);
                        }
                    }
                    else if (Type == PermissionType.Fields)
                    {
                        FieldsPermission fieldsPermission = new FieldsPermission();
                        fieldsPermission.ModuleID = ModelIndex;
                        fieldsPermission.Index = Index;
                        fieldsPermission.Caption = Description;
                        fieldsPermission.FieldsName = Description;
                        permissionCollection.Add(fieldsPermission);

                        DataRow[] FieldRows = FieldPermissionDataTable.Select("FieldsID='" + Index + "'");
                        foreach (DataRow FieldRow in FieldRows)
                        {
                            FieldPermission fieldPermission = new FieldPermission();
                            fieldPermission.Index = FieldRow["Indentity"].ToString();
                            fieldPermission.FieldName = FieldRow["Description"].ToString();
                            fieldPermission.Editable = Convert.ToBoolean(FieldRow["Editable"]);
                            fieldPermission.Viewable = Convert.ToBoolean(FieldRow["Viewable"]);
                            fieldsPermission.Fields.Add(fieldPermission);
                        }
                    }
                    else if (Type == PermissionType.Functions)
                    {
                        FunctionsPermission functionsPermission = new FunctionsPermission();
                        functionsPermission.ModuleID = ModelIndex;
                        functionsPermission.Index = Index;
                        functionsPermission.Caption = Description;
                        permissionCollection.Add(functionsPermission);

                        DataRow[] FunctionRows = FunctionPermissionDataTable.Select("FunctionsID='" + Index + "'");
                        foreach (DataRow FunctionRow in FunctionRows)
                        {
                            FunctionPermission functionPermission = new FunctionPermission();
                            functionPermission.Caption = FunctionRow["Description"].ToString();
                            functionPermission.Index = FunctionRow["Indentity"].ToString();
                            functionsPermission.Functions.Add(functionPermission);
                        }
                    }
                    else if (Type == PermissionType.Datas)
                    {
                        DatasPermission datasPermission = new DatasPermission();
                        datasPermission.ModuleID = ModelIndex;
                        datasPermission.Index = Index;
                        datasPermission.Caption = Description;
                        permissionCollection.Add(datasPermission);

                        DataRow[] DataRows = DataPermissionDataTable.Select("TableID='" + Index + "'");
                        foreach (DataRow DataRow in DataRows)
                        {
                            DataPermission dataPermission = new DataPermission();
                            dataPermission.Index = DataRow["TableID"].ToString();
                            dataPermission.FieldName = DataRow["FieldName"].ToString();

                            String FieldValueList = DataRow["FieldValues"].ToString();
                            if (!string.IsNullOrEmpty(FieldValueList))
                            {
                                String[] Values = FieldValueList.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                dataPermission.Values.AddRange(Values);
                            }

                            datasPermission.Conditions.Add(dataPermission);
                        }
                    }
                }
            }

            return permissionCollection;
        }

        public Boolean DeletePermissions(String[] PermissionIndex)
        {
            Boolean Result = false;

            StringBuilder Sql_DeletePermissions = new StringBuilder();
            Sql_DeletePermissions.Append("Delete from sys_auth_Permissions where ID in ");
            Sql_DeletePermissions.Append(string.Concat("('", string.Join("','", PermissionIndex), "')"));
            //增加查询条件  Scdel=0  2013-10-17
            //Sql_DeletePermissions.Append("Update sys_auth_Permissions Set Scts_1=Getdate(),Scdel=1 where ID in ");
            //Sql_DeletePermissions.Append(string.Concat("('", string.Join("','", PermissionIndex), "')"));

            StringBuilder Sql_DeleteFieldPermission = new StringBuilder();
            Sql_DeleteFieldPermission.Append("Delete from sys_auth_FieldPermission where FieldsID in ('");
            Sql_DeleteFieldPermission.Append(string.Join("','", PermissionIndex));
            Sql_DeleteFieldPermission.Append("')");

            StringBuilder Sql_DeleteRecordPermission = new StringBuilder();
            Sql_DeleteRecordPermission.Append("Delete from sys_auth_RecordPermission where RecordsID in ('");
            Sql_DeleteRecordPermission.Append(string.Join("','", PermissionIndex));
            Sql_DeleteRecordPermission.Append("')");
            //增加条件  Scdel=0  2013-10-17
            //Sql_DeleteRecordPermission.Append("Update sys_auth_RecordPermission Set Scts_1=Getdate(),Scdel=1 where RecordsID in ('");
            //Sql_DeleteRecordPermission.Append(string.Join("','", PermissionIndex));
            //Sql_DeleteRecordPermission.Append("')");

            StringBuilder Sql_DeleteFunctionPermission = new StringBuilder();
            Sql_DeleteFunctionPermission.Append("Delete from sys_auth_FunctionPermission where FunctionsID in ('");
            Sql_DeleteFunctionPermission.Append(string.Join("','", PermissionIndex));
            Sql_DeleteFunctionPermission.Append("')");
            //增加条件  Scdel=0  2013-10-17
            //Sql_DeleteFunctionPermission.Append("Update sys_auth_FunctionPermission Set Scts_1=Getdate(),Scdel=1 where FunctionsID in ('");
            //Sql_DeleteFunctionPermission.Append(string.Join("','", PermissionIndex));
            //Sql_DeleteFunctionPermission.Append("')");

            StringBuilder Sql_DeleteDataPermission = new StringBuilder();
            Sql_DeleteDataPermission.Append("Delete from sys_auth_DataPermission where TableID in ('");
            Sql_DeleteDataPermission.Append(string.Join("','", PermissionIndex));
            Sql_DeleteDataPermission.Append("')");

            IDbConnection DbConnection = GetConntion();
            Transaction Transaction = new Transaction(DbConnection);

            try
            {
                object r = ExcuteCommand(Sql_DeletePermissions.ToString(), Transaction);
                Result = (Convert.ToInt32(r) == 1);

                r = ExcuteCommand(Sql_DeleteFieldPermission.ToString(), Transaction);
                Result = Result && (Convert.ToInt32(r) == 1);

                r = ExcuteCommand(Sql_DeleteRecordPermission.ToString(), Transaction);
                Result = Result && (Convert.ToInt32(r) == 1);

                r = ExcuteCommand(Sql_DeleteFunctionPermission.ToString(), Transaction);
                Result = Result && (Convert.ToInt32(r) == 1);

                r = ExcuteCommand(Sql_DeleteDataPermission.ToString(), Transaction);
                Result = Result && (Convert.ToInt32(r) == 1);

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

            return Result;
        }

        public Boolean UpdatePermissions(PermissionCollection Permissions)
        {
            if (Permissions == null || Permissions.Count == 0)
                return true;

            Boolean Result = false;

            List<string> PermissionIndexList = new List<string>();
            foreach (Permission Permission in Permissions)
            {
                PermissionIndexList.Add(Permission.Index);
            }

            String[] PermissionIndex = PermissionIndexList.ToArray();
            Result = DeletePermissions(PermissionIndex);

            StringBuilder Sql_Permissions = new StringBuilder();
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Permissions.Append("select * from sys_auth_Permissions where ID in ");
            Sql_Permissions.Append(string.Concat("('", string.Join("','", PermissionIndex), "')"));
            Sql_Permissions.Append(" order by ClsInfo");

            StringBuilder Sql_FieldPermission = new StringBuilder();
            Sql_FieldPermission.Append("select * from sys_auth_FieldPermission where FieldsID in (");
            //增加查询条件  Scdel=0  2013-10-17
            Sql_FieldPermission.Append("select ID from sys_auth_Permissions where  ID in ");
            Sql_FieldPermission.Append(string.Concat("('", string.Join("','", PermissionIndex), "')"));
            Sql_FieldPermission.Append(" And ClsInfo='Fields'");
            Sql_FieldPermission.Append(") order by Indentity");

            StringBuilder Sql_RecordPermission = new StringBuilder();
            Sql_RecordPermission.Append("Select * from sys_auth_RecordPermission where RecordsID in (");
            //增加查询条件  Scdel=0  2013-10-17
            Sql_RecordPermission.Append("select ID from sys_auth_Permissions where ID in ");
            Sql_RecordPermission.Append(string.Concat("('", string.Join("','", PermissionIndex), "')"));
            Sql_RecordPermission.Append(" And ClsInfo='Records'");
            Sql_RecordPermission.Append(") order by Indentity");

            StringBuilder Sql_FunctionPermission = new StringBuilder();
            Sql_FunctionPermission.Append("Select * from sys_auth_FunctionPermission where FunctionsID in (");
            //增加查询条件  Scdel=0  2013-10-17
            Sql_FunctionPermission.Append("select ID from sys_auth_Permissions where  ID in ");
            Sql_FunctionPermission.Append(string.Concat("('", string.Join("','", PermissionIndex), "')"));
            Sql_FunctionPermission.Append(" And ClsInfo='Functions'");
            Sql_FunctionPermission.Append(") order by Indentity");

            StringBuilder Sql_DataPermission = new StringBuilder();
            Sql_DataPermission.Append("Select * from sys_auth_DataPermission where TableID in (");
            //增加查询条件  Scdel=0  2013-10-17
            Sql_DataPermission.Append("select ID from sys_auth_Permissions where ID in ");
            Sql_DataPermission.Append(string.Concat("('", string.Join("','", PermissionIndex), "')"));
            Sql_DataPermission.Append(" And ClsInfo='Datas'");
            Sql_DataPermission.Append(") order by TableID");

            List<String> Sql_Commands = new List<string>();
            Sql_Commands.Add(Sql_Permissions.ToString());
            Sql_Commands.Add(Sql_FieldPermission.ToString());
            Sql_Commands.Add(Sql_RecordPermission.ToString());
            Sql_Commands.Add(Sql_FunctionPermission.ToString());
            Sql_Commands.Add(Sql_DataPermission.ToString());

            DataSet dataset = GetDataSet(Sql_Commands.ToArray());
            if (dataset != null)
            {
                DataTable PermissionDataTable = dataset.Tables["sys_auth_Permissions"];
                DataTable FieldPermissionDataTable = dataset.Tables["sys_auth_FieldPermission"];
                DataTable RecordPermissionDataTable = dataset.Tables["sys_auth_RecordPermission"];
                DataTable FunctionPermissionDataTable = dataset.Tables["sys_auth_FunctionPermission"];
                DataTable DataPermissionDataTable = dataset.Tables["sys_auth_DataPermission"];

                foreach (Permission Permission in Permissions)
                {
                    DataRow Row;
                    DataRow[] Rows = PermissionDataTable.Select("ID='" + Permission.Index + "'");
                    if (Rows.Length > 0)
                    {
                        Row = Rows[0];
                    }
                    else
                    {
                        Row = PermissionDataTable.NewRow();
                        PermissionDataTable.Rows.Add(Row);
                    }

                    Row["ID"] = Permission.Index;
                    Row["SCTS"] = DateTime.Now.ToString();
                    Row["ModuleID"] = Permission.ModuleID;
                    Row["Description"] = Permission.Caption;
                    Row["Scdel"] = 0;

                    if (Permission is RecordsPermission)
                    {
                        RecordsPermission records = Permission as RecordsPermission;
                        foreach (RecordListElement record in records.RecordPermissionList)
                        {
                            DataRow recordRow;
                            DataRow[] recordRows = RecordPermissionDataTable.Select("Indentity='" + record.Index + "' and RecordsID='" + Permission.Index + "'");
                            if (recordRows.Length > 0)
                            {
                                recordRow = recordRows[0];
                            }
                            else
                            {
                                recordRow = RecordPermissionDataTable.NewRow();
                                RecordPermissionDataTable.Rows.Add(recordRow);

                                recordRow["ID"] = Guid.NewGuid().ToString();
                                recordRow["SCTS"] = DateTime.Now.ToString();
                            }

                            recordRow["RecordsID"] = Permission.Index;
                            recordRow["Indentity"] = record.Index;
                            recordRow["Description"] = record.Caption;
                            recordRow["RecordCode"] = record.Code;
                            recordRow["Scdel"] = 0;
                        }

                        Row["ClsInfo"] = "Records";
                    }
                    else if (Permission is FieldsPermission)
                    {
                        FieldsPermission fields = Permission as FieldsPermission;
                        foreach (FieldPermission field in fields.Fields)
                        {
                            DataRow fieldRow;
                            DataRow[] fieldRows = FieldPermissionDataTable.Select("Indentity='" + field.Index + "'");
                            if (fieldRows.Length > 0)
                            {
                                fieldRow = fieldRows[0];
                            }
                            else
                            {
                                fieldRow = FieldPermissionDataTable.NewRow();
                                FieldPermissionDataTable.Rows.Add(fieldRow);

                                fieldRow["ID"] = Guid.NewGuid().ToString();
                                fieldRow["SCTS"] = DateTime.Now.ToString();
                            }

                            fieldRow["FieldsID"] = Permission.Index;
                            fieldRow["Indentity"] = field.Index;
                            fieldRow["Description"] = field.FieldName;
                            fieldRow["Editable"] = field.Editable;
                            fieldRow["Viewable"] = field.Viewable;
                            fieldRow["Scdel"] = 0;
                        }

                        Row["ClsInfo"] = "Fields";
                    }
                    else if (Permission is FunctionsPermission)
                    {
                        FunctionsPermission functions = Permission as FunctionsPermission;
                        foreach (FunctionPermission function in functions.Functions)
                        {
                            DataRow functionRow;
                            DataRow[] functionRows = FunctionPermissionDataTable.Select("Indentity='" + function.Index + "' and FunctionsID = '" + Permission.Index + "'");
                            if (functionRows.Length > 0)
                            {
                                functionRow = functionRows[0];
                            }
                            else
                            {
                                functionRow = FunctionPermissionDataTable.NewRow();
                                FunctionPermissionDataTable.Rows.Add(functionRow);

                                functionRow["ID"] = Guid.NewGuid().ToString();
                                functionRow["SCTS"] = DateTime.Now.ToString();
                            }

                            functionRow["FunctionsID"] = Permission.Index;
                            functionRow["Indentity"] = function.Index;
                            functionRow["Description"] = function.Caption;
                            functionRow["Scdel"] = 0;

                        }

                        Row["ClsInfo"] = "Functions";
                    }
                    else if (Permission is DatasPermission)
                    {
                        DatasPermission Datas = Permission as DatasPermission;
                        foreach (DataPermission Data in Datas.Conditions)
                        {
                            DataRow dataRow;
                            DataRow[] dataRows = DataPermissionDataTable.Select("TableID='" + Permission.Index + "' and FieldName='" + Data.FieldName + "'");
                            if (dataRows.Length > 0)
                            {
                                dataRow = dataRows[0];
                            }
                            else
                            {
                                dataRow = DataPermissionDataTable.NewRow();
                                DataPermissionDataTable.Rows.Add(dataRow);

                                dataRow["ID"] = Guid.NewGuid().ToString();
                                dataRow["SCTS"] = DateTime.Now.ToString();
                            }

                            dataRow["TableID"] = Data.Index;
                            dataRow["SCTS"] = DateTime.Now.ToString();
                            dataRow["FieldName"] = Data.FieldName;
                            dataRow["FieldValues"] = string.Join(";", Data.Values.ToArray());
                            dataRow["Scdel"] = 0;
                        }

                        Row["ClsInfo"] = "Datas";
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
    }
}
