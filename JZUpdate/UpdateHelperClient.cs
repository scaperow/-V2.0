using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Collections;
using WcfExtensions;
using TransferServiceCommon;
using System.Data;
using Yqun.Common.Encoder;
using ICSharpCode.SharpZipLib.Zip;
using ADOX;
using System.Data.OleDb;
using System.Threading;

namespace JZUpgrade
{
    public class UpdateHelperClient
    {
        //private Logger log = new Logger();
        private String updateFlag = "1";
        public string DataSource = "";
        public string DataBaseInstance = "";
        public string Username = "";
        public string PassWord = "";
        public bool ISDataBaseAttachFile = false;
        public bool DataIntegratedLogin = false;
        private String conStr = "";
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //public UpdateHelperClient(Logger log,String _updateFlag)
        //{
        //    UpdateHelperClient(_updateFlag);
        //}
        public UpdateHelperClient(String _updateFlag)
        {
            //log = _log;
            updateFlag = _updateFlag;
            String dataSourcePath = Path.Combine(System.Windows.Forms.Application.StartupPath, "AppConfig/Data/jzdb.mdb");
            conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dataSourcePath + ";Jet OLEDB:Database Password=1q2w3e4R;Jet OLEDB:Engine Type=5";

            logger.Info(dataSourcePath);

            if (!File.Exists(dataSourcePath))
            {
                if (CreateJZDB(conStr))
                {
                    CreatTabalForSXCJ(conStr);
                }
            }
            else
            {
                logger.Info("存在,不用创建数据库文件！");
            }
        }

