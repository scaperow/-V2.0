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
using System.Diagnostics;
using System.Collections;
using System.Globalization;
using Microsoft.VisualBasic;

namespace Yqun.Data.DataBase
{
    public class SQLClientData : IData
    {
        //ʹ��log4net.dll��־�ӿ�ʵ����־��¼
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SQLClientData()
        {
        }

        /// <summary>
        /// ��õ�ǰ�к�
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
        /// ��õ�ǰ�к�
        /// </summary>
        private String loginUserName
        {
            get
            {
                return Yqun.Common.ContextCache.ApplicationContext.Current.UserName;
            }
        }

        #region IData Members

        #region DataAccessBase

        #region DataCommon

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

        public string GetConnString(DataBaseType HereType,
            string DataSource,
            string DataBaseInstance,
            string Username,
            string PassWord,
            bool ISDataBaseAttachFile)
        {
            try
            {
                string str = "";
                if (!ISDataBaseAttachFile)
                {
                    str = @"Data Source=" +
                        DataSource +
                        ";Initial Catalog=" +
                        DataBaseInstance +
                        ";User ID=" +
                        Username +
                        ";Password=" +
                        PassWord + ";Max Pool Size=1000;Application Name=YQUN_SQLCLIET;Connect Timeout=100";
                }
                else
                {
                    str = @"Data Source=" +
                        DataSource +
                        ";AttachDbFilename=" +
                        DataBaseInstance +
                        ";User ID=" +
                        Username +
                        ";Password=" +
                        PassWord + ";Max Pool Size=1000;Application Name=YQUN_SQLCLIET";
                }

                return str;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string GetIntegratedConnString(string DataSource,
            string DataBaseInstance, bool ISDataBaseAttachFile)
        {
            try
            {
                string str = "";
                if (!ISDataBaseAttachFile)
                {
                    str = @"Data Source=" +
                       DataSource +
                       ";Initial Catalog=" +
                       DataBaseInstance +
                       ";" + "Integrated Security=True;Max Pool Size=1000;Application Name=YQUN_SQLCLIET;Connect Timeout=60";
                }
                else
                {
                    str = @"Data Source=" + DataSource +
                        ";AttachDbFilename=" + DataBaseInstance + ";Integrated Security=True;User Instance=True;Max Pool Size=1000;Application Name=YQUN_SQLCLIET";
                }
                return str;
            }
            catch (Exception )
            {
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

                SqlConnection conn = new SqlConnection(str);
                return conn;
            }
            catch (Exception )
            {
                return null;
            }
        }

        public System.Data.IDbConnection GetConntion(string ConnectionString)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConnectionString);
                
                return conn;
            }
            catch (Exception )
            {
                return null;
            }
        }

