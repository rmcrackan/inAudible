// Decompiled with JetBrains decompiler
// Type: iTunesLib.IITPlaylistCollection
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace iTunesLib
{
  [Guid("FF194254-909D-4437-9C50-3AAC2AE6305C")]
  [CompilerGenerated]
  [TypeIdentifier]
  [ComImport]
  public interface IITPlaylistCollection : IEnumerable
  {
    [SpecialName]
    void _VtblGap1_2();

    [DispId(1610743810)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IITPlaylist get_ItemByName([MarshalAs(UnmanagedType.BStr), In] string Name);
  }
}
