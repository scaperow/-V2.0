using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Yqun.MainUI
{
    public class ProcessHelper
    {
        public static bool IsRuningProcess(String name)
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
    }
}
