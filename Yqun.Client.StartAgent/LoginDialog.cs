using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Yqun.Common.ContextCache;
using Yqun.Common.Encoder;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Collections;
using System.Net;
using Yqun.Bases;

namespace Yqun.MainUI
{
    public partial class LoginDialog : Form
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public bool IsUpdate = false;
        private int TimerShowType = 0;
        System.Timers.Timer tmShow = new System.Timers.Timer();
        public LoginDialog()
        {
            InitializeComponent();

            string[] ps = new string[]{
                                        SystemFolder.DataFolder,
                                        SystemFolder.DockingConfig,
                                        SystemFolder.DataSourceFolder
                                      };

            for (int i = 0; i < ps.Length; i++)
            {
                if (!Directory.Exists(ps[i]))
                {
                    Directory.CreateDirectory(ps[i]);
                }
            }

            SetDefaultIcon();
            LoadUsers();

            log4net.Config.XmlConfigurator.Configure();
        }

        internal bool Login(Boolean IsRemote)
        {
            string UserName = com_User.Text;
            string Password = text_Pass.Text;

            Yqun.Common.ContextCache.ApplicationContext.Current.ISLocalService = !IsRemote;

            return Yqun.Services.Agent.LoginProcess(UserName, Password, IsRemote);
        }

        internal bool TestDbConnection(Boolean IsRemote)
        {
            return Yqun.Services.Agent.TestDbConnection(IsRemote);
        }

        private void SetDefaultIcon()
        {
            this.Icon = Yqun.ClientConfigurationInfo.GetDefaultIcon();
        }

        #region 用户名自动完成

        public static void AutoCompleteCombo(ComboBox cbo, KeyEventArgs e)
        {
            String sTypedText;
            Int32 iFoundIndex;
            Object oFoundItem;
            String sFoundText;
            String sAppendText;

            switch (e.KeyCode)
            {
                case Keys.Back:
                    return;
                case Keys.Left:
                    return;
                case Keys.Right:
                    return;
                case Keys.Tab:
                    return;
                case Keys.Up:
                    return;
                case Keys.Delete:
                    return;
                case Keys.Down:
                    return;
            }

            sTypedText = cbo.Text;
            iFoundIndex = cbo.FindString(sTypedText);

            if (iFoundIndex >= 0)
            {
                oFoundItem = cbo.Items[iFoundIndex];
                sFoundText = cbo.GetItemText(oFoundItem);
                sAppendText = sFoundText.Substring(sTypedText.Length);
                cbo.Text = sTypedText.ToString() + sAppendText.ToString();

                cbo.SelectionStart = sTypedText.Length;
                cbo.SelectionLength = sAppendText.Length;
            }
        }

        private void comboBox_Apps_KeyUp(object sender, KeyEventArgs e)
        {
            AutoCompleteCombo(this.com_User, e);
        }

        internal void MemberUser()
        {
            DataSet d = Yqun.ClientConfigurationInfo.GetUserNames();
            string name = this.com_User.Text;
            DataRow[] rs = d.Tables[0].Select("NAME = " + "'" + name + "'");
            DataRow r;
            if (rs.Length == 0)
            {
                r = d.Tables[0].NewRow();
                d.Tables[0].Rows.Add(r);
            }
            else
            {
                r = rs[0];
            }

            r[0] = name;
            r[1] = DateTime.Now.ToString();
            d.AcceptChanges();

            d.Tables[0].DefaultView.Sort = "SCTS DESC";
            DataTable dt = d.Tables[0].DefaultView.ToTable();
            d.Tables.Clear();
            d.Tables.Add(dt);

            Yqun.ClientConfigurationInfo.SetUserNames(d);
        }

        void LoadUsers()
        {
            DataSet d = Yqun.ClientConfigurationInfo.GetUserNames();

            com_User.Items.Clear();
            for (int i = 0; i < d.Tables[0].Rows.Count; i++)
            {
                string name = d.Tables[0].Rows[i][0].ToString();
                this.com_User.Items.Add(name);
            }

            if (this.com_User.Items.Count > 0)
            {
                this.com_User.SelectedIndex = 0;
            }
        }

