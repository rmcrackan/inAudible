// Decompiled with JetBrains decompiler
// Type: CustomControls.OS.OFNOTIFY
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;

namespace CustomControls.OS
{
  public struct OFNOTIFY
  {
    public NMHDR hdr;
    public IntPtr OPENFILENAME;
    public IntPtr fileNameShareViolation;
  }
}
