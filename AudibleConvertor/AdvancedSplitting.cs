// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.AdvancedSplitting
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using AudioSoundEditor;
using CustomScrollBar;
using Inwards;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagLib;

namespace AudibleConvertor
{
  public class AdvancedSplitting : Form
  {
    public string sourceAacFile = "";
    public string origM4B = "";
    public AdvancedSplitting.Chapters myChapters = new AdvancedSplitting.Chapters();
    public List<double> splitPoints = new List<double>();
    public string tmpAACfile = "";
    private Stopwatch analyzeStopWatch = new Stopwatch();
    private SPECTR_ENH_GENERAL_SETTINGS descSpectrumEnhGeneral = new SPECTR_ENH_GENERAL_SETTINGS();
    private bool zoomOutAfterCompletion = true;
    private List<bool> enabledChapters = new List<bool>();
    public SupportLibraries supportLibs = new SupportLibraries();
    private int BUFFERLEN = 1200;
    private ContextMenu m_mnuGraphicItemsContextMenu = new ContextMenu();
    private Stopwatch stopWatch = new Stopwatch();
    private const string SILENCE_DETECTION = "Silence detection";
    private const int VOLUME_FLAT = 0;
    private const int VOLUME_SLIDING = 1;
    private bool chapterEdited;
    public bool overdriveMode;
    public Overdrive overdriveFile;
    public bool applied;
    public VirtualWAV myVirtualWav;
    public bool cdProcessingMode;
    public bool m4bMode;
    public string ffmpegPath;
    public string soxPath;
    public string mp4boxPath;
    public bool fileMode;
    private int tickerDir;
    private int selectedIndex;
    private bool disableEvents;
    private bool beLessAnnoying;
    private bool scrolling;
    private byte[] m_byteBuffer;
    private long currentScrollPosition;
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
    public short m_nLastUniqueId;
    private bool alignChapters;
    private IContainer components;
    private Label labelSpectrum;
    public PictureBox Picture1;
    private ProgressBar progressBar1;
    public GroupBox Frame5;
    private Button buttonPause;
    public Button buttonPlay;
    public Button buttonStop;
    public Button buttonPlaySelection;
    public Label LabelStatus;
    private System.Windows.Forms.Timer TimerMenuEnabler;
    private System.Windows.Forms.Timer timerDisplayWaveform;
    private System.Windows.Forms.Timer TimerReload;
    private System.Windows.Forms.Timer timerPlaybackPos;
    public Label Label2;
    public Label Label3;
    public Label Label4;
    public Label Label5;
    public Label Label6;
    public Label LabelSelectionEnd;
    public GroupBox Frame4;
    public Label LabelSelectionBegin;
    public Label LabelSelectionDuration;
    public Label LabelRangeBegin;
    public Label LabelRangeEnd;
    public Label LabelRangeDuration;
    public Label LabelTotalDuration;
    public Label Label8;
    private AudioSoundEditor.AudioSoundEditor audioSoundEditor1;
    public DataGridView dgvChapters;
    private Button btnApply;
    private Button btnCancel;
    private Button btnNextKeep;
    private Button btnNextDel;
    private GroupBox grpSilence;
    private Label lblSilenceThresh;
    private System.Windows.Forms.TextBox txtSilenceThresh;
    private Button btnFindSilence;
    private OpenFileDialog openFileDialog1;
    private SaveFileDialog saveFileDialog1;
    private Button btnFixedSplit;
    private System.Windows.Forms.TextBox txtFixedSplit;
    private Label label1;
    private BackgroundWorker backgroundWorker1;
    private BackgroundWorker backgroundWorker2;
    private BackgroundWorker backgroundWorker3;
    private Button btnSpectogram;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem loadFileToolStripMenuItem;
    private ToolStripMenuItem exitToolStripMenuItem;
    private ToolStripMenuItem zoomToolStripMenuItem;
    private ToolStripMenuItem zoomToselectionToolStripMenuItem;
    private ToolStripMenuItem zoomToFullwaveformToolStripMenuItem;
    private ToolStripMenuItem zoominToolStripMenuItem;
    private ToolStripMenuItem zoomoutToolStripMenuItem;
    private Label label18;
    private Label label17;
    private ToolStripMenuItem chaptersToolStripMenuItem;
    private ToolStripMenuItem nextChapterToolStripMenuItem;
    private ToolStripMenuItem deleteChapterToolStripMenuItem;
    private ToolStripMenuItem previousChapterToolStripMenuItem;
    private ToolStripMenuItem deletePreviousChapterToolStripMenuItem;
    private ToolStripMenuItem loadCUEFileToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripMenuItem saveCUEFileToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripMenuItem insertNewChapterToolStripMenuItem;
    private ToolStripMenuItem settingsToolStripMenuItem;
    private ToolStripMenuItem channelViewToolStripMenuItem;
    private ToolStripMenuItem combinedToolStripMenuItem;
    private ToolStripMenuItem discreteToolStripMenuItem;
    private Button btnZoomIn;
    private Button btnZoomOut;
    private Button btnZoomFull;
    private ToolStripMenuItem clearChapterListToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripMenuItem chapterNamesNumbersInFilenameToolStripMenuItem;
    private ToolStripMenuItem showChapterDebugToolStripMenuItem;
    private ToolStripMenuItem pruneDisabledChaptersToolStripMenuItem;
    private ToolStripMenuItem detectedSilenceAlignmentToolStripMenuItem;
    private ToolStripMenuItem middleToolStripMenuItem;
    private ToolStripMenuItem startToolStripMenuItem;
    private ToolStripMenuItem endToolStripMenuItem;
    private ToolStripMenuItem hardDeleteChapterToolStripMenuItem;
    public Button btnDeselect;
    private Button btnLeft;
    private Button btnRight;
    private ScrollBarEx scrollBarEx1;
    private DataGridViewCheckBoxColumn enabled;
    private DataGridViewTextBoxColumn chapterName;
    private DataGridViewTextBoxColumn pos;
    private DataGridViewTextBoxColumn time;
    private DataGridViewCheckBoxColumn special;
    private DataGridViewCheckBoxColumn split;
    private ToolStripMenuItem mouseWheelZoomsToPointerToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator4;
    private Button btnZoomToSelected;
    private ToolStripMenuItem splitEngineToolStripMenuItem;
    private ToolStripMenuItem ffmpegToolStripMenuItem;
    private ToolStripMenuItem mP3SPLTToolStripMenuItem;
    private CheckBox chkSplitNames;
    private Button btnAlignChapters;

    public AdvancedSplitting()
    {
      this.InitializeComponent();
    }

    private void SetChaptersEdited(bool value)
    {
      this.chapterEdited = value;
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      Point mousePosition = Control.MousePosition;
      Control control = (Control) this.Picture1;
      while (control.RectangleToScreen(control.ClientRectangle).Contains(mousePosition))
      {
        control = control.Parent;
        if (control == null || control == this)
        {
          Point client = this.Picture1.PointToClient(mousePosition);
          if (this.mouseWheelZoomsToPointerToolStripMenuItem.Checked)
          {
            int num1 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.Scroll(client.X - this.Picture1.Width / 2);
          }
          if (e.Delta > 0)
          {
            int num2 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.ZoomIn();
            return;
          }
          int num3 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.ZoomOut();
          return;
        }
      }
      base.OnMouseWheel(e);
    }

    private void AdvancedSplitting_Load(object sender, EventArgs e)
    {
      int num1 = (int) this.audioSoundEditor1.InitEditor();
      if (this.myVirtualWav.advancedOptions == null || this.myVirtualWav.advancedOptions.chapterEditorTempFile)
      {
        int num2 = (int) this.audioSoundEditor1.SetStoreMode(enumStoreModes.STORE_MODE_TEMP_FILE);
      }
      else
      {
        int num3 = (int) this.audioSoundEditor1.SetStoreMode(enumStoreModes.STORE_MODE_MEMORY_BUFFER);
        if (this.myVirtualWav.advancedOptions.SplitMode == AdvancedOptions.SplitTypes.MP3SPLT)
        {
          this.ffmpegToolStripMenuItem.Checked = false;
          this.mP3SPLTToolStripMenuItem.Checked = true;
        }
        else
        {
          this.ffmpegToolStripMenuItem.Checked = true;
          this.mP3SPLTToolStripMenuItem.Checked = false;
        }
      }
      this.audioSoundEditor1.UndoEnable(false);
      int num4 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.Create(this.Handle, this.Picture1.Left, this.Picture1.Top, this.Picture1.Width, this.Picture1.Height);
      int num5 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.EnableScrollbarsDuringPlayback(false);
      WANALYZER_GENERAL_SETTINGS settings1 = new WANALYZER_GENERAL_SETTINGS();
      int num6 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsGeneralGet(ref settings1);
      settings1.bAutoRefresh = true;
      int num7 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsGeneralSet(settings1);
      WANALYZER_SCROLLBARS_SETTINGS settings2 = new WANALYZER_SCROLLBARS_SETTINGS();
      int num8 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsScrollbarsGet(ref settings2);
      settings2.bVisibleBottom = false;
      settings2.nHeightInPixels = (short) 40;
      settings2.nType = enumWaveScrollbarType.WSCROLLBAR_TYPE_RECT;
      settings2.nVisibleRangeType = enumScrollbarWaveVisibleRangeType.SCROLL_WAVE_VISIBLE_TRANSP_GLASS_OUTER;
      try
      {
        settings2.colorVisibleRange = Color.White;
      }
      catch (Exception ex)
      {
        Audible.diskLogger(ex.ToString());
      }
      int num9 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsScrollbarsSet(settings2);
      WANALYZER_WAVEFORM_SETTINGS settings3 = new WANALYZER_WAVEFORM_SETTINGS();
      int num10 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsWaveGet(ref settings3);
      settings3.nSelectionMode = enumWaveformSelectionMode.WAVE_SEL_TRANSPARENT_GLASS;
      settings3.nTransparentGlassType = enumTranspGlassType.TRANSP_GLASS_TYPE_3D_VERT;
      settings3.nStereoVisualizationMode = enumWaveformStereoModes.STEREO_MODE_CHANNELS_MIXED;
      int num11 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsWaveSet(settings3);
      int num12 = (int) this.audioSoundEditor1.DisplaySpectrumEnh.Create(this.labelSpectrum.Handle);
      int num13 = (int) this.audioSoundEditor1.DisplaySpectrumEnh.Show(true);
      int num14 = (int) this.audioSoundEditor1.DisplaySpectrumEnh.SettingsGeneralGet(ref this.descSpectrumEnhGeneral);
      this.descSpectrumEnhGeneral.nGraphType = enumSpectrumEnhTypes.SPECTR_ENH_AREA_LEFT_TOP;
      this.descSpectrumEnhGeneral.bBackBitmapVisible = false;
      this.descSpectrumEnhGeneral.nResolution = 8192;
      int num15 = (int) this.audioSoundEditor1.DisplaySpectrumEnh.SettingsGeneralSet(this.descSpectrumEnhGeneral);
      SPECTR_ENH_RULERS_SETTINGS settings4 = new SPECTR_ENH_RULERS_SETTINGS();
      int num16 = (int) this.audioSoundEditor1.DisplaySpectrumEnh.SettingsRulersGet(ref settings4, ref this.m_fontText);
      settings4.bScaleRightVisible = false;
      settings4.bScaleBottomVisible = false;
      int num17 = (int) this.audioSoundEditor1.DisplaySpectrumEnh.SettingsRulersSet(settings4, this.m_fontText);
      this.m_idDspReverbInternal = 0;
      this.m_idDspBalanceInternal = 0;
      this.m_idDspReverbExternal = 0;
      this.m_idDspBalanceExternal = 0;
      this.m_idDspBassBoostExternal = 0;
      this.m_idVstKarmaFxEq = 0;
      int num18 = (int) this.audioSoundEditor1.EnableSoundPreloadForPlayback(true);
      int num19 = (int) this.audioSoundEditor1.EnableAutoWaveAnalysisOnLoad(true);
      this.m_hWndVuMeterLeft = this.CreateVuMeter(this.label18, enumGraphicBarOrientations.GRAPHIC_BAR_ORIENT_VERTICAL);
      this.m_hWndVuMeterRight = this.CreateVuMeter(this.label17, enumGraphicBarOrientations.GRAPHIC_BAR_ORIENT_VERTICAL);
      MenuItem menuItem = new MenuItem("Toggle split point");
      menuItem.Click += new System.EventHandler(this.mnuItemChangeSettings_Click);
      this.m_mnuGraphicItemsContextMenu.MenuItems.Add(menuItem);
      this.chapterNamesNumbersInFilenameToolStripMenuItem.Checked = this.myChapters.includeFileNumbers;
      try
      {
        AdvancedSplitting advancedSplitting = new AdvancedSplitting();
        this.Width = advancedSplitting.Width;
        this.Height = advancedSplitting.Height;
        advancedSplitting.Dispose();
      }
      catch (Exception ex)
      {
        Audible.diskLogger(ex.ToString());
      }
      this.disableEvents = true;
      this.stopWatch.Start();
      if (this.m4bMode)
      {
        this.LockUI();
        this.LoadWholeAAC();
        this.SetChapterMarkers();
      }
      else if (this.cdProcessingMode)
      {
        this.LockUI();
        this.LoadVirtualWAV();
        this.SetChapterMarkers();
      }
      else if (this.fileMode)
        this.loadFileToolStripMenuItem.PerformClick();
      else if (this.overdriveMode)
      {
        this.btnApply.Text = "Save chapter markers";
        this.Text = "Overdrive chapter editor - " + Path.GetFileNameWithoutExtension(Path.GetFileName(this.overdriveFile.mp3Filename));
        this.LockUI();
        int num3 = (int) this.audioSoundEditor1.CloseSound();
        int num20 = (int) this.audioSoundEditor1.SetLoadingMode(enumLoadingModes.LOAD_MODE_NEW);
        if (this.audioSoundEditor1.LoadSound(this.overdriveFile.mp3Filename) != enumErrorCodes.ERR_NOERROR)
        {
          int num21 = (int) MessageBox.Show("Cannot load file " + this.overdriveFile.mp3Filename);
        }
        this.SetChapterMarkers();
        this.myVirtualWav.sampleRate = 22050;
        this.myVirtualWav.channels = 2;
        this.myVirtualWav.aacMode = true;
        this.myVirtualWav.M4BtoWAV(this.overdriveFile.mp3Filename);
      }
      this.TimerMenuEnabler.Enabled = true;
    }

    private void mnuItemChangeSettings_Click(object sender, EventArgs e)
    {
    }

    private void LoadVirtualWAV()
    {
      List<SourcePCM> pcMcollection = this.myVirtualWav.GetPCMcollection(0L, (long) this.myVirtualWav.totalSeconds * (long) (this.myVirtualWav.sampleRate * this.myVirtualWav.channels * this.myVirtualWav.bitsPerChannel / 8));
      this.analyzeStopWatch.Reset();
      this.analyzeStopWatch.Start();
      for (int index = 0; index < pcMcollection.Count; ++index)
      {
        int num = (int) this.audioSoundEditor1.AppendAutomationItemAddFromFile(pcMcollection[index].fileName, 0, -1, 100f);
      }
      int num1 = (int) this.audioSoundEditor1.AppendAutomationExecute();
    }

    private void SetChapterMarkers()
    {
      int chapNum = 1;
      int realChap = 1;
      Audible audible = new Audible();
      foreach (double chap in this.myChapters.GetDoubleList())
      {
        int index = this.dgvChapters.Rows.Add();
        bool flag = true;
        try
        {
          if (chapNum <= this.enabledChapters.Count)
            flag = this.enabledChapters[chapNum - 1];
        }
        catch
        {
          flag = true;
        }
        this.dgvChapters.Rows[index].Cells[0].Value = (object) flag;
        this.dgvChapters.Rows[index].Cells[1].Value = (object) this.myChapters.GetDescription(chapNum - 1);
        short num = 0;
        bool special = false;
        if (chapNum == 1 || this.myChapters.GetDoubleList().Count == chapNum && this.myVirtualWav.totalSeconds - chap < 10.0)
          special = true;
        if (flag)
        {
          num = this.myChapters.generatedDescriptions ? this.AddChapterMarker(chap, chapNum, special, realChap, "") : this.AddChapterMarker(chap, chapNum, special, realChap, this.myChapters.GetChapter(chapNum - 1).description);
          ++realChap;
        }
        else
          this.dgvChapters.Rows[chapNum - 1].DefaultCellStyle.BackColor = Color.Red;
        this.dgvChapters.Rows[index].Cells[2].Value = (object) num;
        this.dgvChapters.Rows[index].Cells[3].Value = (object) chap;
        this.dgvChapters.Rows[index].Cells[4].Value = (object) special;
        ++chapNum;
      }
      this.RefreshGrid();
    }

    private short AddChapterMarker(double chap, int chapNum, bool special, int realChap, string descriptiveText = "")
    {
      WANALYZER_VERTICAL_LINE settings;
      settings.color = Color.Red;
      if (special)
        settings.color = Color.Blue;
      settings.nDashStyle = enumWaveformLineDashStyles.LINE_DASH_STYLE_SOLID;
      settings.nWidth = (short) 2;
      settings.nTranspFactor = (short) 0;
      settings.nHighCap = enumLineCaps.LINE_CAP_SQUARE;
      settings.nLowCap = enumLineCaps.LINE_CAP_SQUARE;
      settings.nDashCap = enumLineDashCaps.LINE_DASH_CAP_FLAT;
      short pos = this.audioSoundEditor1.DisplayWaveformAnalyzer.GraphicItemVerticalLineAdd("Chapter " + (object) chapNum, "Chapter " + (object) chapNum, (int) (chap * 1000.0), settings);
      if (descriptiveText == "")
        this.SetChapterText(pos, chapNum, realChap, "");
      else
        this.SetChapterText(pos, chapNum, realChap, descriptiveText);
      return pos;
    }

    private void SetChapterText(short pos, int internalChapter, int realChapter, string descriptiveText = "")
    {
      WANALYZER_BUDDY_TEXT settings = new WANALYZER_BUDDY_TEXT();
      settings.nAlignment = enumBuddyAlignment.BUDDY_ALIGN_RIGHT_TOP;
      settings.colorText = Color.Yellow;
      Font fontText = new Font("Ariel", 11f);
      string strText = realChapter.ToString() + " (" + (object) internalChapter + ")";
      if (realChapter == internalChapter)
        strText = " " + realChapter.ToString();
      if (!this.myChapters.generatedDescriptions)
        strText = " " + descriptiveText + " (" + (object) internalChapter + ")";
      int num = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GraphicItemBuddyTextSet(pos, strText, settings, ref fontText);
    }

    public void LoadWholeAAC(string file)
    {
      int num1 = (int) this.audioSoundEditor1.CloseSound();
      int num2 = (int) this.audioSoundEditor1.SetLoadingMode(enumLoadingModes.LOAD_MODE_NEW);
      if (this.audioSoundEditor1.LoadSound(file) == enumErrorCodes.ERR_NOERROR)
        return;
      int num3 = (int) MessageBox.Show("Cannot load file " + file);
    }

