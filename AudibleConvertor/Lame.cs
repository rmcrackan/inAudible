// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.Lame
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TagLib;
using TagLib.Id3v2;

namespace AudibleConvertor
{
  internal class Lame
  {
    public int bitrate = 64;
    public int channels = 2;
    public int sampleRate = 44100;
    public int vbrQuality = 5;
    public LibMp3Lame.MPEG_mode outputMode = LibMp3Lame.MPEG_mode.JOINT_STEREO;
    public int pcmBufferSize = 1048576;
    public AdvancedOptions myAdvancedOptions = new AdvancedOptions();
    public bool vbr;
    private LibMp3Lame mp3lib;
    public string outputFile;
    private BinaryWriter bw;
    public string lamePath;
    public string helixPath;
    public string lameCLIoptions;
    public string ffmpegPath;
    public string neroAACpath;
    public string opusPath;
    public string oggPath;
    public string soxPath;
    public SupportLibraries mySupportLibs;
    public int percentComplete;
    public bool m4bThread;
    public bool cancel;

    public Lame()
    {
      this.mp3lib = new LibMp3Lame();
    }

    internal void SetMono()
    {
      this.outputMode = LibMp3Lame.MPEG_mode.MONO;
    }

    internal void SetStereo()
    {
      this.outputMode = LibMp3Lame.MPEG_mode.JOINT_STEREO;
    }

    internal void prepareFiles()
    {
      System.IO.File.Delete(this.outputFile);
      this.bw = new BinaryWriter((Stream) new FileStream(this.outputFile, FileMode.Create));
    }

    internal void SetVBRMode()
    {
      this.mp3lib.LameInit();
      this.mp3lib.LameSetMode(this.outputMode);
      this.mp3lib.LameSetVBRtag(1);
      this.mp3lib.LameSetVBRQuality(this.vbrQuality);
      this.mp3lib.LameSetVBR(1);
      this.mp3lib.LameSetInSampleRate(this.channels * this.sampleRate);
      this.mp3lib.LameSetOutSampleRate(this.sampleRate);
      this.mp3lib.LameSetNumChannels(this.channels);
      this.mp3lib.LameSetScale(2f);
      this.mp3lib.LameInitParams();
    }

    internal void SetCBRMode()
    {
      this.mp3lib.LameInit();
      this.mp3lib.LameSetMode(this.outputMode);
      this.mp3lib.LameSetBRate(this.bitrate);
      this.mp3lib.LameSetInSampleRate(this.channels * this.sampleRate);
      this.mp3lib.LameSetOutSampleRate(this.sampleRate);
      this.mp3lib.LameSetNumChannels(this.channels);
      this.mp3lib.LameSetScale(2f);
      this.mp3lib.LameInitParams();
    }

    public static byte[] getBytes(string fileName, long start, int count)
    {
      using (BinaryReader binaryReader = new BinaryReader((Stream) System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
      {
        binaryReader.BaseStream.Seek(start, SeekOrigin.Begin);
        return binaryReader.ReadBytes(count);
      }
    }

    private IEnumerable<short> getShorts(string fileName, int start, int count)
    {
      List<short> shortList = new List<short>(count);
      using (FileStream fileStream = System.IO.File.OpenRead(fileName))
      {
        fileStream.Seek((long) start, SeekOrigin.Begin);
        BinaryReader binaryReader = new BinaryReader((Stream) fileStream);
        for (int index = 0; index < count; ++index)
          shortList.Add(binaryReader.ReadInt16());
      }
      return (IEnumerable<short>) shortList;
    }

    internal void SetOutputFile(string outputMP3)
    {
      this.outputFile = outputMP3;
    }

    internal void EncodePCM(SourcePCM wavFile)
    {
      long start = wavFile.start;
      while (start < wavFile.end)
      {
        int length1 = this.pcmBufferSize / 2;
        if (start + (long) this.pcmBufferSize > wavFile.end)
          length1 = (int) ((wavFile.end - start) / 2L);
        IEnumerable<short> shorts = this.getShorts(wavFile.fileName, (int) start, length1);
        short[] bufferL = new short[length1];
        short[] bufferR = new short[length1];
        int index = 0;
        foreach (short num in shorts)
        {
          if (index % 2 == 0)
            bufferL[index] = num;
          else
            bufferR[index] = num;
          ++index;
        }
        byte[] mp3Buffer = new byte[length1];
        int length2 = this.mp3lib.LameEncodeBuffer(bufferL, bufferR, length1, mp3Buffer);
        byte[] numArray = new byte[length2];
        Array.Copy((Array) mp3Buffer, (Array) numArray, length2);
        foreach (byte num in numArray)
          this.bw.Write(num);
        start += (long) this.pcmBufferSize;
      }
    }

    internal void Close()
    {
      byte[] mp3Buffer = new byte[this.pcmBufferSize / 2];
      int length = this.mp3lib.LameEncodeFlush(mp3Buffer);
      byte[] numArray = new byte[length];
      Array.Copy((Array) mp3Buffer, (Array) numArray, length);
      foreach (byte num in numArray)
        this.bw.Write(num);
      this.bw.Close();
    }

    internal int EncodeVirtualWav(VirtualWAV myVirtualWav, long start, long end)
    {
      long num1 = (long) (myVirtualWav.sampleRate * myVirtualWav.channels * myVirtualWav.bitsPerChannel / 8);
      long start1 = start * num1;
      long end1 = end * num1;
      foreach (SourcePCM sourcePcm in myVirtualWav.GetPCMcollection(start1, end1))
      {
        long start2 = sourcePcm.start;
        while (start2 < sourcePcm.end)
        {
          int length1 = this.pcmBufferSize / 2;
          if (start2 + (long) this.pcmBufferSize > sourcePcm.end)
            length1 = (int) ((sourcePcm.end - start2) / 2L);
          IEnumerable<short> shorts = this.getShorts(sourcePcm.fileName, (int) start2, length1);
          short[] bufferL = new short[length1];
          short[] bufferR = new short[length1];
          int index = 0;
          foreach (short num2 in shorts)
          {
            if (index % 2 == 0)
              bufferL[index] = num2;
            else
              bufferR[index] = num2;
            ++index;
          }
          byte[] mp3Buffer = new byte[length1];
          int length2 = this.mp3lib.LameEncodeBuffer(bufferL, bufferR, length1, mp3Buffer);
          byte[] numArray = new byte[length2];
          Array.Copy((Array) mp3Buffer, (Array) numArray, length2);
          foreach (byte num2 in numArray)
            this.bw.Write(num2);
          start2 += (long) this.pcmBufferSize;
        }
      }
      this.Close();
      return 0;
    }

    internal int LAMEencodeVirtualWav(VirtualWAV myVirtualWav, long start, long end, string fileName)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.lamePath;
      process.StartInfo.Arguments = "- -r " + this.lameCLIoptions + " \"" + fileName + "\"";
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardInput = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.Start();
      BinaryWriter binaryWriter = new BinaryWriter(process.StandardInput.BaseStream);
      long num1 = (long) (myVirtualWav.sampleRate * myVirtualWav.channels * myVirtualWav.bitsPerChannel / 8);
      long start1 = start * num1;
      long end1 = end * num1;
      List<SourcePCM> pcMcollection = myVirtualWav.GetPCMcollection(start1, end1);
      long num2 = 0;
      foreach (SourcePCM sourcePcm in pcMcollection)
      {
        long start2 = sourcePcm.start;
        while (start2 < sourcePcm.end)
        {
          int count = this.pcmBufferSize;
          num2 += (long) this.pcmBufferSize;
          this.percentComplete = (int) ((double) num2 / (double) end1 * 100.0);
          if (start2 + (long) this.pcmBufferSize > sourcePcm.end)
            count = (int) (sourcePcm.end - start2);
          byte[] bytes = Lame.getBytes(sourcePcm.fileName, (long) (int) start2, count);
          binaryWriter.Write(bytes);
          binaryWriter.Flush();
          start2 += (long) this.pcmBufferSize;
        }
      }
      binaryWriter.Close();
      return 1;
    }

    internal int FLACencodeVirtualWav(VirtualWAV myVirtualWav, long start, long end, string fileName)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.ffmpegPath;
      process.StartInfo.Arguments = "-y -i \"" + myVirtualWav.aacFile + "\" -c:a flac \"" + fileName + "\"";
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.RedirectStandardError = true;
      process.Start();
      if (!myVirtualWav.aacMode)
        return 1;
      long num1 = (long) ((double) myVirtualWav.sampleRate * (double) myVirtualWav.channels * 2.0 * (double) (end - start));
      long num2 = 0;
      StreamReader streamReader = new StreamReader(process.StandardError.BaseStream);
      while (!process.HasExited)
      {
        Thread.Sleep(200);
        process.Refresh();
        string str = streamReader.ReadLine();
        if (str.StartsWith("size="))
          num2 = long.Parse(str.Split('=')[1].Trim().Split('k')[0]) * 1024L;
        this.percentComplete = (int) ((double) num2 / (double) num1 * 100.0);
      }
      return 1;
    }

