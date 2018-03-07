using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace BizComponents
{
    public class RichTextBoxEx : RichTextBox, IDisposable
    {
        protected IRichEditOle IRichEditOleValue = null;
        protected IntPtr IRichEditOlePtr = IntPtr.Zero;

        protected IRichEditOle GetRichEditOleInterface()
        {
            IntPtr ptr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(IntPtr)));
            Marshal.WriteIntPtr(ptr, IntPtr.Zero);
            try
            {
                if (0 != Win32.SendMessage(this.Handle, Messages.EM_GETOLEINTERFACE, IntPtr.Zero, ptr))
                {
                    IntPtr pRichEdit = Marshal.ReadIntPtr(ptr);
                    try
                    {
                        if (pRichEdit != IntPtr.Zero)
                        {
                            Guid guid = new Guid("00020D00-0000-0000-c000-000000000046");
                            Marshal.QueryInterface(pRichEdit, ref guid, out this.IRichEditOlePtr);

                            this.IRichEditOleValue = (IRichEditOle)Marshal.GetTypedObjectForIUnknown(this.IRichEditOlePtr, typeof(IRichEditOle));
                            if (this.IRichEditOleValue == null)
                            {
                                throw new Exception("获取接口IRichEditOle失败。");
                            }
                        }
                        else
                        {
                            throw new Exception("读取指针失败。");
                        }
                    }
                    finally
                    {
                        Marshal.Release(pRichEdit);
                    }
                }
                else
                {
                    throw new Exception("EM_GETOLEINTERFACE 消息失败。");
                }
            }
            catch
            {
                this.ReleaseRichEditOleInterface();
            }
            finally
            {
                Marshal.FreeCoTaskMem(ptr);
            }

            return this.IRichEditOleValue;
        }

        protected void ReleaseRichEditOleInterface()
        {
            if (this.IRichEditOlePtr != IntPtr.Zero)
            {
                Marshal.Release(this.IRichEditOlePtr);
            }

            this.IRichEditOlePtr = IntPtr.Zero;
            this.IRichEditOleValue = null;
        }

        public Size GetObjectSize(Size defaultSize)
        {
            Size size = defaultSize;

            IRichEditOle richEditInterface = this.GetRichEditOleInterface();
            if (richEditInterface.GetObjectCount() == 0)
                return size;

            REOBJECT reObject = new REOBJECT();

            // S_OK == 0, so 0 == success.
            if (0 == richEditInterface.GetObject(0, reObject, GetObjectOptions.REO_GETOBJ_POLEOBJ))
            {
                try
                {
                    IntPtr pViewObject = IntPtr.Zero;
                    Guid guid = new Guid("{0000010d-0000-0000-C000-000000000046}");
                    Marshal.QueryInterface(reObject.poleobj, ref guid, out pViewObject);

                    IViewObject viewobject = (IViewObject)Marshal.GetTypedObjectForIUnknown(pViewObject, typeof(IViewObject));

                    size = new Size(reObject.sizel.x, reObject.sizel.y);

                    //Release the pViewObject
                    Marshal.Release(pViewObject);
                }
                finally
                {
                    Marshal.Release(reObject.poleobj);
                }
            }

            return size;
        }

        public Image Ole2Image(int width,int height)
        {
            Bitmap result = null;

            IRichEditOle richEditInterface = this.GetRichEditOleInterface();
            if (richEditInterface.GetObjectCount() == 0)
                return result;

            REOBJECT reObject = new REOBJECT();

            // S_OK == 0, so 0 == success.
            if (0 == richEditInterface.GetObject(0, reObject, GetObjectOptions.REO_GETOBJ_POLEOBJ))
            {
                try
                {
                    IntPtr pViewObject = IntPtr.Zero;
                    Guid guid = new Guid("{0000010d-0000-0000-C000-000000000046}");
                    Marshal.QueryInterface(reObject.poleobj, ref guid, out pViewObject);

                    IViewObject viewobject = (IViewObject)Marshal.GetTypedObjectForIUnknown(pViewObject, typeof(IViewObject));

                    int imgwidth = width;
                    int imgheight = System.Convert.ToInt32(reObject.sizel.y * 1.0 / reObject.sizel.x * imgwidth);
                    result = new Bitmap(imgwidth, imgheight);
                    Rectangle imgRectangle = new Rectangle(0, 0, imgwidth, imgheight);
                    using (Graphics g = Graphics.FromImage(result))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                        IntPtr hdc = g.GetHdc();
                        viewobject.Draw(1, -1, IntPtr.Zero, IntPtr.Zero,
                                        IntPtr.Zero, hdc, ref imgRectangle,
                                        ref imgRectangle, IntPtr.Zero, 0);
                        g.ReleaseHdc(hdc);
                        g.DrawRectangle(Pens.White, imgRectangle);
                    }

                    //Release the pViewObject
                    Marshal.Release(pViewObject);
                }
                finally
                {
                    Marshal.Release(reObject.poleobj);
                }
            }

            return result;
        }

        #region IDisposable Members

        public new void Dispose()
        {
            this.ReleaseRichEditOleInterface();
            base.Dispose();
        }
        
        #endregion
    }
}
