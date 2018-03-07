using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using BizCommon;
using System.Windows.Forms;
using System.ServiceModel;
using TransferServiceCommon;
using System.Collections;

namespace Yqun.BO.BusinessManager
{
    public class LineHelper : BOBase
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Sys_Line GetLineByID(Guid projectID)
        {
            Sys_Line line = null;
            String sql = "SELECT LineName,ID,IPAddress,Port,UserName ,PassWord ,DataBaseName FROM dbo.sys_line WHERE ID='" + projectID + "'";
            DataTable dt = GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                line = new Sys_Line();
                line.ID = new Guid(dt.Rows[0]["ID"].ToString());
                line.LineName = dt.Rows[0]["LineName"].ToString();
                line.LineIP = dt.Rows[0]["IPAddress"].ToString();
                line.LinePort = dt.Rows[0]["Port"].ToString();
                line.UserName = dt.Rows[0]["UserName"].ToString();
                line.PassWord = dt.Rows[0]["PassWord"].ToString();
                line.DataBaseName = dt.Rows[0]["DataBaseName"].ToString();
            }
            return line;
        }

        /// <summary>
        /// 更新一条线的所有模板、表单、公式等信息
        /// </summary>
        /// <param name="lineID"></param>
        /// <returns></returns>
        public Boolean UpdateLine(Guid lineID, List<String> moduleIDs, Boolean isModule, Boolean isRelationSheet)
        {
            List<String> files = new List<String>();
            if (isModule)
            {
                files.Add("sys_dictionary");
                files.Add("sys_formulas");
                files.Add("sys_module");
                files.Add("sys_stadium_config");
                files.Add("sys_module_config");
                if (isRelationSheet)
                {
                    files.Add("sys_module_sheet");
                }
            }
            else
            {
                files.Add("sys_sheet");
            }
            Sys_Line line = GetLineByID(lineID);
            Boolean flag = true;
            String moduleIDStr = "'" + String.Join("','", moduleIDs.ToArray()) + "'";
            if (line != null)
            {
                try
                {
                    String path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~"), "update/" + line.LineName);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    List<String> list = new List<string>();
                    foreach (String tbName in files)
                    {
                        list.Add(Path.Combine(path, tbName + ".dat"));
                        flag = flag && OutPutFile(tbName, path, moduleIDStr, isModule);
                    }
                    String zipFile = Path.Combine(path, line.LineName + ".zip");
                    if (File.Exists(zipFile))
                    {
                        File.Delete(zipFile);
                    }
                    flag = flag && JZCommonHelper.CreateZipFile(list, zipFile);
                    if (flag)
                    {
                        FileStream stream = null;
                        JZFile f = new JZFile();
                        f.FileName = line.LineName + ".zip";
                        stream = new FileInfo(zipFile).OpenRead();
                        Byte[] buffer = new Byte[stream.Length];
                        stream.Read(buffer, 0, Convert.ToInt32(stream.Length));
                        f.FileType = "all";
                        f.FileData = buffer;
                        stream.Close();
                        String lineAddress = "net.tcp://" + line.LineIP + ":" + line.LinePort + "/TransferService.svc";
                        object obj = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "RunUpdate",
                            new Object[] { f, line.LineIP, line.UserName, line.PassWord, isModule, isRelationSheet });
                        flag = Convert.ToBoolean(obj);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("generate updated file error for line " + line.LineName + ": " + ex.Message);
                    flag = false;
                }
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// 通过bcp命令，生成dat文件
        /// </summary>
        /// <param name="tbName"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private Boolean OutPutFile(String tbName, String path, String moduleIDStr, Boolean isModule)
        {
            String fileName = Path.Combine(path, tbName + ".dat");
            Boolean flag = true;
            List<String> auth = GetAuthInfoFromConnectionString(Connection.ConnectionString);
            String server = auth[0];
            String userName = auth[1];
            String pw = auth[2];

            String valid = " -c -S " + server + " -U " + userName + " -P " + pw;
            try
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                String bcp = "bcp " + Connection.Database + ".dbo." + tbName + " out " + fileName + valid;

                if (tbName == "sys_module")
                {
                    bcp = "bcp \"SELECT * FROM " + Connection.Database + ".dbo.sys_module  WHERE ID in (" +
                        moduleIDStr + ")\" queryout " + fileName + valid;
                }
                else if (tbName == "sys_stadium_config")
                {
                    bcp = "bcp \"SELECT * FROM " + Connection.Database + ".dbo.sys_stadium_config WHERE ID in (" +
                        moduleIDStr + ")\" queryout " + fileName + valid;
                }
                else if (tbName == "sys_module_config")
                {
                    bcp = "bcp \"SELECT * FROM " + Connection.Database + ".dbo.sys_module_config WHERE ModuleID in (" +
                       moduleIDStr + ")\" queryout " + fileName + valid;
                }
                else if (tbName == "sys_formulas")
                {

                    bcp = "bcp \"SELECT * FROM " + Connection.Database + ".dbo.sys_formulas WHERE ModuleID IN (" +
                        moduleIDStr + ") and SheetID in (SELECT DISTINCT SheetID FROM " + Connection.Database + ".dbo.sys_module_sheet WHERE ModuleID in(" +
                        moduleIDStr + "))\" queryout " + fileName + valid;
                }
                else if (tbName == "sys_sheet")
                {
                    if (isModule)
                    {
                        bcp = "bcp \"SELECT * FROM " + Connection.Database + ".dbo.sys_sheet WHERE ID IN (SELECT DISTINCT SheetID FROM " + Connection.Database + ".dbo.sys_module_sheet WHERE ModuleID in(" +
                            moduleIDStr + "))\" queryout " + fileName + valid;
                    }
                    else
                    {
                        bcp = "bcp \"SELECT * FROM " + Connection.Database + ".dbo.sys_sheet WHERE ID IN (" +
                        moduleIDStr + ")\" queryout " + fileName + valid;
                    }
                }
                else if (tbName == "sys_module_sheet")
                {
                    bcp = "bcp \"SELECT * FROM " + Connection.Database + ".dbo.sys_module_sheet WHERE ModuleID IN (" +
                        moduleIDStr + ")\" queryout " + fileName + valid;
                }
                bcp = "master..xp_cmdshell '" + bcp.Replace("'", "''") + "' ";
                //flag = JZCommonHelper.ExeCommand(bcp);
                //logger.Error(bcp);
                flag = ExcuteCommand(bcp) >= 1;
            }
            catch (Exception e)
            {
                logger.Error("OutPutFile error: " + e.Message);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// 通过传来的文件流，更新服务端数据库
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public Boolean RunUpdate(JZFile file, String server, String userName, String password, Boolean isModule, Boolean isRelationSheet)
        {
            String path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~"), "update");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            String fName = Path.Combine(path, file.FileName);
            File.WriteAllBytes(fName, file.FileData);
            Boolean flag = JZCommonHelper.UnZipFile(fName);
            if (flag)
            {
                String spName = "sp_update";
                List<String> files = new List<String>();
                if (isModule)
                {
                    files.Add("sys_dictionary");
                    files.Add("sys_formulas");
                    files.Add("sys_module");
                    files.Add("sys_stadium_config");
                    files.Add("sys_module_config");
                    if (isRelationSheet)
                    {
                        files.Add("sys_module_sheet");
                        spName = "sp_update_module_sheet";
                    }
                    else
                    {
                        spName = "sp_update_module";
                    }
                }
                else
                {
                    files.Add("sys_sheet");
                    spName = "sp_update_sheet";
                }

                foreach (String item in files)
                {
                    String fPath = Path.Combine(path, item + ".dat");
                    if (File.Exists(fPath))
                    {
                        String bcp = "bcp " + Connection.Database + ".dbo." + item + "_update in " + fPath + " -c -S " + server +
                            " -U " + userName + " -P " + password;
                        bcp = "master..xp_cmdshell '" + bcp.Replace("'", "''") + "' ";
                        //flag = JZCommonHelper.ExeCommand(bcp);
                        //logger.Error(bcp);
                        flag = ExcuteCommand(bcp) >= 1;
                    }
                }
                if (flag)
                {
                    logger.Error("start store procedure: " + spName);
                    int i = RunStoreProcedure(spName);
                    logger.Error("end " + spName + ": " + i);
                    flag = i > 0;
                }
            }
            return flag;
        }

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

        public DataTable GetLineList()
        {
            String sql = "SELECT ID,LineName,Description,IPAddress,Port FROM dbo.sys_line WHERE IsActive=1 Order By LineName";
            return GetDataTable(sql);
        }

        public Boolean SyncLineAndModule(List<String> moduleIDs, List<Guid> lineIDs, Boolean isModule, Boolean isRelationSheet)
        {
            Boolean flag = true;
            foreach (var item in lineIDs)
            {
                flag = flag && UpdateLine(item, moduleIDs, isModule, isRelationSheet);
            }
            return flag;
        }

        #region 为某线路生成指定模板的代码

        public Boolean UpdateSpecialModule(Guid sourceModuleID, Guid destModuleID)
        {
            String moduleLibAddress = "net.tcp://Lib.kingrocket.com:8066/TransferService.svc";
            //去模板库中取模板数据
            object obj = CallRemoteServerMethod(moduleLibAddress, "Yqun.BO.BusinessManager.dll", "GetSpecialFile",
                            new Object[] { destModuleID });

            JZFile file = obj as JZFile;
            if (file != null)
            {
                List<String> auth = GetAuthInfoFromConnectionString(Connection.ConnectionString);
                String server = auth[0];
                String userName = auth[1];
                String password = auth[2];
                //将取到的数据更新进自己的数据库中
                return RunSpecialUpdate(file, server, userName, password, sourceModuleID);
            }
            else
            {
                return false;
            }
        }

        public JZFile GetSpecialFile(Guid moduleID)
        {
            String[] files = new String[] { "sys_biz_ModuleCatlog", "sys_biz_SheetCatlog", "sys_dictionary",
                "sys_formulas", "sys_module", "sys_sheet", "sys_module_sheet", "sys_stadium_config", "sys_module_config"};
            List<String> moduleIDs = new List<string>();
            moduleIDs.Add(moduleID.ToString());
            Boolean flag = true;
            String moduleIDStr = "'" + String.Join("','", moduleIDs.ToArray()) + "'";

            try
            {
                String path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~"), "update");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                List<String> list = new List<string>();
                foreach (String tbName in files)
                {
                    list.Add(Path.Combine(path, tbName + ".dat"));
                    flag = flag && OutPutFile(tbName, path, moduleIDStr, true);
                }
                String zipFile = Path.Combine(path, moduleID + ".zip");
                if (File.Exists(zipFile))
                {
                    File.Delete(zipFile);
                }
                flag = flag && JZCommonHelper.CreateZipFile(list, zipFile);
                if (flag)
                {
                    FileStream stream = null;
                    JZFile f = new JZFile();
                    f.FileName = moduleID + ".zip";
                    stream = new FileInfo(zipFile).OpenRead();
                    Byte[] buffer = new Byte[stream.Length];
                    stream.Read(buffer, 0, Convert.ToInt32(stream.Length));
                    f.FileType = "all";
                    f.FileData = buffer;
                    stream.Close();
                    return f;
                }
            }
            catch (Exception ex)
            {
                logger.Error("generate special file error for module " + moduleID + ": " + ex.Message);

            }
            return null;
        }

        public List<String> GetAuthInfoFromConnectionString(String connectionString)
        {
            List<String> list = new List<string>();
            String[] arr = connectionString.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            String server = "";
            String userName = "sa";
            String password = "";
            foreach (var item in arr)
            {
                if (item.StartsWith("Data Source="))
                {
                    server = item.Replace("Data Source=", "");
                }
                else if (item.StartsWith("User ID="))
                {
                    userName = item.Replace("User ID=", "");
                }
                else if (item.StartsWith("Password="))
                {
                    password = item.Replace("Password=", "");
                }
            }
            list.Add(server);
            list.Add(userName);
            list.Add(password);

            return list;
        }

        public Boolean RunSpecialUpdate(JZFile file, String server, String userName, String password, Guid moduleID)
        {
            String path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~"), "update");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            String fName = Path.Combine(path, file.FileName);
            File.WriteAllBytes(fName, file.FileData);
            Boolean flag = JZCommonHelper.UnZipFile(fName);
            if (flag)
            {
                String[] files = new String[] { "sys_biz_ModuleCatlog", "sys_biz_SheetCatlog", "sys_dictionary",
                "sys_formulas", "sys_module", "sys_sheet", "sys_module_sheet", "sys_stadium_config", "sys_module_config"};

                foreach (String item in files)
                {
                    String fPath = Path.Combine(path, item + ".dat");
                    if (File.Exists(fPath))
                    {
                        String bcp = "bcp " + Connection.Database + ".dbo." + item + "_update in " + fPath + " -c -S " + server +
                            " -U " + userName + " -P " + password;
                        flag = flag && JZCommonHelper.ExeCommand(bcp);
                        logger.Error(bcp);
                    }
                }
                if (flag)
                {
                    String sql = @"UPDATE dbo.sys_module_update SET ID=b.ID, Name=b.Name, Description=b.Description
	                                FROM dbo.sys_module_update a JOIN dbo.sys_module b
	                                ON b.ID='" + moduleID + @"' 
	                                UPDATE dbo.sys_formulas_update SET ModuleID='" + moduleID + @"' 
	                                UPDATE dbo.sys_module_sheet_update SET ModuleID='" + moduleID + @"' 
	                                UPDATE dbo.sys_module_config_update SET ModuleID='" + moduleID + @"' 
	                                UPDATE dbo.sys_stadium_config_update SET ID='" + moduleID + @"'";
                    ExcuteCommand(sql);
                    int i = RunStoreProcedure("sp_update");
                    flag = i > 0;
                }
            }
            return flag;
        }

        public DataSet GetLineModuleList()
        {
            String moduleLibAddress = "net.tcp://Lib.kingrocket.com:8066/TransferService.svc";

            //去模板库中取模板数据
            object obj = CallRemoteServerMethod(moduleLibAddress, "Yqun.BO.BusinessManager.dll", "GetModuleCategoryAndModule",
                            new Object[] { });
            return obj as DataSet;
        }
        #endregion

        public List<Sys_Line> GetUploadLines()
        {
            List<Sys_Line> lst = new List<Sys_Line>();
            try
            {
                Sys_Line line = null;
                String sql = "SELECT ID,LineName,IPAddress,Port,UserName ,PassWord ,DataBaseName,Description,JSDWCode,StartUpload,TestRoomCodeMap,UploadAddress FROM dbo.sys_line WHERE IsActive=1 and StartUpload=1 ";
                //logger.Error("GetUploadLines sql:"+sql);
                DataTable dt = GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        line = new Sys_Line();
                        line.ID = new Guid(dt.Rows[i]["ID"].ToString());
                        line.LineName = dt.Rows[i]["LineName"].ToString();
                        line.LineIP = dt.Rows[i]["IPAddress"].ToString();
                        line.LinePort = dt.Rows[i]["Port"].ToString();
                        line.UserName = dt.Rows[i]["UserName"].ToString();
                        line.PassWord = dt.Rows[i]["PassWord"].ToString();
                        line.DataBaseName = dt.Rows[i]["DataBaseName"].ToString();
                        line.Description = dt.Rows[i]["Description"].ToString();
                        line.JSDWCode = dt.Rows[i]["JSDWCode"].ToString();
                        line.StartUpload = int.Parse(dt.Rows[i]["StartUpload"].ToString());
                        line.TestRoomCodeMap = dt.Rows[i]["TestRoomCodeMap"].ToString();
                        line.UploadAddress = dt.Rows[i]["UploadAddress"].ToString();
                        lst.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("GetUploadLines error:" + ex.ToString());
            }

            return lst;
        }
        public List<Sys_Line> GetUploadLinesByIP(string IP)
        {
            List<Sys_Line> lst = new List<Sys_Line>();
            try
            {
                Sys_Line line = null;
                String sql = "SELECT ID,LineName,IPAddress,Port,UserName ,PassWord ,DataBaseName,Description,JSDWCode,StartUpload,TestRoomCodeMap,ModuleCodeMap,UploadAddress FROM dbo.sys_line WHERE IsActive=1 and StartUpload=1 and IPAddress='" + IP + "' ";
                //logger.Error("GetUploadLines sql:"+sql);
                DataTable dt = GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        line = new Sys_Line();
                        line.ID = new Guid(dt.Rows[i]["ID"].ToString());
                        line.LineName = dt.Rows[i]["LineName"].ToString();
                        line.LineIP = dt.Rows[i]["IPAddress"].ToString();
                        line.LinePort = dt.Rows[i]["Port"].ToString();
                        line.UserName = dt.Rows[i]["UserName"].ToString();
                        line.PassWord = dt.Rows[i]["PassWord"].ToString();
                        line.DataBaseName = dt.Rows[i]["DataBaseName"].ToString();
                        line.Description = dt.Rows[i]["Description"].ToString();
                        line.JSDWCode = dt.Rows[i]["JSDWCode"].ToString();
                        line.StartUpload = int.Parse(dt.Rows[i]["StartUpload"].ToString());
                        line.TestRoomCodeMap = dt.Rows[i]["TestRoomCodeMap"].ToString();
                        line.ModuleCodeMap = dt.Rows[i]["ModuleCodeMap"].ToString();
                        line.UploadAddress = dt.Rows[i]["UploadAddress"].ToString();
                        lst.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("GetUploadLinesByIP error:" + ex.ToString());
            }

            return lst;
        }

        public List<Sys_Line> GetGGCUploadLinesByIP(string IP)
        {
            List<Sys_Line> lst = new List<Sys_Line>();
            try
            {
                Sys_Line line = null;
                String sql = "SELECT ID,LineName,IPAddress,Port,UserName ,PassWord ,DataBaseName,Description,JSDWCode,StartUpload,TestRoomCodeMap,ModuleCodeMap,UploadAddress FROM dbo.sys_line WHERE IsActive=1 and GGCStartUpload=1 and IPAddress='" + IP + "' ";
                //logger.Error("GetUploadLines sql:"+sql);
                DataTable dt = GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        line = new Sys_Line();
                        line.ID = new Guid(dt.Rows[i]["ID"].ToString());
                        line.LineName = dt.Rows[i]["LineName"].ToString();
                        line.LineIP = dt.Rows[i]["IPAddress"].ToString();
                        line.LinePort = dt.Rows[i]["Port"].ToString();
                        line.UserName = dt.Rows[i]["UserName"].ToString();
                        line.PassWord = dt.Rows[i]["PassWord"].ToString();
                        line.DataBaseName = dt.Rows[i]["DataBaseName"].ToString();
                        line.Description = dt.Rows[i]["Description"].ToString();
                        line.JSDWCode = dt.Rows[i]["JSDWCode"].ToString();
                        line.StartUpload = int.Parse(dt.Rows[i]["StartUpload"].ToString());
                        line.TestRoomCodeMap = dt.Rows[i]["TestRoomCodeMap"].ToString();
                        line.ModuleCodeMap = dt.Rows[i]["ModuleCodeMap"].ToString();
                        line.UploadAddress = dt.Rows[i]["UploadAddress"].ToString();
                        lst.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("GetUploadLinesByIP error:" + ex.ToString());
            }

            return lst;
        }

        public List<Sys_Line> GetStatisticsLinesByIP(string IP)
        {
            List<Sys_Line> lst = new List<Sys_Line>();
            try
            {
                Sys_Line line = null;
                String sql = "SELECT ID,LineName,IPAddress,Port,UserName ,PassWord ,DataBaseName,Description FROM dbo.sys_line WHERE IsActive=1 and [Statistics] = 1 and IPAddress = '" + IP + "'";
                DataTable dt = GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        line = new Sys_Line();
                        line.ID = new Guid(dt.Rows[i]["ID"].ToString());
                        line.LineName = dt.Rows[i]["LineName"].ToString();
                        line.LineIP = dt.Rows[i]["IPAddress"].ToString();
                        line.LinePort = dt.Rows[i]["Port"].ToString();
                        line.UserName = dt.Rows[i]["UserName"].ToString();
                        line.PassWord = dt.Rows[i]["PassWord"].ToString();
                        line.DataBaseName = dt.Rows[i]["DataBaseName"].ToString();
                        line.Description = dt.Rows[i]["Description"].ToString();
                        lst.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("GetStatisticsLinesByIP error:" + ex.ToString());
            }

            return lst;
        }

        public List<Sys_Line> GetMQWSLinesByIP(string IP)
        {
            List<Sys_Line> lst = new List<Sys_Line>();
            try
            {
                Sys_Line line = null;
                String sql = "SELECT ID,LineName,IPAddress,Port,UserName ,PassWord ,DataBaseName,Description,JSDWCode,StartUpload,TestRoomCodeMap,ModuleCodeMap,UploadAddress FROM dbo.sys_line WHERE IsActive=1 ";
                //logger.Error("GetUploadLines sql:"+sql);
                DataTable dt = GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        line = new Sys_Line();
                        line.ID = new Guid(dt.Rows[i]["ID"].ToString());
                        line.LineName = dt.Rows[i]["LineName"].ToString();
                        line.LineIP = dt.Rows[i]["IPAddress"].ToString();
                        line.LinePort = dt.Rows[i]["Port"].ToString();
                        line.UserName = dt.Rows[i]["UserName"].ToString();
                        line.PassWord = dt.Rows[i]["PassWord"].ToString();
                        line.DataBaseName = dt.Rows[i]["DataBaseName"].ToString();
                        line.Description = dt.Rows[i]["Description"].ToString();
                        line.JSDWCode = dt.Rows[i]["JSDWCode"].ToString();
                        line.StartUpload = int.Parse(dt.Rows[i]["StartUpload"].ToString());
                        line.TestRoomCodeMap = dt.Rows[i]["TestRoomCodeMap"].ToString();
                        line.ModuleCodeMap = dt.Rows[i]["ModuleCodeMap"].ToString();
                        line.UploadAddress = dt.Rows[i]["UploadAddress"].ToString();
                        lst.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("GetUploadLinesByIP error:" + ex.ToString());
            }

            return lst;
        }

        /// <summary>
        /// 获取所有在线用户
        /// </summary>
        /// <returns></returns>
        public DataTable GetOnlineUserList()
        {
            String sql = "SELECT  SessionID ,LineID ,0 AS SelectTag,LineName,SegmentName,CompanyName,TestRoomName ,UserName ,LastActiveTime ,LoginTime FROM dbo.sys_auth_Users_Online  WHERE LastActiveTime>DATEADD(mi,-10,GETDATE()) ORDER BY LastActiveTime DESC";
            return GetDataTable(sql);
        }
    }
}
