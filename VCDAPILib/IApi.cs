// Decompiled with JetBrains decompiler
// Type: VCDAPILib.IApi
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace VCDAPILib
{
  [CompilerGenerated]
  [TypeIdentifier]
  [Guid("6D92904F-76F6-4664-BDAF-4B82AEA4CA90")]
  [ComImport]
  public interface IApi
  {
    [SpecialName]
    void _VtblGap1_1();

    [DispId(2)]
    int VCDEject([MarshalAs(UnmanagedType.BStr)] string DriveOrFilename);

    [SpecialName]
    void _VtblGap2_13();

    [DispId(18)]
    int VCDIsProperlyInstalled();

    [SpecialName]
    void _VtblGap3_5();

    [DispId(24)]
    [return: MarshalAs(UnmanagedType.BStr)]
    string VCDGetVCDBurnerDriveLetters();

    [SpecialName]
    void _VtblGap4_7();

    [DispId(32)]
    [return: MarshalAs(UnmanagedType.BStr)]
    string VCDGetVCDPath([In] int dwPathID);

    [DispId(33)]
    uint VCDSetVCDPath([In] uint dwPathID, [MarshalAs(UnmanagedType.BStr), In] string szPath);

    [SpecialName]
    void _VtblGap5_1();

    [DispId(41)]
    uint InitMusicFileMode([In] sbyte cDrive);
  }
}
