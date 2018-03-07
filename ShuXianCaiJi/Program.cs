using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using ShuXianCaiJiComponents;
using YqunMainAppBase;
using System.Drawing;
using System.Data;
using ShuXianCaiJiModule;
using System.Threading;
using Kingrocket.CJ.Components;

namespace ShuXianCaiJi
{
    static class Program
    {

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            new StartHandle().StartInit();
        }
    }
}
