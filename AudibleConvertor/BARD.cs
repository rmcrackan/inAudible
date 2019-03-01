// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.BARD
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace AudibleConvertor
{
  internal class BARD
  {
    public SupportLibraries supportLibs = new SupportLibraries();

    private string bardFile { get; set; }

    private string userDir { get; set; }

    public string userName { get; set; }

    public string userPass { get; set; }

    public string[] fileList { get; set; }

    public BARD(string _bardFile, string _userDIr)
    {
      this.bardFile = _bardFile;
      this.userDir = _userDIr;
      this.InitUserDir();
    }

    private void InitUserDir()
    {
      if (!Directory.Exists(this.userDir))
        Directory.CreateDirectory(this.userDir);
      if (File.Exists(this.userDir + "\\DeviceID"))
        return;
      Audible.diskLogger("Initializing BARDEmu...");
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.supportLibs.BardEmu;
      process.StartInfo.Arguments = "-h \"" + this.userDir + "\" -i";
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.UseShellExecute = false;
      process.Start();
      process.WaitForExit();
    }

    internal bool HasValidUser()
    {
      bool flag = false;
      if (Directory.GetFiles(this.userDir, "DAISY*.pem").Length > 0)
        flag = true;
      return flag;
    }

    internal void GetAccountKey()
    {
      Audible.diskLogger("Getting BARD account key...");
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.supportLibs.BardEmu;
      process.StartInfo.Arguments = "-h \"" + this.userDir + "\" -l -u=" + this.userName + " -p=" + this.userPass;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardOutput = true;
      process.Start();
      Audible.diskLogger(process.StandardOutput.ReadToEnd());
      process.WaitForExit();
    }

    internal bool HasDecryptionKey()
    {
      bool flag = false;
      if (Directory.GetFiles(Path.GetDirectoryName(this.bardFile), "*.ao.extended").Length > 0)
        flag = true;
      return flag;
    }

    internal void GetBookKey()
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.supportLibs.BardEmu;
      process.StartInfo.Arguments = "-h \"" + this.userDir + "\" -a -f \"" + Path.GetDirectoryName(this.bardFile) + "\" -u=" + this.userName + " -p=" + this.userPass;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardOutput = true;
      process.Start();
      Audible.diskLogger(process.StandardOutput.ReadToEnd());
      process.WaitForExit();
    }

    internal void Decrypt()
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.supportLibs.BardEmu;
      process.StartInfo.Arguments = "-h \"" + this.userDir + "\" -d -f \"" + Path.GetDirectoryName(this.bardFile) + "\"";
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardOutput = true;
      process.Start();
      Audible.diskLogger(process.StandardOutput.ReadToEnd());
      process.WaitForExit();
      this.fileList = this.GetFileList();
    }

    private string[] GetFileList()
    {
      return new List<string>((IEnumerable<string>) Directory.GetFiles(Path.GetDirectoryName(this.bardFile), "decrypted-*.3gp")).ToArray();
    }

    private bool FitsMask(string sFileName, string sFileMask)
    {
      return new Regex(sFileMask.Replace(".", "[.]").Replace("*", ".*").Replace("?", ".")).IsMatch(sFileName);
    }
  }
}
