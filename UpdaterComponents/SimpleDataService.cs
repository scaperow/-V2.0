using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SQLite;

namespace UpdaterComponents
{
    public class SimpleDataService
    {
        SQLiteData sqliteata;
        public SimpleDataService(String DataSource, int PageSize, String Password)
        {
            sqliteata = new SQLiteData();
            sqliteata.ConnStr = "Data Source=" + DataSource + ";page size=" + PageSize + ";Password=" + Password + ";cache size=4000;BinaryGUID=False;";
        }

        public SimpleDataService(String DataSource, int PageSize)
        {
            sqliteata = new SQLiteData();
            sqliteata.ConnStr = "Data Source=" + DataSource + ";page size=" + PageSize + ";cache size=4000;BinaryGUID=False;";
        }

        /// <summary>
        /// 获得一个数据库连接,可以用于事务
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            using (SQLiteConnection connection = new SQLiteConnection(sqliteata.ConnStr))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                return sqliteata.GetConntion(sqliteata.ConnStr);
            }
        }

        #region  执行简单SQL语句

        /// <summary>
        /// 通过一条SQL命令获得一个表
        /// </summary>
        /// <param name="SqlCommandText"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string SqlCommandText)
        {
            using (SQLiteConnection connection = new SQLiteConnection(sqliteata.ConnStr))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                return sqliteata.GetDataTable(SqlCommandText, connection);
            }
        }

        /// <summary>
        /// 通过一条SQL命令获得一个表
        /// </summary>
        /// <param name="SqlCommandText"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string[] SelectSqlText)
        {
            using (SQLiteConnection connection = new SQLiteConnection(sqliteata.ConnStr))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                return sqliteata.GetDataSet(SelectSqlText, connection);
            }
        }

        /// <summary>
        /// 在打开的事务中执行一条SQL命令
        /// </summary>
        /// <param name="SqlCommandText">SQL命令</param>
        /// <param name="Transaction">一个在外部打开事务,也要在外部提交</param>
        /// <returns>成功返回1,失败返回-1</returns>
        public int ExcuteCommand(string SqlCommandText, Transaction Transaction)
        {
            using (SQLiteConnection connection = new SQLiteConnection(sqliteata.ConnStr))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                return sqliteata.ExcuteCommand(SqlCommandText, Transaction);
            }
        }

        /// <summary>
        /// 在打开的事务中执行一条SQL命令
        /// </summary>
        /// <param name="SqlCommandText">SQL命令</param>
        /// <param name="Transaction">一个在外部打开事务,也要在外部提交</param>
        /// <returns>成功返回1,失败返回-1</returns>
        public int[] ExcuteCommands(string[] SqlCommandText, Transaction Transaction)
        {
            using (SQLiteConnection connection = new SQLiteConnection(sqliteata.ConnStr))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                return sqliteata.ExcuteCommands(SqlCommandText, Transaction);
            }
        }

        /// <summary>
        /// 在打开的事务中执行一条SQL命令
        /// </summary>
        /// <param name="SqlCommandText">SQL命令</param>
        /// <param name="Transaction">一个在外部打开事务,也要在外部提交</param>
        /// <returns>成功返回1,失败返回-1</returns>
        public int UpdateCommand(DataTable WillUpdateDataTable, Transaction Transaction)
        {
            using (SQLiteConnection connection = new SQLiteConnection(sqliteata.ConnStr))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                return sqliteata.Update(WillUpdateDataTable, Transaction);
            }
        }

        /// <summary>
        /// 执行一条SQL命令
        /// </summary>
        /// <param name="SqlCommandText"></param>
        /// <returns>成功返回1,失败返回-1</returns>
        public int ExcuteCommand(string SqlCommandText)
        {
            using (SQLiteConnection connection = new SQLiteConnection(sqliteata.ConnStr))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                return sqliteata.ExcuteCommand(SqlCommandText, connection);
            }
        }

        /// <summary>
        /// 执行多条SQL命令
        /// </summary>
        /// <param name="SqlCommandText"></param>
        /// <returns>成功返回1,失败返回-1</returns>
        public int[] ExcuteCommands(string[] SqlCommandText)
        {
            using (SQLiteConnection connection = new SQLiteConnection(sqliteata.ConnStr))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                return sqliteata.ExcuteCommands(SqlCommandText, connection);
            }
        }

        #endregion
    }
}
