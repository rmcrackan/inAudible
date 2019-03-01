// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormPitch
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SoundEditor
{
  public class FormPitch : Form
  {
    private TrackBar trackBar1;
    private Label label2;
    private Label labelMessage;
    private Button buttonCancel;
    private Button buttonOK;
    private TextBox textBoxSemitones;
    private Container components;
    public bool m_bCancel;
    public float m_fChangeValue;

    public FormPitch()
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
      this.trackBar1 = new TrackBar();
      this.textBoxSemitones = new TextBox();
      this.label2 = new Label();
      this.labelMessage = new Label();
      this.buttonCancel = new Button();
      this.buttonOK = new Button();
      this.trackBar1.BeginInit();
      this.SuspendLayout();
      this.trackBar1.Location = new Point(68, 112);
      this.trackBar1.Maximum = 5000;
      this.trackBar1.Minimum = -5000;
      this.trackBar1.Name = "trackBar1";
      this.trackBar1.Size = new Size(184, 45);
      this.trackBar1.TabIndex = 18;
      this.trackBar1.TickFrequency = 500;
      this.trackBar1.Scroll += new EventHandler(this.trackBar1_Scroll);
      this.textBoxSemitones.Location = new Point(116, 72);
      this.textBoxSemitones.Name = "textBoxSemitones";
      this.textBoxSemitones.Size = new Size(88, 20);
      this.textBoxSemitones.TabIndex = 17;
      this.textBoxSemitones.Text = "";
      this.textBoxSemitones.KeyPress += new KeyPressEventHandler(this.textBoxSemitones_KeyPress);
      this.textBoxSemitones.TextChanged += new EventHandler(this.textBoxSemitones_TextChanged);
      this.label2.Location = new Point(88, 48);
      this.label2.Name = "label2";
      this.label2.Size = new Size(144, 24);
      this.label2.TabIndex = 16;
      this.label2.Text = "Semitones";
      this.label2.TextAlign = ContentAlignment.MiddleCenter;
      this.labelMessage.Location = new Point(8, 8);
      this.labelMessage.Name = "labelMessage";
      this.labelMessage.Size = new Size(304, 24);
      this.labelMessage.TabIndex = 15;
      this.labelMessage.Text = "Change Pitch without changing Tempo or Playback Rate";
      this.labelMessage.TextAlign = ContentAlignment.MiddleCenter;
      this.buttonCancel.DialogResult = DialogResult.Cancel;
      this.buttonCancel.Location = new Point(172, 176);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(104, 24);
      this.buttonCancel.TabIndex = 14;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
      this.buttonOK.Location = new Point(44, 176);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new Size(96, 24);
      this.buttonOK.TabIndex = 13;
      this.buttonOK.Text = "OK";
      this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
      this.AutoScaleBaseSize = new Size(5, 13);
      this.ClientSize = new Size(320, 222);
      this.ControlBox = false;
      this.Controls.Add((Control) this.trackBar1);
      this.Controls.Add((Control) this.textBoxSemitones);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.labelMessage);
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.buttonOK);
      this.KeyPreview = true;
      this.Name = nameof (FormPitch);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Pitch change";
      this.Load += new EventHandler(this.FormPitch_Load);
      this.trackBar1.EndInit();
      this.ResumeLayout(false);
    }

    private void FormPitch_Load(object sender, EventArgs e)
    {
      this.textBoxSemitones.Text = "0";
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      this.m_bCancel = false;
      this.Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.m_bCancel = true;
      this.Close();
    }

    private void trackBar1_Scroll(object sender, EventArgs e)
    {
      this.m_fChangeValue = (float) this.trackBar1.Value / 100f;
      this.textBoxSemitones.Text = this.m_fChangeValue.ToString();
    }

    private void textBoxSemitones_TextChanged(object sender, EventArgs e)
    {
      if (this.textBoxSemitones.Text == "" || this.textBoxSemitones.Text == "-")
        return;
      this.m_fChangeValue = Convert.ToSingle(this.textBoxSemitones.Text);
      this.trackBar1.Value = (int) ((double) this.m_fChangeValue * 100.0);
    }

    private void textBoxSemitones_KeyPress(object sender, KeyPressEventArgs e)
    {
      e.Handled = FormMain.CheckKeyPress(this.textBoxSemitones, Convert.ToInt32(e.KeyChar));
    }
  }
}
