using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yqun.Services;

namespace TestDataUploadWS
{
    public class DBHelper
    {
        public static object CallLocalService(string AssemblyName, string MethodName, object[] Parameters, String DataInstance)
        {

            return Transfer.CallLocalServiceWithDBArgs(AssemblyName, MethodName, Parameters, "112.124.99.146", DataInstance, "sygldb_kingrocket_f", "wdxlzyn@#830");
        }
    }
}
