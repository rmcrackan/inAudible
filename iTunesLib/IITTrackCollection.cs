// Decompiled with JetBrains decompiler
// Type: iTunesLib.IITTrackCollection
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.CustomMarshalers;

namespace iTunesLib
{
  [TypeIdentifier]
  [Guid("755D76F1-6B85-4CE4-8F5F-F88D9743DCD8")]
  [CompilerGenerated]
  [ComImport]
  public interface IITTrackCollection : IEnumerable
  {
    [SpecialName]
    void _VtblGap1_4();

    [DispId(-4)]
    [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (EnumeratorToEnumVariantMarshaler))]
    new IEnumerator GetEnumerator();
  }
}
