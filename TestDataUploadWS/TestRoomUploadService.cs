using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using BizCommon;
using System.ServiceModel;
using System.Collections;
using TransferServiceCommon;
using System.Xml;
using LABTRANSINTERFACE;
using System.Configuration;
using System.Threading;
using Newtonsoft.Json;
using System.Timers;

namespace TestDataUploadWS
{
    public partial class TestRoomUploadService : ServiceBase
    {
        private Dictionary<string, EventHandler> Handlers;
        public Dictionary<System.Timers.Timer, string> Times = new Dictionary<System.Timers.Timer, string>();
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public TestRoomUploadService()
        {
            log4net.Config.XmlConfigurator.Configure();
            InitializeComponent();
            Handlers = new Dictionary<string, EventHandler>();
        }

        private System.Timers.Timer RegisterTask(string timeConfig, int interval, EventHandler startHandler, EventHandler finishHandler)
        {
            if (interval <= 0)
            {
                interval = Convert.ToInt32(ConfigurationManager.AppSettings["UploadIntervalTime"]);
            }

            if (interval == 0)
            {
                interval = 600;
            }

            var timer = new System.Timers.Timer(interval);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(delegate(object sender, ElapsedEventArgs e)
            {
                var t = sender as System.Timers.Timer;
                var config = Times[timer];
                var span = ConfigurationManager.AppSettings[config];
                var now = DateTime.Now;

                if (IsInrange(now, span))
                {
                    if (startHandler != null)
                    {
                        try
                        {
                            startHandler.Invoke(sender, e);
                        }
                        catch (Exception startError)
                        {
                            Logger.Error(startError);
                        }
                    }
                }
                else
                {
                    if (finishHandler != null)
                    {
                        finishHandler.Invoke(sender, e);
                    }
                }
            });

            Times[timer] = timeConfig;

            return timer;
        }

        protected override void OnStart(string[] args)
        {


            if (ConfigurationManager.AppSettings["CaiJiStartUpload"] == "1")
            {
                UploadCaiJiLines();
            }

            if (ConfigurationManager.AppSettings["GGCStartUpload"] == "1")
            {
                UploadGGCLines();
            }

            if (ConfigurationManager.AppSettings["StatisticsSynchronous"] == "1")
            {
                StartStatistics();
            }

            var linesTimer = RegisterTask("UploadTimeSpan", 0, new EventHandler(delegate
            {
                UploadLines();
            }), null);

            linesTimer.Start();

            Logger.Info("试验数据上传服务启动");
        }

        /// <summary>
        /// 上传工管中心以前的数据
        /// </summary>
        private void UploadCaiJiLines()
        {
            List<Sys_Line> lstLine;
            string strLocalServerIP = ConfigurationManager.AppSettings["LocalServerIP"];
            String libAddress = "net.tcp://lib.kingrocket.com:8066/TransferService.svc";
            object objLib = CallRemoteServerMethod(libAddress, "Yqun.BO.BusinessManager.dll", "GetMQWSLinesByIP",
                new Object[] { strLocalServerIP });
            if (objLib != null)
            {
                lstLine = objLib as List<Sys_Line>;

                //foreach (Sys_Line line in lstLine)
                //{
                //    logger.Info(string.Format("【{0}】采集数据上传处理开始", line.LineName));
                //    UploadCaiJi ub = new UploadCaiJi(line.LineIP, line.DataBaseName, line.UserName,line.PassWord);
                //    ub.StartApplyQueue(line.DataBaseName);
                //}
                UploadCaiJiNew ub = new UploadCaiJiNew();
                ub.StartApplyQueue("MQDealDoc");
            }
            else
            {
                Logger.Info(string.Format("采集数据上传没有需要处理的线路"));
            }
        }

        /// <summary>
        /// 上传工管中心以前的数据
        /// </summary>
        private void UploadGGCLines()
        {
            List<Sys_Line> lstLine;
            string strLocalServerIP = ConfigurationManager.AppSettings["LocalServerIP"];
            String libAddress = "net.tcp://lib.kingrocket.com:8066/TransferService.svc";
            object objLib = CallRemoteServerMethod(libAddress, "Yqun.BO.BusinessManager.dll", "GetGGCUploadLinesByIP",
                new Object[] { strLocalServerIP });
            if (objLib != null)
            {
                lstLine = objLib as List<Sys_Line>;
                foreach (Sys_Line line in lstLine)
                {
                    Logger.Info(string.Format("【{0}】GGC上传资料开始", line.LineName));
                    UploadEMC ub = new UploadEMC(line.LineName, line.LineIP, line.LinePort);
                    ub.StartUpload();
                }
            }
            else
            {
                Logger.Info(string.Format("GGC没有需要上传的线路"));
            }
        }

