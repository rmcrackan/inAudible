// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.FormLHR
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using Amib.Threading;
using CUETools.AccurateRip;
using CUETools.CDImage;
using CUETools.Codecs;
using CUETools.Codecs.FLAKE;
using CUETools.CTDB;
using CUETools.Ripper;
using CUETools.Ripper.SCSI;
using IniParser;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AudibleConvertor
{
  public class FormLHR : Form
  {
    private BindingList<DriveList> driveList = new BindingList<DriveList>();
    public string iniPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "inAudible", "config.ini");
    private IContainer components;
    private DataGridView dataGridView1;
    private RichTextBox rtbLog;
    private Button btnOutputDir;
    private Button btnRip;
    private SplitContainer splitContainer1;
    private Button btnCancel;
    private GroupBox groupBox1;
    private CheckBox chkAccurateRip;
    private RadioButton rdParanoid;
    private RadioButton rdSecure;
    private RadioButton rdBurst;
    private CheckBox chkSingleWav;
    private CheckBox chkEject;
    private CheckBox chkRipOnInsert;
    private CheckBox chkAutoQuality;
    private DataGridViewTextBoxColumn drive;
    private DataGridViewTextBoxColumn model;
    private DataGridViewTextBoxColumn discNum;
    private DataGridViewTextBoxColumn diskNumber;
    private DataGridViewTextBoxColumn tracks;
    private DataGridViewTextBoxColumn accurateRip;
    private DataGridViewTextBoxColumn progress;
    private DataGridViewTextBoxColumn mode;
    private DataGridViewTextBoxColumn info;
    private DataGridViewTextBoxColumn errors;
    private CheckBox chkDebug;
    private Button btnApply;
    public TextBox txtTargetDir;
    private GroupBox grpFormat;
    private RadioButton rdFLAC;
    public RadioButton rdWAV;
    public string cdWavPath;
    private SmartThreadPool smartThreadPool;
    private IWorkItemResult<int>[] wir;
    private bool ripping;
    public SupportLibraries supportLibs;
    public bool apply;
    private ManagementEventWatcher watcher;
    private BackgroundWorker m_oWorker;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle3 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle4 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle5 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle6 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle7 = new DataGridViewCellStyle();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormLHR));
      this.dataGridView1 = new DataGridView();
      this.drive = new DataGridViewTextBoxColumn();
      this.model = new DataGridViewTextBoxColumn();
      this.discNum = new DataGridViewTextBoxColumn();
      this.diskNumber = new DataGridViewTextBoxColumn();
      this.tracks = new DataGridViewTextBoxColumn();
      this.accurateRip = new DataGridViewTextBoxColumn();
      this.progress = new DataGridViewTextBoxColumn();
      this.mode = new DataGridViewTextBoxColumn();
      this.info = new DataGridViewTextBoxColumn();
      this.errors = new DataGridViewTextBoxColumn();
      this.rtbLog = new RichTextBox();
      this.btnOutputDir = new Button();
      this.txtTargetDir = new TextBox();
      this.btnRip = new Button();
      this.splitContainer1 = new SplitContainer();
      this.grpFormat = new GroupBox();
      this.rdFLAC = new RadioButton();
      this.rdWAV = new RadioButton();
      this.groupBox1 = new GroupBox();
      this.chkDebug = new CheckBox();
      this.chkAutoQuality = new CheckBox();
      this.chkRipOnInsert = new CheckBox();
      this.chkAccurateRip = new CheckBox();
      this.rdParanoid = new RadioButton();
      this.rdSecure = new RadioButton();
      this.rdBurst = new RadioButton();
      this.chkSingleWav = new CheckBox();
      this.chkEject = new CheckBox();
      this.btnCancel = new Button();
      this.btnApply = new Button();
      ((ISupportInitialize) this.dataGridView1).BeginInit();
      this.splitContainer1.BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.grpFormat.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.AllowUserToDeleteRows = false;
      this.dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Columns.AddRange((DataGridViewColumn) this.drive, (DataGridViewColumn) this.model, (DataGridViewColumn) this.discNum, (DataGridViewColumn) this.diskNumber, (DataGridViewColumn) this.tracks, (DataGridViewColumn) this.accurateRip, (DataGridViewColumn) this.progress, (DataGridViewColumn) this.mode, (DataGridViewColumn) this.info, (DataGridViewColumn) this.errors);
      this.dataGridView1.Location = new Point(3, 3);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.Size = new Size(851, 136);
      this.dataGridView1.TabIndex = 0;
      this.drive.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.drive.DataPropertyName = "drive";
      this.drive.HeaderText = "Drive";
      this.drive.Name = "drive";
      this.drive.Width = 57;
      this.model.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.model.DataPropertyName = "model";
      this.model.HeaderText = "Model";
      this.model.Name = "model";
      this.model.Width = 61;
      this.discNum.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.discNum.DataPropertyName = "discNum";
      gridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
      this.discNum.DefaultCellStyle = gridViewCellStyle1;
      this.discNum.HeaderText = "Disc #";
      this.discNum.Name = "discNum";
      this.discNum.Width = 63;
      this.diskNumber.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.diskNumber.DataPropertyName = "discTime";
      gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
      this.diskNumber.DefaultCellStyle = gridViewCellStyle2;
      this.diskNumber.HeaderText = "Time";
      this.diskNumber.Name = "diskNumber";
      this.diskNumber.Width = 55;
      this.tracks.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.tracks.DataPropertyName = "tracks";
      gridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
      this.tracks.DefaultCellStyle = gridViewCellStyle3;
      this.tracks.HeaderText = "Tracks";
      this.tracks.Name = "tracks";
      this.tracks.Width = 65;
      this.accurateRip.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.accurateRip.DataPropertyName = "accurateRip";
      gridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
      this.accurateRip.DefaultCellStyle = gridViewCellStyle4;
      this.accurateRip.HeaderText = "AccurateRip";
      this.accurateRip.Name = "accurateRip";
      this.accurateRip.Width = 91;
      this.progress.DataPropertyName = "progress";
      this.progress.HeaderText = "Progress";
      this.progress.Name = "progress";
      this.progress.Visible = false;
      this.progress.Width = 73;
      this.mode.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.mode.DataPropertyName = "modeDescription";
      gridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
      this.mode.DefaultCellStyle = gridViewCellStyle5;
      this.mode.HeaderText = "Mode";
      this.mode.Name = "mode";
      this.mode.Width = 59;
      this.info.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.info.DataPropertyName = "info";
      gridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
      this.info.DefaultCellStyle = gridViewCellStyle6;
      this.info.HeaderText = "Info";
      this.info.Name = "info";
      this.info.Width = 50;
      this.errors.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.errors.DataPropertyName = "errors";
      gridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
      this.errors.DefaultCellStyle = gridViewCellStyle7;
      this.errors.HeaderText = "Errors";
      this.errors.Name = "errors";
      this.errors.Width = 59;
      this.rtbLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.rtbLog.Location = new Point(3, 3);
      this.rtbLog.Name = "rtbLog";
      this.rtbLog.ScrollBars = RichTextBoxScrollBars.Vertical;
      this.rtbLog.Size = new Size(588, 178);
      this.rtbLog.TabIndex = 1;
      this.rtbLog.Text = "";
      this.btnOutputDir.Location = new Point(12, 10);
      this.btnOutputDir.Name = "btnOutputDir";
      this.btnOutputDir.Size = new Size(75, 23);
      this.btnOutputDir.TabIndex = 2;
      this.btnOutputDir.Text = "Output Dir";
      this.btnOutputDir.UseVisualStyleBackColor = true;
      this.btnOutputDir.Click += new EventHandler(this.button1_Click);
      this.txtTargetDir.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txtTargetDir.Location = new Point(94, 12);
      this.txtTargetDir.Name = "txtTargetDir";
      this.txtTargetDir.Size = new Size(775, 20);
      this.txtTargetDir.TabIndex = 3;
      this.btnRip.Anchor = AnchorStyles.Bottom;
      this.btnRip.Location = new Point(403, 375);
      this.btnRip.Name = "btnRip";
      this.btnRip.Size = new Size(75, 23);
      this.btnRip.TabIndex = 4;
      this.btnRip.Text = "Rip";
      this.btnRip.UseVisualStyleBackColor = true;
      this.btnRip.Click += new EventHandler(this.btnRip_Click);
      this.splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.splitContainer1.Location = new Point(12, 39);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = Orientation.Horizontal;
      this.splitContainer1.Panel1.Controls.Add((Control) this.dataGridView1);
      this.splitContainer1.Panel2.Controls.Add((Control) this.grpFormat);
      this.splitContainer1.Panel2.Controls.Add((Control) this.groupBox1);
      this.splitContainer1.Panel2.Controls.Add((Control) this.rtbLog);
      this.splitContainer1.Size = new Size(857, 330);
      this.splitContainer1.SplitterDistance = 142;
      this.splitContainer1.TabIndex = 5;
      this.grpFormat.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.grpFormat.Controls.Add((Control) this.rdFLAC);
      this.grpFormat.Controls.Add((Control) this.rdWAV);
      this.grpFormat.Location = new Point(605, 144);
      this.grpFormat.Name = "grpFormat";
      this.grpFormat.Size = new Size(244, 36);
      this.grpFormat.TabIndex = 28;
      this.grpFormat.TabStop = false;
      this.grpFormat.Text = "Format";
      this.rdFLAC.AutoSize = true;
      this.rdFLAC.Location = new Point(187, 13);
      this.rdFLAC.Name = "rdFLAC";
      this.rdFLAC.Size = new Size(51, 17);
      this.rdFLAC.TabIndex = 1;
      this.rdFLAC.Text = "FLAC";
      this.rdFLAC.UseVisualStyleBackColor = true;
      this.rdWAV.AutoSize = true;
      this.rdWAV.Checked = true;
      this.rdWAV.Location = new Point(6, 13);
      this.rdWAV.Name = "rdWAV";
      this.rdWAV.Size = new Size(50, 17);
      this.rdWAV.TabIndex = 0;
      this.rdWAV.TabStop = true;
      this.rdWAV.Text = "WAV";
      this.rdWAV.UseVisualStyleBackColor = true;
      this.groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.groupBox1.Controls.Add((Control) this.chkDebug);
      this.groupBox1.Controls.Add((Control) this.chkAutoQuality);
      this.groupBox1.Controls.Add((Control) this.chkRipOnInsert);
      this.groupBox1.Controls.Add((Control) this.chkAccurateRip);
      this.groupBox1.Controls.Add((Control) this.rdParanoid);
      this.groupBox1.Controls.Add((Control) this.rdSecure);
      this.groupBox1.Controls.Add((Control) this.rdBurst);
      this.groupBox1.Controls.Add((Control) this.chkSingleWav);
      this.groupBox1.Controls.Add((Control) this.chkEject);
      this.groupBox1.Location = new Point(605, 3);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(249, 135);
      this.groupBox1.TabIndex = 27;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Rip Options";
      this.chkDebug.AccessibleDescription = "Create a one WAV file that contains all tracks for this CD";
      this.chkDebug.AccessibleName = "Single WAV file";
      this.chkDebug.AutoSize = true;
      this.chkDebug.Location = new Point(125, 88);
      this.chkDebug.Name = "chkDebug";
      this.chkDebug.Size = new Size(58, 17);
      this.chkDebug.TabIndex = 29;
      this.chkDebug.Text = "Debug";
      this.chkDebug.UseVisualStyleBackColor = true;
      this.chkAutoQuality.AccessibleDescription = "Verify rip against internet DB";
      this.chkAutoQuality.AccessibleName = "";
      this.chkAutoQuality.AutoSize = true;
      this.chkAutoQuality.Checked = true;
      this.chkAutoQuality.CheckState = CheckState.Checked;
      this.chkAutoQuality.Location = new Point(6, 111);
      this.chkAutoQuality.Name = "chkAutoQuality";
      this.chkAutoQuality.Size = new Size(238, 17);
      this.chkAutoQuality.TabIndex = 28;
      this.chkAutoQuality.Text = "Fallback to Paranoid on unrecoverable errors";
      this.chkAutoQuality.UseVisualStyleBackColor = true;
      this.chkRipOnInsert.AccessibleDescription = "Begin ripping as soon as disc is inserted";
      this.chkRipOnInsert.AccessibleName = "Rip on insert";
      this.chkRipOnInsert.AutoSize = true;
      this.chkRipOnInsert.Checked = true;
      this.chkRipOnInsert.CheckState = CheckState.Checked;
      this.chkRipOnInsert.Location = new Point(125, 19);
      this.chkRipOnInsert.Name = "chkRipOnInsert";
      this.chkRipOnInsert.Size = new Size(85, 17);
      this.chkRipOnInsert.TabIndex = 27;
      this.chkRipOnInsert.Text = "Rip on insert";
      this.chkRipOnInsert.UseVisualStyleBackColor = true;
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
      this.rdParanoid.AccessibleDescription = "Paranoid mode - read as many times as necessary";
      this.rdParanoid.AutoSize = true;
      this.rdParanoid.Location = new Point(6, 65);
      this.rdParanoid.Name = "rdParanoid";
      this.rdParanoid.Size = new Size(111, 17);
      this.rdParanoid.TabIndex = 25;
      this.rdParanoid.Text = "Paranoid (n reads)";
      this.rdParanoid.UseVisualStyleBackColor = true;
      this.rdSecure.AccessibleDescription = "Secure mode - read each sector twice";
      this.rdSecure.AutoSize = true;
      this.rdSecure.Location = new Point(6, 42);
      this.rdSecure.Name = "rdSecure";
      this.rdSecure.Size = new Size(103, 17);
      this.rdSecure.TabIndex = 24;
      this.rdSecure.Text = "Secure (2 reads)";
      this.rdSecure.UseVisualStyleBackColor = true;
      this.rdBurst.AutoSize = true;
      this.rdBurst.Checked = true;
      this.rdBurst.Location = new Point(6, 19);
      this.rdBurst.Name = "rdBurst";
      this.rdBurst.Size = new Size(88, 17);
      this.rdBurst.TabIndex = 23;
      this.rdBurst.TabStop = true;
      this.rdBurst.Text = "Burst (1 read)";
      this.rdBurst.UseVisualStyleBackColor = true;
      this.chkSingleWav.AccessibleDescription = "Create a one WAV file that contains all tracks for this CD";
      this.chkSingleWav.AccessibleName = "Single WAV file";
      this.chkSingleWav.AutoSize = true;
      this.chkSingleWav.Location = new Point(125, 65);
      this.chkSingleWav.Name = "chkSingleWav";
      this.chkSingleWav.Size = new Size(107, 17);
      this.chkSingleWav.TabIndex = 16;
      this.chkSingleWav.Text = "Single file per CD";
      this.chkSingleWav.UseVisualStyleBackColor = true;
      this.chkEject.AccessibleDescription = "Eject disc after rip is complete";
      this.chkEject.AccessibleName = "Eject on completion";
      this.chkEject.AutoSize = true;
      this.chkEject.Checked = true;
      this.chkEject.CheckState = CheckState.Checked;
      this.chkEject.Location = new Point(125, 43);
      this.chkEject.Name = "chkEject";
      this.chkEject.Size = new Size(119, 17);
      this.chkEject.TabIndex = 12;
      this.chkEject.Text = "Eject on completion";
      this.chkEject.UseVisualStyleBackColor = true;
      this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnCancel.Enabled = false;
      this.btnCancel.Location = new Point(794, 375);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 6;
      this.btnCancel.Text = "Exit rip mode";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.btnApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.btnApply.Location = new Point(12, 375);
      this.btnApply.Name = "btnApply";
      this.btnApply.Size = new Size(168, 23);
      this.btnApply.TabIndex = 7;
      this.btnApply.Text = "Load this session into inAudible";
      this.btnApply.UseVisualStyleBackColor = true;
      this.btnApply.Click += new EventHandler(this.btnApply_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(881, 410);
      this.Controls.Add((Control) this.btnApply);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.splitContainer1);
      this.Controls.Add((Control) this.btnRip);
      this.Controls.Add((Control) this.txtTargetDir);
      this.Controls.Add((Control) this.btnOutputDir);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (FormLHR);
      this.Text = "Lord High Ripper";
      this.FormClosing += new FormClosingEventHandler(this.FormLHR_FormClosing);
      ((ISupportInitialize) this.dataGridView1).EndInit();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.grpFormat.ResumeLayout(false);
      this.grpFormat.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public FormLHR()
    {
      this.InitializeComponent();
      this.networkDevice();
    }

    public void init()
    {
      this.driveList.Clear();
      foreach (char ch in CDDrivesList.DrivesAvailable())
        this.driveList.Add(new DriveList(ch.ToString(), this.GetNextTrackNumber()));
      this.dataGridView1.AutoGenerateColumns = false;
      this.dataGridView1.DataSource = (object) this.driveList;
      DataGridViewProgressColumn viewProgressColumn = new DataGridViewProgressColumn();
      this.dataGridView1.Columns.Add((DataGridViewColumn) viewProgressColumn);
      viewProgressColumn.HeaderText = "Progress";
      viewProgressColumn.DataPropertyName = "progressBar";
      viewProgressColumn.Width = 100;
      viewProgressColumn.DisplayIndex = 7;
      this.dataGridView1.Columns[7].DisplayIndex = 10;
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

    private bool DeleteBadRip(string sourceWavName, DriveList dl)
    {
      if (MessageBox.Show("There were errors ripping disc " + dl.discNum + " in drive " + dl.drive + ".\r\nDo you want to delete the ripped file?", "Delete bad rip?", MessageBoxButtons.YesNo) != DialogResult.Yes)
        return false;
      this.SafeDelete(sourceWavName);
      this.SafeDelete(Path.ChangeExtension(sourceWavName, ".cue"));
      Directory.Delete(Path.GetDirectoryName(sourceWavName), true);
      this.SetText("RED-Deleted this rip.");
      return true;
    }

    private int GetDriveIdByLetter(string driveLetter)
    {
      int num = 0;
      for (int index = 0; index < this.driveList.Count; ++index)
      {
        if (this.driveList[index].drive == driveLetter.Substring(0, 1))
          num = index;
      }
      return num;
    }

    private void watcher_EventArrived(object sender, EventArrivedEventArgs e)
    {
      ManagementBaseObject managementBaseObject = (ManagementBaseObject) e.NewEvent["TargetInstance"];
      string str1 = (string) managementBaseObject["DeviceID"];
      Console.WriteLine(str1);
      Console.WriteLine(managementBaseObject.Properties["VolumeName"].Value);
      Console.WriteLine((string) managementBaseObject["Name"]);
      if (managementBaseObject.Properties["VolumeName"].Value != null)
      {
        this.SetText("BLUE-CD inserted into " + str1);
        this.driveList[this.GetDriveIdByLetter(str1)].progressBar = 0;
        this.UpdateDrive(str1);
        this.UpdateGrid();
        if (!this.ripping || !this.chkRipOnInsert.Checked)
          return;
        string str2 = str1.Substring(0, 1);
        int index1 = -1;
        for (int index2 = 0; index2 < this.driveList.Count; ++index2)
        {
          if (this.driveList[index2].drive == str2)
            index1 = index2;
        }
        if (index1 <= -1)
          return;
        if (this.smartThreadPool.IsIdle)
          this.smartThreadPool = new SmartThreadPool(10000, this.driveList.Count);
        this.driveList[index1].mode = this.GetQuality();
        this.wir[index1] = this.smartThreadPool.QueueWorkItem<DriveList, int, int>(new Amib.Threading.Func<DriveList, int, int>(this.DoRip), this.driveList[index1], this.GetQuality());
        this.UpdateGrid();
        if (!this.smartThreadPool.IsIdle || this.m_oWorker.IsBusy)
          return;
        this.m_oWorker.RunWorkerAsync();
      }
      else
      {
        this.SetText("BLUE-CD ejected from " + str1);
        this.UpdateDrive(str1);
        this.UpdateGrid();
      }
    }

    private int GetNextTrackNumber()
    {
      int num = 0;
      for (int index = 0; index < this.driveList.Count; ++index)
      {
        int result = 0;
        int.TryParse(this.driveList[index].discNum, out result);
        if (result > num)
          num = result;
      }
      return num + 1;
    }

    private void DebugLog(string text)
    {
      if (!this.chkDebug.Checked)
        return;
      this.SetText(text);
    }

    private void UpdateDrive(string driveName)
    {
      string driveLetter = driveName.Substring(0, 1);
      bool flag = false;
      for (int index = 0; index < this.driveList.Count; ++index)
      {
        if (this.driveList[index].discNum != "")
          flag = true;
      }
      if (this.driveList[this.GetDriveIdByLetter(driveName)].progressBar == 100)
      {
        this.DebugLog("Not updating disc # because progress = 100");
      }
      else
      {
        if (this.txtTargetDir.Text != "" && !flag)
        {
          int num = 0;
          string text = this.txtTargetDir.Text;
          if (text != "")
          {
            string[] directories = Directory.GetDirectories(text);
            Array.Sort<string>(directories);
            foreach (string str in directories)
            {
              try
              {
                string[] strArray = str.Split('-');
                num = int.Parse(strArray[strArray.Length - 1].Trim());
              }
              catch
              {
              }
            }
          }
          if (num > 0)
          {
            this.DebugLog("Updating disc # based on directories");
            for (int index = 0; index < this.driveList.Count; ++index)
            {
              if (this.driveList[index].drive == driveLetter)
              {
                this.driveList[index].Update(driveLetter, num + 1);
                return;
              }
            }
          }
        }
        for (int index = 0; index < this.driveList.Count; ++index)
        {
          if (this.driveList[index].drive == driveLetter)
          {
            this.DebugLog("Updating disc " + driveLetter + " to use next track # based on grid");
            this.driveList[index].Update(driveLetter, this.GetNextTrackNumber());
          }
        }
      }
    }

    private void SetText(string text)
    {
      if (this.rtbLog.InvokeRequired)
        this.Invoke((Delegate) new FormLHR.SetTextCallback(this.SetText), (object) text);
      else
        this.Log(text);
    }

    private void Log(string text)
    {
      int num = 60000;
      string text1 = "[" + DateTime.Now.ToLongTimeString() + "] - " + text + "\r\n";
      if (this.rtbLog.Text.Length + text1.Length > num)
        this.rtbLog.Text = this.rtbLog.Text.Substring(text1.Length, this.rtbLog.Text.Length - text1.Length);
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
      this.AppendColourText(this.rtbLog, color, text1);
      Audible.diskLogger(text1);
      this.rtbLog.SelectionStart = this.rtbLog.Text.Length;
      this.rtbLog.ScrollToCaret();
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

    private void button1_Click(object sender, EventArgs e)
    {
      try
      {
        this.cdWavPath = new FileIniDataParser().LoadFile(this.iniPath)["system"]["wav_path"];
      }
      catch
      {
      }
      if (!CommonFileDialog.IsPlatformSupported)
        return;
      CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
      commonOpenFileDialog.IsFolderPicker = true;
      commonOpenFileDialog.Title = "Select source folder";
      if (this.txtTargetDir.Text.Trim() != "")
        commonOpenFileDialog.InitialDirectory = this.txtTargetDir.Text;
      else if (this.cdWavPath != "")
        commonOpenFileDialog.InitialDirectory = this.cdWavPath;
      if (commonOpenFileDialog.ShowDialog() != CommonFileDialogResult.Ok)
        return;
      this.txtTargetDir.Text = commonOpenFileDialog.FileName;
      this.UpdateDriveListWithDiscNumbers();
    }

    private void UpdateDriveListWithDiscNumbers()
    {
      int num = 0;
      string text = this.txtTargetDir.Text;
      if (text != "")
      {
        string[] directories = Directory.GetDirectories(text);
        Array.Sort<string>(directories);
        foreach (string str in directories)
        {
          try
          {
            string[] strArray = str.Split('-');
            num = int.Parse(strArray[strArray.Length - 1].Trim());
          }
          catch
          {
          }
        }
      }
      for (int index = 0; index < this.driveList.Count; ++index)
      {
        if (!this.driveList[index].empty)
        {
          int result = 0;
          int.TryParse(this.driveList[index].discNum, out result);
          this.driveList[index].discNum = (result + num).ToString();
        }
      }
      if (num <= 0)
        return;
      this.SetText("BLUE-Based on the folders in your selected directory, discs ripped in this session will start from \"" + (object) (num + 1) + "\".");
    }

    private void FormLHR_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.watcher.Stop();
    }

    private void UpdateGrid()
    {
      if (this.dataGridView1.InvokeRequired)
        this.Invoke((Delegate) new FormLHR.SetGridCallback(this.UpdateGrid), new object[0]);
      else
        this.dataGridView1.Refresh();
    }

    private void btnRip_Click(object sender, EventArgs e)
    {
      if (this.txtTargetDir.Text.Trim() == "" || !Directory.Exists(this.txtTargetDir.Text.Trim()))
      {
        int num = (int) MessageBox.Show("You need to specify a valid output directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
      }
      else
      {
        this.SetText("GREEN-Ripper active.");
        this.btnRip.Enabled = false;
        this.btnCancel.Enabled = true;
        this.m_oWorker = new BackgroundWorker();
        this.m_oWorker.DoWork += new DoWorkEventHandler(this.m_oWorker_DoWork);
        this.m_oWorker.ProgressChanged += new ProgressChangedEventHandler(this.m_oWorker_ProgressChanged);
        this.m_oWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.m_oWorker_RunWorkerCompleted);
        this.m_oWorker.WorkerReportsProgress = true;
        this.m_oWorker.WorkerSupportsCancellation = true;
        this.m_oWorker.RunWorkerAsync();
      }
    }

    private string GetRipPath(int discNum)
    {
      string path = this.txtTargetDir.Text + "\\" + ("Disc - " + discNum.ToString("D3"));
      if (!Directory.Exists(path))
      {
        try
        {
          Directory.CreateDirectory(path);
        }
        catch (Exception ex)
        {
          this.SetText("Error creating output dir: " + ex.Message);
        }
      }
      return path;
    }

    private void CDReadProgress(object sender, ReadProgressArgs e)
    {
      CDDriveReader audioSource = (CDDriveReader) sender;
      int num1 = e.Position - e.PassStart;
      TimeSpan timeSpan = DateTime.Now - e.PassTime;
      double speed = timeSpan.TotalSeconds > 0.0 ? (double) num1 / timeSpan.TotalSeconds / 75.0 : 1.0;
      double num2 = (double) (e.Position - e.PassStart) / (double) (e.PassEnd - e.PassStart);
      string str1 = e.Pass > 0 ? " (Retry)" : "";
      if (timeSpan.TotalSeconds <= 0.0 || e.Pass < 0)
        string.Format("{0}{1}...", (object) e.Action, (object) str1);
      else
        string.Format("{0} @{1:00.00}x{2}...", (object) e.Action, (object) speed, (object) str1);
      try
      {
        this.BeginInvoke((System.Action) (() =>
        {
          for (int index = 0; index < this.driveList.Count; ++index)
          {
            if (this.driveList[index].drive == audioSource.Path.Substring(0, 1))
            {
              double num3 = 100.0 * (double) e.Position / (double) audioSource.TOC.AudioLength;
              string str2 = string.Format("{0:00}%", (object) num3);
              if (this.driveList[index].progress != str2)
              {
                this.driveList[index].progress = str2;
                this.driveList[index].progressBar = (int) num3;
                this.driveList[index].info = string.Format("{0:00.00}x", (object) speed);
              }
              if (this.driveList[index].errors != e.ErrorsCount.ToString())
                this.driveList[index].errors = e.ErrorsCount.ToString();
            }
          }
        }));
      }
      catch
      {
      }
    }

    private int DoRip(DriveList dl, int quality)
    {
      int num = 0;
      if (dl.empty)
        return 0;
      this.SetText("Ripping disc " + dl.discNum + " in drive " + dl.drive + "...");
      string str = this.GetRipPath(dl.iDiscNum) + "\\cdimage.wav";
      CDDriveReader audioSource = new CDDriveReader();
      audioSource.Open(dl.drive.ToCharArray()[0]);
      int driveReadOffset = 0;
      AccurateRipVerify.FindDriveReadOffset(audioSource.ARName, out driveReadOffset);
      audioSource.DriveOffset = driveReadOffset;
      audioSource.CorrectionQuality = quality;
      audioSource.DebugMessages = false;
      audioSource.ForceD8 = false;
      audioSource.ForceBE = false;
      string detectReadCommand = audioSource.AutoDetectReadCommand;
      AccurateRipVerify ar = new AccurateRipVerify(audioSource.TOC, WebRequest.GetSystemWebProxy());
      AudioBuffer audioBuffer = new AudioBuffer((IAudioSource) audioSource, 65536);
      AccurateRipVerify.CalculateCDDBId(audioSource.TOC);
      string accurateRipId = AccurateRipVerify.CalculateAccurateRipId(audioSource.TOC);
      CUEToolsDB cueToolsDb = new CUEToolsDB(audioSource.TOC, (IWebProxy) null);
      cueToolsDb.Init(ar);
      cueToolsDb.ContactDB((string) null, "inAudible", audioSource.ARName, true, false, CTDBMetadataSearch.Fast);
      ar.ContactAccurateRip(accurateRipId);
      CTDBResponseMeta ctdbResponseMeta1 = (CTDBResponseMeta) null;
      using (IEnumerator<CTDBResponseMeta> enumerator = cueToolsDb.Metadata.GetEnumerator())
      {
        if (enumerator.MoveNext())
          ctdbResponseMeta1 = enumerator.Current;
      }
      if (this.chkDebug.Checked)
      {
        this.SetText("MusicBrainz entries (" + (object) cueToolsDb.Metadata.Count<CTDBResponseMeta>() + "):");
        foreach (CTDBResponseMeta ctdbResponseMeta2 in cueToolsDb.Metadata)
          this.SetText(ctdbResponseMeta2.artist + " - " + ctdbResponseMeta2.album);
      }
      IAudioDest audioDest;
      if (this.rdFLAC.Checked)
      {
        str = Path.ChangeExtension(str, ".flac");
        string path = str;
        FlakeWriterSettings flakeWriterSettings = new FlakeWriterSettings();
        flakeWriterSettings.PCM = audioBuffer.PCM;
        flakeWriterSettings.EncoderMode = "7";
        FlakeWriterSettings settings = flakeWriterSettings;
        audioDest = (IAudioDest) new FlakeWriter(path, settings);
      }
      else
        audioDest = (IAudioDest) new WAVWriter(str, (Stream) null, new WAVWriterSettings(audioSource.PCM));
      Audible.diskLogger(string.Format("Drive       : {0}", (object) audioSource.Path));
      Audible.diskLogger(string.Format("Read offset : {0}", (object) audioSource.DriveOffset));
      Audible.diskLogger(string.Format("Read cmd    : {0}", (object) audioSource.CurrentReadCommand));
      Audible.diskLogger(string.Format("Secure mode : {0}", (object) audioSource.CorrectionQuality));
      Audible.diskLogger(string.Format("Filename    : {0}", (object) str));
      Audible.diskLogger(string.Format("Disk length : {0}", (object) CDImageLayout.TimeToString(audioSource.TOC.AudioLength)));
      Audible.diskLogger(string.Format("AccurateRip : {0}", ar.ARStatus == null ? (object) "ok" : (object) ar.ARStatus));
      Audible.diskLogger(string.Format("MusicBrainz : {0}", ctdbResponseMeta1 == null ? (object) "not found" : (object) (ctdbResponseMeta1.artist + " - " + ctdbResponseMeta1.album)));
      ProgressMeter progressMeter = new ProgressMeter();
      audioSource.ReadProgress += new EventHandler<ReadProgressArgs>(this.CDReadProgress);
      audioDest.FinalSampleCount = audioSource.Length;
      dl.realStart = DateTime.Now;
      while (audioSource.Read(audioBuffer, -1) != 0)
      {
        ar.Write(audioBuffer);
        audioDest.Write(audioBuffer);
        if (!this.ripping)
        {
          this.SetText("RED-Stopping drive " + dl.drive);
          dl.info = "Cancelled";
          break;
        }
      }
      if (!this.ripping)
      {
        try
        {
          audioDest.Close();
        }
        catch
        {
        }
        try
        {
          audioSource.Close();
        }
        catch
        {
        }
        audioSource.ReadProgress -= new EventHandler<ReadProgressArgs>(this.CDReadProgress);
        return -1;
      }
      dl.progressBar = 100;
      TimeSpan timeSpan = DateTime.Now - dl.realStart;
      this.DebugLog(string.Format("Results : {0:0.00}x; {1:d5} errors; {2:d2}:{3:d2}:{4:d2}", (object) ((double) audioSource.Length / timeSpan.TotalSeconds / (double) audioSource.PCM.SampleRate), (object) audioSource.FailedSectors.PopulationCount(), (object) timeSpan.Hours, (object) timeSpan.Minutes, (object) timeSpan.Seconds));
      this.SetText(string.Format("GREEN-Disc {0} ripped @ {4:0.00}x in {1:d2}:{2:d2}:{3:d2}", (object) dl.discNum, (object) timeSpan.Hours, (object) timeSpan.Minutes, (object) timeSpan.Seconds, (object) ((double) audioSource.Length / timeSpan.TotalSeconds / (double) audioSource.PCM.SampleRate)));
      List<int> intList = new List<int>();
      for (int index = 1; (long) index <= (long) audioSource.TOC.AudioTracks; ++index)
      {
        for (uint start = audioSource.TOC[index].Start; start <= audioSource.TOC[index].End; ++start)
        {
          if (audioSource.FailedSectors[(int) start])
          {
            this.SetText(string.Format("RED-Track {0} contains errors", (object) index));
            intList.Add(index);
            break;
          }
        }
      }
      StringWriter stringWriter = new StringWriter();
      ar.GenerateFullLog((TextWriter) stringWriter, true, accurateRipId);
      stringWriter.Close();
      this.DebugLog(stringWriter.ToString());
      AdvancedSplitting.Chapters chapters = this.GetChapters(audioSource);
      audioDest.Close();
      bool flag = false;
      if (intList.Count > 0)
        flag = true;
      if (this.chkAccurateRip.Checked)
      {
        if (this.VerifyAccurateRip(stringWriter.ToString(), dl))
        {
          dl.accurateRip = "Good rip";
          flag = false;
        }
        else
        {
          dl.accurateRip = "Bad rip";
          flag = true;
        }
      }
      if (intList.Count > 0)
      {
        dl.info = "Done - Errors";
        flag = true;
      }
      else
        dl.info = "Done";
      if (flag && this.chkAutoQuality.Checked && quality < 2)
      {
        audioSource.Close();
        audioSource.ReadProgress -= new EventHandler<ReadProgressArgs>(this.CDReadProgress);
        this.SetText("RED-Disc " + dl.discNum + " had errors. Trying again using Paranoid mode.");
        dl.mode = 2;
        this.UpdateGrid();
        this.DoRip(dl, 2);
        return num;
      }
      if (flag && quality == 2 && this.DeleteBadRip(str, dl))
      {
        audioSource.Close();
        audioSource.ReadProgress -= new EventHandler<ReadProgressArgs>(this.CDReadProgress);
        return -1;
      }
      if (this.chkSingleWav.Checked)
      {
        this.CreateCUE(audioSource, str);
      }
      else
      {
        this.SetText("Splitting tracks on disc " + dl.discNum);
        if (this.rdWAV.Checked)
          this.SplitWavByChapterSox(chapters, str, 1);
        else
          this.SplitWavByChapterFFmpeg(chapters, str, 1);
        this.SafeDelete(str);
      }
      if (this.chkEject.Checked)
      {
        if (this.chkDebug.Checked)
          this.SetText("Ejecting " + dl.drive);
        try
        {
          audioSource.EjectDisk();
        }
        catch
        {
        }
      }
      audioSource.Close();
      audioSource.ReadProgress -= new EventHandler<ReadProgressArgs>(this.CDReadProgress);
      return num;
    }

    private void CreateCUE(CDDriveReader audioSource, string destFile)
    {
      StringWriter stringWriter = new StringWriter();
      stringWriter.WriteLine("FILE \"{0}\" WAVE", (object) destFile);
      for (int index1 = 1; index1 <= audioSource.TOC.TrackCount; ++index1)
      {
        if (audioSource.TOC[index1].IsAudio)
        {
          stringWriter.WriteLine("  TRACK {0:00} AUDIO", (object) audioSource.TOC[index1].Number);
          if (audioSource.TOC[index1].ISRC != null)
            stringWriter.WriteLine("    ISRC {0}", (object) audioSource.TOC[index1].ISRC);
          if (audioSource.TOC[index1].DCP || audioSource.TOC[index1].PreEmphasis)
            stringWriter.WriteLine("    FLAGS{0}{1}", audioSource.TOC[index1].PreEmphasis ? (object) " PRE" : (object) "", audioSource.TOC[index1].DCP ? (object) " DCP" : (object) "");
          for (int index2 = audioSource.TOC[index1].Pregap > 0U ? 0 : 1; (long) index2 <= (long) audioSource.TOC[index1].LastIndex; ++index2)
            stringWriter.WriteLine("    INDEX {0:00} {1}", (object) index2, (object) audioSource.TOC[index1][index2].MSF);
        }
      }
      stringWriter.Close();
      StreamWriter streamWriter = new StreamWriter(Path.ChangeExtension(destFile, ".cue"));
      streamWriter.Write(stringWriter.ToString());
      streamWriter.Close();
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

    private void SplitWavByChapterFFmpeg(AdvancedSplitting.Chapters myChapters, string sourceWavName, int startingTrack)
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
          str2 = str2 + " -t " + (myChapters.GetChapterDouble(pos + 1) - myChapters.GetChapterDouble(pos)).ToString("0.000", (IFormatProvider) CultureInfo.InvariantCulture) + " -ss " + myChapters.GetChapterDouble(pos).ToString("0.000", (IFormatProvider) CultureInfo.InvariantCulture) + " \"" + str1 + " - " + (pos + startingTrack).ToString("D3") + extension + "\"";
        else
          str2 = str2 + " -ss " + myChapters.GetChapterDouble(pos).ToString("0.000", (IFormatProvider) CultureInfo.InvariantCulture) + " \"" + str1 + " - " + (pos + startingTrack).ToString("D3") + extension + "\"";
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
        int exitCode = process.ExitCode;
      }
    }

    private AdvancedSplitting.Chapters GetChapters(CDDriveReader audioSource)
    {
      AdvancedSplitting.Chapters chapters = new AdvancedSplitting.Chapters();
      for (int index1 = 1; index1 <= audioSource.TOC.TrackCount; ++index1)
      {
        if (audioSource.TOC[index1].IsAudio)
        {
          for (int index2 = audioSource.TOC[index1].Pregap > 0U ? 0 : 1; (long) index2 <= (long) audioSource.TOC[index1].LastIndex; ++index2)
          {
            string[] strArray = audioSource.TOC[index1][index2].MSF.Split(':');
            double time = double.Parse(strArray[2]) * 0.01 + double.Parse(strArray[1]) + double.Parse(strArray[0]) * 60.0;
            chapters.Add(time);
          }
        }
      }
      return chapters;
    }

    private int GetQuality()
    {
      int num = 0;
      if (this.rdBurst.Checked)
        num = 0;
      else if (this.rdSecure.Checked)
        num = 1;
      else if (this.rdParanoid.Checked)
        num = 2;
      return num;
    }

    [DllImport("winmm.dll")]
    private static extern int mciSendString(string command, StringBuilder buffer, int bufferSize, IntPtr hwndCallback);

    private void m_oWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      this.ripping = true;
      int count = this.driveList.Count;
      this.smartThreadPool = new SmartThreadPool(10000, count);
      this.wir = new IWorkItemResult<int>[count];
      int index1 = 0;
      for (int index2 = 0; index2 < this.driveList.Count; ++index2)
      {
        if (!this.driveList[index2].empty)
        {
          this.driveList[index2].realStart = DateTime.Now;
          this.driveList[index2].mode = this.GetQuality();
        }
        this.wir[index1] = this.smartThreadPool.QueueWorkItem<DriveList, int, int>(new Amib.Threading.Func<DriveList, int, int>(this.DoRip), this.driveList[index2], this.GetQuality());
        ++index1;
      }
      int num1 = 0;
      int num2 = 0;
      while (num1 != this.wir.Length)
      {
        Thread.Sleep(1000);
        num1 = 0;
        for (int index2 = 0; index2 < this.wir.Length; ++index2)
        {
          if (this.wir[index2].IsCompleted)
            ++num1;
        }
        if (num2 != num1 && num1 > 0)
        {
          for (int index2 = num2; index2 < num1; ++index2)
            this.m_oWorker.ReportProgress((int) ((double) num1 / (double) this.wir.Length * 100.0));
          num2 = num1;
        }
      }
      this.smartThreadPool.WaitForIdle();
      this.smartThreadPool.Shutdown();
    }

    private void m_oWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
    }

    private void m_oWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (!e.Cancelled)
      {
        Exception error = e.Error;
      }
      if (!this.chkRipOnInsert.Checked)
      {
        this.ripping = false;
        this.btnCancel.Enabled = false;
        this.btnRip.Enabled = true;
      }
      this.SetText("GREEN-Completed.");
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.ripping = false;
      this.btnCancel.Enabled = false;
      this.btnRip.Enabled = true;
    }

    private bool VerifyAccurateRip(string logContents, DriveList dl)
    {
      bool flag = true;
      try
      {
        string[] strArray1 = logContents.Split('\n');
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
        bool[] array = new bool[int.Parse(dl.tracks)];
        for (int index = num1; index <= num2; ++index)
        {
          string[] strArray2 = strArray1[index].Trim().Split(' ');
          int result = 0;
          if (int.TryParse(strArray2[0], out result) && !array[result - 1])
            array[result - 1] = strArray1[index].Contains("Accurately ripped");
        }
        if (Array.IndexOf<bool>(array, false) == -1)
        {
          this.DebugLog("GREEN-AccurateRip reports all tracks ripped perfectly.");
          dl.accurateRip = "Rip OK";
          return true;
        }
        int num3 = 0;
        for (int index = 0; index < array.Length; ++index)
        {
          if (!array[index])
            ++num3;
        }
        if (num3 == array.Length)
        {
          dl.accurateRip = "Disc mismatch?";
          this.SetText("GREEN-All tracks report AccurateRip mismatch. Unless the disc is very damaged, it is likely that AccurateRip has incorrectly identified this disc.");
        }
        else
        {
          string str = "";
          dl.accurateRip = "Bad Rip";
          for (int index = 0; index < array.Length; ++index)
          {
            if (!array[index])
              str = str + (index + 1).ToString() + ", ";
          }
          this.SetText("RED-Accurate reports track(s) " + str.Substring(0, str.Length - 2) + " were ripped incorrectly.");
        }
      }
      catch (Exception ex)
      {
        Audible.diskLogger("error reading log file " + ex.ToString());
        return true;
      }
      return flag;
    }

    private void btnApply_Click(object sender, EventArgs e)
    {
      if (this.txtTargetDir.Text != "")
        this.apply = true;
      this.Close();
    }

    private delegate void SetTextCallback(string text);

    private delegate void SetGridCallback();
  }
}
