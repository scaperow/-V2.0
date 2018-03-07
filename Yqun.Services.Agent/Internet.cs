using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Net.Sockets;

namespace Yqun.Services
{
    public class Internet
    {
        private static int NETWORK_ALIVE_LAN = 0x00000001;
        private static int NETWORK_ALIVE_WAN = 0x00000002;
        private static int NETWORK_ALIVE_AOL = 0x00000004;

        [DllImport("sensapi.dll")]
        private extern static bool IsNetworkAlive(ref int flags);

        [DllImport("sensapi.dll")]
        private extern static bool IsDestinationReachable(string dest, IntPtr ptr);

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int connectionDescription, int reservedValue);

        public Internet()
        {
        }

        public static bool IsConnected()
        {
            int desc = 0;
            bool state = InternetGetConnectedState(out desc, 0);
            return state;
        }

        public static bool IsLanAlive()
        {
            return IsNetworkAlive(ref NETWORK_ALIVE_LAN);
        }

        public static bool IsWanAlive()
        {
            return IsNetworkAlive(ref NETWORK_ALIVE_WAN);
        }

        public static bool IsAOLAlive()
        {
            return IsNetworkAlive(ref NETWORK_ALIVE_AOL);
        }

        public static bool IsDestinationAlive(string Destination)
        {
            return (IsDestinationReachable(Destination, IntPtr.Zero));
        }

        /// <summary>
        /// 在指定时间内尝试连接指定主机上的指定端口。 （默认端口：80,默认链接超时：5000毫秒）
        /// </summary>
        /// <param name="HostNameOrIp">主机名称或者IP地址</param>
        /// <param name="port">端口</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns>返回布尔类型</returns>
        public static bool IsHostAlive(string HostNameOrIp, int? port, int? timeOut)
        {
            TcpClient tc = new TcpClient();
            tc.SendTimeout = timeOut ?? 5000;
            tc.ReceiveTimeout = timeOut ?? 5000;

            bool isAlive;

            try
            {
                tc.Connect(HostNameOrIp, port ?? 80);
                isAlive = true;
            }
            catch
            {
                isAlive = false;
            }

            finally
            {
                tc.Close();
            }

            return isAlive;
        }
    }
}
