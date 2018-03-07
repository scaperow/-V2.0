using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using Yqun.Bases;
using YqunMainAppBase;
using System.Threading;
using Yqun.Common.ContextCache;
using System.Drawing;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Yqun.Bases.Exceptions;
using Yqun.Services;
using Yqun.Interfaces;

namespace Yqun.MainUI
{
    static class Program
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static SplashScreen splash;

        [STAThread]
        static void Main(string[] args)
        {
            try
            {

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Registration registration = new Registration();
                string filename = Path.Combine(Application.StartupPath, "License.dat");
                bool AuthResult = false;
                if (!(File.Exists(filename) && registration.AuthSerial(filename)))
                {
                    if (DialogResult.OK == registration.ShowDialog())
                    {
                        AuthResult = true;
                    }
                }
                else
                {
                    AuthResult = true;
                }

                if (AuthResult)
                {
                //JZUpgrade.Logger log = new JZUpgrade.Logger();
                //log.Logfolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log");
                //log.IsUseLog = true;
                //log.QueueBufferSize = 5;
                //JZUpgrade.UpdateHelperClient uhc = new JZUpgrade.UpdateHelperClient(log, "8");

                    //if (!ProcessHelper.IsRuningProcess("JZUpgradeAgent"))
                //{
                //    ProcessStartInfo Info = new ProcessStartInfo();
                //    Info.CreateNoWindow = false;
                //    Info.UseShellExecute = true;
                //    Info.FileName = Path.Combine(Application.StartupPath, "JZUpgrade.exe");
                //    Info.Arguments = "\"8\"";
                //    //1 管理系统文件+不执行，2 采集系统文件+不执行，3 管理系统数据+不执行，
                //    //4 管理系统文件+数据+执行； 
                //    //5 采集系统文件+执行
                //    //6 管理系统执行
                //    //7 采集系统执行
                //    //8 管理系统文件+执行
                //    Process.Start(Info);

                    //}
                Again:
                    Boolean isRemote = Internet.IsWanAlive();
                    LoginDialog loginDialog = new LoginDialog();

                    //如果有sys.xml且NetTcpDomainAddress做了配置，强制更改Endpoint设置
                    string EndPointAddress0 = BizCommon.ConfigHelper.GetEndpointAddress("TransferServiceEndPoint").ToString();
                    string NetTcpDomainAddress0 = BizCommon.ConfigHelper.GetSysXmlValue("NetTcpDomainAddress");
                    string strAddressM0 = EndPointAddress0.Replace("net.tcp://", "");
                    strAddressM0 = strAddressM0.Substring(0, strAddressM0.IndexOf('/'));
                    string strAddressE0 = EndPointAddress0.Replace("net.tcp://", "").Replace(strAddressM0, "");
                    if (!string.IsNullOrEmpty(NetTcpDomainAddress0))//配置了域名地址，开始判断域名地址
                    {
                        EndPointAddress0 = "net.tcp://" + NetTcpDomainAddress0 + strAddressE0;
                        BizCommon.ConfigHelper.SetEndpointAddress("TransferServiceEndPoint", EndPointAddress0);
                    }
                    if (DialogResult.OK == loginDialog.ShowDialog())
                    {
                        if (loginDialog.IsUpdate == false)
                        {
                            #region 登录
                            const String resourceName = "Splash.jpg"; // 嵌入的资源图片的名字

                            splash = SplashScreen.Current;
                            splash.SetTransparentKey = Color.Fuchsia;
                            splash.SetBackgroundImage = GetResource(resourceName);

                            splash.SetFade = true;//设置淡入淡出效果

                            splash.ShowSplashScreen();

                            AppDomain currentDomain = AppDomain.CurrentDomain;
                            currentDomain.AssemblyLoad += asmLoadHandler;

                            splash.SetStatus = "验证用户并登录...";

                            if (loginDialog.com_User.Text.Trim() == "")
                            {
                                splash.CloseSplashScreen();
                                MessageBox.Show("请输入用户名。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                goto Again;
                            }

                            try
                            {
                                isRemote = true;
                                Boolean logined = loginDialog.Login(isRemote);
                                if (!logined)
                                {
                                    //Modifyed By TanLiPing In 2014-03-16
                                    //添加智能识别DNS和域名代码
                                    if (Yqun.Services.Agent.TestNetwork() == true)//net.tcp配置可以正常访问网络
                                    {
                                        splash.CloseSplashScreen();
                                        MessageBox.Show("用户名或密码错误，请重新输入。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        goto Again;
                                    }
                                    else//使用备用地址
                                    {
                                        bool bNetWorkIsConnected = false;
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
                                        if (bNetWorkIsConnected)
                                        {
                                            logined = loginDialog.Login(isRemote);
                                            if (!logined)
                                            {
                                                splash.CloseSplashScreen();
                                                MessageBox.Show("用户名或密码错误，请重新输入。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                goto Again;
                                            }
                                        }
                                        else
                                        {
                                            splash.CloseSplashScreen();
                                            MessageBox.Show("连接不上服务器端，请检查网络配置。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            goto Again;
                                        }

                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                splash.CloseSplashScreen();
                                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                goto Again;
                            }

                            splash.SetStatus = "登录成功并注册用户信息...";

                            loginDialog.MemberUser();

                            splash.SetStatus = "启动主程序...";
                            splash.CloseSplashScreen();

                            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                            //验证初始密码，用户取消退出程序
                            if (!loginDialog.ChangDefaultPwd())
                            {
                                Application.Exit();
                            }
                            else
                            {
                                Application.Run(new MainForm());
                            }
                            #endregion
                        }
                        else
                        {
                            #region 更新
                            JZUpgrade.UpdateAlert ua = new JZUpgrade.UpdateAlert(1);
                            if (ua.ShowDialog() == DialogResult.OK)
                            {
                                if (!ProcessHelper.IsRuningProcess("JZUpgradeAgent"))
                                {
                                    ProcessStartInfo Info = new ProcessStartInfo();
                                    Info.CreateNoWindow = false;
                                    Info.UseShellExecute = true;
                                    Info.FileName = Path.Combine(Application.StartupPath, "JZUpgrade.exe");
                                    Info.Arguments = "\"8\"";
                                    //1 管理系统文件+不执行，2 采集系统文件+不执行，3 管理系统数据+不执行，
                                    //4 管理系统文件+数据+执行； 
                                    //5 采集系统文件+执行
                                    //6 管理系统执行
                                    //7 采集系统执行
                                    //8 管理系统文件+执行
                                    Process.Start(Info);
                                    //Process currentProcess = Process.GetCurrentProcess();
                                    //currentProcess.Kill();
                                }
                            }
                            #endregion
                        }
                    }
                }
                //}
                //else
                //{
                //    MessageBox.Show("一个实例正在运行...", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    Application.Exit();
                //}
            }
            catch (Exception ex)
            {
                String Error = string.Format("应用程序出错，出错原因：{0}", ex.Message);
                logger.Error(Error);
                MessageBox.Show(Error, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        //static bool IsRunning(string[] args)
        //{
        //    var checkMutex = true;

        //    if (args != null && args.Length >= 1 && args[0] == "dev")
        //    {
        //        checkMutex = false;
        //    }

        //    if (checkMutex)
        //    {
        //        try
        //        {
        //            var currentProcess = Process.GetCurrentProcess();
        //            var processes = Process.GetProcessesByName(currentProcess.ProcessName);
        //            var counter = -1;
        //            foreach (var process in processes)
        //            {
        //                if (process.MainModule.FileName == currentProcess.MainModule.FileName)
        //                {
        //                    counter++;
        //                }
        //            }

        //            if (counter > 0)
        //            {
        //                IntPtr h = FindWindow(null, "C:\\Windows\\system32\\cmd.exe");
        //                ShowWindow(h, ShowWindowCommands.Show);
        //                SetForegroundWindow(h);
        //                SetFocus(h);
        //                System.Diagnostics.Debug.WriteLine(h);

        //                return true;
        //            }
        //        }
        //        catch (Exception e) { logger.Error(e); }
        //    }

        //    return false;
        //}

        static void ApplyLatestPatch()
        {
            String FileCachePath = Path.Combine(Application.StartupPath, "FileCache");
            if (!Directory.Exists(FileCachePath))
                Directory.CreateDirectory(FileCachePath);

            string[] Infos = Directory.GetFiles(FileCachePath);
            foreach (String Info in Infos)
            {
                File.Copy(Info, Path.Combine(Application.StartupPath, Path.GetFileName(Info)), true);
                File.Delete(Info);
            }

        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            String Message = "ExceptionObject：\r\n" + e.ExceptionObject.ToString() + "\r\n\r\nIsTerminating：\r\n" + e.IsTerminating;
            MessageBox.Show(Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static System.Drawing.Image GetResource(String resource)
        {
            return Properties.Resources.Splash;
        }

        static void asmLoadHandler(object sender, AssemblyLoadEventArgs args)
        {
            splash.SetStatus = "加载程序集: " + args.LoadedAssembly.GetName().Name + " ...";
        }
	
    }
}
