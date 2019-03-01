// Decompiled with JetBrains decompiler
// Type: iTunesLib.IITPlaylist
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace iTunesLib
{
  [TypeIdentifier]
  [Guid("3D5E072F-2A77-4B17-9E73-E03B77CCCCA9")]
  [CompilerGenerated]
  [ComImport]
  public interface IITPlaylist : IITObject
  {
    [SpecialName]
    void _VtblGap1_1();

    string Name { [DispId(1610743809)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(1610743809)] [param: MarshalAs(UnmanagedType.BStr), In] set; }

    [SpecialName]
    void _VtblGap2_5();

    [DispId(1610809344)]
    void Delete();
  }
}
