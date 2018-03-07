using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Yqun.Bases
{
    public class SystemFolder
    {
        public static string StartPath
        {
            get
            {
                return Application.StartupPath;
            }
        }

        public static string AppConfig
        {
            get
            {
                return Path.Combine(Application.StartupPath, "AppConfig");
            }
        }

        public static string LogFolder
        {
            get
            {
                return Path.Combine(AppConfig, "Log");
            }
        }

        public static string CacheFolder
        {
            get
            {
                return Path.Combine(AppConfig, "Cache");
            }
        }

        public static string DataFolder
        {
            get
            {
                return Path.Combine(AppConfig, "Data");
            }
        }

        public static string DockingConfig
        {
            get
            {
                return Path.Combine(AppConfig, "Dock");
            }
        }

        public static string TipsFolder
        {
            get
            {
                return Path.Combine(AppConfig, "Tips");
            }
        }

        public static string SkinFolder
        {
            get
            {
                return Path.Combine(AppConfig, "Skins");
            }
        }

        public static string DataSourceFolder
        {
            get
            {
                return Path.Combine(AppConfig, "DataSource");
            }
        }
    }
}
