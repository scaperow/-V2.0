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

    #region DataBaseType

    public enum DataBaseType : int
    {
        MSSQLServer2k = 0,
        MSSQLServer2k5 = 1,
        Access = 2,
        Oracle = 3,
        SQLITE = 4,
        UnKnown = 999
    }

    #endregion

    #region Transaction

    public class Transaction : IDisposable
    {

        private IDbConnection _DB;
        private IDbTransaction _DBTransaction;

        public IDbTransaction DBTransaction
        {
            get { return _DBTransaction; }
        }

        public Transaction(IDbConnection Connection)
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
            _DB = Connection;
            _DBTransaction = _DB.BeginTransaction();
        }

        public Transaction(IDbConnection Connection, IsolationLevel IsoLevel)
            : this(Connection, true, IsoLevel)
        {
        }

        public Transaction(IDbConnection Connection, bool aHandle, IsolationLevel IsoLevel)
        {
            if (aHandle)
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }
                _DB = Connection;
                _DBTransaction = _DB.BeginTransaction(IsoLevel);
            }
        }

        public void Commit()
        {
            if (_DB != null)
            {
                _DBTransaction.Commit();
                _DBTransaction.Dispose();
                
                if (_DB.State != ConnectionState.Closed)
                {
                    _DB.Close();
                }
                _DB = null;
            }
        }

        public void Dispose()
        {
            if (_DB != null)
            {
                _DBTransaction.Rollback();
                _DBTransaction.Dispose();

                if (_DB.State != ConnectionState.Closed)
                {
                    _DB.Close();
                }
                _DB = null;
            }
        }

        public void Rollback()
        {
            Dispose();
        }

    }

    #endregion
    
    #region Interface
    public interface IData
    {

        #region DataAccessBase

        #region DataCommon

        string GetConnString(DataBaseType HereType,
            string DataSource,
            string DataBaseInstance,
            string Username,
            string PassWord, bool ISDataBaseAttachFile);

        string GetIntegratedConnString(string DataSource,
            string DataBaseInstance, bool ISDataBaseAttachFile);

        IDbConnection GetConntion(DataBaseType HereType,
            string DataSource,
            string DataBaseInstance,
            string Username,
            string PassWord, bool ISDataBaseAttachFile);

        IDbConnection GetConntion(string ConnectionString);

        IDbDataAdapter GetDataAdapter(string SelectText,
            IDbConnection Connection);

        IDbCommand GetDbCommand(string CommandText,
            IDbConnection Connection);

        DbCommandBuilder CreateAdapterCommand(ref IDbDataAdapter DbDataAdapter);

        string ConnStr { get;set; }

        #endregion DataCommon

        #region DataAccess

        DataTable GetTableStructBySql(string SelectCommandText,
     IDbConnection Connection);

        DataTable GetTableStruct(string TableName,
             IDbConnection Connection);

        DataSet GetTableStructSet(string TableName,
             IDbConnection Connection);

        DataSet GetTableStructsSets(string[] TableNames,
             IDbConnection Connection);

        DataTable[] GetTableStructs(string[] TableNames,
             IDbConnection Connection);

        DataSet GetDataSet(string SelectSqlText,
             IDbConnection Connection);

        DataSet GetDataSet(string SelectSqlText,
            string[] ConditionColumns,
            string[] CompareExpressions,
            object[] ParameterValues,
            IDbConnection Connection);

        DataSet GetDataSet(string SelectSqlText,
            object[] ParameterValues,
            IDbConnection Connection);


        DataSet GetDataSet(string[] SelectSqlText,
            IDbConnection Connection);


        DataSet[] GetDataSets(string[] SelectSqlText,
            IDbConnection Connection);


        DataTable GetDataTable(string SelectSqlText,
            IDbConnection Connection);


        object ExcuteScalar(string SqlCommandText,
            IDbConnection Connection);


        object[] ExcuteScalars(string[] SqlCommandText,
            IDbConnection Connection);


        int ExcuteCommand(string SqlCommandText,
            IDbConnection Connection);

        int RunStoreProcedure(string sp, IDbConnection Connection);

        int[] ExcuteCommands(string[] SqlCommandText,
            IDbConnection Connection);

        Boolean ExcuteCommandsWithTransaction(List<IDbCommand> commands,
            IDbConnection Connection);

        int ExcuteCommand(string SqlCommandText,
            Transaction HereTransaction);


        int[] ExcuteCommands(string[] SqlCommandText,
            Transaction HereTransaction);


        IDataReader ExcuteReader(string SQLCommandText,
            IDbConnection Connection
            );

        int Update(DataSet WillUpdateDataSet,
            IDbConnection Connection);

        int Update(DataTable WillUpdateDataTable,
            IDbConnection Connection);

        int Update(DataRow[] WillUpdateDataRows,
            IDbConnection Connection);

        int Update(DataSet WillUpdateDataSet,
            Transaction HereTransaction);

        int Update(DataTable WillUpdateDataTable,
            Transaction HereTransaction);

        int Update(DataRow[] WillUpdateDataRows,
            Transaction HereTransaction);

        int UpdateByID(DataSet WillUpdateDataSet, IDbConnection Connection);

        /// <summary>
        /// 手动写SQL语句，以ID为条件，
        /// where ID = @ID
        /// </summary>
        /// <param name="WillUpdateDataSet">数据集</param>
        /// <param name="HereTransaction">事务</param>
        /// <returns>1／－1</returns>
        int UpdateByID(DataSet WillUpdateDataSet, Transaction HereTransaction);

        #endregion

        #region appendix

        void AddNullToTable(ref DataSet OriginalSet,
            string TableName,
            int AddRows);


        void AddNullToTable(ref DataSet OriginalSet,
            int TableIndex,
            int AddRows);


        string DataFromSelectToTableName(string select);


        string GetTrueTableName(string TableName);


        string[] DataSplitCommand(string command,
            string splString);


        string DataSpl(string originalstring,
            string firstspl,
            string lastspl);

        #endregion

        #region BlobColumns

        int SaveDataToBlobColumn(DataBaseType HereDataBaseType,
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            byte[] AimValue,
            IDbConnection Connection);

        int SaveDataToBlobColumn(DataBaseType HereDataBaseType,
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            int Offset,
            byte[] AimValue,
            IDbConnection Connection);

        int SaveDataToBlobColumn(DataBaseType HereDataBaseType,
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            string AimValue,
            IDbConnection Connection);

        int SaveDataToBlobColumn(DataBaseType HereDataBaseType,
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            int Offset,
            string AimValue,
            IDbConnection Connection);

        int SaveDataToBlobColumn(DataBaseType HereDataBaseType,
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            byte[] AimValue,
            Transaction HereTransaction);

        int SaveDataToBlobColumn(DataBaseType HereDataBaseType,
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            int Offset,
            byte[] AimValue,
            Transaction HereTransaction);

        int SaveDataToBlobColumn(DataBaseType HereDataBaseType,
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            string AimValue,
            Transaction HereTransaction);

        int SaveDataToBlobColumn(DataBaseType HereDataBaseType,
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            int Offset,
            string AimValue,
            Transaction HereTransaction);

        byte[] ReadDataFromBlobColumn(DataBaseType HereDataBaseType,
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            IDbConnection Connection);


        byte[] ReadDataFromBlobColumn(DataBaseType HereDataBaseType,
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            long Offset,
            int ReadLenght,
            IDbConnection Connection);

        byte[] ReadDataFromBlobColumn(DataBaseType HereDataBaseType,
            string TableName,
            string AimColumnName,
            string Condition,
            long Offset,
            int ReadLenght,
            IDbConnection Connection);

        string ReadTextFromBlobColumn(DataBaseType HereDataBaseType,
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            IDbConnection Connection);


        string ReadTextFromBlobColumn(DataBaseType HereDataBaseType,
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            long Offset,
            int ReadLenght,
            IDbConnection Connection);

        DataSet SelectWithBlobColumnCondition(DataBaseType HereDataBaseType,
            string TableName,
            string SelectColumns,
            string AimColumnName,
            string MatchWords,
            int MatchType,
            IDbConnection Connection);

        long GetBlobDataLength(DataBaseType HereDataBaseType,
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            IDbConnection Connection);

        long GetBlobDataLength(DataBaseType HereDataBaseType,
            string TableName,
            string AimColumnName,
            string Condition,
            IDbConnection Connection);
        #endregion

        #endregion DataAccessBase

        #region DataBaseWatch
        int DataWatch(DataBaseType HereDataBaseType,
            string SqlCondition,
            string ProcessInfo,
            string ProcessType);
        #endregion DataBaseWatch

        #region DataBaseManagement

        string GetMasterBasePath(DataBaseType HereDataBaseType,
            string DataBaseName,
            IDbConnection Connection);


        int IsDatabaseExist(DataBaseType HereDataBaseType,
            string DataBaseName,
            IDbConnection Connection);

        int CreateNewDataBase(DataBaseType HereDataBaseType,
            string DataBaseName,
            string DataBasePath,
            IDbConnection Connection);

        int DropDataBase(DataBaseType HereDataBaseType,
            string DataBaseName,
            IDbConnection Connection);

        string FromTypeToDefaultValueSQL(DataBaseType HereDataBaseType,
            string DataType,
            object DefaultValue);

        int IsDefaultExist(DataBaseType HereDataBaseType,
            string ConstraintName,
            IDbConnection Connection);

        int CreateDefault(DataBaseType HereDataBaseType,
            string TableName,
            string ColumnName,
            string ConstraintName,
            object DefaultValue,
            IDbConnection Connection);

        int CreateDefault(DataBaseType HereDataBaseType,
            string TableName,
            string ColumnName,
            string ConstraintName,
            object DefaultValue,
            Transaction HereTransaction);

        int DropDefault(DataBaseType HereDataBaseType,
            string TableName,
            string ConstraintName,
            IDbConnection Connection);

        int DropDefault(DataBaseType HereDataBaseType,
            string TableName,
            string ConstraintName,
            Transaction HereTransaction);

        int IsPrimarykeyExist(DataBaseType HereDataBaseType,
            string PrimarykeyName,
            IDbConnection Connection);

        int CreatePrimarykey(DataBaseType HereDataBaseType,
            string TableName,
            string ColumnName,
            string Primarykey,
            IDbConnection Connection);

        int CreatePrimarykey(DataBaseType HereDataBaseType,
            string TableName,
            string ColumnName,
            string Primarykey,
            Transaction HereTransaction);

        int DropPrimarykey(DataBaseType HereDataBaseType,
            string TableName,
            string Primarykey,
            IDbConnection Connection);

        int DropPrimarykey(DataBaseType HereDataBaseType,
            string TableName,
            string Primarykey,
            Transaction HereTransaction);

        int IsTableExist(DataBaseType HereDataBaseType,
            string TableName,
            IDbConnection Connection);

        int CreateTable(DataBaseType HereDataBaseType,
            string TableName,
            string[] Columns,
            string[] DataTypes,
            string[] Lenght,
            string[] NullAbles,
            IDbConnection Connection);

        int CreateTable(DataBaseType HereDataBaseType,
            string TableName,
            string[] Columns,
            string[] DataTypes,
            string[] Lenght,
            string[] NullAbles,
            Transaction HereTransaction);

        int DropTable(DataBaseType HereDataBaseType,
            string TableName,
            IDbConnection Connection);

        int DropTable(DataBaseType HereDataBaseType,
            string TableName,
            Transaction HereTransaction);

        int IsColumnExist(DataBaseType HereDataBaseType,
            string TableName,
            string ColumnName,
            IDbConnection Connection);

        int AddColumnToTable(DataBaseType HereDataBaseType,
            string TableName,
            string ColumnName,
            string DataType,
            string Lenght,
            string NullAble,
            IDbConnection Connection);

        int AddColumnToTable(DataBaseType HereDataBaseType,
            string TableName,
            string ColumnName,
            string DataType,
            string Lenght,
            string NullAble,
            Transaction HereTransaction);

        int DropColumnFromTable(DataBaseType HereDataBaseType,
            string TableName,
            string ColumnName,
            IDbConnection Connection);

        int DropColumnFromTable(DataBaseType HereDataBaseType,
            string TableName,
            string ColumnName,
            Transaction HereTransaction);

        int ModifyColumnDataType(DataBaseType HereDataBaseType,
            string TableName,
            string ColumnName,
            string DataType,
            string Lenght,
            string NullAble,
            IDbConnection Connection);

        int ModifyColumnDataType(DataBaseType HereDataBaseType,
            string TableName,
            string ColumnName,
            string DataType,
            string Lenght,
            string NullAble,
            Transaction HereTransaction);

        #endregion DataBaseManagement

    }
    #endregion Interface

}
