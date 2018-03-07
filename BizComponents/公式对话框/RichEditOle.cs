using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace BizComponents
{
    [ComImport()]
    [Guid("00020D00-0000-0000-c000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IRichEditOle
    {
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetClientSite([Out] IntPtr site);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetObjectCount();

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetLinkCount();

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetObject(int iob, [In, Out] REOBJECT lpreobject, [MarshalAs(UnmanagedType.U4)]GetObjectOptions flags);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int InsertObject(REOBJECT lpreobject);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int ConvertObject(int iob, Guid rclsidNew, string lpstrUserTypeNew);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int ActivateAs(Guid rclsid, Guid rclsidAs);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int SetHostNames(string lpstrContainerApp, string lpstrContainerObj);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int SetLinkAvailable(int iob, bool fAvailable);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int SetDvaspect(int iob, uint dvaspect);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int HandsOffStorage(int iob);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int SaveCompleted(int iob, IntPtr lpstg);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int InPlaceDeactivate();

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int ContextSensitiveHelp(bool fEnterMode);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetClipboardData([In, Out] ref CHARRANGE lpchrg, [MarshalAs(UnmanagedType.U4)] GetObjectOptions reco, [Out] IntPtr lplpdataobj);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int ImportDataObject(IntPtr lpdataobj, int cf, IntPtr hMetaPict);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CHARRANGE
    {
        public int cpMin;
        public int cpMax;
    }

    public enum GetObjectOptions
    {
        REO_GETOBJ_NO_INTERFACES = 0x00000000,
        REO_GETOBJ_POLEOBJ = 0x00000001,
        REO_GETOBJ_PSTG = 0x00000002,
        REO_GETOBJ_POLESITE = 0x00000004,
        REO_GETOBJ_ALL_INTERFACES = 0x00000007,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CLSID
    {
        public int a;
        public short b;
        public short c;
        public byte d;
        public byte e;
        public byte f;
        public byte g;
        public byte h;
        public byte i;
        public byte j;
        public byte k;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SIZEL
    {
        public int x;
        public int y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class REOBJECT
    {
        public REOBJECT()
        {
        }

        public int cbStruct = Marshal.SizeOf(typeof(REOBJECT));		// Size of structure
        public int cp = 0;											// Character position of object
        public CLSID clsid = new CLSID();							// Class ID of object
        public IntPtr poleobj = IntPtr.Zero;						// OLE object interface
        public IntPtr pstg = IntPtr.Zero;							// Associated storage interface
        public IntPtr polesite = IntPtr.Zero;						// Associated client site interface
        public SIZEL sizel = new SIZEL();							// Size of object (may be 0,0)
        public uint dvaspect = 0;									// Display aspect to use
        public uint dwFlags = 0;									// Object status flags
        public uint dwUser = 0;										// Dword for user's use
    }
}
