// Decompiled with JetBrains decompiler
// Type: iTunesLib.IITSourceCollection
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace iTunesLib
{
  [TypeIdentifier]
  [Guid("2FF6CE20-FF87-4183-B0B3-F323D047AF41")]
  [CompilerGenerated]
  [ComImport]
  public interface IITSourceCollection : IEnumerable
  {
    [SpecialName]
    void _VtblGap1_2();

    [DispId(1610743810)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IITSource get_ItemByName([MarshalAs(UnmanagedType.BStr), In] string Name);
  }
}
