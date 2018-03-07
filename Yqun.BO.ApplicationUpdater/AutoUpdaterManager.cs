using System;
using System.Collections.Generic;
using System.Text;
using UpdaterCommon;
using System.Data;
using Yqun.Data.DataBase;
using System.Collections;

namespace Yqun.BO.ApplicationUpdater
{
    public class AutoUpdaterManager : BOBase
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Boolean IsNeedUpdateSoftware(DataTable LocalData)
        {
            Boolean Result = false;

            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select FileName,FileVersion from sys_updaterfiletable");

            DataTable Data = GetDataTable(sql_select.ToString());
            if (Data != null)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    String FileName = Row["FileName"].ToString();
                    String FileVersion = Row["FileVersion"].ToString();

                    DataRow[] LocalDataRows = LocalData.Select("FileName = '" + FileName + "'");
                    if (LocalDataRows.Length > 0)
                    {
                        DataRow LocalDataRow = LocalDataRows[0];
                        String localFileVersion = LocalDataRow["FileVersion"].ToString();

                        if (string.Compare(FileVersion, localFileVersion) > 0)
                        {
                            Result = true;
                            break;
                        }
                    }
                    else
                    {
                        Result = true;
                        break;
                    }
                }
            }

            return Result;
        }

        public List<UpdaterFileInfo> GetExistingFile()
        {
            List<UpdaterFileInfo> Files = new List<UpdaterFileInfo>();

            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select FileName,FileVersion,FileData from sys_updaterfiletable");

            DataTable DataTable = GetDataTable(sql_select.ToString());
            foreach (DataRow Row in DataTable.Rows)
            {
                UpdaterFileInfo File = new UpdaterFileInfo();

                File.FileName = Row["FileName"].ToString();
                File.FileVersion = Row["FileVersion"].ToString();
                File.FileData = (byte[])Row["FileData"];
                Files.Add(File);
            }
 
            return Files; 
        }

        public Boolean SaveUpdaterFile(List<UpdaterFileInfo> UpdaterFiles)
        {
            Boolean Result = false;

            IDbConnection Connection = GetConntion();
            Transaction Transaction = new Transaction(Connection);

            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select * from sys_updaterfiletable");

            DataTable Data = GetDataTable(sql_select.ToString());
            if (Data != null)
            {
                foreach (UpdaterFileInfo UpdaterFile in UpdaterFiles)
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
                object r = Update(Data, Transaction);
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

        public List<UpdaterFileInfo> GetNewUpdaterFile(List<UpdaterFileInfo> LocalUpdaterFile)
        {
            List<UpdaterFileInfo> ServerUpdaterFile = GetExistingFile();
            List<UpdaterFileInfo> NewUpdaterFile = new List<UpdaterFileInfo>();

            foreach (UpdaterFileInfo ServerFile in ServerUpdaterFile)
            {
                int index = LocalUpdaterFile.IndexOf(ServerFile);
                if (index == -1 || (index != -1 && ServerFile.CompareTo(LocalUpdaterFile[index]) == 1))
                {
                    NewUpdaterFile.Add(ServerFile);
                }
            }

            return NewUpdaterFile;
        }
    }
}
