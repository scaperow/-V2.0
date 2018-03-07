using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using System.Data;
using System.Collections;
using UpdaterCommon;
using System.Windows.Forms;

namespace UpdaterComponents
{
    public class LocalFileVersionManager
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static Boolean IsNeedUpdateSoftware()
        {
            String AppConfig = Path.Combine(Application.StartupPath, "AppConfig");
            String DataSource = Path.Combine(AppConfig, "DataUpdater.dat");
            SimpleDataService DataService = new SimpleDataService(DataSource, 1024);

            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select FileName,FileVersion from sys_updaterfiletable");

            DataTable Data = DataService.GetDataTable(sql_select.ToString());
            if (Data == null)
            {
                logger.Error(string.Format("文件 {0} 已损坏，请联系管理员！"));
                throw new Exception(string.Format("文件 {0} 已损坏，请联系管理员！"));
            }

            Boolean r = Convert.ToBoolean(ServerFileVersionManager.CallRemoteService("Yqun.BO.ApplicationUpdater.dll", "IsNeedUpdateSoftware", new object[] { Data }));
            logger.Error(r ? "" : "");
            return r;
        }

        public static List<UpdaterFileInfo> GetLocalOldFile()
        {
            List<UpdaterFileInfo> oldFilesInfo = new List<UpdaterFileInfo>();

            String AppConfig = Path.Combine(Application.StartupPath, "AppConfig");
            String DataSource = Path.Combine(AppConfig, "DataUpdater.dat");
            SimpleDataService DataService = new SimpleDataService(DataSource, 1024);

            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select FileName,FileVersion from sys_updaterfiletable");

            DataTable Data = DataService.GetDataTable(sql_select.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    UpdaterFileInfo oldFileInfo = new UpdaterFileInfo();
                    oldFileInfo.FileName = Row["FileName"].ToString();
                    oldFileInfo.FileVersion = Row["FileVersion"].ToString();
                    oldFilesInfo.Add(oldFileInfo);
                }
            }

            return oldFilesInfo;
        }

        public static Boolean SaveLocalFile(List<UpdaterFileInfo> UpdaterFilesInfo)
        {
            Boolean Result = false;

            String AppConfig = Path.Combine(Application.StartupPath, "AppConfig");
            String DataSource = Path.Combine(AppConfig, "DataUpdater.dat");
            SimpleDataService DataService = new SimpleDataService(DataSource, 1024);

            IDbConnection Connection = DataService.GetConnection();
            Transaction Transaction = new Transaction(Connection);

            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select * from sys_updaterfiletable");

            DataTable Data = DataService.GetDataTable(sql_select.ToString());
            if (Data != null)
            {
                foreach (UpdaterFileInfo UpdaterFile in UpdaterFilesInfo)
                {
                    DataRow DataRow;
                    DataRow[] DataRows = Data.Select("FileName='" + UpdaterFile.FileName + "'");
                    if (DataRows.Length > 0)
                    {
                        DataRow = DataRows[0];

                        DataRow["FileName"] = UpdaterFile.FileName;
                        DataRow["FileData"] = UpdaterFile.FileData;
                        DataRow["FileVersion"] = UpdaterFile.FileVersion;
                    }
                    else
                    {
                        DataRow = Data.NewRow();
                        DataRow["ID"] = Guid.NewGuid().ToString();
                        DataRow["FileName"] = UpdaterFile.FileName;
                        DataRow["FileData"] = UpdaterFile.FileData;
                        DataRow["FileVersion"] = UpdaterFile.FileVersion;
                        Data.Rows.Add(DataRow);
                    }
                }
            }

            try
            {
                object r = DataService.UpdateCommand(Data, Transaction);
                Result = (Convert.ToInt32(r) == 1);

                if (Result)
                {
                    Transaction.Commit();
                }
                else
                {
                    Transaction.Rollback();
                }
            }
            catch
            {
                Transaction.Rollback();
            }

            return Result;
        }
    }
}
