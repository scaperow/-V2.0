using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.Win.Spread;
using Yqun.Common.Encoder;
using System.Data;
using Yqun.Services;
using BizCommon;

namespace BizComponents
{
    public class WriteFunctionRuntime
    {
        internal class ConditionExpression<T>
        {
            private String _ColumnName;
            public String ColumnName
            {
                get
                {
                    return _ColumnName;
                }
                set
                {
                    _ColumnName = value;
                }
            }

            private String _Operation;
            public String Operation
            {
                get
                {
                    return _Operation;
                }
                set
                {
                    _Operation = value;
                }
            }

            private T _Value;
            public T Value
            {
                get
                {
                    return _Value;
                }
                set
                {
                    _Value = value;
                }
            }
        }

        public static void ExcuteFunction(string TableName, DataTable Data)
        {
            if (Data == null ||
               (Data != null && Data.Rows.Count == 0))
                return;

            List<WriteDataFunctionInfo> FunctionInfos = DepositoryWriteFunction.InitByModelIndex(TableName);
            object SCPT = DBNull.Value;
            foreach (DataRow row in Data.Rows)
            {
                foreach (WriteDataFunctionInfo FunctionInfo in FunctionInfos)
                {
                    List<ConditionExpression<string>> ConditionExpressions = GetWriteToTableFilter(FunctionInfo, row);
                    List<ConditionExpression<object>> UpdatedConditionExpressions = GetWriteToTableUpdatedColumn(FunctionInfo, row);

                    if (row.RowState != DataRowState.Deleted)
                    {
                        SCPT = row["SCPT"].ToString();
                    }
                    else
                    {
                        SCPT = row["SCPT", DataRowVersion.Original].ToString();
                    }

                    UpdateWriteToTable(FunctionInfo, UpdatedConditionExpressions, ConditionExpressions, SCPT);
                }
            }
        }

        private static List<ConditionExpression<object>> GetWriteToTableUpdatedColumn(WriteDataFunctionInfo FunctionInfo, DataRow row)
        {
            List<ConditionExpression<object>> UpdatedColumns = new List<ConditionExpression<object>>();
            if (FunctionInfo.Modifications.Count > 0)
            {
                for (int i = 0; i < FunctionInfo.Modifications.Count; i++)
                {
                    ExpressionInfo Info = FunctionInfo.Modifications[i];
                    ConditionExpression<object> UpdatedColumnExpression = new ConditionExpression<object>();

                    UpdatedColumnExpression.ColumnName = Info.DataItem.Name;

                    if (row.Table.Columns.Contains(Info.DataValue.Name))
                    {
                        Type type = row.Table.Columns[Info.DataValue.Name].DataType;
                        if (type.FullName.ToLower().Contains("int") ||
                            type.FullName.ToLower().Contains("decimal"))
                        {
                            float Current = 0f;

                            try
                            {
                                float.TryParse(row[Info.DataValue.Name].ToString(), out Current);
                            }
                            catch
                            { }

                            UpdatedColumnExpression.Value = Current;
                        }
                        else if (type.FullName.ToLower().Contains("datetime"))
                        {
                            try
                            {
                                UpdatedColumnExpression.Value = row[Info.DataValue.Name];
                            }
                            catch
                            {
                                UpdatedColumnExpression.Value = DBNull.Value;
                            }
                        }
                    }

                    switch (Info.Operation)
                    {
                        case "累计加":
                            UpdatedColumnExpression.Operation = "+";
                            break;
                        case "累计减":
                            UpdatedColumnExpression.Operation = "-";
                            break;
                        case "赋值":
                            UpdatedColumnExpression.Operation = "=";
                            break;
                        default:
                            UpdatedColumnExpression.Operation = "+";
                            break;
                    }

                    UpdatedColumns.Add(UpdatedColumnExpression);
                }
            }

            return UpdatedColumns;
        }