        private Boolean CreateJZDB(String conn)
        {
            ADOX.Catalog catalog = new Catalog();
            ADOX.Table table = null;
            ADODB.Connection cn = new ADODB.Connection();
            Boolean flag = false;
            try
            {
                catalog.Create(conn);

                cn.Open(conn, null, null, -1);
                catalog.ActiveConnection = cn;
                //新建表
                table = new ADOX.Table();
                table.Name = "sys_update";
                table.ParentCatalog = catalog;
                ADOX.Column column = new ADOX.Column();
                column.ParentCatalog = catalog;
                column.Type = DataTypeEnum.adVarWChar;
                column.Name = "ID";
                table.Columns.Append(column, DataTypeEnum.adVarWChar, 36);
                //设置主键
                table.Keys.Append("PrimaryKey", KeyTypeEnum.adKeyPrimary, "ID", "", "");

                table.Columns.Append("FileName", DataTypeEnum.adVarWChar, 50);
                table.Columns.Append("FileType", DataTypeEnum.adSmallInt, 9);
                table.Columns.Append("CreatedServerTime", DataTypeEnum.adVarWChar, 50);
                table.Columns.Append("FileState", DataTypeEnum.adSmallInt, 9);

                catalog.Tables.Append(table);

                flag = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            finally
            {
                table = null;
                catalog = null;
                cn.Close();
            }
            return flag;
        }

        /// <summary>
        /// 采集系统创建表
        /// </summary>
        /// <returns></returns>
        public bool CreatTabalForSXCJ(string _ConnnectionStr)
        {
            try
            {
                List<ColumnStruct> _ListColumnStruct = null;
                ADOX.Catalog _Catalog = new Catalog();
                ADODB.Connection _Connection = new ADODB.Connection();
                _Connection.Open(_ConnnectionStr, null, null, -1);
                _Catalog.ActiveConnection = _Connection;

                if (_ListColumnStruct == null)
                {
                    _ListColumnStruct = new List<ColumnStruct>();
                }
                _ListColumnStruct.Add(new ColumnStruct("UserID", DataTypeEnum.adVarWChar, 36, true));
                _ListColumnStruct.Add(new ColumnStruct("UserName", DataTypeEnum.adVarWChar, 20, false));
                _ListColumnStruct.Add(new ColumnStruct("UserPwd", DataTypeEnum.adVarWChar, 50, false));
                _ListColumnStruct.Add(new ColumnStruct("UserCode", DataTypeEnum.adVarWChar, 50, false));
                _ListColumnStruct.Add(new ColumnStruct("UserTestCode", DataTypeEnum.adVarWChar, 50, false));
                if (CreateTable("UserInfo", _ListColumnStruct, _Catalog))
                {
                    _ListColumnStruct.Clear();
                }

                if (_ListColumnStruct == null)
                {
                    _ListColumnStruct = new List<ColumnStruct>();
                }
                _ListColumnStruct.Add(new ColumnStruct("ID", DataTypeEnum.adVarWChar, 36, true));
                _ListColumnStruct.Add(new ColumnStruct("ModuleID", DataTypeEnum.adVarWChar, 36, false));
                _ListColumnStruct.Add(new ColumnStruct("ModuleName", DataTypeEnum.adVarWChar, 200, false));
                _ListColumnStruct.Add(new ColumnStruct("DeviceType", DataTypeEnum.adSmallInt, 9, false));
                _ListColumnStruct.Add(new ColumnStruct("SerialNumber", DataTypeEnum.adSmallInt, 9, false));
                _ListColumnStruct.Add(new ColumnStruct("Config", DataTypeEnum.adLongVarWChar, 1000, false));
                if (CreateTable("sys_module_config", _ListColumnStruct, _Catalog))
                {
                    _ListColumnStruct.Clear();
                }

                if (_ListColumnStruct == null)
                {
                    _ListColumnStruct = new List<ColumnStruct>();
                }
                _ListColumnStruct.Add(new ColumnStruct("ID", DataTypeEnum.adVarWChar, 36, true));
                _ListColumnStruct.Add(new ColumnStruct("DataID", DataTypeEnum.adVarWChar, 36, false));
                _ListColumnStruct.Add(new ColumnStruct("StadiumID", DataTypeEnum.adVarWChar, 36, false));
                _ListColumnStruct.Add(new ColumnStruct("ModuleID", DataTypeEnum.adVarWChar, 36, false));
                _ListColumnStruct.Add(new ColumnStruct("WTBH", DataTypeEnum.adVarWChar, 50, false));
                _ListColumnStruct.Add(new ColumnStruct("TestRoomCode", DataTypeEnum.adVarWChar, 50, false));
                _ListColumnStruct.Add(new ColumnStruct("SerialNumber", DataTypeEnum.adSmallInt, 9, false));
                _ListColumnStruct.Add(new ColumnStruct("UserName", DataTypeEnum.adVarWChar, 100, false));
                _ListColumnStruct.Add(new ColumnStruct("CreatedTime", DataTypeEnum.adVarWChar, 50, false));
                _ListColumnStruct.Add(new ColumnStruct("TestData", DataTypeEnum.adLongVarWChar, 8000, false));
                _ListColumnStruct.Add(new ColumnStruct("RealTimeDate", DataTypeEnum.adLongVarWChar, 8000, false));
                _ListColumnStruct.Add(new ColumnStruct("Status", DataTypeEnum.adSmallInt, 9, false));
                _ListColumnStruct.Add(new ColumnStruct("TotalNumber", DataTypeEnum.adSmallInt, 9, false));
                _ListColumnStruct.Add(new ColumnStruct("MachineCode", DataTypeEnum.adVarWChar, 50, false));
                _ListColumnStruct.Add(new ColumnStruct("EquipmentCode", DataTypeEnum.adVarWChar, 50, false));
                _ListColumnStruct.Add(new ColumnStruct("UploadInfo", DataTypeEnum.adLongVarWChar, 8000, false));
                _ListColumnStruct.Add(new ColumnStruct("UploadCode", DataTypeEnum.adVarWChar, 50, false));
                _ListColumnStruct.Add(new ColumnStruct("UploadTDB", DataTypeEnum.adSmallInt, 4, false));
                _ListColumnStruct.Add(new ColumnStruct("UploadEMC", DataTypeEnum.adSmallInt, 4, false));
                if (CreateTable("sys_test_data", _ListColumnStruct, _Catalog))
                {
                    _ListColumnStruct.Clear();
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return false;
        }

        /// <summary>
        /// 判断表是否存在
        /// </summary>
        /// <param name="_TableName">表名</param>
        /// <param name="_Catalog">数据库对象</param>
        /// <param name="_Connection">数据库连接信息</param>
        /// <returns>False存在；True不存在</returns>
        private bool TableExist(string _TableName, ADOX.Catalog _Catalog, ADODB.Connection _Connection)
        {
            if (_Catalog.Tables.Count > 0)
            {
                for (int i = 0; i < _Catalog.Tables.Count; i++)
                {
                    if (_Catalog.Tables[i].Name == _TableName)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        /// <summary>
        /// 创建表方法
        /// </summary>
        /// <param name="_TableName">表明</param>
        /// <param name="_Hashtable">列名和数据类型</param>
        /// <param name="ADOX.Catalog">主键位置</param>
        /// <returns>False：创建失败；True创建失败</returns>
        private bool CreateTable(string _TableName, List<ColumnStruct> _ListColumnStruct, ADOX.Catalog _Catalog)
        {
            ADOX.Table _Table = null;
            try
            {
                _Table = new ADOX.Table();
                _Table.Name = _TableName;
                for (int i = 0; i < _ListColumnStruct.Count; i++)
                {
                    _Table.Columns.Append(_ListColumnStruct[i]._ColumnName, _ListColumnStruct[i]._DataTypeEnum, _ListColumnStruct[i]._ColumnLong);
                    if (_ListColumnStruct[i]._IsKeyPrimary)
                    {
                        _Table.Keys.Append("PrimaryKey", KeyTypeEnum.adKeyPrimary, _ListColumnStruct[i]._ColumnName, "", "");
                    }
                }
                _Catalog.Tables.Append(_Table);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            finally
            {
                _Table = null;
            }
            return false;
        }



        public String GetConnectionString()
        {
            return conStr;
        }

        public DateTime GetMaxUpdateTime()
        {
            String conStr = GetConnectionString();
            OleDbConnection con = new OleDbConnection(conStr);
            String sql = "SELECT TOP 1 CreatedServerTime FROM sys_update ORDER BY CreatedServerTime DESC";
            DateTime dtime = new DateTime(2999, 1, 1);
            DataTable dt = new DataTable();
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                OleDbDataAdapter myDp = new OleDbDataAdapter(sql, con);
                myDp.Fill(dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    dtime = System.Convert.ToDateTime(dt.Rows[0][0]);
                }
                else
                {
                    dtime = new DateTime(1999, 1, 1);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            finally
            {
                if (con.State != System.Data.ConnectionState.Closed)
                {
                    con.Close();
                }
            }

            return dtime;
        }

        public void SaveUpdateInfo(String id, String fileName, String fileType, String createdServerTime)
        {
            String sql = String.Format(@"INSERT INTO sys_update
                                                    ( ID ,
                                                      FileName ,
                                                      FileType ,
                                                      CreatedServerTime ,
                                                      FileState
                                                    )
                                            VALUES  ( '{0}','{1}',{2},'{3}',0)",
                                    id, fileName, fileType, createdServerTime);
            String conStr = GetConnectionString();
            OleDbConnection con = new OleDbConnection(conStr);
            OleDbCommand cmd = new OleDbCommand(sql, con);
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            finally
            {
                if (con.State != System.Data.ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

        public void FinishUpdate(String id, Int32 fileState)
        {
            String sql = String.Format(@"UPDATE sys_update set FileState={1} where ID='{0}' ", id, fileState);
            String conStr = GetConnectionString();
            OleDbConnection con = new OleDbConnection(conStr);
            OleDbCommand cmd = new OleDbCommand(sql, con);
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            finally
            {
                if (con.State != System.Data.ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

        public DataTable GetNewUpdate()
        {
            DateTime maxTime = GetMaxUpdateTime();
            if (updateFlag == "3")
            {
                DataTable dt = GetLocalDBTimeList();
                CallRemoteService("Yqun.BO.BusinessManager.dll", "GenerateDBUpdate", new object[] { dt });
            }
            return CallRemoteService("Yqun.BO.BusinessManager.dll", "GetNewUpdate", new object[] { maxTime, updateFlag }) as DataTable;
        }

        public Byte[] GetUpdateFileByID(Guid id)
        {
            return CallRemoteService("Yqun.BO.BusinessManager.dll", "GetUpdateFileByID", new object[] { id }) as Byte[];
        }

        private DataTable GetLocalDBTimeList()
        {
            String sql = String.Format("SELECT * FROM sys_update_catch"
                   , updateFlag);
            String conStr = GetConnectionString();
            OleDbConnection con = new OleDbConnection(conStr);

            DataTable dt = new DataTable();
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                OleDbDataAdapter myDp = new OleDbDataAdapter(sql, con);
                myDp.Fill(dt);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            finally
            {
                if (con.State != System.Data.ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            return dt;
        }

        public DataTable GetUnUsedUpdateInfo()
        {
            String sql = String.Format("SELECT * FROM sys_update WHERE FileType={0} and FileState=0 Order by CreatedServerTime"
                   , updateFlag);
            String conStr = GetConnectionString();
            OleDbConnection con = new OleDbConnection(conStr);

            DataTable dt = new DataTable();
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                OleDbDataAdapter myDp = new OleDbDataAdapter(sql, con);
                myDp.Fill(dt);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            finally
            {
                if (con.State != System.Data.ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            return dt;
        }

        public String GetSubFolder(String fileType)
        {
            String str = "";
            if (fileType == "1")
            {
                str = "GL";
            }
            else if (fileType == "2")
            {
                str = "CJ";
            }
            else if (fileType == "9")
            {
                str = "DY";
            }
            else if (fileType == "10")
            {
                str = "LD";
            }
            else
            {
                str = "DB";
            }
            return str;
        }

        public object CallRemoteService(string AssemblyName, string MethodName, object[] Parameters)
        {
            try
            {
                var configFile = AppDomain.CurrentDomain.BaseDirectory + "\\" + ApplicationHelper.GetApplicationName(updateFlag) + ".exe.config";
                configFile = System.IO.Path.GetFullPath(configFile);
                logger.Error(configFile);
                var proxy = WcfClientHelper.GetProxy<ITransferService>(configFile);
                System.Collections.Hashtable hashtable = new System.Collections.Hashtable();
                hashtable["assembly_name"] = AssemblyName;
                hashtable["method_name"] = MethodName;
                hashtable["method_paremeters"] = Parameters;
                System.IO.Stream source_stream = Serialize.SerializeToStream(hashtable);
                System.IO.Stream zip_stream = Compression.CompressStream(source_stream); source_stream.Dispose();
                System.IO.Stream stream_result = proxy.InvokeMethod(zip_stream); zip_stream.Dispose();
                System.IO.Stream ms = ReadMemoryStream(stream_result); stream_result.Dispose();
                System.IO.Stream unzip_stream = Compression.DeCompressStream(ms); ms.Dispose();
                System.Collections.Hashtable Result = Serialize.DeSerializeFromStream(unzip_stream) as System.Collections.Hashtable;
                return Result["return_value"];
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 从输入流中读出内存流
        /// </summary>
        /// <param name="Params"></param>
        /// <returns></returns>
        private System.IO.MemoryStream ReadMemoryStream(System.IO.Stream Params)
        {
            System.IO.MemoryStream serviceStream = new System.IO.MemoryStream();
            byte[] buffer = new byte[4096 * 4096];
            int bytesRead = 0;
            int byteCount = 0;

            do
            {
                bytesRead = Params.Read(buffer, 0, buffer.Length);
                serviceStream.Write(buffer, 0, bytesRead);

                byteCount = byteCount + bytesRead;
            } while (bytesRead > 0);

            serviceStream.Position = 0;

            return serviceStream;
        }

        public Boolean UnZipFile(string zipFilePath)
        {
            if (!File.Exists(zipFilePath))
            {
                return false;
            }
            Boolean flag = true;
            try
            {
                using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipFilePath)))
                {
                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        string directoryName = Path.GetDirectoryName(zipFilePath);
                        string fileName = Path.GetFileName(theEntry.Name);

                        if (directoryName.Length > 0 && !Directory.Exists(directoryName))
                        {
                            Directory.CreateDirectory(directoryName);
                        }

                        if (fileName != String.Empty)
                        {
                            using (FileStream streamWriter = File.Create(Path.Combine(directoryName, fileName)))
                            {

                                int size = 2048;
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    size = s.Read(data, 0, data.Length);
                                    if (size > 0)
                                    {
                                        streamWriter.Write(data, 0, size);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public void RunSqlCommand(String sql)
        {
            String conStr = GetConnectionString();
            OleDbConnection con = new OleDbConnection(conStr);

            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            finally
            {
                if (con.State != System.Data.ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

        public bool IsNewest()
        {
            bool IsNewest = false;
            DataTable dtNewUpdate = new DataTable();
            DataTable dtUnUsedUpdate = new DataTable(); ;
            //1 管理系统文件+不执行，
            //2 采集系统文件+不执行，
            //3 管理系统数据+不执行，
            //4 管理系统文件+数据+执行； 
            //5 采集系统文件+执行
            //6 管理系统执行
            //7 采集系统执行
            //8 管理系统文件+执行
            //9 电液伺服更新+执行
            //10 领导版管理+执行
            if (updateFlag == "1" || updateFlag == "2" || updateFlag == "3")
            {
                dtNewUpdate = GetNewUpdate();
                dtUnUsedUpdate = GetUnUsedUpdateInfo();
            }
            else if (updateFlag == "4")
            {
                updateFlag = "1";
                dtNewUpdate = GetNewUpdate();
                dtUnUsedUpdate = GetUnUsedUpdateInfo();

                updateFlag = "3";
                dtNewUpdate = GetNewUpdate();
                dtUnUsedUpdate = GetUnUsedUpdateInfo();
            }
            else if (updateFlag == "5")
            {
                updateFlag = "2";
                dtNewUpdate = GetNewUpdate();
                dtUnUsedUpdate = GetUnUsedUpdateInfo();
            }
            else if (updateFlag == "6")
            {
                updateFlag = "1";
                dtNewUpdate = GetNewUpdate();
                dtUnUsedUpdate = GetUnUsedUpdateInfo();

                updateFlag = "3";
                dtNewUpdate = GetNewUpdate();
                dtUnUsedUpdate = GetUnUsedUpdateInfo();
            }
            else if (updateFlag == "7")
            {
                updateFlag = "2";
                dtNewUpdate = GetNewUpdate();
                dtUnUsedUpdate = GetUnUsedUpdateInfo();
            }
            else if (updateFlag == "8")
            {
                updateFlag = "1";
                dtNewUpdate = GetNewUpdate();
                dtUnUsedUpdate = GetUnUsedUpdateInfo();
            }
            else if (updateFlag == "9")
            {
                updateFlag = "9";
                dtNewUpdate = GetNewUpdate();
                dtUnUsedUpdate = GetUnUsedUpdateInfo();
            }
            else if (updateFlag == "10")
            {
                updateFlag = "10";
                dtNewUpdate = GetNewUpdate();
                dtUnUsedUpdate = GetUnUsedUpdateInfo();
            }
            if (dtNewUpdate != null && dtUnUsedUpdate != null)
            {
                if (dtNewUpdate.Rows.Count > 0 || dtUnUsedUpdate.Rows.Count > 0)
                {
                    IsNewest = false;
                }
                else
                {
                    IsNewest = true;
                }
            }
            else
            {
                IsNewest = true;
            }

            return IsNewest;
        }

        public string GetUploadFlag()
        {
            string strUploadFlag = updateFlag;
            //1 管理系统文件+不执行，
            //2 采集系统文件+不执行，
            //3 管理系统数据+不执行，
            //4 管理系统文件+数据+执行； 
            //5 采集系统文件+执行
            //6 管理系统执行
            //7 采集系统执行
            //8 管理系统文件+执行
            //9 电液伺服更新+执行
            //10 领导版管理+执行
            if (updateFlag == "1" || updateFlag == "2" || updateFlag == "3")
            {
            }
            else if (updateFlag == "4")
            {
                strUploadFlag = "1";
            }
            else if (updateFlag == "5")
            {
                strUploadFlag = "2";
            }
            else if (updateFlag == "6")
            {
                strUploadFlag = "1";
            }
            else if (updateFlag == "7")
            {
                strUploadFlag = "2";
            }
            else if (updateFlag == "8")
            {
                strUploadFlag = "1";
            }
            else if (updateFlag == "9")
            {
                strUploadFlag = "9";
            }
            else if (updateFlag == "10")
            {
                strUploadFlag = "10";
            }
            return strUploadFlag;
        }
    }

    /// <summary>
    /// 创建列对象
    /// </summary>
    public class ColumnStruct
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string _ColumnName;
        /// <summary>
        /// 数据类型
        /// </summary>
        public DataTypeEnum _DataTypeEnum;
        /// <summary>
        /// 数据长度
        /// </summary>
        public int _ColumnLong;
        /// <summary>
        /// 是否主键，True主键；False非主键
        /// </summary>
        public bool _IsKeyPrimary;

        /// <summary>
        /// 初始化值
        /// </summary>
        /// <param name="_ColumnName"></param>
        /// <param name="_DataTypeEnum"></param>
        /// <param name="_ColumnLong"></param>
        /// <param name="_IsKeyPrimary"></param>
        public ColumnStruct(string _ColumnName, DataTypeEnum _DataTypeEnum, int _ColumnLong, bool _IsKeyPrimary)
        {
            this._ColumnName = _ColumnName;
            this._DataTypeEnum = _DataTypeEnum;
            this._ColumnLong = _ColumnLong;
            this._IsKeyPrimary = _IsKeyPrimary;
        }
    }
}
