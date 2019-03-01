// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.DataGridViewProgressColumn
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System.Windows.Forms;

namespace AudibleConvertor
{
  public class DataGridViewProgressColumn : DataGridViewImageColumn
  {
    public DataGridViewProgressColumn()
    {
      this.CellTemplate = (DataGridViewCell) new DataGridViewProgressCell();
    }
  }
}
