// Decompiled with JetBrains decompiler
// Type: AutoItX3Lib.IAutoItX3
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AutoItX3Lib
{
  [CompilerGenerated]
  [Guid("3D54C6B8-D283-40E0-8FAB-C97F05947EE8")]
  [TypeIdentifier]
  [ComImport]
  public interface IAutoItX3
  {
    [SpecialName]
    void _VtblGap1_14();

    [DispId(15)]
    int AutoItSetOption([MarshalAs(UnmanagedType.BStr), In] string strOption, [In] int nValue);

    [SpecialName]
    void _VtblGap2_4();

    [DispId(20)]
    int ControlClick([MarshalAs(UnmanagedType.BStr), In] string strTitle, [MarshalAs(UnmanagedType.BStr), In] string strText, [MarshalAs(UnmanagedType.BStr), In] string strControl, [MarshalAs(UnmanagedType.BStr), In] string strButton = "LEFT", [In] int nNumClicks = 1, [In] int nX = -2147483647, [In] int nY = -2147483647);

    [DispId(21)]
    [return: MarshalAs(UnmanagedType.BStr)]
    string ControlCommand([MarshalAs(UnmanagedType.BStr), In] string strTitle, [MarshalAs(UnmanagedType.BStr), In] string strText, [MarshalAs(UnmanagedType.BStr), In] string strControl, [MarshalAs(UnmanagedType.BStr), In] string strCommand, [MarshalAs(UnmanagedType.BStr), In] string strExtra);

    [SpecialName]
    void _VtblGap3_13();

    [DispId(35)]
    int ControlSend([MarshalAs(UnmanagedType.BStr), In] string strTitle, [MarshalAs(UnmanagedType.BStr), In] string strText, [MarshalAs(UnmanagedType.BStr), In] string strControl, [MarshalAs(UnmanagedType.BStr), In] string strSendText, [In] int nMode = 0);

    [SpecialName]
    void _VtblGap4_16();

    [DispId(52)]
    int MouseMove([In] int nX, [In] int nY, [In] int nSpeed = -1);

    [SpecialName]
    void _VtblGap5_20();

    [DispId(73)]
    void Send([MarshalAs(UnmanagedType.BStr), In] string strSendText, [In] int nMode = 0);

    [SpecialName]
    void _VtblGap6_7();

    [DispId(81)]
    int WinExists([MarshalAs(UnmanagedType.BStr), In] string strTitle, [MarshalAs(UnmanagedType.BStr), In] string strText = "");

    [SpecialName]
    void _VtblGap7_5();

    [DispId(87)]
    [return: MarshalAs(UnmanagedType.BStr)]
    string WinGetHandle([MarshalAs(UnmanagedType.BStr), In] string strTitle, [MarshalAs(UnmanagedType.BStr), In] string strText = "");
  }
}
