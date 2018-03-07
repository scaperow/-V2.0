using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;

namespace JZUpgrade
{
    static class Program
    {
        private static readonly string DomainName = "PlatformDomain";

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                if (AppDomain.CurrentDomain.FriendlyName != DomainName)
                {
                    string exeFilePath = Assembly.GetExecutingAssembly().Location;
                    string applicationFolder = Path.GetDirectoryName(exeFilePath);

                    string starterExePath = applicationFolder + "\\JZUpgradeAgent.exe";
                    if (File.Exists(starterExePath))
                    {
                        XmlSerializer serialier = new XmlSerializer(typeof(AppDomainSetup));
                        string xmlFilePath = applicationFolder + "\\XmlData.xml";
                        using (var stream = File.Create(xmlFilePath))
                        {
                            serialier.Serialize(stream, AppDomain.CurrentDomain.SetupInformation);
                        }

                        exeFilePath = exeFilePath.Replace(" ", "*");
                        xmlFilePath = xmlFilePath.Replace(" ", "*");
                        string argspath = exeFilePath + " " + xmlFilePath + " " + args[0];

                        Process starter = Process.Start(starterExePath, argspath);
                    }

                    return;
                }

                if (args.Length == 1)
                {
                    String updateFlag = args[0];
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new JZUpgrade(updateFlag));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
