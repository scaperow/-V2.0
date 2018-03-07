using System;
using System.Collections.Generic;
using System.Text;
using System.Management;

namespace Yqun.Common.ContextCache
{
    public class NetWorkInformation
    {
        public static string GetMacAddress()
        {
            string MacAddress = "";

            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances(); 
            foreach (ManagementObject mo in moc) 
            { 
                if ((bool)mo["IPEnabled"] == true)
                    MacAddress = mo["MacAddress"].ToString(); 
                mo.Dispose();
            }

            return MacAddress; 
        }
    }
}
