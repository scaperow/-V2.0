using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using System.Drawing;
using Yqun.Common.ContextCache;
using Yqun.Common.Encoder;
using System.IO;
using Yqun.Interfaces;
using Yqun.Permissions;
using Yqun.Bases;
using Yqun.Permissions.Common;
using Yqun.Bases.Exceptions;
using System.Collections;
using System.Windows.Forms;
using System.Threading;

namespace Yqun.Services
{
    public class Agent
    {
        //ʹ��log4net.dll��־�ӿ�ʵ����־��¼
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Agent()
        {
        }

        internal static bool ContainControlBase(string BizID)
        {
            WinUILayout Layout = Cache.CustomCache["63B5B22C-2063-4079-A238-913B5AA35BF1"] as WinUILayout;
            return Layout.Contains(BizID, LayoutDockType.Middle);
        }

        internal static object GetControlBase(string BizID)
        {
            WinUILayout Layout = Cache.CustomCache["63B5B22C-2063-4079-A238-913B5AA35BF1"] as WinUILayout;
            return Layout.GetControl(BizID, LayoutDockType.Middle);
        }

        internal static void AddControlBase(string Key, string Description, Icon icon, Control ToAddControl, LayoutDockType DockType, string ReferenceKey)
        {
            WinUILayout Layout = Cache.CustomCache["63B5B22C-2063-4079-A238-913B5AA35BF1"] as WinUILayout;
            Layout.AddControl(Key, Description, icon, ToAddControl, LayoutDockType.Middle, "");
        }

        public static object OpenRuntime(string BizID)
        {
            return OpenRuntime(BizID, true);
        }

        public static object OpenRuntime(string BizID, bool IsWindow)
        {
            bool bb = ContainControlBase(BizID);

            StringBuilder Sql_Select = new StringBuilder();
            Sql_Select.Append("select SYS_SOLFORDERTYPE.RUNDLL,SYS_SOLFORDERTYPE.RUNCLASS  from SYS_SOLFORDERTYPE left join SYS_SOLCONTENT  on SYS_SOLFORDERTYPE.TYPEFALG=SYS_SOLCONTENT.TYPEFALG where SYS_SOLCONTENT.ID='");
            Sql_Select.Append(BizID);
            Sql_Select.Append("' OR SYS_SOLCONTENT.NAME='");
            Sql_Select.Append(BizID);
            Sql_Select.Append("'");

            string FileName = "", TypeName = "";

            try
            {
                DataTable dt = CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { Sql_Select.ToString() }) as DataTable;
                if (dt != null && dt.Rows.Count > 0)
                {
                    FileName = dt.Rows[0]["RUNDLL"].ToString();
                    TypeName = dt.Rows[0]["RUNCLASS"].ToString();

                    object o = null;
                    if (!bb)
                    {
                        o = GetCellMthdInstance(FileName, TypeName);
                    }
                    else
                    {
                        o = GetControlBase(BizID);
                    }

                    if (o is Form && IsWindow)
                    {
                        //Form MainForm = Cache.CustomCache["63B5B22C-2063-4079-A238-913B5AA35BF1"] as Form;
                        //((Form)o).ShowDialog(MainForm);
                        ((Form)o).ShowDialog();

                        return o;
                    }

                    PropertyInfo pInfo = o.GetType().GetProperty("BizID");
                    if (pInfo != null) pInfo.SetValue(o, BizID, null);

                    pInfo = o.GetType().GetProperty("IDE");
                    if (pInfo != null) pInfo.SetValue(o, false, null);

                    return o;
                }
                else
                {
                    return null;
                }

            }
            catch (ServiceAccessException ex)
            {
                throw new ServiceAccessException(ex.Message);
            }
            catch (Exception ex)
            {
                ex.Source = TypeName;
                throw ex;
            }
        }

