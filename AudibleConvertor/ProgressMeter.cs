// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.ProgressMeter
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using CUETools.Ripper;
using CUETools.Ripper.SCSI;
using System;

namespace AudibleConvertor
{
  internal class ProgressMeter
  {
    public DateTime realStart;

    public ProgressMeter()
    {
      this.realStart = DateTime.Now;
    }

    public void ReadProgress(object sender, ReadProgressArgs e)
    {
      CDDriveReader cdDriveReader = (CDDriveReader) sender;
      int num1 = e.Position - e.PassStart;
      TimeSpan timeSpan1 = DateTime.Now - e.PassTime;
      double num2 = timeSpan1.TotalSeconds > 0.0 ? (double) num1 / timeSpan1.TotalSeconds / 75.0 : 1.0;
      TimeSpan timeSpan2 = DateTime.Now - this.realStart;
      TimeSpan timeSpan3 = TimeSpan.FromMilliseconds(timeSpan2.TotalMilliseconds / (double) Math.Max(1, e.PassStart + (num1 + e.Pass * (e.PassEnd - e.PassStart)) / (cdDriveReader.CorrectionQuality + 1)) * (double) cdDriveReader.TOC.AudioLength);
      Console.Write("\r{9} : {0:00}%; {1:00.00}x; {2} ({10:0.00}%) errors; {3:d2}:{4:d2}:{5:d2}/{6:d2}:{7:d2}:{8:d2}", (object) (100.0 * (double) e.Position / (double) cdDriveReader.TOC.AudioLength), (object) num2, (object) e.ErrorsCount, (object) timeSpan2.Hours, (object) timeSpan2.Minutes, (object) timeSpan2.Seconds, (object) timeSpan3.Hours, (object) timeSpan3.Minutes, (object) timeSpan3.Seconds, e.Pass < 1 ? (object) "Progress   " : (object) string.Format("Retry {0:00}   ", (object) e.Pass), (object) (num1 > 0 ? 100.0 * (double) e.ErrorsCount / (double) num1 / 2352.0 : 0.0));
    }
  }
}
