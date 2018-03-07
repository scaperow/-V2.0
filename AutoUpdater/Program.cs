using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace AutoUpdater
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config"));
            log4net.Config.XmlConfigurator.ConfigureAndWatch(file);

            if (args.Length >= 2)
            {
                String MainAppName = args[0];
                String ExeStartPath = args[1];

                Process[] ps = Process.GetProcessesByName(MainAppName);
                foreach (Process p in ps)
                {
                    p.Kill();
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new AutoUpdaterForm());

                Process.Start(ExeStartPath);
            }
        }
    }
}
