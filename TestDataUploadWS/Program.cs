using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;

namespace TestDataUploadWS
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
 
            /* enable this part will compile only the function of big data statistics */
            
            try
            {
                new TestRoomUploadService().StartStatistics();
            }
            catch (Exception e)
            {
                File.WriteAllText("c:/error.txt", e.ToString());
                Console.WriteLine(e.ToString());
            }
            Console.ReadLine();
            return;
            

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            { 
                new TestRoomUploadService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
