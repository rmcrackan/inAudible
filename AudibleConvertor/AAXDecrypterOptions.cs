// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.AAXDecrypterOptions
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;

namespace AudibleConvertor
{
  public class AAXDecrypterOptions : ICloneable
  {
    public string DLLPath = "";
    public string aaxFile;
    public string wavFile;
    public double totalSeconds;
    public string mp3File;
    public int splitPosition;
    public int piecenum;
    public string lameOptions;
    public int start;
    public int end;

    public AAXDecrypterOptions(string file, string wav, double seconds)
    {
      this.aaxFile = file;
      this.wavFile = wav;
      this.totalSeconds = seconds;
    }

    public object Clone()
    {
      return this.MemberwiseClone();
    }
  }
}
