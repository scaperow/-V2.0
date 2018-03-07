using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Odbc;
using System.Data.OleDb;
using System.ComponentModel;

namespace Yqun.Data.DataBase
{
    public class OdbcData:IData
    {
        #region IData 成员

        string _ConnStr = "";
        public string ConnStr
        {
            get
            {
                return _ConnStr;
            }
            set
            {
                _ConnStr = value;
            }
        }

        public string GetConnString(DataBaseType HereType, string DataSource, string DataBaseInstance, string Username, string PassWord, bool ISDataBaseAttachFile)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public string GetIntegratedConnString(string DataSource, string DataBaseInstance, bool ISDataBaseAttachFile)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public System.Data.IDbConnection GetConntion(DataBaseType HereType, string DataSource, string DataBaseInstance, string Username, string PassWord, bool ISDataBaseAttachFile)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public System.Data.IDbConnection GetConntion(string ConnectionString)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public System.Data.IDbDataAdapter GetDataAdapter(string SelectText, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public System.Data.IDbCommand GetDbCommand(string CommandText, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public System.Data.Common.DbCommandBuilder CreateAdapterCommand(ref System.Data.IDbDataAdapter DbDataAdapter)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public System.Data.DataTable GetTableStructBySql(string SelectCommandText, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public System.Data.DataTable GetTableStruct(string TableName, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public System.Data.DataSet GetTableStructSet(string TableName, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public System.Data.DataSet GetTableStructsSets(string[] TableNames, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public System.Data.DataTable[] GetTableStructs(string[] TableNames, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public System.Data.DataSet GetDataSet(string SelectSqlText, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public System.Data.DataSet GetDataSet(string SelectSqlText, string[] ConditionColumns, string[] CompareExpressions, object[] ParameterValues, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public System.Data.DataSet GetDataSet(string SelectSqlText, object[] ParameterValues, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public System.Data.DataSet GetDataSet(string[] SelectSqlText, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public System.Data.DataSet[] GetDataSets(string[] SelectSqlText, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public System.Data.DataTable GetDataTable(string SelectSqlText, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public object ExcuteScalar(string SqlCommandText, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public object[] ExcuteScalars(string[] SqlCommandText, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int RunStoreProcedure(string sp, IDbConnection Connection)
        {
            return -1;
        }

        public int ExcuteCommand(string SqlCommandText, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int[] ExcuteCommands(string[] SqlCommandText, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int ExcuteCommand(string SqlCommandText, Transaction HereTransaction)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int[] ExcuteCommands(string[] SqlCommandText, Transaction HereTransaction)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public System.Data.IDataReader ExcuteReader(string SQLCommandText, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Update(System.Data.DataSet WillUpdateDataSet, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Update(System.Data.DataTable WillUpdateDataTable, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Update(System.Data.DataRow[] WillUpdateDataRows, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Update(System.Data.DataSet WillUpdateDataSet, Transaction HereTransaction)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Update(System.Data.DataTable WillUpdateDataTable, Transaction HereTransaction)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Update(System.Data.DataRow[] WillUpdateDataRows, Transaction HereTransaction)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UpdateByID(System.Data.DataSet WillUpdateDataSet, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UpdateByID(System.Data.DataSet WillUpdateDataSet, Transaction HereTransaction)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void AddNullToTable(ref System.Data.DataSet OriginalSet, string TableName, int AddRows)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void AddNullToTable(ref System.Data.DataSet OriginalSet, int TableIndex, int AddRows)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public string DataFromSelectToTableName(string select)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public string GetTrueTableName(string TableName)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public string[] DataSplitCommand(string command, string splString)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public string DataSpl(string originalstring, string firstspl, string lastspl)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int SaveDataToBlobColumn(DataBaseType HereDataBaseType, string TableName, string IDColumnName, string IDValue, string AimColumnName, byte[] AimValue, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int SaveDataToBlobColumn(DataBaseType HereDataBaseType, string TableName, string IDColumnName, string IDValue, string AimColumnName, int Offset, byte[] AimValue, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int SaveDataToBlobColumn(DataBaseType HereDataBaseType, string TableName, string IDColumnName, string IDValue, string AimColumnName, string AimValue, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int SaveDataToBlobColumn(DataBaseType HereDataBaseType, string TableName, string IDColumnName, string IDValue, string AimColumnName, int Offset, string AimValue, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int SaveDataToBlobColumn(DataBaseType HereDataBaseType, string TableName, string IDColumnName, string IDValue, string AimColumnName, byte[] AimValue, Transaction HereTransaction)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int SaveDataToBlobColumn(DataBaseType HereDataBaseType, string TableName, string IDColumnName, string IDValue, string AimColumnName, int Offset, byte[] AimValue, Transaction HereTransaction)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int SaveDataToBlobColumn(DataBaseType HereDataBaseType, string TableName, string IDColumnName, string IDValue, string AimColumnName, string AimValue, Transaction HereTransaction)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int SaveDataToBlobColumn(DataBaseType HereDataBaseType, string TableName, string IDColumnName, string IDValue, string AimColumnName, int Offset, string AimValue, Transaction HereTransaction)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public byte[] ReadDataFromBlobColumn(DataBaseType HereDataBaseType, string TableName, string IDColumnName, string IDValue, string AimColumnName, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public byte[] ReadDataFromBlobColumn(DataBaseType HereDataBaseType, string TableName, string AimColumnName, string Condition, long Offset, int ReadLenght, IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public byte[] ReadDataFromBlobColumn(DataBaseType HereDataBaseType, string TableName, string IDColumnName, string IDValue, string AimColumnName, long Offset, int ReadLenght, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public string ReadTextFromBlobColumn(DataBaseType HereDataBaseType, string TableName, string IDColumnName, string IDValue, string AimColumnName, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public string ReadTextFromBlobColumn(DataBaseType HereDataBaseType, string TableName, string IDColumnName, string IDValue, string AimColumnName, long Offset, int ReadLenght, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public System.Data.DataSet SelectWithBlobColumnCondition(DataBaseType HereDataBaseType, string TableName, string SelectColumns, string AimColumnName, string MatchWords, int MatchType, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public long GetBlobDataLength(DataBaseType HereDataBaseType, string TableName, string IDColumnName, string IDValue, string AimColumnName, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public long GetBlobDataLength(DataBaseType HereDataBaseType, string TableName, string AimColumnName, string Condition, IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented");
        }

        public int DataWatch(DataBaseType HereDataBaseType, string SqlCondition, string ProcessInfo, string ProcessType)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public string GetMasterBasePath(DataBaseType HereDataBaseType, string DataBaseName, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int IsDatabaseExist(DataBaseType HereDataBaseType, string DataBaseName, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int CreateNewDataBase(DataBaseType HereDataBaseType, string DataBaseName, string DataBasePath, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int DropDataBase(DataBaseType HereDataBaseType, string DataBaseName, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public string FromTypeToDefaultValueSQL(DataBaseType HereDataBaseType, string DataType, object DefaultValue)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int IsDefaultExist(DataBaseType HereDataBaseType, string ConstraintName, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int CreateDefault(DataBaseType HereDataBaseType, string TableName, string ColumnName, string ConstraintName, object DefaultValue, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int CreateDefault(DataBaseType HereDataBaseType, string TableName, string ColumnName, string ConstraintName, object DefaultValue, Transaction HereTransaction)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int DropDefault(DataBaseType HereDataBaseType, string TableName, string ConstraintName, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int DropDefault(DataBaseType HereDataBaseType, string TableName, string ConstraintName, Transaction HereTransaction)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int IsPrimarykeyExist(DataBaseType HereDataBaseType, string PrimarykeyName, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int CreatePrimarykey(DataBaseType HereDataBaseType, string TableName, string ColumnName, string Primarykey, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int CreatePrimarykey(DataBaseType HereDataBaseType, string TableName, string ColumnName, string Primarykey, Transaction HereTransaction)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int DropPrimarykey(DataBaseType HereDataBaseType, string TableName, string Primarykey, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int DropPrimarykey(DataBaseType HereDataBaseType, string TableName, string Primarykey, Transaction HereTransaction)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int IsTableExist(DataBaseType HereDataBaseType, string TableName, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int CreateTable(DataBaseType HereDataBaseType, string TableName, string[] Columns, string[] DataTypes, string[] Lenght, string[] NullAbles, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int CreateTable(DataBaseType HereDataBaseType, string TableName, string[] Columns, string[] DataTypes, string[] Lenght, string[] NullAbles, Transaction HereTransaction)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int DropTable(DataBaseType HereDataBaseType, string TableName, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int DropTable(DataBaseType HereDataBaseType, string TableName, Transaction HereTransaction)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int IsColumnExist(DataBaseType HereDataBaseType, string TableName, string ColumnName, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int AddColumnToTable(DataBaseType HereDataBaseType, string TableName, string ColumnName, string DataType, string Lenght, string NullAble, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int AddColumnToTable(DataBaseType HereDataBaseType, string TableName, string ColumnName, string DataType, string Lenght, string NullAble, Transaction HereTransaction)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int DropColumnFromTable(DataBaseType HereDataBaseType, string TableName, string ColumnName, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int DropColumnFromTable(DataBaseType HereDataBaseType, string TableName, string ColumnName, Transaction HereTransaction)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int ModifyColumnDataType(DataBaseType HereDataBaseType, string TableName, string ColumnName, string DataType, string Lenght, string NullAble, System.Data.IDbConnection Connection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int ModifyColumnDataType(DataBaseType HereDataBaseType, string TableName, string ColumnName, string DataType, string Lenght, string NullAble, Transaction HereTransaction)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region IData 成员


        public bool ExcuteCommandsWithTransaction(List<IDbCommand> commands, IDbConnection Connection)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