    internal int M4BencodeVirtualWav(VirtualWAV myVirtualWav, long start, long end, string fileName, int bitrate)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.neroAACpath;
      process.StartInfo.Arguments = "-ignorelength -br " + (object) bitrate + " -if - -of \"" + fileName + "\"";
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardInput = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.Start();
      BinaryWriter binaryWriter = new BinaryWriter(process.StandardInput.BaseStream);
      long num1 = (long) (myVirtualWav.sampleRate * myVirtualWav.channels * myVirtualWav.bitsPerChannel / 8);
      long start1 = start * num1;
      long end1 = end * num1;
      List<SourcePCM> pcMcollection = myVirtualWav.GetPCMcollection(start1, end1);
      binaryWriter.Write(myVirtualWav.Get44kStereoHeader());
      binaryWriter.Flush();
      long num2 = 0;
      foreach (SourcePCM sourcePcm in pcMcollection)
      {
        long start2 = sourcePcm.start;
        while (start2 < sourcePcm.end)
        {
          int count = this.pcmBufferSize;
          num2 += (long) this.pcmBufferSize;
          this.percentComplete = (int) ((double) num2 / (double) end1 * 100.0);
          if (start2 + (long) this.pcmBufferSize > sourcePcm.end)
            count = (int) (sourcePcm.end - start2);
          byte[] bytes = Lame.getBytes(sourcePcm.fileName, (long) (int) start2, count);
          binaryWriter.Write(bytes);
          binaryWriter.Flush();
          start2 += (long) this.pcmBufferSize;
        }
      }
      binaryWriter.Close();
      return 1;
    }

    private void ThreadedM4B(BinaryReader reader, string fileName, byte[] header, M4BOptions m4bOptions)
    {
      this.m4bThread = true;
      string encodingOptions = "";
      encodingOptions = !m4bOptions.vbr ? "-br " + (object) m4bOptions.bitrate : "-q " + (object) m4bOptions.quality;
      encodingOptions = encodingOptions + " " + this.myAdvancedOptions.GetCodecOptions("NeroAAC") + " ";
      new Thread((ThreadStart) (() =>
      {
        Process process = new Process();
        process.StartInfo = new ProcessStartInfo();
        process.StartInfo.FileName = this.neroAACpath;
        process.StartInfo.Arguments = "-ignorelength " + encodingOptions + " -if - -of \"" + fileName + "\"";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.Start();
        BinaryWriter binaryWriter = new BinaryWriter(process.StandardInput.BaseStream);
        binaryWriter.Write(header);
        binaryWriter.Flush();
        int count = 1;
        byte[] buffer = new byte[4096];
        while (count > 0)
        {
          count = reader.Read(buffer, 0, buffer.Length);
          binaryWriter.Write(buffer, 0, count);
          binaryWriter.Flush();
        }
        Audible.diskLogger("M4B thread ended");
        binaryWriter.Close();
        this.m4bThread = false;
      })).Start();
    }

    private void ThreadedFDK(BinaryReader reader, string fileName, EncodingOptions myEncodingOptions)
    {
      string encodingOptions = "";
      if (myEncodingOptions.m4bOptions.bitrate == 32)
        encodingOptions = "--profile 29";
      if (myEncodingOptions.m4bOptions.bitrate == 32 && myEncodingOptions.channels == 1)
        encodingOptions = "--profile 5";
      else if (myEncodingOptions.m4bOptions.bitrate == 64)
        encodingOptions = "--profile 5";
      if (myEncodingOptions.m4bOptions.vbr)
        encodingOptions = encodingOptions + " --bitrate-mode " + (object) myEncodingOptions.m4bOptions.quality;
      else
        encodingOptions = encodingOptions + " --bitrate " + (object) myEncodingOptions.m4bOptions.bitrate + "000 ";
      encodingOptions = encodingOptions + " " + this.myAdvancedOptions.GetCodecOptions("FDK") + " ";
      this.m4bThread = true;
      new Thread((ThreadStart) (() =>
      {
        Process process = new Process();
        process.StartInfo = new ProcessStartInfo();
        process.StartInfo.FileName = Path.GetDirectoryName(this.ffmpegPath) + "\\fdkaac.exe";
        process.StartInfo.Arguments = "--raw --raw-format S16L --raw-channels " + (object) myEncodingOptions.channels + " --raw-rate " + (object) myEncodingOptions.sampleRate + " " + encodingOptions + " -o \"" + fileName + "\" -";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.Start();
        BinaryWriter binaryWriter = new BinaryWriter(process.StandardInput.BaseStream);
        int count = 1;
        byte[] buffer = new byte[4096];
        while (count > 0)
        {
          count = reader.Read(buffer, 0, buffer.Length);
          binaryWriter.Write(buffer, 0, count);
          binaryWriter.Flush();
        }
        Audible.diskLogger("M4B thread ended");
        binaryWriter.Close();
        this.m4bThread = false;
      })).Start();
    }

