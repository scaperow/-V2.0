using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.IO;

namespace Yqun.Data.AccessHelper
{
    /// <summary>
    /// Access数据访问类
    /// </summary>
    public class OleDbDataHelper
    {
        /// <summary>
        /// 获取数据库连接信息
        /// </summary>
        /// <returns></returns>
        private string GetConnectionStr()
        {
            return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Path.Combine(System.Windows.Forms.Application.StartupPath, "AppConfig/Data/jzdb.mdb") + ";Jet OLEDB:Database Password=1q2w3e4R;Jet OLEDB:Engine Type=5"; ;
        }

        /// <summary>
        /// 获取数据连接对象
        /// </summary>
        /// <param name="ConnectionStr"></param>
        /// <returns></returns>
        private OleDbConnection GetConnection(string ConnectionStr)
        {
            try
            {
                return new OleDbConnection(ConnectionStr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string SqlStr)
        {
            try
            {
                return GetDataTable(SqlStr, GetConnection(GetConnectionStr()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <param name="_OleDbConnection"></param>
        /// <returns></returns>
        private DataTable GetDataTable(string SqlStr, OleDbConnection _OleDbConnection)
        {
            try
            {
                if (_OleDbConnection.State != ConnectionState.Open)
                {
                    _OleDbConnection.Open();
                }
                using (OleDbDataAdapter _da = new OleDbDataAdapter(SqlStr, _OleDbConnection))
                {
                    DataSet _ds = new DataSet();
                    _da.Fill(_ds);
                    if (_ds.Tables.Count > 0)
                    {
                        return _ds.Tables[0];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _OleDbConnection.Close();
                _OleDbConnection.Dispose();
            }
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <returns></returns>
        public bool ExcuteCommand(string SqlStr)
        {
            try
            {
                return ExcuteCommand(SqlStr, GetConnection(GetConnectionStr()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <param name="_OleDbConnection"></param>
        /// <returns></returns>
        private bool ExcuteCommand(string SqlStr, OleDbConnection _OleDbConnection)
        {
            try
            {
                if (_OleDbConnection.State != ConnectionState.Open)
                {
                    _OleDbConnection.Open();
                }
                using (OleDbCommand _OleDbCommand = new OleDbCommand(SqlStr, _OleDbConnection))
                {
                    _OleDbCommand.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _OleDbConnection.Close();
                _OleDbConnection.Dispose();
            }
        }

        
    }
}
