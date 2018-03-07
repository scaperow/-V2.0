using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ADOX;
using System.Data.OleDb;
using System.Data;
using Yqun.Services;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;
using System.Diagnostics;
using System.Threading;

namespace BizUpgrade
{
    public delegate void UpdateProgressHandler(String title, Int32 step, Int32 max);
    public delegate void CloseHandler();

    public class UpgradeHelper
    {
        private Logger log = null;
        private AutoResetEvent resetEvent = new AutoResetEvent(false);
        private UpgradeBar ub = new UpgradeBar();
        private DataTable upgradeTable = null;
        private Boolean hasUpgrade = false;

        public UpgradeHelper()
        {
            log = new Logger();
            log.Logfolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log");
            log.IsUseLog = true;
            log.QueueBufferSize = 5;
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
                log.WriteLog(ex.Message, true);
            }
            finally
            {
                table = null;
                catalog = null;
                cn.Close();
            }
            return flag;
        }

        private DateTime GetMaxUpdateTime()
        {
            String dataSourcePath = Path.Combine(System.Windows.Forms.Application.StartupPath, "AppConfig/Data/jzdb.mdb");
            String conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dataSourcePath + ";Jet OLEDB:Database Password=1q2w3e4R;Jet OLEDB:Engine Type=5";

            if (!File.Exists(dataSourcePath))
            {
                if (CreateJZDB(conStr))
                {
                    CreatTabalForSXCJ(conStr);
                }
            }
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
                log.WriteLog(ex.Message, true);
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

        /// <summary>
        /// 获取更新文件，并记录本地数据库
        /// </summary>
        /// <param name="upgradeFlag">1表示管理，2表示数显，3表示电液伺服，4表示管理更新，5表示数显采集更新，6表示电液伺服采集更新</param>
        /// <returns></returns>
        public Boolean HasUpgradeFiles(Int32 upgradeFlag)
        {
            try
            {
                DateTime maxTime = GetMaxUpdateTime();
                upgradeTable = Agent.CallService("Yqun.BO.BusinessManager.dll", "GetNewUpdate", new object[] { maxTime, upgradeFlag.ToString() }) as DataTable;
                if (upgradeTable != null && upgradeTable.Rows.Count > 0)
                {
                    Thread t1 = new Thread(new ThreadStart(Run));
                    t1.Start();
                    ub.Show();

                    resetEvent.WaitOne();
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message, true);
            }
            return hasUpgrade;
        }

        private void Run()
        {
            try
            {
                String path = Path.Combine(Application.StartupPath, "update");
                Int32 max = upgradeTable.Rows.Count;
                UpdateProgressBar("系统开始更新...", 0, max);
                for (int i = 0; i < upgradeTable.Rows.Count; i++)
                {
                    Int32 fileState = 1;
                    Int32 fileType = 0;
                    try
                    {
                        fileType = Int32.Parse(upgradeTable.Rows[i]["FileType"].ToString());
                        String fn = upgradeTable.Rows[i]["FileName"].ToString();
                        UpdateProgressBar("正在获取更新文件：" + fn, i + 1, max);
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        String file = Path.Combine(path, fn);

                        byte[] bytes = GetUpdateFileByID(new Guid(upgradeTable.Rows[i]["ID"].ToString()));
                        if (bytes != null)
                        {
                            File.WriteAllBytes(file, bytes);
                            if (fileType == 1 || fileType == 2 || fileType == 3)
                            {
                                fileState = 0;
                                hasUpgrade = true;
                            }
                            else if (fileType == 4 || fileType == 5 || fileType == 6)
                            {
                                Boolean success = UnZipFile(file);
                                if (success)
                                {
                                    String[] files = Directory.GetFiles(path);
                                    foreach (String fileName in files)
                                    {
                                        ProcessUpdateFile(fileName, Application.StartupPath);
                                    }
                                    File.Delete(file);
                                }
                                fileState = 1;
                            }
                            else
                            {
                                fileState = -1;
                            }
                        }
                    }
                    catch (Exception exx)
                    {
                        log.WriteLog(exx.Message, true);
                        fileState = -1;
                    }
                    SaveUpdateInfo(upgradeTable.Rows[i]["ID"].ToString(), upgradeTable.Rows[i]["FileName"].ToString(), fileType,
                                Convert.ToDateTime(upgradeTable.Rows[i]["CreatedServerTime"]).ToString("yyyy-MM-dd HH:mm:ss.fff"), fileState);
                }
                UpdateProgressBar("更新完成...", max, max);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message, true);
            }
            finally
            {
                resetEvent.Set();
                Thread.Sleep(300);
                CloseProgressBar();
            }
        }

