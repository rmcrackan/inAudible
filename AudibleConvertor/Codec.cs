// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.Codec
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System.ComponentModel;

namespace AudibleConvertor
{
  public class Codec : INotifyPropertyChanged
  {
    private string _description;

    public string Description
    {
      get
      {
        return this._description;
      }
      set
      {
        this._description = value;
        if (this.PropertyChanged == null)
          return;
        this.PropertyChanged((object) this, new PropertyChangedEventArgs(nameof (Description)));
      }
    }

    public string Name { get; set; }

    public bool AACompatible { get; set; }

    public bool Advanced { get; set; }

    public bool Lossless { get; set; }

    public string Extension { get; set; }

    public Codec(string _description, string _codec, bool _aacompatible, bool _advanced, bool _lossless, string _extension)
    {
      this.Description = _description;
      this.Name = _codec;
      this.AACompatible = _aacompatible;
      this.Advanced = _advanced;
      this.Lossless = _lossless;
      this.Extension = _extension;
    }

    public event PropertyChangedEventHandler PropertyChanged;
  }
}
