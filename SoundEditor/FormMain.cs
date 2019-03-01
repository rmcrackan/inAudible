// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormMain
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using AudibleConvertor.Properties;
using AudioSoundEditor;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SoundEditor
{
  public class FormMain : Form
  {
    private int BUFFERLEN = 1200;
    private const int VOLUME_FLAT = 0;
    private const int VOLUME_SLIDING = 1;
    public GroupBox Frame4;
    public Label Label2;
    public Label Label3;
    public Label Label4;
    public Label Label5;
    public Label Label6;
    public Label LabelSelectionBegin;
    public Label LabelSelectionEnd;
    public Label LabelSelectionDuration;
    public Label LabelRangeBegin;
    public Label LabelRangeEnd;
    public Label LabelRangeDuration;
    public Label LabelTotalDuration;
    public Label Label8;
    public GroupBox Frame5;
    public PictureBox Picture1;
    public Label LabelStatus;
    private OpenFileDialog openFileDialog1;
    public MainMenu MainMenu1;
    public MenuItem mnuFile;
    public MenuItem mnuFileLoad;
    public MenuItem mnuFileExport;
    public MenuItem mnuFileBar1;
    public MenuItem mnuFileExit;
    public MenuItem mnuEdit;
    public MenuItem mnuEditCut;
    public MenuItem mnuEditCopy;
    public MenuItem mnuEditPaste;
    public MenuItem mnuEditPasteInsert;
    public MenuItem mnuEditPasteMix;
    public MenuItem mnuEditBar0;
    public MenuItem mnuEditDeleteSel;
    public MenuItem mnuEditReduceToSel;
    public MenuItem mnuEditBar1;
    public MenuItem mnuEditSelectAll;
    public MenuItem mnuEditRemoveSel;
    public MenuItem mnuTools;
    public MenuItem mnuToolsApplyBackground;
    public MenuItem mnuToolsInsertSilence;
    public MenuItem mnuToolsAppendSound;
    public MenuItem mnuToolsInsertSound;
    public MenuItem mnuToolsMixSound;
    public MenuItem mnuToolsBar0;
    public MenuItem mnuToolsOptions;
    public MenuItem mnuZoom;
    public MenuItem mnuZoomToSelection;
    public MenuItem mnuZoomToFullWaveform;
    public MenuItem mnuZoomIn;
    public MenuItem mnuZoomOut;
    public MenuItem mnuHelp;
    public MenuItem mnuHelpAbout;
    public Timer TimerReload;
    public Timer TimerMenuEnabler;
    private ProgressBar progressBar1;
    private IContainer components;
    public Button buttonPlay;
    public Button buttonStop;
    public Button buttonPlaySelection;
    private Timer timerDisplayWaveform;
    private Button buttonPause;
    private MenuItem menuItem1;
    private MenuItem menuItem2;
    private AudioSoundEditor.AudioSoundEditor audioSoundEditor1;
    private MenuItem menuEffectsFlatVolume;
    private MenuItem menuEffectsSlidingVolume;
    private MenuItem menuEffectsVolumeAutomation;
    private MenuItem menuToolsOverwrite;
    private MenuItem menuItem3;
    private MenuItem menuEffectsApplyWaveReverb;
    private MenuItem menuItem5;
    private MenuItem menuEffectsApplyEqualizer;
    private MenuItem mnuEffects;
    private MenuItem menuItem4;
    private MenuItem menuItem6;
    private MenuItem menuEffectsApplyReverbInternal;
    private MenuItem menuEffectsApplyReverbExternal;
    private MenuItem menuEffectsApplyBalanceInternal;
    private MenuItem menuEffectsApplyBalanceExternal;
    private MenuItem menuEffectsApplyBassBoostExternal;
    private MenuItem menuItem7;
    private MenuItem menuItem8;
    private MenuItem menuItem9;
    private MenuItem menuEffectsApplyClassicEQ;
    private MenuItem menuEffectsApplyVstFromDisk;
    private MenuItem menuEffectsApplyTempoChange;
    private MenuItem menuEffectsApplyRateChange;
    private MenuItem menuEffectsApplyPitchChange;
    private MenuItem menuItem10;
    private MenuItem menuEffectsReverseSound;
    private MenuItem mnuToolsRemoveSilence;
    private MenuItem mnuEditBar00;
    private MenuItem mnuEditUndo;
    private MenuItem menuFileRawLoad;
    private MenuItem menuItem11;
    private MenuItem menuItem12;
    private MenuItem menuEffectsApplyLowPass;
    private MenuItem menuEffectsApplyHighPass;
    private MenuItem menuEffectsApplyBandPass;
    private MenuItem menuEffectsApplyBandStop;
    private Timer timerPlaybackPos;
    private Label label1;
    private TrackBar trackBar1;
    private MenuItem menuItem13;
    private MenuItem menuItem14;
    private MenuItem menuEffectsApplyNormalizePeak;
    private MenuItem menuEffectsApplyNormalizeTarget;
    private MenuItem mnuWaveformOptions;
    private MenuItem menuItem15;
    private MenuItem menuItem16;
    private MenuItem menuItemRemoveClicksPops;
    private MenuItem menuItemRemoveHiss;
    private MenuItem menuEffectsDCOffsetRemoval;
    private Label label7;
    public PictureBox pictureBoxWaveformScroll;
    private MenuItem menuItem17;
    private MenuItem mnuToolsTrimSilence;
    private MenuItem menuItem18;
    private MenuItem mnuFileGenerateTone;
    private MenuItem mnuFileGenerateSlidingTone;
    private MenuItem mnuFileGenerateCompositeTone;
    private MenuItem mnuFileGenerateNoise;
    private MenuItem mnuFileGenerateDtmf;
    private MenuItem mnuFileGenerateBinauralTone;
    private MenuItem mnuFileGenerateSpeech;
    private Label label18;
    private Label label17;
    private Label labelSpectrum;
    private MenuItem menuEffectsVocalRemoval;
    private byte[] m_byteBuffer;
    private DSPCallbackFunction addrReverbCallback;
    private DSPCallbackFunction addrBalanceCallback;
    public int m_idDspReverbInternal;
    public int m_idDspBalanceInternal;
    public int m_idDspReverbExternal;
    public int m_idDspBalanceExternal;
    public int m_idDspBassBoostExternal;
    public int m_idVstKarmaFxEq;
    public int m_idVstFromFile;
    private float[] m_buffReverbLeft;
    private float[] m_buffReverbRight;
    private int m_posReverb;
    private short m_nBalancePercentageInternal;
    private BASSBOOST_PARAMETERS m_paramBassBoostExternal;
    private string m_strExportPathname;
    private IntPtr m_hWndWaveformScroller;
    private IntPtr m_hWndVuMeterLeft;
    private IntPtr m_hWndVuMeterRight;
    internal Font m_fontText;

    public FormMain()
    {
      this.InitializeComponent();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.Frame4 = new GroupBox();
      this.Label2 = new Label();
      this.Label3 = new Label();
      this.Label4 = new Label();
      this.Label5 = new Label();
      this.Label6 = new Label();
      this.LabelSelectionBegin = new Label();
      this.LabelSelectionEnd = new Label();
      this.LabelSelectionDuration = new Label();
      this.LabelRangeBegin = new Label();
      this.LabelRangeEnd = new Label();
      this.LabelRangeDuration = new Label();
      this.LabelTotalDuration = new Label();
      this.Label8 = new Label();
      this.Frame5 = new GroupBox();
      this.buttonPause = new Button();
      this.buttonPlay = new Button();
      this.buttonStop = new Button();
      this.buttonPlaySelection = new Button();
      this.Picture1 = new PictureBox();
      this.LabelStatus = new Label();
      this.openFileDialog1 = new OpenFileDialog();
      this.MainMenu1 = new MainMenu(this.components);
      this.mnuFile = new MenuItem();
      this.mnuFileLoad = new MenuItem();
      this.menuFileRawLoad = new MenuItem();
      this.mnuFileExport = new MenuItem();
      this.menuItem18 = new MenuItem();
      this.mnuFileGenerateTone = new MenuItem();
      this.mnuFileGenerateSlidingTone = new MenuItem();
      this.mnuFileGenerateBinauralTone = new MenuItem();
      this.mnuFileGenerateCompositeTone = new MenuItem();
      this.mnuFileGenerateNoise = new MenuItem();
      this.mnuFileGenerateDtmf = new MenuItem();
      this.mnuFileGenerateSpeech = new MenuItem();
      this.mnuFileBar1 = new MenuItem();
      this.mnuFileExit = new MenuItem();
      this.mnuEdit = new MenuItem();
      this.mnuEditUndo = new MenuItem();
      this.mnuEditBar00 = new MenuItem();
      this.mnuEditCut = new MenuItem();
      this.mnuEditCopy = new MenuItem();
      this.mnuEditPaste = new MenuItem();
      this.mnuEditPasteInsert = new MenuItem();
      this.mnuEditPasteMix = new MenuItem();
      this.mnuEditBar0 = new MenuItem();
      this.mnuEditDeleteSel = new MenuItem();
      this.mnuEditReduceToSel = new MenuItem();
      this.mnuEditBar1 = new MenuItem();
      this.mnuEditSelectAll = new MenuItem();
      this.mnuEditRemoveSel = new MenuItem();
      this.mnuTools = new MenuItem();
      this.mnuToolsInsertSilence = new MenuItem();
      this.mnuToolsTrimSilence = new MenuItem();
      this.mnuToolsRemoveSilence = new MenuItem();
      this.menuItem1 = new MenuItem();
      this.mnuToolsApplyBackground = new MenuItem();
      this.menuItem2 = new MenuItem();
      this.mnuToolsAppendSound = new MenuItem();
      this.mnuToolsInsertSound = new MenuItem();
      this.mnuToolsMixSound = new MenuItem();
      this.menuToolsOverwrite = new MenuItem();
      this.mnuToolsBar0 = new MenuItem();
      this.mnuToolsOptions = new MenuItem();
      this.mnuWaveformOptions = new MenuItem();
      this.mnuEffects = new MenuItem();
      this.menuEffectsFlatVolume = new MenuItem();
      this.menuEffectsSlidingVolume = new MenuItem();
      this.menuEffectsVolumeAutomation = new MenuItem();
      this.menuItem3 = new MenuItem();
      this.menuEffectsApplyWaveReverb = new MenuItem();
      this.menuItem5 = new MenuItem();
      this.menuEffectsApplyEqualizer = new MenuItem();
      this.menuItem4 = new MenuItem();
      this.menuItem6 = new MenuItem();
      this.menuEffectsApplyReverbInternal = new MenuItem();
      this.menuEffectsApplyReverbExternal = new MenuItem();
      this.menuEffectsApplyBalanceInternal = new MenuItem();
      this.menuEffectsApplyBalanceExternal = new MenuItem();
      this.menuEffectsApplyBassBoostExternal = new MenuItem();
      this.menuItem7 = new MenuItem();
      this.menuItem8 = new MenuItem();
      this.menuEffectsApplyClassicEQ = new MenuItem();
      this.menuEffectsApplyVstFromDisk = new MenuItem();
      this.menuItem9 = new MenuItem();
      this.menuEffectsApplyTempoChange = new MenuItem();
      this.menuEffectsApplyRateChange = new MenuItem();
      this.menuEffectsApplyPitchChange = new MenuItem();
      this.menuItem10 = new MenuItem();
      this.menuEffectsReverseSound = new MenuItem();
      this.menuItem11 = new MenuItem();
      this.menuItem12 = new MenuItem();
      this.menuEffectsApplyLowPass = new MenuItem();
      this.menuEffectsApplyHighPass = new MenuItem();
      this.menuEffectsApplyBandPass = new MenuItem();
      this.menuEffectsApplyBandStop = new MenuItem();
      this.menuItem14 = new MenuItem();
      this.menuItem13 = new MenuItem();
      this.menuEffectsApplyNormalizePeak = new MenuItem();
      this.menuEffectsApplyNormalizeTarget = new MenuItem();
      this.menuEffectsDCOffsetRemoval = new MenuItem();
      this.menuItem15 = new MenuItem();
      this.menuItem16 = new MenuItem();
      this.menuItemRemoveClicksPops = new MenuItem();
      this.menuItemRemoveHiss = new MenuItem();
      this.menuItem17 = new MenuItem();
      this.menuEffectsVocalRemoval = new MenuItem();
      this.mnuZoom = new MenuItem();
      this.mnuZoomToSelection = new MenuItem();
      this.mnuZoomToFullWaveform = new MenuItem();
      this.mnuZoomIn = new MenuItem();
      this.mnuZoomOut = new MenuItem();
      this.mnuHelp = new MenuItem();
      this.mnuHelpAbout = new MenuItem();
      this.TimerReload = new Timer(this.components);
      this.TimerMenuEnabler = new Timer(this.components);
      this.progressBar1 = new ProgressBar();
      this.timerDisplayWaveform = new Timer(this.components);
      this.audioSoundEditor1 = new AudioSoundEditor.AudioSoundEditor();
      this.timerPlaybackPos = new Timer(this.components);
      this.label1 = new Label();
      this.trackBar1 = new TrackBar();
      this.label7 = new Label();
      this.pictureBoxWaveformScroll = new PictureBox();
      this.label18 = new Label();
      this.label17 = new Label();
      this.labelSpectrum = new Label();
      this.Frame4.SuspendLayout();
      this.Frame5.SuspendLayout();
      ((ISupportInitialize) this.Picture1).BeginInit();
      this.trackBar1.BeginInit();
      ((ISupportInitialize) this.pictureBoxWaveformScroll).BeginInit();
      this.SuspendLayout();
      this.Frame4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.Frame4.BackColor = SystemColors.Control;
      this.Frame4.Controls.Add((Control) this.Label2);
      this.Frame4.Controls.Add((Control) this.Label3);
      this.Frame4.Controls.Add((Control) this.Label4);
      this.Frame4.Controls.Add((Control) this.Label5);
      this.Frame4.Controls.Add((Control) this.Label6);
      this.Frame4.Controls.Add((Control) this.LabelSelectionBegin);
      this.Frame4.Controls.Add((Control) this.LabelSelectionEnd);
      this.Frame4.Controls.Add((Control) this.LabelSelectionDuration);
      this.Frame4.Controls.Add((Control) this.LabelRangeBegin);
      this.Frame4.Controls.Add((Control) this.LabelRangeEnd);
      this.Frame4.Controls.Add((Control) this.LabelRangeDuration);
      this.Frame4.Controls.Add((Control) this.LabelTotalDuration);
      this.Frame4.Controls.Add((Control) this.Label8);
      this.Frame4.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.Frame4.ForeColor = SystemColors.ControlText;
      this.Frame4.Location = new Point(12, 373);
      this.Frame4.Name = "Frame4";
      this.Frame4.RightToLeft = RightToLeft.No;
      this.Frame4.Size = new Size(340, 117);
      this.Frame4.TabIndex = 17;
      this.Frame4.TabStop = false;
      this.Frame4.Text = "Positions";
      this.Label2.BackColor = SystemColors.Control;
      this.Label2.Cursor = Cursors.Default;
      this.Label2.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.Label2.ForeColor = SystemColors.ControlText;
      this.Label2.Location = new Point(28, 60);
      this.Label2.Name = "Label2";
      this.Label2.RightToLeft = RightToLeft.No;
      this.Label2.Size = new Size(65, 17);
      this.Label2.TabIndex = 25;
      this.Label2.Text = "Selection";
      this.Label3.BackColor = SystemColors.Control;
      this.Label3.Cursor = Cursors.Default;
      this.Label3.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.Label3.ForeColor = SystemColors.ControlText;
      this.Label3.Location = new Point(28, 80);
      this.Label3.Name = "Label3";
      this.Label3.RightToLeft = RightToLeft.No;
      this.Label3.Size = new Size(65, 17);
      this.Label3.TabIndex = 24;
      this.Label3.Text = "Visible range";
      this.Label4.BackColor = SystemColors.Control;
      this.Label4.Cursor = Cursors.Default;
      this.Label4.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.Label4.ForeColor = SystemColors.ControlText;
      this.Label4.Location = new Point(96, 40);
      this.Label4.Name = "Label4";
      this.Label4.RightToLeft = RightToLeft.No;
      this.Label4.Size = new Size(41, 17);
      this.Label4.TabIndex = 23;
      this.Label4.Text = "Begin";
      this.Label5.BackColor = SystemColors.Control;
      this.Label5.Cursor = Cursors.Default;
      this.Label5.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.Label5.ForeColor = SystemColors.ControlText;
      this.Label5.Location = new Point(168, 40);
      this.Label5.Name = "Label5";
      this.Label5.RightToLeft = RightToLeft.No;
      this.Label5.Size = new Size(41, 17);
      this.Label5.TabIndex = 22;
      this.Label5.Text = "End";
      this.Label6.BackColor = SystemColors.Control;
      this.Label6.Cursor = Cursors.Default;
      this.Label6.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.Label6.ForeColor = SystemColors.ControlText;
      this.Label6.Location = new Point(240, 40);
      this.Label6.Name = "Label6";
      this.Label6.RightToLeft = RightToLeft.No;
      this.Label6.Size = new Size(53, 17);
      this.Label6.TabIndex = 21;
      this.Label6.Text = "Duration";
      this.LabelSelectionBegin.BackColor = Color.White;
      this.LabelSelectionBegin.BorderStyle = BorderStyle.Fixed3D;
      this.LabelSelectionBegin.Cursor = Cursors.Default;
      this.LabelSelectionBegin.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.LabelSelectionBegin.ForeColor = Color.Black;
      this.LabelSelectionBegin.Location = new Point(96, 60);
      this.LabelSelectionBegin.Name = "LabelSelectionBegin";
      this.LabelSelectionBegin.RightToLeft = RightToLeft.No;
      this.LabelSelectionBegin.Size = new Size(69, 17);
      this.LabelSelectionBegin.TabIndex = 20;
      this.LabelSelectionBegin.Text = "00:00:00.000";
      this.LabelSelectionBegin.TextAlign = ContentAlignment.TopCenter;
      this.LabelSelectionEnd.BackColor = Color.White;
      this.LabelSelectionEnd.BorderStyle = BorderStyle.Fixed3D;
      this.LabelSelectionEnd.Cursor = Cursors.Default;
      this.LabelSelectionEnd.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.LabelSelectionEnd.ForeColor = Color.Black;
      this.LabelSelectionEnd.Location = new Point(168, 60);
      this.LabelSelectionEnd.Name = "LabelSelectionEnd";
      this.LabelSelectionEnd.RightToLeft = RightToLeft.No;
      this.LabelSelectionEnd.Size = new Size(69, 17);
      this.LabelSelectionEnd.TabIndex = 19;
      this.LabelSelectionEnd.Text = "00:00:00.000";
      this.LabelSelectionEnd.TextAlign = ContentAlignment.TopCenter;
      this.LabelSelectionDuration.BackColor = Color.White;
      this.LabelSelectionDuration.BorderStyle = BorderStyle.Fixed3D;
      this.LabelSelectionDuration.Cursor = Cursors.Default;
      this.LabelSelectionDuration.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.LabelSelectionDuration.ForeColor = Color.Black;
      this.LabelSelectionDuration.Location = new Point(240, 60);
      this.LabelSelectionDuration.Name = "LabelSelectionDuration";
      this.LabelSelectionDuration.RightToLeft = RightToLeft.No;
      this.LabelSelectionDuration.Size = new Size(69, 17);
      this.LabelSelectionDuration.TabIndex = 18;
      this.LabelSelectionDuration.Text = "00:00:00.000";
      this.LabelSelectionDuration.TextAlign = ContentAlignment.TopCenter;
      this.LabelRangeBegin.BackColor = Color.White;
      this.LabelRangeBegin.BorderStyle = BorderStyle.Fixed3D;
      this.LabelRangeBegin.Cursor = Cursors.Default;
      this.LabelRangeBegin.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.LabelRangeBegin.ForeColor = Color.Black;
      this.LabelRangeBegin.Location = new Point(96, 80);
      this.LabelRangeBegin.Name = "LabelRangeBegin";
      this.LabelRangeBegin.RightToLeft = RightToLeft.No;
      this.LabelRangeBegin.Size = new Size(69, 17);
      this.LabelRangeBegin.TabIndex = 17;
      this.LabelRangeBegin.Text = "00:00:00.000";
      this.LabelRangeBegin.TextAlign = ContentAlignment.TopCenter;
      this.LabelRangeEnd.BackColor = Color.White;
      this.LabelRangeEnd.BorderStyle = BorderStyle.Fixed3D;
      this.LabelRangeEnd.Cursor = Cursors.Default;
      this.LabelRangeEnd.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.LabelRangeEnd.ForeColor = Color.Black;
      this.LabelRangeEnd.Location = new Point(168, 80);
      this.LabelRangeEnd.Name = "LabelRangeEnd";
      this.LabelRangeEnd.RightToLeft = RightToLeft.No;
      this.LabelRangeEnd.Size = new Size(69, 17);
      this.LabelRangeEnd.TabIndex = 16;
      this.LabelRangeEnd.Text = "00:00:00.000";
      this.LabelRangeEnd.TextAlign = ContentAlignment.TopCenter;
      this.LabelRangeDuration.BackColor = Color.White;
      this.LabelRangeDuration.BorderStyle = BorderStyle.Fixed3D;
      this.LabelRangeDuration.Cursor = Cursors.Default;
      this.LabelRangeDuration.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.LabelRangeDuration.ForeColor = Color.Black;
      this.LabelRangeDuration.Location = new Point(240, 80);
      this.LabelRangeDuration.Name = "LabelRangeDuration";
      this.LabelRangeDuration.RightToLeft = RightToLeft.No;
      this.LabelRangeDuration.Size = new Size(69, 17);
      this.LabelRangeDuration.TabIndex = 15;
      this.LabelRangeDuration.Text = "00:00:00.000";
      this.LabelRangeDuration.TextAlign = ContentAlignment.TopCenter;
      this.LabelTotalDuration.BackColor = Color.White;
      this.LabelTotalDuration.BorderStyle = BorderStyle.Fixed3D;
      this.LabelTotalDuration.Cursor = Cursors.Default;
      this.LabelTotalDuration.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.LabelTotalDuration.ForeColor = Color.Black;
      this.LabelTotalDuration.Location = new Point(168, 20);
      this.LabelTotalDuration.Name = "LabelTotalDuration";
      this.LabelTotalDuration.RightToLeft = RightToLeft.No;
      this.LabelTotalDuration.Size = new Size(69, 17);
      this.LabelTotalDuration.TabIndex = 14;
      this.LabelTotalDuration.Text = "00:00:00.000";
      this.LabelTotalDuration.TextAlign = ContentAlignment.TopCenter;
      this.Label8.BackColor = SystemColors.Control;
      this.Label8.Cursor = Cursors.Default;
      this.Label8.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.Label8.ForeColor = SystemColors.ControlText;
      this.Label8.Location = new Point(28, 20);
      this.Label8.Name = "Label8";
      this.Label8.RightToLeft = RightToLeft.No;
      this.Label8.Size = new Size(129, 21);
      this.Label8.TabIndex = 13;
      this.Label8.Text = "Recorded sound duration";
      this.Frame5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.Frame5.BackColor = SystemColors.Control;
      this.Frame5.Controls.Add((Control) this.buttonPause);
      this.Frame5.Controls.Add((Control) this.buttonPlay);
      this.Frame5.Controls.Add((Control) this.buttonStop);
      this.Frame5.Controls.Add((Control) this.buttonPlaySelection);
      this.Frame5.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.Frame5.ForeColor = SystemColors.ControlText;
      this.Frame5.Location = new Point(12, 496);
      this.Frame5.Name = "Frame5";
      this.Frame5.RightToLeft = RightToLeft.No;
      this.Frame5.Size = new Size(340, 56);
      this.Frame5.TabIndex = 15;
      this.Frame5.TabStop = false;
      this.Frame5.Text = "Playback";
      this.buttonPause.Enabled = false;
      this.buttonPause.Location = new Point(170, 16);
      this.buttonPause.Name = "buttonPause";
      this.buttonPause.Size = new Size(80, 28);
      this.buttonPause.TabIndex = 10;
      this.buttonPause.Text = "Pause";
      this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
      this.buttonPlay.BackColor = SystemColors.Control;
      this.buttonPlay.Cursor = Cursors.Default;
      this.buttonPlay.Enabled = false;
      this.buttonPlay.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.buttonPlay.ForeColor = SystemColors.ControlText;
      this.buttonPlay.Location = new Point(8, 16);
      this.buttonPlay.Name = "buttonPlay";
      this.buttonPlay.RightToLeft = RightToLeft.No;
      this.buttonPlay.Size = new Size(80, 28);
      this.buttonPlay.TabIndex = 9;
      this.buttonPlay.Text = "Play";
      this.buttonPlay.UseVisualStyleBackColor = false;
      this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
      this.buttonStop.BackColor = SystemColors.Control;
      this.buttonStop.Cursor = Cursors.Default;
      this.buttonStop.Enabled = false;
      this.buttonStop.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.buttonStop.ForeColor = SystemColors.ControlText;
      this.buttonStop.Location = new Point(251, 16);
      this.buttonStop.Name = "buttonStop";
      this.buttonStop.RightToLeft = RightToLeft.No;
      this.buttonStop.Size = new Size(80, 28);
      this.buttonStop.TabIndex = 8;
      this.buttonStop.Text = "Stop";
      this.buttonStop.UseVisualStyleBackColor = false;
      this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
      this.buttonPlaySelection.BackColor = SystemColors.Control;
      this.buttonPlaySelection.Cursor = Cursors.Default;
      this.buttonPlaySelection.Enabled = false;
      this.buttonPlaySelection.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.buttonPlaySelection.ForeColor = SystemColors.ControlText;
      this.buttonPlaySelection.Location = new Point(89, 16);
      this.buttonPlaySelection.Name = "buttonPlaySelection";
      this.buttonPlaySelection.RightToLeft = RightToLeft.No;
      this.buttonPlaySelection.Size = new Size(80, 28);
      this.buttonPlaySelection.TabIndex = 7;
      this.buttonPlaySelection.Text = "Play selection";
      this.buttonPlaySelection.UseVisualStyleBackColor = false;
      this.buttonPlaySelection.Click += new System.EventHandler(this.buttonPlaySelection_Click);
      this.Picture1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.Picture1.BackColor = Color.Black;
      this.Picture1.Cursor = Cursors.Default;
      this.Picture1.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.Picture1.ForeColor = SystemColors.WindowText;
      this.Picture1.Location = new Point(12, 8);
      this.Picture1.Name = "Picture1";
      this.Picture1.RightToLeft = RightToLeft.No;
      this.Picture1.Size = new Size(680, 284);
      this.Picture1.TabIndex = 13;
      this.Picture1.TabStop = false;
      this.Picture1.Visible = false;
      this.LabelStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.LabelStatus.BackColor = SystemColors.Control;
      this.LabelStatus.Cursor = Cursors.Default;
      this.LabelStatus.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.LabelStatus.ForeColor = SystemColors.ControlText;
      this.LabelStatus.Location = new Point(82, 560);
      this.LabelStatus.Name = "LabelStatus";
      this.LabelStatus.RightToLeft = RightToLeft.No;
      this.LabelStatus.Size = new Size(201, 13);
      this.LabelStatus.TabIndex = 16;
      this.LabelStatus.Text = "Status: Idle";
      this.LabelStatus.TextAlign = ContentAlignment.TopCenter;
      this.MainMenu1.MenuItems.AddRange(new MenuItem[6]
      {
        this.mnuFile,
        this.mnuEdit,
        this.mnuTools,
        this.mnuEffects,
        this.mnuZoom,
        this.mnuHelp
      });
      this.mnuFile.Index = 0;
      this.mnuFile.MenuItems.AddRange(new MenuItem[13]
      {
        this.mnuFileLoad,
        this.menuFileRawLoad,
        this.mnuFileExport,
        this.menuItem18,
        this.mnuFileGenerateTone,
        this.mnuFileGenerateSlidingTone,
        this.mnuFileGenerateBinauralTone,
        this.mnuFileGenerateCompositeTone,
        this.mnuFileGenerateNoise,
        this.mnuFileGenerateDtmf,
        this.mnuFileGenerateSpeech,
        this.mnuFileBar1,
        this.mnuFileExit
      });
      this.mnuFile.Text = "&File";
      this.mnuFileLoad.Index = 0;
      this.mnuFileLoad.Shortcut = Shortcut.CtrlO;
      this.mnuFileLoad.Text = "&Load...";
      this.mnuFileLoad.Click += new System.EventHandler(this.mnuFileLoad_Click);
      this.menuFileRawLoad.Index = 1;
      this.menuFileRawLoad.Text = "Load &raw file...";
      this.menuFileRawLoad.Click += new System.EventHandler(this.menuFileRawLoad_Click);
      this.mnuFileExport.Index = 2;
      this.mnuFileExport.Shortcut = Shortcut.CtrlS;
      this.mnuFileExport.Text = "Save and &export...";
      this.mnuFileExport.Click += new System.EventHandler(this.mnuFileExport_Click);
      this.menuItem18.Index = 3;
      this.menuItem18.Text = "-";
      this.mnuFileGenerateTone.Index = 4;
      this.mnuFileGenerateTone.Text = "Generate simple wave &tone...";
      this.mnuFileGenerateTone.Click += new System.EventHandler(this.mnuFileGenerateTone_Click);
      this.mnuFileGenerateSlidingTone.Index = 5;
      this.mnuFileGenerateSlidingTone.Text = "Generate &sliding wave tone...";
      this.mnuFileGenerateSlidingTone.Click += new System.EventHandler(this.mnuFileGenerateSlidingTone_Click);
      this.mnuFileGenerateBinauralTone.Index = 6;
      this.mnuFileGenerateBinauralTone.Text = "Generate &binaural wave tone...";
      this.mnuFileGenerateBinauralTone.Click += new System.EventHandler(this.mnuFileGenerateBinauralTone_Click);
      this.mnuFileGenerateCompositeTone.Index = 7;
      this.mnuFileGenerateCompositeTone.Text = "Generate &composite wave tone...";
      this.mnuFileGenerateCompositeTone.Click += new System.EventHandler(this.mnuFileGenerateCompositeTone_Click);
      this.mnuFileGenerateNoise.Index = 8;
      this.mnuFileGenerateNoise.Text = "Generate &noise...";
      this.mnuFileGenerateNoise.Click += new System.EventHandler(this.mnuFileGenerateNoise_Click);
      this.mnuFileGenerateDtmf.Index = 9;
      this.mnuFileGenerateDtmf.Text = "Generate &DTMF tones...";
      this.mnuFileGenerateDtmf.Click += new System.EventHandler(this.mnuFileGenerateDtmf_Click);
      this.mnuFileGenerateSpeech.Index = 10;
      this.mnuFileGenerateSpeech.Text = "Generate speech from text...";
      this.mnuFileGenerateSpeech.Click += new System.EventHandler(this.mnuFileGenerateSpeech_Click);
      this.mnuFileBar1.Index = 11;
      this.mnuFileBar1.Text = "-";
      this.mnuFileExit.Index = 12;
      this.mnuFileExit.Text = "E&xit";
      this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
      this.mnuEdit.Index = 1;
      this.mnuEdit.MenuItems.AddRange(new MenuItem[13]
      {
        this.mnuEditUndo,
        this.mnuEditBar00,
        this.mnuEditCut,
        this.mnuEditCopy,
        this.mnuEditPaste,
        this.mnuEditPasteInsert,
        this.mnuEditPasteMix,
        this.mnuEditBar0,
        this.mnuEditDeleteSel,
        this.mnuEditReduceToSel,
        this.mnuEditBar1,
        this.mnuEditSelectAll,
        this.mnuEditRemoveSel
      });
      this.mnuEdit.Text = "&Edit";
      this.mnuEditUndo.Index = 0;
      this.mnuEditUndo.Text = "&Undo";
      this.mnuEditUndo.Click += new System.EventHandler(this.mnuEditUndo_Click);
      this.mnuEditBar00.Index = 1;
      this.mnuEditBar00.Text = "-";
      this.mnuEditCut.Index = 2;
      this.mnuEditCut.Shortcut = Shortcut.CtrlX;
      this.mnuEditCut.Text = "Cu&t";
      this.mnuEditCut.Click += new System.EventHandler(this.mnuEditCut_Click);
      this.mnuEditCopy.Index = 3;
      this.mnuEditCopy.Shortcut = Shortcut.CtrlC;
      this.mnuEditCopy.Text = "&Copy";
      this.mnuEditCopy.Click += new System.EventHandler(this.mnuEditCopy_Click);
      this.mnuEditPaste.Index = 4;
      this.mnuEditPaste.Shortcut = Shortcut.CtrlV;
      this.mnuEditPaste.Text = "&Paste";
      this.mnuEditPaste.Click += new System.EventHandler(this.mnuEditPaste_Click);
      this.mnuEditPasteInsert.Index = 5;
      this.mnuEditPasteInsert.Text = "Paste in '&Insert mode'";
      this.mnuEditPasteInsert.Click += new System.EventHandler(this.mnuEditPasteInsert_Click);
      this.mnuEditPasteMix.Index = 6;
      this.mnuEditPasteMix.Text = "Paste in '&Mix mode'";
      this.mnuEditPasteMix.Click += new System.EventHandler(this.mnuEditPasteMix_Click);
      this.mnuEditBar0.Index = 7;
      this.mnuEditBar0.Text = "-";
      this.mnuEditDeleteSel.Index = 8;
      this.mnuEditDeleteSel.Shortcut = Shortcut.Del;
      this.mnuEditDeleteSel.Text = "&Delete selection";
      this.mnuEditDeleteSel.Click += new System.EventHandler(this.mnuEditDeleteSel_Click);
      this.mnuEditReduceToSel.Index = 9;
      this.mnuEditReduceToSel.Text = "&Reduce to selection";
      this.mnuEditReduceToSel.Click += new System.EventHandler(this.mnuEditReduceToSel_Click);
      this.mnuEditBar1.Index = 10;
      this.mnuEditBar1.Text = "-";
      this.mnuEditSelectAll.Index = 11;
      this.mnuEditSelectAll.Shortcut = Shortcut.CtrlA;
      this.mnuEditSelectAll.Text = "Select &all";
      this.mnuEditSelectAll.Click += new System.EventHandler(this.mnuEditSelectAll_Click);
      this.mnuEditRemoveSel.Index = 12;
      this.mnuEditRemoveSel.Text = "Re&move selection";
      this.mnuEditRemoveSel.Click += new System.EventHandler(this.mnuEditRemoveSel_Click);
      this.mnuTools.Index = 2;
      this.mnuTools.MenuItems.AddRange(new MenuItem[13]
      {
        this.mnuToolsInsertSilence,
        this.mnuToolsTrimSilence,
        this.mnuToolsRemoveSilence,
        this.menuItem1,
        this.mnuToolsApplyBackground,
        this.menuItem2,
        this.mnuToolsAppendSound,
        this.mnuToolsInsertSound,
        this.mnuToolsMixSound,
        this.menuToolsOverwrite,
        this.mnuToolsBar0,
        this.mnuToolsOptions,
        this.mnuWaveformOptions
      });
      this.mnuTools.Text = "&Tools";
      this.mnuToolsInsertSilence.Index = 0;
      this.mnuToolsInsertSilence.Text = "Insert &silence...";
      this.mnuToolsInsertSilence.Click += new System.EventHandler(this.mnuToolsInsertSilence_Click);
      this.mnuToolsTrimSilence.Index = 1;
      this.mnuToolsTrimSilence.Text = "Trim initial and final silence...";
      this.mnuToolsTrimSilence.Click += new System.EventHandler(this.mnuToolsTrimSilence_Click);
      this.mnuToolsRemoveSilence.Index = 2;
      this.mnuToolsRemoveSilence.Text = "&Remove silence...";
      this.mnuToolsRemoveSilence.Click += new System.EventHandler(this.mnuToolsRemoveSilence_Click);
      this.menuItem1.Index = 3;
      this.menuItem1.Text = "-";
      this.mnuToolsApplyBackground.Index = 4;
      this.mnuToolsApplyBackground.Text = "Apply &background sound...";
      this.mnuToolsApplyBackground.Click += new System.EventHandler(this.mnuToolsApplyBackground_Click);
      this.menuItem2.Index = 5;
      this.menuItem2.Text = "-";
      this.mnuToolsAppendSound.Index = 6;
      this.mnuToolsAppendSound.Text = "&Append sound file...";
      this.mnuToolsAppendSound.Click += new System.EventHandler(this.mnuToolsAppendSound_Click);
      this.mnuToolsInsertSound.Index = 7;
      this.mnuToolsInsertSound.Text = "&Insert sound file...";
      this.mnuToolsInsertSound.Click += new System.EventHandler(this.mnuToolsInsertSound_Click);
      this.mnuToolsMixSound.Index = 8;
      this.mnuToolsMixSound.Text = "&Mix sound file...";
      this.mnuToolsMixSound.Click += new System.EventHandler(this.mnuToolsMixSound_Click);
      this.menuToolsOverwrite.Index = 9;
      this.menuToolsOverwrite.Text = "&Overwrite with sound file...";
      this.menuToolsOverwrite.Click += new System.EventHandler(this.menuToolsOverwrite_Click);
      this.mnuToolsBar0.Index = 10;
      this.mnuToolsBar0.Text = "-";
      this.mnuToolsOptions.Index = 11;
      this.mnuToolsOptions.Text = "&Options...";
      this.mnuToolsOptions.Click += new System.EventHandler(this.mnuToolsOptions_Click);
      this.mnuWaveformOptions.Index = 12;
      this.mnuWaveformOptions.Text = "&Waveform analyzer settings...";
      this.mnuWaveformOptions.Click += new System.EventHandler(this.mnuWaveformOptions_Click);
      this.mnuEffects.Index = 3;
      this.mnuEffects.MenuItems.AddRange(new MenuItem[26]
      {
        this.menuEffectsFlatVolume,
        this.menuEffectsSlidingVolume,
        this.menuEffectsVolumeAutomation,
        this.menuItem3,
        this.menuEffectsApplyWaveReverb,
        this.menuItem5,
        this.menuEffectsApplyEqualizer,
        this.menuItem4,
        this.menuItem6,
        this.menuItem7,
        this.menuItem8,
        this.menuItem9,
        this.menuEffectsApplyTempoChange,
        this.menuEffectsApplyRateChange,
        this.menuEffectsApplyPitchChange,
        this.menuItem10,
        this.menuEffectsReverseSound,
        this.menuItem11,
        this.menuItem12,
        this.menuItem14,
        this.menuItem13,
        this.menuEffectsDCOffsetRemoval,
        this.menuItem15,
        this.menuItem16,
        this.menuItem17,
        this.menuEffectsVocalRemoval
      });
      this.mnuEffects.Text = "Effe&cts";
      this.menuEffectsFlatVolume.Index = 0;
      this.menuEffectsFlatVolume.Text = "Apply &flat volume...";
      this.menuEffectsFlatVolume.Click += new System.EventHandler(this.menuEffectsFlatVolume_Click);
      this.menuEffectsSlidingVolume.Index = 1;
      this.menuEffectsSlidingVolume.Text = "Apply &sliding volume...";
      this.menuEffectsSlidingVolume.Click += new System.EventHandler(this.menuEffectsSlidingVolume_Click);
      this.menuEffectsVolumeAutomation.Enabled = false;
      this.menuEffectsVolumeAutomation.Index = 2;
      this.menuEffectsVolumeAutomation.Text = "Apply volume &automation...";
      this.menuEffectsVolumeAutomation.Click += new System.EventHandler(this.menuEffectsVolumeAutomation_Click);
      this.menuItem3.Index = 3;
      this.menuItem3.Text = "-";
      this.menuEffectsApplyWaveReverb.Index = 4;
      this.menuEffectsApplyWaveReverb.Text = "Apply &WavesReverb DirectX effect...";
      this.menuEffectsApplyWaveReverb.Click += new System.EventHandler(this.menuEffectsApplyWaveReverb_Click);
      this.menuItem5.Index = 5;
      this.menuItem5.Text = "-";
      this.menuEffectsApplyEqualizer.Index = 6;
      this.menuEffectsApplyEqualizer.Text = "Apply &Equalizer...";
      this.menuEffectsApplyEqualizer.Click += new System.EventHandler(this.menuEffectsApplyEqualizer_Click);
      this.menuItem4.Index = 7;
      this.menuItem4.Text = "-";
      this.menuItem6.Index = 8;
      this.menuItem6.MenuItems.AddRange(new MenuItem[5]
      {
        this.menuEffectsApplyReverbInternal,
        this.menuEffectsApplyReverbExternal,
        this.menuEffectsApplyBalanceInternal,
        this.menuEffectsApplyBalanceExternal,
        this.menuEffectsApplyBassBoostExternal
      });
      this.menuItem6.Text = "Custom DSPs";
      this.menuEffectsApplyReverbInternal.Index = 0;
      this.menuEffectsApplyReverbInternal.Text = "Apply &Reverb Custom DSP (internal)...";
      this.menuEffectsApplyReverbInternal.Click += new System.EventHandler(this.menuEffectsApplyReverbInternal_Click);
      this.menuEffectsApplyReverbExternal.Index = 1;
      this.menuEffectsApplyReverbExternal.Text = "Apply Reverb &Custom DSP (external)...";
      this.menuEffectsApplyReverbExternal.Click += new System.EventHandler(this.menuEffectsApplyReverbExternal_Click);
      this.menuEffectsApplyBalanceInternal.Index = 2;
      this.menuEffectsApplyBalanceInternal.Text = "Apply Balance Custom DSP (internal)...";
      this.menuEffectsApplyBalanceInternal.Click += new System.EventHandler(this.menuEffectsApplyBalanceInternal_Click);
      this.menuEffectsApplyBalanceExternal.Index = 3;
      this.menuEffectsApplyBalanceExternal.Text = "Apply Balance Custom DSP (external)...";
      this.menuEffectsApplyBalanceExternal.Click += new System.EventHandler(this.menuEffectsApplyBalanceExternal_Click);
      this.menuEffectsApplyBassBoostExternal.Index = 4;
      this.menuEffectsApplyBassBoostExternal.Text = "Apply &Bass Boost Custom DSP (external)...";
      this.menuEffectsApplyBassBoostExternal.Click += new System.EventHandler(this.menuEffectsApplyBassBoostExternal_Click);
      this.menuItem7.Index = 9;
      this.menuItem7.Text = "-";
      this.menuItem8.Index = 10;
      this.menuItem8.MenuItems.AddRange(new MenuItem[2]
      {
        this.menuEffectsApplyClassicEQ,
        this.menuEffectsApplyVstFromDisk
      });
      this.menuItem8.Text = "VST";
      this.menuEffectsApplyClassicEQ.Index = 0;
      this.menuEffectsApplyClassicEQ.Text = "Load and apply Reverber...";
      this.menuEffectsApplyClassicEQ.Click += new System.EventHandler(this.menuEffectsApplyClassicEQ_Click);
      this.menuEffectsApplyVstFromDisk.Index = 1;
      this.menuEffectsApplyVstFromDisk.Text = "Load and apply a VST from disk...";
      this.menuEffectsApplyVstFromDisk.Click += new System.EventHandler(this.menuEffectsApplyVstFromDisk_Click);
      this.menuItem9.Index = 11;
      this.menuItem9.Text = "-";
      this.menuEffectsApplyTempoChange.Index = 12;
      this.menuEffectsApplyTempoChange.Text = "Change Tempo...";
      this.menuEffectsApplyTempoChange.Click += new System.EventHandler(this.menuEffectsApplyTempoChange_Click);
      this.menuEffectsApplyRateChange.Index = 13;
      this.menuEffectsApplyRateChange.Text = "Change Playback rate...";
      this.menuEffectsApplyRateChange.Click += new System.EventHandler(this.menuEffectsApplyRateChange_Click);
      this.menuEffectsApplyPitchChange.Index = 14;
      this.menuEffectsApplyPitchChange.Text = "Change Pitch...";
      this.menuEffectsApplyPitchChange.Click += new System.EventHandler(this.menuEffectsApplyPitchChange_Click);
      this.menuItem10.Index = 15;
      this.menuItem10.Text = "-";
      this.menuEffectsReverseSound.Index = 16;
      this.menuEffectsReverseSound.Text = "Reverse sound";
      this.menuEffectsReverseSound.Click += new System.EventHandler(this.menuEffectsReverseSound_Click);
      this.menuItem11.Index = 17;
      this.menuItem11.Text = "-";
      this.menuItem12.Index = 18;
      this.menuItem12.MenuItems.AddRange(new MenuItem[4]
      {
        this.menuEffectsApplyLowPass,
        this.menuEffectsApplyHighPass,
        this.menuEffectsApplyBandPass,
        this.menuEffectsApplyBandStop
      });
      this.menuItem12.Text = "Filters";
      this.menuEffectsApplyLowPass.Index = 0;
      this.menuEffectsApplyLowPass.Text = "Low pass filter...";
      this.menuEffectsApplyLowPass.Click += new System.EventHandler(this.menuEffectsApplyLowPass_Click);
      this.menuEffectsApplyHighPass.Index = 1;
      this.menuEffectsApplyHighPass.Text = "High pass filter...";
      this.menuEffectsApplyHighPass.Click += new System.EventHandler(this.menuEffectsApplyHighPass_Click);
      this.menuEffectsApplyBandPass.Index = 2;
      this.menuEffectsApplyBandPass.Text = "Band pass filter...";
      this.menuEffectsApplyBandPass.Click += new System.EventHandler(this.menuEffectsApplyBandPass_Click);
      this.menuEffectsApplyBandStop.Index = 3;
      this.menuEffectsApplyBandStop.Text = "Band stop filter...";
      this.menuEffectsApplyBandStop.Click += new System.EventHandler(this.menuEffectsApplyBandStop_Click);
      this.menuItem14.Index = 19;
      this.menuItem14.Text = "-";
      this.menuItem13.Index = 20;
      this.menuItem13.MenuItems.AddRange(new MenuItem[2]
      {
        this.menuEffectsApplyNormalizePeak,
        this.menuEffectsApplyNormalizeTarget
      });
      this.menuItem13.Text = "Normalize";
      this.menuEffectsApplyNormalizePeak.Index = 0;
      this.menuEffectsApplyNormalizePeak.Text = "To highest peak in sound";
      this.menuEffectsApplyNormalizePeak.Click += new System.EventHandler(this.menuEffectsApplyNormalizePeak_Click);
      this.menuEffectsApplyNormalizeTarget.Index = 1;
      this.menuEffectsApplyNormalizeTarget.Text = "To target level...";
      this.menuEffectsApplyNormalizeTarget.Click += new System.EventHandler(this.menuEffectsApplyNormalizeTarget_Click);
      this.menuEffectsDCOffsetRemoval.Index = 21;
      this.menuEffectsDCOffsetRemoval.Text = "DC Offset removal";
      this.menuEffectsDCOffsetRemoval.Click += new System.EventHandler(this.menuEffectsDCOffsetRemoval_Click);
      this.menuItem15.Index = 22;
      this.menuItem15.Text = "-";
      this.menuItem16.Index = 23;
      this.menuItem16.MenuItems.AddRange(new MenuItem[2]
      {
        this.menuItemRemoveClicksPops,
        this.menuItemRemoveHiss
      });
      this.menuItem16.Text = "Noise removal";
      this.menuItemRemoveClicksPops.Index = 0;
      this.menuItemRemoveClicksPops.Text = "Remove clicks and pops...";
      this.menuItemRemoveClicksPops.Click += new System.EventHandler(this.menuItemRemoveClicksPops_Click);
      this.menuItemRemoveHiss.Index = 1;
      this.menuItemRemoveHiss.Text = "Remove hiss...";
      this.menuItemRemoveHiss.Click += new System.EventHandler(this.menuItemRemoveHiss_Click);
      this.menuItem17.Index = 24;
      this.menuItem17.Text = "-";
      this.menuEffectsVocalRemoval.Index = 25;
      this.menuEffectsVocalRemoval.Text = "Vocal removal";
      this.menuEffectsVocalRemoval.Click += new System.EventHandler(this.menuEffectsVocalRemoval_Click);
      this.mnuZoom.Index = 4;
      this.mnuZoom.MenuItems.AddRange(new MenuItem[4]
      {
        this.mnuZoomToSelection,
        this.mnuZoomToFullWaveform,
        this.mnuZoomIn,
        this.mnuZoomOut
      });
      this.mnuZoom.Text = "&Zoom";
      this.mnuZoomToSelection.Index = 0;
      this.mnuZoomToSelection.Text = "Zoom to &selection";
      this.mnuZoomToSelection.Click += new System.EventHandler(this.mnuZoomToSelection_Click);
      this.mnuZoomToFullWaveform.Index = 1;
      this.mnuZoomToFullWaveform.Text = "Zoom to full &waveform";
      this.mnuZoomToFullWaveform.Click += new System.EventHandler(this.mnuZoomToFullWaveform_Click);
      this.mnuZoomIn.Index = 2;
      this.mnuZoomIn.Text = "Zoom &in";
      this.mnuZoomIn.Click += new System.EventHandler(this.mnuZoomIn_Click);
      this.mnuZoomOut.Index = 3;
      this.mnuZoomOut.Text = "Zoom &out";
      this.mnuZoomOut.Click += new System.EventHandler(this.mnuZoomOut_Click);
      this.mnuHelp.Index = 5;
      this.mnuHelp.MenuItems.AddRange(new MenuItem[1]
      {
        this.mnuHelpAbout
      });
      this.mnuHelp.Text = "&Help";
      this.mnuHelpAbout.Index = 0;
      this.mnuHelpAbout.Text = "&About...";
      this.mnuHelpAbout.Click += new System.EventHandler(this.mnuHelpAbout_Click);
      this.TimerReload.Tick += new System.EventHandler(this.TimerReload_Tick);
      this.TimerMenuEnabler.Tick += new System.EventHandler(this.TimerMenuEnabler_Tick);
      this.progressBar1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.progressBar1.Location = new Point(80, 576);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new Size(204, 16);
      this.progressBar1.TabIndex = 19;
      this.progressBar1.Visible = false;
      this.timerDisplayWaveform.Tick += new System.EventHandler(this.timerDisplayWaveform_Tick);
      this.audioSoundEditor1.Location = new Point(16, 548);
      this.audioSoundEditor1.Name = "audioSoundEditor1";
      this.audioSoundEditor1.Size = new Size(48, 48);
      this.audioSoundEditor1.TabIndex = 23;
      this.audioSoundEditor1.WaveAnalysisStart += new AudioSoundEditor.AudioSoundEditor.EventHandler(this.audioSoundEditor1_WaveAnalysisStart);
      this.audioSoundEditor1.WaveAnalysisPerc += new AudioSoundEditor.AudioSoundEditor.WaveAnalysisPercEventHandler(this.audioSoundEditor1_WaveAnalysisPerc);
      this.audioSoundEditor1.WaveAnalysisDone += new AudioSoundEditor.AudioSoundEditor.WaveAnalysisDoneEventHandler(this.audioSoundEditor1_WaveAnalysisDone);
      this.audioSoundEditor1.SoundEditStarted += new AudioSoundEditor.AudioSoundEditor.SoundEditStartedEventHandler(this.audioSoundEditor1_SoundEditStarted);
      this.audioSoundEditor1.SoundEditPerc += new AudioSoundEditor.AudioSoundEditor.SoundEditPercEventHandler(this.audioSoundEditor1_SoundEditPerc);
      this.audioSoundEditor1.SoundEditDone += new AudioSoundEditor.AudioSoundEditor.SoundEditDoneEventHandler(this.audioSoundEditor1_SoundEditDone);
      this.audioSoundEditor1.WaveAnalyzerSelectionChange += new AudioSoundEditor.AudioSoundEditor.WaveAnalyzerSelectionChangeEventHandler(this.audioSoundEditor1_WaveAnalyzerSelectionChange);
      this.audioSoundEditor1.WaveAnalyzerDisplayRangeChange += new AudioSoundEditor.AudioSoundEditor.WaveAnalyzerDisplayRangeChangeEventHandler(this.audioSoundEditor1_WaveAnalyzerDisplayRangeChange);
      this.audioSoundEditor1.SoundExportStarted += new AudioSoundEditor.AudioSoundEditor.EventHandler(this.audioSoundEditor1_SoundExportStarted);
      this.audioSoundEditor1.SoundExportPerc += new AudioSoundEditor.AudioSoundEditor.SoundExportPercEventHandler(this.audioSoundEditor1_SoundExportPerc);
      this.audioSoundEditor1.SoundExportDone += new AudioSoundEditor.AudioSoundEditor.SoundExportDoneEventHandler(this.audioSoundEditor1_SoundExportDone);
      this.audioSoundEditor1.SoundPlaybackDone += new AudioSoundEditor.AudioSoundEditor.EventHandler(this.audioSoundEditor1_SoundPlaybackDone);
      this.audioSoundEditor1.SoundPlaybackStopped += new AudioSoundEditor.AudioSoundEditor.EventHandler(this.audioSoundEditor1_SoundPlaybackStopped);
      this.audioSoundEditor1.SoundPlaybackPaused += new AudioSoundEditor.AudioSoundEditor.EventHandler(this.audioSoundEditor1_SoundPlaybackPaused);
      this.audioSoundEditor1.SoundPlaybackPlaying += new AudioSoundEditor.AudioSoundEditor.EventHandler(this.audioSoundEditor1_SoundPlaybackPlaying);
      this.audioSoundEditor1.SoundLoadingStarted += new AudioSoundEditor.AudioSoundEditor.EventHandler(this.audioSoundEditor1_SoundLoadingStarted);
      this.audioSoundEditor1.SoundLoadingPerc += new AudioSoundEditor.AudioSoundEditor.SoundLoadingPercEventHandler(this.audioSoundEditor1_SoundLoadingPerc);
      this.audioSoundEditor1.SoundLoadingDone += new AudioSoundEditor.AudioSoundEditor.SoundLoadingDoneEventHandler(this.audioSoundEditor1_SoundLoadingDone);
      this.audioSoundEditor1.WaveScrollerManualScroll += new AudioSoundEditor.AudioSoundEditor.WaveScrollerManualScrollEventHandler(this.audioSoundEditor1_WaveScrollerManualScroll);
      this.audioSoundEditor1.WaveScrollerMouseNotification += new AudioSoundEditor.AudioSoundEditor.WaveScrollerMouseNotificationEventHandler(this.audioSoundEditor1_WaveScrollerMouseNotification);
      this.audioSoundEditor1.VUMeterValueChange += new AudioSoundEditor.AudioSoundEditor.VUMeterValueChangeEventHandler(this.audioSoundEditor1_VUMeterValueChange);
      this.timerPlaybackPos.Tick += new System.EventHandler(this.timerPlaybackPos_Tick);
      this.label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.label1.Location = new Point(696, 12);
      this.label1.Name = "label1";
      this.label1.Size = new Size(56, 52);
      this.label1.TabIndex = 24;
      this.label1.Text = "Waveform Vertical Zoom";
      this.label1.TextAlign = ContentAlignment.TopCenter;
      this.trackBar1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
      this.trackBar1.Location = new Point(700, 72);
      this.trackBar1.Maximum = 500;
      this.trackBar1.Minimum = 50;
      this.trackBar1.Name = "trackBar1";
      this.trackBar1.Orientation = Orientation.Vertical;
      this.trackBar1.Size = new Size(45, 224);
      this.trackBar1.TabIndex = 25;
      this.trackBar1.TickFrequency = 20;
      this.trackBar1.TickStyle = TickStyle.Both;
      this.trackBar1.Value = 100;
      this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
      this.label7.Anchor = AnchorStyles.Bottom;
      this.label7.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label7.ForeColor = Color.Black;
      this.label7.Location = new Point(157, 301);
      this.label7.Name = "label7";
      this.label7.Size = new Size(391, 17);
      this.label7.TabIndex = 148;
      this.label7.Text = "Use the mouse to manually scroll the waveform below or to pause playback";
      this.label7.TextAlign = ContentAlignment.MiddleCenter;
      this.pictureBoxWaveformScroll.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.pictureBoxWaveformScroll.BackColor = Color.Black;
      this.pictureBoxWaveformScroll.BorderStyle = BorderStyle.FixedSingle;
      this.pictureBoxWaveformScroll.Cursor = Cursors.Hand;
      this.pictureBoxWaveformScroll.ForeColor = SystemColors.WindowText;
      this.pictureBoxWaveformScroll.Location = new Point(12, 321);
      this.pictureBoxWaveformScroll.Name = "pictureBoxWaveformScroll";
      this.pictureBoxWaveformScroll.RightToLeft = RightToLeft.No;
      this.pictureBoxWaveformScroll.Size = new Size(680, 46);
      this.pictureBoxWaveformScroll.TabIndex = 147;
      this.pictureBoxWaveformScroll.TabStop = false;
      this.pictureBoxWaveformScroll.Visible = false;
      this.label18.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.label18.BackColor = Color.Black;
      this.label18.Location = new Point(708, 321);
      this.label18.Name = "label18";
      this.label18.Size = new Size(13, 275);
      this.label18.TabIndex = 150;
      this.label17.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.label17.BackColor = Color.Black;
      this.label17.Location = new Point(720, 321);
      this.label17.Name = "label17";
      this.label17.Size = new Size(13, 275);
      this.label17.TabIndex = 149;
      this.labelSpectrum.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.labelSpectrum.BackColor = Color.Black;
      this.labelSpectrum.Location = new Point(358, 373);
      this.labelSpectrum.Name = "labelSpectrum";
      this.labelSpectrum.Size = new Size(334, 223);
      this.labelSpectrum.TabIndex = 151;
      this.labelSpectrum.Visible = false;
      this.AutoScaleBaseSize = new Size(5, 13);
      this.ClientSize = new Size(756, 601);
      this.Controls.Add((Control) this.labelSpectrum);
      this.Controls.Add((Control) this.label18);
      this.Controls.Add((Control) this.label17);
      this.Controls.Add((Control) this.label7);
      this.Controls.Add((Control) this.pictureBoxWaveformScroll);
      this.Controls.Add((Control) this.trackBar1);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.audioSoundEditor1);
      this.Controls.Add((Control) this.progressBar1);
      this.Controls.Add((Control) this.Frame4);
      this.Controls.Add((Control) this.Frame5);
      this.Controls.Add((Control) this.Picture1);
      this.Controls.Add((Control) this.LabelStatus);
      this.Menu = this.MainMenu1;
      this.Name = nameof (FormMain);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Sound Editor";
      this.Load += new System.EventHandler(this.FormMain_Load);
      this.Resize += new System.EventHandler(this.FormMain_Resize);
      this.Frame4.ResumeLayout(false);
      this.Frame5.ResumeLayout(false);
      ((ISupportInitialize) this.Picture1).EndInit();
      this.trackBar1.EndInit();
      ((ISupportInitialize) this.pictureBoxWaveformScroll).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private IntPtr CreateVuMeter(Label ctrlPosition, enumGraphicBarOrientations nOrientation)
    {
      IntPtr hWndGraphicBar = this.audioSoundEditor1.GraphicBarsManager.Create(this.Handle, ctrlPosition.Left, ctrlPosition.Top, ctrlPosition.Width, ctrlPosition.Height);
      int num1 = (int) this.audioSoundEditor1.GraphicBarsManager.SetRange(hWndGraphicBar, 0, (int) short.MaxValue);
      GRAPHIC_BAR_SETTINGS settings = new GRAPHIC_BAR_SETTINGS();
      int graphicalSettings = (int) this.audioSoundEditor1.GraphicBarsManager.GetGraphicalSettings(hWndGraphicBar, ref settings);
      settings.bAutomaticDrop = true;
      settings.nOrientation = nOrientation;
      int num2 = (int) this.audioSoundEditor1.GraphicBarsManager.SetGraphicalSettings(hWndGraphicBar, settings);
      return hWndGraphicBar;
    }

    public static bool CheckKeyPress(TextBox textbox, int key)
    {
      if (key >= 48 && key <= 57)
        return false;
      char ch = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
      int int32 = Convert.ToInt32(ch);
      return key != int32 && key != 8 && key != 45 || key == int32 && textbox.Text.IndexOf(ch) != -1;
    }

    private void ReverbCallback(IntPtr bufferSamples, int bufferSamplesLength, int nUserData)
    {
      float[] numArray = new float[bufferSamplesLength / 4];
      Marshal.Copy(bufferSamples, numArray, 0, bufferSamplesLength / 4);
      int index = 0;
      while (index < bufferSamplesLength / 4)
      {
        float num1 = numArray[index] + this.m_buffReverbLeft[this.m_posReverb] / 2f;
        float num2 = numArray[index + 1] + this.m_buffReverbRight[this.m_posReverb] / 2f;
        this.m_buffReverbLeft[this.m_posReverb] = num1;
        numArray[index] = num1;
        this.m_buffReverbRight[this.m_posReverb] = num2;
        numArray[index + 1] = num2;
        ++this.m_posReverb;
        if (this.m_posReverb == this.BUFFERLEN)
          this.m_posReverb = 0;
        index += 2;
      }
      Marshal.Copy(numArray, 0, bufferSamples, bufferSamplesLength / 4);
    }

    private void BalanceCallback(IntPtr bufferSamples, int bufferSamplesLength, int nUserData)
    {
      if ((int) this.m_nBalancePercentageInternal == 0)
        return;
      float[] numArray = new float[bufferSamplesLength / 4];
      Marshal.Copy(bufferSamples, numArray, 0, bufferSamplesLength / 4);
      int num = bufferSamplesLength;
      int index = 0;
      do
      {
        if ((int) this.m_nBalancePercentageInternal < 0)
          numArray[index + 1] = (float) ((double) numArray[index + 1] * (double) (100 + (int) this.m_nBalancePercentageInternal) / 100.0);
        else
          numArray[index] = (float) ((double) numArray[index] * (double) (100 - (int) this.m_nBalancePercentageInternal) / 100.0);
        num -= 8;
        index += 2;
      }
      while (num > 0);
      Marshal.Copy(numArray, 0, bufferSamples, bufferSamplesLength / 4);
    }

    private void GetSelectedRange(ref int nBeginSelectionInMs, ref int nEndSelectionInMs)
    {
      bool bSelectionAvailable = false;
      int selection = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GetSelection(ref bSelectionAvailable, ref nBeginSelectionInMs, ref nEndSelectionInMs);
      if (!bSelectionAvailable)
      {
        nBeginSelectionInMs = 0;
        nEndSelectionInMs = -1;
      }
      if (nBeginSelectionInMs != nEndSelectionInMs)
        return;
      nEndSelectionInMs = -1;
    }

    private void FormMain_Load(object sender, EventArgs e)
    {
      int num1 = (int) this.audioSoundEditor1.InitEditor();
      this.audioSoundEditor1.UndoEnable(true);
      int num2 = (int) this.audioSoundEditor1.SetStoreMode(enumStoreModes.STORE_MODE_TEMP_FILE);
      int num3 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.Create(this.Handle, this.Picture1.Left, this.Picture1.Top, this.Picture1.Width, this.Picture1.Height);
      int num4 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.EnableScrollbarsDuringPlayback(true);
      WANALYZER_GENERAL_SETTINGS settings1 = new WANALYZER_GENERAL_SETTINGS();
      int num5 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsGeneralGet(ref settings1);
      settings1.bAutoRefresh = true;
      int num6 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsGeneralSet(settings1);
      WANALYZER_SCROLLBARS_SETTINGS settings2 = new WANALYZER_SCROLLBARS_SETTINGS();
      int num7 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsScrollbarsGet(ref settings2);
      settings2.bVisibleBottom = false;
      settings2.nHeightInPixels = (short) 40;
      settings2.nType = enumWaveScrollbarType.WSCROLLBAR_TYPE_WAVEFORM;
      settings2.nVisibleRangeType = enumScrollbarWaveVisibleRangeType.SCROLL_WAVE_VISIBLE_TRANSP_GLASS_OUTER;
      settings2.colorVisibleRange = Color.White;
      int num8 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsScrollbarsSet(settings2);
      WANALYZER_WAVEFORM_SETTINGS settings3 = new WANALYZER_WAVEFORM_SETTINGS();
      int num9 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsWaveGet(ref settings3);
      settings3.nSelectionMode = enumWaveformSelectionMode.WAVE_SEL_TRANSPARENT_GLASS;
      settings3.nTransparentGlassType = enumTranspGlassType.TRANSP_GLASS_TYPE_3D_VERT;
      settings3.nStereoVisualizationMode = enumWaveformStereoModes.STEREO_MODE_CHANNELS_BOTH;
      int num10 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsWaveSet(settings3);
      this.m_hWndWaveformScroller = this.audioSoundEditor1.DisplayWaveformScroller.Create(this.Handle, this.pictureBoxWaveformScroll.Left, this.pictureBoxWaveformScroll.Top, this.pictureBoxWaveformScroll.Width, this.pictureBoxWaveformScroll.Height);
      int num11 = (int) this.audioSoundEditor1.DisplayWaveformScroller.SetViewLength(this.m_hWndWaveformScroller, 20000);
      WSCROLLER_SETTINGS settings4 = new WSCROLLER_SETTINGS();
      int num12 = (int) this.audioSoundEditor1.DisplayWaveformScroller.SettingsGet(this.m_hWndWaveformScroller, ref settings4);
      settings4.bAppearance3d = false;
      settings4.colorPositionLine = Color.White;
      settings4.colorBorder = Color.Black;
      settings4.nUpdateSpeed = enumWaveScrollerAutoUpdate.WAVEFORMSCROLLER_UPDATE_FAST;
      settings4.nPositionLineWidth = (short) 2;
      int num13 = (int) this.audioSoundEditor1.DisplayWaveformScroller.SettingsSet(this.m_hWndWaveformScroller, settings4);
      int num14 = (int) this.audioSoundEditor1.DisplaySpectrumEnh.CreateNew(this.Handle, this.labelSpectrum.Left, this.labelSpectrum.Top, this.labelSpectrum.Width, this.labelSpectrum.Height);
      int num15 = (int) this.audioSoundEditor1.DisplaySpectrumEnh.Show(true);
      SPECTR_ENH_GENERAL_SETTINGS settings5 = new SPECTR_ENH_GENERAL_SETTINGS();
      int num16 = (int) this.audioSoundEditor1.DisplaySpectrumEnh.SettingsGeneralGet(ref settings5);
      settings5.nGraphType = enumSpectrumEnhTypes.SPECTR_ENH_AREA_LEFT_TOP;
      settings5.bBackBitmapVisible = false;
      settings5.nResolution = 8192;
      int num17 = (int) this.audioSoundEditor1.DisplaySpectrumEnh.SettingsGeneralSet(settings5);
      SPECTR_ENH_RULERS_SETTINGS settings6 = new SPECTR_ENH_RULERS_SETTINGS();
      int num18 = (int) this.audioSoundEditor1.DisplaySpectrumEnh.SettingsRulersGet(ref settings6, ref this.m_fontText);
      settings6.bScaleRightVisible = false;
      settings6.bScaleBottomVisible = false;
      int num19 = (int) this.audioSoundEditor1.DisplaySpectrumEnh.SettingsRulersSet(settings6, this.m_fontText);
      this.m_idDspReverbInternal = 0;
      this.m_idDspBalanceInternal = 0;
      this.m_idDspReverbExternal = 0;
      this.m_idDspBalanceExternal = 0;
      this.m_idDspBassBoostExternal = 0;
      this.m_idVstKarmaFxEq = 0;
      int num20 = (int) this.audioSoundEditor1.EnableSoundPreloadForPlayback(true);
      int num21 = (int) this.audioSoundEditor1.EnableAutoWaveAnalysisOnLoad(true);
      this.m_hWndVuMeterLeft = this.CreateVuMeter(this.label18, enumGraphicBarOrientations.GRAPHIC_BAR_ORIENT_VERTICAL);
      this.m_hWndVuMeterRight = this.CreateVuMeter(this.label17, enumGraphicBarOrientations.GRAPHIC_BAR_ORIENT_VERTICAL);
      this.TimerMenuEnabler.Enabled = true;
      this.Icon = Resources.AudioIcon;
    }

    private void buttonPlay_Click(object sender, EventArgs e)
    {
      bool bSelectionAvailable = false;
      int nBeginPosInMs = 0;
      int nEndPosInMs = 0;
      int selection = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GetSelection(ref bSelectionAvailable, ref nBeginPosInMs, ref nEndPosInMs);
      int num = (int) this.audioSoundEditor1.PlaySoundRange(nBeginPosInMs, -1);
    }

    private void buttonPlaySelection_Click(object sender, EventArgs e)
    {
      bool bSelectionAvailable = false;
      int nBeginPosInMs = 0;
      int nEndPosInMs = 0;
      int selection = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GetSelection(ref bSelectionAvailable, ref nBeginPosInMs, ref nEndPosInMs);
      if (!bSelectionAvailable)
        return;
      int num = (int) this.audioSoundEditor1.PlaySoundRange(nBeginPosInMs, nEndPosInMs);
    }

    private void buttonPause_Click(object sender, EventArgs e)
    {
      if (this.buttonPause.Text == "Pause")
      {
        int num1 = (int) this.audioSoundEditor1.PauseSound();
      }
      else
      {
        int num2 = (int) this.audioSoundEditor1.ResumeSound();
      }
    }

    private void buttonStop_Click(object sender, EventArgs e)
    {
      int num = (int) this.audioSoundEditor1.StopSound();
    }

    private void mnuFileLoad_Click(object sender, EventArgs e)
    {
      if (this.audioSoundEditor1.GetSoundDuration() > 0 && MessageBox.Show("A sound is currently in memory: do you want to create a new one?", "Sound Editor", MessageBoxButtons.YesNo) == DialogResult.No)
        return;
      int num1 = (int) this.audioSoundEditor1.CloseSound();
      this.LabelRangeBegin.Text = "00:00:00.000";
      this.LabelRangeEnd.Text = "00:00:00.000";
      this.LabelRangeDuration.Text = "00:00:00.000";
      this.LabelSelectionBegin.Text = "00:00:00.000";
      this.LabelSelectionEnd.Text = "00:00:00.000";
      this.LabelSelectionDuration.Text = "00:00:00.000";
      this.LabelTotalDuration.Text = "00:00:00.000";
      this.openFileDialog1.Filter = "Supported Sounds (*.mp3;*.mp2;*.wav;*.ogg;*.aiff;*.wma;*.wmv;*.asx;*.asf;*.m4a;*.mp4;*.flac;*.aac;*.ac3;*.wv;*.au;*.aif;*.w64;*.voc;*.sf;*.paf;*.pvf;*.caf;*.svx;*.ircam;*.mpc;*.spx;*.opus;*.ape;*.it;*.xm;*.s3m;*.mod;*.mtm;*.mo3;*.cda)|*.mp3;*.mp2;*.wav;*.ogg;*.aiff;*.wma;*.wmv;*.asx;*.asf;*.m4a;*.mp4;*.flac;*.aac;*.ac3;*.wv;*.au;*.aif;*.w64;*.voc;*.sf;*.paf;*.pvf;*.caf;*.svx;*.ircam;*.mpc;*.spx;*.opus;*.ape;*.it;*.xm;*.s3m;*.mod;*.mtm;*.mo3;*.cda|MP3 and MP2 sounds (*.mp3;*.mp2)|*.mp3;*.mp2|AAC and MP4 sounds (*.aac;*.mp4)|*.aac;*.mp4|WAV sounds (*.wav)|*.wav|OGG Vorbis sounds (*.ogg)|*.ogg|AIFF sounds (*.aiff)|*.aiff|Windows Media sounds (*.wma;*.wmv;*.asx;*.asf)|*.wma;*.wmv;*.asx;*.asf|AC3 sounds (*.ac3)|*.ac3;|ALAC sounds (*.m4a)|*.ac3;|FLAC sounds (*.flac)|*.flac;|WavPack sounds (*.wv)|*.wv;|Opus sounds (*.opus)|*.opus;|MOD music (*.it;*.xm;*.s3m;*.mod;*.mtm;*.mo3)|*.it;*.xm;*.s3m;*.mod;*.mtm;*.mo3|CD tracks (*.cda)|*.cda|All files (*.*)|*.*";
      this.openFileDialog1.Title = "Load a sound file";
      if (this.openFileDialog1.ShowDialog() == DialogResult.Cancel)
        return;
      int num2 = (int) this.audioSoundEditor1.SetLoadingMode(enumLoadingModes.LOAD_MODE_NEW);
      if (this.audioSoundEditor1.LoadSound(this.openFileDialog1.FileName) == enumErrorCodes.ERR_NOERROR)
        return;
      int num3 = (int) MessageBox.Show("Cannot load file " + this.openFileDialog1.FileName);
    }

    private void menuFileRawLoad_Click(object sender, EventArgs e)
    {
      if (this.audioSoundEditor1.GetSoundDuration() > 0 && MessageBox.Show("A sound is currently in memory: do you want to create a new one?", "Sound Editor", MessageBoxButtons.YesNo) == DialogResult.No)
        return;
      int num1 = (int) this.audioSoundEditor1.CloseSound();
      this.LabelRangeBegin.Text = "00:00:00.000";
      this.LabelRangeEnd.Text = "00:00:00.000";
      this.LabelRangeDuration.Text = "00:00:00.000";
      this.LabelSelectionBegin.Text = "00:00:00.000";
      this.LabelSelectionEnd.Text = "00:00:00.000";
      this.LabelSelectionDuration.Text = "00:00:00.000";
      this.LabelTotalDuration.Text = "00:00:00.000";
      this.openFileDialog1.Filter = "Raw formats (*.raw;*.vox)|*.raw;*.vox|All files (*.*)|*.*";
      this.openFileDialog1.Title = "Load a raw file";
      if (this.openFileDialog1.ShowDialog() == DialogResult.Cancel)
        return;
      FormRawCodecSettings rawCodecSettings = new FormRawCodecSettings();
      rawCodecSettings.audioSoundEditor1 = this.audioSoundEditor1;
      int num2 = (int) rawCodecSettings.ShowDialog((IWin32Window) this);
      if (rawCodecSettings.m_bCancel)
        return;
      int num3 = (int) this.audioSoundEditor1.SetLoadingMode(enumLoadingModes.LOAD_MODE_NEW);
      if (this.audioSoundEditor1.LoadSoundFromRawFile(this.openFileDialog1.FileName, rawCodecSettings.m_nEncodeMode, rawCodecSettings.m_bIsBigEndian, rawCodecSettings.m_nSamplerate, rawCodecSettings.m_nChannels) == enumErrorCodes.ERR_NOERROR)
        return;
      int num4 = (int) MessageBox.Show("Cannot load file " + this.openFileDialog1.FileName);
    }

    private void mnuFileLoadFromMemory_Click(object sender, EventArgs e)
    {
      if (this.audioSoundEditor1.GetSoundDuration() > 0 && MessageBox.Show("A sound is currently in memory: do you want to create a new one?", "Sound Editor", MessageBoxButtons.YesNo) == DialogResult.No)
        return;
      int num1 = (int) this.audioSoundEditor1.CloseSound();
      this.LabelRangeBegin.Text = "00:00:00.000";
      this.LabelRangeEnd.Text = "00:00:00.000";
      this.LabelRangeDuration.Text = "00:00:00.000";
      this.LabelSelectionBegin.Text = "00:00:00.000";
      this.LabelSelectionEnd.Text = "00:00:00.000";
      this.LabelSelectionDuration.Text = "00:00:00.000";
      this.LabelTotalDuration.Text = "00:00:00.000";
      this.openFileDialog1.Filter = "Supported Sounds (*.mp3;*.mp2;*.wav;*.ogg;*.aiff;*.wma;*.wmv;*.asx;*.asf;*.m4a;*.mp4;*.flac;*.aac;*.ac3;*.wv;*.au;*.aif;*.w64;*.voc;*.sf;*.paf;*.pvf;*.caf;*.svx ;*.it;*.xm;*.s3m;*.mod;*.mtm;*.mo3;*.cda)|*.mp3;*.mp2;*.wav;*.ogg;*.aiff;*.wma;*.wmv;*.asx;*.asf;*.m4a;*.mp4;*.flac;*.aac;*.ac3;*.wv;*.au;*.aif;*.w64;*.voc;*.sf;*.paf;*.pvf;*.caf;*.svx ;*.it;*.xm;*.s3m;*.mod;*.mtm;*.mo3;*.cda|MP3 and MP2 sounds (*.mp3;*.mp2)|*.mp3;*.mp2|AAC and MP4 sounds (*.aac;*.mp4)|*.aac;*.mp4|WAV sounds (*.wav)|*.wav|OGG Vorbis sounds (*.ogg)|*.ogg|AIFF sounds (*.aiff)|*.aiff|Windows Media sounds (*.wma;*.wmv;*.asx;*.asf)|*.wma;*.wmv;*.asx;*.asf|AC3 sounds (*.ac3)|*.ac3;|ALAC sounds (*.m4a)|*.ac3;|FLAC sounds (*.flac)|*.flac;|WavPack sounds (*.wv)|*.wv;|All files (*.*)|*.*";
      this.openFileDialog1.Title = "Preload a sound file";
      if (this.openFileDialog1.ShowDialog() == DialogResult.Cancel)
        return;
      FileStream fileStream = new FileStream(this.openFileDialog1.FileName, FileMode.Open);
      BinaryReader binaryReader = new BinaryReader((Stream) fileStream);
      this.m_byteBuffer = new byte[fileStream.Length];
      binaryReader.Read(this.m_byteBuffer, 0, (int) fileStream.Length);
      int num2 = (int) this.audioSoundEditor1.SetLoadingMode(enumLoadingModes.LOAD_MODE_NEW);
      if (this.audioSoundEditor1.LoadSoundFromMemory(this.m_byteBuffer, (int) fileStream.Length) != enumErrorCodes.ERR_NOERROR)
      {
        int num3 = (int) MessageBox.Show("Cannot load file " + this.openFileDialog1.FileName);
      }
      binaryReader.Close();
      fileStream.Close();
    }

    private void menuFileRawLoadFromMemory_Click(object sender, EventArgs e)
    {
      if (this.audioSoundEditor1.GetSoundDuration() > 0 && MessageBox.Show("A sound is currently in memory: do you want to create a new one?", "Sound Editor", MessageBoxButtons.YesNo) == DialogResult.No)
        return;
      int num1 = (int) this.audioSoundEditor1.CloseSound();
      this.LabelRangeBegin.Text = "00:00:00.000";
      this.LabelRangeEnd.Text = "00:00:00.000";
      this.LabelRangeDuration.Text = "00:00:00.000";
      this.LabelSelectionBegin.Text = "00:00:00.000";
      this.LabelSelectionEnd.Text = "00:00:00.000";
      this.LabelSelectionDuration.Text = "00:00:00.000";
      this.LabelTotalDuration.Text = "00:00:00.000";
      this.openFileDialog1.Filter = "Raw formats (*.raw;*.vox)|*.raw;*.vox|All files (*.*)|*.*";
      this.openFileDialog1.Title = "Load a raw file";
      if (this.openFileDialog1.ShowDialog() == DialogResult.Cancel)
        return;
      FormRawCodecSettings rawCodecSettings = new FormRawCodecSettings();
      rawCodecSettings.audioSoundEditor1 = this.audioSoundEditor1;
      int num2 = (int) rawCodecSettings.ShowDialog((IWin32Window) this);
      if (rawCodecSettings.m_bCancel)
        return;
      FileStream fileStream = new FileStream(this.openFileDialog1.FileName, FileMode.Open);
      BinaryReader binaryReader = new BinaryReader((Stream) fileStream);
      this.m_byteBuffer = new byte[fileStream.Length];
      binaryReader.Read(this.m_byteBuffer, 0, (int) fileStream.Length);
      int num3 = (int) this.audioSoundEditor1.SetLoadingMode(enumLoadingModes.LOAD_MODE_NEW);
      if (this.audioSoundEditor1.LoadSoundFromRawMemory(this.m_byteBuffer, (int) fileStream.Length, rawCodecSettings.m_nEncodeMode, rawCodecSettings.m_bIsBigEndian, rawCodecSettings.m_nSamplerate, rawCodecSettings.m_nChannels) != enumErrorCodes.ERR_NOERROR)
      {
        int num4 = (int) MessageBox.Show("Cannot load file " + this.openFileDialog1.FileName);
      }
      binaryReader.Close();
      fileStream.Close();
    }

    private void mnuFileExport_Click(object sender, EventArgs e)
    {
      FormExport formExport = new FormExport();
      formExport.audioSoundEditor1 = this.audioSoundEditor1;
      int num = (int) formExport.ShowDialog((IWin32Window) this);
      this.m_strExportPathname = formExport.m_strExportPathname;
    }

    private void mnuFileExit_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void mnuEditCut_Click(object sender, EventArgs e)
    {
      bool bSelectionAvailable = false;
      int nBeginPosInMs = 0;
      int nEndPosInMs = 0;
      int selection = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GetSelection(ref bSelectionAvailable, ref nBeginPosInMs, ref nEndPosInMs);
      if (!bSelectionAvailable || nBeginPosInMs == nEndPosInMs)
        return;
      this.mnuEditCopy_Click(sender, e);
      this.mnuEditDeleteSel_Click(sender, e);
    }

    private void mnuEditCopy_Click(object sender, EventArgs e)
    {
      bool bSelectionAvailable = false;
      int nBeginPosInMs = 0;
      int nEndPosInMs = 0;
      int selection = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GetSelection(ref bSelectionAvailable, ref nBeginPosInMs, ref nEndPosInMs);
      if (!bSelectionAvailable || nBeginPosInMs == nEndPosInMs)
        return;
      int clipboard = (int) this.audioSoundEditor1.CopyRangeToClipboard(nBeginPosInMs, nEndPosInMs);
    }

    private void mnuEditPaste_Click(object sender, EventArgs e)
    {
      if (this.audioSoundEditor1.GetSoundDuration() > 0)
      {
        int num1 = (int) this.audioSoundEditor1.SetLoadingMode(enumLoadingModes.LOAD_MODE_APPEND);
      }
      else
      {
        int num2 = (int) this.audioSoundEditor1.SetLoadingMode(enumLoadingModes.LOAD_MODE_NEW);
      }
      if (this.audioSoundEditor1.LoadSoundFromClipboard() == enumErrorCodes.ERR_NOERROR)
        return;
      int num3 = (int) MessageBox.Show("Cannot load from system clipboard");
    }

    private void mnuEditPasteInsert_Click(object sender, EventArgs e)
    {
      bool bSelectionAvailable = false;
      int nBeginPosInMs = 0;
      int nEndPosInMs = 0;
      int selection = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GetSelection(ref bSelectionAvailable, ref nBeginPosInMs, ref nEndPosInMs);
      if (bSelectionAvailable)
      {
        int num1 = (int) this.audioSoundEditor1.SetInsertPos(nBeginPosInMs);
      }
      else
      {
        int num2 = (int) this.audioSoundEditor1.SetInsertPos(0);
      }
      int num3 = (int) this.audioSoundEditor1.SetLoadingMode(enumLoadingModes.LOAD_MODE_INSERT);
      if (this.audioSoundEditor1.LoadSoundFromClipboard() == enumErrorCodes.ERR_NOERROR)
        return;
      int num4 = (int) MessageBox.Show("Cannot paste from system clipboard");
    }

    private void mnuEditPasteMix_Click(object sender, EventArgs e)
    {
      int nBeginSelectionInMs = 0;
      int nEndSelectionInMs = 0;
      this.GetSelectedRange(ref nBeginSelectionInMs, ref nEndSelectionInMs);
      int num1 = (int) this.audioSoundEditor1.SetMixingPos(nBeginSelectionInMs, nEndSelectionInMs);
      int num2 = (int) this.audioSoundEditor1.SetLoadingMode(enumLoadingModes.LOAD_MODE_MIX);
      if (this.audioSoundEditor1.LoadSoundFromClipboard() == enumErrorCodes.ERR_NOERROR)
        return;
      int num3 = (int) MessageBox.Show("Cannot paste from system clipboard");
    }

    private void mnuEditDeleteSel_Click(object sender, EventArgs e)
    {
      bool bSelectionAvailable = false;
      int nBeginPosInMs = 0;
      int nEndPosInMs = 0;
      int selection = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GetSelection(ref bSelectionAvailable, ref nBeginPosInMs, ref nEndPosInMs);
      if (!bSelectionAvailable)
        return;
      int num1 = (int) this.audioSoundEditor1.DeleteRange(nBeginPosInMs, nEndPosInMs);
      int num2 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SetSelection(false, 0, 0);
    }

    private void mnuEditReduceToSel_Click(object sender, EventArgs e)
    {
      bool bSelectionAvailable = false;
      int nBeginPosInMs = 0;
      int nEndPosInMs = 0;
      int selection = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GetSelection(ref bSelectionAvailable, ref nBeginPosInMs, ref nEndPosInMs);
      if (!bSelectionAvailable)
        return;
      int num = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SetSelection(false, 0, 0);
      int range = (int) this.audioSoundEditor1.ReduceToRange(nBeginPosInMs, nEndPosInMs);
    }

    private void mnuEditSelectAll_Click(object sender, EventArgs e)
    {
      int num = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SetSelection(true, 0, -1);
    }

    private void mnuEditRemoveSel_Click(object sender, EventArgs e)
    {
      int num = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SetSelection(false, 0, -1);
    }

    private void mnuToolsApplyBackground_Click(object sender, EventArgs e)
    {
      FormChooseBackgroundFile chooseBackgroundFile = new FormChooseBackgroundFile();
      if (chooseBackgroundFile.ShowDialog((IWin32Window) this) == DialogResult.Cancel || chooseBackgroundFile.m_strPathname == "")
        return;
      int num1 = (int) this.audioSoundEditor1.SetLoadingMode(enumLoadingModes.LOAD_MODE_MIX);
      bool bSelectionAvailable = false;
      int nBeginPosInMs = 0;
      int nEndPosInMs = 0;
      int selection = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GetSelection(ref bSelectionAvailable, ref nBeginPosInMs, ref nEndPosInMs);
      if (!bSelectionAvailable)
      {
        nBeginPosInMs = 0;
        nEndPosInMs = -1;
      }
      else if (nBeginPosInMs == nEndPosInMs)
        nEndPosInMs = -1;
      int num2 = (int) this.audioSoundEditor1.SetMixingPos(nBeginPosInMs, nEndPosInMs);
      int num3 = (int) this.audioSoundEditor1.SetMixingParams(false, chooseBackgroundFile.m_bLoop, 100f, 100f, enumVolumeScales.SCALE_LINEAR);
      if (this.audioSoundEditor1.LoadSound(chooseBackgroundFile.m_strPathname) == enumErrorCodes.ERR_NOERROR)
        return;
      int num4 = (int) MessageBox.Show("Cannot load file");
    }

    private void mnuToolsInsertSilence_Click(object sender, EventArgs e)
    {
      FormSilence formSilence = new FormSilence();
      if (formSilence.ShowDialog((IWin32Window) this) == DialogResult.Cancel || formSilence.m_nSilenceLengthInMs == -1)
        return;
      bool bSelectionAvailable = false;
      int nBeginPosInMs = 0;
      int nEndPosInMs = 0;
      int selection = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GetSelection(ref bSelectionAvailable, ref nBeginPosInMs, ref nEndPosInMs);
      int num1 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SetSelection(false, 0, 0);
      if (bSelectionAvailable)
      {
        int num2 = (int) this.audioSoundEditor1.InsertSilence(nBeginPosInMs, formSilence.m_nSilenceLengthInMs);
      }
      else
      {
        int num3 = (int) this.audioSoundEditor1.InsertSilence(0, formSilence.m_nSilenceLengthInMs);
      }
    }

    private void mnuToolsAppendSound_Click(object sender, EventArgs e)
    {
      this.openFileDialog1.Filter = "Supported Sounds (*.mp3;*.mp2;*.wav;*.ogg;*.aiff;*.wma;*.wmv;*.asx;*.asf;*.m4a;*.mp4;*.flac;*.aac;*.ac3;*.wv;*.au;*.aif;*.w64;*.voc;*.sf;*.paf;*.pvf;*.caf;*.svx ;*.it;*.xm;*.s3m;*.mod;*.mtm;*.mo3;*.cda)|*.mp3;*.mp2;*.wav;*.ogg;*.aiff;*.wma;*.wmv;*.asx;*.asf;*.m4a;*.mp4;*.flac;*.aac;*.ac3;*.wv;*.au;*.aif;*.w64;*.voc;*.sf;*.paf;*.pvf;*.caf;*.svx ;*.it;*.xm;*.s3m;*.mod;*.mtm;*.mo3;*.cda|MP3 and MP2 sounds (*.mp3;*.mp2)|*.mp3;*.mp2|AAC and MP4 sounds (*.aac;*.mp4)|*.aac;*.mp4|WAV sounds (*.wav)|*.wav|OGG Vorbis sounds (*.ogg)|*.ogg|AIFF sounds (*.aiff)|*.aiff|Windows Media sounds (*.wma;*.wmv;*.asx;*.asf)|*.wma;*.wmv;*.asx;*.asf|AC3 sounds (*.ac3)|*.ac3;|ALAC sounds (*.m4a)|*.ac3;|FLAC sounds (*.flac)|*.flac;|WavPack sounds (*.wv)|*.wv;|All files (*.*)|*.*";
      this.openFileDialog1.Title = "Load a sound file";
      if (this.openFileDialog1.ShowDialog() == DialogResult.Cancel)
        return;
      int num1 = (int) this.audioSoundEditor1.SetLoadingMode(enumLoadingModes.LOAD_MODE_APPEND);
      if (this.audioSoundEditor1.LoadSound(this.openFileDialog1.FileName) == enumErrorCodes.ERR_NOERROR)
        return;
      int num2 = (int) MessageBox.Show("Cannot load file " + this.openFileDialog1.FileName);
    }

    private void mnuToolsInsertSound_Click(object sender, EventArgs e)
    {
      this.openFileDialog1.Filter = "Supported Sounds (*.mp3;*.mp2;*.wav;*.ogg;*.aiff;*.wma;*.wmv;*.asx;*.asf;*.m4a;*.mp4;*.flac;*.aac;*.ac3;*.wv;*.au;*.aif;*.w64;*.voc;*.sf;*.paf;*.pvf;*.caf;*.svx ;*.it;*.xm;*.s3m;*.mod;*.mtm;*.mo3;*.cda)|*.mp3;*.mp2;*.wav;*.ogg;*.aiff;*.wma;*.wmv;*.asx;*.asf;*.m4a;*.mp4;*.flac;*.aac;*.ac3;*.wv;*.au;*.aif;*.w64;*.voc;*.sf;*.paf;*.pvf;*.caf;*.svx ;*.it;*.xm;*.s3m;*.mod;*.mtm;*.mo3;*.cda|MP3 and MP2 sounds (*.mp3;*.mp2)|*.mp3;*.mp2|AAC and MP4 sounds (*.aac;*.mp4)|*.aac;*.mp4|WAV sounds (*.wav)|*.wav|OGG Vorbis sounds (*.ogg)|*.ogg|AIFF sounds (*.aiff)|*.aiff|Windows Media sounds (*.wma;*.wmv;*.asx;*.asf)|*.wma;*.wmv;*.asx;*.asf|AC3 sounds (*.ac3)|*.ac3;|ALAC sounds (*.m4a)|*.ac3;|FLAC sounds (*.flac)|*.flac;|WavPack sounds (*.wv)|*.wv;|All files (*.*)|*.*";
      this.openFileDialog1.Title = "Load a sound file";
      if (this.openFileDialog1.ShowDialog() == DialogResult.Cancel)
        return;
      bool bSelectionAvailable = false;
      int nBeginPosInMs = 0;
      int nEndPosInMs = 0;
      int selection = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GetSelection(ref bSelectionAvailable, ref nBeginPosInMs, ref nEndPosInMs);
      if (bSelectionAvailable)
      {
        int num1 = (int) this.audioSoundEditor1.SetInsertPos(nBeginPosInMs);
      }
      else
      {
        int num2 = (int) this.audioSoundEditor1.SetInsertPos(0);
      }
      int num3 = (int) this.audioSoundEditor1.SetLoadingMode(enumLoadingModes.LOAD_MODE_INSERT);
      if (this.audioSoundEditor1.LoadSound(this.openFileDialog1.FileName) == enumErrorCodes.ERR_NOERROR)
        return;
      int num4 = (int) MessageBox.Show("Cannot load file " + this.openFileDialog1.FileName);
    }

    private void mnuToolsMixSound_Click(object sender, EventArgs e)
    {
      this.openFileDialog1.Filter = "Supported Sounds (*.mp3;*.mp2;*.wav;*.ogg;*.aiff;*.wma;*.wmv;*.asx;*.asf;*.m4a;*.mp4;*.flac;*.aac;*.ac3;*.wv;*.au;*.aif;*.w64;*.voc;*.sf;*.paf;*.pvf;*.caf;*.svx ;*.it;*.xm;*.s3m;*.mod;*.mtm;*.mo3;*.cda)|*.mp3;*.mp2;*.wav;*.ogg;*.aiff;*.wma;*.wmv;*.asx;*.asf;*.m4a;*.mp4;*.flac;*.aac;*.ac3;*.wv;*.au;*.aif;*.w64;*.voc;*.sf;*.paf;*.pvf;*.caf;*.svx ;*.it;*.xm;*.s3m;*.mod;*.mtm;*.mo3;*.cda|MP3 and MP2 sounds (*.mp3;*.mp2)|*.mp3;*.mp2|AAC and MP4 sounds (*.aac;*.mp4)|*.aac;*.mp4|WAV sounds (*.wav)|*.wav|OGG Vorbis sounds (*.ogg)|*.ogg|AIFF sounds (*.aiff)|*.aiff|Windows Media sounds (*.wma;*.wmv;*.asx;*.asf)|*.wma;*.wmv;*.asx;*.asf|AC3 sounds (*.ac3)|*.ac3;|ALAC sounds (*.m4a)|*.ac3;|FLAC sounds (*.flac)|*.flac;|WavPack sounds (*.wv)|*.wv;|All files (*.*)|*.*";
      this.openFileDialog1.Title = "Load a sound file";
      if (this.openFileDialog1.ShowDialog() == DialogResult.Cancel)
        return;
      int nBeginSelectionInMs = 0;
      int nEndSelectionInMs = 0;
      this.GetSelectedRange(ref nBeginSelectionInMs, ref nEndSelectionInMs);
      int num1 = (int) this.audioSoundEditor1.SetMixingPos(nBeginSelectionInMs, nEndSelectionInMs);
      int num2 = (int) this.audioSoundEditor1.SetLoadingMode(enumLoadingModes.LOAD_MODE_MIX);
      if (this.audioSoundEditor1.LoadSound(this.openFileDialog1.FileName) == enumErrorCodes.ERR_NOERROR)
        return;
      int num3 = (int) MessageBox.Show("Cannot load file " + this.openFileDialog1.FileName);
    }

    private void menuToolsOverwrite_Click(object sender, EventArgs e)
    {
      this.openFileDialog1.Filter = "Supported Sounds (*.mp3;*.mp2;*.wav;*.ogg;*.aiff;*.wma;*.wmv;*.asx;*.asf;*.m4a;*.mp4;*.flac;*.aac;*.ac3;*.wv;*.au;*.aif;*.w64;*.voc;*.sf;*.paf;*.pvf;*.caf;*.svx ;*.it;*.xm;*.s3m;*.mod;*.mtm;*.mo3;*.cda)|*.mp3;*.mp2;*.wav;*.ogg;*.aiff;*.wma;*.wmv;*.asx;*.asf;*.m4a;*.mp4;*.flac;*.aac;*.ac3;*.wv;*.au;*.aif;*.w64;*.voc;*.sf;*.paf;*.pvf;*.caf;*.svx ;*.it;*.xm;*.s3m;*.mod;*.mtm;*.mo3;*.cda|MP3 and MP2 sounds (*.mp3;*.mp2)|*.mp3;*.mp2|AAC and MP4 sounds (*.aac;*.mp4)|*.aac;*.mp4|WAV sounds (*.wav)|*.wav|OGG Vorbis sounds (*.ogg)|*.ogg|AIFF sounds (*.aiff)|*.aiff|Windows Media sounds (*.wma;*.wmv;*.asx;*.asf)|*.wma;*.wmv;*.asx;*.asf|AC3 sounds (*.ac3)|*.ac3;|ALAC sounds (*.m4a)|*.ac3;|FLAC sounds (*.flac)|*.flac;|WavPack sounds (*.wv)|*.wv;|All files (*.*)|*.*";
      this.openFileDialog1.Title = "Load a sound file";
      if (this.openFileDialog1.ShowDialog() == DialogResult.Cancel)
        return;
      int nBeginSelectionInMs = 0;
      int nEndSelectionInMs = 0;
      this.GetSelectedRange(ref nBeginSelectionInMs, ref nEndSelectionInMs);
      int num1 = (int) this.audioSoundEditor1.SetOverwritePos(nBeginSelectionInMs, nEndSelectionInMs);
      int num2 = (int) this.audioSoundEditor1.SetLoadingMode(enumLoadingModes.LOAD_MODE_OVERWRITE);
      if (this.audioSoundEditor1.LoadSound(this.openFileDialog1.FileName) == enumErrorCodes.ERR_NOERROR)
        return;
      int num3 = (int) MessageBox.Show("Cannot load file " + this.openFileDialog1.FileName);
    }

    private void mnuToolsOptions_Click(object sender, EventArgs e)
    {
      int num = (int) new FormOptions()
      {
        audioSoundEditor1 = this.audioSoundEditor1
      }.ShowDialog((IWin32Window) this);
    }

    private void mnuWaveformOptions_Click(object sender, EventArgs e)
    {
      int num = (int) new FormWaveformSettings()
      {
        audioSoundEditor1 = this.audioSoundEditor1
      }.ShowDialog();
    }

    private void mnuZoomToSelection_Click(object sender, EventArgs e)
    {
      int selection = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.ZoomToSelection(true);
    }

    private void mnuZoomToFullWaveform_Click(object sender, EventArgs e)
    {
      int fullSound = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.ZoomToFullSound();
    }

    private void mnuZoomIn_Click(object sender, EventArgs e)
    {
      int num = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.ZoomIn();
    }

    private void mnuZoomOut_Click(object sender, EventArgs e)
    {
      int num = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.ZoomOut();
    }

    private void mnuHelpAbout_Click(object sender, EventArgs e)
    {
    }

    private void TimerMenuEnabler_Tick(object sender, EventArgs e)
    {
      this.mnuEditUndo.Enabled = this.audioSoundEditor1.UndoIsAvailable();
      if (this.audioSoundEditor1.IsSoundAvailableInClipboard())
      {
        this.mnuEditPaste.Enabled = true;
        if (this.audioSoundEditor1.GetSoundDuration() > 0)
        {
          this.mnuEditPaste.Text = "Paste in '&Append mode'";
          this.mnuEditPasteInsert.Visible = true;
          this.mnuEditPasteMix.Visible = true;
        }
        else
        {
          this.mnuEditPaste.Text = "&Paste";
          this.mnuEditPasteInsert.Visible = false;
          this.mnuEditPasteMix.Visible = false;
        }
      }
      else
      {
        this.mnuEditPaste.Enabled = false;
        this.mnuEditPasteInsert.Visible = false;
        this.mnuEditPasteMix.Visible = false;
        this.mnuEditPaste.Text = "Paste";
      }
      bool bSelectionAvailable = false;
      int nBeginPosInMs = 0;
      int nEndPosInMs = 0;
      int selection = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GetSelection(ref bSelectionAvailable, ref nBeginPosInMs, ref nEndPosInMs);
      if (bSelectionAvailable)
      {
        this.mnuEditCut.Enabled = true;
        this.mnuEditCopy.Enabled = true;
        this.mnuEditDeleteSel.Enabled = true;
        this.mnuEditReduceToSel.Enabled = true;
        this.mnuEditRemoveSel.Enabled = true;
        this.mnuZoomToSelection.Enabled = true;
      }
      else
      {
        this.mnuEditCut.Enabled = false;
        this.mnuEditCopy.Enabled = false;
        this.mnuEditDeleteSel.Enabled = false;
        this.mnuEditReduceToSel.Enabled = false;
        this.mnuEditRemoveSel.Enabled = false;
        this.mnuZoomToSelection.Enabled = false;
      }
      if (this.audioSoundEditor1.GetSoundDuration() > 0)
      {
        this.mnuEditSelectAll.Enabled = true;
        this.mnuToolsInsertSilence.Enabled = true;
        this.mnuToolsApplyBackground.Enabled = true;
        this.mnuFileExport.Enabled = true;
        this.mnuEffects.Enabled = true;
        this.mnuZoom.Enabled = true;
      }
      else
      {
        this.mnuEditSelectAll.Enabled = false;
        this.mnuToolsInsertSilence.Enabled = false;
        this.mnuToolsApplyBackground.Enabled = false;
        this.mnuFileExport.Enabled = false;
        this.mnuEffects.Enabled = false;
        this.mnuZoom.Enabled = false;
      }
    }

    private void TimerReload_Tick(object sender, EventArgs e)
    {
      this.TimerReload.Enabled = false;
      int soundDuration = this.audioSoundEditor1.GetSoundDuration();
      if (soundDuration == 0)
      {
        int num = (int) this.audioSoundEditor1.CloseSound();
        this.LabelRangeBegin.Text = "00:00:00.000";
        this.LabelRangeEnd.Text = "00:00:00.000";
        this.LabelRangeDuration.Text = "00:00:00.000";
        this.LabelSelectionBegin.Text = "00:00:00.000";
        this.LabelSelectionEnd.Text = "00:00:00.000";
        this.LabelSelectionDuration.Text = "00:00:00.000";
        this.LabelTotalDuration.Text = "00:00:00.000";
      }
      else
      {
        this.LabelTotalDuration.Text = this.audioSoundEditor1.GetFormattedTime(soundDuration, true, true);
        this.LabelTotalDuration.Refresh();
        int num = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.AnalyzeFullSound();
        if (this.audioSoundEditor1.GetStoreMode() == enumStoreModes.STORE_MODE_MEMORY_BUFFER)
          ;
      }
    }

    private void timerDisplayWaveform_Tick(object sender, EventArgs e)
    {
      this.timerDisplayWaveform.Enabled = false;
      int num = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SetDisplayRange(0, -1);
    }

    private void audioSoundEditor1_SoundExportStarted(object sender, EventArgs e)
    {
      this.LabelStatus.Text = "Status: Exporting...";
      this.progressBar1.Visible = true;
      this.LabelStatus.Refresh();
    }

    private void audioSoundEditor1_SoundExportPerc(object sender, SoundExportPercEventArgs e)
    {
      if (this.progressBar1.Value == (int) e.nPercentage)
        return;
      this.progressBar1.Value = (int) e.nPercentage;
      this.LabelStatus.Text = "Status: Exporting... " + e.nPercentage.ToString() + "%";
      this.progressBar1.Refresh();
      this.LabelStatus.Refresh();
    }

    private void audioSoundEditor1_SoundExportDone(object sender, SoundExportDoneEventArgs e)
    {
      this.LabelStatus.Text = "Status: Idle";
      this.progressBar1.Visible = false;
      this.LabelStatus.Refresh();
      if (e.nResult != enumErrorCodes.ERR_NOERROR)
      {
        int num1 = (int) MessageBox.Show("Export failed with the following error code: " + e.nResult.ToString());
      }
      else
      {
        int num2 = (int) MessageBox.Show("Editing session exported to " + this.m_strExportPathname);
      }
    }

    private void audioSoundEditor1_SoundEditStarted(object sender, SoundEditStartedEventArgs e)
    {
      this.LabelStatus.Text = "Status: Editing ...";
      this.progressBar1.Visible = true;
      this.LabelStatus.Refresh();
      this.progressBar1.Refresh();
    }

    private void audioSoundEditor1_SoundEditPerc(object sender, SoundEditPercEventArgs e)
    {
      if (this.progressBar1.Value == (int) e.nPercentage)
        return;
      this.progressBar1.Value = (int) e.nPercentage;
      this.LabelStatus.Text = "Status: Editing... " + e.nPercentage.ToString() + "%";
      this.progressBar1.Refresh();
      this.LabelStatus.Refresh();
    }

    private void audioSoundEditor1_SoundEditDone(object sender, SoundEditDoneEventArgs e)
    {
      this.LabelStatus.Text = "Status: Idle";
      this.progressBar1.Visible = false;
      this.LabelStatus.Refresh();
      if (e.bResult)
      {
        this.TimerReload.Enabled = true;
      }
      else
      {
        int num = (int) MessageBox.Show("Editing failed due to error " + this.audioSoundEditor1.LastError.ToString());
      }
    }

    private void audioSoundEditor1_SoundPlaybackDone(object sender, EventArgs e)
    {
      this.timerPlaybackPos.Enabled = false;
      this.buttonPause.Text = "Pause";
      this.LabelStatus.Text = "Status: Idle";
      this.progressBar1.Visible = false;
      this.LabelStatus.Refresh();
    }

    private void audioSoundEditor1_SoundPlaybackPlaying(object sender, EventArgs e)
    {
      bool bSelectionAvailable = false;
      int nBeginPosInMs = 0;
      int nEndPosInMs = 0;
      int selection = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GetSelection(ref bSelectionAvailable, ref nBeginPosInMs, ref nEndPosInMs);
      int playbackPosition = this.audioSoundEditor1.GetPlaybackPosition();
      this.progressBar1.Value = 0;
      this.progressBar1.Visible = true;
      this.buttonPause.Text = "Pause";
      this.LabelStatus.Text = "Status: Playing..." + this.audioSoundEditor1.GetFormattedTime(playbackPosition, false, true);
      this.LabelStatus.Refresh();
      this.timerPlaybackPos.Enabled = true;
    }

    private void audioSoundEditor1_SoundPlaybackPaused(object sender, EventArgs e)
    {
      this.buttonPause.Text = "Resume";
      this.LabelStatus.Text = "Status: Playback paused";
      this.LabelStatus.Refresh();
    }

    private void audioSoundEditor1_SoundPlaybackStopped(object sender, EventArgs e)
    {
      this.timerPlaybackPos.Enabled = false;
      this.buttonPause.Text = "Pause";
      this.LabelStatus.Text = "Status: Idle";
      this.progressBar1.Visible = false;
      this.LabelStatus.Refresh();
    }

    private void audioSoundEditor1_WaveAnalysisStart(object sender, EventArgs e)
    {
      this.LabelStatus.Text = "Status: Analyzing waveform...";
      this.progressBar1.Value = 0;
      this.progressBar1.Visible = true;
      this.progressBar1.Refresh();
      this.LabelStatus.Refresh();
    }

    private void audioSoundEditor1_WaveAnalysisPerc(object sender, WaveAnalysisPercEventArgs e)
    {
      if (this.progressBar1.Value == (int) e.nPercentage)
        return;
      this.progressBar1.Value = (int) e.nPercentage;
      this.LabelStatus.Text = "Status: Analyzing waveform... " + e.nPercentage.ToString() + "%";
      this.progressBar1.Refresh();
      this.LabelStatus.Refresh();
    }

    private void audioSoundEditor1_WaveAnalysisDone(object sender, WaveAnalysisDoneEventArgs e)
    {
      this.timerDisplayWaveform.Enabled = true;
      this.progressBar1.Visible = false;
      this.LabelStatus.Text = "Status: Idle";
      this.buttonPlay.Enabled = true;
      this.buttonPause.Enabled = true;
      this.buttonStop.Enabled = true;
    }

    private void audioSoundEditor1_WaveAnalyzerDisplayRangeChange(object sender, WaveAnalyzerDisplayRangeChangeEventArgs e)
    {
      this.LabelRangeBegin.Text = this.audioSoundEditor1.GetFormattedTime(e.nBeginPosInMs, true, true);
      this.LabelRangeEnd.Text = this.audioSoundEditor1.GetFormattedTime(e.nEndPosInMs, true, true);
      this.LabelRangeDuration.Text = this.audioSoundEditor1.GetFormattedTime(e.nEndPosInMs - e.nBeginPosInMs, true, true);
    }

    private void audioSoundEditor1_WaveAnalyzerSelectionChange(object sender, WaveAnalyzerSelectionChangeEventArgs e)
    {
      if (e.bSelectionAvailable)
      {
        if (e.nEndPosInMs - e.nBeginPosInMs > 0)
          this.buttonPlaySelection.Enabled = true;
        else
          this.buttonPlaySelection.Enabled = false;
        this.LabelSelectionBegin.Text = this.audioSoundEditor1.GetFormattedTime(e.nBeginPosInMs, true, true);
        this.LabelSelectionEnd.Text = this.audioSoundEditor1.GetFormattedTime(e.nEndPosInMs, true, true);
        this.LabelSelectionDuration.Text = this.audioSoundEditor1.GetFormattedTime(e.nEndPosInMs - e.nBeginPosInMs, true, true);
      }
      else
      {
        this.buttonPlaySelection.Enabled = false;
        this.LabelSelectionBegin.Text = "00:00:00.000";
        this.LabelSelectionEnd.Text = "00:00:00.000";
        this.LabelSelectionDuration.Text = "00:00:00.000";
      }
    }

    private void menuEffectsFlatVolume_Click(object sender, EventArgs e)
    {
      FormVolume formVolume = new FormVolume();
      formVolume.audioSoundEditor1 = this.audioSoundEditor1;
      formVolume.m_nVolumeMode = (short) 0;
      int num1 = (int) formVolume.ShowDialog((IWin32Window) this);
      if (formVolume.m_bCancel)
        return;
      int nBeginSelectionInMs = 0;
      int nEndSelectionInMs = 0;
      this.GetSelectedRange(ref nBeginSelectionInMs, ref nEndSelectionInMs);
      int num2 = (int) this.audioSoundEditor1.Effects.VolumeFlatApply(nBeginSelectionInMs, nEndSelectionInMs, formVolume.m_nAffectedChannels, (float) formVolume.m_nInitialVolume, enumVolumeScales.SCALE_LINEAR);
    }

    private void menuEffectsSlidingVolume_Click(object sender, EventArgs e)
    {
      FormVolume formVolume = new FormVolume();
      formVolume.audioSoundEditor1 = this.audioSoundEditor1;
      formVolume.m_nVolumeMode = (short) 1;
      int num1 = (int) formVolume.ShowDialog((IWin32Window) this);
      if (formVolume.m_bCancel)
        return;
      int nBeginSelectionInMs = 0;
      int nEndSelectionInMs = 0;
      this.GetSelectedRange(ref nBeginSelectionInMs, ref nEndSelectionInMs);
      int num2 = (int) this.audioSoundEditor1.Effects.VolumeSlidingApply(nBeginSelectionInMs, nEndSelectionInMs, formVolume.m_nAffectedChannels, (float) formVolume.m_nInitialVolume, (float) formVolume.m_nFinalVolume, enumVolumeScales.SCALE_LINEAR);
    }

    private void menuEffectsVolumeAutomation_Click(object sender, EventArgs e)
    {
      FormVolumeAutomation volumeAutomation = new FormVolumeAutomation();
      volumeAutomation.audioSoundEditor1 = this.audioSoundEditor1;
      int num1 = (int) volumeAutomation.ShowDialog((IWin32Window) this);
      if (volumeAutomation.m_bCancel)
        return;
      int nBeginSelectionInMs = 0;
      int nEndSelectionInMs = 0;
      this.GetSelectedRange(ref nBeginSelectionInMs, ref nEndSelectionInMs);
      int num2 = (int) this.audioSoundEditor1.Effects.VolumeAutomationApply(nBeginSelectionInMs, nEndSelectionInMs, volumeAutomation.m_nAffectedChannels);
    }

    private void audioSoundEditor1_SoundLoadingStarted(object sender, EventArgs e)
    {
      this.LabelStatus.Text = "Status: Loading... 0%";
      this.progressBar1.Value = 0;
      this.progressBar1.Visible = true;
      this.progressBar1.Refresh();
      this.LabelStatus.Refresh();
    }

    private void audioSoundEditor1_SoundLoadingPerc(object sender, SoundLoadingPercEventArgs e)
    {
      if (this.progressBar1.Value == (int) e.nPercentage)
        return;
      this.progressBar1.Value = (int) e.nPercentage;
      this.LabelStatus.Text = "Status: Loading... " + e.nPercentage.ToString() + "%";
      this.progressBar1.Refresh();
      this.LabelStatus.Refresh();
    }

    private void audioSoundEditor1_SoundLoadingDone(object sender, SoundLoadingDoneEventArgs e)
    {
      this.LabelStatus.Text = "Status: Idle";
      this.LabelStatus.Refresh();
      this.progressBar1.Visible = false;
      if (!e.bResult)
      {
        int num1 = (int) MessageBox.Show("Sound failed to load with the following error code: " + this.audioSoundEditor1.LastError.ToString());
      }
      else
      {
        enumLoadingModes nMode = enumLoadingModes.LOAD_MODE_NEW;
        int loadingMode = (int) this.audioSoundEditor1.GetLoadingMode(ref nMode);
        if (nMode != enumLoadingModes.LOAD_MODE_NEW)
        {
          this.TimerReload.Enabled = true;
        }
        else
        {
          int soundDuration = this.audioSoundEditor1.GetSoundDuration();
          if (soundDuration == 0)
          {
            int num2 = (int) this.audioSoundEditor1.CloseSound();
            this.LabelRangeBegin.Text = "00:00:00.000";
            this.LabelRangeEnd.Text = "00:00:00.000";
            this.LabelRangeDuration.Text = "00:00:00.000";
            this.LabelSelectionBegin.Text = "00:00:00.000";
            this.LabelSelectionEnd.Text = "00:00:00.000";
            this.LabelSelectionDuration.Text = "00:00:00.000";
            this.LabelTotalDuration.Text = "00:00:00.000";
          }
          else
          {
            this.LabelTotalDuration.Text = this.audioSoundEditor1.GetFormattedTime(soundDuration, true, true);
            this.LabelTotalDuration.Refresh();
          }
        }
      }
    }

    private void menuEffectsApplyWaveReverb_Click(object sender, EventArgs e)
    {
      if (this.audioSoundEditor1.GetChannels() > 2)
      {
        int num1 = (int) MessageBox.Show("The current song has more than 2 channels so DMO effects cannot be applied");
      }
      else
      {
        FormWavesReverb formWavesReverb = new FormWavesReverb();
        int num2 = (int) formWavesReverb.ShowDialog((IWin32Window) this);
        if (formWavesReverb.m_bCancel)
          return;
        EffectsWavesReverb fx = new EffectsWavesReverb();
        fx.InGain = formWavesReverb.m_fInGain;
        fx.ReverbMix = formWavesReverb.m_fReverbMix;
        fx.ReverbTime = formWavesReverb.m_fReverbTime;
        fx.HighFrequencyRtRatio = formWavesReverb.m_fHighFreqRTRatio;
        int nBeginSelectionInMs = 0;
        int nEndSelectionInMs = 0;
        this.GetSelectedRange(ref nBeginSelectionInMs, ref nEndSelectionInMs);
        int num3 = (int) this.audioSoundEditor1.Effects.DirectXApply(nBeginSelectionInMs, nEndSelectionInMs, fx);
      }
    }

    private void menuEffectsApplyEqualizer_Click(object sender, EventArgs e)
    {
      FormEqualizer formEqualizer = new FormEqualizer();
      formEqualizer.audioSoundEditor1 = this.audioSoundEditor1;
      int num1 = (int) formEqualizer.ShowDialog((IWin32Window) this);
      if (formEqualizer.m_bCancel)
        return;
      int nBeginSelectionInMs = 0;
      int nEndSelectionInMs = 0;
      this.GetSelectedRange(ref nBeginSelectionInMs, ref nEndSelectionInMs);
      int num2 = (int) this.audioSoundEditor1.Effects.EqualizerApply(nBeginSelectionInMs, nEndSelectionInMs);
    }

    private void menuEffectsApplyReverbInternal_Click(object sender, EventArgs e)
    {
      if (this.m_idDspReverbInternal == 0)
      {
        this.m_idDspReverbInternal = this.audioSoundEditor1.Effects.CustomDspInternalLoad();
        this.addrReverbCallback = new DSPCallbackFunction(this.ReverbCallback);
        this.m_buffReverbLeft = new float[this.BUFFERLEN];
        this.m_buffReverbRight = new float[this.BUFFERLEN];
        int num = (int) this.audioSoundEditor1.Effects.CustomDspInternalSetFunction(this.m_idDspReverbInternal, this.addrReverbCallback);
      }
      int nBeginSelectionInMs = 0;
      int nEndSelectionInMs = 0;
      this.GetSelectedRange(ref nBeginSelectionInMs, ref nEndSelectionInMs);
      int num1 = (int) this.audioSoundEditor1.Effects.CustomDspUseFloatSamples(true);
      int num2 = (int) this.audioSoundEditor1.Effects.CustomDspApply(nBeginSelectionInMs, nEndSelectionInMs, this.m_idDspReverbInternal, 0);
    }

    private void menuEffectsApplyReverbExternal_Click(object sender, EventArgs e)
    {
      if (this.m_idDspReverbExternal == 0)
      {
        string strPathname = IntPtr.Size != 8 ? "MyCustomDSP.dll" : "MyCustomDSP64.dll";
        this.m_idDspReverbExternal = this.audioSoundEditor1.Effects.CustomDspExternalLoad(strPathname);
        if (this.m_idDspReverbExternal == 0)
        {
          int num = (int) MessageBox.Show("Cannot load " + strPathname);
          return;
        }
        int num1 = (int) this.audioSoundEditor1.Effects.CustomDspExternalSetFunction(this.m_idDspReverbExternal, enumDspExternalFunctions.DSP_FUNC_CALLBACK, "fnReverbCallback");
      }
      int nBeginSelectionInMs = 0;
      int nEndSelectionInMs = 0;
      this.GetSelectedRange(ref nBeginSelectionInMs, ref nEndSelectionInMs);
      int num2 = (int) this.audioSoundEditor1.Effects.CustomDspUseFloatSamples(true);
      int num3 = (int) this.audioSoundEditor1.Effects.CustomDspApply(nBeginSelectionInMs, nEndSelectionInMs, this.m_idDspReverbExternal, 0);
    }

    private void menuEffectsApplyBalanceInternal_Click(object sender, EventArgs e)
    {
      if (this.m_idDspBalanceInternal == 0)
      {
        this.m_idDspBalanceInternal = this.audioSoundEditor1.Effects.CustomDspInternalLoad();
        this.addrBalanceCallback = new DSPCallbackFunction(this.BalanceCallback);
        int num = (int) this.audioSoundEditor1.Effects.CustomDspInternalSetFunction(this.m_idDspBalanceInternal, this.addrBalanceCallback);
      }
      FormBalance formBalance = new FormBalance();
      formBalance.audioSoundEditor1 = this.audioSoundEditor1;
      formBalance.m_bUseInternal = true;
      int num1 = (int) formBalance.ShowDialog((IWin32Window) this);
      if (formBalance.m_bCancel)
        return;
      this.m_nBalancePercentageInternal = formBalance.m_nBalancePercentage;
      int nBeginSelectionInMs = 0;
      int nEndSelectionInMs = 0;
      this.GetSelectedRange(ref nBeginSelectionInMs, ref nEndSelectionInMs);
      int num2 = (int) this.audioSoundEditor1.Effects.CustomDspUseFloatSamples(true);
      int num3 = (int) this.audioSoundEditor1.Effects.CustomDspApply(nBeginSelectionInMs, nEndSelectionInMs, this.m_idDspBalanceInternal, 0);
    }

    private void menuEffectsApplyBalanceExternal_Click(object sender, EventArgs e)
    {
      if (this.m_idDspBalanceExternal == 0)
      {
        string strPathname = IntPtr.Size != 8 ? "MyCustomDSP.dll" : "MyCustomDSP64.dll";
        this.m_idDspBalanceExternal = this.audioSoundEditor1.Effects.CustomDspExternalLoad(strPathname);
        if (this.m_idDspBalanceExternal == 0)
        {
          int num = (int) MessageBox.Show("Cannot load " + strPathname);
          return;
        }
        int num1 = (int) this.audioSoundEditor1.Effects.CustomDspExternalSetFunction(this.m_idDspBalanceExternal, enumDspExternalFunctions.DSP_FUNC_CALLBACK, "fnBalanceCallback");
        int num2 = (int) this.audioSoundEditor1.Effects.CustomDspExternalSetFunction(this.m_idDspBalanceExternal, enumDspExternalFunctions.DSP_FUNC_PARAMS_GET, "fnBalanceGetParameters");
        int num3 = (int) this.audioSoundEditor1.Effects.CustomDspExternalSetFunction(this.m_idDspBalanceExternal, enumDspExternalFunctions.DSP_FUNC_PARAMS_SET, "fnBalanceSetParameters");
        int num4 = (int) this.audioSoundEditor1.Effects.CustomDspExternalSetFunction(this.m_idDspBalanceExternal, enumDspExternalFunctions.DSP_FUNC_COMMAND_SEND, "fnBalanceSendCommand");
      }
      FormBalance formBalance = new FormBalance();
      formBalance.audioSoundEditor1 = this.audioSoundEditor1;
      formBalance.m_bUseInternal = false;
      formBalance.m_idDspBalanceExternal = this.m_idDspBalanceExternal;
      int num5 = (int) formBalance.ShowDialog((IWin32Window) this);
      if (formBalance.m_bCancel)
        return;
      int nBeginSelectionInMs = 0;
      int nEndSelectionInMs = 0;
      this.GetSelectedRange(ref nBeginSelectionInMs, ref nEndSelectionInMs);
      int num6 = (int) this.audioSoundEditor1.Effects.CustomDspUseFloatSamples(true);
      int num7 = (int) this.audioSoundEditor1.Effects.CustomDspApply(nBeginSelectionInMs, nEndSelectionInMs, this.m_idDspBalanceExternal, 0);
    }

    private void menuEffectsApplyBassBoostExternal_Click(object sender, EventArgs e)
    {
      if (this.m_idDspBassBoostExternal == 0)
      {
        this.m_idDspBassBoostExternal = this.audioSoundEditor1.Effects.CustomDspExternalLoad("MyCustomDspWithUI.dll");
        if (this.m_idDspBassBoostExternal == 0)
        {
          int num = (int) MessageBox.Show("Cannot load MyCustomDspWithUI.dll");
          return;
        }
        int num1 = (int) this.audioSoundEditor1.Effects.CustomDspExternalSetFunction(this.m_idDspBassBoostExternal, enumDspExternalFunctions.DSP_FUNC_CALLBACK, "fnBassBoostCallback");
        int num2 = (int) this.audioSoundEditor1.Effects.CustomDspExternalSetFunction(this.m_idDspBassBoostExternal, enumDspExternalFunctions.DSP_FUNC_PARAMS_GET, "fnBassBoostGetParameters");
        int num3 = (int) this.audioSoundEditor1.Effects.CustomDspExternalSetFunction(this.m_idDspBassBoostExternal, enumDspExternalFunctions.DSP_FUNC_PARAMS_SET, "fnBassBoostSetParameters");
        int num4 = (int) this.audioSoundEditor1.Effects.CustomDspExternalSetFunction(this.m_idDspBassBoostExternal, enumDspExternalFunctions.DSP_FUNC_COMMAND_SEND, "fnBassBoostSendCommand");
        int num5 = (int) this.audioSoundEditor1.Effects.CustomDspExternalSetFunction(this.m_idDspBassBoostExternal, enumDspExternalFunctions.DSP_FUNC_EDITOR_DISPLAY, "fnBassBoostEditorDisplay");
        int num6 = (int) this.audioSoundEditor1.Effects.CustomDspExternalSetFunction(this.m_idDspBassBoostExternal, enumDspExternalFunctions.DSP_FUNC_EDITOR_GET_INFO, "fnBassBoostEditorGetInfo");
        this.m_paramBassBoostExternal.nFrequencyHz = (short) 200;
        this.m_paramBassBoostExternal.nBoostdB = (short) 12;
      }
      this.m_paramBassBoostExternal.nSampleRate = this.audioSoundEditor1.GetFrequency();
      IntPtr num7 = Marshal.AllocHGlobal(Marshal.SizeOf((object) this.m_paramBassBoostExternal));
      Marshal.StructureToPtr((object) this.m_paramBassBoostExternal, num7, true);
      int num8 = (int) this.audioSoundEditor1.Effects.CustomDspExternalSetParameters(this.m_idDspBassBoostExternal, num7);
      Marshal.FreeHGlobal(num7);
      FormBassBoost formBassBoost = new FormBassBoost();
      formBassBoost.audioSoundEditor1 = this.audioSoundEditor1;
      formBassBoost.m_idDspBassBoostExternal = this.m_idDspBassBoostExternal;
      int num9 = (int) formBassBoost.ShowDialog((IWin32Window) this);
      if (formBassBoost.m_bCancel)
        return;
      int nBeginSelectionInMs = 0;
      int nEndSelectionInMs = 0;
      this.GetSelectedRange(ref nBeginSelectionInMs, ref nEndSelectionInMs);
      int num10 = (int) this.audioSoundEditor1.Effects.CustomDspUseFloatSamples(true);
      int num11 = (int) this.audioSoundEditor1.Effects.CustomDspApply(nBeginSelectionInMs, nEndSelectionInMs, this.m_idDspBassBoostExternal, 0);
    }

    private void menuEffectsApplyClassicEQ_Click(object sender, EventArgs e)
    {
      if (this.audioSoundEditor1.GetChannels() != 2)
      {
        int num1 = (int) MessageBox.Show("The current song is not Stereo so VST effects cannot be applied");
      }
      else
      {
        if (this.m_idVstKarmaFxEq == 0)
        {
          this.m_idVstKarmaFxEq = this.audioSoundEditor1.Effects.VstLoad("TAL-Reverb-3.dll");
          if (this.m_idVstKarmaFxEq == 0)
          {
            int num2 = (int) MessageBox.Show("Cannot load VST effect");
            return;
          }
        }
        FormHostVstEditor formHostVstEditor = new FormHostVstEditor();
        formHostVstEditor.audioSoundEditor1 = this.audioSoundEditor1;
        formHostVstEditor.m_idVst = this.m_idVstKarmaFxEq;
        int num3 = (int) formHostVstEditor.ShowDialog((IWin32Window) this);
        if (formHostVstEditor.m_bCancel)
          return;
        int nBeginSelectionInMs = 0;
        int nEndSelectionInMs = 0;
        this.GetSelectedRange(ref nBeginSelectionInMs, ref nEndSelectionInMs);
        int num4 = (int) this.audioSoundEditor1.Effects.VstApply(nBeginSelectionInMs, nEndSelectionInMs, this.m_idVstKarmaFxEq);
      }
    }

    private void menuEffectsApplyVstFromDisk_Click(object sender, EventArgs e)
    {
      this.openFileDialog1.Filter = "VST effect files (*.dll)|*.dll|All files (*.*)|*.*";
      this.openFileDialog1.Title = "Load a VST effect from disk";
      this.openFileDialog1.FileName = "";
      if (this.openFileDialog1.ShowDialog() == DialogResult.Cancel)
        return;
      if (this.m_idVstFromFile != 0)
      {
        int num1 = (int) this.audioSoundEditor1.Effects.VstFree(this.m_idVstFromFile);
      }
      this.m_idVstFromFile = this.audioSoundEditor1.Effects.VstLoad(this.openFileDialog1.FileName);
      if (this.m_idVstFromFile == 0)
      {
        if (this.audioSoundEditor1.LastError == enumErrorCodes.ERR_INVALID_PLATFORM)
        {
          int num2 = (int) MessageBox.Show("Cannot load VST effects under X64 versions of Windows: in order to enable support for VST effects under X64, you need to recompile this sample specifically for x86");
        }
        else
        {
          int num3 = (int) MessageBox.Show("Cannot load VST effect '" + this.openFileDialog1.FileName + "'");
        }
      }
      else
      {
        VstEditorInfo Info = new VstEditorInfo();
        int info = (int) this.audioSoundEditor1.Effects.VstEditorGetInfo(this.m_idVstFromFile, ref Info);
        if (Info.bIsEditorAvailable)
        {
          FormHostVstEditor formHostVstEditor = new FormHostVstEditor();
          formHostVstEditor.audioSoundEditor1 = this.audioSoundEditor1;
          formHostVstEditor.m_idVst = this.m_idVstFromFile;
          int num4 = (int) formHostVstEditor.ShowDialog((IWin32Window) this);
          if (formHostVstEditor.m_bCancel)
            return;
        }
        int nBeginSelectionInMs = 0;
        int nEndSelectionInMs = 0;
        this.GetSelectedRange(ref nBeginSelectionInMs, ref nEndSelectionInMs);
        int num5 = (int) this.audioSoundEditor1.Effects.VstApply(nBeginSelectionInMs, nEndSelectionInMs, this.m_idVstFromFile);
      }
    }

    private void menuEffectsApplyTempoChange_Click(object sender, EventArgs e)
    {
      FormTempoRate formTempoRate = new FormTempoRate();
      formTempoRate.m_bIsChangingTempo = true;
      int num1 = (int) formTempoRate.ShowDialog((IWin32Window) this);
      if (formTempoRate.m_bCancel)
        return;
      int nBeginSelectionInMs = 0;
      int nEndSelectionInMs = 0;
      this.GetSelectedRange(ref nBeginSelectionInMs, ref nEndSelectionInMs);
      int num2 = (int) this.audioSoundEditor1.Effects.TempoApply(nBeginSelectionInMs, nEndSelectionInMs, formTempoRate.m_fChangePercentage);
    }

    private void menuEffectsApplyRateChange_Click(object sender, EventArgs e)
    {
      FormTempoRate formTempoRate = new FormTempoRate();
      formTempoRate.m_bIsChangingTempo = false;
      int num1 = (int) formTempoRate.ShowDialog((IWin32Window) this);
      if (formTempoRate.m_bCancel)
        return;
      int nBeginSelectionInMs = 0;
      int nEndSelectionInMs = 0;
      this.GetSelectedRange(ref nBeginSelectionInMs, ref nEndSelectionInMs);
      int num2 = (int) this.audioSoundEditor1.Effects.PlaybackRateApply(nBeginSelectionInMs, nEndSelectionInMs, formTempoRate.m_fChangePercentage);
    }

    private void menuEffectsApplyPitchChange_Click(object sender, EventArgs e)
    {
      FormPitch formPitch = new FormPitch();
      int num1 = (int) formPitch.ShowDialog((IWin32Window) this);
      if (formPitch.m_bCancel)
        return;
      int nBeginSelectionInMs = 0;
      int nEndSelectionInMs = 0;
      this.GetSelectedRange(ref nBeginSelectionInMs, ref nEndSelectionInMs);
      int num2 = (int) this.audioSoundEditor1.Effects.PitchApply(nBeginSelectionInMs, nEndSelectionInMs, formPitch.m_fChangeValue);
    }

    private void menuEffectsReverseSound_Click(object sender, EventArgs e)
    {
      int nBeginSelectionInMs = 0;
      int nEndSelectionInMs = 0;
      this.GetSelectedRange(ref nBeginSelectionInMs, ref nEndSelectionInMs);
      int num = (int) this.audioSoundEditor1.Effects.ReverseApply(nBeginSelectionInMs, nEndSelectionInMs);
    }

    private void mnuToolsRemoveSilence_Click(object sender, EventArgs e)
    {
      FormRemoveSilence formRemoveSilence = new FormRemoveSilence();
      int num1 = (int) formRemoveSilence.ShowDialog((IWin32Window) this);
      if (formRemoveSilence.m_bCancel)
        return;
      int nBeginSelectionInMs = 0;
      int nEndSelectionInMs = 0;
      this.GetSelectedRange(ref nBeginSelectionInMs, ref nEndSelectionInMs);
      int num2 = (int) this.audioSoundEditor1.RemoveSilence(nBeginSelectionInMs, nEndSelectionInMs, formRemoveSilence.m_nSilenceThreshold, formRemoveSilence.m_nSilenceMinLengthInMs);
    }

    private void mnuEditUndo_Click(object sender, EventArgs e)
    {
      int num = (int) this.audioSoundEditor1.UndoApply();
    }

    private void ApplyFilter(enumFilterTypes nFilter)
    {
      FormFilters formFilters = new FormFilters();
      formFilters.m_nFilterType = nFilter;
      int num1 = (int) formFilters.ShowDialog((IWin32Window) this);
      if (formFilters.m_bCancel)
        return;
      int nBeginSelectionInMs = 0;
      int nEndSelectionInMs = 0;
      this.GetSelectedRange(ref nBeginSelectionInMs, ref nEndSelectionInMs);
      int num2 = (int) this.audioSoundEditor1.Effects.FilterApply(nBeginSelectionInMs, nEndSelectionInMs, formFilters.m_nFilterName, formFilters.m_nFilterType, formFilters.m_fFrequency1, formFilters.m_fFrequency2, formFilters.m_fGain, formFilters.m_fCutoffWidth);
    }

    private void menuEffectsApplyLowPass_Click(object sender, EventArgs e)
    {
      this.ApplyFilter(enumFilterTypes.FILTER_TYPE_LOW_PASS);
    }

    private void menuEffectsApplyHighPass_Click(object sender, EventArgs e)
    {
      this.ApplyFilter(enumFilterTypes.FILTER_TYPE_HIGH_PASS);
    }

    private void menuEffectsApplyBandPass_Click(object sender, EventArgs e)
    {
      this.ApplyFilter(enumFilterTypes.FILTER_TYPE_BAND_PASS);
    }

    private void menuEffectsApplyBandStop_Click(object sender, EventArgs e)
    {
      this.ApplyFilter(enumFilterTypes.FILTER_TYPE_BAND_STOP);
    }

    private void timerPlaybackPos_Tick(object sender, EventArgs e)
    {
      int playbackPosition = this.audioSoundEditor1.GetPlaybackPosition();
      bool bSelectionAvailable = false;
      int nBeginPosInMs = 0;
      int nEndPosInMs = 0;
      int selection = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GetSelection(ref bSelectionAvailable, ref nBeginPosInMs, ref nEndPosInMs);
      int num1 = bSelectionAvailable ? nEndPosInMs : this.audioSoundEditor1.GetSoundDuration();
      if (num1 == 0)
        return;
      int num2 = 100 * playbackPosition / num1;
      if (num2 > 100)
        num2 = 100;
      this.progressBar1.Value = num2;
      if (this.buttonPause.Text == "Resume")
        this.LabelStatus.Text = "Status: Paused at " + this.audioSoundEditor1.GetFormattedTime(playbackPosition, false, true);
      else
        this.LabelStatus.Text = "Status: Playing..." + this.audioSoundEditor1.GetFormattedTime(playbackPosition, false, true);
    }

    private void trackBar1_Scroll(object sender, EventArgs e)
    {
      WANALYZER_GENERAL_SETTINGS settings = new WANALYZER_GENERAL_SETTINGS();
      int num1 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsGeneralGet(ref settings);
      float num2 = (float) this.trackBar1.Value;
      settings.fVerticalZoomFactor = num2 / 100f;
      int num3 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsGeneralSet(settings);
    }

    private void menuEffectsApplyNormalizePeak_Click(object sender, EventArgs e)
    {
      int nBeginSelectionInMs = 0;
      int nEndSelectionInMs = 0;
      this.GetSelectedRange(ref nBeginSelectionInMs, ref nEndSelectionInMs);
      int num = (int) this.audioSoundEditor1.Effects.NormalizationSimpleApply(nBeginSelectionInMs, nEndSelectionInMs);
    }

    private void menuEffectsApplyNormalizeTarget_Click(object sender, EventArgs e)
    {
      FormNormalize formNormalize = new FormNormalize();
      int num1 = (int) formNormalize.ShowDialog((IWin32Window) this);
      if (formNormalize.m_bCancel)
        return;
      int nBeginSelectionInMs = 0;
      int nEndSelectionInMs = 0;
      this.GetSelectedRange(ref nBeginSelectionInMs, ref nEndSelectionInMs);
      int num2 = (int) this.audioSoundEditor1.Effects.NormalizationAdvancedApply(nBeginSelectionInMs, nEndSelectionInMs, formNormalize.m_fLevelTarget, formNormalize.m_fLevelBelow, formNormalize.m_fLevelAbove, formNormalize.m_bRemoveDCOffset);
    }

    private void menuItemRemoveClicksPops_Click(object sender, EventArgs e)
    {
      FormDeClick formDeClick = new FormDeClick();
      int num1 = (int) formDeClick.ShowDialog((IWin32Window) this);
      if (formDeClick.m_bCancel)
        return;
      int nBeginSelectionInMs = 0;
      int nEndSelectionInMs = 0;
      this.GetSelectedRange(ref nBeginSelectionInMs, ref nEndSelectionInMs);
      int num2 = (int) this.audioSoundEditor1.Effects.DeClickFilterApply(nBeginSelectionInMs, nEndSelectionInMs, formDeClick.m_nSensitivity, formDeClick.m_nGroupSensitivity, formDeClick.m_bRecoverPitch);
    }

    private void menuItemRemoveHiss_Click(object sender, EventArgs e)
    {
      FormDeHiss formDeHiss = new FormDeHiss();
      int num1 = (int) formDeHiss.ShowDialog((IWin32Window) this);
      if (formDeHiss.m_bCancel)
        return;
      int num2 = (int) MessageBox.Show("In this sample the noise profile is taken from the first 800 milliseconds of the file under editing");
      int num3 = (int) this.audioSoundEditor1.Effects.DeNoiseFilterProfileSet(0, 800);
      int num4 = (int) this.audioSoundEditor1.Effects.DeNoiseFilterApply(0, -1, (enumWindowTypes) formDeHiss.m_nWindowType, formDeHiss.m_nWindowSize, (float) formDeHiss.m_nNoiseGain, (float) formDeHiss.m_nFrequencySmoothing, formDeHiss.m_fAttackDecayTimeInSec);
    }

    private void menuEffectsDCOffsetRemoval_Click(object sender, EventArgs e)
    {
      int num = (int) this.audioSoundEditor1.Effects.DcOffsetRemovalApply();
    }

    private void menuEffectsVocalRemoval_Click(object sender, EventArgs e)
    {
      FormVocalRemoval formVocalRemoval = new FormVocalRemoval();
      int num1 = (int) formVocalRemoval.ShowDialog((IWin32Window) this);
      if (formVocalRemoval.m_bCancel)
        return;
      int nBeginSelectionInMs = 0;
      int nEndSelectionInMs = 0;
      this.GetSelectedRange(ref nBeginSelectionInMs, ref nEndSelectionInMs);
      int num2 = (int) this.audioSoundEditor1.Effects.VocalRemovalApply(nBeginSelectionInMs, nEndSelectionInMs, formVocalRemoval.m_nAttenuation, formVocalRemoval.m_nGain, formVocalRemoval.m_nCutoffFreq);
    }

    private void audioSoundEditor1_WaveScrollerManualScroll(object sender, WaveScrollerMabualScrollEventArgs e)
    {
      Console.WriteLine("WaveScrollerManualScroll " + e.nNewPosInMs.ToString());
    }

    private void audioSoundEditor1_WaveScrollerMouseNotification(object sender, WaveScrollerMouseNotificationEventArgs e)
    {
      if (e.nAction != enumMouseActions.MOUSE_ACTION_RIGHT_CLICK)
        return;
      Console.WriteLine("Pressed right button at position " + e.nPressPosInMs.ToString());
    }

    private void mnuToolsTrimSilence_Click(object sender, EventArgs e)
    {
      FormRemoveSilence formRemoveSilence = new FormRemoveSilence();
      formRemoveSilence.m_bTrimOnly = true;
      int num1 = (int) formRemoveSilence.ShowDialog((IWin32Window) this);
      if (formRemoveSilence.m_bCancel)
        return;
      int num2 = (int) this.audioSoundEditor1.TrimSilence(formRemoveSilence.m_nSilenceThreshold);
    }

    private void mnuFileGenerateTone_Click(object sender, EventArgs e)
    {
      if (this.audioSoundEditor1.GetSoundDuration() > 0 && MessageBox.Show("A sound is currently in memory: do you want to create a new one?", "Sound Editor", MessageBoxButtons.YesNo) == DialogResult.No)
        return;
      FormWaveTone formWaveTone = new FormWaveTone();
      formWaveTone.audioSoundEditor1 = this.audioSoundEditor1;
      int num = (int) formWaveTone.ShowDialog((IWin32Window) this);
      if (formWaveTone.m_bCancel)
        return;
      this.TimerReload.Enabled = true;
    }

    private void mnuFileGenerateSlidingTone_Click(object sender, EventArgs e)
    {
      if (this.audioSoundEditor1.GetSoundDuration() > 0 && MessageBox.Show("A sound is currently in memory: do you want to create a new one?", "Sound Editor", MessageBoxButtons.YesNo) == DialogResult.No)
        return;
      FormSlidingWaveTone formSlidingWaveTone = new FormSlidingWaveTone();
      formSlidingWaveTone.audioSoundEditor1 = this.audioSoundEditor1;
      int num = (int) formSlidingWaveTone.ShowDialog((IWin32Window) this);
      if (formSlidingWaveTone.m_bCancel)
        return;
      this.TimerReload.Enabled = true;
    }

    private void mnuFileGenerateBinauralTone_Click(object sender, EventArgs e)
    {
      if (this.audioSoundEditor1.GetSoundDuration() > 0 && MessageBox.Show("A sound is currently in memory: do you want to create a new one?", "Sound Editor", MessageBoxButtons.YesNo) == DialogResult.No)
        return;
      FormBinauralWaveTone binauralWaveTone = new FormBinauralWaveTone();
      binauralWaveTone.audioSoundEditor1 = this.audioSoundEditor1;
      int num = (int) binauralWaveTone.ShowDialog((IWin32Window) this);
      if (binauralWaveTone.m_bCancel)
        return;
      this.TimerReload.Enabled = true;
    }

    private void mnuFileGenerateCompositeTone_Click(object sender, EventArgs e)
    {
      if (this.audioSoundEditor1.GetSoundDuration() > 0 && MessageBox.Show("A sound is currently in memory: do you want to create a new one?", "Sound Editor", MessageBoxButtons.YesNo) == DialogResult.No)
        return;
      FormCompositeWaveTone compositeWaveTone = new FormCompositeWaveTone();
      compositeWaveTone.audioSoundEditor1 = this.audioSoundEditor1;
      int num = (int) compositeWaveTone.ShowDialog((IWin32Window) this);
      if (compositeWaveTone.m_bCancel)
        return;
      this.TimerReload.Enabled = true;
    }

    private void mnuFileGenerateNoise_Click(object sender, EventArgs e)
    {
      if (this.audioSoundEditor1.GetSoundDuration() > 0 && MessageBox.Show("A sound is currently in memory: do you want to create a new one?", "Sound Editor", MessageBoxButtons.YesNo) == DialogResult.No)
        return;
      FormNoise formNoise = new FormNoise();
      formNoise.audioSoundEditor1 = this.audioSoundEditor1;
      int num = (int) formNoise.ShowDialog((IWin32Window) this);
      if (formNoise.m_bCancel)
        return;
      this.TimerReload.Enabled = true;
    }

    private void mnuFileGenerateDtmf_Click(object sender, EventArgs e)
    {
      if (this.audioSoundEditor1.GetSoundDuration() > 0 && MessageBox.Show("A sound is currently in memory: do you want to create a new one?", "Sound Editor", MessageBoxButtons.YesNo) == DialogResult.No)
        return;
      FormDtmfTones formDtmfTones = new FormDtmfTones();
      formDtmfTones.audioSoundEditor1 = this.audioSoundEditor1;
      int num = (int) formDtmfTones.ShowDialog((IWin32Window) this);
      if (formDtmfTones.m_bCancel)
        return;
      this.TimerReload.Enabled = true;
    }

    private void mnuFileGenerateSpeech_Click(object sender, EventArgs e)
    {
      if (this.audioSoundEditor1.GetSoundDuration() > 0 && MessageBox.Show("A sound is currently in memory: do you want to create a new one?", "Sound Editor", MessageBoxButtons.YesNo) == DialogResult.No)
        return;
      FormSpeech formSpeech = new FormSpeech();
      formSpeech.audioSoundEditor1 = this.audioSoundEditor1;
      int num = (int) formSpeech.ShowDialog((IWin32Window) this);
      if (formSpeech.m_bCancel)
        return;
      this.TimerReload.Enabled = true;
    }

    private void audioSoundEditor1_VUMeterValueChange(object sender, VUMeterValueChangeEventArgs e)
    {
      int num1 = (int) this.audioSoundEditor1.GraphicBarsManager.SetValue(this.m_hWndVuMeterLeft, (int) e.nPeakLeft);
      int num2 = (int) this.audioSoundEditor1.GraphicBarsManager.SetValue(this.m_hWndVuMeterRight, (int) e.nPeakRight);
    }

    private void FormMain_Resize(object sender, EventArgs e)
    {
      try
      {
        int num1 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.Move(this.Picture1.Location.X, this.Picture1.Location.Y, this.Picture1.Size.Width, this.Picture1.Size.Height);
        int num2 = (int) this.audioSoundEditor1.DisplayWaveformScroller.Move(this.m_hWndWaveformScroller, this.pictureBoxWaveformScroll.Location.X, this.pictureBoxWaveformScroll.Location.Y, this.pictureBoxWaveformScroll.Size.Width, this.pictureBoxWaveformScroll.Size.Height);
        int num3 = (int) this.audioSoundEditor1.DisplaySpectrumEnh.Move(this.labelSpectrum.Location.X, this.labelSpectrum.Location.Y, this.labelSpectrum.Size.Width, this.labelSpectrum.Size.Height);
        int num4 = (int) this.audioSoundEditor1.GraphicBarsManager.Move(this.m_hWndVuMeterLeft, this.label18.Location.X, this.label18.Location.Y, this.label18.Size.Width, this.label18.Size.Height);
        int num5 = (int) this.audioSoundEditor1.GraphicBarsManager.Move(this.m_hWndVuMeterRight, this.label17.Location.X, this.label17.Location.Y, this.label17.Size.Width, this.label17.Size.Height);
      }
      catch
      {
      }
    }
  }
}
