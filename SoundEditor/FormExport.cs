// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormExport
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using AudioSoundEditor;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SoundEditor
{
  public class FormExport : Form
  {
    public GroupBox Frame2;
    public GroupBox FrameFrequencies;
    public GroupBox Frame5;
    public GroupBox Frame4;
    public RadioButton radioButton16bits;
    public RadioButton radioButton8bits;
    public RadioButton radioButtonStereo;
    public RadioButton radioButtonMono;
    public RadioButton radioButton48000;
    public RadioButton radioButton44100;
    public RadioButton radioButton22050;
    public RadioButton radioButton11025;
    public RadioButton radioButtonExportSelection;
    public RadioButton radioButtonExportFull;
    public Button buttonCancel;
    public Button buttonOK;
    public GroupBox FrameResampleSettings;
    private ComboBox comboBoxFormats;
    public RadioButton radioButton32bits;
    public RadioButton radioButton32bitsFloat;
    public GroupBox FrameBitsPerSample;
    internal AudioSoundEditor.AudioSoundEditor audioSoundEditor1;
    protected int m_nBeginSelectionInMs;
    protected int m_nEndSelectionInMs;
    public string m_strExportPathname;
    private short m_ACMCodec;
    private short m_ACMCodecFormat;
    public RadioButton radioButtonSurround;
    private Button button1;
    private TextBox txtOutputFile;
    private Label label1;
    private GroupBox grpCodecSettings;
    private ComboBox cmbBitrates;
    private RadioButton rbCBR;
    private RadioButton rbVBR;
    private TrackBar trackBar1;
    private GroupBox grpPresets;
    private RadioButton rbMedium;
    private TextBox txtVBR;
    private RadioButton rbExtreme;
    private RadioButton rbStandard;
    private RadioButton rbInsane;
    private Container components;

    public FormExport()
    {
      this.InitializeComponent();
      this.cmbBitrates.SelectedIndex = this.cmbBitrates.Items.IndexOf((object) "128");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.FrameResampleSettings = new GroupBox();
      this.FrameBitsPerSample = new GroupBox();
      this.radioButton32bitsFloat = new RadioButton();
      this.radioButton32bits = new RadioButton();
      this.radioButton16bits = new RadioButton();
      this.radioButton8bits = new RadioButton();
      this.Frame2 = new GroupBox();
      this.radioButtonSurround = new RadioButton();
      this.radioButtonStereo = new RadioButton();
      this.radioButtonMono = new RadioButton();
      this.FrameFrequencies = new GroupBox();
      this.radioButton48000 = new RadioButton();
      this.radioButton44100 = new RadioButton();
      this.radioButton22050 = new RadioButton();
      this.radioButton11025 = new RadioButton();
      this.Frame5 = new GroupBox();
      this.radioButtonExportSelection = new RadioButton();
      this.radioButtonExportFull = new RadioButton();
      this.buttonCancel = new Button();
      this.Frame4 = new GroupBox();
      this.comboBoxFormats = new ComboBox();
      this.buttonOK = new Button();
      this.button1 = new Button();
      this.txtOutputFile = new TextBox();
      this.label1 = new Label();
      this.grpCodecSettings = new GroupBox();
      this.txtVBR = new TextBox();
      this.rbVBR = new RadioButton();
      this.trackBar1 = new TrackBar();
      this.cmbBitrates = new ComboBox();
      this.rbCBR = new RadioButton();
      this.grpPresets = new GroupBox();
      this.rbExtreme = new RadioButton();
      this.rbStandard = new RadioButton();
      this.rbMedium = new RadioButton();
      this.rbInsane = new RadioButton();
      this.FrameResampleSettings.SuspendLayout();
      this.FrameBitsPerSample.SuspendLayout();
      this.Frame2.SuspendLayout();
      this.FrameFrequencies.SuspendLayout();
      this.Frame5.SuspendLayout();
      this.Frame4.SuspendLayout();
      this.grpCodecSettings.SuspendLayout();
      this.trackBar1.BeginInit();
      this.grpPresets.SuspendLayout();
      this.SuspendLayout();
      this.FrameResampleSettings.BackColor = SystemColors.Control;
      this.FrameResampleSettings.Controls.Add((Control) this.FrameBitsPerSample);
      this.FrameResampleSettings.Controls.Add((Control) this.Frame2);
      this.FrameResampleSettings.Controls.Add((Control) this.FrameFrequencies);
      this.FrameResampleSettings.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FrameResampleSettings.ForeColor = SystemColors.ControlText;
      this.FrameResampleSettings.Location = new Point(21, 122);
      this.FrameResampleSettings.Name = "FrameResampleSettings";
      this.FrameResampleSettings.RightToLeft = RightToLeft.No;
      this.FrameResampleSettings.Size = new Size(382, 165);
      this.FrameResampleSettings.TabIndex = 17;
      this.FrameResampleSettings.TabStop = false;
      this.FrameResampleSettings.Text = "Resample settings";
      this.FrameBitsPerSample.BackColor = SystemColors.Control;
      this.FrameBitsPerSample.Controls.Add((Control) this.radioButton32bitsFloat);
      this.FrameBitsPerSample.Controls.Add((Control) this.radioButton32bits);
      this.FrameBitsPerSample.Controls.Add((Control) this.radioButton16bits);
      this.FrameBitsPerSample.Controls.Add((Control) this.radioButton8bits);
      this.FrameBitsPerSample.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FrameBitsPerSample.ForeColor = SystemColors.ControlText;
      this.FrameBitsPerSample.Location = new Point(263, 20);
      this.FrameBitsPerSample.Name = "FrameBitsPerSample";
      this.FrameBitsPerSample.RightToLeft = RightToLeft.No;
      this.FrameBitsPerSample.Size = new Size(105, 124);
      this.FrameBitsPerSample.TabIndex = 19;
      this.FrameBitsPerSample.TabStop = false;
      this.FrameBitsPerSample.Text = "Bits per sample";
      this.radioButton32bitsFloat.BackColor = SystemColors.Control;
      this.radioButton32bitsFloat.Cursor = Cursors.Default;
      this.radioButton32bitsFloat.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.radioButton32bitsFloat.ForeColor = SystemColors.ControlText;
      this.radioButton32bitsFloat.Location = new Point(12, 89);
      this.radioButton32bitsFloat.Name = "radioButton32bitsFloat";
      this.radioButton32bitsFloat.RightToLeft = RightToLeft.No;
      this.radioButton32bitsFloat.Size = new Size(93, 23);
      this.radioButton32bitsFloat.TabIndex = 23;
      this.radioButton32bitsFloat.TabStop = true;
      this.radioButton32bitsFloat.Text = "32 bits float";
      this.radioButton32bitsFloat.UseVisualStyleBackColor = false;
      this.radioButton32bits.BackColor = SystemColors.Control;
      this.radioButton32bits.Cursor = Cursors.Default;
      this.radioButton32bits.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.radioButton32bits.ForeColor = SystemColors.ControlText;
      this.radioButton32bits.Location = new Point(12, 66);
      this.radioButton32bits.Name = "radioButton32bits";
      this.radioButton32bits.RightToLeft = RightToLeft.No;
      this.radioButton32bits.Size = new Size(93, 23);
      this.radioButton32bits.TabIndex = 22;
      this.radioButton32bits.TabStop = true;
      this.radioButton32bits.Text = "32 bits";
      this.radioButton32bits.UseVisualStyleBackColor = false;
      this.radioButton16bits.BackColor = SystemColors.Control;
      this.radioButton16bits.Cursor = Cursors.Default;
      this.radioButton16bits.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.radioButton16bits.ForeColor = SystemColors.ControlText;
      this.radioButton16bits.Location = new Point(12, 43);
      this.radioButton16bits.Name = "radioButton16bits";
      this.radioButton16bits.RightToLeft = RightToLeft.No;
      this.radioButton16bits.Size = new Size(93, 23);
      this.radioButton16bits.TabIndex = 21;
      this.radioButton16bits.TabStop = true;
      this.radioButton16bits.Text = "16 bits";
      this.radioButton16bits.UseVisualStyleBackColor = false;
      this.radioButton8bits.BackColor = SystemColors.Control;
      this.radioButton8bits.Cursor = Cursors.Default;
      this.radioButton8bits.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.radioButton8bits.ForeColor = SystemColors.ControlText;
      this.radioButton8bits.Location = new Point(12, 20);
      this.radioButton8bits.Name = "radioButton8bits";
      this.radioButton8bits.RightToLeft = RightToLeft.No;
      this.radioButton8bits.Size = new Size(93, 23);
      this.radioButton8bits.TabIndex = 20;
      this.radioButton8bits.TabStop = true;
      this.radioButton8bits.Text = "8 bits";
      this.radioButton8bits.UseVisualStyleBackColor = false;
      this.Frame2.BackColor = SystemColors.Control;
      this.Frame2.Controls.Add((Control) this.radioButtonSurround);
      this.Frame2.Controls.Add((Control) this.radioButtonStereo);
      this.Frame2.Controls.Add((Control) this.radioButtonMono);
      this.Frame2.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.Frame2.ForeColor = SystemColors.ControlText;
      this.Frame2.Location = new Point(141, 20);
      this.Frame2.Name = "Frame2";
      this.Frame2.RightToLeft = RightToLeft.No;
      this.Frame2.Size = new Size(100, 124);
      this.Frame2.TabIndex = 16;
      this.Frame2.TabStop = false;
      this.Frame2.Text = "Channels";
      this.radioButtonSurround.BackColor = SystemColors.Control;
      this.radioButtonSurround.Cursor = Cursors.Default;
      this.radioButtonSurround.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.radioButtonSurround.ForeColor = SystemColors.ControlText;
      this.radioButtonSurround.Location = new Point(8, 66);
      this.radioButtonSurround.Name = "radioButtonSurround";
      this.radioButtonSurround.RightToLeft = RightToLeft.No;
      this.radioButtonSurround.Size = new Size(86, 17);
      this.radioButtonSurround.TabIndex = 19;
      this.radioButtonSurround.TabStop = true;
      this.radioButtonSurround.Text = "Surround";
      this.radioButtonSurround.UseVisualStyleBackColor = false;
      this.radioButtonStereo.BackColor = SystemColors.Control;
      this.radioButtonStereo.Cursor = Cursors.Default;
      this.radioButtonStereo.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.radioButtonStereo.ForeColor = SystemColors.ControlText;
      this.radioButtonStereo.Location = new Point(8, 43);
      this.radioButtonStereo.Name = "radioButtonStereo";
      this.radioButtonStereo.RightToLeft = RightToLeft.No;
      this.radioButtonStereo.Size = new Size(77, 17);
      this.radioButtonStereo.TabIndex = 18;
      this.radioButtonStereo.TabStop = true;
      this.radioButtonStereo.Text = "Stereo";
      this.radioButtonStereo.UseVisualStyleBackColor = false;
      this.radioButtonMono.BackColor = SystemColors.Control;
      this.radioButtonMono.Cursor = Cursors.Default;
      this.radioButtonMono.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.radioButtonMono.ForeColor = SystemColors.ControlText;
      this.radioButtonMono.Location = new Point(8, 20);
      this.radioButtonMono.Name = "radioButtonMono";
      this.radioButtonMono.RightToLeft = RightToLeft.No;
      this.radioButtonMono.Size = new Size(69, 17);
      this.radioButtonMono.TabIndex = 17;
      this.radioButtonMono.TabStop = true;
      this.radioButtonMono.Text = "Mono";
      this.radioButtonMono.UseVisualStyleBackColor = false;
      this.FrameFrequencies.BackColor = SystemColors.Control;
      this.FrameFrequencies.Controls.Add((Control) this.radioButton48000);
      this.FrameFrequencies.Controls.Add((Control) this.radioButton44100);
      this.FrameFrequencies.Controls.Add((Control) this.radioButton22050);
      this.FrameFrequencies.Controls.Add((Control) this.radioButton11025);
      this.FrameFrequencies.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FrameFrequencies.ForeColor = SystemColors.ControlText;
      this.FrameFrequencies.Location = new Point(19, 20);
      this.FrameFrequencies.Name = "FrameFrequencies";
      this.FrameFrequencies.RightToLeft = RightToLeft.No;
      this.FrameFrequencies.Size = new Size(100, 124);
      this.FrameFrequencies.TabIndex = 12;
      this.FrameFrequencies.TabStop = false;
      this.FrameFrequencies.Text = "Frequencies";
      this.radioButton48000.BackColor = SystemColors.Control;
      this.radioButton48000.Cursor = Cursors.Default;
      this.radioButton48000.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.radioButton48000.ForeColor = SystemColors.ControlText;
      this.radioButton48000.Location = new Point(8, 20);
      this.radioButton48000.Name = "radioButton48000";
      this.radioButton48000.RightToLeft = RightToLeft.No;
      this.radioButton48000.Size = new Size(53, 17);
      this.radioButton48000.TabIndex = 22;
      this.radioButton48000.TabStop = true;
      this.radioButton48000.Text = "48000";
      this.radioButton48000.UseVisualStyleBackColor = false;
      this.radioButton44100.BackColor = SystemColors.Control;
      this.radioButton44100.Cursor = Cursors.Default;
      this.radioButton44100.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.radioButton44100.ForeColor = SystemColors.ControlText;
      this.radioButton44100.Location = new Point(8, 43);
      this.radioButton44100.Name = "radioButton44100";
      this.radioButton44100.RightToLeft = RightToLeft.No;
      this.radioButton44100.Size = new Size(61, 17);
      this.radioButton44100.TabIndex = 15;
      this.radioButton44100.TabStop = true;
      this.radioButton44100.Text = "44100";
      this.radioButton44100.UseVisualStyleBackColor = false;
      this.radioButton22050.BackColor = SystemColors.Control;
      this.radioButton22050.Cursor = Cursors.Default;
      this.radioButton22050.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.radioButton22050.ForeColor = SystemColors.ControlText;
      this.radioButton22050.Location = new Point(8, 66);
      this.radioButton22050.Name = "radioButton22050";
      this.radioButton22050.RightToLeft = RightToLeft.No;
      this.radioButton22050.Size = new Size(61, 17);
      this.radioButton22050.TabIndex = 14;
      this.radioButton22050.TabStop = true;
      this.radioButton22050.Text = "22050";
      this.radioButton22050.UseVisualStyleBackColor = false;
      this.radioButton11025.BackColor = SystemColors.Control;
      this.radioButton11025.Cursor = Cursors.Default;
      this.radioButton11025.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.radioButton11025.ForeColor = SystemColors.ControlText;
      this.radioButton11025.Location = new Point(8, 89);
      this.radioButton11025.Name = "radioButton11025";
      this.radioButton11025.RightToLeft = RightToLeft.No;
      this.radioButton11025.Size = new Size(61, 17);
      this.radioButton11025.TabIndex = 13;
      this.radioButton11025.TabStop = true;
      this.radioButton11025.Text = "11025";
      this.radioButton11025.UseVisualStyleBackColor = false;
      this.Frame5.BackColor = SystemColors.Control;
      this.Frame5.Controls.Add((Control) this.radioButtonExportSelection);
      this.Frame5.Controls.Add((Control) this.radioButtonExportFull);
      this.Frame5.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.Frame5.ForeColor = SystemColors.ControlText;
      this.Frame5.Location = new Point(21, 34);
      this.Frame5.Name = "Frame5";
      this.Frame5.RightToLeft = RightToLeft.No;
      this.Frame5.Size = new Size(161, 84);
      this.Frame5.TabIndex = 16;
      this.Frame5.TabStop = false;
      this.Frame5.Text = "Export range";
      this.radioButtonExportSelection.BackColor = SystemColors.Control;
      this.radioButtonExportSelection.Cursor = Cursors.Default;
      this.radioButtonExportSelection.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.radioButtonExportSelection.ForeColor = SystemColors.ControlText;
      this.radioButtonExportSelection.Location = new Point(8, 51);
      this.radioButtonExportSelection.Name = "radioButtonExportSelection";
      this.radioButtonExportSelection.RightToLeft = RightToLeft.No;
      this.radioButtonExportSelection.Size = new Size(125, 17);
      this.radioButtonExportSelection.TabIndex = 10;
      this.radioButtonExportSelection.TabStop = true;
      this.radioButtonExportSelection.Text = "Selection only";
      this.radioButtonExportSelection.UseVisualStyleBackColor = false;
      this.radioButtonExportFull.BackColor = SystemColors.Control;
      this.radioButtonExportFull.Cursor = Cursors.Default;
      this.radioButtonExportFull.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.radioButtonExportFull.ForeColor = SystemColors.ControlText;
      this.radioButtonExportFull.Location = new Point(8, 24);
      this.radioButtonExportFull.Name = "radioButtonExportFull";
      this.radioButtonExportFull.RightToLeft = RightToLeft.No;
      this.radioButtonExportFull.Size = new Size(137, 21);
      this.radioButtonExportFull.TabIndex = 9;
      this.radioButtonExportFull.TabStop = true;
      this.radioButtonExportFull.Text = "Full sound";
      this.radioButtonExportFull.UseVisualStyleBackColor = false;
      this.buttonCancel.BackColor = SystemColors.Control;
      this.buttonCancel.Cursor = Cursors.Default;
      this.buttonCancel.DialogResult = DialogResult.Cancel;
      this.buttonCancel.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.buttonCancel.ForeColor = SystemColors.ControlText;
      this.buttonCancel.Location = new Point(222, 388);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.RightToLeft = RightToLeft.No;
      this.buttonCancel.Size = new Size(117, 25);
      this.buttonCancel.TabIndex = 15;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = false;
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      this.Frame4.BackColor = SystemColors.Control;
      this.Frame4.Controls.Add((Control) this.comboBoxFormats);
      this.Frame4.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.Frame4.ForeColor = SystemColors.ControlText;
      this.Frame4.Location = new Point(189, 34);
      this.Frame4.Name = "Frame4";
      this.Frame4.RightToLeft = RightToLeft.No;
      this.Frame4.Size = new Size(214, 84);
      this.Frame4.TabIndex = 14;
      this.Frame4.TabStop = false;
      this.Frame4.Text = "Output format";
      this.comboBoxFormats.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBoxFormats.Location = new Point(8, 32);
      this.comboBoxFormats.Name = "comboBoxFormats";
      this.comboBoxFormats.Size = new Size(196, 22);
      this.comboBoxFormats.TabIndex = 0;
      this.comboBoxFormats.SelectedIndexChanged += new System.EventHandler(this.comboBoxFormats_SelectedIndexChanged);
      this.buttonOK.BackColor = SystemColors.Control;
      this.buttonOK.Cursor = Cursors.Default;
      this.buttonOK.DialogResult = DialogResult.OK;
      this.buttonOK.Enabled = false;
      this.buttonOK.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.buttonOK.ForeColor = SystemColors.ControlText;
      this.buttonOK.Location = new Point(86, 388);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.RightToLeft = RightToLeft.No;
      this.buttonOK.Size = new Size(117, 25);
      this.buttonOK.TabIndex = 12;
      this.buttonOK.Text = "Export start";
      this.buttonOK.UseVisualStyleBackColor = false;
      this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
      this.button1.Location = new Point(345, 4);
      this.button1.Name = "button1";
      this.button1.Size = new Size(58, 23);
      this.button1.TabIndex = 18;
      this.button1.Text = "Browse";
      this.button1.UseVisualStyleBackColor = false;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      this.txtOutputFile.Location = new Point(90, 6);
      this.txtOutputFile.Name = "txtOutputFile";
      this.txtOutputFile.Size = new Size(249, 20);
      this.txtOutputFile.TabIndex = 19;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(26, 9);
      this.label1.Name = "label1";
      this.label1.Size = new Size(58, 13);
      this.label1.TabIndex = 20;
      this.label1.Text = "Output File";
      this.grpCodecSettings.Controls.Add((Control) this.txtVBR);
      this.grpCodecSettings.Controls.Add((Control) this.rbVBR);
      this.grpCodecSettings.Controls.Add((Control) this.trackBar1);
      this.grpCodecSettings.Controls.Add((Control) this.cmbBitrates);
      this.grpCodecSettings.Controls.Add((Control) this.rbCBR);
      this.grpCodecSettings.Location = new Point(21, 293);
      this.grpCodecSettings.Name = "grpCodecSettings";
      this.grpCodecSettings.Size = new Size(206, 89);
      this.grpCodecSettings.TabIndex = 21;
      this.grpCodecSettings.TabStop = false;
      this.grpCodecSettings.Text = "Codec Settings";
      this.grpCodecSettings.Visible = false;
      this.txtVBR.Location = new Point(168, 47);
      this.txtVBR.Name = "txtVBR";
      this.txtVBR.ReadOnly = true;
      this.txtVBR.Size = new Size(28, 20);
      this.txtVBR.TabIndex = 6;
      this.rbVBR.AutoSize = true;
      this.rbVBR.Location = new Point(8, 43);
      this.rbVBR.Name = "rbVBR";
      this.rbVBR.Size = new Size(47, 17);
      this.rbVBR.TabIndex = 1;
      this.rbVBR.Text = "VBR";
      this.rbVBR.UseVisualStyleBackColor = true;
      this.rbVBR.CheckedChanged += new System.EventHandler(this.rbVBR_CheckedChanged);
      this.trackBar1.Location = new Point(61, 43);
      this.trackBar1.Maximum = 9;
      this.trackBar1.Name = "trackBar1";
      this.trackBar1.Size = new Size(104, 45);
      this.trackBar1.TabIndex = 1;
      this.trackBar1.Value = 5;
      this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
      this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
      this.cmbBitrates.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbBitrates.FormattingEnabled = true;
      this.cmbBitrates.Items.AddRange(new object[6]
      {
        (object) "256",
        (object) "128",
        (object) "96",
        (object) "64",
        (object) "32",
        (object) "16"
      });
      this.cmbBitrates.Location = new Point(61, 20);
      this.cmbBitrates.Name = "cmbBitrates";
      this.cmbBitrates.Size = new Size(138, 21);
      this.cmbBitrates.TabIndex = 5;
      this.rbCBR.AutoSize = true;
      this.rbCBR.Checked = true;
      this.rbCBR.Location = new Point(8, 20);
      this.rbCBR.Name = "rbCBR";
      this.rbCBR.Size = new Size(47, 17);
      this.rbCBR.TabIndex = 0;
      this.rbCBR.TabStop = true;
      this.rbCBR.Text = "CBR";
      this.rbCBR.UseVisualStyleBackColor = true;
      this.rbCBR.CheckedChanged += new System.EventHandler(this.rbCBR_CheckedChanged);
      this.grpPresets.Controls.Add((Control) this.rbInsane);
      this.grpPresets.Controls.Add((Control) this.rbExtreme);
      this.grpPresets.Controls.Add((Control) this.rbStandard);
      this.grpPresets.Controls.Add((Control) this.rbMedium);
      this.grpPresets.Location = new Point(233, 293);
      this.grpPresets.Name = "grpPresets";
      this.grpPresets.Size = new Size(170, 89);
      this.grpPresets.TabIndex = 22;
      this.grpPresets.TabStop = false;
      this.grpPresets.Text = "Presets";
      this.grpPresets.Visible = false;
      this.rbExtreme.AutoSize = true;
      this.rbExtreme.Location = new Point(7, 66);
      this.rbExtreme.Name = "rbExtreme";
      this.rbExtreme.Size = new Size(63, 17);
      this.rbExtreme.TabIndex = 2;
      this.rbExtreme.Text = "Extreme";
      this.rbExtreme.UseVisualStyleBackColor = true;
      this.rbExtreme.CheckedChanged += new System.EventHandler(this.rbExtreme_CheckedChanged);
      this.rbStandard.AutoSize = true;
      this.rbStandard.Location = new Point(7, 43);
      this.rbStandard.Name = "rbStandard";
      this.rbStandard.Size = new Size(68, 17);
      this.rbStandard.TabIndex = 1;
      this.rbStandard.Text = "Standard";
      this.rbStandard.UseVisualStyleBackColor = true;
      this.rbStandard.CheckedChanged += new System.EventHandler(this.rbStandard_CheckedChanged);
      this.rbMedium.AutoSize = true;
      this.rbMedium.Location = new Point(7, 20);
      this.rbMedium.Name = "rbMedium";
      this.rbMedium.Size = new Size(62, 17);
      this.rbMedium.TabIndex = 0;
      this.rbMedium.Text = "Medium";
      this.rbMedium.UseVisualStyleBackColor = true;
      this.rbMedium.CheckedChanged += new System.EventHandler(this.rbMedium_CheckedChanged);
      this.rbInsane.AutoSize = true;
      this.rbInsane.Location = new Point(84, 20);
      this.rbInsane.Name = "rbInsane";
      this.rbInsane.Size = new Size(57, 17);
      this.rbInsane.TabIndex = 3;
      this.rbInsane.Text = "Insane";
      this.rbInsane.UseVisualStyleBackColor = true;
      this.rbInsane.CheckedChanged += new System.EventHandler(this.rbInsane_CheckedChanged);
      this.AutoScaleBaseSize = new Size(5, 13);
      this.ClientSize = new Size(422, 422);
      this.ControlBox = false;
      this.Controls.Add((Control) this.grpPresets);
      this.Controls.Add((Control) this.grpCodecSettings);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.txtOutputFile);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.FrameResampleSettings);
      this.Controls.Add((Control) this.Frame5);
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.Frame4);
      this.Controls.Add((Control) this.buttonOK);
      this.Name = nameof (FormExport);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Export sound under editing to file";
      this.Load += new System.EventHandler(this.FormExport_Load);
      this.FrameResampleSettings.ResumeLayout(false);
      this.FrameBitsPerSample.ResumeLayout(false);
      this.Frame2.ResumeLayout(false);
      this.FrameFrequencies.ResumeLayout(false);
      this.Frame5.ResumeLayout(false);
      this.Frame4.ResumeLayout(false);
      this.grpCodecSettings.ResumeLayout(false);
      this.grpCodecSettings.PerformLayout();
      this.trackBar1.EndInit();
      this.grpPresets.ResumeLayout(false);
      this.grpPresets.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private void FormExport_Load(object sender, EventArgs e)
    {
      this.radioButton44100.Checked = true;
      this.radioButtonStereo.Checked = true;
      this.radioButton16bits.Checked = true;
      if (this.audioSoundEditor1.GetChannels() > 2)
      {
        this.radioButtonStereo.Checked = false;
        this.radioButtonSurround.Enabled = true;
        this.radioButtonSurround.Checked = true;
      }
      else
      {
        this.radioButtonStereo.Checked = true;
        this.radioButtonSurround.Enabled = false;
        this.radioButtonSurround.Checked = false;
      }
      bool bSelectionAvailable = false;
      int selection = (int) this.audioSoundEditor1.DisplayWaveformAnalyzer.GetSelection(ref bSelectionAvailable, ref this.m_nBeginSelectionInMs, ref this.m_nEndSelectionInMs);
      if (bSelectionAvailable)
      {
        this.radioButtonExportFull.Checked = false;
        this.radioButtonExportSelection.Enabled = true;
        this.radioButtonExportSelection.Checked = true;
      }
      else
      {
        this.radioButtonExportFull.Checked = true;
        this.radioButtonExportSelection.Enabled = false;
        this.radioButtonExportSelection.Checked = false;
        this.m_nBeginSelectionInMs = 0;
        this.m_nEndSelectionInMs = -1;
      }
      this.comboBoxFormats.Items.Add((object) "Microsoft WAV");
      this.comboBoxFormats.Items.Add((object) "MP3");
      this.comboBoxFormats.Items.Add((object) "OGG Vorbis");
      this.comboBoxFormats.Items.Add((object) "Windows Media Audio (WMA)");
      this.comboBoxFormats.Items.Add((object) "AAC/MP4");
      this.comboBoxFormats.Items.Add((object) "Audio Compression Manager codec");
      this.comboBoxFormats.Items.Add((object) "Apple/SGI AIFF");
      this.comboBoxFormats.Items.Add((object) "Sun/NeXT AU");
      this.comboBoxFormats.Items.Add((object) "OPUS");
      this.comboBoxFormats.SelectedIndex = 0;
    }

    private void SetVBRScale(int low, int high)
    {
      this.trackBar1.Maximum = high;
      this.trackBar1.Minimum = low;
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      int nStartPosition;
      int nEndPosition;
      if (this.radioButtonExportFull.Checked)
      {
        nStartPosition = 0;
        nEndPosition = -1;
      }
      else
      {
        nStartPosition = this.m_nBeginSelectionInMs;
        nEndPosition = this.m_nEndSelectionInMs;
      }
      int nFrequency = 44100;
      int nChannels = 2;
      if (this.radioButton48000.Checked)
        nFrequency = 48000;
      if (this.radioButton44100.Checked)
        nFrequency = 44100;
      if (this.radioButton22050.Checked)
        nFrequency = 22050;
      if (this.radioButton11025.Checked)
        nFrequency = 11025;
      if (this.radioButtonSurround.Checked)
        nChannels = this.audioSoundEditor1.GetChannels();
      if (this.radioButtonStereo.Checked)
        nChannels = 2;
      if (this.radioButtonMono.Checked)
        nChannels = 1;
      string str = Path.GetDirectoryName(this.txtOutputFile.Text) + "\\" + Path.GetFileNameWithoutExtension(this.txtOutputFile.Text);
      switch (this.comboBoxFormats.SelectedIndex)
      {
        case 0:
          this.audioSoundEditor1.EncodeFormats.FormatToUse = enumEncodingFormats.ENCODING_FORMAT_WAV;
          this.audioSoundEditor1.EncodeFormats.WAV.EncodeMode = !this.radioButton8bits.Checked ? (!this.radioButton16bits.Checked ? (!this.radioButton32bits.Checked ? enumWavEncodeModes.WAV_ENCODE_FLOAT32 : enumWavEncodeModes.WAV_ENCODE_PCM_S32) : enumWavEncodeModes.WAV_ENCODE_PCM_S16) : enumWavEncodeModes.WAV_ENCODE_PCM_U8;
          str += ".wav";
          break;
        case 1:
          this.audioSoundEditor1.EncodeFormats.FormatToUse = enumEncodingFormats.ENCODING_FORMAT_MP3;
          this.audioSoundEditor1.EncodeFormats.MP3.EncodeMode = enumMp3EncodeModes.MP3_ENCODE_PRESETS;
          if (this.rbStandard.Checked)
            this.audioSoundEditor1.EncodeFormats.MP3.Preset = enumMp3EncodePresets.MP3_PRESET_STANDARD;
          else if (this.rbMedium.Checked)
            this.audioSoundEditor1.EncodeFormats.MP3.Preset = enumMp3EncodePresets.MP3_PRESET_MEDIUM;
          else if (this.rbExtreme.Checked)
            this.audioSoundEditor1.EncodeFormats.MP3.Preset = enumMp3EncodePresets.MP3_PRESET_EXTREME;
          else if (this.rbInsane.Checked)
            this.audioSoundEditor1.EncodeFormats.MP3.Preset = enumMp3EncodePresets.MP3_PRESET_INSANE;
          if (this.rbVBR.Checked)
          {
            this.audioSoundEditor1.EncodeFormats.MP3.EncodeMode = enumMp3EncodeModes.MP3_ENCODE_CUSTOM;
            this.audioSoundEditor1.EncodeFormats.MP3.CustomString = "-V " + (object) this.trackBar1.Value;
          }
          else if (this.rbCBR.Checked)
          {
            this.audioSoundEditor1.EncodeFormats.MP3.EncodeMode = enumMp3EncodeModes.MP3_ENCODE_CBR;
            this.audioSoundEditor1.EncodeFormats.MP3.CBR = int.Parse(this.cmbBitrates.SelectedItem.ToString());
          }
          str += ".mp3";
          break;
        case 2:
          this.audioSoundEditor1.EncodeFormats.FormatToUse = enumEncodingFormats.ENCODING_FORMAT_OGG;
          if (this.rbVBR.Checked)
          {
            this.audioSoundEditor1.EncodeFormats.OGG.EncodeMode = enumOggEncodeModes.OGG_ENCODE_QUALITY;
            this.audioSoundEditor1.EncodeFormats.OGG.Quality = (float) this.trackBar1.Value;
          }
          else if (this.rbCBR.Checked)
          {
            this.audioSoundEditor1.EncodeFormats.OGG.EncodeMode = enumOggEncodeModes.OGG_ENCODE_BITRATE;
            this.audioSoundEditor1.EncodeFormats.OGG.Bitrate = int.Parse(this.cmbBitrates.SelectedItem.ToString()) * 1000;
          }
          str += ".ogg";
          break;
        case 3:
          this.audioSoundEditor1.EncodeFormats.FormatToUse = enumEncodingFormats.ENCODING_FORMAT_WMA;
          this.audioSoundEditor1.EncodeFormats.WMA.EncodeMode = !this.radioButtonSurround.Checked || nChannels <= 2 ? enumWmaEncodeModes.WMA_ENCODE_CBR : enumWmaEncodeModes.WMA_ENCODE_CBR_PRO_16;
          this.audioSoundEditor1.EncodeFormats.WMA.EncodeMode = enumWmaEncodeModes.WMA_ENCODE_CBR;
          this.audioSoundEditor1.EncodeFormats.WMA.CBR = nFrequency == 48000 || nFrequency == 44100 ? 128000 : 32000;
          str += ".wma";
          break;
        case 4:
          this.audioSoundEditor1.EncodeFormats.FormatToUse = enumEncodingFormats.ENCODING_FORMAT_AAC;
          str += ".aac";
          if (this.rbVBR.Checked)
          {
            this.audioSoundEditor1.EncodeFormats.AAC.EncodeMode = enumAacEncodeModes.AAC_ENCODE_VBR_QUALITY;
            this.audioSoundEditor1.EncodeFormats.AAC.Quality = (float) this.trackBar1.Value;
            break;
          }
          if (this.rbCBR.Checked)
          {
            this.audioSoundEditor1.EncodeFormats.AAC.EncodeMode = enumAacEncodeModes.AAC_ENCODE_CUSTOM;
            this.audioSoundEditor1.EncodeFormats.AAC.CustomString = "-b " + this.cmbBitrates.SelectedItem.ToString();
            break;
          }
          break;
        case 5:
          this.audioSoundEditor1.EncodeFormats.FormatToUse = enumEncodingFormats.ENCODING_FORMAT_ACM;
          this.audioSoundEditor1.EncodeFormats.ACM.EncodeMode = enumAcmEncodeModes.ACM_ENCODE_USE_CODEC_INDEX;
          this.audioSoundEditor1.EncodeFormats.ACM.CodecIndex = this.m_ACMCodec;
          this.audioSoundEditor1.EncodeFormats.ACM.CodecFormatIndex = this.m_ACMCodecFormat;
          str += ".wav";
          break;
        case 6:
          this.audioSoundEditor1.EncodeFormats.FormatToUse = enumEncodingFormats.ENCODING_FORMAT_AIFF;
          this.audioSoundEditor1.EncodeFormats.AIFF.EncodeMode = !this.radioButton8bits.Checked ? (!this.radioButton16bits.Checked ? (!this.radioButton32bits.Checked ? enumAIFFEncodeModes.AIFF_ENCODE_FLOAT32 : enumAIFFEncodeModes.AIFF_ENCODE_PCM_S32) : enumAIFFEncodeModes.AIFF_ENCODE_PCM_S16) : enumAIFFEncodeModes.AIFF_ENCODE_PCM_U8;
          str += ".aiff";
          break;
        case 7:
          this.audioSoundEditor1.EncodeFormats.FormatToUse = enumEncodingFormats.ENCODING_FORMAT_AU;
          this.audioSoundEditor1.EncodeFormats.AU.EncodeMode = !this.radioButton8bits.Checked ? (!this.radioButton16bits.Checked ? (!this.radioButton32bits.Checked ? enumAUEncodeModes.AU_ENCODE_FLOAT32 : enumAUEncodeModes.AU_ENCODE_PCM_S32) : enumAUEncodeModes.AU_ENCODE_PCM_S16) : enumAUEncodeModes.AU_ENCODE_PCM_S8;
          str += ".au";
          break;
        case 8:
          this.audioSoundEditor1.EncodeFormats.FormatToUse = enumEncodingFormats.ENCODING_FORMAT_OPUS;
          str += ".opus";
          if (this.rbVBR.Checked)
          {
            this.audioSoundEditor1.EncodeFormats.OPUS.EncodeMode = enumOpusEncodeModes.OPUS_ENCODE_VBR;
            this.audioSoundEditor1.EncodeFormats.OPUS.Bitrate = this.trackBar1.Value * 1000;
            break;
          }
          if (this.rbCBR.Checked)
          {
            this.audioSoundEditor1.EncodeFormats.OPUS.EncodeMode = enumOpusEncodeModes.OPUS_ENCODE_HARD_CBR;
            this.audioSoundEditor1.EncodeFormats.OPUS.Bitrate = int.Parse(this.cmbBitrates.SelectedItem.ToString()) * 1000;
            break;
          }
          break;
      }
      this.m_strExportPathname = str;
      if (File.Exists(str))
      {
        try
        {
          File.Delete(str);
        }
        catch
        {
        }
      }
      int file = (int) this.audioSoundEditor1.ExportToFile(nFrequency, nChannels, 0, nStartPosition, nEndPosition, str);
      this.Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.m_strExportPathname = "";
      this.Close();
    }

    private void comboBoxFormats_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.audioSoundEditor1.GetChannels() > 2)
      {
        this.radioButtonStereo.Checked = false;
        this.radioButtonSurround.Enabled = true;
        this.radioButtonSurround.Checked = true;
      }
      else
      {
        this.radioButtonStereo.Checked = true;
        this.radioButtonSurround.Enabled = false;
        this.radioButtonSurround.Checked = false;
      }
      this.radioButtonMono.Enabled = true;
      switch (this.comboBoxFormats.SelectedIndex)
      {
        case 0:
          this.radioButton48000.Enabled = true;
          this.radioButton22050.Enabled = true;
          this.radioButton11025.Enabled = true;
          this.radioButton8bits.Enabled = true;
          this.FrameResampleSettings.Visible = true;
          this.FrameFrequencies.Visible = true;
          this.FrameBitsPerSample.Visible = true;
          this.grpCodecSettings.Visible = false;
          this.grpPresets.Visible = false;
          break;
        case 1:
          this.radioButton48000.Enabled = true;
          this.radioButton22050.Enabled = true;
          this.radioButton11025.Enabled = true;
          this.radioButton8bits.Enabled = true;
          this.radioButtonSurround.Enabled = false;
          this.radioButtonSurround.Checked = false;
          this.radioButtonStereo.Checked = true;
          this.FrameResampleSettings.Visible = true;
          this.FrameFrequencies.Visible = true;
          this.FrameBitsPerSample.Visible = false;
          this.grpCodecSettings.Visible = true;
          this.grpPresets.Visible = true;
          this.SetVBRScale(0, 9);
          break;
        case 2:
          this.radioButton48000.Enabled = true;
          this.radioButton22050.Enabled = true;
          this.radioButton11025.Enabled = true;
          this.radioButton8bits.Enabled = true;
          this.FrameResampleSettings.Visible = true;
          this.FrameFrequencies.Visible = true;
          this.FrameBitsPerSample.Visible = false;
          this.grpCodecSettings.Visible = true;
          this.grpPresets.Visible = false;
          this.SetVBRScale(0, 9);
          break;
        case 3:
          this.radioButton48000.Enabled = true;
          this.radioButton22050.Enabled = true;
          this.radioButton11025.Enabled = false;
          this.radioButton8bits.Enabled = false;
          this.radioButtonMono.Enabled = false;
          this.radioButton44100.Checked = true;
          this.radioButton16bits.Checked = true;
          this.FrameResampleSettings.Visible = true;
          this.FrameFrequencies.Visible = true;
          this.FrameBitsPerSample.Visible = false;
          this.grpCodecSettings.Visible = false;
          this.grpPresets.Visible = false;
          break;
        case 4:
          this.radioButton48000.Enabled = true;
          this.radioButton22050.Enabled = true;
          this.radioButton11025.Enabled = true;
          this.radioButton8bits.Enabled = true;
          this.FrameResampleSettings.Visible = true;
          this.FrameFrequencies.Visible = true;
          this.FrameBitsPerSample.Visible = false;
          this.grpCodecSettings.Visible = true;
          this.grpPresets.Visible = false;
          this.SetVBRScale(0, 250);
          break;
        case 5:
          this.FrameResampleSettings.Visible = false;
          FormACMCodecs formAcmCodecs = new FormACMCodecs();
          formAcmCodecs.audioSoundEditor1 = this.audioSoundEditor1;
          if (formAcmCodecs.ShowDialog() == DialogResult.Cancel)
            break;
          this.m_ACMCodec = formAcmCodecs.m_ACMCodec;
          this.m_ACMCodecFormat = formAcmCodecs.m_ACMCodecFormat;
          this.grpCodecSettings.Visible = false;
          this.grpPresets.Visible = false;
          break;
        case 6:
          this.radioButton48000.Enabled = true;
          this.radioButton22050.Enabled = true;
          this.radioButton11025.Enabled = true;
          this.radioButton8bits.Enabled = true;
          this.FrameResampleSettings.Visible = true;
          this.FrameFrequencies.Visible = true;
          this.FrameBitsPerSample.Visible = true;
          this.grpCodecSettings.Visible = false;
          this.grpPresets.Visible = false;
          break;
        case 7:
          this.radioButton48000.Enabled = true;
          this.radioButton22050.Enabled = true;
          this.radioButton11025.Enabled = true;
          this.radioButton8bits.Enabled = true;
          this.FrameResampleSettings.Visible = true;
          this.FrameFrequencies.Visible = true;
          this.FrameBitsPerSample.Visible = true;
          this.grpCodecSettings.Visible = false;
          this.grpPresets.Visible = false;
          break;
        case 8:
          this.radioButton48000.Enabled = false;
          this.radioButton22050.Enabled = false;
          this.radioButton11025.Enabled = false;
          this.radioButton8bits.Enabled = false;
          this.FrameResampleSettings.Visible = true;
          this.FrameFrequencies.Visible = false;
          this.FrameBitsPerSample.Visible = false;
          this.grpCodecSettings.Visible = true;
          this.grpPresets.Visible = false;
          this.SetVBRScale(6, 256);
          break;
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      SaveFileDialog saveFileDialog = new SaveFileDialog();
      saveFileDialog.AddExtension = true;
      if (saveFileDialog.ShowDialog() != DialogResult.OK)
        return;
      this.txtOutputFile.Text = saveFileDialog.FileName;
      this.buttonOK.Enabled = true;
    }

    private void rbVBR_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.rbVBR.Checked || !(this.txtVBR.Text == ""))
        return;
      this.DeselectPresets();
      this.txtVBR.Text = this.trackBar1.Value.ToString();
    }

    private void trackBar1_ValueChanged(object sender, EventArgs e)
    {
      this.txtVBR.Text = this.trackBar1.Value.ToString();
      this.DeselectPresets();
    }

    private void trackBar1_Scroll(object sender, EventArgs e)
    {
      this.rbVBR.Checked = true;
      this.DeselectPresets();
    }

    private void DeselectPresets()
    {
      this.rbExtreme.Checked = false;
      this.rbInsane.Checked = false;
      this.rbMedium.Checked = false;
      this.rbStandard.Checked = false;
    }

    private void DeselectCodecs()
    {
      this.rbCBR.Checked = false;
      this.rbVBR.Checked = false;
    }

    private void rbCBR_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.rbCBR.Checked)
        return;
      this.DeselectPresets();
    }

    private void rbMedium_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.rbMedium.Checked)
        return;
      this.DeselectCodecs();
    }

    private void rbInsane_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.rbInsane.Checked)
        return;
      this.DeselectCodecs();
    }

    private void rbStandard_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.rbStandard.Checked)
        return;
      this.DeselectCodecs();
    }

    private void rbExtreme_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.rbExtreme.Checked)
        return;
      this.DeselectCodecs();
    }
  }
}
