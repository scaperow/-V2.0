using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ShuXianCaiJiComponents;
using ShuXianCaiJiModule;
using System.IO;
using System.Threading;
using FileMD5Contrast;

namespace Kingrocket.CJ.Components
{
    public partial class LoginForm : Form
    {

        Logger _Log=null;
        public bool IsUpdate;
        CaijiCommHelper _CaijiCommHelper = null;

        public bool bNetWorkIsConnected = true;

        public bool ImagChang = true;

        string ErrMgs = string.Empty;

        FileMD5 _FileMD5 = null;


        SXCJModule _Module = null;

        private delegate void SetControlText(Control _Control, string text);

        private delegate void SetContrnlEnabled(Control _Control, bool IsEnabled);

        private delegate void SetTimeContrnlEnabled(System.Windows.Forms.Timer _Control, bool IsEnabled);

        public LoginForm(Logger log, CaijiCommHelper caijiCommHelper,
            SXCJModule jzmodel)
        {
            InitializeComponent();
            _Log = log;
            _CaijiCommHelper = caijiCommHelper;
            _Module = jzmodel;
            LoadUsers();
        }

        private void chkShowPwd_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPwd.Checked)
            {
                txtUserPwd.PasswordChar = char.MinValue;
                txtUserPwd.Text=txtUserPwd.Text;
            }
            else
            {
                txtUserPwd.PasswordChar='*';
                txtUserPwd.Text=txtUserPwd.Text;
            }
        }

        private void lblClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                DataSet d = Yqun.ClientConfigurationInfo.GetUserNames();
                d.Tables[0].Clear();
                d.AcceptChanges();

                Yqun.ClientConfigurationInfo.SetUserNames(d);
                this.cmbUserName.Items.Clear();
            }
            catch (Exception ex)
            {
                _Log.WriteLog(ex.StackTrace, true, false);
            }
        }


        public void MemberUser()
        {
            try
            {
                DataSet d = Yqun.ClientConfigurationInfo.GetUserNames();
                string name = this.cmbUserName.Text;
                DataRow[] rs = d.Tables[0].Select("NAME = " + "'" + name + "'");
                if (rs.Length == 0)
                {
                    DataRow r = d.Tables[0].NewRow();
                    r[0] = name;
                    d.Tables[0].Rows.Add(r);
                }

                d.AcceptChanges();
                Yqun.ClientConfigurationInfo.SetUserNames(d);
            }
            catch (Exception ex)
            {
                _Log.WriteLog(ex.StackTrace, true, false);
            }
        }

        void LoadUsers()
        {
            try
            {
                DataSet d = Yqun.ClientConfigurationInfo.GetUserNames();

                cmbUserName.Items.Clear();
                for (int i = 0; i < d.Tables[0].Rows.Count; i++)
                {
                    string name = d.Tables[0].Rows[i][0].ToString();
                    this.cmbUserName.Items.Add(name);
                }

                if (this.cmbUserName.Items.Count > 0)
                {
                    this.cmbUserName.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                _Log.WriteLog(ex.StackTrace, true, false);
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

            #region 更新或执行更新\工管中心MD5验证
            Thread _Thread = new Thread(new ThreadStart(InitUpdate));
            _Thread.Start();
            #endregion
        }

        private void InitUpdate()
        {
            SetThreadControlEnabled(btnLogin, false);
            if (_CaijiCommHelper.TestServerConnection())
            {
                bNetWorkIsConnected=true;
            }
            else
            {
                bNetWorkIsConnected=false;
            }

            SetThreadControlText(btnLogin, "更新");
        ChangSYS:

            SetThreadControlText(lblUpdateStatus, "初始化连接服务器.....");
            if (bNetWorkIsConnected)
            {
                JZUpgrade.UpdateHelperClient uhc = new JZUpgrade.UpdateHelperClient("5");
                if (!uhc.IsNewest())
                {

                    DataTable dt = uhc.GetNewUpdate();
                    bool flag = false;
                    try
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                SetThreadControlText(lblUpdateStatus, "正在获取更新...");
                                String subFolder = uhc.GetSubFolder(dt.Rows[i]["FileType"].ToString());
                                String path = Path.Combine(Application.StartupPath, "update\\" + subFolder);
                                if (!Directory.Exists(path))
                                {
                                    Directory.CreateDirectory(path);
                                }
                                String file = Path.Combine(path, dt.Rows[i]["FileName"].ToString());
                                byte[] bytes = uhc.GetUpdateFileByID(new Guid(dt.Rows[i]["ID"].ToString()));
                                SetThreadControlText(lblUpdateStatus, "正在保存更新...");
                                if (bytes != null)
                                {
                                    File.WriteAllBytes(file, bytes);
                                    uhc.SaveUpdateInfo(dt.Rows[i]["ID"].ToString(), dt.Rows[i]["FileName"].ToString(),
                                        dt.Rows[i]["FileType"].ToString(),
                                        System.Convert.ToDateTime(dt.Rows[i]["CreatedServerTime"]).ToString("yyyy-MM-dd HH:mm:ss.fff"));
                                }
                                if (i == dt.Rows.Count - 1)
                                {
                                    SetThreadControlText(lblUpdateStatus, "更新文件获取成功！");
                                }
                            }

                            SetThreadControlText(lblUpdateStatus, "获取更新完毕");

                            flag = true;
                        }
                        else
                        {
                            SetThreadControlText(lblUpdateStatus, "获取更新完毕");

                            flag = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        _Log.WriteLog(ex.StackTrace, false, true);
                    }
                    timerUpdate.Enabled = false;
                    PICUpdateStatus.BackgroundImage = Properties.Resources.dot_green;
                    IsUpdate = false;
                    if (flag == true)
                    {

                        SetThreadControlText(lblUpdateStatus, "请点击更新按钮开始更新");
                        SetThreadControlEnabled(btnLogin, true);
                        IsUpdate = true;
                    }
                    else
                    {
                        PICUpdateStatus.BackgroundImage = Properties.Resources.dot_red;
                        SetThreadControlText(lblUpdateStatus, "更新失败,请和管理员联系");
                        IsUpdate = false;
                    }
                }
                else
                {


                    #region 工管中心MD5验证

                    SetThreadControlText(lblUpdateStatus, "工管中心文件验证.....");
                    if (_CaijiCommHelper.GetIsRemotCheck())
                    {
                        string ErrMsg = string.Empty;
                        if (!GetMD5(_CaijiCommHelper.GetECode(_Module.SpecialSetting.MachineCode), out ErrMsg))
                        {
                            MessageBox.Show(ErrMsg, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            SetThreadControlText(lblUpdateStatus, "系统验证失败，请联系管理员!");
                            return;
                        }
                        else
                        {
                            _Log.WriteLog("系统验证成功", false, false);
                        }
                    }
                    #endregion

                    SetThreadControlText(btnLogin, "登陆");
                    SetThreadControlEnabled(btnLogin, true);
                    IsUpdate = false;
                    PICUpdateStatus.Image = Kingrocket.CJ.Components.Properties.Resources.dot_green;
                    SetThreadControlText(lblUpdateStatus, "当前是最新版本");
                }
            }
            else
            {
                ChangAddress();
                if (bNetWorkIsConnected)
                {
                    goto ChangSYS;
                }
                else
                {
                    SetThreadControlEnabled(btnLogin,true);
                    SetThreadControlText(btnLogin, "登陆");
                    Yqun.Common.ContextCache.ApplicationContext.Current.ISLocalService = true;
                    SetThreadControlText(lblUpdateStatus, "网络连接失败，转到本地登录,请联系管理员");
                    Yqun.Common.ContextCache.ApplicationContext.Current.ISLocalService = true;
                }
            }
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            try
            {
                if (bNetWorkIsConnected)
                {
                    if (ImagChang)
                    {
                        PICUpdateStatus.Image = Kingrocket.CJ.Components.Properties.Resources.dot_orange;
                        ImagChang = false;
                    }
                    else
                    {
                        PICUpdateStatus.Image = Kingrocket.CJ.Components.Properties.Resources.dot_green;
                        ImagChang = true;
                    }
                }
                else
                {
                    PICUpdateStatus.Image = Kingrocket.CJ.Components.Properties.Resources.dot_red;
                }
            }
            catch (Exception ex)
            {
                _Log.WriteLog(ex.StackTrace, true, false);
            }
        }


        public void ChangAddress()
        {
            if (!Yqun.Common.ContextCache.ApplicationContext.Current.ISLocalService)
            {
                try
                {
                    SetThreadControlText(lblUpdateStatus,"切换到备用地址登录中...");
                    string EndPointAddress = BizCommon.ConfigHelper.GetEndpointAddress("TransferServiceEndPoint").ToString();
                    string NetTcpDomainAddress = BizCommon.ConfigHelper.GetSysXmlValue("NetTcpDomainAddress");
                    string strAddressM = EndPointAddress.Replace("net.tcp://", "");
                    strAddressM = strAddressM.Substring(0, strAddressM.IndexOf('/'));
                    string strAddressE = EndPointAddress.Replace("net.tcp://", "").Replace(strAddressM, "");
                    if (!string.IsNullOrEmpty(NetTcpDomainAddress))//配置了域名地址，开始判断域名地址
                    {
                        EndPointAddress = "net.tcp://" + NetTcpDomainAddress + strAddressE;
                        BizCommon.ConfigHelper.SetEndpointAddress("TransferServiceEndPoint", EndPointAddress);
                        if (Yqun.Services.Agent.TestNetwork() == true)//域名可以连接上
                        {
                            bNetWorkIsConnected = true;
                        }
                        else
                        {
                            string NetTcpIPAddress = BizCommon.ConfigHelper.GetSysXmlValue("NetTcpIPAddress");
                            if (!string.IsNullOrEmpty(NetTcpIPAddress))
                            {
                                EndPointAddress = "net.tcp://" + NetTcpIPAddress + strAddressE;
                                BizCommon.ConfigHelper.SetEndpointAddress("TransferServiceEndPoint", EndPointAddress);
                                if (Yqun.Services.Agent.TestNetwork() == true)
                                {
                                    bNetWorkIsConnected = true;
                                }
                            }
                            else
                            {
                                bNetWorkIsConnected = false;
                                EndPointAddress = "net.tcp://" + strAddressM + strAddressE;
                                BizCommon.ConfigHelper.SetEndpointAddress("TransferServiceEndPoint", EndPointAddress);
                            }
                        }
                    }
                    else
                    {
                        string NetTcpIPAddress = BizCommon.ConfigHelper.GetSysXmlValue("NetTcpIPAddress");
                        if (!string.IsNullOrEmpty(NetTcpIPAddress))
                        {
                            EndPointAddress = "net.tcp://" + NetTcpIPAddress + strAddressE;
                            BizCommon.ConfigHelper.SetEndpointAddress("TransferServiceEndPoint", EndPointAddress);
                            if (Yqun.Services.Agent.TestNetwork() == true)
                            {
                                bNetWorkIsConnected = true;
                            }
                        }
                        else
                        {
                            bNetWorkIsConnected = false;
                            EndPointAddress = "net.tcp://" + strAddressM + strAddressE;
                            BizCommon.ConfigHelper.SetEndpointAddress("TransferServiceEndPoint", EndPointAddress);
                        }


                    }
                }
                catch(Exception ex)
                {
                    _Log.WriteLog(ex.StackTrace, false, true);
                }
            }
        }

        private void SetThreadControlText(Control _Control, string text)
        {
            try
            {
                if (_Control.InvokeRequired)
                {
                    _Control.Invoke(new SetControlText(SetThreadControlText), new object[] { _Control, text });
                }
                else
                {
                    _Control.Text = text;
                }
            }
            catch (Exception ex)
            {
                _Log.WriteLog(ex.StackTrace, true, false);
            }
        }

        private void SetThreadControlEnabled(Control _Control, bool isEnabled)
        {
            try
            {
                if (_Control.InvokeRequired)
                {
                    _Control.Invoke(new SetContrnlEnabled(SetThreadControlEnabled), new object[] { _Control, isEnabled });
                }
                else
                {
                    _Control.Enabled = isEnabled;
                }
            }
            catch (Exception ex)
            {
                _Log.WriteLog(ex.StackTrace, true, false);
            }
        }

        public bool GetMD5(string EMachinCode,out string ErrMsg)
        {
            try
            {
                ErrMsg = string.Empty;
                if (_FileMD5 == null)
                {
                    _FileMD5 = new FileMD5();
                }
                return _FileMD5.FileMD5VS(EMachinCode, 3, out ErrMsg);
            }
            catch (Exception ex)
            {
                _Log.WriteLog(ex.StackTrace, true, false);
                ErrMsg = "程序内部错误，请联系管理！";
            }
            return false;
        }
    }
}
