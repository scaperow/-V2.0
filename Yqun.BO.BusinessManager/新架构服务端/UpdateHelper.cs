using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using System.IO;
using System.Data;
using System.ServiceModel;
using TransferServiceCommon;
using System.Collections;

namespace Yqun.BO.BusinessManager
{
    public class UpdateHelper : BOBase
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private object CallRemoteServerMethod(String address, string AssemblyName, string MethodName, object[] Parameters)
        {
            object obj = null;
            try
            {
                using (ChannelFactory<ITransferService> channelFactory = new ChannelFactory<ITransferService>("sClient", new EndpointAddress(address)))
                {

                    ITransferService proxy = channelFactory.CreateChannel();
                    using (proxy as IDisposable)
                    {
                        Hashtable hashtable = new Hashtable();
                        hashtable["assembly_name"] = AssemblyName;
                        hashtable["method_name"] = MethodName;
                        hashtable["method_paremeters"] = Parameters;

                        Stream source_stream = Yqun.Common.Encoder.Serialize.SerializeToStream(hashtable);
                        Stream zip_stream = Yqun.Common.Encoder.Compression.CompressStream(source_stream);
                        source_stream.Dispose();
                        Stream stream_result = proxy.InvokeMethod(zip_stream);
                        zip_stream.Dispose();
                        Stream ms = ReadMemoryStream(stream_result);
                        stream_result.Dispose();
                        Stream unzip_stream = Yqun.Common.Encoder.Compression.DeCompressStream(ms);
                        ms.Dispose();
                        Hashtable Result = Yqun.Common.Encoder.Serialize.DeSerializeFromStream(unzip_stream) as Hashtable;

                        obj = Result["return_value"];
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("call remote server method error: " + ex.Message);
            }
            return obj;
        }

        private MemoryStream ReadMemoryStream(Stream Params)
        {
            MemoryStream serviceStream = new MemoryStream();
            byte[] buffer = new byte[10000];
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

        public Boolean UploadUpdateFile(JZFile f)
        {
            Boolean result = false;
            if (f != null && f.FileData != null)
            {
                try
                {
                    String subFolder = GetSubFolder(f.FileType);
                    String path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~"), "update\\" + subFolder);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    String fName = Path.Combine(path, f.FileName);
                    File.WriteAllBytes(fName, f.FileData);
                    ExcuteCommand("UPDATE dbo.sys_update SET FileState=0 WHERE FileType=" + f.FileType); 
                    String sql = String.Format(@"INSERT INTO dbo.sys_update
                                                    ( ID ,
                                                      FileName ,
                                                      FileType ,
                                                      CreatedServerTime ,
                                                      FileState
                                                    )
                                            VALUES  ( '{0}','{1}',{2},GETDATE(),1)", Guid.NewGuid(), f.FileName, f.FileType);
                    ExcuteCommand(sql);
                    result = true;
                }
                catch (Exception ex)
                {
                    logger.Error("上传更新包失败：" + ex.Message);
                    result = false;
                }

            }
            return result;
        }

        public Boolean UploadFileByLineID(JZFile f, String lineID)
        {
            Boolean result = true;
            if (f != null && f.FileData != null)
            {
                try
                {
                    String sql = "SELECT ID,LineName,Description,IPAddress,Port FROM dbo.sys_line WHERE ID='" + lineID + "'";
                    DataTable dt = GetDataTable(sql);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Boolean flag = true;
                        String lineAddress = "net.tcp://" + dt.Rows[i]["IPAddress"] + ":" + dt.Rows[i]["Port"] + "/TransferService.svc";
                        object obj = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "UploadUpdateFile",
                            new Object[] { f});
                        flag = Convert.ToBoolean(obj);
                        result = result & flag;
                    }
                    
                }
                catch (Exception ex)
                {
                    logger.Error("上传更新包失败：" + ex.Message + "; lineID=" + lineID);
                    result = false;
                }

            }
            return result;
        }

        public Byte[] GetUpdateFileByID(Guid id)
        {
            String sql = String.Format("SELECT * FROM dbo.sys_update WHERE ID='{0}'", id);
            DataTable dt = GetDataTable(sql);
            
            Byte[] buffer = null;
            if (dt != null && dt.Rows.Count == 1)
            {
                String subFolder = GetSubFolder(dt.Rows[0]["FileType"].ToString());
                String path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~"), "update\\" + subFolder);
                String file = Path.Combine(path, dt.Rows[0]["FileName"].ToString());
                if (File.Exists(file))
                {
                    FileStream stream = null;

                    stream = new FileInfo(file).OpenRead();
                    buffer = new Byte[stream.Length];
                    stream.Read(buffer, 0, Convert.ToInt32(stream.Length));
                    stream.Close();
                }
            }
            return buffer;
        }

        public DataTable GetNewUpdate(DateTime maxTime, String updateFlag)
        {
            String sql = String.Format("SELECT ID,FileName,FileType,FileState,convert(varchar,CreatedServerTime, 121) CreatedServerTime  FROM dbo.sys_update WHERE FileType={0} AND CreatedServerTime>'{1}' AND FileState>0 Order by CreatedServerTime"
                    , updateFlag, maxTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            return GetDataTable(sql);
        }

        public DateTime GetMaxUpdateTime()
        {
            String sql = "SELECT MAX(CreatedServerTime) FROM dbo.sys_update";
            Object obj = ExcuteScalar(sql);
            DateTime dt = new DateTime(1999, 1, 1);
            if (obj != null && obj != DBNull.Value)
            {
                dt = System.Convert.ToDateTime(obj);
            }
            return dt;
        }

        public void SaveUpdateInfo(String id, String fileName, String fileType, String createdServerTime)
        {
            String sql = String.Format(@"INSERT INTO dbo.sys_update
                                                    ( ID ,
                                                      FileName ,
                                                      FileType ,
                                                      CreatedServerTime ,
                                                      FileState
                                                    )
                                            VALUES  ( '{0}','{1}',{2},'{3}',1)",
                                    id, fileName, fileType, createdServerTime);
            ExcuteCommand(sql);
        }

        public void FinishUpdate(String id)
        {
            String sql = String.Format(@"UPDATE dbo.sys_update set FileState=1 where ID='{0}' ", id);
            ExcuteCommand(sql);
        }

        public void GenerateDBUpdate(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                List<String> newTB = new List<string>();
                newTB.Add("dbo.sys_formulas");
                newTB.Add("dbo.sys_line_formulas");
                newTB.Add("dbo.sys_module");
                newTB.Add("dbo.sys_sheet");
                newTB.Add("dbo.sys_stadium_config");
                Boolean hasNewUpdate = false;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    String tbName = dt.Rows[i]["TableName"].ToString().ToLower();
                    String sql = String.Format("SELECT COUNT(1) FROM {0} WHERE Scts_1>'{1}' ", tbName,
                        Convert.ToDateTime(dt.Rows[i]["LastTime"]).ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    if (newTB.Contains(tbName))
                    {
                        sql = String.Format("SELECT COUNT(1) FROM {0} WHERE LastEditedTime>'{1}' ", tbName,
                        Convert.ToDateTime(dt.Rows[i]["LastTime"]).ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    }
                    sql = String.Format("SELECT COUNT(1) FROM sys_sheet WHERE LastEditedTime>'{0}' ", 
                        Convert.ToDateTime(dt.Rows[i]["LastTime"]).ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    Object obj = ExcuteScalar(sql);
                    if (obj != null && obj != DBNull.Value)
                    {
                        hasNewUpdate = Convert.ToInt32(obj) > 0;
                        if (hasNewUpdate)
                        {
                            break;
                        }
                    }
                }
                if (hasNewUpdate)
                {
                    String userName = "sa";
                    String pw = "wdxlzyn@#830";
                    String server = "ISSDCPLCDMK";
                    String valid = " -c -S " + server + " -U " + userName + " -P " + pw;

                    String subFolder = GetSubFolder("3");
                    String path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~"), "update\\" + subFolder);
                    String file = Path.Combine(path, "sys_sheet.dat");
                    String bcp = "bcp \"SELECT * FROM " + Connection.Database + ".dbo.sys_sheet WHERE LastEditedTime > '" +
                        Convert.ToDateTime(dt.Rows[0]["LastTime"]).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' \" queryout " + file + valid;
                    JZCommonHelper.ExeCommand(bcp);
                    String fileName = Yqun.Common.ContextCache.ApplicationContext.Current.UserName + "_" +
                        Yqun.Common.ContextCache.ApplicationContext.Current.Identification.MacAddress + "_" +
                        DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff") + ".zip";
                    String zipFile = Path.Combine(path, fileName);
                    if (File.Exists(zipFile))
                    {
                        File.Delete(zipFile);
                    }
                    List<String> list = new List<string>();
                    list.Add(file);
                    Boolean flag = JZCommonHelper.CreateZipFile(list, zipFile);
                    SaveUpdateInfo(Guid.NewGuid().ToString(), fileName, "3", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                }
            }
        }

        private String GetSubFolder(String fileType)
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
            else if (fileType == "4")
            {
                str = "DY";
            }
            else
            {
                str = "DB";
            }
            return str;
        }
    }
}
