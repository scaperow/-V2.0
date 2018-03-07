using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YqunMainAppBase;
using System.Drawing;
using System.Diagnostics;
using ShuXianCaiJiModule;
using System.Windows.Forms;
using ShuXianCaiJiComponents;
using System.Data;
using Kingrocket.CJ.Components;
using System.IO;

namespace Kingrocket.CJ.Components
{
    public class StartHandle
    {
        private static Logger log = null;
        delegate void DelShowFrom();
        delegate void DelCloseForm();
        delegate void DeLSetText(string text);



        /// <summary>
        /// 初始化等待窗体实例
        /// </summary>
         YqunMainAppBase.SplashScreen _SplashScreen = null;
        /// <summary>
        /// 设置等待窗体状态
        /// </summary>
        /// <param name="text"></param>
        public void SetText(string text)
        {
            try
            {
                if (_SplashScreen.InvokeRequired)
                {
                    _SplashScreen.Invoke(new DeLSetText(SetText), new object[] { text });
                }
                else
                {
                    _SplashScreen.SetStatus = text;
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.StackTrace, true, true);
            }

        }

        /// <summary>
        /// 打开等待窗体
        /// </summary>
         public void ShowForm()
        {
            if (_SplashScreen == null)
            {
                _SplashScreen = SplashScreen.Current;
                _SplashScreen.SetTransparentKey = Color.Fuchsia;
                _SplashScreen.SetBackgroundImage = Kingrocket.CJ.Components.Properties.Resources.Splash;
                _SplashScreen.SetFade = true;//设置淡入淡出效果
            }
            _SplashScreen.ShowSplashScreen();
        }

        /// <summary>
        ///关闭等待窗体 
        /// </summary>
         public void CloseFrom()
        {
            if (_SplashScreen.InvokeRequired)
            {
                _SplashScreen.Invoke(new DelCloseForm(CloseFrom));
            }
            else
            {
                _SplashScreen.CloseSplashScreen();
                _SplashScreen.Dispose();
            }
            _SplashScreen = null;
        }

        /// <summary>
        /// 升级程序调用
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
         public bool IsRuningProcess(String name)
        {
            Process[] processes = Process.GetProcesses();
            foreach (Process p in processes)
            {
                if (p.ProcessName.ToLower() == name.ToLower())
                {
                    return true;
                }
            }
            return false;
        }


