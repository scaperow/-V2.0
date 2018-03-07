using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace JZUpgrade
{
    public delegate void UpdateProgressHandler(String title, Int32 step);
    public delegate void CloseHandler();

    public partial class UpgradeForm : Form
    {
        private UpgradeHelperClient uf = null;
        private Logger log = new Logger();
        private String applicationName = "";

        public UpgradeForm(String _applicationName)
        {
            InitializeComponent();
            applicationName = _applicationName;
            log.Logfolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log");
            log.IsUseLog = true;
            log.QueueBufferSize = 5;
            uf = new UpgradeHelperClient(log);
        }

        private void UpgradeForm_Load(object sender, EventArgs e)
        {
            Thread t1 = new Thread(new ThreadStart(Run));
            t1.Start();
        }

        private void Run()
        {
            try
            {
                DataTable dt = uf.GetUpgradeFiles();
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (!String.IsNullOrEmpty(applicationName))
                    {
                        Process[] arrayProcess = Process.GetProcessesByName(applicationName);
                        if (arrayProcess.Length > 0)
                        {
                            try
                            {
                                foreach (var item in arrayProcess)
                                {
                                    item.Kill();
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                    String path = Path.Combine(Application.StartupPath, "update");
                    String sql = "";
                    Boolean hasError = false;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int flag = -1;
                        String file = Path.Combine(path, dt.Rows[i]["FileName"].ToString());
                        if (File.Exists(file))
                        {
                            try
                            {
                                Boolean success = uf.UnZipFile(file);
                                if (success)
                                {
                                    String[] files = Directory.GetFiles(path);
                                    foreach (String fileName in files)
                                    {
                                        ProcessUpdateFile(fileName, Application.StartupPath);
                                    }
                                    File.Delete(file);
                                    flag = 1;
                                }
                            }
                            catch (Exception exx)
                            {
                                log.WriteLog(exx.Message, true);
                            }
                        }
                        if (flag == -1)
                        {
                            hasError = true;
                        }
                        sql = String.Format(@"UPDATE sys_update set FileState={0} where ID='{1}' ", flag, dt.Rows[i]["ID"].ToString());
                        uf.RunSqlCommand(sql);
                    }
                    String msg = hasError ? "更新成功！" : "更新失败，请联系管理员";
                    MessageBox.Show(msg);
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message, true);
            }
            finally
            {
                Thread.Sleep(600);
                if (!String.IsNullOrEmpty(applicationName))
                {
                    try
                    {
                        ProcessStartInfo Info = new ProcessStartInfo();
                        Info.FileName = Path.Combine(Application.StartupPath, applicationName + ".exe");
                        Info.Arguments = String.Concat("\"", Application.ProductName, "\" \"", Application.ExecutablePath, "\"");
                        Process.Start(Info);
                    }
                    catch
                    {
                    }
                    
                }
                CloseForm();
            }
        }

        private void ProcessUpdateFile(String file, String path)
        {
            String fileName = Path.GetFileName(file).ToLower().Trim();
            if (fileName.EndsWith(".zip"))
            {
                return;
            }
            else if (fileName.EndsWith(".bat"))
            {
                Process p = new Process();
                ProcessStartInfo pi = new ProcessStartInfo(file);
                pi.UseShellExecute = false;
                pi.CreateNoWindow = true;
                pi.RedirectStandardOutput = true;
                p.StartInfo = pi;
                p.Start();
                p.WaitForExit();
                p.Close();
            }
            else if (fileName.EndsWith(".sql"))
            {
                String sql = "";
                using (StreamReader sr = new StreamReader(file))
                {
                    sql = sr.ReadToEnd();
                }
                uf.RunSqlCommand(sql);
            }
            else
            {
                if (File.Exists(Path.Combine(path, fileName)))
                {
                    File.Delete(Path.Combine(path, fileName));
                }
                File.Move(file, Path.Combine(path, fileName));
            }
            File.Delete(file);

        }

        private void UpdateProgressInfo(String title, Int32 step)
        {
            if (this.InvokeRequired)
            {
                UpdateProgressHandler ssvh = new UpdateProgressHandler(UpdateProgressInfo);
                this.BeginInvoke(ssvh, title, step);
            }
            else
            {
                StatusLabel.Text = title;
                progressBar1.Value = step;
            }
        }

        private void CloseForm()
        {
            if (this.InvokeRequired)
            {
                CloseHandler ch = new CloseHandler(CloseForm);
                this.BeginInvoke(ch);
            }
            else
            {
                Close();
            }
        }

    }
}