    private void ThreadedFLAC(BinaryReader reader, string fileName, EncodingOptions myEncodingOptions)
    {
      this.m4bThread = true;
      string encodingOptions = " " + this.myAdvancedOptions.GetCodecOptions("FLAC") + " ";
      new Thread((ThreadStart) (() =>
      {
        Process process = new Process();
        process.StartInfo = new ProcessStartInfo();
        process.StartInfo.FileName = this.ffmpegPath;
        process.StartInfo.Arguments = "-y -f s16le -ac " + (object) myEncodingOptions.channels + " -ar " + (object) myEncodingOptions.sampleRate + " -i pipe:0 -c:a flac " + encodingOptions + "\"" + fileName + "\"";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.Start();
        BinaryWriter binaryWriter = new BinaryWriter(process.StandardInput.BaseStream);
        int count = 1;
        byte[] buffer = new byte[4096];
        while (count > 0)
        {
          count = reader.Read(buffer, 0, buffer.Length);
          binaryWriter.Write(buffer, 0, count);
          binaryWriter.Flush();
        }
        Audible.diskLogger("M4B thread ended");
        binaryWriter.Close();
        this.m4bThread = false;
      })).Start();
    }

    private void ThreadedFFmpeg(BinaryReader reader, string fileName, EncodingOptions myEncodingOptions, string codec)
    {
      this.m4bThread = true;
      string encodingOptions = " ";
      string str = codec.Split('_')[1];
      string ffmpegCodecName = "";
      if (str == "mp3")
      {
        ffmpegCodecName = "libmp3lame";
        if (this.vbr)
          encodingOptions = encodingOptions + " -q:a " + (object) this.vbrQuality;
        else
          encodingOptions = encodingOptions + " -b:a " + (object) this.bitrate + "k ";
      }
      if (str == "aac")
      {
        ffmpegCodecName = "aac";
        if (this.vbr)
          encodingOptions = encodingOptions + " -q:a " + (object) this.vbrQuality;
        else
          encodingOptions = encodingOptions + " -b:a " + (object) this.bitrate + "k ";
      }
      new Thread((ThreadStart) (() =>
      {
        Process process = new Process();
        process.StartInfo = new ProcessStartInfo();
        process.StartInfo.FileName = this.ffmpegPath;
        process.StartInfo.Arguments = "-y -f s16le -ac " + (object) myEncodingOptions.channels + " -ar " + (object) myEncodingOptions.sampleRate + " -i pipe:0 -codec:a " + ffmpegCodecName + " " + encodingOptions + "\"" + fileName + "\"";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.Start();
        BinaryWriter binaryWriter = new BinaryWriter(process.StandardInput.BaseStream);
        int count = 1;
        byte[] buffer = new byte[4096];
        while (count > 0)
        {
          count = reader.Read(buffer, 0, buffer.Length);
          binaryWriter.Write(buffer, 0, count);
          binaryWriter.Flush();
        }
        Audible.diskLogger("FFMPEG thread ended");
        binaryWriter.Close();
        if (!myEncodingOptions.doNotTag)
        {
          while (this.IsFileLocked(fileName))
            Thread.Sleep(200);
          this.tagAndRenameMP3TagLib(fileName, myEncodingOptions.audible, myEncodingOptions);
        }
        this.m4bThread = false;
      })).Start();
    }

    private void ThreadedWAV(BinaryReader reader, string fileName, EncodingOptions myEncodingOptions)
    {
      this.m4bThread = true;
      new Thread((ThreadStart) (() =>
      {
        Process process = new Process();
        process.StartInfo = new ProcessStartInfo();
        process.StartInfo.FileName = this.soxPath;
        fileName = Path.GetDirectoryName(fileName) + "\\" + Path.GetFileNameWithoutExtension(fileName) + " - .wav";
        process.StartInfo.Arguments = "-t raw -b 16 -e signed -c " + (object) myEncodingOptions.channels + " -r " + (object) myEncodingOptions.sampleRate + " - \"" + fileName + "\" --show-progress trim 0 4740 : newfile : restart";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.Start();
        BinaryWriter binaryWriter = new BinaryWriter(process.StandardInput.BaseStream);
        int count = 1;
        byte[] buffer = new byte[4096];
        while (count > 0)
        {
          count = reader.Read(buffer, 0, buffer.Length);
          binaryWriter.Write(buffer, 0, count);
          binaryWriter.Flush();
        }
        Audible.diskLogger("M4B thread ended");
        binaryWriter.Close();
        this.m4bThread = false;
      })).Start();
    }

    private void ThreadedWAVSox(BinaryReader reader, string fileName, EncodingOptions myEncodingOptions)
    {
      this.m4bThread = true;
      new Thread((ThreadStart) (() =>
      {
        Process process = new Process();
        process.StartInfo = new ProcessStartInfo();
        process.StartInfo.FileName = this.soxPath;
        process.StartInfo.Arguments = "-t raw -b 16 -e signed -c " + (object) myEncodingOptions.channels + " -r " + (object) myEncodingOptions.sampleRate + " - \"" + fileName + "\"";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.Start();
        BinaryWriter binaryWriter = new BinaryWriter(process.StandardInput.BaseStream);
        int count = 1;
        byte[] buffer = new byte[4096];
        while (count > 0)
        {
          count = reader.Read(buffer, 0, buffer.Length);
          binaryWriter.Write(buffer, 0, count);
          binaryWriter.Flush();
        }
        Audible.diskLogger("M4B thread ended");
        binaryWriter.Close();
        this.m4bThread = false;
      })).Start();
    }

    private void ThreadedWAVfull(BinaryReader reader, string fileName, EncodingOptions myEncodingOptions)
    {
      this.m4bThread = true;
      string str = " " + this.myAdvancedOptions.GetCodecOptions("FLAC") + " ";
      new Thread((ThreadStart) (() =>
      {
        Process process = new Process();
        process.StartInfo = new ProcessStartInfo();
        process.StartInfo.FileName = this.ffmpegPath;
        process.StartInfo.Arguments = "-y -f s16le -ac " + (object) myEncodingOptions.channels + " -ar " + (object) myEncodingOptions.sampleRate + " -i pipe:0 \"" + fileName + "\"";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.Start();
        BinaryWriter binaryWriter = new BinaryWriter(process.StandardInput.BaseStream);
        int count = 1;
        byte[] buffer = new byte[4096];
        while (count > 0)
        {
          count = reader.Read(buffer, 0, buffer.Length);
          binaryWriter.Write(buffer, 0, count);
          binaryWriter.Flush();
        }
        Audible.diskLogger("M4B thread ended");
        binaryWriter.Close();
        this.m4bThread = false;
      })).Start();
    }

    private void ThreadedWAVbyChapters(BinaryReader reader, string fileName, EncodingOptions myEncodingOptions)
    {
      this.m4bThread = true;
      new Thread((ThreadStart) (() =>
      {
        Process process = new Process();
        process.StartInfo = new ProcessStartInfo();
        process.StartInfo.FileName = this.soxPath;
        process.StartInfo.Arguments = "-t raw -b 16 -e signed -c " + (object) myEncodingOptions.channels + " -r " + (object) myEncodingOptions.sampleRate + " - \"" + fileName + "\"";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.Start();
        BinaryWriter binaryWriter = new BinaryWriter(process.StandardInput.BaseStream);
        int count = 1;
        byte[] buffer = new byte[4096];
        while (count > 0)
        {
          count = reader.Read(buffer, 0, buffer.Length);
          binaryWriter.Write(buffer, 0, count);
          binaryWriter.Flush();
        }
        Audible.diskLogger("M4B thread ended");
        binaryWriter.Close();
        this.m4bThread = false;
      })).Start();
    }

