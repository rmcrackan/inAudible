// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormNoise
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
  public class FormNoise : Form
  {
    internal AudioSoundEditor.AudioSoundEditor audioSoundEditor1;
    public bool m_bCancel;
    private IContainer components;
    public ComboBox ComboNoiseType;
    public TextBox TextAmplitude;
    public TextBox TextDuration;
    public Button CommandOK;
    public Button CommandCancel;
    public Label Label1;
    public Label Label3;
    public Label Label4;

    public FormNoise()
    {
      this.InitializeComponent();
    }

    private void FormNoise_Load(object sender, EventArgs e)
    {
      this.ComboNoiseType.Items.Add((object) "White");
      this.ComboNoiseType.Items.Add((object) "Pink");
      this.ComboNoiseType.Items.Add((object) "Brown");
      this.ComboNoiseType.SelectedIndex = 0;
    }

    private void CommandOK_Click(object sender, EventArgs e)
    {
      enumErrorCodes enumErrorCodes = this.audioSoundEditor1.SoundGenerator.NoiseGenerate((enumSoundGenNoiseTypes) this.ComboNoiseType.SelectedIndex, 44100, 2, 0.0f, Convert.ToSingle(this.TextAmplitude.Text) / 100f, Convert.ToInt32(this.TextDuration.Text), 0, 0);
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

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.ComboNoiseType = new ComboBox();
      this.TextAmplitude = new TextBox();
      this.TextDuration = new TextBox();
      this.CommandOK = new Button();
      this.CommandCancel = new Button();
      this.Label1 = new Label();
      this.Label3 = new Label();
      this.Label4 = new Label();
      this.SuspendLayout();
      this.ComboNoiseType.BackColor = SystemColors.Window;
      this.ComboNoiseType.Cursor = Cursors.Default;
      this.ComboNoiseType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.ComboNoiseType.ForeColor = SystemColors.WindowText;
      this.ComboNoiseType.Location = new Point(186, 24);
      this.ComboNoiseType.Name = "ComboNoiseType";
      this.ComboNoiseType.RightToLeft = RightToLeft.No;
      this.ComboNoiseType.Size = new Size(145, 21);
      this.ComboNoiseType.TabIndex = 15;
      this.TextAmplitude.AcceptsReturn = true;
      this.TextAmplitude.BackColor = SystemColors.Window;
      this.TextAmplitude.Cursor = Cursors.IBeam;
      this.TextAmplitude.ForeColor = SystemColors.WindowText;
      this.TextAmplitude.Location = new Point(186, 57);
      this.TextAmplitude.MaxLength = 0;
      this.TextAmplitude.Name = "TextAmplitude";
      this.TextAmplitude.RightToLeft = RightToLeft.No;
      this.TextAmplitude.Size = new Size(145, 20);
      this.TextAmplitude.TabIndex = 14;
      this.TextAmplitude.Text = "80";
      this.TextDuration.AcceptsReturn = true;
      this.TextDuration.BackColor = SystemColors.Window;
      this.TextDuration.Cursor = Cursors.IBeam;
      this.TextDuration.ForeColor = SystemColors.WindowText;
      this.TextDuration.Location = new Point(186, 92);
      this.TextDuration.MaxLength = 0;
      this.TextDuration.Name = "TextDuration";
      this.TextDuration.RightToLeft = RightToLeft.No;
      this.TextDuration.Size = new Size(145, 20);
      this.TextDuration.TabIndex = 13;
      this.TextDuration.Text = "2000";
      this.CommandOK.BackColor = SystemColors.Control;
      this.CommandOK.Cursor = Cursors.Default;
      this.CommandOK.ForeColor = SystemColors.ControlText;
      this.CommandOK.Location = new Point(66, 168);
      this.CommandOK.Name = "CommandOK";
      this.CommandOK.RightToLeft = RightToLeft.No;
      this.CommandOK.Size = new Size(105, 33);
      this.CommandOK.TabIndex = 12;
      this.CommandOK.Text = "OK";
      this.CommandOK.UseVisualStyleBackColor = false;
      this.CommandOK.Click += new System.EventHandler(this.CommandOK_Click);
      this.CommandCancel.BackColor = SystemColors.Control;
      this.CommandCancel.Cursor = Cursors.Default;
      this.CommandCancel.DialogResult = DialogResult.Cancel;
      this.CommandCancel.ForeColor = SystemColors.ControlText;
      this.CommandCancel.Location = new Point(194, 168);
      this.CommandCancel.Name = "CommandCancel";
      this.CommandCancel.RightToLeft = RightToLeft.No;
      this.CommandCancel.Size = new Size(105, 33);
      this.CommandCancel.TabIndex = 11;
      this.CommandCancel.Text = "Cancel";
      this.CommandCancel.UseVisualStyleBackColor = false;
      this.CommandCancel.Click += new System.EventHandler(this.CommandCancel_Click);
      this.Label1.BackColor = SystemColors.Control;
      this.Label1.Cursor = Cursors.Default;
      this.Label1.ForeColor = SystemColors.ControlText;
      this.Label1.Location = new Point(2, 26);
      this.Label1.Name = "Label1";
      this.Label1.RightToLeft = RightToLeft.No;
      this.Label1.Size = new Size(169, 17);
      this.Label1.TabIndex = 19;
      this.Label1.Text = "Noise type";
      this.Label1.TextAlign = ContentAlignment.TopRight;
      this.Label3.BackColor = SystemColors.Control;
      this.Label3.Cursor = Cursors.Default;
      this.Label3.ForeColor = SystemColors.ControlText;
      this.Label3.Location = new Point(2, 58);
      this.Label3.Name = "Label3";
      this.Label3.RightToLeft = RightToLeft.No;
      this.Label3.Size = new Size(169, 17);
      this.Label3.TabIndex = 18;
      this.Label3.Text = "Amplitude in % (0-100)";
      this.Label3.TextAlign = ContentAlignment.TopRight;
      this.Label4.BackColor = SystemColors.Control;
      this.Label4.Cursor = Cursors.Default;
      this.Label4.ForeColor = SystemColors.ControlText;
      this.Label4.Location = new Point(2, 93);
      this.Label4.Name = "Label4";
      this.Label4.RightToLeft = RightToLeft.No;
      this.Label4.Size = new Size(169, 17);
      this.Label4.TabIndex = 17;
      this.Label4.Text = "Duration in ms";
      this.Label4.TextAlign = ContentAlignment.TopRight;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(365, 221);
      this.ControlBox = false;
      this.Controls.Add((Control) this.ComboNoiseType);
      this.Controls.Add((Control) this.TextAmplitude);
      this.Controls.Add((Control) this.TextDuration);
      this.Controls.Add((Control) this.CommandOK);
      this.Controls.Add((Control) this.CommandCancel);
      this.Controls.Add((Control) this.Label1);
      this.Controls.Add((Control) this.Label3);
      this.Controls.Add((Control) this.Label4);
      this.Name = nameof (FormNoise);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Noise";
      this.Load += new System.EventHandler(this.FormNoise_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
