// Decompiled with JetBrains decompiler
// Type: CustomControls.OS.SetWindowPosFlags
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

namespace CustomControls.OS
{
  public enum SetWindowPosFlags
  {
    SWP_NOSIZE = 1,
    SWP_NOMOVE = 2,
    SWP_NOZORDER = 4,
    SWP_NOREDRAW = 8,
    SWP_NOACTIVATE = 16,
    SWP_DRAWFRAME = 32,
    SWP_FRAMECHANGED = 32,
    SWP_SHOWWINDOW = 64,
    SWP_HIDEWINDOW = 128,
    SWP_NOCOPYBITS = 256,
    SWP_NOOWNERZORDER = 512,
    SWP_NOREPOSITION = 512,
    SWP_NOSENDCHANGING = 1024,
    SWP_DEFERERASE = 8192,
    SWP_ASYNCWINDOWPOS = 16384,
  }
}
