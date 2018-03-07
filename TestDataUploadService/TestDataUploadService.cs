using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;

namespace TestDataUploadService
{
    public partial class TestDataUploadService : ServiceBase
    {
        Logger log = null;
        public TestDataUploadService()
        {
            InitializeComponent();
            log = new Logger();
            log.IsUseLog = true;
            log.Logfolder = Path.Combine(Directory.GetCurrentDirectory(), "log");
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                backgroundWorker1.RunWorkerAsync();
                log.WriteLog("服务启动", true);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message, false);
            }
        }

        protected override void OnStop()
        {
            try
            {
                backgroundWorker1.CancelAsync();
                log.WriteLog("服务停止", true);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message, false);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }
    }
}
