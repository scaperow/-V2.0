using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace BizCommon
{
    public class JZUser32Helper
    {
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(
            int hWnd,
            int Msg,
            int wParam,
            ref COPYDATASTRUCT lParam);

        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern int FindWindow(String lpClassName, String lpWindowName);
    }
}
