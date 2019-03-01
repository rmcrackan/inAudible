// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.FormOverdrive
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using Inwards;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagLib;
using TagLib.Mpeg;

namespace AudibleConvertor
{
  public class FormOverdrive : Form
  {
    public List<Overdrive> overdriveFiles = new List<Overdrive>();
    private string prefix = "";
    private bool ffmpegMode = true;
    private bool newMergeEngine = true;
    private IContainer components;
    private ContextMenuStrip contextMenuStrip1;
    private ToolStripMenuItem chap1ToolStripMenuItem;
    private SplitContainer splitContainer1;
    private Button btnRemove;
    private Button btnRemovePrefix;
    private DataGridView dgvChapters;
    private Button btnChapterBreakdown;
    private Button btnModify;
    private Button btnSelectFiles;
    private CheckBox chkVerifySplits;
    private CheckBox chkIncludeChapterName;
    private Button btnChapterize;
    private RichTextBox rtbLog;
    private Button btnSelectNonChapters;
    private CheckBox chkOldEngine;
    private System.Windows.Forms.TextBox txtTitle;
    private Label lblTitle;
    private PictureBox pictureBox1;
    public SupportLibraries mySupportLibs;
    private bool cancel;
    private DataTable dt;
    private Image currentImage;
    private TagLib.Tag myTags;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormOverdrive));
      this.contextMenuStrip1 = new ContextMenuStrip(this.components);
      this.chap1ToolStripMenuItem = new ToolStripMenuItem();
      this.splitContainer1 = new SplitContainer();
      this.btnSelectNonChapters = new Button();
      this.btnRemove = new Button();
      this.btnRemovePrefix = new Button();
      this.dgvChapters = new DataGridView();
      this.btnChapterBreakdown = new Button();
      this.btnModify = new Button();
      this.btnSelectFiles = new Button();
      this.chkOldEngine = new CheckBox();
      this.chkVerifySplits = new CheckBox();
      this.chkIncludeChapterName = new CheckBox();
      this.btnChapterize = new Button();
      this.rtbLog = new RichTextBox();
      this.lblTitle = new Label();
      this.txtTitle = new System.Windows.Forms.TextBox();
      this.pictureBox1 = new PictureBox();
      this.contextMenuStrip1.SuspendLayout();
      this.splitContainer1.BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      ((ISupportInitialize) this.dgvChapters).BeginInit();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.SuspendLayout();
      this.contextMenuStrip1.Items.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.chap1ToolStripMenuItem
      });
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new Size(107, 26);
      this.contextMenuStrip1.ItemClicked += new ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
      this.chap1ToolStripMenuItem.Name = "chap1ToolStripMenuItem";
      this.chap1ToolStripMenuItem.Size = new Size(106, 22);
      this.chap1ToolStripMenuItem.Text = "chap1";
      this.splitContainer1.BorderStyle = BorderStyle.FixedSingle;
      this.splitContainer1.Dock = DockStyle.Fill;
      this.splitContainer1.Location = new Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = Orientation.Horizontal;
      this.splitContainer1.Panel1.Controls.Add((Control) this.txtTitle);
      this.splitContainer1.Panel1.Controls.Add((Control) this.lblTitle);
      this.splitContainer1.Panel1.Controls.Add((Control) this.btnSelectNonChapters);
      this.splitContainer1.Panel1.Controls.Add((Control) this.btnRemove);
      this.splitContainer1.Panel1.Controls.Add((Control) this.btnRemovePrefix);
      this.splitContainer1.Panel1.Controls.Add((Control) this.dgvChapters);
      this.splitContainer1.Panel1.Controls.Add((Control) this.btnChapterBreakdown);
      this.splitContainer1.Panel1.Controls.Add((Control) this.btnModify);
      this.splitContainer1.Panel1.Controls.Add((Control) this.btnSelectFiles);
      this.splitContainer1.Panel2.Controls.Add((Control) this.pictureBox1);
      this.splitContainer1.Panel2.Controls.Add((Control) this.chkOldEngine);
      this.splitContainer1.Panel2.Controls.Add((Control) this.chkVerifySplits);
      this.splitContainer1.Panel2.Controls.Add((Control) this.chkIncludeChapterName);
      this.splitContainer1.Panel2.Controls.Add((Control) this.btnChapterize);
      this.splitContainer1.Panel2.Controls.Add((Control) this.rtbLog);
      this.splitContainer1.Size = new Size(591, 509);
      this.splitContainer1.SplitterDistance = 311;
      this.splitContainer1.TabIndex = 11;
      this.btnSelectNonChapters.Anchor = AnchorStyles.Bottom;
      this.btnSelectNonChapters.Enabled = false;
      this.btnSelectNonChapters.Location = new Point(157, 283);
      this.btnSelectNonChapters.Name = "btnSelectNonChapters";
      this.btnSelectNonChapters.Size = new Size(275, 23);
      this.btnSelectNonChapters.TabIndex = 17;
      this.btnSelectNonChapters.Text = "De-select all chapters that don't contain \"Chapter\"";
      this.btnSelectNonChapters.UseVisualStyleBackColor = true;
      this.btnSelectNonChapters.Click += new EventHandler(this.btnSelectNonChapters_Click);
      this.btnRemove.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.btnRemove.BackColor = Color.FromArgb((int) byte.MaxValue, 128, 0);
      this.btnRemove.Location = new Point(14, 283);
      this.btnRemove.Name = "btnRemove";
      this.btnRemove.Size = new Size(138, 23);
      this.btnRemove.TabIndex = 16;
      this.btnRemove.Text = "Remove de-selected chapters";
      this.btnRemove.UseVisualStyleBackColor = false;
      this.btnRemove.Click += new EventHandler(this.btnRemove_Click);
      this.btnRemovePrefix.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnRemovePrefix.Enabled = false;
      this.btnRemovePrefix.Location = new Point(486, 283);
      this.btnRemovePrefix.Name = "btnRemovePrefix";
      this.btnRemovePrefix.Size = new Size(91, 23);
      this.btnRemovePrefix.TabIndex = 15;
      this.btnRemovePrefix.Text = "Remove Prefix";
      this.btnRemovePrefix.UseVisualStyleBackColor = true;
      this.btnRemovePrefix.Click += new EventHandler(this.btnRemovePrefix_Click);
      this.dgvChapters.AllowUserToAddRows = false;
      this.dgvChapters.AllowUserToDeleteRows = false;
      this.dgvChapters.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dgvChapters.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvChapters.Enabled = false;
      this.dgvChapters.Location = new Point(12, 60);
      this.dgvChapters.Name = "dgvChapters";
      this.dgvChapters.Size = new Size(564, 217);
      this.dgvChapters.TabIndex = 14;
      this.dgvChapters.CellClick += new DataGridViewCellEventHandler(this.dgvChapters_CellClick);
      this.dgvChapters.CellEndEdit += new DataGridViewCellEventHandler(this.dgvChapters_CellEndEdit);
      this.btnChapterBreakdown.Anchor = AnchorStyles.Top;
      this.btnChapterBreakdown.Location = new Point(225, 3);
      this.btnChapterBreakdown.Name = "btnChapterBreakdown";
      this.btnChapterBreakdown.Size = new Size(138, 23);
      this.btnChapterBreakdown.TabIndex = 13;
      this.btnChapterBreakdown.Text = "Show chapter breakdown";
      this.btnChapterBreakdown.UseVisualStyleBackColor = true;
      this.btnChapterBreakdown.Click += new EventHandler(this.btnChapterBreakdown_Click);
      this.btnModify.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btnModify.Location = new Point(423, 3);
      this.btnModify.Name = "btnModify";
      this.btnModify.Size = new Size(154, 23);
      this.btnModify.TabIndex = 12;
      this.btnModify.Text = "Manually modify chapters";
      this.btnModify.UseVisualStyleBackColor = true;
      this.btnModify.Click += new EventHandler(this.btnModify_Click);
      this.btnSelectFiles.Location = new Point(12, 3);
      this.btnSelectFiles.Name = "btnSelectFiles";
      this.btnSelectFiles.Size = new Size(140, 23);
      this.btnSelectFiles.TabIndex = 11;
      this.btnSelectFiles.Text = "Select Overdrive MP3's";
      this.btnSelectFiles.UseVisualStyleBackColor = true;
      this.btnSelectFiles.Click += new EventHandler(this.btnSelectFiles_Click);
      this.chkOldEngine.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.chkOldEngine.AutoSize = true;
      this.chkOldEngine.Location = new Point(492, 95);
      this.chkOldEngine.Name = "chkOldEngine";
      this.chkOldEngine.Size = new Size(84, 30);
      this.chkOldEngine.TabIndex = 18;
      this.chkOldEngine.Text = "Compatibility\r\nMode";
      this.chkOldEngine.UseVisualStyleBackColor = true;
      this.chkVerifySplits.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.chkVerifySplits.AutoSize = true;
      this.chkVerifySplits.Checked = true;
      this.chkVerifySplits.CheckState = CheckState.Checked;
      this.chkVerifySplits.Location = new Point(11, 168);
      this.chkVerifySplits.Name = "chkVerifySplits";
      this.chkVerifySplits.Size = new Size(166, 17);
      this.chkVerifySplits.TabIndex = 11;
      this.chkVerifySplits.Text = "Verify that splits fall on silence";
      this.chkVerifySplits.UseVisualStyleBackColor = true;
      this.chkIncludeChapterName.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.chkIncludeChapterName.AutoSize = true;
      this.chkIncludeChapterName.Checked = true;
      this.chkIncludeChapterName.CheckState = CheckState.Checked;
      this.chkIncludeChapterName.Location = new Point(384, 168);
      this.chkIncludeChapterName.Name = "chkIncludeChapterName";
      this.chkIncludeChapterName.Size = new Size(185, 17);
      this.chkIncludeChapterName.TabIndex = 10;
      this.chkIncludeChapterName.Text = "Include chapter name in file name";
      this.chkIncludeChapterName.UseVisualStyleBackColor = true;
      this.btnChapterize.Anchor = AnchorStyles.Bottom;
      this.btnChapterize.Location = new Point(245, 164);
      this.btnChapterize.Name = "btnChapterize";
      this.btnChapterize.Size = new Size(98, 23);
      this.btnChapterize.TabIndex = 9;
      this.btnChapterize.Text = "Chapterize";
      this.btnChapterize.UseVisualStyleBackColor = true;
      this.btnChapterize.Click += new EventHandler(this.btnChapterize_Click);
      this.rtbLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.rtbLog.Location = new Point(12, 2);
      this.rtbLog.Name = "rtbLog";
      this.rtbLog.Size = new Size(472, 156);
      this.rtbLog.TabIndex = 8;
      this.rtbLog.Text = "";
      this.lblTitle.AutoSize = true;
      this.lblTitle.Location = new Point(15, 36);
      this.lblTitle.Name = "lblTitle";
      this.lblTitle.Size = new Size(62, 13);
      this.lblTitle.TabIndex = 18;
      this.lblTitle.Text = "Album Title:";
      this.txtTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txtTitle.Location = new Point(83, 33);
      this.txtTitle.Name = "txtTitle";
      this.txtTitle.Size = new Size(493, 20);
      this.txtTitle.TabIndex = 19;
      this.pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.pictureBox1.BorderStyle = BorderStyle.FixedSingle;
      this.pictureBox1.Location = new Point(489, 3);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(88, 86);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox1.TabIndex = 20;
      this.pictureBox1.TabStop = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(591, 509);
      this.Controls.Add((Control) this.splitContainer1);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (FormOverdrive);
      this.Text = "Overdrive Chapterizer";
      this.contextMenuStrip1.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.Panel2.PerformLayout();
      this.splitContainer1.EndInit();
      this.splitContainer1.ResumeLayout(false);
      ((ISupportInitialize) this.dgvChapters).EndInit();
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.ResumeLayout(false);
    }

    public FormOverdrive()
    {
      this.InitializeComponent();
    }

    private void btnSelectFiles_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Multiselect = true;
      openFileDialog.Filter = "Overdrive MP3 files|*.mp3|All files (*.*)|*.*";
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;
      this.dt = (DataTable) null;
      this.overdriveFiles.Clear();
      this.btnRemovePrefix.Enabled = false;
      this.prefix = "";
      this.btnSelectNonChapters.Enabled = false;
      Array.Sort<string>(openFileDialog.FileNames);
      int num1 = 0;
      TagLib.File tagFile = TagLib.File.Create(openFileDialog.FileNames[0]);
      this.myTags = tagFile.Tag;
      this.GetMP3Info(tagFile);
      if (this.myTags.Album == null || this.myTags.Album == "")
        this.txtTitle.Text = this.myTags.Title.Replace(" - Part 1", "").Replace(" - Part 01", "");
      else
        this.txtTitle.Text = this.myTags.Album;
      this.GetCover();
      foreach (string fileName in openFileDialog.FileNames)
      {
        Overdrive overdrive = new Overdrive(fileName);
        overdrive.ffprobePath = Path.GetDirectoryName(this.mySupportLibs.ffmpegPath) + "\\ffprobe.exe";
        if (!overdrive.HasOverdriveMetadata())
        {
          int num2 = (int) MessageBox.Show("Cannot find any Overdrive metadata.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
          return;
        }
        overdrive.ParseChapters();
        if (num1 == 0)
          overdrive.RemoveSubchapters(true);
        else
          overdrive.RemoveSubchapters(false);
        this.overdriveFiles.Add(overdrive);
        this.debugLog(Path.GetFileName(fileName) + " - " + (object) overdrive.ReturnChapters().Count() + " chapters");
        if (overdrive.errorText != "")
          this.debugLog("WARNING-" + overdrive.errorText);
        num1 += overdrive.ReturnChapters().Count();
      }
      this.RemoveRemainingDupes();
      this.PopulateDatatable();
      if (this.chkVerifySplits.Checked)
      {
        this.dgvChapters.Enabled = true;
        this.btnChapterize.Text = "Cancel";
        this.btnChapterize.BackColor = Color.Red;
        this.btnRemove.Enabled = false;
        this.splitContainer1.Panel1.Enabled = false;
        Task.Factory.StartNew((Action) (() =>
        {
          this.InsertLogSpace();
          this.SetText("Analyzing (this could take a while)...");
          this.AlignChapterSplitsFFmpeg();
        })).ContinueWith((Action<Task>) (t =>
        {
          this.SetText("Analysis complete.");
          this.CancelAlignment();
          this.dgvChapters.Enabled = true;
          this.btnRemove.Enabled = true;
          this.splitContainer1.Panel1.Enabled = true;
        }), TaskScheduler.FromCurrentSynchronizationContext());
      }
      else
        this.dgvChapters.Enabled = true;
      this.debugLog("EAA-" + (object) num1 + " input chapters");
      this.UpdateContextMenu();
    }

    private void GetMP3Info(TagLib.File tagFile)
    {
      foreach (AudioHeader codec in tagFile.Properties.Codecs)
      {
        string str = ".";
        if (codec.XingHeader.Present)
          str = " and appears to have a Xing header.";
        else if (codec.VBRIHeader.Present)
          str = " and appears to have a VBRI header.";
        this.SetText("FYI-Source file is " + codec.Description + " at " + (object) codec.AudioBitrate + "Kbits @ " + (object) codec.AudioSampleRate + "Hz, " + (object) codec.ChannelMode + str);
      }
    }

    private void GetCover()
    {
      if (this.myTags.Pictures.Length <= 0)
        return;
      MemoryStream memoryStream = new MemoryStream(this.myTags.Pictures[0].Data.Data);
      if (memoryStream != null && memoryStream.Length > 4096L)
      {
        this.currentImage = Image.FromStream((Stream) memoryStream);
        this.pictureBox1.Image = this.currentImage.GetThumbnailImage(100, 100, (Image.GetThumbnailImageAbort) null, IntPtr.Zero);
      }
      memoryStream.Close();
    }

    private void PopulateDatatable()
    {
      this.dt = new DataTable("Chapters");
      this.dt.Columns.Add(new DataColumn("Enabled", typeof (bool)));
      this.dt.Columns.Add("Id", Type.GetType("System.Int32"));
      this.dt.Columns.Add("Title", Type.GetType("System.String"));
      this.dt.Columns.Add("Time", Type.GetType("System.String"));
      this.dt.Columns.Add("Offset", Type.GetType("System.String"));
      this.dt.Columns.Add("File Num", Type.GetType("System.String"));
      this.dt.Columns.Add("First", Type.GetType("System.Int32"));
      this.dt.Columns.Add("Chapter", Type.GetType("System.String"));
      int num = 1;
      List<string> stringList = new List<string>();
      List<Color> colorList = new List<Color>();
      for (int index = 0; index < this.overdriveFiles.Count; ++index)
      {
        AdvancedSplitting.Chapters chapters = this.overdriveFiles[index].ReturnChapters();
        for (int pos = 0; pos < chapters.Count(); ++pos)
        {
          this.dt.Rows.Add((object) true, (object) num, (object) chapters.GetDescription(pos), (object) chapters.GetChapterFormatted(pos), (object) chapters.GetChapterOffset(pos).ToString("0.###"), (object) (index + 1), (object) pos);
          ++num;
          stringList.Add(chapters.GetDescription(pos));
          colorList.Add(chapters.GetChapterColor(pos));
          if (chapters.GetDescription(pos).ToLower().StartsWith("chapter") && chapters.GetDescription(pos) != Overdrive.mergeText)
            this.btnSelectNonChapters.Enabled = true;
        }
        string str = Utilities.CommonPrefix(stringList.ToArray());
        if (this.prefix == "" && str != "" && (this.prefix.ToLower().Trim() != "chapter" && str.Length > 1) && stringList.Count > 1)
        {
          this.prefix = str;
          this.SetText("WARNING-All chapters appear to start with \"" + this.prefix + "\".");
          this.btnRemovePrefix.Enabled = true;
        }
      }
      this.dgvChapters.DataSource = (object) this.dt;
      this.dgvChapters.Columns[6].Visible = false;
      this.dgvChapters.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
      for (int index = 0; index < colorList.Count; ++index)
        this.dgvChapters.Rows[index].DefaultCellStyle.BackColor = colorList[index];
      for (int index = 0; index < this.dt.Rows.Count; ++index)
      {
        if ((int) this.dt.Rows[index]["First"] == 0)
        {
          DataGridViewButtonCell gridViewButtonCell = new DataGridViewButtonCell();
          this.dgvChapters.Rows[index].Cells[5] = (DataGridViewCell) gridViewButtonCell;
          this.dgvChapters.Rows[index].Cells[5].Value = this.dt.Rows[index][5];
        }
        else
        {
          DataGridViewButtonCell gridViewButtonCell = new DataGridViewButtonCell();
          this.dgvChapters.Rows[index].Cells[7] = (DataGridViewCell) gridViewButtonCell;
          this.dgvChapters.Rows[index].Cells[7].Value = this.dt.Rows[index][1];
        }
      }
      foreach (DataGridViewColumn column in (BaseCollection) this.dgvChapters.Columns)
      {
        column.SortMode = DataGridViewColumnSortMode.NotSortable;
        if (column.Name == "File Num" || column.Name == "Chapter")
          column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      }
      this.UpdateContextMenu();
    }

    private void InsertLogSpace()
    {
      this.Invoke((System.Action) (() => this.rtbLog.AppendText("\r\n")));
    }

    private void CancelAlignment()
    {
      this.cancel = false;
      this.Invoke((System.Action) (() =>
      {
        this.btnChapterize.Text = "Chapterize";
        this.btnChapterize.BackColor = Color.FromKnownColor(KnownColor.Control);
        this.btnChapterize.FlatStyle = FlatStyle.Standard;
      }));
    }

    private void AlignChapterSplitsOld()
    {
      Utilities utilities1 = new Utilities();
      utilities1.StopwatchStart();
      VirtualWAV myVirtualWav = new VirtualWAV();
      myVirtualWav.ffmpegPath = this.mySupportLibs.ffmpegPath;
      myVirtualWav.soxPath = this.mySupportLibs.soxPath;
      myVirtualWav.sampleRate = 22050;
      myVirtualWav.channels = 2;
      myVirtualWav.aacMode = true;
      for (int index = 0; index < this.overdriveFiles.Count; ++index)
      {
        AdvancedSplitting.Chapters chapters = this.overdriveFiles[index].ReturnChapters();
        for (int pos = 0; pos < chapters.Count(); ++pos)
        {
          if (this.cancel)
          {
            this.CancelAlignment();
            return;
          }
          if (chapters.GetChapterDouble(pos) != 0.0)
          {
            Form1 form1 = new Form1();
            myVirtualWav.M4BtoWAV(this.overdriveFiles[index].mp3Filename);
            form1.myAdvancedOptions.verifySplitsSearchSize = 10;
            form1.myAdvancedOptions.doNotVerifyIfSilence = false;
            string withoutExtension = Path.GetFileNameWithoutExtension(this.overdriveFiles[index].mp3Filename);
            Utilities utilities2 = new Utilities();
            utilities2.StopwatchStart();
            double val = form1.VerifyChapterSplit(chapters.GetChapterDouble(pos), myVirtualWav);
            string str = "";
            if (Math.Abs(val - chapters.GetChapterDouble(pos)) > 5.0)
              str = "WARNING-";
            utilities2.StopwatchStop();
            this.SetText(str + (object) (pos + 1) + " - " + withoutExtension + ": \"" + chapters.GetDescription(pos) + "\" offset by " + string.Format("{0:0.0}", (object) (val - chapters.GetChapterDouble(pos))) + "s - search time was " + utilities2.StopwatchGetElapsed(""));
            chapters.SetChapter(pos, val);
          }
        }
        this.InsertLogSpace();
      }
      utilities1.StopwatchStop();
      this.SetText("Total search time was " + utilities1.StopwatchGetElapsed(""));
    }

    private string ExtractSubset(string file, double start, double duration)
    {
      string directoryName = Path.GetDirectoryName(file);
      SupportLibraries supportLibraries = new SupportLibraries();
      string str = directoryName + "\\scratch.mp3";
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = supportLibraries.ffmpegPath;
      process.StartInfo.WorkingDirectory = directoryName;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.Arguments = "-y -i \"" + file + "\" -c copy -t " + (object) duration + " -ss " + (object) start + " \"" + str + "\"";
      process.StartInfo.UseShellExecute = false;
      Audible.diskLogger(process.StartInfo.FileName.ToString() + " " + process.StartInfo.Arguments.ToString());
      process.Start();
      process.WaitForExit();
      return str;
    }

    private double FindSilenceFFmpeg(string file, double chapter, int threshold)
    {
      double start = chapter - (double) threshold;
      if (start < 0.0)
        start = 0.0;
      double duration = (double) (threshold * 2);
      string subset = this.ExtractSubset(file, start, duration);
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.mySupportLibs.ffmpegPath;
      process.StartInfo.WorkingDirectory = Path.GetDirectoryName(file);
      process.StartInfo.Arguments = "-i \"" + subset + "\" -af silencedetect=n=-50dB:d=0.25 -f null -";
      process.StartInfo.RedirectStandardError = true;
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.CreateNoWindow = true;
      Audible.diskLogger(process.StartInfo.FileName.ToString() + " " + process.StartInfo.Arguments.ToString());
      process.Start();
      string end = process.StandardError.ReadToEnd();
      process.WaitForExit();
      System.IO.File.Delete(subset);
      List<double[]> numArrayList = new List<double[]>();
      double num1 = 0.0;
      int num2 = 0;
      int index = 0;
      string str1 = end;
      char[] chArray = new char[1]{ '\n' };
      foreach (string str2 in str1.Split(chArray))
      {
        if (str2.Contains("silence_duration"))
        {
          string[] strArray = str2.Split(']')[1].Trim().Replace("\r", "").Split('|');
          double[] numArray = new double[2]
          {
            double.Parse(strArray[0].Trim().Split(' ')[1]),
            double.Parse(strArray[1].Trim().Split(' ')[1])
          };
          numArrayList.Add(numArray);
          if (num1 < numArray[1])
          {
            num1 = numArray[1];
            index = num2;
          }
          ++num2;
        }
      }
      if (numArrayList.Count == 0)
        return chapter;
      double[] numArray1 = numArrayList[index];
      return start + numArray1[0] - numArray1[1] / 2.0;
    }

    private void AlignChapterSplitsFFmpeg()
    {
      Utilities utilities1 = new Utilities();
      utilities1.StopwatchStart();
      int row = 0;
      for (int index = 0; index < this.overdriveFiles.Count; ++index)
      {
        AdvancedSplitting.Chapters chapters = this.overdriveFiles[index].ReturnChapters();
        for (int pos = 0; pos < chapters.Count(); ++pos)
        {
          if (this.cancel)
          {
            this.CancelAlignment();
            return;
          }
          if (chapters.GetChapterDouble(pos) != 0.0)
          {
            Path.GetFileNameWithoutExtension(this.overdriveFiles[index].mp3Filename);
            Utilities utilities2 = new Utilities();
            utilities2.StopwatchStart();
            double silenceFfmpeg = this.FindSilenceFFmpeg(this.overdriveFiles[index].mp3Filename, chapters.GetChapterDouble(pos), 10);
            utilities2.StopwatchStop();
            double offset = silenceFfmpeg - chapters.GetChapterDouble(pos);
            Color color = Color.LightGreen;
            if (Math.Abs(offset) > 5.0)
              color = Color.Red;
            this.UpdateOffsetGrid(row, offset, color);
            chapters.SetChapterOffset(pos, offset);
            chapters.SetChapterColor(pos, color);
            chapters.SetChapter(pos, silenceFfmpeg);
          }
          ++row;
        }
        this.Invoke((System.Action) (() => this.dgvChapters.PerformLayout()));
      }
      utilities1.StopwatchStop();
      this.SetText("Total search time was " + utilities1.StopwatchGetElapsed(""));
    }

    private void UpdateOffsetGrid(int row, double offset, Color color)
    {
      this.dgvChapters.Rows[row].DefaultCellStyle.BackColor = color;
      this.dt.Rows[row]["Offset"] = (object) offset.ToString("0.###");
      Form1.SetControlPropertyThreadSafe((Control) this.dgvChapters, "CurrentCell", (object) this.dgvChapters.Rows[row].Cells[0]);
      this.dgvChapters.Rows[row - 1].Selected = false;
      if (this.dgvChapters.Rows.Count <= row)
        return;
      this.dgvChapters.Rows[row].Selected = true;
    }

    private void AlignChapterSplits()
    {
      Utilities utilities1 = new Utilities();
      utilities1.StopwatchStart();
      FormChapterAligner formChapterAligner = new FormChapterAligner();
      for (int index = 0; index < this.overdriveFiles.Count; ++index)
      {
        AdvancedSplitting.Chapters chapters = this.overdriveFiles[index].ReturnChapters();
        for (int pos = 0; pos < chapters.Count(); ++pos)
        {
          if (this.cancel)
          {
            this.CancelAlignment();
            return;
          }
          if (chapters.GetChapterDouble(pos) != 0.0)
          {
            AdjustFile adjustFile = new AdjustFile();
            adjustFile.File = this.overdriveFiles[index].mp3Filename;
            adjustFile.Chapter = chapters.GetChapterDouble(pos);
            formChapterAligner.fileToAdjust = adjustFile;
            Utilities utilities2 = new Utilities();
            utilities2.StopwatchStart();
            int num = (int) formChapterAligner.ShowDialog();
            adjustFile.Cleanup();
            double offset = formChapterAligner.fileToAdjust.Offset;
            string str = "";
            if (Math.Abs(offset - chapters.GetChapterDouble(pos)) > 5.0)
              str = "WARNING-";
            utilities2.StopwatchStop();
            string withoutExtension = Path.GetFileNameWithoutExtension(this.overdriveFiles[index].mp3Filename);
            this.SetText(str + (object) (pos + 1) + " - " + withoutExtension + ": \"" + chapters.GetDescription(pos) + "\" offset by " + string.Format("{0:0.0}", (object) (offset - chapters.GetChapterDouble(pos))) + "s - search time was " + utilities2.StopwatchGetElapsed(""));
            chapters.SetChapter(pos, offset);
          }
        }
        this.InsertLogSpace();
      }
      utilities1.StopwatchStop();
      this.SetText("Total search time was " + utilities1.StopwatchGetElapsed(""));
    }

    private void AlignChapterSplitsBatch()
    {
      Utilities utilities = new Utilities();
      utilities.StopwatchStart();
      FormChapterAligner frmSplitter = new FormChapterAligner();
      frmSplitter.myOverdriveFiles = this.overdriveFiles;
      int num;
      Task.Factory.StartNew((Action) (() => num = (int) frmSplitter.ShowDialog())).ContinueWith((Action<Task>) (t => {}), TaskScheduler.FromCurrentSynchronizationContext());
      while (!frmSplitter.jobComplete)
      {
        Thread.Sleep(100);
        if (!frmSplitter.myJobStatus.Shown)
          this.SetText(frmSplitter.myJobStatus.GetStatus());
      }
      this.overdriveFiles = frmSplitter.myOverdriveFiles;
      utilities.StopwatchStop();
      this.SetText("Total search time was " + utilities.StopwatchGetElapsed(""));
    }

    public void RemoveRemainingDupes()
    {
      for (int index1 = 0; index1 < this.overdriveFiles.Count; ++index1)
      {
        AdvancedSplitting.Chapters chapters1 = this.overdriveFiles[index1].ReturnChapters();
        if (chapters1.GetDescription(0) == Overdrive.mergeText)
        {
          string str = "";
          for (int index2 = 1; index2 < this.overdriveFiles.Count; ++index2)
          {
            AdvancedSplitting.Chapters chapters2 = this.overdriveFiles[index1 - index2].ReturnChapters();
            if (chapters2.Count() != 0)
            {
              str = chapters2.GetDescription(chapters2.Count() - 1);
              break;
            }
          }
          for (int pos = 0; pos < chapters1.Count(); ++pos)
          {
            if (str == chapters1.GetDescription(pos) && this.overdriveFiles[index1].ReturnChapters().Count() > 1 && str != Overdrive.mergeText)
              this.overdriveFiles[index1].ReturnChapters().Remove(pos);
          }
        }
      }
    }

    private void UpdateContextMenu()
    {
      this.contextMenuStrip1.Items.Clear();
      for (int index = 0; index < this.overdriveFiles.Count; ++index)
      {
        this.contextMenuStrip1.Items.Add(Path.GetFileNameWithoutExtension(this.overdriveFiles[index].mp3Filename));
        AdvancedSplitting.Chapters chapters = this.overdriveFiles[index].ReturnChapters();
        string str = "";
        for (int pos = 0; pos < chapters.Count(); ++pos)
        {
          if (!(chapters.GetDescription(pos) == Overdrive.mergeText))
            str = str + "\"" + chapters.GetDescription(pos) + "\" ";
        }
        this.contextMenuStrip1.Items[index].ToolTipText = str;
      }
    }

    private string GetRealChapterName(int index)
    {
      List<string> stringList = new List<string>();
      for (int index1 = 0; index1 < this.overdriveFiles.Count; ++index1)
      {
        int num = this.overdriveFiles[index1].ReturnChapters().Count();
        for (int index2 = 0; index2 < num; ++index2)
          stringList.Add(this.overdriveFiles[index1].ReturnChapters().GetChapterNames(false)[index2]);
      }
      List<string> source = new List<string>();
      foreach (string str in stringList)
      {
        if (source.Count == 0 || source.Last<string>() != str)
          source.Add(str);
      }
      return source[index];
    }

    private void debugLog(string text)
    {
      if (text.Length == 0)
        return;
      int maxValue = int.MaxValue;
      string text1;
      if (text.StartsWith("NODATE-"))
      {
        text = text.Replace("NODATE-", "");
        text1 = text + "\r\n";
      }
      else
        text1 = "[" + DateTime.Today.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "] - " + text + "\r\n";
      if (this.rtbLog.Text.Length + text1.Length > maxValue)
        this.rtbLog.Text = this.rtbLog.Text.Substring(text1.Length, this.rtbLog.Text.Length - text1.Length);
      Color color = Color.Black;
      if (text1.Contains("FYI-"))
      {
        text1 = text1.Replace("FYI-", "");
        color = Color.Blue;
      }
      if (text1.Contains("WARNING-"))
      {
        text1 = text1.Replace("WARNING-", "");
        color = Color.Red;
      }
      if (text1.Contains("EAA-"))
      {
        text1 = text1.Replace("EAA-", "");
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

    private void btnChapterize_Click(object sender, EventArgs e)
    {
      this.newMergeEngine = this.chkOldEngine.Checked;
      if (this.btnChapterize.Text == "Cancel")
      {
        this.SetText("WARNING-Cancelling analysis...");
        this.cancel = true;
      }
      else
      {
        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        string str = "mp3";
        saveFileDialog1.DefaultExt = str;
        saveFileDialog1.Filter = str.ToUpper() + " files (*." + str + ")|*" + str + "|All files (*.*)|*.*";
        saveFileDialog1.AddExtension = true;
        this.myTags.Album = this.txtTitle.Text;
        DialogResult dialogResult = saveFileDialog1.ShowDialog();
        int offset = 0;
        if (dialogResult != DialogResult.OK)
          return;
        this.btnChapterize.Enabled = false;
        Utilities totalUtils = new Utilities();
        totalUtils.StopwatchStart();
        Task.Factory.StartNew((Action) (() =>
        {
          for (int index = 0; index < this.overdriveFiles.Count; ++index)
          {
            this.SetText("Cutting " + Path.GetFileName(this.overdriveFiles[index].mp3Filename));
            if (this.newMergeEngine)
              this.CutFilesNoXing(this.overdriveFiles[index], saveFileDialog1.FileName, offset);
            else if (this.ffmpegMode)
              this.CutFileByChapterFFmpeg(this.overdriveFiles[index], saveFileDialog1.FileName, offset);
            else
              this.CutFileByChapterMP3SPLT(this.overdriveFiles[index], saveFileDialog1.FileName, offset);
            offset += this.overdriveFiles[index].ReturnChapters().Count();
            if (this.overdriveFiles[index].ReturnChapters().Count() == 0)
              ++offset;
          }
          this.FinalizeSegments(offset, this.FindOverlappingSegments(), saveFileDialog1.FileName);
        })).ContinueWith((Action<Task>) (t =>
        {
          totalUtils.StopwatchStop();
          this.SetText("EAA-Completed in " + totalUtils.StopwatchGetElapsed(""));
          this.btnChapterize.Enabled = true;
        }), TaskScheduler.FromCurrentSynchronizationContext());
      }
    }

    private void FinalizeSegments(int totalFiles, int[] overlaps, string filename)
    {
      int realChapter = 1;
      int[] numArray = new int[totalFiles];
      int index1 = 0;
      for (int index2 = 0; index2 < this.overdriveFiles.Count; ++index2)
      {
        int num = this.overdriveFiles[index2].ReturnChapters().Count();
        for (int index3 = 0; index3 < num; ++index3)
        {
          numArray[index1] = index2;
          ++index1;
        }
      }
      for (int postion = 0; postion < totalFiles; ++postion)
      {
        if (Array.IndexOf<int>(overlaps, postion) == -1)
        {
          this.SetText("Fixing tags " + (object) (postion + 1) + "...");
          if (this.newMergeEngine)
            this.TagAndRenameNewEngine(postion, realChapter, filename, this.overdriveFiles[numArray[postion]]);
          else
            this.TagAndRename(postion, realChapter, filename, this.overdriveFiles[numArray[postion]]);
          ++realChapter;
        }
        else
        {
          this.SetText("Merging " + (object) postion + " & " + (object) (postion + 1) + "...");
          if (this.newMergeEngine)
            this.CombineTagAndRenameNewEngine(postion, realChapter, filename, this.overdriveFiles[numArray[postion]]);
          else
            this.CombineTagAndRename(postion, realChapter, filename, this.overdriveFiles[numArray[postion]]);
        }
      }
    }

    private void TagAndRenameNewEngine(int postion, int realChapter, string outputFile, Overdrive overdrive)
    {
      string path = Path.GetFileNameWithoutExtension(outputFile) + " - " + realChapter.ToString("D3");
      if (this.chkIncludeChapterName.Checked)
        path = path + " - " + this.GetRealChapterName(realChapter - 1);
      string withoutExtension = Path.GetFileNameWithoutExtension(path);
      string str1 = "s-" + postion.ToString("D3") + ".mp3";
      string str2 = Path.GetDirectoryName(outputFile) + "\\" + path + ".mp3";
      if (System.IO.File.Exists(str2))
      {
        try
        {
          System.IO.File.Delete(str2);
        }
        catch
        {
        }
      }
      System.IO.File.Move(Path.GetDirectoryName(outputFile) + "\\" + str1, str2);
      TagLib.File file = TagLib.File.Create(str2);
      TagLib.Id3v2.Tag tag = (TagLib.Id3v2.Tag) file.GetTag(TagTypes.Id3v2, false);
      if (tag != null)
        tag.Version = (byte) 3;
      this.myTags.CopyTo(file.Tag, true);
      file.Tag.Track = (uint) realChapter;
      file.Tag.Title = withoutExtension;
      file.Tag.Pictures = this.myTags.Pictures;
      bool flag = false;
      while (!flag)
      {
        try
        {
          file.Save();
          flag = true;
        }
        catch
        {
          Thread.Sleep(200);
        }
      }
      file.Dispose();
    }

    private void TagAndRename(int postion, int realChapter, string outputFile, Overdrive overdrive)
    {
      string path = Path.GetFileNameWithoutExtension(outputFile) + " - " + realChapter.ToString("D3");
      if (this.chkIncludeChapterName.Checked)
        path = path + " - " + this.GetRealChapterName(realChapter - 1);
      string withoutExtension = Path.GetFileNameWithoutExtension(path);
      string str1 = "s-" + postion.ToString("D3") + ".mp3";
      string str2 = Path.GetDirectoryName(outputFile) + "\\" + path + ".mp3";
      if (System.IO.File.Exists(str2))
      {
        try
        {
          System.IO.File.Delete(str2);
        }
        catch
        {
        }
      }
      System.IO.File.Move(Path.GetDirectoryName(outputFile) + "\\" + str1, str2);
      TagLib.File file = TagLib.File.Create(str2);
      TagLib.Id3v2.Tag tag = (TagLib.Id3v2.Tag) file.GetTag(TagTypes.Id3v2, false);
      if (tag != null)
        tag.Version = (byte) 3;
      file.Tag.Track = (uint) realChapter;
      file.Tag.Title = withoutExtension;
      file.Tag.Album = this.myTags.Album;
      bool flag = false;
      while (!flag)
      {
        try
        {
          file.Save();
          flag = true;
        }
        catch
        {
          Thread.Sleep(200);
        }
      }
      file.Dispose();
    }

    private void DeleteXingHeader(string file)
    {
      byte[] pattern1 = new byte[6]
      {
        (byte) 0,
        (byte) 73,
        (byte) 110,
        (byte) 102,
        (byte) 111,
        (byte) 0
      };
      byte[] pattern2 = new byte[5]
      {
        (byte) 0,
        byte.MaxValue,
        (byte) 251,
        (byte) 80,
        (byte) 0
      };
      int num = this.GetPositionAfterMatch(pattern2, file) - pattern2.Length;
      if (num < 0)
      {
        num = this.GetPositionAfterMatch(pattern1, file) - pattern1.Length;
        if (num < 0)
        {
          this.debugLog("Couldn't find Xing header");
          return;
        }
      }
      while (this.IsFileLocked(new FileInfo(file)))
        Thread.Sleep(100);
      using (Stream output = (Stream) System.IO.File.Open(file, FileMode.Open))
      {
        using (BinaryWriter binaryWriter = new BinaryWriter(output))
        {
          binaryWriter.BaseStream.Position = (long) num;
          for (int index = 0; index < 328; ++index)
            binaryWriter.Write(0);
        }
      }
    }

    protected virtual bool IsFileLocked(FileInfo file)
    {
      FileStream fileStream = (FileStream) null;
      try
      {
        fileStream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
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

    private int GetPositionAfterMatch(byte[] pattern, string file)
    {
      byte[] numArray = System.IO.File.ReadAllBytes(file);
      for (int index1 = 0; index1 < numArray.Length - pattern.Length; ++index1)
      {
        bool flag = true;
        for (int index2 = 0; index2 < pattern.Length; ++index2)
        {
          if ((int) numArray[index1 + index2] != (int) pattern[index2])
          {
            flag = false;
            break;
          }
        }
        if (flag)
          return index1 + pattern.Length;
      }
      return 0;
    }

    private void FixMP3File(string file)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.mySupportLibs.mp3valPath;
      process.StartInfo.Arguments = "-f -nb \"" + file + "\"";
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.CreateNoWindow = true;
      process.Start();
      process.WaitForExit();
    }

    private void CombineTagAndRenameNewEngine(int postion, int realChapter, string outputFile, Overdrive overdrive)
    {
      string str1 = Path.GetFileNameWithoutExtension(outputFile) + " - " + (realChapter - 1).ToString("D3");
      if (this.chkIncludeChapterName.Checked)
        str1 = str1 + " - " + this.GetRealChapterName(realChapter - 2);
      string path1 = str1 + ".mp3";
      string withoutExtension = Path.GetFileNameWithoutExtension(path1);
      string str2 = "s-" + postion.ToString("D3") + ".mp3";
      string[] strArray = new string[2]
      {
        Path.GetDirectoryName(outputFile) + "\\" + path1,
        Path.GetDirectoryName(outputFile) + "\\" + str2
      };
      using (FileStream fileStream1 = System.IO.File.Create(Path.GetDirectoryName(outputFile) + "\\od-temp.mp3"))
      {
        foreach (string path2 in strArray)
        {
          using (FileStream fileStream2 = System.IO.File.OpenRead(path2))
            fileStream2.CopyTo((Stream) fileStream1);
        }
      }
      string str3 = Path.GetDirectoryName(outputFile) + "\\" + path1;
      System.IO.File.Delete(Path.GetDirectoryName(outputFile) + "\\" + str2);
      System.IO.File.Delete(Path.GetDirectoryName(outputFile) + "\\" + path1);
      System.IO.File.Move(Path.GetDirectoryName(outputFile) + "\\od-temp.mp3", str3);
      TagLib.File file = TagLib.File.Create(str3);
      this.myTags.CopyTo(file.Tag, true);
      file.Tag.Album = file.Tag.Title;
      file.Tag.Pictures = this.myTags.Pictures;
      file.Tag.Track = (uint) (realChapter - 1);
      file.Tag.Title = withoutExtension;
      bool flag = false;
      while (!flag)
      {
        try
        {
          file.Save();
          flag = true;
        }
        catch
        {
          Thread.Sleep(200);
        }
      }
      file.Dispose();
      this.FixMP3File(str3);
    }

    private void CombineTagAndRename(int postion, int realChapter, string outputFile, Overdrive overdrive)
    {
      string str1 = Path.GetFileNameWithoutExtension(outputFile) + " - " + (realChapter - 1).ToString("D3");
      if (this.chkIncludeChapterName.Checked)
        str1 = str1 + " - " + this.GetRealChapterName(realChapter - 2);
      string path1 = str1 + ".mp3";
      string withoutExtension = Path.GetFileNameWithoutExtension(path1);
      string str2 = "s-" + postion.ToString("D3") + ".mp3";
      string[] strArray = new string[2]
      {
        Path.GetDirectoryName(outputFile) + "\\" + path1,
        Path.GetDirectoryName(outputFile) + "\\" + str2
      };
      using (FileStream fileStream1 = System.IO.File.Create(Path.GetDirectoryName(outputFile) + "\\od-temp.mp3"))
      {
        foreach (string path2 in strArray)
        {
          using (FileStream fileStream2 = System.IO.File.OpenRead(path2))
            fileStream2.CopyTo((Stream) fileStream1);
        }
      }
      string str3 = Path.GetDirectoryName(outputFile) + "\\" + path1;
      System.IO.File.Delete(Path.GetDirectoryName(outputFile) + "\\" + str2);
      System.IO.File.Delete(Path.GetDirectoryName(outputFile) + "\\" + path1);
      System.IO.File.Move(Path.GetDirectoryName(outputFile) + "\\od-temp.mp3", str3);
      TagLib.File file = TagLib.File.Create(str3);
      file.Tag.Track = (uint) (realChapter - 1);
      file.Tag.Title = withoutExtension;
      file.Tag.Album = this.myTags.Album;
      bool flag = false;
      while (!flag)
      {
        try
        {
          file.Save();
          flag = true;
        }
        catch
        {
          Thread.Sleep(200);
        }
      }
      file.Dispose();
      this.FixMP3File(str3);
    }

    private int[] FindOverlappingSegments()
    {
      List<int> intList = new List<int>();
      string description = this.overdriveFiles[0].ReturnChapters().GetDescription(this.overdriveFiles[0].ReturnChapters().Count() - 1);
      int num = this.overdriveFiles[0].ReturnChapters().Count();
      for (int index = 1; index < this.overdriveFiles.Count; ++index)
      {
        if (this.overdriveFiles[index].ReturnChapters().Count() == 0 || description == this.overdriveFiles[index].ReturnChapters().GetDescription(0) || this.overdriveFiles[index].ReturnChapters().GetDescription(0) == Overdrive.mergeText)
        {
          if (this.overdriveFiles[index].ReturnChapters().GetDescription(0) == Overdrive.mergeText || this.overdriveFiles[index].ReturnChapters().GetDescription(0) == "")
            this.overdriveFiles[index].SetChapterDescription(0, description);
          this.SetText(description + " is duplicated in " + Path.GetFileName(this.overdriveFiles[index].mp3Filename));
          intList.Add(num);
        }
        if (this.overdriveFiles[index].ReturnChapters().Count() > 0)
        {
          description = this.overdriveFiles[index].ReturnChapters().GetDescription(this.overdriveFiles[index].ReturnChapters().Count() - 1);
          num += this.overdriveFiles[index].ReturnChapters().Count();
        }
        else
          ++num;
      }
      return intList.ToArray();
    }

    private void CutFileByChapterFFmpeg(Overdrive thisOverdrive, string outputFile, int offset)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.mySupportLibs.ffmpegPath;
      process.StartInfo.WorkingDirectory = Path.GetDirectoryName(outputFile);
      string str = "";
      List<double> doubleList = thisOverdrive.ReturnChapters().GetDoubleList();
      if (doubleList.Count == 0)
      {
        System.IO.File.Copy(thisOverdrive.mp3Filename, Path.GetDirectoryName(outputFile) + "\\s-" + offset.ToString("D3") + ".mp3", true);
      }
      else
      {
        for (int index = 0; index < doubleList.Count; ++index)
        {
          if (doubleList.Count != index + 1)
            str = str + " -acodec copy -t " + (doubleList[index + 1] - doubleList[index]).ToString("0.000", (IFormatProvider) CultureInfo.InvariantCulture) + " -ss " + doubleList[index].ToString("0.000", (IFormatProvider) CultureInfo.InvariantCulture) + " s-" + (index + offset).ToString("D3") + ".mp3";
          else
            str = str + " -acodec copy -ss " + doubleList[index].ToString("0.000", (IFormatProvider) CultureInfo.InvariantCulture) + " s-" + (index + offset).ToString("D3") + ".mp3";
        }
        process.StartInfo.Arguments = string.Format("-y -i \"{0}\" -write_xing 0 {1}", (object) thisOverdrive.mp3Filename, (object) str);
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        Audible.diskLogger(process.StartInfo.FileName.ToString() + " " + process.StartInfo.Arguments.ToString());
        process.Start();
        process.WaitForExit();
        int exitCode = process.ExitCode;
      }
    }

    private void CutFilesNoXing(Overdrive thisOverdrive, string outputFile, int offset)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.mySupportLibs.mp3SplitPath;
      process.StartInfo.WorkingDirectory = Path.GetDirectoryName(outputFile);
      string str1 = "";
      List<double> doubleList = thisOverdrive.ReturnChapters().GetDoubleList();
      if (doubleList.Count == 0)
      {
        System.IO.File.Copy(thisOverdrive.mp3Filename, Path.GetDirectoryName(outputFile) + "\\s-" + offset.ToString("D3") + ".mp3", true);
      }
      else
      {
        for (int index = 0; index < doubleList.Count; ++index)
          str1 = str1 + AdvancedSplitting.SecondsToMp3SpltTime(doubleList[index]) + " ";
        string str2 = str1 + "EOF";
        process.StartInfo.Arguments = string.Format("-f \"{0}\" {1} -n -x -d \"{3}\" -o \"{2}\"", (object) thisOverdrive.mp3Filename, (object) str2, (object) "od-@n", (object) Path.GetDirectoryName(outputFile));
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        Audible.diskLogger(process.StartInfo.FileName.ToString() + " " + process.StartInfo.Arguments.ToString());
        process.Start();
        process.WaitForExit();
        int exitCode = process.ExitCode;
        FileInfo[] files = new DirectoryInfo(Path.GetDirectoryName(outputFile)).GetFiles("od-*.mp3");
        for (int index = 0; index < files.Length; ++index)
        {
          int num = int.Parse(Path.GetFileNameWithoutExtension(files[index].Name).Split('-')[1]);
          string sourceFileName = Path.GetDirectoryName(outputFile) + "\\" + files[index].Name;
          string str3 = Path.GetDirectoryName(outputFile) + "\\s-" + (num + offset - 1).ToString("D3") + ".mp3";
          try
          {
            System.IO.File.Delete(str3);
          }
          catch
          {
          }
          System.IO.File.Move(sourceFileName, str3);
        }
      }
    }

    private void CutFileByChapterMP3SPLT(Overdrive thisOverdrive, string outputFile, int offset)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.mySupportLibs.mp3SplitPath;
      process.StartInfo.WorkingDirectory = Path.GetDirectoryName(outputFile);
      string str1 = "";
      List<double> doubleList = thisOverdrive.ReturnChapters().GetDoubleList();
      if (doubleList.Count == 0)
      {
        System.IO.File.Copy(thisOverdrive.mp3Filename, Path.GetDirectoryName(outputFile) + "\\s-" + offset.ToString("D3") + ".mp3", true);
      }
      else
      {
        for (int index = 0; index < doubleList.Count; ++index)
          str1 = str1 + AdvancedSplitting.SecondsToMp3SpltTime(doubleList[index]) + " ";
        string str2 = str1 + "EOF";
        process.StartInfo.Arguments = string.Format("-f \"{0}\" {1} -d \"{3}\" -o \"{2}\"", (object) thisOverdrive.mp3Filename, (object) str2, (object) "od-@n", (object) Path.GetDirectoryName(outputFile));
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        Audible.diskLogger(process.StartInfo.FileName.ToString() + " " + process.StartInfo.Arguments.ToString());
        process.Start();
        process.WaitForExit();
        int exitCode = process.ExitCode;
        FileInfo[] files = new DirectoryInfo(Path.GetDirectoryName(outputFile)).GetFiles("od-*.mp3");
        for (int index = 0; index < files.Length; ++index)
        {
          int num = int.Parse(Path.GetFileNameWithoutExtension(files[index].Name).Split('-')[1]);
          System.IO.File.Move(Path.GetDirectoryName(outputFile) + "\\" + files[index].Name, Path.GetDirectoryName(outputFile) + "\\s-" + (num + offset - 1).ToString("D3") + ".mp3");
        }
      }
    }

    private void btnModify_Click(object sender, EventArgs e)
    {
      if (this.overdriveFiles.Count == 0)
        return;
      this.contextMenuStrip1.Show((Control) this.btnModify, new Point(0, this.btnModify.Height));
    }

    private void LaunchChapterAdjuster(string selectedFile)
    {
      this.debugLog("Editing " + selectedFile);
      int index1 = 0;
      for (int index2 = 0; index2 < this.overdriveFiles.Count; ++index2)
      {
        if (Path.GetFileNameWithoutExtension(this.overdriveFiles[index2].mp3Filename) == selectedFile)
        {
          index1 = index2;
          break;
        }
      }
      AdvancedSplitting advancedSplitting = new AdvancedSplitting();
      advancedSplitting.ffmpegPath = this.mySupportLibs.ffmpegPath;
      advancedSplitting.overdriveFile = this.overdriveFiles[index1];
      advancedSplitting.myChapters = this.overdriveFiles[index1].ReturnChapters();
      advancedSplitting.overdriveMode = true;
      VirtualWAV virtualWav = new VirtualWAV();
      AdvancedSplitting.Chapters chapters = this.overdriveFiles[index1].ReturnChapters();
      virtualWav.totalSeconds = chapters.GetLastChapter() * 2.0;
      advancedSplitting.myVirtualWav = virtualWav;
      advancedSplitting.Height = Screen.PrimaryScreen.Bounds.Height;
      advancedSplitting.Width = Screen.PrimaryScreen.Bounds.Width;
      int num = (int) advancedSplitting.ShowDialog();
      if (advancedSplitting.applied)
      {
        this.debugLog("FYI-New chapters applied");
        this.overdriveFiles[index1] = advancedSplitting.overdriveFile;
        if (index1 > 0)
          this.AddMerge(index1);
        this.UpdateContextMenu();
        this.PopulateDatatable();
      }
      else
        this.debugLog("WARNING-Changed discarded");
      advancedSplitting.Dispose();
    }

    private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {
      this.LaunchChapterAdjuster(e.ClickedItem.Text);
    }

    private void AddMerge(int index)
    {
      AdvancedSplitting.Chapters chapters = this.overdriveFiles[index].ReturnChapters();
      AdvancedSplitting.Chapters newChapters = new AdvancedSplitting.Chapters();
      newChapters.Add(0.0, Overdrive.mergeText);
      if (chapters.GetChapterDouble(0) <= 0.0)
        return;
      for (int pos = 0; pos < chapters.Count(); ++pos)
        newChapters.Add(chapters.GetChapter(pos).time, chapters.GetChapter(pos).description);
      this.overdriveFiles[index].SetChapters(newChapters);
    }

    private void SetText(string text)
    {
      if (this.rtbLog.InvokeRequired)
        this.Invoke((Delegate) new FormOverdrive.SetTextCallback(this.SetText), (object) text);
      else
        this.debugLog(text);
    }

    private void btnChapterBreakdown_Click(object sender, EventArgs e)
    {
      foreach (Overdrive overdriveFile in this.overdriveFiles)
      {
        this.debugLog("NODATE-WARNING-" + Path.GetFileNameWithoutExtension(overdriveFile.mp3Filename) + ":");
        AdvancedSplitting.Chapters chapters = overdriveFile.ReturnChapters();
        for (int pos = 0; pos < chapters.Count(); ++pos)
        {
          string description = chapters.GetDescription(pos);
          if (description == Overdrive.mergeText)
            this.debugLog("NODATE-(second half of previous chapter)");
          else
            this.debugLog("NODATE-FYI- " + description);
        }
      }
    }

    private void btnRemovePrefix_Click(object sender, EventArgs e)
    {
      for (int index = 0; index < this.overdriveFiles.Count; ++index)
      {
        AdvancedSplitting.Chapters chapters = this.overdriveFiles[index].ReturnChapters();
        for (int pos = 0; pos < chapters.Count(); ++pos)
          chapters.SetDescription(pos, chapters.GetDescription(pos).Replace(this.prefix, ""));
      }
      this.PopulateDatatable();
      this.btnRemovePrefix.Enabled = false;
    }

    private void btnRemove_Click(object sender, EventArgs e)
    {
      int index1 = 0;
      List<int> intList = new List<int>();
      for (int index2 = 0; index2 < this.overdriveFiles.Count; ++index2)
      {
        AdvancedSplitting.Chapters chapters = this.overdriveFiles[index2].ReturnChapters();
        int num1 = 0;
        int num2 = chapters.Count();
        for (int pos = 0; pos < num2; ++pos)
        {
          if (this.dt.Rows.Count > index1)
          {
            if (!(bool) this.dt.Rows[index1][0])
            {
              if (pos == 0)
              {
                chapters.SetDescription(pos, Overdrive.mergeText);
              }
              else
              {
                chapters.Remove(pos - num1);
                ++num1;
              }
              intList.Add(index1);
            }
            ++index1;
          }
        }
      }
      try
      {
        if (this.overdriveFiles[0].ReturnChapters().GetDescription(0) == Overdrive.mergeText)
        {
          AdvancedSplitting.Chapters chapters = this.overdriveFiles[0].ReturnChapters();
          chapters.SetDescription(0, chapters.GetDescription(1));
          chapters.Remove(1);
        }
      }
      catch (Exception ex)
      {
        Audible.diskLogger("Blew up trying to fix first chapter description: " + ex.ToString());
      }
      this.PopulateDatatable();
    }

    private void dgvChapters_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
      int num1 = 0;
      for (int index = 0; index < this.overdriveFiles.Count; ++index)
      {
        AdvancedSplitting.Chapters chapters = this.overdriveFiles[index].ReturnChapters();
        int num2 = chapters.Count();
        for (int pos = 0; pos < num2; ++pos)
        {
          if (num1 == e.RowIndex)
            chapters.SetDescription(pos, this.dt.Rows[e.RowIndex][2].ToString());
          ++num1;
        }
      }
      this.UpdateContextMenu();
    }

    private void dgvChapters_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.ColumnIndex < 0)
        return;
      string name = this.dgvChapters.Columns[e.ColumnIndex].Name;
      if (name.Equals("File Num"))
      {
        int num1 = 0;
        for (int index1 = 0; index1 < this.overdriveFiles.Count; ++index1)
        {
          int num2 = this.overdriveFiles[index1].ReturnChapters().Count();
          for (int index2 = 0; index2 < num2; ++index2)
          {
            if (num1 == e.RowIndex)
              this.LaunchChapterAdjuster(Path.GetFileNameWithoutExtension(this.overdriveFiles[index1].mp3Filename));
            ++num1;
          }
        }
      }
      else
      {
        if (!name.Equals("Chapter"))
          return;
        int num1 = 0;
        FormChapterAligner formChapterAligner = new FormChapterAligner();
        for (int index = 0; index < this.overdriveFiles.Count; ++index)
        {
          AdvancedSplitting.Chapters chapters = this.overdriveFiles[index].ReturnChapters();
          int num2 = chapters.Count();
          for (int pos = 0; pos < num2; ++pos)
          {
            if (num1 == e.RowIndex)
            {
              AdjustFile adjustFile = new AdjustFile();
              adjustFile.File = this.overdriveFiles[index].mp3Filename;
              adjustFile.Chapter = chapters.GetChapterDouble(pos);
              formChapterAligner.fileToAdjust = adjustFile;
              formChapterAligner.fileToAdjust.SearchRange = 30.0;
              int num3 = (int) formChapterAligner.ShowDialog();
              adjustFile.Cleanup();
              if (formChapterAligner.applied)
              {
                double offset = formChapterAligner.fileToAdjust.Offset;
                if (offset == 0.0)
                {
                  double chapter = adjustFile.Chapter;
                }
                else
                {
                  chapters.SetChapter(pos, offset);
                  this.dgvChapters.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightBlue;
                  this.dt.Rows[e.RowIndex]["Offset"] = (object) (offset - adjustFile.Chapter).ToString("0.###");
                }
              }
            }
            ++num1;
          }
        }
      }
    }

    private void btnSelectNonChapters_Click(object sender, EventArgs e)
    {
      for (int index = 0; index < this.dt.Rows.Count; ++index)
      {
        if (!this.dt.Rows[index]["Title"].ToString().ToLower().Contains("chapter") && this.dt.Rows[index]["Title"].ToString() != Overdrive.mergeText)
          this.dt.Rows[index][0] = (object) false;
      }
    }

    private delegate void SetTextCallback(string text);
  }
}
