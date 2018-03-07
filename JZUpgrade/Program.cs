using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace JZUpgrade
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length >= 1)
            {
                Application.Run(new UpgradeForm(args[0]));
            }
            else
            {
                Application.Run(new UpgradeForm(""));
            }
        }
    }
}
