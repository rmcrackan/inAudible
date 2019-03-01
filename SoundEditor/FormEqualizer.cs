// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormEqualizer
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
  public class FormEqualizer : Form
  {
    private bool m_bOpeningEqualizer = true;
    private ComboBox comboBoxPresets;
    private Label label14;
    private Label label13;
    private TrackBar trackBar16000Hz;
    private Label label12;
    private TrackBar trackBar14000Hz;
    private Label label10;
    private Label label2;
    private Label label9;
    private Label label8;
    private Label label7;
    private Label label6;
    private Label label5;
    private Label label4;
    private Label label3;
    private TrackBar trackBar12000Hz;
    private TrackBar trackBar6000Hz;
    private TrackBar trackBar3000Hz;
    private TrackBar trackBar1000Hz;
    private TrackBar trackBar600Hz;
    private TrackBar trackBar310Hz;
    private TrackBar trackBar170Hz;
    private Label label11;
    private TrackBar trackBar80Hz;
    private Button buttonCancel;
    private Button buttonOK;
    internal AudioSoundEditor.AudioSoundEditor audioSoundEditor1;
    public bool m_bCancel;
    private Container components;

    public FormEqualizer()
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
      this.comboBoxPresets = new ComboBox();
      this.label14 = new Label();
      this.label13 = new Label();
      this.trackBar16000Hz = new TrackBar();
      this.label12 = new Label();
      this.trackBar14000Hz = new TrackBar();
      this.label10 = new Label();
      this.label2 = new Label();
      this.label9 = new Label();
      this.label8 = new Label();
      this.label7 = new Label();
      this.label6 = new Label();
      this.label5 = new Label();
      this.label4 = new Label();
      this.label3 = new Label();
      this.trackBar12000Hz = new TrackBar();
      this.trackBar6000Hz = new TrackBar();
      this.trackBar3000Hz = new TrackBar();
      this.trackBar1000Hz = new TrackBar();
      this.trackBar600Hz = new TrackBar();
      this.trackBar310Hz = new TrackBar();
      this.trackBar170Hz = new TrackBar();
      this.label11 = new Label();
      this.trackBar80Hz = new TrackBar();
      this.buttonCancel = new Button();
      this.buttonOK = new Button();
      this.trackBar16000Hz.BeginInit();
      this.trackBar14000Hz.BeginInit();
      this.trackBar12000Hz.BeginInit();
      this.trackBar6000Hz.BeginInit();
      this.trackBar3000Hz.BeginInit();
      this.trackBar1000Hz.BeginInit();
      this.trackBar600Hz.BeginInit();
      this.trackBar310Hz.BeginInit();
      this.trackBar170Hz.BeginInit();
      this.trackBar80Hz.BeginInit();
      this.SuspendLayout();
      this.comboBoxPresets.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBoxPresets.Location = new Point(186, 208);
      this.comboBoxPresets.Name = "comboBoxPresets";
      this.comboBoxPresets.Size = new Size(172, 21);
      this.comboBoxPresets.TabIndex = 134;
      this.comboBoxPresets.SelectedIndexChanged += new System.EventHandler(this.comboBoxPresets_SelectedIndexChanged);
      this.label14.Location = new Point(194, 184);
      this.label14.Name = "label14";
      this.label14.Size = new Size(156, 20);
      this.label14.TabIndex = 133;
      this.label14.Text = "Apply WinAmp (TM) presets";
      this.label13.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label13.Location = new Point(468, 24);
      this.label13.Name = "label13";
      this.label13.Size = new Size(48, 16);
      this.label13.TabIndex = 131;
      this.label13.Text = "16 Khz";
      this.label13.TextAlign = ContentAlignment.MiddleCenter;
      this.trackBar16000Hz.Location = new Point(468, 48);
      this.trackBar16000Hz.Maximum = 1500;
      this.trackBar16000Hz.Minimum = -1500;
      this.trackBar16000Hz.Name = "trackBar16000Hz";
      this.trackBar16000Hz.Orientation = Orientation.Vertical;
      this.trackBar16000Hz.Size = new Size(45, 128);
      this.trackBar16000Hz.TabIndex = 132;
      this.trackBar16000Hz.TickFrequency = 150;
      this.trackBar16000Hz.TickStyle = TickStyle.Both;
      this.trackBar16000Hz.Scroll += new System.EventHandler(this.trackBar16000Hz_Scroll);
      this.label12.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label12.Location = new Point(420, 24);
      this.label12.Name = "label12";
      this.label12.Size = new Size(48, 16);
      this.label12.TabIndex = 129;
      this.label12.Text = "14 Khz";
      this.label12.TextAlign = ContentAlignment.MiddleCenter;
      this.trackBar14000Hz.Location = new Point(420, 48);
      this.trackBar14000Hz.Maximum = 1500;
      this.trackBar14000Hz.Minimum = -1500;
      this.trackBar14000Hz.Name = "trackBar14000Hz";
      this.trackBar14000Hz.Orientation = Orientation.Vertical;
      this.trackBar14000Hz.Size = new Size(45, 128);
      this.trackBar14000Hz.TabIndex = 130;
      this.trackBar14000Hz.TickFrequency = 150;
      this.trackBar14000Hz.TickStyle = TickStyle.Both;
      this.trackBar14000Hz.Scroll += new System.EventHandler(this.trackBar14000Hz_Scroll);
      this.label10.Location = new Point(20, 152);
      this.label10.Name = "label10";
      this.label10.Size = new Size(8, 16);
      this.label10.TabIndex = 128;
      this.label10.Text = "_";
      this.label10.TextAlign = ContentAlignment.TopCenter;
      this.label2.Location = new Point(20, 48);
      this.label2.Name = "label2";
      this.label2.Size = new Size(8, 16);
      this.label2.TabIndex = (int) sbyte.MaxValue;
      this.label2.Text = "+";
      this.label2.TextAlign = ContentAlignment.MiddleCenter;
      this.label9.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label9.Location = new Point(372, 24);
      this.label9.Name = "label9";
      this.label9.Size = new Size(48, 16);
      this.label9.TabIndex = 119;
      this.label9.Text = "12 Khz";
      this.label9.TextAlign = ContentAlignment.MiddleCenter;
      this.label8.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label8.Location = new Point(324, 24);
      this.label8.Name = "label8";
      this.label8.Size = new Size(48, 16);
      this.label8.TabIndex = 118;
      this.label8.Text = "6 Khz";
      this.label8.TextAlign = ContentAlignment.MiddleCenter;
      this.label7.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label7.Location = new Point(276, 24);
      this.label7.Name = "label7";
      this.label7.Size = new Size(48, 16);
      this.label7.TabIndex = 117;
      this.label7.Text = "3 Khz";
      this.label7.TextAlign = ContentAlignment.MiddleCenter;
      this.label6.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label6.Location = new Point(228, 24);
      this.label6.Name = "label6";
      this.label6.Size = new Size(48, 16);
      this.label6.TabIndex = 116;
      this.label6.Text = "1 Khz";
      this.label6.TextAlign = ContentAlignment.MiddleCenter;
      this.label5.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label5.Location = new Point(180, 24);
      this.label5.Name = "label5";
      this.label5.Size = new Size(48, 16);
      this.label5.TabIndex = 115;
      this.label5.Text = "600 Hz";
      this.label5.TextAlign = ContentAlignment.MiddleCenter;
      this.label4.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label4.Location = new Point(132, 24);
      this.label4.Name = "label4";
      this.label4.Size = new Size(48, 16);
      this.label4.TabIndex = 114;
      this.label4.Text = "310 Hz";
      this.label4.TextAlign = ContentAlignment.MiddleCenter;
      this.label3.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label3.Location = new Point(84, 24);
      this.label3.Name = "label3";
      this.label3.Size = new Size(48, 16);
      this.label3.TabIndex = 113;
      this.label3.Text = "170 Hz";
      this.label3.TextAlign = ContentAlignment.MiddleCenter;
      this.trackBar12000Hz.Location = new Point(372, 48);
      this.trackBar12000Hz.Maximum = 1500;
      this.trackBar12000Hz.Minimum = -1500;
      this.trackBar12000Hz.Name = "trackBar12000Hz";
      this.trackBar12000Hz.Orientation = Orientation.Vertical;
      this.trackBar12000Hz.Size = new Size(45, 128);
      this.trackBar12000Hz.TabIndex = 126;
      this.trackBar12000Hz.TickFrequency = 150;
      this.trackBar12000Hz.TickStyle = TickStyle.Both;
      this.trackBar12000Hz.Scroll += new System.EventHandler(this.trackBar12000Hz_Scroll);
      this.trackBar6000Hz.Location = new Point(324, 48);
      this.trackBar6000Hz.Maximum = 1500;
      this.trackBar6000Hz.Minimum = -1500;
      this.trackBar6000Hz.Name = "trackBar6000Hz";
      this.trackBar6000Hz.Orientation = Orientation.Vertical;
      this.trackBar6000Hz.Size = new Size(45, 128);
      this.trackBar6000Hz.TabIndex = 125;
      this.trackBar6000Hz.TickFrequency = 150;
      this.trackBar6000Hz.TickStyle = TickStyle.Both;
      this.trackBar6000Hz.Scroll += new System.EventHandler(this.trackBar6000Hz_Scroll);
      this.trackBar3000Hz.Location = new Point(276, 48);
      this.trackBar3000Hz.Maximum = 1500;
      this.trackBar3000Hz.Minimum = -1500;
      this.trackBar3000Hz.Name = "trackBar3000Hz";
      this.trackBar3000Hz.Orientation = Orientation.Vertical;
      this.trackBar3000Hz.Size = new Size(45, 128);
      this.trackBar3000Hz.TabIndex = 124;
      this.trackBar3000Hz.TickFrequency = 150;
      this.trackBar3000Hz.TickStyle = TickStyle.Both;
      this.trackBar3000Hz.Scroll += new System.EventHandler(this.trackBar3000Hz_Scroll);
      this.trackBar1000Hz.Location = new Point(228, 48);
      this.trackBar1000Hz.Maximum = 1500;
      this.trackBar1000Hz.Minimum = -1500;
      this.trackBar1000Hz.Name = "trackBar1000Hz";
      this.trackBar1000Hz.Orientation = Orientation.Vertical;
      this.trackBar1000Hz.Size = new Size(45, 128);
      this.trackBar1000Hz.TabIndex = 123;
      this.trackBar1000Hz.TickFrequency = 150;
      this.trackBar1000Hz.TickStyle = TickStyle.Both;
      this.trackBar1000Hz.Scroll += new System.EventHandler(this.trackBar1000Hz_Scroll);
      this.trackBar600Hz.Location = new Point(180, 48);
      this.trackBar600Hz.Maximum = 1500;
      this.trackBar600Hz.Minimum = -1500;
      this.trackBar600Hz.Name = "trackBar600Hz";
      this.trackBar600Hz.Orientation = Orientation.Vertical;
      this.trackBar600Hz.Size = new Size(45, 128);
      this.trackBar600Hz.TabIndex = 122;
      this.trackBar600Hz.TickFrequency = 150;
      this.trackBar600Hz.TickStyle = TickStyle.Both;
      this.trackBar600Hz.Scroll += new System.EventHandler(this.trackBar600Hz_Scroll);
      this.trackBar310Hz.Location = new Point(132, 48);
      this.trackBar310Hz.Maximum = 1500;
      this.trackBar310Hz.Minimum = -1500;
      this.trackBar310Hz.Name = "trackBar310Hz";
      this.trackBar310Hz.Orientation = Orientation.Vertical;
      this.trackBar310Hz.Size = new Size(45, 128);
      this.trackBar310Hz.TabIndex = 121;
      this.trackBar310Hz.TickFrequency = 150;
      this.trackBar310Hz.TickStyle = TickStyle.Both;
      this.trackBar310Hz.Scroll += new System.EventHandler(this.trackBar310Hz_Scroll);
      this.trackBar170Hz.Location = new Point(84, 48);
      this.trackBar170Hz.Maximum = 1500;
      this.trackBar170Hz.Minimum = -1500;
      this.trackBar170Hz.Name = "trackBar170Hz";
      this.trackBar170Hz.Orientation = Orientation.Vertical;
      this.trackBar170Hz.Size = new Size(45, 128);
      this.trackBar170Hz.TabIndex = 120;
      this.trackBar170Hz.TickFrequency = 150;
      this.trackBar170Hz.TickStyle = TickStyle.Both;
      this.trackBar170Hz.Scroll += new System.EventHandler(this.trackBar170Hz_Scroll);
      this.label11.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label11.Location = new Point(36, 24);
      this.label11.Name = "label11";
      this.label11.Size = new Size(48, 16);
      this.label11.TabIndex = 112;
      this.label11.Text = "80 Hz";
      this.label11.TextAlign = ContentAlignment.MiddleCenter;
      this.trackBar80Hz.Location = new Point(36, 48);
      this.trackBar80Hz.Maximum = 1500;
      this.trackBar80Hz.Minimum = -1500;
      this.trackBar80Hz.Name = "trackBar80Hz";
      this.trackBar80Hz.Orientation = Orientation.Vertical;
      this.trackBar80Hz.Size = new Size(45, 128);
      this.trackBar80Hz.TabIndex = 111;
      this.trackBar80Hz.Tag = (object) "";
      this.trackBar80Hz.TickFrequency = 150;
      this.trackBar80Hz.TickStyle = TickStyle.Both;
      this.trackBar80Hz.Scroll += new System.EventHandler(this.trackBar80Hz_Scroll);
      this.buttonCancel.DialogResult = DialogResult.Cancel;
      this.buttonCancel.Location = new Point(288, 248);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(104, 24);
      this.buttonCancel.TabIndex = 136;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      this.buttonOK.Location = new Point(160, 248);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new Size(96, 24);
      this.buttonOK.TabIndex = 135;
      this.buttonOK.Text = "OK";
      this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
      this.AutoScaleBaseSize = new Size(5, 13);
      this.ClientSize = new Size(544, 294);
      this.ControlBox = false;
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.buttonOK);
      this.Controls.Add((Control) this.comboBoxPresets);
      this.Controls.Add((Control) this.label14);
      this.Controls.Add((Control) this.label13);
      this.Controls.Add((Control) this.trackBar16000Hz);
      this.Controls.Add((Control) this.label12);
      this.Controls.Add((Control) this.trackBar14000Hz);
      this.Controls.Add((Control) this.label10);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label9);
      this.Controls.Add((Control) this.label8);
      this.Controls.Add((Control) this.label7);
      this.Controls.Add((Control) this.label6);
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.trackBar12000Hz);
      this.Controls.Add((Control) this.trackBar6000Hz);
      this.Controls.Add((Control) this.trackBar3000Hz);
      this.Controls.Add((Control) this.trackBar1000Hz);
      this.Controls.Add((Control) this.trackBar600Hz);
      this.Controls.Add((Control) this.trackBar310Hz);
      this.Controls.Add((Control) this.trackBar170Hz);
      this.Controls.Add((Control) this.label11);
      this.Controls.Add((Control) this.trackBar80Hz);
      this.Name = nameof (FormEqualizer);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Equalizer settings";
      this.Load += new System.EventHandler(this.FormEqualizer_Load);
      this.trackBar16000Hz.EndInit();
      this.trackBar14000Hz.EndInit();
      this.trackBar12000Hz.EndInit();
      this.trackBar6000Hz.EndInit();
      this.trackBar3000Hz.EndInit();
      this.trackBar1000Hz.EndInit();
      this.trackBar600Hz.EndInit();
      this.trackBar310Hz.EndInit();
      this.trackBar170Hz.EndInit();
      this.trackBar80Hz.EndInit();
      this.ResumeLayout(false);
    }

    private void ResetEqualizerBands()
    {
      int count = this.audioSoundEditor1.Effects.EqualizerBandGetCount();
      for (short index = 0; (int) index < count; ++index)
      {
        float frequency = this.audioSoundEditor1.Effects.EqualizerBandGetFrequency((int) index);
        float fBandWidth = 0.0f;
        float fGain = 0.0f;
        int num1 = (int) this.audioSoundEditor1.Effects.EqualizerBandGetParams(frequency, ref fBandWidth, ref fGain);
        int num2 = (int) this.audioSoundEditor1.Effects.EqualizerBandSetParams(frequency, fBandWidth, 0.0f);
      }
      this.trackBar80Hz.Value = 0;
      this.trackBar170Hz.Value = 0;
      this.trackBar310Hz.Value = 0;
      this.trackBar600Hz.Value = 0;
      this.trackBar1000Hz.Value = 0;
      this.trackBar3000Hz.Value = 0;
      this.trackBar6000Hz.Value = 0;
      this.trackBar12000Hz.Value = 0;
      this.trackBar14000Hz.Value = 0;
      this.trackBar16000Hz.Value = 0;
    }

    private void UpdateBandsValues()
    {
      int count = this.audioSoundEditor1.Effects.EqualizerBandGetCount();
      for (short index = 0; (int) index < count; ++index)
      {
        float fBandWidth = 0.0f;
        float fGain = 0.0f;
        int num = (int) this.audioSoundEditor1.Effects.EqualizerBandGetParams(this.audioSoundEditor1.Effects.EqualizerBandGetFrequency((int) index), ref fBandWidth, ref fGain);
        switch (index)
        {
          case 0:
            this.trackBar80Hz.Value = (int) (short) fGain * 100;
            break;
          case 1:
            this.trackBar170Hz.Value = (int) (short) fGain * 100;
            break;
          case 2:
            this.trackBar310Hz.Value = (int) (short) fGain * 100;
            break;
          case 3:
            this.trackBar600Hz.Value = (int) (short) fGain * 100;
            break;
          case 4:
            this.trackBar1000Hz.Value = (int) (short) fGain * 100;
            break;
          case 5:
            this.trackBar3000Hz.Value = (int) (short) fGain * 100;
            break;
          case 6:
            this.trackBar6000Hz.Value = (int) (short) fGain * 100;
            break;
          case 7:
            this.trackBar12000Hz.Value = (int) (short) fGain * 100;
            break;
          case 8:
            this.trackBar14000Hz.Value = (int) (short) fGain * 100;
            break;
          case 9:
            this.trackBar16000Hz.Value = (int) (short) fGain * 100;
            break;
        }
      }
    }

    private void FormEqualizer_Load(object sender, EventArgs e)
    {
      this.comboBoxPresets.Items.Add((object) "None");
      this.comboBoxPresets.Items.Add((object) "Classical");
      this.comboBoxPresets.Items.Add((object) "Club");
      this.comboBoxPresets.Items.Add((object) "Dance");
      this.comboBoxPresets.Items.Add((object) "Full Bass");
      this.comboBoxPresets.Items.Add((object) "Full Bass Treble");
      this.comboBoxPresets.Items.Add((object) "Full Treble");
      this.comboBoxPresets.Items.Add((object) "Laptop Speakers");
      this.comboBoxPresets.Items.Add((object) "Large Hall");
      this.comboBoxPresets.Items.Add((object) "Live");
      this.comboBoxPresets.Items.Add((object) "Party");
      this.comboBoxPresets.Items.Add((object) "Pop");
      this.comboBoxPresets.Items.Add((object) "Reggae");
      this.comboBoxPresets.Items.Add((object) "Rock");
      this.comboBoxPresets.Items.Add((object) "Ska");
      this.comboBoxPresets.Items.Add((object) "Soft");
      this.comboBoxPresets.Items.Add((object) "Soft Rock");
      this.comboBoxPresets.Items.Add((object) "Techno");
      this.comboBoxPresets.SelectedIndex = 0;
      int frequency = this.audioSoundEditor1.GetFrequency();
      if (frequency <= 11025)
      {
        this.trackBar6000Hz.Visible = false;
        this.trackBar12000Hz.Visible = false;
        this.trackBar14000Hz.Visible = false;
        this.trackBar16000Hz.Visible = false;
      }
      else if (frequency <= 22050)
      {
        this.trackBar12000Hz.Visible = false;
        this.trackBar14000Hz.Visible = false;
        this.trackBar16000Hz.Visible = false;
      }
      else if (frequency <= 44100)
        this.trackBar16000Hz.Visible = false;
      if (this.audioSoundEditor1.Effects.EqualizerBandGetCount() == 0)
      {
        int num1 = (int) this.audioSoundEditor1.Effects.EqualizerBandAdd(80f, 12f, 0.0f);
        int num2 = (int) this.audioSoundEditor1.Effects.EqualizerBandAdd(170f, 12f, 0.0f);
        int num3 = (int) this.audioSoundEditor1.Effects.EqualizerBandAdd(310f, 12f, 0.0f);
        int num4 = (int) this.audioSoundEditor1.Effects.EqualizerBandAdd(600f, 12f, 0.0f);
        int num5 = (int) this.audioSoundEditor1.Effects.EqualizerBandAdd(1000f, 12f, 0.0f);
        int num6 = (int) this.audioSoundEditor1.Effects.EqualizerBandAdd(3000f, 12f, 0.0f);
        int num7 = (int) this.audioSoundEditor1.Effects.EqualizerBandAdd(6000f, 12f, 0.0f);
        int num8 = (int) this.audioSoundEditor1.Effects.EqualizerBandAdd(12000f, 12f, 0.0f);
        int num9 = (int) this.audioSoundEditor1.Effects.EqualizerBandAdd(14000f, 12f, 0.0f);
        int num10 = (int) this.audioSoundEditor1.Effects.EqualizerBandAdd(16000f, 12f, 0.0f);
      }
      else
        this.UpdateBandsValues();
    }

    private void comboBoxPresets_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.m_bOpeningEqualizer)
        this.m_bOpeningEqualizer = false;
      else if (this.comboBoxPresets.SelectedIndex == 0)
      {
        this.ResetEqualizerBands();
      }
      else
      {
        int num = (int) this.audioSoundEditor1.Effects.EqualizerLoadPresets((enumEqualizerPresets) (this.comboBoxPresets.SelectedIndex - 1));
        this.UpdateBandsValues();
      }
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      this.m_bCancel = false;
      this.Hide();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.m_bCancel = true;
      this.Hide();
    }

    private void trackBar80Hz_Scroll(object sender, EventArgs e)
    {
      int num = (int) this.audioSoundEditor1.Effects.EqualizerBandSetGain(80f, (float) this.trackBar80Hz.Value / 100f);
    }

    private void trackBar170Hz_Scroll(object sender, EventArgs e)
    {
      int num = (int) this.audioSoundEditor1.Effects.EqualizerBandSetGain(170f, (float) this.trackBar170Hz.Value / 100f);
    }

    private void trackBar310Hz_Scroll(object sender, EventArgs e)
    {
      int num = (int) this.audioSoundEditor1.Effects.EqualizerBandSetGain(310f, (float) this.trackBar310Hz.Value / 100f);
    }

    private void trackBar600Hz_Scroll(object sender, EventArgs e)
    {
      int num = (int) this.audioSoundEditor1.Effects.EqualizerBandSetGain(600f, (float) this.trackBar600Hz.Value / 100f);
    }

    private void trackBar1000Hz_Scroll(object sender, EventArgs e)
    {
      int num = (int) this.audioSoundEditor1.Effects.EqualizerBandSetGain(1000f, (float) this.trackBar1000Hz.Value / 100f);
    }

    private void trackBar3000Hz_Scroll(object sender, EventArgs e)
    {
      int num = (int) this.audioSoundEditor1.Effects.EqualizerBandSetGain(3000f, (float) this.trackBar3000Hz.Value / 100f);
    }

    private void trackBar6000Hz_Scroll(object sender, EventArgs e)
    {
      int num = (int) this.audioSoundEditor1.Effects.EqualizerBandSetGain(6000f, (float) this.trackBar6000Hz.Value / 100f);
    }

    private void trackBar12000Hz_Scroll(object sender, EventArgs e)
    {
      int num = (int) this.audioSoundEditor1.Effects.EqualizerBandSetGain(12000f, (float) this.trackBar12000Hz.Value / 100f);
    }

    private void trackBar14000Hz_Scroll(object sender, EventArgs e)
    {
      int num = (int) this.audioSoundEditor1.Effects.EqualizerBandSetGain(14000f, (float) this.trackBar14000Hz.Value / 100f);
    }

    private void trackBar16000Hz_Scroll(object sender, EventArgs e)
    {
      int num = (int) this.audioSoundEditor1.Effects.EqualizerBandSetGain(16000f, (float) this.trackBar16000Hz.Value / 100f);
    }
  }
}
