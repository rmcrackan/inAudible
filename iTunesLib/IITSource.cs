// Decompiled with JetBrains decompiler
// Type: iTunesLib.IITSource
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace iTunesLib
{
  [Guid("AEC1C4D3-AEF1-4255-B892-3E3D13ADFDF9")]
  [TypeIdentifier]
  [CompilerGenerated]
  [ComImport]
  public interface IITSource : IITObject
  {
    [SpecialName]
    void _VtblGap1_11();

    IITPlaylistCollection Playlists { [DispId(1610809347)] [return: MarshalAs(UnmanagedType.Interface)] get; }
  }
}