        static object GetInstance(string FileName, string TypeName)
        {
            try
            {
                object ins = null;
                if (File.Exists(FileName))
                {
                    Assembly a = Assembly.LoadFrom(FileName);
                    ins = a.CreateInstance(TypeName);
                }

                return ins;
            }
            catch (ServiceAccessException ex)
            {
                throw new ServiceAccessException(ex.Message);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    throw ex.InnerException;
                }
                else
                {
                    throw ex;
                }
            }
        }

        public static object CallService(string AssemblyName, string MethodName, object[] Parameters)
        {
            return Transfer.CallService(AssemblyName, MethodName, Parameters);
        }

        public static object CallLocalService(string AssemblyName, string MethodName, object[] Parameters)
        {
            return Transfer.CallLocalService(AssemblyName, MethodName, Parameters);
        }

        public static object CallRemoteService(string AssemblyName, string MethodName, object[] Parameters)
        {
            return Transfer.CallRemoteService(AssemblyName, MethodName, Parameters);
        }

        public static object CallLocalService(object[] ConnectionArgs, string AssemblyName, string MethodName, object[] Parameters)
        {
            return Transfer.CallLocalService(ConnectionArgs, AssemblyName, MethodName, Parameters);
        }

        public static object CallRemoteService(object[] ConnectionArgs, string AssemblyName, string MethodName, object[] Parameters)
        {
            return Transfer.CallRemoteService(ConnectionArgs, AssemblyName, MethodName, Parameters);
        }

        public static object GetCellMthdInstance(string DllName, string ClassName)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + DllName;
            return GetInstance(path, ClassName);
        }

        public static bool Login(string UserName, string Password)
        {
            Boolean IsSuccessful = System.Convert.ToBoolean(CallService("Yqun.BO.LoginBO.dll", "Login", new object[] { UserName, Password }));
            return IsSuccessful;
        }

        public static bool LoginProcess(string UserName, string Password, bool IsRemote)
        {
            if (!CheckUser(UserName, Password, IsRemote))
            {
                return false;
            }

            return SetUserContext(UserName, Password);
        }

        /// <summary>
        /// ��¼�û����ص�¼�ּ�����ʾ������Ϣ
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="MachineCode"></param>
        /// <param name="IsRemote"></param>
        /// <returns></returns>
        public static string LoginProcessmsg(string UserName, string Password,string MachineCode, bool IsRemote)
        {
            string msg = null;
            try
            {
                msg = CheckUserMsg(UserName, Password, MachineCode, IsRemote);

                SetUserContext(UserName, Password);
            }
            catch (Exception ex)
            {
                msg += "�쳣��Ϣ��"+ex.StackTrace;
            }
            return msg;
        }



        /// <summary>
        /// �����û�������
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static Boolean SetUserContext(string UserName, string Password)
        {
            StringBuilder sql_select = new StringBuilder();
            //���Ӳ�ѯ����  Scdel=0  2013-10-17
            sql_select.Append("select * from sys_auth_Users where Scdel=0 and UserName = '");
            sql_select.Append(UserName);
            sql_select.Append("'");

            DataTable userInfo = CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { sql_select.ToString() }) as DataTable;
            DataTable dt = CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { "SELECT TOP 1 HigWayClassification FROM dbo.sys_engs_ProjectInfo" }) as DataTable;
            if (dt != null && dt.Rows.Count == 1)
            {
                Yqun.Common.ContextCache.ApplicationContext.Current.SheetTitle = dt.Rows[0][0].ToString();
            }
            Yqun.Common.ContextCache.ApplicationContext.Current.UserName = UserName;
            Yqun.Common.ContextCache.ApplicationContext.Current.Password = Password;
            Yqun.Common.ContextCache.ApplicationContext.Current.Identification = new IdentificationInfo();
            Yqun.Common.ContextCache.ApplicationContext.Current.Identification.MachineName = Environment.MachineName;
            Yqun.Common.ContextCache.ApplicationContext.Current.Identification.OSVersion = Environment.OSVersion.VersionString;
            Yqun.Common.ContextCache.ApplicationContext.Current.Identification.UserName = Environment.UserName;
            try
            {
                Yqun.Common.ContextCache.ApplicationContext.Current.Identification.MacAddress = NetWorkInformation.GetMacAddress();
            }
            catch (Exception ex)
            {
                Yqun.Common.ContextCache.ApplicationContext.Current.Identification.MacAddress = "";
                logger.Error("GetMacAddress error:" + ex.ToString());
            }
            Yqun.Common.ContextCache.ApplicationContext.Current.LocalStartPath = Application.StartupPath;
            Yqun.Common.ContextCache.ApplicationContext.Current.DeniedModuleIDs = "";

            if (userInfo != null && userInfo.Rows.Count > 0)
            {
                Yqun.Common.ContextCache.ApplicationContext.Current.MachineCode = userInfo.Rows[0]["Devices"].ToString();
                Yqun.Common.ContextCache.ApplicationContext.Current.UserCode = userInfo.Rows[0]["Code"].ToString();
                Yqun.Common.ContextCache.ApplicationContext.Current.IsSystemUser = System.Convert.ToBoolean(userInfo.Rows[0]["IsSys"]);
                Yqun.Common.ContextCache.ApplicationContext.Current.Roles = DepositoryRole.Init(userInfo.Rows[0]["ID"].ToString());
                foreach (Role Role in Yqun.Common.ContextCache.ApplicationContext.Current.Roles)
                {
                    Role.Permissions = DepositoryPermission.Init(Role.Index);
                }
                Hashtable ht = OrganizationManager.GetProjectInformation(UserName);
                if (ht != null)
                {

                    Yqun.Common.ContextCache.ApplicationContext.Current.InProject = ht[NodeType.����] as ObjectInfo;
                    Yqun.Common.ContextCache.ApplicationContext.Current.InSegment = ht[NodeType.���] as ObjectInfo;
                    Yqun.Common.ContextCache.ApplicationContext.Current.InCompany = ht[NodeType.��λ] as ObjectInfo;
                    Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom = ht[NodeType.������] as ObjectInfo;
                }
                if (Yqun.Common.ContextCache.ApplicationContext.Current.UserCode.Length >= 16)
                {
                    Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code = Yqun.Common.ContextCache.ApplicationContext.Current.UserCode.Substring(0, 16);
                    //���õ�ģ�� Added by Tan in 20140729
                    object objD = CallService("Yqun.BO.QualificationAuthManager.dll", "GetDeniedModuleIDs", new object[] { Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code });
                    Yqun.Common.ContextCache.ApplicationContext.Current.DeniedModuleIDs = objD == null ? "" : objD.ToString();

                }
                else
                {
                    Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code = "-1";
                }

                return (userInfo != null && userInfo.Rows.Count > 0);
            }
            else
            {
                dt = CallService("Yqun.BO.LoginBO.dll", "GetRemoteAdminTable", new object[] { UserName }) as DataTable;
                if (dt != null && dt.Rows.Count > 0)
                {
                    Yqun.Common.ContextCache.ApplicationContext.Current.UserCode = dt.Rows[0]["Code"].ToString();
                    Yqun.Common.ContextCache.ApplicationContext.Current.IsSystemUser = true;
                    Yqun.Common.ContextCache.ApplicationContext.Current.Roles = new RoleCollection();
                    Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code = "-1";
                    Hashtable ht = OrganizationManager.GetProjectInformation(UserName);
                    if (ht != null)
                    {
                        Yqun.Common.ContextCache.ApplicationContext.Current.InProject = ht[NodeType.����] as ObjectInfo;
                    }
                }
                return (dt != null && dt.Rows.Count > 0);
            }
        }

        /// <summary>
        /// ���ػ���  ��־��  2013-10-18
        /// </summary>
        public static void LoadCache(object status)
        {

            //�޸�Bug307
            Thread.Sleep(500);
            int step = 1;
            String Msg = "ϵͳ���ڸ���...";
            IProgressCallback callBack = status as IProgressCallback;

            if (Yqun.Common.ContextCache.ApplicationContext.Current.ISLocalService)
            {
                if (callBack != null)
                    callBack.End();
                return;
            }

            callBack.SetText(Msg);
            callBack.Begin(0, 100);

            DataTable DataCache = null;

            object o = "";
            List<CacheHelper> ht = new List<CacheHelper>();
            try
            {
                DataCache = GetDataCache();
                step = ProgressGo(callBack, step);
            }
            catch (Exception ex)
            {
                Msg = string.Format("���»���ʧ�ܣ�ԭ�򣺡�{0}�����뼰ʱ��ϵ����Ա��", ex.Message);
                logger.Error(Msg);
                if (callBack != null)
                {
                    callBack.End();
                }
                return;
            }
            CompareField();
            step = ProgressGo(callBack, step);
            Boolean needLoadAgain = true;
            foreach (DataRow Row in DataCache.Rows)
            {
                String TableName = Row["TableName"].ToString();
                CacheHelper ch = new CacheHelper();
                ch.TableName = TableName;
                ch.AlreadyIDList = new List<string>();
                ch.HasData = true;
                try
                {
                    o = CallLocalService("Yqun.BO.LoginBO.dll", "GetMaxSCTS1", new object[] { TableName });
                    if (o == null || o.ToString() == "")
                    {
                        ch.MaxTime = new DateTime(1900, 1, 1);
                    }
                    else
                    {
                        ch.MaxTime = System.Convert.ToDateTime(o);
                    }

                }
                catch (Exception ex)
                {
                    ch.MaxTime = new DateTime(1900, 1, 1);
                    logger.Error("��ȡ�������ʱ�����" + ex.Message);
                }
                ht.Add(ch);
            }
            step = ProgressGo(callBack, step);

            while (needLoadAgain)
            {
                try
                {
                    List<String> sqls = GetCacheSqls(ht);
                    DataSet ds = GetNewData(sqls);

                    needLoadAgain = ds.Tables.Count > 0;
                    Thread.Sleep(50);
                    foreach (var ch in ht)
                    {
                        ch.HasData = false;
                    }

                    foreach (DataTable dt in ds.Tables)
                    {
                        needLoadAgain = UpdateTableData(dt.TableName, dt);

                        if (!needLoadAgain)
                        {
                            break;
                        }
                        CacheHelper ch = GetCacheHelperByTableName(ht, dt.TableName);
                        if (ch != null)
                        {
                            ch.HasData = true;
                            foreach (DataRow row in dt.Rows)
                            {
                                ch.AlreadyIDList.Add(row["ID"].ToString());
                                logger.Error(row["ID"].ToString() + "������");
                            }
                        }
                        step = ProgressGo(callBack, step);
                    }
                }
                catch (Yqun.Bases.Exceptions.ServiceAccessException sae)
                {
                    logger.Error("������ڴ���������Ժ�����:" + sae.Message);
                    needLoadAgain = false;
                }
                catch (Exception ex)
                {
                    Msg = string.Format("��������ʧ�ܣ�ԭ�򣺡�{0}�����뼰ʱ��ϵ����Ա��", ex.Message);
                    logger.Error(Msg);
                    needLoadAgain = false;
                }
            }

            callBack.SetText("���³ɹ���");
            callBack.StepTo(100);
            Thread.Sleep(2000);
            if (callBack != null)
            {
                callBack.End();
            }
        }

        /// <summary>
        /// ���ػ��� zhangdahang 2013-11-15
        /// �㷨˼·��
        /// 1���ӷ����������Ҫ���µı��������汾���б�
        /// 2���Աȱ��ض�Ӧ��İ汾�������Ƿ���Ҫ����
        /// 3������Ҫ���µı���ȡ����е����м�¼��ID���汾���б�
        /// 4���Աȱ�������еļ�¼�Ͱ汾��ͳ����Ҫ���µļ�¼��
        /// 5������Ҫ���µļ�¼���еļ�¼���и��£�������Ҫ���������¡�
        /// </summary>
        /// <param name="status"></param>
        public static void LoadCache_Update(object status)
        {


            //�޸�Bug307
            #region ׼��

            float step = 1;
            String Msg = "ϵͳ���ڸ���...";
            IProgressCallback callBack = status as IProgressCallback;

            if (Yqun.Common.ContextCache.ApplicationContext.Current.ISLocalService)
            {
                if (callBack != null)
                    callBack.End();
                return;
            }

            DataTable ServerDataCache = null;
            DataTable LocalDataCache = null;

            //˨��汾�У���û�а汾�У���ӡ�Stcs_1���У�Ŀǰ��ʱ����Ϊ��¼�汾
            CompareField();
            #endregion

            callBack.SetText(Msg);
            callBack.Begin(0, 100);

            Thread.Sleep(500);
            //��ȡ����������
            try
            {
                ServerDataCache = GetServerDataCache();
                LocalDataCache = GetLocalDataCache();

            }
            catch (Exception ex)
            {
                Msg = string.Format("���»���ʧ�ܣ�ԭ�򣺡�{0}�����뼰ʱ��ϵ����Ա��", ex.Message);
                logger.Error(Msg);
                if (callBack != null)
                {
                    callBack.End();
                }
                return;
            }
            step = ProgressGo(callBack, step);
            DataTable updateTable = GetUpdateDateTable(ServerDataCache, LocalDataCache);

            if (updateTable.Rows.Count == 0)
            {
                if (callBack != null)
                {
                    callBack.End();
                }
                return;
            }


            logger.Error("����" + updateTable.Rows.Count + "����Ҫ����");

            DataSet UpdateDB = new DataSet();
            foreach (DataRow cacheRow in updateTable.Rows)
            {

                string tableName = cacheRow["tableName"].ToString();
                callBack.SetText("������" + tableName + "��Ҫ���µļ�¼");

                DataTable ServerTable = GetServerDataTalbe("select ID,Scts_1 from " + tableName);
                if (LocalDataCache.Select("tableName ='" + tableName + "'").Length == 0)
                {
                    DataRow newCacheRow = LocalDataCache.NewRow();
                    newCacheRow.ItemArray = cacheRow.ItemArray;
                    LocalDataCache.Rows.Add(newCacheRow);
                    callBack.SetText("����±�" + tableName);
                    UpdateTableData(LocalDataCache.TableName, LocalDataCache);
                }
                DataTable LocalTable = GetLocalDataTable("select ID,Scts_1 from " + tableName);
                DataTable updateRecordTable = GetUpdateDateTable(ServerTable, LocalTable);
                lock (ProcessInfoList)
                {
                    if (!ProcessInfoList.ContainsKey(tableName.ToLower().Trim()))
                    {
                        ProcessInfo pi = new ProcessInfo();
                        pi.CompletedRowsCount = 0;
                        pi.TableRowsCount = updateRecordTable.Rows.Count;
                        ProcessInfoList.Add(tableName.ToLower().Trim(), pi);
                    }
                }
                UpdateDB.Tables.Add(updateRecordTable);
            }

            //����
            foreach (DataTable table in UpdateDB.Tables)
            {
                string tableName = table.TableName;

                #region ���Ը���
                if (table.Rows.Count == 0)
                {
                    DataTable cacheTalbe = GetServerDataTalbe("select * from sys_biz_Cache where tableName='" + tableName + "'");
                    UpdateTableData("sys_biz_Cache", cacheTalbe);
                    continue;
                }
                #endregion



                callBack.SetText("��ʼ���£���" + tableName);

                string IDStr = "";
                int index = 0;
                Int32 pageSize = GetPageSize(tableName);

                for (; index < table.Rows.Count; index++)
                {
                    IDStr += "'" + table.Rows[index]["ID"].ToString() + "',";
                    if ((index != 0 && index % pageSize == 0) || index == table.Rows.Count - 1)
                    {
                        try
                        {
                            IDStr = IDStr.Substring(IDStr.Length - 1) == "," ? IDStr.Remove(IDStr.Length - 1) : IDStr;
                            string sqlStr = "select * from " + tableName + " where ID in (" + IDStr + ")";
                            IDStr = "";

                            object[] parameterArr = new object[] { 
                                                    tableName, 
                                                    sqlStr,
                                                    callBack,
                                                    table ,
                                                ServerDataCache};
                            //����һ���߳� �����±������ݾ�
                            System.Threading.Thread process = GetOneThread();
                            process.Start(parameterArr);
                        }
                        catch (Exception e)
                        {
                            logger.Error(e.ToString());

                        }
                    }
                }

            }
            bool HasRunningThread = true;
            while (HasRunningThread)
            {
                HasRunningThread = false;
                lock (RunningThreads)
                {
                    foreach (Thread t in RunningThreads)
                    {
                        if (t.ThreadState == ThreadState.Running)
                        {
                            HasRunningThread = true;
                            break;
                        }
                    }
                }
                Thread.Sleep(50);
            }

            logger.Error("�������");
            callBack.SetText("���³ɹ���");
            callBack.StepTo(100);
            Thread.Sleep(1000);
            if (callBack != null)
            {
                callBack.End();
            }
        }

        #region For LoadCache_Update zhangdahang

        private static List<Thread> RunningThreads = new List<Thread>();
        /// <summary>
        /// ��ȡһ�����õ��߳�
        /// �����Ѿ���ɵ��߳̽����ͷţ�������GC������������
        /// �����߳�������ܳ���15
        /// </summary>
        /// <returns></returns>
        private static Thread GetOneThread()
        {
            while (true)
            {
                lock (RunningThreads)
                {
                    for (int i = 0; i < RunningThreads.Count; )
                    {
                        if (RunningThreads[i].ThreadState == ThreadState.Stopped)
                        {
                            RunningThreads.RemoveAt(i); GC.Collect();
                        }
                        else
                        {
                            i++;
                        }
                    }

                    if (RunningThreads.Count < 16)
                    {
                        foreach (Thread t in RunningThreads)
                        {
                            if (t.ThreadState == ThreadState.Unstarted)
                            {
                                return t;
                            }
                        }
                        Thread freeThread = new Thread(new ParameterizedThreadStart(UpdateProcess));
                        RunningThreads.Add(freeThread);
                        return freeThread;
                    }

                }
                Thread.Sleep(50);
            }
        }
        /// <summary>
        /// ��������е���Ҫ���µļ�¼����
        /// </summary>
        private static Dictionary<string, ProcessInfo> ProcessInfoList = new Dictionary<string, ProcessInfo>();
        private struct ProcessInfo
        {
            public int CompletedRowsCount;
            public int TableRowsCount;
        }

        /// <summary>
        /// �����ҳ��С
        /// </summary>
        /// <param name="tableName">����</param>
        /// <returns></returns>
        private static int GetPageSize(string tableName)
        {
            int pageSize = 500;
            int oneRecordSize = GetRecordSize(tableName, false);
            if (oneRecordSize > 0)
            {

                pageSize = 1 * 1024 * 1024 / oneRecordSize;
            }
            else
            {
                oneRecordSize = GetRecordSize(tableName, true);
                if (oneRecordSize > 0)
                {
                    pageSize = 1 * 1024 * 1024 / oneRecordSize;
                }
            }
            return pageSize;
        }
        /// <summary>
        /// ����������е�����¼���ڴ��С
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="remoteDB"></param>
        /// <returns></returns>
        private static int GetRecordSize(string tableName, bool remoteDB)
        {
            if (remoteDB)
            {
                DataTable dt = GetServerDataTalbe("select top 1 * from " + tableName);
                if (dt.Rows.Count > 0)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("", dt);
                    return Serialize.CompressMessage(ht).Length;
                }
            }
            else
            {
                DataTable dt = GetLocalDataTable("select top 1 * from " + tableName);
                if (dt.Rows.Count > 0)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("", dt);
                    return Serialize.CompressMessage(ht).Length;
                }
                else
                {
                    return 0;
                }
            }
            return 0;
        }
        /// <summary>
        /// ������ɺ󣬱��ܱ��¼���и���
        /// 
        /// </summary>
        /// <param name="callBack"></param>
        /// <param name="TalbeID"></param>
        /// <param name="tableName"></param>
        /// <param name="CompleteRowsCount"></param>
        private static void Agent_updateProcessEvent(IProgressCallback callBack, string tableName)
        {

            try
            {

                int totalRowsCount = 0;
                int totalCompleteRowsCount = 0;
                lock (ProcessInfoList)
                {
                    foreach (string key in ProcessInfoList.Keys)
                    {
                        totalCompleteRowsCount += ProcessInfoList[key].CompletedRowsCount;
                        totalRowsCount += ProcessInfoList[key].TableRowsCount;
                    }
                }
                double completeRate = (double)totalCompleteRowsCount / totalRowsCount;
                ProgressGo(callBack, (Int32)(completeRate * 100));

            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }
        }
        /// <summary>
        /// ���±������ݿ�
        /// </summary>
        /// <param name="parameters"></param>
        private static void UpdateProcess(object parameters)
        {
            object[] parameterArr = parameters as object[];// { tableName, sqlStr, index, updateRecordTable.Rows.Count };

            string tableName = parameterArr[0].ToString();
            string sqlStr = parameterArr[1].ToString();
            IProgressCallback callBack = (IProgressCallback)parameterArr[2];
            DataTable ServerTable = parameterArr[3] as DataTable;
            DataTable ServerDataCache = parameterArr[4] as DataTable;
            //��Զ�̻�ȡҪ���µļ�¼
            DataTable NewDataTable = GetServerDataTalbe(sqlStr);

            NewDataTable.TableName = tableName;
            //����dataCache
            parameterArr = new object[] { tableName, NewDataTable, ServerTable, ServerDataCache };
            UpdateDataCache(parameterArr);

            lock (ProcessInfoList)
            {
                if (ProcessInfoList.ContainsKey(tableName.ToLower().Trim()))
                {
                    ProcessInfo pi = ProcessInfoList[tableName];
                    if (NewDataTable.Rows.Count <= pi.TableRowsCount)
                    {
                        pi.CompletedRowsCount += NewDataTable.Rows.Count;
                        ProcessInfoList[tableName] = pi;
                    }
                    callBack.SetText("���ڸ��±�" + tableName + "���������" + ((int)((double)pi.CompletedRowsCount / pi.TableRowsCount * 100)).ToString() + "%");
                }
            }
            //���½�����
            Agent_updateProcessEvent(callBack, tableName);


        }
        /// <summary>
        /// ����dataCache
        /// </summary>
        /// <param name="tableInfo"></param>
        private static void UpdateDataCache(object tableInfo)
        {
            object[] parameterArr = tableInfo as object[];
            string tableName = parameterArr[0].ToString();
            DataTable NewDataTable = parameterArr[1] as DataTable;
            DataTable ServerTable = parameterArr[2] as DataTable;
            DataTable ServerDataCache = parameterArr[3] as DataTable;
            //���¼�¼
            UpdateTableData(tableName, NewDataTable);

            //����Ƿ���ȫ������
            DataTable LocalTable = GetLocalDataTable("select ID,Scts_1 from " + ServerTable.TableName);
            DataTable updateRecordTable = GetUpdateDateTable(ServerTable, LocalTable);
            if (updateRecordTable.Rows.Count == 0)
            {
                //�����ܱ�
                DataTable cacheTalbe = GetServerDataTalbe("select * from sys_biz_Cache where tableName='" + tableName + "'");
                UpdateTableData("sys_biz_Cache", cacheTalbe);
            }


        }
        /// <summary>
        /// �Ƚ�Զ�̱��еļ�¼�뱾�ر��еļ�¼�����ұ���ȱ�ٻ�汾����Զ�̵ļ�¼����
        /// </summary>
        /// <param name="ServerCache">Զ�̱�</param>
        /// <param name="LocalCache">���ر�</param>
        /// <returns>��Ҫ���µı�</returns>
        private static DataTable GetUpdateDateTable(DataTable ServerTable, DataTable LocalTable)
        {
            DataTable resultDT = ServerTable.Clone();

            foreach (DataRow row in ServerTable.Rows)
            {
                if (row["Scts_1"].ToString() == "")
                {
                    continue;
                }
                DataRow[] localRows = LocalTable.Select("ID='" + row["ID"].ToString() + "'");

                if (localRows.Length > 0)
                {
                    try
                    {
                        if (localRows[0]["Scts_1"] == null || localRows[0]["Scts_1"].ToString() == "" || DateTime.Parse(localRows[0]["Scts_1"].ToString()) < DateTime.Parse(row["Scts_1"].ToString()))
                        {
                            resultDT.Rows.Add(row.ItemArray);
                        }
                    }
                    catch
                    {
                        resultDT.Rows.Add(row.ItemArray);
                    }
                }
                else
                {
                    resultDT.Rows.Add(row.ItemArray);
                    ///�µı�������Ҫ����������Ҫ�����ڴ���Ӵ�����Ĵ���
                }
            }
            return resultDT;
        }

        /// <summary>
        /// ��ȡԶ�̱�����м�¼
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private static DataTable GetServerDataTalbe(string sql)
        {
            return CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { sql }) as DataTable;

        }
        /// <summary>
        /// ��ȡ���ر�����м�¼
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private static DataTable GetLocalDataTable(string sql)
        {
            return CallLocalService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { sql }) as DataTable;
        }
        /// <summary>
        /// ��ȡԶ�̱�
        /// zhangdahang 20131115
        /// </summary>
        /// <returns></returns>
        private static DataTable GetServerDataCache()
        {
            return GetServerDataTalbe("select  ID,TableName,Scts_1 from sys_biz_Cache");

        }
        /// <summary>
        /// ��ȡ���ر�
        /// zhangdahang 20131115
        /// </summary>
        /// <returns></returns>
        private static DataTable GetLocalDataCache()
        {
            return GetLocalDataTable("select ID,TableName,Scts_1 from sys_biz_Cache");

        }
        /// <summary>
        /// ��ȷ���ƽ�����
        /// </summary>
        /// <param name="callBack"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        private static float ProgressGo(IProgressCallback callBack, float step)
        {
            float newStep = 0;
            if (step < 99)
            {
                newStep = step + 1;
            }
            else
            {
                newStep = step;
            }
            callBack.StepTo((Int32)newStep);
            return newStep;
        }
        #endregion


        private static Int32 ProgressGo(IProgressCallback callBack, Int32 step)
        {
            Int32 newStep = 0;
            if (step <= 99)
            {
                newStep = step + 1;
            }
            else
            {
                newStep = step;
            }
            callBack.StepTo(newStep);
            return newStep;
        }

        private static CacheHelper GetCacheHelperByTableName(List<CacheHelper> ht, String tableName)
        {
            CacheHelper ch = null;
            foreach (var item in ht)
            {
                if (item.TableName.ToLower() == tableName.ToLower())
                {
                    ch = item;
                    break;
                }
            }
            return ch;
        }

        private static List<String> GetCacheSqls(List<CacheHelper> ht)
        {
            List<String> list = new List<string>();
            int top = 1000;
            foreach (CacheHelper de in ht)
            {
                if (!de.HasData)
                {
                    continue;
                }
                StringBuilder sql = new StringBuilder();
                if (de.TableName.ToLower() == "sys_biz_sheet")
                {
                    top = 15;
                }
                else
                {
                    top = 1000;
                }
                sql.Append(" Select top " + top + " * from " + de.TableName + " where ");
                sql.Append(" Scts_1>'" + de.MaxTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' ");
                if (de.AlreadyIDList.Count > 0)
                {
                    sql.Append(" and ID not in ('");
                    sql.Append(String.Join("','", de.AlreadyIDList.ToArray()));

                    sql.Append("') ");
                }
                sql.Append(" order by Scts_1");

                list.Add(sql.ToString());
            }
            return list;
        }

        #region �ж��û�

        public static bool CheckUser(string UserName, string Password, Boolean IsRemote)
        {
            object obj;
            if (IsRemote)
            {
                try
                {
                    obj = CallRemoteService("Yqun.BO.LoginBO.dll", "CheckUser", new object[] { UserName, Password });
                }
                catch (ServiceAccessException ex)
                {
                    obj = null;
                }
            }
            else
            {
                obj = CallLocalService("Yqun.BO.LoginBO.dll", "CheckUser", new object[] { UserName, Password });
            }

            if (obj == null)
                return false;
            return System.Convert.ToBoolean(obj);
        }

        public static string CheckUserMsg(string UserName, string Password,string MachineCode, Boolean IsRemote)
        {
            object obj = null; ;
            if (IsRemote)
            {
                try
                {
                    obj = CallRemoteService("Yqun.BO.LoginBO.dll", "LoginMsg", new object[] { UserName, Password,MachineCode });
                }
                catch (ServiceAccessException ex)
                {
                    obj = null;
                }
            }
            return obj.ToString();
        }


        #endregion

        #region �޸��û�����

        public static int SetUserPassword(string UserName, string Password)
        {
            try
            {
                StringBuilder SQL_UpdateData = new StringBuilder();
                //��������  Scts_1=Getdate()  2013-10-17
                SQL_UpdateData.Append("Update sys_auth_Users set Scts_1=Getdate(),Password ='");
                SQL_UpdateData.Append(EncryptSerivce.Encrypt(Password));
                SQL_UpdateData.Append("' Where UserName = '");
                SQL_UpdateData.Append(UserName);
                SQL_UpdateData.Append("'");

                object o = CallService("Yqun.BO.BOBase.dll", "ExcuteCommand", new object[] { SQL_UpdateData.ToString() });
                if (o == null)
                {
                    return -1;
                }

                return (System.Convert.ToInt32(o));
            }
            catch
            {
                return -1;
            }
        }

        #endregion

        #region ���»���

        private static void CompareField()
        {
            CallLocalService("Yqun.BO.LoginBO.dll", "CompareField", new object[] { });
        }
        private static DataSet GetNewData(List<String> ht)
        {
            return CallService("Yqun.BO.LoginBO.dll", "GetNewData", new object[] { ht }) as DataSet;
        }

        private static DataTable GetDataCache()
        {
            return CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { "select * from sys_biz_Cache" }) as DataTable;
        }

        private static object GetMaxSCTS(String TableName)
        {
            return CallLocalService("Yqun.BO.LoginBO.dll", "GetMaxSCTS", new object[] { TableName });
        }

        private static object GetMaxSCTS1(String TableName)
        {
            return CallLocalService("Yqun.BO.LoginBO.dll", "GetMaxSCTS1", new object[] { TableName });
        }

        private static DataTable GetNewDataSCTS(String TableName, object scts)
        {
            return CallService("Yqun.BO.LoginBO.dll", "GetNewDataSCTS", new object[] { TableName, scts }) as DataTable;
        }

        private static int GetNewDataCount(String TableName, object scts)
        {
            return System.Convert.ToInt32(CallService("Yqun.BO.LoginBO.dll", "GetNewDataCount", new object[] { TableName, scts }));
        }

        private static DataTable GetNewTableData(String TableName, object scts)
        {
            return CallService("Yqun.BO.LoginBO.dll", "GetNewTableData", new object[] { TableName, scts }) as DataTable;
        }

        private static DataTable GetNewTableData2(String TableName, object scts)
        {
            return CallService("Yqun.BO.LoginBO.dll", "GetNewTableData2", new object[] { TableName, scts }) as DataTable;
        }

        private static DataTable GetNewTableData3(String TableName, List<string> IDList)
        {
            return CallService("Yqun.BO.LoginBO.dll", "GetNewTableData3", new object[] { TableName, IDList }) as DataTable;
        }

        private static List<String> GetNewTableIDList(String TableName)
        {
            return CallService("Yqun.BO.LoginBO.dll", "GetNewTableIDList", new object[] { TableName }) as List<String>;
        }

        private static List<String> GetExistingTableIDList(String TableName)
        {
            return CallLocalService("Yqun.BO.LoginBO.dll", "GetNewTableIDList", new object[] { TableName }) as List<String>;
        }

        private static Boolean UpdateTableData(String TableName, DataTable Data)
        {
            if (Data.Rows.Count == 0)
                return true;

            return System.Convert.ToBoolean(CallLocalService("Yqun.BO.LoginBO.dll", "UpdateTableData", new object[] { TableName, Data }));
        }

        private static Boolean ExecuteDeletedCommand(String TableName, List<string> IDList, List<string> ExistingIDList)
        {
            List<string> DeletedIDList = new List<string>();
            foreach (String id in ExistingIDList)
            {
                if (!IDList.Contains(id))
                    DeletedIDList.Add(id);
            }

            if (DeletedIDList.Count > 0)
            {
                StringBuilder sql_Delete = new StringBuilder();
                //sql_Delete.Append("Delete from ");
                //sql_Delete.Append(TableName);
                //sql_Delete.Append(" Where ID in ('");
                //sql_Delete.Append(string.Join("','", DeletedIDList.ToArray()));
                //sql_Delete.Append("')");
                // 2013-10-17
                sql_Delete.Append("Update ");
                sql_Delete.Append(TableName);
                sql_Delete.Append(" Set Scts_1=Getdate(),Scdel=1 Where ID in ('");
                sql_Delete.Append(string.Join("','", DeletedIDList.ToArray()));
                sql_Delete.Append("')");

                object r = CallLocalService("Yqun.BO.LoginBO.dll", "ExcuteCommand", new object[] { sql_Delete.ToString() });
                return (System.Convert.ToInt32(r) == 1);
            }

            return true;
        }

        private static bool ExecuteUpdateCommand(string TableName, List<string> IDList, List<string> ExistingIDList)
        {
            logger.Error(string.Format("���ڸ������ݱ�{0}��...", TableName));

            List<string> UpdatedIDList = new List<string>();
            foreach (String id in IDList)
            {
                if (!ExistingIDList.Contains(id))
                    UpdatedIDList.Add(id);
            }

            Boolean Result = false;
            int UpdatedCount = 0;
            int Count = (TableName.ToLower() == "sys_biz_sheet" ? 1 : 10000);
            List<string> group = new List<string>();
            for (int i = 0; i < UpdatedIDList.Count; i++)
            {
                group.Add(UpdatedIDList[i]);

                if (group.Count % Count == 0 && group.Count != 0)
                {
                    DataTable Data = GetNewTableData3(TableName, group);
                    if (group.Count > Count)
                    {
                        Result = Result && UpdateTableData(TableName, Data);
                    }
                    else
                    {
                        Result = UpdateTableData(TableName, Data);
                    }

                    UpdatedCount = UpdatedCount + group.Count;
                    logger.Error(string.Format("��������ݱ�{0}����{1}%...", TableName, UpdatedCount * 100 / UpdatedIDList.Count));
                    group.Clear();
                }
            }

            if (group.Count > 0)
            {
                DataTable Data = GetNewTableData3(TableName, group);
                if (group.Count > Count)
                {
                    Result = Result && UpdateTableData(TableName, Data);
                }
                else
                {
                    Result = UpdateTableData(TableName, Data);
                }

                UpdatedCount = UpdatedCount + group.Count;
                logger.Error(string.Format("��������ݱ�{0}����{1}%...", TableName, UpdatedCount * 100 / UpdatedIDList.Count));
            }

            return true;
        }

        #endregion ���»���

        #region ��������

        public static Boolean TestNetwork()
        {
            try
            {
                return System.Convert.ToBoolean(CallRemoteService("Yqun.BO.LoginBO.dll", "TestNetwork", new object[] { }));
            }
            catch (Exception ex)
            {
                logger.Error("TestNetwork error:" + ex.ToString());
                return false;
            }
        }

        public static Boolean TestDbConnection(Boolean IsRemote)
        {
            if (IsRemote)
            {
                return System.Convert.ToBoolean(CallRemoteService("Yqun.BO.LoginBO.dll", "TestDbConnection", new object[] { }));
            }
            else
            {
                return System.Convert.ToBoolean(CallLocalService("Yqun.BO.LoginBO.dll", "TestDbConnection", new object[] { }));
            }
        }

        public static Boolean TestDbConnection(Boolean IsRemote, object[] ConnectionArgs)
        {
            if (IsRemote)
            {
                return System.Convert.ToBoolean(CallRemoteService(ConnectionArgs, "Yqun.BO.LoginBO.dll", "TestDbConnection", new object[] { }));
            }
            else
            {
                return System.Convert.ToBoolean(CallLocalService(ConnectionArgs, "Yqun.BO.LoginBO.dll", "TestDbConnection", new object[] { }));
            }
        }

        #endregion ��������
    }

    public class CacheHelper
    {
        public String TableName { get; set; }
        public DateTime MaxTime { get; set; }
        public List<String> AlreadyIDList { get; set; }
        public Boolean HasData { get; set; }
    }
}
