using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Yqun.Bases;
using Yqun.Common.Encoder;
using Yqun.Common.ContextCache;
using System.IO;
using Yqun.Services;
using ShrinkDatabaseApp.Properties;

namespace ShrinkDatabaseApp
{
    public partial class MainForm : Form
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        Font defaultFont, boldFont;

        public MainForm()
        {
            InitializeComponent();

            defaultFont = this.label1.Font;
            boldFont = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold);
            this.Icon = Resources.cab;
        }

        private void ButtonExe_Click(object sender, EventArgs e)
        {
            String Info = "";
            Boolean Result = false;

            try
            {
                DataSet DataSet = new DataSet("DataSourceSet");
                String FileName = Path.Combine(SystemFolder.DataSourceFolder, "DataSource.xml");

                if (File.Exists(FileName))
                {
                    DataSet.ReadXml(FileName);

                    String DataAdapterType = DataSetCoder.GetProperty(DataSet, 0, "name", "DataAdapterType", "value").ToString();
                    String DataBaseType = DataSetCoder.GetProperty(DataSet, 0, "name", "DataBaseType", "value").ToString();
                    String DataSource = DataSetCoder.GetProperty(DataSet, 0, "name", "DataSource", "value").ToString();
                    String DataInstance = DataSetCoder.GetProperty(DataSet, 0, "name", "DataInstance", "value").ToString();
                    String DataUserName = DataSetCoder.GetProperty(DataSet, 0, "name", "DataUserName", "value").ToString();
                    String DataPassword = DataSetCoder.GetProperty(DataSet, 0, "name", "DataPassword", "value").ToString();
                    String DataISAttach = DataSetCoder.GetProperty(DataSet, 0, "name", "DataISAttach", "value").ToString();
                    String DataIntegratedLogin = DataSetCoder.GetProperty(DataSet, 0, "name", "DataIntegratedLogin", "value").ToString();

                    ServerLoginInfos.DBConnectionInfo.DataAdapterType = DataAdapterType;
                    ServerLoginInfos.DBConnectionInfo.DataBaseType = DataBaseType;
                    ServerLoginInfos.DBConnectionInfo.DataSource = EncryptSerivce.Dencrypt(DataSource);
                    ServerLoginInfos.DBConnectionInfo.DataInstance = EncryptSerivce.Dencrypt(DataInstance);
                    ServerLoginInfos.DBConnectionInfo.DataUserName = EncryptSerivce.Dencrypt(DataUserName);
                    ServerLoginInfos.DBConnectionInfo.DataPassword = EncryptSerivce.Dencrypt(DataPassword);
                    ServerLoginInfos.DBConnectionInfo.DataISAttach = DataISAttach;
                    ServerLoginInfos.DBConnectionInfo.LocalStartPath = AppDomain.CurrentDomain.BaseDirectory + @"\";
                    ServerLoginInfos.DBConnectionInfo.DataIntegratedLogin = DataIntegratedLogin;

                    String DataBaseName = ServerLoginInfos.DBConnectionInfo.DataInstance;
                    int index = DataBaseName.ToLower().IndexOf("[apppath]");
                    if (index != -1)
                    {
                        DataBaseName = DataBaseName.ToLower().Replace("[apppath]", Application.StartupPath);
                    }

                    StringBuilder sql_select = new StringBuilder();
                    sql_select.Append("dump transaction [");
                    sql_select.Append(DataBaseName);
                    sql_select.Append("] with no_log;backup log [");
                    sql_select.Append(DataBaseName);
                    sql_select.Append("] with no_log;dbcc shrinkdatabase([");
                    sql_select.Append(DataBaseName);
                    sql_select.Append("])");

                    object o = Agent.CallLocalService("Yqun.BO.BOBase.dll", "ExcuteCommand", new object[] { sql_select.ToString() });
                    Result = (System.Convert.ToInt32(o) == 1);

                    Info = (Result ? "压缩数据库成功。" : "压缩数据库失败！");
                }
                else
                {
                    Info = "数据库连接信息未找到，请核实与铁路试验信息管理系统放在同一目录下！";
                }
            }
            catch(Exception ex)
            {
                logger.Error("ShrinkDatabaseApp:" + ex.Message);
                Info = "压缩数据库失败！";
            }

            this.label1.Text = Info;
            this.label1.ForeColor = (Result ? Color.Black : Color.Red);
            this.label1.Font = (Result ? defaultFont : boldFont);
        }
    }
}