        #endregion 用户名自动完成



        /// <summary>
        /// 默认密码修改
        /// </summary>
        /// <returns>true密码不是初始密码或修改初始密码成功，false用户取消初始密码修改，程序退出</returns>
        internal bool ChangDefaultPwd()
        {
            if (text_Pass.Text != "1" && text_Pass.Text != "111111" && text_Pass.Text != "888888")
            {
                return true;
            }
            ChangeDefaultPasswordForm changDefaultpwdForm = new ChangeDefaultPasswordForm();
        changPwd:
            if (changDefaultpwdForm.ShowDialog() == DialogResult.OK)
            {
                if (changDefaultpwdForm.Changpwd(text_Pass.Text))
                {
                    return true;
                }
                else
                {
                    goto changPwd;
                }
            }
            return false;
        }

        private void LoginDialog_Load(object sender, EventArgs e)
        {
            button_OK.Enabled = false;
            //lblUpdateMsg.Visible = true;
            picUpdateMsg.BackgroundImage = Properties.Resources.dot_red;
            lblUpdateMsg.Text = "准备测试网络连接...";
            TimerShowType = 3;//测试网络连接
            tmShow.AutoReset = true;
            tmShow.Elapsed += new System.Timers.ElapsedEventHandler(tmShow_Elapsed);
            tmShow.Interval = 1000;
            tmShow.Enabled = true;
            tmShow.Start();

            ThreadStart threadStart = new ThreadStart(CheckUpdate);
            Thread thread = new Thread(threadStart);
            thread.Start(); //启动新线程
        }

