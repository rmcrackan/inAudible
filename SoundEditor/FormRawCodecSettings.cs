// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormRawCodecSettings
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
  public class FormRawCodecSettings : Form
  {
    private Button buttonCancel;
    private Button buttonOK;
    private Container components;
    private Label label1;
    private Label label2;
    private Label label3;
    private ComboBox comboBoxEncodeModes;
    private ComboBox comboBoxSampleRates;
    private ComboBox comboBoxChannels;
    private CheckBox checkBoxIsBigEndian;
    internal AudioSoundEditor.AudioSoundEditor audioSoundEditor1;
    public bool m_bCancel;
    public enumRAWEncodeModes m_nEncodeMode;
    public int m_nSamplerate;
    public int m_nChannels;
    public bool m_bIsBigEndian;

    public FormRawCodecSettings()
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
      this.buttonCancel = new Button();
      this.buttonOK = new Button();
      this.label1 = new Label();
      this.label2 = new Label();
      this.label3 = new Label();
      this.comboBoxEncodeModes = new ComboBox();
      this.comboBoxSampleRates = new ComboBox();
      this.comboBoxChannels = new ComboBox();
      this.checkBoxIsBigEndian = new CheckBox();
      this.SuspendLayout();
      this.buttonCancel.DialogResult = DialogResult.Cancel;
      this.buttonCancel.Location = new Point(160, 168);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(104, 24);
      this.buttonCancel.TabIndex = 16;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      this.buttonOK.Location = new Point(32, 168);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new Size(96, 24);
      this.buttonOK.TabIndex = 15;
      this.buttonOK.Text = "OK";
      this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
      this.label1.Location = new Point(16, 26);
      this.label1.Name = "label1";
      this.label1.Size = new Size(104, 16);
      this.label1.TabIndex = 17;
      this.label1.Text = "Encode mode";
      this.label2.Location = new Point(16, 58);
      this.label2.Name = "label2";
      this.label2.Size = new Size(104, 16);
      this.label2.TabIndex = 18;
      this.label2.Text = "Sample rate";
      this.label3.Location = new Point(16, 90);
      this.label3.Name = "label3";
      this.label3.Size = new Size(104, 16);
      this.label3.TabIndex = 19;
      this.label3.Text = "Channels";
      this.comboBoxEncodeModes.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBoxEncodeModes.Location = new Point(136, 24);
      this.comboBoxEncodeModes.Name = "comboBoxEncodeModes";
      this.comboBoxEncodeModes.Size = new Size(128, 21);
      this.comboBoxEncodeModes.TabIndex = 20;
      this.comboBoxSampleRates.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBoxSampleRates.Location = new Point(136, 56);
      this.comboBoxSampleRates.Name = "comboBoxSampleRates";
      this.comboBoxSampleRates.Size = new Size(128, 21);
      this.comboBoxSampleRates.TabIndex = 21;
      this.comboBoxChannels.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBoxChannels.Location = new Point(136, 88);
      this.comboBoxChannels.Name = "comboBoxChannels";
      this.comboBoxChannels.Size = new Size(128, 21);
      this.comboBoxChannels.TabIndex = 22;
      this.checkBoxIsBigEndian.Location = new Point(136, 128);
      this.checkBoxIsBigEndian.Name = "checkBoxIsBigEndian";
      this.checkBoxIsBigEndian.Size = new Size(128, 16);
      this.checkBoxIsBigEndian.TabIndex = 23;
      this.checkBoxIsBigEndian.Text = "Is big endian";
      this.AutoScaleBaseSize = new Size(5, 13);
      this.ClientSize = new Size(292, 206);
      this.ControlBox = false;
      this.Controls.Add((Control) this.checkBoxIsBigEndian);
      this.Controls.Add((Control) this.comboBoxChannels);
      this.Controls.Add((Control) this.comboBoxSampleRates);
      this.Controls.Add((Control) this.comboBoxEncodeModes);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.buttonOK);
      this.Name = nameof (FormRawCodecSettings);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Codec settings";
      this.Load += new System.EventHandler(this.FormRawCodecSettings_Load);
      this.ResumeLayout(false);
    }

    private void FormRawCodecSettings_Load(object sender, EventArgs e)
    {
      for (short nEncodeModeIndex = 0; (int) nEncodeModeIndex < (int) this.audioSoundEditor1.EncodeFormats.RAW.GetEncodeModesCount(); ++nEncodeModeIndex)
        this.comboBoxEncodeModes.Items.Add((object) this.audioSoundEditor1.EncodeFormats.RAW.GetEncodeModeDesc(nEncodeModeIndex));
      this.comboBoxEncodeModes.SelectedIndex = 0;
      this.comboBoxSampleRates.Items.Add((object) "6000");
      this.comboBoxSampleRates.Items.Add((object) "8000");
      this.comboBoxSampleRates.Items.Add((object) "11025");
      this.comboBoxSampleRates.Items.Add((object) "16000");
      this.comboBoxSampleRates.Items.Add((object) "22050");
      this.comboBoxSampleRates.Items.Add((object) "32000");
      this.comboBoxSampleRates.Items.Add((object) "44100");
      this.comboBoxSampleRates.Items.Add((object) "48000");
      this.comboBoxSampleRates.Items.Add((object) "64000");
      this.comboBoxSampleRates.Items.Add((object) "88200");
      this.comboBoxSampleRates.Items.Add((object) "96000");
      this.comboBoxSampleRates.SelectedIndex = 0;
      this.comboBoxChannels.Items.Add((object) "1 - Mono");
      this.comboBoxChannels.Items.Add((object) "2 - Stereo");
      this.comboBoxChannels.SelectedIndex = 0;
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      this.m_nEncodeMode = (enumRAWEncodeModes) this.comboBoxEncodeModes.SelectedIndex;
      switch (this.comboBoxSampleRates.SelectedIndex)
      {
        case 0:
          this.m_nSamplerate = 6000;
          break;
        case 1:
          this.m_nSamplerate = 8000;
          break;
        case 2:
          this.m_nSamplerate = 11025;
          break;
        case 3:
          this.m_nSamplerate = 16000;
          break;
        case 4:
          this.m_nSamplerate = 22050;
          break;
        case 5:
          this.m_nSamplerate = 32000;
          break;
        case 6:
          this.m_nSamplerate = 44100;
          break;
        case 7:
          this.m_nSamplerate = 48000;
          break;
        case 8:
          this.m_nSamplerate = 64000;
          break;
        case 9:
          this.m_nSamplerate = 88200;
          break;
        case 10:
          this.m_nSamplerate = 96000;
          break;
      }
      switch (this.comboBoxChannels.SelectedIndex)
      {
        case 0:
          this.m_nChannels = 1;
          break;
        case 1:
          this.m_nChannels = 2;
          break;
      }
      this.m_bIsBigEndian = this.checkBoxIsBigEndian.Checked;
      this.m_bCancel = false;
      this.Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.m_bCancel = true;
      this.Close();
    }
  }
}