    private void ThreadedSoxSilence(BinaryReader reader, string fileName, EncodingOptions myEncodingOptions)
    {
      this.m4bThread = true;
      Task.Factory.StartNew((Action) (() =>
      {
        Process process = new Process();
        process.StartInfo = new ProcessStartInfo();
        process.StartInfo.FileName = "cmd";
        process.StartInfo.Arguments = "/C \" \"" + this.soxPath + "\" -t raw -b 16 -e signed -c " + (object) myEncodingOptions.channels + " -r " + (object) myEncodingOptions.sampleRate + " - -n --show-progress silence 1 0 1% 1 " + string.Format("{0:0.0}", (object) myEncodingOptions.silenceThreshold) + " 1% : newfile : restart 2>\"" + fileName + "\" \"";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.Start();
        BinaryWriter binaryWriter = new BinaryWriter(process.StandardInput.BaseStream);
        int count = 1;
        byte[] buffer = new byte[4096];
        while (count > 0)
        {
          count = reader.Read(buffer, 0, buffer.Length);
          binaryWriter.Write(buffer, 0, count);
          binaryWriter.Flush();
        }
        Audible.diskLogger("SOX thread ended");
        binaryWriter.Close();
        this.m4bThread = false;
      }));
    }

    private void ThreadedSoxDetectSilence(BinaryReader reader, string fileName, EncodingOptions myEncodingOptions)
    {
      this.m4bThread = true;
      Task.Factory.StartNew((Action) (() =>
      {
        Process process = new Process();
        process.StartInfo = new ProcessStartInfo();
        process.StartInfo.FileName = "cmd";
        process.StartInfo.Arguments = "/C \" \"" + this.soxPath + "\" -t raw -b 16 -e signed -c " + (object) myEncodingOptions.channels + " -r " + (object) myEncodingOptions.sampleRate + " - -n stats 2>\"" + fileName + "\" \"";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.Start();
        BinaryWriter binaryWriter = new BinaryWriter(process.StandardInput.BaseStream);
        int count = 1;
        byte[] buffer = new byte[4096];
        while (count > 0)
        {
          count = reader.Read(buffer, 0, buffer.Length);
          binaryWriter.Write(buffer, 0, count);
          binaryWriter.Flush();
        }
        Audible.diskLogger("SOX thread ended");
        binaryWriter.Close();
        this.m4bThread = false;
      }));
    }

    private void Normalize(BinaryReader reader, string fileName, EncodingOptions myEncodingOptions)
    {
      this.m4bThread = true;
      Task.Factory.StartNew((Action) (() =>
      {
        Process process = new Process();
        process.StartInfo = new ProcessStartInfo();
        process.StartInfo.FileName = "cmd";
        process.StartInfo.Arguments = "/C \" \"" + this.mySupportLibs.ffmpegPath + "\" -y -f s16le -ac " + (object) myEncodingOptions.channels + " -ar " + (object) myEncodingOptions.sampleRate + " -i pipe:0 -af volume=-12dB,volumedetect -f null NUL 2>\"" + fileName + "\" \"";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.Start();
        BinaryWriter binaryWriter = new BinaryWriter(process.StandardInput.BaseStream);
        int count = 1;
        byte[] buffer = new byte[4096];
        while (count > 0)
        {
          count = reader.Read(buffer, 0, buffer.Length);
          binaryWriter.Write(buffer, 0, count);
          binaryWriter.Flush();
        }
        Audible.diskLogger("Normalize thread ended");
        binaryWriter.Close();
        this.m4bThread = false;
      }));
    }

    private void ThreadedSoxPlay(BinaryReader reader, string fileName, EncodingOptions myEncodingOptions)
    {
      this.m4bThread = true;
      Task.Factory.StartNew((Action) (() =>
      {
        Process process = new Process();
        process.StartInfo = new ProcessStartInfo();
        process.StartInfo.FileName = "cmd";
        process.StartInfo.Arguments = "/C \" \"" + this.soxPath + "\" -t raw -b 16 -e signed -c " + (object) 2 + " -r " + (object) 44100 + " - -d\"";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.Start();
        BinaryWriter binaryWriter = new BinaryWriter(process.StandardInput.BaseStream);
        int count = 1;
        byte[] buffer = new byte[4096];
        while (count > 0)
        {
          count = reader.Read(buffer, 0, buffer.Length);
          binaryWriter.Write(buffer, 0, count);
          binaryWriter.Flush();
        }
        Audible.diskLogger("SOX thread ended");
        binaryWriter.Close();
        this.m4bThread = false;
      }));
    }

    private void tagAndRenameMP3TagLib(string fileName, Audible myAudible, EncodingOptions myEncodingOptions)
    {
      TagLib.File file = TagLib.File.Create(fileName);
      string str = myAudible.title;
      if (myAudible.addTrackToTitle)
        str = str + " - " + myEncodingOptions.trackNum.ToString("D3");
      if (myEncodingOptions.audible.newChaps.useAsTitle)
        str = myEncodingOptions.audible.newChaps.GetChapter(myEncodingOptions.trackNum - 1).description;
      if (myEncodingOptions.audible.newChaps.useTrackAndTitleAsTitle)
        str = myEncodingOptions.audible.title + " - " + myEncodingOptions.audible.newChaps.GetChapter(myEncodingOptions.trackNum - 1).description;
      file.Tag.Title = str;
      if (myEncodingOptions.embedCover)
      {
        IPicture picture = (IPicture) new Picture(myAudible.coverPath);
        file.Tag.Pictures = new IPicture[1]{ picture };
      }
      file.Tag.Performers = new string[1]
      {
        myAudible.author
      };
      file.Tag.Album = myAudible.album;
      if (myAudible.year != "")
        file.Tag.Year = uint.Parse(myAudible.year);
      file.Tag.Comment = myAudible.GetComments();
      file.Tag.Composers = new string[1]
      {
        myEncodingOptions.audible.narrator
      };
      file.Tag.Track = (uint) myEncodingOptions.trackNum;
      file.Tag.TrackCount = (uint) (myEncodingOptions.chapters.Count() - 1);
      if (myEncodingOptions.audible.genre != null)
        file.Tag.Genres = new string[1]
        {
          myEncodingOptions.audible.genre
        };
      if (Path.GetExtension(fileName) == ".mp3")
      {
        TagLib.Id3v2.Tag tag = (TagLib.Id3v2.Tag) file.GetTag(TagTypes.Id3v2, false);
        if (tag != null)
          tag.Version = (byte) 3;
        UserTextInformationFrame informationFrame1 = new UserTextInformationFrame("UFID", StringType.UTF16);
        informationFrame1.Text = new string[1]
        {
          myEncodingOptions.audible.bookId
        };
        tag.AddFrame((Frame) informationFrame1);
        UserTextInformationFrame informationFrame2 = new UserTextInformationFrame("NARRATEDBY", StringType.UTF16);
        informationFrame2.Text = new string[1]
        {
          myEncodingOptions.audible.narrator
        };
        tag.AddFrame((Frame) informationFrame2);
        UserTextInformationFrame informationFrame3 = new UserTextInformationFrame("WOAS", StringType.UTF16);
        informationFrame3.Text = new string[1]
        {
          "http://www.audible.com/pd/" + myEncodingOptions.audible.id
        };
        tag.AddFrame((Frame) informationFrame3);
        if (myEncodingOptions.audible.publisher != null && myEncodingOptions.audible.publisher != "")
        {
          UserTextInformationFrame informationFrame4 = new UserTextInformationFrame("Publisher", StringType.UTF16);
          informationFrame4.Text = new string[1]
          {
            myEncodingOptions.audible.publisher
          };
          tag.AddFrame((Frame) informationFrame4);
        }
      }
      file.Save();
    }

