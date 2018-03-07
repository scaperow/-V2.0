using System;
using System.Collections.Generic;
using System.Text;
using Yqun.Data.DataBase;
using Yqun.Common.ContextCache;
using Yqun.Common;
using System.Data;
using System.Xml;
using System.Collections;
using System.Runtime.InteropServices;

namespace Yqun.BO
{
    public class BOBase : Yqun.Data.DataBase.DataService
    {
        public BOBase()
        {
            DBConnectionInfo context = Yqun.Common.ContextCache.ServerLoginInfos.DBConnectionInfo;

            string DataAdapterType = context.DataAdapterType;
            string DataBaseType = context.DataBaseType;
            string DataSource = context.DataSource;
            string DataInstance = context.DataInstance;
            string DataUserName = context.DataUserName;
            string DataPassword = context.DataPassword;

            bool DataISAttach = false;
            bool.TryParse(context.DataISAttach,out DataISAttach);
            bool DataIntegratedLogin = false;


            //DataAdapterType = "sqlclient";
            //DataBaseType = "mssqlserver2k5";
            //DataSource = @".\SQLEXPRESS";
            //DataInstance = "testDB";
            //DataUserName = "sa";
            //DataPassword = "1q2w3e4r";
            //DataISAttach = false;
            //DataIntegratedLogin = false;

            bool.TryParse(context.DataIntegratedLogin, out DataIntegratedLogin);

            string DataAppRoot = context.LocalStartPath;

            CreateInstance(DataAdapterType, DataBaseType, DataIntegratedLogin, DataSource, DataInstance, DataUserName, DataPassword, DataISAttach, DataAppRoot);
        }

        public BOBase(string _DataAdapterType,
            string _DataBaseType,
            bool _ISIntegrated,
            string _DataSource,
            string _Instance,
            string _UserName,
            string _Password,
            bool _ISAttach,
            string _AppRoot
            )
            : base(_DataAdapterType, _DataBaseType, _ISIntegrated,
            _DataSource, _Instance, _UserName, _Password, _ISAttach, _AppRoot)
        {

        }
    }
}
