// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormSpeech
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
  public class FormSpeech : Form
  {
    private IContainer components;
    public TextBox TextSpeechString;
    public Button CommandCancel;
    public Button CommandOK;
    public TextBox TextAmplitude;
    public Label Label1;
    public Label Label3;
    public TextBox TextPathname;
    public Label label2;
    private Button buttonBrowse;
    public Label label4;
    private ComboBox comboBoxVoice;
    private OpenFileDialog openFileDialog1;
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
      this.TextSpeechString = new TextBox();
      this.CommandCancel = new Button();
      this.CommandOK = new Button();
      this.TextAmplitude = new TextBox();
      this.Label1 = new Label();
      this.Label3 = new Label();
      this.TextPathname = new TextBox();
      this.label2 = new Label();
      this.buttonBrowse = new Button();
      this.label4 = new Label();
      this.comboBoxVoice = new ComboBox();
      this.openFileDialog1 = new OpenFileDialog();
      this.SuspendLayout();
      this.TextSpeechString.AcceptsReturn = true;
      this.TextSpeechString.BackColor = SystemColors.Window;
      this.TextSpeechString.Cursor = Cursors.IBeam;
      this.TextSpeechString.ForeColor = SystemColors.WindowText;
      this.TextSpeechString.Location = new Point(184, 21);
      this.TextSpeechString.MaxLength = 0;
      this.TextSpeechString.Name = "TextSpeechString";
      this.TextSpeechString.RightToLeft = RightToLeft.No;
      this.TextSpeechString.Size = new Size(424, 20);
      this.TextSpeechString.TabIndex = 20;
      this.TextSpeechString.Text = "This is a simple text";
      this.CommandCancel.BackColor = SystemColors.Control;
      this.CommandCancel.Cursor = Cursors.Default;
      this.CommandCancel.DialogResult = DialogResult.Cancel;
      this.CommandCancel.ForeColor = SystemColors.ControlText;
      this.CommandCancel.Location = new Point(372, 206);
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
      this.CommandOK.Location = new Point(244, 206);
      this.CommandOK.Name = "CommandOK";
      this.CommandOK.RightToLeft = RightToLeft.No;
      this.CommandOK.Size = new Size(105, 33);
      this.CommandOK.TabIndex = 14;
      this.CommandOK.Text = "OK";
      this.CommandOK.UseVisualStyleBackColor = false;
      this.CommandOK.Click += new System.EventHandler(this.CommandOK_Click);
      this.TextAmplitude.AcceptsReturn = true;
      this.TextAmplitude.BackColor = SystemColors.Window;
      this.TextAmplitude.Cursor = Cursors.IBeam;
      this.TextAmplitude.ForeColor = SystemColors.WindowText;
      this.TextAmplitude.Location = new Point(184, (int) sbyte.MaxValue);
      this.TextAmplitude.MaxLength = 0;
      this.TextAmplitude.Name = "TextAmplitude";
      this.TextAmplitude.RightToLeft = RightToLeft.No;
      this.TextAmplitude.Size = new Size(145, 20);
      this.TextAmplitude.TabIndex = 12;
      this.TextAmplitude.Text = "100";
      this.Label1.BackColor = SystemColors.Control;
      this.Label1.Cursor = Cursors.Default;
      this.Label1.ForeColor = SystemColors.ControlText;
      this.Label1.Location = new Point(0, 23);
      this.Label1.Name = "Label1";
      this.Label1.RightToLeft = RightToLeft.No;
      this.Label1.Size = new Size(169, 17);
      this.Label1.TabIndex = 21;
      this.Label1.Text = "String of text to speech";
      this.Label1.TextAlign = ContentAlignment.TopRight;
      this.Label3.BackColor = SystemColors.Control;
      this.Label3.Cursor = Cursors.Default;
      this.Label3.ForeColor = SystemColors.ControlText;
      this.Label3.Location = new Point(0, 129);
      this.Label3.Name = "Label3";
      this.Label3.RightToLeft = RightToLeft.No;
      this.Label3.Size = new Size(169, 17);
      this.Label3.TabIndex = 17;
      this.Label3.Text = "Amplitude in % (0-100)";
      this.Label3.TextAlign = ContentAlignment.TopRight;
      this.TextPathname.AcceptsReturn = true;
      this.TextPathname.BackColor = SystemColors.Window;
      this.TextPathname.Cursor = Cursors.IBeam;
      this.TextPathname.ForeColor = SystemColors.WindowText;
      this.TextPathname.Location = new Point(184, 56);
      this.TextPathname.MaxLength = 0;
      this.TextPathname.Name = "TextPathname";
      this.TextPathname.RightToLeft = RightToLeft.No;
      this.TextPathname.Size = new Size(424, 20);
      this.TextPathname.TabIndex = 22;
      this.label2.BackColor = SystemColors.Control;
      this.label2.Cursor = Cursors.Default;
      this.label2.ForeColor = SystemColors.ControlText;
      this.label2.Location = new Point(0, 58);
      this.label2.Name = "label2";
      this.label2.RightToLeft = RightToLeft.No;
      this.label2.Size = new Size(169, 17);
      this.label2.TabIndex = 23;
      this.label2.Text = "File of text to speech";
      this.label2.TextAlign = ContentAlignment.TopRight;
      this.buttonBrowse.Location = new Point(614, 56);
      this.buttonBrowse.Name = "buttonBrowse";
      this.buttonBrowse.Size = new Size(95, 21);
      this.buttonBrowse.TabIndex = 24;
      this.buttonBrowse.Text = "Browse...";
      this.buttonBrowse.UseVisualStyleBackColor = true;
      this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
      this.label4.BackColor = SystemColors.Control;
      this.label4.Cursor = Cursors.Default;
      this.label4.ForeColor = SystemColors.ControlText;
      this.label4.Location = new Point(0, 93);
      this.label4.Name = "label4";
      this.label4.RightToLeft = RightToLeft.No;
      this.label4.Size = new Size(169, 17);
      this.label4.TabIndex = 25;
      this.label4.Text = "Speaking voice";
      this.label4.TextAlign = ContentAlignment.TopRight;
      this.comboBoxVoice.FormattingEnabled = true;
      this.comboBoxVoice.Location = new Point(184, 91);
      this.comboBoxVoice.Name = "comboBoxVoice";
      this.comboBoxVoice.Size = new Size(198, 21);
      this.comboBoxVoice.TabIndex = 26;
      this.openFileDialog1.FileName = "openFileDialog1";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(721, 259);
      this.ControlBox = false;
      this.Controls.Add((Control) this.comboBoxVoice);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.buttonBrowse);
      this.Controls.Add((Control) this.TextPathname);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.TextSpeechString);
      this.Controls.Add((Control) this.CommandCancel);
      this.Controls.Add((Control) this.CommandOK);
      this.Controls.Add((Control) this.TextAmplitude);
      this.Controls.Add((Control) this.Label1);
      this.Controls.Add((Control) this.Label3);
      this.Name = nameof (FormSpeech);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Speech text";
      this.Load += new System.EventHandler(this.FormSpeech_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public FormSpeech()
    {
      this.InitializeComponent();
    }

    private void CommandOK_Click(object sender, EventArgs e)
    {
      float fAmplitude = Convert.ToSingle(this.TextAmplitude.Text) / 100f;
      enumErrorCodes enumErrorCodes = enumErrorCodes.ERR_NOERROR;
      if (this.TextSpeechString.Text != "")
        enumErrorCodes = this.audioSoundEditor1.SoundGenerator.SpeechGenerateFromString(44100, 2, this.TextSpeechString.Text, this.comboBoxVoice.SelectedIndex, fAmplitude, true);
      else if (this.TextPathname.Text != "")
        enumErrorCodes = this.audioSoundEditor1.SoundGenerator.SpeechGenerateFromFile(44100, 2, this.TextPathname.Text, this.comboBoxVoice.SelectedIndex, fAmplitude, true);
      if (enumErrorCodes == enumErrorCodes.ERR_NOERROR)
      {
        this.Close();
      }
      else
      {
        int num = (int) MessageBox.Show("Cannot add the stream due to error " + enumErrorCodes.ToString());
      }
    }

    private void CommandCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void FormSpeech_Load(object sender, EventArgs e)
    {
      int nVoices = 0;
      int num1 = (int) this.audioSoundEditor1.SpeechVoicesNumGet(ref nVoices);
      if (nVoices > 0)
      {
        for (int nVoice = 0; nVoice < nVoices; ++nVoice)
          this.comboBoxVoice.Items.Add((object) this.audioSoundEditor1.SpeechVoiceAttributeGet(nVoice, enumSapiVoiceAttributes.SAPI_VOICE_ATTRIBUTE_NAME));
        this.comboBoxVoice.SelectedIndex = 0;
        this.CommandOK.Enabled = true;
      }
      else
      {
        int num2 = (int) MessageBox.Show("No Speech API voice is installed inside the system");
        this.CommandOK.Enabled = false;
      }
    }

    private void buttonBrowse_Click(object sender, EventArgs e)
    {
      this.openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
      this.openFileDialog1.Title = "Choose a text file to speech";
      if (this.openFileDialog1.ShowDialog() != DialogResult.OK || this.openFileDialog1.FileName == "")
        return;
      this.TextPathname.Text = this.openFileDialog1.FileName;
      this.TextSpeechString.Text = "";
    }
  }
}
