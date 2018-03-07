using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Text.RegularExpressions;

namespace Yqun.Data.DataBase
{
    public class SQLiteData : IData
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SQLiteData()
        {
        }

        /// <summary>
        /// 获得当前行号
        /// </summary>
        private int currentLineNumber
        {
            get
            {
                System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);
                return st.GetFrame(0).GetFileLineNumber();
            }
        }

        /// <summary>
        /// 获得当前行号
        /// </summary>
        private String methodName
        {
            get
            {
                System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);
                return st.GetFrame(0).GetMethod().Name;
            }
        }

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

        #region IData Members

        #region DataAccessBase

        #region DataCommon

        public string GetConnString(DataBaseType HereType,
            string DataSource,
            string DataBaseInstance,
            string Username,
            string PassWord,
            bool ISDataBaseAttachFile)
        {
            try
            {
                string str = "Data Source=" + DataSource + ";page size=4096;cache size=8000;Application Name=YQUN_SQLITE";
                return str;
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[GetConnString@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return "";
            }
        }

        public string GetIntegratedConnString(string DataSource,
            string DataBaseInstance, bool ISDataBaseAttachFile)
        {
            try
            {
                return GetConnString(DataBaseType.SQLITE, DataSource, DataBaseInstance, string.Empty, string.Empty, ISDataBaseAttachFile);
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[GetIntegratedConnString@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return "";
            }
        }

        public System.Data.IDbConnection GetConntion(DataBaseType HereType,
            string DataSource,
            string DataBaseInstance,
            string Username,
            string PassWord, bool ISDataBaseAttachFile)
        {
            try
            {
                string str = GetConnString(HereType,
                    DataSource,
                    DataBaseInstance,
                    Username,
                    PassWord, ISDataBaseAttachFile);

                SQLiteConnection  conn = new SQLiteConnection(str);
                return conn;
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[GetConntion@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return null;
            }
        }

        public System.Data.IDbConnection GetConntion(string ConnectionString)
        {
            try
            {
                SQLiteConnection conn = new SQLiteConnection(ConnectionString);
                return conn;
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[GetConntion@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return null;
            }
        }

        public IDbDataAdapter GetDataAdapter(string SelectText,
            IDbConnection Connection)
        {
            try
            {
                SelectText = DateTimeTransform.TransfromFrom(SelectText);
                SQLiteDataAdapter adp = new SQLiteDataAdapter(SelectText,
                    (SQLiteConnection)Connection);
                return adp;
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[GetDataAdapter@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return new SQLiteDataAdapter();
            }
        }

        public IDbCommand GetDbCommand(string CommandText,
            IDbConnection Connection)
        {
            try
            {
                CommandText = DateTimeTransform.TransfromFrom(CommandText);
                SQLiteCommand comm = new SQLiteCommand(CommandText,
                    (SQLiteConnection)Connection);
                return comm;
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[GetDbCommand@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return new SQLiteCommand();
            }
        }

        public DbCommandBuilder CreateAdapterCommand(ref IDbDataAdapter DbDataAdapter)
        {
            try
            {
                DbCommandBuilder b = new SQLiteCommandBuilder((SQLiteDataAdapter)DbDataAdapter);
                return b;
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[CreateAdapterCommand@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return new SQLiteCommandBuilder();
            }
        }

        #endregion

        #region DataAccess

        /// <summary>
        /// 根据SelectCommandText获取一个空表。
        /// </summary>
        /// <param name="SelectCommandText">Select语句</param>
        /// <param name="Connection">连接</param>
        /// <returns>空数据表</returns>
        public DataTable GetTableStructBySql(string SelectCommandText,
            IDbConnection Connection)
        {
            try
            {
                DataSet ds = GetDataSet(SelectCommandText, Connection);
                DataTable dt = ds.Tables[0];
                dt.Clear();
                dt.AcceptChanges();

                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                
                return dt;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                logger.Error(string.Format("[GetTableStructBySql@{0}]:{1},{2}", currentLineNumber, ee.Message, SelectCommandText), ee);
                return new DataTable();
            }
        }

        /// <summary>
        /// 根据表名称获取一个包含全部字段的空表
        /// </summary>
        /// <param name="TableName">表名称</param>
        /// <param name="Connection">连接</param>
        /// <returns>空数据表</returns>
        public DataTable GetTableStruct(string TableName,IDbConnection Connection)
        {
            try
            {
                DataSet ds = GetTableStructSet(TableName, Connection);
                ds.Tables[0].TableName = TableName;
                ds.AcceptChanges();
                
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return ds.Tables[0];
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                logger.Error(string.Format("[GetTableStruct@{0}]:{1},{2}", currentLineNumber, ee.Message, TableName), ee);
                return new DataTable();
            }
        }

        /// <summary>
        /// 获取包含一个空表的数据集
        /// </summary>
        /// <param name="TableName">表名称</param>
        /// <param name="Connection">连接</param>
        /// <returns>包含一个空表的数据集</returns>
        public DataSet GetTableStructSet(string TableName,IDbConnection Connection)
        {
            try
            {
                DataSet d = new DataSet();
                IDbDataAdapter ad = GetDataAdapter("select * from " + TableName + " Limit 1", Connection);
                ad.Fill(d);
                d.Clear();
                d.Tables[0].TableName = TableName;
                d.AcceptChanges();

                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                return d;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                logger.Error(string.Format("[GetTableStructSet@{0}]:{1},{2}", currentLineNumber, ee.Message, TableName), ee);
                return new DataSet();
            }
        }
        
        /// <summary>
        /// 获取包含多个空表的数据集，要处理表名称相同问题,出现相同表时，表名称 ＝ 原始表名称＋“&”＋在数组中的序列号
        /// </summary>
        /// <param name="TableNames">表名称</param>
        /// <param name="Connection">连接</param>
        /// <returns>获取包含多个空表的数据集</returns>
        public DataSet GetTableStructsSets(string[] TableNames,IDbConnection Connection)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                
                string[] name = new string[TableNames.Length];
                string current = String.Empty;
                ArrayList al = new ArrayList();

                // 表名去重
                int count = 0;
                for (int i = 0; i < TableNames.Length; i++)
                {                    
                    try
                    {
                        dt = GetTableStruct(TableNames[i], Connection);
                        dt.TableName = TableNames[i];
                        ds.Tables.Add(dt.Clone());
                    }
                    catch (Exception)
                    {
                        count++;
                        dt = GetTableStruct(TableNames[i], Connection);
                        dt.TableName = TableNames[i] + "&" + count.ToString();
                        ds.Tables.Add(dt.Clone());
                    }
                }

                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                ds.AcceptChanges();
                return ds;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                logger.Error(string.Format("[GetTableStructsSets@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return new DataSet();
            }
        }

        /// <summary>
        /// 获取空表的数组
        /// </summary>
        /// <param name="TableNames">表名称</param>
        /// <param name="Connection">连接</param>
        /// <returns>空表的数组</returns>
        public DataTable[] GetTableStructs(string[] TableNames,IDbConnection Connection)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable[] dt = new DataTable[TableNames.Length];

                ds = GetTableStructsSets(TableNames, Connection);
                for (int i = 0; i < TableNames.Length; i++)
                {
                    dt[i] = ds.Tables[i].Copy();
                    dt[i].TableName = TableNames[i];
                    dt[i].AcceptChanges();
                }
                
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return dt;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                logger.Error(string.Format("[GetTableStructs@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return new DataTable[0];
            }
        }


        /// <summary>
        /// 根据SQL语句获取数据集，注意处理表名称相同问题。
        /// </summary>
        /// <param name="SelectSqlText">Sql查询 语句</param>
        /// <param name="Connection">连接</param>
        /// <returns>数据集</returns>
        public DataSet GetDataSet(string SelectSqlText,
            IDbConnection Connection)
        {
            // 将每一个表的列Caption属性设置成“表名称.列名称”的形式，手动来做。
            try
            {
                DataSet d = new DataSet();
                IDbDataAdapter ad = GetDataAdapter(SelectSqlText, Connection);
                ad.Fill(d);

                // 取列名
                string[] strCol = SQLResolver.GetColNameFromSelectCommand(SelectSqlText);
                // 取表名
                string strTable = SQLResolver.GetTableNamesFromSeleCommandText(SelectSqlText);
                // 表名数组 
                string[] tableArr = strTable.Split(',');
                for (int k = 0; k < tableArr.Length;k++ )
                {
                    tableArr[k] = tableArr[k].Trim();
                }

                // 表名/表别名
                Hashtable h = new Hashtable();
                // 临时
                string[] t;

                // 如果没有使用别名
                if (tableArr[0].Contains(" ") == false)
                {
                    try
                    {
                        try
                        {
                            for (int i = 0; i < d.Tables[0].Columns.Count; i++)
                            {
                                d.Tables[0].Columns[i].Caption = d.Tables[0].Columns[i].ColumnName;
                                d.Tables[0].Columns[i].ExtendedProperties.Add("EXP", d.Tables[0].Columns[i].ColumnName);
                            }

                            for (int i = 0; i < d.Tables[0].Columns.Count; i++)
                            {
                                string name = strCol[i];

                                if (name.IndexOf("*") == -1)
                                {
                                    d.Tables[0].Columns[i].Caption = name;
                                    d.Tables[0].Columns[i].ExtendedProperties["EXP"] = name;
                                }
                            }
                        }
                        catch { }
                    }
                    catch { }

                    d.Tables[0].TableName = strTable;
                }
                else // 如果使用了别名
                {
                    try
                    {

                        // 把别名换成表名
                        for (int i = 0; i < tableArr.Length; i++)
                        {
                            t = tableArr[i].Split(' ');
                            // 备用
                            h.Add(t[1], t[0]);

                            for (int j = 0; j < strCol.Length; j++)
                            {
                                if (i == 0)
                                {
                                    int cur = (strCol[0].Split(' ')).GetLength(0);
                                    strCol[0] = (strCol[0].Split(' '))[cur - 1];
                                }
                                else
                                {
                                    strCol[j] = strCol[j].Replace(((string)t[1]).ToUpper(), ((string)t[0]).ToUpper());
                                }
                            }
                        }

                        string current = String.Empty;
                        foreach (DictionaryEntry de in h)
                        {
                            // 写表名
                            current = current + "," + ((string)de.Value).ToLower();
                        }
                        d.Tables[0].TableName = current.Substring(1, current.Length - 1);

                        // 写入Caption属性
                        for (int i = 0; i < d.Tables[0].Columns.Count; i++)
                        {
                            d.Tables[0].Columns[i].Caption = strCol[i];
                            d.Tables[0].Columns[i].ExtendedProperties["EXP"] = strCol[i];
                        }
                    }
                    catch
                    {
                    }
                }

                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return d;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                logger.Error(string.Format("[GetDataSet@{0}]:{1},{2}", currentLineNumber, ee.Message, SelectSqlText), ee);
                return new DataSet();
            }
        }

        /// <summary>
        /// 用参数的方式组织成条件语句，再与SelectSqlText组织成一个完整的查询语句，查询出一个数据集。
        /// 比如：select A from t where A = @AA,然后把@AA参数赋值。此方法可以防止SQL语句欺骗。
        /// </summary>
        /// <param name="SelectSqlText">查询语句，不包含Where部分，只包含Where之前的部分</param>
        /// <param name="ConditionColumns">条件列，数组长度和顺序与以下几个数组相同</param>
        /// <param name="CompareExpressions">判断符号，等于号(=)、大于、小于、不等于，数组长度和顺序与以下几个数组相同</param>
        /// <param name="ParameterValues">条件列的值，数组长度和顺序与以上几个数组相同</param>
        /// <param name="Connection">连接</param>
        /// <returns>数据集</returns>
        public DataSet GetDataSet(string SelectSqlText,
            string[] ConditionColumns,
            string[] CompareExpressions,
            object[] ParameterValues,
            IDbConnection Connection)
        {
            try 
            {
                DataSet ds = new DataSet();
                IDbCommand cmd = Connection.CreateCommand();
                string w = " Where ";
                for (int i = 0; i < ConditionColumns.Length; i++)
                {
                    if (i < ConditionColumns.Length - 1)
                    {
                        w = w + ConditionColumns[i] + " " + CompareExpressions[i] + " " + "@P" + i.ToString() + " and ";
                    }
                    else
                    {
                        w = w + ConditionColumns[i] + " " + CompareExpressions[i] + " " + "@P" + i.ToString();
                    }
                }

                SelectSqlText = DateTimeTransform.TransfromFrom(SelectSqlText);
                cmd.CommandText = SelectSqlText + w;
                cmd.CommandType = CommandType.Text;
                
                for (int i = 0; i < ConditionColumns.Length; i++)
                {
                    SQLiteParameter para = new SQLiteParameter("@P" + i.ToString(), ParameterValues[i]);
                    para.Value = ParameterValues[i];
                    cmd.Parameters.Add(para);
                }

                IDbDataAdapter ad = GetDataAdapter("", Connection); ;
                ad.SelectCommand = cmd;
                ad.Fill(ds);
                
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return ds;
            }
            catch (Exception ee) 
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                logger.Error(string.Format("[GetDataSet@{0}]:{1},{2}", currentLineNumber, ee.Message, SelectSqlText), ee);
                
                return new DataSet();
            }            
        }

        /// <summary>
        /// 给定一个带参数的查询语句，查询出一个数据集。
        /// 比如：select A from t where A = @AA,然后把@AA参数赋值。此方法可以防止SQL语句欺骗。
        /// </summary>
        /// <param name="SelectSqlText">查询语句，完整语句，包含Where部分</param>
        /// <param name="ParameterValues">各参数的值，数组长度和顺序与给定的参数语句参数个数和顺序相同</param>
        /// <param name="Connection">连接</param>
        /// <returns>数据集</returns>
        public DataSet GetDataSet(string SelectSqlText,
            object[] ParameterValues,
            IDbConnection Connection)
        {
            try
            {
                DataSet ds = new DataSet();
                IDbCommand cmd = Connection.CreateCommand();
                cmd.CommandText = DateTimeTransform.TransfromFrom(SelectSqlText);
                cmd.CommandType = CommandType.Text;

                IDbDataAdapter ad = GetDataAdapter(SelectSqlText, Connection);
                ad.SelectCommand = cmd;
                for (int i = 0; i < ParameterValues.Length; i++)
                {
                    SQLiteParameter para = new SQLiteParameter("@" + i.ToString(), ParameterValues[i]);
                    para.Value = ParameterValues[i];
                    ad.SelectCommand.Parameters.Add(para);
                }

                ad.Fill(ds);

                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                return ds;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                logger.Error(string.Format("[GetDataSet@{0}]:{1},{2}", currentLineNumber, ee.Message, SelectSqlText), ee);

                return new DataSet();
            } 
        }

        /// <summary>
        /// 调用上面某个方法，获取包含多个表的数据集，注意处理表名称相同的问题。
        /// </summary>
        /// <param name="SelectSqlText">SQL语句数组</param>
        /// <param name="Connection">连接</param>
        /// <returns>数据集</returns>
        public DataSet GetDataSet(string[] SelectSqlText,IDbConnection Connection)
        {
            try
            {
                DataSet ds = new DataSet();
                string[] TableNames = new string[SelectSqlText.Length];

                for (int i = 0; i < TableNames.Length; i++)
                {
                    try
                    {
                        DataTable dt = (GetDataSet(SelectSqlText[i], Connection)).Tables[0];
                        ds.Tables.Add(dt.Copy());
                    }
                    catch
                    {
                        continue;
                    }
                }

                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                return ds;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                logger.Error(string.Format("[GetDataSet@{0}]:{1},{2}", currentLineNumber, ee.Message, SelectSqlText), ee);

                return new DataSet();
            }
        }

        /// <summary>
        /// 调用上面某方法，获取一个数据集数组
        /// </summary>
        /// <param name="SelectSqlText">SQL语句数组</param>
        /// <param name="Connection">连接</param>
        /// <returns>数据集数组</returns>
        public DataSet[] GetDataSets(string[] SelectSqlText,IDbConnection Connection)
        {
            try 
            {
                DataSet ds = new DataSet();
                DataSet[] dsArr = new DataSet[SelectSqlText.Length];
                

                ds = GetDataSet(SelectSqlText, Connection);

                for (int i = 0; i < ds.Tables.Count; i++)                 
                {
                    DataSet dsElement = new DataSet();
                    dsElement.Tables.Add(ds.Tables[i].Copy());
                    dsArr[i] = dsElement;
                }
               
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return dsArr;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                logger.Error(string.Format("[GetDataSets@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return null;
            }
        }

        /// <summary>
        /// 调用上面某方法，获取数据表
        /// </summary>
        /// <param name="SelectSqlText">SQL语句</param>
        /// <param name="Connection">连接</param>
        /// <returns>数据表</returns>
        public DataTable GetDataTable(string SelectSqlText,IDbConnection Connection)
        {
            try 
            {
                DataTable dt = GetDataSet(SelectSqlText, Connection).Tables[0];
                
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return dt;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                logger.Error(string.Format("[GetDataTable@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return null;
            }
        }

        /// <summary>
        /// 获取第一行第一列数据对象
        /// </summary>
        /// <param name="SqlCommandText">SQL语句</param>
        /// <param name="Connection">连接</param>
        /// <returns>对象</returns>
        public object ExcuteScalar(string SqlCommandText,IDbConnection Connection)
        {
            try
            {
                DataTable dt = GetDataTable(SqlCommandText, Connection);

                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                return dt.Rows[0][0];
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                logger.Error(string.Format("[ExcuteScalar@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return null;
            }
        }

        /// <summary>
        /// 获取第一行第一列数据对象数组
        /// </summary>
        /// <param name="SqlCommandText">SQL语句</param>
        /// <param name="Connection">连接</param>
        /// <returns>对象数组</returns>
        public object[] ExcuteScalars(string[] SqlCommandText,IDbConnection Connection)
        {
            try
            {
                object[] o = new object[SqlCommandText.Length];
                for (int i = 0; i < SqlCommandText.Length; i++)
                {
                    o[i] = ExcuteScalar(SqlCommandText[i],Connection);
                }
                
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return o;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                logger.Error(string.Format("[ExcuteScalars@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return null;
            }
        }

        public int RunStoreProcedure(string sp, IDbConnection Connection)
        {
            return -1;
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="SqlCommandText">语句</param>
        /// <param name="Connection">连接</param>
        /// <returns>正确为1，异常为-1</returns>
        public int ExcuteCommand(string SqlCommandText,
            IDbConnection Connection)
        {
            try
            {
                SQLiteConnection SQLiteConn = Connection as SQLiteConnection;
                if (SQLiteConn == null)
                    throw new ArgumentException("参数Connection不是SQLiteConnection类。");

                if (Connection.State == ConnectionState.Closed)
                {
                    SQLiteConn.Open();
                }

                SQLiteConnection Conn = Connection as SQLiteConnection;
                SQLiteTransaction Trans = Conn.BeginTransaction();

                try
                {
                    IDbCommand com = GetDbCommand(SqlCommandText, SQLiteConn);
                    int i = com.ExecuteNonQuery();
                    Trans.Commit();
                }
                catch(Exception ee)
                {
                    Trans.Rollback();

                    if (Connection.State != ConnectionState.Closed)
                    {
                        Connection.Close();
                    }

                    logger.Error(string.Format("[ExcuteCommand@{0}]:{1}", currentLineNumber, ee.Message), ee);
                    return -1;
                }

                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                return 1;
            }
            catch(Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                logger.Error(string.Format("[ExcuteCommand@{0}]:{1},{2}", currentLineNumber, ee.Message, SqlCommandText), ee);
                return -1;
            }
        }

        /// <summary>
        /// 循环执行命令数组
        /// </summary>
        /// <param name="SqlCommandText">命令数组</param>
        /// <param name="Connection">连接</param>
        /// <returns>1或-1的数组</returns>
        public int[] ExcuteCommands(string[] SqlCommandText,
            IDbConnection Connection)
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            try 
            {
                int[] r = new int[SqlCommandText.Length];
                for (int i = 0; i < SqlCommandText.Length; i++)
                {
                    r[i] = -1;
                }

                SQLiteConnection Conn = Connection as SQLiteConnection;
                SQLiteTransaction Trans = Conn.BeginTransaction();

                try
                {
                    for (int i = 0; i < SqlCommandText.Length; i++)
                    {
                        IDbCommand com = GetDbCommand(SqlCommandText[i], Connection);
                        com.ExecuteNonQuery();
                        r[i] = 1;
                    }

                    Trans.Commit();
                }
                catch
                {
                    Trans.Rollback();
                }

                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return r;
            }
            catch (Exception ee )
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                logger.Error(string.Format("[ExcuteCommand@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return null;
            }
            
        }

        public Boolean ExcuteCommandsWithTransaction(List<IDbCommand> commands, IDbConnection Connection)
        {
            return false;
        }

        /// <summary>
        /// 使用事务执行命令，不在这里提交，在其他使用者程序集里提交
        /// </summary>
        /// <param name="SqlCommandText">SQL命令语句</param>
        /// <param name="HereTransaction">事务</param>
        /// <returns>正确为1，异常为-1</returns>
        public int ExcuteCommand(string SqlCommandText,Transaction HereTransaction)
        {
            try 
            {
                IDbConnection conn = HereTransaction.DBTransaction.Connection;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                IDbCommand comm = GetDbCommand(SqlCommandText, conn);
                comm.Transaction = HereTransaction.DBTransaction;

                return comm.ExecuteNonQuery();
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[ExcuteCommand@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return -1;
            }
        }
        
        /// <summary>
        /// 循环执行多个命令，示例程序
        /// </summary>
        /// <param name="SqlCommandText">命令文本</param>
        /// <param name="HereTransaction">事务</param>
        /// <returns>每个命令执行返回值组成的数组</returns>
        public int[] ExcuteCommands(string[] SqlCommandText,Transaction HereTransaction)
        {
            try
            {
                IDbConnection conn = HereTransaction.DBTransaction.Connection;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                int[] r = new int[SqlCommandText.Length];
                for (int i = 0; i < SqlCommandText.Length; i++)
                {
                    r[i] = ExcuteCommand(SqlCommandText[i], HereTransaction);
                }

                return r;
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[ExcuteCommands@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return null;
            }
        }

        /// <summary>
        /// 获取数据读取对象
        /// </summary>
        /// <param name="SQLCommandText">命令文本</param>
        /// <param name="Connection">连接</param>
        /// <returns>IDataReader</returns>
        public IDataReader ExcuteReader(string SQLCommandText,
            IDbConnection Connection)
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                IDbCommand cmd = GetDbCommand(SQLCommandText, Connection);
                return cmd.ExecuteReader();
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                logger.Error(string.Format("[ExcuteReader@{0}]:{1},{2}", currentLineNumber, ee.Message, SQLCommandText), ee);
                return null;
            }
        }

        /// <summary>
        /// 更新数据集
        /// </summary>
        /// <param name="WillUpdateDataSet">数据集</param>
        /// <param name="Connection">连接</param>
        /// <returns>1或－1</returns>
        public int Update(DataSet WillUpdateDataSet,IDbConnection Connection)
        {
            WillUpdateDataSet = WillUpdateDataSet.GetChanges();
            if (WillUpdateDataSet == null || WillUpdateDataSet.Tables.Count == 0 ||
                WillUpdateDataSet.Tables[0].Rows.Count == 0
                ) 
            {
                return 1;
            }

            try
            {
                string tName = GetTrueTableName(WillUpdateDataSet.Tables[0].TableName);
                string col = String.Empty;
                for (int i = 0; i < WillUpdateDataSet.Tables[0].Columns.Count; i++)
                {
                    if (i == 0)
                    {
                        col = WillUpdateDataSet.Tables[0].Columns[i].ColumnName;
                        col = WillUpdateDataSet.Tables[0].Columns[i].Caption;
                    }
                    else
                    {
                        col = col + "," + WillUpdateDataSet.Tables[0].Columns[i].Caption;
                    }
                }
                string SQL = "select " + col + " from " + tName;

                IDbDataAdapter ad = GetDataAdapter(SQL, Connection);

                //手动处理命令
                CreteCommand(ref ad,WillUpdateDataSet,Connection); 
                
                SQLiteDataAdapter adParticular = (SQLiteDataAdapter)ad;

                adParticular.Update(WillUpdateDataSet.Tables[0]);

                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return 1;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                logger.Error(string.Format("[Update@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return -1;
            }
            
        }

        private void CreteCommand(ref IDbDataAdapter ad, DataSet WillUpdateDataSet,IDbConnection Connection)
        {
            #region 插入命令

            if (WillUpdateDataSet != null && ad != null)
            {

                SQLiteCommand up = new SQLiteCommand((SQLiteConnection)Connection);

                string upComm = "INSERT INTO " + WillUpdateDataSet.Tables[0].TableName.ToUpper() + " ";
                string cols="";
                string values="";
                for (int i = 0; i < WillUpdateDataSet.Tables[0].Columns.Count; i++)
                {
                    DataColumn c = WillUpdateDataSet.Tables[0].Columns[i];
                    string colName = c.ColumnName.ToUpper();
                    Type t = c.DataType;

                    DbType dt = DbType.String;
                    if (t == typeof(string))
                    {
                        dt = DbType.String;
                    }
                    else if (t == typeof(int)
                        )
                    {
                        dt = DbType.Int32;
                    }
                    else if (t == typeof(long))
                    {
                        dt = DbType.Int64;
                    }
                    else if (t == typeof(short))
                    {
                        dt = DbType.Int16;
                    }
                    else if (t == typeof(decimal))
                    {
                        dt = DbType.Decimal;
                    }
                    else if (t == typeof(double))
                    {
                        dt = DbType.Double;
                    }
                    else if (t == typeof(DateTime))
                    {
                        dt = DbType.DateTime;
                    }
                    else if (t == typeof(byte[]))
                    {
                        dt = DbType.Binary;
                    }

                    int k = i + 1;
                    SQLiteParameter p = new SQLiteParameter("@param" + k.ToString(), dt, colName, DataRowVersion.Current);
                    up.Parameters.Add(p);

                    cols += colName + ",";
                    values += p.ParameterName + ",";
                }

                cols = cols.Trim().TrimEnd(",".ToCharArray());
                values = values.Trim().TrimEnd(",".ToCharArray());

                cols = "(" + cols + ")";
                values = "(" + values + ")";

                upComm += cols + " VALUES " + values;

                up.CommandText = upComm;
                ad.InsertCommand = up;
            }

            #endregion

            #region 更新命令

            if (WillUpdateDataSet != null && ad != null)
            {

                SQLiteCommand up = new SQLiteCommand((SQLiteConnection)Connection);

                string upComm = "UPDATE " + WillUpdateDataSet.Tables[0].TableName.ToUpper() + " SET ";
                for (int i = 0; i < WillUpdateDataSet.Tables[0].Columns.Count; i++)
                {
                    DataColumn c = WillUpdateDataSet.Tables[0].Columns[i];
                    string colName = c.ColumnName.ToUpper();
                    Type t = c.DataType;

                    DbType dt = DbType.String;
                    if (t == typeof(string))
                    {
                        dt = DbType.String;
                    }
                    else if (t == typeof(int)
                        )
                    {
                        dt = DbType.Int32;
                    }
                    else if (t == typeof(long))
                    {
                        dt = DbType.Int64;
                    }
                    else if (t == typeof(short))
                    {
                        dt = DbType.Int16;
                    }
                    else if (t == typeof(decimal))
                    {
                        dt = DbType.Decimal;
                    }
                    else if (t == typeof(double))
                    {
                        dt = DbType.Double;
                    }
                    else if (t == typeof(DateTime))
                    {
                        dt = DbType.DateTime;
                    }
                    else if (t == typeof(byte[]))
                    {
                        dt = DbType.Binary;
                    }

                    int k = i + 1;
                    SQLiteParameter p = new SQLiteParameter("@param" + k.ToString(), dt, colName, DataRowVersion.Current);
                    up.Parameters.Add(p);

                    upComm += colName + " = " + p.ParameterName + ",";
                }

                upComm = upComm.Trim().TrimEnd(",".ToCharArray());

                int kk = up.Parameters.Count + 1;
                SQLiteParameter p1 = new SQLiteParameter("@param" + kk.ToString(), DbType.String, "ID", DataRowVersion.Original);
                up.Parameters.Add(p1);
                upComm += " WHERE ID = " + p1.ParameterName;

                up.CommandText = upComm;
                ad.UpdateCommand = up;

            }

            #endregion

            #region 删除命令

            if (WillUpdateDataSet != null && ad != null)
            {

                SQLiteCommand up = new SQLiteCommand((SQLiteConnection)Connection);

                string upComm = "DELETE FROM " + WillUpdateDataSet.Tables[0].TableName.ToUpper() + " ";

                int k = 1;
                SQLiteParameter p1 = new SQLiteParameter("@param" + k.ToString(), DbType.String, "ID", DataRowVersion.Original);
                up.Parameters.Add(p1);
                upComm += " WHERE ID = " + p1.ParameterName;

                up.CommandText = upComm;
                ad.DeleteCommand = up;

            }

            #endregion
        }
        
        /// <summary>
        /// 更新数据表
        /// </summary>
        /// <param name="WillUpdateDataTable">数据表</param>
        /// <param name="Connection">连接</param>
        /// <returns>1／－1</returns>
        public int Update(DataTable WillUpdateDataTable,IDbConnection Connection)
        {
            try
            {
                DataSet ds = new DataSet();
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }
                ds.Tables.Add(WillUpdateDataTable.Copy());
                if (Update(ds, Connection)>0)
                {
                    if (Connection.State != ConnectionState.Closed)
                    {
                        Connection.Close();
                    }
                    return 1;
                }
                
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return -1;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                logger.Error(string.Format("[Update@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        /// <summary>
        /// 更新行集
        /// </summary>
        /// <param name="WillUpdateDataRows">行集</param>
        /// <param name="Connection">连接</param>
        /// <returns>1／－1</returns>
        public int Update(DataRow[] WillUpdateDataRows,IDbConnection Connection)
        {
            if (WillUpdateDataRows == null || WillUpdateDataRows.Length == 0 
                )
            {
                return 1;
            }

            DataTable t = WillUpdateDataRows[0].Table.Clone();
            for (int i = 0; i < WillUpdateDataRows.Length;i++ )
            {
                t.ImportRow(WillUpdateDataRows[i]);
            }

            return Update(t, Connection);
        }

        /// <summary>
        /// 用事务更新数据集
        /// </summary>
        /// <param name="WillUpdateDataSet"></param>
        /// <param name="HereTransaction"></param>
        /// <returns></returns>
        public int Update(DataSet WillUpdateDataSet,Transaction HereTransaction)
        {
            try
            {
                WillUpdateDataSet = WillUpdateDataSet.GetChanges();
                if (WillUpdateDataSet == null || WillUpdateDataSet.Tables.Count == 0 ||
                    WillUpdateDataSet.Tables[0].Rows.Count == 0
                    )
                {
                    return 1;
                }

                IDbConnection conn = HereTransaction.DBTransaction.Connection;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string tName = GetTrueTableName(WillUpdateDataSet.Tables[0].TableName);
                string col = String.Empty;
                for (int i = 0; i < WillUpdateDataSet.Tables[0].Columns.Count; i++)
                {
                    if (i == 0)
                    {
                        col = WillUpdateDataSet.Tables[0].Columns[i].ColumnName;
                    }
                    else
                    {
                        col = col + "," + WillUpdateDataSet.Tables[0].Columns[i].ColumnName;
                    }
                }
                string SQL = "select " + col + " from " + tName;

                IDbDataAdapter ad = GetDataAdapter(SQL, conn);
                //DbCommandBuilder cb = CreateAdapterCommand(ref ad);
                // 以下把ad转换成"详细类"的原因是 : 如果不转换则ad中的TableName必须为Table.

                //手动处理命令
                CreteCommand(ref ad, WillUpdateDataSet, HereTransaction.DBTransaction.Connection); 
                
                SQLiteDataAdapter adParticular = (SQLiteDataAdapter)ad;                
                adParticular.SelectCommand.Transaction = (SQLiteTransaction)HereTransaction.DBTransaction;
                adParticular.Update(WillUpdateDataSet.Tables[0]);                

                return 1;
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[Update@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        /// <summary>
        /// 用事务更新数据表
        /// </summary>
        /// <param name="WillUpdateDataTable"></param>
        /// <param name="HereTransaction"></param>
        /// <returns></returns>
        public int Update(DataTable WillUpdateDataTable,Transaction HereTransaction)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(WillUpdateDataTable.Copy());
                return Update(ds, HereTransaction);
            }
            catch (Exception ee) 
            {
                logger.Error(string.Format("[Update@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return -1;
            }
        }
        
        /// <summary>
        /// 用事务更新行集
        /// </summary>
        /// <param name="WillUpdateDataRows"></param>
        /// <param name="HereTransaction"></param>
        /// <returns></returns>
        public int Update(DataRow[] WillUpdateDataRows,Transaction HereTransaction)
        {
            try
            {
                if (WillUpdateDataRows == null || WillUpdateDataRows.Length == 0)
                {
                    return 1;
                }

                DataTable t = WillUpdateDataRows[0].Table.Clone();
                for (int i = 0; i < WillUpdateDataRows.Length; i++)
                {
                    t.ImportRow(WillUpdateDataRows[i]);
                }

                return Update(t, HereTransaction);
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[Update@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        /// <summary>
        /// 手动写SQL语句，以ID为条件，
        /// where ID = @ID
        /// </summary>
        /// <param name="WillUpdateDataSet">数据集</param>
        /// <param name="Connection">连接</param>
        /// <returns>1或-1</returns>
        public int UpdateByID(DataSet WillUpdateDataSet, IDbConnection Connection) 
        {
            try
            {
                return Update(WillUpdateDataSet, Connection);
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                logger.Error(string.Format("[UpdateByID@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        /// <summary>
        /// 手动写SQL语句，以ID为条件，
        /// where ID = @ID
        /// </summary>
        /// <param name="WillUpdateDataSet">数据集</param>
        /// <param name="HereTransaction">事务</param>
        /// <returns>1／－1</returns>
        public int UpdateByID(DataSet WillUpdateDataSet, Transaction HereTransaction)
        {
            try
            {
                return Update(WillUpdateDataSet, HereTransaction);
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[UpdateByID@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        #endregion

        #region appendix

        public void AddNullToTable(ref DataSet OriginalSet,
            string TableName,
            int AddRows)
        {
            try
            {
                for (int i = 0; i < AddRows; i++)
                {
                    DataRow row = OriginalSet.Tables[TableName].NewRow();
                    for (int y = 0; y < OriginalSet.Tables[TableName].Columns.Count; y++)
                    {
                        row[y] = DBNull.Value;

                    }

                    OriginalSet.Tables[TableName].Rows.Add(row);
                }
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[AddNullToTable@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return;
            }
        }

        public void AddNullToTable(ref DataSet OriginalSet,
            int TableIndex,
            int AddRows)
        {
            try
            {
                for (int i = 0; i < AddRows; i++)
                {
                    DataRow row = OriginalSet.Tables[TableIndex].NewRow();
                    for (int y = 0; y < OriginalSet.Tables[TableIndex].Columns.Count; y++)
                    {
                        row[y] = DBNull.Value;

                    }

                    OriginalSet.Tables[TableIndex].Rows.Add(row);
                }
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[AddNullToTable@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return;
            }
        }

        public string DataFromSelectToTableName(string select)
        {
            try
            {
                string tempStr = select.ToLower();
                int x = tempStr.IndexOf(" from ");
                if (x != -1)
                {
                    tempStr = tempStr.Substring(x + 6, tempStr.Length - x - 6);
                }
                tempStr = tempStr.Trim();

                tempStr = tempStr.Replace(" order by ", " where ");
                tempStr = tempStr.Replace(" group by ", " where ");
                int x1 = tempStr.IndexOf(" where ");
                string table = tempStr;
                if (x1 != -1)
                {
                    table = tempStr.Substring(0, x1);
                }

                table = table.Trim();

                return table;
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[DataFromSelectToTableName@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return null;
            }
        }

        public string GetTrueTableName(string TableName)
        {
            try
            {
                int check = TableName.IndexOf("&");
                if (check != -1)
                {
                    TableName = TableName.Substring(0, check);
                }
                return TableName;
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[GetTrueTableName@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return null;
            }
        }

        public string[] DataSplitCommand(string command,
            string splString)
        {
            try
            {
                string tempCommand = command;
                int x = tempCommand.IndexOf(splString);
                int SPLSymbolNumber = 0;
                while (x != -1)
                {

                    SPLSymbolNumber++;


                    tempCommand = tempCommand.Substring(x + splString.Length,
                        tempCommand.Length - x - splString.Length);
                    x = tempCommand.IndexOf(splString);

                }
                string[] retString = new string[SPLSymbolNumber + 1];
                int xx = command.IndexOf(splString);
                if (xx != -1)
                {
                    for (int i = 0; i < SPLSymbolNumber; i++)
                    {
                        int y = command.IndexOf(splString);
                        retString[i] = command.Substring(0, y);
                        command = command.Substring(y + splString.Length,
                            command.Length - y - splString.Length);
                    }
                    retString[SPLSymbolNumber] = command;
                }
                else
                {

                    retString[SPLSymbolNumber] = command;
                }
                return retString;
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[DataSplitCommand@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return null;
            }
        }

        public string DataSpl(string originalstring,
            string firstspl,
            string lastspl)
        {
            try
            {
                string str = "";

                int x = originalstring.ToLower().IndexOf(firstspl.ToLower());
                if (x == -1)
                {
                    return "";
                }

                str = originalstring.Substring(x + firstspl.Length,
                    originalstring.Length - x - firstspl.Length);
                str = str.Trim();
                str = str.TrimStart(new char[] { '=' });
                str = str.Trim();

                x = str.ToLower().IndexOf(lastspl.ToLower());
                if (x > 0)
                {
                    str = str.Substring(0, x);
                }

                return str;
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[DataSpl@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return null;
            }
        }

        #endregion

        #region BlobColumns


        /// <summary>
        /// 暂时不用实现
        /// </summary>
        /// <param name="HereDataBaseType"></param>
        /// <param name="TableName"></param>
        /// <param name="IDColumnName"></param>
        /// <param name="IDValue"></param>
        /// <param name="AimColumnName"></param>
        /// <param name="AimValue"></param>
        /// <param name="Connection"></param>
        /// <returns></returns>
        public int SaveDataToBlobColumn(DataBaseType HereDataBaseType,
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            byte[] AimValue,
            IDbConnection Connection)
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }
                Transaction trans = new Transaction(Connection, IsolationLevel.ReadCommitted);
                int x = SaveDataToBlobColumn(HereDataBaseType,
                    TableName,
                    IDColumnName,
                    IDValue,
                    AimColumnName,
                    AimValue,
                    trans);

                if (x >= 0)
                {
                    x = 1;
                }

                try
                {
                    trans.Commit();

                    if (Connection.State != ConnectionState.Closed)
                    {
                        Connection.Close();
                    }
                }
                catch
                {
                    x = -1;
                }

                if (x != 1)
                {
                    try
                    {
                        trans.Rollback();
                        if (Connection.State != ConnectionState.Closed)
                        {
                            Connection.Close();
                        }
                    }
                    catch
                    {
                        x = -1;
                    }
                }
                return x;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                logger.Error(string.Format("[SaveDataToBlobColumn@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        /// <summary>
        /// 暂时不用实现
        /// </summary>
        /// <param name="HereDataBaseType"></param>
        /// <param name="TableName"></param>
        /// <param name="IDColumnName"></param>
        /// <param name="IDValue"></param>
        /// <param name="AimColumnName"></param>
        /// <param name="Offset"></param>
        /// <param name="AimValue"></param>
        /// <param name="Connection"></param>
        /// <returns></returns>
        public int SaveDataToBlobColumn(DataBaseType HereDataBaseType,
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            int Offset,
            byte[] AimValue,
            IDbConnection Connection)
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }
                Transaction trans = new Transaction(Connection, IsolationLevel.ReadCommitted);
                int x = SaveDataToBlobColumn(HereDataBaseType,
                    TableName,
                    IDColumnName,
                    IDValue,
                    AimColumnName,
                    Offset,
                    AimValue,
                    trans);

                if (x >= 0)
                {
                    x = 1;
                }

                try
                {
                    trans.Commit();
                    if (Connection.State != ConnectionState.Closed)
                    {
                        Connection.Close();
                    }
                }
                catch
                {
                    x = -1;
                }

                if (x != 1)
                {
                    try
                    {
                        trans.Rollback();
                        if (Connection.State != ConnectionState.Closed)
                        {
                            Connection.Close();
                        }
                    }
                    catch
                    {
                        x = -1;
                    }
                }
                return x;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    try
                    {
                        Connection.Close();
                    }
                    catch { }
                }
                logger.Error(string.Format("[SaveDataToBlobColumn@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return -1;

            }
        }

        /// <summary>
        /// 暂时不用实现
        /// </summary>
        /// <param name="HereDataBaseType"></param>
        /// <param name="TableName"></param>
        /// <param name="IDColumnName"></param>
        /// <param name="IDValue"></param>
        /// <param name="AimColumnName"></param>
        /// <param name="AimValue"></param>
        /// <param name="Connection"></param>
        /// <returns></returns>
        public int SaveDataToBlobColumn(DataBaseType HereDataBaseType,
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            string AimValue,
            IDbConnection Connection)
        {

            try
            {

                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }
                Transaction trans = new Transaction(Connection, IsolationLevel.ReadCommitted);
                int x = SaveDataToBlobColumn(HereDataBaseType,
                    TableName,
                    IDColumnName,
                    IDValue,
                    AimColumnName,
                    AimValue,
                    trans);

                if (x >= 0)
                {
                    x = 1;
                }

                try
                {
                    trans.Commit();
                    if (Connection.State != ConnectionState.Closed)
                    {
                        Connection.Close();
                    }
                }
                catch
                {
                    x = -1;
                }

                if (x != 1)
                {
                    try
                    {
                        trans.Rollback();
                        if (Connection.State != ConnectionState.Closed)
                        {
                            Connection.Close();
                        }
                    }
                    catch
                    {
                        x = -1;
                    }
                }
                return x;

            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                logger.Error(string.Format("[SaveDataToBlobColumn@{0}]:{1}", currentLineNumber, ee.Message), ee);

                return -1;
            }
        }

        /// <summary>
        /// 暂时不用实现
        /// </summary>
        /// <param name="HereDataBaseType"></param>
        /// <param name="TableName"></param>
        /// <param name="IDColumnName"></param>
        /// <param name="IDValue"></param>
        /// <param name="AimColumnName"></param>
        /// <param name="Offset"></param>
        /// <param name="AimValue"></param>
        /// <param name="Connection"></param>
        /// <returns></returns>
        public int SaveDataToBlobColumn(DataBaseType HereDataBaseType,
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            int Offset,
            string AimValue,
            IDbConnection Connection)
        {
            try
            {

                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }
                Transaction trans = new Transaction(Connection, IsolationLevel.ReadCommitted);
                int x = SaveDataToBlobColumn(HereDataBaseType,
                    TableName,
                    IDColumnName,
                    IDValue,
                    AimColumnName,
                    Offset,
                    AimValue,
                    trans);

                if (x >= 0)
                {
                    x = 1;
                }

                try
                {
                    trans.Commit();
                    if (Connection.State != ConnectionState.Closed)
                    {
                        Connection.Close();
                    }
                }
                catch
                {
                    x = -1;
                }

                if (x != 1)
                {
                    try
                    {
                        trans.Rollback();
                        if (Connection.State != ConnectionState.Closed)
                        {
                            Connection.Close();
                        }
                    }
                    catch
                    {
                        x = -1;
                    }
                }
                return x;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                logger.Error(string.Format("[SaveDataToBlobColumn@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        /// <summary>
        /// 暂时不用实现
        /// </summary>
        /// <param name="HereDataBaseType"></param>
        /// <param name="TableName"></param>
        /// <param name="IDColumnName"></param>
        /// <param name="IDValue"></param>
        /// <param name="AimColumnName"></param>
        /// <param name="AimValue"></param>
        /// <param name="HereTransaction"></param>
        /// <returns></returns>
        public int SaveDataToBlobColumn(DataBaseType HereDataBaseType,
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            byte[] AimValue,
            Transaction HereTransaction)
        {
            try
            {
                SQLiteConnection Connection = (SQLiteConnection)HereTransaction.DBTransaction.Connection;
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                string update = "update " + TableName +
                    " set " + AimColumnName + "=@datavalue where " +
                    IDColumnName + " = '" + IDValue + "'";
                SQLiteCommand com = new SQLiteCommand(update, Connection);

                com.Parameters.Add(new SQLiteParameter("@datavalue", AimValue));
                com.Transaction = (SQLiteTransaction)HereTransaction.DBTransaction;
                int x = com.ExecuteNonQuery();

                if (x >= 0)
                {
                    x = 1;
                }

                return x;
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[SaveDataToBlobColumn@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        /// <summary>
        /// 暂时不用实现
        /// </summary>
        /// <param name="HereDataBaseType"></param>
        /// <param name="TableName"></param>
        /// <param name="IDColumnName"></param>
        /// <param name="IDValue"></param>
        /// <param name="AimColumnName"></param>
        /// <param name="Offset"></param>
        /// <param name="AimValue"></param>
        /// <param name="HereTransaction"></param>
        /// <returns></returns>
        public int SaveDataToBlobColumn(DataBaseType HereDataBaseType,
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            int Offset,
            byte[] AimValue,
            Transaction HereTransaction)
        {
            try
            {
                SQLiteConnection Connection = (SQLiteConnection)HereTransaction.DBTransaction.Connection;
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                string update = "update " + TableName +
                    " set " + AimColumnName + "=@datavalue where " +
                    IDColumnName + " = '" + IDValue + "'";
                SQLiteCommand com = new SQLiteCommand(update, Connection);

                byte[] DataByte;
                if (Offset >= 0 && Offset < AimValue.Length)
                {
                    DataByte = new byte[AimValue.Length - Offset];
                    AimValue.CopyTo(DataByte, Offset);
                }
                else
                {
                    DataByte = new byte[AimValue.Length];
                    AimValue.CopyTo(DataByte,0);
                }

                com.Parameters.Add(new SQLiteParameter("@datavalue", DataByte));
                com.Transaction = (SQLiteTransaction)HereTransaction.DBTransaction;
                int x = com.ExecuteNonQuery();
                if (x >= 0)
                {
                    x = 1;
                }

                return x;
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[SaveDataToBlobColumn@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        /// <summary>
        /// 暂时不用实现
        /// </summary>
        /// <param name="HereDataBaseType"></param>
        /// <param name="TableName"></param>
        /// <param name="IDColumnName"></param>
        /// <param name="IDValue"></param>
        /// <param name="AimColumnName"></param>
        /// <param name="AimValue"></param>
        /// <param name="HereTransaction"></param>
        /// <returns></returns>
        public int SaveDataToBlobColumn(DataBaseType HereDataBaseType,
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            string AimValue,
            Transaction HereTransaction)
        {
            try
            {
                SQLiteConnection Connection = (SQLiteConnection)HereTransaction.DBTransaction.Connection;
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                string update = "update " + TableName +
                    " set " + AimColumnName + "=@datavalue where " +
                    IDColumnName + " = '" + IDValue + "'";
                SQLiteCommand com = new SQLiteCommand(update, Connection);

                com.Parameters.Add(new SQLiteParameter("@datavalue", AimValue));
                com.Transaction = (SQLiteTransaction)HereTransaction.DBTransaction;
                int x = com.ExecuteNonQuery();
                if (x >= 0)
                {
                    x = 1;
                }

                return x;
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[SaveDataToBlobColumn@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        /// <summary>
        /// 暂时不用实现
        /// </summary>
        /// <param name="HereDataBaseType"></param>
        /// <param name="TableName"></param>
        /// <param name="IDColumnName"></param>
        /// <param name="IDValue"></param>
        /// <param name="AimColumnName"></param>
        /// <param name="Offset"></param>
        /// <param name="AimValue"></param>
        /// <param name="HereTransaction"></param>
        /// <returns></returns>
        public int SaveDataToBlobColumn(DataBaseType HereDataBaseType,
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            int Offset,
            string AimValue,
            Transaction HereTransaction)
        {
            try
            {
                SQLiteConnection Connection = (SQLiteConnection)HereTransaction.DBTransaction.Connection;
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                string update = "update " + TableName +
                    " set " + AimColumnName + "=@datavalue where " +
                    IDColumnName + " = '" + IDValue + "'";
                SQLiteCommand com = new SQLiteCommand(update, Connection);

                string DataByte;
                if (Offset >= 0 && Offset < AimValue.Length)
                {
                    DataByte = AimValue.Substring(Offset);
                }
                else
                {
                    DataByte = AimValue;
                }

                com.Parameters.Add(new SQLiteParameter("@datavalue", DataByte));
                com.Transaction = (SQLiteTransaction)HereTransaction.DBTransaction;
                int x = com.ExecuteNonQuery();
                if (x >= 0)
                {
                    x = 1;
                }

                return x;
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[SaveDataToBlobColumn@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        /// <summary>
        /// 暂时不用实现
        /// </summary>
        /// <param name="HereDataBaseType"></param>
        /// <param name="TableName"></param>
        /// <param name="IDColumnName"></param>
        /// <param name="IDValue"></param>
        /// <param name="AimColumnName"></param>
        /// <param name="Connection"></param>
        /// <returns></returns>
        public byte[] ReadDataFromBlobColumn(DataBaseType HereDataBaseType,
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            IDbConnection Connection)
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                string select = "select " + AimColumnName +
                    " from " + TableName + " where " +
                    IDColumnName + " = '" + IDValue + "'";
                SQLiteDataReader r = (SQLiteDataReader)ExcuteReader(select, Connection);
                bool canread = r.Read();
                byte[] b = null;
                if (canread)
                {
                    b = (byte[])r[0];
                }
                if (Connection.State != ConnectionState.Closed)
                {
                    try
                    {
                        Connection.Close();
                    }
                    catch { }
                }
                return b;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    try
                    {
                        Connection.Close();
                    }
                    catch { }
                }

                logger.Error(string.Format("[ReadDataFromBlobColumn@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return null;
            }
        }

        public byte[] ReadDataFromBlobColumn(DataBaseType HereDataBaseType,
            string TableName,
            string AimColumnName,
            string Condition,
            long Offset,
            int ReadLenght,
            IDbConnection Connection)
        {
            if (Connection.State == ConnectionState.Closed) Connection.Open();
            string select = "select " + AimColumnName + " from " + TableName + " " + Condition;
            SQLiteDataReader r = (SQLiteDataReader)ExcuteReader(select, Connection);
            bool canread = r.Read();
            byte[] b = new byte[ReadLenght];
            if (canread)
            {
                r.GetBytes(0, Offset, b, 0, b.Length);
            }

            if (Connection.State != ConnectionState.Closed)
            {
                Connection.Close();
            }

            return b;
        }


        /// <summary>
        /// 暂时不用实现
        /// </summary>
        /// <param name="HereDataBaseType"></param>
        /// <param name="TableName"></param>
        /// <param name="IDColumnName"></param>
        /// <param name="IDValue"></param>
        /// <param name="AimColumnName"></param>
        /// <param name="Offset"></param>
        /// <param name="ReadLenght"></param>
        /// <param name="Connection"></param>
        /// <returns></returns>
        public byte[] ReadDataFromBlobColumn(DataBaseType HereDataBaseType,
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            long Offset,
            int ReadLenght,
            IDbConnection Connection)
        {
            try
            {

                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                string select = "select " + AimColumnName +
                    " from " + TableName + " where " +
                    IDColumnName + " = '" + IDValue + "'";
                SQLiteDataReader r = (SQLiteDataReader)ExcuteReader(select, Connection);
                bool canread = r.Read();
                byte[] b = new byte[ReadLenght];
                if (canread)
                {
                    r.GetBytes(0, Offset, b, 0, b.Length);
                }

                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                return b;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                logger.Error(string.Format("[ReadDataFromBlobColumn@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return null;
            }
        }

        /// <summary>
        /// 暂时不用实现
        /// </summary>
        /// <param name="HereDataBaseType"></param>
        /// <param name="TableName"></param>
        /// <param name="IDColumnName"></param>
        /// <param name="IDValue"></param>
        /// <param name="AimColumnName"></param>
        /// <param name="Connection"></param>
        /// <returns></returns>
        public string ReadTextFromBlobColumn(DataBaseType HereDataBaseType,
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            IDbConnection Connection)
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                string select = "select " + AimColumnName +
                    " from " + TableName + " where " +
                    IDColumnName + " = '" + IDValue + "'";
                SQLiteDataReader r = (SQLiteDataReader)ExcuteReader(select, Connection);
                bool canread = r.Read();
                string str = "";
                if (canread)
                {
                    str = r[0].ToString();
                }

                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                return str;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                logger.Error(string.Format("[ReadDataFromBlobColumn@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return null;
            }
        }

        /// <summary>
        /// 暂时不用实现
        /// </summary>
        /// <param name="HereDataBaseType"></param>
        /// <param name="TableName"></param>
        /// <param name="IDColumnName"></param>
        /// <param name="IDValue"></param>
        /// <param name="AimColumnName"></param>
        /// <param name="Offset"></param>
        /// <param name="ReadLenght"></param>
        /// <param name="Connection"></param>
        /// <returns></returns>
        public string ReadTextFromBlobColumn(DataBaseType HereDataBaseType,
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            long Offset,
            int ReadLenght,
            IDbConnection Connection)
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }
                Offset += 1;
                string select = "select substr(" + AimColumnName +
                    "," + Offset + "," + ReadLenght + ") " +
                    " from " + TableName + " where " +
                    IDColumnName + " = '" + IDValue + "'";
                SQLiteCommand comm = (SQLiteCommand)GetDbCommand(select, Connection);
                object o = comm.ExecuteScalar();
                string str = "";
                if (o != null)
                {
                    str = o.ToString();
                }

                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                return str;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                logger.Error(string.Format("[ReadDataFromBlobColumn@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return null;
            }
        }

        /// <summary>
        /// 暂时不用实现
        /// </summary>
        /// <param name="HereDataBaseType"></param>
        /// <param name="TableName"></param>
        /// <param name="SelectColumns"></param>
        /// <param name="AimColumnName"></param>
        /// <param name="MatchWords"></param>
        /// <param name="MatchType"></param>
        /// <param name="Connection"></param>
        /// <returns></returns>
        public DataSet SelectWithBlobColumnCondition(DataBaseType HereDataBaseType,
            string TableName,
            string SelectColumns,
            string AimColumnName,
            string MatchWords,
            int MatchType,
            IDbConnection Connection)
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                string sele = "";
                sele += "select " + SelectColumns;
                string wh = "";
                if (MatchType == 0)
                {
                    wh = AimColumnName + " = " + "'" + MatchWords + "'";
                }
                else if (MatchType == 1)
                {
                    wh = AimColumnName + " LIKE " + "'" + MatchWords + "%'";
                }
                else if (MatchType == 2)
                {
                    wh = AimColumnName + " LIKE " + "'%" + MatchWords + "'";
                }
                else if (MatchType == 3)
                {
                    wh = AimColumnName + " LIKE " + "'%" + MatchWords + "%'";
                }

                sele += " from " + TableName;
                if (wh != "")
                {
                    sele += " where " + wh;
                }

                DataSet d = GetDataSet(sele, Connection);
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return d;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                logger.Error(string.Format("[SelectWithBlobColumnCondition@{0}]:{1}", currentLineNumber, ee.Message), ee);
                return null;
            }
        }

        /// <summary>
        /// 得到特定表特定字段的字节长度
        /// </summary>
        /// <param name="HereDataBaseType"></param>
        /// <param name="TableName"></param>
        /// <param name="IDColumnName"></param>
        /// <param name="IDValue"></param>
        /// <param name="AimColumnName"></param>
        /// <param name="Connection"></param>
        /// <returns></returns>
        public long GetBlobDataLength(DataBaseType HereDataBaseType,
            string TableName,
            string IDColumnName,
            string IDValue,
            string AimColumnName,
            IDbConnection Connection)
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }
                string select = "select length(" + AimColumnName + ") " +
                    " from " + TableName + " where " +
                    IDColumnName + " = '" + IDValue + "'";
                SQLiteCommand comm = (SQLiteCommand)GetDbCommand(select, Connection);
                object o = comm.ExecuteScalar();
                long x = 0;
                if (o != null)
                {
                    x = long.Parse(o.ToString());
                }

                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                return x;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                logger.Error(string.Format("[{0}@{1}]:{2}", methodName, currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        public long GetBlobDataLength(DataBaseType HereDataBaseType,
            string TableName,
            string AimColumnName,
            string Condition,
            IDbConnection Connection)
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }
                string select = "select length(" + AimColumnName + ") " +
                    " from " + TableName + Condition;
                SQLiteCommand comm = (SQLiteCommand)GetDbCommand(select, Connection);
                object o = comm.ExecuteScalar();
                long x = 0;
                if (o != null)
                {
                    x = long.Parse(o.ToString());
                }

                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                return x;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                logger.Error(string.Format("[{0}@{1}]:{2}", methodName, currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        #endregion

        #endregion

        #region DataWatch
        /// <summary>
        /// 暂时不用实现
        /// </summary>
        /// <param name="HereDataBaseType"></param>
        /// <param name="SqlCondition"></param>
        /// <param name="ProcessInfo"></param>
        /// <param name="ProcessType"></param>
        /// <returns></returns>
        public int DataWatch(DataBaseType HereDataBaseType, string SqlCondition, string ProcessInfo, string ProcessType)
        {
            return -1;
        }
        #endregion

        #region DataBaseManagement

        /// <summary>
        /// 暂不用实现
        /// </summary>
        /// <param name="HereDataBaseType">数据库类型枚举</param>
        /// <param name="DataBaseName">数据库名称</param>
        /// <param name="Connection">连接</param>
        /// <returns>路径</returns>
        public string GetMasterBasePath(DataBaseType HereDataBaseType,
            string DataBaseName,
            IDbConnection Connection)
        {
            return null;
        }

        /// <summary>
        /// 暂不用实现
        /// </summary>
        /// <param name="HereDataBaseType">数据类型</param>
        /// <param name="DataBaseName">数据库名称</param>
        /// <param name="Connection">连接</param>
        /// <returns>1存在，0不存在，－1异常</returns>
        public int IsDatabaseExist(DataBaseType HereDataBaseType,
            string DataBaseName,
            IDbConnection Connection)
        {
            return -1;

        }

        /// <summary>
        /// 暂不用实现
        /// </summary>
        /// <param name="HereDataBaseType">数据库类型</param>
        /// <param name="DataBaseName">数据库名称</param>
        /// <param name="DataBasePath">路径</param>
        /// <param name="Connection">连接</param>
        /// <returns></returns>
        public int CreateNewDataBase(DataBaseType HereDataBaseType,
            string DataBaseName,
            string DataBasePath,
            IDbConnection Connection)
        {
            return -1;

        }

        /// <summary>
        /// 暂不用实现
        /// </summary>
        /// <param name="HereDataBaseType"></param>
        /// <param name="DataBaseName"></param>
        /// <param name="Connection"></param>
        /// <returns></returns>
        public int DropDataBase(DataBaseType HereDataBaseType,
            string DataBaseName,
            IDbConnection Connection)
        {
            return -1;
        }


        public string FromTypeToDefaultValueSQL(DataBaseType HereDataBaseType,
            string DataType,
            object DefaultValue)
        {
            try
            {
                string str = "";
                if (DataType.ToLower().Trim() == "text" ||
                    DataType.ToLower().Trim() == "varchar" ||
                    DataType.ToLower().Trim() == "nvarchar")
                {
                    str = "N" + "'" + DefaultValue.ToString() + "'";
                }
                else if (DataType.ToLower().Trim() == "date" ||
                         DataType.ToLower().Trim() == "time" ||
                         DataType.ToLower().Trim() == "boolean")
                {
                    str = "'" + DefaultValue.ToString() + "'";
                }
                else
                {
                    str = DefaultValue.ToString();
                }
                return str;
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[{0}@{1}]:{2}", methodName, currentLineNumber, ee.Message), ee);
                return "";
            }
        }

        public int IsDefaultExist(DataBaseType HereDataBaseType,
            string ConstraintName,
            IDbConnection Connection)
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                SQLiteConnection SQLiteConn = Connection as SQLiteConnection;
                DataTable dt = SQLiteConn.GetSchema("Columns");
                int Result = 0;
                foreach (DataRow Row in dt.Rows)
                {
                    if (Row["Column_Name"].ToString().ToUpper() == ConstraintName.Trim().ToUpper() &&
                        Row["Column_HasDefault"].ToString().ToUpper() == "TRUE")
                    {
                        Result = 1;
                        break;
                    }
                }

                return Result;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                logger.Error(string.Format("[{0}@{1}]:{2}", methodName, currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        public int CreateDefault(DataBaseType HereDataBaseType,
            string TableName,
            string ColumnName,
            string ConstraintName,
            object DefaultValue,
            IDbConnection Connection)
        {
            try
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return 1;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                logger.Error(string.Format("[{0}@{1}]:{2}", methodName, currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        public int CreateDefault(DataBaseType HereDataBaseType,
            string TableName,
            string ColumnName,
            string ConstraintName,
            object DefaultValue,
            Transaction HereTransaction)
        {
            try
            {
                return 1;
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[{0}@{1}]:{2}", methodName, currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        public int DropDefault(DataBaseType HereDataBaseType,
            string TableName,
            string ConstraintName,
            IDbConnection Connection)
        {
            try
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return 1;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                logger.Error(string.Format("[{0}@{1}]:{2}", methodName, currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        public int DropDefault(DataBaseType HereDataBaseType,
            string TableName,
            string ConstraintName,
            Transaction HereTransaction)
        {
            try
            {
                return 1;
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[{0}@{1}]:{2}", methodName, currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        public int IsPrimarykeyExist(DataBaseType HereDataBaseType,
            string PrimarykeyName,
            IDbConnection Connection)
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                SQLiteConnection SQLiteConn = Connection as SQLiteConnection;
                DataTable dt = SQLiteConn.GetSchema("Columns");
                int Result = 0;
                foreach (DataRow Row in dt.Rows)
                {
                    if (Row["Column_Name"].ToString().ToUpper() == PrimarykeyName.Trim().ToUpper() &&
                        Row["Primary_Key"].ToString().ToUpper() == "TRUE")
                    {
                        Result = 1;
                        break;
                    }
                }

                return Result;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                logger.Error(string.Format("[{0}@{1}]:{2}", methodName, currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        public int CreatePrimarykey(DataBaseType HereDataBaseType,
            string TableName,
            string ColumnName,
            string Primarykey,
            IDbConnection Connection)
        {
            try
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return 1;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                logger.Error(string.Format("[{0}@{1}]:{2}", methodName, currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        public int CreatePrimarykey(DataBaseType HereDataBaseType,
            string TableName,
            string ColumnName,
            string Primarykey,
            Transaction HereTransaction)
        {
            try
            {
                return 1;
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[{0}@{1}]:{2}", methodName, currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        public int DropPrimarykey(DataBaseType HereDataBaseType,
            string TableName,
            string Primarykey,
            IDbConnection Connection)
        {
            try
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return 1;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                logger.Error(string.Format("[{0}@{1}]:{2}", methodName, currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        public int DropPrimarykey(DataBaseType HereDataBaseType,
            string TableName,
            string Primarykey,
            Transaction HereTransaction)
        {
            try
            {
                return 1;
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[{0}@{1}]:{2}", methodName, currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        public int IsTableExist(DataBaseType HereDataBaseType,
            string TableName,
            IDbConnection Connection)
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                SQLiteConnection SQLiteConn = Connection as SQLiteConnection;
                DataTable dt = SQLiteConn.GetSchema("Tables");
                int Result = 0;
                foreach (DataRow Row in dt.Rows)
                {
                    if (Row["Table_Name"].ToString().ToUpper() == TableName.Trim().ToUpper())
                    {
                        Result = 1;
                        break;
                    }
                }

                return Result;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    try
                    {
                        Connection.Close();
                    }
                    catch
                    { }
                }
                logger.Error(string.Format("[{0}@{1}]:{2}", methodName, currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        public int CreateTable(DataBaseType HereDataBaseType,
            string TableName,
            string[] Columns,
            string[] DataTypes,
            string[] Lenght,
            string[] NullAbles,
            IDbConnection Connection)
        {
            try
            {
                if (IsTableExist(HereDataBaseType, TableName, Connection) == 1)
                    DropTable(HereDataBaseType, TableName, Connection);

                string comm2 = "CREATE TABLE [" + TableName + "] (	[ID]	[varchar(36)] NOT NULL ,	[SYSTEM_DEFAULT_NORM_CODE]	[varchar(200)] NULL,	[SCTS]	[timestamp] NULL DEFAULT (Current_Date)," +
                    "[SCUS]	[nvarchar(50)] NULL,	[SCDF]	[smallint] NULL DEFAULT (0),	[SCPT]	[varchar(36)] NULL,	[SCAP]	[nvarchar(50)] NULL,	[SCST]	[varchar(50)] NULL,	[SCWT]	[smallint] NULL DEFAULT (0)," +
                     "[SCER]	[smallint] NULL DEFAULT (0),	[SCDL]	[smallint] NULL DEFAULT (0),	[SCCT]	[smallint] NULL DEFAULT (0),	[SCSV]	[smallint] NULL DEFAULT (0),PRIMARY KEY ([ID]));";

                int ii = ExcuteCommand(comm2, Connection);

                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return ii;

            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                logger.Error(string.Format("[{0}@{1}]:{2}", methodName, currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        public int CreateTable(DataBaseType HereDataBaseType,
            string TableName,
            string[] Columns,
            string[] DataTypes,
            string[] Lenght,
            string[] NullAbles,
            Transaction HereTransaction)
        {
            try
            {
                if (Columns.Length != DataTypes.Length ||
                    Columns.Length != Lenght.Length ||
                    Columns.Length != NullAbles.Length)
                {
                    return 0;
                }

                if (IsTableExist(HereDataBaseType, TableName, HereTransaction.DBTransaction.Connection) == 1)
                    DropTable(HereDataBaseType, TableName,HereTransaction);

                string comm2 = "CREATE TABLE [" + TableName + "] (	[ID]	[varchar(36)] NOT NULL ,	[SYSTEM_DEFAULT_NORM_CODE]	[varchar(200)] NULL,	[SCTS]	[timestamp] NULL DEFAULT (Current_Date)," +
                         "[SCUS]	[nvarchar(50)] NULL,	[SCDF]	[smallint] NULL DEFAULT (0),	[SCPT]	[varchar(36)] NULL,	[SCAP]	[nvarchar(50)] NULL,	[SCST]	[varchar(50)] NULL,	[SCWT]	[smallint] NULL DEFAULT (0)," +
                          "[SCER]	[smallint] NULL DEFAULT (0),	[SCDL]	[smallint] NULL DEFAULT (0),	[SCCT]	[smallint] NULL DEFAULT (0),	[SCSV]	[smallint] NULL DEFAULT (0),PRIMARY KEY ([ID]));";

                return ExcuteCommand(comm2, HereTransaction);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int DropTable(DataBaseType HereDataBaseType,
            string TableName,
            IDbConnection Connection)
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                SQLiteConnection SQLiteConn = Connection as SQLiteConnection;
                string comm1 = "DROP TABLE IF EXISTS " + SQLiteConn.Database + "." + TableName;
                int i = ExcuteCommand(comm1, Connection);

                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                return i;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                logger.Error(string.Format("[{0}@{1}]:{2}", methodName, currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        public int DropTable(DataBaseType HereDataBaseType,
            string TableName,
            Transaction HereTransaction)
        {
            try
            {
                SQLiteConnection SQLiteConn = HereTransaction.DBTransaction.Connection as SQLiteConnection;
                string comm1 = "DROP TABLE IF EXISTS " + SQLiteConn.Database + "." + TableName;

                return ExcuteCommand(comm1, HereTransaction);
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[{0}@{1}]:{2}", methodName, currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        public int IsColumnExist(DataBaseType HereDataBaseType,
            string TableName,
            string ColumnName,
            IDbConnection Connection)
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                SQLiteConnection SQLiteConn = Connection as SQLiteConnection;
                DataTable dt = SQLiteConn.GetSchema("Columns");
                int Result = 0;
                foreach (DataRow Row in dt.Rows)
                {
                    if (Row["Column_Name"].ToString().ToUpper() == ColumnName.Trim().ToUpper() &&
                        Row["Table_Name"].ToString().ToUpper() == TableName.Trim().ToUpper())
                    {
                        Result = 1;
                        break;
                    }
                }

                return Result;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                logger.Error(string.Format("[{0}@{1}]:{2}", methodName, currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        public int AddColumnToTable(DataBaseType HereDataBaseType,
            string TableName,
            string ColumnName,
            string DataType,
            string Lenght,
            string NullAble,
            IDbConnection Connection)
        {
            try
            {

                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                if (IsColumnExist(HereDataBaseType, TableName, ColumnName, Connection) == 1)
                    DropColumnFromTable(HereDataBaseType, TableName, ColumnName, Connection);

                string comm2 = "";
                if (Lenght != "" && Lenght != null)
                {
                    comm2 = "alter table " + TableName + " add " + ColumnName +
                        " " + DataType + "(" + Lenght + ") " + NullAble;   
                }
                else
                {
                    comm2 = "alter table " + TableName + " add " +
                        ColumnName + " " + DataType + " " + NullAble;

                 
                }

                return ExcuteCommand(comm2, Connection);
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                logger.Error(string.Format("[{0}@{1}]:{2}", methodName, currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        public int AddColumnToTable(DataBaseType HereDataBaseType,
            string TableName,
            string ColumnName,
            string DataType,
            string Lenght,
            string NullAble,
            Transaction HereTransaction)
        {
            try
            {
                if (IsColumnExist(HereDataBaseType, TableName, ColumnName, HereTransaction.DBTransaction.Connection) == 1)
                    DropColumnFromTable(HereDataBaseType, TableName, ColumnName, HereTransaction.DBTransaction.Connection);

                string comm2 = "";
                if (Lenght != "" && Lenght != null)
                {
                    comm2 = "alter table " + TableName + " add " + ColumnName +
                        " " + DataType + "(" + Lenght + ") " + NullAble;
                }
                else
                {
                    comm2 = "alter table " + TableName + " add " +
                        ColumnName + " " + DataType + " " + NullAble;
                }

                return ExcuteCommand(comm2, HereTransaction);
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[{0}@{1}]:{2}", methodName, currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        public int DropColumnFromTable(DataBaseType HereDataBaseType,
            string TableName,
            string ColumnName,
            IDbConnection Connection)
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                string SQL_DropColumn = SQLGenerator.DropColumn(TableName, ColumnName, Connection);
                return ExcuteCommand(SQL_DropColumn,Connection);
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                logger.Error(string.Format("[{0}@{1}]:{2}", methodName, currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        public int DropColumnFromTable(DataBaseType HereDataBaseType,
            string TableName,
            string ColumnName,
            Transaction HereTransaction)
        {
            try
            {
                string SQL_DropColumn = SQLGenerator.DropColumn(TableName, ColumnName, HereTransaction.DBTransaction.Connection);
                return ExcuteCommand(SQL_DropColumn, HereTransaction);
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[{0}@{1}]:{2}", methodName, currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        public int ModifyColumnDataType(DataBaseType HereDataBaseType,
            string TableName,
            string ColumnName,
            string DataType,
            string Lenght,
            string NullAble,
            IDbConnection Connection)
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                ColumnSchema columnschema = new ColumnSchema();
                columnschema.ColumnName = ColumnName;
                columnschema.DataTypeName = DataType;
                columnschema.AllowDBNull = NullAble.Trim().ToUpper() == "NULL"? true:false;

                string SQL_ModifyColumn = SQLGenerator.ModifyColumn(TableName, ColumnName, columnschema, Connection);
                return ExcuteCommand(SQL_ModifyColumn, Connection);
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                logger.Error(string.Format("[{0}@{1}]:{2}", methodName, currentLineNumber, ee.Message), ee);
                return -1;
            }
        }

        public int ModifyColumnDataType(DataBaseType HereDataBaseType,
            string TableName,
            string ColumnName,
            string DataType,
            string Lenght,
            string NullAble,
            Transaction HereTransaction)
        {
            ColumnSchema columnschema = new ColumnSchema();
            columnschema.ColumnName = ColumnName;
            columnschema.DataTypeName = DataType;
            columnschema.AllowDBNull = NullAble.Trim().ToUpper() == "NULL" ? true : false;

            string SQL_ModifyColumn = SQLGenerator.ModifyColumn(TableName, ColumnName, columnschema, HereTransaction.DBTransaction.Connection);
            return ExcuteCommand(SQL_ModifyColumn, HereTransaction);
        }

        #endregion

        #endregion
    }

    public enum Operation
    {
        Delete,
        Modify
    }

    public class ColumnSchema
    {
        string m_ColumnName;
        bool m_AllowDBNull;
        string m_DataTypeName;
        bool m_IsKey;
        bool m_IsAutoIncrement;
        bool m_IsUnique;
        string m_DefaultValue;

        public ColumnSchema()
        {
        }

        public string ColumnName
        {
            get
            {
                return m_ColumnName;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.Compare(m_ColumnName, value, true) != 0)
                    {
                        m_ColumnName = value;
                    }
                }
                else
                {
                    throw new ArgumentNullException("ColumnName不能为null或空。");
                }
            }
        }

        public bool AllowDBNull
        {
            get
            {
                return m_AllowDBNull;
            }

            set
            {
                m_AllowDBNull = value;
            }
        }

        public string DataTypeName
        {
            get
            {
                return m_DataTypeName;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.Compare(m_DataTypeName, value, true) != 0)
                    {
                        m_DataTypeName = value;
                    }
                }
                else
                {
                    throw new ArgumentNullException("ColumnName不能为null或空。");
                }
            }
        }

        public bool IsKey
        {
            get
            {
                return m_IsKey;
            }
            set
            {
                if (value != m_IsKey)
                {
                    m_IsKey = value;
                    AllowDBNull = !IsKey;
                }
            }
        }

        public bool IsUnique
        {
            get
            {
                return m_IsUnique;
            }
            set
            {
                if (value != m_IsUnique)
                {
                    m_IsUnique = value;
                }
            }
        }

        public bool IsAutoIncrement
        {
            get
            {
                return m_IsAutoIncrement;
            }
            set
            {
                if (value != m_IsAutoIncrement)
                {
                    m_IsAutoIncrement = value;
                    IsKey = IsAutoIncrement;
                }
            }
        }

        public string DefaultValue
        {
            get
            {
                return m_DefaultValue;
            }
            set
            {
                if (value == null)
                {
                    m_DefaultValue = string.Empty;
                }
            }
        }
        public static ColumnSchema NullColumnSchema
        {
            get
            {
                ColumnSchema Null = new ColumnSchema();
                Null.ColumnName = "ColumnName";
                Null.DataTypeName = "Text";
                return Null;
            }
        }
    }

    public class SQLGenerator
    {
        static string ColumnSchemas(string TableName, Operation SQL_Operation, string ColumnName, ColumnSchema NewColumnName, IDbConnection Connection)
        {
            SQLiteConnection SQLiteConn = Connection as SQLiteConnection;
            if (SQLiteConn == null)
                throw new ArgumentNullException("Connection");

            if (string.IsNullOrEmpty(TableName))
                throw new ArgumentNullException("TableName");

            if (string.IsNullOrEmpty(ColumnName))
                throw new ArgumentNullException("ColumnName");

            string sql_Select = "select * from " + TableName.Trim();
            SQLiteCommand Command = new SQLiteCommand(sql_Select, SQLiteConn);
            SQLiteDataReader DataReader = Command.ExecuteReader();
            DataTable dt = DataReader.GetSchemaTable();
            StringBuilder SQL_ColumnSchema = new StringBuilder();
            foreach (DataRow Row in dt.Rows)
            {
                if (SQL_Operation == Operation.Delete)
                {
                    if (string.Compare(ColumnName, Row["ColumnName"].ToString(), true) == 0)
                        continue;
                }
                else if (SQL_Operation == Operation.Modify)
                {
                    if (string.Compare(ColumnName, Row["ColumnName"].ToString(), true) == 0)
                    {
                        #region ColumnSchema
                        
                        SQL_ColumnSchema.Append("[");
                        SQL_ColumnSchema.Append(NewColumnName.ColumnName);
                        SQL_ColumnSchema.Append("] ");
                        SQL_ColumnSchema.Append(NewColumnName.DataTypeName);

                        if (Row["IsAutoIncrement"].ToString().ToUpper() == "TRUE")
                        {
                            SQL_ColumnSchema.Append(" NOT NULL");
                            SQL_ColumnSchema.Append(" PRIMARY KEY");
                            SQL_ColumnSchema.Append(" AUTOINCREMENT");
                        }
                        else
                        {
                            if (Row["IsKey"].ToString().ToUpper() == "TRUE")
                            {
                                SQL_ColumnSchema.Append(" PRIMARY KEY");
                            }

                            if (NewColumnName.AllowDBNull)
                            {
                                SQL_ColumnSchema.Append(" NULL");
                            }
                            else
                            {
                                SQL_ColumnSchema.Append(" NOT NULL");
                            }

                            if (Row["IsUnique"].ToString().ToUpper() == "TRUE")
                            {
                                SQL_ColumnSchema.Append(" UNIQUE");
                            }
                        }

                        if (Row["DefaultValue"].ToString().ToUpper() != "")
                        {
                            SQL_ColumnSchema.Append(" DEFAULT ");
                            SQL_ColumnSchema.Append(Row["DefaultValue"].ToString());
                        }

                        SQL_ColumnSchema.Append(",");

                        #endregion ColumnSchema
                    }

                    continue;
                }

                SQL_ColumnSchema.Append("[");
                SQL_ColumnSchema.Append(Row["ColumnName"].ToString());
                SQL_ColumnSchema.Append("] ");
                SQL_ColumnSchema.Append(Row["DataTypeName"].ToString());

                if (Row["IsAutoIncrement"].ToString().ToUpper() == "TRUE")
                {
                    SQL_ColumnSchema.Append(" NOT NULL");
                    SQL_ColumnSchema.Append(" PRIMARY KEY");
                    SQL_ColumnSchema.Append(" AUTOINCREMENT");
                }
                else
                {
                    if (Row["IsKey"].ToString().ToUpper() == "TRUE")
                    {
                        SQL_ColumnSchema.Append(" PRIMARY KEY");
                    }

                    if (Row["AllowDBNull"].ToString().ToUpper() == "TRUE")
                    {
                        SQL_ColumnSchema.Append(" NULL");
                    }
                    else
                    {
                        SQL_ColumnSchema.Append(" NOT NULL");
                    }

                    if (Row["IsUnique"].ToString().ToUpper() == "TRUE")
                    {
                        SQL_ColumnSchema.Append(" UNIQUE");
                    }
                }

                if (Row["DefaultValue"].ToString().ToUpper() != "")
                {
                    SQL_ColumnSchema.Append(" DEFAULT ");
                    SQL_ColumnSchema.Append(Row["DefaultValue"].ToString());
                }

                SQL_ColumnSchema.Append(",");
            }

            DataReader.Close();

            return SQL_ColumnSchema.ToString().TrimEnd(',');
        }

        static string ColumnList(string TableName, Operation SQL_Operation, string ColumnName,ColumnSchema NewColumnName,IDbConnection Connection)
        {
            SQLiteConnection SQLiteConn = Connection as SQLiteConnection;
            if (SQLiteConn == null)
                throw new ArgumentNullException("Connection");

            if (string.IsNullOrEmpty(TableName))
                throw new ArgumentNullException("TableName");

            if (string.IsNullOrEmpty(ColumnName))
                throw new ArgumentNullException("ColumnName");

            string sql_Select = "select * from " + TableName.Trim();
            SQLiteCommand Command = new SQLiteCommand(sql_Select, SQLiteConn);
            SQLiteDataReader DataReader = Command.ExecuteReader();
            DataTable dt = DataReader.GetSchemaTable();
            StringBuilder SQL_ColumnSchema = new StringBuilder();
            foreach (DataRow Row in dt.Rows)
            {
                if (SQL_Operation == Operation.Delete)
                {
                    if (string.Compare(ColumnName, Row["ColumnName"].ToString(), true) == 0)
                        continue;
                }
                else if (SQL_Operation == Operation.Modify)
                {
                    if (string.Compare(ColumnName, Row["ColumnName"].ToString(), true) == 0)
                    {
                        SQL_ColumnSchema.Append(NewColumnName.ColumnName);
                        SQL_ColumnSchema.Append(",");
                    }

                    continue;
                }

                SQL_ColumnSchema.Append(Row["ColumnName"].ToString());
                SQL_ColumnSchema.Append(",");
            }

            DataReader.Close();

            return SQL_ColumnSchema.ToString().TrimEnd(',');
        }

        public static string ModifyColumn(string TableName, string ColumnName,ColumnSchema NewColumnName, IDbConnection Connection)
        {
            SQLiteConnection SQLiteConn = Connection as SQLiteConnection;
            if (SQLiteConn == null)
                throw new ArgumentNullException("Connection");

            if (string.IsNullOrEmpty(TableName))
                throw new ArgumentNullException("TableName");

            if (string.IsNullOrEmpty(ColumnName))
                throw new ArgumentNullException("ColumnName");

            if (Connection.State == ConnectionState.Closed)
            {
                SQLiteConn.Open();
            }


            int index = NewColumnName.DataTypeName.IndexOf("(");
            if (index!= -1)
            {
                NewColumnName.DataTypeName = NewColumnName.DataTypeName.Substring(0, index);
            }

            StringBuilder sql_newfieldschema = new StringBuilder();
            StringBuilder sql_fieldschema = new StringBuilder();
            StringBuilder sql_fieldlist = new StringBuilder();

            string sql_Select = "select * from " + TableName.Trim();
            SQLiteCommand Command = new SQLiteCommand(sql_Select, SQLiteConn);
            SQLiteDataReader DataReader = Command.ExecuteReader();
            DataTable dt = DataReader.GetSchemaTable();
            foreach (DataRow Row in dt.Rows)
            {
                sql_fieldschema.Append("[");
                sql_fieldschema.Append(Row["ColumnName"].ToString());
                sql_fieldschema.Append("] ");
                sql_fieldschema.Append(Row["DataTypeName"].ToString());

                sql_fieldlist.Append("[");
                sql_fieldlist.Append(Row["ColumnName"].ToString());
                sql_fieldlist.Append("]");

                if (string.Compare(ColumnName, Row["ColumnName"].ToString(), true) == 0)
                {
                    sql_newfieldschema.Append("[");
                    sql_newfieldschema.Append(ColumnName);
                    sql_newfieldschema.Append("] ");
                    sql_newfieldschema.Append(NewColumnName.DataTypeName);
                }
                else
                {
                    sql_newfieldschema.Append("[");
                    sql_newfieldschema.Append(Row["ColumnName"].ToString());
                    sql_newfieldschema.Append("] ");
                    sql_newfieldschema.Append(Row["DataTypeName"].ToString());
                }

                switch (Row["DataTypeName"].ToString().Trim().ToUpper())
                {
                    case "INTEGER":
                    case "FLOAT":
                    case "REAL":
                    case "NUMERIC":
                        if (Row["NumericPrecision"].ToString() != "255")
                        {
                            sql_fieldschema.Append("(");
                            sql_fieldschema.Append(Row["NumericPrecision"].ToString());
                            if (Row["NumericScale"].ToString() != "255")
                            {
                                sql_fieldschema.Append(",");
                                sql_fieldschema.Append(Row["NumericScale"].ToString());
                            }
                            sql_fieldschema.Append(") ");

                            sql_newfieldschema.Append("(");
                            sql_newfieldschema.Append(Row["NumericPrecision"].ToString());
                            if (Row["NumericScale"].ToString() != "255")
                            {

                                sql_newfieldschema.Append(",");
                                sql_newfieldschema.Append(Row["NumericScale"].ToString());
                            }
                            sql_newfieldschema.Append(") ");
                        }
                        break;
                    case "VARCHAR":
                    case "NVARCHAR":
                        sql_fieldschema.Append("(");
                        sql_fieldschema.Append(Row["ColumnSize"].ToString());
                        sql_fieldschema.Append(") ");
                        sql_newfieldschema.Append("(");
                        sql_newfieldschema.Append(Row["ColumnSize"].ToString());
                        sql_newfieldschema.Append(") ");
                        break;
                    case "TEXT":
                        if (Row["ColumnSize"].ToString() != Int32.MaxValue.ToString())
                        {
                            sql_fieldschema.Append("(");
                            sql_fieldschema.Append(Row["ColumnSize"].ToString());
                            sql_fieldschema.Append(") ");
                            sql_newfieldschema.Append("(");
                            sql_newfieldschema.Append(Row["ColumnSize"].ToString());
                            sql_newfieldschema.Append(") ");
                        }
                        break;
                    case "BOOLEAN":
                    case "DATE":
                    case "TIME":
                    case "BLOB":
                    case "TIMESTAMP":
                        break;
                }

                if (Row["IsAutoIncrement"].ToString().ToUpper() == "TRUE")
                {
                    sql_fieldschema.Append(" NOT NULL");
                    sql_fieldschema.Append(" PRIMARY KEY");
                    sql_fieldschema.Append(" AUTOINCREMENT");
                    sql_newfieldschema.Append(" NOT NULL");
                    sql_newfieldschema.Append(" PRIMARY KEY");
                    sql_newfieldschema.Append(" AUTOINCREMENT");
                }
                else
                {
                    if (Row["IsKey"].ToString().ToUpper() == "TRUE")
                    {
                        sql_fieldschema.Append(" PRIMARY KEY");
                        sql_newfieldschema.Append(" PRIMARY KEY");
                    }
                    else
                    {
                        if (Row["IsUnique"].ToString().ToUpper() == "TRUE")
                        {
                            sql_fieldschema.Append(" UNIQUE");
                            sql_newfieldschema.Append(" UNIQUE");
                        }
                    }

                    if (Row["AllowDBNull"].ToString().ToUpper() == "TRUE")
                    {
                        sql_fieldschema.Append(" NULL");
                        sql_newfieldschema.Append(" NULL");
                    }
                    else
                    {
                        sql_fieldschema.Append(" NOT NULL");
                        sql_newfieldschema.Append(" NOT NULL");
                    }
                }

                if (Row["DefaultValue"].ToString().ToUpper() != "")
                {
                    sql_fieldschema.Append(" DEFAULT ");
                    sql_fieldschema.Append(Row["DefaultValue"].ToString());
                    sql_newfieldschema.Append(" DEFAULT ");
                    sql_newfieldschema.Append(Row["DefaultValue"].ToString());
                }
 
                sql_fieldschema.Append(",");
                sql_newfieldschema.Append(",");
                sql_fieldlist.Append(",");
            }

            DataReader.Close();

            StringBuilder SQL_ModifyColumn = new StringBuilder();
            SQL_ModifyColumn.Append("CREATE TEMPORARY TABLE " + TableName + "_backup (" + sql_fieldschema.ToString().TrimEnd(',') + ");");
            SQL_ModifyColumn.Append("INSERT INTO " + TableName + "_backup SELECT " + sql_fieldlist.ToString().TrimEnd(',') + " FROM " + TableName + ";");
            SQL_ModifyColumn.Append("DROP TABLE IF EXISTS " + TableName + ";");
            SQL_ModifyColumn.Append("CREATE TABLE " + TableName + " (" + sql_newfieldschema.ToString().TrimEnd(',') + ");");
            SQL_ModifyColumn.Append("INSERT INTO " + TableName + " SELECT " + sql_fieldlist.ToString().TrimEnd(',') + " FROM " + TableName + "_backup;");
            SQL_ModifyColumn.Append("DROP TABLE IF EXISTS " + TableName + "_backup;");

            return SQL_ModifyColumn.ToString();
        }

        public static string DropColumn(string TableName, string ColumnName, IDbConnection Connection)
        {
            SQLiteConnection SQLiteConn = Connection as SQLiteConnection;
            if (SQLiteConn == null)
                throw new ArgumentNullException("Connection");

            if (string.IsNullOrEmpty(TableName))
                throw new ArgumentNullException("TableName");

            if (string.IsNullOrEmpty(ColumnName))
                throw new ArgumentNullException("ColumnName");

            if (Connection.State == ConnectionState.Closed)
            {
                SQLiteConn.Open();
            }

            StringBuilder sql_newfieldschema = new StringBuilder();
            StringBuilder sql_fieldschema = new StringBuilder();
            StringBuilder sql_fieldlist = new StringBuilder();
            StringBuilder sql_newfieldlist = new StringBuilder();

            string sql_Select = "select * from " + TableName.Trim();
            SQLiteCommand Command = new SQLiteCommand(sql_Select, SQLiteConn);
            SQLiteDataReader DataReader = Command.ExecuteReader();
            DataTable dt = DataReader.GetSchemaTable();
            foreach (DataRow Row in dt.Rows)
            {
                #region 列名称和长度

                sql_fieldschema.Append("[");
                sql_fieldschema.Append(Row["ColumnName"].ToString());
                sql_fieldschema.Append("] ");
                sql_fieldschema.Append(Row["DataTypeName"].ToString());

                sql_fieldlist.Append("[");
                sql_fieldlist.Append(Row["ColumnName"].ToString());
                sql_fieldlist.Append("]");

                switch (Row["DataTypeName"].ToString().Trim().ToUpper())
                {
                    case "INTEGER":
                    case "FLOAT":
                    case "REAL":
                    case "NUMERIC":
                        if (Row["NumericPrecision"].ToString() != "255")
                        {
                            sql_fieldschema.Append("(");
                            sql_fieldschema.Append(Row["NumericPrecision"].ToString());
                            if (Row["NumericScale"].ToString() != "255")
                            {
                                sql_fieldschema.Append(",");
                                sql_fieldschema.Append(Row["NumericScale"].ToString());
                            }
                            sql_fieldschema.Append(") ");
                        }
                        break;
                    case "VARCHAR":
                    case "NVARCHAR":
                        sql_fieldschema.Append("(");
                        sql_fieldschema.Append(Row["ColumnSize"].ToString());
                        sql_fieldschema.Append(") ");
                        break;
                    case "TEXT":
                        if (Row["ColumnSize"].ToString() != Int32.MaxValue.ToString())
                        {
                            sql_fieldschema.Append("(");
                            sql_fieldschema.Append(Row["ColumnSize"].ToString());
                            sql_fieldschema.Append(") ");
                        }
                        break;
                    case "BOOLEAN":
                    case "DATE":
                    case "TIME":
                    case "BLOB":
                    case "TIMESTAMP":
                        break;
                }

                #endregion 列名称和长度

                if (string.Compare(ColumnName, Row["ColumnName"].ToString(), true) != 0)
                {
                    #region 列名称和长度

                    sql_newfieldschema.Append("[");
                    sql_newfieldschema.Append(Row["ColumnName"].ToString());
                    sql_newfieldschema.Append("] ");
                    sql_newfieldschema.Append(Row["DataTypeName"].ToString());

                    sql_newfieldlist.Append("[");
                    sql_newfieldlist.Append(Row["ColumnName"].ToString());
                    sql_newfieldlist.Append("]");

                    switch (Row["DataTypeName"].ToString().Trim().ToUpper())
                    {
                        case "INTEGER":
                        case "FLOAT":
                        case "REAL":
                        case "NUMERIC":
                            if (Row["NumericPrecision"].ToString() != "255")
                            {
                                sql_newfieldschema.Append("(");
                                sql_newfieldschema.Append(Row["NumericPrecision"].ToString());
                                if (Row["NumericScale"].ToString() != "255")
                                {

                                    sql_newfieldschema.Append(",");
                                    sql_newfieldschema.Append(Row["NumericScale"].ToString());
                                }
                                sql_newfieldschema.Append(") ");
                            }
                            break;
                        case "VARCHAR":
                        case "NVARCHAR":
                            sql_newfieldschema.Append("(");
                            sql_newfieldschema.Append(Row["ColumnSize"].ToString());
                            sql_newfieldschema.Append(") ");
                            break;
                        case "TEXT":
                            if (Row["ColumnSize"].ToString() != Int32.MaxValue.ToString())
                            {
                                sql_newfieldschema.Append("(");
                                sql_newfieldschema.Append(Row["ColumnSize"].ToString());
                                sql_newfieldschema.Append(") ");
                            }
                            break;
                        case "BOOLEAN":
                        case "DATE":
                        case "TIME":
                        case "BLOB":
                        case "TIMESTAMP":
                            break;
                    }

                    #endregion 列名称和长度

                    if (Row["IsAutoIncrement"].ToString().ToUpper() == "TRUE")
                    {
                        sql_newfieldschema.Append(" NOT NULL");
                        sql_newfieldschema.Append(" PRIMARY KEY");
                        sql_newfieldschema.Append(" AUTOINCREMENT");
                    }
                    else
                    {
                        if (Row["IsKey"].ToString().ToUpper() == "TRUE")
                        {
                            sql_newfieldschema.Append(" PRIMARY KEY");
                        }
                        else
                        {
                            if (Row["IsUnique"].ToString().ToUpper() == "TRUE")
                            {
                                sql_newfieldschema.Append(" UNIQUE");
                            }
                        }

                        if (Row["AllowDBNull"].ToString().ToUpper() == "TRUE")
                        {
                            sql_newfieldschema.Append(" NULL");
                        }
                        else
                        {
                            sql_newfieldschema.Append(" NOT NULL");
                        }
                    }

                    if (Row["DefaultValue"].ToString().ToUpper() != "")
                    {
                        sql_newfieldschema.Append(" DEFAULT ");
                        sql_newfieldschema.Append(Row["DefaultValue"].ToString());
                    }

                    sql_newfieldschema.Append(",");
                    sql_newfieldlist.Append(",");
                }

                if (Row["IsAutoIncrement"].ToString().ToUpper() == "TRUE")
                {
                    sql_fieldschema.Append(" NOT NULL");
                    sql_fieldschema.Append(" PRIMARY KEY");
                    sql_fieldschema.Append(" AUTOINCREMENT");
                }
                else
                {
                    if (Row["IsKey"].ToString().ToUpper() == "TRUE")
                    {
                        sql_fieldschema.Append(" PRIMARY KEY");
                    }
                    else
                    {
                        if (Row["IsUnique"].ToString().ToUpper() == "TRUE")
                        {
                            sql_fieldschema.Append(" UNIQUE");
                        }
                    }

                    if (Row["AllowDBNull"].ToString().ToUpper() == "TRUE")
                    {
                        sql_fieldschema.Append(" NULL");
                    }
                    else
                    {
                        sql_fieldschema.Append(" NOT NULL");
                    }
                }

                if (Row["DefaultValue"].ToString().ToUpper() != "")
                {
                    sql_fieldschema.Append(" DEFAULT ");
                    sql_fieldschema.Append(Row["DefaultValue"].ToString());
                }

                sql_fieldschema.Append(",");
                sql_fieldlist.Append(",");
            }

            DataReader.Close();

            StringBuilder SQL_DropColumn = new StringBuilder();
            SQL_DropColumn.Append("CREATE TEMPORARY TABLE " + TableName + "_backup (" + sql_fieldschema.ToString().TrimEnd(',') + ");");
            SQL_DropColumn.Append("INSERT INTO " + TableName + "_backup SELECT " + sql_fieldlist.ToString().TrimEnd(',') + " FROM " + TableName + ";");
            SQL_DropColumn.Append("DROP TABLE IF EXISTS " + TableName + ";");
            SQL_DropColumn.Append("CREATE TABLE " + TableName + " (" + sql_newfieldschema.ToString().TrimEnd(',') + ");");
            SQL_DropColumn.Append("INSERT INTO " + TableName + " SELECT " + sql_newfieldlist.ToString().TrimEnd(',') + " FROM " + TableName + "_backup;");
            SQL_DropColumn.Append("DROP TABLE IF EXISTS " + TableName + "_backup;");

            return SQL_DropColumn.ToString();
        }

        public static string CreateDefault(string TableName, string ColumnName, string DefaultValue, IDbConnection Connection)
        {
            return " select * from " + TableName + "where 1<>1";
        }

        public static string DropDefault(string TableName, string ColumnName, IDbConnection Connection)
        {
            return " select * from " + TableName + "where 1<>1";
        }

        public static string CreatePrimaryKey(string TableName, string PrimaryKeyName,IDbConnection Connection)
        {
            return " select * from " + TableName + "where 1<>1";
        }

        public static string DropPrimaryKey(string TableName, string PrimaryKeyName, IDbConnection Connection)
        {
            return " select * from " + TableName + "where 1<>1";
        }
    }

    public class DateTimeTransform
    {
        static string[] _datetimePatterns = new string[]{
                                                    @"'\s*\d{4}\s*-\s*\d{1,2}\s*-\s*\d{1,2}\s+\d{1,2}\s*:\s*\d{1,2}\s*:\s*\d{1,2}\s*.\s*\d+\s*'",
                                                    @"'\s*\d{4}\s*-\s*\d{1,2}\s*-\s*\d{1,2}\s+\d{1,2}\s*:\s*\d{1,2}\s*:\s*\d{1,2}\s*'",
                                                    @"'\s*\d{4}\s*-\s*\d{1,2}\s*-\s*\d{1,2}\s+\d{1,2}\s*:\s*\d{1,2}\s*'",
                                                    @"'\s*\d{4}\s*-\s*\d{1,2}\s*-\s*\d{1,2}\s*'",
                                                    @"'\s*\d{1,2}\s*:\s*\d{1,2}\s*:\s*\d{1,2}\s*'",
                                                    @"'\s*\d{1,2}\s*:\s*\d{1,2}\s*'"
                                                    };

        static string[] _datetimeFormats = new string[] {
                                                      "yyyy-MM-dd HH:mm:ss.ffff",
                                                      "yyyy-MM-dd HH:mm:ss.fff",
                                                      "yyyy-MM-dd HH:mm:ss.ff",
                                                      "yyyy-MM-dd HH:mm:ss.f",
                                                      "yyyy-MM-dd HH:mm:ss",
                                                      "yyyy-MM-dd HH:mm",
                                                      "yyyy-MM-dd",
                                                      "HH:mm:ss",
                                                      "HH:mm"
                                               };

        public static string TransfromFrom(string input)
        {
            if (input.StartsWith("#HANDLED# ")) 
            {
                input = input.Substring(10, input.Length - 10);
                return input;
            }
             

            Regex r;
            for (int i = 0; i < _datetimePatterns.Length; i++)
            {
                r = new Regex(_datetimePatterns[i]);
                MatchCollection MatchCol = r.Matches(input);
                foreach (Match m in MatchCol)
                {
                    string ProcessedString = m.Value.Trim('\'', '\'');
                    List<string> SubStrings = new List<string>();
                    int StringStart = 0;
                    for (int j = 0; j < ProcessedString.Length; j++)
                    {
                        string Char = ProcessedString.Substring(j, 1);
                        if (Char == "-" || Char == ":" || Char == ".")
                        {
                            SubStrings.Add(ProcessedString.Substring(StringStart, j - StringStart).Trim(' ', '\t'));
                            SubStrings.Add(Char);
                            StringStart = j + 1;
                        }
                    }

                    SubStrings.Add(ProcessedString.Substring(StringStart).Trim(' ', '\t'));
                    string[] strings = new string[SubStrings.Count];
                    SubStrings.CopyTo(strings);
                    ProcessedString = String.Concat(strings);

                    int SpaceIndex = ProcessedString.IndexOfAny(new char[] { ' ', '\t' });
                    string ReplaceString = string.Empty;
                    if (SpaceIndex != -1)
                    {
                        for (int j = SpaceIndex; j < ProcessedString.Length; j++)
                        {
                            string Char = ProcessedString.Substring(j, 1);
                            if (Char != " " && Char != '\t'.ToString())
                            {
                                ReplaceString = ProcessedString.Substring(SpaceIndex, j - SpaceIndex);
                                break;
                            }
                        }
                    }

                    if (ReplaceString != string.Empty)
                    {
                        ProcessedString = ProcessedString.Replace(ReplaceString, " ");
                    }

                    int DotIndex = ProcessedString.IndexOfAny(new char[] { '.' });
                    if (DotIndex != -1)
                    {
                        ProcessedString = ProcessedString.Substring(0, DotIndex);
                    }

                    DateTime Result;
                    if (DateTime.TryParse(ProcessedString, out Result))
                    {
                        ProcessedString = "'" + Result.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    }
                    else
                    {
                        ProcessedString = "'" + ProcessedString + "'";
                    }

                    input = input.Replace(m.Value, ProcessedString);
                }
            }

            return input;
        }
    }
}
