// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.WizardOptions
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

namespace AudibleConvertor
{
  public class WizardOptions
  {
    private bool _applied;

    public string InputFile { get; set; }

    public string OutputFile { get; set; }

    public bool Split { get; set; }

    public bool CUE { get; set; }

    public bool ChaptersFirst { get; set; }

    public string Codec { get; set; }

    public string SampleRate { get; set; }

    public string Bitrate { get; set; }

    public string Channels { get; set; }

    public bool Start { get; set; }

    public bool FirstTime { get; set; }

    public bool Applied
    {
      get
      {
        return this._applied;
      }
      set
      {
        this._applied = value;
      }
    }
  }
}
