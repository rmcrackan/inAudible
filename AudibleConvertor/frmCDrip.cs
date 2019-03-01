// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.frmCDrip
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using IniParser;
using Ionic.Utils;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AudibleConvertor
{
  public class frmCDrip : Form
  {
    public string iniPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "inAudible", "config.ini");
    private string driveLetter = "";
    private IContainer components;
    private ComboBox cmbDrives;
    private Label label1;
    private DataGridView dgvTracks;
    private DataGridViewTextBoxColumn track;
    private DataGridViewTextBoxColumn duration;
    private Button btnBegin;
    private Button btnSetDestinationPath;
    private ProgressBar progressBar1;
    private Button btnClose;
    public TextBox txtOutputPath;
    private CheckBox chkParanoia;
    private CheckBox chkMultidisc;
    private Button btnCancel;
    private TextBox txtRipLog;
    private CheckBox chkEject;
    private CheckBox chkBurst;
    private CheckBox chkRipOnInsert;
    private CheckBox chkDebug;
    public string cddaPath;
    public string cdWavPath;
    private string scsi;
    public bool apply;
    private bool paranoia;
    private bool debug;
    private SynchronizationContext _syncContext;
    private bool ripped;
    private BackgroundWorker m_oWorker;
    private ManagementEventWatcher watcher;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmCDrip));
      this.cmbDrives = new ComboBox();
      this.label1 = new Label();
      this.dgvTracks = new DataGridView();
      this.track = new DataGridViewTextBoxColumn();
      this.duration = new DataGridViewTextBoxColumn();
      this.btnBegin = new Button();
      this.btnSetDestinationPath = new Button();
      this.txtOutputPath = new TextBox();
      this.progressBar1 = new ProgressBar();
      this.btnClose = new Button();
      this.chkParanoia = new CheckBox();
      this.chkMultidisc = new CheckBox();
      this.btnCancel = new Button();
      this.txtRipLog = new TextBox();
      this.chkEject = new CheckBox();
      this.chkBurst = new CheckBox();
      this.chkRipOnInsert = new CheckBox();
      this.chkDebug = new CheckBox();
      ((ISupportInitialize) this.dgvTracks).BeginInit();
      this.SuspendLayout();
      this.cmbDrives.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.cmbDrives.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbDrives.FormattingEnabled = true;
      this.cmbDrives.Location = new Point(72, 10);
      this.cmbDrives.Name = "cmbDrives";
      this.cmbDrives.Size = new Size(382, 21);
      this.cmbDrives.TabIndex = 0;
      this.cmbDrives.SelectedIndexChanged += new EventHandler(this.cmbDrives_SelectedIndexChanged);
      this.label1.AutoSize = true;
      this.label1.Location = new Point(13, 13);
      this.label1.Name = "label1";
      this.label1.Size = new Size(53, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "CD Drive:";
      this.dgvTracks.AllowUserToAddRows = false;
      this.dgvTracks.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
      this.dgvTracks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvTracks.Columns.AddRange((DataGridViewColumn) this.track, (DataGridViewColumn) this.duration);
      this.dgvTracks.Location = new Point(16, 84);
      this.dgvTracks.Name = "dgvTracks";
      this.dgvTracks.Size = new Size(246, 333);
      this.dgvTracks.TabIndex = 2;
      this.track.HeaderText = "Track";
      this.track.Name = "track";
      this.track.ReadOnly = true;
      this.duration.HeaderText = "Duration";
      this.duration.Name = "duration";
      this.duration.ReadOnly = true;
      this.btnBegin.Location = new Point(279, 182);
      this.btnBegin.Name = "btnBegin";
      this.btnBegin.Size = new Size(75, 23);
      this.btnBegin.TabIndex = 3;
      this.btnBegin.Text = "Rip";
      this.btnBegin.UseVisualStyleBackColor = true;
      this.btnBegin.Click += new EventHandler(this.btnBegin_Click);
      this.btnSetDestinationPath.Location = new Point(16, 46);
      this.btnSetDestinationPath.Name = "btnSetDestinationPath";
      this.btnSetDestinationPath.Size = new Size(75, 23);
      this.btnSetDestinationPath.TabIndex = 4;
      this.btnSetDestinationPath.Text = "Output Path";
      this.btnSetDestinationPath.UseVisualStyleBackColor = true;
      this.btnSetDestinationPath.Click += new EventHandler(this.btnSetDestinationPath_Click);
      this.txtOutputPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txtOutputPath.Location = new Point(97, 48);
      this.txtOutputPath.Name = "txtOutputPath";
      this.txtOutputPath.Size = new Size(358, 20);
      this.txtOutputPath.TabIndex = 5;
      this.progressBar1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.progressBar1.Location = new Point(16, 423);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new Size(438, 23);
      this.progressBar1.Style = ProgressBarStyle.Continuous;
      this.progressBar1.TabIndex = 6;
      this.btnClose.Anchor = AnchorStyles.Bottom;
      this.btnClose.Location = new Point(196, 452);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new Size(75, 23);
      this.btnClose.TabIndex = 7;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new EventHandler(this.btnClose_Click);
      this.chkParanoia.AutoSize = true;
      this.chkParanoia.Location = new Point(299, 130);
      this.chkParanoia.Name = "chkParanoia";
      this.chkParanoia.Size = new Size(137, 17);
      this.chkParanoia.TabIndex = 8;
      this.chkParanoia.Text = "Paranoia Mode (slower)";
      this.chkParanoia.UseVisualStyleBackColor = true;
      this.chkMultidisc.AutoSize = true;
      this.chkMultidisc.Checked = true;
      this.chkMultidisc.CheckState = CheckState.Checked;
      this.chkMultidisc.Location = new Point(299, 84);
      this.chkMultidisc.Name = "chkMultidisc";
      this.chkMultidisc.Size = new Size(84, 17);
      this.chkMultidisc.TabIndex = 9;
      this.chkMultidisc.Text = "Mutidisc Rip";
      this.chkMultidisc.UseVisualStyleBackColor = true;
      this.btnCancel.Enabled = false;
      this.btnCancel.Location = new Point(379, 182);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 10;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.txtRipLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.txtRipLog.Location = new Point(279, 211);
      this.txtRipLog.Multiline = true;
      this.txtRipLog.Name = "txtRipLog";
      this.txtRipLog.ScrollBars = ScrollBars.Vertical;
      this.txtRipLog.Size = new Size(176, 206);
      this.txtRipLog.TabIndex = 11;
      this.chkEject.AutoSize = true;
      this.chkEject.Checked = true;
      this.chkEject.CheckState = CheckState.Checked;
      this.chkEject.Location = new Point(299, 107);
      this.chkEject.Name = "chkEject";
      this.chkEject.Size = new Size(119, 17);
      this.chkEject.TabIndex = 12;
      this.chkEject.Text = "Eject on completion";
      this.chkEject.UseVisualStyleBackColor = true;
      this.chkBurst.AutoSize = true;
      this.chkBurst.Checked = true;
      this.chkBurst.CheckState = CheckState.Checked;
      this.chkBurst.Location = new Point(299, 153);
      this.chkBurst.Name = "chkBurst";
      this.chkBurst.Size = new Size(80, 17);
      this.chkBurst.TabIndex = 13;
      this.chkBurst.Text = "Burst Mode";
      this.chkBurst.UseVisualStyleBackColor = true;
      this.chkRipOnInsert.AutoSize = true;
      this.chkRipOnInsert.Location = new Point(379, 84);
      this.chkRipOnInsert.Name = "chkRipOnInsert";
      this.chkRipOnInsert.Size = new Size(85, 17);
      this.chkRipOnInsert.TabIndex = 14;
      this.chkRipOnInsert.Text = "Rip on insert";
      this.chkRipOnInsert.UseVisualStyleBackColor = true;
      this.chkDebug.AutoSize = true;
      this.chkDebug.Location = new Point(379, 153);
      this.chkDebug.Name = "chkDebug";
      this.chkDebug.Size = new Size(58, 17);
      this.chkDebug.TabIndex = 15;
      this.chkDebug.Text = "Debug";
      this.chkDebug.UseVisualStyleBackColor = true;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(467, 487);
      this.Controls.Add((Control) this.chkDebug);
      this.Controls.Add((Control) this.chkRipOnInsert);
      this.Controls.Add((Control) this.chkBurst);
      this.Controls.Add((Control) this.chkEject);
      this.Controls.Add((Control) this.txtRipLog);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.chkMultidisc);
      this.Controls.Add((Control) this.chkParanoia);
      this.Controls.Add((Control) this.btnClose);
      this.Controls.Add((Control) this.progressBar1);
      this.Controls.Add((Control) this.txtOutputPath);
      this.Controls.Add((Control) this.btnSetDestinationPath);
      this.Controls.Add((Control) this.btnBegin);
      this.Controls.Add((Control) this.dgvTracks);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.cmbDrives);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (frmCDrip);
      this.Text = "Rip CD to WAV";
      this.FormClosing += new FormClosingEventHandler(this.frmCDrip_FormClosing);
      ((ISupportInitialize) this.dgvTracks).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public frmCDrip()
    {
      this.InitializeComponent();
      this.networkDevice();
      this.IsDriveReady();
      this._syncContext = SynchronizationContext.Current;
    }

    private bool IsDriveReady()
    {
      bool flag = false;
      foreach (DriveInfo driveInfo in ((IEnumerable<DriveInfo>) DriveInfo.GetDrives()).Where<DriveInfo>((System.Func<DriveInfo, bool>) (d => d.DriveType == DriveType.CDRom)))
      {
        if (driveInfo.IsReady)
        {
          this.driveLetter = driveInfo.Name;
          flag = true;
        }
      }
      return flag;
    }

    public void CreateDriveDropdown()
    {
      foreach (object cdDrive in this.GetCdDrives())
        this.cmbDrives.Items.Add(cdDrive);
    }

    private List<string> GetCdDrives()
    {
      List<string> stringList = new List<string>();
      Process process = new Process();
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.RedirectStandardError = true;
      process.StartInfo.FileName = this.cddaPath;
      process.StartInfo.Arguments = "-info-only -gui -scanbus";
      process.Start();
      string end = process.StandardError.ReadToEnd();
      process.WaitForExit();
      string str1 = end;
      char[] chArray1 = new char[1]{ '\n' };
      foreach (string text in str1.Split(chArray1))
      {
        Audible.diskLogger(text);
        if (text.ToUpper().Contains("ROM") || text.ToUpper().Contains("DVD") || text.ToLower().Contains("emova"))
        {
          string[] strArray = text.Split('\t');
          string str2 = strArray[1] + " - " + strArray[2];
          stringList.Add(str2);
        }
      }
      if (stringList.Count == 0)
      {
        string str2 = end;
        char[] chArray2 = new char[1]{ '\n' };
        foreach (string str3 in str2.Split(chArray2))
        {
          if (str3.StartsWith("\t") && !str3.Contains("*") && !str3.Contains("HOST ADAPTOR"))
          {
            string[] strArray = str3.Split('\t');
            string str4 = strArray[1] + " - " + strArray[2];
            stringList.Add(str4);
          }
        }
      }
      return stringList;
    }

    private void cmbDrives_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.cmbDrives.SelectedIndex < 0)
        return;
      this.scsi = this.cmbDrives.Items[this.cmbDrives.SelectedIndex].ToString().Split('-')[0].Trim();
      this.UpdateGridWithTracks();
    }

    private void UpdateGridWithTracks()
    {
      List<string> tracks = this.GetTracks(this.scsi);
      this.dgvTracks.Rows.Clear();
      int num = 1;
      foreach (string str in tracks)
      {
        int index = this.dgvTracks.Rows.Add();
        this.dgvTracks.Rows[index].Cells[0].Value = (object) num;
        this.dgvTracks.Rows[index].Cells[1].Value = (object) str;
        ++num;
      }
      this.SetText((num - 1).ToString() + " tracks on disc");
    }

    private List<string> GetTracks(string scsi)
    {
      List<string> stringList = new List<string>();
      Process p = new Process();
      p.StartInfo.UseShellExecute = false;
      p.StartInfo.CreateNoWindow = true;
      p.StartInfo.RedirectStandardOutput = true;
      p.StartInfo.RedirectStandardError = true;
      p.StartInfo.FileName = this.cddaPath;
      p.StartInfo.Arguments = "-info-only dev=" + scsi + " -gui -verbose-level toc";
      string txtOutput = "";
      Thread thread = new Thread((ThreadStart) (() =>
      {
        p.Start();
        txtOutput = p.StandardError.ReadToEnd();
        p.WaitForExit();
      }));
      thread.Start();
      bool flag = false;
      for (int index = 0; index < 50; ++index)
      {
        Thread.Sleep(100);
        if (!thread.IsAlive)
        {
          flag = true;
          break;
        }
      }
      if (!flag)
      {
        p.Kill();
        return stringList;
      }
      string[] strArray1 = txtOutput.Split('\n');
      int num = 0;
      for (int index = 0; index < strArray1.Length; ++index)
      {
        if (strArray1[index].StartsWith("Album title:"))
        {
          num = index + 1;
          break;
        }
      }
      for (int index1 = num; index1 < strArray1.Length && strArray1[index1].StartsWith("T"); ++index1)
      {
        string[] strArray2 = strArray1[index1].Split(' ');
        for (int index2 = 1; index2 < strArray2.Length; ++index2)
        {
          if (strArray2[index2].Contains<char>(':'))
          {
            stringList.Add(strArray2[index2]);
            break;
          }
        }
      }
      return stringList;
    }

    private void btnSetDestinationPath_Click(object sender, EventArgs e)
    {
      try
      {
        this.cdWavPath = new FileIniDataParser().LoadFile(this.iniPath)["system"]["wav_path"];
      }
      catch
      {
      }
      if (CommonFileDialog.IsPlatformSupported)
      {
        CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
        commonOpenFileDialog.IsFolderPicker = true;
        commonOpenFileDialog.Title = "Select source folder";
        if (this.txtOutputPath.Text.Trim() != "")
          commonOpenFileDialog.InitialDirectory = this.txtOutputPath.Text;
        else if (this.cdWavPath != "")
          commonOpenFileDialog.InitialDirectory = this.cdWavPath;
        if (commonOpenFileDialog.ShowDialog() != CommonFileDialogResult.Ok)
          return;
        this.txtOutputPath.Text = commonOpenFileDialog.FileName;
      }
      else
      {
        FolderBrowserDialogEx folderBrowserDialogEx = new FolderBrowserDialogEx();
        folderBrowserDialogEx.Description = "Select a folder to write to:";
        folderBrowserDialogEx.ShowNewFolderButton = true;
        folderBrowserDialogEx.ShowEditBox = true;
        if (this.txtOutputPath.Text.Trim() != "")
          folderBrowserDialogEx.SelectedPath = this.txtOutputPath.Text;
        else if (this.cdWavPath != "")
          folderBrowserDialogEx.SelectedPath = this.cdWavPath;
        folderBrowserDialogEx.ShowFullPathInEditBox = true;
        folderBrowserDialogEx.RootFolder = Environment.SpecialFolder.MyComputer;
        if (folderBrowserDialogEx.ShowDialog() != DialogResult.OK)
          return;
        this.txtOutputPath.Text = folderBrowserDialogEx.SelectedPath;
      }
    }

    private void btnBegin_Click(object sender, EventArgs e)
    {
      if (this.txtOutputPath.Text.Trim() == "" || !Directory.Exists(this.txtOutputPath.Text.Trim()))
      {
        int num1 = (int) MessageBox.Show("You need to specify a valid output directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
      }
      else if (this.scsi == null || this.scsi == "")
      {
        int num2 = (int) MessageBox.Show("You need to select a CD drive.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
      }
      else if (!this.IsDriveReady())
      {
        int num3 = (int) MessageBox.Show("There does not appear to be a disc in the drive.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
      }
      else
      {
        this.ripped = true;
        this.paranoia = this.chkParanoia.Checked;
        this.debug = this.chkDebug.Checked;
        int num4 = 1;
        if (this.chkMultidisc.Checked)
          num4 = this.GetNextTrackNumber();
        this.dgvTracks.Refresh();
        this.m_oWorker = new BackgroundWorker();
        this.m_oWorker.DoWork += new DoWorkEventHandler(this.m_oWorker_DoWork);
        this.m_oWorker.ProgressChanged += new ProgressChangedEventHandler(this.m_oWorker_ProgressChanged);
        this.m_oWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.m_oWorker_RunWorkerCompleted);
        this.m_oWorker.WorkerReportsProgress = true;
        this.m_oWorker.WorkerSupportsCancellation = true;
        this.m_oWorker.RunWorkerAsync((object) num4);
        this.btnCancel.Enabled = true;
        this.btnBegin.Enabled = false;
        this.btnClose.Enabled = false;
      }
    }

    private void m_oWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (!e.Cancelled && e.Error == null && this.chkEject.Checked)
      {
        this.SetText("Ejecting...");
        if (this.driveLetter == "")
        {
          frmCDrip.mciSendString("set CDAudio door open", (StringBuilder) null, 0, IntPtr.Zero);
        }
        else
        {
          int num = (int) this.driveLetter.ToLower().ToCharArray()[0];
          frmCDrip.mciSendString("open " + this.driveLetter + " type CDAudio alias drive" + this.driveLetter.Substring(0, 1), (StringBuilder) null, 0, IntPtr.Zero);
          frmCDrip.mciSendString("set drive" + this.driveLetter.Substring(0, 1) + " door open", (StringBuilder) null, 0, IntPtr.Zero);
        }
      }
      this.SetText("Completed.");
      this.btnCancel.Enabled = false;
      this.btnBegin.Enabled = true;
      this.btnClose.Enabled = true;
    }

    private void m_oWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      this.progressBar1.Value = e.ProgressPercentage;
    }

    private void m_oWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      int nextTrackNumber = (int) e.Argument;
      if (this.chkBurst.Checked)
        this.RipAllTracks(nextTrackNumber, this.txtOutputPath.Text);
      else
        this.RipSelectedTracks(nextTrackNumber);
    }

    private void RipAllTracks(int nextTrackNumber, string outputFolder)
    {
      int trackNum = 1;
      string str = "";
      if (this.paranoia)
        str = "-paranoia";
      Process process = new Process()
      {
        StartInfo = new ProcessStartInfo()
        {
          FileName = this.cddaPath,
          UseShellExecute = false,
          RedirectStandardOutput = true,
          RedirectStandardError = true,
          WorkingDirectory = outputFolder,
          Arguments = "dev=" + this.scsi + " -gui -alltracks " + str + " -no-infofile",
          CreateNoWindow = true
        }
      };
      process.OutputDataReceived += (DataReceivedEventHandler) ((sender, args) => this.Display(args.Data, ref trackNum, ref nextTrackNumber, outputFolder));
      process.ErrorDataReceived += (DataReceivedEventHandler) ((sender, args) => this.Display(args.Data, ref trackNum, ref nextTrackNumber, outputFolder));
      this.dgvTracks.Rows[trackNum - 1].Selected = true;
      process.Start();
      process.BeginOutputReadLine();
      process.BeginErrorReadLine();
      process.WaitForExit();
    }

    private void Display(string output, ref int trackNum, ref int nextTrackNumber, string outputFolder)
    {
      if (output == null)
        return;
      if (this.debug)
        this.SetText(output);
      if (output.Contains("100%"))
      {
        if (output.Contains("recorded"))
        {
          try
          {
            this.SetText("Track " + (object) trackNum + " (" + (object) nextTrackNumber + ")");
            Form1.SetControlPropertyThreadSafe((Control) this.dgvTracks, "FirstDisplayedScrollingRowIndex", (object) this.dgvTracks.SelectedRows[0].Index);
            Audible.diskLogger(output);
            this.dgvTracks.Rows[trackNum - 1].Selected = false;
            if (output.Contains("medium") || output.Contains("noticable"))
            {
              this.dgvTracks.Rows[trackNum - 1].DefaultCellStyle.BackColor = Color.Yellow;
              this.SetText(output.Split('%')[1].Trim() + "%" + output.Split('%')[2].Trim());
            }
            else if (output.Contains("major"))
            {
              this.dgvTracks.Rows[trackNum - 1].DefaultCellStyle.BackColor = Color.Red;
              this.SetText(output.Split('%')[1].Trim() + "%" + output.Split('%')[2].Trim());
            }
            else if (!output.Contains("success"))
            {
              this.dgvTracks.Rows[trackNum - 1].DefaultCellStyle.BackColor = Color.Orange;
              this.SetText(output.Split('%')[1].Trim() + "% " + output.Split('%')[2].Trim());
            }
            else
              this.dgvTracks.Rows[trackNum - 1].DefaultCellStyle.BackColor = Color.LightGreen;
            if (this.dgvTracks.Rows.Count > trackNum)
              this.dgvTracks.Rows[trackNum].Selected = true;
            string str1 = outputFolder + "\\track - " + nextTrackNumber.ToString("D3") + ".wav";
            string str2 = "audio_" + trackNum.ToString("D2") + ".wav";
            File.Delete(str1);
            File.Move(outputFolder + "\\" + str2, str1);
            ++nextTrackNumber;
            ++trackNum;
            this.m_oWorker.ReportProgress(100);
            return;
          }
          catch (Exception ex)
          {
            Audible.diskLogger("Blew up at 100% - " + output + " : " + ex.ToString());
            return;
          }
        }
      }
      if (!output.Contains<char>('%'))
        return;
      int result = 0;
      try
      {
        if (!int.TryParse(output.Split('%')[0], out result))
          return;
        this.m_oWorker.ReportProgress(result);
      }
      catch (Exception ex)
      {
        Audible.diskLogger("Blew up - " + output + " : " + ex.ToString());
      }
    }

    private int GetNextTrackNumber()
    {
      IOrderedEnumerable<string> orderedEnumerable = ((IEnumerable<string>) Directory.GetFiles(this.txtOutputPath.Text, "*.wav")).OrderBy<string, string>((System.Func<string, string>) (f => new DirectoryInfo(f).Name));
      int num = 0;
      foreach (string path in (IEnumerable<string>) orderedEnumerable)
      {
        string fileName = Path.GetFileName(path);
        string[] strArray;
        if (fileName.Contains<char>('-'))
          strArray = fileName.Split('-');
        else
          strArray = fileName.Split('_');
        try
        {
          num = int.Parse(strArray[1].Trim().Split('.')[0]);
        }
        catch
        {
          Audible.diskLogger("Failed to parse track number = " + (object) strArray + " for file " + path);
        }
      }
      return num + 1;
    }

    private void RipSelectedTracks(int nextTrackNumber)
    {
      for (int index = 0; index < this.dgvTracks.Rows.Count; ++index)
      {
        this.SetText("Track " + (object) (index + 1) + " (" + (object) nextTrackNumber + ")");
        this.dgvTracks.Rows[index].Selected = true;
        Form1.SetControlPropertyThreadSafe((Control) this.dgvTracks, "FirstDisplayedScrollingRowIndex", (object) this.dgvTracks.SelectedRows[0].Index);
        this.RipCDTrack(int.Parse(this.dgvTracks.Rows[index].Cells[0].Value.ToString()), this.txtOutputPath.Text, nextTrackNumber);
        ++nextTrackNumber;
        int percentProgress = (int) ((double) (index + 1) / (double) this.dgvTracks.Rows.Count * 100.0);
        this.dgvTracks.Rows[index].Selected = false;
        this.dgvTracks.Rows[index].DefaultCellStyle.BackColor = Color.LightGreen;
        this.m_oWorker.ReportProgress(percentProgress);
        if (this.m_oWorker.CancellationPending)
        {
          this.ripped = false;
          this.m_oWorker.ReportProgress(0);
          break;
        }
      }
    }

    private void RipCDTrack(int track, string outputFolder, int newTrackNumber)
    {
      string str1 = "";
      if (this.paranoia)
        str1 = "-paranoia audio";
      Process process = new Process();
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.WorkingDirectory = outputFolder;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.RedirectStandardError = true;
      process.StartInfo.FileName = this.cddaPath;
      process.StartInfo.Arguments = "dev=" + this.scsi + " -gui " + str1 + " -s -x -t " + (object) track;
      process.Start();
      process.StandardError.ReadToEnd();
      process.WaitForExit();
      string str2 = outputFolder + "\\track - " + newTrackNumber.ToString("D3") + ".wav";
      File.Delete(str2);
      File.Move(outputFolder + "\\audio.wav", str2);
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      if (this.ripped)
        this.apply = true;
      this.Close();
    }

    public void networkDevice()
    {
      try
      {
        this.watcher = new ManagementEventWatcher(new ManagementScope("\\root\\CIMV2", new ConnectionOptions()
        {
          EnablePrivileges = true,
          Authority = (string) null,
          Authentication = AuthenticationLevel.Default
        }), (EventQuery) new WqlEventQuery()
        {
          EventClassName = "__InstanceModificationEvent",
          WithinInterval = new TimeSpan(0, 0, 1),
          Condition = "TargetInstance ISA 'Win32_LogicalDisk' and TargetInstance.DriveType = 5"
        });
        this.watcher.EventArrived += new EventArrivedEventHandler(this.watcher_EventArrived);
        this.watcher.Start();
      }
      catch (ManagementException ex)
      {
      }
    }

    private void watcher_EventArrived(object sender, EventArrivedEventArgs e)
    {
      ManagementBaseObject managementBaseObject = (ManagementBaseObject) e.NewEvent["TargetInstance"];
      string str = (string) managementBaseObject["DeviceID"];
      Console.WriteLine(str);
      Console.WriteLine(managementBaseObject.Properties["VolumeName"].Value);
      Console.WriteLine((string) managementBaseObject["Name"]);
      if (managementBaseObject.Properties["VolumeName"].Value != null)
      {
        this.SetText("CD inserted");
        this.driveLetter = str;
        this.UpdateGrid();
        if (!this.chkRipOnInsert.Checked || !this.ripped)
          return;
        this.Invoke((System.Action) (() => this.btnBegin.PerformClick()));
      }
      else
      {
        Console.WriteLine("CD has been ejected");
        this.SetText("CD ejected");
      }
    }

    private void UpdateGrid()
    {
      if (this.dgvTracks.InvokeRequired)
        this.Invoke((Delegate) new frmCDrip.SetGridCallback(this.UpdateGrid), new object[0]);
      else
        this.UpdateGridWithTracks();
    }

    private void frmCDrip_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.watcher.Stop();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      if (!this.m_oWorker.IsBusy)
        return;
      this.m_oWorker.CancelAsync();
    }

    private void Log(string text)
    {
      int num = 60000;
      string text1 = "[" + DateTime.Now.ToLongTimeString() + "] - " + text + "\r\n";
      if (this.txtRipLog.Text.Length + text1.Length > num)
        this.txtRipLog.Text = this.txtRipLog.Text.Substring(text1.Length, this.txtRipLog.Text.Length - text1.Length);
      this.txtRipLog.AppendText(text1);
    }

    [DllImport("winmm.dll")]
    private static extern int mciSendString(string command, StringBuilder buffer, int bufferSize, IntPtr hwndCallback);

    private void SetText(string text)
    {
      if (this.txtRipLog.InvokeRequired)
        this.Invoke((Delegate) new frmCDrip.SetTextCallback(this.SetText), (object) text);
      else
        this.Log(text);
    }

    private delegate void SetGridCallback();

    private delegate void SetTextCallback(string text);
  }
}
