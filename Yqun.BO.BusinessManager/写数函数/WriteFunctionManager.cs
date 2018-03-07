using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using System.Data;
using Yqun.Data.DataBase;

namespace Yqun.BO.BusinessManager
{
    public class WriteFunctionManager : BOBase
    {
        /// <summary>
        /// 获得所有指定表单的写数函数
        /// </summary>
        /// <param name="SheetID"></param>
        /// <returns></returns>
        public List<WriteDataFunctionInfo> InitWriteFunctionByModelIndex(string ModelIndex)
        {
            List<WriteDataFunctionInfo> WriteDataFunctionInfos = new List<WriteDataFunctionInfo>();
            StringBuilder sql_Select = new StringBuilder();
            sql_Select.Append("select ID from sys_writefunction where ModelIndex ='");
            sql_Select.Append(ModelIndex.Trim("[]".ToCharArray()));
            sql_Select.Append("'");

            DataTable Data = GetDataTable(sql_Select.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                foreach (DataRow row in Data.Rows)
                {
                    string FunctionIndex = row["ID"].ToString();
                    WriteDataFunctionInfo functionInfo = InitWriteFunctionByFunctionIndex(FunctionIndex);
                    WriteDataFunctionInfos.Add(functionInfo);
                }
            }

            return WriteDataFunctionInfos;
        }

        public WriteDataFunctionInfo InitWriteFunctionByFunctionIndex(string FunctionIndex)
        {
            WriteDataFunctionInfo functionInfo = new WriteDataFunctionInfo();
            if (!string.IsNullOrEmpty(FunctionIndex))
            {
                StringBuilder Sql_Function = new StringBuilder();
                Sql_Function.Append("select * from sys_writefunction where ID='");
                Sql_Function.Append(FunctionIndex);
                Sql_Function.Append("'");

                DataTable data = GetDataTable(Sql_Function.ToString());
                if (data != null && data.Rows.Count > 0)
                {
                    functionInfo.Index = data.Rows[0]["ID"].ToString();
                    functionInfo.Name = data.Rows[0]["FunctionName"].ToString();
                    functionInfo.WriteInTableIndex = data.Rows[0]["SheetIndex"].ToString();
                    functionInfo.WriteInTableText = data.Rows[0]["SheetName"].ToString();
                    functionInfo.ReadOutTableIndex = data.Rows[0]["ModelIndex"].ToString();

                    String ConditionList = data.Rows[0]["ConditionList"].ToString();
                    StringBuilder Sql_Condition = new StringBuilder();
                    Sql_Condition.Append("Select * From Sys_WriteFunctionCondition where ID in ('");
                    Sql_Condition.Append(ConditionList.Replace(",","','"));
                    Sql_Condition.Append("')");

                    String ModifyList = data.Rows[0]["ModifyList"].ToString();
                    StringBuilder Sql_Modify = new StringBuilder();
                    Sql_Modify.Append("Select * From Sys_WriteFunctionModify where ID in ('");
                    Sql_Modify.Append(ModifyList.Replace(",", "','"));
                    Sql_Modify.Append("')");

                    List<string> sql_Commands = new List<string>();
                    sql_Commands.Add(Sql_Condition.ToString());
                    sql_Commands.Add(Sql_Modify.ToString());

                    DataSet dataset = GetDataSet(sql_Commands.ToArray());
                    if (dataset != null)
                    {
                        DataTable TableCondition = dataset.Tables["Sys_WriteFunctionCondition"];
                        DataTable TableModify = dataset.Tables["Sys_WriteFunctionModify"];

                        foreach (DataRow row in TableCondition.Rows)
                        {
                            ExpressionInfo expressionInfo = new ExpressionInfo();
                            expressionInfo.Index = row["ID"].ToString();
                            expressionInfo.DataItem.Name = row["ColName"].ToString();
                            expressionInfo.DataItem.Text = row["ColDescription"].ToString();
                            expressionInfo.DataValue.Name = row["ConditionColName"].ToString();
                            expressionInfo.DataValue.Text = row["ConditionColDescription"].ToString();
                            expressionInfo.Operation = row["ConditionType"].ToString();

                            functionInfo.Conditions.Add(expressionInfo);
                        }

                        foreach (DataRow row in TableModify.Rows)
                        {
                            ExpressionInfo expressionInfo = new ExpressionInfo();
                            expressionInfo.Index = row["ID"].ToString();
                            expressionInfo.DataItem.Name = row["ColName"].ToString();
                            expressionInfo.DataItem.Text = row["ColDescription"].ToString();
                            expressionInfo.DataValue.Name = row["ModifyColName"].ToString();
                            expressionInfo.DataValue.Text = row["ModifyColDescription"].ToString();
                            expressionInfo.Operation = row["ModifyType"].ToString();

                            functionInfo.Modifications.Add(expressionInfo);
                        }
                    }
                }
            }

            return functionInfo;
        }

