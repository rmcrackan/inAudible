// Decompiled with JetBrains decompiler
// Type: iTunesLib.IiTunes
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace iTunesLib
{
  [CompilerGenerated]
  [TypeIdentifier]
  [Guid("9DD6680B-3EDC-40DB-A771-E6FE4832E34A")]
  [ComImport]
  public interface IiTunes
  {
    [SpecialName]
    void _VtblGap1_17();

    [DispId(1610743825)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IITPlaylist CreatePlaylist([MarshalAs(UnmanagedType.BStr), In] string playlistName);

    [SpecialName]
    void _VtblGap2_4();

    [DispId(1610743830)]
    void Quit();

    IITSourceCollection Sources { [DispId(1610743831)] [return: MarshalAs(UnmanagedType.Interface)] get; }

    [SpecialName]
    void _VtblGap3_30();

    IITLibraryPlaylist LibraryPlaylist { [DispId(1610743862)] [return: MarshalAs(UnmanagedType.Interface)] get; }
  }
}
