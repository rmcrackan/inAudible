// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormBinauralWaveTone
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
  public class FormBinauralWaveTone : Form
  {
    private IContainer components;
    public TextBox TextFreqRight;
    public ComboBox ComboWaveType;
    public TextBox TextFreqLeft;
    public TextBox TextAmplitude;
    public TextBox TextDuration;
    public Button CommandOK;
    public Button CommandCancel;
    public Label Label6;
    public Label Label1;
    public Label Label2;
    public Label Label3;
    public Label Label4;
    internal AudioSoundEditor.AudioSoundEditor audioSoundEditor1;
    public bool m_bCancel;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.TextFreqRight = new TextBox();
      this.ComboWaveType = new ComboBox();
      this.TextFreqLeft = new TextBox();
      this.TextAmplitude = new TextBox();
      this.TextDuration = new TextBox();
      this.CommandOK = new Button();
      this.CommandCancel = new Button();
      this.Label6 = new Label();
      this.Label1 = new Label();
      this.Label2 = new Label();
      this.Label3 = new Label();
      this.Label4 = new Label();
      this.SuspendLayout();
      this.TextFreqRight.AcceptsReturn = true;
      this.TextFreqRight.BackColor = SystemColors.Window;
      this.TextFreqRight.Cursor = Cursors.IBeam;
      this.TextFreqRight.ForeColor = SystemColors.WindowText;
      this.TextFreqRight.Location = new Point(216, 93);
      this.TextFreqRight.MaxLength = 0;
      this.TextFreqRight.Name = "TextFreqRight";
      this.TextFreqRight.RightToLeft = RightToLeft.No;
      this.TextFreqRight.Size = new Size(145, 20);
      this.TextFreqRight.TabIndex = 28;
      this.TextFreqRight.Text = "440";
      this.ComboWaveType.BackColor = SystemColors.Window;
      this.ComboWaveType.Cursor = Cursors.Default;
      this.ComboWaveType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.ComboWaveType.ForeColor = SystemColors.WindowText;
      this.ComboWaveType.Location = new Point(216, 18);
      this.ComboWaveType.Name = "ComboWaveType";
      this.ComboWaveType.RightToLeft = RightToLeft.No;
      this.ComboWaveType.Size = new Size(145, 21);
      this.ComboWaveType.TabIndex = 22;
      this.TextFreqLeft.AcceptsReturn = true;
      this.TextFreqLeft.BackColor = SystemColors.Window;
      this.TextFreqLeft.Cursor = Cursors.IBeam;
      this.TextFreqLeft.ForeColor = SystemColors.WindowText;
      this.TextFreqLeft.Location = new Point(216, 57);
      this.TextFreqLeft.MaxLength = 0;
      this.TextFreqLeft.Name = "TextFreqLeft";
      this.TextFreqLeft.RightToLeft = RightToLeft.No;
      this.TextFreqLeft.Size = new Size(145, 20);
      this.TextFreqLeft.TabIndex = 21;
      this.TextFreqLeft.Text = "880";
      this.TextAmplitude.AcceptsReturn = true;
      this.TextAmplitude.BackColor = SystemColors.Window;
      this.TextAmplitude.Cursor = Cursors.IBeam;
      this.TextAmplitude.ForeColor = SystemColors.WindowText;
      this.TextAmplitude.Location = new Point(216, 129);
      this.TextAmplitude.MaxLength = 0;
      this.TextAmplitude.Name = "TextAmplitude";
      this.TextAmplitude.RightToLeft = RightToLeft.No;
      this.TextAmplitude.Size = new Size(145, 20);
      this.TextAmplitude.TabIndex = 20;
      this.TextAmplitude.Text = "80";
      this.TextDuration.AcceptsReturn = true;
      this.TextDuration.BackColor = SystemColors.Window;
      this.TextDuration.Cursor = Cursors.IBeam;
      this.TextDuration.ForeColor = SystemColors.WindowText;
      this.TextDuration.Location = new Point(216, 166);
      this.TextDuration.MaxLength = 0;
      this.TextDuration.Name = "TextDuration";
      this.TextDuration.RightToLeft = RightToLeft.No;
      this.TextDuration.Size = new Size(145, 20);
      this.TextDuration.TabIndex = 19;
      this.TextDuration.Text = "2000";
      this.CommandOK.BackColor = SystemColors.Control;
      this.CommandOK.Cursor = Cursors.Default;
      this.CommandOK.ForeColor = SystemColors.ControlText;
      this.CommandOK.Location = new Point(80, 222);
      this.CommandOK.Name = "CommandOK";
      this.CommandOK.RightToLeft = RightToLeft.No;
      this.CommandOK.Size = new Size(105, 33);
      this.CommandOK.TabIndex = 18;
      this.CommandOK.Text = "OK";
      this.CommandOK.UseVisualStyleBackColor = false;
      this.CommandOK.Click += new System.EventHandler(this.CommandOK_Click);
      this.CommandCancel.BackColor = SystemColors.Control;
      this.CommandCancel.Cursor = Cursors.Default;
      this.CommandCancel.DialogResult = DialogResult.Cancel;
      this.CommandCancel.ForeColor = SystemColors.ControlText;
      this.CommandCancel.Location = new Point(208, 222);
      this.CommandCancel.Name = "CommandCancel";
      this.CommandCancel.RightToLeft = RightToLeft.No;
      this.CommandCancel.Size = new Size(105, 33);
      this.CommandCancel.TabIndex = 17;
      this.CommandCancel.Text = "Cancel";
      this.CommandCancel.UseVisualStyleBackColor = false;
      this.CommandCancel.Click += new System.EventHandler(this.CommandCancel_Click);
      this.Label6.BackColor = SystemColors.Control;
      this.Label6.Cursor = Cursors.Default;
      this.Label6.ForeColor = SystemColors.ControlText;
      this.Label6.Location = new Point(12, 94);
      this.Label6.Name = "Label6";
      this.Label6.RightToLeft = RightToLeft.No;
      this.Label6.Size = new Size(189, 17);
      this.Label6.TabIndex = 29;
      this.Label6.Text = "Frequency on right channel in Hz";
      this.Label6.TextAlign = ContentAlignment.TopRight;
      this.Label1.BackColor = SystemColors.Control;
      this.Label1.Cursor = Cursors.Default;
      this.Label1.ForeColor = SystemColors.ControlText;
      this.Label1.Location = new Point(32, 20);
      this.Label1.Name = "Label1";
      this.Label1.RightToLeft = RightToLeft.No;
      this.Label1.Size = new Size(169, 17);
      this.Label1.TabIndex = 27;
      this.Label1.Text = "Waveform type";
      this.Label1.TextAlign = ContentAlignment.TopRight;
      this.Label2.BackColor = SystemColors.Control;
      this.Label2.Cursor = Cursors.Default;
      this.Label2.ForeColor = SystemColors.ControlText;
      this.Label2.Location = new Point(12, 58);
      this.Label2.Name = "Label2";
      this.Label2.RightToLeft = RightToLeft.No;
      this.Label2.Size = new Size(189, 17);
      this.Label2.TabIndex = 26;
      this.Label2.Text = "Frequency on left channel in Hz";
      this.Label2.TextAlign = ContentAlignment.TopRight;
      this.Label3.BackColor = SystemColors.Control;
      this.Label3.Cursor = Cursors.Default;
      this.Label3.ForeColor = SystemColors.ControlText;
      this.Label3.Location = new Point(32, 130);
      this.Label3.Name = "Label3";
      this.Label3.RightToLeft = RightToLeft.No;
      this.Label3.Size = new Size(169, 17);
      this.Label3.TabIndex = 25;
      this.Label3.Text = "Amplitude in % (0-100)";
      this.Label3.TextAlign = ContentAlignment.TopRight;
      this.Label4.BackColor = SystemColors.Control;
      this.Label4.Cursor = Cursors.Default;
      this.Label4.ForeColor = SystemColors.ControlText;
      this.Label4.Location = new Point(32, 167);
      this.Label4.Name = "Label4";
      this.Label4.RightToLeft = RightToLeft.No;
      this.Label4.Size = new Size(169, 17);
      this.Label4.TabIndex = 24;
      this.Label4.Text = "Duration in ms";
      this.Label4.TextAlign = ContentAlignment.TopRight;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(392, 276);
      this.ControlBox = false;
      this.Controls.Add((Control) this.TextFreqRight);
      this.Controls.Add((Control) this.ComboWaveType);
      this.Controls.Add((Control) this.TextFreqLeft);
      this.Controls.Add((Control) this.TextAmplitude);
      this.Controls.Add((Control) this.TextDuration);
      this.Controls.Add((Control) this.CommandOK);
      this.Controls.Add((Control) this.CommandCancel);
      this.Controls.Add((Control) this.Label6);
      this.Controls.Add((Control) this.Label1);
      this.Controls.Add((Control) this.Label2);
      this.Controls.Add((Control) this.Label3);
      this.Controls.Add((Control) this.Label4);
      this.Name = nameof (FormBinauralWaveTone);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Binaural wave tone";
      this.Load += new System.EventHandler(this.FormCompositeWaveTone_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public FormBinauralWaveTone()
    {
      this.InitializeComponent();
    }

    private void FormCompositeWaveTone_Load(object sender, EventArgs e)
    {
      this.ComboWaveType.Items.Add((object) "Sine wave");
      this.ComboWaveType.Items.Add((object) "Square wave");
      this.ComboWaveType.Items.Add((object) "Sawtooth wave");
      this.ComboWaveType.Items.Add((object) "Sawtooth negative wave");
      this.ComboWaveType.Items.Add((object) "Triangle wave");
      this.ComboWaveType.SelectedIndex = 0;
    }

    private void CommandOK_Click(object sender, EventArgs e)
    {
      float fAmplitude = Convert.ToSingle(this.TextAmplitude.Text) / 100f;
      enumErrorCodes enumErrorCodes1 = this.audioSoundEditor1.SoundGenerator.CompositeWaveTonePrepare(44100, 2);
      if (enumErrorCodes1 != enumErrorCodes.ERR_NOERROR)
      {
        int num1 = (int) MessageBox.Show("Cannot prepare the binaural wave stream due to error " + enumErrorCodes1.ToString());
        int num2 = (int) this.audioSoundEditor1.CloseSound();
      }
      else if (this.TextFreqLeft.Text == "" || this.TextFreqLeft.Text == "0")
      {
        int num1 = (int) MessageBox.Show("Please enter a frequency in Hz for the Left channel");
        int num2 = (int) this.audioSoundEditor1.CloseSound();
      }
      else if (this.TextFreqRight.Text == "" || this.TextFreqRight.Text == "0")
      {
        int num1 = (int) MessageBox.Show("Please enter a frequency in Hz for the Right channel");
        int num2 = (int) this.audioSoundEditor1.CloseSound();
      }
      else
      {
        enumErrorCodes enumErrorCodes2 = this.audioSoundEditor1.SoundGenerator.CompositeWaveToneAddNewWaveTone((enumSoundGenWaveTypes) this.ComboWaveType.SelectedIndex, Convert.ToSingle(this.TextFreqLeft.Text), fAmplitude, Convert.ToInt32(this.TextDuration.Text), 0, (short) 0);
        if (enumErrorCodes2 != enumErrorCodes.ERR_NOERROR)
        {
          int num1 = (int) MessageBox.Show("Cannot add the left wave stream due to error " + enumErrorCodes2.ToString());
          int num2 = (int) this.audioSoundEditor1.CloseSound();
        }
        else
        {
          enumErrorCodes enumErrorCodes3 = this.audioSoundEditor1.SoundGenerator.CompositeWaveToneAddNewWaveTone((enumSoundGenWaveTypes) this.ComboWaveType.SelectedIndex, Convert.ToSingle(this.TextFreqRight.Text), fAmplitude, Convert.ToInt32(this.TextDuration.Text), 0, (short) 1);
          if (enumErrorCodes3 != enumErrorCodes.ERR_NOERROR)
          {
            int num1 = (int) MessageBox.Show("Cannot add the right wave stream due to error " + enumErrorCodes3.ToString());
            int num2 = (int) this.audioSoundEditor1.CloseSound();
          }
          else
          {
            enumErrorCodes enumErrorCodes4 = this.audioSoundEditor1.SoundGenerator.CompositeWaveToneGenerate(10, 10);
            if (enumErrorCodes4 == enumErrorCodes.ERR_NOERROR)
            {
              this.m_bCancel = false;
              this.Close();
            }
            else
            {
              int num1 = (int) MessageBox.Show("Cannot generate the binaural wave stream due to error " + enumErrorCodes4.ToString());
              int num2 = (int) this.audioSoundEditor1.CloseSound();
            }
          }
        }
      }
    }

    private void CommandCancel_Click(object sender, EventArgs e)
    {
      this.m_bCancel = true;
      this.Close();
    }
  }
}
