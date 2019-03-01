// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormCompositeWaveTone
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
  public class FormCompositeWaveTone : Form
  {
    private IContainer components;
    public TextBox TextFreqSecond;
    public ComboBox ComboWaveType;
    public TextBox TextFreqFirst;
    public TextBox TextAmplitude;
    public TextBox TextDuration;
    public Button CommandOK;
    public Button CommandCancel;
    public Label Label6;
    public Label Label1;
    public Label Label2;
    public Label Label3;
    public Label Label4;
    public TextBox TextFreqFourth;
    public TextBox TextFreqThird;
    public Label label5;
    public Label label8;
    private Label label7;
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
      this.TextFreqSecond = new TextBox();
      this.ComboWaveType = new ComboBox();
      this.TextFreqFirst = new TextBox();
      this.TextAmplitude = new TextBox();
      this.TextDuration = new TextBox();
      this.CommandOK = new Button();
      this.CommandCancel = new Button();
      this.Label6 = new Label();
      this.Label1 = new Label();
      this.Label2 = new Label();
      this.Label3 = new Label();
      this.Label4 = new Label();
      this.TextFreqFourth = new TextBox();
      this.TextFreqThird = new TextBox();
      this.label5 = new Label();
      this.label8 = new Label();
      this.label7 = new Label();
      this.SuspendLayout();
      this.TextFreqSecond.AcceptsReturn = true;
      this.TextFreqSecond.BackColor = SystemColors.Window;
      this.TextFreqSecond.Cursor = Cursors.IBeam;
      this.TextFreqSecond.ForeColor = SystemColors.WindowText;
      this.TextFreqSecond.Location = new Point(185, 140);
      this.TextFreqSecond.MaxLength = 0;
      this.TextFreqSecond.Name = "TextFreqSecond";
      this.TextFreqSecond.RightToLeft = RightToLeft.No;
      this.TextFreqSecond.Size = new Size(145, 20);
      this.TextFreqSecond.TabIndex = 28;
      this.TextFreqSecond.Text = "440";
      this.ComboWaveType.BackColor = SystemColors.Window;
      this.ComboWaveType.Cursor = Cursors.Default;
      this.ComboWaveType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.ComboWaveType.ForeColor = SystemColors.WindowText;
      this.ComboWaveType.Location = new Point(185, 65);
      this.ComboWaveType.Name = "ComboWaveType";
      this.ComboWaveType.RightToLeft = RightToLeft.No;
      this.ComboWaveType.Size = new Size(145, 21);
      this.ComboWaveType.TabIndex = 22;
      this.TextFreqFirst.AcceptsReturn = true;
      this.TextFreqFirst.BackColor = SystemColors.Window;
      this.TextFreqFirst.Cursor = Cursors.IBeam;
      this.TextFreqFirst.ForeColor = SystemColors.WindowText;
      this.TextFreqFirst.Location = new Point(185, 104);
      this.TextFreqFirst.MaxLength = 0;
      this.TextFreqFirst.Name = "TextFreqFirst";
      this.TextFreqFirst.RightToLeft = RightToLeft.No;
      this.TextFreqFirst.Size = new Size(145, 20);
      this.TextFreqFirst.TabIndex = 21;
      this.TextFreqFirst.Text = "880";
      this.TextAmplitude.AcceptsReturn = true;
      this.TextAmplitude.BackColor = SystemColors.Window;
      this.TextAmplitude.Cursor = Cursors.IBeam;
      this.TextAmplitude.ForeColor = SystemColors.WindowText;
      this.TextAmplitude.Location = new Point(185, 249);
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
      this.TextDuration.Location = new Point(185, 286);
      this.TextDuration.MaxLength = 0;
      this.TextDuration.Name = "TextDuration";
      this.TextDuration.RightToLeft = RightToLeft.No;
      this.TextDuration.Size = new Size(145, 20);
      this.TextDuration.TabIndex = 19;
      this.TextDuration.Text = "2000";
      this.CommandOK.BackColor = SystemColors.Control;
      this.CommandOK.Cursor = Cursors.Default;
      this.CommandOK.ForeColor = SystemColors.ControlText;
      this.CommandOK.Location = new Point(67, 339);
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
      this.CommandCancel.Location = new Point(195, 339);
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
      this.Label6.Location = new Point(1, 141);
      this.Label6.Name = "Label6";
      this.Label6.RightToLeft = RightToLeft.No;
      this.Label6.Size = new Size(169, 17);
      this.Label6.TabIndex = 29;
      this.Label6.Text = "Second frequency in Hz";
      this.Label6.TextAlign = ContentAlignment.TopRight;
      this.Label1.BackColor = SystemColors.Control;
      this.Label1.Cursor = Cursors.Default;
      this.Label1.ForeColor = SystemColors.ControlText;
      this.Label1.Location = new Point(1, 67);
      this.Label1.Name = "Label1";
      this.Label1.RightToLeft = RightToLeft.No;
      this.Label1.Size = new Size(169, 17);
      this.Label1.TabIndex = 27;
      this.Label1.Text = "Waveform type";
      this.Label1.TextAlign = ContentAlignment.TopRight;
      this.Label2.BackColor = SystemColors.Control;
      this.Label2.Cursor = Cursors.Default;
      this.Label2.ForeColor = SystemColors.ControlText;
      this.Label2.Location = new Point(1, 105);
      this.Label2.Name = "Label2";
      this.Label2.RightToLeft = RightToLeft.No;
      this.Label2.Size = new Size(169, 17);
      this.Label2.TabIndex = 26;
      this.Label2.Text = "First frequency in Hz";
      this.Label2.TextAlign = ContentAlignment.TopRight;
      this.Label3.BackColor = SystemColors.Control;
      this.Label3.Cursor = Cursors.Default;
      this.Label3.ForeColor = SystemColors.ControlText;
      this.Label3.Location = new Point(1, 250);
      this.Label3.Name = "Label3";
      this.Label3.RightToLeft = RightToLeft.No;
      this.Label3.Size = new Size(169, 17);
      this.Label3.TabIndex = 25;
      this.Label3.Text = "Amplitude in % (0-100)";
      this.Label3.TextAlign = ContentAlignment.TopRight;
      this.Label4.BackColor = SystemColors.Control;
      this.Label4.Cursor = Cursors.Default;
      this.Label4.ForeColor = SystemColors.ControlText;
      this.Label4.Location = new Point(1, 287);
      this.Label4.Name = "Label4";
      this.Label4.RightToLeft = RightToLeft.No;
      this.Label4.Size = new Size(169, 17);
      this.Label4.TabIndex = 24;
      this.Label4.Text = "Duration in ms";
      this.Label4.TextAlign = ContentAlignment.TopRight;
      this.TextFreqFourth.AcceptsReturn = true;
      this.TextFreqFourth.BackColor = SystemColors.Window;
      this.TextFreqFourth.Cursor = Cursors.IBeam;
      this.TextFreqFourth.ForeColor = SystemColors.WindowText;
      this.TextFreqFourth.Location = new Point(185, 214);
      this.TextFreqFourth.MaxLength = 0;
      this.TextFreqFourth.Name = "TextFreqFourth";
      this.TextFreqFourth.RightToLeft = RightToLeft.No;
      this.TextFreqFourth.Size = new Size(145, 20);
      this.TextFreqFourth.TabIndex = 34;
      this.TextFreqFourth.Text = "110";
      this.TextFreqThird.AcceptsReturn = true;
      this.TextFreqThird.BackColor = SystemColors.Window;
      this.TextFreqThird.Cursor = Cursors.IBeam;
      this.TextFreqThird.ForeColor = SystemColors.WindowText;
      this.TextFreqThird.Location = new Point(185, 178);
      this.TextFreqThird.MaxLength = 0;
      this.TextFreqThird.Name = "TextFreqThird";
      this.TextFreqThird.RightToLeft = RightToLeft.No;
      this.TextFreqThird.Size = new Size(145, 20);
      this.TextFreqThird.TabIndex = 32;
      this.TextFreqThird.Text = "220";
      this.label5.BackColor = SystemColors.Control;
      this.label5.Cursor = Cursors.Default;
      this.label5.ForeColor = SystemColors.ControlText;
      this.label5.Location = new Point(1, 215);
      this.label5.Name = "label5";
      this.label5.RightToLeft = RightToLeft.No;
      this.label5.Size = new Size(169, 17);
      this.label5.TabIndex = 35;
      this.label5.Text = "Fourth frequency in Hz";
      this.label5.TextAlign = ContentAlignment.TopRight;
      this.label8.BackColor = SystemColors.Control;
      this.label8.Cursor = Cursors.Default;
      this.label8.ForeColor = SystemColors.ControlText;
      this.label8.Location = new Point(1, 179);
      this.label8.Name = "label8";
      this.label8.RightToLeft = RightToLeft.No;
      this.label8.Size = new Size(169, 17);
      this.label8.TabIndex = 33;
      this.label8.Text = "Third frequency in Hz";
      this.label8.TextAlign = ContentAlignment.TopRight;
      this.label7.Location = new Point(21, 11);
      this.label7.Name = "label7";
      this.label7.Size = new Size(324, 39);
      this.label7.TabIndex = 36;
      this.label7.Text = "If you don't want a specific frequency to be added to the composite wave tone, set its value to 0 or leave its field empty";
      this.label7.TextAlign = ContentAlignment.MiddleCenter;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(366, 384);
      this.ControlBox = false;
      this.Controls.Add((Control) this.label7);
      this.Controls.Add((Control) this.TextFreqFourth);
      this.Controls.Add((Control) this.TextFreqThird);
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.label8);
      this.Controls.Add((Control) this.TextFreqSecond);
      this.Controls.Add((Control) this.ComboWaveType);
      this.Controls.Add((Control) this.TextFreqFirst);
      this.Controls.Add((Control) this.TextAmplitude);
      this.Controls.Add((Control) this.TextDuration);
      this.Controls.Add((Control) this.CommandOK);
      this.Controls.Add((Control) this.CommandCancel);
      this.Controls.Add((Control) this.Label6);
      this.Controls.Add((Control) this.Label1);
      this.Controls.Add((Control) this.Label2);
      this.Controls.Add((Control) this.Label3);
      this.Controls.Add((Control) this.Label4);
      this.Name = nameof (FormCompositeWaveTone);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Composite wave tone";
      this.Load += new System.EventHandler(this.FormCompositeWaveTone_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public FormCompositeWaveTone()
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
        int num1 = (int) MessageBox.Show("Cannot prepare the composite wave stream due to error " + enumErrorCodes1.ToString());
        int num2 = (int) this.audioSoundEditor1.CloseSound();
      }
      else
      {
        if (this.TextFreqFirst.Text != "" && this.TextFreqFirst.Text != "0")
        {
          enumErrorCodes enumErrorCodes2 = this.audioSoundEditor1.SoundGenerator.CompositeWaveToneAddNewWaveTone((enumSoundGenWaveTypes) this.ComboWaveType.SelectedIndex, Convert.ToSingle(this.TextFreqFirst.Text), fAmplitude, Convert.ToInt32(this.TextDuration.Text), 0, (short) -1);
          if (enumErrorCodes2 != enumErrorCodes.ERR_NOERROR)
          {
            int num1 = (int) MessageBox.Show("Cannot add the first wave stream due to error " + enumErrorCodes2.ToString());
            int num2 = (int) this.audioSoundEditor1.CloseSound();
            return;
          }
        }
        if (this.TextFreqSecond.Text != "" && this.TextFreqSecond.Text != "0")
        {
          enumErrorCodes enumErrorCodes2 = this.audioSoundEditor1.SoundGenerator.CompositeWaveToneAddNewWaveTone((enumSoundGenWaveTypes) this.ComboWaveType.SelectedIndex, Convert.ToSingle(this.TextFreqSecond.Text), fAmplitude, Convert.ToInt32(this.TextDuration.Text), 0, (short) -1);
          if (enumErrorCodes2 != enumErrorCodes.ERR_NOERROR)
          {
            int num1 = (int) MessageBox.Show("Cannot add the second wave stream due to error " + enumErrorCodes2.ToString());
            int num2 = (int) this.audioSoundEditor1.CloseSound();
            return;
          }
        }
        if (this.TextFreqThird.Text != "" && this.TextFreqThird.Text != "0")
        {
          enumErrorCodes enumErrorCodes2 = this.audioSoundEditor1.SoundGenerator.CompositeWaveToneAddNewWaveTone((enumSoundGenWaveTypes) this.ComboWaveType.SelectedIndex, Convert.ToSingle(this.TextFreqThird.Text), fAmplitude, Convert.ToInt32(this.TextDuration.Text), 0, (short) -1);
          if (enumErrorCodes2 != enumErrorCodes.ERR_NOERROR)
          {
            int num1 = (int) MessageBox.Show("Cannot add the third wave stream due to error " + enumErrorCodes2.ToString());
            int num2 = (int) this.audioSoundEditor1.CloseSound();
            return;
          }
        }
        if (this.TextFreqFourth.Text != "" && this.TextFreqFourth.Text != "0")
        {
          enumErrorCodes enumErrorCodes2 = this.audioSoundEditor1.SoundGenerator.CompositeWaveToneAddNewWaveTone((enumSoundGenWaveTypes) this.ComboWaveType.SelectedIndex, Convert.ToSingle(this.TextFreqFourth.Text), fAmplitude, Convert.ToInt32(this.TextDuration.Text), 0, (short) -1);
          if (enumErrorCodes2 != enumErrorCodes.ERR_NOERROR)
          {
            int num1 = (int) MessageBox.Show("Cannot add the fourth wave stream due to error " + enumErrorCodes2.ToString());
            int num2 = (int) this.audioSoundEditor1.CloseSound();
            return;
          }
        }
        enumErrorCodes enumErrorCodes3 = this.audioSoundEditor1.SoundGenerator.CompositeWaveToneGenerate(10, 10);
        if (enumErrorCodes3 == enumErrorCodes.ERR_NOERROR)
        {
          this.m_bCancel = false;
          this.Close();
        }
        else
        {
          int num1 = (int) MessageBox.Show("Cannot generate the composite wave stream due to error " + enumErrorCodes3.ToString());
          int num2 = (int) this.audioSoundEditor1.CloseSound();
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
