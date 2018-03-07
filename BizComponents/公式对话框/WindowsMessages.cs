using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace BizComponents
{
    public class Win32
    {
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int message, IntPtr wParam, IntPtr lParam);
    }

    public class Messages
    {
        public const int WM_USER = 0x0400;
        public const int EM_GETOLEINTERFACE = WM_USER + 60;
    }
}