        private void UploadLines()
        {
            List<Sys_Line> lstLine;
            string strLocalServerIP = ConfigurationManager.AppSettings["LocalServerIP"];
            String libAddress = "net.tcp://lib.kingrocket.com:8066/TransferService.svc";
            //String libAddress = "net.tcp://TanPC:7006/TransferService.svc";
            object objLib = CallRemoteServerMethod(libAddress, "Yqun.BO.BusinessManager.dll", "GetUploadLinesByIP",
                new Object[] { strLocalServerIP });
            if (objLib != null)
            {
                lstLine = objLib as List<Sys_Line>;
                foreach (Sys_Line line in lstLine)
                {
                    if (string.IsNullOrEmpty(line.UploadAddress))
                    {
                        Logger.Info(string.Format("【{0}】未配置上传地址,不能上传数据", line.LineName));
                    }
                    else if (string.IsNullOrEmpty(line.TestRoomCodeMap))
                    {
                        Logger.Info(string.Format("【{0}】未配置试验室编码映射,不能上传数据", line.LineName));
                    }
                    else
                    {
                        Logger.Info(string.Format("【{0}】上传资料开始", line.LineName));
                        UploadBase ub = new UploadBase(line.LineName, line.LineIP, line.LinePort, line.DataBaseName, line.UploadAddress, line.TestRoomCodeMap, line.JSDWCode);
                        ub.StartUpload();
                        Logger.Info(string.Format("【{0}】上传资料结束", line.LineName));

                    }
                }
            }
            else
            {
                Logger.Info(string.Format("没有需要上传的线路"));
            }
        }

        public void StartStatistics()
        {
            List<Sys_Line> lstLine;
            string strLocalServerIP = ConfigurationManager.AppSettings["LocalServerIP"];
            String libAddress = "net.tcp://lib.kingrocket.com:8066/TransferService.svc";
            object objLib = CallRemoteServerMethod(libAddress, "Yqun.BO.BusinessManager.dll", "GetStatisticsLinesByIP",
                new Object[] { strLocalServerIP });

            if (objLib != null)
            {
                lstLine = objLib as List<Sys_Line>;
                foreach (Sys_Line line in lstLine)
                {
                    Logger.Info("开始同步线路 " + line.LineName + " 的数据");
                    Console.WriteLine("开始同步线路 " + line.LineName + " 的数据");
                    var service = new StatisticsRunner(line);
                    service.StartApplyQueue();

                    Thread.Sleep(1000);
                }
            }
            else
            {
                Logger.Info(string.Format("没有需要同步的线路"));
            }
        }

        public static bool IsInrange(DateTime time, string config)
        {
            if (!string.IsNullOrEmpty(config))
            {
                string[] sUTS = config.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in sUTS)
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        string[] ss = s.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        int start = int.Parse(ss[0]);
                        int end = int.Parse(ss[1]);
                        if (time.Hour >= start && time.Hour <= end)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        protected override void OnStop()
        {
            Logger.Info("试验数据上传服务停止");
        }

        #region 通用方法

        public object CallRemoteServerMethod(String address, string AssemblyName, string MethodName, object[] Parameters)
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
                Logger.Error("call remote server method error: " + ex.Message);
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

        public static void SaveConfig(string key, string value)
        {
            // Open App.Config of executable
            System.Configuration.Configuration config =
             ConfigurationManager.OpenExeConfiguration
                        (ConfigurationUserLevel.None);

            // Add an Application Setting.
            config.AppSettings.Settings.Add(key, value);

            // Save the changes in App.config file.
            config.Save(ConfigurationSaveMode.Modified);

            // Force a reload of a changed section.
            ConfigurationManager.RefreshSection("appSettings");
        }
        #endregion

        internal void Start()
        {
            OnStart(null);
        }
    }
}