        public bool SaveWriteFunctionInfo(WriteDataFunctionInfo FunctionInfo)
        {
            Boolean Result = true;

            StringBuilder Sql_Function = new StringBuilder();
            Sql_Function.Append("Select ID,FunctionName,SheetID,SheetName,ParentID,ConditionList,ModifyList From Sys_WriteFunction Where ID = '");
            Sql_Function.Append(FunctionInfo.Index);
            Sql_Function.Append("'");

            StringBuilder Sql_ModifyList = new StringBuilder();
            Sql_ModifyList.Append("Select ID,ColName,ColDescription,ModifyType,ModifyColName,ModifyColDescription From Sys_WriteFunctionModify where ID in ('");
            Sql_ModifyList.Append(FunctionInfo.ModificationIDs.Replace(",", "','"));
            Sql_ModifyList.Append("')");

            StringBuilder Sql_ConditionList = new StringBuilder();
            Sql_ConditionList.Append("Select ID,ColName,ColDescription,ConditionType,ConditionColName,ConditionColDescription From Sys_WriteFunctionCondition where ID in ('");
            Sql_ConditionList.Append(FunctionInfo.ConditionIDs.Replace(",", "','"));
            Sql_ConditionList.Append("')");

            List<string> sql_Commands = new List<string>();
            sql_Commands.Add(Sql_Function.ToString());
            sql_Commands.Add(Sql_ModifyList.ToString());
            sql_Commands.Add(Sql_ConditionList.ToString());

            DataSet dataset = GetDataSet(sql_Commands.ToArray());
            if (dataset != null)
            {
                DataTable TableFunction = dataset.Tables["Sys_WriteFunction"];
                DataTable ConditionFunction = dataset.Tables["Sys_WriteFunctionCondition"];
                DataTable ModifyFunction = dataset.Tables["Sys_WriteFunctionModify"];

                if (TableFunction != null)
                {
                    if (TableFunction.Rows.Count > 0)
                    {
                        DataRow Row = TableFunction.Rows[0];
                        Row["FunctionName"] = FunctionInfo.Name;
                        Row["SheetIndex"] = FunctionInfo.WriteInTableIndex;
                        Row["SheetName"] = FunctionInfo.WriteInTableText;
                        Row["ModelIndex"] = FunctionInfo.ReadOutTableIndex;
                        Row["ConditionList"] = FunctionInfo.ConditionIDs;
                        Row["ModifyList"] = FunctionInfo.ModificationIDs;
                    }
                    else
                    {
                        DataRow Row = TableFunction.NewRow();
                        Row["ID"] = FunctionInfo.Index;
                        Row["FunctionName"] = FunctionInfo.Name;
                        Row["SheetIndex"] = FunctionInfo.WriteInTableIndex;
                        Row["SheetName"] = FunctionInfo.WriteInTableText;
                        Row["ModelIndex"] = FunctionInfo.ReadOutTableIndex;
                        Row["ConditionList"] = FunctionInfo.ConditionIDs;
                        Row["ModifyList"] = FunctionInfo.ModificationIDs;
                        TableFunction.Rows.Add(Row);
                    }

                    foreach (ExpressionInfo Info in FunctionInfo.Modifications)
                    {
                        DataRow[] Rows = ModifyFunction.Select("ID = '" + Info.Index + "'");
                        if (Rows != null && Rows.Length > 0)
                        {
                            DataRow Row = Rows[0];
                            Row["ID"] = Info.Index;
                            Row["ColName"] = Info.DataItem.Name;
                            Row["ColDescription"] = Info.DataItem.Text;
                            Row["ModifyType"] = Info.Operation;
                            Row["ModifyColName"] = Info.DataValue.Name;
                            Row["ModifyColDescription"] = Info.DataValue.Text;
                        }
                        else
                        {
                            DataRow Row = ModifyFunction.NewRow();
                            Row["ID"] = Info.Index;
                            Row["ColName"] = Info.DataItem.Name;
                            Row["ColDescription"] = Info.DataItem.Text;
                            Row["ModifyType"] = Info.Operation;
                            Row["ModifyColName"] = Info.DataValue.Name;
                            Row["ModifyColDescription"] = Info.DataValue.Text;
                            ModifyFunction.Rows.Add(Row);
                        }
                    }

                    foreach (ExpressionInfo Info in FunctionInfo.Conditions)
                    {
                        DataRow[] Rows = ConditionFunction.Select("ID = '" + Info.Index + "'");
                        if (Rows != null && Rows.Length > 0)
                        {
                            DataRow Row = Rows[0];
                            Row["ID"] = Info.Index;
                            Row["ColName"] = Info.DataItem.Name;
                            Row["ColDescription"] = Info.DataItem.Text;
                            Row["ConditionType"] = Info.Operation;
                            Row["ConditionColName"] = Info.DataValue.Name;
                            Row["ConditionColDescription"] = Info.DataValue.Text;
                        }
                        else
                        {
                            DataRow Row = ConditionFunction.NewRow();
                            Row["ID"] = Info.Index;
                            Row["ColName"] = Info.DataItem.Name;
                            Row["ColDescription"] = Info.DataItem.Text;
                            Row["ConditionType"] = Info.Operation;
                            Row["ConditionColName"] = Info.DataValue.Name;
                            Row["ConditionColDescription"] = Info.DataValue.Text;
                            ConditionFunction.Rows.Add(Row);
                        }
                    }
                }

                IDbConnection Connection = GetConntion();
                Transaction Transaction = new Transaction(Connection);

                try
                {
                    object r = Update(dataset, Transaction);
                    Result = Result & (Convert.ToInt32(r) > 0);

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

        public Boolean DeleteWriteFunctionInfo(WriteDataFunctionInfo FunctionInfo)
        {
            String ModifyList = FunctionInfo.ModificationIDs;
            StringBuilder Sql_ModifyList = new StringBuilder();
            Sql_ModifyList.Append("Delete From sys_writefunctionmodify where ID in ('");
            Sql_ModifyList.Append(ModifyList.Replace(",", "','"));
            Sql_ModifyList.Append("')");

            String ConditionList = FunctionInfo.ConditionIDs;
            StringBuilder Sql_ConditionList = new StringBuilder();
            Sql_ConditionList.Append("Delete From sys_writefunctioncondition where ID in ('");
            Sql_ConditionList.Append(ConditionList.Replace(",", "','"));
            Sql_ConditionList.Append("')");

            StringBuilder Sql_Delete = new StringBuilder();
            Sql_Delete.Append("Delete From sys_writefunction where ID in ('");
            Sql_Delete.Append(FunctionInfo.Index);
            Sql_Delete.Append("')");

            List<string> sql_Commands = new List<string>();
            sql_Commands.Add(Sql_ModifyList.ToString());
            sql_Commands.Add(Sql_ConditionList.ToString());
            sql_Commands.Add(Sql_Delete.ToString());

            Boolean Result = true;

            IDbConnection Connection = GetConntion();
            Transaction Transaction = new Transaction(Connection);

            try
            {
                object r = ExcuteCommands(sql_Commands.ToArray(),Transaction);
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

            return Result;
        }
    }
}
