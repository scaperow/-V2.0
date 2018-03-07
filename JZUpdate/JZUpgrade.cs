using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Data;
using System.Runtime.InteropServices;

namespace JZUpgrade
{
    public delegate void SetVisibleHandler(Boolean flag);
    public delegate void UpdateProgressHandler(String title, Int32 step);
    public delegate void CloseHandler();

    public class JZUpgrade : Form
    {
        private String updateFlag = "";
        private UpdateHelperClient uhc = null;
        //private Logger log = new Logger();
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const int WM_COPYDATA = 0x004A;

        public JZUpgrade(String _updateFlag)
        {
            //Thread.Sleep(30 * 1000);
            InitializeComponent();
            updateFlag = _updateFlag;
            //log.Logfolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log");
            //log.IsUseLog = true;
            //log.QueueBufferSize = 5;
            uhc = new UpdateHelperClient(_updateFlag);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(0, 85);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(346, 20);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 1;
            // 
            // label
            // 
            this.label.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label.Location = new System.Drawing.Point(0, 1);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(346, 83);
            this.label.TabIndex = 0;
            this.label.Text = "���ڸ���";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // JZUpgrade
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(345, 106);
            this.ControlBox = false;
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.label);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "JZUpgrade";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "����������-���½���";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.JZUpdate_Load);
            this.ResumeLayout(false);

        }

        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.ProgressBar progressBar;

        #endregion

        private Boolean GetUpdate()
        {
            Boolean flag = false;
            try
            {
                DataTable dt = uhc.GetNewUpdate();
                if (dt != null && dt.Rows.Count > 0)
                {
                    //SendUpdateStatus("1");
                    UpdateVisible(false);
                    UpdateProgressInfo("���ڻ�ȡ���¡���", 0);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        String subFolder = uhc.GetSubFolder(dt.Rows[i]["FileType"].ToString());
                        String path = Path.Combine(Application.StartupPath, "update\\" + subFolder);
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        String file = Path.Combine(path, dt.Rows[i]["FileName"].ToString());
                        logger.Info(file);
                        byte[] bytes = uhc.GetUpdateFileByID(new Guid(dt.Rows[i]["ID"].ToString()));
                        if (bytes != null)
                        {
                            File.WriteAllBytes(file, bytes);
                            uhc.SaveUpdateInfo(dt.Rows[i]["ID"].ToString(), dt.Rows[i]["FileName"].ToString(),
                                dt.Rows[i]["FileType"].ToString(),
                                Convert.ToDateTime(dt.Rows[i]["CreatedServerTime"]).ToString("yyyy-MM-dd HH:mm:ss.fff"));
                            flag = true;
                        }
                        if (i == dt.Rows.Count - 1)
                        {
                            UpdateProgressInfo("��ȡ�������", 100);
                        }
                        else
                        {
                            UpdateProgressInfo("���ڻ�ȡ���¡���", (int)((100 / dt.Rows.Count) * (i + 1)));
                        }
                    }

                }
                else
                {
                    //SendUpdateStatus("0");
                    UpdateProgressInfo("��ȡ�������", 100);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            return flag;
        }

        //public Boolean GetUpdateNew(Label lblUpdateMsg)
        //{
        //    Boolean flag = false;
        //    try
        //    {
        //        DataTable dt = uhc.GetNewUpdate();
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                lblUpdateMsg.Text = "���ڻ�ȡ���¡�";
        //                String subFolder = uhc.GetSubFolder(dt.Rows[i]["FileType"].ToString());
        //                String path = Path.Combine(Application.StartupPath, "update\\" + subFolder);
        //                if (!Directory.Exists(path))
        //                {
        //                    Directory.CreateDirectory(path);
        //                }
        //                String file = Path.Combine(path, dt.Rows[i]["FileName"].ToString());
        //                log.WriteLog(file, true);
        //                byte[] bytes = uhc.GetUpdateFileByID(new Guid(dt.Rows[i]["ID"].ToString()));
        //                lblUpdateMsg.Text = "���ڱ�����¡�";
        //                if (bytes != null)
        //                {
        //                    File.WriteAllBytes(file, bytes);
        //                    uhc.SaveUpdateInfo(dt.Rows[i]["ID"].ToString(), dt.Rows[i]["FileName"].ToString(),
        //                        dt.Rows[i]["FileType"].ToString(),
        //                        Convert.ToDateTime(dt.Rows[i]["CreatedServerTime"]).ToString("yyyy-MM-dd HH:mm:ss.fff"));
        //                    flag = true;
        //                }
        //                if (i == dt.Rows.Count - 1)
        //                {
        //                    lblUpdateMsg.Text = "��ȡ�������";
        //                }
        //            }

        //        }
        //        else
        //        {
        //            lblUpdateMsg.Text = "��ȡ�������";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.WriteLog(ex.Message, true);
        //    }
        //    return flag;
        //}


        private void LoadUpdate()
        {
            try
            {
                DataTable dt = uhc.GetUnUsedUpdateInfo();
                if (dt != null && dt.Rows.Count > 0)
                {
                    //UpdateAlert ua = new UpdateAlert();
                    //ua.Show(this);
                    UpdateVisible(true);
                    UpdateProgressInfo("����Ӧ�ø��¡���", 1);
                    Boolean mainFormRun = false;
                    Boolean applyUpdate = true;
                    //1 ����ϵͳ�ļ�+��ִ�У�
                    //2 �ɼ�ϵͳ�ļ�+��ִ�У�
                    //3 ����ϵͳ����+��ִ�У�
                    //4 ����ϵͳ�ļ�+����+ִ�У� 
                    //5 �ɼ�ϵͳ�ļ�+ִ��
                    //6 ����ϵͳִ��
                    //7 �ɼ�ϵͳִ��
                    //8 ����ϵͳ�ļ�+ִ��
                    //9 ��Һ�ŷ��ļ�+ִ��
                    //10 �쵼�����+ִ��
                    String appName = ApplicationHelper.GetApplicationName(updateFlag);
                    mainFormRun = true;
                    if (applyUpdate)
                    {
                        Thread.Sleep(1500);
                        bool bUpdateFlag = true;//���±�ǣ������ж��Ƿ�ȫ������
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Int32 fileState = -1;
                            String subFolder = uhc.GetSubFolder(dt.Rows[i]["FileType"].ToString());
                            String path = Path.Combine(Application.StartupPath, "update\\" + subFolder);

                            String file = Path.Combine(path, dt.Rows[i]["FileName"].ToString());
                            String agentPath = Path.Combine(path, "JZUpgradeAgent.exe");
                            if (File.Exists(agentPath))
                            {
                                try
                                {
                                    File.Delete(agentPath);
                                }
                                catch (Exception e)
                                {
                                    logger.Error("ɾ�� JZUpgradeAgent.exe ʧ�ܣ�ԭ��" + e.ToString());
                                }
                            }

                            if (File.Exists(file))
                            {
                                Boolean flag = uhc.UnZipFile(file);
                                if (flag)
                                {
                                    String[] files = Directory.GetFiles(path);
                                    foreach (String fileName in files)
                                    {
                                        if (dt.Rows[i]["FileType"].ToString() == "1" || dt.Rows[i]["FileType"].ToString() == "2"
                                            || dt.Rows[i]["FileType"].ToString() == "4" || dt.Rows[i]["FileType"].ToString() == "10")
                                        {
                                            //�����ɼ��ļ�ϵͳ����
                                            bUpdateFlag = bUpdateFlag & ProcessUpdateFile(fileName, Application.StartupPath);
                                        }
                                        else if (dt.Rows[i]["FileType"].ToString() == "3")
                                        {
                                            //����ϵͳ�������ݿ⻺�����
                                            LoadDBData(fileName);
                                        }
                                    }
                                    if (bUpdateFlag == true)
                                    {
                                        File.Delete(file);
                                        fileState = 1;
                                    }
                                }
                                else
                                {
                                    bUpdateFlag = false;
                                    logger.Error("�ļ���ѹʧ��,�ļ�����" + file);
                                }
                            }
                            if (i == dt.Rows.Count - 1)
                            {
                                UpdateProgressInfo("Ӧ�ø������", 100);
                            }
                            else
                            {
                                UpdateProgressInfo("����Ӧ�ø��¡���", (int)((100 / dt.Rows.Count) * (i + 1)));
                            }
                            uhc.FinishUpdate(dt.Rows[i]["ID"].ToString(), fileState);
                        }
                        if (bUpdateFlag == false)
                        {
                            MessageBox.Show("�����ļ�δ���³ɹ�����鿴������־�ֶ����¡�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                    }
                    if (mainFormRun)
                    {
                        try
                        {
                            ProcessStartInfo Info = new ProcessStartInfo();
                            Info.FileName = Path.Combine(Application.StartupPath, ApplicationHelper.GetApplicationName(updateFlag) + ".exe");
                            Info.Arguments = String.Concat("\"", Application.ProductName, "\" \"", Application.ExecutablePath, "\"");
                            Process.Start(Info);
                        }
                        catch (Exception exxx)
                        {
                            logger.Error(exxx.Message);
                        }
                    }
                }
                else
                {
                    UpdateProgressInfo("��ȡ�������", 100);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        private bool ProcessUpdateFile(String file, String path)
        {
            bool bSuccess = false;
            try
            {
                String fileName = Path.GetFileName(file).ToLower();
                if (fileName.EndsWith(".zip"))
                {
                    return true;
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
                    uhc.RunSqlCommand(sql);
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
                bSuccess = true;
            }
            catch (Exception ex)
            {
                bSuccess = false;
                logger.Error("�ļ�����ʧ��,�ļ�����" + file);
                logger.Error("�ļ�����ʧ��,������Ϣ��" + ex.Message);
            }
            return bSuccess;
        }

        private void LoadDBData(String file)
        {
            try
            {
                String fileName = Path.GetFileName(file).ToLower();
                if (fileName.EndsWith(".dat"))
                {
                    String bcp = "";
                    String item = Path.GetFileNameWithoutExtension(file);
                    if (uhc.ISDataBaseAttachFile)
                    {
                        bcp = "bcp " + uhc.DataBaseInstance + ".dbo." + item + "_update in " + file + " -c -T -S " + uhc.DataSource;
                    }
                    else
                    {
                        bcp = "bcp " + uhc.DataBaseInstance + ".dbo." + item + "_update in " + file + " -c -S " + uhc.DataSource +
                                " -U " + uhc.Username + " -P " + uhc.PassWord;
                    }
                    ExeCommand(bcp);
                    uhc.RunSqlCommand("exec sp_update");
                }

                File.Delete(file);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public Boolean ExeCommand(string commandText)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            Boolean strOutput = true;
            try
            {
                p.Start();
                p.StandardInput.WriteLine(commandText);
                p.StandardInput.WriteLine("exit");
                p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                p.Close();
            }
            catch
            {
                strOutput = false;
            }
            return strOutput;
        }

        private void Run()
        {

            try
            {
                //1 ����ϵͳ�ļ�+��ִ�У�
                //2 �ɼ�ϵͳ�ļ�+��ִ�У�
                //3 ����ϵͳ����+��ִ�У�
                //4 ����ϵͳ�ļ�+����+ִ�У� 
                //5 �ɼ�ϵͳ�ļ�+ִ��
                //6 ����ϵͳִ��
                //7 �ɼ�ϵͳִ��
                //8 ����ϵͳ�ļ�+ִ��
                //9 ��Һ�ŷ�����+ִ��
                //10 �쵼�����+ִ��
                if (updateFlag == "1" || updateFlag == "2" || updateFlag == "3")
                {
                    GetUpdate();
                }
                else if (updateFlag == "4")
                {
                    updateFlag = "1";
                    uhc = new UpdateHelperClient( updateFlag);
                    GetUpdate();
                    Thread.Sleep(600);
                    LoadUpdate();

                    updateFlag = "3";
                    uhc = new UpdateHelperClient( updateFlag);
                    GetUpdate();
                    Thread.Sleep(600);
                    LoadUpdate();
                }
                else if (updateFlag == "5")
                {
                    updateFlag = "2";
                    uhc = new UpdateHelperClient( updateFlag);
                    //GetUpdate();
                    //Thread.Sleep(600);
                    LoadUpdate();
                }
                else if (updateFlag == "6")
                {
                    updateFlag = "1";
                    uhc = new UpdateHelperClient( updateFlag);
                    LoadUpdate();

                    updateFlag = "3";
                    uhc = new UpdateHelperClient( updateFlag);
                    LoadUpdate();
                }
                else if (updateFlag == "7")
                {
                    updateFlag = "2";
                    uhc = new UpdateHelperClient( updateFlag);
                    LoadUpdate();
                }
                else if (updateFlag == "8")
                {
                    updateFlag = "1";
                    uhc = new UpdateHelperClient( updateFlag);
                    //GetUpdate();
                    //Thread.Sleep(600);
                    LoadUpdate();
                }
                else if (updateFlag == "9")
                {
                    updateFlag = "9";
                    uhc = new UpdateHelperClient( updateFlag);
                    GetUpdate();
                    Thread.Sleep(600);
                    LoadUpdate();
                }
                else if (updateFlag == "10")
                {
                    updateFlag = "10";
                    uhc = new UpdateHelperClient( updateFlag);
                    GetUpdate();
                    Thread.Sleep(600);
                    LoadUpdate();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            finally
            {
                Thread.Sleep(600);
                CloseForm();
            }
        }

        private void JZUpdate_Load(object sender, EventArgs e)
        {
            UpdateVisible(false);
            //UpdateProgressInfo("��ʼӦ�ø��¡���", 0);

            Thread t1 = new Thread(new ThreadStart(Run));
            t1.Start();
        }

        protected override void DefWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_COPYDATA:
                    User32Helper.COPYDATASTRUCT myStr = new User32Helper.COPYDATASTRUCT();
                    Type mytype = myStr.GetType();
                    myStr = (User32Helper.COPYDATASTRUCT)m.GetLParam(mytype);
                    if (myStr.lpData == "1")
                    {
                        UpdateVisible(true);
                    }
                    break;
                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }

        private void UpdateVisible(Boolean flag)
        {
            if (this.InvokeRequired)
            {
                SetVisibleHandler ssvh = new SetVisibleHandler(UpdateVisible);
                this.BeginInvoke(ssvh, flag);
            }
            else
            {
                if (flag)
                {
                    this.Left = Screen.PrimaryScreen.WorkingArea.Width - 300;
                    this.Top = Screen.PrimaryScreen.WorkingArea.Height - 120;
                    this.Width = 300;
                    this.Height = 120;
                    this.Visible = true;
                }
                else
                {
                    this.Left = -300;
                    this.Top = -120;
                    this.Width = 1;
                    this.Height = 1;
                    this.Visible = false;
                }
            }
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
                label.Text = title;
                progressBar.Value = step;
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

        private void SendUpdateStatus(String flag)
        {
            int hd = User32Helper.FindWindow(null, "��·������Ϣ����ϵͳ");
            if (hd > 0)
            {
                User32Helper.COPYDATASTRUCT cds;
                cds.dwData = (IntPtr)100;
                cds.lpData = flag;
                cds.cbData = 10;
                User32Helper.SendMessage(hd, WM_COPYDATA, 0, ref cds);
            }
        }
    }
}
