// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.ComboboxItem
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

namespace AudibleConvertor
{
  public class ComboboxItem
  {
    public string Text { get; set; }

    public object Value { get; set; }

    public override string ToString()
    {
      return this.Text;
    }
  }
}