    private void tagAndRenameMP3(string inputFile, string outputFile, Audible myAudible, EncodingOptions myEncodingOptions)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.ffmpegPath;
      bool flag = false;
      if (inputFile == outputFile)
      {
        flag = true;
        outputFile = Path.GetDirectoryName(outputFile) + "\\" + Path.GetFileNameWithoutExtension(outputFile) + "-tmp.mp3";
      }
      string str1 = "Narrated by " + myAudible.narrator + ". " + myAudible.GetComments();
      string str2 = myAudible.title;
      if (myAudible.addTrackToTitle)
        str2 = str2 + " - " + myEncodingOptions.trackNum.ToString("D3");
      if (myEncodingOptions.audible.newChaps.useAsTitle)
        str2 = myEncodingOptions.audible.newChaps.GetChapter(myEncodingOptions.trackNum - 1).description;
      if (myEncodingOptions.audible.newChaps.useTrackAndTitleAsTitle)
        str2 = myEncodingOptions.audible.title + " - " + myEncodingOptions.audible.newChaps.GetChapter(myEncodingOptions.trackNum - 1).description;
      if (!myEncodingOptions.embedCover)
        process.StartInfo.Arguments = string.Format("-y -loglevel panic -i \"{0}\" -id3v2_version 3 -write_id3v1 1 -metadata title=\"{2}\" -metadata artist=\"{3}\" -metadata album=\"{4}\" -metadata date=\"{5}\" -metadata track=\"{6}\" -metadata comment=\"{7}\" -acodec copy \"{1}\"", (object) inputFile, (object) outputFile, (object) str2, (object) myAudible.author, (object) myAudible.album, (object) myAudible.year, (object) myEncodingOptions.trackNum, (object) str1);
      else
        process.StartInfo.Arguments = string.Format("-y -loglevel panic -i \"{0}\" -i \"{7}\" -map 0:0 -map 1:0 -c copy -id3v2_version 3 -metadata:s:v title=\"Album cover\" -metadata:s:v comment=\"Cover (Front)\" -write_id3v1 1 -metadata title=\"{2}\" -metadata artist=\"{3}\" -metadata album=\"{4}\" -metadata date=\"{5}\" -metadata track=\"{6}\" -metadata comment=\"{8}\" -acodec copy \"{1}\"", (object) inputFile, (object) outputFile, (object) str2, (object) myAudible.author, (object) myAudible.album, (object) myAudible.year, (object) myEncodingOptions.trackNum, (object) myAudible.coverPath, (object) str1);
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.UseShellExecute = false;
      Audible.diskLogger(process.StartInfo.FileName.ToString() + " " + process.StartInfo.Arguments.ToString());
      process.Start();
      process.WaitForExit();
      int exitCode = process.ExitCode;
      if (!flag)
        return;
      Form1.SafeRename(outputFile, inputFile);
    }

    private void ThreadedLAME(BinaryReader reader, string fileName, string lameOptions, EncodingOptions myEncodingOptions)
    {
      this.m4bThread = true;
      new Thread((ThreadStart) (() =>
      {
        string str1 = "";
        if (myEncodingOptions.sampleRate == 44100)
          str1 = "-s 44.1 ";
        if (myEncodingOptions.sampleRate == 22050)
          str1 = "-s 22.05 ";
        if (myEncodingOptions.channels == 1)
          str1 += " -m m ";
        string str2 = myEncodingOptions.audible.title;
        if (myEncodingOptions.audible.addTrackToTitle)
          str2 = str2 + " - " + myEncodingOptions.trackNum.ToString("D3");
        if (myEncodingOptions.audible.newChaps.useAsTitle)
          str2 = myEncodingOptions.audible.newChaps.GetChapter(myEncodingOptions.trackNum - 1).description;
        if (myEncodingOptions.audible.newChaps.useTrackAndTitleAsTitle)
          str2 = myEncodingOptions.audible.title + " - " + myEncodingOptions.audible.newChaps.GetChapter(myEncodingOptions.trackNum - 1).description;
        string str3 = " --add-id3v2 --pad-id3v2 --ignore-tag-errors --ta \"" + myEncodingOptions.audible.author.Replace("\"", "\\\"") + "\" --tt \"" + str2.Replace("\"", "\\\"") + "\" --tl \"" + myEncodingOptions.audible.album.Replace("\"", "\\\"") + "\" --ty \"" + myEncodingOptions.audible.year + "\" --tn \"" + (object) myEncodingOptions.trackNum + "\" --tc \"" + myEncodingOptions.audible.GetComments().Replace("\"", "\\\"") + "\" ";
        if (myEncodingOptions.embedCover)
          str3 = str3 + " --ti \"" + myEncodingOptions.audible.coverPath + "\" ";
        if (myEncodingOptions.doNotTag)
          str3 = "";
        Process process = new Process();
        process.StartInfo = new ProcessStartInfo();
        process.StartInfo.FileName = this.lamePath;
        process.StartInfo.Arguments = "- -r " + str1 + " " + this.lameCLIoptions + " " + str3 + " \"" + fileName + "\"";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.Start();
        Audible.diskLogger("lame args = " + process.StartInfo.Arguments);
        BinaryWriter binaryWriter = new BinaryWriter(process.StandardInput.BaseStream);
        int count = 1;
        byte[] buffer = new byte[4096];
        while (count > 0)
        {
          count = reader.Read(buffer, 0, buffer.Length);
          binaryWriter.Write(buffer, 0, count);
          binaryWriter.Flush();
        }
        Audible.diskLogger("LAME thread ended");
        binaryWriter.Close();
        while (this.IsFileLocked(fileName))
          Thread.Sleep(200);
        try
        {
          TagLib.File file = TagLib.File.Create(fileName);
          file.Tag.Composers = new string[1]
          {
            myEncodingOptions.audible.narrator
          };
          file.Tag.TrackCount = (uint) (myEncodingOptions.chapters.Count() - 1);
          if (myEncodingOptions.audible.genre != null)
            file.Tag.Genres = new string[1]
            {
              myEncodingOptions.audible.genre
            };
          TagLib.Id3v2.Tag tag = (TagLib.Id3v2.Tag) file.GetTag(TagTypes.Id3v2);
          tag.AddFrame((Frame) new UserTextInformationFrame("UFID", StringType.UTF16)
          {
            Text = new string[1]
            {
              myEncodingOptions.audible.bookId
            }
          });
          tag.AddFrame((Frame) new UserTextInformationFrame("NARRATEDBY", StringType.UTF16)
          {
            Text = new string[1]
            {
              myEncodingOptions.audible.narrator
            }
          });
          tag.AddFrame((Frame) new UserTextInformationFrame("WOAS", StringType.UTF16)
          {
            Text = new string[1]
            {
              "http://www.audible.com/pd/" + myEncodingOptions.audible.id
            }
          });
          if (myEncodingOptions.audible.publisher != null && myEncodingOptions.audible.publisher != "")
            tag.AddFrame((Frame) new UserTextInformationFrame("Publisher", StringType.UTF16)
            {
              Text = new string[1]
              {
                myEncodingOptions.audible.publisher
              }
            });
          file.Save();
        }
        catch
        {
        }
        this.m4bThread = false;
      })).Start();
    }

    private void ThreadedHelix(BinaryReader reader, string fileName, string lameOptions, EncodingOptions myEncodingOptions)
    {
      this.m4bThread = true;
      new Thread((ThreadStart) (() =>
      {
        string str = (!myEncodingOptions.lameOptions.vbr ? "-B" + (object) (myEncodingOptions.lameOptions.bitrate / myEncodingOptions.channels) : "-V" + (object) myEncodingOptions.lameOptions.vbrQuality) + " " + this.myAdvancedOptions.GetCodecOptions("Helix") + " ";
        Process process = new Process();
        process.StartInfo = new ProcessStartInfo();
        process.StartInfo.FileName = this.helixPath;
        process.StartInfo.Arguments = "-U2 -X2 " + str + " - \"" + fileName + "\"";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.Start();
        BinaryWriter binaryWriter = new BinaryWriter(process.StandardInput.BaseStream);
        int count = 1;
        byte[] buffer = new byte[4096];
        while (count > 0)
        {
          count = reader.Read(buffer, 0, buffer.Length);
          binaryWriter.Write(buffer, 0, count);
          binaryWriter.Flush();
        }
        Audible.diskLogger("LAME thread ended");
        binaryWriter.Close();
        if (!myEncodingOptions.doNotTag)
        {
          while (this.IsFileLocked(fileName))
            Thread.Sleep(200);
          this.tagAndRenameMP3TagLib(fileName, myEncodingOptions.audible, myEncodingOptions);
        }
        this.m4bThread = false;
      })).Start();
    }

    private void ThreadedOpus(BinaryReader reader, string fileName, EncodingOptions myEncodingOptions)
    {
      this.m4bThread = true;
      new Thread((ThreadStart) (() =>
      {
        OpusOptions opusOptions = myEncodingOptions.opusOptions;
        long start = opusOptions.start;
        long end = opusOptions.end;
        Audible audible = opusOptions.myAudible;
        string encodingArgs = opusOptions.encodingArgs;
        string str1 = "- --raw --raw-rate ";
        string str2 = (myEncodingOptions.sampleRate != 44100 ? (object) (str1 + "22100 ") : (object) (str1 + "44100 ")).ToString() + " --raw-chan " + (object) myEncodingOptions.channels + " " + this.myAdvancedOptions.GetCodecOptions("Opus") + " ";
        Process process = new Process();
        process.StartInfo = new ProcessStartInfo();
        process.StartInfo.FileName = this.opusPath;
        process.StartInfo.Arguments = str2 + " " + encodingArgs + " --artist \"" + audible.author + "\" --title \"" + audible.title + "\" --album \"" + audible.title + "\" \"" + fileName + "\"";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.Start();
        BinaryWriter binaryWriter = new BinaryWriter(process.StandardInput.BaseStream);
        int count = 1;
        byte[] buffer = new byte[4096];
        while (count > 0)
        {
          count = reader.Read(buffer, 0, buffer.Length);
          binaryWriter.Write(buffer, 0, count);
          binaryWriter.Flush();
        }
        Audible.diskLogger("LAME thread ended");
        binaryWriter.Close();
        this.m4bThread = false;
      })).Start();
    }

    private void ThreadedOgg(BinaryReader reader, string fileName, EncodingOptions myEncodingOptions)
    {
      this.m4bThread = true;
      new Thread((ThreadStart) (() =>
      {
        OggOptions oggOptions = myEncodingOptions.oggOptions;
        VirtualWAV virtualWav = oggOptions.myVirtualWav;
        long start = oggOptions.start;
        long end = oggOptions.end;
        Audible audible = oggOptions.myAudible;
        string encodingArgs = oggOptions.encodingArgs;
        string str1 = "- -r -R ";
        string str2 = (myEncodingOptions.sampleRate != 44100 ? (object) (str1 + "22100 ") : (object) (str1 + "44100 ")).ToString() + " -C " + (object) myEncodingOptions.channels + " " + this.myAdvancedOptions.GetCodecOptions("Ogg") + " ";
        Process process = new Process();
        process.StartInfo = new ProcessStartInfo();
        process.StartInfo.FileName = this.oggPath;
        process.StartInfo.Arguments = str2 + " " + encodingArgs + " -N " + (object) oggOptions.trackNum + " -t \"" + audible.title + "\" -l \"" + audible.title + "\" -a \"" + audible.author + "\" -o \"" + fileName + "\"";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.Start();
        BinaryWriter binaryWriter = new BinaryWriter(process.StandardInput.BaseStream);
        int count = 1;
        byte[] buffer = new byte[4096];
        while (count > 0)
        {
          count = reader.Read(buffer, 0, buffer.Length);
          binaryWriter.Write(buffer, 0, count);
          binaryWriter.Flush();
        }
        Audible.diskLogger("LAME thread ended");
        binaryWriter.Close();
        this.m4bThread = false;
      })).Start();
    }

    internal int PreprocessVirtualWav(VirtualWAV myVirtualWav, string fileName, EncodingOptions myEncodingOptions)
    {
      string str1 = "";
      if (myEncodingOptions.downmix && myEncodingOptions.channels == 1 && myVirtualWav.channels == 2)
        str1 = "remix 1-2";
      else if (myEncodingOptions.channels == 1 || myVirtualWav.channels == 1)
        str1 = "remix 1";
      string str2 = "";
      if (myVirtualWav.DRC)
        str2 += " compand 0.3,1 -90,-90,-70,-70,-60,-20,0,0 -5 0 0.2 ";
      Audible.diskLogger("myEncodingOptions.startChap=" + (object) myEncodingOptions.startChap + ", myEncodingOptions.endChap=" + (object) myEncodingOptions.endChap);
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.soxPath;
      string str3 = " ";
      if (myVirtualWav.normalize)
        str3 = " -af \"volume=" + myVirtualWav.normalizeLevel.ToString("0.0", (IFormatProvider) CultureInfo.InvariantCulture) + "dB\" ";
      if (myVirtualWav.DRC)
        str3 = " -af \"compand=0 0:1 1:-90/-900 -70/-70 -30/-9 0/-3:6:0:0:0\" ";
      if (myEncodingOptions.encoder.ToLower() != "helix")
      {
        process.StartInfo.Arguments = "-t raw -b 16 -e signed -c " + (object) myVirtualWav.channels + " -r " + (object) myVirtualWav.sampleRate + " - -r " + (object) myEncodingOptions.sampleRate + " -c " + (object) myEncodingOptions.channels + " -t raw - " + str1 + str2;
      }
      else
      {
        if (myEncodingOptions.sampleRate == 44100 && myEncodingOptions.channels == 2 && !myEncodingOptions.lameOptions.vbr)
          myEncodingOptions.sampleRate = 22050;
        process.StartInfo.Arguments = "-t raw -b 16 -e signed -c " + (object) myVirtualWav.channels + " -r " + (object) myVirtualWav.sampleRate + " - -r " + (object) myEncodingOptions.sampleRate + " -c " + (object) myEncodingOptions.channels + " -t wav - " + str1 + str2;
      }
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardInput = true;
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.RedirectStandardError = true;
      process.StartInfo.CreateNoWindow = true;
      string path = "";
      if (myVirtualWav.aacMode || myVirtualWav.omni)
      {
        process.StartInfo.FileName = myVirtualWav.ffmpegPath;
        process.StartInfo.EnvironmentVariables["ActivationBytes"] = myVirtualWav.AAXkey;
        double num1 = (double) (myEncodingOptions.endChap - myEncodingOptions.startChap);
        double num2 = (double) myEncodingOptions.startChap;
        double endChap = (double) myEncodingOptions.endChap;
        if (myEncodingOptions.doubleChapters && !this.myAdvancedOptions.legacyChapterMode)
        {
          num2 = myEncodingOptions.dStartChap;
          num1 = myEncodingOptions.dEndChap - num2;
        }
        if (myEncodingOptions.encoder.ToLower() == "detectsilence")
          num1 = 0.2;
        string str4 = " -i \"" + myVirtualWav.aacFile + "\" ";
        if (myVirtualWav.omni)
        {
          path = myVirtualWav.GetFFmpegConcat();
          str4 = !myVirtualWav.panicMode ? " -f concat -i \"" + path + "\" " : " -loglevel panic -f concat -i \"" + path + "\" ";
        }
        process.StartInfo.Arguments = "-y -ss " + num2.ToString("0.000", (IFormatProvider) CultureInfo.InvariantCulture) + " -t " + num1.ToString("0.000", (IFormatProvider) CultureInfo.InvariantCulture) + str4 + " -ac " + (object) myEncodingOptions.channels + " -ar " + (object) myEncodingOptions.sampleRate + " -f s16le -acodec pcm_s16le " + str3 + " - ";
        if (myEncodingOptions.encoder.ToLower() == "helix")
        {
          if (myEncodingOptions.sampleRate == 44100 && myEncodingOptions.channels == 2 && !myEncodingOptions.lameOptions.vbr)
            myEncodingOptions.sampleRate = 22050;
          process.StartInfo.Arguments = "-y -ss " + num2.ToString("0.000", (IFormatProvider) CultureInfo.InvariantCulture) + " -t " + num1.ToString("0.000", (IFormatProvider) CultureInfo.InvariantCulture) + str4 + " -ac " + (object) myEncodingOptions.channels + " -ar " + (object) myEncodingOptions.sampleRate + " -f wav " + str3 + " - ";
        }
      }
      process.Start();
      Audible.diskLogger("resampling=" + process.StartInfo.Arguments);
      BinaryWriter binaryWriter = new BinaryWriter(process.StandardInput.BaseStream);
      BinaryReader reader = new BinaryReader(process.StandardOutput.BaseStream);
      if (myEncodingOptions.encoder.ToLower() == "nero")
      {
        if (myEncodingOptions.sampleRate == 44100 && myEncodingOptions.channels == 2)
          this.ThreadedM4B(reader, fileName, myVirtualWav.Get44kStereoHeader(), myEncodingOptions.m4bOptions);
        else if (myEncodingOptions.sampleRate == 44100 && myEncodingOptions.channels == 1)
          this.ThreadedM4B(reader, fileName, myVirtualWav.Get44kMonoHeader(), myEncodingOptions.m4bOptions);
        else if (myEncodingOptions.sampleRate == 22050 && myEncodingOptions.channels == 1)
          this.ThreadedM4B(reader, fileName, myVirtualWav.Get22kMonoHeader(), myEncodingOptions.m4bOptions);
        else if (myEncodingOptions.sampleRate == 22050 && myEncodingOptions.channels == 2)
          this.ThreadedM4B(reader, fileName, myVirtualWav.Get22kStereoHeader(), myEncodingOptions.m4bOptions);
      }
      else if (myEncodingOptions.encoder.ToLower() == "fdk")
        this.ThreadedFDK(reader, fileName, myEncodingOptions);
      else if (myEncodingOptions.encoder.ToLower() == "lame")
        this.ThreadedLAME(reader, fileName, this.lameCLIoptions, myEncodingOptions);
      else if (myEncodingOptions.encoder.ToLower() == "helix")
        this.ThreadedHelix(reader, fileName, this.lameCLIoptions, myEncodingOptions);
      else if (myEncodingOptions.encoder.ToLower() == "flac")
        this.ThreadedFLAC(reader, fileName, myEncodingOptions);
      else if (myEncodingOptions.encoder.ToLower().StartsWith("ffmpeg_"))
        this.ThreadedFFmpeg(reader, fileName, myEncodingOptions, myEncodingOptions.encoder.ToLower());
      else if (myEncodingOptions.encoder.ToLower() == "opus")
        this.ThreadedOpus(reader, fileName, myEncodingOptions);
      else if (myEncodingOptions.encoder.ToLower() == "ogg")
        this.ThreadedOgg(reader, fileName, myEncodingOptions);
      else if (myEncodingOptions.encoder.ToLower() == "soxsilence")
        this.ThreadedSoxSilence(reader, fileName, myEncodingOptions);
      else if (myEncodingOptions.encoder.ToLower() == "detectsilence")
        this.ThreadedSoxDetectSilence(reader, fileName, myEncodingOptions);
      else if (myEncodingOptions.encoder.ToLower() == "normalize")
        this.Normalize(reader, fileName, myEncodingOptions);
      else if (myEncodingOptions.encoder.ToLower() == "wav")
        this.ThreadedWAV(reader, fileName, myEncodingOptions);
      else if (myEncodingOptions.encoder.ToLower() == "fullwav")
        this.ThreadedWAVfull(reader, fileName, myEncodingOptions);
      else if (myEncodingOptions.encoder.ToLower() == "wavbychapters")
        this.ThreadedWAVbyChapters(reader, fileName, myEncodingOptions);
      else if (myEncodingOptions.encoder.ToLower() == "soxplay")
        this.ThreadedSoxPlay(reader, fileName, myEncodingOptions);
      if (myVirtualWav.aacMode || myVirtualWav.omni)
      {
        long num1 = (long) ((double) myEncodingOptions.sampleRate * (double) myEncodingOptions.channels * 2.0 * (double) (myEncodingOptions.endChap - myEncodingOptions.startChap));
        StreamReader streamReader = new StreamReader(process.StandardError.BaseStream);
        long num2 = 0;
        while (this.m4bThread)
        {
          Thread.Sleep(200);
          if (!myVirtualWav.omni)
          {
            string str4 = streamReader.ReadLine();
            long endChap = myEncodingOptions.endChap;
            long startChap = myEncodingOptions.startChap;
            if (str4 != null && str4.StartsWith("size="))
              num2 = long.Parse(str4.Split('=')[1].Trim().Split('k')[0]) * 1024L;
            this.percentComplete = (int) ((double) num2 / (double) num1 * 100.0);
          }
          else
          {
            try
            {
              if (!myVirtualWav.panicMode)
              {
                string str4 = streamReader.ReadLine();
                if (str4 != null)
                {
                  if (str4.StartsWith("size="))
                  {
                    string[] strArray = str4.Split('=')[2].Split(' ')[0].Split(':');
                    this.percentComplete = (int) ((double.Parse(strArray[0]) * 60.0 * 60.0 + double.Parse(strArray[1]) * 60.0 + double.Parse(strArray[2])) / myVirtualWav.totalSeconds * 100.0);
                  }
                }
              }
              else if (myEncodingOptions.audible != null)
              {
                num2 = new FileInfo(fileName).Length;
                long num3 = (long) (double.Parse(myEncodingOptions.audible.nfo.targetBitrate) / 8.0 * 1000.0 * (double) (myEncodingOptions.endChap - myEncodingOptions.startChap));
                this.percentComplete = (int) ((double) num2 / (double) num3 * 100.0);
              }
            }
            catch
            {
            }
          }
        }
        if (myVirtualWav.omni)
        {
          try
          {
            System.IO.File.Delete(path);
          }
          catch
          {
          }
        }
        return 1;
      }
      long num4 = (long) (myVirtualWav.sampleRate * myVirtualWav.channels * myVirtualWav.bitsPerChannel / 8);
      long start1 = myEncodingOptions.startChap * num4;
      long end = myEncodingOptions.endChap * num4;
      List<SourcePCM> pcMcollection = myVirtualWav.GetPCMcollection(start1, end);
      long num5 = 0;
      using (List<SourcePCM>.Enumerator enumerator = pcMcollection.GetEnumerator())
      {
label_90:
        while (enumerator.MoveNext())
        {
          SourcePCM current = enumerator.Current;
          if (this.cancel)
          {
            binaryWriter.Close();
            break;
          }
          long start2 = current.start;
          while (true)
          {
            if (start2 < current.end && !this.cancel)
            {
              int count = this.pcmBufferSize;
              num5 += (long) this.pcmBufferSize;
              this.percentComplete = (int) ((double) num5 / (double) end * 100.0);
              if (start2 + (long) this.pcmBufferSize > current.end)
                count = (int) (current.end - start2);
              byte[] bytes = Lame.getBytes(current.fileName, start2, count);
              binaryWriter.Write(bytes);
              binaryWriter.Flush();
              start2 += (long) this.pcmBufferSize;
            }
            else
              goto label_90;
          }
        }
      }
      binaryWriter.Close();
      while (this.m4bThread)
        Thread.Sleep(200);
      return 1;
    }

    protected virtual bool IsFileLocked(string fileName)
    {
      FileInfo fileInfo = new FileInfo(fileName);
      FileStream fileStream = (FileStream) null;
      try
      {
        fileStream = fileInfo.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
      }
      catch (IOException ex)
      {
        return true;
      }
      finally
      {
        if (fileStream != null)
          fileStream.Close();
      }
      return false;
    }

    internal int OpusEncodeVirtualWav(OpusOptions myOpusOptions)
    {
      VirtualWAV virtualWav = myOpusOptions.myVirtualWav;
      long start1 = myOpusOptions.start;
      long end1 = myOpusOptions.end;
      string fileName = myOpusOptions.fileName;
      Audible audible = myOpusOptions.myAudible;
      string encodingArgs = myOpusOptions.encodingArgs;
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.opusPath;
      process.StartInfo.Arguments = encodingArgs + " --artist \"" + audible.author + "\" --title \"" + audible.title + "\" --album \"" + audible.title + "\" \"" + fileName + "\"";
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardInput = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.Start();
      BinaryWriter binaryWriter = new BinaryWriter(process.StandardInput.BaseStream);
      long num1 = (long) (virtualWav.sampleRate * virtualWav.channels * virtualWav.bitsPerChannel / 8);
      long start2 = start1 * num1;
      long end2 = end1 * num1;
      List<SourcePCM> pcMcollection = virtualWav.GetPCMcollection(start2, end2);
      long num2 = 0;
      foreach (SourcePCM sourcePcm in pcMcollection)
      {
        long start3 = sourcePcm.start;
        while (start3 < sourcePcm.end)
        {
          int count = this.pcmBufferSize;
          num2 += (long) this.pcmBufferSize;
          this.percentComplete = (int) ((double) num2 / (double) end2 * 100.0);
          if (start3 + (long) this.pcmBufferSize > sourcePcm.end)
            count = (int) (sourcePcm.end - start3);
          byte[] bytes = Lame.getBytes(sourcePcm.fileName, (long) (int) start3, count);
          binaryWriter.Write(bytes);
          binaryWriter.Flush();
          start3 += (long) this.pcmBufferSize;
        }
      }
      binaryWriter.Close();
      return 1;
    }

    internal int OggEncodeVirtualWav(OggOptions myOggOptions)
    {
      VirtualWAV virtualWav = myOggOptions.myVirtualWav;
      long start1 = myOggOptions.start;
      long end1 = myOggOptions.end;
      string fileName = myOggOptions.fileName;
      Audible audible = myOggOptions.myAudible;
      string encodingArgs = myOggOptions.encodingArgs;
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.oggPath;
      process.StartInfo.Arguments = encodingArgs + " -N " + (object) myOggOptions.trackNum + " -t \"" + audible.title + "\" -l \"" + audible.title + "\" -a \"" + audible.author + "\" -o \"" + fileName + "\"";
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardInput = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.Start();
      BinaryWriter binaryWriter = new BinaryWriter(process.StandardInput.BaseStream);
      long num1 = (long) (virtualWav.sampleRate * virtualWav.channels * virtualWav.bitsPerChannel / 8);
      long start2 = start1 * num1;
      long end2 = end1 * num1;
      List<SourcePCM> pcMcollection = virtualWav.GetPCMcollection(start2, end2);
      long num2 = 0;
      foreach (SourcePCM sourcePcm in pcMcollection)
      {
        long start3 = sourcePcm.start;
        while (start3 < sourcePcm.end)
        {
          int count = this.pcmBufferSize;
          num2 += (long) this.pcmBufferSize;
          this.percentComplete = (int) ((double) num2 / (double) end2 * 100.0);
          if (start3 + (long) this.pcmBufferSize > sourcePcm.end)
            count = (int) (sourcePcm.end - start3);
          byte[] bytes = Lame.getBytes(sourcePcm.fileName, (long) (int) start3, count);
          binaryWriter.Write(bytes);
          binaryWriter.Flush();
          start3 += (long) this.pcmBufferSize;
        }
      }
      binaryWriter.Close();
      return 1;
    }
  }
}
