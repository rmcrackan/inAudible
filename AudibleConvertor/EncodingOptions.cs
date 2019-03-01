// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.EncodingOptions
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;

namespace AudibleConvertor
{
  internal class EncodingOptions : ICloneable
  {
    public int sampleRate = 44100;
    public int channels = 2;
    public bool downmix = true;
    private int vbrRate = 100;
    public string aaxKey = "";
    public AdvancedSplitting.Chapters chapters = new AdvancedSplitting.Chapters();
    public double silenceThreshold = 2.0;
    public string encoder;
    public int bitrate;
    public long startChap;
    public double dStartChap;
    public long endChap;
    public double dEndChap;
    public bool doubleChapters;
    public OpusOptions opusOptions;
    public OggOptions oggOptions;
    public M4BOptions m4bOptions;
    public LameOptions lameOptions;
    public bool vbr;
    public Audible audible;
    public int trackNum;
    public bool embedCover;
    public bool doNotTag;

    public object Clone()
    {
      return this.MemberwiseClone();
    }
  }
}
