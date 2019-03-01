// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.VirtualWAV
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using Amib.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace AudibleConvertor
{
  [Serializable]
  public class VirtualWAV : ICloneable
  {
    public int sampleRate = 44100;
    public int channels = 2;
    public int bitsPerChannel = 16;
    public string soxPath = "";
    public string ffmpegPath = "";
    public string ffprobePath = "";
    public string aax2wavPath = "";
    public string instarip = "";
    public string ngPath = "";
    public string trackDumpPath = "";
    public string aacFile = "";
    public bool panicMode = true;
    public bool removeAudible = true;
    public string AAXkey = "";
    public string AAXiv = "";
    public SupportLibraries supportLibs = new SupportLibraries();
    public string inputFileType = "";
    public SourceAudio.Accuracy accuracy = SourceAudio.Accuracy.High;
    public double totalSeconds;
    public long fileLength;
    public bool aacMode;
    public bool singleWavMode;
    public bool omni;
    public bool normalize;
    public bool DRC;
    public double normalizeLevel;
    public int originalBitrate;
    public bool mergedSuccessfully;
    public int percentComplete;
    public AdvancedOptions advancedOptions;
    public JobProgress myJobProgress;
    private List<SourcePCM> wavs;
    public SourceAudio[] audioFiles;

    public VirtualWAV()
    {
      this.wavs = new List<SourcePCM>();
    }

    public byte[] GetByteSegment(double startChap, double endChap)
    {
      int num1 = 1048576;
      long num2 = (long) (this.sampleRate * this.channels * this.bitsPerChannel / 8);
      List<SourcePCM> pcMcollection = this.GetPCMcollection((long) startChap * num2, (long) endChap * num2);
      long num3 = 0;
      List<byte[]> numArrayList = new List<byte[]>();
      foreach (SourcePCM sourcePcm in pcMcollection)
      {
        long start = sourcePcm.start;
        while (start < sourcePcm.end)
        {
          int count = num1;
          num3 += (long) num1;
          if (start + (long) num1 > sourcePcm.end)
            count = (int) (sourcePcm.end - start);
          byte[] bytes = Lame.getBytes(sourcePcm.fileName, start, count);
          numArrayList.Add(bytes);
          start += (long) num1;
        }
      }
      byte[] numArray1 = new byte[(int) ((double) num3 / (double) this.channels)];
      long index1 = 0;
      foreach (byte[] numArray2 in numArrayList)
      {
        for (long index2 = 0; index2 < (long) numArray2.Length; ++index2)
        {
          if (index2 % (long) this.channels == 0L)
          {
            numArray1[index1] = numArray2[index2];
            ++index1;
          }
        }
      }
      return numArray1;
    }

    public byte[] Get44kStereoHeader()
    {
      return new byte[44]
      {
        (byte) 82,
        (byte) 73,
        (byte) 70,
        (byte) 70,
        (byte) 36,
        (byte) 254,
        (byte) 11,
        (byte) 5,
        (byte) 87,
        (byte) 65,
        (byte) 86,
        (byte) 69,
        (byte) 102,
        (byte) 109,
        (byte) 116,
        (byte) 32,
        (byte) 16,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 1,
        (byte) 0,
        (byte) 2,
        (byte) 0,
        (byte) 68,
        (byte) 172,
        (byte) 0,
        (byte) 0,
        (byte) 16,
        (byte) 177,
        (byte) 2,
        (byte) 0,
        (byte) 4,
        (byte) 0,
        (byte) 16,
        (byte) 0,
        (byte) 100,
        (byte) 97,
        (byte) 116,
        (byte) 97,
        (byte) 0,
        (byte) 254,
        (byte) 11,
        (byte) 5
      };
    }

    public byte[] Get44kMonoHeader()
    {
      return new byte[44]
      {
        (byte) 82,
        (byte) 73,
        (byte) 70,
        (byte) 70,
        (byte) 36,
        (byte) 36,
        (byte) 144,
        (byte) 0,
        (byte) 87,
        (byte) 65,
        (byte) 86,
        (byte) 69,
        (byte) 102,
        (byte) 109,
        (byte) 116,
        (byte) 32,
        (byte) 16,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 1,
        (byte) 0,
        (byte) 1,
        (byte) 0,
        (byte) 68,
        (byte) 172,
        (byte) 0,
        (byte) 0,
        (byte) 136,
        (byte) 88,
        (byte) 1,
        (byte) 0,
        (byte) 2,
        (byte) 0,
        (byte) 16,
        (byte) 0,
        (byte) 100,
        (byte) 97,
        (byte) 116,
        (byte) 97,
        (byte) 0,
        (byte) 36,
        (byte) 144,
        (byte) 0
      };
    }

    public byte[] Get22kMonoHeader()
    {
      return new byte[44]
      {
        (byte) 82,
        (byte) 73,
        (byte) 70,
        (byte) 70,
        (byte) 160,
        (byte) 150,
        (byte) 61,
        (byte) 1,
        (byte) 87,
        (byte) 65,
        (byte) 86,
        (byte) 69,
        (byte) 102,
        (byte) 109,
        (byte) 116,
        (byte) 32,
        (byte) 16,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 1,
        (byte) 0,
        (byte) 1,
        (byte) 0,
        (byte) 34,
        (byte) 86,
        (byte) 0,
        (byte) 0,
        (byte) 68,
        (byte) 172,
        (byte) 0,
        (byte) 0,
        (byte) 2,
        (byte) 0,
        (byte) 16,
        (byte) 0,
        (byte) 100,
        (byte) 97,
        (byte) 116,
        (byte) 97,
        (byte) 124,
        (byte) 150,
        (byte) 61,
        (byte) 1
      };
    }

    public byte[] Get22kStereoHeader()
    {
      return new byte[44]
      {
        (byte) 82,
        (byte) 73,
        (byte) 70,
        (byte) 70,
        (byte) 164,
        (byte) 79,
        (byte) 184,
        (byte) 14,
        (byte) 87,
        (byte) 65,
        (byte) 86,
        (byte) 69,
        (byte) 102,
        (byte) 109,
        (byte) 116,
        (byte) 32,
        (byte) 16,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 1,
        (byte) 0,
        (byte) 2,
        (byte) 0,
        (byte) 34,
        (byte) 86,
        (byte) 0,
        (byte) 0,
        (byte) 136,
        (byte) 88,
        (byte) 1,
        (byte) 0,
        (byte) 4,
        (byte) 0,
        (byte) 16,
        (byte) 0,
        (byte) 100,
        (byte) 97,
        (byte) 116,
        (byte) 97,
        (byte) 128,
        (byte) 79,
        (byte) 184,
        (byte) 14
      };
    }

    public void AddWAV(SourcePCM wav)
    {
      for (int index = 0; index < this.wavs.Count; ++index)
      {
        if (this.wavs[index].fileName == wav.fileName)
        {
          this.wavs[index].relativeEnd = wav.end - wav.start + this.fileLength;
          this.UpdateStats();
          return;
        }
      }
      wav.relativeStart = this.fileLength;
      wav.relativeEnd = wav.end - wav.start + this.fileLength;
      this.wavs.Add(wav);
      this.UpdateStats();
    }

    public void AddAAC(string file)
    {
      this.aacFile = file;
      SourcePCM wav = new SourcePCM();
      wav.AddWAV(file);
      this.AddWAV(wav);
    }

    public List<SourcePCM> GetTotalWavs()
    {
      return this.wavs;
    }

    internal void UpdateStats()
    {
      this.fileLength = 0L;
      foreach (SourcePCM wav in this.wavs)
        this.fileLength += wav.end - wav.start;
      this.totalSeconds = (double) this.fileLength / (double) (this.sampleRate * this.channels * this.bitsPerChannel / 8);
    }

    public double ConvertPositionToSeconds(long pos)
    {
      return (double) pos / (double) (this.sampleRate * this.channels * this.bitsPerChannel / 8);
    }

    public string GetFormattedTime()
    {
      TimeSpan timeSpan = TimeSpan.FromSeconds(this.totalSeconds);
      return ((int) timeSpan.TotalHours).ToString("D2") + ":" + timeSpan.Minutes.ToString("D2") + ":" + timeSpan.Seconds.ToString("D2");
    }

    public static string GetFormattedTime(double totalSeconds)
    {
      TimeSpan timeSpan = TimeSpan.FromSeconds(totalSeconds);
      return ((int) timeSpan.TotalHours).ToString("D2") + ":" + timeSpan.Minutes.ToString("D2") + ":" + timeSpan.Seconds.ToString("D2");
    }

    public bool Physical2VirtualWAV(string wav)
    {
      bool flag = true;
      SourcePCM wav1 = new SourcePCM();
      wav1.AddWAV(wav);
      this.AddWAV(wav1);
      return flag;
    }

    public bool ConstructStudioWAV(string flac)
    {
      bool flag = true;
      Audible audible = new Audible();
      Audible.diskLogger("Decompressing FLAC...");
      SourcePCM wav = new SourcePCM();
      string str = Path.GetDirectoryName(flac) + "\\" + Path.GetFileNameWithoutExtension(flac) + ".wav";
      this.DecompressFLAC(flac, str);
      wav.AddWAV(str);
      this.AddWAV(wav);
      return flag;
    }

    public bool AACtoWAV(string aac)
    {
      bool flag = true;
      this.AddAAC(aac);
      this.aacMode = true;
      return flag;
    }

    public bool M4BtoWAV(string aac)
    {
      bool flag = true;
      this.AddAAC(aac);
      this.aacMode = true;
      return flag;
    }

    private void GetSampleRateFromInput(string tFile)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.ffprobePath;
      process.StartInfo.Arguments = "-loglevel panic -show_streams -print_format flat \"" + tFile + "\"";
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.Start();
      string end = process.StandardOutput.ReadToEnd();
      process.WaitForExit();
      string str1 = end;
      char[] chArray1 = new char[1]{ '\n' };
      foreach (string str2 in str1.Split(chArray1))
      {
        char[] chArray2 = new char[1]{ '=' };
        string[] strArray = str2.Split(chArray2);
        if (strArray[0] == "streams.stream.0.channels")
        {
          try
          {
            this.channels = int.Parse(strArray[1].Replace("\"", "").TrimEnd('\r', '\n'));
          }
          catch
          {
          }
        }
        else if (strArray[0] == "streams.stream.0.sample_rate")
        {
          try
          {
            this.sampleRate = int.Parse(strArray[1].Replace("\"", "").TrimEnd('\r', '\n'));
          }
          catch
          {
          }
        }
        else if (strArray[0] == "streams.stream.0.bit_rate")
        {
          try
          {
            this.originalBitrate = (int) Math.Round(double.Parse(strArray[1].Replace("\"", "").TrimEnd('\r', '\n')) / 1000.0, MidpointRounding.AwayFromZero);
          }
          catch
          {
          }
        }
      }
    }

    public string GetMostLikelyFileExtension(string dir)
    {
      try
      {
        Audible.diskLogger("About to check if directory...");
        if (!dir.Contains("|"))
        {
          if (File.GetAttributes(dir).HasFlag((Enum) FileAttributes.Directory))
            goto label_4;
        }
        string[] strArray = dir.Split('|');
        Audible.diskLogger("Not directory mode - trying to select filetype of first file: " + dir);
        return Path.GetExtension(strArray[0]);
      }
      catch (Exception ex)
      {
        Audible.diskLogger("Blew up trying to determine if dir or file array: " + ex.ToString());
        return ".flac";
      }
label_4:
      string str1 = ".mp3";
      IOrderedEnumerable<string> orderedEnumerable = ((IEnumerable<string>) Directory.GetDirectories(dir)).OrderBy<string, string>((System.Func<string, string>) (f => new DirectoryInfo(f).Name));
      int num = 0;
      Audible.diskLogger("Guessing file type...");
      foreach (string str2 in (IEnumerable<string>) orderedEnumerable)
        ++num;
      try
      {
        if (num == 0)
        {
          foreach (string path in (IEnumerable<string>) ((IEnumerable<string>) Directory.GetFiles(dir)).OrderByDescending<string, long>((System.Func<string, long>) (fn => new FileInfo(fn).Length)))
          {
            str1 = Path.GetExtension(path);
            if (!(str1 == ""))
              return str1;
          }
        }
        else
        {
          foreach (string path1 in (IEnumerable<string>) orderedEnumerable)
          {
            foreach (string path2 in (IEnumerable<string>) ((IEnumerable<string>) Directory.GetFiles(path1)).OrderByDescending<string, long>((System.Func<string, long>) (fn => new FileInfo(fn).Length)))
            {
              str1 = Path.GetExtension(path2);
              if (!(str1 == ""))
                return str1;
            }
          }
        }
      }
      catch (Exception ex)
      {
        Audible.diskLogger(ex.ToString());
      }
      return str1;
    }

    public string GetMostLikelyFileExtensionNew(string dir)
    {
      try
      {
        if (!dir.Contains("|"))
        {
          if (File.GetAttributes(dir).HasFlag((Enum) FileAttributes.Directory))
            goto label_4;
        }
        Audible.diskLogger("Processing directory as file '" + dir + "' - " + (object) File.GetAttributes(dir));
        return Path.GetExtension(dir.Split('|')[0]);
      }
      catch (Exception ex)
      {
        Audible.diskLogger("Blew up trying to determine if dir or file array: " + ex.ToString());
        return ".flac";
      }
label_4:
      Hashtable source = new Hashtable();
      Audible.diskLogger("Recursive list:");
      foreach (string enumerateFile in Directory.EnumerateFiles(dir, "*.*", SearchOption.AllDirectories))
      {
        Audible.diskLogger(enumerateFile);
        string extension = Path.GetExtension(enumerateFile);
        if (!source.ContainsKey((object) extension))
          source.Add((object) extension, (object) 1);
        else
          source[(object) extension] = (object) ((int) source[(object) extension] + 1);
      }
      return source.Cast<DictionaryEntry>().OrderByDescending<DictionaryEntry, object>((System.Func<DictionaryEntry, object>) (entry => entry.Value)).ToList<DictionaryEntry>()[0].Key.ToString();
    }

    public List<double> ConstructOmniChapters(string dir)
    {
      List<double> doubleList = new List<double>();
      this.totalSeconds = 0.0;
      this.inputFileType = "*";
      string str1;
      try
      {
        str1 = this.GetMostLikelyFileExtension(dir);
        if (str1 == "")
        {
          Audible.diskLogger("Couldn't determine extension.");
          return doubleList;
        }
        Audible.diskLogger("Extension = " + str1);
        this.panicMode = !(str1.ToLower() == ".wav") && !(str1.ToLower() == ".mp3");
      }
      catch (Exception ex1)
      {
        Audible.diskLogger("Crash trying to get extension: " + ex1.ToString());
        try
        {
          Audible.diskLogger("Trying another method...");
          str1 = Path.GetExtension(Directory.EnumerateFiles(dir, "*.*", SearchOption.AllDirectories).ElementAt<string>(0));
        }
        catch (Exception ex2)
        {
          Audible.diskLogger("Nope, that didn't work, either: " + ex2.ToString());
          return doubleList;
        }
      }
      try
      {
        this.inputFileType = str1.Substring(1, str1.Length - 1).ToUpper();
      }
      catch (Exception ex)
      {
        Audible.diskLogger("Crash trying to get file type: " + ex.ToString());
        return doubleList;
      }
      Audible audible = new Audible();
      List<string> source1 = new List<string>();
      bool flag = false;
      if (dir.Contains("|") || !File.GetAttributes(dir).HasFlag((Enum) FileAttributes.Directory))
      {
        Audible.diskLogger("This appears to be a file, not a directory: " + dir);
        flag = true;
        string str2 = dir;
        char[] chArray = new char[1]{ '|' };
        foreach (string str3 in str2.Split(chArray))
          source1.Add(str3);
      }
      if (!flag)
      {
        int num = 0;
        IOrderedEnumerable<string> orderedEnumerable = ((IEnumerable<string>) Directory.GetDirectories(dir)).OrderBy<string, string>((System.Func<string, string>) (f => new DirectoryInfo(f).Name));
        foreach (string str2 in (IEnumerable<string>) orderedEnumerable)
          ++num;
        if (num == 0)
        {
          IOrderedEnumerable<string> source2 = ((IEnumerable<string>) Directory.GetFiles(dir, "*" + str1)).OrderBy<string, string>((System.Func<string, string>) (f => new DirectoryInfo(f).Name));
          if (source2.Count<string>() == 0)
            return doubleList;
          foreach (string path in (IEnumerable<string>) source2)
          {
            if (Path.GetExtension(path) != ".cue" && Path.GetExtension(path) != ".txt")
              source1.Add(path);
          }
        }
        else
        {
          foreach (string path1 in (IEnumerable<string>) orderedEnumerable)
          {
            Audible.diskLogger("dir = " + path1);
            IOrderedEnumerable<string> source2 = ((IEnumerable<string>) Directory.GetFiles(path1, "*" + str1)).OrderBy<string, string>((System.Func<string, string>) (f => new DirectoryInfo(f).Name));
            if (source2.Count<string>() != 0)
            {
              foreach (string path2 in (IEnumerable<string>) source2)
              {
                if (Path.GetExtension(path2) != ".cue" && Path.GetExtension(path2) != ".txt")
                  source1.Add(path2);
              }
            }
          }
        }
      }
      this.GetSampleRateFromInput(source1.ElementAt<string>(0));
      SmartThreadPool smartThreadPool = new SmartThreadPool(10000, Environment.ProcessorCount);
      IWorkItemResult<int>[] workItemResultArray = new IWorkItemResult<int>[source1.Count];
      this.myJobProgress = new JobProgress();
      this.myJobProgress.totalFiles = source1.Count;
      this.myJobProgress.completedItems = new bool[source1.Count];
      this.myJobProgress.filesNames = new string[source1.Count];
      this.myJobProgress.totalCompleted = 0;
      this.audioFiles = new SourceAudio[source1.Count];
      for (int index = 0; index < source1.Count; ++index)
        workItemResultArray[index] = smartThreadPool.QueueWorkItem<string, int, int>(new Amib.Threading.Func<string, int, int>(this.ThreadedMediaAdd), source1[index], index);
      int num1 = 0;
      int num2 = 0;
      while (num1 != workItemResultArray.Length)
      {
        Thread.Sleep(200);
        num1 = 0;
        for (int index = 0; index < workItemResultArray.Length; ++index)
        {
          if (workItemResultArray[index].IsCompleted)
          {
            this.myJobProgress.filesNames[index] = source1[index];
            this.myJobProgress.completedItems[index] = true;
            ++num1;
            this.myJobProgress.totalCompleted = num1;
          }
        }
        if (num2 != num1 && num1 > 0)
        {
          for (int index = num2; index < num1; ++index)
            Audible.diskLogger((index + 1).ToString() + "/" + (object) workItemResultArray.Length + " - " + source1[index]);
          num2 = num1;
        }
      }
      this.myJobProgress.completed = true;
      smartThreadPool.WaitForIdle();
      smartThreadPool.Shutdown();
      double num3 = 0.0;
      doubleList.Add(0.0);
      for (int index = 0; index < this.audioFiles.Length - 1; ++index)
      {
        num3 += ((IEnumerable<SourceAudio>) this.audioFiles).ElementAt<SourceAudio>(index).duration;
        doubleList.Add(num3);
      }
      this.SetTotalFileSize();
      return doubleList;
    }

    private int ThreadedMediaAdd(string file, int position)
    {
      SourceAudio sourceAudio = new SourceAudio(this.supportLibs);
      sourceAudio.accurateDuration = this.accuracy;
      sourceAudio.Add(file);
      this.audioFiles[position] = sourceAudio;
      this.totalSeconds += sourceAudio.duration;
      return 0;
    }

    private void SetTotalFileSize()
    {
      long num = 0;
      for (int index = 0; index < this.audioFiles.Length; ++index)
        num += new FileInfo(this.audioFiles[index].fileName).Length;
      this.fileLength = num;
    }

    public string GetFFmpegConcat()
    {
      string str = string.Empty;
      try
      {
        str = Path.GetTempFileName();
        new FileInfo(str).Attributes = FileAttributes.Temporary;
        Audible.diskLogger("TEMP file created at: " + str);
        StringBuilder stringBuilder = new StringBuilder();
        foreach (SourceAudio audioFile in this.audioFiles)
          stringBuilder.AppendLine("file '" + audioFile.fileName.Replace("'", "'\\''") + "'");
        StreamWriter streamWriter = File.AppendText(str);
        streamWriter.Write(stringBuilder.ToString());
        streamWriter.Flush();
        streamWriter.Close();
      }
      catch (Exception ex)
      {
        Audible.diskLogger("Unable to create TEMP file or set its attributes: " + ex.Message);
      }
      return str;
    }

    public List<double> ConstructCDWAV(string dir)
    {
      if (dir.Contains(".mp3") || dir.Contains(".3gp"))
        dir = this.supportLibs.tempPath + "\\tmpWAVs";
      List<double> doubleList = new List<double>();
      Audible audible = new Audible();
      Audible.diskLogger("Creating monolithic WAV from CD rips...");
      List<string> stringList = new List<string>();
      IOrderedEnumerable<string> orderedEnumerable = ((IEnumerable<string>) Directory.GetDirectories(dir)).OrderBy<string, string>((System.Func<string, string>) (f => new DirectoryInfo(f).Name));
      int num = 0;
      foreach (string str in (IEnumerable<string>) orderedEnumerable)
        ++num;
      if (num == 0)
      {
        IOrderedEnumerable<string> source = ((IEnumerable<string>) Directory.GetFiles(dir, "*.wav")).OrderBy<string, string>((System.Func<string, string>) (f => new DirectoryInfo(f).Name));
        if (source.Count<string>() == 0)
          return doubleList;
        this.GetSampleRateFromInput(source.ElementAt<string>(0));
        foreach (string inputWav in (IEnumerable<string>) source)
        {
          SourcePCM wav = new SourcePCM();
          wav.AddWAV(inputWav);
          this.AddWAV(wav);
        }
      }
      else
      {
        foreach (string path in (IEnumerable<string>) orderedEnumerable)
        {
          stringList.Add(path);
          IOrderedEnumerable<string> source = ((IEnumerable<string>) Directory.GetFiles(path, "*.wav")).OrderBy<string, string>((System.Func<string, string>) (f => new DirectoryInfo(f).Name));
          if (source.Count<string>() != 0)
          {
            this.GetSampleRateFromInput(source.ElementAt<string>(0));
            foreach (string inputWav in (IEnumerable<string>) source)
            {
              SourcePCM wav = new SourcePCM();
              wav.AddWAV(inputWav);
              this.AddWAV(wav);
            }
          }
        }
      }
      foreach (SourcePCM totalWav in this.GetTotalWavs())
        doubleList.Add((double) (long) this.ConvertPositionToSeconds(totalWav.relativeStart));
      return doubleList;
    }

    public List<double> ConstructMP3WAV(string[] dirs)
    {
      List<double> doubleList = new List<double>();
      Audible audible = new Audible();
      Audible.diskLogger("Creating monolithic WAV from MP3's...");
      this.GetSampleRateFromInput(dirs[0]);
      foreach (string dir in dirs)
      {
        SourcePCM wav = new SourcePCM();
        wav.AddWAV(dir);
        this.AddWAV(wav);
      }
      foreach (SourcePCM totalWav in this.GetTotalWavs())
        doubleList.Add((double) (long) this.ConvertPositionToSeconds(totalWav.relativeStart));
      return doubleList;
    }

    public bool ConstructVirtualWAV(List<string> workingDirs)
    {
      bool flag = true;
      new Audible().advancedOptions = this.advancedOptions;
      if (workingDirs.Count == 1)
      {
        Audible.diskLogger("One disk merge");
        foreach (string workingDir in workingDirs)
          this.mergeSingleDisc(workingDir);
      }
      else
      {
        Audible.diskLogger("Multi disk merge");
        flag = this.mergeMultiDirs(workingDirs);
      }
      this.mergedSuccessfully = true;
      return flag;
    }

    private void mergeSingleDisc(string dir)
    {
      foreach (string inputWav in ((IEnumerable<string>) Directory.GetFiles(dir, "*.wav")).OrderBy<string, DateTime>((System.Func<string, DateTime>) (f => new FileInfo(f).CreationTime)).ToArray<string>())
      {
        SourcePCM wav = new SourcePCM();
        wav.AddWAV(inputWav);
        this.AddWAV(wav);
      }
    }

    public bool mergeMultiDirs(List<string> workingDirs)
    {
      bool flag = true;
      int num = 1;
      string overlap = "";
      int count = workingDirs.Count;
      foreach (string workingDir in workingDirs)
      {
        if (num == 1)
        {
          Audible.diskLogger("Merging first disc");
          overlap = this.mergeFirstDir(workingDir);
        }
        else if (num == workingDirs.Count)
        {
          Audible.diskLogger("Merging last disc");
          if (!this.mergeLastDir(workingDir, overlap))
            return false;
        }
        else
        {
          Audible.diskLogger("Merging middle disc");
          overlap = this.mergeMiddleDir(workingDir, overlap);
          if (overlap == "failed")
            return false;
        }
        ++num;
        this.percentComplete = (int) ((double) num / (double) count * 100.0);
      }
      return flag;
    }

    private string mergeFirstDir(string dir)
    {
      string[] array = ((IEnumerable<string>) Directory.GetFiles(dir, "*.wav")).OrderBy<string, DateTime>((System.Func<string, DateTime>) (f => new FileInfo(f).CreationTime)).ToArray<string>();
      for (int index = 0; index < array.Length - 1; ++index)
      {
        SourcePCM wav = new SourcePCM();
        wav.AddWAV(array[index]);
        this.AddWAV(wav);
      }
      return array[array.Length - 1];
    }

    private string mergeMiddleDir(string dir, string overlap)
    {
      Audible audible = new Audible();
      audible.advancedOptions = this.advancedOptions;
      string[] array = ((IEnumerable<string>) Directory.GetFiles(dir, "*.wav")).OrderBy<string, DateTime>((System.Func<string, DateTime>) (f => new FileInfo(f).CreationTime)).ToArray<string>();
      if (!audible.findOverlap(overlap, array[0]))
        return "failed";
      SourcePCM wav1 = new SourcePCM();
      wav1.AddWAV(overlap);
      wav1.end = audible.startOfPatternInTargetFile;
      this.AddWAV(wav1);
      if (!this.singleWavMode)
      {
        SourcePCM wav2 = new SourcePCM();
        wav2.AddWAV(array[0]);
        wav2.start = audible.startOfPatternInSourceFile;
        this.AddWAV(wav2);
      }
      for (int index = 0; index < array.Length - 2; ++index)
      {
        SourcePCM wav2 = new SourcePCM();
        wav2.AddWAV(array[index + 1]);
        this.AddWAV(wav2);
      }
      overlap = array[array.Length - 1];
      return overlap;
    }

    private bool mergeLastDir(string dir, string overlap)
    {
      Audible audible = new Audible();
      audible.advancedOptions = this.advancedOptions;
      string[] array = ((IEnumerable<string>) Directory.GetFiles(dir, "*.wav")).OrderBy<string, DateTime>((System.Func<string, DateTime>) (f => new FileInfo(f).CreationTime)).ToArray<string>();
      bool overlap1 = audible.findOverlap(overlap, array[0]);
      if (!overlap1)
        return false;
      SourcePCM wav1 = new SourcePCM();
      wav1.AddWAV(overlap);
      wav1.end = audible.startOfPatternInTargetFile;
      this.AddWAV(wav1);
      SourcePCM wav2 = new SourcePCM();
      wav2.AddWAV(array[0]);
      wav2.start = audible.startOfPatternInSourceFile;
      this.AddWAV(wav2);
      if (array.Length > 1)
      {
        for (int index = 0; index < array.Length - 1; ++index)
        {
          SourcePCM wav3 = new SourcePCM();
          wav3.AddWAV(array[index + 1]);
          this.AddWAV(wav3);
        }
      }
      return overlap1;
    }

    internal List<SourcePCM> GetPCMcollection(long start, long end)
    {
      List<SourcePCM> sourcePcmList = new List<SourcePCM>();
      bool flag = false;
      foreach (SourcePCM wav in this.wavs)
      {
        SourcePCM sourcePcm = (SourcePCM) wav.Clone();
        long num1 = start - wav.relativeStart;
        long num2 = end - wav.relativeEnd;
        if (!flag && start <= wav.relativeEnd && start >= wav.relativeStart)
        {
          flag = true;
          sourcePcm.start = wav.start + num1;
        }
        if (end <= wav.relativeEnd && end >= wav.relativeStart)
        {
          sourcePcm.end = wav.end + num2;
          sourcePcmList.Add(sourcePcm);
          break;
        }
        if (flag)
          sourcePcmList.Add(sourcePcm);
      }
      return sourcePcmList;
    }

    public object Clone()
    {
      return this.MemberwiseClone();
    }

    internal double[] RemoveAudibleMarkers()
    {
      long num1 = (long) (this.sampleRate * this.channels * this.bitsPerChannel / 8);
      double num2 = this.SkipAudibleIntro(this.wavs[0].fileName, 0.1);
      double thresh1 = 0.1;
      while (num2 < 2.0)
      {
        num2 = this.SkipAudibleIntro(this.wavs[0].fileName, thresh1);
        thresh1 += 0.05;
      }
      Audible.diskLogger("longest gap is " + (object) num2);
      int thresh2 = 10;
      double num3;
      for (num3 = this.SkipAudibleOutro(this.wavs[this.wavs.Count - 1].fileName, (int) this.totalSeconds, thresh2); this.totalSeconds - num3 < 1.0 && thresh2 < 100; num3 = this.SkipAudibleOutro(this.wavs[this.wavs.Count - 1].fileName, (int) this.totalSeconds, thresh2))
        thresh2 += 5;
      double num4;
      if (num3 == 0.0)
        num4 = (double) this.wavs[this.wavs.Count - 1].relativeEnd;
      else if (this.aacMode)
      {
        num4 = num3;
        num1 = 1L;
      }
      else
        num4 = (double) this.wavs[this.wavs.Count - 1].relativeEnd - (double) this.wavs[this.wavs.Count - 1].end + (double) num1 * num3;
      double num5 = num4 / (double) num1;
      Audible.diskLogger("Final cut @ " + (object) num5 + " seconds (" + (object) (this.wavs[this.wavs.Count - 1].relativeEnd / num1) + ")");
      return new double[2]{ num2, num5 };
    }

    private void DecompressFLAC(string flacFile, string wavFile)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.ffmpegPath;
      process.StartInfo.Arguments = "-y -i \"" + flacFile + "\" \"" + wavFile + "\"";
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardInput = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      Audible.diskLogger(process.StartInfo.FileName + " " + process.StartInfo.Arguments);
      process.Start();
      process.WaitForExit();
    }

    private double SkipAudibleIntro(string file, double thresh)
    {
      string str1 = "cmd";
      string str2 = "/C \" \"" + this.soxPath + "\" \"" + file + "\" -n --show-progress trim 0 540 silence 1 0.3 1% 1 " + thresh.ToString("0.00", (IFormatProvider) CultureInfo.InvariantCulture) + " 1% 2>&1\"";
      if (this.aacMode)
        str2 = "/C \" \"" + this.ffmpegPath + "\" -ss 0 -t 540 -i \"" + file + "\" -f s16le -acodec pcm_s16le - |  \"" + this.soxPath + "\" -t raw -b 16 -e signed -c " + (object) this.channels + " -r " + (object) this.sampleRate + " - -n --show-progress silence 1 0.3 1% 1 " + thresh.ToString("0.00", (IFormatProvider) CultureInfo.InvariantCulture) + " 1% 2>&1\"";
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = str1;
      process.StartInfo.Arguments = str2;
      process.StartInfo.EnvironmentVariables["ActivationBytes"] = this.AAXkey;
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.CreateNoWindow = true;
      process.Start();
      string end = process.StandardOutput.ReadToEnd();
      process.WaitForExit();
      string[] strArray1 = end.Split('\r');
      List<string> stringList = new List<string>();
      foreach (string str3 in strArray1)
      {
        if (str3.StartsWith("In:"))
        {
          string str4 = str3.Split(' ')[1];
          try
          {
            string[] strArray2 = str4.Split(':');
            string str5 = strArray2[0].Trim() + ":" + strArray2[1].Trim() + ":" + strArray2[2].Trim();
            stringList.Add(str5);
          }
          catch
          {
            Audible.diskLogger("Could not find This is Audble");
          }
        }
      }
      string s = stringList[stringList.Count - 1];
      double totalSeconds = TimeSpan.Parse(s).TotalSeconds;
      Audible.diskLogger("This is Audible found at " + s + " - " + (object) totalSeconds);
      return totalSeconds;
    }

    private double SkipAudibleOutro(string file, int totalSeconds, int thresh)
    {
      double num = 0.0;
      long length = new FileInfo(file).Length;
      string str1 = "cmd";
      string str2;
      if (length < 100000000L)
        str2 = "/C \" \"" + this.soxPath + "\"  -t raw -b 16 -e signed -c " + (object) this.channels + " -r " + (object) this.sampleRate + " \"" + file + "\" -n --show-progress silence 1 0 1% 1 1.5 1% : newfile : restart 2>&1\"";
      else
        str2 = "/C \" \"" + this.soxPath + "\"  -t raw -b 16 -e signed -c " + (object) this.channels + " -r " + (object) this.sampleRate + " \"" + file + "\" -n --show-progress trim " + (object) (totalSeconds - thresh) + " silence 1 0 1% 1 1.5 1% 2>&1\"";
      if (this.aacMode)
        str2 = "/C \" \"" + this.ffmpegPath + "\" -ss " + (totalSeconds - thresh).ToString("0.000", (IFormatProvider) CultureInfo.InvariantCulture) + " -loglevel panic -i \"" + file + "\" -f s16le -acodec pcm_s16le - |  \"" + this.soxPath + "\" -t raw -b 16 -e signed -c " + (object) this.channels + " -r " + (object) this.sampleRate + " - -n --show-progress  silence 1 0 1% 1 1.5 1% 2>&1\"";
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = str1;
      process.StartInfo.Arguments = str2;
      process.StartInfo.EnvironmentVariables["ActivationBytes"] = this.AAXkey;
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.CreateNoWindow = true;
      process.Start();
      string end = process.StandardOutput.ReadToEnd();
      process.WaitForExit();
      string[] strArray1 = end.Split('\r');
      Audible.diskLogger("finished analyzing end - " + (object) strArray1.Length);
      List<string> stringList = new List<string>();
      for (int index = 0; index < strArray1.Length; ++index)
      {
        if (index > 0 && strArray1[index - 1].StartsWith("In:") && (strArray1[index] == "\n" || strArray1[index] == "\nDone."))
        {
          string[] strArray2 = strArray1[index - 1].Split(' ');
          string str3 = strArray2[1];
          if (strArray2[0] == "In:100%")
            str3 = strArray2[2];
          if (!(str3 == ""))
          {
            string str4 = strArray2[2];
            if (strArray2[0] == "In:100%")
              str4 = strArray2[3];
            if (this.aacMode)
            {
              if (strArray2[0] == "In:0.00%")
                str4 = "[" + strArray2[1] + "]";
            }
            try
            {
              if (double.Parse(str4.Substring(1, str4.Length - 2).Split(':')[2]) >= 1.0)
              {
                string[] strArray3 = str3.Split(':');
                string str5 = strArray3[0].Trim() + ":" + strArray3[1].Trim() + ":" + strArray3[2].Trim();
                stringList.Add(str5);
              }
            }
            catch
            {
              Audible.diskLogger("error parsing " + str3);
            }
          }
        }
      }
      try
      {
        string s = stringList[stringList.Count - 1];
        num = TimeSpan.Parse(s).TotalSeconds;
        Audible.diskLogger("Audible Hopes You Have Enjoyed This Program found at " + s + " - " + (object) num);
      }
      catch
      {
        Audible.diskLogger("Error analyzing audible outro");
      }
      if (this.aacMode)
        num = (double) totalSeconds - num;
      return num;
    }

    internal bool IsMono()
    {
      if (this.channels == 1)
        return true;
      string str1 = !this.aacMode ? (!this.omni ? this.wavs[0].fileName : this.audioFiles[0].fileName) : this.aacFile;
      string str2 = "cmd";
      string str3 = "/C \" \"" + this.soxPath + "\" \"" + str1 + "\" -n remix 1,2i stats trim 0 540 2>&1\"";
      if (this.aacMode || this.omni)
        str3 = "/C \" \"" + this.ffmpegPath + "\" -ss 0 -t 540 -i \"" + str1 + "\" -f s16le -acodec pcm_s16le - |  \"" + this.soxPath + "\" -t raw -b 16 -e signed -c " + (object) this.channels + " -r " + (object) this.sampleRate + " - -n remix 1,2i stats 2>&1\"";
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = str2;
      process.StartInfo.Arguments = str3;
      process.StartInfo.EnvironmentVariables["ActivationBytes"] = this.AAXkey;
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.CreateNoWindow = true;
      process.Start();
      string end = process.StandardOutput.ReadToEnd();
      process.WaitForExit();
      string[] strArray1 = end.Split('\r');
      double num1 = 0.0;
      double num2 = 0.0;
      foreach (string str4 in strArray1)
      {
        char[] chArray = new char[1]{ ' ' };
        string[] strArray2 = str4.Split(chArray);
        try
        {
          if (strArray2[0] == "\nMax")
            num1 = double.Parse(strArray2[strArray2.Length - 1]);
          if (strArray2[0] == "\nMin")
            num2 = double.Parse(strArray2[strArray2.Length - 1]);
        }
        catch
        {
        }
      }
      return Math.Abs(num1) <= 0.0 && Math.Abs(num2) <= 0.0;
    }

    internal int DecryptAAX(string file, string wFile, string DLLpath)
    {
      Audible audible = new Audible();
      SourcePCM wav = new SourcePCM();
      int num = this.AAX2Raw(file, wFile, DLLpath);
      wav.AddWAV(wFile);
      this.AddWAV(wav);
      return num;
    }

    internal int DecryptAA(string file, string wFile)
    {
      Audible audible = new Audible();
      SourcePCM wav = new SourcePCM();
      int num = this.AA2MP3(file, wFile);
      this.StripXing(wFile);
      wav.AddWAV(wFile);
      this.AddWAV(wav);
      return num;
    }

    private string StripXing(string inputFile)
    {
      string sourceFileName = Path.GetDirectoryName(inputFile) + "\\temp.mp3";
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.supportLibs.mp3SplitPath;
      process.StartInfo.Arguments = "\"" + inputFile + "\" 0.0 EOF -n -x -d \"" + Path.GetDirectoryName(inputFile) + "\" -o temp";
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.UseShellExecute = false;
      process.Start();
      process.WaitForExit();
      string destFileName = Path.GetDirectoryName(inputFile) + "\\" + Path.GetFileNameWithoutExtension(inputFile) + ".mp3";
      File.Delete(inputFile);
      File.Move(sourceFileName, destFileName);
      return destFileName;
    }

    private string PackMp3File(string inputFile)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.supportLibs.mp3packerPath;
      process.StartInfo.Arguments = "-s -t -f --keep-ok out \"" + inputFile + "\"";
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.UseShellExecute = false;
      process.Start();
      process.WaitForExit();
      string sourceFileName = Path.GetDirectoryName(inputFile) + "\\" + Path.GetFileNameWithoutExtension(inputFile) + "-vbr.mp3";
      string destFileName = Path.GetDirectoryName(inputFile) + "\\" + Path.GetFileNameWithoutExtension(inputFile) + ".mp3";
      File.Move(sourceFileName, destFileName);
      return destFileName;
    }

    public int DecryptAAC(string file, string wFile, string DLLpath)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.instarip;
      process.StartInfo.Arguments = "\"" + file + "\" \"" + wFile + "\" \"" + Path.GetDirectoryName(DLLpath) + "\\AAXSDKWin.dll\"";
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardInput = true;
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      Audible.diskLogger("decrypt command = " + process.StartInfo.Arguments);
      process.Start();
      string end = process.StandardOutput.ReadToEnd();
      process.WaitForExit();
      if (process.ExitCode != 0)
        return process.ExitCode;
      string[] strArray = end.Split('\n');
      string str1 = "";
      string str2 = "";
      foreach (string str3 in strArray)
      {
        if (str3.StartsWith("IV"))
          str2 = str3.Split('=')[1].Trim();
        if (str3.StartsWith("Key ="))
          str1 = str3.Split('=')[1].Trim();
      }
      Audible.diskLogger("IV = " + str2 + ", Key = " + str1);
      this.AAXkey = str1;
      this.AAXiv = str2;
      return process.ExitCode;
    }

    public bool IncludesKeys(string file)
    {
      bool flag = false;
      string[] strArray = file.Split('_');
      int num = 0;
      foreach (string str in strArray)
      {
        if (str.ToLower().StartsWith("key="))
          ++num;
        if (str.ToLower().StartsWith("iv="))
          ++num;
      }
      if (num == 2)
        flag = true;
      return flag;
    }

    public int CustomtAAXDecrypt(string file, string wFile, string DLLpath)
    {
      string str1 = "";
      string str2 = "";
      if (this.IncludesKeys(file))
      {
        string str3 = file;
        char[] chArray = new char[1]{ '_' };
        foreach (string str4 in str3.Split(chArray))
        {
          if (str4.ToLower().StartsWith("key="))
            str1 = str4.Split('=')[1].Trim();
          if (str4.ToLower().StartsWith("iv="))
            str2 = str4.Split('=')[1].Trim();
        }
      }
      Process process1 = new Process();
      process1.StartInfo = new ProcessStartInfo();
      process1.StartInfo.FileName = this.instarip;
      process1.StartInfo.Arguments = "\"" + file + "\" \"" + wFile + "\" \"" + Path.GetDirectoryName(DLLpath) + "\\AAXSDKWin.dll\" -keyonly";
      process1.StartInfo.RedirectStandardOutput = true;
      process1.StartInfo.RedirectStandardError = true;
      process1.StartInfo.CreateNoWindow = true;
      process1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process1.StartInfo.UseShellExecute = false;
      process1.Start();
      string end = process1.StandardOutput.ReadToEnd();
      process1.WaitForExit();
      if (process1.ExitCode != 0 && str1 == "")
        return process1.ExitCode;
      string str5 = end;
      char[] chArray1 = new char[1]{ '\n' };
      foreach (string str3 in str5.Split(chArray1))
      {
        if (str3.StartsWith("IV"))
          str2 = str3.Split('=')[1].Trim();
        if (str3.StartsWith("Key ="))
          str1 = str3.Split('=')[1].Trim();
      }
      Audible.diskLogger("IV = " + str2 + ", Key = " + str1);
      this.AAXkey = str1;
      this.AAXiv = str2;
      try
      {
        File.Delete(Path.GetDirectoryName(wFile) + "\\funny.aac");
      }
      catch
      {
      }
      Process process2 = new Process();
      process2.StartInfo = new ProcessStartInfo();
      process2.StartInfo.FileName = this.trackDumpPath;
      process2.StartInfo.Arguments = "\"" + file + "\" \"" + wFile + "\"";
      process2.StartInfo.UseShellExecute = false;
      process2.StartInfo.RedirectStandardInput = true;
      process2.StartInfo.CreateNoWindow = true;
      process2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process2.StartInfo.EnvironmentVariables["KEY"] = str1;
      process2.StartInfo.EnvironmentVariables["IV"] = str2;
      process2.StartInfo.EnvironmentVariables["CHANNELS"] = this.channels.ToString();
      process2.StartInfo.EnvironmentVariables["SAMPLE_RATE"] = this.sampleRate.ToString();
      process2.StartInfo.WorkingDirectory = Path.GetDirectoryName(wFile);
      Audible.diskLogger("decrypt command = " + process2.StartInfo.Arguments);
      process2.Start();
      process2.WaitForExit();
      return process2.ExitCode;
    }

    public int ngDecrypt(string file, string wFile, string key)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.ngPath + "mp4trackdump.exe";
      process.StartInfo.Arguments = "-c " + (object) this.channels + " -r " + (object) this.sampleRate + " \"" + file + "\"";
      process.StartInfo.EnvironmentVariables["VARIABLE"] = key;
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.WorkingDirectory = Path.GetDirectoryName(wFile);
      process.Start();
      string end = process.StandardOutput.ReadToEnd();
      process.WaitForExit();
      if (end.Contains("checksums mismatch, aborting!"))
        return -99;
      return process.ExitCode;
    }

    public int ngDecryptFFmpeg(string file, string wFile, string key)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.ngPath + "ffmpeg.exe";
      process.StartInfo.Arguments = "-y -i \"" + file + "\"  -c:a copy \"" + wFile + "\"";
      process.StartInfo.EnvironmentVariables["ActivationBytes"] = key;
      process.StartInfo.RedirectStandardError = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.WorkingDirectory = Path.GetDirectoryName(wFile);
      process.Start();
      string end = process.StandardError.ReadToEnd();
      process.WaitForExit();
      if (end.Contains("[AAX] checksums mismatch"))
        return -99;
      return process.ExitCode;
    }

    public int ngVerifyKey(string file, string key)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.ffmpegPath;
      process.StartInfo.Arguments = "-y -i \"" + file + "\"";
      process.StartInfo.EnvironmentVariables["ActivationBytes"] = key;
      process.StartInfo.RedirectStandardError = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.UseShellExecute = false;
      process.Start();
      string end = process.StandardError.ReadToEnd();
      process.WaitForExit();
      return end.Contains("[AAX] checksums mismatch") ? -99 : 0;
    }

    private int AAX2Raw(string file, string wFile, string DLLpath)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.aax2wavPath;
      process.StartInfo.Arguments = "\"" + file + "\" \"" + wFile + "\" \"" + Path.GetDirectoryName(DLLpath) + "\\AAXSDKWin.dll\"";
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardInput = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      Audible.diskLogger("decrypt command = " + process.StartInfo.Arguments);
      process.Start();
      process.WaitForExit();
      return process.ExitCode;
    }

    private int AA2MP3(string file, string wFile)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = Path.GetDirectoryName(this.ngPath) + "\\AA-ng.exe";
      process.StartInfo.Arguments = "\"" + file + "\" \"" + wFile + "\"";
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardInput = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.Start();
      process.WaitForExit();
      return process.ExitCode;
    }

    public int DecryptAAXSegment(AAXDecrypterOptions myAAX)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.aax2wavPath;
      string destFileName = myAAX.wavFile + ".dll";
      File.Copy(Path.GetDirectoryName(myAAX.DLLPath) + "\\AAXSDKWin.dll", destFileName, true);
      process.StartInfo.Arguments = "\"" + myAAX.aaxFile + "\" \"" + myAAX.wavFile + "\" \"" + destFileName + "\" " + (object) myAAX.start + " " + (object) myAAX.end;
      process.StartInfo.UseShellExecute = false;
      Audible.diskLogger("decrypt command = " + process.StartInfo.Arguments);
      process.Start();
      process.WaitForExit();
      return process.ExitCode;
    }
  }
}
