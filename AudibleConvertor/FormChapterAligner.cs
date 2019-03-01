// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.FormChapterAligner
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using AudioSoundEditor;
using Inwards;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AudibleConvertor
{
  public class FormChapterAligner : Form
  {
    public JobStatus myJobStatus = new JobStatus();
    private string g_strNextOperation = "";
    private IContainer components;
    private AudioSoundEditor.AudioSoundEditor audioSoundEditor1;
    private PictureBox Picture1;
    private Button btnApply;
    private Button btnCancel;
    public AdjustFile fileToAdjust;
    public List<Overdrive> myOverdriveFiles;
    private bool wavLoadComplete;
    private bool silenceDetectionComplete;
    public bool jobComplete;
    public bool applied;
    private int FileNum;
    private int ChapNum;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormChapterAligner));
      this.audioSoundEditor1 = new AudioSoundEditor.AudioSoundEditor();
      this.Picture1 = new PictureBox();
      this.btnApply = new Button();
      this.btnCancel = new Button();
      ((ISupportInitialize) this.Picture1).BeginInit();
      this.SuspendLayout();
      this.audioSoundEditor1.Location = new Point(571, 144);
      this.audioSoundEditor1.Name = "audioSoundEditor1";
      this.audioSoundEditor1.Size = new Size(48, 48);
      this.audioSoundEditor1.TabIndex = 0;
      this.audioSoundEditor1.WaveAnalysisDone += new AudioSoundEditor.AudioSoundEditor.WaveAnalysisDoneEventHandler(this.audioSoundEditor1_WaveAnalysisDone);
      this.audioSoundEditor1.SoundLoadingDone += new AudioSoundEditor.AudioSoundEditor.SoundLoadingDoneEventHandler(this.audioSoundEditor1_SoundLoadingDone);
      this.audioSoundEditor1.WaveAnalyzerLineMoveEnd += new AudioSoundEditor.AudioSoundEditor.WaveAnalyzerLineMoveEndEventHandler(this.audioSoundEditor1_WaveAnalyzerLineMoveEnd);
      this.audioSoundEditor1.SilencePosDetectionDone += new AudioSoundEditor.AudioSoundEditor.SilencePosDetectionDoneEventHandler(this.audioSoundEditor1_SilencePosDetectionDone);
      this.Picture1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.Picture1.BackColor = Color.Black;
      this.Picture1.Location = new Point(12, 12);
      this.Picture1.Name = "Picture1";
      this.Picture1.Size = new Size(594, 309);
      this.Picture1.TabIndex = 1;
      this.Picture1.TabStop = false;
      this.Picture1.Visible = false;
      this.btnApply.Anchor = AnchorStyles.Bottom;
      this.btnApply.Enabled = false;
      this.btnApply.Location = new Point(272, 327);
      this.btnApply.Name = "btnApply";
      this.btnApply.Size = new Size(75, 23);
      this.btnApply.TabIndex = 2;
      this.btnApply.Text = "Apply";
      this.btnApply.UseVisualStyleBackColor = true;
      this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
      this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnCancel.Location = new Point(531, 327);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 3;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(618, 360);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.btnApply);
      this.Controls.Add((Control) this.Picture1);
      this.Controls.Add((Control) this.audioSoundEditor1);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (FormChapterAligner);
      this.Text = nameof (FormChapterAligner);
      this.Shown += new System.EventHandler(this.FormChapterAligner_Shown);
      this.Resize += new System.EventHandler(this.FormChapterAligner_Resize);
      ((ISupportInitialize) this.Picture1).EndInit();
      this.ResumeLayout(false);
    }

    public FormChapterAligner()
    {
      this.InitializeComponent();
    }

    public void Init()
    {
      int num1 = (int) this.audioSoundEditor1.InitEditor();
      int num2 = (int) this.audioSoundEditor1.SetStoreMode(enumStoreModes.STORE_MODE_MEMORY_BUFFER);
      int num3 = (int) this.audioSoundEditor1.SetLoadingMode(enumLoadingModes.LOAD_MODE_NEW);
      int num4 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.Create(this.Handle, this.Picture1.Left, this.Picture1.Top, this.Picture1.Width, this.Picture1.Height);
      int num5 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.EnableScrollbarsDuringPlayback(true);
      WANALYZER_GENERAL_SETTINGS settings1 = new WANALYZER_GENERAL_SETTINGS();
      int num6 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsGeneralGet(ref settings1);
      settings1.bAutoRefresh = true;
      int num7 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsGeneralSet(settings1);
      WANALYZER_SCROLLBARS_SETTINGS settings2 = new WANALYZER_SCROLLBARS_SETTINGS();
      int num8 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsScrollbarsGet(ref settings2);
      settings2.bVisibleBottom = false;
      settings2.bVisibleTop = false;
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
      int num12 = (int) this.audioSoundEditor1.EnableSoundPreloadForPlayback(true);
      int num13 = (int) this.audioSoundEditor1.EnableAutoWaveAnalysisOnLoad(true);
    }

    public void AlignChapter()
    {
      this.fileToAdjust.ExtractSubset();
      enumErrorCodes enumErrorCodes = this.audioSoundEditor1.LoadSound(this.fileToAdjust.TempFile);
      if (enumErrorCodes == enumErrorCodes.ERR_NOERROR)
        return;
      Audible.diskLogger("Cannot load file " + this.fileToAdjust.File + ": " + enumErrorCodes.ToString());
    }

    private void AlignAllChapters()
    {
      for (int index = 0; index < this.myOverdriveFiles.Count; ++index)
      {
        AdvancedSplitting.Chapters chapters = this.myOverdriveFiles[index].ReturnChapters();
        for (int pos = 0; pos < chapters.Count(); ++pos)
        {
          if (chapters.GetChapterDouble(pos) != 0.0)
          {
            this.fileToAdjust = new AdjustFile();
            this.fileToAdjust.File = this.myOverdriveFiles[index].mp3Filename;
            this.fileToAdjust.Chapter = chapters.GetChapterDouble(pos);
            Utilities utilities = new Utilities();
            utilities.StopwatchStart();
            this.fileToAdjust.ExtractSubset();
            enumErrorCodes enumErrorCodes = this.audioSoundEditor1.LoadSound(this.fileToAdjust.TempFile);
            if (enumErrorCodes != enumErrorCodes.ERR_NOERROR)
              Audible.diskLogger("Cannot load file " + this.fileToAdjust.File + ": " + enumErrorCodes.ToString());
            double offset = this.fileToAdjust.Offset;
            utilities.StopwatchStop();
            string withoutExtension = Path.GetFileNameWithoutExtension(this.myOverdriveFiles[index].mp3Filename);
            Audible.diskLogger((pos + 1).ToString() + " - " + withoutExtension + ": \"" + chapters.GetDescription(pos) + "\" offset by " + string.Format("{0:0.0}", (object) (offset - chapters.GetChapterDouble(pos))) + "s - search time was " + utilities.StopwatchGetElapsed(""));
            chapters.SetChapter(pos, offset);
          }
        }
      }
    }

    private void FormChapterAligner_Load(object sender, EventArgs e)
    {
      this.Init();
      this.AlignChapter();
      this.jobComplete = true;
    }

    private void BatchAlign()
    {
      for (int index = 0; index < this.myOverdriveFiles.Count; ++index)
      {
        AdvancedSplitting.Chapters chapters = this.myOverdriveFiles[index].ReturnChapters();
        for (int pos = 0; pos < chapters.Count(); ++pos)
        {
          if (chapters.GetChapterDouble(pos) != 0.0)
          {
            int num1 = (int) this.audioSoundEditor1.CloseSound();
            this.fileToAdjust = new AdjustFile();
            this.fileToAdjust.File = this.myOverdriveFiles[index].mp3Filename;
            this.fileToAdjust.Chapter = chapters.GetChapterDouble(pos);
            new Utilities().StopwatchStart();
            this.fileToAdjust.ExtractSubset();
            int num2 = (int) this.audioSoundEditor1.UseThreadsInSyncMode(true);
            this.wavLoadComplete = false;
            enumErrorCodes enumErrorCodes = this.audioSoundEditor1.LoadSound(this.fileToAdjust.TempFile);
            if (enumErrorCodes == enumErrorCodes.ERR_NOERROR)
              return;
            Audible.diskLogger("Cannot load file " + this.fileToAdjust.File + ": " + enumErrorCodes.ToString());
            return;
          }
        }
      }
    }

    private short AddChapterMarker(double chap, int chapNum, Color color)
    {
      WANALYZER_VERTICAL_LINE settings;
      settings.color = color;
      settings.nDashStyle = enumWaveformLineDashStyles.LINE_DASH_STYLE_SOLID;
      settings.nWidth = (short) 2;
      settings.nTranspFactor = (short) 0;
      settings.nHighCap = enumLineCaps.LINE_CAP_SQUARE;
      settings.nLowCap = enumLineCaps.LINE_CAP_SQUARE;
      settings.nDashCap = enumLineDashCaps.LINE_DASH_CAP_FLAT;
      return this.audioSoundEditor1.DisplayWaveformAnalyzer.GraphicItemVerticalLineAdd("Chapter " + (object) chapNum, "Chapter " + (object) chapNum, (int) (chap * 1000.0), settings);
    }

    private void audioSoundEditor1_SilencePosDetectionDone(object sender, SilencePosDetectionDoneEventArgs e)
    {
      if (this.audioSoundEditor1.SilencePositionsGetNum() > 1)
      {
        int num1 = (int) this.audioSoundEditor1.SilencePositionsDetect((short) 800, this.fileToAdjust.IncrementSilence, false);
      }
      else
      {
        int nRangeStart = 0;
        int nRangeEnd = 0;
        for (int index = 0; index < this.audioSoundEditor1.SilencePositionsGetNum(); ++index)
        {
          int range = (int) this.audioSoundEditor1.SilencePositionsGetRange(index, ref nRangeStart, ref nRangeEnd);
          double chap = ((double) nRangeStart + (double) (nRangeEnd - nRangeStart) / 2.0) / 1000.0;
          int num2 = (int) this.AddChapterMarker(chap, index, Color.Red);
          this.fileToAdjust.Offset = this.fileToAdjust.Chapter - this.fileToAdjust.SearchRange + chap;
          int num3 = (int) this.audioSoundEditor1.CloseSound();
          this.fileToAdjust.Cleanup();
          double offset = this.fileToAdjust.Offset;
          this.silenceDetectionComplete = true;
        }
      }
    }

    private void audioSoundEditor1_WaveAnalysisDone(object sender, WaveAnalysisDoneEventArgs e)
    {
      this.g_strNextOperation = "FindSilence";
      this.wavLoadComplete = true;
      int num = (int) this.AddChapterMarker(this.fileToAdjust.SearchRange, 1, Color.White);
      this.PreviewChapter(this.fileToAdjust.SearchRange * 1000.0);
      this.fileToAdjust.Cleanup();
    }

    private void audioSoundEditor1_SoundLoadingDone(object sender, SoundLoadingDoneEventArgs e)
    {
      if (e.bResult)
        return;
      int num = (int) MessageBox.Show("Sound failed to load with the following error code: " + this.audioSoundEditor1.LastError.ToString());
    }

    private void FormChapterAligner_Shown(object sender, EventArgs e)
    {
      this.Init();
      this.AlignChapter();
      this.jobComplete = true;
    }

    private void FormChapterAligner_Resize(object sender, EventArgs e)
    {
      try
      {
        int num = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.Move(this.Picture1.Location.X, this.Picture1.Location.Y, this.Picture1.Size.Width, this.Picture1.Size.Height);
      }
      catch
      {
      }
    }

    private void audioSoundEditor1_WaveAnalyzerLineMoveEnd(object sender, WaveAnalyzerLineEventArgs e)
    {
      int nUniqueId = (int) e.nUniqueID;
      int nPosInMs = e.nPosInMs;
      this.fileToAdjust.Offset = this.fileToAdjust.Chapter - this.fileToAdjust.SearchRange + (double) nPosInMs / 1000.0;
      this.btnApply.Enabled = true;
      this.btnApply.Select();
      this.PreviewChapter((double) nPosInMs);
    }

    private void PreviewChapter(double time)
    {
      int nToPosition = (int) (this.fileToAdjust.SearchRange * 2.0 * 1000.0) - (int) time - 10000;
      int num = (int) this.audioSoundEditor1.PlaySoundRange((int) time, nToPosition);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btnApply_Click(object sender, EventArgs e)
    {
      this.applied = true;
      this.Close();
    }
  }
}
