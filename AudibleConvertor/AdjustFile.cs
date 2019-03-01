// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.AdjustFile
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System.Diagnostics;
using System.IO;

namespace AudibleConvertor
{
  public class AdjustFile
  {
    private double _searchRange = 10.0;
    private int _silenceSize = 750;
    private int _silenceIncrement = 250;

    public string File { get; set; }

    public double Chapter { get; set; }

    public string TempFile { get; set; }

    public double Offset { get; set; }

    public int IncrementSilence
    {
      get
      {
        this._silenceSize += this._silenceIncrement;
        return this._silenceSize;
      }
      set
      {
        this._silenceSize = value;
      }
    }

    public double SearchRange
    {
      get
      {
        return this._searchRange;
      }
      set
      {
        this._searchRange = value;
      }
    }

    public void Cleanup()
    {
      try
      {
        System.IO.File.Delete(this.TempFile);
      }
      catch
      {
      }
    }

    public void ExtractSubset()
    {
      string directoryName = Path.GetDirectoryName(this.File);
      double num1 = this.Chapter - this.SearchRange;
      this.TempFile = directoryName + "\\scratch.mp3";
      if (num1 < 0.0)
        num1 = 0.0;
      double num2 = this.SearchRange * 2.0;
      SupportLibraries supportLibraries = new SupportLibraries();
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = supportLibraries.ffmpegPath;
      process.StartInfo.WorkingDirectory = directoryName;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.Arguments = "-y -i \"" + this.File + "\" -c copy -t " + (object) num2 + " -ss " + (object) num1 + " \"" + this.TempFile + "\"";
      process.StartInfo.UseShellExecute = false;
      Audible.diskLogger(process.StartInfo.FileName.ToString() + " " + process.StartInfo.Arguments.ToString());
      process.Start();
      process.WaitForExit();
    }
  }
}
