using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QuartzJobs;
using System.IO;

namespace WriteBakData
{
    static class Program
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config"));
            log4net.Config.XmlConfigurator.ConfigureAndWatch(file);

            logger.Info(string.Format("WriteBakData:{0}", DateTime.Now.ToString()));

            DataUploadJob Job = new DataUploadJob();
            Job.Execute(null);
        }
    }
}
