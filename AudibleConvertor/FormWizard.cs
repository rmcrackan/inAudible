// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.FormWizard
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using AeroWizard;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using TagLib;

namespace AudibleConvertor
{
  public class FormWizard : Form
  {
    private Audible myAudible = new Audible();
    private AdvancedSplitting.Chapters myChapters = new AdvancedSplitting.Chapters();
    private SupportLibraries supportLibs = new SupportLibraries();
    private VirtualWAV myVW = new VirtualWAV();
    public WizardOptions myWizardOptions = new WizardOptions();
    private IContainer components;
    private WizardControl wizardControl1;
    private WizardPage wpIntro;
    private WizardPage wpSelectFile;
    private System.Windows.Forms.TextBox txtIntroText;
    private Button btnSourceFile;
    private System.Windows.Forms.TextBox txtInputFile;
    private OpenFileDialog openFileDialog1;
    private WizardPage wpSettings;
    private WizardPage wpSummary;
    private GroupBox grpFileInfo;
    private System.Windows.Forms.TextBox txtComments;
    private Label lblComments;
    private System.Windows.Forms.TextBox txtYear;
    private System.Windows.Forms.TextBox txtAuthor;
    private System.Windows.Forms.TextBox txtTitle;
    private Label lblYear;
    private Label lblAuthor;
    private Label lblTitle;
    private PictureBox pictureBox1;
    private GroupBox grpQuality;
    private System.Windows.Forms.TextBox txtChannels;
    private System.Windows.Forms.TextBox txtSampleRate;
    private System.Windows.Forms.TextBox txtBitrate;
    private System.Windows.Forms.TextBox txtFormat;
    private Label lblChannels;
    private Label lblSampleRate;
    private Label lblBitrate;
    private Label lblFormat;
    private WizardPage wpChapters;
    private System.Windows.Forms.TextBox txtChapterCount;
    private System.Windows.Forms.TextBox txtDuration;
    private Label lblChapterCount;
    private Label lblDuration;
    private CheckBox chkCreateCUE;
    private Label lblSplitByChapters;
    private RadioButton rdSplitNo;
    private RadioButton rdSplitYes;
    private System.Windows.Forms.TextBox txtChapterInstructions;
    private CheckBox chkChapterNumberPosition;
    private System.Windows.Forms.TextBox txtEncodingInstructions;
    private Label lblOutputCodec;
    private ComboBox cmbCodec;
    private Label lblOutputSampleRate;
    private ComboBox cmbChannels;
    private ComboBox cmbBitrate;
    private Label lblOutputChannels;
    private Label lblOutputBitrate;
    private ComboBox cmbSampleRate;
    private CheckBox chkBeginConversion;
    private System.Windows.Forms.TextBox txtSummary;
    private Button btnOutputFile;
    private System.Windows.Forms.TextBox txtOutputFile;
    private SaveFileDialog saveFileDialog1;
    private GroupBox grpOutputFolder;
    private CheckBox chkAutoTitle;
    private CheckBox chkTitle;
    private CheckBox chkAuthor;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormWizard));
      this.wizardControl1 = new WizardControl();
      this.wpIntro = new WizardPage();
      this.txtIntroText = new System.Windows.Forms.TextBox();
      this.wpSelectFile = new WizardPage();
      this.grpQuality = new GroupBox();
      this.txtChapterCount = new System.Windows.Forms.TextBox();
      this.txtDuration = new System.Windows.Forms.TextBox();
      this.lblChapterCount = new Label();
      this.lblDuration = new Label();
      this.txtChannels = new System.Windows.Forms.TextBox();
      this.txtSampleRate = new System.Windows.Forms.TextBox();
      this.txtBitrate = new System.Windows.Forms.TextBox();
      this.txtFormat = new System.Windows.Forms.TextBox();
      this.lblChannels = new Label();
      this.lblSampleRate = new Label();
      this.lblBitrate = new Label();
      this.lblFormat = new Label();
      this.grpFileInfo = new GroupBox();
      this.pictureBox1 = new PictureBox();
      this.txtComments = new System.Windows.Forms.TextBox();
      this.lblComments = new Label();
      this.txtYear = new System.Windows.Forms.TextBox();
      this.txtAuthor = new System.Windows.Forms.TextBox();
      this.txtTitle = new System.Windows.Forms.TextBox();
      this.lblYear = new Label();
      this.lblAuthor = new Label();
      this.lblTitle = new Label();
      this.btnSourceFile = new Button();
      this.txtInputFile = new System.Windows.Forms.TextBox();
      this.wpChapters = new WizardPage();
      this.chkChapterNumberPosition = new CheckBox();
      this.chkCreateCUE = new CheckBox();
      this.lblSplitByChapters = new Label();
      this.rdSplitNo = new RadioButton();
      this.rdSplitYes = new RadioButton();
      this.txtChapterInstructions = new System.Windows.Forms.TextBox();
      this.wpSettings = new WizardPage();
      this.cmbChannels = new ComboBox();
      this.cmbBitrate = new ComboBox();
      this.lblOutputChannels = new Label();
      this.lblOutputBitrate = new Label();
      this.cmbSampleRate = new ComboBox();
      this.lblOutputSampleRate = new Label();
      this.cmbCodec = new ComboBox();
      this.lblOutputCodec = new Label();
      this.txtEncodingInstructions = new System.Windows.Forms.TextBox();
      this.wpSummary = new WizardPage();
      this.grpOutputFolder = new GroupBox();
      this.chkAutoTitle = new CheckBox();
      this.chkTitle = new CheckBox();
      this.chkAuthor = new CheckBox();
      this.btnOutputFile = new Button();
      this.txtOutputFile = new System.Windows.Forms.TextBox();
      this.chkBeginConversion = new CheckBox();
      this.txtSummary = new System.Windows.Forms.TextBox();
      this.openFileDialog1 = new OpenFileDialog();
      this.saveFileDialog1 = new SaveFileDialog();
      this.wizardControl1.BeginInit();
      this.wpIntro.SuspendLayout();
      this.wpSelectFile.SuspendLayout();
      this.grpQuality.SuspendLayout();
      this.grpFileInfo.SuspendLayout();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.wpChapters.SuspendLayout();
      this.wpSettings.SuspendLayout();
      this.wpSummary.SuspendLayout();
      this.grpOutputFolder.SuspendLayout();
      this.SuspendLayout();
      this.wizardControl1.BackColor = Color.White;
      this.wizardControl1.Dock = DockStyle.Fill;
      this.wizardControl1.Font = new Font("Segoe UI", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.wizardControl1.Location = new Point(0, 0);
      this.wizardControl1.Name = "wizardControl1";
      this.wizardControl1.Pages.Add(this.wpIntro);
      this.wizardControl1.Pages.Add(this.wpSelectFile);
      this.wizardControl1.Pages.Add(this.wpChapters);
      this.wizardControl1.Pages.Add(this.wpSettings);
      this.wizardControl1.Pages.Add(this.wpSummary);
      this.wizardControl1.Size = new Size(598, 551);
      this.wizardControl1.TabIndex = 0;
      this.wizardControl1.Text = "Conversion Wizard";
      this.wizardControl1.Title = "Conversion Wizard";
      this.wizardControl1.Finished += new EventHandler(this.wizardControl1_Finished);
      this.wpIntro.Controls.Add((Control) this.txtIntroText);
      this.wpIntro.Name = "wpIntro";
      this.wpIntro.NextPage = this.wpSelectFile;
      this.wpIntro.Size = new Size(551, 397);
      this.wpIntro.TabIndex = 0;
      this.wpIntro.Text = "Introduction";
      this.txtIntroText.Dock = DockStyle.Fill;
      this.txtIntroText.Location = new Point(0, 0);
      this.txtIntroText.Multiline = true;
      this.txtIntroText.Name = "txtIntroText";
      this.txtIntroText.ReadOnly = true;
      this.txtIntroText.Size = new Size(551, 397);
      this.txtIntroText.TabIndex = 1;
      this.txtIntroText.TabStop = false;
      this.txtIntroText.Text = "Welcome to the inAudible conversion wizard.\r\n{first_time}\r\nThis tool will walk you through the steps required to strip the DRM from your AAX or AA file and/or convert it to a different format.";
      this.wpSelectFile.Controls.Add((Control) this.grpQuality);
      this.wpSelectFile.Controls.Add((Control) this.grpFileInfo);
      this.wpSelectFile.Controls.Add((Control) this.btnSourceFile);
      this.wpSelectFile.Controls.Add((Control) this.txtInputFile);
      this.wpSelectFile.Name = "wpSelectFile";
      this.wpSelectFile.NextPage = this.wpChapters;
      this.wpSelectFile.ShowNext = false;
      this.wpSelectFile.Size = new Size(551, 397);
      this.wpSelectFile.TabIndex = 1;
      this.wpSelectFile.Text = "Select File";
      this.grpQuality.Controls.Add((Control) this.txtChapterCount);
      this.grpQuality.Controls.Add((Control) this.txtDuration);
      this.grpQuality.Controls.Add((Control) this.lblChapterCount);
      this.grpQuality.Controls.Add((Control) this.lblDuration);
      this.grpQuality.Controls.Add((Control) this.txtChannels);
      this.grpQuality.Controls.Add((Control) this.txtSampleRate);
      this.grpQuality.Controls.Add((Control) this.txtBitrate);
      this.grpQuality.Controls.Add((Control) this.txtFormat);
      this.grpQuality.Controls.Add((Control) this.lblChannels);
      this.grpQuality.Controls.Add((Control) this.lblSampleRate);
      this.grpQuality.Controls.Add((Control) this.lblBitrate);
      this.grpQuality.Controls.Add((Control) this.lblFormat);
      this.grpQuality.Location = new Point(3, (int) byte.MaxValue);
      this.grpQuality.Name = "grpQuality";
      this.grpQuality.Size = new Size(545, 139);
      this.grpQuality.TabIndex = 3;
      this.grpQuality.TabStop = false;
      this.grpQuality.Text = "Quality Information";
      this.grpQuality.Visible = false;
      this.txtChapterCount.Location = new Point(246, 51);
      this.txtChapterCount.Name = "txtChapterCount";
      this.txtChapterCount.ReadOnly = true;
      this.txtChapterCount.Size = new Size(100, 23);
      this.txtChapterCount.TabIndex = 11;
      this.txtDuration.Location = new Point(246, 22);
      this.txtDuration.Name = "txtDuration";
      this.txtDuration.ReadOnly = true;
      this.txtDuration.Size = new Size(100, 23);
      this.txtDuration.TabIndex = 10;
      this.lblChapterCount.AutoSize = true;
      this.lblChapterCount.Location = new Point(187, 54);
      this.lblChapterCount.Name = "lblChapterCount";
      this.lblChapterCount.Size = new Size(54, 15);
      this.lblChapterCount.TabIndex = 9;
      this.lblChapterCount.Text = "Chapters";
      this.lblDuration.AutoSize = true;
      this.lblDuration.Location = new Point(187, 25);
      this.lblDuration.Name = "lblDuration";
      this.lblDuration.Size = new Size(53, 15);
      this.lblDuration.TabIndex = 8;
      this.lblDuration.Text = "Duration";
      this.txtChannels.Location = new Point(81, 109);
      this.txtChannels.Name = "txtChannels";
      this.txtChannels.ReadOnly = true;
      this.txtChannels.Size = new Size(100, 23);
      this.txtChannels.TabIndex = 7;
      this.txtSampleRate.Location = new Point(81, 80);
      this.txtSampleRate.Name = "txtSampleRate";
      this.txtSampleRate.ReadOnly = true;
      this.txtSampleRate.Size = new Size(100, 23);
      this.txtSampleRate.TabIndex = 6;
      this.txtBitrate.Location = new Point(81, 51);
      this.txtBitrate.Name = "txtBitrate";
      this.txtBitrate.ReadOnly = true;
      this.txtBitrate.Size = new Size(100, 23);
      this.txtBitrate.TabIndex = 5;
      this.txtFormat.Location = new Point(81, 22);
      this.txtFormat.Name = "txtFormat";
      this.txtFormat.ReadOnly = true;
      this.txtFormat.Size = new Size(100, 23);
      this.txtFormat.TabIndex = 4;
      this.lblChannels.AutoSize = true;
      this.lblChannels.Location = new Point(14, 112);
      this.lblChannels.Name = "lblChannels";
      this.lblChannels.Size = new Size(56, 15);
      this.lblChannels.TabIndex = 3;
      this.lblChannels.Text = "Channels";
      this.lblSampleRate.AutoSize = true;
      this.lblSampleRate.Location = new Point(3, 83);
      this.lblSampleRate.Name = "lblSampleRate";
      this.lblSampleRate.Size = new Size(72, 15);
      this.lblSampleRate.TabIndex = 2;
      this.lblSampleRate.Text = "Sample Rate";
      this.lblBitrate.AutoSize = true;
      this.lblBitrate.Location = new Point(29, 54);
      this.lblBitrate.Name = "lblBitrate";
      this.lblBitrate.Size = new Size(41, 15);
      this.lblBitrate.TabIndex = 1;
      this.lblBitrate.Text = "Bitrate";
      this.lblFormat.AutoSize = true;
      this.lblFormat.Location = new Point(25, 25);
      this.lblFormat.Name = "lblFormat";
      this.lblFormat.Size = new Size(45, 15);
      this.lblFormat.TabIndex = 0;
      this.lblFormat.Text = "Format";
      this.grpFileInfo.Controls.Add((Control) this.pictureBox1);
      this.grpFileInfo.Controls.Add((Control) this.txtComments);
      this.grpFileInfo.Controls.Add((Control) this.lblComments);
      this.grpFileInfo.Controls.Add((Control) this.txtYear);
      this.grpFileInfo.Controls.Add((Control) this.txtAuthor);
      this.grpFileInfo.Controls.Add((Control) this.txtTitle);
      this.grpFileInfo.Controls.Add((Control) this.lblYear);
      this.grpFileInfo.Controls.Add((Control) this.lblAuthor);
      this.grpFileInfo.Controls.Add((Control) this.lblTitle);
      this.grpFileInfo.Location = new Point(3, 32);
      this.grpFileInfo.Name = "grpFileInfo";
      this.grpFileInfo.Size = new Size(545, 217);
      this.grpFileInfo.TabIndex = 2;
      this.grpFileInfo.TabStop = false;
      this.grpFileInfo.Text = "File Information";
      this.grpFileInfo.Visible = false;
      this.pictureBox1.Location = new Point(458, 20);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(81, 74);
      this.pictureBox1.TabIndex = 8;
      this.pictureBox1.TabStop = false;
      this.txtComments.Location = new Point(81, 100);
      this.txtComments.Multiline = true;
      this.txtComments.Name = "txtComments";
      this.txtComments.ReadOnly = true;
      this.txtComments.ScrollBars = ScrollBars.Vertical;
      this.txtComments.Size = new Size(458, 111);
      this.txtComments.TabIndex = 7;
      this.lblComments.AutoSize = true;
      this.lblComments.Location = new Point(6, 91);
      this.lblComments.Name = "lblComments";
      this.lblComments.Size = new Size(66, 15);
      this.lblComments.TabIndex = 6;
      this.lblComments.Text = "Comments";
      this.txtYear.Location = new Point(81, 71);
      this.txtYear.Name = "txtYear";
      this.txtYear.ReadOnly = true;
      this.txtYear.Size = new Size(81, 23);
      this.txtYear.TabIndex = 5;
      this.txtAuthor.Location = new Point(81, 46);
      this.txtAuthor.Name = "txtAuthor";
      this.txtAuthor.ReadOnly = true;
      this.txtAuthor.Size = new Size(361, 23);
      this.txtAuthor.TabIndex = 4;
      this.txtTitle.Location = new Point(81, 20);
      this.txtTitle.Name = "txtTitle";
      this.txtTitle.ReadOnly = true;
      this.txtTitle.Size = new Size(361, 23);
      this.txtTitle.TabIndex = 3;
      this.lblYear.AutoSize = true;
      this.lblYear.Location = new Point(7, 71);
      this.lblYear.Name = "lblYear";
      this.lblYear.Size = new Size(30, 15);
      this.lblYear.TabIndex = 2;
      this.lblYear.Text = "Year";
      this.lblAuthor.AutoSize = true;
      this.lblAuthor.Location = new Point(7, 46);
      this.lblAuthor.Name = "lblAuthor";
      this.lblAuthor.Size = new Size(44, 15);
      this.lblAuthor.TabIndex = 1;
      this.lblAuthor.Text = "Author";
      this.lblTitle.AutoSize = true;
      this.lblTitle.Location = new Point(7, 23);
      this.lblTitle.Name = "lblTitle";
      this.lblTitle.Size = new Size(30, 15);
      this.lblTitle.TabIndex = 0;
      this.lblTitle.Text = "Title";
      this.btnSourceFile.Location = new Point(0, 3);
      this.btnSourceFile.Name = "btnSourceFile";
      this.btnSourceFile.Size = new Size(75, 23);
      this.btnSourceFile.TabIndex = 1;
      this.btnSourceFile.Text = "Load File";
      this.btnSourceFile.UseVisualStyleBackColor = true;
      this.btnSourceFile.Click += new EventHandler(this.btnSourceFile_Click);
      this.txtInputFile.Location = new Point(84, 3);
      this.txtInputFile.Name = "txtInputFile";
      this.txtInputFile.Size = new Size(464, 23);
      this.txtInputFile.TabIndex = 0;
      this.wpChapters.Controls.Add((Control) this.chkChapterNumberPosition);
      this.wpChapters.Controls.Add((Control) this.chkCreateCUE);
      this.wpChapters.Controls.Add((Control) this.lblSplitByChapters);
      this.wpChapters.Controls.Add((Control) this.rdSplitNo);
      this.wpChapters.Controls.Add((Control) this.rdSplitYes);
      this.wpChapters.Controls.Add((Control) this.txtChapterInstructions);
      this.wpChapters.Name = "wpChapters";
      this.wpChapters.NextPage = this.wpSettings;
      this.wpChapters.Size = new Size(551, 397);
      this.wpChapters.TabIndex = 4;
      this.wpChapters.Text = "Chapters";
      this.chkChapterNumberPosition.AutoSize = true;
      this.chkChapterNumberPosition.Location = new Point(6, 194);
      this.chkChapterNumberPosition.Name = "chkChapterNumberPosition";
      this.chkChapterNumberPosition.Size = new Size(366, 19);
      this.chkChapterNumberPosition.TabIndex = 5;
      this.chkChapterNumberPosition.Text = "Start file name with chapter number (eg: \"01 - book name.mp3\")";
      this.chkChapterNumberPosition.UseVisualStyleBackColor = true;
      this.chkCreateCUE.AutoSize = true;
      this.chkCreateCUE.Location = new Point(6, 168);
      this.chkCreateCUE.Name = "chkCreateCUE";
      this.chkCreateCUE.Size = new Size(203, 19);
      this.chkCreateCUE.TabIndex = 4;
      this.chkCreateCUE.Text = "Create a CUE file for later splitting";
      this.chkCreateCUE.UseVisualStyleBackColor = true;
      this.lblSplitByChapters.AutoSize = true;
      this.lblSplitByChapters.Location = new Point(3, 147);
      this.lblSplitByChapters.Name = "lblSplitByChapters";
      this.lblSplitByChapters.Size = new Size(180, 15);
      this.lblSplitByChapters.TabIndex = 3;
      this.lblSplitByChapters.Text = "Split the output file into chapters";
      this.rdSplitNo.AutoSize = true;
      this.rdSplitNo.Location = new Point(238, 143);
      this.rdSplitNo.Name = "rdSplitNo";
      this.rdSplitNo.Size = new Size(41, 19);
      this.rdSplitNo.TabIndex = 2;
      this.rdSplitNo.Text = "No";
      this.rdSplitNo.UseVisualStyleBackColor = true;
      this.rdSplitNo.CheckedChanged += new EventHandler(this.rdSplitNo_CheckedChanged);
      this.rdSplitYes.AutoSize = true;
      this.rdSplitYes.Checked = true;
      this.rdSplitYes.Location = new Point(189, 143);
      this.rdSplitYes.Name = "rdSplitYes";
      this.rdSplitYes.Size = new Size(43, 19);
      this.rdSplitYes.TabIndex = 1;
      this.rdSplitYes.TabStop = true;
      this.rdSplitYes.Text = "Yes";
      this.rdSplitYes.UseVisualStyleBackColor = true;
      this.rdSplitYes.CheckedChanged += new EventHandler(this.rdSplitYes_CheckedChanged);
      this.txtChapterInstructions.Location = new Point(0, 3);
      this.txtChapterInstructions.Multiline = true;
      this.txtChapterInstructions.Name = "txtChapterInstructions";
      this.txtChapterInstructions.ReadOnly = true;
      this.txtChapterInstructions.Size = new Size(548, 131);
      this.txtChapterInstructions.TabIndex = 0;
      this.txtChapterInstructions.Text = componentResourceManager.GetString("txtChapterInstructions.Text");
      this.wpSettings.Controls.Add((Control) this.cmbChannels);
      this.wpSettings.Controls.Add((Control) this.cmbBitrate);
      this.wpSettings.Controls.Add((Control) this.lblOutputChannels);
      this.wpSettings.Controls.Add((Control) this.lblOutputBitrate);
      this.wpSettings.Controls.Add((Control) this.cmbSampleRate);
      this.wpSettings.Controls.Add((Control) this.lblOutputSampleRate);
      this.wpSettings.Controls.Add((Control) this.cmbCodec);
      this.wpSettings.Controls.Add((Control) this.lblOutputCodec);
      this.wpSettings.Controls.Add((Control) this.txtEncodingInstructions);
      this.wpSettings.Name = "wpSettings";
      this.wpSettings.NextPage = this.wpSummary;
      this.wpSettings.Size = new Size(551, 397);
      this.wpSettings.TabIndex = 2;
      this.wpSettings.Text = "Conversion Settings";
      this.cmbChannels.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbChannels.FormattingEnabled = true;
      this.cmbChannels.Items.AddRange(new object[3]
      {
        (object) "Auto",
        (object) "Mono",
        (object) "Stereo"
      });
      this.cmbChannels.Location = new Point(483, 343);
      this.cmbChannels.Name = "cmbChannels";
      this.cmbChannels.Size = new Size(65, 23);
      this.cmbChannels.TabIndex = 8;
      this.cmbChannels.SelectedIndexChanged += new EventHandler(this.cmbChannels_SelectedIndexChanged);
      this.cmbBitrate.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbBitrate.FormattingEnabled = true;
      this.cmbBitrate.Items.AddRange(new object[4]
      {
        (object) "32",
        (object) "64",
        (object) "96",
        (object) "128"
      });
      this.cmbBitrate.Location = new Point(346, 343);
      this.cmbBitrate.Name = "cmbBitrate";
      this.cmbBitrate.Size = new Size(69, 23);
      this.cmbBitrate.TabIndex = 7;
      this.cmbBitrate.SelectedIndexChanged += new EventHandler(this.cmbBitrate_SelectedIndexChanged);
      this.lblOutputChannels.AutoSize = true;
      this.lblOutputChannels.Location = new Point(421, 346);
      this.lblOutputChannels.Name = "lblOutputChannels";
      this.lblOutputChannels.Size = new Size(56, 15);
      this.lblOutputChannels.TabIndex = 6;
      this.lblOutputChannels.Text = "Channels";
      this.lblOutputBitrate.AutoSize = true;
      this.lblOutputBitrate.Location = new Point(299, 346);
      this.lblOutputBitrate.Name = "lblOutputBitrate";
      this.lblOutputBitrate.Size = new Size(41, 15);
      this.lblOutputBitrate.TabIndex = 5;
      this.lblOutputBitrate.Text = "Bitrate";
      this.cmbSampleRate.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbSampleRate.FormattingEnabled = true;
      this.cmbSampleRate.Items.AddRange(new object[2]
      {
        (object) "22050",
        (object) "44100"
      });
      this.cmbSampleRate.Location = new Point(215, 343);
      this.cmbSampleRate.Name = "cmbSampleRate";
      this.cmbSampleRate.Size = new Size(78, 23);
      this.cmbSampleRate.TabIndex = 4;
      this.cmbSampleRate.SelectedIndexChanged += new EventHandler(this.cmbSampleRate_SelectedIndexChanged);
      this.lblOutputSampleRate.AutoSize = true;
      this.lblOutputSampleRate.Location = new Point(137, 346);
      this.lblOutputSampleRate.Name = "lblOutputSampleRate";
      this.lblOutputSampleRate.Size = new Size(72, 15);
      this.lblOutputSampleRate.TabIndex = 3;
      this.lblOutputSampleRate.Text = "Sample Rate";
      this.cmbCodec.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbCodec.FormattingEnabled = true;
      this.cmbCodec.Items.AddRange(new object[2]
      {
        (object) "MP3",
        (object) "AAC/M4B"
      });
      this.cmbCodec.Location = new Point(50, 343);
      this.cmbCodec.Name = "cmbCodec";
      this.cmbCodec.Size = new Size(81, 23);
      this.cmbCodec.TabIndex = 2;
      this.cmbCodec.SelectedIndexChanged += new EventHandler(this.cmbCodec_SelectedIndexChanged);
      this.lblOutputCodec.AutoSize = true;
      this.lblOutputCodec.Location = new Point(3, 346);
      this.lblOutputCodec.Name = "lblOutputCodec";
      this.lblOutputCodec.Size = new Size(41, 15);
      this.lblOutputCodec.TabIndex = 1;
      this.lblOutputCodec.Text = "Codec";
      this.txtEncodingInstructions.Location = new Point(3, 3);
      this.txtEncodingInstructions.Multiline = true;
      this.txtEncodingInstructions.Name = "txtEncodingInstructions";
      this.txtEncodingInstructions.ReadOnly = true;
      this.txtEncodingInstructions.Size = new Size(545, 330);
      this.txtEncodingInstructions.TabIndex = 0;
      this.txtEncodingInstructions.Text = componentResourceManager.GetString("txtEncodingInstructions.Text");
      this.wpSummary.Controls.Add((Control) this.grpOutputFolder);
      this.wpSummary.Controls.Add((Control) this.btnOutputFile);
      this.wpSummary.Controls.Add((Control) this.txtOutputFile);
      this.wpSummary.Controls.Add((Control) this.chkBeginConversion);
      this.wpSummary.Controls.Add((Control) this.txtSummary);
      this.wpSummary.IsFinishPage = true;
      this.wpSummary.Name = "wpSummary";
      this.wpSummary.Size = new Size(551, 397);
      this.wpSummary.TabIndex = 3;
      this.wpSummary.Text = "Output and final summary";
      this.grpOutputFolder.Controls.Add((Control) this.chkAutoTitle);
      this.grpOutputFolder.Controls.Add((Control) this.chkTitle);
      this.grpOutputFolder.Controls.Add((Control) this.chkAuthor);
      this.grpOutputFolder.Location = new Point(4, 4);
      this.grpOutputFolder.Name = "grpOutputFolder";
      this.grpOutputFolder.Size = new Size(541, 100);
      this.grpOutputFolder.TabIndex = 4;
      this.grpOutputFolder.TabStop = false;
      this.grpOutputFolder.Text = "Output folder structure";
      this.chkAutoTitle.AutoSize = true;
      this.chkAutoTitle.Checked = true;
      this.chkAutoTitle.CheckState = CheckState.Checked;
      this.chkAutoTitle.Location = new Point(6, 75);
      this.chkAutoTitle.Name = "chkAutoTitle";
      this.chkAutoTitle.Size = new Size((int) sbyte.MaxValue, 19);
      this.chkAutoTitle.TabIndex = 2;
      this.chkAutoTitle.Text = "Author - Book Title";
      this.chkAutoTitle.UseVisualStyleBackColor = true;
      this.chkTitle.AutoSize = true;
      this.chkTitle.Location = new Point(6, 47);
      this.chkTitle.Name = "chkTitle";
      this.chkTitle.Size = new Size(95, 19);
      this.chkTitle.TabIndex = 1;
      this.chkTitle.Text = "Book Title ->";
      this.chkTitle.UseVisualStyleBackColor = true;
      this.chkAuthor.AutoSize = true;
      this.chkAuthor.Location = new Point(6, 22);
      this.chkAuthor.Name = "chkAuthor";
      this.chkAuthor.Size = new Size(79, 19);
      this.chkAuthor.TabIndex = 0;
      this.chkAuthor.Text = "Author ->";
      this.chkAuthor.UseVisualStyleBackColor = true;
      this.btnOutputFile.Location = new Point(3, 109);
      this.btnOutputFile.Name = "btnOutputFile";
      this.btnOutputFile.Size = new Size(75, 23);
      this.btnOutputFile.TabIndex = 3;
      this.btnOutputFile.Text = "Output File";
      this.btnOutputFile.UseVisualStyleBackColor = true;
      this.btnOutputFile.Click += new EventHandler(this.btnOutputFile_Click);
      this.txtOutputFile.Location = new Point(84, 110);
      this.txtOutputFile.Name = "txtOutputFile";
      this.txtOutputFile.Size = new Size(461, 23);
      this.txtOutputFile.TabIndex = 2;
      this.chkBeginConversion.AutoSize = true;
      this.chkBeginConversion.Checked = true;
      this.chkBeginConversion.CheckState = CheckState.Checked;
      this.chkBeginConversion.Location = new Point(181, 319);
      this.chkBeginConversion.Name = "chkBeginConversion";
      this.chkBeginConversion.Size = new Size(188, 19);
      this.chkBeginConversion.TabIndex = 1;
      this.chkBeginConversion.Text = "Begin Conversion Immediately";
      this.chkBeginConversion.UseVisualStyleBackColor = true;
      this.txtSummary.Location = new Point(3, 139);
      this.txtSummary.Multiline = true;
      this.txtSummary.Name = "txtSummary";
      this.txtSummary.ReadOnly = true;
      this.txtSummary.Size = new Size(545, 133);
      this.txtSummary.TabIndex = 0;
      this.txtSummary.Text = componentResourceManager.GetString("txtSummary.Text");
      this.openFileDialog1.FileName = "openFileDialog1";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(598, 551);
      this.Controls.Add((Control) this.wizardControl1);
      this.Name = nameof (FormWizard);
      this.Text = nameof (FormWizard);
      this.Load += new EventHandler(this.FormWizard_Load);
      this.wizardControl1.EndInit();
      this.wpIntro.ResumeLayout(false);
      this.wpIntro.PerformLayout();
      this.wpSelectFile.ResumeLayout(false);
      this.wpSelectFile.PerformLayout();
      this.grpQuality.ResumeLayout(false);
      this.grpQuality.PerformLayout();
      this.grpFileInfo.ResumeLayout(false);
      this.grpFileInfo.PerformLayout();
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.wpChapters.ResumeLayout(false);
      this.wpChapters.PerformLayout();
      this.wpSettings.ResumeLayout(false);
      this.wpSettings.PerformLayout();
      this.wpSummary.ResumeLayout(false);
      this.wpSummary.PerformLayout();
      this.grpOutputFolder.ResumeLayout(false);
      this.grpOutputFolder.PerformLayout();
      this.ResumeLayout(false);
    }

    public FormWizard()
    {
      this.InitializeComponent();
    }

    public void Init()
    {
      if (this.myWizardOptions.FirstTime)
        this.txtIntroText.Text = this.txtIntroText.Text.Replace("{first_time}", "\r\nThis is the first time that you launched inAudible.  You can use this wizard to convert your Audible files, or you can click cancel now and continue with the main application. You can always launch this wizard again from the File menu.\r\n");
      else
        this.txtIntroText.Text = this.txtIntroText.Text.Replace("{first_time}", "");
    }

    private bool GetCoverFromAAX(string aaxFile)
    {
      try
      {
        TagLib.File file = TagLib.File.Create(aaxFile, "audio/mp4", ReadStyle.Average);
        if (file.Tag.Pictures.Length > 0)
        {
          IPicture picture = file.Tag.Pictures[0];
          this.pictureBox1.Image = Image.FromStream((Stream) new MemoryStream(file.Tag.Pictures[0].Data.Data)).GetThumbnailImage(this.pictureBox1.Width, this.pictureBox1.Height, (Image.GetThumbnailImageAbort) null, IntPtr.Zero);
          return true;
        }
      }
      catch
      {
      }
      return false;
    }

    private bool ScrapeAAJPG(string file)
    {
      bool flag;
      try
      {
        string str = Path.GetTempPath() + "\\inAudible-scraped-" + Guid.NewGuid().ToString() + ".jpg";
        long length1 = new FileInfo(file).Length;
        long length2 = 100000;
        if (length1 < length2)
          length2 = length1;
        byte[] buffer = new byte[length2];
        long offset = length1 - length2;
        using (BinaryReader binaryReader = new BinaryReader((Stream) new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
        {
          binaryReader.BaseStream.Seek(offset, SeekOrigin.Begin);
          binaryReader.Read(buffer, 0, (int) length2);
        }
        byte[] bytes1 = Encoding.ASCII.GetBytes("JFIF");
        long sourceIndex = Audible.ByteSearchBHM(buffer, bytes1, 1) - 6L;
        long length3 = length2 - sourceIndex;
        byte[] bytes2 = new byte[length3];
        Array.Copy((Array) buffer, sourceIndex, (Array) bytes2, 0L, length3);
        System.IO.File.WriteAllBytes(str, bytes2);
        flag = true;
        Image image = (Image) new Bitmap(str);
        this.pictureBox1.Image = image.GetThumbnailImage(this.pictureBox1.Width, this.pictureBox1.Height, (Image.GetThumbnailImageAbort) null, new IntPtr());
        image.Dispose();
        System.IO.File.Delete(str);
      }
      catch (Exception ex)
      {
        Audible.diskLogger("Failed to find cover art in AA file: " + ex.ToString());
        flag = false;
      }
      return flag;
    }

    private void GetSampleRateFromInput(string tFile, ref VirtualWAV myVirtualWav)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.supportLibs.ffprobePath;
      process.StartInfo.Arguments = "-loglevel panic -show_streams -print_format flat \"" + tFile + "\"";
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.Start();
      string end = process.StandardOutput.ReadToEnd();
      process.WaitForExit();
      string str1 = end;
      char[] chArray1 = new char[1]{ '\n' };
      foreach (string str2 in str1.Split(chArray1))
      {
        char[] chArray2 = new char[1]{ '=' };
        string[] strArray = str2.Split(chArray2);
        if (strArray[0] == "streams.stream.0.channels")
        {
          try
          {
            string s = strArray[1].Replace("\"", "").TrimEnd('\r', '\n');
            myVirtualWav.channels = int.Parse(s);
          }
          catch
          {
          }
        }
        else if (strArray[0] == "streams.stream.0.sample_rate")
        {
          try
          {
            string s = strArray[1].Replace("\"", "").TrimEnd('\r', '\n');
            myVirtualWav.sampleRate = int.Parse(s);
          }
          catch
          {
          }
        }
        else if (strArray[0] == "streams.stream.0.bit_rate")
        {
          try
          {
            string s = strArray[1].Replace("\"", "").TrimEnd('\r', '\n');
            myVirtualWav.originalBitrate = (int) Math.Round(double.Parse(s) / 1000.0, MidpointRounding.AwayFromZero);
          }
          catch
          {
          }
        }
      }
    }

    private void SetSummaryText()
    {
      FormWizard formWizard = new FormWizard();
      string text1 = formWizard.txtSummary.Text;
      formWizard.Dispose();
      string str1 = text1.Replace("{source_bitrate}", this.txtBitrate.Text).Replace("{source_codec}", this.txtFormat.Text).Replace("{source_sample_rate}", this.txtSampleRate.Text);
      string str2;
      if (this.rdSplitYes.Checked)
      {
        string text2 = this.txtChapterCount.Text;
        str2 = str1.Replace("{split}", text2).Replace("{target_codec}", this.cmbCodec.Text + "'s");
      }
      else
        str2 = str1.Replace("{split}", "a single").Replace("{target_codec}", this.cmbCodec.Text);
      this.txtSummary.Text = str2.Replace("{target_bitrate}", this.cmbBitrate.Text).Replace("{target_sample_rate}", this.cmbSampleRate.Text);
      this.txtSummary.SelectionStart = this.txtSummary.Text.Length;
      this.txtSummary.DeselectAll();
    }

    private void SetEncodingText(string codec)
    {
      FormWizard formWizard = new FormWizard();
      string text = formWizard.txtEncodingInstructions.Text;
      formWizard.Dispose();
      string newValue = !(codec == "M4B") ? (!(codec == "mp332") ? "Your file is an AA/ACELP. These are very low quality and cannot be losslessly converted. While inAudible can convert this for you, it is recommended that you download a Format 4 or Enhanced version, instead." : "Your file is an AA/MP3. If you want a lossless conversion, you will need to select MP3 output.") : "Your file is an AAX/AAC. If you want a lossless conversion, you will need to select AAC/M4B output.";
      this.txtEncodingInstructions.Text = text.Replace("{replace_me}", newValue);
      this.txtEncodingInstructions.SelectionStart = this.txtEncodingInstructions.Text.Length;
      this.txtEncodingInstructions.DeselectAll();
    }

    private void btnSourceFile_Click(object sender, EventArgs e)
    {
      this.openFileDialog1 = new OpenFileDialog();
      this.openFileDialog1.Multiselect = false;
      this.openFileDialog1.Filter = "Audible files (*.aa, *.aax)|*.aa;*.aax";
      if (this.openFileDialog1.ShowDialog() != DialogResult.OK)
        return;
      this.myAudible = new Audible();
      string fileName = this.openFileDialog1.FileName;
      this.txtInputFile.Text = fileName;
      this.grpFileInfo.Visible = true;
      this.grpQuality.Visible = true;
      this.wpSelectFile.ShowNext = true;
      string extension = Path.GetExtension(fileName);
      if (extension == ".aa")
      {
        this.myAudible.getMetaData(fileName);
        this.myAudible.nfo.sourceFormat = "Audible AA";
        this.ScrapeAAJPG(fileName);
        this.txtDuration.Text = this.GetTotalTimeFromAA(fileName);
        this.myChapters.SetDoubleChapters(this.myAudible.getAudibleChapters(fileName));
      }
      else if (extension == ".aax")
      {
        this.myAudible.GetMetaDataTaglib(fileName);
        this.myAudible.nfo.sourceFormat = "Audible AAX";
        this.myChapters.SetDoubleChapters(this.myAudible.getAAXChapters(fileName));
        this.GetCoverFromAAX(fileName);
        this.txtDuration.Text = this.myAudible.GetTotalTimeFormatted();
      }
      this.txtTitle.Text = this.myAudible.title;
      this.txtAuthor.Text = this.myAudible.author;
      this.txtYear.Text = this.myAudible.year;
      this.txtComments.Text = this.myAudible.GetComments();
      this.GetSampleRateFromInput(fileName, ref this.myVW);
      this.txtFormat.Text = this.myAudible.codec;
      this.txtBitrate.Text = this.myVW.originalBitrate.ToString() + "kbits";
      this.txtSampleRate.Text = this.myVW.sampleRate.ToString() + "Hz";
      if (this.myVW.channels == 1)
        this.txtChannels.Text = "Mono";
      else
        this.txtChannels.Text = "Stereo";
      this.txtChapterCount.Text = this.myChapters.Count().ToString();
      this.SetEncodingText(this.myAudible.codec);
      this.SetOutputSelections();
      string asciiTag = this.myAudible.GetASCIITag(this.myAudible.title);
      this.txtOutputFile.Text = Path.GetDirectoryName(fileName) + "\\" + asciiTag + ".mp3";
      this.chkAuthor.Text = this.myAudible.author + " ->";
      this.chkTitle.Text = this.myAudible.title + " ->";
      this.chkAutoTitle.Text = this.myAudible.author + " - " + this.myAudible.title;
      this.SetSummaryText();
    }

    private void SetOutputSelections()
    {
      if (this.myAudible.codec == "M4B")
        this.cmbCodec.Text = "AAC/M4B";
      else
        this.cmbCodec.Text = "MP3";
      if (this.myVW.channels == 1)
        this.cmbChannels.Text = "Mono";
      else
        this.cmbChannels.Text = "Auto";
      this.cmbSampleRate.Text = this.myVW.sampleRate.ToString();
      if (this.myVW.sampleRate < 22050)
        this.cmbSampleRate.Text = "22050";
      if (this.myVW.originalBitrate <= 32)
        this.cmbBitrate.Text = "32";
      else if (this.myVW.originalBitrate >= 60 && this.myVW.originalBitrate <= 70)
        this.cmbBitrate.Text = "64";
      else if (this.myVW.originalBitrate >= 110 && this.myVW.originalBitrate <= 131)
        this.cmbBitrate.Text = "128";
      else
        this.cmbBitrate.Text = this.myVW.originalBitrate.ToString();
    }

    private string GetTotalTimeFromAA(string file)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.supportLibs.ffprobePath;
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

    private void FormWizard_Load(object sender, EventArgs e)
    {
      this.txtIntroText.SelectionStart = this.txtIntroText.Text.Length;
      this.txtIntroText.DeselectAll();
      this.txtChapterInstructions.SelectionStart = this.txtChapterInstructions.Text.Length;
      this.txtChapterInstructions.DeselectAll();
      this.txtEncodingInstructions.SelectionStart = this.txtEncodingInstructions.Text.Length;
      this.txtEncodingInstructions.DeselectAll();
      this.txtSummary.SelectionStart = this.txtSummary.Text.Length;
      this.txtSummary.DeselectAll();
    }

    private void btnOutputFile_Click(object sender, EventArgs e)
    {
      this.saveFileDialog1.DefaultExt = "mp3";
      this.saveFileDialog1.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
      this.saveFileDialog1.AddExtension = true;
      if (this.saveFileDialog1.ShowDialog() != DialogResult.OK)
        return;
      this.txtOutputFile.Text = this.saveFileDialog1.FileName;
    }

    private void cmbCodec_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.SetSummaryText();
    }

    private void cmbSampleRate_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.SetSummaryText();
    }

    private void cmbBitrate_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.SetSummaryText();
    }

    private void cmbChannels_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.SetSummaryText();
    }

    private void wizardControl1_Finished(object sender, EventArgs e)
    {
      this.myWizardOptions.InputFile = this.txtInputFile.Text;
      this.myWizardOptions.OutputFile = this.txtOutputFile.Text;
      this.myWizardOptions.Split = this.rdSplitYes.Checked;
      this.myWizardOptions.CUE = this.chkCreateCUE.Checked;
      this.myWizardOptions.ChaptersFirst = this.chkCreateCUE.Checked;
      this.myWizardOptions.Codec = this.cmbCodec.Text;
      this.myWizardOptions.SampleRate = this.cmbSampleRate.Text;
      this.myWizardOptions.Bitrate = this.cmbBitrate.Text;
      this.myWizardOptions.Channels = this.cmbChannels.Text;
      this.myWizardOptions.Start = this.chkBeginConversion.Checked;
      this.myWizardOptions.Applied = true;
    }

    private void rdSplitNo_CheckedChanged(object sender, EventArgs e)
    {
      this.SetSummaryText();
    }

    private void rdSplitYes_CheckedChanged(object sender, EventArgs e)
    {
      this.SetSummaryText();
    }
  }
}
