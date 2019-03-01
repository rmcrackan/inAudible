// Decompiled with JetBrains decompiler
// Type: iTunesLib.IITLibraryPlaylist
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace iTunesLib
{
  [CompilerGenerated]
  [TypeIdentifier]
  [Guid("53AE1704-491C-4289-94A0-958815675A3D")]
  [ComImport]
  public interface IITLibraryPlaylist : IITPlaylist, IITObject
  {
    [SpecialName]
    new void _VtblGap1_1();

    new string Name { [DispId(1610743809)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(1610743809)] [param: MarshalAs(UnmanagedType.BStr), In] set; }

    [SpecialName]
    new void _VtblGap2_5();

    [DispId(1610809344)]
    new void Delete();

    [SpecialName]
    void _VtblGap3_13();

    IITTrackCollection Tracks { [DispId(1610809358)] [return: MarshalAs(UnmanagedType.Interface)] get; }

    [DispId(1610874880)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IITOperationStatus AddFile([MarshalAs(UnmanagedType.BStr), In] string filePath);
  }
}