        //获得写入数据表的数据过滤条件
        private static List<ConditionExpression<string>> GetWriteToTableFilter(WriteDataFunctionInfo FunctionInfo, DataRow row)
        {
            List<ConditionExpression<string>> DataFilter = new List<ConditionExpression<string>>();
            foreach (ExpressionInfo Info in FunctionInfo.Conditions)
            {
                if (row.Table.Columns.Contains(Info.DataValue.Name))
                {
                    ConditionExpression<string> ElementFilter = new ConditionExpression<string>();
                    ElementFilter.ColumnName = Info.DataItem.Name;

                    switch (Info.Operation)
                    {
                        case "等于":
                            ElementFilter.Operation = "=";
                            break;
                        case "大于":
                            ElementFilter.Operation = ">";
                            break;
                        case "大于或等于":
                            ElementFilter.Operation = ">=";
                            break;
                        case "小于":
                            ElementFilter.Operation = "<";
                            break;
                        case "小于或等于":
                            ElementFilter.Operation = "<=";
                            break;
                        case "取最大值":
                        case "取最小值":
                            ElementFilter.Operation = "=";
                            break;
                    }

                    String ColValue = "";
                    if (Info.Operation.ToLower() != "取最大值" &&
                        Info.Operation.ToLower() != "取最小值")
                    {
                        if (row.RowState != DataRowState.Deleted)
                        {
                            ColValue = row[Info.DataValue.Name].ToString();
                        }
                        else
                        {
                            ColValue = row[Info.DataValue.Name, DataRowVersion.Original].ToString();
                        }

                        Type type = row.Table.Columns[Info.DataValue.Name].DataType;
                        if (!type.FullName.ToLower().Contains("int") &&
                            !type.FullName.ToLower().Contains("decimal"))
                        {
                            if (type.FullName.ToLower().Contains("datetime"))
                            {
                                DateTime datetime = DateTime.Parse(ColValue);
                                if (Info.Operation.ToLower().Contains("大于"))
                                {
                                    ColValue = datetime.Year.ToString() + "-" +
                                               datetime.Month.ToString("00") + "-" +
                                               datetime.Day.ToString("00") + " 23:59:59";
                                }
                                else if (Info.Operation.ToLower().Contains("小于"))
                                {
                                    ColValue = datetime.Year.ToString() + "-" +
                                               datetime.Month.ToString("00") + "-" +
                                               datetime.Day.ToString("00") + " 00:00:00";
                                }
                            }

                            ColValue = "'" + ColValue + "'";
                        }
                    }
                    else
                    {
                        DataTable Table = row.Table;
                        Object Object;
                        switch (Info.Operation)
                        {
                            case "取最大值":
                                Object = Agent.CallService("Yqun.BO.LoginBO.dll", "ExcuteScalar", new Object[] { "select max(" + Info.DataValue.Name + ") from " + Table.TableName });
                                if (Object != null)
                                {
                                    ColValue = System.Convert.ToString(Object);
                                }
                                break;
                            case "取最小值":
                                Object = Agent.CallService("Yqun.BO.LoginBO.dll", "ExcuteScalar", new Object[] { "select min(" + Info.DataValue.Name + ") from " + Table.TableName });
                                if (Object != null)
                                {
                                    ColValue = System.Convert.ToString(Object);
                                }
                                break;
                        }

                        Type type = row.Table.Columns[Info.DataValue.Name].DataType;
                        if (!type.FullName.ToLower().Contains("int") &&
                            !type.FullName.ToLower().Contains("decimal"))
                        {
                            ColValue = "'" + ColValue + "'";
                        }
                    }

                    ElementFilter.Value = ColValue;
                    DataFilter.Add(ElementFilter);
                }
            }

            return DataFilter;
        }