    public void LoadWholeAAC()
    {
      int num1 = (int) this.audioSoundEditor1.CloseSound();
      int num2 = (int) this.audioSoundEditor1.SetLoadingMode(enumLoadingModes.LOAD_MODE_NEW);
      if (this.audioSoundEditor1.LoadSound(this.sourceAacFile) == enumErrorCodes.ERR_NOERROR)
        return;
      int num3 = (int) MessageBox.Show("Cannot load file " + this.sourceAacFile);
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

    private void AdvancedSplitting_Resize(object sender, EventArgs e)
    {
      try
      {
        int x1 = this.Frame4.Location.X;
        int width = this.dgvChapters.Size.Width;
        int x2 = this.dgvChapters.Location.X;
        this.dgvChapters.Size = new Size(this.Frame4.Location.X - 24, this.dgvChapters.Size.Height);
        this.btnNextDel.Location = new Point(this.dgvChapters.Location.X + this.dgvChapters.Size.Width - this.btnNextDel.Size.Width, this.btnNextDel.Location.Y);
        this.btnAlignChapters.Location = new Point((this.dgvChapters.Location.X + this.dgvChapters.Size.Width - this.btnAlignChapters.Size.Width + 10) / 2, this.btnAlignChapters.Location.Y);
        int num1 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.Move(this.Picture1.Location.X, this.Picture1.Location.Y, this.Picture1.Size.Width, this.Picture1.Size.Height);
        int num2 = (int) this.audioSoundEditor1.DisplaySpectrumEnh.Move(0, 0, this.labelSpectrum.Size.Width, this.labelSpectrum.Size.Height);
        int num3 = (int) this.audioSoundEditor1.GraphicBarsManager.Move(this.m_hWndVuMeterLeft, this.label18.Location.X, this.label18.Location.Y, this.label18.Size.Width, this.label18.Size.Height);
        int num4 = (int) this.audioSoundEditor1.GraphicBarsManager.Move(this.m_hWndVuMeterRight, this.label17.Location.X, this.label17.Location.Y, this.label17.Size.Width, this.label17.Size.Height);
      }
      catch
      {
      }
    }

    private void buttonPlay_Click(object sender, EventArgs e)
    {
      bool bSelectionAvailable = false;
      int nBeginPosInMs = 0;
      int nEndPosInMs = 0;
      int selection = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GetSelection(ref bSelectionAvailable, ref nBeginPosInMs, ref nEndPosInMs);
      if (bSelectionAvailable)
      {
        int num1 = (int) this.audioSoundEditor1.PlaySoundRange(nBeginPosInMs, nEndPosInMs);
      }
      else
      {
        int num2 = (int) this.audioSoundEditor1.PlaySoundRange(0, -1);
      }
    }

    private void TimerMenuEnabler_Tick(object sender, EventArgs e)
    {
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

    private void audioSoundEditor1_SoundLoadingDone(object sender, SoundLoadingDoneEventArgs e)
    {
      this.stopWatch.Stop();
      TimeSpan elapsed = this.stopWatch.Elapsed;
      Audible.diskLogger("File loaded in " + string.Format("{0:00}:{1:00}:{2:00}.{3:00}", (object) elapsed.Hours, (object) elapsed.Minutes, (object) elapsed.Seconds, (object) (elapsed.Milliseconds / 10)));
      if (this.audioSoundEditor1.GetStoreMode() == enumStoreModes.STORE_MODE_TEMP_FILE)
      {
        Audible.diskLogger("TEMP = " + Path.GetTempPath());
        Audible.diskLogger("temp wav: " + this.audioSoundEditor1.GetTempFilePathname() + " - " + string.Format("{0:0,0}", (object) this.audioSoundEditor1.GetTempFileSize()));
      }
      this.stopWatch.Reset();
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
        int soundDuration1 = this.audioSoundEditor1.GetSoundDuration();
        if (this.myChapters.Count() > 0 && this.myChapters.GetLastChapter() * 1000.0 > (double) soundDuration1)
        {
          int pos = this.myChapters.Count() - 1;
          this.myChapters.SetChapter(pos, (double) soundDuration1 / 1000.0);
          this.dgvChapters.Rows[pos].Cells[3].Value = (object) ((double) soundDuration1 / 1000.0);
          int num2 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GraphicItemHorzPositionSet(short.Parse(this.dgvChapters.Rows[pos].Cells[2].Value.ToString()), soundDuration1 - 100, 0);
        }
        if (nMode != enumLoadingModes.LOAD_MODE_NEW)
        {
          this.TimerReload.Enabled = true;
        }
        else
        {
          int soundDuration2 = this.audioSoundEditor1.GetSoundDuration();
          if (soundDuration2 == 0)
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
            this.LabelTotalDuration.Text = this.audioSoundEditor1.GetFormattedTime(soundDuration2, true, true);
            this.LabelTotalDuration.Refresh();
          }
        }
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

    private void audioSoundEditor1_SoundLoadingStarted(object sender, EventArgs e)
    {
      this.LabelStatus.Text = "Status: Loading... 0%";
      this.progressBar1.Value = 0;
      this.progressBar1.Visible = true;
      this.progressBar1.Refresh();
      this.LabelStatus.Refresh();
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
      this.analyzeStopWatch = new Stopwatch();
      this.analyzeStopWatch.Start();
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
      try
      {
        this.myVirtualWav.totalSeconds = (double) this.audioSoundEditor1.GetSoundDuration() / 1000.0;
      }
      catch
      {
      }
      this.LockUI();
      this.disableEvents = false;
      this.analyzeStopWatch.Stop();
      TimeSpan elapsed = this.analyzeStopWatch.Elapsed;
      Audible.diskLogger("Analysis completed in " + string.Format("{0:00}:{1:00}:{2:00}.{3:00}", (object) elapsed.Hours, (object) elapsed.Minutes, (object) elapsed.Seconds, (object) (elapsed.Milliseconds / 10)));
      this.analyzeStopWatch.Reset();
    }

    private void audioSoundEditor1_WaveAnalyzerDisplayRangeChange(object sender, WaveAnalyzerDisplayRangeChangeEventArgs e)
    {
      this.LabelRangeBegin.Text = this.audioSoundEditor1.GetFormattedTime(e.nBeginPosInMs, true, true);
      this.LabelRangeEnd.Text = this.audioSoundEditor1.GetFormattedTime(e.nEndPosInMs, true, true);
      this.LabelRangeDuration.Text = this.audioSoundEditor1.GetFormattedTime(e.nEndPosInMs - e.nBeginPosInMs, true, true);
      if (this.scrolling)
        return;
      int nWidthInMs = 0;
      int nWidthInPixels = 0;
      int displayWidth = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GetDisplayWidth(ref nWidthInMs, ref nWidthInPixels);
      int soundDuration = this.audioSoundEditor1.GetSoundDuration();
      int nBeginPosInMs = 0;
      int nEndPosInMs = 0;
      int displayRange = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GetDisplayRange(ref nBeginPosInMs, ref nEndPosInMs);
      int num1 = (int) ((double) nEndPosInMs / (double) soundDuration * 100.0);
      int num2 = (int) ((double) nWidthInMs / (double) soundDuration * 100.0);
      if (num2 < 2)
        num2 = 2;
      this.scrollBarEx1.LargeChange = num2;
      this.scrollBarEx1.Value = num1;
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
        if (e.nEndPosInMs - e.nBeginPosInMs > 0)
        {
          if (this.grpSilence.Text == "Silence detection")
          {
            this.grpSilence.Text = "Silence detection - CURRENT SELECTION ONLY";
            this.grpSilence.ForeColor = Color.Red;
            this.btnDeselect.Enabled = true;
          }
          this.btnZoomToSelected.Enabled = true;
        }
        else
        {
          this.grpSilence.Text = "Silence detection";
          this.grpSilence.ForeColor = SystemColors.ControlText;
          this.btnDeselect.Enabled = false;
          this.btnZoomToSelected.Enabled = false;
        }
      }
      else
      {
        this.buttonPlaySelection.Enabled = false;
        this.LabelSelectionBegin.Text = "00:00:00.000";
        this.LabelSelectionEnd.Text = "00:00:00.000";
        this.LabelSelectionDuration.Text = "00:00:00.000";
      }
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

    private void audioSoundEditor1_SoundLoadingPerc(object sender, SoundLoadingPercEventArgs e)
    {
      if (this.progressBar1.Value == (int) e.nPercentage)
        return;
      this.progressBar1.Value = (int) e.nPercentage;
      this.LabelStatus.Text = "Status: Loading... " + e.nPercentage.ToString() + "%";
      this.progressBar1.Refresh();
      this.LabelStatus.Refresh();
    }

    private void audioSoundEditor1_VUMeterValueChange(object sender, VUMeterValueChangeEventArgs e)
    {
      int num1 = (int) this.audioSoundEditor1.GraphicBarsManager.SetValue(this.m_hWndVuMeterLeft, (int) e.nPeakLeft);
      int num2 = (int) this.audioSoundEditor1.GraphicBarsManager.SetValue(this.m_hWndVuMeterRight, (int) e.nPeakRight);
    }

    private void audioSoundEditor1_WaveScrollerManualScroll(object sender, WaveScrollerMabualScrollEventArgs e)
    {
      this.currentScrollPosition = e.nNewPosInMs;
    }

    private void audioSoundEditor1_WaveScrollerMouseNotification(object sender, WaveScrollerMouseNotificationEventArgs e)
    {
      if (e.nAction == enumMouseActions.MOUSE_ACTION_RIGHT_CLICK || e.nAction == enumMouseActions.MOUSE_ACTION_LEFT_CLICK || e.nAction != enumMouseActions.MOUSE_ACTION_LEFT_UP)
        return;
      int nBeginPosInMs1 = 0;
      int nEndPosInMs = 0;
      int displayRange = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GetDisplayRange(ref nBeginPosInMs1, ref nEndPosInMs);
      int num1 = (nEndPosInMs - nBeginPosInMs1) / 2;
      int nBeginPosInMs2 = (int) this.currentScrollPosition - num1;
      nEndPosInMs = (int) this.currentScrollPosition + num1;
      int num2 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SetSelection(true, nBeginPosInMs2, nEndPosInMs);
      int selection = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.ZoomToSelection(true);
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

    private void dgvChapters_SelectionChanged(object sender, EventArgs e)
    {
      if (this.disableEvents)
        return;
      if (this.beLessAnnoying)
        this.beLessAnnoying = false;
      if (this.dgvChapters.SelectedRows.Count > 1)
      {
        this.disableEvents = true;
        int num1 = (int) this.audioSoundEditor1.StopSound();
        try
        {
          int nBeginPosInMs = (int) (this.myChapters.GetChapterDouble(this.dgvChapters.SelectedRows[0].Index) * 1000.0);
          int nEndPosInMs = (int) (this.myChapters.GetChapterDouble(this.dgvChapters.SelectedRows[this.dgvChapters.SelectedRows.Count - 1].Index) * 1000.0);
          if (nBeginPosInMs > nEndPosInMs)
          {
            int num2 = nBeginPosInMs;
            nBeginPosInMs = nEndPosInMs;
            nEndPosInMs = num2;
          }
          this.disableEvents = false;
          int num3 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SetSelection(true, nBeginPosInMs, nEndPosInMs);
          int selection = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.ZoomToSelection(true);
          int num4 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SetSelection(true, nBeginPosInMs, nEndPosInMs);
        }
        catch
        {
        }
        this.disableEvents = false;
      }
      else
      {
        this.grpSilence.Text = "Silence detection";
        this.grpSilence.ForeColor = SystemColors.ControlText;
        this.PreviewChapter(-1);
        this.disableEvents = false;
      }
    }

    private void dgvChapters_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
    }

    private void PreviewChapter(int forcedPreview = -1)
    {
      try
      {
        int pos = this.dgvChapters.CurrentCell.RowIndex;
        if (forcedPreview > -1)
          pos = forcedPreview;
        int num1 = 30000;
        int nBeginPosInMs = (int) (this.myChapters.GetChapterDouble(pos) * 1000.0) - num1;
        if (nBeginPosInMs < 0)
          nBeginPosInMs = 0;
        int num2 = (int) (this.myChapters.GetChapterDouble(pos) * 1000.0) + num1;
        int num3 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SetSelection(true, nBeginPosInMs, num2);
        this.zoomToselectionToolStripMenuItem.PerformClick();
        int num4 = (int) this.audioSoundEditor1.PlaySoundRange((int) (this.myChapters.GetChapterDouble(pos) * 1000.0), num2);
      }
      catch
      {
      }
    }

    private void audioSoundEditor1_WaveAnalyzerLineMoveEnd(object sender, WaveAnalyzerLineEventArgs e)
    {
      short nUniqueId = e.nUniqueID;
      int nPosInMs = e.nPosInMs;
      for (int index = 0; index < this.dgvChapters.RowCount; ++index)
      {
        if (this.dgvChapters.Rows[index].Cells[2].Value.ToString() == nUniqueId.ToString())
        {
          this.dgvChapters.Rows[index].Cells[3].Value = (object) ((double) nPosInMs / 1000.0);
          this.myChapters.SetChapter(index, (double) nPosInMs / 1000.0);
          this.PreviewChapter(index);
        }
      }
    }

    private void AdvancedSplitting_FormClosing(object sender, FormClosingEventArgs e)
    {
      bool flag = false;
      if (!(this.tmpAACfile != ""))
        return;
      while (!flag)
      {
        try
        {
          System.IO.File.Delete(this.tmpAACfile);
          flag = true;
        }
        catch
        {
          Thread.Sleep(100);
        }
      }
    }

    private void dgvChapters_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      if (this.disableEvents)
        return;
      if (e.ColumnIndex == 1 && e.RowIndex > -1)
      {
        this.beLessAnnoying = true;
        try
        {
          this.SetChaptersEdited(true);
          this.myChapters.generatedDescriptions = false;
          this.SetChapterText(short.Parse(this.dgvChapters.Rows[e.RowIndex].Cells[2].Value.ToString()), e.RowIndex, e.RowIndex + 1, this.dgvChapters.Rows[e.RowIndex].Cells[1].Value.ToString());
          this.myChapters.SetDescription(e.RowIndex, this.dgvChapters.Rows[e.RowIndex].Cells[1].Value.ToString());
        }
        catch
        {
        }
      }
      if (e.ColumnIndex != 0 || e.RowIndex == -1)
        return;
      this.SetChaptersEdited(true);
      this.beLessAnnoying = true;
      if (bool.Parse(this.dgvChapters.Rows[e.RowIndex].Cells[0].Value.ToString()))
      {
        try
        {
          bool special = false;
          try
          {
            special = bool.Parse(this.dgvChapters.Rows[e.RowIndex].Cells[4].Value.ToString());
          }
          catch
          {
          }
          this.dgvChapters.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
          short num = this.AddChapterMarker(double.Parse(this.dgvChapters.Rows[e.RowIndex].Cells[3].Value.ToString()), e.RowIndex + 1, special, e.RowIndex + 1, "");
          this.dgvChapters.Rows[e.RowIndex].Cells[2].Value = (object) num;
        }
        catch
        {
        }
      }
      else
      {
        try
        {
          this.dgvChapters.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
          int num = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GraphicItemRemove((short) int.Parse(this.dgvChapters.Rows[e.RowIndex].Cells[2].Value.ToString()));
        }
        catch
        {
        }
      }
      this.FixChapterBuddyText();
    }

    private void DeleteChapter(int row)
    {
      int num = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GraphicItemRemove((short) int.Parse(this.dgvChapters.Rows[row].Cells[2].Value.ToString()));
      this.dgvChapters.Rows[row].Cells[0].Value = (object) true;
      this.dgvChapters.Rows.RemoveAt(row);
      this.myChapters.Remove(row);
    }

    private void FixChapterBuddyText()
    {
      int realChapter = 1;
      for (int index = 0; index < this.dgvChapters.Rows.Count; ++index)
      {
        try
        {
          int num = int.Parse(this.dgvChapters.Rows[index].Cells[2].Value.ToString());
          if (!this.myChapters.generatedDescriptions)
            this.SetChapterText((short) num, index + 1, realChapter, this.dgvChapters.Rows[index].Cells[1].Value.ToString());
          else
            this.SetChapterText((short) num, index + 1, realChapter, "");
          if (bool.Parse(this.dgvChapters.Rows[index].Cells[0].Value.ToString()))
            ++realChapter;
        }
        catch
        {
        }
      }
    }

    private void dgvChapters_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
    {
      if (e.ColumnIndex != 0 || e.RowIndex == -1)
        return;
      this.dgvChapters.EndEdit();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btnApply_Click(object sender, EventArgs e)
    {
      if (this.overdriveMode)
      {
        this.applied = true;
        this.myChapters.Clear();
        int pos = 0;
        for (int index = 0; index < this.dgvChapters.Rows.Count; ++index)
        {
          if (bool.Parse(this.dgvChapters.Rows[index].Cells[0].Value.ToString()))
          {
            this.myChapters.Add(double.Parse(this.dgvChapters.Rows[index].Cells[3].Value.ToString()));
            if (!this.myChapters.generatedDescriptions)
              this.myChapters.SetDescription(pos, this.dgvChapters.Rows[index].Cells[1].Value.ToString());
            ++pos;
            if (this.dgvChapters.Rows[index].Cells[5].Value != null && bool.Parse(this.dgvChapters.Rows[index].Cells[5].Value.ToString()))
              this.splitPoints.Add(double.Parse(this.dgvChapters.Rows[index].Cells[3].Value.ToString()));
          }
        }
        this.overdriveFile.SetChapters(this.myChapters);
        this.Close();
      }
      else if (!this.fileMode)
      {
        this.applied = true;
        this.myChapters.Clear();
        int pos = 0;
        for (int index = 0; index < this.dgvChapters.Rows.Count; ++index)
        {
          if (bool.Parse(this.dgvChapters.Rows[index].Cells[0].Value.ToString()))
          {
            this.myChapters.Add(double.Parse(this.dgvChapters.Rows[index].Cells[3].Value.ToString()));
            if (!this.myChapters.generatedDescriptions)
              this.myChapters.SetDescription(pos, this.dgvChapters.Rows[index].Cells[1].Value.ToString());
            ++pos;
            if (this.dgvChapters.Rows[index].Cells[5].Value != null && bool.Parse(this.dgvChapters.Rows[index].Cells[5].Value.ToString()))
              this.splitPoints.Add(double.Parse(this.dgvChapters.Rows[index].Cells[3].Value.ToString()));
          }
        }
        this.Close();
      }
      else
      {
        string aacFile = this.myVirtualWav.aacFile;
        string str = Path.GetExtension(aacFile).TrimStart('.');
        this.saveFileDialog1 = new SaveFileDialog();
        this.saveFileDialog1.DefaultExt = str;
        this.saveFileDialog1.Filter = str.ToUpper() + " files (*." + str + ")|*" + str + "|All files (*.*)|*.*";
        this.saveFileDialog1.AddExtension = true;
        if (this.saveFileDialog1.FileName == null || this.saveFileDialog1.FileName == "")
          this.saveFileDialog1.FileName = Path.GetFileName(aacFile.Replace(".tmp", ""));
        if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
        {
          this.myChapters.Clear();
          for (int index = 0; index < this.dgvChapters.Rows.Count; ++index)
          {
            if (bool.Parse(this.dgvChapters.Rows[index].Cells[0].Value.ToString()))
              this.myChapters.Add(double.Parse(this.dgvChapters.Rows[index].Cells[3].Value.ToString()), this.dgvChapters.Rows[index].Cells[1].Value.ToString());
          }
          int num = (int) this.audioSoundEditor1.StopSound();
          this.LockUI();
          this.backgroundWorker3.RunWorkerAsync();
        }
        this.saveFileDialog1.Dispose();
      }
    }

    private bool QuickSplitExternalFile(string targetFile)
    {
      string directoryName = Path.GetDirectoryName(targetFile);
      string withoutExtension = Path.GetFileNameWithoutExtension(targetFile);
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.ffmpegPath;
      process.StartInfo.WorkingDirectory = directoryName;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      string str1 = "";
      string str2 = this.myVirtualWav.aacFile;
      if (this.origM4B != "")
        str2 = this.origM4B;
      string str3 = Path.GetExtension(str2);
      string str4 = "";
      string str5 = "";
      if (str3.ToLower() == ".m4b" || str3.ToLower() == ".m4a" || str3.ToLower() == ".mp4")
      {
        str4 = " -map 0 -vn ";
        str3 = ".m4a";
      }
      else if (this.mP3SPLTToolStripMenuItem.Checked && (str3.ToLower() == ".mp3" || str3.ToLower() == ".flac"))
      {
        this.MP3SPLT(targetFile);
        return true;
      }
      List<string> stringList = new List<string>();
      for (int pos = 0; pos < this.myChapters.GetDoubleList().Count; ++pos)
      {
        string str6 = withoutExtension + " - " + (pos + 1).ToString("D3") + str3;
        if (this.chkSplitNames.Checked)
          str6 = withoutExtension + " - " + (pos + 1).ToString("D3") + " - " + this.myChapters.GetChapter(pos).description + str3;
        stringList.Add(directoryName + "\\" + str6);
        if (this.myChapters.GetDoubleList().Count != pos + 1)
          str1 = str1 + " " + str5 + " -c copy " + str4 + " -t " + (this.myChapters.GetChapterDouble(pos + 1) - this.myChapters.GetChapterDouble(pos)).ToString("0.000", (IFormatProvider) CultureInfo.InvariantCulture) + " -ss " + this.myChapters.GetChapterDouble(pos).ToString("0.000", (IFormatProvider) CultureInfo.InvariantCulture) + " \"" + str6 + "\"";
        else
          str1 = str1 + " " + str5 + " -c copy " + str4 + " -ss " + this.myChapters.GetChapterDouble(pos).ToString("0.000", (IFormatProvider) CultureInfo.InvariantCulture) + " \"" + str6 + "\"";
      }
      process.StartInfo.Arguments = string.Format("-y -i \"{0}\" -write_xing 0 {1}", (object) str2, (object) str1);
      if (process.StartInfo.Arguments.Length > 8000)
        return false;
      process.StartInfo.UseShellExecute = false;
      Audible.diskLogger(process.StartInfo.FileName.ToString() + " " + process.StartInfo.Arguments.ToString());
      process.Start();
      process.WaitForExit();
      int exitCode = process.ExitCode;
      try
      {
        if (exitCode == 0)
        {
          Audible tmpAudible = new Audible();
          tmpAudible.ffmpegPath = this.ffmpegPath;
          tmpAudible.GetGenericMetaDataTagLib(str2);
          tmpAudible.GetCoverArt(str2);
          for (int index = 0; index < stringList.Count; ++index)
            this.TagSplitFile(stringList[index], index + 1, stringList.Count, tmpAudible);
        }
      }
      catch (Exception ex)
      {
        Audible.diskLogger("Failed to update tags: + " + ex.ToString());
      }
      return true;
    }

    private void TagSplitFile(string fileName, int track, int totalTracks, Audible tmpAudible)
    {
      TagLib.File file = TagLib.File.Create(fileName);
      TagLib.Id3v2.Tag tag = (TagLib.Id3v2.Tag) file.GetTag(TagTypes.Id3v2, false);
      if (tag != null)
        tag.Version = (byte) 3;
      if (tmpAudible.title != null)
        file.Tag.Title = tmpAudible.title;
      if (tmpAudible.author != null)
        file.Tag.Performers[0] = tmpAudible.author;
      if (tmpAudible.year != null)
        file.Tag.Year = uint.Parse(tmpAudible.year);
      if (tmpAudible.narrator != null)
        file.Tag.Composers[0] = tmpAudible.narrator;
      if (tmpAudible.GetComments() != null)
        file.Tag.Comment = tmpAudible.GetComments();
      if (tmpAudible.album != null)
        file.Tag.Album = tmpAudible.album;
      file.Tag.Track = (uint) track;
      file.Tag.TrackCount = (uint) totalTracks;
      if (tmpAudible.hasCoverArt)
        file.Tag.Pictures = new IPicture[1]
        {
          tmpAudible.coverArt
        };
      int num = 10;
      while (num > 0)
      {
        try
        {
          file.Save();
          num = 0;
        }
        catch
        {
          --num;
          Thread.Sleep(200);
        }
      }
    }

    private void MP3SPLT(string targetFile)
    {
      string directoryName = Path.GetDirectoryName(targetFile);
      string withoutExtension = Path.GetFileNameWithoutExtension(targetFile);
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.supportLibs.mp3SplitPath;
      process.StartInfo.WorkingDirectory = directoryName;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      string str = "";
      string aacFile = this.myVirtualWav.aacFile;
      string extension = Path.GetExtension(aacFile);
      List<string> stringList = new List<string>();
      for (int pos = 0; pos < this.myChapters.GetDoubleList().Count; ++pos)
      {
        stringList.Add(directoryName + "\\" + withoutExtension + " - " + Utilities.GetLeadingZeroesFileNumber(pos + 1, this.myChapters.GetDoubleList().Count) + extension);
        str = this.myChapters.GetDoubleList().Count == pos + 1 ? str + "EOF" : str + AdvancedSplitting.SecondsToMp3SpltTime(this.myChapters.GetChapterDouble(pos)) + " ";
      }
      process.StartInfo.Arguments = string.Format("-f \"{0}\" {1} -d \"{3}\" -o \"{2}\"", (object) aacFile, (object) str, (object) (withoutExtension + " - @n"), (object) directoryName);
      process.StartInfo.UseShellExecute = false;
      Audible.diskLogger(process.StartInfo.FileName.ToString() + " " + process.StartInfo.Arguments.ToString());
      process.Start();
      process.WaitForExit();
      int exitCode = process.ExitCode;
      try
      {
        if (exitCode != 0)
          return;
        Audible tmpAudible = new Audible();
        tmpAudible.ffmpegPath = this.ffmpegPath;
        tmpAudible.GetGenericMetaDataTagLib(aacFile);
        tmpAudible.GetCoverArt(aacFile);
        for (int index = 0; index < stringList.Count; ++index)
          this.TagSplitFile(stringList[index], index + 1, stringList.Count, tmpAudible);
      }
      catch (Exception ex)
      {
        Audible.diskLogger("Failed to update tags: + " + ex.ToString());
      }
    }

    public static string SecondsToMp3SpltTime(double time)
    {
      int num1 = (int) time / 60;
      double num2 = time - (double) (num1 * 60);
      return num1.ToString() + "." + num2.ToString("0.00", (IFormatProvider) CultureInfo.InvariantCulture);
    }

    private void SplitExternalFile(string targetFile)
    {
      string directoryName = Path.GetDirectoryName(targetFile);
      string withoutExtension = Path.GetFileNameWithoutExtension(targetFile);
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.ffmpegPath;
      process.StartInfo.WorkingDirectory = directoryName;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      string aacFile = this.myVirtualWav.aacFile;
      string str1 = Path.GetExtension(aacFile);
      string str2 = "";
      if (str1.ToLower() == ".m4b" || str1.ToLower() == ".m4a" || str1.ToLower() == ".mp4")
      {
        str2 = " -map 0 -vn ";
        str1 = ".m4a";
      }
      else if (this.mP3SPLTToolStripMenuItem.Checked && (str1.ToLower() == ".mp3" || str1.ToLower() == ".flac"))
      {
        this.MP3SPLT(targetFile);
        return;
      }
      List<string> stringList = new List<string>();
      for (int pos = 0; pos < this.myChapters.GetDoubleList().Count; ++pos)
      {
        string str3 = withoutExtension + " - " + (pos + 1).ToString("D3") + str1;
        stringList.Add(directoryName + "\\" + str3);
        string str4;
        if (this.myChapters.GetDoubleList().Count != pos + 1)
          str4 = " -c copy " + str2 + " -t " + (this.myChapters.GetChapterDouble(pos + 1) - this.myChapters.GetChapterDouble(pos)).ToString("0.000", (IFormatProvider) CultureInfo.InvariantCulture) + " -ss " + this.myChapters.GetChapterDouble(pos).ToString("0.000", (IFormatProvider) CultureInfo.InvariantCulture) + " \"" + str3 + "\"";
        else
          str4 = " -c copy " + str2 + " -ss " + this.myChapters.GetChapterDouble(pos).ToString("0.000", (IFormatProvider) CultureInfo.InvariantCulture) + " \"" + str3 + "\"";
        process.StartInfo.Arguments = string.Format("-y -i \"{0}\" -write_xing 0 {1}", (object) aacFile, (object) str4);
        process.StartInfo.UseShellExecute = false;
        Audible.diskLogger(process.StartInfo.FileName.ToString() + " " + process.StartInfo.Arguments.ToString());
        process.Start();
        process.WaitForExit();
        this.backgroundWorker3.ReportProgress((int) ((double) (pos + 1) / (double) this.myChapters.GetDoubleList().Count * 100.0));
      }
      int exitCode = process.ExitCode;
      try
      {
        if (exitCode != 0)
          return;
        Audible tmpAudible = new Audible();
        tmpAudible.GetGenericMetaDataTagLib(aacFile);
        tmpAudible.GetCoverArt(aacFile);
        for (int index = 0; index < stringList.Count; ++index)
          this.TagSplitFile(stringList[index], index + 1, stringList.Count, tmpAudible);
      }
      catch (Exception ex)
      {
        Audible.diskLogger("Failed to update tags: + " + ex.ToString());
      }
    }

    private void button1_Click_1(object sender, EventArgs e)
    {
      this.LoadVirtualWAV();
    }

    private void audioSoundEditor1_AppendAutomationDone(object sender, AppendAutomationDoneEventArgs e)
    {
      this.LabelStatus.Text = "Status: Idle";
      this.LabelStatus.Refresh();
      this.progressBar1.Visible = false;
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
          int num = (int) this.audioSoundEditor1.CloseSound();
          this.LabelRangeBegin.Text = "00:00:00.000";
          this.LabelRangeEnd.Text = "00:00:00.000";
          this.LabelRangeDuration.Text = "00:00:00.000";
          this.LabelSelectionBegin.Text = "00:00:00.000";
          this.LabelSelectionEnd.Text = "00:00:00.000";
          this.LabelSelectionDuration.Text = "00:00:00.000";
          this.LabelTotalDuration.Text = "00:00:00.000";
          return;
        }
        this.LabelTotalDuration.Text = this.audioSoundEditor1.GetFormattedTime(soundDuration, true, true);
        this.LabelTotalDuration.Refresh();
      }
      int num1 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.AnalyzeFullSound();
      this.analyzeStopWatch.Stop();
      TimeSpan elapsed = this.stopWatch.Elapsed;
      Audible.diskLogger("WAV segments loaded in " + string.Format("{0:00}:{1:00}:{2:00}.{3:00}", (object) elapsed.Hours, (object) elapsed.Minutes, (object) elapsed.Seconds, (object) (elapsed.Milliseconds / 10)));
      this.analyzeStopWatch.Reset();
    }

