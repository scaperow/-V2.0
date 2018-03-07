using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Yqun.Common.Split;
using Yqun.Common.Encoder;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using Yqun.Common.ContextCache;
using Yqun.Bases;
using Yqun.Bases.Exceptions;

namespace Yqun
{
    public class ClientConfigurationInfo
    {
        #region 应用程序版本

        public static void ValidateSoftwareVersion()
        {
            byte[] key = new byte[0];
            if (File.Exists(SystemFolder.StartPath + "\\Configurations.dll"))
            {
                key = FileIO.ReadFileToBytes(SystemFolder.StartPath + "\\Configurations.dll");
                key = Encrypt.DecryptBytes(key, "`!@#$%^&*~!@#$$^%*$%#^%$@@@^&)(^&*(");
            }

            string info = "";
            if (key.Length > 0)
            {
                info = Encoding.UTF8.GetString(key);
            }

            if (info.StartsWith("单机版5.0"))
            {
                Yqun.Common.ContextCache.ApplicationContext.Current.ISLocalService = true;
            }
            else if (info.StartsWith("网络版5.0"))
            {
                Yqun.Common.ContextCache.ApplicationContext.Current.ISLocalService = false;
            }
            else
            {
                throw new SoftwareVersionErrorException("验证软件版本失败。");
            }
        }

        #endregion 应用程序版本

        #region 应用程序名称

        public static string GetAppName() 
        {
            string key = "";
            if (File.Exists(SystemFolder.AppConfig + "\\AppName.dat"))
            {
                key = FileIO.ReadFileToString(SystemFolder.AppConfig + "\\AppName.dat", "utf-8");
            }

            return key;
        }

        public static void SetAppName(string AppName)
        {
            string path = SystemFolder.AppConfig + "\\AppName.dat";
            Yqun.Common.Encoder.FileIO.WriteFile(AppName, "utf-8", path);
        }

        #endregion 应用程序名称

        #region 加载缓存方法

        public static DataTable GetCacheData(string tablename)
        {
            DataTable dt = null;

            string path = SystemFolder.AppConfig + "\\Cache";
            string name = string.Format("T_{0}.xml", tablename);
            string filename = Path.Combine(path, name);
            if (File.Exists(filename))
            {
                dt = new DataTable();
                dt.ReadXml(filename);
            }

            return dt;
        }

        public static DataSet GetCacheData()
        {
            DataSet ds = new DataSet();

            string path = SystemFolder.AppConfig + "\\Cache";
            string[] fs = Directory.GetFiles(path, "T_*.xml");

            DataTable t = new DataTable();
            t.Columns.Add(new DataColumn("FILENAME", typeof(string)));

            for (int i = 0; i < fs.Length; i++)
            {
                DataRow r = t.NewRow();
                r[0] = fs[i];
                t.Rows.Add(r);
            }

            t.AcceptChanges();

            DataRow[] rs = t.Select("FILENAME <> ''", "FILENAME");

            for (int i = 0; i < rs.Length; i++)
            {
                DataTable tt = new DataTable();
                tt.ReadXml(rs[i][0].ToString());
                ds.Tables.Add(tt);
            }

            return ds;
        }

        #endregion 加载缓存方法

        /// <summary>
        /// 获得程序默认的图标
        /// </summary>
        /// <returns></returns>
        public static Icon GetDefaultIcon()
        {
            string path = SystemFolder.StartPath + "\\FormIco.ico";
            if (File.Exists(path))
            {
                Icon ico = new Icon(path);
                return ico;
            }

            return null;
        }

        #region 保存登陆成功的用户名称

        public static DataSet GetUserNames()
        {
            try
            {
                string dir = SystemFolder.AppConfig;
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                string path = SystemFolder.AppConfig + "\\Users.dat";
                string txt = Yqun.Common.Encoder.FileIO.ReadFileToString(path,"utf-8");

                DataSet d = Yqun.Common.Encoder.DataSetCoder.DeSerializeDataSetFromXml(txt);
                return d;
            }
            catch
            {
                DataSet d = new DataSet();
                DataTable t = new DataTable();
                t.Columns.Add(new DataColumn("NAME", typeof(string)));
                t.Columns.Add(new DataColumn("SCTS", typeof(DateTime)));
                d.Tables.Add(t);

                return d;
            }
        }

        public static void SetUserNames(DataSet UserSet)
        {
            try
            {
                string dir = SystemFolder.AppConfig;
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                string path = SystemFolder.AppConfig + "\\Users.dat";
                string txt = Yqun.Common.Encoder.DataSetCoder.SerializeDataSetToXml(UserSet);
                Yqun.Common.Encoder.FileIO.WriteFile(txt, "utf-8", path);
            }
            catch
            {}
        }

        #endregion 保存登陆成功的用户名称
    }
}