         public void StartInit()
        {
            log = new Logger(Path.Combine(Directory.GetCurrentDirectory(), "Log"));
            /// <summary>
            /// 登录实例
            /// </summary>
            LoginForm _LoginForm = null;

            /// <summary>
            /// 获取调用实例
            /// </summary>
            CaijiCommHelper _CaijiCommHelper = null;

            /// <summary>
            /// 试验初始化配置信息
            /// </summary>
            SXCJModule _Module = null;

            try
            {
                _Module = CalHelper.LoadModule();
                _CaijiCommHelper = new CaijiCommHelper(log, _Module);

                _LoginForm = new LoginForm(log, _CaijiCommHelper, _Module);
            ReLogin:
                if (DialogResult.OK == _LoginForm.ShowDialog())
                {
                    if (_LoginForm.IsUpdate && !IsRuningProcess("JZUpgradeAgent"))
                    {
                        JZUpgrade.UpdateAlert ua = new JZUpgrade.UpdateAlert(2);
                        if (ua.ShowDialog() != DialogResult.OK)
                        {
                            goto ReLogin;
                        }

                        ProcessStartInfo Info = new ProcessStartInfo();
                        Info.CreateNoWindow = false;
                        Info.UseShellExecute = true;
                        Info.FileName = Path.Combine(Application.StartupPath, "JZUpgrade.exe");
                        Info.Arguments = "\"5\"";
                        Process.Start(Info);
                        _LoginForm.Enabled = false;
                        _LoginForm.lblUpdateStatus.Text = "正在更新...";
                        goto ReLogin;
                    }

                    #region 用户登录
                    if (_LoginForm.bNetWorkIsConnected)
                    {
                        #region 在线登陆
                        ShowForm();
                        SetText("用户登录中.......");
                        try
                        {
                            string msg = _CaijiCommHelper.Login(_LoginForm.cmbUserName.Text, _LoginForm.txtUserPwd.Text, _Module.SpecialSetting.MachineCode, true);
                            if (msg.ToLower().Trim() != "true")
                            {
                                CloseFrom();
                                if (MessageBox.Show(msg, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                                {

                                    goto ReLogin;
                                }
                                else
                                {
                                    Application.Exit();
                                    return;
                                }
                            }
                            else
                            {
                                #region 网络登录成功

                                _LoginForm.timerUpdate.Stop();

                                #region 同步本地用户

                                SetText("同步本地用户.......");
                                _CaijiCommHelper.SysUserInfo(Yqun.Common.ContextCache.ApplicationContext.Current.UserName,
                                    Yqun.Common.ContextCache.ApplicationContext.Current.Password,
                                    Yqun.Common.ContextCache.ApplicationContext.Current.UserCode,
                                    Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code);
                                #endregion

                                #region 同步本地模板
                                SetText("初始化用户配置信息.......");

                                SetText("同步本地模板.......");
                                _CaijiCommHelper.UpdateLocalModelInfo(_Module.SpecialSetting.TestRoomCode);
                                #endregion

                                if (!Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
                                {
                                    #region 同步本地配置信息

                                    try
                                    {
                                        SetText("检查本地配置信息状态.......");
                                        switch (_CaijiCommHelper.GetClientConfigStatus(_Module.SpecialSetting.MachineCode))
                                        {
                                            case -1:
                                                SetText("本地配置信息状态检查失败，请联系管理员.......");
                                                break;
                                            case 0:
                                                //SetText("同步本地配置信息.......");
                                                //_CaijiCommHelper.UploadClientConfig(_Module.SpecialSetting.TestRoomCode, _Module.SpecialSetting.MachineCode, Newtonsoft.Json.JsonConvert.SerializeObject(_Module.SpecialSetting));
                                                MessageBox.Show("本地设备信息未在服务上注册，请联系管理员！", "本地配置提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                                CloseFrom();
                                                goto ReLogin;
                                            case 1:
                                                SetText("同步本地配置信息.......");
                                                _CaijiCommHelper.UpDateClientConfig(_Module.SpecialSetting.MachineCode, Newtonsoft.Json.JsonConvert.SerializeObject(_Module.SpecialSetting));
                                                break;
                                            case 2:
                                                SetText("同步本地配置信息.......");
                                                _CaijiCommHelper.GetClientConfig(ref _Module, _Module.SpecialSetting.MachineCode);
                                                break;
                                        }
                                        SetText("同步本地配置信息完成.......");
                                    }
                                    catch (Exception exSysConfig)
                                    {
                                        log.WriteLog(exSysConfig.Message + System.Environment.NewLine + exSysConfig.TargetSite, true, true);
                                    }

                                    #endregion
                                }
                                #region 上传本地数据

                                if (!Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
                                {
                                    SetText("上传本地数据中.......");
                                    if (_CaijiCommHelper.UpdateLocalDateToServer())
                                    {
                                        SetText("上传本地数据成功.......");
                                    }
                                    else
                                    {

                                        SetText("上传本地数据失败，下次启动将继续上传.......");
                                    }
                                }

                                #endregion

                                #region 记录历史登陆
                                _LoginForm.MemberUser();
                                #endregion

                                #endregion
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("网络连接失败！已经转至本地服务登录！" + ex.Message, "登录提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            Yqun.Common.ContextCache.ApplicationContext.Current.ISLocalService = true;
                        }
                        #endregion
                    }
                    else
                    {
                        #region 本地用户登录
                        ShowForm();
                        SetText("用户登录中.......");
                        SetText("读取本地登录用户信息.......");
                        DataTable _DataTable = new DataTable();

                        _DataTable = _CaijiCommHelper.GetUserInfo(_LoginForm.cmbUserName.Text, _LoginForm.txtUserPwd.Text);

                        if (_DataTable != null && _DataTable.Rows.Count > 0)
                        {
                            Yqun.Common.ContextCache.ApplicationContext.Current.UserName = _LoginForm.cmbUserName.Text;
                            Yqun.Common.ContextCache.ApplicationContext.Current.Password = _LoginForm.txtUserPwd.Text;
                            Yqun.Common.ContextCache.ApplicationContext.Current.UserCode = _DataTable.Rows[0]["UserCode"].ToString();
                            Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code = _DataTable.Rows[0]["UserTestCode"].ToString();
                            Yqun.Common.ContextCache.ApplicationContext.Current.ISLocalService = true;
                            _Module = CalHelper.LoadModule();
                        }
                        else
                        {
                            if (MessageBox.Show("用户名或密码错误，请查证后在登录！", "登录错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                            {

                                goto ReLogin;
                            }
                            else
                            {
                                Application.Exit();
                                return;
                            }
                        }
                        #endregion
                    }

                    #region 释放资源
                    if (_SplashScreen != null)
                    {
                        CloseFrom();
                    }
                    #endregion


                    #endregion

                    Application.EnableVisualStyles();
                    //Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainForm(log, _Module, _CaijiCommHelper));

                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.StackTrace, true, false);
            }
        }
    }
}
