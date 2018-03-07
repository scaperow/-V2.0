using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace BizComponents
{
    public class YqunRichTextBox : RichTextBoxEx
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct ChARRANGE
        {
            public int cpMin;   //范围的第一个字符 (从零开始)
            public int cpMax;   //范围的最后一个字符 (-1表示文档的结束)
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct FORMATRANGE
        {
            public IntPtr hdc;             //用来绘画的实际设备上下文
            public IntPtr hdcTarget;       //决定文本格式的目标设备上下文
            public RECT rc;                //要绘制的范围(以twips为单位)
            public RECT rcPage;            //整个设备上下文的页面范围(以twips为单位)
            public ChARRANGE chrg;         //要绘制的文本的范围(请看以前的规范)
        }

        private const int WM_USER = 0x0400;
        private const int EM_FORMATRANGE = WM_USER + 57;
        private const int WM_ERASEBKGND = 0x0014;

        private const int TECHNOLOGY = 2;
        private const int DT_RASPRINTER = 2;

        private const int PHYSICALOFFSETX = 112;
        private const int PHYSICALOFFSETY = 113;

        private const int LOGPIXELSX = 88;
        private const int LOGPIXELSY = 90;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        [DllImport("gdi32.dll")]
        private static extern int GetDeviceCaps(IntPtr hDC, int nIndex);

        public Boolean IsPrinter(Graphics g)
        {
            IntPtr hdc = g.GetHdc();
            bool isprinter = (DT_RASPRINTER == GetDeviceCaps(hdc, TECHNOLOGY));
            g.ReleaseHdc(hdc);

            return isprinter;
        }

        public void Draw(Graphics g, Rectangle r, String Rtf, Boolean IsPreview)
        {
            this.Rtf = Rtf;

            float ScaleX, ScaleY;
            bool isprinter = IsPrinter(g);
            if (isprinter)
            {
                ScaleX = 100f;
                ScaleY = 100f;
            }
            else
            {
                ScaleX = g.DpiX;
                ScaleY = g.DpiY;
            }

            int LeftOffset=0, TopOffset=0;
            IntPtr hdc = g.GetHdc();
            if (isprinter && !IsPreview)
            {
                LeftOffset = System.Convert.ToInt32(GetDeviceCaps(hdc, PHYSICALOFFSETX) * 1.0 / GetDeviceCaps(hdc, LOGPIXELSX) * 1440f);
                TopOffset = System.Convert.ToInt32(GetDeviceCaps(hdc, PHYSICALOFFSETY) * 1.0 / GetDeviceCaps(hdc, LOGPIXELSY) * 1440f);
            }

            //计算绘制的大小
            RECT rcDrawTo;
            rcDrawTo.Top = Convert(ScaleY, r.Top) - TopOffset;
            rcDrawTo.Bottom = Convert(ScaleY, r.Bottom);
            rcDrawTo.Left = Convert(ScaleX, r.Left) - LeftOffset;
            rcDrawTo.Right = Convert(ScaleX, r.Right);

            //计算页面的大小
            RECT rcPage;
            rcPage.Top = Convert(ScaleY, r.Top);
            rcPage.Bottom = Convert(ScaleY, r.Bottom);
            rcPage.Left = Convert(ScaleX, r.Left);
            rcPage.Right = Convert(ScaleX, r.Right);

            FORMATRANGE fmtRange;
            fmtRange.chrg.cpMax = this.TextLength;  //绘制的字符的开始和结束 
            fmtRange.chrg.cpMin = 0;
            fmtRange.hdc = hdc;                     //用于绘画的实际设备环境
            fmtRange.hdcTarget = hdc;               //目标设备环境
            fmtRange.rc = rcDrawTo;                 //绘制的大小
            fmtRange.rcPage = rcPage;               //页面的大小 

            IntPtr wparam = IntPtr.Zero;
            wparam = new IntPtr(1);

            //获得内存中 FORMATRANGE 结构的指针
            IntPtr lparam = IntPtr.Zero;
            lparam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fmtRange));
            Marshal.StructureToPtr(fmtRange, lparam, false);

            //发送要绘制的数据
            SendMessage(Handle, EM_FORMATRANGE, wparam, lparam);

            //释放分配的内存块
            Marshal.FreeCoTaskMem(lparam);

            //释放上一次调用使用的设备环境句柄
            g.ReleaseHdc(hdc);

            //释放控件缓存的信息
            SendMessage(Handle, EM_FORMATRANGE, new IntPtr(0), new IntPtr(0));
        }

        public void Draw(Graphics g, Rectangle r, float Left, float Top)
        {
            IntPtr hdc = g.GetHdc();
            bool IsPrinter = (DT_RASPRINTER == GetDeviceCaps(hdc, TECHNOLOGY));
            g.ReleaseHdc(hdc);

            float ScaleX = g.DpiX, ScaleY = g.DpiY;
            if (IsPrinter)
            {
                ScaleX = ScaleY = 100f;
            }

            //计算绘制的大小,页面的大小
            RECT rectToPrint, rectPage;
            rectPage.Top = Convert(ScaleY, r.Top);
            rectPage.Bottom = Convert(ScaleY, r.Bottom);
            rectPage.Left = Convert(ScaleX, r.Left + 1);
            rectPage.Right = Convert(ScaleX, r.Right);

            Rectangle Printr = new Rectangle(System.Convert.ToInt32(r.Left + Left), System.Convert.ToInt32(r.Top + Top), System.Convert.ToInt32(r.Width - Left), System.Convert.ToInt32(r.Height - Top));
            rectToPrint.Top = Convert(ScaleY, Printr.Top);
            rectToPrint.Bottom = Convert(ScaleY, Printr.Bottom);
            rectToPrint.Left = Convert(ScaleX, Printr.Left + 1);
            rectToPrint.Right = Convert(ScaleX, Printr.Right);

            hdc = g.GetHdc();

            FORMATRANGE fmtRange;
            fmtRange.chrg.cpMax = this.TextLength;  //绘制的字符的开始和结束 
            fmtRange.chrg.cpMin = 0;
            fmtRange.hdc = hdc;                     //用于绘画的实际设备环境
            fmtRange.hdcTarget = hdc;               //目标设备环境
            fmtRange.rc = rectToPrint;              //绘制的大小
            fmtRange.rcPage = rectPage;             //页面的大小 

            IntPtr wparam = IntPtr.Zero;
            wparam = new IntPtr(1);

            //获得内存中 FORMATRANGE 结构的指针
            IntPtr lparam = IntPtr.Zero;
            lparam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fmtRange));
            Marshal.StructureToPtr(fmtRange, lparam, false);

            //发送要绘制的数据
            SendMessage(Handle, EM_FORMATRANGE, wparam, lparam);

            //释放分配的内存块
            Marshal.FreeCoTaskMem(lparam);

            //释放上一次调用使用的设备环境句柄
            g.ReleaseHdc(hdc);

            //释放控件缓存的信息
            SendMessage(Handle, EM_FORMATRANGE, new IntPtr(0), new IntPtr(0));
        }

        #region 坐标转换函数

        private int Convert(float Factor, int ConvertedArgs)
        {
            return (int)Math.Round(Math.Ceiling((double)(((float)(ConvertedArgs * 0x5a0)) / Factor)));
        }

        private int Convert(float Factor, float ConvertedArgs)
        {
            return (int)Math.Round(((ConvertedArgs * 1440f) / Factor));
        }

        #endregion 坐标转换函数
    }
}
