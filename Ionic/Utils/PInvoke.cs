// Decompiled with JetBrains decompiler
// Type: Ionic.Utils.PInvoke
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Ionic.Utils
{
  internal static class PInvoke
  {
    public delegate int BrowseFolderCallbackProc(IntPtr hwnd, int msg, IntPtr lParam, IntPtr lpData);

    internal static class User32
    {
      [DllImport("user32.dll", CharSet = CharSet.Auto)]
      public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, string lParam);

      [DllImport("user32.dll", CharSet = CharSet.Auto)]
      public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);

      [DllImport("user32.dll", SetLastError = true)]
      public static extern IntPtr FindWindowEx(HandleRef hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

      [DllImport("user32.dll", SetLastError = true)]
      public static extern bool SetWindowText(IntPtr hWnd, string text);
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("00000002-0000-0000-c000-000000000046")]
    [SuppressUnmanagedCodeSecurity]
    [ComImport]
    public interface IMalloc
    {
      [MethodImpl(MethodImplOptions.PreserveSig)]
      IntPtr Alloc(int cb);

      [MethodImpl(MethodImplOptions.PreserveSig)]
      IntPtr Realloc(IntPtr pv, int cb);

      [MethodImpl(MethodImplOptions.PreserveSig)]
      void Free(IntPtr pv);

      [MethodImpl(MethodImplOptions.PreserveSig)]
      int GetSize(IntPtr pv);

      [MethodImpl(MethodImplOptions.PreserveSig)]
      int DidAlloc(IntPtr pv);

      [MethodImpl(MethodImplOptions.PreserveSig)]
      void HeapMinimize();
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class BROWSEINFO
    {
      public IntPtr Owner;
      public IntPtr pidlRoot;
      public IntPtr pszDisplayName;
      public string Title;
      public int Flags;
      public PInvoke.BrowseFolderCallbackProc callback;
      public IntPtr lParam;
      public int iImage;
    }

    [SuppressUnmanagedCodeSecurity]
    internal static class Shell32
    {
      [DllImport("shell32.dll", CharSet = CharSet.Auto)]
      public static extern IntPtr SHBrowseForFolder([In] PInvoke.BROWSEINFO lpbi);

      [DllImport("shell32.dll")]
      public static extern int SHGetMalloc([MarshalAs(UnmanagedType.LPArray), Out] PInvoke.IMalloc[] ppMalloc);

      [DllImport("shell32.dll", CharSet = CharSet.Auto)]
      public static extern bool SHGetPathFromIDList(IntPtr pidl, IntPtr pszPath);

      [DllImport("shell32.dll")]
      public static extern int SHGetSpecialFolderLocation(IntPtr hwnd, int csidl, ref IntPtr ppidl);
    }
  }
}
