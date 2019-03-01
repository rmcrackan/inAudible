// Decompiled with JetBrains decompiler
// Type: iTunesLib.IITUserPlaylist
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace iTunesLib
{
  [CompilerGenerated]
  [TypeIdentifier]
  [Guid("0A504DED-A0B5-465A-8A94-50E20D7DF692")]
  [ComImport]
  public interface IITUserPlaylist : IITPlaylist, IITObject
  {
    [SpecialName]
    void _VtblGap1_26();

    [DispId(1610874883)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IITTrack AddTrack([MarshalAs(UnmanagedType.Struct), In] ref object iTrackToAdd);

    [SpecialName]
    void _VtblGap2_8();

    [DispId(1610874892)]
    void Reveal();
  }
}
