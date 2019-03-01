// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormTempoRate
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SoundEditor
{
  public class FormTempoRate : Form
  {
    private Button buttonCancel;
    private Button buttonOK;
    private Label label2;
    private TrackBar trackBar1;
    private Container components;
    private Label labelMessage;
    private TextBox textBoxPercentage;
    public bool m_bIsChangingTempo;
    public bool m_bCancel;
    public float m_fChangePercentage;

    public FormTempoRate()
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
      this.buttonCancel = new Button();
      this.buttonOK = new Button();
      this.labelMessage = new Label();
      this.label2 = new Label();
      this.textBoxPercentage = new TextBox();
      this.trackBar1 = new TrackBar();
      this.trackBar1.BeginInit();
      this.SuspendLayout();
      this.buttonCancel.DialogResult = DialogResult.Cancel;
      this.buttonCancel.Location = new Point(176, 176);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(104, 24);
      this.buttonCancel.TabIndex = 8;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
      this.buttonOK.Location = new Point(48, 176);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new Size(96, 24);
      this.buttonOK.TabIndex = 7;
      this.buttonOK.Text = "OK";
      this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
      this.labelMessage.Location = new Point(12, 8);
      this.labelMessage.Name = "labelMessage";
      this.labelMessage.Size = new Size(304, 24);
      this.labelMessage.TabIndex = 9;
      this.labelMessage.Text = "-";
      this.labelMessage.TextAlign = ContentAlignment.MiddleCenter;
      this.label2.Location = new Point(92, 48);
      this.label2.Name = "label2";
      this.label2.Size = new Size(144, 24);
      this.label2.TabIndex = 10;
      this.label2.Text = "Percentage";
      this.label2.TextAlign = ContentAlignment.MiddleCenter;
      this.textBoxPercentage.Location = new Point(120, 72);
      this.textBoxPercentage.Name = "textBoxPercentage";
      this.textBoxPercentage.Size = new Size(88, 20);
      this.textBoxPercentage.TabIndex = 11;
      this.textBoxPercentage.Text = "";
      this.textBoxPercentage.TextChanged += new EventHandler(this.textBoxPercentage_TextChanged);
      this.trackBar1.Location = new Point(72, 112);
      this.trackBar1.Maximum = 9000;
      this.trackBar1.Minimum = -9000;
      this.trackBar1.Name = "trackBar1";
      this.trackBar1.Size = new Size(184, 45);
      this.trackBar1.TabIndex = 12;
      this.trackBar1.TickFrequency = 1000;
      this.trackBar1.Scroll += new EventHandler(this.trackBar1_Scroll);
      this.AutoScaleBaseSize = new Size(5, 13);
      this.ClientSize = new Size(328, 214);
      this.ControlBox = false;
      this.Controls.Add((Control) this.trackBar1);
      this.Controls.Add((Control) this.textBoxPercentage);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.labelMessage);
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.buttonOK);
      this.KeyPreview = true;
      this.Name = nameof (FormTempoRate);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Tempo change";
      this.KeyPress += new KeyPressEventHandler(this.FormTempoRate_KeyPress);
      this.Load += new EventHandler(this.FormTempoRate_Load);
      this.trackBar1.EndInit();
      this.ResumeLayout(false);
    }

    private void FormTempoRate_Load(object sender, EventArgs e)
    {
      if (this.m_bIsChangingTempo)
      {
        this.Text = "Tempo change";
        this.labelMessage.Text = "Change Tempo without affecting Pitch";
      }
      else
      {
        this.Text = "Playback Rate change";
        this.labelMessage.Text = "Change Playback Rate affecting both Tempo and Pitch";
      }
      this.textBoxPercentage.Text = "0";
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
      this.m_fChangePercentage = (float) this.trackBar1.Value / 100f;
      this.textBoxPercentage.Text = this.m_fChangePercentage.ToString();
    }

    private void textBoxPercentage_TextChanged(object sender, EventArgs e)
    {
      if (this.textBoxPercentage.Text == "" || this.textBoxPercentage.Text == "-")
        return;
      this.m_fChangePercentage = Convert.ToSingle(this.textBoxPercentage.Text);
      this.trackBar1.Value = (int) ((double) this.m_fChangePercentage * 100.0);
    }

    private void FormTempoRate_KeyPress(object sender, KeyPressEventArgs e)
    {
      e.Handled = FormMain.CheckKeyPress(this.textBoxPercentage, Convert.ToInt32(e.KeyChar));
    }
  }
}