    private void audioSoundEditor1_AppendAutomationTotalPerc(object sender, AppendAutomationTotalPercEventArgs e)
    {
      if (this.progressBar1.Value == (int) e.nPercentage)
        return;
      this.progressBar1.Value = (int) e.nPercentage;
      this.LabelStatus.Text = "Status: Loading... " + e.nPercentage.ToString() + "%";
      this.progressBar1.Refresh();
      this.LabelStatus.Refresh();
    }

    private void audioSoundEditor1_AppendAutomationStarted(object sender, EventArgs e)
    {
      this.LabelStatus.Text = "Status: Loading... 0%";
      this.progressBar1.Value = 0;
      this.progressBar1.Visible = true;
      this.progressBar1.Refresh();
      this.LabelStatus.Refresh();
    }

    private void btnNextKeep_Click(object sender, EventArgs e)
    {
      this.MoveAndMarkChapter(1, true);
      this.PreviewChapter(-1);
    }

    private void MoveAndMarkChapter(int direction, bool keep)
    {
      if (this.dgvChapters.CurrentCell == null)
      {
        this.dgvChapters.Rows[0].Selected = true;
        this.dgvChapters.CurrentCell = this.dgvChapters.Rows[0].Cells[0];
        this.dgvChapters.Rows[0].Cells[0].Value = (object) keep;
      }
      else if (this.dgvChapters.CurrentCell.RowIndex + 1 == this.myChapters.Count() && direction == 1)
      {
        this.dgvChapters.Rows[this.dgvChapters.CurrentCell.RowIndex].Cells[0].Value = (object) keep;
        this.dgvChapters.Rows[this.dgvChapters.CurrentCell.RowIndex].Selected = false;
        this.dgvChapters.Rows[0].Selected = true;
        this.dgvChapters.CurrentCell = this.dgvChapters.Rows[0].Cells[0];
      }
      else if (this.dgvChapters.CurrentCell.RowIndex == 0 && direction == -1)
      {
        this.dgvChapters.Rows[this.myChapters.Count() - 1].Cells[0].Value = (object) keep;
        this.dgvChapters.Rows[this.myChapters.Count() - 1].Selected = false;
        this.dgvChapters.Rows[this.myChapters.Count() - 1].Selected = true;
        this.dgvChapters.CurrentCell = this.dgvChapters.Rows[this.myChapters.Count() - 1].Cells[0];
      }
      else
      {
        this.dgvChapters.Rows[this.dgvChapters.CurrentCell.RowIndex].Cells[0].Value = (object) keep;
        this.dgvChapters.Rows[this.dgvChapters.CurrentCell.RowIndex].Selected = false;
        if (this.myChapters.Count() > 1)
          this.dgvChapters.Rows[this.dgvChapters.CurrentCell.RowIndex + direction].Selected = true;
        this.dgvChapters.CurrentCell = this.dgvChapters.Rows[this.dgvChapters.CurrentCell.RowIndex + direction].Cells[0];
      }
    }

    private void btnNextDel_Click(object sender, EventArgs e)
    {
      this.MoveAndMarkChapter(1, false);
      this.PreviewChapter(-1);
    }

    private void btnFindSilence_Click(object sender, EventArgs e)
    {
      this.alignChapters = false;
      bool bSelectionAvailable = false;
      int nBeginPosInMs = 0;
      int nEndPosInMs = 0;
      int selection = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GetSelection(ref bSelectionAvailable, ref nBeginPosInMs, ref nEndPosInMs);
      if (this.chapterEdited && nEndPosInMs - nBeginPosInMs == 0)
      {
        if (MessageBox.Show("You have edited the chapter list and have not selected a range to search.\n\nWould you like to erase the current chapter list and replace it with the newly detected chapters?", "Erase modified chapters?", MessageBoxButtons.YesNo) == DialogResult.No)
          return;
        this.SetChaptersEdited(false);
      }
      this.LockUI();
      this.disableEvents = true;
      this.progressBar1.Visible = true;
      int num = (int) this.audioSoundEditor1.SilencePositionsDetect((short) 800, (int) (double.Parse(this.txtSilenceThresh.Text) * 1000.0), false);
    }

    private void LockUI()
    {
      this.btnApply.Enabled = !this.btnApply.Enabled;
      this.btnCancel.Enabled = !this.btnCancel.Enabled;
      this.grpSilence.Enabled = !this.grpSilence.Enabled;
      this.btnNextDel.Enabled = !this.btnNextDel.Enabled;
      this.btnNextKeep.Enabled = !this.btnNextKeep.Enabled;
      this.dgvChapters.Enabled = !this.dgvChapters.Enabled;
      this.btnSpectogram.Enabled = !this.btnSpectogram.Enabled;
      this.chkSplitNames.Enabled = !this.chkSplitNames.Enabled;
    }

    private double FormattedToSeconds(string text)
    {
      string[] strArray = text.Split(':');
      return double.Parse(strArray[2]) + double.Parse(strArray[1]) * 60.0 + double.Parse(strArray[0]) * 60.0 * 60.0;
    }

    private void NewSilenceSearch()
    {
      bool flag = false;
      double seconds1 = this.FormattedToSeconds(this.LabelSelectionBegin.Text);
      double seconds2 = this.FormattedToSeconds(this.LabelSelectionEnd.Text);
      if (seconds2 - seconds1 > 0.0 || this.dgvChapters.SelectedRows.Count > 1)
        flag = true;
      int nRangeStart = 0;
      int nRangeEnd = 0;
      if (!flag)
      {
        this.myChapters.Clear();
        this.myChapters.Add(0.0);
        for (int nPortionIndex = 0; nPortionIndex < this.audioSoundEditor1.SilencePositionsGetNum(); ++nPortionIndex)
        {
          int range = (int) this.audioSoundEditor1.SilencePositionsGetRange(nPortionIndex, ref nRangeStart, ref nRangeEnd);
          double time = ((double) nRangeStart + (double) (nRangeEnd - nRangeStart) / 2.0) / 1000.0;
          if (this.middleToolStripMenuItem.Checked)
            time = ((double) nRangeStart + (double) (nRangeEnd - nRangeStart) / 2.0) / 1000.0;
          else if (this.startToolStripMenuItem.Checked)
            time = ((double) nRangeStart + 0.1) / 1000.0;
          else if (this.endToolStripMenuItem.Checked)
            time = ((double) nRangeEnd - 0.1) / 1000.0;
          this.myChapters.Add(time);
        }
      }
      else
      {
        List<double> newChaps = new List<double>();
        for (int nPortionIndex = 0; nPortionIndex < this.audioSoundEditor1.SilencePositionsGetNum(); ++nPortionIndex)
        {
          int range = (int) this.audioSoundEditor1.SilencePositionsGetRange(nPortionIndex, ref nRangeStart, ref nRangeEnd);
          double num = (double) (nRangeEnd - 1000) / 1000.0;
          if (this.middleToolStripMenuItem.Checked)
            num = ((double) nRangeStart + (double) (nRangeEnd - nRangeStart) / 2.0) / 1000.0;
          else if (this.startToolStripMenuItem.Checked)
            num = ((double) nRangeStart + 0.1) / 1000.0;
          else if (this.endToolStripMenuItem.Checked)
            num = ((double) nRangeEnd - 0.1) / 1000.0;
          if (num > seconds1 && num < seconds2)
            newChaps.Add(num);
        }
        if (newChaps.Count <= 0)
          return;
        this.myChapters = this.MergeChapters2(newChaps, this.myChapters);
      }
    }

    private void FindSoxSilence()
    {
      Lame myLame1 = new Lame();
      myLame1.myAdvancedOptions = this.myVirtualWav.advancedOptions;
      myLame1.ffmpegPath = this.ffmpegPath;
      int num1 = 0;
      int num2 = (int) ((double) this.audioSoundEditor1.GetSoundDuration() / 1000.0);
      EncodingOptions myEncodingOptions = new EncodingOptions();
      bool flag = false;
      double seconds1 = this.FormattedToSeconds(this.LabelSelectionBegin.Text);
      double seconds2 = this.FormattedToSeconds(this.LabelSelectionEnd.Text);
      if (seconds2 - seconds1 > 0.0)
      {
        num1 = (int) seconds1;
        num2 = (int) seconds2;
        flag = true;
      }
      myEncodingOptions.encoder = "soxsilence";
      myEncodingOptions.startChap = (long) num1;
      myEncodingOptions.endChap = (long) num2;
      myEncodingOptions.silenceThreshold = double.Parse(this.txtSilenceThresh.Text.Trim());
      myLame1.soxPath = this.soxPath;
      myEncodingOptions.channels = 1;
      myEncodingOptions.sampleRate = 8000;
      string outputTextFile = this.myVirtualWav.advancedOptions.GetTempPath() + "\\chapters.txt";
      Task task = (Task) Task.Factory.StartNew<int>((System.Func<int>) (() => myLame1.PreprocessVirtualWav(this.myVirtualWav, outputTextFile, myEncodingOptions)));
      while (!task.IsCompleted)
      {
        Thread.Sleep(1000);
        this.backgroundWorker1.ReportProgress(myLame1.percentComplete);
      }
      if (!flag)
      {
        this.myChapters.SetDoubleChapters(this.ParseSoxSilence(outputTextFile));
      }
      else
      {
        List<double> soxSilence = this.ParseSoxSilence(outputTextFile);
        soxSilence.RemoveAt(0);
        for (int index = 0; index < soxSilence.Count; ++index)
          soxSilence[index] = soxSilence[index] + seconds1;
        this.zoomOutAfterCompletion = false;
        this.selectedIndex = this.dgvChapters.CurrentCell.RowIndex;
        if (soxSilence.Count <= 0)
          return;
        this.myChapters.SetDoubleChapters(this.MergeChapters(soxSilence, this.myChapters.GetDoubleList()));
      }
    }

    private List<double> MergeChapters(List<double> newChaps, List<double> oldChaps)
    {
      List<double> doubleList = new List<double>();
      int num = newChaps.Count + oldChaps.Count;
      int index1 = 0;
      int index2 = 0;
      bool flag = false;
      this.enabledChapters.Clear();
      for (int index3 = 0; index3 < num; ++index3)
      {
        if (oldChaps.Count == index1)
        {
          doubleList.Add(newChaps[index2]);
          this.enabledChapters.Add(true);
          ++index2;
        }
        else if (oldChaps[index1] - newChaps[index2] <= 0.0 || flag)
        {
          this.enabledChapters.Add(bool.Parse(this.dgvChapters.Rows[index1].Cells[0].Value.ToString()));
          doubleList.Add(oldChaps[index1]);
          ++index1;
        }
        else if (oldChaps[index1] - newChaps[index2] > 0.0)
        {
          doubleList.Add(newChaps[index2]);
          this.enabledChapters.Add(true);
          ++index2;
          if (index2 == newChaps.Count)
          {
            flag = true;
            --index2;
          }
        }
      }
      return doubleList;
    }

