// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormWaveformSettings
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using AudioSoundEditor;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SoundEditor
{
  public class FormWaveformSettings : Form
  {
    internal AudioSoundEditor.AudioSoundEditor audioSoundEditor1;
    public bool m_bCancel;
    public int m_nOriginalResolution;
    internal Font m_fontText;
    private IContainer components;
    private GroupBox groupBox1;
    private ColorDialog colorDialog1;
    private GroupBox groupBox2;
    private GroupBox groupBox3;
    private Button buttonOK;
    private Button buttonCancel;
    private Label label9;
    private Label label8;
    private Label label7;
    private Label label6;
    private Label label5;
    private Label label4;
    private Label label3;
    private Label label2;
    private Label label1;
    private Label labelWaveformLine;
    private Label labelWaveformBackground;
    private Label labelPositionLine;
    private Label labelPlaybackLine;
    private Label labelRulersBackground;
    private Label labelRulersLines;
    private Label labelRulersText;
    private Label labelScrollbarsBackground;
    private Label labelScrollbarsThumb;
    private ComboBox comboBoxResolution;
    private Label label13;
    private Label label12;
    private Label label11;
    private Label label10;
    private CheckBox checkBoxAppearance3d;
    private ComboBox comboBoxPlaybackLineType;
    private ComboBox comboBoxStereoMode;
    private ComboBox comboBoxPositionLineType;
    private CheckBox checkBoxRulerLeft;
    private CheckBox checkBoxUseHalfColor;
    private CheckBox checkBoxPlaybackLine;
    private CheckBox checkBoxRulerBottom;
    private CheckBox checkBoxRulerTop;
    private CheckBox checkBoxScrollbarBottom;
    private CheckBox checkBoxScrollbarTop;
    private CheckBox checkBoxRulerRight;
    private TextBox textBoxZoom;
    private Label label14;
    private Label label17;
    private Label labelSelectionScroll;
    private Label label15;
    private Label labelSelection;
    private ComboBox comboBoxType;
    private Label label16;
    private ComboBox comboBoxVisibleRangeType;
    private Label label18;

    public FormWaveformSettings()
    {
      this.InitializeComponent();
    }

    private void FormWaveformSettings_Load(object sender, EventArgs e)
    {
      this.comboBoxResolution.Items.Add((object) "Minimum");
      this.comboBoxResolution.Items.Add((object) "Very low");
      this.comboBoxResolution.Items.Add((object) "Low");
      this.comboBoxResolution.Items.Add((object) "Middle");
      this.comboBoxResolution.Items.Add((object) "High");
      this.comboBoxResolution.Items.Add((object) "Very high");
      this.comboBoxResolution.Items.Add((object) "Maximum");
      this.comboBoxStereoMode.Items.Add((object) "Both channels");
      this.comboBoxStereoMode.Items.Add((object) "Left channel");
      this.comboBoxStereoMode.Items.Add((object) "Right channel");
      this.comboBoxStereoMode.Items.Add((object) "Mixed channels");
      this.comboBoxPositionLineType.Items.Add((object) "Solid");
      this.comboBoxPositionLineType.Items.Add((object) "Dashed");
      this.comboBoxPositionLineType.Items.Add((object) "Dotted");
      this.comboBoxPositionLineType.Items.Add((object) "Dash-Dot");
      this.comboBoxPositionLineType.Items.Add((object) "Dash-Dot-Dot");
      this.comboBoxPlaybackLineType.Items.Add((object) "Solid");
      this.comboBoxPlaybackLineType.Items.Add((object) "Dashed");
      this.comboBoxPlaybackLineType.Items.Add((object) "Dotted");
      this.comboBoxPlaybackLineType.Items.Add((object) "Dash-Dot");
      this.comboBoxPlaybackLineType.Items.Add((object) "Dash-Dot-Dot");
      this.comboBoxType.Items.Add((object) "Rectangle");
      this.comboBoxType.Items.Add((object) "Waveform");
      this.comboBoxVisibleRangeType.Items.Add((object) "Horizontal lines on top and bottom");
      this.comboBoxVisibleRangeType.Items.Add((object) "Horizontal line on top");
      this.comboBoxVisibleRangeType.Items.Add((object) "Horizontal line on bottom");
      this.comboBoxVisibleRangeType.Items.Add((object) "Inner transparent glass");
      this.comboBoxVisibleRangeType.Items.Add((object) "Outer transparent glass");
      WANALYZER_GENERAL_SETTINGS settings1 = new WANALYZER_GENERAL_SETTINGS();
      int num1 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsGeneralGet(ref settings1);
      this.textBoxZoom.Text = settings1.fHorizontalZoomFactor.ToString();
      this.m_nOriginalResolution = (int) settings1.nResolution;
      this.comboBoxResolution.SelectedIndex = this.m_nOriginalResolution - 100;
      this.checkBoxAppearance3d.Checked = settings1.bAppearance3d;
      this.labelPositionLine.BackColor = settings1.colorPositionLine;
      this.labelPlaybackLine.BackColor = settings1.colorPlaybackLine;
      this.checkBoxPlaybackLine.Checked = settings1.bPlaybackLineVisible;
      this.comboBoxPositionLineType.SelectedIndex = (int) settings1.nPositionLineDashStyle;
      this.comboBoxPlaybackLineType.SelectedIndex = (int) settings1.nPlaybackLineDashStyle;
      WANALYZER_WAVEFORM_SETTINGS settings2 = new WANALYZER_WAVEFORM_SETTINGS();
      int num2 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsWaveGet(ref settings2);
      this.labelWaveformLine.BackColor = settings2.colorWaveLinePeak;
      this.labelWaveformLine.BackColor = settings2.colorWaveLineCenter;
      this.labelWaveformBackground.BackColor = settings2.colorWaveBackground;
      this.comboBoxStereoMode.SelectedIndex = (int) settings2.nStereoVisualizationMode;
      this.checkBoxUseHalfColor.Checked = settings2.bUseHalfColorsForPeaks;
      this.labelSelection.BackColor = settings2.colorTransparentGlass;
      WANALYZER_RULERS_SETTINGS settings3 = new WANALYZER_RULERS_SETTINGS();
      int num3 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsRulersGet(ref settings3, ref this.m_fontText);
      this.checkBoxRulerLeft.Checked = settings3.bVisibleLeft;
      this.checkBoxRulerRight.Checked = settings3.bVisibleRight;
      this.checkBoxRulerTop.Checked = settings3.bVisibleTop;
      this.checkBoxRulerBottom.Checked = settings3.bVisibleBottom;
      this.labelRulersBackground.BackColor = settings3.colorBackground;
      this.labelRulersLines.BackColor = settings3.colorTicks;
      this.labelRulersText.BackColor = settings3.colorText;
      WANALYZER_SCROLLBARS_SETTINGS settings4 = new WANALYZER_SCROLLBARS_SETTINGS();
      int num4 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsScrollbarsGet(ref settings4);
      this.labelScrollbarsBackground.BackColor = settings4.colorBackground;
      this.labelScrollbarsThumb.BackColor = settings4.colorThumb;
      this.checkBoxScrollbarTop.Checked = settings4.bVisibleTop;
      this.checkBoxScrollbarBottom.Checked = settings4.bVisibleBottom;
      this.labelSelectionScroll.BackColor = settings4.colorVisibleRange;
      this.comboBoxType.SelectedIndex = (int) settings4.nType;
      this.comboBoxVisibleRangeType.SelectedIndex = (int) settings4.nVisibleRangeType;
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      WANALYZER_GENERAL_SETTINGS settings1 = new WANALYZER_GENERAL_SETTINGS();
      int num1 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsGeneralGet(ref settings1);
      settings1.fHorizontalZoomFactor = Convert.ToSingle(this.textBoxZoom.Text);
      settings1.nResolution = (enumAnalyzerResolutions) (this.comboBoxResolution.SelectedIndex + 100);
      settings1.bAppearance3d = this.checkBoxAppearance3d.Checked;
      settings1.colorPositionLine = this.labelPositionLine.BackColor;
      settings1.colorPlaybackLine = this.labelPlaybackLine.BackColor;
      settings1.bPlaybackLineVisible = this.checkBoxPlaybackLine.Checked;
      settings1.nPositionLineDashStyle = (enumWaveformLineDashStyles) this.comboBoxPositionLineType.SelectedIndex;
      settings1.nPlaybackLineDashStyle = (enumWaveformLineDashStyles) this.comboBoxPlaybackLineType.SelectedIndex;
      WANALYZER_WAVEFORM_SETTINGS settings2 = new WANALYZER_WAVEFORM_SETTINGS();
      int num2 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsWaveGet(ref settings2);
      settings2.colorWaveLinePeak = this.labelWaveformLine.BackColor;
      settings2.colorWaveLineCenter = this.labelWaveformLine.BackColor;
      settings2.colorWaveBackground = this.labelWaveformBackground.BackColor;
      settings2.nStereoVisualizationMode = (enumWaveformStereoModes) this.comboBoxStereoMode.SelectedIndex;
      settings2.bUseHalfColorsForPeaks = this.checkBoxUseHalfColor.Checked;
      settings2.colorTransparentGlass = this.labelSelection.BackColor;
      WANALYZER_RULERS_SETTINGS settings3 = new WANALYZER_RULERS_SETTINGS();
      int num3 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsRulersGet(ref settings3, ref this.m_fontText);
      settings3.bVisibleLeft = this.checkBoxRulerLeft.Checked;
      settings3.bVisibleRight = this.checkBoxRulerRight.Checked;
      settings3.bVisibleTop = this.checkBoxRulerTop.Checked;
      settings3.bVisibleBottom = this.checkBoxRulerBottom.Checked;
      settings3.colorBackground = this.labelRulersBackground.BackColor;
      settings3.colorTicks = this.labelRulersLines.BackColor;
      settings3.colorText = this.labelRulersText.BackColor;
      WANALYZER_SCROLLBARS_SETTINGS settings4 = new WANALYZER_SCROLLBARS_SETTINGS();
      int num4 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsScrollbarsGet(ref settings4);
      settings4.colorBackground = this.labelScrollbarsBackground.BackColor;
      settings4.colorThumb = this.labelScrollbarsThumb.BackColor;
      settings4.bVisibleTop = this.checkBoxScrollbarTop.Checked;
      settings4.bVisibleBottom = this.checkBoxScrollbarBottom.Checked;
      settings4.colorVisibleRange = this.labelSelectionScroll.BackColor;
      settings4.nType = (enumWaveScrollbarType) this.comboBoxType.SelectedIndex;
      settings4.nVisibleRangeType = (enumScrollbarWaveVisibleRangeType) this.comboBoxVisibleRangeType.SelectedIndex;
      settings4.nHeightInPixels = settings4.nType != enumWaveScrollbarType.WSCROLLBAR_TYPE_WAVEFORM ? (short) 10 : (short) 40;
      int num5 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsGeneralSet(settings1);
      int num6 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsWaveSet(settings2);
      int num7 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsRulersSet(settings3, this.m_fontText);
      int num8 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.SettingsScrollbarsSet(settings4);
      if ((enumAnalyzerResolutions) this.m_nOriginalResolution != settings1.nResolution)
      {
        int num9 = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.AnalyzeFullSound();
      }
      this.m_bCancel = false;
      this.Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.m_bCancel = true;
      this.Close();
    }

    private void labelWaveformLine_Click(object sender, EventArgs e)
    {
      this.colorDialog1.Color = this.labelWaveformLine.BackColor;
      if (this.colorDialog1.ShowDialog() != DialogResult.OK)
        return;
      this.labelWaveformLine.BackColor = this.colorDialog1.Color;
    }

    private void labelWaveformBackground_Click(object sender, EventArgs e)
    {
      this.colorDialog1.Color = this.labelWaveformBackground.BackColor;
      if (this.colorDialog1.ShowDialog() != DialogResult.OK)
        return;
      this.labelWaveformBackground.BackColor = this.colorDialog1.Color;
    }

    private void labelPositionLine_Click(object sender, EventArgs e)
    {
      this.colorDialog1.Color = this.labelPositionLine.BackColor;
      if (this.colorDialog1.ShowDialog() != DialogResult.OK)
        return;
      this.labelPositionLine.BackColor = this.colorDialog1.Color;
    }

    private void labelPlaybackLine_Click(object sender, EventArgs e)
    {
      this.colorDialog1.Color = this.labelPlaybackLine.BackColor;
      if (this.colorDialog1.ShowDialog() != DialogResult.OK)
        return;
      this.labelPlaybackLine.BackColor = this.colorDialog1.Color;
    }

    private void labelRulersBackground_Click(object sender, EventArgs e)
    {
      this.colorDialog1.Color = this.labelRulersBackground.BackColor;
      if (this.colorDialog1.ShowDialog() != DialogResult.OK)
        return;
      this.labelRulersBackground.BackColor = this.colorDialog1.Color;
    }

    private void labelRulersLines_Click(object sender, EventArgs e)
    {
      this.colorDialog1.Color = this.labelRulersLines.BackColor;
      if (this.colorDialog1.ShowDialog() != DialogResult.OK)
        return;
      this.labelRulersLines.BackColor = this.colorDialog1.Color;
    }

    private void labelRulersText_Click(object sender, EventArgs e)
    {
      this.colorDialog1.Color = this.labelRulersText.BackColor;
      if (this.colorDialog1.ShowDialog() != DialogResult.OK)
        return;
      this.labelRulersText.BackColor = this.colorDialog1.Color;
    }

    private void labelScrollbarsBackground_Click(object sender, EventArgs e)
    {
      this.colorDialog1.Color = this.labelScrollbarsBackground.BackColor;
      if (this.colorDialog1.ShowDialog() != DialogResult.OK)
        return;
      this.labelScrollbarsBackground.BackColor = this.colorDialog1.Color;
    }

    private void labelScrollbarsThumb_Click(object sender, EventArgs e)
    {
      this.colorDialog1.Color = this.labelScrollbarsThumb.BackColor;
      if (this.colorDialog1.ShowDialog() != DialogResult.OK)
        return;
      this.labelScrollbarsThumb.BackColor = this.colorDialog1.Color;
    }

    private void labelSelection_Click(object sender, EventArgs e)
    {
      this.colorDialog1.Color = this.labelSelection.BackColor;
      if (this.colorDialog1.ShowDialog() != DialogResult.OK)
        return;
      this.labelSelection.BackColor = this.colorDialog1.Color;
    }

    private void labelSelectionScroll_Click(object sender, EventArgs e)
    {
      this.colorDialog1.Color = this.labelSelectionScroll.BackColor;
      if (this.colorDialog1.ShowDialog() != DialogResult.OK)
        return;
      this.labelSelectionScroll.BackColor = this.colorDialog1.Color;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.labelWaveformLine = new Label();
      this.labelWaveformBackground = new Label();
      this.labelPositionLine = new Label();
      this.labelPlaybackLine = new Label();
      this.labelRulersBackground = new Label();
      this.labelRulersLines = new Label();
      this.labelRulersText = new Label();
      this.labelScrollbarsBackground = new Label();
      this.labelScrollbarsThumb = new Label();
      this.groupBox1 = new GroupBox();
      this.label9 = new Label();
      this.label8 = new Label();
      this.label7 = new Label();
      this.label6 = new Label();
      this.label5 = new Label();
      this.label4 = new Label();
      this.label3 = new Label();
      this.label2 = new Label();
      this.label1 = new Label();
      this.colorDialog1 = new ColorDialog();
      this.groupBox2 = new GroupBox();
      this.textBoxZoom = new TextBox();
      this.label14 = new Label();
      this.comboBoxPlaybackLineType = new ComboBox();
      this.comboBoxStereoMode = new ComboBox();
      this.comboBoxPositionLineType = new ComboBox();
      this.comboBoxResolution = new ComboBox();
      this.label13 = new Label();
      this.label12 = new Label();
      this.label11 = new Label();
      this.label10 = new Label();
      this.checkBoxAppearance3d = new CheckBox();
      this.groupBox3 = new GroupBox();
      this.checkBoxUseHalfColor = new CheckBox();
      this.checkBoxPlaybackLine = new CheckBox();
      this.checkBoxRulerBottom = new CheckBox();
      this.checkBoxRulerTop = new CheckBox();
      this.checkBoxScrollbarBottom = new CheckBox();
      this.checkBoxScrollbarTop = new CheckBox();
      this.checkBoxRulerRight = new CheckBox();
      this.checkBoxRulerLeft = new CheckBox();
      this.buttonOK = new Button();
      this.buttonCancel = new Button();
      this.label15 = new Label();
      this.labelSelection = new Label();
      this.label17 = new Label();
      this.labelSelectionScroll = new Label();
      this.comboBoxType = new ComboBox();
      this.label16 = new Label();
      this.comboBoxVisibleRangeType = new ComboBox();
      this.label18 = new Label();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.SuspendLayout();
      this.labelWaveformLine.BackColor = Color.White;
      this.labelWaveformLine.BorderStyle = BorderStyle.FixedSingle;
      this.labelWaveformLine.ForeColor = SystemColors.ControlText;
      this.labelWaveformLine.Location = new Point(166, 28);
      this.labelWaveformLine.Name = "labelWaveformLine";
      this.labelWaveformLine.Size = new Size(16, 16);
      this.labelWaveformLine.TabIndex = 0;
      this.labelWaveformLine.Click += new System.EventHandler(this.labelWaveformLine_Click);
      this.labelWaveformBackground.BackColor = Color.White;
      this.labelWaveformBackground.BorderStyle = BorderStyle.FixedSingle;
      this.labelWaveformBackground.ForeColor = Color.Black;
      this.labelWaveformBackground.Location = new Point(166, 59);
      this.labelWaveformBackground.Name = "labelWaveformBackground";
      this.labelWaveformBackground.Size = new Size(16, 16);
      this.labelWaveformBackground.TabIndex = 1;
      this.labelWaveformBackground.Click += new System.EventHandler(this.labelWaveformBackground_Click);
      this.labelPositionLine.BackColor = Color.White;
      this.labelPositionLine.BorderStyle = BorderStyle.FixedSingle;
      this.labelPositionLine.ForeColor = Color.Black;
      this.labelPositionLine.Location = new Point(166, 90);
      this.labelPositionLine.Name = "labelPositionLine";
      this.labelPositionLine.Size = new Size(16, 16);
      this.labelPositionLine.TabIndex = 2;
      this.labelPositionLine.Click += new System.EventHandler(this.labelPositionLine_Click);
      this.labelPlaybackLine.BackColor = Color.White;
      this.labelPlaybackLine.BorderStyle = BorderStyle.FixedSingle;
      this.labelPlaybackLine.ForeColor = Color.Black;
      this.labelPlaybackLine.Location = new Point(166, 121);
      this.labelPlaybackLine.Name = "labelPlaybackLine";
      this.labelPlaybackLine.Size = new Size(16, 16);
      this.labelPlaybackLine.TabIndex = 3;
      this.labelPlaybackLine.Click += new System.EventHandler(this.labelPlaybackLine_Click);
      this.labelRulersBackground.BackColor = Color.White;
      this.labelRulersBackground.BorderStyle = BorderStyle.FixedSingle;
      this.labelRulersBackground.ForeColor = Color.Black;
      this.labelRulersBackground.Location = new Point(166, 152);
      this.labelRulersBackground.Name = "labelRulersBackground";
      this.labelRulersBackground.Size = new Size(16, 16);
      this.labelRulersBackground.TabIndex = 4;
      this.labelRulersBackground.Click += new System.EventHandler(this.labelRulersBackground_Click);
      this.labelRulersLines.BackColor = Color.White;
      this.labelRulersLines.BorderStyle = BorderStyle.FixedSingle;
      this.labelRulersLines.ForeColor = Color.Black;
      this.labelRulersLines.Location = new Point(166, 183);
      this.labelRulersLines.Name = "labelRulersLines";
      this.labelRulersLines.Size = new Size(16, 16);
      this.labelRulersLines.TabIndex = 5;
      this.labelRulersLines.Click += new System.EventHandler(this.labelRulersLines_Click);
      this.labelRulersText.BackColor = Color.White;
      this.labelRulersText.BorderStyle = BorderStyle.FixedSingle;
      this.labelRulersText.ForeColor = Color.Black;
      this.labelRulersText.Location = new Point(166, 214);
      this.labelRulersText.Name = "labelRulersText";
      this.labelRulersText.Size = new Size(16, 16);
      this.labelRulersText.TabIndex = 6;
      this.labelRulersText.Click += new System.EventHandler(this.labelRulersText_Click);
      this.labelScrollbarsBackground.BackColor = Color.White;
      this.labelScrollbarsBackground.BorderStyle = BorderStyle.FixedSingle;
      this.labelScrollbarsBackground.ForeColor = Color.Black;
      this.labelScrollbarsBackground.Location = new Point(166, 245);
      this.labelScrollbarsBackground.Name = "labelScrollbarsBackground";
      this.labelScrollbarsBackground.Size = new Size(16, 16);
      this.labelScrollbarsBackground.TabIndex = 7;
      this.labelScrollbarsBackground.Click += new System.EventHandler(this.labelScrollbarsBackground_Click);
      this.labelScrollbarsThumb.BackColor = Color.White;
      this.labelScrollbarsThumb.BorderStyle = BorderStyle.FixedSingle;
      this.labelScrollbarsThumb.ForeColor = Color.Black;
      this.labelScrollbarsThumb.Location = new Point(166, 276);
      this.labelScrollbarsThumb.Name = "labelScrollbarsThumb";
      this.labelScrollbarsThumb.Size = new Size(16, 16);
      this.labelScrollbarsThumb.TabIndex = 8;
      this.labelScrollbarsThumb.Click += new System.EventHandler(this.labelScrollbarsThumb_Click);
      this.groupBox1.Controls.Add((Control) this.label17);
      this.groupBox1.Controls.Add((Control) this.labelSelectionScroll);
      this.groupBox1.Controls.Add((Control) this.label15);
      this.groupBox1.Controls.Add((Control) this.labelSelection);
      this.groupBox1.Controls.Add((Control) this.label9);
      this.groupBox1.Controls.Add((Control) this.label8);
      this.groupBox1.Controls.Add((Control) this.label7);
      this.groupBox1.Controls.Add((Control) this.label6);
      this.groupBox1.Controls.Add((Control) this.label5);
      this.groupBox1.Controls.Add((Control) this.label4);
      this.groupBox1.Controls.Add((Control) this.label3);
      this.groupBox1.Controls.Add((Control) this.label2);
      this.groupBox1.Controls.Add((Control) this.label1);
      this.groupBox1.Controls.Add((Control) this.labelScrollbarsThumb);
      this.groupBox1.Controls.Add((Control) this.labelScrollbarsBackground);
      this.groupBox1.Controls.Add((Control) this.labelRulersText);
      this.groupBox1.Controls.Add((Control) this.labelRulersLines);
      this.groupBox1.Controls.Add((Control) this.labelRulersBackground);
      this.groupBox1.Controls.Add((Control) this.labelPlaybackLine);
      this.groupBox1.Controls.Add((Control) this.labelPositionLine);
      this.groupBox1.Controls.Add((Control) this.labelWaveformBackground);
      this.groupBox1.Controls.Add((Control) this.labelWaveformLine);
      this.groupBox1.Location = new Point(12, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(207, 366);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Colors (click color rectangle to edit)";
      this.label9.Location = new Point(17, 276);
      this.label9.Name = "label9";
      this.label9.Size = new Size(138, 14);
      this.label9.TabIndex = 17;
      this.label9.Text = "Scrollbars thumb";
      this.label9.TextAlign = ContentAlignment.MiddleRight;
      this.label8.Location = new Point(17, 245);
      this.label8.Name = "label8";
      this.label8.Size = new Size(138, 14);
      this.label8.TabIndex = 16;
      this.label8.Text = "Scrollbars background";
      this.label8.TextAlign = ContentAlignment.MiddleRight;
      this.label7.Location = new Point(17, 214);
      this.label7.Name = "label7";
      this.label7.Size = new Size(138, 14);
      this.label7.TabIndex = 15;
      this.label7.Text = "Rulers text";
      this.label7.TextAlign = ContentAlignment.MiddleRight;
      this.label6.Location = new Point(17, 183);
      this.label6.Name = "label6";
      this.label6.Size = new Size(138, 14);
      this.label6.TabIndex = 14;
      this.label6.Text = "Rulers lines";
      this.label6.TextAlign = ContentAlignment.MiddleRight;
      this.label5.Location = new Point(17, 152);
      this.label5.Name = "label5";
      this.label5.Size = new Size(138, 14);
      this.label5.TabIndex = 13;
      this.label5.Text = "Rulers background";
      this.label5.TextAlign = ContentAlignment.MiddleRight;
      this.label4.Location = new Point(17, 121);
      this.label4.Name = "label4";
      this.label4.Size = new Size(138, 14);
      this.label4.TabIndex = 12;
      this.label4.Text = "Playback line";
      this.label4.TextAlign = ContentAlignment.MiddleRight;
      this.label3.Location = new Point(17, 90);
      this.label3.Name = "label3";
      this.label3.Size = new Size(138, 14);
      this.label3.TabIndex = 11;
      this.label3.Text = "Position line";
      this.label3.TextAlign = ContentAlignment.MiddleRight;
      this.label2.Location = new Point(17, 59);
      this.label2.Name = "label2";
      this.label2.Size = new Size(138, 14);
      this.label2.TabIndex = 10;
      this.label2.Text = "Waveform background";
      this.label2.TextAlign = ContentAlignment.MiddleRight;
      this.label1.Location = new Point(17, 29);
      this.label1.Name = "label1";
      this.label1.Size = new Size(138, 14);
      this.label1.TabIndex = 9;
      this.label1.Text = "Waveform line";
      this.label1.TextAlign = ContentAlignment.MiddleRight;
      this.groupBox2.Controls.Add((Control) this.comboBoxVisibleRangeType);
      this.groupBox2.Controls.Add((Control) this.label18);
      this.groupBox2.Controls.Add((Control) this.comboBoxType);
      this.groupBox2.Controls.Add((Control) this.label16);
      this.groupBox2.Controls.Add((Control) this.textBoxZoom);
      this.groupBox2.Controls.Add((Control) this.label14);
      this.groupBox2.Controls.Add((Control) this.comboBoxPlaybackLineType);
      this.groupBox2.Controls.Add((Control) this.comboBoxStereoMode);
      this.groupBox2.Controls.Add((Control) this.comboBoxPositionLineType);
      this.groupBox2.Controls.Add((Control) this.comboBoxResolution);
      this.groupBox2.Controls.Add((Control) this.label13);
      this.groupBox2.Controls.Add((Control) this.label12);
      this.groupBox2.Controls.Add((Control) this.label11);
      this.groupBox2.Controls.Add((Control) this.label10);
      this.groupBox2.Controls.Add((Control) this.checkBoxAppearance3d);
      this.groupBox2.Location = new Point(225, 12);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new Size(410, 208);
      this.groupBox2.TabIndex = 1;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Visualization";
      this.textBoxZoom.Location = new Point(344, 23);
      this.textBoxZoom.Name = "textBoxZoom";
      this.textBoxZoom.Size = new Size(43, 20);
      this.textBoxZoom.TabIndex = 10;
      this.label14.AutoSize = true;
      this.label14.Location = new Point(219, 27);
      this.label14.Name = "label14";
      this.label14.Size = new Size(112, 13);
      this.label14.TabIndex = 9;
      this.label14.Text = "Horizontal zoom factor";
      this.comboBoxPlaybackLineType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBoxPlaybackLineType.FormattingEnabled = true;
      this.comboBoxPlaybackLineType.Location = new Point(212, 120);
      this.comboBoxPlaybackLineType.Name = "comboBoxPlaybackLineType";
      this.comboBoxPlaybackLineType.Size = new Size(175, 21);
      this.comboBoxPlaybackLineType.TabIndex = 8;
      this.comboBoxStereoMode.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBoxStereoMode.FormattingEnabled = true;
      this.comboBoxStereoMode.Location = new Point(18, 120);
      this.comboBoxStereoMode.Name = "comboBoxStereoMode";
      this.comboBoxStereoMode.Size = new Size(169, 21);
      this.comboBoxStereoMode.TabIndex = 7;
      this.comboBoxPositionLineType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBoxPositionLineType.FormattingEnabled = true;
      this.comboBoxPositionLineType.Location = new Point(212, 75);
      this.comboBoxPositionLineType.Name = "comboBoxPositionLineType";
      this.comboBoxPositionLineType.Size = new Size(175, 21);
      this.comboBoxPositionLineType.TabIndex = 6;
      this.comboBoxResolution.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBoxResolution.FormattingEnabled = true;
      this.comboBoxResolution.Location = new Point(19, 75);
      this.comboBoxResolution.Name = "comboBoxResolution";
      this.comboBoxResolution.Size = new Size(169, 21);
      this.comboBoxResolution.TabIndex = 5;
      this.label13.AutoSize = true;
      this.label13.Location = new Point(211, 104);
      this.label13.Name = "label13";
      this.label13.Size = new Size(147, 13);
      this.label13.TabIndex = 4;
      this.label13.Text = "Type of playback position line";
      this.label12.AutoSize = true;
      this.label12.Location = new Point(15, 104);
      this.label12.Name = "label12";
      this.label12.Size = new Size((int) sbyte.MaxValue, 13);
      this.label12.TabIndex = 3;
      this.label12.Text = "Stereo visualization mode";
      this.label11.AutoSize = true;
      this.label11.Location = new Point(212, 59);
      this.label11.Name = "label11";
      this.label11.Size = new Size(146, 13);
      this.label11.TabIndex = 2;
      this.label11.Text = "Type of selection position line";
      this.label10.AutoSize = true;
      this.label10.Location = new Point(15, 59);
      this.label10.Name = "label10";
      this.label10.Size = new Size(57, 13);
      this.label10.TabIndex = 1;
      this.label10.Text = "Resolution";
      this.checkBoxAppearance3d.AutoSize = true;
      this.checkBoxAppearance3d.Location = new Point(19, 29);
      this.checkBoxAppearance3d.Name = "checkBoxAppearance3d";
      this.checkBoxAppearance3d.Size = new Size(101, 17);
      this.checkBoxAppearance3d.TabIndex = 0;
      this.checkBoxAppearance3d.Text = "Appearance 3D";
      this.checkBoxAppearance3d.UseVisualStyleBackColor = true;
      this.groupBox3.Controls.Add((Control) this.checkBoxUseHalfColor);
      this.groupBox3.Controls.Add((Control) this.checkBoxPlaybackLine);
      this.groupBox3.Controls.Add((Control) this.checkBoxRulerBottom);
      this.groupBox3.Controls.Add((Control) this.checkBoxRulerTop);
      this.groupBox3.Controls.Add((Control) this.checkBoxScrollbarBottom);
      this.groupBox3.Controls.Add((Control) this.checkBoxScrollbarTop);
      this.groupBox3.Controls.Add((Control) this.checkBoxRulerRight);
      this.groupBox3.Controls.Add((Control) this.checkBoxRulerLeft);
      this.groupBox3.Location = new Point(225, 226);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new Size(409, 149);
      this.groupBox3.TabIndex = 2;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Visibility";
      this.checkBoxUseHalfColor.AutoSize = true;
      this.checkBoxUseHalfColor.Location = new Point(211, 119);
      this.checkBoxUseHalfColor.Name = "checkBoxUseHalfColor";
      this.checkBoxUseHalfColor.Size = new Size(138, 17);
      this.checkBoxUseHalfColor.TabIndex = 7;
      this.checkBoxUseHalfColor.Text = "Use half color for peaks";
      this.checkBoxUseHalfColor.UseVisualStyleBackColor = true;
      this.checkBoxPlaybackLine.AutoSize = true;
      this.checkBoxPlaybackLine.Location = new Point(211, 88);
      this.checkBoxPlaybackLine.Name = "checkBoxPlaybackLine";
      this.checkBoxPlaybackLine.Size = new Size(125, 17);
      this.checkBoxPlaybackLine.TabIndex = 6;
      this.checkBoxPlaybackLine.Text = "Display playback line";
      this.checkBoxPlaybackLine.UseVisualStyleBackColor = true;
      this.checkBoxRulerBottom.AutoSize = true;
      this.checkBoxRulerBottom.Location = new Point(211, 57);
      this.checkBoxRulerBottom.Name = "checkBoxRulerBottom";
      this.checkBoxRulerBottom.Size = new Size(140, 17);
      this.checkBoxRulerBottom.TabIndex = 5;
      this.checkBoxRulerBottom.Text = "Display time ruler bottom";
      this.checkBoxRulerBottom.UseVisualStyleBackColor = true;
      this.checkBoxRulerTop.AutoSize = true;
      this.checkBoxRulerTop.Location = new Point(211, 26);
      this.checkBoxRulerTop.Name = "checkBoxRulerTop";
      this.checkBoxRulerTop.Size = new Size(123, 17);
      this.checkBoxRulerTop.TabIndex = 4;
      this.checkBoxRulerTop.Text = "Display time ruler top";
      this.checkBoxRulerTop.UseVisualStyleBackColor = true;
      this.checkBoxScrollbarBottom.AutoSize = true;
      this.checkBoxScrollbarBottom.Location = new Point(17, 119);
      this.checkBoxScrollbarBottom.Name = "checkBoxScrollbarBottom";
      this.checkBoxScrollbarBottom.Size = new Size(137, 17);
      this.checkBoxScrollbarBottom.TabIndex = 3;
      this.checkBoxScrollbarBottom.Text = "Display scrollbar bottom";
      this.checkBoxScrollbarBottom.UseVisualStyleBackColor = true;
      this.checkBoxScrollbarTop.AutoSize = true;
      this.checkBoxScrollbarTop.Location = new Point(17, 88);
      this.checkBoxScrollbarTop.Name = "checkBoxScrollbarTop";
      this.checkBoxScrollbarTop.Size = new Size(120, 17);
      this.checkBoxScrollbarTop.TabIndex = 2;
      this.checkBoxScrollbarTop.Text = "Display scrollbar top";
      this.checkBoxScrollbarTop.UseVisualStyleBackColor = true;
      this.checkBoxRulerRight.AutoSize = true;
      this.checkBoxRulerRight.Location = new Point(17, 57);
      this.checkBoxRulerRight.Name = "checkBoxRulerRight";
      this.checkBoxRulerRight.Size = new Size(154, 17);
      this.checkBoxRulerRight.TabIndex = 1;
      this.checkBoxRulerRight.Text = "Display amplitude ruler right";
      this.checkBoxRulerRight.UseVisualStyleBackColor = true;
      this.checkBoxRulerLeft.AutoSize = true;
      this.checkBoxRulerLeft.Location = new Point(18, 26);
      this.checkBoxRulerLeft.Name = "checkBoxRulerLeft";
      this.checkBoxRulerLeft.Size = new Size(148, 17);
      this.checkBoxRulerLeft.TabIndex = 0;
      this.checkBoxRulerLeft.Text = "Display amplitude ruler left";
      this.checkBoxRulerLeft.UseVisualStyleBackColor = true;
      this.buttonOK.Location = new Point(222, 384);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new Size(94, 25);
      this.buttonOK.TabIndex = 3;
      this.buttonOK.Text = "OK";
      this.buttonOK.UseVisualStyleBackColor = true;
      this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
      this.buttonCancel.Location = new Point(334, 384);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(94, 25);
      this.buttonCancel.TabIndex = 4;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      this.label15.Location = new Point(17, 306);
      this.label15.Name = "label15";
      this.label15.Size = new Size(138, 14);
      this.label15.TabIndex = 19;
      this.label15.Text = "Selection 3D glass";
      this.label15.TextAlign = ContentAlignment.MiddleRight;
      this.labelSelection.BackColor = Color.White;
      this.labelSelection.BorderStyle = BorderStyle.FixedSingle;
      this.labelSelection.ForeColor = Color.Black;
      this.labelSelection.Location = new Point(166, 306);
      this.labelSelection.Name = "labelSelection";
      this.labelSelection.Size = new Size(16, 16);
      this.labelSelection.TabIndex = 18;
      this.labelSelection.Click += new System.EventHandler(this.labelSelection_Click);
      this.label17.Location = new Point(17, 336);
      this.label17.Name = "label17";
      this.label17.Size = new Size(138, 14);
      this.label17.TabIndex = 21;
      this.label17.Text = "Scrollbars visible range";
      this.label17.TextAlign = ContentAlignment.MiddleRight;
      this.labelSelectionScroll.BackColor = Color.White;
      this.labelSelectionScroll.BorderStyle = BorderStyle.FixedSingle;
      this.labelSelectionScroll.ForeColor = Color.Black;
      this.labelSelectionScroll.Location = new Point(166, 336);
      this.labelSelectionScroll.Name = "labelSelectionScroll";
      this.labelSelectionScroll.Size = new Size(16, 16);
      this.labelSelectionScroll.TabIndex = 20;
      this.labelSelectionScroll.Click += new System.EventHandler(this.labelSelectionScroll_Click);
      this.comboBoxType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBoxType.FormattingEnabled = true;
      this.comboBoxType.Location = new Point(20, 168);
      this.comboBoxType.Name = "comboBoxType";
      this.comboBoxType.Size = new Size(169, 21);
      this.comboBoxType.TabIndex = 18;
      this.label16.AutoSize = true;
      this.label16.Location = new Point(16, 152);
      this.label16.Name = "label16";
      this.label16.Size = new Size(76, 13);
      this.label16.TabIndex = 17;
      this.label16.Text = "Scrollbars type";
      this.comboBoxVisibleRangeType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBoxVisibleRangeType.FormattingEnabled = true;
      this.comboBoxVisibleRangeType.Location = new Point(213, 168);
      this.comboBoxVisibleRangeType.Name = "comboBoxVisibleRangeType";
      this.comboBoxVisibleRangeType.Size = new Size(175, 21);
      this.comboBoxVisibleRangeType.TabIndex = 21;
      this.label18.AutoSize = true;
      this.label18.Location = new Point(209, 152);
      this.label18.Name = "label18";
      this.label18.Size = new Size(150, 13);
      this.label18.TabIndex = 20;
      this.label18.Text = "Visible range visualization type";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(650, 433);
      this.ControlBox = false;
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.buttonOK);
      this.Controls.Add((Control) this.groupBox3);
      this.Controls.Add((Control) this.groupBox2);
      this.Controls.Add((Control) this.groupBox1);
      this.Name = nameof (FormWaveformSettings);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Waveform analyzer settings";
      this.Load += new System.EventHandler(this.FormWaveformSettings_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
