using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JZUpgrade
{
    public class ApplicationHelper
    {
        public static String GetApplicationName(String flag)
        {
            String str = "";
            switch (flag)
            {
                case "1":
                case "3":
                case "4":
                case "6":
                case "8":
                case "10":
                    str = "铁路试验信息管理系统";
                    break;
                case "2":
                case "5":
                case "7":
                    str = "铁路试验实时采集系统";
                    break;
                case "9":
                    str = "app";
                    break;
                default:
                    break;
            }
            return str;
        }
    }
}
