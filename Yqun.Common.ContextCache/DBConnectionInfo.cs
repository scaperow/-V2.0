using System;
using System.Collections.Generic;
using System.Text;

namespace Yqun.Common.ContextCache
{
    /// <summary>
    /// 数据库连接信息
    /// </summary>
    public class DBConnectionInfo
    {
        string _DataAdapterType = "";
        public string DataAdapterType
        {
            get
            {
                return _DataAdapterType;
            }
            set
            {
                _DataAdapterType = value;
            }
        }

        string _DataBaseType = "";
        public string DataBaseType
        {
            get
            {
                return _DataBaseType;
            }
            set
            {
                _DataBaseType = value;
            }
        }

        string _DataSource = "";
        public string DataSource
        {
            get
            {
                return _DataSource;
            }
            set
            {
                _DataSource = value;
            }
        }

        string _DataInstance = "";
        public string DataInstance
        {
            get
            {
                return _DataInstance;
            }
            set
            {
                _DataInstance = value;
            }
        }

        string _DataUserName = "";
        public string DataUserName
        {
            get
            {
                return _DataUserName;
            }
            set
            {
                _DataUserName = value;
            }
        }

        string _DataPassword = "";
        public string DataPassword
        {
            get
            {
                return _DataPassword;
            }
            set
            {
                _DataPassword = value;
            }
        }

        string _DataISAttach = "false";
        public string DataISAttach
        {
            get
            {
                return _DataISAttach;
            }
            set
            {
                _DataISAttach = value;
            }
        }

        string _DataIntegratedLogin = "false";
        public string DataIntegratedLogin
        {
            get
            {
                return _DataIntegratedLogin;
            }
            set
            {
                _DataIntegratedLogin = value;
            }
        }

        string _LocalStartPath;
        public string LocalStartPath
        {
            get
            {
                return _LocalStartPath;
            }
            set
            {
                _LocalStartPath = value;
            }
        }

        string _AssemblyFile;
        public string AssemblyFile
        {
            get
            {
                return _AssemblyFile;
            }
            set
            {
                _AssemblyFile = value;
            }
        }
    }
}
