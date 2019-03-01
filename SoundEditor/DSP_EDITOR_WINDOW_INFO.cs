// Decompiled with JetBrains decompiler
// Type: SoundEditor.DSP_EDITOR_WINDOW_INFO
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;

namespace SoundEditor
{
  public struct DSP_EDITOR_WINDOW_INFO
  {
    public IntPtr hWnd;
    public byte bIsVisible;
    public int nLeftPosPx;
    public int nTopPosPx;
    public int nWidthPx;
    public int nHeightPx;
  }
}
