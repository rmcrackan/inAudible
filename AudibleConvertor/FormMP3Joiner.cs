// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.FormMP3Joiner
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using Inwards;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudibleConvertor
{
  public class FormMP3Joiner : Form
  {
    private IContainer components;
    private ProgressBar progressBar1;
    private Button btnClose;
    private System.Windows.Forms.TextBox txtStatus;
    public string[] inputFiles;
    public string outputFile;
    public SupportLibraries libs;
    public double[] realDurations;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormMP3Joiner));
      this.progressBar1 = new ProgressBar();
      this.btnClose = new Button();
      this.txtStatus = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      this.progressBar1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.progressBar1.Location = new Point(12, 41);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new Size(632, 23);
      this.progressBar1.TabIndex = 0;
      this.btnClose.Anchor = AnchorStyles.Bottom;
      this.btnClose.Enabled = false;
      this.btnClose.Location = new Point(291, 73);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new Size(75, 23);
      this.btnClose.TabIndex = 1;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new EventHandler(this.btnClose_Click);
      this.txtStatus.Location = new Point(12, 12);
      this.txtStatus.Name = "txtStatus";
      this.txtStatus.Size = new Size(632, 20);
      this.txtStatus.TabIndex = 2;
      this.txtStatus.TextAlign = HorizontalAlignment.Center;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(656, 108);
      this.Controls.Add((Control) this.txtStatus);
      this.Controls.Add((Control) this.btnClose);
      this.Controls.Add((Control) this.progressBar1);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (FormMP3Joiner);
      this.Text = "MP3/M4B Joiner";
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public FormMP3Joiner()
    {
      this.InitializeComponent();
      this.Shown += new EventHandler(this.JoinFiles);
    }

    private void JoinFiles(object sender, EventArgs e)
    {
      Task.Factory.StartNew((Action) (() =>
      {
        if (Path.GetExtension(this.inputFiles[0]).TrimStart('.').ToLower() == "mp3")
        {
          using (FileStream fileStream1 = System.IO.File.Create(this.outputFile))
          {
            int num = 0;
            foreach (string inputFile in this.inputFiles)
            {
              using (FileStream fileStream2 = System.IO.File.OpenRead(inputFile))
              {
                fileStream2.CopyTo((Stream) fileStream1);
                Form1.SetControlPropertyThreadSafe((Control) this.progressBar1, "Value", (object) (int) ((double) num / (double) this.inputFiles.Length * 100.0));
                Form1.SetControlPropertyThreadSafe((Control) this.txtStatus, "Text", (object) ("[" + (object) (num + 1) + "/" + (object) this.inputFiles.Length + "] " + Path.GetFileNameWithoutExtension(inputFile)));
                ++num;
              }
            }
            Form1.SetControlPropertyThreadSafe((Control) this.progressBar1, "Value", (object) 0);
          }
          Form1.SetControlPropertyThreadSafe((Control) this.txtStatus, "Text", (object) "Fixing headers");
          this.FixMP3File(this.outputFile);
          Form1.SetControlPropertyThreadSafe((Control) this.txtStatus, "Text", (object) "Done!");
        }
        else
        {
          string directoryName = Path.GetDirectoryName(this.outputFile);
          this.realDurations = new double[this.inputFiles.Length];
          for (int index = 0; index < this.inputFiles.Length; ++index)
          {
            string target = Path.GetFileNameWithoutExtension(this.inputFiles[index]) + ".aac";
            Form1.SetControlPropertyThreadSafe((Control) this.progressBar1, "Value", (object) (int) ((double) index / (double) this.inputFiles.Length * 100.0));
            Form1.SetControlPropertyThreadSafe((Control) this.txtStatus, "Text", (object) ("Extracting AAC audio [" + (object) (index + 1) + "/" + (object) this.inputFiles.Length + "] " + Path.GetFileNameWithoutExtension(this.inputFiles[index])));
            this.realDurations[index] = this.ExtractAAC(this.inputFiles[index], target);
          }
          Form1.SetControlPropertyThreadSafe((Control) this.progressBar1, "Value", (object) 100);
          string str1 = directoryName + "\\" + Path.GetFileNameWithoutExtension(this.outputFile) + ".aac";
          using (FileStream fileStream1 = System.IO.File.Create(str1))
          {
            int num = 0;
            for (int index = 0; index < this.inputFiles.Length; ++index)
            {
              string str2 = Path.GetDirectoryName(this.inputFiles[index]) + "\\" + Path.GetFileNameWithoutExtension(this.inputFiles[index]) + ".aac";
              using (FileStream fileStream2 = System.IO.File.OpenRead(str2))
              {
                fileStream2.CopyTo((Stream) fileStream1);
                Form1.SetControlPropertyThreadSafe((Control) this.progressBar1, "Value", (object) (int) ((double) num / (double) this.inputFiles.Length * 100.0));
                Form1.SetControlPropertyThreadSafe((Control) this.txtStatus, "Text", (object) ("Merging audio [" + (object) (num + 1) + "/" + (object) this.inputFiles.Length + "] " + Path.GetFileNameWithoutExtension(this.inputFiles[index])));
                ++num;
              }
              try
              {
                this.SafeDelete(str2);
              }
              catch
              {
              }
            }
            Form1.SetControlPropertyThreadSafe((Control) this.progressBar1, "Value", (object) 100);
          }
          Form1.SetControlPropertyThreadSafe((Control) this.txtStatus, "Text", (object) "Computing new chapter layout & reconstructing headers...");
          string str3 = this.CopyChapters(str1);
          Form1.SetControlPropertyThreadSafe((Control) this.txtStatus, "Text", (object) "Copying metadata...");
          this.CopyTags(this.inputFiles[0], str3);
          try
          {
            this.SafeDelete(this.outputFile);
            System.IO.File.Move(str3, this.outputFile);
          }
          catch
          {
          }
          Form1.SetControlPropertyThreadSafe((Control) this.txtStatus, "Text", (object) "Done.");
        }
      })).ContinueWith((Action<Task>) (t => this.btnClose.Enabled = true), TaskScheduler.FromCurrentSynchronizationContext());
    }

    private void SafeDelete(string source)
    {
      for (int index = 0; index < 10; ++index)
      {
        try
        {
          System.IO.File.Delete(source);
          if (!System.IO.File.Exists(source))
            break;
          Thread.Sleep(200);
        }
        catch
        {
        }
      }
    }

    private string GetTotalTimeFromAAC(string file)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.libs.ffprobePath;
      process.StartInfo.Arguments = "-i \"" + file + "\"";
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardInput = true;
      process.StartInfo.RedirectStandardError = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.Start();
      string end = process.StandardError.ReadToEnd();
      process.WaitForExit();
      return Audible.GetM4BTotalTimeffmpeg(end);
    }

    private string CopyChapters(string m4aFile)
    {
      string destFileName = Path.GetDirectoryName(m4aFile) + "\\" + Path.GetFileNameWithoutExtension(m4aFile) + ".tmp.m4a";
      double num = 0.0;
      AdvancedSplitting.Chapters chapters = new AdvancedSplitting.Chapters();
      Audible audible = new Audible();
      for (int index = 0; index < this.inputFiles.Length; ++index)
      {
        AdvancedSplitting.Chapters m4Bchapters = audible.GetM4BChapters(this.inputFiles[index]);
        for (int pos = 0; pos < m4Bchapters.Count(); ++pos)
        {
          AdvancedSplitting.Chapter chapter = m4Bchapters.GetChapter(pos);
          chapters.Add(chapter.time + num, chapter.description);
        }
        num += this.realDurations[index];
      }
      if (chapters.Count() == 0)
        return destFileName;
      string ffmpegChapters = this.GenerateFfmpegChapters(chapters);
      string ffmpegTags = this.GenerateFfmpegTags();
      string path = this.outputFile + ".ff.txt";
      string sourceFileName = Path.GetDirectoryName(this.outputFile) + "\\tempChaps.mp4";
      System.IO.File.WriteAllText(path, ffmpegTags + ffmpegChapters);
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.libs.ffmpegPath;
      process.StartInfo.Arguments = "-y -i \"" + m4aFile + "\" -f ffmetadata -i \"" + path + "\" -map_metadata 1 -bsf:a aac_adtstoasc -c:a copy -map 0 \"" + sourceFileName + "\"";
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.WorkingDirectory = Path.GetDirectoryName(this.outputFile);
      process.Start();
      process.WaitForExit();
      bool flag = true;
      while (flag)
      {
        try
        {
          System.IO.File.Delete(path);
          System.IO.File.Delete(m4aFile);
          System.IO.File.Move(sourceFileName, destFileName);
          flag = false;
        }
        catch
        {
          flag = true;
        }
      }
      return destFileName;
    }

    private string GenerateFfmpegTags()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(";FFMETADATA1\n");
      stringBuilder.Append("major_brand=mp42isom\n");
      stringBuilder.Append("minor_version=1\n");
      stringBuilder.Append("compatible_brands=aax M4B mp42isom\n");
      return stringBuilder.ToString();
    }

    private string GenerateFfmpegChapters(AdvancedSplitting.Chapters chapters)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int pos = 0; pos < chapters.Count() - 1; ++pos)
      {
        TimeSpan.FromSeconds(chapters.GetChapterDouble(pos));
        stringBuilder.Append("[CHAPTER]\n");
        stringBuilder.Append("TIMEBASE=1/1000\n");
        stringBuilder.Append("START=" + (object) (chapters.GetChapterDouble(pos) * 1000.0) + "\n");
        stringBuilder.Append("END=" + (object) (chapters.GetChapterDouble(pos) * 1000.0) + "\n");
        string description = chapters.GetChapter(pos).description;
        stringBuilder.Append("title=" + description + "\n");
      }
      return stringBuilder.ToString();
    }

    private void CopyTags(string source, string target)
    {
      try
      {
        TagLib.File file1 = TagLib.File.Create(source);
        TagLib.File file2 = TagLib.File.Create(target);
        file2.Tag.Album = file1.Tag.Album;
        file2.Tag.Title = file1.Tag.Title;
        file2.Tag.Track = 1U;
        file2.Tag.TrackCount = 1U;
        file2.Tag.Performers = file1.Tag.Performers;
        file2.Tag.Composers = file1.Tag.Composers;
        file2.Tag.Year = file1.Tag.Year;
        file2.Tag.Comment = file1.Tag.Comment;
        if (file1.Tag.Pictures.Length > 0)
          file2.Tag.Pictures = file1.Tag.Pictures;
        file2.Save();
      }
      catch
      {
      }
    }

    private string FixMP4header(string source)
    {
      string str = Path.GetDirectoryName(source) + "\\" + Path.GetFileNameWithoutExtension(source) + ".m4a";
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.libs.ffmpegPath;
      process.StartInfo.WorkingDirectory = Path.GetDirectoryName(source);
      process.StartInfo.Arguments = "-y -i \"" + source + "\" -acodec copy -bsf:a aac_adtstoasc \"" + str + "\"";
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.CreateNoWindow = true;
      process.Start();
      process.WaitForExit();
      try
      {
        System.IO.File.Delete(source);
      }
      catch
      {
      }
      return str;
    }

    private double ExtractAAC(string source, string target)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.libs.ffmpegPath;
      process.StartInfo.WorkingDirectory = Path.GetDirectoryName(source);
      process.StartInfo.Arguments = "-y -i \"" + source + "\" -acodec copy \"" + target + "\"";
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardInput = true;
      process.StartInfo.RedirectStandardError = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.Start();
      string end = process.StandardError.ReadToEnd();
      process.WaitForExit();
      return this.ParseFFmpegOutputForTotalTime(end);
    }

    private double ParseFFmpegOutputForTotalTime(string output)
    {
      string[] strArray1 = output.Split('\n');
      string sTime = "";
      try
      {
        for (int index1 = 0; index1 < strArray1.Length; ++index1)
        {
          if (strArray1[index1].Trim().StartsWith("video:0kB"))
          {
            string[] strArray2 = strArray1[index1 - 1].Split('\r');
            for (int index2 = 0; index2 < strArray2.Length; ++index2)
            {
              if (strArray2[index2].Trim() != "")
                sTime = strArray2[index2].Split('=')[2].Split(' ')[0].Trim();
            }
          }
        }
      }
      catch
      {
        sTime = "00:00:00";
      }
      return Utilities.ConvertTimeStringToDouble(sTime);
    }

    private void FixMP3File(string file)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.libs.mp3valPath;
      process.StartInfo.Arguments = "-f -nb \"" + file + "\"";
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.CreateNoWindow = true;
      process.Start();
      process.WaitForExit();
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}
