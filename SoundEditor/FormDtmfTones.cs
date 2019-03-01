// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormDtmfTones
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
  public class FormDtmfTones : Form
  {
    internal AudioSoundEditor.AudioSoundEditor audioSoundEditor1;
    public bool m_bCancel;
    private IContainer components;
    public TextBox TextSilenceDuration;
    public TextBox TextDtmfString;
    public Button CommandCancel;
    public Button CommandOK;
    public TextBox TextToneDuration;
    public TextBox TextAmplitude;
    public Label Label2;
    public Label Label1;
    public Label Label4;
    public Label Label3;

    public FormDtmfTones()
    {
      this.InitializeComponent();
    }

    private void CommandOK_Click(object sender, EventArgs e)
    {
      enumErrorCodes enumErrorCodes = this.audioSoundEditor1.SoundGenerator.DtmfStringGenerate(this.TextDtmfString.Text, 44100, 2, Convert.ToInt32(this.TextToneDuration.Text), Convert.ToInt32(this.TextSilenceDuration.Text), 10, 10, Convert.ToSingle(this.TextAmplitude.Text) / 100f);
      if (enumErrorCodes == enumErrorCodes.ERR_NOERROR)
      {
        this.m_bCancel = false;
        this.Close();
      }
      else
      {
        int num = (int) MessageBox.Show("Cannot create the stream due to error " + enumErrorCodes.ToString());
      }
    }

    private void CommandCancel_Click(object sender, EventArgs e)
    {
      this.m_bCancel = true;
      this.Close();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.TextSilenceDuration = new TextBox();
      this.TextDtmfString = new TextBox();
      this.CommandCancel = new Button();
      this.CommandOK = new Button();
      this.TextToneDuration = new TextBox();
      this.TextAmplitude = new TextBox();
      this.Label2 = new Label();
      this.Label1 = new Label();
      this.Label4 = new Label();
      this.Label3 = new Label();
      this.SuspendLayout();
      this.TextSilenceDuration.AcceptsReturn = true;
      this.TextSilenceDuration.BackColor = SystemColors.Window;
      this.TextSilenceDuration.Cursor = Cursors.IBeam;
      this.TextSilenceDuration.ForeColor = SystemColors.WindowText;
      this.TextSilenceDuration.Location = new Point(184, 94);
      this.TextSilenceDuration.MaxLength = 0;
      this.TextSilenceDuration.Name = "TextSilenceDuration";
      this.TextSilenceDuration.RightToLeft = RightToLeft.No;
      this.TextSilenceDuration.Size = new Size(145, 20);
      this.TextSilenceDuration.TabIndex = 22;
      this.TextSilenceDuration.Text = "50";
      this.TextDtmfString.AcceptsReturn = true;
      this.TextDtmfString.BackColor = SystemColors.Window;
      this.TextDtmfString.Cursor = Cursors.IBeam;
      this.TextDtmfString.ForeColor = SystemColors.WindowText;
      this.TextDtmfString.Location = new Point(184, 21);
      this.TextDtmfString.MaxLength = 0;
      this.TextDtmfString.Name = "TextDtmfString";
      this.TextDtmfString.RightToLeft = RightToLeft.No;
      this.TextDtmfString.Size = new Size(145, 20);
      this.TextDtmfString.TabIndex = 20;
      this.TextDtmfString.Text = "01234567890";
      this.CommandCancel.BackColor = SystemColors.Control;
      this.CommandCancel.Cursor = Cursors.Default;
      this.CommandCancel.DialogResult = DialogResult.Cancel;
      this.CommandCancel.ForeColor = SystemColors.ControlText;
      this.CommandCancel.Location = new Point(196, 206);
      this.CommandCancel.Name = "CommandCancel";
      this.CommandCancel.RightToLeft = RightToLeft.No;
      this.CommandCancel.Size = new Size(105, 33);
      this.CommandCancel.TabIndex = 15;
      this.CommandCancel.Text = "Cancel";
      this.CommandCancel.UseVisualStyleBackColor = false;
      this.CommandCancel.Click += new System.EventHandler(this.CommandCancel_Click);
      this.CommandOK.BackColor = SystemColors.Control;
      this.CommandOK.Cursor = Cursors.Default;
      this.CommandOK.ForeColor = SystemColors.ControlText;
      this.CommandOK.Location = new Point(68, 206);
      this.CommandOK.Name = "CommandOK";
      this.CommandOK.RightToLeft = RightToLeft.No;
      this.CommandOK.Size = new Size(105, 33);
      this.CommandOK.TabIndex = 14;
      this.CommandOK.Text = "OK";
      this.CommandOK.UseVisualStyleBackColor = false;
      this.CommandOK.Click += new System.EventHandler(this.CommandOK_Click);
      this.TextToneDuration.AcceptsReturn = true;
      this.TextToneDuration.BackColor = SystemColors.Window;
      this.TextToneDuration.Cursor = Cursors.IBeam;
      this.TextToneDuration.ForeColor = SystemColors.WindowText;
      this.TextToneDuration.Location = new Point(184, 58);
      this.TextToneDuration.MaxLength = 0;
      this.TextToneDuration.Name = "TextToneDuration";
      this.TextToneDuration.RightToLeft = RightToLeft.No;
      this.TextToneDuration.Size = new Size(145, 20);
      this.TextToneDuration.TabIndex = 13;
      this.TextToneDuration.Text = "150";
      this.TextAmplitude.AcceptsReturn = true;
      this.TextAmplitude.BackColor = SystemColors.Window;
      this.TextAmplitude.Cursor = Cursors.IBeam;
      this.TextAmplitude.ForeColor = SystemColors.WindowText;
      this.TextAmplitude.Location = new Point(184, 130);
      this.TextAmplitude.MaxLength = 0;
      this.TextAmplitude.Name = "TextAmplitude";
      this.TextAmplitude.RightToLeft = RightToLeft.No;
      this.TextAmplitude.Size = new Size(145, 20);
      this.TextAmplitude.TabIndex = 12;
      this.TextAmplitude.Text = "80";
      this.Label2.BackColor = SystemColors.Control;
      this.Label2.Cursor = Cursors.Default;
      this.Label2.ForeColor = SystemColors.ControlText;
      this.Label2.Location = new Point(0, 95);
      this.Label2.Name = "Label2";
      this.Label2.RightToLeft = RightToLeft.No;
      this.Label2.Size = new Size(169, 17);
      this.Label2.TabIndex = 23;
      this.Label2.Text = "Silence between tones in ms";
      this.Label2.TextAlign = ContentAlignment.TopRight;
      this.Label1.BackColor = SystemColors.Control;
      this.Label1.Cursor = Cursors.Default;
      this.Label1.ForeColor = SystemColors.ControlText;
      this.Label1.Location = new Point(0, 22);
      this.Label1.Name = "Label1";
      this.Label1.RightToLeft = RightToLeft.No;
      this.Label1.Size = new Size(169, 17);
      this.Label1.TabIndex = 21;
      this.Label1.Text = "DTMF string";
      this.Label1.TextAlign = ContentAlignment.TopRight;
      this.Label4.BackColor = SystemColors.Control;
      this.Label4.Cursor = Cursors.Default;
      this.Label4.ForeColor = SystemColors.ControlText;
      this.Label4.Location = new Point(0, 59);
      this.Label4.Name = "Label4";
      this.Label4.RightToLeft = RightToLeft.No;
      this.Label4.Size = new Size(169, 17);
      this.Label4.TabIndex = 18;
      this.Label4.Text = "Tone duration in ms";
      this.Label4.TextAlign = ContentAlignment.TopRight;
      this.Label3.BackColor = SystemColors.Control;
      this.Label3.Cursor = Cursors.Default;
      this.Label3.ForeColor = SystemColors.ControlText;
      this.Label3.Location = new Point(0, 131);
      this.Label3.Name = "Label3";
      this.Label3.RightToLeft = RightToLeft.No;
      this.Label3.Size = new Size(169, 17);
      this.Label3.TabIndex = 17;
      this.Label3.Text = "Amplitude in % (0-100)";
      this.Label3.TextAlign = ContentAlignment.TopRight;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(368, 259);
      this.ControlBox = false;
      this.Controls.Add((Control) this.TextSilenceDuration);
      this.Controls.Add((Control) this.TextDtmfString);
      this.Controls.Add((Control) this.CommandCancel);
      this.Controls.Add((Control) this.CommandOK);
      this.Controls.Add((Control) this.TextToneDuration);
      this.Controls.Add((Control) this.TextAmplitude);
      this.Controls.Add((Control) this.Label2);
      this.Controls.Add((Control) this.Label1);
      this.Controls.Add((Control) this.Label4);
      this.Controls.Add((Control) this.Label3);
      this.Name = nameof (FormDtmfTones);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "DTMF tones";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
