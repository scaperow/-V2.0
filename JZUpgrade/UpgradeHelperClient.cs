using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Data;
using ICSharpCode.SharpZipLib.Zip;

namespace JZUpgrade
{
    public class UpgradeHelperClient
    {
        private Logger log = new Logger();
        private String conStr = "";

        public UpgradeHelperClient(Logger _log)
        {
            log = _log;
            String dataSourcePath = Path.Combine(System.Windows.Forms.Application.StartupPath, "AppConfig/Data/jzdb.mdb");
            if (File.Exists(dataSourcePath))
            {
                conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dataSourcePath + ";Jet OLEDB:Database Password=1q2w3e4R;Jet OLEDB:Engine Type=5";

            }
        }

        public DataTable GetUpgradeFiles()
        {
            if (conStr != "")
            {
                String sql = "SELECT FileName FROM sys_update WHERE FileState=0 Order by CreatedServerTime";
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
                    log.WriteLog(ex.Message, true);
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
            return null;
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
    }
}
