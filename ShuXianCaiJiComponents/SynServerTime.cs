using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ShuXianCaiJiComponents
{
    public class SynServerTime
    {
        #region 修改本地系统时间
        [DllImport("Kernel32.dll")]
        private extern static void GetSystemTime(ref SYSTEMTIME lpSystemTime);

        [DllImport("Kernel32.dll")]
        private extern static uint SetLocalTime(ref SYSTEMTIME lpSystemTime);

        [StructLayout(LayoutKind.Sequential)]
        private struct SYSTEMTIME
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;
        }

        /// <summary>
        /// 将本地时间服务器时间同步
        /// </summary>
        /// <param name="SqlServerTime">时间</param>
        public static uint SynchroTime(DateTime ServerDateTime)
        {
            //服务器时间

            DateTime ServerTime = ServerDateTime;
            SYSTEMTIME st = new SYSTEMTIME();
            st.wYear = Convert.ToUInt16(ServerTime.Year);
            st.wMonth = Convert.ToUInt16(ServerTime.Month);
            st.wDay = Convert.ToUInt16(ServerTime.Day);
            st.wHour = Convert.ToUInt16(ServerTime.Hour);
            st.wMilliseconds = Convert.ToUInt16(ServerTime.Millisecond);
            st.wMinute = Convert.ToUInt16(ServerTime.Minute);
            st.wSecond = Convert.ToUInt16(ServerTime.Second);
            return SetLocalTime(ref st);
        }
        #endregion
    }
}
