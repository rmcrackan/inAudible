// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.FormAbout
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using ExtendedComponents;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AudibleConvertor
{
  public class FormAbout : Form
  {
    public static string appPath = Path.GetDirectoryName(AudibleConvertor.GLOBALS.ExecutablePath);
    public string audibleChaptersPath = "";
    public string neroAACpath = "";
    public string mp4chapsPath = "";
    public string neroAACtagPath = "";
    public string soxPath = "";
    public string iTunesProxyPath = "";
    public string lamePath = "";
    public string helixPath = "";
    public string mp3SplitPath = "";
    public string mp4boxPath = "";
    public string ngPath = "";
    public string ffmpegPath = "";
    public string ffprobePath = "";
    public string opusPath = "";
    public string oggPath = "";
    public string cddaPath = "";
    public string mp4ArtPath = "";
    public string aax2wavPath = "";
    public string instaripPath = "";
    public string atomicParsleyPath = "";
    public string trackDumpPath = "";
    public int threads = Environment.ProcessorCount;
    public string currentClockSpeed = "";
    public string maxClockSpeed = "";
    public string procName = "";
    public string manufacturer = "";
    public string version = "";
    private StringBuilder scrollingText = new StringBuilder();
    public SupportLibraries mySupportLibs;
    public int coreCount;
    public Process musicProcess;
    public Thread endingMusicThread;
    private string[] songs;
    private int songIndex;
    private IContainer components;
    public Label lblTitle;
    private EventLog eventLog1;
    private Label label2;
    private Label label3;
    private TextBox txtProcessor;
    private TextBox txtCores;
    private Label label4;
    private Scroller scroller1;
    private Button btnNextTrack;
    private TrackBar trackBar1;

    public FormAbout()
    {
      this.InitializeComponent();
      this.GetProcessor();
      this.txtProcessor.Text = this.procName;
      Size size1 = TextRenderer.MeasureText(this.txtProcessor.Text, this.txtProcessor.Font);
      this.txtProcessor.Width = size1.Width;
      this.txtProcessor.Height = size1.Height;
      this.txtCores.Text = this.coreCount.ToString() + " / " + (object) Environment.ProcessorCount;
      Size size2 = TextRenderer.MeasureText(this.txtCores.Text, this.txtCores.Font);
      this.txtCores.Width = size2.Width;
      this.txtCores.Height = size2.Height;
    }

    public void StartScroll()
    {
      this.AddVanity();
      this.scroller1.TextToScroll = this.scrollingText.ToString();
      this.scroller1.Interval = 40;
      this.scroller1.Start();
    }

    private void AddVanity()
    {
      string str1 = "Special thanks to:\r\n\r\nChiefwidget\r\nDarku\r\nPattycake\r\nTriforge\r\n\r\n\r\n***\r\n\r\n";
      try
      {
        str1 += File.ReadAllText(FormAbout.appPath + "\\change_log.txt");
      }
      catch
      {
      }
      string str2 = str1 + "\r\n***\r\n\r\nMany free and/or Open Source programs gave their\r\nlives and their license agreements to make this \r\napplication happen. \r\n\r\nWe remember the fallen, below...\r\n\r\n***";
      string str3 = this.scrollingText.ToString();
      this.scrollingText.Clear();
      this.scrollingText.AppendLine(str2);
      this.scrollingText.Append(str3);
      this.scrollingText.AppendLine("\r\n+++ATH0\r\nNO CARRIER");
      StringBuilder stringBuilder1 = new StringBuilder();
      string str4 = this.scrollingText.ToString();
      char[] chArray = new char[1]{ '\n' };
      foreach (string str5 in str4.Split(chArray))
      {
        string str6 = str5;
        if (str5.StartsWith("- "))
          str6 = str5.Replace("- ", "");
        stringBuilder1.Append(this.WordWrap(str6.Replace("\r", "")));
      }
      string[] strArray = stringBuilder1.ToString().Split('\n');
      StringBuilder stringBuilder2 = new StringBuilder();
      for (int index = 0; (double) index < (double) strArray.Length / 2.4; ++index)
        stringBuilder2.AppendLine();
      stringBuilder2.Append(stringBuilder1.ToString());
      this.scrollingText = stringBuilder2;
    }

    private string WordWrap(string input)
    {
      if (input.Contains("\r") || input.Contains("\n"))
        return "";
      int num = 60;
      string[] strArray = input.Split(' ');
      StringBuilder stringBuilder = new StringBuilder();
      string str1 = "";
      foreach (string str2 in strArray)
      {
        if ((str1 + str2).Length > num)
        {
          stringBuilder.AppendLine(str1);
          str1 = "";
        }
        str1 += string.Format("{0} ", (object) str2);
      }
      if (str1.Length > 0)
        stringBuilder.AppendLine(str1);
      return stringBuilder.ToString();
    }

    public void StartMusic()
    {
      string path = FormAbout.appPath + "\\credits";
      if (Directory.Exists(path))
      {
        this.songs = Directory.GetFiles(path);
        Array.Resize<string>(ref this.songs, this.songs.Length + 1);
        this.songs[this.songs.Length - 1] = FormAbout.appPath + "\\beep.mp3";
        this.songIndex = new Random(DateTime.Now.Millisecond).Next(0, this.songs.Length);
        this.FFplay(this.songs[this.songIndex], true);
      }
      else
      {
        this.songs = new string[1];
        this.songs[0] = FormAbout.appPath + "\\beep.mp3";
        this.FFplay(this.songs[0], true);
      }
    }

    private void FFplay(string file, bool background)
    {
      if (!File.Exists(file))
        return;
      if (this.musicProcess != null)
        this.musicProcess.Kill();
      this.musicProcess = new Process();
      this.musicProcess.StartInfo = new ProcessStartInfo();
      this.musicProcess.StartInfo.FileName = Path.GetDirectoryName(this.ffmpegPath) + "\\ffplay.exe";
      this.musicProcess.StartInfo.Arguments = " -nodisp \"" + file + "\"";
      this.musicProcess.StartInfo.CreateNoWindow = true;
      this.musicProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      this.musicProcess.StartInfo.UseShellExecute = false;
      this.musicProcess.Start();
      if (background)
      {
        this.endingMusicThread = new Thread((ThreadStart) (() => this.musicProcess.WaitForExit()));
        this.endingMusicThread.Start();
      }
      else
        this.musicProcess.WaitForExit();
    }

    public void GetProcessor()
    {
      using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("select * from Win32_Processor"))
      {
        using (new ManagementObjectSearcher("select * from Win32_ComputerSystem"))
        {
          using (new ManagementObjectSearcher("select * from Win32_PhysicalMemory"))
          {
            foreach (ManagementObject managementObject in managementObjectSearcher.Get())
            {
              this.currentClockSpeed = managementObject["CurrentClockSpeed"].ToString();
              this.maxClockSpeed = managementObject["MaxClockSpeed"].ToString();
              this.procName = managementObject["Name"].ToString();
              this.manufacturer = managementObject["Manufacturer"].ToString();
              this.version = managementObject["Version"].ToString();
              this.coreCount += int.Parse(managementObject["NumberOfCores"].ToString());
            }
          }
        }
      }
    }

    public void GetVersions()
    {
      this.scrollingText.AppendLine("NeroAAC = " + this.GetNeroVersion() + "\r\n");
      this.scrollingText.AppendLine("FDK AAC = " + this.GetFDKVersion() + "\r\n");
      this.scrollingText.AppendLine("MP4Chaps = " + this.GetMP4chapsVersion() + "\r\n");
      this.scrollingText.AppendLine("MP3SPLT = " + this.GetMP3SPLTVersion() + "\r\n");
      this.scrollingText.AppendLine("SOX = " + this.GetSOXVersion() + "\r\n");
      this.scrollingText.AppendLine("LAME = " + this.GetLAMEVersion() + "\r\n");
      this.scrollingText.AppendLine("Helix = " + this.GetHelixVersion() + "\r\n");
      this.scrollingText.AppendLine("MP4Box = " + this.GetMP4BoxVersion() + "\r\n");
      this.scrollingText.AppendLine("ffmpeg = " + this.GetFfmpegVersion() + "\r\n");
      this.scrollingText.AppendLine("opusEnc = " + this.GetOpusEncVersion() + "\r\n");
      this.scrollingText.AppendLine("CDDA = " + this.GetCDDAVersion() + "\r\n");
      this.scrollingText.AppendLine("MP4Art = " + this.GetMP4ArtVersion() + "\r\n");
      this.scrollingText.AppendLine("Atomicparsley = " + this.GetAtomicParsleyVersion() + "\r\n");
    }

    private string GetMP3SPLTVersion()
    {
      string str1 = "";
      string processOutput = this.GetProcessOutput(this.mySupportLibs.mp3SplitPath, "-v", false);
      char[] chArray = new char[1]{ '\n' };
      foreach (string str2 in processOutput.Split(chArray))
      {
        if (str2.Contains("mp3splt"))
        {
          str1 = str2.Replace("\r", "");
          break;
        }
      }
      return str1;
    }

    private string GetFDKVersion()
    {
      string str1 = "";
      string processOutput = this.GetProcessOutput(Path.GetDirectoryName(this.ffmpegPath) + "\\fdkaac.exe", "", false);
      char[] chArray = new char[1]{ '\n' };
      foreach (string str2 in processOutput.Split(chArray))
      {
        if (str2.Contains("fdkaac"))
        {
          str1 = str2.Replace("\r", "");
          break;
        }
      }
      return str1;
    }

    private string GetAtomicParsleyVersion()
    {
      return this.GetProcessOutput(this.atomicParsleyPath, "-v", false).Replace("\r", "").Replace("\n", "").Split(':')[1].Trim();
    }

    private string GetMP4ArtVersion()
    {
      return this.GetProcessOutput(this.mp4ArtPath, "--version", false).Replace("\r", "").Replace("\n", "").Split('-')[1].Trim();
    }

    private string GetCDDAVersion()
    {
      string str1 = "";
      string processOutput = this.GetProcessOutput(this.cddaPath, "-version", false);
      char[] chArray = new char[1]{ '\n' };
      foreach (string str2 in processOutput.Split(chArray))
      {
        if (str2.Contains("cdda2wav"))
        {
          str1 = str2.Replace("\r", "");
          break;
        }
      }
      return str1;
    }

    private string GetOpusEncVersion()
    {
      string str1 = "";
      string processOutput = this.GetProcessOutput(this.opusPath, "-V", false);
      char[] chArray = new char[1]{ '\n' };
      foreach (string str2 in processOutput.Split(chArray))
      {
        if (str2.Contains("opusenc"))
        {
          str1 = str2.Replace("\r", "");
          break;
        }
      }
      return str1;
    }

    private string GetFfmpegVersion()
    {
      string str1 = "";
      string processOutput = this.GetProcessOutput(this.ffmpegPath, "-version", false);
      char[] chArray = new char[1]{ '\n' };
      foreach (string str2 in processOutput.Split(chArray))
      {
        if (str2.Contains("version"))
        {
          str1 = str2.Replace("\r", "");
          break;
        }
      }
      return str1;
    }

    private string GetMP4BoxVersion()
    {
      string str1 = "";
      string processOutput = this.GetProcessOutput(this.mp4boxPath, "-version", true);
      char[] chArray = new char[1]{ '\n' };
      foreach (string str2 in processOutput.Split(chArray))
      {
        if (str2.Contains("version"))
        {
          str1 = str2.Replace("\r", "");
          break;
        }
      }
      return str1;
    }

    private string GetHelixVersion()
    {
      string str1 = "";
      string processOutput = this.GetProcessOutput(this.helixPath, "", true);
      char[] chArray = new char[1]{ '\n' };
      foreach (string str2 in processOutput.Split(chArray))
      {
        if (str2.Contains("MPEG Layer III audio"))
        {
          string str3 = str2.Replace("\r", "");
          str1 = str3.Substring(str3.IndexOf("MPEG"));
          break;
        }
      }
      return str1;
    }

    private string GetLAMEVersion()
    {
      string str1 = "";
      string processOutput = this.GetProcessOutput(this.lamePath, "--version", false);
      char[] chArray = new char[1]{ '\n' };
      foreach (string str2 in processOutput.Split(chArray))
      {
        if (str2.Contains("version"))
        {
          str1 = str2.Replace("\r", "");
          break;
        }
      }
      return str1;
    }

    private string GetSOXVersion()
    {
      string[] strArray = this.GetProcessOutput(this.soxPath, "--version", false).Replace("\r", "").Replace("\n", "").Split(':');
      return strArray[strArray.Length - 1].Trim();
    }

    private string GetMP4chapsVersion()
    {
      return this.GetProcessOutput(this.mp4chapsPath, "--version", false).Replace("\r", "").Replace("\n", "").Split('-')[1].Trim();
    }

    private string GetNeroVersion()
    {
      string str1 = "";
      string processOutput = this.GetProcessOutput(this.neroAACpath, "", true);
      char[] chArray = new char[1]{ '\n' };
      foreach (string str2 in processOutput.Split(chArray))
      {
        if (str2.Contains("Package version:"))
          str1 = str2.Split(':')[1].Trim().Split(' ')[0];
      }
      return str1;
    }

    private string GetProcessOutput(string exePath, string args, bool useStdErr)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = exePath;
      process.StartInfo.Arguments = args;
      if (!useStdErr)
        process.StartInfo.RedirectStandardOutput = true;
      else
        process.StartInfo.RedirectStandardError = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.UseShellExecute = false;
      process.Start();
      string str = !useStdErr ? process.StandardOutput.ReadToEnd() : process.StandardError.ReadToEnd();
      process.WaitForExit();
      return str;
    }

    private void FormAbout_FormClosing_1(object sender, FormClosingEventArgs e)
    {
      if (this.endingMusicThread == null || !this.endingMusicThread.IsAlive)
        return;
      this.musicProcess.Kill();
      this.endingMusicThread.Abort();
    }

    private void FormAbout_Shown(object sender, EventArgs e)
    {
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.StartScroll();
    }

    private void btnNextTrack_Click(object sender, EventArgs e)
    {
      this.NextSong();
    }

    private void NextSong()
    {
      ++this.songIndex;
      if (this.songIndex == this.songs.Length)
        this.songIndex = 0;
      this.FFplay(this.songs[this.songIndex], true);
    }

    private void trackBar1_Scroll(object sender, EventArgs e)
    {
      if (this.trackBar1.Value <= 0)
        return;
      this.scroller1.Interval = this.trackBar1.Value;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormAbout));
      this.lblTitle = new Label();
      this.eventLog1 = new EventLog();
      this.label2 = new Label();
      this.label3 = new Label();
      this.txtProcessor = new TextBox();
      this.txtCores = new TextBox();
      this.label4 = new Label();
      this.btnNextTrack = new Button();
      this.trackBar1 = new TrackBar();
      this.scroller1 = new Scroller();
      this.eventLog1.BeginInit();
      this.trackBar1.BeginInit();
      this.SuspendLayout();
      this.lblTitle.Dock = DockStyle.Top;
      this.lblTitle.Font = new Font("Arial Narrow", 20.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblTitle.ForeColor = Color.Red;
      this.lblTitle.Location = new Point(0, 0);
      this.lblTitle.Name = "lblTitle";
      this.lblTitle.Size = new Size(583, 37);
      this.lblTitle.TabIndex = 0;
      this.lblTitle.Text = "label1";
      this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
      this.eventLog1.SynchronizingObject = (ISynchronizeInvoke) this;
      this.label2.Anchor = AnchorStyles.Top;
      this.label2.AutoSize = true;
      this.label2.Font = new Font("Arial Narrow", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label2.Location = new Point(214, 46);
      this.label2.Name = "label2";
      this.label2.Size = new Size(154, 23);
      this.label2.TabIndex = 3;
      this.label2.Text = "Fast. Flawless. Free.";
      this.label3.AutoSize = true;
      this.label3.Location = new Point(40, 81);
      this.label3.Name = "label3";
      this.label3.Size = new Size(57, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "Processor:";
      this.txtProcessor.Location = new Point(103, 78);
      this.txtProcessor.Name = "txtProcessor";
      this.txtProcessor.ReadOnly = true;
      this.txtProcessor.Size = new Size(148, 20);
      this.txtProcessor.TabIndex = 5;
      this.txtCores.Location = new Point(103, 101);
      this.txtCores.Name = "txtCores";
      this.txtCores.ReadOnly = true;
      this.txtCores.Size = new Size(55, 20);
      this.txtCores.TabIndex = 6;
      this.label4.AutoSize = true;
      this.label4.Location = new Point(16, 102);
      this.label4.Name = "label4";
      this.label4.Size = new Size(81, 13);
      this.label4.TabIndex = 7;
      this.label4.Text = "Cores/Threads:";
      this.btnNextTrack.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btnNextTrack.Location = new Point(548, 46);
      this.btnNextTrack.Name = "btnNextTrack";
      this.btnNextTrack.Size = new Size(23, 23);
      this.btnNextTrack.TabIndex = 9;
      this.btnNextTrack.Text = "?";
      this.btnNextTrack.UseVisualStyleBackColor = true;
      this.btnNextTrack.Click += new EventHandler(this.btnNextTrack_Click);
      this.trackBar1.Location = new Point(438, 46);
      this.trackBar1.Maximum = 100;
      this.trackBar1.Name = "trackBar1";
      this.trackBar1.Size = new Size(104, 45);
      this.trackBar1.TabIndex = 10;
      this.trackBar1.Value = 60;
      this.trackBar1.Scroll += new EventHandler(this.trackBar1_Scroll);
      this.scroller1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.scroller1.Font = new Font("Arial", 14.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
      this.scroller1.Interval = 40;
      this.scroller1.Location = new Point(12, (int) sbyte.MaxValue);
      this.scroller1.Margin = new Padding(8, 7, 8, 7);
      this.scroller1.Name = "scroller1";
      this.scroller1.Size = new Size(559, 259);
      this.scroller1.TabIndex = 0;
      this.scroller1.TextFont = new Font("Arial", 20f, FontStyle.Bold, GraphicsUnit.Pixel);
      this.scroller1.TextToScroll = "";
      this.scroller1.TopPartSizePercent = 50;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(583, 398);
      this.Controls.Add((Control) this.trackBar1);
      this.Controls.Add((Control) this.btnNextTrack);
      this.Controls.Add((Control) this.scroller1);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.txtCores);
      this.Controls.Add((Control) this.txtProcessor);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.lblTitle);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (FormAbout);
      this.Text = "About inAudible";
      this.FormClosing += new FormClosingEventHandler(this.FormAbout_FormClosing_1);
      this.Shown += new EventHandler(this.FormAbout_Shown);
      this.eventLog1.EndInit();
      this.trackBar1.EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
