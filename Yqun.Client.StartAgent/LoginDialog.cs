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
        //ʹ��log4net.dll��־�ӿ�ʵ����־��¼
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

        #region �û����Զ����

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

        #endregion �û����Զ����



        /// <summary>
        /// Ĭ�������޸�
        /// </summary>
        /// <returns>true���벻�ǳ�ʼ������޸ĳ�ʼ����ɹ���false�û�ȡ����ʼ�����޸ģ������˳�</returns>
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
            lblUpdateMsg.Text = "׼��������������...";
            TimerShowType = 3;//������������
            tmShow.AutoReset = true;
            tmShow.Elapsed += new System.Timers.ElapsedEventHandler(tmShow_Elapsed);
            tmShow.Interval = 1000;
            tmShow.Enabled = true;
            tmShow.Start();

            ThreadStart threadStart = new ThreadStart(CheckUpdate);
            Thread thread = new Thread(threadStart);
            thread.Start(); //�������߳�
        }

        void tmShow_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            string strShowMsg = string.Empty;
            if (TimerShowType == 1)
            {
                strShowMsg = "���ڼ�����...";
            }
            else if (TimerShowType == 2)
            {
                strShowMsg = "���ڻ�ȡ����...";
            }
            else if (TimerShowType == 3)
            {
                strShowMsg = "���ڲ�����������...";
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
            lblUpdateMsg.Text = "���ڲ�����������...";

            //Thread.Sleep(6 * 1000);//������
            //JZUpgrade.Logger log = new JZUpgrade.Logger();
            //log.Logfolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log");
            //log.IsUseLog = true;
            //log.QueueBufferSize = 5;
            JZUpgrade.UpdateHelperClient uhc = new JZUpgrade.UpdateHelperClient("8");
            if (Yqun.Services.Agent.TestNetwork() == true)
            {
                TimerShowType = 1;//������
                lblUpdateMsg.Text = "���ڼ�����...";
                tmShow.Enabled = true;
                tmShow.Start();
                if (uhc.IsNewest() == false)
                {
                    #region �и���
                    tmShow.Stop();
                    tmShow.Enabled = false;
                    text_Pass.Enabled = false;
                    com_User.Enabled = false;
                    Boolean flag = true;

                    tmShow.Stop();
                    tmShow.Enabled = false;
                    TimerShowType = 2;//���ظ���
                    //tmShow = new System.Timers.Timer();
                    picUpdateMsg.BackgroundImage = Properties.Resources.dot_orange;
                    lblUpdateMsg.Text = "���ڻ�ȡ����...";
                    tmShow.Enabled = true;
                    tmShow.Start();
                    //Thread.Sleep(6 * 1000);//������
                    #region ���ظ����ļ�
                    try
                    {
                        DataTable dt = uhc.GetNewUpdate();
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                lblUpdateMsg.Text = "���ڻ�ȡ����...";
                                String subFolder = uhc.GetSubFolder(dt.Rows[i]["FileType"].ToString());
                                String path = Path.Combine(Application.StartupPath, "update\\" + subFolder);
                                if (!Directory.Exists(path))
                                {
                                    Directory.CreateDirectory(path);
                                }
                                String file = Path.Combine(path, dt.Rows[i]["FileName"].ToString());
                                logger.Info(file);
                                byte[] bytes = uhc.GetUpdateFileByID(new Guid(dt.Rows[i]["ID"].ToString()));
                                lblUpdateMsg.Text = "���ڱ������...";
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
                                    lblUpdateMsg.Text = "��ȡ�������";
                                }
                            }

                        }
                        else
                        {
                            lblUpdateMsg.Text = "��ȡ�������";
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
                        lblUpdateMsg.Text = "�������°�ť��ʼ����";
                        button_OK.Text = "����";
                        button_OK.Enabled = true;
                        IsUpdate = true;
                    }
                    else
                    {
                        picUpdateMsg.BackgroundImage = Properties.Resources.dot_red;
                        lblUpdateMsg.Text = "����ʧ��,��͹���Ա��ϵ";
                        IsUpdate = false;
                    }
                    #endregion
                }
                else
                {
                    tmShow.Stop();
                    tmShow.Enabled = false;
                    picUpdateMsg.BackgroundImage = Properties.Resources.dot_green;
                    lblUpdateMsg.Text = "��ǰϵͳ�����°汾,���¼";
                    //lblUpdateMsg.Visible = false;
                    text_Pass.Enabled = true;
                    com_User.Enabled = true;
                    button_OK.Text = "��¼";
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
                button_OK.Text = "��¼";
                button_OK.Enabled = true;
                IsUpdate = false;
                picUpdateMsg.BackgroundImage = Properties.Resources.dot_red;
                lblUpdateMsg.Text = "��������ʧ��,������������";
            }
        }

        private void picSystemSetting_Click(object sender, EventArgs e)
        {
            DataSourceDialog DataSourceDialog = new DataSourceDialog();
            DataSourceDialog.ShowDialog();
        }


        /// <summary>
        /// ����û���¼��¼
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