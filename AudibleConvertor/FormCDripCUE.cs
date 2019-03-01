// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.FormCDripCUE
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using IniParser;
using Ionic.Utils;
using Microsoft.WindowsAPICodePack.Dialogs;
using ProgressODoom;
using Ripper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AudibleConvertor
{
  public class FormCDripCUE : Form
  {
    public string iniPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "inAudible", "config.ini");
    private string driveLetter = "";
    private string imageFileName = "";
    private string sourceWavName = "";
    private IContainer components;
    private ComboBox cmbDrives;
    private Label label1;
    private DataGridView dgvTracks;
    private Button btnBegin;
    private Button btnSetDestinationPath;
    private Button btnClose;
    public TextBox txtOutputPath;
    private CheckBox chkMultidisc;
    private Button btnCancel;
    private CheckBox chkEject;
    private CheckBox chkRipOnInsert;
    private CheckBox chkDebug;
    private CheckBox chkSingleWav;
    private DataGridViewTextBoxColumn track;
    private DataGridViewTextBoxColumn duration;
    private TextBox txtErrors;
    private TextBox txtSpeed;
    private TextBox txtTimeLeft;
    private Label lblErrors;
    private Label lblSpeed;
    private Label lblRipTime;
    private RadioButton rdBurst;
    private RadioButton rdSecure;
    private RadioButton rdParanoid;
    private GroupBox groupBox1;
    private CheckBox chkAccurateRip;
    private RichTextBox rtbActivityLog;
    private DualProgressBar dualProgressBar1;
    private PlainBackgroundPainter plainBackgroundPainter1;
    private PlainBorderPainter plainBorderPainter1;
    private PlainProgressPainter plainProgressPainter1;
    private RoundGlossPainter roundGlossPainter1;
    private PlainProgressPainter plainProgressPainter2;
    private RoundGlossPainter roundGlossPainter2;
    private GradientGlossPainter gradientGlossPainter1;
    public string cddaPath;
    public SupportLibraries supportLibs;
    public string cdWavPath;
    private string scsi;
    public bool apply;
    private bool paranoia;
    private bool debug;
    private long[] trackSizes;
    private Process ripProcess;
    private int currentTrackProgress;
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormCDripCUE));
      this.cmbDrives = new ComboBox();
      this.label1 = new Label();
      this.dgvTracks = new DataGridView();
      this.track = new DataGridViewTextBoxColumn();
      this.duration = new DataGridViewTextBoxColumn();
      this.btnBegin = new Button();
      this.btnSetDestinationPath = new Button();
      this.txtOutputPath = new TextBox();
      this.btnClose = new Button();
      this.chkMultidisc = new CheckBox();
      this.btnCancel = new Button();
      this.chkEject = new CheckBox();
      this.chkRipOnInsert = new CheckBox();
      this.chkDebug = new CheckBox();
      this.chkSingleWav = new CheckBox();
      this.txtErrors = new TextBox();
      this.txtSpeed = new TextBox();
      this.txtTimeLeft = new TextBox();
      this.lblErrors = new Label();
      this.lblSpeed = new Label();
      this.lblRipTime = new Label();
      this.rdBurst = new RadioButton();
      this.rdSecure = new RadioButton();
      this.rdParanoid = new RadioButton();
      this.groupBox1 = new GroupBox();
      this.chkAccurateRip = new CheckBox();
      this.rtbActivityLog = new RichTextBox();
      this.dualProgressBar1 = new DualProgressBar();
      this.plainBackgroundPainter1 = new PlainBackgroundPainter();
      this.roundGlossPainter1 = new RoundGlossPainter();
      this.plainBorderPainter1 = new PlainBorderPainter();
      this.plainProgressPainter2 = new PlainProgressPainter();
      this.roundGlossPainter2 = new RoundGlossPainter();
      this.plainProgressPainter1 = new PlainProgressPainter();
      this.gradientGlossPainter1 = new GradientGlossPainter();
      ((ISupportInitialize) this.dgvTracks).BeginInit();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      this.cmbDrives.AccessibleDescription = "Select which CD drive to rip from";
      this.cmbDrives.AccessibleName = "Source CD drive";
      this.cmbDrives.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.cmbDrives.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbDrives.FormattingEnabled = true;
      this.cmbDrives.Location = new Point(72, 10);
      this.cmbDrives.Name = "cmbDrives";
      this.cmbDrives.Size = new Size(456, 21);
      this.cmbDrives.TabIndex = 0;
      this.cmbDrives.SelectedIndexChanged += new EventHandler(this.cmbDrives_SelectedIndexChanged);
      this.label1.AutoSize = true;
      this.label1.Location = new Point(13, 13);
      this.label1.Name = "label1";
      this.label1.Size = new Size(53, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "CD Drive:";
      this.dgvTracks.AccessibleDescription = "List of tracks on CD";
      this.dgvTracks.AccessibleName = "Track list";
      this.dgvTracks.AllowUserToAddRows = false;
      this.dgvTracks.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
      this.dgvTracks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvTracks.Columns.AddRange((DataGridViewColumn) this.track, (DataGridViewColumn) this.duration);
      this.dgvTracks.Location = new Point(16, 84);
      this.dgvTracks.Name = "dgvTracks";
      this.dgvTracks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgvTracks.Size = new Size(246, 346);
      this.dgvTracks.TabIndex = 2;
      this.track.HeaderText = "Track";
      this.track.Name = "track";
      this.track.ReadOnly = true;
      this.track.Width = 50;
      this.duration.HeaderText = "Duration";
      this.duration.Name = "duration";
      this.duration.ReadOnly = true;
      this.duration.Width = 150;
      this.btnBegin.AccessibleDescription = "Start ripping this CD";
      this.btnBegin.AccessibleName = "Begin rip";
      this.btnBegin.Location = new Point(279, 204);
      this.btnBegin.Name = "btnBegin";
      this.btnBegin.Size = new Size(75, 23);
      this.btnBegin.TabIndex = 3;
      this.btnBegin.Text = "Rip";
      this.btnBegin.UseVisualStyleBackColor = true;
      this.btnBegin.Click += new EventHandler(this.btnBegin_Click);
      this.btnSetDestinationPath.AccessibleDescription = "Select path where WAV files will be created";
      this.btnSetDestinationPath.AccessibleName = "Select output path";
      this.btnSetDestinationPath.Location = new Point(16, 46);
      this.btnSetDestinationPath.Name = "btnSetDestinationPath";
      this.btnSetDestinationPath.Size = new Size(75, 23);
      this.btnSetDestinationPath.TabIndex = 4;
      this.btnSetDestinationPath.Text = "Output Path";
      this.btnSetDestinationPath.UseVisualStyleBackColor = true;
      this.btnSetDestinationPath.Click += new EventHandler(this.btnSetDestinationPath_Click);
      this.txtOutputPath.AccessibleDescription = "Target path";
      this.txtOutputPath.AccessibleName = "Target path";
      this.txtOutputPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txtOutputPath.Location = new Point(97, 48);
      this.txtOutputPath.Name = "txtOutputPath";
      this.txtOutputPath.Size = new Size(432, 20);
      this.txtOutputPath.TabIndex = 5;
      this.btnClose.AccessibleDescription = "Close the ripper";
      this.btnClose.AccessibleName = "Close";
      this.btnClose.Anchor = AnchorStyles.Bottom;
      this.btnClose.Location = new Point(233, 504);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new Size(75, 23);
      this.btnClose.TabIndex = 7;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new EventHandler(this.btnClose_Click);
      this.chkMultidisc.AccessibleDescription = "Number files in this directory assuming that they are all part of one book";
      this.chkMultidisc.AccessibleName = "Multi-disc rip";
      this.chkMultidisc.AutoSize = true;
      this.chkMultidisc.Checked = true;
      this.chkMultidisc.CheckState = CheckState.Checked;
      this.chkMultidisc.Location = new Point(126, 19);
      this.chkMultidisc.Name = "chkMultidisc";
      this.chkMultidisc.Size = new Size(84, 17);
      this.chkMultidisc.TabIndex = 9;
      this.chkMultidisc.Text = "Mutidisc Rip";
      this.chkMultidisc.UseVisualStyleBackColor = true;
      this.btnCancel.AccessibleDescription = "Abort the rip";
      this.btnCancel.AccessibleName = "Cancel the rip";
      this.btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btnCancel.Enabled = false;
      this.btnCancel.Location = new Point(453, 204);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 10;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.chkEject.AccessibleDescription = "Eject disc after rip is complete";
      this.chkEject.AccessibleName = "Eject on completion";
      this.chkEject.AutoSize = true;
      this.chkEject.Checked = true;
      this.chkEject.CheckState = CheckState.Checked;
      this.chkEject.Location = new Point(125, 65);
      this.chkEject.Name = "chkEject";
      this.chkEject.Size = new Size(119, 17);
      this.chkEject.TabIndex = 12;
      this.chkEject.Text = "Eject on completion";
      this.chkEject.UseVisualStyleBackColor = true;
      this.chkRipOnInsert.AccessibleDescription = "Begin ripping as soon as disc is inserted";
      this.chkRipOnInsert.AccessibleName = "Rip on insert";
      this.chkRipOnInsert.AutoSize = true;
      this.chkRipOnInsert.Location = new Point(125, 42);
      this.chkRipOnInsert.Name = "chkRipOnInsert";
      this.chkRipOnInsert.Size = new Size(85, 17);
      this.chkRipOnInsert.TabIndex = 14;
      this.chkRipOnInsert.Text = "Rip on insert";
      this.chkRipOnInsert.UseVisualStyleBackColor = true;
      this.chkDebug.AccessibleDescription = "Shows detailed debugging information of the rip in progress";
      this.chkDebug.AccessibleName = "Debug";
      this.chkDebug.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.chkDebug.AutoSize = true;
      this.chkDebug.Location = new Point(470, 508);
      this.chkDebug.Name = "chkDebug";
      this.chkDebug.Size = new Size(58, 17);
      this.chkDebug.TabIndex = 15;
      this.chkDebug.Text = "Debug";
      this.chkDebug.UseVisualStyleBackColor = true;
      this.chkSingleWav.AccessibleDescription = "Create a one WAV file that contains all tracks for this CD";
      this.chkSingleWav.AccessibleName = "Single WAV file";
      this.chkSingleWav.AutoSize = true;
      this.chkSingleWav.Location = new Point(125, 88);
      this.chkSingleWav.Name = "chkSingleWav";
      this.chkSingleWav.Size = new Size(83, 17);
      this.chkSingleWav.TabIndex = 16;
      this.chkSingleWav.Text = "Single WAV";
      this.chkSingleWav.UseVisualStyleBackColor = true;
      this.txtErrors.AccessibleDescription = "Current uncorrected errors";
      this.txtErrors.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.txtErrors.Location = new Point(55, 443);
      this.txtErrors.Name = "txtErrors";
      this.txtErrors.ReadOnly = true;
      this.txtErrors.Size = new Size(64, 20);
      this.txtErrors.TabIndex = 17;
      this.txtErrors.TextAlign = HorizontalAlignment.Right;
      this.txtSpeed.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.txtSpeed.Location = new Point(222, 444);
      this.txtSpeed.Name = "txtSpeed";
      this.txtSpeed.ReadOnly = true;
      this.txtSpeed.Size = new Size(39, 20);
      this.txtSpeed.TabIndex = 18;
      this.txtTimeLeft.AccessibleDescription = "Estimated total rip time";
      this.txtTimeLeft.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.txtTimeLeft.Location = new Point(421, 443);
      this.txtTimeLeft.Name = "txtTimeLeft";
      this.txtTimeLeft.ReadOnly = true;
      this.txtTimeLeft.Size = new Size(107, 20);
      this.txtTimeLeft.TabIndex = 19;
      this.txtTimeLeft.TextAlign = HorizontalAlignment.Right;
      this.lblErrors.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.lblErrors.AutoSize = true;
      this.lblErrors.Location = new Point(12, 446);
      this.lblErrors.Name = "lblErrors";
      this.lblErrors.Size = new Size(37, 13);
      this.lblErrors.TabIndex = 20;
      this.lblErrors.Text = "Errors:";
      this.lblSpeed.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.lblSpeed.AutoSize = true;
      this.lblSpeed.Location = new Point(156, 446);
      this.lblSpeed.Name = "lblSpeed";
      this.lblSpeed.Size = new Size(60, 13);
      this.lblSpeed.TabIndex = 21;
      this.lblSpeed.Text = "Rip Speed:";
      this.lblRipTime.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.lblRipTime.AutoSize = true;
      this.lblRipTime.Location = new Point(368, 446);
      this.lblRipTime.Name = "lblRipTime";
      this.lblRipTime.Size = new Size(52, 13);
      this.lblRipTime.TabIndex = 22;
      this.lblRipTime.Text = "Rip Time:";
      this.rdBurst.AutoSize = true;
      this.rdBurst.Checked = true;
      this.rdBurst.Location = new Point(6, 19);
      this.rdBurst.Name = "rdBurst";
      this.rdBurst.Size = new Size(88, 17);
      this.rdBurst.TabIndex = 23;
      this.rdBurst.TabStop = true;
      this.rdBurst.Text = "Burst (1 read)";
      this.rdBurst.UseVisualStyleBackColor = true;
      this.rdSecure.AccessibleDescription = "Secure mode - read each sector twice";
      this.rdSecure.AutoSize = true;
      this.rdSecure.Location = new Point(6, 42);
      this.rdSecure.Name = "rdSecure";
      this.rdSecure.Size = new Size(103, 17);
      this.rdSecure.TabIndex = 24;
      this.rdSecure.Text = "Secure (2 reads)";
      this.rdSecure.UseVisualStyleBackColor = true;
      this.rdParanoid.AccessibleDescription = "Paranoid mode - read as many times as necessary";
      this.rdParanoid.AutoSize = true;
      this.rdParanoid.Location = new Point(6, 65);
      this.rdParanoid.Name = "rdParanoid";
      this.rdParanoid.Size = new Size(111, 17);
      this.rdParanoid.TabIndex = 25;
      this.rdParanoid.Text = "Paranoid (n reads)";
      this.rdParanoid.UseVisualStyleBackColor = true;
      this.groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.groupBox1.Controls.Add((Control) this.chkAccurateRip);
      this.groupBox1.Controls.Add((Control) this.rdParanoid);
      this.groupBox1.Controls.Add((Control) this.rdSecure);
      this.groupBox1.Controls.Add((Control) this.rdBurst);
      this.groupBox1.Controls.Add((Control) this.chkSingleWav);
      this.groupBox1.Controls.Add((Control) this.chkRipOnInsert);
      this.groupBox1.Controls.Add((Control) this.chkMultidisc);
      this.groupBox1.Controls.Add((Control) this.chkEject);
      this.groupBox1.Location = new Point(279, 77);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(249, 121);
      this.groupBox1.TabIndex = 26;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Rip Options";
      this.chkAccurateRip.AccessibleDescription = "Verify rip against internet DB";
      this.chkAccurateRip.AccessibleName = "";
      this.chkAccurateRip.AutoSize = true;
      this.chkAccurateRip.Checked = true;
      this.chkAccurateRip.CheckState = CheckState.Checked;
      this.chkAccurateRip.Location = new Point(6, 88);
      this.chkAccurateRip.Name = "chkAccurateRip";
      this.chkAccurateRip.Size = new Size(85, 17);
      this.chkAccurateRip.TabIndex = 26;
      this.chkAccurateRip.Text = "AccurateRip";
      this.chkAccurateRip.UseVisualStyleBackColor = true;
      this.rtbActivityLog.AccessibleDescription = "Activity Log";
      this.rtbActivityLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.rtbActivityLog.Location = new Point(279, 233);
      this.rtbActivityLog.Name = "rtbActivityLog";
      this.rtbActivityLog.ScrollBars = RichTextBoxScrollBars.Vertical;
      this.rtbActivityLog.Size = new Size(249, 197);
      this.rtbActivityLog.TabIndex = 27;
      this.rtbActivityLog.Text = "";
      this.dualProgressBar1.AccessibleDescription = "Rip progress";
      this.dualProgressBar1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dualProgressBar1.BackgroundPainter = (IProgressBackgroundPainter) this.plainBackgroundPainter1;
      this.dualProgressBar1.BorderPainter = (IProgressBorderPainter) this.plainBorderPainter1;
      this.dualProgressBar1.Location = new Point(12, 469);
      this.dualProgressBar1.MarqueePercentage = 25;
      this.dualProgressBar1.MarqueeSpeed = 30;
      this.dualProgressBar1.MarqueeStep = 1;
      this.dualProgressBar1.MasterMaximum = 100;
      this.dualProgressBar1.MasterPainter = (IProgressPainter) this.plainProgressPainter2;
      this.dualProgressBar1.MasterProgressPadding = 0;
      this.dualProgressBar1.MasterValue = 0;
      this.dualProgressBar1.Maximum = 100;
      this.dualProgressBar1.Minimum = 0;
      this.dualProgressBar1.Name = "dualProgressBar1";
      this.dualProgressBar1.PaintMasterFirst = true;
      this.dualProgressBar1.ProgressPadding = 4;
      this.dualProgressBar1.ProgressPainter = (IProgressPainter) this.plainProgressPainter1;
      this.dualProgressBar1.ProgressType = ProgressType.Smooth;
      this.dualProgressBar1.ShowPercentage = false;
      this.dualProgressBar1.Size = new Size(517, 29);
      this.dualProgressBar1.TabIndex = 28;
      this.dualProgressBar1.Value = 0;
      this.plainBackgroundPainter1.Color = Color.FromArgb(240, 240, 240);
      this.plainBackgroundPainter1.GlossPainter = (IGlossPainter) this.roundGlossPainter1;
      this.roundGlossPainter1.AlphaHigh = (int) byte.MaxValue;
      this.roundGlossPainter1.AlphaLow = 0;
      this.roundGlossPainter1.Color = Color.FromArgb(213, 213, 213);
      this.roundGlossPainter1.Style = GlossStyle.Top;
      this.roundGlossPainter1.Successor = (IGlossPainter) null;
      this.roundGlossPainter1.TaperHeight = 3;
      this.plainBorderPainter1.Color = Color.Black;
      this.plainBorderPainter1.RoundedCorners = true;
      this.plainBorderPainter1.Style = PlainBorderPainter.PlainBorderStyle.Flat;
      this.plainProgressPainter2.Color = Color.Yellow;
      this.plainProgressPainter2.GlossPainter = (IGlossPainter) this.roundGlossPainter2;
      this.plainProgressPainter2.LeadingEdge = Color.FromArgb(192, 192, 0);
      this.plainProgressPainter2.ProgressBorderPainter = (IProgressBorderPainter) null;
      this.roundGlossPainter2.AlphaHigh = (int) byte.MaxValue;
      this.roundGlossPainter2.AlphaLow = 0;
      this.roundGlossPainter2.Color = Color.FromArgb(192, 192, 0);
      this.roundGlossPainter2.Style = GlossStyle.Top;
      this.roundGlossPainter2.Successor = (IGlossPainter) null;
      this.roundGlossPainter2.TaperHeight = 3;
      this.plainProgressPainter1.Color = Color.FromArgb(25, (int) sbyte.MaxValue, 0);
      this.plainProgressPainter1.GlossPainter = (IGlossPainter) this.gradientGlossPainter1;
      this.plainProgressPainter1.LeadingEdge = Color.FromArgb(0, 102, 0);
      this.plainProgressPainter1.ProgressBorderPainter = (IProgressBorderPainter) this.plainBorderPainter1;
      this.gradientGlossPainter1.AlphaHigh = 240;
      this.gradientGlossPainter1.AlphaLow = 0;
      this.gradientGlossPainter1.Angle = 90f;
      this.gradientGlossPainter1.Color = Color.FromArgb(188, 233, 143);
      this.gradientGlossPainter1.PercentageCovered = 90;
      this.gradientGlossPainter1.Style = GlossStyle.Top;
      this.gradientGlossPainter1.Successor = (IGlossPainter) null;
      this.AccessibleDescription = "Burst mode - only read each sector once";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(541, 533);
      this.Controls.Add((Control) this.dualProgressBar1);
      this.Controls.Add((Control) this.rtbActivityLog);
      this.Controls.Add((Control) this.groupBox1);
      this.Controls.Add((Control) this.lblRipTime);
      this.Controls.Add((Control) this.lblSpeed);
      this.Controls.Add((Control) this.lblErrors);
      this.Controls.Add((Control) this.txtTimeLeft);
      this.Controls.Add((Control) this.txtSpeed);
      this.Controls.Add((Control) this.txtErrors);
      this.Controls.Add((Control) this.chkDebug);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.btnClose);
      this.Controls.Add((Control) this.txtOutputPath);
      this.Controls.Add((Control) this.btnSetDestinationPath);
      this.Controls.Add((Control) this.btnBegin);
      this.Controls.Add((Control) this.dgvTracks);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.cmbDrives);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (FormCDripCUE);
      this.Text = "Rip CD to WAV";
      this.FormClosing += new FormClosingEventHandler(this.frmCDrip_FormClosing);
      ((ISupportInitialize) this.dgvTracks).EndInit();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public FormCDripCUE()
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
      foreach (string cdDrive in this.GetCdDrives())
        this.cmbDrives.Items.Add((object) (cdDrive + ": - " + this.GetHardwareInfo(cdDrive)));
    }

    private List<string> GetCdDrives()
    {
      List<string> stringList = new List<string>();
      foreach (char cdDriveLetter in CDDrive.GetCDDriveLetters())
        stringList.Add(cdDriveLetter.ToString());
      return stringList;
    }

    private string GetHardwareInfo(string driveLetter)
    {
      string str = "";
      try
      {
        foreach (ManagementObject managementObject in new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_CDROMDrive").Get())
        {
          if (managementObject["Drive"].ToString() == driveLetter + ":")
          {
            str = managementObject["Caption"].ToString();
            break;
          }
        }
      }
      catch (ManagementException ex)
      {
        int num = (int) MessageBox.Show("An error occurred while querying for WMI data: " + ex.Message);
      }
      return str;
    }

    private void cmbDrives_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.cmbDrives.SelectedIndex < 0)
        return;
      this.scsi = this.cmbDrives.Items[this.cmbDrives.SelectedIndex].ToString().Split(':')[0].Trim();
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
      CDDrive cdDrive = new CDDrive();
      try
      {
        cdDrive.Open(scsi.ToCharArray()[0]);
        if (cdDrive.Refresh())
        {
          int num1 = 176400;
          int numTracks = cdDrive.GetNumTracks();
          this.trackSizes = new long[numTracks];
          long num2 = 0;
          for (int track = 1; track <= numTracks; ++track)
          {
            double seconds = (double) cdDrive.TrackSize(track) / (double) num1;
            stringList.Add(FormCDripCUE.SecondsToFormattedTime(seconds));
            num2 = (long) cdDrive.TrackSize(track) + num2;
            this.trackSizes[track - 1] = num2;
          }
        }
        cdDrive.Close();
      }
      catch (Exception ex)
      {
        Audible.diskLogger("Error trying to get track list " + ex.ToString());
        cdDrive.Close();
      }
      return stringList;
    }

    public static string SecondsToFormattedTime(double seconds)
    {
      int num1 = (int) (seconds / 60.0 / 60.0);
      int num2 = (int) (seconds / 60.0) - num1 * 60;
      double num3 = Math.Round(seconds - (double) (num2 * 60) - (double) (num1 * 60 * 60), 0);
      if (num3 == 60.0)
      {
        ++num2;
        num3 = 0.0;
      }
      return num1.ToString("00") + ":" + num2.ToString("00") + ":" + num3.ToString("00");
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
        this.paranoia = this.rdParanoid.Checked;
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
        int num = (int) this.scsi.ToLower().ToCharArray()[0];
        FormCDripCUE.mciSendString("open " + this.scsi + " type CDAudio alias drive" + this.scsi.Substring(0, 1), (StringBuilder) null, 0, IntPtr.Zero);
        FormCDripCUE.mciSendString("set drive" + this.scsi.Substring(0, 1) + " door open", (StringBuilder) null, 0, IntPtr.Zero);
      }
      this.SetText("Completed.");
      this.btnCancel.Enabled = false;
      this.btnBegin.Enabled = true;
      this.btnClose.Enabled = true;
    }

    private void m_oWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      this.dualProgressBar1.MasterMaximum = 100;
      this.dualProgressBar1.MasterValue = e.ProgressPercentage;
      this.dualProgressBar1.Text = e.ProgressPercentage.ToString() + "%";
      this.dualProgressBar1.Maximum = 100;
      if (e.ProgressPercentage == 100)
        return;
      this.dualProgressBar1.Value = this.currentTrackProgress;
    }

    private void m_oWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      this.RipAllTracks((int) e.Argument, this.txtOutputPath.Text);
    }

    private void RipAllTracks(int nextTrackNumber, string outputFolder)
    {
      string path = outputFolder + "\\cdimage.wav";
      if (File.Exists(path))
      {
        try
        {
          File.Delete(path);
        }
        catch (Exception ex)
        {
          Audible.diskLogger(ex.ToString());
        }
      }
      int trackNum = 1;
      string str1 = "";
      if (this.paranoia)
        str1 = "-P";
      string str2 = "";
      if (this.rdBurst.Checked)
        str2 = "-B";
      else if (this.rdSecure.Checked)
        str2 = "-S";
      this.ripProcess = new Process()
      {
        StartInfo = new ProcessStartInfo()
        {
          FileName = this.supportLibs.cueToolsRipper,
          UseShellExecute = false,
          RedirectStandardOutput = true,
          RedirectStandardError = true,
          StandardOutputEncoding = Encoding.GetEncoding(850),
          StandardErrorEncoding = Encoding.GetEncoding(850),
          WorkingDirectory = outputFolder,
          Arguments = "-X -N -D " + this.scsi + ": " + str2 + " " + str1,
          CreateNoWindow = true
        }
      };
      this.ripProcess.OutputDataReceived += (DataReceivedEventHandler) ((sender, args) => this.Display(args.Data, ref trackNum, ref nextTrackNumber, outputFolder));
      this.ripProcess.ErrorDataReceived += (DataReceivedEventHandler) ((sender, args) => this.Display(args.Data, ref trackNum, ref nextTrackNumber, outputFolder));
      this.dgvTracks.Rows[trackNum - 1].Selected = true;
      this.ripProcess.Start();
      this.ripProcess.BeginOutputReadLine();
      this.ripProcess.BeginErrorReadLine();
      this.ripProcess.WaitForExit();
    }

    private void SafeRename(string source, string target)
    {
      for (int index = 0; index < 10; ++index)
      {
        try
        {
          File.Delete(target);
          File.Move(source, target);
          if (File.Exists(target))
            break;
          Thread.Sleep(200);
        }
        catch
        {
        }
      }
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

    private AdvancedSplitting.Chapters ParseCueFile(string cueFile)
    {
      AdvancedSplitting.Chapters chapters1 = new AdvancedSplitting.Chapters();
      List<double> chapters2 = new List<double>();
      List<string> stringList = new List<string>();
      string[] strArray1 = File.ReadAllText(cueFile).Split('\n');
      try
      {
        for (int index = 0; index < strArray1.Length; ++index)
        {
          if (strArray1[index].Trim().StartsWith("INDEX"))
          {
            string[] strArray2 = strArray1[index].Trim().Split(' ')[2].Split(':');
            double num = double.Parse(strArray2[2]) * 0.01 + double.Parse(strArray2[1]) + double.Parse(strArray2[0]) * 60.0;
            chapters2.Add(num);
          }
          if (strArray1[index].Trim().StartsWith("TITLE"))
          {
            string[] strArray2 = strArray1[index].Trim().Split('"');
            stringList.Add(strArray2[1]);
          }
        }
        chapters1.SetDoubleChapters(chapters2);
        chapters1.generatedDescriptions = true;
      }
      catch
      {
      }
      return chapters1;
    }

    private void SplitWavByChapter(AdvancedSplitting.Chapters myChapters, string sourceWavName, int startingTrack)
    {
      string directoryName = Path.GetDirectoryName(sourceWavName);
      string str1 = "track";
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.supportLibs.ffmpegPath;
      process.StartInfo.WorkingDirectory = directoryName;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      string str2 = "";
      string extension = Path.GetExtension(sourceWavName);
      for (int pos = 0; pos < myChapters.GetDoubleList().Count; ++pos)
      {
        if (myChapters.GetDoubleList().Count != pos + 1)
          str2 = str2 + " -c copy -t " + (myChapters.GetChapterDouble(pos + 1) - myChapters.GetChapterDouble(pos)).ToString("0.000", (IFormatProvider) CultureInfo.InvariantCulture) + " -ss " + myChapters.GetChapterDouble(pos).ToString("0.000", (IFormatProvider) CultureInfo.InvariantCulture) + " \"" + str1 + " - " + (pos + startingTrack).ToString("D3") + extension + "\"";
        else
          str2 = str2 + " -c copy  -ss " + myChapters.GetChapterDouble(pos).ToString("0.000", (IFormatProvider) CultureInfo.InvariantCulture) + " \"" + str1 + " - " + (pos + startingTrack).ToString("D3") + extension + "\"";
      }
      process.StartInfo.Arguments = string.Format("-y -i \"{0}\" {1}", (object) sourceWavName, (object) str2);
      if (process.StartInfo.Arguments.Length > 8000)
      {
        this.SetText("Too many tracks to split.");
      }
      else
      {
        process.StartInfo.UseShellExecute = false;
        process.Start();
        process.WaitForExit();
        int exitCode = process.ExitCode;
      }
    }

    private void SplitWavByChapterSox(AdvancedSplitting.Chapters myChapters, string sourceWavName, int startingTrack)
    {
      string directoryName = Path.GetDirectoryName(sourceWavName);
      string str1 = "track";
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.supportLibs.soxPath;
      process.StartInfo.WorkingDirectory = directoryName;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      string extension = Path.GetExtension(sourceWavName);
      for (int pos = 0; pos < myChapters.GetDoubleList().Count; ++pos)
      {
        string str2 = myChapters.GetDoubleList().Count == pos + 1 ? myChapters.GetChapterDouble(pos).ToString("0.000", (IFormatProvider) CultureInfo.InvariantCulture) : myChapters.GetChapterDouble(pos).ToString("0.000", (IFormatProvider) CultureInfo.InvariantCulture) + " " + (myChapters.GetChapterDouble(pos + 1) - myChapters.GetChapterDouble(pos)).ToString("0.000", (IFormatProvider) CultureInfo.InvariantCulture);
        process.StartInfo.Arguments = string.Format("\"{0}\" {1} trim {2}", (object) sourceWavName, (object) ("\"" + str1 + " - " + (pos + startingTrack).ToString("D3") + extension + "\""), (object) str2);
        process.StartInfo.UseShellExecute = false;
        process.Start();
        process.WaitForExit();
        try
        {
          this.currentTrackProgress = 0;
          this.m_oWorker.ReportProgress((int) ((double) pos / (double) myChapters.GetDoubleList().Count * 100.0));
        }
        catch
        {
        }
        int exitCode = process.ExitCode;
      }
    }

    private void Display(string output, ref int trackNum, ref int nextTrackNumber, string outputFolder)
    {
      if (output == null)
        return;
      if (this.chkDebug.Checked)
        this.SetText(output);
      if (output.Contains("Disk length"))
      {
        try
        {
          int num = output.IndexOf(':');
          this.SetText("Source CD is " + output.Substring(num + 1, output.Length - num - 1).Trim() + " minutes.");
        }
        catch
        {
        }
      }
      if (output.Contains("Filename"))
      {
        try
        {
          this.imageFileName = output.Split(':')[1].Trim();
          this.imageFileName = Path.GetFileNameWithoutExtension(this.imageFileName);
        }
        catch
        {
          this.imageFileName = "CDImage";
        }
        if (this.chkDebug.Checked)
          this.SetText("Ripping to " + this.imageFileName);
      }
      if (this.chkAccurateRip.Checked)
      {
        if (output.Contains("AccurateRip : ok"))
          this.SetText("BLUE-Found CD in AccurateRip DB.");
        else if (output.Contains("AccurateRip :"))
          this.SetText("RED-Could not find CD in AccurateRip DB.");
      }
      if (output.Contains("Error: gap detection failed"))
      {
        this.SetText("RED-Your CD drive is unsupported.  Please go to Advanced settings and change from CUE Tools to CDDA2WAV.");
      }
      else
      {
        if (output.Contains("Results"))
        {
          int num = 0;
          try
          {
            try
            {
              string[] strArray = output.Split(';');
              num = int.Parse(strArray[1].Trim().Split(' ')[0]);
              string str1 = strArray[0].Split(':')[1].Trim();
              string str2 = strArray[2].Trim();
              this.SetText((trackNum - 1).ToString() + " tracks ripped @ " + str1 + " in " + str2);
              if (num > 0)
              {
                this.SetText("RED-There were " + (object) num + " read errors detected.");
                this.MarkBadTracks(outputFolder + "\\" + this.imageFileName + ".log");
              }
              else
                this.SetText("GREEN-No read errors detected.");
            }
            catch (Exception ex)
            {
              Audible.diskLogger("couldn't parse results: " + ex.ToString());
            }
            if (num > 0 && this.DeleteBadRip(outputFolder + "\\" + this.imageFileName))
              return;
            if (this.chkSingleWav.Checked)
            {
              nextTrackNumber = nextTrackNumber - this.trackSizes.Length;
              trackNum = trackNum - this.trackSizes.Length;
              string str = outputFolder + "\\track - " + nextTrackNumber.ToString("D3");
              this.sourceWavName = this.imageFileName;
              if (this.chkAccurateRip.Checked && !this.AccurateRip(outputFolder + "\\" + this.sourceWavName + ".log") && this.DeleteBadRip(outputFolder + "\\" + this.sourceWavName))
                return;
              this.SafeRename(outputFolder + "\\" + this.sourceWavName + ".wav", str + ".wav");
              this.SafeRename(outputFolder + "\\" + this.sourceWavName + ".log", str + ".log");
              this.SafeRename(outputFolder + "\\" + this.sourceWavName + ".cue", str + ".cue");
              ++nextTrackNumber;
              ++trackNum;
            }
            else
            {
              string sourceWavName = outputFolder + "\\" + this.imageFileName;
              AdvancedSplitting.Chapters cueFile = this.ParseCueFile(sourceWavName + ".cue");
              if (this.chkAccurateRip.Checked && !this.AccurateRip(sourceWavName + ".log") && this.DeleteBadRip(sourceWavName))
                return;
              this.SetText("Splitting tracks...");
              this.SplitWavByChapterSox(cueFile, sourceWavName + ".wav", nextTrackNumber - this.trackSizes.Length);
              File.Delete(sourceWavName + ".wav");
              File.Delete(sourceWavName + ".cue");
              File.Delete(sourceWavName + ".log");
            }
            this.m_oWorker.ReportProgress(100);
          }
          catch (Exception ex)
          {
            Audible.diskLogger("Blew up at 100% - " + output + " : " + ex.ToString());
          }
        }
        if (!output.Contains<char>('%'))
          return;
        int result = 0;
        try
        {
          long num1 = 0;
          if (File.Exists(outputFolder + "\\" + this.imageFileName + ".wav"))
            num1 = new FileInfo(outputFolder + "\\" + this.imageFileName + ".wav").Length;
          for (int index1 = trackNum - 1; index1 < this.trackSizes.Length; ++index1)
          {
            if (num1 > this.trackSizes[index1])
            {
              this.SetText("Track " + (object) trackNum + " (" + (object) nextTrackNumber + ") ripped");
              Form1.SetControlPropertyThreadSafe((Control) this.dgvTracks, "FirstDisplayedScrollingRowIndex", (object) this.dgvTracks.SelectedRows[0].Index);
              Audible.diskLogger(output);
              this.dgvTracks.Rows[trackNum - 1].Selected = false;
              this.dgvTracks.Rows[trackNum - 1].DefaultCellStyle.BackColor = Color.LightGray;
              if (this.dgvTracks.Rows.Count > trackNum)
                this.dgvTracks.Rows[trackNum].Selected = true;
              ++nextTrackNumber;
              ++trackNum;
            }
            int index2 = index1 - 1;
            if (index2 < 0)
              index2 = 0;
            if (num1 <= this.trackSizes[index1] && num1 >= this.trackSizes[index2])
            {
              long num2 = this.trackSizes[index1] - this.trackSizes[index2];
              this.currentTrackProgress = (int) ((double) (num1 - this.trackSizes[index2]) / (double) num2 * 100.0);
            }
            else if (index2 == 0)
              this.currentTrackProgress = (int) ((double) num1 / (double) this.trackSizes[0] * 100.0);
          }
          if (int.TryParse(output.Split('%')[0].Split(':')[1].Trim(), out result))
            this.m_oWorker.ReportProgress(result);
          try
          {
            string str = output.Split(';')[2].Trim();
            Form1.SetControlPropertyThreadSafe((Control) this.txtErrors, "Text", (object) str.Substring(0, str.IndexOf(')') + 1));
          }
          catch (Exception ex)
          {
            Audible.diskLogger("Blew up on error parse - " + output + " : " + ex.ToString());
          }
          try
          {
            Form1.SetControlPropertyThreadSafe((Control) this.txtSpeed, "Text", (object) output.Split(';')[1].Trim());
          }
          catch (Exception ex)
          {
            Audible.diskLogger("Blew up on speed parse - " + output + " : " + ex.ToString());
          }
          try
          {
            Form1.SetControlPropertyThreadSafe((Control) this.txtTimeLeft, "Text", (object) output.Split(';')[3].Trim());
          }
          catch (Exception ex)
          {
            Audible.diskLogger("Blew up on time left parse - " + output + " : " + ex.ToString());
          }
        }
        catch (Exception ex)
        {
          Audible.diskLogger("Blew up - " + output + " : " + ex.ToString());
        }
      }
    }

    private bool DeleteBadRip(string sourceWavName)
    {
      if (MessageBox.Show("There were errors in your rip.\r\nDo you want to delete the ripped file?", "Delete bad rip?", MessageBoxButtons.YesNo) != DialogResult.Yes)
        return false;
      this.SafeDelete(sourceWavName + ".wav");
      this.SafeDelete(sourceWavName + ".cue");
      this.SafeDelete(sourceWavName + ".log");
      this.SetText("RED-Deleted this rip.");
      return true;
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
        this.Invoke((Delegate) new FormCDripCUE.SetGridCallback(this.UpdateGrid), new object[0]);
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
      this.SetText("Aborting...");
      this.m_oWorker.CancelAsync();
      try
      {
        this.ripProcess.Kill();
        this.DeleteBadRip(this.txtOutputPath.Text + "\\" + this.imageFileName);
      }
      catch (Exception ex)
      {
        Audible.diskLogger("Error trying to kill process: " + ex.ToString());
      }
    }

    private void AppendColourText(RichTextBox box, Color color, string text)
    {
      int textLength1 = box.TextLength;
      box.AppendText(text);
      int textLength2 = box.TextLength;
      box.Select(textLength1, textLength2 - textLength1);
      box.SelectionColor = color;
      box.SelectionLength = 0;
    }

    private void Log(string text)
    {
      int num = 60000;
      string text1 = "[" + DateTime.Now.ToLongTimeString() + "] - " + text + "\r\n";
      if (this.rtbActivityLog.Text.Length + text1.Length > num)
        this.rtbActivityLog.Text = this.rtbActivityLog.Text.Substring(text1.Length, this.rtbActivityLog.Text.Length - text1.Length);
      Color color = Color.Black;
      if (text1.Contains("BLUE-"))
      {
        text1 = text1.Replace("BLUE-", "");
        color = Color.Blue;
      }
      if (text1.Contains("RED-"))
      {
        text1 = text1.Replace("RED-", "");
        color = Color.Red;
      }
      if (text1.Contains("GREEN-"))
      {
        text1 = text1.Replace("GREEN-", "");
        color = Color.DarkGreen;
      }
      this.AppendColourText(this.rtbActivityLog, color, text1);
      Audible.diskLogger(text1);
      this.rtbActivityLog.SelectionStart = this.rtbActivityLog.Text.Length;
      this.rtbActivityLog.ScrollToCaret();
    }

    [DllImport("winmm.dll")]
    private static extern int mciSendString(string command, StringBuilder buffer, int bufferSize, IntPtr hwndCallback);

    private void SetText(string text)
    {
      if (this.rtbActivityLog.InvokeRequired)
        this.Invoke((Delegate) new FormCDripCUE.SetTextCallback(this.SetText), (object) text);
      else
        this.Log(text);
    }

    private void MarkBadTracks(string logFile)
    {
      for (int index = 0; index < 10; ++index)
      {
        try
        {
          if (!File.Exists(logFile))
            Thread.Sleep(200);
        }
        catch
        {
        }
      }
      if (!File.Exists(logFile))
      {
        this.SetText("Error, can't find " + logFile);
      }
      else
      {
        string[] strArray = File.ReadAllText(logFile).Split('\n');
        for (int index = 0; index < strArray.Length; ++index)
        {
          if (strArray[index].Contains("bad_track_count"))
          {
            string str1 = "";
            string str2 = strArray[index].Split(':')[1].Trim();
            char[] chArray = new char[1]{ ',' };
            foreach (string s in str2.Split(chArray))
            {
              int num = int.Parse(s);
              this.dgvTracks.Rows[num - 1].DefaultCellStyle.BackColor = Color.Red;
              str1 = str1 + num.ToString() + ", ";
            }
            this.SetText("RED-Track(s) " + str1.Substring(0, str1.Length - 2) + " had errors.");
            break;
          }
        }
      }
    }

    private bool AccurateRip(string logFile)
    {
      bool flag = true;
      try
      {
        for (int index = 0; index < 10; ++index)
        {
          try
          {
            if (!File.Exists(logFile))
              Thread.Sleep(200);
            else
              break;
          }
          catch
          {
          }
        }
        string str1 = File.ReadAllText(logFile);
        string[] strArray1 = str1.Split('\n');
        int num1 = 0;
        int num2 = 0;
        for (int index = 0; index < strArray1.Length; ++index)
        {
          if (strArray1[index].Contains("[AccurateRip ID"))
          {
            num1 = index + 2;
            if (!strArray1[index].Contains("found."))
            {
              this.SetText("Could not find disc in AccurateRip DB");
              return true;
            }
          }
          if (strArray1[index].Contains("Track Peak "))
            num2 = index - 2;
        }
        if (num1 == 0)
        {
          this.SetText("BLUE-Could not find disc in AccurateRip DB");
          return true;
        }
        bool[] flagArray = new bool[this.trackSizes.Length];
        for (int index = num1; index <= num2; ++index)
        {
          string[] strArray2 = strArray1[index].Trim().Split(' ');
          int result = 0;
          if (int.TryParse(strArray2[0], out result) && !flagArray[result - 1])
            flagArray[result - 1] = strArray1[index].Contains("Accurately ripped");
        }
        if (str1.Contains("Errors detected"))
        {
          flag = false;
          string str2 = "";
          this.SetText("RED-AccurateRip detected errors!");
          for (int index = 0; index < flagArray.Length; ++index)
          {
            if (!flagArray[index])
            {
              this.dgvTracks.Rows[index].DefaultCellStyle.BackColor = Color.Red;
              str2 = str2 + (index + 1).ToString() + ", ";
            }
            else
              this.dgvTracks.Rows[index].DefaultCellStyle.BackColor = Color.LightGreen;
          }
          this.SetText("RED-Track(s) " + str2.Substring(0, str2.Length - 2) + " had errors.");
          if (this.rdBurst.Checked)
            this.SetText("You may want to try again with Secure or Paranoid mode.");
          else if (this.rdSecure.Checked)
            this.SetText("You may want to try again with Paranoid mode.");
          else if (this.rdParanoid.Checked)
            this.SetText("This is the best that I could do. Errors may not be fixable.");
        }
        else
        {
          for (int index = 0; index < this.dgvTracks.Rows.Count; ++index)
            this.dgvTracks.Rows[index].DefaultCellStyle.BackColor = Color.LightGreen;
          this.SetText("GREEN-AccurateRip reports all tracks ripped perfectly.");
          flag = true;
        }
      }
      catch (Exception ex)
      {
        Audible.diskLogger("error reading log file " + ex.ToString());
      }
      return flag;
    }

    private delegate void SetGridCallback();

    private delegate void SetTextCallback(string text);
  }
}
