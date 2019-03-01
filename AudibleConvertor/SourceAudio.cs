// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.SourceAudio
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace AudibleConvertor
{
  [Serializable]
  public class SourceAudio : ICloneable
  {
    public SourceAudio.Accuracy accurateDuration = SourceAudio.Accuracy.High;
    public string fileName;
    public double duration;
    public SupportLibraries libs;

    public SourceAudio(SupportLibraries myLibs)
    {
      this.libs = myLibs;
    }

    internal void Add(string _filename)
    {
      this.fileName = _filename;
      try
      {
        if (Path.GetExtension(_filename).ToLower() == ".wav")
          double.TryParse(this.GetAttribute(_filename, "streams.stream.0.duration"), out this.duration);
        else if (this.accurateDuration == SourceAudio.Accuracy.High)
        {
          try
          {
            this.duration = this.GetRealTimeSlow(_filename);
          }
          catch (Exception ex)
          {
            Audible.diskLogger("Failed to get real duration for " + _filename + ": " + ex.ToString());
          }
        }
        else if (this.accurateDuration == SourceAudio.Accuracy.Medium)
          this.duration = this.GetRealTime(_filename);
        else
          double.TryParse(this.GetAttribute(_filename, "streams.stream.0.duration"), out this.duration);
        Audible.diskLogger(_filename + " - " + (object) this.duration);
      }
      catch (Exception ex)
      {
        Audible.diskLogger("Couldn't add " + _filename + ": " + ex.ToString());
      }
    }

    private double GetRealTime(string file)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.libs.ffmpegPath;
      process.StartInfo.Arguments = "-i \"" + file + "\" -acodec copy -f null -";
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardInput = true;
      process.StartInfo.RedirectStandardError = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.Start();
      string end = process.StandardError.ReadToEnd();
      process.WaitForExit();
      string[] strArray1 = end.Split('\r');
      List<string> stringList = new List<string>();
      foreach (string str in strArray1)
      {
        if (str.Trim().StartsWith("size=N/A time="))
          stringList.Add(str.Split(' ')[1].Split('=')[1]);
      }
      string[] strArray2 = stringList[stringList.Count - 1].Split(':');
//strArray2[0].Trim() + ":" + strArray2[1].Trim() + ":" + strArray2[2].Trim();
      return double.Parse(strArray2[0].Trim()) * 60.0 * 60.0 + double.Parse(strArray2[1].Trim()) * 60.0 + double.Parse(strArray2[2].Trim());
    }

    private double GetRealTimeSlow(string file)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.libs.ffmpegPath;
      string str = this.libs.tempPath + "\\tempTime-" + Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".wav";
      process.StartInfo.Arguments = "-y -loglevel panic -i \"" + file + "\" \"" + str + "\"";
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.Start();
      process.WaitForExit();
      string attribute = this.GetAttribute(str, "streams.stream.0.duration");
      double result = 0.0;
      double.TryParse(attribute, out result);
      this.SafeDelete(str);
      return result;
    }

    private void SafeDelete(string source)
    {
      for (int index = 0; index < 10; ++index)
      {
        try
        {
          File.Delete(source);
          if (!File.Exists(source))
            break;
          Thread.Sleep(200);
        }
        catch
        {
        }
      }
    }

    private string GetAttribute(string tFile, string attribute)
    {
      string str1 = "";
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.libs.ffprobePath;
      process.StartInfo.Arguments = "-loglevel panic -show_streams -print_format flat \"" + tFile + "\"";
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.Start();
      string end = process.StandardOutput.ReadToEnd();
      process.WaitForExit();
      string str2 = end;
      char[] chArray1 = new char[1]{ '\n' };
      foreach (string str3 in str2.Split(chArray1))
      {
        char[] chArray2 = new char[1]{ '=' };
        string[] strArray = str3.Split(chArray2);
        if (strArray[0] == attribute)
        {
          try
          {
            return strArray[1].Replace("\"", "").TrimEnd('\r', '\n');
          }
          catch
          {
          }
        }
      }
      return str1;
    }

    public object Clone()
    {
      return this.MemberwiseClone();
    }

    public enum Accuracy
    {
      None,
      Medium,
      High,
    }
  }
}
