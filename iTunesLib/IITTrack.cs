// Decompiled with JetBrains decompiler
// Type: iTunesLib.IITTrack
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace iTunesLib
{
  [Guid("4CB0915D-1E54-4727-BAF3-CE6CC9A225A1")]
  [CompilerGenerated]
  [TypeIdentifier]
  [ComImport]
  public interface IITTrack : IITObject
  {
    [SpecialName]
    void _VtblGap1_1();

    string Name { [DispId(1610743809)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(1610743809)] [param: MarshalAs(UnmanagedType.BStr), In] set; }

    [SpecialName]
    void _VtblGap2_8();

    ITTrackKind Kind { [DispId(1610809347)] get; }
  }
}
