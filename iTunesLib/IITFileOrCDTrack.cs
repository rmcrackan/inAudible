// Decompiled with JetBrains decompiler
// Type: iTunesLib.IITFileOrCDTrack
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace iTunesLib
{
  [Guid("00D7FE99-7868-4CC7-AD9E-ACFD70D09566")]
  [TypeIdentifier]
  [CompilerGenerated]
  [ComImport]
  public interface IITFileOrCDTrack : IITTrack, IITObject
  {
    [SpecialName]
    void _VtblGap1_8();

    [DispId(1610809344)]
    void Delete();

    [SpecialName]
    void _VtblGap2_27();

    int Finish { [DispId(1610809372)] [param: In] set; [DispId(1610809372)] get; }

    [SpecialName]
    void _VtblGap3_15();

    int Start { [DispId(1610809389)] get; [DispId(1610809389)] [param: In] set; }

    [SpecialName]
    void _VtblGap4_10();

    string Location { [DispId(1610874880)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(1610874880)] [param: MarshalAs(UnmanagedType.BStr), In] set; }

    [SpecialName]
    void _VtblGap5_57();
  }
}
