// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.iTunes
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using AutoItX3Lib;
using iTunesLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using VCDAPILib;

namespace AudibleConvertor
{
  internal class iTunes : IDisposable
  {
    public string aaxFile = "";
    public int retryThreshold = 20;
    public string outputPath = "";
    private string libName = "inAudible";
    public string soxPath = "";
    public string lamePath = "";
    public string targetWAV = "";
    private int sliceNum = 1;
    private string tempWAVname = "tempPieces_";
    public string allOutputFiles = "";
    public string inAudibleTargetDir = "";
    private iTunesApp oItunes;
    private Api myVcd;
    public string[] initialDirs;
    public string[] mergedWAVfiles;
    public double startTime;
    public double endTime;
    private double dumpSize;
    private int zeroGrowthCounter;

    public void Dispose()
    {
      // ISSUE: reference to a compiler-generated method
      this.oItunes.Quit();
      this.oItunes = (iTunesApp) null;
      GC.Collect();
      GC.WaitForPendingFinalizers();
    }

    public static bool checkITunesInstallation()
    {
      bool flag = true;
      try
      {
        // ISSUE: variable of a compiler-generated type
        iTunesApp instance = (iTunesApp) Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("DC0C2640-1415-4644-875C-6F4D769839BA")));
        // ISSUE: variable of a compiler-generated type
        IITSourceCollection sources = instance.Sources;
      }
      catch
      {
        flag = false;
      }
      return flag;
    }

    public static bool checkVirtualCDinstallation()
    {
      bool flag = true;
      try
      {
        // ISSUE: variable of a compiler-generated type
        Api instance = (Api) Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("F0A68B1B-43DE-48DF-A50F-A467B86FD163")));
        // ISSUE: reference to a compiler-generated method
        instance.VCDIsProperlyInstalled();
      }
      catch
      {
        flag = false;
      }
      return flag;
    }

    public void init()
    {
      // ISSUE: variable of a compiler-generated type
      IITSourceCollection sources = this.oItunes.Sources;
      // ISSUE: reference to a compiler-generated method
      // ISSUE: variable of a compiler-generated type
      IITPlaylistCollection playlists = sources.get_ItemByName("Library").Playlists;
      // ISSUE: reference to a compiler-generated method
      // ISSUE: variable of a compiler-generated type
      IITPlaylist itPlaylist = playlists.get_ItemByName(this.libName);
      if (itPlaylist != null)
      {
        // ISSUE: reference to a compiler-generated method
        itPlaylist.Delete();
      }
      // ISSUE: reference to a compiler-generated method
      // ISSUE: variable of a compiler-generated type
      IITPlaylist playlist = this.oItunes.CreatePlaylist(this.libName);
      // ISSUE: variable of a compiler-generated type
      IITLibraryPlaylist libraryPlaylist = this.oItunes.LibraryPlaylist;
      // ISSUE: reference to a compiler-generated method
      libraryPlaylist.AddFile(this.aaxFile);
      // ISSUE: variable of a compiler-generated type
      IITTrackCollection tracks = libraryPlaylist.Tracks;
      foreach (IITTrack itTrack in tracks)
      {
        if (itTrack.Kind == ITTrackKind.ITTrackKindFile)
        {
          // ISSUE: variable of a compiler-generated type
          IITFileOrCDTrack itFileOrCdTrack = (IITFileOrCDTrack) itTrack;
          if (itFileOrCdTrack.Location == this.aaxFile)
          {
            object iTrackToAdd = (object) itTrack;
            // ISSUE: reference to a compiler-generated method
            (playlist as IITUserPlaylist).AddTrack(ref iTrackToAdd);
            // ISSUE: reference to a compiler-generated method
            (playlist as IITUserPlaylist).Reveal();
            itFileOrCdTrack.Start = (int) this.startTime;
            itFileOrCdTrack.Finish = (int) this.endTime;
          }
        }
      }
    }

    public void cleanup()
    {
      // ISSUE: variable of a compiler-generated type
      IITLibraryPlaylist libraryPlaylist = this.oItunes.LibraryPlaylist;
      // ISSUE: reference to a compiler-generated method
      libraryPlaylist.AddFile(this.aaxFile);
      // ISSUE: variable of a compiler-generated type
      IITTrackCollection tracks = libraryPlaylist.Tracks;
      foreach (IITTrack itTrack in tracks)
      {
        if (itTrack.Kind == ITTrackKind.ITTrackKindFile)
        {
          // ISSUE: variable of a compiler-generated type
          IITFileOrCDTrack itFileOrCdTrack = (IITFileOrCDTrack) itTrack;
          if (itFileOrCdTrack.Location == this.aaxFile)
          {
            try
            {
              // ISSUE: reference to a compiler-generated method
              itFileOrCdTrack.Delete();
            }
            catch (Exception ex)
            {
            }
          }
        }
      }
      try
      {
        // ISSUE: reference to a compiler-generated method
        libraryPlaylist.Delete();
      }
      catch (Exception ex)
      {
      }
    }

    public void cleanStartup()
    {
      this.oItunes = (iTunesApp) Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("DC0C2640-1415-4644-875C-6F4D769839BA")));
      // ISSUE: variable of a compiler-generated type
      IITSourceCollection sources = this.oItunes.Sources;
      // ISSUE: reference to a compiler-generated method
      // ISSUE: variable of a compiler-generated type
      IITPlaylistCollection playlists = sources.get_ItemByName("Library").Playlists;
      // ISSUE: reference to a compiler-generated method
      // ISSUE: variable of a compiler-generated type
      IITPlaylist itPlaylist = playlists.get_ItemByName(this.libName);
      if (itPlaylist != null)
      {
        // ISSUE: reference to a compiler-generated method
        itPlaylist.Delete();
      }
      // ISSUE: reference to a compiler-generated method
      // ISSUE: variable of a compiler-generated type
      IITPlaylist playlist = this.oItunes.CreatePlaylist(this.libName);
      // ISSUE: variable of a compiler-generated type
      IITLibraryPlaylist libraryPlaylist = this.oItunes.LibraryPlaylist;
      // ISSUE: reference to a compiler-generated method
      libraryPlaylist.AddFile(this.aaxFile);
      // ISSUE: variable of a compiler-generated type
      IITTrackCollection tracks = libraryPlaylist.Tracks;
      foreach (IITTrack itTrack in tracks)
      {
        if (itTrack.Kind == ITTrackKind.ITTrackKindFile)
        {
          // ISSUE: variable of a compiler-generated type
          IITFileOrCDTrack itFileOrCdTrack = (IITFileOrCDTrack) itTrack;
          if (itFileOrCdTrack.Location == this.aaxFile)
          {
            try
            {
              // ISSUE: reference to a compiler-generated method
              itFileOrCdTrack.Delete();
            }
            catch (Exception ex)
            {
            }
          }
        }
      }
      try
      {
        // ISSUE: reference to a compiler-generated method
        playlist.Delete();
      }
      catch (Exception ex)
      {
      }
    }

    public void die()
    {
      // ISSUE: reference to a compiler-generated method
      this.oItunes.Quit();
      this.oItunes = (iTunesApp) null;
      GC.Collect();
      GC.WaitForPendingFinalizers();
    }

    public int selectBurnPlaylist()
    {
      // ISSUE: variable of a compiler-generated type
      AutoItX3 instance = (AutoItX3) Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("1A671297-FA74-4422-80FA-6C5D8CE4DE04")));
      // ISSUE: reference to a compiler-generated method
      instance.MouseMove(1000, 200, -1);
      // ISSUE: reference to a compiler-generated method
      instance.AutoItSetOption("WinTitleMatchMode", 4);
      string str = "";
      while (str.Length == 0)
      {
        // ISSUE: reference to a compiler-generated method
        str = instance.WinGetHandle(nameof (iTunes), "");
      }
      int num1 = 0;
      // ISSUE: reference to a compiler-generated method
      if (instance.WinExists("handle=" + str, "") != 0)
      {
        // ISSUE: reference to a compiler-generated method
        while (instance.ControlCommand("handle=" + str, "", "Static4", "IsEnabled", "") == "0")
        {
          Thread.Sleep(100);
          ++num1;
          if (num1 > this.retryThreshold)
            return 0;
        }
        // ISSUE: reference to a compiler-generated method
        instance.ControlSend("handle=" + str, "", "Static4", "{ALT}F", 0);
        // ISSUE: reference to a compiler-generated method
        instance.ControlSend("handle=" + str, "", "Static4", "{DOWN}{DOWN}{DOWN}{DOWN}{DOWN}{ENTER}", 0);
      }
      int num2 = 0;
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      while (instance.WinExists("handle=" + instance.WinGetHandle("Burn Settings", ""), "") == 0)
      {
        Thread.Sleep(100);
        ++num2;
        if (num2 > this.retryThreshold)
          return 0;
      }
      return 1;
    }

    public int burnVCD()
    {
      // ISSUE: variable of a compiler-generated type
      AutoItX3 instance = (AutoItX3) Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("1A671297-FA74-4422-80FA-6C5D8CE4DE04")));
      // ISSUE: reference to a compiler-generated method
      instance.AutoItSetOption("WinTitleMatchMode", 4);
      string str1 = "";
      while (str1.Length == 0)
      {
        // ISSUE: reference to a compiler-generated method
        str1 = instance.WinGetHandle(nameof (iTunes), "");
      }
      // ISSUE: reference to a compiler-generated method
      if (instance.WinExists("handle=" + str1, "") != 0)
      {
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        while (instance.WinExists("handle=" + instance.WinGetHandle("Burn Settings", ""), "") == 0)
          Thread.Sleep(100);
        // ISSUE: reference to a compiler-generated method
        string handle = instance.WinGetHandle("Burn Settings", "");
        // ISSUE: reference to a compiler-generated method
        instance.ControlSend("handle=" + handle, "", "ComboBox1", this.getVCDdriveLetter(), 0);
        // ISSUE: reference to a compiler-generated method
        instance.ControlSend("handle=" + handle, "", "ComboBox3", "n", 0);
        // ISSUE: reference to a compiler-generated method
        instance.ControlClick("handle=" + handle, "", "Button7", "LEFT", 1, -2147483647, -2147483647);
        this.virtualCDcapture();
        int num1 = 0;
        string str2 = "";
        while (str2.Length == 0)
        {
          // ISSUE: reference to a compiler-generated method
          str2 = instance.WinGetHandle("[Title:iTunes; CLASS:iTunesCustomModalDialog]", "");
          Thread.Sleep(100);
          ++num1;
          if (num1 > this.retryThreshold * 5)
            return 1;
        }
        int num2 = 0;
        // ISSUE: reference to a compiler-generated method
        while (instance.ControlCommand("handle=" + str2, "", "Static2", "IsEnabled", "") == "0")
        {
          Thread.Sleep(100);
          ++num2;
          if (num2 > this.retryThreshold)
            return 0;
        }
        // ISSUE: reference to a compiler-generated method
        instance.ControlClick("handle=" + str2, "", "Button1", "LEFT", 1, -2147483647, -2147483647);
      }
      return 1;
    }

    private string getVCDdriveLetter()
    {
      // ISSUE: reference to a compiler-generated method
      string burnerDriveLetters = this.myVcd.VCDGetVCDBurnerDriveLetters();
      burnerDriveLetters.Trim();
      return burnerDriveLetters;
    }

    public void virtualCDcapture()
    {
      this.myVcd = (Api) Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("F0A68B1B-43DE-48DF-A50F-A467B86FD163")));
      // ISSUE: reference to a compiler-generated method
      int num = (int) this.myVcd.InitMusicFileMode(Convert.ToSByte((int) Convert.ToChar(this.getVCDdriveLetter())));
    }

    public void eject()
    {
      string vcDdriveLetter = this.getVCDdriveLetter();
      int num = (int) Convert.ToSByte((int) Convert.ToChar(vcDdriveLetter));
      // ISSUE: reference to a compiler-generated method
      this.myVcd.VCDEject(vcDdriveLetter);
    }

    public void setAudioOutputPath(string path)
    {
      // ISSUE: reference to a compiler-generated method
      int num = (int) this.myVcd.VCDSetVCDPath(19U, path);
    }

    internal void killBlankCDdialog()
    {
      string vcDdriveLetter = this.getVCDdriveLetter();
      int num = (int) Convert.ToSByte((int) Convert.ToChar(vcDdriveLetter));
      // ISSUE: reference to a compiler-generated method
      this.myVcd.VCDEject(vcDdriveLetter);
    }

    internal void hailMary()
    {
      // ISSUE: variable of a compiler-generated type
      AutoItX3 instance = (AutoItX3) Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("1A671297-FA74-4422-80FA-6C5D8CE4DE04")));
      // ISSUE: reference to a compiler-generated method
      instance.AutoItSetOption("WinTitleMatchMode", 4);
      string str = "";
      while (str.Length == 0)
      {
        // ISSUE: reference to a compiler-generated method
        str = instance.WinGetHandle(nameof (iTunes), "");
      }
      // ISSUE: reference to a compiler-generated method
      if (instance.WinExists("handle=" + str, "") != 0)
      {
        // ISSUE: reference to a compiler-generated method
        instance.Send("{ESC}", 0);
      }
      this.init();
    }

    internal void VCDinit()
    {
      try
      {
        this.myVcd = (Api) Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("F0A68B1B-43DE-48DF-A50F-A467B86FD163")));
        // ISSUE: reference to a compiler-generated method
        this.outputPath = this.myVcd.VCDGetVCDPath(19);
      }
      catch (Exception ex)
      {
        Audible.diskLogger(ex.ToString());
      }
      this.initialDirs = Directory.GetDirectories(this.outputPath, "VCD*", SearchOption.TopDirectoryOnly);
    }

    internal int checkForCompletion()
    {
      int num1 = 0;
      int num2 = 0;
      // ISSUE: variable of a compiler-generated type
      AutoItX3 instance = (AutoItX3) Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("1A671297-FA74-4422-80FA-6C5D8CE4DE04")));
      // ISSUE: reference to a compiler-generated method
      instance.AutoItSetOption("WinTitleMatchMode", 4);
      if (this.checkOutputDir())
        return 1;
      string str1;
      for (str1 = ""; str1.Length == 0 && num2 < 1; ++num2)
      {
        // ISSUE: reference to a compiler-generated method
        str1 = instance.WinGetHandle("Virtual CD - Task Assignments", "");
      }
      int num3 = 0;
      // ISSUE: reference to a compiler-generated method
      if (instance.WinExists("handle=" + str1, "") != 0)
      {
        // ISSUE: reference to a compiler-generated method
        instance.ControlClick("handle=" + str1, "", "Button5", "LEFT", 1, -2147483647, -2147483647);
      }
      string str2;
      for (str2 = ""; str2.Length == 0 && num3 < 1; ++num3)
      {
        // ISSUE: reference to a compiler-generated method
        str2 = instance.WinGetHandle("[Title:iTunes; CLASS:iTunesCustomModalDialog]", "");
      }
      // ISSUE: reference to a compiler-generated method
      if (instance.WinExists("handle=" + str2, "") != 0)
      {
        // ISSUE: reference to a compiler-generated method
        instance.ControlClick("handle=" + str2, "", "Button2", "LEFT", 1, -2147483647, -2147483647);
      }
      return num1;
    }

    private bool checkOutputDir()
    {
      List<string> workingDirectories = this.getWorkingDirectories();
      double num = 0.0;
      foreach (string p in workingDirectories)
        num += (double) iTunes.GetDirectorySize(p);
      if (num != this.dumpSize)
      {
        this.dumpSize = num;
        this.zeroGrowthCounter = 0;
      }
      else
        ++this.zeroGrowthCounter;
      Audible.diskLogger("Dump size = " + this.dumpSize.ToString("N0") + " - Zero Growth Counter = " + (object) this.zeroGrowthCounter);
      if (this.zeroGrowthCounter > 0 && this.dumpSize > 0.0)
        Thread.Sleep(this.zeroGrowthCounter * 500);
      return this.zeroGrowthCounter > 4 && this.dumpSize > 0.0;
    }

    private bool isPartOfThisSession(string file)
    {
      bool flag = false;
      foreach (string directory in Directory.GetDirectories(this.outputPath, "VCD*", SearchOption.TopDirectoryOnly))
      {
        if (Array.IndexOf<string>(this.initialDirs, directory) < 0 && directory.Contains(file))
          flag = true;
      }
      return flag;
    }

    public List<string> getWorkingDirectories()
    {
      List<string> stringList = new List<string>();
      foreach (string str in (IEnumerable<string>) ((IEnumerable<string>) Directory.GetDirectories(this.outputPath, "VCD*", SearchOption.TopDirectoryOnly)).OrderBy<string, DateTime>((System.Func<string, DateTime>) (f => new DirectoryInfo(f).CreationTime)))
      {
        if (Array.IndexOf<string>(this.initialDirs, str) < 0)
          stringList.Add(str);
      }
      return stringList;
    }

    public static long GetDirectorySize(string p)
    {
      string[] files = Directory.GetFiles(p, "*.*");
      long num = 0;
      foreach (string fileName in files)
      {
        FileInfo fileInfo = new FileInfo(fileName);
        num += fileInfo.Length;
      }
      return num;
    }

    public static FileInfo GetNewestFile(DirectoryInfo directory)
    {
      return ((IEnumerable<FileInfo>) directory.GetFiles()).Union<FileInfo>(((IEnumerable<DirectoryInfo>) directory.GetDirectories()).Select<DirectoryInfo, FileInfo>((System.Func<DirectoryInfo, FileInfo>) (d => iTunes.GetNewestFile(d)))).OrderByDescending<FileInfo, DateTime>((System.Func<FileInfo, DateTime>) (f =>
      {
        if (f != null)
          return f.LastWriteTime;
        return DateTime.MinValue;
      })).FirstOrDefault<FileInfo>();
    }

    public void pipeWAVs(string lameArgs)
    {
      Audible audible = new Audible();
      List<string> workingDirectories = this.getWorkingDirectories();
      if (workingDirectories.Count == 1)
      {
        Audible.diskLogger("One disk merge");
        foreach (string dir in workingDirectories)
          this.pipeSingleDisc(dir, lameArgs);
      }
      else
      {
        Audible.diskLogger("Multi disk merge");
        this.mergeMultiDirsPipe(workingDirectories, false, lameArgs);
      }
    }

    internal void combineAndMergeWAVs(bool doNotRemoveOverlap)
    {
      Audible audible = new Audible();
      List<string> workingDirectories = this.getWorkingDirectories();
      if (workingDirectories.Count == 1)
      {
        Audible.diskLogger("One disk merge");
        foreach (string dir in workingDirectories)
          this.mergeSingleDisc(dir);
      }
      else
      {
        Audible.diskLogger("Multi disk merge");
        this.mergeMultiDirs(workingDirectories, doNotRemoveOverlap);
      }
    }

    private void mergeMultiDirs(List<string> workingDirs, bool doNotRemoveOverlap)
    {
      string str1 = this.inAudibleTargetDir + "\\" + this.targetWAV;
      int num = 1;
      string overlap = "";
      if (!doNotRemoveOverlap)
      {
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
            this.mergeLastDir(workingDir, overlap);
          }
          else
          {
            Audible.diskLogger("Merging middle disc");
            overlap = this.mergeMiddleDir(workingDir, overlap);
          }
          ++num;
        }
      }
      else
      {
        foreach (string workingDir in workingDirs)
        {
          Audible.diskLogger("Merging single disc without removing overlap");
          this.mergeSingleDir(workingDir);
          ++num;
        }
      }
      foreach (string workingDir in workingDirs)
        Directory.Delete(workingDir, true);
      string[] array = ((IEnumerable<string>) Directory.GetFiles(this.inAudibleTargetDir, this.tempWAVname + "*.wav")).OrderBy<string, DateTime>((System.Func<string, DateTime>) (f => new FileInfo(f).CreationTime)).ToArray<string>();
      this.mergedWAVfiles = array;
      foreach (string str2 in array)
      {
        iTunes iTunes = this;
        string str3 = iTunes.allOutputFiles + "\"" + str2 + "\" ";
        iTunes.allOutputFiles = str3;
      }
      Audible.diskLogger("allOutputFiles = " + this.allOutputFiles);
    }

    private void mergeMultiDirsPipe(List<string> workingDirs, bool doNotRemoveOverlap, string lameArgs)
    {
      string str1 = this.inAudibleTargetDir + "\\" + Path.GetFileNameWithoutExtension(this.targetWAV) + ".mp3";
      Audible.diskLogger("Removing overlapping audio...");
      int num = 1;
      string overlap = "";
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.lamePath;
      process.StartInfo.Arguments = "- -r -s 44100 " + lameArgs + " \"" + str1 + "\"";
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardInput = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.Start();
      StreamWriter standardInput = process.StandardInput;
      if (!doNotRemoveOverlap)
      {
        foreach (string workingDir in workingDirs)
        {
          if (num == 1)
          {
            Audible.diskLogger("Merging first disc");
            overlap = this.mergeFirstDirPipe(workingDir, standardInput);
          }
          else if (num == workingDirs.Count)
          {
            Audible.diskLogger("Merging last disc");
            this.mergeLastDirPipe(workingDir, overlap, standardInput);
          }
          else
          {
            Audible.diskLogger("Merging middle disc");
            overlap = this.mergeMiddleDirPipe(workingDir, overlap, standardInput);
          }
          ++num;
        }
      }
      else
      {
        foreach (string workingDir in workingDirs)
        {
          Audible.diskLogger("Merging single disc without removing overlap");
          this.mergeSingleDir(workingDir);
          ++num;
        }
      }
      foreach (string workingDir in workingDirs)
        Directory.Delete(workingDir, true);
      string[] array = ((IEnumerable<string>) Directory.GetFiles(this.inAudibleTargetDir, this.tempWAVname + "*.wav")).OrderBy<string, DateTime>((System.Func<string, DateTime>) (f => new FileInfo(f).CreationTime)).ToArray<string>();
      this.mergedWAVfiles = array;
      foreach (string str2 in array)
      {
        iTunes iTunes = this;
        string str3 = iTunes.allOutputFiles + "\"" + str2 + "\" ";
        iTunes.allOutputFiles = str3;
      }
      Audible.diskLogger("allOutputFiles = " + this.allOutputFiles);
      standardInput.Close();
    }

    private void mergeLastDir(string dir, string overlap)
    {
      Audible audible = new Audible();
      string[] array = ((IEnumerable<string>) Directory.GetFiles(dir, "*.wav")).OrderBy<string, DateTime>((System.Func<string, DateTime>) (f => new FileInfo(f).CreationTime)).ToArray<string>();
      string output = this.inAudibleTargetDir + "\\" + this.tempWAVname + (object) this.sliceNum + ".wav";
      ++this.sliceNum;
      audible.findOverlap(overlap, array[0]);
      audible.mergeWAV(overlap, array[0], output);
      string outputFile = this.inAudibleTargetDir + "\\" + this.tempWAVname + (object) this.sliceNum + ".wav";
      ++this.sliceNum;
      if (array.Length <= 1)
        return;
      string[] filePaths = new string[array.Length - 1];
      for (int index = 0; index < filePaths.Length; ++index)
        filePaths[index] = array[index + 1];
      this.soxCombine(filePaths, outputFile);
    }

    private void mergeLastDirPipe(string dir, string overlap, StreamWriter sw)
    {
      Audible audible = new Audible();
      string[] array = ((IEnumerable<string>) Directory.GetFiles(dir, "*.wav")).OrderBy<string, DateTime>((System.Func<string, DateTime>) (f => new FileInfo(f).CreationTime)).ToArray<string>();
      audible.findOverlap(overlap, array[0]);
      this.mergeByteArray(audible.mergeWAVPipe(overlap, array[0]), sw);
      if (array.Length <= 1)
        return;
      string[] filePaths = new string[array.Length - 1];
      for (int index = 0; index < filePaths.Length; ++index)
        filePaths[index] = array[index + 1];
      this.pipeCombine(filePaths, sw);
    }

    private string mergeMiddleDir(string dir, string overlap)
    {
      Audible audible = new Audible();
      string[] array = ((IEnumerable<string>) Directory.GetFiles(dir, "*.wav")).OrderBy<string, DateTime>((System.Func<string, DateTime>) (f => new FileInfo(f).CreationTime)).ToArray<string>();
      string output = this.inAudibleTargetDir + "\\" + this.tempWAVname + (object) this.sliceNum + ".wav";
      ++this.sliceNum;
      audible.findOverlap(overlap, array[0]);
      audible.mergeWAV(overlap, array[0], output);
      string outputFile = this.inAudibleTargetDir + "\\" + this.tempWAVname + (object) this.sliceNum + ".wav";
      ++this.sliceNum;
      string[] filePaths = new string[array.Length - 2];
      for (int index = 0; index < filePaths.Length; ++index)
        filePaths[index] = array[index + 1];
      this.soxCombine(filePaths, outputFile);
      overlap = array[array.Length - 1];
      return overlap;
    }

    public string mergeMiddleDirPipe(string dir, string overlap, StreamWriter sw)
    {
      Audible audible = new Audible();
      string[] array = ((IEnumerable<string>) Directory.GetFiles(dir, "*.wav")).OrderBy<string, DateTime>((System.Func<string, DateTime>) (f => new FileInfo(f).CreationTime)).ToArray<string>();
      audible.findOverlap(overlap, array[0]);
      this.mergeByteArray(audible.mergeWAVPipe(overlap, array[0]), sw);
      string[] filePaths = new string[array.Length - 2];
      for (int index = 0; index < filePaths.Length; ++index)
        filePaths[index] = array[index + 1];
      this.pipeCombine(filePaths, sw);
      overlap = array[array.Length - 1];
      return overlap;
    }

    public string mergeFirstDirPipe(string dir, StreamWriter sw)
    {
      string[] array = ((IEnumerable<string>) Directory.GetFiles(dir, "*.wav")).OrderBy<string, DateTime>((System.Func<string, DateTime>) (f => new FileInfo(f).CreationTime)).ToArray<string>();
      string[] filePaths = new string[array.Length - 1];
      for (int index = 0; index < filePaths.Length; ++index)
        filePaths[index] = array[index];
      this.pipeCombine(filePaths, sw);
      return array[array.Length - 1];
    }

    private string mergeFirstDir(string dir)
    {
      string outputFile = this.inAudibleTargetDir + "\\" + this.tempWAVname + (object) this.sliceNum + ".wav";
      ++this.sliceNum;
      string[] array = ((IEnumerable<string>) Directory.GetFiles(dir, "*.wav")).OrderBy<string, DateTime>((System.Func<string, DateTime>) (f => new FileInfo(f).CreationTime)).ToArray<string>();
      string[] filePaths = new string[array.Length - 1];
      for (int index = 0; index < filePaths.Length; ++index)
        filePaths[index] = array[index];
      this.soxCombine(filePaths, outputFile);
      return array[array.Length - 1];
    }

    private void mergeSingleDir(string dir)
    {
      string outputFile = this.inAudibleTargetDir + "\\" + this.tempWAVname + (object) this.sliceNum + ".wav";
      ++this.sliceNum;
      string[] array = ((IEnumerable<string>) Directory.GetFiles(dir, "*.wav")).OrderBy<string, DateTime>((System.Func<string, DateTime>) (f => new FileInfo(f).CreationTime)).ToArray<string>();
      string[] filePaths = new string[array.Length];
      for (int index = 0; index < filePaths.Length; ++index)
        filePaths[index] = array[index];
      this.soxCombine(filePaths, outputFile);
    }

    private void mergeSingleDisc(string dir)
    {
      string outputFile = this.inAudibleTargetDir + "\\" + this.tempWAVname + "001.wav";
      this.soxCombine(((IEnumerable<string>) Directory.GetFiles(dir, "*.wav")).OrderBy<string, DateTime>((System.Func<string, DateTime>) (f => new FileInfo(f).CreationTime)).ToArray<string>(), outputFile);
      this.mergedWAVfiles = new string[1];
      this.mergedWAVfiles[0] = outputFile;
      Directory.Delete(dir, true);
    }

    private void pipeSingleDisc(string dir, string lameArgs)
    {
      string str = this.inAudibleTargetDir + "\\" + Path.GetFileNameWithoutExtension(this.targetWAV) + ".mp3";
      string[] array = ((IEnumerable<string>) Directory.GetFiles(dir, "*.wav")).OrderBy<string, DateTime>((System.Func<string, DateTime>) (f => new FileInfo(f).CreationTime)).ToArray<string>();
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.lamePath;
      process.StartInfo.Arguments = "- -r -s 44100 " + lameArgs + " \"" + str + "\"";
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardInput = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.Start();
      StreamWriter standardInput = process.StandardInput;
      this.pipeCombine(array, standardInput);
      standardInput.Close();
      Directory.Delete(dir, true);
    }

    private void soxCombine(string[] filePaths, string outputFile)
    {
      string str = "";
      foreach (string filePath in filePaths)
        str = str + "\"" + filePath + "\" ";
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.soxPath;
      process.StartInfo.Arguments = string.Format("{0} \"{1}\"", (object) str, (object) outputFile);
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.RedirectStandardError = true;
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.CreateNoWindow = true;
      Audible.diskLogger("SOX combine: " + process.StartInfo.FileName + " " + process.StartInfo.Arguments);
      process.Start();
      process.WaitForExit();
      int exitCode = process.ExitCode;
    }

    private void pipeCombine(string[] filePaths, StreamWriter sw)
    {
      foreach (string filePath in filePaths)
      {
        byte[] numArray = File.ReadAllBytes(filePath);
        char[] buffer = new char[numArray.Length];
        for (int index = 44; index < numArray.Length; ++index)
          buffer[index] = Convert.ToChar(numArray[index]);
        sw.Write(buffer, 0, buffer.Length);
        sw.Flush();
      }
    }

    private void mergeByteArray(byte[] bytes, StreamWriter sw)
    {
      iTunes.StreamCopy(sw, bytes);
    }

    internal string getAllOutputFiles()
    {
      return this.allOutputFiles;
    }

    public static StreamReader getRawStreamFromProcess(string exe, string parameters)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = exe;
      process.StartInfo.Arguments = parameters;
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      Audible.diskLogger("stream command = " + process.StartInfo.FileName + " " + process.StartInfo.Arguments);
      process.Start();
      return process.StandardOutput;
    }

    public static void StreamCopy(StreamWriter dest, StreamReader src)
    {
      char[] buffer = new char[4096];
      int count = 1;
      while (count > 0)
      {
        count = src.Read(buffer, 0, buffer.Length);
        dest.Write(buffer, 0, count);
      }
      dest.Flush();
    }

    public static void StreamCopy(StreamWriter dest, byte[] bytes)
    {
      char[] buffer = new char[bytes.Length];
      for (int index = 0; index < bytes.Length; ++index)
        buffer[index] = Convert.ToChar(bytes[index]);
      dest.Write(buffer, 0, buffer.Length);
      dest.Flush();
    }

    internal bool VerfyITunesPass(string[] thisPassDirs, double passTime)
    {
      List<string> stringList = new List<string>();
      foreach (string str in (IEnumerable<string>) ((IEnumerable<string>) Directory.GetDirectories(this.outputPath, "VCD*", SearchOption.TopDirectoryOnly)).OrderBy<string, DateTime>((System.Func<string, DateTime>) (f => new DirectoryInfo(f).CreationTime)))
      {
        if (Array.IndexOf<string>(thisPassDirs, str) < 0)
          stringList.Add(str);
      }
      double num1 = 0.0;
      foreach (string p in stringList)
        num1 += (double) iTunes.GetDirectorySize(p);
      double num2 = 176400.0 * passTime;
      double num3 = Math.Abs(num1 - num2) / num2 * 100.0;
      Audible.diskLogger("Target size = " + (object) num2 + ", actual size = " + (object) num1 + " : percent difference = " + (object) num3);
      if (num3 <= 5.0)
        return true;
      foreach (string path in stringList)
      {
        try
        {
          Directory.Delete(path, true);
        }
        catch
        {
        }
      }
      return false;
    }
  }
}
