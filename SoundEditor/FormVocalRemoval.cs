// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormVocalRemoval
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SoundEditor
{
  public class FormVocalRemoval : Form
  {
    private IContainer components;
    private Button buttonCancel;
    private Button buttonOK;
    private Label labelCutoffFreq;
    private Label labelGain;
    private Label labelVoiceAtt;
    private TrackBar trackBarCutoffFreq;
    private TrackBar trackBarGain;
    private TrackBar trackBarAttenuation;
    public bool m_bCancel;
    public int m_nAttenuation;
    public int m_nGain;
    public int m_nCutoffFreq;

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
      this.labelCutoffFreq = new Label();
      this.labelGain = new Label();
      this.labelVoiceAtt = new Label();
      this.trackBarCutoffFreq = new TrackBar();
      this.trackBarGain = new TrackBar();
      this.trackBarAttenuation = new TrackBar();
      this.trackBarCutoffFreq.BeginInit();
      this.trackBarGain.BeginInit();
      this.trackBarAttenuation.BeginInit();
      this.SuspendLayout();
      this.buttonCancel.DialogResult = DialogResult.Cancel;
      this.buttonCancel.Location = new Point(118, 211);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(104, 24);
      this.buttonCancel.TabIndex = 15;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
      this.buttonOK.Location = new Point(12, 211);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new Size(96, 24);
      this.buttonOK.TabIndex = 14;
      this.buttonOK.Text = "OK";
      this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
      this.labelCutoffFreq.Location = new Point(30, (int) sbyte.MaxValue);
      this.labelCutoffFreq.Name = "labelCutoffFreq";
      this.labelCutoffFreq.Size = new Size(176, 16);
      this.labelCutoffFreq.TabIndex = 78;
      this.labelCutoffFreq.Text = "Cutoff Frequency";
      this.labelCutoffFreq.TextAlign = ContentAlignment.MiddleCenter;
      this.labelGain.Location = new Point(30, 68);
      this.labelGain.Name = "labelGain";
      this.labelGain.Size = new Size(176, 16);
      this.labelGain.TabIndex = 77;
      this.labelGain.Text = "Gain";
      this.labelGain.TextAlign = ContentAlignment.MiddleCenter;
      this.labelVoiceAtt.Location = new Point(30, 9);
      this.labelVoiceAtt.Name = "labelVoiceAtt";
      this.labelVoiceAtt.Size = new Size(176, 16);
      this.labelVoiceAtt.TabIndex = 76;
      this.labelVoiceAtt.Text = "Voice attenuation";
      this.labelVoiceAtt.TextAlign = ContentAlignment.MiddleCenter;
      this.trackBarCutoffFreq.AutoSize = false;
      this.trackBarCutoffFreq.Location = new Point(30, 143);
      this.trackBarCutoffFreq.Maximum = 1000;
      this.trackBarCutoffFreq.Minimum = 20;
      this.trackBarCutoffFreq.Name = "trackBarCutoffFreq";
      this.trackBarCutoffFreq.Size = new Size(176, 40);
      this.trackBarCutoffFreq.TabIndex = 75;
      this.trackBarCutoffFreq.TickFrequency = 50;
      this.trackBarCutoffFreq.TickStyle = TickStyle.Both;
      this.trackBarCutoffFreq.Value = 200;
      this.trackBarCutoffFreq.Scroll += new EventHandler(this.trackBarCutoffFreq_Scroll);
      this.trackBarGain.AutoSize = false;
      this.trackBarGain.Location = new Point(30, 84);
      this.trackBarGain.Maximum = 200;
      this.trackBarGain.Name = "trackBarGain";
      this.trackBarGain.Size = new Size(176, 40);
      this.trackBarGain.TabIndex = 74;
      this.trackBarGain.TickFrequency = 10;
      this.trackBarGain.TickStyle = TickStyle.Both;
      this.trackBarGain.Value = 100;
      this.trackBarGain.Scroll += new EventHandler(this.trackBarGain_Scroll);
      this.trackBarAttenuation.AutoSize = false;
      this.trackBarAttenuation.Location = new Point(30, 25);
      this.trackBarAttenuation.Maximum = 100;
      this.trackBarAttenuation.Name = "trackBarAttenuation";
      this.trackBarAttenuation.Size = new Size(176, 40);
      this.trackBarAttenuation.TabIndex = 73;
      this.trackBarAttenuation.TickFrequency = 5;
      this.trackBarAttenuation.TickStyle = TickStyle.Both;
      this.trackBarAttenuation.Value = 100;
      this.trackBarAttenuation.Scroll += new EventHandler(this.trackBarAttenuation_Scroll);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(235, 249);
      this.ControlBox = false;
      this.Controls.Add((Control) this.labelCutoffFreq);
      this.Controls.Add((Control) this.labelGain);
      this.Controls.Add((Control) this.labelVoiceAtt);
      this.Controls.Add((Control) this.trackBarCutoffFreq);
      this.Controls.Add((Control) this.trackBarGain);
      this.Controls.Add((Control) this.trackBarAttenuation);
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.buttonOK);
      this.Name = nameof (FormVocalRemoval);
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Remove clicks and pops";
      this.Load += new EventHandler(this.FormVocalRemoval_Load);
      this.trackBarCutoffFreq.EndInit();
      this.trackBarGain.EndInit();
      this.trackBarAttenuation.EndInit();
      this.ResumeLayout(false);
    }

    public FormVocalRemoval()
    {
      this.InitializeComponent();
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      this.m_nAttenuation = this.trackBarAttenuation.Value;
      this.m_nGain = this.trackBarGain.Value;
      this.m_nCutoffFreq = this.trackBarCutoffFreq.Value;
      this.m_bCancel = false;
      this.Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.m_bCancel = true;
      this.Close();
    }

    private void FormVocalRemoval_Load(object sender, EventArgs e)
    {
      this.m_nAttenuation = this.trackBarAttenuation.Value;
      this.m_nGain = this.trackBarGain.Value;
      this.m_nCutoffFreq = this.trackBarCutoffFreq.Value;
      this.labelVoiceAtt.Text = "Voice attenuation: " + (object) this.trackBarAttenuation.Value + "%";
      this.labelGain.Text = "Gain: " + (object) this.trackBarGain.Value + "%";
      this.labelCutoffFreq.Text = "Cutoff Frequency: " + (object) this.trackBarCutoffFreq.Value + " Hz";
    }

    private void trackBarAttenuation_Scroll(object sender, EventArgs e)
    {
      this.labelVoiceAtt.Text = "Voice attenuation: " + (object) this.trackBarAttenuation.Value + "%";
    }

    private void trackBarGain_Scroll(object sender, EventArgs e)
    {
      this.labelGain.Text = "Gain: " + (object) this.trackBarGain.Value + "%";
    }

    private void trackBarCutoffFreq_Scroll(object sender, EventArgs e)
    {
      this.labelCutoffFreq.Text = "Cutoff Frequency: " + (object) this.trackBarCutoffFreq.Value + " Hz";
    }
  }
}