    private AdvancedSplitting.Chapters MergeChapters2(List<double> newChaps, AdvancedSplitting.Chapters oldChaps)
    {
      AdvancedSplitting.Chapters chapters = new AdvancedSplitting.Chapters();
      int num = newChaps.Count + oldChaps.Count();
      int pos = 0;
      int index1 = 0;
      bool flag = false;
      this.enabledChapters.Clear();
      for (int index2 = 0; index2 < num; ++index2)
      {
        if (oldChaps.Count() == pos)
        {
          chapters.Add(newChaps[index1], "(End)");
          this.enabledChapters.Add(true);
          ++index1;
        }
        else if (oldChaps.GetChapterDouble(pos) - newChaps[index1] <= 0.0 || flag)
        {
          this.enabledChapters.Add(bool.Parse(this.dgvChapters.Rows[pos].Cells[0].Value.ToString()));
          chapters.Add(oldChaps.GetChapterDouble(pos), oldChaps.GetDescription(pos));
          ++pos;
        }
        else if (oldChaps.GetChapterDouble(pos) - newChaps[index1] > 0.0)
        {
          chapters.Add(newChaps[index1], "new");
          this.enabledChapters.Add(true);
          ++index1;
          if (index1 == newChaps.Count)
          {
            flag = true;
            --index1;
          }
        }
      }
      return chapters;
    }

    private List<double> ParseSoxSilence(string outputTextFile)
    {
      List<double> doubleList = new List<double>();
      List<string> soxOutput = Form1.ParseSoxOutput(outputTextFile);
      doubleList.Add(0.0);
      foreach (string s in soxOutput)
        doubleList.Add(TimeSpan.Parse(s).TotalSeconds - 1.0);
      return doubleList;
    }

    private void ClearChapters(bool uiOnly = false)
    {
      for (int index = 0; index < this.dgvChapters.Rows.Count; ++index)
      {
        int num = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GraphicItemRemove((short) int.Parse(this.dgvChapters.Rows[index].Cells[2].Value.ToString()));
      }
      this.dgvChapters.Rows.Clear();
      if (uiOnly)
        return;
      this.myChapters.Clear();
    }

    private void button1_Click_2(object sender, EventArgs e)
    {
      int fullSound = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.ZoomToFullSound();
    }

    private void btnZoomIn_Click(object sender, EventArgs e)
    {
      int selection = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.ZoomToSelection(true);
    }

    private void button1_Click_3(object sender, EventArgs e)
    {
    }

    private void dgvChapters_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
    {
    }

    private void CopyCleanMP4(string file, string wFile)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.mp4boxPath;
      process.StartInfo.Arguments = "-raw 1 \"" + file + "\" -out \"" + wFile + "\"";
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.UseShellExecute = false;
      process.Start();
      process.WaitForExit();
    }

    private void btnFixedSplit_Click(object sender, EventArgs e)
    {
      this.alignChapters = false;
      this.LockUI();
      this.disableEvents = true;
      this.ClearChapters(false);
      int num1 = (int) this.audioSoundEditor1.StopSound();
      this.audioSoundEditor1.SilencePosDetectionDone -= new AudioSoundEditor.AudioSoundEditor.SilencePosDetectionDoneEventHandler(this.audioSoundEditor1_SilencePosDetectionDone);
      this.audioSoundEditor1.SilencePosDetectionDone += new AudioSoundEditor.AudioSoundEditor.SilencePosDetectionDoneEventHandler(this.audioSoundEditor1_SilencePosDetectionDoneFixedDuration);
      int num2 = (int) this.audioSoundEditor1.SilencePositionsDetect((short) 800, 500, false);
    }

    public void NewFixedSplit()
    {
      double num1 = double.Parse(this.txtFixedSplit.Text.Trim());
      this.myVirtualWav.totalSeconds = (double) this.audioSoundEditor1.GetSoundDuration() / 1000.0;
      int num2 = (int) (this.myVirtualWav.totalSeconds / 60.0 / num1);
      for (int index = 0; index <= num2; ++index)
        this.myChapters.Add(this.GetNearestSilence((double) index * num1 * 60.0));
    }

    private double GetNearestSilence(double chap)
    {
      double num1 = chap;
      int nRangeStart = 0;
      int nRangeEnd = 0;
      for (int nPortionIndex = 0; nPortionIndex < this.audioSoundEditor1.SilencePositionsGetNum(); ++nPortionIndex)
      {
        int range = (int) this.audioSoundEditor1.SilencePositionsGetRange(nPortionIndex, ref nRangeStart, ref nRangeEnd);
        double num2 = (double) ((nRangeEnd - nRangeStart) / 2 + nRangeStart) / 1000.0;
        if (num2 > chap)
          return num2;
      }
      return num1;
    }

    public void FixedSplit()
    {
      double num1 = double.Parse(this.txtFixedSplit.Text.Trim());
      int num2 = (int) (this.myVirtualWav.totalSeconds / 60.0 / num1);
      for (int index = 0; index <= num2; ++index)
      {
        this.backgroundWorker2.ReportProgress((int) ((double) (index + 1) / (double) num2 * 100.0));
        this.myChapters.Add(this.VerifyChapterSplit((double) index * num1 * 60.0));
      }
    }

    public double VerifyChapterSplit(double chapter)
    {
      double num1 = chapter;
      double num2 = 30.0;
      double num3 = chapter - num2;
      double num4 = num3 + num2 * 2.0;
      string str = this.myVirtualWav.advancedOptions.GetTempPath() + "\\tmpchap.txt";
      EncodingOptions myEncodingOptions = new EncodingOptions();
      myEncodingOptions.sampleRate = 44100;
      Lame lame = new Lame();
      myEncodingOptions.encoder = "detectsilence";
      myEncodingOptions.startChap = (long) num3 + (long) num2;
      myEncodingOptions.endChap = (long) num4;
      double num5 = num3 + num2;
      double num6 = num4;
      myEncodingOptions.dStartChap = num5;
      myEncodingOptions.dEndChap = num6;
      myEncodingOptions.doubleChapters = true;
      lame.soxPath = this.soxPath;
      lame.PreprocessVirtualWav(this.myVirtualWav, str, myEncodingOptions);
      if (Form1.DetectSilence(str) && this.myVirtualWav.advancedOptions.doNotVerifyIfSilence)
      {
        Audible.diskLogger("Split falls on silence; skipping...");
        return num1;
      }
      myEncodingOptions.encoder = "soxsilence";
      myEncodingOptions.startChap = (long) num3;
      myEncodingOptions.dStartChap = num3;
      List<string> stringList = new List<string>();
      myEncodingOptions.silenceThreshold = 1.0;
      while (stringList.Count != 1 && myEncodingOptions.silenceThreshold >= 0.25)
      {
        lame.PreprocessVirtualWav(this.myVirtualWav, str, myEncodingOptions);
        myEncodingOptions.silenceThreshold -= 0.25;
        stringList = Form1.ParseSoxOutput(str);
        if (stringList.Count > 0)
          break;
      }
      if (stringList.Count > 0)
      {
        string s = stringList[stringList.Count - 1];
        double totalSeconds = TimeSpan.Parse(s).TotalSeconds;
        Audible.diskLogger("Split on silence found at " + s + " - " + (object) totalSeconds);
        num1 = num3 + totalSeconds;
        Audible.diskLogger("original split = " + (object) chapter + ", star range: " + (object) num3 + ", detected offset: " + (object) totalSeconds + ", new = " + (object) num1 + ", thresh = " + (object) myEncodingOptions.silenceThreshold);
      }
      else
        Audible.diskLogger("Could not find any silence @ " + (object) chapter);
      return num1;
    }

    private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
    {
      this.disableEvents = true;
      this.FindSoxSilence();
    }

    private void backgroundWorker1_DoWork2(object sender, DoWorkEventArgs e)
    {
      this.FixedSplit();
    }

