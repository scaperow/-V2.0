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
    public class DataService
    {
        #region private

        private DataBaseType HereDataBaseType = DataBaseType.MSSQLServer2k5;
        private IData DACClass;
        private string AppRoot = "";
        private bool ISIntegrated = false;
        private string DataSource = "";
        private string Instance = "";
        private string UserName = "";
        private string Password = "";
        private bool ISAttach = false;

        #endregion

        IDbConnection _Connection;
        public IDbConnection Connection 
        {
            get 
            {
                _Connection = this.GetConntion();
                return _Connection;
            }
        }

        #region Constructure

        public string GetTableDescription(string tableName)
        {
            //增加查询条件 Scdel=0    2012-10-19
            string des = Convert.ToString(this.ExcuteScalar("select DESCRIPTION from SYS_TABLES where Scdel=0 and TABLENAME ='" + tableName + "'"));
            return des;
        }

        /// <summary>
        /// 使用参数配置数据引擎，静态上下文在此方法无效
        /// </summary>
        /// <param name="_DataAdapterType">sqlclient,oledb,odbc</param>
        /// <param name="_DataBaseType">mssqlserver2k5,mssqlserver2k,access,oracle</param>
        /// <param name="_ISIntegrated">是否集成Windows安全</param>
        /// <param name="_DataSource">数据源</param>
        /// <param name="_Instance">数据库实例</param>
        /// <param name="_UserName">用户名</param>
        /// <param name="_Password">密码</param>
        /// <param name="_ISAttach">是否附加文件为数据库实例</param>
        /// <param name="_AppRoot">程序执行顶级目录</param>
        [Description("使用参数配置数据引擎，静态上下文在此方法无效")]
        public DataService(string _DataAdapterType,
            string _DataBaseType,
            bool _ISIntegrated,
            string _DataSource,
            string _Instance,
            string _UserName,
            string _Password,
            bool _ISAttach,
            string _AppRoot
            )
        {
            if (_DataAdapterType.ToLower().Trim() == "sqlclient")
            {
                SQLClientData d = new SQLClientData();
                DACClass = (IData)d;
            }
            else if (_DataAdapterType.ToLower().Trim() == "oledb")
            {
                OleDbData d = new OleDbData();
                DACClass = (IData)d;
            }
            else if (_DataAdapterType.ToLower().Trim() == "odbc")
            {
                OdbcData d = new OdbcData();
                DACClass = (IData)d;
            }
            else if (_DataAdapterType.ToLower().Trim() == "sqlite")
            {
                SQLiteData d = new SQLiteData();
                DACClass = (IData)d;
            }
            else
            {
                OleDbData d = new OleDbData();
                DACClass = (IData)d;
            }

            if (_DataBaseType.ToLower().Trim() == "mssqlserver2k5")
            {
                HereDataBaseType = DataBaseType.MSSQLServer2k5;
            }
            else if (_DataBaseType.ToLower().Trim() == "mssqlserver2k")
            {
                HereDataBaseType = DataBaseType.MSSQLServer2k;
            }
            else if (_DataBaseType.ToLower().Trim() == "access")
            {
                HereDataBaseType = DataBaseType.Access;
            }
            else if (_DataBaseType.ToLower().Trim() == "oracle")
            {
                HereDataBaseType = DataBaseType.Oracle;
            }
            else if (_DataBaseType.ToLower().Trim() == "sqlite")
            {
                HereDataBaseType = DataBaseType.SQLITE;
            }
            else
            {
                HereDataBaseType = DataBaseType.UnKnown;
            }

            this.ISIntegrated = _ISIntegrated;
            this.DataSource = _DataSource;
            this.Instance = _Instance;
            this.UserName = _UserName;
            this.Password = _Password;
            this.ISAttach = _ISAttach;
            this.AppRoot = _AppRoot;
        }

        public void CreateInstance(string _DataAdapterType,
            string _DataBaseType,
            bool _ISIntegrated,
            string _DataSource,
            string _Instance,
            string _UserName,
            string _Password,
            bool _ISAttach,
            string _AppRoot
            )
        {
            if (_DataAdapterType.ToLower().Trim() == "sqlclient")
            {
                SQLClientData d = new SQLClientData();
                DACClass = (IData)d;
            }
            else if (_DataAdapterType.ToLower().Trim() == "oledb")
            {
                OleDbData d = new OleDbData();
                DACClass = (IData)d;
            }
            else if (_DataAdapterType.ToLower().Trim() == "odbc")
            {
                OdbcData d = new OdbcData();
                DACClass = (IData)d;
            }
            else if (_DataAdapterType.ToLower().Trim() == "sqlite")
            {
                SQLiteData d = new SQLiteData();
                DACClass = (IData)d;
            }
            else
            {
                OleDbData d = new OleDbData();
                DACClass = (IData)d;
            }

            if (_DataBaseType.ToLower().Trim() == "mssqlserver2k5")
            {
                HereDataBaseType = DataBaseType.MSSQLServer2k5;
            }
            else if (_DataBaseType.ToLower().Trim() == "mssqlserver2k")
            {
                HereDataBaseType = DataBaseType.MSSQLServer2k;
            }
            else if (_DataBaseType.ToLower().Trim() == "access")
            {
                HereDataBaseType = DataBaseType.Access;
            }
            else if (_DataBaseType.ToLower().Trim() == "oracle")
            {
                HereDataBaseType = DataBaseType.Oracle;
            }
            else if (_DataBaseType.ToLower().Trim() == "sqlite")
            {
                HereDataBaseType = DataBaseType.SQLITE;
            }
            else
            {
                HereDataBaseType = DataBaseType.UnKnown;
            }

            this.ISIntegrated = _ISIntegrated;
            this.DataSource = _DataSource;
            this.Instance = _Instance;
            this.UserName = _UserName;
            this.Password = _Password;
            this.ISAttach = _ISAttach;
            this.AppRoot = _AppRoot;
        }


        /// <summary>
        /// 使用静态上下文属性配置数据引擎
        /// </summary>
        public DataService()
        {

        }

        #endregion

        #region IData Members

        #region DataAccessBase

        #region DataCommon

        String AppPath = "[apppath]";
        public string GetConnString()
        {
            string conn = "";
            string trimAppRoot = this.AppRoot.TrimEnd('\\');

            if (this.HereDataBaseType == DataBaseType.MSSQLServer2k5 ||
                this.HereDataBaseType == DataBaseType.MSSQLServer2k||
                this.HereDataBaseType== DataBaseType.SQLITE
                )
            {
                if (this.ISIntegrated)
                {
                    string ins = this.Instance;
                    if (ins.ToLower().IndexOf(AppPath) != -1)
                    {
                        ins = ins.ToLower().Replace(AppPath, trimAppRoot);
                    }

                    string sorce = this.DataSource;
                    if (sorce.ToLower().IndexOf(AppPath) != -1)
                    {
                        sorce = sorce.ToLower().Replace(AppPath, trimAppRoot);
                        this.DataSource = sorce;
                    }
                    
                    conn = DACClass.GetIntegratedConnString(this.DataSource,ins,this.ISAttach);
                }
                else
                {
                    string ins = this.Instance;
                    if (ins.ToLower().IndexOf(AppPath) != -1)
                    {
                        ins = ins.ToLower().Replace(AppPath, trimAppRoot);
                    }

                    string sorce = this.DataSource;
                    if (sorce.ToLower().IndexOf(AppPath) != -1)
                    {
                        sorce = sorce.ToLower().Replace(AppPath, trimAppRoot);
                        this.DataSource = sorce;
                    }

                    conn = DACClass.GetConnString(this.HereDataBaseType, this.DataSource,ins, this.UserName,this.Password,this.ISAttach);

                }
            }
            else
            {
                conn = DACClass.GetConnString(this.HereDataBaseType,
                    this.DataSource,
                    this.Instance,
                    this.UserName,
                    this.Password,
                    this.ISAttach);
            }
            return conn;

        }

        public System.Data.IDbConnection GetConntion()
        {
            string conn = this.GetConnString();
            DACClass.ConnStr = conn;
            return DACClass.GetConntion(conn);
        }

        public System.Data.IDbConnection GetConntion(string ConnectionString)
        {
            return DACClass.GetConntion(ConnectionString);
        }

        public System.Data.IDbDataAdapter GetDataAdapter(string SelectText)
        {
            return DACClass.GetDataAdapter(SelectText, Connection);
        }

        public System.Data.IDbDataAdapter GetDataAdapter(string SelectText,
            System.Data.IDbConnection Connection)
        {
            return DACClass.GetDataAdapter(SelectText, Connection);
        }

        public System.Data.IDbCommand GetDbCommand(string CommandText)
        {
            return DACClass.GetDbCommand(CommandText, Connection);
        }

        public System.Data.IDbCommand GetDbCommand(string CommandText,
            System.Data.IDbConnection Connection)
        {
            return DACClass.GetDbCommand(CommandText, Connection);
        }

        public System.Data.Common.DbCommandBuilder CreateAdapterCommand(ref System.Data.IDbDataAdapter DbDataAdapter)
        {
            return DACClass.CreateAdapterCommand(ref DbDataAdapter);
        }

        #endregion

        #region DataAccess

        public DataTable GetTableStructBySql(string SelectCommandText,
           IDbConnection Connection)
        {
            return DACClass.GetTableStructBySql(SelectCommandText,Connection);
        }

        public DataTable GetTableStructBySql(string SelectCommandText)
        {
            return DACClass.GetTableStructBySql(SelectCommandText, Connection);
        }

        public DataTable GetTableStruct(string TableName,IDbConnection Connection)
        {
            return DACClass.GetTableStruct(TableName, Connection);
        }

        public DataTable GetTableStruct(string TableName)
        {
            return DACClass.GetTableStruct(TableName, Connection);
        }

        public DataSet GetTableStructSet(string TableName,
             IDbConnection Connection)
        {
            return DACClass.GetTableStructSet(TableName,
             Connection);
        }

        public DataSet GetTableStructSet(string TableName)
        {
            return DACClass.GetTableStructSet(TableName,
             Connection);
        }

        public DataSet GetTableStructsSets(string[] TableNames,
             IDbConnection Connection)
        {
            return DACClass.GetTableStructsSets(TableNames,
             Connection);
        }

        public DataSet GetTableStructsSets(string[] TableNames)
        {
            return DACClass.GetTableStructsSets(TableNames,
             Connection);
        }

        public DataTable[] GetTableStructs(string[] TableNames,
             IDbConnection Connection)
        {
            return DACClass.GetTableStructs(TableNames,
             Connection);
        }

        public DataTable[] GetTableStructs(string[] TableNames)
        {
            return DACClass.GetTableStructs(TableNames,
             Connection);
        }

        public System.Data.DataSet GetDataSet(string SelectSqlText)
        {
            return DACClass.GetDataSet(SelectSqlText, Connection);
        }

        public System.Data.DataSet GetDataSet(string SelectSqlText,
            System.Data.IDbConnection Connection)
        {
            return DACClass.GetDataSet(SelectSqlText, Connection);
        }

        public DataSet GetDataSet(string SelectSqlText,
            string[] ConditionColumns,
            string[] CompareExpressions,
            object[] ParameterValues)
        {
            return DACClass.GetDataSet(SelectSqlText,
                ConditionColumns,
                CompareExpressions,
                ParameterValues,
                Connection);
        }

        public DataSet GetDataSet(string SelectSqlText,
            string[] ConditionColumns,
            string[] CompareExpressions,
            object[] ParameterValues,
            IDbConnection Connection)
        {
            return DACClass.GetDataSet(SelectSqlText,
                ConditionColumns,
                CompareExpressions,
                ParameterValues,
                Connection);
        }

        public DataSet GetDataSet(string SelectSqlText,
            object[] ParameterValues,
            IDbConnection Connection)
        {

            return DACClass.GetDataSet(SelectSqlText, ParameterValues, Connection);
        }

        public DataSet GetDataSet(string SelectSqlText,
            object[] ParameterValues)
        {

            return DACClass.GetDataSet(SelectSqlText, ParameterValues, Connection);
        }

        public System.Data.DataSet GetDataSet(string[] SelectSqlText)
        {
            return DACClass.GetDataSet(SelectSqlText, Connection);
        }

        public System.Data.DataSet GetDataSet(string[] SelectSqlText,
            System.Data.IDbConnection Connection)
        {
            return DACClass.GetDataSet(SelectSqlText, Connection);
        }

        public System.Data.DataSet[] GetDataSets(string[] SelectSqlText)
        {
            return DACClass.GetDataSets(SelectSqlText, Connection);
        }

        public System.Data.DataSet[] GetDataSets(string[] SelectSqlText,
            System.Data.IDbConnection Connection)
        {
            return DACClass.GetDataSets(SelectSqlText, Connection);
        }

        public System.Data.DataTable GetDataTable(string SelectSqlText)
        {
            return DACClass.GetDataTable(SelectSqlText, Connection);
        }

        public System.Data.DataTable GetDataTable(string SelectSqlText,
            System.Data.IDbConnection Connection)
        {
            return DACClass.GetDataTable(SelectSqlText, Connection);
        }

        public object ExcuteScalar(string SqlCommandText)
        {
            return DACClass.ExcuteScalar(SqlCommandText, Connection);
        }

        public object ExcuteScalar(string SqlCommandText,
            System.Data.IDbConnection Connection)
        {
            return DACClass.ExcuteScalar(SqlCommandText, Connection);
        }

        public object[] ExcuteScalars(string[] SqlCommandText)
        {
            return DACClass.ExcuteScalars(SqlCommandText,
            Connection);
        }

        public object[] ExcuteScalars(string[] SqlCommandText,
            System.Data.IDbConnection Connection)
        {
            return DACClass.ExcuteScalars(SqlCommandText,
            Connection);
        }

        public int ExcuteCommand(string SqlCommandText)
        {
            return DACClass.ExcuteCommand(SqlCommandText, Connection);
        }

        public int ExcuteCommand(string SqlCommandText,
            System.Data.IDbConnection Connection)
        {
            return DACClass.ExcuteCommand(SqlCommandText, Connection);
        }

        public int[] ExcuteCommands(string[] SqlCommandText)
        {
            return DACClass.ExcuteCommands(SqlCommandText, Connection);
        }

        /// <summary>
        /// 执行带事物的操作
        /// </summary>
        /// <param name="commands"></param>
        /// <returns></returns>
        public Boolean ExcuteCommandsWithTransaction(List<IDbCommand> commands)
        {
            return DACClass.ExcuteCommandsWithTransaction(commands, Connection);
        }

        public int[] ExcuteCommands(string[] SqlCommandText,
            System.Data.IDbConnection Connection)
        {
            return DACClass.ExcuteCommands(SqlCommandText, Connection);
        }

        public int ExcuteCommand(string SqlCommandText,
            Transaction HereTransaction)
        {
            return DACClass.ExcuteCommand(SqlCommandText, HereTransaction);
        }

        public int[] ExcuteCommands(string[] SqlCommandText,
            Transaction HereTransaction)
        {
            return DACClass.ExcuteCommands(SqlCommandText, HereTransaction);
        }

        public System.Data.IDataReader ExcuteReader(string SQLCommandText,
            System.Data.IDbConnection Connection)
        {
            return DACClass.ExcuteReader(SQLCommandText, Connection);
        }

        public System.Data.IDataReader ExcuteReader(string SQLCommandText)
        {
            return DACClass.ExcuteReader(SQLCommandText, Connection);
        }

        public int Update(System.Data.DataSet WillUpdateDataSet)
        {
            return DACClass.Update(WillUpdateDataSet, Connection);
        }

        public int Update(System.Data.DataSet WillUpdateDataSet,
            System.Data.IDbConnection Connection)
        {
            return DACClass.Update(WillUpdateDataSet, Connection);
        }

        public int Update(System.Data.DataTable WillUpdateDataTable)
        {
            return DACClass.Update(WillUpdateDataTable, Connection);
        }

        public int Update(System.Data.DataTable WillUpdateDataTable,
            System.Data.IDbConnection Connection)
        {
            return DACClass.Update(WillUpdateDataTable, Connection);
        }

        public int Update(System.Data.DataRow[] WillUpdateDataRows)
        {
            return DACClass.Update(WillUpdateDataRows, Connection);
        }

        public int Update(System.Data.DataRow[] WillUpdateDataRows,
            System.Data.IDbConnection Connection)
        {
            return DACClass.Update(WillUpdateDataRows, Connection);
        }

        public int Update(System.Data.DataSet WillUpdateDataSet,
            Transaction HereTransaction)
        {
            return DACClass.Update(WillUpdateDataSet, HereTransaction);
        }

        public int Update(System.Data.DataTable WillUpdateDataTable, Transaction HereTransaction)
        {
            return DACClass.Update(WillUpdateDataTable, HereTransaction);
        }

        public int Update(System.Data.DataRow[] WillUpdateDataRows, Transaction HereTransaction)
        {
            return DACClass.Update(WillUpdateDataRows, HereTransaction);
        }

        public int UpdateByID(DataSet WillUpdateDataSet, IDbConnection Connection)
        {
            return DACClass.UpdateByID(WillUpdateDataSet,Connection);
        }

        public int UpdateByID(DataSet WillUpdateDataSet)
        {
            return DACClass.UpdateByID(WillUpdateDataSet, Connection);
        }

        public int UpdateByID(DataSet WillUpdateDataSet, Transaction HereTransaction)
        {
            return DACClass.UpdateByID(WillUpdateDataSet, HereTransaction);
        }

        #endregion

        #region appendix

        public void AddNullToTable(ref System.Data.DataSet OriginalSet, string TableName, int AddRows)
        {
            DACClass.AddNullToTable(ref OriginalSet, TableName, AddRows);
        }

        public void AddNullToTable(ref System.Data.DataSet OriginalSet, int TableIndex, int AddRows)
        {
            DACClass.AddNullToTable(ref OriginalSet, TableIndex, AddRows);
        }

        public string DataFromSelectToTableName(string select)
        {
            return DACClass.DataFromSelectToTableName(select);
        }

        public string GetTrueTableName(string TableName)
        {
            return DACClass.GetTrueTableName(TableName);
        }

        public string[] DataSplitCommand(string command, string splString)
        {
            return DACClass.DataSplitCommand(command, splString);
        }

        public string DataSpl(string originalstring, string firstspl, string lastspl)
        {
            return DACClass.DataSpl(originalstring, firstspl, lastspl);
        }

        #endregion

        #region BlobColumns

        public int SaveDataToBlobColumn(
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            byte[] AimValue)
        {
            return DACClass.SaveDataToBlobColumn(HereDataBaseType,
                    TableName,
                    IDColumnName,
                    IDValue,
                    AimColumnName,
                    AimValue,
                    Connection);
        }

        public int SaveDataToBlobColumn(
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            byte[] AimValue,
            IDbConnection Connection)
        {
            return DACClass.SaveDataToBlobColumn(HereDataBaseType,
                    TableName,
                    IDColumnName,
                    IDValue,
                    AimColumnName,
                    AimValue,
                    Connection);
        }

        public int SaveDataToBlobColumn(
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            int Offset,
            byte[] AimValue)
        {
            return DACClass.SaveDataToBlobColumn(HereDataBaseType,
                    TableName,
                    IDColumnName,
                    IDValue,
                    AimColumnName,
                    Offset,
                    AimValue,
                    Connection);
        }

        public int SaveDataToBlobColumn(
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            int Offset,
            byte[] AimValue,
            IDbConnection Connection)
        {
            return DACClass.SaveDataToBlobColumn(HereDataBaseType,
                    TableName,
                    IDColumnName,
                    IDValue,
                    AimColumnName,
                    Offset,
                    AimValue,
                    Connection);
        }

        public int SaveDataToBlobColumn(
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            string AimValue)
        {
            return DACClass.SaveDataToBlobColumn(HereDataBaseType,
                    TableName,
                    IDColumnName,
                    IDValue,
                    AimColumnName,
                    AimValue,
                    Connection);
        }

        public int SaveDataToBlobColumn(
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            string AimValue,
            IDbConnection Connection)
        {
            return DACClass.SaveDataToBlobColumn(HereDataBaseType,
                    TableName,
                    IDColumnName,
                    IDValue,
                    AimColumnName,
                    AimValue,
                    Connection);
        }

        public int SaveDataToBlobColumn(
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            int Offset,
            string AimValue
            )
        {
            return DACClass.SaveDataToBlobColumn(HereDataBaseType,
                    TableName,
                    IDColumnName,
                    IDValue,
                    AimColumnName,
                    Offset,
                    AimValue,
                    Connection);
        }

        public int SaveDataToBlobColumn(
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            int Offset,
            string AimValue,
            IDbConnection Connection)
        {
            return DACClass.SaveDataToBlobColumn(HereDataBaseType,
                    TableName,
                    IDColumnName,
                    IDValue,
                    AimColumnName,
                    Offset,
                    AimValue,
                    Connection);
        }

        public int SaveDataToBlobColumn(string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            byte[] AimValue,
            Transaction HereTransaction)
        {
            return DACClass.SaveDataToBlobColumn(HereDataBaseType,
                TableName,
                IDColumnName,
                IDValue,
                AimColumnName,
                AimValue,
                HereTransaction);
        }

        public int SaveDataToBlobColumn(
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            int Offset,
            byte[] AimValue,
            Transaction HereTransaction)
        {
            return DACClass.SaveDataToBlobColumn(HereDataBaseType,
                    TableName,
                    IDColumnName,
                    IDValue,
                    AimColumnName,
                    Offset,
                    AimValue,
                    HereTransaction);
        }

        public int SaveDataToBlobColumn(
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            string AimValue,
            Transaction HereTransaction)
        {
            return DACClass.SaveDataToBlobColumn(HereDataBaseType,
                    TableName,
                    IDColumnName,
                    IDValue,
                    AimColumnName,
                    AimValue,
                    HereTransaction);
        }

        public int SaveDataToBlobColumn(
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            int Offset,
            string AimValue,
            Transaction HereTransaction)
        {
            return DACClass.SaveDataToBlobColumn(HereDataBaseType,
                    TableName,
                    IDColumnName,
                    IDValue,
                    AimColumnName,
                    AimValue,
                    HereTransaction);
        }

        public byte[] ReadDataFromBlobColumn(
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName)
        {
            return DACClass.ReadDataFromBlobColumn(HereDataBaseType,
                    TableName,
                    IDColumnName,
                    IDValue,
                    AimColumnName,
                    Connection);
        }

        public byte[] ReadDataFromBlobColumn(
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            System.Data.IDbConnection Connection)
        {
            return DACClass.ReadDataFromBlobColumn(HereDataBaseType,
                    TableName,
                    IDColumnName,
                    IDValue,
                    AimColumnName,
                    Connection);
        }

        public byte[] ReadDataFromBlobColumn(
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            long Offset,
            int ReadLenght)
        {
            return DACClass.ReadDataFromBlobColumn(HereDataBaseType,
                    TableName,
                    IDColumnName,
                    IDValue,
                    AimColumnName,
                    Offset,
                    ReadLenght,
                    Connection);
        }

        public byte[] ReadDataFromBlobColumn(
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            long Offset,
            int ReadLenght,
            System.Data.IDbConnection Connection)
        {
            return DACClass.ReadDataFromBlobColumn(HereDataBaseType,
                    TableName,
                    IDColumnName,
                    IDValue,
                    AimColumnName,
                    Offset,
                    ReadLenght,
                    Connection);
        }

        public byte[] ReadDataFromBlobColumn(string TableName, string AimColumnName, string Condition, long Offset, int ReadLenght)
        {
            return DACClass.ReadDataFromBlobColumn(HereDataBaseType, TableName, AimColumnName, Condition, Offset, ReadLenght, Connection);
        }

        public string ReadTextFromBlobColumn(
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName)
        {
            return DACClass.ReadTextFromBlobColumn(HereDataBaseType,
                    TableName,
                    IDColumnName,
                    IDValue,
                    AimColumnName,
                    Connection);
        }

        public string ReadTextFromBlobColumn(
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            System.Data.IDbConnection Connection)
        {
            return DACClass.ReadTextFromBlobColumn(HereDataBaseType,
                    TableName,
                    IDColumnName,
                    IDValue,
                    AimColumnName,
                    Connection);
        }


        public string ReadTextFromBlobColumn(
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            long Offset,
            int ReadLenght)
        {
            return DACClass.ReadTextFromBlobColumn(HereDataBaseType,
                    TableName,
                    IDColumnName,
                    IDValue,
                    AimColumnName,
                    Offset,
                    ReadLenght,
                    Connection);
        }

        public string ReadTextFromBlobColumn(
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            long Offset,
            int ReadLenght,
            System.Data.IDbConnection Connection)
        {
            return DACClass.ReadTextFromBlobColumn(HereDataBaseType,
                    TableName,
                    IDColumnName,
                    IDValue,
                    AimColumnName,
                    Offset,
                    ReadLenght,
                    Connection);
        }

        public System.Data.DataSet SelectWithBlobColumnCondition(
            string TableName,
            string SelectColumns,
            string AimColumnName,
            string MatchWords,
            int MatchType)
        {
            return DACClass.SelectWithBlobColumnCondition(HereDataBaseType,
                    TableName,
                    SelectColumns,
                    AimColumnName,
                    MatchWords,
                    MatchType,
                    Connection);
        }

        public System.Data.DataSet SelectWithBlobColumnCondition(
            string TableName,
            string SelectColumns,
            string AimColumnName,
            string MatchWords,
            int MatchType,
            System.Data.IDbConnection Connection)
        {
            return DACClass.SelectWithBlobColumnCondition(HereDataBaseType,
                    TableName,
                    SelectColumns,
                    AimColumnName,
                    MatchWords,
                    MatchType,
                    Connection);
        }

        public long GetBlobDataLength(
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName)
        {
            return DACClass.GetBlobDataLength(HereDataBaseType,
                    TableName,
                    IDColumnName,
                    IDValue,
                    AimColumnName,
                    Connection);
        }

        public long GetBlobDataLength(
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            System.Data.IDbConnection Connection)
        {
            return DACClass.GetBlobDataLength(HereDataBaseType,
                    TableName,
                    IDColumnName,
                    IDValue,
                    AimColumnName,
                    Connection);
        }

        public long GetBlobDataLength(string TableName, string AimColumnName, string Condition)
        {
            return DACClass.GetBlobDataLength(HereDataBaseType, TableName, AimColumnName, Condition, Connection);
        }

        #endregion

        #endregion

        #region DataWatch

        public int DataWatch(DataBaseType HereDataBaseType,
            string SqlCondition,
            string ProcessInfo,
            string ProcessType)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region DataBaseManagement

        public string GetMasterBasePath(
            string DataBaseName)
        {
            return DACClass.GetMasterBasePath(HereDataBaseType,
                    DataBaseName,
                    Connection);
        }

        public string GetMasterBasePath(
            string DataBaseName,
            System.Data.IDbConnection Connection)
        {
            return DACClass.GetMasterBasePath(HereDataBaseType,
                    DataBaseName,
                    Connection);
        }

        public int IsDatabaseExist(
            string DataBaseName)
        {
            return DACClass.IsDatabaseExist(HereDataBaseType,
                    DataBaseName,
                    Connection);
        }

        public int IsDatabaseExist(
            string DataBaseName,
            System.Data.IDbConnection Connection)
        {
            return DACClass.IsDatabaseExist(HereDataBaseType,
                    DataBaseName,
                    Connection);
        }

        public int CreateNewDataBase(
            string DataBaseName,
            string DataBasePath)
        {
            return DACClass.CreateNewDataBase(HereDataBaseType,
                    DataBaseName,
                    DataBasePath,
                    Connection);
        }

        public int CreateNewDataBase(
            string DataBaseName,
            string DataBasePath,
            System.Data.IDbConnection Connection)
        {
            return DACClass.CreateNewDataBase(HereDataBaseType,
                    DataBaseName,
                    DataBasePath,
                    Connection);
        }

        public int DropDataBase(
            string DataBaseName)
        {
            return DACClass.DropDataBase(HereDataBaseType,
                    DataBaseName,
                    Connection);
        }

        public int DropDataBase(
            string DataBaseName,
            System.Data.IDbConnection Connection)
        {
            return DACClass.DropDataBase(HereDataBaseType,
                    DataBaseName,
                    Connection);
        }

        public string FromTypeToDefaultValueSQL(
            string DataType,
            object DefaultValue)
        {
            return DACClass.FromTypeToDefaultValueSQL(HereDataBaseType,
                    DataType,
                    DefaultValue);
        }

        public int IsDefaultExist(
            string ConstraintName)
        {
            return DACClass.IsDefaultExist(HereDataBaseType,
                    ConstraintName,
                    Connection);
        }

        public int IsDefaultExist(
            string ConstraintName,
            System.Data.IDbConnection Connection)
        {
            return DACClass.IsDefaultExist(HereDataBaseType,
                    ConstraintName,
                    Connection);
        }

        public int CreateDefault(
            string TableName,
            string ColumnName,
            string ConstraintName,
            object DefaultValue)
        {
            return DACClass.CreateDefault(HereDataBaseType,
                    TableName,
                    ColumnName,
                    ConstraintName,
                    DefaultValue,
                    Connection);
        }

        public int CreateDefault(
            string TableName,
            string ColumnName,
            string ConstraintName,
            object DefaultValue,
            System.Data.IDbConnection Connection)
        {
            return DACClass.CreateDefault(HereDataBaseType,
                    TableName,
                    ColumnName,
                    ConstraintName,
                    DefaultValue,
                    Connection);
        }

        public int CreateDefault(
            string TableName,
            string ColumnName,
            string ConstraintName,
            object DefaultValue,
            Transaction HereTransaction)
        {
            return DACClass.CreateDefault(HereDataBaseType,
                TableName,
                ColumnName,
                ConstraintName,
                DefaultValue,
                HereTransaction);
        }

        public int DropDefault(
            string TableName,
            string ConstraintName)
        {
            return DACClass.DropDefault(HereDataBaseType,
                    TableName,
                    ConstraintName,
                    Connection);
        }

        public int DropDefault(
            string TableName,
            string ConstraintName,
            System.Data.IDbConnection Connection)
        {
            return DACClass.DropDefault(HereDataBaseType,
                    TableName,
                    ConstraintName,
                    Connection);
        }

        public int DropDefault(
            string TableName,
            string ConstraintName,
            Transaction HereTransaction)
        {
            return DACClass.DropDefault(HereDataBaseType,
                TableName,
                ConstraintName,
                HereTransaction);
        }

        public int IsPrimarykeyExist(
            string PrimarykeyName)
        {
            return DACClass.IsPrimarykeyExist(HereDataBaseType,
                PrimarykeyName,
                Connection);
        }

        public int IsPrimarykeyExist(
            string PrimarykeyName,
            System.Data.IDbConnection Connection)
        {
            return DACClass.IsPrimarykeyExist(HereDataBaseType,
                PrimarykeyName,
                Connection);
        }

        public int CreatePrimarykey(
            string TableName,
            string ColumnName,
            string Primarykey)
        {
            return DACClass.CreatePrimarykey(HereDataBaseType,
                    TableName,
                    ColumnName,
                    Primarykey,
                    Connection);
        }

        public int CreatePrimarykey(
            string TableName,
            string ColumnName,
            string Primarykey,
            System.Data.IDbConnection Connection)
        {
            return DACClass.CreatePrimarykey(HereDataBaseType,
                    TableName,
                    ColumnName,
                    Primarykey,
                    Connection);
        }

        public int CreatePrimarykey(
            string TableName,
            string ColumnName,
            string Primarykey,
            Transaction HereTransaction)
        {
            return DACClass.CreatePrimarykey(HereDataBaseType,
                TableName,
                ColumnName,
                Primarykey,
                HereTransaction);
        }

        public int DropPrimarykey(
            string TableName,
            string Primarykey)
        {
            return DACClass.DropPrimarykey(HereDataBaseType,
                TableName,
                Primarykey,
                Connection);
        }

        public int DropPrimarykey(
            string TableName,
            string Primarykey,
            System.Data.IDbConnection Connection)
        {
            return DACClass.DropPrimarykey(HereDataBaseType,
                TableName,
                Primarykey,
                Connection);
        }

        public int DropPrimarykey(
            string TableName,
            string Primarykey,
            Transaction HereTransaction)
        {
            return DACClass.DropPrimarykey(HereDataBaseType,
                TableName,
                Primarykey,
                HereTransaction);
        }

        public int IsTableExist(
            string TableName)
        {
            return DACClass.IsTableExist(HereDataBaseType,
                    TableName,
                    Connection);
        }

        public int IsTableExist(
            string TableName,
            System.Data.IDbConnection Connection)
        {
            return DACClass.IsTableExist(HereDataBaseType,
                    TableName,
                    Connection);
        }

        public int CreateTable(
            string TableName,
            string[] Columns,
            string[] DataTypes,
            string[] Lenght,
            string[] NullAbles)
        {
            return DACClass.CreateTable(HereDataBaseType,
                    TableName,
                    Columns,
                    DataTypes,
                    Lenght,
                    NullAbles,
                    Connection);
        }

        public int CreateTable(
            string TableName,
            string[] Columns,
            string[] DataTypes,
            string[] Lenght,
            string[] NullAbles,
            System.Data.IDbConnection Connection)
        {
            return DACClass.CreateTable(HereDataBaseType,
                    TableName,
                    Columns,
                    DataTypes,
                    Lenght,
                    NullAbles,
                    Connection);
        }

        public int CreateTable(
            string TableName,
            string[] Columns,
            string[] DataTypes,
            string[] Lenght,
            string[] NullAbles,
            Transaction HereTransaction)
        {
            return DACClass.CreateTable(HereDataBaseType,
                TableName,
                Columns,
                DataTypes,
                Lenght, NullAbles,
                HereTransaction);
        }

        public int DropTable(
            string TableName)
        {
            return DACClass.DropTable(HereDataBaseType,
                    TableName,
                    Connection);
        }

        public int DropTable(
            string TableName,
            System.Data.IDbConnection Connection)
        {
            return DACClass.DropTable(HereDataBaseType,
                    TableName,
                    Connection);
        }

        public int DropTable(
            string TableName,
            Transaction HereTransaction)
        {
            return DACClass.DropTable(HereDataBaseType,
                TableName, HereTransaction);
        }

        public int IsColumnExist(
            string TableName,
            string ColumnName)
        {
            return DACClass.IsColumnExist(HereDataBaseType,
                    TableName,
                    ColumnName,
                    Connection);
        }

        public int IsColumnExist(
            string TableName,
            string ColumnName,
            System.Data.IDbConnection Connection)
        {
            return DACClass.IsColumnExist(HereDataBaseType,
                    TableName,
                    ColumnName,
                    Connection);
        }

        public int AddColumnToTable(
            string TableName,
            string ColumnName,
            string DataType,
            string Lenght,
            string NullAble)
        {
            return DACClass.AddColumnToTable(HereDataBaseType,
                    TableName,
                    ColumnName,
                    DataType,
                    Lenght,
                    NullAble,
                    Connection);
        }

        public int AddColumnToTable(
            string TableName,
            string ColumnName,
            string DataType,
            string Lenght,
            string NullAble,
            System.Data.IDbConnection Connection)
        {
            return DACClass.AddColumnToTable(HereDataBaseType,
                    TableName,
                    ColumnName,
                    DataType,
                    Lenght,
                    NullAble,
                    Connection);
        }

        public int AddColumnToTable(
            string TableName,
            string ColumnName,
            string DataType,
            string Lenght,
            string NullAble,
            Transaction HereTransaction)
        {
            return DACClass.AddColumnToTable(HereDataBaseType,
                TableName,
                ColumnName,
                DataType,
                Lenght,
                NullAble,
                HereTransaction);
        }

        public int DropColumnFromTable(
        string TableName,
        string ColumnName)
        {
            return DACClass.DropColumnFromTable(HereDataBaseType,
                    TableName,
                    ColumnName,
                    Connection);
        }

        public int DropColumnFromTable(
            string TableName,
            string ColumnName,
            System.Data.IDbConnection Connection)
        {
            return DACClass.DropColumnFromTable(HereDataBaseType,
                    TableName,
                    ColumnName,
                    Connection);
        }

        public int DropColumnFromTable(
            string TableName,
            string ColumnName,
            Transaction HereTransaction)
        {
            return DACClass.DropColumnFromTable(HereDataBaseType,
                TableName,
                ColumnName,
                HereTransaction);
        }

        public int ModifyColumnDataType(
            string TableName,
            string ColumnName,
            string DataType,
            string Lenght,
            string NullAble)
        {
            return DACClass.ModifyColumnDataType(HereDataBaseType,
                    TableName,
                    ColumnName,
                    DataType,
                    Lenght,
                    NullAble,
                    Connection);
        }

        public int ModifyColumnDataType(
            string TableName,
            string ColumnName,
            string DataType,
            string Lenght,
            string NullAble,
            System.Data.IDbConnection Connection)
        {
            return DACClass.ModifyColumnDataType(HereDataBaseType,
                    TableName,
                    ColumnName,
                    DataType,
                    Lenght,
                    NullAble,
                    Connection);
        }

        public int ModifyColumnDataType(
            string TableName,
            string ColumnName,
            string DataType,
            string Lenght,
            string NullAble,
            Transaction HereTransaction)
        {
            return DACClass.ModifyColumnDataType(HereDataBaseType,
                TableName,
                ColumnName,
                DataType,
                Lenght,
                NullAble,
                HereTransaction);
        }

        #endregion

        #endregion

        public int RunStoreProcedure(string sp)
        {
            return DACClass.RunStoreProcedure(sp, Connection);
        }
    }
}