        void tmShow_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            string strShowMsg = string.Empty;
            if (TimerShowType == 1)
            {
                strShowMsg = "正在检查更新...";
            }
            else if (TimerShowType == 2)
            {
                strShowMsg = "正在获取更新...";
            }
            else if (TimerShowType == 3)
            {
                strShowMsg = "正在测试网络连接...";
            }
            int ShowMsgLenth = strShowMsg.Length;
            int CMsgLenth = lblUpdateMsg.Text.Length;
            if (CMsgLenth < ShowMsgLenth)
            {
                CMsgLenth++;
            }
            else
            {
                CMsgLenth = ShowMsgLenth - 3;
            }
            if (!string.IsNullOrEmpty(strShowMsg))
            {
                lblUpdateMsg.Text = strShowMsg.Substring(0, CMsgLenth);
            }
            //logger.Info(string.Format("TimerShowType:{0}  lblUpdateMsg.Text:{1}", TimerShowType, lblUpdateMsg.Text));
        }
        private void CheckUpdate()
        {
            picUpdateMsg.BackgroundImage = Properties.Resources.dot_orange;
            lblUpdateMsg.Text = "正在测试网络连接...";

            //Thread.Sleep(6 * 1000);//测试用
            //JZUpgrade.Logger log = new JZUpgrade.Logger();
            //log.Logfolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log");
            //log.IsUseLog = true;
            //log.QueueBufferSize = 5;
            JZUpgrade.UpdateHelperClient uhc = new JZUpgrade.UpdateHelperClient("8");
            if (Yqun.Services.Agent.TestNetwork() == true)
            {
                TimerShowType = 1;//检查更新
                lblUpdateMsg.Text = "正在检查更新...";
                tmShow.Enabled = true;
                tmShow.Start();
                if (uhc.IsNewest() == false)
                {
                    #region 有更新
                    tmShow.Stop();
                    tmShow.Enabled = false;
                    text_Pass.Enabled = false;
                    com_User.Enabled = false;
                    Boolean flag = true;

                    tmShow.Stop();
                    tmShow.Enabled = false;
                    TimerShowType = 2;//下载更新
                    //tmShow = new System.Timers.Timer();
                    picUpdateMsg.BackgroundImage = Properties.Resources.dot_orange;
                    lblUpdateMsg.Text = "正在获取更新...";
                    tmShow.Enabled = true;
                    tmShow.Start();
                    //Thread.Sleep(6 * 1000);//测试用
                    #region 下载更新文件
                    try
                    {
                        DataTable dt = uhc.GetNewUpdate();
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                lblUpdateMsg.Text = "正在获取更新...";
                                String subFolder = uhc.GetSubFolder(dt.Rows[i]["FileType"].ToString());
                                String path = Path.Combine(Application.StartupPath, "update\\" + subFolder);
                                if (!Directory.Exists(path))
                                {
                                    Directory.CreateDirectory(path);
                                }
                                String file = Path.Combine(path, dt.Rows[i]["FileName"].ToString());
                                logger.Info(file);
                                byte[] bytes = uhc.GetUpdateFileByID(new Guid(dt.Rows[i]["ID"].ToString()));
                                lblUpdateMsg.Text = "正在保存更新...";
                                if (bytes != null)
                                {
                                    File.WriteAllBytes(file, bytes);
                                    uhc.SaveUpdateInfo(dt.Rows[i]["ID"].ToString(), dt.Rows[i]["FileName"].ToString(),
                                        dt.Rows[i]["FileType"].ToString(),
                                        System.Convert.ToDateTime(dt.Rows[i]["CreatedServerTime"]).ToString("yyyy-MM-dd HH:mm:ss.fff"));
                                    flag = true;
                                }
                                if (i == dt.Rows.Count - 1)
                                {
                                    lblUpdateMsg.Text = "获取更新完毕";
                                }
                            }

                        }
                        else
                        {
                            lblUpdateMsg.Text = "获取更新完毕";
                        }
                    }
                    catch (Exception ex)
                    {
                        flag = false;
                        logger.Error(ex.Message);
                    }
                    #endregion
                    tmShow.Stop();
                    tmShow.Enabled = false;
                    if (flag == true)
                    {
                        picUpdateMsg.BackgroundImage = Properties.Resources.dot_orange;
                        lblUpdateMsg.Text = "请点击更新按钮开始更新";
                        button_OK.Text = "更新";
                        button_OK.Enabled = true;
                        IsUpdate = true;
                    }
                    else
                    {
                        picUpdateMsg.BackgroundImage = Properties.Resources.dot_red;
                        lblUpdateMsg.Text = "更新失败,请和管理员联系";
                        IsUpdate = false;
                    }
                    #endregion
                }
                else
                {
                    tmShow.Stop();
                    tmShow.Enabled = false;
                    picUpdateMsg.BackgroundImage = Properties.Resources.dot_green;
                    lblUpdateMsg.Text = "当前系统是最新版本,请登录";
                    //lblUpdateMsg.Visible = false;
                    text_Pass.Enabled = true;
                    com_User.Enabled = true;
                    button_OK.Text = "登录";
                    button_OK.Enabled = true;
                    IsUpdate = false;
                }
            }
            else
            {
                tmShow.Stop();
                tmShow.Enabled = false;
                text_Pass.Enabled = true;
                com_User.Enabled = true;
                button_OK.Text = "登录";
                button_OK.Enabled = true;
                IsUpdate = false;
                picUpdateMsg.BackgroundImage = Properties.Resources.dot_red;
                lblUpdateMsg.Text = "网络连接失败,请检查网络配置";
            }
        }

        private void picSystemSetting_Click(object sender, EventArgs e)
        {
            DataSourceDialog DataSourceDialog = new DataSourceDialog();
            DataSourceDialog.ShowDialog();
        }


        /// <summary>
        /// 清除用户登录记录
        /// </summary>
        private void picClearUser_Click(object sender, EventArgs e)
        {
            DataSet d = Yqun.ClientConfigurationInfo.GetUserNames();
            d.Tables[0].Clear();
            d.AcceptChanges();

            Yqun.ClientConfigurationInfo.SetUserNames(d);
            this.com_User.Items.Clear();

        }

    }
}