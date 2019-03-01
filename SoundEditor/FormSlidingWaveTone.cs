// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormSlidingWaveTone
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
  public class FormSlidingWaveTone : Form
  {
    private IContainer components;
    public ComboBox ComboInterpolation;
    public TextBox TextFreqEnd;
    public ComboBox ComboWaveType;
    public TextBox TextFreqStart;
    public TextBox TextAmplitude;
    public TextBox TextDuration;
    public Button CommandOK;
    public Button CommandCancel;
    public Label Label7;
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
      this.ComboInterpolation = new ComboBox();
      this.TextFreqEnd = new TextBox();
      this.ComboWaveType = new ComboBox();
      this.TextFreqStart = new TextBox();
      this.TextAmplitude = new TextBox();
      this.TextDuration = new TextBox();
      this.CommandOK = new Button();
      this.CommandCancel = new Button();
      this.Label7 = new Label();
      this.Label6 = new Label();
      this.Label1 = new Label();
      this.Label2 = new Label();
      this.Label3 = new Label();
      this.Label4 = new Label();
      this.SuspendLayout();
      this.ComboInterpolation.BackColor = SystemColors.Window;
      this.ComboInterpolation.Cursor = Cursors.Default;
      this.ComboInterpolation.DropDownStyle = ComboBoxStyle.DropDownList;
      this.ComboInterpolation.ForeColor = SystemColors.WindowText;
      this.ComboInterpolation.Location = new Point(184, 130);
      this.ComboInterpolation.Name = "ComboInterpolation";
      this.ComboInterpolation.RightToLeft = RightToLeft.No;
      this.ComboInterpolation.Size = new Size(145, 21);
      this.ComboInterpolation.TabIndex = 30;
      this.TextFreqEnd.AcceptsReturn = true;
      this.TextFreqEnd.BackColor = SystemColors.Window;
      this.TextFreqEnd.Cursor = Cursors.IBeam;
      this.TextFreqEnd.ForeColor = SystemColors.WindowText;
      this.TextFreqEnd.Location = new Point(184, 93);
      this.TextFreqEnd.MaxLength = 0;
      this.TextFreqEnd.Name = "TextFreqEnd";
      this.TextFreqEnd.RightToLeft = RightToLeft.No;
      this.TextFreqEnd.Size = new Size(145, 20);
      this.TextFreqEnd.TabIndex = 28;
      this.TextFreqEnd.Text = "1700";
      this.ComboWaveType.BackColor = SystemColors.Window;
      this.ComboWaveType.Cursor = Cursors.Default;
      this.ComboWaveType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.ComboWaveType.ForeColor = SystemColors.WindowText;
      this.ComboWaveType.Location = new Point(184, 18);
      this.ComboWaveType.Name = "ComboWaveType";
      this.ComboWaveType.RightToLeft = RightToLeft.No;
      this.ComboWaveType.Size = new Size(145, 21);
      this.ComboWaveType.TabIndex = 22;
      this.TextFreqStart.AcceptsReturn = true;
      this.TextFreqStart.BackColor = SystemColors.Window;
      this.TextFreqStart.Cursor = Cursors.IBeam;
      this.TextFreqStart.ForeColor = SystemColors.WindowText;
      this.TextFreqStart.Location = new Point(184, 57);
      this.TextFreqStart.MaxLength = 0;
      this.TextFreqStart.Name = "TextFreqStart";
      this.TextFreqStart.RightToLeft = RightToLeft.No;
      this.TextFreqStart.Size = new Size(145, 20);
      this.TextFreqStart.TabIndex = 21;
      this.TextFreqStart.Text = "700";
      this.TextAmplitude.AcceptsReturn = true;
      this.TextAmplitude.BackColor = SystemColors.Window;
      this.TextAmplitude.Cursor = Cursors.IBeam;
      this.TextAmplitude.ForeColor = SystemColors.WindowText;
      this.TextAmplitude.Location = new Point(184, 169);
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
      this.TextDuration.Location = new Point(184, 206);
      this.TextDuration.MaxLength = 0;
      this.TextDuration.Name = "TextDuration";
      this.TextDuration.RightToLeft = RightToLeft.No;
      this.TextDuration.Size = new Size(145, 20);
      this.TextDuration.TabIndex = 19;
      this.TextDuration.Text = "2000";
      this.CommandOK.BackColor = SystemColors.Control;
      this.CommandOK.Cursor = Cursors.Default;
      this.CommandOK.ForeColor = SystemColors.ControlText;
      this.CommandOK.Location = new Point(67, 282);
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
      this.CommandCancel.Location = new Point(195, 282);
      this.CommandCancel.Name = "CommandCancel";
      this.CommandCancel.RightToLeft = RightToLeft.No;
      this.CommandCancel.Size = new Size(105, 33);
      this.CommandCancel.TabIndex = 17;
      this.CommandCancel.Text = "Cancel";
      this.CommandCancel.UseVisualStyleBackColor = false;
      this.CommandCancel.Click += new System.EventHandler(this.CommandCancel_Click);
      this.Label7.BackColor = SystemColors.Control;
      this.Label7.Cursor = Cursors.Default;
      this.Label7.ForeColor = SystemColors.ControlText;
      this.Label7.Location = new Point(0, 132);
      this.Label7.Name = "Label7";
      this.Label7.RightToLeft = RightToLeft.No;
      this.Label7.Size = new Size(169, 17);
      this.Label7.TabIndex = 31;
      this.Label7.Text = "Interpolation";
      this.Label7.TextAlign = ContentAlignment.TopRight;
      this.Label6.BackColor = SystemColors.Control;
      this.Label6.Cursor = Cursors.Default;
      this.Label6.ForeColor = SystemColors.ControlText;
      this.Label6.Location = new Point(0, 94);
      this.Label6.Name = "Label6";
      this.Label6.RightToLeft = RightToLeft.No;
      this.Label6.Size = new Size(169, 17);
      this.Label6.TabIndex = 29;
      this.Label6.Text = "End frequency in Hz";
      this.Label6.TextAlign = ContentAlignment.TopRight;
      this.Label1.BackColor = SystemColors.Control;
      this.Label1.Cursor = Cursors.Default;
      this.Label1.ForeColor = SystemColors.ControlText;
      this.Label1.Location = new Point(0, 20);
      this.Label1.Name = "Label1";
      this.Label1.RightToLeft = RightToLeft.No;
      this.Label1.Size = new Size(169, 17);
      this.Label1.TabIndex = 27;
      this.Label1.Text = "Waveform type";
      this.Label1.TextAlign = ContentAlignment.TopRight;
      this.Label2.BackColor = SystemColors.Control;
      this.Label2.Cursor = Cursors.Default;
      this.Label2.ForeColor = SystemColors.ControlText;
      this.Label2.Location = new Point(0, 58);
      this.Label2.Name = "Label2";
      this.Label2.RightToLeft = RightToLeft.No;
      this.Label2.Size = new Size(169, 17);
      this.Label2.TabIndex = 26;
      this.Label2.Text = "Start frequency in Hz";
      this.Label2.TextAlign = ContentAlignment.TopRight;
      this.Label3.BackColor = SystemColors.Control;
      this.Label3.Cursor = Cursors.Default;
      this.Label3.ForeColor = SystemColors.ControlText;
      this.Label3.Location = new Point(0, 170);
      this.Label3.Name = "Label3";
      this.Label3.RightToLeft = RightToLeft.No;
      this.Label3.Size = new Size(169, 17);
      this.Label3.TabIndex = 25;
      this.Label3.Text = "Amplitude in % (0-100)";
      this.Label3.TextAlign = ContentAlignment.TopRight;
      this.Label4.BackColor = SystemColors.Control;
      this.Label4.Cursor = Cursors.Default;
      this.Label4.ForeColor = SystemColors.ControlText;
      this.Label4.Location = new Point(0, 207);
      this.Label4.Name = "Label4";
      this.Label4.RightToLeft = RightToLeft.No;
      this.Label4.Size = new Size(169, 17);
      this.Label4.TabIndex = 24;
      this.Label4.Text = "Duration in ms";
      this.Label4.TextAlign = ContentAlignment.TopRight;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(366, 335);
      this.ControlBox = false;
      this.Controls.Add((Control) this.ComboInterpolation);
      this.Controls.Add((Control) this.TextFreqEnd);
      this.Controls.Add((Control) this.ComboWaveType);
      this.Controls.Add((Control) this.TextFreqStart);
      this.Controls.Add((Control) this.TextAmplitude);
      this.Controls.Add((Control) this.TextDuration);
      this.Controls.Add((Control) this.CommandOK);
      this.Controls.Add((Control) this.CommandCancel);
      this.Controls.Add((Control) this.Label7);
      this.Controls.Add((Control) this.Label6);
      this.Controls.Add((Control) this.Label1);
      this.Controls.Add((Control) this.Label2);
      this.Controls.Add((Control) this.Label3);
      this.Controls.Add((Control) this.Label4);
      this.Name = nameof (FormSlidingWaveTone);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Sliding wave tone";
      this.Load += new System.EventHandler(this.FormSlidingWaveTone_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public FormSlidingWaveTone()
    {
      this.InitializeComponent();
    }

    private void FormSlidingWaveTone_Load(object sender, EventArgs e)
    {
      this.ComboWaveType.Items.Add((object) "Sine wave");
      this.ComboWaveType.Items.Add((object) "Square wave");
      this.ComboWaveType.Items.Add((object) "Sawtooth wave");
      this.ComboWaveType.Items.Add((object) "Square wave, no alias");
      this.ComboWaveType.SelectedIndex = 0;
      this.ComboInterpolation.Items.Add((object) "Linear");
      this.ComboInterpolation.Items.Add((object) "Logarithmic");
      this.ComboInterpolation.SelectedIndex = 0;
    }

    private void CommandOK_Click(object sender, EventArgs e)
    {
      float num1 = Convert.ToSingle(this.TextAmplitude.Text) / 100f;
      bool bLogInterpolation = false;
      if (this.ComboInterpolation.SelectedIndex == 1)
        bLogInterpolation = true;
      enumErrorCodes enumErrorCodes = this.audioSoundEditor1.SoundGenerator.SlidingWaveToneGenerate((enumSoundGenSlidingWaveTypes) this.ComboWaveType.SelectedIndex, 44100, 2, Convert.ToSingle(this.TextFreqStart.Text), Convert.ToSingle(this.TextFreqEnd.Text), num1, num1, bLogInterpolation, Convert.ToInt32(this.TextDuration.Text), 10, 10);
      if (enumErrorCodes == enumErrorCodes.ERR_NOERROR)
      {
        this.m_bCancel = false;
        this.Close();
      }
      else
      {
        int num2 = (int) MessageBox.Show("Cannot add the stream due to error " + enumErrorCodes.ToString());
      }
    }

    private void CommandCancel_Click(object sender, EventArgs e)
    {
      this.m_bCancel = true;
      this.Close();
    }
  }
}
