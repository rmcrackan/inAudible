// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormWaveTone
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
  public class FormWaveTone : Form
  {
    private IContainer components;
    public Button CommandCancel;
    public Button CommandOK;
    public TextBox TextDuration;
    public TextBox TextAmplitude;
    public TextBox TextFreq;
    public ComboBox ComboWaveType;
    public Label Label4;
    public Label Label3;
    public Label Label2;
    public Label Label1;
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
      this.CommandCancel = new Button();
      this.CommandOK = new Button();
      this.TextDuration = new TextBox();
      this.TextAmplitude = new TextBox();
      this.TextFreq = new TextBox();
      this.ComboWaveType = new ComboBox();
      this.Label4 = new Label();
      this.Label3 = new Label();
      this.Label2 = new Label();
      this.Label1 = new Label();
      this.SuspendLayout();
      this.CommandCancel.BackColor = SystemColors.Control;
      this.CommandCancel.Cursor = Cursors.Default;
      this.CommandCancel.DialogResult = DialogResult.Cancel;
      this.CommandCancel.ForeColor = SystemColors.ControlText;
      this.CommandCancel.Location = new Point(195, 203);
      this.CommandCancel.Name = "CommandCancel";
      this.CommandCancel.RightToLeft = RightToLeft.No;
      this.CommandCancel.Size = new Size(105, 33);
      this.CommandCancel.TabIndex = 21;
      this.CommandCancel.Text = "Cancel";
      this.CommandCancel.UseVisualStyleBackColor = false;
      this.CommandCancel.Click += new System.EventHandler(this.CommandCancel_Click);
      this.CommandOK.BackColor = SystemColors.Control;
      this.CommandOK.Cursor = Cursors.Default;
      this.CommandOK.ForeColor = SystemColors.ControlText;
      this.CommandOK.Location = new Point(67, 203);
      this.CommandOK.Name = "CommandOK";
      this.CommandOK.RightToLeft = RightToLeft.No;
      this.CommandOK.Size = new Size(105, 33);
      this.CommandOK.TabIndex = 20;
      this.CommandOK.Text = "OK";
      this.CommandOK.UseVisualStyleBackColor = false;
      this.CommandOK.Click += new System.EventHandler(this.CommandOK_Click);
      this.TextDuration.AcceptsReturn = true;
      this.TextDuration.BackColor = SystemColors.Window;
      this.TextDuration.Cursor = Cursors.IBeam;
      this.TextDuration.ForeColor = SystemColors.WindowText;
      this.TextDuration.Location = new Point(183, 125);
      this.TextDuration.MaxLength = 0;
      this.TextDuration.Name = "TextDuration";
      this.TextDuration.RightToLeft = RightToLeft.No;
      this.TextDuration.Size = new Size(145, 20);
      this.TextDuration.TabIndex = 19;
      this.TextDuration.Text = "2000";
      this.TextAmplitude.AcceptsReturn = true;
      this.TextAmplitude.BackColor = SystemColors.Window;
      this.TextAmplitude.Cursor = Cursors.IBeam;
      this.TextAmplitude.ForeColor = SystemColors.WindowText;
      this.TextAmplitude.Location = new Point(183, 89);
      this.TextAmplitude.MaxLength = 0;
      this.TextAmplitude.Name = "TextAmplitude";
      this.TextAmplitude.RightToLeft = RightToLeft.No;
      this.TextAmplitude.Size = new Size(145, 20);
      this.TextAmplitude.TabIndex = 18;
      this.TextAmplitude.Text = "80";
      this.TextFreq.AcceptsReturn = true;
      this.TextFreq.BackColor = SystemColors.Window;
      this.TextFreq.Cursor = Cursors.IBeam;
      this.TextFreq.ForeColor = SystemColors.WindowText;
      this.TextFreq.Location = new Point(183, 54);
      this.TextFreq.MaxLength = 0;
      this.TextFreq.Name = "TextFreq";
      this.TextFreq.RightToLeft = RightToLeft.No;
      this.TextFreq.Size = new Size(145, 20);
      this.TextFreq.TabIndex = 17;
      this.TextFreq.Text = "700";
      this.ComboWaveType.BackColor = SystemColors.Window;
      this.ComboWaveType.Cursor = Cursors.Default;
      this.ComboWaveType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.ComboWaveType.ForeColor = SystemColors.WindowText;
      this.ComboWaveType.Location = new Point(183, 16);
      this.ComboWaveType.Name = "ComboWaveType";
      this.ComboWaveType.RightToLeft = RightToLeft.No;
      this.ComboWaveType.Size = new Size(145, 21);
      this.ComboWaveType.TabIndex = 16;
      this.Label4.BackColor = SystemColors.Control;
      this.Label4.Cursor = Cursors.Default;
      this.Label4.ForeColor = SystemColors.ControlText;
      this.Label4.Location = new Point(-1, 126);
      this.Label4.Name = "Label4";
      this.Label4.RightToLeft = RightToLeft.No;
      this.Label4.Size = new Size(169, 17);
      this.Label4.TabIndex = 15;
      this.Label4.Text = "Duration in ms";
      this.Label4.TextAlign = ContentAlignment.TopRight;
      this.Label3.BackColor = SystemColors.Control;
      this.Label3.Cursor = Cursors.Default;
      this.Label3.ForeColor = SystemColors.ControlText;
      this.Label3.Location = new Point(-1, 90);
      this.Label3.Name = "Label3";
      this.Label3.RightToLeft = RightToLeft.No;
      this.Label3.Size = new Size(169, 17);
      this.Label3.TabIndex = 14;
      this.Label3.Text = "Amplitude in % (0-100)";
      this.Label3.TextAlign = ContentAlignment.TopRight;
      this.Label2.BackColor = SystemColors.Control;
      this.Label2.Cursor = Cursors.Default;
      this.Label2.ForeColor = SystemColors.ControlText;
      this.Label2.Location = new Point(-1, 55);
      this.Label2.Name = "Label2";
      this.Label2.RightToLeft = RightToLeft.No;
      this.Label2.Size = new Size(169, 17);
      this.Label2.TabIndex = 13;
      this.Label2.Text = "Frequency in Hz";
      this.Label2.TextAlign = ContentAlignment.TopRight;
      this.Label1.BackColor = SystemColors.Control;
      this.Label1.Cursor = Cursors.Default;
      this.Label1.ForeColor = SystemColors.ControlText;
      this.Label1.Location = new Point(-1, 18);
      this.Label1.Name = "Label1";
      this.Label1.RightToLeft = RightToLeft.No;
      this.Label1.Size = new Size(169, 17);
      this.Label1.TabIndex = 12;
      this.Label1.Text = "Waveform type";
      this.Label1.TextAlign = ContentAlignment.TopRight;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(366, (int) byte.MaxValue);
      this.ControlBox = false;
      this.Controls.Add((Control) this.CommandCancel);
      this.Controls.Add((Control) this.CommandOK);
      this.Controls.Add((Control) this.TextDuration);
      this.Controls.Add((Control) this.TextAmplitude);
      this.Controls.Add((Control) this.TextFreq);
      this.Controls.Add((Control) this.ComboWaveType);
      this.Controls.Add((Control) this.Label4);
      this.Controls.Add((Control) this.Label3);
      this.Controls.Add((Control) this.Label2);
      this.Controls.Add((Control) this.Label1);
      this.Name = nameof (FormWaveTone);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Wave Tone";
      this.Load += new System.EventHandler(this.FormWaveTone_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public FormWaveTone()
    {
      this.InitializeComponent();
    }

    private void FormWaveTone_Load(object sender, EventArgs e)
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
      enumErrorCodes enumErrorCodes = this.audioSoundEditor1.SoundGenerator.WaveToneGenerate((enumSoundGenWaveTypes) this.ComboWaveType.SelectedIndex, 44100, 2, (float) Convert.ToInt32(this.TextFreq.Text), Convert.ToSingle(this.TextAmplitude.Text) / 100f, Convert.ToInt32(this.TextDuration.Text), 10, 10);
      if (enumErrorCodes == enumErrorCodes.ERR_NOERROR)
      {
        this.m_bCancel = false;
        this.Close();
      }
      else
      {
        int num = (int) MessageBox.Show("Cannot add the stream due to error " + enumErrorCodes.ToString());
      }
    }

    private void CommandCancel_Click(object sender, EventArgs e)
    {
      this.m_bCancel = true;
      this.Close();
    }
  }
}
