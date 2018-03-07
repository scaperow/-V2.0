using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Yqun.Data.DataBase;
using BizCommon;
using System.Reflection;

namespace Yqun.BO
{
    public class DataTableManager : BOBase
    {
        string[] Columns = new string[] { "ID", "SCTS", "SCPT", "SCCT", "SCCS" };
        string[] DataTypes = new string[] { "VARCHAR", "DATETIME", "VARCHAR", "NVARCHAR", "NVARCHAR" };
        string[] Lengths = new string[] { "36", "", "36", "50", "50" };
        string[] IsNULL = new string[] { "NOT NULL", "NULL", "NULL", "NULL", "NULL" };

        public DataTableManager()
        {
        }

        //是否有数据
        public Boolean HasData(String TableName)
        {
            String sql_select = "select count(*) from " + TableName;
            object o = ExcuteScalar(sql_select);
            return Convert.ToInt32(o) > 0;
        }

        //是否存在表
        public Boolean HaveDataTable(String TableName)
        {
            return IsTableExist(TableName) == 1;
        }

        //创建表
        public Boolean CreateDataTable(String TableName)
        {
            string[] Sql_Commands = new string[]
            {
                //增加查询条件 Scdel=0    2012-10-19
                "select * from sys_tables where Scdel=0 and tablename = " + "'" + TableName + "'",
                "select * from sys_columns where Scdel=0 and tablename = " + "'" + TableName + "'"
            };

            DataSet Data = GetDataSet(Sql_Commands);

            DataTable tblTable = Data.Tables[0];
            DataTable colTable = Data.Tables[1];

            if (tblTable.Rows.Count == 0) return false;

            if (!HaveDataTable(TableName))
            {
                if (!CreateSystemColumn(TableName))
                    return false;
            }

            string BasicDataType, DispType, Length, DecimalLen, NullAble;
            foreach (DataRow Row in colTable.Rows)
            {
                string ID = Row["ID"].ToString();
                string ColName = Row["COLNAME"].ToString();
                string ColType = Row["COLTYPE"].ToString();

                FieldType Type = FieldType.GetFieldType(ColType);
                BasicDataType = Type.BasicDataType;
                DispType = Type.DisplayType;
                Length = Type.Length.ToString();
                DecimalLen = Type.Decimals.ToString();
                NullAble = Type.NullAble;

                if (!HaveDataColumn(TableName, ColName))
                {
                    if (!AppendDataColumn(TableName, ColName, BasicDataType, DispType, Length, DecimalLen, NullAble))
                        return false;
                }
                else
                {
                    if (!UpdateDataColumn(TableName, ColName, BasicDataType, DispType, Length, DecimalLen, NullAble))
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 创建系统列
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        private Boolean CreateSystemColumn(string TableName)
        {
            try
            {
                if (CreateTable(TableName, Columns, DataTypes, Lengths, IsNULL) == -1)
                    return false;

                if (CreatePrimarykey(TableName, "ID", "PMK_" + TableName + "_ID") == -1)
                    return false;
                
                object defaultvalue = 0;
                for (int j = 1; j < Columns.Length; j++)
                {
                    if (DataTypes[j].ToString() == "DATETIME")
                    {
                        defaultvalue = "GETDATE()";
                    }

                    if (DataTypes[j] == "SMALLINT" || DataTypes[j] == "DATETIME")
                    {
                        if (CreateDefault(TableName, Columns[j], "DFT_" + TableName + "_" + Columns[j], defaultvalue) == -1)
                            return false;
                    }
                }                   

                return true;
            }
            catch
            {
            }

            return false;
        }

        /// <summary>
        /// 追加系统列
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        private Boolean AppendSystemColumn(string TableName)
        {
            try
            {
                for (int j = 0; j < Columns.Length; j++)
                {
                    if (!HaveDataColumn(TableName, Columns[j]))
                    {
                        int r = AddColumnToTable(TableName, Columns[j], DataTypes[j], Lengths[j], IsNULL[j]);
                    }
                }

                object defaultvalue = 0;
                for (int j = 1; j < Columns.Length; j++)
                {
                    if (DataTypes[j].ToString() == "DATETIME")
                    {
                        defaultvalue = "GETDATE()";
                    }

                    if (DataTypes[j] == "SMALLINT" || DataTypes[j] == "DATETIME")
                    {
                        if (CreateDefault(TableName, Columns[j], "DFT_" + TableName + "_" + Columns[j], defaultvalue) == -1)
                            return false;
                    }
                }

                return true;
            }
            catch
            {
            }

            return false;
        }

        //删除表
        public Boolean DeleteDataTable(String TableName)
        {
            try
            {
                IDbConnection conn = this.GetConntion();
                Transaction trans = new Transaction(conn);

                try
                {
                    if (DropPrimarykey(TableName, "PMK_" + TableName + "_ID", trans) == -1)
                    {
                        trans.Rollback();
                        return false;
                    }

                    if (this.DropTable(TableName, trans) == -1)
                    {
                        trans.Rollback();
                        return false;
                    }

                    string[] Sql_Deletes = new string[]
                    {
                        //"delete from sys_tables where tablename = " + "'" + TableName + "'",                    
                        //"delete from sys_columns where tablename = " + "'" + TableName + "'"
                        "Update sys_tables Set Scts_1=Getdate(),Scdel=1 where tablename = " + "'" + TableName + "'",                    
                        "Update sys_columns Set Scts_1=Getdate(),Scdel=1 where tablename = " + "'" + TableName + "'"
                    };

                    int[] xx = ExcuteCommands(Sql_Deletes, trans);
                    for (int i = 0; i < xx.Length; i++)
                    {
                        if (xx[i] == -1)
                        {
                            trans.Rollback();
                            return false;
                        }
                    }

                    trans.Commit();

                    return true;
                }
                catch
                {
                    trans.Rollback();
                }
            }
            catch
            { }

            return false;
        }

        //是否存在列
        public Boolean HaveDataColumn(String TableName,String ColumnName)
        {
            return IsColumnExist(TableName, ColumnName) == 1;
        }

        //追加列
        public Boolean AppendDataColumn(String TableName, String[] ColumnName)
        {
            //增加查询条件Scdel=0     2013-10-19
            string[] Sql_Commands = new string[]
            {
                "select * from sys_columns where Scdel=0 and tablename = " + "'" + TableName + "' and colname in ('" + string.Join("','",ColumnName) + "')"
            };

            DataSet Data = GetDataSet(Sql_Commands);
            DataTable colTable = Data.Tables[0];

            if (colTable.Rows.Count == 0) return false;

            string BasicDataType, DispType, Length, DecimalLen, NullAble;
            foreach (DataRow Row in colTable.Rows)
            {
                string ID = Row["ID"].ToString();
                string ColName = Row["COLNAME"].ToString();
                string ColType = Row["COLTYPE"].ToString();

                FieldType Type = FieldType.GetFieldType(ColType);
                BasicDataType = Type.BasicDataType;
                DispType = Type.DisplayType;
                Length = Type.Length.ToString();
                DecimalLen = Type.Decimals.ToString();
                NullAble = Type.NullAble;

                if (!HaveDataColumn(TableName, ColName))
                {
                    if (!AppendDataColumn(TableName, ColName, BasicDataType, DispType, Length, DecimalLen, NullAble))
                        return false;
                }
            }

            return true;
        }

        //追加列
        private Boolean AppendDataColumn(string TableName, string ColName, string ColType, string DispType, string Length, string DecimalLen, string NullAble)
        {
            try
            {
                if (Length == "0")
                {
                    Length = "";
                }
                else
                {
                    if (ColType.ToUpper() == "DECIMAL")
                    {
                        Length = Length + "," + DecimalLen;
                    }
                }

                if (AddColumnToTable(TableName, ColName, ColType, Length, NullAble) == -1)
                {
                    return false;
                }

                string Default = "";
                if (ColType.ToUpper() == "BIGINT" ||
                    ColType.ToUpper() == "INT" ||
                    ColType.ToUpper() == "SMALLINT" ||
                    ColType.ToUpper() == "DECIMAL")
                {
                    Default = "0";
                }

                if (ColType.ToUpper() == "DATETIME")
                {
                    Default = "GETDATE()";
                }

                if (!string.IsNullOrEmpty(Default))
                {
                    if (CreateDefault(TableName, ColName, "DFT_" + TableName + "_" + ColName, Default) == -1)
                    {
                        return false;
                    }
                }

                return true;
            }
            catch
            { }

            return false;
        }

        //更新列
        public Boolean UpdateDataColumn(String TableName, String[] ColumnName)
        {
            //增加查询条件Scdel=0     2013-10-19
            string[] Sql_Commands = new string[]
            {
                "select * from sys_columns where Scdel=0 and tablename = " + "'" + TableName + "' and colname in ('" + string.Join("','",ColumnName) + "')"
            };

            DataSet Data = GetDataSet(Sql_Commands);
            DataTable colTable = Data.Tables[0];

            if (colTable.Rows.Count == 0) return false;

            string BasicDataType, DispType, Length, DecimalLen, NullAble;
            foreach (DataRow Row in colTable.Rows)
            {
                string ID = Row["ID"].ToString();
                string ColName = Row["COLNAME"].ToString();
                string ColType = Row["COLTYPE"].ToString();

                FieldType Type = FieldType.GetFieldType(ColType);
                BasicDataType = Type.BasicDataType;
                DispType = Type.DisplayType;
                Length = Type.Length.ToString();
                DecimalLen = Type.Decimals.ToString();
                NullAble = Type.NullAble;

                if (HaveDataColumn(TableName, ColName))
                {
                    if (!UpdateDataColumn(TableName, ColName, BasicDataType, DispType, Length, DecimalLen, NullAble))
                        return false;
                }
            }

            return true;
        }

        //更新列
        public Boolean UpdateDataColumn(string TableName, string ColName, string ColType, string DispType, string Length, string DecimalLen, string NullAble)
        {
            Boolean r = true;

            try
            {
                if (Length == "0")
                {
                    Length = "";
                }
                else
                {
                    if (ColType.ToUpper() == "DECIMAL")
                    {
                        Length = Length + "," + DecimalLen;
                    }
                }

                if (IsDefaultExist("DFT_" + TableName + "_" + ColName) == 1)
                {
                    DropDefault(TableName, "DFT_" + TableName + "_" + ColName);
                }

                if (ModifyColumnDataType(TableName, ColName, ColType, Length, NullAble) == -1)
                {
                    return false;
                }

                string Default = "";
                if (ColType.ToUpper() == "BIGINT" ||
                    ColType.ToUpper() == "INT" ||
                    ColType.ToUpper() == "SMALLINT" ||
                    ColType.ToUpper() == "DECIMAL")
                {
                    Default = "0";
                }
                else if (ColType.ToUpper() == "DATETIME")
                {
                    Default = "GETDATE()";
                }

                if (!string.IsNullOrEmpty(Default))
                {
                    if (CreateDefault(TableName, ColName, "DFT_" + TableName + "_" + ColName, Default) == -1)
                    {
                        r = r && false;
                    }
                }

                r = r && true;
            }
            catch
            { }

            return r;
        }

        public Boolean DeleteDataColumn(string TableName, String[] ColumnName)
        {
            Boolean Result = false;

            IDbConnection Connecion = GetConntion();
            Transaction Transaction = new Transaction(Connecion);

            try
            {
                for (int i = 0; i < ColumnName.Length; i++)
                {
                    if (IsDefaultExist("DFT_" + TableName + "_" + ColumnName[i]) == 1)
                    {
                        DropDefault(TableName, "DFT_" + TableName + "_" + ColumnName[i]);
                    }

                    int r = DropColumnFromTable(TableName, ColumnName[i], Transaction);
                    if (i != 0)
                    {
                        Result = Result & (r == 1);
                    }
                    else
                    {
                        Result = (r == 1);
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
