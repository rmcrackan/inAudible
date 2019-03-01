// Decompiled with JetBrains decompiler
// Type: CustomControls.OS.WINDOWPOS
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;

namespace CustomControls.OS
{
  public struct WINDOWPOS
  {
    public IntPtr hwnd;
    public IntPtr hwndAfter;
    public int x;
    public int y;
    public int cx;
    public int cy;
    public uint flags;

    public override string ToString()
    {
      return this.x.ToString() + ":" + (object) this.y + ":" + (object) this.cx + ":" + (object) this.cy + ":" + ((SWP_Flags) this.flags).ToString();
    }
  }
}
