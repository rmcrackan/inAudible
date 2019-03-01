// Decompiled with JetBrains decompiler
// Type: CustomControls.OS.POINT
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System.Drawing;

namespace CustomControls.OS
{
  public struct POINT
  {
    public int x;
    public int y;

    public POINT(int x, int y)
    {
      this.x = x;
      this.y = y;
    }

    public POINT(Point point)
    {
      this.x = point.X;
      this.y = point.Y;
    }
  }
}