        private void ProcessUpdateFile(String file, String path)
        {
            String fileName = Path.GetFileName(file).ToLower().Trim();
            if (fileName.EndsWith(".zip"))
            {
                return;
            }
            else if (fileName.EndsWith(".bat"))
            {
                Process p = new Process();
                ProcessStartInfo pi = new ProcessStartInfo(file);
                pi.UseShellExecute = false;
                pi.CreateNoWindow = true;
                pi.RedirectStandardOutput = true;
                p.StartInfo = pi;
                p.Start();
                p.WaitForExit();
                p.Close();
            }
            else if (fileName.EndsWith(".sql"))
            {
                String sql = "";
                using (StreamReader sr = new StreamReader(file))
                {
                    sql = sr.ReadToEnd();
                }
                RunSqlCommand(sql);
            }
            else
            {
                if (File.Exists(Path.Combine(path, fileName)))
                {
                    File.Delete(Path.Combine(path, fileName));
                }
                File.Move(file, Path.Combine(path, fileName));
            }
            File.Delete(file);

        }

        private Byte[] GetUpdateFileByID(Guid id)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetUpdateFileByID", new object[] { id }) as Byte[];
        }

        private void RunSqlCommand(String sql)
        {
            String dataSourcePath = Path.Combine(System.Windows.Forms.Application.StartupPath, "AppConfig/Data/jzdb.mdb");
            String conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dataSourcePath + ";Jet OLEDB:Database Password=1q2w3e4R;Jet OLEDB:Engine Type=5";

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
                log.WriteLog(ex.Message, true);
            }
            finally
            {
                if (con.State != System.Data.ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

        private void SaveUpdateInfo(String id, String fileName, Int32 fileType, String createdServerTime, Int32 fileState)
        {
            String sql = String.Format(@"INSERT INTO sys_update
                                                    ( ID ,
                                                      FileName ,
                                                      FileType ,
                                                      CreatedServerTime ,
                                                      FileState
                                                    )
                                            VALUES  ( '{0}','{1}',{2},'{3}',{4})",
                                    id, fileName, fileType, createdServerTime, fileState);
            String dataSourcePath = Path.Combine(System.Windows.Forms.Application.StartupPath, "AppConfig/Data/jzdb.mdb");
            String conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dataSourcePath + ";Jet OLEDB:Database Password=1q2w3e4R;Jet OLEDB:Engine Type=5";

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
                log.WriteLog(ex.Message, true);
            }
            finally
            {
                if (con.State != System.Data.ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

        private Boolean UnZipFile(string zipFilePath)
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
            catch(Exception exxx)
            {
                log.WriteLog(exxx.Message, true);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// 采集系统创建表
        /// </summary>
        /// <returns></returns>
        private bool CreatTabalForSXCJ(string _ConnnectionStr)
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
                if (CreateTable("sys_test_data", _ListColumnStruct, _Catalog))
                {
                    _ListColumnStruct.Clear();
                }
                return true;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message, true);
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
                log.WriteLog(ex.Message, true);
            }
            finally
            {
                _Table = null;
            }
            return false;
        }

        public void UpdateProgressBar(String title, Int32 step, Int32 max)
        {
            if (ub.InvokeRequired)
            {
                UpdateProgressHandler ssvh = new UpdateProgressHandler(UpdateProgressBar);
                ub.BeginInvoke(ssvh, title, step, max);
            }
            else
            {
                ub.SetProgressBar(title, step, max);
            }
            Thread.Sleep(600);
        }

        public void CloseProgressBar()
        {
            if (ub.InvokeRequired)
            {
                CloseHandler ch = new CloseHandler(CloseProgressBar);
                ub.BeginInvoke(ch);
            }
            else
            {
                ub.Close();
            }
        }

    }
}