    private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      if (e.ProgressPercentage >= 101 || e.ProgressPercentage < 0)
        return;
      this.LabelStatus.Text = "Status: Working...";
      this.progressBar1.Visible = true;
      this.progressBar1.Value = e.ProgressPercentage;
    }

    private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      this.progressBar1.Visible = false;
      this.LabelStatus.Text = "Status: Idle";
      this.ClearChapters(true);
      if (this.myVirtualWav.totalSeconds - this.myChapters.GetChapterDouble(this.myChapters.Count() - 1) > 10.0)
        this.myChapters.Add(this.myVirtualWav.totalSeconds);
      this.SetChapterMarkers();
      int num = (int) this.audioSoundEditor1.StopSound();
      this.LockUI();
      this.RefreshGrid();
      this.disableEvents = false;
    }

    private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
    {
      if (this.QuickSplitExternalFile(this.saveFileDialog1.FileName))
        return;
      this.disableEvents = true;
      this.SplitExternalFile(this.saveFileDialog1.FileName);
    }

    private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      if (e.ProgressPercentage >= 101 || e.ProgressPercentage < 0)
        return;
      this.LabelStatus.Text = "Status: Working...";
      this.progressBar1.Visible = true;
      this.progressBar1.Value = e.ProgressPercentage;
    }

    private void backgroundWorker3_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      if (e.ProgressPercentage >= 101 || e.ProgressPercentage < 0)
        return;
      this.LabelStatus.Text = "Status: Working...";
      this.progressBar1.Visible = true;
      this.progressBar1.Value = e.ProgressPercentage;
    }

    private void button1_Click_4(object sender, EventArgs e)
    {
    }

    private void btnSpectogram_Click(object sender, EventArgs e)
    {
      if (this.btnSpectogram.Text == "Spectrogram")
      {
        this.btnSpectogram.Text = "Waveform";
        this.descSpectrumEnhGeneral.nGraphType = enumSpectrumEnhTypes.SPECTR_ENH_SPECTRAL_VIEW;
      }
      else
      {
        this.btnSpectogram.Text = "Spectrogram";
        this.descSpectrumEnhGeneral.nGraphType = enumSpectrumEnhTypes.SPECTR_ENH_AREA_LEFT_TOP;
      }
      int num = (int) this.audioSoundEditor1.DisplaySpectrumEnh.SettingsGeneralSet(this.descSpectrumEnhGeneral);
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
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
      this.openFileDialog1.Filter = "Supported Sounds (*.mp3;*.mp2;*.wav;*.ogg;*.aiff;*.wma;*.wmv;*.asx;*.asf;*.m4a;*.mp4;*.flac;*.aac;*.ac3;*.wv;*.au;*.aif;*.w64;*.voc;*.sf;*.paf;*.pvf;*.caf;*.svx;*.ircam;*.mpc;*.spx;*.opus;*.ape;*.it;*.xm;*.s3m;*.mod;*.mtm;*.mo3;*.cda)|*.mp3;*.mp2;*.wav;*.ogg;*.aiff;*.wma;*.wmv;*.asx;*.asf;*.m4b;*.m4a;*.mp4;*.flac;*.aac;*.ac3;*.wv;*.au;*.aif;*.w64;*.voc;*.sf;*.paf;*.pvf;*.caf;*.svx;*.ircam;*.mpc;*.spx;*.opus;*.ape;*.it;*.xm;*.s3m;*.mod;*.mtm;*.mo3;*.cda|MP3 and MP2 sounds (*.mp3;*.mp2)|*.mp3;*.mp2|AAC and MP4 sounds (*.aac;*.mp4;*.m4b)|*.aac;*.mp4;*.m4b|WAV sounds (*.wav)|*.wav|OGG Vorbis sounds (*.ogg)|*.ogg|AIFF sounds (*.aiff)|*.aiff|Windows Media sounds (*.wma;*.wmv;*.asx;*.asf)|*.wma;*.wmv;*.asx;*.asf|AC3 sounds (*.ac3)|*.ac3;|ALAC sounds (*.m4a)|*.ac3;|FLAC sounds (*.flac)|*.flac;|WavPack sounds (*.wv)|*.wv;|Opus sounds (*.opus)|*.opus;|MOD music (*.it;*.xm;*.s3m;*.mod;*.mtm;*.mo3)|*.it;*.xm;*.s3m;*.mod;*.mtm;*.mo3|CD tracks (*.cda)|*.cda|All files (*.*)|*.*";
      this.openFileDialog1.Title = "Load a sound file";
      if (this.openFileDialog1.ShowDialog() == DialogResult.Cancel)
        return;
      byte[] numArray = new byte[512];
      try
      {
        using (FileStream fileStream = new FileStream(this.openFileDialog1.FileName, FileMode.Open, FileAccess.Read))
        {
          fileStream.Read(numArray, 0, numArray.Length);
          fileStream.Close();
        }
      }
      catch (UnauthorizedAccessException ex)
      {
      }
      string mimeType = MimeType.GetMimeType(numArray, this.openFileDialog1.FileName);
      Audible.diskLogger("Detected file type = " + mimeType);
      if (mimeType == "application/x-rar-compressed")
      {
        int num2 = (int) MessageBox.Show("This MP3 is really a RAR file. I will try to load it, anyway.\r\n\r\nWeird stuff may happen. You have been warned.\r\n\r\nIt is recommended that you open the file with WinRAR,\r\nextract its contents and load them in using MP3 Processing Mode.", "This is really a RAR file.");
      }
      this.LockUI();
      this.origM4B = this.openFileDialog1.FileName;
      if (Path.GetExtension(this.openFileDialog1.FileName).ToLower() == ".m4b" || Path.GetExtension(this.openFileDialog1.FileName).ToLower() == ".m4a")
      {
        this.tmpAACfile = this.myVirtualWav.advancedOptions.GetTempPath() + "\\" + Path.GetFileNameWithoutExtension(this.openFileDialog1.FileName) + ".tmp.mp4";
        this.CopyCleanMP4(this.openFileDialog1.FileName, this.tmpAACfile);
        this.openFileDialog1.FileName = this.tmpAACfile;
      }
      else
      {
        this.tmpAACfile = "";
        this.origM4B = "";
      }
      int num3 = (int) this.audioSoundEditor1.SetLoadingMode(enumLoadingModes.LOAD_MODE_NEW);
      if (this.audioSoundEditor1.LoadSound(this.openFileDialog1.FileName) != enumErrorCodes.ERR_NOERROR)
      {
        int num4 = (int) MessageBox.Show("Cannot load file " + this.openFileDialog1.FileName);
      }
      this.ClearChapters(false);
      string lower = Path.GetExtension(this.openFileDialog1.FileName).ToLower();
      Audible audible = new Audible();
      Overdrive overdrive = new Overdrive(this.openFileDialog1.FileName);
      overdrive.ffprobePath = Path.GetDirectoryName(this.ffmpegPath) + "\\ffprobe.exe";
      if (this.tmpAACfile != "")
        this.myChapters = audible.GetM4BChapters(this.origM4B);
      else if (lower == ".m4b" || lower == ".m4a" || lower == ".mp4")
        this.myChapters.SetDoubleChapters(audible.getAAXChapters(this.openFileDialog1.FileName));
      else if (lower == ".mp3" && overdrive.HasOverdriveMetadata())
      {
        Audible.diskLogger("Overdrive MP3 detected.");
        overdrive.ParseChapters();
        overdrive.RemoveSubchapters(false);
        this.myChapters = overdrive.ReturnChapters();
      }
      else if (lower == ".mp3")
      {
        AdvancedSplitting.Chapters chaptersFromMp3 = new Form1().GetChaptersFromMP3(this.openFileDialog1.FileName);
        if (chaptersFromMp3.Count() > 0)
          this.myChapters = chaptersFromMp3;
      }
      this.SetChapterMarkers();
      this.myVirtualWav.sampleRate = 22050;
      this.myVirtualWav.channels = 2;
      this.myVirtualWav.aacMode = true;
      this.myVirtualWav.M4BtoWAV(this.openFileDialog1.FileName);
      this.fileMode = true;
      this.btnApply.Text = "Split file";
      this.chkSplitNames.Visible = true;
      this.Text = "Advanced Cutter / Chapterizer - " + Path.GetFileName(this.openFileDialog1.FileName);
      string str = Path.GetDirectoryName(this.openFileDialog1.FileName) + "\\" + Path.GetFileNameWithoutExtension(this.openFileDialog1.FileName) + ".cue";
      if (!System.IO.File.Exists(str))
        return;
      this.LoadCueFile(str);
    }

    private void zoomToselectionToolStripMenuItem_Click(object sender, EventArgs e)
    {
      int selection = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.ZoomToSelection(true);
    }

    private void zoomToFullwaveformToolStripMenuItem_Click(object sender, EventArgs e)
    {
      int fullSound = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.ZoomToFullSound();
    }

    private void zoominToolStripMenuItem_Click(object sender, EventArgs e)
    {
      int num = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.ZoomIn();
    }

    private void zoomoutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      int num = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.ZoomOut();
    }

    private void ZoomControl(int val1, int val2)
    {
      if (val1 > val2)
      {
        int num1 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.ZoomOut();
      }
      else
      {
        if (val1 >= val2)
          return;
        int num2 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.ZoomIn();
      }
    }

    private void trackBar1_ValueChanged(object sender, EventArgs e)
    {
    }

    private void nextChapterToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.btnNextKeep.PerformClick();
    }

    private void deleteChapterToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.btnNextDel.PerformClick();
    }

    private void previousChapterToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.MoveAndMarkChapter(-1, true);
      this.PreviewChapter(-1);
    }

    private void deletePreviousChapterToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.MoveAndMarkChapter(-1, false);
      this.PreviewChapter(-1);
    }

    private void loadCUEFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.LoadCUEFileDialog();
    }

    private void LoadCUEFileDialog()
    {
      if (this.InvokeRequired)
      {
        this.Invoke((Action) (() => this.LoadCUEFileDialog()));
      }
      else
      {
        this.openFileDialog1.Filter = "CUE files (*.cue)|*.cue|All files (*.*)|*.*";
        this.openFileDialog1.Title = "Load a CUE file";
        if (this.openFileDialog1.ShowDialog() == DialogResult.Cancel)
          return;
        if (MessageBox.Show("Would you like to use the chapter titles in this file?", "Use CUE titles", MessageBoxButtons.YesNo) == DialogResult.No)
          this.myChapters.doNotUseDescriptions = true;
        this.LoadCueFile(this.openFileDialog1.FileName);
      }
    }

    private void LoadCueFile(string cueFile)
    {
      this.disableEvents = true;
      this.ClearChapters(false);
      this.myChapters = this.ParseCueFile(cueFile);
      this.SetChapterMarkers();
      int num = (int) this.audioSoundEditor1.StopSound();
      int fullSound = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.ZoomToFullSound();
      this.RefreshGrid();
      this.disableEvents = false;
    }

    private AdvancedSplitting.Chapters ParseCueFile(string cueFile)
    {
      AdvancedSplitting.Chapters chapters1 = new AdvancedSplitting.Chapters();
      List<double> chapters2 = new List<double>();
      List<string> desc = new List<string>();
      string[] strArray1 = System.IO.File.ReadAllText(cueFile).Split('\n');
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
            desc.Add(strArray2[1]);
          }
        }
        if (this.myChapters.doNotUseDescriptions)
        {
          chapters1.SetDoubleChapters(chapters2);
          chapters1.generatedDescriptions = true;
        }
        else
        {
          chapters1.SetChaptersAndDescriptions(chapters2, desc);
          chapters1.generatedDescriptions = false;
        }
      }
      catch
      {
      }
      return chapters1;
    }

    private void saveCUEFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.SaveCUEFileDialog();
    }

    private void SaveCUEFileDialog()
    {
      if (this.InvokeRequired)
      {
        this.Invoke((System.Action) (() => this.SaveCUEFileDialog()));
      }
      else
      {
        string str = Path.GetExtension(this.openFileDialog1.FileName).TrimStart('.');
        this.saveFileDialog1 = new SaveFileDialog();
        this.saveFileDialog1.DefaultExt = str;
        this.saveFileDialog1.Filter = "CUE files (*.cue)|*.cue|All files (*.*)|*.*";
        this.saveFileDialog1.AddExtension = true;
        if (this.openFileDialog1.FileName != null && this.openFileDialog1.FileName != "")
          this.saveFileDialog1.FileName = Path.GetDirectoryName(this.openFileDialog1.FileName) + "\\" + Path.GetFileNameWithoutExtension(this.openFileDialog1.FileName) + ".cue";
        if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
        {
          List<double> chapters = new List<double>();
          List<string> names = new List<string>();
          for (int pos = 0; pos < this.myChapters.Count(); ++pos)
          {
            if (bool.Parse(this.dgvChapters.Rows[pos].Cells[0].Value.ToString()))
            {
              chapters.Add(this.myChapters.GetChapterDouble(pos));
              if (!this.myChapters.generatedDescriptions)
                names.Add(this.dgvChapters.Rows[pos].Cells[1].Value.ToString().Replace('"', '\''));
            }
          }
          System.IO.File.WriteAllText(this.saveFileDialog1.FileName, this.GetCUEfromChapters(chapters, names, Path.GetFileName(this.openFileDialog1.FileName), str.ToUpper()));
        }
        this.saveFileDialog1.Dispose();
      }
    }

    public string GetCUEfromChapters(List<double> chapters, List<string> names, string fileName, string cueType)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("FILE \"" + fileName + "\" " + cueType + "\n");
      int num1 = 1;
      double num2 = 0.0;
      foreach (double chapter in chapters)
      {
        TimeSpan timeSpan = TimeSpan.FromSeconds(chapter - num2);
        int seconds = timeSpan.Seconds;
        int milliseconds = timeSpan.Milliseconds;
        string str = Math.Floor(timeSpan.TotalMinutes).ToString() + ":" + (object) seconds + ":" + (milliseconds / 10).ToString("D2");
        stringBuilder.Append("TRACK " + (object) num1 + " AUDIO\n");
        if (names.Count > 0)
          stringBuilder.Append("  TITLE \"" + names[num1 - 1] + "\"\n");
        else
          stringBuilder.Append("  TITLE \"Chapter " + num1.ToString("D2") + "\"\n");
        stringBuilder.Append("  INDEX 01 " + str + "\n");
        ++num1;
      }
      return stringBuilder.ToString();
    }

    private void insertNewChapterToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.SetChaptersEdited(true);
      bool bSelectionAvailable = false;
      int nBeginPosInMs = 0;
      int nEndPosInMs = 0;
      int selection = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GetSelection(ref bSelectionAvailable, ref nBeginPosInMs, ref nEndPosInMs);
      double thisChapter = 0.0;
      if (bSelectionAvailable)
      {
        List<double> newChaps = new List<double>();
        thisChapter = (double) nBeginPosInMs / 1000.0;
        newChaps.Add(thisChapter);
        if (newChaps.Count > 0)
          this.myChapters = this.MergeChapters2(newChaps, this.myChapters);
      }
      this.disableEvents = true;
      this.ClearChapters(true);
      if (this.myVirtualWav.totalSeconds - this.myChapters.GetChapterDouble(this.myChapters.Count() - 1) > 10.0)
        this.myChapters.Add(this.myVirtualWav.totalSeconds, "(End)");
      this.SetChapterMarkers();
      this.RefreshGrid();
      int index = this.SelectChapterByTime(thisChapter);
      this.dgvChapters.Rows[0].Selected = false;
      this.dgvChapters.Rows[index].Selected = true;
      this.disableEvents = false;
    }

    private int SelectChapterByTime(double thisChapter)
    {
      int num = 0;
      for (int pos = 0; pos < this.myChapters.Count(); ++pos)
      {
        if (this.myChapters.GetChapterDouble(pos) == thisChapter)
          num = pos;
        if (pos + 1 < this.myChapters.Count() && this.myChapters.GetChapterDouble(pos) > thisChapter && this.myChapters.GetChapterDouble(pos + 1) < thisChapter)
          num = thisChapter - this.myChapters.GetChapterDouble(pos) >= this.myChapters.GetChapterDouble(pos + 1) - thisChapter ? pos + 1 : pos;
      }
      return num;
    }

    private void audioSoundEditor1_SilencePosDetectionDone(object sender, SilencePosDetectionDoneEventArgs e)
    {
      int index = -1;
      if (this.dgvChapters.SelectedRows.Count > 1)
        index = this.dgvChapters.SelectedRows[0].Index;
      if (!this.alignChapters)
      {
        this.NewSilenceSearch();
        this.ClearChapters(true);
        int soundDuration = this.audioSoundEditor1.GetSoundDuration();
        this.myChapters.totalTime = (double) soundDuration / 1000.0;
        this.myChapters.SetEndChapter();
        if (this.myChapters.Count() > 0 && this.myChapters.GetLastChapter() * 1000.0 > (double) soundDuration)
        {
          int pos = this.myChapters.Count() - 1;
          this.myChapters.SetChapter(pos, (double) soundDuration / 1000.0);
          this.dgvChapters.Rows[pos].Cells[3].Value = (object) ((double) soundDuration / 1000.0);
          int num = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GraphicItemHorzPositionSet(short.Parse(this.dgvChapters.Rows[pos].Cells[2].Value.ToString()), soundDuration - 100, 0);
        }
      }
      else
      {
        this.ClearChapters(true);
        this.AlignChaptersBySilence();
      }
      this.SetChapterMarkers();
      this.LabelStatus.Text = "Status: Idle";
      this.LabelStatus.Refresh();
      this.progressBar1.Visible = false;
      this.LockUI();
      this.RefreshGrid();
      if (index > -1)
      {
        this.dgvChapters.ClearSelection();
        this.dgvChapters.FirstDisplayedScrollingRowIndex = index;
        this.dgvChapters.Rows[index].Selected = true;
        this.dgvChapters.CurrentCell = this.dgvChapters.Rows[index].Cells[0];
      }
      this.disableEvents = false;
      int num1 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SetSelection(false, 0, 0);
      this.grpSilence.Text = "Silence detection";
      this.grpSilence.ForeColor = SystemColors.ControlText;
    }

    private void AlignChaptersBySilence()
    {
      int searchSize = 30;
      for (int pos = 1; pos < this.myChapters.Count(); ++pos)
      {
        double silenceByChapter = this.GetLargestSilenceByChapter(this.myChapters.GetChapterDouble(pos), searchSize);
        this.myChapters.SetChapter(pos, silenceByChapter);
      }
    }

    private double GetLargestSilenceByChapter(double chapter, int searchSize)
    {
      int nRangeStart = 0;
      int nRangeEnd = 0;
      double num1 = chapter;
      int num2 = (int) ((chapter - (double) searchSize) * 1000.0);
      int num3 = (int) ((chapter + (double) searchSize) * 1000.0);
      int nPortionIndex1 = 0;
      int num4 = 0;
      for (int nPortionIndex2 = 0; nPortionIndex2 < this.audioSoundEditor1.SilencePositionsGetNum(); ++nPortionIndex2)
      {
        int range = (int) this.audioSoundEditor1.SilencePositionsGetRange(nPortionIndex2, ref nRangeStart, ref nRangeEnd);
        if (nRangeStart > num2 && nRangeStart < num3)
        {
          if (nRangeEnd - nRangeStart > num4)
          {
            num4 = nRangeEnd - nRangeStart;
            nPortionIndex1 = nPortionIndex2;
          }
        }
        else if (nRangeStart > num3)
          break;
      }
      int range1 = (int) this.audioSoundEditor1.SilencePositionsGetRange(nPortionIndex1, ref nRangeStart, ref nRangeEnd);
      if (this.middleToolStripMenuItem.Checked)
        num1 = ((double) nRangeStart + (double) (nRangeEnd - nRangeStart) / 2.0) / 1000.0;
      else if (this.startToolStripMenuItem.Checked)
        num1 = ((double) nRangeStart + 0.1) / 1000.0;
      else if (this.endToolStripMenuItem.Checked)
        num1 = ((double) nRangeEnd - 0.1) / 1000.0;
      return num1;
    }

    private void RefreshGrid()
    {
      this.dgvChapters.PerformLayout();
    }

    private void audioSoundEditor1_SilencePosDetectionDoneFixedDuration(object sender, SilencePosDetectionDoneEventArgs e)
    {
      this.NewFixedSplit();
      this.ClearChapters(true);
      if (this.myVirtualWav.totalSeconds - this.myChapters.GetChapterDouble(this.myChapters.GetDoubleList().Count - 1) > 10.0)
        this.myChapters.Add(this.myVirtualWav.totalSeconds);
      this.SetChapterMarkers();
      this.LockUI();
      this.RefreshGrid();
      this.disableEvents = false;
      this.audioSoundEditor1.SilencePosDetectionDone -= new AudioSoundEditor.AudioSoundEditor.SilencePosDetectionDoneEventHandler(this.audioSoundEditor1_SilencePosDetectionDoneFixedDuration);
      this.audioSoundEditor1.SilencePosDetectionDone += new AudioSoundEditor.AudioSoundEditor.SilencePosDetectionDoneEventHandler(this.audioSoundEditor1_SilencePosDetectionDone);
    }

    private void audioSoundEditor1_SilencePosDetectionPerc(object sender, SilencePosDetectionPercEventArgs e)
    {
      if (this.progressBar1.Value == (int) e.nPercentage)
        return;
      this.progressBar1.Value = (int) e.nPercentage;
      this.LabelStatus.Text = "Status: Searching... " + e.nPercentage.ToString() + "%";
      this.progressBar1.Refresh();
      this.LabelStatus.Refresh();
    }

    private void discreteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      WANALYZER_WAVEFORM_SETTINGS settings = new WANALYZER_WAVEFORM_SETTINGS();
      int num1 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsWaveGet(ref settings);
      settings.nStereoVisualizationMode = enumWaveformStereoModes.STEREO_MODE_CHANNELS_BOTH;
      int num2 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsWaveSet(settings);
    }

    private void combinedToolStripMenuItem_Click(object sender, EventArgs e)
    {
      WANALYZER_WAVEFORM_SETTINGS settings = new WANALYZER_WAVEFORM_SETTINGS();
      int num1 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsWaveGet(ref settings);
      settings.nStereoVisualizationMode = enumWaveformStereoModes.STEREO_MODE_CHANNELS_MIXED;
      int num2 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsWaveSet(settings);
    }

    private void btnZoomIn_Click_1(object sender, EventArgs e)
    {
      int num = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.ZoomIn();
    }

    private void btnZoomOut_Click(object sender, EventArgs e)
    {
      int num = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.ZoomOut();
    }

    private void btnZoomFull_Click(object sender, EventArgs e)
    {
      int fullSound = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.ZoomToFullSound();
    }

    private void clearChapterListToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.disableEvents = true;
      this.ClearChapters(false);
      this.myChapters.Add(0.0);
      this.myChapters.Add((double) this.audioSoundEditor1.GetSoundDuration() / 1000.0 - 1.0);
      this.SetChapterMarkers();
      this.disableEvents = false;
    }

    private void SetSplitMarker(short id, bool set)
    {
      WANALYZER_VERTICAL_LINE settings = new WANALYZER_VERTICAL_LINE();
      int num1 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GraphicItemVerticalLineParamsGet(id, ref settings);
      settings.color = Color.White;
      settings.nDashStyle = enumWaveformLineDashStyles.LINE_DASH_STYLE_DASH;
      if (!set)
      {
        settings.color = Color.Red;
        settings.nDashStyle = enumWaveformLineDashStyles.LINE_DASH_STYLE_SOLID;
      }
      settings.nWidth = (short) 2;
      settings.nTranspFactor = (short) 0;
      settings.nHighCap = enumLineCaps.LINE_CAP_SQUARE;
      settings.nLowCap = enumLineCaps.LINE_CAP_SQUARE;
      settings.nDashCap = enumLineDashCaps.LINE_DASH_CAP_FLAT;
      int num2 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GraphicItemVerticalLineParamsSet(id, settings);
    }

    private void audioSoundEditor1_WaveAnalyzerGraphicItemClick(object sender, WaveAnalyzerGraphItemClickEventArgs e)
    {
      Console.WriteLine("WaveAnalyzerGraphicItemClick: id: " + (object) e.nUniqueID + " btn: " + (object) e.nButton + " type: " + (object) e.nGraphicItemType);
      if (e.nButton != enumMouseButtons.MOUSE_BTN_RIGHT)
        return;
      this.m_nLastUniqueId = e.nUniqueID;
      this.m_mnuGraphicItemsContextMenu.Show((Control) this, new Point(e.xPos, e.yPos));
      short nUniqueId = e.nUniqueID;
      for (int index = 0; index < this.dgvChapters.RowCount; ++index)
      {
        if (this.dgvChapters.Rows[index].Cells[2].Value.ToString() == nUniqueId.ToString())
        {
          this.dgvChapters.Rows[index].Cells[5].Value = this.dgvChapters.Rows[index].Cells[5].Value == null || !bool.Parse(this.dgvChapters.Rows[index].Cells[5].Value.ToString()) ? (object) true : (object) false;
          this.SetSplitMarker(e.nUniqueID, bool.Parse(this.dgvChapters.Rows[index].Cells[5].Value.ToString()));
        }
      }
    }

    private void chapterNamesNumbersInFilenameToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.chapterNamesNumbersInFilenameToolStripMenuItem.Checked = !this.chapterNamesNumbersInFilenameToolStripMenuItem.Checked;
      this.myChapters.includeFileNumbers = this.chapterNamesNumbersInFilenameToolStripMenuItem.Checked;
    }

    private void showChapterDebugToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.dgvChapters.Columns[2].Visible = !this.dgvChapters.Columns[2].Visible;
      this.dgvChapters.Columns[3].Visible = !this.dgvChapters.Columns[3].Visible;
      this.dgvChapters.Columns[4].Visible = !this.dgvChapters.Columns[4].Visible;
    }

    public static string SecondsToFormattedTime(double seconds)
    {
      int num1 = (int) (seconds / 60.0 / 60.0);
      int num2 = (int) (seconds / 60.0) - num1 * 60;
      double num3 = seconds - (double) (num2 * 60) - (double) (num1 * 60 * 60);
      return num1.ToString("00") + ":" + num2.ToString("00") + ":" + (object) Math.Round(num3, 3);
    }

    private void dgvChapters_MouseClick(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Right)
        return;
      ContextMenu contextMenu = new ContextMenu();
      int currentMouseOverRow = this.dgvChapters.HitTest(e.X, e.Y).RowIndex;
      if (currentMouseOverRow >= 0)
      {
        double result1 = 0.0;
        double result2 = 0.0;
        double.TryParse(this.dgvChapters.Rows[currentMouseOverRow].Cells[3].Value.ToString(), out result1);
        if (currentMouseOverRow < this.dgvChapters.Rows.Count - 1)
          double.TryParse(this.dgvChapters.Rows[currentMouseOverRow + 1].Cells[3].Value.ToString(), out result2);
        double seconds = result2 - result1;
        string str1 = AdvancedSplitting.SecondsToFormattedTime(seconds);
        if (seconds < 0.0)
          str1 = "0";
        MenuItem menuItem1 = new MenuItem(AdvancedSplitting.SecondsToFormattedTime(result1) + " (" + str1 + ")");
        contextMenu.MenuItems.Add(menuItem1);
        contextMenu.MenuItems.Add("-");
        MenuItem menuItem2 = new MenuItem(string.Format("Set \"001\" at row {0}", (object) currentMouseOverRow.ToString()));
        menuItem2.Click += (System.EventHandler) ((s, eventArgs) => this.renumberChapters(currentMouseOverRow, "", true, 0));
        contextMenu.MenuItems.Add(menuItem2);
        MenuItem menuItem3 = new MenuItem(string.Format("Set \"Chapter 001\" at row {0}", (object) currentMouseOverRow.ToString()));
        menuItem3.Click += (System.EventHandler) ((s, eventArgs) => this.renumberChapters(currentMouseOverRow, "Chapter ", true, 0));
        contextMenu.MenuItems.Add(menuItem3);
        int newChapNum = 0;
        if (this.dgvChapters.Rows[currentMouseOverRow].Cells[1].Value.ToString() == "new")
        {
          contextMenu.MenuItems.Add("-");
          MenuItem menuItem4 = new MenuItem("Delete all \"new\" but this one");
          menuItem4.Click += (System.EventHandler) ((s, eventArgs) => this.PruneExtraNewItems(currentMouseOverRow));
          contextMenu.MenuItems.Add(menuItem4);
        }
        contextMenu.MenuItems.Add("-");
        if (currentMouseOverRow > 0)
        {
          string[] strArray = this.dgvChapters.Rows[currentMouseOverRow - 1].Cells[1].Value.ToString().Split(' ');
          if (strArray.Length > 1)
            int.TryParse(strArray[1], out newChapNum);
          else
            int.TryParse(strArray[0], out newChapNum);
          ++newChapNum;
        }
        if (newChapNum > 1)
        {
          string str2 = newChapNum.ToString("D3");
          MenuItem menuItem4 = new MenuItem(string.Format("Set \"{1}\" at row {0}", (object) currentMouseOverRow.ToString(), (object) str2));
          menuItem4.Click += (System.EventHandler) ((s, eventArgs) => this.renumberChapters(currentMouseOverRow, "", false, newChapNum));
          contextMenu.MenuItems.Add(menuItem4);
          MenuItem menuItem5 = new MenuItem(string.Format("Set \"Chapter {1}\" at row {0}", (object) currentMouseOverRow.ToString(), (object) str2));
          menuItem5.Click += (System.EventHandler) ((s, eventArgs) => this.renumberChapters(currentMouseOverRow, "Chapter ", false, newChapNum));
          contextMenu.MenuItems.Add(menuItem5);
        }
        if (this.dgvChapters.SelectedRows.Count > 1)
        {
          contextMenu.MenuItems.Add("-");
          MenuItem menuItem4 = new MenuItem("Hard delete selected rows");
          menuItem4.Click += (System.EventHandler) ((s, eventArgs) => this.DeleteChapterRows());
          contextMenu.MenuItems.Add(menuItem4);
        }
      }
      contextMenu.Show((Control) this.dgvChapters, new Point(e.X, e.Y));
    }

    private void renumberChapters(int row, string prefix, bool fromZero, int newChapNum = 0)
    {
      this.LockUI();
      for (int pos = row; pos < this.dgvChapters.RowCount; ++pos)
      {
        int num = !fromZero ? pos - row + newChapNum : pos - row + 1;
        this.dgvChapters.Rows[pos].Cells[1].Value = (object) (prefix + num.ToString("D3"));
        this.myChapters.SetDescription(pos, this.dgvChapters.Rows[pos].Cells[1].Value.ToString());
      }
      this.LockUI();
    }

    private void pruneDisabledChaptersToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.LockUI();
      this.disableEvents = true;
      for (int row = this.dgvChapters.RowCount - 1; row >= 0; --row)
      {
        if (!bool.Parse(this.dgvChapters.Rows[row].Cells[0].Value.ToString()))
          this.DeleteChapter(row);
      }
      this.LockUI();
      this.RefreshGrid();
      this.disableEvents = false;
    }

    private void PruneExtraNewItems(int row)
    {
      this.LockUI();
      this.disableEvents = true;
      for (int row1 = this.dgvChapters.RowCount - 1; row1 >= 0; --row1)
      {
        if (this.dgvChapters.Rows[row1].Cells[1].Value.ToString() == "new" && row1 != row)
          this.DeleteChapter(row1);
      }
      this.LockUI();
      this.RefreshGrid();
      int index1 = 0;
      for (int index2 = 0; index2 < this.dgvChapters.RowCount; ++index2)
      {
        if (this.dgvChapters.Rows[index2].Cells[1].Value.ToString() == "new")
          index1 = index2;
      }
      this.dgvChapters.ClearSelection();
      this.dgvChapters.FirstDisplayedScrollingRowIndex = index1;
      this.dgvChapters.Rows[index1].Selected = true;
      this.dgvChapters.CurrentCell = this.dgvChapters.Rows[index1].Cells[0];
      this.disableEvents = false;
    }

    private void dgvChapters_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
    {
      using (SolidBrush solidBrush = new SolidBrush(this.dgvChapters.RowHeadersDefaultCellStyle.ForeColor))
        e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, (Brush) solidBrush, (float) (e.RowBounds.Location.X + 10), (float) (e.RowBounds.Location.Y + 4));
    }

    private void middleToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.middleToolStripMenuItem.Checked)
      {
        this.startToolStripMenuItem.Checked = false;
        this.endToolStripMenuItem.Checked = false;
      }
      if (this.middleToolStripMenuItem.Checked || this.startToolStripMenuItem.Checked || this.endToolStripMenuItem.Checked)
        return;
      this.middleToolStripMenuItem.Checked = true;
    }

    private void startToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.startToolStripMenuItem.Checked)
      {
        this.middleToolStripMenuItem.Checked = false;
        this.endToolStripMenuItem.Checked = false;
      }
      if (this.middleToolStripMenuItem.Checked || this.startToolStripMenuItem.Checked || this.endToolStripMenuItem.Checked)
        return;
      this.startToolStripMenuItem.Checked = true;
    }

    private void endToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.endToolStripMenuItem.Checked)
      {
        this.middleToolStripMenuItem.Checked = false;
        this.startToolStripMenuItem.Checked = false;
      }
      if (this.middleToolStripMenuItem.Checked || this.startToolStripMenuItem.Checked || this.endToolStripMenuItem.Checked)
        return;
      this.endToolStripMenuItem.Checked = true;
    }

    private void hardDeleteChapterToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.DeleteChapterRows();
    }

    private void DeleteChapterRows()
    {
      this.LockUI();
      this.disableEvents = true;
      for (int index = this.dgvChapters.SelectedRows.Count - 1; index >= 0; --index)
        this.DeleteChapter(this.dgvChapters.SelectedRows[index].Index);
      this.LockUI();
      this.RefreshGrid();
      this.disableEvents = false;
    }

    private void btnDeselect_Click(object sender, EventArgs e)
    {
      double seconds = this.FormattedToSeconds(this.LabelSelectionBegin.Text);
      this.FormattedToSeconds(this.LabelSelectionEnd.Text);
      int num = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SetSelection(true, (int) (seconds * 1000.0), (int) (seconds * 1000.0));
    }

    private void btnRight_Click(object sender, EventArgs e)
    {
      int num = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.Scroll(100);
    }

    private void btnLeft_Click(object sender, EventArgs e)
    {
      int num = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.Scroll(-100);
    }

    private void scrollBarEx1_Scroll(object sender, ScrollEventArgs e)
    {
      if (!this.scrolling)
        return;
      int nWidthInMs = 0;
      int nWidthInPixels = 0;
      int displayWidth = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GetDisplayWidth(ref nWidthInMs, ref nWidthInPixels);
      int soundDuration = this.audioSoundEditor1.GetSoundDuration();
      int nBeginPosInMs1 = 0;
      int nEndPosInMs1 = 0;
      int displayRange = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GetDisplayRange(ref nBeginPosInMs1, ref nEndPosInMs1);
      int num1 = nEndPosInMs1 - nBeginPosInMs1;
      int nBeginPosInMs2 = (int) ((double) this.scrollBarEx1.Value / 100.0 * (double) soundDuration);
      int nEndPosInMs2 = nBeginPosInMs2 + num1;
      if (nEndPosInMs2 > soundDuration)
        nEndPosInMs2 = soundDuration;
      if (nBeginPosInMs2 + num1 >= soundDuration)
        nBeginPosInMs2 = soundDuration - num1;
      int num2 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SetSelection(true, nBeginPosInMs2, nEndPosInMs2);
      int selection = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.ZoomToSelection(true);
    }

    private void scrollBarEx1_MouseDown(object sender, MouseEventArgs e)
    {
      this.scrolling = true;
    }

    private void scrollBarEx1_MouseUp(object sender, MouseEventArgs e)
    {
      this.scrolling = false;
    }

    private void btnZoomToSelected_Click(object sender, EventArgs e)
    {
      int selection = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.ZoomToSelection(true);
    }

    private void ffmpegToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.ffmpegToolStripMenuItem.Checked = true;
      this.mP3SPLTToolStripMenuItem.Checked = false;
      this.myVirtualWav.advancedOptions.SplitMode = AdvancedOptions.SplitTypes.ffmpeg;
    }

    private void mP3SPLTToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.mP3SPLTToolStripMenuItem.Checked = true;
      this.ffmpegToolStripMenuItem.Checked = false;
      this.myVirtualWav.advancedOptions.SplitMode = AdvancedOptions.SplitTypes.MP3SPLT;
    }

    private void dgvChapters_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      this.PreviewChapter(-1);
    }

    private void btnAlignChapters_Click(object sender, EventArgs e)
    {
      this.alignChapters = true;
      this.LockUI();
      this.disableEvents = true;
      this.progressBar1.Visible = true;
      int num = (int) this.audioSoundEditor1.SilencePositionsDetect((short) 800, 500, false);
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AdvancedSplitting));
      this.labelSpectrum = new Label();
      this.Picture1 = new PictureBox();
      this.progressBar1 = new ProgressBar();
      this.Frame5 = new GroupBox();
      this.buttonPause = new Button();
      this.buttonPlay = new Button();
      this.buttonStop = new Button();
      this.buttonPlaySelection = new Button();
      this.LabelStatus = new Label();
      this.TimerMenuEnabler = new System.Windows.Forms.Timer(this.components);
      this.timerDisplayWaveform = new System.Windows.Forms.Timer(this.components);
      this.TimerReload = new System.Windows.Forms.Timer(this.components);
      this.timerPlaybackPos = new System.Windows.Forms.Timer(this.components);
      this.Label2 = new Label();
      this.Label3 = new Label();
      this.Label4 = new Label();
      this.Label5 = new Label();
      this.Label6 = new Label();
      this.LabelSelectionEnd = new Label();
      this.Frame4 = new GroupBox();
      this.btnDeselect = new Button();
      this.LabelSelectionBegin = new Label();
      this.LabelSelectionDuration = new Label();
      this.LabelRangeBegin = new Label();
      this.LabelRangeEnd = new Label();
      this.LabelRangeDuration = new Label();
      this.LabelTotalDuration = new Label();
      this.Label8 = new Label();
      this.audioSoundEditor1 = new AudioSoundEditor.AudioSoundEditor();
      this.dgvChapters = new DataGridView();
      this.enabled = new DataGridViewCheckBoxColumn();
      this.chapterName = new DataGridViewTextBoxColumn();
      this.pos = new DataGridViewTextBoxColumn();
      this.time = new DataGridViewTextBoxColumn();
      this.special = new DataGridViewCheckBoxColumn();
      this.split = new DataGridViewCheckBoxColumn();
      this.btnApply = new Button();
      this.btnCancel = new Button();
      this.btnNextKeep = new Button();
      this.btnNextDel = new Button();
      this.grpSilence = new GroupBox();
      this.label1 = new Label();
      this.btnFixedSplit = new Button();
      this.txtFixedSplit = new System.Windows.Forms.TextBox();
      this.btnFindSilence = new Button();
      this.lblSilenceThresh = new Label();
      this.txtSilenceThresh = new System.Windows.Forms.TextBox();
      this.openFileDialog1 = new OpenFileDialog();
      this.saveFileDialog1 = new SaveFileDialog();
      this.backgroundWorker1 = new BackgroundWorker();
      this.backgroundWorker2 = new BackgroundWorker();
      this.backgroundWorker3 = new BackgroundWorker();
      this.btnSpectogram = new Button();
      this.menuStrip1 = new MenuStrip();
      this.fileToolStripMenuItem = new ToolStripMenuItem();
      this.loadFileToolStripMenuItem = new ToolStripMenuItem();
      this.loadCUEFileToolStripMenuItem = new ToolStripMenuItem();
      this.saveCUEFileToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator1 = new ToolStripSeparator();
      this.exitToolStripMenuItem = new ToolStripMenuItem();
      this.settingsToolStripMenuItem = new ToolStripMenuItem();
      this.channelViewToolStripMenuItem = new ToolStripMenuItem();
      this.combinedToolStripMenuItem = new ToolStripMenuItem();
      this.discreteToolStripMenuItem = new ToolStripMenuItem();
      this.splitEngineToolStripMenuItem = new ToolStripMenuItem();
      this.ffmpegToolStripMenuItem = new ToolStripMenuItem();
      this.mP3SPLTToolStripMenuItem = new ToolStripMenuItem();
      this.zoomToolStripMenuItem = new ToolStripMenuItem();
      this.zoomToselectionToolStripMenuItem = new ToolStripMenuItem();
      this.zoomToFullwaveformToolStripMenuItem = new ToolStripMenuItem();
      this.zoominToolStripMenuItem = new ToolStripMenuItem();
      this.zoomoutToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator4 = new ToolStripSeparator();
      this.mouseWheelZoomsToPointerToolStripMenuItem = new ToolStripMenuItem();
      this.chaptersToolStripMenuItem = new ToolStripMenuItem();
      this.nextChapterToolStripMenuItem = new ToolStripMenuItem();
      this.previousChapterToolStripMenuItem = new ToolStripMenuItem();
      this.deleteChapterToolStripMenuItem = new ToolStripMenuItem();
      this.deletePreviousChapterToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator2 = new ToolStripSeparator();
      this.insertNewChapterToolStripMenuItem = new ToolStripMenuItem();
      this.clearChapterListToolStripMenuItem = new ToolStripMenuItem();
      this.hardDeleteChapterToolStripMenuItem = new ToolStripMenuItem();
      this.pruneDisabledChaptersToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator3 = new ToolStripSeparator();
      this.chapterNamesNumbersInFilenameToolStripMenuItem = new ToolStripMenuItem();
      this.showChapterDebugToolStripMenuItem = new ToolStripMenuItem();
      this.detectedSilenceAlignmentToolStripMenuItem = new ToolStripMenuItem();
      this.middleToolStripMenuItem = new ToolStripMenuItem();
      this.startToolStripMenuItem = new ToolStripMenuItem();
      this.endToolStripMenuItem = new ToolStripMenuItem();
      this.label18 = new Label();
      this.label17 = new Label();
      this.btnZoomIn = new Button();
      this.btnZoomOut = new Button();
      this.btnZoomFull = new Button();
      this.btnLeft = new Button();
      this.btnRight = new Button();
      this.scrollBarEx1 = new ScrollBarEx();
      this.btnZoomToSelected = new Button();
      this.chkSplitNames = new CheckBox();
      this.btnAlignChapters = new Button();
      ((ISupportInitialize) this.Picture1).BeginInit();
      this.Frame5.SuspendLayout();
      this.Frame4.SuspendLayout();
      ((ISupportInitialize) this.dgvChapters).BeginInit();
      this.grpSilence.SuspendLayout();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      this.labelSpectrum.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.labelSpectrum.BackColor = Color.Black;
      this.labelSpectrum.Location = new Point(608, 299);
      this.labelSpectrum.Name = "labelSpectrum";
      this.labelSpectrum.Size = new Size(283, 288);
      this.labelSpectrum.TabIndex = 156;
      this.Picture1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.Picture1.BackColor = Color.Black;
      this.Picture1.Cursor = Cursors.Default;
      this.Picture1.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.Picture1.ForeColor = SystemColors.WindowText;
      this.Picture1.Location = new Point(12, 27);
      this.Picture1.Name = "Picture1";
      this.Picture1.RightToLeft = RightToLeft.No;
      this.Picture1.Size = new Size(828, 240);
      this.Picture1.TabIndex = 152;
      this.Picture1.TabStop = false;
      this.Picture1.Visible = false;
      this.progressBar1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.progressBar1.Location = new Point(10, 606);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new Size(204, 16);
      this.progressBar1.TabIndex = 160;
      this.progressBar1.Visible = false;
      this.Frame5.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.Frame5.BackColor = SystemColors.Control;
      this.Frame5.Controls.Add((Control) this.buttonPause);
      this.Frame5.Controls.Add((Control) this.buttonPlay);
      this.Frame5.Controls.Add((Control) this.buttonStop);
      this.Frame5.Controls.Add((Control) this.buttonPlaySelection);
      this.Frame5.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.Frame5.ForeColor = SystemColors.ControlText;
      this.Frame5.Location = new Point(265, 425);
      this.Frame5.Name = "Frame5";
      this.Frame5.RightToLeft = RightToLeft.No;
      this.Frame5.Size = new Size(340, 56);
      this.Frame5.TabIndex = 158;
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
      this.buttonPlaySelection.Text = "Play selected";
      this.buttonPlaySelection.UseVisualStyleBackColor = false;
      this.buttonPlaySelection.Click += new System.EventHandler(this.buttonPlaySelection_Click);
      this.LabelStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.LabelStatus.BackColor = SystemColors.Control;
      this.LabelStatus.Cursor = Cursors.Default;
      this.LabelStatus.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.LabelStatus.ForeColor = SystemColors.ControlText;
      this.LabelStatus.Location = new Point(12, 590);
      this.LabelStatus.Name = "LabelStatus";
      this.LabelStatus.RightToLeft = RightToLeft.No;
      this.LabelStatus.Size = new Size(201, 13);
      this.LabelStatus.TabIndex = 159;
      this.LabelStatus.Text = "Status: Idle";
      this.LabelStatus.TextAlign = ContentAlignment.TopCenter;
      this.TimerMenuEnabler.Tick += new System.EventHandler(this.TimerMenuEnabler_Tick);
      this.Label2.BackColor = SystemColors.Control;
      this.Label2.Cursor = Cursors.Default;
      this.Label2.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.Label2.ForeColor = SystemColors.ControlText;
      this.Label2.Location = new Point(8, 63);
      this.Label2.Name = "Label2";
      this.Label2.RightToLeft = RightToLeft.No;
      this.Label2.Size = new Size(60, 17);
      this.Label2.TabIndex = 25;
      this.Label2.Text = "Selection";
      this.Label3.BackColor = SystemColors.Control;
      this.Label3.Cursor = Cursors.Default;
      this.Label3.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.Label3.ForeColor = SystemColors.ControlText;
      this.Label3.Location = new Point(8, 83);
      this.Label3.Name = "Label3";
      this.Label3.RightToLeft = RightToLeft.No;
      this.Label3.Size = new Size(47, 17);
      this.Label3.TabIndex = 24;
      this.Label3.Text = "Visible range";
      this.Label4.BackColor = SystemColors.Control;
      this.Label4.Cursor = Cursors.Default;
      this.Label4.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.Label4.ForeColor = SystemColors.ControlText;
      this.Label4.Location = new Point(74, 43);
      this.Label4.Name = "Label4";
      this.Label4.RightToLeft = RightToLeft.No;
      this.Label4.Size = new Size(41, 17);
      this.Label4.TabIndex = 23;
      this.Label4.Text = "Begin";
      this.Label5.BackColor = SystemColors.Control;
      this.Label5.Cursor = Cursors.Default;
      this.Label5.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.Label5.ForeColor = SystemColors.ControlText;
      this.Label5.Location = new Point(146, 43);
      this.Label5.Name = "Label5";
      this.Label5.RightToLeft = RightToLeft.No;
      this.Label5.Size = new Size(41, 17);
      this.Label5.TabIndex = 22;
      this.Label5.Text = "End";
      this.Label6.BackColor = SystemColors.Control;
      this.Label6.Cursor = Cursors.Default;
      this.Label6.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.Label6.ForeColor = SystemColors.ControlText;
      this.Label6.Location = new Point(218, 43);
      this.Label6.Name = "Label6";
      this.Label6.RightToLeft = RightToLeft.No;
      this.Label6.Size = new Size(53, 17);
      this.Label6.TabIndex = 21;
      this.Label6.Text = "Duration";
      this.LabelSelectionEnd.BackColor = Color.White;
      this.LabelSelectionEnd.BorderStyle = BorderStyle.Fixed3D;
      this.LabelSelectionEnd.Cursor = Cursors.Default;
      this.LabelSelectionEnd.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.LabelSelectionEnd.ForeColor = Color.Black;
      this.LabelSelectionEnd.Location = new Point(146, 63);
      this.LabelSelectionEnd.Name = "LabelSelectionEnd";
      this.LabelSelectionEnd.RightToLeft = RightToLeft.No;
      this.LabelSelectionEnd.Size = new Size(69, 17);
      this.LabelSelectionEnd.TabIndex = 19;
      this.LabelSelectionEnd.Text = "00:00:00.000";
      this.LabelSelectionEnd.TextAlign = ContentAlignment.TopCenter;
      this.Frame4.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.Frame4.BackColor = SystemColors.Control;
      this.Frame4.Controls.Add((Control) this.btnDeselect);
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
      this.Frame4.Location = new Point(262, 302);
      this.Frame4.Name = "Frame4";
      this.Frame4.RightToLeft = RightToLeft.No;
      this.Frame4.Size = new Size(340, 117);
      this.Frame4.TabIndex = 161;
      this.Frame4.TabStop = false;
      this.Frame4.Text = "Positions";
      this.btnDeselect.BackColor = SystemColors.Control;
      this.btnDeselect.Cursor = Cursors.Default;
      this.btnDeselect.Enabled = false;
      this.btnDeselect.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btnDeselect.ForeColor = SystemColors.ControlText;
      this.btnDeselect.Location = new Point(290, 63);
      this.btnDeselect.Name = "btnDeselect";
      this.btnDeselect.RightToLeft = RightToLeft.No;
      this.btnDeselect.Size = new Size(44, 36);
      this.btnDeselect.TabIndex = 11;
      this.btnDeselect.Text = "De select";
      this.btnDeselect.UseVisualStyleBackColor = false;
      this.btnDeselect.Click += new System.EventHandler(this.btnDeselect_Click);
      this.LabelSelectionBegin.BackColor = Color.White;
      this.LabelSelectionBegin.BorderStyle = BorderStyle.Fixed3D;
      this.LabelSelectionBegin.Cursor = Cursors.Default;
      this.LabelSelectionBegin.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.LabelSelectionBegin.ForeColor = Color.Black;
      this.LabelSelectionBegin.Location = new Point(74, 63);
      this.LabelSelectionBegin.Name = "LabelSelectionBegin";
      this.LabelSelectionBegin.RightToLeft = RightToLeft.No;
      this.LabelSelectionBegin.Size = new Size(69, 17);
      this.LabelSelectionBegin.TabIndex = 20;
      this.LabelSelectionBegin.Text = "00:00:00.000";
      this.LabelSelectionBegin.TextAlign = ContentAlignment.TopCenter;
      this.LabelSelectionDuration.BackColor = Color.White;
      this.LabelSelectionDuration.BorderStyle = BorderStyle.Fixed3D;
      this.LabelSelectionDuration.Cursor = Cursors.Default;
      this.LabelSelectionDuration.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.LabelSelectionDuration.ForeColor = Color.Blue;
      this.LabelSelectionDuration.Location = new Point(218, 63);
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
      this.LabelRangeBegin.Location = new Point(74, 83);
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
      this.LabelRangeEnd.Location = new Point(146, 83);
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
      this.LabelRangeDuration.Location = new Point(218, 83);
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
      this.audioSoundEditor1.Location = new Point(747, 594);
      this.audioSoundEditor1.Name = "audioSoundEditor1";
      this.audioSoundEditor1.Size = new Size(48, 48);
      this.audioSoundEditor1.TabIndex = 26;
      this.audioSoundEditor1.WaveAnalysisStart += new AudioSoundEditor.AudioSoundEditor.EventHandler(this.audioSoundEditor1_WaveAnalysisStart);
      this.audioSoundEditor1.WaveAnalysisPerc += new AudioSoundEditor.AudioSoundEditor.WaveAnalysisPercEventHandler(this.audioSoundEditor1_WaveAnalysisPerc);
      this.audioSoundEditor1.WaveAnalysisDone += new AudioSoundEditor.AudioSoundEditor.WaveAnalysisDoneEventHandler(this.audioSoundEditor1_WaveAnalysisDone);
      this.audioSoundEditor1.WaveAnalyzerSelectionChange += new AudioSoundEditor.AudioSoundEditor.WaveAnalyzerSelectionChangeEventHandler(this.audioSoundEditor1_WaveAnalyzerSelectionChange);
      this.audioSoundEditor1.WaveAnalyzerDisplayRangeChange += new AudioSoundEditor.AudioSoundEditor.WaveAnalyzerDisplayRangeChangeEventHandler(this.audioSoundEditor1_WaveAnalyzerDisplayRangeChange);
      this.audioSoundEditor1.SoundExportStarted += new AudioSoundEditor.AudioSoundEditor.EventHandler(this.audioSoundEditor1_SoundExportStarted);
      this.audioSoundEditor1.SoundPlaybackDone += new AudioSoundEditor.AudioSoundEditor.EventHandler(this.audioSoundEditor1_SoundPlaybackDone);
      this.audioSoundEditor1.SoundPlaybackStopped += new AudioSoundEditor.AudioSoundEditor.EventHandler(this.audioSoundEditor1_SoundPlaybackStopped);
      this.audioSoundEditor1.SoundPlaybackPaused += new AudioSoundEditor.AudioSoundEditor.EventHandler(this.audioSoundEditor1_SoundPlaybackPaused);
      this.audioSoundEditor1.SoundPlaybackPlaying += new AudioSoundEditor.AudioSoundEditor.EventHandler(this.audioSoundEditor1_SoundPlaybackPlaying);
      this.audioSoundEditor1.SoundLoadingStarted += new AudioSoundEditor.AudioSoundEditor.EventHandler(this.audioSoundEditor1_SoundLoadingStarted);
      this.audioSoundEditor1.SoundLoadingPerc += new AudioSoundEditor.AudioSoundEditor.SoundLoadingPercEventHandler(this.audioSoundEditor1_SoundLoadingPerc);
      this.audioSoundEditor1.SoundLoadingDone += new AudioSoundEditor.AudioSoundEditor.SoundLoadingDoneEventHandler(this.audioSoundEditor1_SoundLoadingDone);
      this.audioSoundEditor1.WaveAnalyzerLineMoveEnd += new AudioSoundEditor.AudioSoundEditor.WaveAnalyzerLineMoveEndEventHandler(this.audioSoundEditor1_WaveAnalyzerLineMoveEnd);
      this.audioSoundEditor1.AppendAutomationStarted += new AudioSoundEditor.AudioSoundEditor.EventHandler(this.audioSoundEditor1_AppendAutomationStarted);
      this.audioSoundEditor1.AppendAutomationTotalPerc += new AudioSoundEditor.AudioSoundEditor.AppendAutomationTotalPercEventHandler(this.audioSoundEditor1_AppendAutomationTotalPerc);
      this.audioSoundEditor1.AppendAutomationDone += new AudioSoundEditor.AudioSoundEditor.AppendAutomationDoneEventHandler(this.audioSoundEditor1_AppendAutomationDone);
      this.audioSoundEditor1.SilencePosDetectionPerc += new AudioSoundEditor.AudioSoundEditor.SilencePosDetectionPercEventHandler(this.audioSoundEditor1_SilencePosDetectionPerc);
      this.audioSoundEditor1.SilencePosDetectionDone += new AudioSoundEditor.AudioSoundEditor.SilencePosDetectionDoneEventHandler(this.audioSoundEditor1_SilencePosDetectionDone);
      this.audioSoundEditor1.WaveScrollerManualScroll += new AudioSoundEditor.AudioSoundEditor.WaveScrollerManualScrollEventHandler(this.audioSoundEditor1_WaveScrollerManualScroll);
      this.audioSoundEditor1.WaveScrollerMouseNotification += new AudioSoundEditor.AudioSoundEditor.WaveScrollerMouseNotificationEventHandler(this.audioSoundEditor1_WaveScrollerMouseNotification);
      this.audioSoundEditor1.WaveAnalyzerGraphicItemClick += new AudioSoundEditor.AudioSoundEditor.WaveAnalyzerGraphicItemClickEventHandler(this.audioSoundEditor1_WaveAnalyzerGraphicItemClick);
      this.audioSoundEditor1.VUMeterValueChange += new AudioSoundEditor.AudioSoundEditor.VUMeterValueChangeEventHandler(this.audioSoundEditor1_VUMeterValueChange);
      this.dgvChapters.AllowUserToAddRows = false;
      this.dgvChapters.AllowUserToDeleteRows = false;
      this.dgvChapters.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.dgvChapters.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
      this.dgvChapters.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvChapters.Columns.AddRange((DataGridViewColumn) this.enabled, (DataGridViewColumn) this.chapterName, (DataGridViewColumn) this.pos, (DataGridViewColumn) this.time, (DataGridViewColumn) this.special, (DataGridViewColumn) this.split);
      this.dgvChapters.Location = new Point(13, 302);
      this.dgvChapters.Name = "dgvChapters";
      this.dgvChapters.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgvChapters.Size = new Size(243, 256);
      this.dgvChapters.TabIndex = 163;
      this.dgvChapters.CellBeginEdit += new DataGridViewCellCancelEventHandler(this.dgvChapters_CellBeginEdit);
      this.dgvChapters.CellContentClick += new DataGridViewCellEventHandler(this.dgvChapters_CellContentClick);
      this.dgvChapters.CellContentDoubleClick += new DataGridViewCellEventHandler(this.dgvChapters_CellContentClick);
      this.dgvChapters.CellDoubleClick += new DataGridViewCellEventHandler(this.dgvChapters_CellDoubleClick);
      this.dgvChapters.CellValueChanged += new DataGridViewCellEventHandler(this.dgvChapters_CellValueChanged);
      this.dgvChapters.RowPostPaint += new DataGridViewRowPostPaintEventHandler(this.dgvChapters_RowPostPaint);
      this.dgvChapters.SelectionChanged += new System.EventHandler(this.dgvChapters_SelectionChanged);
      this.dgvChapters.MouseClick += new MouseEventHandler(this.dgvChapters_MouseClick);
      this.enabled.HeaderText = "Enabled";
      this.enabled.Name = "enabled";
      this.enabled.Width = 52;
      this.chapterName.HeaderText = "Chapter";
      this.chapterName.Name = "chapterName";
      this.chapterName.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.chapterName.Width = 50;
      this.pos.HeaderText = "pos";
      this.pos.Name = "pos";
      this.pos.ReadOnly = true;
      this.pos.Visible = false;
      this.time.HeaderText = "time";
      this.time.Name = "time";
      this.time.Visible = false;
      this.special.HeaderText = "special";
      this.special.Name = "special";
      this.special.Visible = false;
      this.split.HeaderText = "split";
      this.split.Name = "split";
      this.split.Visible = false;
      this.btnApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.btnApply.Location = new Point(354, 593);
      this.btnApply.Name = "btnApply";
      this.btnApply.Size = new Size(148, 23);
      this.btnApply.TabIndex = 164;
      this.btnApply.Text = "Apply new chapters";
      this.btnApply.UseVisualStyleBackColor = true;
      this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
      this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnCancel.Location = new Point(816, 593);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 165;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      this.btnNextKeep.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.btnNextKeep.BackColor = Color.Lime;
      this.btnNextKeep.Location = new Point(13, 564);
      this.btnNextKeep.Name = "btnNextKeep";
      this.btnNextKeep.Size = new Size(75, 23);
      this.btnNextKeep.TabIndex = 166;
      this.btnNextKeep.Text = "Next / Keep";
      this.btnNextKeep.UseVisualStyleBackColor = false;
      this.btnNextKeep.Click += new System.EventHandler(this.btnNextKeep_Click);
      this.btnNextDel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.btnNextDel.BackColor = Color.Red;
      this.btnNextDel.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.btnNextDel.ForeColor = Color.Yellow;
      this.btnNextDel.Location = new Point(181, 564);
      this.btnNextDel.Name = "btnNextDel";
      this.btnNextDel.Size = new Size(75, 23);
      this.btnNextDel.TabIndex = 167;
      this.btnNextDel.Text = "Next / Del";
      this.btnNextDel.UseVisualStyleBackColor = false;
      this.btnNextDel.Click += new System.EventHandler(this.btnNextDel_Click);
      this.grpSilence.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.grpSilence.Controls.Add((Control) this.label1);
      this.grpSilence.Controls.Add((Control) this.btnFixedSplit);
      this.grpSilence.Controls.Add((Control) this.txtFixedSplit);
      this.grpSilence.Controls.Add((Control) this.btnFindSilence);
      this.grpSilence.Controls.Add((Control) this.lblSilenceThresh);
      this.grpSilence.Controls.Add((Control) this.txtSilenceThresh);
      this.grpSilence.Location = new Point(265, 487);
      this.grpSilence.Name = "grpSilence";
      this.grpSilence.Size = new Size(337, 74);
      this.grpSilence.TabIndex = 168;
      this.grpSilence.TabStop = false;
      this.grpSilence.Text = "Silence detection";
      this.label1.AutoSize = true;
      this.label1.Location = new Point(24, 46);
      this.label1.Name = "label1";
      this.label1.Size = new Size(185, 13);
      this.label1.TabIndex = 5;
      this.label1.Text = "Split into fixed-sized chunks (minutes):";
      this.btnFixedSplit.Location = new Point(256, 41);
      this.btnFixedSplit.Name = "btnFixedSplit";
      this.btnFixedSplit.Size = new Size(75, 23);
      this.btnFixedSplit.TabIndex = 4;
      this.btnFixedSplit.Text = "Make marks";
      this.btnFixedSplit.UseVisualStyleBackColor = false;
      this.btnFixedSplit.Click += new System.EventHandler(this.btnFixedSplit_Click);
      this.txtFixedSplit.Location = new Point(215, 43);
      this.txtFixedSplit.Name = "txtFixedSplit";
      this.txtFixedSplit.Size = new Size(35, 20);
      this.txtFixedSplit.TabIndex = 3;
      this.txtFixedSplit.Text = "15";
      this.btnFindSilence.Location = new Point(256, 12);
      this.btnFindSilence.Name = "btnFindSilence";
      this.btnFindSilence.Size = new Size(75, 23);
      this.btnFindSilence.TabIndex = 2;
      this.btnFindSilence.Text = "Find Silence";
      this.btnFindSilence.UseVisualStyleBackColor = false;
      this.btnFindSilence.Click += new System.EventHandler(this.btnFindSilence_Click);
      this.lblSilenceThresh.AutoSize = true;
      this.lblSilenceThresh.Location = new Point(37, 17);
      this.lblSilenceThresh.Name = "lblSilenceThresh";
      this.lblSilenceThresh.Size = new Size(172, 13);
      this.lblSilenceThresh.TabIndex = 1;
      this.lblSilenceThresh.Text = "Chapter break threshold (seconds):";
      this.txtSilenceThresh.Location = new Point(215, 14);
      this.txtSilenceThresh.Name = "txtSilenceThresh";
      this.txtSilenceThresh.Size = new Size(35, 20);
      this.txtSilenceThresh.TabIndex = 0;
      this.txtSilenceThresh.Text = "3.25";
      this.saveFileDialog1.OverwritePrompt = false;
      this.backgroundWorker1.WorkerReportsProgress = true;
      this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
      this.backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
      this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
      this.backgroundWorker2.WorkerReportsProgress = true;
      this.backgroundWorker2.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork2);
      this.backgroundWorker2.ProgressChanged += new ProgressChangedEventHandler(this.backgroundWorker2_ProgressChanged);
      this.backgroundWorker2.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
      this.backgroundWorker3.WorkerReportsProgress = true;
      this.backgroundWorker3.DoWork += new DoWorkEventHandler(this.backgroundWorker3_DoWork);
      this.backgroundWorker3.ProgressChanged += new ProgressChangedEventHandler(this.backgroundWorker3_ProgressChanged);
      this.backgroundWorker3.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
      this.btnSpectogram.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnSpectogram.Location = new Point(611, 593);
      this.btnSpectogram.Name = "btnSpectogram";
      this.btnSpectogram.Size = new Size(75, 23);
      this.btnSpectogram.TabIndex = 171;
      this.btnSpectogram.Text = "Spectrogram";
      this.btnSpectogram.UseVisualStyleBackColor = true;
      this.btnSpectogram.Click += new System.EventHandler(this.btnSpectogram_Click);
      this.menuStrip1.Items.AddRange(new ToolStripItem[4]
      {
        (ToolStripItem) this.fileToolStripMenuItem,
        (ToolStripItem) this.settingsToolStripMenuItem,
        (ToolStripItem) this.zoomToolStripMenuItem,
        (ToolStripItem) this.chaptersToolStripMenuItem
      });
      this.menuStrip1.Location = new Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.RenderMode = ToolStripRenderMode.System;
      this.menuStrip1.Size = new Size(903, 24);
      this.menuStrip1.TabIndex = 172;
      this.menuStrip1.Text = "menuStrip1";
      this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[5]
      {
        (ToolStripItem) this.loadFileToolStripMenuItem,
        (ToolStripItem) this.loadCUEFileToolStripMenuItem,
        (ToolStripItem) this.saveCUEFileToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator1,
        (ToolStripItem) this.exitToolStripMenuItem
      });
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new Size(37, 20);
      this.fileToolStripMenuItem.Text = "&File";
      this.loadFileToolStripMenuItem.Name = "loadFileToolStripMenuItem";
      this.loadFileToolStripMenuItem.Size = new Size(182, 22);
      this.loadFileToolStripMenuItem.Text = "Load file";
      this.loadFileToolStripMenuItem.Click += new System.EventHandler(this.loadFileToolStripMenuItem_Click);
      this.loadCUEFileToolStripMenuItem.Name = "loadCUEFileToolStripMenuItem";
      this.loadCUEFileToolStripMenuItem.Size = new Size(182, 22);
      this.loadCUEFileToolStripMenuItem.Text = "Load CUE file";
      this.loadCUEFileToolStripMenuItem.Click += new System.EventHandler(this.loadCUEFileToolStripMenuItem_Click);
      this.saveCUEFileToolStripMenuItem.Name = "saveCUEFileToolStripMenuItem";
      this.saveCUEFileToolStripMenuItem.ShortcutKeys = Keys.S | Keys.Control;
      this.saveCUEFileToolStripMenuItem.Size = new Size(182, 22);
      this.saveCUEFileToolStripMenuItem.Text = "Save CUE file";
      this.saveCUEFileToolStripMenuItem.Click += new System.EventHandler(this.saveCUEFileToolStripMenuItem_Click);
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new Size(179, 6);
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new Size(182, 22);
      this.exitToolStripMenuItem.Text = "Exit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
      this.settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.channelViewToolStripMenuItem,
        (ToolStripItem) this.splitEngineToolStripMenuItem
      });
      this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
      this.settingsToolStripMenuItem.Size = new Size(61, 20);
      this.settingsToolStripMenuItem.Text = "Settings";
      this.channelViewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.combinedToolStripMenuItem,
        (ToolStripItem) this.discreteToolStripMenuItem
      });
      this.channelViewToolStripMenuItem.Name = "channelViewToolStripMenuItem";
      this.channelViewToolStripMenuItem.Size = new Size(145, 22);
      this.channelViewToolStripMenuItem.Text = "Channel view";
      this.combinedToolStripMenuItem.Name = "combinedToolStripMenuItem";
      this.combinedToolStripMenuItem.Size = new Size(130, 22);
      this.combinedToolStripMenuItem.Text = "Combined";
      this.combinedToolStripMenuItem.Click += new System.EventHandler(this.combinedToolStripMenuItem_Click);
      this.discreteToolStripMenuItem.Name = "discreteToolStripMenuItem";
      this.discreteToolStripMenuItem.Size = new Size(130, 22);
      this.discreteToolStripMenuItem.Text = "Discrete";
      this.discreteToolStripMenuItem.Click += new System.EventHandler(this.discreteToolStripMenuItem_Click);
      this.splitEngineToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.ffmpegToolStripMenuItem,
        (ToolStripItem) this.mP3SPLTToolStripMenuItem
      });
      this.splitEngineToolStripMenuItem.Name = "splitEngineToolStripMenuItem";
      this.splitEngineToolStripMenuItem.Size = new Size(145, 22);
      this.splitEngineToolStripMenuItem.Text = "Split Engine";
      this.ffmpegToolStripMenuItem.Checked = true;
      this.ffmpegToolStripMenuItem.CheckState = CheckState.Checked;
      this.ffmpegToolStripMenuItem.Name = "ffmpegToolStripMenuItem";
      this.ffmpegToolStripMenuItem.Size = new Size(124, 22);
      this.ffmpegToolStripMenuItem.Text = "ffmpeg";
      this.ffmpegToolStripMenuItem.Click += new System.EventHandler(this.ffmpegToolStripMenuItem_Click);
      this.mP3SPLTToolStripMenuItem.Name = "mP3SPLTToolStripMenuItem";
      this.mP3SPLTToolStripMenuItem.Size = new Size(124, 22);
      this.mP3SPLTToolStripMenuItem.Text = "MP3SPLT";
      this.mP3SPLTToolStripMenuItem.Click += new System.EventHandler(this.mP3SPLTToolStripMenuItem_Click);
      this.zoomToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[6]
      {
        (ToolStripItem) this.zoomToselectionToolStripMenuItem,
        (ToolStripItem) this.zoomToFullwaveformToolStripMenuItem,
        (ToolStripItem) this.zoominToolStripMenuItem,
        (ToolStripItem) this.zoomoutToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator4,
        (ToolStripItem) this.mouseWheelZoomsToPointerToolStripMenuItem
      });
      this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
      this.zoomToolStripMenuItem.Size = new Size(51, 20);
      this.zoomToolStripMenuItem.Text = "&Zoom";
      this.zoomToselectionToolStripMenuItem.Name = "zoomToselectionToolStripMenuItem";
      this.zoomToselectionToolStripMenuItem.Size = new Size(237, 22);
      this.zoomToselectionToolStripMenuItem.Text = "Zoom to &selection";
      this.zoomToselectionToolStripMenuItem.Click += new System.EventHandler(this.zoomToselectionToolStripMenuItem_Click);
      this.zoomToFullwaveformToolStripMenuItem.Name = "zoomToFullwaveformToolStripMenuItem";
      this.zoomToFullwaveformToolStripMenuItem.Size = new Size(237, 22);
      this.zoomToFullwaveformToolStripMenuItem.Text = "Zoom to full &waveform";
      this.zoomToFullwaveformToolStripMenuItem.Click += new System.EventHandler(this.zoomToFullwaveformToolStripMenuItem_Click);
      this.zoominToolStripMenuItem.Name = "zoominToolStripMenuItem";
      this.zoominToolStripMenuItem.ShortcutKeys = Keys.Add | Keys.Control;
      this.zoominToolStripMenuItem.Size = new Size(237, 22);
      this.zoominToolStripMenuItem.Text = "Zoom &in";
      this.zoominToolStripMenuItem.Click += new System.EventHandler(this.zoominToolStripMenuItem_Click);
      this.zoomoutToolStripMenuItem.Name = "zoomoutToolStripMenuItem";
      this.zoomoutToolStripMenuItem.ShortcutKeys = Keys.Subtract | Keys.Control;
      this.zoomoutToolStripMenuItem.Size = new Size(237, 22);
      this.zoomoutToolStripMenuItem.Text = "Zoom &out";
      this.zoomoutToolStripMenuItem.Click += new System.EventHandler(this.zoomoutToolStripMenuItem_Click);
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new Size(234, 6);
      this.mouseWheelZoomsToPointerToolStripMenuItem.CheckOnClick = true;
      this.mouseWheelZoomsToPointerToolStripMenuItem.Name = "mouseWheelZoomsToPointerToolStripMenuItem";
      this.mouseWheelZoomsToPointerToolStripMenuItem.Size = new Size(237, 22);
      this.mouseWheelZoomsToPointerToolStripMenuItem.Text = "Mouse wheel zooms to pointer";
      this.chaptersToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[13]
      {
        (ToolStripItem) this.nextChapterToolStripMenuItem,
        (ToolStripItem) this.previousChapterToolStripMenuItem,
        (ToolStripItem) this.deleteChapterToolStripMenuItem,
        (ToolStripItem) this.deletePreviousChapterToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator2,
        (ToolStripItem) this.insertNewChapterToolStripMenuItem,
        (ToolStripItem) this.clearChapterListToolStripMenuItem,
        (ToolStripItem) this.hardDeleteChapterToolStripMenuItem,
        (ToolStripItem) this.pruneDisabledChaptersToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator3,
        (ToolStripItem) this.chapterNamesNumbersInFilenameToolStripMenuItem,
        (ToolStripItem) this.showChapterDebugToolStripMenuItem,
        (ToolStripItem) this.detectedSilenceAlignmentToolStripMenuItem
      });
      this.chaptersToolStripMenuItem.Name = "chaptersToolStripMenuItem";
      this.chaptersToolStripMenuItem.Size = new Size(66, 20);
      this.chaptersToolStripMenuItem.Text = "&Chapters";
      this.nextChapterToolStripMenuItem.Name = "nextChapterToolStripMenuItem";
      this.nextChapterToolStripMenuItem.ShortcutKeys = Keys.J | Keys.Control;
      this.nextChapterToolStripMenuItem.Size = new Size(289, 22);
      this.nextChapterToolStripMenuItem.Text = "Next Chapter";
      this.nextChapterToolStripMenuItem.Click += new System.EventHandler(this.nextChapterToolStripMenuItem_Click);
      this.previousChapterToolStripMenuItem.Name = "previousChapterToolStripMenuItem";
      this.previousChapterToolStripMenuItem.ShortcutKeys = Keys.J | Keys.Shift | Keys.Control;
      this.previousChapterToolStripMenuItem.Size = new Size(289, 22);
      this.previousChapterToolStripMenuItem.Text = "Previous Chapter";
      this.previousChapterToolStripMenuItem.Click += new System.EventHandler(this.previousChapterToolStripMenuItem_Click);
      this.deleteChapterToolStripMenuItem.Name = "deleteChapterToolStripMenuItem";
      this.deleteChapterToolStripMenuItem.ShortcutKeys = Keys.K | Keys.Control;
      this.deleteChapterToolStripMenuItem.Size = new Size(289, 22);
      this.deleteChapterToolStripMenuItem.Text = "Delete Chapter";
      this.deleteChapterToolStripMenuItem.Click += new System.EventHandler(this.deleteChapterToolStripMenuItem_Click);
      this.deletePreviousChapterToolStripMenuItem.Name = "deletePreviousChapterToolStripMenuItem";
      this.deletePreviousChapterToolStripMenuItem.ShortcutKeys = Keys.K | Keys.Shift | Keys.Control;
      this.deletePreviousChapterToolStripMenuItem.Size = new Size(289, 22);
      this.deletePreviousChapterToolStripMenuItem.Text = "Delete Previous Chapter";
      this.deletePreviousChapterToolStripMenuItem.Click += new System.EventHandler(this.deletePreviousChapterToolStripMenuItem_Click);
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new Size(286, 6);
      this.insertNewChapterToolStripMenuItem.Name = "insertNewChapterToolStripMenuItem";
      this.insertNewChapterToolStripMenuItem.ShortcutKeys = Keys.I | Keys.Control;
      this.insertNewChapterToolStripMenuItem.Size = new Size(289, 22);
      this.insertNewChapterToolStripMenuItem.Text = "Insert New Chapter";
      this.insertNewChapterToolStripMenuItem.Click += new System.EventHandler(this.insertNewChapterToolStripMenuItem_Click);
      this.clearChapterListToolStripMenuItem.Name = "clearChapterListToolStripMenuItem";
      this.clearChapterListToolStripMenuItem.Size = new Size(289, 22);
      this.clearChapterListToolStripMenuItem.Text = "Clear Chapter List";
      this.clearChapterListToolStripMenuItem.Click += new System.EventHandler(this.clearChapterListToolStripMenuItem_Click);
      this.hardDeleteChapterToolStripMenuItem.Name = "hardDeleteChapterToolStripMenuItem";
      this.hardDeleteChapterToolStripMenuItem.ShortcutKeys = Keys.X | Keys.Alt;
      this.hardDeleteChapterToolStripMenuItem.Size = new Size(289, 22);
      this.hardDeleteChapterToolStripMenuItem.Text = "Hard Delete Selected Chapter(s)";
      this.hardDeleteChapterToolStripMenuItem.Click += new System.EventHandler(this.hardDeleteChapterToolStripMenuItem_Click);
      this.pruneDisabledChaptersToolStripMenuItem.Name = "pruneDisabledChaptersToolStripMenuItem";
      this.pruneDisabledChaptersToolStripMenuItem.ShortcutKeys = Keys.X | Keys.Control;
      this.pruneDisabledChaptersToolStripMenuItem.Size = new Size(289, 22);
      this.pruneDisabledChaptersToolStripMenuItem.Text = "Prune Disabled Chapters";
      this.pruneDisabledChaptersToolStripMenuItem.Click += new System.EventHandler(this.pruneDisabledChaptersToolStripMenuItem_Click);
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new Size(286, 6);
      this.chapterNamesNumbersInFilenameToolStripMenuItem.Checked = true;
      this.chapterNamesNumbersInFilenameToolStripMenuItem.CheckState = CheckState.Checked;
      this.chapterNamesNumbersInFilenameToolStripMenuItem.Name = "chapterNamesNumbersInFilenameToolStripMenuItem";
      this.chapterNamesNumbersInFilenameToolStripMenuItem.Size = new Size(289, 22);
      this.chapterNamesNumbersInFilenameToolStripMenuItem.Text = "Chapter names and numbers in filename";
      this.chapterNamesNumbersInFilenameToolStripMenuItem.Click += new System.EventHandler(this.chapterNamesNumbersInFilenameToolStripMenuItem_Click);
      this.showChapterDebugToolStripMenuItem.Name = "showChapterDebugToolStripMenuItem";
      this.showChapterDebugToolStripMenuItem.Size = new Size(289, 22);
      this.showChapterDebugToolStripMenuItem.Text = "Show chapter debug";
      this.showChapterDebugToolStripMenuItem.Click += new System.EventHandler(this.showChapterDebugToolStripMenuItem_Click);
      this.detectedSilenceAlignmentToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.middleToolStripMenuItem,
        (ToolStripItem) this.startToolStripMenuItem,
        (ToolStripItem) this.endToolStripMenuItem
      });
      this.detectedSilenceAlignmentToolStripMenuItem.Name = "detectedSilenceAlignmentToolStripMenuItem";
      this.detectedSilenceAlignmentToolStripMenuItem.Size = new Size(289, 22);
      this.detectedSilenceAlignmentToolStripMenuItem.Text = "Detected silence alignment";
      this.middleToolStripMenuItem.Checked = true;
      this.middleToolStripMenuItem.CheckOnClick = true;
      this.middleToolStripMenuItem.CheckState = CheckState.Checked;
      this.middleToolStripMenuItem.Name = "middleToolStripMenuItem";
      this.middleToolStripMenuItem.Size = new Size(111, 22);
      this.middleToolStripMenuItem.Text = "Middle";
      this.middleToolStripMenuItem.Click += new System.EventHandler(this.middleToolStripMenuItem_Click);
      this.startToolStripMenuItem.CheckOnClick = true;
      this.startToolStripMenuItem.Name = "startToolStripMenuItem";
      this.startToolStripMenuItem.Size = new Size(111, 22);
      this.startToolStripMenuItem.Text = "Start";
      this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
      this.endToolStripMenuItem.CheckOnClick = true;
      this.endToolStripMenuItem.Name = "endToolStripMenuItem";
      this.endToolStripMenuItem.Size = new Size(111, 22);
      this.endToolStripMenuItem.Text = "End";
      this.endToolStripMenuItem.Click += new System.EventHandler(this.endToolStripMenuItem_Click);
      this.label18.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
      this.label18.BackColor = Color.Black;
      this.label18.Location = new Point(846, 27);
      this.label18.Name = "label18";
      this.label18.Size = new Size(21, 185);
      this.label18.TabIndex = 175;
      this.label17.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
      this.label17.BackColor = Color.Black;
      this.label17.Location = new Point(870, 27);
      this.label17.Name = "label17";
      this.label17.Size = new Size(21, 185);
      this.label17.TabIndex = 174;
      this.btnZoomIn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnZoomIn.Location = new Point(845, 215);
      this.btnZoomIn.Name = "btnZoomIn";
      this.btnZoomIn.Size = new Size(20, 23);
      this.btnZoomIn.TabIndex = 176;
      this.btnZoomIn.Text = "+";
      this.btnZoomIn.UseVisualStyleBackColor = true;
      this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click_1);
      this.btnZoomOut.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnZoomOut.Location = new Point(871, 215);
      this.btnZoomOut.Name = "btnZoomOut";
      this.btnZoomOut.Size = new Size(20, 23);
      this.btnZoomOut.TabIndex = 177;
      this.btnZoomOut.Text = "-";
      this.btnZoomOut.UseVisualStyleBackColor = true;
      this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
      this.btnZoomFull.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnZoomFull.Location = new Point(844, 269);
      this.btnZoomFull.Name = "btnZoomFull";
      this.btnZoomFull.Size = new Size(47, 23);
      this.btnZoomFull.TabIndex = 178;
      this.btnZoomFull.Text = "Full";
      this.btnZoomFull.UseVisualStyleBackColor = true;
      this.btnZoomFull.Click += new System.EventHandler(this.btnZoomFull_Click);
      this.btnLeft.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.btnLeft.Location = new Point(10, 273);
      this.btnLeft.Name = "btnLeft";
      this.btnLeft.Size = new Size(43, 19);
      this.btnLeft.TabIndex = 181;
      this.btnLeft.Text = "<<";
      this.btnLeft.UseVisualStyleBackColor = false;
      this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
      this.btnRight.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnRight.Location = new Point(795, 273);
      this.btnRight.Name = "btnRight";
      this.btnRight.Size = new Size(43, 19);
      this.btnRight.TabIndex = 182;
      this.btnRight.Text = ">>";
      this.btnRight.UseVisualStyleBackColor = false;
      this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
      this.scrollBarEx1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.scrollBarEx1.LargeChange = 100;
      this.scrollBarEx1.Location = new Point(59, 273);
      this.scrollBarEx1.Name = "scrollBarEx1";
      this.scrollBarEx1.Orientation = ScrollBarOrientation.Horizontal;
      this.scrollBarEx1.Size = new Size(732, 19);
      this.scrollBarEx1.TabIndex = 183;
      this.scrollBarEx1.Value = 50;
      this.scrollBarEx1.Scroll += new ScrollEventHandler(this.scrollBarEx1_Scroll);
      this.scrollBarEx1.MouseDown += new MouseEventHandler(this.scrollBarEx1_MouseDown);
      this.scrollBarEx1.MouseUp += new MouseEventHandler(this.scrollBarEx1_MouseUp);
      this.btnZoomToSelected.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnZoomToSelected.Enabled = false;
      this.btnZoomToSelected.Location = new Point(844, 244);
      this.btnZoomToSelected.Name = "btnZoomToSelected";
      this.btnZoomToSelected.Size = new Size(47, 23);
      this.btnZoomToSelected.TabIndex = 184;
      this.btnZoomToSelected.Text = "Zoom";
      this.btnZoomToSelected.UseVisualStyleBackColor = true;
      this.btnZoomToSelected.Click += new System.EventHandler(this.btnZoomToSelected_Click);
      this.chkSplitNames.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.chkSplitNames.AutoSize = true;
      this.chkSplitNames.Location = new Point(350, 570);
      this.chkSplitNames.Name = "chkSplitNames";
      this.chkSplitNames.Size = new Size(165, 17);
      this.chkSplitNames.TabIndex = 185;
      this.chkSplitNames.Text = "Add chapter titles to file name";
      this.chkSplitNames.UseVisualStyleBackColor = true;
      this.chkSplitNames.Visible = false;
      this.btnAlignChapters.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.btnAlignChapters.Location = new Point(97, 564);
      this.btnAlignChapters.Name = "btnAlignChapters";
      this.btnAlignChapters.Size = new Size(75, 23);
      this.btnAlignChapters.TabIndex = 186;
      this.btnAlignChapters.Text = "Align Chapters";
      this.btnAlignChapters.UseVisualStyleBackColor = false;
      this.btnAlignChapters.Click += new System.EventHandler(this.btnAlignChapters_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(903, 626);
      this.Controls.Add((Control) this.btnAlignChapters);
      this.Controls.Add((Control) this.chkSplitNames);
      this.Controls.Add((Control) this.btnZoomToSelected);
      this.Controls.Add((Control) this.scrollBarEx1);
      this.Controls.Add((Control) this.btnRight);
      this.Controls.Add((Control) this.btnLeft);
      this.Controls.Add((Control) this.btnZoomFull);
      this.Controls.Add((Control) this.btnZoomOut);
      this.Controls.Add((Control) this.btnZoomIn);
      this.Controls.Add((Control) this.label18);
      this.Controls.Add((Control) this.label17);
      this.Controls.Add((Control) this.btnSpectogram);
      this.Controls.Add((Control) this.grpSilence);
      this.Controls.Add((Control) this.btnNextDel);
      this.Controls.Add((Control) this.btnNextKeep);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.btnApply);
      this.Controls.Add((Control) this.dgvChapters);
      this.Controls.Add((Control) this.audioSoundEditor1);
      this.Controls.Add((Control) this.Frame4);
      this.Controls.Add((Control) this.progressBar1);
      this.Controls.Add((Control) this.Frame5);
      this.Controls.Add((Control) this.LabelStatus);
      this.Controls.Add((Control) this.labelSpectrum);
      this.Controls.Add((Control) this.Picture1);
      this.Controls.Add((Control) this.menuStrip1);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MinimumSize = new Size(919, 664);
      this.Name = nameof (AdvancedSplitting);
      this.Text = "Advanced Cutter / Chapterizer";
      this.FormClosing += new FormClosingEventHandler(this.AdvancedSplitting_FormClosing);
      this.Load += new System.EventHandler(this.AdvancedSplitting_Load);
      this.Resize += new System.EventHandler(this.AdvancedSplitting_Resize);
      ((ISupportInitialize) this.Picture1).EndInit();
      this.Frame5.ResumeLayout(false);
      this.Frame4.ResumeLayout(false);
      ((ISupportInitialize) this.dgvChapters).EndInit();
      this.grpSilence.ResumeLayout(false);
      this.grpSilence.PerformLayout();
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public class Chapter
    {
      public string description = "";
      public Color color = Color.White;
      public double time;
      public double offset;

      public void Add(double t)
      {
        this.time = t;
      }

      public void Add(double t, string desc)
      {
        this.time = t;
        this.description = Utilities.CleanFileName(desc);
      }

      public string GetASCIITag(string property)
      {
        property = property.Replace("ö", "oe");
        property = property.Replace("Ö", "Oe");
        property = property.Replace("ü", "ue");
        property = property.Replace("Ü", "Ue");
        property = property.Replace("ä", "ae");
        property = property.Replace("Ä", "Ae");
        property = property.Replace("ß", "ss");
        property = new Regex("[^a-zA-Z0-9().,! -]").Replace(property, " ");
        return property;
      }
    }

    public class Chapters
    {
      private List<AdvancedSplitting.Chapter> markers = new List<AdvancedSplitting.Chapter>();
      public bool includeFileNumbers = true;
      public bool generatedDescriptions;
      public bool doNotUseDescriptions;
      public bool customChapterNames;
      public bool customChapters;
      public bool useAsTitle;
      public bool useTrackAndTitleAsTitle;
      public bool noFileNames;
      public bool leadingZeroes;
      public double totalTime;

      public void SetTotalTime(double t)
      {
        this.totalTime = t;
      }

      public void SetRealEndChapter()
      {
        if (this.markers.Count == 0)
          this.Add(0.0);
        if (this.totalTime <= 0.0 || Math.Abs(this.totalTime - this.markers[this.markers.Count - 1].time) <= 1.0)
          return;
        this.Add(this.totalTime);
      }

      public void SetEndChapter()
      {
        if (this.markers.Count == 0)
          this.Add(0.0);
        if (this.totalTime <= 0.0 || Math.Abs(this.totalTime - this.markers[this.markers.Count - 1].time) <= 1.0)
          return;
        this.Add(this.totalTime - 0.9, "(End)");
      }

      public int Count()
      {
        return this.markers.Count<AdvancedSplitting.Chapter>();
      }

      public string GetFullFileName(int pos, string filename)
      {
        return this.GetFinalFileName(pos, filename);
      }

      public string GetFinalFileName(int pos, string path)
      {
        string str = !this.leadingZeroes ? path + " - " + this.GetFormattedFileName(pos) : this.GetFormattedFileName(pos) + " - " + path;
        if (this.noFileNames)
          str = this.GetFormattedFileName(pos);
        return str;
      }

      public int GetLeadingZeroes()
      {
        return this.markers.Count.ToString().Length;
      }

      public string GetFormattedFileName(int pos)
      {
        return !this.generatedDescriptions ? (!this.includeFileNumbers ? Utilities.CleanFileName(this.markers[pos].description) : (pos + 1).ToString("D" + (object) this.GetLeadingZeroes()) + " - " + Utilities.CleanFileName(this.markers[pos].description)) : (pos + 1).ToString("D" + (object) this.GetLeadingZeroes());
      }

      public void Add(double time, string desc)
      {
        AdvancedSplitting.Chapter chapter = new AdvancedSplitting.Chapter();
        chapter.Add(time, desc);
        this.markers.Add(chapter);
      }

      public void Add(double time)
      {
        AdvancedSplitting.Chapter chapter = new AdvancedSplitting.Chapter();
        int num = this.markers.Count + 1;
        chapter.Add(time, num.ToString("D3"));
        this.markers.Add(chapter);
      }

      public void Clear()
      {
        this.markers.Clear();
        this.markers = new List<AdvancedSplitting.Chapter>();
      }

      public List<double> GetDoubleList()
      {
        List<double> doubleList = new List<double>();
        foreach (AdvancedSplitting.Chapter marker in this.markers)
          doubleList.Add(marker.time);
        return doubleList;
      }

      public List<string> GetChapterNames(bool getGenerated = false)
      {
        List<string> stringList = new List<string>();
        if (this.generatedDescriptions && !getGenerated)
          return stringList;
        foreach (AdvancedSplitting.Chapter marker in this.markers)
          stringList.Add(marker.description);
        return stringList;
      }

      public void SetDescription(int pos, string text)
      {
        this.markers[pos].description = text;
      }

      public string GetDescription(int pos)
      {
        string str = "";
        try
        {
          str = this.markers[pos].description;
        }
        catch
        {
        }
        return str;
      }

      public AdvancedSplitting.Chapter GetChapter(int pos)
      {
        return this.markers[pos];
      }

      public void SetChapter(int pos, double val)
      {
        this.markers[pos].time = val;
      }

      public double GetChapterDouble(int pos)
      {
        return this.markers[pos].time;
      }

      public string GetChapterFormatted(int pos)
      {
        return Utilities.ConvertDoubleToTimeString(this.markers[pos].time);
      }

      public double GetChapterOffset(int pos)
      {
        return this.markers[pos].offset;
      }

      public void SetChapterOffset(int pos, double offset)
      {
        this.markers[pos].offset = offset;
      }

      public Color GetChapterColor(int pos)
      {
        return this.markers[pos].color;
      }

      public void SetChapterColor(int pos, Color color)
      {
        this.markers[pos].color = color;
      }

      public void Remove(int pos)
      {
        this.markers.RemoveAt(pos);
      }

      public double GetLastChapter()
      {
        try
        {
          return this.markers[this.markers.Count - 1].time;
        }
        catch
        {
          return 0.0;
        }
      }

      public void SetDoubleChapters(List<double> chapters)
      {
        this.markers.Clear();
        this.markers = new List<AdvancedSplitting.Chapter>();
        this.generatedDescriptions = true;
        int num = 1;
        foreach (double chapter in chapters)
        {
          this.Add(chapter, num.ToString("D3"));
          ++num;
        }
      }

      public void UpdateDoubleChapters(List<double> chapters)
      {
        for (int index = 0; index < chapters.Count; ++index)
          this.markers[index].time = chapters[index];
      }

      public void SetChapterNames(List<string> desc)
      {
        for (int index = 0; index < desc.Count; ++index)
          this.markers[index].description = desc[index];
      }

      public void SetChaptersAndDescriptions(List<double> chapters, List<string> desc)
      {
        this.markers.Clear();
        this.markers = new List<AdvancedSplitting.Chapter>();
        for (int index = 0; index < chapters.Count; ++index)
          this.Add(chapters[index], desc[index]);
      }

      internal void SanityCheck()
      {
        this.RemoveShortChapters();
      }

      private void RemoveShortChapters()
      {
        double num1 = 10.0;
        for (int index = 1; index < this.markers.Count; ++index)
        {
          double num2 = this.markers[index].time - this.markers[index - 1].time;
          if (num1 > num2)
          {
            if (index == 1)
              this.markers.RemoveAt(index);
            else
              this.markers.RemoveAt(index - 1);
            Audible.diskLogger("Removing crappy chapter " + (object) index + " (" + (object) num2 + " seconds )");
          }
        }
      }
    }
  }
}
