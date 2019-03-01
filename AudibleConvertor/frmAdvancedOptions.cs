// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.frmAdvancedOptions
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using Ionic.Utils;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace AudibleConvertor
{
  public class frmAdvancedOptions : Form
  {
    public AdvancedOptions advancedOptions;
    private int clickCounter;
    private IContainer components;
    private Button btnApply;
    private CheckBox chkOverlapOverride;
    private GroupBox groupBox1;
    private RadioButton rdDecrypt;
    private RadioButton rdITunes;
    private TextBox txtAudibleManagerDLLPath;
    private Button btnBrowseDLL;
    private GroupBox groupBox2;
    private RadioButton rdDirectshow;
    private RadioButton rdAAAudibleManager;
    private CheckBox chkNFO;
    private CheckBox chkBeep;
    private GroupBox groupBox3;
    private RadioButton rdCompletionShutdown;
    private RadioButton rdCompletionSleep;
    private RadioButton rdCompletionHibernate;
    private RadioButton rdCompletionNothing;
    private TextBox txtOutputPath;
    private Button btnOutputPath;
    private CheckBox chkSHA256Checksum;
    private CheckBox chkCylon;
    private DataGridView dgvCodecOptions;
    private GroupBox groupBox4;
    private GroupBox groupBox5;
    private TextBox txtSplitSize;
    private TextBox txtSplitThreshold;
    private Label label3;
    private Label label2;
    private Label label1;
    private TextBox txtSplitMinSize;
    private CheckBox chkLegacyChapterSplitting;
    private CheckBox chkLowQualityPreview;
    private GroupBox groupBox6;
    private GroupBox groupBox7;
    private RadioButton rdNG;
    private GroupBox groupBox8;
    private RadioButton rdRipperCDDA2WAV;
    private RadioButton rdRipperCUEtools;
    private ComboBox cmbThreadPriority;
    private Label label4;
    private TextBox txtTempPath;
    private Button btnTempPath;
    private CheckBox chkTempFile;

    public frmAdvancedOptions()
    {
      this.InitializeComponent();
    }

    private void btnApply_Click(object sender, EventArgs e)
    {
      this.advancedOptions.overlapOverride = this.chkOverlapOverride.Checked;
      this.advancedOptions.decrypt = this.rdDecrypt.Checked;
      this.advancedOptions.ng = this.rdNG.Checked;
      if (this.advancedOptions.ng)
        this.advancedOptions.decrypt = true;
      this.advancedOptions.iTunesMode = this.rdITunes.Checked;
      this.advancedOptions.AudibleMangerDLLPath = this.txtAudibleManagerDLLPath.Text;
      this.advancedOptions.aaDirectShow = this.rdDirectshow.Checked;
      this.advancedOptions.nfo = this.chkNFO.Checked;
      this.advancedOptions.beep = this.chkBeep.Checked;
      this.advancedOptions.cylon = this.chkCylon.Checked;
      this.advancedOptions.SHA256Checksum = this.chkSHA256Checksum.Checked;
      this.advancedOptions.outputPath = this.txtOutputPath.Text;
      this.advancedOptions.SetTempPath(this.txtTempPath.Text);
      this.advancedOptions.iOSSplitThreshold = int.Parse(this.txtSplitThreshold.Text.Trim());
      this.advancedOptions.iOSSplitSize = int.Parse(this.txtSplitSize.Text.Trim());
      this.advancedOptions.iOSMinSplitSize = int.Parse(this.txtSplitMinSize.Text.Trim());
      this.advancedOptions.legacyChapterMode = this.chkLegacyChapterSplitting.Checked;
      this.advancedOptions.lowQualityPreview = this.chkLowQualityPreview.Checked;
      this.advancedOptions.chapterEditorTempFile = this.chkTempFile.Checked;
      this.advancedOptions.newRipper = this.rdRipperCUEtools.Checked;
      this.advancedOptions.threadPriority = this.cmbThreadPriority.Text;
      if (this.rdCompletionHibernate.Checked)
        this.advancedOptions.completion = "hibernate";
      if (this.rdCompletionNothing.Checked)
        this.advancedOptions.completion = "none";
      if (this.rdCompletionShutdown.Checked)
        this.advancedOptions.completion = "shutdown";
      if (this.rdCompletionSleep.Checked)
        this.advancedOptions.completion = "sleep";
      this.Close();
    }

    public void SetFields()
    {
      this.chkOverlapOverride.Checked = this.advancedOptions.overlapOverride;
      this.rdDecrypt.Checked = this.advancedOptions.decrypt;
      this.rdNG.Checked = this.advancedOptions.ng;
      this.rdITunes.Checked = this.advancedOptions.iTunesMode;
      this.txtAudibleManagerDLLPath.Text = this.advancedOptions.AudibleMangerDLLPath;
      this.rdDirectshow.Checked = this.advancedOptions.aaDirectShow;
      this.chkNFO.Checked = this.advancedOptions.nfo;
      this.chkLegacyChapterSplitting.Checked = this.advancedOptions.legacyChapterMode;
      this.chkBeep.Checked = this.advancedOptions.beep;
      this.chkCylon.Checked = this.advancedOptions.cylon;
      this.chkSHA256Checksum.Checked = this.advancedOptions.SHA256Checksum;
      this.txtOutputPath.Text = this.advancedOptions.outputPath;
      this.txtTempPath.Text = this.advancedOptions.GetTempPath();
      this.txtSplitThreshold.Text = this.advancedOptions.iOSSplitThreshold.ToString();
      this.txtSplitSize.Text = this.advancedOptions.iOSSplitSize.ToString();
      this.txtSplitMinSize.Text = this.advancedOptions.iOSMinSplitSize.ToString();
      this.chkLowQualityPreview.Checked = this.advancedOptions.lowQualityPreview;
      this.chkTempFile.Checked = this.advancedOptions.chapterEditorTempFile;
      this.cmbThreadPriority.Text = this.advancedOptions.threadPriority;
      if (this.advancedOptions.newRipper)
      {
        this.rdRipperCUEtools.Checked = true;
        this.rdRipperCDDA2WAV.Checked = false;
      }
      else
      {
        this.rdRipperCUEtools.Checked = false;
        this.rdRipperCDDA2WAV.Checked = true;
      }
      if (this.advancedOptions.completion == "hibernate")
        this.rdCompletionHibernate.Checked = true;
      if (this.advancedOptions.completion == "none")
        this.rdCompletionNothing.Checked = true;
      if (this.advancedOptions.completion == "shutdown")
        this.rdCompletionShutdown.Checked = true;
      if (this.advancedOptions.completion == "sleep")
        this.rdCompletionSleep.Checked = true;
      this.dgvCodecOptions.DataSource = (object) this.advancedOptions.codecOptions;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "DLL files (*.dll)|*.dll";
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;
      this.advancedOptions.AudibleMangerDLLPath = openFileDialog.FileName;
      this.txtAudibleManagerDLLPath.Text = this.advancedOptions.AudibleMangerDLLPath;
    }

    private bool CheckFDK(string ffmpegPath)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = ffmpegPath;
      process.StartInfo.Arguments = "-encoders";
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardInput = true;
      process.StartInfo.RedirectStandardError = true;
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.Start();
      string end = process.StandardOutput.ReadToEnd();
      process.WaitForExit();
      return end.Contains("libfdk_aac");
    }

    private void btnOutputPath_Click(object sender, EventArgs e)
    {
      FolderBrowserDialogEx folderBrowserDialogEx = new FolderBrowserDialogEx();
      folderBrowserDialogEx.Description = "Select target folder:";
      folderBrowserDialogEx.ShowNewFolderButton = true;
      folderBrowserDialogEx.ShowEditBox = true;
      if (this.txtOutputPath.Text.Trim() != "")
        folderBrowserDialogEx.SelectedPath = this.txtOutputPath.Text;
      folderBrowserDialogEx.ShowFullPathInEditBox = true;
      folderBrowserDialogEx.RootFolder = Environment.SpecialFolder.MyComputer;
      if (folderBrowserDialogEx.ShowDialog() != DialogResult.OK)
        return;
      this.txtOutputPath.Text = folderBrowserDialogEx.SelectedPath;
    }

    private void txtAudibleManagerDLLPath_MouseClick(object sender, MouseEventArgs e)
    {
      ++this.clickCounter;
      if (this.clickCounter <= 12)
        return;
      this.rdNG.Enabled = true;
      this.rdNG.Text = "inAudible-NG";
    }

    private void txtAudibleManagerDLLPath_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (this.clickCounter <= 12)
        return;
      this.rdNG.Enabled = true;
    }

    private void btnTempPath_Click(object sender, EventArgs e)
    {
      FolderBrowserDialogEx folderBrowserDialogEx = new FolderBrowserDialogEx();
      folderBrowserDialogEx.Description = "Select temp folder:";
      folderBrowserDialogEx.ShowNewFolderButton = true;
      folderBrowserDialogEx.ShowEditBox = true;
      if (this.txtTempPath.Text.Trim() != "")
        folderBrowserDialogEx.SelectedPath = this.txtTempPath.Text;
      folderBrowserDialogEx.ShowFullPathInEditBox = true;
      folderBrowserDialogEx.RootFolder = Environment.SpecialFolder.MyComputer;
      if (folderBrowserDialogEx.ShowDialog() != DialogResult.OK)
        return;
      this.txtTempPath.Text = folderBrowserDialogEx.SelectedPath;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmAdvancedOptions));
      this.btnApply = new Button();
      this.chkOverlapOverride = new CheckBox();
      this.groupBox1 = new GroupBox();
      this.rdNG = new RadioButton();
      this.txtAudibleManagerDLLPath = new TextBox();
      this.rdDecrypt = new RadioButton();
      this.btnBrowseDLL = new Button();
      this.rdITunes = new RadioButton();
      this.groupBox2 = new GroupBox();
      this.rdDirectshow = new RadioButton();
      this.rdAAAudibleManager = new RadioButton();
      this.chkNFO = new CheckBox();
      this.chkBeep = new CheckBox();
      this.groupBox3 = new GroupBox();
      this.rdCompletionShutdown = new RadioButton();
      this.rdCompletionSleep = new RadioButton();
      this.rdCompletionHibernate = new RadioButton();
      this.rdCompletionNothing = new RadioButton();
      this.txtOutputPath = new TextBox();
      this.btnOutputPath = new Button();
      this.chkSHA256Checksum = new CheckBox();
      this.chkCylon = new CheckBox();
      this.dgvCodecOptions = new DataGridView();
      this.groupBox4 = new GroupBox();
      this.groupBox5 = new GroupBox();
      this.label3 = new Label();
      this.label2 = new Label();
      this.label1 = new Label();
      this.txtSplitMinSize = new TextBox();
      this.txtSplitSize = new TextBox();
      this.txtSplitThreshold = new TextBox();
      this.chkLegacyChapterSplitting = new CheckBox();
      this.chkLowQualityPreview = new CheckBox();
      this.groupBox6 = new GroupBox();
      this.groupBox7 = new GroupBox();
      this.groupBox8 = new GroupBox();
      this.rdRipperCDDA2WAV = new RadioButton();
      this.rdRipperCUEtools = new RadioButton();
      this.cmbThreadPriority = new ComboBox();
      this.label4 = new Label();
      this.txtTempPath = new TextBox();
      this.btnTempPath = new Button();
      this.chkTempFile = new CheckBox();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      ((ISupportInitialize) this.dgvCodecOptions).BeginInit();
      this.groupBox4.SuspendLayout();
      this.groupBox5.SuspendLayout();
      this.groupBox6.SuspendLayout();
      this.groupBox7.SuspendLayout();
      this.groupBox8.SuspendLayout();
      this.SuspendLayout();
      this.btnApply.Location = new Point(283, 444);
      this.btnApply.Name = "btnApply";
      this.btnApply.Size = new Size(75, 23);
      this.btnApply.TabIndex = 2;
      this.btnApply.Text = "Apply";
      this.btnApply.UseVisualStyleBackColor = true;
      this.btnApply.Click += new EventHandler(this.btnApply_Click);
      this.chkOverlapOverride.AccessibleDescription = "Use slow but more reliable detection of overlaps in WAV files when ripping with iTunes";
      this.chkOverlapOverride.AutoSize = true;
      this.chkOverlapOverride.Location = new Point(5, 147);
      this.chkOverlapOverride.Name = "chkOverlapOverride";
      this.chkOverlapOverride.Size = new Size(137, 30);
      this.chkOverlapOverride.TabIndex = 3;
      this.chkOverlapOverride.Text = "Use brute-force overlap\r\ndetection";
      this.chkOverlapOverride.UseVisualStyleBackColor = true;
      this.groupBox1.Controls.Add((Control) this.rdNG);
      this.groupBox1.Controls.Add((Control) this.txtAudibleManagerDLLPath);
      this.groupBox1.Controls.Add((Control) this.rdDecrypt);
      this.groupBox1.Controls.Add((Control) this.btnBrowseDLL);
      this.groupBox1.Controls.Add((Control) this.rdITunes);
      this.groupBox1.Location = new Point(13, 181);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(424, 119);
      this.groupBox1.TabIndex = 4;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "AAX Mode";
      this.rdNG.AccessibleDescription = "Use the inAudible-NG decryption engine for AAX files";
      this.rdNG.AutoSize = true;
      this.rdNG.Location = new Point(6, 43);
      this.rdNG.Name = "rdNG";
      this.rdNG.Size = new Size(87, 17);
      this.rdNG.TabIndex = 7;
      this.rdNG.Text = "inAudible-NG";
      this.rdNG.UseVisualStyleBackColor = true;
      this.txtAudibleManagerDLLPath.Location = new Point(135, 91);
      this.txtAudibleManagerDLLPath.Name = "txtAudibleManagerDLLPath";
      this.txtAudibleManagerDLLPath.Size = new Size(279, 20);
      this.txtAudibleManagerDLLPath.TabIndex = 6;
      this.txtAudibleManagerDLLPath.MouseClick += new MouseEventHandler(this.txtAudibleManagerDLLPath_MouseClick);
      this.txtAudibleManagerDLLPath.MouseDoubleClick += new MouseEventHandler(this.txtAudibleManagerDLLPath_MouseDoubleClick);
      this.rdDecrypt.AccessibleDescription = "Using Audible's Audible Manger software to decrypt AAX files";
      this.rdDecrypt.AutoSize = true;
      this.rdDecrypt.Checked = true;
      this.rdDecrypt.Location = new Point(6, 66);
      this.rdDecrypt.Name = "rdDecrypt";
      this.rdDecrypt.Size = new Size(159, 17);
      this.rdDecrypt.TabIndex = 1;
      this.rdDecrypt.TabStop = true;
      this.rdDecrypt.Text = "Audible Manager Decryption";
      this.rdDecrypt.UseVisualStyleBackColor = true;
      this.btnBrowseDLL.Location = new Point(2, 89);
      this.btnBrowseDLL.Name = "btnBrowseDLL";
      this.btnBrowseDLL.Size = new Size(126, 23);
      this.btnBrowseDLL.TabIndex = 5;
      this.btnBrowseDLL.Text = "Audible Manager DLL";
      this.btnBrowseDLL.UseVisualStyleBackColor = true;
      this.btnBrowseDLL.Click += new EventHandler(this.button1_Click);
      this.rdITunes.AccessibleDescription = "Use iTunes and Virtual CD to decrypt AAX files";
      this.rdITunes.AutoSize = true;
      this.rdITunes.Location = new Point(7, 20);
      this.rdITunes.Name = "rdITunes";
      this.rdITunes.Size = new Size(119, 17);
      this.rdITunes.TabIndex = 0;
      this.rdITunes.Text = "iTunes -> Virtual CD";
      this.rdITunes.UseVisualStyleBackColor = true;
      this.groupBox2.Controls.Add((Control) this.rdDirectshow);
      this.groupBox2.Controls.Add((Control) this.rdAAAudibleManager);
      this.groupBox2.Location = new Point(13, 101);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new Size(424, 74);
      this.groupBox2.TabIndex = 9;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "AA Mode";
      this.rdDirectshow.AccessibleDescription = "Use old directshow filter to enable decryption of AA files";
      this.rdDirectshow.AutoSize = true;
      this.rdDirectshow.Location = new Point(6, 19);
      this.rdDirectshow.Name = "rdDirectshow";
      this.rdDirectshow.Size = new Size(103, 17);
      this.rdDirectshow.TabIndex = 7;
      this.rdDirectshow.Text = "Directshow Filter";
      this.rdDirectshow.UseVisualStyleBackColor = true;
      this.rdAAAudibleManager.AccessibleDescription = "Use Audible Manager or inAudible-NG to decrypt AA files";
      this.rdAAAudibleManager.AutoSize = true;
      this.rdAAAudibleManager.Checked = true;
      this.rdAAAudibleManager.Location = new Point(6, 42);
      this.rdAAAudibleManager.Name = "rdAAAudibleManager";
      this.rdAAAudibleManager.Size = new Size(159, 17);
      this.rdAAAudibleManager.TabIndex = 7;
      this.rdAAAudibleManager.TabStop = true;
      this.rdAAAudibleManager.Text = "Audible Manager Decryption";
      this.rdAAAudibleManager.UseVisualStyleBackColor = true;
      this.chkNFO.AccessibleDescription = "Create an NFO file with details about the rip";
      this.chkNFO.AutoSize = true;
      this.chkNFO.Location = new Point(6, 19);
      this.chkNFO.Name = "chkNFO";
      this.chkNFO.Size = new Size(146, 17);
      this.chkNFO.TabIndex = 10;
      this.chkNFO.Text = "Automatically create NFO";
      this.chkNFO.UseVisualStyleBackColor = true;
      this.chkBeep.AccessibleDescription = "Play some music to signify that the rip is complete";
      this.chkBeep.AutoSize = true;
      this.chkBeep.Location = new Point(5, 42);
      this.chkBeep.Name = "chkBeep";
      this.chkBeep.Size = new Size(130, 17);
      this.chkBeep.TabIndex = 11;
      this.chkBeep.Text = "\"Beep\" on completion";
      this.chkBeep.UseVisualStyleBackColor = true;
      this.groupBox3.Controls.Add((Control) this.rdCompletionShutdown);
      this.groupBox3.Controls.Add((Control) this.rdCompletionSleep);
      this.groupBox3.Controls.Add((Control) this.rdCompletionHibernate);
      this.groupBox3.Controls.Add((Control) this.rdCompletionNothing);
      this.groupBox3.Location = new Point(12, 306);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new Size(425, 49);
      this.groupBox3.TabIndex = 12;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "On Completion:";
      this.rdCompletionShutdown.AccessibleDescription = "Shutdown PC after ripping is complete";
      this.rdCompletionShutdown.AutoSize = true;
      this.rdCompletionShutdown.Location = new Point(324, 19);
      this.rdCompletionShutdown.Name = "rdCompletionShutdown";
      this.rdCompletionShutdown.Size = new Size(73, 17);
      this.rdCompletionShutdown.TabIndex = 3;
      this.rdCompletionShutdown.Text = "Shutdown";
      this.rdCompletionShutdown.UseVisualStyleBackColor = true;
      this.rdCompletionSleep.AccessibleDescription = "Sleep when ripping is completed.";
      this.rdCompletionSleep.AutoSize = true;
      this.rdCompletionSleep.Location = new Point((int) sbyte.MaxValue, 19);
      this.rdCompletionSleep.Name = "rdCompletionSleep";
      this.rdCompletionSleep.Size = new Size(52, 17);
      this.rdCompletionSleep.TabIndex = 2;
      this.rdCompletionSleep.Text = "Sleep";
      this.rdCompletionSleep.UseVisualStyleBackColor = true;
      this.rdCompletionHibernate.AccessibleDescription = "Go into hibernation mode after ripping is complete";
      this.rdCompletionHibernate.AutoSize = true;
      this.rdCompletionHibernate.Location = new Point(215, 19);
      this.rdCompletionHibernate.Name = "rdCompletionHibernate";
      this.rdCompletionHibernate.Size = new Size(71, 17);
      this.rdCompletionHibernate.TabIndex = 1;
      this.rdCompletionHibernate.Text = "Hibernate";
      this.rdCompletionHibernate.UseVisualStyleBackColor = true;
      this.rdCompletionNothing.AccessibleDescription = "When ripping is complete, do nothing.";
      this.rdCompletionNothing.AutoSize = true;
      this.rdCompletionNothing.Checked = true;
      this.rdCompletionNothing.Location = new Point(18, 19);
      this.rdCompletionNothing.Name = "rdCompletionNothing";
      this.rdCompletionNothing.Size = new Size(77, 17);
      this.rdCompletionNothing.TabIndex = 0;
      this.rdCompletionNothing.TabStop = true;
      this.rdCompletionNothing.Text = "Do nothing";
      this.rdCompletionNothing.UseVisualStyleBackColor = true;
      this.txtOutputPath.AccessibleDescription = "The default path to save rips to";
      this.txtOutputPath.Location = new Point(101, 364);
      this.txtOutputPath.Name = "txtOutputPath";
      this.txtOutputPath.Size = new Size(325, 20);
      this.txtOutputPath.TabIndex = 14;
      this.btnOutputPath.Location = new Point(14, 361);
      this.btnOutputPath.Name = "btnOutputPath";
      this.btnOutputPath.Size = new Size(81, 23);
      this.btnOutputPath.TabIndex = 13;
      this.btnOutputPath.Text = "Output Path";
      this.btnOutputPath.UseVisualStyleBackColor = true;
      this.btnOutputPath.Click += new EventHandler(this.btnOutputPath_Click);
      this.chkSHA256Checksum.AccessibleDescription = "Calculate a checksum of the decrypted AAC file.";
      this.chkSHA256Checksum.AutoSize = true;
      this.chkSHA256Checksum.Location = new Point(5, 111);
      this.chkSHA256Checksum.Name = "chkSHA256Checksum";
      this.chkSHA256Checksum.Size = new Size(165, 30);
      this.chkSHA256Checksum.TabIndex = 15;
      this.chkSHA256Checksum.Text = "Calculate SHA256 checksum\r\non decrypted AAX ";
      this.chkSHA256Checksum.UseVisualStyleBackColor = true;
      this.chkCylon.AccessibleDescription = "Play some sounds at the beginning and end of the rip";
      this.chkCylon.AutoSize = true;
      this.chkCylon.Location = new Point(6, 65);
      this.chkCylon.Name = "chkCylon";
      this.chkCylon.Size = new Size(52, 17);
      this.chkCylon.TabIndex = 16;
      this.chkCylon.Text = "Cylon";
      this.chkCylon.UseVisualStyleBackColor = true;
      this.dgvCodecOptions.AllowUserToAddRows = false;
      this.dgvCodecOptions.AllowUserToDeleteRows = false;
      this.dgvCodecOptions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvCodecOptions.Location = new Point(8, 19);
      this.dgvCodecOptions.Name = "dgvCodecOptions";
      this.dgvCodecOptions.Size = new Size(407, 58);
      this.dgvCodecOptions.TabIndex = 17;
      this.groupBox4.Controls.Add((Control) this.dgvCodecOptions);
      this.groupBox4.Location = new Point(12, 12);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new Size(424, 83);
      this.groupBox4.TabIndex = 18;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "Custom Codec Switches";
      this.groupBox5.Controls.Add((Control) this.label3);
      this.groupBox5.Controls.Add((Control) this.label2);
      this.groupBox5.Controls.Add((Control) this.label1);
      this.groupBox5.Controls.Add((Control) this.txtSplitMinSize);
      this.groupBox5.Controls.Add((Control) this.txtSplitSize);
      this.groupBox5.Controls.Add((Control) this.txtSplitThreshold);
      this.groupBox5.Location = new Point(443, 11);
      this.groupBox5.Name = "groupBox5";
      this.groupBox5.Size = new Size(184, 110);
      this.groupBox5.TabIndex = 19;
      this.groupBox5.TabStop = false;
      this.groupBox5.Text = "M4B Splitting (in hours)";
      this.label3.AutoSize = true;
      this.label3.Location = new Point(11, 75);
      this.label3.Name = "label3";
      this.label3.Size = new Size(93, 13);
      this.label3.TabIndex = 5;
      this.label3.Text = "Minimum split size:";
      this.label2.AutoSize = true;
      this.label2.Location = new Point(41, 47);
      this.label2.Name = "label2";
      this.label2.Size = new Size(63, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Size of split:";
      this.label1.AutoSize = true;
      this.label1.Location = new Point(6, 24);
      this.label1.Name = "label1";
      this.label1.Size = new Size(98, 13);
      this.label1.TabIndex = 3;
      this.label1.Text = "Split if greater than:";
      this.txtSplitMinSize.AccessibleDescription = "Create no M4B's shorter than this value in hours";
      this.txtSplitMinSize.Location = new Point(110, 72);
      this.txtSplitMinSize.Name = "txtSplitMinSize";
      this.txtSplitMinSize.Size = new Size(25, 20);
      this.txtSplitMinSize.TabIndex = 2;
      this.txtSplitMinSize.Text = "2";
      this.txtSplitSize.AccessibleDescription = "The split M4B's will be no longer than this value in hours";
      this.txtSplitSize.Location = new Point(110, 46);
      this.txtSplitSize.Name = "txtSplitSize";
      this.txtSplitSize.Size = new Size(25, 20);
      this.txtSplitSize.TabIndex = 1;
      this.txtSplitSize.Text = "6";
      this.txtSplitThreshold.AccessibleDescription = "Split the M4B if it longer than this value in hours";
      this.txtSplitThreshold.Location = new Point(110, 21);
      this.txtSplitThreshold.Name = "txtSplitThreshold";
      this.txtSplitThreshold.Size = new Size(25, 20);
      this.txtSplitThreshold.TabIndex = 0;
      this.txtSplitThreshold.Text = "8";
      this.chkLegacyChapterSplitting.AccessibleDescription = "Enable this if splitting does not work on your system";
      this.chkLegacyChapterSplitting.AutoSize = true;
      this.chkLegacyChapterSplitting.Location = new Point(5, 88);
      this.chkLegacyChapterSplitting.Name = "chkLegacyChapterSplitting";
      this.chkLegacyChapterSplitting.Size = new Size(129, 17);
      this.chkLegacyChapterSplitting.TabIndex = 20;
      this.chkLegacyChapterSplitting.Text = "Legacy chapter mode";
      this.chkLegacyChapterSplitting.UseVisualStyleBackColor = true;
      this.chkLowQualityPreview.AccessibleDescription = "When using the editor, downsample the source file for quicker load times and to support large files";
      this.chkLowQualityPreview.AutoSize = true;
      this.chkLowQualityPreview.Checked = true;
      this.chkLowQualityPreview.CheckState = CheckState.Checked;
      this.chkLowQualityPreview.Location = new Point(9, 19);
      this.chkLowQualityPreview.Name = "chkLowQualityPreview";
      this.chkLowQualityPreview.Size = new Size(119, 17);
      this.chkLowQualityPreview.TabIndex = 21;
      this.chkLowQualityPreview.Text = "Low quality preview";
      this.chkLowQualityPreview.UseVisualStyleBackColor = true;
      this.groupBox6.Controls.Add((Control) this.chkTempFile);
      this.groupBox6.Controls.Add((Control) this.chkLowQualityPreview);
      this.groupBox6.Location = new Point(443, (int) sbyte.MaxValue);
      this.groupBox6.Name = "groupBox6";
      this.groupBox6.Size = new Size(184, 68);
      this.groupBox6.TabIndex = 22;
      this.groupBox6.TabStop = false;
      this.groupBox6.Text = "Editor";
      this.groupBox7.Controls.Add((Control) this.chkNFO);
      this.groupBox7.Controls.Add((Control) this.chkBeep);
      this.groupBox7.Controls.Add((Control) this.chkLegacyChapterSplitting);
      this.groupBox7.Controls.Add((Control) this.chkCylon);
      this.groupBox7.Controls.Add((Control) this.chkSHA256Checksum);
      this.groupBox7.Controls.Add((Control) this.chkOverlapOverride);
      this.groupBox7.Location = new Point(443, 274);
      this.groupBox7.Name = "groupBox7";
      this.groupBox7.Size = new Size(184, 189);
      this.groupBox7.TabIndex = 23;
      this.groupBox7.TabStop = false;
      this.groupBox7.Text = "Misc";
      this.groupBox8.Controls.Add((Control) this.rdRipperCDDA2WAV);
      this.groupBox8.Controls.Add((Control) this.rdRipperCUEtools);
      this.groupBox8.Location = new Point(444, 201);
      this.groupBox8.Name = "groupBox8";
      this.groupBox8.Size = new Size(183, 67);
      this.groupBox8.TabIndex = 24;
      this.groupBox8.TabStop = false;
      this.groupBox8.Text = "CD Ripper";
      this.rdRipperCDDA2WAV.AccessibleDescription = "Use CDDA2WAV ripping engine";
      this.rdRipperCDDA2WAV.AutoSize = true;
      this.rdRipperCDDA2WAV.Location = new Point(8, 43);
      this.rdRipperCDDA2WAV.Name = "rdRipperCDDA2WAV";
      this.rdRipperCDDA2WAV.Size = new Size(86, 17);
      this.rdRipperCDDA2WAV.TabIndex = 1;
      this.rdRipperCDDA2WAV.Text = "CDDA2WAV";
      this.rdRipperCDDA2WAV.UseVisualStyleBackColor = true;
      this.rdRipperCUEtools.AccessibleDescription = "Use CUE Tools CD ripping engine";
      this.rdRipperCUEtools.AutoSize = true;
      this.rdRipperCUEtools.Checked = true;
      this.rdRipperCUEtools.Location = new Point(8, 20);
      this.rdRipperCUEtools.Name = "rdRipperCUEtools";
      this.rdRipperCUEtools.Size = new Size(76, 17);
      this.rdRipperCUEtools.TabIndex = 0;
      this.rdRipperCUEtools.TabStop = true;
      this.rdRipperCUEtools.Text = "CUE Tools";
      this.rdRipperCUEtools.UseVisualStyleBackColor = true;
      this.cmbThreadPriority.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbThreadPriority.FormattingEnabled = true;
      this.cmbThreadPriority.Items.AddRange(new object[3]
      {
        (object) "Normal",
        (object) "Below Normal",
        (object) "Low"
      });
      this.cmbThreadPriority.Location = new Point(102, 419);
      this.cmbThreadPriority.Name = "cmbThreadPriority";
      this.cmbThreadPriority.Size = new Size(121, 21);
      this.cmbThreadPriority.TabIndex = 25;
      this.label4.AutoSize = true;
      this.label4.Location = new Point(17, 422);
      this.label4.Name = "label4";
      this.label4.Size = new Size(78, 13);
      this.label4.TabIndex = 26;
      this.label4.Text = "Thread Priority:";
      this.txtTempPath.AccessibleDescription = "The default path to save rips to";
      this.txtTempPath.Location = new Point(101, 393);
      this.txtTempPath.Name = "txtTempPath";
      this.txtTempPath.Size = new Size(325, 20);
      this.txtTempPath.TabIndex = 28;
      this.btnTempPath.Location = new Point(14, 390);
      this.btnTempPath.Name = "btnTempPath";
      this.btnTempPath.Size = new Size(81, 23);
      this.btnTempPath.TabIndex = 27;
      this.btnTempPath.Text = "Temp Path";
      this.btnTempPath.UseVisualStyleBackColor = true;
      this.btnTempPath.Click += new EventHandler(this.btnTempPath_Click);
      this.chkTempFile.AccessibleDescription = "When using the editor, downsample the source file for quicker load times and to support large files";
      this.chkTempFile.AutoSize = true;
      this.chkTempFile.Checked = true;
      this.chkTempFile.CheckState = CheckState.Checked;
      this.chkTempFile.Location = new Point(9, 42);
      this.chkTempFile.Name = "chkTempFile";
      this.chkTempFile.Size = new Size(87, 17);
      this.chkTempFile.TabIndex = 22;
      this.chkTempFile.Text = "Use temp file";
      this.chkTempFile.UseVisualStyleBackColor = true;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(639, 475);
      this.Controls.Add((Control) this.txtTempPath);
      this.Controls.Add((Control) this.btnTempPath);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.cmbThreadPriority);
      this.Controls.Add((Control) this.groupBox8);
      this.Controls.Add((Control) this.groupBox7);
      this.Controls.Add((Control) this.groupBox6);
      this.Controls.Add((Control) this.groupBox5);
      this.Controls.Add((Control) this.groupBox4);
      this.Controls.Add((Control) this.txtOutputPath);
      this.Controls.Add((Control) this.btnOutputPath);
      this.Controls.Add((Control) this.groupBox3);
      this.Controls.Add((Control) this.groupBox2);
      this.Controls.Add((Control) this.groupBox1);
      this.Controls.Add((Control) this.btnApply);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (frmAdvancedOptions);
      this.Text = "Advanced Options";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      ((ISupportInitialize) this.dgvCodecOptions).EndInit();
      this.groupBox4.ResumeLayout(false);
      this.groupBox5.ResumeLayout(false);
      this.groupBox5.PerformLayout();
      this.groupBox6.ResumeLayout(false);
      this.groupBox6.PerformLayout();
      this.groupBox7.ResumeLayout(false);
      this.groupBox7.PerformLayout();
      this.groupBox8.ResumeLayout(false);
      this.groupBox8.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
