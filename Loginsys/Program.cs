using System;
using System.Text;
using System.Data;
using Yqun.Service;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;
using ShuXianCaiJiModule;
using ShuXianCaiJiComponents;
using FileMD5Contrast;

namespace Loginsys
{
    class Program
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config"));
            log4net.Config.XmlConfigurator.ConfigureAndWatch(file);

            logger.Info(string.Format("Loginsys:{0}", DateTime.Now.ToString()));
            
            DoMain();
            Thread.Sleep(3000);
        }

        public static void DoMain()
        {
            try
            {
                #region 更新或执行更新\工管中心MD5验证
                Thread _Thread = new Thread(new ThreadStart(InitUpdate));
                _Thread.Start();
                #endregion

                Logger log = new Logger(Path.Combine(Directory.GetCurrentDirectory(), "Log"));
                StringBuilder sql_select = new StringBuilder();
                sql_select.Append("select top 1 * from sys_login order by loginId");

                DataTable Data = Agent.CallLocalService("Yqun.BO.BOBase.dll", "GetDataTable", new object[] { sql_select.ToString() }) as DataTable;
                if (Data != null)
                {
                    logger.Info(string.Format("发现{0}条登录记录", Data.Rows.Count));

                    DataRow Row = null;
                    if (Data.Rows.Count > 0)
                        Row = Data.Rows[0];
                    else
                    {
                        Row = Data.NewRow();
                        Data.Rows.Add(Row);
                    }

                    String UserName = Row["username"].ToString();
                    String Password = Row["password"].ToString();
                    Boolean r = Agent.LoginProcess(UserName, Password);
                    int Result = (r ? 1 : 0);

                    logger.Info(string.Format("用户‘{0}’登录{1}", UserName, r ? "成功" : "失败"));

                    Row["loginresult"] = Result;

                    object o = Agent.CallLocalService("Yqun.BO.BOBase.dll", "Update", new object[] { Data });
                    r = (Convert.ToInt32(o) == 1);
                    logger.Info(string.Format("保存用户‘{0}’登录状态{1}", UserName, r ? "成功" : "失败"));

                    if (!ProcessHelper.IsRuningProcess("JZUpgradeAgent"))
                    {
                        ProcessStartInfo Info = new ProcessStartInfo();
                        Info.CreateNoWindow = false;
                        Info.UseShellExecute = true;
                        Info.FileName = Path.Combine(Application.StartupPath, "JZUpgrade.exe");
                        Info.Arguments = "\"9\"";
                        //1 管理系统文件+不执行，2 采集系统文件+不执行，3 管理系统数据+不执行，
                        //4 管理系统文件+数据+执行； 
                        //5 采集系统文件+执行
                        //6 管理系统执行
                        //7 采集系统执行
                        //8 管理系统文件+执行
                        //9 电液伺服系统执行
                        Process.Start(Info);

                    }
                    CaijiCommHelper _CaijiCommHelper = new CaijiCommHelper(log);
                    SXCJModule _Module = new SXCJModule();
                    _Module.SpecialSetting.MachineType = Convert.ToInt32(ConfigurationManager.ConfigurationManager.AppSettings["type"]);
                    switch (_CaijiCommHelper.GetClientConfigStatus(Convert.ToString(ConfigurationManager.ConfigurationManager.AppSettings["Machinecode"])))
                    {
                        case -1:
                            break;
                        case 0:
                            _CaijiCommHelper.UploadClientConfig(Convert.ToString(ConfigurationManager.ConfigurationManager.AppSettings["testcode"]), Convert.ToString(ConfigurationManager.ConfigurationManager.AppSettings["Machinecode"]), Newtonsoft.Json.JsonConvert.SerializeObject(_Module.SpecialSetting));
                            break;
                        case 1:
                            _CaijiCommHelper.UpDateClientConfig(Convert.ToString(ConfigurationManager.ConfigurationManager.AppSettings["Machinecode"]), Newtonsoft.Json.JsonConvert.SerializeObject(_Module.SpecialSetting));
                            break;
                        case 2:
                            _CaijiCommHelper.GetClientConfig(ref _Module, Convert.ToString(ConfigurationManager.ConfigurationManager.AppSettings["Machinecode"]));
                            break;
                    }


                    #region OldCode
                    //if (Internet.IsWanAlive())
                    //{
                    //    logger.Info("正在更新试验项目...");
                    //    sql_select = new StringBuilder();
                    //    sql_select.Append("select * from sys_testitem");
                    //    DataTable networkData = Agent.CallRemoteService("Yqun.BO.BOBase.dll", "GetDataTable", new object[] { sql_select.ToString() }) as DataTable;
                    //    DataTable localData = Agent.CallLocalService("Yqun.BO.BOBase.dll", "GetDataTable", new object[] { sql_select.ToString() }) as DataTable;
                    //    if (networkData != null && localData != null)
                    //    {
                    //        foreach (DataRow testRow in networkData.Rows)
                    //        {
                    //            String Index = testRow["ID"].ToString();
                    //            DataRow[] DataRows = localData.Select("ID='" + Index + "'");
                    //            if (DataRows.Length == 0)
                    //            {
                    //                DataRow localRow = localData.NewRow();
                    //                localRow["ID"] = testRow["ID"];
                    //                localRow["ItemName"] = testRow["ItemName"];
                    //                localRow["TestCount"] = testRow["TestCount"];
                    //                localRow["Type"] = testRow["Type"];
                    //                localData.Rows.Add(localRow);
                    //            }
                    //            else
                    //            {
                    //                DataRow localRow = DataRows[0];
                    //                localRow["ID"] = testRow["ID"];
                    //                localRow["ItemName"] = testRow["ItemName"];
                    //                localRow["TestCount"] = testRow["TestCount"];
                    //                localRow["Type"] = testRow["Type"];
                    //            }
                    //        }

                    //        object rt = Agent.CallLocalService("Yqun.BO.BOBase.dll", "Update", new object[] { localData });
                    //        r = (Convert.ToInt32(o) == 1);
                    //        logger.Info(string.Format("更新用户‘{0}’的试验项目{1}", UserName, r ? "成功" : "失败"));
                    //    }
                    //    else if (networkData == null)
                    //    {
                    //        logger.Info("获得服务端的试验项目失败");
                    //        logger.Info(string.Format("更新用户‘{0}’的试验项目失败", UserName));
                    //    }
                    //    else if (localData == null)
                    //    {
                    //        logger.Info("获得本机的试验项目表结构失败");
                    //        logger.Info(string.Format("更新用户‘{0}’的试验项目失败", UserName));
                    //    }
                    //}
                    //else
                    //{
                    //    logger.Info("无法访问Internet，更新试验项目失败");
                    //}
                    #endregion

                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("应用出错：{0}", ex.Message));
            }
        }

        private static void  InitUpdate()
        {

            #region 工管中心MD5验证

            string ErrMsg = string.Empty;
            Logger log = new Logger(Path.Combine(Directory.GetCurrentDirectory(), "Log"));
            CaijiCommHelper _CaijiCommHelper = new CaijiCommHelper(log);
            if (!GetMD5(_CaijiCommHelper.GetECode(Convert.ToString(ConfigurationManager.ConfigurationManager.AppSettings["Machinecode"])), out ErrMsg))
            {                
            }
            else
            {
            }


            #endregion
        }
        public static bool GetMD5(string EMachinCode, out string ErrMsg)
        {
            try
            {
                FileMD5 _FileMD5 = null;
                ErrMsg = string.Empty;
                if (_FileMD5 == null)
                {
                    _FileMD5 = new FileMD5();
                }
                return _FileMD5.FileMD5VS(EMachinCode, 3, out ErrMsg);
            }
            catch (Exception ex)
            {
                ErrMsg = "false";
            }
            return false;
        }
    }

   

    public class ProcessHelper
    {
        public static bool IsRuningProcess(String name)
        {
            Process[] processes = Process.GetProcesses();
            foreach (Process p in processes)
            {
                if (p.ProcessName.ToLower() == name.ToLower())
                {
                    return true;
                }
            }
            return false;
        }
    }

   
}