        public IDbDataAdapter GetDataAdapter(string SelectText,
            IDbConnection Connection)
        {
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter(SelectText,
                    (SqlConnection)Connection);
                return adp;
            }
            catch (Exception )
            {
                return null;
            }
        }

        public IDbDataAdapter GetDataAdapter(string SelectText,string connStr)
        {
            try
            {

                SqlDataAdapter adp = new SqlDataAdapter(SelectText,
                    new SqlConnection(connStr));
                return adp;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IDbCommand GetDbCommand(string CommandText,
            IDbConnection Connection)
        {
            try
            {
                SqlCommand comm = new SqlCommand(CommandText,
                    (SqlConnection)Connection);
                return comm;
            }
            catch (Exception )
            {
                return null;
            }
        }

        public DbCommandBuilder CreateAdapterCommand(ref IDbDataAdapter DbDataAdapter)
        {
            try
            {
                DbCommandBuilder b = new SqlCommandBuilder(DbDataAdapter as SqlDataAdapter);

                DbDataAdapter.InsertCommand = b.GetInsertCommand();
                DbDataAdapter.UpdateCommand = b.GetUpdateCommand();
                DbDataAdapter.DeleteCommand = b.GetDeleteCommand();

                DbDataAdapter.InsertCommand.CommandTimeout = 180;
                DbDataAdapter.UpdateCommand.CommandTimeout = 180;
                DbDataAdapter.DeleteCommand.CommandTimeout = 180;

                return b;
            }
            catch (Exception )
            {
                return null;
            }
        }

        #endregion

        #region DataAccess

        /// <summary>
        /// ����SelectCommandText��ȡһ���ձ�
        /// </summary>
        /// <param name="SelectCommandText">Select���</param>
        /// <param name="Connection">����</param>
        /// <returns>�����ݱ�</returns>
        public DataTable GetTableStructBySql(string SelectCommandText,IDbConnection Connection)
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

                logger.Error(string.Format("[GetTableStructBySql@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                return null;
            }
        }

        /// <summary>
        /// ���ݱ����ƻ�ȡһ������ȫ���ֶεĿձ�
        /// </summary>
        /// <param name="TableName">������</param>
        /// <param name="Connection">����</param>
        /// <returns>�����ݱ�</returns>
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

                logger.Error(string.Format("[GetTableStruct@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                return null;
            }
        }

        /// <summary>
        /// ��ȡ����һ���ձ�����ݼ�
        /// </summary>
        /// <param name="TableName">������</param>
        /// <param name="Connection">����</param>
        /// <returns>����һ���ձ�����ݼ�</returns>
        public DataSet GetTableStructSet(string TableName,IDbConnection Connection)
        {
            try
            {
                DataSet d = new DataSet();
                IDbDataAdapter ad = GetDataAdapter("select top 1 * from " + TableName, Connection);
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

                logger.Error(string.Format("[GetTableStructSet@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                return null;
            }
        }
        
        /// <summary>
        /// ��ȡ��������ձ�����ݼ���Ҫ�����������ͬ����,������ͬ��ʱ�������� �� ԭʼ�����ƣ���&�����������е����к�
        /// </summary>
        /// <param name="TableNames">������</param>
        /// <param name="Connection">����</param>
        /// <returns>��ȡ��������ձ�����ݼ�</returns>
        public DataSet GetTableStructsSets(string[] TableNames,IDbConnection Connection)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string[] name = new string[TableNames.Length];
                string current = String.Empty;
                ArrayList al = new ArrayList();

                //����ȥ��
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

                logger.Error(string.Format("[GetTableStructsSets@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                return null;
            }
        }

        /// <summary>
        /// ��ȡ�ձ������
        /// </summary>
        /// <param name="TableNames">������</param>
        /// <param name="Connection">����</param>
        /// <returns>�ձ������</returns>
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

                logger.Error(string.Format("[GetTableStructs@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                return new DataTable[0];
            }
        }


        /// <summary>
        /// ����SQL����ȡ���ݼ���ע�⴦���������ͬ���⡣
        /// </summary>
        /// <param name="SelectSqlText">Sql��ѯ ���</param>
        /// <param name="Connection">����</param>
        /// <returns>���ݼ�</returns>
        public DataSet GetDataSet(string SelectSqlText,IDbConnection Connection)
        {
            //��ÿһ�������Caption�������óɡ�������.�����ơ�����ʽ���ֶ�������
            try
            {
                DataSet d = new DataSet();
                IDbDataAdapter ad = GetDataAdapter(SelectSqlText, Connection);
                ad.SelectCommand.CommandTimeout = 60;
                ad.Fill(d);

                //����������������
                string[] strCol = SQLResolver.GetColNameFromSelectCommand(SelectSqlText);
                //���������б�����
                string strTable = SQLResolver.GetTableNamesFromSeleCommandText(SelectSqlText);

                //��ֱ��������� 
                string[] tableArr = strTable.Split(',');
                for (int k = 0; k < tableArr.Length;k++ )
                {
                    tableArr[k] = tableArr[k].Trim();
                }

                Hashtable h = new Hashtable();
                string[] t;

                if (d.Tables.Count > 0)
                {
                    if (!tableArr[0].Contains(" "))//���û��ʹ�ñ�ı���
                    {
                        for (int i = 0; i < d.Tables[0].Columns.Count; i++)
                        {
                            d.Tables[0].Columns[i].Caption = d.Tables[0].Columns[i].ColumnName;
                            d.Tables[0].Columns[i].ExtendedProperties.Add("EXP", d.Tables[0].Columns[i].ColumnName);
                        }

                        for (int i = 0; i < d.Tables[0].Columns.Count; i++)
                        {
                            if (i >= strCol.Length)
                                break;

                            string name = strCol[i];

                            if (name.IndexOf("*") == -1)
                            {
                                d.Tables[0].Columns[i].Caption = name;
                                d.Tables[0].Columns[i].ExtendedProperties["EXP"] = name;
                            }
                        }

                        d.Tables[0].TableName = strTable;
                    }
                    else //���ʹ���˱�ı���
                    {
                        for (int i = 0; i < tableArr.Length; i++)
                        {
                            t = tableArr[i].Split(' ');
                            if (t.Length < 2 || t[1] == "")
                                continue;

                            if (!h.ContainsKey(t[1]))
                            {
                                h.Add(t[1], t[0]);
                            }

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
                            current = current + "," + ((string)de.Value).ToLower();
                        }
                        d.Tables[0].TableName = current.Substring(1, current.Length - 1);

                        for (int i = 0; i < d.Tables[0].Columns.Count; i++)
                        {
                            if (i >= strCol.Length)
                                break;

                            string name = strCol[i];
                            if (name.IndexOf("*") == -1)
                            {
                                d.Tables[0].Columns[i].Caption = strCol[i];
                                d.Tables[0].Columns[i].ExtendedProperties["EXP"] = strCol[i];
                            }
                        }
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

                logger.Error(string.Format("[GetDataSet@{0}({3})]:{1},{2}", currentLineNumber, ee.Message, SelectSqlText, loginUserName));
                return new DataSet();
            }
        }

        /// <summary>
        /// �ò����ķ�ʽ��֯��������䣬����SelectSqlText��֯��һ�������Ĳ�ѯ��䣬��ѯ��һ�����ݼ���
        /// ���磺select A from t where A = @AA,Ȼ���@AA������ֵ���˷������Է�ֹSQL�����ƭ��
        /// </summary>
        /// <param name="SelectSqlText">��ѯ��䣬������Where���֣�ֻ����Where֮ǰ�Ĳ���</param>
        /// <param name="ConditionColumns">�����У����鳤�Ⱥ�˳�������¼���������ͬ</param>
        /// <param name="CompareExpressions">�жϷ��ţ����ں�(=)�����ڡ�С�ڡ������ڣ����鳤�Ⱥ�˳�������¼���������ͬ</param>
        /// <param name="ParameterValues">�����е�ֵ�����鳤�Ⱥ�˳�������ϼ���������ͬ</param>
        /// <param name="Connection">����</param>
        /// <returns>���ݼ�</returns>
        public DataSet GetDataSet(string SelectSqlText,string[] ConditionColumns,string[] CompareExpressions,object[] ParameterValues,IDbConnection Connection)
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

                cmd.CommandText = SelectSqlText + w;
                cmd.CommandType = CommandType.Text;
                
                for (int i = 0; i < ConditionColumns.Length; i++)
                {
                    SqlParameter para = new SqlParameter("@P" + i.ToString(), ParameterValues[i]);
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

                logger.Error(string.Format("[GetDataSet@{0}({2})]:{1}, {3}", currentLineNumber, ee.Message, loginUserName, SelectSqlText));
                return null;
            }            
        }

        /// <summary>
        /// ����һ���������Ĳ�ѯ��䣬��ѯ��һ�����ݼ���
        /// ���磺select A from t where A = @AA,Ȼ���@AA������ֵ���˷������Է�ֹSQL�����ƭ��
        /// </summary>
        /// <param name="SelectSqlText">��ѯ��䣬������䣬����Where����</param>
        /// <param name="ParameterValues">��������ֵ�����鳤�Ⱥ�˳��������Ĳ���������������˳����ͬ</param>
        /// <param name="Connection">����</param>
        /// <returns>���ݼ�</returns>
        public DataSet GetDataSet(string SelectSqlText,object[] ParameterValues,IDbConnection Connection)
        {
            try
            {
                DataSet ds = new DataSet();
                IDbCommand cmd = Connection.CreateCommand();
                cmd.CommandText = SelectSqlText;
                cmd.CommandType = CommandType.Text;

                IDbDataAdapter ad = GetDataAdapter(SelectSqlText, Connection);
                ad.SelectCommand = cmd;
                for (int i = 0; i < ParameterValues.Length; i++)
                {
                    SqlParameter para = new SqlParameter("@"+i.ToString(), ParameterValues[i]);
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

                logger.Error(string.Format("[GetDataSet@{0}({2})]:{1}, {3}", currentLineNumber, ee.Message, loginUserName, SelectSqlText));
                return null;
            } 
        }

        /// <summary>
        /// ��������ĳ����������ȡ�������������ݼ���ע�⴦���������ͬ�����⡣
        /// </summary>
        /// <param name="SelectSqlText">SQL�������</param>
        /// <param name="Connection">����</param>
        /// <returns>���ݼ�</returns>
        public DataSet GetDataSet(string[] SelectSqlText,IDbConnection Connection)
        {
            DataSet ds = new DataSet();

            try
            {
                for (int i = 0; i < SelectSqlText.Length; i++)
                {
                    try
                    {
                        DataTable dt = (GetDataSet(SelectSqlText[i], Connection)).Tables[0];
                        ds.Tables.Add(dt.Copy());
                    }
                    catch (Exception)
                    {}
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

                logger.Error(string.Format("[GetDataSet@{0}({2})]:{1},{3}", currentLineNumber, ee.Message, loginUserName, String.Join(";", SelectSqlText))); 
                return ds;
            }
        }

        /// <summary>
        /// ��������ĳ��������ȡһ�����ݼ�����
        /// </summary>
        /// <param name="SelectSqlText">SQL�������</param>
        /// <param name="Connection">����</param>
        /// <returns>���ݼ�����</returns>
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

                logger.Error(string.Format("[GetDataSets@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                return null;
            }
        }

        /// <summary>
        /// ��������ĳ��������ȡ���ݱ�
        /// </summary>
        /// <param name="SelectSqlText">SQL���</param>
        /// <param name="Connection">����</param>
        /// <returns>���ݱ�</returns>
        public DataTable GetDataTable(string SelectSqlText,IDbConnection Connection)
        {
            DataTable dt = new DataTable();

            try 
            {
                DataSet ds = GetDataSet(SelectSqlText, Connection);
                if (ds.Tables.Count > 0)
                    dt = ds.Tables[0];

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

                logger.Error(string.Format("[GetDataTable@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));

                return dt;
            }
        }

        /// <summary>
        /// ��ȡ��һ�е�һ�����ݶ���
        /// </summary>
        /// <param name="SqlCommandText">SQL���</param>
        /// <param name="Connection">����</param>
        /// <returns>����</returns>
        public object ExcuteScalar(string SqlCommandText,IDbConnection Connection)
        {
            try
            {
                DataTable dt = GetDataTable(SqlCommandText, Connection);
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                if (dt.Rows.Count > 0)
                    return dt.Rows[0][0];
                return null;
            }
            catch (Exception ee)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }

                logger.Error(string.Format("[ExcuteScalar@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                return null;
            }
        }

        /// <summary>
        /// ��ȡ��һ�е�һ�����ݶ�������
        /// </summary>
        /// <param name="SqlCommandText">SQL���</param>
        /// <param name="Connection">����</param>
        /// <returns>��������</returns>
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

                logger.Error(string.Format("[ExcuteScalars@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                return null;
            }
        }

        public int RunStoreProcedure(string sp, IDbConnection Connection)
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                IDbCommand com = GetDbCommand(sp, Connection);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 180;
                int i = com.ExecuteNonQuery();
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

                logger.Error(string.Format("[RunStoreProcedure@{0}({2})]:{1},{3}", currentLineNumber, ee.Message, loginUserName, sp));
                return -1;
            }
        }

        /// <summary>
        /// ִ������
        /// </summary>
        /// <param name="SqlCommandText">���</param>
        /// <param name="Connection">����</param>
        /// <returns>��ȷΪ1���쳣Ϊ-1</returns>
        public int ExcuteCommand(string SqlCommandText,IDbConnection Connection)
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                IDbCommand com = GetDbCommand(SqlCommandText, Connection);
                int i = com.ExecuteNonQuery();
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

                logger.Error(string.Format("[ExcuteCommand@{0}({2})]:{1},{3}", currentLineNumber, ee.Message, loginUserName, SqlCommandText));
                return -1;
            }
        }


        /// <summary>
        /// ѭ��ִ����������
        /// </summary>
        /// <param name="SqlCommandText">��������</param>
        /// <param name="Connection">����</param>
        /// <returns>1��-1������</returns>
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

                for (int i = 0; i < SqlCommandText.Length; i++)
                {
                    if (string.IsNullOrEmpty(SqlCommandText[i])) 
                    {
                        r[i] = 1;
                        continue;
                    }

                    IDbCommand com = GetDbCommand(SqlCommandText[i], Connection);
                    com.ExecuteNonQuery();
                    r[i] = 1;
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

                logger.Error(string.Format("[ExcuteCommands@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                foreach (String s in SqlCommandText)
                    logger.Error(s);
            }

            return null;
        }

        /// <summary>
        /// ʹ������ִ��������������ύ��������ʹ���߳������ύ
        /// </summary>
        /// <param name="SqlCommandText">SQL�������</param>
        /// <param name="HereTransaction">����</param>
        /// <returns>��ȷΪ1���쳣Ϊ-1</returns>
        public int ExcuteCommand(string SqlCommandText, Transaction HereTransaction)
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
              
                int i = comm.ExecuteNonQuery();
           
                return 1;
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[ExcuteCommand@{0}({2})]:{1},{3}", currentLineNumber, ee.Message, loginUserName, SqlCommandText));
            }

            return -1;
        }
        
        /// <summary>
        /// ѭ��ִ�ж�����ʾ������
        /// </summary>
        /// <param name="SqlCommandText">�����ı�</param>
        /// <param name="HereTransaction">����</param>
        /// <returns>ÿ������ִ�з���ֵ��ɵ�����</returns>
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
                logger.Error(string.Format("[ExcuteCommands@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
            }

            return null;
        }

        /// <summary>
        /// ʹ������ʵ��ԭ����
        /// </summary>
        /// <param name="commands">��������command</param>
        /// <param name="Connection"></param>
        /// <returns></returns>
        public Boolean ExcuteCommandsWithTransaction(List<IDbCommand> commands, IDbConnection Connection)
        {
            Boolean flag = false;
            IDbTransaction myTrans = null;
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }
                myTrans = Connection.BeginTransaction();
                foreach (IDbCommand cmd in commands)
                {
                    cmd.Connection = Connection;
                    cmd.Transaction = myTrans;
                    cmd.ExecuteNonQuery();
                }
                myTrans.Commit();
                flag = true;
            }
            catch (Exception e)
            {
                myTrans.Rollback();
                throw e;
            }
            finally
            {
                Connection.Close();
            }
            return flag;
        }

        /// <summary>
        /// ��ȡ���ݶ�ȡ����
        /// </summary>
        /// <param name="SQLCommandText">�����ı�</param>
        /// <param name="Connection">����</param>
        /// <returns>IDataReader</returns>
        public IDataReader ExcuteReader(string SQLCommandText,IDbConnection Connection)
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

                logger.Error(string.Format("[ExcuteReader@{0}({2})]:{1},{3}", currentLineNumber, ee.Message, loginUserName, SQLCommandText));
            }

            return null;
        }

        /// <summary>
        /// �������ݼ�
        /// </summary>
        /// <param name="WillUpdateDataSet">���ݼ�</param>
        /// <param name="Connection">����</param>
        /// <returns>1��1</returns>
        public int Update(DataSet WillUpdateDataSet,IDbConnection Connection)
        {
            String errorSql = "";
            try
            {
                foreach (DataTable Data in WillUpdateDataSet.Tables)
                {
                    errorSql = Newtonsoft.Json.JsonConvert.SerializeObject(Data);
                    string tName = GetTrueTableName(Data.TableName);
                    string col = String.Empty;
                    for (int i = 0; i < Data.Columns.Count; i++)
                    {
                        if (i == 0)
                        {
                            col = "[" + Data.Columns[i].ColumnName + "]";
                        }
                        else
                        {
                            col = col + ",[" + Data.Columns[i].ColumnName + "]";
                        }
                    }

                    string SQL = "select " + col + " from " + tName;

                    IDbDataAdapter ad = GetDataAdapter(SQL, Connection);
                    DbCommandBuilder cb = CreateAdapterCommand(ref ad);
                    SqlDataAdapter adParticular = (SqlDataAdapter)ad;
                    adParticular.Update(Data);
                }

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

                List<string> TableNames = new List<string>();
                foreach(DataTable data in WillUpdateDataSet.Tables)
                    TableNames.Add(data.TableName);
                logger.Error(string.Format("[Update@{0}({3})]:{1},{2},error data = {4}", currentLineNumber, ee.Message, string.Join(",", TableNames.ToArray()), loginUserName,errorSql));
            }

            return -1;
        }

        /// <summary>
        /// �������ݱ�
        /// </summary>
        /// <param name="WillUpdateDataTable">���ݱ�</param>
        /// <param name="Connection">����</param>
        /// <returns>1����1</returns>
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
                if (Update(ds, Connection) > 0)
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

                List<string> TableNames = new List<string>();
                TableNames.Add(WillUpdateDataTable.TableName);
                logger.Error(string.Format("[Update@{0}({3})]:{1},{2}", currentLineNumber, ee.Message, string.Join(",", TableNames.ToArray()), loginUserName));
            }

            return -1;
        }

        /// <summary>
        /// �����м�
        /// </summary>
        /// <param name="WillUpdateDataRows">�м�</param>
        /// <param name="Connection">����</param>
        /// <returns>1����1</returns>
        public int Update(DataRow[] WillUpdateDataRows,IDbConnection Connection)
        {
            try
            {
                string col = String.Empty;
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }
                for (int i = 0; i < WillUpdateDataRows[0].Table.Columns.Count; i++)
                {
                    if (i == 0)
                    {
                        col = "[" + WillUpdateDataRows[0].Table.Columns[i].ColumnName + "]";
                    }
                    else
                    {
                        col = col + ",[" + WillUpdateDataRows[0].Table.Columns[i].ColumnName + "]";
                    }
                }
                string SQL = "select " + col + " from " + WillUpdateDataRows[0].Table.TableName;

                IDbDataAdapter ad = GetDataAdapter(SQL, Connection);
                DbCommandBuilder cb = CreateAdapterCommand(ref ad);
                SqlDataAdapter adParticular = (SqlDataAdapter)ad;

                adParticular.Update(WillUpdateDataRows);

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

                logger.Error(string.Format("[Update@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
            }

            return -1;
        }

        /// <summary>
        /// ������������ݼ�
        /// </summary>
        /// <param name="WillUpdateDataSet"></param>
        /// <param name="HereTransaction"></param>
        /// <returns></returns>
        public int Update(DataSet WillUpdateDataSet,Transaction HereTransaction)
        {
            try
            {
                IDbConnection conn = HereTransaction.DBTransaction.Connection;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                foreach (DataTable datatable in WillUpdateDataSet.Tables)
                {
                    string tName = GetTrueTableName(datatable.TableName);
                    string col = String.Empty;
                    for (int i = 0; i < datatable.Columns.Count; i++)
                    {
                        if (i == 0)
                        {
                            col = "[" + datatable.Columns[i].ColumnName + "]";
                        }
                        else
                        {
                            col = col + ",[" + datatable.Columns[i].ColumnName + "]";
                        }
                    }

                    string SQL = "select " + col + " from " + tName;

                    IDbDataAdapter ad = GetDataAdapter(SQL, conn);
                    DbCommandBuilder cb = CreateAdapterCommand(ref ad);

                    SqlDataAdapter adParticular = (SqlDataAdapter)ad;
                    adParticular.SelectCommand.Transaction = (SqlTransaction)HereTransaction.DBTransaction;
                    adParticular.Update(datatable);          
                }

                return 1;
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[Update@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
            }

            return -1;
        }

        /// <summary>
        /// ������������ݱ�
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
                logger.Error(string.Format("[Update@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
            }

            return -1;
        }
        
        /// <summary>
        /// ����������м�
        /// </summary>
        /// <param name="WillUpdateDataRows"></param>
        /// <param name="HereTransaction"></param>
        /// <returns></returns>
        public int Update(DataRow[] WillUpdateDataRows,Transaction HereTransaction)
        {
            try
            {
                IDbConnection conn = HereTransaction.DBTransaction.Connection;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                
                string col = String.Empty;
                for (int i = 0; i < WillUpdateDataRows[0].Table.Columns.Count; i++)
                {
                    if (i == 0)
                    {
                        col = "[" + WillUpdateDataRows[0].Table.Columns[i].ColumnName + "]";
                    }
                    else
                    {
                        col = col + ",[" + WillUpdateDataRows[0].Table.Columns[i].ColumnName + "]";
                    }
                }
                string SQL = "select " + col + " from " + WillUpdateDataRows[0].Table.TableName;

                IDbDataAdapter ad = GetDataAdapter(SQL, conn);
                DbCommandBuilder cb = CreateAdapterCommand(ref ad);
                
                SqlDataAdapter adParticular = (SqlDataAdapter)ad;
                adParticular.SelectCommand.Transaction = (SqlTransaction)HereTransaction.DBTransaction;

                adParticular.Update(WillUpdateDataRows);

                return 1;
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[Update@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
            }

            return -1;
        }

        /// <summary>
        /// �ֶ�дSQL��䣬��IDΪ������
        /// where ID = @ID
        /// </summary>
        /// <param name="WillUpdateDataSet">���ݼ�</param>
        /// <param name="Connection">����</param>
        /// <returns>1����1</returns>
        public int UpdateByID(DataSet WillUpdateDataSet, IDbConnection Connection) 
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                string tName = GetTrueTableName(WillUpdateDataSet.Tables[0].TableName);
                string col = String.Empty;
                for (int i = 0; i < WillUpdateDataSet.Tables[0].Columns.Count; i++)
                {
                    if (i == 0)
                    {
                        col = "[" + WillUpdateDataSet.Tables[0].Columns[i].ColumnName + "]";
                    }
                    else
                    {
                        col = col + ",[" + WillUpdateDataSet.Tables[0].Columns[i].ColumnName + "]";
                    }
                }

                string SQL = "select " + col + " from " + tName;

                IDbDataAdapter ad = GetDataAdapter(SQL, Connection);
                SqlCommandBuilder cb = (SqlCommandBuilder)CreateAdapterCommand(ref ad);

                SqlCommand comm = cb.GetDeleteCommand();

                string strDelete =comm.CommandText;// delete��
                comm.Parameters.Clear();// ��ղ���
                comm.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ID",SqlDbType.VarChar,36,"ID")); // ���@ID����                
                strDelete = strDelete.ToLower(); // ת��Сд
                strDelete = strDelete.Substring(0,strDelete.IndexOf(" where ")); // ȥ��Where����
                strDelete = strDelete + " where ID = @ID";// ���Where����
                comm.CommandText = strDelete;
                ad.DeleteCommand = comm;

                SqlCommand commUpdate = cb.GetUpdateCommand();

                string strUpdate = commUpdate.CommandText;// update��
                strUpdate = strUpdate.ToLower();// ת��Сд                
                string strUpadateWhere = strUpdate.Substring(strUpdate.IndexOf(" where "));// update����where����
                strUpdate = strUpdate.Substring(0, strUpdate.IndexOf(" where "));// update���г�ȥwhere�Ĳ���
                int atCount = 0;// ��������
                int startPos = strUpadateWhere.IndexOf("@", 0);// "@"�ڴ��е�λ��
                // ͳ�Ʋ�������
                for (int i = 0; i < strUpadateWhere.Length; i++)
                {
                    if (startPos != -1)
                    {
                        startPos = strUpadateWhere.IndexOf("@", startPos + 1);
                        atCount++;
                    }
                    else 
                    {
                        break;
                    }
                }
                
                // ɾ������
                for (int i = 0; i < atCount; i++)
                {
                    commUpdate.Parameters.RemoveAt(commUpdate.Parameters.Count - 1);
                }
                
                // ƴдSQL
                strUpdate = strUpdate + " where ID = @ID";
                commUpdate.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ID", SqlDbType.VarChar, 36, "ID"));
                commUpdate.CommandText = strUpdate;
                ad.UpdateCommand = commUpdate;
                
                // insert
                ad.InsertCommand = cb.GetInsertCommand();

                SqlDataAdapter adParticular = (SqlDataAdapter)ad;
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

                logger.Error(string.Format("[UpdateByID@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
            }

            return -1;
        }

        /// <summary>
        /// �ֶ�дSQL��䣬��IDΪ������
        /// where ID = @ID
        /// </summary>
        /// <param name="WillUpdateDataSet">���ݼ�</param>
        /// <param name="HereTransaction">����</param>
        /// <returns>1����1</returns>
        public int UpdateByID(DataSet WillUpdateDataSet, Transaction HereTransaction)
        {
            try
            {
                IDbConnection Connection = HereTransaction.DBTransaction.Connection;
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                // select
                string tName = GetTrueTableName(WillUpdateDataSet.Tables[0].TableName);
                string col = String.Empty;
                for (int i = 0; i < WillUpdateDataSet.Tables[0].Columns.Count; i++)
                {
                    if (i == 0)
                    {
                        col = "[" + WillUpdateDataSet.Tables[0].Columns[i].ColumnName + "]";
                    }
                    else
                    {
                        col = col + ",[" + WillUpdateDataSet.Tables[0].Columns[i].ColumnName + "]";
                    }
                }

                string SQL = "select " + col + " from " + tName;
                
                IDbDataAdapter ad = GetDataAdapter(SQL, Connection);
                SqlCommandBuilder cb = (SqlCommandBuilder)CreateAdapterCommand(ref ad);
                
                SqlDataAdapter adParticular = (SqlDataAdapter)ad;
                adParticular.SelectCommand.Transaction = (SqlTransaction)HereTransaction.DBTransaction;

                // delete
                SqlCommand commDelete = cb.GetDeleteCommand();

                string strDelete = commDelete.CommandText;// delete��
                commDelete.Parameters.Clear();// ��ղ���
                commDelete.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ID", SqlDbType.VarChar, 36, "ID")); // ���@ID����                
                strDelete = strDelete.ToLower(); // ת��Сд
                strDelete = strDelete.Substring(0, strDelete.IndexOf(" where ")); // ȥ��Where����
                strDelete = strDelete + " where ID = @ID";// ���Where����
                commDelete.CommandText = strDelete;
                ad.DeleteCommand = commDelete;

                // update
                SqlCommand commUpdate = cb.GetUpdateCommand();

                string strUpdate = commUpdate.CommandText;// update��
                strUpdate = strUpdate.ToLower();// ת��Сд                
                string strUpadateWhere = strUpdate.Substring(strUpdate.IndexOf(" where "));// update����where����
                strUpdate = strUpdate.Substring(0, strUpdate.IndexOf(" where "));// update���г�ȥwhere�Ĳ���
                int atCount = 0;// ��������
                int startPos = strUpadateWhere.IndexOf("@", 0);// "@"�ڴ��е�λ��
                // ͳ�Ʋ�������
                for (int i = 0; i < strUpadateWhere.Length; i++)
                {
                    if (startPos != -1)
                    {
                        startPos = strUpadateWhere.IndexOf("@", startPos + 1);
                        atCount++;
                    }
                    else
                    {
                        break;
                    }
                }

                // ɾ������
                for (int i = 0; i < atCount; i++)
                {
                    commUpdate.Parameters.RemoveAt(commUpdate.Parameters.Count - 1);
                }

                // ƴдSQL
                strUpdate = strUpdate + " where ID = @ID";
                commUpdate.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ID", SqlDbType.VarChar, 36, "ID"));
                commUpdate.CommandText = strUpdate;
                ad.UpdateCommand = commUpdate;

                // insert
                ad.InsertCommand = cb.GetInsertCommand();

                adParticular.Update(WillUpdateDataSet.Tables[0]);

                return 1;
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[UpdateByID@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
            }

            return -1;
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
                logger.Error(string.Format("[AddNullToTable@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
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
                logger.Error(string.Format("[AddNullToTable@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
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
                logger.Error(string.Format("[DataFromSelectToTableName@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
            }

            return "";
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
                logger.Error(string.Format("[GetTrueTableName@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
            }

            return "";
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
                logger.Error(string.Format("[DataSplitCommand@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
            }

            return new string[0];
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
                logger.Error(string.Format("[DataSpl@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
            }

            return "";
        }

        #endregion

        #region BlobColumns


        /// <summary>
        /// ��ʱ����ʵ��
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

                logger.Error(string.Format("[SaveDataToBlobColumn@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
            }

            return -1;
        }

        /// <summary>
        /// ��ʱ����ʵ��
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
                    Connection.Close();
                }

                logger.Error(string.Format("[SaveDataToBlobColumn@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
            }

            return -1;
        }

        /// <summary>
        /// ��ʱ����ʵ��
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

                logger.Error(string.Format("[SaveDataToBlobColumn@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
            }

            return -1;
        }

        /// <summary>
        /// ��ʱ����ʵ��
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

                logger.Error(string.Format("[SaveDataToBlobColumn@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
            }

            return -1;
        }

        /// <summary>
        /// ��ʱ����ʵ��
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

            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {
                    SqlConnection Connection = (SqlConnection)HereTransaction.DBTransaction.Connection;
                    if (Connection.State == ConnectionState.Closed)
                    {
                        Connection.Open();
                    }

                    string update = "update " + TableName +
                        " set " + AimColumnName + " .WRITE ( @datavalue , 0 , null ) where " +
                        IDColumnName + " = '" + IDValue + "'";
                    SqlCommand com = new SqlCommand(update, Connection);

                    com.Parameters.Add(new SqlParameter("@datavalue", AimValue));
                    com.Transaction = (SqlTransaction)HereTransaction.DBTransaction;
                    int x = com.ExecuteNonQuery();

                    if (x >= 0)
                    {
                        x = 1;
                    }

                    return x;
                }
                catch (Exception ee)
                {
                    logger.Error(string.Format("[SaveDataToBlobColumn@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                    return -1;
                }
            }

            return -1;
        }

        /// <summary>
        /// ��ʱ����ʵ��
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
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {
                    SqlConnection Connection = (SqlConnection)HereTransaction.DBTransaction.Connection;
                    if (Connection.State == ConnectionState.Closed)
                    {
                        Connection.Open();
                    }

                    string update = "update " + TableName +
                        " set " + AimColumnName + " .WRITE ( @datavalue , " +
                        Offset + " , " + AimValue.Length + " ) where " +
                        IDColumnName + " = '" + IDValue + "'";
                    SqlCommand com = new SqlCommand(update, Connection);

                    com.Parameters.Add(new SqlParameter("@datavalue", AimValue));
                    com.Transaction = (SqlTransaction)HereTransaction.DBTransaction;
                    int x = com.ExecuteNonQuery();
                    if (x >= 0)
                    {
                        x = 1;
                    }

                    return x;
                }
                catch (Exception ee)
                {
                    logger.Error(string.Format("[SaveDataToBlobColumn@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                    return -1;
                }
            }

            return -1;
        }

        /// <summary>
        /// ��ʱ����ʵ��
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
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {
                    SqlConnection Connection = (SqlConnection)HereTransaction.DBTransaction.Connection;
                    if (Connection.State == ConnectionState.Closed)
                    {
                        Connection.Open();
                    }

                    string update = "update " + TableName +
                        " set " + AimColumnName + " .WRITE ( @datavalue , 0 , null ) where " +
                        IDColumnName + " = '" + IDValue + "'";
                    SqlCommand com = new SqlCommand(update, Connection);

                    com.Parameters.Add(new SqlParameter("@datavalue", AimValue));
                    com.Transaction = (SqlTransaction)HereTransaction.DBTransaction;
                    int x = com.ExecuteNonQuery();
                    if (x >= 0)
                    {
                        x = 1;
                    }

                    return x;
                }
                catch (Exception ee)
                {
                    logger.Error(string.Format("[SaveDataToBlobColumn@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                    return -1;
                }
            }

            return -1;
        }

        /// <summary>
        /// ��ʱ����ʵ��
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
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {
                    SqlConnection Connection = (SqlConnection)HereTransaction.DBTransaction.Connection;
                    if (Connection.State == ConnectionState.Closed)
                    {
                        Connection.Open();
                    }

                    string update = "update " + TableName +
                        " set " + AimColumnName + " .WRITE ( @datavalue , " +
                        Offset + " , " + AimValue.Length + " ) where " +
                        IDColumnName + " = '" + IDValue + "'";
                    SqlCommand com = new SqlCommand(update, Connection);

                    com.Parameters.Add(new SqlParameter("@datavalue", AimValue));
                    com.Transaction = (SqlTransaction)HereTransaction.DBTransaction;
                    int x = com.ExecuteNonQuery();
                    if (x >= 0)
                    {
                        x = 1;
                    }

                    return x;
                }
                catch (Exception ee)
                {
                    logger.Error(string.Format("[SaveDataToBlobColumn@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                    return -1;
                }
            }

            return -1;
        }

        /// <summary>
        /// ��ʱ����ʵ��
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
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
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
                    SqlDataReader r = (SqlDataReader)ExcuteReader(select, Connection);
                    bool canread = r.Read();
                    byte[] b = null;
                    if (canread)
                    {
                        b = (byte[])r[0];
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

                    logger.Error(string.Format("[ReadDataToBlobColumn@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                }
            }

            return new byte[0];
        }

        public byte[] ReadDataFromBlobColumn(DataBaseType HereDataBaseType,
            string TableName,
            string AimColumnName,
            string Condition,
            long Offset,
            int ReadLenght,
            IDbConnection Connection)
        {
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                if (Connection.State == ConnectionState.Closed) Connection.Open();
                string select = "select " + AimColumnName + " from " + TableName + Condition;
                SqlDataReader r = (SqlDataReader)ExcuteReader(select, Connection);
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

            return new byte[0];
        }


        /// <summary>
        /// ��ʱ����ʵ��
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
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
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
                    SqlDataReader r = (SqlDataReader)ExcuteReader(select, Connection);
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

                    logger.Error(string.Format("[ReadDataToBlobColumn@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                }
            }

            return new byte[0];
        }

        /// <summary>
        /// ��ʱ����ʵ��
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
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
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
                    SqlDataReader r = (SqlDataReader)ExcuteReader(select, Connection);
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

                    logger.Error(string.Format("[ReadDataToBlobColumn@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                }
            }

            return "";
        }

        /// <summary>
        /// ��ʱ����ʵ��
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
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {

                    if (Connection.State == ConnectionState.Closed)
                    {
                        Connection.Open();
                    }
                    Offset += 1;
                    string select = "select SUBSTRING (" + AimColumnName +
                        "," + Offset + "," + ReadLenght + ") " +
                        " from " + TableName + " where " +
                        IDColumnName + " = '" + IDValue + "'";
                    SqlCommand comm = (SqlCommand)GetDbCommand(select, Connection);
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

                    logger.Error(string.Format("[ReadDataToBlobColumn@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                }
            }

            return "";
        }

        /// <summary>
        /// ��ʱ����ʵ��
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
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
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

                    logger.Error(string.Format("[ReadDataToBlobColumn@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                }

            }

            return new DataSet();
        }

        /// <summary>
        /// �õ��ض����ض��ֶε��ֽڳ���
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
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {
                    if (Connection.State == ConnectionState.Closed)
                    {
                        Connection.Open();
                    }
                    string select = "select DATALENGTH (" + AimColumnName + ") " +
                        " from " + TableName + " where " +
                        IDColumnName + " = '" + IDValue + "'";
                    SqlCommand comm = (SqlCommand)GetDbCommand(select, Connection);
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

                    logger.Error(string.Format("[ReadDataToBlobColumn@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                }
            }

            return -1;
        }

        public long GetBlobDataLength(DataBaseType HereDataBaseType,
            string TableName,
            string AimColumnName,
            string Condition,
            IDbConnection Connection)
        {
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {
                    if (Connection.State == ConnectionState.Closed)
                    {
                        Connection.Open();
                    }
                    string select = "select DATALENGTH (" + AimColumnName + ") " +
                        " from " + TableName + Condition;
                    SqlCommand comm = (SqlCommand)GetDbCommand(select, Connection);
                    object o = comm.ExecuteScalar();
                    long x = 0;
                    if (o != null)
                    {
                        x = long.Parse(o.ToString());
                    }

                    if (Connection.State != ConnectionState.Closed)
                    {
                        try
                        {
                            Connection.Close();
                        }
                        catch { }
                    }

                    return x;
                }
                catch (Exception ee)
                {
                    if (Connection.State != ConnectionState.Closed)
                    {
                        Connection.Close();
                    }

                    logger.Error(string.Format("[GetBlobDataLength@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                }
            }

            return -1;
        }


        #endregion

        #endregion

        #region DataWatch
        /// <summary>
        /// ��ʱ����ʵ��
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
        /// �ݲ���ʵ��
        /// </summary>
        /// <param name="HereDataBaseType">���ݿ�����ö��</param>
        /// <param name="DataBaseName">���ݿ�����</param>
        /// <param name="Connection">����</param>
        /// <returns>·��</returns>
        public string GetMasterBasePath(DataBaseType HereDataBaseType,
            string DataBaseName,
            IDbConnection Connection)
        {
            return null;
        }

        /// <summary>
        /// �ݲ���ʵ��
        /// </summary>
        /// <param name="HereDataBaseType">��������</param>
        /// <param name="DataBaseName">���ݿ�����</param>
        /// <param name="Connection">����</param>
        /// <returns>1���ڣ�0�����ڣ���1�쳣</returns>
        public int IsDatabaseExist(DataBaseType HereDataBaseType,
            string DataBaseName,
            IDbConnection Connection)
        {
            return -1;

        }

        /// <summary>
        /// �ݲ���ʵ��
        /// </summary>
        /// <param name="HereDataBaseType">���ݿ�����</param>
        /// <param name="DataBaseName">���ݿ�����</param>
        /// <param name="DataBasePath">·��</param>
        /// <param name="Connection">����</param>
        /// <returns></returns>
        public int CreateNewDataBase(DataBaseType HereDataBaseType,
            string DataBaseName,
            string DataBasePath,
            IDbConnection Connection)
        {
            return -1;

        }

        /// <summary>
        /// �ݲ���ʵ��
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
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {
                    string str = "";
                    if (DataType.ToLower().Trim() == "string" ||
                        DataType.ToLower().Trim() == "guid" ||
                        DataType.ToLower().Trim() == "bigtext" ||
                        DataType.ToLower().Trim() == "autonum" ||
                        DataType.ToLower().Trim() == "hyperlink")
                    {
                        str = "N" + "'" + DefaultValue.ToString() + "'";
                    }
                    else if (DataType.ToLower().Trim() == "datetime")
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
                    logger.Error(string.Format("[GetBlobDataLength@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                }
            }

            return "";
        }

        public int IsDefaultExist(DataBaseType HereDataBaseType,
            string ConstraintName,
            IDbConnection Connection)
        {
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {
                    if (Connection.State == ConnectionState.Closed)
                    {
                        Connection.Open();
                    }

                    string comm1 = "select ID from sys.sysobjects where id = object_id(N'[" +
                        ConstraintName + "]') and OBJECTPROPERTY(id, N'IsDefaultCnst') = 1";

                    object o = ExcuteScalar(comm1, Connection);
                    return (o != null ? 1 : 0);
                }
                catch (Exception ee )
                {
                    if (Connection.State != ConnectionState.Closed)
                    {
                        Connection.Close();
                    }

                    logger.Error(string.Format("[GetBlobDataLength@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                }
            }

            return -1;
        }

        public int CreateDefault(DataBaseType HereDataBaseType,
            string TableName,
            string ColumnName,
            string ConstraintName,
            object DefaultValue,
            IDbConnection Connection)
        {
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {
                    if (Connection.State == ConnectionState.Closed)
                    {
                        Connection.Open();
                    }

                    string comm1 = "if exists (select ID from sys.sysobjects where id = object_id(N'[" +
                        ConstraintName + "]') and OBJECTPROPERTY(id, N'IsDefaultCnst') = 1)" +
                        " ALTER TABLE [" + TableName + "] DROP CONSTRAINT [" +
                        ConstraintName + "]";
                    string comm2 = "ALTER TABLE [" +
                        TableName + "] WITH NOCHECK ADD CONSTRAINT [" +
                        ConstraintName + "] DEFAULT (" +
                        DefaultValue.ToString() + ") FOR [" +
                        ColumnName + "]";
                    string comm = comm1 + ";" + comm2;

                    int i = ExcuteCommand(comm, Connection);

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

                    logger.Error(string.Format("[GetBlobDataLength@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                }
            }

            return -1;
        }

        public int CreateDefault(DataBaseType HereDataBaseType,
            string TableName,
            string ColumnName,
            string ConstraintName,
            object DefaultValue,
            Transaction HereTransaction)
        {
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {
                    string comm1 = "if exists (select ID from sys.sysobjects where id = object_id(N'[" +
                        ConstraintName + "]') and OBJECTPROPERTY(id, N'IsDefaultCnst') = 1)" +
                        " ALTER TABLE [" + TableName + "] DROP CONSTRAINT [" +
                        ConstraintName + "]";
                    string comm2 = "ALTER TABLE [" +
                        TableName + "] WITH NOCHECK ADD CONSTRAINT [" +
                        ConstraintName + "] DEFAULT (" +
                        DefaultValue.ToString() + ") FOR [" +
                        ColumnName + "]";
                    string comm = comm1 + ";" + comm2;

                    return ExcuteCommand(comm, HereTransaction);
                }
                catch (Exception ee)
                {
                    logger.Error(string.Format("[GetBlobDataLength@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                }
            }

            return -1;
        }

        public int DropDefault(DataBaseType HereDataBaseType,
            string TableName,
            string ConstraintName,
            IDbConnection Connection)
        {
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {
                    if (Connection.State == ConnectionState.Closed)
                    {
                        Connection.Open();
                    }

                    string comm1 = "if exists (select ID from sys.sysobjects where id = object_id(N'[" +
                        ConstraintName + "]') and OBJECTPROPERTY(id, N'IsDefaultCnst') = 1)" +
                        " ALTER TABLE [" + TableName +
                        "] DROP CONSTRAINT [" + ConstraintName + "]";

                    return ExcuteCommand(comm1, Connection);
                }
                catch (Exception ee)
                {
                    if (Connection.State != ConnectionState.Closed)
                    {
                        Connection.Close();
                    }

                    logger.Error(string.Format("[GetBlobDataLength@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                }
            }

            return -1;
        }

        public int DropDefault(DataBaseType HereDataBaseType,
            string TableName,
            string ConstraintName,
            Transaction HereTransaction)
        {
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {
                    string comm1 = "if exists (select ID from sys.sysobjects where id = object_id(N'[" +
                        ConstraintName + "]') and OBJECTPROPERTY(id, N'IsDefaultCnst') = 1)" +
                        " ALTER TABLE [" + TableName +
                        "] DROP CONSTRAINT [" + ConstraintName + "]";

                    return ExcuteCommand(comm1, HereTransaction);

                }
                catch (Exception ee)
                {
                    logger.Error(string.Format("[GetBlobDataLength@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                }
            }

            return -1;
        }

        public int IsPrimarykeyExist(DataBaseType HereDataBaseType,
            string PrimarykeyName,
            IDbConnection Connection)
        {
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {
                    if (Connection.State == ConnectionState.Closed)
                    {
                        Connection.Open();
                    }
                    string comm1 = "select ID from sys.sysobjects where id = object_id(N'[" +
                        PrimarykeyName + "]') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1";
                    object o = ExcuteScalar(comm1, Connection);

                    if (Connection.State != ConnectionState.Closed)
                    {
                        Connection.Close();
                    }

                    return (o != null? 1:0);
                }
                catch (Exception ee)
                {
                    if (Connection.State != ConnectionState.Closed)
                    {
                        Connection.Close();
                    }

                    logger.Error(string.Format("[GetBlobDataLength@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                }
            }

            return -1;
        }

        public int CreatePrimarykey(DataBaseType HereDataBaseType,
            string TableName,
            string ColumnName,
            string Primarykey,
            IDbConnection Connection)
        {
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {
                    if (Connection.State == ConnectionState.Closed)
                    {
                        Connection.Open();
                    }
                    string comm1 = "if exists (select ID from sys.sysobjects where id = object_id(N'[" +
                        Primarykey + "]') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)" +
                        " ALTER TABLE [" + TableName + "] DROP CONSTRAINT [" +
                        Primarykey + "]";
                    string comm2 = "ALTER TABLE [" + TableName +
                        "] WITH NOCHECK ADD CONSTRAINT [" + Primarykey +
                        "] PRIMARY KEY  CLUSTERED ( [" + ColumnName + "] )";
                    string comm = comm1 + ";" + comm2;

                    return ExcuteCommand(comm, Connection);
                }
                catch (Exception ee)
                {
                    if (Connection.State != ConnectionState.Closed)
                    {
                        Connection.Close();
                    }

                    logger.Error(string.Format("[GetBlobDataLength@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                }
            }

            return -1;
        }

        public int CreatePrimarykey(DataBaseType HereDataBaseType,
            string TableName,
            string ColumnName,
            string Primarykey,
            Transaction HereTransaction)
        {
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {

                    string comm1 = "if exists (select ID from sys.sysobjects where id = object_id(N'[" +
                        Primarykey + "]') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)" +
                        " ALTER TABLE [" + TableName + "] DROP CONSTRAINT [" +
                        Primarykey + "]";
                    string comm2 = "ALTER TABLE [" + TableName +
                        "] WITH NOCHECK ADD CONSTRAINT [" + Primarykey +
                        "] PRIMARY KEY  CLUSTERED ( [" + ColumnName + "] )";
                    string comm = comm1 + ";" + comm2;
                    return ExcuteCommand(comm, HereTransaction);

                }
                catch (Exception ee)
                {
                    logger.Error(string.Format("[GetBlobDataLength@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                }
            }

            return -1;
        }

        public int DropPrimarykey(DataBaseType HereDataBaseType,
            string TableName,
            string Primarykey,
            IDbConnection Connection)
        {
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {

                    if (Connection.State == ConnectionState.Closed)
                    {
                        Connection.Open();
                    }
                    string comm1 = "if exists (select ID from sys.sysobjects where id = object_id(N'[" +
                        Primarykey + "]') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)" +
                        " ALTER TABLE [" +
                        TableName +
                        "] DROP CONSTRAINT [" +
                        Primarykey + "]";

                    return ExcuteCommand(comm1, Connection);
                }
                catch (Exception ee)
                {
                    if (Connection.State != ConnectionState.Closed)
                    {
                        Connection.Close();
                    }

                    logger.Error(string.Format("[DropPrimarykey@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                }
            }

            return -1;
        }

        public int DropPrimarykey(DataBaseType HereDataBaseType,
            string TableName,
            string Primarykey,
            Transaction HereTransaction)
        {
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {
                    string comm1 = "if exists (select ID from sys.sysobjects where id = object_id(N'[" +
                        Primarykey + "]') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)" +
                        " ALTER TABLE [" +
                        TableName +
                        "] DROP CONSTRAINT [" +
                        Primarykey + "]";

                    return ExcuteCommand(comm1, HereTransaction);


                }
                catch (Exception ee)
                {
                    logger.Error(string.Format("[GetBlobDataLength@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                }
            }

            return -1;
        }

        public int IsTableExist(DataBaseType HereDataBaseType,
            string TableName,
            IDbConnection Connection)
        {
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {
                    if (Connection.State == ConnectionState.Closed)
                    {
                        Connection.Open();
                    }
                    string comm1 = "select ID from sys.sysobjects where id = object_id(N'[" +
                        TableName + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1";
                    object o = ExcuteScalar(comm1, Connection);
                    if (o != null)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception ee)
                {
                    if (Connection.State != ConnectionState.Closed)
                    {
                        Connection.Close();
                    }

                    logger.Error(string.Format("[GetBlobDataLength@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                }
            }

            return -1;
        }

        public int CreateTable(DataBaseType HereDataBaseType,
            string TableName,
            string[] Columns,
            string[] DataTypes,
            string[] Lenght,
            string[] NullAbles,
            IDbConnection Connection)
        {
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {
                    if (Columns.Length != DataTypes.Length ||
                        Columns.Length != Lenght.Length ||
                        Columns.Length != NullAbles.Length)
                    {
                        return 0;
                    }

                    if (Connection.State == ConnectionState.Closed)
                    {
                        Connection.Open();
                    }
                    string comm1 = "if exists (select ID from sys.sysobjects where id = object_id(N'[" +
                        TableName + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [" +
                        TableName + "]";

                    string comm2 = "CREATE TABLE [" + TableName + "]" + " (";
                    for (int i = 0; i < Columns.Length; i++)
                    {
                        if (Lenght[i] != null && Lenght[i] != "")
                        {
                            comm2 += "[" + Columns[i] + "]" + " " + "" +
                                DataTypes[i] + "" + "(" + Lenght[i] +
                                ")" + " " + NullAbles[i] + " ,";

                        }
                        else
                        {
                            comm2 += "[" + Columns[i] + "]" +
                                " " + "" + DataTypes[i] + "" +
                                " " + NullAbles[i] + " ,";
                        }

                    }
                    comm2 = comm2.Trim(new char[] { ',' });
                    comm2 += " )";

                    string comm = comm1 + ";" + comm2;
                    int ii =ExcuteCommand(comm, Connection);

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

                    logger.Error(string.Format("[CreateTable@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                }
            }

            return -1;
        }

        public int CreateTable(DataBaseType HereDataBaseType,
            string TableName,
            string[] Columns,
            string[] DataTypes,
            string[] Lenght,
            string[] NullAbles,
            Transaction HereTransaction)
        {

            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {
                    if (Columns.Length != DataTypes.Length ||
                        Columns.Length != Lenght.Length ||
                        Columns.Length != NullAbles.Length)
                    {
                        return 0;
                    }


                    string comm1 = "if exists (select ID from sys.sysobjects where id = object_id(N'[" +
                        TableName + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [" +
                        TableName + "]";

                    string comm2 = "CREATE TABLE [" + TableName + "]" + " (";
                    for (int i = 0; i < Columns.Length; i++)
                    {
                        if (Lenght[i] != null && Lenght[i] != "")
                        {
                            comm2 += "[" + Columns[i] + "]" + " " + "" +
                                DataTypes[i] + "" + "(" + Lenght[i] +
                                ")" + " " + NullAbles[i] + " ,";

                        }
                        else
                        {
                            comm2 += "[" + Columns[i] + "]" +
                                " " + "" + DataTypes[i] + "" +
                                " " + NullAbles[i] + " ,";
                        }

                    }
                    comm2 = comm2.Trim(new char[] { ',' });
                    comm2 += " )";

                    string comm = comm1 + ";" + comm2;

                    return ExcuteCommand(comm, HereTransaction);


                }
                catch (Exception ee)
                {
                    logger.Error(string.Format("[CreateTable@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                    return -1;
                }
            }
            else if (HereDataBaseType == DataBaseType.MSSQLServer2k)
            {
                return -1;
            }
            else if (HereDataBaseType == DataBaseType.Access)
            {
                return -1;
            }
            else if (HereDataBaseType == DataBaseType.Oracle)
            {
                return -1;
            }
            else
            {
                return -1;
            }
        }

        public int DropTable(DataBaseType HereDataBaseType,
            string TableName,
            IDbConnection Connection)
        {
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {

                    if (Connection.State == ConnectionState.Closed)
                    {
                        Connection.Open();
                    }

                    string comm1 = "if exists (select ID from sys.sysobjects where id = object_id(N'[" +
                        TableName + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [" +
                        TableName + "]";

                    int i= ExcuteCommand(comm1, Connection);

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

                    logger.Error(string.Format("[DropTable@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                    return -1;
                }
            }
            else if (HereDataBaseType == DataBaseType.MSSQLServer2k)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return -1;
            }
            else if (HereDataBaseType == DataBaseType.Access)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return -1;
            }
            else if (HereDataBaseType == DataBaseType.Oracle)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return -1;
            }
            else
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return -1;
            }
        }

        public int DropTable(DataBaseType HereDataBaseType,
            string TableName,
            Transaction HereTransaction)
        {
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {
                    string comm1 = "if exists (select ID from sys.sysobjects where id = object_id(N'[" +
                        TableName + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [" +
                        TableName + "]";

                    return ExcuteCommand(comm1, HereTransaction);
                }
                catch (Exception ee)
                {
                    logger.Error(string.Format("[DropTable@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                    return -1;
                }
            }
            else if (HereDataBaseType == DataBaseType.MSSQLServer2k)
            {
                return -1;
            }
            else if (HereDataBaseType == DataBaseType.Access)
            {
                return -1;
            }
            else if (HereDataBaseType == DataBaseType.Oracle)
            {
                return -1;
            }
            else
            {
                return -1;
            }
        }

        public int IsColumnExist(DataBaseType HereDataBaseType,
            string TableName,
            string ColumnName,
            IDbConnection Connection)
        {
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {
                    if (Connection.State == ConnectionState.Closed)
                    {
                        Connection.Open();
                    }
                    string comm1 = "select id from sys.syscolumns where name = '" +
                        ColumnName + "' AND id = (select ID from sys.sysobjects where ID = object_id(N'" +
                        TableName + "'))";
                    object o = ExcuteScalar(comm1, Connection);
                    if (o != null)
                    {
                        if (Connection.State != ConnectionState.Closed)
                        {
                            Connection.Close();
                        }
                        return 1;
                    }
                    else
                    {
                        if (Connection.State != ConnectionState.Closed)
                        {
                            Connection.Close();
                        }
                        return 0;
                    }
                }
                catch (Exception ee)
                {
                    if (Connection.State != ConnectionState.Closed)
                    {
                        Connection.Close();
                    }
                    logger.Error(string.Format("[IsColumnExist@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName)); 
                    return -1;
                }
            }
            else if (HereDataBaseType == DataBaseType.MSSQLServer2k)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return -1;
            }
            else if (HereDataBaseType == DataBaseType.Access)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return -1;
            }
            else if (HereDataBaseType == DataBaseType.Oracle)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return -1;
            }
            else
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
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
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {

                    if (Connection.State == ConnectionState.Closed)
                    {
                        Connection.Open();
                    }
                    string comm1 = "if exists (select id from sys.syscolumns where name = '" +
                        ColumnName +
                        "' AND id = (select ID from sys.sysobjects where ID = object_id(N'" +
                        TableName + "')))  alter table [" + TableName + "] drop column [" + ColumnName + "]";
                    string comm2 = "";
                    if (Lenght != "" && Lenght != null)
                    {
                        comm2 = "alter table [" + TableName + "] add [" + ColumnName +
                            "] " + DataType + "(" + Lenght + ") " + NullAble;
                    }
                    else
                    {
                        comm2 = "alter table [" + TableName + "] add [" +
                            ColumnName + "] " + DataType + " " + NullAble;
                    }
                    string comm = comm1 + ";" + comm2;
                    return ExcuteCommand(comm, Connection);

                }
                catch (Exception ee)
                {
                    if (Connection.State != ConnectionState.Closed)
                    {
                        Connection.Close();
                    }
                    logger.Error(string.Format("[AddColumnToTable@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                    return -1;
                }
            }
            else if (HereDataBaseType == DataBaseType.MSSQLServer2k)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return -1;
            }
            else if (HereDataBaseType == DataBaseType.Access)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return -1;
            }
            else if (HereDataBaseType == DataBaseType.Oracle)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return -1;
            }
            else
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
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
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {
                    TableName = TableName.Trim();
                    string comm1 = "if exists (select id from sys.syscolumns where name = '" +
                        ColumnName +
                        "' AND id = (select ID from sys.sysobjects where ID = object_id(N'" +
                        TableName + "')))  alter table [" + TableName + "] drop column [" + ColumnName + "]";
                    string comm2 = "";
                    if (Lenght != "" && Lenght != null)
                    {
                        comm2 = "alter table [" + TableName + "] add [" + ColumnName +
                            "] " + DataType + "(" + Lenght + ") " + NullAble;
                    }
                    else
                    {
                        comm2 = "alter table [" + TableName + "] add [" +
                            ColumnName + "] " + DataType + " " + NullAble;
                    }
  
                    string comm = comm1 + ";" + comm2 ;
                    return ExcuteCommand(comm, HereTransaction);
                }
                catch (Exception ee)
                {
                    logger.Error(string.Format("[AddColumnToTable@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                    return -1;
                }
            }
            else if (HereDataBaseType == DataBaseType.MSSQLServer2k)
            {
                return -1;
            }
            else if (HereDataBaseType == DataBaseType.Access)
            {
                return -1;
            }
            else if (HereDataBaseType == DataBaseType.Oracle)
            {
                return -1;
            }
            else
            {
                return -1;
            }
        }

        public int DropColumnFromTable(DataBaseType HereDataBaseType,
            string TableName,
            string ColumnName,
            IDbConnection Connection)
        {
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {
                    if (Connection.State == ConnectionState.Closed)
                    {
                        Connection.Open();
                    }

                    string comm1 = "if exists (select id from sys.syscolumns where name = '" +
                        ColumnName +
                        "' AND id = (select ID from sys.sysobjects where ID = object_id(N'" +
                        TableName + "')))  alter table [" +
                        TableName + "] drop column [" +
                        ColumnName + "]";

                    return ExcuteCommand(comm1, Connection);
                }
                catch (Exception ee)
                {
                    if (Connection.State != ConnectionState.Closed)
                    {
                        Connection.Close();
                    }
                    logger.Error(string.Format("[DropColumnFromTable@{0}({2})]:{1}", currentLineNumber, ee.Message));
                    return -1;
                }
            }
            else if (HereDataBaseType == DataBaseType.MSSQLServer2k)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return -1;
            }
            else if (HereDataBaseType == DataBaseType.Access)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return -1;
            }
            else if (HereDataBaseType == DataBaseType.Oracle)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return -1;
            }
            else
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return -1;
            }
        }

        public int DropColumnFromTable(DataBaseType HereDataBaseType,
            string TableName,
            string ColumnName,
            Transaction HereTransaction)
        {
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {
                    string comm1 = "if exists (select id from sys.syscolumns where name = '" +
                        ColumnName +
                        "' AND id = (select ID from sys.sysobjects where ID = object_id(N'" +
                        TableName + "')))  alter table [" +
                        TableName + "] drop column [" +
                        ColumnName + "]";

                    return ExcuteCommand(comm1, HereTransaction);
                }
                catch (Exception ee)
                {
                    logger.Error(string.Format("[DropColumnFromTable@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                    return -1;
                }
            }
            else if (HereDataBaseType == DataBaseType.MSSQLServer2k)
            {
                return -1;
            }
            else if (HereDataBaseType == DataBaseType.Access)
            {
                return -1;
            }
            else if (HereDataBaseType == DataBaseType.Oracle)
            {
                return -1;
            }
            else
            {
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
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {
                    if (Connection.State == ConnectionState.Closed)
                    {
                        Connection.Open();
                    }

                    string comm1 = "";
                    if (Lenght != "" && Lenght != null)
                    {
                        comm1 = "if exists (select id from sys.syscolumns where name = '" +
                            ColumnName +
                            "' AND id = (select ID from sys.sysobjects where ID = object_id(N'" +
                            TableName + "'))) alter table [" + TableName +
                            "] alter column [" + ColumnName +
                            "] " + DataType + " " + "(" + Lenght + ")" +
                            " " + NullAble;
                    }
                    else
                    {
                        comm1 = "if exists (select id from sys.syscolumns where name = '" +
                            ColumnName +
                            "' AND id = (select ID from sys.sysobjects where ID = object_id(N'" +
                            TableName + "'))) alter table [" + TableName +
                            "] alter column [" + ColumnName + "] " +
                            DataType + " " + NullAble;
                    }

                    return ExcuteCommand(comm1, Connection);
                }
                catch (Exception ee )
                {
                    Connection.Close();

                    logger.Error(string.Format("[ModifyColumnDataType@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                    return -1;
                }

            }
            else if (HereDataBaseType == DataBaseType.MSSQLServer2k)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return -1;
            }
            else if (HereDataBaseType == DataBaseType.Access)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return -1;
            }
            else if (HereDataBaseType == DataBaseType.Oracle)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return -1;
            }
            else
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
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
            if (HereDataBaseType == DataBaseType.MSSQLServer2k5)
            {
                try
                {
                    string comm1 = "";
                    if (Lenght != "" && Lenght != null)
                    {
                        comm1 = "if exists (select id from sys.syscolumns where name = '" +
                            ColumnName +
                            "' AND id = (select ID from sys.sysobjects where ID = object_id(N'" +
                            TableName + "'))) alter table [" + TableName +
                            "] alter column [" + ColumnName +
                            "] " + DataType + " " + "(" + Lenght + ")" +
                            " " + NullAble;
                    }
                    else
                    {
                        comm1 = "if exists (select id from sys.syscolumns where name = '" +
                            ColumnName +
                            "' AND id = (select ID from sys.sysobjects where ID = object_id(N'" +
                            TableName + "'))) alter table [" + TableName +
                            "] alter column [" + ColumnName + "] " +
                            DataType + " " + NullAble;
                    }

                    return ExcuteCommand(comm1, HereTransaction);
                }
                catch (Exception ee)
                {
                    logger.Error(string.Format("[ModifyColumnDataType@{0}({2})]:{1}", currentLineNumber, ee.Message, loginUserName));
                    return -1;
                }

            }
            else if (HereDataBaseType == DataBaseType.MSSQLServer2k)
            {
                return -1;
            }
            else if (HereDataBaseType == DataBaseType.Access)
            {
                return -1;
            }
            else if (HereDataBaseType == DataBaseType.Oracle)
            {
                return -1;
            }
            else
            {
                return -1;
            }
        }

        #endregion

        #endregion
    }
}
