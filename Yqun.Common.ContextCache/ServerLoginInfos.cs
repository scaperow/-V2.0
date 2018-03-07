using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Runtime.Remoting.Messaging;

namespace Yqun.Common.ContextCache
{
    public class ServerLoginInfos
    {
        public ServerLoginInfos() 
        {}

        /// <summary>
        /// 服务端配置信息
        /// </summary>
        static DBConnectionInfo _DBConnectionInfo = new DBConnectionInfo();
        public static DBConnectionInfo DBConnectionInfo
        {
            get
            {
                return _DBConnectionInfo;
            }
            set
            {
                _DBConnectionInfo = value;
            }
        }
    }
}
