using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UpdaterCommon;
using UpdaterComponents;
using System.IO;
using System.Diagnostics;

namespace AutoUpdater
{
    public partial class AutoUpdaterForm : Form
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public AutoUpdaterForm()
        {
            InitializeComponent();
        }

        private void AutoUpdaterForm_Shown(object sender, EventArgs e)
        {
            try
            {
                List<UpdaterFileInfo> updateLocalFile = LocalFileVersionManager.GetLocalOldFile();
                List<UpdaterFileInfo> newUpdateFile = ServerFileVersionManager.GetNewUpdaterFile(updateLocalFile);
                UpdateSystem(newUpdateFile);
            }
            catch(Exception ex)
            {
                Hide();
                MessageBox.Show(string.Format("更新失败，原因为 {0}\r\n\r\n点击确定，启动系统...", ex.Message), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void UpdateSystem(List<UpdaterFileInfo> newUpdateFile)
        {
            if (newUpdateFile.Count == 0)
            {
                Close();
                return;
            }

            logger.Error(string.Format("需要更新的文件个数：{0}", newUpdateFile.Count));

            List<String> finishedUpdateFile = new List<String>();
            List<String> failureUpdateFile = new List<String>();

            int TotalBytes = 0;
            foreach (UpdaterFileInfo updaterFileInfo in newUpdateFile)
                TotalBytes = TotalBytes + updaterFileInfo.FileData.Length;

            logger.Error(string.Format("需要更新的数据大小：{0}", TotalBytes));

            int CurrentTotalBytes = 0;
            foreach (UpdaterFileInfo updaterFileInfo in newUpdateFile)
            {
                logger.Error(string.Format("正在更新的文件是‘{0}’", updaterFileInfo.FileName));

                String FileName = Path.Combine(Application.StartupPath, updaterFileInfo.FileName);

                try
                {
                    File.WriteAllBytes(FileName, updaterFileInfo.FileData);
                    CurrentTotalBytes = CurrentTotalBytes + updaterFileInfo.FileData.Length;

                    StatusLabel.Text = string.Format("正在更新文件‘{0}’...", updaterFileInfo.FileName);
                    progressBar1.Value = (int)Math.Round(CurrentTotalBytes * 100.0 / TotalBytes, 0);

                    finishedUpdateFile.Add(updaterFileInfo.FileName);

                    logger.Error(string.Format("文件‘{0}’更新成功", updaterFileInfo.FileName));
                }
                catch(Exception ex)
                {
                    failureUpdateFile.Add(updaterFileInfo.FileName);

                    logger.Error(string.Format("文件‘{0}’更新失败，失败的原因是 {1}", updaterFileInfo.FileName, ex.Message));
                }

                Update();
            }

            Boolean Result = LocalFileVersionManager.SaveLocalFile(newUpdateFile);
            Update();
            Hide();
            String Message = string.Format("更新完成,成功{0}个,失败{1}个\r\n{2}\r\n点击确定，启动系统...", finishedUpdateFile.Count, failureUpdateFile.Count, string.Join("\r\n", failureUpdateFile.ToArray()));
            logger.Error(Message);
            MessageBox.Show(Message);
            Close();
        }
    }
}