        //将变化的值更新到待写入的数据表
        private static void UpdateWriteToTable(WriteDataFunctionInfo FunctionInfo, List<ConditionExpression<object>> UpdatedConditionExpressions, List<ConditionExpression<string>> ConditionExpressions, object Relation)
        {
            List<string> WriteToTableFilter = new List<string>();
            foreach (ConditionExpression<string> element in ConditionExpressions)
            {
                WriteToTableFilter.Add(element.ColumnName + " " + element.Operation + " " + element.Value);
            }

            if (UpdatedConditionExpressions.Count > 0)
            {
                StringBuilder Sql_Data = new StringBuilder();
                Sql_Data.Append("select ID from ");
                Sql_Data.Append(FunctionInfo.WriteInTableText);
                Sql_Data.Append(" where ");
                Sql_Data.Append("SCPT = '" + Relation + "'");

                DataTable TableData = Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { Sql_Data.ToString() }) as DataTable;
                if (TableData != null && TableData.Rows.Count > 0)
                {
                    List<string> UpdateCommands = new List<string>();
                    foreach (DataRow Row in TableData.Rows)
                    {
                        String ID = Row["ID"].ToString();
                        StringBuilder Sql_Update = new StringBuilder();
                        Sql_Update.Append("Update ");
                        Sql_Update.Append(FunctionInfo.WriteInTableText);
                        Sql_Update.Append(" Set ");

                        for (int i = 0; i < UpdatedConditionExpressions.Count; i++)
                        {
                            ConditionExpression<object> UpdatedColumn = UpdatedConditionExpressions[i];

                            if (UpdatedColumn.Value == null)
                                continue;

                            Sql_Update.Append("[" + UpdatedColumn.ColumnName + "]");
                            Sql_Update.Append("=");

                            if (UpdatedColumn.Operation != "=")
                            {
                                Sql_Update.Append("[" + UpdatedColumn.ColumnName + "]");
                                Sql_Update.Append(UpdatedColumn.Operation);
                            }

                            Sql_Update.Append("(");

                            Type type = UpdatedColumn.Value.GetType();
                            if (type.FullName.ToLower().Contains("int") ||
                            type.FullName.ToLower().Contains("decimal"))
                            {
                                Sql_Update.Append(UpdatedColumn.Value.ToString());
                            }
                            else
                            {
                                Sql_Update.Append("'");
                                Sql_Update.Append(UpdatedColumn.Value.ToString());
                                Sql_Update.Append("'");
                            }

                            Sql_Update.Append(")");
                            if (i + 1 < UpdatedConditionExpressions.Count)
                                Sql_Update.Append(" , ");

                        }

                        Sql_Update.Append(" Where ID ='");
                        Sql_Update.Append(ID);
                        Sql_Update.Append("'");

                        UpdateCommands.Add(Sql_Update.ToString());
                    }
                    
                    if (UpdateCommands.Count > 0)
                    {
                        String Update = string.Join(";", UpdateCommands.ToArray());
                        object r = Agent.CallService("Yqun.BO.LoginBO.dll", "ExcuteCommand", new object[] { Update.ToString() });
                    }
                }
                else
                {
                    List<string> ColumnNameList = new List<string>();
                    List<object> ColumnValueList = new List<object>();

                    for (int i = 0; i < UpdatedConditionExpressions.Count; i++)
                    {
                        ConditionExpression<object> UpdatedColumn = UpdatedConditionExpressions[i];
                        ColumnNameList.Add("[" + UpdatedColumn.ColumnName + "]");
                        ColumnValueList.Add(UpdatedColumn.Value);
                    }

                    string ColumnNames = string.Join(",", ColumnNameList.ToArray());

                    List<string> Fields = new List<string>();
                    List<string> FieldValues = new List<string>();
                    String ID = Guid.NewGuid().ToString();
                    foreach (ConditionExpression<string> element in ConditionExpressions)
                    {
                        if (!Fields.Contains(element.ColumnName) && !ColumnNameList.Contains("[" + element.ColumnName + "]"))
                        {
                            Fields.Add("[" + element.ColumnName + "]");
                            FieldValues.Add(element.Value);
                        }
                    }

                    StringBuilder Sql_Insert = new StringBuilder();
                    Sql_Insert.Append("Insert into ");
                    Sql_Insert.Append(FunctionInfo.WriteInTableText);
                    Sql_Insert.Append("(ID,SCPT,");

                    Sql_Insert.Append(ColumnNames);

                    if (Fields.Count > 0)
                    {
                        Sql_Insert.Append(",");
                        Sql_Insert.Append(string.Join(",", Fields.ToArray()));
                    }

                    Sql_Insert.Append(") values('");
                    Sql_Insert.Append(ID);
                    Sql_Insert.Append("','");
                    Sql_Insert.Append(Relation);
                    Sql_Insert.Append("',");
                    for (int j = 0; j < ColumnValueList.Count; j++)
                    {
                        object ColumnValue = ColumnValueList[j];
                        Type type = ColumnValue.GetType();
                        if (type.FullName.ToLower().Contains("int") ||
                        type.FullName.ToLower().Contains("decimal"))
                        {
                            Sql_Insert.Append(ColumnValue.ToString());
                        }
                        else
                        {
                            Sql_Insert.Append("'");
                            Sql_Insert.Append(ColumnValue.ToString());
                            Sql_Insert.Append("'");
                        }

                        if (j + 1 < ColumnValueList.Count)
                            Sql_Insert.Append(",");

                    }

                    if (Fields.Count > 0)
                    {
                        Sql_Insert.Append(",");
                        Sql_Insert.Append(string.Join(",", FieldValues.ToArray()));
                    }

                    Sql_Insert.Append(")");

                    object r = Agent.CallService("Yqun.BO.LoginBO.dll", "ExcuteCommand", new object[] { Sql_Insert.ToString() });
                }
            }
        }
    }
}
