using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Security.Policy;
using System.Security.Permissions;

namespace JZUpgradeAgent
{
    /// <summary>
    /// 用来解决在自动更新的时候部分文件被占用的问题
    /// </summary>
    internal class Program
    {
        private static readonly string DomainName = "PlatformDomain";
        private static AppDomainSetup setup;

        [STAThread]
        private static void Main(string[] args)
        {
            try
            {
                var processes = Process.GetProcessesByName("铁路试验信息管理系统");
                foreach (var process in processes)
                {
                    if (Path.GetDirectoryName(process.MainModule.FileName) == Path.GetDirectoryName(Application.StartupPath))
                    {
                        process.Kill();
                    }
                }
            }
            catch (Exception) { }

            try
            {
                string executablePath = string.Empty;
                if (args.Length > 0)
                {
                    executablePath = args[0];
                    executablePath = executablePath.Replace("*", " ");
                    if (args.Length > 1)
                    {
                        string xmlFilePath = args[1];
                        xmlFilePath = xmlFilePath.Replace("*", " ");
                        if (File.Exists(xmlFilePath))
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(AppDomainSetup));
                            using (var stream = File.Open(xmlFilePath, FileMode.Open, FileAccess.Read))
                            {
                                setup = serializer.Deserialize(stream) as AppDomainSetup;
                            }
                            File.Delete(xmlFilePath);
                        }
                    }
                }
                if (File.Exists(executablePath))
                {
                    AppDomainSetup appDomainShodowCopySetup = AppDomain.CurrentDomain.SetupInformation;
                    if (setup != null) appDomainShodowCopySetup = setup;

                    appDomainShodowCopySetup.ShadowCopyFiles = true.ToString();

                    Evidence baseEvidence = AppDomain.CurrentDomain.Evidence;
                    Evidence evidence = new Evidence(baseEvidence);

                    AppDomain platformDomain = AppDomain.CreateDomain(DomainName, evidence, appDomainShodowCopySetup);
                    platformDomain.ExecuteAssembly(executablePath, evidence, new String[] { args[2] });

                    AppDomain.Unload(platformDomain);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}