// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.RichTextBoxExtensions
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System.Drawing;
using System.Windows.Forms;

namespace AudibleConvertor
{
  public static class RichTextBoxExtensions
  {
    public static void AppendText(this RichTextBox box, string text, Color color)
    {
      box.SelectionStart = box.TextLength;
      box.SelectionLength = 0;
      box.SelectionColor = color;
      box.AppendText(text);
      box.SelectionColor = box.ForeColor;
    }
  }
}
